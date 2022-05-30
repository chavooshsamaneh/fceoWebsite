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

public partial class Employee_Amoozesh_InActivePeriod : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
        {
            Response.Redirect("PeriodsView.aspx");
            return;
        }
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TrainingStatusChangeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            
            SetKeys();
            
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldPeriod["PageMode"].ToString());

        string PPId = Utility.DecryptQS(HiddenFieldPeriod["PPId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTrainingStatusChange();
            }
            else if (PageMode == "Edit")
            {

                //if (string.IsNullOrEmpty(PPId))
                //{
                //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                //    return;
                //}
                //else
                //{
                //    Edit(int.Parse(PPId));
                //}

            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodsView.aspx?PPId=" + HiddenFieldPeriod["PPId"] + "&PageMode=" + Request.QueryString["PageMode"]);
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldPeriod["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
        HiddenFieldPeriod["PPId"] = Server.HtmlDecode(Request.QueryString["PPId"].ToString());
        HiddenFieldPeriod["PSCId"] = Server.HtmlDecode(Request.QueryString["PSCId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldPeriod["PageMode"].ToString());
        string PPId = Utility.DecryptQS(HiddenFieldPeriod["PPId"].ToString());
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(int.Parse(PPId));
        if (PeriodPresentManager.Count == 1)
        {
            RoundPanelInActivePeriod.HeaderText = "لغو دوره آموزشی : " + PeriodPresentManager[0]["CrsName"].ToString();
        }
        else
        {
            Response.Redirect("PeriodsView.aspx");
            return;
        }       

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }      
        SetMode(PageMode);
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;           
        }
    }

    private void SetViewModeKeys()
    {
        string PSCId = Utility.DecryptQS(HiddenFieldPeriod["PSCId"].ToString());
        if (string.IsNullOrEmpty(PSCId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        FillForm(int.Parse(PSCId));
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        cmbInActiveReason.Enabled = false;
        txtDescription.Enabled = false;
        txtMailDate.Enabled = false;
        txtMailNo.Enabled = false;

        //RoundPanelInActivePeriod.HeaderText = "مشاهده";

        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        cmbInActiveReason.SelectedIndex = 0;

        this.ViewState["BtnSave"] = btnSave.Enabled;

        RoundPanelInActivePeriod.HeaderText = "جدید";
    }

    private void FillForm(int PSCId)
    {
        TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();
        TrainingStatusChangeManager.FindByCode(PSCId);
        if (TrainingStatusChangeManager.Count == 1)
        {
            txtMailNo.Text = TrainingStatusChangeManager[0]["MailNo"].ToString();
            txtMailDate.Text = TrainingStatusChangeManager[0]["MailDate"].ToString();
            txtDescription.Text = TrainingStatusChangeManager[0]["Description"].ToString();            
            cmbInActiveReason.SelectedIndex =int.Parse(TrainingStatusChangeManager[0]["Reasone"].ToString());
        }
    }
    private void InsertTrainingStatusChange()
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.MessageManager MessageManager = new TSP.DataManager.MessageManager();
        TSP.DataManager.MessageReceiverManager MessageReceiverManager = new TSP.DataManager.MessageReceiverManager();
        TransactionManager.Add(PeriodPresentManager);
        TransactionManager.Add(TrainingStatusChangeManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(MessageManager);
        TransactionManager.Add(MessageReceiverManager);

        try
        {
            TransactionManager.BeginSave();

            DataRow TrainingStsChngRow = TrainingStatusChangeManager.NewRow();
            int TableType = (int)TSP.DataManager.TableCodes.PeriodPresent;
            int PPId = int.Parse(Utility.DecryptQS(HiddenFieldPeriod["PPId"].ToString()));        
            TrainingStsChngRow["TableId"] = PPId;
            TrainingStsChngRow["TableType"] = TableType;
            TrainingStsChngRow["RequestType"] = 0;
            TrainingStsChngRow["Reasone"] = cmbInActiveReason.SelectedIndex;
            TrainingStsChngRow["MailNo"] = txtMailNo.Text;
            TrainingStsChngRow["MailDate"] = txtMailDate.Text;
            TrainingStsChngRow["Date"] = Utility.GetDateOfToday();
            TrainingStsChngRow["Description"] = txtDescription.Text;
            TrainingStsChngRow["ModifiedDate"] = DateTime.Now;
            TrainingStsChngRow["UserId"] = Utility.GetCurrentUser_UserId();

            TrainingStatusChangeManager.AddRow(TrainingStsChngRow);

            int cn = TrainingStatusChangeManager.Save();
            if (cn > 0)
            {
                PeriodPresentManager.FindByCode(PPId);
                if (PeriodPresentManager.Count == 1)
                {
                    PeriodPresentManager[0]["Status"] = 3;
                    int cnt = PeriodPresentManager.Save();
                    if (cnt > 0)
                    {
                        int CurrentUserId = Utility.GetCurrentUser_UserId();
                        //*****Check If User In TaskDoer*****
                        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
                        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                        int TaskCode = (int)TSP.DataManager.WorkFlowTask.PeriodInActivateRequest;
                        WorkFlowTaskManager.FindByTaskCode(TaskCode);
                        if (WorkFlowTaskManager.Count > 0)
                        {
                            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                            TaskDoerManager.FindByTaskId(TaskId);
                            if (TaskDoerManager.Count > 0)
                            {
                                TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                                int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                                if (NmcId > -1)
                                {
                                    int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(PPId, TaskCode, NmcId, Utility.GetCurrentUser_UserId());
                                    if (StartWorkFlow < 0)
                                    {
                                        TransactionManager.CancelSave();
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                                    }
                                    else
                                    {
                                        PeriodPresentManager.FindByCode(PPId);
                                        if (PeriodPresentManager.Count == 1)
                                        {
                                            // RoundPanelInActivePeriod.HeaderText = "لغو دوره آموزشی : " + PeriodPresentManager[0]["CrsName"].ToString();
                                            string PeriodName = PeriodPresentManager[0]["CrsName"].ToString();
                                            DataTable dtPeridRegister = PeriodRegisterManager.SelectPeriodRegister(PPId,0);
                                            if (dtPeridRegister.Rows.Count > 0)
                                            {
                                                int MessageSended = SendMessageForRegistrarInfo(dtPeridRegister, PeriodName, TransactionManager, MessageManager, MessageReceiverManager);
                                                if (MessageSended > 0)
                                                {
                                                    TransactionManager.EndSave();
                                                    this.DivReport.Visible = true;
                                                    this.LabelWarning.Text = "ذخیره انجام شد.";
                                                }
                                                else
                                                {
                                                    TransactionManager.CancelSave();
                                                    this.DivReport.Visible = true;
                                                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                                                }
                                            }
                                            else
                                            {
                                                TransactionManager.EndSave();
                                                this.DivReport.Visible = true;
                                                this.LabelWarning.Text = "ذخیره انجام شد.";
                                            }
                                        }
                                        else
                                        {
                                            TransactionManager.CancelSave();
                                            this.DivReport.Visible = true;
                                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                                        }
                                    }
                                }
                            }
                        }                      
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد تکراری می باشد";
                }
                else if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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

    private int SendMessageForRegistrarInfo(DataTable dtPeriodRegister, string PeriodName, TSP.DataManager.TransactionManager TransactionManager, TSP.DataManager.MessageManager MessageManager, TSP.DataManager.MessageReceiverManager MessageReceiverManager)
    {
        DataRow MsgRow = MessageManager.NewRow();

        MsgRow["SenderId"] = 0;
        MsgRow["SenderType"] = 0;
        MsgRow["IsSenderPart"] = 0;
        MsgRow["MsgTypeId"] = 1;
        MsgRow["NeedConfirm"] = 0;
        MsgRow["MsgSubject"] = "لغو دوره آموزشی";
        string RejectReason = cmbInActiveReason.SelectedItem.Text;
        string Body = "<p style=\"margin: 0px; text-align: right\">با سلام </p><p style=\"margin: 0px; text-align: right\">";
                    Body += "  بدین وسیله به اطلاع می رسانیم دوره اموزشی" + PeriodName;
                    Body += "</p><p style=\"margin: 0px; text-align: right\">به دلیل   </p><p style=\"margin: 0px; text-align: right\"><br/></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">";
                    Body += RejectReason;
                    Body += " لغو گردیده است.";
        MsgRow["MsgBody"] = Body;
        MsgRow["Date"] = Utility.GetDateOfToday();
        MsgRow["Priority"] = 0;              
        MsgRow["FutureStatus"] = 2;
        MsgRow["InActive"] = 0;
        MsgRow["UserId"] = Utility.GetCurrentUser_UserId();
        MsgRow["ModifiedDate"] = DateTime.Now;

        MessageManager.AddRow(MsgRow);
        int cn = MessageManager.Save();
        if (cn < 0)
        {
            TransactionManager.CancelSave();
            return cn;
        }
        for (int i = 0; i < dtPeriodRegister.Rows.Count; i++)
        {
            int ReceiverId = int.Parse(dtPeriodRegister.Rows[i]["MeId"].ToString());
            DataRow MsgrRow = MessageReceiverManager.NewRow();
            MsgrRow["MsgId"] = (int)(MessageManager[0]["MsgId"]);
            MsgrRow["IsRead"] = 0;
            MsgrRow["ReceiverId"] = ReceiverId;
            MsgrRow["ReceiverType"] = 4;
            MsgrRow["IsReceiverPart"] = 0;            
            MsgrRow["IsResignation"] = 0;                
            MsgrRow["InActive"] = 0;
            MsgrRow["UserId"] = Utility.GetCurrentUser_UserId();
            MsgrRow["ModifiedDate"] = DateTime.Now;
            MsgrRow["Answer"] = 0;
            MessageReceiverManager.AddRow(MsgrRow);           
        }
        int cnt = MessageReceiverManager.Save();
        if (cnt> 0)
        {
            return cnt;
        }
        else
        {
            TransactionManager.CancelSave();
            return cnt;
        }
    }  
    #endregion
}
