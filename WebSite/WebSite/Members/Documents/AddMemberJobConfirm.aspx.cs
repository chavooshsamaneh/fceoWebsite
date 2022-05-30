using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.IO;

public partial class Members_Documents_AddMemberJobConfirm : System.Web.UI.Page
{
    #region property

    string PrePgMd
    {
        get { return HiddenFieldPage["PrePgMd"].ToString(); }
        set { HiddenFieldPage["PrePgMd"] = value; }
    }
    int MFId
    {
        get { return int.Parse(HiddenFieldPage["MFId"].ToString()); }
        set { HiddenFieldPage["MFId"] = value; }
    }

    int _MeId
    {
        get { return int.Parse(HiddenFieldPage["MeId"].ToString()); }
        set { HiddenFieldPage["MeId"] = value; }
    }
    string PageMode
    {
        get { return HiddenFieldPage["PageMode"].ToString(); }
        set { HiddenFieldPage["PageMode"] = value; }
    }
    int JobConfId
    {
        get { return int.Parse(HiddenFieldPage["JobConfId"].ToString()); }
        set { HiddenFieldPage["JobConfId"] = value; }
    }

    private bool IsPageRefresh = false;
    #endregion

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MFId"] == null || Request.QueryString["PrePgMd"] == null || Request.QueryString["JobConfId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("MemberFiles.aspx");
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
            SetKeys();
        }

        if (Session["JobFileURL"] != null)
        {
            ImageConf.ImageUrl = Session["JobFileURL"].ToString();

        }

        if (Session["JobGrdURL"] != null)
        {
            ImageGrd.ImageUrl = Session["JobGrdURL"].ToString();
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (!CheckPermitionForEdit(MFId, PageMode))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.";
            return;
        }

        SetEditModeKeys();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearForm();
        Response.Redirect("MemberJobConfirm.aspx?PgMd=" + Utility.EncryptQS(PrePgMd) + "&MFId=" + Utility.EncryptQS(MFId.ToString()) + "&DocType=" + Request.QueryString["DocType"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
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

                Edit(JobConfId);


            }
        }
    }

    protected void flpConfAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageAttach(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
        HiddenFieldPage["DocExpired"] = 0;
        int MeId = 0;
        lblMeName.Text = "";
        lblMeFileNo.Text = "";
        lblLicenseDate.Text = "";
        SetVisible();
      
        HiddenFieldPage["Conf1"] = 0;
    
        if (string.IsNullOrEmpty(txtMeId.Text))
        {
            SetMessage("کد عضویت تایید کننده را وارد نمایید");
            return;
        }
        if (string.IsNullOrEmpty(txtDateTo.Text)|| string.IsNullOrEmpty(txtDateFrom.Text))
        {
            SetMessage("تاریخ های همکاری را وارد نمایید");
            return;
        }
        int.TryParse(txtMeId.Text, out MeId);
        if(MeId==Utility.GetCurrentUser_MeId())
        {
            SetMessage("کد عضویت تایید کننده نمی تواند با کد عضویت متقاضی یکی باشد");
            return;
        }
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        DataTable dtMeIdConfirm= DocMemberFileJobConfirmationManager.FindByMeIdConfirm(MFId, MeId,0, txtDateFrom.Text, txtDateTo.Text);
        if (dtMeIdConfirm.Rows.Count>0)
        {
            SetMessage("در یک درخواست کد عضویت تایید کننده نمی تواند تکراری باشد");
            return;
        }
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            SetMessage("کد عضویت وارد شده معتبر نمی باشد");
            return;
        }

        DataTable dtMemberLicenceManager = MemberLicenceManager.SelectByMemberId(MeId, 0);
        dtMemberLicenceManager.DefaultView.RowFilter = "LicenceCode <> " + (int)TSP.DataManager.Licence.kardani;
        int Count = dtMemberLicenceManager.DefaultView.Count;
        bool CanAccept = false;
        if (Count > 0)
        {
            for (int i = 0; i < Count; i++)
            {
                string LicEndDate = dtMemberLicenceManager.DefaultView[i]["EndDate"].ToString();
                Utility.Date objDate = new Utility.Date(LicEndDate);
                string TenYearsAgo = objDate.AddYears(10);
                string Today = Utility.GetDateOfToday();
                int IsDocExp = string.Compare(Today, TenYearsAgo);
                if (IsDocExp >= 0)
                {
                    CanAccept = true;
                    lblLicenseDate.Text = dtMemberLicenceManager.DefaultView[0]["EndDate"].ToString();
                    break;
                }
            }
            if (!CanAccept && PageMode != "View")
            {
                SetMessage("عضو وارد شده نمی تواند سابقه کار شما را تایید نماید.باید از مدرک فارغ التحصیلی تایید کننده ده سال گذشته باشد");
                return;
            }

        }


        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeName"]))
        {
            lblMeName.Text = MemberManager[0]["MeName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
        {
            lblMeFileNo.Text = MemberManager[0]["FileNo"].ToString();
        }
        else if (PageMode != "View")
        {
            SetMessage("عضو وارد شده نمی تواند سابقه کار شما را تایید نماید.این عضو دارای پروانه اشتغال به کار نمی باشد");
            return;
        }




        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
        {
            string FileDate = MemberManager[0]["FileDate"].ToString();
            if (FileDate.CompareTo(Utility.GetDateOfToday()) <= 0)
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

           
                DataTable dtDocMe = DocMemberFileManager.SelectMainRequest(MeId, 0);
                if (dtDocMe.Rows.Count > 0)
                {
                    if (Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["TaskCode"]) ||
                        (Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument ))
                        //&& Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess))
                    {
                        HiddenFieldPage["DocExpired"] = 1;
                        SetMessage("تاریخ اعتبار پروانه عضو وارد شده به اتمام رسیده است. عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");

                        return;
                    }
                }
                else
                {
                    HiddenFieldPage["DocExpired"] = 1;
                    SetMessage("تاریخ اعتبار پروانه عضو وارد شده به اتمام رسیده است. عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");

                    return;
                }
            }
        }
        else
        {
            HiddenFieldPage["DocExpired"] = 1;
            SetMessage("تاریخ اعتبار پروانه عضو وارد شده مشخص نمی باشد.عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");
            return;
        }
        HiddenFieldPage["Conf1"] = 1;
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        if ((string.IsNullOrEmpty(Utility.DecryptQS(Request.QueryString["JobConfId"]))) || (string.IsNullOrEmpty(Utility.DecryptQS(Request.QueryString["MFId"]))))
        {
            Response.Redirect("MemberJobConfirm.aspx");
            return;
        }
        PrePgMd = Utility.DecryptQS(Request.QueryString["PrePgMd"]);
        JobConfId = int.Parse(Utility.DecryptQS(Request.QueryString["JobConfId"]));
        PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
        MFId = int.Parse(Utility.DecryptQS(Request.QueryString["MFId"]));

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);

        CheckPermitionForEdit(MFId, PageMode);
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
        //Set Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        EnableControls(false);
        FillForm(JobConfId);
        SetVisible();

        RoundPanelJobConfirm.HeaderText = "مشاهده";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    private void SetEditModeKeys()
    {
        PageMode = "Edit";
        //Check UserPermission       
        //Set Button's Enable
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        EnableControls(true);

        FillForm(JobConfId);
        SetVisible();

        RoundPanelJobConfirm.HeaderText = "ویرایش";
    }

    private void SetNewModeKeys()
    {
        PageMode = "New";
        JobConfId = -1;
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        cmbConfirmType.SelectedIndex = cmbConfirmType.Items.FindByValue(((int)TSP.DataManager.DocumentJobConfirmType.Office).ToString()).Index;

        SetVisible();
        ClearForm();
        EnableControls(true);

        RoundPanelJobConfirm.HeaderText = "جدید";
    }

    private void FillForm(int JobConfId)
    {
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        DocMemberFileJobConfirmationManager.FindByCode(JobConfId);
        if (DocMemberFileJobConfirmationManager.Count == 1)
        {
            if (DocMemberFileJobConfirmationManager[0]["ConfirmType"] != null)
                cmbConfirmType.SelectedIndex = cmbConfirmType.Items.FindByValue(DocMemberFileJobConfirmationManager[0]["ConfirmType"].ToString()).Index;

            txtDateFrom.Text = DocMemberFileJobConfirmationManager[0]["FromDate"].ToString();

            txtDateTo.Text = DocMemberFileJobConfirmationManager[0]["ToDate"].ToString();

            txtPosition.Text = DocMemberFileJobConfirmationManager[0]["Position"].ToString();

            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
            {
                txtOfficeName.Text = DocMemberFileJobConfirmationManager[0]["Name"].ToString();
                txtOfficeMfNo.Text = DocMemberFileJobConfirmationManager[0]["MFNo"].ToString();
                txtDescription.Text = DocMemberFileJobConfirmationManager[0]["Description"].ToString();
                ImageConf.ImageUrl = DocMemberFileJobConfirmationManager[0]["FileURL"].ToString();
                ImageGrd.ImageUrl = DocMemberFileJobConfirmationManager[0]["GrdURL"].ToString();
                HiddenFieldPage["GrdnameURL"] = DocMemberFileJobConfirmationManager[0]["GrdURL"].ToString().Replace("~", "../..");
            }
            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
            {
                txtMeId.Text = DocMemberFileJobConfirmationManager[0]["MeId"].ToString();
                txtMeId_TextChanged(this, new EventArgs());
                txtDescription.Text = DocMemberFileJobConfirmationManager[0]["Description"].ToString();
                ImageConf.ImageUrl = DocMemberFileJobConfirmationManager[0]["FileURL"].ToString();
            }
            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.GovCom)
            {
                txtOfficeName.Text = DocMemberFileJobConfirmationManager[0]["Name"].ToString();
                txtDescription.Text = DocMemberFileJobConfirmationManager[0]["Description"].ToString();
                ImageConf.ImageUrl = DocMemberFileJobConfirmationManager[0]["FileURL"].ToString();
            }
            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
            {
                ComboProvince.Text = DocMemberFileJobConfirmationManager[0]["Name"].ToString();
                //txtDescription.Text = DocMemberFileJobConfirmationManager[0]["Description"].ToString();
                ImageConf.ImageUrl = DocMemberFileJobConfirmationManager[0]["FileURL"].ToString();
            }

        }

    }

    private void SetVisible()
    {
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
        {
            lblDescription.ClientVisible = true;
            txtDescription.ClientVisible = true;
            lblOfficeName.ClientVisible = true;
            txtOfficeName.ClientVisible = true;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = true;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = true;
            flpGrdAttach.ClientVisible = true;
            ImageGrd.ClientVisible = true;
            if (ImageGrd.ImageUrl == "" && Session["JobGrdURL"] == null)
            {
                ImageGrd.ImageUrl = "~/Images/noimage.gif";
            }

            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
        {
            lblDescription.ClientVisible = false;
            txtDescription.ClientVisible = false;
            lblOfficeName.ClientVisible = false;
            txtOfficeName.ClientVisible = false;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = false;
            RoundPanelconfMember1.ClientVisible = true;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";

            HiddenFieldPage["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.GovCom)
        {
            lblDescription.ClientVisible = true;
            txtDescription.ClientVisible = true;
            lblOfficeName.ClientVisible = true;
            txtOfficeName.ClientVisible = true;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = true;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";
            HiddenFieldPage["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;

        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
        {
            lblDescription.ClientVisible = false;
            txtDescription.ClientVisible = false;
            lblOfficeName.ClientVisible = false;
            txtOfficeName.ClientVisible = false;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = false;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";

            HiddenFieldPage["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = true;
            ComboProvince.ClientVisible = true;
        }
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(int.Parse(Utility.DecryptQS(Request.QueryString["MFId"])), (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        _MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);

        if (uploadedFile.IsValid)
        {

            if (id == "flpConfAttach")
            {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);

                    ret = "flpConfAttach_MeId" + _MeId.ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

                } while (File.Exists(MapPath("~/Image/DocMeFile/JobConfirm/") + ret) == true);
                string tempFileName = MapPath("~/Image/DocMeFile/JobConfirm/") + ret;

                uploadedFile.SaveAs(tempFileName, true);
                Session["JobFileURL"] = "~/Image/DocMeFile/JobConfirm/" + ret;
            }

            else if (id == "flpGrdAttach")
            {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);

                    ret = "flpGrdAttach_MeId" + _MeId.ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

                } while (id == "flpGrdAttach" && File.Exists(MapPath("~/Image/DocMeFile/OfficeGrade/") + ret) == true);
                string tempFileName = MapPath("~/Image/DocMeFile/OfficeGrade/") + ret;

                uploadedFile.SaveAs(tempFileName, true);
                Session["JobGrdURL"] = "~/Image/DocMeFile/OfficeGrade/" + ret;
                HiddenFieldPage["GrdnameURL"] = "../../Image/DocMeFile/OfficeGrade/" + ret;
            }

        }
        return ret;
    }

    private void ClearForm()
    {
        txtDescription.Text = "";
        if (cmbConfirmType.SelectedIndex == 1)
        {
            cmbConfirmType.SelectedIndex = 1;
        }
        else
        {
            cmbConfirmType.SelectedIndex = 0;
        }
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        txtPosition.Text = "";
        txtDescription.Text = "";
        txtMeId.Text = "";
        txtOfficeMfNo.Text = "";
        txtOfficeName.Text = "";
        ImageConf.ImageUrl = "~/Images/noimage.gif";
        ImageGrd.ImageUrl = "~/Images/noimage.gif";
        Session["JobFileURL"] = null;
        Session["JobGrdURL"] = null;
        lblMeName.Text = "- - -";
        lblMeFileNo.Text = "- - -";
        lblLicenseDate.Text = "- - -";
        SetVisible();
    }

    private void EnableControls(bool Enable)
    {
        cmbConfirmType.ClientEnabled = Enable;
        ComboProvince.ClientEnabled = Enable;

        txtDateFrom.Enabled = Enable;
        txtDateTo.Enabled = Enable;
        txtPosition.Enabled = Enable;

        txtOfficeName.ClientEnabled = Enable;
        txtOfficeMfNo.ClientEnabled = Enable;
        txtDescription.ClientEnabled = Enable;

        txtMeId.ClientEnabled = Enable;
        ComboProvince.ClientEnabled = Enable;
        lblHelpDes.Visible = Enable;
        lblHelpMeId.Visible = Enable;

        //image visible 
        flpConfAttach.Visible = Enable;
        flpGrdAttach.Visible = Enable;
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

    private void Insert()
    {
        if (Session["JobFileURL"] == null)
        {
            SetMessage("تصویر فرم سابقه کار ثبت نشده است");
            return;
        }
        if (cmbConfirmType.Value == null)
        {
            SetMessage("نوع تایید کننده را انتخاب نمایید");
            return;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
        {
            if (HiddenFieldPage["Conf1"].ToString() != "1")
            {
                SetMessage("اطلاعات تایید کننده معتبر نمی باشد");
                return;
            }
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
        {

            if (Session["JobGrdURL"] == null)
            {
                SetMessage("تصویر پروانه یا گواهی رتبه بندی ثبت نشده است");
                return;
            }
        }

        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
        {
            if (Utility.IsDBNullOrNullValue(ComboProvince.Text))
            {
                SetMessage("استان را انتخاب نمایید");
                return;
            }
        }

        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();

        try
        {
            if (cmbConfirmType.Value == null)
            {
                SetMessage("نوع تایید کننده را مشخص کنید");
                return;
            }

            DataRow MejobConfirmRow = DocMemberFileJobConfirmationManager.NewRow();
            MejobConfirmRow["MfId"] = MFId;

            if (!Utility.IsDBNullOrNullValue(txtDateFrom.Text))
                MejobConfirmRow["FromDate"] = txtDateFrom.Text;
            if (!Utility.IsDBNullOrNullValue(txtDateTo.Text))
                MejobConfirmRow["ToDate"] = txtDateTo.Text;
            if (!Utility.IsDBNullOrNullValue(txtPosition.Text))
                MejobConfirmRow["Position"] = txtPosition.Text;

            MejobConfirmRow["ConfirmType"] = cmbConfirmType.Value;
            if (!Utility.IsDBNullOrNullValue(txtOfficeName.Text))
                MejobConfirmRow["Name"] = txtOfficeName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfficeMfNo.Text))
                MejobConfirmRow["MFNo"] = txtOfficeMfNo.Text;

            if (!Utility.IsDBNullOrNullValue(txtDescription.Text))
                MejobConfirmRow["Description"] = txtDescription.Text;

            if (!Utility.IsDBNullOrNullValue(txtMeId.Text))
            {
                MejobConfirmRow["MeId"] = txtMeId.Text;
                MejobConfirmRow["Name"] = lblMeName.Text;
            }
          

            if (!Utility.IsDBNullOrNullValue(ComboProvince.Text))
            {
                MejobConfirmRow["Name"] = ComboProvince.Text;
            }

            MejobConfirmRow["FileURL"] = "~/image/DocMeFile/JobConfirm/" + Path.GetFileName(Session["JobFileURL"].ToString());
            if (Session["JobGrdURL"] != null)
            {
                MejobConfirmRow["GrdURL"] = "~/image/DocMeFile/OfficeGrade/" + Path.GetFileName(Session["JobGrdURL"].ToString());
            }

            MejobConfirmRow["UserId"] = Utility.GetCurrentUser_UserId();
            MejobConfirmRow["ModifiedDate"] = DateTime.Now;
            DocMemberFileJobConfirmationManager.AddRow(MejobConfirmRow);

            int cn = DocMemberFileJobConfirmationManager.Save();
            if (cn > 0)
            {                
                JobConfId = Convert.ToInt32(DocMemberFileJobConfirmationManager[0]["JobConfId"]);
                SetMessage("ذخیره با موفقیت انجام شد.");
                SetEditModeKeys();
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Edit(int JobConfId)
    {
        if (Session["JobFileURL"] == null && Utility.IsDBNullOrNullValue(ImageConf.ImageUrl))
        {
            SetMessage("تصویر فرم سابقه کار ثبت نشده است");
            return;
        }
        if (cmbConfirmType.Value == null)
        {
            SetMessage("نوع تایید کننده را انتخاب نمایید");
            return;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
        {
            if (HiddenFieldPage["Conf1"].ToString() != "1")
            {
                SetMessage("اطلاعات تایید کننده معتبر نمی باشد");
                return;
            }
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
        {

            if (Session["JobGrdURL"] == null)
            {
                SetMessage("تصویر پروانه یا گواهی رتبه بندی ثبت نشده است");
                return;
            }
        }

        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
        {
            if (Utility.IsDBNullOrNullValue(ComboProvince.Text))
            {
                SetMessage("استان را انتخاب نمایید");
                return;
            }
        }
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();

        try
        {
            if (cmbConfirmType.Value == null)
            {
                SetMessage("نوع تایید کننده را مشخص کنید");
                return;
            }
            DocMemberFileJobConfirmationManager.FindByCode(JobConfId);
            if (DocMemberFileJobConfirmationManager.Count == 1)
            {
                DocMemberFileJobConfirmationManager[0].BeginEdit();
                if (!Utility.IsDBNullOrNullValue(txtDateFrom.Text))
                    DocMemberFileJobConfirmationManager[0]["FromDate"] = txtDateFrom.Text;
                if (!Utility.IsDBNullOrNullValue(txtDateTo.Text))
                    DocMemberFileJobConfirmationManager[0]["ToDate"] = txtDateTo.Text;
                if (!Utility.IsDBNullOrNullValue(txtPosition.Text))
                    DocMemberFileJobConfirmationManager[0]["Position"] = txtPosition.Text;

                DocMemberFileJobConfirmationManager[0]["ConfirmType"] = cmbConfirmType.Value;
                if (txtOfficeName.Text != null)
                    DocMemberFileJobConfirmationManager[0]["Name"] = txtOfficeName.Text;
                if (txtOfficeMfNo.Text != null)
                    DocMemberFileJobConfirmationManager[0]["MFNo"] = txtOfficeMfNo.Text;

                if (txtDescription.Text != null)
                    DocMemberFileJobConfirmationManager[0]["Description"] = txtDescription.Text;

                if (!Utility.IsDBNullOrNullValue(txtMeId.Text))
                {
                    DocMemberFileJobConfirmationManager[0]["MeId"] = txtMeId.Text;
                    DocMemberFileJobConfirmationManager[0]["Name"] = lblMeName.Text;
                }
                if (!Utility.IsDBNullOrNullValue(ComboProvince.Text))
                {
                    DocMemberFileJobConfirmationManager[0]["Name"] = ComboProvince.Text;
                }

                if (Session["JobFileURL"] != null)
                    DocMemberFileJobConfirmationManager[0]["FileURL"] = "~/image/DocMeFile/JobConfirm/" + Path.GetFileName(Session["JobFileURL"].ToString());
                if (Session["JobGrdURL"] != null)
                    DocMemberFileJobConfirmationManager[0]["GrdURL"] = "~/image/DocMeFile/OfficeGrade/" + Path.GetFileName(Session["JobGrdURL"].ToString());


                DocMemberFileJobConfirmationManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileJobConfirmationManager[0]["ModifiedDate"] = DateTime.Now;

                DocMemberFileJobConfirmationManager[0].EndEdit();

                int cn = DocMemberFileJobConfirmationManager.Save();
                if (cn > 0)
                {
                    SetMessage("ذخیره انجام شد.");
                }
                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است.");
                }
            }
            else
            {
                SetMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }

        }

        catch (Exception err)
        {

            SetError(err);
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
    #endregion
    #endregion
}