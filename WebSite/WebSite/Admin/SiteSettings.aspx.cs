using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SiteSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_IsTspAdmin() == false)
            Response.Redirect("~/Login.aspx");

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (IsPostBack == false)
            Load_PageData();
    }

    void Load_PageData()
    {
        try
        {
            txtAdminUserId.Text = System.Configuration.ConfigurationManager.AppSettings["AdminUserId"];
            cmbWebsiteAccessible.Value = System.Configuration.ConfigurationManager.AppSettings["WebsiteIsNotAccessible"];
            txtLoginCookieTimeOut.Text = System.Configuration.ConfigurationManager.AppSettings["LoginCookieTimeOut"];
            txtQueryTimeOut.Text = System.Configuration.ConfigurationManager.AppSettings["QueryTimeOut"];
            txtFileHandlerTimeOut.Text = System.Configuration.ConfigurationManager.AppSettings["FileHandlerTimeOut"];
            cmbExceptionError.Value = System.Configuration.ConfigurationManager.AppSettings["ShowExceptionError"];
            txtMaxSizeForUploadFile.Text = System.Configuration.ConfigurationManager.AppSettings["MaxSizeForUploadFile"];
            txtSoftwareVersion.Text = System.Configuration.ConfigurationManager.AppSettings["SoftwareVersion"];
            txtSoftwareEditedNo.Text = System.Configuration.ConfigurationManager.AppSettings["SoftwareEditedNo"];
            txtNezamFarsWebsiteAddress.Text = System.Configuration.ConfigurationManager.AppSettings["NezamFarsWebsiteAddress"];
            cmbMemberSearchAccessible.Value = System.Configuration.ConfigurationManager.AppSettings["IsMemberSearchAccessible"];
            cmbIsZeroInvoiceCheck.Value = System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"];
            txtLetterActionDuration.Text = System.Configuration.ConfigurationManager.AppSettings["LetterActionDuration"];
            txtWFExpireDateDuration.Text = System.Configuration.ConfigurationManager.AppSettings["WFExpireDateDuration"];
            cmbDefaultCountry.DataBind();
            cmbDefaultCountry.Value = System.Configuration.ConfigurationManager.AppSettings["CurrentCounId"];
            cmbDefaultProvince.DataBind();
            cmbDefaultProvince.Value = System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceId"];
            cmbDefaultCity.DataBind();
            cmbDefaultCity.Value = System.Configuration.ConfigurationManager.AppSettings["CurrentCitId"];
            txtProvinceNezamCode.Text = System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceNezamCode"];
            cmbCheckMunPermission.Value = System.Configuration.ConfigurationManager.AppSettings["CheckMunPermission"];
            cmbCreateAccount.Value = System.Configuration.ConfigurationManager.AppSettings["CreateAccount"];
            cmbIsAmoozeshConditionsChecked.Value = System.Configuration.ConfigurationManager.AppSettings["IdAmoozeshConditionsChecked"];
            txtDefaultNewsExpireDate.Text = System.Configuration.ConfigurationManager.AppSettings["DefaultNewsExpireDate"];

            //*********************وب سرویس عصر فرا ارتباط
            txtWebService.Text = System.Configuration.ConfigurationManager.AppSettings["ir.afe.www.WebService"];
            txtBoxService.Text = System.Configuration.ConfigurationManager.AppSettings["ir.afe.www.BoxService"];
            txtSMSUsername.Text = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
            //txtSMSPassword.Text = System.Configuration.ConfigurationManager.AppSettings["SMSPassWord"];
            txtSMSNumber.Text = System.Configuration.ConfigurationManager.AppSettings["SMSNumber"];
            //***************************************************************
            txtSMSPacketSize.Text = System.Configuration.ConfigurationManager.AppSettings["SMSPacketSize"];
            txtNoOfSMSPacketSendBeforeThreadSleep.Text = System.Configuration.ConfigurationManager.AppSettings["NoOfSMSPacketSendBeforeThreadSleep"];
            txtSMSThreadSleepTime.Text = System.Configuration.ConfigurationManager.AppSettings["SMSThreadSleepTime"];
            //*********************وب سرویس عصر فرا مگفا
            txtMagfaWebServiceURL.Text = System.Configuration.ConfigurationManager.AppSettings["MagfaURL"];
            txtMagfaSMSUserName.Text = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSUserName"];
            //txtMagfaSMSPassword.Text = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSPassWord"];
            txtMagfaSMSNumber.Text = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSNumber"];
            txtMagfaSMSDomain.Text = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSDomain"];
            //***************************************************************
            //txtEmail.Text = System.Configuration.ConfigurationManager.AppSettings["Email_ID"];
            txtEmailDisplayName.Text = System.Configuration.ConfigurationManager.AppSettings["Email_DisplayName"];
            txtSMPTName.Text = System.Configuration.ConfigurationManager.AppSettings["SMPT_Name"];
            cmbSMTPUseDefaultCredentials.Value = System.Configuration.ConfigurationManager.AppSettings["SMPT_UseDefaultCredentials"];
            cmbSMPTEnableSsl.Value = System.Configuration.ConfigurationManager.AppSettings["SMPT_EnableSsl"];
            txtSMPTPort.Text = System.Configuration.ConfigurationManager.AppSettings["SMPT_Port"];
            txtEmailLinkTimeOut.Text = System.Configuration.ConfigurationManager.AppSettings["EmailLinkTimeOut"];
            //**************************************************************            
           // txtConnectionString.Text = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NezamFarsConnectionString1"].ConnectionString;

            Configuration configurationAd = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Admin");
            AppSettingsSection appSettingsSectionAd = (AppSettingsSection)configurationAd.GetSection("appSettings");
            if (appSettingsSectionAd != null)
            {
                //txtEmailPass.Text = appSettingsSectionAd.Settings["Email_Pass"].Value;
                cmbShowMessage.Value = appSettingsSectionAd.Settings["Show"].Value;
                txtShowmsg.Text = appSettingsSectionAd.Settings["txtShow"].Value;
            }




        }
        catch (Exception) { }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                appSettingsSection.Settings["AdminUserId"].Value = txtAdminUserId.Text;
                appSettingsSection.Settings["WebsiteIsNotAccessible"].Value = cmbWebsiteAccessible.Value.ToString();
                appSettingsSection.Settings["LoginCookieTimeOut"].Value = txtLoginCookieTimeOut.Text;
                appSettingsSection.Settings["QueryTimeOut"].Value = txtQueryTimeOut.Text;
                appSettingsSection.Settings["FileHandlerTimeOut"].Value = txtFileHandlerTimeOut.Text;
                appSettingsSection.Settings["ShowExceptionError"].Value = cmbExceptionError.Value.ToString();
                appSettingsSection.Settings["MaxSizeForUploadFile"].Value = txtMaxSizeForUploadFile.Text;
                appSettingsSection.Settings["SoftwareVersion"].Value = txtSoftwareVersion.Text;
                appSettingsSection.Settings["SoftwareEditedNo"].Value = txtSoftwareEditedNo.Text;
                appSettingsSection.Settings["NezamFarsWebsiteAddress"].Value = txtNezamFarsWebsiteAddress.Text;
                appSettingsSection.Settings["IsMemberSearchAccessible"].Value = cmbMemberSearchAccessible.Value.ToString();
                appSettingsSection.Settings["IsZeroInvoiceCheck"].Value = cmbIsZeroInvoiceCheck.Value.ToString();
                appSettingsSection.Settings["LetterActionDuration"].Value = txtLetterActionDuration.Text;
                appSettingsSection.Settings["WFExpireDateDuration"].Value = txtWFExpireDateDuration.Text;
                appSettingsSection.Settings["CurrentCounId"].Value = cmbDefaultCountry.Value.ToString();
                appSettingsSection.Settings["CurrentProvinceId"].Value = cmbDefaultProvince.Value.ToString();
                appSettingsSection.Settings["CurrentCitId"].Value = cmbDefaultCity.Value.ToString();
                appSettingsSection.Settings["CurrentProvinceNezamCode"].Value = txtProvinceNezamCode.Text;
                appSettingsSection.Settings["CheckMunPermission"].Value = cmbCheckMunPermission.Value.ToString();
                appSettingsSection.Settings["CreateAccount"].Value = cmbCreateAccount.Value.ToString();
                appSettingsSection.Settings["IdAmoozeshConditionsChecked"].Value = cmbIsAmoozeshConditionsChecked.Value.ToString();
                appSettingsSection.Settings["DefaultNewsExpireDate"].Value = txtDefaultNewsExpireDate.Text;

                //*******************************
                appSettingsSection.Settings["ir.afe.www.WebService"].Value = txtWebService.Text;
                appSettingsSection.Settings["ir.afe.www.BoxService"].Value = txtBoxService.Text;
                appSettingsSection.Settings["SMSUserName"].Value = txtSMSUsername.Text;
                //appSettingsSection.Settings["SMSPassWord"].Value = txtSMSPassword.Text;
                appSettingsSection.Settings["SMSNumber"].Value = txtSMSNumber.Text;
                //********************************
                appSettingsSection.Settings["SMSPacketSize"].Value = txtSMSPacketSize.Text;
                appSettingsSection.Settings["NoOfSMSPacketSendBeforeThreadSleep"].Value = txtNoOfSMSPacketSendBeforeThreadSleep.Text;
                appSettingsSection.Settings["SMSThreadSleepTime"].Value = txtSMSThreadSleepTime.Text;
                //********************************
                appSettingsSection.Settings["MagfaURL"].Value = txtMagfaWebServiceURL.Text;
                appSettingsSection.Settings["MagfaSMSUserName"].Value = txtMagfaSMSUserName.Text;
                //appSettingsSection.Settings["MagfaSMSPassWord"].Value = txtMagfaSMSPassword.Text;
                appSettingsSection.Settings["MagfaSMSNumber"].Value = txtMagfaSMSNumber.Text;
                appSettingsSection.Settings["MagfaSMSDomain"].Value = txtMagfaSMSDomain.Text;
                //********************************
                //appSettingsSection.Settings["Email_ID"].Value = txtEmail.Text;
                appSettingsSection.Settings["Email_DisplayName"].Value = txtEmailDisplayName.Text;
                appSettingsSection.Settings["SMPT_Name"].Value = txtSMPTName.Text;
                appSettingsSection.Settings["SMPT_UseDefaultCredentials"].Value = cmbSMTPUseDefaultCredentials.Value.ToString();
                appSettingsSection.Settings["SMPT_EnableSsl"].Value = cmbSMPTEnableSsl.Value.ToString();
                appSettingsSection.Settings["SMPT_Port"].Value = txtSMPTPort.Text;
                appSettingsSection.Settings["EmailLinkTimeOut"].Value = txtEmailLinkTimeOut.Text;
                //*****************************               
                configuration.Save();

            }

            Configuration configurationAd = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Admin");
            AppSettingsSection appSettingsSectionAd = (AppSettingsSection)configurationAd.GetSection("appSettings");
            if (appSettingsSectionAd != null)
            {
                //appSettingsSectionAd.Settings["Email_Pass"].Value = txtEmailPass.Text;
                appSettingsSectionAd.Settings["Show"].Value = cmbShowMessage.Value.ToString();
                appSettingsSectionAd.Settings["txtShow"].Value = txtShowmsg.Text;
                configurationAd.Save();
            }

            ShowMessage("ذخیره انجام شد");
            return;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        ShowMessage("خطایی در ذخیره انجام گرفته است");
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}