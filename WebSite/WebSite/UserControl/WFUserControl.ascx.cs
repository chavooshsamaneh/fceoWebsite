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
using System.ComponentModel;
using System.IO;

[ToolboxData("Tsp")]
[DefaultEvent("Callback")]
public partial class WFUserControl : System.Web.UI.UserControl
{
    [Browsable(true), Category("Events"), Description("when callback is called")]
    public event DevExpress.Web.CallbackEventHandlerBase Callback;

    [Browsable(true), Category("Events"), Description("when end of next step is called")]
    public event EventHandler EndNextStep;

    // [Browsable(true), Category("Events"), Description("when PerformCallback is called")]
    // public event EventHandler PerformCallback;

    Boolean gridHasCallback = false;
    [Browsable(true), Category("Behavior"), Description("Determine that gridcallback has parameters or no")]
    public Boolean GridHasCallback
    {
        get
        {
            return this.gridHasCallback;
        }
        set
        {
            this.gridHasCallback = value;
            string Parameters = "";
            if (GridHasCallback)
            {
                Parameters = "WFUserControl";
            }
            btnSendNextWorkStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('Send');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnConfirmAndEnd.ClientSideEvents.Click = "function(s, e) {CallbackPanelWorkFlow.PerformCallback('EndConfirm');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnRejectAndEnd.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('EndReject');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnSentBackStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('SendPreStep');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnSentNextStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('SendNextStep');" + GridName + ".PerformCallback('" + Parameters + "');}";
        }
    }

