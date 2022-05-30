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

public partial class Office : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       

        btnSearch_Click(this, new EventArgs());
        if (!IsPostBack)
        {
            cmbMFType.DataBind();
            cmbMFType.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbMFType.SelectedIndex = 0;
            comboGrade.DataBind();
            comboGrade.Items.Insert(0, new ListEditItem("<همه>", null));
            comboGrade.SelectedIndex = 0;
            
        }


        Search();
        String Script = @"
                        function SetEmpty(){                 
                        TextOfId.SetText('');
                        TextOfName.SetText('');
                        txtManagerName.SetText('');
                        txtMeNo.SetText('');
                        cmbMFType.SetSelectedIndex(0);    
                        comboGrade.SetSelectedIndex(0); 
                        }";
        Script += "function CheckSearch() {if (TextOfId.GetText('') == '' && TextOfName.GetText('') == '' && txtManagerName.GetText('') == '' && txtMeNo.GetText('') == '' && cmbMFType.GetSelectedIndex() == 0 && comboGrade.GetSelectedIndex() == 0) { return 0; } else { return 1; }}";
        // alert(1);  }";        
                        
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", Script, true);
    }
    protected void comboFamily_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {

        // OdbDataView.SelectParameters[1].DefaultValue = e.Parameter;
        //comboFamily.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        if (String.IsNullOrEmpty(txtOfId.Text.Trim()) == false && int.Parse(txtOfId.Text.Trim()) > 0)
            ObjectDataSourceOffice.SelectParameters["OfId"].DefaultValue = txtOfId.Text.Trim();
        else
            ObjectDataSourceOffice.SelectParameters["OfId"].DefaultValue = "-1";

        if (cmbMFType.SelectedIndex > 0)
            ObjectDataSourceOffice.SelectParameters["MFType"].DefaultValue = cmbMFType.Value.ToString();
        else
            ObjectDataSourceOffice.SelectParameters["MFType"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(TextOfName.Text))
            ObjectDataSourceOffice.SelectParameters["OfName"].DefaultValue = TextOfName.Text.Trim();
        else
            ObjectDataSourceOffice.SelectParameters["OfName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtManagerName.Text))
            ObjectDataSourceOffice.SelectParameters["ManagerFullName"].DefaultValue = txtManagerName.Text.Trim();
        else
            ObjectDataSourceOffice.SelectParameters["ManagerFullName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtMeNo.Text))
            ObjectDataSourceOffice.SelectParameters["MeNo"].DefaultValue = txtMeNo.Text.Trim();
        else
            ObjectDataSourceOffice.SelectParameters["MeNo"].DefaultValue = "%";
        if (comboGrade.SelectedIndex > 0)
            ObjectDataSourceOffice.SelectParameters["GrdId"].DefaultValue = comboGrade.Value.ToString();
        else
            ObjectDataSourceOffice.SelectParameters["GrdId"].DefaultValue = "-1";

        
        DataViewOffice.DataBind();
    }
}
