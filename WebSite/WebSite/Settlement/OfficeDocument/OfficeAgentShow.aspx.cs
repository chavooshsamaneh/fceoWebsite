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

public partial class Settlement_OfficeDocument_OfficeAgentShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                AgentId.Value = Server.HtmlDecode(Request.QueryString["OagId"]).ToString();
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string OagId = Utility.DecryptQS(AgentId.Value);
            string OfId = Utility.DecryptQS(OfficeId.Value);


            if ((string.IsNullOrEmpty(OagId)) || (string.IsNullOrEmpty(OfId)))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;

            }
            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();

            FillForm(int.Parse(OagId));
        }
    }
    protected void FillForm(int OagId)
    {
        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        AgentManager.FindByCode(OagId);
        if (AgentManager.Count == 1)
        {

            txtOfAgName.Text = AgentManager[0]["OagName"].ToString();

            if (AgentManager[0]["Tel"].ToString() != "")
            {
                if (AgentManager[0]["Tel"].ToString().IndexOf("-") > 0)
                {
                    txtOfAgTel_pre.Text = AgentManager[0]["Tel"].ToString().Substring(0, AgentManager[0]["Tel"].ToString().IndexOf("-"));
                    txtOfAgTel.Text = AgentManager[0]["Tel"].ToString().Substring(AgentManager[0]["Tel"].ToString().IndexOf("-") + 1, AgentManager[0]["Tel"].ToString().Length - AgentManager[0]["Tel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfAgTel.Text = AgentManager[0]["Tel"].ToString();
                }
            }

            if (AgentManager[0]["Fax"].ToString() != "")
            {
                if (AgentManager[0]["Fax"].ToString().IndexOf("-") > 0)
                {
                    txtOfAgFax_pre.Text = AgentManager[0]["Fax"].ToString().Substring(0, AgentManager[0]["Fax"].ToString().IndexOf("-"));
                    txtOfAgFax.Text = AgentManager[0]["Fax"].ToString().Substring(AgentManager[0]["Fax"].ToString().IndexOf("-") + 1, AgentManager[0]["Fax"].ToString().Length - AgentManager[0]["Fax"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfAgFax.Text = AgentManager[0]["Fax"].ToString();
                }
            }
            txtOfAgEmail1.Text = AgentManager[0]["Email"].ToString();
            txtOfAgWebsite.Text = AgentManager[0]["Website"].ToString();
            txtOfAgAddress.Text = AgentManager[0]["Address"].ToString();
            txtOfAgResponsible.Text = AgentManager[0]["Responsible"].ToString();
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
    }
}
