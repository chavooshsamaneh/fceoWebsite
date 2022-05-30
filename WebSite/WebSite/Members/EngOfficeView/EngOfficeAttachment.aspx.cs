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
using DevExpress.Web;
using System.IO;

public partial class Members_EngOfficeView_EngOfficeAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "EngOffice":
                Response.Redirect("EngOfficeShowInfo.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeShowInfo.aspx?PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&EOfId=" + EngFileId.Value);
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (EngFileId.Value != null)
        {
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            if (e.GetValue("RefTable") == null)
                return;
            string CurretnEOfId = e.GetValue("RefTable").ToString();
            if (EOfId == CurretnEOfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
}