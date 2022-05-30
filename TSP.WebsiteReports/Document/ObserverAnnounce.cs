using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class ObserverAnnounce : DevExpress.XtraReports.UI.XtraReport
    {
        public ObserverAnnounce(int MeId)
        {
            InitializeComponent();

            TSP.DataManager.MemberManager MemberManager = new DataManager.MemberManager();
            TSP.DataManager.EngOfficeManager EngOfficeManager = new DataManager.EngOfficeManager();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new DataManager.OfficeMemberManager();

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                lblMeId.Text = MeId.ToString();
                lblMemberName.Text = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                lblMemberFileNo.Text = MemberManager[0]["FileNo"].ToString();
                lblMemberMajor.Text = MemberManager[0]["LastMjName"].ToString();
            }

            lblYear.Text = TSP.DataManager.Utility.GetYearOfToday();
            EngOfficeManager.SelectEngOfficeByMeId(MeId);
            if (EngOfficeManager.Count > 0)
            {
                lblEngOfId.Text = EngOfficeManager[0]["EngOfId"].ToString();
                lblEngOffExpireDate.Text = EngOfficeManager[0]["LastExpireDate"].ToString();
                switch (Convert.ToInt32(EngOfficeManager[0]["OfpId"]))
                {
                    case (int)TSP.DataManager.OfficePosition.EngOfficeManager:
                        chkEngOffManager.Checked = true;
                        break;
                    case (int)TSP.DataManager.OfficePosition.EngOfficeEmployed:
                        chkEngOffMember.Checked = true;
                        break;
                }
            }
            else
            {
                OfficeMemberManager.SelectActiveOfficeMemberByMeId(MeId);
                if (OfficeMemberManager.Count > 0)
                {
                    lblOfId.Text = OfficeMemberManager[0]["OfId"].ToString();
                    lblOfficeExpireDate.Text = OfficeMemberManager[0]["LastExpireDate"].ToString();
                    switch (Convert.ToInt32(OfficeMemberManager[0]["OfpId"]))
                    {
                        case (int)TSP.DataManager.OfficePosition.Manager:
                            chkOfficeManager.Checked = true;
                            break;
                        case (int)TSP.DataManager.OfficePosition.Board:
                            chkOfficeBoard.Checked = true;
                            break;
                        case (int)TSP.DataManager.OfficePosition.Employed:
                            chkOfficeMember.Checked = true;
                            break;
                    }
                }
            }
        }

    }
}
