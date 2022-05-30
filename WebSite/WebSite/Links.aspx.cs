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

public partial class Links : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxDataView1.DataSource = ObjectDataSource1;
        ASPxDataView1.DataBind();

        if (!IsPostBack)
        {

            ComboType.DataBind();
            ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));

        }
    }
    protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ASPxDataView1.DataSource = ObjectDataSource1;
        ASPxDataView1.DataBind();
    }

    protected void Image_DataBinding(object sender, EventArgs e)
    {
        Image img = (Image)sender;
        if (string.IsNullOrEmpty(img.ImageUrl))
            img.ImageUrl = "~/images/noimage.gif";
        else
            img.ImageUrl = img.ImageUrl;
    }
}
