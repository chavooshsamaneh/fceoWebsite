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
using DevExpress.Web;

public partial class Employee_OfficeRegister_OfficeLetters : System.Web.UI.Page
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
        if (!IsPostBack)
        {
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
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            CustomAspxDevGridView1.Visible = per.CanView;

            LetterMode["Mode"] = "";
            LetterId["Id"] = "";

            Session["FillOfLetter"] = null;
            Session["IsEdited_OffLetter"] = false;

            ViewState["PMode"] = "";
            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }
            try
            {
                OfficeId.Value = Request.QueryString["OfId"].ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

                //HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();
            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            //string Mode = Utility.DecryptQS(HDMode.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);


            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            //OdbOfLetters.FilterParameters[0].DefaultValue = OfId;
            OdbOfLetters.SelectParameters[0].DefaultValue = OfId;
            OdbOfLetters.SelectParameters[1].DefaultValue = OfReId;

            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                Session["Header"] = "شرکت : " + OfManager[0]["OfName"].ToString();

            OfficeInfoUserControl.OfReId = int.Parse(OfReId);

            TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "Document")
                CheckWorkFlowPermissionForDoc();
            else if (Department == "MemberShip")
                CheckWorkFlowPermissionForOffice();

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if ( ReqManager[0]["IsConfirm"].ToString() != "0")
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }
                if (ReqManager[0]["IsConfirm"].ToString() == "0") //Not Answered
                {
                    OdbOfLetters.SelectParameters[3].DefaultValue = "2";

                }
            }            
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;

            SetMenuItem();
        }

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = OdbOfLetters;
        Session["Title"] = "روزنامه های رسمی";

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];


        this.DivReport.Visible = true;
        // Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        DivReport.Style["visibility"] = "hidden";

        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(ViewState["PMode"].ToString());

        int OlId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OlId = (int)row["OlId"];
        }

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

                if (string.IsNullOrEmpty(OlId.ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(OlId);
                }

            }

        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Page = "";
        switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
        {
            case "MemberShip":
                Page = "OfficeInsert.aspx";
                break;
            default:
                Page = "OfficeDocumentInsert.aspx";
                break;
        }
        Response.Redirect(Page + "?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
              + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        string PageName = "Office.aspx";
        switch (Dprt)
        {
            case "MemberShip":
                PageName = "Office.aspx";
                break;
            case "Document":
                PageName = "OfficeDocument.aspx";
                break;
        }
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?PostId=" + OfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {

            Response.Redirect(PageName);
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Office":
                string Page = "";
                switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
                {
                    case "MemberShip":
                        Page = "OfficeInsert.aspx";
                        break;
                    default:
                        Page = "OfficeDocumentInsert.aspx";
                        break;
                }
                Response.Redirect(Page + "?OfId=" + OfficeId.Value + "&" + "PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + OfficeId.Value + "&" + "PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Agent":
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&" + "PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        // string OlId = Utility.DecryptQS(LetterId.Value);

        if (string.IsNullOrEmpty(OfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        else
        {
            int OlId = -1;
            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                OlId = (int)row["OlId"];
            }
            if (OlId == -1)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Disable();
                TSP.DataManager.Permission per = FindPermissionClass();

                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;


                //  btnDelete.Enabled = false;
                //  btnDelete2.Enabled = false;
                btnSave.Visible = false;
                FillForm(OlId);
                ASPxPopupControl1.HeaderText = "مشاهده";

            }

        }

    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int OlId = -1;
        int OfReId = -1;
        string InActiveName = "";

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OlId = (int)row["OlId"];
            OfReId = (int)row["OfReId"];
            InActiveName = row["InActiveName"].ToString();

        }
        if (OlId == -1)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();

            LetterManager.FindByCode(OlId);
            if (LetterManager.Count == 1)
            {
                try
                {
                    int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                    if (OfReId == CurrentOfReId)
                    {
                        LetterManager[0].Delete();
                        LetterManager.Save();
                        CustomAspxDevGridView1.DataBind();

                        DivReport.Style["visibility"] = "block";
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                        {
                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            return;
                        }

                        InsertInActive(OlId, CurrentOfReId);
                        //if (Convert.ToBoolean(LetterManager[0]["InActive"]))
                        //{
                        //    DivReport.Style["visibility"] = "block";
                        //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                        //    return;
                        //}
                        //else
                        //{
                        //    LetterManager[0].BeginEdit();
                        //    LetterManager[0]["InActive"] = 1;
                        //    LetterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        //    LetterManager[0].EndEdit();

                        //}
                    }
                    CheckMenuImageCurrentPage(CurrentOfReId);

                }
                catch (Exception err)
                {

                    DivReport.Style["visibility"] = "block";
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

            }
        }
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (OfficeRequest.Value != null)
        {
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnOfReId = e.GetValue("OfReId").ToString();
            if (OfReId == CurretnOfReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }

    }

    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {


        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));
            int OlId = (int)row["OlId"];
            int OfReId = (int)row["OfReId"];


            int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
            if (OfReId == CurrentOfReId)
            {
                string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                {
                    //if (!CheckPermitionForEditForDoc(OfReId))
                    //{

                    e.Result = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    CustomAspxDevGridView1.JSProperties["cpShow"] = 0;

                }

            }
            else
            {
                btnSave.Visible = false;
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }


        }
    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;
        TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OffLeManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            OffLeManager.FindByOfCode(OfId);

            for (int i = 0; i < OffLeManager.Count; i++)
            {
                if (OffLeManager[i]["LetterNo"].ToString() == e.NewValues["LetterNo"].ToString() && OffLeManager[i]["PageNo"].ToString() == e.NewValues["PageNo"].ToString() && OffLeManager[i]["InActiveName"].ToString() == "فعال")
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }

            DataRow dr = OffLeManager.NewRow();

            dr["OlId"] = 0;
            dr["OfReId"] = OfReId;

            dr["OfId"] = OfId;
            // dr["OfId"] = int.Parse(Server.HtmlEncode(Request.QueryString["OfId"].ToString()));
            dr["LetterNo"] = e.NewValues["LetterNo"].ToString();
            dr["PageNo"] = int.Parse(e.NewValues["PageNo"].ToString());
            dr["Date"] = e.NewValues["Date"].ToString();



            dr["UserId"] = Utility.GetCurrentUser_UserId();

            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["ModifiedDate"] = DateTime.Now;

            OffLeManager.AddRow(dr);
            trans.BeginSave();
            int cnt = OffLeManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffLetter"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeLetter;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";

                    //this.DivReport.Visible = true;
                    //this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";

                    CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                    Session["IsEdited_OffLetter"] = true;
                    CustomAspxDevGridView1.DataBind();
                    //CustomAspxDevGridView1.CancelEdit();

                }

            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            CustomAspxDevGridView1.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;

        int OlId = int.Parse(e.Keys["OlId"].ToString());
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
        TSP.DataManager.OfficialLetterManager OffLeManager2 = new TSP.DataManager.OfficialLetterManager();

        OffLeManager2.FindByCode(OlId);
        int OfReId = int.Parse(OffLeManager2[0]["OfReId"].ToString());


        int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
        if (OfReId == CurrentOfReId)
        {
            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if ((Department == "Document" && CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && CheckPermitionForEditForOffice(OfReId)))
            {
                TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
                TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

                trans.Add(OffLeManager);
                trans.Add(WorkFlowStateManager);

                try
                {

                    OffLeManager.FindByCode(int.Parse(e.Keys["OlId"].ToString()));
                    if (OffLeManager.Count == 1)
                    {

                        OffLeManager[0].BeginEdit();
                        OffLeManager[0]["LetterNo"] = e.NewValues["LetterNo"].ToString();
                        OffLeManager[0]["PageNo"] = int.Parse(e.NewValues["PageNo"].ToString());
                        OffLeManager[0]["Date"] = e.NewValues["Date"].ToString();

                        OffLeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        if (e.NewValues["Description"] != null)
                            OffLeManager[0]["Description"] = e.NewValues["Description"].ToString();
                        OffLeManager[0]["ModifiedDate"] = DateTime.Now;
                        OffLeManager[0].EndEdit();
                        trans.BeginSave();
                        int cnt = OffLeManager.Save();
                        if (cnt > 0)
                        {
                            int UpdateState = -1;
                            if (!(Convert.ToBoolean(Session["IsEdited_OffLetter"].ToString())))
                            {
                                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                                int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeLetter;
                                int WfCode = -1;
                                if (Department == "Document")
                                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                                else if (Department == "MemberShip")
                                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                                if (WfCode == -1)
                                {
                                    trans.CancelSave();
                                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                                    return;
                                }
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                            }
                            if (UpdateState == -4)
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                            }
                            else
                            {
                                trans.EndSave();

                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                                Session["IsEdited_OffLetter"] = true;

                                CustomAspxDevGridView1.DataBind();
                                CustomAspxDevGridView1.CancelEdit();

                            }
                        }
                        else
                        {
                            trans.CancelSave();
                            CustomAspxDevGridView1.CancelEdit();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.CancelEdit();

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }

                }
                catch (Exception err)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.CancelEdit();

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 2601)
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                        }
                        else
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }
                }

            }
            else
            {
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
            }

        }
        else
        {
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        }

    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
            Session["OffMenuArrayList"] = arr;
        }
        else
            CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
        }
    }
    #endregion

    #region Methods
    protected void Insert()
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OffLeManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            DataRow dr = OffLeManager.NewRow();
            dr["OlId"] = 0;
            dr["OfReId"] = OfReId;

            dr["OfId"] = OfId;
            // dr["OfId"] = int.Parse(Server.HtmlEncode(Request.QueryString["OfId"].ToString()));
            dr["LetterNo"] = txtLeNo.Text;
            dr["PageNo"] = txtLePageNo.Text;
            dr["Date"] = txtLeDate.Text;
            dr["Description"] = txtLeDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            OffLeManager.AddRow(dr);
            trans.BeginSave();
            if (OffLeManager.Save() == 1)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffLetter"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeLetter;
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

                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    CustomAspxDevGridView1.DataBind();
                    Session["IsEdited_OffLetter"] = true;
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
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

    protected void Edit(int OlId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OffLeManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value)); ;

            OffLeManager.FindByCode(OlId);
            if (OffLeManager.Count == 1)
            {

                OffLeManager[0].BeginEdit();
                OffLeManager[0]["LetterNo"] = txtLeNo.Text;
                OffLeManager[0]["PageNo"] = txtLePageNo.Text;
                OffLeManager[0]["Date"] = txtLeDate.Text;
                OffLeManager[0]["Description"] = txtLeDesc.Text;
                OffLeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OffLeManager[0]["ModifiedDate"] = DateTime.Now;
                OffLeManager[0].EndEdit();
                trans.BeginSave();
                int cnt = OffLeManager.Save();
                OffLeManager.DataTable.AcceptChanges();
                if (cnt == 0)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_OffLetter"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeLetter;
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
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        CustomAspxDevGridView1.DataBind();
                        this.LabelWarning.Text = "ذخیره انجام شد";
                        trans.EndSave();
                        Session["IsEdited_OffLetter"] = true;
                    }
                }

            }
            else
            {
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

    protected void ClearForm()
    {
        txtLeDate.Text = "";
        txtLeDesc.Text = "";
        txtLeNo.Text = "";
        txtLePageNo.Text = "";
    }

    protected void Enable()
    {
        txtLeDate.Enabled = true;
        txtLeDesc.Enabled = true;
        txtLeNo.Enabled = true;
        txtLePageNo.Enabled = true;

    }

    protected void Disable()
    {
        txtLeDate.Enabled = false;
        txtLeDesc.Enabled = false;
        txtLeNo.Enabled = false;
        txtLePageNo.Enabled = false;
    }

    protected void FillForm(int OlId)
    {
        TSP.DataManager.OfficialLetterManager LeManager = new TSP.DataManager.OfficialLetterManager();
        try
        {
            LeManager.FindByCode(OlId);
            if (LeManager.Count > 0)
            {
                txtLeDate.Text = LeManager[0]["Date"].ToString();
                txtLeDesc.Text = LeManager[0]["Description"].ToString();
                txtLeNo.Text = LeManager[0]["LetterNo"].ToString();
                txtLePageNo.Text = LeManager[0]["PageNo"].ToString();
            }
            else
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
                return;

            }
        }
        catch (Exception err)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
            return;
        }
    }

    #region WF
    //******************************Offic Doc*****************************************************
    private void CheckWorkFlowPermissionForDoc()
    {
        CheckWorkFlowPermissionForSaveForDoc();
    }

    private void CheckWorkFlowPermissionForSaveForDoc()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        if (FirstNmcIdType == 0)
                        {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    //***********************************************************************************
    private void CheckWorkFlowPermissionForOffice()
    {
        CheckWorkFlowPermissionForSaveForOffice();
    }

    private void CheckWorkFlowPermissionForSaveForOffice()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        if (FirstNmcIdType == 0)
                        {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        }
                    }
                }
            }
        }
        return false;

    }
    #endregion

    protected void InsertInActive(int OlId, int OfReId)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = OlId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.OfficeLetter;
        dr["ReqId"] = OfReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        CustomAspxDevGridView1.DataBind();

        DivReport.Style["visibility"] = "block";
        this.LabelWarning.Text = "ذخیره انجام شد";
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
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int OfReId)
    {
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        OffLetterManager.FindForDelete(OfReId);

        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffLetterManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "";
                arr[2] = 0;

            }
            Session["OffMenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(OfReId);
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffLetterManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "";
                arr[2] = 0;

            }
            Session["OffMenuArrayList"] = arr;

        }

    }

    protected void SetMenuItem()
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[6] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));

        }
    }

    private TSP.DataManager.Permission FindPermissionClass()
    {
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {
            return (TSP.DataManager.OfficialLetterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        else if (Department == "Document")
        {
            return (TSP.DataManager.OfficeMemberManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        return (TSP.DataManager.OfficialLetterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
    }
    #endregion
}
