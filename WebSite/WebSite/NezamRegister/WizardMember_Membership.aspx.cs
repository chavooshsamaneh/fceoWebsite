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

public partial class NezamRegister_WizardMember_Membership : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }
        ASPxMenu1.Items.FindByName("Membership").Selected = true;

        if (Session["MemberMembership"] != null && (Boolean)Session["MemberMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Member"] != null && ((DataTable)Session["Member"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMadrak"] != null && ((DataTable)Session["TblOfMadrak"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Madrak").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Madrak").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Madrak").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblJob"] != null && ((DataTable)Session["TblJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblActivity"] != null && Session["TblActivity2"] != null && ((DataTable)Session["TblActivity"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Activity").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Activity").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblLanguage"] != null && ((DataTable)Session["TblLanguage"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Language").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Language").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Language").Image.Height = Unit.Pixel(15);
        }
        if (Session["MemberSummary"] != null && (Boolean)Session["MemberSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if (IsPostBack == false)
        {
            lblImg.Text = "* تصویر انتخابی بایستی :";
            lblImg.Text += "<ul><li>رنگی، پشت سفید، بدون کادر و نوشته باشد باشد(برای خانم ها عکس با مقنعه و حجاب کامل و برای آقایان با لباس رسمی، بدون کراوات و کلاه باشد)</li>";
            lblImg.Text += "<li>حجم فایل تصویر بایستی حداکثر " + ((new TSP.WebControls.CustomAspxUploadControl()).MaxSizeForUploadFile / 1000) + "KB باشد</li>";
            lblImg.Text += "<li>در اندازه " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi باشد" + "</li>";
            lblImg.Text += "<li>طی یکسال گذشته گرفته شده باشد</li></ul>";

            decimal MemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.FirstMembershipCost);
            decimal YearMemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.YearlyMembershipCost);
            decimal Total = MemberShipCost + YearMemberShipCost;
            lblMemberShipCost.Text = "پرداخت مبلغ " + Total.ToString("#,#") + " ریال به حساب بانک تجارت شعبه نظام مهندسی";
        }
    }

    Boolean CheckSecurityCode()
    {
        //if (panelSecurityCode.Visible == true)
        //{
            return Captcha.IsValid; //TSP.WebControls.CustomASPxCaptcha.Check(Captcha.Code, txtSecurityCode.Text);
        //}
        //else
        //    return true;
    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='../Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
        //panelSecurityCode.Visible = true;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (chbIAgree.Checked == false)
            ShowInputError("موارد اعلام شده مورد موافقت قرار نگرفته است");
        else if (CheckSecurityCode() == false)
            ShowInputError("کد امنیتی وارد شده اشتباه است");
        else
        {
            Session["MemberMembership"] = true;
            Response.Redirect("WizardMember.aspx");
        }
        //txtSecurityCode.Text = "";
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberRegister).ToString());
    }

}
