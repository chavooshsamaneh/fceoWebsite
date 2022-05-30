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

public partial class Employee_Amoozesh_Periods : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCodeList"].DefaultValue = ((int)TSP.DataManager.WorkFlows.PeriodConfirming).ToString() + "," + ((int)TSP.DataManager.WorkFlows.PrindPeriodCertificates).ToString();

            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("-----------------", null));
            CmbTask.SelectedIndex = 0;
            LoadWfHelpPeriodConfirm();
            LoadWfHelpPeriodPrint();
            Session["SendBackDataTable_PP"] = null;
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            cmbInstitue.DataBind();
            cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            TSP.DataManager.Permission per = TSP.DataManager.PeriodPresentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSendSms.Enabled = btnSendSms2.Enabled = btnView.Enabled = btnView1.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            btnDelReq.Enabled = btnDelReq2.Enabled = per.CanDelete;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            btnChange.Enabled = btnChange2.Enabled = CheckWorkFlowPermissionForChangeReq();
            GridViewPeriods.Visible = per.CanView;

            this.ViewState["btnedit"] = btnEdit.Enabled;
            this.ViewState["btndel"] = btnDelReq.Enabled;
            this.ViewState["btnview"] = btnView.Enabled;
        }


        if (this.ViewState["btnedit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btndel"] != null)
            this.btnDelReq.Enabled = this.btnDelReq2.Enabled = (bool)this.ViewState["btndel"];
        if (this.ViewState["btnview"] != null)
            btnSendSms.Enabled = btnSendSms2.Enabled = this.btnView.Enabled = this.btnView1.Enabled = (bool)this.ViewState["btnview"];


        SetPageFilter();
        SetGridRowIndex();

        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtStartDateFrom = document.getElementById('" + txtStartDateFrom.ClientID + "').value;";
        script += "var txtStartDateTo = document.getElementById('" + txtStartDateTo.ClientID + "').value;";
        script += "var txtTestDateFrom = document.getElementById('" + txtTestDateFrom.ClientID + "').value;";
        script += "var txtTestDateTo = document.getElementById('" + txtTestDateTo.ClientID + "').value;";

        script += "if (txtPPCode.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && txtTestDateFrom=='' && txtTestDateTo=='' && cmbInstitue.GetSelectedIndex() == 0   && cmbCourse.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        CmbTask.SetSelectedIndex(0);
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
    }
    #region btnClick
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
            Response.Redirect("AddPeriods.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change")
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()), false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnPrintRequest_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        try
        {
            if (!CheckpermisionForNewRequestPrint())
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
            Response.Redirect("AddPeriods.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("PrintRequest")
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()), false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnDelReq_Click(object sender, EventArgs e)
    {
        int PPRId = -1;
        try
        {
            if (!CheckpermisionForDeleteRequestPrint())
            {
                return;
            }

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
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Institute)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "این درخواست در پرتال موسسه ثبت شده و توسط شما قابل ویرایش نمی باشد.";
                                return;
                            }
                            PeriodPresentRequestManager.DeletePeriodPresentRequestInfo(PPRId);
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
                            GridViewPeriods.DataBind();
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        try
        {
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
                            Response.Redirect("AddPeriods.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("View")
                                + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPRId.ToString()) + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()), false);
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
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int PPRId = -1;
        string GridFilterString = GridViewPeriods.FilterExpression;
        if (!CheckWorkFlowPermission(PPId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد");
        }

        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
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
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد");
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
                            if (Convert.ToInt32(PeriodPresentRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Institute)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "این درخواست در پرتال موسسه ثبت شده و توسط شما قابل ویرایش نمی باشد.";
                                return;
                            }
                            string SearchFilterString = GenerateFilterString();
                            Response.Redirect("AddPeriods.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                          + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPRId.ToString()) + "&PPRId=" + Utility.EncryptQS(PPRId.ToString()), false);
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
    protected void btnPeriodAttender_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewPeriods.FilterExpression;
        int PPId = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PPId"];
        }
        if (PPId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        string SearchFilterString = GenerateFilterString();
        Response.Redirect("PeriodAttender.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));


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
                        Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
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
    protected void btnTestSession_Click(object sender, EventArgs e)
    {
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
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        PPManager.FindByCode(PPId);
        if (string.Compare(PPManager[0]["TestDate"].ToString(), Utility.GetDateOfToday()) > 0)
        {
            SetMessage("امکان تنظیم صورت جلسه قبل از برگزاری آزمون وجود ندارد");
            return;
        }
        string PageMode = "";
        int TsId = -1;
        if (Convert.ToInt32(PPManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.StartTest)
            PageMode = "View";
        TSP.DataManager.TestSessionManager SessionManager = new TSP.DataManager.TestSessionManager();
        SessionManager.FindByPeriodCode(PPId);
        if (SessionManager.Count == 1)
        {
            TsId = int.Parse(SessionManager[0]["TsId"].ToString());
            if (PageMode != "View")
                PageMode = "Edit";
        }
        else if (PageMode != "View")
            PageMode = "New";

        Response.Redirect("TestSession.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&TsId=" + Utility.EncryptQS(TsId.ToString()) + "&PgM=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Periods";

        GridViewExporter.WriteXlsToResponse(true);
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
        if (CmbTask.SelectedIndex != 0)
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbPeriod.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtPPCode.Text))
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = txtPPCode.Text;
        else
            OdbPeriod.SelectParameters["PPCode"].DefaultValue = "%";

        GridViewPeriods.DataBind();
    }
    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            string SMSBody = "";
            string MobileNo = "";
            TSP.DataManager.UserType SMSUltId = TSP.DataManager.UserType.Member;
            int CurrentTaskCode = -1;
            int PPId = -1;
            string PeriodTitle = "";
            if (GridViewPeriods.FocusedRowIndex > -1)
            {
                DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
                PPId = (int)row["PPId"];
                PeriodTitle = row["PeriodTitle"].ToString();
                CurrentTaskCode = (int)row["TaskCode"];
            }
            if (PPId == -1)
            {
                SetMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            SMSUltId = TSP.DataManager.UserType.Member;
            if (CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByLearningExpert
                && CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.ConfirmPeriodAndEndProccess)
            {
                SetMessage("با توجه به مرحله گردش کار دوره آموزشی امکان ارسال پیامک برای شرکت کنندگان این دوره وجود ندارد.");
                return;
            }
            SetMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            switch (CurrentTaskCode)
            {
                case (int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByLearningExpert:

                    SMSBody = "شركت كننده محترم در " + PeriodTitle + " با توجه به ثبت نمرات دوره، لطفا جهت اطلاع از وضعيت نمره خود به پروفايل شخصي مراجعه فرماييد.";

                    break;
                case (int)TSP.DataManager.WorkFlowTask.ConfirmPeriodAndEndProccess:
                    SMSBody = "شركت كننده محترم در " + PeriodTitle + " به اطلاع مي‌رساند گواهينامه پايان دوره جهت اقدامات صدور، تمديد و يا ارتقاء پروانه اشتغال به كار مهندسي تاييد گرديد.";
                    break;
                default:
                    break;
            }
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            DataTable dtPeriodRegister = PeriodRegisterManager.SelectPeriodRegisterForPeriodsForSMS(PPId);
            if (dtPeriodRegister.Rows.Count == 0)
            {
                SetMessage("ثبت نام کننده ای جهت ارسال پیامک یافت نشد");
                return;
            }
            for (int i = 0; i < dtPeriodRegister.Rows.Count; i++)
            {
                MobileNo = dtPeriodRegister.Rows[0]["MobileNo"].ToString();
                if (!string.IsNullOrWhiteSpace(MobileNo))
                {
                    SendSMSNotification(Utility.Notifications.NotificationTypes.PeriodAttenderSendSMS, SMSBody, MobileNo, PPId.ToString(), SMSUltId);
                }
            }
            TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count != 1)
            {
                SetMessage("خطا در بروزرسانی وضعیت ارسال پیامک ایجاد شد");
                return;
            }
            PeriodPresentManager[0].BeginEdit();
            PeriodPresentManager[0]["IsSMSSent"] = 1;
            PeriodPresentManager[0]["SendSMSDate"] = Utility.GetDateOfToday();
            PeriodPresentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PeriodPresentManager[0]["ModifiedDate"] = DateTime.Now;
            PeriodPresentManager[0].EndEdit();
            PeriodPresentManager.Save();
            SetMessage("ارسال پیامک با موفقیت انجام شد");
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewPeriods.DataBind();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    #endregion
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
                        SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمایید");
                        return;
                    }
                    GridViewPeriods.JSProperties["cpURL"] = "../../ReportForms/Amoozesh/Periodattender.aspx?PPId=" + Utility.EncryptQS(PPId.ToString());
                    break;
            }
            //cpPrint
        }
        //else
        //    GridViewPeriods.DataBind();
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
                        int WfCode = int.Parse(GridRequest.GetDataRow(index0)["WorkFlowCode"].ToString());// (int)TSP.DataManager.WorkFlows.PeriodConfirming;
                        WFUserControl.PerformCallback(PPRId, TableType, WfCode, e);
                        GridViewPeriods.DataBind();
                    }
                }
            }
        }
        else
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
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
                    case "InsId":

                        cmbInstitue.DataBind();
                        if (Value == "-1")
                        {
                            cmbInstitue.DataBind();
                            cmbInstitue.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbInstitue.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbInstitue.SelectedIndex = cmbInstitue.Items.FindByValue(Value).Index;
                        }
                        break;
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
            if (OdbPeriod.SelectParameters[i].DefaultValue != "%" && OdbPeriod.SelectParameters[i].DefaultValue != "-1")
            {
                FilterString += OdbPeriod.SelectParameters[i].Name + "&";
                FilterString += OdbPeriod.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    #region WorkflowMethods
    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequestPrint()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCodePrindPeriodCertificatesSaveRequest = (int)TSP.DataManager.WorkFlowTask.PrindPeriodCertificatesSaveRequest;
        TSP.DataManager.WFPermission PerPrint = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCodePrindPeriodCertificatesSaveRequest, TableType, Utility.GetCurrentUser_UserId());
        if (!PerPrint.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForDeleteRequestPrint()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int TaskCodePrindPeriodCertificatesSaveRequest = (int)TSP.DataManager.WorkFlowTask.PrindPeriodCertificatesSaveRequest;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerPrint = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCodePrindPeriodCertificatesSaveRequest, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest && !PerPrint.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }
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
    #endregion
    private void SetMessage(string Msg)
    {

        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;

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

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId, TSP.DataManager.UserType SMSUltId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)SMSUltId).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
    #endregion
}
