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
using DevExpress.Web;
using System.IO;

public partial class Members_Documents_AddMemberExam : System.Web.UI.Page
{
    #region Properties
    private bool IsPageRefresh = false;
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
            Response.Redirect("~/ErrorPage.aspx");
        }
        if (this.ViewState["dtTConDetail"] != null)
        {
            DataTable dtTConDetail = (DataTable)this.ViewState["dtTConDetail"];
            cmbTestType.DataSource = dtTConDetail;
            cmbTestType.DataBind();
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


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

        if (!IsPostBack)
        {
            HiddenFieldExam["name"] = 0;
            HiddenFieldExam["PeriodImg"] = 0;
            HiddenFieldExam["TCondId"] = null;
            Session["ExamFileURL"] = null;
            Session["ExamFileURLConf"] = null;
            Session["ImplementPeriodFileURL"] = null;

            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            SetKeys();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (!Utility.IsDBNullOrNullValue(Session["ExamFileURL"]))
            HyperLinkExamFileURL.ImageUrl = Session["ExamFileURL"].ToString();
        if (!Utility.IsDBNullOrNullValue(Session["ImplementPeriodFileURL"]))
            HyperLinkPeriodImg.ImageUrl = Session["ImplementPeriodFileURL"].ToString();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        cmbMajor.ClientEnabled = true;
        EnableControls();
        ClearForm();
        HiddenFieldExam["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldExam["MExmId"] = "";
        RoundPanelExam.HeaderText = "جدید";
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave.Enabled = true;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!CheckPermitionForEdit(_MFId, _PageMode))
        {
            SetMessage( "امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.");
            return;
        }
        EnableControls();
        cmbMajor.ClientEnabled = false;
        HiddenFieldExam["PageMode"] = Utility.EncryptQS("Edit");
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        SetEditModeKeys();
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberExam.aspx?PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&MFId=" + Utility.EncryptQS(_MFId.ToString()));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (_PageMode == "New")
        {
            Insert();
        }
        else if (_PageMode == "Edit")
        {
            Edit(_MExmDId);
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
    protected void flpAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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
    #endregion

    #region Methods    
    protected string SaveImageAttach(UploadedFile uploadedFile, string type)
    {
        string ret = "";
        string imgName = "";
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MfId = int.Parse(Utility.DecryptQS(Request.QueryString["MFId"]));
        DocMemberFileManager.FindByCode(MfId, 0);

        if (DocMemberFileManager.Count == 1)
        {
            imgName = DocMemberFileManager[0]["MeId"].ToString() + "_";
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        if (uploadedFile.IsValid)
        {
            if (type == "imgExam")
            {
                HiddenFieldExam["name"] = 0;
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    ret = "ImgExam" + imgName + Path.GetRandomFileName() + ImageType.Extension;
                } while (File.Exists(MapPath("~/image/DocMeFile/Exams/") + ret) == true);// || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
                string tempFileName = MapPath("~/Image/DocMeFile/Exams/") + ret;
                uploadedFile.SaveAs(tempFileName, true);
                Session["ExamFileURL"] = "~/Image/DocMeFile/Exams/" + ret;
            }
            else if (type == "ImageImplementPeriod")
            {
                HiddenFieldExam["PeriodImg"] = 0;
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

    private void SetKeys()
    {
        try
        {
            _MExmDId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MExmDId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            _PrePageMode = Request.QueryString["PrePgMd"];
            _MFId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"]));
            ObjdsMemberLicence.SelectParameters[0].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            SetMode(_PageMode);
            CheckPermitionForEdit(_MFId, _PageMode);
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه ایجاد شده است.";
            Utility.SaveWebsiteError(err);
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
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled =
        btnNew2.Enabled =
        btnEdit.Enabled =
        btnEdit2.Enabled = true;
        txtEntrantCode.Enabled =
        txtPoint.Enabled =
        txtUserCode.Enabled =
        cmbGrade.Enabled =
        cmbTestType.Enabled =
        cmbMajor.ClientEnabled =
        ComboTestCondition.Enabled = false;
        FillForm(_MExmDId);
        RoundPanelExam.HeaderText = "مشاهده";
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        btnEdit.Enabled =
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        EnableControls();
        ClearForm();
        RoundPanelExam.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnEdit.Enabled =
        btnEdit2.Enabled = false;
        btnSave.Enabled =
        btnSave2.Enabled = true;
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
        Session["ImplementPeriodFileURL"] = null;
        HyperLinkPeriodImg.ImageUrl =
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
         UploadControlPeriodImgURL.ClientVisible =
         RoundPanelMeExam.Enabled = ComboTestCondition.Enabled = true;
    }

    private void Insert()
    {
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
        try
        {
            if (ComboTestCondition.SelectedItem == null || ComboTestCondition.SelectedItem.Value == null)
            {
                SetMessage( "آزمون را انتخاب نمایید.");
                return;
            }
            DataTable dt= DocTestConditionDetailManager.SelectByTestCondition(Convert.ToInt32(ComboTestCondition.SelectedItem.Value));
            if(dt.Rows.Count>0 && Convert.ToBoolean(dt.Rows[0]["NeedFileUpload"]) && Session["ImplementPeriodFileURL"]==null)
            {

                SetMessage("آپلود تصویر گواهینامه آموزشی اجباری می باشد.");
                return;
            }                
            DataTable dtMeExam = DocMemberExamDetailManager.SelectByMFId(_MFId, Utility.GetCurrentUser_MeId());
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
            MemberExamRow["UserCode"] = "---";
            MemberExamRow["EntranceCode"] = "---";
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
                SetMessage( "خطایی در ذخیره انجام گرفته است.");
                return;
            }

            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            cmbMajor.ClientEnabled = false;
            _PageMode = "Edit";
            _MExmDId = Convert.ToInt32(DocMemberExamDetailManager[0]["MExmDId"]);
            RoundPanelExam.HeaderText = "ویرایش";
            SetMessage("ذخیره انجام شد.");
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming, _MFId, (int)TSP.DataManager.TableType.DocMemberExam, "ثبت آزمون جدید", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);

        }
        catch (Exception err)
        {
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
            DataTable dtMeExam = DocMemberExamDetailManager.SelectByMFId(-1, Utility.GetCurrentUser_MeId());
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

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId, string PageMode)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortal(TableId, PageMode);
        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
        btnEdit.Enabled = btnEdit2.Enabled = WFPermission.BtnEdit;
        BtnNew.Enabled = btnNew2.Enabled = WFPermission.BtnNew;
        return WFPermission.BtnEdit;
    }
    private void SetMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
    #endregion
}
