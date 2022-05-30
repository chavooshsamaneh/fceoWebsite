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

public partial class Settlement_MemberDocument_MemberFile : System.Web.UI.Page
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
            HiddenFieldMeFile["MfList"] = "";
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming).ToString();
            SetPageFilter();
            SetGridRowIndex();
            LoadWfHelpPrint();
            this.ViewState["btnView"] = btnView.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;

            Session["SendBackDataTable_StlMeFile"] = "";
            ComboBoxWorkFlow.SelectedIndex = -1;
            Search(true);
        }
        Session["DataTable"] = GridViewMemberFile.Columns;
        Session["DataSource"] = ObjdsMemberFileMainRequest;
        Session["Title"] = "پروانه اشتغال به کار شخص حقیقی";
        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");
        DeletedColumnsName.Add("CheckBox");

        Session["DeletedColumnsName"] = DeletedColumnsName;

        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];

        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtDateEndAuditor = document.getElementById('" + txtDateEndAuditor.ClientID + "').value;";
        script += "var txtDateEndAuditorTo = document.getElementById('" + txtDateEndAuditorTo.ClientID + "').value;";

        script += "if ( txtMeId.GetText() == '' && txtName.GetText() == ''&& txtFamily.GetText() == ''  &&  txtFollowCode.GetText() == '' &&  txtEndAuditor.GetText() == '' && CmbReqType.GetSelectedIndex() == 0&& ComboBoxWorkFlow.GetSelectedIndex() == -1  && txtEndDateFrom==''  && txtDateEndAuditor=='' && txtDateEndAuditorTo==''"
            + " && txtEndDateTo==''  &&  txtMFNo.GetText() == ''  && txtDateEndAuditorTo=='') return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); txtName.SetText(''); txtFamily.SetText('');txtFollowCode.SetText(''); txtEndAuditor.SetText('');
                    CmbReqType.SetSelectedIndex(-1); ComboBoxWorkFlow.SetSelectedIndex(-1);
                    document.getElementById('" + txtEndDateFrom.ClientID + "').value = ''; document.getElementById('" + txtEndDateTo.ClientID + "').value = ''; document.getElementById('" + txtDateEndAuditorTo.ClientID + "').value = '';";
        script += "document.getElementById('" + txtDateEndAuditor.ClientID + "').value = '';}</SCRIPT>";


        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    //*************************************************************************************************************

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search(false);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        int focucedIndex = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory != null)
            {
                focucedIndex = GridViewMeFileHistory.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                    DataRow row = GridViewMeFileHistory.GetDataRow(focucedIndex);
                    int MfId = (int)row["MfId"];
                    int TableId = (int)row["MfId"];

                    Response.Redirect("../MemberDocument/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
                }
            }
            else
            {

                ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {

            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک رکرد را انتخاب نمایید.");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            ShowMessage("ابتدا یک در خواست مربوط پرونده مورد نظر را انتخاب نمایید.");
            return;
        }
        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
            if (MRsId == 1)
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
                if (dtDocMeFile.Rows.Count > 0)
                {
                    int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    if (CheckPermitionForEdit(MfId))
                    {
                        NextPage("Edit");
                    }
                    else
                    {

                        ShowMessage("امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.");
                    }
                }
                else
                {

                    ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.");
                }

            }
            else
            {

                ShowMessage("امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            }
        }
        else
        {

            ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
        }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "MemberDocument";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnPrePrint_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex < 0)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MfId = -2;
        int MeId = -2;
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
        DataRow SelectedRow = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
        if (SelectedRow == null)
        {
            ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
            return;
        }

        if (!Utility.IsDBNullOrNullValue(SelectedRow["MeId"]))
            MeId = int.Parse(SelectedRow["MeId"].ToString());
        if (!Utility.IsDBNullOrNullValue(SelectedRow["MfId"]))
            MfId = Convert.ToInt32(SelectedRow["MfId"]);
        //GridViewMemberFile.JSProperties["cpDoPrePrint"] = 1;
        //GridViewMemberFile.JSProperties["cpPrePrintPath"] = "../../ReportForms/ReportMemberFilePrePrint.aspx?MeId="
        //    + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('../../ReportForms/ReportMemberFilePrePrint.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "' ,'_blank');", true);
    }
    //protected void btnClearSearch_OnClick(object sender, EventArgs e)
    //{
    //    Clear();
    //    GridViewMemberFile.DataBind();
    //}

    //*************************************************************************************************************
    //protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    //{
    //    int focucedIndex = -1;
    //    if (GridViewMemberFile.FocusedRowIndex > -1)
    //    {
    //        TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
    //        if (GridViewMeFileHistory != null)
    //        {
    //            focucedIndex = GridViewMeFileHistory.FocusedRowIndex;
    //            if (focucedIndex > -1)
    //            {
    //                DataRow row = GridViewMeFileHistory.GetDataRow(focucedIndex);
    //                int MfId = (int)row["MfId"];
    //                int MeFileTableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
    //                if (e.Parameter == "Send")
    //                {
    //                    SendMemberFileDocToNextStep(MfId);
    //                    GridViewMemberFile.DataBind();
    //                }
    //                else
    //                {
    //                    SelectSendBackTask(WorkFlowCode, MfId);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            PanelSaveSuccessfully.ClientVisible = true;
    //            PanelMain.ClientVisible = false;
    //            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //            lblInstitueWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";

    //        }
    //    }
    //    else
    //    {
    //        lblError.Text = "ردیفی انتخاب نشده است.";
    //    }
    //}

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP") return;


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

    #region GridViewMemberFile
    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberFile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
        {
            return;
        }
        GridViewMemberFile.DataBind();
        GridViewMemberFile.ExpandRow(GridViewMemberFile.FocusedRowIndex);

        GridViewMemberFile.JSProperties["cpDoPrintAll"] = 0;
        GridViewMemberFile.JSProperties["cpPrintAllPath"] = "";

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MfId = -2;
        int MeId = -2;
        DataRow MeFileRow;
        switch (e.Parameters)
        {
            case "ReportAll":
                #region ReportAll
                if (GridViewMemberFile.FocusedRowIndex < 0)
                {
                    ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                    return;
                }
                MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
                if (MeFileRow == null)
                {
                    ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                    return;
                }
                MeId = int.Parse(MeFileRow["MeId"].ToString());
                TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistoryReportAll = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileHistoryReportAll != null)
                {
                    if (GridViewMeFileHistoryReportAll.FocusedRowIndex > -1)
                    {
                        DataRow row = GridViewMeFileHistoryReportAll.GetDataRow(GridViewMeFileHistoryReportAll.FocusedRowIndex);
                        if (row != null)
                        {
                            if (!Utility.IsDBNullOrNullValue(row["MfId"]))
                                MfId = Convert.ToInt32(row["MfId"]);
                            GridViewMemberFile.JSProperties["cpDoPrintAll"] = 1;
                            GridViewMemberFile.JSProperties["cpPrintAllPath"] = "../../ReportForms/DocMemberFileReport.aspx?MeId="
                                + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());

                        }
                        else
                        {
                            ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                            return;
                        }
                    }
                }
                else
                {
                    DataTable dtDocMeFile1 = DocMemberFileManager.SelectLastVersion(MeId, 0, -1);
                    if (dtDocMeFile1.Rows.Count > 0)
                    {
                        MfId = (int)dtDocMeFile1.Rows[0]["MfId"];
                        GridViewMemberFile.JSProperties["cpDoPrintAll"] = 1;
                        GridViewMemberFile.JSProperties["cpPrintAllPath"] = "../../ReportForms/DocMemberFileReport.aspx?MeId="
                            + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());
                    }
                    else
                    {
                        ShowMessage("خطایی در بازخوانی اطلاعات پروانه و درخواست این فرد ایجاد شده است");
                        return;
                    }
                }
                #endregion
                break;
            case "PrePrint":
                #region
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
                DataRow SelectedRow = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
                if (SelectedRow == null)
                {
                    ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
                    return;
                }

                if (!Utility.IsDBNullOrNullValue(SelectedRow["MeId"]))
                    MeId = int.Parse(SelectedRow["MeId"].ToString());
                if (!Utility.IsDBNullOrNullValue(SelectedRow["MfId"]))
                    MfId = Convert.ToInt32(SelectedRow["MfId"]);
                GridViewMemberFile.JSProperties["cpDoPrePrint"] = 1;
                GridViewMemberFile.JSProperties["cpPrePrintPath"] = "../../ReportForms/ReportMemberFilePrePrint.aspx?MeId="
                    + Utility.EncryptQS(MeId.ToString()) + "&MFId=" + Utility.EncryptQS(MfId.ToString());

                #endregion
                break;
            default:
                GridViewMemberFile.Selection.UnselectAll();
                break;

        }
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
    }

    protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }
                else if (!Utility.IsDBNullOrNullValue(e.GetValue("TaskName")))
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                }
                //if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                //{
                //btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //  btnWFState.ImageUrl = "~/Images/WFStart.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //  btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                // btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                //{
                //btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //  btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                //}
                //else
                //{
                //}
            }

        }
    }

    protected void GridViewMemberFile_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
    }
    #endregion

    #region GridViewMemberFileHistory
    protected void GridViewMeFileHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {
        int MfId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(MfId, 0);
        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MfId"]))
            Session["MeId"] = DocMemberFileManager[0]["MeId"].ToString();

        int Index = GridViewMemberFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewMemberFile.FocusedRowIndex = Index;
    }

    protected void GridViewMeFileHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "MailNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                // e.Editor.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ٍExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMeFileHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MailNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMeFileHistory.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMeFileHistory.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }
                if (Utility.IsDBNullOrNullValue(e.GetValue("TaskName")))
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();

                //if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFStart.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                //}
                //else
                //{
                //}
            }
        }
    }

    #endregion


    #endregion

    #region Methods
    private void ResetObjdsMemberFileMainRequest(Boolean SetDefualt)
    {
        ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";
        ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";
        ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";
        ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = "-1";
        ObjdsMemberFileMainRequest.SelectParameters["FirstName"].DefaultValue = "%";
        ObjdsMemberFileMainRequest.SelectParameters["LastName"].DefaultValue = "%";
        ObjdsMemberFileMainRequest.SelectParameters["MFNo"].DefaultValue = "%";
        ObjdsMemberFileMainRequest.SelectParameters["TaskCodeAccConf"].DefaultValue = "-1";
        ComboBoxWorkFlow.DataBind();
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        if (TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(-2, (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument, Utility.GetCurrentUser_UserId()) > 0)
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument).ToString();
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = ComboBoxWorkFlow.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument).ToString()).Index;
        }
        else if (TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(-2, (int)TSP.DataManager.WorkFlowTask.NezamEmployeeInSettlementConfirmingDocument, Utility.GetCurrentUser_UserId()) > 0)
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = "-1";
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = ComboBoxWorkFlow.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.NezamEmployeeInSettlementConfirmingDocument).ToString()).Index;
        }

        else if (TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(-2, (int)TSP.DataManager.WorkFlowTask.ModireMaskanConfirmatingDocument, Utility.GetCurrentUser_UserId()) > 0)
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.ModireMaskanConfirmatingDocument).ToString();
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = ComboBoxWorkFlow.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.ModireMaskanConfirmatingDocument).ToString()).Index;
        }
        else if (TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(-2, (int)TSP.DataManager.WorkFlowTask.RoadAndurbanismConfirmingDocument, Utility.GetCurrentUser_UserId()) > 0)
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.RoadAndurbanismConfirmingDocument).ToString();
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = ComboBoxWorkFlow.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.RoadAndurbanismConfirmingDocument).ToString()).Index;
        }

        else if (TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(-2, (int)TSP.DataManager.WorkFlowTask.PrintDocumentByNezamEmployee, Utility.GetCurrentUser_UserId()) > 0)
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.PrintDocumentByNezamEmployee).ToString();
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = ComboBoxWorkFlow.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.PrintDocumentByNezamEmployee).ToString()).Index;
        }
        else
        {
            ObjdsMemberFileMainRequest.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.RejectDocumentOfMemberAndEndProcess).ToString();
            if (SetDefualt)
                ComboBoxWorkFlow.SelectedIndex = -1;
        }
    }
    private void Search(Boolean SetDefualt)
    {
        ResetObjdsMemberFileMainRequest(SetDefualt);
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

        if (CmbReqType.SelectedIndex != -1)
            ObjdsMemberFileMainRequest.SelectParameters["LastConfirmReqType"].DefaultValue = CmbReqType.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["LastConfirmReqType"].DefaultValue = "-1";
        if (ComboBoxWorkFlow.SelectedIndex != -1)
            ObjdsMemberFileMainRequest.SelectParameters["TaskCodeAccConf"].DefaultValue = ComboBoxWorkFlow.Value.ToString();
        else
            ObjdsMemberFileMainRequest.SelectParameters["TaskCodeAccConf"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtDateEndAuditor.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDate"].DefaultValue = txtDateEndAuditor.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtDateEndAuditorTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDateTo"].DefaultValue = txtDateEndAuditorTo.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDateTo"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtEndAuditor.Text))
            ObjdsMemberFileMainRequest.SelectParameters["WFDoerName"].DefaultValue = txtEndAuditor.Text;
        else
            ObjdsMemberFileMainRequest.SelectParameters["WFDoerName"].DefaultValue = "%";


        GridViewMemberFile.DataBind();
    }


    private void ShowMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }

    private void NextPage(string Mode)
    {
        int MfId = -1;
        int MeId = -1;
        int focucedIndex = -1;
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

                    ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
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

            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string GridFilterString = GridViewMemberFile.FilterExpression;

            if (Mode == "New")
            {
                MfId = -1;
                Response.Redirect("MemberFileBasicInfo.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
            }
            else
            {
                Response.Redirect("MemberFileBasicInfo.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
        }
    }


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

            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
        //LoginManager.FindByCode(UserId);

        //if (LoginManager.Count > 0)
        //{
        //    int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
        //    int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
        //    NezamChartManager.FindByEmpId(EmpId, UltId);
        //    if (NezamChartManager.Count > 0)
        //    {
        //        NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
        //    }
        //    else
        //    {
        //        DivReport.Visible = true;
        //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
        //    }
        //}
        //else
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return (-1);
        //}
        //return (NmcId);
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
    #endregion
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
