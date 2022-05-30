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

public partial class NezamRegister_WizardOffice_Membership : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxMenu1.Items.FindByName("Membership").Selected = true;

        if (Session["OfficeMembership"] != null && (Boolean)Session["OfficeMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Office"] != null && ((DataTable)Session["Office"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Office").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Office").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Office").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfAgent"] != null && ((DataTable)Session["TblOfAgent"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Agent").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Agent").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Agent").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfLetter"] != null && ((DataTable)Session["TblOfLetter"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Letter").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Letter").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Letter").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMember"] != null && ((DataTable)Session["TblOfMember"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfJob"] != null && ((DataTable)Session["TblOfJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["OfficeSummary"] != null && (Boolean)Session["OfficeSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if (!IsPostBack)
        {
            string MemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice).ToString();
            lblMemberShipCost.Text = "فیش بانکی مربوط به پرداخت ورودیه به مبلغ " + MemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی";

            string YearMemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice).ToString();
            lblYearMemberShipCost.Text = "فیش بانکی مربوط به پرداخت حق عضویت سالانه به مبلغ " + YearMemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی";
        }
    }

    Boolean CheckSecurityCode()
    {
        return Captcha.IsValid;
    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='../Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
        //  panelSecurityCode.Visible = true;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (chbIAgree.Checked == false)
            ShowInputError("موارد اعلام شده مورد موافقت قرار نگرفته است");
        else if (CheckSecurityCode() == false)
            ShowInputError("کد امنیتی وارد شده اشتباه است");
        else
        {
            Session["OfficeMembership"] = true;
            Response.Redirect("WizardOffice.aspx");
        }
        // txtSecurityCode.Text = "";
    }
}
