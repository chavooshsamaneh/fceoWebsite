using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace TSP.WebsiteReports.Document
{
    public partial class MemberPeriodsReport : DevExpress.XtraReports.UI.XtraReport
    {


        public MemberPeriodsReport(int MeId, string RequestDate)
        {
            LoadData(MeId, RequestDate);
        }

        public MemberPeriodsReport(int MeId, bool IsBrief, string RequestDate)
        {
            LoadData(MeId, RequestDate);
            if (IsBrief)
            {
                xrTable2.DeleteColumn(xrTMarkTitr);
                xrTable2.DeleteColumn(xrTCreateDateTitr);
                xrTable3.DeleteColumn(xrTResName);
                xrTable3.DeleteColumn(xrTTestDate);
            }

        }

        private void LoadData(int MeId, string RequestDate)
        {
            InitializeComponent();

            //SelectMethod = "selectPeriodRegisterForMemberUpgrade" TypeName = "TSP.DataManager.PeriodRegisterManager" >
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new DataManager.PeriodRegisterManager();
            DataTable dt = PeriodRegisterManager.selectPeriodRegisterForMemberReport(MeId, -1, -1, -1, 0, RequestDate);
            if (dt.Rows.Count > 0)
            {
                xrTCrsNameAndHour.DataBindings.Add("NavigateUrl", dt, "PreviewURL");
                xrTCrsNameAndHour.DataBindings.Add("Text", dt, "CrsNameAndHour");
                xrTGrdName.DataBindings.Add("Text", dt, "GrdName");
                xrTMjName.DataBindings.Add("Text", dt, "MjName");
                xrTResName.DataBindings.Add("Text", dt, "ResName");
                xrTTestDate.DataBindings.Add("Text", dt, "TestDate");
                CheckBoxISSpent.DataBindings.Add("Checked", dt, "ISSpent");
                this.DataSource = dt;
            }
            this.BeforePrint += MemberPeriodsReport_BeforePrint;
        }


        private void MemberPeriodsReport_BeforePrint(object sender, PrintEventArgs e)
        {
            // Create a new rule and add it to a report.

            FormattingRule formattingRuleMeMadrak = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleMeMadrak});
            formattingRuleMeMadrak.Name = "formattingRuleMeMadrak";
            // Specify the rule's properties.
            formattingRuleMeMadrak.DataSource = this.DataSource;
            formattingRuleMeMadrak.DataMember = this.DataMember;
            //formattingRule1.Condition = "[DataSource.RowCount]>0";
            formattingRuleMeMadrak.Condition = "[color] > 0";
            formattingRuleMeMadrak.Formatting.BackColor = Color.LightCoral;

            // Apply this rule to the detail band.
            this.Detail.FormattingRules.Add(formattingRuleMeMadrak);
          



        }
    }
}
