using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class MemberLicenceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MemberLicenceReport(int MeId, int MFId)
        {
            InitializeComponent();

            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            DataTable dtMajor = DocMemberFileMajorManager.SelectMemberFileById(MFId, MeId, -1);

            if (dtMajor.Rows.Count > 0)
            {
                xrTMlName.DataBindings.Add("Text", dtMajor, "MlName");
                xrTFMjName.DataBindings.Add("Text", dtMajor, "FMjName");
                xrTMjType.DataBindings.Add("Text", dtMajor, "MjType");
               // xrTUnCount.DataBindings.Add("Text", dtMajor, "UnCount");
                xrTUnName.DataBindings.Add("Text", dtMajor, "UnName");
                xrTUnEndDate.DataBindings.Add("Text", dtMajor, "EndDate");
                xrTUnGrade.DataBindings.Add("Text", dtMajor, "UnGrade");
                xrTInActives.DataBindings.Add("Text", dtMajor, "InActives");

                this.DataSource = dtMajor;
            }
        }

    }
}
