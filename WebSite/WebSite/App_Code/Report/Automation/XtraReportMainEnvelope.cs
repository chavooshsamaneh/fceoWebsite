using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for XtraReportMainEnvelope
/// </summary>
public class XtraReportMainEnvelope : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRSubreport xrSubMember;
    private XRSubreport xrSubOffice;
    private XRSubreport xrSubOrganization;
    private XRSubreport xrSubEmployee;
    private XRSubreport xrSubOtherPerson;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportMainEnvelope(int LetterId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        String SenderAddress = String.Empty;
        TSP.DataManager.Automation.LetterRecieversManager LetterRecieversManager = new TSP.DataManager.Automation.LetterRecieversManager();
        TSP.DataManager.Automation.SecretariatManager secretariatManager = new TSP.DataManager.Automation.SecretariatManager();
        TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();
        LettersManager.FindById(LetterId);
        if (LettersManager.Count == 1)
        {
            secretariatManager.FindById(Convert.ToInt32(LettersManager[0]["Secretariat"]));
            if (secretariatManager.Count == 1)
                SenderAddress = secretariatManager[0]["Address"].ToString() + " - " + secretariatManager[0]["SName"].ToString();
        }

        DataTable dtRecieversEmp = LetterRecieversManager.FindByLetterId(LetterId, (int)TSP.DataManager.AutomationLetterRecieverTypes.Employee);
        if (dtRecieversEmp.Rows.Count > 0)
        {
            XtraReportEnvelope XtraReportEnvEmployee = new XtraReportEnvelope(dtRecieversEmp.Copy(), SenderAddress);
            xrSubEmployee.ReportSource = XtraReportEnvEmployee;
        }

        DataTable dtRecieversMe = LetterRecieversManager.FindByLetterId(LetterId, (int)TSP.DataManager.AutomationLetterRecieverTypes.Member);
        if (dtRecieversMe.Rows.Count > 0)
        {
            XtraReportEnvelope XtraReportEnvMember = new XtraReportEnvelope(dtRecieversMe.Copy(), SenderAddress);
            xrSubMember.ReportSource = XtraReportEnvMember;
        }

        DataTable dtRecieversOff = LetterRecieversManager.FindByLetterId(LetterId, (int)TSP.DataManager.AutomationLetterRecieverTypes.Office);
        if (dtRecieversOff.Rows.Count > 0)
        {
            XtraReportEnvelope XtraReportEnvOffice = new XtraReportEnvelope(dtRecieversOff.Copy(), SenderAddress);
            xrSubOffice.ReportSource = XtraReportEnvOffice;
        }

        DataTable dtRecieversOrg = LetterRecieversManager.FindByLetterId(LetterId, (int)TSP.DataManager.AutomationLetterRecieverTypes.Organization);
        if (dtRecieversOrg.Rows.Count > 0)
        {
            XtraReportEnvelope XtraReportEnvOrganization = new XtraReportEnvelope(dtRecieversOrg.Copy(), SenderAddress);
            xrSubOrganization.ReportSource = XtraReportEnvOrganization;
        }

        DataTable dtRecieversOtp = LetterRecieversManager.FindByLetterId(LetterId, (int)TSP.DataManager.AutomationLetterRecieverTypes.OtherPerson);
        if (dtRecieversOtp.Rows.Count > 0)
        {
            XtraReportEnvelope XtraReportEnvOtherPerson = new XtraReportEnvelope(dtRecieversOtp.Copy(), SenderAddress);
            xrSubOtherPerson.ReportSource = XtraReportEnvOtherPerson;
        }
    }

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
        string resourceFileName = "XtraReportMainEnvelope.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrSubOtherPerson = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubEmployee = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubOrganization = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubOffice = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubMember = new DevExpress.XtraReports.UI.XRSubreport();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubOtherPerson,
            this.xrSubEmployee,
            this.xrSubOrganization,
            this.xrSubOffice,
            this.xrSubMember});
        this.Detail.HeightF = 207.2917F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrSubOtherPerson
        // 
        this.xrSubOtherPerson.LocationFloat = new DevExpress.Utils.PointFloat(0F, 184F);
        this.xrSubOtherPerson.Name = "xrSubOtherPerson";
        this.xrSubOtherPerson.SizeF = new System.Drawing.SizeF(223.9583F, 22.99999F);
        // 
        // xrSubEmployee
        // 
        this.xrSubEmployee.LocationFloat = new DevExpress.Utils.PointFloat(0F, 138F);
        this.xrSubEmployee.Name = "xrSubEmployee";
        this.xrSubEmployee.SizeF = new System.Drawing.SizeF(223.9583F, 22.99999F);
        // 
        // xrSubOrganization
        // 
        this.xrSubOrganization.LocationFloat = new DevExpress.Utils.PointFloat(0F, 92F);
        this.xrSubOrganization.Name = "xrSubOrganization";
        this.xrSubOrganization.SizeF = new System.Drawing.SizeF(223.9583F, 23F);
        // 
        // xrSubOffice
        // 
        this.xrSubOffice.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46F);
        this.xrSubOffice.Name = "xrSubOffice";
        this.xrSubOffice.SizeF = new System.Drawing.SizeF(223.9583F, 23F);
        // 
        // xrSubMember
        // 
        this.xrSubMember.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrSubMember.Name = "xrSubMember";
        this.xrSubMember.SizeF = new System.Drawing.SizeF(223.9583F, 23F);
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 93F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // XtraReportMainEnvelope
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Margins = new System.Drawing.Printing.Margins(100, 93, 93, 100);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

}
