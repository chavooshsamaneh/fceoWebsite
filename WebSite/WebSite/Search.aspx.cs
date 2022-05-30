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

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (Request.QueryString["Title"] != null)
            {
                txtSearch.Text = Utility.DecryptQS(Request.QueryString["Title"].ToString());
                linkFormsSearch.HRef = "/Search.aspx?Title=" + Request.QueryString["Title"] + "#FormSearchSection";
                linkNewsSearch.HRef = "/Search.aspx?Title=" + Request.QueryString["Title"] + "#NewsSearchSection";
                linkPeriodsSearch.HRef = "/Search.aspx?Title=" + Request.QueryString["Title"] + "#PeriodSearchSection";
                linkRulesSearch.HRef = "/Search.aspx?Title=" + Request.QueryString["Title"] + "#RulesSearchSection";
                linkTenderSearch.HRef = "/Search.aspx?Title=" + Request.QueryString["Title"] + "#TenderSearchSection";
                SearchNews();
                SearchPeriods();
                SearchRules();
                SearchForms();
                SearchTender();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (Utility.IsDBNullOrNullValue(txtSearch.Text))
        {
            ShowMessage("عنوان مورد نظر را وارد نمایید");
            return;
        }
        linkFormsSearch.HRef = "/Search.aspx?Title=" + Utility.EncryptQS(txtSearch.Text) + "#FormSearchSection";
        linkNewsSearch.HRef = "/Search.aspx?Title=" + Utility.EncryptQS(txtSearch.Text) + "#NewsSearchSection";
        linkPeriodsSearch.HRef = "/Search.aspx?Title=" + Utility.EncryptQS(txtSearch.Text) + "#PeriodSearchSection";
        linkRulesSearch.HRef = "/Search.aspx?Title=" + Utility.EncryptQS(txtSearch.Text) + "#RulesSearchSection";
        linkTenderSearch.HRef = "/Search.aspx?Title=" + Utility.EncryptQS(txtSearch.Text) + "#TenderSearchSection";
        SearchNews();
        SearchPeriods();
        SearchRules();
        SearchForms();
    }

    protected void LinkButtonNewsDetail_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        Response.Redirect("~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(lb.CommandArgument) + "&PageMode=" + Utility.EncryptQS("Archive"));
    }

    protected void Rating1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxRatingControl Rating = (DevExpress.Web.ASPxRatingControl)sender;
        Rating.Value = int.Parse(Rating.ToolTip);
        Rating.ToolTip = "";
    }

    protected void Image2_DataBinding(object sender, EventArgs e)
    {
        HtmlImage img = (HtmlImage)sender;
        if (string.IsNullOrEmpty(img.Src))
            img.Src = "~/images/noimage.gif";
        else
            img.Src = img.Src;//"~/News/" + img.ImageUrl;
    }

    protected void lblBody_DataBinding(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl cont = new HtmlGenericControl("div");

        Label lbl = (Label)sender;
        string Stripped = System.Text.RegularExpressions.Regex.Replace(lbl.Text, @"<(.|\n)*?>", string.Empty);

        if (Stripped.Length >= 400)
            lbl.Text = Server.HtmlDecode(Stripped).Substring(1, 400);

    }
    protected void btnViewPeriods_Click(object sender, EventArgs e)
    {
        string InsId = "";
        string PPId = "";
        LinkButton lb = (LinkButton)sender;
        string[] Parameters = lb.CommandArgument.Split(';');
        PPId = Parameters[0].ToString();
        InsId = Parameters[1].ToString();
        string PType = Parameters[2].ToString();
        if (PType == "0")
            Response.Redirect("PeriodsView.aspx?PPId=" + Utility.EncryptQS(PPId) + "&InsId=" + Utility.EncryptQS(InsId) + "&PrePg=" + Utility.EncryptQS("Period"));
        else if (PType == "1")
            Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(PPId) + "&InsId=" + Utility.EncryptQS(InsId) + "&PrePg=" + Utility.EncryptQS("Period"));
    }

    private void SearchNews()
    {

        if (!string.IsNullOrEmpty(txtSearch.Text))
            ObjdsNews.SelectParameters["Title"].DefaultValue = txtSearch.Text;
        else
            ObjdsNews.SelectParameters["Title"].DefaultValue = "%";
        ObjdsNews.SelectParameters["Body"].DefaultValue = "%";

        ObjdsNews.SelectParameters["FromDate"].DefaultValue = "1";

        ObjdsNews.SelectParameters["ToDate"].DefaultValue = "2";

        ObjdsNews.SelectParameters["SubjectId"].DefaultValue = "-1";

        ObjdsNews.SelectParameters["Importance"].DefaultValue = "-1";

        ObjdsNews.SelectParameters["AgentId"].DefaultValue = "-1";

        ObjdsNews.SelectParameters["ExGroupId"].DefaultValue = "-1";

        ObjdsNews.SelectParameters["ExGroupId"].DefaultValue = "-1";

        ObjdsNews.SelectParameters["IsNotification"].DefaultValue = "-1";
        
        DataViewNews.DataBind();
    }
    private void SearchPeriods()
    {
          if (!string.IsNullOrEmpty(txtSearch.Text))
              OdbPeriod.SelectParameters["PeriodTitle"].DefaultValue = txtSearch.Text;
        else
              OdbPeriod.SelectParameters["PeriodTitle"].DefaultValue = "%";
          DataViewPeriods.DataBind();
    }

    private void SearchRules()
    {
        if (!string.IsNullOrEmpty(txtSearch.Text))
            ObjdsRules.SelectParameters["RuName"].DefaultValue = txtSearch.Text;
        else
            ObjdsRules.SelectParameters["RuName"].DefaultValue = "%";

        DataViewRules.DataBind();
    }

    private void SearchForms()
    {
        if (!string.IsNullOrEmpty(txtSearch.Text))
            ObjdsForms.SelectParameters["FoName"].DefaultValue = txtSearch.Text;
        else
            ObjdsForms.SelectParameters["FoName"].DefaultValue = "%";    

        DataViewForms.DataBind();
    }

    private void SearchTender()
    {
        if (!string.IsNullOrEmpty(txtSearch.Text))
            ObjectDataSourceTender.SelectParameters["TeName"].DefaultValue = txtSearch.Text;
        else
            ObjectDataSourceTender.SelectParameters["TeName"].DefaultValue = "%";
        DataViewTender.DataBind();
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

}