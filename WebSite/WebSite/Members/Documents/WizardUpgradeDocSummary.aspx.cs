using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardUpgradeDocSummary : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();
        if (!IsPostBack)
        {
            SetMenueImage();
            FillInfo();
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Session["WizardDocUpgradeSummary"] = true;
        Response.Redirect("WizardUpgradeDocFinish.aspx?PgMode=" + Utility.EncryptQS("EPayment"));
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        //TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        //DataTable dtRes = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(-1, Utility.GetCurrentUser_MeId(), 0);
        //Boolean GoTouploadDoc = false;
        //if (dtRes.Rows.Count > 0)
        //{
        //    dtRes.DefaultView.RowFilter = "MaxGradeId=" + ((int)TSP.DataManager.DocumentGrads.Grade1).ToString();
        //    if (dtRes.DefaultView.Count > 0)
        //        GoTouploadDoc = true;
        //}

        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
        //if (dtDocMeFile.Rows.Count <= 0)
        //{
        //    ShowMessage("بدلیل نداشتن پروانه تایید شده امکان ثبت درخواست ارتقاء پایه جدید وجود ندارد.");
        //    return;
        //}
        //if (Convert.ToBoolean(dtDocMeFile.Rows[0]["IsTemporary"]))
        //{
        //    GoTouploadDoc = true;
        //}

        //if (GoTouploadDoc)
        //{
        //    Response.Redirect("WizardUpgradeUploaddocs.aspx");
        //}
        //else
        Response.Redirect("WizardUpgradeJobConfirm.aspx");
    }
    #endregion

    #region Methods
    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
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

        if (Session["WizardUpgradeImgfrontDoc"] != null || Session["ImgTaxOfficeLetter"] != null)
        {

            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void FillInfo()
    {
        try
        {
            if (Session["WizardUpgradeImgTaxOfficeLetter"] != null)
            {
                HpTaxOfficeLetter.NavigateUrl = Session["WizardUpgradeImgTaxOfficeLetter"].ToString();
                HpTaxOfficeLetter.ClientVisible = true;
                lblTaxOfficeLetter.ClientVisible = true;
            }
            else
            {
                HpTaxOfficeLetter.ClientVisible = false;
                lblTaxOfficeLetter.ClientVisible = false;
            }

            if (Session["WizardUpgradeImgFrontDoc"] != null)
            {
                linkImgFrontDoc.NavigateUrl = Session["WizardUpgradeImgFrontDoc"].ToString();
                linkImgFrontDoc.ClientVisible =
                lblImgFrontDoc.ClientVisible = true;
            }
            else
            {
                linkImgFrontDoc.ClientVisible =
             lblImgFrontDoc.ClientVisible = false;
            }
            if (Session["WizardUpgradeImgBackDoc"] != null)
            {
                linkImBackDoc.NavigateUrl = Session["WizardUpgradeImgBackDoc"].ToString();
                linkImgFrontDoc.ClientVisible =
                lblImgBackDoc.ClientVisible = true;
            }
            else
            {
                linkImBackDoc.ClientVisible =
             lblImgBackDoc.ClientVisible = false;
            }

            if (Session["ImgPeriodImage"] != null)
            {
                HpPeriodImage.NavigateUrl = Session["ImgPeriodImage"].ToString();
                HpPeriodImage.ClientVisible = true;
                lblPeriodImage.ClientVisible = true;
            }
            else
            {
                HpPeriodImage.ClientVisible = false;
                lblPeriodImage.ClientVisible = false;
            }
            if (Session["HseFileURL"] != null)
            {
                HpHseImage.NavigateUrl = Session["HseFileURL"].ToString();
                HpHseImage.ClientVisible = true;
                lblHseImage.ClientVisible = true;
            }
            else
            {
                HpHseImage.ClientVisible = false;
                lblHseImage.ClientVisible = false;
            }

            FillMembershipInfo();
            BindJobConfirmGrid();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        }
    }

    private void FillMembershipInfo()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        int MReId = -2;
        int MeId = Utility.GetCurrentUser_MeId();
        ReqManager.FindByMemberId(MeId, 0, 1, -1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
        {
            if (File.Exists(Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
        }

        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
        if (attachManager.Count > 0)
        {
            HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }

        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
        if (attachManager.Count > 0)
        {
            HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }

        if (MemberManager[0]["SexId"].ToString() == "2")
        {
            lblSoldire.ClientVisible = true;
            HpSoldier.ClientVisible = true;
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
            if (attachManager.Count > 0)
            {
                lblSoldire.ClientVisible = true;
                HpSoldier.ClientVisible = true;
                HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
            }
        }
        else
        {
            lblSoldire.ClientVisible = false;
            HpSoldier.ClientVisible = false;
        }

    }

    private void BindJobConfirmGrid()
    {
        if (Session["WizardUpgradeJobConfirm"] != null)
        {
            GrdvJobCon.DataSource = (DataTable)Session["WizardUpgradeJobConfirm"];
            GrdvJobCon.DataBind();
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocUpgradeSummary"] == null && Session["WizardDocUpgradeOath"] == null
         && Session["WizardUpgradeJobConfirm"] == null)
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocUpgradeOath"] == null)
        {
            ShowMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }


    #endregion
}