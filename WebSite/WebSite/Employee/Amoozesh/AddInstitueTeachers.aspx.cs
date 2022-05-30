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

public partial class Employee_Amoozesh_AddInstitueTeachers : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["InsTeId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("Institue.aspx");
                return;
            }
            TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            SetKey();
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString()));
            CheckCertificatePermission(InsId);
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

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnabledControls();
        HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldInsTeacher["InsTeacherId"] = "";
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueTeachers.aspx?InsId=" + HiddenFieldInsTeacher["InsId"] + "&PgMd=" + HiddenFieldInsTeacher["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldInsTeacher["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();

            }
            else if (PageMode == "Edit")
            {
                string InsTeacherId = Utility.DecryptQS(HiddenFieldInsTeacher["InsTeacherId"].ToString());
                if (string.IsNullOrEmpty(InsTeacherId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(InsTeacherId));
                }
            }
        }
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            HiddenFieldInsTeacher["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldInsTeacher["InsId"] = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();
            HiddenFieldInsTeacher["InsTeacherId"] = Server.HtmlDecode(Request.QueryString["InsTeId"]).ToString();
            HiddenFieldInsTeacher["PrePageMode"] = Server.HtmlDecode(Request.QueryString["PrPgMd"]).ToString();


            string PageMode = Utility.DecryptQS(HiddenFieldInsTeacher["PageMode"].ToString());
            string InsId = Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString());

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString())));

            if (InstitueManager.Count > 0)
            {
                lblInsName.Text = "موسسه: " + InstitueManager[0]["InsName"].ToString();
            }

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                SetMode(PageMode);
                //  CheckWorkFlowPermission();
            }
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode(string PageMode)
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
        string InsTeacherId = Utility.DecryptQS(HiddenFieldInsTeacher["InsTeacherId"].ToString());
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        txtDescription.Enabled = false;
        txtStartDate.Enabled = false;
        txtEndDate.Enabled = false;
        btnSearch1.Enabled = false;
        FillForm(int.Parse(InsTeacherId));

        RoundPanelInsTeacher.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        ClearForm();
        RoundPanelInsTeacher.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string InsTeacherId = Utility.DecryptQS(HiddenFieldInsTeacher["InsTeacherId"].ToString());
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (string.IsNullOrEmpty(InsTeacherId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnabledControls();
        FillForm(int.Parse(InsTeacherId));

        RoundPanelInsTeacher.HeaderText = "ویرایش";
    }

    private void EnabledControls()
    {
        txtStartDate.Enabled = true;
        txtEndDate.Enabled = true;
        txtDescription.Enabled = true;
        btnSearch1.Enabled = true;
    }

    protected void FillForm(int InsTeacherId)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldInsTeacher["PageMode"].ToString());

        TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        InstitueTeachersManager.FindByCode(InsTeacherId);
        if (InstitueTeachersManager.Count == 1)
        {
            txtStartDate.Text = InstitueTeachersManager[0]["StartDate"].ToString();
            txtEndDate.Text = InstitueTeachersManager[0]["EndDate"].ToString();
            txtDescription.Text = InstitueTeachersManager[0]["Description"].ToString();
            string TeId = InstitueTeachersManager[0]["TeId"].ToString();
            TeacherManager.FindByCode(int.Parse(TeId));
            if (TeacherManager.Count == 1)
            {
                txtFileNo.Text = TeacherManager[0]["FileNo"].ToString();
                txtName.Text = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
                txtFileDate.Text = TeacherManager[0]["FileDate"].ToString();
                cmbLicence.DataBind();
                cmbLicence.SelectedIndex = cmbLicence.Items.FindByValue(TeacherManager[0]["LiId"].ToString()).Index;
                cmbMajor.DataBind();
                cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(TeacherManager[0]["MjId"].ToString()).Index;
            }
        }
    }

    protected void ClearForm()
    {
        cmbLicence.SelectedIndex = -1;
        cmbMajor.SelectedIndex = -1;
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtFileDate.Text = "";
        txtFileNo.Text = "";
        txtName.Text = "";
        txtStartDate.Text = "";
    }

    protected void Disable()
    {
        txtStartDate.Enabled = false;
        txtEndDate.Enabled = false;
        txtDescription.Enabled = false;
    }

    protected void Enable()
    {
        txtDescription.Enabled = true;
        txtEndDate.Enabled = true;
        txtStartDate.Enabled = true;
    }

    private void Insert()
    {
        try
        {
            TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
            TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

            DataRow InsTeacherRow = InstitueTeachersManager.NewRow();
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString()));
            DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId, 0);
            if (dtInsCert.Rows.Count > 0)
            {
                int InsCId = int.Parse(dtInsCert.Rows[0]["InsCId"].ToString());
                InsTeacherRow["InsCId"] = InsCId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است.";
                return;
            }
            InsTeacherRow["InsId"] = int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString()));
            InsTeacherRow["TeId"] = int.Parse(HiddenFieldInsTeacher["TeId"].ToString());
            InsTeacherRow["StartDate"] = txtStartDate.Text;
            InsTeacherRow["EndDate"] = txtEndDate.Text;
            InsTeacherRow["Description"] = txtDescription.Text;
            InsTeacherRow["InActive"] = 0;
            InsTeacherRow["UserId"] = Utility.GetCurrentUser_UserId();
            InsTeacherRow["ModifiedDate"] = DateTime.Now;

            InstitueTeachersManager.AddRow(InsTeacherRow);
            int cn = InstitueTeachersManager.Save();
            if (cn > 0)
            {
                HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldInsTeacher["InsTeacherId"] = Utility.EncryptQS(InstitueTeachersManager[0]["InsTeacherId"].ToString());
                RoundPanelInsTeacher.HeaderText = "ویرایش";
                TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;

                TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();

                TeacherManager.FindByCode(int.Parse(HiddenFieldInsTeacher["TeId"].ToString()));
                if (TeacherManager.Count == 1)
                {
                    txtFileNo.Text = TeacherManager[0]["FileNo"].ToString();
                    txtName.Text = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
                    txtFileDate.Text = TeacherManager[0]["FileDate"].ToString();
                    cmbLicence.DataBind();
                    cmbLicence.SelectedIndex = cmbLicence.Items.FindByValue(TeacherManager[0]["LiId"].ToString()).Index;
                    cmbMajor.DataBind();
                    cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(TeacherManager[0]["MjId"].ToString()).Index;
                }

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است.";
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

    protected void Edit(int InsTeacherId)
    {

        TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
        InstitueTeachersManager.FindByCode(InsTeacherId);
        if (InstitueTeachersManager.Count == 1)
        {
            try
            {
                InstitueTeachersManager[0].BeginEdit();
                InstitueTeachersManager[0]["StartDate"] = txtStartDate.Text;
                InstitueTeachersManager[0]["TeId"] = int.Parse(HiddenFieldInsTeacher["TeId"].ToString());
                InstitueTeachersManager[0]["EndDate"] = txtEndDate.Text;
                InstitueTeachersManager[0]["Description"] = txtDescription.Text;
                InstitueTeachersManager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                InstitueTeachersManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueTeachersManager[0].EndEdit();
                int cn = InstitueTeachersManager.Save();
                if (cn == 1)
                {
                    HiddenFieldInsTeacher["InsTeacherId"] = Utility.EncryptQS(InstitueTeachersManager[0]["InsTeacherId"].ToString());
                    HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
                    RoundPanelInsTeacher.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
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
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }


    }

    private int FindInstitueCertificate(int InsId)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId, 0);
        int InsCertType = -1;
        if (dtInsCert.Rows.Count > 0)
        {
            InsCertType = int.Parse(dtInsCert.Rows[0]["Type"].ToString());
        }
        return InsCertType;


    }

    private void CheckCertificatePermission(int InsId)
    {
        int CertType = FindInstitueCertificate(InsId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldInsTeacher["PageMode"].ToString());
        if (CertType == 1 || CertType == 2)
        {
            switch (PageMode)
            {
                case "View":

                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    break;
                case "Edit":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion
}
