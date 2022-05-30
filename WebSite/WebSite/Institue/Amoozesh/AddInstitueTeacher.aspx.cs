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

public partial class Institue_Amoozesh_AddInstitueTeacher : System.Web.UI.Page
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
                Response.Redirect("InstitueHome.aspx");
                return;
            }
            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["btnDisActiveImg"] != null)
            this.btnDisActive.Image.Url = this.btnDisActive2.Image.Url = (string)this.ViewState["btnDisActiveImg"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnabledControls();
        HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldInsTeacher["InsTeacherId"] = "";

        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnDisActive.Enabled = false;
        btnDisActive2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnabledControls();
        HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");

        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnDisActive.Enabled = true;
        btnDisActive2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
        string InsTeacherId = Utility.DecryptQS(HiddenFieldInsTeacher["InsTeacherId"].ToString());
        InstitueTeachersManager.FindByCode(int.Parse(InsTeacherId));
        if (Convert.ToBoolean(InstitueTeachersManager[0]["InActive"].ToString()))
        {
            Active(int.Parse(InsTeacherId));
        }
        else
        {
            InActive(int.Parse(InsTeacherId));
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueTeachers.aspx?InsId=" + HiddenFieldInsTeacher["InsId"]);
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
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldInsTeacher["PageMode"].ToString());
        string InsId = Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString());
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

        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        btnNew.Enabled = true;
        btnNew2.Enabled = true;

        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        btnDisActive.Enabled = true;
        btnDisActive2.Enabled = true;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
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
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnDisActive.Enabled = false;
        btnDisActive2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        RoundPanelInsTeacher.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string InsTeacherId = Utility.DecryptQS(HiddenFieldInsTeacher["InsTeacherId"].ToString());

        //ُSet Button's Enable
        btnDisActive.Enabled = true;
        btnDisActive2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;

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
            ChangeDisableButtonIcon(Convert.ToBoolean(InstitueTeachersManager[0]["InActive"].ToString()));
            string TeId = InstitueTeachersManager[0]["TeId"].ToString();
            HiddenFieldInsTeacher["TeId"] = TeId;
            HiddenFieldInsTeacher["CurrentTeId"] = TeId;
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
        cmbMajor.SelectedIndex = -1;
        cmbLicence.SelectedIndex = -1;
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
            TSP.DataManager.InstitueTeachersManager InstitueTeachersManager2 = new TSP.DataManager.InstitueTeachersManager();
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString()));
            string TeId = HiddenFieldInsTeacher["TeId"].ToString();
            InstitueTeachersManager2.FindByInstitue(InsId);
            if (InstitueTeachersManager2.Count > 0)
            {
                InstitueTeachersManager2.CurrentFilter = "TeId=" + TeId + " and " + "InActive=0";
                if (InstitueTeachersManager2.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد.";
                    return;
                }
            }
            DataRow InsTeacherRow = InstitueTeachersManager.NewRow();
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
                HiddenFieldInsTeacher["CurrentTeId"] = HiddenFieldInsTeacher["TeId"].ToString();
                HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldInsTeacher["InsTeacherId"] = Utility.EncryptQS(InstitueTeachersManager[0]["InsTeacherId"].ToString());
                RoundPanelInsTeacher.HeaderText = "ویرایش";
                btnNew.Enabled = true;
                btnNew2.Enabled = true;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave2.Enabled = true;
                btnSave.Enabled = true;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;
                this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
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
                if (HiddenFieldInsTeacher["CurrentTeId"].ToString() != HiddenFieldInsTeacher["TeId"].ToString())
                {
                    TSP.DataManager.InstitueTeachersManager InstitueTeachersManager2 = new TSP.DataManager.InstitueTeachersManager();
                    int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString()));
                    string TeId = HiddenFieldInsTeacher["TeId"].ToString();
                    InstitueTeachersManager2.FindByInstitue(InsId);
                    if (InstitueTeachersManager2.Count > 0)
                    {
                        InstitueTeachersManager2.CurrentFilter = "TeId=" + TeId + " and " + "InActive=0";
                        if (InstitueTeachersManager2.Count > 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد.";
                            return;
                        }
                    }
                }
                InstitueTeachersManager[0].BeginEdit();
                InstitueTeachersManager[0]["StartDate"] = txtStartDate.Text;
                InstitueTeachersManager[0]["TeId"] = int.Parse(HiddenFieldInsTeacher["TeId"].ToString());
                InstitueTeachersManager[0]["EndDate"] = txtEndDate.Text;
                InstitueTeachersManager[0]["Description"] = txtDescription.Text;
                InstitueTeachersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
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

    private void ChangeDisableButtonIcon(Boolean InActive)
    {
        if (InActive)
        {
            btnDisActive.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive.ToolTip = "فعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive2.ToolTip = "فعال کردن";
        }
        else
        {
            btnDisActive.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive.ToolTip = "غیرفعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive2.ToolTip = "غیرفعال کردن";
        }
        this.ViewState["btnDisActiveImg"] = this.btnDisActive.Image.Url;
    }

    protected void Active(int InsTeacherId)
    {

        TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
        InstitueTeachersManager.FindByCode(InsTeacherId);
        if (InstitueTeachersManager.Count == 1)
        {
            try
            {
                InstitueTeachersManager[0].BeginEdit();
                InstitueTeachersManager[0]["InActive"] = 0;
                InstitueTeachersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                InstitueTeachersManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueTeachersManager[0].EndEdit();
                int cn = InstitueTeachersManager.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(false);
                    HiddenFieldInsTeacher["InsTeacherId"] = Utility.EncryptQS(InstitueTeachersManager[0]["InsTeacherId"].ToString());
                    HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
                    RoundPanelInsTeacher.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
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

    protected void InActive(int InsTeacherId)
    {

        TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
        InstitueTeachersManager.FindByCode(InsTeacherId);
        if (InstitueTeachersManager.Count == 1)
        {
            try
            {
                InstitueTeachersManager[0].BeginEdit();
                InstitueTeachersManager[0]["InActive"] = 1;
                InstitueTeachersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                InstitueTeachersManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueTeachersManager[0].EndEdit();
                int cn = InstitueTeachersManager.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(true);
                    HiddenFieldInsTeacher["InsTeacherId"] = Utility.EncryptQS(InstitueTeachersManager[0]["InsTeacherId"].ToString());
                    HiddenFieldInsTeacher["PageMode"] = Utility.EncryptQS("Edit");
                    RoundPanelInsTeacher.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
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
    #endregion
}
