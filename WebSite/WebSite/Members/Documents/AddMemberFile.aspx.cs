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
using System.Globalization;
using System.Drawing;
using System.IO;
public partial class Members_Documents_AddMemberFile : System.Web.UI.Page
{
    DataTable dtMemberFileMajor = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        // RoundPanelBasicInfo.Enabled = false;
        if (Utility.IsDocMemberFileMajorCheckInComission())
        {
            GridViewMajor.Columns["MailNo"].Visible = true;
            GridViewMajor.Columns["MailDate"].Visible = true;
        }
        else
        {
            GridViewMajor.Columns["MailNo"].Visible = false;
            GridViewMajor.Columns["MailDate"].Visible = false;
        }

        // txtMailNoCom.Attributes["onkeyup"] = "return ltr_override(event)";
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("MemberFiles.aspx");
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



            Session["AccountingManager"] =
             Session["HseFileURL"] =
            Session["ImgOldDocFrontURL"] =
            Session["ImgOldDocBackURL"] =
            Session["ImgTaxOfficeLetter"] = Session["ImgJooshPeriod"] = null;

            Session["AccountingManager"] = CreateAccountingManager();

            SetKeys();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            SetLabelRegEnter();
        }
        BindAccountingGrid();
        if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
        {
            hpImgTaxOfficeLetter.ImageUrl = Session["ImgTaxOfficeLetter"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
        {
            hpImgFrontOldDoc.ImageUrl = Session["ImgOldDocFrontURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
        {
            hpImgBackOldDoc.ImageUrl = Session["ImgOldDocBackURL"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
        {
            hpImgJooshPeriod.ImageUrl = Session["ImgJooshPeriod"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
        {
            hpImgHse.ImageUrl = Session["HseFileURL"].ToString();
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());
        if (!string.IsNullOrEmpty(MeId) && !string.IsNullOrEmpty(MFId))
            CheckColor(int.Parse(MFId), int.Parse(MeId));
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

    }

    //***************************************Buttons******************************************************************
    protected void BtnNew_Click(object sender, EventArgs e)
    {

        ClearControls();
        SetNewModeKeys();
        EnableControls();
        //CheckWorkFlowPermission();        
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());

            if (string.IsNullOrEmpty(MFId) && PageMode != "New")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            //if (PageMode != "New")
            //{
            //    if (CheckMembershipInActiveMajor()) return;
            //}

            if (PageMode == "New")
            {
                InsertMemberFile();
            }
            else if (PageMode == "Edit")
            {
                Edit(int.Parse(MFId));
            }
            else if (PageMode == "ReDuplicate")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate);
            }
            else if (PageMode == "Revival")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.Revival);
            }
            else if (PageMode == "Change")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.Change);
            }
            else if (PageMode == "UpGrade")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.UpGrade);
            }
            else if (PageMode == "Qualification")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.Qualification);

            }
            else if (PageMode == "Reissues")
            {
                InsertNewRequest(int.Parse(MFId), TSP.DataManager.DocumentOfMemberRequestType.Reissues);
            }
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
            Response.Redirect("MemberFiles.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFiles.aspx");
        }
    }

    //protected void btnAddMajor_Click(object sender, EventArgs e)
    //{
    //    string Warning = "";
    //    if (Session["TestMemberFileMajor"] == null)
    //    {
    //        Session["TestMemberFileMajor"] = CreatedtMeFileMajor();
    //        GridViewMajor.DataSource = dtMemberFileMajor;
    //        GridViewMajor.DataBind();
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
    //        return;
    //    }
    //    dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

    //    #region Check Conditions
    //    //if (cmbMajor.SelectedIndex < 0)
    //    //{
    //    //    ShowMessage("مدرک تحصیلی فرد انتخاب نشده است");
    //    //    return;
    //    //}
    //    //if (cmbMajorType.SelectedIndex < 0)
    //    //{
    //    //    ShowMessage("رشته موضوع پروانه انتخاب نشده است");
    //    //    return;
    //    //}
    //    for (int i = 0; i < dtMemberFileMajor.Rows.Count; i++)
    //    {
    //        if (dtMemberFileMajor.Rows[i].RowState != DataRowState.Deleted)
    //        {
    //            if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsInActived"]) != 1)
    //            {
    //                if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["MlId"]) == Convert.ToInt32(cmbMajor.SelectedItem.Value))
    //                    && (Convert.ToInt32(dtMemberFileMajor.Rows[i]["FMjId"]) == Convert.ToInt32(cmbFileMajor.SelectedItem.Value)))
    //                {
    //                    ShowMessage("این مدرک تحصیلی قبلا ثبت شده است");
    //                    return;
    //                }

    //                if (Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsMaster"]) == 1 && cmbMajorType.SelectedIndex == 0)
    //                {
    //                    ShowMessage("پیش از این رشته موضوع پروانه انتخاب شده است" + "<br>" + "جهت تغییر رشته موضوع پروانه، ابتدا رشته موضوع پروانه قبلی را حذف و سپس این رشته را به عنوان موضوع پروانه انتخاب نمایید");
    //                    return;
    //                }

    //                if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["FMjId"]) == Convert.ToInt32(cmbFileMajor.SelectedItem.Value)))
    //                {
    //                    Warning = "هشدار: پیش از این رشته مدرک انتخاب شده در لیست رشته ها ثبت شده است";
    //                }

    //                if ((Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsPrinted"]) == 1) && Convert.ToInt32(dtMemberFileMajor.Rows[i]["IsMaster"]) == 0 && chkIsPrinted.Checked)
    //                {
    //                    ShowMessage("به غیر از مدرک موضوع پروانه ، تنها یک مدرک را می توان برای چاپ در گواهینامه انتخاب نمود");
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //    #endregion

    //    DataRow dr = dtMemberFileMajor.NewRow();

    //    try
    //    {
    //        if (RoundPanelComission.Visible)
    //        {
    //            dr["MailDate"] = txtMailDateCom.Text;
    //            dr["MailNo"] = txtMailNoCom.Text;
    //        }
    //        dr["MlName"] = cmbMajor.SelectedItem.Text.ToString();
    //        dr["MlId"] = cmbMajor.SelectedItem.Value.ToString();
    //        dr["FMjName"] = cmbFileMajor.SelectedItem.Text.ToString();
    //        dr["FMjId"] = cmbFileMajor.SelectedItem.Value.ToString();
    //        dr["IsInActived"] = 0;
    //        dr["InActives"] = "فعال";
    //        dr["UnName"] = txtUnivercity.Text;
    //        dr["UnCount"] = txtUniCountry.Text;
    //        dr["UnEndDate"] = txtUNiEndDate.Text;
    //        dr["UnGrade"] = txtUniGrade.Text;
    //        if ((cmbMajorType.SelectedIndex == 1) && (chkIsPrinted.Checked))
    //        {
    //            dr["IsPrinted"] = 1;
    //            dr["IsPrintedName"] = "چاپ در گواهینامه";
    //        }
    //        else
    //            dr["IsPrinted"] = 0;

    //        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
    //        MajorManager.FindByCode(int.Parse(cmbFileMajor.SelectedItem.Value.ToString()));
    //        if (MajorManager.Count <= 0)
    //        {
    //            ShowMessage("خطایی در اضافه کردن رخ داده است");
    //            return;
    //        }
    //        dr["MjCode"] = MajorManager[0]["MjCode"].ToString();

    //        if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
    //        {
    //            dr["IsPrinted"] = 1;
    //            dr["IsPrintedName"] = "چاپ در گواهینامه";
    //            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
    //            TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();

    //            #region Check Licence Conditions
    //            MemberLicenceManager.FindByCode(int.Parse(cmbMajor.SelectedItem.Value.ToString()));
    //            if (MemberLicenceManager.Count <= 0)
    //            {
    //                ShowMessage("خطایی در اضافه کردن رخ داده است");
    //                return;
    //            }
    //            //dr["MjCode"] = MemberLicenceManager[0]["MjCode"].ToString();
    //            string EndDate = MemberLicenceManager[0]["EndDate"].ToString();
    //            LicenceManager.FindByCode(int.Parse(MemberLicenceManager[0]["LiId"].ToString()));
    //            if (LicenceManager.Count <= 0)
    //            {
    //                ShowMessage("خطایی در اضافه کردن رخ داده است");
    //                return;
    //            }
    //            if (Utility.IsDBNullOrNullValue(LicenceManager[0]["JobDurationNeddForMeFile"]))
    //            {
    //                ShowMessage("مقطع مدرک تحصیلی انتخاب شده جهت پروانه اشتغال دارای اعتبار نمی باشد");
    //                return;
    //            }

    //            int JobDurationNeedForMeFile = int.Parse(LicenceManager[0]["JobDurationNeddForMeFile"].ToString());
    //            PersianCalendar FC = new PersianCalendar();
    //            DateTime EndDateMiladi = Utility.Date.ShamsiToMiladi(int.Parse(EndDate.Substring(0, 4)), int.Parse(EndDate.Substring(5, 2)), int.Parse(EndDate.Substring(8, 2)));
    //            DateTime DtAddYear = FC.AddYears(EndDateMiladi, JobDurationNeedForMeFile);
    //            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
    //            string AcceptedJobDuration = PDate.GetYear(DtAddYear) + "/" + PDate.GetMonth(DtAddYear).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYear).ToString().PadLeft(2, '0');
    //            int IsDateAccepted = string.Compare(Utility.GetDateOfToday(), AcceptedJobDuration);
    //            if (IsDateAccepted < 0)
    //            {
    //                ShowMessage("رشته انتخاب شده نمی تواند رشته موضوع پروانه باشد.برای صدور پروانه بایستی" + JobDurationNeedForMeFile.ToString() + " سال از تاریخ فارغ التحصیلی رشته موضوع پروانه گذشته باشد.");
    //                if (Utility.IsJobDurationNeedForMeFileChecked())
    //                    return;
    //            }
    //            #endregion

    //            dr["MjType"] = "می باشد";
    //            dr["IsMaster"] = 1;
    //            int MajorCount = dtMemberFileMajor.Rows.Count;
    //            for (int i = 0; i <= MajorCount - 1; i++)
    //            {
    //                if (dtMemberFileMajor.Rows[i].RowState != DataRowState.Deleted)
    //                {
    //                    dtMemberFileMajor.Rows[i].BeginEdit();

    //                    dtMemberFileMajor.Rows[i]["MjType"] = "نمی باشد";
    //                    dtMemberFileMajor.Rows[i]["IsMaster"] = 0;

    //                    dtMemberFileMajor.Rows[i].EndEdit();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            dr["MjType"] = "نمی باشد";
    //            dr["IsMaster"] = 0;
    //        }



    //        dtMemberFileMajor.Rows.Add(dr);
    //        GridViewMajor.DataSource = dtMemberFileMajor;
    //        GridViewMajor.DataBind();
    //        cmbMajor.SelectedIndex = -1;
    //        cmbMajorType.SelectedIndex = 0;
    //        if (!string.IsNullOrWhiteSpace(Warning))
    //            ShowMessage(Warning);
    //        KeepPageState();
    //        txtUniCountry.Text = "";
    //        txtUNiEndDate.Text = "";
    //        txtUniGrade.Text = "";
    //        txtUnivercity.Text = "";
    //        txtMfNoSuggested.Text = "";
    //        chkIsPrinted.Checked = false;
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        ShowMessage("خطایی در اضافه کردن رخ داده است");
    //    }
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (HiddenFieldDocMemberFile["MFId"] != null || !string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            if (CheckPermitionForEdit(int.Parse(MfId)))
            {
                //if (CheckCanEditFishForEdit(Convert.ToInt32(MfId)))
                //{
                //    PanelAccountingInserting.Visible = true;
                //    GridViewAccounting.Columns["Delete"].Visible = true;
                //}
                //else
                //{
                //    PanelAccountingInserting.Visible = false;
                //    GridViewAccounting.Columns["Delete"].Visible = false;
                //}
                EnableControls();

                txtMeId.Enabled = false;

                //cmbMajorType.SelectedIndex = 0;
                //KeepPageState();
                // cmbMajor.SelectedIndex = 0;
                //cmbMajor_SelectedIndexChanged(this, new EventArgs());

                HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                RoundPanelMemberFile.HeaderText = "ویرایش";
            }
            else
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.");
            }
        }
        else
        {
            ShowMessage("امکان ویرایش اطلاعات وجود ندارد.");
        }
    }
    //*********************************************************************************************************
    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberJobConfirm.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&MeId=" + HiddenFieldDocMemberFile["MeId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&MeId=" + HiddenFieldDocMemberFile["MeId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

        }
    }

    //protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
    //    cmbMajor.DataBind();
    //    string MjId = "";
    //    if (cmbMajor.SelectedItem != null)
    //    {
    //        if (cmbMajor.Items.Count > 1)
    //            cmbMajorType.Enabled = true;
    //        else
    //            cmbMajorType.Enabled = false;
    //        int MLId = int.Parse(cmbMajor.SelectedItem.Value.ToString());
    //        MemberLicenceManager.FindByCode(MLId);
    //        if (MemberLicenceManager.Count == 1)
    //        {
    //            MjId = MemberLicenceManager[0]["MjId"].ToString();
    //            cmbFileMajor.SelectedIndex = 0;
    //        }
    //    }
    //    ObjdsMajor.SelectParameters["MjId"].DefaultValue = MjId;
    //    cmbFileMajor.DataBind();
    //    string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

    //    if (!string.IsNullOrWhiteSpace(MjId) && cmbFileMajor.Items != null)
    //    {
    //        cmbFileMajor.SelectedIndex = cmbFileMajor.Items.FindByValue(MjId).Index;

    //        if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
    //            CreateAndSetSuggestedMFNo(Convert.ToInt32(MjId), Convert.ToInt32(txtMeId.Text));
    //        else
    //        {
    //            txtMfNoSuggested.Text = "- - -";
    //        }
    //    }
    //    FillMeLicenceInfo();
    //}

    //protected void cmbFileMajor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (cmbFileMajor.Items != null)
    //    {
    //        string MjId = cmbFileMajor.SelectedItem.Value.ToString();
    //        cmbFileMajor.SelectedIndex = cmbFileMajor.Items.FindByValue(MjId).Index;

    //        if (cmbMajorType.SelectedIndex == 0)//*******رشته موضوع پروانه
    //            CreateAndSetSuggestedMFNo(Convert.ToInt32(MjId), Convert.ToInt32(txtMeId.Text));
    //        else
    //        {
    //            txtMfNoSuggested.Text = "- - -";
    //        }
    //    }
    //}

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
    //*****************در صورتی که بخواهیم از قسمت کمسیون هم ازی رشته استفاده کنیم رویداده به کنترل اضافه گردد*****************************************
    //protected void cmbFileMajor_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    int MlId = -1;
    //    int MemberMjId = -1;
    //    int MeFileMjId = -1;
    //    TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
    //    //if (cmbMajor.Value != null)
    //    //    MlId = Convert.ToInt32(cmbMajor.Value);
    //    //else
    //    //    return;
    //    MemberLicenceManager.FindByCode(MlId);
    //    if (MemberLicenceManager.Count != 1 || Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MjId"]))
    //        return;
    //    //if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["CounName"]))
    //    //    txtUniCountry.Text = MemberLicenceManager[0]["CounName"].ToString();
    //    //if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["EndDate"]))
    //    //    txtUNiEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
    //    //if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["Avg"]))
    //    //    txtUniGrade.Text = MemberLicenceManager[0]["Avg"].ToString();
    //    //if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
    //    //    txtUnivercity.Text = MemberLicenceManager[0]["UnName"].ToString();

    //    //MemberMjId = Convert.ToInt32(MemberLicenceManager[0]["MjId"]);
    //    //if (cmbFileMajor.Value != null)
    //    //    MeFileMjId = Convert.ToInt32(cmbFileMajor.Value);

    //    if (Utility.IsDocMemberFileMajorCheckInComission())
    //    {
    //        if (MemberMjId != MeFileMjId)
    //            RoundPanelComission.Visible = true;
    //        else
    //        {
    //            RoundPanelComission.Visible = false;
    //            txtMailNoCom.Text = "";
    //            txtMailTitleCom.Text = "";
    //            txtMailDateCom.Text = "";
    //        }
    //    }
    //}
    //***************************************Grids******************************************************************
    //protected void GridViewMajor_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    //{
    //    e.Cancel = true;

    //    GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
    //    GridViewMajor.DataBind();

    //    int Id = -1;
    //    if (GridViewMajor.FocusedRowIndex > -1)
    //    {
    //        Id = GridViewMajor.FocusedRowIndex;
    //    }
    //    if (Id == -1)
    //    {
    //        ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
    //        return;

    //    }
    //    else
    //    {
    //        int MFId = -1;
    //        if (!Utility.IsDBNullOrNullValue(HiddenFieldDocMemberFile["MFId"]) && HiddenFieldDocMemberFile["MFId"] != null)
    //            MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
    //        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
    //        if (PageMode != "Edit" && PageMode != "New")
    //            MFId = -1;
    //        dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
    //        if (Utility.IsDBNullOrNullValue(dtMemberFileMajor.Rows.Find(e.Keys["Id"])["MfId"])
    //            || MFId == int.Parse(dtMemberFileMajor.Rows.Find(e.Keys["Id"])["MfId"].ToString()))
    //        {
    //            dtMemberFileMajor.Rows.Find(e.Keys["Id"]).Delete();
    //        }
    //        else
    //        {
    //            if (dtMemberFileMajor.Rows.Find(e.Keys["Id"])["InActives"] != "غیرفعال")
    //            {
    //                DataRow dr = dtMemberFileMajor.Rows.Find(e.Keys["Id"]);
    //                dtMemberFileMajor.Rows.Find(e.Keys["Id"]).BeginEdit();
    //                dtMemberFileMajor.Rows.Find(e.Keys["Id"])["IsInActived"] = 1;
    //                dtMemberFileMajor.Rows.Find(e.Keys["Id"])["InActives"] = "غیرفعال";
    //                dtMemberFileMajor.Rows.Find(e.Keys["Id"]).EndEdit();
    //            }
    //        }

    //        Session["TestMemberFileMajor"] = dtMemberFileMajor;
    //        GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
    //        GridViewMajor.DataBind();
    //        dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];

    //    }
    //}

    protected void GridViewMajor_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
    }

    protected void GridViewMajor_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        FilldtMajor();
        if (HiddenFieldDocMemberFile["MFId"] != null)
        {
            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            DataRow dr = GridViewMajor.GetDataRow(e.VisibleIndex);
            if (dr != null)
            {
                if (dr.RowState == DataRowState.Unchanged)
                {
                    string CurretnMfId = e.GetValue("MfId").ToString();
                    if (MFId == CurretnMfId)
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

    protected void GridViewMajor_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "MailNo")
            e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
        if (e.Column.FieldName == "MailDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMajor_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MailNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

    }


    //protected void CallbackPanelDocMe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    //{
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    DocMemberFileManager.ClearBeforeFill = true;
    //    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
    //    MemberManager.ClearBeforeFill = true;
    //    TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
    //    TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
    //    TransferMemberManager.ClearBeforeFill = true;

    //    dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
    //    dtMemberFileMajor.Rows.Clear();
    //    Session["TestMemberFileMajor"] = dtMemberFileMajor;
    //    GridViewMajor.DataSource = dtMemberFileMajor;
    //    GridViewMajor.DataBind();
    //    txtExpDate.Text = "";
    //    lblFileNo.Text = "";
    //    lblMeLastName.Text = "";
    //    lblMeName.Text = "";
    //    lblMFNo.Text = "";
    //    lblPreMeNo.Text = "";
    //    lblPreProvince.Text = "";
    //    txtRegDate.Text = "";
    //    txtSerialNo.Text = "";
    //    lblTransferDate.Text = "";
    //    //RoundPanelBasicInfo.Visible = false;
    //    TblTransfer.Visible = false;
    //    SetTransferTypeVisible(false);
    //    ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = "";
    //    cmbMajor.DataBind();
    //    cmbFileMajor.Text = "";
    //    ImgMember.ImageUrl = "";
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //    if (!string.IsNullOrEmpty(txtMeId.Text.Trim()))
    //    {
    //        MemberManager.FindByCode(int.Parse(txtMeId.Text.Trim()));
    //        HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(txtMeId.Text.Trim());
    //        if (MemberManager.Count == 1)
    //        {
    //            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
    //            if (MRsId == 1)
    //            {
    //                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AccId"]))
    //                {
    //                    btnSave.Enabled = true;
    //                    btnSave2.Enabled = true;
    //                    int AccId = int.Parse(MemberManager[0]["AccId"].ToString());
    //                    decimal Balance = AccManager.GetAccountBalance(AccId, Utility.GetDateOfToday());
    //                    if (Balance != 0)
    //                    {
    //                        ShowMessage("مانده حساب عضو مورد نظر صفر نمی باشد.");
    //                        btnSave.Enabled = false;
    //                        btnSave2.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        btnSave.Enabled = true;
    //                        btnSave2.Enabled = true;
    //                    }
    //                }
    //                else
    //                {
    //                    ShowMessage("حساب عضو انتخاب شده نامشخص می باشد.");
    //                    btnSave.Enabled = false;
    //                    btnSave2.Enabled = false;
    //                }
    //                ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = txtMeId.Text.Trim();
    //                cmbFileMajor.DataBind();
    //                cmbMajor.SelectedIndex = 0;
    //                cmbMajor_SelectedIndexChanged(this, new EventArgs());
    //                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
    //                    lblMeName.Text = MemberManager[0]["FirstName"].ToString();
    //                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
    //                    lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
    //                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
    //                    ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
    //                else
    //                    ImgMember.ImageUrl = "~/Images/Person.png";

    //                TransferMemberManager.FindByMemberId(int.Parse(txtMeId.Text.Trim()), TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
    //                if (TransferMemberManager.Count > 0)
    //                {
    //                    //DataRow[] dr = TransferMemberManager.DataTable.Select("TransferType=" + (int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
    //                    //if (dr.Length > 0)
    //                    //{
    //                    TblTransfer.Visible = true;
    //                    SetTransferTypeVisible(true);
    //                    if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
    //                        lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
    //                    if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
    //                        lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
    //                    if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferDate"]))
    //                        lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
    //                    //}
    //                }
    //                else
    //                {
    //                    TblTransfer.Visible = false;
    //                    SetTransferTypeVisible(false);
    //                }
    //                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(int.Parse(txtMeId.Text.Trim()), 0);
    //                if (dtMeFile.Rows.Count > 0)
    //                {
    //                    btnSave.Enabled = false;
    //                    btnSave2.Enabled = false;
    //                    ShowMessage("عضو انتخاب شده دارای پروانه اشتغال می باشد.");

    //                    int MfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
    //                    DocMemberFileManager.FindByCode(MfId, 0);
    //                    if (DocMemberFileManager.Count == 1)
    //                    {
    //                        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
    //                        {
    //                            lblWorkFlowState.Visible = true;
    //                            lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
    //                        }
    //                        else
    //                        {
    //                            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
    //                            lblWorkFlowState.Visible = true;
    //                        }
    //                        FillForm(MfId);
    //                    }
    //                }
    //                else
    //                {
    //                    //RoundPanelBasicInfo.Visible = false;
    //                    lblWorkFlowState.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                ShowMessage("عضویت عضو انتخابی در وضعیت لغو شده یا در گردش می باشد.");
    //            }

    //        }
    //        else
    //        {
    //            ShowMessage("کد عضویت وارد شده معتبر نمی باشد.");
    //        }
    //    }
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //}  

    protected void CallbackPanelComission_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //string[] Parameters = e.Parameter.Split(';');
        //string LetterNo = Parameters[1];
        //string ReType = Parameters[0];
        //switch (ReType)
        //{
        //    case "CheckLetter":
        //        if (CheckLetterValidationAndFillForComission(LetterNo) < 0)
        //        {
        //            lblErrorMailCom.ClientVisible = true;
        //            txtMailDateCom.Text = "";
        //            txtMailTitleCom.Text = "";
        //        }
        //        else
        //            lblErrorMailCom.ClientVisible = false;

        //        break;
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

    //protected void GridViewAccounting_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    //{
    //    GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
    //    GridViewAccounting.JSProperties["cpMessage"] = "";
    //    TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];

    //    DataRow dr = AccountingManager.DataTable.Rows.Find(e.Keys["AccountingId"]);
    //    dr.Delete();
    //    e.Cancel = true;

    //    GridViewAccounting.CancelEdit();

    //    GridViewAccounting.DataSource = AccountingManager.DataTable;
    //    GridViewAccounting.DataBind();
    //}

    protected void GridViewAccounting_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;

        if (Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString()) == "View")
        {
            if (e.Row.Cells.Count > 5)
                e.Row.Cells[4].Controls[0].Visible = false;
        }

        if (HiddenFieldDocMemberFile["MFId"] != null)
        {
            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            DataRow dr = GridViewAccounting.GetDataRow(e.VisibleIndex);
            if (dr != null)
            {
                if (dr.RowState == DataRowState.Unchanged)
                {
                    string CurretnMfId = e.GetValue("TableTypeId").ToString();
                    if (MFId == CurretnMfId)
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

    //protected void GridViewAccounting_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
    //    GridViewAccounting.JSProperties["cpMessage"] = "";

    //    if (e.Parameters == "Add")
    //    {
    //        try
    //        {
    //            ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text);
    //            if (Convert.ToBoolean(ArrayAcc[0]) == false)
    //            {
    //                GridViewAccounting.JSProperties["cpMessage"] = ArrayAcc[1].ToString();// "این شماره فیش قبلا در سیستم ثبت شده است";
    //                GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
    //            }
    //            else
    //            {
    //                RowInserting();
    //                GridViewAccounting.JSProperties["cpSaveComplete"] = "1";
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            Utility.SaveWebsiteError(err);
    //            GridViewAccounting.JSProperties["cpSaveComplete"] = "0";
    //        }
    //    }
    //}
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

    //protected void RowInserting()
    //{
    //    if (Session["AccountingManager"] == null)
    //        return;
    //    TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];
    //    DataRow dr = AccountingManager.NewRow();
    //    dr.BeginEdit();
    //    dr["TableTypeId"] = -1;
    //    dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
    //    dr["Type"] = (int)TSP.DataManager.AccountingPaymentType.Fiche;
    //    dr["Bank"] = DBNull.Value;
    //    dr["BranchCode"] = DBNull.Value;
    //    dr["BranchName"] = DBNull.Value;
    //    dr["AccType"] = cmbAccType.Value;
    //    dr["AccTypeName"] = cmbAccType.Text;
    //    dr["Number"] = txtaNumber.Text;
    //    dr["Date"] = txtaDate.Text;
    //    dr["Amount"] = txtaAmount.Text;
    //    dr["CreateDate"] = Utility.GetDateOfToday();
    //    dr["UserId"] = Utility.GetCurrentUser_UserId();
    //    dr["ModifiedDate"] = DateTime.Now;

    //    dr.EndEdit();
    //    AccountingManager.AddRow(dr);

    //    GridViewAccounting.DataSource = AccountingManager.DataTable;
    //    GridViewAccounting.DataBind();
    //    ClearFormAccounting();
    //}

    //protected void ClearFormAccounting()
    //{
    //    txtaAmount.Text = "";
    //    txtaDate.Text = "";
    //    txtaNumber.Text = "";
    //    txtaDesc.Text = "";
    //}

    private void SetLabelRegEnter()
    {
        //TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        //CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString(), Utility.GetCurrentUser_AgentId());
        //if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
        //{
        //    decimal TotalCost = Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        //    lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ريال بابت صدور/ارتقا/تمدید پروانه باید پرداخت شود.";
        //    HiddenFieldDocMemberFile["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
        //}
    }

    //private void SetAccountingFilterExpression()
    //{
    //    ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.DocMemberFile).ToString();
    //}

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }
    #endregion

    private DataTable CreatedtMeFileMajor()
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
        dtMemberFileMajor.Columns.Add("MfId");
        dtMemberFileMajor.Columns.Add("InActives");
        dtMemberFileMajor.Columns.Add("IsInActived");
        dtMemberFileMajor.Columns.Add("MailNo");
        dtMemberFileMajor.Columns.Add("MailDate");
        dtMemberFileMajor.Columns.Add("IsPrinted");
        dtMemberFileMajor.Columns.Add("IsPrintedName");

        dtMemberFileMajor.Columns.Add("MlId");
        dtMemberFileMajor.Columns["MlId"].AutoIncrement = true;
        dtMemberFileMajor.Columns["MlId"].AutoIncrementSeed = 1;

        dtMemberFileMajor.Columns.Add("FMjId");
        dtMemberFileMajor.Columns["FMjId"].AutoIncrement = true;
        dtMemberFileMajor.Columns["FMjId"].AutoIncrementSeed = 1;

        return dtMemberFileMajor;
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
            HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
            HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];
            HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());

            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
            string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());

            //ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = MeId;
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            SetMode(PageMode);
            //CheckWorkFlowPermission();
            if (PageMode != "New") CheckLicenceInfo();
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
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
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
        KardanImageVisible(PageMode, Convert.ToInt32(DocMemberFileManager[0]["Type"]));
        CheckHasCivilLicenceAndSetJooshPeriod();
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
        }
    }
    #endregion

    #region Set New-Edit-View Mode
    private void SetNewModeKeys()
    {
        //PanelAccountingInserting.Visible = true;
        //GridViewAccounting.Columns["Delete"].Visible = true;

        txtMeId.Enabled = true;
        //PanelMajor.Visible = true;
        //RoundPanelBasicInfo.Visible = false;
        TblTransfer.Visible = false;
        SetTransferTypeVisible(false);

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        MenuMemberFile.Enabled = false;

        //ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = "";

        ClearForm();
        ClearControls();
        RoundPanelMemberFile.HeaderText = "جدید";
    }

    private void SetViewModeKeys()
    {
        MenuMemberFile.Enabled = true;


        //PanelAccountingInserting.Visible = false;
        // GridViewAccounting.Columns["Delete"].Visible = false;

        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);
        DisableControl();
        SetTransferControlEnable(false);
        //cmbMajorType.SelectedIndex = 0;
        //KeepPageState();
        //cmbMajor.SelectedIndex = 0;
        //cmbMajor_SelectedIndexChanged(this, new EventArgs());

        RoundPanelMemberFile.HeaderText = "مشاهده";
        txtMeId.Enabled = false;
        if (!CheckPermitionForEdit(MFId))
        {
            btnEdit.Enabled =
            btnEdit2.Enabled = btnSave.Enabled = btnSave2.Enabled = false;
        }
        //InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, MFId);
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
    }

    private void SetEditModeKeys()
    {
        if (HiddenFieldDocMemberFile["MFId"] == null || string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));

        MenuMemberFile.Enabled = true;

        EnableControls();
        txtMeId.Enabled = false;
        FillForm(MFId);
        //cmbMajorType.SelectedIndex = 0;
        //KeepPageState();
        // cmbMajor.SelectedIndex = 0;
        // cmbMajor_SelectedIndexChanged(this, new EventArgs());
        //InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, MFId);
        RoundPanelMemberFile.HeaderText = "ویرایش";
    }
    #endregion

    #region Set Request Mode
    /// <summary>
    /// المثنی
    /// </summary>
    private void SetReDuplicateModeKeys()
    {
        //PanelAccountingInserting.Visible = true;
        //PanelMajor.Visible = false;
        // GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        lblRequestType.Text = "درخواست صدور المثنی";
    }

    /// <summary>
    /// تمدید
    /// </summary>
    private void SetRevivalModeKeys()
    {
        //PanelMajor.Visible = false;
        //GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        lblRequestType.Text = "درخواست تمدید";
    }

    /// <summary>
    /// تغییرات
    /// </summary>
    private void SetChangeModeKeys()
    {
        //PanelMajor.Visible = false;
        //GridViewMajor.Columns["clmnDelete"].Visible = false;
        SetRequestsSetting();
        lblRequestType.Text = "درخواست تغییرات";
        SetTransferControlEnable(true);
    }

    /// <summary>
    /// ارتقاء پایه
    /// </summary>
    private void SetUpGradeModeKeys()
    {
        SetRequestsSetting();
        lblRequestType.Text = "درخواست ارتقاء پایه";
        SetTransferControlEnable(true);
    }

    /// <summary>
    /// درج صلاحیت جدید
    /// </summary>
    private void SetQualificationModeKeys()
    {
        SetRequestsSetting();
        lblRequestType.Text = "درخواست تغییر صلاحیت";
        SetTransferControlEnable(true);

    }

    /// <summary>
    /// صدور مجدد
    /// </summary>
    private void SetReissuesModeKeys()
    {
        EnableControls();
        SetRequestsSetting();
        lblRequestType.Text = "صدور مجدد";
        txtMeId.Enabled = true;
        SetTransferControlEnable(true);
    }

    #endregion

    private void SetRequestsSetting()
    {
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);
        //cmbMajorType.SelectedIndex = 0;
        //KeepPageState();
        //cmbMajor.SelectedIndex = 0;
        //cmbMajor_SelectedIndexChanged(this, new EventArgs());

        RoundPanelMemberFile.HeaderText = "مشاهده";
        lblRequestType.Visible = true;
        txtMeId.Enabled = false;
        SetTransferControlEnable(false);
        SetTransferTypeVisible(false);
        MenuMemberFile.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["btnAddMajor"] = btnAddMajor.Enabled;
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
        #endregion

        int MasterMjId = 0;
        int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
        int MReId = -2;

        #region Fill MemberInfo
        ReqManager.FindByMemberId(MeId, 0, 1, -1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        else
        {
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        }
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeId"]))
                txtMeId.Text = MemberManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                lblMeName.Text = MemberManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            else
                ImgMember.ImageUrl = "../../Images/Person.png";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
                ImageKardan.ImageUrl = MemberManager[0]["NezamKardanConfirmURL"].ToString();

            else
                ImageKardan.ImageUrl = "~/Images/noimage.gif";

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                HpIdNo.NavigateUrl = HpIdNo.ImageUrl = attachManager[0]["FilePath"].ToString();
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                HpSSN.NavigateUrl = HpSSN.ImageUrl = attachManager[0]["FilePath"].ToString();
            }
            if (MemberManager[0]["SexId"].ToString() == "2")
            {
                lblSoldire.ClientVisible = true;
                HpSoldire.ClientVisible = true;
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {

                    HpSoldire.NavigateUrl = HpSoldire.ImageUrl = attachManager[0]["FilePath"].ToString();
                }

            }
            else
            {
                lblSoldire.ClientVisible = false;
                HpSoldire.ClientVisible = false;
            }


        }
        #endregion

        #region Fill TransferMember Info
        TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
        if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
        {
            TblTransfer.Visible = true;
            SetTransferTypeVisible(true);
            lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
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

        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
        if (PageMode != "Reissues")
        {
            //*****Responsiblity***********************
            #region Fill Responsiblity
            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count == 1)
            {
                MasterMjId = Convert.ToInt32(DocMemberFileMajorManager[0]["MjId"]);
            }
            DataTable dtResDes = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
            if (dtResDes.Rows.Count == 1)
            {
                txtGradeDes.Text = dtResDes.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResObs = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            if (dtResObs.Rows.Count == 1)
            {
                txtGradeObs.Text = dtResObs.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResImp = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
            if (dtResImp.Rows.Count == 1)
            {
                txtGradeImp.Text = dtResImp.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResMapping = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
            if (dtResMapping.Rows.Count == 1)
            {
                txtGradeMapping.Text = dtResMapping.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResTraffic = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Traffic);
            if (dtResTraffic.Rows.Count == 1)
            {
                txtGradeTraffic.Text = dtResTraffic.Rows[0]["GrdName"].ToString();
            }
            DataTable dtResUrbanism = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism);
            if (dtResUrbanism.Rows.Count == 1)
            {
                txtGradeUrbanism.Text = dtResUrbanism.Rows[0]["GrdName"].ToString();
            }
            #endregion

            //*****DocMeInfo***********************
            DocMemberFileManager.FindByCode(MFId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                #region Set Document BaseInfo
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                    txtExpDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                    txtRegDate.Text = DocMemberFileManager[0]["RegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"])
                    && Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int)TSP.DataManager.DocumentOfMemberRequestType.New
                    && Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival
                    && Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer)
                    lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
                else
                    lblMFNo.Text = "- - -";
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                    txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["Description"]))
                    txtDescription.Text = DocMemberFileManager[0]["Description"].ToString();

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
                    SetRoundpanelRequestHeaderAndLabales(Convert.ToInt32(DocMemberFileManager[0]["Type"]));

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

                #endregion

                #region MemberFileMajor
                DataTable dtMajor = DocMemberFileMajorManager.SelectMemberFileById(MFId, MeId, -1, -1);
                dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
                int MfMjId = -1;
                for (int i = 0; i < dtMajor.Rows.Count; i++)
                {
                    MfMjId = Convert.ToInt32(dtMajor.Rows[i]["MFMjId"]);
                    DataRow dr = dtMemberFileMajor.NewRow();
                    dr["Id"] = dtMajor.Rows[i]["MFMjId"].ToString();
                    dr["MlId"] = dtMajor.Rows[i]["MlId"];
                    dr["FMjId"] = dtMajor.Rows[i]["FMjId"].ToString();
                    dr["MlName"] = dtMajor.Rows[i]["MlName"].ToString();
                    dr["FMjName"] = dtMajor.Rows[i]["FMjName"].ToString();
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

                }
                dtMemberFileMajor.AcceptChanges();
                GridViewMajor.DataSource = dtMemberFileMajor;
                GridViewMajor.DataBind();
                #endregion
            }
            //string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
            //  if (PageMode != "ReDuplicate" && PageMode != "Revival" && PageMode != "Change" && PageMode != "UpGrade" && PageMode != "Qualification" && PageMode != "Reissues")
            FillAccountingGrid();
        }
    }

    //private void FillMeLicenceInfo()
    //{

    //    int MlId = -1;
    //    if (cmbMajor.Value != null)
    //        MlId = Convert.ToInt32(cmbMajor.Value);
    //    else
    //        return;
    //    TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
    //    MemberLicenceManager.FindByCode(MlId);
    //    if (MemberLicenceManager.Count != 1 || Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MjId"]))
    //        return;
    //    if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["CounName"]))
    //        txtUniCountry.Text = MemberLicenceManager[0]["CounName"].ToString();
    //    if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["EndDate"]))
    //        txtUNiEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
    //    if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["Avg"]))
    //        txtUniGrade.Text = MemberLicenceManager[0]["Avg"].ToString();
    //    if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
    //        txtUnivercity.Text = MemberLicenceManager[0]["UnName"].ToString();
    //}

    #endregion

    #region Set Control's Enable-Clear
    private void ClearForm()
    {
        //cmbMajorType.SelectedIndex = 0;
        //KeepPageState();
        // cmbMajor.SelectedIndex = 0;
        // cmbMajor_SelectedIndexChanged(this, new EventArgs());
    }

    private void ClearControls()
    {
        Session["AccountingManager"] = CreateAccountingManager();
        BindAccountingGrid();
        txtMeId.Text = "";
        if (Session["TestMemberFileMajor"] == null)
            CreateMajorDataTable();
        else
        {
            dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            dtMemberFileMajor.Rows.Clear();
            Session["TestMemberFileMajor"] = dtMemberFileMajor;
            GridViewMajor.DataSource = dtMemberFileMajor;
            GridViewMajor.DataBind();
        }
        txtDescription.Text = "";
        //txtUniCountry.Text = "";
        //txtUNiEndDate.Text = "";
        //txtUniGrade.Text = "";
        //txtUnivercity.Text = "";

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

        //ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = "";
        //cmbMajor.DataBind();
        //cmbMajor_SelectedIndexChanged(this, new EventArgs());
        //cmbFileMajor.Text = "";
        //cmbMajor.Text = "";
        cmbTransferType.SelectedIndex = 0;
        ImgMember.ImageUrl = "~/Images/Person.png";
        HiddenFieldDocMemberFile["MeId"] = "";
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldDocMemberFile["MFId"] = "";
        txtGradeDes.Text = "";
        txtGradeImp.Text = "";
        txtGradeObs.Text = "";
        txtGradeUrbanism.Text = "";
        txtGradeTraffic.Text = "";
        txtGradeMapping.Text = "";
        //txtMfNoSuggested.Text = "";
    }

    //private void ResetComissionControls()
    //{
    //    RoundPanelComission.Visible = false;
    //    txtMailDateCom.Text = "";
    //    txtMailNoCom.Text = "";
    //    txtMailNoCom.Attributes["onkeyup"] = "return ltr_override(event)";
    //}

    private void EnableControls()
    {
        lblRequestType.Visible =
        cmbIsTemporary.Enabled =
        txtExpDate.Enabled =
        txtRegDate.Enabled =
        txtSerialNo.Enabled =
        txtDescription.Enabled =
        cmbTransferType.Enabled =
        flpFrontOldDoc.ClientVisible =
        flpBackOldDoc.ClientVisible =
        flpTaxOfficeLetter.ClientVisible =
        GridViewMajor.Enabled = true;
        if (lblJooshPeriod.ClientVisible == true)
            flpJooshPeriod.ClientVisible = true;
        else flpJooshPeriod.ClientVisible = false;
        if (lblHse.ClientVisible == true)
            flpHse.ClientVisible = true;
        else flpHse.ClientVisible = false;
        //GridViewMajor.Columns["clmnDelete"].Visible = true;
        //PanelMajor.Visible = true;
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
        flpJooshPeriod.ClientVisible =
        flpHse.ClientVisible  = false;

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
        //TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();

        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(LetterRelatedTablesManager);
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
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            #region Insert DocMemberFile
            DataRow MemberFileRow = DocMemberFileManager.NewRow();
            MemberFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Member;
            MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
            MemberFileRow["MeId"] = MeId;
            MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();

            TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
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
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                MemberFileRow["ImgOldDocFrontURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                MemberFileRow["ImgOldDocBackURL"] = Session["ImgOldDocBackURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
                MemberFileRow["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
                MemberFileRow["PeriodImageURL"] = Session["ImgJooshPeriod"].ToString();


            if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
                MemberFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();
            

            //MemberFileRow["MailDate"] = txtMailDate.Text;
            //MemberFileRow["MailNo"] = txtMailNo.Text;
            MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberFileRow["ModifiedDate"] = DateTime.Now;
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
            int CurrentNmcId = Utility.GetCurrentUser_MeId();
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            string Description = "شروع گردش کار پروانه اشتغال به کار توسط شخص حقیقی";
            if (WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Description) <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TransactionManager.EndSave();

            #region Set Controls AfterSave
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
            HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(DocMemberFileManager[0]["MfId"].ToString());
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
        //TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
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

        try
        {
            TransactionManager.BeginSave();

            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));

            TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            if (TransferMemberManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                {
                    PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                    IsTransfer = true;
                }
            }
            #region Comment
            //if (Session["TestMemberFileMajor"] == null)
            //{
            //    TransactionManager.CancelSave();
            //    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            //    return;
            //}
            //dtMemberFileMajor = (DataTable)Session["TestMemberFileMajor"];
            //dtMemberFileMajor.DefaultView.RowFilter = "IsInActived=0";
            //if (dtMemberFileMajor.DefaultView.Count == 0)
            //{
            //    TransactionManager.CancelSave();
            //    ShowMessage("رشته پروانه را انتخاب نمایید.");
            //    dtMemberFileMajor.DefaultView.RowFilter = "";
            //    return;
            //}

            //  dtMemberFileMajor.DefaultView.RowFilter = "";
            //#region Accounting Fish
            //int TableType = -1;
            //TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);// Convert.ToInt32(TableTypeManager[0]["TtId"]);
            //if (TableType == -1)
            //{
            //    TransactionManager.CancelSave();
            //    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
            //    return;
            //}
            //AccountingManagerForDel.FindByTableTypeId(Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MFId"])), TableType);
            //int cntAcc = AccountingManagerForDel.Count;
            //for (int i = 0; i < cntAcc; i++)
            //{
            //    AccountingManagerForDel[0].Delete();
            //    AccountingManagerForDel.DataTable.AcceptChanges();
            //}
            //AccountingManagerForDel.Save();

            //for (int i = 0; i < AccountingManager.Count; i++)
            //{
            //    AccountingManager[i].BeginEdit();
            //    //  AccountingManager[i]["TableType"] = TableType;
            //    if (Convert.ToInt32(AccountingManager[i]["TableTypeId"]) == -1)
            //        AccountingManager[i]["TableTypeId"] = MfId;
            //    AccountingManager[i].EndEdit();
            //}
            //AccountingManager.Save();
            //AccountingManager.DataTable.AcceptChanges();

            //#endregion
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
            DocMemberFileManager[0].EndEdit();
            if (DocMemberFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            DocMemberFileManager.DataTable.AcceptChanges();
            #endregion           
            TransactionManager.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));

            Session["TestMemberFileMajor"] = dtMemberFileMajor;
            GridViewMajor.DataSource = (DataTable)Session["TestMemberFileMajor"];
            GridViewMajor.DataBind();
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
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        DocMemberFileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(LetterRelatedTablesManager);
        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(RequestInActivesManager);
        #endregion

        Boolean IsTransfer = false;
        string PreMfNo = "";
        string PrCode = "";
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            if (TransferMemberManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                {
                    PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
                    PreMfNo = TransferMemberManager[0]["FileNo"].ToString();
                    IsTransfer = true;
                }
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

            #region DocMe Insert
            DataRow MeFileRow = DocMemberFileManager.NewRow();
            MeFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Member;
            MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
            //MeFileRow["MailNo"] = txtMailNo.Text;
            //MeFileRow["MailDate"] = txtMailDate.Text;
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
            {
                MeFileRow["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
                MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
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
            }
            else
                MeFileRow["Type"] = (int)DocumentOfMemberRequestType;
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                MeFileRow["PrId"] = DocMemberFileManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrIdOrigin"]))
                MeFileRow["PrIdOrigin"] = DocMemberFileManager[0]["PrIdOrigin"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                MeFileRow["MFNo"] = DocMemberFileManager[0]["MFNo"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNoOrigin"]))
                MeFileRow["MFNoOrigin"] = DocMemberFileManager[0]["MFNoOrigin"].ToString();

            MeFileRow["CreateDate"] = Utility.GetDateOfToday();
            MeFileRow["Description"] = txtDescription.Text;
            MeFileRow["IsConfirm"] = 0;
            MeFileRow["InActive"] = 0;
            MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MeFileRow["ModifiedDate"] = DateTime.Now;            
            if (!Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))
                MeFileRow["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["ImgJooshPeriod"]))
                MeFileRow["PeriodImageURL"] = Session["ImgJooshPeriod"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["HseFileURL"]))
                MeFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();

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

            int CurrentNmcId = Utility.GetCurrentUser_MeId();
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            string Description = "شروع گردش کار درخواست ارتقاء پایه پروانه اشتغال توسط عضو حقیقی";
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Description);
            if (WfStart <= 0)
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

            //**********SerialNo=PrCode +MjCode+MFSerialNo*********
            string MfNo = "";
            string MfSerialNo = "";
            if (!IsTransfer)
            {

                ArrayList MeFileNo = new System.Collections.ArrayList();
                MeFileNo = CreateMFNo(MeId, IsTransfer, PrCode, DocMemberFileManager);
                if (Convert.ToBoolean(MeFileNo[0]))//****IF HAS ERROR IN FINDING MFNO
                {
                    TransactionManager.CancelSave();
                    return;
                }
                MfNo = MeFileNo[1].ToString();
                MfSerialNo = MeFileNo[2].ToString();

                DocMemberFileManager[0]["MFNo"] = MfNo;
                DocMemberFileManager[0]["MFSerialNo"] = MfSerialNo;
            }
            else if (!string.IsNullOrWhiteSpace(PreMfNo))
            {
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
            DocMemberFileMajorManager.FindByMFId(-1, MeId);
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
                    //int TableType = -1;
                    //TSP.DataManager.TableTypeManager TableTypeManager = new TSP.DataManager.TableTypeManager();
                    //TableTypeManager.FindByTtCode(TSP.DataManager.TableType.MemberFile);
                    //if (TableTypeManager.Count == 1)
                    //{
                    //    TableType = Convert.ToInt32(TableTypeManager[0]["TtId"]);
                    //}

                    for (int i = 0; i < AccountingManager.Count; i++)
                    {
                        AccountingManager[i].BeginEdit();
                        //   AccountingManager[i]["TableType"] = TableType;
                        if (Convert.ToInt32(AccountingManager[i]["TableTypeId"]) == -1)
                            AccountingManager[i]["TableTypeId"] = TableId;
                        AccountingManager[i].EndEdit();
                    }
                    AccountingManager.Save();
                    AccountingManager.DataTable.AcceptChanges();
                }
                #endregion
            }

            TransactionManager.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            #region Set Settings After Insert
            MenuMemberFile.Enabled = true;

            HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
            HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
            lblMFNo.Text = MfNo;

            DocMemberFileManager.FindByCode(int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString()), 0);
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
            }

            //cmbMajorType.SelectedIndex = 0;
            //KeepPageState();
            //cmbMajor.SelectedIndex = 0;
            //cmbMajor_SelectedIndexChanged(this, new EventArgs());

            RoundPanelMemberFile.HeaderText = "ویرایش";
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
    //private int FindNmcId(int TaskId)
    //{
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int NmcId = -1;
    //    NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
    //    if (NmcId > 0)
    //    {
    //        return NmcId;
    //    }
    //    else
    //    {
    //        ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
    //        return (-1);
    //    }
    //}

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count <= 0)
        {
            return false;
        }
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());

                    if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
                    {
                        if (FirstNmcId == Utility.GetCurrentUser_MeId())
                        {
                            return true;
                        }
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

    //private void CheckWorkFlowPermission()
    //{
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    int TaskOrder = -1;
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }
    //    if (TaskOrder != 0)
    //    {
    //        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
    //        CheckWorkFlowPermissionForSave(PageMode);
    //        if (PageMode != "New" && PageMode != "ReDuplicate" && PageMode != "Revival" && PageMode != "Change" && PageMode != "UpGrade" && PageMode != "Qualification" && PageMode != "Reissues")
    //            CheckWorkFlowPermissionForEdit(PageMode);
    //    }
    //}

    //private void CheckWorkFlowPermissionForSave(string PageMode)
    //{
    //    int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
    //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;

    //    TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
    //    BtnNew.Enabled = btnNew2.Enabled = WFPermission.BtnNew;
    //    if (PageMode == "New")
    //        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
    //    else if (PageMode == "ReDuplicate" || PageMode == "Revival" || PageMode == "Change" || PageMode == "UpGrade" || PageMode == "Qualification")
    //    {
    //        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnNewRequest;
    //    }

    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //}

    //private void CheckWorkFlowPermissionForEdit(string PageMode)
    //{

    //    string TabelId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
    //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
    //    int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;

    //    TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);

    //    btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
    //    btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit;

    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;

    //}

    //private void InsertWorkFlowStateView(int TableType, int TableId)
    //{
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    try
    //    {
    //        int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده پرونده پروانه اشتغال به کار", Utility.GetCurrentUser_UserId());
    //        if (ViewState == -4)
    //        {
    //            ShowMessage("خطایی در مشاهده اطلاعات انجام گرفته است");
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                ShowMessage("اطلاعات تکراری می باشد");
    //            }
    //            else
    //            {
    //                ShowMessage("خطایی در ذخیره انجام گرفته است");
    //            }
    //        }
    //        else
    //        {
    //            ShowMessage("خطایی در ذخیره انجام گرفته است");
    //        }
    //    }
    //}
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
        dtMemberFileMajor.Columns.Add("MfId");
        dtMemberFileMajor.Columns.Add("InActives");
        dtMemberFileMajor.Columns.Add("IsInActived");
        dtMemberFileMajor.Columns.Add("MailNo");
        dtMemberFileMajor.Columns.Add("MailDate");
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

        TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
        if (TransferMemberManager.Count > 0)
        {
            //DataRow[] dr = TransferMemberManager.DataTable.Select("TransferType=" + (int)TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //if (dr.Length > 0)
            //{
            // PrCode = dr[0]["FileNo"].ToString().Remove(2);
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            {
                MfNo = TransferMemberManager[0]["FileNo"].ToString();
                IsTrnsfered = true;
            }
            //  }
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
        //txtMfNoSuggested.Text = MfNo;
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

    private void SetRoundpanelRequestHeaderAndLabales(int DocumentOfMemberRequestType)
    {
        string RequestComment = "";
        string RegDateComment = "";
        switch (DocumentOfMemberRequestType)
        {
            case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                lblRequestType.Text = "درخواست صدور";
                lblRegDate.Text = "تاریخ صدور";
                RequestComment = "";
                RegDateComment = "تاریخ صدور به صورت پیش فرض تاریخ ثبت درخواست و تاریخ پایان اعتبار سه سال بعد می باشد ";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                lblRequestType.Text = "درخواست درج صلاحیت جدید";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "در درخواست درج صلاحیت جدید شما قادر به تغییر پایه-صلاحیت و همچنین تمدید پروانه اشتغال می باشد";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد.در صورت نیاز امکان تغییر این تاریخ ها وجود دارد";

                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                lblRequestType.Text = "درخواست صدور المثنی";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "";
                RegDateComment = "";
                //PanelMajor.Visible = false;
                GridViewMajor.Enabled = false;
                //GridViewMajor.Columns["clmnDelete"].Visible = false;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                lblRequestType.Text = "درخواست تمدید";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "در درخواست تمدید پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                lblRequestType.Text = "درخواست ارتقاء پایه";
                lblRegDate.Text = "تاریخ آخرین تمدید";
                RequestComment = "در درخواست ارتقاء پایه شما قادر به تغییر پایه-صلاحیت و همچنین تمدید پروانه اشتغال می باشد";
                RegDateComment = "تاریخ آخرین تمدید و پایان اعتبار به صورت پیش فرض تاریخ تمدید و پایان اعتبار آخرین درخواست پروانه شخص می باشد.در صورت نیاز امکان تغییر این تاریخ ها وجود دارد";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                lblRequestType.Text = "درخواست تغییرات";
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
            lblRegDateComment.Text = RegDateComment;
        }
        else
            lblRegDateComment.Visible = false;
    }

    //private void KeepPageState()
    //{
    //    if (cmbMajorType.SelectedIndex == 1)
    //    {
    //        lblWarningIsPrinted.ClientVisible = true;
    //        chkIsPrinted.ClientVisible = true;
    //        PanelSuggestMFNo.ClientVisible = false;
    //    }
    //    else
    //    {
    //        lblWarningIsPrinted.ClientVisible = false;
    //        chkIsPrinted.ClientVisible = false;
    //        PanelSuggestMFNo.ClientVisible = true;
    //    }
    //}

    private void CheckLicenceInfo()
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
        DataTable dtMajor = DocMemberFileMajorManager.SelectMemberFileById(MFId, MeId, 0);
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
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        ProjectJobHistoryManager.FindForDelete(0, MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Documents));
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Height = Utility.MenuImgSize;
        }
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

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId_" +Utility.GetCurrentUser_MeId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while ((id == "flpFrontOldDoc" && File.Exists(MapPath("~/image/DocMeFile/OldDoc/") + ret) == true)
                  || (id == "flpBackOldDoc" && File.Exists(MapPath("~/image/DocMeFile/OldDoc/") + ret) == true)
                  || (id == "flpTaxOfficeLetter" && File.Exists(MapPath("~/image/DocMeFile/TaxOffice/") + ret) == true)
                  || (id == "flpHse" && File.Exists(MapPath("~/image/DocMeFile/Hse/") + ret) == true)
                  || (id == "flpJooshPeriod" && File.Exists(MapPath("~/image/DocMeFile/JooshPeriod/") + ret) == true));

            string FileName = "";

            if (id == "flpHse")
            {
                Session["HseFileURL"] = "~/image/DocMeFile/Hse/" + ret;
                FileName = MapPath("~/image/DocMeFile/Hse/") + ret;
            }            
            if (id == "flpFrontOldDoc")
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

    private void KardanImageVisible(string PageMode, int Type)
    {
        lblKardanAttach.Visible = false;      
        ImageKardan.ClientVisible = false;
        HiddenFieldDocMemberFile["IsKardanObliq"] = 0;

        if (!(PageMode == "New" || PageMode == "Edit" || PageMode == "View"))
            return;

        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(Utility.GetCurrentUser_MeId(), 0);
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {
            lblKardanAttach.Visible = true;
            ImageKardan.ClientVisible = true;
            HiddenFieldDocMemberFile["IsKardanObliq"] = 1;
        }

    }

    private void CheckHasCivilLicenceAndSetJooshPeriod()
    {
        flpJooshPeriod.ClientVisible = hpImgJooshPeriod.ClientVisible = lblJooshPeriod.ClientVisible =
            flpHse.ClientVisible = hpImgHse.ClientVisible = lblHse.ClientVisible = false;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(Utility.GetCurrentUser_MeId(), 0);
        if (dtMemberFileMajor.Rows.Count == 0)
            return;
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
             + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {
            flpJooshPeriod.ClientVisible = hpImgJooshPeriod.ClientVisible = lblJooshPeriod.ClientVisible = true;

        }
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
          + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Architecture + "  or  " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {
           // flpJooshPeriod.ClientVisible = hpImgJooshPeriod.ClientVisible = lblJooshPeriod.ClientVisible =
                 flpHse.ClientVisible = hpImgHse.ClientVisible = lblHse.ClientVisible
                = true;

        }
        dtMemberFileMajor.DefaultView.RowFilter = "";
    }
    #endregion
}
