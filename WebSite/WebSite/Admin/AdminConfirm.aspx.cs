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

public partial class Admin_AdminConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

        if (Utility.GetCurrentUser_IsTspAdmin() == false)
            Response.Redirect("~/Login.aspx");
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        txtPass.Text = Utility.GeneratePassword();
    }

    protected void btnGenerateDefault_Click(object sender, EventArgs e)
    {
        txtPass.Text = "tsp" + DateTime.Today.Year + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(txtPass.Text))
        {
            lblMessage.Text = "رمز عبور وارد نشده است";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        try
        {
            Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");

            if (appSettingsSection != null)
            {

                appSettingsSection.Settings["CurrentProvinceValueAdId"].Value = Utility.EncryptPassword2(txtUsername.Text.ToLower()) + "$" + Utility.EncryptPassword2(txtPass.Text);
                configuration.Save();
                lblMessage.Text = "ذخیره انجام شد";
                lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
                return;
            }
        }
        catch (Exception) { }
        lblMessage.Text = "خطایی در ذخیره انجام گرفته است";
        lblMessage.ForeColor = System.Drawing.Color.Red;
    }
}

