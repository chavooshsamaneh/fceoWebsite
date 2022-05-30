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

public partial class Settlement_EngOfficeDocument_EngOfficeAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["EOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string EOfId = Utility.DecryptQS(EngFileId.Value);

            ObjectDataSource1.SelectParameters[0].DefaultValue = ((int)TSP.DataManager.TableCodes.EngOffFile).ToString();
            ObjectDataSource1.SelectParameters[1].DefaultValue = EOfId;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("EngOfficeShow.aspx?EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
    }
    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = System.IO.Path.GetFileName(hp.Value.ToString());
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "EngOffice":
                Response.Redirect("EngOfficeShow.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }

    }
}
