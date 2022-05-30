using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Employee_SMS_Setting : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermissionForSmsSetting(Utility.GetCurrentUser_UserId(),
              (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = btnSave2.Enabled = per.CanView;
            this.ViewState["btnSave"] = btnSave.Enabled;

            cmbWebServiceSMSType.Value = System.Configuration.ConfigurationManager.AppSettings["CurrentSMSWebService"];
            LoadCredit();
        }

        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
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
                appSettingsSection.Settings["CurrentSMSWebService"].Value = cmbWebServiceSMSType.Value.ToString();
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

    void LoadCredit()
    {
        try
        {
            string[] SmsInfo = new string[4];
            SmsInfo = Utility.GetMagfaWebServiceInformation();
            string UserName = SmsInfo[0];
            string PassWord = SmsInfo[1];
            string Number = SmsInfo[2];
            string DomainName = SmsInfo[3];

            lblMagfaNumber.Text = Number;
            SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
            ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
            ssq.PreAuthenticate = true;
            double MagfaRemainingCredit = ssq.getCredit(DomainName);
            if (MagfaRemainingCredit > 0)
                lblMagfaCreditInfo.Text = MagfaRemainingCredit.ToString("N") + " ریال";
            else
                lblMagfaCreditInfo.Text = "اتمام اعتبار";
    
            ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
            string[] SmsInfoAFE = new string[2];
            SmsInfoAFE = Utility.GetSMSWebServiceInformation();
            string UserNameAFE = SmsInfoAFE[0];
            string PasswordAFE = SmsInfoAFE[1];
            string NumberAFE = SmsInfoAFE[2];

            lblAFENumber.Text = NumberAFE;
            string arc = BoxService.GetRemainingCredit(UserNameAFE, PasswordAFE);
            double AFERemainingCredit = 0;
            Double.TryParse(arc,out AFERemainingCredit);
            if (AFERemainingCredit > 0)
                lblAFECreditInfo.Text = AFERemainingCredit.ToString("N") + " ریال";
            else
                lblAFECreditInfo.Text = "اتمام اعتبار";
    
            string[] SmsInfoPrdco = new string[2];
            SmsInfoPrdco = Utility.GetPrdcoWebServiceInformation();
            string UserNamePrdco = SmsInfoPrdco[0];
            string PasswordPrdco = SmsInfoPrdco[1];
            string NumberPrdco = SmsInfoPrdco[2];
            lblPrdcoNumber.Text = NumberPrdco;
          
            SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();

            double PrdcoRemainingCredit = sendSoapClient.Credit(UserNamePrdco, PasswordPrdco);
           
            if (PrdcoRemainingCredit > 0)
                lblPrdcoCreditInfo.Text = PrdcoRemainingCredit.ToString("N") + " ریال";
            else
                lblPrdcoCreditInfo.Text = "اتمام اعتبار";
        }
        catch (Exception ex)
        {
            lblPrdcoCreditInfo.Text = "خطا در ارتباط با وب سرویس";
            Utility.SaveWebsiteError(ex);
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}