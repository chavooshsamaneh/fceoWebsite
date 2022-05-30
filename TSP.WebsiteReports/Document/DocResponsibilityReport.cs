using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class DocResponsibilityReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DocResponsibilityReport(int MFId, int MeId)
        {
            InitializeComponent();

            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new DataManager.DocMemberFileDetailManager();
            DataTable dt = DocMemberFileDetailManager.SelectById(MFId, MeId, 0);
            if (dt.Rows.Count > 0)
            {
                xrTMjName.DataBindings.Add("Text", dt, "MjName");
                xrTGMRName.DataBindings.Add("Text", dt, "GMRName");
                xrTActTypeName.DataBindings.Add("Text", dt, "ActTypeName");
                xrTDate.DataBindings.Add("Text", dt, "Date");
                xrTInActives.DataBindings.Add("Text", dt, "InActives");
                xrTRespDateDiff.DataBindings.Add("Text", dt, "RespDateDiff");
                xrTCanUpgrade.DataBindings.Add("Text", dt, "CanUpgrade");
                this.DataSource = dt;
            }
        }

    }
}
