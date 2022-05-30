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

public partial class Employee_Amoozesh_Seminar : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowId;
            }
            cmbInstitue.DataBind();
            cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));


            Session["SendBackDataTable_Se"] = null;

            // CheckWorkFlowPermission();
            TSP.DataManager.Permission per = TSP.DataManager.SeminarManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnView.Enabled = per.CanView;
            btnView1.Enabled = per.CanView;

            GridViewSeminar.ClientVisible = per.CanView;
        }




        string script = @"function CheckSearch() {var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtStartDateFrom = document.getElementById('" + txtStartDateFrom.ClientID + "').value;";
        script += "var txtStartDateTo = document.getElementById('" + txtStartDateTo.ClientID + "').value; ";
        script += " if ( txtSeminarSubject.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && cmbInstitue.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }";
        script += @"function ClearSearch() {
      
        
        txtSeminarSubject.SetText('');
        cmbInstitue.SetSelectedIndex(0);
        CmbTask.SetSelectedIndex(0);
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtStartDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtStartDateTo.ClientID + @"').value='';
     }";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);


        SetPageFilter();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    #region btn
    protected void btnView_Click(object sender, EventArgs e)
    {

        int SeReqId = -1;
        try
        {

            int SeId = -1;
            string SearchFilterString = GenerateFilterString();
            string GridFilterString = GridViewSeminar.FilterExpression;

            if (GridViewSeminar.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
                SeId = (int)row["SeId"];
            }
            if (SeId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا درخواست یک سمینار را انتخاب نمائید";
                return;
            }
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }

        }
        catch (Exception err) { Utility.SaveWebsiteError(err); }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        int SeReqId = -1;
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            string SearchFilterString = GenerateFilterString();
            string GridFilterString = GridViewSeminar.FilterExpression;
            if (GridViewSeminar.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
                SeId = (int)row["SeId"];
            }
            if (SeId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
                return;
            }

            SeReqId = -2;
            TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
            SeminarRequestManager.SelectSeminarRequest(SeId, -1, 0);
            if (SeminarRequestManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            SeminarRequestManager.SelectSeminarRequest(SeId, -1, 1);
            if (SeminarRequestManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل عدم وجود درخواست تایید شده برای این سمینار، امکان ثبت درخواست جدید وجود ندارد.";
                return;
            }
            SeReqId = Convert.ToInt32(SeminarRequestManager[SeminarRequestManager.Count - 1]["SeReqId"]);
            Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change")
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString), false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnDelReq_Click(object sender, EventArgs e)
    {
        int SeReqId = -1;
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }

            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                        TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
                        SeminarRequestManager.FindByCode(SeReqId);
                        if (SeminarRequestManager.Count == 1)
                        {
                            if (Convert.ToInt32(SeminarRequestManager[0]["IsConfirm"]) != 0)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            //if (Convert.ToInt32(SeminarRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Institute)
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "این درخواست در پرتال موسسه ثبت شده و توسط شما قابل حذف نمی باشد.";
                            //    return;
                            //}
                            SeminarRequestManager.DeleteSeminarRequestInfo(SeReqId);
                            ShowMessage("حذف درخواست با موفقیت انجام شد");
                            GridViewSeminar.DataBind();
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطا در بازخوانی اطلاعات";
                            return;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف درخواست انجام گرفته است";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AmoozeshHome.aspx");

    }

    protected void btnJudge_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            SeId = (int)row["SeId"];
        }
        if (SeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            CheckWorkFlowPermission(SeId);

        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.Seminar;
            DataRow PeriodRow = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            int TableId = int.Parse(PeriodRow["SeId"].ToString());

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Seminars";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSeminarAttender_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            SeId = (int)row["SeId"];
        }
        if (SeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        string SearchFilterString = GenerateFilterString();
        string GridFilterString = GridViewSeminar.FilterExpression;
        Response.Redirect("SeminarAttender.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        int SeReqId = -1;
        string SearchFilterString = GenerateFilterString();
        string GridFilterString = GridViewSeminar.FilterExpression;
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            SeId = (int)row["SeId"];
        }
        if (SeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
        if (GridRequest != null)
        {
            if (GridRequest.VisibleRowCount > 0)
            {
                int index0 = GridRequest.FocusedRowIndex;
                if (index0 != -1)
                {
                    SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                    TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
                    SeminarRequestManager.FindByCode(SeReqId);
                    if (SeminarRequestManager.Count == 1)
                    {
                        if (Convert.ToInt32(SeminarRequestManager[0]["IsConfirm"]) != 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش برای درخواست پاسخ داده شده وجود ندارد";
                            return;
                        }
                        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطا در بازخوانی اطلاعات";
                        return;
                    }

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        string SearchFilterString = GenerateFilterString();
        string GridFilterString = GridViewSeminar.FilterExpression;
        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&SeReqId=" + Utility.EncryptQS("-1") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }
    #endregion

    protected void GridViewSeminar_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();

    }

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["SeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("SeId");
        int Index = GridViewSeminar.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewSeminar.FocusedRowIndex = Index;
    }

    protected void GridViewRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
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

    protected void GridViewSeminar_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    GridViewSeminar.JSProperties["cpPrint"] = 1;
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewSeminar.Columns;
                    Session["DataSource"] = OdbSeminar;
                    Session["Title"] = "سمینارها";
                    break;
            }
        }
        else
            GridViewSeminar.DataBind();
    }

    protected void GridViewSeminar_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewSeminar.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewSeminar.Columns["WFState"], "btnWFState");
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

    protected void GridViewSeminar_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {

        int SeReqId = -1;
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
                        int WfCode = (int)TSP.DataManager.WorkFlows.SeminarConfirming;
                        WFUserControl.PerformCallback(SeReqId, TableType, WfCode, e);
                        GridViewSeminar.DataBind();
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
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (cmbInstitue.SelectedItem != null && cmbInstitue.SelectedItem.Value != null)
            OdbSeminar.SelectParameters["InsId"].DefaultValue = cmbInstitue.SelectedItem.Value.ToString();
        else
            OdbSeminar.SelectParameters["InsId"].DefaultValue = "-1";
        if (!string.IsNullOrWhiteSpace(txtStartDateFrom.Text))
            OdbSeminar.SelectParameters["StartDateFrom"].DefaultValue = txtStartDateFrom.Text;
        else
            OdbSeminar.SelectParameters["StartDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrWhiteSpace(txtStartDateTo.Text))
            OdbSeminar.SelectParameters["StartDateTo"].DefaultValue = txtStartDateTo.Text;
        else
            OdbSeminar.SelectParameters["StartDateTo"].DefaultValue = "2";
        if (!string.IsNullOrWhiteSpace(txtEndDateFrom.Text))
            OdbSeminar.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbSeminar.SelectParameters["EndDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrWhiteSpace(txtEndDateTo.Text))
            OdbSeminar.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            OdbSeminar.SelectParameters["EndDateTo"].DefaultValue = "2";
        if (CmbTask.SelectedIndex != 0)
            OdbSeminar.SelectParameters["TaskCode"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbSeminar.SelectParameters["TaskCode"].DefaultValue = "-1";

        if (!string.IsNullOrWhiteSpace(txtSeminarSubject.Text))
            OdbSeminar.SelectParameters["SeminarSubject"].DefaultValue = txtSeminarSubject.Text;
        else
            OdbSeminar.SelectParameters["SeminarSubject"].DefaultValue = "%";

        GridViewSeminar.DataBind();
    }
    #endregion

    #region Methods

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Seminar);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Seminar);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    private void CheckWorkFlowPermission(int SeId)
    {
        int CurrentSubOrder = -1;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        int SeminarExpertOrder = -1;

        int SeminarExpertCode = (int)TSP.DataManager.WorkFlowTask.SeminarExpertCommitteeConfirming;
        WorkFlowTaskManager.FindByTaskCode(SeminarExpertCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            SeminarExpertOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        int SeminarLearningOrder = -1;
        int SeminarLearningCode = (int)TSP.DataManager.WorkFlowTask.SeminarLearningCommitteeGrading;
        WorkFlowTaskManager.FindByTaskCode(SeminarLearningCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            SeminarLearningOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }


        int CurrentStateOrder = -1;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)(TSP.DataManager.TableCodes.Seminar);
        //int TableId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString()));
        int TableId = SeId;
        DataTable dtWorkState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkState.Rows.Count > 0)
        {
            CurrentStateOrder = int.Parse(dtWorkState.Rows[0]["TaskOrder"].ToString());
            CurrentSubOrder = int.Parse(dtWorkState.Rows[0]["SubOrder"].ToString());
        }

        if ((CurrentStateOrder == SeminarExpertOrder) || (CurrentStateOrder != -1))
        {
            if (SeminarExpertOrder < SeminarLearningOrder)
            {
                if (SeminarExpertOrder != 0)
                {
                    Boolean CommitteeGradingper = CheckWorkFlowPermissionForExpert(CurrentSubOrder);
                    if (CommitteeGradingper)
                    {
                        //  btnJudge.Enabled = true;
                        HDState.Value = Utility.EncryptQS("ExpertGroup");
                        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Judge") + "&State=" + HDState.Value);

                    }

                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت نظر برای شما وجود ندارد ";
                        return;
                        //btnJudge.Enabled = false;
                    }

                }
            }
            else
            {
                if (SeminarLearningOrder != 0)
                {
                    Boolean ComissionGradingPer = CheckWorkFlowPermissionForLearningGrading(CurrentSubOrder);
                    if (ComissionGradingPer)
                    {
                        // btnJudge.Enabled = true;
                        HDState.Value = Utility.EncryptQS("LearningGrade");
                        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Judge") + "&State=" + HDState.Value);


                    }

                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت نظر برای شما وجود ندارد ";
                        return;
                        //   btnJudge.Enabled = false;
                    }

                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ثبت نظر برای شما وجود ندارد ";
            return;
            //btnJudge.Enabled = false;

        }
        // CheckWorkFlowPermissionForSave();
    }

    private Boolean CheckWorkFlowPermissionForExpert(int CurrentSubOrder)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int SeminarExpertCommitteeConfirmingCode = (int)TSP.DataManager.WorkFlowTask.SeminarExpertCommitteeConfirming;
        WorkFlowTaskManager.FindByTaskCode(SeminarExpertCommitteeConfirmingCode);
        int DoerId = -1;
        DoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(SeminarExpertCommitteeConfirmingCode, CurrentSubOrder, Utility.GetCurrentUser_UserId(), true);
        if (DoerId > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean CheckWorkFlowPermissionForLearningGrading(int CurrentSubOrder)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        int SeminarLearningCommitteeGradingCode = (int)TSP.DataManager.WorkFlowTask.SeminarLearningCommitteeGrading;
        WorkFlowTaskManager.FindByTaskCode(SeminarLearningCommitteeGradingCode);

        int DoerId = -1;
        DoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(SeminarLearningCommitteeGradingCode, CurrentSubOrder, Utility.GetCurrentUser_UserId(), true);
        if (DoerId > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {

                int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));

                GridViewSeminar.DataBind();
                Index = GridViewSeminar.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewSeminar.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewSeminar.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewSeminar.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewSeminar.FilterExpression = GrdFlt;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());
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
            OdbSeminar.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "SeminarSubject":
                        txtSeminarSubject.Text = Value;
                        break;

                    case "EndDateFrom":
                        if (Value != "1")
                            txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        if (Value != "2")
                            txtEndDateTo.Text = Value;
                        break;
                    case "StartDateFrom":
                        if (Value != "1")
                            txtStartDateFrom.Text = Value;
                        break;
                    case "StartDateTo":
                        if (Value != "2")
                            txtStartDateTo.Text = Value;
                        break;
                    case "InsId":
                        cmbInstitue.DataBind();
                        if (Value == "-1")
                        {
                            cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbInstitue.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbInstitue.SelectedIndex = cmbInstitue.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "TaskCode":
                        CmbTask.DataBind();
                        if (Value == "-1")
                        {
                            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            CmbTask.SelectedIndex = 0;
                        }
                        else
                        {
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        }
                        break;
                }
            }
        }
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < OdbSeminar.SelectParameters.Count; i++)
        {
            if (OdbSeminar.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += OdbSeminar.SelectParameters[i].Name + "&";
                FilterString += OdbSeminar.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    #endregion
}
