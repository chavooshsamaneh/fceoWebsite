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

public partial class Members_Amoozesh_AddPreRegister : System.Web.UI.Page
{
    string PageMode;
    string PRegisterId;
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PRegisterId"]))
        {
            Response.Redirect("PreRegister.aspx");
            return;
        }
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
            cmbCourse.SelectedIndex = 0;
            ObjdsInstitue.SelectParameters["InActive"].DefaultValue = "0";
            SetKeys();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnDelete"] != null)
        //  this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnabledControls();
        RoundPanelPreRegister.HeaderText = "جدید";
        HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldPreRegister["PRegisterId"] = Utility.EncryptQS("-1");

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnabledControls();
        HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("Edit");
        RoundPanelPreRegister.HeaderText = "ویرایش";
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        //        btnDelete.Enabled = true;
        //        btnDelete2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PreRegister.aspx");
    }

    protected void GridViewCourseHours_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

        PageMode = Utility.DecryptQS(HiddenFieldPreRegister["PageMode"].ToString());
        if (PageMode == "View")
        {
            e.Row.Enabled = false;
        }
        else
        {
            e.Row.Enabled = true;
        }


        // chbMorning.Checked = (Boolean)(CourseHoursManager.DataTable.Rows[0]["Morning"]);
        //chbAfternoon.Checked = (Boolean)(CourseHoursManager.DataTable.Rows[0]["Noon"]);
        //chbNoon.Checked = (Boolean)(CourseHoursManager.DataTable.Rows[0]["Afternoon"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        string PageMode = Utility.DecryptQS(HiddenFieldPreRegister["PageMode"].ToString());

        string PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertPreRegister();

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(PRegisterId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditPreRegister(int.Parse(PRegisterId));
                }

            }
        }
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

    }

    #endregion

    #region Methods
    private void FillGrid()
    {
        PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());
        // HDMorning.Clear();
        TSP.DataManager.CourseHoursManager CourseHoursManager = (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
        if (CourseHoursManager.DataTable.Rows.Count == 0)
        {
            for (int i = 0; i < 7; i++)
            {
                DataRow CourseHourseRow = CourseHoursManager.NewRow();
                switch (i)
                {
                    case 0:
                        CourseHourseRow["DayName"] = "شنبه";
                        break;
                    case 1:
                        CourseHourseRow["DayName"] = "یکشنبه";
                        break;
                    case 2:
                        CourseHourseRow["DayName"] = "دوشنبه";
                        break;
                    case 3:
                        CourseHourseRow["DayName"] = "سه شنبه";
                        break;
                    case 4:
                        CourseHourseRow["DayName"] = "چهارشنبه";
                        break;
                    case 5:
                        CourseHourseRow["DayName"] = "پنجشنبه";
                        break;
                    case 6:
                        CourseHourseRow["DayName"] = "جمعه";
                        break;
                }

                CourseHourseRow["Morning"] = 0;
                CourseHourseRow["Noon"] = 0;
                CourseHourseRow["Afternoon"] = 0;
                CourseHourseRow["PRegisterId"] = int.Parse(PRegisterId);
                CourseHourseRow["UserId"] = Utility.GetCurrentUser_UserId();
                CourseHourseRow["ModifiedDate"] = DateTime.Now;
                CourseHoursManager.AddRow(CourseHourseRow);
            }
            Session["CourseHourseManager"] = CourseHoursManager;

        }
        else
        {
            for (int i = 0; i < CourseHoursManager.Count; i++)
            {
                ViewState["Mode"] = "Edit";
                if (Convert.ToBoolean(CourseHoursManager[i]["Morning"]))
                    HDMorning.Add("m" + CourseHoursManager[i]["CrsHoursId"].ToString(), CourseHoursManager[i]["Morning"]);
                //if (Convert.ToBoolean(CourseHoursManager[i]["Noon"]))
                //    HDNoon.Add("n" + CourseHoursManager[i]["CrsHoursId"].ToString(), CourseHoursManager[i]["Noon"]);
                if (Convert.ToBoolean(CourseHoursManager[i]["Afternoon"]))
                    HDAfterNoon.Add("a" + CourseHoursManager[i]["CrsHoursId"].ToString(), CourseHoursManager[i]["Afternoon"]);
            }
        }
        GridViewCourseHours.DataSource = CourseHoursManager.DataTable;
        GridViewCourseHours.KeyFieldName = "CrsHoursId";
        GridViewCourseHours.DataBind();
    }

    private TSP.DataManager.CourseHoursManager CreateCourseHoursManager()
    {
        TSP.DataManager.CourseHoursManager manager = new TSP.DataManager.CourseHoursManager();
        PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());
        manager.FindByPRegisterId(int.Parse(PRegisterId));

        return manager;
    }

    private void SetKeys()
    {
        HiddenFieldPreRegister["PRegisterId"] = Request.QueryString["PRegisterId"].ToString();
        HiddenFieldPreRegister["PageMode"] = Request.QueryString["PageMode"];
        PageMode = Utility.DecryptQS(HiddenFieldPreRegister["PageMode"].ToString());
        PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());
        Session["CourseHourseManager"] = CreateCourseHoursManager();
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode();
    }

    private void SetMode()
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        btnNew.Enabled = true;
        btnNew2.Enabled = true;


        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        //        btnDelete.Enabled = true;
        //        btnDelete2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        cmbCourse.ClientEnabled = false;
        cmbinstitue.ClientEnabled = false;
        cmbTeacher.ClientEnabled = false;
        GridViewCourseHours.Enabled = false;

        FillForm(int.Parse(PRegisterId));

        RoundPanelPreRegister.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //        btnDelete.Enabled = false;
        //        btnDelete2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        FillGrid();
        RoundPanelPreRegister.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //ُSet Button's Enable

        //            btnDelete.ClientEnabled = true;
        //            btnDelete.ClientEnabled = true;

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        if (string.IsNullOrEmpty(PRegisterId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnabledControls();
        FillForm(int.Parse(PRegisterId));
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;


        RoundPanelPreRegister.HeaderText = "ویرایش";
    }

    private void EnabledControls()
    {
        cmbCourse.ClientEnabled = true;
        cmbinstitue.ClientEnabled = true;
        cmbTeacher.ClientEnabled = true;
        GridViewCourseHours.Enabled = true;

    }

    private void FillForm(int PRegisterId)
    {
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();
        PreRegisterManager.FindByCode(PRegisterId);

        if (PreRegisterManager.Count > 0)
        {
            cmbCourse.DataBind();
            cmbTeacher.DataBind();
            cmbinstitue.DataBind();
            ObjdsInstitue.SelectParameters["InActive"].DefaultValue = "0";
            cmbinstitue.DataBind();
            cmbinstitue.SelectedIndex = cmbinstitue.Items.IndexOfValue(PreRegisterManager[0]["InsId"].ToString());
            cmbCourse.SelectedIndex = cmbCourse.Items.IndexOfValue(PreRegisterManager[0]["CrsId"].ToString());
            cmbTeacher.SelectedIndex = cmbTeacher.Items.IndexOfValue(PreRegisterManager[0]["TeId"].ToString());
            FillGrid();
        }
        else
        {
            Response.Redirect("PreRegister.aspx");
            return;
        }
    }

    private void ClearForm()
    {
        cmbTeacher.DataBind();
        cmbTeacher.SelectedIndex = 0;
        cmbinstitue.SelectedIndex = 0;
        cmbCourse.SelectedIndex = 0;
        ObjdsInstitue.SelectParameters["InActive"].DefaultValue = "0";
        ResetGrid();
    }

    private void ResetGrid()
    {
        HDAfterNoon.Clear();
        HDMorning.Clear();
        //HDNoon.Clear();
        TSP.DataManager.CourseHoursManager manager = new TSP.DataManager.CourseHoursManager();
        Session["CourseHourseManager"] = manager;
        FillGrid();
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void InsertPreRegister()
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();

        int MeId = Utility.GetCurrentUser_MeId();
        TransactionManager.Add(CourseHoursManager);
        TransactionManager.Add(PreRegisterManager);

        try
        {
            TransactionManager.BeginSave();
            DataRow PreRegisterRow = PreRegisterManager.NewRow();
            PreRegisterRow["MeId"] = MeId;
            PreRegisterRow["CrsId"] = cmbCourse.SelectedItem.Value.ToString();
            if (cmbTeacher.SelectedItem != null)
                PreRegisterRow["TeId"] = cmbTeacher.SelectedItem.Value.ToString();
            if (cmbinstitue.SelectedItem != null)
                PreRegisterRow["InsId"] = cmbinstitue.SelectedItem.Value.ToString();
            PreRegisterRow["RegisteringDate"] = Utility.GetDateOfToday();
            PreRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
            PreRegisterRow["ModifiedDate"] = DateTime.Now;

            PreRegisterManager.AddRow(PreRegisterRow);

            int cn = PreRegisterManager.Save();
            if (cn > 0)
            {
                InsertCourseHours(int.Parse(PreRegisterManager[0]["PRegisterId"].ToString()), CourseHoursManager, TransactionManager);
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    private void InsertCourseHours(int PRegisterId, TSP.DataManager.CourseHoursManager CourseHoursManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        GridViewCourseHours.DataSource = CourseHoursManager.DataTable;
        Boolean IsCourseHoursSelected = false;
        for (int i = 0; i < GridViewCourseHours.VisibleRowCount; i++)
        {
            DataRow gridRow = GridViewCourseHours.GetDataRow(i);
            DataRow CourseHoursRow = CourseHoursManager.DataTable.Rows.Find(int.Parse(gridRow["CrsHoursId"].ToString()));
            CourseHoursRow.BeginEdit();
            if (HDMorning.Contains("m" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()))
            {
                CourseHoursRow["Morning"] = (bool)HDMorning["m" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()];
                if ((bool)HDMorning["m" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()]
                   && !IsCourseHoursSelected)
                {
                    IsCourseHoursSelected = true;
                }
            }
            else
                CourseHoursRow["Morning"] = false;
            //if (HDNoon.Contains("n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()))
            //{
            //    CourseHoursRow["Noon"] = (bool)HDNoon["n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()];
            //    if ((bool)HDNoon["n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()]
            //       && !IsCourseHoursSelected)
            //    {
            //        IsCourseHoursSelected = true;
            //    }
            //}
            //else
            //    CourseHoursRow["Noon"] = false;
            if (HDAfterNoon.Contains("a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()))
            {
                CourseHoursRow["Noon"] = CourseHoursRow["Afternoon"] = (bool)HDAfterNoon["a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()];
                if ((bool)HDAfterNoon["a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()]
                  && !IsCourseHoursSelected)
                {
                    IsCourseHoursSelected = true;
                }
            }
            else
                CourseHoursRow["Noon"] = CourseHoursRow["Afternoon"] = false;

            CourseHoursRow["DayName"] = gridRow["DayName"];
            CourseHoursRow["PRegisterId"] = PRegisterId;
            CourseHoursRow["UserId"] = Utility.GetCurrentUser_UserId();
            CourseHoursRow["ModifiedDate"] = DateTime.Now;

            CourseHoursRow.EndEdit();
        }
        if (!IsCourseHoursSelected)
        {
            TransactionManager.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "روز و ساعت پیشنهادی خود را انتخاب نمایید";
            return;
        }
        if (CourseHoursManager.Save() > 0)
        {
            TransactionManager.EndSave();
            HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldPreRegister["PRegisterId"] = Utility.EncryptQS(PRegisterId.ToString());
            ResetGrid();
            Session["CourseHourseManager"] = CreateCourseHoursManager();
            FillGrid();
            RoundPanelPreRegister.HeaderText = "ویرایش";
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره با موفقیت انجام شد.";
        }
        else
        {
            TransactionManager.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        }

    }

    private Boolean DeleteCourseHours(int PRegisterId, TSP.DataManager.CourseHoursManager CourseHoursManager)
    {
        CourseHoursManager.FindByPRegisterId(PRegisterId);
        for (int i = 0; i < CourseHoursManager.Count; i++)
        {
            CourseHoursManager[i].Delete();
        }
        if (CourseHoursManager.Save() > 0)
        {
            return true;
        }
        else
        {
            return false;
            DivReport.Visible = true;
            LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
        return true;
    }

    private void EditPreRegister(int PRegisterId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();

        int MeId = Utility.GetCurrentUser_MeId();
        TransactionManager.Add(CourseHoursManager);
        TransactionManager.Add(PreRegisterManager);

        try
        {
            TransactionManager.BeginSave();

            PreRegisterManager.FindByCode(PRegisterId);

            PreRegisterManager[0].BeginEdit();

            PreRegisterManager[0]["MeId"] = MeId;
            PreRegisterManager[0]["CrsId"] = cmbCourse.SelectedItem.Value.ToString();
            if (cmbTeacher.SelectedItem != null)
                PreRegisterManager[0]["TeId"] = cmbTeacher.SelectedItem.Value.ToString();
            if (cmbinstitue.SelectedItem != null)
                PreRegisterManager[0]["InsId"] = cmbinstitue.SelectedItem.Value.ToString();
            PreRegisterManager[0]["RegisteringDate"] = Utility.GetDateOfToday();
            PreRegisterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PreRegisterManager[0]["ModifiedDate"] = DateTime.Now;

            PreRegisterManager[0].EndEdit();

            int cn = PreRegisterManager.Save();
            if (cn > 0)
            {
                //if (!DeleteCourseHours(PRegisterId, CourseHoursManager))
                //{
                //    TransactionManager.CancelSave();
                //    return;
                //}
                InsertCourseHours(int.Parse(PreRegisterManager[0]["PRegisterId"].ToString()), CourseHoursManager, TransactionManager);
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        }
    }
    #endregion

}
