using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_TechnicalServices_Project_ImplementerOfficeInsert : System.Web.UI.Page
{
    #region Properties
    private int _ImpOfficeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ImpOfficeId"]);
        }
        set
        {
            HiddenFieldPage["ImpOfficeId"] = value.ToString();
        }
    }
    private int _ImOfficeReqId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ImOfficeReqId"]);
        }
        set
        {
            HiddenFieldPage["ImOfficeReqId"] = value.ToString();
        }
    }
    private string _PageMode
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
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Visible = BtnNew2.Visible = per.CanNew;
            btnEdit.Visible = btnEdit2.Visible = per.CanEdit;
            btnSave.Enabled = btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Visible;
            this.ViewState["BtnNew"] = BtnNew.Visible;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Visible = this.btnEdit2.Visible = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Visible = this.BtnNew2.Visible = (bool)this.ViewState["BtnNew"];
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!CheckPermitionForEdit(_ImOfficeReqId))
        {
            ShowMessage("امکان ویرایش اطلاعات در این مرحله از گرش کار وجود ندارد.");
            return;
        }
        _PageMode = "Edit";
        SetEditModeKeys();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        _ImOfficeReqId = -1;
        _ImpOfficeId = -1;
        _PageMode = "New";
        SetNewModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Update();
                break;
            case "Change":
                InsertNewRequest(TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.Change);
                break;
            case "InActive":
                InsertNewRequest(TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.InActive);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImplementerOffice.aspx");
    }
    protected void flpOfDocumentFront_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl f = (ASPxUploadControl)sender;
        e.CallbackData = SaveImage(e.UploadedFile, f.ID);
    }

    protected void flpOfDocumentBack_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl f = (ASPxUploadControl)sender;
        e.CallbackData = SaveImage(e.UploadedFile, f.ID);
    }
    protected void MainMenu_ItemClick(object source, MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "OfficeMember":
                Response.Redirect("ImplementerOfficeMember.aspx?ReqId=" + Utility.EncryptQS(_ImOfficeReqId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&ImpOfficeId=" + Utility.EncryptQS(_ImpOfficeId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"], false);
                break;
            case "OfficeInfo":
                Response.Redirect("ImplementerOfficeInsert.aspx?PgMd=" + Utility.EncryptQS(_PageMode) + "&ReqId=" + Utility.EncryptQS(_ImOfficeReqId.ToString()) + "&ImpOfficeId=" + Utility.EncryptQS(_ImpOfficeId.ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]), false);
                break;
        }
    }
    #endregion
    #region Methods
    private void ShowMessage(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;

    }
    #region SetKey-Method
    private void SetKeys()
    {
        try
        {


            if (string.IsNullOrEmpty(Request.QueryString["ImpOfficeId"]) || string.IsNullOrEmpty(Request.QueryString["ReqId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
            {
                ShowMessage("خطا در ارسال اطلاعات ایجاد شده است");
                return;
            }
            _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PgMd"].ToString()));
            _ImpOfficeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ImpOfficeId"]));
            _ImOfficeReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ReqId"]));

            SetMode(_PageMode);
            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطا در ارسال اطلاعات ایجاد شده است");
            return;
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "New":
                SetNewModeKeys();
                break;
            case "Edit":
                SetEditModeKeys();
                break;
            case "View":
                SetViewModeKeys();
                break;
        }
    }
    private void SetNewModeKeys()
    {
        MainMenu.Enabled = false;
        CheckAccess();
        SetEnable(true);
        ClearForm();
        _PageMode = "New";
        RoundPanelPage.HeaderText = "جدید";
    }
    private void SetEditModeKeys()
    {
        MainMenu.Enabled = true;
        FillForm();
        CheckAccess();
        SetEnable(true);
        _PageMode = "Edit";
        RoundPanelPage.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        MainMenu.Enabled = true;
        FillForm();
        CheckAccess();
        SetEnable(false);
        _PageMode = "View";
        RoundPanelPage.HeaderText = "مشاهده";
    }
    #endregion
    #region FillForm
    private void FillForm()
    {
        TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();

        try
        {
            ImplementerOfficeRequest.NewRow();
            ImplementerOfficeRequest.SelectById(_ImOfficeReqId);
            if (ImplementerOfficeRequest.Count <= 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است");
                return;
            }
            txtImpOfficeId.Text = ImplementerOfficeRequest[0]["ImpOfficeId"].ToString();
            ComboOfficeType.DataBind();
            ComboOfficeType.SelectedIndex = ComboOfficeType.Items.FindByValue(ImplementerOfficeRequest[0]["OfficeType"].ToString()).Index;
            txtOfName.Text = ImplementerOfficeRequest[0]["Name"].ToString();
            txtOfAddress.Text = ImplementerOfficeRequest[0]["Address"].ToString();
            txtOfRegNo.Text = ImplementerOfficeRequest[0]["RegisterNo"].ToString();
            txtOfRegDate.Text = ImplementerOfficeRequest[0]["RegisterDate"].ToString();
            txtMeNo.Text = ImplementerOfficeRequest[0]["MeNo"].ToString();
            txtFileNo.Text = ImplementerOfficeRequest[0]["FileNo"].ToString();
            CombProvince.DataBind();
            CombProvince.SelectedIndex = CombProvince.Items.FindByValue(ImplementerOfficeRequest[0]["PrId"].ToString()).Index;
            txtDocFirstDate.Text = ImplementerOfficeRequest[0]["DocFirstDate"].ToString();
            txtDocDate.Text = ImplementerOfficeRequest[0]["DocDate"].ToString();
            txtDocExpireDate.Text = ImplementerOfficeRequest[0]["DocExpireDate"].ToString();
            CoboGrade.DataBind();
            CoboGrade.SelectedIndex = CoboGrade.Items.FindByValue(ImplementerOfficeRequest[0]["ImpGradeId"].ToString()).Index;
            if (!Utility.IsDBNullOrNullValue(ImplementerOfficeRequest[0]["DocImgUrlFront"]))
            {
                imgDocumentFront.ImageUrl = ImplementerOfficeRequest[0]["DocImgUrlFront"].ToString();
                imgDocumentFront.ClientVisible = true;
            }
            if (!Utility.IsDBNullOrNullValue(ImplementerOfficeRequest[0]["DocImgUrlFront"]))
            {
                ImageDocumentBack.ImageUrl = ImplementerOfficeRequest[0]["DocImgUrlFront"].ToString();
                ImageDocumentBack.ClientVisible = true;
            }
            txtPostalCode.Text = ImplementerOfficeRequest[0]["PostalCode"].ToString();
            txtOfDesc.Text = ImplementerOfficeRequest[0]["Description"].ToString();
            lblWorkFlowState.Text = ImplementerOfficeRequest[0]["TaskName"].ToString();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }

    }
    #endregion
    #region SetControls
    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Visible = BtnNew2.Visible = per.CanNew;
        btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        if (_PageMode == "New" || _PageMode == "Edit")
            btnEdit.Enabled = btnEdit2.Enabled = false;
        else if (_PageMode == "View")
            btnSave.Enabled = btnSave2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    private void ClearForm()
    {
        Session["DocumentBack"] = Session["DocumentFront"] = null;
        txtImpOfficeId.Text = txtFileNo.Text = txtMeNo.Text =
        txtOfAddress.Text = txtOfDesc.Text = txtOfName.Text = txtOfRegDate.Text =
        txtOfRegNo.Text = ImageDocumentBack.ImageUrl = imgEndUploadDocumentFront.ImageUrl = "";

    }
    private void SetEnable(bool Enable)
    {
        txtImpOfficeId.Enabled = false;
        txtDocDate.Enabled = txtDocExpireDate.Enabled = txtDocFirstDate.Enabled = CombProvince.Enabled =
        txtFileNo.Enabled = txtMeNo.Enabled =
        txtOfAddress.Enabled = txtOfDesc.Enabled = txtOfName.Enabled = txtOfRegDate.Enabled =
        txtOfRegNo.Enabled = flpOfDocumentBack.ClientVisible = flpOfDocumentFront.ClientVisible = Enable;
        if (_PageMode == "New")
            ComboOfficeType.Enabled = true;
        else
            ComboOfficeType.Enabled = false;


    }
    #endregion
    #region Insert & Update
    private void Insert()
    {
        TSP.DataManager.TechnicalServices.ImplementerOfficeManager ImplementerOfficeManager = new TSP.DataManager.TechnicalServices.ImplementerOfficeManager();
        TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(ImplementerOfficeManager);
        TransactionManager.Add(ImplementerOfficeRequest);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice;
            int TaskId = -1;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            string TaskName = WorkFlowTaskManager[0]["TaskName"].ToString();


            #region Save ImpOffice
            TransactionManager.BeginSave();
            DataRow drImpOffice = ImplementerOfficeManager.NewRow();
            drImpOffice["WfCurrentTaskId"] = TaskId;
            drImpOffice["OfficeType"] = ComboOfficeType.Value;
            drImpOffice["Name"] = txtOfName.Text;
            drImpOffice["Address"] = txtOfAddress.Text;
            drImpOffice["RegisterNo"] = txtOfRegNo.Text;
            drImpOffice["RegisterDate"] = txtOfRegDate.Text;
            drImpOffice["MeNo"] = txtMeNo.Text;
            drImpOffice["FileNo"] = txtFileNo.Text;
            if (CombProvince.SelectedItem != null && CombProvince.SelectedItem.Value != null)
                drImpOffice["PrId"] = CombProvince.SelectedItem.Value;

            drImpOffice["DocFirstDate"] = txtDocFirstDate.Text;
            drImpOffice["DocDate"] = txtDocDate.Text;
            drImpOffice["DocExpireDate"] = txtDocExpireDate.Text;

            drImpOffice["ImpGradeId"] = CoboGrade.Value;
            drImpOffice["PostalCode"] = txtPostalCode.Text;
            drImpOffice["Description"] = txtOfDesc.Text;

            if (Session["DocumentFront"] != null)
                drImpOffice["DocImgUrlFront"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentFront"].ToString());
            if (Session["DocumentBack"] != null)
                drImpOffice["DocImgUrlBack"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentBack"].ToString());


            drImpOffice["Status"] = TSP.DataManager.TechnicalServices.ImplementerOfficeStatus.NotConfirm;
            drImpOffice["ModifiedDate"] = DateTime.Now;
            drImpOffice["CreateDate"] = Utility.GetDateOfToday();
            drImpOffice["UserId"] = Utility.GetCurrentUser_UserId();
            ImplementerOfficeManager.AddRow(drImpOffice);
            ImplementerOfficeManager.Save();
            ImplementerOfficeManager.DataTable.AcceptChanges();
            #endregion
            int ImpOfficeId = Convert.ToInt32(ImplementerOfficeManager[0]["ImpOfficeId"]);
            int ImOfficeReqId = InsertRequest(ImplementerOfficeRequest, ImpOfficeId, TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.New, TaskId);

            #region StartWorkFlow

            int CurrentNmcId = FindNmcId(TaskId);
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }

            int StateId = WorkFlowStateManager.StartWorkFlow(ImOfficeReqId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, "ثبت شرکت مجری جدید");
            if (StateId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            ImplementerOfficeManager[0].BeginEdit();
            ImplementerOfficeManager[0]["WfCurrentStatId"] = StateId;
            ImplementerOfficeManager[0].EndEdit();
            if (ImplementerOfficeManager.Save() == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            ImplementerOfficeRequest[0].BeginEdit();
            ImplementerOfficeRequest[0]["WfCurrentStatId"] = StateId;
            ImplementerOfficeRequest[0].EndEdit();
            if (ImplementerOfficeRequest.Save() == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion
            TransactionManager.EndSave();
            _ImOfficeReqId = ImOfficeReqId;
            _ImpOfficeId = ImpOfficeId;
            if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + TaskName;
            }
            SetEditModeKeys();
            ShowMessage("ذخیره انجام شد.");
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در ذخیره اطلاعات رخ داده است");
        }

    }
    private void Update()
    {
        TSP.DataManager.TechnicalServices.ImplementerOfficeManager ImplementerOfficeManager = new TSP.DataManager.TechnicalServices.ImplementerOfficeManager();
        TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ImplementerOfficeManager);
        TransactionManager.Add(ImplementerOfficeRequest);
        try
        {
            TransactionManager.BeginSave();
            ImplementerOfficeRequest.SelectById(_ImOfficeReqId);
            if (ImplementerOfficeRequest.Count <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
                return;
            }
            ImplementerOfficeRequest[0].BeginEdit();
            ImplementerOfficeRequest[0]["Name"] = txtOfName.Text;
            ImplementerOfficeRequest[0]["Address"] = txtOfAddress.Text;
            ImplementerOfficeRequest[0]["RegisterNo"] = txtOfRegNo.Text;
            ImplementerOfficeRequest[0]["RegisterDate"] = txtOfRegDate.Text;
            ImplementerOfficeRequest[0]["MeNo"] = txtMeNo.Text;
            ImplementerOfficeRequest[0]["FileNo"] = txtFileNo.Text;
            if (CombProvince.SelectedItem != null && CombProvince.SelectedItem.Value != null)
                ImplementerOfficeRequest[0]["PrId"] = CombProvince.SelectedItem.Value;

            ImplementerOfficeRequest[0]["DocFirstDate"] = txtDocFirstDate.Text;
            ImplementerOfficeRequest[0]["DocDate"] = txtDocDate.Text;
            ImplementerOfficeRequest[0]["DocExpireDate"] = txtDocExpireDate.Text;
            ImplementerOfficeRequest[0]["ImpGradeId"] = CoboGrade.Value;
            if (Session["DocumentFront"] != null)
                ImplementerOfficeRequest[0]["DocImgUrlFront"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentFront"].ToString());
            else
                ImplementerOfficeRequest[0]["DocImgUrlFront"] = "";
            if (Session["DocumentBack"] != null)
                ImplementerOfficeRequest[0]["DocImgUrlBack"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentBack"].ToString());
            else
                ImplementerOfficeRequest[0]["DocImgUrlBack"] = "";
            ImplementerOfficeRequest[0]["PostalCode"] = txtPostalCode.Text;
            ImplementerOfficeRequest[0]["Description"] = txtOfDesc.Text;
            ImplementerOfficeRequest[0]["ModifiedDate"] = DateTime.Now;
            ImplementerOfficeRequest[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ImplementerOfficeRequest[0].EndEdit();
            ImplementerOfficeRequest.Save();
            if (Convert.ToInt16(ImplementerOfficeRequest[0]["Type"]) == (int)TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.New)
            {
                #region Update ImplementerOfficeManager
                ImplementerOfficeManager.SelectById(_ImpOfficeId);
                if (ImplementerOfficeManager.Count <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
                    return;
                }
                ImplementerOfficeManager[0].BeginEdit();
                ImplementerOfficeManager[0]["Name"] = txtOfName.Text;
                ImplementerOfficeManager[0]["Address"] = txtOfAddress.Text;
                ImplementerOfficeManager[0]["RegisterNo"] = txtOfRegNo.Text;
                ImplementerOfficeManager[0]["RegisterDate"] = txtOfRegDate.Text;
                ImplementerOfficeManager[0]["MeNo"] = txtMeNo.Text;
                ImplementerOfficeManager[0]["FileNo"] = txtFileNo.Text;
                if (CombProvince.SelectedItem != null && CombProvince.SelectedItem.Value != null)
                    ImplementerOfficeManager[0]["PrId"] = CombProvince.SelectedItem.Value;

                ImplementerOfficeManager[0]["DocFirstDate"] = txtDocFirstDate.Text;
                ImplementerOfficeManager[0]["DocDate"] = txtDocDate.Text;
                ImplementerOfficeManager[0]["DocExpireDate"] = txtDocExpireDate.Text;
                ImplementerOfficeManager[0]["ImpGradeId"] = CoboGrade.Value;
                if (Session["DocumentFront"] != null)
                    ImplementerOfficeManager[0]["DocImgUrlFront"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentFront"].ToString());
                else
                    ImplementerOfficeManager[0]["DocImgUrlFront"] = "";
                if (Session["DocumentBack"] != null)
                    ImplementerOfficeManager[0]["DocImgUrlBack"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentBack"].ToString());
                else
                    ImplementerOfficeManager[0]["DocImgUrlBack"] = "";
                ImplementerOfficeManager[0]["PostalCode"] = txtPostalCode.Text;
                ImplementerOfficeManager[0]["Description"] = txtOfDesc.Text;
                ImplementerOfficeManager[0]["ModifiedDate"] = DateTime.Now;
                ImplementerOfficeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ImplementerOfficeManager[0].EndEdit();
                ImplementerOfficeManager.Save();
                #endregion
            }
            TransactionManager.EndSave();
            ShowMessage("ذخیره انجام شد");
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }
    }
    private int InsertRequest(TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest, int ImpOfficeId, TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType ImplementerOfficeRequestType, int TaskId)
    {
        DataRow drImpOfficeReq = ImplementerOfficeRequest.NewRow();
        drImpOfficeReq["WfCurrentTaskId"] = TaskId;
        drImpOfficeReq["ImpOfficeId"] = ImpOfficeId;
        drImpOfficeReq["OfficeType"] = ComboOfficeType.Value;
        drImpOfficeReq["Name"] = txtOfName.Text;
        drImpOfficeReq["Address"] = txtOfAddress.Text;
        drImpOfficeReq["RegisterNo"] = txtOfRegNo.Text;
        drImpOfficeReq["RegisterDate"] = txtOfRegDate.Text;
        drImpOfficeReq["MeNo"] = txtMeNo.Text;
        drImpOfficeReq["FileNo"] = txtFileNo.Text;
        if (CombProvince.SelectedItem != null && CombProvince.SelectedItem.Value != null)
            drImpOfficeReq["PrId"] = CombProvince.SelectedItem.Value;

        drImpOfficeReq["DocFirstDate"] = txtDocFirstDate.Text;
        drImpOfficeReq["DocDate"] = txtDocDate.Text;
        drImpOfficeReq["DocExpireDate"] = txtDocExpireDate.Text;
        drImpOfficeReq["ImpGradeId"] = CoboGrade.Value;

        drImpOfficeReq["Description"] = txtOfDesc.Text;
        if (Session["DocumentFront"] != null)
            drImpOfficeReq["DocImgUrlFront"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentFront"].ToString());
        if (Session["DocumentBack"] != null)
            drImpOfficeReq["DocImgUrlBack"] = "/Image/TechnicalServices/Implementer/ImpelementerOffice/" + System.IO.Path.GetFileName(Session["DocumentBack"].ToString());
        drImpOfficeReq["IsConfirm"] = 0;
        drImpOfficeReq["PostalCode"] = txtPostalCode.Text;
        drImpOfficeReq["Type"] = (int)ImplementerOfficeRequestType;
        drImpOfficeReq["ModifiedDate"] = DateTime.Now;
        drImpOfficeReq["CreateDate"] = Utility.GetDateOfToday();
        drImpOfficeReq["UserId"] = Utility.GetCurrentUser_UserId();

        ImplementerOfficeRequest.AddRow(drImpOfficeReq);
        ImplementerOfficeRequest.Save();
        ImplementerOfficeRequest.DataTable.AcceptChanges();
        return Convert.ToInt32(ImplementerOfficeRequest[0]["ImOfficeReqId"]);

    }
    private void InsertNewRequest(TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType ImplementerOfficeRequestType)
    {
        TSP.DataManager.TechnicalServices.ImplementerOfficeManager ImplementerOfficeManager = new TSP.DataManager.TechnicalServices.ImplementerOfficeManager();
        TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(ImplementerOfficeManager);
        TransactionManager.Add(ImplementerOfficeRequest);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice;
            int TaskId = -1;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            string TaskName = WorkFlowTaskManager[0]["TaskName"].ToString();

            int ImOfficeReqId = InsertRequest(ImplementerOfficeRequest, _ImpOfficeId, TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.New, TaskId);

            #region StartWorkFlow

            int CurrentNmcId = FindNmcId(TaskId);
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }

            int StateId = WorkFlowStateManager.StartWorkFlow(ImOfficeReqId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, "ثبت درخواست جدید");
            if (StateId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            ImplementerOfficeManager[0].BeginEdit();
            ImplementerOfficeManager[0]["WfCurrentStatId"] = StateId;
            ImplementerOfficeManager[0].EndEdit();
            if (ImplementerOfficeManager.Save() == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            ImplementerOfficeRequest[0].BeginEdit();
            ImplementerOfficeRequest[0]["WfCurrentStatId"] = StateId;
            ImplementerOfficeRequest[0].EndEdit();
            if (ImplementerOfficeRequest.Save() == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion
            TransactionManager.EndSave();
            _ImOfficeReqId = ImOfficeReqId;
            if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + TaskName;
            }
            SetEditModeKeys();
            ShowMessage("ذخیره انجام شد.");

        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
        }
    }
    #endregion
    #region WF
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave(_PageMode);
        if (_PageMode == "View" || _PageMode == "Edit")
        {
            CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSImplementerOffice);
        int Permission = Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
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
            this.LabelWarning.Text = "شما سطح دسترسی گردش کار جهت ثبت اطلاعات را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*******Editing Task Code
        int WFCode = (int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming;
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice;
        TSP.DataManager.WFPermission SaveWFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(SaveTaskCode, WFCode, _ImOfficeReqId, Utility.GetCurrentUser_UserId(), PageMode);
        this.ViewState["BtnNew"] = BtnNew.Visible = BtnNew2.Visible = SaveWFPer.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Visible = btnEdit2.Visible = SaveWFPer.BtnEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = SaveWFPer.BtnSave;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSImplementerOffice), TableId, (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
            return true;
        else
            return false;
    }
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
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }
    #endregion
    protected string SaveImage(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Implementer/ImpelementerOffice/") + ret) == true);
            string tempFileName = MapPath("~/Image/TechnicalServices/Implementer/ImpelementerOffice/") + ret;
            uploadedFile.SaveAs(tempFileName, true);

            if (id == "flpOfDocumentFront")
            {
                Session["DocumentFront"] = "~/Image/TechnicalServices/Implementer/ImpelementerOffice/" + ret;
            }
            else if (id == "flpOfDocumentBack")
            {
                Session["DocumentBack"] = "~/Image/Implementer/ImpelementerOffice/" + ret;
            }

        }
        return ret;
    }

    #endregion

}