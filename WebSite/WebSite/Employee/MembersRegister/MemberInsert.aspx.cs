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
using System.Drawing;
using DevExpress.Web;
using System.IO;

public partial class Employee_MembersRegister_MemberInsert : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;

    DataTable dtOfImg = null;
    int _IsMeTemp
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldInfo["IsMeTemp"]);
            }
            catch
            {
                return -1;
            }
        }
        set
        {
            HiddenFieldInfo["IsMeTemp"] = value;
        }
    }
    int _MeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldInfo["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString())));
            }
        }
        set
        {
            HiddenFieldInfo["MeId"] = value;
        }
    }
    int _MReId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldInfo["MReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MReId"].ToString())));
            }
        }
        set
        {
            HiddenFieldInfo["MReId"] = value;
        }
    }
    string _PageMode
    {
        get
        {
            // return Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
            return HiddenFieldInfo["PageMode"].ToString();

        }
        set
        {
            HiddenFieldInfo["PageMode"] = value;
        }
    }
    int _RequestType
    {
        get
        {
            return Convert.ToInt32(HiddenFieldInfo["RequestType"]);

        }
        set
        {
            HiddenFieldInfo["RequestType"] = value;
        }
    }
    //int TypeReqNo
    //{
    //    get;
    //    set;
    //}

    string _MeImgUpload
    {
        get
        {
            try { return HiddenFieldInfo["MeImgUpload"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldInfo["MeImgUpload"] = value;
        }
    }
    string _MeImgSign
    {
        get
        {
            try { return HiddenFieldInfo["MeImgSign"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldInfo["MeImgSign"] = value;
        }
    }

    string _MeImageIdNo
    {
        get
        {
            try { return HiddenFieldInfo["MeImageIdNo"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["MeImageIdNo"] = value;
        }
    }
    string _MeImageIdNoP2
    {
        get
        {
            try { return HiddenFieldInfo["MeImageIdNoP2"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["MeImageIdNoP2"] = value;
        }
    }
    string _MeImageIdNoPDes
    {
        get
        {
            try { return HiddenFieldInfo["MeImageIdNoDes"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["MeImageIdNoDes"] = value;
        }
    }
    string _MeImageSSN
    {
        get
        {
            try { return HiddenFieldInfo["FileOfSSN"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["FileOfSSN"] = value;
        }
    }
    string _MeImageSSNBack
    {
        get
        {
            try { return HiddenFieldInfo["FileOfSSNBack"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["FileOfSSNBack"] = value;
        }
    }
    string _MeImageSol
    {
        get
        {
            try
            {
                return HiddenFieldInfo["FileOfSol"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["FileOfSol"] = value;
        }
    }
    string _MeImageSolBack
    {
        get
        {
            try
            {
                return HiddenFieldInfo["FileOfSolBack"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["FileOfSolBack"] = value;
        }
    }
    string _MeImageResident
    {
        get
        {
            try
            {
                return HiddenFieldInfo["FileOfResident"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["FileOfResident"] = value;
        }
    }
    string _MeImageEnteghali
    {
        get
        {
            try
            {
                return HiddenFieldInfo["MeImageEnteghali"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["MeImageEnteghali"] = value;
        }
    }


    string _MeImageKardan
    {
        get
        {
            try
            {
                return HiddenFieldInfo["MeImageKardan"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldInfo["MeImageKardan"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        #region Page Refresh
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

        SetFilterExpression();
        SetLabelRegEnter();
        if (string.IsNullOrEmpty(Request.QueryString["TMe"]))
        {
            Response.Redirect("Members.aspx");
            return;
        }
        HiddenFieldInfo["TMe"] = Request.QueryString["TMe"];

        #region //حذف شده و برای اطمینان و جلوگیری از دستکاری کاربر اینجا نیز ویزیبل آنها فالس شده است***************
        lblNationality.Visible = false;
        txtNationality.Visible = false;
        #endregion

        ASPxLabelImgWarning.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";

        if (!IsPostBack)
        {
            ViewState["IsEdited_MeInfo"] = false;

            Session["AccountingManager"] = null;
            Session["AccountingManager"] = CreateAccountingManager();

            #region Reset Sessions
            Session["MeReqUpload"] =
            Session["MeStatus"] =
            Session["TblOfImg10"] =
            Session["MenuArrayList"] = null;
            #endregion

            #region Attachment's DataTable
            if (Session["TblOfImg10"] == null)
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

                Session["TblOfImg10"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfImg10"];

            FillAspxGridFlp(dtOfImg);
            #endregion

            TSP.DataManager.Permission per = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = btnSave2.Enabled = per.CanNew;
            btnPrint.Enabled = btnPrint2.Enabled = per.CanView;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = BtnNew2.Enabled = per.CanNew;

            SetKey();
            if (_PageMode != "NewMe")
            {
                TSP.DataManager.Permission Per = TSP.DataManager.MemberManager.GetUserPermissionForChangeAgent(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                drdAgent.Enabled = Per.CanEdit;
            }
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnView"] = btnPrint.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

        }
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnView"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)


            //if (_PageMode != "View")
            //{
            //    if (ChbTCheckFileNo.Checked)
            //    {
            //        PanelEntegaliDoc.ClientVisible = true;
            //    }
            //    else
            //    {
            //        PanelEntegaliDoc.ClientVisible = false;
            //    }
            //}
            this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    #region  Buttons
    protected void btnAddAccounting_Click(object sender, EventArgs e)
    {
        //if (cmbaType.Value == null)
        //{
        //    ShowMessage("نوع را انتخاب نمایید");
        //    return;
        //}
        RowInserting();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Session["MeReqUpload"] =
        Session["MeStatus"] =
        Session["TblOfImg10"] = null;

        string UserName = "";
        int IsMeTemp = -1;
        if (!Utility.IsDBNullOrNullValue(HiddenFieldInfo["TMe"]))
            IsMeTemp = Convert.ToInt32(Utility.DecryptQS(HiddenFieldInfo["TMe"].ToString()));
        if (IsMeTemp == 0)
            UserName = _MeId.ToString();
        else if (IsMeTemp == 1)
        {
            UserName = "M" + _MeId.ToString();

        }
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            if (IsMeTemp == 0)
                Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Utility.EncryptQS(_MReId.ToString()));
            else
                Response.Redirect("MembersTemp.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Utility.EncryptQS(_MReId.ToString()));
        }
        else
        {
            if (IsMeTemp == 0)
                Response.Redirect("Members.aspx?MReId=" + Utility.EncryptQS(_MReId.ToString()));
            else
                Response.Redirect("MembersTemp.aspx?MReId=" + Utility.EncryptQS(_MReId.ToString()));
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                if (_PageMode != "NewMe")
                {
                    string Msg = "";
                    if (!TSP.DataManager.MemberManager.CheckMembershipValidation(_MeId, ref Msg, true))
                    {
                        ShowMessage(Msg);
                        return;
                    }
                }
                if (_PageMode == TSP.DataManager.MemberRequestType.Request.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.ReturnToCurrentProvince.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.Dead.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.Fired.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.FakeLicense.ToString()
                    || _PageMode == TSP.DataManager.MemberRequestType.Cancel.ToString())
                {
                    InsertNewRequestForchange(_RequestType);
                }
                else if (_PageMode == "Edit")
                {
                    EditRequest(_MReId);
                }
                else if (_PageMode == "NewMe")
                {
                    if (ChEnteghali.Checked == true)
                    {
                        if (Utility.IsDBNullOrNullValue(_MeImageEnteghali))
                        {
                            ShowMessage("تصویر نامه انتقالی را انتخاب نمایید");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtTransferDate.Text))
                        {
                            ShowMessage("تاریخ انتقالی را وارد نمایید");
                            return;
                        }
                    }
                    InsertTempMember();
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ReportForms/MembersReport.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()));

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(_MeId, ref Msg, true))
        {
            ShowMessage(Msg);
            return;
        }
        if (!CheckPermitionForEdit(_MReId))
        {
            ShowMessage("تنها در وضعیت ثبت اطلاعات و در صورت داشتن سطح دسترسی گردش کار امکان ویرایش اطلاعات برای شما وجود دارد.");
            return;
        }
        TSP.DataManager.Permission per = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = btnSave2.Enabled = per.CanEdit;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        _PageMode = "Edit";
        ASPxRoundPanelMeInsert.HeaderText = "ویرایش";
        SetEditMode();

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.MemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = btnEdit.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        HiddenFieldInfo["TMe"] = Utility.EncryptQS("1");
        _MeId = -1;
        _PageMode = "New";
        _MReId = -1;
        SetNewMode();
    }
    #endregion
    protected void AspxGridFlp_PageIndexChanged(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(_MReId))
        {
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            FillAspxGridFlp(attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, _MReId, (short)TSP.DataManager.AttachType.Attachments));
        }
    }

    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        AspxGridFlp.JSProperties["cpMessage"] = "";

        FillAspxGridFlp((DataTable)Session["TblOfImg9"]);

        int Id = -1;
        if (AspxGridFlp.FocusedRowIndex > -1)
        {
            Id = AspxGridFlp.FocusedRowIndex;
        }
        if (Id == -1)
        {
            AspxGridFlp.JSProperties["cpMessage"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {
            dtOfImg = (DataTable)Session["TblOfImg9"];
            dtOfImg.Rows.Find(e.Keys["Id"]).Delete();
            Session["TblOfImg9"] = dtOfImg;
            FillAspxGridFlp((DataTable)Session["TblOfImg9"]);
            dtOfImg = (DataTable)Session["TblOfImg9"];
        }

    }

    protected void AspxGridFlp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Split('$')[0] != "Insert")
            return;

        AspxGridFlp.JSProperties["cpMessage"] = "";
        AspxGridFlp.JSProperties["cpState"] = "-1";

        if (Session["TblOfImg10"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImg10"];

            DataRow dr = dtOfImg.NewRow();

            try
            {
                if (Session["MeReqUpload"] != null)
                {

                    dr["ImgUrl"] = "~/Image/Members/MeRequest/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr["TempImgUrl"] = "~/Image/temp/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr["fileName"] = Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr["Description"] = e.Parameters.Split('$')[1];
                }
                else
                {
                    AspxGridFlp.JSProperties["cpMessage"] = "فایل مورد نظر را انتخاب نمایید";
                    AspxGridFlp.JSProperties["cpState"] = "0";
                    return;
                }


                dr["Mode"] = 0;
                dtOfImg.Rows.Add(dr);
                FillAspxGridFlp(dtOfImg);

                Session["MeReqUpload"] = null;

                AspxGridFlp.JSProperties["cpState"] = "1";

            }
            catch
            {
                AspxGridFlp.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است";
                AspxGridFlp.JSProperties["cpState"] = "0";
            }
        }
    }

    protected void MenuTop_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TP = "";
        if (Request.QueryString["TP"] != null)
        {
            TP = Request.QueryString["TP"];
        }
        int IsMeTemp = -1;
        if (!Utility.IsDBNullOrNullValue(HiddenFieldInfo["TMe"]))
            IsMeTemp = Convert.ToInt32(Utility.DecryptQS(HiddenFieldInfo["TMe"].ToString()));
        string Mode = "Request";
        if (IsMeTemp == 1)
            Mode = "TempMe";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode)
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode)
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode)
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode)
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode)
                    + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    #region FileUploads

    protected void flp_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveFileImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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

    protected void flpIdNo_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageIdNo(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpSSN_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSSN(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpSoldier_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSoldier(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FlpTLetter_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageEnteghali(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpResident_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageResident(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpKardani_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageKardani(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion
    protected void CallBackMembers_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallBackMembers.JSProperties["cpPrint"] = 0;
        string[] Parameters = e.Parameter.Split(';');
        switch (Parameters[0])
        {
            //case "Edit":
            //    btnEdit_Click();
            //break;
            case "Print":
                CallBackMembers.JSProperties["cpPrint"] = 1;
                CallBackMembers.JSProperties["cpURL"] = "../../ReportForms/MembersReport.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS("Manager") + "&Password=" + Utility.EncryptQS("-1") + "&UserId=" + Utility.EncryptQS("-1");
                break;
        }
    }

    #region Accounting Fish
    protected void GridViewAccounting_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (Session["AccountingManager"] != null)
        {
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
            GridViewAccounting.JSProperties["cpMessage"] = "";
            DataRow dr = AccountingManager.DataTable.Rows.Find(e.Keys["AccountingId"]);
            dr.Delete();
            e.Cancel = true;
            GridViewAccounting.CancelEdit();

            GridViewAccounting.DataSource = AccountingManager.DataTable;
            GridViewAccounting.DataBind();
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
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
            catch (Exception)
            {
                GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
            }
        }
    }
    #endregion
    #endregion

    #region Methods

    private void SetFilterExpression()
    {
        ObjectDataSourceAccType.FilterExpression = "AccTypeId IN (" + ((int)TSP.DataManager.TSAccountingAccType.Registeration).ToString() +
            "," + ((int)TSP.DataManager.TSAccountingAccType.Entrance).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance).ToString() + ")";
    }

    private void SetLabelRegEnter()
    {
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        decimal TotalCost = (GetFirstMembershipCost(CostSettingsManager) + GetEnteranceCost(CostSettingsManager));
        lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ریال بابت عضویت و ورود باید پرداخت شود.";
        lblReg.Text = "مبلغ: " + GetFirstMembershipCost(CostSettingsManager).ToString("#,#") + " بابت عضویت باید پرداخت شود.";
        HiddenFieldInfo["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
    }

    #region SetKey-SetMode (Request)
    private void SetKey()
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["MeId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
            _MReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
            if (string.IsNullOrEmpty(_MeId.ToString()) || string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            if (_PageMode == TSP.DataManager.MemberRequestType.Request.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.Request;
            else if (_PageMode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince;
            else if (_PageMode == TSP.DataManager.MemberRequestType.ReturnToCurrentProvince.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.ReturnToCurrentProvince;
            else if (_PageMode == TSP.DataManager.MemberRequestType.Dead.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.Dead;
            else if (_PageMode == TSP.DataManager.MemberRequestType.Fired.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.Fired;
            else if (_PageMode == TSP.DataManager.MemberRequestType.FakeLicense.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.FakeLicense;
            else if (_PageMode == TSP.DataManager.MemberRequestType.Cancel.ToString())
                _RequestType = (int)TSP.DataManager.MemberRequestType.Cancel;

            OdbProvince.FilterParameters[0].DefaultValue = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();

            #region SetPageMode
            TSP.DataManager.Permission per = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            if (_PageMode != "Request" && !per.CanView)
            {
                Response.Redirect("Members.aspx");
            }
            switch (_PageMode)
            {
                case "NewMe":
                    SetNewMode();
                    break;
                case "Request":
                case "Cancel":
                case "Dead":
                case "Fired":
                case "ReturnToCurrentProvince":
                case "TransferToOtherProvince":
                    SetRequestMode();
                    break;
                case "View":
                    SetViewMode();
                    break;
                case "Edit":
                    SetEditMode();
                    break;
            }
            #endregion

            #region checkFarsDocMe//******************************************************پروانه اشتغال به کار*****************************
            HiddenFieldInfo["HasFarsDon"] = 0;
            lblAlarmHasMeDoc.Visible = false;
            if (_MeId != -1)
            {

                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.SelectLastVersion(_MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, 1);
                if (DocMemberFileManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrIdOrigin"]) && Convert.ToInt32(DocMemberFileManager[0]["PrIdOrigin"]) == Utility.GetCurrentProvinceId())
                    {
                        HiddenFieldInfo["HasFarsDon"] = 1;
                        lblAlarmHasMeDoc.Text = "این عضو دارای پروانه اشتغال به کار با کد استان فارس می باشد.شماره پروانه " + DocMemberFileManager[0]["MfNo"].ToString();
                        lblAlarmHasMeDoc.Visible = true;
                    }

                }
            }
            #endregion

            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی بازیابی اطلاعات وجود ندارد");
            return;
        }
    }

    private void SetNewMode()
    {
        ASPxRoundPanelMeInsert.HeaderText = "جدید";
        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        ClearForm();
        SetEnteghaliControlsVisible();
        SetSoldireControlsVisible();
        SetEnabledForEditing(true);
        ASPxRoundPanelAttachFiles.Visible = false;
        lblMeStatus.Visible = txtMeStatus.Visible = false;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        MenuTop.Enabled = false;
    }

    private void SetRequestMode()
    {
        ASPxRoundPanelMeInsert.HeaderText = "جدید";
        ASPxRoundPanelAccounting.Visible = false;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        FillMemberForInsertNewRequest(_MeId);
        SetEnteghaliControlsVisible();
        SetSoldireControlsVisible();
        MenuTop.Enabled = false;
        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        SetRoundPanelHeaderByReq(-1, _PageMode);

    }

    private void SetEditMode()
    {
        _PageMode = "Edit";
        MenuTop.Enabled = true;
        ASPxRoundPanelMeInsert.HeaderText = "ویرایش";
        TSP.DataManager.Permission per = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
            btnSave.Enabled = btnSave2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        FillFormRequest(_MReId);
        SetEnabledForEditing(true);
        SetSoldireControlsVisible();
        SetEnteghaliControlsVisible();
        CheckMenuImage(_MeId, _MReId);
    }

    private void SetViewMode()
    {
        ASPxRoundPanelMeInsert.HeaderText = "مشاهده";
        TSP.DataManager.Permission per = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
            btnEdit.Enabled = btnEdit2.Enabled = true;
        btnSave.Enabled = btnSave2.Enabled = false;
        FillFormRequest(_MReId);
        SetEnabledForEditing(false);
        SetSoldireControlsVisible();
        SetEnteghaliControlsVisible();
        CheckMenuImage(_MeId, _MReId);
    }

    private void SetRoundPanelHeaderByReq(int isCreated, string Pagemode)
    {
        txtTransferMeNo.ValidationSettings.RequiredField.IsRequired = true;
        if (isCreated != (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince && Pagemode != TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString())
        {
            CmbTransferStatus.Items.Remove(CmbTransferStatus.Items.FindByValue(((int)TSP.DataManager.TransferMemberType.GoToOtherProvince).ToString()));
        }
        else
        {
            CmbTransferStatus.Items.Remove(CmbTransferStatus.Items.FindByValue(((int)TSP.DataManager.TransferMemberType.ReturnToCurrentProvince).ToString()));
            CmbTransferStatus.Items.Remove(CmbTransferStatus.Items.FindByValue(((int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince).ToString()));
            txtTransferMeNo.ValidationSettings.RequiredField.IsRequired = false;
        }
        // if (Pagemode == "NewReq")
        if (Pagemode == TSP.DataManager.MemberRequestType.Request.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست تغییرات";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Dead || Pagemode == TSP.DataManager.MemberRequestType.Dead.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست فوت شده";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.FakeLicense || Pagemode == TSP.DataManager.MemberRequestType.FakeLicense.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست ثبت مدرک جعلی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Fired || Pagemode == TSP.DataManager.MemberRequestType.Fired.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست اخراج از سازمان";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Cancel || Pagemode == TSP.DataManager.MemberRequestType.Cancel.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست لغو عضویت";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ReturnToCurrentProvince || Pagemode == TSP.DataManager.MemberRequestType.ReturnToCurrentProvince.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست بازگشت به سازمان";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince || Pagemode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString())
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست انتقال به استان دیگر";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.AgentChange)
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست تغییرات نمایندگی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.BankAccNoChange)
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست تغییرات شماره حساب";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ChangeBaseInfo)
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست تغییرات اطلاعات پایه";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ChangeLicence)
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست تغییرات مدرک تحصیلی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Create)
        {
            ASPxRoundPanelMeInsert.HeaderText += "-درخواست ثبت اولیه";
        }
    }

    #endregion    
    #region FillForms
    /// <summary>
    /// Call this methos in : New Request
    /// </summary>
    /// <param name="MeId"></param>
    protected void FillMemberForInsertNewRequest(int MeId)
    {
        try
        {
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            int MReId = -2;
            ReqManager.FindByMemberId(MeId, 0, 1, -1);
            if (ReqManager.Count > 0)
                MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
            else
            {
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return;
            }

            MeManager.FindByCode(MeId);
            if (MeManager.Count <= 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return;
            }
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["NezamKardanConfirmURL"]))
            {
                _MeImageKardan = HpKardani.NavigateUrl = MeManager[0]["NezamKardanConfirmURL"].ToString();
                HpKardani.ClientVisible = true;
                HDFlpResident.Set("Kardani", 1);
                panelKardani.ClientVisible = true;
                ChkBKardani.Checked = true;
            }
            else
            {
                _MeImageKardan = null;
                HpKardani.NavigateUrl = "";
                HpKardani.ClientVisible = false;
                HDFlpResident.Set("Kardani", 0);
                panelKardani.ClientVisible = false;
                ChkBKardani.Checked = false;
            }
            txtMeNo.Text = MeManager[0]["MeNo"].ToString();

            txtMeStatus.Text = MeManager[0]["MrsName"].ToString();
            #region Fill Base Info
            string htel = MeManager[0]["HomeTel"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeTel"]))
            {
                if (MeManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                {
                    txtHometel_cityCode.Text = MeManager[0]["HomeTel"].ToString().Substring(0, MeManager[0]["HomeTel"].ToString().IndexOf("-"));
                    txtHometel.Text = MeManager[0]["HomeTel"].ToString().Substring(MeManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MeManager[0]["HomeTel"].ToString().Length - MeManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtHometel.Text = MeManager[0]["HomeTel"].ToString();
                }
            }

            string wtel = MeManager[0]["WorkTel"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["WorkTel"]))
            {
                if (MeManager[0]["WorkTel"].ToString().IndexOf("-") > 0)
                {
                    txtWorkTel_cityCode.Text = MeManager[0]["WorkTel"].ToString().Substring(0, MeManager[0]["WorkTel"].ToString().IndexOf("-"));
                    txtWorkTel.Text = MeManager[0]["WorkTel"].ToString().Substring(MeManager[0]["WorkTel"].ToString().IndexOf("-") + 1, MeManager[0]["WorkTel"].ToString().Length - MeManager[0]["WorkTel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtWorkTel.Text = MeManager[0]["WorkTel"].ToString();
                }
            }

            string ftel = MeManager[0]["FaxNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FaxNo"]))
            {
                if (MeManager[0]["FaxNo"].ToString().IndexOf("-") > 0)
                {
                    txtFaxNo_cityCode.Text = MeManager[0]["FaxNo"].ToString().Substring(0, MeManager[0]["FaxNo"].ToString().IndexOf("-"));
                    txtFaxNo.Text = MeManager[0]["FaxNo"].ToString().Substring(MeManager[0]["FaxNo"].ToString().IndexOf("-") + 1, MeManager[0]["FaxNo"].ToString().Length - MeManager[0]["FaxNo"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtFaxNo.Text = MeManager[0]["FaxNo"].ToString();
                }
            }

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["CitId"]))
            {
                drdCitId.DataBind();
                drdCitId.SelectedIndex = drdCitId.Items.IndexOfValue(MeManager[0]["CitId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["AgentId"]))
            {
                drdAgent.DataBind();
                drdAgent.SelectedIndex = drdAgent.Items.IndexOfValue(MeManager[0]["AgentId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["SexId"]))
            {
                drdSexId.DataBind();
                drdSexId.SelectedIndex = drdSexId.Items.IndexOfValue(MeManager[0]["SexId"].ToString());
                if (Convert.ToInt32(MeManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
                {
                    if (!Utility.IsDBNullOrNullValue(MeManager[0]["SoId"]))
                    {
                        drdSoId.DataBind();
                        drdSoId.SelectedIndex = drdSoId.Items.IndexOfValue(MeManager[0]["SoId"].ToString());

                    }

                    if (!Utility.IsDBNullOrNullValue(MeManager[0]["MilitaryCommitment"]))
                    {
                        chbSoLdire.Checked = Convert.ToBoolean(MeManager[0]["MilitaryCommitment"]);
                    }
                }
            }

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["MarId"]))
            {
                drdMarId.DataBind();
                drdMarId.SelectedIndex = drdMarId.Items.IndexOfValue(MeManager[0]["MarId"].ToString());
            }

            if ((!string.IsNullOrEmpty(MeManager[0]["ImageUrl"].ToString())))
            {
                _MeImgUpload = imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();
                HDFlpMember.Set("name", 1);
            }
            if ((!string.IsNullOrEmpty(MeManager[0]["SignUrl"].ToString())))
            {
                _MeImgSign = ImgSign.ImageUrl = MeManager[0]["SignUrl"].ToString();
                HDFlpSign.Set("name", 1);

            }

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["Email"]))
                txtEmail.Text = MeManager[0]["Email"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeAdr"]))
                txtHomeAdr.Text = MeManager[0]["HomeAdr"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomePO"]))
                txtHomePO.Text = MeManager[0]["HomePO"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["LastName"]))
                txtLastName.Text = MeManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["LastNameEn"]))
                txtLastNameEn.Text = MeManager[0]["LastNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["MobileNo"]))
                txtMobileNo.Text = MeManager[0]["MobileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FirstName"]))
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FirstNameEn"]))
                txtFirstNameEn.Text = MeManager[0]["FirstNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["Website"]))
                txtWebsite.Text = MeManager[0]["Website"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["WorkAdr"]))
                txtWorkAdr.Text = MeManager[0]["WorkAdr"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["WorkPO"]))
                txtWorkPO.Text = MeManager[0]["WorkPO"].ToString();

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FatherName"]))
                txtFatherName.Text = MeManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["BirhtDate"]))
                txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["BirthPlace"]))
                txtBirhtPlace.Text = MeManager[0]["BirthPlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["IdNo"]))
                txtIdNo.Text = MeManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["IssuePlace"]))
                txtIssuePlace.Text = MeManager[0]["IssuePlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["SSN"]))
                txtSSN.Text = MeManager[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["BankAccNo"]))
                txtBankAccNo.Text = MeManager[0]["BankAccNo"].ToString();

            int comId = 0;
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["ComId"]))
            {
                comId = int.Parse(MeManager[0]["ComId"].ToString());
                chbComId.DataBind();
                int i;
                for (i = 0; i < chbComId.Items.Count; i++)
                {
                    int y = int.Parse(chbComId.Items[i].Value);

                    if ((y &= comId) == int.Parse(chbComId.Items[i].Value))
                        chbComId.Items[i].Selected = true;
                }
            }
            #endregion

            FillTransfer(MReId, false);//, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //SetTransferByType();//(true, false);

            #region Fill Attachments

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                _MeImageIdNo = HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("name", 1);

            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
            if (attachManager.Count > 0)
            {
                _MeImageIdNoP2 = HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("IdNoP2", 1);

            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
            if (attachManager.Count > 0)
            {
                _MeImageIdNoPDes = HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("IdNoPDes", 1);

            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                _MeImageSSN = HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSSN.Set("name", 1);

            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
            if (attachManager.Count > 0)
            {
                _MeImageSSNBack = HpSSNBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSSN.Set("SSNBack", 1);

            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
            if (attachManager.Count > 0)
            {
                _MeImageResident = HpResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpResident.Set("name", 1);

            }

            if (Convert.ToInt32(MeManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {
                    HpSoldier.ClientVisible = true;
                    _MeImageSol = HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
                }

                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                if (attachManager.Count > 0)
                {
                    HpSoldierBack.ClientVisible = true;
                    _MeImageSolBack = HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                }

            }
            #endregion

        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
        }

    }

    /// <summary>
    /// اطلاعات یک عضو را بر اساس درخواست انتخاب شده پر می کند
    /// it is called in following methods : InsertRequest,
    /// </summary>
    /// <param name="MReId"></param>
    protected void FillFormRequest(int MReId)
    {
        try
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.MemberConfirming, MReId, (int)TSP.DataManager.TableType.TSProject, "مشاهده اطلاعات پایه عضویت", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.ViewInfo);

            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByCode(MReId);
            if (ReqManager.Count <= 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return;
            }


            Boolean IsTemp = false;
            if (Convert.ToBoolean(ReqManager[0]["IsMeTemp"]))
            {
                IsTemp = true;
                _IsMeTemp = 1;
            }
            else
                _IsMeTemp = 0;
            _RequestType = Convert.ToInt32(ReqManager[0]["IsCreated"]);
            if (ReqManager[0]["IsCreated"].ToString() == "1")
            {
                ASPxRoundPanelAccounting.Visible = true;
                FillGrid();
                if (_PageMode == "View")
                    PanelAccountingInserting.Visible = false;
            }
            else
                PanelAccountingInserting.Visible = false;
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            #region Fill Base Info

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["NezamKardanConfirmURL"]))
            {
                _MeImageKardan = HpKardani.NavigateUrl = ReqManager[0]["NezamKardanConfirmURL"].ToString();
                HpKardani.ClientVisible = true;
                HDFlpResident.Set("Kardani", 1);
                panelKardani.ClientVisible = true;
                ChkBKardani.Checked = true;
            }
            else
            {
                _MeImageKardan = null;
                HpKardani.NavigateUrl = "";
                HpKardani.ClientVisible = false;
                HDFlpResident.Set("Kardani", 0);
                panelKardani.ClientVisible = false;
                ChkBKardani.Checked = false;
            }

            txtMeStatus.Text = ReqManager[0]["MrsName"].ToString();
            txtMeNo.Text = ReqManager[0]["MeNo"].ToString();

            string htel = ReqManager[0]["HomeTel"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomeTel"]))
            {
                if (ReqManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                {
                    txtHometel_cityCode.Text = ReqManager[0]["HomeTel"].ToString().Substring(0, ReqManager[0]["HomeTel"].ToString().IndexOf("-"));
                    txtHometel.Text = ReqManager[0]["HomeTel"].ToString().Substring(ReqManager[0]["HomeTel"].ToString().IndexOf("-") + 1, ReqManager[0]["HomeTel"].ToString().Length - ReqManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtHometel.Text = ReqManager[0]["HomeTel"].ToString();
                }
            }

            string wtel = ReqManager[0]["WorkTel"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkTel"]))
            {
                if (ReqManager[0]["WorkTel"].ToString().IndexOf("-") > 0)
                {
                    txtWorkTel_cityCode.Text = ReqManager[0]["WorkTel"].ToString().Substring(0, ReqManager[0]["WorkTel"].ToString().IndexOf("-"));
                    txtWorkTel.Text = ReqManager[0]["WorkTel"].ToString().Substring(ReqManager[0]["WorkTel"].ToString().IndexOf("-") + 1, ReqManager[0]["WorkTel"].ToString().Length - ReqManager[0]["WorkTel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtWorkTel.Text = ReqManager[0]["WorkTel"].ToString();
                }
            }

            string ftel = ReqManager[0]["FaxNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FaxNo"]))
            {
                if (ReqManager[0]["FaxNo"].ToString().IndexOf("-") > 0)
                {
                    txtFaxNo_cityCode.Text = ReqManager[0]["FaxNo"].ToString().Substring(0, ReqManager[0]["FaxNo"].ToString().IndexOf("-"));
                    txtFaxNo.Text = ReqManager[0]["FaxNo"].ToString().Substring(ReqManager[0]["FaxNo"].ToString().IndexOf("-") + 1, ReqManager[0]["FaxNo"].ToString().Length - ReqManager[0]["FaxNo"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtFaxNo.Text = ReqManager[0]["FaxNo"].ToString();
                }
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BankAccNo"]))
            {
                txtBankAccNo.Text = ReqManager[0]["BankAccNo"].ToString();
            }

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["CitId"]))
            {
                drdCitId.DataBind();
                drdCitId.SelectedIndex = drdCitId.Items.IndexOfValue(ReqManager[0]["CitId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["AgentId"]))
            {
                drdAgent.DataBind();
                drdAgent.SelectedIndex = drdAgent.Items.IndexOfValue(ReqManager[0]["AgentId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SexId"]))
            {
                drdSexId.DataBind();
                drdSexId.SelectedIndex = drdSexId.Items.IndexOfValue(ReqManager[0]["SexId"].ToString());
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SoId"]))
                {
                    drdSoId.DataBind();
                    drdSoId.SelectedIndex = drdSoId.Items.IndexOfValue(ReqManager[0]["SoId"].ToString());
                }

            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MilitaryCommitment"]))
            {
                chbSoLdire.Checked = Convert.ToBoolean(ReqManager[0]["MilitaryCommitment"]);
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MarId"]))
            {
                drdMarId.DataBind();
                drdMarId.SelectedIndex = drdMarId.Items.IndexOfValue(ReqManager[0]["MarId"].ToString());
            }

            if ((!string.IsNullOrEmpty(ReqManager[0]["ImageUrl"].ToString())))
            {
                _MeImgUpload = imgMember.ImageUrl = ReqManager[0]["ImageUrl"].ToString();
                HDFlpMember.Set("name", 1);

            }
            if ((!string.IsNullOrEmpty(ReqManager[0]["SignUrl"].ToString())))
            {
                _MeImgSign = ImgSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();
                HDFlpSign.Set("name", 1);

            }

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["Email"]))
                txtEmail.Text = ReqManager[0]["Email"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomeAdr"]))
                txtHomeAdr.Text = ReqManager[0]["HomeAdr"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomePO"]))
                txtHomePO.Text = ReqManager[0]["HomePO"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["LastName"]))
                txtLastName.Text = ReqManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["LastNameEn"]))
                txtLastNameEn.Text = ReqManager[0]["LastNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MobileNo"]))
                txtMobileNo.Text = ReqManager[0]["MobileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FirstName"]))
                txtFirstName.Text = ReqManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FirstNameEn"]))
                txtFirstNameEn.Text = ReqManager[0]["FirstNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["Website"]))
                txtWebsite.Text = ReqManager[0]["Website"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkAdr"]))
                txtWorkAdr.Text = ReqManager[0]["WorkAdr"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkPO"]))
                txtWorkPO.Text = ReqManager[0]["WorkPO"].ToString();

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FatherName"]))
                txtFatherName.Text = ReqManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BirhtDate"]))
                txtBirthDate.Text = ReqManager[0]["BirhtDate"].ToString().Trim();

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BirthPlace"]))
                txtBirhtPlace.Text = ReqManager[0]["BirthPlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IdNo"]))
                txtIdNo.Text = ReqManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IssuePlace"]))
                txtIssuePlace.Text = ReqManager[0]["IssuePlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SSN"]))
                txtSSN.Text = ReqManager[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ArchitectorCode"]))
                txtArchitectorCode.Text = ReqManager[0]["ArchitectorCode"].ToString();

            int comId = 0;
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ComId"]))
            {
                comId = int.Parse(ReqManager[0]["ComId"].ToString());
                chbComId.DataBind();
                int i;
                for (i = 0; i < chbComId.Items.Count; i++)
                {
                    int y = int.Parse(chbComId.Items[i].Value);

                    if ((y &= comId) == int.Parse(chbComId.Items[i].Value))
                        chbComId.Items[i].Selected = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["Description"]))
                txtStDesc.Text = ReqManager[0]["Description"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RequestDesc"]))
                txtReqDesc.Text = ReqManager[0]["RequestDesc"].ToString();
            #endregion


            SetRoundPanelHeaderByReq(Convert.ToInt32(ReqManager[0]["IsCreated"]), _PageMode);

            #region Transfer Info*** مورد انتقالی داشته است
            //******بایستی بر اساس درخواست جاری بیاورد و اگر نه اگر  ویرایش هم کرده باشیم مربوط به درخواست های قبلی را می آورد

            FillTransfer(MReId, false);//, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);


            #endregion

            #region Attachment Info From tblAttachment (تصویر شناسنامه ، تصویر کارت ملی ، تصویر کارت پایان خدمت)

            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            if (!IsTemp)
            {
                AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
            }
            else
            {
                AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
            }
            if (Session["TblOfImg10"] != null)
            {
                dtOfImg = (DataTable)Session["TblOfImg10"];
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
                FillAspxGridFlp(dtOfImg);
            }
            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
            }
            if (attachManager.Count > 0)
            {
                _MeImageIdNo = HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("name", 1);

            }
            else
                HpIdNo.ClientVisible = false;

            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
            }
            if (attachManager.Count > 0)
            {
                _MeImageIdNoP2 = HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("IdNoP2", 1);

            }
            else
                HIdNoP2.ClientVisible = false;


            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
            }
            if (attachManager.Count > 0)
            {
                _MeImageIdNoPDes = HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpIdNo.Set("IdNoPDes", 1);

            }
            else
                HIdNoPDes.ClientVisible = false;

            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
            }
            if (attachManager.Count > 0)
            {
                _MeImageSSN = HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSSN.Set("name", 1);
            }
            else
                HpSSN.ClientVisible = false;


            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
            }
            if (attachManager.Count > 0)
            {
                _MeImageSSNBack = HpSSNBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSSN.Set("SSNBack", 1);
            }
            else
                HpSSNBack.ClientVisible = false;


            if (!IsTemp)
            {
                attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
            }
            else
            {
                attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
            }
            if (attachManager.Count > 0)
            {
                HpResident.ClientVisible = true;
                _MeImageResident = HpResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpResident.Set("name", 1);

            }
            else
                HpResident.ClientVisible = false;

            if (Convert.ToInt32(ReqManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                if (attachManager.Count > 0)
                {
                    HpSoldier.ClientVisible = true;
                    _MeImageSol = HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HpSoldier.ClientVisible = false;

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                if (attachManager.Count > 0)
                {
                    HpSoldierBack.ClientVisible = true;
                    _MeImageSolBack = HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HpSoldierBack.ClientVisible = false;
            }
            #endregion


        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }

    }

    protected void FillTransfer(int MReId, Boolean IsTempMember)
    {
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        transferManager.FindByMemberId(MReId, -1);
        if (transferManager.Count <= 0)
        {
            ChEnteghali.Checked = false;
            return;
        }
        ChEnteghali.Checked = true;

        txtTransferDate.Text = transferManager[0]["TransferDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(transferManager[0]["TransferType"]))
        {
            CmbTransferStatus.SelectedIndex = CmbTransferStatus.Items.FindByValue(transferManager[0]["TransferType"].ToString()).Index;
        }
        else
            CmbTransferStatus.SelectedIndex = -1;
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["FileNo"]))
        {
            ChbTCheckFileNo.Checked = true;
            txtPreProvinceFileNo.Text = transferManager[0]["FileNo"].ToString();
        }
        txtTransferMeNo.Text = transferManager[0]["MeNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["Body"]))
            txtTransferBodyResone.Text = transferManager[0]["Body"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["FirstDocRegDate"]))
            txtFirstDocRegDate.Text = transferManager[0]["FirstDocRegDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocRegDate"]))
            txtCurrentDocRegDate.Text = transferManager[0]["CurrentDocRegDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocExpDate"]))
            txtCurrentDocExpDate.Text = transferManager[0]["CurrentDocExpDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["DocPrId"]))
        {
            ComboDocPreProvince.DataBind();
            ComboDocPreProvince.SelectedIndex = ComboDocPreProvince.Items.FindByValue(transferManager[0]["DocPrId"].ToString()).Index;
        }

        ComboOtherProvince.DataBind();
        ComboOtherProvince.SelectedIndex = ComboOtherProvince.Items.IndexOfValue(transferManager[0]["PrId"].ToString());
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["ImageUrl"]))
        {
            _MeImageEnteghali = ImgTransferToFars.ImageUrl = transferManager[0]["ImageUrl"].ToString();
            HDFlpLetter["name"] = 1;
        }

    }

    #endregion

    #region Inserts
    #region Insert TempMember
    protected void InsertTempMember()
    {
        if (IsPageRefresh)
            return;
        #region Check Image
        if (Utility.IsDBNullOrNullValue(_MeImgUpload))
        {
            ShowMessage("تصویر را مجدداً انتخاب نمایید");
            return;

        }
        if (Utility.IsDBNullOrNullValue(_MeImageIdNo))
        {
            ShowMessage("تصویر شناسنامه را مجدداً انتخاب نمایید");
            return;
        }
        if (Utility.IsDBNullOrNullValue(_MeImageSSN))
        {
            ShowMessage("تصویر کارت ملی را مجدداً انتخاب نمایید");
            return;
        }

        #endregion
        string fileName = "", pathAx = "", Password = "";
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();

        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        if (Session["AccountingManager"] == null)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired) + "(لطفا اطلاعات فیش را مجدد وارد نمایید)");
            GridViewAccounting.DataSource = null;
            GridViewAccounting.DataBind();
            return;
        }
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        #endregion
        if (AccountingManager.Count == 0)
        {
            ShowMessage("ورود اطلاعات فیش الزامی است");
            return;
        }

        trans.Add(TempMemberManager);
        trans.Add(transferManager);
        trans.Add(ReqManager);
        trans.Add(LogManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(CostSettingsManager);
        trans.Add(AttachManager);
        trans.Add(AccountingManager);

        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming);
        if (WorkFlowTaskManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
            return;
        }
        int WFTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
        DataRow drMember = TempMemberManager.NewRow();
        //string MReId = "";
        try
        {
            trans.BeginSave();
            #region Insert TempMember
            drMember["MeNo"] = "";
            if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
            {
                drMember["NezamKardanConfirmURL"] = _MeImageKardan;
                HpKardani.NavigateUrl = _MeImageKardan;
            }
            else
            {
                drMember["NezamKardanConfirmURL"] = DBNull.Value;
            }
            drMember["FirstName"] = txtFirstName.Text;
            drMember["LastName"] = txtLastName.Text;
            drMember["FirstNameEn"] = txtFirstNameEn.Text;
            drMember["LastNameEn"] = txtLastNameEn.Text;
            drMember["TiId"] = DBNull.Value;
            drMember["FatherName"] = txtFatherName.Text;
            drMember["BirhtDate"] = txtBirthDate.Text;
            drMember["BirthPlace"] = txtBirhtPlace.Text;
            drMember["IdNo"] = txtIdNo.Text;
            drMember["IssuePlace"] = txtIssuePlace.Text;
            drMember["SSN"] = txtSSN.Text;
            drMember["MobileNo"] = txtMobileNo.Text;
            drMember["HomeAdr"] = txtHomeAdr.Text;
            if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
                drMember["HomeTel"] = txtHometel_cityCode.Text + "-" + txtHometel.Text;
            else if (txtHometel.Text != "")
                drMember["HomeTel"] = txtHometel.Text;
            if (!string.IsNullOrEmpty(txtHomePO.Text))
                drMember["HomePO"] = txtHomePO.Text;
            else
                drMember["HomePO"] = DBNull.Value;
            drMember["WorkAdr"] = txtWorkAdr.Text;
            if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
                drMember["WorkTel"] = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
            else if (txtWorkTel.Text != "")
                drMember["WorkTel"] = txtWorkTel.Text;
            if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
                drMember["FaxNo"] = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
            else if (txtFaxNo.Text != "")
                drMember["FaxNo"] = txtFaxNo.Text;
            if (!string.IsNullOrEmpty(txtWorkPO.Text))
                drMember["WorkPO"] = txtWorkPO.Text;
            else
                drMember["WorkPO"] = DBNull.Value;

            drMember["BankAccNo"] = txtBankAccNo.Text;
            drMember["BankAccNo"] = DBNull.Value;
            drMember["AccId"] = DBNull.Value;
            drMember["MsId"] = (int)TSP.DataManager.TemporaryMemberStatus.Pending;

            if (drdSexId.Value != null)
            {
                drMember["SexId"] = Convert.ToInt32(drdSexId.Value);

                if (Convert.ToInt32(drdSexId.Value) == (int)TSP.DataManager.SexManager.Sex.Male)
                {
                    if (drdSoId.Value != null)
                        drMember["SoId"] = drdSoId.Value.ToString();
                    else
                        drMember["SoId"] = DBNull.Value;
                }

            }
            else
            {
                drMember["SexId"] = DBNull.Value;
                drMember["SoId"] = DBNull.Value;
            }

            if (drdMarId.Value != null)
                drMember["MarId"] = drdMarId.Value.ToString();
            else
                drMember["MarId"] = DBNull.Value;


            if (drdAgent.Value != null)
                drMember["AgentId"] = drdAgent.Value.ToString();
            else
                drMember["AgentId"] = DBNull.Value;
            drMember["FileNo"] = DBNull.Value;
            drMember["RelId"] = DBNull.Value;
            int comId = 0;
            for (int i = 0; i < chbComId.Items.Count; i++)
            {
                if (chbComId.Items[i].Selected)
                    comId = comId + int.Parse(chbComId.Items[i].Value.ToString());

            }
            if (comId > 0)
                drMember["ComId"] = comId;
            else
                drMember["ComId"] = 0;
            drMember["AtId"] = DBNull.Value;

            drMember["CitId"] = drdCitId.Value;
            drMember["Nationality"] = txtNationality.Text;
            drMember["Website"] = txtWebsite.Text;
            drMember["Email"] = txtEmail.Text;

            string PerDate = Utility.GetDateOfToday();
            drMember["CreateDate"] = PerDate;
            /////////??????????????drMember["Description"] = txtDesc.Text;
            drMember["UserId"] = Utility.GetCurrentUser_UserId();
            drMember["ModifiedDate"] = DateTime.Now;
            drMember["InActive"] = 0;
            TempMemberManager.AddRow(drMember);
            #endregion

            if (TempMemberManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            TempMemberManager.DataTable.AcceptChanges();

            _MeId = int.Parse(TempMemberManager[0]["TMeId"].ToString());
            #region UserLog
            Password = InsertLogin(LogManager, _MeId, TempMemberManager[0]["IdNo"].ToString(), TempMemberManager[0]["Email"].ToString());
            #endregion
            #region Request
            InsertRequest(ReqManager, _MeId, comId, WFTaskId);
            #endregion
            #region Enteghali
            if (ChEnteghali.Checked)
                InsertTransferMember_Request(transferManager, _MReId, true, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            #endregion
            #region Set ImageURL In tblTempMember and tblRequestMember
            TempMemberManager[0].BeginEdit();
            ReqManager[0].BeginEdit();
            if (!Utility.IsDBNullOrNullValue(_MeImgUpload))
            {
                TempMemberManager[0]["ImageUrl"] = "~/Image/Members/Person/Request/" + "TmpMeImage" + _MeId + "TmpMeImage" + Path.GetExtension(_MeImgUpload);
                ReqManager[0]["ImageUrl"] = "~/Image/Members/Person/Request/" + "TmpMeImage" + _MeId + "TmpMeImage" + Path.GetExtension(_MeImgUpload);
            }
            if (!Utility.IsDBNullOrNullValue(_MeImgSign))
            {
                TempMemberManager[0]["SignUrl"] = "~/Image/Members/Sign/Request/" + "TmpMeSign" + _MeId + "TmpMeSign" + Path.GetExtension(_MeImgSign);
                ReqManager[0]["SignUrl"] = "~/Image/Members/Sign/Request/" + "TmpMeSign" + _MeId + "TmpMeSign" + Path.GetExtension(_MeImgSign);
            }
            ReqManager[0].EndEdit();
            ReqManager.Save();
            TempMemberManager[0].EndEdit();
            TempMemberManager.Save();
            #endregion
            #region Accounting Fish
            decimal TotalAmount = 0;
            decimal TotalCost = GetFirstMembershipCost(CostSettingsManager) + ((ChEnteghali.Checked) ? 0 : GetEnteranceCost(CostSettingsManager));
            for (int i = 0; i < AccountingManager.Count; i++)
            {
                TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
            }

            if (TotalCost > TotalAmount)
            {
                ShowMessage("مجموع مبالغ واریزی کمتر از حق عضویت است");
                trans.CancelSave();
                return;
            }

            for (int i = 0; i < AccountingManager.Count; i++)
            {
                AccountingManager[i].BeginEdit();
                AccountingManager[i]["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TempMember); //****(int)TSP.DataManager.TableCodes.Member;
                AccountingManager[i]["TableTypeId"] = _MeId;
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();
            #endregion
            #region attachment
            InsertMemberDocumentAttachments(AttachManager, _MeId, _MReId, true);
            #endregion
            #region StartWorkFlow
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LogManager);//FindNmcId();
            //int MReId = int.Parse(ReqManager[0]["MReId"].ToString());
            int WfCnt = WorkFlowStateManager.StartWorkFlow(_MReId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);

            if (WfCnt > 0)
            {
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                    {
                        lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                    }
                }

                trans.EndSave();
                ShowMessage("  ذخیره با کد عضویت " + "M" + _MeId.ToString() + "و رمز عبور  " + Password + " انجام شد");
            }
            else
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

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
            return;
        }
        SetEditMode();
        IsPageRefresh = true;
    }

    protected string InsertLogin(TSP.DataManager.LoginManager LogManager, int MeId, string IdNo, string Email)
    {
        String Password = Utility.GeneratePassword();
        DataRow logRow = LogManager.NewRow();
        logRow.BeginEdit();
        logRow["UserName"] = "M" + MeId;
        //***if (!string.IsNullOrEmpty(IdNo))
        logRow["Password"] = Utility.EncryptPassword(Password);
        logRow["UltId"] = (int)TSP.DataManager.UserType.TemporaryMembers;
        logRow["MeId"] = MeId;
        logRow["Email"] = Email;
        logRow["UserId2"] = Utility.GetCurrentUser_UserId();
        logRow["IsValid"] = 1;
        logRow["ModifiedDate"] = DateTime.Now;
        logRow.EndEdit();
        LogManager.AddRow(logRow);
        LogManager.Save();
        return Password;
    }

    protected void InsertRequest(TSP.DataManager.MemberRequestManager ReqManager, int MeId, int ComId, int WFTaskId)
    {
        DataRow drq = ReqManager.NewRow();
        drq["IsMeTemp"] = 1;
        drq["MeNo"] = "";
        if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
        {
            drq["NezamKardanConfirmURL"] = _MeImageKardan;
            HpKardani.NavigateUrl = _MeImageKardan;
        }
        else
        {
            drq["NezamKardanConfirmURL"] = DBNull.Value;
        }
        drq["MeId"] = MeId;
        drq["FirstName"] = txtFirstName.Text;
        drq["LastName"] = txtLastName.Text;
        drq["FirstNameEn"] = txtFirstNameEn.Text;
        drq["LastNameEn"] = txtLastNameEn.Text;

        drq["MobileNo"] = txtMobileNo.Text;
        drq["HomeAdr"] = txtHomeAdr.Text;
        if (txtArchitectorCode.Text != "")
            drq["ArchitectorCode"] = txtArchitectorCode.Text;
        if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
            drq["HomeTel"] = txtHometel_cityCode.Text + "-" + txtHometel.Text;
        else if (txtHometel.Text != "")
            drq["HomeTel"] = txtHometel.Text;
        if (!string.IsNullOrEmpty(txtHomePO.Text))
            drq["HomePO"] = txtHomePO.Text;
        else
            drq["HomePO"] = DBNull.Value;
        drq["WorkAdr"] = txtWorkAdr.Text;
        if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
            drq["WorkTel"] = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
        else if (txtWorkTel.Text != "")
            drq["WorkTel"] = txtWorkTel.Text;
        if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
            drq["FaxNo"] = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
        else if (txtFaxNo.Text != "")
            drq["FaxNo"] = txtFaxNo.Text;
        if (!string.IsNullOrEmpty(txtWorkPO.Text))
            drq["WorkPO"] = txtWorkPO.Text;
        else
            drq["WorkPO"] = DBNull.Value;
        if (txtBankAccNo.Text.Trim() != "")
            drq["BankAccNo"] = txtBankAccNo.Text;
        else
            drq["BankAccNo"] = DBNull.Value;

        if (drdMarId.Value != null)
            drq["MarId"] = drdMarId.Value.ToString();
        else
            drq["MarId"] = DBNull.Value;

        if (drdSoId.Value != null)
            drq["SoId"] = drdSoId.Value.ToString();
        else
            drq["SoId"] = DBNull.Value;

        if (drdCitId.Value != null)
            drq["CitId"] = drdCitId.Value.ToString();
        else
            drq["CitId"] = DBNull.Value;

        if (drdAgent.Value != null)
            drq["AgentId"] = drdAgent.Value.ToString();
        else
            drq["AgentId"] = DBNull.Value;

        if (drdSexId.Value != null)
            drq["SexId"] = drdSexId.Value.ToString();
        else
            drq["SexId"] = DBNull.Value;

        drq["Website"] = txtWebsite.Text;
        drq["Email"] = txtEmail.Text;

        drq["IsCreated"] = (int)TSP.DataManager.MembershipRegistrationStatus.Pending;//در جریان نیما
        // drq["IsCreated"] = 1;

        drq["CreateDate"] = Utility.GetDateOfToday();
        drq["UserId"] = Utility.GetCurrentUser_UserId();
        drq["ModifiedDate"] = DateTime.Now;

        drq["FatherName"] = txtFatherName.Text;
        drq["BirhtDate"] = txtBirthDate.Text;
        drq["BirthPlace"] = txtBirhtPlace.Text;
        drq["IdNo"] = txtIdNo.Text;
        drq["IssuePlace"] = txtIssuePlace.Text;
        drq["SSN"] = txtSSN.Text;
        drq["ComId"] = ComId;
        drq["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Pending;//در جریان
        drq["Requester"] = 1;//*****Requested by Employee
        drq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberRequest);
        drq["IsMeTemp"] = 1;
        drq["WFCurrentTaskId"] = WFTaskId;
        ReqManager.AddRow(drq);
        if (ReqManager.Save() > 0)
        {
            ReqManager.DataTable.AcceptChanges();
            _MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        }
    }

    #endregion

    /// <summary>
    /// Insert New Request for member
    /// </summary>
    protected void InsertNewRequestForchange(int ReqType)
    {
        if (IsPageRefresh)
            return;
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager MReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

        trans.Add(MReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowTaskManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(TransferManager);
        // trans.Add(DocMemberFileManager);
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming);
        if (WorkFlowTaskManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
            return;
        }
        int WFTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
        #endregion
        try
        {
            #region Check Conditions
            int MfId = -1;
            //************Check if Member has another request or not!!**********************
            MReqManager.FindByMemberId(_MeId, 0, 0, 0);
            if (MReqManager.Count > 0)
            {
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            MReqManager.FindByMemberId(_MeId, 0, 0, 1);
            if (MReqManager.Count > 0)
            {
                ShowMessage("به دلیل عدم پاسخ درخواست اولیه امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            if (ChbTCheckFileNo.Checked
                  && Convert.ToInt16(HiddenFieldInfo["HasFarsDon"]) == 1)
            {
                ShowMessage("امکان ثبت اطلاعات پروانه مربوط به سایر استان ها برای این عضو وجود ندارد.نامبرده دارای پروانه اشتغال به کار با کد استان فارس می باشد.");
                return;
            }
            if (
             ReqType == (int)TSP.DataManager.MemberRequestType.Dead
                || ReqType == (int)TSP.DataManager.MemberRequestType.Fired
                || ReqType == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince
                || ReqType == (int)TSP.DataManager.MemberRequestType.Cancel)
            {
                #region Check DocMemberFile
                DocMemberFileManager.SelectLastVersion(_MeId, 0, 0);
                if (DocMemberFileManager.Count > 0)
                {
                    ShowMessage("امکان ثبت این نوع درخواست وجود ندارد.عضو انتخاب شده دارای پرونده درجریان در واحد پروانه اشتغال به کار می باشد.");
                    return;
                }

                #endregion
            }
            //*********************************************************************************
            #endregion
            trans.BeginSave();
            DataRow drMeReq = MReqManager.NewRow();
            #region Insert Request From textboxes

            drMeReq["MeId"] = _MeId;
            if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
            {
                drMeReq["NezamKardanConfirmURL"] = _MeImageKardan;
                HpKardani.NavigateUrl = _MeImageKardan;
            }
            else
            {
                drMeReq["NezamKardanConfirmURL"] = DBNull.Value;
            }
            drMeReq["MeNo"] = txtMeNo.Text;
            drMeReq["BankAccNo"] = txtBankAccNo.Text;
            drMeReq["FirstName"] = txtFirstName.Text;
            drMeReq["LastName"] = txtLastName.Text;
            drMeReq["FirstNameEn"] = txtFirstNameEn.Text;
            drMeReq["LastNameEn"] = txtLastNameEn.Text;
            drMeReq["MobileNo"] = txtMobileNo.Text;
            drMeReq["HomeAdr"] = txtHomeAdr.Text;
            if (txtArchitectorCode.Text != "")
                drMeReq["ArchitectorCode"] = txtArchitectorCode.Text;
            if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
                drMeReq["HomeTel"] = txtHometel_cityCode.Text + "-" + txtHometel.Text;
            else if (txtHometel.Text != "")
                drMeReq["HomeTel"] = txtHometel.Text;
            if (!string.IsNullOrEmpty(txtHomePO.Text))
                drMeReq["HomePO"] = txtHomePO.Text;
            else
                drMeReq["HomePO"] = DBNull.Value;
            drMeReq["WorkAdr"] = txtWorkAdr.Text;
            if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
                drMeReq["WorkTel"] = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
            else if (txtWorkTel.Text != "")
                drMeReq["WorkTel"] = txtWorkTel.Text;
            if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
                drMeReq["FaxNo"] = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
            else if (txtFaxNo.Text != "")
                drMeReq["FaxNo"] = txtFaxNo.Text;
            if (!string.IsNullOrEmpty(txtWorkPO.Text))
                drMeReq["WorkPO"] = txtWorkPO.Text;
            else
                drMeReq["WorkPO"] = DBNull.Value;

            if (drdSexId.Value != null)
                drMeReq["SexId"] = drdSexId.Value;
            else
                drMeReq["SexId"] = DBNull.Value;

            if (drdMarId.Value != null)
                drMeReq["MarId"] = drdMarId.Value;
            else
                drMeReq["MarId"] = DBNull.Value;

            if (drdSoId.Value != null)
                drMeReq["SoId"] = drdSoId.Value;
            else
                drMeReq["SoId"] = DBNull.Value;

            drMeReq["MilitaryCommitment"] = chbSoLdire.Checked;

            if (drdCitId.Value != null)
                drMeReq["CitId"] = drdCitId.Value;
            else
                drMeReq["CitId"] = DBNull.Value;

            if (drdAgent.Value != null)
                drMeReq["AgentId"] = drdAgent.Value;
            else
                drMeReq["AgentId"] = DBNull.Value;

            drMeReq["Website"] = txtWebsite.Text;
            drMeReq["Email"] = txtEmail.Text;
            drMeReq["Description"] = txtStDesc.Text;
            drMeReq["RequestDesc"] = txtReqDesc.Text;
            drMeReq["IsConfirm"] = 0;//معلق

            drMeReq["FatherName"] = txtFatherName.Text;
            drMeReq["BirhtDate"] = txtBirthDate.Text;
            drMeReq["BirthPlace"] = txtBirhtPlace.Text;
            drMeReq["IdNo"] = txtIdNo.Text;
            drMeReq["IssuePlace"] = txtIssuePlace.Text;
            drMeReq["SSN"] = txtSSN.Text;

            int comId = 0;
            for (int i = 0; i < chbComId.Items.Count; i++)
            {
                if (chbComId.Items[i].Selected)
                    comId = comId + int.Parse(chbComId.Items[i].Value.ToString());
            }
            if (comId > 0)
                drMeReq["ComId"] = comId;
            else
                drMeReq["ComId"] = 0;

            drMeReq["UserId"] = Utility.GetCurrentUser_UserId();
            drMeReq["ModifiedDate"] = DateTime.Now;
            drMeReq["LetterNo"] = "";
            drMeReq["LetterDate"] = "";
            drMeReq["CreateDate"] = Utility.GetDateOfToday();
            drMeReq["Requester"] = (int)TSP.DataManager.MembershipRequest.Employee;

            if (Utility.IsDBNullOrNullValue(_MeImgUpload))
                drMeReq["ImageUrl"] = imgMember.ImageUrl;
            else
            {
                drMeReq["ImageUrl"] = _MeImgUpload;
            }
            if (Utility.IsDBNullOrNullValue(_MeImgSign))
                drMeReq["SignUrl"] = ImgSign.ImageUrl;
            else
            {
                drMeReq["SignUrl"] = _MeImgSign;
            }

            drMeReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberRequest);
            #endregion
            // }

            drMeReq["Iscreated"] = ReqType;
            if (ReqType == (int)TSP.DataManager.MemberRequestType.Dead)
            {
                drMeReq["InActive"] = 1;
                drMeReq["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Dead;
            }
            else if (ReqType == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince)
            {
                drMeReq["InActive"] = 1;
                drMeReq["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince;
            }
            else
            {
                drMeReq["InActive"] = 0;
                drMeReq["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;
            }
            drMeReq["WFCurrentTaskId"] = WFTaskId;
            MReqManager.AddRow(drMeReq);

            if (MReqManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            MReqManager.DataTable.AcceptChanges();
            int MReId = int.Parse(MReqManager[0]["MReId"].ToString());

            TSP.DataManager.MemberRequestManager.UpdateMeNo(MReId, trans);
            #region Attachment
            #region  File Peyvast
            if (Session["TblOfImg10"] != null)
            {
                dtOfImg = (DataTable)Session["TblOfImg10"];

                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
                        drImg["RefTable"] = MReId;
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
            InsertMemberDocumentAttachments(attachManager, _MeId, MReId, false);

            #endregion
            #region Enteghali
            if (ChEnteghali.Checked)
            {
                if (CmbTransferStatus.SelectedIndex == -1)
                {
                    trans.CancelSave();
                    ShowMessage("نوع انتقالی را انتخاب نمایید");
                    return;
                }
                InsertTransferMember_Request(TransferManager, MReId, false, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));

            }
            #endregion
            #region WorkFlow
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            if (ReqType == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince)
            {
                SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
            }

            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                trans.CancelSave();
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            int NmcId = FindNmcId(TaskId);
            if (NmcId == -1)
            {
                trans.CancelSave();
                return;
            }
            DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
            WorkflowStateRow["TaskId"] = TaskId;
            WorkflowStateRow["TableId"] = MReId;
            WorkflowStateRow["NmcId"] = NmcId;
            WorkflowStateRow["SubOrder"] = 1;
            WorkflowStateRow["NmcIdType"] = 0;
            WorkflowStateRow["UserId"] = Utility.GetCurrentUser_UserId();
            WorkflowStateRow["ModifiedDate"] = DateTime.Now;

            WorkFlowStateManager.AddRow(WorkflowStateRow);
            int cnt = WorkFlowStateManager.Save();
            if (cnt <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #endregion

            trans.EndSave();
            _MReId = MReId;
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
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
            return;
        }
        SetEditMode();
        IsPageRefresh = true;
    }

    /// <summary>
    /// جهت افرادی که از استان دیگری انتقالی گرفته اند
    /// </summary>
    /// <param name="transferManager"></param>
    /// <param name="MReId"></param>
    private void InsertTransferMember_Request(TSP.DataManager.TransferMemberManager transferManager, int MReId, Boolean IsTempMember, TSP.DataManager.TransferMemberType TransferMemberType)
    {
        //PanelEnteghali.ClientVisible = true;
        DataRow drTransfer = transferManager.NewRow();
        if (ComboOtherProvince.Value != null)
            drTransfer["PrId"] = ComboOtherProvince.Value;
        drTransfer["TransferDate"] = txtTransferDate.Text;
        drTransfer["TransferType"] = (int)TransferMemberType;
        drTransfer["TableId"] = MReId;
        drTransfer["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
        drTransfer["Body"] = txtTransferBodyResone.Text;
        drTransfer["MeNo"] = txtTransferMeNo.Text;
        if (ChbTCheckFileNo.Checked)
        {
            drTransfer["FileNo"] = txtPreProvinceFileNo.Text;
            drTransfer["FirstDocRegDate"] = txtFirstDocRegDate.Text;
            drTransfer["CurrentDocRegDate"] = txtCurrentDocExpDate.Text;
            drTransfer["CurrentDocExpDate"] = txtCurrentDocRegDate.Text;
            if (ComboDocPreProvince.SelectedItem != null && ComboDocPreProvince.SelectedItem.Value != null)
                drTransfer["DocPrId"] = ComboDocPreProvince.SelectedItem.Value;
        }
        drTransfer["IsConfirmed"] = 0;
        drTransfer["UserId"] = Utility.GetCurrentUser_UserId();
        drTransfer["ModifiedDate"] = DateTime.Now;

        if (!Utility.IsDBNullOrNullValue(_MeImageEnteghali))
        {
            if (IsTempMember)
                drTransfer["ImageUrl"] = "~/Image/Members/Transport/" + "TmpMeTrans" + MReId + "TmpMeTrans" + Path.GetExtension(_MeImageEnteghali);
            else
                drTransfer["ImageUrl"] = _MeImageEnteghali;
        }
        else if (!string.IsNullOrEmpty(ImgTransferToFars.ImageUrl))
        {
            drTransfer["ImageUrl"] = ImgTransferToFars.ImageUrl;
        }

        transferManager.AddRow(drTransfer);
        if (transferManager.Save() == 1)
        {
            // ImgTransferToFars.ClientVisible = true;
            ImgTransferToFars.ImageUrl = transferManager[0]["ImageUrl"].ToString();
        }
        transferManager.DataTable.AcceptChanges();
    }

    private Boolean CopyMeRequestInTempMember(int MeId, TSP.DataManager.MemberRequestManager MemberRequestManager, TSP.DataManager.TempMemberManager MemberManager)
    {
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            return false;
        }
        int reqRowNo = MemberRequestManager.Count - 1;
        MemberManager[0].BeginEdit();
        MemberManager[0]["FirstName"] = MemberRequestManager[reqRowNo]["FirstName"];
        MemberManager[0]["LastName"] = MemberRequestManager[reqRowNo]["LastName"];
        MemberManager[0]["FirstNameEn"] = MemberRequestManager[reqRowNo]["FirstNameEn"];
        MemberManager[0]["LastNameEn"] = MemberRequestManager[reqRowNo]["LastNameEn"];
        MemberManager[0]["MobileNo"] = MemberRequestManager[reqRowNo]["MobileNo"];
        MemberManager[0]["HomeAdr"] = MemberRequestManager[reqRowNo]["HomeAdr"];
        MemberManager[0]["HomeTel"] = MemberRequestManager[reqRowNo]["HomeTel"];
        MemberManager[0]["HomePO"] = MemberRequestManager[reqRowNo]["HomePO"];
        MemberManager[0]["WorkAdr"] = MemberRequestManager[reqRowNo]["WorkAdr"];
        MemberManager[0]["WorkTel"] = MemberRequestManager[reqRowNo]["WorkTel"];
        MemberManager[0]["FaxNo"] = MemberRequestManager[reqRowNo]["FaxNo"];
        MemberManager[0]["WorkPO"] = MemberRequestManager[reqRowNo]["WorkPO"];
        MemberManager[0]["BankAccNo"] = MemberRequestManager[reqRowNo]["BankAccNo"];
        MemberManager[0]["MarId"] = MemberRequestManager[reqRowNo]["MarId"];
        MemberManager[0]["SoId"] = MemberRequestManager[reqRowNo]["SoId"];
        MemberManager[0]["MilitaryCommitment"] = MemberRequestManager[reqRowNo]["MilitaryCommitment"];
        MemberManager[0]["Website"] = MemberRequestManager[reqRowNo]["Website"];
        MemberManager[0]["Email"] = MemberRequestManager[reqRowNo]["Email"];
        MemberManager[0]["CitId"] = MemberRequestManager[reqRowNo]["CitId"];
        MemberManager[0]["AgentId"] = MemberRequestManager[reqRowNo]["AgentId"];
        MemberManager[0]["FatherName"] = MemberRequestManager[reqRowNo]["FatherName"];
        MemberManager[0]["BirhtDate"] = MemberRequestManager[reqRowNo]["BirhtDate"];
        MemberManager[0]["BirthPlace"] = MemberRequestManager[reqRowNo]["BirthPlace"];
        MemberManager[0]["IdNo"] = MemberRequestManager[reqRowNo]["IdNo"];
        MemberManager[0]["IssuePlace"] = MemberRequestManager[reqRowNo]["IssuePlace"];
        MemberManager[0]["SSN"] = MemberRequestManager[reqRowNo]["SSN"];
        MemberManager[0]["SexId"] = MemberRequestManager[reqRowNo]["SexId"];
        MemberManager[0]["SignUrl"] = MemberRequestManager[reqRowNo]["SignUrl"];
        MemberManager[0]["ImageUrl"] = MemberRequestManager[reqRowNo]["ImageUrl"];


        MemberManager[0].EndEdit();
        if (MemberManager.Save() > 0)
            return true;
        else
            return false;
    }

    private Boolean CopyMeRequestInMember(int MeId, TSP.DataManager.MemberRequestManager MemberRequestManager, TSP.DataManager.MemberManager MemberManager)
    {
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            return false;
        }
        int reqRowNo = MemberRequestManager.Count - 1;
        MemberManager[0].BeginEdit();
        MemberManager[0]["FirstName"] = MemberRequestManager[reqRowNo]["FirstName"];
        MemberManager[0]["LastName"] = MemberRequestManager[reqRowNo]["LastName"];
        MemberManager[0]["FirstNameEn"] = MemberRequestManager[reqRowNo]["FirstNameEn"];
        MemberManager[0]["LastNameEn"] = MemberRequestManager[reqRowNo]["LastNameEn"];
        MemberManager[0]["MobileNo"] = MemberRequestManager[reqRowNo]["MobileNo"];
        MemberManager[0]["HomeAdr"] = MemberRequestManager[reqRowNo]["HomeAdr"];
        MemberManager[0]["HomeTel"] = MemberRequestManager[reqRowNo]["HomeTel"];
        MemberManager[0]["HomePO"] = MemberRequestManager[reqRowNo]["HomePO"];
        MemberManager[0]["WorkAdr"] = MemberRequestManager[reqRowNo]["WorkAdr"];
        MemberManager[0]["WorkTel"] = MemberRequestManager[reqRowNo]["WorkTel"];
        MemberManager[0]["FaxNo"] = MemberRequestManager[reqRowNo]["FaxNo"];
        MemberManager[0]["WorkPO"] = MemberRequestManager[reqRowNo]["WorkPO"];
        MemberManager[0]["BankAccNo"] = MemberRequestManager[reqRowNo]["BankAccNo"];
        MemberManager[0]["MarId"] = MemberRequestManager[reqRowNo]["MarId"];
        MemberManager[0]["SoId"] = MemberRequestManager[reqRowNo]["SoId"];
        MemberManager[0]["MilitaryCommitment"] = MemberRequestManager[reqRowNo]["MilitaryCommitment"];
        MemberManager[0]["Website"] = MemberRequestManager[reqRowNo]["Website"];
        MemberManager[0]["Email"] = MemberRequestManager[reqRowNo]["Email"];
        MemberManager[0]["CitId"] = MemberRequestManager[reqRowNo]["CitId"];
        MemberManager[0]["AgentId"] = MemberRequestManager[reqRowNo]["AgentId"];
        MemberManager[0]["FatherName"] = MemberRequestManager[reqRowNo]["FatherName"];
        MemberManager[0]["BirhtDate"] = MemberRequestManager[reqRowNo]["BirhtDate"];
        MemberManager[0]["BirthPlace"] = MemberRequestManager[reqRowNo]["BirthPlace"];
        MemberManager[0]["IdNo"] = MemberRequestManager[reqRowNo]["IdNo"];
        MemberManager[0]["IssuePlace"] = MemberRequestManager[reqRowNo]["IssuePlace"];
        MemberManager[0]["SSN"] = MemberRequestManager[reqRowNo]["SSN"];
        MemberManager[0]["SexId"] = MemberRequestManager[reqRowNo]["SexId"];
        MemberManager[0]["SignUrl"] = MemberRequestManager[reqRowNo]["SignUrl"];
        MemberManager[0]["ImageUrl"] = MemberRequestManager[reqRowNo]["ImageUrl"];
        MemberManager[0]["ArchitectorCode"] = MemberRequestManager[reqRowNo]["ArchitectorCode"];

        MemberManager[0].EndEdit();
        if (MemberManager.Save() > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// save IdNo,IdNoP2,IdNoPDes,SSN,SSNBack,SSNBack,SoldierCard,SoldierCardBack,ResidentDoc
    /// </summary>
    /// <param name="AttachManager"></param>
    /// <param name="MeId"></param>
    /// <param name="MReId"></param>
    /// <param name="IsInsertTempMe"></param>
    protected void InsertMemberDocumentAttachments(TSP.DataManager.AttachmentsManager AttachManager, int MeId, int MReId, Boolean IsInsertTempMe)
    {
        if (!Utility.IsDBNullOrNullValue(_MeImageIdNo))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNo, _MeImageIdNo, MeId.ToString(), false);
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageIdNoP2))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoP2, _MeImageIdNoP2, MeId.ToString(), false);

        }
        if (!Utility.IsDBNullOrNullValue(_MeImageIdNoPDes))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoPDes, _MeImageIdNoPDes, MeId.ToString(), false);

        }
        if (!Utility.IsDBNullOrNullValue(_MeImageSSN))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSN, _MeImageSSN, MeId.ToString(), false);
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageSSNBack))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSNBack, _MeImageSSNBack, MeId.ToString(), false);
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageSol))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCard, _MeImageSol, MeId.ToString(), false);
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageSolBack))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCardBack, _MeImageSolBack, MeId.ToString(), false);
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageResident))
        {
            InsertAttchment(AttachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.ResidentDoc, _MeImageResident, MeId.ToString(), false);
        }
        AttachManager.Save();
    }

    private void InsertAttchment(TSP.DataManager.AttachmentsManager attachManager, int TtId, int RefTable, TSP.DataManager.AttachType AttachType, string FilePath, string FileName, Boolean NeedSave = true)
    {
        DataRow drAtt = attachManager.NewRow();
        drAtt["TtId"] = TtId;
        drAtt["RefTable"] = RefTable;
        drAtt["AttId"] = AttachType;
        drAtt["IsValid"] = 1;
        drAtt["FilePath"] = FilePath;
        drAtt["FileName"] = FileName;
        drAtt["Description"] = DBNull.Value;
        drAtt["UserId"] = Utility.GetCurrentUser_UserId();
        drAtt["ModfiedDate"] = DateTime.Now;
        attachManager.AddRow(drAtt);
        if (NeedSave)
        {
            attachManager.Save();
            attachManager.DataTable.AcceptChanges();
        }
    }
    #endregion

    #region Edits
    protected void EditRequest(int MReId)
    {
        if (IsPageRefresh)
            return;
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager MReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MReqManager);
        trans.Add(MeManager);
        trans.Add(attachManager);
        trans.Add(TransferManager);
        trans.Add(TempMemberManager);
        #endregion
        try
        {
            if (ChbTCheckFileNo.Checked
                 && Convert.ToInt16(HiddenFieldInfo["HasFarsDon"]) == 1)
            {
                ShowMessage("امکان ثبت اطلاعات پروانه مربوط به سایر استان ها برای این عضو وجود ندارد.نامبرده دارای پروانه اشتغال به کار با کد استان فارس می باشد.");
                return;
            }
            trans.BeginSave();
            MReqManager.FindByCode(MReId);
            Boolean IsTemp = false;

            if (MReqManager.Count != 1)
            {
                trans.EndSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (MReqManager[0]["IsMeTemp"].ToString() == "True")
                IsTemp = true;
            MReqManager[0].BeginEdit();
            #region Edit MeRequest

            if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
            {
                MReqManager[0]["NezamKardanConfirmURL"] = _MeImageKardan;
                HpKardani.NavigateUrl = _MeImageKardan;
            }
            else
            {
                MReqManager[0]["NezamKardanConfirmURL"] = DBNull.Value;
            }

            MReqManager[0]["BankAccNo"] = txtBankAccNo.Text;
            MReqManager[0]["FirstName"] = txtFirstName.Text;
            MReqManager[0]["LastName"] = txtLastName.Text;
            MReqManager[0]["FirstNameEn"] = txtFirstNameEn.Text;
            MReqManager[0]["LastNameEn"] = txtLastNameEn.Text;
            MReqManager[0]["MobileNo"] = txtMobileNo.Text;
            MReqManager[0]["HomeAdr"] = txtHomeAdr.Text;
            MReqManager[0]["ArchitectorCode"] = txtArchitectorCode.Text;
            if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
                MReqManager[0]["HomeTel"] = txtHometel_cityCode.Text + "-" + txtHometel.Text;
            else if (txtHometel.Text != "")
                MReqManager[0]["HomeTel"] = txtHometel.Text;
            if (!string.IsNullOrEmpty(txtHomePO.Text))
                MReqManager[0]["HomePO"] = txtHomePO.Text;
            MReqManager[0]["WorkAdr"] = txtWorkAdr.Text;
            if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
                MReqManager[0]["WorkTel"] = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
            else if (txtWorkTel.Text != "")
                MReqManager[0]["WorkTel"] = txtWorkTel.Text;
            if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
                MReqManager[0]["FaxNo"] = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
            else if (txtFaxNo.Text != "")
                MReqManager[0]["FaxNo"] = txtFaxNo.Text;
            if (!string.IsNullOrEmpty(txtWorkPO.Text))
                MReqManager[0]["WorkPO"] = txtWorkPO.Text;

            MReqManager[0]["FatherName"] = txtFatherName.Text;
            MReqManager[0]["BirhtDate"] = txtBirthDate.Text;
            MReqManager[0]["BirthPlace"] = txtBirhtPlace.Text;
            MReqManager[0]["IdNo"] = txtIdNo.Text;
            MReqManager[0]["IssuePlace"] = txtIssuePlace.Text;
            MReqManager[0]["SSN"] = txtSSN.Text;
            int comId = 0;
            for (int i = 0; i < chbComId.Items.Count; i++)
            {
                if (chbComId.Items[i].Selected)
                    comId = comId + int.Parse(chbComId.Items[i].Value.ToString());

            }
            if (comId > 0)
                MReqManager[0]["ComId"] = comId;

            if (drdSexId.Value != null)
                MReqManager[0]["SexId"] = drdSexId.Value;

            if (drdMarId.Value != null)
                MReqManager[0]["MarId"] = drdMarId.Value;
            else
                MReqManager[0]["MarId"] = DBNull.Value;

            if (drdSoId.Value != null)
                MReqManager[0]["SoId"] = drdSoId.Value;
            else
                MReqManager[0]["SoId"] = DBNull.Value;

            MReqManager[0]["MilitaryCommitment"] = chbSoLdire.Checked;

            if (drdCitId.Value != null)
                MReqManager[0]["CitId"] = drdCitId.Value;
            else
                MReqManager[0]["CitId"] = DBNull.Value;

            if (drdAgent.Value != null)
                MReqManager[0]["AgentId"] = drdAgent.Value;
            else
                MReqManager[0]["AgentId"] = DBNull.Value;
            MReqManager[0]["Website"] = txtWebsite.Text;
            MReqManager[0]["Email"] = txtEmail.Text;
            #endregion         

            if (Convert.ToInt32(MReqManager[0]["IsCreated"]) == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince)
                MReqManager[0]["InActive"] = 1;//غیرفعال

            //********************************************

            MReqManager[0]["RequestDesc"] = txtReqDesc.Text;
            MReqManager[0]["Description"] = txtStDesc.Text;

            if (!string.IsNullOrEmpty(txtArchitectorCode.Text))
                MReqManager[0]["ArchitectorCode"] = txtArchitectorCode.Text;

            MReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            MReqManager[0]["ModifiedDate"] = DateTime.Now;
            MReqManager[0]["LetterNo"] = "";
            MReqManager[0]["LetterDate"] = "";

            #region Save Images
            if (!Utility.IsDBNullOrNullValue(_MeImgSign))
            {
                MReqManager[0]["SignUrl"] = ImgSign.ImageUrl = _MeImgSign;
            }
            if (!Utility.IsDBNullOrNullValue(_MeImgUpload))
            {
                MReqManager[0]["ImageUrl"] = imgMember.ImageUrl = _MeImgUpload;
            }

            #endregion

            MReqManager[0].EndEdit();

            if (MReqManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            #region       //****************انتقال از سایر استان ها به فارس**********انتقال به سایر استان ها******************
            if (ChEnteghali.Checked == true)
            {
                if (ComboOtherProvince.Value == null)
                {
                    trans.CancelSave();
                    ShowMessage("استان انتقالی مشخص نشده است");
                    return;
                }

                if (!EditTransfer(TransferManager, MReId, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString())))//, false, TSP.DataManager.TransferMemberType.GoToOtherProvince))
                {
                    InsertTransferMember_Request(TransferManager, MReId, false, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));
                }
            }
            else
            {
                #region Delete Transfer
                TransferManager.FindByMemberId(MReId, -1);
                if (TransferManager.Count == 1)
                {
                    TransferManager[0].Delete();
                    TransferManager.Save();
                }
                #endregion
            }
            #endregion
            #region tblAttachment
            if (Session["TblOfImg10"] != null)
            {
                dtOfImg = (DataTable)Session["TblOfImg10"];


                if (dtOfImg.GetChanges() != null)
                {
                    DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                    if (insRows.Length > 0)
                    {
                        for (int i = 0; i < insRows.Length; i++)
                        {
                            DataRow drImg = attachManager.NewRow();
                            drImg["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
                            drImg["RefTable"] = MReqManager[0]["MReId"];
                            drImg["AttId"] = 1;
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
            }
            #endregion
            #region MeDocumetnAttachments
            if (!Utility.IsDBNullOrNullValue(_MeImageIdNo))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageIdNo;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNo, _MeImageIdNo, _MeId.ToString());
                HpIdNo.NavigateUrl = _MeImageIdNo;
            }

            if (!Utility.IsDBNullOrNullValue(_MeImageIdNoP2))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageIdNoP2;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoP2, _MeImageIdNoP2, _MeId.ToString());
                HIdNoP2.NavigateUrl = _MeImageIdNoP2;
            }

            if (!Utility.IsDBNullOrNullValue(_MeImageIdNoPDes))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageIdNoPDes;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoPDes, _MeImageIdNoPDes, _MeId.ToString());
                HIdNoPDes.NavigateUrl = _MeImageIdNoPDes;
            }

            if (!Utility.IsDBNullOrNullValue(_MeImageSSN))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageSSN;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSN, _MeImageSSN, _MeId.ToString());
                HpSSN.NavigateUrl = _MeImageSSN;
            }


            if (!Utility.IsDBNullOrNullValue(_MeImageSSNBack))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageSSNBack;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSNBack, _MeImageSSNBack, _MeId.ToString());
                HpSSNBack.NavigateUrl = _MeImageSSNBack;
            }


            if (!Utility.IsDBNullOrNullValue(_MeImageResident))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageResident;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.ResidentDoc, _MeImageResident, _MeId.ToString());
                HpResident.NavigateUrl = _MeImageResident;
            }

            if (!Utility.IsDBNullOrNullValue(_MeImageSol))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageSol;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCard, _MeImageSol, _MeId.ToString());
                HpSoldier.NavigateUrl = _MeImageSol;
            }

            if (!Utility.IsDBNullOrNullValue(_MeImageSolBack))
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = _MeImageSolBack;
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCardBack, _MeImageSolBack, _MeId.ToString());
                HpSoldierBack.NavigateUrl = _MeImageSolBack;
            }
            #endregion

            if (Convert.ToInt32(MReqManager[0]["IsCreated"]) == (int)TSP.DataManager.MemberRequestType.Create)
            {
                if (Convert.ToBoolean(MReqManager[0]["IsMeTemp"]))
                {
                    if (!CopyMeRequestInTempMember(_MeId, MReqManager, TempMemberManager))
                    {
                        trans.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                        return;
                    }
                }
                else
                {
                    if (!CopyMeRequestInMember(_MeId, MReqManager, MeManager))
                    {
                        trans.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                        return;
                    }
                }
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(ViewState["IsEdited_MeInfo"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.Member;
                int Tableid = MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, Tableid, UpdateTableType, "ویرایش اطلاعات عضو", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                ViewState["IsEdited_MeInfo"] = true;
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
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
        IsPageRefresh = true;
    }

    private bool EditTransfer(TSP.DataManager.TransferMemberManager TransferManager, int MReId, TSP.DataManager.TransferMemberType TransferMemberType)//, Boolean IsTempMember)
    {
        TransferManager.FindByMemberId(MReId, -1);// Convert.ToInt16(TransferMemberType));
        if (TransferManager.Count > 0)
        {
            TransferManager[0].BeginEdit();
            if (ComboOtherProvince.Value != null)
                TransferManager[0]["PrId"] = ComboOtherProvince.Value;
            TransferManager[0]["TransferType"] = (int)TransferMemberType;
            TransferManager[0]["TransferDate"] = txtTransferDate.Text;
            TransferManager[0]["MeNo"] = txtTransferMeNo.Text;
            TransferManager[0]["Body"] = txtTransferBodyResone.Text;
            if (ChbTCheckFileNo.Checked)
            {
                TransferManager[0]["FileNo"] = txtPreProvinceFileNo.Text;
                TransferManager[0]["FirstDocRegDate"] = txtFirstDocRegDate.Text;
                TransferManager[0]["CurrentDocRegDate"] = txtCurrentDocExpDate.Text;
                TransferManager[0]["CurrentDocExpDate"] = txtCurrentDocRegDate.Text;
                if (ComboDocPreProvince.SelectedItem != null && ComboDocPreProvince.SelectedItem.Value != null)
                    TransferManager[0]["DocPrId"] = ComboDocPreProvince.SelectedItem.Value;
            }
            else
            {
                TransferManager[0]["FileNo"] = "";
                TransferManager[0]["FirstDocRegDate"] = "";
                TransferManager[0]["CurrentDocRegDate"] = "";
                TransferManager[0]["CurrentDocExpDate"] = "";
                TransferManager[0]["DocPrId"] = DBNull.Value;
            }
            TransferManager[0]["IsConfirmed"] = 0;
            TransferManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TransferManager[0]["ModifiedDate"] = DateTime.Now;

            #region editImage
            if (!Utility.IsDBNullOrNullValue(_MeImageEnteghali))
            {
                TransferManager[0]["ImageUrl"] = _MeImageEnteghali;
                //chTImgEdit = true;
            }
            #endregion

            TransferManager[0].EndEdit();
            TransferManager.Save();
            return true;
        }
        return false;
    }

    #endregion

    #region Accounting
    /*********************************************************************************************************************************************************************/
    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetEnteranceCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }
    /*********************************************************************************************************************************************************************/
    #endregion

    #region WF Methods
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
            ShowMessage("اطلاعات شما به عنوان انجام دهنده عملیات انتخاب شده در جریان کار ثبت نشده است.");
            return (-1);
        }
    }  

    private Boolean CheckPermitionForEdit(int TableId)
    {
        int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindByCode(TableId);
        if (MemberRequestManager.Count != 1)
        {
            return false;
        }
        if (Convert.ToInt32(MemberRequestManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
            WfCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
        }

        TSP.DataManager.WFPermission WFPermission = new TSP.DataManager.WFPermission();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        Boolean CanEdit1 = WorkFlowPermission.CheckPermissionForEditByUser(TableId, WfCode, TaskCode, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_NmcIdType());
        Boolean CanEdit2 = WorkFlowPermission.CheckPermissionForEditByUser(TableId, WfCode, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_NmcIdType());
        if (CanEdit1 || CanEdit2)
            return true;
        else
            return false;
    }

    private void CheckWorkFlowPermission()
    {
        string PgMode = _PageMode;
        if (PgMode == "NewMe"
            || PgMode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.Cancel.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.Create.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.Dead.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.FakeLicense.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.Fired.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.Request.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.ReturnToCurrentProvince.ToString()
            || PgMode == TSP.DataManager.MemberRequestType.TransferToOtherProvince.ToString())
        {
            PgMode = "New";
        }
        CheckWorkFlowPermissionForSave(PgMode);
        if (PgMode != "New")
            CheckWorkFlowPermissionForEdit(PgMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermission2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, TableType, Utility.GetCurrentUser_UserId(), PageMode);

        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermission2.BtnSave;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {

        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;

        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindByCode(_MReId);
        if (MemberRequestManager.Count == 1)
        {
            if (Convert.ToInt32(MemberRequestManager[0]["MsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
            {
                TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
                TaskCode2 = (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMemberTransfer;
                WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
            }
        }
        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, _MReId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermission2 = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode2, WFCode, _MReId, Utility.GetCurrentUser_UserId(), PageMode);

        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermission2.BtnSave;
        btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit || WFPermission2.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    #endregion

    #region Save Images and Attachments

    protected string SaveFileImage(UploadedFile uploadedFile)
    {

        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/MeRequest/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["MeReqUpload"] = tempFileName;

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
                if (_MReId == -1)
                    ret = "TempMeImg" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeImg" + _MReId.ToString() + "TempMeImg" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeImg" + _MeId.ToString() + "MeImg" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Members/Person/Request/") + ret) == true);
            string tempFileName = "~/Image/Members/Person/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImgUpload = tempFileName;
        }
        return ret;
    }

    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TepMeSign" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TepMeSign" + _MReId.ToString() + "TepMeSign" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeSign" + _MeId.ToString() + "MeSign" + Path.GetRandomFileName() + ImageType.Extension;


            } while (File.Exists(MapPath("~/Image/Members/Sign/Request/") + ret) == true);
            string tempFileName = "~/Image/Members/Sign/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImgSign = tempFileName;
        }
        return ret;
    }

    protected string SaveImageIdNo(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeIdNo" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeIdNo" + _MReId.ToString() + "MeIdNo" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeIdNo" + _MeId.ToString() + "MeIdNo" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/IdNo/Request/") + ret) == true);
            string tempFileName = "~/image/Members/IdNo/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);

            if (id == "flpIdNo")
                _MeImageIdNo = tempFileName;
            if (id == "flpIdNoP2")
                _MeImageIdNoP2 = tempFileName;
            if (id == "flpIdNoPDes")
                _MeImageIdNoPDes = tempFileName;
        }
        return ret;
    }

    protected string SaveImageSSN(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeSSN" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeSSN" + _MReId.ToString() + "TempMeSSN" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeSSN" + _MeId.ToString() + "MeSSN" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/SSN/Request/") + ret) == true);
            string tempFileName = "~/image/Members/SSN/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);

            if (id == "flpSSN")
                _MeImageSSN = tempFileName;
            if (id == "flpSSNBack")
                _MeImageSSNBack = tempFileName;
        }
        return ret;
    }

    protected string SaveImageSoldier(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeSol" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeSol" + _MReId.ToString() + "TempMeSol" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeSol" + _MeId.ToString() + "MeSol" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Soldier/Request/") + ret) == true);
            string tempFileName = "~/image/Members/Soldier/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);

            if (id == "flpSoldier")
                _MeImageSol = tempFileName;
            if (id == "flpSoldierBack")
                _MeImageSolBack = tempFileName;
        }
        return ret;
    }

    protected string SaveImageEnteghali(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeTrans" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeTrans" + _MReId.ToString() + "TempMeTrans" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeTrans" + _MeId.ToString() + "MeTrans" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/Transport/") + ret) == true);
            string tempFileName = "~/image/Members/Transport/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImageEnteghali = tempFileName;

        }
        return ret;
    }

    protected string SaveImageResident(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeResid" + Path.GetRandomFileName() + ImageType.Extension;
                else if (_IsMeTemp == 1)
                    ret = "TempMeResid" + _MReId.ToString() + "TempMeResid" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "MeResid" + _MeId.ToString() + "MeMeResid" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Resident/Request/") + ret) == true);
            string tempFileName = "~/image/Members/Resident/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImageResident = tempFileName;
        }
        return ret;
    }
    protected string SaveImageKardani(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (_MReId == -1)
                    ret = "TempMeKardid" + Path.GetRandomFileName() + ImageType.Extension;
                else
                    ret = "KardMReId" + _MReId.ToString() + "" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/NezamKardani/Request/") + ret) == true);
            string tempFileName = "~/image/Members/NezamKardani/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImageKardan = tempFileName;
        }
        return ret;
    }
    #endregion

    #region Set Controls
    private void SetSoldireControlsVisible()
    {
        if (drdSexId.Value != null)
        {
            if (Convert.ToInt32(drdSexId.Value) == (int)TSP.DataManager.SexManager.Sex.Male)
            {
                lblSolFile.ClientVisible = flpSoldier.ClientVisible = flpSoldierBack.ClientVisible =
                drdSoId.ClientVisible = lblSol.ClientVisible = true;
                if (_PageMode == "View")
                {
                    flpSoldier.ClientVisible = flpSoldierBack.ClientVisible = false;
                }
                if (drdSoId.SelectedIndex == 3 || drdSoId.SelectedIndex == 4 || drdSoId.SelectedIndex == 5)
                {
                    chbSoLdire.ClientVisible = true;
                }
                else
                {
                    chbSoLdire.ClientVisible = false;
                    chbSoLdire.Checked = false;
                }
            }
            else
            {
                lblSolFile.ClientVisible = flpSoldier.ClientVisible =

                lblSolFileBack.ClientVisible = flpSoldierBack.ClientVisible = chbSoLdire.ClientVisible =

                drdSoId.ClientVisible = lblSol.ClientVisible = false;
            }
        }
    }

    private void SetEnteghaliControlsVisible()
    {
        if (!ChEnteghali.Checked)
            PanelEnteghali.ClientVisible = false;
        else
            PanelEnteghali.ClientVisible = true;
        if (!ChbTCheckFileNo.Checked)
            PanelEntegaliDoc.ClientVisible = false;
        else
            PanelEntegaliDoc.ClientVisible = true;
    }

    private void ClearForm()
    {
        txtBankAccNo.Text =
        txtBirhtPlace.Text =
        txtBirthDate.Text =
        txtEmail.Text =
        txtFatherName.Text =
        txtFaxNo.Text =
        txtFaxNo_cityCode.Text =
        txtFirstName.Text =
        txtFirstNameEn.Text =
        txtHomeAdr.Text =
        txtHomePO.Text =
        txtHometel.Text =
        txtHometel_cityCode.Text =
        txtIdNo.Text =
        txtIssuePlace.Text =
        txtLastName.Text =
        txtLastNameEn.Text =
        txtMeNo.Text =
        txtMeStatus.Text =
        txtMobileNo.Text =
        txtTransferBodyResone.Text =
        txtTransferDate.Text =
        txtPreProvinceFileNo.Text =
        txtTransferMeNo.Text =
        lblReasoneToTranseferOtherProv.Text =
        txtFirstDocRegDate.Text =
        txtCurrentDocRegDate.Text =
        txtCurrentDocExpDate.Text =
        txtReqDesc.Text =
        txtSSN.Text =
        txtStDesc.Text =
        txtWebsite.Text =
        txtWorkAdr.Text =
        txtWorkPO.Text =
        txtWorkTel.Text =
        txtArchitectorCode.Text =
        txtWorkTel_cityCode.Text = "";

        ChEnteghali.Checked = ChbTCheckFileNo.Checked = lblAlarmHasMeDoc.Visible = false;

        ComboOtherProvince.SelectedIndex =
        CmbTransferStatus.SelectedIndex =
        ComboDocPreProvince.SelectedIndex =
        drdAgent.SelectedIndex =
        drdCitId.SelectedIndex =
        drdMarId.SelectedIndex =
        drdSoId.SelectedIndex = -1;
        drdSexId.DataBind();
        drdSexId.SelectedIndex = 0;

        HpIdNo.NavigateUrl =
        HIdNoP2.NavigateUrl =
        HIdNoPDes.NavigateUrl =
        HpSoldier.NavigateUrl =
        HpSoldierBack.NavigateUrl =
        HpSSN.NavigateUrl =
        HpSSNBack.NavigateUrl =
        HpResident.NavigateUrl = "";
        ImgSign.ImageUrl = "~/Images/noimage.gif";
        imgMember.ImageUrl = "~/Images/Person.png";
        Session["AccountingManager"] = null;
        Session["AccountingManager"] = CreateAccountingManager();
        GridViewAccounting.DataSource = null;
        GridViewAccounting.DataBind();


        HpKardani.NavigateUrl = "";
        HpKardani.ClientVisible = false;
        HDFlpResident.Set("Kardani", 0);
        ChkBKardani.Checked = false;
        panelKardani.ClientVisible = false;

        _MeImageKardan =
            _MeImgSign =
                _MeImageSSN =
                _MeImageSol =
                _MeImageIdNo =
                _MeImageResident =
                _MeImageIdNoP2 = _MeImageIdNoPDes =
                _MeImageSolBack =
                _MeImageSSNBack =
                 null;

    }

    protected void SetEnabledForEditing(Boolean Enabled)
    {
        txtBirhtPlace.ClientEnabled =
        txtBirthDate.Enabled =
        txtEmail.ClientEnabled =
        txtFatherName.ClientEnabled =
        txtFaxNo.ClientEnabled =
        txtFaxNo_cityCode.ClientEnabled =
        txtFirstName.ClientEnabled =
        txtFirstNameEn.ClientEnabled =
        txtHomeAdr.ClientEnabled =
        txtHomePO.ClientEnabled =
        txtHometel.ClientEnabled =
        txtHometel_cityCode.ClientEnabled =
        txtIdNo.ClientEnabled =
        txtIssuePlace.ClientEnabled =
        txtLastName.ClientEnabled =
        txtLastNameEn.ClientEnabled =
        txtMobileNo.ClientEnabled =
        txtSSN.ClientEnabled =
        txtWebsite.ClientEnabled =
        txtWorkAdr.ClientEnabled =
        txtWorkPO.ClientEnabled =
        txtWorkTel.ClientEnabled =
        txtWorkTel_cityCode.ClientEnabled =
        drdAgent.ClientEnabled =
        drdCitId.ClientEnabled =
        drdMarId.ClientEnabled =
        drdSexId.ClientEnabled =
        chbSoLdire.ClientEnabled =
        txtArchitectorCode.Enabled =
        chbComId.Enabled =
        txtBankAccNo.ClientEnabled =
        // ASPxRoundPanelChangeStatus.Enabled = Enabled;
        ASPxLabelImgWarning.Visible = TblFile.Visible =
        drdSoId.ClientEnabled =
        PanelEnteghali.Enabled = ChEnteghali.ClientEnabled =
        txtTransferDate.Enabled =
        txtPreProvinceFileNo.ClientEnabled =
        txtTransferMeNo.ClientEnabled =
        ComboOtherProvince.ClientEnabled =
        FlpTLetter.ClientVisible =
        ChbTCheckFileNo.ClientEnabled =
        ComboDocPreProvince.ClientEnabled =
        flpSoldier.ClientVisible =
        flpSoldierBack.ClientVisible =
        flpImage.ClientVisible =
        flpSign.ClientVisible =
        flpIdNo.Visible =
        flpIdNoP2.ClientVisible =
        flpIdNoPDes.Visible =
        flpSSN.Visible =
        flpSSNBack.Visible =
        flpResident.Visible =
        FlpTLetter.ClientVisible =
        GridViewAccounting.Columns["ColumnDelete"].Visible =
         ASPxRoundPanelAccounting.Visible =
        PanelAccountingInserting.Visible =
        txtReqDesc.Enabled =
         ChkBKardani.ClientEnabled = flpKardani.ClientVisible =
        Enabled;
    }

    //private void SetRequestEnabled(int MeId)
    //{
    //    if (CmbStatus.SelectedIndex == 0)
    //        return;

    //    if (Convert.ToInt32(CmbStatus.Value.ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.Dead
    //     || Convert.ToInt32(CmbStatus.Value.ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.Fired
    //     || Convert.ToInt32(CmbStatus.Value.ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince
    //     || Convert.ToInt32(CmbStatus.Value.ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.Cancel)
    //    {
    //        SetEnabledForEditing(false);

    //        txtReqDesc.ClientEnabled = true;

    //        if (string.IsNullOrEmpty(HpIdNo.NavigateUrl))
    //            HpIdNo.ClientVisible = false;

    //        if (string.IsNullOrEmpty(HpSoldier.NavigateUrl))
    //            HpSoldier.ClientVisible = false;

    //        if (string.IsNullOrEmpty(HpSSN.NavigateUrl))
    //            HpSSN.ClientVisible = false;

    //        if (string.IsNullOrEmpty(HpResident.NavigateUrl))
    //            HpResident.ClientVisible = false;

    //        SetControlValidation();
    //    }
    //    else
    //    {
    //        CheckColor(MeId);
    //    }
    //}

    private void SetControlValidation()
    {
        if (drdAgent.ClientEnabled == false || drdAgent.Enabled == false)
        {
            drdAgent.ValidationSettings.RequiredField.IsRequired = false;
        }
        else
            drdAgent.ValidationSettings.RequiredField.IsRequired = true;

        if (txtSSN.ClientEnabled == false || txtSSN.Enabled == false)
        {
            txtSSN.ValidationSettings.RequiredField.IsRequired = false;
        }
        else
            txtSSN.ValidationSettings.RequiredField.IsRequired = true;

        if (txtHometel.ClientEnabled == false || txtHometel.Enabled == false)
        {
            txtHometel.ValidationSettings.RequiredField.IsRequired = false;
        }
        else
            txtHometel.ValidationSettings.RequiredField.IsRequired = true;

        if (txtHomeAdr.ClientEnabled == false || txtHometel.Enabled == false)
        {
            txtHomeAdr.ValidationSettings.RequiredField.IsRequired = false;
        }
        else
            txtHomeAdr.ValidationSettings.RequiredField.IsRequired = true;
    }
    #endregion

    #region Accounting Fish
    protected void FillGrid()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        int TableType = -1;
        TSP.DataManager.TableTypeManager TableTypeManager = new TSP.DataManager.TableTypeManager();
        int IsMeTemp = -1;
        if (!Utility.IsDBNullOrNullValue(HiddenFieldInfo["TMe"]))
            IsMeTemp = Convert.ToInt32(Utility.DecryptQS(HiddenFieldInfo["TMe"].ToString()));
        if (IsMeTemp == 0)
            TableTypeManager.FindByTtCode(TSP.DataManager.TableType.Member);
        else if (IsMeTemp == 1)
            TableTypeManager.FindByTtCode(TSP.DataManager.TableType.TempMember);
        if (TableTypeManager.Count == 1)
        {
            TableType = Convert.ToInt32(TableTypeManager[0]["TtId"]);
        }
        AccountingManager.FindByTableTypeId(_MeId, TableType);
        GridViewAccounting.DataSource = AccountingManager.DataTable;
        GridViewAccounting.DataBind();
    }

    protected void RowInserting()
    {
        //string ProjectId = Utility.DecryptQS(HDProjectId.Value);

        if (Session["AccountingManager"] == null)
            Session["AccountingManager"] = CreateAccountingManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];

        DataRow dr = AccountingManager.NewRow();

        dr.BeginEdit();
        dr["TableTypeId"] = -1;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.Member;
        //dr["Type"] = cmbaType.Value;
        dr["Type"] = (int)TSP.DataManager.AccountingPaymentType.Fiche;
        //dr["TypeName"] = cmbaType.SelectedItem.Text;
        //if (cmbaType.Value.ToString() == "1")
        //{
        dr["Bank"] = DBNull.Value;
        dr["BranchCode"] = DBNull.Value;
        dr["BranchName"] = DBNull.Value;
        //}
        //else
        //{
        //    dr["Bank"] = txtaBank.Text;
        //    dr["BranchCode"] = txtaBranchCode.Text;
        //    dr["BranchName"] = txtaBranchName.Text;
        //}
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
    #endregion

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
        arr.Add(0);//arr[5]-->Request

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        //سرعت را خیلی پایین آورده بود کامنت شد
        ////////ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        ////////if (ProjectJobHistoryManager.Count > 0)
        ////////{
        ////////    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
        ////////    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
        ////////    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
        ////////    arr[1] = 1;
        ////////}
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    protected void CheckColor(int MeId)
    {
        bool change = false;
        // TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AttachmentsManager attachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();

        int IsMeTemp = -1;
        if (!Utility.IsDBNullOrNullValue(HiddenFieldInfo["TMe"]))
            IsMeTemp = Convert.ToInt32(Utility.DecryptQS(HiddenFieldInfo["TMe"].ToString()));
        //MeManager.FindByCode(MeId);
        MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
        if (MemberRequestManager.Count > 0)
        {
            int MreId = -1;
            //MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
            //if (MemberRequestManager.Count > 0)
            //{
            MreId = Convert.ToInt32(MemberRequestManager[0]["MreId"]);
            //}
            if (drdAgent.Value != null && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["AgentId"]))
            {
                if (drdAgent.Value.ToString() != MemberRequestManager[0]["AgentId"].ToString())
                {
                    drdAgent.ForeColor = Color.Red;
                    change = true;
                }

                //if (drdAgent.SelectedItem.Text != CmbReAgent.SelectedItem.Text)
                //    CmbReAgent.ForeColor = Color.Red;
            }
            else if (drdAgent.Value != null && Utility.IsDBNullOrNullValue(MemberRequestManager[0]["AgentId"]))
            {
                drdAgent.ForeColor = Color.Red;
                change = true;
            }

            if (MreId != -1)
            {
                attachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MreId, (short)TSP.DataManager.AttachType.IdNo);
                if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && attachmentsManager.Count == 0)
                {
                    HpIdNo.ForeColor = Color.Red;
                    change = true;
                }
                else if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    if (HpIdNo.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
                    {
                        HpIdNo.ForeColor = Color.Red;
                        change = true;
                    }
                }
                else if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    HpIdNo.ForeColor = Color.Red;
                    change = true;
                }


                //Session["FileOfSSN"]

                attachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MreId, (short)TSP.DataManager.AttachType.SSN);
                if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && attachmentsManager.Count == 0)
                {
                    HpSSN.ForeColor = Color.Red;
                    change = true;
                }
                else if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    if (HpSSN.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
                    {
                        HpSSN.ForeColor = Color.Red;
                        change = true;
                    }
                }
                else if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    HpSSN.ForeColor = Color.Red;
                    change = true;
                }

                attachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MreId, (short)TSP.DataManager.AttachType.ResidentDoc);
                if (!Utility.IsDBNullOrNullValue(HpResident.NavigateUrl) && attachmentsManager.Count == 0)
                {
                    HpResident.ForeColor = Color.Red;
                    change = true;
                }
                else if (!Utility.IsDBNullOrNullValue(HpResident.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    if (HpResident.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
                    {
                        HpResident.ForeColor = Color.Red;
                        change = true;
                    }
                }
                else if (!Utility.IsDBNullOrNullValue(HpResident.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    HpResident.ForeColor = Color.Red;
                    change = true;
                }


                //Session["FileOfSol"]
                attachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MreId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && attachmentsManager.Count == 0)
                {
                    HpSoldier.ForeColor = Color.Red;
                    change = true;
                }
                else if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    if (HpSoldier.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
                    {
                        HpSoldier.ForeColor = Color.Red;
                        change = true;
                    }
                }
                else if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
                {
                    HpSoldier.ForeColor = Color.Red;
                    change = true;
                }

            }

            if (txtBankAccNo.Text != MemberRequestManager[0]["BankAccNo"].ToString())
            {
                txtBankAccNo.ForeColor = Color.Red;
                change = true;
            }

            if (!String.IsNullOrWhiteSpace(imgMember.ImageUrl))
            {
                if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["ImageUrl"]))
                {
                    if (MemberRequestManager[0]["ImageUrl"].ToString() != imgMember.ImageUrl)
                    {
                        imgMember.Border.BorderColor = Color.Red;
                        change = true;
                    }
                }
                else// if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["ImageUrl"]))
                {
                    imgMember.Border.BorderColor = Color.Red;
                    change = true;
                }
            }


            if (!Utility.IsDBNullOrNullValue(ImgSign.ImageUrl) && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["SignUrl"]))
            {
                if (ImgSign.ImageUrl != MemberRequestManager[0]["SignUrl"].ToString())
                {
                    ImgSign.Border.BorderColor = Color.Red;
                    change = true;
                }
            }
            else if (!Utility.IsDBNullOrNullValue(ImgSign.ImageUrl) && Utility.IsDBNullOrNullValue(MemberRequestManager[0]["SignUrl"]))
            {
                ImgSign.Border.BorderColor = Color.Red;
                change = true;
            }


            if (drdCitId.Value != null && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["CitId"]))
            {
                if (drdCitId.Value.ToString() != MemberRequestManager[0]["CitId"].ToString())
                {
                    drdCitId.ForeColor = Color.Red;
                    change = true;
                }
            }

            if (txtEmail.Text != MemberRequestManager[0]["Email"].ToString())
            {
                txtEmail.ForeColor = Color.Red;
                change = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["FaxNo"]) && MemberRequestManager[0]["FaxNo"].ToString().Contains("-"))
            {
                if (txtFaxNo.Text != MemberRequestManager[0]["FaxNo"].ToString().Substring(MemberRequestManager[0]["FaxNo"].ToString().IndexOf("-") + 1, MemberRequestManager[0]["FaxNo"].ToString().Length - MemberRequestManager[0]["FaxNo"].ToString().IndexOf("-") - 1))
                {
                    txtFaxNo.ForeColor = Color.Red;
                    change = true;
                }
                if (txtFaxNo_cityCode.Text != MemberRequestManager[0]["FaxNo"].ToString().Substring(0, MemberRequestManager[0]["FaxNo"].ToString().IndexOf("-")))
                {
                    txtFaxNo_cityCode.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["FaxNo"]) && !MemberRequestManager[0]["FaxNo"].ToString().Contains("-"))
            {
                if (txtFaxNo.Text != MemberRequestManager[0]["FaxNo"].ToString())
                {
                    txtFaxNo.ForeColor = Color.Red;
                    change = true;
                }
            }

            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["FaxNo"]) && !string.IsNullOrEmpty(txtFaxNo.Text))
            {
                txtFaxNo.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["FaxNo"]) && !string.IsNullOrEmpty(txtFaxNo_cityCode.Text))
            {
                txtFaxNo_cityCode.ForeColor = Color.Red;
                change = true;
            }

            if (txtHomeAdr.Text.Trim() != MemberRequestManager[0]["HomeAdr"].ToString())
            {
                txtHomeAdr.ForeColor = Color.Red;
                change = true;
            }
            string HomePO = Utility.ConvertDBNullToString(MemberRequestManager[0]["HomePO"]);
            if (txtHomePO.Text != HomePO)
            {
                txtHomePO.ForeColor = Color.Red;
                change = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["HomeTel"]) && MemberRequestManager[0]["HomeTel"].ToString().Contains("-"))
            {
                if (txtHometel.Text != MemberRequestManager[0]["HomeTel"].ToString().Substring(MemberRequestManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MemberRequestManager[0]["HomeTel"].ToString().Length - MemberRequestManager[0]["HomeTel"].ToString().IndexOf("-") - 1))
                {
                    txtHometel.ForeColor = Color.Red;
                    change = true;
                }

                if (txtHometel_cityCode.Text != MemberRequestManager[0]["HomeTel"].ToString().Substring(0, MemberRequestManager[0]["HomeTel"].ToString().IndexOf("-")))
                {
                    txtHometel_cityCode.ForeColor = Color.Red;
                    change = true;
                }

            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["HomeTel"]) && !MemberRequestManager[0]["HomeTel"].ToString().Contains("-"))
            {
                if (txtHometel.Text != MemberRequestManager[0]["HomeTel"].ToString())
                {
                    txtHometel.ForeColor = Color.Red;
                    change = true;
                }
            }
            //string HomeTel = Utility.ConvertDBNullToString(MeManager[0]["HomeTel"]);
            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["HomeTel"]) && !string.IsNullOrEmpty(txtHometel.Text))
            {
                txtHometel.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["HomeTel"]) && !string.IsNullOrEmpty(txtHometel_cityCode.Text))
            {
                txtHometel_cityCode.ForeColor = Color.Red;
                change = true;
            }

            if (txtLastName.Text != MemberRequestManager[0]["LastName"].ToString())
            {
                txtLastName.ForeColor = Color.Red;
                change = true;
            }
            if (txtLastNameEn.Text != MemberRequestManager[0]["LastNameEn"].ToString())
            {
                txtLastNameEn.ForeColor = Color.Red;
                change = true;
            }
            if (drdMarId.Value != null && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["MarId"]))
            {
                if (drdMarId.Value.ToString() != MemberRequestManager[0]["MarId"].ToString())
                {
                    drdMarId.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (drdSexId.Value != null && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["SexId"]))
            {
                if (drdSexId.Value.ToString() != MemberRequestManager[0]["SexId"].ToString())
                {
                    drdSexId.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (txtMobileNo.Text != MemberRequestManager[0]["MobileNo"].ToString())
            {
                txtMobileNo.ForeColor = Color.Red;
                change = true;
            }
            if (txtFirstName.Text != MemberRequestManager[0]["FirstName"].ToString())
            {
                txtFirstName.ForeColor = Color.Red;
                change = true;
            }
            if (txtFirstNameEn.Text != MemberRequestManager[0]["FirstNameEn"].ToString())
            {
                txtFirstNameEn.ForeColor = Color.Red;
                change = true;
            }
            if (drdSoId.Value != null && !Utility.IsDBNullOrNullValue(MemberRequestManager[0]["SoId"]))
            {
                if (drdSoId.Value.ToString() != MemberRequestManager[0]["SoId"].ToString())
                {
                    drdSoId.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (txtWebsite.Text != MemberRequestManager[0]["Website"].ToString())
            {
                txtWebsite.ForeColor = Color.Red;
                change = true;
            }
            if (txtWorkAdr.Text != MemberRequestManager[0]["WorkAdr"].ToString())
            {
                txtWorkAdr.ForeColor = Color.Red;
                change = true;
            }
            string WorkPO = Utility.ConvertDBNullToString(MemberRequestManager[0]["WorkPO"]);
            if (txtWorkPO.Text != WorkPO)
            {
                txtWorkPO.ForeColor = Color.Red;
                change = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["WorkTel"]) && MemberRequestManager[0]["WorkTel"].ToString().Contains("-"))
            {
                if (txtWorkTel.Text != MemberRequestManager[0]["WorkTel"].ToString().Substring(MemberRequestManager[0]["WorkTel"].ToString().IndexOf("-") + 1, MemberRequestManager[0]["WorkTel"].ToString().Length - MemberRequestManager[0]["WorkTel"].ToString().IndexOf("-") - 1))
                {
                    txtWorkTel.ForeColor = Color.Red;
                    change = true;
                }
                if (txtWorkTel_cityCode.Text != MemberRequestManager[0]["WorkTel"].ToString().Substring(0, MemberRequestManager[0]["WorkTel"].ToString().IndexOf("-")))
                {
                    txtWorkTel_cityCode.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(MemberRequestManager[0]["WorkTel"]) && !MemberRequestManager[0]["WorkTel"].ToString().Contains("-"))
            {
                if (txtWorkTel.Text != MemberRequestManager[0]["WorkTel"].ToString())
                {
                    txtWorkTel.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["WorkTel"]) && !string.IsNullOrEmpty(txtWorkTel.Text))
            {
                txtWorkTel.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["WorkTel"]) && !string.IsNullOrEmpty(txtWorkTel_cityCode.Text))
            {
                txtWorkTel_cityCode.ForeColor = Color.Red;
                change = true;
            }

            if (txtFatherName.Text != MemberRequestManager[0]["FatherName"].ToString())
            {
                txtFatherName.ForeColor = Color.Red;
                change = true;
            }
            if (txtIdNo.Text != MemberRequestManager[0]["IdNo"].ToString())
            {
                txtIdNo.ForeColor = Color.Red;
                change = true;
            }
            if (txtSSN.Text != MemberRequestManager[0]["SSN"].ToString())
            {
                txtSSN.ForeColor = Color.Red;
                change = true;
            }
            if (txtIssuePlace.Text != MemberRequestManager[0]["IssuePlace"].ToString())
            {
                txtIssuePlace.ForeColor = Color.Red;
                change = true;
            }
            if (txtBirhtPlace.Text != MemberRequestManager[0]["BirthPlace"].ToString())
            {
                txtBirhtPlace.ForeColor = Color.Red;
                change = true;
            }
            if (txtBirthDate.Text != MemberRequestManager[0]["BirhtDate"].ToString())
            {
                txtBirthDate.ForeColor = Color.Red;
                change = true;
            }

        }
        if (change == true)
        {
            if (Session["MenuArrayList"] != null)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
                if (Session["MenuArrayList"] != null)
                {
                    ArrayList arr = (ArrayList)Session["MenuArrayList"];
                    arr[5] = 1;
                    Session["MenuArrayList"] = arr;
                }
            }
            else
            {
                if (!Utility.IsDBNullOrNullValue(_MReId))
                    CheckMenuImage(MeId, _MReId);
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
                if (Session["MenuArrayList"] != null)
                {
                    ArrayList arr = (ArrayList)Session["MenuArrayList"];
                    arr[5] = 1;
                    Session["MenuArrayList"] = arr;
                }
            }


        }


    }

    protected TSP.DataManager.TechnicalServices.AccountingManager CreateAccountingManager()
    {
        TSP.DataManager.TechnicalServices.AccountingManager manager = new TSP.DataManager.TechnicalServices.AccountingManager();
        return manager;
    }

    void FillAspxGridFlp(DataTable dt)
    {
        AspxGridFlp.DataSource = dt;
        AspxGridFlp.DataBind();
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");        
        this.LabelWarning.Text = Message;
    }
    #endregion
}
