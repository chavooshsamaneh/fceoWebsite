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

public partial class Employee_Amoozesh_AddTeacherSalary : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (Request.QueryString["PgMd"] == null || Request.QueryString["SalaryId"] == null)
        {
            Response.Redirect("TeachersSalary.aspx");
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKey();


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        HiddenFieldTeSalary["SalaryId"] = Utility.EncryptQS("");
       HiddenFieldTeSalary["PageMode"] = Utility.EncryptQS("New");
        RoundPanelTeSalary.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
       
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        ClearForm();
        Enable();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(HiddenFieldTeSalary["PageMode"].ToString());
        string SalaryId = Utility.DecryptQS(HiddenFieldTeSalary["SalaryId"].ToString());

        if (string.IsNullOrEmpty(SalaryId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();

                TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
              
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                HiddenFieldTeSalary["PageMode"] = Utility.EncryptQS("Edit");
                RoundPanelTeSalary.HeaderText = "ویرایش";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTeSalary["PageMode"].ToString());

        string SalaryId = Utility.DecryptQS(HiddenFieldTeSalary["SalaryId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTeacherSalary();

                //Response.Redirect("AddCourse.aspx?TeId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(SalaryId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditTeacherSalary(int.Parse(SalaryId));
                }

            }

        }
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeachersSalary.aspx");
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            HiddenFieldTeSalary["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldTeSalary["SalaryId"] = Server.HtmlDecode(Request.QueryString["SalaryId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldTeSalary["PageMode"].ToString());
        string SalaryId = Utility.DecryptQS(HiddenFieldTeSalary["SalaryId"].ToString());
       
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);           
        }
    }

    private void SetMode(string PageMode)
    {       
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
        string SalaryId = Utility.DecryptQS(HiddenFieldTeSalary["SalaryId"].ToString());
        if (string.IsNullOrEmpty(SalaryId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
         
        }

        btnSave.Enabled = false;
        btnSave2.Enabled = false;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        this.ViewState["BtnNew"] = BtnNew.Enabled;

        FillForm(int.Parse(SalaryId));

        Disable();
        RoundPanelTeSalary.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        Enable();
        ClearForm();

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        RoundPanelTeSalary.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string SalaryId = Utility.DecryptQS(HiddenFieldTeSalary["SalaryId"].ToString());
        //*****Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        if (string.IsNullOrEmpty(SalaryId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Enable();
        FillForm(int.Parse(SalaryId));
       
        RoundPanelTeSalary.HeaderText = "ویرایش";
    }

    private void FillForm(int SalaryId)
    {
        TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
        TeachersSalaryManager.FindByCode(SalaryId);
        if (TeachersSalaryManager.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(TeachersSalaryManager[0]["StartDate"]))
            txtDate.Text = TeachersSalaryManager[0]["StartDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(TeachersSalaryManager[0]["SalaryNonPractical"]))
            txtNonPractical.Text = TeachersSalaryManager[0]["SalaryNonPractical"].ToString();

        if (!Utility.IsDBNullOrNullValue(TeachersSalaryManager[0]["SalaryPractical"]))
            txtPractical.Text = TeachersSalaryManager[0]["SalaryPractical"].ToString();

        if (!Utility.IsDBNullOrNullValue(TeachersSalaryManager[0]["SalaryWorkroom"]))
            txtWorkRoom.Text = TeachersSalaryManager[0]["SalaryWorkroom"].ToString();
            cmbLicence.DataBind();
            if(!Utility.IsDBNullOrNullValue(TeachersSalaryManager[0]["LiId"]))
                cmbLicence.SelectedIndex = cmbLicence.Items.FindByValue(TeachersSalaryManager[0]["LiId"].ToString()).Index;
        }       
    }

    private void ClearForm()
    {
        txtWorkRoom.Text = "";
        txtPractical.Text = "";
        txtNonPractical.Text = "";
        txtDate.Text = "";
        cmbLicence.SelectedIndex = 0;
    }

    private void Disable()
    {
        txtDate.Enabled = false;
        txtNonPractical.Enabled = false;
        txtPractical.Enabled = false;
        txtWorkRoom.Enabled = false;
        cmbLicence.Enabled = false;
    }

    private void Enable()
    {
        txtDate.Enabled = true;
        txtNonPractical.Enabled = true;
        txtPractical.Enabled = true;
        txtWorkRoom.Enabled = true;
        cmbLicence.Enabled = true;
    }

    private void InsertTeacherSalary()
    {
        try
        {
            TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
            DataRow SalaryRow = TeachersSalaryManager.NewRow();          
            SalaryRow["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString());
            SalaryRow["SalaryPractical"] = txtPractical.Text;
            SalaryRow["SalaryNonPractical"] =txtNonPractical.Text;
            SalaryRow["SalaryWorkroom"] = txtWorkRoom.Text;         
            SalaryRow["StartDate"] = txtDate.Text;
            SalaryRow["UserId"] = Utility.GetCurrentUser_UserId();
            SalaryRow["ModifiedDate"] = DateTime.Now;
            TeachersSalaryManager.AddRow(SalaryRow);
            int cn = TeachersSalaryManager.Save();
            if (cn > 0)
            {
                TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;

                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                HiddenFieldTeSalary["PageMode"] = Utility.EncryptQS("New");
                HiddenFieldTeSalary["SalaryId"] = Utility.EncryptQS(SalaryRow["SalaryId"].ToString());

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
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
    }

    private void EditTeacherSalary(int SalaryId)
    {
        try
        {
            TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            if (LoginManager.Count < 0)
            {
                Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                return;
            }

            TeachersSalaryManager.FindByCode(SalaryId);
            if (TeachersSalaryManager.Count>0)
            {
                TeachersSalaryManager[0].BeginEdit();
                TeachersSalaryManager[0]["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString());
                TeachersSalaryManager[0]["SalaryPractical"] =txtPractical.Text;
                TeachersSalaryManager[0]["SalaryNonPractical"] =txtNonPractical.Text;
                TeachersSalaryManager[0]["SalaryWorkroom"] = txtWorkRoom.Text;               
                TeachersSalaryManager[0]["StartDate"] = txtDate.Text;
                TeachersSalaryManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeachersSalaryManager[0]["ModifiedDate"] = DateTime.Now;
                TeachersSalaryManager[0].EndEdit();
                int cn = TeachersSalaryManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره با انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
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
    }

    private void DeleteTeacherSalary(int SalaryId)
    {
        TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
        TeachersSalaryManager.FindByCode(SalaryId);
        if (TeachersSalaryManager.Count > 0)
        {
            TeachersSalaryManager[0].Delete();
            int cn = TeachersSalaryManager.Save();

            if (cn > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }
    #endregion   
}
