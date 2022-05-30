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

public partial class Office_OfficeMembershipRequest : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
    #region Events
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

            ObjectDataSourceOffice.SelectParameters[0].DefaultValue = OfId;

            Session["SendBackDataTable_OffRq"] = "";
            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming);
            if (WorkFlowManager.Count == 1)
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowManager[0]["WorkFlowId"].ToString();
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            string OfId = Utility.DecryptQS(OfficeId.Value);
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

            ReqManager.FindByOfficeId(int.Parse(OfId), 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            else
            {
                Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS("-1") + "&PageMode="
                    + Utility.EncryptQS("NewReqMembership") + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("MemberShip"));

            }
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
        if (IsPageRefresh)
            return;
        int OfReId = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfReId = (int)row["OfReId"];
        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value
            + "&Dprt=" + Utility.EncryptQS("MemberShip"));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        Response.Redirect("OfficeHome.aspx?MeId=" + OfficeId.Value);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

        int OfReId = -1;
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfReId = (int)row["OfReId"];

        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        try
        {
            if (!CheckPermitionForEdit(OfReId))
                return;

            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return;
            }
            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
                && Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش درخواست مورد نظر وجود ندارد";
                return;
            }
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
                return;
            }

            Response.Redirect("OfficeRequestInsert.aspx?OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                + "&OfId=" + OfficeId.Value + "&Dprt=" + Utility.EncryptQS("MemberShip"));

        }
        catch (Exception ex)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        int OfReId = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfReId = (int)row["OfReId"];
        }
        if (OfReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        if (!CheckPermitionForDelete(OfReId))
            return;

        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            return;
        }
        if (ReqManager[0]["Type"].ToString() == "0")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان لغو درخواست اولیه وجود ندارد";
            return;
        }
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

        Delete(OfReId);
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        //if (GridViewOffice.FocusedRowIndex > -1)
        //{
        //    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
        //    DataRow Row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
        //    int TableId = int.Parse(Row["OfReId"].ToString());

        //    Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید.";
        //}
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
            DataRow DocMeFileRow = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
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

    protected void GridViewOffice_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewOffice.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewOffice.Columns["WFState"], "btnWFState");
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

    protected void GridViewOffice_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "AnswerDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    #region WorkFlow
    private void SendMemberFileDocToNextStep(int OfReId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
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
                            GridViewOffice.DataBind();
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
            GridViewOffice.DataBind();
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.Visible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مرحله بعد گردش کار نا مشخص است.";
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
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;

            int Permission = ReqManager.CheckPermissionOfficeConfirmingSendBackTask(TableId, CurrentTaskCode);
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
                                    lblInstitueWarning.Text = "عملیات بعد در گردش کار نامشخص است.";
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
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            int OfReId = int.Parse(MeFileRow["OfReId"].ToString());
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
            if (e.Parameter == "Send")
            {
                SendMemberFileDocToNextStep(OfReId);
                GridViewOffice.DataBind();
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
    #endregion

    private Boolean CheckPermitionForEdit(int TableId)
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
                        return true;
                        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        //if (dtWorkFlowState.Rows.Count > 0)
                        //{
                        //    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        //    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                        //    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                        //    if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                        //    {
                        //        if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                        //            return true;
                        //        else
                        //            Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                        //    }
                        //}
                    }
                }
            }
        }
        Message = "امکان ویرایش این درخواست برای شما وجود ندارد";
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

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
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
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان لغو درخواست در این مرحله از گردش کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }

    protected void Delete(int OfReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();

        trans.Add(ReqManager);
        trans.Add(AgentManager);
        trans.Add(MemManager);
        trans.Add(JobManager);
        trans.Add(LetterManager);
        trans.Add(FinManager);
        trans.Add(JobQualityManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(RequestInActivesManager);


        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

            ReqManager.FindByCode(OfReId);

            if (ReqManager.Count > 0)
            {
                trans.BeginSave();

                AgentManager.FindForDelete(OfReId);
                if (AgentManager.Count > 0)
                {
                    int c = AgentManager.Count;
                    for (int i = 0; i < c; i++)
                        AgentManager[0].Delete();

                    AgentManager.Save();
                }

                MemManager.FindForDelete(OfReId, 0);
                if (MemManager.Count > 0)
                {
                    int c = MemManager.Count;
                    for (int i = 0; i < c; i++)
                        MemManager[0].Delete();

                    MemManager.Save();

                }

                LetterManager.FindForDelete(OfReId);
                if (LetterManager.Count > 0)
                {
                    int c = LetterManager.Count;
                    for (int i = 0; i < c; i++)
                        LetterManager[0].Delete();

                    LetterManager.Save();
                }


                JobManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableType.OfficeRequest);

                if (JobManager.Count > 0)
                {
                    int c = JobManager.Count;

                    for (int i = 0; i < c; i++)
                    {
                        JobQualityManager.FindByJobCode(int.Parse(JobManager[i]["JhId"].ToString()));
                        if (JobQualityManager.Count > 0)
                        {
                            int y = JobQualityManager.Count;
                            for (int x = 0; x < y; x++)
                                JobQualityManager[0].Delete();

                            JobQualityManager.Save();
                        }

                        JobManager[0].Delete();

                    }

                    JobManager.Save();
                }

                FinManager.FindForDelete(OfReId);
                if (FinManager.Count > 0)
                {
                    int c = FinManager.Count;
                    for (int i = 0; i < c; i++)
                        FinManager[0].Delete();

                    FinManager.Save();
                }

                int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, OfReId);
                if (WorkFlowStateManager.Count > 0)
                {
                    WorkFlowStateManager[0].Delete();
                    WorkFlowStateManager.Save();
                }

                RequestInActivesManager.FindByTableIdTableType(-1, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember), OfReId);
                int ReqInCount = RequestInActivesManager.Count;
                if (ReqInCount > 0)
                {
                    for (int i = 0; i < ReqInCount; i++)
                    {
                        RequestInActivesManager[0].Delete();
                        RequestInActivesManager.Save();
                        RequestInActivesManager.DataTable.AcceptChanges();
                    }
                }

                ReqManager[0].Delete();

                int cn = ReqManager.Save();

                trans.EndSave();
                GridViewOffice.DataBind();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "درخواست مورد نظر با موفقیت لغو شد";


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
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان لغو درخواست مورد نظر وجود ندارد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در لغو درخواست انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در لغو درخواست انجام گرفته است";
            }
        }
    }
    #endregion


}
