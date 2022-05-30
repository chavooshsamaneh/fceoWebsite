using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardQualificationKardan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();

        if (!IsPostBack)
        {
            HiddenFieldAccConfirm["Conf1"] =
                HiddenFieldAccConfirm["Conf2"] = HiddenFieldAccConfirm["HSEImg"] = 0;

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
            if (dtDocMeFile.Rows.Count <= 0)
            {
                SetMessage("بدلیل نداشتن پروانه تایید شده امکان ثبت درخواست درج صلاحیت جدید وجود ندارد.");
                return;
            }
            SetMenueImage();
            HasCivilLicence();

        }
        if (Session["WizardQualificationImgFrontDoc"] != null)
        {
            imgFrontDoc.ImageUrl = Session["WizardQualificationImgFrontDoc"].ToString();
            HiddenFieldAccConfirm["FrontDoc"] = 1;
        }
        if (Session["WizardQualificationImgBackDoc"] != null)
        {
            imgBackDoc.ImageUrl = Session["WizardQualificationImgBackDoc"].ToString();
            HiddenFieldAccConfirm["backDoc"] = 1;
        }
        if (Session["WizardQualificationImgTaxOfficeLetter"] != null)
        {
            imgTaxOfficeLetter.ImageUrl = Session["WizardQualificationImgTaxOfficeLetter"].ToString();
            HiddenFieldAccConfirm["TaxOfficeLetter"] = 1;
        }

        if (Session["ImgPeriodImage"] != null)
        {
            imgPeriodImage.ImageUrl = Session["ImgPeriodImage"].ToString();
            HiddenFieldAccConfirm["PeriodImage"] = 1;
        }
        
        if (ValidImpAndMajor())
        {
            PanelflpHSE.ClientVisible =
            lblHSE.ClientVisible = true;
        }
        if (Session["HSEFileURL"] != null)
        {
            HeyperLinkHSEImg.NavigateUrl = Session["HSEFileURL"].ToString();
            HeyperLinkHSEImg.ClientVisible = true;
            HiddenFieldAccConfirm["HSEImg"] = 1;
        }


        System.Collections.ArrayList ExpireDateResult = new System.Collections.ArrayList();
        ExpireDateResult = CheckDocumentExpireDateForTaxLetter();
        if (!Convert.ToBoolean(ExpireDateResult[0]) && !string.IsNullOrWhiteSpace(ExpireDateResult[1].ToString()))
        {
            SetMessage(ExpireDateResult[1].ToString());
            return;
        }
        if (Convert.ToBoolean(ExpireDateResult[0]))
        {
            lblTaxOfficeLetter.Visible = flpTaxOfficeLetter.ClientVisible = lblValidationTaxOfficeLetter.ClientVisible = imgTaxOfficeLetter.ClientVisible = false;
        }

        if (Convert.ToBoolean(ExpireDateResult[0]))
        {
            RoundPanelTaxLetter.Visible = false;
        }

        if (!Convert.ToBoolean(ExpireDateResult[0]) && Convert.ToBoolean(Session["WizardRevivalCivilLicence"]))
        {
            RoundPanelPeriodJoosh.Visible = true;
        }
        else
            RoundPanelPeriodJoosh.Visible = false;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        if (Session["WizardQualificationImgfrontDoc"] == null || Session["WizardQualificationImgBackDoc"] == null)
        {
            SetMessage("هر دو تصویر رو و پشت پروانه اشتغال باید بارگذاری شود.");
            return;
        }
        System.Collections.ArrayList ExpireDateResult = new System.Collections.ArrayList();
        ExpireDateResult = CheckDocumentExpireDateForTaxLetter();
        if (!Convert.ToBoolean(ExpireDateResult[0]) && !string.IsNullOrWhiteSpace(ExpireDateResult[1].ToString()))
        {
            SetMessage(ExpireDateResult[1].ToString());
            return;
        }
        if (!Convert.ToBoolean(ExpireDateResult[0]) && Session["WizardQualificationImgTaxOfficeLetter"] == null)
        {
            SetMessage("بارگذاری تصویر استعلام اداره امور مالیاتی الزامی می باشد.");
            return;
        }
        if (!Convert.ToBoolean(ExpireDateResult[0]) && Convert.ToBoolean(Session["WizardRevivalCivilLicence"]) && Session["ImgPeriodImage"] == null)
        {
            SetMessage("آپلود تصویر گواهینامه جوش الزامی می باشد");
            return;
        }
            if (!CheckExamForImpToNext())
        {
            SetMessage("امکان ثبت زمینه آزمون اجرا بدون ثبت گواهی HSE وجود ندارد. ");
            return;
        }
        #region تعیین صفحه بعد براساس نوع صلاحیت های عضو
        //**************************************************************************************************
        // اگر متقاضی دارای پروانه اشتغال در زمینه نظارت بود و درخواست درج صلاحیت اجرا داشت نیاز به ارائه سابقه کار نیست
        // پس لطفاً صفحه سابقه کار برای این اشخاص نشان داده نشود و
        //در توضیحات روی سایت نیز مساله اعلام شود و برعکس(اگر متقاضی دارای پروانه اشتغال در زمینه اجرا بود و  درخواست درج صلاحیت نظارت داشت نیاز به ارائه سابقه کار نیست.... )
        //**************************************************************************************************
        DataTable dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];
        int cntAll = dtExamDetail.Rows.Count;
        dtExamDetail.DefaultView.RowFilter = "TTypeId =" + ((int)TSP.DataManager.DocTestType.Implement).ToString();

        if (cntAll == dtExamDetail.DefaultView.Count)//**اگر در این درخواست فقط اجرا را وارد کرده باشد
        {
            dtExamDetail.DefaultView.RowFilter = "";
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation).Count > 0)
            {
                //برای وقتی که قبلا نظارت گرفته باشد
                Response.Redirect("WizardQualificationSummary.aspx");
            }
            else
                Response.Redirect("WizardQualificationJobConfirm.aspx");
        }
        else
        {
            dtExamDetail.DefaultView.RowFilter = "TTypeId =" + ((int)TSP.DataManager.DocTestType.Observation).ToString();
            if (cntAll == dtExamDetail.DefaultView.Count)
            {
                dtExamDetail.DefaultView.RowFilter = "";
                TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
                if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Implement).Count > 0)
                {
                    //برای وقتی که قبلا اجرا گرفته باشد
                    Response.Redirect("WizardQualificationSummary.aspx");
                }
                else
                    Response.Redirect("WizardQualificationJobConfirm.aspx");
            }
            else
            {
                dtExamDetail.DefaultView.RowFilter = "";
                Response.Redirect("WizardQualificationJobConfirm.aspx");
            }
        }
        #endregion
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardQualificationExam.aspx");
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

    #region Methods
    /// <summary>
    /// پیرو بازخورد ٩٩/Fe/١١٥ 
    ///  موارد مورد نياز براي آپلود گواهينامه HSE براي اعضاي رشته عمران و معماري
    /// در صورت درخواست درج صلاحيت اجرا در پروانه
    /// </summary>
    /// <returns></returns>
    private bool CheckExamForImpToNext()
    {
        DataTable dtExams = (DataTable)Session["WizardDocQualificationExam"];
        dtExams.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement;
        if (dtExams.DefaultView.Count > 0)
        {
            dtExams.DefaultView.RowFilter += " AND (MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran + " OR MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari + ")";
            if (dtExams.DefaultView.Count > 0 && Session["HseFileURL"] == null)
            {
                dtExams.DefaultView.RowFilter = "";
                return false;
            }
        }
        dtExams.DefaultView.RowFilter = "";
        return true;
    }
    /// <summary>
    /// پیرو بازخورد ٩٩/Fe/١١٥ 
    ///  موارد مورد نياز براي آپلود گواهينامه HSE براي اعضاي رشته عمران و معماري
    /// در صورت درخواست درج صلاحيت اجرا در پروانه
    /// </summary>
    /// <returns></returns>
    private bool ValidImpAndMajor()
    {
        DataTable dtExams = (DataTable)Session["WizardDocQualificationExam"];
        dtExams.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement + " AND (MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran + " OR MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari + ")";
        if (dtExams.DefaultView.Count > 0)
        {
            dtExams.DefaultView.RowFilter = "";
            return true;
        }
        dtExams.DefaultView.RowFilter = "";
        return false;
    }
    private void SetWarningLableDisable()
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void SetMenueImage()
    {
        if (Session["WizardDocQualificationOath"] != null && (Boolean)Session["WizardDocQualificationOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocQualificationExam"] != null && ((DataTable)Session["WizardDocQualificationExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardQualificationJobConfirm"] != null && ((DataTable)Session["WizardQualificationJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocQualificationSummary"] != null && (Boolean)Session["WizardDocQualificationSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocQualificationExam"] == null && Session["WizardDocQualificationSummary"] == null && Session["WizardDocQualificationOath"] == null
         && Session["WizardQualificationJobConfirm"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocQualificationOath"] == null)
        {
            SetMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        string tempFileName = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId_" + Utility.GetCurrentUser_MeId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/DocMeFile/DocMeFile/OldDoc/") + ret) == true
            || (id == "flpHSE" && File.Exists(MapPath("~/image/DocMeFile/HSE/") + ret) == true));

            if (id == "flpFrontDoc")
            {
                Session["WizardQualificationImgfrontDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpBackDoc")
            {
                Session["WizardQualificationImgBackDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpTaxOfficeLetter")
            {
                Session["WizardQualificationImgTaxOfficeLetter"] = "~/Image/DocMeFile/TaxOffice/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/TaxOffice/") + ret;
            }
            else if (id == "flplblPeriodImage")
            {
                Session["ImgPeriodImage"] = "~/Image/DocMeFile/JooshPeriod/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/JooshPeriod/") + ret;
            }
            else if (id == "flpHSE")
            {
                tempFileName = MapPath("~/image/DocMeFile/HSE/") + ret;
                Session["HSEFileURL"] = "~/image/DocMeFile/HSE/" + ret;
            }

            uploadedFile.SaveAs(tempFileName, true);


        }
        return ret;
    }

    private System.Collections.ArrayList CheckDocumentExpireDateForTaxLetter()
    {
        System.Collections.ArrayList ReturnValue = new System.Collections.ArrayList();
        ReturnValue.Add(true);
        ReturnValue.Add("");
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
        if (DocMemberFileManager.Count != 1)
        {
            ReturnValue[0] = false;
            ReturnValue[1] = "آخرین درخواست تایید شده یافت نشد.";
        }
        if (Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
        {
            ReturnValue[0] = false;
            ReturnValue[1] = "تاریخ اعتبار پروانه شما مشخص نمی باشد.";
            return ReturnValue;
        }
        string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
        Utility.Date objDate = new Utility.Date(CrtEndDate);
        string LastMonth = objDate.AddMonths(-2);
        string Today = Utility.GetDateOfToday();
        int IsDocExp = string.Compare(Today, LastMonth);
        if (IsDocExp <= 0)
        {
            ReturnValue[0] = true;
            ReturnValue[1] = "تاریخ اعتبار به پایان نرسیده است.از دو ماه قبل از به پایان رسیدن اعتبار پروانه نیاز به ثبت نامه تسویه حساب امور مالیاتی می باشد.";
            return ReturnValue;
        }

        ReturnValue[0] = false;
        ReturnValue[1] = "";
        return ReturnValue;
    }

    private void HasCivilLicence()
    {
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(Utility.GetCurrentUser_MeId(), 0);
        if (dtMemberFileMajor.Rows.Count == 0)
            return;
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
             + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {
            Session["WizardRevivalCivilLicence"] = true;
        }
        else
        {
            Session["WizardRevivalCivilLicence"] = false;
        }
        dtMemberFileMajor.DefaultView.RowFilter = "";        
    }


    #endregion
}