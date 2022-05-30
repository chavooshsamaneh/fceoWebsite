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

public partial class Forms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmbFormType.DataBind();
            cmbFormType.Items.Insert(0,new ListEditItem("--------------",-1));
            cmbFormType.SelectedIndex = 0;
          
        }
        Search();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtCode.Text))
            ObjdsForms.SelectParameters["FoCode"].DefaultValue = txtCode.Text;
        else
            ObjdsForms.SelectParameters["FoCode"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjdsForms.SelectParameters["FoName"].DefaultValue = txtName.Text;
        else
            ObjdsForms.SelectParameters["FoName"].DefaultValue = "%";

        if (cmbFormType.SelectedItem!=null && !Utility.IsDBNullOrNullValue(cmbFormType.SelectedItem.Value))
            ObjdsForms.SelectParameters["FormTypeId"].DefaultValue =cmbFormType.SelectedItem.Value.ToString();
        else
            ObjdsForms.SelectParameters["FormTypeId"].DefaultValue = "-1";

        DataViewForms.DataBind();
    }
}
