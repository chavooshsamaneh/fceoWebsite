using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;
using System.Drawing;

public partial class Employee_OfficeRegister_OfficeInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    DataTable dtOfImg = null;

    #region Properties
    string _PageMode
    {
        set
        {
            HiddenFieldOffice["PageMode"] = value;
        }
        get
        {
            return HiddenFieldOffice["PageMode"].ToString();
        }
    }

    int _OfId
    {
        set
        {
            HiddenFieldOffice["OfId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldOffice["OfId"]);
        }
    }

    int _OfReId
    {
        set
        {
            HiddenFieldOffice["OfReId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldOffice["OfReId"]);
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {      
        #region Refresh
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
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
        {
            Response.Redirect("Office.aspx");
            return;
        }

        if (!IsPostBack)
        {
            ResetSession();
            #region Set DataTable
            if (Session["TblOfReImg"] == null)
            {
                dtOfImg = new DataTable();
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns.Add("Description");
                dtOfImg.Columns.Add("Id");
                dtOfImg.Columns["Id"].AutoIncrement = true;
                dtOfImg.Columns["Id"].AutoIncrementSeed = 1;

                Session["TblOfReImg"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfReImg"];

            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
            #endregion

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

    #region Buttons Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        imgOfArm.ClientVisible = false;
        imgOfSign.ClientVisible = false;

        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave2.Enabled = btnSave.Enabled = per.CanNew;

        _OfId = -1;
        _PageMode = "New";
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;

        ClearForm();
        SetEnable(true);
        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (_OfReId == -1 || string.IsNullOrEmpty(_OfReId.ToString()) || _OfId == -1 || string.IsNullOrEmpty(_OfId.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                if (!CheckPermitionForEdit(_OfReId))
                {
                    ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.");
                    return;
                }
                SetEnable(true);
                imgOfArm.ClientVisible = true;
                imgOfSign.ClientVisible = true;

                TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = per.CanEdit;

                _PageMode = "Edit";

                MenuDetails.Enabled = true;
                RoundPanelOffice.HeaderText = "ویرایش";
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (_PageMode != "New")
        {
            if (_OfId == -1 || string.IsNullOrEmpty(_OfId.ToString()))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            if (_PageMode == "View" && _PageMode == "Edit")
            {
                if (_OfReId == -1 || string.IsNullOrEmpty(_OfReId.ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
            }
        }

        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Edit(_OfId, _OfReId);
                break;
            case "ChangeBaseInfo":
                InsertMembershipRequest(_OfId, (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo);
                break;
            case "ChangeShareHolderAndBaseInfo":
                InsertMembershipRequest(_OfId, (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo);
                break;
            case "NewReqMembership":
                InsertMembershipRequest(_OfId, (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset);
                break;
            case "InValid":
                InsertMembershipRequest(_OfId, (int)TSP.DataManager.OfficeRequestType.Invalid);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Session["TblOfReImg"] = null;
        //Session["MeReqUpload"] = null;
        //Session["FileOfArm2"] = null;
        //Session["FileOfSign2"] = null;
        ResetSession();
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _OfId != -1 && !string.IsNullOrEmpty(_OfId.ToString()) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Office.aspx?PostId=" + Utility.EncryptQS(_OfId.ToString()) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
            Response.Redirect("Office.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {
        if (Session["TblOfReImg"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfReImg"];

            DataRow dr = dtOfImg.NewRow();

            try
            {
                if (Session["MeReqUpload"] != null)
                {

                    dr[0] = "~/Image/Office/OffRequest/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[2] = Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[1] = "~/Image/temp/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[5] = txtDescImg.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }


                dr[3] = 0;
                dtOfImg.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();

                Session["MeReqUpload"] = null;

                txtDescImg.Text = "";
                ASPxImage2.ClientVisible = false;


            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }

    }
    #endregion

    protected void MenuDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;

        if (_OfReId == -1 || string.IsNullOrEmpty(_OfReId.ToString()))
        {
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByOfficeId(_OfId, 0, 0);
            if (ReqManager.Count > 0)
                _OfReId = Convert.ToInt32(ReqManager[0]["OfReId"]);//درخواست "ثبت اولیه" برای ثبت اطلاعات
            else
            {
                ReqManager.FindByOfficeId(_OfId, -1, -1);
                if (ReqManager.Count > 0)
                    _OfReId = Convert.ToInt32(ReqManager[0]["OfReId"]);
            }
        }

        switch (e.Item.Name)
        {

            case "Agent":

                Response.Redirect("OfficeAgent.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));

                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));


                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));


                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));


                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt="
                    + Utility.EncryptQS("MemberShip"));


                break;

            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));

                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("MemberShip"));

                break;
        }

    }

    #region FileUploads

    protected void flp_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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

    protected void flpOfArm_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageArm(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpOfSign_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion
    #endregion

    #region Methods
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #region Set Mode-Key
    private void SetKey()
    {
        try
        {
            _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
            _OfId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfId"]).ToString()));
            _OfReId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfReId"]).ToString()));

            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            //TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
            //ASPxTextBoxAmount.Text = GetFirstMembershipCost(CostSettingsManager).ToString("#,#");            

            CheckPermission();
            SetMode();

            if (_OfReId == -1
                || string.IsNullOrEmpty(_OfReId.ToString()))
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
            else
            {
                TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                ReqManager.FindByCode(_OfReId);
                if (ReqManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["TaskName"]))
                        lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager[0]["TaskName"].ToString();
                    else
                        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
                }
                else
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
                }
            }

            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode()
    {
        switch (_PageMode)
        {
            case "View":
                SetViewMode();
                break;
            case "New"://صدور عضو حقوقی جدید
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
            case "NewReqMembership"://درخواست تغییرات عضویت
                SetNewReqMembershipMode();
                break;
            case "ChangeBaseInfo":
                SetChangeBaseInfoMode();
                break;
            case "ChangeShareHolderAndBaseInfo":
                SetChangeShareHolderAndBaseInfoMode();
                break;
                
            case "InValid":
                SetInValidMode();
                break;
        }
    }

    private void SetNewMode()
    {
        // RoundPanelAccounting.Visible = false;
        SetEnable(true);
        //RoundPanelOfficeGrade.Visible = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;
        ClearForm();

    }

    private void SetEditMode()
    {
        RoundPanelOffice.HeaderText = "ویرایش";
        SetEnable(true);

        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        if (_OfId == -1 || string.IsNullOrEmpty(_OfId.ToString()) || _OfReId == -1 || string.IsNullOrEmpty(_OfReId.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillRequest(_OfId, _OfReId);
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByCode(_OfReId);
        if (ReqManager.Count > 0)
        {
            if (ReqManager[0]["IsConfirm"].ToString() == "0")
                InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.OfficeRequest, _OfReId);

            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
                || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)
            {
                cmbActivityType.ClientVisible = false;
                lblActivityType.ClientVisible = false;
            }
        }
    }

    private void SetViewMode()
    {
        RoundPanelOffice.HeaderText = "مشاهده";
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        SetEnable(false);
        txtReRequestDesc.Enabled = false;
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;

        if (_OfId == -1 || string.IsNullOrEmpty(_OfId.ToString()) || _OfReId == -1 || string.IsNullOrEmpty(_OfReId.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        FillRequest(_OfId, _OfReId);

        ReqManager.FindByCode(_OfReId);
        if (ReqManager.Count <= 0)
            return;
        if (ReqManager[0]["IsConfirm"].ToString() == "0")
            InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.OfficeRequest, _OfReId);
        else//**************پاسخ داده شده   // || (!Convert.ToBoolean(ReqManager[0]["Requester"])))//FromMember
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }

        if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
            || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست تغییرات عضویت و یادرخواست اولیه
        {
            cmbActivityType.ClientVisible = false;
            lblActivityType.ClientVisible = false;
        }
        
    }

    private void SetNewReqMembershipMode()
    {
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        if (!FillForm(_OfId))
            return;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        RoundPanelOffice.HeaderText = "جدید _ درخواست تغییرات عضویت";        
        MenuDetails.Enabled = false;

        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;


        cmbActivityType.ClientVisible = false;
        lblActivityType.ClientVisible = false;
    }

    private void SetChangeBaseInfoMode()
    {
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        //  FillRequest(int.Parse(OfId), int.Parse(OfReId));
        if (!FillForm(_OfId))

            btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        RoundPanelOffice.HeaderText = "جدید _ درخواست تغییرات اطلاعات پایه";

        MenuDetails.Enabled = false;

        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;

        DisableForChangeBaseInfo();
    }

    private void SetChangeShareHolderAndBaseInfoMode()
    {
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        if (!FillForm(_OfId))

            btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید _ درخواست تغییرات اطلاعات پایه و سهامداران";

        MenuDetails.Enabled = false;

        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;

        DisableForChangeBaseInfo();
    }

    private void SetInValidMode()
    {
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;

        //FillRequest(int.Parse(OfId), int.Parse(OfReId));
        FillForm(_OfId);
        SetEnable(false);
        txtReRequestDesc.Enabled = true;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید _ درخواست ابطال عضویت حقوقی";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
    }
    #endregion

    private void CheckPermission()
    {

        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = btnSave2.Enabled = per.CanEdit || per.CanNew;

        if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
        {
            String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
            if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        else if (_PageMode != "New" && !per.CanView)
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());


    }

    #region Enable
    private void SetEnable(Boolean Enabled)
    {
        txtOfAddress.Enabled = Enabled;
        txtOfDesc.Enabled = Enabled;
        txtOfEmail.Enabled = Enabled;
        txtOfFax.Enabled = Enabled;
        txtOfFax_pre.Enabled = Enabled;
        txtOfMobile.Enabled = Enabled;
        txtOfName.Enabled = Enabled;
        txtOfNameEn.ClientEnabled = Enabled;
        txtOfRegDate.Enabled = Enabled;
        txtOfRegNo.Enabled = Enabled;
        txtOfRegPlace.Enabled = Enabled;
        txtOfStock.Enabled = Enabled;
        txtOfSubject.Enabled = Enabled;
        txtOfTel1.Enabled = Enabled;
        txtOfTel1_pre.Enabled = Enabled;
        txtOfTel2.Enabled = Enabled;
        txtOfTel2_pre.Enabled = Enabled;
        txtOfValue.Enabled = Enabled;
        txtOfWebsite.Enabled = Enabled;
        drdOfType.Enabled = Enabled;
        flpOfArm.ClientVisible = Enabled;
        flpOfSign.ClientVisible = Enabled;
        cmbMembershipRequstType.Enabled = Enabled;
        cmbActivityType.Enabled = Enabled;
        TblFile.Visible = Enabled;
        txtReRequestDesc.Enabled = Enabled;
        CheckBoxConditionalApprove.Enabled = Enabled;
    }

    protected void DisableForChangeBaseInfo()
    {
        // txtOfDesc.Enabled = false;
        txtOfName.Enabled = false;
        txtOfRegDate.Enabled = false;
        txtOfRegNo.Enabled = false;
        txtOfRegPlace.Enabled = false;
        drdOfType.Enabled = false;
        cmbActivityType.Enabled = false;
        cmbMembershipRequstType.Enabled = false;
    }
    #endregion

    private void ClearForm()
    {

        txtOfId.Text = "";
        txtMeNo.Text = "";
        txtOfAddress.Text = "";
        txtOfDesc.Text = "";
        txtOfEmail.Text = "";
        txtOfFax.Text = "";
        txtOfFax_pre.Text = "";
        txtOfMobile.Text = "";
        txtOfName.Text = "";
        txtOfNameEn.Text = "";
        txtOfRegDate.Text = "";
        txtOfRegNo.Text = "";
        txtOfRegPlace.Text = "";
        txtOfStock.Text = "";
        txtOfSubject.Text = "";
        txtOfTel1.Text = "";
        txtOfTel1_pre.Text = "";
        txtOfTel2.Text = "";
        txtOfTel2_pre.Text = "";
        txtOfValue.Text = "";
        txtOfWebsite.Text = "";
        cmbMembershipRequstType.SelectedIndex = 0;

        txtOfRegDate.Text = "";
        txtOfAddress.Text = "";
        txtOfDesc.Text = "";
        imgOfArm.ImageUrl = "";
        imgOfSign.ImageUrl = "";

        HDFlpArm.Set("name", "0");
        HDFlpSign.Set("name", "0");

        cmbActivityType.SelectedIndex = -1;
        txtReRequestDesc.Text = "";

        dtOfImg = (DataTable)Session["TblOfReImg"];
        dtOfImg.Rows.Clear();
        Session["TblOfReImg"] = dtOfImg;
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

        Session["OffMenuArrayList"] = null;
        System.Collections.ArrayList arr = new System.Collections.ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        Session["OffMenuArrayList"] = arr;

        for (int i = 0; i < MenuDetails.Items.Count; i++)
        {
            MenuDetails.Items[i].Image.Url = "";
        }

    }

    #region FillForm
    protected void FillRequest(int OfId, int OfReId)
    {
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        try
        {
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            RoundPanelOffice.HeaderText += " مشخصات درخواست _ درخواست " + ReqManager[0]["TypeName"].ToString();
            txtOfId.Text = ReqManager[0]["OfId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
                txtMeNo.Text = ReqManager[0]["MeNo"].ToString();
            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo
                || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
            {
                RoundPanelFileAttachment.Visible = true;
                RoundPanelFileAttachment.Enabled = false;
                DisableForChangeBaseInfo();
            }
            txtOfName.Text = ReqManager[0]["OfName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OfNameEn"]))
                txtOfNameEn.Text = ReqManager[0]["OfNameEn"].ToString();

            if (ReqManager[0]["Tel1"].ToString() != "")
            {
                if (ReqManager[0]["Tel1"].ToString().IndexOf("-") > 0)
                {
                    txtOfTel1_pre.Text = ReqManager[0]["Tel1"].ToString().Substring(0, ReqManager[0]["Tel1"].ToString().IndexOf("-"));
                    txtOfTel1.Text = ReqManager[0]["Tel1"].ToString().Substring(ReqManager[0]["Tel1"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel1"].ToString().Length - ReqManager[0]["Tel1"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfTel1.Text = ReqManager[0]["Tel1"].ToString();
                }
            }
            if (ReqManager[0]["Tel2"].ToString() != "")
            {
                if (ReqManager[0]["Tel2"].ToString().IndexOf("-") > 0)
                {
                    txtOfTel2_pre.Text = ReqManager[0]["Tel2"].ToString().Substring(0, ReqManager[0]["Tel2"].ToString().IndexOf("-"));
                    txtOfTel2.Text = ReqManager[0]["Tel2"].ToString().Substring(ReqManager[0]["Tel2"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel2"].ToString().Length - ReqManager[0]["Tel2"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfTel2.Text = ReqManager[0]["Tel2"].ToString();
                }
            }
            if (ReqManager[0]["Fax"].ToString() != "")
            {
                if (ReqManager[0]["Fax"].ToString().IndexOf("-") > 0)
                {
                    txtOfFax_pre.Text = ReqManager[0]["Fax"].ToString().Substring(0, ReqManager[0]["Fax"].ToString().IndexOf("-"));
                    txtOfFax.Text = ReqManager[0]["Fax"].ToString().Substring(ReqManager[0]["Fax"].ToString().IndexOf("-") + 1, ReqManager[0]["Fax"].ToString().Length - ReqManager[0]["Fax"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfFax.Text = ReqManager[0]["Fax"].ToString();
                }
            }
            txtOfMobile.Text = ReqManager[0]["MobileNo"].ToString();
            txtOfEmail.Text = ReqManager[0]["Email"].ToString();
            txtOfWebsite.Text = ReqManager[0]["Website"].ToString();
            txtOfAddress.Text = ReqManager[0]["Address"].ToString();
            if ((!Utility.IsDBNullOrNullValue(ReqManager[0]["ArmUrl"])))
            {
                imgOfArm.ImageUrl = ReqManager[0]["ArmUrl"].ToString();
                HDFlpArm["name"] = 1;

            }
            else
            {
                imgOfArm.ImageUrl = "~/images/noimage.gif/";
            }

            if ((!Utility.IsDBNullOrNullValue(ReqManager[0]["SignUrl"])))
            {
                imgOfSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();
                HDFlpSign["name"] = 1;

            }
            else
            {
                imgOfSign.ImageUrl = "~/images/noimage.gif/";
            }

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MembershipRequstType"]))
            {
                cmbMembershipRequstType.SelectedIndex = cmbMembershipRequstType.Items.FindByValue(ReqManager[0]["MembershipRequstType"].ToString()).Index;
            }

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ActivityType"]))
            {
                cmbActivityType.SelectedIndex = Convert.ToBoolean(ReqManager[0]["ActivityType"]) == true ? 1 : 0;
            }
            drdOfType.DataBind();
            drdOfType.Value = ReqManager[0]["OtId"].ToString();
            txtOfSubject.Text = ReqManager[0]["Subject"].ToString();
            txtOfRegDate.Text = ReqManager[0]["RegOfDate"].ToString();
            txtOfRegNo.Text = ReqManager[0]["RegOfNo"].ToString();
            txtOfRegPlace.Text = ReqManager[0]["RegOfPlace"].ToString();
            txtOfStock.Text = ReqManager[0]["Stock"].ToString();
            if (!string.IsNullOrEmpty(ReqManager[0]["VolumeInvest"].ToString()))
                txtOfValue.Text = Decimal.Parse(ReqManager[0]["VolumeInvest"].ToString()).ToString("##");

            #region attach
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
            dtOfImg = (DataTable)Session["TblOfReImg"];
            for (int i = 0; i < attachManager.Count; i++)
            {
                DataRow dr = dtOfImg.NewRow();
                dr[0] = attachManager[i]["FilePath"];
                dr[1] = attachManager[i]["FilePath"].ToString();
                dr[5] = attachManager[i]["Description"].ToString();

                string fileName = Path.GetFileName(attachManager[0]["FilePath"].ToString());
                dr[2] = fileName;
                dr[3] = 1;
                dr[6] = attachManager[i][0];
                dtOfImg.Rows.Add(dr);

            }

            dtOfImg.AcceptChanges();
            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
            #endregion
         
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RequestDesc"]))
                txtReRequestDesc.Text = ReqManager[0]["RequestDesc"].ToString();

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OfficeDescription"]))
                txtOfDesc.Text = ReqManager[0]["OfficeDescription"].ToString();
            CheckBoxConditionalApprove.Checked = Convert.ToBoolean(ReqManager[0]["ConditionalApproval"]);
            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo
                && _PageMode != "NewReq")
            {
                CheckColor(OfId);
            }
            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid)
            {
                SetEnable(false);
                if (_PageMode == "Edit")
                    txtReRequestDesc.Enabled = true;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected Boolean FillForm(int OfId)
    {
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        txtOfId.Text = OfId.ToString();
        if (!Utility.IsDBNullOrNullValue(OffManager[0]["MeNo"]))
            txtMeNo.Text = OffManager[0]["MeNo"].ToString();
        txtOfName.Text = OffManager[0]["OfName"].ToString();
        if (!Utility.IsDBNullOrNullValue(OffManager[0]["OfNameEn"]))
            txtOfNameEn.Text = OffManager[0]["OfNameEn"].ToString();
        drdOfType.Value = OffManager[0]["OtId"].ToString();

        if (OffManager[0]["Tel1"].ToString() != "")
        {
            if (OffManager[0]["Tel1"].ToString().IndexOf("-") > 0)
            {
                txtOfTel1_pre.Text = OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-"));
                txtOfTel1.Text = OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfTel1.Text = OffManager[0]["Tel1"].ToString();
            }
        }
        if (OffManager[0]["Tel2"].ToString() != "")
        {
            if (OffManager[0]["Tel2"].ToString().IndexOf("-") > 0)
            {
                txtOfTel2_pre.Text = OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-"));
                txtOfTel2.Text = OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfTel2.Text = OffManager[0]["Tel2"].ToString();
            }
        }
        if (OffManager[0]["Fax"].ToString() != "")
        {
            if (OffManager[0]["Fax"].ToString().IndexOf("-") > 0)
            {
                txtOfFax_pre.Text = OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-"));
                txtOfFax.Text = OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfFax.Text = OffManager[0]["Fax"].ToString();
            }
        }

        if (!Utility.IsDBNullOrNullValue(OffManager[0]["MembershipRequstType"]))
        {
            cmbMembershipRequstType.SelectedIndex = cmbMembershipRequstType.Items.FindByValue(OffManager[0]["MembershipRequstType"].ToString()).Index;
        }

        if (!Utility.IsDBNullOrNullValue(OffManager[0]["ActivityType"]))
        {
            cmbActivityType.SelectedIndex = Convert.ToBoolean(OffManager[0]["ActivityType"]) == true ? 1 : 0;
        }

        txtOfMobile.Text = OffManager[0]["MobileNo"].ToString();
        txtOfEmail.Text = OffManager[0]["Email"].ToString();
        txtOfWebsite.Text = OffManager[0]["Website"].ToString();
        txtOfAddress.Text = OffManager[0]["Address"].ToString();
        txtOfSubject.Text = OffManager[0]["Subject"].ToString();
        txtOfRegDate.Text = OffManager[0]["RegDate"].ToString();//****RegOfDate in tblOfficeRequest == RegDate in tblOffice
        txtOfRegNo.Text = OffManager[0]["RegOfNo"].ToString();
        txtOfRegPlace.Text = OffManager[0]["RegPlace"].ToString();
        txtOfStock.Text = OffManager[0]["Stock"].ToString();
        if (!string.IsNullOrEmpty(OffManager[0]["VolumeInvest"].ToString()))
            txtOfValue.Text = Decimal.Parse(OffManager[0]["VolumeInvest"].ToString()).ToString("##");
        txtOfDesc.Text = OffManager[0]["Description"].ToString();

        if ((!Utility.IsDBNullOrNullValue(OffManager[0]["ArmUrl"])))
        {
            imgOfArm.ImageUrl = OffManager[0]["ArmUrl"].ToString();
            HDFlpArm["name"] = 1;
        }
        else
            imgOfArm.ImageUrl = "~/images/noimage.gif/";

        if ((!Utility.IsDBNullOrNullValue(OffManager[0]["SignUrl"])))
        {
            imgOfSign.ImageUrl = OffManager[0]["SignUrl"].ToString();
            HDFlpSign["name"] = 1;
        }
        else
            imgOfSign.ImageUrl = "~/images/noimage.gif/";
        if (Convert.ToInt32(OffManager[0]["MrsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
        {
            CheckBoxConditionalApprove.Checked = true;
        }

        return true;
    }
    #endregion

    #region SaveImage
    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Sign/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfSign2"] = tempFileName2;
            //Session["FileOfSign2"] = ret;

        }
        return ret;
    }

    protected string SaveImageArm(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Arm/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfArm2"] = tempFileName2;
            //Session["FileOfArm2"] = ret;

        }
        return ret;
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Office/OffRequest/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["MeReqUpload"] = tempFileName;

        }
        return ret;
    }
    #endregion

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSaveOffice(_PageMode);
        if (_PageMode == "View" || _PageMode == "Edit")
        {
            CheckWorkFlowPermissionForEditForOffice(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSaveOffice(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permission2 = Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
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
            this.LabelWarning.Text = "شما سطح دسترسی جهت ثبت اطلاعات عضو حقوقی را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForOffice(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _OfReId, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permisssion2 = Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _OfReId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2 > 0)
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
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
                case "ReDuplicate":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "Revival":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "Change":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

            }
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
            return true;
        else
            return false;
    }
    #endregion

    #region Insert
    protected void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.MemberStatusChangeManager ChManager = new TSP.DataManager.MemberStatusChangeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        trans.Add(OffManager);
        trans.Add(LogManager);
        trans.Add(ReqManager);
        trans.Add(ChManager);
        trans.Add(WorkFlowStateManager);


        string pathAx = "", Password = "";

        try
        {
            //int AccId = -1, ParentAccId = -1, MembershipEarningsAccId = -1, MainBankAccId = -1;
            //string Des2 = "";
            //decimal Amount = 0;

            string PerDate = Utility.GetDateOfToday();
            trans.BeginSave();
            #region Insert Office
            DataRow drOffice = OffManager.NewRow();
            drOffice["OfId"] = 0;
            drOffice["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drOffice["OfNameEn"] = txtOfNameEn.Text;
            drOffice["PrefixCode"] = DBNull.Value;
            if (drdOfType.Value != null)
                drOffice["OtId"] = int.Parse(drdOfType.Value.ToString());
            drOffice["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            drOffice["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            drOffice["OatId"] = DBNull.Value;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drOffice["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drOffice["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drOffice["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drOffice["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drOffice["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drOffice["Fax"] = txtOfFax.Text;
            drOffice["MobileNo"] = txtOfMobile.Text;
            drOffice["Email"] = txtOfEmail.Text;
            drOffice["Website"] = txtOfWebsite.Text;
            drOffice["Address"] = txtOfAddress.Text;
            drOffice["Subject"] = txtOfSubject.Text;
            drOffice["RegDate"] = txtOfRegDate.Text;
            drOffice["RegOfNo"] = txtOfRegNo.Text;
            drOffice["RegPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drOffice["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drOffice["VolumeInvest"] = txtOfValue.Text;
            drOffice["MeNo"] = DBNull.Value;
            drOffice["FileNo"] = DBNull.Value;
            drOffice["MrsId"] = 2;
            drOffice["DocumentStatus"] = (int)TSP.DataManager.OfficeDocumentStatus.DoNotHaveDocument;

            if (Session["FileOfArm2"] != null)
            {
                pathAx = Server.MapPath("~/Image/Temp/");
                imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                drOffice["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }

            if (Session["FileOfSign2"] != null)
            {
                pathAx = Server.MapPath("~/Image/Temp/");
                imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                drOffice["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
            }
            drOffice["IsLock"] = 0;
            drOffice["CreateDate"] = PerDate;
            drOffice["Description"] = txtOfDesc.Text;
            drOffice["UserId"] = Utility.GetCurrentUser_UserId();
            drOffice["ModifiedDate"] = DateTime.Now;
            OffManager.AddRow(drOffice);
            if (OffManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            #endregion
            OffManager.DataTable.AcceptChanges();
            int OfId = Convert.ToInt32(OffManager[0]["OfId"]);
            _OfId = Convert.ToInt32(OffManager[0]["OfId"]);

            #region Login
            Password = InsertLogin(LogManager, OfId, OffManager[0]["RegOfNo"].ToString(), OffManager[0]["Email"].ToString());
            #endregion

            #region Request
            InsertOfficeRequest(ReqManager, OfId, OffManager[0]["ArmUrl"].ToString(), OffManager[0]["SignUrl"].ToString());
            _OfReId = Convert.ToInt32(ReqManager[0]["OfReId"]);
            #endregion

            #region StartWorkFlow
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LogManager);
            WorkFlowStateManager.StartWorkFlow(_OfReId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0);
            #endregion
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                }
            }
            trans.EndSave();
            TSP.DataManager.OfficeManager.UpdateMeNo(OfId);
            this.ViewState["BtnEdit"] = btnEdit2.Enabled = btnEdit.Enabled = false;

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "  ذخیره با نام کاربری " + "com" + OffManager[0]["OfId"].ToString() + "و رمز عبور " + Password + " انجام شد";
            txtOfId.Text = OfId.ToString();
            OffManager.FindByCode(OfId);
            if (OffManager.Count == 1 && !Utility.IsDBNullOrNullValue(OffManager[0]["MeNo"]))
                txtMeNo.Text = OffManager[0]["MeNo"].ToString();

            _PageMode = "Edit";
            RoundPanelOffice.HeaderText = "ویرایش";
            MenuDetails.Enabled = true;

            if ((!Utility.IsDBNullOrNullValue(OffManager[0]["ArmUrl"])))
            {
                imgOfArm.ClientVisible = true;
                imgOfArm.ImageUrl = OffManager[0]["ArmUrl"].ToString();
            }

            if ((!Utility.IsDBNullOrNullValue(OffManager[0]["SignUrl"])))
            {
                imgOfSign.ClientVisible = true;
                imgOfSign.ImageUrl = OffManager[0]["SignUrl"].ToString();
            }

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
            return;
        }
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                Session["FileOfSign2"] = null;

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                Session["FileOfArm2"] = null;


            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected string InsertLogin(TSP.DataManager.LoginManager LogManager, int OfId, string RegOfNo, string Email)
    {
        String Password = Utility.GeneratePassword();
        DataRow logRow = LogManager.NewRow();
        logRow.BeginEdit();
        logRow["UserName"] = "com" + OfId.ToString();
        if (!string.IsNullOrEmpty(RegOfNo))
            logRow["Password"] = Utility.EncryptPassword(Password);
        logRow["UltId"] = 2;
        logRow["MeId"] = OfId;
        logRow["Email"] = Email;
        logRow["IsValid"] = 1;
        logRow["UserId2"] = Utility.GetCurrentUser_UserId();
        logRow["ModifiedDate"] = DateTime.Now;
        logRow.EndEdit();
        LogManager.AddRow(logRow);
        LogManager.Save();
        return Password;
    }

    protected void InsertOfficeRequest(TSP.DataManager.OfficeRequestManager ReqManager, int OfId, string ArmUrl, string SignUrl)
    {
        DataRow drReq = ReqManager.NewRow();
        drReq["OfId"] = OfId;
        drReq["OfName"] = txtOfName.Text;
        if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
            drReq["OfNameEn"] = txtOfNameEn.Text;
        if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
            drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
        else if (txtOfTel1.Text != "")
            drReq["Tel1"] = txtOfTel1.Text;
        if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
            drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
        else if (txtOfTel2.Text != "")
            drReq["Tel2"] = txtOfTel2.Text;
        if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
            drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
        else if (txtOfFax.Text != "")
            drReq["Fax"] = txtOfFax.Text;
        drReq["MobileNo"] = txtOfMobile.Text;
        drReq["Email"] = txtOfEmail.Text;
        drReq["Website"] = txtOfWebsite.Text;
        drReq["Address"] = txtOfAddress.Text;
        if (drdOfType.Value != null)
            drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
        drReq["Subject"] = txtOfSubject.Text;
        drReq["RegOfDate"] = txtOfRegDate.Text;
        drReq["RegOfNo"] = txtOfRegNo.Text;
        drReq["RegOfPlace"] = txtOfRegPlace.Text;
        if (txtOfStock.Text != "")
            drReq["Stock"] = int.Parse(txtOfStock.Text);
        if (txtOfValue.Text != "")
            drReq["VolumeInvest"] = txtOfValue.Text;

        drReq["CreateDate"] = Utility.GetDateOfToday();
        drReq["Type"] = (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo;//درخواست ثبت نام اولیه
        drReq["Requester"] = 1;
        drReq["UserId"] = Utility.GetCurrentUser_UserId();
        drReq["ModifiedDate"] = DateTime.Now;
        if (!string.IsNullOrEmpty(ArmUrl))
            drReq["ArmUrl"] = ArmUrl;

        if (!string.IsNullOrEmpty(SignUrl))
            drReq["SignUrl"] = SignUrl;

        drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);
        drReq["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
        drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
        drReq["OfficeDescription"] = txtOfDesc.Text;
        drReq["RequestDesc"] = txtReRequestDesc.Text;
        drReq["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;
        ReqManager.AddRow(drReq);
        ReqManager.Save();
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده اطلاعات پایه عضو حقوقی", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    protected void InsertMembershipRequest(int OfId, int OfficeRequestType)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);


        try
        {
           
            ReqManager.FindByOfficeId(OfId, 1, -1);
            if (ReqManager.Count == 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }

            trans.BeginSave();
            #region Insert Requset
            DataRow drReq = ReqManager.NewRow();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["PrId"]))
                drReq["PrId"] = ReqManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegDate"]))
                drReq["RegDate"] = ReqManager[0]["RegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ExpireDate"]))
                drReq["ExpireDate"] = ReqManager[0]["ExpireDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegPlaceId"]))
                drReq["RegPlaceId"] = ReqManager[0]["RegPlaceId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
                drReq["MFNo"] = ReqManager[0]["MFNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                drReq["MFType"] = ReqManager[0]["MFType"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
                drReq["IsTemp"] = ReqManager[0]["IsTemp"].ToString();

            drReq["OfId"] = OfId;
            drReq["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drReq["OfNameEn"] = txtOfNameEn.Text;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax.Text;
            drReq["MobileNo"] = txtOfMobile.Text;
            drReq["Email"] = txtOfEmail.Text;
            drReq["Website"] = txtOfWebsite.Text;
            drReq["Address"] = txtOfAddress.Text;
            if (drdOfType.Value != null)
                drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
            drReq["Subject"] = txtOfSubject.Text;
            drReq["RegOfDate"] = txtOfRegDate.Text;
            drReq["RegOfNo"] = txtOfRegNo.Text;
            drReq["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drReq["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drReq["VolumeInvest"] = txtOfValue.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Type"] = OfficeRequestType;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["RequestDesc"] = txtReRequestDesc.Text;
            drReq["OfficeDescription"] = txtOfDesc.Text;

            drReq["ModifiedDate"] = DateTime.Now;

            drReq["LetterNo"] = "";
            drReq["LetterDate"] = "";
            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

            drReq["Requester"] = 1;//کارمند

            if (Session["FileOfArm2"] != null)
            {
                imgOfArm.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                drReq["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }
            else
                drReq["ArmUrl"] = imgOfArm.ImageUrl;

            if (Session["FileOfSign2"] != null)
            {
                imgOfSign.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                drReq["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());

            }
            else
                drReq["SignUrl"] = imgOfSign.ImageUrl;

            drReq["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            drReq["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;
            ReqManager.AddRow(drReq);
            if (ReqManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            ReqManager.DataTable.AcceptChanges();

            int TableId = _OfReId = Convert.ToInt32(ReqManager[ReqManager.Count - 1]["OfReId"]);


            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    drImg["RefTable"] = ReqManager[ReqManager.Count - 1]["OfReId"];
                    drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                    drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                    drImg["IsValid"] = 1;
                    drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                    drImg["UserId"] = Utility.GetCurrentUser_UserId();
                    drImg["ModfiedDate"] = DateTime.Now;
                    attachManager.AddRow(drImg);
                    int imgcnt = attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                    if (imgcnt == 1)
                    {
                        dtOfImg.Rows[i].BeginEdit();
                        dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                        dtOfImg.Rows[i].EndEdit();

                        if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                        {
                            string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);

                        }

                    }
                }
            }

            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int CurrentNmcId = FindNmcId(SaveInfoTaskId);
            if (CurrentNmcId == -1)
            {
                trans.CancelSave();
                return;
            }

            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);

            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);

            //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
                }
            }

            trans.EndSave();

            TSP.DataManager.Permission per = TSP.DataManager.OfficeRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            MenuDetails.Enabled = true;
            _PageMode = "Edit";
            RoundPanelOffice.HeaderText = "ویرایش";

            ShowMessage("ذخیره انجام شد");

            CheckColor(OfId);
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
            return;
        }
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void InsertInActtiveOfficeMembers(TSP.DataManager.OfficeMemberManager OffMemberManager, TSP.DataManager.DocMemberFileManager DocMemberFileManager, TSP.DataManager.RequestInActivesManager ReqInActiveManager, int OfId, int OfReId)
    {
        int MemberFileId = -1;
        int MeId = -1;
        int OfmMfId = -1;
        OffMemberManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Member, 0, 1);
        if (OffMemberManager.Count > 0)
        {
            for (int i = 0; i < OffMemberManager.Count; i++)
            {
                MeId = Convert.ToInt32(OffMemberManager[i]["PersonId"]);
                if (Utility.IsDBNullOrNullValue(OffMemberManager[i]["MfId"])) continue;
                OfmMfId = Convert.ToInt32(OffMemberManager[i]["MfId"]);

                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                    if (OfmMfId != MemberFileId)
                    {
                        DataRow drOfm = OffMemberManager.NewRow();
                        drOfm["OfReId"] = OfReId;
                        drOfm["MfId"] = MemberFileId;
                        drOfm["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
                        drOfm["PersonId"] = MeId; ;
                        drOfm["SignUrl"] = OffMemberManager[i]["SignUrl"];
                        drOfm["OfId"] = OfId;
                        drOfm["OfpId"] = OffMemberManager[i]["OfpId"];
                        drOfm["StartDate"] = OffMemberManager[i]["StartDate"];
                        drOfm["HasSignRight"] = OffMemberManager[i]["HasSignRight"];
                        drOfm["IsFullTime"] = OffMemberManager[i]["IsFullTime"];
                        drOfm["Description"] = OffMemberManager[i]["Description"];
                        drOfm["UserId"] = Utility.GetCurrentUser_UserId();
                        drOfm["ModifiedDate"] = DateTime.Now;
                        OffMemberManager.AddRow(drOfm);

                        DataRow dr = ReqInActiveManager.NewRow();
                        dr["TableId"] = OffMemberManager[i]["OfmId"];
                        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
                        dr["ReqId"] = OfReId;
                        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                        dr["InActive"] = 1;
                        dr["SysInActive"] = 1;
                        dr["CreateDate"] = Utility.GetDateOfToday();
                        dr["UserId"] = Utility.GetCurrentUser_UserId();
                        dr["ModifiedDate"] = DateTime.Now;
                        ReqInActiveManager.AddRow(dr);

                    }
                }
            }
            OffMemberManager.Save();
            ReqInActiveManager.Save();
        }

    }
    #endregion

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

    #region Edit
    protected void Edit(int OfId, int OfReId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(ReqManager);
        trans.Add(attachManager);

        string pathAx = "";

        bool chArmEdit = false;
        bool chSignEdit = false;

        try
        {
            trans.BeginSave();
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
                return;
            }
            ReqManager[0].BeginEdit();
            ReqManager[0]["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                ReqManager[0]["OfNameEn"] = txtOfNameEn.Text;
            if (drdOfType.Value != null)
                ReqManager[0]["OtId"] = int.Parse(drdOfType.Value.ToString());

            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                ReqManager[0]["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                ReqManager[0]["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                ReqManager[0]["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                ReqManager[0]["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                ReqManager[0]["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                ReqManager[0]["Fax"] = txtOfFax.Text;
            ReqManager[0]["MobileNo"] = txtOfMobile.Text;
            ReqManager[0]["Email"] = txtOfEmail.Text;
            ReqManager[0]["Website"] = txtOfWebsite.Text;
            ReqManager[0]["Address"] = txtOfAddress.Text;
            ReqManager[0]["Subject"] = txtOfSubject.Text;

            //???????ReqManager[0]["RegDate"] = txtOfRegDate.Text;
            ReqManager[0]["RegOfDate"] = txtOfRegDate.Text;

            ReqManager[0]["RegOfNo"] = txtOfRegNo.Text;
            ReqManager[0]["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                ReqManager[0]["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                ReqManager[0]["VolumeInvest"] = txtOfValue.Text;

            #region editArmImage
            if (Session["FileOfArm2"] != null)
            {
                if ((!string.IsNullOrEmpty(ReqManager[0]["ArmUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()))))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;

                    }
                    catch (Exception err)
                    {
                        trans.CancelSave();
                        Utility.SaveWebsiteError(err);
                        ShowMessage("امکان ویرایش فایل نمی باشد.");
                        return;
                    }

                }
                else
                {
                    try
                    {
                        pathAx = Server.MapPath("~/Image/Temp/");
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;


                    }
                    catch (Exception err)
                    {
                        trans.CancelSave();
                        Utility.SaveWebsiteError(err);
                        ShowMessage("امکان ویرایش فایل نمی باشد.");
                        return;
                    }
                }
            }
            #endregion

            #region editSignImage
            if (Session["FileOfSign2"] != null)
            {
                if ((!string.IsNullOrEmpty(ReqManager[0]["SignUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["SignUrl"].ToString()))))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(ReqManager[0]["SignUrl"].ToString()));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                        ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                        chSignEdit = true;



                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        return;
                    }

                }
                else
                {
                    try
                    {

                        pathAx = Server.MapPath("~/Image/Temp/");
                        imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                        ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                        chSignEdit = true;


                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        return;
                    }
                }
            }
            #endregion
            ReqManager[0]["RequestDesc"] = txtReRequestDesc.Text;// txtOfDesc.Text;
            ReqManager[0]["OfficeDescription"] = txtOfDesc.Text;

            ReqManager[0]["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            ReqManager[0]["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ReqManager[0]["ModifiedDate"] = DateTime.Now;
            ReqManager[0]["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;
            ReqManager[0].EndEdit();

            ReqManager.Save();

            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.GetChanges() != null)
            {
                DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
                        drImg["RefTable"] = OfReId;
                        drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                        drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        attachManager.AddRow(drImg);
                        int imgcnt = attachManager.Save();
                        attachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImg.Rows[i].BeginEdit();
                            dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                            dtOfImg.Rows[i].EndEdit();

                            if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                            {
                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);

                            }

                        }
                    }

                }

            }
            trans.EndSave();
            _PageMode = "Edit";
            RoundPanelOffice.HeaderText = "ویرایش";
            ShowMessage("ذخیره انجام شد");

            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)
                CheckColor(OfId);
        }

        catch (Exception err)
        {
            trans.CancelSave();
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }

        }
        if (Session["FileOfArm2"] != null)
        {
            if (chArmEdit == true)
            {
                try
                {
                    string ArmSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    System.IO.File.Copy(ArmSoource, ArmTarget, true);
                    imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                    Session["FileOfArm2"] = null;

                }
                catch (Exception err)
                {
                    Utility.SaveWebsiteError(err);
                }
            }

        }
        if (Session["FileOfSign2"] != null)
        {
            if (chSignEdit == true)
            {
                try
                {
                    string SignSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    System.IO.File.Copy(SignSoource, SignTarget, true);
                    imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                    Session["FileOfSign2"] = null;
                }
                catch (Exception err)
                {
                    Utility.SaveWebsiteError(err);
                }

            }

        }
    }
    #endregion

    private void ResetSession()
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        Session["DesObsGrade"] = null;
        Session["OffMenuArrayList"] = null;
    }

    #region Menu Image

    protected void CheckColor(int OfId)
    {
        bool change = false;

        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count == 1)
        {
            if (txtOfName.Text != OffManager[0]["OfName"].ToString())
                txtOfName.ForeColor = Color.Red;
            if (txtOfNameEn.Text != OffManager[0]["OfNameEn"].ToString())
                txtOfNameEn.ForeColor = Color.Red;

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && OffManager[0]["Tel1"].ToString().Contains("-"))
            {
                if (txtOfTel1.Text != OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1))
                {
                    txtOfTel1.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfTel1_pre.Text != OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-")))
                {
                    txtOfTel1_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !OffManager[0]["Tel1"].ToString().Contains("-"))
            {
                if (txtOfTel1.Text != OffManager[0]["Tel1"].ToString())
                {
                    txtOfTel1.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !string.IsNullOrEmpty(txtOfTel1.Text))
            {
                txtOfTel1.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !string.IsNullOrEmpty(txtOfTel1_pre.Text))
            {
                txtOfTel1_pre.ForeColor = Color.Red;
                change = true;
            }

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && OffManager[0]["Tel2"].ToString().Contains("-"))
            {
                if (txtOfTel2.Text != OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1))
                {
                    txtOfTel2.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfTel2_pre.Text != OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-")))
                {
                    txtOfTel2_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !OffManager[0]["Tel2"].ToString().Contains("-"))
            {
                if (txtOfTel2.Text != OffManager[0]["Tel2"].ToString())
                {
                    txtOfTel2.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !string.IsNullOrEmpty(txtOfTel2.Text))
            {
                txtOfTel2.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !string.IsNullOrEmpty(txtOfTel2_pre.Text))
            {
                txtOfTel2_pre.ForeColor = Color.Red;
                change = true;
            }

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && OffManager[0]["Fax"].ToString().Contains("-"))
            {
                if (txtOfFax.Text != OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1))
                {
                    txtOfFax.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfFax_pre.Text != OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-")))
                {
                    txtOfFax_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !OffManager[0]["Fax"].ToString().Contains("-"))
            {
                if (txtOfFax.Text != OffManager[0]["Fax"].ToString())
                {
                    txtOfFax.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !string.IsNullOrEmpty(txtOfFax.Text))
            {
                txtOfFax.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !string.IsNullOrEmpty(txtOfFax_pre.Text))
            {
                txtOfFax_pre.ForeColor = Color.Red;
                change = true;
            }

            if (txtOfMobile.Text != OffManager[0]["MobileNo"].ToString())
            {
                txtOfMobile.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfEmail.Text != OffManager[0]["Email"].ToString())
            {
                txtOfEmail.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfWebsite.Text != OffManager[0]["Website"].ToString())
            {
                txtOfWebsite.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfAddress.Text != OffManager[0]["Address"].ToString())
            {
                txtOfAddress.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegDate.Text != OffManager[0]["RegDate"].ToString())
            {
                txtOfRegDate.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegNo.Text != OffManager[0]["RegOfNo"].ToString())
            {
                txtOfRegNo.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegPlace.Text != OffManager[0]["RegPlace"].ToString())
            {
                txtOfRegPlace.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfStock.Text != OffManager[0]["Stock"].ToString())
            {
                txtOfStock.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfSubject.Text != OffManager[0]["Subject"].ToString())
            {
                txtOfSubject.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfValue.Text != OffManager[0]["VolumeInvest"].ToString())
            {
                txtOfValue.ForeColor = Color.Red;
                change = true;
            }
            if (drdOfType.Value != null && !Utility.IsDBNullOrNullValue(OffManager[0]["OtId"]))
            {
                if (Convert.ToInt32(drdOfType.Value) != Convert.ToInt32(OffManager[0]["OtId"]))
                {
                    drdOfType.ForeColor = Color.Red;
                    change = true;
                }
            }

        }
        if (change == true)
        {
            if (Session["OffMenuArrayList"] != null)
            {
                System.Collections.ArrayList arr = (System.Collections.ArrayList)Session["OffMenuArrayList"];
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
                Session["OffMenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_OfReId);
                System.Collections.ArrayList arr = (System.Collections.ArrayList)Session["OffMenuArrayList"];
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
                Session["OffMenuArrayList"] = arr;
            }


        }

    }


    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.OfficeManager officeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager officeRequestManager = new TSP.DataManager.OfficeRequestManager();



        System.Collections.ArrayList arr = new System.Collections.ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        arr.Add(0);//arr[7]-->Group


        officeRequestManager.FindByCode(OfReId);
        if (officeRequestManager.Count > 0)
        {
            int OfId = Convert.ToInt32(officeRequestManager[0]["OfId"]);
            officeManager.FindByCode(OfId);
            if (officeManager.Count > 0)
            {
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
            }
        }


        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }
    #endregion

    #endregion
}