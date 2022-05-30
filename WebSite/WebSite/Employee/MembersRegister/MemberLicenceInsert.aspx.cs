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

public partial class Employee_MembersRegister_MemberLicenceInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    string _PageMode
    {
        get
        {
            return HiddenFieldLicence["PgMode"].ToString(); //PgMode.Value;
        }
        set
        {
            // PgMode.Value
            HiddenFieldLicence["PgMode"] = value;
        }
    }

    int _MeId
    {
        get
        {
            //return Convert.ToInt32(MemberId.Value);
            try
            {
                return Convert.ToInt32(HiddenFieldLicence["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            }
        }
        set
        {
            //  MemberId.Value
            HiddenFieldLicence["MeId"] = value.ToString();
        }
    }

    int _MReId
    {
        get
        {
            // return Convert.ToInt32(MemberRequest.Value);
            try
            {
                return Convert.ToInt32(HiddenFieldLicence["MReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
            }
        }
        set
        {
            //  MemberRequest.Value
            HiddenFieldLicence["MReId"] = value.ToString();
        }
    }

    int _MLId
    {
        get
        {
            // return Convert.ToInt32(LicenceId.Value);
            return Convert.ToInt32(HiddenFieldLicence["MlId"]);
        }
        set
        {
            //    LicenceId.Value
            HiddenFieldLicence["MlId"] = value.ToString();
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
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

        FillMemberName();

        if (IsCallback)
            ShowImageInReloadPage();

        if (ComboCountry.Value != null)
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
            ObjectDataSourceSearchUniversity.SelectParameters["UnName"].DefaultValue = txtUniNameSearch.Text;
            CustomAspxDevGridView1.DataBind();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MReId = -1;
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
        {
            TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
            LiManager.FindByCode(_MLId);
            if (LiManager.Count > 0)
            {
                MReId = Convert.ToInt32(LiManager[0]["MReId"]);
            }
        }
        else if (Mode == "TempMe")
        {
            TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
            LiManager.FindByCode(_MLId);
            if (LiManager.Count > 0)
            {
                MReId = Convert.ToInt32(LiManager[0]["MReId"]);
            }
        }

        if (MReId == _MReId)
        {
            if (!CheckPermitionForEdit(MReId))
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
                return;
            }
        }
        else
        {
            ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
            return;
        }

        if (Utility.IsDBNullOrNullValue(_MLId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetEnabled(true);

        TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        _PageMode = "Edit";
        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (cmbLicenceType.Value.ToString() == "1")
        {
            int LiId = Convert.ToInt32(drdLicence.Value);
            TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
            LicenceManager.FindByCode(LiId);
            if (LicenceManager.Count == 1)
            {
                int LicenceCode = Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
                if (LicenceCode == (int)TSP.DataManager.Licence.kardani)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "مدرک کاردانی را نمی توان به عنوان پیش فرض انتخاب کرد";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
                return;
            }
        }
        string Mode = Utility.DecryptQS(HDMode.Value);

        switch (_PageMode)
        {
            case "New":
                if (Mode == "Request")
                    Insert();
                else if (Mode == "TempMe")
                    InsertMeTempLi();
                break;
            case "Edit":
                if (Utility.IsDBNullOrNullValue(_MLId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if (Mode == "Request")
                    Edit(_MLId);
                else if (Mode == "TempMe")
                    EditTempMeLi(_MLId);
                break;

            case "EditInActive":
                if (Utility.IsDBNullOrNullValue(_MLId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if (Mode == "Request")
                {
                    TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
                    MemberLicenceManager.FindByCode(_MLId);
                    if (MemberLicenceManager.Count != 1)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
                        return;
                    }
                    if (Convert.ToUInt32(MemberLicenceManager[0]["MReId"]) == _MReId)
                        Edit(_MLId);
                    else
                        InsertAndInActive(_MLId);
                }
                else if (Mode == "TempMe")
                    EditTempMeLi(_MLId);
                break;
            case "Confirming":
                if (Utility.IsDBNullOrNullValue(_MLId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                EditConfirming(_MLId);
                break;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        _MLId = -1;
        _PageMode = "New";
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        SetIranForCountry();
        if (!Utility.IsDBNullOrNullValue(ComboCountry.Value))
            FillUniversity(ComboCountry.Value.ToString(), "");
        SetEnabled(true);

        txtUniName.Text = "";
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] Parameters = e.Parameters.Split(';');
        if (Parameters.Length == 0)
            return;
        if (Parameters[0] == "Search")
        {
            FillUniversity(Parameters[2], Parameters[1]);
        }
        else
        {
            string country = Parameters[0];
            string UnName = Parameters[1];
            string ReType = Parameters[2];
            switch (ReType)
            {
                case "btnRefresh":
                    SetIranForCountry();
                    break;
                default:
                    FillUniversity(country, UnName);
                    break;
            }
        }
    }

    protected void flpLicense_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "Image");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadScoreImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "ScoreImage");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void UploadControlInquiry_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "InquiryImage");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadEquivalentImageURL_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "Equivalent");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadEntranceExamConfImageURL_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "EntranceExamConfImageURL");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    #region Methods

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetKey()
    {
        try
        {
            HiddenFieldLicence["IsConfMode"] = 0;
            Session["License"] = null;

            HiddenFieldUniValue.Set("Id", null);
            if (string.IsNullOrEmpty(Request.QueryString["MReId"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }
            ViewState["IsEdited_MeMadrak"] = false;

            #region CheckPermission
            TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            #endregion

            _PageMode = Utility.DecryptQS(Request.QueryString["aPageMode"].ToString());
            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            _MLId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MlId"].ToString()));
            _MReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());

            string Mode = Utility.DecryptQS(HDMode.Value);

            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(_MeId);

            if (string.IsNullOrEmpty(_PageMode) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            SetMajorParameter(_PageMode);

            try
            {
                #region SetMode
                switch (_PageMode)
                {
                    case "View":
                        #region SetViewMode
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                        }

                        SetEnabled(false);

                        //   HpLicense.ClientVisible = true;

                        if (_MLId == null)
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        btnEdit.Enabled = per.CanEdit;
                        btnEdit2.Enabled = per.CanEdit;

                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        if (Mode == "Request")
                            FillForm(_MLId);
                        else if (Mode == "TempMe")
                            FillFormTempMeLi(_MLId);

                        //txtLetterDate.Enabled = false;
                        //txtLetterNo.ClientEnabled = false;
                        //txtSDate.Enabled = false;
                        //txtSNo.ClientEnabled = false;                        
                        ASPxRoundPanel2.HeaderText = "مشاهده";
                        break;
                    #endregion
                    case "New":
                        #region SetNewMode

                        SetEnabled(true);

                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;
                        ASPxRoundPanel2.HeaderText = "جدید";

                        ClearForm();
                        SetIranForCountry();
                        if (!Utility.IsDBNullOrNullValue(ComboCountry.Value))
                            FillUniversity(ComboCountry.Value.ToString(), "");
                        PanelInQuiry.Visible = false;
                        break;
                    #endregion
                    case "Edit":
                        #region SetEditMode
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                        }

                        SetEnabled(true);
                        HpLicense.ClientVisible = true;

                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;

                        if (_MLId == null)
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        if (Mode == "Request")
                            FillForm(_MLId);
                        else if (Mode == "TempMe")
                            FillFormTempMeLi(_MLId);
                        ASPxRoundPanel2.Enabled = true;
                        ASPxRoundPanel2.HeaderText = "ویرایش";
                        PanelInQuiry.Visible = false;
                        break;
                    #endregion
                    case "EditInActive":
                        #region SetEditMode
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                        }

                        SetEnabled(true);
                        HpLicense.ClientVisible = true;

                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;

                        if (_MLId == null)
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        if (Mode == "Request")
                        {
                            FillForm(_MLId);
                        }
                        else if (Mode == "TempMe")
                        {
                            FillFormTempMeLi(_MLId);
                            PanelInQuiry.Visible = false;
                        }
                        ASPxRoundPanel2.Enabled = true;
                        ASPxRoundPanel2.HeaderText = "ویرایش و غیرفعال";

                        break;
                    #endregion
                    case "Confirming":
                        HiddenFieldLicence["IsConfMode"] = 1;
                        #region SetConfirmingMode
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                        }
                        if (_MLId == null)
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }

                        FillForm(_MLId);
                        PanelInQuiry.Visible = true;
                        ASPxRoundPanel2.Enabled = true;
                        ASPxRoundPanel2.HeaderText = "استعلام و تایید مدرک";
                        break;
                        #endregion
                }
                #endregion

                #region Check & Set Permission
                CheckWorkFlowPermission();

                if (Mode == "Home")
                {
                    #region Home Mode
                    ReqManager.FindByMemberId(_MLId, -1, 1);
                    if ((ReqManager[0]["IsConfirm"].ToString() != "0") || (!Convert.ToBoolean(ReqManager[0]["Requester"])))
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;

                    }

                    if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                    }
                    #endregion
                }
                else if (Mode == "Request")
                {
                    #region Request Mode

                    ReqManager.FindByCode(_MReId);
                    if (ReqManager.Count > 0)
                    {
                        if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                        {
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnSave.Enabled = false;
                            btnSave2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                        }

                        if ((!Utility.IsDBNullOrNullValue(ReqManager[0]["MsId"]))
                                && //(Convert.ToInt32(ReqManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                               ((Convert.ToInt32(ReqManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.Fired)
                                || (Convert.ToInt32(ReqManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.Dead)
                                || (Convert.ToInt32(ReqManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.Cancel)))
                        {
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnSave.Enabled = false;
                            btnSave2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                        }

                        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
                        if (_MLId == null)
                        {
                            LiManager.FindByCode(_MLId);
                            if (LiManager.Count == 1)
                            {
                                if (Convert.ToInt32(LiManager[0]["MReId"]) != _MReId)
                                {
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    btnEdit.Enabled = false;
                                    btnEdit2.Enabled = false;
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (_PageMode == "Confirming")
                {
                    SetEditModeForRequest();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    protected void SetMajorParameter(string mode)
    {
        switch (mode)
        {
            case "New":
                ODBMajor.SelectParameters["InActiveMajor"].DefaultValue = "0";
                break;
            case "Edit":
                ODBMajor.SelectParameters["InActiveMajor"].DefaultValue = "0";
                break;
            case "EditInActive":
                ODBMajor.SelectParameters["InActiveMajor"].DefaultValue = "0";
                break;
            case "View":
                ODBMajor.SelectParameters["InActiveMajor"].DefaultValue = "-1";
                break;
        }

    }

    protected void SetIranForCountry()
    {
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = ComboCountry.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;
    }

    protected void SetEditModeForRequest()
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        BtnNew.Enabled = false;
        BtnNew2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        ComboCountry.Enabled = false;
        txtAvg.Enabled = false;
        txtCity.Enabled = false;
        txtDescription.Enabled = false;
        txtEndDate.Enabled = false;
        txtNumUnit.Enabled = false;
        txtStartDate.Enabled = false;
        txtThesis.Enabled = false;
        txtUniName.Enabled = false;
        //ChbDefault.Enabled = false;
        cmbLicenceType.Enabled = false;
        drdLicence.Enabled = false;
        drdMajor.Enabled = false;
        txtdrdUniName.Enabled = false;
        btnSearch1.Enabled = false;
        HpLicense.ClientVisible = true;
        flpLicense.Visible = false;
        ChbEstelam.Visible = true;
        //PanelUploadControlInquiry.ClientVisible = true;
        //lblSNo.Visible = true;
        //lblSDate.Visible = true;
        //txtSDate.Visible = true;
        //txtSNo.Visible = true;
        //ChbConfirm.Visible = true;
        lblConfirm.Visible = true;
        cmbConfirm.Visible = true;
        //PanelConfirmType.ClientVisible = true;
        //lblNo.Visible = true;
        //lblDate.Visible = true;
        //txtLetterDate.Visible = true;
        //txtLetterNo.Visible = true;
        //txtSTitle.Visible = true;
        //lblSTitle.Visible = true;
        //txtLetterTitle.Visible = true;
        //lblLetterTitle.Visible = true;

    }

    protected void SetEnabled(Boolean Enabled)
    {
        txtAvg.Enabled = Enabled;
        txtCity.Enabled = Enabled;
        txtDescription.Enabled = Enabled;
        txtdrdUniName.Enabled = Enabled;
        txtEndDate.Enabled = Enabled;
        //txtLetterDate.Enabled = Enabled;
        //txtLetterNo.ClientEnabled = Enabled;
        txtNumUnit.Enabled = Enabled;
        //txtSDate.Enabled = Enabled;
        //txtSNo.ClientEnabled = Enabled;
        txtStartDate.Enabled = Enabled;
        txtThesis.Enabled = Enabled;
        txtUniName.Enabled = Enabled;
        drdLicence.Enabled = Enabled;
        drdMajor.Enabled = Enabled;
        cmbConfirm.Enabled = Enabled;
        cmbLicenceType.Enabled = Enabled;
        ChbEstelam.Enabled = Enabled;
        ComboCountry.Enabled = Enabled;
        btnSearch1.Enabled = Enabled;
        flpLicense.Visible = Enabled;
        ChbEstelam.Enabled = Enabled;
        cmbConfirm.Enabled = Enabled;
        UploadControlInquiry.Visible = Enabled;

        FileUploadScoreImage.Visible = Enabled;
        FileUploadEntranceExamConfImageURL.Visible = Enabled;
        FileUploadEquivalentImageURL.Visible = Enabled;
    }

    protected void ClearForm()
    {
        drdLicence.SelectedIndex = -1;
        drdMajor.SelectedIndex = -1;
        ComboCountry.SelectedIndex = -1;
        txtCity.Text = "";
        txtdrdUniName.Text = "";
        txtUniName.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtNumUnit.Text = "";
        txtAvg.Text = "";
        txtThesis.Text = "";
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        txtCity.Text = String.Empty;
        cmbConfirm.SelectedIndex = -1;
        ChbEstelam.Checked = false;
        FillUniversity("-2", "");
        HDFlpLicense.Set("name", 0);
        HDFlpLicense.Set("InquiryImage", 0);
        HDFlpLicense.Set("Score", 0);
        HDFlpLicense.Set("EntranceExamConfImageURL", 0);
        HDFlpLicense.Set("Equivalent", 0);
        cmbLicenceType.SelectedIndex = -1;
        ClearImageUrl();
    }

    #region Insert-Update
    protected void Insert()
    {
        if (IsPageRefresh)
            return;
        #region DefineManagers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();
        //TSP.DataManager.UniversityManager UnManager = new TSP.DataManager.UniversityManager();
        //TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        //TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();

        trans.Add(LiManager);
        trans.Add(LiManager2);
        //trans.Add(UnManager);
        //trans.Add(MeManager);
        trans.Add(WorkFlowStateManager);
        //trans.Add(AttachManager);
        trans.Add(MemberRequestManager);
        #endregion

        bool IsAttach = false;

        try
        {
            trans.BeginSave();

            int Tableid = _MReId;

            LiManager2.FindByMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                            LiManager2.DataTable.AcceptChanges();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == drdMajor.Value.ToString()) && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            DataRow dr = LiManager.NewRow();

            if (drdLicence.Value != null)
                dr["LiId"] = int.Parse(drdLicence.Value.ToString());
            if (ComboCountry.Value != null)
                dr["CounId"] = int.Parse(ComboCountry.Value.ToString());
            else
                dr["CounId"] = DBNull.Value;
            dr["MeId"] = _MeId;
            if (drdMajor.Value != null)
                dr["MjId"] = int.Parse(drdMajor.Value.ToString());
            if (HiddenFieldUniValue.Get("Id") != null)
                dr["UnId"] = Convert.ToInt32(HiddenFieldUniValue.Get("Id"));
            dr["UnName"] = txtdrdUniName.Text;
            dr["CitName"] = txtCity.Text;
            dr["CitId"] = DBNull.Value;
            if (txtAvg.Text != "")
                dr["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                dr["NumUnit"] = int.Parse(txtNumUnit.Text);
            dr["StartDate"] = txtStartDate.Text;
            dr["EndDate"] = txtEndDate.Text;
            dr["IsConfirm"] = 0;
            dr["IsInquiry"] = 0;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["Thesis"] = txtThesis.Text;
            dr["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = Tableid;

            if (Session["License"] != null)
            {
                dr["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ImageUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }

            if (Session["ScoresImage"] != null)
            {
                dr["ScoresImageURL"] = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.NavigateUrl = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.ClientVisible = true;
                Session["ScoresImage"] = null;
            }

            if (Session["EquivalentImage"] != null)
            {
                dr["EquivalentImageURL"] = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ImageUrl = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ClientVisible = true;
                Session["EquivalentImage"] = null;
            }
            if (Session["EntranceExamConfImageURL"] != null)
            {
                dr["EntranceExamConfImageURL"] = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ImageUrl = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ClientVisible = true;
                Session["EntranceExamConfImageURL"] = null;
            }

            LiManager.AddRow(dr);
            if (LiManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int MlId = int.Parse(LiManager[0]["MlId"].ToString());
            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, Tableid, UpdateTableType, "اضافه کردن مدرک تحصیلی", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }

         
            int MemberRequestId = _MReId;
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, trans);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, trans);       

            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            ViewState["IsEdited_MeMadrak"] = true;

            if (Session["MenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_MeId, Tableid);
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            _MLId = Convert.ToInt32(LiManager[0]["MlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
        }
        catch (Exception err)
        {
            trans.CancelSave();
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

    protected void InsertMeTempLi()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberLicenceManager LiManager2 = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(LiManager);
        trans.Add(LiManager2);
        trans.Add(WorkFlowStateManager);

        bool IsAttach = false;

        try
        {
            trans.BeginSave();

            int Tableid = _MReId;

            LiManager2.FindByTMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == drdMajor.Value.ToString()) && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            DataRow dr = LiManager.NewRow();

            if (drdLicence.Value != null)
                dr["LiId"] = int.Parse(drdLicence.Value.ToString());
            if (ComboCountry.Value != null)
                dr["CounId"] = int.Parse(ComboCountry.Value.ToString());
            else
                dr["CounId"] = DBNull.Value;
            dr["TMeId"] = _MeId;
            if (drdMajor.Value != null)
                dr["MjId"] = int.Parse(drdMajor.Value.ToString());
            if (HiddenFieldUniValue.Get("Id") != null)
                dr["UnId"] = Convert.ToInt32(HiddenFieldUniValue.Get("Id"));
            dr["UnName"] = txtdrdUniName.Text;
            dr["CitName"] = txtCity.Text;
            dr["CitId"] = DBNull.Value;
            if (txtAvg.Text != "")
                dr["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                dr["NumUnit"] = int.Parse(txtNumUnit.Text);
            dr["StartDate"] = txtStartDate.Text;
            dr["EndDate"] = txtEndDate.Text;
            dr["IsConfirm"] = 0;
            dr["IsInquiry"] = 0;
            if (Session["License"] != null)
            {
                dr["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                HpLicense.ImageUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                Session["License"] = null;
                //IsAttach = true;
            }
            else
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["Thesis"] = txtThesis.Text;
            dr["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = Tableid;

            LiManager.AddRow(dr);
            int cnt = LiManager.Save();
            if (cnt <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int MlId = int.Parse(LiManager[0]["TMlId"].ToString());
            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, Tableid, UpdateTableType, "اضافه کردن مدرک تحصیلی", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            ViewState["IsEdited_MeMadrak"] = true;
            trans.EndSave();


            if (Session["MenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_MeId, Tableid);
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }

            TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            _MLId = Convert.ToInt32(LiManager[0]["TMlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";

        }
        catch (Exception err)
        {
            trans.CancelSave();
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

        //if (IsAttach)
        //{
        //    try
        //    {
        //        string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["License"].ToString());
        //        string ImgTarget = Server.MapPath("~/Image/Members/License/") + Path.GetFileName(Session["License"].ToString());
        //        File.Copy(ImgSoource, ImgTarget, true);
        //        File.Delete(ImgSoource);
        //        HpLicense.ClientVisible = true;
        //        HpLicense.NavigateUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
        //        Session["License"] = null;                
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
    }

    protected void InsertAndInActive(int MLId)
    {
        if (IsPageRefresh)
            return;
        #region DefineManagers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.RequestInActivesManager InActivesManager = new TSP.DataManager.RequestInActivesManager();

        trans.Add(LiManager);
        trans.Add(LiManager2);
        trans.Add(WorkFlowStateManager);
        trans.Add(InActivesManager);
        trans.Add(MemberRequestManager);
        #endregion

        //  bool IsAttach = false;

        try
        {
            trans.BeginSave();
            LiManager2.FindByMeId(_MeId);
            if (LiManager2.Count > 0)
            {

                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true)//&& Convert.ToInt32(LiManager2[i]["MlId"]) != MLId)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                            LiManager2.DataTable.AcceptChanges();
                        }
                    }
                }
            }
            InsertInActive(InActivesManager, MLId, _MReId);

            DataRow dr = LiManager.NewRow();

            if (drdLicence.Value != null)
                dr["LiId"] = int.Parse(drdLicence.Value.ToString());
            if (ComboCountry.Value != null)
                dr["CounId"] = int.Parse(ComboCountry.Value.ToString());
            else
                dr["CounId"] = DBNull.Value;
            dr["MeId"] = _MeId;
            if (drdMajor.Value != null)
                dr["MjId"] = int.Parse(drdMajor.Value.ToString());
            if (HiddenFieldUniValue.Get("Id") != null)
                dr["UnId"] = Convert.ToInt32(HiddenFieldUniValue.Get("Id"));
            dr["UnName"] = txtdrdUniName.Text;
            dr["CitName"] = txtCity.Text;
            dr["CitId"] = DBNull.Value;
            if (txtAvg.Text != "")
                dr["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                dr["NumUnit"] = int.Parse(txtNumUnit.Text);
            dr["StartDate"] = txtStartDate.Text;
            dr["EndDate"] = txtEndDate.Text;
            dr["IsConfirm"] = 0;
            dr["IsInquiry"] = 0;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["Thesis"] = txtThesis.Text;
            dr["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = _MReId;

            if (cmbConfirm.SelectedIndex == 1 || cmbConfirm.SelectedIndex == 2 || cmbConfirm.SelectedIndex == 3)
            {
                dr["IsConfirm"] = cmbConfirm.Value;// (cmbConfirm.SelectedIndex == 1) ? 1 : 2;
                dr["LetterNo"] = "";
                dr["LetterDate"] = "";
            }
            else
                dr["IsConfirm"] = 0;

            if (ChbEstelam.Checked == true)
            {

                dr["IsInquiry"] = 1;
                dr["InquiryNo"] = "";
                dr["InquiryDate"] = "";
                dr["InquerySaveDate"] = Utility.GetDateOfToday();
                if (Session["LicenseInquery"] != null)
                {
                    dr["InquiryImageURL"] = Session["LicenseInquery"].ToString();
                    InquiryImageLink.ClientVisible = true;
                    InquiryImageLink.ImageUrl = Session["LicenseInquery"].ToString();
                    Session["LicenseInquery"] = null;
                }
                else if (Utility.IsDBNullOrNullValue(InquiryImageLink.ImageUrl))
                {
                    trans.CancelSave();
                    ShowMessage("تصویر استعلام مدرک تحصیلی را انتخاب نمایید");
                    return;
                }
                else
                {
                    dr["InquiryImageURL"] = InquiryImageLink.ImageUrl;
                    InquiryImageLink.ClientVisible = true;
                }

            }
            else
                dr["IsInquiry"] = 0;

            if (Session["License"] != null)
            {
                dr["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ImageUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else if (Utility.IsDBNullOrNullValue(HpLicense.ImageUrl))
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }
            else
            {
                dr["ImageURL"] = HpLicense.ImageUrl;
                HpLicense.ClientVisible = true;
            }

            if (Session["ScoresImage"] != null)
            {
                dr["ScoresImageURL"] = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.NavigateUrl = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.ClientVisible = true;
                Session["ScoresImage"] = null;
            }

            if (Session["EquivalentImage"] != null)
            {
                dr["EquivalentImageURL"] = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ImageUrl = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ClientVisible = true;
                Session["EquivalentImage"] = null;
            }
            if (Session["EntranceExamConfImageURL"] != null)
            {
                dr["EntranceExamConfImageURL"] = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ImageUrl = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ClientVisible = true;
                Session["EntranceExamConfImageURL"] = null;
            }

            LiManager.AddRow(dr);
            if (LiManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int MlId = int.Parse(LiManager[0]["MlId"].ToString());
            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, _MReId, UpdateTableType, "غیرفعال و افزودن مدرک تحصیلی", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }

            int MemberRequestId = _MReId;
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, trans);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, trans);

            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            ViewState["IsEdited_MeMadrak"] = true;

            if (Session["MenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_MeId, _MReId);
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            _MLId = Convert.ToInt32(LiManager[0]["MlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
        }
        catch (Exception err)
        {
            trans.CancelSave();
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

    protected void InsertInActive(TSP.DataManager.RequestInActivesManager Manager, int MlId, int MReId)
    {
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MlId;
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberLicence);
        dr["ReqId"] = MReId;
        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();
    }

    protected void Edit(int MlId)
    {
        if (IsPageRefresh)
            return;
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();

        trans.Add(MemberRequestManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(LiManager);
        trans.Add(LiManager2);
        #endregion
        // bool IsAttach = false;
        try
        {
            trans.BeginSave();

            LiManager2.FindByMeId(_MeId);
            if (LiManager2.Count > 0)
            {

                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true && Convert.ToInt32(LiManager2[i]["MlId"]) != MlId)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                            LiManager2.DataTable.AcceptChanges();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == drdMajor.Value.ToString()) && Convert.ToInt32(LiManager2[i]["MlId"]) != MlId && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            LiManager.FindByCode(MlId);
            LiManager[0].BeginEdit();
            if (drdLicence.Value != null)
                LiManager[0]["LiId"] = int.Parse(drdLicence.Value.ToString());

            LiManager[0]["MeId"] = _MeId;
            if (drdMajor.Value != null)
                LiManager[0]["MjId"] = int.Parse(drdMajor.Value.ToString());
            if (HiddenFieldUniValue.Get("Id") != null)
            {
                LiManager[0]["UnId"] = Convert.ToInt32(HiddenFieldUniValue.Get("Id"));
                LiManager[0]["UnName"] = txtdrdUniName.Text;
            }
            else
                LiManager[0]["UnName"] = txtUniName.Text;

            LiManager[0]["Thesis"] = txtThesis.Text;
            LiManager[0]["CitName"] = txtCity.Text;
            if (txtAvg.Text != "")
                LiManager[0]["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                LiManager[0]["NumUnit"] = int.Parse(txtNumUnit.Text);
            LiManager[0]["StartDate"] = txtStartDate.Text;
            LiManager[0]["EndDate"] = txtEndDate.Text;
            LiManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            LiManager[0]["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            LiManager[0]["Description"] = txtDescription.Text;
            if (Session["License"] != null)
            {
                LiManager[0]["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ImageUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else if (Utility.IsDBNullOrNullValue(HpLicense.ImageUrl))
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }

            if (Session["ScoresImage"] != null)
            {
                LiManager[0]["ScoresImageURL"] = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.NavigateUrl = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.ClientVisible = true;
                Session["ScoresImage"] = null;
            }

            if (Session["EquivalentImage"] != null)
            {
                LiManager[0]["EquivalentImageURL"] = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ImageUrl = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ClientVisible = true;
                Session["EquivalentImage"] = null;
            }
            if (Session["EntranceExamConfImageURL"] != null)
            {
                LiManager[0]["EntranceExamConfImageURL"] = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ImageUrl = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ClientVisible = true;
                Session["EntranceExamConfImageURL"] = null;
            }
            /////
            if (ChbEstelam.Checked == true)
            {

                LiManager[0]["IsInquiry"] = 1;
                LiManager[0]["InquiryNo"] = "";
                LiManager[0]["InquiryDate"] = "";
                LiManager[0]["InquerySaveDate"] = Utility.GetDateOfToday();
            }
            else
                LiManager[0]["IsInquiry"] = 0;
            if (Session["LicenseInquery"] != null)
            {
                LiManager[0]["InquiryImageURL"] = Session["LicenseInquery"].ToString();
                InquiryImageLink.ClientVisible = true;
                InquiryImageLink.ImageUrl = Session["LicenseInquery"].ToString();
                Session["LicenseInquery"] = null;
            }
            /////
            LiManager[0]["ModifiedDate"] = DateTime.Now;

            LiManager[0].EndEdit();
            if (LiManager.Save() != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int Tableid = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, Tableid, UpdateTableType, "ویرایش مدرک تحصیلی", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }
            int MemberRequestId = _MReId;
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, trans);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, trans);

            trans.EndSave();
            _MLId = Convert.ToInt32(LiManager[0]["MlId"].ToString());
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            ViewState["IsEdited_MeMadrak"] = true;



        }

        catch (Exception err)
        {
            trans.CancelSave();
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

    protected void EditTempMeLi(int TMlId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberLicenceManager LiManager2 = new TSP.DataManager.TempMemberLicenceManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(LiManager);
        trans.Add(LiManager2);

        bool IsAttach = false;
        try
        {
            trans.BeginSave();
            LiManager2.FindByTMeId(_MeId);
            if (LiManager2.Count > 0)
            {

                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true && Convert.ToInt32(LiManager2[i]["TMlId"]) != TMlId)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == drdMajor.Value.ToString()) && Convert.ToInt32(LiManager2[i]["TMlId"]) != TMlId && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            LiManager.FindByCode(TMlId);
            LiManager[0].BeginEdit();
            if (drdLicence.Value != null)
                LiManager[0]["LiId"] = int.Parse(drdLicence.Value.ToString());

            LiManager[0]["TMeId"] = _MeId;
            if (drdMajor.Value != null)
                LiManager[0]["MjId"] = int.Parse(drdMajor.Value.ToString());

            if (HiddenFieldUniValue.Get("Id") != null)
            {
                LiManager[0]["UnId"] = Convert.ToInt32(HiddenFieldUniValue.Get("Id"));
                LiManager[0]["UnName"] = txtdrdUniName.Text;
            }
            else
                LiManager[0]["UnName"] = txtUniName.Text;

            LiManager[0]["CitName"] = txtCity.Text;
            if (txtAvg.Text != "")
                LiManager[0]["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                LiManager[0]["NumUnit"] = int.Parse(txtNumUnit.Text);
            LiManager[0]["StartDate"] = txtStartDate.Text;
            LiManager[0]["EndDate"] = txtEndDate.Text;

            LiManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            LiManager[0]["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);
            LiManager[0]["Thesis"] = txtThesis.Text;
            LiManager[0]["Description"] = txtDescription.Text;
            LiManager[0]["ModifiedDate"] = DateTime.Now;
            if (Session["License"] != null)
            {
                LiManager[0]["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                HpLicense.ImageUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                Session["License"] = null;
            }
            else if (Utility.IsDBNullOrNullValue(HpLicense.ImageUrl))
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }

            LiManager[0].EndEdit();
            if (LiManager.Save() != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int Tableid = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, Tableid, UpdateTableType, "ویرایش مدرک تحصیلی", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }

            trans.EndSave();
            _MLId = Convert.ToInt32(LiManager[0]["TMlId"].ToString());
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            ViewState["IsEdited_MeMadrak"] = true;
        }
        catch (Exception err)
        {
            trans.CancelSave();
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

    protected void EditConfirming(int MlId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        trans.Add(LiManager);
        if (Session["LicenseInquery"] == null)
        {
            SetMessage("تصویر استعلام بارگذاری نشده است");
            return;
        }
        try
        {
            trans.BeginSave();
            LiManager.FindByCode(MlId);
            LiManager[0].BeginEdit();

            if (cmbConfirm.SelectedIndex == 1 || cmbConfirm.SelectedIndex == 2 || cmbConfirm.SelectedIndex == 3)
            {
                LiManager[0]["IsConfirm"] = cmbConfirm.Value;
                LiManager[0]["LetterNo"] = "";
                LiManager[0]["LetterDate"] = "";
            }
            else
                LiManager[0]["IsConfirm"] = 0;

            if (ChbEstelam.Checked == true)
            {

                LiManager[0]["IsInquiry"] = 1;
                LiManager[0]["InquiryNo"] = "";
                LiManager[0]["InquiryDate"] = "";
                LiManager[0]["InquerySaveDate"] = Utility.GetDateOfToday();
            }
            else
                LiManager[0]["IsInquiry"] = 0;
            if (Session["LicenseInquery"] != null)
            {
                LiManager[0]["InquiryImageURL"] = Session["LicenseInquery"].ToString();
                InquiryImageLink.ClientVisible = true;
                InquiryImageLink.ImageUrl = Session["LicenseInquery"].ToString();
                Session["LicenseInquery"] = null;
            }
            else
            {
                trans.CancelSave();
                ShowMessage("تصویراستعلام مدرک تحصیلی را انتخاب نمایید");
                return;
            }
            if (Session["ScoresImage"] != null)
            {
                LiManager[0]["ScoresImageURL"] = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.NavigateUrl = "~/Image/Members/Scores/" + Path.GetFileName(Session["ScoresImage"].ToString());
                HyperLinkScore.ClientVisible = true;
                Session["ScoresImage"] = null;
            }

            if (Session["EquivalentImage"] != null)
            {
                LiManager[0]["EquivalentImageURL"] = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ImageUrl = "~/Image/Members/Equivalent/" + Path.GetFileName(Session["EquivalentImage"].ToString());
                HyperLinkEquivalent.ClientVisible = true;
                Session["EquivalentImage"] = null;
            }
            if (Session["EntranceExamConfImageURL"] != null)
            {
                LiManager[0]["EntranceExamConfImageURL"] = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ImageUrl = "~/Image/Members/EntranceExamConfirmation/" + Path.GetFileName(Session["EntranceExamConfImageURL"].ToString());
                HyperLinkEntranceExamConf.ClientVisible = true;
                Session["EntranceExamConfImageURL"] = null;
            }

            LiManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            LiManager[0].EndEdit();
            LiManager.Save();
            _PageMode = "Confirming";
            ASPxRoundPanel2.HeaderText = "استعلام و تایید مدرک";
            SetMessage("ذخیره انجام شد");
            trans.EndSave();
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در ذخیره انجام گرفته است");
        }
    }
    #endregion

    int FindMaxLiId(int NewValue, TSP.DataManager.MemberLicenceManager LiManager)
    {
        //= new TSP.DataManager.MemberLicenceManager();
        LiManager.FindByMeId(_MeId);

        int Max = NewValue;
        for (int i = 0; i < LiManager.Count; i++)
        {
            //DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
            if (Max < int.Parse(LiManager[i]["LiId"].ToString()))
                Max = int.Parse(LiManager[i]["LiId"].ToString());
        }
        return Max;
    }

    int FindMaxLiIdForTempMe(int NewValue, TSP.DataManager.TempMemberLicenceManager LiManager)
    {
        //= new TSP.DataManager.MemberLicenceManager();
        LiManager.FindByTMeId(_MeId);

        int Max = NewValue;
        for (int i = 0; i < LiManager.Count; i++)
        {
            //DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
            if (Max < int.Parse(LiManager[i]["LiId"].ToString()))
                Max = int.Parse(LiManager[i]["LiId"].ToString());
        }
        return Max;
    }

    protected void FillForm(int MlId)
    {
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        LiManager.FindByCode(MlId);
        if (LiManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            return;
        }
        //txtAvg.Text = LiManager[0]["Avg"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Avg"]))
            txtAvg.Text = Convert.ToDecimal(LiManager[0]["Avg"]).ToString("##.00");
        txtDescription.Text = LiManager[0]["Description"].ToString();
        txtEndDate.Text = LiManager[0]["EndDate"].ToString();
        txtNumUnit.Text = LiManager[0]["NumUnit"].ToString();
        txtStartDate.Text = LiManager[0]["StartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Thesis"]))
            txtThesis.Text = LiManager[0]["Thesis"].ToString();
        //txtCity.Text = LiManager[0]["CitName"].ToString();       

        drdLicence.DataBind();
        drdLicence.SelectedIndex = drdLicence.Items.IndexOfValue(LiManager[0]["LiId"].ToString());
        drdMajor.DataBind();
        drdMajor.SelectedIndex = drdMajor.Items.IndexOfValue(LiManager[0]["MjId"].ToString());
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CounId"]))
        {
            ComboCountry.DataBind();
            ComboCountry.SelectedIndex = ComboCountry.Items.IndexOfValue(LiManager[0]["CounId"].ToString());
            ODBCity.SelectParameters[0].DefaultValue = ComboCountry.Value.ToString();
        }
        else
            ComboCountry.SelectedIndex = -1;

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["UnId"]))
        {
            txtdrdUniName.Text = LiManager[0]["UnName"].ToString();
            HiddenFieldUniValue["Id"] = LiManager[0]["UnId"];
        }
        else
        {
            lblUniwar.Visible = true;
            Label4.Visible = true;
            txtUniName.Visible = true;
            txtUniName.Text = LiManager[0]["UnName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CitName"]))
            txtCity.Text = LiManager[0]["CitName"].ToString();

        if (int.Parse(LiManager[0]["IsInquiry"].ToString()) == 1)
        {
            PanelInQuiry.Visible = true;
            ChbEstelam.Checked = true;
            if (!Utility.IsDBNullOrNullValue(LiManager[0]["InquiryImageURL"]))
            {
                InquiryImageLink.ClientVisible = true;
                InquiryImageLink.ImageUrl = LiManager[0]["InquiryImageURL"].ToString();

            }
            if (!Utility.IsDBNullOrNullValue(LiManager[0]["IsConfirm"].ToString()))
                cmbConfirm.SelectedIndex = Convert.ToInt32(LiManager[0]["IsConfirm"]);
            else
                cmbConfirm.SelectedIndex = -1;
        }
        else
        {
            PanelInQuiry.Visible = false;
            cmbConfirm.SelectedIndex = -1;
        }

        if (Convert.ToBoolean(LiManager[0]["DefaultValue"]))
            cmbLicenceType.SelectedIndex = 0;
        else
            cmbLicenceType.SelectedIndex = 1;


        if (!Utility.IsDBNullOrNullValue(LiManager[0]["ImageURL"]))
        {
            HpLicense.ClientVisible = true;

            HpLicense.ImageUrl = LiManager[0]["ImageURL"].ToString();
            HDFlpLicense.Set("name", 1);
        }
        else
        {
            HpLicense.ClientVisible = false;
            HDFlpLicense.Set("name", 0);
        }
        ///////************bbbbbbbbbbbbb
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["ScoresImageURL"]))
        {
            HyperLinkScore.ClientVisible = true;
            HyperLinkScore.NavigateUrl = LiManager[0]["ScoresImageURL"].ToString();
            HDFlpLicense.Set("Score", 1);
        }
        else
        {
            HyperLinkScore.ClientVisible = false;
            HDFlpLicense.Set("Score", 0);
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["EntranceExamConfImageURL"]))
        {
            HyperLinkEntranceExamConf.ClientVisible = true;
            HyperLinkEntranceExamConf.ImageUrl = LiManager[0]["EntranceExamConfImageURL"].ToString();
            HDFlpLicense.Set("EntranceExamConfImageURL", 1);
        }
        else
        {
            HyperLinkEntranceExamConf.ClientVisible = false;
            HDFlpLicense.Set("EntranceExamConfImageURL", 0);
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["EquivalentImageURL"]))
        {
            HyperLinkEquivalent.ClientVisible = true;

            HyperLinkEquivalent.ImageUrl = LiManager[0]["EquivalentImageURL"].ToString();
            HDFlpLicense.Set("Equivalent", 1);
        }
        else
        {
            HyperLinkEquivalent.ClientVisible = false;
            HDFlpLicense.Set("Equivalent", 0);
        }
        //////*********eeeeeeeeeee

    }

    protected void FillFormTempMeLi(int TMlId)
    {
        PanelInQuiry.Visible = false;
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();

        LiManager.FindByCode(TMlId);
        if (LiManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            return;
        }
        //txtAvg.Text = LiManager[0]["Avg"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Avg"]))
            txtAvg.Text = Convert.ToDecimal(LiManager[0]["Avg"]).ToString("##.00");
        txtDescription.Text = LiManager[0]["Description"].ToString();
        txtEndDate.Text = LiManager[0]["EndDate"].ToString();
        txtNumUnit.Text = LiManager[0]["NumUnit"].ToString();
        txtStartDate.Text = LiManager[0]["StartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Thesis"]))
            txtThesis.Text = LiManager[0]["Thesis"].ToString();
        //txtCity.Text = LiManager[0]["CitName"].ToString();


        drdLicence.DataBind();
        drdLicence.SelectedIndex = drdLicence.Items.IndexOfValue(LiManager[0]["LiId"].ToString());
        drdMajor.DataBind();
        drdMajor.SelectedIndex = drdMajor.Items.IndexOfValue(LiManager[0]["MjId"].ToString());
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CounId"]))
        {
            ComboCountry.DataBind();
            ComboCountry.SelectedIndex = ComboCountry.Items.IndexOfValue(LiManager[0]["CounId"].ToString());
            ODBCity.SelectParameters[0].DefaultValue = ComboCountry.Value.ToString();
        }
        else
            ComboCountry.SelectedIndex = -1;

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["UnId"]))
        {
            txtdrdUniName.Text = LiManager[0]["UnName"].ToString();
            HiddenFieldUniValue["Id"] = LiManager[0]["UnId"];
        }
        else
        {
            lblUniwar.Visible = true;
            Label4.Visible = true;
            txtUniName.Visible = true;
            txtUniName.Text = LiManager[0]["UnName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CitName"]))
        {
            txtCity.Text = LiManager[0]["CitName"].ToString();
        }

        if (Convert.ToBoolean(LiManager[0]["DefaultValue"]))
            cmbLicenceType.SelectedIndex = 0;
        else
            cmbLicenceType.SelectedIndex = 1;


        if (!Utility.IsDBNullOrNullValue(LiManager[0]["ImageURL"]))
        {
            HpLicense.ClientVisible = true;
            HpLicense.ImageUrl = LiManager[0]["ImageURL"].ToString();
            HDFlpLicense.Set("name", 1);
        }
        else
        {
            HpLicense.ClientVisible = false;
            HDFlpLicense.Set("name", 0);
        }

        //AttachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberLicence, TMlId, (short)TSP.DataManager.AttachType.MemberLicense);
        //if (AttachManager.Count > 0)
        //{
        //    HpLicense.ClientVisible = true;
        //    HpLicense.NavigateUrl = AttachManager[0]["FilePath"].ToString();
        //    HDFlpLicense.Set("name", 1);
        //}
        //else
        //{
        //    HpLicense.ClientVisible = false;

        //}

    }

    #region WF Permission
    private void CheckWorkFlowPermission()
    {
        if (_PageMode != "New")
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int Permission = -1; int PermissionTransferMe = -1;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        int Permission1 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, Utility.GetCurrentUser_UserId());
        PermissionTransferMe = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission1 > 0 || PermissionTransferMe > 0)
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
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1; int PermissionTransferMe = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _MReId, TaskCode, Utility.GetCurrentUser_UserId());
        int Permisssion1 = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _MReId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, Utility.GetCurrentUser_UserId());

        PermissionTransferMe = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _MReId, (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion1 > 0 || PermissionTransferMe > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                case "EditInActive":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    #endregion

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember)
            {
                return true;
            }
        }

        return false;

    }

    protected void FillUniversity(string country, String UnName)
    {
        if (string.IsNullOrEmpty(country)) return;
        ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = country;
        ObjectDataSourceSearchUniversity.SelectParameters["UnName"].DefaultValue = UnName;
        CustomAspxDevGridView1.DataBind();
    }

    protected string SaveImage(UploadedFile uploadedFile, string ImageUploadType)
    {
        string ret = "";
        string tempFileName = "";
        if (uploadedFile.IsValid)
        {
            switch (ImageUploadType)
            {
                case "InquiryImage":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);


                        ret = _MeId.ToString() + "Inq" + _MReId.ToString() + "Inq" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/Members/LicenseInquery/") + ret) == true);
                    tempFileName = "~/image/Members/LicenseInquery/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["LicenseInquery"] = tempFileName;
                    #endregion
                    break;
                case "Image":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _MeId.ToString() + "lic" + _MReId.ToString() + "lic" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/Members/License/") + ret) == true);// || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
                    tempFileName = MapPath("~/Image/Members/License/") + ret;
                    uploadedFile.SaveAs(tempFileName, true);
                    // Session["ExPlaceUpload"] = tempFileName;
                    Session["License"] = tempFileName;
                    #endregion
                    break;
                case "ScoreImage":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _MeId.ToString() + "Scr" + _MReId.ToString() + "Scr" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/Members/Scores/") + ret) == true);
                    tempFileName = MapPath("~/image/Members/Scores/") + ret;
                    uploadedFile.SaveAs(tempFileName, true);
                    Session["ScoresImage"] = tempFileName;
                    #endregion
                    break;
                case "Equivalent":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _MeId.ToString() + "Eqv" + _MReId.ToString() + "Eqv" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/Members/Equivalent/") + ret) == true);
                    tempFileName = MapPath("~/image/Members/Equivalent/") + ret;
                    uploadedFile.SaveAs(tempFileName, true);
                    Session["EquivalentImage"] = tempFileName;
                    #endregion
                    break;

                case "EntranceExamConfImageURL":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _MeId.ToString() + "Ent" + _MReId.ToString() + "Ent" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/Members/EntranceExamConfirmation/") + ret) == true);
                    tempFileName = MapPath("~/image/Members/EntranceExamConfirmation/") + ret;
                    uploadedFile.SaveAs(tempFileName, true);
                    Session["EntranceExamConfImageURL"] = tempFileName;
                    #endregion
                    break;
            }
        }
        return ret;
    }

    protected void ShowImageInReloadPage()
    {
        if (!Utility.IsDBNullOrNullValue(Session["LicenseInquery"]))
        {
            ImageInquiryImageUploded.ClientVisible = true;
            InquiryImageLink.ClientVisible = true;

            InquiryImageLink.ImageUrl = Session["LicenseInquery"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(Session["License"]))
        {
            imgEndUploadImgIdNo.ClientVisible = true;
            HpLicense.ClientVisible = true;
            HpLicense.ImageUrl = Session["License"].ToString();

        }
        if (!Utility.IsDBNullOrNullValue(Session["ScoresImage"]))
        {
            imgEndUploadImgClientScore.ClientVisible = true;
            HyperLinkScore.ClientVisible = true;
            HyperLinkScore.NavigateUrl = Session["ScoresImage"].ToString();
            ///it is maybe pdf file so it does not have ImageUrl 
        }
        if (!Utility.IsDBNullOrNullValue(Session["EntranceExamConfImageURL"]))
        {
            imgEndUploadImgClientEntranceExamConfImage.ClientVisible = true;
            HyperLinkEntranceExamConf.ClientVisible = true;
            HyperLinkEntranceExamConf.ImageUrl = Session["EntranceExamConfImageURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["EquivalentImage"]))
        {
            imgEndUploadImgClientEquivalent.ClientVisible = true;
            HyperLinkEquivalent.ClientVisible = true;
            HyperLinkEquivalent.ImageUrl = Session["EquivalentImage"].ToString();
        }
    }

    protected void ClearImageUrl()
    {
        HpLicense.ClientVisible = false;
        HpLicense.ImageUrl = "";
        Session["License"] = null;

        InquiryImageLink.ClientVisible = false;
        InquiryImageLink.ImageUrl = "";
        Session["LicenseInquery"] = null;

        HyperLinkScore.ClientVisible = false;
        HyperLinkScore.NavigateUrl = "";
        Session["ScoresImage"] = null;

        HyperLinkEntranceExamConf.ClientVisible = false;
        HyperLinkEntranceExamConf.ImageUrl = "";
        Session["EntranceExamConfImageURL"] = null;

        HyperLinkEquivalent.ClientVisible = false;
        HyperLinkEquivalent.ImageUrl = "";
        Session["EquivalentImage"] = null;
    }

    private int CheckLetterValidationAndFill(string LetterNo)
    {
        int LetterId = -1;
        TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();
        LettersManager.FindByLetterNumber(LetterNo);
        if (LettersManager.Count > 0)
        {
            LetterId = int.Parse(LettersManager[0]["LetterId"].ToString());
        }
        return LetterId;
    }

    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Attachment
        arr.Add(0);//arr[5]-->MainInfo

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[1] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    private void FillMemberName()
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        MemberInfoUserControl.MeId = Convert.ToInt32(_MeId);
        MemberInfoUserControl.MReId = _MReId;
        if (Mode == "TempMe")
        {
            MemberInfoUserControl.IsMeTemp = true;
        }
    }

    private void SetMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion
}
