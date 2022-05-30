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

public partial class Tender : System.Web.UI.Page
{
    //test
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxDataView1.DataSource = ObjectDataSource1;
        ASPxDataView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ASPxDataView1.DataSource = ObjectDataSource1;
        ASPxDataView1.DataBind();

    }
}
