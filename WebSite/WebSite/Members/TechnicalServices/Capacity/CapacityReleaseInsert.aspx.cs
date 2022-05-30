using DevExpress.Web;
using System;
using System.Data;
using System.IO;

public partial class Members_TechnicalServices_Capacity_CapacityReleaseInsert : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;
    public string _PageMode
    {
        get
        {
            return HiddenFieldPage["PgMd"].ToString();
        }
        set
        {
            HiddenFieldPage["PgMd"] = value.ToString();
        }
    }
    private int _CurrentCapRTaskCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CurrentCapRTaskCode"]);
        }
        set
        {
            HiddenFieldPage["CurrentCapRTaskCode"] = value.ToString();
        }
    }
    private int _CapRTaskId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CapRTaskId"]);
        }
        set
        {
            HiddenFieldPage["CapRTaskId"] = value.ToString();
        }
    }
    int _ObserverType
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ObserverType"]);
        }
        set
        {
            HiddenFieldPage["ObserverType"] = value;
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
            HiddenFieldPage["ProjectId"] = value;
        }
    }
    int _CapRId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CapRId"]);
        }
        set
        {
            HiddenFieldPage["CapRId"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        #region PageRefresh
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        #endregion

        if (!IsPostBack)
        {
            if (Request.QueryString["PRj"] == null || Request.QueryString["CapRId"] == null || Request.QueryString["PgMd"] == null)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
                return;
            }
            SetKeys();
            CheckWorkFlowPermissionForSave();
            this.ViewState["BtnSave"] = btnSave.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];

        if (Convert.ToInt32(HiddenFieldPage["Letter"]) == 1)
            hpFilePathLetter.ClientVisible = true;

    }
    protected void txtProjectCode_TextChanged(object sender, EventArgs e)
    {
        if (_PageMode != "New")
        {
            ShowMessage("در این حالت قادر به تغییر کد پروژه ثبت شده نیستید");
            return;
        }
        int ProjectId = -1;
        int.TryParse(txtProjectCode.Text, out ProjectId);
        if (ProjectId == -1)
        {
            ShowMessage("کد پروژه را صحیح وارد نمایید");
            return;
        }
        _ProjectId = ProjectId;
        if (!FillprojectInfo())
        {
            ClearForm();
            SetControlsEnabled(false);
            txtProjectCode.Enabled = true;
            btnSave.Enabled = false;
            ShowMessage(".این پروژه درخواست تایید نشده دارد یا خطای در بازیابی اطلاعات آن ایجاد شده");
            return;
        }
        else
        {
            SetControlsEnabled(true);
            btnSave.Enabled = true;
        }
        if (!CheckInsertConditions(ProjectId))
        {
            ClearForm();
            SetControlsEnabled(false);
            txtProjectCode.Enabled = true;
            btnSave.Enabled = false;
        }
        else
        {
            SetControlsEnabled(true);
            txtProjectCode.Enabled = false;
        }

    }
    public void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;

            case "Edit":
                Update();
                break;
        }
    }
    public void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CapacityRelease.aspx");
    }
    protected void flpAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveFile(sender, e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    #region Set Key-Mode
    public void SetKeys()
    {
        _ProjectId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PRj"].ToString()));
        _CapRId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["CapRId"].ToString()));
        _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
        SetMode();
    }
    private void SetMode()
    {
        switch (_PageMode)
        {
            case "New":
                SetNewModeKeys();
                break;
            case "View":
                SetViewModeKeys();
                break;
            case "Edit":
                SetEditModeKeys();
                break;
        }
    }
    private void SetNewModeKeys()
    {
        btnSave.Enabled = true;
        SetControlsEnabled(true);

        txtProjectCode.Enabled = true;
        ClearForm();
        RoundPanelProjectSearch.Visible = true;
        RoundPanelPage.HeaderText = "جدید";
    }
    private void SetViewModeKeys()
    {
        btnSave.Enabled = false;
        SetControlsEnabled(false);
        RoundPanelProjectSearch.Visible = false;
        txtProjectCode.Enabled = false;
        RoundPanelPage.Enabled = false;
        FillprojectInfo();
        FillForm();
        RoundPanelPage.HeaderText = "مشاهده";
        InsertWorkFlowStateView();
    }
    private void SetEditModeKeys()
    {
        btnSave.Enabled = true;
        SetControlsEnabled(true);
        RoundPanelProjectSearch.Visible = false;
        txtProjectCode.Enabled = false;
        FillprojectInfo();
        FillForm();
        RoundPanelPage.HeaderText = "ویرایش";
    }
    #endregion
    #region Set Visible & Enabled
    private void SetControlsEnabled(Boolean Enabled)
    {
        txtProjectCode.Enabled =
        cmbReasonType.Enabled =
        txtLetterCode.Enabled =
        txtLetterDateMun.Enabled =
        flpLetter.Enabled =
        txtDes.Enabled = Enabled;
    }
    private void SetVisiblelityControl(TSP.DataManager.TechnicalServices.CapacityReleaseManager.ReleaseType ReleaseType)
    {
        if (ReleaseType == TSP.DataManager.TechnicalServices.CapacityReleaseManager.ReleaseType.Nothing)
        {
            lblLetterCode.Text = "";
            lblLetterDateMun.Text = "";

            lblTrFileUplodeLetter.ClientVisible = false;
        }

        if (ReleaseType == TSP.DataManager.TechnicalServices.CapacityReleaseManager.ReleaseType.End)
        {
            lblLetterCode.Text = "شماره گزارش پایانی صادره از شهرداری";
            lblLetterDateMun.Text = "تاریخ گزارش پایانی صادره از شهرداری*";
        }

        if (ReleaseType == TSP.DataManager.TechnicalServices.CapacityReleaseManager.ReleaseType.Closed)
        {
            lblLetterCode.Text = "شماره نامه تعطیلی کارگاه وارده به نظام*";
            lblLetterDateMun.Text = "تاریخ نامه تعطیلی کارگاه وارده به نظام*";
        }

    }
    #endregion
    #region Fill Form
    private void FillForm()
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = new DataTable();
        dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSCapacityRelease, _CapRId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            _CurrentCapRTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            _CapRTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
        }
        TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager = new TSP.DataManager.TechnicalServices.CapacityReleaseManager();
        CapacityReleaseManager.SelectCapacityRelease(_CapRId);
        if (CapacityReleaseManager.Count != 1)
        {
            ShowMessage("خطای در بازیابی اطلاعات پیش آمده است");
            return;
        }
        try
        {
            int type = 0;
            if (!Utility.IsDBNullOrNullValue(CapacityReleaseManager[0]["Type"]))
            {
                cmbReasonType.SelectedIndex = cmbReasonType.Items.FindByValue(CapacityReleaseManager[0]["Type"].ToString()).Index;
                type = Convert.ToInt32(CapacityReleaseManager[0]["Type"].ToString());
            }
            else
                cmbReasonType.SelectedIndex = 0;

            SetVisiblelityControl((TSP.DataManager.TechnicalServices.CapacityReleaseManager.ReleaseType)type);

            if (!Utility.IsDBNullOrNullValue(CapacityReleaseManager[0]["LetterCode"]))
                txtLetterCode.Text = CapacityReleaseManager[0]["LetterCode"].ToString();
            else
                txtLetterCode.Text = "";
            if (!Utility.IsDBNullOrNullValue(CapacityReleaseManager[0]["LetterDate"]))
                txtLetterDateMun.Text = CapacityReleaseManager[0]["LetterDate"].ToString();
            else
                txtLetterDateMun.Text = "";
            if (!Utility.IsDBNullOrNullValue(CapacityReleaseManager[0]["Description"]))
                txtDes.Text = CapacityReleaseManager[0]["Description"].ToString();
            else
                txtDes.Text = "";

            if (!Utility.IsDBNullOrNullValue(CapacityReleaseManager[0]["UrlLetter"]))
            {
                HiddenFieldPage["Letter"] = 1;
                hpFilePathLetter.ClientVisible = true;
                hpFilePathLetter.NavigateUrl = CapacityReleaseManager[0]["UrlLetter"].ToString();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }
    private bool FillprojectInfo()
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        DataTable dtRequestIdLastVersion = ProjectRequestManager.SelectRequestIdLastVersion(_ProjectId, 1);
        if (dtRequestIdLastVersion.Rows.Count == 0 || Utility.IsDBNullOrNullValue(dtRequestIdLastVersion.Rows[0]["PrjReId"]))
            return false;
        else
            prjInfo.Fill(Convert.ToInt32(dtRequestIdLastVersion.Rows[0]["PrjReId"]));
        return true;
    }
    #endregion  
    bool CheckInsertConditions(int ProjectId)
    {
        TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager = new TSP.DataManager.TechnicalServices.CapacityReleaseManager();
        CapacityReleaseManager.SelectMemberCapacityReleaseByConfirm(Utility.GetCurrentUser_MeId(), ProjectId,0);
        if (CapacityReleaseManager.Count > 0)
        {
            ShowMessage("شما پیش تر درخواست آزاد سازی ظرفیت نظارت برای این پروژه را ثبت کردیده اید.");
            return false;
        }
        //***عضو دارای ظرفیت آزاد نشده نباشد
        TSP.DataManager.TechnicalServices.ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.ObserversManager();
        DataTable dtObserverCapacityDecrementActive = ObserversManager.SelectObserverCapacityDecrementActiveByProjectId(ProjectId, Utility.GetCurrentUser_MeId(), 1, Utility.GetDateOfToday(), 0);
        if (dtObserverCapacityDecrementActive.Rows.Count == 0)
        {
            ShowMessage("شما در این پروژه دارای ظرفیت کسر شده نیستید");
            return false;
        }
        //********پروژه تایید نشده باشد
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        DataTable dtProjectRequest = ProjectRequestManager.SelectProjectRequestCount(ProjectId, 0);
        if (Convert.ToInt32(dtProjectRequest.Rows[0]["RequestCount"]) != 0)
        {
            ShowMessage("پروژه انتخاب  شده دارای درخواست در جریان می باشد. پس از تایید درخواست پروژه قادر به ثبت درخواست بازگشت ظرفیت نظارت می باشید");
            return false;
        }

        return true;
    }
    bool CheckEditConditions(int CapRId, int TaskCode)
    {
        TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager = new TSP.DataManager.TechnicalServices.CapacityReleaseManager();
        CapacityReleaseManager.SelectCapacityRelease(CapRId);
        if (CapacityReleaseManager.Count != 1)
            return false;
        if (_CapRTaskId != Convert.ToInt32(CapacityReleaseManager[0]["WFCurrentTaskId"]))
            return false;

        TSP.DataManager.TechnicalServices.ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.ObserversManager();
        DataTable dtObserverCapacityDecrementActive = ObserversManager.SelectObserverCapacityDecrementActiveByProjectId(_ProjectId, Utility.GetCurrentUser_MeId(), 1, CapacityReleaseManager[0]["CreateDate"].ToString(), 0);
        if (dtObserverCapacityDecrementActive.Rows.Count == 0)
            return false;

        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        DataTable dtProjectRequest = ProjectRequestManager.SelectProjectRequestCount(_ProjectId, 0);
        if (Convert.ToInt32(dtProjectRequest.Rows[0]["RequestCount"]) != 0)
            return false;

        return true;
    }
    private string SaveFile(object sender, UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId" + Utility.GetCurrentUser_MeId().ToString() + "-" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Release/") + ret) == true);

            ASPxUploadControl UploadControl = (ASPxUploadControl)sender;
            string FullFilePath = "";
            FullFilePath = "~/Image/TechnicalServices/Release/" + ret;
            HiddenFieldPage["LetterUrl"] = FullFilePath;
            uploadedFile.SaveAs(MapPath(FullFilePath), true);

        }
        return ret;
    }
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (cmbReasonType.SelectedIndex == 0)
        {
            ShowMessage("دلیل بازگشت ظرفیت باید مشخص باشد");
            return;
        }

        if (Convert.ToInt32(HiddenFieldPage["Letter"]) == 0 || Utility.IsDBNullOrNullValue(HiddenFieldPage["LetterUrl"]))
        {
            ShowMessage("بارگذاری تصویر الزامی است");
            return;
        }

        if (!CheckInsertConditions(_ProjectId))
        {
            return;
        }

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager = new TSP.DataManager.TechnicalServices.CapacityReleaseManager();

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(WorkFlowTaskManager);
        TransactionManager.Add(CapacityReleaseManager);

        try
        {
            _CapRTaskId = -2;
            TransactionManager.BeginSave();
            DataRow drCapRelease = CapacityReleaseManager.NewRow();

            drCapRelease["ProjectId"] = _ProjectId;
            drCapRelease["MeId"] = Utility.GetCurrentUser_MeId();
            drCapRelease["MeIdType"] = Convert.ToInt16(TSP.DataManager.TSCapacityReleaseMeIdType.Member);
            drCapRelease["CreateDate"] = Utility.GetDateOfToday();

            drCapRelease["WFCurrentTaskId"] = _CapRTaskId;
            drCapRelease["IsConfirm"] = "0";

            drCapRelease["Type"] = Convert.ToInt16(cmbReasonType.SelectedItem.Value.ToString());

            if (Utility.IsDBNullOrNullValue(txtLetterDateMun.Text))
                drCapRelease["LetterDate"] = "";
            else
                drCapRelease["LetterDate"] = txtLetterDateMun.Text;
            if (Utility.IsDBNullOrNullValue(txtLetterCode.Text))
                drCapRelease["LetterCode"] = "";
            else
                drCapRelease["LetterCode"] = txtLetterCode.Text;

            drCapRelease["UrlLetter"] = HiddenFieldPage["LetterUrl"].ToString();

            if (Utility.IsDBNullOrNullValue(txtDes.Text))
                drCapRelease["Description"] = "";
            else
                drCapRelease["Description"] = txtDes.Text;

            drCapRelease["UserId"] = Utility.GetCurrentUser_UserId();
            drCapRelease["ModifiedDate"] = DateTime.Now;

            CapacityReleaseManager.AddRow(drCapRelease);
            if (CapacityReleaseManager.Save() != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CapacityReleaseManager.DataTable.AcceptChanges();
            _CapRId = Convert.ToInt32(CapacityReleaseManager[0]["CapRId"]);
            if (!InsertWF(WorkFlowStateManager, WorkFlowTaskManager))
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (_CapRTaskId == -2)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CapacityReleaseManager[0].BeginEdit();
            CapacityReleaseManager[0]["WFCurrentTaskId"] = _CapRTaskId;
            CapacityReleaseManager[0].EndEdit();
            if (CapacityReleaseManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            TransactionManager.EndSave();

            ShowMessage("ذخیره انجام شد");
            _PageMode = "Edit";
            SetEditModeKeys();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره اطلاعات رخ داده است");
        }

    }
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (!CheckEditConditions(_CapRId, _CurrentCapRTaskCode))
        {
            ShowMessage("قادر به ویرایش این درخواست بازگشت ظرفیت نمی باشید");
            return;
        }
        if (cmbReasonType.SelectedIndex == 0)
        {
            ShowMessage("دلیل بازگشت ظرفیت باید مشخص باشد");
            return;
        }
        if (Convert.ToInt32(HiddenFieldPage["Letter"]) == 0 || Utility.IsDBNullOrNullValue(HiddenFieldPage["LetterUrl"]))
        {
            ShowMessage("بارگذاری تصویر نامه الزامی است");
            return;
        }
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager = new TSP.DataManager.TechnicalServices.CapacityReleaseManager();

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(WorkFlowTaskManager);
        TransactionManager.Add(CapacityReleaseManager);
        try
        {
            TransactionManager.BeginSave();
            CapacityReleaseManager.SelectCapacityRelease(_CapRId);
            if (CapacityReleaseManager.Count != 1)
            {
                TransactionManager.EndSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CapacityReleaseManager[0].BeginEdit();

            CapacityReleaseManager[0]["Type"] = Convert.ToInt16(cmbReasonType.SelectedItem.Value.ToString());

            if (Utility.IsDBNullOrNullValue(txtLetterDateMun.Text))
                CapacityReleaseManager[0]["LetterDate"] = "";
            else
                CapacityReleaseManager[0]["LetterDate"] = txtLetterDateMun.Text;
            if (Utility.IsDBNullOrNullValue(txtLetterCode.Text))
                CapacityReleaseManager[0]["LetterCode"] = "";
            else
                CapacityReleaseManager[0]["LetterCode"] = txtLetterCode.Text;


            CapacityReleaseManager[0]["UrlLetter"] = HiddenFieldPage["LetterUrl"].ToString();

            if (Utility.IsDBNullOrNullValue(txtDes.Text))
                CapacityReleaseManager[0]["Description"] = "";
            else
                CapacityReleaseManager[0]["Description"] = txtDes.Text;

            CapacityReleaseManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            CapacityReleaseManager[0]["ModifiedDate"] = DateTime.Now;

            CapacityReleaseManager[0].EndEdit();

            if (CapacityReleaseManager.Save() != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CapacityReleaseManager.DataTable.AcceptChanges();

            if (!InsertWF(WorkFlowStateManager, WorkFlowTaskManager))
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (_CapRTaskId == -2)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CapacityReleaseManager[0].BeginEdit();
            CapacityReleaseManager[0]["WFCurrentTaskId"] = _CapRTaskId;
            CapacityReleaseManager[0].EndEdit();
            if (CapacityReleaseManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            TransactionManager.EndSave();

            ShowMessage("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره اطلاعات رخ داده است");
        }
    }
    #region WF
    private void CheckWorkFlowPermissionForSave()
    {
        Boolean per = false;
        switch (_PageMode)
        {
            case "New":
                per = true;
                break;
            case "Edit":
                if (_CurrentCapRTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveCapacityReleaseRequestInfo)
                    per = true;
                else
                    per = false;
                break;
            case "View":
                per = false;
                break;
        }
        btnSave.Enabled = per;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }
    private bool InsertWF(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
    {
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveCapacityReleaseRequestInfo);
        if (WorkFlowTaskManager.Count != 1)
            return false;
        int NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        int ReasonType = Convert.ToInt16(cmbReasonType.SelectedItem.Value.ToString());
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSCapacityRelease);
        int TableId = _CapRId;
        int CurrentNmcId = Utility.GetCurrentUser_MeId();

        if (_PageMode == "New")
        {
            int StateId1 = WorkFlowStateManager.StartWorkFlow(TableId, (int)TSP.DataManager.WorkFlowTask.SaveCapacityReleaseRequestInfo, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId);
            if (StateId1 <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }
            WorkFlowStateManager.DataTable.AcceptChanges();
        }
        int StateId2 = 0;
        if (Utility.GetCurrentAgentCode() == Utility.GetCurrentUser_AgentId() && (int)TSP.DataManager.TSReasonCapacityRelease.WorkshopClosure == ReasonType)
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ControlEmployeeConfirmingCapacityReleaseRequest);
            if (WorkFlowTaskManager.Count != 1)
                return false;
            _CapRTaskId = NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            StateId2 = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, _CapRTaskId, "ارسال اتوماتیک درخواست بازگشت ظرفیت به کارشناس واحد کنترل و ارزیابی توسط عضو", CurrentNmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), "", Utility.GetDateOfToday());
        }
        if (Utility.GetCurrentAgentCode() == Utility.GetCurrentUser_AgentId() && (int)TSP.DataManager.TSReasonCapacityRelease.Finish == ReasonType)
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.TSUnitEmployeeConfirminCapacityReleaseRequest);
            if (WorkFlowTaskManager.Count != 1)
                return false;
            _CapRTaskId = NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            StateId2 = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, _CapRTaskId, "ارسال اتوماتیک درخواست بازگشت ظرفیت به کارشناس خدمات مهندسی توسط عضو", CurrentNmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), "", Utility.GetDateOfToday());
        }
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId() && (int)TSP.DataManager.TSReasonCapacityRelease.WorkshopClosure == ReasonType)
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ControlAgentEmployeeConfirminCapacityReleaseRequest);
            if (WorkFlowTaskManager.Count != 1)
                return false;
            _CapRTaskId = NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            StateId2 = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, _CapRTaskId, "ارسال اتوماتیک درخواست بازگشت ظرفیت به کارشناس کنترل مضاعف دفتر نمایندگی توسط عضو", CurrentNmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), "", Utility.GetDateOfToday());
        }
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId() && (int)TSP.DataManager.TSReasonCapacityRelease.Finish == ReasonType)
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.AgentManagementConfirminCapacityReleaseRequest);
            if (WorkFlowTaskManager.Count != 1)
                return false;
            _CapRTaskId = NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            StateId2 = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, _CapRTaskId, "ارسال اتوماتیک درخواست بازگشت ظرفیت به رئیس دفتر نمایندگی توسط عضو", CurrentNmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), "", Utility.GetDateOfToday());
        }
        if (StateId2 <= 0)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        return true;
    }
    private void InsertWorkFlowStateView()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSCapacityRelease);
        int TableId = _CapRId;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده اطلاعات توسط عضو", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد.");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
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



    private void ClearForm()
    {
        txtProjectCode.Text =
        txtLetterCode.Text =
        txtLetterDateMun.Text = "";
        cmbReasonType.SelectedIndex = 0;

        imgEndUploadLetter.ClientVisible = false;
        HiddenFieldPage["Letter"] = 0;
        lblValidationLetter.ClientVisible = false;
        hpFilePathLetter.ClientVisible = false;
        hpFilePathLetter.NavigateUrl = "";
        txtDes.Text = "";
    }
    private void ShowMessage(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    #endregion
}