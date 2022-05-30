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

public partial class Employee_Amoozesh_InsertTeacherResearches : System.Web.UI.Page
{
    string PageMode;
    string TResearchId;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeacherId"]))
        {
            Response.Redirect("Teachers.aspx");
            return;
        }


        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;



            //  MenuTeacherInfo.Enabled = true;

            SetKeys();

            string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
            CheckCertificatePermission(int.Parse(TeId));

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());
        string TResearchId = Utility.DecryptQS(HiddenFieldResearch["TResearchId"].ToString());

        if (string.IsNullOrEmpty(TResearchId))
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
                EnableControls();
                btnSave.Enabled = true;
                btnSave2.Enabled = true;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                HiddenFieldResearch["PageMode"] = Utility.EncryptQS("Edit");
                RoundPanelResearch.HeaderText = "ویرایش";
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string TResearchId = Utility.DecryptQS(HiddenFieldResearch["TResearchId"].ToString());
        DeleteTeacherResearch(int.Parse(TResearchId));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherResearchAct.aspx?PageMode=" + HiddenFieldResearch["PrePageMode"].ToString() + "&TeacherId=" + HiddenFieldResearch["TeacherId"].ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());

        string TResearchId = Utility.DecryptQS(HiddenFieldResearch["TResearchId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTeacherResearch();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TResearchId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditTeacherResearch(int.Parse(TResearchId));
                }

            }
            else
            {
                if (PageMode == "Judge")
                {
                    if (string.IsNullOrEmpty(TResearchId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                    {
                        int TableType = -1;
                        if (IsTeacherMember())
                            TableType = (int)(TSP.DataManager.TableCodes.MemberResearchActivity);
                        else
                            TableType = (int)(TSP.DataManager.TableCodes.TeacherResearchActivity);
                        string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
                        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
                        DataTable dtTeacherJudgment = TeacherJudgmentManager.FindByResearchActivity(int.Parse(TeId), int.Parse(TResearchId), TableType);
                        if (dtTeacherJudgment.Rows.Count == 1)
                        {
                            int JudgeId = int.Parse(dtTeacherJudgment.Rows[0]["JudgeId"].ToString());
                            EditJudgment(JudgeId);
                        }
                        else
                            InsertJudgment();
                    }
                }
            }

        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnableControls();
        HiddenFieldResearch["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldResearch["TResearchId"] = "";
        RoundPanelResearch.HeaderText = "جدید";
        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        this.ViewState["BtnNew"] = btnNew.Enabled;

    }
    #endregion

    #region Methods
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

    private void ClearForm()
    {
        txtDescription.Text = "";
        txtJudgeDescription.Text = "";
        txtResearchDate.Text = "";
        txtResearchName.Text = "";
        cmbResearchType.SelectedIndex = 0;
    }

    private void SetKeys()
    {
        HiddenFieldResearch["TeacherId"] = Request.QueryString["TeacherId"].ToString();
        HiddenFieldResearch["PrePageMode"] = Request.QueryString["PrePageMode"];
        HiddenFieldResearch["PageMode"] = Request.QueryString["PageMode"];
        HiddenFieldResearch["TResearchId"] = Request.QueryString["TResearchId"];
        string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
        PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());
        TResearchId = Utility.DecryptQS(HiddenFieldResearch["TResearchId"].ToString());
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherManager.FindByCode(int.Parse(TeId));

        if (TeacherManager.Count > 0)
        {
            lblTeacherName.Text = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
        }
        else
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
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
            case "Judge":
                SetJudgeModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

        //Set Textboxe's & comboboxe's Enabled
        txtResearchName.Enabled = false;
        txtResearchDate.Enabled = false;
        lblTeacherName.Enabled = false;
        txtJudgeDescription.Enabled = false;
        txtDescription.Enabled = false;
        cmbResearchType.Enabled = false;


        FillForm(int.Parse(TResearchId));

        RoundPanelResearch.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        ClearForm();

        RoundPanelResearch.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (string.IsNullOrEmpty(TResearchId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();

        FillForm(int.Parse(TResearchId));

        RoundPanelResearch.HeaderText = "ویرایش";
    }

    private void SetJudgeModeKeys()
    {

        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
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

        //Set Textboxe's & comboboxe's Enabled
        txtDescription.Enabled = false;
        txtResearchDate.Enabled = false;
        // txtJudgeDescription.Enabled = false;
        txtResearchName.Enabled = false;
        cmbResearchType.Enabled = false;

        FillForm(int.Parse(TResearchId));

        RoundPanelResearch.HeaderText = "مشاهده";
        RoundPanelJudge.Visible = true;
    }

    private void FillForm(int TResearchId)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
        TeacherResearchActivityManager.FindByCode(TResearchId);

        if (TeacherResearchActivityManager.Count > 0)
        {
            txtDescription.Text = TeacherResearchActivityManager[0]["Description"].ToString();
            txtResearchDate.Text = TeacherResearchActivityManager[0]["ResearchDate"].ToString();
            txtResearchName.Text = TeacherResearchActivityManager[0]["ResearchName"].ToString();
            cmbResearchType.DataBind();
            cmbResearchType.SelectedIndex = cmbResearchType.Items.IndexOfValue(TeacherResearchActivityManager[0]["RaId"].ToString());
            int TeId = int.Parse(TeacherResearchActivityManager[0]["TeId"].ToString());
            int TableType = -1;
            if (IsTeacherMember())
                TableType = (int)(TSP.DataManager.TableCodes.MemberResearchActivity);
            else
                TableType = (int)(TSP.DataManager.TableCodes.TeacherResearchActivity);
            DataTable dtJudgment = TeacherJudgmentManager.FindByResearchActivity(TeId, TResearchId, TableType);
            if (dtJudgment.Rows.Count > 0)
            {
                int RaId = int.Parse(cmbResearchType.SelectedItem.Value.ToString());
                float SystemGrade = 0;
                ResearchActivityManager.FindByCode(RaId);
                if (ResearchActivityManager.Count > 0)
                {
                    SystemGrade = float.Parse(ResearchActivityManager[0]["Grade"].ToString());
                }
                int JudgeGrade = int.Parse(dtJudgment.Rows[0]["JudgeGrade"].ToString());
                if (SystemGrade == JudgeGrade)
                    rdbtnIsConfirm.SelectedIndex = 0;
                else
                    rdbtnIsConfirm.SelectedIndex = 1;
                txtJudgeDescription.Text = dtJudgment.Rows[0]["JudgeViewPoint"].ToString();
            }
        }
    }

    private void EnableControls()
    {
        cmbResearchType.Enabled = true;
        txtDescription.Enabled = true;

        txtJudgeDescription.Enabled = true;
        txtResearchDate.Enabled = true;
        txtResearchName.Enabled = true;
    }

    private void InsertTeacherResearch()
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        try
        {
            DataRow ResearchRow = TeacherResearchActivityManager.NewRow();

            ResearchRow["RaId"] = int.Parse(cmbResearchType.SelectedItem.Value.ToString());
            string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());

            DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(int.Parse(TeId));
            if (dtTeCert.Rows.Count > 0)
            {
                int TcId = int.Parse(dtTeCert.Rows[0]["TcId"].ToString());
                ResearchRow["TcId"] = TcId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پرونده ای برای این استاد تشکیل نشده است.";
                return;
            }
            ResearchRow["TeId"] = int.Parse(TeId);
            ResearchRow["ResearchName"] = txtResearchName.Text;
            ResearchRow["ResearchDate"] = txtResearchDate.Text;
            ResearchRow["Description"] = txtDescription.Text;
            ResearchRow["UserId"] = Utility.GetCurrentUser_UserId();
            ResearchRow["ModifiedDate"] = DateTime.Now;

            TeacherResearchActivityManager.AddRow(ResearchRow);

            int cn = TeacherResearchActivityManager.Save();
            if (cn > 0)
            {
                HiddenFieldResearch["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldResearch["TResearchId"] = Utility.EncryptQS(TeacherResearchActivityManager[0]["TResearchId"].ToString());
                RoundPanelResearch.HeaderText = "ویرایش";
                TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit || per.CanNew;
                btnSave2.Enabled = per.CanEdit || per.CanNew;

                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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

    private void EditTeacherResearch(int TResearchId)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        try
        {
            TeacherResearchActivityManager.Fill();
            DataRow TResearchRow = TeacherResearchActivityManager.DataTable.Rows.Find(TResearchId);
            if (TResearchRow != null)
            {
                TResearchRow.BeginEdit();

                TResearchRow["RaId"] = int.Parse(cmbResearchType.SelectedItem.Value.ToString());
                string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
                TResearchRow["TeId"] = int.Parse(TeId);
                TResearchRow["ResearchName"] = txtResearchName.Text;
                TResearchRow["ResearchDate"] = txtResearchDate.Text;
                TResearchRow["Description"] = txtDescription.Text;
                TResearchRow["UserId"] = Utility.GetCurrentUser_UserId();
                TResearchRow["ModifiedDate"] = DateTime.Now;

                TResearchRow.EndEdit();

                int cn = TeacherResearchActivityManager.Save();

                if (cn > 0)
                {
                    TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                   
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = per.CanNew || per.CanEdit;
                    btnSave2.Enabled = per.CanNew || per.CanEdit;

                    this.ViewState["BtnSave"] = btnSave.Enabled;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnNew"] = btnNew.Enabled;

                    this.DivReport.Visible = true;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

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

    private void DeleteTeacherResearch(int TResearchId)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();
        try
        {
            TeacherResearchActivityManager.FindByCode(TResearchId);
            if (TeacherResearchActivityManager.Count > 0)
            {
                TeacherResearchActivityManager[0].Delete();

                int cn = TeacherResearchActivityManager.Save();

                if (cn > 0)
                {
                    ClearForm();
                    HiddenFieldResearch["PageMode"] = Utility.EncryptQS("New");
                    HiddenFieldResearch["TResearchId"] = "";
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    RoundPanelResearch.HeaderText = "جدید";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void InsertJudgment()
    {
        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        try
        {
            int RaId = int.Parse(cmbResearchType.SelectedItem.Value.ToString());
            ResearchActivityManager.FindByCode(RaId);
            if (ResearchActivityManager.Count > 0)
            {
                float SystemGrade = float.Parse(ResearchActivityManager[0]["Grade"].ToString());

                LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                string TResearchId = Utility.DecryptQS(HiddenFieldResearch["TResearchId"].ToString());
                DataRow JudgmentRow = TeacherJudgmentManager.NewRow();
                string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
                if (IsTeacherMember())
                    JudgmentRow["TableType"] = (int)(TSP.DataManager.TableCodes.MemberResearchActivity);
                else
                    JudgmentRow["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherResearchActivity);
                JudgmentRow["TableId"] = int.Parse(TResearchId);
                JudgmentRow["TeId"] = int.Parse(TeId);
                if (rdbtnIsConfirm.SelectedIndex == 0)
                    JudgmentRow["JudgeGrade"] = SystemGrade;
                else
                    JudgmentRow["JudgeGrade"] = 0;
                JudgmentRow["JudgeViewPoint"] = txtJudgeDescription.Text;
                JudgmentRow["EmpId"] = EmpId;
                //JudgmentRow["MeetingId"] = "";
                JudgmentRow["UserId"] = Utility.GetCurrentUser_UserId();
                JudgmentRow["ModifiedDate"] = DateTime.Now;

                TeacherJudgmentManager.AddRow(JudgmentRow);
                int cn = TeacherJudgmentManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void EditJudgment(int JudgeId)
    {
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TeacherJudgmentManager.FindByCode(JudgeId);

        if (TeacherJudgmentManager.Count > 0)
        {
            int RaId = int.Parse(cmbResearchType.SelectedItem.Value.ToString());
            ResearchActivityManager.FindByCode(RaId);
            if (ResearchActivityManager.Count > 0)
            {
                float SystemGrade = float.Parse(ResearchActivityManager[0]["Grade"].ToString());

                LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());

                TeacherJudgmentManager[0].BeginEdit();

                string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
                if (IsTeacherMember())
                    TeacherJudgmentManager[0]["TableType"] = (int)(TSP.DataManager.TableCodes.MemberResearchActivity);
                else
                    TeacherJudgmentManager[0]["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherResearchActivity);
                TeacherJudgmentManager[0]["TeId"] = int.Parse(TeId);
                if (rdbtnIsConfirm.SelectedIndex == 0)
                    TeacherJudgmentManager[0]["JudgeGrade"] = SystemGrade;
                else
                    TeacherJudgmentManager[0]["JudgeGrade"] = 0;
                TeacherJudgmentManager[0]["EmpId"] = EmpId;
                TeacherJudgmentManager[0]["JudgeViewPoint"] = txtJudgeDescription.Text;
                //JudgmentRow["MeetingId"] = "";
                TeacherJudgmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherJudgmentManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherJudgmentManager[0].EndEdit();

                int cn = TeacherJudgmentManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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

    private Boolean IsTeacherMember()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeId));

        if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            return true;
        else
            return false;
    }

    private int FindTeacherCertificate(int TeId)
    {
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeId);
        int TeCertType = -1;
        if (dtTeCert.Rows.Count > 0)
        {
            TeCertType = int.Parse(dtTeCert.Rows[0]["Type"].ToString());
        }
        return TeCertType;


    }

    private void CheckCertificatePermission(int TeId)
    {
        int CertType = FindTeacherCertificate(TeId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());
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

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string TeId = Utility.DecryptQS(HiddenFieldResearch["TeacherId"].ToString());
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        int ComissionGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.ComissionGradingTeacher;
        int CommitteeGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeGradingTeacher;

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerComissionGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ComissionGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerCommitteeGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(CommitteeGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);

        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerComissionGrading.BtnEdit || WFPerCommitteeGrading.BtnEdit;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave || WFPerComissionGrading.BtnSave || WFPerCommitteeGrading.BtnSave;
        btnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || WFPerComissionGrading.BtnNew || WFPerCommitteeGrading.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion
    #endregion

}
