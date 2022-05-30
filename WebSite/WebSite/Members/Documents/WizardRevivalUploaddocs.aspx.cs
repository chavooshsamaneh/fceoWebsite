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

public partial class Members_Documents_WizardRevivalUploaddocs : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();

        if (!IsPostBack)
        {
            HiddenFieldAccConfirm["Conf1"] =
                HiddenFieldAccConfirm["Conf2"] = 0;

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
            if (dtDocMeFile.Rows.Count <= 0)
            {
                SetMessage("بدلیل نداشتن پروانه تایید شده امکان ثبت درخواست تمدید جدید وجود ندارد.");
                return;
            }
            HasCivilLicence();
            if (Convert.ToBoolean(dtDocMeFile.Rows[0]["IsTemporary"]))
            {
                HiddenFieldAccConfirm["IsTemporary"] = 1;
            }
            else
            {
                HiddenFieldAccConfirm["IsTemporary"] = 0;
            }
            SetMenueImage();

        }
        if (Session["WizardRevivalImgFrontDoc"] != null)
        {
            imgFrontDoc.ImageUrl = Session["WizardRevivalImgFrontDoc"].ToString();
            HiddenFieldAccConfirm["FrontDoc"] = 1;
        }
        if (Session["WizardRevivalImgBackDoc"] != null)
        {
            imgBackDoc.ImageUrl = Session["WizardRevivalImgBackDoc"].ToString();
            HiddenFieldAccConfirm["backDoc"] = 1;
        }

        if (Session["ImgPeriodImage"] != null)
        {
            imgPeriodImage.ImageUrl = Session["ImgPeriodImage"].ToString();
            HiddenFieldAccConfirm["PeriodImage"] = 1;
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
        if (Session["HseFileURL"] != null)
        {
            hpImgHse.ImageUrl = Session["HseFileURL"].ToString();
            HiddenFieldAccConfirm["Hse"] = 1;
        }
        if (Session["WizardRevivalImgTaxOfficeLetter"] != null)
        {
            imgTaxOfficeLetter.ImageUrl = Session["WizardRevivalImgTaxOfficeLetter"].ToString();
            HiddenFieldAccConfirm["TaxOfficeLetter"] = 1;
        }

        if (!Convert.ToBoolean(Session["WizardRevivalCivilLicence"]))
        {
            lblWarningPeriod.Visible = lblPeriodImage.Visible =
              flplblPeriodImage.Visible =
              imgEndPeriodImage.ClientVisible =
              imgPeriodImage.ClientVisible =
              lblValidationPeriodImage.ClientVisible = false;


        }
        if (!Convert.ToBoolean(Session["WizardRevivalArchitectLicence"]) && !Convert.ToBoolean(Session["WizardRevivalCivilLicence"]))
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

        if (Session["WizardRevivalImgfrontDoc"] == null || Session["WizardRevivalImgBackDoc"] == null)
        {
            SetMessage("هر دو تصویر رو و پشت پروانه اشتغال باید بارگذاری شود.");
            return;
        }
        if (Session["WizardRevivalImgTaxOfficeLetter"] == null)
        {
            SetMessage("بارگذاری تصویر استعلام اداره امور مالیاتی الزامی می باشد.");
            return;
        }
        if (ValidImpAndMajor() && Session["HseFileURL"] == null)
        {
            SetMessage("ثبت گواهی HSE جهت تمدید پروانه دارای صلاحیت اجرا اجباری می باشد . ");
            return;
        }
        if (!Convert.ToBoolean(HiddenFieldAccConfirm["IsTemporary"]) && Convert.ToBoolean(Session["WizardRevivalCivilLicence"]) && Session["ImgPeriodImage"]==null)
        {
            SetMessage("جهت تمدید پروانه اشتغال رشته عمران آپلود گواهی دوره جوش الزامی می باشد");
            return;
        }
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtRes = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(-1, Utility.GetCurrentUser_MeId(), 0);
        Boolean GoToSummary = false;
        if (dtRes.Rows.Count > 0)
        {
            dtRes.DefaultView.RowFilter = "MaxGradeId=" + ((int)TSP.DataManager.DocumentGrads.Grade1).ToString();
            if (dtRes.DefaultView.Count > 0)
                GoToSummary = true;
        }
        if (Convert.ToBoolean(HiddenFieldAccConfirm["IsTemporary"]))
            GoToSummary = true;
        if (GoToSummary)
        {
            Response.Redirect("WizardrevivalDocSummary.aspx");
        }
        else
            Response.Redirect("WizardRevivalJobConfirm.aspx");

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
    ///در صورت تمديد پروانه اي كه صلاحيت اجرا در آن ثبت شده
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
        if (Session["WizardDocRevivalOath"] != null && (Boolean)Session["WizardDocRevivalOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocRevivalExam"] != null && ((DataTable)Session["WizardDocRevivalExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardRevivalJobConfirm"] != null && ((DataTable)Session["WizardRevivalJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocRevivalSummary"] != null && (Boolean)Session["WizardDocRevivalSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardRevivalImgfrontDoc"] != null)
        {
            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocRevivalSummary"] == null && Session["WizardDocRevivalOath"] == null
         && Session["WizardRevivalJobConfirm"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocRevivalOath"] == null)
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

            } while (File.Exists(MapPath("~/Image/DocMeFile/DocMeFile/OldDoc/") + ret) == true);

            if (id == "flpFrontDoc")
            {
                Session["WizardRevivalImgfrontDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpBackDoc")
            {
                Session["WizardRevivalImgBackDoc"] = "~/Image/DocMeFile/OldDoc/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OldDoc/") + ret;
            }
            else if (id == "flpTaxOfficeLetter")
            {
                Session["WizardRevivalImgTaxOfficeLetter"] = "~/Image/DocMeFile/TaxOffice/" + ret;
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
        dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
   + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Architecture + "  or  " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
        if (dtMemberFileMajor.DefaultView.Count > 0)
        {

            Session["WizardRevivalArchitectLicence"] = true;

        }
        else
        {
            Session["WizardRevivalArchitectLicence"] = false;
        }
        dtMemberFileMajor.DefaultView.RowFilter = "";
        //}
    }

    #endregion
}