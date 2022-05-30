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

public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Utility.IsMemberSearchAccessible())
        //{
        //    Response.Redirect("SearchIsNotAccessible.aspx");
        //}
        if (!IsPostBack)
        {
            ObjectDataSourceMember.CacheDuration = Utility.GetCacheDuration();

            ViewState["Filter"] = "";
      
            ComboMajor.DataBind();
            ComboMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            ComboMajor.SelectedIndex = -1;

            //ASPxComboBox1.SelectedIndex = 1;
        }
        if (ViewState["Filter"] != null)
            ObjectDataSourceMember.FilterExpression = ViewState["Filter"].ToString();

        //ASPxDataView1.RowPerPage = int.Parse(ASPxComboBox1.SelectedItem.Text);
        //ASPxDataView1.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjectDataSourceMember.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjectDataSourceMember.SelectParameters["MeId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtMeNo.Text))
            ObjectDataSourceMember.SelectParameters["MeNo"].DefaultValue = txtMeNo.Text;
        else
            ObjectDataSourceMember.SelectParameters["MeNo"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtFirstName.Text))
            ObjectDataSourceMember.SelectParameters["FirstName"].DefaultValue = txtFirstName.Text;
        else
            ObjectDataSourceMember.SelectParameters["FirstName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtLastName.Text))
            ObjectDataSourceMember.SelectParameters["LastName"].DefaultValue = txtLastName.Text;
        else
            ObjectDataSourceMember.SelectParameters["LastName"].DefaultValue = "%";
        if (ComboMajor.SelectedItem != null && ComboMajor.Value != null)
            ObjectDataSourceMember.SelectParameters["MjId"].DefaultValue = ComboMajor.Value.ToString();
        else
            ObjectDataSourceMember.SelectParameters["MjId"].DefaultValue = "-1";
        if (ComboImplement.SelectedItem != null && ComboImplement.Value != null)
            ObjectDataSourceMember.SelectParameters["HasImpDoc"].DefaultValue = ComboImplement.Value.ToString();
        else
            ObjectDataSourceMember.SelectParameters["HasImpDoc"].DefaultValue = "-1";

        DataViewMembers.DataBind();
    }
}
