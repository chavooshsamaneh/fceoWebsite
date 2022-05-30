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

public partial class Settlement_OfficeDocument_OfficeFinancialStatusShow : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["OfsId"]))
            {
                Response.Redirect("OfficeFinancialStatus.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                FinancialId.Value = Server.HtmlDecode(Request.QueryString["OfsId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();


            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfsId = Utility.DecryptQS(FinancialId.Value);

            if ((string.IsNullOrEmpty(OfId)) || (string.IsNullOrEmpty(OfsId)))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();

            FillForm(int.Parse(OfsId));
        }
    }
    protected void FillForm(int OfsId)
    {
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        FinManager.FindByCode(OfsId);
        if (FinManager.Count == 1)
        {

            CmbName.DataBind();
            CmbName.SelectedIndex = CmbName.Items.IndexOfValue(int.Parse(FinManager[0]["OfdId"].ToString()));
            txtDesc.Text = FinManager[0]["Description"].ToString();
            decimal Cost = Convert.ToDecimal(FinManager[0]["Value"].ToString());
            txtValue.Text = Cost.ToString("#,#");
        }

        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.OfficeFinancialStatus, OfsId);
        AspxGridFlp.DataBind();
       

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);

    }
}
