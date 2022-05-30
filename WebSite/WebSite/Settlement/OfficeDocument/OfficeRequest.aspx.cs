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

public partial class Settlement_OfficeDocument_OfficeRequest : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewOffice.JSProperties["cpSelectedIndex"] = 0;
            GridViewOffice.JSProperties["cpIsReturn"] = 0;
            GridViewOffice.JSProperties["cpIsPostBack"] = 1;
            ObjdsOffice.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringDocumentOff).ToString();
            ObjdsOffice.CacheDuration = Utility.GetCacheDuration();

            Session["SendBackDataTable_OffConfStl"] = "";
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.OfficeConfirming).ToString();
        }
        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");
        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewOffice.Columns;
        Session["DataSource"] = ObjdsOffice;
        Session["Title"] = "شرکت ها";

        SetPageFilter();
        SetGridRowIndex();

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewOffice.FilterExpression;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int OfId = int.Parse(MeFileRow["OfId"].ToString());

                TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
                OffManager.FindByCode(OfId);
                if (OffManager.Count == 1)
                {
                    int MRsId = int.Parse(OffManager[0]["MrsId"].ToString());
                    if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
                        || MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel
                        || MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
                    {
                        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                        ReqManager.FindByOfficeId(OfId, -1, -1);
                        if (ReqManager.Count > 0)
                        {
                            int OfReId = int.Parse(ReqManager[0]["OfReId"].ToString());
                            if (CheckPermitionForEdit(OfReId))
                            {
                                Response.Redirect("OfficeRequestShow.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));// + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.";
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
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        int OfReId = -1;
        int OfId = -1;
        string GridFilterString = GridViewOffice.FilterExpression;
        //string SearchFilterString = GenerateFilterString();

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
                        OfId = int.Parse(GridRequest.GetDataRow(index0)["OfId"].ToString());
                        Response.Redirect("OfficeRequestShow.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));// + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    }

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
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                focucedIndex = GridRequest.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    DataRow row = GridRequest.GetDataRow(focucedIndex);
                    int TableId = (int)row["OfReId"];

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

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewOffice.Columns["WFState"].Visible = false;
        GridViewExporter.FileName = "OfficeDocument";

        GridViewExporter.WriteXlsToResponse(true);
        GridViewOffice.Columns["WFState"].Visible = true;
    }

    /*********************************************************************************************************************************************************************/

    protected void GridViewOffice_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("IsConfirm") != null)
        {
            if (e.GetValue("IsConfirm").ToString() == "0" && e.GetValue("Type").ToString() != "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

    protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate" || e.DataColumn.FieldName == "MFNo")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridViewRequest == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
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

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "AnswerDate" || e.Column.FieldName == "MFNo")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewOffice_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

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

    protected void GridViewOffice_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewOffice_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewOffice.JSProperties["cpIsPostBack"] = 1;
    }

    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["OfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewOffice.FocusedRowIndex = Index;
    }

    protected void GridViewOffice_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewOffice.JSProperties["cpPrint"] = 0;
        switch (e.Parameters)
        {
            case "Print":
                GridViewOffice.DetailRows.CollapseAllRows();
                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("WFState");
                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewOffice.Columns;
                Session["DataSource"] = ObjdsOffice;
                Session["Title"] = "شرکت ها";
                GridViewOffice.JSProperties["cpPrint"] = 1;
                break;
            default:
                GridViewOffice.DataBind();
                break;
        }
    }
    /*********************************************************************************************************************************************************************/
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP")
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
        if (GridViewOffice.FocusedRowIndex < 0)
        {

            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        DataRow rowOff = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
        int OfId = (int)rowOff["OfId"];

        TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (grid == null)
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        if (grid.FocusedRowIndex < 0)
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        DataRow row = grid.GetDataRow(grid.FocusedRowIndex);
        int OfReId = (int)row["OfReId"];
        int WfCode = (int)row["WorkFlowCode"];

        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        //if (WfCode == (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming)
        //{
        //    WFUserControl.SetMsgText("پرونده عضو حقوقی انتخاب شده توسط واحد عضویت در حال بررسی می باشد.جهت انجام عملیات از طریق واحد عضویت اقدام نمایید");       
        //    return;
        //}
        WFUserControl.PerformCallback(OfReId, TableType, WfCode, e);
        grid.DataBind();
        GridViewOffice.DataBind();
    }
    #endregion

    #region Methods   

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringDocumentOff;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;

                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
                    return true;

            }

        }
        return false;
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
                    GridViewOffice.FilterExpression = GrdFlt;
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

                    GridViewOffice.DataBind();
                    Index = GridViewOffice.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewOffice.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewOffice.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewOffice.JSProperties["cpSelectedIndex"] = Index;
                        GridViewOffice.DetailRows.ExpandRow(Index);
                        GridViewOffice.FocusedRowIndex = Index;
                        GridViewOffice.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    #endregion
    #endregion
}
