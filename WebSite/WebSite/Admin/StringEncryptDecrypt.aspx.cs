using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StringEncryptDecrypt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        PanelResult.Visible = true;
        try
        {
            lblResult.Text = "Encrypt: &nbsp;<b>" + Utility.EncryptQS(txtStr.Text) + "</b>";
        }
        catch (Exception err)
        {
            lblResult.Text = "Error: &nbsp;<b>" + err.Message + "</b>";
        }
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        PanelResult.Visible = true;
        try
        {
            lblResult.Text = "Decrypt: &nbsp;<b>" + Utility.DecryptQS(txtStr.Text) + "</b>";
        }
        catch (Exception err)
        {
            lblResult.Text = "Error: &nbsp;<b>" + err.Message + "</b>";
        }
    }
}