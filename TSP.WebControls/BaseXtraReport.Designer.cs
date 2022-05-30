namespace TSP.WebControls
{
    partial class BaseXtraReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MasterDetail = new DevExpress.XtraReports.UI.DetailBand();
            this.MasterPageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.MasterPageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // MasterDetail
            // 
            this.MasterDetail.Name = "MasterDetail";
            this.MasterDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.MasterDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // MasterPageHeader
            // 
            this.MasterPageHeader.Name = "MasterPageHeader";
            // 
            // MasterPageFooter
            // 
            this.MasterPageFooter.Name = "MasterPageFooter";
            // 
            // BaseXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.MasterDetail,
            this.MasterPageHeader,
            this.MasterPageFooter});
            this.Name = "BaseXtraReport";
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "9.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand MasterDetail;
        private DevExpress.XtraReports.UI.PageHeaderBand MasterPageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand MasterPageFooter;

    }
}
