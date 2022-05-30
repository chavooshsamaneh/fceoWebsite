using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for XtraReportMembers
/// </summary>
public class XtraReportMembers : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private XRControlStyle xrControlStyle1;

    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
    TSP.DataManager.MemberRequestManager MRManager = new TSP.DataManager.MemberRequestManager();
    TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();

    private XRSubreport xrSubrJob;
    private XRSubreport xrSubrActivity;
    private XRSubreport xrSubrLanguage;
    private XRSubreport xrSubrResearch;
    private XRLabel xrLabel39;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel38;
    private PageHeaderBand PageHeader;
    private XRTable xrTable2;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell79;
    private XRTable xrTable3;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell64;
    private XRLabel xrLabel37;
    private XRTableCell xrTableCell63;
    private XRPictureBox xrPictureBox2;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell67;
    private XRLabel xrLabel36;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRPanel xrPanel2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTNameEn;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTName;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTFamilyEn;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTFamily;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTSSN;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTIdNo;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTBirthDate;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTFatherName;
    private XRTableCell xrTableCell16;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTIssuePlace;
    private XRTableCell xrTName2;
    private XRTableCell xrTBirthPlace;
    private XRTableCell xrTableCell7;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTHomePo;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTHomeTel;
    private XRTableCell xrTableCell32;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTHomeAddr;
    private XRTableCell xrTableCell28;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTWorkPo;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTWorkTel;
    private XRTableCell xrTableCell36;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTWorkAddr;
    private XRTableCell xrTableCell20;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTFaxNo;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTMobileNo;
    private XRTableCell xrTableCell40;
    private XRTableRow xrTableRow29;
    private XRTableCell xrTMarId;
    private XRTableCell xrTableCell788;
    private XRTableCell xrTSexId;
    private XRTableCell xrTableCell83;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTNationality;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTSolId;
    private XRTableCell xrTableCell87;
    private XRTableRow xrTableRow31;
    private XRTableCell xrTAgentName;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTCitName;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTWebSite;
    private XRTableCell xrTableCell19;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTEmail;
    private XRTableCell xrTableCell44;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTDesc;
    private XRTableCell xrTableCell13;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTFileDate;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTFileNo;
    private XRTableCell xrTableCell48;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTCommission;
    private XRTableCell xrTableCell54;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTAtType;
    private XRTableCell xrTableCell58;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell15;
    private XRTableRow TransferRow1;
    private XRTableCell xrTPrName;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTTransferDate;
    private XRTableRow TransferRow2;
    private XRTableCell xrTTFileNo;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTMeNo;
    private XRTableCell xrTableCell29;
    private XRPictureBox xrPBSign;
    private XRPictureBox xrPBImage;
    private XRSubreport xrSubrMadrak;
    private WinControlContainer winControlContainer1;
    private DevExpress.XtraEditors.CheckEdit checkEdit1;
    private XRTableRow xrTableRowMeId;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTMeId;
    private XRTableCell xrTlblMeId;
    private XRPageBreak xrPageBreak1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRTableRow xrTableRowUser;
    private XRTableCell xrTPassword;
    private XRTableCell xrTlblPassword;
    private XRTableCell xrTUsername;
    private XRTableCell xrTlblUserName;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRLabel txtCurrentDate;
    private XRTableRow xrTableRowFalooCode;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTFalooCode;
    private XRTableCell xrTableCell33;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportMembers(int MeId, int MReId, string PageMode, string Password, int UserId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        MeManager.FindByCode(MeId);
        MRManager.FindByCode(MReId);
        bool IsMeTemp = Convert.ToBoolean(MRManager[0]["IsMeTemp"]);
        txtCurrentDate.Text = Utility.GetDateOfToday();
        if (PageMode == "Wizard")
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(UserId);
            xrTableRowMeId.Visible = false;
            xrTableRowUser.Visible = true;
            xrTableRowFalooCode.Visible = true;
            xrTPassword.Text = Password;
            xrTUsername.Text = LoginManager[0]["UserName"].ToString();
        }
        else if (PageMode == "Manager")
        {
            xrTableRowMeId.Visible = true;
            xrTableRowUser.Visible = false;
            xrTableRowFalooCode.Visible = false;
            if (!IsMeTemp)
                xrTMeId.Text = MeId.ToString();
            else xrTMeId.Text = "M" + MeId.ToString();
        }

        xrTFalooCode.DataBindings.Add("Text", MRManager.DataTable, "FollowCode");
        xrTName.DataBindings.Add("Text", MRManager.DataTable, "FirstName");
        xrTNameEn.DataBindings.Add("Text", MRManager.DataTable, "FirstNameEn");
        xrTFamily.DataBindings.Add("Text", MRManager.DataTable, "LastName");
        xrTFamilyEn.DataBindings.Add("Text", MRManager.DataTable, "LastNameEn");
        xrTIdNo.DataBindings.Add("Text", MRManager.DataTable, "IdNo");
        xrTSSN.DataBindings.Add("Text", MRManager.DataTable, "SSN");
        xrTFatherName.DataBindings.Add("Text", MRManager.DataTable, "FatherName");
        xrTBirthDate.DataBindings.Add("Text", MRManager.DataTable, "BirhtDate");
        xrTBirthPlace.DataBindings.Add("Text", MRManager.DataTable, "BirthPlace");
        xrTIssuePlace.DataBindings.Add("Text", MRManager.DataTable, "IssuePlace");
        xrTHomeTel.DataBindings.Add("Text", MRManager.DataTable, "HomeTel");
        xrTHomePo.DataBindings.Add("Text", MRManager.DataTable, "HomePo");
        xrTHomeAddr.DataBindings.Add("Text", MRManager.DataTable, "HomeAdr");
        xrTWorkTel.DataBindings.Add("Text", MRManager.DataTable, "WorkTel");
        xrTWorkPo.DataBindings.Add("Text", MRManager.DataTable, "WorkPo");
        xrTWorkAddr.DataBindings.Add("Text", MRManager.DataTable, "WorkAdr");
        xrTMobileNo.DataBindings.Add("Text", MRManager.DataTable, "MobieNo");
        xrTFaxNo.DataBindings.Add("Text", MRManager.DataTable, "FaxNo");
        xrTMarId.DataBindings.Add("Text", MRManager.DataTable, "MarName");
        xrTSolId.DataBindings.Add("Text", MRManager.DataTable, "SoName");
        xrTCitName.DataBindings.Add("Text", MRManager.DataTable, "CitName");
        xrTAgentName.DataBindings.Add("Text", MRManager.DataTable, "AgentName");
        xrTWebSite.DataBindings.Add("Text", MRManager.DataTable, "WebSite");
        xrTEmail.DataBindings.Add("Text", MRManager.DataTable, "Email");
        xrPBImage.DataBindings.Add("ImageUrl", MRManager.DataTable, "ImageUrl");
        xrPBSign.DataBindings.Add("ImageUrl", MRManager.DataTable, "SignUrl");
        xrTDesc.DataBindings.Add("Text", MRManager.DataTable, "Description");

        if (!IsMeTemp)
        {
            xrTSexId.DataBindings.Add("Text", MeManager.DataTable, "SexName");
            xrTNationality.DataBindings.Add("Text", MeManager.DataTable, "Nationality");
            xrTFileNo.DataBindings.Add("Text", MeManager.DataTable, "FileNo");
            xrTFileDate.DataBindings.Add("Text", MeManager.DataTable, "FileDate");
            xrTCommission.DataBindings.Add("Text", MeManager.DataTable, "MeCommissions");
            xrTAtType.DataBindings.Add("Text", MeManager.DataTable, "MeActivityTypes");
        }
        else
        {
            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            TempMemberManager.FindByCode(MeId);
            xrTSexId.DataBindings.Add("Text", TempMemberManager.DataTable, "SexName");
            xrTNationality.DataBindings.Add("Text", TempMemberManager.DataTable, "Nationality");
            xrTFileNo.Text = "----------";
            xrTFileDate.Text = "----------";
            xrTCommission.DataBindings.Add("Text", TempMemberManager.DataTable, "MeCommissions");
           xrTAtType.DataBindings.Add("Text", TempMemberManager.DataTable, "MeActivityTypes");
        }

        transferManager.FindByMemberId(MReId, 1);
        if (transferManager.Count > 0)
        {
            checkEdit1.Checked = true;

            xrTTransferDate.DataBindings.Add("Text", transferManager.DataTable, "TransferDate");
            xrTTFileNo.DataBindings.Add("Text", transferManager.DataTable, "FileNo");
            xrTMeNo.DataBindings.Add("Text", transferManager.DataTable, "MeNo");
            xrTPrName.DataBindings.Add("Text", transferManager.DataTable, "PrName");
        }
        else
        {
            TransferRow1.Visible = false;
            TransferRow2.Visible = false;

            xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell11.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell15.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
        }


        XtraReportMemberActivity ReAt = new XtraReportMemberActivity(MeId, MReId, IsMeTemp);
        xrSubrActivity.ReportSource = ReAt;

        XtraReportMemberJob ReJob = new XtraReportMemberJob(MeId, MReId, IsMeTemp);
        xrSubrJob.ReportSource = ReJob;

        XtraReportMemberLanguage ReLan = new XtraReportMemberLanguage(MeId, MReId, IsMeTemp);
        xrSubrLanguage.ReportSource = ReLan;

     ////  // XtraReportMemberMadrak ReMa = new XtraReportMemberMadrak(MeId, MReId, IsMeTemp);
     //////   xrSubrMadrak.ReportSource = ReMa;

        //XtraReportMemberResearch ReRa = new XtraReportMemberResearch(MeId);
        //xrSubrResearch.ReportSource = ReRa;
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
        string resourceFileName = "XtraReportMembers.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrSubrResearch = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubrLanguage = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubrActivity = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubrJob = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRowMeId = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTlblMeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRowUser = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTPassword = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTlblPassword = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTUsername = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTlblUserName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTNameEn = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTFamilyEn = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFamily = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTSSN = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTIdNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTBirthDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFatherName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTIssuePlace = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTName2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTBirthPlace = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTHomePo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTHomeTel = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTHomeAddr = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTWorkPo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTWorkTel = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTWorkAddr = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTFaxNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMobileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMarId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell788 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTSexId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTNationality = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTSolId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAgentName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCitName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPBSign = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPBImage = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTWebSite = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTEmail = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTDesc = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTFileDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCommission = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAtType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.winControlContainer1 = new DevExpress.XtraReports.UI.WinControlContainer();
        this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
        this.TransferRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTransferDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTPrName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TransferRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTFileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrSubrMadrak = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.txtCurrentDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRowFalooCode = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFalooCode = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubrResearch,
            this.xrSubrLanguage,
            this.xrSubrActivity,
            this.xrSubrJob,
            this.xrPanel2,
            this.xrSubrMadrak,
            this.xrPageBreak1});
        this.Detail.Dpi = 254F;
        this.Detail.Font = new System.Drawing.Font("Tahoma", 8F);
        this.Detail.HeightF = 2643F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.StylePriority.UseBorders = false;
        this.Detail.StylePriority.UseFont = false;
        this.Detail.StylePriority.UseTextAlignment = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrSubrResearch
        // 
        this.xrSubrResearch.Dpi = 254F;
        this.xrSubrResearch.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2519F);
        this.xrSubrResearch.Name = "xrSubrResearch";
        this.xrSubrResearch.SizeF = new System.Drawing.SizeF(1849F, 64F);
        // 
        // xrSubrLanguage
        // 
        this.xrSubrLanguage.Dpi = 254F;
        this.xrSubrLanguage.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2392F);
        this.xrSubrLanguage.Name = "xrSubrLanguage";
        this.xrSubrLanguage.SizeF = new System.Drawing.SizeF(1849F, 64F);
        // 
        // xrSubrActivity
        // 
        this.xrSubrActivity.Dpi = 254F;
        this.xrSubrActivity.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2265F);
        this.xrSubrActivity.Name = "xrSubrActivity";
        this.xrSubrActivity.SizeF = new System.Drawing.SizeF(1849F, 64F);
        // 
        // xrSubrJob
        // 
        this.xrSubrJob.Dpi = 254F;
        this.xrSubrJob.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2138F);
        this.xrSubrJob.Name = "xrSubrJob";
        this.xrSubrJob.SizeF = new System.Drawing.SizeF(1849F, 64F);
        // 
        // xrPanel2
        // 
        this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel2.BorderWidth = 2;
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(1849F, 1884.355F);
        this.xrPanel2.StylePriority.UseBorders = false;
        this.xrPanel2.StylePriority.UseBorderWidth = false;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(9.999995F, 11.77084F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowMeId,
            this.xrTableRowFalooCode,
            this.xrTableRowUser,
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow6,
            this.xrTableRow8,
            this.xrTableRow7,
            this.xrTableRow9,
            this.xrTableRow5,
            this.xrTableRow10,
            this.xrTableRow29,
            this.xrTableRow30,
            this.xrTableRow31,
            this.xrTableRow16,
            this.xrTableRow32,
            this.xrTableRow11,
            this.xrTableRow15,
            this.xrTableRow13,
            this.xrTableRow18,
            this.xrTableRow20,
            this.xrTableRow12,
            this.TransferRow1,
            this.TransferRow2});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1829F, 1859.75F);
        this.xrTable1.StyleName = "xrControlStyle1";
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UsePadding = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRowMeId
        // 
        this.xrTableRowMeId.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell24,
            this.xrTMeId,
            this.xrTlblMeId});
        this.xrTableRowMeId.Dpi = 254F;
        this.xrTableRowMeId.Name = "xrTableRowMeId";
        this.xrTableRowMeId.Weight = 0.41141386823995318D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.Weight = 2.08D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.Weight = 1.42D;
        // 
        // xrTMeId
        // 
        this.xrTMeId.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTMeId.Dpi = 254F;
        this.xrTMeId.Name = "xrTMeId";
        this.xrTMeId.StylePriority.UseBorders = false;
        this.xrTMeId.Text = "xrTMeId";
        this.xrTMeId.Weight = 2D;
        // 
        // xrTlblMeId
        // 
        this.xrTlblMeId.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTlblMeId.Dpi = 254F;
        this.xrTlblMeId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTlblMeId.Name = "xrTlblMeId";
        this.xrTlblMeId.StylePriority.UseBorders = false;
        this.xrTlblMeId.StylePriority.UseFont = false;
        this.xrTlblMeId.Text = ": کد عضویت";
        this.xrTlblMeId.Weight = 1.4010722100656454D;
        // 
        // xrTableRowUser
        // 
        this.xrTableRowUser.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTPassword,
            this.xrTlblPassword,
            this.xrTUsername,
            this.xrTlblUserName});
        this.xrTableRowUser.Dpi = 254F;
        this.xrTableRowUser.Name = "xrTableRowUser";
        this.xrTableRowUser.Weight = 0.41141386823995318D;
        // 
        // xrTPassword
        // 
        this.xrTPassword.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTPassword.Dpi = 254F;
        this.xrTPassword.Name = "xrTPassword";
        this.xrTPassword.StylePriority.UseBorders = false;
        this.xrTPassword.Text = "xrTPassword";
        this.xrTPassword.Weight = 2.08D;
        // 
        // xrTlblPassword
        // 
        this.xrTlblPassword.Dpi = 254F;
        this.xrTlblPassword.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTlblPassword.Name = "xrTlblPassword";
        this.xrTlblPassword.StylePriority.UseFont = false;
        this.xrTlblPassword.Text = ": کلمه عبور";
        this.xrTlblPassword.Weight = 1.42D;
        // 
        // xrTUsername
        // 
        this.xrTUsername.Dpi = 254F;
        this.xrTUsername.Name = "xrTUsername";
        this.xrTUsername.Text = "xrTUsername";
        this.xrTUsername.Weight = 2D;
        // 
        // xrTlblUserName
        // 
        this.xrTlblUserName.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTlblUserName.Dpi = 254F;
        this.xrTlblUserName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTlblUserName.Name = "xrTlblUserName";
        this.xrTlblUserName.StylePriority.UseBorders = false;
        this.xrTlblUserName.StylePriority.UseFont = false;
        this.xrTlblUserName.Text = ": نام کاربری";
        this.xrTlblUserName.Weight = 1.4010722100656454D;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTNameEn,
            this.xrTableCell1,
            this.xrTName,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseTextAlignment = false;
        this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableRow1.Weight = 0.58858613176004659D;
        // 
        // xrTNameEn
        // 
        this.xrTNameEn.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTNameEn.Dpi = 254F;
        this.xrTNameEn.Name = "xrTNameEn";
        this.xrTNameEn.StylePriority.UseBorders = false;
        this.xrTNameEn.StylePriority.UseTextAlignment = false;
        this.xrTNameEn.Text = "xrTNameEn";
        this.xrTNameEn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTNameEn.Weight = 2.08D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "(نام (انگلیسی";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell1.Weight = 1.42D;
        // 
        // xrTName
        // 
        this.xrTName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTName.Dpi = 254F;
        this.xrTName.Name = "xrTName";
        this.xrTName.StylePriority.UseBorders = false;
        this.xrTName.StylePriority.UseTextAlignment = false;
        this.xrTName.Text = "xrTName";
        this.xrTName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTName.Weight = 2D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = ": نام";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 1.4010722100656454D;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTFamilyEn,
            this.xrTableCell6,
            this.xrTFamily,
            this.xrTableCell8});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.49999999999999994D;
        // 
        // xrTFamilyEn
        // 
        this.xrTFamilyEn.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTFamilyEn.Dpi = 254F;
        this.xrTFamilyEn.Name = "xrTFamilyEn";
        this.xrTFamilyEn.StylePriority.UseBorders = false;
        this.xrTFamilyEn.Text = "xrTFamilyEn";
        this.xrTFamilyEn.Weight = 2.08D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBackColor = false;
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Text = "(نام خانوادگی(انگلیسی";
        this.xrTableCell6.Weight = 1.42D;
        // 
        // xrTFamily
        // 
        this.xrTFamily.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTFamily.Dpi = 254F;
        this.xrTFamily.Name = "xrTFamily";
        this.xrTFamily.StylePriority.UseBorders = false;
        this.xrTFamily.Text = "xrTFamily";
        this.xrTFamily.Weight = 2D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.Text = ": نام خانوادگی";
        this.xrTableCell8.Weight = 1.4010722100656454D;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTSSN,
            this.xrTableCell10,
            this.xrTIdNo,
            this.xrTableCell12});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5D;
        // 
        // xrTSSN
        // 
        this.xrTSSN.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTSSN.Dpi = 254F;
        this.xrTSSN.Name = "xrTSSN";
        this.xrTSSN.StylePriority.UseBorders = false;
        this.xrTSSN.Text = "xrTSSN";
        this.xrTSSN.Weight = 2.08D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBackColor = false;
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.Text = "کد ملی";
        this.xrTableCell10.Weight = 1.42D;
        // 
        // xrTIdNo
        // 
        this.xrTIdNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTIdNo.Dpi = 254F;
        this.xrTIdNo.Name = "xrTIdNo";
        this.xrTIdNo.StylePriority.UseBorders = false;
        this.xrTIdNo.Text = "xrTIdNo";
        this.xrTIdNo.Weight = 2D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.Text = ": شماره شناسنامه";
        this.xrTableCell12.Weight = 1.4010722100656454D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTBirthDate,
            this.xrTableCell14,
            this.xrTFatherName,
            this.xrTableCell16});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.49999999999999989D;
        // 
        // xrTBirthDate
        // 
        this.xrTBirthDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTBirthDate.Dpi = 254F;
        this.xrTBirthDate.Name = "xrTBirthDate";
        this.xrTBirthDate.StylePriority.UseBorders = false;
        this.xrTBirthDate.Text = "xrTBirthDate";
        this.xrTBirthDate.Weight = 2.08D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBackColor = false;
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.Text = "تاریخ تولد";
        this.xrTableCell14.Weight = 1.42D;
        // 
        // xrTFatherName
        // 
        this.xrTFatherName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTFatherName.Dpi = 254F;
        this.xrTFatherName.Name = "xrTFatherName";
        this.xrTFatherName.StylePriority.UseBorders = false;
        this.xrTFatherName.Text = "xrTFatherName";
        this.xrTFatherName.Weight = 2D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBackColor = false;
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.Text = ": نام پدر";
        this.xrTableCell16.Weight = 1.4010722100656454D;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTIssuePlace,
            this.xrTName2,
            this.xrTBirthPlace,
            this.xrTableCell7});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.5D;
        // 
        // xrTIssuePlace
        // 
        this.xrTIssuePlace.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTIssuePlace.Dpi = 254F;
        this.xrTIssuePlace.Name = "xrTIssuePlace";
        this.xrTIssuePlace.StylePriority.UseBorders = false;
        this.xrTIssuePlace.Text = "xrTIssuePlace";
        this.xrTIssuePlace.Weight = 2.08D;
        // 
        // xrTName2
        // 
        this.xrTName2.BackColor = System.Drawing.Color.Empty;
        this.xrTName2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTName2.Dpi = 254F;
        this.xrTName2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTName2.Name = "xrTName2";
        this.xrTName2.StylePriority.UseBackColor = false;
        this.xrTName2.StylePriority.UseBorders = false;
        this.xrTName2.StylePriority.UseFont = false;
        this.xrTName2.Text = "محل صدور شناسنامه";
        this.xrTName2.Weight = 1.42D;
        // 
        // xrTBirthPlace
        // 
        this.xrTBirthPlace.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTBirthPlace.Dpi = 254F;
        this.xrTBirthPlace.Name = "xrTBirthPlace";
        this.xrTBirthPlace.StylePriority.UseBorders = false;
        this.xrTBirthPlace.Text = "xrTBirthPlace";
        this.xrTBirthPlace.Weight = 2D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBackColor = false;
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Text = ": محل تولد";
        this.xrTableCell7.Weight = 1.4010722100656454D;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTHomePo,
            this.xrTableCell30,
            this.xrTHomeTel,
            this.xrTableCell32});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.5D;
        // 
        // xrTHomePo
        // 
        this.xrTHomePo.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTHomePo.Dpi = 254F;
        this.xrTHomePo.Name = "xrTHomePo";
        this.xrTHomePo.StylePriority.UseBorders = false;
        this.xrTHomePo.Text = "xrTHomePo";
        this.xrTHomePo.Weight = 2.08D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBackColor = false;
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = "کد پستی محل سکونت";
        this.xrTableCell30.Weight = 1.42D;
        // 
        // xrTHomeTel
        // 
        this.xrTHomeTel.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTHomeTel.Dpi = 254F;
        this.xrTHomeTel.Name = "xrTHomeTel";
        this.xrTHomeTel.StylePriority.UseBorders = false;
        this.xrTHomeTel.Text = "xrTHomeTel";
        this.xrTHomeTel.Weight = 2D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBackColor = false;
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.StylePriority.UseFont = false;
        this.xrTableCell32.Text = ": تلفن محل سکونت";
        this.xrTableCell32.Weight = 1.4010722100656454D;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTHomeAddr,
            this.xrTableCell28});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.5D;
        // 
        // xrTHomeAddr
        // 
        this.xrTHomeAddr.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTHomeAddr.Dpi = 254F;
        this.xrTHomeAddr.Name = "xrTHomeAddr";
        this.xrTHomeAddr.StylePriority.UseBorders = false;
        this.xrTHomeAddr.Text = "xrTHomeAddr";
        this.xrTHomeAddr.Weight = 5.5D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBackColor = false;
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseFont = false;
        this.xrTableCell28.Text = ": آدرس محل سکونت";
        this.xrTableCell28.Weight = 1.4010722100656454D;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTWorkPo,
            this.xrTableCell34,
            this.xrTWorkTel,
            this.xrTableCell36});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.49999999999999978D;
        // 
        // xrTWorkPo
        // 
        this.xrTWorkPo.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTWorkPo.Dpi = 254F;
        this.xrTWorkPo.Name = "xrTWorkPo";
        this.xrTWorkPo.StylePriority.UseBorders = false;
        this.xrTWorkPo.Text = "xrTWorkPo";
        this.xrTWorkPo.Weight = 2.08D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBackColor = false;
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.Text = "کد پستی محل کار";
        this.xrTableCell34.Weight = 1.42D;
        // 
        // xrTWorkTel
        // 
        this.xrTWorkTel.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTWorkTel.Dpi = 254F;
        this.xrTWorkTel.Name = "xrTWorkTel";
        this.xrTWorkTel.StylePriority.UseBorders = false;
        this.xrTWorkTel.Text = "xrTWorkTel";
        this.xrTWorkTel.Weight = 2D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBackColor = false;
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.Text = ": تلفن محل کار";
        this.xrTableCell36.Weight = 1.4010722100656454D;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTWorkAddr,
            this.xrTableCell20});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.49999999999999978D;
        // 
        // xrTWorkAddr
        // 
        this.xrTWorkAddr.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTWorkAddr.Dpi = 254F;
        this.xrTWorkAddr.Name = "xrTWorkAddr";
        this.xrTWorkAddr.StylePriority.UseBorders = false;
        this.xrTWorkAddr.Text = "xrTWorkAddr";
        this.xrTWorkAddr.Weight = 5.5D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = ": آدرس محل کار";
        this.xrTableCell20.Weight = 1.4010722100656454D;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTFaxNo,
            this.xrTableCell38,
            this.xrTMobileNo,
            this.xrTableCell40});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.5D;
        // 
        // xrTFaxNo
        // 
        this.xrTFaxNo.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTFaxNo.Dpi = 254F;
        this.xrTFaxNo.Name = "xrTFaxNo";
        this.xrTFaxNo.StylePriority.UseBorders = false;
        this.xrTFaxNo.Text = "xrTFaxNo";
        this.xrTFaxNo.Weight = 2.08D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBackColor = false;
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.Text = "شماره فکس";
        this.xrTableCell38.Weight = 1.42D;
        // 
        // xrTMobileNo
        // 
        this.xrTMobileNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTMobileNo.Dpi = 254F;
        this.xrTMobileNo.Name = "xrTMobileNo";
        this.xrTMobileNo.StylePriority.UseBorders = false;
        this.xrTMobileNo.Text = "xrTMobileNo";
        this.xrTMobileNo.Weight = 2D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseBackColor = false;
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.Text = ": شماره تلفن همراه";
        this.xrTableCell40.Weight = 1.4010722100656454D;
        // 
        // xrTableRow29
        // 
        this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTMarId,
            this.xrTableCell788,
            this.xrTSexId,
            this.xrTableCell83});
        this.xrTableRow29.Dpi = 254F;
        this.xrTableRow29.Name = "xrTableRow29";
        this.xrTableRow29.Weight = 0.49999999999999978D;
        // 
        // xrTMarId
        // 
        this.xrTMarId.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTMarId.Dpi = 254F;
        this.xrTMarId.Name = "xrTMarId";
        this.xrTMarId.StylePriority.UseBorders = false;
        this.xrTMarId.Text = "xrTMarId";
        this.xrTMarId.Weight = 2.08D;
        // 
        // xrTableCell788
        // 
        this.xrTableCell788.Dpi = 254F;
        this.xrTableCell788.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell788.Name = "xrTableCell788";
        this.xrTableCell788.StylePriority.UseFont = false;
        this.xrTableCell788.Text = "وضعیت تآهل";
        this.xrTableCell788.Weight = 1.42D;
        // 
        // xrTSexId
        // 
        this.xrTSexId.Dpi = 254F;
        this.xrTSexId.Name = "xrTSexId";
        this.xrTSexId.Text = "xrTSexId";
        this.xrTSexId.Weight = 2D;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell83.Dpi = 254F;
        this.xrTableCell83.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.StylePriority.UseBorders = false;
        this.xrTableCell83.StylePriority.UseFont = false;
        this.xrTableCell83.Text = ": جنسیت";
        this.xrTableCell83.Weight = 1.4010722100656454D;
        // 
        // xrTableRow30
        // 
        this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTNationality,
            this.xrTableCell85,
            this.xrTSolId,
            this.xrTableCell87});
        this.xrTableRow30.Dpi = 254F;
        this.xrTableRow30.Name = "xrTableRow30";
        this.xrTableRow30.Weight = 0.49999999999999978D;
        // 
        // xrTNationality
        // 
        this.xrTNationality.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTNationality.Dpi = 254F;
        this.xrTNationality.Name = "xrTNationality";
        this.xrTNationality.StylePriority.UseBorders = false;
        this.xrTNationality.Text = "xrTNationality";
        this.xrTNationality.Weight = 2.08D;
        // 
        // xrTableCell85
        // 
        this.xrTableCell85.Dpi = 254F;
        this.xrTableCell85.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell85.Name = "xrTableCell85";
        this.xrTableCell85.StylePriority.UseFont = false;
        this.xrTableCell85.Text = "ملیت";
        this.xrTableCell85.Weight = 1.42D;
        // 
        // xrTSolId
        // 
        this.xrTSolId.Dpi = 254F;
        this.xrTSolId.Name = "xrTSolId";
        this.xrTSolId.Text = "xrTSolId";
        this.xrTSolId.Weight = 2D;
        // 
        // xrTableCell87
        // 
        this.xrTableCell87.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell87.Dpi = 254F;
        this.xrTableCell87.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell87.Name = "xrTableCell87";
        this.xrTableCell87.StylePriority.UseBorders = false;
        this.xrTableCell87.StylePriority.UseFont = false;
        this.xrTableCell87.Text = ": وضعیت سربازی";
        this.xrTableCell87.Weight = 1.4010722100656454D;
        // 
        // xrTableRow31
        // 
        this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAgentName,
            this.xrTableCell4,
            this.xrTCitName,
            this.xrTableCell9});
        this.xrTableRow31.Dpi = 254F;
        this.xrTableRow31.Name = "xrTableRow31";
        this.xrTableRow31.Weight = 0.50001925878786835D;
        // 
        // xrTAgentName
        // 
        this.xrTAgentName.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTAgentName.Dpi = 254F;
        this.xrTAgentName.Name = "xrTAgentName";
        this.xrTAgentName.StylePriority.UseBorders = false;
        this.xrTAgentName.Text = "xrTAgentName";
        this.xrTAgentName.Weight = 2.08D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.Text = "نمایندگی";
        this.xrTableCell4.Weight = 1.42D;
        // 
        // xrTCitName
        // 
        this.xrTCitName.Dpi = 254F;
        this.xrTCitName.Name = "xrTCitName";
        this.xrTCitName.Text = "xrTCitName";
        this.xrTCitName.Weight = 2D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.Text = ": محل اقامت";
        this.xrTableCell9.Weight = 1.4010722100656454D;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell17,
            this.xrTableCell18});
        this.xrTableRow16.Dpi = 254F;
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 0.49996148242426253D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPBSign});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 2.08D;
        // 
        // xrPBSign
        // 
        this.xrPBSign.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPBSign.Dpi = 254F;
        this.xrPBSign.LocationFloat = new DevExpress.Utils.PointFloat(286F, 13F);
        this.xrPBSign.Name = "xrPBSign";
        this.xrPBSign.SizeF = new System.Drawing.SizeF(250F, 250F);
        this.xrPBSign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPBSign.StylePriority.UseBorders = false;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "تصویر امضا";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell5.Weight = 1.42D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPBImage});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "xrTableCell17";
        this.xrTableCell17.Weight = 2D;
        // 
        // xrPBImage
        // 
        this.xrPBImage.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPBImage.Dpi = 254F;
        this.xrPBImage.LocationFloat = new DevExpress.Utils.PointFloat(265F, 16F);
        this.xrPBImage.Name = "xrPBImage";
        this.xrPBImage.SizeF = new System.Drawing.SizeF(250F, 250F);
        this.xrPBImage.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPBImage.StylePriority.UseBorders = false;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.Text = ": تصویر";
        this.xrTableCell18.Weight = 1.4010722100656454D;
        // 
        // xrTableRow32
        // 
        this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTWebSite,
            this.xrTableCell19});
        this.xrTableRow32.Dpi = 254F;
        this.xrTableRow32.Name = "xrTableRow32";
        this.xrTableRow32.Weight = 0.500465309682312D;
        // 
        // xrTWebSite
        // 
        this.xrTWebSite.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTWebSite.Dpi = 254F;
        this.xrTWebSite.Name = "xrTWebSite";
        this.xrTWebSite.StylePriority.UseBorders = false;
        this.xrTWebSite.Text = "xrTWebSite";
        this.xrTWebSite.Weight = 5.5D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.Text = ": آدرس وب سایت";
        this.xrTableCell19.Weight = 1.4010722100656454D;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTEmail,
            this.xrTableCell44});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 0.49955394910555623D;
        // 
        // xrTEmail
        // 
        this.xrTEmail.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTEmail.Dpi = 254F;
        this.xrTEmail.Name = "xrTEmail";
        this.xrTEmail.StylePriority.UseBorders = false;
        this.xrTEmail.Text = "xrTEmail";
        this.xrTEmail.Weight = 5.5D;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell44.Dpi = 254F;
        this.xrTableCell44.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseBackColor = false;
        this.xrTableCell44.StylePriority.UseBorders = false;
        this.xrTableCell44.StylePriority.UseFont = false;
        this.xrTableCell44.Text = ": آدرس پست الکترونیکی";
        this.xrTableCell44.Weight = 1.4010722100656454D;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDesc,
            this.xrTableCell13});
        this.xrTableRow15.Dpi = 254F;
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 0.49999999999999994D;
        // 
        // xrTDesc
        // 
        this.xrTDesc.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTDesc.Dpi = 254F;
        this.xrTDesc.Name = "xrTDesc";
        this.xrTDesc.StylePriority.UseBorders = false;
        this.xrTDesc.Text = "xrTDesc";
        this.xrTDesc.Weight = 5.5D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = ": توضیحات";
        this.xrTableCell13.Weight = 1.4010722100656454D;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTFileDate,
            this.xrTableCell46,
            this.xrTFileNo,
            this.xrTableCell48});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 0.50000000000000022D;
        // 
        // xrTFileDate
        // 
        this.xrTFileDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTFileDate.Dpi = 254F;
        this.xrTFileDate.Name = "xrTFileDate";
        this.xrTFileDate.StylePriority.UseBorders = false;
        this.xrTFileDate.Text = "xrTFileDate";
        this.xrTFileDate.Weight = 2.08D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell46.Dpi = 254F;
        this.xrTableCell46.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseBackColor = false;
        this.xrTableCell46.StylePriority.UseBorders = false;
        this.xrTableCell46.StylePriority.UseFont = false;
        this.xrTableCell46.Text = "تاریخ اعتبار پروانه ";
        this.xrTableCell46.Weight = 1.42D;
        // 
        // xrTFileNo
        // 
        this.xrTFileNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTFileNo.Dpi = 254F;
        this.xrTFileNo.Name = "xrTFileNo";
        this.xrTFileNo.StylePriority.UseBorders = false;
        this.xrTFileNo.Text = "xrTFileNo";
        this.xrTFileNo.Weight = 2D;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell48.Dpi = 254F;
        this.xrTableCell48.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseBackColor = false;
        this.xrTableCell48.StylePriority.UseBorders = false;
        this.xrTableCell48.StylePriority.UseFont = false;
        this.xrTableCell48.Text = ": شماره پروانه اشتغال";
        this.xrTableCell48.Weight = 1.4010722100656454D;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCommission,
            this.xrTableCell54});
        this.xrTableRow18.Dpi = 254F;
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 0.49999999999999967D;
        // 
        // xrTCommission
        // 
        this.xrTCommission.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTCommission.Dpi = 254F;
        this.xrTCommission.Name = "xrTCommission";
        this.xrTCommission.StylePriority.UseBorders = false;
        this.xrTCommission.Text = "xrTCommission";
        this.xrTCommission.Weight = 5.5D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell54.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseBackColor = false;
        this.xrTableCell54.StylePriority.UseBorders = false;
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.Text = ": کمیسیون ها";
        this.xrTableCell54.Weight = 1.4010722100656454D;
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAtType,
            this.xrTableCell58});
        this.xrTableRow20.Dpi = 254F;
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 0.49999999999999967D;
        // 
        // xrTAtType
        // 
        this.xrTAtType.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTAtType.Dpi = 254F;
        this.xrTAtType.Name = "xrTAtType";
        this.xrTAtType.StylePriority.UseBorders = false;
        this.xrTAtType.Text = "xrTAtType";
        this.xrTAtType.Weight = 5.5D;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell58.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell58.Dpi = 254F;
        this.xrTableCell58.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.StylePriority.UseBackColor = false;
        this.xrTableCell58.StylePriority.UseBorders = false;
        this.xrTableCell58.StylePriority.UseFont = false;
        this.xrTableCell58.Text = ": زمینه فعالیت";
        this.xrTableCell58.Weight = 1.4010722100656454D;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell15});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.49999999999999956D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.Text = "از استان دیگری به استان فارس منتقل شده ام";
        this.xrTableCell11.Weight = 6.5904373257020072D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.winControlContainer1});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 0.31063488436363795D;
        // 
        // winControlContainer1
        // 
        this.winControlContainer1.Dpi = 254F;
        this.winControlContainer1.LocationFloat = new DevExpress.Utils.PointFloat(14F, 12F);
        this.winControlContainer1.Name = "winControlContainer1";
        this.winControlContainer1.SizeF = new System.Drawing.SizeF(64F, 50F);
        this.winControlContainer1.WinControl = this.checkEdit1;
        // 
        // checkEdit1
        // 
        this.checkEdit1.Location = new System.Drawing.Point(0, 0);
        this.checkEdit1.Name = "checkEdit1";
        this.checkEdit1.Properties.Caption = "";
        this.checkEdit1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.checkEdit1.ShowToolTips = false;
        this.checkEdit1.Size = new System.Drawing.Size(23, 19);
        this.checkEdit1.TabIndex = 0;
        // 
        // TransferRow1
        // 
        this.TransferRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTransferDate,
            this.xrTableCell23,
            this.xrTPrName,
            this.xrTableCell22});
        this.TransferRow1.Dpi = 254F;
        this.TransferRow1.Name = "TransferRow1";
        this.TransferRow1.Weight = 0.49999999999999967D;
        // 
        // xrTTransferDate
        // 
        this.xrTTransferDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTTransferDate.Dpi = 254F;
        this.xrTTransferDate.Name = "xrTTransferDate";
        this.xrTTransferDate.StylePriority.UseBorders = false;
        this.xrTTransferDate.Text = "xrTTransferDate";
        this.xrTTransferDate.Weight = 2.0825123018042646D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = "تاریخ انتقالی";
        this.xrTableCell23.Weight = 1.4204018589393113D;
        // 
        // xrTPrName
        // 
        this.xrTPrName.Dpi = 254F;
        this.xrTPrName.Name = "xrTPrName";
        this.xrTPrName.Text = "xrTPrName";
        this.xrTPrName.Weight = 1.9970858392564241D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = ": استان قبلی";
        this.xrTableCell22.Weight = 1.4010722100656454D;
        // 
        // TransferRow2
        // 
        this.TransferRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTFileNo,
            this.xrTableCell26,
            this.xrTMeNo,
            this.xrTableCell29});
        this.TransferRow2.Dpi = 254F;
        this.TransferRow2.Name = "TransferRow2";
        this.TransferRow2.Weight = 0.35009870236317558D;
        // 
        // xrTTFileNo
        // 
        this.xrTTFileNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTFileNo.Dpi = 254F;
        this.xrTTFileNo.Name = "xrTTFileNo";
        this.xrTTFileNo.StylePriority.UseBorders = false;
        this.xrTTFileNo.Text = "xrTTFileNo";
        this.xrTTFileNo.Weight = 2.0825123018042646D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.Text = "شماره پروانه";
        this.xrTableCell26.Weight = 1.4204018589393113D;
        // 
        // xrTMeNo
        // 
        this.xrTMeNo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTMeNo.Dpi = 254F;
        this.xrTMeNo.Name = "xrTMeNo";
        this.xrTMeNo.StylePriority.UseBorders = false;
        this.xrTMeNo.Text = "xrTMeNo";
        this.xrTMeNo.Weight = 1.9970858392564241D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.Text = ": کد عضویت";
        this.xrTableCell29.Weight = 1.4010722100656454D;
        // 
        // xrSubrMadrak
        // 
        this.xrSubrMadrak.Dpi = 254F;
        this.xrSubrMadrak.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2011F);
        this.xrSubrMadrak.Name = "xrSubrMadrak";
        this.xrSubrMadrak.SizeF = new System.Drawing.SizeF(1849F, 64F);
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.Dpi = 254F;
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1968F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 146F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1849F, 100F);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079D;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3D;
        // 
        // xrTable6
        // 
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1829F, 80F);
        this.xrTable6.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Dpi = 254F;
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1D;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3D;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(286F, 11F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(1524F, 64F);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس - واحد عضویت";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(11F, 11F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(64F, 48F);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // xrLabel39
        // 
        this.xrLabel39.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(53F, 11F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(254F, 51F);
        this.xrLabel39.StylePriority.UseBorders = false;
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.Text = "(نام (انگلیسی";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 492F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1849F, 440F);
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1D;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell79.BorderWidth = 2;
        this.xrTableCell79.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrTableCell79.Dpi = 254F;
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UseBorderWidth = false;
        this.xrTableCell79.Weight = 3D;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1829F, 421F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTableCell64,
            this.xrTableCell63});
        this.xrTableRow23.Dpi = 254F;
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 0.78947368421052644D;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.Weight = 0.65650628758884633D;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.Weight = 1.7702296336796066D;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(296F, 169F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(495F, 53F);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "مشخصات شخص حقیقی";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell63.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.Weight = 0.57326407873154717D;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(21F, 42F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(300F, 250F);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell67,
            this.xrTableCell71,
            this.xrTableCell72});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.Weight = 0.15789473684210531D;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36,
            this.txtCurrentDate});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.65650628758884633D;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(315.25F, 0F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(85F, 42F);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = ":تاریخ";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.Weight = 1.7702296336796066D;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.Weight = 0.57326407873154717D;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 150F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 203F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.57326407873154717D;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.Weight = 1.7702296336796066D;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseTextAlignment = false;
        this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell74.Weight = 0.65650628758884633D;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.15789473684210506D;
        // 
        // txtCurrentDate
        // 
        this.txtCurrentDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.txtCurrentDate.Dpi = 254F;
        this.txtCurrentDate.Font = new System.Drawing.Font("Tahoma", 8F);
        this.txtCurrentDate.LocationFloat = new DevExpress.Utils.PointFloat(15.00001F, 2.142853F);
        this.txtCurrentDate.Name = "txtCurrentDate";
        this.txtCurrentDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.txtCurrentDate.SizeF = new System.Drawing.SizeF(300.2501F, 41.99997F);
        this.txtCurrentDate.StylePriority.UseBorders = false;
        this.txtCurrentDate.StylePriority.UseFont = false;
        this.txtCurrentDate.StylePriority.UseTextAlignment = false;
        this.txtCurrentDate.Text = ":شماره";
        this.txtCurrentDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRowFalooCode
        // 
        this.xrTableRowFalooCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableRowFalooCode.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell27,
            this.xrTFalooCode,
            this.xrTableCell33});
        this.xrTableRowFalooCode.Dpi = 254F;
        this.xrTableRowFalooCode.Name = "xrTableRowFalooCode";
        this.xrTableRowFalooCode.StylePriority.UseBorders = false;
        this.xrTableRowFalooCode.Weight = 0.41141386823995318D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.Weight = 2.08D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.Weight = 1.42D;
        // 
        // xrTFalooCode
        // 
        this.xrTFalooCode.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTFalooCode.Dpi = 254F;
        this.xrTFalooCode.Name = "xrTFalooCode";
        this.xrTFalooCode.StylePriority.UseBorders = false;
        this.xrTFalooCode.Text = "xrTFalooCode";
        this.xrTFalooCode.Weight = 2D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = ":کدپیگیری";
        this.xrTableCell33.Weight = 1.4010722100656454D;
        // 
        // XtraReportMembers
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(155, 34, 150, 203);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.Version = "11.2";
        this.Watermark.ShowBehind = false;
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }
}
