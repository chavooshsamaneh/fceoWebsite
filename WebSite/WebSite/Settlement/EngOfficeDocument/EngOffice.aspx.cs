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
using System.Collections.Generic;

public partial class Settlement_EngOfficeDocument_EngOffice : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //  OdbEngOffice.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringDocumentEngOffice).ToString();
            Session["SendBackDataTable_EngOffConfStl"] = "";
            GridViewEngOffice.JSProperties["cpSelectedIndex"] = 0;
            GridViewEngOffice.JSProperties["cpIsReturn"] = 0;
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.EngOfficeConfirming).ToString();
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        SetPageFilter();
        SetGridRowIndex();

        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        script += "var txtWFFrom = document.getElementById('" + txtWFFrom.ClientID + "').value;";
        script += "var txtWFTo = document.getElementById('" + txtWFTo.ClientID + "').value;";

        script += "if (txtEndAuditor.GetText()=='' && txtWFFrom=='' && txtWFTo=='' && txtEndDateFrom=='' && txtEndDateTo=='' && txtCreateDateFrom=='' && txtCreateDateTo=='' && txtFollowCode.GetText() == '' && txtManagerfamily.GetText() == '' && txtManagerName.GetText() == '' && txtEngOffId.GetText() == '' && txtEngOffName.GetText() == '' && CmbReqType.GetSelectedIndex() == 0 &&  CmbTask.GetSelectedIndex() == 0 ) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        txtEndAuditor.SetText('');
        txtFollowCode.SetText('');
        txtManagerfamily.SetText('');        
        txtManagerName.SetText('');
        txtEngOffId.SetText('');
        txtEngOffName.SetText('');
        CmbReqType.SetSelectedIndex(0);
        CmbTask.SetSelectedIndex(0);
        document.getElementById('" + txtWFFrom.ClientID + @"').value='';
        document.getElementById('" + txtWFTo.ClientID + @"').value='';
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateTo.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        //string SearchFilterString = GenerateFilterString();

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int EngOfId = int.Parse(MeFileRow["EngOfId"].ToString());

                TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
                EngOffManager.FindByCode(EngOfId);
                if (EngOffManager.Count == 1)
                {
                    int MRsId = int.Parse(EngOffManager[0]["IsConfirm"].ToString());

                    TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
                    FileManager.FindByEngOffCode(EngOfId, -1, -1);
                    if (FileManager.Count > 0)
                    {
                        int EOfId = int.Parse(FileManager[0]["EOfId"].ToString());
                        if (CheckPermitionForEdit(EOfId))
                        {
                            Response.Redirect("EngOfficeShow.aspx?EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));// + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        //string SearchFilterString = GenerateFilterString();

        int EOfId = -1;
        int EngOfId = -1;
        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());
                    EngOfId = int.Parse(GridRequest.GetDataRow(index0)["EngOfId"].ToString());
                    Response.Redirect("EngOfficeShow.aspx?EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));// + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
            }
        }


    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        int focucedIndex = -1;
        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                focucedIndex = GridRequest.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                    DataRow row = GridRequest.GetDataRow(focucedIndex);
                    int TableId = (int)row["EOfId"];

                    Response.Redirect("../MemberDocument/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }


    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }

    /*********************************************************************************************************************************************************************/
    protected void GridViewEngOffice_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType != GridViewRowType.Data)
        //    return;
        //if (e.GetValue("IsConfirm") != null)
        //{
        //    if (e.GetValue("IsConfirm").ToString() == "0")
        //        e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        //}
        if (e.RowType != GridViewRowType.Data)
            return;
        if (!Utility.IsDBNullOrNullValue(e.GetValue("FileConfirm")))
        {
            if (e.GetValue("FileConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        if (!Utility.IsDBNullOrNullValue(e.GetValue("IsConfirm")))
        {
            if (e.GetValue("IsConfirm").ToString() == "3")
                e.Row.ForeColor = System.Drawing.Color.Brown;
        }
        if (!Utility.IsDBNullOrNullValue(e.GetValue("ReqType")))
        {
            switch ((Convert.ToInt32(e.GetValue("ReqType"))))
            {
                case (int)TSP.DataManager.EngOffFileType.Change:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                case (int)TSP.DataManager.EngOffFileType.Invalid:
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    break;
                case (int)TSP.DataManager.EngOffFileType.Reduplicate:
                    e.Row.ForeColor = System.Drawing.Color.DarkMagenta;
                    break;
                case (int)TSP.DataManager.EngOffFileType.Revival:
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    break;
                case (int)TSP.DataManager.EngOffFileType.SaveFileDocument:
                    break;
            }

        }

    }

    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["EngOfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewEngOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewEngOffice.FocusedRowIndex = Index;
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP") return;

        List<object> s = GridViewEngOffice.GetSelectedFieldValues("NotConfirmOfReId");
        if (s.Count == 0)
        {
            WFUserControl.SetMsgText("ابتدا یک یا چند ردیف را انتخاب نمایید(تیک بزنید)");
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        int WFCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        for (int i = 0; i < s.Count; i++)
        {
            if (!Utility.IsDBNullOrNullValue(s[i]))
                WFUserControl.PerformCallback(Convert.ToInt32(s[i].ToString()), TableType, WFCode, e);
        }

        #region Comment
        //if (e.Parameter == "DoNextTaskOfClosePopUP")
        //{
        //    WFUserControl.PerformCallback(-2, -2, -2, e);
        //    return;
        //}
        //int focucedIndex = -1;
        //if (GridViewEngOffice.FocusedRowIndex > -1)
        //{
        //    DataRow rowOff = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
        //    int EngOfId = (int)rowOff["EngOfId"];

        //    TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        //    if (grid != null)
        //    {
        //        focucedIndex = grid.FocusedRowIndex;
        //        if (focucedIndex > -1)
        //        {
        //            DataRow row = grid.GetDataRow(focucedIndex);
        //            int EOfId = (int)row["EOfId"];
        //            int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        //            int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        //            WFUserControl.PerformCallback(EOfId, TableType, WfCode, e);
        //        }
        //        else
        //        {
        //            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
        //        return;
        //    }
        //}
        //else
        //{
        //    WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
        //    return;
        //}

        #endregion
    }

    #endregion

    #region Methods
    //private void SelectSendBackTask(int TableType, int TableId)
    //{

    //    TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
    //    FileManager.FindByCode(TableId);
    //    if (FileManager.Count > 0)
    //    {
    //        if (FileManager[0]["IsConfirm"].ToString() != "0")
    //        {
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            PanelMain.ClientVisible = false;
    //            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //            lblInstitueWarning.Text = "وضعیت درخواست مورد نظر مشخص شده است.امکان تغییر وضعیت وجود ندارد";
    //            return;
    //        }

    //        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
    //        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
    //        if (dtWorkFlowState.Rows.Count > 0)
    //        {
    //            int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
    //            WorkFlowManager.FindByTableType(TableType, CurrentWorkFlowCode);
    //            if (WorkFlowManager.Count > 0)
    //            {

    //                //Check 
    //                int SendBackTask = WorkFlowStateManager.CalculateSendBackTask(TableType, TableId, Utility.GetCurrentUser_UserId());
    //                switch (SendBackTask)
    //                {
    //                    case -1:
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                        PanelMain.ClientVisible = false;
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
    //                        break;
    //                    case -2:
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                        PanelMain.ClientVisible = false;
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "جریان کار پرونده عضو انتخاب شده به اتمام رسیده است.";
    //                        break;
    //                    case -3:
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                        PanelMain.ClientVisible = false;
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "عملیاتی برای پرونده عضو انتخاب شده انجام نشده است.";
    //                        break;
    //                    default:
    //                        int WorkFlowId = int.Parse(WorkFlowManager[0]["WorkFlowId"].ToString());
    //                        DataTable dtSendBackTask = WorkFlowStateManager.SelectSendBackTask(SendBackTask, WorkFlowId);
    //                        if (dtSendBackTask.Rows.Count > 0)
    //                        {
    //                            Session["SendBackDataTable_OfConf"] = dtSendBackTask;
    //                            cmbSendBackTask.DataSource = dtSendBackTask;
    //                            cmbSendBackTask.ValueField = "TaskId";
    //                            cmbSendBackTask.TextField = "TaskName";
    //                            cmbSendBackTask.DataBind();
    //                            cmbSendBackTask.SelectedIndex = 0;
    //                            PanelSaveSuccessfully.ClientVisible = false;
    //                            PanelMain.ClientVisible = true;
    //                        }
    //                        break;
    //                }
    //            }
    //            else
    //            {
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                PanelMain.ClientVisible = false;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "اطلاعات جریان کار تغییر یافته است.";
    //            }
    //        }
    //        else
    //        {
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            PanelMain.ClientVisible = false;
    //            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //            lblInstitueWarning.Text = "عملیاتی برای پرونده عضو انتخاب شده انجام نشده است.";
    //        }
    //    }
    //    else
    //    {
    //        PanelSaveSuccessfully.ClientVisible = true;
    //        PanelMain.ClientVisible = false;
    //        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //        lblInstitueWarning.Text = "درخواستی انتخاب نشده است";
    //    }


    //}

    //private void SendOfficeDocToNextStep(int EOfId, int EngOfId)
    //{
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
    //    int WorkflowCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
    //    int RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
    //    int ConfirmProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
    //    int MembershipUnitConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice;

    //    int ConfirmProccessTaskId = -1;
    //    int RejectProccessTaskId = -1;

    //    WorkFlowTaskManager.FindByTaskCode(ConfirmProccessTaskCode);
    //    if (WorkFlowTaskManager.Count == 1)
    //    {
    //        ConfirmProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //    }
    //    WorkFlowTaskManager.FindByTaskCode(RejectProccessTaskCode);
    //    if (WorkFlowTaskManager.Count == 1)
    //    {
    //        RejectProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //    }
    //    WorkFlowTaskManager.FindByTaskCode(MembershipUnitConfirmingTaskCode);
    //    if (WorkFlowTaskManager.Count == 1)
    //    {
    //        MembershipUnitConfirmingTaskCode = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //    }
    //    DataTable dtSendBack = (DataTable)Session["SendBackDataTable_OfConf"];
    //    cmbSendBackTask.DataSource = dtSendBack;
    //    cmbSendBackTask.ValueField = "TaskId";
    //    cmbSendBackTask.TextField = "TaskName";
    //    cmbSendBackTask.DataBind();

    //    int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());

    //    TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
    //    TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
    //    TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();

    //    TSP.DataManager.AccountingProjectManager ProjectManager = new TSP.DataManager.AccountingProjectManager();
    //    TSP.DataManager.AccountingCostCenterManager CostCenterManager = new TSP.DataManager.AccountingCostCenterManager();
    //    ProjectManager.Fill();
    //    CostCenterManager.Fill();
    //    TSP.DataManager.AccountingDocument Document = new TSP.DataManager.AccountingDocument(TransactionManager, Utility.GetCurrentUser_AgentId(), Convert.ToInt32(ProjectManager[0]["PrjId"]), Convert.ToInt32(CostCenterManager[0]["CCId"]));

    //    TSP.DataManager.AccountingAccount Account = new TSP.DataManager.AccountingAccount(TransactionManager, Utility.GetCurrentUser_AgentId());
    //    TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
    //    TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();

    //    TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();

    //    TransactionManager.Add(MemManager);
    //    TransactionManager.Add(WorkFlowStateManager);
    //    TransactionManager.Add(EngOffManager);
    //    TransactionManager.Add(FileManager);
    //    TransactionManager.Add(SettingsManager);
    //    TransactionManager.Add(CostSettingsManager);


    //    FileManager.FindByCode(EOfId);
    //    //if (MembershipUnitConfirmingTaskCode == SelectedTaskId)
    //    //{
    //    //    if ((Convert.ToBoolean(OfReqManager[0]["Requester"]) == true) && (Utility.IsDBNullOrNullValue(OfReqManager[0]["LetterNo"])))
    //    //    {
    //    //        PanelMain.ClientVisible = false;
    //    //        PanelSaveSuccessfully.ClientVisible = true;
    //    //        lblInstitueWarning.Text = "به دلیل کامل نبودن مشخصات درخواست امکان ارسال به مرحله بعد وجود ندارد";
    //    //        return;
    //    //    }
    //    //}

    //    int AccId = -1, ParentAccId = -1, MembershipEarningsAccId = -1, MainBankAccId = -1;
    //    string Des1 = "", Des2 = "";
    //    decimal Amount = 0;

    //    #region Accounting
    //    if (Utility.IsZeroInvoiceCheck())
    //    {
    //        ParentAccId = GetParentAccId(SettingsManager);
    //        if (ParentAccId == -1)
    //        {
    //            PanelMain.ClientVisible = false;
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            lblInstitueWarning.Text = "لطفا حساب جاری اعضا را در قسمت تنظیم حسابها انتخاب نمایید.";
    //            return;
    //        }

    //        MembershipEarningsAccId = GetMembershipEarningsAccId(SettingsManager);
    //        if (MembershipEarningsAccId == -1)
    //        {
    //            PanelMain.ClientVisible = false;
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            lblInstitueWarning.Text = "لطفا حساب درآمد عضویت را در قسمت تنظیم حسابها انتخاب نمایید";
    //            return;
    //        }

    //        MainBankAccId = GetMainBankAccId(SettingsManager);
    //        if (MainBankAccId == -1)
    //        {
    //            PanelMain.ClientVisible = false;
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            lblInstitueWarning.Text = "لطفا حساب بانک اصلی را در قسمت تنظیم حسابها انتخاب نمایید";
    //            return;
    //        }

    //        Amount = GetFirstMembershipCost(CostSettingsManager);
    //        if (Amount == -1)
    //        {
    //            PanelMain.ClientVisible = false;
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            lblInstitueWarning.Text = "لطفا ورودیه عضویت را در قسمت تنظیم هزینه های دریافتی وارد نمایید";
    //            return;
    //        }
    //    }
    //    #endregion

    //    int NmcId = FindNmcId();

    //    try
    //    {
    //        TransactionManager.BeginSave();
    //        // int TableType = (int)TSP.DataManager.TableCodes.Institue;
    //        string Url = "<a href='../../Employee/Document/EngOffice/EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
    //        string MsgContent = "";
    //        int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, EOfId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId(), MsgContent, Url);
    //        switch (SendDoc)
    //        {
    //            case -6:
    //                TransactionManager.CancelSave();
    //                PanelMain.ClientVisible = false;
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                lblInstitueWarning.Text = "امکان ارسال پرونده عضو به مرحله جاری وجود ندارد.";
    //                break;
    //            case -4:
    //                TransactionManager.CancelSave();
    //                PanelMain.ClientVisible = false;
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
    //                break;
    //            case -5:
    //                TransactionManager.CancelSave();
    //                PanelMain.ClientVisible = false;
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
    //                break;
    //            default:
    //                if (SelectedTaskId == ConfirmProccessTaskId)
    //                {
    //                    if ((Utility.IsDBNullOrNullValue(FileManager[0]["serialNo"])) || (Utility.IsDBNullOrNullValue(FileManager[0]["ExpireDate"])))
    //                    {
    //                        TransactionManager.CancelSave();
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                        PanelMain.ClientVisible = false;
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "شماره سریال و تاریخ اعتبار پروانه مشخص نشده است.امکان تغییر وضعیت وجود ندارد";
    //                        return;
    //                    }

    //                    if (Utility.IsEngOfficeMemberConfirmRequestNeeded())
    //                    {
    //                        DataTable dtEngOfficeMember = MemManager.selectEngOfficeMember(EOfId, -1, -1);
    //                        for (int i = 0; i < dtEngOfficeMember.Rows.Count; i++)
    //                        {
    //                            if ((Utility.IsDBNullOrNullValue(dtEngOfficeMember.Rows[i]["IsConfirm"])) || (Utility.IsDBNullOrNullValue(dtEngOfficeMember.Rows[i]["ConfirmDate"])))
    //                            {
    //                                TransactionManager.CancelSave();
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                                PanelMain.ClientVisible = false;
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                                lblInstitueWarning.Text = "به دلیل عدم پاسخ تمامی اعضای دفتر،امکان تغییر وضعیت وجود ندارد";
    //                                return;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        DataTable dtEngOfficeMember = MemManager.selectEngOfficeMember(EOfId, -1, -1);
    //                        for (int i = 0; i < dtEngOfficeMember.Rows.Count; i++)
    //                        {
    //                            MemManager.FindEngOfficeMemberByOfmCode(Convert.ToInt32(dtEngOfficeMember.Rows[i]["OfmId"]));
    //                            MemManager[i].BeginEdit();
    //                            MemManager[i]["IsConfirm"] = 1;
    //                            MemManager[i]["ConfirmDate"] = Utility.GetDateOfToday();
    //                            MemManager[i].EndEdit();
    //                            if (MemManager.Save() > 0)
    //                                MemManager.DataTable.AcceptChanges();
    //                            else
    //                            {
    //                                TransactionManager.CancelSave();
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                                lblInstitueWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
    //                                PanelMain.ClientVisible = false;
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                                return;
    //                            }
    //                        }
    //                    }

    //                    #region InsertConfirm
    //                    FileManager[0].BeginEdit();
    //                    FileManager[0]["IsConfirm"] = 1;//تایید
    //                    FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                    FileManager[0]["AnswerDate"] = Utility.GetDateOfToday();
    //                    FileManager[0].EndEdit();

    //                    if (FileManager.Save() > 0)
    //                    {
    //                        EngOffManager.FindByCode(EngOfId);

    //                        #region SetOfficeFromRequest

    //                        EngOffManager[0].BeginEdit();
    //                        EngOffManager[0]["EOfTId"] = FileManager[0]["EOfTId"];
    //                        EngOffManager[0]["EngOffName"] = FileManager[0]["EngOffName"];
    //                        EngOffManager[0]["ParticipateLetterNo"] = FileManager[0]["ParticipateLetterNo"];
    //                        EngOffManager[0]["ParticipateLetterDate"] = FileManager[0]["ParticipateLetterDate"];
    //                        EngOffManager[0]["EngOffNo"] = FileManager[0]["EngOffNo"];
    //                        EngOffManager[0]["EngOffLoc"] = FileManager[0]["EngOffLoc"];
    //                        EngOffManager[0]["FileNo"] = FileManager[0]["FileNo"];
    //                        EngOffManager[0]["Description"] = FileManager[0]["Description"];


    //                        #endregion

    //                        if (FileManager[0]["Type"].ToString() == "0")//درخواست صدور اولیه
    //                        {
    //                            EngOffManager[0]["IsConfirm"] = 1;//تایید شده

    //                            //#region Document

    //                            //Des1 = GetDes1(EngOffManager[0], Amount);
    //                            //AccId = int.Parse(EngOffManager[0]["AccId"].ToString());
    //                            //Document.Insert(AccId, MembershipEarningsAccId, Amount, Des1, EngOfId, "", TSP.DataManager.AccountingTT.MembershipRegistration, Utility.GetCurrentUser_UserId());

    //                            //#endregion
    //                        }
    //                        else
    //                            if (FileManager[0]["Type"].ToString() == "4")//ابطال
    //                            {
    //                                EngOffManager[0]["IsConfirm"] = 3;
    //                                EngOffManager[0]["InActive"] = 1;
    //                            }

    //                        EngOffManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                        EngOffManager[0].EndEdit();
    //                        EngOffManager.Save();

    //                        TransactionManager.EndSave();
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                        lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                        GridViewEngOffice.DataBind();
    //                        PanelMain.ClientVisible = false;
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                    }
    //                    else
    //                    {
    //                        TransactionManager.CancelSave();
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                        lblInstitueWarning.Text = "خطایی در ذخیره رخ داده است.";
    //                        PanelMain.ClientVisible = false;
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                    }
    //                    #endregion


    //                }
    //                else if (SelectedTaskId == RejectProccessTaskId)
    //                {
    //                    //OfReqManager.FindByCode(OfReId);
    //                    FileManager[0].BeginEdit();
    //                    FileManager[0]["IsConfirm"] = 2;//تایید نشده;
    //                    FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                    FileManager[0]["AnswerDate"] = Utility.GetDateOfToday();

    //                    FileManager[0].EndEdit();
    //                    if (FileManager.Save() > 0)
    //                    {
    //                        if (FileManager[0]["Type"].ToString() == "0")
    //                        {
    //                            EngOffManager.FindByCode(EngOfId);
    //                            EngOffManager[0].BeginEdit();
    //                            EngOffManager[0]["IsConfirm"] = 2;//تایید نشده
    //                            EngOffManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                            EngOffManager[0].EndEdit();
    //                            EngOffManager.Save();
    //                        }


    //                        TransactionManager.EndSave();
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                        lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                        PanelMain.ClientVisible = false;
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                    }
    //                    else
    //                    {
    //                        TransactionManager.CancelSave();
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                        lblInstitueWarning.Text = "خطایی در ذخیره رخ داده است.";
    //                        PanelMain.ClientVisible = false;
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                    }
    //                }
    //                else if (SelectedTaskId == RejectProccessTaskId)
    //                {

    //                }
    //                else
    //                {
    //                    TransactionManager.EndSave();
    //                    lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                    lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                    PanelMain.ClientVisible = false;
    //                    PanelSaveSuccessfully.ClientVisible = true;
    //                }
    //                break;
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        TransactionManager.CancelSave();
    //        Utility.SaveWebsiteError(err);
    //        lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //        lblInstitueWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
    //        PanelMain.ClientVisible = false;
    //        PanelSaveSuccessfully.ClientVisible = true;
    //    }
    //}

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;

        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }

    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringDocumentEngOffice;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;

                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
                    return true;

            }

        }
        return false;
    }

    /*********************************************************************************************************************************************************************/
    private int GetParentAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembersCurrentAccountOffice.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetMembershipEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembershipEarnings.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetDes1(DataRow Of, decimal Amount)
    {
        string Des = "جهت حق عضویت جدید شرکت " + " " + Of["OfName"].ToString() + " " + "به مبلغ" + " " + Amount.ToString("#,#") + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    private string GetDes2(DataRow Of)
    {
        Utility.Date Date = new Utility.Date();
        string Des = "واریز حق عضویت جدید شرکت " + Of["OfName"].ToString() + " " + "جهت سال" + " " + Date.Year.ToString();
        return Des;
    }

    #region FilteringMethod
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))//&& !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                //string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                //if (!string.IsNullOrEmpty(SrchFlt))
                //    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewEngOffice.FilterExpression = GrdFlt;
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

                    GridViewEngOffice.DataBind();
                    Index = GridViewEngOffice.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewEngOffice.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewEngOffice.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewEngOffice.JSProperties["cpSelectedIndex"] = Index;
                        GridViewEngOffice.DetailRows.ExpandRow(Index);
                        GridViewEngOffice.FocusedRowIndex = Index;
                        GridViewEngOffice.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    #endregion

    void Search()
    {
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text) || !string.IsNullOrEmpty(txtEndDateTo.Text) || !string.IsNullOrEmpty(txtFollowCode.Text)
            || !string.IsNullOrEmpty(txtManagerName.Text) || !string.IsNullOrEmpty(txtManagerfamily.Text) || !string.IsNullOrEmpty(txtEngOffName.Text)
            || !string.IsNullOrEmpty(txtEngOffId.Text) || CmbReqType.SelectedIndex > 0 || CmbTask.SelectedIndex > 0 || !string.IsNullOrEmpty(txtCreateDateFrom.Text) || !string.IsNullOrEmpty(txtCreateDateTo.Text)
            || !string.IsNullOrEmpty(txtEndAuditor.Text))
            OdbEngOffice.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringDocumentEngOffice).ToString();

        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            OdbEngOffice.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbEngOffice.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            OdbEngOffice.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            OdbEngOffice.SelectParameters["EndDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            OdbEngOffice.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else
            OdbEngOffice.SelectParameters["FollowCode"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtManagerName.Text))
            OdbEngOffice.SelectParameters["ManagerName"].DefaultValue = txtManagerName.Text;
        else
            OdbEngOffice.SelectParameters["ManagerName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtManagerfamily.Text))
            OdbEngOffice.SelectParameters["Managerfamily"].DefaultValue = txtManagerfamily.Text;
        else
            OdbEngOffice.SelectParameters["Managerfamily"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtEngOffName.Text))
            OdbEngOffice.SelectParameters["EngOffName"].DefaultValue = txtEngOffName.Text;
        else
            OdbEngOffice.SelectParameters["EngOffName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtEngOffId.Text))
            OdbEngOffice.SelectParameters["EngOfId"].DefaultValue = txtEngOffId.Text;
        else
            OdbEngOffice.SelectParameters["EngOfId"].DefaultValue = "-1";

        if (CmbReqType.SelectedIndex > 0)
            OdbEngOffice.SelectParameters["ReqType"].DefaultValue = CmbReqType.Value.ToString();
        else
            OdbEngOffice.SelectParameters["ReqType"].DefaultValue = "-1";

        if (CmbTask.SelectedIndex > 0)
            OdbEngOffice.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbEngOffice.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            OdbEngOffice.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else
            OdbEngOffice.SelectParameters["CreateDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            OdbEngOffice.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else
            OdbEngOffice.SelectParameters["CreateDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtWFTo.Text))
            OdbEngOffice.SelectParameters["WFDateTo"].DefaultValue = txtWFTo.Text;
        else
            OdbEngOffice.SelectParameters["WFDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtWFFrom.Text))
            OdbEngOffice.SelectParameters["WFDateFrom"].DefaultValue = txtWFFrom.Text;
        else
            OdbEngOffice.SelectParameters["WFDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtEndAuditor.Text))
            OdbEngOffice.SelectParameters["WFDoerName"].DefaultValue = txtEndAuditor.Text;
        else
            OdbEngOffice.SelectParameters["WFDoerName"].DefaultValue = "%";

        GridViewEngOffice.DataBind();
    }
    #endregion
}
