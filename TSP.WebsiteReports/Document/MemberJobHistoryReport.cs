using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class MemberJobHistoryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MemberJobHistoryReport(int MeId, int MFId)
        {
            InitializeComponent();

            TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
            DataTable dt = ProjectJobHistoryManager.SelectByTableType(MeId, (int)TSP.DataManager.TableCodes.DocMemberFile, MFId, 0);

            if (dt.Rows.Count > 0)
            {
                xrTProjectName.DataBindings.Add("Text", dt, "ProjectName");
                xrTTypeName.DataBindings.Add("Text", dt, "TypeName");
                xrTEmployer.DataBindings.Add("Text", dt, "Employer");
                xrTStartCorporateDate.DataBindings.Add("Text", dt, "StartCorporateDate");
                xrTEndCorporateDate.DataBindings.Add("Text", dt, "EndCorporateDate");
                xrTCitName.DataBindings.Add("Text", dt, "CitName");
                xrTTtName.DataBindings.Add("Text", dt, "TtName");
                xrTInActiveName.DataBindings.Add("Text", dt, "InActiveName");

                this.DataSource = dt;
            }
        }

    }
}
