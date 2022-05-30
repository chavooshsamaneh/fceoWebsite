using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_OfficeRegister_OfficeAgentInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["OagId"]) || string.IsNullOrEmpty(Request.QueryString["OfReId"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }

            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            TSP.DataManager.Permission per = FindPermissionClass();
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            // btnDelete.Enabled = per.CanDelete;
            //btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            Session["IsEdited_OffAgent"] = false;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["APageMode"].ToString());
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                AgentId.Value = Server.HtmlDecode(Request.QueryString["OagId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OagId = Utility.DecryptQS(AgentId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);

            OfficeInfoUserControl.OfReId = int.Parse(OfReId);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            if (PageMode != "New" && !per.CanView)
            {
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                          + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
            }
            switch (PageMode)
            {
                case "View":

                    Disable();

                    if (string.IsNullOrEmpty(OagId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    //btnDelete.Enabled = false;
                    //btnDelete2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(OagId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";


                    break;


                case "New":
                    Enable();
                    //btnDelete.Enabled = false;
                    //btnDelete2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;

                case "Edit":
                    Enable();

                    // btnDelete.Enabled = per.CanDelete;
                    // btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(OagId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(OagId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";



                    break;


            }

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "Document")
                CheckWorkFlowPermissionForDoc();
            else if (Department == "MemberShip")
                CheckWorkFlowPermissionForOffice();

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if ((Convert.ToBoolean(ReqManager[0]["Requester"]) == false) || (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                }
            }
            TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
            if (!string.IsNullOrEmpty(OagId))
            {
                AgentManager.FindByCode(int.Parse(OagId));
                if (AgentManager.Count == 1)
                {
                    if (AgentManager[0]["OfReId"].ToString() != OfReId)
                    {
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                    }
                }
            }


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            //this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnDelete"] != null)
        //    this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
              + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OagId = Utility.DecryptQS(AgentId.Value);

        if (string.IsNullOrEmpty(OfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(OagId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();

                TSP.DataManager.Permission per = FindPermissionClass();
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }

        }

    }
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{

    //    string PageMode = Utility.DecryptQS(PgMode.Value);
    //    string OfId = Utility.DecryptQS(OfficeId.Value);
    //    string OagId = Utility.DecryptQS(AgentId.Value);

    //    if (string.IsNullOrEmpty(OfId))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //        return;
    //    }
    //    if (string.IsNullOrEmpty(OagId))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //        return;
    //    }
    //    if (string.IsNullOrEmpty(PageMode))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //        return;
    //    }
    //    else
    //    {

    //        if (PageMode == "Edit" && (!string.IsNullOrEmpty(OagId)))
    //        {
    //            Delete(int.Parse(OagId));
    //        }
    //    }

    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OagId = Utility.DecryptQS(AgentId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
            {

                Insert();


            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(OagId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(int.Parse(OagId));
                }

            }

        }



    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = FindPermissionClass();

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        AgentId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }

    #endregion

    #region Methods
    protected void FillForm(int OagId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        AgentManager.FindByCode(OagId);
        if (AgentManager.Count == 1)
        {

            txtOfAgName.Text = AgentManager[0]["OagName"].ToString();

            if (AgentManager[0]["Tel"].ToString() != "")
            {
                if (AgentManager[0]["Tel"].ToString().IndexOf("-") > 0)
                {
                    txtOfAgTel_pre.Text = AgentManager[0]["Tel"].ToString().Substring(0, AgentManager[0]["Tel"].ToString().IndexOf("-"));
                    txtOfAgTel.Text = AgentManager[0]["Tel"].ToString().Substring(AgentManager[0]["Tel"].ToString().IndexOf("-") + 1, AgentManager[0]["Tel"].ToString().Length - AgentManager[0]["Tel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfAgTel.Text = AgentManager[0]["Tel"].ToString();
                }
            }

            if (AgentManager[0]["Fax"].ToString() != "")
            {
                if (AgentManager[0]["Fax"].ToString().IndexOf("-") > 0)
                {
                    txtOfAgFax_pre.Text = AgentManager[0]["Fax"].ToString().Substring(0, AgentManager[0]["Fax"].ToString().IndexOf("-"));
                    txtOfAgFax.Text = AgentManager[0]["Fax"].ToString().Substring(AgentManager[0]["Fax"].ToString().IndexOf("-") + 1, AgentManager[0]["Fax"].ToString().Length - AgentManager[0]["Fax"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfAgFax.Text = AgentManager[0]["Fax"].ToString();
                }
            }
            txtOfAgEmail1.Text = AgentManager[0]["Email"].ToString();
            txtOfAgWebsite.Text = AgentManager[0]["Website"].ToString();
            txtOfAgAddress.Text = AgentManager[0]["Address"].ToString();
            txtOfAgResponsible.Text = AgentManager[0]["Responsible"].ToString();
        }


    }
    protected void Insert()
    {
        if (IsPageRefresh)
            return;
        string pagemode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);

        if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.OfficeAgentManager OffAgManager = new TSP.DataManager.OfficeAgentManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(OffAgManager);

        try
        {
            OffAgManager.FindByOfCode(int.Parse(OfId));
            for (int i = 0; i < OffAgManager.Count; i++)
            {
                if (OffAgManager[i]["OagName"].ToString() == txtOfAgName.Text && OffAgManager[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            DataRow dr = OffAgManager.NewRow();
            dr["OagId"] = 0;
            dr["OfReId"] = int.Parse(OfReId);

            dr["OfId"] = int.Parse(OfId);
            dr["OagName"] = txtOfAgName.Text;
            if (txtOfAgTel_pre.Text != "" && txtOfAgTel.Text != "")
                dr["Tel"] = txtOfAgTel_pre.Text + "-" + txtOfAgTel.Text;
            else if (txtOfAgTel.Text != "")
                dr["Tel"] = txtOfAgTel.Text;
            if (txtOfAgFax_pre.Text != "" && txtOfAgFax.Text != "")
                dr["Fax"] = txtOfAgFax_pre.Text + "-" + txtOfAgFax.Text;
            else if (txtOfAgFax.Text != "")
                dr["Fax"] = txtOfAgFax.Text;
            if (txtOfAgEmail1.Text != "")
                dr["Email"] = txtOfAgEmail1.Text;
            if (txtOfAgWebsite.Text != "")
                dr["Website"] = txtOfAgWebsite.Text;
            if (txtOfAgAddress.Text != "")
                dr["Address"] = txtOfAgAddress.Text;
            if (txtOfAgResponsible.Text != "")
                dr["Responsible"] = txtOfAgResponsible.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            OffAgManager.AddRow(dr);
            trans.BeginSave();
            if (OffAgManager.Save() == 1)
            {

                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffAgent"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeAgent;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, int.Parse(OfReId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    AgentId.Value = Utility.EncryptQS(OffAgManager[0]["OagId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    Session["IsEdited_OffAgent"] = true;

                    if (Session["OffMenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                        arr[0] = 1;
                        Session["OffMenuArrayList"] = arr;
                    }
                    else
                    {
                        CheckMenuImage(int.Parse(OfReId));
                        ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                        arr[0] = 1;
                        Session["OffMenuArrayList"] = arr;
                    }
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }
    protected void Edit(int OagId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(AgentManager);

        try
        {
            int OfId = Convert.ToInt32(Utility.DecryptQS(OfficeId.Value));

            AgentManager.FindByOfCode(OfId);
            for (int i = 0; i < AgentManager.Count; i++)
            {
                if (AgentManager[i]["OagName"].ToString() == txtOfAgName.Text && Convert.ToInt32(AgentManager[i]["OagId"]) != OagId && AgentManager[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }


            AgentManager.FindByCode(OagId);
            if (AgentManager.Count > 0)
            {
                AgentManager[0].BeginEdit();
                AgentManager[0]["OfId"] = Utility.DecryptQS(OfficeId.Value);
                AgentManager[0]["Address"] = txtOfAgAddress.Text;
                AgentManager[0]["Email"] = txtOfAgEmail1.Text;
                AgentManager[0]["OagName"] = txtOfAgName.Text;
                AgentManager[0]["Responsible"] = txtOfAgResponsible.Text;
                AgentManager[0]["WebSite"] = txtOfAgWebsite.Text;


                if (txtOfAgTel_pre.Text != "" && txtOfAgTel.Text != "")
                    AgentManager[0]["Tel"] = txtOfAgTel_pre.Text + "-" + txtOfAgTel.Text;
                else if (txtOfAgTel.Text != "")
                    AgentManager[0]["Tel"] = txtOfAgTel.Text;

                if (txtOfAgFax_pre.Text != "" && txtOfAgFax.Text != "")
                    AgentManager[0]["Fax"] = txtOfAgFax_pre.Text + "-" + txtOfAgFax.Text;
                else if (txtOfAgFax.Text != "")
                    AgentManager[0]["Fax"] = txtOfAgFax.Text;


                AgentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AgentManager[0]["ModifiedDate"] = DateTime.Now;
                AgentManager[0].EndEdit();
                trans.BeginSave();
                if (AgentManager.Save() == 1)
                {
                    string OfReId = Utility.DecryptQS(OfficeRequest.Value);


                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_OffAgent"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeAgent;
                        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                        int WfCode = -1;
                        if (Department == "Document")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                        else if (Department == "MemberShip")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                        if (WfCode == -1)
                        {
                            trans.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, int.Parse(OfReId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        trans.EndSave();
                        AgentId.Value = Utility.EncryptQS(AgentManager[0]["OagId"].ToString());
                        PgMode.Value = Utility.EncryptQS("Edit");
                        ASPxRoundPanel2.HeaderText = "ویرایش";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                        Session["IsEdited_OffAgent"] = true;
                    }

                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }
        }

        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }
    }
    //protected void Delete(int OagId)
    //{
    //    TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();


    //    AgentManager.FindByCode(OagId);
    //    if (AgentManager.Count == 1)
    //    {
    //        try
    //        {
    //                AgentManager[0].Delete();

    //                int cn = AgentManager.Save();
    //                if (cn == 1)
    //                {
    //                    AgentId.Value = Utility.EncryptQS("");
    //                    PgMode.Value = Utility.EncryptQS("New");
    //                    ASPxRoundPanel2.HeaderText = "جدید";
    //                    btnDelete.Enabled = false;
    //                    btnDelete2.Enabled = false;
    //                    this.ViewState["BtnDelete"] = btnDelete.Enabled;

    //                    ClearForm();
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "حذف انجام شد";

    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "حذف انجام نشد";
    //                }

    //        }
    //        catch (Exception err)
    //        {

    //            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //            {
    //                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //                if (se.Number == 547)
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
    //                }
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
    //            }
    //        }

    //    }
    //}
    protected void Enable()
    {
        txtOfAgAddress.Enabled = true;
        txtOfAgEmail1.Enabled = true;
        txtOfAgFax.Enabled = true;
        txtOfAgFax_pre.Enabled = true;
        txtOfAgName.Enabled = true;
        txtOfAgResponsible.Enabled = true;
        txtOfAgTel.Enabled = true;
        txtOfAgTel_pre.Enabled = true;
        txtOfAgWebsite.Enabled = true;


    }
    protected void Disable()
    {
        txtOfAgAddress.Enabled = false;
        txtOfAgEmail1.Enabled = false;
        txtOfAgFax.Enabled = false;
        txtOfAgFax_pre.Enabled = false;
        txtOfAgName.Enabled = false;
        txtOfAgResponsible.Enabled = false;
        txtOfAgTel.Enabled = false;
        txtOfAgTel_pre.Enabled = false;
        txtOfAgWebsite.Enabled = false;
    }
    protected void ClearForm()
    {
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {

            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }

        }
        txtOfAgAddress.Text = "";

    }
    private void CheckWorkFlowPermissionForDoc()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode != "New")
            CheckWorkFlowPermissionForEditForDoc(PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForDoc(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForDoc(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        int Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), TaskCode, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2 > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForOffice()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode != "New")
            CheckWorkFlowPermissionForEditForOffice(PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForOffice(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت ثبت اطلاعات شعبه هاي شركت را در جريان كار نداريد.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForOffice(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;        
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;        
        int Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId),  (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId),  (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2>0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();


        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office

        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    private TSP.DataManager.Permission FindPermissionClass()
    {
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {
            return (TSP.DataManager.OfficeAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        else if (Department == "Document")
        {
            return (TSP.DataManager.OfficeAgentManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        return (TSP.DataManager.OfficeAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
    }
    #endregion
}
