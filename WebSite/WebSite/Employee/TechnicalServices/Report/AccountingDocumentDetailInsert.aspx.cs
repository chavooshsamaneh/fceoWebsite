using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_AccountingDocumentDetailInsert : System.Web.UI.Page
{

    int _AccDocId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["AccDocId"]);
        }
        set
        {
            HiddenFieldPage["AccDocId"] = value.ToString();
        }
    }
    int _AccDocDetailId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["AccDocDetailId"]);
        }
        set
        {
            HiddenFieldPage["AccDocDetailId"] = value.ToString();
        }
    }
    string _PageModeDocument
    {
        get
        {
            return HiddenFieldPage["PageModeDocument"].ToString();
        }
        set
        {
            HiddenFieldPage["PageModeDocument"] = value.ToString();
        }
    }
    string _PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value.ToString();
        }
    }
    int _ProjectObserversId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ProjectObserversId"]);
        }
        set
        {
            HiddenFieldPage["ProjectObserversId"] = value.ToString();
        }
    }
    int _ObserversMemberTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ObserversMemberTypeId"]);
        }
        set
        {
            HiddenFieldPage["ObserversMemberTypeId"] = value.ToString();
        }
    }
    int _ProjectId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ProjectId"]);
        }
        set
        {
            HiddenFieldPage["ProjectId"] = value.ToString();
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = btnSave2.Enabled = PerObserverReportList.CanNew || PerObserverReportList.CanEdit;
            BtnNew.Enabled = BtnNew2.Enabled = PerObserverReportList.CanNew;
            //ObjdAccountingDocumentDetail

            if (!IsPostBack)
            {
                SetKey();
            }
            if (this.ViewState["BtnSave"] != null)
                this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
            if (this.ViewState["BtnEdit"] != null)
                this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
            if (this.ViewState["BtnNew"] != null)
                this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "Edit":
                //Edit(_AccDocDetailId);
                break;
            case "New":
                Insert();
                break;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountingDocumentDetail.aspx?AccDocId=" + Utility.EncryptQS(_AccDocId.ToString()) + "&PrePgMd=" + Utility.EncryptQS(_PageModeDocument));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void txtProjectId_TextChanged(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(txtProjectId.Text))
        {
            SetMessage("کد پروژه را وارد نمایید");
            ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = "-2";
            return;
        }
        string ProjectId = txtProjectId.Text;
        ClearForm();
        txtProjectId.Text = ProjectId;

        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        ProjectManager.FindByProjectId(Convert.ToInt32(txtProjectId.Text));
        if (ProjectManager.Count == 0)
        {
            SetMessage("کد پروژه وارد شده معتبر نمی باشد.");
            return;
        }

        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId() && Convert.ToInt32(ProjectManager[0]["AgentId"]) != Utility.GetCurrentUser_AgentId())
        {
            SetMessage("شما تنها قادر به ثبت لیست جهت پروژه های مربوط به نمایندگی خود می باشید.");
            return;
        }
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = txtProjectId.Text;
        _ProjectId = Convert.ToInt32(ProjectId);
        txtProjectOwner.Text = ProjectManager[0]["OwnerFullName"].ToString();
        txtRegNo.Text = ProjectManager[0]["MainRegisterNo"].ToString();

        DataTable dtFish = FillFish();
        checkFish(dtFish);

    }


    protected void txtRegNo_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtRegNo.Text))
        {
            SetMessage("پلاک ثبتی پروژه را وارد نمایید");
            ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = "-2";
            return;
        }
        string RegNo = txtRegNo.Text;
        ClearForm();
        txtRegNo.Text = RegNo;
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        System.Data.DataTable dtProject = ProjectManager.SearchProjectByMainRegisterNo(txtRegNo.Text);
        if (dtProject.Rows.Count == 0)
        {
            SetMessage("پلاک ثبتی وارد شده معتبر نمی باشد.");
            return;
        }
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId() && Convert.ToInt32(ProjectManager[0]["AgentId"]) != Utility.GetCurrentUser_AgentId())
        {
            SetMessage("شما تنها قادر به ثبت لیست جهت پروژه های مربوط به نمایندگی خود می باشید.");
            return;
        }
        txtProjectId.Text = dtProject.Rows[0]["ProjectId"].ToString();
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = txtProjectId.Text;

        txtProjectOwner.Text = dtProject.Rows[0]["OwnerFullName"].ToString();
        DataTable dtFish = FillFish();
        checkFish(dtFish);

    }
    protected void comboProjectObserver_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(comboProjectObserver.SelectedItem))
        {
            return;
        }
        _ProjectObserversId = Convert.ToInt32(comboProjectObserver.SelectedItem.Value);
        try
        {
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            ProjectRequestManager.UpdateObserverPriceByObserverId(_ProjectObserversId, null, Project_ObserversManager);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطا در بازخوانی اطلاعات ایجاد شده است");
        }
        FillPrjoectObserver(_ProjectObserversId);
        if (string.IsNullOrWhiteSpace(txtYearName.Text))
        {
            SetMessage("سال تعرفه برای ناظر انتخاب شده مشخص نشده است و محاسبات با خطا مواجه خواهد شد.لطفا اقدام به تصحیح اطلاعات در پروژه مربوطه  نمایید");
            return;
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
            if (string.IsNullOrEmpty(Request.QueryString["AccDocId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["AccDocDetailId"])
                 || string.IsNullOrEmpty(Request.QueryString["PrePgMd"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
            btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew || PerObserverReportList.CanEdit;

            _PageModeDocument = Utility.DecryptQS(Request.QueryString["PrePgMd"]);
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            _AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
            _AccDocDetailId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocDetailId"]));
            switch (_PageMode)
            {
                case "New":
                    SetNewMode();
                    break;
                case "Edit":
                    SetViewMode();
                    break;
                case "View":
                    SetViewMode();
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در بازیابی اطلاعات ایجاد شده است");
        }
    }
    private void SetNewMode()
    {
        _ObserversMemberTypeId=_ProjectObserversId = _ProjectId = _AccDocDetailId = -1;
        _PageMode = "New";
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = false;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew;
        RoundPanelPage.HeaderText = "جدید";
        ClearForm();
        SetEnable(true);
        FillFormListInfo(_AccDocId);
    }
    private void SetViewMode()
    {
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew || PerObserverReportList.CanEdit;
        SetEnable(false);
        FillFormListInfo(_AccDocId);

        FillDocumentDetail();
        RoundPanelPage.HeaderText = "مشاهده";
        btnSave.Enabled = btnSave2.Enabled = false;
    }
    private void SetEditMode()
    {
        _PageMode = "Edit";
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanEdit;
        SetEnable(true);
        FillFormListInfo(_AccDocId);
        FillDocumentDetail();
        RoundPanelPage.HeaderText = "ویرایش";
    }
    private void ClearForm()
    {
        txtProjectId.Text = txtRegNo.Text = txtProjectOwner.Text = txtMeId.Text = txtObsType.Text=
        txtObserverAccNo.Text = txtMeFileNo.Text = txtObserversTypeTitle.Text = txtIsPayFivePercent.Text =
        txtWage.Text = txtCapacityDecrement.Text = txtYearName.Text =
        txtPrice.Text = txtNezamShare.Text = txtObservershare.Text = txtInsuranceShare.Text = txtNezamKardanShare.Text =
        txtDescription.Text = "";
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = "-2";
        ObjectDataSourceObserver.SelectParameters["InActive"].DefaultValue = "0";
        
        comboProjectObserver.SelectedIndex = -1;
        GridViewAccountingDocumentDetail.DataSource = "";
        GridViewAccountingDocumentDetail.DataBind();
        ObjdContract.SelectParameters["PrjImpObsDsgnId"].DefaultValue = "-2";
        ObjdContract.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-2";
        GridViewContract.DataBind();
    }
    private void SetEnable(Boolean Enabled)
    {
        txtListName.Enabled = CmbType.Enabled =
         txtListDate.Enabled = txtListNo.Enabled = txtStatusName.Enabled = txtProjectOwner.Enabled = txtMeId.Enabled = txtObsType.Enabled =
        txtObserverAccNo.Enabled = txtMeFileNo.Enabled = txtObserversTypeTitle.Enabled =
        txtWage.Enabled = txtCapacityDecrement.Enabled = txtYearName.Enabled =
        txtPrice.Enabled = false;
        txtProjectId.Enabled = txtRegNo.Enabled =

        txtNezamShare.Enabled = txtObservershare.Enabled = txtInsuranceShare.Enabled = txtNezamKardanShare.Enabled =
        txtDescription.Enabled = comboProjectObserver.Enabled = txtIsPayFivePercent.ClientEnabled = Enabled;
    }
    private void FillFormListInfo(int AccDocId)
    {
        TSP.DataManager.TechnicalServices.AccountingDocumentManager AccountingDocumentManager = new TSP.DataManager.TechnicalServices.AccountingDocumentManager();
        AccountingDocumentManager.FindByCode(AccDocId);
        if (AccountingDocumentManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListDate"]))
            txtListDate.Text = AccountingDocumentManager[0]["ListDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListName"]))
            txtListName.Text = AccountingDocumentManager[0]["ListName"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListNo"]))
            txtListNo.Text = AccountingDocumentManager[0]["ListNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["StatusName"]))
            txtStatusName.Text = AccountingDocumentManager[0]["StatusName"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["Type"]))
        {
            CmbType.SelectedIndex = CmbType.Items.FindByValue(AccountingDocumentManager[0]["Type"].ToString()).Index;
            if (Convert.ToInt32(AccountingDocumentManager[0]["Type"]) == (int)TSP.DataManager.TSAccountingDocumentType.PayObserverShare)
            {
                txtNezamShare.Enabled = txtObservershare.Enabled = txtNezamKardanShare.Enabled = txtInsuranceShare.Enabled = false;
            }
        }
        else
            CmbType.SelectedIndex = -1;
    }
    private void FillDocumentDetail()
    {
        TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
        AccountingDocumentDetailManager.FindByCode(_AccDocDetailId);
        if (AccountingDocumentDetailManager.Count == 0)
        {
            SetMessage("کد پروژه وار شده صحیح نمی باشد");
            return;
        }
        _ProjectObserversId = Convert.ToInt32(AccountingDocumentDetailManager[0]["ProjectObserversId"]);
        _ProjectId = Convert.ToInt32(AccountingDocumentDetailManager[0]["ProjectId"]);
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["OwnerName"]))
            txtProjectOwner.Text = AccountingDocumentDetailManager[0]["OwnerName"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["Description"]))
            txtDescription.Text = AccountingDocumentDetailManager[0]["Description"].ToString();
        txtProjectId.Text = _ProjectId.ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["RegisteredNo"]))
            txtRegNo.Text = AccountingDocumentDetailManager[0]["RegisteredNo"].ToString();
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
        comboProjectObserver.DataBind();
        comboProjectObserver.SelectedIndex = comboProjectObserver.Items.FindByValue(_ProjectObserversId.ToString()).Index;

        FillPrjoectObserver(_ProjectObserversId);
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["NezamShare"]))
            txtNezamShare.Text = AccountingDocumentDetailManager[0]["NezamShare"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["ObserverShare"]))
            txtObservershare.Text = AccountingDocumentDetailManager[0]["ObserverShare"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["NezamKardanShare"]))
            txtNezamKardanShare.Text = AccountingDocumentDetailManager[0]["NezamKardanShare"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentDetailManager[0]["InsuranceShare"]))
            txtInsuranceShare.Text = AccountingDocumentDetailManager[0]["InsuranceShare"].ToString();
        FillFish();
    }
    private void FillPrjoectObserver(int ProjectObserversId)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        Project_ObserversManager.FindByProjectObserversId(ProjectObserversId);
        if (Project_ObserversManager.Count == 0)
        {
            SetMessage("خطا در بازخوانی اطلاعات ایجاد شده است");
            return;
        }
        _ProjectId = Convert.ToInt32(Project_ObserversManager[0]["ProjectId"]);
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["ID"]))
            txtMeId.Text = Project_ObserversManager[0]["ID"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["MemberTypeId"]))
        {
            _ObserversMemberTypeId = Convert.ToInt32(Project_ObserversManager[0]["MemberTypeId"]);
            if (Convert.ToInt32(Project_ObserversManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Member)
                txtObsType.Text = "کارشناس";
            else if (Convert.ToInt32(Project_ObserversManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                txtObsType.Text = "کاردان";

        }
        
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["BankAccNo"]))
            txtObserverAccNo.Text = Project_ObserversManager[0]["BankAccNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["No"]))
            txtMeFileNo.Text = Project_ObserversManager[0]["No"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["ObserversTypeTitle"]))
            txtObserversTypeTitle.Text = Project_ObserversManager[0]["ObserversTypeTitle"].ToString();
        //if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["IsPayFivePercent"]))
        // txtIsPayFivePercent.Text = "100"; Project_ObserversManager[0]["IsPayFivePercent"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["Wage"]))
            txtWage.Text = Project_ObserversManager[0]["Wage"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["CapacityDecrement"]))
            txtCapacityDecrement.Text = Project_ObserversManager[0]["CapacityDecrement"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["YearName"]))
            txtYearName.Text = Project_ObserversManager[0]["YearName"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["Price"]))
            txtPrice.Text = Convert.ToDouble(Project_ObserversManager[0]["Price"]).ToString("r");
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["NezamShare"]))
            HiddenFieldPage["NezamShare"] = txtNezamShare.Text = Project_ObserversManager[0]["NezamShare"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["NezamKardanShare"]))
            HiddenFieldPage["NezamKardanShare"] = txtNezamKardanShare.Text = Project_ObserversManager[0]["NezamKardanShare"].ToString();        

        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["ObserverShare"]))
            HiddenFieldPage["ObserverShare"] = txtObservershare.Text = Project_ObserversManager[0]["ObserverShare"].ToString();
        if (!Utility.IsDBNullOrNullValue(Project_ObserversManager[0]["InsuranceShare"]))
            HiddenFieldPage["InsuranceShare"] = txtInsuranceShare.Text = Project_ObserversManager[0]["InsuranceShare"].ToString();
        TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
        AccountingDocumentDetailManager.FindByObsever(ProjectObserversId);
        if (AccountingDocumentDetailManager.Count > 0)
        {
            GridViewAccountingDocumentDetail.DataSource = AccountingDocumentDetailManager.DataTable;
            GridViewAccountingDocumentDetail.DataBind();
        }


        ObjdContract.SelectParameters["PrjImpObsDsgnId"].DefaultValue = ProjectObserversId.ToString();
        ObjdContract.SelectParameters["ProjectIngridientTypeId"].DefaultValue = ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
        GridViewContract.DataBind();
    }
    private void Insert()
    {
        TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        try
        {
            //AccountingDocumentDetailManager.FindByObsever(_ProjectObserversId);
            //if (AccountingDocumentDetailManager.Count > 0 )
            //{                
            //    SetMessage("پیش از این  ناظر انتخاب شده در لیست شماره " + AccountingDocumentDetailManager[0]["ListNo"].ToString() + "ذخیره شده است");
            //    return;
            //}
            if (!string.IsNullOrWhiteSpace(txtMeId.Text))
            {
                string Msg = "";
                if (_ObserversMemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    if (!TSP.DataManager.MemberManager.CheckMembershipValidation(Convert.ToInt32(txtMeId.Text), ref Msg))
                    {
                        SetMessage(Msg);
                        return;
                    }
                }
            }
            int AccountingId = -1;
            AccountingDetailManager.SelectAccDetailByTableId(_ProjectObserversId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
            if (AccountingDetailManager.Count != 0)
            {
                AccountingId = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
            }
            double NezamShare = Convert.ToDouble(txtNezamShare.Text);
            double ObserverShare = Convert.ToDouble(txtObservershare.Text);
            double InsuranceShare = Convert.ToDouble(txtInsuranceShare.Text);
            double NezamKardanShare = Convert.ToDouble(txtNezamKardanShare.Text);
            
            AccountingDocumentDetailManager.InsertAccDocDetail(_ProjectId
                , _ProjectObserversId, NezamShare + ObserverShare + InsuranceShare+ NezamKardanShare, NezamShare, ObserverShare, InsuranceShare, NezamKardanShare, _AccDocId, (int)TSP.DataManager.TSDocumentDetailType.SaveReport
                , txtDescription.Text, Utility.GetCurrentUser_UserId(), AccountingId);

            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            //else
            //{
            //    SetMessage("پیش از این اسامی کلیه ناظران جستجو شده ذخیره شده است جهت چاپ گزارش مربوطه با استفاده از شماره لیست گزارش می توانید در صفحه لیست گزارش ها اقدام به چاپ لیست مورد نظر نمایید. ");
            //}
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private DataTable FillFish()
    {
       TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
        string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
        DataTable dt = AccountingManager.SelectExistAccountingByAccTypeListForDocumentDetailInsert(-1, TableType, _ProjectId, AccTypeList);
        GridViewProjFish.DataSource = dt;
        GridViewProjFish.DataBind();
   
        return dt;
    }
    private void checkFish(DataTable dtFish)
    {
        bool FlagAll = false;
        bool FlagOnce = false;
        if (dtFish.Rows.Count > 0)
        {

            int Status = Convert.ToInt32(dtFish.Rows[0]["Status"]);
            if (!Utility.IsDBNullOrNullValue(dtFish.Rows[0]["PaymentDate"]) && string.Compare(dtFish.Rows[0]["PaymentDate"].ToString(), txtListDate.Text) < 0 && Status == (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
            {
                FlagAll = true;
            }
            if (Utility.IsDBNullOrNullValue(dtFish.Rows[0]["PaymentDate"]) || (!Utility.IsDBNullOrNullValue(dtFish.Rows[0]["PaymentDate"]) && string.Compare(dtFish.Rows[0]["PaymentDate"].ToString(), txtListDate.Text) >= 0) || Status != (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
            {
                FlagOnce = true;
            }
        }

        this.btnSave.Enabled = this.btnSave2.Enabled = true;
        if (!FlagAll)
        {
            this.btnSave.Enabled = this.btnSave2.Enabled = false;
            SetMessage("برای ناظران این پروژه حداقل باید یک فیش پرداخت شده با تاریخ قبل از تاریخ لیست وجود داشته باشد");
        }
        if (FlagAll && FlagOnce)
        {
            SetMessage("برای ناظران این پروژه حداقل یک فیش پرداخت نشده و یا با تاریخ قبل از تاریخ لیست وجود دارد مراقب باشید");
        }
    }
    #endregion
}