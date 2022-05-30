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
    public partial class MemberExamReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MemberExamReport(int MeId, int MFId)
        {
            LoadData(MeId, MFId);
        }

        public MemberExamReport(int MeId, int MFId, bool IsBrief)
        {
            LoadData(MeId, MFId);
            if (IsBrief)
            {
                xrTable3.DeleteColumn(xrTUserCode);
                xrTable3.DeleteColumn(xrTEntranceCode);
                xrTable2.DeleteColumn(xrTUserCodeTitr);
                xrTable2.DeleteColumn(xrTEntranceCodeTitr);

            }
        }
        private void LoadData(int MeId, int MFId)
        {
            InitializeComponent();
            TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new DataManager.DocMemberExamDetailManager();
            DataTable dt = DocMemberExamDetailManager.SelectByMFId(MFId, MeId);
            if (dt.DefaultView.Count > 0)
            {
                xrTMjName.DataBindings.Add("Text", dt, "MjName");
                xrTUserCode.DataBindings.Add("Text", dt, "UserCode");
                xrTEntranceCode.DataBindings.Add("Text", dt, "EntranceCode");
                xrTGrade.DataBindings.Add("Text", dt, "GrdName");
                xrTTTypeName.DataBindings.Add("Text", dt, "TTypeName");
                xrTTitle.DataBindings.Add("Text", dt, "Title");
                xrTPoint.DataBindings.Add("Text", dt, "Point");
                xrTInActiveStatus.DataBindings.Add("Text", dt, "InActiveStatus");
                TRowExamImage.DataBindings.Add("NavigateUrl", dt, "FileURL");
                TRowExamImage.DataBindings.Add("Text", dt, "HasFileURL");
                TRowExamConfirmImageURL.DataBindings.Add("NavigateUrl", dt, "ExamConfirmImageURL");
                TRowExamConfirmImageURL.DataBindings.Add("Text", dt, "HasExamConfirmImageURL");
                TRowPeriodImage.DataBindings.Add("NavigateUrl", dt, "PeriodImgURL");
                TRowPeriodImage.DataBindings.Add("Text", dt, "HasPeriodImgURL");
                xrTReqType.DataBindings.Add("Text", dt, "MFType");

                this.DataSource = dt;
            }
            this.BeforePrint += MemberExamReport_BeforePrint;
        }

        private void MemberExamReport_BeforePrint(object sender, PrintEventArgs e)
        {
            FormattingRule formattingRuleExamImage = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleExamImage});
            formattingRuleExamImage.Name = "formattingRuleExamImage";
            formattingRuleExamImage.DataSource = this.DataSource;
            formattingRuleExamImage.DataMember = this.DataMember;
            formattingRuleExamImage.Condition = "[ColorFileURL] > 0";
            formattingRuleExamImage.Formatting.BackColor = Color.GreenYellow;
            TRowExamImage.FormattingRules.Add(formattingRuleExamImage);


            FormattingRule formattingRuleExamConfirmImage = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleExamConfirmImage});
            formattingRuleExamConfirmImage.Name = "formattingRuleExamConfirmImage";
            formattingRuleExamConfirmImage.DataSource = this.DataSource;
            formattingRuleExamConfirmImage.DataMember = this.DataMember;
            formattingRuleExamConfirmImage.Condition = "[ColorExamConfirmImageURL] > 0";
            formattingRuleExamConfirmImage.Formatting.BackColor = Color.GreenYellow;
            TRowExamConfirmImageURL.FormattingRules.Add(formattingRuleExamConfirmImage);


            FormattingRule formattingRulePeriodImage = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRulePeriodImage});
            formattingRulePeriodImage.Name = "formattingRulePeriodImage";
            formattingRulePeriodImage.DataSource = this.DataSource;
            formattingRulePeriodImage.DataMember = this.DataMember;
            formattingRulePeriodImage.Condition = "[ColorPeriodImgURL] > 0";
            formattingRulePeriodImage.Formatting.BackColor = Color.GreenYellow;
            TRowPeriodImage.FormattingRules.Add(formattingRulePeriodImage);
        }
    }
}
