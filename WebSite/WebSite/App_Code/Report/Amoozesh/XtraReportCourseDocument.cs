using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportCourseDocument
/// </summary>
public class XtraReportCourseDocument : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabelSSN;
    private XRLabel xrLabelGrade;
    private XRLabel xrLabelFather;
    private XRLabel xrLabelDocNo;
    private XRLabel xrLabelPersonName;
    private XRLabel xrLabelCourseName;
    private XRLabel xrLabelLicenceNo;
    private XRLabel xrLabelDate;
    private XRLabel xrLabelMajor;
    private XRLabel xrLabelOfficeName;
    private XRLabel xrLabelMemberNo;
    private XRLabel xrLabelCourseCode;
    private XRLabel xrLabelGraduateDate;
    private XRLabel xrLabelLearningResponsible;
    private XRLabel xrLabelLearningProvince;
    private XRLabel xrLabelMaskanResponsible;
    private XRLabel xrLabelLeraningPermissionNo;
    private XRLabel xrLabelFromDate;
    private XRLabel xrLabelProvince;
    private XRLabel xrLabelToDate;
    private XRLabel xrLabelLearningPermissionDate;
    private XRLabel xrLabelDuration;
    private XRTable xrTableTeachers;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell3;
    private XRTable xrTableTeachersFile;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell5;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportCourseDocument(int PPId, int MeId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        //TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        //DataTable dtCourseDoc = PeriodRegisterManager.SelectCourseDocReport(PPId, MeId);

        //if (dtCourseDoc.Rows.Count > 0)
        //{
        //    XtraReportEnvelope XtraReportEnvEmployee = new XtraReportEnvelope(dtRecieversEmp.Copy(), SenderAddress);
        //    xrSubEmployee.ReportSource = XtraReportEnvEmployee;
        //}

        //this.DataSource = dtCourseDoc;
        //lblName.DataBindings.Add("Text", dtCourseDoc, "EmpFullName");
        //lblReceiverAddress.DataBindings.Add("Text", dtCourseDoc, "EmpAddress");

        //ClearAllControls();
        //this.xrLabelPersonName.Text = GetCorrectText(memberView["MeName"]);
        //this.xrLabelFather.Text = GetCorrectText(memberView["FatherName"]);
        //this.xrLabelIdNo.Text = GetCorrectText(memberView["IdNo"]);
        //this.xrLabelDocNo.Text = GetCorrectText(memberView["FileNo"]);
        //this.xrLabelMajor.Text = GetCorrectText(memberView["LastMjName"]);

        //this.xrLabelGrade.Text = GetResGrade(memberView) + "-" + GetMaxGradeName(memberView);
        //this.xrLabelProvince.Text = this.xrLabelLearningProvince.Text = "";
        //this.xrLabelMemberNo.Text = GetCorrectText(memberView["MeId"]);

        //this.xrLabelCourseName.Text = GetCorrectText(courseView["CrsTitle"]);
        //this.xrLabelFromDate.Text = GetCorrectText(courseView["StartDate"]);
        //this.xrLabelToDate.Text = GetCorrectText(courseView["EndDate"]);
        //this.xrLabelDuration.Text = GetCorrectText(courseView["Duration"]);
        //this.xrLabelOfficeName.Text = GetCorrectText(courseView["InsName"]);
        //this.xrLabelLeraningPermissionNo.Text = GetCorrectText(courseView["RegNo"]);
        //this.xrLabelLearningPermissionDate.Text = GetCorrectText(courseView["RegDate"]);
        //this.xrLabelGraduateDate.Text = GetCorrectText(courseView["TestDate"]);



        //int? courseId = (int)courseView["PPId"];
        //spRepTeachers.Fill(dtTeachers, courseId);

        //for (int i = 0; i < this.dtTeachers.Count; i++)
        //{
        //    xrTableTeachers.Rows[i].Cells[0].Text = GetCorrectText(this.dtTeachers[i]["TeName"]);
        //    xrTableTeachersFile.Rows[i].Cells[0].Text = GetCorrectText(this.dtTeachers[i]["FileNo"]);
        //}
    }

    //private void ClearAllControls()
    //{

    //    for (int i = 0; i < this.xrTableTeachers.Rows.Count; i++)
    //        for (int j = 0; j < this.xrTableTeachers.Rows[i].Cells.Count; j++)
    //            xrTableTeachers.Rows[i].Cells[j].Text = "";

    //    for (int i = 0; i < this.xrTableTeachersFile.Rows.Count; i++)
    //        for (int j = 0; j < this.xrTableTeachersFile.Rows[i].Cells.Count; j++)
    //            xrTableTeachersFile.Rows[i].Cells[j].Text = "";

    //}
    //private string GetMaxGradeName(System.Data.DataRowView drv)
    //{
    //    string grade1 = "پایه یک";
    //    string grade2 = "پایه دو";
    //    string grade3 = "پایه سه";
    //    string arshad = "پایه ارشد";

    //    if (GetCorrectText(drv["ImpName"]).Contains("1") || GetCorrectText(drv["ObsName"]).Contains("1") ||
    //        GetCorrectText(drv["DesName"]).Contains("1"))
    //        return grade1;
    //    if (GetCorrectText(drv["ImpName"]).Contains("2") || GetCorrectText(drv["ObsName"]).Contains("2") ||
    //        GetCorrectText(drv["DesName"]).Contains("2"))
    //        return grade2;
    //    if (GetCorrectText(drv["ImpName"]).Contains("3") || GetCorrectText(drv["ObsName"]).Contains("3") ||
    //        GetCorrectText(drv["DesName"]).Contains("3"))
    //        return grade3;
    //    return "";
    //}
    //private string GetResGrade(System.Data.DataRowView drv)
    //{
    //    string res = "";
    //    if (!string.IsNullOrEmpty(GetCorrectText(drv["ObsName"])) && drv["ObsName"] != null)
    //        res = "نظارت";
    //    if (!string.IsNullOrEmpty(GetCorrectText(drv["DesName"])) && drv["DesName"] != null)
    //    {
    //        if (string.IsNullOrEmpty(res))
    //            res = "طراحی";
    //        else
    //            res += " وطراحی";
    //    }
    //    if (!string.IsNullOrEmpty(GetCorrectText(drv["ImpName"])) && drv["ImpName"] != null)
    //    {
    //        if (string.IsNullOrEmpty(res))
    //            res = "اجرا";
    //        else
    //            res += " واجرا";
    //    }
    //    return res;

    //}

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
        string resourceFileName = "XtraReportCourseDocument.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelSSN = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelGrade = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelFather = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelDocNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPersonName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelCourseName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelLicenceNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMajor = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelOfficeName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMemberNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelCourseCode = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelGraduateDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelLearningResponsible = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelLearningProvince = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMaskanResponsible = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelLeraningPermissionNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelFromDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelProvince = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelToDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelLearningPermissionDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelDuration = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrTableTeachers = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableTeachersFile = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachers)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachersFile)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrTableTeachersFile,
            this.xrTableTeachers,
            this.xrLabelSSN,
            this.xrLabelGrade,
            this.xrLabelFather,
            this.xrLabelDocNo,
            this.xrLabelPersonName,
            this.xrLabelCourseName,
            this.xrLabelLicenceNo,
            this.xrLabelDate,
            this.xrLabelMajor,
            this.xrLabelOfficeName,
            this.xrLabelMemberNo,
            this.xrLabelCourseCode,
            this.xrLabelGraduateDate,
            this.xrLabelLearningResponsible,
            this.xrLabelLearningProvince,
            this.xrLabelMaskanResponsible,
            this.xrLabelLeraningPermissionNo,
            this.xrLabelFromDate,
            this.xrLabelProvince,
            this.xrLabelToDate,
            this.xrLabelLearningPermissionDate,
            this.xrLabelDuration});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 2970F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabelSSN
        // 
        this.xrLabelSSN.Dpi = 254F;
        this.xrLabelSSN.LocationFloat = new DevExpress.Utils.PointFloat(947.2083F, 886.3542F);
        this.xrLabelSSN.Name = "xrLabelSSN";
        this.xrLabelSSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelSSN.SizeF = new System.Drawing.SizeF(286F, 58F);
        this.xrLabelSSN.StylePriority.UseTextAlignment = false;
        this.xrLabelSSN.Text = "کد ملی";
        this.xrLabelSSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelGrade
        // 
        this.xrLabelGrade.Dpi = 254F;
        this.xrLabelGrade.LocationFloat = new DevExpress.Utils.PointFloat(931.3333F, 981.6042F);
        this.xrLabelGrade.Name = "xrLabelGrade";
        this.xrLabelGrade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelGrade.SizeF = new System.Drawing.SizeF(444F, 58F);
        this.xrLabelGrade.StylePriority.UseTextAlignment = false;
        this.xrLabelGrade.Text = "پایه";
        this.xrLabelGrade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelFather
        // 
        this.xrLabelFather.Dpi = 254F;
        this.xrLabelFather.LocationFloat = new DevExpress.Utils.PointFloat(1629.833F, 886.3542F);
        this.xrLabelFather.Name = "xrLabelFather";
        this.xrLabelFather.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelFather.SizeF = new System.Drawing.SizeF(318F, 58F);
        this.xrLabelFather.StylePriority.UseTextAlignment = false;
        this.xrLabelFather.Text = "نام پدر";
        this.xrLabelFather.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDocNo
        // 
        this.xrLabelDocNo.Dpi = 254F;
        this.xrLabelDocNo.LocationFloat = new DevExpress.Utils.PointFloat(2180.166F, 981.6042F);
        this.xrLabelDocNo.Name = "xrLabelDocNo";
        this.xrLabelDocNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelDocNo.SizeF = new System.Drawing.SizeF(444F, 58F);
        this.xrLabelDocNo.StylePriority.UseTextAlignment = false;
        this.xrLabelDocNo.Text = "شماره پروانه";
        this.xrLabelDocNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPersonName
        // 
        this.xrLabelPersonName.Dpi = 254F;
        this.xrLabelPersonName.LocationFloat = new DevExpress.Utils.PointFloat(2074.333F, 886.3542F);
        this.xrLabelPersonName.Name = "xrLabelPersonName";
        this.xrLabelPersonName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelPersonName.SizeF = new System.Drawing.SizeF(444F, 58F);
        this.xrLabelPersonName.StylePriority.UseTextAlignment = false;
        this.xrLabelPersonName.Text = "نام شخص";
        this.xrLabelPersonName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelCourseName
        // 
        this.xrLabelCourseName.Dpi = 254F;
        this.xrLabelCourseName.LocationFloat = new DevExpress.Utils.PointFloat(296.3332F, 1087.438F);
        this.xrLabelCourseName.Name = "xrLabelCourseName";
        this.xrLabelCourseName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelCourseName.SizeF = new System.Drawing.SizeF(942F, 58F);
        this.xrLabelCourseName.StylePriority.UseTextAlignment = false;
        this.xrLabelCourseName.Text = "نام دوره";
        this.xrLabelCourseName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelLicenceNo
        // 
        this.xrLabelLicenceNo.Dpi = 254F;
        this.xrLabelLicenceNo.LocationFloat = new DevExpress.Utils.PointFloat(402.1668F, 425.9792F);
        this.xrLabelLicenceNo.Name = "xrLabelLicenceNo";
        this.xrLabelLicenceNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelLicenceNo.SizeF = new System.Drawing.SizeF(254F, 58F);
        this.xrLabelLicenceNo.StylePriority.UseTextAlignment = false;
        this.xrLabelLicenceNo.Text = "شماره";
        this.xrLabelLicenceNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDate
        // 
        this.xrLabelDate.Dpi = 254F;
        this.xrLabelDate.LocationFloat = new DevExpress.Utils.PointFloat(402.1668F, 505.3542F);
        this.xrLabelDate.Name = "xrLabelDate";
        this.xrLabelDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelDate.SizeF = new System.Drawing.SizeF(254F, 58F);
        this.xrLabelDate.StylePriority.UseTextAlignment = false;
        this.xrLabelDate.Text = "تاریخ";
        this.xrLabelDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelMajor
        // 
        this.xrLabelMajor.Dpi = 254F;
        this.xrLabelMajor.LocationFloat = new DevExpress.Utils.PointFloat(1486.958F, 981.6042F);
        this.xrLabelMajor.Name = "xrLabelMajor";
        this.xrLabelMajor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelMajor.SizeF = new System.Drawing.SizeF(444F, 58F);
        this.xrLabelMajor.StylePriority.UseTextAlignment = false;
        this.xrLabelMajor.Text = "رشته";
        this.xrLabelMajor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelOfficeName
        // 
        this.xrLabelOfficeName.Dpi = 254F;
        this.xrLabelOfficeName.LocationFloat = new DevExpress.Utils.PointFloat(296.3332F, 1240.896F);
        this.xrLabelOfficeName.Name = "xrLabelOfficeName";
        this.xrLabelOfficeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelOfficeName.SizeF = new System.Drawing.SizeF(799F, 58F);
        this.xrLabelOfficeName.StylePriority.UseTextAlignment = false;
        this.xrLabelOfficeName.Text = "نام موسسه";
        this.xrLabelOfficeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelMemberNo
        // 
        this.xrLabelMemberNo.Dpi = 254F;
        this.xrLabelMemberNo.LocationFloat = new DevExpress.Utils.PointFloat(1613.958F, 1087.438F);
        this.xrLabelMemberNo.Name = "xrLabelMemberNo";
        this.xrLabelMemberNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelMemberNo.SizeF = new System.Drawing.SizeF(323F, 58F);
        this.xrLabelMemberNo.StylePriority.UseTextAlignment = false;
        this.xrLabelMemberNo.Text = "شماره عضویت";
        this.xrLabelMemberNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelCourseCode
        // 
        this.xrLabelCourseCode.Dpi = 254F;
        this.xrLabelCourseCode.LocationFloat = new DevExpress.Utils.PointFloat(1994.958F, 1637.771F);
        this.xrLabelCourseCode.Name = "xrLabelCourseCode";
        this.xrLabelCourseCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelCourseCode.SizeF = new System.Drawing.SizeF(439F, 58F);
        this.xrLabelCourseCode.StylePriority.UseTextAlignment = false;
        this.xrLabelCourseCode.Text = "کد دوره";
        this.xrLabelCourseCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelGraduateDate
        // 
        this.xrLabelGraduateDate.Dpi = 254F;
        this.xrLabelGraduateDate.LocationFloat = new DevExpress.Utils.PointFloat(2211.916F, 1426.104F);
        this.xrLabelGraduateDate.Name = "xrLabelGraduateDate";
        this.xrLabelGraduateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelGraduateDate.SizeF = new System.Drawing.SizeF(228F, 58F);
        this.xrLabelGraduateDate.StylePriority.UseTextAlignment = false;
        this.xrLabelGraduateDate.Text = "تاریخ دوره";
        this.xrLabelGraduateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelLearningResponsible
        // 
        this.xrLabelLearningResponsible.Dpi = 254F;
        this.xrLabelLearningResponsible.LocationFloat = new DevExpress.Utils.PointFloat(1968.5F, 1828.271F);
        this.xrLabelLearningResponsible.Name = "xrLabelLearningResponsible";
        this.xrLabelLearningResponsible.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelLearningResponsible.SizeF = new System.Drawing.SizeF(482F, 58F);
        this.xrLabelLearningResponsible.StylePriority.UseTextAlignment = false;
        this.xrLabelLearningResponsible.Text = "رئیس دوره آموزش";
        this.xrLabelLearningResponsible.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabelLearningProvince
        // 
        this.xrLabelLearningProvince.Dpi = 254F;
        this.xrLabelLearningProvince.LocationFloat = new DevExpress.Utils.PointFloat(867.8333F, 1341.438F);
        this.xrLabelLearningProvince.Name = "xrLabelLearningProvince";
        this.xrLabelLearningProvince.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelLearningProvince.SizeF = new System.Drawing.SizeF(243F, 58F);
        this.xrLabelLearningProvince.StylePriority.UseTextAlignment = false;
        this.xrLabelLearningProvince.Text = "استان";
        this.xrLabelLearningProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelMaskanResponsible
        // 
        this.xrLabelMaskanResponsible.Dpi = 254F;
        this.xrLabelMaskanResponsible.LocationFloat = new DevExpress.Utils.PointFloat(920.75F, 1822.979F);
        this.xrLabelMaskanResponsible.Name = "xrLabelMaskanResponsible";
        this.xrLabelMaskanResponsible.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelMaskanResponsible.SizeF = new System.Drawing.SizeF(492F, 58F);
        this.xrLabelMaskanResponsible.StylePriority.UseTextAlignment = false;
        this.xrLabelMaskanResponsible.Text = "رئیس سازمان ";
        this.xrLabelMaskanResponsible.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabelLeraningPermissionNo
        // 
        this.xrLabelLeraningPermissionNo.Dpi = 254F;
        this.xrLabelLeraningPermissionNo.LocationFloat = new DevExpress.Utils.PointFloat(1957.916F, 1341.438F);
        this.xrLabelLeraningPermissionNo.Name = "xrLabelLeraningPermissionNo";
        this.xrLabelLeraningPermissionNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelLeraningPermissionNo.SizeF = new System.Drawing.SizeF(434F, 58F);
        this.xrLabelLeraningPermissionNo.StylePriority.UseTextAlignment = false;
        this.xrLabelLeraningPermissionNo.Text = "مجوز آموزشی";
        this.xrLabelLeraningPermissionNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelFromDate
        // 
        this.xrLabelFromDate.Dpi = 254F;
        this.xrLabelFromDate.LocationFloat = new DevExpress.Utils.PointFloat(2317.75F, 1240.896F);
        this.xrLabelFromDate.Name = "xrLabelFromDate";
        this.xrLabelFromDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelFromDate.SizeF = new System.Drawing.SizeF(323F, 58F);
        this.xrLabelFromDate.StylePriority.UseTextAlignment = false;
        this.xrLabelFromDate.Text = "ازتاریخ";
        this.xrLabelFromDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelProvince
        // 
        this.xrLabelProvince.Dpi = 254F;
        this.xrLabelProvince.LocationFloat = new DevExpress.Utils.PointFloat(2243.666F, 1087.438F);
        this.xrLabelProvince.Name = "xrLabelProvince";
        this.xrLabelProvince.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelProvince.SizeF = new System.Drawing.SizeF(381F, 58F);
        this.xrLabelProvince.StylePriority.UseTextAlignment = false;
        this.xrLabelProvince.Text = "نام استان";
        this.xrLabelProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelToDate
        // 
        this.xrLabelToDate.Dpi = 254F;
        this.xrLabelToDate.LocationFloat = new DevExpress.Utils.PointFloat(1809.75F, 1240.896F);
        this.xrLabelToDate.Name = "xrLabelToDate";
        this.xrLabelToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelToDate.SizeF = new System.Drawing.SizeF(323F, 58F);
        this.xrLabelToDate.StylePriority.UseTextAlignment = false;
        this.xrLabelToDate.Text = "تا تاریخ";
        this.xrLabelToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelLearningPermissionDate
        // 
        this.xrLabelLearningPermissionDate.Dpi = 254F;
        this.xrLabelLearningPermissionDate.LocationFloat = new DevExpress.Utils.PointFloat(1524F, 1341.438F);
        this.xrLabelLearningPermissionDate.Name = "xrLabelLearningPermissionDate";
        this.xrLabelLearningPermissionDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelLearningPermissionDate.SizeF = new System.Drawing.SizeF(265F, 58F);
        this.xrLabelLearningPermissionDate.StylePriority.UseTextAlignment = false;
        this.xrLabelLearningPermissionDate.Text = "تاریخ مجوز";
        this.xrLabelLearningPermissionDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDuration
        // 
        this.xrLabelDuration.Dpi = 254F;
        this.xrLabelDuration.LocationFloat = new DevExpress.Utils.PointFloat(1576.916F, 1240.896F);
        this.xrLabelDuration.Name = "xrLabelDuration";
        this.xrLabelDuration.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelDuration.SizeF = new System.Drawing.SizeF(175F, 58F);
        this.xrLabelDuration.StylePriority.UseTextAlignment = false;
        this.xrLabelDuration.Text = "مدت";
        this.xrLabelDuration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTableTeachers
        // 
        this.xrTableTeachers.Dpi = 254F;
        this.xrTableTeachers.LocationFloat = new DevExpress.Utils.PointFloat(767.2917F, 1410.229F);
        this.xrTableTeachers.Name = "xrTableTeachers";
        this.xrTableTeachers.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3});
        this.xrTableTeachers.SizeF = new System.Drawing.SizeF(445F, 174F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 3;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 3;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 3;
        // 
        // xrTableTeachersFile
        // 
        this.xrTableTeachersFile.Dpi = 254F;
        this.xrTableTeachersFile.LocationFloat = new DevExpress.Utils.PointFloat(2006.5F, 1484.104F);
        this.xrTableTeachersFile.Name = "xrTableTeachersFile";
        this.xrTableTeachersFile.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTableTeachersFile.SizeF = new System.Drawing.SizeF(444F, 138F);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell1";
        this.xrTableCell4.Weight = 3;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell2";
        this.xrTableCell5.Weight = 3;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "xrTableCell3";
        this.xrTableCell6.Weight = 3;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(2643.187F, 885.9341F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLabel1.Text = ":خانم/آقای";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1947.833F, 885.9341F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(100.5416F, 58.41998F);
        this.xrLabel2.Text = ":فرزند";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1233.208F, 885.9341F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(179.5417F, 58.41998F);
        this.xrLabel3.Text = ":با کد ملی";
        // 
        // XtraReportCourseDocument
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Tahoma", 9.75F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 5.291667F;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachers)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachersFile)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    //private void InitializeComponent()
    //{
    //    components = new System.ComponentModel.Container();
    //    this.Detail = new DevExpress.XtraReports.UI.DetailBand();
    //    this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
    //    this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
    //    ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
    //    this.BottomMargin.Height = 100;
    //    this.TopMargin.Height = 100;
    //    this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
    //    this.Detail,
    //    this.TopMargin,
    //    this.BottomMargin});
    //    ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    //    this.Detail = new DevExpress.XtraReports.UI.DetailBand();
    //    this.xrTableTeachersFile = new DevExpress.XtraReports.UI.XRTable();
    //    this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrTableTeachers = new DevExpress.XtraReports.UI.XRTable();
    //    this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
    //    this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
    //    this.xrLabelMaskanResponsible = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelLearningResponsible = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelCourseCode = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelLicenceNo = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelDate = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelPersonName = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelFather = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelIdNo = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelDocNo = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelMajor = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelGrade = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelProvince = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelMemberNo = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelCourseName = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelFromDate = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelToDate = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelDuration = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelOfficeName = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelLeraningPermissionNo = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelLearningPermissionDate = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelLearningProvince = new DevExpress.XtraReports.UI.XRLabel();
    //    this.xrLabelGraduateDate = new DevExpress.XtraReports.UI.XRLabel();
    //    ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachersFile)).BeginInit();
    //    ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachers)).BeginInit();
    //    ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
    //    // 
    //    // Detail
    //    // 
    //    this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
    //        this.xrTableTeachersFile,
    //        this.xrTableTeachers,
    //        this.xrLabelMaskanResponsible,
    //        this.xrLabelLearningResponsible,
    //        this.xrLabelCourseCode,
    //        this.xrLabelLicenceNo,
    //        this.xrLabelDate,
    //        this.xrLabelPersonName,
    //        this.xrLabelFather,
    //        this.xrLabelIdNo,
    //        this.xrLabelDocNo,
    //        this.xrLabelMajor,
    //        this.xrLabelGrade,
    //        this.xrLabelProvince,
    //        this.xrLabelMemberNo,
    //        this.xrLabelCourseName,
    //        this.xrLabelFromDate,
    //        this.xrLabelToDate,
    //        this.xrLabelDuration,
    //        this.xrLabelOfficeName,
    //        this.xrLabelLeraningPermissionNo,
    //        this.xrLabelLearningPermissionDate,
    //        this.xrLabelLearningProvince,
    //        this.xrLabelGraduateDate});
    //    this.Detail.Dpi = 254F;
    //    this.Detail.Height = 2970;
    //    this.Detail.Name = "Detail";
    //    this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
    //    this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
    //    // 
    //    // xrTableTeachersFile
    //    // 
    //    this.xrTableTeachersFile.Dpi = 254F;
    //    this.xrTableTeachersFile.Location = new System.Drawing.Point(1984, 1492);
    //    this.xrTableTeachersFile.Name = "xrTableTeachersFile";
    //    this.xrTableTeachersFile.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
    //        this.xrTableRow4,
    //        this.xrTableRow5,
    //        this.xrTableRow6});
    //    this.xrTableTeachersFile.Size = new System.Drawing.Size(444, 138);
    //    // 
    //    // xrTableRow4
    //    // 
    //    this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell4});
    //    this.xrTableRow4.Dpi = 254F;
    //    this.xrTableRow4.Name = "xrTableRow4";
    //    this.xrTableRow4.Weight = 1;
    //    // 
    //    // xrTableCell4
    //    // 
    //    this.xrTableCell4.Dpi = 254F;
    //    this.xrTableCell4.Name = "xrTableCell4";
    //    this.xrTableCell4.Text = "xrTableCell1";
    //    this.xrTableCell4.Weight = 3;
    //    // 
    //    // xrTableRow5
    //    // 
    //    this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell5});
    //    this.xrTableRow5.Dpi = 254F;
    //    this.xrTableRow5.Name = "xrTableRow5";
    //    this.xrTableRow5.Weight = 1;
    //    // 
    //    // xrTableCell5
    //    // 
    //    this.xrTableCell5.Dpi = 254F;
    //    this.xrTableCell5.Name = "xrTableCell5";
    //    this.xrTableCell5.Text = "xrTableCell2";
    //    this.xrTableCell5.Weight = 3;
    //    // 
    //    // xrTableRow6
    //    // 
    //    this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell6});
    //    this.xrTableRow6.Dpi = 254F;
    //    this.xrTableRow6.Name = "xrTableRow6";
    //    this.xrTableRow6.Weight = 1;
    //    // 
    //    // xrTableCell6
    //    // 
    //    this.xrTableCell6.Dpi = 254F;
    //    this.xrTableCell6.Name = "xrTableCell6";
    //    this.xrTableCell6.Text = "xrTableCell3";
    //    this.xrTableCell6.Weight = 3;
    //    // 
    //    // xrTableTeachers
    //    // 
    //    this.xrTableTeachers.Dpi = 254F;
    //    this.xrTableTeachers.Location = new System.Drawing.Point(587, 1413);
    //    this.xrTableTeachers.Name = "xrTableTeachers";
    //    this.xrTableTeachers.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
    //        this.xrTableRow1,
    //        this.xrTableRow2,
    //        this.xrTableRow3});
    //    this.xrTableTeachers.Size = new System.Drawing.Size(445, 174);
    //    // 
    //    // xrTableRow1
    //    // 
    //    this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell1});
    //    this.xrTableRow1.Dpi = 254F;
    //    this.xrTableRow1.Name = "xrTableRow1";
    //    this.xrTableRow1.Weight = 1;
    //    // 
    //    // xrTableCell1
    //    // 
    //    this.xrTableCell1.Dpi = 254F;
    //    this.xrTableCell1.Name = "xrTableCell1";
    //    this.xrTableCell1.Text = "xrTableCell1";
    //    this.xrTableCell1.Weight = 3;
    //    // 
    //    // xrTableRow2
    //    // 
    //    this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell2});
    //    this.xrTableRow2.Dpi = 254F;
    //    this.xrTableRow2.Name = "xrTableRow2";
    //    this.xrTableRow2.Weight = 1;
    //    // 
    //    // xrTableCell2
    //    // 
    //    this.xrTableCell2.Dpi = 254F;
    //    this.xrTableCell2.Name = "xrTableCell2";
    //    this.xrTableCell2.Text = "xrTableCell2";
    //    this.xrTableCell2.Weight = 3;
    //    // 
    //    // xrTableRow3
    //    // 
    //    this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
    //        this.xrTableCell3});
    //    this.xrTableRow3.Dpi = 254F;
    //    this.xrTableRow3.Name = "xrTableRow3";
    //    this.xrTableRow3.Weight = 1;
    //    // 
    //    // xrTableCell3
    //    // 
    //    this.xrTableCell3.Dpi = 254F;
    //    this.xrTableCell3.Name = "xrTableCell3";
    //    this.xrTableCell3.Text = "xrTableCell3";
    //    this.xrTableCell3.Weight = 3;
    //    // 
    //    // xrLabelMaskanResponsible
    //    // 
    //    this.xrLabelMaskanResponsible.Dpi = 254F;
    //    this.xrLabelMaskanResponsible.Location = new System.Drawing.Point(826, 1815);
    //    this.xrLabelMaskanResponsible.Name = "xrLabelMaskanResponsible";
    //    this.xrLabelMaskanResponsible.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelMaskanResponsible.Size = new System.Drawing.Size(492, 58);
    //    this.xrLabelMaskanResponsible.StylePriority.UseTextAlignment = false;
    //    this.xrLabelMaskanResponsible.Text = "رئیس سازمان ";
    //    this.xrLabelMaskanResponsible.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
    //    // 
    //    // xrLabelLearningResponsible
    //    // 
    //    this.xrLabelLearningResponsible.Dpi = 254F;
    //    this.xrLabelLearningResponsible.Location = new System.Drawing.Point(1873, 1820);
    //    this.xrLabelLearningResponsible.Name = "xrLabelLearningResponsible";
    //    this.xrLabelLearningResponsible.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelLearningResponsible.Size = new System.Drawing.Size(482, 58);
    //    this.xrLabelLearningResponsible.StylePriority.UseTextAlignment = false;
    //    this.xrLabelLearningResponsible.Text = "رئیس دوره آموزش";
    //    this.xrLabelLearningResponsible.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
    //    // 
    //    // xrLabelCourseCode
    //    // 
    //    this.xrLabelCourseCode.Dpi = 254F;
    //    this.xrLabelCourseCode.Location = new System.Drawing.Point(1900, 1630);
    //    this.xrLabelCourseCode.Name = "xrLabelCourseCode";
    //    this.xrLabelCourseCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelCourseCode.Size = new System.Drawing.Size(439, 58);
    //    this.xrLabelCourseCode.StylePriority.UseTextAlignment = false;
    //    this.xrLabelCourseCode.Text = "کد دوره";
    //    this.xrLabelCourseCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelLicenceNo
    //    // 
    //    this.xrLabelLicenceNo.Dpi = 254F;
    //    this.xrLabelLicenceNo.Location = new System.Drawing.Point(307, 418);
    //    this.xrLabelLicenceNo.Name = "xrLabelLicenceNo";
    //    this.xrLabelLicenceNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelLicenceNo.Size = new System.Drawing.Size(254, 58);
    //    this.xrLabelLicenceNo.StylePriority.UseTextAlignment = false;
    //    this.xrLabelLicenceNo.Text = "شماره";
    //    this.xrLabelLicenceNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelDate
    //    // 
    //    this.xrLabelDate.Dpi = 254F;
    //    this.xrLabelDate.Location = new System.Drawing.Point(307, 497);
    //    this.xrLabelDate.Name = "xrLabelDate";
    //    this.xrLabelDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelDate.Size = new System.Drawing.Size(254, 58);
    //    this.xrLabelDate.StylePriority.UseTextAlignment = false;
    //    this.xrLabelDate.Text = "تاریخ";
    //    this.xrLabelDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelPersonName
    //    // 
    //    this.xrLabelPersonName.Dpi = 254F;
    //    this.xrLabelPersonName.Location = new System.Drawing.Point(1979, 878);
    //    this.xrLabelPersonName.Name = "xrLabelPersonName";
    //    this.xrLabelPersonName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelPersonName.Size = new System.Drawing.Size(444, 58);
    //    this.xrLabelPersonName.StylePriority.UseTextAlignment = false;
    //    this.xrLabelPersonName.Text = "نام شخص";
    //    this.xrLabelPersonName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelFather
    //    // 
    //    this.xrLabelFather.Dpi = 254F;
    //    this.xrLabelFather.Location = new System.Drawing.Point(1535, 878);
    //    this.xrLabelFather.Name = "xrLabelFather";
    //    this.xrLabelFather.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelFather.Size = new System.Drawing.Size(318, 58);
    //    this.xrLabelFather.StylePriority.UseTextAlignment = false;
    //    this.xrLabelFather.Text = "نام پدر";
    //    this.xrLabelFather.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelIdNo
    //    // 
    //    this.xrLabelIdNo.Dpi = 254F;
    //    this.xrLabelIdNo.Location = new System.Drawing.Point(852, 878);
    //    this.xrLabelIdNo.Name = "xrLabelIdNo";
    //    this.xrLabelIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelIdNo.Size = new System.Drawing.Size(286, 58);
    //    this.xrLabelIdNo.StylePriority.UseTextAlignment = false;
    //    this.xrLabelIdNo.Text = "شماره شناسنامه";
    //    this.xrLabelIdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelDocNo
    //    // 
    //    this.xrLabelDocNo.Dpi = 254F;
    //    this.xrLabelDocNo.Location = new System.Drawing.Point(2085, 974);
    //    this.xrLabelDocNo.Name = "xrLabelDocNo";
    //    this.xrLabelDocNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelDocNo.Size = new System.Drawing.Size(444, 58);
    //    this.xrLabelDocNo.StylePriority.UseTextAlignment = false;
    //    this.xrLabelDocNo.Text = "شماره پروانه";
    //    this.xrLabelDocNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelMajor
    //    // 
    //    this.xrLabelMajor.Dpi = 254F;
    //    this.xrLabelMajor.Location = new System.Drawing.Point(1392, 974);
    //    this.xrLabelMajor.Name = "xrLabelMajor";
    //    this.xrLabelMajor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelMajor.Size = new System.Drawing.Size(444, 58);
    //    this.xrLabelMajor.StylePriority.UseTextAlignment = false;
    //    this.xrLabelMajor.Text = "رشته";
    //    this.xrLabelMajor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelGrade
    //    // 
    //    this.xrLabelGrade.Dpi = 254F;
    //    this.xrLabelGrade.Location = new System.Drawing.Point(836, 974);
    //    this.xrLabelGrade.Name = "xrLabelGrade";
    //    this.xrLabelGrade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelGrade.Size = new System.Drawing.Size(444, 58);
    //    this.xrLabelGrade.StylePriority.UseTextAlignment = false;
    //    this.xrLabelGrade.Text = "پایه";
    //    this.xrLabelGrade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelProvince
    //    // 
    //    this.xrLabelProvince.Dpi = 254F;
    //    this.xrLabelProvince.Location = new System.Drawing.Point(2148, 1080);
    //    this.xrLabelProvince.Name = "xrLabelProvince";
    //    this.xrLabelProvince.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelProvince.Size = new System.Drawing.Size(381, 58);
    //    this.xrLabelProvince.StylePriority.UseTextAlignment = false;
    //    this.xrLabelProvince.Text = "نام استان";
    //    this.xrLabelProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelMemberNo
    //    // 
    //    this.xrLabelMemberNo.Dpi = 254F;
    //    this.xrLabelMemberNo.Location = new System.Drawing.Point(1519, 1080);
    //    this.xrLabelMemberNo.Name = "xrLabelMemberNo";
    //    this.xrLabelMemberNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelMemberNo.Size = new System.Drawing.Size(323, 58);
    //    this.xrLabelMemberNo.StylePriority.UseTextAlignment = false;
    //    this.xrLabelMemberNo.Text = "شماره عضویت";
    //    this.xrLabelMemberNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelCourseName
    //    // 
    //    this.xrLabelCourseName.Dpi = 254F;
    //    this.xrLabelCourseName.Location = new System.Drawing.Point(201, 1080);
    //    this.xrLabelCourseName.Name = "xrLabelCourseName";
    //    this.xrLabelCourseName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelCourseName.Size = new System.Drawing.Size(942, 58);
    //    this.xrLabelCourseName.StylePriority.UseTextAlignment = false;
    //    this.xrLabelCourseName.Text = "نام دوره";
    //    this.xrLabelCourseName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelFromDate
    //    // 
    //    this.xrLabelFromDate.Dpi = 254F;
    //    this.xrLabelFromDate.Location = new System.Drawing.Point(2222, 1233);
    //    this.xrLabelFromDate.Name = "xrLabelFromDate";
    //    this.xrLabelFromDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelFromDate.Size = new System.Drawing.Size(323, 58);
    //    this.xrLabelFromDate.StylePriority.UseTextAlignment = false;
    //    this.xrLabelFromDate.Text = "ازتاریخ";
    //    this.xrLabelFromDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelToDate
    //    // 
    //    this.xrLabelToDate.Dpi = 254F;
    //    this.xrLabelToDate.Location = new System.Drawing.Point(1714, 1233);
    //    this.xrLabelToDate.Name = "xrLabelToDate";
    //    this.xrLabelToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelToDate.Size = new System.Drawing.Size(323, 58);
    //    this.xrLabelToDate.StylePriority.UseTextAlignment = false;
    //    this.xrLabelToDate.Text = "تا تاریخ";
    //    this.xrLabelToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelDuration
    //    // 
    //    this.xrLabelDuration.Dpi = 254F;
    //    this.xrLabelDuration.Location = new System.Drawing.Point(1482, 1233);
    //    this.xrLabelDuration.Name = "xrLabelDuration";
    //    this.xrLabelDuration.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelDuration.Size = new System.Drawing.Size(175, 58);
    //    this.xrLabelDuration.StylePriority.UseTextAlignment = false;
    //    this.xrLabelDuration.Text = "مدت";
    //    this.xrLabelDuration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelOfficeName
    //    // 
    //    this.xrLabelOfficeName.Dpi = 254F;
    //    this.xrLabelOfficeName.Location = new System.Drawing.Point(201, 1233);
    //    this.xrLabelOfficeName.Name = "xrLabelOfficeName";
    //    this.xrLabelOfficeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelOfficeName.Size = new System.Drawing.Size(799, 58);
    //    this.xrLabelOfficeName.StylePriority.UseTextAlignment = false;
    //    this.xrLabelOfficeName.Text = "نام موسسه";
    //    this.xrLabelOfficeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelLeraningPermissionNo
    //    // 
    //    this.xrLabelLeraningPermissionNo.Dpi = 254F;
    //    this.xrLabelLeraningPermissionNo.Location = new System.Drawing.Point(1863, 1334);
    //    this.xrLabelLeraningPermissionNo.Name = "xrLabelLeraningPermissionNo";
    //    this.xrLabelLeraningPermissionNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelLeraningPermissionNo.Size = new System.Drawing.Size(434, 58);
    //    this.xrLabelLeraningPermissionNo.StylePriority.UseTextAlignment = false;
    //    this.xrLabelLeraningPermissionNo.Text = "مجوز آموزشی";
    //    this.xrLabelLeraningPermissionNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelLearningPermissionDate
    //    // 
    //    this.xrLabelLearningPermissionDate.Dpi = 254F;
    //    this.xrLabelLearningPermissionDate.Location = new System.Drawing.Point(1429, 1334);
    //    this.xrLabelLearningPermissionDate.Name = "xrLabelLearningPermissionDate";
    //    this.xrLabelLearningPermissionDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelLearningPermissionDate.Size = new System.Drawing.Size(265, 58);
    //    this.xrLabelLearningPermissionDate.StylePriority.UseTextAlignment = false;
    //    this.xrLabelLearningPermissionDate.Text = "تاریخ مجوز";
    //    this.xrLabelLearningPermissionDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelLearningProvince
    //    // 
    //    this.xrLabelLearningProvince.Dpi = 254F;
    //    this.xrLabelLearningProvince.Location = new System.Drawing.Point(773, 1334);
    //    this.xrLabelLearningProvince.Name = "xrLabelLearningProvince";
    //    this.xrLabelLearningProvince.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelLearningProvince.Size = new System.Drawing.Size(243, 58);
    //    this.xrLabelLearningProvince.StylePriority.UseTextAlignment = false;
    //    this.xrLabelLearningProvince.Text = "استان";
    //    this.xrLabelLearningProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // xrLabelGraduateDate
    //    // 
    //    this.xrLabelGraduateDate.Dpi = 254F;
    //    this.xrLabelGraduateDate.Location = new System.Drawing.Point(2117, 1418);
    //    this.xrLabelGraduateDate.Name = "xrLabelGraduateDate";
    //    this.xrLabelGraduateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
    //    this.xrLabelGraduateDate.Size = new System.Drawing.Size(228, 58);
    //    this.xrLabelGraduateDate.StylePriority.UseTextAlignment = false;
    //    this.xrLabelGraduateDate.Text = "تاریخ دوره";
    //    this.xrLabelGraduateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
    //    // 
    //    // XtraReportCourse
    //    // 
    //    this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
    //        this.Detail});
    //    this.Dpi = 254F;
    //    this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
    //    this.GridSize = new System.Drawing.Size(2, 2);
    //    this.Landscape = true;
    //    this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
    //    this.Name = "XtraReportCourse";
    //    this.PageHeight = 2100;
    //    this.PageWidth = 2970;
    //    this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
    //    this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
    //    this.Version = "9.1";
    //    ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachersFile)).EndInit();
    //    ((System.ComponentModel.ISupportInitialize)(this.xrTableTeachers)).EndInit();
    //    ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    //}
    #endregion
}
