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
using DevExpress.Web;

public partial class Employee_Employee_AddSettlementAgent : System.Web.UI.Page
{
    string _SignImage
    {
        get
        {
            try { return HiddenFieldSettlementAgent["SignImage"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldSettlementAgent["SignImage"] = value;
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["StlAgntId"]))
        {
            Response.Redirect("SettlementAgent.aspx");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            HiddenFieldSettlementAgent["SignImage"] = HiddenFieldSettlementAgent["MeId"] = "";
            TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            SetKey();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        if (!Utility.IsDBNullOrNullValue(HiddenFieldSettlementAgent["SignImage"]))
        {

            HpSign.ImageUrl = HiddenFieldSettlementAgent["SignImage"].ToString();

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldSettlementAgent["PageMode"].ToString());

        if (cmbMemberType.SelectedIndex == 0 && PageMode == "New")
        {

            if (string.IsNullOrEmpty(txtMeNo.Text.Trim()))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را وارد نمایید";
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()) || string.IsNullOrEmpty(txtIdNo.Text.Trim()))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات عضویت شخص انتخاب شده باید تکمیل باشد جهت تکمیل اطلاعات از قسمت عضویت اقدام فرمایید";
                return;
            }
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertSettlementAgent();

            }
            else if (PageMode == "Edit")
            {
                string StlAgentId = Utility.DecryptQS(HiddenFieldSettlementAgent["StlAgentId"].ToString());

                if (string.IsNullOrEmpty(StlAgentId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditSettlementAgent(int.Parse(StlAgentId));
                }

            }

        }

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {


        HiddenFieldSettlementAgent["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldSettlementAgent["StlAgentId"] = "";
        TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        HiddenFieldSettlementAgent["PageMode"] = Utility.EncryptQS("Edit");
        TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        SetEditModeKeys();


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SettlementAgent.aspx");
    }

    protected void txtMeID_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        if (!(string.IsNullOrEmpty(txtMeNo.Text)))
        {
            FillFormByMeNo(int.Parse(txtMeNo.Text));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "کد عضویت را وارد نمایید";

        }
    }

    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods

