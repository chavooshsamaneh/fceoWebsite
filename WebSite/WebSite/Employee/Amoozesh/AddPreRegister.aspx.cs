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

public partial class Employee_Amoozesh_AddPreRegister : System.Web.UI.Page
{
    string PageMode;
    string PRegisterId;

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
            HiddenFieldPreRegister["CitId"] = "";
            HiddenFieldPreRegister["CitName"] = "";
            TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;                     

            cmbCourse.SelectedIndex = 0;
            cmbinstitue.SelectedIndex = 0;
            cmbTeacher.SelectedIndex = 0;

            //Session["CourseHourseManager"] = CreateCourseHoursManager();
            SetKeys();


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;            
            this.ViewState["BtnNew"] = btnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];        
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        //if (this.ViewState["btnSearch1"] != null)
        //    this.btnSearch1.Enabled = (bool)this.ViewState["btnSearch1"];
        if (this.ViewState["btnSearchCity"] != null)
            this.btnSearchCity.Enabled = (bool)this.ViewState["btnSearchCity"];
        if (HiddenFieldPreRegister["CitName"] != null && (!string.IsNullOrEmpty(HiddenFieldPreRegister["CitName"].ToString())))
            txtCity.Text = HiddenFieldPreRegister["CitName"].ToString();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        RoundPanelPreRegister.HeaderText = "جدید";
        HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldPreRegister["PRegisterId"] = "";
        TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        
        this.ViewState["BtnNew"] = btnNew.Enabled;        
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnabledControls();
        HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("Edit");
        RoundPanelPreRegister.HeaderText = "ویرایش";
        TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled =per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        
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

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        //if ((!string.IsNullOrEmpty(txtMeNo.Text)) && (cmbCourse.SelectedIndex > -1))
        //{
        //    int MeId =int.Parse(txtMeNo.Text);
        //    TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();            
        //    //DataTable dtPreRegister = PreRegisterManager.SelectByMeId(int.Parse(MeId));            
        //    int CrsId = int.Parse(cmbCourse.SelectedItem.Value.ToString());
        //    PreRegisterManager.FindByMeId(MeId, CrsId);
        //    if (PreRegisterManager.Count == 1)
        //    {

        //    }
        //}
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        try
        {
            string MeId = txtMeNo.Text;
            if (!string.IsNullOrEmpty(MeId))
            {
                MeManager.FindByCode(int.Parse(MeId));
                if (MeManager.Count > 0)
                {
                    txtName.Text = MeManager[0]["FirstName"].ToString();
                    txtFamily.Text = MeManager[0]["LastName"].ToString();

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد عضویت را وارد نمایید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را وارد نمایید";
                return;
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
        }
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

    }

    protected void cmbinstitue_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        string CitId = e.Parameter;
        if (!string.IsNullOrEmpty(CitId))
        {
            ObjdsInstitue.SelectParameters[0].DefaultValue = CitId;
            cmbinstitue.DataBind();
            cmbinstitue.SelectedIndex = 0;
            HiddenFieldPreRegister["CitId"] = CitId;
            CityManager.FindByCode(int.Parse(CitId));
            if(CityManager.Count==1)
                HiddenFieldPreRegister["CitName"] = CityManager[0]["CitName"].ToString();
        }
    }

    #endregion

    #region Methods
    private void FillGrid()
    {
        
        PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());
        
        TSP.DataManager.CourseHoursManager CourseHoursManager= (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
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
                if (Convert.ToBoolean(CourseHoursManager[i]["Noon"]))
                    HDNoon.Add("n" + CourseHoursManager[i]["CrsHoursId"].ToString(), CourseHoursManager[i]["Noon"]);
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
       // HiddenFieldPreRegister["MeId"] = Request.QueryString["MeId"];
      //  TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
       // string MeId = Utility.DecryptQS(HiddenFieldPreRegister["MeId"].ToString());
        //MemberManager.FindByCode(int.Parse(MeId));
        //lblMemberName.Text = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
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
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.ClientEnabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.ClientEnabled = true;
            btnEdit2.ClientEnabled = true;
        }       

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        
        this.ViewState["BtnNew"] = btnNew.Enabled;


        //Set Textboxe's & comboboxe's Enabled
        cmbCourse.ClientEnabled = false;
        cmbinstitue.ClientEnabled=false;
        cmbTeacher.ClientEnabled = false;
        GridViewCourseHours.Enabled = false;
        txtName.Enabled = false;
        txtMeNo.Enabled = false;
        txtFamily.Enabled = false;

       // btnSearch1.Enabled = false;
        btnSearchCity.Enabled = false;
        //this.ViewState["btnSearch1"] = btnSearch1.Enabled;
        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        FillForm(int.Parse(PRegisterId));

        RoundPanelPreRegister.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        

        ClearForm();
        FillGrid();
        RoundPanelPreRegister.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        btnSave.ClientEnabled = per.CanEdit;
        btnSave2.ClientEnabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        

        if (string.IsNullOrEmpty(PRegisterId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnabledControls();
        FillForm(int.Parse(PRegisterId));

        RoundPanelPreRegister.HeaderText = "ویرایش";
    }

    private void EnabledControls()
    {
        cmbCourse.ClientEnabled = true;
        cmbinstitue.ClientEnabled = true;
        cmbTeacher.ClientEnabled = true;
        txtName.Enabled = true;
        txtMeNo.Enabled = true;
        txtFamily.Enabled = true;
        GridViewCourseHours.Enabled = true;

    }

    private void FillForm(int PRegisterId)
    {
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        PreRegisterManager.FindByCode(PRegisterId);

        if (PreRegisterManager.Count > 0)
        {
            cmbCourse.DataBind();
            cmbTeacher.DataBind();
            cmbinstitue.DataBind();
             string CitId="";
             if (!Utility.IsDBNullOrNullValue(PreRegisterManager[0]["CitId"]))
             {
                 CitId = PreRegisterManager[0]["CitId"].ToString();
                 if (!string.IsNullOrEmpty(CitId))
                 {
                     CityManager.FindByCode(int.Parse(CitId));
                     if (CityManager.Count == 1)
                     {
                         ObjdsInstitue.SelectParameters[0].DefaultValue = CitId;
                         cmbinstitue.DataBind();
                         cmbinstitue.SelectedIndex = cmbinstitue.Items.IndexOfValue(PreRegisterManager[0]["InsId"].ToString());
                         txtCity.Text = CityManager[0]["CitName"].ToString();
                     }
                     else
                     {
                         this.DivReport.Visible = true;
                         this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگر تغییر یافته است.";
                     }
                 }
                 else
                 {
                     ObjdsInstitue.SelectParameters[0].DefaultValue = CitId;
                     cmbinstitue.DataBind();
                 }
             }
             else
             {
                 ObjdsInstitue.SelectParameters[0].DefaultValue = CitId;
                 cmbinstitue.DataBind();
             }
            cmbCourse.SelectedIndex = cmbCourse.Items.IndexOfValue(PreRegisterManager[0]["CrsId"].ToString());
            cmbTeacher.SelectedIndex = cmbTeacher.Items.IndexOfValue(PreRegisterManager[0]["TeId"].ToString());
            txtMeNo.Text = PreRegisterManager[0]["MeId"].ToString();
            txtFamily.Text = PreRegisterManager[0]["MeFamily"].ToString();
            txtName.Text = PreRegisterManager[0]["MeName"].ToString();
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
        txtCity.Text = "";
        txtMeNo.Text = "";
        txtFamily.Text = "";
        txtName.Text = "";
        cmbTeacher.SelectedIndex = 0;
        cmbinstitue.SelectedIndex = 0;
        cmbCourse.SelectedIndex = 0;
        ObjdsInstitue.SelectParameters[0].DefaultValue = "";
        ResetGrid();
    }

    private void ResetGrid()
    {
        HDAfterNoon.Clear();
        HDMorning.Clear();
        HDNoon.Clear();
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
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();

        int MeId = int.Parse(txtMeNo.Text); //int.Parse(Utility.DecryptQS(HiddenFieldPreRegister["MeId"].ToString()));
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
            if (!string.IsNullOrEmpty(HiddenFieldPreRegister["CitId"].ToString()))
                 PreRegisterRow["CitId"] = HiddenFieldPreRegister["CitId"];
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
           // DataRow CourseHoursRow = CourseHoursManager.NewRow();
            DataRow gridRow = GridViewCourseHours.GetDataRow(i);
            DataRow CourseHoursRow= CourseHoursManager.DataTable.Rows.Find(int.Parse(gridRow["CrsHoursId"].ToString()));
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
            if (HDNoon.Contains("n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()))
            {
                CourseHoursRow["Noon"] = (bool)HDNoon["n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()];
                if ((bool)HDNoon["n" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()]
                    && !IsCourseHoursSelected)
                {
                    IsCourseHoursSelected = true;
                }
            }
            else
                CourseHoursRow["Noon"] = false;
            if (HDAfterNoon.Contains("a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()))
            {
                CourseHoursRow["Afternoon"] = (bool)HDAfterNoon["a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()];
                if ((bool)HDAfterNoon["a" + Math.Abs(int.Parse(gridRow["CrsHoursId"].ToString())).ToString()]
                    && !IsCourseHoursSelected)
                {
                    IsCourseHoursSelected = true;
                }
            }
            else
                CourseHoursRow["Afternoon"] = false;

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
        int cn = CourseHoursManager.Save();
        if (cn > 0)
        {           
            TransactionManager.EndSave();
            HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldPreRegister["PRegisterId"] = Utility.EncryptQS(PRegisterId.ToString());
            ResetGrid();
            Session["CourseHourseManager"] = CreateCourseHoursManager();
            FillGrid();
            RoundPanelPreRegister.HeaderText = "ویرایش";
            TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
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

    private void DeletePreRegister(int PRegisterId)
    {
        try
        {
            TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
            TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();
            TSP.DataManager.CourseHoursManager CourseHoursManager = new TSP.DataManager.CourseHoursManager();
            TransactionManager.Add(PreRegisterManager);
            TransactionManager.Add(CourseHoursManager);

            TransactionManager.BeginSave();

            CourseHoursManager.FindByPRegisterId(PRegisterId);

            if (CourseHoursManager.Count > 0)
            {
                int CrsHourseCount = CourseHoursManager.Count;
                for (int i = 0; i < CrsHourseCount; i++)
                {
                    CourseHoursManager[0].Delete();
                }
                int cn = CourseHoursManager.Save();
                if (cn > 0)
                {
                    PreRegisterManager.FindByCode(PRegisterId);
                    if (PreRegisterManager.Count > 0)
                    {
                        PreRegisterManager[0].Delete();

                        int cnt = PreRegisterManager.Save();
                        if (cnt > 0)
                        {
                            TransactionManager.EndSave();
                            ClearForm();
                            cmbCourse.Enabled= true;
                            HiddenFieldPreRegister["PRegisterId"] = "";
                            HiddenFieldPreRegister["PageMode"] = Utility.EncryptQS("New");
                            TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                            btnNew.Enabled = per.CanNew;
                            btnNew2.Enabled = per.CanNew;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            btnSave.Enabled = per.CanNew;
                            btnSave2.Enabled = per.CanNew;
                            this.ViewState["BtnSave"] = btnSave.Enabled;
                            this.ViewState["BtnEdit"] = btnEdit.Enabled;                            
                            this.ViewState["BtnNew"] = btnNew.Enabled;
    
                            DivReport.Visible = true;
                            LabelWarning.Text = "ذخیره انجام شد.";
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            DivReport.Visible = true;
                            LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        DivReport.Visible = true;
                        LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }          
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void EditPreRegister(int PRegisterId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = (TSP.DataManager.CourseHoursManager)(Session["CourseHourseManager"]);
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();

        int MeId = int.Parse(txtMeNo.Text);
        TransactionManager.Add(CourseHoursManager);
        TransactionManager.Add(PreRegisterManager);

        try
        {
            TransactionManager.BeginSave();

            PreRegisterManager.FindByCode(PRegisterId);

            PreRegisterManager[0].BeginEdit();

            PreRegisterManager[0]["MeId"] = MeId;
            PreRegisterManager[0]["CrsId"] = cmbCourse.SelectedItem.Value.ToString();
            if(cmbTeacher.SelectedItem!=null)
                PreRegisterManager[0]["TeId"] = cmbTeacher.SelectedItem.Value.ToString();
            if (!string.IsNullOrEmpty(HiddenFieldPreRegister["CitId"].ToString()))
                PreRegisterManager[0]["CitId"]  = HiddenFieldPreRegister["CitId"];
            if(cmbinstitue.SelectedItem!= null)
                PreRegisterManager[0]["InsId"] = cmbinstitue.SelectedItem.Value.ToString();
            PreRegisterManager[0]["RegisteringDate"] = Utility.GetDateOfToday();
            PreRegisterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PreRegisterManager[0]["ModifiedDate"] = DateTime.Now;

            PreRegisterManager[0].EndEdit();

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
