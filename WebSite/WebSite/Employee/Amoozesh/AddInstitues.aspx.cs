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
public partial class Employee_Amoozesh_AddInstitues : System.Web.UI.Page
{
    int _InsCId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldInstitue["InsCId"]);
        }
        set
        {
            HiddenFieldInstitue["InsCId"] = value;
        }
    }
    int _InsId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldInstitue["InsId"]);
        }
        set
        {
            HiddenFieldInstitue["InsId"] = value;
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("Institue.aspx");
                return;
            }

            CheckUserPermission();
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
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        MenuInstitue.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        // InstitueId.Value = Utility.EncryptQS("");
        _InsId = -1;
        PgMode.Value = Utility.EncryptQS("New");
        RoundPanelMain.HeaderText = "جدید";
        ClearForm();
        Enable();
        lblWorkFlowState.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

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

                if (Utility.IsDBNullOrNullValue(_InsCId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                Edit(_InsCId);
            }
            else if (PageMode == "Change")
            {
                InsertRequest(TSP.DataManager.InstitueCertificateManager.RequestType.change);
            }
            else if (PageMode == "Revival")
                InsertRequest(TSP.DataManager.InstitueCertificateManager.RequestType.Revival);

        }



    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Institue.aspx?");

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Enable();

        TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        PgMode.Value = Utility.EncryptQS("Edit");
        RoundPanelMain.HeaderText = "ویرایش";
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsId=" + Utility.EncryptQS(_InsId.ToString()) + "&InsCId=" + Utility.EncryptQS(_InsCId.ToString()) + "&PgMd=" + PgMode.Value.ToString());
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsId=" + Utility.EncryptQS(_InsId.ToString()) + "&InsCId=" + Utility.EncryptQS(_InsCId.ToString()) + "&PgMd=" + PgMode.Value.ToString());
                break;
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsId=" + Utility.EncryptQS(_InsId.ToString()) + "&InsCId=" + Utility.EncryptQS(_InsCId.ToString()) + "&PgMd=" + PgMode.Value.ToString());
                break;
            case "InsTeacher":
                Response.Redirect("InstitueTeachers.aspx?InsId=" + Utility.EncryptQS(_InsId.ToString()) + "&InsCId=" + Utility.EncryptQS(_InsCId.ToString()) + "&PgMd=" + PgMode.Value.ToString());
                break;
        }
    }

    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetKey()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            _InsId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["InsId"]));
            _InsCId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["InsCId"]));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);
            CheckWorkFlowPermission();
        }
    }

    private void SetMode(string Mode)
    {
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        switch (Mode)
        {
            case "View":
                Disable();
                if (Utility.IsDBNullOrNullValue(_InsCId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(_InsCId);
                RoundPanelMain.HeaderText = "مشاهده";
                MenuInstitue.Enabled = true;
                break;
            case "New":
                Enable();
                btnSave2.Enabled = true;
                btnSave.Enabled = true;
                BtnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                RoundPanelMain.HeaderText = "جدید";
                MenuInstitue.Enabled = false;
                ClearForm();
                lblWorkFlowState.Visible = false;
                break;
            case "Edit":
                SetEditMode();
                break;
            case "Change":
            case "Revival":
                Enable();
                if (Utility.IsDBNullOrNullValue(_InsCId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillForm(_InsCId);
                RoundPanelMain.Enabled = true;
                if (Mode == "Change")
                    RoundPanelMain.HeaderText = "جدید-درخواست تغییرات";
                else
                    RoundPanelMain.HeaderText = "جدید-درخواست تمدید";
                MenuInstitue.Enabled = false;
                break;
        }
    }

    private void SetEditMode()
    {
        Enable();

        if (Utility.IsDBNullOrNullValue(_InsCId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        FillForm(_InsCId);
        RoundPanelMain.Enabled = true;
        RoundPanelMain.HeaderText = "ویرایش";
        MenuInstitue.Enabled = true;
    }

    protected void FillForm(int InsCId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.InstitueCertificateManager manager = new TSP.DataManager.InstitueCertificateManager();

        manager.FindByCode(InsCId);
        if (manager.Count == 1)
        {
            ASPxComboBoxCity.DataBind();
            if (!Utility.IsDBNullOrNullValue(manager[0]["CitId"]))
                ASPxComboBoxCity.SelectedIndex = ASPxComboBoxCity.Items.FindByValue(manager[0]["CitId"]).Index;
            txtInsName.Text = manager[0]["InsName"].ToString();
            txtManager.Text = manager[0]["Manager"].ToString();
            txtRegNo.Text = manager[0]["RegNo"].ToString();
            txtRegDate.Text = manager[0]["RegDate"].ToString();
            txtRegPlace.Text = manager[0]["RegPlace"].ToString();
            // txtFileNo.Text = manager[0]["FileNo"].ToString();
            txtTel1.Text = manager[0]["Tel1"].ToString();
            txtTel2.Text = manager[0]["Tel2"].ToString();
            txtMobileNo.Text = manager[0]["MobileNo"].ToString();
            txtAddress.Text = manager[0]["Address"].ToString();
            txtEmail.Text = manager[0]["Email"].ToString();
            txtWebSite.Text = manager[0]["WebSite"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();
            txtEndDate.Text = manager[0]["EndDate"].ToString();
            txtStartDate.Text = manager[0]["StartDate"].ToString();
            txtFileNo.Text = manager[0]["FileNo"].ToString();
            txtSerialNo.Text = manager[0]["SerialNo"].ToString();
            lblWorkFlowState.Text = "وضعیت درخواست: " + manager[0]["TaskName"].ToString();
        }
    }

    protected void ClearForm()
    {

        //txtCity.Text = "";
        // chbInActive.Checked = false;
        ASPxComboBoxCity.SelectedIndex = -1;
        txtRegDate.Text =
        txtDesc.Text = txtAddress.Text = txtDesc.Text = txtEmail.Text =
            txtInsName.Text = txtManager.Text = txtMobileNo.Text = txtRegDate.Text = txtRegNo.Text = txtRegPlace.Text = txtTel1.Text = txtTel2.Text = txtWebSite.Text = "";
    }

    protected void Disable()
    {
        ASPxComboBoxCity.ClientEnabled =
        txtInsName.Enabled =
        txtManager.Enabled =
        txtRegNo.Enabled =
        txtRegDate.Enabled =
        txtRegPlace.Enabled =
        txtTel1.Enabled =
        txtTel2.Enabled =
        txtMobileNo.Enabled =
        txtAddress.Enabled =
        txtEmail.Enabled =
        txtWebSite.Enabled =
        txtDesc.Enabled = false;
    }

    protected void Enable()
    {
        ASPxComboBoxCity.ClientEnabled =
        txtInsName.Enabled =
        txtManager.Enabled =
        txtRegNo.Enabled =
        txtRegDate.Enabled =
        txtRegPlace.Enabled =
        txtTel1.Enabled =
        txtTel2.Enabled =
        txtMobileNo.Enabled =
        txtAddress.Enabled =
        txtEmail.Enabled =
        txtWebSite.Enabled =
        txtDesc.Enabled = true;
    }

    #region Insert-Edit
    protected void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        TransactionManager.Add(InstitueManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(InstitueCertificateManager);
        TransactionManager.Add(WorkFlowTaskManager);

        String Password = "";
        int CitId = -1;
        int NmcId = -1;
        try
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            NmcId = FindNmcId(TaskId);
            if (NmcId <= -1)
            {
                return;
            }

            DataTable dtIns = InstitueManager.SearchInstitueByName(txtInsName.Text.Trim());
            if (dtIns.Rows.Count > 0)
            {
                SetMessage("موسسه تکراری می باشد.");
                return;
            }
            if (ASPxComboBoxCity.SelectedItem == null)
            {
                SetMessage("شهر را انتخاب نمایید.");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        try
        {
            CitId = Convert.ToInt32(ASPxComboBoxCity.SelectedItem.Value);
            TransactionManager.BeginSave();
            DataRow row = InstitueManager.NewRow();
            row["CitId"] = CitId;
            row["InsName"] = txtInsName.Text;
            row["Manager"] = txtManager.Text;
            row["RegNo"] = txtRegNo.Text;
            row["RegDate"] = txtRegDate.Text;
            row["RegPlace"] = txtRegPlace.Text;
            row["Tel1"] = txtTel1.Text;
            row["Tel2"] = txtTel2.Text;
            row["MobileNo"] = txtMobileNo.Text;
            row["Address"] = txtAddress.Text;
            row["Email"] = txtEmail.Text;
            row["WebSite"] = txtWebSite.Value;
            row["Description"] = txtDesc.Text;
            row["InActive"] = 0;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            InstitueManager.AddRow(row);

            if (InstitueManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            #region UserLog
            Password = Utility.GeneratePassword();
            DataRow logRow = LoginManager.NewRow();
            logRow.BeginEdit();
            logRow["UserName"] = "Ins" + InstitueManager[0]["InsId"].ToString();
            if (!string.IsNullOrEmpty(InstitueManager[0]["RegNo"].ToString()))
                logRow["Password"] = Utility.EncryptPassword(Password);
            logRow["UltId"] = 6;
            logRow["MeId"] = InstitueManager[0]["InsId"].ToString();
            logRow["Email"] = InstitueManager[0]["Email"].ToString();
            logRow["IsValid"] = 1;
            logRow["UserId2"] = Utility.GetCurrentUser_UserId();
            logRow["ModifiedDate"] = DateTime.Now;
            logRow.EndEdit();
            LoginManager.AddRow(logRow);
            if (LoginManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion

            #region InsCertificate
            DataRow InsCrt = InstitueCertificateManager.NewRow();
            InsCrt["Type"] = 0;
            InsCrt["InsId"] = InstitueManager[0]["InsId"];
            InsCrt["InsName"] = txtInsName.Text;
            InsCrt["Manager"] = txtManager.Text;
            InsCrt["RegNo"] = txtRegNo.Text;
            InsCrt["RegDate"] = txtRegDate.Text;
            InsCrt["RegPlace"] = txtRegPlace.Text;
            InsCrt["Tel1"] = txtTel1.Text;
            InsCrt["Tel2"] = txtTel2.Text;
            InsCrt["MobileNo"] = txtMobileNo.Text;
            InsCrt["Address"] = txtAddress.Text;
            InsCrt["Email"] = txtEmail.Text;
            InsCrt["WebSite"] = txtWebSite.Value;
            InsCrt["Description"] = txtDesc.Text;
            InsCrt["IsConfirmed"] = 0;
            InsCrt["FileNo"] = txtFileNo.Text;
            InsCrt["SerialNo"] = txtSerialNo.Text;
            InsCrt["StartDate"] = txtStartDate.Text;
            InsCrt["EndDate"] = txtEndDate.Text;
            InsCrt["UserId"] = Utility.GetCurrentUser_UserId();
            InsCrt["ModifiedDate"] = DateTime.Now;

            InstitueCertificateManager.AddRow(InsCrt);

            if (InstitueCertificateManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion

            #region WF
            int TableId = int.Parse(InstitueCertificateManager[0]["InsCId"].ToString());

            if (!StartWorkFlow(TableId, NmcId, WorkFlowStateManager, TransactionManager))
            {
                return;
            }

            #endregion

            TransactionManager.EndSave();

            #region Next Task Of Insert
            TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            MenuInstitue.Enabled = true;
            _InsId = Convert.ToInt32(InstitueManager[0]["InsId"]);
            _InsCId = TableId;
            PgMode.Value = Utility.EncryptQS("Edit");
            RoundPanelMain.HeaderText = "ویرایش";
            MenuInstitue.Enabled = true;
            SetMessage("  ذخیره با کد عضویت " + "Ins" + InstitueManager[0]["InsId"].ToString() + "و رمز عبور " + Password + " انجام شد");
            //CeckPermissionForJudge();
            //??????????
            //////TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
            //////CityManager.FindByCode(CitId);
            //////if (CityManager.Count > 0)
            //////{
            //////    txtCity.Text = CityManager[0]["CitName"].ToString();
            //////}
            #endregion
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    SetMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    private Boolean StartWorkFlow(int TableId, int NmcId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count <= 0)
        {
            TransactionManager.CancelSave();
            SetMessage("خطایی در ذخیره انجام شد.");
            return false;
        }
        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

        DataRow StateRow = WorkFlowStateManager.NewRow();
        StateRow["TaskId"] = TaskId;
        StateRow["TableId"] = TableId;
        StateRow["NmcId"] = NmcId;
        StateRow["SubOrder"] = 1;
        //StateRow["Description"] = "";
        StateRow["StateType"] = 0;
        StateRow["UpdateTableType"] = TableId;
        StateRow["UserId"] = Utility.GetCurrentUser_UserId();
        StateRow["ModifiedDate"] = DateTime.Now;

        WorkFlowStateManager.AddRow(StateRow);
        if (WorkFlowStateManager.Save() <= 0)
        {

            TransactionManager.CancelSave();
            SetMessage("خطایی در ذخیره انجام شد.");
            return false;
        }
        SetMessage("ذخیره انجام شد.");

        return true;
    }

    protected void Edit(int InsCId)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        try
        {
            InstitueCertificateManager.FindByCode(InsCId);
            if (InstitueCertificateManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            if (ASPxComboBoxCity.SelectedItem == null)
            {
                SetMessage("شهر را انتخاب نمایید.");
                return;
            }
            InstitueCertificateManager[0].BeginEdit();
            InstitueCertificateManager[0]["CitId"] = ASPxComboBoxCity.SelectedItem.Value;
            InstitueCertificateManager[0]["InsName"] = txtInsName.Text;
            InstitueCertificateManager[0]["Manager"] = txtManager.Text;
            InstitueCertificateManager[0]["RegNo"] = txtRegNo.Text;
            InstitueCertificateManager[0]["RegDate"] = txtRegDate.Text;
            InstitueCertificateManager[0]["RegPlace"] = txtRegPlace.Text;
            InstitueCertificateManager[0]["Tel1"] = txtTel1.Text;
            InstitueCertificateManager[0]["Tel2"] = txtTel2.Text;
            InstitueCertificateManager[0]["MobileNo"] = txtMobileNo.Text;
            InstitueCertificateManager[0]["Address"] = txtAddress.Text;
            InstitueCertificateManager[0]["Email"] = txtEmail.Text;
            InstitueCertificateManager[0]["WebSite"] = txtWebSite.Value;
            InstitueCertificateManager[0]["Description"] = txtDesc.Text;
            InstitueCertificateManager[0]["FileNo"] = txtFileNo.Text;
            InstitueCertificateManager[0]["SerialNo"] = txtSerialNo.Text;
            InstitueCertificateManager[0]["StartDate"] = txtStartDate.Text;
            InstitueCertificateManager[0]["EndDate"] = txtEndDate.Text;
            InstitueCertificateManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            InstitueCertificateManager[0]["ModifiedDate"] = DateTime.Now;
            InstitueCertificateManager[0].EndEdit();

            if (InstitueCertificateManager.Save() <= 0)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            //  _InsId = Convert.ToInt32(InstitueCertificateManager[0]["InsId"]);
            PgMode.Value = Utility.EncryptQS("Edit");
            RoundPanelMain.HeaderText = "ویرایش";
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

    private void InsertRequest(TSP.DataManager.InstitueCertificateManager.RequestType RequestType)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(InstitueCertificateManager);
        try
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int NmcId = -1;
            NmcId = FindNmcId(TaskId);
            if (NmcId == -1) return;
            TransactionManager.BeginSave();
            InstitueCertificateManager.FindByCode(_InsCId);
            if (InstitueCertificateManager.Count <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("امکان تمدید گواهینامه انتخاب شده وجود ندارد.");
                return;
            }
            DataRow InsCertificateRow = InstitueCertificateManager.NewRow();
            InsCertificateRow["Type"] = (int)RequestType;
            InsCertificateRow["InsId"] = _InsId;
            InsCertificateRow["FileNo"] = InstitueCertificateManager[0]["FileNo"].ToString();
            if (ASPxComboBoxCity.SelectedItem != null)
                InsCertificateRow["CitId"] = ASPxComboBoxCity.SelectedItem.Value;
            InsCertificateRow["InsName"] = txtInsName.Text;
            InsCertificateRow["Manager"] = txtManager.Text;
            InsCertificateRow["RegNo"] = txtRegNo.Text;
            InsCertificateRow["RegDate"] = txtRegDate.Text;
            InsCertificateRow["RegPlace"] = txtRegPlace.Text;
            InsCertificateRow["Tel1"] = txtTel1.Text;
            InsCertificateRow["Tel2"] = txtTel2.Text;
            InsCertificateRow["MobileNo"] = txtMobileNo.Text;
            InsCertificateRow["Address"] = txtAddress.Text;
            InsCertificateRow["Email"] = txtEmail.Text;
            InsCertificateRow["WebSite"] = txtWebSite.Value;
            InsCertificateRow["Description"] = txtDesc.Text;
            InsCertificateRow["FileNo"] = txtFileNo.Text;
            InsCertificateRow["SerialNo"] = txtSerialNo.Text;
            InsCertificateRow["StartDate"] = txtStartDate.Text;
            InsCertificateRow["EndDate"] = txtEndDate.Text;
            InsCertificateRow["Date"] = Utility.GetDateOfToday();
            InsCertificateRow["IsConfirmed"] = 0;
            InsCertificateRow["UserId"] = Utility.GetCurrentUser_UserId();
            InsCertificateRow["ModifiedDate"] = DateTime.Now;

            InstitueCertificateManager.AddRow(InsCertificateRow);
            if (InstitueCertificateManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                return;
            }
            InstitueCertificateManager.DataTable.AcceptChanges();
            int InsCerId = Convert.ToInt32(InstitueCertificateManager[InstitueCertificateManager.Count - 1]["InsCId"]);

            string Description = "شروع گردش کار درخواست  موسسه";
            int WfStartId = WorkFlowStateManager.StartWorkFlow(InsCerId, (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo, NmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStartId <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            //DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
            //WorkflowStateRow["TaskId"] = TaskId;
            //WorkflowStateRow["TableId"] = InsCerId;
            //WorkflowStateRow["NmcId"] = NmcId;
            //WorkflowStateRow["SubOrder"] = 1;
            //WorkflowStateRow["UserId"] = Utility.GetCurrentUser_UserId();
            //WorkflowStateRow["ModifiedDate"] = DateTime.Now;

            //WorkFlowStateManager.AddRow(WorkflowStateRow);
            //if (WorkFlowStateManager.Save() <= 0)
            //{
            //    TransactionManager.CancelSave();
            //    SetMessage("خطایی در ذخیره انجام شد.");
            //    return;
            //}
            TransactionManager.EndSave();
            SetMessage("ذخیره انجام شد.");
            #region Next Task Of Insert
            TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            MenuInstitue.Enabled = true;
            _InsCId = InsCerId;
            PgMode.Value = Utility.EncryptQS("Edit");
            RoundPanelMain.HeaderText = "ویرایش";
            #endregion
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
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
    #endregion

    private void CheckUserPermission()
    {
        //****Check table permission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanNew || per.CanEdit;
        btnSave2.Enabled = per.CanNew || per.CanEdit;
    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        //int TaskOrder = -1;
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        string PageMode = Utility.DecryptQS(PgMode.Value.ToString());
        CheckWorkFlowPermissionForSave(PageMode);
        if (PageMode != "New" && PageMode != "Change" && PageMode != "Revival")
        {
            CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.Institue;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                case "Change":
                case "Revival":
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
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت ثبت اطلاعات موسسه جدید را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.Institue;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _InsCId, TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
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
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
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
                    break;
            }
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }
        //if (Permisssion < 0)
        //{
        //    btnEdit.Enabled = false;
        //    btnEdit2.Enabled = false;
        //    btnSave.Enabled = false;
        //    btnSave2.Enabled = false;

        //}

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    //private void CheckWorkFlowPermissionForJudge()
    //{
    //    //****TableId      
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.Institue;

    //    int WFCode = (int)TSP.DataManager.WorkFlows.InstitueConfirming;
    //    int SpecifyObserverTaskCode = (int)TSP.DataManager.WorkFlowTask.LearningManagerConfirmingAndSpecifyObserverForInstitue;
    //    int ObserverConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.ObserverConfirmingInstitue;
    //    int CommitteeConfirmingInstitueTasckCode = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingInstitue;

    //    TSP.DataManager.WFPermission WFPerSpecifyObserver = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SpecifyObserverTaskCode, WFCode, _InsId, Utility.GetCurrentUser_UserId());
    //    TSP.DataManager.WFPermission WFPerObserverConfirming = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ObserverConfirmingTaskCode, WFCode, _InsId, Utility.GetCurrentUser_UserId());
    //    TSP.DataManager.WFPermission WFPerCommiteeConfirming = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(CommitteeConfirmingInstitueTasckCode, WFCode, _InsId, Utility.GetCurrentUser_UserId());

    //    this.ViewState["btnJudgmentVisible"] = btnJudgment.Visible = btnJudgment2.Visible = (WFPerObserverConfirming.BtnNew || WFPerSpecifyObserver.BtnNew || WFPerCommiteeConfirming.BtnNew);
    //}


    //private int FindNmcId(TSP.DataManager.LoginManager LoginManager, TSP.DataManager.TransactionManager TransactionManager)
    //{
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    LoginManager.FindByCode(UserId);
    //    int NmcId = -1;
    //    if (LoginManager.Count > 0)
    //    {
    //        int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
    //        int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
    //        NezamChartManager.FindByEmpId(EmpId, UltId);
    //        if (NezamChartManager.Count > 0)
    //        {
    //            NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
    //        }
    //        else
    //        {
    //            TransactionManager.CancelSave();
    //            DivReport.Visible = true;
    //            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //        }
    //    }
    //    else
    //    {
    //        TransactionManager.CancelSave();
    //        Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
    //        return (-1);
    //    }
    //    return (NmcId);
    //}

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }

    private Boolean CheckWorkFlowPermissionForLearningManagerConfirmingInstitue()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();


        int LearningManagerConfirmingInstitueCode = (int)TSP.DataManager.WorkFlowTask.LearningManagerConfirmingAndSpecifyObserverForInstitue;
        WorkFlowTaskManager.FindByTaskCode(LearningManagerConfirmingInstitueCode);
        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        TaskDoerManager.FindByTaskId(TaskId);

        if (TaskDoerManager.Count > 0)
        {
            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
            NezamMemberChartManager.FindByNcId(NcId);

            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

            LoginManager.FindByMeIdUltId(EmpId, 4);
            if (LoginManager.Count > 0)
            {
                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                int CurrentUserId = Utility.GetCurrentUser_UserId();
                if (CurrentUserId == userId)
                {
                    return true;
                    // MenuTeacherInfo.Items[4].Enabled = true;
                }
                else
                {
                    return false;
                    //  MenuTeacherInfo.Items[4].Enabled = false;
                }
            }
            else
            {
                return false;
                // MenuTeacherInfo.Items[4].Enabled = false;
            }
        }
        else
        {
            return false;
            //  MenuTeacherInfo.Items[4].Enabled = false;
        }
    }

    private Boolean CheckWorkFlowPermissionForObserverConfrimingInstitue()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();


        int LearningManagerConfirmingInstitueCode = (int)TSP.DataManager.WorkFlowTask.ObserverConfirmingInstitue;
        WorkFlowTaskManager.FindByTaskCode(LearningManagerConfirmingInstitueCode);
        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        TaskDoerManager.FindByTaskId(TaskId);

        if (TaskDoerManager.Count > 0)
        {
            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
            NezamMemberChartManager.FindByNcId(NcId);

            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

            LoginManager.FindByMeIdUltId(EmpId, 4);
            if (LoginManager.Count > 0)
            {
                int userId = int.Parse(LoginManager[0]["UserId"].ToString());

                int CurrentUserId = Utility.GetCurrentUser_UserId();
                if (CurrentUserId == userId)
                {
                    return true;
                    // MenuTeacherInfo.Items[4].Enabled = true;
                }
                else
                {
                    return false;
                    //  MenuTeacherInfo.Items[4].Enabled = false;
                }
            }
            else
            {
                return false;
                // MenuTeacherInfo.Items[4].Enabled = false;
            }
        }
        else
        {
            return false;
            //  MenuTeacherInfo.Items[4].Enabled = false;
        }
    }

    private Boolean CheckWorkFlowPermissionForCommitteeConfirmingInstitue()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();


        int LearningManagerConfirmingInstitueCode = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingInstitue;
        WorkFlowTaskManager.FindByTaskCode(LearningManagerConfirmingInstitueCode);
        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        TaskDoerManager.FindByTaskId(TaskId);

        if (TaskDoerManager.Count > 0)
        {
            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
            NezamMemberChartManager.FindByNcId(NcId);

            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

            LoginManager.FindByMeIdUltId(EmpId, 4);
            if (LoginManager.Count > 0)
            {
                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                int CurrentUserId = Utility.GetCurrentUser_UserId();
                if (CurrentUserId == userId)
                {
                    return true;
                    // MenuTeacherInfo.Items[4].Enabled = true;
                }
                else
                {
                    return false;
                    //  MenuTeacherInfo.Items[4].Enabled = false;
                }
            }
            else
            {
                return false;
                // MenuTeacherInfo.Items[4].Enabled = false;
            }
        }
        else
        {
            return false;
            //  MenuTeacherInfo.Items[4].Enabled = false;
        }
    }

    //private void CeckPermissionForJudge()
    //{
    //    int LearningManagerConfirmingInstitueOrder = -1;
    //    int ObserverConfrimingInstitueOrder = -1;
    //    int CommitteeConfirmingInstitueOrder = -1;

    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.InstitueJudgmentManager InstitueJudgmentManager = new TSP.DataManager.InstitueJudgmentManager();
    //    InstitueJudgmentManager.ClearBeforeFill = true;

    //    WorkFlowTaskManager.ClearBeforeFill = true;

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.LearningManagerConfirmingAndSpecifyObserverForInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        LearningManagerConfirmingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ObserverConfirmingInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        ObserverConfrimingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        CommitteeConfirmingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    int CurrentTaskOrder = -1;                
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.Institue;
    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType,_InsId);
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        lblWorkFlowState.Visible = true;
    //        lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //        string PageMode = "New";
    //        if (CurrentTaskOrder == LearningManagerConfirmingInstitueOrder)
    //        {

    //            DataTable dtLearningManager = InstitueJudgmentManager.SelectLearningJudgment(int.Parse(InsId));
    //            if (dtLearningManager.Rows.Count > 0)
    //                PageMode = "Edit";
    //            btnJudgment.Enabled = true;
    //            // MenuInstitue.Items[4].Enabled = true;
    //            // Response.Redirect("InstitueLearningManagerJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);
    //        }
    //        else
    //        {
    //            if (CurrentTaskOrder == ObserverConfrimingInstitueOrder)
    //            {
    //                TSP.DataManager.InstitueObserveManager InstitueObserveManager = new TSP.DataManager.InstitueObserveManager();
    //                InstitueObserveManager.FindByInstitue(int.Parse(InsId));
    //                int InsObserveId = -1;
    //                if (InstitueObserveManager.Count > 0)
    //                    PageMode = "Edit";
    //                btnJudgment.Enabled = true;
    //                // MenuInstitue.Items[4].Enabled = true;
    //                // Response.Redirect("InstitueObserveJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);

    //            }
    //            else
    //            {
    //                if (CurrentTaskOrder == CommitteeConfirmingInstitueOrder)
    //                {
    //                    DataTable dtLearningManager = InstitueJudgmentManager.SelectComitteeJudgment(int.Parse(InsId));
    //                    if (dtLearningManager.Rows.Count > 0)
    //                        PageMode = "Edit";
    //                    btnJudgment.Enabled = true;
    //                    //MenuInstitue.Items[4].Enabled = true;
    //                    //Response.Redirect("InstitueCommitteeJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);
    //                }
    //                else
    //                {
    //                    btnJudgment.Enabled = false;
    //                    // MenuInstitue.Items[4].Enabled = false;
    //                }
    //            }
    //        }

    //    }
    //    else
    //    {
    //        btnJudgment.Enabled = false;
    //        //MenuInstitue.Items[4].Enabled = false;
    //    }
    //    this.ViewState["btnJudgment"] = btnJudgment.Enabled;
    //}
    #endregion

    //private void SendForJudge()
    //{
    //    int LearningManagerConfirmingInstitueOrder = -1;
    //    int ObserverConfrimingInstitueOrder = -1;
    //    int CommitteeConfirmingInstitueOrder = -1;

    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.InstitueJudgmentManager InstitueJudgmentManager = new TSP.DataManager.InstitueJudgmentManager();
    //    InstitueJudgmentManager.ClearBeforeFill = true;

    //    WorkFlowTaskManager.ClearBeforeFill = true;

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.LearningManagerConfirmingAndSpecifyObserverForInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        LearningManagerConfirmingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ObserverConfirmingInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        ObserverConfrimingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());

    //    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingInstitue);
    //    if (WorkFlowTaskManager.Count != 0)
    //        CommitteeConfirmingInstitueOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    int CurrentTaskOrder = -1;
    //    //****TableId
    //    string InsId = Utility.DecryptQS(InstitueId.Value.ToString());
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.Institue;
    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(InsId));
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //        string PageMode = "New";
    //        if (CurrentTaskOrder == LearningManagerConfirmingInstitueOrder)
    //        {

    //            DataTable dtLearningManager = InstitueJudgmentManager.SelectLearningJudgment(int.Parse(InsId));
    //            if (dtLearningManager.Rows.Count > 0)
    //                PageMode = "Edit";
    //            Response.Redirect("InstitueLearningManagerJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);
    //        }
    //        else
    //        {
    //            if (CurrentTaskOrder == ObserverConfrimingInstitueOrder)
    //            {
    //                TSP.DataManager.InstitueObserveManager InstitueObserveManager = new TSP.DataManager.InstitueObserveManager();
    //                InstitueObserveManager.FindByInstitue(int.Parse(InsId));
    //                int InsObserveId = -1;
    //                if (InstitueObserveManager.Count > 0)
    //                    PageMode = "Edit";
    //                Response.Redirect("InstitueObserveJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);

    //            }
    //            else
    //            {
    //                if (CurrentTaskOrder == CommitteeConfirmingInstitueOrder)
    //                {
    //                    DataTable dtLearningManager = InstitueJudgmentManager.SelectComitteeJudgment(int.Parse(InsId));
    //                    if (dtLearningManager.Rows.Count > 0)
    //                        PageMode = "Edit";
    //                    Response.Redirect("InstitueCommitteeJudgment.aspx?InsId=" + Utility.EncryptQS(InsId) + "&PageMode=" + Utility.EncryptQS(PageMode) + "&PrPgMd=" + PgMode.Value);
    //                }
    //            }
    //        }

    //    }

    //}

    //private int FindInstitueCertificate(int InsId)
    //{
    //    TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

    //    DataTable dtInsCertUnknown = InstitueCertificateManager.SelectLastVersion(InsId, -1);
    //    int InsCertType = -1;
    //    int CertConfirmState = -1;
    //    if (dtInsCertUnknown.Rows.Count > 0)
    //    {
    //        InsCertType = int.Parse(dtInsCertUnknown.Rows[0]["Type"].ToString());
    //        CertConfirmState = int.Parse(dtInsCertUnknown.Rows[0]["IsConfirmed"].ToString());
    //        if (CertConfirmState == 1)
    //        {
    //            InsCertType = -2;
    //        }
    //        if (CertConfirmState == 2)
    //        {
    //            InsCertType = -3;
    //        }
    //    }
    //    //else
    //    //{
    //    //    DataTable dtInsCertConfirm = InstitueCertificateManager.SelectLastVersion(InsId, 1);
    //    //    if (dtInsCertConfirm.Rows.Count > 0)
    //    //    {
    //    //        InsCertType= - 2;
    //    //    }
    //    //    else
    //    //    {
    //    //        DataTable dtInsCertReject = InstitueCertificateManager.SelectLastVersion(InsId,-1);
    //    //        //if (dtInsCertReject.Rows.Count > 0)
    //    //        //{
    //    //        //    InsCertType = -3;
    //    //        //}
    //    //    }
    //    //}
    //    return InsCertType;
    //}

    //  private void CheckCertificatePermission(int InsId)
    //  {
    //int CertType = FindInstitueCertificate(InsId);
    //if (CertType == -1)
    //{
    //    this.DivReport.Visible = true;
    //    this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
    //    return;
    //}
    //if (CertType == -2)
    //{
    //    this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده تایید شده است.";
    //    btnEdit.Enabled = false;
    //    btnEdit2.Enabled = false;
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //}
    //if (CertType == -3)
    //{
    //    this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده عدم تایید می باشد.";
    //    btnEdit.Enabled = false;
    //    btnEdit2.Enabled = false;
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //}
    //  string PageMode = Utility.DecryptQS(PgMode.Value);
    // if (CertType == 1 || CertType == 2)
    // {
    //switch (PageMode)
    //{
    //    case "View":

    //        btnEdit.Enabled = false;
    //        btnEdit2.Enabled = false;
    //        break;
    //    case "Edit":
    //        btnEdit.Enabled = false;
    //        btnEdit2.Enabled = false;
    //        btnSave.Enabled = false;
    //        btnSave2.Enabled = false;
    //        break;
    //}
    // }
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //}

    #endregion
}
