using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;
using System.Collections;

public partial class Members_Documents_WizardAccConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();
        if (!IsPostBack)
        {
             HiddenFieldAccConfirm["chbIAgree"] = HiddenFieldAccConfirm["HSEImg"] = 0;
            SetMenueImage();
            if (Session["chbIAgree"] != null)
            {
                if (Convert.ToBoolean(Session["chbIAgree"]))
                {
                    HiddenFieldAccConfirm["chbIAgree"] = 1;
                }
            }
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

        if (Session["ImgTaxOfficeLetter"] != null)
        {
            imgTaxOfficeLetter.ImageUrl = Session["ImgTaxOfficeLetter"].ToString();
            HiddenFieldAccConfirm["TaxOfficeLetter"] = 1;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(Session["ImgTaxOfficeLetter"]))// && !chbIAgree.Checked)
        {
            SetMessage("بارگذاری تصویر استعلام اداره امور مالیاتی الزامی می باشد.");
            return;
        }

        if (Session["WizardDocExam"] == null)
        {
            SetMessage("اطلاعات آزمون ثبت نشده است.");
            return;
        }
        if (Session["WizardDocExam"] != null && !(((DataTable)Session["WizardDocExam"]).Rows.Count > 0))
        {
            SetMessage("اطلاعات آزمون ثبت نشده است. ورود اطلاعات آزمون اجباری می باشد");
            return;
        }

        if (!CheckExamForImpToNext())
        {
            SetMessage("امکان ثبت زمینه آزمون اجرا بدون ثبت گواهی HSE وجود ندارد. ");
            return;
        }

        Response.Redirect("WizardDocJobConfirm.aspx");
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardDocExam.aspx");
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
    private bool CheckExamForImpToNext()
    {
        DataTable dtExams = (DataTable)Session["WizardDocExam"];
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
    private bool ValidImpAndMajor()
    {
        DataTable dtExams = (DataTable)Session["WizardDocExam"];
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
        if (Session["WizardDocOath"] != null && (Boolean)Session["WizardDocOath"] == true)
        {
            ASPxMenu1.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["HseFileURL"] != null || (Session["ImgTaxOfficeLetter"] != null || CbIAgree()))
        {
            ASPxMenu1.Items.FindByName("AccConfirm").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("AccConfirm").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("AccConfirm").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocExam"] != null && ((DataTable)Session["WizardDocExam"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocJob"] != null && ((DataTable)Session["WizardDocJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocJobConfirm"] != null && ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocSummary"] != null && (Boolean)Session["WizardDocSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocExam"] == null && Session["WizardDocOath"] == null
        && Session["WizardDocJobConfirm"] == null && Session["WizardDocSummary"] == null
            //&& Session["WizardDocMajor"] == null && Session["WizardDocJob"] == null
            // && Session["WizardDocResposblity"] == null && Session["WizardDocPeriods"] == null
            )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocOath"] == null)
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
            } while ((id == "flpTaxOfficeLetter" && File.Exists(MapPath("~/image/DocMeFile/TaxOffice/") + ret) == true)
                || (id == "flpHSE" && File.Exists(MapPath("~/image/DocMeFile/HSE/") + ret) == true)
                );

            if (id == "flpTaxOfficeLetter")
            {
                Session["ImgTaxOfficeLetter"] = "~/Image/DocMeFile/TaxOffice/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/TaxOffice/") + ret;
                Session["chbIAgree"] = true;
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

    private Boolean CbIAgree()
    {
        if (Session["chbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["chbIAgree"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #endregion
}