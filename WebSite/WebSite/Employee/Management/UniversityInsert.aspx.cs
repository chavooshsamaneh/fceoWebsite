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

public partial class Employee_Management_UniversityInsert : System.Web.UI.Page
{
    #region Private Members
    string PageMode;
    string UniId;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");      
        if (!IsPostBack)
        {
            HiddenFieldUniversity["PageMode"] = "";
            HiddenFieldUniversity["UnId"] = "";
            HiddenFieldUniversity["NewMode"] = Utility.EncryptQS("New");
            HiddenFieldUniversity["btn1"] = "";
            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["UnId"])))
            {
                Response.Redirect("University.aspx");
                return;
            }
            SetKeys();

            cmbUniType.SelectedIndex = 0;
            rdbIsForeign.SelectedIndex = 0;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew2.Enabled = this.btnNew.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        RoundPanelUniversityInsert.HeaderText = "جدید";
        ClearForm();
        EnableControls();
        HiddenFieldUniversity["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldUniversity["UnId"] = Utility.EncryptQS("-1");

        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldUniversity["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                EnableControls();
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                InsertUniversity();
                break;
            case "Edit":
                if (string.IsNullOrEmpty(HiddenFieldUniversity["UnId"].ToString())) //string.IsNullOrEmpty(UnId.Value))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    UniId = Utility.DecryptQS(HiddenFieldUniversity["UnId"].ToString());
                }
                EditUniversity(int.Parse(UniId));
                break;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldUniversity["PageMode"].ToString());
        UniId = Utility.DecryptQS(HiddenFieldUniversity["UnId"].ToString());
        if (CheckIfInActive(Convert.ToInt32(UniId)))
        {
            return;
        }
        if (string.IsNullOrEmpty(UniId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(PageMode) && PageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                TSP.DataManager.Permission per = TSP.DataManager.UniversityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnSave2.Enabled = true;
                EnableControls();
                HiddenFieldUniversity["PageMode"] = Utility.EncryptQS("Edit");
                RoundPanelUniversityInsert.HeaderText = "ویرایش";
            }
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("University.aspx?");
    }     

    #endregion

    #region Methods
    private void InsertUniversity()
    {
        string UnCode = txtUnCode.Text;
        string UnName = txtUnName.Text;
        string Description = txtDescription.Text;

        if (string.IsNullOrEmpty(UnCode))
        {
            ShowMessage("کد دانشگاه لازم میباشد");
            return;
        }
        if (string.IsNullOrEmpty(UnName))
        {
            ShowMessage("نام دانشگاه لازم میباشد");
            return;
        }
       
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.UniversityManager UnManager = new TSP.DataManager.UniversityManager();
        
        TransactionManager.Add(UnManager);
        System.Data.DataRow dr = UnManager.NewRow();
        try
        {
            dr["UnType"] = cmbUniType.SelectedIndex;
            if (cmbCoun.Value != null)
            {
                dr["CounId"] = cmbCoun.Value;
                if (cmbCoun.Value.ToString() == Utility.GetCurrentCounId().ToString())
                    dr["IsForeign"] = 0;
                else
                    dr["IsForeign"] = 1;

            }
            else
            {
                ShowMessage("کشور را انتخاب نمایید");
                return;
            }
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["InActive"] = 0;
            dr["UnCode"] = UnCode;
            dr["UnName"] = UnName;
            dr["Description"] = Description;
            dr["ModifiedDate"] = DateTime.Now;
            if (chbConfirmed.Checked)
            {
                dr["IsConfirmed"] = 1;
                dr["LetterNo"] = "";
                dr["LetterDate"] = "";
            }

            UnManager.AddRow(dr);
            if (UnManager.Save() <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            //Save UnId in it's hiddenField
            UniId = UnManager[0]["unId"].ToString();
            HiddenFieldUniversity["UnId"] = Utility.EncryptQS(UniId);
            //Save PageMode in it's hiddenfield
            PageMode = Utility.EncryptQS("Edit");
            HiddenFieldUniversity["PageMode"] = PageMode;
            SetEditModeKeys();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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
                    this.LabelWarning.Text = "کد دانشگاه تکراری می باشد.";
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

    private void EditUniversity(int UniId)
    {      
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();        
        TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();        
        TransactionManager.Add(UniversityManager);

        UniversityManager.FindByCode(UniId);
        if (UniversityManager.Count == 1)
        {
            try
            {
                UniversityManager[0].BeginEdit();

                if (!string.IsNullOrEmpty(txtUnCode.Text))
                {
                    UniversityManager[0]["UnCode"] = txtUnCode.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد دانشگاه را وارد نمایید";
                    return;
                }

                if (!string.IsNullOrEmpty(txtUnName.Text))
                {
                    UniversityManager[0]["UnName"] = txtUnName.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "نام دانشگاه را وارد نمایید";
                    return;
                }

                if (cmbCoun.Value != null)
                {
                    UniversityManager[0]["CounId"] = cmbCoun.Value;
                    if (cmbCoun.Value.ToString() == Utility.GetCurrentCounId().ToString())
                        UniversityManager[0]["IsForeign"] = 0;
                    else
                        UniversityManager[0]["IsForeign"] = 1;

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کشور را انتخاب نمایید";
                    return;
                }
                UniversityManager[0]["Description"] = txtDescription.Text;
                UniversityManager[0]["UnType"] = cmbUniType.SelectedIndex;
                UniversityManager[0]["CounId"] = cmbCoun.Value;
                if (chbConfirmed.Checked)
                {
                    UniversityManager[0]["IsConfirmed"] = 1;
                    UniversityManager[0]["LetterNo"] = "";
                    UniversityManager[0]["LetterDate"] = "";
                }
                UniversityManager[0]["InActive"] = 0;
                UniversityManager[0].EndEdit();
                if (UniversityManager.Save() <= 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }               

                UniversityManager.UpdateMemberLicenceByUnId(UniId); //--update unname in memberlicence

                HiddenFieldUniversity["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldUniversity["unId"] = Utility.EncryptQS(UniId.ToString());
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";


            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
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
                        this.LabelWarning.Text = "کد دانشگاه تکراری می باشد.";
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
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
        }
    }

    private void SetKeys()
    {
        HiddenFieldUniversity["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
        HiddenFieldUniversity["UnId"] = Server.HtmlDecode(Request.QueryString["UnId"]).ToString();
        PageMode = Utility.DecryptQS(HiddenFieldUniversity["PageMode"].ToString());
        UniId = Utility.DecryptQS(HiddenFieldUniversity["UnId"].ToString());
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
        TSP.DataManager.Permission per = CheckTablePermission();
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
        txtUnName.ClientEnabled = false;
        txtUnCode.ClientEnabled = false;
        txtDescription.ClientEnabled = false;
        rdbIsForeign.ClientEnabled = false;
        cmbCoun.ClientEnabled = false;
        cmbUniType.ClientEnabled = false;

        FillForm(int.Parse(UniId));

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        RoundPanelUniversityInsert.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = CheckTablePermission();
        btnEdit.Enabled =  btnEdit2.Enabled =false;
        btnSave.Enabled = btnSave2.Enabled = per.CanEdit;
        ClearForm();
        EnableControls();
        HiddenFieldUniversity["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldUniversity["UnId"] = Utility.EncryptQS("-1");
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        RoundPanelUniversityInsert.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = CheckTablePermission();
        btnEdit.Enabled =  btnEdit2.Enabled =false;
        btnSave.Enabled = btnSave2.Enabled =per.CanEdit;

        if (string.IsNullOrEmpty(UniId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        EnableControls();
        FillForm(int.Parse(UniId));
        RoundPanelUniversityInsert.HeaderText = "ویرایش";
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void ClearForm()
    {
        txtDescription.Text = "";
        txtUnCode.Text = "";
        txtUnName.Text = "";
        cmbCoun.SelectedIndex = -1;
        cmbCoun.DataBind();
        cmbCoun.SelectedIndex = cmbCoun.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;
        cmbUniType.SelectedIndex = 0;
    }

    private void EnableControls()
    {
        txtUnName.ClientEnabled = true;
        txtUnCode.ClientEnabled = true;
        txtDescription.ClientEnabled = true;
        rdbIsForeign.ClientEnabled = true;
        cmbCoun.ClientEnabled = true;
        cmbUniType.ClientEnabled = true;
    }

    private void FillForm(int UnId)
    {
        string PageMode = Utility.EncryptQS(HiddenFieldUniversity["PageMode"].ToString());

        TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();
        UniversityManager.FindByCode(UnId);
        if (UniversityManager.Count == 1)
        {
            txtDescription.Text = UniversityManager[0]["Description"].ToString();
            txtUnCode.Text = UniversityManager[0]["UnCode"].ToString();
            txtUnName.Text = UniversityManager[0]["UnName"].ToString();
            cmbCoun.DataBind();
            cmbCoun.SelectedIndex = cmbCoun.Items.IndexOfValue(UniversityManager[0]["CounId"].ToString());
            cmbUniType.SelectedIndex = cmbUniType.Items.IndexOfValue(UniversityManager[0]["UnType"].ToString());
            rdbIsForeign.SelectedIndex = int.Parse(UniversityManager[0]["IsForeign"].ToString());
            if (Convert.ToBoolean(UniversityManager[0]["IsConfirmed"]) == true)
            {
                chbConfirmed.Checked = true;
            }
        }
    }

    private Boolean CheckIfInActive(int UnId)
    {
        TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();
        UniversityManager.FindByCode(UnId);
        if (UniversityManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToBoolean(UniversityManager[0]["InActive"]))
        {
            ShowMessage("امکان ویرایش رکورد غیر فعال وجود ندارد");
            return false;
        }
        return true;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private TSP.DataManager.Permission CheckTablePermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.UniversityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit || per.CanNew;
        btnSave2.Enabled = per.CanEdit || per.CanNew;
        btnNew.Enabled = per.CanNew = btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit = btnEdit2.Enabled = per.CanEdit;
        return per;
    }
  
    #endregion

}
