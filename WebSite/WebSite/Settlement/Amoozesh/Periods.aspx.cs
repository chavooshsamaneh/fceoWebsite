using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DevExpress.Web;
public partial class Settlement_Amoozesh_Periods : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.PeriodConfirming).ToString();
            Session["SendBackDataTable_PP"] = null;
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            cmbInstitue.DataBind();
            cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
           
        }

   

        SetPageFilter();
        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtStartDateFrom = document.getElementById('" + txtStartDateFrom.ClientID + "').value;";
        script += "var txtStartDateTo = document.getElementById('" + txtStartDateTo.ClientID + "').value;";
        script += "var txtTestDateFrom = document.getElementById('" + txtTestDateFrom.ClientID + "').value;";
        script += "var txtTestDateTo = document.getElementById('" + txtTestDateTo.ClientID + "').value;";

        script += "if (txtPPCode.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && txtTestDateFrom=='' && txtTestDateTo=='' && cmbInstitue.GetSelectedIndex() == 0   && cmbCourse.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == -1) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        CmbTask.SetSelectedIndex(-1);
        txtPPCode.SetText('');
        cmbInstitue.SetSelectedIndex(0);
        cmbCourse.SetSelectedIndex(0);
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtStartDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtStartDateTo.ClientID + @"').value='';
        document.getElementById('" + txtTestDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtTestDateTo.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        LoadWfHelpPeriodConfirm();
    }

    protected void GridViewPeriods_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
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
                        SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمایید");
                        return;
                    }
                  GridViewPeriods.JSProperties["cpURL"]="../../ReportForms/Amoozesh/Periodattender.aspx?PPId=" + Utility.EncryptQS(PPId.ToString());
                    break;
            }
            //cpPrint
        }
        else
            GridViewPeriods.DataBind();
    }

    protected void GridViewPeriods_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "TestDate")
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
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);

                        string GridFilterString = GridViewPeriods.FilterExpression;
                        String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPRId.ToString());
                        Response.Redirect("../MemberDocument/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
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

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PPId"] = (sender as ASPxGridView).GetMasterRowFieldValues("PPId");
        int Index = GridViewPeriods.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewPeriods.FocusedRowIndex = Index;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (cmbInstitue.SelectedItem != null && cmbInstitue.SelectedItem.Value != null)
            OdbPeriod.SelectParameters["InsId"].DefaultValue = cmbInstitue.SelectedItem.Value.ToString();
        else
            OdbPeriod.SelectParameters["InsId"].DefaultValue = "-1";
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
        if (CmbTask.SelectedIndex != -1)
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtPPCode.Text))
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = txtPPCode.Text;
        else
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = "%";

        GridViewPeriods.DataBind();
    }
    #endregion

    #region Methods
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewPeriods.FilterExpression = GrdFlt;
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

    //#region WorkflowMethods
    ///// <summary>
    ///// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    ///// </summary>
    ///// <returns></returns>
    //private Boolean CheckWorkFlowPermissionForChangeReq()
    //{
    //    int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
    //    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
    //    TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
    //    return (WFPer.BtnNewRequest);
    //}

    ///// <summary>
    ///// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    ///// </summary>
    ///// <returns></returns>
    //private Boolean CheckpermisionForNewRequest()
    //{
    //    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
    //    TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
    //    if (!Per.BtnNewRequest)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
    //        return false;
    //    }
    //    return true;
    //}

    //private bool CheckWorkFlowPermission(int PPRId)
    //{
    //    int PermissionEdit = -1;
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
    //    PermissionEdit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, PPRId, TaskCode, Utility.GetCurrentUser_UserId());
    //    if (PermissionEdit > 0)
    //        return true;
    //    else
    //        return false;
    //}
    //#endregion
    private void SetMessage(string Msg)
    {

        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;

    }

    void LoadWfHelpPeriodConfirm()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.PeriodConfirming));
        dt.DefaultView.RowFilter = "TaskOrder<>0";
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        DataTable dt4 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 4 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 4 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 4 == 2) { dt3.Rows.Add(dr.ItemArray); }
            if (count % 4 == 3) { dt4.Rows.Add(dr.ItemArray); }
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
    #endregion
}