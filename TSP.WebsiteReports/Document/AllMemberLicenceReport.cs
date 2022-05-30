using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Drawing.Printing;

namespace TSP.WebsiteReports.Document
{
    public partial class AllMemberLicenceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AllMemberLicenceReport(int MeId,bool IsBrief)
        {
            LoadData(MeId);
            if (IsBrief)
            {
                xrTable3.DeleteColumn(xrTAvg);
                xrTable3.DeleteColumn(xrTInquiry);
                xrTable3.DeleteColumn(xrTconfirm);
                xrTable3.DeleteColumn(TRowScoresImageURL);
                xrTable3.DeleteColumn(TRowEquivalentImageURL);

                xrTable2.DeleteColumn(xrTAvgTitr);
                xrTable2.DeleteColumn(xrTInquiryTitr);
                xrTable2.DeleteColumn(xrTconfirmTitr);
                xrTable2.DeleteColumn(TRowScoresImageURLTitr);
                xrTable2.DeleteColumn(XRowEquivalentImageURLTitr);
            }
        }

        public AllMemberLicenceReport(int MeId)
        {
            LoadData(MeId);
        }
        private void LoadData(int MeId)
        {
            InitializeComponent();
            TSP.DataManager.MemberLicenceManager MeLiManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dt = MeLiManager.SelectMemberLicence(MeId, -1, 1, 0, -1); //SelectByMemberId(MeId, 0);            

            xrTDefaultValue.DataBindings.Add("Text", dt, "DefaultValueName");
            xrTLiName.DataBindings.Add("Text", dt, "LiName");
            xrTMjName.DataBindings.Add("Text", dt, "MjName");
            xrTUnName.DataBindings.Add("Text", dt, "UnName");
            xrTEndDate.DataBindings.Add("Text", dt, "EndDate");
            xrTAvg.DataBindings.Add("Text", dt, "Avg");
            xrTAvg.DataBindings["Text"].FormatString = "{0:0.##}";
            xrTInquiry.DataBindings.Add("Text", dt, "Inquiry");
            xrTconfirm.DataBindings.Add("Text", dt, "confirm");
            TRowLicenceImage.DataBindings.Add("NavigateUrl", dt, "FilePath");  
            TRowLicenceImage.DataBindings.Add("Text", dt, "HasFilePath");
            TRowLicenceInqueryImage.DataBindings.Add("NavigateUrl", dt, "InquiryImageURL");
            TRowLicenceInqueryImage.DataBindings.Add("Text", dt, "HasInquiryImage");
            TRowScoresImageURL.DataBindings.Add("NavigateUrl", dt, "ScoresImageURL");
            TRowScoresImageURL.DataBindings.Add("Text", dt, "HasScoreImage");
            TRowEquivalentImageURL.DataBindings.Add("NavigateUrl", dt, "EquivalentImageURL");
            TRowEquivalentImageURL.DataBindings.Add("Text", dt, "HasEquivalentImageURL");
            TRowEntranceExamConfImageURL.DataBindings.Add("NavigateUrl", dt, "EntranceExamConfImageURL");
            TRowEntranceExamConfImageURL.DataBindings.Add("Text", dt, "HasEntranceExamConfImageURL");

            this.DataSource = dt;
           this.BeforePrint += MemberPeriodsReport_BeforePrint;
        }
        private void MemberPeriodsReport_BeforePrint(object sender, PrintEventArgs e)
        {
            FormattingRule formattingRuleMeMadrak = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleMeMadrak});
            formattingRuleMeMadrak.Name = "formattingRuleMeMadrak";            
            formattingRuleMeMadrak.DataSource = this.DataSource;
            formattingRuleMeMadrak.DataMember = this.DataMember;
            formattingRuleMeMadrak.Condition = "[ColorFilePath] > 0";            
            formattingRuleMeMadrak.Formatting.BackColor = Color.GreenYellow;            
            TRowLicenceImage.FormattingRules.Add(formattingRuleMeMadrak);

            FormattingRule formattingRuleInquery = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleInquery});
            formattingRuleInquery.Name = "formattingRuleInquery";            
            formattingRuleInquery.DataSource = this.DataSource;
            formattingRuleInquery.DataMember = this.DataMember;
            formattingRuleInquery.Condition = "[ColorInquiryImage] > 0";
            formattingRuleInquery.Formatting.BackColor = Color.GreenYellow;
            TRowLicenceInqueryImage.FormattingRules.Add(formattingRuleInquery);

            
            FormattingRule formattingRuleMeMadrakScoreImage = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleMeMadrakScoreImage});
            formattingRuleMeMadrakScoreImage.Name = "formattingRuleMeMadrakScoreImage";            
            formattingRuleMeMadrakScoreImage.DataSource = this.DataSource;
            formattingRuleMeMadrakScoreImage.DataMember = this.DataMember;
            formattingRuleMeMadrakScoreImage.Condition = "[ColorScoreImage] > 0";
            formattingRuleMeMadrakScoreImage.Formatting.BackColor = Color.GreenYellow;
            TRowScoresImageURL.FormattingRules.Add(formattingRuleMeMadrakScoreImage);

            FormattingRule formattingRuleMeMadrakEquivalentImageURL = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleMeMadrakEquivalentImageURL});
            formattingRuleMeMadrakEquivalentImageURL.Name = "formattingRuleMeMadrakEquivalentImageURL";            
            formattingRuleMeMadrakEquivalentImageURL.DataSource = this.DataSource;
            formattingRuleMeMadrakEquivalentImageURL.DataMember = this.DataMember;
            formattingRuleMeMadrakEquivalentImageURL.Condition = "[ColorEquivalentImageURL] > 0";
            formattingRuleMeMadrakEquivalentImageURL.Formatting.BackColor = Color.GreenYellow;
            TRowEquivalentImageURL.FormattingRules.Add(formattingRuleMeMadrakEquivalentImageURL);

            FormattingRule formattingRulentranceExamConf = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRulentranceExamConf});
            formattingRulentranceExamConf.Name = "formattingRulentranceExamConf";            
            formattingRulentranceExamConf.DataSource = this.DataSource;
            formattingRulentranceExamConf.DataMember = this.DataMember;
            formattingRulentranceExamConf.Condition = "[ColorEntranceExamConfImageURL] > 0";
            formattingRulentranceExamConf.Formatting.BackColor = Color.GreenYellow;
            TRowEntranceExamConfImageURL.FormattingRules.Add(formattingRulentranceExamConf);
            
                

        }

    }
}
