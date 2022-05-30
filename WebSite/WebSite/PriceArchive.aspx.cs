using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PriceArchive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    protected void btnPriceArchiveShow_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        Response.Redirect("~/PriceArchiveShow.aspx?PrAId=" + Utility.EncryptQS(lb.CommandArgument));
    }
}