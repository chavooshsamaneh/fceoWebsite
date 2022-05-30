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

public partial class Institue_Amoozesh_Period : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            OdbPeriod.SelectParameters["InsId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCodeList"].DefaultValue = ((int)TSP.DataManager.WorkFlows.PeriodConfirming).ToString() + "," + ((int)TSP.DataManager.WorkFlows.PrindPeriodCertificates).ToString();
          
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("-----------------", null));
            CmbTask.SelectedIndex = 0;
            Session["SendBackDataTable_Period"] = null;
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            InstitueId.Value = Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
            btnInActive.Visible = false;
            LoadWfHelpPeriodConfirm();
            LoadWfHelpPeriodPrint();
        }

        SetPageFilter();
        SetGridRowIndex();

        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtStartDateFrom = document.getElementById('" + txtStartDateFrom.ClientID + "').value;";
        script += "var txtStartDateTo = document.getElementById('" + txtStartDateTo.ClientID + "').value;";
        script += "var txtTestDateFrom = document.getElementById('" + txtTestDateFrom.ClientID + "').value;";
        script += "var txtTestDateTo = document.getElementById('" + txtTestDateTo.ClientID + "').value;";

        script += "if (txtPPCode.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && txtTestDateFrom=='' && txtTestDateTo=='' && cmbCourse.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        CmbTask.SetSelectedIndex(0);
        txtPPCode.SetText('');
        cmbCourse.SetSelectedIndex(0);
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtStartDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtStartDateTo.ClientID + @"').value='';
        document.getElementById('" + txtTestDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtTestDateTo.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            string GridFilterString = GridViewPeriods.FilterExpression;
            if (GridViewPeriods.FocusedRowIndex > -1)
            {
                DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
                PPId = (int)row["PPId"];
            }
            if (PPId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
                return;
            }

            PPRId = -2;// int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
            TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
            PeriodPresentRequestManager.Select(-1, PPId, -1, 0);
            if (PeriodPresentRequestManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            PeriodPresentRequestManager.Select(-1, PPId, -1, 1);
            if (PeriodPresentRequestManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل عدم وجود درخواست تایید شده برای این دوره، امکان ثبت درخواست جدید وجود ندارد.";
                return;
            }
            PPRId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPRId"]);
            Response.Redirect("AddPeriod.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change")
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString()) + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()) + "&InsId=" + InstitueId.Value, false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Periods";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPeriod.aspx?PPId=" + Utility.EncryptQS("-1") + "&PPRId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&InsId=" + InstitueId.Value);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        string GridFilterString = GridViewPeriods.FilterExpression;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        string SearchFilterString = GenerateFilterString();
                        PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        Response.Redirect("AddPeriod.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("View")
                        + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)  +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString())
                        + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()) + "&InsId=" + InstitueId.Value, false);
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
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueHome.aspx?MeId=" + InstitueId.Value);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        string GridFilterString = GridViewPeriods.FilterExpression;

        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());

                        if (!CheckWorkFlowPermission(PPRId))
                        {
                            ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
                            return;
                        }

                        TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
                        PeriodPresentRequestManager.FindByCode(PPRId);
                        if (PeriodPresentRequestManager.Count == 1)
                        {
                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["IsConfirm"]) != 0)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان ویرایش برای درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }

                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Employee)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "این درخواست در پرتال کارمندان ثبت شده و توسط شما قابل ویرایش نمی باشد.";
                                return;
                            }
                            string SearchFilterString = GenerateFilterString();
                            Response.Redirect("AddPeriod.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                                  + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString())
                          + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()) + "&InsId=" + InstitueId.Value, false);
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
    }

    protected void btnDelReq_Click(object sender, EventArgs e)
    {
        int PPRId = -1;
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
                        PeriodPresentRequestManager.FindByCode(PPRId);
                        if (PeriodPresentRequestManager.Count == 1)
                        {
                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["IsConfirm"]) != 0)
                            {
                                ShowMessage("امکان حذف درخواست پاسخ داده شده وجود ندارد");
                                return;
                            }
                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Employee)
                            {
                                ShowMessage("این درخواست در پرتال کارمندان ثبت شده و توسط شما قابل حذف نمی باشد.");
                                return;
                            }

                            int PPId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPId"]);
                            PeriodPresentRequestManager.DeletePeriodPresentRequestInfo(PPId, PPRId, 1);
                            ShowMessage("حذف درخواست با موفقیت انجام شد");
                            GridViewPeriods.DataBind();
                        }
                        else
                        {
                            ShowMessage("خطا در بازخوانی اطلاعات");
                            return;
                        }
                    }
                    else
                    {
                        ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    }
                }
                else
                {
                    ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                }
            }
            else
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در حذف درخواست انجام گرفته است");
        }
    }

    protected void btnPeriodAttender_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        string GridFilterString = GridViewPeriods.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        Response.Redirect("PeriodAttender.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString()));
    }

    protected void btnPresent_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int InsId = -1;
         int TaskCode = -1;
       
        Int16 Status = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            if (!Utility.IsDBNullOrNullValue(row["PPId"]))
                      PPId = (int)row["PPId"];
            if (!Utility.IsDBNullOrNullValue(row["InsId"]))
            InsId = (int)row["InsId"];
            if (!Utility.IsDBNullOrNullValue(row["Status"]))
            Status = (Int16)row["Status"];
            if (!Utility.IsDBNullOrNullValue(row["TaskCode"]))
                TaskCode = (int)row["TaskCode"];
          
        }
        if (PPId == -1 )
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
         if (InsId != Utility.GetCurrentUser_MeId())
        {
            ShowMessage("امکان ثبت نمرات امتحانی برای شما وجود ندارد");
            return;
        }
         string GridFilterString = GridViewPeriods.FilterExpression;
         string SearchFilterString = GenerateFilterString();
         Response.Redirect("PeriodAttendanceAndTestMarks.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&TCode=" + Utility.EncryptQS(TaskCode.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString()));

    }

    protected void GridViewPeriods_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    GridViewPeriods.JSProperties["cpPrint"] = 1;
                    // grdCartable.DetailRows.CollapseAllRows();
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewPeriods.Columns;
                    Session["DataSource"] = OdbPeriod;
                    Session["Title"] = "دوره های آموزشی";
                    break;
                case "PrintAttender":
                    GridViewPeriods.JSProperties["cpPrint"] = 2;
                    int PPId = -1;
                    string GridFilterString = GridViewPeriods.FilterExpression;

                    if (GridViewPeriods.FocusedRowIndex > -1)
                    {
                        DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
                        PPId = (int)row["PPId"];
                    }
                    if (PPId == -1)
                    {
                        ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمایید");
                        return;
                    }
                    GridViewPeriods.JSProperties["cpURL"] = "../../ReportForms/Amoozesh/Periodattender.aspx?PPId=" + Utility.EncryptQS(PPId.ToString());
                    break;


            }
            //cpPrint
        }
        else
            GridViewPeriods.DataBind();
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        int PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                        int WfCode = (int)TSP.DataManager.WorkFlows.PeriodConfirming;
                        WFUserControl.PerformCallback(PPRId, TableType, WfCode, e);
                        GridViewPeriods.DataBind();
                    }
                }
            }
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    //protected void GridViewPeriods_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
    //        e.Cell.Style["direction"] = "ltr";

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewPeriods.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewPeriods.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            }

    //            if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            }
    //            else
    //            {
    //            }
    //        }
    //    }
    //}

    protected void GridViewPeriods_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewPeriods_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (!Utility.IsDBNullOrNullValue(e.GetValue("ReqConfirm")))
        {
            if (e.GetValue("ReqConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        int PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        int PPId = int.Parse(GridRequest.GetDataRow(index0)["PPId"].ToString());
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);

                        string GridFilterString = GridViewPeriods.FilterExpression;
                        String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPId.ToString());
                        Response.Redirect("../InstitueInfo/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                            + "&TblId=" + Utility.EncryptQS(PPRId.ToString())
                              + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    }
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int PPId = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            InActivePeriod(PPId);
            GridViewPeriods.DataBind();
        }
    }

    protected void GridViewRequest_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PPId"] = (sender as ASPxGridView).GetMasterRowFieldValues("PPId");
        int Index = GridViewPeriods.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewPeriods.FocusedRowIndex = Index;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        OdbPeriod.SelectParameters["InsId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        if (cmbCourse.SelectedItem != null && cmbCourse.SelectedItem.Value != null)
            OdbPeriod.SelectParameters["CrsId"].DefaultValue = cmbCourse.SelectedItem.Value.ToString();
        else
            OdbPeriod.SelectParameters["CrsId"].DefaultValue = "-1";
        if (!string.IsNullOrWhiteSpace(txtStartDateFrom.Text))
            OdbPeriod.SelectParameters["StartDateFrom"].DefaultValue = txtStartDateFrom.Text;
        else
            OdbPeriod.SelectParameters["StartDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrWhiteSpace(txtStartDateTo.Text))
            OdbPeriod.SelectParameters["StartDateTo"].DefaultValue = txtStartDateTo.Text;
        else
            OdbPeriod.SelectParameters["StartDateTo"].DefaultValue = "2";
        if (!string.IsNullOrWhiteSpace(txtEndDateFrom.Text))
            OdbPeriod.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbPeriod.SelectParameters["EndDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrWhiteSpace(txtEndDateTo.Text))
            OdbPeriod.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            OdbPeriod.SelectParameters["EndDateTo"].DefaultValue = "2";
        if (!string.IsNullOrWhiteSpace(txtTestDateFrom.Text))
            OdbPeriod.SelectParameters["TestDateFrom"].DefaultValue = txtTestDateFrom.Text;
        else
            OdbPeriod.SelectParameters["TestDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrWhiteSpace(txtTestDateTo.Text))
            OdbPeriod.SelectParameters["TestDateTo"].DefaultValue = txtTestDateTo.Text;
        else
            OdbPeriod.SelectParameters["TestDateTo"].DefaultValue = "2";

        if (!string.IsNullOrWhiteSpace(txtPPCode.Text))
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = txtPPCode.Text;
        else
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = "%";
        if (CmbTask.SelectedIndex != 0)
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = "-1";
        
        GridViewPeriods.DataBind();
    }
    #endregion

    #region Methods

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewPeriods.FilterExpression = GrdFlt;
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
            OdbPeriod.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    //case "InsId":

                    //    cmbInstitue.DataBind();
                    //    if (Value == "-1")
                    //    {
                    //        cmbInstitue.DataBind();
                    //        cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                    //        cmbInstitue.SelectedIndex = 0;
                    //    }
                    //    else
                    //    {
                    //        cmbInstitue.SelectedIndex = cmbInstitue.Items.FindByValue(Value).Index;
                    //    }
                    //    break;
                    case "CrsId":
                        cmbCourse.DataBind();
                        if (Value == "-1")
                        {
                            cmbCourse.DataBind();
                            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbCourse.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbCourse.SelectedIndex = cmbCourse.Items.FindByValue(Value).Index;
                        }
                        break;

                    case "StartDateFrom":
                        if (Value != "1")
                            txtStartDateFrom.Text = Value;
                        break;
                    case "StartDateTo":
                        if (Value != "2")
                            txtStartDateTo.Text = Value;
                        break;
                    case "EndDateFrom":
                        if (Value != "1")
                            txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        if (Value != "2")
                            txtEndDateTo.Text = Value;
                        break;
                    case "TestDateFrom":
                        if (Value != "1")
                            txtTestDateFrom.Text = Value;
                        break;
                    case "TestDateTo":
                        if (Value != "2")
                            txtTestDateTo.Text = Value;
                        break;
                    case "TaskId":
                        if (Value != "-1")
                        {
                            CmbTask.DataBind();
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "PPCode":
                        txtPPCode.Text = Value;
                        break;
                }
            }
        }
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < OdbPeriod.SelectParameters.Count; i++)
        {
            if (OdbPeriod.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += OdbPeriod.SelectParameters[i].Name + "&";
                FilterString += OdbPeriod.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {

                int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));

                GridViewPeriods.DataBind();
                Index = GridViewPeriods.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewPeriods.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewPeriods.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewPeriods.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }

    #region WorkflowMethods
    private bool CheckWorkFlowPermission(int PPRId)
    {
        int PermissionEdit = -1;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        PermissionEdit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, PPRId, TaskCode, Utility.GetCurrentUser_UserId());
        if (PermissionEdit > 0)
            return true;
        else
            return false;
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
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    private Boolean CheckPermissionForDeleteRequest(int TableId)
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        return (TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.PeriodConfirming, SaveTaskCode, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId));
    }

    private Boolean CheckPermissionForInActiveRequest(int TableId)
    {
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectPeriodAndEndProccess;
        int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmPeriodAndEndProccess;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        if (!WorkFlowPermission.CheckCurrentTaskCode(RejectTaskCode, TableType, TableId) && !WorkFlowPermission.CheckCurrentTaskCode(ConfirmTaskCode, TableType, TableId))
            return true;
        return false;

    }
    #endregion

    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    private void DeletePeriod(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TransactionManager.Add(PeriodPresentManager);
        TransactionManager.Add(WorkFlowStateManager);
        try
        {
            TransactionManager.BeginSave();
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            PeriodPresentManager[0].Delete();
            if (PeriodPresentManager.Save() <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
                return;
            }
            int WfCode = (int)TSP.DataManager.WorkFlows.PeriodConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, PPId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();
                WorkFlowStateManager.Save();
            }

            TransactionManager.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetDeleteError(err);
        }
    }

    private void InActivePeriod(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        try
        {
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            PeriodPresentManager[0].BeginEdit();
            PeriodPresentManager[0]["InActive"] = 1;
            PeriodPresentManager[0].EndEdit();
            if (PeriodPresentManager.Save() <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
                return;
            }

            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    void LoadWfHelpPeriodConfirm()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.PeriodConfirming));
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
            RepeaterWfHelp1.DataSource = dt1;
            RepeaterWfHelp1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHelp2.DataSource = dt2;
            RepeaterWfHelp2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHelp3.DataSource = dt3;
            RepeaterWfHelp3.DataBind();
        }
    }
    void LoadWfHelpPeriodPrint()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.PrindPeriodCertificates));
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
