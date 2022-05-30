using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportPeriodLicence1
/// </summary>
public class XtraReportPeriodLicence1 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRLabel xrLabelPPCode;
    private XRLabel xrLabelPPTeFileNo;
    private XRLabel xrLabelPPTeacherName;
    private XRLabel xrLabelPPTestDate;
    private XRLabel xrLabelInsRegDate;
    private XRLabel xrLabelInsName;
    private XRLabel xrLabelPPDuration;
    private XRLabel xrLabelPPEndDate;
    private XRLabel xrLabelFileNo;
    private XRLabel xrLabelMajor;
    private XRLabel xrLabelPPStartDate;
    private XRLabel xrLabelPPName;
    private XRLabel xrLabelInsRegNo;
    private XRLabel xrLabelPaye;
    private XRLabel xrLabelProvince;
    private XRLabel xrLabelInsPrId;
    private XRLabel xrLabelMeId;
    private XRLabel xrLabelNumber;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportPeriodLicence1()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
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
        string resourceFileName = "XtraReportPeriodLicence1.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelInsPrId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelProvince = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPaye = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelInsRegNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPStartDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMajor = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelFileNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPEndDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPDuration = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelInsName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelInsRegDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPTestDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPTeacherName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPTeFileNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPPCode = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelNumber = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMeId = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelMeId,
            this.xrLabelNumber,
            this.xrLabelPPCode,
            this.xrLabelPPTeFileNo,
            this.xrLabelPPTeacherName,
            this.xrLabelPPTestDate,
            this.xrLabelInsRegDate,
            this.xrLabelInsName,
            this.xrLabelPPDuration,
            this.xrLabelPPEndDate,
            this.xrLabelFileNo,
            this.xrLabelMajor,
            this.xrLabelPPStartDate,
            this.xrLabelPPName,
            this.xrLabelInsRegNo,
            this.xrLabelPaye,
            this.xrLabelProvince,
            this.xrLabelInsPrId});
        this.Detail.Height = 800;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabelInsPrId
        // 
        this.xrLabelInsPrId.Location = new System.Drawing.Point(225, 58);
        this.xrLabelInsPrId.Name = "xrLabelInsPrId";
        this.xrLabelInsPrId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelInsPrId.Size = new System.Drawing.Size(175, 25);
        this.xrLabelInsPrId.StylePriority.UseTextAlignment = false;
        this.xrLabelInsPrId.Text = "xrLabelInsPrId";
        this.xrLabelInsPrId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelProvince
        // 
        this.xrLabelProvince.Location = new System.Drawing.Point(0, 50);
        this.xrLabelProvince.Multiline = true;
        this.xrLabelProvince.Name = "xrLabelProvince";
        this.xrLabelProvince.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelProvince.Size = new System.Drawing.Size(117, 40);
        this.xrLabelProvince.StylePriority.UseTextAlignment = false;
        this.xrLabelProvince.Text = "xrLabelProvince";
        this.xrLabelProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPaye
        // 
        this.xrLabelPaye.Location = new System.Drawing.Point(217, 125);
        this.xrLabelPaye.Name = "xrLabelPaye";
        this.xrLabelPaye.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPaye.Size = new System.Drawing.Size(283, 25);
        this.xrLabelPaye.StylePriority.UseTextAlignment = false;
        this.xrLabelPaye.Text = "xrLabelPaye";
        this.xrLabelPaye.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelInsRegNo
        // 
        this.xrLabelInsRegNo.Location = new System.Drawing.Point(125, 175);
        this.xrLabelInsRegNo.Name = "xrLabelInsRegNo";
        this.xrLabelInsRegNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelInsRegNo.Size = new System.Drawing.Size(175, 25);
        this.xrLabelInsRegNo.StylePriority.UseTextAlignment = false;
        this.xrLabelInsRegNo.Text = "xrLabelInsRegNo";
        this.xrLabelInsRegNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPName
        // 
        this.xrLabelPPName.Location = new System.Drawing.Point(350, 183);
        this.xrLabelPPName.Name = "xrLabelPPName";
        this.xrLabelPPName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPName.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPName.StylePriority.UseTextAlignment = false;
        this.xrLabelPPName.Text = "xrLabelPPName";
        this.xrLabelPPName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPStartDate
        // 
        this.xrLabelPPStartDate.Location = new System.Drawing.Point(575, 175);
        this.xrLabelPPStartDate.Name = "xrLabelPPStartDate";
        this.xrLabelPPStartDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPStartDate.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPStartDate.StylePriority.UseTextAlignment = false;
        this.xrLabelPPStartDate.Text = "xrLabelPPStartDate";
        this.xrLabelPPStartDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelMajor
        // 
        this.xrLabelMajor.Location = new System.Drawing.Point(500, 108);
        this.xrLabelMajor.Name = "xrLabelMajor";
        this.xrLabelMajor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelMajor.Size = new System.Drawing.Size(175, 25);
        this.xrLabelMajor.StylePriority.UseTextAlignment = false;
        this.xrLabelMajor.Text = "xrLabelMajor";
        this.xrLabelMajor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelFileNo
        // 
        this.xrLabelFileNo.Location = new System.Drawing.Point(467, 0);
        this.xrLabelFileNo.Name = "xrLabelFileNo";
        this.xrLabelFileNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelFileNo.Size = new System.Drawing.Size(225, 33);
        this.xrLabelFileNo.StylePriority.UseTextAlignment = false;
        this.xrLabelFileNo.Text = "xrLabelFileNo";
        this.xrLabelFileNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPEndDate
        // 
        this.xrLabelPPEndDate.Location = new System.Drawing.Point(567, 225);
        this.xrLabelPPEndDate.Name = "xrLabelPPEndDate";
        this.xrLabelPPEndDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPEndDate.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPEndDate.StylePriority.UseTextAlignment = false;
        this.xrLabelPPEndDate.Text = "xrLabelPPEndDate";
        this.xrLabelPPEndDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPDuration
        // 
        this.xrLabelPPDuration.Location = new System.Drawing.Point(525, 267);
        this.xrLabelPPDuration.Name = "xrLabelPPDuration";
        this.xrLabelPPDuration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPDuration.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPDuration.StylePriority.UseTextAlignment = false;
        this.xrLabelPPDuration.Text = "xrLabelPPDuration";
        this.xrLabelPPDuration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelInsName
        // 
        this.xrLabelInsName.Location = new System.Drawing.Point(317, 225);
        this.xrLabelInsName.Name = "xrLabelInsName";
        this.xrLabelInsName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelInsName.Size = new System.Drawing.Size(175, 25);
        this.xrLabelInsName.StylePriority.UseTextAlignment = false;
        this.xrLabelInsName.Text = "xrLabelInsName";
        this.xrLabelInsName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelInsRegDate
        // 
        this.xrLabelInsRegDate.Location = new System.Drawing.Point(117, 242);
        this.xrLabelInsRegDate.Name = "xrLabelInsRegDate";
        this.xrLabelInsRegDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelInsRegDate.Size = new System.Drawing.Size(175, 25);
        this.xrLabelInsRegDate.StylePriority.UseTextAlignment = false;
        this.xrLabelInsRegDate.Text = "xrLabelInsRegDate";
        this.xrLabelInsRegDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPTestDate
        // 
        this.xrLabelPPTestDate.Location = new System.Drawing.Point(0, 283);
        this.xrLabelPPTestDate.Name = "xrLabelPPTestDate";
        this.xrLabelPPTestDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPTestDate.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPTestDate.StylePriority.UseTextAlignment = false;
        this.xrLabelPPTestDate.Text = "xrLabelPPTestDate";
        this.xrLabelPPTestDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelPPTeacherName
        // 
        this.xrLabelPPTeacherName.Location = new System.Drawing.Point(192, 342);
        this.xrLabelPPTeacherName.Name = "xrLabelPPTeacherName";
        this.xrLabelPPTeacherName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPTeacherName.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPTeacherName.StylePriority.UseTextAlignment = false;
        this.xrLabelPPTeacherName.Text = "xrLabelPPTeacherName";
        this.xrLabelPPTeacherName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPPTeFileNo
        // 
        this.xrLabelPPTeFileNo.Location = new System.Drawing.Point(358, 375);
        this.xrLabelPPTeFileNo.Name = "xrLabelPPTeFileNo";
        this.xrLabelPPTeFileNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPTeFileNo.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPTeFileNo.StylePriority.UseTextAlignment = false;
        this.xrLabelPPTeFileNo.Text = "xrLabelPPTeFileNo";
        this.xrLabelPPTeFileNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPPCode
        // 
        this.xrLabelPPCode.Location = new System.Drawing.Point(425, 433);
        this.xrLabelPPCode.Name = "xrLabelPPCode";
        this.xrLabelPPCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPPCode.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPPCode.StylePriority.UseTextAlignment = false;
        this.xrLabelPPCode.Text = "xrLabelPPCode";
        this.xrLabelPPCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelNumber
        // 
        this.xrLabelNumber.Location = new System.Drawing.Point(50, 100);
        this.xrLabelNumber.Name = "xrLabelNumber";
        this.xrLabelNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelNumber.Size = new System.Drawing.Size(138, 24);
        this.xrLabelNumber.StylePriority.UseTextAlignment = false;
        this.xrLabelNumber.Text = "xrLabelNumber";
        this.xrLabelNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelMeId
        // 
        this.xrLabelMeId.Location = new System.Drawing.Point(17, 358);
        this.xrLabelMeId.Name = "xrLabelMeId";
        this.xrLabelMeId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelMeId.Size = new System.Drawing.Size(138, 24);
        this.xrLabelMeId.StylePriority.UseTextAlignment = false;
        this.xrLabelMeId.Text = "xrLabelMeId";
        this.xrLabelMeId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportPeriodLicence1
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Name = "XtraReportPeriodLicence1";
        this.PageHeight = 1100;
        this.PageWidth = 850;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
