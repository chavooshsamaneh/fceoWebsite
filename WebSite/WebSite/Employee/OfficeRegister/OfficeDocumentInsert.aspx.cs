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
using System.Drawing;

public partial class Employee_OfficeRegister_OfficeDocumentInsert : System.Web.UI.Page
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
            Response.Redirect("OfficeInsert.aspx");
            return;
        }

        if (!IsPostBack)
        {
            ResetSessions();

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

            Session["AccountingManager"] = null;
            Session["AccountingManager"] = CreateAccountingManager();

            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

            SetAccountingFilterExpression();
            SetLabelRegEnter();
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        BindAccountingGrid();
    }

    protected void txtOfId_TextChanged(object sender, EventArgs e)
    {
        Boolean IsOfficeValid = true;
        int OfId = -1;
        int.TryParse(txtOfId.Text, out OfId);
        if (OfId == null || OfId <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مقدار وارد شده به عنوان کد عضویت صحیح نمی باشد";
            return;
        }
        ClearFormForSearchOfId();
        if (!FillForm(OfId))
        {
            ClearFormForSearchOfId();
            return;
        }

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

        ReqManager.FindByOfficeId(OfId, 0, (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل عدم پاسخ درخواست ثبت نام اولیه, امکان درخواست صدور پروانه وجود ندارد";
            IsOfficeValid = false;
            return;
        }
        ReqManager.FindByOfficeId(OfId, 1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);//صدور
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود پروانه فعال امکان صدور پروانه مجدد وجود ندارد ";
            IsOfficeValid = false;
            return;
        }

        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده,امکان ثبت درخواست جدید وجود ندارد";
            IsOfficeValid = false;
            return;
        }
        if (!IsOfficeValid)
            ClearFormForSearchOfId();
        else
            _OfId = OfId;

    }

    #region Buttons Click
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string PageName = "OfficeDocument.aspx";

        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"])
            && _OfId != -1 && !string.IsNullOrEmpty(_OfId.ToString())
            && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?PostId=" + Utility.EncryptQS(_OfId.ToString()) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

        }
        else
            Response.Redirect(PageName);
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
                if (CheckPermitionForEdit(_OfReId))
                {
                    if (CheckCanEditFishForEdit(_OfReId))
                    {
                        PanelAccountingInserting.Visible = true;
                        GridViewAccounting.Columns["Delete"].Visible = true;
                    }
                    else
                    {
                        PanelAccountingInserting.Visible = false;
                        GridViewAccounting.Columns["Delete"].Visible = false;
                    }

                    SetEnable(true);//حذف شود اجازه ویرایش جزئیات اطلاعات عضویت شرکت
                    txtFileNo.Enabled = true;
                    txtReRequestDesc.Enabled = true;
                    ComboDocType.Enabled = true;
                    TblFile.Visible = true;

                    imgOfArm.ClientVisible = true;
                    imgOfSign.ClientVisible = true;

                    TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnSave.Enabled = per.CanEdit;
                    btnSave2.Enabled = per.CanEdit;
                    this.ViewState["BtnSave"] = btnSave.Enabled;

                    _PageMode = "Edit";
                    RoundPanelOffice.HeaderText = "ویرایش";
                    MenuDetails.Enabled = true;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
                }

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
                InsertReqDocument(_OfId);
                break;

            case "Edit":
                EditRequest(_OfReId);
                break;

            case "Change":
                InsertNewRequest(_OfId, TSP.DataManager.OfficeRequestType.Change);
                break;
            case "Revival":
                InsertNewRequest(_OfId, TSP.DataManager.OfficeRequestType.Revival);
                break;
            case "Reduplicate":
                InsertNewRequest(_OfId, TSP.DataManager.OfficeRequestType.Reduplicate);
                break;
            case "DocumentInvalid":
                InsertNewRequest(_OfId, TSP.DataManager.OfficeRequestType.DocumentInvalid);
                break;

            case "ChangeShareHolderAndBaseInfo":
                InsertNewRequest(_OfId, TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo);
                break;
        }

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        _OfId = -1;
        _PageMode = "New";
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;

        ClearForm();
        SetEnable(true);
        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;
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
                    + "&Dprt=" + Utility.EncryptQS("Document"));

                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("Document"));


                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("Document"));


                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("Document"));


                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"] + "&Dprt="
                    + Utility.EncryptQS("Document"));


                break;

            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("Document"));

                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + Utility.EncryptQS("Document"));

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

    #region CallBack's
    protected void Callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        Callback.JSProperties["cpPrint"] = 0;
        switch (e.Parameter)
        {
            case "Print":
                Callback.JSProperties["cpPrint"] = 1;
                Callback.JSProperties["cpURL"] = "../../ReportForms/OfficeReport.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString()) + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString());
                break;
        }
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }
    #endregion

    #region Accounting Fish

    protected void GridViewAccounting_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
        GridViewAccounting.JSProperties["cpMessage"] = "";
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];

        DataRow dr = AccountingManager.DataTable.Rows.Find(e.Keys["AccountingId"]);
        dr.Delete();
        e.Cancel = true;

        GridViewAccounting.CancelEdit();

        GridViewAccounting.DataSource = AccountingManager.DataTable;
        GridViewAccounting.DataBind();
    }

    protected void GridViewAccounting_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            if (_PageMode == "View")
            {
                if (e.Row.Cells.Count > 5)
                    e.Row.Cells[4].Controls[0].Visible = false;
            }
        }
    }

    protected void GridViewAccounting_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
        GridViewAccounting.JSProperties["cpMessage"] = "";

        if (e.Parameters == "Add")
        {
            try
            {
                if ((new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumber(txtaNumber.Text) == false)
                {
                    GridViewAccounting.JSProperties["cpMessage"] = "این شماره فیش قبلا در سیستم ثبت شده است";
                    GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
                }
                else
                {
                    RowInserting();
                    GridViewAccounting.JSProperties["cpSaveComplete"] = "1";
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
            }
        }
    }
    #endregion
    #endregion

    #region Methods

    #region SetKey
    private void SetKey()
    {
        try
        {
            _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
            _OfId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfId"]).ToString()));
            _OfReId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfReId"]).ToString()));

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

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

    private void SetMode()
    {
        switch (_PageMode)
        {
            case "New"://درخواست صدور پروانه
                SetNewMode();
                break;

            case "Edit":
                SetEditMode();
                break;

            case "View":
                SetViewMode();
                break;

            case "Change":
                SetRequestsMode("تغییرات پروانه");
                break;

            case "Revival":
                SetRequestsMode("تمدید پروانه");
                break;

            case "Reduplicate":
                SetRequestsMode("صدور المثنی پروانه");
                break;
            case "DocumentInvalid":
                SetRequestsMode("ابطال پروانه");
                break;

            case "ChangeShareHolderAndBaseInfo":
                SetRequestsMode("تغییرات اطلاعات پایه و سهامداران");
                break;
        }
    }

    private void SetNewMode()
    {
        SetEnable(true);

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید-درخواست صدور پروانه جدید";
        ClearForm();
        MenuDetails.Enabled = false;
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

            SetRoundPanelVisibleByRequestType(Convert.ToInt32(ReqManager[0]["Type"]));
        }

        RoundPanelOffice.Enabled = true;
    }

    private void SetViewMode()
    {
        RoundPanelOffice.HeaderText = "مشاهده";

        btnSave.Enabled = false;
        btnSave2.Enabled = false;


        SetEnable(false);

        txtFileNo.Enabled = false;
        txtReRequestDesc.Enabled = false;
        ComboDocType.Enabled = false;
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;

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
            else//**************پاسخ داده شده 
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;

            }

            SetRoundPanelVisibleByRequestType(Convert.ToInt32(ReqManager[0]["Type"]));
        }
    }

    private void SetRequestsMode(string PageModePersianName)
    {
        SetEnable(true);
        RoundPanelAccounting.Visible = true;
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;

        FillForm(_OfId);
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(_OfId, 1, -1, 1);
        FillDocumentReq(ReqManager);

        ClearRequestInfo();

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        RoundPanelOffice.HeaderText = "جدید_درخواست " + PageModePersianName;
        lblRegDateComment.Visible = true;
        lblRegDateComment.Text = "تاریخ ها به صورت پیش فرض مربوط بهدآخرین پروانه تایید شده شرکت می باشد.";
        MenuDetails.Enabled = false;
        if (_PageMode == "DocumentInvalid")
            SetEnable(false);
        if (_PageMode == "ChangeShareHolderAndBaseInfo")
        {
            DisableForChangeBaseInfo();
            SetRoundPanelVisibleByRequestType((int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo);
        }
    }

    #endregion

    private void SetRoundPanelVisibleByRequestType(int Type)
    {
        if (Type == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
                || Type == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo
              || Type == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
        {
            cmbActivityType.ClientVisible = false;
            lblActivityType.ClientVisible = false;
            RoundPanelDocumentBasicInfo.Visible = false;
            RoundPanelAccounting.Visible = false;
        }
    }

    #region Accounting Fish
    protected TSP.DataManager.TechnicalServices.AccountingManager CreateAccountingManager()
    {
        TSP.DataManager.TechnicalServices.AccountingManager manager = new TSP.DataManager.TechnicalServices.AccountingManager();
        return manager;
    }

    protected void FillAccountingGrid()
    {
        if (Session["AccountingManager"] != null)
        {
            if (Utility.IsDBNullOrNullValue(Utility.DecryptQS(Request.QueryString["OfReId"])))
                return;
            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            AccountingManager.FindByTableTypeId(_OfReId, TableType);
            GridViewAccounting.DataSource = AccountingManager.DataTable;
            GridViewAccounting.DataBind();
            Session["AccountingManager"] = AccountingManager;
        }
    }

    protected void BindAccountingGrid()
    {
        if (Session["AccountingManager"] != null)
        {
            GridViewAccounting.DataSource = ((TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"]).DataTable;
            GridViewAccounting.DataBind();
        }
    }

    protected void RowInserting()
    {
        if (Session["AccountingManager"] == null)
            return;

        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];

        DataRow dr = AccountingManager.NewRow();

        dr.BeginEdit();
        dr["TableTypeId"] = -1;
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
        dr["Type"] = (int)TSP.DataManager.AccountingPaymentType.Fiche;
        dr["Bank"] = DBNull.Value;
        dr["BranchCode"] = DBNull.Value;
        dr["BranchName"] = DBNull.Value;
        dr["AccType"] = cmbAccType.Value;
        dr["AccTypeName"] = cmbAccType.Text;
        dr["Number"] = txtaNumber.Text;
        dr["Date"] = txtaDate.Text;
        dr["Amount"] = txtaAmount.Text;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;

        dr.EndEdit();
        AccountingManager.AddRow(dr);

        GridViewAccounting.DataSource = AccountingManager.DataTable;
        GridViewAccounting.DataBind();
        ClearFormAccounting();
    }

    protected void ClearFormAccounting()
    {
        txtaAmount.Text = "";
        txtaDate.Text = "";
        txtaNumber.Text = "";
        txtaDesc.Text = "";
    }

    private void SetLabelRegEnter()
    {
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.OfficeDocument.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
        {
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ريال بابت صدور/تمدید پروانه شخص حقوقی باید پرداخت شود.";
            HiddenFieldOffice["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
        }
    }

    private void SetAccountingFilterExpression()
    {
        ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.OfficeDocument).ToString();
    }
    #endregion

    #region Save Image
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

    #region FillForm

    protected Boolean FillForm(int OfId)
    {
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count != 1)
        {
            // ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            ShowMessage("کد عضویت شرکت مورد نظر معتبر نمی باشد");
            return false;
        }
        // txtOfId.Text = OfId.ToString();
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
        txtOfRegDate.Text = OffManager[0]["RegDate"].ToString();//RegOfDate in tblOfficeRequest == RegDate in tblOffice
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

    /// <summary>
    /// it uses in setMode functions
    /// </summary>
    /// <param name="OfId"></param>
    /// <param name="OfReId"></param>
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
            RoundPanelOffice.HeaderText = "مشخصات درخواست _ درخواست " + ReqManager[0]["TypeName"].ToString();
            txtOfId.Text = ReqManager[0]["OfId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
                txtMeNo.Text = ReqManager[0]["MeNo"].ToString();
            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
            {
                RoundPanelFileAttachment.Visible = true;
                //RoundPanelFileAttachment.Enabled = false;
                TblFile.Visible = false;
                //DisableForChangeBaseInfo();
            }
            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست های مربوط به پروانه باشد
            {
                FillDocumentReq(ReqManager);
                FillAccountingGrid();
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

            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
          
            ComboBoxGrade.DataBind();          
            if (!string.IsNullOrEmpty(ReqManager[0]["GrdId"].ToString()))
                ComboBoxGrade.SelectedIndex = ComboBoxGrade.Items.FindByValue(ReqManager[0]["GrdId"].ToString()).Index;
            else
            {
                lblGrade.ClientVisible = ComboBoxGrade.ClientVisible = false;
            }
            if (Convert.ToInt32(ReqManager[0]["MFType"]) != (int)TSP.DataManager.EngOfficeType.Implimentation)
                lblGrade.ClientVisible = ComboBoxGrade.ClientVisible = false;
            else
                lblGrade.ClientVisible = ComboBoxGrade.ClientVisible = true;
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
            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid
                || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.DocumentInvalid)
            {
                SetEnable(false);
                if (_PageMode == "Edit")
                    txtReRequestDesc.Enabled = true;
            }

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
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void FillDocumentReq(TSP.DataManager.OfficeRequestManager ReqManager)
    {
        if (ReqManager.Count < 0)
            return;
        //int OfReId = Convert.ToInt32(ReqManager[0]["OfReId"]);
        RoundPanelFileAttachment.Visible = true;
        txtOfId.Text = ReqManager[0]["OfId"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
            txtMeNo.Text = ReqManager[0]["MeNo"].ToString();

        #region //***********Document GRade*****************
        if (ReqManager[0]["MFType"].ToString() == "1")//شرکت طراح و ناظر
        {
            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
            cmbActivityType.SelectedIndex = -1;
            lblActivityType.ClientVisible = false;
            cmbActivityType.ClientVisible = false;
        }
        else if (ReqManager[0]["MFType"].ToString() == "2")//شرکت اجرا
        {
            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
            lblActivityType.ClientVisible = true;
            cmbActivityType.ClientVisible = true;
        }
        #endregion

        txtReRequestDesc.Text = ReqManager[0]["RequestDesc"].ToString();
        txtFileNo.Text = ReqManager[0]["MFNo"].ToString();
        txtFollowCode.Text = ReqManager[0]["FollowCode"].ToString();

        txtdExpDate.Visible = true;
        txtdLastRegDate.Visible = true;
        txtdSerialNo.Visible = true;
        cmbdIsTemporary.Visible = true;

        txtdExpDate.Text = ReqManager[0]["ExpireDate"].ToString();
        txtdLastRegDate.Text = ReqManager[0]["RegDate"].ToString();
        txtdSerialNo.Text = ReqManager[0]["SerialNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
        {
            if (Convert.ToBoolean(ReqManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = 1;
            else
                cmbdIsTemporary.SelectedIndex = 0;
        }
    }

    #endregion

    #region Insert

    protected void InsertReqDocument(int OfId)
    {
        if (IsPageRefresh)
            return;
        #region Check Conditions
        if (Session["AccountingManager"] == null)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        if (AccountingManager.Count == 0)
        {
            ShowMessage("ورود اطلاعات فیش الزامی است");
            return;
        }
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.OfficeDocument.ToString(), Utility.GetCurrentUser_AgentId());
        decimal TotalAmount = 0;
        decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        for (int i = 0; i < AccountingManager.Count; i++)
        {
            TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
        }

        if (TotalCost > TotalAmount)
        {
            ShowMessage("مجموع مبالغ واریزی کمتر از هزینه صدور/المثنی/تمدید پروانه است");
            return;
        }
        #endregion

        #region Define Manager
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        trans.Add(AccountingManager);
        trans.Add(CostSettingsManager);
        trans.Add(OfficeManager);
        #endregion
        try
        {
            if (drdOfType.Value == null)
            {
                ShowMessage("نوع شرکت را انتخاب نمایید.");
                return;
            }
            if (ComboDocType.Value == null)
            {
                ShowMessage("نوع پروانه را انتخاب نمایید");
                return;
            }
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = "";
            //string MFMjCode = "0000000";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage("خطایی در بازخوانی اطلاعات انجام گرفته است");
                return;
            }
            if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.EngOfficeType.Implimentation && (ComboBoxGrade.SelectedItem == null || ComboBoxGrade.SelectedItem.Value == null))
            {
                ShowMessage("پایه شرکت را انتخاب نمایید.");
                return;
            }
            string Alert = "";
            //***Check Member Condition
            Alert = CheckMembersByOfficeType(OfId, Convert.ToInt32(ComboDocType.Value), Convert.ToInt32(drdOfType.Value));
            trans.BeginSave();
            OfficeManager.FindByCode(OfId);
            OfficeManager[0].BeginEdit();
            OfficeManager[0]["DocumentStatus"] = (int)TSP.DataManager.OfficeDocumentStatus.Pending;
            OfficeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OfficeManager[0].EndEdit();
            if (OfficeManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            #region Insert New Request
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
            drReq["Type"] = (int)TSP.DataManager.OfficeRequestType.SaveFileDocument;//درخواست صدور پروانه
            drReq["UserId"] = Utility.GetCurrentUser_UserId();

            drReq["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                drReq["SerialNo"] = txtdSerialNo.Text;
            drReq["ExpireDate"] = txtdExpDate.Text;
            if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                drReq["IsTemp"] = 0;
            else
                drReq["IsTemp"] = 1;

            drReq["ModifiedDate"] = DateTime.Now;
            drReq["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;

            drReq["MFType"] = ComboDocType.Value;
            if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
            {
                MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                cmbActivityType.ClientVisible = false;
            }
            else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
            {
                MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                cmbActivityType.ClientVisible = true;
            }
            if (ComboBoxGrade.SelectedItem != null && ComboBoxGrade.SelectedItem.Value != null)
                drReq["GrdId"] = ComboBoxGrade.SelectedItem.Value;
            else drReq["GrdId"] = DBNull.Value;

            drReq["LetterNo"] = "";
            drReq["LetterDate"] = "";
            drReq["RegPlaceId"] = Utility.GetCurrentProvinceId();//استان فارس
            drReq["PrId"] = Utility.GetCurrentProvinceId();//استان فارس

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

            drReq["OfficeDescription"] = txtOfDesc.Text;
            drReq["RequestDesc"] = txtReRequestDesc.Text;
            drReq["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;

            ReqManager.AddRow(drReq);

            if (ReqManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            ReqManager.DataTable.AcceptChanges();
            #endregion

            _OfReId = Convert.ToInt32(ReqManager[0]["OfReId"]);
            #region Accounting Fish
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
            if (TableType == -1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                trans.CancelSave();
                return;
            }

            for (int i = 0; i < AccountingManager.Count; i++)
            {
                AccountingManager[i].BeginEdit();
                AccountingManager[i]["TableType"] = TableType;
                AccountingManager[i]["TableTypeId"] = Convert.ToInt32(ReqManager[ReqManager.Count - 1]["OfReId"]);
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();

            #endregion

            string MFNo = SetOfficeMfNo(OfId, int.Parse(ReqManager[0]["OfReId"].ToString()), PrCode, MFCode, ReqManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }
            #region SaveImg
            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    drImg["RefTable"] = ReqManager[0]["OfReId"];
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
            #endregion

            #region Insert WF
            int TableId = int.Parse(ReqManager[0]["OfReId"].ToString());
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
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
            if (WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0) <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
                }
            }

            #region Do Next Task Of Insert
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
            txtFileNo.Text = MFNo;// MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
            _PageMode = "Edit";
            RoundPanelOffice.HeaderText = "ویرایش";
            RoundPanelFileAttachment.Visible = true;
            trans.EndSave();
            ShowMessage("ذخیره انجام شد" + Alert);
            CheckColor(OfId);
            #endregion
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
        #region Move Images
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                //imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Session["FileOfSign2"].ToString();

            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                //imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Session["FileOfArm2"].ToString();

            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        #endregion
    }

    private void InsertNewRequest(int OfId, TSP.DataManager.OfficeRequestType OfficeRequestType)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = null;
        if ((int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
            || (int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
            || (int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Revival)
        {
            #region CheckFish Validation
            if (Session["AccountingManager"] == null)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }
            AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            if (AccountingManager.Count == 0)
            {
                ShowMessage("ورود اطلاعات فیش الزامی است");
                return;
            }
            TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.OfficeDocument.ToString(), Utility.GetCurrentUser_AgentId());
            decimal TotalAmount = 0;
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            for (int i = 0; i < AccountingManager.Count; i++)
            {
                TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
            }

            if (TotalCost > TotalAmount)
            {
                ShowMessage("مجموع مبالغ واریزی کمتر از هزینه صدور/المثنی/تمدید پروانه است");
                return;
            }
            #endregion
        }
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.OfficeRequestManager ReqManager2 = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(attachManager);
        trans.Add(ReqManager2);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        #endregion
        try
        {
            trans.BeginSave();

            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = "";
            string MFNo = "";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                trans.CancelSave();
                ShowMessage("خطایی در بازخوانی اطلاعات انجام گرفته است");
                return;
            }


            ReqManager2.FindByOfficeId(OfId, 0, -1);
            if (ReqManager2.Count > 0)
            {
                trans.CancelSave();
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            ReqManager2.FindByOfficeId(OfId, 1, -1, 1);
            if (ReqManager2.Count == 0)
            {
                trans.CancelSave();
                ShowMessage("امکان ذخیره اطلاعات وجود ندارد.عضو حقوقی انتخاب شده دارای پروانه تایید شده نمی باشد.");
                return;
            }

            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);// (int)TSP.DataManager.TableCodes.OfficeRequest;      
            #region Insert New Request

            DataRow drReq = ReqManager2.NewRow();
            drReq["LetterNo"] = "";
            drReq["LetterDate"] = "";
            drReq["OfId"] = OfId;
            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["MFSerialNo"]))
                drReq["MFSerialNo"] = ReqManager2[0]["MFSerialNo"].ToString();
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                drReq["SerialNo"] = txtdSerialNo.Text;
            if (!string.IsNullOrWhiteSpace(txtdLastRegDate.Text))
                drReq["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdExpDate.Text))
                drReq["ExpireDate"] = txtdExpDate.Text;
            drReq["Type"] = (int)OfficeRequestType;
            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["PrId"]))
                drReq["PrId"] = ReqManager2[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["RegPlaceId"]))
                drReq["RegPlaceId"] = ReqManager2[0]["RegPlaceId"].ToString();

            drReq["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;

            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["MFNo"]))
            {
                if ((int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
                {
                    drReq["MFNo"] = ReqManager2[0]["MFNo"];
                    drReq["MFType"] = ReqManager2[0]["MFType"];
                }
                else
                {
                    MFNo = ReqManager2[0]["MFNo"].ToString();
                    string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    string Code = MFNoMajor[0];

                    if (ComboDocType.Value != null)
                    {
                        drReq["MFType"] = ComboDocType.Value;

                        if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                        {
                            MFNoMajor[0] = MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                            drReq["ActivityType"] = DBNull.Value;
                        }
                        else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
                        {
                            MFNoMajor[0] = MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                            drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
                        }


                        drReq["MFNo"] = MFNo;

                        MFNo = string.Join("-", MFNoMajor);

                        drReq["MFNo"] = MFNo;
                    }

                    else if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["MFNo"]))
                    {
                        drReq["MFNo"] = ReqManager2[0]["MFNo"];
                        drReq["MFType"] = ReqManager2[0]["MFType"];

                    }
                }
            }

            drReq["IsConfirm"] = 0;
            drReq["InActive"] = 0;
            drReq["OfficeDescription"] = txtOfDesc.Text;
            drReq["RequestDesc"] = txtReRequestDesc.Text;
            drReq["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;
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
            drReq["Requester"] = 1;//کارمند
            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

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
            if (ComboBoxGrade.SelectedItem != null && ComboBoxGrade.SelectedItem.Value != null)
                drReq["GrdId"] = ComboBoxGrade.SelectedItem.Value;
            ReqManager2.AddRow(drReq);
            int cn = ReqManager2.Save();
            if (cn <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion
            int TableId = Convert.ToInt32(ReqManager2[ReqManager2.Count - 1]["OfReId"]);
            txtFileNo.Text = ReqManager2[0]["MFNo"].ToString();
            #region Attachments
            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    drImg["RefTable"] = TableId;
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
            #endregion

            #region  WF
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
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

            string Description = GenerateWFDescription(OfficeRequestType);
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion

            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);
            if ((int)OfficeRequestType != (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
            {
                if (string.IsNullOrWhiteSpace(MFCode))
                {
                    trans.CancelSave();
                    ShowMessage("نوع پروانه نامشخص است");
                    return;
                }

                MFNo = SetOfficeMfNo(OfId, TableId, PrCode, MFCode, ReqManager2);
                if (string.IsNullOrWhiteSpace(MFNo))
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }

            if ((int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
                || (int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
                || (int)OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Revival)
            {
                #region Accounting Fish
                if (AccountingManager != null)
                {
                    for (int i = 0; i < AccountingManager.Count; i++)
                    {
                        AccountingManager[i].BeginEdit();
                        AccountingManager[i]["TableType"] = TableType;
                        AccountingManager[i]["TableTypeId"] = TableId;
                        AccountingManager[i].EndEdit();
                    }
                    AccountingManager.Save();
                    AccountingManager.DataTable.AcceptChanges();
                }
                #endregion
            }

            trans.EndSave();
            ReqManager2.FindByCode(TableId);
            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["TaskName"]))
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager2[0]["TaskName"].ToString();
            }
            #region Do Next Task Of Inserting
            txtFileNo.Text = MFNo;
            _OfReId = TableId;
            _PageMode = "Edit";
            ShowMessage("ذخیره انجام شد.");
            TSP.DataManager.Permission per = TSP.DataManager.OfficeRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            RoundPanelOffice.HeaderText = "ویرایش";
            RoundPanelFileAttachment.Visible = true;

            MenuDetails.Enabled = true;
            CheckColor(OfId);
            #endregion
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
        #region Images
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
            catch (Exception)
            {
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
            catch (Exception)
            {
            }
        }
        #endregion
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

    #region Update

    protected void EditRequest(int OfReId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManagerForDel = new TSP.DataManager.TechnicalServices.AccountingManager();

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(AccountingManager);
        trans.Add(AccountingManagerForDel);

        string pathAx = "";
        byte[] img = null;
        bool chArmEdit = false;
        bool chSignEdit = false;

        int PrId = Utility.GetCurrentProvinceId();
        ProvinceManager.FindByCode(PrId);
        string PrCode = "";
        string MFCode = "";
        string MFMjCode = "0000000";
        string Alert = "";
        try
        {
            if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.EngOfficeType.Implimentation && (ComboBoxGrade.SelectedItem == null || ComboBoxGrade.SelectedItem.Value == null))
            {
                ShowMessage("پایه شرکت را انتخاب نمایید");
                return;
            }
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage("خطایی در بازخوانی اطلاعات کد نظام مهندسی ایجاد شده است");
                return;
            }
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            //***Check Member Condition
            Alert = CheckMembersByOfficeType(Convert.ToInt32(ReqManager[0]["OfId"]), Convert.ToInt32(ComboDocType.Value), Convert.ToInt32(drdOfType.Value));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }

        try
        {

            trans.BeginSave();

            //****ReqManager.FindByCode at top*******
            int ReqType = Convert.ToInt32(ReqManager[0]["Type"]);
            if (ReqType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
           || ReqType == (int)TSP.DataManager.OfficeRequestType.Revival
           || ReqType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument)
            {

                if (AccountingManager.Count == 0)
                {
                    ShowMessage("ورود اطلاعات فیش الزامی است");
                    trans.CancelSave();
                    return;
                }
            }
            #region Edit Request
            ReqManager[0].BeginEdit();
            int OfId = Convert.ToInt32(ReqManager[0]["OfId"]);
            ReqManager[0]["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                ReqManager[0]["OfNameEn"] = txtOfNameEn.Text;
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
            if (drdOfType.Value != null)
                ReqManager[0]["OtId"] = int.Parse(drdOfType.Value.ToString());
            ReqManager[0]["Subject"] = txtOfSubject.Text;
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

                        //img = flpOfArm.FileBytes;
                        // fileNameArm = Utility.GenerateName(Path.GetExtension(flpOfArm.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        // flpOfArm.SaveAs(pathAx + fileNameArm);
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());

                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;

                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                    }

                }
                else
                {
                    try
                    {


                        //img = flpOfArm.FileBytes;
                        // fileNameArm = Utility.GenerateName(Path.GetExtension(flpOfArm.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        // flpOfArm.SaveAs(pathAx + fileNameArm);
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());

                        ReqManager[0]["ArmImage"] = img;
                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;


                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
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

                        /// img = flpOfSign.FileBytes;
                        //fileNameSign = Utility.GenerateName(Path.GetExtension(flpOfSign.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        //  flpOfSign.SaveAs(pathAx + fileNameSign);
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

                        //img = flpOfSign.FileBytes;
                        //fileNameSign = Utility.GenerateName(Path.GetExtension(flpOfSign.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        //flpOfSign.SaveAs(pathAx + fileNameSign);
                        imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());

                        ReqManager[0]["SignImage"] = img;
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
            ReqManager[0]["ConditionalApproval"] = CheckBoxConditionalApprove.Checked;
            ReqManager[0]["RequestDesc"] = txtReRequestDesc.Text;
            ReqManager[0]["OfficeDescription"] = txtOfDesc.Text;

            ReqManager[0]["LetterNo"] = "";
            ReqManager[0]["LetterDate"] = "";
            ReqManager[0]["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            ReqManager[0]["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            if (ComboDocType.Value != null)
                ReqManager[0]["MFType"] = ComboDocType.Value;
            if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.EngOfficeType.Implimentation) //( ComboBoxGrade.SelectedItem != null && ComboBoxGrade.SelectedItem.Value != null)
                ReqManager[0]["GrdId"] = ComboBoxGrade.SelectedItem.Value;
            else    ReqManager[0]["GrdId"] =DBNull.Value;
                

            ////if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
            ////{
            if (ComboDocType.Value != null)
            {
                if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                    MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
                    MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                //if (!CheckMembersByOfficeType(OfId, Convert.ToInt32(ComboDocType.Value)))
                //{
                //    trans.CancelSave();
                //    return;
                //}
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع پروانه را انتخاب نمایید";
                return;
            }
            ////}

            #region SetMFNo
            TSP.DataManager.OfficeMemberManager OffMemManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            DataTable dtOfMe = OffMemManager.SelectOfficeMember(OfId, 1, -1, 0);//return member
            if (dtOfMe.Rows.Count > 0)
            {
                for (int m = 0; m < dtOfMe.Rows.Count; m++)
                {
                    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
                    if (dtMj.Rows.Count > 0)
                    {
                        int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                        int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                        int i = -1;
                        char[] Code = MFMjCode.ToCharArray();

                        switch (ParentId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;

                        }
                        if (i != -1)
                        {
                            //MFMjCode = Code.ToString();
                            MFMjCode = new string(Code);
                        }
                    }
                    dtMj.Clear();
                }
            }



            #endregion

            string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
            while (MFSerialNo.Length < 5)
            {
                MFSerialNo = "0" + MFSerialNo;
            }

            if (string.IsNullOrWhiteSpace(MFCode))
            {
                trans.CancelSave();
                ShowMessage("نوع پروانه نامشخص است");
                return;
            }

            ReqManager[0]["MFNo"] = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;

            ReqManager[0]["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                ReqManager[0]["SerialNo"] = txtdSerialNo.Text;
            ReqManager[0]["ExpireDate"] = txtdExpDate.Text;
            if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                ReqManager[0]["IsTemp"] = 0;
            else
                ReqManager[0]["IsTemp"] = 1;

            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ReqManager[0].EndEdit();
            if (ReqManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            #region Attachment
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
            #endregion

            txtFileNo.Text = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;

            if (ReqType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
           || ReqType == (int)TSP.DataManager.OfficeRequestType.Revival
           || ReqType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument)
            {
                #region Accounting Fish
                int TableType = -1;
                TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                if (TableType == -1)
                {
                    trans.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                    return;
                }
                AccountingManagerForDel.FindByTableTypeId(OfReId, TableType);
                int cntAcc = AccountingManagerForDel.Count;
                for (int i = 0; i < cntAcc; i++)
                {
                    AccountingManagerForDel[0].Delete();
                    AccountingManagerForDel.DataTable.AcceptChanges();
                }
                AccountingManagerForDel.Save();

                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    AccountingManager[i].BeginEdit();
                    AccountingManager[i]["TableType"] = TableType;
                    AccountingManager[i]["TableTypeId"] = OfReId;
                    AccountingManager[i].EndEdit();
                }
                AccountingManager.Save();
                AccountingManager.DataTable.AcceptChanges();

                #endregion
            }
            _PageMode = "Edit";
            RoundPanelOffice.HeaderText = "ویرایش";
            ShowMessage("ذخیره انجام شد");
            trans.EndSave();
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
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
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
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                }
            }

        }
    }
    #endregion

    private string CheckMembersByOfficeType(int OfId, int MFType, int OtId)
    {
        #region CheckMembers
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        int Permission = OfficeRequestManager.CheckOfficeMemberConditions(OfId
                                                 , MFType
                                                 , OtId
                                                 , OfMeManager, DocMemberFileManager, OthPersonManager, GradeManager);
        if (Permission == 0)
            return "";
        else
            return WorkFlowPermission.FindRequestErrorMsg(Permission);

        #region Comment
        ////int min = 0;
        //bool IsDes = false;
        //bool IsObs = false;
        ////bool IsImp = false;
        //DataTable dtOffMembers = OfMeManager.FindOfficeActiveMember(OfId);
        //for (int i = 0; i < dtOffMembers.Rows.Count; i++)
        //{
        //    int MeId = int.Parse(dtOffMembers.Rows[i]["PersonId"].ToString());
        //    int OfpId = Convert.ToInt32(dtOffMembers.Rows[i]["OfpId"]);

        //    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
        //    {
        //        if (OfpId == (int)TSP.DataManager.OfficePosition.Manager || OfpId == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)//مدیر عامل
        //        {
        //            if (Convert.ToInt32(dtOffMembers.Rows[i]["OfmType"]) != (int)TSP.DataManager.OfficeMemberType.Member)
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.مدیرعامل شرکت باید تنها از بین اعضا انتخاب شود";
        //                return false;
        //            }
        //            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        //            if (dtMeFile.Rows.Count > 0)
        //            {
        //                int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

        //                DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
        //                if (dtMeDetail.Rows.Count == 0)
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت طراحی ندارد";
        //                    return false;
        //                }
        //                //else
        //                //    min += 1;
        //                DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
        //                if (dtMeDetail2.Rows.Count == 0)
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت نظارت ندارد";
        //                    return false;
        //                }
        //                //else
        //                //    min += 1;

        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.مدیر عامل شرکت دارای پروانه اشتغال به کار نمی باشد";
        //                return false;
        //            }
        //        }
        //        else if (OfpId == (int)TSP.DataManager.OfficePosition.Board)//عضو هیئت مدیره
        //        {
        //            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        //            if (dtMeFile.Rows.Count > 0)
        //            {

        //                int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
        //                DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
        //                if (dtMeDetail.Rows.Count > 0)
        //                    IsDes = true;
        //                DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
        //                if (dtMeDetail2.Rows.Count > 0)
        //                    IsObs = true;
        //            }

        //        }
        //    }
        //else if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
        //{
        //    if (Convert.ToInt32(dtOffMembers.Rows[i]["OfmType"]) != (int)TSP.DataManager.OfficeMemberType.Member)
        //        continue;
        //    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        //    if (dtMeFile.Rows.Count > 0)
        //    {
        //        if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
        //        {
        //            min += 1;
        //        }
        //    }
        //}


        //   }
        //if (MFType == 1)
        //{
        //    if (IsDes == false || IsObs == false)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
        //        return false;
        //    }
        //}
        //else if (MFType == 2)
        //{
        //    if (IsImp == false)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
        //        return false;
        //    }
        //}
        #region Commented**Check in workflow
        //if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
        //{
        //    if (min < 2)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 2 نفر از اعضای شرکت دارای صلاحیت پروانه مورد تقاضا باشند";
        //        return false;
        //    }
        //}
        #endregion
        #endregion
        #endregion
    }

    protected void SetEnable(Boolean Enable)
    {
        TblFile.Visible = Enable;
        ComboDocType.Enabled = Enable;
        cmbActivityType.ClientEnabled = Enable;
        txtOfId.Enabled = Enable;
        cmbdIsTemporary.Enabled = Enable;
        txtdLastRegDate.Enabled = Enable;
        txtdExpDate.Enabled = Enable;
        txtdSerialNo.Enabled = Enable;
        btnSetRegDate.Enabled = Enable;
        cmbMembershipRequstType.Enabled = Enable;
        cmbActivityType.ClientEnabled = Enable;
        CheckBoxConditionalApprove.ClientEnabled = Enable;
        PanelAccountingInserting.Visible = Enable;
        GridViewAccounting.Columns["Delete"].Visible = Enable;

        txtOfAddress.Enabled = Enable;
        txtOfDesc.Enabled = Enable;
        txtOfEmail.Enabled = Enable;
        txtOfFax.Enabled = Enable;
        txtOfFax_pre.Enabled = Enable;
        txtOfMobile.Enabled = Enable;
        txtOfName.Enabled = Enable;
        txtOfNameEn.ClientEnabled = Enable;
        txtOfRegDate.Enabled = Enable;
        txtOfRegNo.Enabled = Enable;
        txtOfRegPlace.Enabled = Enable;
        txtOfStock.Enabled = Enable;
        txtOfSubject.Enabled = Enable;
        txtOfTel1.Enabled = Enable;
        txtOfTel1_pre.Enabled = Enable;
        txtOfTel2.Enabled = Enable;
        txtOfTel2_pre.Enabled = Enable;
        txtOfValue.Enabled = Enable;
        txtOfWebsite.Enabled = Enable;
        drdOfType.Enabled = Enable;
        flpOfArm.ClientVisible = Enable;
        flpOfSign.ClientVisible = Enable;
        ComboBoxGrade.Enabled = Enable;
    }
    protected void DisableForChangeBaseInfo()
    {
        txtOfName.Enabled = false;
        txtOfRegDate.Enabled = false;
        txtOfRegNo.Enabled = false;
        txtOfRegPlace.Enabled = false;
        drdOfType.Enabled = false;
        cmbActivityType.Enabled = false;
        cmbMembershipRequstType.Enabled = false;
    }
    protected void ClearForm()
    {
        txtOfId.Text =
        txtMeNo.Text =
        txtOfAddress.Text =
        txtOfDesc.Text =
        txtOfEmail.Text =
        txtOfFax.Text =
        txtOfFax_pre.Text =
        txtOfMobile.Text =
        txtOfName.Text =
        txtOfNameEn.Text =
        txtOfRegDate.Text =
        txtOfRegNo.Text =
        txtOfRegPlace.Text =
        txtOfStock.Text =
        txtOfSubject.Text =
        txtOfTel1.Text =
        txtOfTel1_pre.Text =
        txtOfTel2.Text =
        txtOfTel2_pre.Text =
        txtOfValue.Text =
        txtOfWebsite.Text =
        txtOfRegDate.Text =
        txtOfAddress.Text =
        txtOfDesc.Text =
        imgOfArm.ImageUrl =
        imgOfSign.ImageUrl =
        txtdExpDate.Text =
        txtdLastRegDate.Text =
        txtdSerialNo.Text = "";

        HDFlpArm.Set("name", "0");
        HDFlpSign.Set("name", "0");

        cmbMembershipRequstType.SelectedIndex = 0;

        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
        ComboDocType.DataBind();
        ComboDocType.SelectedIndex = -1;
        cmbActivityType.SelectedIndex = -1;
        txtReRequestDesc.Text = "";
        txtFileNo.Text = "";
        ComboBoxGrade.SelectedIndex = -1;
        if (Session["TblOfReImg"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfReImg"];
            dtOfImg.Rows.Clear();
            Session["TblOfReImg"] = dtOfImg;
            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
        }

        Session["OffMenuArrayList"] = null;
        ArrayList arr = new ArrayList();
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

    protected void ClearFormForSearchOfId()
    {
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


        txtdExpDate.Text = "";
        txtdLastRegDate.Text = "";
        txtdSerialNo.Text = "";
        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
        ComboDocType.DataBind();
        ComboDocType.SelectedIndex = -1;
        cmbActivityType.SelectedIndex = -1;


        if (Session["TblOfReImg"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfReImg"];
            dtOfImg.Rows.Clear();
            Session["TblOfReImg"] = dtOfImg;
            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
        }

        Session["OffMenuArrayList"] = null;
        ArrayList arr = new ArrayList();
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

    private void ClearRequestInfo()
    {
        txtReRequestDesc.Text = "";
        txtFollowCode.Text = "";
        txtdSerialNo.Text = "";
    }

    private void ClearDocumentInfo()
    {
        ComboDocType.DataBind();
        ComboDocType.SelectedIndex = -1;
        txtFileNo.Text = "";
        txtdSerialNo.Text = "";
        txtdLastRegDate.Text = "";
        txtdExpDate.Text = "";
        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
    }

    #region Capacity-Grade
    protected DataTable GetOfficeDsgnAndObsGrade(int OfId)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dt = OfficeMemberManager.FindOfficeDsngAndObsGrade(OfId);
        return dt;
    }

    protected string GetOfficeGradeName(int OfReId)
    {
        string GrdName = "";
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        OfficeRequestManager.FindByCode(OfReId);
        if (OfficeRequestManager.Count == 1)
        {
            GrdName = OfficeRequestManager[0]["GrdName"].ToString();
        }
        return GrdName;
    }
    #endregion

    #region WF
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
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

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSaveDoc(_PageMode);
        if (_PageMode == "View" || _PageMode == "Edit")
        {
            CheckWorkFlowPermissionForEditForDoc(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSaveDoc(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
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
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForDoc(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _OfReId, TaskCode, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _OfReId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
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
                    //btnSave.Enabled = true;
                    //btnSave2.Enabled = true;
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

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
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
    }
    #endregion

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
                ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
                Session["OffMenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_OfReId);
                ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
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



        ArrayList arr = new ArrayList();
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

    private void SetMeDocDefualtRegisterDate(int DocumentSetExpireDateType)
    {
        txtdLastRegDate.Text = Utility.GetDateOfToday();
        Utility.Date Date = new Utility.Date();
        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void SetMeDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtdLastRegDate.Text))
        {
            txtdLastRegDate.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtdLastRegDate.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                // Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    /// <summary>
    /// برای درخواست صدور-تمدید-المثنی ورود فیش الزامی می باشد
    /// </summary>
    /// <param name="MfId"></param>
    /// <returns></returns>
    private Boolean CheckCanEditFishForEdit(int OfReId)
    {
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        OfficeRequestManager.FindByCode(OfReId);
        if (OfficeRequestManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }

        int OfficeRequestType = Convert.ToInt32(OfficeRequestManager[0]["Type"]);
        if (OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
            || OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
            || OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Revival)
        {
            return true;
        }
        return false;
    }

    private string GenerateWFDescription(TSP.DataManager.OfficeRequestType OfficeRequestType)
    {
        string Des = "";
        switch (OfficeRequestType)
        {
            case TSP.DataManager.OfficeRequestType.Change:
                Des = "شروع گردش کار درخواست تغییرات پروانه شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.DocumentInvalid:
                Des = "شروع گردش کار درخواست ابطال پروانه شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.Invalid:
                Des = "شروع گردش کار درخواست ابطال عضویت شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.Reduplicate:
                Des = "شروع گردش کار درخواست صدور المثنی پروانه شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.Revival:
                Des = "شروع گردش کار درخواست تمدید پروانه شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.SaveFileDocument:
                Des = "شروع گردش کار درخواست صدور پروانه شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.SaveMembershipRequset:
                Des = "شروع گردش کار درخواست تغییرات عضویت شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.SaveRequestInfo:
                Des = "شروع گردش کار درخواست ثبت عضویت شخص حقوقی";
                break;
            case TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo:
                Des = "شروع گردش کار درخواست اطلاعات پایه و سهامداران شخص حقوقی";
                break;
        }
        return Des;
    }

    private void CheckPermission()
    {

        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = btnSave2.Enabled = per.CanEdit || per.CanNew;
    }

    private string SetOfficeMfNo(int OfId, int OfReId, string PrCode, string MFCode, TSP.DataManager.OfficeRequestManager ReqManager)
    {
        string MFNo = "";
        string MFMjCode = "0000000";
        #region SetMFNo
        TSP.DataManager.OfficeMemberManager OffMemManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        DataTable dtOfMe = OffMemManager.SelectOfficeMember(OfId, 1, -1, 0);//return member
        if (dtOfMe.Rows.Count > 0)
        {
            for (int m = 0; m < dtOfMe.Rows.Count; m++)
            {
                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
                if (dtMj.Rows.Count > 0)
                {
                    //int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                    int i = -1;
                    char[] Code = MFMjCode.ToCharArray();

                    switch (ParentId)
                    {
                        case (int)TSP.DataManager.MainMajors.Architecture:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Civil:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Electronic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mapping:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mechanic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Traffic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Urbanism:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        default:
                            i = -1;
                            break;
                    }
                    if (i != -1)
                    {
                        MFMjCode = new string(Code);
                    }
                }
                dtMj.Clear();
            }
        }
        #endregion
        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count != 1)
            return "";
        string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
        while (MFSerialNo.Length < 5)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        ReqManager[0]["MFNo"] = MFNo = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
        ReqManager.Save();
        return MFNo;
    }

    private void ResetSessions()
    {
        #region Reset Sessions
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        Session["DesObsGrade"] = null;
        Session["OffMenuArrayList"] = null;

        #endregion
    }
    #endregion
}