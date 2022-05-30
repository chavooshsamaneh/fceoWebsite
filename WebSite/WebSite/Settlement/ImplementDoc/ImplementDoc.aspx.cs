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

public partial class Settlement_ImplementDoc_ImplementDoc : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
            Session["SendBackDataTable_StlImpDoc"] = "";

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming).ToString();
            SetPageFilter();
            SetGridRowIndex();
        }

        Session["DataTable"] = GridViewMemberFile.Columns;
        Session["DataSource"] = ObjdsMeFileSubRequest;
        Session["Title"] = "مجوز فعالیت مجری حقیقی";

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtImpDoc = DocMemberFileManager.SelectImplementDoc(-1, MfId);
                if (dtImpDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtImpDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtImpDoc.Rows[0]["MemberId"].ToString());
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                        if (MRsId == 1)
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeFileId(MeFileId, 0);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                int LastMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                                if (CheckPermitionForEdit(LastMfId))
                                {
                                    NextPage("Edit");
                                }
                                else
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "برای شما امکان ویرایش اطلاعات وجود ندارد.";
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
                            this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
            DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
            
            Response.Redirect("../MemberDocument/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
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
        GridViewExporter.FileName = "ImplementDocument";

        GridViewExporter.WriteXlsToResponse(true);
        //GridViewMemberFile.Columns["Status"].Visible = true;
    }

    protected void GridViewMeFileHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("MemberId")))
            Session["MeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MemberId");
        //int ImpDocId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //DocMemberFileManager.ClearBeforeFill = true;
        //DocMemberFileManager.FindByCode(ImpDocId, 1);
        //if (DocMemberFileManager.Count == 1)
        //{
        //    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
        //    {
        //        int MfId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
        //        DocMemberFileManager.FindByCode(MfId, 0);
        //        if (DocMemberFileManager.Count == 1)
        //        {
        //            Session["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
        //        }
        //    }
        //}

        //int Index = GridViewMemberFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        //GridViewMemberFile.FocusedRowIndex = Index;
    }

    protected void GridViewMemberFile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewMemberFile.DataBind();
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
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                // e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        //if (e.DataColumn.FieldName == "TaskId")
        //{
        //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
        //    if (btnWFState != null)
        //    {
        //        if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
        //        {
        //            btnWFState.ToolTip = "تعریف نشده";
        //            btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
        //            return;
        //        }

        //        if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFStart.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFInProcess.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
        //        }
        //        else
        //        {
        //        }
        //    }
        //}
    }

    protected void GridViewMeFileHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                // e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMeFileHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        //if (e.DataColumn.FieldName == "TaskId")
        //{
        //    DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
        //    if (GridViewMeFileHistory == null)
        //        return;
        //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMeFileHistory.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMeFileHistory.Columns["WFState"], "btnWFState");
        //    if (btnWFState != null)
        //    {
        //        if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
        //        {
        //            btnWFState.ToolTip = "تعریف نشده";
        //            btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
        //            return;
        //        }

        //        if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFStart.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFInProcess.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
        //        }
        //        else
        //        {
        //        }
        //    }
        //}
    }

    protected void GridViewMemberFile_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int MfId = (int)row["MfId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
                    int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
                    WFUserControl.PerformCallback(MfId, TableType, WfCode, e);
                    GridViewMemberFile.DataBind();
                }
                else
                {
                    WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    return;
                }
            }
            else
            {
                WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }
    #endregion

    #region Methods
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
                        //***ImpDocId
                        MfId = (int)row["MfId"];
                        //***MfId
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
                //***MfId
                MeId = (int)row["MeId"];
                if (Mode != "New" || Mode != "View")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeFileId(MeId, 0);
                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        //***ImpDocId
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
                MfId = -1;
                Response.Redirect("ImplementDocBasicInfo.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("ImplementDocBasicInfo.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
        }
    }

    //private void SelectSendBackTask(int TableType, int TableId)
    //{
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming, TableId);
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        int CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //        int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
    //        int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
    //        int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
    //        int CurrentNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
    //        int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
    //        int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
    //        int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
    //        // if (CurrentNmcIdType == 0)
    //        {
    //            WorkFlowManager.FindByTableType(TableType, CurrentWorkFlowCode);
    //            if (WorkFlowManager.Count > 0)
    //            {
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
    //                        lblInstitueWarning.Text = "جریان کار پرونده پروانه انتخاب شده به اتمام رسیده است.";
    //                        break;
    //                    case -3:
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                        PanelMain.ClientVisible = false;
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
    //                        break;
    //                    default:
    //                        int WorkFlowId = int.Parse(WorkFlowManager[0]["WorkFlowId"].ToString());
    //                        DataTable dtSendBackTask = WorkFlowStateManager.SelectSendBackTask(SendBackTask, WorkFlowId);
    //                        if (dtSendBackTask.Rows.Count > 0)
    //                        {
    //                            Session["SendBackDataTable_StlImpDoc"] = dtSendBackTask;
    //                            cmbSendBackTask.DataSource = dtSendBackTask;
    //                            cmbSendBackTask.ValueField = "TaskId";
    //                            cmbSendBackTask.TextField = "TaskName";
    //                            cmbSendBackTask.DataBind();
    //                            cmbSendBackTask.SelectedIndex = 0;
    //                            PanelSaveSuccessfully.Visible = false;
    //                            PanelMain.Visible = true;
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
    //        //else
    //        //{
    //        //    PanelSaveSuccessfully.ClientVisible = true;
    //        //    PanelMain.ClientVisible = false;
    //        //    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //        //    lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
    //        //}
    //    }
    //    else
    //    {
    //        PanelSaveSuccessfully.ClientVisible = true;
    //        PanelMain.ClientVisible = false;
    //        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //        lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
    //    }

    //}

    //private void SendMemberFileDocToNextStep(int MfId)
    //{
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
    //    int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //    int RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess;
    //    int ConfirmProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess;
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

    //    DataTable dtSendBack = (DataTable)Session["SendBackDataTable_StlImpDoc"];
    //    cmbSendBackTask.DataSource = dtSendBack;
    //    cmbSendBackTask.ValueField = "TaskId";
    //    cmbSendBackTask.TextField = "TaskName";
    //    cmbSendBackTask.DataBind();

    //    int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
    //    TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

    //    TransactionManager.Add(WorkFlowStateManager);
    //    TransactionManager.Add(DocMemberFileManager);
    //    DocMemberFileManager.ClearBeforeFill = true;

    //    int NmcId = FindNmcId();
    //    if (NmcId > 0)
    //    {
    //        TransactionManager.BeginSave();
    //        string Url = "<a href='../ImageReport/AddImplementDoc.aspx?MFId=" + "" + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
    //        string MsgContent = "";
    //        int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, MfId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId(), MsgContent, Url);
    //        switch (SendDoc)
    //        {
    //            case -6:
    //                TransactionManager.CancelSave();
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                PanelMain.ClientVisible = false;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "امکان ارسال پرونده پروانه به مرحله جاری وجود ندارد.";
    //                break;
    //            case -4:
    //                TransactionManager.CancelSave();
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                PanelMain.ClientVisible = false;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
    //                break;
    //            case -5:
    //                TransactionManager.CancelSave();
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                PanelMain.ClientVisible = false;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
    //                break;
    //            case -8:
    //                TransactionManager.CancelSave();
    //                PanelSaveSuccessfully.ClientVisible = true;
    //                PanelMain.ClientVisible = false;
    //                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
    //                break;
    //            default:
    //                //  DocMemberFileManager.FindByCode(MfId,1);
    //                DocMemberFileManager.SelectImplementDoc(-1, MfId);
    //                if (DocMemberFileManager.Count > 0)
    //                {
    //                    int MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
    //                    DataTable dtMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeFileId(MeId, 0);
    //                    if (dtMeFile.Rows.Count > 0)
    //                    {
    //                        if (SelectedTaskId == RejectProccessTaskId)
    //                        {
    //                            DocMemberFileManager[0].BeginEdit();
    //                            DocMemberFileManager[0]["IsConfirm"] = 2;
    //                            DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                            DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;
    //                            DocMemberFileManager[0].EndEdit();
    //                            int cnt = DocMemberFileManager.Save();
    //                            if (cnt > 0)
    //                            {
    //                                TransactionManager.EndSave();
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                                lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                                PanelMain.ClientVisible = false;
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                                GridViewMemberFile.DataBind();
    //                            }
    //                            else
    //                            {
    //                                TransactionManager.CancelSave();
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                                lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
    //                                PanelMain.ClientVisible = false;
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                            }
    //                        }
    //                        else if (SelectedTaskId == ConfirmProccessTaskId)
    //                        {
    //                            int Type = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
    //                            if (Type != (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
    //                            {
    //                                if (Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]) ||
    //                                    Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]) ||
    //                                    Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
    //                                {
    //                                    TransactionManager.CancelSave();
    //                                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                                    lblInstitueWarning.Text = "اطلاعات مربوط به تاریخ اعتبار و شماره سریال پروانه ثبت نشده است.";
    //                                    PanelMain.ClientVisible = false;
    //                                    PanelSaveSuccessfully.ClientVisible = true;
    //                                }
    //                            }

    //                            DocMemberFileManager[0].BeginEdit();
    //                            DocMemberFileManager[0]["IsConfirm"] = 1;
    //                            if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
    //                                DocMemberFileManager[0]["InActive"] = 1;
    //                            DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                            DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;
    //                            DocMemberFileManager[0].EndEdit();
    //                            int cnt = DocMemberFileManager.Save();
    //                            if (cnt > 0)
    //                            {
    //                                TransactionManager.EndSave();
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                                lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                                PanelMain.ClientVisible = false;
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                                GridViewMemberFile.DataBind();
    //                            }
    //                            else
    //                            {
    //                                TransactionManager.CancelSave();
    //                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                                lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
    //                                PanelMain.ClientVisible = false;
    //                                PanelSaveSuccessfully.ClientVisible = true;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            TransactionManager.EndSave();
    //                            lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
    //                            lblInstitueWarning.Text = "ذخیره انجام شد.";
    //                            PanelMain.ClientVisible = false;
    //                            PanelSaveSuccessfully.ClientVisible = true;
    //                            GridViewMemberFile.DataBind();
    //                        }
    //                    }
    //                    else
    //                    {
    //                        TransactionManager.CancelSave();
    //                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                        lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
    //                        PanelMain.ClientVisible = false;
    //                        PanelSaveSuccessfully.ClientVisible = true;
    //                    }

    //                }
    //                else
    //                {
    //                    TransactionManager.CancelSave();
    //                    PanelSaveSuccessfully.ClientVisible = true;
    //                    PanelMain.ClientVisible = false;
    //                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //                    lblInstitueWarning.Text = "اطلاعات پرونده انتخاب شده توسط کاربر دیگری تغییر یافته است.";
    //                }
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        PanelSaveSuccessfully.ClientVisible = true;
    //        PanelMain.ClientVisible = false;
    //        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
    //        lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
    //    }
    //    GridViewMemberFile.DataBind();
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
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingImplementDoc;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
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
    #endregion
}