    private void SetKey()
    {
        try
        {
            HiddenFieldSettlementAgent["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldSettlementAgent["StlAgentId"] = Server.HtmlDecode(Request.QueryString["StlAgntId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldSettlementAgent["PageMode"].ToString());
        string StlAgentId = Utility.DecryptQS(HiddenFieldSettlementAgent["StlAgentId"].ToString());

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
        string StlAgentId = Utility.DecryptQS(HiddenFieldSettlementAgent["StlAgentId"].ToString());
        if (string.IsNullOrEmpty(StlAgentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
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

        FillForm(int.Parse(StlAgentId));

        Disable();
        RoundPanelSettelmentAgent.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        Disable();
        txtMeNo.ClientEnabled = true;
        txtDescription.ClientEnabled = true;
        cmbMemberType.Enabled = true;

        flpSign.ClientVisible = true;
        ClearForm();

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //btnSave.Enabled = true;
        //btnSave2.Enabled = true;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        RoundPanelSettelmentAgent.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string StlAgentId = Utility.DecryptQS(HiddenFieldSettlementAgent["StlAgentId"].ToString());
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        if (string.IsNullOrEmpty(StlAgentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillForm(int.Parse(StlAgentId));
        cmbMemberType.Enabled = false;
        txtDescription.ClientEnabled = flpSign.ClientVisible = true;

        RoundPanelSettelmentAgent.HeaderText = "ویرایش";
    }

    protected void FillForm(int StlAgentId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        SettlementAgentManager.FindByCode(StlAgentId);
        if (SettlementAgentManager.Count == 1)
        {
            txtName.Text = SettlementAgentManager[0]["Name"].ToString();
            txtAddress.Text = SettlementAgentManager[0]["Address"].ToString();
            txtBrithDate.Text = SettlementAgentManager[0]["BirthDate"].ToString();
            txtDescription.Text = SettlementAgentManager[0]["Description"].ToString();
            txtEmail.Text = SettlementAgentManager[0]["Email"].ToString();
            txtFamily.Text = SettlementAgentManager[0]["Family"].ToString();
            txtFatherName.Text = SettlementAgentManager[0]["Father"].ToString();
            txtMobile.Text = SettlementAgentManager[0]["MobileNo"].ToString();
            txtSSN.Text = SettlementAgentManager[0]["SSN"].ToString();
            txtIdNo.Text = SettlementAgentManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(SettlementAgentManager[0]["SignImageURL"]))
            {
                _SignImage = HpSign.ImageUrl = SettlementAgentManager[0]["SignImageURL"].ToString();

            }

            if (!string.IsNullOrEmpty(SettlementAgentManager[0]["MeId"].ToString()))
            {
                txtMeNo.Text = SettlementAgentManager[0]["MeId"].ToString();
                cmbMemberType.SelectedIndex = 0;
                txtMeNo.ClientVisible =lblMeId.ClientVisible= true;
                Disable();
            }
            else
            {
                cmbMemberType.SelectedIndex = 1;
                Enable();
                txtMeNo.ClientVisible = lblMeId.ClientVisible = false;
            }
            int TableType = (int)TSP.DataManager.TableCodes.SettlementAgent;
            TelManager.FindByTablePrimaryKey(StlAgentId, TableType);
            if (TelManager.Count > 0)
            {
                int NumberKind = int.Parse(TelManager[0]["Kind"].ToString());
                if (NumberKind == 0)
                    txtTell.Text = TelManager[0]["Number"].ToString();
            }
        }
    }

    protected void ClearForm()
    {
        txtTell.Text = "";
        txtSSN.Text = "";
        txtName.Text = "";
        txtMobile.Text = "";
        txtMeNo.Text = "";
        txtIdNo.Text = "";
        txtFatherName.Text = "";
        txtFamily.Text = "";
        txtEmail.Text = "";
        txtDescription.Text = "";
        txtBrithDate.Text = "";
        txtAddress.Text = "";
        HpSign.ImageUrl = "";

        cmbMemberType.SelectedIndex = 0;
        txtMeNo.ClientVisible = lblMeId.ClientVisible = true;

    }

    protected void Disable()
    {
        txtAddress.ClientEnabled = false;
        txtBrithDate.Enabled = false;
        txtDescription.ClientEnabled = false;
        txtEmail.ClientEnabled = false;
        txtFamily.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtMeNo.ClientEnabled = false;
        txtMobile.ClientEnabled = false;
        txtName.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtTell.ClientEnabled = false;
        flpSign.ClientVisible = false;

    }

    protected void Enable()
    {
        txtTell.ClientEnabled = true;
        txtSSN.ClientEnabled = true;
        txtName.ClientEnabled = true;
        txtMobile.ClientEnabled = true;
        txtMeNo.ClientEnabled = true;
        txtIdNo.ClientEnabled = true;
        txtFatherName.ClientEnabled = true;
        txtFamily.ClientEnabled = true;
        txtEmail.ClientEnabled = true;
        txtDescription.ClientEnabled = true;
        txtBrithDate.Enabled = true;
        txtAddress.ClientEnabled = true;
    }

    private void InsertSettlementAgent()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();

        TransactionManager.Add(SettlementAgentManager);
        TransactionManager.Add(TelManager);
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(NezamChartManager);
        TransactionManager.Add(NezamMemberChartManager);

        try
        {
            TransactionManager.BeginSave();
            DataRow SetlRow = SettlementAgentManager.NewRow();
            SetlRow["Name"] = txtName.Text;
            SetlRow["Family"] = txtFamily.Text;
            SetlRow["Father"] = txtFatherName.Text;
            SetlRow["IdNo"] = txtIdNo.Text;
            SetlRow["BirthDate"] = txtBrithDate.Text;
            if (!Utility.IsDBNullOrNullValue(txtSSN.Text))
                SetlRow["SSN"] = txtSSN.Text;
            if (!Utility.IsDBNullOrNullValue(txtMobile.Text))
                SetlRow["MobileNo"] = txtMobile.Text;
            if (!Utility.IsDBNullOrNullValue(txtEmail.Text))
                SetlRow["Email"] = txtEmail.Text;
            if (!Utility.IsDBNullOrNullValue(txtAddress.Text))
                SetlRow["Address"] = txtAddress.Text;
            if (cmbMemberType.SelectedIndex == 0 && !string.IsNullOrEmpty(txtMeNo.Text.Trim()))
                SetlRow["MeId"] = int.Parse(txtMeNo.Text.Trim());
            if (!Utility.IsDBNullOrNullValue(txtDescription.Text))
                SetlRow["Description"] = txtDescription.Text;
            SetlRow["InActive"] = 0;
            SetlRow["UserId"] = Utility.GetCurrentUser_UserId();
            SetlRow["ModifiedDate"] = DateTime.Now;
            if (!Utility.IsDBNullOrNullValue(_SignImage))
                SetlRow["SignImageURL"] = _SignImage;

            SettlementAgentManager.AddRow(SetlRow);
            if (SettlementAgentManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            InsertTell(int.Parse(SettlementAgentManager[0]["StlAgentId"].ToString()), TelManager, TransactionManager);
            int StlAgentId = int.Parse(SettlementAgentManager[0]["StlAgentId"].ToString());
            //if (SettlementAgentManager[0]["MeId"] != null)
            //{
          //  int IdNo = int.Parse(SettlementAgentManager[0]["IdNo"].ToString());
            string Email = SettlementAgentManager[0]["Email"].ToString();
            if (InsertNezamChart(StlAgentId, NezamChartManager, NezamMemberChartManager))
                InsertLogin(StlAgentId, Email, TransactionManager, LoginManager);
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }

            //}
            //else
            //{
           // TransactionManager.EndSave();

            HiddenFieldSettlementAgent["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldSettlementAgent["StlAgentId"] = Utility.EncryptQS(StlAgentId.ToString());
        
            //TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            //BtnNew.Enabled = per.CanNew;
            //btnNew2.Enabled = per.CanNew;
            //btnEdit.Enabled = false;
            //btnEdit2.Enabled = false;
            //btnSave.Enabled = per.CanEdit;
            //btnSave2.Enabled = per.CanEdit;
            //this.ViewState["BtnEdit"] = btnEdit.Enabled;
            //this.ViewState["BtnSave"] = btnSave.Enabled;
            //this.ViewState["BtnNew"] = BtnNew.Enabled;
            //RoundPanelSettelmentAgent.HeaderText = "ویرایش";
            //this.DivReport.Visible = true;
            //this.LabelWarning.Text = "ذخیره انجام شد.";
            //}
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            TransactionManager.CancelSave();
            SetError(err);
            Utility.SaveWebsiteError(err);
        }  
        SetEditModeKeys();
    }

    private void InsertTell(int TableId, TSP.DataManager.TelManager TelManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        DataRow TelRow = TelManager.NewRow();
        TelRow["TtType"] = (int)TSP.DataManager.TableCodes.SettlementAgent;
        TelRow["TtId"] = TableId;
        TelRow["Kind"] = 0;
        TelRow["Number"] = txtTell.Text;
        TelRow["InActive"] = 0;
        TelRow["UserId"] = Utility.GetCurrentUser_UserId();
        TelRow["ModifiedDate"] = DateTime.Now;
        TelManager.AddRow(TelRow);
        int cn = TelManager.Save();
        if (cn < 0)
        {
            TransactionManager.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            return;
        }
    }

    private void EditSettlementAgent(int StlAgentId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        TransactionManager.Add(SettlementAgentManager);
        TransactionManager.Add(TelManager);

        try
        {
            TransactionManager.BeginSave();

            SettlementAgentManager.FindByCode(StlAgentId);
            if (SettlementAgentManager.Count == 1)
            {
                SettlementAgentManager[0].BeginEdit();

                SettlementAgentManager[0]["Name"] = txtName.Text;
                SettlementAgentManager[0]["Family"] = txtFamily.Text;
                SettlementAgentManager[0]["Father"] = txtFatherName.Text;
                SettlementAgentManager[0]["IdNo"] = txtIdNo.Text;
                SettlementAgentManager[0]["BirthDate"] = txtBrithDate.Text;
                SettlementAgentManager[0]["SSN"] = txtSSN.Text;
                SettlementAgentManager[0]["MobileNo"] = txtMobile.Text;
                SettlementAgentManager[0]["Email"] = txtEmail.Text;
                SettlementAgentManager[0]["Address"] = txtAddress.Text;
                if (cmbMemberType.SelectedIndex == 0)
                    SettlementAgentManager[0]["MeId"] = txtMeNo.Text;
                SettlementAgentManager[0]["Description"] = txtDescription.Text;
                SettlementAgentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                SettlementAgentManager[0]["ModifiedDate"] = DateTime.Now;
                if (!Utility.IsDBNullOrNullValue(_SignImage))
                    SettlementAgentManager[0]["SignImageURL"] = _SignImage;
                SettlementAgentManager[0].EndEdit();
                int cn = SettlementAgentManager.Save();
                if (cn > 0)
                {
                    EditTel(StlAgentId, TelManager, TransactionManager);
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
            Utility.SaveWebsiteError(err);
        }
    }

    private void EditTel(int StlAgentId, TSP.DataManager.TelManager TelManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        TelManager.FindByTablePrimaryKey(StlAgentId, (int)TSP.DataManager.TableCodes.SettlementAgent);
        if (TelManager.Count == 1)
        {
            TelManager[0]["TtType"] = (int)TSP.DataManager.TableCodes.SettlementAgent;
            TelManager[0]["TtId"] = StlAgentId;
            TelManager[0]["Kind"] = 0;
            TelManager[0]["Number"] = txtTell.Text;
            TelManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TelManager[0]["ModifiedDate"] = DateTime.Now;
            int cn = TelManager.Save();
            if (TelManager.Count > 0)
            {
                TransactionManager.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(txtTell.Text))
            {
                InsertTell(StlAgentId, TelManager, TransactionManager);
            }
        }
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

    private void FillFormByMeNo(int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        try
        {
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                txtAddress.Text = MemberManager[0]["HomeAdr"].ToString();
                txtBrithDate.Text = MemberManager[0]["BirhtDate"].ToString();
                txtDescription.Text = MemberManager[0]["Description"].ToString();
                txtEmail.Text = MemberManager[0]["Email"].ToString();
                txtFamily.Text = MemberManager[0]["LastName"].ToString();
                txtFatherName.Text = MemberManager[0]["FatherName"].ToString();
                txtIdNo.Text = MemberManager[0]["IdNo"].ToString();
                txtMeNo.Text = MemberManager[0]["MeId"].ToString();
                txtMobile.Text = MemberManager[0]["MobileNo"].ToString();
                txtName.Text = MemberManager[0]["FirstName"].ToString();
                txtSSN.Text = MemberManager[0]["SSN"].ToString();
                txtTell.Text = MemberManager[0]["HomeTel"].ToString();
            }
        }
        catch (Exception err)
        {
        }
    }

    private void InsertLogin(int StlAgentId, string Email, TSP.DataManager.TransactionManager TransactionManager, TSP.DataManager.LoginManager LogManager)
    {
        String Password = Utility.GeneratePassword();
        DataRow logRow = LogManager.NewRow();
        logRow.BeginEdit();
        logRow["UserName"] = "Stl" + StlAgentId.ToString();
        logRow["Password"] = Utility.EncryptPassword(Password);
        logRow["UltId"] = 7;
        logRow["MeId"] = StlAgentId;
        logRow["Email"] = Email;
        logRow["IsValid"] = 1;
        logRow["ModifiedDate"] = DateTime.Now;
        logRow.EndEdit();
        LogManager.AddRow(logRow);
        if (LogManager.Save() > 0)
        {
            TransactionManager.EndSave();

            HiddenFieldSettlementAgent["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldSettlementAgent["StlAgentId"] = Utility.EncryptQS(StlAgentId.ToString());

            TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            RoundPanelSettelmentAgent.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "  ذخیره با نام کاربری " + "Stl" + StlAgentId.ToString() + "و رمز عبور " + Password + " انجام شد";
        }
        else
        {
            TransactionManager.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private Boolean InsertNezamChart(int StlAgentId, TSP.DataManager.NezamChartManager NezamChartManager, TSP.DataManager.NezamMemberChartManager NezamMemberChartManager)
    {
        Boolean IsSave = false;

        DataRow drNc = NezamChartManager.NewRow();
        drNc["NcName"] = "مسکن";
        drNc["ParentId"] = Utility.GetSettelmentParentNcId();
        drNc["IsExternal"] = 1;
        drNc["IsDepartment"] = 0;
        drNc["UserId"] = Utility.GetCurrentUser_UserId();
        drNc["ModifiedDate"] = DateTime.Now;

        NezamChartManager.AddRow(drNc);

        if (NezamChartManager.Save() > 0)
        {
            NezamChartManager.DataTable.AcceptChanges();
            int NcId = Convert.ToInt32(NezamChartManager[NezamChartManager.Count - 1]["NcId"]);
            DataRow drNmc = NezamMemberChartManager.NewRow();
            drNmc["NcId"] = NcId;
            drNmc["EmpId"] = StlAgentId;
            drNmc["UltId"] = (int)TSP.DataManager.UserType.Settlement;
            drNmc["InActive"] = 0;
            drNmc["UserId"] = Utility.GetCurrentUser_UserId();
            drNmc["ModifiedDate"] = DateTime.Now;

            NezamMemberChartManager.AddRow(drNmc);
            if (NezamMemberChartManager.Save() > 0)
                IsSave = true;

        }

        return IsSave;
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);

                ret = "IdNo" + txtIdNo.Text + "IdNo" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/SettlmentSign/") + ret) == true);
            string tempFileName = "~/image/SettlmentSign/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _SignImage = tempFileName;
        }
        return ret;
    }
    #endregion
}
