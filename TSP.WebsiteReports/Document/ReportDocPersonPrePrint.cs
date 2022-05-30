using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.Document
{
    public partial class ReportDocPersonPrePrint : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportDocPersonPrePrint(int MeId)
        {
            InitializeComponent();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DataTable dt = DocMemberFileManager.SelectForReportMemberFile(MeId, -1);
            if (dt.Rows.Count == 1)
                SetRowSource(dt.Rows[0], true);
        }


        public ReportDocPersonPrePrint(int MfId, Boolean FromFollowPage)
        {
            InitializeComponent();
            if (FromFollowPage)
                picBoxMeImage.Visible = true;
            else
                picBoxMeImage.Visible = false;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DataTable dt = DocMemberFileManager.SelectForReportMemberFile(-1, -1, MfId);
            if (dt.Rows.Count == 1)
            {
                if (Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 1)
                    SetRowSource(dt.Rows[0], true);
                else
                    SetRowSource(dt.Rows[0], false);
            }            
        }
        public ReportDocPersonPrePrint(int MfId, int MeId)
        {
            InitializeComponent();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DataTable dt = DocMemberFileManager.SelectForReportMemberFile(-1, -1, MfId);
            if (dt.Rows.Count == 1)
            {
                    SetRowSource(dt.Rows[0], true);

            }
        }
        #region Methods
        private void ClearAllControls()
        {
            for (int i = 0; i < this.xrTableInfo.Rows.Count; i++)
                for (int j = 0; j < this.xrTableInfo.Rows[i].Cells.Count; j++)
                    xrTableInfo.Rows[i].Cells[j].Text = "";

            for (int i = 0; i < this.TableMajors.Rows.Count; i++)
                for (int j = 0; j < this.TableMajors.Rows[i].Cells.Count; j++)
                    TableMajors.Rows[i].Cells[j].Text = "";

            for (int i = 0; i < tblFishesh.Rows.Count; i++)
            {
                tblFishesh.Rows[i].Cells[0].Text = "";
            }
        }

        public void SetRowSource(System.Data.DataRow drv, Boolean ShowMfNo)
        {
            ClearAllControls();
            txtDate.Text = DataManager.Utility.GetDateOfToday();
            if (!ShowMfNo)
            {
                this.xrLabelDocNo.Visible = false;
            }
            #region Set Member BaseInfo
            this.xrLabelPersonName.Text = DataManager.Utility.GetCorrectText(drv["MeFullName"]);
            //string MeFullName = DataManager.Utility.GetCorrectText(drv["MeFullName"]);
            string Gender = DataManager.Utility.GetCorrectText(drv["Gender"]);
            this.xrLabelIdNo.Text = DataManager.Utility.GetCorrectText(drv["IdNo"]);
            this.xrLabelIssue.Text = DataManager.Utility.GetCorrectText(drv["BirthPlace"]);
            this.xrLabelBirthDate.Text = DataManager.Utility.GetCorrectText(drv["BirhtDate"]);
            this.xrLabelMemberNo.Text = DataManager.Utility.GetCorrectText(drv["MeNo"]);
            this.xrLabelDocNo.Text = DataManager.Utility.GetCorrectText(drv["FileNo"]);
            this.xrLabelSSN.Text = DataManager.Utility.GetCorrectText(drv["SSN"]);
            this.xrLabelAddress.Text = DataManager.Utility.GetCorrectText(drv["HomeAdr"]);

            if (!DataManager.Utility.IsDBNullOrNullValue(drv["PrNameOrigin"]))
                this.xrLabelProvince2.Text = DataManager.Utility.GetCorrectText(drv["PrNameOrigin"]);
            else
                this.xrLabelProvince2.Text = DataManager.Utility.GetCorrectText(drv["PrName"]);
            #endregion

            if (drv["FollowCode"] != null)
                lblFollowingCode.Text = drv["FollowCode"].ToString();
            //******************************************************************************************************************************************************************            
            int TableTypeMeFile = DataManager.TableTypeManager.FindTtId(DataManager.TableType.DocMemberFileDetail);
            int MfId = Convert.ToInt32(drv["MfId"]);
            int MeId = Convert.ToInt32(drv["MeId"]);
            int Type = Convert.ToInt32(drv["Type"]);
            if (drv["ImageUrl"] != null)
                picBoxMeImage.ImageUrl = drv["ImageUrl"].ToString();
            #region مقدار دهی تاریخ های اعتبار
            //***********************تاریخ های اعتبار*************************
            this.xrLabelExpireDate.Text = DataManager.Utility.GetCorrectText(drv["ExpireDate"]);
            if (CheckIsTransferAndSetDate(MeId, Type))
            {
                if (Type == (int)DataManager.DocumentOfMemberRequestType.Transfer)
                    this.xrLabelLastRenew.Text = "";
                else
                    this.xrLabelLastRenew.Text = DataManager.Utility.GetCorrectText(drv["RegDate"]);
            }
            else
            {
                this.xrLabelFirstIssue.Text = DataManager.Utility.GetCorrectText(drv["FirstRegDate"]);

                if (IsRegDateSet(MfId, MeId, Type))
                {
                    this.xrLabelLastRenew.Text = DataManager.Utility.GetCorrectText(drv["RegDate"]);
                }
                else
                {
                    this.xrLabelLastRenew.Text = "";
                }
            }
            #endregion
            int RowCount = 0;
            string ResSting = " در رشته ";
            string MasteMjCode = "";
            int MasteMjId = -1;
            int MasteMlId = -1;
           // int TitleId = 1;
            //****************صلاحیت ها و پایه های رشته موضوع پروانه
            RowCount = SetMasterRsponsiblity(MfId, MeId, RowCount, ResSting, ref MasteMjCode, ref MasteMjId, ref MasteMlId);
            //****************صلاحیت ها و پایه های سایر رشته های پروانه
            SetOtherRsponsiblity(MfId, MeId, RowCount, ResSting, MasteMjCode, MasteMjId);
            //***************************************************************           
            DataManager.TechnicalServices.AccountingManager AccountingManager = new DataManager.TechnicalServices.AccountingManager();
            DataTable dtAccounting = AccountingManager.FindByTableTypeId(MfId, DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile));
            for (int i = 0; i < dtAccounting.Rows.Count; i++)
            {
                if (i > tblFishesh.Rows.Count - 1)
                    AddNewRowToFishTable(i);
                tblFishesh.Rows[i].Cells[0].Text = "مبلغ فیش: " + Convert.ToDecimal(dtAccounting.Rows[i]["Amount"]).ToString("#,#") + "ریال  " + "  شماره فیش:" + dtAccounting.Rows[i]["Number"].ToString() + "  " + " تاریخ فیش:" + dtAccounting.Rows[i]["Date"].ToString();
            }
        }

        private string GetActTypeName(string TableCodeMeDocDetail, int MfId, int MeId, int ResId)
        {
            string par1 = "(";
            string par2 = ")";
            string ActTypeName = "";
            DataManager.TableTypeManager TableTypeManager = new DataManager.TableTypeManager();
            TableTypeManager.FindByTtCode(DataManager.TableType.DocMemberFileDetail);
            DataTable dtTableType = TableTypeManager.DataTable;
            //  spSelectTableTypeTableAdapter.Fill(dtTableType, -1, TableCodeMeDocDetail);
            int TableType = -1;
            if (dtTableType.Rows.Count > 0)
            {
                TableType = (int)dtTableType.Rows[0]["TtId"];
            }

            //  spSelectDocMemberFileAcceptTypeForReportTableAdapter.Fill(dtDocMemberFileAcceptType, MfId, MeId, ResId, TableType);
            DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new DataManager.DocMemberFileDetailManager();
            DataTable dtDocMemberFileAcceptType = DocMemberFileDetailManager.SelectDocMemberFileAcceptTypeForReport(MfId, MeId, ResId, TableType, -1);
            if (dtDocMemberFileAcceptType.Rows.Count == 1)
            {
                if (dtDocMemberFileAcceptType.Rows[0]["ActTypeName"] != null
                    && !string.IsNullOrWhiteSpace(dtDocMemberFileAcceptType.Rows[0]["ActTypeName"].ToString()))
                {
                    ActTypeName = dtDocMemberFileAcceptType.Rows[0]["ActTypeName"].ToString();
                    ActTypeName = par1 + ActTypeName + par2;
                }
            }
            dtDocMemberFileAcceptType.Clear();
            return ActTypeName;
        }

        private Boolean IsRegDateSet(int MfId, int MeId, int Type)
        {
            //  NezamDocPrint.DataSetNezamTableAdapters.spSelectDocMemberFileTableAdapter spSelectDocMemberFileTableAdapter = new DataSetNezamTableAdapters.spSelectDocMemberFileTableAdapter();
            //  NezamDocPrint.DataSetNezam.spSelectDocMemberFileDataTable dtDocMemberFile = new DataSetNezam.spSelectDocMemberFileDataTable();
            //    spSelectDocMemberFileTableAdapter.Fill(dtDocMemberFile, -1, MeId, -1, -1, (int)DataManager.DocumentTypesOfMember.DocMemberFile, 
            //(int)DataManager.WorkFlows.DocumentOfMemberConfirming, -1, (int)DataManager.DocumentOfMemberRequestType.OldDocumentRenew, 0);
            DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DocMemberFileManager.FindDocument(MeId, (int)DataManager.DocumentTypesOfMember.DocMemberFile, -1, -1, (int)DataManager.DocumentOfMemberRequestType.OldDocumentRenew, 0);
            DataTable dtDocMemberFile = DocMemberFileManager.DataTable;
            if (dtDocMemberFile.Rows.Count > 0)
                return true;
            else
            {
                if (Type == (int)DataManager.DocumentOfMemberRequestType.Qualification
                   || Type == (int)DataManager.DocumentOfMemberRequestType.ReDuplicate
                     || Type == (int)DataManager.DocumentOfMemberRequestType.Revival
                     || Type == (int)DataManager.DocumentOfMemberRequestType.UpGrade)
                {
                    return true;
                }
                else
                {
                    dtDocMemberFile.Clear();
                    //   spSelectDocMemberFileTableAdapter.Fill(dtDocMemberFile, -1, MeId, -1, -1, (int)DataManager.DocumentTypesOfMember.DocMemberFile, (int)DataManager.WorkFlows.DocumentOfMemberConfirming, 1, -1, 0);
                    DocMemberFileManager.FindDocument(MeId, (int)DataManager.DocumentTypesOfMember.DocMemberFile, -1, 1, -1, 0);
                    dtDocMemberFile = DocMemberFileManager.DataTable;
                    if (dtDocMemberFile.Rows.Count > 0)
                    {
                        DataRow[] dr = dtDocMemberFile.Select("Type=" + ((int)DataManager.DocumentOfMemberRequestType.Qualification).ToString()
                                                    + " OR " + "Type=" + ((int)DataManager.DocumentOfMemberRequestType.ReDuplicate).ToString()
                                                    + " OR " + "Type=" + ((int)DataManager.DocumentOfMemberRequestType.Revival).ToString()
                                                    + " OR " + "Type=" + ((int)DataManager.DocumentOfMemberRequestType.UpGrade).ToString()
                                                     + " OR " + "Type=" + ((int)DataManager.DocumentOfMemberRequestType.New).ToString());
                        if (dr.Length > 0)
                            return true;
                    }
                }
            }

            return false;
        }

        private Boolean CheckIsTransferAndSetDate(int MeId, int Type)
        {
            Boolean CheckTransfer = false;
           
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DocMemberFileManager.FindDocument(MeId, 0, -1, 1, -1, 0);
            
            DocMemberFileManager.CurrentFilter = "Type=" + ((int)TSP.DataManager.DocumentOfMemberRequestType.Transfer).ToString() + " OR " + "Type=" + ((int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival).ToString();
            if (DocMemberFileManager.DataTable.DefaultView.Count > 0)
                CheckTransfer = true;
            if (CheckTransfer
              || Type == (int)DataManager.DocumentOfMemberRequestType.Transfer || Type == (int)DataManager.DocumentOfMemberRequestType.TransferAndRevival)
            {               
                DataManager.TransferMemberManager TransferMemberManager = new DataManager.TransferMemberManager();
                DataTable dtTransfer = TransferMemberManager.FindByMemberId(MeId, DataManager.TransferMemberType.TransferFromOtherProvince);
                if (dtTransfer.Rows.Count == 0)
                {

                    dtTransfer = TransferMemberManager.FindByMemberId(MeId, DataManager.TransferMemberType.ReturnToCurrentProvince);
                    if (dtTransfer.Rows.Count == 0)
                        return false;
                }
                if (DataManager.Utility.IsDBNullOrNullValue(dtTransfer.Rows[0]["FileNo"]))
                    return false;
                if (!DataManager.Utility.IsDBNullOrNullValue(dtTransfer.Rows[0]["FirstDocRegDate"]))
                {
                    this.xrLabelFirstIssue.Text = dtTransfer.Rows[0]["FirstDocRegDate"].ToString();
                }
                else
                {
                    xrLabelFirstIssue.Text = "";
                }
            }
            else
                return false;
            return true;
        }
        //************************************************************************************
        private int SetMasterRsponsiblity(int MfId, int MeId, int RowCount, string ResSting, ref string MasteMjCode, ref int MasteMjId, ref int MasteMlId)
        {
            string BehindMajorName = "";
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            DataTable ListDocMajors = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, 0, 1);
            for (int i = 0; i < ListDocMajors.Rows.Count; i++)
            {
                //if (!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[0]["LicenceCode"]) && Convert.ToInt32(ListDocMajors.Rows[0]["LicenceCode"]) == 7)
                //    TitleId = 7;
                //************Major's Info****************
                if (!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[0]["LiName"]))
                    this.xrLabelLicence.Text = ListDocMajors.Rows[0]["LiName"].ToString();
                if (!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[0]["MjName"]))
                    this.xrLabelPersonMajor.Text = ListDocMajors.Rows[i]["MjName"].ToString();
                if (!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[0]["UnName"]))
                    this.xrLabelUniversity.Text = ListDocMajors.Rows[0]["UnName"].ToString();
                if (!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[0]["EndDate"]))
                    this.lblUnEndDate.Text = ListDocMajors.Rows[i]["EndDate"].ToString();
               
                BehindMajorName = ListDocMajors.Rows[i]["MjName"].ToString();
                MasteMlId = Convert.ToInt32(ListDocMajors.Rows[i]["MlId"]);

                SetConditionalMajorInfo(MeId, MfId);
                //******************************************
                MasteMjCode = ListDocMajors.Rows[i]["FMjCode"].ToString();
                MasteMjId = Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]);
                //   var ListRes = DocMemberFileDetailManager.SelectResponsblityByMajor(MfId, MeId, ((int)DataManager.TableType.DocMemberFileMajor).ToString(), ((int)DataManager.TableType.DocMemberFileDetail).ToString(), ListDocMajors[i].FMjId);
                DataTable ListRes = DocMemberFileDetailManager.FindDocMemberFileMaxResponsibility(MfId, MeId, Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]));
                if (ListRes.Rows.Count > 0)
                {
                    int FMjParentId = Convert.ToInt32(ListDocMajors.Rows[i]["FMjParentId"]);
                    #region Find ActTypeName
                    //******************Find ActTypeName****************************************************
                    ArrayList ArrNameObs = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                    ArrayList ArrNameDes = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Design, FMjParentId);
                    ArrayList ArrNameImp = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                    ArrayList ArrNameMapping = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Mapping, FMjParentId);
                    ArrayList ArrNameTraffic = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Traffic, FMjParentId);
                    ArrayList ArrNameUrbanism = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Urbanism, FMjParentId);
                    ArrayList ArrNameGas = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Gas, FMjParentId);
                    //*******************************************************************************************************************************************************************
                    #endregion

                    if (FMjParentId != (int)DataManager.MainMajors.Traffic
                            && FMjParentId != (int)DataManager.MainMajors.Urbanism
                            && FMjParentId != (int)DataManager.MainMajors.Mapping)
                    {
                        #region
                        if (IsDesignAndObservesionNeedToJoin(MfId, MeId, FMjParentId,
                            Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]), ListRes.Rows[0]["ObsGrade"].ToString(), ListRes.Rows[0]["DesGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            String ResDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design);
                            if (String.IsNullOrWhiteSpace(ResDate))
                                ResDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[1].Text = ResDate;
                            if (Convert.ToBoolean(ArrNameObs[3]))
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"].ToString() + "-نظارت و طراحی  " + ArrNameObs[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameObs[0];
                            else
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameObs[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameObs[0];
                            //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes.Rows[0].DesGrade + "-" + " نظارت و طراحی";
                            RowCount++;
                        }
                        else
                        {
                            if (ListRes.Rows[0]["ObsGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ObsGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                                if (Convert.ToBoolean(ArrNameObs[3]))
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ObsGrade"] + "-نظارت  " + ArrNameObs[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameObs[0];
                                else
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameObs[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameObs[0];
                                //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ObsGrade"] + "-" + " نظارت";
                                RowCount++;
                            }
                            if (ListRes.Rows[0]["DesGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["DesGrade"].ToString()))
                            {
                                if (FMjParentId == ((int)DataManager.MainMajors.Civil))
                                {
                                    if (xrTableInfo.Rows.Count <= RowCount)
                                    {
                                        AddNewRowToTable(RowCount);
                                    }
                                    xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                    xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                    if (Convert.ToBoolean(ArrNameDes[3]))
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"] + "-محاسبات  " + ArrNameDes[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameDes[0];
                                    else
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameDes[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameDes[0];
                                }
                                else
                                {
                                    if (xrTableInfo.Rows.Count <= RowCount)
                                    {
                                        AddNewRowToTable(RowCount);
                                    }
                                    xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                    xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                    if (Convert.ToBoolean(ArrNameDes[3]))
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"] + "-طراحی  " + ArrNameDes[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameDes[0];
                                    else
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameDes[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameDes[0];
                                }
                                RowCount++;
                            }
                        }

                        if (ListRes.Rows[0]["GasGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["GasGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            } xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Gas, FMjParentId);
                            if (Convert.ToBoolean(ArrNameGas[3]))
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["GasGrade"] + "-نظارت  " + ArrNameGas[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameGas[0];
                            else
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameGas[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameGas[0];
                            RowCount++;
                        }

                        if (ListRes.Rows[0]["ImpGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ImpGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            } xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                            if (Convert.ToBoolean(ArrNameImp[3]))
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ImpGrade"] + "-اجرا  " + ArrNameImp[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameImp[0];
                            else
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameImp[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameImp[0];
                            RowCount++;
                        }

                        #endregion
                    }
                    else
                    {
                        #region  //اگر رشته ترافیک-شهرسازی-نقشه برداری باشد و نام صلاحیت نمایش داده نمی شود
                        if (ListRes.Rows[0]["MappingGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["MappingGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Mapping, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["MappingGrade"] + " " + ArrNameMapping[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameMapping[0];
                            //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].MappingGrade;
                            RowCount++;
                        }
                        if (ListRes.Rows[0]["TrafficGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["TrafficGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Traffic, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["TrafficGrade"].ToString() + " " + ArrNameTraffic[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameTraffic[0];
                            //    this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].TrafficGrade;
                            RowCount++;
                        }
                        if (ListRes.Rows[0]["UrbanismGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["UrbanismGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Urbanism, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["UrbanismGrade"].ToString() + " " + ArrNameUrbanism[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameUrbanism[0];
                            //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].UrbanismGrade;
                            RowCount++;
                        }

                        if (ListRes.Rows[0]["ImpGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ImpGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ImpGrade"].ToString() + " " + ArrNameImp[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameImp[0];
                            //   this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].ImpGrade;
                            RowCount++;
                        }
                        if (ListRes.Rows[0]["ObsGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ObsGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ObsGrade"] + " " + ArrNameObs[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameObs[0];
                            //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].ObsGrade;
                            RowCount++;
                        }
                        if (ListRes.Rows[0]["DesGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["DesGrade"].ToString()))
                        {
                            if (xrTableInfo.Rows.Count <= RowCount)
                            {
                                AddNewRowToTable(RowCount);
                            }
                            xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                            xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"].ToString() + " " + ArrNameDes[1] + ResSting + BehindMajorName + Environment.NewLine + ArrNameDes[0];
                            //  this.xrTableGrade.Rows[RowCount].Cells[0].Text = ListRes[0].DesGrade;
                            RowCount++;
                        }

                        #endregion
                    }
                }
            }
            return RowCount;
        }

        private void SetOtherRsponsiblity(int MfId, int MeId, int RowCount, string ResSting, string MasteMjCode, int MasteMjId)
        {
            //RowCount++;
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            DataTable ListDocMajors = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, 0, 0);
            string FMjParentCode = "";
            int IndexMemberLicenceByMeId = 0;
            for (int i = 0; i < ListDocMajors.Rows.Count; i++)
            {
                //if (!!DataManager.Utility.IsDBNullOrNullValue(ListDocMajors.Rows[i]["LicenceCode"]) && Convert.ToInt32(ListDocMajors.Rows[i]["LicenceCode"]) == 7)
                //    TitleId = 7;
                if (IndexMemberLicenceByMeId < TableMajors.Rows.Count && Convert.ToBoolean(ListDocMajors.Rows[i]["IsPrinted"]))
                {
                    TableMajors.Rows[IndexMemberLicenceByMeId].Cells[2].Text = DataManager.Utility.GetCorrectText(ListDocMajors.Rows[i]["LiName"]);
                    TableMajors.Rows[IndexMemberLicenceByMeId].Cells[1].Text = DataManager.Utility.GetCorrectText(ListDocMajors.Rows[i]["MjName"]);
                    TableMajors.Rows[IndexMemberLicenceByMeId].Cells[0].Text = DataManager.Utility.GetCorrectText(ListDocMajors.Rows[i]["EndDate"]);
                    IndexMemberLicenceByMeId++;
                }
                FMjParentCode = ListDocMajors.Rows[i]["FMjParentCode"].ToString();
                //*******برای افرادی که رشته کارشناسی و ارشد آنها دقیقا یکی است.تا از چاپ شدن مجدد صلاحیت ها جلوگیری کند
                if (MasteMjCode != ListDocMajors.Rows[i]["FMjCode"].ToString() || MasteMjId != Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]))
                {
                    var ListRes = DocMemberFileDetailManager.FindDocMemberFileMaxResponsibility(MfId, MeId, Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]));

                    if (ListRes.Rows.Count > 0)
                    {
                        int FMjParentId = Convert.ToInt32(ListDocMajors.Rows[i]["FMjParentId"]);
                        #region Find ActTypeName
                        //******************Find ActTypeName****************************************************
                        ArrayList ArrNameObs = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                        ArrayList ArrNameDes = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Design, FMjParentId);
                        ArrayList ArrNameImp = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                        ArrayList ArrNameMapping = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Mapping, FMjParentId);
                        ArrayList ArrNameTraffic = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Traffic, FMjParentId);
                        ArrayList ArrNameUrbanism = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Urbanism, FMjParentId);
                        ArrayList ArrNameGas = GetActTypeName(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Gas, FMjParentId);

                        //*******************************************************************************************************************************************************************
                        #endregion

                        if (FMjParentId != (int)DataManager.MainMajors.Traffic
                                && FMjParentId != (int)DataManager.MainMajors.Urbanism
                                && FMjParentId != (int)DataManager.MainMajors.Mapping)
                        {
                            if (IsDesignAndObservesionNeedToJoin(MfId, MeId, FMjParentId, Convert.ToInt32(ListDocMajors.Rows[i]["FMjId"]), ListRes.Rows[0]["ObsGrade"].ToString(), ListRes.Rows[0]["DesGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                                String ResDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                if (String.IsNullOrWhiteSpace(ResDate))
                                    ResDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[1].Text = ResDate;
                                if (Convert.ToBoolean(ArrNameObs[3]))
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"].ToString() + "-نظارت و طراحی  " + ArrNameObs[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameObs[0];
                                else
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameObs[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameObs[0];
                                RowCount++;
                            }
                            else
                            {
                                if (ListRes.Rows[0]["ObsGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ObsGrade"].ToString()))
                                {
                                    if (xrTableInfo.Rows.Count <= RowCount)
                                    {
                                        AddNewRowToTable(RowCount);
                                    }
                                    xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"].ToString() + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"].ToString();
                                    xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                                    if (Convert.ToBoolean(ArrNameObs[3]))
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ObsGrade"].ToString() + "-نظارت  " + ArrNameObs[1] + ResSting + ListDocMajors.Rows[i]["MjName"].ToString() + Environment.NewLine + ArrNameObs[0];
                                    else
                                        xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameObs[1] + ResSting + ListDocMajors.Rows[i]["MjName"].ToString() + Environment.NewLine + ArrNameObs[0];
                                    RowCount++;
                                }
                                if (ListRes.Rows[0]["DesGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["DesGrade"].ToString()))
                                {
                                    if (FMjParentId == ((int)DataManager.MainMajors.Civil))
                                    {
                                        if (xrTableInfo.Rows.Count <= RowCount)
                                        {
                                            AddNewRowToTable(RowCount);
                                        }
                                        xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                        xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                        if (Convert.ToBoolean(ArrNameDes[3]))
                                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"] + "-محاسبات  " + ArrNameDes[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameDes[0];
                                        else
                                            xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameDes[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameDes[0];
                                    }
                                    else
                                    {
                                        if (xrTableInfo.Rows.Count <= RowCount)
                                        {
                                            AddNewRowToTable(RowCount);
                                        }
                                        xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                        xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                        if (Convert.ToBoolean(ArrNameDes[3]))
                                            xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"] + "-طراحی  " + ArrNameDes[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameDes[0];
                                        else
                                            xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameDes[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameDes[0];
                                    }
                                    RowCount++;
                                }
                            }

                            if (ListRes.Rows[0]["GasGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["GasGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Gas, FMjParentId);
                                if (Convert.ToBoolean(ArrNameGas[3]))
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["GasGrade"] + "-نظارت  " + ArrNameGas[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameGas[0];
                                else
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameGas[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameGas[0];
                            }

                            if (ListRes.Rows[0]["ImpGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ImpGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                                if (Convert.ToBoolean(ArrNameImp[3]))
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ImpGrade"] + "-اجرا  " + ArrNameImp[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameImp[0];
                                else
                                    xrTableInfo.Rows[RowCount].Cells[0].Text = ArrNameImp[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameImp[0];
                            }
                        }
                        else
                        {
                            #region  //اگر رشته ترافیک-شهرسازی-نقشه برداری باشد و نام صلاحیت نمایش داده نمی شود
                            if (ListRes.Rows[0]["MappingGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["MappingGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Mapping, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["MappingGrade"] + " " + ArrNameMapping[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameMapping[0];
                                RowCount++;
                            }
                            if (ListRes.Rows[0]["TrafficGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["TrafficGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Traffic, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["TrafficGrade"] + " " + ArrNameTraffic[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameTraffic[0];
                                RowCount++;
                            }
                            if (ListRes.Rows[0]["UrbanismGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["UrbanismGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Urbanism, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["UrbanismGrade"] + " " + ArrNameUrbanism[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameUrbanism[0];
                                RowCount++;
                            }

                            if (ListRes.Rows[0]["ImpGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ImpGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Implement, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ImpGrade"] + " " + ArrNameImp[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameImp[0];
                                RowCount++;
                            }
                            if (ListRes.Rows[0]["ObsGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["ObsGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["ObsGrade"] + " " + ArrNameObs[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameObs[0];
                                RowCount++;
                            }
                            if (ListRes.Rows[0]["DesGrade"] != null && !string.IsNullOrEmpty(ListRes.Rows[0]["DesGrade"].ToString()))
                            {
                                if (xrTableInfo.Rows.Count <= RowCount)
                                {
                                    AddNewRowToTable(RowCount);
                                }
                                xrTableInfo.Rows[RowCount].Cells[2].Text = ListDocMajors.Rows[i]["LiName"] + Environment.NewLine + " کد:" + ListDocMajors.Rows[i]["FMjCode"];
                                xrTableInfo.Rows[RowCount].Cells[1].Text = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, FMjParentId);
                                xrTableInfo.Rows[RowCount].Cells[0].Text = ListRes.Rows[0]["DesGrade"] + " " + ArrNameDes[1] + ResSting + ListDocMajors.Rows[i]["MjName"] + Environment.NewLine + ArrNameDes[0];
                            }

                            #endregion
                        }
                    }
                }
            }
        }

        /// <summary>
        /// اگر در تعریف یک رشته چاپ در گواهینامه انتخاب شود آن مدرک نیز چاپ می شود
        /// این مورد جهت افرادی است که مدرک دکترا و کارشناسی ارشد دارند اما مقطع قبلی آنها موضوع پروانه می باشد
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="MfId"></param>
        private void SetConditionalMajorInfo(int MeId, int MfId)
        {
            //TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            //var ListDocMajors = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, 0);
            //var PrintedMajor = ListDocMajors.Where(a => a.IsPrinted == true && a.IsMaster == false).ToList();
            //if (PrintedMajor.Count == 1)
            //{
            //    this.xrLabelLicence.Text = PrintedMajor[0].LiName;
            //}
        }

        Boolean IsDesignAndObservesionNeedToJoin(int MfId, int MeId, int MjParentId, int MjId, string ObsGrade, string DesGrade)
        {
            if (MjParentId == (int)DataManager.MainMajors.Electronic || MjParentId == (int)DataManager.MainMajors.Mechanic || MjParentId == (int)DataManager.MainMajors.Architecture)
                if (ObsGrade != null && !string.IsNullOrEmpty(ObsGrade)
                    && DesGrade != null && !string.IsNullOrEmpty(DesGrade))
                {
                    String DesignDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Design, MjParentId);
                    String ObsDate = GetResponsibiltyDate(MeId, MfId, DataManager.DocumentResponsibilityType.Observation, MjParentId);
                    ArrayList ArrObsActType = GetActTypeId(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Observation, MjParentId);
                    ArrayList ArrDesignActType = GetActTypeId(MfId, MeId, (int)DataManager.DocumentResponsibilityType.Design, MjParentId);
                    int DesActTypeId = Convert.ToInt32(ArrDesignActType[0]);
                    int ObsActTypeId = Convert.ToInt32(ArrObsActType[0]);
                    int DesResRangeId = Convert.ToInt32(ArrDesignActType[1]);
                    int ObsResRangeId = Convert.ToInt32(ArrObsActType[1]);
                    if (ObsGrade != DesGrade || DesignDate != ObsDate || DesActTypeId != ObsActTypeId || DesResRangeId != ObsResRangeId)
                        return false;
                    else
                        return true;
                }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <param name="MjId"></param>
        /// <returns>ArrNames[0]:ActTypeName ;ArrNames[1]:ResRangeTextOnCard</returns>
        private ArrayList GetActTypeName(int MfId, int MeId, int ResId, int MjId)
        {
            ArrayList ArrNames = new ArrayList();
            ArrNames.Add("");
            ArrNames.Add("");
            ArrNames.Add("");
            ArrNames.Add("");
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
            string par1 = "- ";// "(";
            string par2 = "";//  ")";
            string ActTypeName = "";
            string ResRangeTextOnCard = "";
            string ResRangeTextOnCardDescription = "";// "محدوده صلاحیت";
            Boolean IsGradePrint = true;//چاپ پایه و صلاحیت

            DataTable ActType = DocMemberFileAcceptTypeManager.SelectDocMemberFileAcceptTypeForReport(MfId, MeId, ResId, MjId);

            if (ActType.Rows.Count == 1)
            {
                if (ActType.Rows[0]["ActTypeName"] != null
                    && !string.IsNullOrWhiteSpace(ActType.Rows[0]["ActTypeName"].ToString()))
                {
                    ActTypeName = ActType.Rows[0]["ActTypeName"].ToString();
                    ActTypeName = par1 + ActTypeName + par2;
                }

                if (ActType.Rows[0]["ResRangeTextOnCard"] != null
                    && !string.IsNullOrWhiteSpace(ActType.Rows[0]["ResRangeTextOnCard"].ToString()))
                {
                    ResRangeTextOnCard = ActType.Rows[0]["ResRangeTextOnCard"].ToString();
                    ResRangeTextOnCard = par1 + ResRangeTextOnCard + par2;
                }

                if (ActType.Rows[0]["ResRangeDes"] != null
                  && !string.IsNullOrWhiteSpace(ActType.Rows[0]["ResRangeDes"].ToString()))
                {
                    ResRangeTextOnCardDescription = ActType.Rows[0]["ResRangeDes"].ToString();
                }
                if (ActType.Rows[0]["IsGradePrint"] != null)
                {
                    IsGradePrint = (bool)ActType.Rows[0]["IsGradePrint"];
                }
            }

            ArrNames[0] = ActTypeName;
            ArrNames[1] = ResRangeTextOnCard;
            ArrNames[2] = ResRangeTextOnCardDescription;
            ArrNames[3] = IsGradePrint;
            return ArrNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <param name="MjId"></param>
        /// <returns>Arr[0]:ActTypeId ;Arr[1]:ResRangeId</returns>
        private ArrayList GetActTypeId(int MfId, int MeId, int ResId, int MjId)
        {
            ArrayList ArrId = new ArrayList();
            ArrId.Add("-1");
            ArrId.Add("-1");
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
            DataTable ActType = DocMemberFileAcceptTypeManager.SelectDocMemberFileAcceptTypeForReport(MfId, MeId, ResId, MjId);

            if (ActType.Rows.Count == 1)
            {
                if (ActType.Rows[0]["ActTypeId"] != null
                    && !string.IsNullOrWhiteSpace(ActType.Rows[0]["ActTypeId"].ToString()))
                {
                    ArrId[0] = ActType.Rows[0]["ActTypeId"].ToString();
                }

                if (ActType.Rows[0]["ResRangeId"] != null
                    && !string.IsNullOrWhiteSpace(ActType.Rows[0]["ResRangeId"].ToString()))
                {
                    ArrId[1] = ActType.Rows[0]["ResRangeId"].ToString();
                }
            }
            return ArrId;
        }

        private String GetResponsibiltyDate(int MeId, int MfId, DataManager.DocumentResponsibilityType ResponsibilityType, int MjId)
        {
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            var ListResDate = DocMemberFileDetailManager.SelectReportMemberDetailFileRegDate(MeId, MfId, (int)ResponsibilityType, MjId);
            if (ListResDate.Rows.Count > 0)
                return ListResDate.Rows[0]["RegDate"].ToString();
            else
                return "";
        }

        private String GetResponsibiltyDate(int MeId, int MfId, DataManager.DocumentResponsibilityType ResponsibilityType)
        {
            return GetResponsibiltyDate(MeId, MfId, ResponsibilityType, -1);

        }

        private void AddNewRowToTable(int RowCount)
        {
            xrTableInfo.Rows[xrTableInfo.Rows.LastRow.Index].Cells[0].Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left))));
            xrTableInfo.Rows[xrTableInfo.Rows.LastRow.Index].Cells[1].Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.None))));
            xrTableInfo.Rows[xrTableInfo.Rows.LastRow.Index].Cells[2].Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right))));
            XRTableCell cell0 = new XRTableCell();
            cell0.StyleName = xrTableInfo.Rows[RowCount - 1].StyleName;
            cell0.Location = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[0]).Location;
            cell0.Size = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[0]).Size;
            cell0.Padding = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[0]).Padding;
            cell0.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left))));
            cell0.CanShrink = true;

            XRTableCell cell1 = new XRTableCell();
            cell1.StyleName = xrTableInfo.Rows[RowCount - 1].StyleName;
            cell1.Location = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[1]).Location;
            cell1.Size = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[1]).Size;
            cell1.Padding = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[1]).Padding;
            cell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right))));
            cell1.CanShrink = true;
            XRTableCell cell2 = new XRTableCell();
            cell2.StyleName = xrTableInfo.Rows[RowCount - 1].StyleName;
            cell2.Location = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[2]).Location;
            cell2.Size = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[2]).Size;
            cell2.Padding = ((XRTableCell)xrTableInfo.Rows[RowCount - 1].Cells[2]).Padding;
            cell2.CanShrink = true;
            cell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom))));
            XRTableRow newRow = new XRTableRow();
            newRow.CanShrink = true;
            newRow.Cells.Add(cell0);
            newRow.Cells.Add(cell1);
            newRow.Cells.Add(cell2);

            this.xrTableInfo.Rows.Add(newRow);
        }

        private void AddNewRowToFishTable(int RowCount)
        {
            tblFishesh.Rows[tblFishesh.Rows.LastRow.Index].Cells[0].Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left))));

            XRTableCell cell0 = new XRTableCell();
            cell0.StyleName = tblFishesh.Rows[RowCount - 1].StyleName;
            cell0.Location = ((XRTableCell)tblFishesh.Rows[RowCount - 1].Cells[0]).Location;
            cell0.Size = ((XRTableCell)tblFishesh.Rows[RowCount - 1].Cells[0]).Size;
            cell0.Padding = ((XRTableCell)tblFishesh.Rows[RowCount - 1].Cells[0]).Padding;
            cell0.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left))));
            cell0.CanShrink = true;

            XRTableRow newRow = new XRTableRow();
            newRow.CanShrink = true;
            newRow.Cells.Add(cell0);

            this.tblFishesh.Rows.Add(newRow);
        }
        #endregion
    }
}
