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
using System.IO;

public partial class NezamRegister_WizardOfficeSummary : System.Web.UI.Page
{
    DataTable dtOffice = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        ASPxMenu1.Items.FindByName("Summary").Selected = true;

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

        if (Session["Office"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مدت زمان اعتبار صفحه به پایان رسیده است.مجدداً اقدام نمایید";
            this.btnNext.Enabled = false;
            return;
        }
        if (!IsPostBack)
        {
            if (Session["Office"] != null)
            {
                dtOffice = (DataTable)Session["Office"];

                if (dtOffice.Rows.Count != 0)
                {
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Address"]))
                        ASofaddr.Text = dtOffice.Rows[0]["Address"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Description"]))
                        ASofdesc.Text = dtOffice.Rows[0]["Description"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Email"]))
                        ASofemail.Text = dtOffice.Rows[0]["Email"].ToString();
                    //if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["FileNo"]))
                    //    ASoffileno.Text = dtOffice.Rows[0]["FileNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["MobileNo"]))
                        ASofmobileno.Text = dtOffice.Rows[0]["MobileNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OfName"]))
                        ASOfName.Text = dtOffice.Rows[0]["OfName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OfNameEn"]))
                        ASOfNameEn.Text = dtOffice.Rows[0]["OfNameEn"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["RegDate"]))
                        ASofrregdate.Text = dtOffice.Rows[0]["RegDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["RegOfNo"]))
                        ASregno.Text = dtOffice.Rows[0]["RegOfNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["RegPlace"]))
                        ASofregplace.Text = dtOffice.Rows[0]["RegPlace"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Stock"]))
                        ASofstock.Text = dtOffice.Rows[0]["Stock"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Subject"]))
                        ASofsubject.Text = dtOffice.Rows[0]["Subject"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["VolumeInvest"]))
                        ASofvalue.Text = dtOffice.Rows[0]["VolumeInvest"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Website"]))
                        ASofwebsite.Text = dtOffice.Rows[0]["Website"].ToString();

                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Tel1"]))
                        ASoftel1.Text = dtOffice.Rows[0]["Tel1"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Tel2"]))
                        ASoftel2.Text = dtOffice.Rows[0]["Tel2"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["Fax"]))
                        ASoffax.Text = dtOffice.Rows[0]["Fax"].ToString();

                    if (Session["fileOfSign"] != null)
                        imgOfSigna.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["fileOfSign"].ToString());

                    if (Session["fileOfArm"] != null)
                        imgOfArma.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["fileOfArm"].ToString());


                    //if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OatName"]))
                    //    ASofattype.Text = dtOffice.Rows[0]["OatName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OtName"]))
                        ASOfType.Text = dtOffice.Rows[0]["OtName"].ToString();

                }
            }
            GvAgents.DataSource = (DataTable)Session["TblOfAgent"];
            GvAgents.DataBind();
            GvLetters.DataSource = (DataTable)Session["TblOfLetter"];
            GvLetters.DataBind();
            GvMembers.DataSource = (DataTable)Session["TblOfMember"];
            GvMembers.DataBind();
            GrdvJob.DataSource = (DataTable)Session["TblOfJob"];
            GrdvJob.DataBind();


        }
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeLetters.aspx");

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!ChbCheck.Checked)
            lblCheck.Visible = true;
        else
        {
            Session["OfficeSummary"] = true;
            Response.Redirect("WizardOfficeFinish.aspx");
        }
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeSummary).ToString());
    }
}
