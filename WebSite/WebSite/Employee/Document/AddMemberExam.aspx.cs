using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web;
using System.IO;

public partial class Employee_Document_AddMemberExam : System.Web.UI.Page
{
    #region Properties
    int _MeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldExam["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]));
            }
        }
        set
        {
            HiddenFieldExam["MeId"] = value;
        }
    }
    int _MfIdOrigin
    {
        get
        {
            return Convert.ToInt32(HiddenFieldExam["MfIdOrigin"]);
        }
        set
        {
            HiddenFieldExam["MfIdOrigin"] = value;
        }
    }
    int _MFId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldExam["MFId"]);
        }
        set
        {
            HiddenFieldExam["MFId"] = value;
        }
    }
    int _MExmDId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldExam["MExmDId"]);
        }
        set
        {
            HiddenFieldExam["MExmDId"] = value;
        }
    }
    int _TTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldExam["TTypeId"]);
        }
        set
        {
            HiddenFieldExam["TTypeId"] = value;
        }
    }
    string _PageMode
    {
        get
        {
            return HiddenFieldExam["PageMode"].ToString();
        }
        set
        {
            HiddenFieldExam["PageMode"] = value;
        }
    }
    string _PrePageMode
    {
        get
        {
            return HiddenFieldExam["PrePageMode"].ToString();
        }
        set
        {
            HiddenFieldExam["PrePageMode"] = value;
        }
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MFId"] == null || Request.QueryString["PrePgMd"] == null || Request.QueryString["MExmDId"] == null || Request.QueryString["PgMd"] == null)
        {
            ShowMessage("مقادیر صفحه نامعتبر می باشد.");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                HiddenFieldExam["PreTCondId"] = null;
                Session["ExamFileURL"] = null;
                Session["ExamFileURLConf"] = null;
                Session["ImplementPeriodFileURL"] = null;


                TSP.DataManager.Permission per = TSP.DataManager.DocMemberExamDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                BtnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = per.CanNew || per.CanEdit;
                btnSave2.Enabled = per.CanNew || per.CanEdit;

                SetKeys();

                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = BtnNew.Enabled;
                this.ViewState["BtnSave"] = btnSave.Enabled;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            }
        }

        if (!Utility.IsDBNullOrNullValue(_MeId))
        {
            MemberInfoUserControl1.MeId = _MeId;
            MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (!Utility.IsDBNullOrNullValue(Session["ExamFileURL"]))
            HyperLinkExamFileURL.ImageUrl = Session["ExamFileURL"].ToString();
        if (!Utility.IsDBNullOrNullValue(Session["ExamFileURLConf"]))
            HyperLinkExamConfirmImageURL.ImageUrl = Session["ExamFileURLConf"].ToString();
        if (!Utility.IsDBNullOrNullValue(Session["ImplementPeriodFileURL"]))
            HyperLinkPeriodImg.ImageUrl = Session["ImplementPeriodFileURL"].ToString();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        cmbMajor.ClientEnabled = true;
        ClearForm();
        EnableControls();
        _PageMode = "New";
        _MExmDId = -1;
        RoundPanelExam.HeaderText = "جدید";
        TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (_MFId != _MfIdOrigin)
        {
            ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
            return;
        }

        if (!CheckPermitionForEdit(_MFId))
        {
            ShowMessage("امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.");
            return;
        }

        SetEditModeKeys();
        CheckWorkFlowPermission();
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
              && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("MemberExam.aspx?PgMd=" + _PrePageMode + "&MFId=" + Utility.EncryptQS(_MFId.ToString())
                   + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("MemberExam.aspx?PgMd=" + _PrePageMode + "&MFId=" + Utility.EncryptQS(_MFId.ToString()));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Edit(_MExmDId);
                break;
        }
    }
    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string MjId = "";
        cmbMajor.DataBind();
        if (cmbMajor.SelectedItem != null)
        {
            MjId = cmbMajor.SelectedItem.Value.ToString();
        }
        ObjdsTestCondition.SelectParameters[1].DefaultValue = MjId;

        ObjectDataSourceTestType.SelectParameters["TCondId"].DefaultValue = "-2";
        cmbTestType.DataBind();
        ComboTestCondition.SelectedIndex = -1;
    }
    protected void ComboTestCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboTestCondition.SelectedItem == null || ComboTestCondition.SelectedItem.Value == null)
            return;
        int TCondId = Convert.ToInt32(ComboTestCondition.SelectedItem.Value);
        ObjectDataSourceTestType.SelectParameters["TCondId"].DefaultValue = TCondId.ToString();
        cmbTestType.DataBind();
    }
    protected void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        _TTypeId = -2;
        if (cmbTestType.SelectedItem != null && cmbTestType.SelectedItem.Value != null)
        {
            int TCondDId = Convert.ToInt32(cmbTestType.SelectedItem.Value);
            TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
            DocTestConditionDetailManager.FindByCode(TCondDId);
            if (DocTestConditionDetailManager.Count == 1)
            {
                lblPeriod.ClientVisible = PanelPeriodImg.ClientVisible = Convert.ToBoolean(DocTestConditionDetailManager[0]["NeedFileUpload"]);
                _TTypeId = Convert.ToInt32(DocTestConditionDetailManager[0]["TTypeId"]);
            }


        }
        else
        {
            PanelPeriodImg.ClientVisible = false;
        }
    }
    protected void UploadControlPeriodImgURL_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageAttach(e.UploadedFile, "ImageImplementPeriod");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void flpAttachImgExam_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageAttach(e.UploadedFile, "imgExam");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void flpAttachImgConf_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageAttach(e.UploadedFile, "imgConf");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        _MExmDId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MExmDId"].ToString()));
        _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
        _PrePageMode = Request.QueryString["PrePgMd"];
        _MFId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"]));
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(_MFId, 0);
        if (DocMemberFileManager.Count == 1)
        {
            _MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        ObjdsMemberLicence.SelectParameters[0].DefaultValue = _MeId.ToString();
        SetMode(_PageMode);
        CheckWorkFlowPermission();
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
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberExamManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled = btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
        txtEntrantCode.Enabled =
        txtPoint.Enabled =
        txtUserCode.Enabled =
        cmbGrade.Enabled =
        cmbTestType.Enabled =
        cmbMajor.ClientEnabled =
        ComboTestCondition.Enabled = false;
        HyperLinkExamConfirmImageURL.ClientEnabled = true;
        FillForm(_MExmDId);
        RoundPanelExam.HeaderText = "مشاهده";
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        btnEdit.Enabled = btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        EnableControls();
        ClearForm();
        RoundPanelExam.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberExamManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        EnableControls();
        cmbMajor.ClientEnabled =
        ComboTestCondition.Enabled = false;
        FillForm(_MExmDId);
        _PageMode = "Edit";
        RoundPanelExam.HeaderText = "ویرایش";
    }

    private void FillForm(int MExmDId)
    {
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        DocMemberExamDetailManager.FindByCode(MExmDId);
        if (DocMemberExamDetailManager.Count == 1)
        {
            _MfIdOrigin = Convert.ToInt32(DocMemberExamDetailManager[0]["MfId"]);
            cmbMajor.DataBind();
            cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(DocMemberExamDetailManager[0]["MjId"].ToString()).Index;
            cmbMajor_SelectedIndexChanged(this, new EventArgs());
            int TCondId = Convert.ToInt32(DocMemberExamDetailManager[0]["TCondId"]);
            ObjectDataSourceTestType.SelectParameters["TCondId"].DefaultValue = TCondId.ToString();
            cmbTestType.DataBind();
            ComboTestCondition.DataBind();
            ComboTestCondition.SelectedIndex = ComboTestCondition.Items.FindByValue(DocMemberExamDetailManager[0]["TCondId"].ToString()).Index;
            _TTypeId = Convert.ToInt32(DocMemberExamDetailManager[0]["TTypeId"]);

            if (!Utility.IsDBNullOrNullValue(DocMemberExamDetailManager[0]["GrdId"]))
            {
                cmbGrade.DataBind();
                cmbGrade.SelectedIndex = cmbGrade.Items.FindByValue(DocMemberExamDetailManager[0]["GrdId"].ToString()).Index;
                TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
                DataTable dtTestConditionDetail = DocTestConditionDetailManager.SelectByTestCondition(TCondId, _TTypeId, Convert.ToInt32(DocMemberExamDetailManager[0]["GrdId"]));
                if (dtTestConditionDetail.Rows.Count > 0)
                {
                    cmbTestType.SelectedIndex = cmbTestType.Items.FindByValue(dtTestConditionDetail.Rows[0]["TCondDId"].ToString()).Index;
                    lblPeriod.ClientVisible = PanelPeriodImg.ClientVisible = Convert.ToBoolean(dtTestConditionDetail.Rows[0]["NeedFileUpload"]);          
                }
                else
                    cmbTestType.SelectedIndex = -1;
            }
            txtPoint.Text = DocMemberExamDetailManager[0]["Point"].ToString();
            txtUserCode.Text = DocMemberExamDetailManager[0]["UserCode"].ToString();
            txtEntrantCode.Text = DocMemberExamDetailManager[0]["EntranceCode"].ToString();
            HyperLinkPeriodImg.ImageUrl = DocMemberExamDetailManager[0]["PeriodImgURL"].ToString();
            HyperLinkExamConfirmImageURL.ImageUrl = DocMemberExamDetailManager[0]["ExamConfirmImageURL"].ToString();
            HyperLinkExamFileURL.ImageUrl = DocMemberExamDetailManager[0]["FileURL"].ToString();            
        }
    }

    private void ClearForm()
    {
        txtEntrantCode.Text = "";
        txtPoint.Text = "";
        txtUserCode.Text = "";
        cmbMajor.SelectedIndex = 0;
        cmbMajor_SelectedIndexChanged(this, new EventArgs());
        cmbGrade.SelectedIndex = 0;
        cmbTestType.SelectedIndex =
        ComboTestCondition.SelectedIndex = -1;
        Session["ExamFileURL"] = null;
        Session["ExamFileURLConf"] = null;
        Session["ImplementPeriodFileURL"] = null;
        HyperLinkPeriodImg.ImageUrl =
        HyperLinkExamConfirmImageURL.ImageUrl =
        HyperLinkExamFileURL.ImageUrl = "";
        txtUserCode.Text = "";
        txtEntrantCode.Text = "";
    }

    private void EnableControls()
    {
        txtEntrantCode.Enabled =
        txtPoint.Enabled =
        txtUserCode.Enabled =
        cmbGrade.Enabled =
        cmbTestType.Enabled =
        cmbMajor.ClientEnabled =
        flpAttach.ClientVisible =
        flpAttachConf.ClientVisible =
        UploadControlPeriodImgURL.ClientVisible =
        RoundPanelMeExam.Enabled = ComboTestCondition.Enabled = true;
    }

    private void Insert()
    {
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            if (ComboTestCondition.SelectedItem == null || ComboTestCondition.SelectedItem.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "آزمون را انتخاب نمایید.";
                return;
            }
            DataTable dtMeExam = DocMemberExamDetailManager.SelectByMFId(_MFId, _MeId);
            if (dtMeExam.Rows.Count > 0)
            {
                DataRow[] dr = dtMeExam.Select("TCondId=" + ComboTestCondition.SelectedItem.Value.ToString() + "and InactiveValue=0" + "and TTypeId=" + _TTypeId.ToString() + " and GrdId=" + cmbGrade.SelectedItem.Value.ToString());
                if (dr.Length > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError);
                    return;
                }
            }
            DataRow MemberExamRow = DocMemberExamDetailManager.NewRow();
            MemberExamRow["MFId"] = _MFId;
            MemberExamRow["TCondId"] = ComboTestCondition.SelectedItem.Value;
            if (!Utility.IsDBNullOrNullValue(Session["ExamFileURLConf"]))
                MemberExamRow["ExamConfirmImageURL"] = Session["ExamFileURLConf"].ToString();
            MemberExamRow["TTypeId"] = _TTypeId;
            MemberExamRow["GrdId"] = cmbGrade.SelectedItem.Value;
            MemberExamRow["Point"] = txtPoint.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ExamFileURL"]))
                MemberExamRow["FileURL"] = Session["ExamFileURL"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["ImplementPeriodFileURL"]))
                MemberExamRow["PeriodImgURL"] = Session["ImplementPeriodFileURL"].ToString();
            MemberExamRow["UserCode"] = txtUserCode.Text;
            MemberExamRow["EntranceCode"] = txtEntrantCode.Text;
            MemberExamRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberExamRow["ModifiedDate"] = DateTime.Now;
            DocMemberExamDetailManager.AddRow(MemberExamRow);

            if (DocMemberExamDetailManager.Save() <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.DocMemberExamManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            cmbMajor.ClientEnabled = false;
            _PageMode = "Edit";
            _MExmDId = Convert.ToInt32(DocMemberExamDetailManager[0]["MExmDId"]);
            RoundPanelExam.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);

            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming, _MFId, (int)TSP.DataManager.TableType.DocMemberExam, "ثبت آزمون جدید", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int MExmDId)
    {
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        try
        {

            if (ComboTestCondition.SelectedItem == null || ComboTestCondition.SelectedItem.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "آزمون را انتخاب نمایید.";
                return;
            }
            DataTable dtMeExam = DocMemberExamDetailManager.SelectByMFId(-1, _MeId);
            if (dtMeExam.Rows.Count > 0)
            {
                DataRow[] dr = dtMeExam.Select("MExmDId <>" + MExmDId.ToString() + "and TCondId=" + ComboTestCondition.SelectedItem.Value.ToString() + "and InactiveValue=0" + "and TTypeId=" + _TTypeId.ToString() + " and GrdId=" + cmbGrade.SelectedItem.Value.ToString());
                if (dr.Length > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError);
                    return;
                }
            }

            DocMemberExamDetailManager.FindByCode(MExmDId);
            if (DocMemberExamDetailManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                return;
            }
            DocMemberExamDetailManager[0].BeginEdit();
            DocMemberExamDetailManager[0]["MFId"] = _MFId;
            DocMemberExamDetailManager[0]["TCondId"] = ComboTestCondition.SelectedItem.Value;
            DocMemberExamDetailManager[0]["UserCode"] = "---";
            DocMemberExamDetailManager[0]["EntranceCode"] = "---";
            if (!Utility.IsDBNullOrNullValue(Session["ExamFileURLConf"]))
                DocMemberExamDetailManager[0]["ExamConfirmImageURL"] = Session["ExamFileURLConf"].ToString();
            DocMemberExamDetailManager[0]["TTypeId"] = _TTypeId;
            DocMemberExamDetailManager[0]["GrdId"] = cmbGrade.SelectedItem.Value;
            DocMemberExamDetailManager[0]["Point"] = txtPoint.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ExamFileURL"]))
                DocMemberExamDetailManager[0]["FileURL"] = Session["ExamFileURL"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["ImplementPeriodFileURL"]))
                DocMemberExamDetailManager[0]["PeriodImgURL"] = Session["ImplementPeriodFileURL"].ToString();
            DocMemberExamDetailManager[0]["UserCode"] = txtUserCode.Text;
            DocMemberExamDetailManager[0]["EntranceCode"] = txtEntrantCode.Text;
            DocMemberExamDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocMemberExamDetailManager[0]["ModifiedDate"] = DateTime.Now;
            DocMemberExamDetailManager[0].EndEdit();

            if (DocMemberExamDetailManager.Save() <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming, _MFId, (int)TSP.DataManager.TableType.DocMemberExam, "ویرایش اطلاعات آزمون", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
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
    #region WF
    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldExam["PageMode"].ToString());
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        if (Request.QueryString["MFId"] == null || string.IsNullOrWhiteSpace(Request.QueryString["MFId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        int TableId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"].ToString()));
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, TableId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerDocUnit = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, WFCode, TableId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerDocUnitResp = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, WFCode, TableId, Utility.GetCurrentUser_UserId(), PageMode);

        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave || WFPerDocUnit.BtnSave || WFPerDocUnitResp.BtnSave;
        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerDocUnit.BtnEdit || WFPerDocUnitResp.BtnEdit;
        BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || WFPerDocUnit.BtnNew || WFPerDocUnitResp.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                int PermissionDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                int PermissionDocUnitrRsp = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                if (Permission > 0 || PermissionDocUnit > 0 || PermissionDocUnitrRsp > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
    #endregion
    protected string SaveImageAttach(UploadedFile uploadedFile, string type)
    {
        string ret = "";
        string imgName = _MeId.ToString() + "_";

        if (uploadedFile.IsValid)
        {
            if (type == "imgExam")
            {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    ret = "ImgExam" + imgName + Path.GetRandomFileName() + ImageType.Extension;
                } while (File.Exists(MapPath("~/image/DocMeFile/Exams/") + ret) == true);
                string tempFileName = MapPath("~/Image/DocMeFile/Exams/") + ret;
                uploadedFile.SaveAs(tempFileName, true);
                Session["ExamFileURL"] = "~/Image/DocMeFile/Exams/" + ret;
            }
            else if (type == "imgConf")
            {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    ret = "ImgExamConf" + imgName + Path.GetRandomFileName() + ImageType.Extension;
                } while (File.Exists(MapPath("~/image/DocMeFile/ExamsConfirm/") + ret) == true);
                string tempFileName = MapPath("~/Image/DocMeFile/ExamsConfirm/") + ret;
                uploadedFile.SaveAs(tempFileName, true);
                Session["ExamFileURLConf"] = "~/Image/DocMeFile/ExamsConfirm/" + ret;
            }
            else if (type == "ImageImplementPeriod")
            {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    ret = "ImgPeriod" + imgName + Path.GetRandomFileName() + ImageType.Extension;

                } while (File.Exists(MapPath("~/image/DocMeFile/ImplementPeriod/") + ret) == true);
                string tempFileName1 = MapPath("~/image/DocMeFile/ImplementPeriod/") + ret;
                uploadedFile.SaveAs(tempFileName1, true);
                Session["ImplementPeriodFileURL"] = "~/image/DocMeFile/ImplementPeriod/" + ret;

            }
        }
        return ret;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
