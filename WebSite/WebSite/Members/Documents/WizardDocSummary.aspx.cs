using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class Members_Documents_WizardDocSummary : System.Web.UI.Page
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
        Session["WizardDocSummary"] = true;
        Response.Redirect("WizardDocFinish.aspx?PgMode=" + Utility.EncryptQS("EPayment"));
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardDocJobConfirm.aspx");
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
        if (Session["WizardDocOath"] != null && (Boolean)Session["WizardDocOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
 
        if (Session["WizardDocExam"] != null && ((DataTable)Session["WizardDocExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["HseFileURL"] != null || (Session["ImgTaxOfficeLetter"] != null || CbIAgree()))
        {
            MenuSteps.Items.FindByName("AccConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("AccConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("AccConfirm").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocJobConfirm"] != null && ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }
     
        if (Session["WizardDocJob"] != null && ((DataTable)Session["WizardDocJob"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
 
        if (Session["WizardDocSummary"] != null && (Boolean)Session["WizardDocSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
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
            if (Session["ImgTaxOfficeLetter"] != null)
            {
                HpTaxOfficeLetter.NavigateUrl = Session["ImgTaxOfficeLetter"].ToString();
                HpTaxOfficeLetter.ClientVisible = true;
                lblTaxOfficeLetter.ClientVisible = true;
            }
            else
            {
                HpTaxOfficeLetter.ClientVisible = false;
                lblTaxOfficeLetter.ClientVisible = false;

            }

            if (CbIAgree() && Session["ImgTaxOfficeLetter"] == null)
            {
                lblTaxOfficeLetterOath.ClientVisible = true;
            }
            else
            {
                lblTaxOfficeLetterOath.ClientVisible = false;
            }

            if (Session["HSEFileURL"] != null)
            {
                HpHSEImg.NavigateUrl = Session["HSEFileURL"].ToString();
                HpHSEImg.ClientVisible = true;
                lblHSE.ClientVisible = true;
            }
            else
            {
                HpHSEImg.ClientVisible = false;
                lblHSE.ClientVisible = false;
            }


            FillMembershipInfo();

            BindExamGrid();

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


    private void BindExamGrid()
    {
        if (Session["WizardDocExam"] != null)
        {
            GridViewExam.DataSource = (DataTable)Session["WizardDocExam"];
            GridViewExam.DataBind();
        }
    }

    private void BindJobConfirmGrid()
    {
        if (Session["WizardDocJobConfirm"] != null)
        {
            GrdvJobCon.DataSource = (DataTable)Session["WizardDocJobConfirm"];
            GrdvJobCon.DataBind();
        }
    }

    private void BindMajorGrid()
    {
        if (Session["WizardDocMajor"] != null)
        {
          
        }
    }

    private void BindJobGrid()
    {
        if (Session["WizardDocJob"] != null)
        {
            GrdvJob.DataSource = (DataTable)Session["WizardDocJob"];
            GrdvJob.DataBind();
        }
    }

    private void BindResponsblityGrid()
    {
        if (Session["WizardDocResposblity"] != null)
        {
            
        }
    }

    private void BindPeriodsGrid()
    {
       
    }

    private Boolean CheckTsTimeOut()
    {

        if (Session["WizardDocExam"] == null && Session["WizardDocOath"] == null
            && Session["ACCFileURL"] == null && Session["WizardDocJobConfirm"] == null && Session["WizardDocSummary"] == null
          
           )
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocOath"] == null)
        {
            ShowMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
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