using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class TaxConfirmLetter : DevExpress.XtraReports.UI.XtraReport
    {
        public TaxConfirmLetter(int MeId)
        {
            InitializeComponent();
            txtDate.Text = TSP.DataManager.Utility.GetDateOfToday();
            xrLabel9.Text = TSP.DataManager.Utility.GetDateOfToday()+"/"+MeId.ToString();
            try
            {
                TSP.DataManager.MemberManager MemberManager = new DataManager.MemberManager();
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count > 0)
                {


                    if (MemberManager[0]["FileNo"] != null && !string.IsNullOrEmpty(MemberManager[0]["FileNo"].ToString()))
                    {
                        lblNecessry.Text = "به منظور مراجعه به اداره مالیات به همراه داشتن مدارک ذیل الزامی می باشد 1-كپي تمام صفحات شناسنامه 2-كپي كارت ملي 3-كپي سند مالكيت يا قرارداد اجاره محل سكونت يا فعاليت 4-كپي پروانه اشتغال 5-مهر نظام معماری";
                        lblDocType.Text = "تمدید";
                        xrlblWarning.Visible = false;
                    }
                       
                    else
                    {
                        lblNecessry.Text = "به منظور مراجعه به اداره مالیات به همراه داشتن مدارک ذیل الزامی می باشد 1-كپي تمام صفحات شناسنامه 2-كپي كارت ملي 3-كپي سند مالكيت يا قرارداد اجاره محل سكونت يا فعاليت";
                        lblDocType.Text = "صدور";
                        xrlblWarning.Visible = false;
                    }
                        
                    if (MemberManager[0]["IdNo"] != null)
                        txtIdno.Text = MemberManager[0]["IdNo"].ToString();
                    if (MemberManager[0]["SSN"] != null)
                        txtSSN.Text = MemberManager[0]["SSN"].ToString();
                    if (MemberManager[0]["FirstName"] != null && MemberManager[0]["LastName"] != null)
                        xrLMeName.Text = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                    if (MemberManager[0]["AgentId"] != null || string.IsNullOrWhiteSpace(MemberManager[0]["AgentId"].ToString()))
                    {
                        if (Convert.ToInt16(MemberManager[0]["AgentId"]) == 1)
                        {
                            txtTaxName.Text = "عضو محترم، با توجه به کد پستی اقامتگاه قانونی اعلام شده و بر اساس جدول پیوست به حوزه مالیاتی مربوطه مراجعه فرمایید.";
                            xrLabel1.Text = "رئیس محترم اداره امور مالیاتی شیراز";
                            lblTell.Visible = txtTell.Visible = false;
                        }
                        else
                        {
                            TSP.DataManager.AccountingAgentManager AccountingAgentManager = new DataManager.AccountingAgentManager();
                            AccountingAgentManager.FindByCode(Convert.ToInt16(MemberManager[0]["AgentId"]));
                            if (AccountingAgentManager.Count == 1)
                            {
                                lblTell.Visible = txtTell.Visible = true;
                                if (AccountingAgentManager[0]["TaxOfficeAdress"] != null)
                                    txtTaxName.Text = "آدرس امور مالیاتی استان فارس : " + AccountingAgentManager[0]["TaxOfficeAdress"].ToString();
                                if (AccountingAgentManager[0]["TaxOfficeTell"] != null)
                                    txtTell.Text = AccountingAgentManager[0]["TaxOfficeTell"].ToString();
                                if (AccountingAgentManager[0]["TaxOfficeName"] != null && AccountingAgentManager[0]["Name"] != null)
                                    xrLabel1.Text = "رئیس محترم اداره امور مالیاتی " + AccountingAgentManager[0]["TaxOfficeName"].ToString() + " " + AccountingAgentManager[0]["Name"].ToString();
                            }
                        }
                    }
                }
                TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
                PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.TaxConfirmLetter);
                if (PrintAssignerSettingManager.Count > 0)
                {
                    lblGovName.Text = PrintAssignerSettingManager[0]["GmnName"].ToString();
                    lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
                    if (string.IsNullOrEmpty(PrintAssignerSettingManager[0]["SignUrl"].ToString()))
                    {
                        xrPicSign.Visible = false;
                    //    xrlblWarning.Visible = false;
                    }
                    else
                    {
                        xrPicSign.Visible = true;
                      //  xrlblWarning.Visible = true;
                        xrPicSign.ImageUrl = PrintAssignerSettingManager[0]["SignUrl"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
