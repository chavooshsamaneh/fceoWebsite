using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardQualificationSummary : System.Web.UI.Page
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
        Session["WizardDocQualificationSummary"] = true;
        Response.Redirect("WizardQualificationFinish.aspx?PgMode=" + Utility.EncryptQS("EPayment"));
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {

        if (Session["WizardDocQualificationExam"] == null)
        {
            Response.Redirect("WizardQualificationExam.aspx");
            return;
        }
        DataTable dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];
        // if (dtExamDetail.Rows.Count == 1 && Convert.ToInt32(dtExamDetail.Rows[0]["TTypeId"]) == (int)TSP.DataManager.DocTestType.Implement)
        int cntAll = dtExamDetail.Rows.Count;
        dtExamDetail.DefaultView.RowFilter = "TTypeId =" + ((int)TSP.DataManager.DocTestType.Implement).ToString();
        if (cntAll == dtExamDetail.DefaultView.Count)
        {
            dtExamDetail.DefaultView.RowFilter = "";
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation).Count > 0)
            {
                //برای وقتی که قبلا نظارت گرفته باشد
                Response.Redirect("WizardQualificationKardan.aspx");
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
                    Response.Redirect("WizardQualificationKardan.aspx");
                }
                else
                    Response.Redirect("WizardQualificationJobConfirm.aspx");
            }
            else
                Response.Redirect("WizardQualificationJobConfirm.aspx");
        }

        Response.Redirect("WizardQualificationJobConfirm.aspx");
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

        if ( Session["WizardQualificationImgfrontDoc"] != null)
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
        if (Session["WizardDocQualificationExam"] != null)
        {
            GridViewExam.DataSource = (DataTable)Session["WizardDocQualificationExam"];
            GridViewExam.DataBind();
        }
    }

    private void BindJobConfirmGrid()
    {
        if (Session["WizardQualificationJobConfirm"] != null)
        {
            GrdvJobCon.DataSource = (DataTable)Session["WizardQualificationJobConfirm"];
            GrdvJobCon.DataBind();
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocQualificationExam"] == null && Session["WizardDocQualificationSummary"] == null && Session["WizardDocQualificationOath"] == null
         && Session["WizardQualificationJobConfirm"] == null)
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocQualificationOath"] == null)
        {
            ShowMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private Boolean CbIAgree()
    {
        if (Session["WizardQualificationchbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["WizardQualificationchbIAgree"]))
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