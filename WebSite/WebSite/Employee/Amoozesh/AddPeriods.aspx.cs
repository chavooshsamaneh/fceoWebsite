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
using System.IO;
public partial class Employee_Amoozesh_AddPeriods : System.Web.UI.Page
{
    DataTable dtOfPP = new DataTable();
    DataTable dtSchedule = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            SetKey(true);

            #region Make DataTables
            if (Session["PPTeacher"] == null)
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
            else
                dtOfPP = (DataTable)Session["PPTeacher"];

            GridViewTeacher.DataSource = dtOfPP;
            GridViewTeacher.DataBind();


            if (Session["Schedule"] == null)
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
            else
                dtSchedule = (DataTable)Session["Schedule"];

            GridViewSchedule.DataSource = dtSchedule;
            GridViewSchedule.DataBind();
            #endregion

            string PageMode = Utility.DecryptQS(PgMode.Value);
            //string PPId = Utility.DecryptQS(PeriodId.Value);
            string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
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
        if (PeriodId.Value != null)
        {
            CheckRegistrationCount(Convert.ToInt32(Utility.DecryptQS(PeriodId.Value)));
        }
        if (Session["PPTeacher"] != null)
        {
            dtOfPP = (DataTable)Session["PPTeacher"];
            GridViewTeacher.DataSource = dtOfPP;
            GridViewTeacher.DataBind();
        }
    }

    #region btn Click
    //protected void BtnNew_Click(object sender, EventArgs e)
    //{
    //    PeriodId.Value = Utility.EncryptQS("");
    //    PgMode.Value = Utility.EncryptQS("New");
    //    ASPxRoundPanel2.HeaderText = "جدید";
    //    ClearForm();
    //    SetEnabled(true);
    //    TSP.DataManager.Permission per = TSP.DataManager.PeriodPresentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

    //    //btnDisActive.Enabled = false;
    //    //btnDisActive2.Enabled = false;
    //    btnEdit2.Enabled = false;
    //    btnEdit.Enabled = false;
    //    btnSave.Enabled = per.CanNew;
    //    btnSave2.Enabled = per.CanNew;
    //    btnAddTeacher.ClientVisible = true;
    //    RoundPanelRequest.ClientVisible = false;

    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    //this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
    //    AspxMenu1.Enabled = false;
    //}

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
            if (Convert.ToInt32(PeriodPresentRequestManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Institute)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "این درخواست در پرتال موسسه ثبت شده و توسط شما قابل ویرایش نمی باشد.";
                return;
            }
            SetEnabled(true);
            TSP.DataManager.Permission per = TSP.DataManager.PeriodPresentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnAddTeacher.ClientVisible = true;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            ODBCourse.FilterParameters["InActive"].DefaultValue = "";
            cmbCrsId.DataBind();
            FillFormForRequest(PPRId);
            ASPxRoundPanel2.Enabled = true;
            ASPxRoundPanel2.HeaderText = "ویرایش";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PPId = Utility.DecryptQS(PeriodId.Value);
        string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
        if (PageMode != "PrintRequest")
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
        }
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
            case "PrintRequest":
                InsertNewRequest(TSP.DataManager.PeriodPresentRequestType.PrintRequest);
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
            Response.Redirect("Periods.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Periods.aspx");
        }

    }

    protected void btnAddTeacher_Click(object sender, EventArgs e)
    {
        if (Session["PPTeacher"] != null)
        {
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

                //for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
                //{
                //    if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
                //    {
                //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
                //        txt.Text = "";
                //    }

                //}
                txtSalNonpractical.Text
             = txtHoNonPractical.Text = txtSalPractical.Text = txtHoPractical.Text =
           txtSalWorkroom.Text = txtHoWorkroom.Text = txtTTeName.Text = txtTMajor.Text =
           txtTLicence.Text = txtTFileNo.Text = txtTMeId.Text = txtTPaye.Text = txtTeDesc.Text = "";
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
    }
    #endregion

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

    #region Grid Schedule
    protected void GridViewSchedule_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["Schedule"] != null)
        {
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
                //if (!Utility.IsDBNullOrNullValue(dr["SchId"]))
                //    if (!IsInCurrentRequestSchedule(Convert.ToInt32(dr["SchId"])))
                //    {   GridViewSchedule.CancelEdit();
                //        ShowCallBackMessage(2, "امکان ویرایش زمان بندی مربوط به درخواست های قبل وجود ندارد");
                //      //  e.Cancel = true;

                //        return;
                //    }

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
                e.Cancel = true;
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
            GridViewSchedule.CancelEdit();
            return;
        }
        else
        {
            dtSchedule = (DataTable)Session["Schedule"];

            DataRow dr = dtSchedule.Rows.Find(e.Keys[0]);
            //if (!Utility.IsDBNullOrNullValue(dr["SchId"]))
            //    if (!IsInCurrentRequestSchedule(Convert.ToInt32(dr["SchId"])))
            //    {
            //        ShowCallBackMessage(2, "امکان حذف زمان بندی مربوط به درخواست های قبل وجود ندارد");
            //        GridViewSchedule.CancelEdit();
            //        return;
            //    }

            dtSchedule.Rows.Find(e.Keys[0]).Delete();
            Session["Schedule"] = dtSchedule;
            GridViewSchedule.DataSource = (DataTable)Session["Schedule"];
            GridViewSchedule.DataBind();
            dtSchedule = (DataTable)Session["Schedule"];
        }
    }

    protected void GridViewSchedule_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode == "View")
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
                e.Visible = false;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
                e.Visible = false;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                e.Visible = false;
        }
        else
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
                e.Visible = true;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
                e.Visible = true;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                e.Visible = true;
        }
    }

    protected void GridViewSchedule_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PersianDateControls.PersianDateTextBox calender = (PersianDateControls.PersianDateTextBox)GridViewSchedule.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSchedule.Columns["Date"], "txtDate");
        if (string.IsNullOrEmpty(calender.Text))
        {
            e.RowError = "فیلد تاریخ را وارد نمایید";
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
    #endregion

    #region Grid Teacher
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
            GridViewTeacher.CancelEdit();
            return;
        }
        else
        {
            dtOfPP = (DataTable)Session["PPTeacher"];

            DataRow dr = dtOfPP.Rows.Find(e.Keys[0]);
            //if (!Utility.IsDBNullOrNullValue(dr["TrTeId"]))
            //    if (!IsInCurrentRequestTeacher(Convert.ToInt32(dr["TrTeId"])))
            //    {
            //        ShowCallBackMessage(1, "امکان حذف مدرس مربوط به درخواست های قبل وجود ندارد");
            //        GridViewTeacher.CancelEdit();
            //        return;
            //    }

            dtOfPP.Rows.Find(e.Keys[0]).Delete();
            Session["PPTeacher"] = dtOfPP;
            GridViewTeacher.DataSource = (DataTable)Session["PPTeacher"];
            GridViewTeacher.DataBind();
            dtOfPP = (DataTable)Session["PPTeacher"];
        }
    }

    protected void GridViewTeacher_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        HiddenFieldPage["cpMsg"] = "";
        HiddenFieldPage["cpError"] = 0;
        if (Session["PPTeacher"] == null)
        {
            ShowCallBackMessage(1, Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            e.Cancel = true;
            return;
        }
        dtOfPP = (DataTable)Session["PPTeacher"];

        try
        {
            DataRow dr = dtOfPP.Rows.Find(e.Keys[0]);
            //if (!Utility.IsDBNullOrNullValue(dr["TrTeId"]))
            //    if (!IsInCurrentRequestTeacher(Convert.ToInt32(dr["TrTeId"])))
            //    {
            //        ShowCallBackMessage(1, "امکان حذف مدرس مربوط به درخواست های قبل وجود ندارد");
            //        e.Cancel = true;
            //        return;
            //    }

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
            e.Cancel = true;
            Utility.SaveWebsiteError(err);
            ShowCallBackMessage(1, "خطایی در اضافه کردن رخ داده است");
        }

        e.Cancel = true;
    }

    protected void GridViewTeacher_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode == "View")
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
                e.Visible = false;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
                e.Visible = false;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                e.Visible = false;
        }
        else
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
                e.Visible = true;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
                e.Visible = true;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                e.Visible = true;
        }
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
    #endregion

    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        if (string.IsNullOrEmpty(PPId.ToString()))
        {
            Response.Redirect("Periods.aspx");
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
    protected void WFUserControl_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
        string PPId = Utility.DecryptQS(PeriodId.Value);
        if (Utility.IsDBNullOrNullValue(PPRId) || Convert.ToInt32(PPRId) == -1)
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("ابتدا دوره آموزشی را ذخیره نمایید");
            return;
        }

        int PeriodsTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(PeriodsTableType, Convert.ToInt32(PPRId));
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        string Qs = "~/Employee/Amoozesh/AddPeriods.aspx?PPId=" + PeriodId.Value + "PageMode=" + PgMode.Value
             + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PPRId=" + Utility.EncryptQS(PPRId.ToString());
        WFUserControl.QueryStringForRedirect = Qs;
        WFUserControl.PerformCallback(Convert.ToInt32(PPRId), PeriodsTableType, WfCode, e);
        SetKey(false);
    }

    #endregion

    #region Methods

    private void SetKey(Boolean SetParameters = true)
    {
        if (SetParameters)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Periods.aspx");
                return;
            }
            Session["IsEdited_Period"] = false;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
                PeriodRequestId.Value = Server.HtmlDecode(Request.QueryString["PPRId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("Periods.aspx");
            }
        }

        Session["TeacherUpload"] = null;
        Session["PPTeacher"] = null;
        Session["Schedule"] = null;
    }

    void SetMode(string PageMode, int PPRId)
    {
        switch (PageMode)
        {
            case "View":
                ODBCourse.FilterParameters["InActive"].DefaultValue = "";
                cmbCrsId.DataBind();
                btnEdit.Enabled = true;
                btnEdit2.Enabled = true;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                btnAddTeacher.ClientVisible = false;
                CheckWorkFlowPermission();
                FillFormForRequest(PPRId);
                SetEnabled(false);
                InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest), PPRId);
                ASPxRoundPanel2.HeaderText = "مشاهده";
                break;
            case "New":
                ODBCourse.FilterParameters["InActive"].DefaultValue = "False";
                cmbCrsId.DataBind();
                SetEnabled(true);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                AspxMenu1.Enabled = false;
                ODBPoll.SelectParameters["DateOfToday"].DefaultValue = Utility.GetDateOfToday();
                break;
            case "Edit":
                ODBCourse.FilterParameters["InActive"].DefaultValue = "False";
                cmbCrsId.DataBind();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillFormForRequest(PPRId);
                SetEnabled(true);
                InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest), PPRId);
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                break;
            case "Change":
                ODBCourse.FilterParameters["InActive"].DefaultValue = "False";
                cmbCrsId.DataBind();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillFormForRequest(PPRId);
                SetEnabled(true);
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "درخواست تغییرات دوره آموزشی";
                break;
            case "PrintRequest":
                ODBCourse.FilterParameters["InActive"].DefaultValue = "";
                cmbCrsId.DataBind();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillFormForRequest(PPRId);
                SetEnabled(false);
                ASPxRoundPanel2.Enabled = false;
                ASPxRoundPanel2.HeaderText = "درخواست چاپ گواهینامه دوره آموزشی";
                break;
        }
    }

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
            trans.BeginSave();
            DataRow drP = PeriodManager.NewRow();
            drP["PPCode"] = txtPPCode.Text;
            drP["InsId"] = cmbInstitue.Value;
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
                drP["PollId"] = -1;

            drP["TestHour"] = txtTestHour.Text;
            drP["TestPlace"] = txtTestPlace.Text;
            drP["PeriodCost"] = txtPeriodCost.Text;
            if (!string.IsNullOrEmpty(txtTestCost.Text))
                drP["TestCost"] = txtTestCost.Text;
            else
                drP["TestCost"] = DBNull.Value;

            //if (!string.IsNullOrEmpty(txtDiscount.Text))
            //    drP["Discount"] = txtDiscount.Text;
            //else
            drP["Discount"] = DBNull.Value;
            drP["Status"] = (int)TSP.DataManager.PeriodPresentStatus.Inserting;
            drP["InActive"] = 0;
            drP["UserId"] = Utility.GetCurrentUser_UserId();
            drP["ModifiedDate"] = DateTime.Now;

            PeriodManager.AddRow(drP);

            if (PeriodManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
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
            //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                //TaskDoerManager.FindByTaskId(TaskId);
                //if (TaskDoerManager.Count > 0)
                //{
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
                //}
                //else
                //{
                //    trans.CancelSave();
                //    ShowMessage("خطایی در ذخیره انجام گرفته است");
                //}
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
            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
            Session["IsEdited_Period"] = true;
            ShowMessage("ذخیره انجام شد");
            AspxMenu1.Enabled = true;
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
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

        trans.Add(SchManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(PPTeManager);
        trans.Add(PeriodPresentRequestManager);
        trans.Add(PeriodManager);
        try
        {
            trans.BeginSave();
            PeriodPresentRequestManager.FindByCode(PPRId);
            if (PeriodPresentRequestManager.Count != 1)
            {
                ShowMessage("خطایی در ویرایش اطلاعات رخ داده است");
                return;
            }
            int PPId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPId"]);
            int Type = Convert.ToInt32(PeriodPresentRequestManager[0]["Type"]);


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
                    //********8
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
            //********8
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

            if (PeriodPresentRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_Period"].ToString())))
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, PPRId, TableType, "Update", Utility.GetCurrentUser_UserId());
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

                // dtOfCost.AcceptChanges();
            }
            #endregion

            trans.EndSave();
            ASPxRoundPanel2.HeaderText = "ویرایش";
            PgMode.Value = Utility.EncryptQS("Edit");
            Session["IsEdited_Period"] = true;
            ShowMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            trans.CancelSave();

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

        trans.Add(PeriodManager);
        trans.Add(SchManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OpinionManager);
        trans.Add(PPTeManager);
        trans.Add(PeriodPresentRequestManager);

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
                //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
                TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
                string Description = "آغاز گردش کار دوره آموزشی";
                if (Type == TSP.DataManager.PeriodPresentRequestType.PrintRequest)
                {
                    TaskCode = (int)TSP.DataManager.WorkFlowTask.PrindPeriodCertificatesSaveRequest;
                    Description = "آغاز گردش کار درخواست چاپ گواهینامه دوره آموزشی";
                }
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    //TaskDoerManager.FindByTaskId(TaskId);
                    //if (TaskDoerManager.Count > 0)
                    //{
                    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                    int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                    if (NmcId > -1)
                    {
                        int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(PPRId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0, Description);
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
                    //}
                    //else
                    //{
                    //    trans.CancelSave();
                    //    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    //    return;
                    //}
                }
                else
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }

                #endregion
                trans.EndSave();
                PeriodRequestId.Value = Utility.EncryptQS(PPRId.ToString());
                if (Type == TSP.DataManager.PeriodPresentRequestType.PrintRequest)
                {
                    PgMode.Value = Utility.EncryptQS("View");
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                }
                else
                {

                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                }
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
        DataRow drP = ppRequestManager.NewRow();
        drP["PPId"] = PPId;
        drP["PPCode"] = txtPPCode.Text;
        drP["InsId"] = cmbInstitue.Value;
        drP["CrsId"] = cmbCrsId.Value;
        drP["StartDate"] = txtStartDate.Text;
        drP["EndDate"] = txtEndDate.Text;
        drP["Place"] = txtPlace.Text;
        drP["Description"] = txtDesc.Text;
        if (!string.IsNullOrWhiteSpace(txtCapacity.Text))
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
            drP["PollId"] = -1;

        drP["TestHour"] = txtTestHour.Text;
        drP["TestPlace"] = txtTestPlace.Text;
        if (!string.IsNullOrWhiteSpace(txtPeriodCost.Text))
            drP["PeriodCost"] = txtPeriodCost.Text;
        else
            drP["PeriodCost"] = 0;
        if (!string.IsNullOrEmpty(txtTestCost.Text))
            drP["TestCost"] = txtTestCost.Text;
        else
            drP["TestCost"] = DBNull.Value;
        drP["Discount"] = DBNull.Value;
        drP["Status"] = Status;
        drP["InActive"] = 0;
        drP["Type"] = Type;
        drP["IsConfirm"] = IsConfirm;

        drP["LetterNo"] = "";
        drP["LetterDate"] = "";

        drP["UltId"] = (int)TSP.DataManager.UserType.Employee;
        drP["UserId"] = Utility.GetCurrentUser_UserId();
        drP["ModifiedDate"] = DateTime.Now;
        ppRequestManager.AddRow(drP);
        if (ppRequestManager.Save() > 0) return true;
        else return false;
    }
    #endregion

    #region Form
    private void SetEnabled(Boolean Enabled)
    {
        GridViewSchedule.Enabled = Enabled;
        txtStartDate.Enabled = Enabled;
        txtEndDate.Enabled = Enabled;
        txtTestDate.Enabled = Enabled;
        txtStartRegisterDate.Enabled = Enabled;
        txtEndRegisterDate.Enabled = Enabled;
        tblTeacher.Visible = Enabled;

        cmbPoll.Enabled = Enabled;
        cmbCrsId.Enabled = Enabled;
        cmbInstitue.Enabled = Enabled;
        txtPDuration.Enabled = Enabled;
        txtPoint.Enabled = Enabled;
        txtValidDuration.Enabled = Enabled;
        txtbPracticalDuration.Enabled = Enabled;
        txtbNonPracticalDuration.Enabled = Enabled;
        txtbWorkroomDuration.Enabled = Enabled;
        txtPPCode.Enabled = Enabled;
        txtCapacity.Enabled = Enabled;
        txtPeriodCost.Enabled = Enabled;
        txtDiscount.Enabled = Enabled;
        txtTestCost.Enabled = Enabled;
        txtStartDate.Enabled = Enabled;
        txtEndDate.Enabled = Enabled;
        txtTestDate.Enabled = Enabled;
        txtTestHour.Enabled = Enabled;
        txtStartRegisterDate.Enabled = Enabled;
        txtEndRegisterDate.Enabled = Enabled;
        ComboPPType.Enabled = Enabled;
        txtPlace.Enabled = Enabled;
        txtTestPlace.Enabled = Enabled;
        txtDesc.Enabled = Enabled;
    }

    //protected void Enable()
    //{
    //    GridViewSchedule.Enabled = true;
    //    txtStartDate.Enabled = true;
    //    txtEndDate.Enabled = true;
    //    txtTestDate.Enabled = true;
    //    txtStartRegisterDate.Enabled = true;
    //    txtEndRegisterDate.Enabled = true;


    //    for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
    //    {
    //        if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
    //        {
    //            DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
    //            txt.ClientEnabled = true;
    //        }
    //        else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxComboBox)
    //        {
    //            DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel4.Controls[i];
    //            cmb.ClientEnabled = true;
    //        }
    //        else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxMemo)
    //        {
    //            DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel4.Controls[i];
    //            memo.ClientEnabled = true;
    //        }
    //    }
    //    for (int i = 0; i < ASPxRoundPanel6.Controls.Count; i++)
    //    {
    //        if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxTextBox)
    //        {
    //            DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel6.Controls[i];
    //            txt.Enabled = true;
    //        }
    //        else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxComboBox)
    //        {
    //            DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel6.Controls[i];
    //            cmb.Enabled = true;
    //        }
    //        else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxMemo)
    //        {
    //            DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel6.Controls[i];
    //            memo.Enabled = true;
    //        }
    //    }
    //}

    //protected void Disable()
    //{

    //    GridViewSchedule.Enabled = false;
    //    txtStartDate.Enabled = false;
    //    txtEndDate.Enabled = false;
    //    txtTestDate.Enabled = false;
    //    txtStartRegisterDate.Enabled = false;
    //    txtEndRegisterDate.Enabled = false;

    //    for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
    //    {
    //        if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
    //        {
    //            DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
    //            txt.ClientEnabled = false;
    //        }
    //        else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxComboBox)
    //        {
    //            DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel4.Controls[i];
    //            cmb.ClientEnabled = false;
    //        }
    //        else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxMemo)
    //        {
    //            DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel4.Controls[i];
    //            memo.ClientEnabled = false;
    //        }
    //    }
    //    for (int i = 0; i < ASPxRoundPanel6.Controls.Count; i++)
    //    {
    //        if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxTextBox)
    //        {
    //            DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel6.Controls[i];
    //            txt.Enabled = false;
    //        }
    //        else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxComboBox)
    //        {
    //            DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel6.Controls[i];
    //            cmb.Enabled = false;
    //        }
    //        else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxMemo)
    //        {
    //            DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel6.Controls[i];
    //            memo.Enabled = false;
    //        }
    //    }
    //}

    //protected void ClearForm()
    //{
    //    txtStartDate.Text = "";
    //    txtEndDate.Text = "";
    //    txtTestDate.Text = "";
    //    txtStartRegisterDate.Text = "";
    //    txtEndRegisterDate.Text = "";


    //    //for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
    //    //{
    //    //    if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
    //    //    {
    //    //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
    //    //        txt.Text = "";
    //    //    }
    //    //    else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxComboBox)
    //    //    {
    //    //        DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel4.Controls[i];
    //    //        cmb.DataBind();
    //    //        //cmb.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
    //    //        cmb.SelectedIndex = -1;
    //    //    }
    //    //    else if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxMemo)
    //    //    {
    //    //        DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel4.Controls[i];
    //    //        memo.Text = "";

    //    //    }
    //    //}
    //    //for (int i = 0; i < ASPxRoundPanel6.Controls.Count; i++)
    //    //{
    //    //    if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxTextBox)
    //    //    {
    //    //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel6.Controls[i];
    //    //        txt.Text = "";
    //    //    }
    //    //    else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxComboBox)
    //    //    {
    //    //        DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel6.Controls[i];
    //    //        cmb.DataBind();
    //    //        //cmb.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
    //    //        cmb.SelectedIndex = -1;
    //    //    }
    //    //    else if (ASPxRoundPanel6.Controls[i] is DevExpress.Web.ASPxMemo)
    //    //    {
    //    //        DevExpress.Web.ASPxMemo memo = (DevExpress.Web.ASPxMemo)ASPxRoundPanel6.Controls[i];
    //    //        memo.Text = "";

    //    //    }
    //    //}
    //    txtSalNonpractical.Text
    //      = txtHoNonPractical.Text = txtSalPractical.Text = txtHoPractical.Text =
    //    txtSalWorkroom.Text = txtHoWorkroom.Text = txtTTeName.Text = txtTMajor.Text =
    //    txtTLicence.Text = txtTFileNo.Text = txtTMeId.Text = txtTPaye.Text = txtTeDesc.Text = "";
    //    cmbTeId.DataBind();
    //    cmbTeId.SelectedIndex = -1;
    //    dtOfPP = (DataTable)Session["PPTeacher"];
    //    dtOfPP.Rows.Clear();
    //    GridViewTeacher.DataSource = dtOfPP;
    //    GridViewTeacher.DataBind();

    //    TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
    //    GridViewSchedule.DataSource = SchManager.DataTable;
    //    GridViewSchedule.DataBind();
    //}


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
                    txtTestCost.Text = TestCost.ToString("#,#");
                }
                txtEndDate.Text = manager[0]["EndDate"].ToString();
                decimal PeriodCost = Convert.ToDecimal(manager[0]["PeriodCost"].ToString());
                txtPeriodCost.Text = PeriodCost.ToString("#,#");
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
                if (cmbCrsId.Items.FindByValue(manager[0]["CrsId"].ToString()) != null)
                {
                    cmbCrsId.SelectedIndex = cmbCrsId.Items.FindByValue(manager[0]["CrsId"].ToString()).Index;
                    cmbCrsId_SelectedIndexChanged(this, new EventArgs());
                }
                ODBPoll.SelectParameters["PollId"].DefaultValue = manager[0]["PollId"].ToString();
                cmbPoll.DataBind();
                if (cmbPoll.Items.FindByValue(manager[0]["PollId"].ToString()) != null)
                    cmbPoll.SelectedIndex = cmbPoll.Items.FindByValue(manager[0]["PollId"].ToString()).Index;

                cmbInstitue.DataBind();
                cmbInstitue.SelectedIndex = cmbInstitue.Items.FindByValue(manager[0]["InsId"].ToString()).Index;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">1: Teacher;2:Scheduale</param>
    /// <param name="Msg"></param>
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
        string PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForSave(PageMode);
        if (PageMode == "View" || PageMode == "Edit")
            CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int Permission = -1; int PermissionPrintReq = -1;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo, Utility.GetCurrentUser_UserId());
        PermissionPrintReq = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.PrindPeriodCertificatesSaveRequest, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || PermissionPrintReq > 0)
        {
            //BtnNew.Enabled = true;
            //BtnNew2.Enabled = true;
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
            //BtnNew.Enabled = false;
            //BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            ShowMessage("شما سطح دسترسی جهت درخواست ثبت دوره های آموزشی را ندارید.");
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string PPRId = Utility.DecryptQS(PeriodRequestId.Value);
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1; int PermissionPrintReq = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(PPRId), (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo, Utility.GetCurrentUser_UserId());
        PermissionPrintReq = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(PPRId), (int)TSP.DataManager.WorkFlowTask.PrindPeriodCertificatesSaveRequest, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || PermissionPrintReq > 0)
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
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده مشخصات دوره آموزشی", Utility.GetCurrentUser_UserId());
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

    private void CheckRegistrationCount(int PPId)
    {
        DivAlert.Visible = false;

        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtPP = PeriodPresentManager.SelectPeriodPresentHasEpayment(PPId);
        if (dtPP.Rows.Count > 0)
        {
            DataTable dtPR = PeriodRegisterManager.SelectPeriodRegisterForPeriod(-1, PPId, -1);
            if (dtPR.Rows.Count > 0)
            {
                txtPeriodCost.Enabled = txtTestCost.Enabled = false;
                DivAlert.Visible = true;
            }
        }

    }
    #endregion
}
