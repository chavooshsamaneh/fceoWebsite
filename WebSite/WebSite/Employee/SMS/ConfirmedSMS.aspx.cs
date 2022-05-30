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

public partial class Employee_SMS_ConfirmedSMS : System.Web.UI.Page
{
    //Boolean IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            GridViewOutBox.JSProperties["cpShowSendSMS"] = 0;
            GridViewOutBox.JSProperties["cpSelectedRowValue"] = 0;
            GridViewOutBox.JSProperties["cpIsPostBack"] = 1;
            #region Permission
            TSP.DataManager.Permission Per = TSP.DataManager.SmsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnView.Enabled = Per.CanView;
            btnView2.Enabled = Per.CanView;
            btnTracing.Enabled = Per.CanView;
            btnTracing2.Enabled = Per.CanView;
            GridViewOutBox.Visible = Per.CanView;
            BtnNew.Enabled = Per.CanNew;
            BtnNew2.Enabled = Per.CanNew;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnSendSMS.Enabled = btnSendSMS2.Enabled = CheckWorkFlowPermissionForConfirmAndSendSMS();

            TSP.DataManager.Permission PerSmsReciever = TSP.DataManager.SmsRecieverManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSMSRecievers.Enabled = PerSmsReciever.CanView;
            btnSMSRecievers2.Enabled = PerSmsReciever.CanView;
            #endregion
            Session["SendBackDataTable_SMSConf"] = "";

            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.SMSConfirming);
            if (WorkFlowManager.Count == 1)
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowManager[0]["WorkFlowId"].ToString();

            cmbSMSType.DataBind();
            cmbSMSType.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbSMSType.SelectedIndex = 0;

            cmbLanguage.DataBind();
            cmbLanguage.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbLanguage.SelectedIndex = 0;
            FillGridByUser();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSendSMS"] = btnSendSMS.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnSMSRecievers"] = btnSMSRecievers.Enabled;
            this.ViewState["BtnTracing"] = btnTracing.Enabled;
        }

        SetPageFilter();
        SetGridRowIndex();

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnSendSMS"] != null)
            this.btnSendSMS.Enabled = this.btnSendSMS2.Enabled = (bool)this.ViewState["BtnSendSMS"];
        if (this.ViewState["BtnSMSRecievers"] != null)
            this.btnSMSRecievers.Enabled = this.btnSMSRecievers2.Enabled = (bool)this.ViewState["BtnSMSRecievers"];
        if (this.ViewState["BtnTracing"] != null)
            this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnTracing"];

        string script = @"<SCRIPT language='javascript'> function CheckSearch() {var SmsSubject = txtSmsSubject.GetText; var txtStartDate = document.getElementById('" + txtStartDate.ClientID + "').value;";
        script += "var txtEndDate = document.getElementById('" + txtEndDate.ClientID + "').value;";
        script += "var txtSMSDotoDateFrom = document.getElementById('" + txtSMSDotoDateFrom.ClientID + "').value;";
        script += "var txtSMSDotoDateTo = document.getElementById('" + txtSMSDotoDateTo.ClientID + "').value;";
        script += "var txtExpireDateFrom = document.getElementById('" + txtExpireDateFrom.ClientID + "').value;";
        script += "var txtExpireDateTo = document.getElementById('" + txtExpireDateTo.ClientID + "').value;";

        script += "if (SmsSubject=='' && txtStartDate==''&& txtEndDate=='' && txtSMSDotoDateFrom=='' && txtSMSDotoDateTo=='' && txtExpireDateFrom=='' &&  txtExpireDateTo=='' && txtRecieverId.GetText()=='' && txtRecieverMobile.GetText()==''&& cmbLanguage.GetSelectedIndex() == 0 && cmbSMSType.GetSelectedIndex() == 0 ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtSmsSubject.SetText('');
                    txtRecieverId.SetText(''); txtRecieverMobile.SetText('');
                    cmbLanguage.SetSelectedIndex(0); cmbSMSType.SetSelectedIndex(0);
                    document.getElementById('" + txtStartDate.ClientID + "').value = ''; document.getElementById('" + txtEndDate.ClientID + "').value = '';";
        script += "document.getElementById('" + txtSMSDotoDateFrom.ClientID + "').value = '';document.getElementById('" + txtSMSDotoDateTo.ClientID + "').value = '';document.getElementById('" + txtExpireDateFrom.ClientID + "').value = '';document.getElementById('" + txtExpireDateTo.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);

    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewOutBox.FocusedRowIndex > -1)
        {
            DataRow SMSRow = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            if (SMSRow != null)
            {
                int SMSId = (int)SMSRow["SmsId"];
                if (!CheckPermitionForEdit(SMSId))
                {
                    return;
                }
                NextPage("Edit");
            }
            else
            {
                ShowMessage("ردیفی انتخاب نشده است.");
            }
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewOutBox.FocusedRowIndex > -1)
        {
            DataRow SMSRow = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            if (SMSRow != null)
            {
                int SMSId = (int)SMSRow["SmsId"];
                if (CheckPermitionForDelete(SMSId))
                {
                    DeleteSMS(SMSId);
                }
                else
                {
                    InActiveSMS(SMSId);
                }
                GridViewOutBox.DataBind();
            }
            else
            {
                ShowMessage("ردیفی انتخاب نشده است.");
            }
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {

        if (GridViewOutBox.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex)["SMSId"].ToString());
            string GridFilterString = GridViewOutBox.FilterExpression;
            string SearchFilterString = GenerateFilterString();
            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);

            int TableType = (int)TSP.DataManager.TableCodes.SMS;
            DataRow SMSRow = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            int TableId = int.Parse(SMSRow["SMSId"].ToString());
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.SMSConfirming;

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }

    protected void btnSMSRecievers_Click(object sender, EventArgs e)
    {
        int SMSId = -1;
        if (GridViewOutBox.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            SMSId = (int)row["SMSId"];
            Response.Redirect("SMSRecieveresReport.aspx?SMSId=" + Utility.EncryptQS(SMSId.ToString()));
        }
        else
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex)["WFTaskType"]) != (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
        {
            ShowMessage("گردش کار پیامک انتخاب شده، تایید نشده است");
            return;
        }

        int SMSId = Convert.ToInt32(GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex)["SMSId"]);
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count == 1)
        {
            if ((Convert.ToBoolean(SmsManager[0]["InActive"])))
            {
                ShowMessage("پیامک انتخاب شده غیر فعال شده است.امکان ارسال پیامک غیر فعال شده وجود ندارد.");
                return;
            }
            String SMSExpireDate = SmsManager[0]["ExpireDate"].ToString();
            String DateNow = Utility.GetDateOfToday();
            if (String.Compare(SMSExpireDate, DateNow) < 0)
            {
                ShowMessage("مهلت اعتبار پیامک به پایان رسیده است");
                return;
            }
            if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SMSDotoDate"]))
            {
                if (String.Compare(DateNow, SmsManager[0]["SMSDotoDate"].ToString()) < 0)
                {
                    ShowMessage("مهلت اعتبار پیامک آغاز نشده است");
                    return;
                }
            }
        }
        else
        {
            ShowMessage("خطایی در خواندن اطلاعات ایجاد گردیده است");
            return;
        }
        string GridFilterString = GridViewOutBox.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        Response.Redirect("SendSMS.aspx?SMSId=" + Utility.EncryptQS(SMSId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "SMSInbox";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }
    #endregion

    #region GridView
    protected void GridViewOutBox_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewOutBox.JSProperties["cpShowSendSMS"] = 0;
        GridViewOutBox.JSProperties["cpSelectedRowValue"] = 0;
        if (e.Parameters == TSP.DataManager.WorkFlowPermission.WFUserControlGridsCallbackName)
        {
            int SMSId = Convert.ToInt32(GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex)["SMSId"]);
            if (Convert.ToInt32(GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex)["WFTaskType"]) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
            {
                GridViewOutBox.JSProperties["cpShowSendSMS"] = 1;
                GridViewOutBox.JSProperties["cpSelectedRowValue"] = SMSId;
            }
        }
        GridViewOutBox.DataBind();
    }

    protected void GridViewConfirmMsg_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewOutBox_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (!string.IsNullOrEmpty(txtRecieverId.Text))
        {
            TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
            SmsRecieverManager.FindByRecieverId(int.Parse(e.GetValue("SmsId").ToString()), int.Parse(txtRecieverId.Text.Trim()));
            if (SmsRecieverManager.Count > 0)
            {
                //گيرنده بدون شماره
                if (Utility.IsDBNullOrNullValue(SmsRecieverManager[0]["RecieverCellPhone"]))
                    e.Row.BackColor = System.Drawing.Color.Red;
            }
        }

        if (e.GetValue("WFTaskType") != null)
        {
            if (e.GetValue("WFTaskType").ToString() == "1" || e.GetValue("WFTaskType").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }

    }

    protected void GridViewOutBox_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewOutBox.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewOutBox.Columns["WFState"], "btnWFState");
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

    #endregion

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewOutBox.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            DataRow row = GridViewOutBox.GetDataRow(GridViewOutBox.FocusedRowIndex);
            int SMSId = int.Parse(MeFileRow["SMSId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.SMS;
            int WFCode = (int)TSP.DataManager.WorkFlows.SMSConfirming;
            WFUserControl.PerformCallback(SMSId, TableType, WFCode, e);
        }
        else
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
        }

    }
    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int SMSId = -1;
        int focucedIndex = GridViewOutBox.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewOutBox.GetDataRow(focucedIndex);
            SMSId = (int)row["SMSId"];
        }
        if (SMSId == -1 && Mode != "New")
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                SMSId = -1;
                Response.Redirect("NewSms.aspx?SMSId=" + Utility.EncryptQS(SMSId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&PgName=" + Utility.EncryptQS("ConfirmedSMS.aspx"));
            }
            else
            {

                string GridFilterString = GridViewOutBox.FilterExpression;
                string SearchFilterString = GenerateFilterString();
                Response.Redirect("NewSms.aspx?SMSId=" + Utility.EncryptQS(SMSId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&PgName=" + Utility.EncryptQS("ConfirmedSMS.aspx") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
        }
    }

    private void DeleteSMS(int SMSId)
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(SmsManager);
        TransactionManager.Add(SmsRecieverManager);

        try
        {
            TransactionManager.BeginSave();

            DataTable dtSMSRe = SmsRecieverManager.FindBySMSId(SMSId);
            if (dtSMSRe.Rows.Count > 0)
            {
                int ReCount = dtSMSRe.Rows.Count;
                for (int i = 0; i < ReCount; i++)
                {
                    SmsRecieverManager.FindByCode(int.Parse(dtSMSRe.Rows[i]["SmsReId"].ToString()));
                    if (SmsRecieverManager.Count == 1)
                    {
                        SmsRecieverManager[0].Delete();
                        int cn = SmsRecieverManager.Save();
                        if (cn < 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage("خطایی در حذف انجام گرفته است");
                            return;
                        }
                    }

                }
            }
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1)
            {
                SmsManager[0].Delete();
                int count = SmsManager.Save();
                if (count > 0)
                {
                    TransactionManager.EndSave();
                    ShowMessage("ذخیره انجام شد.");
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در حذف انجام گرفته است");
                }
            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در حذف انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetDeleteError(err);
        }
    }

    private void InActiveSMS(int SMSId)
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        try
        {
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1)
            {
                SmsManager[0].BeginEdit();
                SmsManager[0]["InActive"] = 1;
                SmsManager[0].EndEdit();
                int cn = SmsManager.Save();
                if (cn > 0)
                {
                    ShowMessage("ذخیره انجام شد.");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                ShowMessage("خطایی در حذف انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در حذف انجام گرفته است");
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowMessage("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSMSInfo;
        int TaskId = -1;

        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());


        int TableType = (int)TSP.DataManager.TableCodes.SMS;
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowState.Rows.Count <= 0)
        {
            ShowMessage("برای پیامک انتخاب شده وضعیت گردش کار نامشخص می باشد.");
            return false;
        }
        int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
        int PermissionSaveInfoITManager = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS, Utility.GetCurrentUser_UserId());
        if (PermissionSaveInfoITManager > 0)
            return true;
        if (TaskId != CurrentTaskId)
        {
            ShowMessage("با توجه به مرحله گردش کار پیامک انتخاب شده قادر به ویرایش اطلاعات آن نمی باشید.");
            return false;
        }
        DataTable dtFIrstWFState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        if (dtFIrstWFState.Rows.Count <= 0)
        {
            ShowMessage("برای پیامک انتخاب شده وضعیت گردش کار نامشخص می باشد.");
            return false;
        }
        int CurrentNmcId = int.Parse(dtFIrstWFState.Rows[0]["NmcId"].ToString());
        NezamMemberChartManager.FindByNmcId(CurrentNmcId);
        if (NezamMemberChartManager.Count <= 0)
        {
            ShowMessage("اطلاعات کاربری شما در چارت سازمانی نا مشخص می باشد.");
            return false;
        }
        int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
        if (EmpId != Utility.GetCurrentUser_MeId())
        {
            ShowMessage("تنها ایجاد کننده پیامک و مدیریت واحد فناوری اطلاعات، قادر به ویرایش اطلاعات آن می باشد.");
            return false;
        }
        return true;
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.SMSConfirming
         , (int)TSP.DataManager.WorkFlowTask.SaveSMSInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);

        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        //int TaskOrder = -1;
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSMSInfo;
        //int TaskId = -1;

        //WorkFlowTaskManager.FindByTaskCode(TaskCode);
        //if (WorkFlowTaskManager.Count > 0)
        //{
        //    TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        //    TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

        //    if (TaskOrder != 0)
        //    {
        //        int TableType = (int)TSP.DataManager.TableCodes.SMS;
        //        DataTable dtWfStates = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        //        if (dtWfStates.Rows.Count > 1)
        //        {
        //            return false;
        //        }
        //        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        //        if (dtWorkFlowState.Rows.Count > 0)
        //        {
        //            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
        //            if (TaskId == CurrentTaskId)
        //            {
        //                int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
        //                NezamMemberChartManager.FindByNmcId(CurrentNmcId);
        //                if (NezamMemberChartManager.Count > 0)
        //                {
        //                    int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
        //                    if (EmpId == Utility.GetCurrentUser_MeId())
        //                    {
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }

        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}
    }

    #region Set Grid Index
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))// && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());
                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewOutBox.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {
                string PostId = Utility.DecryptQS(Request.QueryString["PostId"].ToString());
                if (!string.IsNullOrEmpty(PostId))
                {
                    int PostKeyValue = int.Parse(PostId);

                    GridViewOutBox.DataBind();
                    Index = GridViewOutBox.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewOutBox.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewOutBox.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewOutBox.JSProperties["cpSelectedIndex"] = Index;
                        GridViewOutBox.DetailRows.ExpandRow(Index);
                        GridViewOutBox.FocusedRowIndex = Index;
                        GridViewOutBox.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < objdsSMS.SelectParameters.Count; i++)
        {
            if (objdsSMS.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += objdsSMS.SelectParameters[i].Name + "&";
                FilterString += objdsSMS.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            objdsSMS.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "SMSStartDate":

                        txtStartDate.Text = Value;
                        break;
                    case "SMSEndDate":
                        txtEndDate.Text = Value;
                        break;
                    case "RecieverCellPhone":
                        txtRecieverMobile.Text = Value;
                        break;
                    case "ExpireDateFrom":
                        txtExpireDateFrom.Text = Value;
                        break;
                    case "ExpireDateTo":
                        txtExpireDateTo.Text = Value;
                        break;
                    case "SMSDotoDateTo":
                        txtSMSDotoDateTo.Text = Value;
                        break;
                    case "SMSDotoDateFrom":
                        txtSMSDotoDateFrom.Text = Value;
                        break;
                    case "IsFarsi":
                        cmbLanguage.DataBind();
                        if (Value == "-1")
                        {
                            cmbLanguage.Items.Insert(0, new ListEditItem("<همه>", null));
                            cmbLanguage.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbLanguage.SelectedIndex = cmbLanguage.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "SmsTypeId":
                        cmbSMSType.DataBind();
                        if (Value == "-1")
                        {
                            cmbSMSType.Items.Insert(0, new ListEditItem("<همه>", null));
                            cmbSMSType.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbSMSType.SelectedIndex = cmbLanguage.Items.FindByValue(Value).Index;
                        }
                        break;
                }
            }
        }
    }
    #endregion

    #region Sending SMS Methods

    /// <summary>
    /// Do next task of sending SMS
    /// </summary>
    /// <param name="SmsManager"></param>
    /// <param name="SmsRecieverManager"></param>
    /// <param name="SMSId"></param>
    /// <param name="SelectedTaskId"></param>
    /// <returns></returns>
    private int DoNextTaskOfSMS(TSP.DataManager.SmsManager SmsManager, TSP.DataManager.SmsRecieverManager SmsRecieverManager, int SMSId, int SelectedTaskId)
    {
        int Per = -1;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int RejectProccessTaskCode = -1;
        int ConfirmProccessTaskCode = -1;
        int ConfirmProccessTaskId = -1;
        int RejectProccessTaskId = -1;

        RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectSMSAndEndProcess;
        ConfirmProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmSMSAndEndProccess;
        WorkFlowTaskManager.FindByTaskCode(ConfirmProccessTaskCode);
        if (WorkFlowTaskManager.Count == 1)
        {
            ConfirmProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        }

        WorkFlowTaskManager.FindByTaskCode(RejectProccessTaskCode);
        if (WorkFlowTaskManager.Count == 1)
        {
            RejectProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        }

        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count == 1)
        {
            string SMSExpireDate = SmsManager[0]["ExpireDate"].ToString();
            string DateNow = Utility.GetDateOfToday();
            int IsExpired = string.Compare(SMSExpireDate, DateNow);
            if (IsExpired < 0)
            {
                return (int)TSP.DataManager.ErrorSMSRequest.SMSExpired;
            }
        }
        else
        {
            return (int)TSP.DataManager.ErrorSMSRequest.SMSInfoWasLost;
        }

        if (SelectedTaskId == ConfirmProccessTaskId)
        {
            return (DoNextTaskOfSMSConfirming(SmsManager, SmsRecieverManager, SMSId));
        }
        else if (SelectedTaskId == RejectProccessTaskId)
        {
            return (DoNextTaskOfSMSRejecting(SmsManager, SmsRecieverManager, SMSId));
        }
        else
        {
            Per = 0;
        }

        return Per;
    }

    /// <summary>
    /// Perform the next tasks of Confirming MemberRequest
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="CurrentUserAgentId"></param>
    /// <param name="CurrentUserId"></param>
    /// <returns></returns>
    private int DoNextTaskOfSMSConfirming(TSP.DataManager.SmsManager SmsManager, TSP.DataManager.SmsRecieverManager SmsRecieverManager, int SMSId)
    {
        int Per = -1;
        string[] SMSInfo = new string[3];
        SMSInfo = Utility.GetSMSWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        long Number = long.Parse(SMSInfo[2]);
        ArrayList arlMobileSMS = new ArrayList();
        string[] MobileNumbers = new string[0];
        string[] SubMobileNumbers = new string[0];
        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count == 1 && !(Convert.ToBoolean(SmsManager[0]["InActive"])))
        {
            string SMSBody = SmsManager[0]["SmsBody"].ToString();
            DataTable dtSMSReciever = SmsRecieverManager.FindBySMSId(SMSId);
            if (dtSMSReciever.Rows.Count > 0)
            {
                MobileNumbers = new string[dtSMSReciever.Rows.Count];
                int RecieverCount = dtSMSReciever.Rows.Count;
                //*****SendType=0 SMS don't save In MemoryCard,SendType=1 SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
                string SendType = "1";
                int SubRecieverCount = (RecieverCount / 80) + 1;
                int StartPosition = 0;
                int EndPosition = 0;
                for (int j = 0; j < SubRecieverCount; j++)
                {
                    EndPosition = StartPosition + 80;
                    if (EndPosition > RecieverCount)
                        EndPosition = RecieverCount;
                    for (int i = 0; i < EndPosition; i++)
                    {
                        MobileNumbers[i] = dtSMSReciever.Rows[i + StartPosition]["RecieverCellPhone"].ToString();
                    }
                    string[] SMSResult = SendMessage(UserName, PassWord, Number, MobileNumbers, SMSBody, SendType);
                    Per = SMSDeliveryReport(SMSResult, SMSId, dtSMSReciever, SmsManager, SmsRecieverManager);
                    MobileNumbers = new string[0];
                    StartPosition = StartPosition + 80;
                }
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
        return Per;
    }

    /// <summary>
    /// Perform the next tasks of Rejecting MemberRequest
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="CurrentUserId"></param>
    /// <returns></returns>
    private int DoNextTaskOfSMSRejecting(TSP.DataManager.SmsManager SmsManager, TSP.DataManager.SmsRecieverManager SmsRecieverManager, int SMSId)
    {
        int Per = 0;

        return Per;
    }

    /// <summary>
    /// Send SMS and returns the sms's result
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <param name="Number"></param>
    /// <param name="MobilesNumbers"></param>
    /// <param name="Body"></param>
    /// <param name="sendType"></param>
    /// <returns>SMSResult</returns>
    private string[] SendMessage(string UserName, string PassWord, long Number, string[] MobilesNumbers, string Body, string sendType)
    {
        ir.afe.www.WebService WebService = new ir.afe.www.WebService();
        string[] SMSRerult = WebService.SendMessage(UserName, PassWord, Number, MobilesNumbers, Body, sendType);
        return SMSRerult;
    }

    /// <summary>
    /// Find the report of sms delivery by the returned value
    /// </summary>
    /// <param name="ReturnedValue"></param>
    /// <param name="SMSId"></param>
    /// <param name="RecieversDataTable"></param>
    /// <param name="SmsManager"></param>
    /// <param name="SmsRecieverManager"></param>
    /// <returns></returns>
    private int SMSDeliveryReport(string[] ReturnedValue, int SMSId, DataTable RecieversDataTable, TSP.DataManager.SmsManager SmsManager, TSP.DataManager.SmsRecieverManager SmsRecieverManager)
    {
        int Per = -1;
        int SuccessfullCount = 0;
        int SmsReId = -1;
        for (int i = 0; i < ReturnedValue.Length; i++)
        {
            SmsReId = int.Parse(RecieversDataTable.Rows[i]["SmsReId"].ToString());
            SmsRecieverManager.FindByCode(SmsReId);
            if (SmsRecieverManager.Count == 1)
            {
                SmsRecieverManager[0].BeginEdit();

                switch (ReturnedValue[i])
                {
                    case "Send Successfully":
                        SuccessfullCount++;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.SendSuccessfully;
                        SmsRecieverManager[0]["IsDelivered"] = 1;
                        break;

                    case "Mobile Number is Empty":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MobileNumberIsEmpty;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("پارامتر شماره موبایل خالی است و حاوی هیچ مقداری نمی باشد.");
                        break;

                    case "Virtual Number is Empty":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.VirtualNumberIsEmpty;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("پارامتر شماره اختصاصی خالی است و حاوی هیچ مقداری نمی باشد.");
                        break;

                    case "Message Body is Invalid":
                        SmsRecieverManager[0]["DeliveredReturnValue"] = (int)TSP.DataManager.ErrorSMSRequest.MessageBodyIsInvalid;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("متن پیام کوتاه معتبر نمی باشد.");
                        break;

                    case "Message Type is Invalid":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MessageTypeIsInvalid;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("مقدار پارامتر Message Typeمعتبر نمی باشد.");
                        break;

                    case "Message is UnKnow":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MessageIsUnKnow;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("پیام معتبر نیست.");
                        break;

                    case "Mobile Array is Empty":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MobileArrayIsEmpty;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("لیست شماره موبایل ها خالی است.");
                        break;


                    case "Message is too Long":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MessageIsTooLong;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage(".متن پیام طولانی تر از حد مجاز است");
                        break;
                    case "User Not Enable":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UserNotEnable;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("نام کاربری غیر فعال است.");
                        break;

                    case "No Credit":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.NoCredit;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("اعتبار شما به اتمام رسیده است.");
                        break;

                    case "Quota Full":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.QuotaFull;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("محددیت مصرف روزانه شما به اتمام رسیده است.");
                        break;
                    case "Wrong Number":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.WrongNumber;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("شماره اختصاصی اشتباه است.");
                        break;
                    case "Username or Password Wrong":
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UsernameOrPasswordWrong;
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        ShowMessage("نام کاربری یا رمز عبور اشتباه است.");
                        break;
                }
                SmsRecieverManager[0].EndEdit();
                if (SmsRecieverManager.Save() <= 0)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count > 0)
        {
            SmsManager[0].BeginEdit();
            if (SuccessfullCount > 0)
            {
                SmsManager[0]["IsDelivered"] = 1;
                Per = (int)TSP.DataManager.ErrorSMSRequest.SendSuccessfully;
            }
            else
            {
                SmsManager[0]["IsDelivered"] = 0;
                Per = (int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS;
            }
            SmsManager[0].EndEdit();
            if (SmsManager.Save() <= 0)
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
        return Per;
    }

    #endregion

    private void FillGridByUser()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TaskIdITManagerConfirmingSMS = FindTaskId((int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS);
        int TaskIdExecutiveManagerConfirmingSMS = FindTaskId((int)TSP.DataManager.WorkFlowTask.ExecutiveManagerConfirmingSMS);
        DataTable dtTDoerITManager = TaskDoerManager.FindByDoerId(TaskIdITManagerConfirmingSMS, -1, Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType(), -1);
        DataTable dtTDoerExecutiveManager = TaskDoerManager.FindByDoerId(TaskIdExecutiveManagerConfirmingSMS, -1, Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType(), -1);
        if (dtTDoerITManager.Rows.Count > 0 || dtTDoerExecutiveManager.Rows.Count > 0)
        {
            objdsSMS.SelectParameters["SaverEmpId"].DefaultValue = "-1";
        }
        else
            objdsSMS.SelectParameters["SaverEmpId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();

        GridViewOutBox.DataBind();
    }

    private int FindTaskId(int TaskCode)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count != 1)
            return -1;
        return (Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]));
    }

    private Boolean CheckWorkFlowPermissionForConfirmAndSendSMS()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS;
        int TableType = (int)TSP.DataManager.TableCodes.SMS;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtSmsSubject.Text))
            objdsSMS.SelectParameters["SmsSubject"].DefaultValue = txtSmsSubject.Text.Trim();
        else
            objdsSMS.SelectParameters["SmsSubject"].DefaultValue = "%";

        //objdsSMS.SelectParameters["SMSId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtRecieverId.Text))
            objdsSMS.SelectParameters["RecieverId"].DefaultValue = txtRecieverId.Text.Trim();
        else
            objdsSMS.SelectParameters["RecieverId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtRecieverMobile.Text))
            objdsSMS.SelectParameters["RecieverCellPhone"].DefaultValue = txtRecieverMobile.Text.Trim();
        else
            objdsSMS.SelectParameters["RecieverCellPhone"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtSmsSubject.Text))
            objdsSMS.SelectParameters["SmsSubject"].DefaultValue = txtSmsSubject.Text.Trim();
        else
            objdsSMS.SelectParameters["SmsSubject"].DefaultValue = "%";

        if (cmbLanguage.SelectedIndex > 0)
            objdsSMS.SelectParameters["IsFarsi"].DefaultValue = cmbLanguage.SelectedItem.Value.ToString();
        else
            objdsSMS.SelectParameters["IsFarsi"].DefaultValue = "-1";
        if (cmbSMSType.SelectedIndex > 0)
            objdsSMS.SelectParameters["SmsTypeId"].DefaultValue = cmbSMSType.SelectedItem.Value.ToString();
        else
            objdsSMS.SelectParameters["SmsTypeId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtStartDate.Text))
            objdsSMS.SelectParameters["SMSStartDate"].DefaultValue = txtStartDate.Text.Trim();
        else
            objdsSMS.SelectParameters["SMSStartDate"].DefaultValue = "9999/99/99";
        if (!string.IsNullOrEmpty(txtEndDate.Text))
            objdsSMS.SelectParameters["SMSEndDate"].DefaultValue = txtEndDate.Text.Trim();
        else
            objdsSMS.SelectParameters["SMSEndDate"].DefaultValue = "9999/99/99";


        if (!string.IsNullOrEmpty(txtExpireDateFrom.Text))
            objdsSMS.SelectParameters["ExpireDateFrom"].DefaultValue = txtExpireDateFrom.Text.Trim();
        else
            objdsSMS.SelectParameters["ExpireDateFrom"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtExpireDateTo.Text))
            objdsSMS.SelectParameters["ExpireDateTo"].DefaultValue = txtExpireDateTo.Text.Trim();
        else
            objdsSMS.SelectParameters["ExpireDateTo"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtSMSDotoDateTo.Text))
            objdsSMS.SelectParameters["SMSDotoDateTo"].DefaultValue = txtSMSDotoDateTo.Text.Trim();
        else
            objdsSMS.SelectParameters["SMSDotoDateTo"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtSMSDotoDateFrom.Text))
            objdsSMS.SelectParameters["SMSDotoDateFrom"].DefaultValue = txtSMSDotoDateFrom.Text.Trim();
        else
            objdsSMS.SelectParameters["SMSDotoDateFrom"].DefaultValue = "9999/99/99";
        FillGridByUser();

    }

    #endregion
}
