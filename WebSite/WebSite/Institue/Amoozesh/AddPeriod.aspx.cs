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

public partial class Institue_Amoozesh_AddPeriod : System.Web.UI.Page
{
    DataTable dtOfPP = new DataTable();
    DataTable dtSchedule = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        //this.DivReport.Visible = false;
        //this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        //this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Period.aspx");
                return;
            }
            Session["IsEdited_Period"] = false;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
                PeriodRequestId.Value = Server.HtmlDecode(Request.QueryString["PPRId"]).ToString();
                InstitueId.Value = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("Period.aspx");
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PPId = Utility.DecryptQS(PeriodId.Value);
            string PPRId = Utility.DecryptQS(PeriodRequestId.Value);


            Session["TeacherUpload"] = null;
            Session["PPTeacher"] = null;
            Session["Schedule"] = null;

            #region Make DataTables
            if (Session["PPTeacher"] == null)
            {
                CreateDataTableTeacher();
            }
            else
                dtOfPP = (DataTable)Session["PPTeacher"];

            GridViewTeacher.DataSource = dtOfPP;
            GridViewTeacher.DataBind();


            if (Session["Schedule"] == null)
            {
                CreateDataTableSchedule();
            }
            else
                dtSchedule = (DataTable)Session["Schedule"];

            GridViewSchedule.DataSource = dtSchedule;
            GridViewSchedule.DataBind();
            #endregion

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            if (string.IsNullOrEmpty(PPRId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            SetMode(PageMode, int.Parse(PPRId));
            CheckWorkFlowPermission();
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        PeriodId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnAddTeacher.ClientVisible = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        AspxMenu1.Enabled = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        int PPRId = Convert.ToInt32(Utility.DecryptQS(PeriodRequestId.Value));

        if (Utility.IsDBNullOrNullValue(PPRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
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
                this.LabelWarning.Text = "این درخواست در پرتال کارمند ثبت شده و توسط شما قابل ویرایش نمی باشد.";
                return;
            }
            Enable();

            btnAddTeacher.ClientVisible = true;
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";

            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            FillFormForRequest(PPRId);
            ASPxRoundPanel2.Enabled = true;
            ASPxRoundPanel2.HeaderText = "ویرایش";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (GridViewTeacher.VisibleRowCount == 0)
        {
            ShowMessage("اطلاعات برای ذخیره کافی نمی باشد.مدرس دوره را وارد نمایید");
            return;
        }
        if (Utility.IsAmoozeshConditionChecked() && GridViewSchedule.VisibleRowCount == 0)
        {
            ShowMessage("اطلاعات برای ذخیره کافی نمی باشد.زمان برگزاری جلسات را وارد نمایید");
            return;
        }
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PPId = Utility.DecryptQS(PeriodId.Value);
        string PPRId = Utility.DecryptQS(PeriodRequestId.Value);

        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PPId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                EditRequest(int.Parse(PPRId));
                break;
            case "Change":
                InsertNewRequest(TSP.DataManager.PeriodPresentRequestType.Change);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["PPTeacher"] = null;
        Session["Schedule"] = null;
        Session["TeacherUpload"] = null;
        Session["PPGrade"] = null;
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
       && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("Period.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Period.aspx");
        }

    }

    protected void OdbSchedule_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }

    protected void OdbSchedule_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;

    }

    protected void OdbSchedule_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;

    }

    protected void GridViewSchedule_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["Schedule"] == null)
        {
            CreateDataTableSchedule();
        }
        dtSchedule = (DataTable)Session["Schedule"];

        try
        {
            DataRow dr = dtSchedule.NewRow();
            PersianDateControls.PersianDateTextBox calender = (PersianDateControls.PersianDateTextBox)GridViewSchedule.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSchedule.Columns["Date"], "txtDate");
            if (e.NewValues["Number"] != null)
                dr["Number"] = e.NewValues["Number"];
            else
                dr["Number"] = DBNull.Value;
            dr["Date"] = calender.Text;
            dr["StartTime"] = e.NewValues["StartTime"];
            dr["EndTime"] = e.NewValues["EndTime"];
            dr["Description"] = e.NewValues["Description"];
            dr["Type"] = 0;//period
            dtSchedule.Rows.Add(dr);
            Session["Schedule"] = dtSchedule;
            GridViewSchedule.CancelEdit();
            GridViewSchedule.DataSource = dtSchedule;
            GridViewSchedule.DataBind();
        }
        catch (Exception err)
        {
            GridViewSchedule.CancelEdit();
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در اضافه کردن رخ داده است");
        }

        e.Cancel = true;
    }

    protected void GridViewSchedule_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["Schedule"] != null)
        {
            dtSchedule = (DataTable)Session["Schedule"];
            try
            {
                DataRow dr = dtSchedule.Rows.Find(e.Keys[0]);
                if (!Utility.IsDBNullOrNullValue(dr["SchId"]))
                    if (!IsInCurrentRequestSchedule(Convert.ToInt32(dr["SchId"])))
                    {
                        ShowCallBackMessage(2, "امکان ویرایش زمان بندی مربوط به درخواست های قبل وجود ندارد");
                        GridViewSchedule.CancelEdit();
                        return;
                    }

                dr.BeginEdit();
                if (e.NewValues["Number"] != null)
                    dr["Number"] = e.NewValues["Number"];
                else
                    dr["Number"] = DBNull.Value;
                dr["Date"] = e.NewValues["Date"];
                dr["StartTime"] = e.NewValues["StartTime"];
                dr["EndTime"] = e.NewValues["EndTime"];
                dr["Description"] = e.NewValues["Description"];
                dr.EndEdit();
                Session["Schedule"] = dtSchedule;

                GridViewSchedule.CancelEdit();
                GridViewSchedule.DataSource = dtSchedule;
                GridViewSchedule.DataBind();
            }
            catch (Exception err)
            {
                GridViewSchedule.CancelEdit();
                Utility.SaveWebsiteError(err);
                ShowCallBackMessage(2, "خطایی در اضافه کردن رخ داده است");
            }
        }
        e.Cancel = true;

    }

    protected void GridViewSchedule_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewSchedule.DataSource = (DataTable)Session["Schedule"];
        GridViewSchedule.DataBind();

        int Id = -1;
        if (GridViewSchedule.FocusedRowIndex > -1)
        {
            Id = GridViewSchedule.FocusedRowIndex;
        }
        if (Id == -1)
        {
            ShowCallBackMessage(2, "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
            return;
        }
        else
        {
            dtSchedule = (DataTable)Session["Schedule"];

            DataRow dr = dtSchedule.Rows.Find(e.Keys[0]);
            if (!Utility.IsDBNullOrNullValue(dr["SchId"]))
                if (!IsInCurrentRequestSchedule(Convert.ToInt32(dr["SchId"])))
                {
                    ShowCallBackMessage(2, "امکان حذف زمان بندی مربوط به درخواست های قبل وجود ندارد");
                    GridViewSchedule.CancelEdit();
                    return;
                }

            dtSchedule.Rows.Find(e.Keys[0]).Delete();
            Session["Schedule"] = dtSchedule;
            GridViewSchedule.DataSource = (DataTable)Session["Schedule"];
            GridViewSchedule.DataBind();
            dtSchedule = (DataTable)Session["Schedule"];
        }
    }

    protected void GridViewSchedule_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PersianDateControls.PersianDateTextBox calender = (PersianDateControls.PersianDateTextBox)GridViewSchedule.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSchedule.Columns["Date"], "txtDate");
        if (string.IsNullOrEmpty(calender.Text))
        {
            e.RowError = "فیلد تاریخ را وارد نمایید";
        }

        if (string.Compare(calender.Text, txtStartDate.Text) < 0)
        {
            e.RowError = "تاریخ جلسات نمی توانید قبل از شروع دوره باشد";
        }
        if (string.Compare(calender.Text, txtEndDate.Text) > 0)
        {
            e.RowError = "تاریخ جلسات نمی توانید بعد از پایان دوره باشد";
        }
    }

    protected void GridViewSchedule_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (PeriodRequestId.Value != null)
        {
            string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
            if (e.GetValue("PPRId") == null)
                return;
            string CurretnPPRId = e.GetValue("PPRId").ToString();
            if (PPRId == CurretnPPRId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void cmbTeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.TeachersSalaryManager SalaryManager = new TSP.DataManager.TeachersSalaryManager();

        try
        {
            int TeId = int.Parse(cmbTeId.Value.ToString());
            TeManager.FindByCode(TeId);
            if (TeManager.Count > 0)
            {
                if (string.IsNullOrEmpty(TeManager[0]["MeId"].ToString()))
                {
                    txtTMeId.Text = "";
                    txtTPaye.Text = "";
                    //lblMeId.Enabled = false;
                    //lblPaye.Enabled = false;
                    txtTPaye.Enabled = false;
                    txtTMeId.Enabled = false;
                    this.lblInsError.Visible = true;
                    this.lblInsError.Text = "استاد مورد نظر عضو حقیقی نظام مهندسی نمی باشد";
                }
                else
                {
                    //lblMeId.Enabled = true;
                    //lblPaye.Enabled = true;
                    txtTPaye.Enabled = true;
                    txtTMeId.Enabled = true;
                    this.lblInsError.Visible = false;
                    txtTMeId.Text = TeManager[0]["MeId"].ToString();
                    //txtTPaye.Text=?????????

                }
                int LiId = int.Parse(TeManager[0]["LiId"].ToString());
                DataTable dtSalary = SalaryManager.SelectByLicence(LiId);
                if (dtSalary.Rows.Count > 0)
                {
                    txtSalNonpractical.Text = dtSalary.Rows[0]["SalaryNonPractical"].ToString();
                    txtSalPractical.Text = dtSalary.Rows[0]["SalaryPractical"].ToString();
                    txtSalWorkroom.Text = dtSalary.Rows[0]["SalaryWorkroom"].ToString();
                }
                else
                {
                    txtSalNonpractical.Text = "";
                    txtSalPractical.Text = "";
                    txtSalWorkroom.Text = "";
                }

                txtTFileNo.Text = TeManager[0]["FileNo"].ToString();
                txtTLicence.Text = TeManager[0]["LiName"].ToString();
                txtTMajor.Text = TeManager[0]["MjName"].ToString();
                txtTTeName.Text = TeManager[0]["TeName"].ToString();
            }
            else
            {
                ShowMessage("امکان مشاهده اطلاعات وجود ندارد");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void cmbCrsId_SelectedIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.CourseManager CrsManager = new TSP.DataManager.CourseManager();
        try
        {
            int CrsId = int.Parse(cmbCrsId.Value.ToString());
            CrsManager.FindByCode(CrsId);
            if (CrsManager.Count > 0)
            {
                txtPDuration.Text = CrsManager[0]["Duration"].ToString();
                txtPoint.Text = CrsManager[0]["Point"].ToString();
                //txtPTypeName.Text = CrsManager[0]["TypeName"].ToString();
                txtValidDuration.Text = CrsManager[0]["ValidDuration"].ToString();
                txtbPracticalDuration.Text = CrsManager[0]["PracticalDuration"].ToString();
                txtbWorkroomDuration.Text = CrsManager[0]["WorkroomDuration"].ToString();
                txtbNonPracticalDuration.Text = CrsManager[0]["NonPracticalDuration"].ToString();
                if (!Utility.IsDBNullOrNullValue(CrsManager[0]["CrsCode"]))
                    txtCrsCode.Text = CrsManager[0]["CrsCode"].ToString();

            }
            else
            {
                ShowMessage("امکان مشاهده اطلاعات وجود ندارد");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void txtCrsCode_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.CourseManager CrsManager = new TSP.DataManager.CourseManager();
        try
        {
            // int CrsId = int.Parse(cmbCrsId.Value.ToString());
            CrsManager.FindByCrsCode(txtCrsCode.Text.Trim());
            if (CrsManager.Count > 0)
            {
                cmbCrsId.DataBind();
                cmbCrsId.SelectedIndex = cmbCrsId.Items.FindByValue(CrsManager[0]["CrsId"].ToString()).Index;
                txtPDuration.Text = CrsManager[0]["Duration"].ToString();
                txtPoint.Text = CrsManager[0]["Point"].ToString();
                //txtPTypeName.Text = CrsManager[0]["TypeName"].ToString();
                txtValidDuration.Text = CrsManager[0]["ValidDuration"].ToString();
                txtbPracticalDuration.Text = CrsManager[0]["PracticalDuration"].ToString();
                txtbWorkroomDuration.Text = CrsManager[0]["WorkroomDuration"].ToString();
                txtbNonPracticalDuration.Text = CrsManager[0]["NonPracticalDuration"].ToString();

            }
            else
            {
                ShowMessage("اطلاعاتی برای کد درس وارد شده یافت نشد");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void btnAddTeacher_Click(object sender, EventArgs e)
    {
        if (Session["PPTeacher"] == null)
        {
            CreateDataTableTeacher();
        }
        dtOfPP = (DataTable)Session["PPTeacher"];

        DataRow dr = dtOfPP.NewRow();

        try
        {
            if (dtOfPP.Rows.Count > 0)
            {
                for (int i = 0; i < dtOfPP.Rows.Count; i++)
                {
                    if (dtOfPP.Rows[i].RowState == DataRowState.Deleted) continue;
                    if (dtOfPP.Rows[i]["TeId"].ToString() == cmbTeId.Value.ToString())
                    {
                        ShowMessage("استاد مورد نظر قبلاً انتخاب شده است");
                        return;
                    }
                }
            }

            if (cmbTeId.Value != null)
                dr["TeId"] = cmbTeId.Value.ToString();
            if (!string.IsNullOrEmpty(txtHoPractical.Text))
                dr["PracticalHour"] = Convert.ToInt16(txtHoPractical.Text);
            else
                dr["PracticalHour"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtHoNonPractical.Text))
                dr["NonPracticalHour"] = Convert.ToInt16(txtHoNonPractical.Text);
            else
                dr["NonPracticalHour"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtHoWorkroom.Text))
                dr["WorkroomHour"] = Convert.ToInt16(txtHoWorkroom.Text);
            else
                dr["WorkroomHour"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtSalPractical.Text))
                dr["PracticalSalary"] = txtSalPractical.Text;
            else
                dr["PracticalSalary"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtSalNonpractical.Text))
                dr["NonPracticalSalary"] = txtSalNonpractical.Text;
            else
                dr["NonPracticalSalary"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtSalWorkroom.Text))
                dr["WorkroomSalary"] = txtSalWorkroom.Text;
            else
                dr["WorkroomSalary"] = DBNull.Value;

            dr["Description"] = txtTeDesc.Text;
            dr["MjName"] = txtTMajor.Text;
            dr["LiName"] = txtTLicence.Text;
            //string[] split = cmbTeId.Text.Split(new Char[] { ';' });
            dr["TeName"] = cmbTeId.Text;// split[1];
            dr["MeId"] = txtTMeId.Text;
            dr["FileNo"] = txtTFileNo.Text;

            dtOfPP.Rows.Add(dr);
            GridViewTeacher.DataSource = dtOfPP;
            GridViewTeacher.DataBind();

            ClearTeacherForm();
            txtTeDesc.Text = "";
            cmbTeId.DataBind();
            cmbTeId.SelectedIndex = -1;
            txtTPaye.Enabled = true;
            txtTMeId.Enabled = true;
            this.lblInsError.Visible = false;
        }
        catch
        {
            ShowMessage("خطایی در اضافه کردن رخ داده است");
        }

    }

    protected void GridViewTeacher_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewTeacher.DataSource = (DataTable)Session["PPTeacher"];
        GridViewTeacher.DataBind();

        int Id = -1;
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            Id = GridViewTeacher.FocusedRowIndex;
        }
        if (Id == -1)
        {
            ShowCallBackMessage(1, "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
            return;
        }
        else
        {
            dtOfPP = (DataTable)Session["PPTeacher"];

            DataRow dr = dtOfPP.Rows.Find(e.Keys[0]);
            if (!Utility.IsDBNullOrNullValue(dr["TrTeId"]))
                if (!IsInCurrentRequestTeacher(Convert.ToInt32(dr["TrTeId"])))
                {
                    ShowCallBackMessage(1, "امکان حذف مدرس مربوط به درخواست های قبل وجود ندارد");
                    GridViewTeacher.CancelEdit();
                    return;
                }

            dtOfPP.Rows.Find(e.Keys[0]).Delete();
            Session["PPTeacher"] = dtOfPP;
            GridViewTeacher.DataSource = (DataTable)Session["PPTeacher"];
            GridViewTeacher.DataBind();
            dtOfPP = (DataTable)Session["PPTeacher"];
        }
    }

    protected void GridViewTeacher_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["PPTeacher"] != null)
        {
            dtOfPP = (DataTable)Session["PPTeacher"];

            try
            {
                DataRow dr = dtOfPP.Rows.Find(e.Keys[0]);
                if (!Utility.IsDBNullOrNullValue(dr["TrTeId"]))
                    if (!IsInCurrentRequestTeacher(Convert.ToInt32(dr["TrTeId"])))
                    {
                        ShowCallBackMessage(1, "امکان حذف مدرس مربوط به درخواست های قبل وجود ندارد");
                        GridViewTeacher.CancelEdit();
                        return;
                    }

                dr.BeginEdit();
                dr["PracticalHour"] = e.NewValues["PracticalHour"];
                dr["NonPracticalHour"] = e.NewValues["NonPracticalHour"];
                dr["WorkroomHour"] = e.NewValues["WorkroomHour"];
                dr["PracticalSalary"] = e.NewValues["PracticalSalary"];
                dr["NonPracticalSalary"] = e.NewValues["NonPracticalSalary"];
                dr["WorkroomSalary"] = e.NewValues["WorkroomSalary"];
                dr.EndEdit();
                GridViewTeacher.CancelEdit();
                GridViewTeacher.DataSource = dtOfPP;
                GridViewTeacher.DataBind();
            }
            catch (Exception err)
            {
                GridViewSchedule.CancelEdit();
                Utility.SaveWebsiteError(err);
                ShowCallBackMessage(1, "خطایی در اضافه کردن رخ داده است");
            }
        }

        e.Cancel = true;
    }

    protected void GridViewTeacher_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (PeriodRequestId.Value != null)
        {
            string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
            if (e.GetValue("PPRId") == null)
                return;
            string CurretnPPRId = e.GetValue("PPRId").ToString();
            if (PPRId == CurretnPPRId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        if (string.IsNullOrEmpty(PPId.ToString()))
        {
            Response.Redirect("Period.aspx");
        }


        switch (e.Item.Name)
        {
            case "Costs":
                Response.Redirect("PeriodCosts.aspx?PPId=" + PeriodId.Value + "&PPRId=" + PeriodRequestId.Value + "&PageMode=" + PgMode.Value);
                break;
            case "TestMarks":

                TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
                PPManager.FindByCode(PPId);
                if (Convert.ToInt32(PPManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.StartTest || Convert.ToInt32(PPManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection)
                {
                    Response.Redirect("PeriodTestMarks.aspx?PPId=" + PeriodId.Value + "&PPRId=" + PeriodRequestId.Value + "&PageMode=" + PgMode.Value);
                }
                else
                {
                    ShowMessage("امکان ثبت نمرات آزمون در این وضعیت از دوره وجود ندارد");
                    return;
                }

                break;
            case "InValid":
                if (!string.IsNullOrEmpty(PeriodId.Value))
                {
                    int PSCId = -1;
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);

                    if (!IsInActive(PPId))
                    {
                        Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.DecryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("New") + "&PageMode=" + PgMode.Value + "&InsId=" + Request.QueryString["InsId"]);
                    }
                    else
                    {
                        TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();

                        TrainingStatusChangeManager.FindByTableType(TableType, PPId, 0);
                        if (TrainingStatusChangeManager.Count > 0)
                        {
                            PSCId = int.Parse(TrainingStatusChangeManager[0]["PSCId"].ToString());
                            Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.EncryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "&PageMode=" + PgMode.Value + "&InsId=" + Request.QueryString["InsId"]);
                        }
                    }
                }
                break;

        }
    }

    #endregion

    #region Methods
    #region Insert Update
    protected void Insert()
    {
        if (Session["PPTeacher"] == null)
        {
            ShowMessage("برای ثبت دوره انتخاب مدرسان دوره الزامی می باشد");
            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.PeriodOpinionManager OpinionManager = new TSP.DataManager.PeriodOpinionManager();
        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();
        TSP.DataManager.PeriodPresentRequestManager ppRequestManager = new TSP.DataManager.PeriodPresentRequestManager();

        trans.Add(PeriodManager);
        trans.Add(SchManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OpinionManager);
        trans.Add(PPTeManager);
        trans.Add(ppRequestManager);

        try
        {
            string InsId = Utility.DecryptQS(InstitueId.Value);
            trans.BeginSave();
            DataRow drP = PeriodManager.NewRow();
            drP["PPCode"] = txtPPCode.Text;
            drP["InsId"] = InsId;
            drP["CrsId"] = cmbCrsId.Value;
            drP["StartDate"] = txtStartDate.Text;
            drP["EndDate"] = txtEndDate.Text;
            drP["Place"] = txtPlace.Text;
            drP["Description"] = txtDesc.Text;
            drP["Capacity"] = txtCapacity.Text;
            //******
            drP["StartRegisterDate"] = txtStartRegisterDate.Text;
            drP["EndRegisterDate"] = txtEndRegisterDate.Text;
            //******
            drP["CreateDate"] = Utility.GetDateOfToday();
            drP["TestDate"] = txtTestDate.Text;
            if (ComboPPType.Value != null)
                drP["PeriodType"] = ComboPPType.Value;
            else
                drP["PeriodType"] = 0;

            if (cmbPoll.Value != null)
                drP["PollId"] = cmbPoll.Value;
            else
                drP["PollId"] = DBNull.Value;

            drP["TestHour"] = txtTestHour.Text;
            drP["TestPlace"] = txtTestPlace.Text;
            drP["PeriodCost"] = txtPeriodCost.Text;
            if (!string.IsNullOrEmpty(txtTestCost.Text))
                drP["TestCost"] = txtTestCost.Text;
            else
                drP["TestCost"] = DBNull.Value;            
            drP["Discount"] = DBNull.Value;
            drP["Status"] = (int)TSP.DataManager.PeriodPresentStatus.Inserting;
            drP["InActive"] = 0;
            drP["UserId"] = Utility.GetCurrentUser_UserId();
            drP["ModifiedDate"] = DateTime.Now;

            PeriodManager.AddRow(drP);
            int cnt = PeriodManager.Save();
            if (cnt > 0)
            {
                int TableId = int.Parse(PeriodManager[0]["PPId"].ToString());
                int Status = int.Parse(PeriodManager[0]["Status"].ToString());

                if (!InsertPeriodPresentRequest(ppRequestManager, TSP.DataManager.PeriodPresentRequestType.SaveInfo, 0, TableId, Status))
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }

                int PPRId = Convert.ToInt32(ppRequestManager[0]["PPRId"]);
                #region Teachers
                Int16 HoPractical = 0;
                Int16 HoNonPractical = 0;
                Int16 HoWorkroom = 0;
                dtOfPP = (DataTable)Session["PPTeacher"];
                if (dtOfPP.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfPP.DefaultView.Count; i++)
                    {
                        if (dtOfPP.Rows[i].RowState == DataRowState.Deleted) continue;
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["PracticalHour"].ToString()))
                            HoPractical += Int16.Parse(dtOfPP.Rows[i]["PracticalHour"].ToString());
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["NonPracticalHour"].ToString()))
                            HoNonPractical += Int16.Parse(dtOfPP.Rows[i]["NonPracticalHour"].ToString());
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["WorkroomHour"].ToString()))
                            HoWorkroom += Int16.Parse(dtOfPP.Rows[i]["WorkroomHour"].ToString());
                    }
                    if (HoPractical != Int16.Parse(txtbPracticalDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات عملی تعریف شده برای استاد با تعداد ساعات عملی دوره برابر نمی باشد");
                        return;
                    }
                    if (HoNonPractical != Int16.Parse(txtbNonPracticalDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات تئوری تعریف شده  برای  استاد با تعداد ساعات تئوری دوره برابر نمی باشد");
                        return;
                    }
                    if (HoWorkroom != Int16.Parse(txtbWorkroomDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات بازدید از کارگاه تعریف شده برای استاد با تعداد ساعات بازدید از کارگاه دوره برابر نمی باشد");
                        return;
                    }
                    for (int i = 0; i < dtOfPP.DefaultView.Count; i++)
                    {
                        if (dtOfPP.Rows[i].RowState == DataRowState.Deleted) continue;
                        DataRow drTe = PPTeManager.NewRow();
                        drTe["PkId"] = TableId;
                        drTe["TeId"] = dtOfPP.Rows[i]["TeId"];
                        drTe["PPRId"] = PPRId;
                        drTe["PracticalHour"] = dtOfPP.Rows[i]["PracticalHour"];
                        drTe["NonPracticalHour"] = dtOfPP.Rows[i]["NonPracticalHour"];
                        drTe["WorkroomHour"] = dtOfPP.Rows[i]["WorkroomHour"];
                        drTe["PracticalSalary"] = dtOfPP.Rows[i]["PracticalSalary"];
                        drTe["NonPracticalSalary"] = dtOfPP.Rows[i]["NonPracticalSalary"];
                        drTe["WorkroomSalary"] = dtOfPP.Rows[i]["WorkroomSalary"];
                        drTe["Description"] = dtOfPP.Rows[i]["Description"].ToString();
                        drTe["UserId"] = Utility.GetCurrentUser_UserId();
                        drTe["Type"] = 0;//period
                        drTe["InActive"] = 0;
                        drTe["ModifiedDate"] = DateTime.Now;
                        PPTeManager.AddRow(drTe);
                        int Tecnt = PPTeManager.Save();
                        PPTeManager.DataTable.AcceptChanges();
                        if (Tecnt == 1)
                        {
                            dtOfPP.Rows[i].BeginEdit();
                            dtOfPP.Rows[i]["TrTeId"] = PPTeManager[PPTeManager.Count - 1]["TrTeId"].ToString();
                            dtOfPP.Rows[i].EndEdit();
                        }
                    }
                }
                #endregion

                #region Schedule
                if (Session["Schedule"] != null)
                {
                    dtSchedule = (DataTable)Session["Schedule"];
                    if (dtSchedule.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSchedule.Rows.Count; i++)
                        {
                            if (dtSchedule.Rows[i].RowState == DataRowState.Deleted) continue;
                            DataRow drSc = SchManager.NewRow();
                            drSc["TtId"] = TableId;
                            drSc["PPRId"] = PPRId;
                            drSc["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                            drSc["Number"] = dtSchedule.Rows[i]["Number"];
                            drSc["Date"] = dtSchedule.Rows[i]["Date"];
                            drSc["StartTime"] = dtSchedule.Rows[i]["StartTime"];
                            drSc["EndTime"] = dtSchedule.Rows[i]["EndTime"];
                            drSc["Type"] = dtSchedule.Rows[i]["Type"];
                            drSc["Description"] = dtSchedule.Rows[i]["Description"].ToString();
                            drSc["UserId"] = Utility.GetCurrentUser_UserId();
                            drSc["ModifiedDate"] = DateTime.Now;
                            SchManager.AddRow(drSc);
                            int Tecnt = SchManager.Save();
                            SchManager.DataTable.AcceptChanges();
                            if (Tecnt == 1)
                            {
                                dtSchedule.Rows[i].BeginEdit();
                                dtSchedule.Rows[i]["SchId"] = SchManager[SchManager.Count - 1]["SchId"].ToString();
                                dtSchedule.Rows[i].EndEdit();
                            }
                        }
                    }
                }
                #endregion

                #region WorkFlow
                ////*****Check is User In TaskDoer*****
                TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
                TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    TaskDoerManager.FindByTaskId(TaskId);
                    if (TaskDoerManager.Count > 0)
                    {
                        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                        int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                        if (NmcId > -1)
                        {
                            int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(PPRId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0, "آغاز جریان کار دوره های آموزشی");
                            if (StartWorkFlow < 0)
                            {
                                trans.CancelSave();
                                ShowMessage("خطایی در ذخیره انجام گرفته است");
                            }
                        }
                        else
                        {
                            trans.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.UserNotExistInNezamChart));
                        }
                    }
                    else
                    {
                        trans.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
                #endregion

                #region Opinion
                DataTable dtSet = SetManager.SelectActiveQuestionSet();
                DataRow drOp = OpinionManager.NewRow();
                drOp["PeriodId"] = TableId;
                drOp["PeriodReqId"] = PPRId;
                drOp["QuCode"] = dtSet.Rows[0]["QuCode"];
                drOp["StartDate"] = PeriodManager[0]["EndDate"];
                drOp["ExpiredDate"] = PeriodManager[0]["TestDate"];
                drOp["UserId"] = Utility.GetCurrentUser_UserId();
                drOp["ModifiedDate"] = DateTime.Now;
                OpinionManager.AddRow(drOp);
                OpinionManager.Save();

                #endregion

                trans.EndSave();

                PeriodRequestId.Value = Utility.EncryptQS(PPRId.ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                Session["IsEdited_Period"] = true;
                ShowMessage("ذخیره انجام شد");
                AspxMenu1.Enabled = true;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }
            else
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else if (se.Number == 2627)
                {
                    ShowMessage("کد دوره تکراری می باشد");
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
    }

    protected void EditRequest(int PPRId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
        TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        trans.Add(SchManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(PPTeManager);
        trans.Add(PeriodPresentRequestManager);
        trans.Add(LetterRelatedTablesManager);
        trans.Add(PeriodManager);
        try
        {
            PeriodPresentRequestManager.FindByCode(PPRId);
            if (PeriodPresentRequestManager.Count == 1)
            {
                int PPId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPId"]);
                int Type = Convert.ToInt32(PeriodPresentRequestManager[0]["Type"]);

                trans.BeginSave();
                if (Type == (int)TSP.DataManager.PeriodPresentRequestType.SaveInfo)
                {
                    #region EditPeriod
                    PeriodManager.FindByCode(PPId);
                    if (PeriodManager.Count == 1)
                    {
                        PeriodManager[0].BeginEdit();
                        PeriodManager[0]["PPCode"] = txtPPCode.Text;
                        PeriodManager[0]["CrsId"] = cmbCrsId.Value;
                        PeriodManager[0]["StartDate"] = txtStartDate.Text;
                        PeriodManager[0]["EndDate"] = txtEndDate.Text;
                        PeriodManager[0]["Place"] = txtPlace.Text;
                        PeriodManager[0]["Description"] = txtDesc.Text;
                        PeriodManager[0]["Capacity"] = txtCapacity.Text;
                        //********
                        PeriodManager[0]["StartRegisterDate"] = txtStartRegisterDate.Text;
                        PeriodManager[0]["EndRegisterDate"] = txtEndRegisterDate.Text;
                        //********
                        if (ComboPPType.Value != null)
                            PeriodManager[0]["PeriodType"] = ComboPPType.Value;

                        if (cmbPoll.Value != null)
                            PeriodManager[0]["PollId"] = cmbPoll.Value;


                        PeriodManager[0]["TestDate"] = txtTestDate.Text;
                        PeriodManager[0]["TestHour"] = txtTestHour.Text;
                        PeriodManager[0]["TestPlace"] = txtTestPlace.Text;
                        if (!string.IsNullOrEmpty(txtTestCost.Text))
                            PeriodManager[0]["TestCost"] = txtTestCost.Text;
                        else
                            PeriodManager[0]["TestCost"] = DBNull.Value;

                        PeriodManager[0]["PeriodCost"] = txtPeriodCost.Text;
                        PeriodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        PeriodManager[0].EndEdit();
                        if (PeriodManager.Save() <= 0)
                        {
                            trans.CancelSave();
                            ShowMessage(".خطایی در ذخیره انجام گرفته است");
                            return;
                        }
                    }
                    #endregion
                }
                PeriodPresentRequestManager[0].BeginEdit();
                PeriodPresentRequestManager[0]["PPCode"] = txtPPCode.Text;
                PeriodPresentRequestManager[0]["CrsId"] = cmbCrsId.Value;
                PeriodPresentRequestManager[0]["StartDate"] = txtStartDate.Text;
                PeriodPresentRequestManager[0]["EndDate"] = txtEndDate.Text;
                PeriodPresentRequestManager[0]["Place"] = txtPlace.Text;
                PeriodPresentRequestManager[0]["Description"] = txtDesc.Text;
                PeriodPresentRequestManager[0]["Capacity"] = txtCapacity.Text;
                //********
                PeriodPresentRequestManager[0]["StartRegisterDate"] = txtStartRegisterDate.Text;
                PeriodPresentRequestManager[0]["EndRegisterDate"] = txtEndRegisterDate.Text;
                //********
                if (ComboPPType.Value != null)
                    PeriodPresentRequestManager[0]["PeriodType"] = ComboPPType.Value;

                if (cmbPoll.Value != null)
                    PeriodPresentRequestManager[0]["PollId"] = cmbPoll.Value;

                PeriodPresentRequestManager[0]["TestDate"] = txtTestDate.Text;
                PeriodPresentRequestManager[0]["TestHour"] = txtTestHour.Text;
                PeriodPresentRequestManager[0]["TestPlace"] = txtTestPlace.Text;
                if (!string.IsNullOrEmpty(txtTestCost.Text))
                    PeriodPresentRequestManager[0]["TestCost"] = txtTestCost.Text;
                else
                    PeriodPresentRequestManager[0]["TestCost"] = DBNull.Value;

                PeriodPresentRequestManager[0]["PeriodCost"] = txtPeriodCost.Text;
                PeriodPresentRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                PeriodPresentRequestManager[0].EndEdit();
                int cnt = PeriodPresentRequestManager.Save();
                if (cnt > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_Period"].ToString())))
                    {
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, PPRId, TableType, "ویرایش اطلاعات توسط موسسه", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        ShowMessage(".خطایی در ذخیره انجام گرفته است");
                        return;
                    }

                    #region Schedule
                    if (Session["Schedule"] != null)
                    {
                        dtSchedule = (DataTable)Session["Schedule"];
                        if (dtSchedule.GetChanges() != null)
                        {
                            DataRow[] DelSchRows = dtSchedule.Select(null, null, DataViewRowState.Deleted);
                            DataRow[] EditSchRows = dtSchedule.Select(null, null, DataViewRowState.ModifiedCurrent);
                            DataRow[] InsSchRows = dtSchedule.Select(null, null, DataViewRowState.Added);

                            if (DelSchRows.Length > 0)
                            {
                                for (int i = 0; i < DelSchRows.Length; i++)
                                {
                                    SchManager.FindByCode(int.Parse(DelSchRows[i]["SchId", DataRowVersion.Original].ToString()));
                                    SchManager[0].Delete();
                                    SchManager.Save();
                                    SchManager.DataTable.AcceptChanges();
                                }
                            }

                            if (EditSchRows.Length > 0)
                            {
                                for (int i = 0; i < EditSchRows.Length; i++)
                                {
                                    SchManager.FindByCode(int.Parse(EditSchRows[i]["SchId"].ToString()));
                                    SchManager[0].BeginEdit();
                                    SchManager[0]["TtId"] = PPId;
                                    SchManager[0]["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                                    SchManager[0]["Number"] = EditSchRows[i]["Number"];
                                    SchManager[0]["Date"] = EditSchRows[i]["Date"];
                                    SchManager[0]["StartTime"] = EditSchRows[i]["StartTime"];
                                    SchManager[0]["EndTime"] = EditSchRows[i]["EndTime"];
                                    SchManager[0]["Type"] = EditSchRows[i]["Type"];
                                    SchManager[0]["Description"] = EditSchRows[i]["Description"];
                                    SchManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                    SchManager[0]["ModifiedDate"] = DateTime.Now;
                                    SchManager[0].EndEdit();
                                    SchManager.Save();
                                    SchManager.DataTable.AcceptChanges();
                                }
                            }

                            if (InsSchRows.Length > 0)
                            {
                                for (int i = 0; i < InsSchRows.Length; i++)
                                {
                                    DataRow drSch = SchManager.NewRow();
                                    drSch["TtId"] = PPId;
                                    drSch["PPRId"] = PPRId;
                                    drSch["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                                    drSch["Number"] = InsSchRows[i]["Number"];
                                    drSch["Date"] = InsSchRows[i]["Date"];
                                    drSch["StartTime"] = InsSchRows[i]["StartTime"];
                                    drSch["EndTime"] = InsSchRows[i]["EndTime"];
                                    drSch["Type"] = InsSchRows[i]["Type"];
                                    drSch["Description"] = InsSchRows[i]["Description"].ToString();
                                    drSch["UserId"] = Utility.GetCurrentUser_UserId();
                                    drSch["ModifiedDate"] = DateTime.Now;
                                    SchManager.AddRow(drSch);
                                    SchManager.Save();
                                    InsSchRows[i].BeginEdit();
                                    InsSchRows[i]["SchId"] = SchManager[SchManager.Count - 1]["SchId"];
                                    InsSchRows[i].EndEdit();
                                    SchManager.DataTable.AcceptChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    #region save teacher
                    Int16 HoPractical = 0;
                    Int16 HoNonPractical = 0;
                    Int16 HoWorkroom = 0;
                    dtOfPP = (DataTable)Session["PPTeacher"];
                    if (dtOfPP.GetChanges() != null)
                    {
                        for (int i = 0; i < dtOfPP.Rows.Count; i++)
                        {
                            if (dtOfPP.Rows[i].RowState == DataRowState.Deleted) continue;
                            if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["PracticalHour"].ToString()))
                                HoPractical += Int16.Parse(dtOfPP.Rows[i]["PracticalHour"].ToString());
                            if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["NonPracticalHour"].ToString()))
                                HoNonPractical += Int16.Parse(dtOfPP.Rows[i]["NonPracticalHour"].ToString());
                            if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["WorkroomHour"].ToString()))
                                HoWorkroom += Int16.Parse(dtOfPP.Rows[i]["WorkroomHour"].ToString());
                        }
                        if (HoPractical != Int16.Parse(txtbPracticalDuration.Text))
                        {
                            trans.CancelSave();
                            ShowMessage("تعداد ساعات عملی تعریف شده با تعداد ساعات عملی دوره برابر نمی باشد");
                            return;
                        }
                        if (HoNonPractical != Int16.Parse(txtbNonPracticalDuration.Text))
                        {
                            trans.CancelSave();
                            ShowMessage("تعداد ساعات تئوری تعریف شده با تعداد ساعات تئوری دوره برابر نمی باشد");
                            return;
                        }
                        if (HoWorkroom != Int16.Parse(txtbWorkroomDuration.Text))
                        {
                            trans.CancelSave();
                            ShowMessage("تعداد ساعات بازدید از کارگاه تعریف شده با تعداد ساعات بازدید از کارگاه دوره برابر نمی باشد");
                            return;
                        }

                        DataRow[] delRows = dtOfPP.Select(null, null, DataViewRowState.Deleted);
                        DataRow[] EditRows = dtOfPP.Select(null, null, DataViewRowState.ModifiedCurrent);
                        DataRow[] insRows = dtOfPP.Select(null, null, DataViewRowState.Added);

                        if (delRows.Length > 0)
                        {
                            for (int i = 0; i < delRows.Length; i++)
                            {
                                PPTeManager.FindByCode(int.Parse(delRows[i]["TrTeId", DataRowVersion.Original].ToString()));
                                PPTeManager[0].Delete();
                                PPTeManager.Save();
                                PPTeManager.DataTable.AcceptChanges();

                            }
                        }

                        if (EditRows.Length > 0)
                        {
                            for (int i = 0; i < EditRows.Length; i++)
                            {
                                PPTeManager.FindByCode(int.Parse(EditRows[i]["TrTeId"].ToString()));

                                PPTeManager[0].BeginEdit();
                                PPTeManager[0]["PracticalHour"] = EditRows[i]["PracticalHour"];
                                PPTeManager[0]["NonPracticalHour"] = EditRows[i]["NonPracticalHour"];
                                PPTeManager[0]["WorkroomHour"] = EditRows[i]["WorkroomHour"];
                                PPTeManager[0]["PracticalSalary"] = EditRows[i]["PracticalSalary"];
                                PPTeManager[0]["NonPracticalSalary"] = EditRows[i]["NonPracticalSalary"];
                                PPTeManager[0]["WorkroomSalary"] = EditRows[i]["WorkroomSalary"];
                                PPTeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                PPTeManager[0].EndEdit();

                                PPTeManager.Save();
                                PPTeManager.DataTable.AcceptChanges();
                            }

                        }

                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {
                                DataRow drTe = PPTeManager.NewRow();
                                drTe["PkId"] = PPId;
                                drTe["PPRId"] = PPRId;
                                drTe["TeId"] = insRows[i]["TeId"];
                                drTe["PracticalHour"] = insRows[i]["PracticalHour"];
                                drTe["NonPracticalHour"] = insRows[i]["NonPracticalHour"];
                                drTe["WorkroomHour"] = insRows[i]["WorkroomHour"];
                                drTe["PracticalSalary"] = insRows[i]["PracticalSalary"];
                                drTe["NonPracticalSalary"] = insRows[i]["NonPracticalSalary"];
                                drTe["WorkroomSalary"] = insRows[i]["WorkroomSalary"];
                                drTe["Description"] = insRows[i]["Description"].ToString();
                                drTe["Type"] = 0;//period
                                drTe["InActive"] = 0;
                                drTe["UserId"] = Utility.GetCurrentUser_UserId();
                                drTe["ModifiedDate"] = DateTime.Now;
                                PPTeManager.AddRow(drTe);

                                PPTeManager.Save();
                                insRows[i].BeginEdit();
                                insRows[i]["TrTeId"] = PPTeManager[PPTeManager.Count - 1]["TrTeId"];
                                insRows[i].EndEdit();

                                PPTeManager.DataTable.AcceptChanges();

                            }

                        }
                    }
                    #endregion

                    trans.EndSave();
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    PgMode.Value = Utility.EncryptQS("Edit");
                    Session["IsEdited_Period"] = true;
                    dtOfPP.AcceptChanges();
                    Session["PPTeacher"] = dtOfPP;
                    dtSchedule.AcceptChanges();
                    Session["Schedule"] = dtSchedule;
                    ShowMessage("ذخیره انجام شد");
                }
                else
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }
            else
            {
                ShowMessage("خطایی در ویرایش اطلاعات رخ داده است");
            }

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else if (se.Number == 2627)
                {
                    ShowMessage("کد دوره تکراری می باشد");
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
    }

    protected void InsertNewRequest(TSP.DataManager.PeriodPresentRequestType Type)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.PeriodOpinionManager OpinionManager = new TSP.DataManager.PeriodOpinionManager();
        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();
        TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        trans.Add(PeriodManager);
        trans.Add(SchManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OpinionManager);
        trans.Add(PPTeManager);
        trans.Add(PeriodPresentRequestManager);
        trans.Add(LetterRelatedTablesManager);

        try
        {
            trans.BeginSave();
            int PPId = Convert.ToInt32(Utility.DecryptQS(PeriodId.Value));
            PeriodManager.FindByCode(PPId);
            if (PeriodManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            int Status = Convert.ToInt32(PeriodManager[0]["Status"]);
            if (!InsertPeriodPresentRequest(PeriodPresentRequestManager, Type, 0, PPId, Status))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            else
            {
                int PPRId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPRId"]);

                #region save teacher
                Int16 HoPractical = 0;
                Int16 HoNonPractical = 0;
                Int16 HoWorkroom = 0;

                dtOfPP = (DataTable)Session["PPTeacher"];

                if (dtOfPP.GetChanges() != null)
                {
                    for (int i = 0; i < dtOfPP.Rows.Count; i++)
                    {
                        if (dtOfPP.Rows[i].RowState == DataRowState.Deleted) continue;
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["PracticalHour"].ToString()))
                            HoPractical += Int16.Parse(dtOfPP.Rows[i]["PracticalHour"].ToString());
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["NonPracticalHour"].ToString()))
                            HoNonPractical += Int16.Parse(dtOfPP.Rows[i]["NonPracticalHour"].ToString());
                        if (!string.IsNullOrEmpty(dtOfPP.Rows[i]["WorkroomHour"].ToString()))
                            HoWorkroom += Int16.Parse(dtOfPP.Rows[i]["WorkroomHour"].ToString());
                    }
                    if (HoPractical != Int16.Parse(txtbPracticalDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات عملی تعریف شده با تعداد ساعات عملی دوره برابر نمی باشد");
                        return;
                    }
                    if (HoNonPractical != Int16.Parse(txtbNonPracticalDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات تئوری تعریف شده با تعداد ساعات تئوری دوره برابر نمی باشد");
                        return;
                    }
                    if (HoWorkroom != Int16.Parse(txtbWorkroomDuration.Text))
                    {
                        trans.CancelSave();
                        ShowMessage("تعداد ساعات بازدید از کارگاه تعریف شده با تعداد ساعات بازدید از کارگاه دوره برابر نمی باشد");
                        return;
                    }

                    DataRow[] delRows = dtOfPP.Select(null, null, DataViewRowState.Deleted);
                    DataRow[] EditRows = dtOfPP.Select(null, null, DataViewRowState.ModifiedCurrent);
                    DataRow[] insRows = dtOfPP.Select(null, null, DataViewRowState.Added);

                    if (delRows.Length > 0)
                    {
                        for (int i = 0; i < delRows.Length; i++)
                        {
                            PPTeManager.FindByCode(int.Parse(delRows[i]["TrTeId", DataRowVersion.Original].ToString()));
                            PPTeManager[0].Delete();
                            PPTeManager.Save();
                            PPTeManager.DataTable.AcceptChanges();

                        }
                    }

                    if (EditRows.Length > 0)
                    {
                        for (int i = 0; i < EditRows.Length; i++)
                        {
                            PPTeManager.FindByCode(int.Parse(EditRows[i]["TrTeId"].ToString()));

                            PPTeManager[0].BeginEdit();
                            PPTeManager[0]["PPRId"] = PPRId;
                            PPTeManager[0]["PracticalHour"] = EditRows[i]["PracticalHour"];
                            PPTeManager[0]["NonPracticalHour"] = EditRows[i]["NonPracticalHour"];
                            PPTeManager[0]["WorkroomHour"] = EditRows[i]["WorkroomHour"];
                            PPTeManager[0]["PracticalSalary"] = EditRows[i]["PracticalSalary"];
                            PPTeManager[0]["NonPracticalSalary"] = EditRows[i]["NonPracticalSalary"];
                            PPTeManager[0]["WorkroomSalary"] = EditRows[i]["WorkroomSalary"];
                            PPTeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            PPTeManager[0].EndEdit();

                            PPTeManager.Save();
                            PPTeManager.DataTable.AcceptChanges();
                        }
                    }

                    if (insRows.Length > 0)
                    {
                        for (int i = 0; i < insRows.Length; i++)
                        {
                            DataRow drTe = PPTeManager.NewRow();
                            drTe["PkId"] = PPId;
                            drTe["PPRId"] = PPRId;
                            drTe["TeId"] = insRows[i]["TeId"];
                            drTe["PracticalHour"] = insRows[i]["PracticalHour"];
                            drTe["NonPracticalHour"] = insRows[i]["NonPracticalHour"];
                            drTe["WorkroomHour"] = insRows[i]["WorkroomHour"];
                            drTe["PracticalSalary"] = insRows[i]["PracticalSalary"];
                            drTe["NonPracticalSalary"] = insRows[i]["NonPracticalSalary"];
                            drTe["WorkroomSalary"] = insRows[i]["WorkroomSalary"];
                            drTe["Description"] = insRows[i]["Description"].ToString();
                            drTe["Type"] = 0;//period
                            drTe["InActive"] = 0;
                            drTe["UserId"] = Utility.GetCurrentUser_UserId();
                            drTe["ModifiedDate"] = DateTime.Now;
                            PPTeManager.AddRow(drTe);

                            PPTeManager.Save();
                            insRows[i].BeginEdit();
                            insRows[i]["TrTeId"] = PPTeManager[PPTeManager.Count - 1]["TrTeId"];
                            insRows[i].EndEdit();

                            PPTeManager.DataTable.AcceptChanges();
                        }
                    }

                    // dtOfCost.AcceptChanges();
                }
                #endregion

                #region Schedule
                if (Session["Schedule"] != null)
                {
                    dtSchedule = (DataTable)Session["Schedule"];
                    if (dtSchedule.GetChanges() != null)
                    {
                        DataRow[] DelSchRows = dtSchedule.Select(null, null, DataViewRowState.Deleted);
                        DataRow[] EditSchRows = dtSchedule.Select(null, null, DataViewRowState.ModifiedCurrent);
                        DataRow[] InsSchRows = dtSchedule.Select(null, null, DataViewRowState.Added);

                        if (DelSchRows.Length > 0)
                        {
                            for (int i = 0; i < DelSchRows.Length; i++)
                            {
                                SchManager.FindByCode(int.Parse(DelSchRows[i]["SchId", DataRowVersion.Original].ToString()));
                                SchManager[0].Delete();
                                SchManager.Save();
                                SchManager.DataTable.AcceptChanges();
                            }
                        }

                        if (EditSchRows.Length > 0)
                        {
                            for (int i = 0; i < EditSchRows.Length; i++)
                            {
                                SchManager.FindByCode(int.Parse(EditSchRows[i]["SchId"].ToString()));
                                SchManager[0].BeginEdit();
                                SchManager[0]["TtId"] = PPId;
                                SchManager[0]["PPRId"] = PPRId;
                                SchManager[0]["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                                SchManager[0]["Number"] = EditSchRows[i]["Number"];
                                SchManager[0]["Date"] = EditSchRows[i]["Date"];
                                SchManager[0]["StartTime"] = EditSchRows[i]["StartTime"];
                                SchManager[0]["EndTime"] = EditSchRows[i]["EndTime"];
                                SchManager[0]["Type"] = EditSchRows[i]["Type"];
                                SchManager[0]["Description"] = EditSchRows[i]["Description"];
                                SchManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                SchManager[0]["ModifiedDate"] = DateTime.Now;
                                SchManager[0].EndEdit();
                                SchManager.Save();
                                SchManager.DataTable.AcceptChanges();
                            }
                        }

                        if (InsSchRows.Length > 0)
                        {
                            for (int i = 0; i < InsSchRows.Length; i++)
                            {
                                DataRow drSch = SchManager.NewRow();
                                drSch["TtId"] = PPId;
                                drSch["PPRId"] = PPRId;
                                drSch["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                                drSch["Number"] = InsSchRows[i]["Number"];
                                drSch["Date"] = InsSchRows[i]["Date"];
                                drSch["StartTime"] = InsSchRows[i]["StartTime"];
                                drSch["EndTime"] = InsSchRows[i]["EndTime"];
                                drSch["Type"] = InsSchRows[i]["Type"];
                                drSch["Description"] = InsSchRows[i]["Description"].ToString();
                                drSch["UserId"] = Utility.GetCurrentUser_UserId();
                                drSch["ModifiedDate"] = DateTime.Now;
                                SchManager.AddRow(drSch);
                                SchManager.Save();
                                InsSchRows[i].BeginEdit();
                                InsSchRows[i]["SchId"] = SchManager[SchManager.Count - 1]["SchId"];
                                InsSchRows[i].EndEdit();
                                SchManager.DataTable.AcceptChanges();
                            }
                        }
                    }
                }
                #endregion

                #region WorkFlow
                ////*****Check is User In TaskDoer*****
                TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
                TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    TaskDoerManager.FindByTaskId(TaskId);
                    if (TaskDoerManager.Count > 0)
                    {
                        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                        int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                        if (NmcId > -1)
                        {
                            int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(PPRId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0, "آغاز گردش کار دوره آموزشی");
                            if (StartWorkFlow < 0)
                            {
                                trans.CancelSave();
                                ShowMessage("خطایی در ذخیره انجام گرفته است");
                                return;
                            }
                        }
                        else
                        {
                            trans.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.UserNotExistInNezamChart));
                            return;
                        }
                    }
                    else
                    {
                        trans.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                        return;
                    }
                }

                #endregion

                //#region Opinion
                //DataTable dtSet = SetManager.SelectActiveQuestionSet();
                //DataRow drOp = OpinionManager.NewRow();
                //drOp["PeriodId"] = PPId;
                //drOp["PeriodReqId"] = PPRId;
                //drOp["QuCode"] = dtSet.Rows[0]["QuCode"];
                //drOp["StartDate"] = PeriodPresentRequestManager[0]["EndDate"];
                //drOp["ExpiredDate"] = PeriodPresentRequestManager[0]["TestDate"];
                //drOp["UserId"] = Utility.GetCurrentUser_UserId();
                //drOp["ModifiedDate"] = DateTime.Now;
                //OpinionManager.AddRow(drOp);
                //OpinionManager.Save();
                //#endregion

                //#region Letter
                //int LetterId = CheckLetterValidationAndFill(txtLetterNo.Text);
                //if (LetterId == -1)
                //{
                //    ShowMessage("شماره نامه معتبر نمی باشد.");
                //    trans.CancelSave();
                //    return;
                //}
                //bool cletter = InsertLetterRelatedTables(LetterRelatedTablesManager, LetterId, PPRId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest));
                //if (!cletter)
                //{
                //    trans.CancelSave();
                //    ShowMessage("خطایی در ذخیره انجام گرفته است");
                //    return;
                //}
                //#endregion

                trans.EndSave();
                PeriodRequestId.Value = Utility.EncryptQS(PPRId.ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                Session["IsEdited_Period"] = true;
                ShowMessage("ذخیره انجام شد");
                AspxMenu1.Enabled = true;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else if (se.Number == 2627)
                {
                    ShowMessage("کد دوره تکراری می باشد");
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
    }

    protected bool InsertPeriodPresentRequest(TSP.DataManager.PeriodPresentRequestManager ppRequestManager, TSP.DataManager.PeriodPresentRequestType Type, Int16 IsConfirm, int PPId, int Status)
    {
        string InsId = Utility.DecryptQS(InstitueId.Value);
        DataRow drP = ppRequestManager.NewRow();
        drP["PPId"] = PPId;
        drP["PPCode"] = txtPPCode.Text;
        drP["InsId"] = InsId;
        drP["CrsId"] = cmbCrsId.Value;
        drP["StartDate"] = txtStartDate.Text;
        drP["EndDate"] = txtEndDate.Text;
        drP["Place"] = txtPlace.Text;
        drP["Description"] = txtDesc.Text;
        drP["Capacity"] = txtCapacity.Text;
        drP["StartRegisterDate"] = txtStartRegisterDate.Text;
        drP["EndRegisterDate"] = txtEndRegisterDate.Text;
        drP["CreateDate"] = Utility.GetDateOfToday();
        drP["TestDate"] = txtTestDate.Text;
        if (ComboPPType.Value != null)
            drP["PeriodType"] = ComboPPType.Value;
        else
            drP["PeriodType"] = 0;

        if (cmbPoll.Value != null)
            drP["PollId"] = cmbPoll.Value;
        else
            drP["PollId"] = DBNull.Value;

        drP["TestHour"] = txtTestHour.Text;
        drP["TestPlace"] = txtTestPlace.Text;
        drP["PeriodCost"] = txtPeriodCost.Text;
        if (!string.IsNullOrEmpty(txtTestCost.Text))
            drP["TestCost"] = txtTestCost.Text;
        else
            drP["TestCost"] = DBNull.Value;
        drP["Discount"] = DBNull.Value;
        drP["Status"] = Status;
        drP["InActive"] = 0;
        drP["Type"] = Type;
        drP["IsConfirm"] = IsConfirm;

        //int LetterId = CheckLetterValidationAndFill(txtLetterNo.Text);
        //if (LetterId < -1)
        //{
        //    ShowMessage("شماره نامه معتبر نمی باشد.");
        //    return false;
        //}
        //drP["LetterNo"] = txtLetterNo.Text.Trim();
        //drP["LetterDate"] = txtLetterDate.Text.Trim();

        drP["UltId"] = (int)TSP.DataManager.UserType.Institute;
        drP["UserId"] = Utility.GetCurrentUser_UserId();
        drP["ModifiedDate"] = DateTime.Now;
        ppRequestManager.AddRow(drP);
        if (ppRequestManager.Save() > 0) return true;
        else return false;
    }
    #endregion

    #region Form
    private void CreateDataTableTeacher()
    {
        dtOfPP = new DataTable();
        dtOfPP.Columns.Add("TeId");
        dtOfPP.Columns.Add("TeName");
        dtOfPP.Columns.Add("LiName");
        dtOfPP.Columns.Add("MjName");
        dtOfPP.Columns.Add("PracticalHour");
        dtOfPP.Columns.Add("NonPracticalHour");
        dtOfPP.Columns.Add("WorkroomHour");
        dtOfPP.Columns.Add("PracticalSalary");
        dtOfPP.Columns.Add("NonPracticalSalary");
        dtOfPP.Columns.Add("WorkroomSalary");
        dtOfPP.Columns.Add("Description");
        dtOfPP.Columns.Add("TrTeId");
        dtOfPP.Columns.Add("Id");
        dtOfPP.Columns.Add("FileNo");
        dtOfPP.Columns.Add("MeId");
        //dtOfPP.Columns.Add("Paye");
        dtOfPP.Columns["Id"].AutoIncrement = true;
        dtOfPP.Columns["Id"].AutoIncrementSeed = 1;
        dtOfPP.Constraints.Add("PK_ID", dtOfPP.Columns["Id"], true);
        dtOfPP.Columns.Add("PPRId");
        Session["PPTeacher"] = dtOfPP;
    }
    private void CreateDataTableSchedule()
    {
        dtSchedule = new DataTable();
        dtSchedule.Columns.Add("SchId");
        dtSchedule.Columns.Add("Subject");
        dtSchedule.Columns.Add("Number");
        dtSchedule.Columns.Add("Date");
        dtSchedule.Columns.Add("StartTime");
        dtSchedule.Columns.Add("EndTime");
        dtSchedule.Columns.Add("Description");
        dtSchedule.Columns.Add("Type");
        dtSchedule.Columns.Add("Id");
        dtSchedule.Columns["Id"].AutoIncrement = true;
        dtSchedule.Columns["Id"].AutoIncrementSeed = 1;
        dtSchedule.Constraints.Add("Id", dtSchedule.Columns["Id"], true);
        dtSchedule.Columns.Add("PPRId");
        Session["Schedule"] = dtSchedule;
    }
    protected void Enable()
    {
        cmbPoll.Enabled =
         cmbCrsId.Enabled =
          ComboPPType.Enabled =
        PanelBaseInfo.Enabled =
        GridViewSchedule.Enabled =
        txtStartDate.Enabled =
        txtEndDate.Enabled =
        txtTestDate.Enabled =
        txtStartRegisterDate.Enabled =
        txtEndRegisterDate.Enabled =
      PanelBaseTeacher.Visible =
      GridViewTeacher.Enabled = true;
    }

    protected void Disable()
    {
        cmbPoll.Enabled =
            cmbCrsId.Enabled =
            ComboPPType.Enabled =
             PanelBaseInfo.Enabled =
        GridViewSchedule.Enabled =
        txtStartDate.Enabled =
        txtEndDate.Enabled =
        txtTestDate.Enabled =
        txtStartRegisterDate.Enabled =
        txtEndRegisterDate.Enabled =
        PanelBaseTeacher.Visible =
        GridViewTeacher.Enabled = false;

    }

    protected void ClearForm()
    {
        txtCrsCode.Text =
            txtPDuration.Text = txtPoint.Text = txtValidDuration.Text =
            txtbPracticalDuration.Text = txtbNonPracticalDuration.Text =
            txtbWorkroomDuration.Text = txtPPCode.Text = txtCapacity.Text =
            txtDiscount.Text =
            txtPeriodCost.Text = txtStartDate.Text = txtEndDate.Text =
            txtTestDate.Text = txtTestHour.Text = txtStartRegisterDate.Text =
            txtEndRegisterDate.Text = txtPlace.Text = txtTestPlace.Text = txtDesc.Text =
        txtStartDate.Text =
        txtEndDate.Text =
        txtTestDate.Text =
        txtStartRegisterDate.Text =
        txtEndRegisterDate.Text = "";
        cmbCrsId.SelectedIndex =
            ComboPPType.SelectedIndex = -1;
        ClearTeacherForm();

        dtOfPP = (DataTable)Session["PPTeacher"];
        dtOfPP.Rows.Clear();
        GridViewTeacher.DataSource = dtOfPP;
        GridViewTeacher.DataBind();

        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        GridViewSchedule.DataSource = SchManager.DataTable;
        GridViewSchedule.DataBind();
    }

    private void ClearTeacherForm()
    {
        cmbTeId.SelectedIndex = -1;
        txtSalNonpractical.Text = txtHoNonPractical.Text =
         txtSalPractical.Text = txtHoPractical.Text = txtSalWorkroom.Text =
     txtHoWorkroom.Text = txtTTeName.Text = txtTMajor.Text = txtTLicence.Text =
     txtTFileNo.Text = txtTMeId.Text = txtTPaye.Text = txtTeDesc.Text = "";
    }
    void SetMode(string PageMode, int PPRId)
    {
        switch (PageMode)
        {
            case "View":
                btnEdit.Enabled = true;
                btnEdit2.Enabled = true;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                btnAddTeacher.ClientVisible = false;
                CheckWorkFlowPermission();
                FillFormForRequest(PPRId);
                Disable();
                InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest), PPRId);
                ASPxRoundPanel2.HeaderText = "مشاهده";
                break;
            case "New":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                AspxMenu1.Enabled = false;
                ODBPoll.SelectParameters["DateOfToday"].DefaultValue = Utility.GetDateOfToday();
                break;
            case "Edit":
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillFormForRequest(PPRId);
                Enable();
                InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest), PPRId);
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                break;
            case "Change":
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillFormForRequest(PPRId);
                Enable();
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "درخواست تغییرات دوره آموزشی";
                break;
        }
    }

    protected void FillFormForRequest(int PPRId)
    {
        TSP.DataManager.PeriodPresentRequestManager manager = new TSP.DataManager.PeriodPresentRequestManager();
        TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        try
        {
            manager.FindByCode(PPRId);
            if (manager.Count == 1)
            {
                int PPId = Convert.ToInt32(manager[0]["PPId"]);
                decimal TestCost = 0;
                txtCapacity.Text = manager[0]["Capacity"].ToString();
                if (!Utility.IsDBNullOrNullValue(manager[0]["TestCost"]))
                {
                    TestCost = Convert.ToDecimal(manager[0]["TestCost"].ToString());
                    txtTestCost.Text = TestCost.ToString("##");
                }
                txtEndDate.Text = manager[0]["EndDate"].ToString();
                decimal PeriodCost = Convert.ToDecimal(manager[0]["PeriodCost"].ToString());
                txtPeriodCost.Text = PeriodCost.ToString("##");
                txtPPCode.Text = manager[0]["PPCode"].ToString();
                txtPlace.Text = manager[0]["Place"].ToString();

                txtTestDate.Text = manager[0]["TestDate"].ToString();
                txtTestHour.Text = manager[0]["TestHour"].ToString();
                txtTestPlace.Text = manager[0]["TestPlace"].ToString();

                txtStartDate.Text = manager[0]["StartDate"].ToString();
                //*******
                txtStartRegisterDate.Text = manager[0]["StartRegisterDate"].ToString();
                txtEndRegisterDate.Text = manager[0]["EndRegisterDate"].ToString();
                //*******
                //cmbCrsId.DataBind();
                cmbTeId.DataBind();
                txtDesc.Text = manager[0]["Description"].ToString();
                cmbCrsId.DataBind();
                cmbCrsId.SelectedIndex = cmbCrsId.Items.FindByValue(manager[0]["CrsId"].ToString()).Index;
                cmbCrsId_SelectedIndexChanged(this, new EventArgs());

                ODBPoll.SelectParameters["PollId"].DefaultValue = manager[0]["PollId"].ToString();
                cmbPoll.DataBind();
                if (!Utility.IsDBNullOrNullValue(manager[0]["PollId"]))
                    cmbPoll.SelectedIndex = cmbPoll.Items.FindByValue(manager[0]["PollId"].ToString()).Index;
                #region Teachers
                PPTeManager.FindByPeriodRequestId(PPId, PPRId, 0);
                if (PPTeManager.Count > 0)
                {
                    dtOfPP = (DataTable)Session["PPTeacher"];
                    for (int i = 0; i < PPTeManager.Count; i++)
                    {
                        DataRow dr = dtOfPP.NewRow();
                        dr["PPRId"] = PPTeManager[i]["PPRId"].ToString();
                        dr["TeId"] = PPTeManager[i]["TeId"].ToString();
                        dr["TeName"] = PPTeManager[i]["TeName"].ToString();
                        dr["LiName"] = PPTeManager[i]["LiName"].ToString();
                        dr["MjName"] = PPTeManager[i]["MjName"].ToString();
                        dr["PracticalHour"] = PPTeManager[i]["PracticalHour"];
                        dr["NonPracticalHour"] = PPTeManager[i]["NonPracticalHour"].ToString();
                        dr["WorkroomHour"] = PPTeManager[i]["WorkroomHour"].ToString();
                        dr["PracticalSalary"] = PPTeManager[i]["PracticalSalary"].ToString();
                        dr["NonPracticalSalary"] = PPTeManager[i]["NonPracticalSalary"].ToString();
                        dr["WorkroomSalary"] = PPTeManager[i]["WorkroomSalary"].ToString();
                        dr["Description"] = PPTeManager[i]["Description"].ToString();
                        dr["TrTeId"] = PPTeManager[i]["TrTeId"];
                        dtOfPP.Rows.Add(dr);
                    }
                    dtOfPP.AcceptChanges();
                    GridViewTeacher.DataSource = dtOfPP;
                    GridViewTeacher.DataBind();
                }
                #endregion

                SchManager.FindByPeriodRequest(PPId, PPRId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest));
                if (SchManager.Count > 0)
                {
                    if (Session["Schedule"] != null)
                    {
                        dtSchedule = (DataTable)Session["Schedule"];
                        for (int i = 0; i < SchManager.Count; i++)
                        {
                            DataRow dr = dtSchedule.NewRow();
                            dr["PPRId"] = SchManager[i]["PPRId"].ToString();
                            dr["SchId"] = SchManager[i]["SchId"].ToString();
                            dr["Number"] = SchManager[i]["Number"].ToString();
                            dr["Date"] = SchManager[i]["Date"].ToString();
                            dr["StartTime"] = SchManager[i]["StartTime"];
                            dr["EndTime"] = SchManager[i]["EndTime"].ToString();
                            dr["Type"] = SchManager[i]["Type"].ToString();
                            dr["Description"] = SchManager[i]["Description"].ToString();
                            dtSchedule.Rows.Add(dr);
                        }
                        dtSchedule.AcceptChanges();
                        GridViewSchedule.DataSource = dtSchedule;
                        GridViewSchedule.DataBind();
                    }
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");

        }
    }

    private Boolean IsInActive(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.InvalidPeriod)
            {
                return true;
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

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void ShowCallBackMessage(int type, string Msg)
    {
        if (type == 1)
        {
            GridViewTeacher.JSProperties["cpMsg"] = Msg;
            GridViewTeacher.JSProperties["cpError"] = 1;
        }
        else
        {
            GridViewSchedule.JSProperties["cpMsg"] = Msg;
            GridViewSchedule.JSProperties["cpError"] = 1;
        }
    }
    #endregion

    #region WFMethods
    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode == "View" || PageMode == "Edit")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int Permission = -1;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            ShowMessage("شما سطح دسترسی جهت درخواست ثبت دوره های آموزشی را ندارید.");
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(PPRId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
                case "Change":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
            }
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده اطلاعات توسط موسسه", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
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
    }
    #endregion

    bool IsInCurrentRequestSchedule(int SchId)
    {
        int PPRId = Convert.ToInt32(Utility.DecryptQS(PeriodRequestId.Value));
        TSP.DataManager.ScheduleManager smanager = new TSP.DataManager.ScheduleManager();
        smanager.FindByCode(SchId);
        if (smanager.Count == 1)
            if (Convert.ToInt32(smanager[0]["PPRId"]) == PPRId)
                return true;
            else return false;
        else return false;
    }

    bool IsInCurrentRequestTeacher(int TeId)
    {
        int PPRId = Convert.ToInt32(Utility.DecryptQS(PeriodRequestId.Value));
        TSP.DataManager.TrainingTeachersManager tmanager = new TSP.DataManager.TrainingTeachersManager();
        tmanager.FindByCode(TeId);
        if (tmanager.Count == 1)
            if (Convert.ToInt32(tmanager[0]["PPRId"]) == PPRId)
                return true;
            else return false;
        else return false;
    }
    #endregion
}


