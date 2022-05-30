using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Office
/// </summary>
public class OfficeReports : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        
    TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();

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
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell74;
    private XRLabel xrLabel35;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRLabel xrLabel33;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell67;
    private XRLabel xrLabel36;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRLabel xrLabel34;
    private XRTable xrTable4;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell73;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTNameEn;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTName;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTRegDate;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTOfType;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel12;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTSubject;
    private XRTableCell xrTableCell12;
    private XRLabel xrLabel3;
    private XRLabel xrLabel15;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTStock;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTValue;
    private XRTableCell xrTableCell24;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTTel2;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTTel1;
    private XRTableCell xrTableCell32;
    private XRLabel xrLabel6;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTMobileNo;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTFax;
    private XRTableCell xrTableCell36;
    private XRLabel xrLabel9;
    private XRLabel xrLabel21;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTDesc;
    private XRTableCell xrTableCell70;
    private XRLabel xrLabel20;
    private XRLabel xrLabel17;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRLabel xrLabel30;
    private XRPictureBox xrPBArm;
    private XRPictureBox xrPBSign;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRSubreport xrSubLetters;
    private XRSubreport xrSubAgent;
    private XRSubreport xrSubOfficeJobHistory;
    private XRSubreport xrSubOfficeMembers;
    private XRSubreport xrSubFileNo;
    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel11;
    private XRTable xrTable7;
    private XRTableCell xrTFileNo;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTMFType;
    private XRTableCell xrTableCell25;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTLetterDate;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTLetterNo;
    private XRTableCell xrTableCell40;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTRequestDesc;
    private XRTableCell xrTableCell48;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTSerialNo;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTIsTemp;
    private XRTableCell xrTableCell52;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTExpireDate;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTFileNoRegDate;
    private XRTableCell xrTableCell58;
    private XRTableRow xrTRImpCapacity;
    private XRTableCell xrTCapacity;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTGrade;
    private XRTableCell xrTableCell66;
    private XRPageBreak xrPageBreak1;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTRegPlace;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTRegNo;
    private XRTableCell xrTableCell7;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTAddress;
    private XRTableCell xrTableCell13;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTWebSite;
    private XRTableCell xrTableCell14;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTEmail;
    private XRTableCell xrTableCell5;
    private XRLabel xrLabel1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel10;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OfficeReports(int OfId, int OfReId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************
        //OffManager.FindByCode(OfId);

        //xrTName.DataBindings.Add("Text", OffManager.DataTable, "OfName");
        //xrTNameEn.DataBindings.Add("Text", OffManager.DataTable, "OfNameEn");
        //xrTOfType.DataBindings.Add("Text", OffManager.DataTable, "OtName");
        //xrTRegDate.DataBindings.Add("Text", OffManager.DataTable, "RegDate");
        //xrTRegNo.DataBindings.Add("Text", OffManager.DataTable, "RegOfNo");
        //xrTRegPlace.DataBindings.Add("Text", OffManager.DataTable, "RegPlace");
        //xrTSubject.DataBindings.Add("Text", OffManager.DataTable, "Subject");
        //xrTValue.DataBindings.Add("Text", OffManager.DataTable, "VolumeInvest");
        //xrTStock.DataBindings.Add("Text", OffManager.DataTable, "Stock");
        //xrTAddress.DataBindings.Add("Text", OffManager.DataTable, "Address");
        //xrTTel1.DataBindings.Add("Text", OffManager.DataTable, "Tel1");
        //xrTTel2.DataBindings.Add("Text", OffManager.DataTable, "Tel2");
        //xrTFax.DataBindings.Add("Text", OffManager.DataTable, "Fax");
        //xrTMobileNo.DataBindings.Add("Text", OffManager.DataTable, "MobileNo");
        //xrTWebSite.DataBindings.Add("Text", OffManager.DataTable, "WebSite");
        //xrTEmail.DataBindings.Add("Text", OffManager.DataTable, "Email");
        //xrPBArm.DataBindings.Add("ImageUrl", OffManager.DataTable, "ArmUrl");
        //xrPBSign.DataBindings.Add("ImageUrl", OffManager.DataTable, "SignUrl");
        //xrTDesc.DataBindings.Add("Text", OffManager.DataTable, "Description");

        //SetFileNoProperty(OfId, OfReId);        

        ////OfficeAaza_Kardan ReKa = new OfficeAaza_Kardan(OfId );
        ////xrSubKardan.ReportSource = ReKa;

        ////OfficeAaza_Member ReMe = new OfficeAaza_Member(OfId);
        ////xrSubMember.ReportSource = ReMe;

        ////OfficeAaza_Others ReOt = new OfficeAaza_Others(OfId);
        ////xrSubOthers.ReportSource = ReOt;

        //OfficeAgent ReAg = new OfficeAgent(OfId,OfReId);
        //xrSubAgent.ReportSource = ReAg;

        //OfficeLetter ReLe = new OfficeLetter(OfId, OfReId);
        //xrSubLetters.ReportSource = ReLe;

        //OfficeJobHistory ReJH = new OfficeJobHistory(OfId, OfReId);
        //xrSubOfficeJobHistory.ReportSource = ReJH;

        //OfficeMembers ReMem = new OfficeMembers(OfId, OfReId);
        //xrSubOfficeMembers.ReportSource = ReMem;

        //xrTValue.DataBindings[0].FormatString = "{0:#,#}";
        
    }

    //private void SetFileNoProperty(int OfId, int OfReId)
    //{
    //    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();       

    //    ReqManager.FindByCode(OfReId);
    //    if (ReqManager.Count == 1)
    //    {
    //        int MFType = -1;
    //        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
    //            MFType = Convert.ToInt32(ReqManager[0]["MFType"]);

    //        SetGradesProperty(OfId, OfReId, MFType);

    //        if (ReqManager[0]["Type"].ToString() != "0")//درخواست های مربوط به پروانه باشد
    //        {
    //            xrTMFType.DataBindings.Add("Text", ReqManager.DataTable, "MFType");
    //            xrTFileNo.DataBindings.Add("Text", ReqManager.DataTable, "MFNo");
    //            xrTLetterNo.DataBindings.Add("Text", ReqManager.DataTable, "LetterNo");
    //            xrTLetterDate.DataBindings.Add("Text", ReqManager.DataTable, "LetterDate");
    //            xrTRequestDesc.DataBindings.Add("Text", ReqManager.DataTable, "RequestDesc");
    //            //xrTIsTemp.DataBindings.Add("Text", ReqManager.DataTable, "IsTemp");
    //            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
    //            {
    //                if (Convert.ToBoolean(ReqManager[0]["IsTemp"]))
    //                    xrTIsTemp.Text = "پروانه موقت";
    //                else
    //                    xrTIsTemp.Text = "پروانه دائم";
    //            }

    //            xrTSerialNo.DataBindings.Add("Text", ReqManager.DataTable, "SerialNo");
    //            xrTFileNoRegDate.DataBindings.Add("Text", ReqManager.DataTable, "RegDate");
    //            xrTExpireDate.DataBindings.Add("Text", ReqManager.DataTable, "ExpireDate");
    //        }

    //        else
    //        {
    //            xrTMFType.Text = " --- ";
    //            xrTFileNo.Text = " --- ";
    //            xrTLetterNo.Text = " --- ";
    //            xrTLetterDate.Text = " --- ";
    //            xrTRequestDesc.Text = " --- ";
    //            xrTIsTemp.Text = " --- ";
    //            xrTSerialNo.Text = " --- ";
    //            xrTFileNoRegDate.Text = " --- ";
    //            xrTExpireDate.Text = " --- ";
    //        }
    //    }
    //}

    //private void SetGradesProperty(int OfId, int OfReId, int MFType)
    //{
    //    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign || MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement)
    //    {
    //        OfficeFileNo ReFN = new OfficeFileNo(OfId, OfReId);
    //        xrSubFileNo.ReportSource = ReFN;

    //        xrTRImpCapacity.Visible = false;
    //        xrTExpireDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTExpireDate.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell54.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTFileNoRegDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTFileNoRegDate.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell58.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));

    //        xrPanel1.Height -= 55;
    //    }
    //    else if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
    //    {
    //        GetOfficeImpCapacity(OfId);
    //        xrTRImpCapacity.Visible = true;
    //    }
    //    else
    //    {
    //        xrTRImpCapacity.Visible = false;
    //        xrTExpireDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTExpireDate.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell54.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTFileNoRegDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTFileNoRegDate.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));
    //        xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)((xrTableCell58.Borders | DevExpress.XtraPrinting.BorderSide.Bottom)));

    //        xrPanel1.Height -= 55;            
    //    }
    //}

    //private void GetOfficeImpCapacity(int OfId)
    //{
    //    Capacity Capacity = new Capacity();
    //    ArrayList arr = Capacity.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Office, OfId);
    //    if (arr.Count != 0 && arr[0].ToString()!="0")
    //        xrTCapacity.Text = arr[0].ToString();
    //    else
    //        xrTCapacity.Text = " --- ";

    //    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
    //    int GrdId = OfficeMemberManager.FindOffImpGrade(OfId);
    //    if (GrdId != 0)
    //        xrTGrade.Text = GrdId.ToString();
    //    else
    //        xrTGrade.Text = " --- ";
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
        string resourceFileName = "Office.resx";
        DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        this.xrTFileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMFType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrSubLetters = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubAgent = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTNameEn = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRegDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOfType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRegPlace = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTRegNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTSubject = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTStock = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTValue = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAddress = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTel2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTel1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMobileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFax = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTWebSite = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTEmail = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPBSign = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPBArm = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTDesc = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubOfficeJobHistory = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrSubOfficeMembers = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.xrSubFileNo = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTLetterDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLetterNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRequestDesc = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTSerialNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTIsTemp = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTExpireDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFileNoRegDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTRImpCapacity = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrade = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
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
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // xrTableRow10
        // 
        xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTFileNo,
            this.xrTableCell10,
            this.xrTMFType,
            this.xrTableCell25});
        xrTableRow10.Dpi = 254F;
        xrTableRow10.Name = "xrTableRow10";
        xrTableRow10.Weight = 0.64705882352941169;
        // 
        // xrTFileNo
        // 
        this.xrTFileNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTFileNo.Dpi = 254F;
        this.xrTFileNo.Name = "xrTFileNo";
        this.xrTFileNo.StylePriority.UseBorders = false;
        this.xrTFileNo.Text = "xrTFileNo";
        this.xrTFileNo.Weight = 1.2240109718366719;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.Text = "شماره پروانه";
        this.xrTableCell10.Weight = 0.83391858303669664;
        // 
        // xrTMFType
        // 
        this.xrTMFType.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTMFType.Dpi = 254F;
        this.xrTMFType.Name = "xrTMFType";
        this.xrTMFType.StylePriority.UseBorders = false;
        this.xrTMFType.Text = "xrTMFType";
        this.xrTMFType.Weight = 1.1774284754853794;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell25.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.Weight = 0.83690236760311709;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(188, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(148, 42);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "نوع پروانه";
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubLetters,
            this.xrSubAgent,
            this.xrTable4,
            this.xrSubOfficeJobHistory,
            this.xrSubOfficeMembers,
            this.xrPageBreak1,
            this.xrSubFileNo,
            this.xrPanel1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 1844;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrSubLetters
        // 
        this.xrSubLetters.Dpi = 254F;
        this.xrSubLetters.Location = new System.Drawing.Point(0, 1757);
        this.xrSubLetters.Name = "xrSubLetters";
        this.xrSubLetters.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrSubAgent
        // 
        this.xrSubAgent.Dpi = 254F;
        this.xrSubAgent.Location = new System.Drawing.Point(0, 1503);
        this.xrSubAgent.Name = "xrSubAgent";
        this.xrSubAgent.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.BorderWidth = 2;
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Location = new System.Drawing.Point(0, 0);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
        this.xrTable4.Size = new System.Drawing.Size(1849, 682);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73});
        this.xrTableRow24.Dpi = 254F;
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 1;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Text = "xrTableCell73";
        this.xrTableCell73.Weight = 3;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable1.Location = new System.Drawing.Point(10, 10);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow19,
            this.xrTableRow3,
            this.xrTableRow6,
            this.xrTableRow4,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow5,
            this.xrTableRow11,
            this.xrTableRow7,
            this.xrTableRow21});
        this.xrTable1.Size = new System.Drawing.Size(1829, 660);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UsePadding = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.xrTableRow1.Weight = 0.42968749999999989;
        // 
        // xrTNameEn
        // 
        this.xrTNameEn.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTNameEn.Dpi = 254F;
        this.xrTNameEn.Name = "xrTNameEn";
        this.xrTNameEn.StylePriority.UseBorders = false;
        this.xrTNameEn.StylePriority.UseTextAlignment = false;
        this.xrTNameEn.Text = "xrTNameEn";
        this.xrTNameEn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTNameEn.Weight = 2.08;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "(نام شرکت (انگلیسی";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell1.Weight = 1.42;
        // 
        // xrTName
        // 
        this.xrTName.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTName.Dpi = 254F;
        this.xrTName.Name = "xrTName";
        this.xrTName.StylePriority.UseBorders = false;
        this.xrTName.StylePriority.UseTextAlignment = false;
        this.xrTName.Text = "xrTName";
        this.xrTName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTName.Weight = 2;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 1.42;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(146, 11);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(190, 42);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "نام شرکت";
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRegDate,
            this.xrTableCell6,
            this.xrTOfType,
            this.xrTableCell8});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.42968749999999989;
        // 
        // xrTRegDate
        // 
        this.xrTRegDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTRegDate.Dpi = 254F;
        this.xrTRegDate.Name = "xrTRegDate";
        this.xrTRegDate.StylePriority.UseBorders = false;
        this.xrTRegDate.Text = "xrTRegDate";
        this.xrTRegDate.Weight = 2.08;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBackColor = false;
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.Text = "تاریخ ثبت شرکت";
        this.xrTableCell6.Weight = 1.42;
        // 
        // xrTOfType
        // 
        this.xrTOfType.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTOfType.Dpi = 254F;
        this.xrTOfType.Name = "xrTOfType";
        this.xrTOfType.StylePriority.UseBorders = false;
        this.xrTOfType.Text = "xrTOfType";
        this.xrTOfType.Weight = 2;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 1.42;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel12.Location = new System.Drawing.Point(85, 0);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(254, 51);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.Text = "نوع شرکت";
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRegPlace,
            this.xrTableCell4,
            this.xrTRegNo,
            this.xrTableCell7});
        this.xrTableRow19.Dpi = 254F;
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 0.42968749999999989;
        // 
        // xrTRegPlace
        // 
        this.xrTRegPlace.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTRegPlace.Dpi = 254F;
        this.xrTRegPlace.Name = "xrTRegPlace";
        this.xrTRegPlace.StylePriority.UseBorders = false;
        this.xrTRegPlace.Text = "xrTRegPlace";
        this.xrTRegPlace.Weight = 2.08;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "محل ثبت شرکت";
        this.xrTableCell4.Weight = 1.42;
        // 
        // xrTRegNo
        // 
        this.xrTRegNo.Dpi = 254F;
        this.xrTRegNo.Name = "xrTRegNo";
        this.xrTRegNo.Text = "xrTRegNo";
        this.xrTRegNo.Weight = 2;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 1.42;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel15.Location = new System.Drawing.Point(82, 0);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.Size = new System.Drawing.Size(254, 51);
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "شماره ثبت شرکت";
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTSubject,
            this.xrTableCell12});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.42968750000000006;
        // 
        // xrTSubject
        // 
        this.xrTSubject.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTSubject.Dpi = 254F;
        this.xrTSubject.Name = "xrTSubject";
        this.xrTSubject.StylePriority.UseBorders = false;
        this.xrTSubject.Text = "xrTSubject";
        this.xrTSubject.Weight = 5.5;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 1.42;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel3.Location = new System.Drawing.Point(85, 0);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(254, 51);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "موضوع شرکت";
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTStock,
            this.xrTableCell22,
            this.xrTValue,
            this.xrTableCell24});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.42968750000000006;
        // 
        // xrTStock
        // 
        this.xrTStock.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTStock.Dpi = 254F;
        this.xrTStock.Name = "xrTStock";
        this.xrTStock.StylePriority.UseBorders = false;
        this.xrTStock.Text = "xrTStock";
        this.xrTStock.Weight = 2.08;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell22.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBackColor = false;
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.Text = "تعداد سهام شرکت";
        this.xrTableCell22.Weight = 1.42;
        // 
        // xrTValue
        // 
        this.xrTValue.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTValue.Dpi = 254F;
        this.xrTValue.Name = "xrTValue";
        this.xrTValue.StylePriority.UseBorders = false;
        this.xrTValue.Text = "xrTValue";
        this.xrTValue.Weight = 2;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBackColor = false;
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 1.42;
        // 
        // xrLabel21
        // 
        this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel21.Location = new System.Drawing.Point(61, 3);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.Size = new System.Drawing.Size(278, 50);
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.Text = "میزان سرمایه شرکت";
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAddress,
            this.xrTableCell13});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.42968750000000006;
        // 
        // xrTAddress
        // 
        this.xrTAddress.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTAddress.Dpi = 254F;
        this.xrTAddress.Name = "xrTAddress";
        this.xrTAddress.StylePriority.UseBorders = false;
        this.xrTAddress.Text = "xrTAddress";
        this.xrTAddress.Weight = 5.5;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.Weight = 1.42;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel9.Location = new System.Drawing.Point(82, 0);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.Size = new System.Drawing.Size(254, 51);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "آدرس شرکت";
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTel2,
            this.xrTableCell30,
            this.xrTTel1,
            this.xrTableCell32});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.42968750000000011;
        // 
        // xrTTel2
        // 
        this.xrTTel2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTTel2.Dpi = 254F;
        this.xrTTel2.Name = "xrTTel2";
        this.xrTTel2.StylePriority.UseBorders = false;
        this.xrTTel2.Text = "xrTTel2";
        this.xrTTel2.Weight = 2.08;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBackColor = false;
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.Text = "تلفن 2";
        this.xrTableCell30.Weight = 1.42;
        // 
        // xrTTel1
        // 
        this.xrTTel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTTel1.Dpi = 254F;
        this.xrTTel1.Name = "xrTTel1";
        this.xrTTel1.StylePriority.UseBorders = false;
        this.xrTTel1.Text = "xrTTel1";
        this.xrTTel1.Weight = 2;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell32.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBackColor = false;
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.Weight = 1.42;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel6.Location = new System.Drawing.Point(85, 0);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.Size = new System.Drawing.Size(254, 51);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "تلفن 1";
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTMobileNo,
            this.xrTableCell34,
            this.xrTFax,
            this.xrTableCell36});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.42968749999999978;
        // 
        // xrTMobileNo
        // 
        this.xrTMobileNo.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTMobileNo.Dpi = 254F;
        this.xrTMobileNo.Name = "xrTMobileNo";
        this.xrTMobileNo.StylePriority.UseBorders = false;
        this.xrTMobileNo.Text = "xrTMobileNo";
        this.xrTMobileNo.Weight = 2.08;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBackColor = false;
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.Text = "شماره همراه";
        this.xrTableCell34.Weight = 1.42;
        // 
        // xrTFax
        // 
        this.xrTFax.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTFax.Dpi = 254F;
        this.xrTFax.Name = "xrTFax";
        this.xrTFax.StylePriority.UseBorders = false;
        this.xrTFax.Text = "xrTFax";
        this.xrTFax.Weight = 2;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell36.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBackColor = false;
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.Text = "xrTableCell36";
        this.xrTableCell36.Weight = 1.42;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel17.Location = new System.Drawing.Point(82, 2);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.Size = new System.Drawing.Size(254, 51);
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.Text = "فکس";
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTWebSite,
            this.xrTableCell14});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.42968749999999978;
        // 
        // xrTWebSite
        // 
        this.xrTWebSite.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTWebSite.Dpi = 254F;
        this.xrTWebSite.Name = "xrTWebSite";
        this.xrTWebSite.StylePriority.UseBorders = false;
        this.xrTWebSite.Text = "xrTWebSite";
        this.xrTWebSite.Weight = 5.5;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel26});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 1.42;
        // 
        // xrLabel26
        // 
        this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel26.Location = new System.Drawing.Point(82, 0);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.Size = new System.Drawing.Size(254, 51);
        this.xrLabel26.StylePriority.UseBorders = false;
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.Text = "آدرس وب سایت";
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTEmail,
            this.xrTableCell5});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 0.42968749999999978;
        // 
        // xrTEmail
        // 
        this.xrTEmail.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTEmail.Dpi = 254F;
        this.xrTEmail.Name = "xrTEmail";
        this.xrTEmail.StylePriority.UseBorders = false;
        this.xrTEmail.Text = "xrTEmail";
        this.xrTEmail.Weight = 5.5;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.42;
        // 
        // xrLabel27
        // 
        this.xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel27.Location = new System.Drawing.Point(19, 3);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.Size = new System.Drawing.Size(318, 51);
        this.xrLabel27.StylePriority.UseBorders = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.Text = "آدرس پست الکترونیکی";
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell37,
            this.xrTableCell38});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.42968750000000011;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell27.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPBSign});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.Weight = 2.08;
        // 
        // xrPBSign
        // 
        this.xrPBSign.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPBSign.Dpi = 254F;
        this.xrPBSign.Location = new System.Drawing.Point(265, 5);
        this.xrPBSign.Name = "xrPBSign";
        this.xrPBSign.Size = new System.Drawing.Size(250, 250);
        this.xrPBSign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPBSign.StylePriority.UseBorders = false;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = "تصویر مهر شرکت";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell28.Weight = 1.42;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPBArm});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Text = "xrTableCell37";
        this.xrTableCell37.Weight = 2;
        // 
        // xrPBArm
        // 
        this.xrPBArm.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPBArm.Dpi = 254F;
        this.xrPBArm.Location = new System.Drawing.Point(251, 5);
        this.xrPBArm.Name = "xrPBArm";
        this.xrPBArm.Size = new System.Drawing.Size(250, 250);
        this.xrPBArm.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPBArm.StylePriority.UseBorders = false;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell38.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.Weight = 1.42;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel30.Location = new System.Drawing.Point(82, 0);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.Size = new System.Drawing.Size(254, 51);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "تصویر آرم شرکت";
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDesc,
            this.xrTableCell70});
        this.xrTableRow21.Dpi = 254F;
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.Weight = 0.42968749999999978;
        // 
        // xrTDesc
        // 
        this.xrTDesc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTDesc.Dpi = 254F;
        this.xrTDesc.Name = "xrTDesc";
        this.xrTDesc.StylePriority.UseBorders = false;
        this.xrTDesc.Text = "xrTDesc";
        this.xrTDesc.Weight = 5.5;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell70.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.xrTableCell70.Dpi = 254F;
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.StylePriority.UseBackColor = false;
        this.xrTableCell70.StylePriority.UseBorders = false;
        this.xrTableCell70.Text = "xrTableCell70";
        this.xrTableCell70.Weight = 1.42;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel20.Location = new System.Drawing.Point(85, 0);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.Size = new System.Drawing.Size(254, 51);
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.Text = "توضیحات";
        // 
        // xrSubOfficeJobHistory
        // 
        this.xrSubOfficeJobHistory.Dpi = 254F;
        this.xrSubOfficeJobHistory.Location = new System.Drawing.Point(0, 1630);
        this.xrSubOfficeJobHistory.Name = "xrSubOfficeJobHistory";
        this.xrSubOfficeJobHistory.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrSubOfficeMembers
        // 
        this.xrSubOfficeMembers.Dpi = 254F;
        this.xrSubOfficeMembers.Location = new System.Drawing.Point(0, 1376);
        this.xrSubOfficeMembers.Name = "xrSubOfficeMembers";
        this.xrSubOfficeMembers.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.Dpi = 254F;
        this.xrPageBreak1.Location = new System.Drawing.Point(0, 1270);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // xrSubFileNo
        // 
        this.xrSubFileNo.Dpi = 254F;
        this.xrSubFileNo.Location = new System.Drawing.Point(0, 1143);
        this.xrSubFileNo.Name = "xrSubFileNo";
        this.xrSubFileNo.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.BorderWidth = 2;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5,
            this.xrTable7});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 698);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 430);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel5
        // 
        this.xrPanel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel5.BorderWidth = 1;
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel11.Location = new System.Drawing.Point(1556, 11);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.Size = new System.Drawing.Size(262, 50);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseForeColor = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "پروانه";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.BorderWidth = 1;
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable7.Location = new System.Drawing.Point(10, 90);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow10,
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow16,
            this.xrTRImpCapacity});
        this.xrTable7.Size = new System.Drawing.Size(1829, 330);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseBorderWidth = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTLetterDate,
            this.xrTableCell26,
            this.xrTLetterNo,
            this.xrTableCell40});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.64705882352941169;
        // 
        // xrTLetterDate
        // 
        this.xrTLetterDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTLetterDate.Dpi = 254F;
        this.xrTLetterDate.Name = "xrTLetterDate";
        this.xrTLetterDate.StylePriority.UseBorders = false;
        this.xrTLetterDate.Text = "xrTLetterDate";
        this.xrTLetterDate.Weight = 1.2240109718366719;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.Text = "تاریخ نامه";
        this.xrTableCell26.Weight = 0.83391858303669664;
        // 
        // xrTLetterNo
        // 
        this.xrTLetterNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTLetterNo.Dpi = 254F;
        this.xrTLetterNo.Name = "xrTLetterNo";
        this.xrTLetterNo.StylePriority.UseBorders = false;
        this.xrTLetterNo.Text = "xrTLetterNo";
        this.xrTLetterNo.Weight = 1.1774284754853794;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell40.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.Weight = 0.83690236760311709;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel4.Location = new System.Drawing.Point(82, 3);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(254, 51);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "شماره نامه";
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRequestDesc,
            this.xrTableCell48});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 0.64705882352941169;
        // 
        // xrTRequestDesc
        // 
        this.xrTRequestDesc.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTRequestDesc.Dpi = 254F;
        this.xrTRequestDesc.Name = "xrTRequestDesc";
        this.xrTRequestDesc.StylePriority.UseBorders = false;
        this.xrTRequestDesc.Text = "xrTRequestDesc";
        this.xrTRequestDesc.Weight = 3.2353580303587477;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell48.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell48.Dpi = 254F;
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseBorders = false;
        this.xrTableCell48.Text = "xrTableCell48";
        this.xrTableCell48.Weight = 0.83690236760311709;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel5.Location = new System.Drawing.Point(82, 0);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(254, 51);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "شرح درخواست";
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTSerialNo,
            this.xrTableCell50,
            this.xrTIsTemp,
            this.xrTableCell52});
        this.xrTableRow14.Dpi = 254F;
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 0.64705882352941169;
        // 
        // xrTSerialNo
        // 
        this.xrTSerialNo.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTSerialNo.Dpi = 254F;
        this.xrTSerialNo.Name = "xrTSerialNo";
        this.xrTSerialNo.StylePriority.UseBorders = false;
        this.xrTSerialNo.Text = "xrTSerialNo";
        this.xrTSerialNo.Weight = 1.2240109718366719;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.StylePriority.UseBorders = false;
        this.xrTableCell50.Text = "شماره سریال";
        this.xrTableCell50.Weight = 0.83391858303669664;
        // 
        // xrTIsTemp
        // 
        this.xrTIsTemp.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTIsTemp.Dpi = 254F;
        this.xrTIsTemp.Name = "xrTIsTemp";
        this.xrTIsTemp.StylePriority.UseBorders = false;
        this.xrTIsTemp.Text = "xrTIsTemp";
        this.xrTIsTemp.Weight = 1.1774284754853794;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell52.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.xrTableCell52.Dpi = 254F;
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseBorders = false;
        this.xrTableCell52.Text = "xrTableCell52";
        this.xrTableCell52.Weight = 0.83690236760311709;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel7.Location = new System.Drawing.Point(82, 0);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.Size = new System.Drawing.Size(254, 51);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "موقت/دائم";
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTExpireDate,
            this.xrTableCell54,
            this.xrTFileNoRegDate,
            this.xrTableCell58});
        this.xrTableRow16.Dpi = 254F;
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 0.64705882352941169;
        // 
        // xrTExpireDate
        // 
        this.xrTExpireDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTExpireDate.Dpi = 254F;
        this.xrTExpireDate.Name = "xrTExpireDate";
        this.xrTExpireDate.StylePriority.UseBorders = false;
        this.xrTExpireDate.Text = "xrTExpireDate";
        this.xrTExpireDate.Weight = 1.2240109718366719;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseBorders = false;
        this.xrTableCell54.Text = "تاریخ پایان اعتبار";
        this.xrTableCell54.Weight = 0.83391858303669664;
        // 
        // xrTFileNoRegDate
        // 
        this.xrTFileNoRegDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTFileNoRegDate.Dpi = 254F;
        this.xrTFileNoRegDate.Name = "xrTFileNoRegDate";
        this.xrTFileNoRegDate.StylePriority.UseBorders = false;
        this.xrTFileNoRegDate.Text = "xrTFileNoRegDate";
        this.xrTFileNoRegDate.Weight = 1.1774284754853794;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell58.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.xrTableCell58.Dpi = 254F;
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.StylePriority.UseBorders = false;
        this.xrTableCell58.Text = "xrTableCell58";
        this.xrTableCell58.Weight = 0.83690236760311709;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel8.Location = new System.Drawing.Point(82, 0);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.Size = new System.Drawing.Size(254, 51);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "تاریخ آخرین تمدید";
        // 
        // xrTRImpCapacity
        // 
        this.xrTRImpCapacity.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCapacity,
            this.xrTableCell60,
            this.xrTGrade,
            this.xrTableCell66});
        this.xrTRImpCapacity.Dpi = 254F;
        this.xrTRImpCapacity.Name = "xrTRImpCapacity";
        this.xrTRImpCapacity.Weight = 0.64705882352941169;
        // 
        // xrTCapacity
        // 
        this.xrTCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTCapacity.Dpi = 254F;
        this.xrTCapacity.Name = "xrTCapacity";
        this.xrTCapacity.StylePriority.UseBorders = false;
        this.xrTCapacity.Text = "xrTCapacity";
        this.xrTCapacity.Weight = 1.2240109718366719;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell60.Dpi = 254F;
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.StylePriority.UseBorders = false;
        this.xrTableCell60.Text = "ظرفیت";
        this.xrTableCell60.Weight = 0.83391858303669664;
        // 
        // xrTGrade
        // 
        this.xrTGrade.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTGrade.Dpi = 254F;
        this.xrTGrade.Name = "xrTGrade";
        this.xrTGrade.StylePriority.UseBorders = false;
        this.xrTGrade.Text = "xrTGrade";
        this.xrTGrade.Weight = 1.1774284754853794;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell66.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrTableCell66.Dpi = 254F;
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.StylePriority.UseBorders = false;
        this.xrTableCell66.Text = "xrTableCell66";
        this.xrTableCell66.Weight = 0.83690236760311709;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel10.Location = new System.Drawing.Point(82, 0);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(254, 51);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "پایه اجرا";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 463;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Location = new System.Drawing.Point(0, 0);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable2.Size = new System.Drawing.Size(1849, 440);
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1;
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
        this.xrTableCell79.Weight = 3;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 9.75F);
        this.xrTable3.Location = new System.Drawing.Point(10, 10);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
        this.xrTable3.Size = new System.Drawing.Size(1829, 420);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
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
        this.xrTableRow23.Weight = 0.78947368421052644;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.Weight = 0.65650628758884633;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.Weight = 1.7702296336796066;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.Location = new System.Drawing.Point(296, 169);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.Size = new System.Drawing.Size(495, 53);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "مشخصات شخص حقوقی";
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
        this.xrTableCell63.Weight = 0.57326407873154717;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(21, 42);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(300, 250);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.15789473684210506;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell74.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseTextAlignment = false;
        this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell74.Weight = 0.65650628758884633;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel35.Location = new System.Drawing.Point(254, 0);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.Size = new System.Drawing.Size(127, 42);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = ":شماره";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.Weight = 1.7702296336796066;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell76.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33});
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.57326407873154717;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel33.Location = new System.Drawing.Point(183, 0);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.Size = new System.Drawing.Size(156, 42);
        this.xrLabel33.StylePriority.UseBorders = false;
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = ":شماره فرم";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.xrTableRow22.Weight = 0.15789473684210531;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.65650628758884633;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel36.Location = new System.Drawing.Point(296, 0);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.Size = new System.Drawing.Size(85, 42);
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
        this.xrTableCell71.Weight = 1.7702296336796066;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.Weight = 0.57326407873154717;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel34.Location = new System.Drawing.Point(148, 0);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.Size = new System.Drawing.Size(190, 42);
        this.xrLabel34.StylePriority.UseBorders = false;
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = ":شماره ویرایش";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 148;
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
        this.xrTable5.Location = new System.Drawing.Point(0, 21);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.Size = new System.Drawing.Size(1849, 100);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable6
        // 
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Location = new System.Drawing.Point(10, 10);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable6.Size = new System.Drawing.Size(1829, 80);
        this.xrTable6.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Dpi = 254F;
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel38.Location = new System.Drawing.Point(286, 11);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel38.Size = new System.Drawing.Size(1524, 64);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس ( واحد عضویت در سازمان )   تلفن:   8-071162" +
            "74194";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Location = new System.Drawing.Point(32, 11);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.Size = new System.Drawing.Size(64, 48);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        // 
        // Office
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(119, 122, 107, 201);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
