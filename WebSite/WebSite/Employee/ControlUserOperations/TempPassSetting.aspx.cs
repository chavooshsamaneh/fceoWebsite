using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Employee_ControlUserOperations_TempPassSetting : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermissionForTempPassSetting(Utility.GetCurrentUser_UserId(),
          (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = btnSave2.Enabled = per.CanView;
            this.ViewState["btnSave"] = btnSave.Enabled;
            FillForm();

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                appSettingsSection.Settings["HaveTempPassEmpId"].Value = cmbEmpTempPass.Value.ToString();
                appSettingsSection.Settings["HaveTempPassStlId"].Value = cmbStlTempPass.Value.ToString();
                appSettingsSection.Settings["HaveTempPassMeId"].Value = cmbMeTempPass.Value.ToString();
                appSettingsSection.Settings["HaveTempPassTempMemberId"].Value = cmbTempMeTempPass.Value.ToString();
                configuration.Save();
                ShowMessage("ذخیره انجام شد");
                IsPageRefresh = true;
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        ShowMessage("خطایی در ذخیره انجام گرفته است");
    }

    private void FillForm()
    {
        cmbEmpTempPass.Value = System.Configuration.ConfigurationManager.AppSettings["HaveTempPassEmpId"];
        cmbStlTempPass.Value = System.Configuration.ConfigurationManager.AppSettings["HaveTempPassStlId"];
        cmbMeTempPass.Value = System.Configuration.ConfigurationManager.AppSettings["HaveTempPassMeId"];
        cmbTempMeTempPass.Value = System.Configuration.ConfigurationManager.AppSettings["HaveTempPassTempMemberId"];
    }
    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}