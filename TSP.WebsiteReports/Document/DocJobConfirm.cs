using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class DocJobConfirm : DevExpress.XtraReports.UI.XtraReport
    {
        public DocJobConfirm(int MFId)
        {
            LoadData(MFId);
        }

        public DocJobConfirm(int MFId,bool IsBrief)
        {
            LoadData(MFId);
            if (IsBrief)
            {
                //xrTable3.DeleteColumn(xrTMeId);
                //xrTable3.DeleteColumn(xrTName);
                xrTable3.DeleteColumn(xrTMFNo);

                //xrTable2.DeleteColumn(xrTMeIdTitr);
                //xrTable2.DeleteColumn(xrTNameTitr);
                xrTable2.DeleteColumn(xrTMFNoTitr);
            }
        }
        private void LoadData(int MFId)
        {
            InitializeComponent();
            TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
            System.Data.DataTable dtJobConfirm = DocMemberFileJobConfirmationManager.FindByMfId(MFId);

            if (dtJobConfirm.Rows.Count > 0)
            {
                xrTConfirmTypeName.DataBindings.Add("Text", dtJobConfirm, "ConfirmTypeName");
                xrTMeId.DataBindings.Add("Text", dtJobConfirm, "MeId");
                xrTName.DataBindings.Add("Text", dtJobConfirm, "Name");
                xrTMFNo.DataBindings.Add("Text", dtJobConfirm, "MFNo");
                xrTReqType.DataBindings.Add("Text", dtJobConfirm, "MFType");   
                xrTDescription.DataBindings.Add("Text", dtJobConfirm, "Description");

                xrTrFromDate.DataBindings.Add("Text", dtJobConfirm, "FromDate");
                xrTrEndDate.DataBindings.Add("Text", dtJobConfirm, "ToDate");
                xrTrPosition.DataBindings.Add("Text", dtJobConfirm, "Position");
                xrTJobDateDiff.DataBindings.Add("Text", dtJobConfirm, "JobDateDiff");

                TRowFileImage.DataBindings.Add("NavigateUrl", dtJobConfirm, "FileURL");
                TRowFileImage.DataBindings.Add("Text", dtJobConfirm, "HasFileURL");

                TRowGradeImage.DataBindings.Add("NavigateUrl", dtJobConfirm, "GrdURL");                
                TRowGradeImage.DataBindings.Add("Text", dtJobConfirm, "HasGrdURL");

                xrTInActiveName.DataBindings.Add("Text", dtJobConfirm, "InActives");

                this.DataSource = dtJobConfirm;
            }
            this.BeforePrint += DocJobConfirm_BeforePrint;
        }

        private void DocJobConfirm_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            FormattingRule formattingRuleFileImage = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleFileImage});
            formattingRuleFileImage.Name = "formattingRuleFileImage";
            formattingRuleFileImage.DataSource = this.DataSource;
            formattingRuleFileImage.DataMember = this.DataMember;
            formattingRuleFileImage.Condition = "[ColorFileURL] > 0";
            formattingRuleFileImage.Formatting.BackColor = Color.GreenYellow;
            TRowFileImage.FormattingRules.Add(formattingRuleFileImage);


            FormattingRule formattingRuleGrdURL = new FormattingRule();
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            formattingRuleGrdURL});
            formattingRuleGrdURL.Name = "formattingRuleGrdURL";
            formattingRuleGrdURL.DataSource = this.DataSource;
            formattingRuleGrdURL.DataMember = this.DataMember;
            formattingRuleGrdURL.Condition = "[ColorGrdURL] > 0";
            formattingRuleGrdURL.Formatting.BackColor = Color.GreenYellow;
            TRowGradeImage.FormattingRules.Add(formattingRuleGrdURL); 
        }
    }
}
