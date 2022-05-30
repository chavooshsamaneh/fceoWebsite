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

public partial class NezamRegister_WizardOfficePrint : System.Web.UI.Page
{
    DataTable dtOffice = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = -1;
        string Pass = "";
        String Code = "";

        try
        {

            UserId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["UId"].ToString())));
            Pass = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["P"].ToString()));
            Code = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["C"].ToString()));
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        try
        {
            if (UserId == -1)
                return;

            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(UserId);
            if (LoginManager.Count == 0)
                return;

            int OfId = Convert.ToInt32(LoginManager[0]["MeId"]);
            TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
            OfficeManager.FindByCode(OfId);
            if (OfficeManager.Count > 0)
            {
                dtOffice = OfficeManager.DataTable;

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

                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["SignUrl"]))
                        imgOfSigna.ImageUrl = dtOffice.Rows[0]["SignUrl"].ToString();

                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["ArmUrl"]))
                        imgOfArma.ImageUrl = dtOffice.Rows[0]["ArmUrl"].ToString();

                    //if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OatName"]))
                    //    ASofattype.Text = dtOffice.Rows[0]["OatName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OtName"]))
                        ASOfType.Text = dtOffice.Rows[0]["OtName"].ToString();



                    ASPassword.Text = Pass;

                    ASUserName.Text = LoginManager[0]["UserName"].ToString();

                }
            }


            TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
            AgentManager.FindByOfCode(OfId);
            GvAgents.DataSource = AgentManager.DataTable;
            GvAgents.DataBind();

            TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();
            LetterManager.FindByOfCode(OfId);
            GvLetters.DataSource = LetterManager.DataTable;
            GvLetters.DataBind();

            TSP.DataManager.OfficeMemberManager ofMeManager = new TSP.DataManager.OfficeMemberManager();
            ofMeManager.FindByOfficeCode(OfId);
            GvMembers.DataSource = ofMeManager.DataTable;
            GvMembers.DataBind();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

}
