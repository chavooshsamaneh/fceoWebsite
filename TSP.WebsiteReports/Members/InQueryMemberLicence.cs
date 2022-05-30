using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Members
{
    public partial class InQueryMemberLicence : DevExpress.XtraReports.UI.XtraReport
    {
        public InQueryMemberLicence(int MeId, int MlId, int LicenceInqueryRequestAsignerId, int IsMeTemp)
        {
            InitializeComponent();

            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
            TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
            PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.InqueryMemberLicence);
            if (PrintAssignerSettingManager.Count > 0)
            {
                lblGovName.Text = "با تشکر - " + PrintAssignerSettingManager[0]["GmnName"].ToString();
                lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
                if (string.IsNullOrEmpty(PrintAssignerSettingManager[0]["SignUrl"].ToString()))
                {
                    xrPicSign.Visible = false;
                    xrlblWarning.Visible = false;
                }
                else
                {
                    xrPicSign.Visible = true;
                    xrlblWarning.Visible = true;
                    xrPicSign.ImageUrl = PrintAssignerSettingManager[0]["SignUrl"].ToString();
                }
            }

            if (IsMeTemp == 0)
            {
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count == 1)
                {
                    lblMeName.Text = MemberManager[0]["MeName"].ToString();
                    lblIDNo.Text = MemberManager[0]["IdNo"].ToString();
                    lblPlace.Text = MemberManager[0]["IssuePlace"].ToString();
                    lblFatherName.Text = MemberManager[0]["FatherName"].ToString();
                }

                MemberLicenceManager.FindByCode(MlId);
                if (MemberLicenceManager.Count == 1)
                {
                    lblMajor.Text = MemberLicenceManager[0]["MeLicenceNamertl"].ToString();
                    lblUnName.Text = MemberLicenceManager[0]["UnName"].ToString();
                    lblEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
                }
            }
            else
            {
                TempMemberManager.FindByCode(MeId);
                if (TempMemberManager.Count == 1)
                {
                    lblMeName.Text = TempMemberManager[0]["MeName"].ToString();
                    lblIDNo.Text = TempMemberManager[0]["IdNo"].ToString();
                    lblPlace.Text = TempMemberManager[0]["IssuePlace"].ToString();
                    lblFatherName.Text = TempMemberManager[0]["FatherName"].ToString();
                }

                TempMemberLicenceManager.FindByCode(MlId);
                if (TempMemberLicenceManager.Count == 1)
                {
                    lblMajor.Text = TempMemberLicenceManager[0]["MeLicenceNamertl"].ToString();
                    lblUnName.Text = TempMemberLicenceManager[0]["UnName"].ToString();
                    lblEndDate.Text = TempMemberLicenceManager[0]["EndDate"].ToString();
                }
            }
        }
    }
}
