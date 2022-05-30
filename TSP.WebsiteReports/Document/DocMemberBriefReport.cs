using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace TSP.WebsiteReports.Document
{
    public partial class DocMemberBriefReport : DevExpress.XtraReports.UI.XtraReport
    {


        //public DocMemberBriefReport(int MeId, int MFId, int UserId)
        //{
        //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //    try
        //    {
        //        int ViewState = WorkFlowStateManager.InsertWorkFlowViewState((int)TSP.DataManager.TableCodes.DocMemberFile, MFId, "مشاهده پرونده پروانه اشتغال به کار", UserId);
        //        if (ViewState == -4)
        //        {
        //            //  ShowMessage("خطایی در مشاهده اطلاعات انجام گرفته است");
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        //  Utility.SaveWebsiteError(err);
        //    }
        //    InitializeComponent();
        //    LoadData(MeId, MFId);
        //}

        public DocMemberBriefReport(int MeId, int MFId)
        {
            InitializeComponent();
            LoadData(MeId, MFId);
        }

        private void LoadData(int MeId, int MFId)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();


            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                xrTFirstName.DataBindings.Add("Text", MemberManager.DataTable, "FirstName");
                xrTLastName.DataBindings.Add("Text", MemberManager.DataTable, "LastName");
                xrTMeId.DataBindings.Add("Text", MemberManager.DataTable, "MeId");
                xrTMeNo.DataBindings.Add("Text", MemberManager.DataTable, "MeNo");
                picBoxMeImage.DataBindings.Add("ImageUrl", MemberManager.DataTable, "ImageUrl");
                picBoxMeImage.DataBindings.Add("NavigateUrl", MemberManager.DataTable, "ImageUrl");
                xrPictureBoxKardan.DataBindings.Add("ImageUrl", MemberManager.DataTable, "NezamKardanConfirmURL");
                xrPictureBoxKardan.DataBindings.Add("NavigateUrl", MemberManager.DataTable, "NezamKardanConfirmURL");
            

                int MReId = -2;
                ReqManager.FindByMemberId(MeId, 0, 1, -1);
                if (ReqManager.Count <= 0)
                {
                    return;
                }
                MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
                if (attachManager.Count > 0)
                {
                    PictureBoxIdNo.ImageUrl = PictureBoxIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
                }

                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                if (attachManager.Count > 0)
                {
                    PictureBoxIdNoP2.ImageUrl = PictureBoxIdNoP2.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                }
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                if (attachManager.Count > 0)
                {
                    PictureBoxIdNoDes.ImageUrl = PictureBoxIdNoDes.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                }


                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
                if (attachManager.Count > 0)
                {
                    PictureBoxSSN.ImageUrl = PictureBoxSSN.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                }

                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
                if (attachManager.Count > 0)
                {
                    PictureBoxSSNBack.ImageUrl = PictureBoxSSNBack.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                }
                
                if (Convert.ToInt32(MemberManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                    if (attachManager.Count > 0)
                    {
                        PictureBoxSoldireURL.ImageUrl = PictureBoxSoldireURL.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                    }

                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                    if (attachManager.Count > 0)
                    {
                        PictureBoxSoldireBack.ImageUrl = PictureBoxSoldireBack.NavigateUrl =  attachManager[0]["FilePath"].ToString();
                    }

                    TableCellSoldireURL.Text = "تصاویر پشت و روی کارت پایان خدمت";
                    PictureBoxSoldireURL.Visible = true;
                    PictureBoxSoldireBack.Visible = true;
                }
                else
                {
                    TableCellSoldireURL.Text = "";
                    PictureBoxSoldireURL.Visible = false;
                    PictureBoxSoldireBack.Visible = false;
                }
            }

            string RequestDate = "1";
            DocMemberFileManager.FindByCode(MFId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                xrTEtebarDate.DataBindings.Add("Text", DocMemberFileManager.DataTable, "ExpireDate");
                xrTTamdidDate.DataBindings.Add("Text", DocMemberFileManager.DataTable, "LastRevivalDate");
                xrTMFNO.DataBindings.Add("Text", DocMemberFileManager.DataTable, "MFNo");
                xrTMovaghat.DataBindings.Add("Text", DocMemberFileManager.DataTable, "IsTemporaryName");
                xrTFirstRegDate.DataBindings.Add("Text", DocMemberFileManager.DataTable, "FirstRegDate");
                xrTMFType.DataBindings.Add("Text", DocMemberFileManager.DataTable, "MFType");            

                xrPictureBoxImgOldDocBackURL.DataBindings.Add("ImageUrl", DocMemberFileManager.DataTable, "ImgOldDocBackURL");
                xrPictureBoxImgOldDocBackURL.DataBindings.Add("NavigateUrl", DocMemberFileManager.DataTable, "ImgOldDocBackURL");

                xrPictureBoxImgOldDocFrontURL.DataBindings.Add("ImageUrl", DocMemberFileManager.DataTable, "ImgOldDocFrontURL");
                xrPictureBoxImgOldDocFrontURL.DataBindings.Add("NavigateUrl", DocMemberFileManager.DataTable, "ImgOldDocFrontURL");

                xrPictureBoxTaxOfficeLetterURL.DataBindings.Add("ImageUrl", DocMemberFileManager.DataTable, "TaxOfficeLetterURL");
                xrPictureBoxTaxOfficeLetterURL.DataBindings.Add("NavigateUrl", DocMemberFileManager.DataTable, "TaxOfficeLetterURL");

                xrPictureBoxJooshPeriod.DataBindings.Add("ImageUrl", DocMemberFileManager.DataTable, "PeriodImageURL");
                xrPictureBoxJooshPeriod.DataBindings.Add("NavigateUrl", DocMemberFileManager.DataTable, "PeriodImageURL");

                xrPictureBoxHSE.DataBindings.Add("ImageUrl", DocMemberFileManager.DataTable, "ImgHSEURL");
                xrPictureBoxHSE.DataBindings.Add("NavigateUrl", DocMemberFileManager.DataTable, "ImgHSEURL");

                RequestDate = DocMemberFileManager[0]["CreateDate"].ToString();
            }

            //*****Responsiblity*********
            #region Responsiblity
            //DataTable dtResDes = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
            //if (dtResDes.Rows.Count == 1)
            //{
            //    xrTTarahi.DataBindings.Add("Text", dtResDes, "GrdName");
            //}
            //else
            //    xrTTarahi.Text = "----------";
            //DataTable dtResObs = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            //if (dtResObs.Rows.Count == 1)
            //{
            //    xrTNezarat.DataBindings.Add("Text", dtResObs, "GrdName");
            //}
            //else
            //    xrTNezarat.Text = "----------";
            //DataTable dtResImp = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
            //if (dtResImp.Rows.Count == 1)
            //{
            //    xrTEjra.DataBindings.Add("Text", dtResImp, "GrdName");
            //}
            //else
            //    xrTEjra.Text = "----------";

            //DataTable dtResMapping = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
            //if (dtResMapping.Rows.Count == 1)
            //{
            //    xrTMapping.DataBindings.Add("Text", dtResMapping, "GrdName");
            //}
            //else
            //    xrTMapping.Text = "----------";
            //DataTable dtResTraffic = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Traffic);
            //if (dtResTraffic.Rows.Count == 1)
            //{
            //    xrTTraffic.DataBindings.Add("Text", dtResTraffic, "GrdName");
            //}
            //else
            //    xrTTraffic.Text = "----------";
            //DataTable dtResUrbanism = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism);
            //if (dtResUrbanism.Rows.Count == 1)
            //{
            //    xrTUrban.DataBindings.Add("Text", dtResUrbanism, "GrdName");
            //}
            //else
            //    xrTUrban.Text = "----------";
            #endregion


        

            AllMemberLicenceReport AllMemberLicenceReport = new AllMemberLicenceReport(MeId,true);
            xrSubreportAllLicence.ReportSource = AllMemberLicenceReport;

          

            MemberExamReport MemberExamReport = new MemberExamReport(MeId, MFId,true);
            xrSubreportExam.ReportSource = MemberExamReport;

            MemberPeriodsReport MemberPeriodsReport = new MemberPeriodsReport(MeId, RequestDate);
            xrSubreportPeriod.ReportSource = MemberPeriodsReport;

            DocJobConfirm DocJobConfirm = new DocJobConfirm(MFId,true);
            xrSubreportJobConfirm.ReportSource = DocJobConfirm;

            lblCurrentDateTime.Text = "تاریخ چاپ " + TSP.DataManager.Utility.GetDateOfToday() + " ساعت " + TSP.DataManager.Utility.GetCurrentTime();
        }

    }
}
