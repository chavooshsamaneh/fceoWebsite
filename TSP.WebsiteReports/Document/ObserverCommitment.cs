using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class ObserverCommitment : DevExpress.XtraReports.UI.XtraReport
    {
        int meid, userid;
        public ObserverCommitment(int MeId)
        {
            InitializeComponent();

            meid = MeId;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            //TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
            //PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.MemberCartRequestPrinting);
            //if (PrintAssignerSettingManager.Count > 0)
            //{
            //    //    lblGovName.Text = "با تشکر - " + PrintAssignerSettingManager[0]["GmnName"].ToString();
            //    lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
            //    if (string.IsNullOrEmpty(PrintAssignerSettingManager[0]["SignUrl"].ToString()))
            //    {
            //        xrPicSign.Visible = false;
            //        xrlblWarning.Visible = false;
            //    }
            //    else
            //    {
            //        xrPicSign.Visible = true;
            //        xrlblWarning.Visible = true;
            //        xrPicSign.ImageUrl = PrintAssignerSettingManager[0]["SignUrl"].ToString();
            //    }
            //}

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                xrLMeId.Text = meid.ToString();
                xrLMeName.Text = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                xrLFileNo.Text = MemberManager[0]["FileNo"].ToString();
            }
        }

    }
}