    string gridName = "";
    [Browsable(true), Category("Behavior"), Description("Name of grid that's supposed to callback")]
    public string GridName
    {
        get
        {
            return this.gridName;
        }
        set
        {
            this.gridName = value;
            string Parameters = "";
            if (GridHasCallback)
            {
                Parameters = TSP.DataManager.WorkFlowPermission.WFUserControlGridsCallbackName;
            }

            btnSendNextWorkStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('Send');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnConfirmAndEnd.ClientSideEvents.Click = "function(s, e) {CallbackPanelWorkFlow.PerformCallback('EndConfirm');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnRejectAndEnd.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('EndReject');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnSentBackStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('SendPreStep');" + GridName + ".PerformCallback('" + Parameters + "');}";
            btnSentNextStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('SendNextStep');" + GridName + ".PerformCallback('" + Parameters + "');}";
        }
    }

    string sessionName = "";
    [Browsable(true), Category("Behavior"), Description("Session name that's supposed to hold combobox datasource")]
    public string SessionName
    {
        get
        {
            return this.sessionName;
        }
        set
        {
            this.sessionName = value;
        }
    }

    private string _QueryStringForRedirect = "";
    [Browsable(true), Category("Behavior"), Description("QueryString For Redirecting after close PopUp")]
    public string QueryStringForRedirect
    {
        set
        {
            _QueryStringForRedirect = value;
        }
        get
        {
            return _QueryStringForRedirect;
        }
    }

    private Boolean _HasError;
    public Boolean HasError
    {
        set
        {
            _HasError = value;
        }
        get
        {
            return _HasError;
        }
    }

    // private static DataTable _dtSendBack;
    private DataTable dtSendBack
    {
        set
        {
            //DataTable dt = ((DataView)ObjdsWFTask.Select()).Table.Copy();
            //_dtSendBack = value;
            Session[sessionName] = value;
        }
        get
        {
            //  DataTable dt = ((DataView)ObjdsWFTask.Select()).Table.Copy();
            // return dt;
            //return (DataTable)_dtSendBack;
            DataTable dt = new DataTable();
            if (Session[sessionName] != null)
                return (DataTable)Session[sessionName];
            else
                return dt;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsCallback)
        {
            Utility.Date d = new Utility.Date(Utility.GetDateOfToday());
            string DefultExpireDate = d.AddDays(Utility.GetWFExpireDateDuration());
            txtExpireDate.Text = DefultExpireDate;
            // cmbPriority.SelectedIndex = -1;
            btnSendNextWorkStep.ClientSideEvents.Click = "function(s, e) {	CallbackPanelWorkFlow.PerformCallback('Send');" + GridName + ".PerformCallback('');}";
            lblWFWarning.Text = "";
            CheckBoxDoNotSendSMS.Checked = false;
            if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Employee)
            {
                CheckBoxDoNotSendSMS.Visible = false;
            }
        }
    }

    protected void CallbackPanelWorkFlow_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (this.Callback != null)
            this.Callback(sender, e);
    }

    //*************************************************************************************************************************   
    #region WF Methods
    /// <summary>
    /// در صفحات مدیریت اصلی (جایی که دکمه گردش کار کلیک می شود)این تابع فراخوانی می شود
    /// </summary>
    /// <param name="TableId"></param>
    /// <param name="TableType"></param>
    /// <param name="WFCode"></param>
    /// <param name="e"></param>
    public void PerformCallback(int TableId, int TableType, int WFCode, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "DoNextTaskOfClosePopUP") CallbackPanelWorkFlow.JSProperties["cpIsPopUpClose"] = 0;
        // else CallbackPanelWorkFlow.JSProperties["cpIsPopUpClose"] = 1;
        if (TableId == -2 && TableType == -2 && WFCode == -2 && e.Parameter != "DoNextTaskOfClosePopUP")
            return;
        switch (e.Parameter)
        {
            case "FillcmbTask":
                this.SelectSendBackTask(TableType, WFCode, TableId);
                break;
            #region SendDocToNextStep
            case "Send":
                this.SendDocToNextStep(TableId, WFCode, TableType, e.Parameter);
                break;

            case "SendNextStep":
                this.SendDocToNextStep(TableId, WFCode, TableType, e.Parameter);
                break;

            case "SendPreStep":
                this.SendDocToNextStep(TableId, WFCode, TableType, e.Parameter);
                break;

            case "EndConfirm":
                this.SendDocToNextStep(TableId, WFCode, TableType, e.Parameter);
                break;

            case "EndReject":
                this.SendDocToNextStep(TableId, WFCode, TableType, e.Parameter);
                break;
            #endregion
            case "DoNextTaskOfClosePopUP":
                //  CallbackPanelWorkFlow.JSProperties["cpIsPopUpClose"] = 1;
                DoNextTaskOfPopUpClose(WFCode);
                break;
            default:
                SetMsgText(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                break;
        }
    }

    /// <summary>
    /// هنگام باز شدن پنجره گردش کار صدا زده می شود.در این تابع شرایط پرونده و سطح دسترسی کاربر بررسی و بر اساس آن لیست عملیات ها به کاربر نمایش داده می شود  
    /// </summary>
    /// <param name="TableType"></param>
    /// <param name="WfCode"></param>
    /// <param name="TableId"></param>
    public void SelectSendBackTask(int TableType, int WfCode, int TableId)
    {
        try
        {

            TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
            DataTable dtExtraInfo = WorkFlowPermission.GetMemberInfoForSMSByWFCode(WfCode, TableId, -2, Utility.GetCurrentUser_LoginType());
            string ExtraInfo = "";
            if (dtExtraInfo.Rows.Count > 0)
                ExtraInfo = dtExtraInfo.Rows[0]["ExtraInfo"].ToString();

            CallbackPanelWorkFlow.JSProperties["cpWfName"] = "";
            CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "";
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count <= 0)
            {
                SetMsgText("عملیاتی برای پرونده انتخاب شده انجام نشده است." + ExtraInfo);
                return;
            }
            int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            string CurrentTaskName = dtWorkFlowState.Rows[0]["TaskName"].ToString();
            string WorkFlowName = dtWorkFlowState.Rows[0]["WorkFlowName"].ToString();
            int WorkFlowId = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowId"].ToString());
            int Permission = 0;

            if (WorkFlowPermission.SetSMSControlsVisible(CurrentTaskCode))
            {
                chbIsSendSMS.ClientVisible = true;
            }

            CallbackPanelWorkFlow.JSProperties["cpWfName"] = "گردش کار:" + WorkFlowName;
            CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "نام مرحله:" + CurrentTaskName;

            Permission = WorkFlowPermission.CheckSelectSendBackTaskPermissionForSpecificWF(WfCode, TableId, CurrentTaskCode, Utility.GetCurrentUser_UserId());

            switch (Permission)
            {
                case 0:
                    int SendBackTask = -1;
                    int SpecifiedSendBackTask = WorkFlowPermission.GetSpecifiedSendBackTask(WfCode, CurrentTaskCode, Utility.GetCurrentUser_LoginType(), TableId, Utility.GetCurrentUser_UserId());
                    if (SpecifiedSendBackTask == 0)
                        SendBackTask = WorkFlowStateManager.CalculateSendBackTaskByWfCode(WfCode, TableId, Utility.GetCurrentUser_UserId());
                    else
                        SendBackTask = SpecifiedSendBackTask;
                    switch (SendBackTask)
                    {
                        case (int)TSP.DataManager.WorkFlowStateManager.Errors.YouCanNotSend:
                            SetMsgText("امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد." + ExtraInfo);
                            break;
                        case (int)TSP.DataManager.WorkFlowStateManager.Errors.ProcessEnd:
                            SetMsgText("گردش کار پرونده انتخاب شده به اتمام رسیده است." + ExtraInfo);
                            break;
                        case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoTaskFind:
                            SetMsgText("عملیاتی برای پرونده انتخاب شده انجام نشده است." + ExtraInfo);
                            break;
                        case (int)TSP.DataManager.WorkFlowStateManager.Errors.UnKnownSendBackTask:
                            SetMsgText("مراحل ارسال پرونده برای شما مشخص نشده است." + ExtraInfo);
                            break;
                        default:
                            int OppositTaskId = WorkFlowPermission.FindTaskIdForFiltterSendBackDataTable(WfCode, TableId);
                            dtSendBack = WorkFlowStateManager.SelectSendBackTask(SendBackTask, WorkFlowId, OppositTaskId);
                            if (dtSendBack.Rows.Count > 0)
                            {
                                cmbSendBackTask.DataSource = dtSendBack;
                                cmbSendBackTask.ValueField = "TaskId";
                                cmbSendBackTask.TextField = "TaskName";
                                cmbSendBackTask.DataBind();
                                cmbSendBackTask.SelectedIndex = -1;
                                PanelSaveSuccessfully.ClientVisible = false;
                                PanelMain.ClientVisible = true;
                            }
                            break;
                    }
                    break;

                default:

                    string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(Permission);
                    if (!string.IsNullOrEmpty(ErrorMsg))
                    {
                        SetMsgText(ErrorMsg);
                    }
                    else
                    {
                        SetMsgText("خطایی در بازیابی اطلاعات ایجاد شده است.");
                    }
                    break;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMsgText("خطایی در بازیابی اطلاعات ایجاد شده است.");
        }
    }

    /// <summary>
    /// این تابع پس از انتخاب یک مرحله و کلیک دکمه ذخیره / تایید کلی/رد کلی/تایید و پاس/مرجوع و رد یک پرونده در پنجره گردش کار فراخوانی می شود
    /// </summary>
    /// <param name="TableId"></param>
    /// <param name="WfCode"></param>
    /// <param name="TableType"></param>
    /// <param name="RequestName"></param>
    public void SendDocToNextStep(int TableId, int WfCode, int TableType, string RequestName)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        string Url = "";
        string MsgContent = "";
        int SelectedTaskCode = -2;
        int PriorityId = -1;
        string ExpireDate = "";
        int CurrentTaskOrder = -1;
        if (cmbPriority.SelectedItem != null && cmbPriority.SelectedItem.Value != null)
        {
            PriorityId = Convert.ToInt32(cmbPriority.SelectedItem.Value);
        }
        ExpireDate = txtExpireDate.Text;
        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateObserverWorkManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission(WfCode, Utility.GetCurrentUser_AgentId(), TransactionManager);
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        #endregion

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        int CurrentTaskId = -1;
        int CurrentTaskCode = -1;
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }

        #region بر اساس دکمه کلیک شده عملیات انتخاب شده را بدست می آورد
        int SelectedTaskId = FindSelectedTaskId(RequestName, CurrentTaskCode, CurrentTaskOrder);
        if (SelectedTaskId < 0)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            if (SelectedTaskId == -2)
                SetMsgText("مرحله گردش کار مورد نظر را از لیست کشویی انتخاب نمایید.");
            else
                SetMsgText("در بازیابی اطلاعات مشکل ایجاد شده است.لطفا از پنجره گردش کار خارج شده و مجدد ردیف مورد نظر را انتخاب و بر روی دکمه مربوطه کلیک نمایید.");
            return;
        }
        WorkFlowTaskManager.FindByCode(SelectedTaskId);
        if (WorkFlowTaskManager.Count == 1)
        {
            SelectedTaskCode = int.Parse(WorkFlowTaskManager[0]["TaskCode"].ToString());
        }
        #endregion

        #region Transactions
        string Msg = "";
        switch (WfCode)
        {
            case (int)TSP.DataManager.WorkFlows.MemberConfirming:
                TransactionManager.Add(MemberRequestManager);
                break;
            case (int)TSP.DataManager.WorkFlows.SMSConfirming:
                TransactionManager.Add(SmsManager);
                TransactionManager.Add(SmsRecieverManager);
                break;
        }

        switch (WfCode)
        {
            case (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming:
                TransactionManager.Add(WorkFlowStateObserverWorkManager);
                break;
            default:
                TransactionManager.Add(WorkFlowStateManager);
                break;
        }

        #endregion
        int NmcId = FindNmcId(CurrentTaskId);
        int NmcIdType = Utility.GetCurrentUser_NmcIdType();
        if (NmcId < 0)
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member || Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Teacher)
                NmcId = Utility.GetCurrentUser_MeId();
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                SetMsgText("به علت نامشخص بودن سمت شما در چارت سازمانی و یا عدم تنظیم سطح دسترسی شما در گردش کار، قادر به ارسال پرونده به مرحله انتخاب شده نمی باشید.");
                return;
            }
        }

        try
        {
            DataTable dtExtraInfo = WorkFlowPermission.GetMemberInfoForSMSByWFCode(WfCode, TableId, SelectedTaskCode, Utility.GetCurrentUser_LoginType());
            string ExtraInfo = "";
            if (dtExtraInfo.Rows.Count > 0)
                ExtraInfo = dtExtraInfo.Rows[0]["ExtraInfo"].ToString();
            TransactionManager.BeginSave();
            Int64 SendDocStateId = -4;

            //ObserverWorkREquest
            switch (WfCode)
            {
                case (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming:
                    SendDocStateId = WorkFlowStateObserverWorkManager.SendDocToNextStep(TableId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), PriorityId, ExpireDate);
                    break;
                default:
                    SendDocStateId = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), "", "", PriorityId, ExpireDate);
                    break;
            }

            switch (SendDocStateId)
            {
                case (int)TSP.DataManager.WorkFlowStateManager.Errors.CannotSendToCurrentState:
                    TransactionManager.CancelSave();
                    SetMsgText("امکان ارسال پرونده به مرحله جاری وجود ندارد." + ExtraInfo);
                    break;
                case (int)TSP.DataManager.WorkFlowStateManager.Errors.Erorr:
                    TransactionManager.CancelSave();
                    SetMsgText("خطایی در ذخیره انجام شد." + ExtraInfo);
                    break;
                case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoStateFound:
                    TransactionManager.CancelSave();
                    SetMsgText("برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است." + ExtraInfo);
                    break;
                case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoTaskDoerFind:
                    TransactionManager.CancelSave();
                    SetMsgText("انجام دهنده عملیات بعد نامشخص می باشد." + ExtraInfo);
                    break;
                default:
                    #region Send Doc
                    int DoNextTask = 0;
                    ArrayList ArrayReturnValue = new ArrayList();
                    DoNextTask = WorkFlowPermission.CheckAndDoSendDocToNextStepConditions(WfCode, TableId, SelectedTaskId, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(), Utility.GetCurrentUser_AgentId(), Utility.GetCurrentCounId(), ref ArrayReturnValue, CurrentTaskId, SendDocStateId);
                    string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(DoNextTask);
                    if (DoNextTask != 0)
                    {
                        if (DoNextTask == (int)TSP.DataManager.ErrorRequest.OneOfMemberIsInAnotherOffice)
                        {
                            TransactionManager.CancelSave();
                            SetMsgText(ErrorMsg + ArrayReturnValue[0]);
                            return;
                        }
                        if (!string.IsNullOrEmpty(ErrorMsg))
                        {
                            TransactionManager.CancelSave();
                            SetMsgText(ErrorMsg + ExtraInfo);
                            return;
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            SetMsgText("خطایی در ذخیره انجام گرفته است." + ExtraInfo);
                            return;
                        }
                    }
                    Boolean IsEmailSent = true;
                    switch (WfCode)
                    {
                        case (int)TSP.DataManager.WorkFlows.MemberConfirming:
                            #region MeConf
                            int TaskIdMemberLicenceInquiryAndConfirming = FindSelectedTaskId((int)TSP.DataManager.WorkFlowTask.MemberLicenceInquiryAndConfirming, WorkFlowTaskManager);
                            if (SelectedTaskId == TaskIdMemberLicenceInquiryAndConfirming)
                            {
                                IsEmailSent = SendEmail(TableId, MemberRequestManager);
                            }
                            #endregion
                            break;
                    }

                    TransactionManager.EndSave();
                    if (!IsEmailSent)
                        SetMsgText("ذخیره انجام شد. ارسال ایمیل اطلاعات کاربری به عضو انجام نشد" + ExtraInfo, System.Drawing.Color.Green);
                    else
                        SetMsgText("ذخیره انجام شد." + ExtraInfo, System.Drawing.Color.Green);

                    #region Send Notifications
                    try
                    {
                        #region Session WF
                        if (WfCode == (int)TSP.DataManager.WorkFlows.Session)
                        {
                            TSP.DataManager.Session.SessionRequestsManager SessionRequestsManager = new TSP.DataManager.Session.SessionRequestsManager();
                            SessionRequestsManager.FindById(TableId);
                            ArrayList NotificationTypes = GetSessionNotificationSendType(Convert.ToInt32(SessionRequestsManager[0]["SessionDeclare"]));
                            if (NotificationTypes.Count > 0)
                            {
                                if (Convert.ToInt32(SessionRequestsManager[0]["RequestType"]) == (int)TSP.DataManager.Session.RequestTypesManager.Types.Save ||
                                    Convert.ToInt32(SessionRequestsManager[0]["RequestType"]) == (int)TSP.DataManager.Session.RequestTypesManager.Types.Edit)
                                {
                                    TSP.DataManager.Session.SpecificMembersManager SpecificMembersManager = new TSP.DataManager.Session.SpecificMembersManager();
                                    SendSessionNotification(SpecificMembersManager.SelectSpecificMembersDetail(TableId.ToString()), NotificationTypes, Utility.Notifications.NotificationTypes.SessionHolding, SessionRequestsManager[0]["SessionTitle"].ToString(), SessionRequestsManager[0]["StartDate"].ToString(), SessionRequestsManager[0]["StartTime"].ToString(), SessionRequestsManager[0]["LocationName"].ToString());
                                }
                                else if (Convert.ToInt32(SessionRequestsManager[0]["RequestType"]) == (int)TSP.DataManager.Session.RequestTypesManager.Types.ChangeDateTime)
                                {
                                    TSP.DataManager.Session.SpecificMembersManager SpecificMembersManager = new TSP.DataManager.Session.SpecificMembersManager();
                                    SendSessionNotification(SpecificMembersManager.SelectSpecificMembersDetail(Convert.ToInt32(SessionRequestsManager[0]["SessionId"])), NotificationTypes, Utility.Notifications.NotificationTypes.SessionChangeDateTime, SessionRequestsManager[0]["SessionTitle"].ToString(), SessionRequestsManager[0]["StartDate"].ToString(), SessionRequestsManager[0]["StartTime"].ToString(), SessionRequestsManager[0]["LocationName"].ToString());
                                }
                                else if (Convert.ToInt32(SessionRequestsManager[0]["RequestType"]) == (int)TSP.DataManager.Session.RequestTypesManager.Types.Cancel)
                                {
                                    TSP.DataManager.Session.SpecificMembersManager SpecificMembersManager = new TSP.DataManager.Session.SpecificMembersManager();
                                    SendSessionNotification(SpecificMembersManager.SelectSpecificMembersDetail(Convert.ToInt32(SessionRequestsManager[0]["SessionId"])), NotificationTypes, Utility.Notifications.NotificationTypes.SessionCancel, SessionRequestsManager[0]["SessionTitle"].ToString(), SessionRequestsManager[0]["StartDate"].ToString(), SessionRequestsManager[0]["StartTime"].ToString(), SessionRequestsManager[0]["LocationName"].ToString());
                                }
                            }
                        }
                        #endregion

                        #region Other
                        WorkFlowTaskManager.FindByCode(SelectedTaskId);
                        string WFTaskSMSBody = "";
                        if (WorkFlowTaskManager.Count == 1)
                            WFTaskSMSBody = WorkFlowTaskManager[0]["SmsBody"].ToString();
                        if (chbIsSendSMS.Checked || !Utility.IsDBNullOrNullValue(WFTaskSMSBody) || (dtExtraInfo.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtExtraInfo.Rows[0]["SMSBody"])))
                        {
                            if (CheckBoxDoNotSendSMS.Checked == false)
                                SendSMSNotification(TableId, dtExtraInfo, WFTaskSMSBody);
                        }
                        #endregion
                    }
                    catch (Exception err)
                    {
                        Utility.SaveWebsiteError(err);
                    }
                    #endregion
                    #region Call Shardari WebService For 
                    if (WfCode == (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming)
                    {

                        int TaskIdConfirmTSWorkRequestConfirminAndEndProccess = FindSelectedTaskId((int)TSP.DataManager.WorkFlowTask.ConfirmTSWorkRequestConfirminAndEndProccess, WorkFlowTaskManager);
                        if (TaskIdConfirmTSWorkRequestConfirminAndEndProccess == SelectedTaskId)
                        {
                            TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
                            ObserverWorkRequestChangesManager.FindByObsWorkReqChangeId(TableId);
                            if (ObserverWorkRequestChangesManager.Count == 0)
                            {
                                SetMsgText("ذخیره انجام شد.خطایی در ارسال اطلاعات به شهرداری ایجاد شده.");
                                return;
                            }
                            int LastMfId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MfId"]);
                            int MeId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MeId"]);
                            int MjParentId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MasterMfMjParentId"]);
                            string ObsDate = Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ObsDate"]) ? "" : ObserverWorkRequestChangesManager[0]["ObsDate"].ToString();
                            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
                            int AgentId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MeAgentId"]);
                            string Year = "";
                            if (AgentId == Utility.GetCurrentAgentCode())
                                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
                            else
                                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
                            if (CapacityAssignmentManager.Count > 0)
                            {
                                Year = CapacityAssignmentManager[0]["Year"].ToString();
                            }

                            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
                            System.Data.DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, MjParentId);
                            System.Data.DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, MjParentId);
                            System.Data.DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping, -1, MjParentId);
                            string DesDate = dtMeDetailDesign.Rows.Count > 0 ? (Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["Date"]) ? "" : dtMeDetailDesign.Rows[0]["Date"].ToString()) : "";
                            string UrbenismDate = dtMeDetailUrbanism.Rows.Count > 0 ? (Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["Date"]) ? "" : dtMeDetailUrbanism.Rows[0]["Date"].ToString()) : "";
                            string MappingDate = dtMeDetailMapping.Rows.Count > 0 ? (Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["Date"]) ? "" : dtMeDetailMapping.Rows[0]["Date"].ToString()) : "";
                            ShahrdariWebservice(MjParentId,
                                ObsDate
                            , DesDate
                            , UrbenismDate
                            , MappingDate
                             , Convert.ToDecimal(ObserverWorkRequestChangesManager[0]["ShirazMunicipalityMeter"])
                            , Convert.ToDecimal(ObserverWorkRequestChangesManager[0]["ShirazMunicipalityDesignMeter"])
                            , Convert.ToDecimal(ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismTarh"]) + Convert.ToDecimal(ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]), Convert.ToInt16(Year), MeId);
                        }
                    }
                    #endregion
                    #endregion
                    break;
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMsgText("خطایی در ذخیره انجام گرفته است.");
        }

        if (this.EndNextStep != null)
            this.EndNextStep(this, EventArgs.Empty);
    }

    private void DoNextTaskOfPopUpClose(int WFCode)
    {
        switch (WFCode)
        {
            case (int)TSP.DataManager.WorkFlows.TSProjectConfirming:
            case (int)TSP.DataManager.WorkFlows.TSPlansConfirming:
                if (!string.IsNullOrEmpty(QueryStringForRedirect))
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(QueryStringForRedirect);
                break;
        }

    }
    #region Set Messages
    /// <summary>
    /// پیغام مورد نظر را در پنجره گردش کار نمایش می دهد
    /// </summary>
    /// <param name="msg"></param>
    public void SetMsgText(string msg)
    {
        PanelSaveSuccessfully.ClientVisible = true;
        PanelMain.ClientVisible = false;
        lblWFWarning.ForeColor = System.Drawing.Color.Red;
        if (!string.IsNullOrWhiteSpace(lblWFWarning.Text))
        {
            lblWFWarning.Text += "\n";
        }
        lblWFWarning.Text += msg;
    }

    /// <summary>
    /// پیغام مورد نظر را در پنجره گردش کار به رنگ انتخاب شده نمایش می دهد
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="color"></param>
    public void SetMsgText(string msg, System.Drawing.Color color)
    {
        PanelSaveSuccessfully.ClientVisible = true;
        PanelMain.ClientVisible = false;
        lblWFWarning.ForeColor = color;
        lblWFWarning.Text = msg;
    }
    #endregion

    /// <summary>
    /// اطلاعات کاربر جاری در چارت سازمانی را براساس یک عملیات خاص در گردش کار بدست می آورد
    /// </summary>
    /// <param name="TaskId"></param>
    /// <returns></returns>
    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetMsgText("سمت سازمانی شما در تنظیمات سطح دسترسی گردش کار، برای این عملیات ثبت نشده است");
            return (-1);
        }
    }

    private int FindSelectedTaskId(int TaskCode, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
    {
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count == 1)
            return (Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]));
        return -1;
    }

    private int FindSelectedTaskId(string RequestName, int CurrentTaskCode, int CurrentTaskOrder)
    {
        int SelectedTaskId = -1;
        //if (Utility.IsDBNullOrNullValue(Session[SessionName]))
        //    return SelectedTaskId;
        //// DataTable dtSendBack = (DataTable)Session[SessionName];
        switch (RequestName)
        {
            case "Send":
                cmbSendBackTask.DataSource = dtSendBack;
                cmbSendBackTask.ValueField = "TaskId";
                cmbSendBackTask.TextField = "TaskName";
                cmbSendBackTask.DataBind();
                if (cmbSendBackTask.SelectedItem == null)
                {
                    //return SelectedTaskId;
                    return -2;
                }
                SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
                break;
            case "SendNextStep":
                if (dtSendBack.Rows.Count == 0)
                    return -1;
                DataRow[] drNextStep = dtSendBack.Select("TaskOrder > " + CurrentTaskOrder + " and TaskOrder <> 0"
                     + " and Type <>" + (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask, "TaskOrder asc"
                   );
                if (drNextStep.Length == 0)
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    SetMsgText("جهت تایید و پاس پرونده از گزینه ارسال پیشرفته استفاده نمایید.");
                }
                if (drNextStep.Length > 0)
                {
                    SelectedTaskId = Convert.ToInt32(drNextStep[0]["TaskId"].ToString());
                }
                break;
            case "SendPreStep":
                if (dtSendBack.Rows.Count == 0)
                    return -1;
                DataRow[] drPreStep = dtSendBack.Select("TaskOrder < " + CurrentTaskOrder + " and Type <>" + (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask
                                        + " and Type <>" + (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask + " and TaskOrder <> 0", "TaskOrder desc");

                if (drPreStep.Length == 0)
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    SetMsgText("جهت مرجوع پرونده از گزینه ارسال پیشرفته استفاده نمایید.");
                }
                if (drPreStep.Length > 0)
                {
                    SelectedTaskId = Convert.ToInt32(drPreStep[0]["TaskId"].ToString());
                }
                break;
            case "EndConfirm":
                if (dtSendBack.Rows.Count == 0)
                    return -1;
                DataRow[] drEndConfirmStep = dtSendBack.Select("Type = " + (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask);
                if (drEndConfirmStep.Length == 0)
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    SetMsgText("شما سطح دسترسی گردش کار جهت تایید کلی پرونده را ندارید.");
                }
                else if (drEndConfirmStep.Length > 0)
                {
                    SelectedTaskId = Convert.ToInt32(drEndConfirmStep[0]["TaskId"].ToString());
                }
                break;
            case "EndReject":
                if (dtSendBack.Rows.Count == 0)
                    return -1;
                DataRow[] drEndRejectStep = dtSendBack.Select("Type = " + (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask);
                if (drEndRejectStep.Length == 0)
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    SetMsgText("شما سطح دسترسی گردش کار جهت رد کلی پرونده را ندارید.");
                }
                else if (drEndRejectStep.Length > 0)
                {
                    SelectedTaskId = Convert.ToInt32(drEndRejectStep[0]["TaskId"].ToString());
                }
                break;
        }
        return SelectedTaskId;
    }
    #endregion

    #region SessionNotifications
    private ArrayList GetSessionNotificationSendType(int SessionDeclare)
    {
        TSP.DataManager.Session.SessionDeclareTypesManager SessionDeclareTypesManager = new TSP.DataManager.Session.SessionDeclareTypesManager();
        SessionDeclareTypesManager.Fill();
        ArrayList NotificationSendTypes = new ArrayList();
        for (int i = 0; i < SessionDeclareTypesManager.Count; i++)
        {
            int DeclareTypeId = Convert.ToInt32(SessionDeclareTypesManager[i]["TypeId"]);
            int y = DeclareTypeId;

            if ((y &= SessionDeclare) == DeclareTypeId)
                NotificationSendTypes.Add(DeclareTypeId);
        }

        return NotificationSendTypes;
    }

    private void SendSessionNotification(DataTable dtMembers, ArrayList SendTypes, Utility.Notifications.NotificationTypes NotificationType, String SessionTitle, String SessionDate, String SessionStartTime, String SessionLocation)
    {
        for (int i = 0; i < SendTypes.Count; i++)
        {
            switch (Convert.ToInt32(SendTypes[i]))
            {
                case (int)TSP.DataManager.Session.SessionDeclareTypesManager.DeclareTypes.Email:
                    Utility.Notifications EmailNotifications = new Utility.Notifications(Utility.Notifications.SendTypes.Email, NotificationType);

                    String Email = "";
                    for (int j = 0; j < dtMembers.Rows.Count; j++)
                    {
                        if (Utility.IsDBNullOrNullValue(dtMembers.Rows[j]["Email"]) == false && String.IsNullOrWhiteSpace(dtMembers.Rows[j]["Email"].ToString().Trim()) == false)
                        {
                            if (String.IsNullOrWhiteSpace(Email) == false)
                                Email += EmailNotifications.Splitter;
                            Email += dtMembers.Rows[j]["Email"].ToString();
                        }
                    }

                    if (String.IsNullOrWhiteSpace(Email) == false)
                    {
                        DataRow dr = EmailNotifications.NotificationData.NewRow();
                        dr["EmailAddress"] = Email;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionLocation"] = SessionLocation;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionChangeDateTime || NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionStartTime"] = SessionStartTime;
                        dr["SessionTitle"] = SessionTitle;
                        dr["SessionDate"] = SessionDate;
                        EmailNotifications.NotificationData.Rows.Add(dr);
                        EmailNotifications.NotificationData.AcceptChanges();
                        EmailNotifications.Send();
                    }
                    break;
                case (int)TSP.DataManager.Session.SessionDeclareTypesManager.DeclareTypes.Message:
                    Utility.Notifications MessageNotifications = new Utility.Notifications(Utility.Notifications.SendTypes.Message, NotificationType);

                    String MessageMeId = "", MessageUltId = "";
                    for (int j = 0; j < dtMembers.Rows.Count; j++)
                    {
                        if (String.IsNullOrWhiteSpace(MessageMeId) == false)
                        {
                            MessageMeId += MessageNotifications.Splitter;
                            MessageUltId += MessageNotifications.Splitter;
                        }
                        MessageMeId += dtMembers.Rows[j]["MeId"].ToString();
                        MessageUltId += dtMembers.Rows[j]["UltId"].ToString();
                    }

                    if (String.IsNullOrWhiteSpace(MessageMeId) == false)
                    {
                        DataRow dr = MessageNotifications.NotificationData.NewRow();
                        dr["MessageMeId"] = MessageMeId;
                        dr["MessageUltId"] = MessageUltId;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionLocation"] = SessionLocation;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionChangeDateTime || NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionStartTime"] = SessionStartTime;
                        dr["SessionTitle"] = SessionTitle;
                        dr["SessionDate"] = SessionDate;
                        MessageNotifications.NotificationData.Rows.Add(dr);
                        MessageNotifications.NotificationData.AcceptChanges();
                        MessageNotifications.Send();
                    }
                    break;
                case (int)TSP.DataManager.Session.SessionDeclareTypesManager.DeclareTypes.SMS:
                    SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);

                    String SMSMeId = "", SMSUltId = "", SMSMobileNo = "", SMSResult = "";
                    for (int j = 0; j < dtMembers.Rows.Count; j++)
                    {
                        if (Utility.IsDBNullOrNullValue(dtMembers.Rows[j]["MobileNo"]) == false && String.IsNullOrWhiteSpace(dtMembers.Rows[j]["MobileNo"].ToString().Trim()) == false)
                        {
                            if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
                            {
                                SMSMobileNo += SMSNotifications.Splitter;
                                SMSMeId += SMSNotifications.Splitter;
                                SMSUltId += SMSNotifications.Splitter;
                                SMSResult += SMSNotifications.Splitter;
                            }
                            SMSMeId += dtMembers.Rows[j]["MeId"].ToString();
                            SMSUltId += dtMembers.Rows[j]["UltId"].ToString();
                            SMSMobileNo += dtMembers.Rows[j]["MobileNo"].ToString();
                        }
                    }

                    if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
                    {
                        DataRow dr = SMSNotifications.NotificationData.NewRow();
                        dr["SMSMobileNo"] = SMSMobileNo;
                        dr["SMSResult"] = SMSResult;
                        dr["SMSMeId"] = SMSMeId;
                        dr["SMSUltId"] = SMSUltId;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionLocation"] = SessionLocation;
                        if (NotificationType == Utility.Notifications.NotificationTypes.SessionChangeDateTime || NotificationType == Utility.Notifications.NotificationTypes.SessionHolding)
                            dr["SessionStartTime"] = SessionStartTime;
                        dr["SessionTitle"] = SessionTitle;
                        dr["SessionDate"] = SessionDate;
                        SMSNotifications.NotificationData.Rows.Add(dr);
                        SMSNotifications.NotificationData.AcceptChanges();

                        switch (Utility.GetCurrentSMSWebService())
                        {
                            case (int)TSP.DataManager.SMSWebServiceType.Magfa:
                                SMSNotifications.SendSMSByMagfa();
                                break;
                            case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
                                SMSNotifications.SendSMS();
                                break;
                        }
                    }
                    break;
            }
        }
    }
    #endregion

    private void SendSMSNotification(int TableId, DataTable dtMembers, string WFTaskSMSBody)
    {

        if (dtMembers.Rows.Count <= 0) return;

        String SMSMeId = "", SMSUltId = "", SMSMobileNo = "", SMSResult = "", SMSBody = "";
        for (int j = 0; j < dtMembers.Rows.Count; j++)
        {
            SMSBody = dtMembers.Rows[j]["SMSBody"].ToString() + (!string.IsNullOrWhiteSpace(WFTaskSMSBody) ? (!string.IsNullOrWhiteSpace(dtMembers.Rows[j]["SMSBody"].ToString()) ? "\n" + WFTaskSMSBody : WFTaskSMSBody) : "") + (!string.IsNullOrWhiteSpace(txtSMSBody.Text.Trim()) ? "\n" + txtSMSBody.Text.Trim() : "");
            if (!Utility.IsDBNullOrNullValue(dtMembers.Rows[j]["SMSMobileNo"]) && !String.IsNullOrWhiteSpace(dtMembers.Rows[j]["SMSMobileNo"].ToString().Trim()))
            {
                SMSMeId = dtMembers.Rows[j]["SMSMeId"].ToString();
                SMSUltId = dtMembers.Rows[j]["SMSUltId"].ToString();
                SMSMobileNo = dtMembers.Rows[j]["SMSMobileNo"].ToString();
                SendSMSNotification(Utility.Notifications.NotificationTypes.AutomaticSMS, SMSBody, SMSMobileNo, SMSMeId, Convert.ToInt32(SMSUltId));
            }
        }



    }
    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId, int SMSUltId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = SMSUltId;
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }

    #region TS Methods
    # region Selected Task Is Done Operations


    private Boolean SendEmail(int MReId, TSP.DataManager.MemberRequestManager ReqManager)
    {
        ReqManager.FindByCode(MReId);
        if (ReqManager.Count > 0)
        {
            if (Convert.ToBoolean(ReqManager[0]["IsMeTemp"]) == false)
                return true;

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeId"]) && !Utility.IsDBNullOrNullValue(ReqManager[0]["Email"]))
            {
                Utility.Notifications Notification = new Utility.Notifications(Utility.Notifications.SendTypes.Email, Utility.Notifications.NotificationTypes.GetMembershipNo);
                DataRow dr = Notification.NotificationData.NewRow();
                dr["EmailAddress"] = ReqManager[0]["Email"].ToString();
                dr["MeId"] = ReqManager[0]["MeId"].ToString();
                dr["Username"] = ReqManager[0]["MeId"].ToString();
                Notification.NotificationData.Rows.Add(dr);
                Notification.NotificationData.AcceptChanges();
                return Notification.Send();
            }
        }
        return false;
    }

    #endregion

    #region Shahrdari Webservice Workrequest

    private void ShahrdariWebservice(int MjParentId, string ObsDate, string DesignDate, string UrbenismDate,string MappingDate, decimal MunObsCapacity, decimal MunDesignCapacity, decimal MunUrbenismCapacity, Int16 Year, int _MeId)
    {
        //        لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            #region MainServer

            WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();

            int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0; int MappingId = 0;
            Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0; Int16 MappingGrad = 0;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
            if (dtMember.Rows.Count != 1)
                return;
            string LastDocRegDate = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastDocRegDate"]) ? "" : dtMember.Rows[0]["LastDocRegDate"].ToString();
            MasterMfMjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
            ObsId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ObsId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["ObsId"]);
            DesId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["DesId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["DesId"]);
            UrbanismId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["UrbanismId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["UrbanismId"]);
            switch (ObsId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    ObsGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    ObsGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    ObsGrad = 3;
                    break;
            }
            switch (DesId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    ObsGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    DesGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    DesGrad = 3;
                    break;
            }
            switch (UrbanismId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    UrbanismGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    UrbanismGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    UrbanismGrad = 3;
                    break;
            }
            switch (MappingId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    MappingGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    MappingGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    MappingGrad = 3;
                    break;
            }
            if (string.IsNullOrWhiteSpace(ObsDate))
                ObsDate = MappingDate;
            #region اطلاعات پایه مهندس
            WorkRequestsrvEngineerToOthers.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthers.ClsEngineer1();
            ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthers.Eng_Info();
            ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
            ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
            ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
            ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
            ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
            ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
            ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
            ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
            ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
            ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
            ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
            ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
            ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
            ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
            ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
            ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
            ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
            switch (MjParentId)
            {
                case 1:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                    break;
                case 2:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                    break;
                case 3:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                    break;
                case 4:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                    break;
                case 5:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                    break;
                case 6:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                    break;
                case 7:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                    break;
            }
            IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthers);
            IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);

            #endregion
            #region اطلاعات پروانه
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement PEngjob = new WorkRequestsrvEngineerToOthers.Eng_JobAgreement();
            PEngjob.JobAgreementExportDate = LastDocRegDate;// تاریخ صدور پروانه اشتغال جاری
            PEngjob.JobAgreementExpireDate = dtMember.Rows[0]["FileDate"].ToString();//تاریخ اعتبار پروانه اشتغال جاری
                                                                                     //PEngjob.NIdEng = ClsEngineer1Info.NidEngineer;
            PEngjob.NIdJobAgreement_tmp = 0;
            PEngjob.CI_JobAgreementType = 1;
            PEngjob.CI_JobAgreementBaseExport = 0;
            PEngjob.CI_Region = 1;

            IsrvEngineerToOthersClient.SaveEng_JobAgreementCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs>(GetResultJobAgreement);
            IsrvEngineerToOthersClient.SaveEng_JobAgreementAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), PEngjob);

            #endregion
            #region اطلاعات جدول صلاحیت

            int Len = 1;
            if ((DesGrad > 0 || UrbanismGrad > 0) && (ObsGrad > 0 || MappingGrad > 0))
                Len = 2;
            WorkRequestsrvEngineerToOthers.Eng_Competence[] ListengCompetence = new WorkRequestsrvEngineerToOthers.Eng_Competence[Len];
            if (ObsGrad > 0 || MappingGrad > 0)
            {

                WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                engCompetenceObj.CI_Ability = 1;
                engCompetenceObj.CI_Base = ObsGrad > 0 ? ObsGrad : MappingGrad;
                engCompetenceObj.IsActive = true;
                ListengCompetence[0] = engCompetenceObj;

            }
            if (DesGrad > 0 || UrbanismGrad > 0)
            {
                WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();          
                if (MasterMfMjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    engCompetenceObj.CI_Ability = 4;
                else
                    engCompetenceObj.CI_Ability = 2;

                engCompetenceObj.CI_Base = DesGrad > 0 ? DesGrad : UrbanismGrad;
                engCompetenceObj.IsActive = true;
                if (Len == 1)
                    ListengCompetence[0] = engCompetenceObj;
                else if (Len == 2)
                    ListengCompetence[1] = engCompetenceObj;
            }
            IsrvEngineerToOthersClient.SaveEng_CompetenceCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs>(GetResultCompetence);
            IsrvEngineerToOthersClient.SaveEng_Competence("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListengCompetence);

            #endregion
            #region اطلاعات ظرفیت مهندس در آماده بکاری
            int Count = 1;
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                Count = 2;
            string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            WorkRequestsrvEngineerToOthers.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs[Count];
            //CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
            {
                #region نظارت و طراحی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsde.CI_Ability = 4;
                else
                    ClsQtaInputsde.CI_Ability = 2;
                ClsQtaInputsde.Date = Utility.GetDateOfToday();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsde.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsde.Meter = MunDesignCapacity;
                ClsQtaInputsde.Time = time;
                ClsQtaInputsde.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsde;
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsOb.CI_Ability = 1;
                //ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                ClsQtaInputsOb.Meter = MunObsCapacity;
                //ClsQtaInputsOb.Time = time;
                ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[1] = ClsQtaInputsOb;
                #endregion
            }
            else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ طراحی یا شهرسازی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsDes.CI_Ability = 4;
                else
                    ClsQtaInputsDes.CI_Ability = 2;
                ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsDes.Meter = MunDesignCapacity;
                ClsQtaInputsDes.Time = time;
                ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsDes;
                #endregion
            }
            else if (!string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ نظارت
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsObs.CI_Ability = 1;
                ClsQtaInputsObs.Meter = MunObsCapacity;
                ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[0] = ClsQtaInputsObs;
                #endregion
            }

            IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
            IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);

            #endregion
            #endregion
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetGetResultEngineerToOthersClient(object sender, WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthers.ClsErrorResult ErrorResult = e.Result;
        if (ErrorResult.BizErrors.Length == 0)
        {
            //ok;
        }
        else
        {
            //  ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }
    private void GetResultEngineerToOthers(object sender, WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthers.ClsEngineer1 ResultClsEngineer1 = e.Result;
        if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
        {
            //ok;
        }
        else
        {

            // ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }
    private void GetResultJobAgreement(object sender, WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs e)
    {

        WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = e.Result;
        if (ResultJobAgreement != null)
        {
            //ok;

        }
        else
        {
        }
    }

    private void GetResultCompetence(object sender, WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = e.Result;
        if (ResultCompetence.Length > 0)
        {
            //ok;
        }
        else
        {
        }
    }
    #endregion


    #endregion

}
