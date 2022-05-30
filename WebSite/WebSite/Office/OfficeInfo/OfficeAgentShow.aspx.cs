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

public partial class Office_OfficeInfo_OfficeAgentShow : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

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
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        btnEdit.Enabled = false;
        btnEdit1.Enabled = false;

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OagId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("OfficeAgent.aspx");

            }

            SetKeys();

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value
            + "&PageMode=" + Request.QueryString["PageMode"] + "&Dprt=" + Request.QueryString["Dprt"]);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OagId = Utility.DecryptQS(AgentId.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);

        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        OfManager.FindByCode(int.Parse(OfId));
        if ((bool)OfManager[0]["IsLock"] == true)
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string OfficeLockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 1, 1);

            string lockers = Utility.GetFormattedObject(OfficeLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;
        }

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
                string Department = Utility.DecryptQS(HDMode.Value);
                if (Utility.IsDBNullOrNullValue(OagId) || Utility.IsDBNullOrNullValue(OfReId) || Utility.IsDBNullOrNullValue(Department))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                    return;
                Edit(int.Parse(OagId));
                break;
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        btnEdit1.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        AgentId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string OagId = Utility.DecryptQS(AgentId.Value);
        int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
        string Department = Utility.DecryptQS(HDMode.Value);

        if (Utility.IsDBNullOrNullValue(OagId) || Utility.IsDBNullOrNullValue(OfReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
            return;

        Enable();
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        PgMode.Value = Utility.EncryptQS("Edit");
        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    void SetKeys()
    {
        try
        {
            OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
            AgentId.Value = Server.HtmlDecode(Request.QueryString["OagId"]).ToString();
            OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
            PgMode.Value = Server.HtmlDecode(Request.QueryString["APageMode"]).ToString();
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Dprt"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OagId = Utility.DecryptQS(AgentId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string Department = Utility.DecryptQS(HDMode.Value);

        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(OagId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        SetMode(PageMode, Department, int.Parse(OagId));

    }
    void SetMode(string PageMode, string Department, int OagId)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        switch (PageMode)
        {
            case "View":
                Disable();
                btnEdit.Enabled = true;
                btnEdit1.Enabled = true;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(OagId);
                ASPxRoundPanel2.HeaderText = "مشاهده";
                break;
            case "New":
                Enable();
                btnEdit1.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                ClearForm();
                break;
            case "Edit":
                Enable();
                btnEdit1.Enabled = false;
                btnEdit.Enabled = false;
                FillForm(OagId);
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                break;
        }

        switch (Department)
        {
            case "Home":
                SetEnabled(false);
                break;

            case "Document":
                if (string.IsNullOrEmpty(OfReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                if (!CheckPermitionForEditForDoc(int.Parse(OfReId)))
                    SetEnabled(false);
                else SetEnabled(true);

                TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                ReqManager.FindByCode(int.Parse(OfReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || (ReqManager[0]["IsConfirm"].ToString() != "0"))
                        SetEnabled(false);
                    else SetEnabled(true);
                }
                TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
                AgentManager.FindByCode(OagId);
                if (AgentManager.Count == 1)
                {
                    if (AgentManager[0]["OfReId"].ToString() != OfReId)
                        SetEnabled(false);
                    else SetEnabled(true);
                }
                break;
            case "MemberShip":
                if (string.IsNullOrEmpty(OfReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if (!CheckPermitionForEditForOffice(int.Parse(OfReId)))
                    SetEnabled(false);
                else SetEnabled(true);
                break;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    void SetEnabled(bool Enabled)
    {
        btnEdit.Enabled = Enabled;
        btnEdit1.Enabled = Enabled;
        btnSave.Enabled = Enabled;
        btnSave2.Enabled = Enabled;
        btnNew.Enabled = Enabled;
        btnNew1.Enabled = Enabled;
    }
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
    protected void FillForm(int OagId)
    {
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
                btnEdit1.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                AgentId.Value = Utility.EncryptQS(OffAgManager[0]["OagId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
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
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(AgentManager);

        try
        {
            string OfId = Utility.DecryptQS(OfficeId.Value);

            AgentManager.FindByOfCode(int.Parse(OfId));
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
                    trans.EndSave();
                    AgentId.Value = Utility.EncryptQS(AgentManager[0]["OagId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
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

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
}
