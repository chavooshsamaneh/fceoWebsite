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

public partial class Rules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmbRulesType.DataBind();
            cmbRulesType.Items.Insert(0, new ListEditItem(" ", -1));
            cmbRulesType.SelectedIndex = 0;

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
            ObjdsRules.SelectParameters["RuId"].DefaultValue = txtCode.Text;
        else
            ObjdsRules.SelectParameters["RuId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjdsRules.SelectParameters["RuName"].DefaultValue = txtName.Text;
        else
            ObjdsRules.SelectParameters["RuName"].DefaultValue = "%";

        if (cmbRulesType.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbRulesType.SelectedItem.Value))
            ObjdsRules.SelectParameters["RulesTypeId"].DefaultValue = cmbRulesType.SelectedItem.Value.ToString();
        else
            ObjdsRules.SelectParameters["RulesTypeId"].DefaultValue = "-1";

        DataViewForms.DataBind();
    }
}
