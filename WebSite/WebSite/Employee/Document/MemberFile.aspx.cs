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

public partial class Employee_Document_MemberFile : System.Web.UI.Page
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
            SetHelpAddress();
            LoadWfHelpPrint();
            comboMjParent.DataBind();
            comboMjParent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد"));
            comboMjParent.SelectedIndex = 0;
            comboGrade.DataBind();
            comboGrade.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد"));
            comboGrade.SelectedIndex = 0;
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming).ToString();

            //GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
            Session["SendBackDataTable_EmpMeFile"] = "";
            #region Permission and viewstates
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewMemberFile.Visible = per.CanView;
            btnPrint.Enabled = btnprint2.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;

            this.ViewState["BtnRequset"] =
            btnReDuplicate.Enabled = btnReDuplicate2.Enabled =
            btnRevival.Enabled = btnRevival2.Enabled =
            btnUpGrade.Enabled = btnUpGrade2.Enabled =
            btnChange.Enabled = btnChange2.Enabled =
            btnQualification.Enabled = btnQualification2.Enabled =
            btnReissues.Enabled = btnReissues2.Enabled = btnTransferedMemberRequest.Enabled = btnTransferedMemberRequest2.Enabled = CheckWorkFlowPermissionForChangeReq();

            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnRequset"] = btnReDuplicate.Enabled;
            #endregion
            GridViewMemberFile.JSProperties["cpSelectedIndex"] = 0;
            GridViewMemberFile.JSProperties["cpIsReturn"] = 0;
        }

        SetPageFilter();
        SetGridRowIndex();
        ////Search();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnprint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnRequset"] != null)
            btnReDuplicate.Enabled = btnReDuplicate2.Enabled =
            btnRevival.Enabled = btnRevival2.Enabled =
            btnUpGrade.Enabled = btnUpGrade2.Enabled =
            btnChange.Enabled = btnChange2.Enabled =
            btnQualification.Enabled = btnQualification2.Enabled =
            btnReissues.Enabled = btnReissues2.Enabled = btnTransferedMemberRequest.Enabled = btnTransferedMemberRequest2.Enabled = (bool)this.ViewState["BtnRequset"];

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtDateEndAuditor = document.getElementById('" + txtDateEndAuditor.ClientID + "').value;";
        script += "var txtDateEndAuditorTo = document.getElementById('" + txtDateEndAuditorTo.ClientID + "').value;";

        script += "var LastRequestCreateDateFrom = document.getElementById('" + txtLastRequestCreateDateFrom.ClientID + "').value;";
        script += "var LastRequestCreateDateTo = document.getElementById('" + txtLastRequestCreateDateTo.ClientID + "').value;";
        script += "if ( txtDateEndAuditor=='' && txtDateEndAuditorTo=='' && txtEndDateTo=='' && txtEndDateFrom=='' && txtMeId.GetText() == '' && txtMFNo.GetText() == '' && txtFamily.GetText() == '' && txtName.GetText() == '' && txtFollowCode.GetText() == '' && CmbReqType.GetSelectedIndex() == 0  && CmbLastReqType.GetSelectedIndex() == 0 &&  CmbPaymentType.GetSelectedIndex() == 0 && CmbPaymentStatus.GetSelectedIndex() == 0 && CmbFaultMemberRegister.GetSelectedIndex() == 0 && CmbFaultDocument.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == -1 && LastRequestCreateDateFrom=='' && txtDateEndAuditorTo=='' && comboGrade.GetSelectedIndex() == 0 && comboMjParent.GetSelectedIndex() == 0 && ComboRequesterType.GetSelectedIndex() == 0 ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); txtName.SetText(''); txtFamily.SetText('');   txtFollowCode.SetText(''); txtEndAuditor.SetText('');
                    CmbReqType.SetSelectedIndex(0);CmbLastReqType.SetSelectedIndex(0); CmbPaymentType.SetSelectedIndex(0); CmbPaymentStatus.SetSelectedIndex(0); CmbFaultMemberRegister.SetSelectedIndex(0); CmbFaultDocument.SetSelectedIndex(0); CmbTask.SetSelectedIndex(-1); comboGrade.SetSelectedIndex(0); comboMjParent.SetSelectedIndex(0); ComboRequesterType.SetSelectedIndex(0);
                    document.getElementById('" + txtEndDateFrom.ClientID + "').value = ''; document.getElementById('" + txtEndDateTo.ClientID + "').value = ''; document.getElementById('" + txtDateEndAuditorTo.ClientID + "').value = ''; document.getElementById('" + txtLastRequestCreateDateFrom.ClientID + "').value = ''; document.getElementById('" + txtLastRequestCreateDateTo.ClientID + "').value = '';";
        script += "document.getElementById('" + txtDateEndAuditor.ClientID + "').value = '';}</SCRIPT>";


        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void GridViewMemberFile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Search":
                    //   Search();
                    //   GridViewMemberFile.DataBind();
                    break;
                default:
                    GridViewMemberFile.DataBind();
                    GridViewMemberFile.DetailRows.ExpandRow(GridViewMemberFile.FocusedRowIndex);
                    break;
            }
        }
    }

    protected void GridViewMemberFile_DataBinding(object sender, EventArgs e)
    {
        (sender as ASPxGridView).DataSource = GetDataSource();
    }

    protected void GridViewMemberFile_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("LastConfirm") != null)
        {
            if (e.GetValue("LastConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        SetGridRowColor(e);
    }

    //protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    //{

    //    switch (e.Column.FieldName)
    //    {
    //        case "MFNo":
    //        case "RegDate":
    //        case "LastExpireDate":
    //        case "WFDate":
    //            e.Editor.Style["direction"] = "ltr";
    //            break;
    //    }
    //}

    //protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    switch (e.DataColumn.FieldName)
    //    {
    //        case "LastExpireDate":
    //        case "WFDate":
    //        case "RegDate":
    //        case "MFNo":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //    }

    //    //if (e.DataColumn.FieldName == "TaskId")
    //    //{
    //    //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
    //    //    if (btnWFState != null)
    //    //    {
    //    //        if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //    //        {
    //    //            btnWFState.ToolTip = "تعریف نشده";
    //    //            btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //    //            return;
    //    //        }
    //    //        if (Utility.IsDBNullOrNullValue(e.GetValue("TaskName")))
    //    //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();

    //    //    }
    //    //}

    //    //if (e.DataColumn.Name == "ExpireState")
    //    //{
    //    //    DevExpress.Web.ASPxImage ImgExpireState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["ExpireState"], "ImgExpireState");
    //    //    if (ImgExpireState != null)
    //    //    {
    //    //        if (Utility.IsDBNullOrNullValue(e.GetValue("LastExpireDate")))
    //    //        {
    //    //            ImgExpireState.ToolTip = "نامشخص";
    //    //            ImgExpireState.ImageUrl = "~/Images/WFUnNounState.png";
    //    //            return;
    //    //        }
    //    //        else if (String.Compare(e.GetValue("LastExpireDate").ToString(), Utility.GetDateOfToday()) >= 0)
    //    //        {
    //    //            ImgExpireState.ToolTip = "دارای اعتبار";
    //    //            ImgExpireState.ImageUrl = "~/Images/CertificateValid.png";
    //    //        }
    //    //        else
    //    //        {
    //    //            ImgExpireState.ToolTip = "پایان اعتبار";
    //    //            ImgExpireState.ImageUrl = "~/Images/CertificateExpired.png";
    //    //        }
    //    //    }
    //    //}
    //}

    //protected void GridViewMemberFile_PageIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
    //}
    //**********************************************************************************
    protected void GridViewMeFileHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {

        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("MeId")))
            Session["MeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MeId");
        int Index = GridViewMemberFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewMemberFile.FocusedRowIndex = Index;
        // GridViewMemberFile.DataBind();
    }

    //protected void GridViewMeFileHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    //{
    //    switch (e.Column.FieldName)
    //    {
    //        case "MailNo":
    //            e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
    //            break;
    //        case "MFNo":
    //        case "MailDate":
    //        case "RegDate":
    //        case "ExpireDate":
    //            e.Editor.Style["direction"] = "ltr";
    //            break;
    //    }

    //}

    //protected void GridViewMeFileHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    switch (e.DataColumn.FieldName)
    //    {
    //        case "MFNo":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //        case "MailNo":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //        case "MailDate":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //        case "RegDate":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //        case "ExpireDate":
    //            e.Cell.Style["direction"] = "ltr";
    //            break;
    //    }

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
    //        if (GridViewMeFileHistory == null)
    //            return;
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMeFileHistory.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMeFileHistory.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            } if (Utility.IsDBNullOrNullValue(e.GetValue("TaskName")))
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //            //if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            //{
    //            //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //            //    btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            //}
    //            //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            //{
    //            //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //            //    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            //}
    //            //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            //{
    //            //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //            //    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            //}
    //            //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            //{
    //            //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //            //    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            //}
    //            //else
    //            //{
    //            //}
    //        }
    //    }
    //}

    //*************************Callback's Events******************************************

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        //if (GridViewMemberFile.FocusedRowIndex > -1)
        //{
        //    TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
        //    if (GridViewMeFileHistory != null)
        //    {
        //        if (GridViewMeFileHistory.FocusedRowIndex > -1)
        //        {

        //            DataRow row = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
        //            if (row == null || Utility.IsDBNullOrNullValue(row["MfId"]))
        //                return;
        //            int MfId = (int)row["MfId"];
        //            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //            int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        //            WFUserControl.PerformCallback(MfId, TableType, WFCode, e);
        //        }
        //    }
        //    else
        //    {
        //        WFUserControl.SetMsgText("برای ارسال پرونده به مرحله بعد ابتدا یک درخواست را انتخاب نمائید");
        //        WFUserControl.PerformCallback(-2, -2, -2, e);

        //    }
        //}
        //else
        //{
        //    WFUserControl.SetMsgText("ردیف مورد نظر را انتخاب نمایید.");
        //    WFUserControl.PerformCallback(-2, -2, -2, e);
        //}


        List<object> s = GridViewMemberFile.GetSelectedFieldValues("MaxMfId");
        if (s.Count == 0)
        {
            WFUserControl.SetMsgText("ابتدا یک یا چند ردیف را انتخاب نمایید(تیک بزنید)");
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;

        for (int i = 0; i < s.Count; i++)
        {
            if (!Utility.IsDBNullOrNullValue(s[i]))
                WFUserControl.PerformCallback(Convert.ToInt32(s[i].ToString()), TableType, WFCode, e);
        }

    }

    protected void CallbackMeDoc_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (IsPageRefresh)
        {
            return;
        }
        int MfId = -2;
        int MeId = -2;
        DataRow FocusedRow;
        CallbackMeDoc.JSProperties["cpDoPrintAll"] = 0;
        CallbackMeDoc.JSProperties["cpPrintAllPath"] = "";
        switch (e.Parameter)
        {
            case "New":
                BtnNew_Click();
                break;
            case "Edit":
                btnEdit_Click();
                break;
            case "View":
                btnView_Click();
                break;
            case "Delete":
                btnDelete_Click(this, new EventArgs());
                break;
            case "Upgrade":
                btnUpGrade_Click();
                break;
            case "ReDuplicate":
                btnReDuplicate_Click();
                break;
            case "Revival":
                btnRevival_Click();
                break;
            case "Change":
                btnChange_Click();
                break;
            case "Qualification":
                btnQualification_Click();
                break;
            case "Tracing":
                btnTracing_Click(this, new EventArgs());
                break;
            case "Reissues":
                btnReissues_Click();
                break;
            case "TransferedMemberReq":
                btnTransferedMemberRequest_Click();
                break;
            case "Print":
                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("WFState");
                DeletedColumnsName.Add("ExpireState");

                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewMemberFile.Columns;
                Session["DataSource"] = ObjdsMemberFileMainRequest;
                Session["Title"] = "مدیریت پروانه اشتغال به کار";

                GridViewMemberFile.DetailRows.CollapseAllRows();
                CallbackMeDoc.JSProperties["cpDoPrint"] = 1;
                break;
            case "ReportAll":
                if (GridViewMemberFile.FocusedRowIndex < 0)
                {
                    ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                    return;
                }
                TSP.WebControls.CustomAspxDevGridView GridViewMeFileReportAll = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileReportAll == null)
                {
                    ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                    return;
                }

                if (GridViewMeFileReportAll.FocusedRowIndex > -1)
                {
                    FocusedRow = GridViewMeFileReportAll.GetDataRow(GridViewMeFileReportAll.FocusedRowIndex);
                    if (FocusedRow == null)
                    {
                        ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                        return;
                    }

                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MeId"]))
                        MeId = int.Parse(FocusedRow["MeId"].ToString());
                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MfId"]))
                        MfId = Convert.ToInt32(FocusedRow["MfId"]);

                    CallbackMeDoc.JSProperties["cpDoPrintAll"] = 1;
                    CallbackMeDoc.JSProperties["cpPrintAllPath"] = "../../ReportForms/DocMemberFileReport.aspx?MeId="
                        + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());
                }

                break;
            case "ReportBreif":
                if (GridViewMemberFile.FocusedRowIndex < 0)
                {
                    ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                    return;
                }

                TSP.WebControls.CustomAspxDevGridView GridViewMeFileReportBreif = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileReportBreif != null)
                {
                    if (GridViewMeFileReportBreif.FocusedRowIndex < 0)
                    {
                        ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                        return;
                    }
                    FocusedRow = GridViewMeFileReportBreif.GetDataRow(GridViewMeFileReportBreif.FocusedRowIndex);
                    if (FocusedRow == null)
                    {
                        ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                        return;
                    }

                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MeId"]))
                        MeId = int.Parse(FocusedRow["MeId"].ToString());
                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MfId"]))
                        MfId = Convert.ToInt32(FocusedRow["MfId"]);

                    CallbackMeDoc.JSProperties["cpDoPrintAll"] = 1;
                    CallbackMeDoc.JSProperties["cpPrintAllPath"] = "../../ReportForms/DocMemberFileReport.aspx?MeId="
                        + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "&IsBreif=" + Utility.EncryptQS("1");
                }

                break;
            case "PrePrint":
                if (GridViewMemberFile.FocusedRowIndex < 0)
                {
                    ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                    return;
                }
                TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileHistory == null)
                {

                    ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
                    return;
                }
                if (GridViewMeFileHistory.FocusedRowIndex < 0)
                {

                    ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
                    return;
                }
                FocusedRow = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
                if (FocusedRow != null)
                {

                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MeId"]))
                        MeId = int.Parse(FocusedRow["MeId"].ToString());
                    if (!Utility.IsDBNullOrNullValue(FocusedRow["MfId"]))
                        MfId = Convert.ToInt32(FocusedRow["MfId"]);
                    CallbackMeDoc.JSProperties["cpDoPrePrint"] = 1;
                    CallbackMeDoc.JSProperties["cpPrePrintPath"] = "../../ReportForms/ReportMemberFilePrePrint.aspx?MeId="
                        + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());
                }
                break;

        }
    }

    #region btn Click
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }

    protected void BtnNew_Click()
    {
        NextPage("New");
    }

    protected void btnView_Click()
    {
        NextPage("View");
    }

    protected void btnEdit_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1)
        {
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            return;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
            return;
        }
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
            return;
        }
        int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        if (CheckPermitionForEdit(MfId))
        {
            NextPage("Edit");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.";
        }
    }

    //*****ارتقاء پایه
    protected void btnUpGrade_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        int MfId = CheckRequestCondition(MeId);
        if (MfId < 0)
            return;
        NextPage("UpGrade");

    }

    //*****المثنی
    protected void btnReDuplicate_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        int MfId = CheckRequestCondition(MeId);
        if (MfId < 0) return;
        NextPage("ReDuplicate");
    }

    //****تمدید
    protected void btnRevival_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());
        int MfId = CheckRequestCondition(MeId);
        if (MfId < 0) return;
        if (Revival(MfId, MeId))
            NextPage("Revival");
    }

    //***تغییرات
    protected void btnChange_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        int MfId = CheckRequestCondition(MeId);
        if (MfId < 0) return;
        NextPage("Change");
    }

    //***درج صلاحیت جدید
    protected void btnQualification_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());
        int MfId = CheckRequestCondition(MeId);
        if (MfId < 0) return;
        NextPage("Qualification");
    }

    //***صدور مجدد
    protected void btnReissues_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            return;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
        {
            ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("امکان ثبت درخواست در پروانه اشتغال به کار برای این عضو وجود ندارد.این عضو درواحد عضویت درخواست درجریان دارد.چنانچه پس از 48 ساعت درخواست وی تایید نشد،با واحد عضویت سازمان تماس حاصل نمایید. ");
            return;
        }


        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
        if (DocMemberFileManager.Count > 0)
        {
            ShowMessage("بدلیل وجود درخواست درجریان برای پروانه انتخاب شده،امکان ثبت درخواست جدید وجود ندارد.");
            return;
        }
        int MfId = -1;
        DocMemberFileManager.FindByDocumentType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        DocMemberFileManager.CurrentFilter = "IsConfirm<>2 and " + "Type<>" + ((int)TSP.DataManager.DocumentOfMemberRequestType.New).ToString() + " and " + "Type<>" + ((int)TSP.DataManager.DocumentOfMemberRequestType.Change).ToString()
            + " and " + "Type<>" + ((int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument).ToString() + " and " + "Type<>" + ((int)TSP.DataManager.DocumentOfMemberRequestType.Transfer).ToString()
             + " and " + "Type<>" + ((int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival).ToString();
        if (DocMemberFileManager.Count > 0)
        {
            ShowMessage("امکان درخواست صدور مجدد برای پروانه های دارای ارتقاء و یا تمدید وجود ندارد.");
            return;
        }
        else
        {
            DocMemberFileManager.CurrentFilter = "";
            DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (DocMemberFileManager.Count == 1)
                MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
            else
            {
                DocMemberFileManager.SelectLastVersion(MeId, 0, -1);
                if (DocMemberFileManager.Count == 1)
                    MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                    return;
                }
            }
        }

        if (CheckpermisionForNewRequest())
            NextPage("Reissues");
    }
    //***تخصیص شماره پروانه اعضای انتقالی
    protected void btnTransferedMemberRequest_Click()
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());
        if (TransferedMemberRequest(MeId) > 0)
            NextPage("TransferedMemberRequest");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MeId = int.Parse(MeFileRow["MeId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
                if (dtDocMeFile.Rows.Count > 0)
                {
                    int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    if (CheckPermitionForDelete(MfId))
                    {
                        DeleteRequest(MfId, MeId);
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای شما امکان لغو درخواست وجود ندارد.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان لغو درخواست وجود ندارد.";
                }
            }
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"].ToString());
            string GridFilterString = GridViewMemberFile.FilterExpression;
            string SearchFilterString = GenerateFilterString();
            TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory != null)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                DataRow DocMeFileRow = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
                int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;


                String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                    "&PostId=" + Utility.EncryptQS(PostId.ToString());

                if (IsCallback)
                {
                    ASPxWebControl.RedirectOnCallback("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                }
                else
                {
                    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        //GridViewMemberFile.Columns["Status"].Visible = false;
        GridViewExporterDocMe.FileName = "MemberDocument";

        GridViewExporterDocMe.WriteXlsToResponse(true);
        //GridViewMemberFile.Columns["Status"].Visible = true;
    }

    private int CheckRequestCondition(int MeId)
    {

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("امکان ثبت درخواست در پروانه اشتغال به کار برای این عضو وجود ندارد.این عضو درواحد عضویت درخواست درجریان دارد.چنانچه پس از 48 ساعت درخواست وی تایید نشد،با واحد عضویت سازمان تماس حاصل نمایید. ");
            return -1;
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            return -1;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
        {
            if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در جریان می باشد.");
                return -1;
            }
            else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Cancel)
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی لغو شده می باشد.");
                return -1;
            }
            else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember)
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی قطع شده می باشد.");
                return -1;
            }
            else
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی تایید شده نمی باشد.");
                return -1;
            }
        }


        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
        if (DocMemberFileManager.Count > 0)
        {
            ShowMessage("بدلیل وجود درخواست درجریان برای پروانه انتخاب شده،امکان ثبت درخواست جدید وجود ندارد.");
            return -1;
        }

        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
            return -1;
        }
        if (CheckpermisionForNewRequest())
            return (int)dtDocMeFile.Rows[0]["MfId"];
        else
            return -1;

    }
    #endregion

    #region Check Regqust

    private Boolean Revival(int MfId, int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        Boolean CanRevival = true;
        try
        {
            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (Convert.ToBoolean(DocMemberFileManager[0]["IsConfirm"]) == true)
                {
                    string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                    Utility.Date objDate = new Utility.Date(CrtEndDate);
                    string LastMonth = objDate.AddMonths(-2);
                    string Today = Utility.GetDateOfToday();
                    int IsDocExp = string.Compare(Today, LastMonth);
                    if (IsDocExp <= 0)
                    {
                        ShowMessage("تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.");
                        CanRevival = false;
                    }
                }
                else
                {
                    ShowMessage("امکان تمدید برای پروانه تایید نشده وجود ندارد.");
                    CanRevival = false;
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                CanRevival = false;
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
            CanRevival = false;
        }
        return CanRevival;
    }

    private int TransferedMemberRequest(int MeId)
    {

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("امکان ثبت درخواست در پروانه اشتغال به کار برای این عضو وجود ندارد.این عضو درواحد عضویت درخواست درجریان دارد.چنانچه پس از 48 ساعت درخواست وی تایید نشد،با واحد عضویت سازمان تماس حاصل نمایید. ");
            return -1;
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            return -1;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
        {
            ShowMessage("تنها برای اعضای انتقال یافته به سایر استان ها امکان ثبت این نوع درخواست وجود دارد.");
            return -1;
        }

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
        if (DocMemberFileManager.Count > 0)
        {
            ShowMessage("بدلیل وجود درخواست درجریان برای پروانه انتخاب شده،امکان ثبت درخواست جدید وجود ندارد.");
            return -1;
        }

        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
            return -1;
        }
        if (CheckpermisionForNewRequest())
            return (int)dtDocMeFile.Rows[0]["MfId"];
        else
            return -1;
    }
    #endregion

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int MfId = -1;
        int MeId = -1;
        int focucedIndex = -1;
        int PostId = -1;
        string SearchFilterString = GenerateFilterString();
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            PostId = (int)GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"];
        }
        if (Mode == "View")
        {
            if (GridViewMemberFile.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileHistory != null)
                {
                    focucedIndex = GridViewMeFileHistory.FocusedRowIndex;
                    if (focucedIndex > -1)
                    {
                        DataRow row = GridViewMeFileHistory.GetDataRow(focucedIndex);
                        MfId = (int)row["MfId"];
                        MeId = (int)row["MeId"];
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
        }
        else
        {
            focucedIndex = GridViewMemberFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                MeId = (int)row["MeId"];
                if (Mode != "New" && Mode != "View" && Mode != "Edit")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DocMemberFileManager.FindByDocumentType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
                    if (DocMemberFileManager.Count == 1)
                    {
                        if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) != 2)//درخواست اولیه تایید نشده
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                            }
                        }
                        else
                            MfId = MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
                    }
                    else
                    {
                        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                        if (dtDocMeFile.Rows.Count <= 0)
                        {
                            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
                            return;
                        }
                        MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }
                if (Mode == "Edit")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }

            }
        }

        if (MfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            string GridFilterString = GridViewMemberFile.FilterExpression;
            if (Mode == "New")
            {
                MeId = -1;
                MfId = -1;
                if (IsCallback)
                {
                    ASPxWebControl.RedirectOnCallback("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                }
                else
                {
                    Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                }
            }
            else
            {
                if (IsCallback)
                {
                    ASPxWebControl.RedirectOnCallback("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                }
                else
                {
                    Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                }
            }
        }
    }

    //private int FindNmcId()
    //{
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int NmcId = -1;

    //    NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
    //    if (NmcId > 0)
    //    {
    //        return NmcId;
    //    }
    //    else
    //    {
    //        DivReport.Visible = true;
    //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //        return (-1);
    //    }
    //}


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
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }


    #region WF
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());

            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo
                || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument
                || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument)
            {
                int PermissionSaveInfo = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
                int PermissionDocumentUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                int PermissionDocumentUnitResp = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());

                if (PermissionSaveInfo > 0 || PermissionDocumentUnit > 0 || PermissionDocumentUnitResp > 0)
                    return true;
            }
        }
        return false;
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {

        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming
         , (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);

        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //WorkFlowStateManager.ClearBeforeFill = true;
        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //int WfCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        //DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //if (dtState.Rows.Count > 0)// == 1)
        //{
        //    dtState.DefaultView.RowFilter = "StateType=" + ((int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep).ToString();
        //    if (dtState.DefaultView.Count == 1)
        //    {
        //        int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //        //int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //        //int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //        //int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //        //int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        //        //int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        //        if (int.Parse(dtState.Rows[0]["TaskCode"].ToString()) == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo)
        //        {
        //            DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        //            if (dtWorkFlowState.Rows.Count > 0)
        //            {
        //                int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        //                int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
        //                //int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
        //                if (FirstTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo)
        //                {
        //                    if (Utility.GetCurrentUser_MeId() == Convert.ToInt32(dtState.Rows[0]["EmpId"]) || FirstNmcId == FindNmcId(Convert.ToInt32(dtWorkFlowState.Rows[0]["TaskId"])))
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //return false;
    }

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);// (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            ShowMessage("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }
    #endregion

    private void DeleteRequest(int MfId, int MeId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocOffJobHistoryQualityManager DocOffJobHistoryQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        TransactionManager.Add(RequestInActivesManager);
        TransactionManager.Add(DocMemberExamDetailManager);
        TransactionManager.Add(DocMemberExamManager);
        TransactionManager.Add(DocMemberFileDetailManager);
        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(DocMemberFileJobConfirmationManager);
        try
        {
            TransactionManager.BeginSave();

            #region DocDetails
            DataTable dtDocDetail = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
            if (dtDocDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dtDocDetail.Rows.Count; i++)
                {
                    int MfdId = (int)dtDocDetail.Rows[i]["MfdId"];
                    DocMemberFileDetailManager.FindByCode(MfdId);
                    if (DocMemberFileDetailManager.Count == 1)
                    {
                        if (Convert.ToInt32(DocMemberFileDetailManager[0]["MfId"]) == MfId)
                        {


                            DocMemberFileDetailManager[0].Delete();
                            if (DocMemberFileDetailManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }
            #endregion

            #region Delete Majors
            DataTable dtDocMajor = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, -1);
            if (dtDocMajor.Rows.Count > 0)
            {
                for (int i = 0; i < dtDocMajor.Rows.Count; i++)
                {
                    int MFMjId = (int)dtDocMajor.Rows[i]["MFMjId"];
                    DocMemberFileMajorManager.FindByCode(MFMjId);
                    if (DocMemberFileMajorManager.Count == 1)
                    {
                        if (MfId == Convert.ToInt32(DocMemberFileMajorManager[0]["MFId"]))
                        {
                            DocMemberFileMajorManager[0].Delete();
                            if (DocMemberFileMajorManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }
            #endregion

            #region DocMemberFileJobConfirmationManager
            DocMemberFileJobConfirmationManager.FindByMfIdForDelete(MfId);
            int cntJobConfirm = DocMemberFileJobConfirmationManager.Count;
            for (int i = 0; i < cntJobConfirm; i++)
            {
                DocMemberFileJobConfirmationManager[0].Delete();
                if (DocMemberFileJobConfirmationManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
                DocMemberFileJobConfirmationManager.DataTable.AcceptChanges();
            }
            #endregion

            #region DeleteExams
            DataTable dtMeExam = DocMemberExamManager.SelectDocMemberFileExamForDelete(MfId);
            if (dtMeExam.Rows.Count > 0)
            {
                for (int i = 0; i < dtMeExam.Rows.Count; i++)
                {
                    int MExmId = (int)dtMeExam.Rows[i]["MExmId"];
                    DocMemberExamDetailManager.SelectByExam(MExmId);
                    int cntDetail = DocMemberExamDetailManager.Count;
                    for (int j = 0; j < cntDetail; j++)
                    {
                        DocMemberExamDetailManager[0].Delete();
                        if (DocMemberExamDetailManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                            return;
                        }
                        DocMemberExamDetailManager.DataTable.AcceptChanges();
                    }

                    DocMemberExamManager.FindByCode(MExmId);
                    if (DocMemberExamManager.Count == 1)
                    {
                        DocMemberExamManager[0].Delete();
                        if (DocMemberExamManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                            return;
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }

            #endregion

            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming, MfId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[0].Delete();
                    if (WorkFlowStateManager.Save() <= 0)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                    WorkFlowStateManager.DataTable.AcceptChanges();
                }
            }

            #endregion

            #region Delete MemeberFile
            Boolean CheckHasInActiveDoc = false;
            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.New
                    || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival
                    || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer)
                    CheckHasInActiveDoc = true;

                DocMemberFileManager[0].Delete();
                if (DocMemberFileManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                return;
            }
            if (CheckHasInActiveDoc)
            {
                DocMemberFileManager.DataTable.AcceptChanges();
                DocMemberFileManager.FindByMeId(MeId);
                if (DocMemberFileManager.Count > 0
                  && Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.New
                    && Convert.ToInt32(DocMemberFileManager[0]["InActive"]) == 1)
                {
                    DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
                    DocMemberFileManager[DocMemberFileManager.Count - 1]["InActive"] = 0;
                    DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
                    DocMemberFileManager.Save();
                }
            }
            #endregion

            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            #region Delete Fish

            AccountingManager.FindByTableTypeId(MfId, TableType);
            int cnt = AccountingManager.Count;
            for (int i = 0; i < cnt; i++)
            {
                AccountingDetailManager.FindByAccountingId(Convert.ToInt32(AccountingManager[0]["AccountingId"]));
                int cntAccDetail = AccountingDetailManager.Count;
                for (int j = 0; j < cntAccDetail; j++)
                {
                    AccountingDetailManager[0].Delete();
                    AccountingDetailManager.Save();
                    AccountingDetailManager.DataTable.AcceptChanges();
                }
                AccountingManager[0].Delete();
                AccountingManager.Save();
                AccountingManager.DataTable.AcceptChanges();
            }
            #endregion

            #region DeleteInActives
            RequestInActivesManager.FindByReqId(MfId, TableType);
            int cntInActive = RequestInActivesManager.Count;
            for (int i = 0; i < cntInActive; i++)
            {
                RequestInActivesManager[0].Delete();
                RequestInActivesManager.Save();
                RequestInActivesManager.DataTable.AcceptChanges();
            }
            #endregion
            TransactionManager.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لغو در خواست انجام گرفت.";

            GridViewMemberFile.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
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
                ShowMessage("خطایی در حذف انجام گرفته است.");
            }
        }
        else
        {
            ShowMessage("خطایی در حذف انجام گرفته است.");
        }
    }

    #region Set Grid Index
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMemberFile.FilterExpression = GrdFlt;
                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
            }
        }

    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjdsMemberFileMainRequest.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "FirstName":
                        txtName.Text = Value;
                        break;
                    case "LastName":
                        txtFamily.Text = Value;
                        break;
                    case "FollowCode":
                        txtFollowCode.Text = Value;
                        break;
                    //case "MjId":
                    //    drdMajor.DataBind();
                    //    if (Value == "-1")
                    //    {
                    //        drdMajor.DataBind();
                    //        drdMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                    //        drdMajor.SelectedIndex = 0;
                    //    }
                    //    else
                    //    {
                    //        drdMajor.SelectedIndex = drdMajor.Items.FindByValue(Value).Index;
                    //    }
                    //    break;
                    case "MFNo":
                        txtMFNo.Text = Value;
                        break;
                    case "EndDateFrom":
                        if (Value != "1")
                            txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        if (Value != "2")
                            txtEndDateTo.Text = Value;
                        break;

                    case "LastConfirmReqType":
                        if (Value != "-1")
                            CmbReqType.Value = Value;
                        break;
                    case "LastRequsetType":
                        if (Value != "-1")
                            CmbLastReqType.Value = Value;
                        break;

                    case "TaskId":
                        if (Value != "-1")
                        {
                            CmbTask.DataBind();
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;// int.Parse(Value);// + 1;
                        }
                        break;
                    case "GradeId":
                        if (Value != "-1")
                        {
                            comboGrade.DataBind();
                            comboGrade.SelectedIndex = comboGrade.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "MjParentId":
                        if (Value != "-1")
                        {
                            comboMjParent.DataBind();
                            comboMjParent.SelectedIndex = comboMjParent.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "RequesterType":
                        if (Value != "-1")
                        {
                            ComboRequesterType.DataBind();
                            ComboRequesterType.SelectedIndex = ComboRequesterType.Items.FindByValue(Value).Index;
                        }
                        break;
                }
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

                    GridViewMemberFile.DataBind();
                    Index = GridViewMemberFile.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewMemberFile.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewMemberFile.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewMemberFile.JSProperties["cpSelectedIndex"] = Index;
                        GridViewMemberFile.DetailRows.ExpandRow(Index);
                        GridViewMemberFile.FocusedRowIndex = Index;
                        GridViewMemberFile.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsMemberFileMainRequest.SelectParameters.Count; i++)
        {
            if (ObjdsMemberFileMainRequest.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsMemberFileMainRequest.SelectParameters[i].Name + "&";
                FilterString += ObjdsMemberFileMainRequest.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetGridRowColor(ASPxGridViewTableRowEventArgs e)
    {
        if (e.GetValue("LastRequsetType") != null)
        {
            // if (e.GetValue("LastConfirm").ToString() == "0")
            int LastRequsetType = Convert.ToInt32(e.GetValue("LastRequsetType"));
            switch (LastRequsetType)
            {
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument:
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                    e.Row.ForeColor = System.Drawing.Color.DarkMagenta;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                    e.Row.ForeColor = System.Drawing.Color.Magenta;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer:
                    e.Row.ForeColor = System.Drawing.Color.Gold;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival:
                    e.Row.ForeColor = System.Drawing.Color.Gold;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
            }
        }

        if (e.GetValue("LastConfirmReqType") != null)
        {
            if (Convert.ToInt32(e.GetValue("LastConfirmReqType")) == (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
                e.Row.ForeColor = System.Drawing.Color.Red;
        }
    }

    private object GetDataSource()
    {
        return ObjdsMemberFileMainRequest;
    }

    private void Search()
    {
        //ResetObjdsMemberFileMainRequest();        
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFamily.Text))
            ObjdsMemberFileMainRequest.SelectParameters["LastName"].DefaultValue = txtFamily.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjdsMemberFileMainRequest.SelectParameters["FirstName"].DefaultValue = txtName.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["FirstName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtMFNo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["MFNo"].DefaultValue = txtMFNo.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["MFNo"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = "-1";

        if (CmbReqType.SelectedIndex != -1 && CmbReqType.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["LastConfirmReqType"].DefaultValue = CmbReqType.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastConfirmReqType"].DefaultValue = "-1";

        if (CmbLastReqType.SelectedIndex != -1 && CmbLastReqType.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["LastRequsetType"].DefaultValue = CmbLastReqType.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastRequsetType"].DefaultValue = "-1";

        if (CmbPaymentType.SelectedIndex != -1 && CmbPaymentType.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["PaymentType"].DefaultValue = CmbPaymentType.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["PaymentType"].DefaultValue = "-1";
        if (CmbPaymentStatus.SelectedIndex != -1 && CmbPaymentStatus.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["PaymentStatus"].DefaultValue = CmbPaymentStatus.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["PaymentStatus"].DefaultValue = "-1";
        if (CmbFaultMemberRegister.SelectedIndex != -1 && CmbFaultMemberRegister.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["MeDataComplete"].DefaultValue = CmbFaultMemberRegister.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["MeDataComplete"].DefaultValue = "-1";

        if (CmbFaultDocument.SelectedIndex != -1 && CmbFaultDocument.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["MeDocComplete"].DefaultValue = CmbFaultDocument.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["MeDocComplete"].DefaultValue = "-1";

        if (CmbTask.SelectedIndex != -1)
            ObjdsMemberFileMainRequest.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtEndAuditor.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDoerName"].DefaultValue = txtEndAuditor.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDoerName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtDateEndAuditor.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDate"].DefaultValue = txtDateEndAuditor.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtDateEndAuditorTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDateTo"].DefaultValue = txtDateEndAuditorTo.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDateTo"].DefaultValue = "2";


        if (!string.IsNullOrEmpty(txtDateRequstRegister.Text))
            ObjdsMemberFileMainRequest.SelectParameters["CreateDateLastRequst"].DefaultValue = txtDateRequstRegister.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["CreateDateLastRequst"].DefaultValue = "1";


        if (!string.IsNullOrEmpty(txtLastRequestCreateDateFrom.Text))
            ObjdsMemberFileMainRequest.SelectParameters["LastRequestCreateDateFrom"].DefaultValue = txtLastRequestCreateDateFrom.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastRequestCreateDateFrom"].DefaultValue = "1";


        if (!string.IsNullOrEmpty(txtLastRequestCreateDateTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["LastRequestCreateDateTo"].DefaultValue = txtLastRequestCreateDateTo.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastRequestCreateDateTo"].DefaultValue = "2";

        if (comboGrade.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["GradeId"].DefaultValue = comboGrade.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["GradeId"].DefaultValue = "-1";

        if (comboMjParent.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["MjParentId"].DefaultValue = comboMjParent.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["MjParentId"].DefaultValue = "-1";

        if (ComboRequesterType.SelectedIndex != 0)
            ObjdsMemberFileMainRequest.SelectParameters["RequesterType"].DefaultValue = ComboRequesterType.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["RequesterType"].DefaultValue = "-1";

        GridViewMemberFile.DataBind();
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MeFileDocument).ToString());
    }

    void LoadWfHelpPrint()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHepPrint1.DataSource = dt1;
            RepeaterWfHepPrint1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHepPrint2.DataSource = dt2;
            RepeaterWfHepPrint2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHepPrint3.DataSource = dt3;
            RepeaterWfHepPrint3.DataBind();
        }
    }

    #endregion

}
