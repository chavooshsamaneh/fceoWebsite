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

public partial class Members_MemberInfo_MemberRequestInsert : System.Web.UI.Page
{
    DataTable dtOfImg = null;
    private bool IsPageRefresh = false;

    int _MeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldMember["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString())));
            }
        }
        set
        {
            HiddenFieldMember["MeId"] = value;
        }
    }

    int _MReId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldMember["MReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MReId"].ToString())));
            }
        }
        set
        {
            HiddenFieldMember["MReId"] = value;
        }
    }

    string _PageMode
    {
        get
        {
            return HiddenFieldMember["PageMode"].ToString();
        }
        set
        {
            HiddenFieldMember["PageMode"] = value;
        }
    }
    string _MeImageKardan
    {
        get
        {
            try
            {
                return HiddenFieldMember["MeImageKardan"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldMember["MeImageKardan"] = value;
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
        SetPageLoad();

        if (Session["TblOfImg9"] != null)
            BindAspxGridFlp((DataTable)Session["TblOfImg9"]);
        ////// امکان ویرایش نام و نام خانوادگی و نمایندگی و شماره حساب از پرتال اعضا وجود نداشته باشد///////
        SetEnabledForMember();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    #region btnClick
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfImg9"] =
        Session["FileOfResident"] = null;
        Response.Redirect("MemberRequest.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_IsLock())
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            ShowMessage("به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.");
            return;

        }
        if (_MeId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        try
        {
            if (_PageMode == "New")
                Insert();
            else if (_PageMode == "Edit")
            {
                Edit(_MReId);
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion

    #region FileUpload
    /// <summary>
    /// فایل پیوست
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void flpImage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageMember(e.UploadedFile);
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

    protected void FlpTLetter_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageLetter(e.UploadedFile);
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

    protected void flpResident_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageResident(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    protected void AspxGridFlp_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Split('$')[0] != "Insert")
            return;

        AspxGridFlp.JSProperties["cpMessage"] = "";
        AspxGridFlp.JSProperties["cpState"] = "-1";

        if (Session["TblOfImg9"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImg9"];

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
                BindAspxGridFlp(dtOfImg);

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

    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        AspxGridFlp.JSProperties["cpMessage"] = "";

        BindAspxGridFlp((DataTable)Session["TblOfImg9"]);

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
            BindAspxGridFlp((DataTable)Session["TblOfImg9"]);
            dtOfImg = (DataTable)Session["TblOfImg9"];
        }

    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        Session["TblOfImg9"] = null;

        //if (PageMode == "New")
        //{
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&Mode=" + Utility.EncryptQS("Request"));
                break;
            //case "Research":
            //    Response.Redirect("MemberResearch.aspx?MeId=" + MemberId.Value  + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value);
            //    break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&Mode=" + Utility.EncryptQS("Request"));
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&Mode=" + Utility.EncryptQS("Request"));
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&Mode=" + Utility.EncryptQS("Request"));
                break;

        }
    }

    //protected void CallbackPanelEnteghali_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    //{
    //    string value = e.Parameter;
    //    if (value == "true")
    //    {
    //        ComboTPr.ValidationSettings.RequiredField.IsRequired = true;
    //        txtTMeNo.ValidationSettings.RequiredField.IsRequired = true;
    //        PanelEnteghali.ClientVisible = true;
    //    }
    //    else
    //    {
    //        ComboTPr.ValidationSettings.RequiredField.IsRequired = false;
    //        txtTMeNo.ValidationSettings.RequiredField.IsRequired = false;
    //        PanelEnteghali.ClientVisible = false;
    //    }
    //    ComboTPr.SelectedIndex = -1;
    //    txtTDate.Text = "";
    //    txtTMeNo.Text = "";
    //    Session["LetterImg2"] = null;
    //    HDFlpLetter.Set("name", 0);
    //    Timg.ImageUrl = "";
    //    ChbTCheckFileNo.Checked = false;
    //    txtTFileNo.Text = "";
    //}
    #endregion

    #region Methods

    private void SetPageLoad()
    {
        ASPxLabelImgWarning.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";
        //SetEnable();

        if (!IsPostBack)
        {

            #region ResetSession

            ViewState["Img"] = "";
            Session["MeReqUpload"] =
            Session["TblOfImg9"] =
            Session["MeSignUpload"] =
            Session["FileOfIdNo"] = Session["FileOfIdNoP2"] = Session["FileOfIdNoPDes"] =
            Session["FileOfSSN"] = Session["FileOfSSNBack"] =
            Session["LetterImg2"] =
            Session["FileOfSol"] = Session["FileOfSolBack"] =
            Session["MenuArrayList"] =
            Session["MeImgUpload"] =            
            Session["FileOfResident"] = null;

            #endregion


            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["MReId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");
            }

            if (Session["TblOfImg9"] == null)
            {
                Session["TblOfImg9"] = CreateDataTableAttachments();
            }
            else
                dtOfImg = (DataTable)Session["TblOfImg9"];
            BindAspxGridFlp(dtOfImg);
            _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
            _MeId = Utility.GetCurrentUser_MeId();
            _MReId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MReId"].ToString())));
            if (_MeId == null || _MReId == null || string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            OdbProvince.FilterParameters[0].DefaultValue = Utility.GetCurrentProvinceNezamCode().ToString();
            #region SetPageMode
            switch (_PageMode)
            {
                case "New":
                    SetNewMode();
                    break;
                case "Edit":
                    SetEditMode();
                    break;
                case "View":
                    SetViewMode();
                    break;
            }
            #endregion

        }
        if (Session["MeImgUpload"] != null)
            imgMember.ImageUrl = Session["MeImgUpload"].ToString();

        if (Session["MeSignUpload"] != null)
            ImgSign.ImageUrl = Session["MeSignUpload"].ToString();
        if (Session["FileOfResident"] != null)
        {
            HypLinkResident.ClientVisible = true;
            HypLinkResident.NavigateUrl = Session["FileOfResident"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(_MeImageKardan) )
        {
            HpKardani.ClientVisible = true;
            HpKardani.NavigateUrl = _MeImageKardan;
        }
        if (Session["FileOfIdNo"] != null)
        {
            HpIdNo.ClientVisible = true;
            HpIdNo.NavigateUrl = Session["FileOfIdNo"].ToString();
        }
        if (Session["FileOfIdNoP2"] != null)
        {
            HIdNoP2.ClientVisible = true;
            HIdNoP2.NavigateUrl = Session["FileOfIdNoP2"].ToString();
        }
        if (Session["FileOfIdNoPDes"] != null)
        {
            HIdNoPDes.ClientVisible = true;
            HIdNoPDes.NavigateUrl = Session["FileOfIdNoPDes"].ToString();
        }
        if (Session["FileOfSSN"] != null)
        {
            HpSSN.ClientVisible = true;
            HpSSN.NavigateUrl = Session["FileOfSSN"].ToString();
        }
        if (Session["FileOfSSNBack"] != null)
        {
            hssnBack.ClientVisible = true;
            hssnBack.NavigateUrl = Session["FileOfSSNBack"].ToString();
        }
        if (Session["FileOfSol"] != null)
        {
            HpSoldier.ClientVisible = true;
            HpSoldier.NavigateUrl = Session["FileOfSol"].ToString();
        }
        if (Session["FileOfSolBack"] != null)
        {
            HpSoldierBack.ClientVisible = true;
            HpSoldierBack.NavigateUrl = Session["FileOfSolBack"].ToString();
        }
        if (Session["LetterImg2"] != null)
        {
            imgletter.ClientVisible = true;
            imgletter.ImageUrl = Session["LetterImg2"].ToString();
        }

        //SetEnteghaliVisible();
    }

    private void SetNewMode()
    {
        FillFormForInsert(_MeId);
        RoundPanelPage.HeaderText = "جدید";
        ASPxMenu1.Enabled = false;
        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        SetEnable(true);
        SetSoldireControlsVisible();
        SetRoundPanelHeaderByReq(-1, _PageMode);
        SetEnteghaliVisible();
    }

    private void SetEditMode()
    {
        RoundPanelPage.HeaderText = "ویرایش";
        FillFormRequest(_MReId);
        SetEnable(true);
        _PageMode = "Edit";
        ASPxMenu1.Enabled = true;
        SetSoldireControlsVisible();
        SetEnteghaliVisible();
    }

    private void SetViewMode()
    {
        // Disable();
        btnSave.Visible = false;
        btnSave2.Visible = false;
        RoundPanelPage.HeaderText = "مشاهده";
        FillFormRequest(_MReId);
        SetEnable(false);
        SetSoldireControlsVisible();
        SetEnteghaliVisible();
    }
    private void SetSoldireControlsVisible()
    {
        if (drdSexId.Value != null)
        {
            if (Convert.ToInt32(drdSexId.Value) == (int)TSP.DataManager.SexManager.Sex.Male)
            {
                lblSolFile.ClientVisible = flpSoldier.ClientVisible = flpSoldierBack.ClientVisible =
                drdSoId.ClientVisible = lblSolStatus.ClientVisible = lblWarnSolImage.ClientVisible = lblWarnSolImage.ClientVisible = true;
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

                drdSoId.ClientVisible = lblSolStatus.ClientVisible = lblWarnSolImage.ClientVisible = false;
            }
        }
    }

    private void SetEnteghaliVisible()
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
        if (Pagemode == "New")// TSP.DataManager.MemberRequestType.Request.ToString())
        {
            RoundPanelPage.HeaderText += "-درخواست تغییرات";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Dead)
        {
            RoundPanelPage.HeaderText += "-درخواست فوت شده";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.FakeLicense)
        {
            RoundPanelPage.HeaderText += "-درخواست ثبت مدرک جعلی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Fired)
        {
            RoundPanelPage.HeaderText += "-درخواست اخراج از سازمان";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Cancel)
        {
            RoundPanelPage.HeaderText += "-درخواست لغو عضویت";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ReturnToCurrentProvince)
        {
            RoundPanelPage.HeaderText += "-درخواست بازگشت به سازمان";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince)
        {
            RoundPanelPage.HeaderText += "-درخواست انتقال به استان دیگر";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.AgentChange)
        {
            RoundPanelPage.HeaderText += "-درخواست تغییرات نمایندگی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.BankAccNoChange)
        {
            RoundPanelPage.HeaderText += "-درخواست تغییرات شماره حساب";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ChangeBaseInfo)
        {
            RoundPanelPage.HeaderText += "-درخواست تغییرات اطلاعات پایه";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.ChangeLicence)
        {
            RoundPanelPage.HeaderText += "-درخواست تغییرات مدرک تحصیلی";
        }
        else if (isCreated == (int)TSP.DataManager.MemberRequestType.Create)
        {
            RoundPanelPage.HeaderText += "-درخواست ثبت اولیه";
        }
    }
    #region Save Images
    /// <summary>
    /// فایل پیوست
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    protected string SaveImage(UploadedFile uploadedFile)
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

    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeSign" + _MeId.ToString() + "MeSign" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Members/Sign/Request/") + ret) == true);//|| File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = "~/Image/Members/Sign/Request/" + ret;// MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Session["MeSignUpload"] = tempFileName;

        }
        return ret;
    }

    protected string SaveImageMember(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeImg" + _MeId.ToString() + "MeImg" + Path.GetRandomFileName() + ImageType.Extension;
                ret2 = "MeImg" + _MeId.ToString() + "MeImg" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Members/Person/Request/") + ret) == true);
            string tempFileName = "~/Image/Members/Person/Request/" + ret;
            string tempFileName2 = "~/Image/Members/Person/Request/" + ret2;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Utility.FixedSize(MapPath(tempFileName), MapPath(tempFileName2), Utility.HorRes, Utility.VerRes);
            Session["MeImgUpload"] = tempFileName2;

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
                ret = "MeIdNo" + _MeId.ToString() + "MeIdNo" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/IdNo/Request/") + ret) == true);
            string tempFileName = "~/image/Members/IdNo/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            if (id == "flpIdNo")
                Session["FileOfIdNo"] = tempFileName;
            if (id == "flpIdNoP2")
                Session["FileOfIdNoP2"] = tempFileName;
            if (id == "flpIdNoPDes")
                Session["FileOfIdNoPDes"] = tempFileName;
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
                ret = "MeSSN" + _MeId.ToString() + "MeSSN" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/SSN/Request/") + ret) == true);
            string tempFileName = "~/image/Members/SSN/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);

            if (id == "flpSSN")
                Session["FileOfSSN"] = tempFileName;// tempFileName2;

            if (id == "flpSSNBack")
                Session["FileOfSSNBack"] = tempFileName;

        }
        return ret;
    }

    protected string SaveImageLetter(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeTrans" + _MeId.ToString() + "MeTrans" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/Transport/") + ret) == true);
            string tempFileName = "~/image/Members/Transport/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Session["LetterImg2"] = tempFileName;
        }
        return ret;
    }

    protected string SaveImageResident(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        string tempFileName = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                if (id == "flpResident")
                    ret = "MeResid" + _MeId.ToString() + "MeMeResid" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Resident/Request/") + ret) == true);


            if (id == "flpResident")
            {
                tempFileName = "~/image/Members/Resident/Request/" + ret;
                Session["FileOfResident"] = tempFileName;
            }

            uploadedFile.SaveAs(MapPath(tempFileName), true);
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
    
    protected string SaveImageSoldier(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeSol" + _MeId.ToString() + "MeSol" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Soldier/Request/") + ret) == true);
            string tempFileName = "~/image/Members/Soldier/Request/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);

            if (id == "flpSoldier")
                Session["FileOfSol"] = tempFileName;// tempFileName2;
            if (id == "flpSoldierBack")
                Session["FileOfSolBack"] = tempFileName;

        }
        return ret;
    }

    #endregion

    #region Fill Forms
    protected void FillFormForInsert(int MeId)
    {
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();

        memberManager.FindByCode(MeId);
        if (memberManager.Count <= 0)
        {
            ShowMessage("امکان مشاهده اطلاعات وجود ندارد");
            return;

        }
        #region  اطلاعات پایه
        txtMeNo.Text = memberManager[0]["MeNo"].ToString();
        //txtFileNo.Text = memberManager[0]["FileNo"].ToString();

        txtFirstName.Text = memberManager[0]["FirstName"].ToString();
        txtLastName.Text = memberManager[0]["LastName"].ToString();
        txtFirstNameEn.Text = memberManager[0]["FirstNameEn"].ToString();
        txtLastNameEn.Text = memberManager[0]["LastNameEn"].ToString();
        txtMobileNo.Text = memberManager[0]["MobileNo"].ToString();
        txtHomeAdr.Text = memberManager[0]["HomeAdr"].ToString();
        txtHomePO.Text = memberManager[0]["HomePO"].ToString();
        txtWorkAdr.Text = memberManager[0]["WorkAdr"].ToString();
        txtWorkPO.Text = memberManager[0]["WorkPO"].ToString();
        txtWebsite.Text = memberManager[0]["Website"].ToString();
        txtEmail.Text = memberManager[0]["Email"].ToString();
        txtBirhtPlace.Text = memberManager[0]["BirthPlace"].ToString();
        txtBirthDate.Text = memberManager[0]["BirhtDate"].ToString();
        txtIdNo.Text = memberManager[0]["IdNo"].ToString();
        txtSSN.Text = memberManager[0]["SSN"].ToString();
        txtIssuePlace.Text = memberManager[0]["IssuePlace"].ToString();
        txtFatherName.Text = memberManager[0]["FatherName"].ToString();
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["BankAccNo"]))
            txtBankAccNo.Text = memberManager[0]["BankAccNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["ArchitectorCode"]))
            txtArchitectorCode.Text = memberManager[0]["ArchitectorCode"].ToString();
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["NezamKardanConfirmURL"]))
        {
            _MeImageKardan = HpKardani.NavigateUrl = memberManager[0]["NezamKardanConfirmURL"].ToString();
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
        string htel = memberManager[0]["HomeTel"].ToString();
        if (memberManager[0]["HomeTel"].ToString() != "")
        {
            if (memberManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
            {
                txtHometel_cityCode.Text = memberManager[0]["HomeTel"].ToString().Substring(0, memberManager[0]["HomeTel"].ToString().IndexOf("-"));
                txtHometel.Text = memberManager[0]["HomeTel"].ToString().Substring(memberManager[0]["HomeTel"].ToString().IndexOf("-") + 1, memberManager[0]["HomeTel"].ToString().Length - memberManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtHometel.Text = memberManager[0]["HomeTel"].ToString();
            }
        }

        string wtel = memberManager[0]["WorkTel"].ToString();
        if (memberManager[0]["WorkTel"].ToString() != "")
        {
            if (memberManager[0]["WorkTel"].ToString().IndexOf("-") > 0)
            {
                txtWorkTel_cityCode.Text = memberManager[0]["WorkTel"].ToString().Substring(0, memberManager[0]["WorkTel"].ToString().IndexOf("-"));
                txtWorkTel.Text = memberManager[0]["WorkTel"].ToString().Substring(memberManager[0]["WorkTel"].ToString().IndexOf("-") + 1, memberManager[0]["WorkTel"].ToString().Length - memberManager[0]["WorkTel"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtWorkTel.Text = memberManager[0]["WorkTel"].ToString();
            }
        }

        string ftel = memberManager[0]["FaxNo"].ToString();
        if (memberManager[0]["FaxNo"].ToString() != "")
        {
            if (memberManager[0]["FaxNo"].ToString().IndexOf("-") > 0)
            {
                txtFaxNo_cityCode.Text = memberManager[0]["FaxNo"].ToString().Substring(0, memberManager[0]["FaxNo"].ToString().IndexOf("-"));
                txtFaxNo.Text = memberManager[0]["FaxNo"].ToString().Substring(memberManager[0]["FaxNo"].ToString().IndexOf("-") + 1, memberManager[0]["FaxNo"].ToString().Length - memberManager[0]["FaxNo"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtFaxNo.Text = memberManager[0]["FaxNo"].ToString();
            }
        }

        if (!Utility.IsDBNullOrNullValue(memberManager[0]["CitId"]))
        {
            drdCitId.DataBind();
            drdCitId.SelectedIndex = drdCitId.Items.FindByValue(memberManager[0]["CitId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["AgentId"]))
        {
            drdAgent.DataBind();
            drdAgent.SelectedIndex = drdAgent.Items.FindByValue(memberManager[0]["AgentId"].ToString()).Index;
        }
        #endregion

        #region کمیسیون ها
        int comId = 0;
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["ComId"]))
        {
            comId = int.Parse(memberManager[0]["ComId"].ToString());

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

        int MReId = -2;
        ReqManager.FindByMemberId(MeId, 0, 1, -1);
        if (ReqManager.Count > 0)
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);

        #region جنسیت و سربازی
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["SexId"]))
        {
            drdSexId.DataBind();
            drdSexId.SelectedIndex = drdSexId.Items.FindByValue(memberManager[0]["SexId"].ToString()).Index;
        }
        if (Convert.ToInt32(drdSexId.Value) == Convert.ToInt32(TSP.DataManager.SexManager.Sex.Male))
        {
            if (!Utility.IsDBNullOrNullValue(memberManager[0]["SoId"]))
            {
                drdSoId.DataBind();
                drdSoId.SelectedIndex = drdSoId.Items.FindByValue(memberManager[0]["SoId"].ToString()).Index;
            }

            if (!Utility.IsDBNullOrNullValue(memberManager[0]["MilitaryCommitment"]))
            {
                chbSoLdire.Checked = Convert.ToBoolean(memberManager[0]["MilitaryCommitment"]);
            }
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
            if (attachManager.Count > 0)
            {
                HpSoldier.ClientVisible = true;
                Session["FileOfSol"] = HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSol.Set("name", 1);
            }
            else
                HpSoldier.ClientVisible = false;

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);

            if (attachManager.Count > 0)
            {
                HpSoldierBack.ClientVisible = true;
                Session["FileOfSolBack"] = HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HDFlpSol.Set("SolBack", 1);
            }
            else
                HpSoldierBack.ClientVisible = false;

        }

        #endregion

        if (!Utility.IsDBNullOrNullValue(memberManager[0]["MarId"]))
        {
            drdMarId.DataBind();
            drdMarId.SelectedIndex = drdMarId.Items.FindByValue(memberManager[0]["MarId"].ToString()).Index;
        }
        #region تصویر پرسنلی و تصویر امضا
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["ImageUrl"]))
        {
            Session["MeImgUpload"] = imgMember.ImageUrl = memberManager[0]["ImageUrl"].ToString();
            HDFlpMember.Set("name", 1);
        }
        if (!Utility.IsDBNullOrNullValue(memberManager[0]["SignUrl"]))
        {
            ImgSign.ImageUrl = memberManager[0]["SignUrl"].ToString();
            HDFlpSign.Set("name", 1);
        }
        #endregion
        FillTransfer(MReId);
        //SetEnteghaliVisible();
        #region Attachments
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.Attachments);
        dtOfImg = (DataTable)Session["TblOfImg9"];
        for (int i = 0; i < attachManager.Count; i++)
        {
            DataRow dr = dtOfImg.NewRow();
            dr[0] = attachManager[i]["FilePath"].ToString();
            dr[1] = attachManager[i]["FilePath"].ToString();
            dr[5] = attachManager[i]["Description"].ToString();

            string fileName = Path.GetFileName(attachManager[0]["FilePath"].ToString());
            dr[2] = fileName;
            dr[3] = 1;
            dr[4] = attachManager[i][0];
            dtOfImg.Rows.Add(dr);
        }
        dtOfImg.AcceptChanges();
        BindAspxGridFlp(dtOfImg);


        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
        if (attachManager.Count > 0)
        {
            HpIdNo.ClientVisible = true;
            Session["FileOfIdNo"] = HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpIdNo.Set("name", 1);
        }
        else
            HpIdNo.ClientVisible = false;


        attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
        if (attachManager.Count > 0)
        {
            HIdNoP2.ClientVisible = true;
            Session["FileOfIdNoP2"] = HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpIdNo.Set("IdNoP2", 1);
        }
        else
            HIdNoP2.ClientVisible = false;


        attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
        if (attachManager.Count > 0)
        {
            HIdNoPDes.ClientVisible = true;
            Session["FileOfIdNoPDes"] = HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpIdNo.Set("IdNoPDes", 1);
        }
        else
            HIdNoPDes.ClientVisible = false;


        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
        if (attachManager.Count > 0)
        {
            HpSSN.ClientVisible = true;
            Session["FileOfSSN"] = HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpSSN.Set("name", 1);
        }
        else
            HpSSN.ClientVisible = false;


        attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);

        if (attachManager.Count > 0)
        {
            hssnBack.ClientVisible = true;
            Session["FileOfSSNBack"] = hssnBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpSSN.Set("SSNBack", 1);
        }
        else
            hssnBack.ClientVisible = false;



        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
        if (attachManager.Count > 0)
        {
            HypLinkResident.ClientVisible = true;
            Session["FileOfResident"] = HypLinkResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpResident.Set("name", 1);

        }
        #endregion
    }

    protected void FillFormRequest(int MReId)
    {
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();

        ReqManager.FindByCode(MReId);
        if (ReqManager.Count <= 0)
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجو آمده است");
            return;
        }
        Boolean IsTemp = false;
        if (ReqManager[0]["IsMeTemp"].ToString() == "True")
            IsTemp = true;
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["TaskName"]))
            lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager[0]["TaskName"].ToString();
        else
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        #region اطلاعات پایه
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
            txtMeNo.Text = ReqManager[0]["MeNo"].ToString();
        txtFirstName.Text = ReqManager[0]["FirstName"].ToString();
        txtLastName.Text = ReqManager[0]["LastName"].ToString();
        txtFirstNameEn.Text = ReqManager[0]["FirstNameEn"].ToString();
        txtLastNameEn.Text = ReqManager[0]["LastNameEn"].ToString();
        txtMobileNo.Text = ReqManager[0]["MobileNo"].ToString();
        txtHomeAdr.Text = ReqManager[0]["HomeAdr"].ToString();
        txtHomePO.Text = ReqManager[0]["HomePO"].ToString();
        txtWorkAdr.Text = ReqManager[0]["WorkAdr"].ToString();
        txtWorkPO.Text = ReqManager[0]["WorkPO"].ToString();
        txtWebsite.Text = ReqManager[0]["Website"].ToString();
        txtEmail.Text = ReqManager[0]["Email"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BankAccNo"]))
            txtBankAccNo.Text = ReqManager[0]["BankAccNo"].ToString();
        txtBirhtPlace.Text = ReqManager[0]["BirthPlace"].ToString();
        txtBirthDate.Text = ReqManager[0]["BirhtDate"].ToString();
        txtIdNo.Text = ReqManager[0]["IdNo"].ToString();
        txtSSN.Text = ReqManager[0]["SSN"].ToString();
        txtIssuePlace.Text = ReqManager[0]["IssuePlace"].ToString();
        txtFatherName.Text = ReqManager[0]["FatherName"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ArchitectorCode"]))
            txtArchitectorCode.Text = ReqManager[0]["ArchitectorCode"].ToString();
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

        string htel = ReqManager[0]["HomeTel"].ToString();
        if (ReqManager[0]["HomeTel"].ToString() != "")
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
        if (ReqManager[0]["WorkTel"].ToString() != "")
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
        if (ReqManager[0]["FaxNo"].ToString() != "")
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
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["CitId"]))
        {
            drdCitId.DataBind();
            drdCitId.SelectedIndex = drdCitId.Items.FindByValue(ReqManager[0]["CitId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["AgentId"]))
        {
            drdAgent.DataBind();
            drdAgent.SelectedIndex = drdAgent.Items.FindByValue(ReqManager[0]["AgentId"].ToString()).Index;
        }

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MarId"]))
        {
            drdMarId.DataBind();
            drdMarId.SelectedIndex = drdMarId.Items.FindByValue(ReqManager[0]["MarId"].ToString()).Index;
        }
        #endregion
        #region    کمسیون ها
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
        #endregion

        #region جنسیت و سربازی
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SexId"]))
        {
            drdSexId.DataBind();
            drdSexId.SelectedIndex = drdSexId.Items.FindByValue(ReqManager[0]["SexId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SoId"]))
        {
            drdSoId.DataBind();
            drdSoId.SelectedIndex = drdSoId.Items.FindByValue(ReqManager[0]["SoId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MilitaryCommitment"]))
        {
            chbSoLdire.Checked = Convert.ToBoolean(ReqManager[0]["MilitaryCommitment"]);
        }
        //این خطوط اضافی است پایین تر تکرار شده همانند پرتال کارمندان حذف شود
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
            Session["FileOfSol"] = HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpSol.Set("name", 1);
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
            Session["FileOfSolBack"] = HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpSol.Set("SolBack", 1);
        }
        else
            HpSoldierBack.ClientVisible = false;
        #endregion
        #region تصویر امضا و تصویر پرسنلی
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ImageUrl"]))
        {
            Session["MeImgUpload"] = imgMember.ImageUrl = ReqManager[0]["ImageUrl"].ToString();
            ViewState["Img"] = ReqManager[0]["ImageUrl"].ToString();
            HDFlpMember.Set("name", 1);
        }

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SignUrl"]))
        {
            ImgSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();
            HDFlpSign.Set("name", 1);
        }
        #endregion
        #region فایل پیوست
        if (!IsTemp)
        {
            AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        }
        else
        {
            AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        }
        dtOfImg = (DataTable)Session["TblOfImg9"];
        for (int i = 0; i < attachManager.Count; i++)
        {
            DataRow dr = dtOfImg.NewRow();
            dr[0] = attachManager[i]["FilePath"].ToString();
            dr[1] = attachManager[i]["FilePath"].ToString();
            dr[5] = attachManager[i]["Description"].ToString();

            string fileName = Path.GetFileName(attachManager[0]["FilePath"].ToString());
            dr[2] = fileName;
            dr[3] = 1;
            dr[4] = attachManager[i][0];
            dtOfImg.Rows.Add(dr);
        }
        dtOfImg.AcceptChanges();
        BindAspxGridFlp(dtOfImg);
        #endregion
        #region تصاویر
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
            HpIdNo.ClientVisible = true;
            Session["FileOfIdNo"] = HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
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
            HIdNoP2.ClientVisible = true;
            Session["FileOfIdNoP2"] = HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
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
            HIdNoPDes.ClientVisible = true;
            Session["FileOfIdNoPDes"] = HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
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
            HpSSN.ClientVisible = true;
            Session["FileOfSSN"] = HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
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
            hssnBack.ClientVisible = true;
            Session["FileOfSSNBack"] = hssnBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpSSN.Set("SSNBack", 1);
        }
        else
            hssnBack.ClientVisible = false;


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
            HypLinkResident.ClientVisible = true;
            Session["FileOfResident"] = HypLinkResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
            HDFlpResident.Set("name", 1);

        }
        #endregion

        SetRoundPanelHeaderByReq(Convert.ToInt32(ReqManager[0]["IsCreated"]), _PageMode);
        FillTransfer(MReId);
    }

    protected void FillTransfer(int MReId)
    {
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        transferManager.FindByMemberId(MReId, -1);
        if (transferManager.Count <= 0)
        {
            ChEnteghali.Checked = false;
            return;
        }
        ChEnteghali.Checked = true;

        txtTDate.Text = transferManager[0]["TransferDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(transferManager[0]["TransferType"]))
        {
            CmbTransferStatus.SelectedIndex = CmbTransferStatus.Items.FindByValue(transferManager[0]["TransferType"].ToString()).Index;
        }
        else
            CmbTransferStatus.SelectedIndex = -1;
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["FileNo"]))
        {
            ChbTCheckFileNo.Checked = true;
            txtTFileNo.Text = transferManager[0]["FileNo"].ToString();
        }
        txtTransferMeNo.Text = transferManager[0]["MeNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["Body"]))
            txtTransferBodyResone.Text = transferManager[0]["Body"].ToString();
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["DocPrId"]))
        {
            ComboDocPreProvince.DataBind();
            ComboDocPreProvince.SelectedIndex = ComboDocPreProvince.Items.FindByValue(transferManager[0]["DocPrId"].ToString()).Index;
        }

        ComboTPr.DataBind();
        ComboTPr.SelectedIndex = ComboTPr.Items.FindByValue(transferManager[0]["PrId"].ToString()).Index;
        if (!Utility.IsDBNullOrNullValue(transferManager[0]["ImageUrl"]))
        {
            imgletter.ImageUrl = transferManager[0]["ImageUrl"].ToString();
            HDFlpLetter["name"] = 1;
        }

    }

    private void BindAspxGridFlp(DataTable dtOfImg)
    {
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();
    }
    #endregion

    #region Register Condition
    private bool CheckCondition(int MeId, out String Message)
    {
        if (!CheckOffice(MeId))
        {
            Message = "شما در شرکت یا دفتری عضو می باشید";
            return false;
        }
        if (!CheckActiveProject(MeId))
        {
            Message = "اطلاعات شما بعنوان مجری در یک پروژه فعال ثبت شده است";
            return false;
        }
        Message = "";
        return true;
    }

    private bool CheckActiveProject(int MeId)
    {
        TSP.DataManager.TechnicalServices.Project_ImplementerManager projectImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        projectImplementerManager.FindByMemberIdTypeId(MeId, (int)TSP.DataManager.TSMemberType.Member);
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager projectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        for (int i = 0; i < projectImplementerManager.Count; i++)
        {
            int PrjImpId = Convert.ToInt32(projectImplementerManager[i]["prjImpId"].ToString());
            projectCapacityDecrementManager.FindNotFreeByPrjImpObsDsgnId(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            if (projectCapacityDecrementManager.Count > 0)
                return false;
        }
        return true;
    }

    private bool CheckOffice(int MeId)
    {//???????????????
        //Capacity capacity = new Capacity();
        //if (capacity.CheckIsInOffice(MeId) != -1)
        //    return false;
        //if (capacity.CheckIsInEngOffice(MeId) != -1)
        //return false;
        //???????????????
        return true;
    }

    #endregion

    #region Insert-Update
    protected void Insert()
    {
        if (IsPageRefresh)
            return;

        if (Session["MeImgUpload"] == null)
        {
            ShowMessage("تصویر پرسنلی اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfIdNo"] == null)
        {
            ShowMessage("تصویر صفحه اول شناسنامه اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfSSN"] == null)
        {
            ShowMessage("تصویر روی کارت ملی اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfSol"] == null && Convert.ToInt32(drdSexId.Value) == Convert.ToInt32(TSP.DataManager.SexManager.Sex.Male))
        {
            ShowMessage("تصویر روی کارت پایان خدمت اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }


        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager MReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MemberRequestManager memberRequestManager = new TSP.DataManager.MemberRequestManager();

        trans.Add(MReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowTaskManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(TransferManager);
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming);
        if (WorkFlowTaskManager.Count != 1)
        {
            ShowMessage("خطایی در ذخیره اطلاعات رخ داده است");
            return;
        }
        memberRequestManager.FindByMemberId(_MeId, 0, -1);
        if (memberRequestManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }
        int WFTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
        try
        {
            trans.BeginSave();
            #region Insert MemberRequest          
            DataRow dr = MReqManager.NewRow();
            dr["MeId"] = _MeId;
            dr["MeNo"] = txtMeNo.Text;
            dr["FirstName"] = txtFirstName.Text;
            dr["LastName"] = txtLastName.Text;
            dr["FirstNameEn"] = txtFirstNameEn.Text;
            dr["LastNameEn"] = txtLastNameEn.Text;
            dr["MobileNo"] = txtMobileNo.Text;
            dr["HomeAdr"] = txtHomeAdr.Text;
            if (txtArchitectorCode.Text != "")
                dr["ArchitectorCode"] = txtArchitectorCode.Text;
            if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
                dr["HomeTel"] = txtHometel_cityCode.Text + "-" + txtHometel.Text;
            else if (txtHometel.Text != "")
                dr["HomeTel"] = txtHometel.Text;
            if (!string.IsNullOrEmpty(txtHomePO.Text))
                dr["HomePO"] = txtHomePO.Text;
            else
                dr["HomePO"] = DBNull.Value;
            dr["WorkAdr"] = txtWorkAdr.Text;
            if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
                dr["WorkTel"] = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
            else if (txtWorkTel.Text != "")
                dr["WorkTel"] = txtWorkTel.Text;
            if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
                dr["FaxNo"] = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
            else if (txtFaxNo.Text != "")
                dr["FaxNo"] = txtFaxNo.Text;
            if (!string.IsNullOrEmpty(txtWorkPO.Text))
                dr["WorkPO"] = txtWorkPO.Text;
            else
                dr["WorkPO"] = DBNull.Value;

            if (drdSexId.Value != null)
                dr["SexId"] = drdSexId.Value;
            else
                dr["SexId"] = DBNull.Value;

            if (drdMarId.Value != null)
                dr["MarId"] = drdMarId.Value;
            else
                dr["MarId"] = DBNull.Value;

            if (drdSoId.Value != null)
                dr["SoId"] = drdSoId.Value;
            else
                dr["SoId"] = DBNull.Value;

            dr["MilitaryCommitment"] = chbSoLdire.Checked;

            if (drdCitId.Value != null)
                dr["CitId"] = drdCitId.Value;
            else
                dr["CitId"] = DBNull.Value;

            if (drdAgent.Value != null)
                dr["AgentId"] = drdAgent.Value;
            else
                dr["AgentId"] = DBNull.Value;

            dr["Website"] = txtWebsite.Text;
            dr["Email"] = txtEmail.Text;
            dr["RequestDesc"] = txtDesc.Text;
            dr["IsConfirm"] = (int)TSP.DataManager.MemberConfirmType.Pending;//معلق
            dr["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
            dr["IsCreated"] = (int)TSP.DataManager.MemberRequestType.Request;
            dr["BankAccNo"] = txtBankAccNo.Text;
            dr["FatherName"] = txtFatherName.Text;
            dr["BirhtDate"] = txtBirthDate.Text;
            dr["BirthPlace"] = txtBirhtPlace.Text;
            dr["IdNo"] = txtIdNo.Text;
            dr["IssuePlace"] = txtIssuePlace.Text;
            dr["SSN"] = txtSSN.Text;
            if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
            {
                dr["NezamKardanConfirmURL"] = _MeImageKardan;
                HpKardani.NavigateUrl = _MeImageKardan;
            }
            else
                dr["NezamKardanConfirmURL"] = DBNull.Value;
            int comId = 0;
            for (int i = 0; i < chbComId.Items.Count; i++)
            {
                if (chbComId.Items[i].Selected)
                    comId = comId + int.Parse(chbComId.Items[i].Value.ToString());
            }
            if (comId > 0)
                dr["ComId"] = comId;
            else
                dr["ComId"] = 0;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["Requester"] = (int)TSP.DataManager.MembershipRequest.Member;
            dr["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberRequest);
            dr["WFCurrentTaskId"] = WFTaskId;

            if (Session["MeImgUpload"] != null)
            {
                dr["ImageUrl"] = Session["MeImgUpload"];// "~/Image/Members/Person/Request/" + _MReId.ToString() + Path.GetExtension(Session["MeImgUpload"].ToString());
            }

            if (Session["MeSignUpload"] != null)
            {
                dr["SignUrl"] = Session["MeSignUpload"];// "~/Image/Members/Sign/Request/" + _MReId.ToString() + Path.GetExtension(Session["MeSignUpload"].ToString());
            }


            MReqManager.AddRow(dr);

            if (MReqManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #endregion

            MReqManager.DataTable.AcceptChanges();
            int MReId = int.Parse(MReqManager[0]["MReId"].ToString());

            #region Attachment
            if (Session["TblOfImg9"] != null)
            {
                dtOfImg = (DataTable)Session["TblOfImg9"];

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

            if (Session["FileOfIdNo"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNo, Session["FileOfIdNo"].ToString(), _MeId.ToString());
            }
            if (Session["FileOfIdNoP2"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoP2, Session["FileOfIdNoP2"].ToString(), _MeId.ToString());
            }
            if (Session["FileOfIdNoPDes"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.IdNoPDes, Session["FileOfIdNoPDes"].ToString(), _MeId.ToString());
            }
            if (Session["FileOfSSN"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSN, Session["FileOfSSN"].ToString(), _MeId.ToString());
            }
            if (Session["FileOfSSNBack"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SSNBack, Session["FileOfSSNBack"].ToString(), _MeId.ToString());
            }

            if (Session["FileOfResident"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.ResidentDoc, Session["FileOfResident"].ToString(), _MeId.ToString());
            }

            if (Session["FileOfSol"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCard, Session["FileOfSol"].ToString(), _MeId.ToString());
            }
            if (Session["FileOfSolBack"] != null)
            {
                InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, MReId, TSP.DataManager.AttachType.SoldierCardBack, Session["FileOfSolBack"].ToString(), _MeId.ToString());
            }
            #endregion

            #region Enteghali
            if (ChEnteghali.Checked)
            {
                InsertTransferMember(TransferManager, MReId, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));
            }
            #endregion

            #region Return-Comment
            //else
            //{
            //    TransferManager.FindByMemberId(MReId, (int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //    if (TransferManager.Count == 1)
            //    {
            //        TransferManager[0].Delete();
            //        TransferManager.Save();
            //    }
            //}

            //if (ChEnteghali.Checked == true)
            //{
            //    #region Transfer
            //    TransferManager.FindByMemberId(MReId, (int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //    if (TransferManager.Count > 0)
            //    {
            //        #region Edit Transfer
            //        TransferManager[0].BeginEdit();
            //        if (ComboTPr.Value != null)
            //            TransferManager[0]["PrId"] = ComboTPr.Value;
            //        TransferManager[0]["TransferDate"] = txtTDate.Text;
            //        TransferManager[0]["MeNo"] = txtTMeNo.Text;
            //        if (ChbTCheckFileNo.Checked)
            //        {
            //            TransferManager[0]["FileNo"] = txtTFileNo.Text;
            //        }
            //        else
            //        {
            //            TransferManager[0]["FileNo"] = "";
            //            TransferManager[0]["FirstDocRegDate"] = "";
            //            TransferManager[0]["CurrentDocRegDate"] = "";
            //            TransferManager[0]["CurrentDocExpDate"] = "";
            //            TransferManager[0]["DocPrId"] = DBNull.Value;
            //        }
            //        TransferManager[0]["IsConfirmed"] = 0;
            //        TransferManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //        TransferManager[0]["ModifiedDate"] = DateTime.Now;

            //        #region editImage
            //        if (Session["LetterImg2"] != null)
            //        {
            //            TransferManager[0]["ImageUrl"] = "~/Image/Members/Transport/" + MReId.ToString() + Path.GetExtension(Session["LetterImg2"].ToString());
            //        }
            //        #endregion

            //        TransferManager[0].EndEdit();
            //        TransferManager.Save();
            //        #endregion
            //    }
            //    else
            //    {
            //        InsertTransferMember(TransferManager, MReId, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));
            //    }
            //    #endregion
            //}
            //else
            //{
            //    TransferManager.FindByMemberId(MReId, (int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //    if (TransferManager.Count == 1)
            //    {
            //        TransferManager[0].Delete();
            //        TransferManager.Save();
            //    }
            //}


            //if (IsReturn)
            //{
            //    DataRow drt = TransferManager.NewRow();
            //    if (ComboPrId.Value != null)
            //        drt["PrId"] = ComboPrId.Value;
            //    drt["TransferType"] = 3;
            //    drt["TableId"] = MReId;
            //    drt["TtId"] = TSP.DataManager.TableCodes.MemberRequest;
            //    drt["Body"] = txtTransferBodyResone.Text;
            //    drt["MeNo"] = txtTMeNo.Text;
            //    drt["FileNo"] = txtTFileNo.Text;
            //    drt["UserId"] = Utility.GetCurrentUser_UserId();
            //    drt["ModifiedDate"] = DateTime.Now;
            //    TransferManager.AddRow(drt);
            //    TransferManager.Save();

            //    txtTransferBodyResone.ClientVisible = true;
            //    ComboPrId.ClientVisible = true;
            //    ASPxLabelPr.ClientVisible = true;
            //    ASPxLabelPrDesc.ClientVisible = true;
            //    ASPxLabel16.ClientVisible = true;
            //    ASPxLabel15.ClientVisible = true;
            //    txtTFileNo.ClientVisible = true;
            //    txtTMeNo.ClientVisible = true;
            //}
            #endregion

            #region WorkFlow
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            //////if (Convert.ToInt32(comboRequestType.Value) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
            //////{
            //////    SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
            //////}
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int CurrentUserId = Utility.GetCurrentUser_UserId();
            int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId;
            String Description1 = "آغاز گردش کار درخواست تغییرات عضویت توسط شخص حقیقی";
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
            if (WorkFlowStateManager.InsertWorkFlowState(TableType, MReId, TaskId, Description1, _MeId, NmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) <= 0)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #endregion

            trans.EndSave();
            _MReId = MReId;
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
            }
            SetEditMode();
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

    private void InsertAttchment(TSP.DataManager.AttachmentsManager attachManager, int TtId, int RefTable, TSP.DataManager.AttachType AttachType, string FilePath, string FileName)
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
        attachManager.Save();
        attachManager.DataTable.AcceptChanges();
    }

    private void Edit(int MReId)
    {
        if (IsPageRefresh)
            return;
        if (Session["MeImgUpload"] == null)
        {
            ShowMessage("تصویر پرسنلی اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfIdNo"] == null)
        {
            ShowMessage("تصویر صفحه اول شناسنامه اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfSSN"] == null)
        {
            ShowMessage("تصویر روی کارت ملی اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        if (Session["FileOfSol"] == null && Convert.ToInt32(drdSexId.Value) == Convert.ToInt32(TSP.DataManager.SexManager.Sex.Male))
        {
            ShowMessage("تصویر روی کارت پایان خدمت اجباری می باشد.لطفا تصاویر را مجددا بارگذاری نمایید");
            return;
        }
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager MReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();

        trans.Add(MReqManager);
        trans.Add(attachManager);
        trans.Add(TransferManager);

        try
        {
            trans.BeginSave();

            #region Edit MemberRequest
            MReqManager.FindByCode(MReId);

            if (MReqManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                trans.CancelSave();
                return;
            }

            Boolean IsTemp = false;
            if (MReqManager[0]["IsMeTemp"].ToString() == "True")
                IsTemp = true;

            MReqManager[0].BeginEdit();
            MReqManager[0]["FirstName"] = txtFirstName.Text;
            MReqManager[0]["LastName"] = txtLastName.Text;
            MReqManager[0]["FirstNameEn"] = txtFirstNameEn.Text;
            MReqManager[0]["LastNameEn"] = txtLastNameEn.Text;
            MReqManager[0]["MobileNo"] = txtMobileNo.Text;
            MReqManager[0]["HomeAdr"] = txtHomeAdr.Text;
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
            if (txtArchitectorCode.Text != "")
                MReqManager[0]["ArchitectorCode"] = txtArchitectorCode.Text;
            MReqManager[0]["FatherName"] = txtFatherName.Text;
            MReqManager[0]["BirhtDate"] = txtBirthDate.Text;
            MReqManager[0]["BirthPlace"] = txtBirhtPlace.Text;
            MReqManager[0]["IdNo"] = txtIdNo.Text;
            MReqManager[0]["IssuePlace"] = txtIssuePlace.Text;
            MReqManager[0]["SSN"] = txtSSN.Text;
            if (!Utility.IsDBNullOrNullValue(_MeImageKardan))
            {
                MReqManager[0]["NezamKardanConfirmURL"] = _MeImageKardan;
                HpKardani.NavigateUrl = _MeImageKardan;
            }
            else
                MReqManager[0]["NezamKardanConfirmURL"] = DBNull.Value;
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

            MReqManager[0]["RequestDesc"] = txtDesc.Text;
            MReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            MReqManager[0]["ModifiedDate"] = DateTime.Now;

            if (Session["MeSignUpload"] != null)
            {
                MReqManager[0]["SignUrl"] = Session["MeSignUpload"];
            }
            if (Session["MeImgUpload"] != null)
            {
                MReqManager[0]["ImageUrl"] = Session["MeImgUpload"];
            }
            MReqManager[0].EndEdit();
            if (MReqManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #endregion

            #region If Enteghali
            if (ChEnteghali.Checked == true)
            {
                TransferManager.FindByMemberId(MReId, -1);
                if (TransferManager.Count > 0)
                {
                    EditTransferMember(TransferManager, MReId, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));
                }
                else
                {
                    InsertTransferMember(TransferManager, MReId, (TSP.DataManager.TransferMemberType)Convert.ToInt32(CmbTransferStatus.Value.ToString()));
                    #region Comment
                    //DataRow drTransfer = TransferManager.NewRow();
                    //drTransfer.BeginEdit();
                    //if (ComboTPr.Value != null)
                    //    drTransfer["PrId"] = ComboTPr.Value;
                    //drTransfer["TransferDate"] = txtTDate.Text;
                    //drTransfer["TransferType"] = 1;
                    //drTransfer["TableId"] = MReId;
                    //drTransfer["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
                    //drTransfer["Body"] = "";
                    //drTransfer["IsConfirmed"] = 0;
                    //drTransfer["MeNo"] = txtTMeNo.Text;
                    //drTransfer["FileNo"] = txtTFileNo.Text;
                    //drTransfer["UserId"] = Utility.GetCurrentUser_UserId();
                    //drTransfer["ModifiedDate"] = DateTime.Now;
                    //if (Session["LetterImg2"] != null)
                    //{
                    //    try
                    //    {
                    //        TransferManager.DataTable.AcceptChanges();
                    //        drTransfer["ImageUrl"] = Session["LetterImg2"];
                    //        chTImgEdit = true;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Utility.SaveWebsiteError(ex);
                    //        ShowMessage("خطایی در ذخیره تصویر انتقالی ایجاد شده است.");
                    //        trans.CancelSave();
                    //        return;
                    //    }

                    //}
                    //TransferManager.AddRow(drTransfer);
                    #endregion
                }
            }
            #endregion

            #region Attachments
            dtOfImg = (DataTable)Session["TblOfImg9"];
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
            //  string pathAx = Server.MapPath("~/image/Temp/");
            #region Save Images
            ////تصویر شناسنامه
            if (Session["FileOfIdNo"] != null)
            {

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfIdNo"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.IdNo, Session["FileOfIdNo"].ToString(), _MeId.ToString());

            }
            if (Session["FileOfIdNoP2"] != null)
            {

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfIdNoP2"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.IdNoP2, Session["FileOfIdNoP2"].ToString(), _MeId.ToString());

            }
            if (Session["FileOfIdNoPDes"] != null)
            {

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfIdNoPDes"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.IdNoPDes, Session["FileOfIdNoPDes"].ToString(), _MeId.ToString());

            }
            ////تصویر کارت ملی
            if (Session["FileOfSSN"] != null)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();

                    attachManager[0]["FilePath"] = Session["FileOfSSN"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.SSN, Session["FileOfSSN"].ToString(), _MeId.ToString());

            }
            if (Session["FileOfSSNBack"] != null)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();

                    attachManager[0]["FilePath"] = Session["FileOfSSNBack"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.SSNBack, Session["FileOfSSNBack"].ToString(), _MeId.ToString());

            }
            ////تصویر مدرک محل سکونت
            if (Session["FileOfResident"] != null)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfResident"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.ResidentDoc, Session["FileOfResident"].ToString(), _MeId.ToString());

            }

            ////تصویر کارت پایان خدمت
            if (Session["FileOfSol"] != null)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfSol"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.SoldierCard, Session["FileOfSol"].ToString(), _MeId.ToString());

            }
            if (Session["FileOfSolBack"] != null)
            {
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                }
                if (attachManager.Count > 0)
                {
                    attachManager[0].BeginEdit();
                    attachManager[0]["FilePath"] = Session["FileOfSolBack"];
                    attachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    attachManager[0].EndEdit();
                    attachManager.Save();
                }
                else
                    InsertAttchment(attachManager, (int)TSP.DataManager.TableCodes.MemberRequest, _MReId, TSP.DataManager.AttachType.SoldierCardBack, Session["FileOfSolBack"].ToString(), _MeId.ToString());

            }
            #endregion
            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            ////CheckColor(_MeId);
            SetEditMode();
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

    protected void InsertTransferMember(TSP.DataManager.TransferMemberManager transferManager, int MReId, TSP.DataManager.TransferMemberType TransferMemberType)
    {
        //SetEnteghaliVisible();
        DataRow drTransfer = transferManager.NewRow();
        drTransfer.BeginEdit();
        if (ComboTPr.Value != null)
            drTransfer["PrId"] = ComboTPr.Value;
        drTransfer["TransferDate"] = txtTDate.Text;
        drTransfer["TransferType"] = (int)TransferMemberType;
        drTransfer["TableId"] = MReId;
        drTransfer["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        drTransfer["Body"] = txtTransferBodyResone.Text;
        drTransfer["MeNo"] = txtTransferMeNo.Text;
        drTransfer["FileNo"] = txtTFileNo.Text;
        if (ChbTCheckFileNo.Checked)
        {
            drTransfer["FileNo"] = txtTFileNo.Text;
            if (ComboDocPreProvince.SelectedItem != null && ComboDocPreProvince.SelectedItem.Value != null)
                drTransfer["DocPrId"] = ComboDocPreProvince.SelectedItem.Value;
        }
        else
        {
            drTransfer["FileNo"] = "";
            drTransfer["FirstDocRegDate"] = "";
            drTransfer["CurrentDocRegDate"] = "";
            drTransfer["CurrentDocExpDate"] = "";
            drTransfer["DocPrId"] = DBNull.Value;
        }
        drTransfer["UserId"] = Utility.GetCurrentUser_UserId();
        drTransfer["ModifiedDate"] = DateTime.Now;

        if (Session["LetterImg2"] != null)
        {
            drTransfer["ImageUrl"] = "~/Image/Members/Transport/" + MReId.ToString() + Path.GetExtension(Session["LetterImg2"].ToString());
        }

        transferManager.AddRow(drTransfer);
        int tcnt = transferManager.Save();
        if (tcnt == 1)
        {
            imgletter.ClientVisible = true;
            imgletter.ImageUrl = transferManager[0]["ImageUrl"].ToString();
        }
    }

    protected void EditTransferMember(TSP.DataManager.TransferMemberManager TransferManager, int MReId, TSP.DataManager.TransferMemberType TransferMemberType)
    {
        TransferManager[0].BeginEdit();
        TransferManager[0]["MeNo"] = txtTransferMeNo.Text;
        if (ComboTPr.Value != null)
            TransferManager[0]["PrId"] = ComboTPr.Value;
        TransferManager[0]["TransferDate"] = txtTDate.Text;
        TransferManager[0]["TransferType"] = (int)TransferMemberType;
        TransferManager[0]["Body"] = txtTransferBodyResone.Text;
        if (ChbTCheckFileNo.Checked)
        {
            TransferManager[0]["FileNo"] = txtTFileNo.Text;
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
        TransferManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        TransferManager[0]["ModifiedDate"] = DateTime.Now;
        if (Session["LetterImg2"] != null)
        {
            TransferManager[0]["ImageUrl"] = Session["LetterImg2"];
        }
        TransferManager[0].EndEdit();

        TransferManager.Save();
    }
    #endregion

    #region Set Control Enables
    private void SetEnable(Boolean IsEnable)
    {
        RoundPanelCommissions.Disabled = RoundPanelAttachments.Disabled = !IsEnable;
        ChEnteghali.ClientEnabled = PanelEnteghali.Enabled =
          RoundPanelBaseInfo.Enabled =
           TblFile.Visible =
              flpImage.ClientVisible =
         flpSign.ClientVisible =
         flpIdNo.ClientVisible =
         flpIdNoP2.ClientVisible =
         flpIdNoPDes.ClientVisible =
         flpSSN.ClientVisible =
         flpSSNBack.ClientVisible =
         flpSoldier.ClientVisible =
         flpSoldierBack.ClientVisible =
         flpResident.ClientVisible =
         FlpTLetter.ClientVisible =
         ChkBKardani.ClientEnabled = flpKardani.ClientVisible =
         IsEnable;
    }

    /// <summary>
    /// امکان ویرایش نام و نام خانوادگی و نمایندگی و شماره حساب از پرتال اعضا وجود نداشته باشد
    /// </summary>
    void SetEnabledForMember()
    {
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        drdAgent.Enabled = false;
        txtBankAccNo.Enabled = false;
    }
    #endregion

    DataTable CreateDataTableAttachments()
    {
        DataTable dtOfImg = new DataTable();
        dtOfImg.Columns.Add("ImgUrl");
        dtOfImg.Columns.Add("TempImgUrl");
        dtOfImg.Columns.Add("fileName");
        dtOfImg.Columns.Add("Mode");
        dtOfImg.Columns.Add("Code");
        dtOfImg.Columns.Add("Description");
        dtOfImg.Columns.Add("Id");
        dtOfImg.Columns["Id"].AutoIncrement = true;
        dtOfImg.Columns["Id"].AutoIncrementSeed = 1;
        dtOfImg.Constraints.Add("PK_ID", dtOfImg.Columns["Id"], true);
        return dtOfImg;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        //this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}


