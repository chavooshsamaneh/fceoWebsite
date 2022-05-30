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

public partial class Help_ShowHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            try
            {
                int Id = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Id"].ToString())));
                if (String.IsNullOrEmpty(Id.ToString()))
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                Utility.Help objHelp = new Utility.Help((Utility.Help.HelpFiles)Id);

                HelpFrame.Attributes["src"] = objHelp.HelpFileUrl;
                DownloadHelpFrame.Attributes["src"] = "DownloadHelp.aspx?Id=" + Request.QueryString["Id"].ToString();
                this.Title = objHelp.Title;
            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
        }
    }
}
