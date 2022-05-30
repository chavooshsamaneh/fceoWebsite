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

public partial class Help_Help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            XmlDataSource1.XPath = "/*/Employee/*";
        else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            XmlDataSource1.XPath = "/*/Members/*";
        else
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
        ASPxTreeList1.DataBind();
    }

    protected void lnkIndex_DataBound(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink lnkIndex = (DevExpress.Web.ASPxHyperLink)sender;
        lnkIndex.Enabled = Boolean.Parse(lnkIndex.ToolTip);
        lnkIndex.ToolTip = "";
    }
}
