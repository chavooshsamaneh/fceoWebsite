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

public partial class Office_OfficeRequest : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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
            string OfId = Utility.GetCurrentUser_MeId().ToString();
            if (string.IsNullOrEmpty(OfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            OfficeId.Value = Utility.EncryptQS(OfId);

            ObjectDataSource1.SelectParameters[0].DefaultValue = OfId;

            Session["SendBackDataTable_OffRq"] = "";
            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.OfficeConfirming);
            if (WorkFlowManager.Count == 1)
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowManager[0]["WorkFlowId"].ToString();

        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            int OfId = Convert.ToInt32(Utility.DecryptQS(OfficeId.Value));
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

            ReqManager.FindByOfficeId(OfId, 0, (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل عدم پاسخ درخواست ثبت نام اولیه, امکان درخواست صدور پروانه وجود ندارد";
                return;
            }
            ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);//صدور
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ثبت مجدد درخواست صدور پروانه وجود ندارد ";
                return;
            }

            ReqManager.FindByOfficeId(OfId, 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده,امکان ثبت درخواست جدید وجود ندارد";
                return;
            }

            Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS("0") + "&PageMode=" + Utility.EncryptQS("NewReq")
                + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfReId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfReId = (int)row["OfReId"];
        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
            Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("View")
                + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();


        int OfReId = -1;
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfReId = (int)row["OfReId"];

        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            try
            {
                if (!CheckPermitionForEdit(OfReId))
                    return;

                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                        return;
                    }
                    if (ReqManager[0]["IsConfirm"].ToString() != "0")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش درخواست پاسخ داده شده وجود ندارد";
                    }

                    Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                        + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
                }

            }
            catch (Exception)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        int OfReId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfReId = (int)row["OfReId"];
        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count > 0)
            {
                if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان لغو درخواست برای شما وجود ندارد";
                        return;
                    }
                    if (ReqManager[0]["IsConfirm"].ToString() != "0")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "وضعیت درخواست مورد نظر مشخص شده است.امکان لغو درخواست وجود ندارد";

                        return;

                    }
                    if (!CheckPermitionForDelete(OfReId))
                        return;

                    Delete(OfId, OfReId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان لغو درخواست اولیه وجود ندارد";
                    return;

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeHome.aspx?MeId=" + OfficeId.Value);
    }
    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

            ReqManager.FindByOfficeId(OfId, 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }

            ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);
            if (ReqManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست تمدید وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است";
                return;
            }
            else if (ReqManager[0]["IsConfirm"].ToString() != "1")
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست تمدید وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                return;
            }

            ReqManager.FindByOfficeId(OfId, -1, -1);//return last OfReId
            if (ReqManager.Count > 0)
            {
                int OfReId = int.Parse(ReqManager[0]["OfReId"].ToString());
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, OfReId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfOfficeAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {
                        string CrtEndDate = ReqManager[0]["ExpireDate"].ToString();
                        Utility.Date objDate = new Utility.Date(CrtEndDate);
                        string LastMonth = objDate.AddMonths(-1);
                        string Today = Utility.GetDateOfToday();
                        int IsDocExp = string.Compare(Today, LastMonth);
                        if (IsDocExp > 0)
                        {
                            Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Revival")
                                + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست تمدید وجود ندارد.تاریخ اعتبار پروانه به پایان نرسیده است";
                            return;
                        }

                    }
                }
            }
        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
        }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            ReqManager.FindByOfficeId(OfId, 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);
            if (ReqManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است";
                return;
            }
            else if (ReqManager[0]["IsConfirm"].ToString() != "1")
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                return;
            }

            ReqManager.FindByOfficeId(OfId, -1, -1);//return last OfReId
            if (ReqManager.Count > 0)
            {
                if (ReqManager[0]["IsConfirm"].ToString() != "1")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                    return;
                }
                else
                {
                    int OfReId = int.Parse(ReqManager[0]["OfReId"].ToString());
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, OfReId);
                    if (dtWfState.Rows.Count > 0)
                    {
                        int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                        int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfOfficeAndEndProcess;
                        int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess;
                        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

                        int RejectTaskId = -1;
                        int ConfirmTaskId = -1;
                        int SaveInfoTaskId = -1;

                        WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                        if (WorkFlowTaskManager.Count > 0)
                        {
                            SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }


                        WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                        if (WorkFlowTaskManager.Count > 0)
                        {
                            RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }

                        WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                        if (WorkFlowTaskManager.Count > 0)
                        {
                            ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }

                        if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                        {
                            Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change")
                                + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
                        }
                    }
                }
            }
        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
        }
    }
    protected void btnReduplicate_Click(object sender, EventArgs e)
    {
        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            ReqManager.FindByOfficeId(OfId, 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);
            if (ReqManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست المثنی وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است";
                return;
            }
            else if (ReqManager[0]["IsConfirm"].ToString() != "1")
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست المثنی وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                return;
            }

            ReqManager.FindByOfficeId(OfId, -1, -1);//return last OfReId
            if (ReqManager.Count > 0)
            {
                if (ReqManager[0]["IsConfirm"].ToString() != "1")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                    return;
                }

                int OfReId = int.Parse(ReqManager[0]["OfReId"].ToString());
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, OfReId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfOfficeAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {
                        Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Reduplicate")
                            + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("Document"));
                    }
                }
            }
        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
        }
    }

    #region WorkFlow
    private void SendMemberFileDocToNextStep(int OfReId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int NextStepTaskId = -1;

        DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, SaveInfoTaskCode, WorkflowCode);
        if (dtNextTopTask.Rows.Count > 0)
        {
            int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
            WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            DataTable dtSendBack = (DataTable)Session["SendBackDataTable_OffRq"];
            cmbSendBackTask.DataSource = dtSendBack;
            cmbSendBackTask.ValueField = "TaskId";
            cmbSendBackTask.TextField = "TaskName";
            cmbSendBackTask.DataBind();

            int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
            if (SelectedTaskId == NextStepTaskId)
            {
                TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

                TransactionManager.Add(WorkFlowStateManager);

                int NmcId = Utility.GetCurrentUser_MeId();
                int NmcIdType = 2;
                if (NmcId > 0)
                {
                    TransactionManager.BeginSave();
                    string Url = "<a href='../../Employee/OfficeRegister/OfficeRegister1.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Utility.EncryptQS("View") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "' target=_blank>اینجا کلیک کنید</a>";

                    string MsgContent = "";
                    int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, OfReId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url);

                    switch (SendDoc)
                    {
                        case -6:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.Visible = true;
                            PanelMain.Visible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله جاری وجود ندارد.";
                            break;
                        case -4:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.Visible = true;
                            PanelMain.Visible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                            break;
                        case -5:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.Visible = true;
                            PanelMain.Visible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                            break;
                        case -8:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.Visible = true;
                            PanelMain.Visible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                            break;
                        default:
                            TransactionManager.EndSave();
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
                            lblInstitueWarning.Text = "ذخیره انجام شد.";
                            PanelMain.ClientVisible = false;
                            PanelSaveSuccessfully.ClientVisible = true;
                            CustomAspxDevGridView1.DataBind();
                            break;
                    }
                }
                else
                {
                    PanelSaveSuccessfully.Visible = true;
                    PanelMain.Visible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
                }

            }
            else
            {
                PanelSaveSuccessfully.Visible = true;
                PanelMain.Visible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";

            }
            CustomAspxDevGridView1.DataBind();
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.Visible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مرحله بعد جریان کار نا مشخص است.";
        }



    }
    private void SelectSendBackTask(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        WorkFlowStateManager.ClearBeforeFill = true;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskOrder = int.Parse(dtWorkFlowLastState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowLastState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

            int Permission = ReqManager.CheckPermissionOfficeDocConfirmingSendBackTask(TableId, CurrentTaskCode);
            if (Permission != 0)
            {
                string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(Permission);
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = ErrorMsg;
                return;
            }


            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());

                    if (FirstNmcIdType == 2)
                    {
                        if (FirstNmcId == Utility.GetCurrentUser_MeId())
                        {
                            ReqManager.FindByCode(TableId);
                            if (ReqManager.Count == 1)
                            {

                                DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, DocMeFileSaveInfoTaskCode, CurrentWorkFlowCode);
                                if (dtNextTopTask.Rows.Count > 0)
                                {
                                    Session["SendBackDataTable_OffRq"] = dtNextTopTask;
                                    cmbSendBackTask.DataSource = dtNextTopTask;
                                    cmbSendBackTask.ValueField = "TaskId";
                                    cmbSendBackTask.TextField = "TaskName";
                                    cmbSendBackTask.DataBind();
                                    cmbSendBackTask.SelectedIndex = 0;
                                    PanelSaveSuccessfully.Visible = false;
                                    PanelMain.Visible = true;
                                }
                                else
                                {
                                    PanelSaveSuccessfully.ClientVisible = true;
                                    PanelMain.ClientVisible = false;
                                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                    lblInstitueWarning.Text = "عملیات بعد در جریان کار نامشخص است.";
                                }

                            }
                            else
                            {
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                lblInstitueWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                            }
                        }
                        else
                        {
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                        }
                    }
                    else
                    {
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                        lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                    }
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
                }
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            }
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
        }
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            int OfReId = int.Parse(MeFileRow["OfReId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
            if (e.Parameter == "Send")
            {
                SendMemberFileDocToNextStep(OfReId);
                CustomAspxDevGridView1.DataBind();
            }
            else
            {
                SelectSendBackTask(TableType, OfReId);
            }
        }
        else
        {
            PanelMain.ClientVisible = false;
            PanelSaveSuccessfully.ClientVisible = true;
            lblInstitueWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            return;
        }
    }
    protected void btnTracing_Click(object sender, EventArgs e)
    {
        //if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        //{
        //    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        //    DataRow Row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
        //    int TableId = int.Parse(Row["OfReId"].ToString());

        //    Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید.";
        //} 
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
            DataRow DocMeFileRow = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["OfReId"].ToString());
            int WorkFlowCode = int.Parse(DocMeFileRow["WorkFlowCode"].ToString());
            Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }
    #endregion

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
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
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
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
        if (string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
    private Boolean CheckPermitionForDelete(int TableId)
    {
        string Message = "";
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        dtState.DefaultView.RowFilter = "StateType=" + ((int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep).ToString();
        if (dtState.DefaultView.Count == 1)
        {
            int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
            if (CurrentNmcId == Utility.GetCurrentUser_MeId() && CurrentNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId)
            {
                if (CurrentTaskCode == TaskCode)
                    return true;
            }
            else
                Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان لغو درخواست برای شما وجود ندارد";
        }
        if (string.IsNullOrEmpty(Message))
            Message = "امکان لغو درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }

    protected void Delete(int OfId, int OfReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            trans.BeginSave();
            ReqManager.DeleteRequest(OfReId, OfId);

            int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, OfReId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }

            trans.EndSave();
            CustomAspxDevGridView1.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "MFNo" || e.DataColumn.FieldName == "AnswerDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)CustomAspxDevGridView1.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "MFNo" || e.Column.FieldName == "AnswerDate")
            e.Editor.Style["direction"] = "ltr";
    }

}

