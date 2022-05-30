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

public partial class Employee_Amoozesh_AddTeachingCertificateGrade : System.Web.UI.Page
{
    string PageMode;
    string TGradeId;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["TGdrId"]))
        {
            Response.Redirect("Institue.aspx");
            return;
        }

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            //btnNew.ClientEnabled = per.CanNew;
            //btnNew2.ClientEnabled = per.CanNew;
            //btnEdit.ClientEnabled = per.CanEdit;
            //btnEdit2.ClientEnabled = per.CanEdit;
            //btnSave.ClientEnabled = per.CanNew || per.CanEdit;
            //btnSave2.ClientEnabled = per.CanNew || per.CanEdit;
            HiddenFieldTeachingGrade["New"] = Utility.EncryptQS("New");

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TGradeId = Utility.DecryptQS(HiddenFieldTeachingGrade["TGradeId"].ToString());

        if (!CheckPermissionForEdit(int.Parse(TGradeId)))
        {
            ShowMessage("امکان ویرایش رکورد غیر فعال وجود ندارد.");
            return;
        }

        EnableControls();
        HiddenFieldTeachingGrade["PageMode"] = Utility.EncryptQS("Edit");
        TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTeachingGrade["PageMode"].ToString());

        string TGradeId = Utility.DecryptQS(HiddenFieldTeachingGrade["TGradeId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTeachingGrade();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TGradeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditTeachingGrade(int.Parse(TGradeId));
                }

            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeachingCertificateGrade.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnableControls();
        HiddenFieldTeachingGrade["PageMode"] = Utility.EncryptQS("New");
        RoundPanelGrade.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        // this.ViewState["BtnSaveClient"] = btnSave.ClientEnabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //  this.ViewState["BtnEditClient"] = btnEdit.ClientEnabled;
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

    private void ClearForm()
    {
        txtDescription.Text = "";
        txtMeeting.Text = "";
        txtMinGrade.Text = "";
        txtMinGradeRelateJob.Text = "";
        txtMinGradeTeaching.Text = "";
        txtSumTeachJob.Text = "";
        txtDate.Text = "";
    }

    private void SetKeys()
    {
        HiddenFieldTeachingGrade["PageMode"] = Request.QueryString["PgMd"];
        HiddenFieldTeachingGrade["TGradeId"] = Request.QueryString["TGdrId"];
        PageMode = Utility.DecryptQS(HiddenFieldTeachingGrade["PageMode"].ToString());
        TGradeId = Utility.DecryptQS(HiddenFieldTeachingGrade["TGradeId"].ToString());
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
        TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled

        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        //btnSave.ClientEnabled = false;
        //btnSave2.ClientEnabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            //   btnNew.ClientEnabled = true;
            //   btnNew2.ClientEnabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            //    btnEdit.ClientEnabled = true;
            //    btnEdit2.ClientEnabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        txtMeeting.ClientEnabled = false;
        txtMinGrade.ClientEnabled = false;
        txtMinGradeRelateJob.ClientEnabled = false;
        txtSumTeachJob.ClientEnabled = false;
        txtDescription.ClientEnabled = false;
        txtDate.Enabled = false;
        txtMinGradeTeaching.ClientEnabled = false;
        FillForm(int.Parse(TGradeId));

        RoundPanelGrade.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //  btnEdit.ClientEnabled = false;
        //   btnEdit2.ClientEnabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        // btnSave.ClientEnabled = per.CanNew;
        // btnSave2.ClientEnabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;


        ClearForm();

        RoundPanelGrade.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //ُSet Button's Enable
        TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //btnEdit.ClientEnabled = false;
        // btnEdit2.ClientEnabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        //btnSave.ClientEnabled = per.CanEdit;
        //btnSave2.ClientEnabled = per.CanEdit;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (string.IsNullOrEmpty(TGradeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();

        FillForm(int.Parse(TGradeId));

        RoundPanelGrade.HeaderText = "ویرایش";
    }

    private void FillForm(int TGradeId)
    {
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TeachingGradeManager.FindByCode(TGradeId);

        if (TeachingGradeManager.Count > 0)
        {
            txtDescription.Text = TeachingGradeManager[0]["Description"].ToString();
             txtMeeting.Text = TeachingGradeManager[0]["MeetingId"].ToString();
            txtMinGrade.Text = TeachingGradeManager[0]["MinGrade"].ToString();
            txtMinGradeRelateJob.Text = TeachingGradeManager[0]["MinGradeRelateJob"].ToString();
            txtMinGradeTeaching.Text = TeachingGradeManager[0]["MinGradeTeaching"].ToString();
            txtSumTeachJob.Text = TeachingGradeManager[0]["SumResearchTeach"].ToString();
            //chbInActive.Checked = TeachingGradeManager[0]["TGradeDate"].ToString();
            txtDate.Text = TeachingGradeManager[0]["TGradeDate"].ToString();
        }

    }

    private void EnableControls()
    {
        txtMeeting.ClientEnabled = true;
        txtMinGrade.ClientEnabled = true;
        txtMinGradeRelateJob.ClientEnabled = true;
        txtSumTeachJob.ClientEnabled = true;
        txtDescription.ClientEnabled = true;
        txtMinGradeTeaching.ClientEnabled = true;
        txtDate.Enabled = true;
    }

    private void InsertTeachingGrade()
    {
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();

        try
        {

            DataRow TGradeRow = TeachingGradeManager.NewRow();
            TGradeRow["MinGrade"] = txtMinGrade.Text;
            TGradeRow["MinGradeTeaching"] = txtMinGradeTeaching.Text;
            TGradeRow["MinGradeRelateJob"] = txtMinGradeRelateJob.Text;
            TGradeRow["SumResearchTeach"] = txtSumTeachJob.Text;
            TGradeRow["Description"] = txtDescription.Text;
            TGradeRow["TGradeDate"] = txtDate.Text;
            TGradeRow["MeetingId"] = txtMeeting.Text;
            TGradeRow["InActive"] = 0;
            TGradeRow["UserId"] = Utility.GetCurrentUser_UserId();
            TGradeRow["ModifiedDate"] = DateTime.Now;
            TeachingGradeManager.AddRow(TGradeRow);
            int cn = TeachingGradeManager.Save();

            if (cn > 0)
            {
                HiddenFieldTeachingGrade["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldTeachingGrade["TGradeId"] = Utility.EncryptQS(TeachingGradeManager[0]["TGradeId"].ToString());

                int TGrdId = int.Parse(TeachingGradeManager[0]["TGradeId"].ToString());
                TeachingGradeManager.ClearBeforeFill = true;
                DataTable dtGrade = TeachingGradeManager.SelectByInActive(false);
                if (dtGrade.Rows.Count > 0)
                {
                    for (int i = 0; i < dtGrade.Rows.Count; i++)
                    {
                        if (int.Parse(dtGrade.Rows[i]["TGradeId"].ToString()) != TGrdId)
                        {
                            TeachingGradeManager.FindByCode(int.Parse(dtGrade.Rows[i]["TGradeId"].ToString()));
                            TeachingGradeManager[0].BeginEdit();
                            TeachingGradeManager[0]["InActive"] = true;
                            TeachingGradeManager[0].EndEdit();

                            cn = TeachingGradeManager.Save();
                            TeachingGradeManager.DataTable.AcceptChanges();
                            if (cn < 0)
                            {
                                return;
                            }
                        }
                    }
                }
                TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                RoundPanelGrade.HeaderText = "ویرایش";
                this.ViewState["BtnNew"] = btnNew.Enabled;

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
            SetError(err);
        }
    }

    private void EditTeachingGrade(int TGradeId)
    {

        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();

        try
        {
            TeachingGradeManager.FindByCode(TGradeId);
            if (TeachingGradeManager.Count > 0)
            {
                TeachingGradeManager[0].BeginEdit();

                TeachingGradeManager[0]["MinGrade"] = txtMinGrade.Text;
                TeachingGradeManager[0]["MinGradeTeaching"] = txtMinGradeTeaching.Text;
                TeachingGradeManager[0]["MinGradeRelateJob"] = txtMinGradeRelateJob.Text;
                TeachingGradeManager[0]["SumResearchTeach"] = txtSumTeachJob.Text;
                TeachingGradeManager[0]["Description"] = txtDescription.Text;
                TeachingGradeManager[0]["TGradeDate"] = txtDate.Text;
                TeachingGradeManager[0]["MeetingId"] = txtMeeting.Text;                
                TeachingGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeachingGradeManager[0]["ModifiedDate"] = DateTime.Now;

                TeachingGradeManager[0].EndEdit();

                int cn = TeachingGradeManager.Save();

                if (cn > 0)
                {
                    TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnNew.Enabled = per.CanNew;
                    btnNew2.Enabled = per.CanNew;
                    // btnNew.ClientEnabled = per.CanNew;
                    //  btnNew2.ClientEnabled = per.CanNew;

                    btnSave.Enabled = per.CanEdit || per.CanNew;
                    btnSave2.Enabled = per.CanEdit || per.CanNew;
                    //   btnSave.ClientEnabled = per.CanEdit || per.CanNew;
                    //   btnSave2.ClientEnabled = per.CanEdit || per.CanNew;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private Boolean CheckPermissionForEdit(int TGradeId)
    {
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TeachingGradeManager.FindByCode(TGradeId);
        if (TeachingGradeManager.Count != 1)
        {
            return false;
        }
        if (Convert.ToBoolean(TeachingGradeManager[0]["InActive"]))
            return false;
        else
            return true;

        return true;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}
