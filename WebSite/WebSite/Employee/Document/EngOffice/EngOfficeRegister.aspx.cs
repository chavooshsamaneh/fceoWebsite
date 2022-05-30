using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.IO;

public partial class Employee_Document_EngOffice_EngOfficeRegister : System.Web.UI.Page
{
    string _PageMode
    {
        get
        {
            return PgMode.Value;
        }
        set
        {
            PgMode.Value = value;
        }
    }

    int _EngOfficeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(EngOfficeId.Value);
            }
            catch
            {
                return (Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EngOfId"])));
            }
        }
        set
        {
            EngOfficeId.Value = value.ToString();
        }
    }

    int _EngFileId
    {
        get
        {
            try
            {
                return Convert.ToInt32(EngFileId.Value);
            }
            catch
            {
                return (Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EOfId"])));
            }
        }
        set
        {
            EngFileId.Value = value.ToString();
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        txtaNumber.Attributes["onkeyup"] = "return ltr_override(event)";
        txtLetterNo.Attributes["onkeyup"] = "return ltr_override(event)";
        txtDaftarNo.Attributes["onkeyup"] = "return ltr_override(event)";

        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                SetPermission();
                SetKey();
                Session["AccountingManager"] = null;
                Session["AccountingManager"] = CreateAccountingManager();

                SetAccountingFilterExpression();
                SetLabelRegEnter();

                SetMode();

                BindAccountingGrid();
                CheckWorkFlowPermission();

                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = BtnNew.Enabled;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (!Utility.IsDBNullOrNullValue(Session["ImgOwnership"]))
        {
            ImageOwnership.NavigateUrl = Session["ImgOwnership"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["Imgpartnership"]))
        {
            Imagepartnership.NavigateUrl = Session["Imgpartnership"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgPartnerDisclaimer"]))
        {
            ImagePartnerDisclaimer.NavigateUrl = Session["ImgPartnerDisclaimer"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgInqueryMembers"]))
        {
            ImageInqueryMembers.NavigateUrl = Session["ImgInqueryMembers"].ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _EngOfficeId != null && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("EngOffice.aspx?PostId=" + Utility.EncryptQS(_EngOfficeId.ToString()) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("EngOffice.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  string PageMode = Utility.DecryptQS(PgMode.Value);
        //  string EOfId = Utility.DecryptQS(EngFileId.Value);

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            switch (_PageMode)
            {
                case "New":
                    Insert();

                    break;
                case "Edit":
                    if (_EngFileId == null || _EngFileId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        Edit(_EngFileId);
                    break;
                case "Change":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.Change);
                    break;

                case "ChangeBaseInfo":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.ChangeBaseInfo);
                    break;

                case "Revival":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.Revival);
                    break;

                case "Reduplicate":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.Reduplicate);
                    break;

                case "InValid":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.Invalid);
                    break;
                case "Activate":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.Activate);
                    break;

                case "ConditionalAprrove":
                    if (_EngFileId == null || _EngFileId == -1 || _EngOfficeId == null || _EngOfficeId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        InsertNewRequest(_EngOfficeId, _EngFileId, TSP.DataManager.EngOffFileType.ConditionalAprrove);
                    break;
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        _EngFileId = -1;
        _PageMode = "New";
        RoundPanelRequest.HeaderText = "جدید";
        ASPxMenu1.Enabled = false;

        ClearForm();
        SetEnabled(true);
        //SetNewMode();

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // string EOfId = Utility.DecryptQS(EngFileId.Value);
        //string OfId = Utility.DecryptQS(OfficeId.Value);

        if (_EngFileId == null || _EngFileId == -1)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (CheckPermitionForEdit(_EngFileId))
            {
                SetEnabled(true);
                txtRequestDesc.Enabled = true;
                TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                _PageMode = "Edit";
                RoundPanelRequest.HeaderText = "ویرایش";
                ASPxMenu1.Enabled = true;
            }
            else
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.");
            }

        }

    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + Utility.EncryptQS(_EngOfficeId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&EOfId=" + Utility.EncryptQS(_EngFileId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + Utility.EncryptQS(_EngOfficeId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&EOfId=" + Utility.EncryptQS(_EngFileId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + Utility.EncryptQS(_EngOfficeId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&EOfId=" + Utility.EncryptQS(_EngFileId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void CallbackPanelReq_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }

    protected void FileUploadOwnership_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "Ownership");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadpartnership_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "partnership");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadPartnerDisclaimer_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "PartnerDisclaimer");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadInqueryMembers_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile, "InqueryMembers");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

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
                ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text);
                if (Convert.ToBoolean(ArrayAcc[0]) == false)
                {
                    GridViewAccounting.JSProperties["cpMessage"] = ArrayAcc[1].ToString();// "این شماره فیش قبلا در سیستم ثبت شده است";
                    GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
                }
                else
                {
                    RowInserting();
                    GridViewAccounting.JSProperties["cpSaveComplete"] = "1";
                }
            }
            catch (Exception)
            {
                GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
            }
        }
    }

    protected void CallbackAccFish_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ClearFormAccounting();
        SetAccountingFilterExpression();
        SetLabelRegEnter();
    }
    #endregion
    #endregion

    #region Methods
    protected string SaveImage(UploadedFile uploadedFile, string ImageUploadType)
    {
        string ret = "";
        string tempFileName = "";
        if (uploadedFile.IsValid)
        {
            switch (ImageUploadType)
            {
                case "Ownership":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _EngOfficeId.ToString() + "Own" + _EngFileId.ToString() + "Own" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/EngOffice/Ownership/") + ret) == true);
                    tempFileName = "~/image/EngOffice/Ownership/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["ImgOwnership"] = tempFileName;
                    #endregion
                    break;
                case "partnership":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _EngOfficeId.ToString() + "part" + _EngFileId.ToString() + "part" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/EngOffice/partnership/") + ret) == true);
                    tempFileName = "~/image/EngOffice/partnership/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["Imgpartnership"] = tempFileName;
                    #endregion
                    break;

                case "PartnerDisclaimer":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _EngOfficeId.ToString() + "PrDis" + _EngFileId.ToString() + "PrDis" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/EngOffice/PartnerDisclaimer/") + ret) == true);
                    tempFileName = "~/image/EngOffice/PartnerDisclaimer/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["ImgPartnerDisclaimer"] = tempFileName;
                    #endregion
                    break;
                case "InqueryMembers":
                    #region
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _EngOfficeId.ToString() + "InqofMe" + _EngFileId.ToString() + "InqofMe" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/image/EngOffice/InqueryMembers/") + ret) == true);
                    tempFileName = "~/image/EngOffice/InqueryMembers/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["ImgInqueryMembers"] = tempFileName;
                    #endregion
                    break;

            }
        }
        return ret;
    }

    private void SetKey()
    {
        try
        {
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
            _EngOfficeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EngOfId"].ToString()));
            _EngFileId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EOfId"].ToString()));

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
       TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        switch (_PageMode)
        {
            case "View":
                #region View
                SetEnabled(false);
                txtFileNo.Enabled = false;
                txtLetterDate.Enabled = false;
                txtLetterNo.Enabled = false;
                if (_EngFileId == null || _EngOfficeId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(_EngFileId);
                InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), _EngFileId);

                #endregion
                break;
            case "New":
                #region New
                SetEnabled(true);

                Session["AccountingManager"] = CreateAccountingManager();
   
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ClearForm();
                ASPxMenu1.Enabled = false;
                #endregion
                break;

            case "Edit":
                SetEditMode();
                break;
            case "Change":
                 #region Change
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.Change);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
            case "ChangeBaseInfo":
                #region ChangeBaseInfo
                FillForm(_EngFileId);
                DisableForChangeBaseInfo();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.ChangeBaseInfo);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
            case "Revival":
                #region Revival
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                if (cmbdIsTemporary.SelectedItem != null && cmbdIsTemporary.SelectedItem.Value != null)
                    SetMeDocDefualtRegisterDate(Convert.ToInt32(cmbdIsTemporary.SelectedItem.Value));
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.Revival);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
            case "Reduplicate":
                #region Reduplicate
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.Reduplicate);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
            case "InValid":
                #region InValid
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.Invalid);
                ASPxMenu1.Enabled = false;
                SetEnabled(false);
                txtRequestDesc.Text = "";
                #endregion
                break;
            case "Activate":
                #region Activate
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.Activate);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
            case"ConditionalAprrove":
                #region ConditionalAprrove
                FillForm(_EngFileId);
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.EngOffFileType.ConditionalAprrove);
                ASPxMenu1.Enabled = false;
                txtRequestDesc.Text = "";
                #endregion
                break;
        }
    }
    private void SetEditMode()
    {
        #region Edit
        SetEnabled(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        if (_EngFileId == null || _EngOfficeId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        _PageMode = "Edit";
        FillForm(_EngFileId);
        InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), _EngFileId);

        RoundPanelRequest.HeaderText = "ویرایش";
        #endregion

    }
    private void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanEdit || per.CanNew;
        btnSave2.Enabled = per.CanNew || per.CanEdit;
    }

    protected void ClearForm()
    {
        txtFirstRegDate.Text =
        txtEngOffName.Text =
        txtAddress.Text =
        txtDaftarLoc.Text =
        txtDaftarNo.Text =
        txtDesc.Text =
        txtFax.Text =
        txtFileNo.Text =
        txtLetterDate.Text =
        txtLetterNo.Text =
        txtTel.Text =
        txtExpDate.Text =
        txtLastRegDate.Text =
        txtdSerialNo.Text =
        txtRequestDesc.Text =
        txtEmail.Text =
        txtAddress.Text =
        txtMobileNo.Text =
        txtTel.Text = "";
        ComboEOfTId.DataBind();
        ComboEOfTId.SelectedIndex = 0;
        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
    }

    protected void SetEnabled(Boolean Enabled)
    {
        txtAddress.Enabled =
        txtDaftarLoc.Enabled =
        txtDaftarNo.Enabled =
        txtDesc.Enabled =
        txtFax.Enabled =
        txtLetterDate.Enabled =
        txtLetterNo.Enabled =
        txtTel.Enabled =
        ComboEOfTId.Enabled =
        txtMobileNo.Enabled =
        txtEmail.Enabled =
        txtEngOffName.Enabled =
        txtaDesc.Enabled =
        PanelAccountingInserting.Visible =
        GridViewAccounting.Columns["Delete"].Visible =
        cmbdIsTemporary.Enabled =
        txtLastRegDate.Enabled =
        txtExpDate.Enabled =
        txtdSerialNo.Enabled =
        FileUploadImagepartnership.Enabled =
            FileUploadOwnership.Enabled =
            FileUploadPartnerDisclaimer.Enabled = FileUploadImageInqueryMembers.Enabled = Enabled;
    }

    protected void DisableForChangeBaseInfo()
    {
        txtDaftarLoc.Enabled = false;
        txtDaftarNo.Enabled = false;
        txtDesc.Enabled = false;
        txtLetterDate.Enabled = false;
        txtLetterNo.Enabled = false;
        ComboEOfTId.Enabled = false;
        PanelAccountingInserting.Visible = false;
        GridViewAccounting.Columns["Delete"].Visible = false;

        cmbdIsTemporary.Enabled = false;
        txtLastRegDate.Enabled = false;
        txtExpDate.Enabled = false;
        txtdSerialNo.Enabled = false;
    }

    private void FillForm(int EOfId)
    {
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();

        fileManager.FindByCode(EOfId);
        if (fileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["EOfTId"]))
            {
                ComboEOfTId.DataBind();
                ComboEOfTId.SelectedIndex = ComboEOfTId.Items.IndexOfValue(fileManager[0]["EOfTId"].ToString());
            }
            txtEngOffId.Text = fileManager[0]["EngOfId"].ToString();
            txtLetterNo.Text = fileManager[0]["ParticipateLetterNo"].ToString();
            txtLetterDate.Text = fileManager[0]["ParticipateLetterDate"].ToString();
            txtDaftarNo.Text = fileManager[0]["EngOffNo"].ToString();
            txtDaftarLoc.Text = fileManager[0]["EngOffLoc"].ToString();
            txtFileNo.Text = fileManager[0]["FileNo"].ToString();
            txtDesc.Text = fileManager[0]["Description"].ToString();
            txtRequestDesc.Text = fileManager[0]["RequestDesc"].ToString();
            txtdSerialNo.Text = fileManager[0]["SerialNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(Convert.ToInt32(fileManager[0]["IsTemp"]).ToString()).Index;
            txtExpDate.Text = fileManager[0]["ExpireDate"].ToString();//**تاریخ پایان اعتبار
            txtLastRegDate.Text = fileManager[0]["RegDate"].ToString();//**تاریخ صدور درخواست
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["FirstRegDate"]))//**تاریخ اولین صدور
                txtFirstRegDate.Text = fileManager[0]["FirstRegDate"].ToString();
            txtEmail.Text = fileManager[0]["Email"].ToString();
            txtFax.Text = fileManager[0]["FaxNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["TellNo"]))
                txtTel.Text = fileManager[0]["TellNo"].ToString();
            txtMobileNo.Text = fileManager[0]["MobileNo"].ToString();
            txtEngOffName.Text = fileManager[0]["EngOffName"].ToString();
            HiddenFieldEngOffice["OriginalEngOffName"] = fileManager[0]["EngOffName"].ToString();
            txtAddress.Text = fileManager[0]["Address"].ToString();

            if (!Utility.IsDBNullOrNullValue(fileManager[0]["ImageOwnership"]))
            {
                ImageOwnership.NavigateUrl = fileManager[0]["ImageOwnership"].ToString();
            }
            else ImageOwnership.NavigateUrl = "";
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["Imagepartnership"]))
            {
                Imagepartnership.NavigateUrl = fileManager[0]["Imagepartnership"].ToString();
            }
            else Imagepartnership.NavigateUrl = "";
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["ImagePartnerDisclaimer"]))
            {
                ImagePartnerDisclaimer.NavigateUrl = fileManager[0]["ImagePartnerDisclaimer"].ToString();
            }
            else ImagePartnerDisclaimer.NavigateUrl = "";
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["ImageInqueryMembers"]))
            {
                ImageInqueryMembers.NavigateUrl = fileManager[0]["ImageInqueryMembers"].ToString();
            }
            else ImageInqueryMembers.NavigateUrl = "";

            FillAccountingGrid();

            if (!Utility.IsDBNullOrNullValue(fileManager[0]["Type"]))
            {
                SetRoundpanelRequestHeaderAndLabales(Convert.ToInt32(fileManager[0]["Type"]));
                if (Convert.ToInt32(fileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo
                    || Convert.ToInt32(fileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.ConditionalAprrove)
                {
                    if ((_PageMode == "ChangeBaseInfo") || (_PageMode == "Edit") || (_PageMode == "ConditionalAprrove"))
                    {
                        DisableForChangeBaseInfo();
                        txtFax.Enabled = true;
                        txtTel.Enabled = true;
                        txtMobileNo.Enabled = true;
                        txtEmail.Enabled = true;
                        txtEngOffName.Enabled = true;
                    }
                }

                else  if (Convert.ToInt32(fileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)
                {
                    if (_PageMode != "Activate")
                    {
                        SetEnabled(false);
                    }
                }               
            }
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        }

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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
                    return true;
            }
        }
        return false;

    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            CheckWorkFlowPermissionForSave(_PageMode);
            if (_PageMode == "View" || _PageMode == "Edit")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        int Permission = -1;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
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
            ShowMessage("شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.");
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _EngFileId, TaskCode, Utility.GetCurrentUser_UserId());
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
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
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
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده اطلاعات دفتر", Utility.GetCurrentUser_UserId());
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

    protected void Insert()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        if (AccountingManager.Count == 0)
        {
            ShowMessage("ورود اطلاعات فیش الزامی است");
            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.EngOfficeManager EngOffManagerTemp = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        trans.Add(EngOffManager);
        trans.Add(fileManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            EngOffManagerTemp.SelectEngOfficeByName(txtEngOffName.Text.Trim());
            if (EngOffManagerTemp.Count > 0)
            {
                ShowMessage("نام دفتر وارد شده تکراری می باشد");
                return;
            }
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            string MFMjCode = "0000000";
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }

            int TaskId = -1;
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            string PerDate = Utility.GetDateOfToday();
            #region Insert EngOffice
            DataRow Offrow = EngOffManager.NewRow();
            if (ComboEOfTId.Value != null)
                Offrow["EOfTId"] = ComboEOfTId.Value;
            Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
            if (!Utility.IsDBNullOrNullValue(txtLetterDate.Text))
                Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
            else
                Offrow["ParticipateLetterDate"] = DBNull.Value;
            Offrow["EngOffNo"] = txtDaftarNo.Text;
            Offrow["EngOffLoc"] = txtDaftarLoc.Text;
            Offrow["Description"] = txtDesc.Text;
            Offrow["CreateDate"] = PerDate;
            Offrow["InActive"] = 0;
            Offrow["Address"] = txtAddress.Text;
            if (!Utility.IsDBNullOrNullValue(txtTel.Text))
                Offrow["TellNo"] = txtTel.Text;
            Offrow["MobileNo"] = txtMobileNo.Text;
            Offrow["FaxNo"] = txtFax.Text;
            Offrow["Email"] = txtEmail.Text;
            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
            Offrow["UserId"] = Utility.GetCurrentUser_UserId();
            Offrow["ModifiedDate"] = DateTime.Now;
            Offrow["IsConfirm"] = 0;

            EngOffManager.AddRow(Offrow);

            trans.BeginSave();
            if (EngOffManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            _EngOfficeId = int.Parse(EngOffManager[0]["EngOfId"].ToString());
            #endregion

            #region Insert EngOffFile
            DataRow drFile = fileManager.NewRow();
            drFile["EngOfId"] = _EngOfficeId;
            drFile["Type"] = (int)TSP.DataManager.EngOffFileType.SaveFileDocument;//صدور
            if (ComboEOfTId.Value != null)
                drFile["EOfTId"] = ComboEOfTId.Value;
            drFile["ParticipateLetterNo"] = txtLetterNo.Text.Trim();
            if (!Utility.IsDBNullOrNullValue(txtLetterDate.Text))
                drFile["ParticipateLetterDate"] = txtLetterDate.Text;
            else
                drFile["ParticipateLetterDate"] = DBNull.Value;

            drFile["EngOffNo"] = txtDaftarNo.Text.Trim();
            drFile["EngOffLoc"] = txtDaftarLoc.Text;
            drFile["Description"] = txtDesc.Text;
            drFile["RequestDesc"] = txtRequestDesc.Text;
            drFile["CreateDate"] = PerDate;
            drFile["Requester"] = 1;//Employee
            drFile["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);
            drFile["InActive"] = 0;
            drFile["UserId"] = Utility.GetCurrentUser_UserId();
            drFile["ModifiedDate"] = DateTime.Now;
            drFile["IsConfirm"] = 0;
            drFile["FileLetterNo"] = "";
            drFile["FileLetterDate"] = "";
            drFile["RegPlaceId"] = Utility.GetCurrentProvinceId();//استان فارس
            drFile["PrId"] = Utility.GetCurrentProvinceId();//استان فارس         
            drFile["EngOffName"] = txtEngOffName.Text.Trim();
            drFile["Address"] = txtAddress.Text;
            if (!Utility.IsDBNullOrNullValue(txtTel.Text))
                drFile["TellNo"] = txtTel.Text;
            drFile["MobileNo"] = txtMobileNo.Text;
            drFile["FaxNo"] = txtFax.Text;
            drFile["Email"] = txtEmail.Text;
            if (!string.IsNullOrWhiteSpace(txtLastRegDate.Text))
                drFile["RegDate"] = txtLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                drFile["SerialNo"] = txtdSerialNo.Text;
            if (!string.IsNullOrWhiteSpace(txtExpDate.Text))
                drFile["ExpireDate"] = txtExpDate.Text;
            if (cmbdIsTemporary.SelectedItem != null)
            {
                if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                    drFile["IsTemp"] = 0;
                else
                    drFile["IsTemp"] = 1;
            }
            if (Session["ImgOwnership"] != null)
                drFile["ImageOwnership"] = Session["ImgOwnership"];
            if (Session["Imgpartnership"] != null)
                drFile["Imagepartnership"] = Session["Imgpartnership"];
            if (Session["ImgPartnerDisclaimer"] != null)
                drFile["ImagePartnerDisclaimer"] = Session["ImgPartnerDisclaimer"];
            if (Session["ImgInqueryMembers"] != null)
                drFile["ImageInqueryMembers"] = Session["ImgInqueryMembers"];

            fileManager.AddRow(drFile);
            if (fileManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            fileManager.DataTable.AcceptChanges();
            string MFSerialNo = fileManager[0]["MFSerialNo"].ToString();
            while (MFSerialNo.Length < 5)
            {
                MFSerialNo = "0" + MFSerialNo;
            }
            fileManager[0]["FileNo"] = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
            fileManager.Save();

            txtFileNo.Text = fileManager[0]["FileNo"].ToString();

            _EngFileId = int.Parse(fileManager[0]["EOfId"].ToString());
            #endregion


            int CurrentNmcId = FindNmcId(TaskId);
            int WfStart = WorkFlowStateManager.StartWorkFlow(_EngFileId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #region Accounting Fish
            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
            if (TableType == -1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }

            for (int i = 0; i < AccountingManager.Count; i++)
            {
                AccountingManager[i].BeginEdit();
                AccountingManager[i]["TableType"] = TableType;
                AccountingManager[i]["TableTypeId"] = _EngFileId;
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();

            #endregion
            trans.EndSave();
            lblFileNo.Visible = true;
            txtFileNo.Visible = true;
            ASPxMenu1.Enabled = true;
            
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            SetEditMode();
            Session["ImgOwnership"] = null;
            Session["Imgpartnership"] = null;
            Session["ImgPartnerDisclaimer"] = null;
            Session["ImgInqueryMembers"] = null;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
            Utility.SaveWebsiteError(err);
        }
    }

    protected void Edit(int EOfId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManagerForDel = new TSP.DataManager.TechnicalServices.AccountingManager();
        if (_PageMode == "Revival" || _PageMode == "Reduplicate")
        {
            if (AccountingManager.Count == 0)
            {
                ShowMessage("ورود اطلاعات فیش الزامی است");
                return;
            }
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOfficeManager EngOffManagerTemp = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        trans.Add(OffMemberManager);
        trans.Add(EngOfficeManager);
        trans.Add(fileManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(AccountingManager);
        trans.Add(AccountingManagerForDel);

        try
        {
            if (HiddenFieldEngOffice["OriginalEngOffName"].ToString() != txtEngOffName.Text.Trim())
            {
                EngOffManagerTemp.SelectEngOfficeByName(txtEngOffName.Text.Trim());
                if (EngOffManagerTemp.Count > 0)
                {
                    ShowMessage("نام دفتر وارد شده تکراری می باشد");
                    return;
                }
            }

            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }

            trans.BeginSave();

            #region Accounting Fish
            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
            if (TableType == -1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            AccountingManagerForDel.FindByTableTypeId(EOfId, TableType);
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
                AccountingManager[i]["TableTypeId"] = EOfId;
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();

            #endregion

            #region OfficeFile
            fileManager.FindByCode(EOfId);

            if (fileManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                trans.CancelSave();
                return;
            }
            fileManager[0].BeginEdit();
            if (ComboEOfTId.Value != null)
                fileManager[0]["EOfTId"] = ComboEOfTId.Value;
            fileManager[0]["ParticipateLetterNo"] = txtLetterNo.Text;
            if (!Utility.IsDBNullOrNullValue(txtLetterDate.Text))
                fileManager[0]["ParticipateLetterDate"] = txtLetterDate.Text;
            else
                fileManager[0]["ParticipateLetterDate"] = DBNull.Value;

            fileManager[0]["EngOffNo"] = txtDaftarNo.Text.Trim();
            fileManager[0]["EngOffLoc"] = txtDaftarLoc.Text;
            fileManager[0]["Description"] = txtDesc.Text;
            fileManager[0]["RequestDesc"] = txtRequestDesc.Text;
            fileManager[0]["EngOffName"] = txtEngOffName.Text.Trim();
            fileManager[0]["Address"] = txtAddress.Text;
            if (!Utility.IsDBNullOrNullValue(txtTel.Text))
                fileManager[0]["TellNo"] = txtTel.Text;
            fileManager[0]["MobileNo"] = txtMobileNo.Text;
            fileManager[0]["FaxNo"] = txtFax.Text;
            fileManager[0]["Email"] = txtEmail.Text;

            if (!string.IsNullOrWhiteSpace(txtLastRegDate.Text))
                fileManager[0]["RegDate"] = txtLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                fileManager[0]["SerialNo"] = txtdSerialNo.Text;

            if (!string.IsNullOrWhiteSpace(txtExpDate.Text))
                fileManager[0]["ExpireDate"] = txtExpDate.Text;
            if (cmbdIsTemporary.SelectedItem != null)
            {
                if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                    fileManager[0]["IsTemp"] = 0;
                else
                    fileManager[0]["IsTemp"] = 1;
            }

            if (Session["ImgOwnership"] != null)
                fileManager[0]["ImageOwnership"] = Session["ImgOwnership"].ToString();
            if (Session["Imgpartnership"] != null)
                fileManager[0]["Imagepartnership"] = Session["Imgpartnership"].ToString();
            if (Session["ImgPartnerDisclaimer"] != null)
                fileManager[0]["ImagePartnerDisclaimer"] = Session["ImgPartnerDisclaimer"].ToString();
            if (Session["ImgInqueryMembers"] != null)
                fileManager[0]["ImageInqueryMembers"] = Session["ImgInqueryMembers"].ToString();

            fileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            fileManager[0]["ModifiedDate"] = DateTime.Now;
            fileManager[0].EndEdit();
            if (fileManager.Save() <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                trans.CancelSave();
                return;
            }
            #endregion

            int EngOfId = Convert.ToInt32(fileManager[0]["EngOfId"]);
            if (Convert.ToInt32(fileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.SaveFileDocument)
            {
                if (!EditEngOffice(EngOfId, EngOfficeManager))
                {
                    trans.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }
            string MFNo = SetOfficeMfNo(EngOfId, EOfId, PrCode, MFCode, fileManager, MeMjManager, OffMemberManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            trans.EndSave();
            txtFileNo.Text = MFNo;
            _PageMode = "Edit";
            HiddenFieldEngOffice["OriginalEngOffName"] = txtEngOffName.Text.Trim();
            RoundPanelRequest.HeaderText = "ویرایش";
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            Session["ImgOwnership"] = null;
            Session["Imgpartnership"] = null;
            Session["ImgPartnerDisclaimer"] = null;
            Session["ImgInqueryMembers"] = null;
        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    if (Utility.ShowExceptionError())
                        this.LabelWarning.Text += err.Message;
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                if (Utility.ShowExceptionError())
                    this.LabelWarning.Text += err.Message;
            }

            Utility.SaveWebsiteError(err);
        }
    }

    private void InsertNewRequest(int EngOfId, int EOfId, TSP.DataManager.EngOffFileType EngOffFileType)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = null;
        AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        if (EngOffFileType == TSP.DataManager.EngOffFileType.Revival
            || EngOffFileType == TSP.DataManager.EngOffFileType.Reduplicate)
        {
            #region CheckFish Validation
            if (Session["AccountingManager"] == null)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }
            if (AccountingManager.Count == 0)
            {
                ShowMessage("ورود اطلاعات فیش الزامی است");
                return;
            }
            TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
            cmbAccType.DataBind();
            if (cmbAccType.SelectedItem.Value != null && Convert.ToInt32(cmbAccType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument)
                CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.EngOfficeDocument.ToString(), Utility.GetCurrentUser_AgentId());
            else if (cmbAccType.SelectedItem.Value != null && Convert.ToInt32(cmbAccType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument2)
                CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.EngOfficeDocument2.ToString(), Utility.GetCurrentUser_AgentId());

            decimal TotalAmount = 0;
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            for (int i = 0; i < AccountingManager.Count; i++)
            {
                TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
            }

            if (TotalCost > TotalAmount)
            {
                ShowMessage("مجموع مبالغ واریزی کمتر از هزینه صدور/تمدید دفاتر است");
                return;
            }
            #endregion
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOffFileManager FileManager2 = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.EngOfficeTypeManager EngOfficeTypeManager = new TSP.DataManager.EngOfficeTypeManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        trans.Add(FileManager);
        trans.Add(FileManager2);
        trans.Add(WorkFlowStateManager);
        trans.Add(WorkFlowTaskManager);
        trans.Add(AccountingManager);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            trans.BeginSave();
            FileManager.FindByEngOffCode(EngOfId, 0, -1);
            if (FileManager.Count > 0)
            {
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            FileManager.FindByCode(EOfId);
            if (FileManager.Count != 1)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return;
            }

            if (EngOffFileType != TSP.DataManager.EngOffFileType.Invalid)
            {
                EngOfficeTypeManager.FindByCode(Convert.ToInt32(ComboEOfTId.Value));
                if (EngOfficeTypeManager.Count == 1)
                {
                    if (Convert.ToBoolean(EngOfficeTypeManager[0]["InActive"]))
                    {
                        ShowMessage("نوع دفتر انتخاب شده معتبر نمی باشد");
                        return;
                    }
                }
                else
                {
                    ShowMessage("خطایی در انتخاب نوع دفتر ایجاد شده است");
                    return;
                }
            }

            DataRow Offrow = FileManager2.NewRow();

            Offrow["EngOfId"] = EngOfId;
            if (ComboEOfTId.Value != null)
                Offrow["EOfTId"] = ComboEOfTId.Value;
            Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
            if (!Utility.IsDBNullOrNullValue(txtLetterDate.Text))
                Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
            else
                Offrow["ParticipateLetterDate"] = DBNull.Value;

            Offrow["EngOffNo"] = txtDaftarNo.Text;
            Offrow["EngOffLoc"] = txtDaftarLoc.Text;
            Offrow["Description"] = txtDesc.Text;
            Offrow["RequestDesc"] = txtRequestDesc.Text;
            Offrow["CreateDate"] = Utility.GetDateOfToday();
            Offrow["InActive"] = 0;
            Offrow["UserId"] = Utility.GetCurrentUser_UserId();
            Offrow["ModifiedDate"] = DateTime.Now;
            Offrow["IsConfirm"] = 0;
            Offrow["FileLetterNo"] = "";
            Offrow["FileLetterDate"] = "";
            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
            Offrow["Address"] = txtAddress.Text;
            if (!Utility.IsDBNullOrNullValue(txtTel.Text))
                Offrow["TellNo"] = txtTel.Text;
            Offrow["MobileNo"] = txtMobileNo.Text;
            Offrow["FaxNo"] = txtFax.Text;
            Offrow["Email"] = txtEmail.Text;

            if (!Utility.IsDBNullOrNullValue(FileManager[0]["MFSerialNo"]))
                Offrow["MFSerialNo"] = FileManager[0]["MFSerialNo"].ToString();
            if (!string.IsNullOrEmpty(txtLastRegDate.Text))
                Offrow["RegDate"] = txtLastRegDate.Text;
            if (!string.IsNullOrEmpty(txtExpDate.Text))
                Offrow["ExpireDate"] = txtExpDate.Text;
            Offrow["Type"] = (int)EngOffFileType;
            if (!Utility.IsDBNullOrNullValue(FileManager[0]["PrId"]))
                Offrow["PrId"] = FileManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegPlaceId"]))
                Offrow["RegPlaceId"] = FileManager[0]["RegPlaceId"].ToString();
            if (!Utility.IsDBNullOrNullValue(FileManager[0]["FileNo"]))
                Offrow["FileNo"] = FileManager[0]["FileNo"].ToString();

            Offrow["CreateDate"] = Utility.GetDateOfToday();
            Offrow["Requester"] = 1;//کارمند
            Offrow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);

            if (Session["ImgOwnership"] != null)
                Offrow["ImageOwnership"] = Session["ImgOwnership"];
            if (Session["Imgpartnership"] != null)
                Offrow["Imagepartnership"] = Session["Imgpartnership"];
            if (Session["ImgPartnerDisclaimer"] != null)
                Offrow["ImagePartnerDisclaimer"] = Session["ImgPartnerDisclaimer"];
            if (Session["ImgInqueryMembers"] != null)
                Offrow["ImageInqueryMembers"] = Session["ImgInqueryMembers"];

            FileManager2.AddRow(Offrow);
            if (FileManager2.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            txtFileNo.Text = FileManager2[0]["FileNo"].ToString();

            int EOfId2 = int.Parse(FileManager2[FileManager2.Count - 1]["EOfId"].ToString());

            int TableId = Convert.ToInt32(FileManager2[FileManager2.Count - 1]["EOfId"]);

            if (EngOffFileType == TSP.DataManager.EngOffFileType.Revival
                 || EngOffFileType == TSP.DataManager.EngOffFileType.Change
                 || EngOffFileType == TSP.DataManager.EngOffFileType.Reduplicate
                 || EngOffFileType == TSP.DataManager.EngOffFileType.Activate
                || EngOffFileType == TSP.DataManager.EngOffFileType.ConditionalAprrove)
            {
                #region Accounting Fish
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);

                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    AccountingManager[i].BeginEdit();
                    AccountingManager[i]["TableType"] = TableType;
                    if (Convert.ToInt32(AccountingManager[i]["TableTypeId"]) == -1)
                        AccountingManager[i]["TableTypeId"] = TableId;
                    AccountingManager[i].EndEdit();
                }
                AccountingManager.Save();
                AccountingManager.DataTable.AcceptChanges();

                #endregion
            }

            #region  WF
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
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
            string Description = GenerateWFDescriptionByRequest(EngOffFileType);
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion

            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, EngOfId, TableId);

            string MFNo = SetOfficeMfNo(EngOfId, TableId, PrCode, MFCode, FileManager2, MeMjManager, OffMemberManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));

            TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            _EngFileId = EOfId2;
            _PageMode = "Edit";
            txtFileNo.Text = MFNo;
            RoundPanelRequest.HeaderText = "ویرایش";
            ASPxMenu1.Enabled = true;
            SetEditMode();
            Session["ImgOwnership"] = null;
            Session["Imgpartnership"] = null;
            Session["ImgPartnerDisclaimer"] = null;
            Session["ImgInqueryMembers"] = null;
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
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
    }

    protected Boolean EditEngOffice(int EngOfId, TSP.DataManager.EngOfficeManager EngOfficeManager)
    {
        EngOfficeManager.FindByCode(EngOfId);

        if (EngOfficeManager.Count == 1)
        {
            EngOfficeManager[0].BeginEdit();
            if (ComboEOfTId.Value != null)
                EngOfficeManager[0]["EOfTId"] = ComboEOfTId.Value;
            EngOfficeManager[0]["ParticipateLetterNo"] = txtLetterNo.Text;
            if (!Utility.IsDBNullOrNullValue(txtLetterDate.Text))
                EngOfficeManager[0]["ParticipateLetterDate"] = txtLetterDate.Text;
            else
                EngOfficeManager[0]["ParticipateLetterDate"] = DBNull.Value;

            EngOfficeManager[0]["EngOffNo"] = txtDaftarNo.Text;
            EngOfficeManager[0]["EngOffLoc"] = txtDaftarLoc.Text;
            EngOfficeManager[0]["Description"] = txtDesc.Text;
            EngOfficeManager[0]["EngOffName"] = txtEngOffName.Text.Trim();
            EngOfficeManager[0]["Address"] = txtAddress.Text;
            if (!Utility.IsDBNullOrNullValue(txtTel.Text))
                EngOfficeManager[0]["TellNo"] = txtTel.Text;
            EngOfficeManager[0]["MobileNo"] = txtMobileNo.Text;
            EngOfficeManager[0]["FaxNo"] = txtFax.Text;
            EngOfficeManager[0]["Email"] = txtEmail.Text;
            EngOfficeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            EngOfficeManager[0]["ModifiedDate"] = DateTime.Now;
            EngOfficeManager[0].EndEdit();
            if (EngOfficeManager.Save() > 0)
            {
                return true;
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return false;
            }
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
            return false;
        }
    }

    protected void InsertInActtiveOfficeMembers(TSP.DataManager.OfficeMemberManager OffMemberManager, TSP.DataManager.DocMemberFileManager DocMemberFileManager, TSP.DataManager.RequestInActivesManager ReqInActiveManager, int EngOfId, int EOfId)
    {
        int MemberFileId = -1;
        int MeId = -1;
        int OfmMfId = -1;
        DataTable dtEngOffMembers = OffMemberManager.FindEngOfficeActiveMembers(EngOfId, (int)TSP.DataManager.OfficeMemberType.Member, 0, 1);
        if (dtEngOffMembers.Rows.Count > 0)
        {
            for (int i = 0; i < dtEngOffMembers.Rows.Count; i++)
            {
                MeId = Convert.ToInt32(dtEngOffMembers.Rows[i]["PersonId"]);
                if (!Utility.IsDBNullOrNullValue(dtEngOffMembers.Rows[i]["MfId"]))
                    OfmMfId = Convert.ToInt32(dtEngOffMembers.Rows[i]["MfId"]);

                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                    if (OfmMfId != MemberFileId)
                    {
                        DataRow drOfm = OffMemberManager.NewRow();
                        drOfm["OfReId"] = EOfId;
                        drOfm["MfId"] = MemberFileId;
                        drOfm["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
                        drOfm["PersonId"] = MeId;
                        drOfm["OfKind"] = 1;
                        drOfm["SignUrl"] = dtEngOffMembers.Rows[i]["SignUrl"];
                        drOfm["OfId"] = EngOfId;
                        drOfm["OfpId"] = dtEngOffMembers.Rows[i]["OfpId"];
                        drOfm["StartDate"] = dtEngOffMembers.Rows[i]["StartDate"];
                        drOfm["HasSignRight"] = dtEngOffMembers.Rows[i]["HasSignRight"];
                        drOfm["IsFullTime"] = dtEngOffMembers.Rows[i]["IsFullTime"];
                        drOfm["Description"] = dtEngOffMembers.Rows[i]["Description"];
                        drOfm["UserId"] = Utility.GetCurrentUser_UserId();
                        drOfm["ModifiedDate"] = DateTime.Now;
                        OffMemberManager.AddRow(drOfm);

                        DataRow dr = ReqInActiveManager.NewRow();
                        dr["TableId"] = dtEngOffMembers.Rows[i]["OfmId"];
                        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
                        dr["ReqId"] = EOfId;
                        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
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
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);// (int)TSP.DataManager.TableType.EngOffice;           
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            AccountingManager.FindByTableTypeId(_EngFileId, TableType);
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
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffice);
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
        // ClearFormAccounting();
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
        cmbAccType.DataBind();
        if (cmbAccType.SelectedItem.Value != null && Convert.ToInt32(cmbAccType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument)
            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.EngOfficeDocument.ToString(), Utility.GetCurrentUser_AgentId());
        else if (cmbAccType.SelectedItem.Value != null && Convert.ToInt32(cmbAccType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument2)
            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.EngOfficeDocument2.ToString(), Utility.GetCurrentUser_AgentId());

        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
        {
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ريال بابت صدور/تمدید پروانه دفاتر باید پرداخت شود.";
            HiddenFieldEngOffice["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
        }
    }

    private void SetAccountingFilterExpression()
    {
        ObjectDataSourceAccType.FilterExpression = "AccTypeId IN ( " + ((int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument).ToString() +
            "," + ((int)TSP.DataManager.TSAccountingAccType.EngOfficeDocument2).ToString() + ")";
        //  cmbAccType.DataBind();
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    #endregion

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        //this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetMeDocDefualtRegisterDate(int DocumentSetExpireDateType)
    {
        txtLastRegDate.Text = Utility.GetDateOfToday();
        Utility.Date Date = new Utility.Date();
        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void SetMeDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtLastRegDate.Text))
        {
            txtLastRegDate.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtLastRegDate.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                // Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void SetRoundpanelRequestHeaderAndLabales(int DocumentOfMemberRequestType)
    {
        string RegDateComment = "";
        switch (DocumentOfMemberRequestType)
        {
            case (int)TSP.DataManager.EngOffFileType.SaveFileDocument:
                RoundPanelRequest.HeaderText += "-درخواست صدور";
                lblRegDate.Text = "تاریخ صدور";
                RegDateComment = "تاریخ صدور به صورت پیش فرض تاریخ ثبت درخواست و تاریخ پایان اعتبار سه سال بعد می باشد ";
                break;
            case (int)TSP.DataManager.EngOffFileType.Reduplicate:
                RoundPanelRequest.HeaderText += "-درخواست صدور المثنی";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RegDateComment = "";

                break;
            case (int)TSP.DataManager.EngOffFileType.Revival:
                RoundPanelRequest.HeaderText += "-درخواست تمدید";
                lblRegDate.Text = "تاریخ تمدید";
                RegDateComment = "تاریخ تمدید به صورت پیش فرض تاریخ ثبت درخواست و تاریخ پایان اعتبار سه سال پس از آن می باشد";
                break;
            case (int)TSP.DataManager.EngOffFileType.Change:
                RoundPanelRequest.HeaderText += "-درخواست تغییرات";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
            case (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo:
                RoundPanelRequest.HeaderText += "-درخواست تغییرات اطلاعات پایه";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RegDateComment = "";
                break;
            case (int)TSP.DataManager.EngOffFileType.Invalid:
                RoundPanelRequest.HeaderText += "-درخواست ابطال";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RegDateComment = "";
                break;
            case (int)TSP.DataManager.EngOffFileType.Activate:
                RoundPanelRequest.HeaderText += "-درخواست احیاء مجدد";
                lblRegDate.Text = "تاریخ صدور";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
            case (int)TSP.DataManager.EngOffFileType.ConditionalAprrove:
                RoundPanelRequest.HeaderText += "-درخواست تایید مشروط";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
                
        }
        if (!string.IsNullOrEmpty(RegDateComment))
        {
            lblRegDateComment.Visible = true;
            lblRegDateComment.Text = RegDateComment;
        }
        else
            lblRegDateComment.Visible = false;
        lblWarningText.Visible = ImgWarningMsg.ClientVisible = false;
        TSP.DataManager.OfficeMemberManager OfficeMeManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dtEngOffMember;
        if (_EngFileId != -1)
            dtEngOffMember = OfficeMeManager.selectEngOfficeMemberByEOfId(_EngFileId);
        else
            dtEngOffMember = OfficeMeManager.SelectLastRequestOfficeMember(_EngOfficeId, 0, -1);
        for (int i = 0; i < dtEngOffMember.Rows.Count; i++)
        {
            ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(Convert.ToInt32(dtEngOffMember.Rows[i]["PersonId"]), _EngOfficeId, TSP.DataManager.OfficeMemberKind.EngOffice);
            if (!Convert.ToBoolean(ResultMembershipanother[0]))
            {
                lblWarningText.Text += ResultMembershipanother[2].ToString();
                lblWarningText.Visible = ImgWarningMsg.ClientVisible = true;
                Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
            }
        }

    }

    private string GenerateWFDescriptionByRequest(TSP.DataManager.EngOffFileType EngOffFileType)
    {
        string Description = "";
        switch (EngOffFileType)
        {
            case TSP.DataManager.EngOffFileType.Change:
                Description = "آغاز گردش کار درخواست تغییرات پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.ChangeBaseInfo:
                Description = "آغاز گردش کار درخواست تغییرات اطلاعات پایه پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.Invalid:
                Description = "آغاز گردش کار درخواست ابطال پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.Reduplicate:
                Description = "آغاز گردش کار درخواست المثنی پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.Revival:
                Description = "آغاز گردش کار درخواست تمدید پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.SaveFileDocument:
                Description = "آغاز گردش کار صدور پروانه دفتر";
                break;
            case TSP.DataManager.EngOffFileType.ConditionalAprrove:
                Description = "آغاز گردش کار درخواست تایید مشروط پروانه دفتر";
                break;
        }
        return Description;
    }

    private string SetOfficeMfNo(int EngOfId, int EOfId, string PrCode, string MFCode, TSP.DataManager.EngOffFileManager fileManager, TSP.DataManager.DocMemberFileMajorManager MeMjManager, TSP.DataManager.OfficeMemberManager OffMemberManager)
    {
        string MFNo = "";
        string MFMjCode = "0000000";
        #region SetMFNo
        DataTable dtOfMe = OffMemberManager.selectActiveEngOfficeMember(EOfId, EngOfId);//return member
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
        fileManager.FindByCode(EOfId);
        if (fileManager.Count != 1)
            return "";
        string MFSerialNo = fileManager[0]["MFSerialNo"].ToString();
        while (MFSerialNo.Length < 5)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        fileManager[0]["FileNo"] = MFNo = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
        fileManager.Save();
        return MFNo;
    }
    #endregion
}