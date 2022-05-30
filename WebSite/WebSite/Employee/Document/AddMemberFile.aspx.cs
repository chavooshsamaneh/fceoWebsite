using DevExpress.Web;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;

public partial class Employee_Document_AddMemberFile : System.Web.UI.Page
{
    DataTable dtMemberFileMajor = null;
    #region Perperties
    int _MeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldDocMemberFile["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]));
            }
        }
        set
        {
            HiddenFieldDocMemberFile["MeId"] = value;
        }
    }

    int _MfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldDocMemberFile["MfId"]);
        }
        set
        {
            HiddenFieldDocMemberFile["MfId"] = value;
        }
    }

    string _PageMode
    {
        get
        {
            return HiddenFieldDocMemberFile["PageMode"].ToString();
        }
        set
        {
            HiddenFieldDocMemberFile["PageMode"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MeId"] == null || Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("MemberFile.aspx");
            }
            OdbProvince.FilterParameters[0].DefaultValue = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();
            #region Major's DataTable
            Session["TestMemberFileMajor"] = null;

            if (Session["TestMemberFileMajor"] == null)
            {
                CreateMajorDataTable();
            }
            else
                dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

            GridViewMajor.DataSource = dtMemberFileMajor;

            GridViewMajor.DataBind();
            #endregion

            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            TSP.DataManager.Permission Detailper = TSP.DataManager.DocMemberFileMajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnAddMajor.Enabled = Detailper.CanNew;

            Session["AccountingManager"] =
            Session["FishFileURL"] =
                     Session["HseFileURL"] =
            Session["ImgOldDocFrontURL"] =
            Session["ImgOldDocBackURL"] =
            Session["ImgTaxOfficeLetter"] =
            Session["ACCFileURL"] = Session["ImgJooshPeriod"] = null;
            Session["AccountingManager"] = CreateAccountingManager();

            SetKeys();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;

            SetLabelRegEnter();
            SetAccountingFilterExpression();
        }
        BindAccountingGrid();
        KeepPageState();
        if ((_MeId != null && _MfId != null) && (_MeId != -1 && _MfId != -1))
            CheckColor(_MfId, _MeId);
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnAddMajor"] != null)
            this.btnAddMajor.Enabled = (bool)this.ViewState["btnAddMajor"];

        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
        {
            hpImgFrontOldDoc.ImageUrl = Session["ImgOldDocFrontURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
        {
            hpImgBackOldDoc.ImageUrl = Session["ImgOldDocBackURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
        {
            hpImgTaxOfficeLetter.ImageUrl = Session["ImgTaxOfficeLetter"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
        {
            hpImgJooshPeriod.ImageUrl = Session["ImgJooshPeriod"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
        {
            hpImgHse.ImageUrl = Session["HseFileURL"].ToString();
        }
        //if (!Utility.IsDBNullOrNullValue(Session["FishFileURL"]))
        //    ImageFish.ImageUrl = Session["FishFileURL"].ToString();

        //if (!Utility.IsDBNullOrNullValue(Session["ACCFileURL"]))
        //    ImageAcc.NavigateUrl = ImageAcc.ImageUrl = Session["ACCFileURL"].ToString();

    }

    //***************************************Buttons******************************************************************
    protected void BtnNew_Click(object sender, EventArgs e)
    {

        ClearControls();
        SetNewModeKeys();
        EnableControls();
        CheckWorkFlowPermission();

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string StartDate = txtRegDate.Text;
        Utility.Date objDate = new Utility.Date(txtExpDate.Text);
        string result = objDate.AddMonths(-12);
        int IsDocExp = string.Compare(StartDate, result);
        if (IsDocExp > 0)
        {
            ShowMessage("حداقل زمان اعتبار پروانه یک سال می باشد.تاریخ های اعتبار را تصحیح نمایید.");
            return;
        }
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (_MfId == null && _PageMode != "New")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (_PageMode != "New")
        {
            if (CheckMembershipInActiveMajor()) return;
        }

        if (_PageMode == "New")
        {
            InsertMemberFile();
        }
        else if (_PageMode == "Edit")
        {
            Edit(_MfId);
        }
        else if (_PageMode == "ReDuplicate")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate);
        }
        else if (_PageMode == "Revival")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.Revival);
        }
        else if (_PageMode == "Change")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.Change);
        }
        else if (_PageMode == "UpGrade")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.UpGrade);
        }
        else if (_PageMode == "Qualification")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.Qualification);

        }
        else if (_PageMode == "Reissues")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.Reissues);
        }
        else if (_PageMode == "TransferedMemberRequest")
        {
            InsertNewRequest(_MfId, TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFile.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFile.aspx");
        }
    }

    protected void btnAddMajor_Click(object sender, EventArgs e)
    {
        string Warning = "";
        if (Session["TestMemberFileMajor"] == null)
        {
            // Session["TestMemberFileMajor"] = CreatedtMeFileMajor();
            CreateMajorDataTable();
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

        #region Check Conditions
        if (cmbMajor.SelectedIndex < 0)
        {
            ShowMessage("مدرک تحصیلی فرد انتخاب نشده است");
            return;
        }
        if (cmbMajorType.SelectedIndex < 0)
        {
            ShowMessage("رشته موضوع پروانه انتخاب نشده است");
            return;
        }
        for (int i = 0; i < dtMemberFileMajor.Rows.Count; i++)
        {
            if (dtMemberFileMajor.Rows[i].RowState != DataRowState.Deleted)
            {
                if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsInActived"]) != 1)
                {
                    if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["MlId"]) == Convert.ToInt32(cmbMajor.SelectedItem.Value))
                        && (Convert.ToInt32(dtMemberFileMajor.Rows[i]["FMjId"]) == Convert.ToInt32(cmbFileMajor.SelectedItem.Value)))
                    {
                        ShowMessage("این مدرک تحصیلی قبلا ثبت شده است");
                        return;
                    }

                    if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsMaster"]) == 1 && cmbMajorType.SelectedIndex == 0)
                    {
                        ShowMessage("پیش از این رشته موضوع پروانه انتخاب شده است" + "<br>" + "جهت تغییر رشته موضوع پروانه، ابتدا رشته موضوع پروانه قبلی را حذف و سپس این رشته را به عنوان موضوع پروانه انتخاب نمایید");
                        return;
                    }

                    if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["FMjId"]) == Convert.ToInt32(cmbFileMajor.SelectedItem.Value)))
                    {
                        Warning = "هشدار: پیش از این رشته مدرک انتخاب شده در لیست رشته ها ثبت شده است";
                    }

                    //if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsPrinted"]) == 1) && Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsMaster"]) == 0 && chkIsPrinted.Checked)
                    //{
                    //    ShowMessage("به غیر از مدرک موضوع پروانه ، تنها یک مدرک را می توان برای چاپ مقطع بر روی گواهینامه انتخاب نمود");
                    //    return;
                    //}
                }
            }
        }
        #endregion

        DataRow dr = dtMemberFileMajor.NewRow();

        try
        {
            dr["MlName"] = cmbMajor.SelectedItem.Text.ToString();
            dr["MlId"] = cmbMajor.SelectedItem.Value.ToString();
            dr["FMjName"] = cmbFileMajor.SelectedItem.Text.ToString();
            dr["FMjId"] = cmbFileMajor.SelectedItem.Value.ToString();
            dr["IsInActived"] = 0;
            dr["InActives"] = "فعال";
            dr["UnName"] = txtUnivercity.Text;
            dr["UnCount"] = txtUniCountry.Text;
            dr["UnEndDate"] = txtUNiEndDate.Text;
            dr["UnGrade"] = txtUniGrade.Text;
            if ((cmbMajorType.SelectedIndex == 1) && (chkIsPrinted.Checked))
            {
                dr["IsPrinted"] = 1;
                dr["IsPrintedName"] = "چاپ مقطع بر روی گواهینامه";
            }
            else
                dr["IsPrinted"] = 0;

            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            MajorManager.FindByCode(int.Parse(cmbFileMajor.SelectedItem.Value.ToString()));
            if (MajorManager.Count <= 0)
            {
                ShowMessage("خطایی در اضافه کردن رخ داده است");
                return;
            }
            dr["MjCode"] = MajorManager[0]["MjCode"].ToString();

            if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
            {
                dr["IsPrinted"] = 1;
                dr["IsPrintedName"] = "چاپ مقطع بر روی گواهینامه";
                TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
                TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();

                #region Check Licence Conditions
                MemberLicenceManager.FindByCode(int.Parse(cmbMajor.SelectedItem.Value.ToString()));
                if (MemberLicenceManager.Count <= 0)
                {
                    ShowMessage("خطایی در اضافه کردن رخ داده است");
                    return;
                }
                //dr["MjCode"] = MemberLicenceManager[0]["MjCode"].ToString();
                string EndDate = MemberLicenceManager[0]["EndDate"].ToString();
                LicenceManager.FindByCode(int.Parse(MemberLicenceManager[0]["LiId"].ToString()));
                if (LicenceManager.Count <= 0)
                {
                    ShowMessage("خطایی در اضافه کردن رخ داده است");
                    return;
                }
                if (Utility.IsDBNullOrNullValue(LicenceManager[0]["JobDurationNeddForMeFile"]))
                {
                    ShowMessage("مقطع مدرک تحصیلی انتخاب شده جهت پروانه اشتغال دارای اعتبار نمی باشد");
                    return;
                }

                int JobDurationNeedForMeFile = int.Parse(LicenceManager[0]["JobDurationNeddForMeFile"].ToString());
                PersianCalendar FC = new PersianCalendar();
                DateTime EndDateMiladi = Utility.Date.ShamsiToMiladi(int.Parse(EndDate.Substring(0, 4)), int.Parse(EndDate.Substring(5, 2)), int.Parse(EndDate.Substring(8, 2)));
                DateTime DtAddYear = FC.AddYears(EndDateMiladi, JobDurationNeedForMeFile);
                System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
                string AcceptedJobDuration = PDate.GetYear(DtAddYear) + "/" + PDate.GetMonth(DtAddYear).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYear).ToString().PadLeft(2, '0');
                int IsDateAccepted = string.Compare(Utility.GetDateOfToday(), AcceptedJobDuration);
                if (IsDateAccepted < 0)
                {
                    ShowMessage("رشته انتخاب شده نمی تواند رشته موضوع پروانه باشد.برای صدور پروانه بایستی" + JobDurationNeedForMeFile.ToString() + " سال از تاریخ فارغ التحصیلی رشته موضوع پروانه گذشته باشد.");
                    if (Utility.IsJobDurationNeedForMeFileChecked())
                        return;
                }
                #endregion

                dr["MjType"] = "می باشد";
                dr["IsMaster"] = 1;
                int MajorCount = dtMemberFileMajor.Rows.Count;
                for (int i = 0; i <= MajorCount - 1; i++)
                {
                    if (dtMemberFileMajor.Rows[i].RowState != DataRowState.Deleted)
                    {
                        dtMemberFileMajor.Rows[i].BeginEdit();

                        dtMemberFileMajor.Rows[i]["MjType"] = "نمی باشد";
                        dtMemberFileMajor.Rows[i]["IsMaster"] = 0;

                        dtMemberFileMajor.Rows[i].EndEdit();
                    }
                }
            }
            else
            {
                dr["MjType"] = "نمی باشد";
                dr["IsMaster"] = 0;
            }



            dtMemberFileMajor.Rows.Add(dr);
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
            cmbMajor.SelectedIndex = -1;
            cmbMajorType.SelectedIndex = 0;
            if (!string.IsNullOrWhiteSpace(Warning))
                ShowMessage(Warning);
            KeepPageState();
            txtUniCountry.Text = "";
            txtUNiEndDate.Text = "";
            txtUniGrade.Text = "";
            txtUnivercity.Text = "";
            txtMfNoSuggested.Text = "";
            chkIsPrinted.Checked = false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در اضافه کردن رخ داده است");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (_MfId == null)
        {
            ShowMessage("امکان ویرایش اطلاعات وجود ندارد.");
            return;
        }
        if (!CheckPermitionForEdit(_MfId))
        {
            ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.");
            return;
        }
        if (CheckCanEditFishForEdit(_MfId))
        {
            PanelAccountingInserting.Visible = true;
            GridViewAccounting.Columns["Delete"].Visible = true;
        }
        else
        {
            PanelAccountingInserting.Visible = false;
            GridViewAccounting.Columns["Delete"].Visible = false;
        }
        SetEditModeKeys();
    }
    //*********************************************************************************************************
    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberConfirmJobHistory.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Accounting":
                Response.Redirect("DocumentAccounting.aspx?MFId=" + Utility.EncryptQS(_MfId.ToString()) + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;


        }
    }

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
        int MReId = -2;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.ClearBeforeFill = true;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        #region Clear Controls
        if (Session["TestMemberFileMajor"] != null)
        {
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            dtMemberFileMajor.Rows.Clear();
            Session["TestMemberFileMajor"] = dtMemberFileMajor;
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
        }
        else
        {
            //Session["TestMemberFileMajor"] = CreatedtMeFileMajor();
            // dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            CreateMajorDataTable();
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
        }

        cmbIsTemporary.SelectedIndex = cmbIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);

        //////ResetComissionControls();
        _MeId = -1;
        lblFileNo.Text = "";
        lblMeLastName.Text = "";
        lblMeName.Text = "";
        lblMFNo.Text = "";
        lblPreMeNo.Text = "";
        lblPreProvince.Text = "";
        txtSerialNo.Text = "";
        lblTransferDate.Text = "";

        txtDescription.Text = "";
        txtUniCountry.Text = "";
        txtUNiEndDate.Text = "";
        txtUniGrade.Text = "";
        txtUnivercity.Text = "";

        TblTransfer.Visible = false;
        SetTransferTypeVisible(false);
        ObjdsMemberLicence.SelectParameters["MeId"].DefaultValue = "-2";
        ObjdsMemberLicenceTitle.SelectParameters["MeId"].DefaultValue = "-2";
        cmbMajor.DataBind();
        cmbMajor.Text = "";
        cmbFileMajor.Text = "";
        ImgMember.NavigateUrl = ImgMember.ImageUrl = "";
        txtMfNoSuggested.Text = "";
        HyperLinkTransfer.NavigateUrl = "";
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        #endregion
        if (string.IsNullOrEmpty(txtMeId.Text.Trim()))
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotAccepted));
            return;
        }
        MemberManager.FindByCode(int.Parse(txtMeId.Text.Trim()));
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(int.Parse(txtMeId.Text.Trim()), ref Msg))
        {
            ShowMessage(Msg);
            return;
        }

        #region Check Conditions
        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
            return;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotAccepted));
            return;
        }

        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(int.Parse(txtMeId.Text.Trim()), (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, -1);
        if (dtMeFile.Rows.Count > 0)
        {
            ShowMessage("پیش از این برای عضو انتخاب شده درخواست پروانه اشتغال ثبت شده است.");
            return;
        }
        #endregion
        #region Fill Member Info
        _MeId = Convert.ToInt32(txtMeId.Text.Trim());
        ObjdsMemberLicence.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        ObjdsMemberLicenceTitle.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        cmbFileMajor.DataBind();
        cmbMajor.SelectedIndex = 0;
        cmbMajor_SelectedIndexChanged(this, new EventArgs());
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            ImgMember.NavigateUrl = ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
        else
            ImgMember.NavigateUrl = ImgMember.ImageUrl = "~/Images/Person.png";

        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
        if (attachManager.Count > 0)
        {
            HpIdNo.NavigateUrl = HpIdNo.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        else
        {
            HpIdNo.NavigateUrl = HpIdNo.ImageUrl = "~/Images/noimage.gif";
            HpIdNo.ForeColor = System.Drawing.Color.Red;
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
        if (attachManager.Count > 0)
        {
            HpSSN.NavigateUrl = HpSSN.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        else
        {
            HpSSN.NavigateUrl = HpSSN.ImageUrl = "~/Images/noimage.gif";
            HpSSN.ForeColor = System.Drawing.Color.Red;
        }
        if (MemberManager[0]["SexId"].ToString() == "2")
        {
            lblSoldire.ClientVisible = true;
            HpSoldire.ClientVisible = true;
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
            if (attachManager.Count > 0)
            {
                lblSoldire.ClientVisible = true;
                HpSoldire.ClientVisible = true;
                HpSoldire.NavigateUrl = HpSoldire.ImageUrl = attachManager[0]["FilePath"].ToString();
            }
            else
            {
                lblSoldire.ClientVisible = true;
                HpSoldire.ClientVisible = true;
                HpSoldire.NavigateUrl = HpSoldire.ImageUrl = "~/Images/noimage.gif";

            }
        }
        else
        {
            lblSoldire.ClientVisible = false;
            HpSoldire.ClientVisible = false;
        }


        //  TransferMemberManager.FindByMemberId(int.Parse(txtMeId.Text.Trim()));
        TransferMemberManager.FindByMemberId(int.Parse(txtMeId.Text.Trim()), TSP.DataManager.TransferMemberType.AllTypes, 1);
        if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
        {
            TblTransfer.Visible = true;
            SetTransferTypeVisible(true);
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferTypeName"]))
                txtTransferStatus.Text = TransferMemberManager[0]["TransferTypeName"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
                lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferDate"]))
                lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                lblPreProvince.Text = TransferMemberManager[0]["PrName"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FirstDocRegDate"]))
                txtFirstDocRegDate.Text = TransferMemberManager[0]["FirstDocRegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocRegDate"]))
                txtCurrentDocRegDate.Text = TransferMemberManager[0]["CurrentDocRegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocExpDate"]))
                txtCurrentDocExpDate.Text = TransferMemberManager[0]["CurrentDocExpDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
            {
                ComboDocPr.DataBind();
                ComboDocPr.SelectedIndex = ComboDocPr.Items.FindByValue(TransferMemberManager[0]["DocPrId"].ToString()).Index;
            }

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["ImageUrl"]))
                HyperLinkTransfer.NavigateUrl = TransferMemberManager[0]["ImageUrl"].ToString();
        }
        else
        {
            TblTransfer.Visible = false;
            SetTransferTypeVisible(false);
        }
        #endregion
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = true;
    }

    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        cmbMajor.DataBind();
        string MjId = "";
        if (cmbMajor.SelectedItem != null)
        {
            if (cmbMajor.Items.Count > 1)
                cmbMajorType.ClientEnabled = true;
            else
                cmbMajorType.ClientEnabled = false;
            int MLId = int.Parse(cmbMajor.SelectedItem.Value.ToString());
            MemberLicenceManager.FindByCode(MLId);
            if (MemberLicenceManager.Count == 1)
            {
                MjId = MemberLicenceManager[0]["MjId"].ToString();
                cmbFileMajor.SelectedIndex = 0;
            }
        }
        ObjdsMajor.SelectParameters["MjId"].DefaultValue = MjId;
        cmbFileMajor.DataBind();
        if (!string.IsNullOrWhiteSpace(MjId) && cmbFileMajor.Items != null)
        {
            cmbFileMajor.SelectedIndex = cmbFileMajor.Items.FindByValue(MjId).Index;

            if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
                CreateAndSetSuggestedMFNo(Convert.ToInt32(MjId), Convert.ToInt32(txtMeId.Text));
            else
            {
                txtMfNoSuggested.Text = "- - -";
            }
        }
        FillMeLicenceInfo();
    }

    protected void cmbFileMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbFileMajor.Items != null)
        {
            string MjId = cmbFileMajor.SelectedItem.Value.ToString();
            cmbFileMajor.SelectedIndex = cmbFileMajor.Items.FindByValue(MjId).Index;

            if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
                CreateAndSetSuggestedMFNo(Convert.ToInt32(MjId), Convert.ToInt32(txtMeId.Text));
            else
            {
                txtMfNoSuggested.Text = "- - -";
            }
        }
    }

    //*****************در صورتی که بخواهیم از قسمت کمسیون هم ازی رشته استفاده کنیم رویداده به کنترل اضافه گردد*****************************************
    protected void cmbFileMajor_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int MlId = -1;
        int MemberMjId = -1;
        int MeFileMjId = -1;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        if (cmbMajor.Value != null)
            MlId = Convert.ToInt32(cmbMajor.Value);
        else
            return;
        MemberLicenceManager.FindByCode(MlId);
        if (MemberLicenceManager.Count != 1 || Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MjId"]))
            return;
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["CounName"]))
            txtUniCountry.Text = MemberLicenceManager[0]["CounName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["EndDate"]))
            txtUNiEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["Avg"]))
            txtUniGrade.Text = MemberLicenceManager[0]["Avg"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
            txtUnivercity.Text = MemberLicenceManager[0]["UnName"].ToString();

        MemberMjId = Convert.ToInt32(MemberLicenceManager[0]["MjId"]);
        if (cmbFileMajor.Value != null)
            MeFileMjId = Convert.ToInt32(cmbFileMajor.Value);
    }
    //***************************************Grids******************************************************************
    protected void GridViewMajor_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
        GridViewMajor.DataBind();

        int Id = -1;
        if (GridViewMajor.FocusedRowIndex > -1)
        {
            Id = GridViewMajor.FocusedRowIndex;
        }
        if (Id == -1)
        {
            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
            return;

        }
        else
        {
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            if ((_PageMode == "Edit" || _PageMode == "New") && (Utility.IsDBNullOrNullValue(dtMemberFileMajor.Rows.Find(e.Keys["Id"])["MfId"])
                || _MfId == int.Parse(dtMemberFileMajor.Rows.Find(e.Keys["Id"])["MfId"].ToString())))
            {
                dtMemberFileMajor.Rows.Find(e.Keys["Id"]).Delete();
            }
            else
            {
                if (dtMemberFileMajor.Rows.Find(e.Keys["Id"])["InActives"] != "غیرفعال")
                {
                    DataRow dr = dtMemberFileMajor.Rows.Find(e.Keys["Id"]);
                    dtMemberFileMajor.Rows.Find(e.Keys["Id"]).BeginEdit();
                    dtMemberFileMajor.Rows.Find(e.Keys["Id"])["IsInActived"] = 1;
                    dtMemberFileMajor.Rows.Find(e.Keys["Id"])["InActives"] = "غیرفعال";
                    dtMemberFileMajor.Rows.Find(e.Keys["Id"]).EndEdit();
                }
            }

            Session["TestMemberFileMajor"] = dtMemberFileMajor;
            GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
            GridViewMajor.DataBind();
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

        }
    }

    protected void GridViewMajor_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        //FilldtMajor();
        //if (_MfId != null)
        //{
        DataRow dr = GridViewMajor.GetDataRow(e.VisibleIndex);
        if (dr != null)
        {
            if (dr.RowState == DataRowState.Unchanged)
            {
                string CurretnMfId = e.GetValue("MfId").ToString();
                if (!string.IsNullOrWhiteSpace(CurretnMfId) && _MfId == Convert.ToInt32(CurretnMfId))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
            if (dr.RowState == DataRowState.Added)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
        //}
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
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
        if (e.RowType != GridViewRowType.Data)
            return;

        if (_PageMode == "View")
        {
            if (e.Row.Cells.Count > 5)
                e.Row.Cells[4].Controls[0].Visible = false;
        }

        if (_MfId != null)
        {
            DataRow dr = GridViewAccounting.GetDataRow(e.VisibleIndex);
            if (dr != null)
            {
                if (dr.RowState == DataRowState.Unchanged)
                {
                    string CurretnMfId = e.GetValue("TableTypeId").ToString();
                    if (_MfId == Convert.ToInt32(CurretnMfId))
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
                if (dr.RowState == DataRowState.Added)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
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
            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            AccountingManager.FindAccountingFishForMeDocument(Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"])), Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"])));
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
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
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
        dr["Status"] = ((int)TSP.DataManager.TSAccountingStatus.Payment).ToString();
        dr["StatusName"] = "پرداخت";
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
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
        {
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ريال بابت صدور/ارتقا/تمدید پروانه باید پرداخت شود.";
            HiddenFieldDocMemberFile["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
        }
    }

    private void SetAccountingFilterExpression()
    {
        ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.DocMemberFile).ToString();
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }
    #endregion

    //private DataTable CreatedtMeFileMajor()
    //{
    //    dtMemberFileMajor = new DataTable();
    //    dtMemberFileMajor.Columns.Add("Id");
    //    dtMemberFileMajor.Columns["Id"].AutoIncrement = true;
    //    dtMemberFileMajor.Columns["Id"].AutoIncrementSeed = 1;
    //    dtMemberFileMajor.Constraints.Add("PK_ID", dtMemberFileMajor.Columns["Id"], true);
    //    dtMemberFileMajor.Columns.Add("MlName");
    //    dtMemberFileMajor.Columns.Add("FMjName");
    //    dtMemberFileMajor.Columns.Add("IsMaster");
    //    dtMemberFileMajor.Columns.Add("MjType");
    //    dtMemberFileMajor.Columns.Add("MfId");
    //    dtMemberFileMajor.Columns.Add("InActives");
    //    dtMemberFileMajor.Columns.Add("IsInActived");
    //    //dtMemberFileMajor.Columns.Add("MailNo");
    //    //dtMemberFileMajor.Columns.Add("MailDate");
    //    dtMemberFileMajor.Columns.Add("IsPrinted");
    //    dtMemberFileMajor.Columns.Add("IsPrintedName");

    //    dtMemberFileMajor.Columns.Add("MlId");
    //    dtMemberFileMajor.Columns["MlId"].AutoIncrement = true;
    //    dtMemberFileMajor.Columns["MlId"].AutoIncrementSeed = 1;

    //    dtMemberFileMajor.Columns.Add("FMjId");
    //    dtMemberFileMajor.Columns["FMjId"].AutoIncrement = true;
    //    dtMemberFileMajor.Columns["FMjId"].AutoIncrementSeed = 1;

    //    return dtMemberFileMajor;
    //}

    private void CreateMajorDataTable()
    {

        dtMemberFileMajor = new DataTable();
        dtMemberFileMajor.Columns.Add("Id");
        dtMemberFileMajor.Columns["Id"].AutoIncrement = true;
        dtMemberFileMajor.Columns["Id"].AutoIncrementSeed = 1;
        dtMemberFileMajor.Constraints.Add("PK_ID", dtMemberFileMajor.Columns["Id"], true);
        dtMemberFileMajor.Columns.Add("MlName");
        dtMemberFileMajor.Columns.Add("FMjName");
        dtMemberFileMajor.Columns.Add("IsMaster");
        dtMemberFileMajor.Columns.Add("MjType");
        dtMemberFileMajor.Columns.Add("MjCode");
        dtMemberFileMajor.Columns.Add("LicenceMjCode");
        dtMemberFileMajor.Columns.Add("MfId");
        dtMemberFileMajor.Columns.Add("InActives");
        dtMemberFileMajor.Columns.Add("IsInActived");
        dtMemberFileMajor.Columns.Add("IsPrinted");
        dtMemberFileMajor.Columns.Add("IsPrintedName");

        dtMemberFileMajor.Columns.Add("UnName");
        dtMemberFileMajor.Columns.Add("UnCount");
        dtMemberFileMajor.Columns.Add("UnEndDate");
        dtMemberFileMajor.Columns.Add("UnGrade");

        dtMemberFileMajor.Columns.Add("MlId");
        dtMemberFileMajor.Columns["MlId"].AutoIncrement = true;
        dtMemberFileMajor.Columns["MlId"].AutoIncrementSeed = 1;

        dtMemberFileMajor.Columns.Add("FMjId");
        dtMemberFileMajor.Columns["FMjId"].AutoIncrement = true;
        dtMemberFileMajor.Columns["FMjId"].AutoIncrementSeed = 1;

        Session["TestMemberFileMajor"] = dtMemberFileMajor;
    }
    private void FilldtMajor()
    {
        if (Session["TestMemberFileMajor"] != null)
        {
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
        }
    }

    #region SetMod-Keys
    private void SetKeys()
    {
        try
        {
            _MfId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"].ToString()));
            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);

            ObjdsMemberLicence.SelectParameters["MeId"].DefaultValue = _MeId.ToString();
            ObjdsMemberLicenceTitle.SelectParameters["MeId"].DefaultValue = _MeId.ToString();
            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            SetMode(_PageMode);
            CheckWorkFlowPermission();
            if (_PageMode != "New")
            {
                CheckLicenceInfo();
                CheckMembershipRequst();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (PageMode != "New")
        {
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(_MfId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
            else
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
        }
        else
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        #region KardanImageVisible
        lblKardanAttach.Visible = false;
        ImageKardan.ClientVisible = false;
        HiddenFieldDocMemberFile["IsKardanObliq"] = 0;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(_MeId, 0);
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {
            lblKardanAttach.Visible = true;
            ImageKardan.ClientVisible = true;
            HiddenFieldDocMemberFile["IsKardanObliq"] = 1;
        }
        #endregion
        switch (PageMode)
        {
            case "New":
                SetNewModeKeys();
                lblWorkFlowState.Visible = false;
                break;

            case "View":
                SetViewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "ReDuplicate":
                SetReDuplicateModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate);
                break;

            case "Revival":
                SetRevivalModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.Revival);
                break;

            case "Change":

                SetChangeModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.Change);
                break;
            case "UpGrade":
                SetUpGradeModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade);
                break;

            case "Qualification":
                SetQualificationModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.Qualification);
                break;
            case "Reissues":
                SetReissuesModeKeys();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.Reissues);
                break;
            case "TransferedMemberRequest":
                TransferedMemberRequest();
                lblWorkFlowState.Visible = false;
                SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest);
                break;
        }
    }
    #endregion

    #region Set New-Edit-View Mode
    private void SetNewModeKeys()
    {
        RoundPanelMemberFile.HeaderText = "جدید";
        PanelAccountingInserting.Visible = true;
        GridViewAccounting.Columns["Delete"].Visible = true;

        txtMeId.Enabled = true;
        PanelMajor.Visible = true;
        //RoundPanelBasicInfo.Visible = false;
        TblTransfer.Visible = false;
        SetTransferTypeVisible(false);

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        MenuMemberFile.Enabled = false;
        flpFrontOldDoc.ClientVisible = true;
        flpBackOldDoc.ClientVisible = true;
        flpTaxOfficeLetter.ClientVisible = true;

        ObjdsMemberLicence.SelectParameters["MeId"].DefaultValue = "";
        ObjdsMemberLicenceTitle.SelectParameters["MeId"].DefaultValue = "";

        ClearForm();
        ClearControls();
    }

    private void SetViewModeKeys()
    {
        RoundPanelMemberFile.HeaderText = "مشاهده";
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }
        MenuMemberFile.Enabled = true;

        PanelAccountingInserting.Visible = false;
        GridViewAccounting.Columns["Delete"].Visible = false;

        FillForm(_MfId);
        DisableControl();
        SetTransferControlEnable(false);
        cmbMajorType.SelectedIndex = 0;
        KeepPageState();
        cmbMajor.SelectedIndex = 0;
        cmbMajor_SelectedIndexChanged(this, new EventArgs());

        txtMeId.Enabled = false;
        if (!CheckPermitionForEdit(_MfId))
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, _MfId);
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
    }

    private void SetEditModeKeys(Boolean InsertViewState = true)
    {
        RoundPanelMemberFile.HeaderText = "ویرایش";
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        _PageMode = "Edit";
        MenuMemberFile.Enabled = true;

        EnableControls();
        txtMeId.Enabled = false;
        FillForm(_MfId);

        cmbMajorType.SelectedIndex = 0;
        KeepPageState();
        cmbMajor.SelectedIndex = 0;
        cmbMajor_SelectedIndexChanged(this, new EventArgs());
        if (InsertViewState)
            InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, _MfId);

    }
    #endregion

    #region Set Request Mode
    /// <summary>
    /// المثنی
    /// </summary>
    private void SetReDuplicateModeKeys()
    {
        PanelAccountingInserting.Visible = true;
        //   PanelMajor.Visible = false;
        //  GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست صدور المثنی";
    }

    /// <summary>
    /// تمدید
    /// </summary>
    private void SetRevivalModeKeys()
    {
        // PanelMajor.Visible = false;
        //  GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست تمدید";
    }

    /// <summary>
    /// تغییرات
    /// </summary>
    private void SetChangeModeKeys()
    {
        //PanelMajor.Visible = false;
        //GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست تغییرات";
        SetTransferControlEnable(true);
    }

    /// <summary>
    /// تغییرات جهت اختصاص کد جدید به اعضای انتقالی
    /// </summary>
    private void TransferedMemberRequest()
    {
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست تخصیص شماره پروانه اعضای انتقالی";
        SetTransferControlEnable(true);
    }

    /// <summary>
    /// ارتقاء پایه
    /// </summary>
    private void SetUpGradeModeKeys()
    {
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست ارتقاء پایه";
        SetTransferControlEnable(true);
    }

    /// <summary>
    /// درج صلاحیت جدید
    /// </summary>
    private void SetQualificationModeKeys()
    {
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "درخواست تغییر صلاحیت";
        SetTransferControlEnable(true);

    }

    /// <summary>
    /// صدور مجدد
    /// </summary>
    private void SetReissuesModeKeys()
    {
        EnableControls();
        SetRequestsSetting();
        RoundPanelMemberFile.HeaderText = "صدور مجدد";
        txtMeId.Enabled = true;
        SetTransferControlEnable(true);
    }

    #endregion

    private void SetRequestsSetting()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            BtnNew.Enabled = btnNew2.Enabled = true;
        }
        btnEdit.Enabled = btnEdit2.Enabled = false;
        FillForm(_MfId);
        cmbMajorType.SelectedIndex = 0;
        KeepPageState();
        cmbMajor.SelectedIndex = 0;
        cmbMajor_SelectedIndexChanged(this, new EventArgs());
        RoundPanelMemberFile.HeaderText = "مشاهده";
        txtMeId.Enabled = false;
        SetTransferControlEnable(false);
        if (_PageMode != "Reissues")
            SetTransferTypeVisible(false);
        MenuMemberFile.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;

        flpFrontOldDoc.ClientVisible = true;
        flpBackOldDoc.ClientVisible = true;
        flpTaxOfficeLetter.ClientVisible = true;
    }

    #region FillForms
    private void FillForm(int MFId)
    {
        #region Define Manager
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        #endregion
        int MReId = -2;
        int MasterMjId = 0;
        ObjectDataSourceJobConfirm.SelectParameters["MfId"].DefaultValue = MFId.ToString();

        #region Fill MemberInfo
        ReqManager.FindByMemberId(_MeId, 0, 1, -1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        else
        {
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        }

        MemberManager.FindByCode(_MeId);
        if (MemberManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeId"]))
                txtMeId.Text = MemberManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                lblMeName.Text = MemberManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                ImgMember.NavigateUrl = ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            else
                ImgMember.NavigateUrl = ImgMember.ImageUrl = "~/Images/Person.png";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
            {
                ImageKardan.ImageUrl = MemberManager[0]["NezamKardanConfirmURL"].ToString();
                ImageKardan.ClientVisible = true;
            }
            else
            {
                ImageKardan.ImageUrl = "~/Images/noimage.gif";
            }
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
        if (attachManager.Count > 0)
        {
            HpIdNo.NavigateUrl = HpIdNo.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
        if (attachManager.Count > 0)
        {
            HpIdNo2.NavigateUrl = HpIdNo2.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
        if (attachManager.Count > 0)
        {
            HpIdNoDes.NavigateUrl = HpIdNoDes.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
        if (attachManager.Count > 0)
        {
            HpSSN.NavigateUrl = HpSSN.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
        if (attachManager.Count > 0)
        {
            HpSSN2.NavigateUrl = HpSSN2.ImageUrl = attachManager[0]["FilePath"].ToString();
        }
        if (MemberManager[0]["SexId"].ToString() == "2")
        {
            lblSoldire.ClientVisible = HpSoldire.ClientVisible = lblSoldire2.ClientVisible = HpSoldire2.ClientVisible = true;
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
            if (attachManager.Count > 0)
            {

                HpSoldire.NavigateUrl = HpSoldire.ImageUrl = attachManager[0]["FilePath"].ToString();
            }
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
            if (attachManager.Count > 0)
            {

                HpSoldire2.NavigateUrl = HpSoldire2.ImageUrl = attachManager[0]["FilePath"].ToString();
            }
        }
        else
        {
            lblSoldire.ClientVisible = HpSoldire.ClientVisible = lblSoldire2.ClientVisible = HpSoldire2.ClientVisible = true;
        }

        #endregion

        #region Fill TransferMember Info
        TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
        if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
        {
            TblTransfer.Visible = true;
            SetTransferTypeVisible(true);

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferTypeName"]))
                txtTransferStatus.Text = TransferMemberManager[0]["TransferTypeName"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
                lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
            lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                lblPreProvince.Text = TransferMemberManager[0]["PrName"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FirstDocRegDate"]))
                txtFirstDocRegDate.Text = TransferMemberManager[0]["FirstDocRegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocRegDate"]))
                txtCurrentDocRegDate.Text = TransferMemberManager[0]["CurrentDocRegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocExpDate"]))
                txtCurrentDocExpDate.Text = TransferMemberManager[0]["CurrentDocExpDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
            {
                ComboDocPr.DataBind();
                ComboDocPr.SelectedIndex = ComboDocPr.Items.FindByValue(TransferMemberManager[0]["DocPrId"].ToString()).Index;
            }
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["ImageUrl"]))
                HyperLinkTransfer.NavigateUrl = TransferMemberManager[0]["ImageUrl"].ToString();
        }
        else
        {
            TblTransfer.Visible = false;
            SetTransferTypeVisible(false);
        }
        #endregion

        if (_PageMode != "Reissues")
        {
            //*****Responsiblity***********************
            #region Fill Responsiblity
            DocMemberFileMajorManager.SelectMemberMasterMajor(_MeId);
            if (DocMemberFileMajorManager.Count == 1)
            {
                MasterMjId = Convert.ToInt32(DocMemberFileMajorManager[0]["MjId"]);
            }
            DataTable dtResDes = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
            if (dtResDes.Rows.Count == 1)
            {
                txtGradeDes.Text = dtResDes.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResObs = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            if (dtResObs.Rows.Count == 1)
            {
                txtGradeObs.Text = dtResObs.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResImp = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
            if (dtResImp.Rows.Count == 1)
            {
                txtGradeImp.Text = dtResImp.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResMapping = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
            if (dtResMapping.Rows.Count == 1)
            {
                txtGradeMapping.Text = dtResMapping.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResTraffic = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Traffic);
            if (dtResTraffic.Rows.Count == 1)
            {
                txtGradeTraffic.Text = dtResTraffic.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResUrbanism = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism);
            if (dtResUrbanism.Rows.Count == 1)
            {
                txtGradeUrbanism.Text = dtResUrbanism.Rows[0]["GrdName"].ToString();
            }
            #endregion

            //*****DocMeInfo***********************
            DocMemberFileManager.FindByCode(MFId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                #region Fill Image


                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ImgOldDocFrontURL"]))
                {
                    hpImgFrontOldDoc.ImageUrl = DocMemberFileManager[0]["ImgOldDocFrontURL"].ToString();

                }
                else
                {
                    hpImgFrontOldDoc.ImageUrl = "~/Images/noimage.gif";

                }

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ImgOldDocBackURL"]))
                {
                    hpImgBackOldDoc.ImageUrl = DocMemberFileManager[0]["ImgOldDocBackURL"].ToString();

                }
                else
                {
                    hpImgBackOldDoc.ImageUrl = "~/Images/noimage.gif";

                }

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaxOfficeLetterURL"]))
                {
                    hpImgTaxOfficeLetter.ImageUrl = DocMemberFileManager[0]["TaxOfficeLetterURL"].ToString();

                }
                else
                {
                    hpImgTaxOfficeLetter.ImageUrl = "~/Images/noimage.gif";

                }
                #endregion

                #region Set Document BaseInfo

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["LivertyDate"]))
                    txtLivertyDate.Text = DocMemberFileManager[0]["LivertyDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                    txtExpDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                    txtRegDate.Text = DocMemberFileManager[0]["RegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                    txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["Description"]))
                    txtDescription.Text = DocMemberFileManager[0]["Description"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["LivertyDate"]))
                    txtLivertyDate.Text = DocMemberFileManager[0]["LivertyDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
                    cmbIsTemporary.SelectedIndex = cmbIsTemporary.Items.FindByValue(DocMemberFileManager[0]["IsTemporary"].ToString()).Index;

                if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer
                    || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival)
                {
                    cmbTransferType.SelectedIndex = cmbTransferType.Items.FindByValue(DocMemberFileManager[0]["Type"].ToString()).Index;
                }
                else
                {
                    SetTransferTypeVisible(false);
                }
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["Type"]))
                {
                    SetRoundpanelRequestHeader(Convert.ToInt32(DocMemberFileManager[0]["Type"]));
                    if ((_PageMode == "View" || _PageMode == "Edit"))
                        SetRoundpanelRequestHeaderAndLabales(Convert.ToInt32(DocMemberFileManager[0]["Type"]));
                }
                try
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeTitleId"]))
                    {

                        cmbMeTitle.DataBind();
                        cmbMeTitle.SelectedIndex = cmbMeTitle.Items.FindByValue(DocMemberFileManager[0]["MeTitleId"].ToString()).Index;

                    }
                    else
                        cmbMeTitle.SelectedIndex = -1;
                }
                catch
                {

                }
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PeriodImageURL"]))
                {
                    hpImgJooshPeriod.ImageUrl = DocMemberFileManager[0]["PeriodImageURL"].ToString();

                }
                else
                {
                    hpImgJooshPeriod.ImageUrl = "~/Images/noimage.gif";

                }
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ImgHSEURL"]))
                {
                    hpImgHse.ImageUrl = DocMemberFileManager[0]["ImgHSEURL"].ToString();

                }
                else
                {
                    hpImgHse.ImageUrl = "~/Images/noimage.gif";

                }
                #endregion

                #region MemberFileMajor
                ResertMajorGrid();
                DataTable dtMajor = DocMemberFileMajorManager.SelectMemberFileById(MFId, _MeId, -1, -1);
                // dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
                int MfMjId = -1;
                for (int i = 0; i < dtMajor.Rows.Count; i++)
                {
                    #region MemberFileMajor
                    MfMjId = Convert.ToInt32(dtMajor.Rows[i]["MFMjId"]);
                    DataRow dr = dtMemberFileMajor.NewRow();
                    dr["Id"] = dtMajor.Rows[i]["MFMjId"].ToString();
                    dr["MlId"] = dtMajor.Rows[i]["MlId"];
                    dr["FMjId"] = dtMajor.Rows[i]["FMjId"].ToString();
                    //dr["MlName"] = dtMajor.Rows[i]["MlName"].ToString();
                    dr["MlName"] = dtMajor.Rows[i]["MlNameWithCode"].ToString();
                    //dr["FMjName"] = dtMajor.Rows[i]["FMjName"].ToString();
                    dr["FMjName"] = dtMajor.Rows[i]["FMjNameWithCode"].ToString();
                    dr["IsPrintedName"] = dtMajor.Rows[i]["PrintState"].ToString();
                    if (Convert.ToBoolean(dtMajor.Rows[i]["IsPrinted"]))
                        dr["IsPrinted"] = 1;
                    else
                        dr["IsPrinted"] = 0;
                    dr["MfId"] = dtMajor.Rows[i]["MfId"].ToString();

                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["FMjCode"]))
                        dr["MjCode"] = dtMajor.Rows[i]["FMjCode"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["UnName"]))
                        dr["UnName"] = dtMajor.Rows[i]["UnName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["CounName"]))
                        dr["UnCount"] = dtMajor.Rows[i]["CounName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["EndDate"]))
                        dr["UnEndDate"] = dtMajor.Rows[i]["EndDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["Avg"]))
                        dr["UnGrade"] = dtMajor.Rows[i]["Avg"].ToString();
                    int InActiveState = -1;
                    if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[i]["InActiveState"]))
                    {
                        InActiveState = Convert.ToInt32(dtMajor.Rows[i]["InActiveState"]);
                        dr["IsInActived"] = InActiveState;
                        if (InActiveState == 0)
                            dr["InActives"] = "فعال";
                        if (InActiveState == 1)
                            dr["InActives"] = "غیرفعال";
                    }

                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MasterMfMjId"]))
                    {
                        if (Convert.ToInt32(DocMemberFileManager[0]["MasterMfMjId"]) == MfMjId)
                        {
                            dr["IsMaster"] = 1;
                            dr["MjType"] = "می باشد";
                        }
                        else
                        {
                            dr["IsMaster"] = 0;
                            dr["MjType"] = "نمی باشد";
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(dtMajor.Rows[i]["IsMaster"]))
                            dr["IsMaster"] = 1;
                        else
                            dr["IsMaster"] = 0;
                        int IsMaster = Convert.ToInt32(dtMajor.Rows[i]["IsMaster"]);
                        if (IsMaster == 1)
                            dr["MjType"] = "می باشد";
                        if (IsMaster == 0)
                            dr["MjType"] = "نمی باشد";
                    }

                    dtMemberFileMajor.Rows.Add(dr);
                    #endregion
                }
                dtMemberFileMajor.AcceptChanges();
                GridViewMajor.DataSource = dtMemberFileMajor;
                GridViewMajor.DataBind();
                #endregion
            }
            FillAccountingGrid();
        }
    }

    private void FillMeLicenceInfo()
    {

        int MlId = -1;
        if (cmbMajor.Value != null)
            MlId = Convert.ToInt32(cmbMajor.Value);
        else
            return;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.FindByCode(MlId);
        if (MemberLicenceManager.Count != 1 || Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MjId"]))
            return;
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["CounName"]))
            txtUniCountry.Text = MemberLicenceManager[0]["CounName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["EndDate"]))
            txtUNiEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["Avg"]))
            txtUniGrade.Text = MemberLicenceManager[0]["Avg"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
            txtUnivercity.Text = MemberLicenceManager[0]["UnName"].ToString();
    }

    #endregion

    #region Set Control's Enable-Clear
    private void ClearForm()
    {
        cmbMajorType.SelectedIndex = 0;
        KeepPageState();
        // cmbMajor.SelectedIndex = 0;
        //  cmbMajor_SelectedIndexChanged(this, new EventArgs());
    }

    private void ResertMajorGrid()
    {
        Session["TestMemberFileMajor"] = null;
        CreateMajorDataTable();
        //  dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
        dtMemberFileMajor.Rows.Clear();
        GridViewMajor.DataSource = dtMemberFileMajor;
        GridViewMajor.DataBind();
    }

    private void ClearControls()
    {
        Session["AccountingManager"] = CreateAccountingManager();
        BindAccountingGrid();
        txtMeId.Text = "";
        ResertMajorGrid();

        txtDescription.Text = "";
        txtUniCountry.Text = "";
        txtUNiEndDate.Text = "";
        txtUniGrade.Text = "";
        txtUnivercity.Text = "";

        txtExpDate.Text = "";
        lblFileNo.Text = "";
        lblMeLastName.Text = "";
        lblMeName.Text = "";
        lblMFNo.Text = "";
        lblPreMeNo.Text = "";
        lblPreProvince.Text = "";
        txtRegDate.Text = "";
        txtSerialNo.Text = "";
        lblTransferDate.Text = "";
        lblWorkFlowState.Text = "وضعیت درخواست نامشخص";
        cmbIsTemporary.SelectedIndex = cmbIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);

        ObjdsMemberLicence.SelectParameters["MeId"].DefaultValue = "";
        ObjdsMemberLicenceTitle.SelectParameters["MeId"].DefaultValue = "";
        cmbMajor.DataBind();
        cmbMajor_SelectedIndexChanged(this, new EventArgs());
        cmbFileMajor.Text = "";
        cmbMajor.Text = "";
        cmbMeTitle.SelectedIndex = -1;
        cmbTransferType.SelectedIndex = 0;
        ImgMember.NavigateUrl = ImgMember.ImageUrl = "~/Images/Person.png";
        _PageMode = "New";
        _MfId = -1;
        txtGradeDes.Text = "";
        txtGradeImp.Text = "";
        txtGradeObs.Text = "";
        txtGradeUrbanism.Text = "";
        txtGradeTraffic.Text = "";
        txtGradeMapping.Text = "";
        txtMfNoSuggested.Text = "";
    }

    private void EnableControls()
    {
        PanelMajor.Visible =
        cmbIsTemporary.Enabled =
        txtExpDate.Enabled =
        txtRegDate.Enabled =
        txtSerialNo.Enabled =
        txtDescription.Enabled =
        cmbTransferType.Enabled =
        cmbMeTitle.Enabled =
        flpFrontOldDoc.ClientVisible =
        flpBackOldDoc.ClientVisible =
        flpTaxOfficeLetter.ClientVisible =
        GridViewMajor.Enabled =
        GridViewMajor.Columns["clmnDelete"].Visible =
        PanelMajor.Visible =
        flpJooshPeriod.ClientVisible = true;
        flpHse.ClientVisible = true;
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileMajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnAddMajor.Enabled = per.CanNew;
        this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
    }

    private void DisableControl()
    {
        cmbIsTemporary.Enabled =
        txtExpDate.Enabled =
        txtRegDate.Enabled =
        txtSerialNo.Enabled =
        txtDescription.Enabled =
        cmbTransferType.Enabled =
        GridViewMajor.Enabled =
        GridViewMajor.Columns["clmnDelete"].Visible =
        PanelMajor.Visible = false;
        flpFrontOldDoc.ClientVisible =
        flpBackOldDoc.ClientVisible =
        flpTaxOfficeLetter.ClientVisible =
         flpJooshPeriod.ClientVisible =
        flpHse.ClientVisible =
              cmbMeTitle.Enabled = false;
        this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;

    }

    private void SetTransferControlEnable(Boolean Enabled)
    {
        txtFirstDocRegDate.Enabled = Enabled;
        txtCurrentDocExpDate.Enabled = Enabled;
        txtCurrentDocRegDate.Enabled = Enabled;
        ComboDocPr.Enabled = Enabled;
    }

    private void SetTransferTypeVisible(Boolean Visible)
    {
        lblTransferType.Visible = Visible;
        cmbTransferType.Visible = Visible;
    }
    #endregion

    #region Insert-Update
    private void InsertMemberFile()
    {
        #region Check Conditions

        dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
        if (dtMemberFileMajor.DefaultView.Count == 0)
        {
            ShowMessage("رشته پروانه را انتخاب نمایید.");
            return;
        }

        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        if (AccountingManager.Count == 0)
        {
            ShowMessage("ورود اطلاعات فیش الزامی است");
            return;
        }
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString(), Utility.GetCurrentUser_AgentId());
        decimal TotalAmount = 0;
        decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        for (int i = 0; i < AccountingManager.Count; i++)
        {
            TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
        }

        if (TotalCost > TotalAmount)
        {
            ShowMessage("مجموع مبالغ واریزی کمتر از هزینه صدور/ارتقا/تمدید پروانه است");
            return;
        }
        #endregion

        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();

        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(TransferMemberManager);
        #endregion
        Boolean IsTransfer = false;
        string PreMFNo = "";
        string PrCode = "";
        PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();// ProvinceManager[0]["NezamCode"].ToString();
        if (string.IsNullOrEmpty(PrCode))
        {
            ShowMessage("کد نظام مهندسی (کد استان) مشخص نمی باشد");
            return;
        }


        try
        {
            TransactionManager.BeginSave();
            #region Insert DocMemberFile
            DataRow MemberFileRow = DocMemberFileManager.NewRow();
            MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
            MemberFileRow["MeId"] = _MeId;
            MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();
            MemberFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Employee;
            TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
            if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                #region TransferMember
                TransferMemberManager[0].BeginEdit();
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["FirstDocRegDate"] = txtFirstDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocRegDate"] = txtCurrentDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocExpDate"] = txtCurrentDocExpDate.Text;

                if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
                    TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;

                TransferMemberManager[0].EndEdit();
                if (TransferMemberManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
                    MemberFileRow["PrIdOrigin"] = Convert.ToInt32(TransferMemberManager[0]["DocPrId"]);
                MemberFileRow["MFNoOrigin"] = TransferMemberManager[0]["FileNo"].ToString();
                PreMFNo = TransferMemberManager[0]["FileNo"].ToString();
                PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                if (cmbTransferType.Value != null)
                    MemberFileRow["Type"] = Convert.ToInt32(cmbTransferType.Value);// TSP.DataManager.DocumentOfMemberRequestType.Transfer;//*****انتقالی
                IsTransfer = true;
                #endregion
            }
            else
            {
                //*****صدور
                MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New;
            }

            if (!string.IsNullOrEmpty(txtSerialNo.Text))
                MemberFileRow["SerialNo"] = txtSerialNo.Text;
            if (!string.IsNullOrEmpty(txtRegDate.Text))
                MemberFileRow["RegDate"] = txtRegDate.Text;
            if (!string.IsNullOrEmpty(txtExpDate.Text))
                MemberFileRow["ExpireDate"] = txtExpDate.Text;
            if (!Utility.IsDBNullOrNullValue(cmbIsTemporary.SelectedItem) && !Utility.IsDBNullOrNullValue(cmbIsTemporary.SelectedItem.Value))
                MemberFileRow["IsTemporary"] = cmbIsTemporary.SelectedItem.Value;
            MemberFileRow["CreateDate"] = Utility.GetDateOfToday();
            MemberFileRow["IsConfirm"] = 0;
            MemberFileRow["InActive"] = 0;
            MemberFileRow["Description"] = txtDescription.Text;
            MemberFileRow["MailDate"] = "";
            MemberFileRow["MailNo"] = "";
            MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberFileRow["ModifiedDate"] = DateTime.Now;

            if (!Utility.IsDBNullOrNullValue(Session["FishFileURL"]))
                MemberFileRow["FishAccConfirmURL"] = Session["FishFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ACCFileURL"]))
                MemberFileRow["AccConfirmURL"] = Session["ACCFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                MemberFileRow["ImgOldDocFrontURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                MemberFileRow["ImgOldDocBackURL"] = Session["ImgOldDocBackURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
                MemberFileRow["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();
            if (cmbMeTitle.SelectedItem != null && cmbMeTitle.SelectedItem.Value != null)
                MemberFileRow["MeTitleId"] = cmbMeTitle.SelectedItem.Value;
            if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
                MemberFileRow["PeriodImageURL"] = Session["ImgJooshPeriod"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
                MemberFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();

            DocMemberFileManager.AddRow(MemberFileRow);
            int cn = DocMemberFileManager.Save();
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion
            if (cn <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #region Accounting Fish
            int TableType = -1;
            TSP.DataManager.TableTypeManager TableTypeManager = new TSP.DataManager.TableTypeManager();
            TableTypeManager.FindByTtCode(TSP.DataManager.TableType.MemberFile);
            if (TableTypeManager.Count == 1)
            {
                TableType = Convert.ToInt32(TableTypeManager[0]["TtId"]);
            }

            for (int i = 0; i < AccountingManager.Count; i++)
            {
                AccountingManager[i].BeginEdit();
                AccountingManager[i]["TableType"] = TableType;
                AccountingManager[i]["TableTypeId"] = Convert.ToInt32(DocMemberFileManager[0]["MFId"]);
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();

            #endregion

            #region SaveMajors

            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

            if (dtMemberFileMajor.DefaultView.Count == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("رشته پروانه را انتخاب نمایید.");
                return;
            }
            for (int i = 0; i < dtMemberFileMajor.Rows.Count; i++)
            {
                DataRow MFMajorRow = DocMemberFileMajorManager.NewRow();
                MFMajorRow["MFId"] = DocMemberFileManager[0]["MFId"].ToString();

                MFMajorRow["MlId"] = dtMemberFileMajor.Rows[i]["MlId"].ToString();
                MFMajorRow["FMjId"] = dtMemberFileMajor.Rows[i]["FMjId"].ToString();
                MFMajorRow["IsMaster"] = int.Parse(dtMemberFileMajor.Rows[i]["IsMaster"].ToString());

                MFMajorRow["MailDate"] = "";
                MFMajorRow["MailNo"] = "";
                MFMajorRow["IsPrinted"] = int.Parse(dtMemberFileMajor.Rows[i]["IsPrinted"].ToString());

                MFMajorRow["UserId"] = Utility.GetCurrentUser_UserId();
                MFMajorRow["ModifiedDate"] = DateTime.Now;
                DocMemberFileMajorManager.AddRow(MFMajorRow);

                int cnt = DocMemberFileMajorManager.Save();
                DocMemberFileMajorManager.DataTable.AcceptChanges();
                if (cnt < 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }

                //---------update mfmjid in docmemberfile----------------------------
                if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsMaster"]) == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["MFMjId"]))
                    {
                        DocMemberFileManager[0].BeginEdit();
                        DocMemberFileManager[0]["MasterMfMjId"] = DocMemberFileMajorManager[0]["MFMjId"];
                        DocMemberFileManager[0].EndEdit();
                    }
                }
            }

            #endregion

            #region Create MfNo
            //**********MFNo=PrCode +MjCode+MFSerialNo*********
            string MjCode = "";
            string MFSerialNo = "";
            string MfNo = "";
            int MjId = -1;
            int MajorCount = dtMemberFileMajor.Rows.Count;
            for (int j = 0; j < MajorCount; j++)
            {
                int IsMaster = int.Parse(dtMemberFileMajor.Rows[j]["IsMaster"].ToString());
                if (IsMaster == 1)
                {
                    MjId = int.Parse(dtMemberFileMajor.Rows[j]["FMjId"].ToString());

                    MjCode = dtMemberFileMajor.Rows[j]["MjCode"].ToString();

                    break;
                }
            }

            int NewSerialNo = DocMemberFileManager.GenerateNewMemberFileSerialNo(_MeId, MjCode);//DocMemberFileManager[0]["MFSerialNo"].ToString();
            if (NewSerialNo <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(DocMemberFileManager.FindErrorMessage(NewSerialNo));
                return;
            }
            MFSerialNo = NewSerialNo.ToString();
            int SerialLen = MFSerialNo.Length;
            int t = 5 - SerialLen;
            for (int i = 0; i < t; i++)
            {
                MFSerialNo = "0" + MFSerialNo;
            }
            MfNo = PrCode + "-" + MjCode + "-" + MFSerialNo;
            #endregion

            #region Set MfNo
            if (DocMemberFileManager.CheckIfMfNoRepitetive(MfNo, _MeId))
            {
                TransactionManager.CancelSave();
                ShowMessage("امکان ذخیره وجود ندارد.شماره پروانه ایجاد شده تکراری می باشد.لطفا پیگیری لازم را انجام دهید.");
                return;
            }
            DocMemberFileManager[0].BeginEdit();

            if (!IsTransfer)
            {
                DocMemberFileManager[0]["MFNo"] = MfNo;
                DocMemberFileManager[0]["MFSerialNo"] = MFSerialNo;
            }
            else
                DocMemberFileManager[0]["MFNo"] = PreMFNo;
            DocMemberFileManager[0].EndEdit();
            int SaveCount = DocMemberFileManager.Save();

            if (SaveCount == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion
            int TaskId = -1;
            int TableId = int.Parse(DocMemberFileManager[0]["MfId"].ToString());
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int CurrentMeId = Utility.GetCurrentUser_MeId();
            int CurrentNmcId = FindNmcId(TaskId);
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            int StateId = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (StateId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            DocMemberFileManager[0].BeginEdit();
            DocMemberFileManager[0]["CurrentWFStateId"] = StateId;
            DocMemberFileManager[0].EndEdit();
            if (DocMemberFileManager.Save() == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            TransactionManager.EndSave();

            #region Set Controls AfterSave
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            MenuMemberFile.Enabled = true;

            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                    txtExpDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                    txtRegDate.Text = DocMemberFileManager[0]["RegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                    txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                    {
                        lblWorkFlowState.Visible = true;
                        lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                    }
                }
            }
            _PageMode = "Edit";
            _MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
            RoundPanelMemberFile.HeaderText = "ویرایش";
            ShowMessage("ذخیره انجام شد.");
            #endregion
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int MfId)
    {

        #region Check Conditions
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManagerForDel = new TSP.DataManager.TechnicalServices.AccountingManager();
        if (Utility.IsDBNullOrNullValue(AccountingManager))
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        AccountingManager.DataTable.DefaultView.RowFilter = "TableTypeId=-1 OR TableTypeId=" + MfId.ToString();

        if (CheckCanEditFish(MfId) && AccountingManager.Count == 0)
        {
            ShowMessage("ورود اطلاعات فیش الزامی است");
            AccountingManager.DataTable.DefaultView.RowFilter = "";
            return;
        }
        AccountingManager.DataTable.DefaultView.RowFilter = "";
        #endregion

        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        DocMemberFileMajorManager.ClearBeforeFill = true;
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();

        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(RequestInActivesManager);
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(AccountingManagerForDel);
        TransactionManager.Add(TransferMemberManager);
        #endregion
        Boolean IsTransfer = false;
        string PrCode = "";
        string PreMfNo = "";
        Boolean IsMfNoChanged = false;
        try
        {
            TransactionManager.BeginSave();

            TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
            if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                IsTransfer = true;
            }
            if (Session["TestMemberFileMajor"] == null)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            dtMemberFileMajor.DefaultView.RowFilter = "IsInActived=0";
            if (dtMemberFileMajor.DefaultView.Count == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("رشته پروانه را انتخاب نمایید.");
                dtMemberFileMajor.DefaultView.RowFilter = "";
                return;
            }
            dtMemberFileMajor.DefaultView.RowFilter = "";
            #region Accounting Fish
            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            if (TableType == -1)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            AccountingManagerForDel.FindByTableTypeId(MfId, TableType);
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
                if (Convert.ToInt32(AccountingManager[i]["TableTypeId"]) == -1)
                    AccountingManager[i]["TableTypeId"] = MfId;
                AccountingManager[i].EndEdit();
            }
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();

            #endregion

            #region Save DocMemberFile And MFSerialNo
            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            DocMemberFileManager[0].BeginEdit();

            if (!string.IsNullOrEmpty(txtSerialNo.Text))
                DocMemberFileManager[0]["SerialNo"] = txtSerialNo.Text;

            if (!string.IsNullOrEmpty(txtRegDate.Text))
                DocMemberFileManager[0]["RegDate"] = txtRegDate.Text;

            if (!string.IsNullOrEmpty(txtExpDate.Text))
                DocMemberFileManager[0]["ExpireDate"] = txtExpDate.Text;

            if (!Utility.IsDBNullOrNullValue(cmbIsTemporary.SelectedItem) && !Utility.IsDBNullOrNullValue(cmbIsTemporary.SelectedItem.Value))
                DocMemberFileManager[0]["IsTemporary"] = cmbIsTemporary.SelectedItem.Value;


            DocMemberFileManager[0]["MailNo"] = "";
            DocMemberFileManager[0]["MailDate"] = "";
            DocMemberFileManager[0]["Description"] = txtDescription.Text;
            DocMemberFileManager[0]["MeId"] = _MeId;
            DocMemberFileManager[0]["PrId"] = Utility.GetCurrentProvinceId();

            if (!Utility.IsDBNullOrNullValue(Session["FishFileURL"]))
                DocMemberFileManager[0]["FishAccConfirmURL"] = Session["FishFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ACCFileURL"]))
                DocMemberFileManager[0]["AccConfirmURL"] = Session["ACCFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                DocMemberFileManager[0]["ImgOldDocFrontURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                DocMemberFileManager[0]["ImgOldDocBackURL"] = Session["ImgOldDocBackURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
                DocMemberFileManager[0]["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
                DocMemberFileManager[0]["PeriodImageURL"] = Session["ImgJooshPeriod"].ToString();


            if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
                DocMemberFileManager[0]["ImgHSEURL"] = Session["HseFileURL"].ToString();

            if (cmbMeTitle.SelectedItem != null && cmbMeTitle.SelectedItem.Value != null)
                DocMemberFileManager[0]["MeTitleId"] = cmbMeTitle.SelectedItem.Value;
            #region TransferMember Info
            TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
            if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                TransferMemberManager[0].BeginEdit();
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["FirstDocRegDate"] = txtFirstDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocRegDate"] = txtCurrentDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocExpDate"] = txtCurrentDocExpDate.Text;

                if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
                    TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;

                TransferMemberManager[0].EndEdit();
                if (TransferMemberManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
                    DocMemberFileManager[0]["PrIdOrigin"] = int.Parse(TransferMemberManager[0]["DocPrId"].ToString());
                else
                    DocMemberFileManager[0]["PrIdOrigin"] = DBNull.Value;
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                {
                    DocMemberFileManager[0]["MFNoOrigin"] = TransferMemberManager[0]["FileNo"].ToString();
                    PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                    PreMfNo = TransferMemberManager[0]["FileNo"].ToString();
                }
                if (CheckIfTransferTypeInsert(Convert.ToInt32(DocMemberFileManager[0]["Type"])) && cmbTransferType.Value != null)
                    DocMemberFileManager[0]["Type"] = Convert.ToInt32(cmbTransferType.Value);
            }
            else
            {
                DocMemberFileManager[0]["PrIdOrigin"] = DBNull.Value;
            }
            #endregion

            #region Create MFNo
            //**********SerialNo=PrCode +MjCode+MFSerialNo*********
            string MfNo = "";
            string MfSerialNo = "";
            if (!IsTransfer)
            {
                ArrayList MeFileNo = new System.Collections.ArrayList();
                MeFileNo = CreateMFNo(_MeId, IsTransfer, PrCode, DocMemberFileManager);
                if (Convert.ToBoolean(MeFileNo[0]))//****IF HAS ERROR IN FINDING MFNO
                {
                    TransactionManager.CancelSave();
                    return;
                }
                MfNo = MeFileNo[1].ToString();
                MfSerialNo = MeFileNo[2].ToString();
                if (DocMemberFileManager.CheckIfMfNoRepitetive(MfNo, _MeId))
                {
                    TransactionManager.CancelSave();
                    ShowMessage("امکان ذخیره وجود ندارد.شماره پروانه ایجاد شده تکراری می باشد.لطفا پیگیری لازم را انجام دهید.");
                    return;
                }
                DocMemberFileManager[0]["MFSerialNo"] = MfSerialNo;
                if (string.Compare(DocMemberFileManager[0]["MFNo"].ToString(), MfNo) != 0)
                    IsMfNoChanged = true;
                DocMemberFileManager[0]["MFNo"] = MfNo;
            }
            else if (!string.IsNullOrWhiteSpace(PreMfNo))
            {
                if (string.Compare(DocMemberFileManager[0]["MFNo"].ToString(), PreMfNo) != 0)
                    IsMfNoChanged = true;
                DocMemberFileManager[0]["MFNo"] = PreMfNo;
            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage("شماره پروانه شخص انتقالی ثبت نشده است");
                return;
            }
            DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;

            DocMemberFileManager[0].EndEdit();
            if (DocMemberFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion

            #endregion

            if (dtMemberFileMajor.GetChanges() != null)
            {
                #region MemberFileMajor
                DataRow[] delRows = dtMemberFileMajor.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtMemberFileMajor.Select(null, null, DataViewRowState.Added);
                DataRow[] EditRows = dtMemberFileMajor.Select(null, null, DataViewRowState.ModifiedCurrent);
                #region Deleted Majors
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        DocMemberFileMajorManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        if (DocMemberFileMajorManager.Count <= 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                        DocMemberFileMajorManager[0].Delete();

                        int SaveDel = DocMemberFileMajorManager.Save();
                        DocMemberFileMajorManager.DataTable.AcceptChanges();
                        if (SaveDel < 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                    }
                }
                #endregion
                #region Edited Majors
                if (EditRows.Length > 0)
                {
                    int MfMjId = -1;
                    for (int j = 0; j < EditRows.Length; j++)
                    {
                        MfMjId = int.Parse(EditRows[j]["Id"].ToString());
                        DocMemberFileMajorManager.FindByCode(int.Parse(EditRows[j]["Id"].ToString()));
                        if (DocMemberFileMajorManager.Count <= 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                        if (Convert.ToInt32(EditRows[j]["IsInActived"]) == 1)
                            InsertInActive(MfMjId, MfId, (int)TSP.DataManager.TableCodes.DocMemberFileMajor, (int)TSP.DataManager.TableCodes.DocMemberFile, RequestInActivesManager);

                        DocMemberFileMajorManager[0].BeginEdit();
                        DocMemberFileMajorManager[0]["IsMaster"] = Convert.ToBoolean(int.Parse(EditRows[j]["IsMaster"].ToString()));
                        DocMemberFileMajorManager[0].EndEdit();
                        int SaveMFMj = DocMemberFileMajorManager.Save();
                        DocMemberFileMajorManager.DataTable.AcceptChanges();
                        if (SaveMFMj < 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                    }
                }
                #endregion
                #region Inserted Majors
                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drMemberFileMajor = DocMemberFileMajorManager.NewRow();
                        drMemberFileMajor["MFId"] = MfId;
                        drMemberFileMajor["MlId"] = insRows[i]["MlId"].ToString();
                        drMemberFileMajor["FMjId"] = insRows[i]["FMjId"].ToString();
                        drMemberFileMajor["IsMaster"] = Convert.ToBoolean(int.Parse(insRows[i]["IsMaster"].ToString()));
                        drMemberFileMajor["IsPrinted"] = Convert.ToBoolean(int.Parse(insRows[i]["IsPrinted"].ToString()));
                        drMemberFileMajor["UserId"] = Utility.GetCurrentUser_UserId();
                        drMemberFileMajor["ModifiedDate"] = DateTime.Now;
                        DocMemberFileMajorManager.AddRow(drMemberFileMajor);

                        if (DocMemberFileMajorManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                        DocMemberFileMajorManager.DataTable.AcceptChanges();
                    }
                }
                #endregion
                #endregion
                #region Update MasterMfMjId
                dtMemberFileMajor.AcceptChanges();
                DataRow drMasterMajor = FindMasterMajorFromdtMemberFileMajor();
                if (drMasterMajor == null)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("رشته موضوع پروانه نامشخص می باشد");
                    return;
                }
                DocMemberFileMajorManager.FindByMFId(-1, _MeId);
                DocMemberFileMajorManager.CurrentFilter = "FMjId=" + drMasterMajor["FMjId"].ToString() + "and MlId=" + drMasterMajor["MlId"].ToString() + "and InActiveState=0";
                if (DocMemberFileMajorManager.Count != 1)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("رشته موضوع پروانه نامشخص می باشد");
                    DocMemberFileMajorManager.CurrentFilter = "";
                    return;
                }
                int MasterMfMjId = Convert.ToInt32(DocMemberFileMajorManager[0]["MfMjId"]);
                DocMemberFileMajorManager.CurrentFilter = "";
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["MasterMfMjId"] = MasterMfMjId;
                DocMemberFileManager[0].EndEdit();
                int cn = DocMemberFileManager.Save();
                DocMemberFileManager.DataTable.AcceptChanges();
                if (cn <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
                #endregion
            }


            TransactionManager.EndSave();
            string Message = "";
            if (IsMfNoChanged)
                Message = "با ذخیره اطلاعات شماره پروانه شخص تغییر یافت.در صورتی که انجام این تغییر به اشتباه می باشد لطفا اطلاعات را مجددا بررسی و تغییرات لازم را انجام دهید.";
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete) + Message);
            dtMemberFileMajor.AcceptChanges();
            Session["TestMemberFileMajor"] = dtMemberFileMajor;
            GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
            GridViewMajor.DataBind();

            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, MfId, (int)TSP.DataManager.TableType.MemberFile, "ویرایش اطلاعات در صفحه مشخصات پروانه اشتغال ", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void InsertNewRequest(int MfId, TSP.DataManager.DocumentOfMemberRequestType DocumentOfMemberRequestType)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = null;
        if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Revival
            || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.UpGrade
            || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Qualification
            || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate
            || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Reissues)
        {
            #region CheckFish Validation
            if (Session["AccountingManager"] == null)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }
            AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
            AccountingManager.DataTable.DefaultView.RowFilter = "TableTypeId=-1";
            if (AccountingManager.Count == 0)
            {
                ShowMessage("ورود اطلاعات فیش الزامی است");
                AccountingManager.DataTable.DefaultView.RowFilter = "";
                return;
            }
            AccountingManager.DataTable.DefaultView.RowFilter = "";
            TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString(), Utility.GetCurrentUser_AgentId());
            decimal TotalAmount = 0;
            decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            for (int i = 0; i < AccountingManager.Count; i++)
            {
                TotalAmount += decimal.Parse(AccountingManager[i]["Amount"].ToString());
            }

            if (TotalCost > TotalAmount)
            {
                ShowMessage("مجموع مبالغ واریزی کمتر از هزینه صدور/ارتقا/تمدید پروانه است");
                return;
            }
            #endregion
        }
        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(RequestInActivesManager);
        TransactionManager.Add(TransferMemberManager);
        #endregion
        Boolean IsMfNoChanged = false;
        Boolean IsTransfer = false;
        string PreMfNo = "";
        string PrCode = "";
        try
        {
            TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
            if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                PreMfNo = TransferMemberManager[0]["FileNo"].ToString();
                IsTransfer = true;
            }

            if (Session["TestMemberFileMajor"] == null)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }

            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            dtMemberFileMajor.DefaultView.RowFilter = "IsInActived=0";
            if (dtMemberFileMajor.Rows.Count == 0)
            {
                ShowMessage("رشته پروانه را انتخاب نمایید.");
                dtMemberFileMajor.DefaultView.RowFilter = "";
                return;
            }
            dtMemberFileMajor.DefaultView.RowFilter = "";

            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count != 1)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return;
            }

            TransactionManager.BeginSave();
            #region TransferMember Info
            TransferMemberManager.FindByMemberId(_MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
            if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                TransferMemberManager[0].BeginEdit();
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["FirstDocRegDate"] = txtFirstDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocRegDate"] = txtCurrentDocRegDate.Text;
                if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
                    TransferMemberManager[0]["CurrentDocExpDate"] = txtCurrentDocExpDate.Text;

                if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
                    TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;
                TransferMemberManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TransferMemberManager[0]["ModifiedDate"] = DateTime.Now;
                TransferMemberManager[0].EndEdit();
                if (TransferMemberManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }

            #endregion

            #region DocMe Insert
            DataRow MeFileRow = DocMemberFileManager.NewRow();
            MeFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Employee;
            MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
            MeFileRow["MailNo"] = "";
            MeFileRow["MailDate"] = "";
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
            {
                MeFileRow["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
                _MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
            }
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFSerialNo"]))
                MeFileRow["MFSerialNo"] = DocMemberFileManager[0]["MFSerialNo"].ToString();
            MeFileRow["RegDate"] = txtRegDate.Text;
            MeFileRow["ExpireDate"] = txtExpDate.Text;
            MeFileRow["Description"] = txtDescription.Text;
            if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Reissues)
            {
                MeFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New;
                if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer)
                    MeFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer;
                if (cmbTransferType.Visible == true && cmbTransferType.Value != null)
                    MeFileRow["Type"] = Convert.ToInt32(cmbTransferType.Value);
            }
            else
                MeFileRow["Type"] = (int)DocumentOfMemberRequestType;
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                MeFileRow["PrId"] = DocMemberFileManager[0]["PrId"].ToString();
            //if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrIdOrigin"]))
            if (IsTransfer && ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
                MeFileRow["PrIdOrigin"] = ComboDocPr.SelectedItem.Value;// DocMemberFileManager[0]["PrIdOrigin"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                MeFileRow["MFNo"] = DocMemberFileManager[0]["MFNo"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNoOrigin"]))
                MeFileRow["MFNoOrigin"] = DocMemberFileManager[0]["MFNoOrigin"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["FishFileURL"]))
                MeFileRow["FishAccConfirmURL"] = Session["FishFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ACCFileURL"]))
                MeFileRow["AccConfirmURL"] = Session["ACCFileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                MeFileRow["ImgOldDocFrontURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                MeFileRow["ImgOldDocBackURL"] = Session["ImgOldDocBackURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
                MeFileRow["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();
            if (cmbMeTitle.SelectedItem != null && cmbMeTitle.SelectedItem.Value != null)
                MeFileRow["MeTitleId"] = cmbMeTitle.SelectedItem.Value;
            if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
                MeFileRow["PeriodImageURL"] = Session["ImgJooshPeriod"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
                MeFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();


            MeFileRow["CreateDate"] = Utility.GetDateOfToday();
            MeFileRow["Description"] = txtDescription.Text;
            MeFileRow["IsConfirm"] = 0;
            MeFileRow["InActive"] = 0;
            MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MeFileRow["ModifiedDate"] = DateTime.Now;

            DocMemberFileManager.AddRow(MeFileRow);
            if (DocMemberFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion
            if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Reissues)
            {
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["InActive"] = 1;
                DocMemberFileManager[0].EndEdit();
                DocMemberFileManager.Save();
                DocMemberFileManager.DataTable.AcceptChanges();
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            int TableId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"]);



            #region  WF
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int CurrentNmcId = FindNmcId(SaveInfoTaskId);
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            string Description = "شروع گردش کار درخواست پروانه اشتغال";
            int WfStartId = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStartId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion

            #region UpdateMFNo
            DocMemberFileManager.FindByCode(TableId, 0);
            if (DocMemberFileManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            DocMemberFileManager[0].BeginEdit();
            DocMemberFileManager[0]["CurrentWFStateId"] = WfStartId;
            //**********SerialNo=PrCode +MjCode+MFSerialNo*********
            string MfNo = "";
            string MfSerialNo = "";
            if (!IsTransfer)
            {

                ArrayList MeFileNo = new System.Collections.ArrayList();
                MeFileNo = CreateMFNo(_MeId, IsTransfer, PrCode, DocMemberFileManager);
                if (Convert.ToBoolean(MeFileNo[0]))//****IF HAS ERROR IN FINDING MFNO
                {
                    TransactionManager.CancelSave();
                    return;
                }
                MfNo = MeFileNo[1].ToString();
                MfSerialNo = MeFileNo[2].ToString();
                if (DocMemberFileManager.CheckIfMfNoRepitetive(MfNo, _MeId))
                {
                    TransactionManager.CancelSave();
                    ShowMessage("امکان ذخیره وجود ندارد.شماره پروانه ایجاد شده تکراری می باشد.لطفا پیگیری لازم را انجام دهید.");
                    return;
                }

                if (string.Compare(DocMemberFileManager[0]["MFNo"].ToString(), MfNo) != 0)
                    IsMfNoChanged = true;
                DocMemberFileManager[0]["MFNo"] = MfNo;
                DocMemberFileManager[0]["MFSerialNo"] = MfSerialNo;
            }
            else if (!string.IsNullOrWhiteSpace(PreMfNo))
            {
                if (string.Compare(DocMemberFileManager[0]["MFNo"].ToString(), PreMfNo) != 0)
                    IsMfNoChanged = true;
                DocMemberFileManager[0]["MFNo"] = PreMfNo;
                MfNo = PreMfNo;
            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage("شماره پروانه شخص انتقالی ثبت نشده است");
                return;
            }
            DocMemberFileManager[0].EndEdit();
            if (DocMemberFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion

            #region MemberFileMajor
            if (dtMemberFileMajor.GetChanges() != null)
            {
                DataRow[] delRows = dtMemberFileMajor.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtMemberFileMajor.Select(null, null, DataViewRowState.Added);
                DataRow[] EditRows = dtMemberFileMajor.Select(null, null, DataViewRowState.ModifiedCurrent);
                #region Deleted Majors
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        DocMemberFileMajorManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        if (DocMemberFileMajorManager.Count > 0)
                        {
                            DocMemberFileMajorManager[0].Delete();

                            int SaveDel = DocMemberFileMajorManager.Save();
                            DocMemberFileMajorManager.DataTable.AcceptChanges();
                            if (SaveDel < 0)
                            {
                                TransactionManager.CancelSave();
                                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                return;
                            }
                        }

                    }
                }
                #endregion
                #region Edited Majors
                if (EditRows.Length > 0)
                {
                    int MfMjId = -1;
                    for (int j = 0; j < EditRows.Length; j++)
                    {
                        MfMjId = int.Parse(EditRows[j]["Id"].ToString());
                        if (Convert.ToInt32(EditRows[j]["IsInActived"]) == 1)
                            InsertInActive(MfMjId, TableId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileMajor), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile), RequestInActivesManager);
                    }
                }
                #endregion
                #region Inserted Majors
                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drMemberFileMajor = DocMemberFileMajorManager.NewRow();
                        drMemberFileMajor["MFId"] = TableId;
                        drMemberFileMajor["MlId"] = insRows[i]["MlId"].ToString();
                        drMemberFileMajor["FMjId"] = insRows[i]["FMjId"].ToString();
                        //  drMemberFileMajor["IsMaster"] = Convert.ToBoolean(int.Parse(insRows[i]["IsMaster"].ToString()));
                        drMemberFileMajor["IsPrinted"] = Convert.ToBoolean(int.Parse(insRows[i]["IsPrinted"].ToString()));
                        drMemberFileMajor["UserId"] = Utility.GetCurrentUser_UserId();
                        drMemberFileMajor["ModifiedDate"] = DateTime.Now;

                        DocMemberFileMajorManager.AddRow(drMemberFileMajor);

                        int count = DocMemberFileMajorManager.Save();
                        DocMemberFileMajorManager.DataTable.AcceptChanges();
                        if (count < 0)
                        {
                            TransactionManager.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                    }
                }


                #endregion
            }

            DocMemberFileMajorManager.DataTable.AcceptChanges();
            #endregion

            #region Update MasterMfMjId
            dtMemberFileMajor.AcceptChanges();
            DataRow drMasterMajor = FindMasterMajorFromdtMemberFileMajor();
            if (drMasterMajor == null)
            {
                TransactionManager.CancelSave();
                ShowMessage("رشته موضوع پروانه نامشخص می باشد");
                return;
            }
            DocMemberFileMajorManager.FindByMFId(-1, _MeId);
            DocMemberFileMajorManager.CurrentFilter = "FMjId=" + drMasterMajor["FMjId"].ToString() + "and MlId=" + drMasterMajor["MlId"].ToString() + "and InActiveState=0";
            if (DocMemberFileMajorManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("رشته موضوع پروانه نامشخص می باشد");
                DocMemberFileMajorManager.CurrentFilter = "";
                return;
            }
            int MasterMfMjId = Convert.ToInt32(DocMemberFileMajorManager[0]["MfMjId"]);
            DocMemberFileMajorManager.CurrentFilter = "";
            DocMemberFileManager[0].BeginEdit();
            DocMemberFileManager[0]["MasterMfMjId"] = MasterMfMjId;
            DocMemberFileManager[0].EndEdit();
            int cn = DocMemberFileManager.Save();
            DocMemberFileManager.DataTable.AcceptChanges();
            if (cn <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            #endregion
            if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Revival
                || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.UpGrade
                || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Qualification
                || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Change
                || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate
                || DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Reissues)
            {
                #region Accounting Fish
                if (AccountingManager != null)
                {
                    for (int i = 0; i < AccountingManager.Count; i++)
                    {
                        AccountingManager[i].BeginEdit();
                        if (Convert.ToInt32(AccountingManager[i]["TableTypeId"]) == -1)
                            AccountingManager[i]["TableTypeId"] = TableId;
                        AccountingManager[i].EndEdit();
                    }
                    AccountingManager.Save();
                    AccountingManager.DataTable.AcceptChanges();
                }
                #endregion
            }
            string Message = "";
            if (IsMfNoChanged)
                Message = "با ذخیره اطلاعات شماره پروانه شخص تغییر یافت.در صورتی که انجام این تغییر به اشتباه می باشد لطفا اطلاعات را مجددا بررسی و تغییرات لازم را انجام دهید.";
            TransactionManager.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete) + Message);
            #region Set Settings After Insert

            _MfId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"]);
            SetEditModeKeys(false);
            #endregion
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    protected bool InsertInActive(int TableId, int ReqId, int TableType, int ReTableType, TSP.DataManager.RequestInActivesManager RequestInActivesManager)
    {
        RequestInActivesManager.FindByTableIdTableType(TableId, TableType, -1, 0);
        if (RequestInActivesManager.Count > 0)
        {
            //if (Convert.ToInt32(RequestInActivesManager[0]["ReqId"]) != ReqId )//******Inserted in Previouse requests
            return true;
        }
        DataRow dr = RequestInActivesManager.NewRow();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        RequestInActivesManager.AddRow(dr);

        if (RequestInActivesManager.Save() > 0)
        {
            RequestInActivesManager.DataTable.AcceptChanges();
            return true;
        }
        else return false;
    }
    #endregion

    #region WorkFlow
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
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo
                          || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument
                        || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            if (FirstTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo
                                  || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument
                                || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument)
                            {
                                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                if (Permission > 0)
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
            CheckWorkFlowPermissionForSave(_PageMode);
            if (_PageMode != "New" && _PageMode != "ReDuplicate" && _PageMode != "Revival" && _PageMode != "Change" && _PageMode != "UpGrade" && _PageMode != "Qualification" && _PageMode != "Reissues" && _PageMode != "TransferedMemberRequest")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermissionDocumentUnit = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermissionDocumentUnitResp = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, TableType, Utility.GetCurrentUser_UserId(), PageMode);


        BtnNew.Enabled = btnNew2.Enabled = WFPermission.BtnNew || WFPermissionDocumentUnit.BtnNew || WFPermissionDocumentUnitResp.BtnNew;
        if (PageMode == "New")
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermissionDocumentUnit.BtnSave || WFPermissionDocumentUnitResp.BtnSave;
        else if (PageMode == "ReDuplicate" || PageMode == "Revival" || PageMode == "Change" || PageMode == "UpGrade" || PageMode == "Qualification")
        {
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnNewRequest || WFPermissionDocumentUnit.BtnNewRequest || WFPermissionDocumentUnitResp.BtnNewRequest;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {

        string TabelId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermissionDocumentUnit = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);

        TSP.DataManager.WFPermission WFPermissionDocumentUnitRes = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);



        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermissionDocumentUnit.BtnSave || WFPermissionDocumentUnitRes.BtnSave;
        btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit || WFPermissionDocumentUnit.BtnEdit || WFPermissionDocumentUnitRes.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده پرونده پروانه اشتغال به کار", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                ShowMessage("خطایی در مشاهده اطلاعات انجام گرفته است");
            }
        }
        catch (Exception err)
        {
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
    }
    #endregion

    void ShowMessage(String Message)
    {
        //this.DivReport.Visible = true;
        //Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='visible'; </script>");
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    private void SetError(Exception err)
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

    private void SetMeDocDefualtRegisterDate(int DocumentSetExpireDateType)
    {
        txtRegDate.Text = Utility.GetDateOfToday();
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
        if (string.IsNullOrEmpty(txtRegDate.Text))
        {
            txtRegDate.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtRegDate.Text);

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



    #region Create MfNo
    /// <summary>
    /// ایجاد شماره پروانه فرد بر اساس استان جاری و رشته فرد
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="DocMemberFileManager"></param>
    /// <param name="ProvinceManager"></param>
    /// <returns>ArrayLsit[0]: Boolean ,ArrayList[1]: MFNo,ArrayList[2]: MFSerialNo</returns>
    private ArrayList CreateMFNo(int MeId, Boolean IsTransfer, string PrePrCode, TSP.DataManager.DocMemberFileManager DocMemberFileManager)
    {
        ArrayList ReturnValues = new System.Collections.ArrayList();
        Boolean Error = false;
        string MjCode = "";
        string MfNo = "";
        string MFSerialNo = "";
        int MjId = -1;
        string PrCode = "";
        if (!IsTransfer)
        {
            // int PrId = Utility.GetCurrentProvinceId();
            // ProvinceManager.FindByCode(PrId);
            PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString(); //ProvinceManager[0]["NezamCode"].ToString();            
            if (string.IsNullOrEmpty(PrCode))
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                ReturnValues.Add(true);
                ReturnValues.Add("");
                ReturnValues.Add("");
                return ReturnValues;
            }
        }
        else
        {
            PrCode = PrePrCode;
        }

        if (!IsTransfer)
        {
            int MajorCount = dtMemberFileMajor.Rows.Count;

            for (int j = 0; j < MajorCount; j++)
            {
                if (dtMemberFileMajor.Rows[j].RowState != DataRowState.Deleted)
                {
                    int IsMaster = Convert.ToInt32(dtMemberFileMajor.Rows[j]["IsMaster"]);
                    if (IsMaster == 1)
                    {
                        MjId = int.Parse(dtMemberFileMajor.Rows[j]["FMjId"].ToString());
                        if (Utility.IsDBNullOrNullValue(dtMemberFileMajor.Rows[j]["MjCode"]))
                        {
                            ShowMessage("کد رشته نامشخص می باشد");
                            ReturnValues.Add(true);
                            ReturnValues.Add("");
                            ReturnValues.Add("");
                            return ReturnValues;
                        }
                        MjCode = dtMemberFileMajor.Rows[j]["MjCode"].ToString();
                        break;
                    }
                }
            }

            int NewSerialNo = DocMemberFileManager.GenerateNewMemberFileSerialNo(MeId, MjCode);
            if (NewSerialNo <= 0)
            {
                ShowMessage(DocMemberFileManager.FindErrorMessage(NewSerialNo));
                ReturnValues.Add(true);
                ReturnValues.Add("");
                ReturnValues.Add("");
                return ReturnValues;
            }
            MFSerialNo = NewSerialNo.ToString();

            int SerialLen = MFSerialNo.Length;
            int t = 5 - SerialLen;
            for (int i = 0; i < t; i++)
            {
                MFSerialNo = "0" + MFSerialNo;
            }
        }
        else
        {

        }
        MfNo = PrCode + "-" + MjCode + "-" + MFSerialNo;

        ReturnValues.Add(Error);
        ReturnValues.Add(MfNo);
        ReturnValues.Add(MFSerialNo);
        return ReturnValues;
    }

    private void CreateAndSetSuggestedMFNo(int MjId, int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        string MjCode = "";
        string MfNo = "";
        string MFSerialNo = "";
        string PrCode = "";
        Boolean IsTrnsfered = false;
        int PrId = Utility.GetCurrentProvinceId();

        TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.AllTypes, 1);
        if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
        {
            MfNo = TransferMemberManager[0]["FileNo"].ToString();
            IsTrnsfered = true;
        }
        if (!IsTrnsfered)
        {
            ProvinceManager.FindByCode(PrId);
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
        }
        if (!IsTrnsfered)
        {
            MajorManager.FindByCode(MjId);
            if (MajorManager.Count != 1)
                return;
            if (Utility.IsDBNullOrNullValue(MajorManager[0]["MjCode"]))
            {
                ShowMessage("کد رشته نامشخص است");
                return;
            }
            MjCode = MajorManager[0]["MjCode"].ToString();
            int NewSerialNo = DocMemberFileManager.GenerateNewMemberFileSerialNo(MeId, MjCode);
            if (NewSerialNo <= 0)
            {
                ShowMessage(DocMemberFileManager.FindErrorMessage(NewSerialNo));
            }

            MFSerialNo = NewSerialNo.ToString();

            int SerialLen = MFSerialNo.Length;
            int t = 5 - SerialLen;
            for (int i = 0; i < t; i++)
            {
                MFSerialNo = "0" + MFSerialNo;
            }

            MfNo = PrCode + "-" + MjCode + "-" + MFSerialNo;
        }
        txtMfNoSuggested.Text = MfNo;
    }
    #endregion

    #region Check Condition's Methods
    private Boolean CheckCanEditFish(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(MfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }

        int DocumentOfMemberRequestType = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
        if (DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Revival
     || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade
               || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.New
             || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification
             || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// برای درخواست ارتقا پایه-صدور-تمدید-درج صلاحیت جدید-المثنی ورود فیش الزامی می باشد. و برای درخواست تغییرات در صورت نیاز می توان ثبت نمود
    /// </summary>
    /// <param name="MfId"></param>
    /// <returns></returns>
    private Boolean CheckCanEditFishForEdit(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(MfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }

        int DocumentOfMemberRequestType = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
        if (DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Revival
     || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade
               || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.New
             || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification
            || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Change
            || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate)
        {
            return true;
        }
        return false;
    }

    private Boolean CheckIfTransferTypeInsert(int DocumentOfMemberRequestType)
    {
        if (DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Change
            || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification
            || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate
           || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.Revival
           || DocumentOfMemberRequestType == (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade)
        {
            return false;
        }
        return true;
    }
    #endregion

    private void SetRoundpanelRequestHeader(int DocumentOfMemberRequestType)
    {
        switch (DocumentOfMemberRequestType)
        {
            case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                RoundPanelMemberFile.HeaderText += "-درخواست صدور";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                RoundPanelMemberFile.HeaderText += "-درخواست درج صلاحیت جدید";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                RoundPanelMemberFile.HeaderText += "-درخواست صدور المثنی";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                RoundPanelMemberFile.HeaderText += "-درخواست تمدید";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                RoundPanelMemberFile.HeaderText += "-درخواست ارتقاء پایه";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                RoundPanelMemberFile.HeaderText += "-درخواست تغییرات";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest:
                RoundPanelMemberFile.HeaderText += "-درخواست تخصیص شماره پروانه عضو انتقالی";

                break;

        }
    }

    private void SetRoundpanelRequestHeaderAndLabales(int DocumentOfMemberRequestType)
    {
        string RequestComment = "";
        string RegDateComment = "";
        switch (DocumentOfMemberRequestType)
        {
            case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                // RoundPanelMemberFile.HeaderText = "درخواست صدور";
                lblRegDate.Text = "تاریخ صدور";
                RequestComment = "";
                RegDateComment = "تاریخ صدور به صورت پیش فرض تاریخ ثبت درخواست و تاریخ پایان اعتبار سه سال بعد می باشد ";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                // RoundPanelMemberFile.HeaderText = "درخواست درج صلاحیت جدید";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "در درخواست درج صلاحیت جدید شما قادر به تغییر پایه-صلاحیت و همچنین تمدید پروانه اشتغال می باشد";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد.در صورت نیاز امکان تغییر این تاریخ ها وجود دارد";

                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                //RoundPanelMemberFile.HeaderText = "درخواست صدور المثنی";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "";
                RegDateComment = "";
                //PanelMajor.Visible = false;
                //GridViewMajor.Enabled = false;
                //GridViewMajor.Columns["clmnDelete"].Visible = false;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                // RoundPanelMemberFile.HeaderText = "درخواست تمدید";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                //RequestComment = "در درخواست تمدید پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                // RoundPanelMemberFile.HeaderText = "درخواست ارتقاء پایه";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "در درخواست ارتقاء پایه شما قادر به تغییر پایه-صلاحیت و همچنین تمدید پروانه اشتغال می باشد";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد.در صورت نیاز امکان تغییر این تاریخ ها وجود دارد";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                //  RoundPanelMemberFile.HeaderText = "درخواست تغییرات";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest:
                //  RoundPanelMemberFile.HeaderText = "درخواست تخصیص شماره پروانه عضو انتقالی";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;

        }

        if (!string.IsNullOrEmpty(RequestComment))
        {
            txtRequestComment.Visible = true;
            txtRequestComment.InnerText = RequestComment;
        }
        else
            txtRequestComment.Visible = false;

        if (!string.IsNullOrEmpty(RegDateComment))
        {
            lblRegDateComment.Visible = true;
            lblRegDateComment.InnerText = RegDateComment;
        }
        else
            lblRegDateComment.Visible = false;
    }

    private void KeepPageState()
    {
        if (cmbMajorType.SelectedIndex == 1)
        {
            lblWarningIsPrinted.ClientVisible = true;
            chkIsPrinted.ClientVisible = true;
            PanelSuggestMFNo.ClientVisible = false;
        }
        else
        {
            lblWarningIsPrinted.ClientVisible = false;
            chkIsPrinted.ClientVisible = false;
            PanelSuggestMFNo.ClientVisible = true;
        }
    }

    private void CheckLicenceInfo()
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMajor = DocMemberFileMajorManager.SelectMemberFileById(_MfId, _MeId, 0);
        lblWarningText.Text = "";
        Boolean HasWarning = false;
        string Warning = "";
        for (int i = 0; i < dtMajor.Rows.Count; i++)
        {
            int MlId = Convert.ToInt32(dtMajor.Rows[i]["MlId"]);
            MemberLicenceManager.FindByCode(MlId);
            if (MemberLicenceManager.Count == 1)
            {
                if (Convert.ToBoolean(MemberLicenceManager[0]["ReqInActive"]))
                {
                    Warning += "مدرک " + MemberLicenceManager[0]["MeLicenceNamertl"].ToString() + " از طریق واحد عضویت غیرفعال شده است." + "\n";
                    HasWarning = true;
                }
            }
        }
        if (HasWarning)
        {
            Warning = "هشدار: " + Warning;
            lblWarningText.Visible = true;
            ImgWarningMsg.ClientVisible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
            lblWarningText.Text = Warning;
        }
        else
        {
            lblWarningText.Visible = false;
            ImgWarningMsg.ClientVisible = false;
            lblWarningText.Text = Warning;
        }

    }

    private void CheckMembershipRequst()
    {
        lblWarningText2.Text = "";
        Boolean HasWarning = false;
        string Warning = "";
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindLastReqByMemberId(_MeId, 0, 0);

        if (MemberRequestManager.Count == 1)
        {
            string RequstType = MemberRequestManager[0]["IsCreated"].ToString();
            string type = "";
            switch (RequstType)
            {
                case "0":
                    type = "تغییرات کلی پرونده عضویت";
                    break;
                case "3":
                    type = "تغییرات اطلاعات پایه";
                    break;
                case "4":
                    type = "تغییرات مدرک تحصیلی";
                    break;
                default:
                    type = "پرونده عضویت؟";
                    break;
            }
            Warning += "درخواست درجریان مربوط به " + type + " در سیستم ثبت شده است.";
            HasWarning = true;
        }
        if (HasWarning)
        {
            Warning = "هشدار: " + Warning;
            lblWarningText2.Visible = true;
            ImgWarningMsg2.ClientVisible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
            lblWarningText2.Text = Warning;
        }
        else
        {
            lblWarningText2.Visible = false;
            ImgWarningMsg2.ClientVisible = false;
            lblWarningText2.Text = Warning;
        }
    }

    private bool CheckMembershipInActiveMajor()
    {
        bool result = false;
        if (Session["TestMemberFileMajor"] == null)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return false;
        }

        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
        Boolean HasWarning = false;
        string Warning = "";
        for (int i = 0; i < dtMemberFileMajor.Rows.Count; i++)
        {
            if (dtMemberFileMajor.Rows[i].RowState != DataRowState.Deleted)
            {
                if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsInActived"]) == 0)
                {
                    int MlId = Convert.ToInt32(dtMemberFileMajor.Rows[i]["MlId"]);
                    MemberLicenceManager.FindByCode(MlId);
                    if (MemberLicenceManager.Count == 1)
                    {
                        if (Convert.ToBoolean(MemberLicenceManager[0]["ReqInActive"]))
                        {
                            Warning += "مدرک " + MemberLicenceManager[0]["MeLicenceNamertl"].ToString() + " از طریق واحد عضویت غیرفعال شده است"
                                 + "\n" + "لطفا مدرک جدید را جایگزین نمائید";
                            result = true;
                            HasWarning = true;
                        }
                    }
                }
            }
        }
        if (HasWarning)
        {
            Warning = "هشدار: " + Warning;
            ShowMessage(Warning);
        }
        return result;
    }

    private DataRow FindMasterMajorFromdtMemberFileMajor()
    {
        DataRow dr = null;
        int MajorCount = dtMemberFileMajor.Rows.Count;
        for (int j = 0; j < MajorCount; j++)
        {
            if (dtMemberFileMajor.Rows[j].RowState != DataRowState.Deleted)
            {
                int IsMaster = Convert.ToInt32(dtMemberFileMajor.Rows[j]["IsMaster"]);
                if (IsMaster == 1)
                {
                    return dtMemberFileMajor.Rows[j];
                    break;
                }
            }
        }
        return dr;
    }

    private void CheckColor(int MfId, int MeId)
    {
        CheckMenueChange(MfId, MeId);
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectByMember(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        DocMemberFileManager.CurrentFilter = "IsConfirm=1 and MfId<" + MfId.ToString();
        if (DocMemberFileManager.Count == 0)
            return;
        Boolean HasChanged = false;
        if (DocMemberFileManager.DataTable.DefaultView[0]["ExpireDate"].ToString() != txtExpDate.Text)
        {
            txtExpDate.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (DocMemberFileManager.DataTable.DefaultView[0]["RegDate"].ToString() != txtRegDate.Text)
        {
            txtRegDate.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (DocMemberFileManager.DataTable.DefaultView[0]["MFNo"].ToString() != lblMFNo.Text)
        {
            lblMFNo.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (DocMemberFileManager.DataTable.DefaultView[0]["SerialNo"].ToString() != txtSerialNo.Text)
        {
            txtSerialNo.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (DocMemberFileManager.DataTable.DefaultView[0]["Description"].ToString() != txtDescription.Text)
        {
            txtDescription.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (cmbIsTemporary.Value != null && DocMemberFileManager.DataTable.DefaultView[0]["IsTemporary"].ToString() != cmbIsTemporary.Value.ToString())
        {
            cmbIsTemporary.ForeColor = Color.Red;
            HasChanged = true;
        }
        if (HasChanged == true)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("BaseInfo")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("BaseInfo")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("BaseInfo")].Image.Height = Utility.MenuImgSize;
        }

    }

    private void CheckMenueChange(int MfId, int MeId)
    {
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        DocMemberExamManager.SelectById(MfId, MeId);
        if (DocMemberExamManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Height = Utility.MenuImgSize;
        }
        DataTable dtRes = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
        if (dtRes.Rows.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Height = Utility.MenuImgSize;
        }

    }

    protected void flpAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";


        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId_" + _MeId.ToString() + Path.GetRandomFileName() + ImageType.Extension;

            } while ((id == "flpAccAttach" && File.Exists(MapPath("~/image/DocMeFile/AccountConfirm/") + ret) == true)
                  || (id == "flpFishAttach" && File.Exists(MapPath("~/image/DocMeFile/Fish/") + ret) == true)
                  || (id == "flpFrontOldDoc" && File.Exists(MapPath("~/image/DocMeFile/OldDoc/") + ret) == true)
                  || (id == "flpBackOldDoc" && File.Exists(MapPath("~/image/DocMeFile/OldDoc/") + ret) == true)
                   || (id == "flpHse" && File.Exists(MapPath("~/image/DocMeFile/Hse/") + ret) == true)
                  || (id == "flpTaxOfficeLetter" && File.Exists(MapPath("~/image/DocMeFile/TaxOffice/") + ret) == true)
                  || (id == "flpJooshPeriod" && File.Exists(MapPath("~/image/DocMeFile/JooshPeriod/") + ret) == true)
                  );

            string FileName = "";

            if (id == "flpHse")
            {
                Session["HseFileURL"] = "~/image/DocMeFile/Hse/" + ret;
                FileName = MapPath("~/image/DocMeFile/Hse/") + ret;
            }
            if (id == "flpAccAttach")
            {
                Session["ACCFileURL"] = "~/image/DocMeFile/AccountConfirm/" + ret;
                FileName = MapPath("~/Image/Temp/") + ret;
            }

            else if (id == "flpFishAttach")
            {
                Session["FishFileURL"] = "~/image/DocMeFile/Fish/" + ret;
                FileName = MapPath("~/image/DocMeFile/Fish/") + ret;
            }

            else if (id == "flpFrontOldDoc")
            {
                Session["ImgOldDocFrontURL"] = "~/image/DocMeFile/OldDoc/" + ret;
                FileName = MapPath("~/image/DocMeFile/OldDoc/") + ret;
            }

            else if (id == "flpBackOldDoc")
            {
                Session["ImgOldDocBackURL"] = "~/image/DocMeFile/OldDoc/" + ret;
                FileName = MapPath("~/image/DocMeFile/OldDoc/") + ret;
            }

            else if (id == "flpTaxOfficeLetter")
            {
                Session["ImgTaxOfficeLetter"] = "~/image/DocMeFile/TaxOffice/" + ret;
                FileName = MapPath("~/image/DocMeFile/TaxOffice/") + ret;
            }
            else if (id == "flpJooshPeriod")
            {
                Session["ImgJooshPeriod"] = "~/image/DocMeFile/JooshPeriod/" + ret;
                FileName = MapPath("~/image/DocMeFile/JooshPeriod/") + ret;
            }

            uploadedFile.SaveAs(FileName, true);
        }
        return ret;
    }

    #endregion
}
