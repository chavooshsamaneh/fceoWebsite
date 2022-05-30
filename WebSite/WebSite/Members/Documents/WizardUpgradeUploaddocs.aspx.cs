using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;

public partial class Members_Documents_WizardUpgradeUploaddocs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();

        if (!IsPostBack)
        {
            HiddenFieldAccConfirm["Conf1"] =
                HiddenFieldAccConfirm["Conf2"] =0;

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
            if (dtDocMeFile.Rows.Count <= 0)
            {
                SetMessage("بدلیل نداشتن پروانه تایید شده امکان ثبت درخواست تمدید جدید وجود ندارد.");
                return;
            }            

            if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["PeriodImageURL"]) && Session["PeriodImageURL"] == null)
            {
                Session["ImgPeriodImage"] = imgPeriodImage.ImageUrl = dtDocMeFile.Rows[0]["PeriodImageURL"].ToString();
            }
            if (ValidImpAndMajor())
            {
                PanelflpHSE.ClientVisible =
                lblHse.ClientVisible = true;
            }
            else
            {
                PanelflpHSE.ClientVisible =
               lblHse.ClientVisible = false;
            }
            if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["ImgHSEURL"]) && Session["HseFileURL"] == null)
            {
                Session["HseFileURL"] = hpImgHse.ImageUrl = dtDocMeFile.Rows[0]["ImgHSEURL"].ToString();
            }
            SetMenueImage();
            HasCivilLicence(); 

        }        
        if (Session["WizardUpgradeImgFrontDoc"] != null)
        {
            imgFrontDoc.ImageUrl = Session["WizardUpgradeImgFrontDoc"].ToString();
            HiddenFieldAccConfirm["FrontDoc"] = 1;
        }
        if (Session["WizardUpgradeImgBackDoc"] != null)
        {
            imgBackDoc.ImageUrl = Session["WizardUpgradeImgBackDoc"].ToString();
            HiddenFieldAccConfirm["backDoc"] = 1;
        }
        if (Session["ImgPeriodImage"] != null)
        {
            imgPeriodImage.ImageUrl = Session["ImgPeriodImage"].ToString();
            HiddenFieldAccConfirm["PeriodImage"] = 1;
        }
        if (Session["HseFileURL"] != null)
        {
            hpImgHse.ImageUrl = Session["HseFileURL"].ToString();
            HiddenFieldAccConfirm["Hse"] = 1;
        }
        if (Session["WizardUpgradeImgTaxOfficeLetter"] != null)
        {
            imgTaxOfficeLetter.ImageUrl = Session["WizardUpgradeImgTaxOfficeLetter"].ToString();
            HiddenFieldAccConfirm["TaxOfficeLetter"] = 1;
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
            RoundPanelKardConfirm.Visible = false;
        }
        if (!Convert.ToBoolean(Session["WizardUpgradeCivilLicence"]))
        {
            lblWarningPeriod.Visible = lblPeriodImage.Visible =
              flplblPeriodImage.Visible =
              imgEndPeriodImage.ClientVisible =
              imgPeriodImage.ClientVisible =
              lblValidationPeriodImage.ClientVisible = false;
        }
        if (!Convert.ToBoolean(Session["WizardUpgradeArchitectLicence"]) && !Convert.ToBoolean(Session["WizardUpgradeCivilLicence"]))
        {
            lblValidationHse.ClientVisible =
            ImgEndHse.ClientVisible =
            hpImgHse.ClientVisible =
            lblHse.Visible =
            flpHse.Visible = false;

        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Session["WizardUpgradeImgfrontDoc"] == null || Session["WizardUpgradeImgBackDoc"] == null)
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
        if (!Convert.ToBoolean(ExpireDateResult[0]) && Session["WizardUpgradeImgTaxOfficeLetter"] == null)
        {
            SetMessage("بارگذاری تصویر استعلام اداره امور مالیاتی الزامی می باشد.");
            return;
        }
        if (ValidImpAndMajor() && Session["HseFileURL"] == null)
        {
            SetMessage("بدلیل دارا بودن صلاحیت اجرا آپلود گواهی  HSE اجباری می باشد . ");
            return;
        }
        if (Convert.ToBoolean(ExpireDateResult[0]) && Convert.ToBoolean(Session["WizardRevivalCivilLicence"]) && Session["ImgPeriodImage"] == null)
        {
            SetMessage("آپلود تصویر گواهینامه جوش الزامی می باشد");
            return;
        }
        Response.Redirect("WizardUpgradeJobConfirm.aspx");

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
    ///در صورت ارتقاء پروانه اي كه صلاحيت اجرا در آن ثبت شده
    /// </summary>
    /// <returns></returns>
    private bool ValidImpAndMajor()
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Implement).Count > 0)
        {
            return true;
        }
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
        if (Session["WizardDocUpgradeOath"] != null && (Boolean)Session["WizardDocUpgradeOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardUpgradeJobConfirm"] != null && ((DataTable)Session["WizardUpgradeJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocUpgradeSummary"] != null && (Boolean)Session["WizardDocUpgradeSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardUpgradeImgfrontDoc"] != null)
        {
            MenuSteps.Items.FindByName("Documents").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Documents").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Documents").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocUpgradeSummary"] == null && Session["WizardDocUpgradeOath"] == null
         && Session["WizardUpgradeJobConfirm"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocUpgradeOath"] == null)
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

            } while ( File.Exists(MapPath("~/Image/DocMeFile/DocMeFile/OldDoc/") + ret) == true);            
            if (id == "flpFrontDoc")
            {
                Session["WizardUpgradeImgfrontDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpBackDoc")
            {
                Session["WizardUpgradeImgBackDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpTaxOfficeLetter")
            {
                Session["WizardUpgradeImgTaxOfficeLetter"] = "~/Image/DocMeFile/TaxOffice/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/TaxOffice/") + ret;
                Session["chbIAgree"] = true;
            }
            else if (id == "flplblPeriodImage")
            {
                Session["ImgPeriodImage"] = "~/Image/DocMeFile/JooshPeriod/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/JooshPeriod/") + ret;
            }
            else if (id == "flpHse")
            {
                Session["HseFileURL"] = "~/image/DocMeFile/Hse/" + ret;
                tempFileName = MapPath("~/image/DocMeFile/Hse/") + ret;
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
            Session["WizardUpgradeCivilLicence"] = true;
        }
        else
        {
            Session["WizardUpgradeCivilLicence"] = false;
        }
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
   + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Architecture + "  or  " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {

            Session["WizardUpgradeArchitectLicence"] = true;

        }
        else
        {
            Session["WizardUpgradeArchitectLicence"] = false;
        }
        dtMemberFileMajor.DefaultView.RowFilter = "";
        //}
    }

    #endregion
}