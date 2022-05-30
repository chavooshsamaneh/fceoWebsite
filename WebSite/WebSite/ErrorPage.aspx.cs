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

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ErrorNo = "";
        if(Request.QueryString["ErrorNo"]!=null)
          ErrorNo= Server.HtmlDecode(Request.QueryString["ErrorNo"].ToString());
        if (!string.IsNullOrEmpty(ErrorNo))
        {
            ErrorCodes.ErrorType ec = ((ErrorCodes.ErrorType)(int.Parse(ErrorNo)));
             ASPxLabel1.Text =  ErrorCodes.GetErrorDescription(ec);
        }
        else
        {
            ASPxLabel1.Text =   ErrorCodes.GetErrorDescription(ErrorCodes.ErrorType.GeneralErr);
        }
     
    }
}
