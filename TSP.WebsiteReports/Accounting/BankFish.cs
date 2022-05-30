using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Text;

namespace TSP.WebsiteReports.Accounting
{
    public partial class BankFish : DevExpress.XtraReports.UI.XtraReport
    {
        static PrivateFontCollection fontCollection;
        //public static FontCollection FontCollection
        //{
        //    get
        //    {
        //        if (fontCollection == null)
        //        {
        //            fontCollection = new PrivateFontCollection();
        //            fontCollection.AddFontFile( HttpContext.Current.Server.MapPath("~/Fonts/BNAZNNBD.ttf"));
        //        }
        //        return fontCollection;
        //    }
        //}
        public BankFish(int AccountingId, int ProjectId, int PrjReId, int PlnTypeId)
        {
            InitializeComponent();
            //customFontStyle.Font = new Font(FontCollection.Families[0], 48F, FontStyle.Regular, GraphicsUnit.Point);

            int MemberRegisterationType = 0;
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new DataManager.TechnicalServices.AccountingManager();
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new DataManager.TechnicalServices.ProjectRequestManager();
            TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
            TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
            TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();

            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new DataManager.TechnicalServices.AccountingDetailManager();

            Clear();
            Boolean IsKardanPayment = false;
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1) return;
            int ProjectCitId = -2;
            int MunId = -2;
            string TafziliCode = "";
            string TafziliCodeProvince = "";
            string AgentCodeForPaymentIdProvince = "";
            Boolean HavePcPose = false;
            int AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
            lblDate.Text = lblDate2.Text = AccountingManager[0]["Date"].ToString();//TSP.DataManager.Utility.GetDateOfToday();
            lblAccountName.Text = lblAccountName2.Text = TSP.DataManager.Utility.GetAccountingOwnerName();
            try
            {
                double amount = Convert.ToDouble(AccountingManager[0]["Amount"]);
                lblAmount.Text = lblAmount2.Text = amount.ToString("#,#");
                lblAmountAlph.Text = lblAmountAlph2.Text = TSP.DataManager.Utility.ConvertNumberToPersianNumber(amount.ToString());

                if (ProjectId != -2)
                {
                    #region TS
                    string OwnerName = "", OwnerSSN = "";
                    System.Data.DataTable dtPrjReq = ProjectRequestManager.GetProjectInfo(PrjReId);
                    if (dtPrjReq.Rows.Count > 0)
                    {
                        string RegisteredNo = "";
                        string MainSection = "";
                        string MainRegion = "";
                        if (dtPrjReq.Rows[0]["AgentCodeForPaymentIdProvince"] != null)
                        {
                            //***در دو صفحه دیگر هم این کد هست اعمال شودPrjAccDesignerInsert/PrjAccountingInsert
                            AgentCodeForPaymentIdProvince = dtPrjReq.Rows[0]["AgentCodeForPaymentIdProvince"].ToString();
                            HavePcPose = true;
                        }
                        if (dtPrjReq.Rows[0]["RegisteredNo"] != null)
                        {
                            RegisteredNo += " پلاک " + dtPrjReq.Rows[0]["RegisteredNo"].ToString();
                        }
                        if (dtPrjReq.Rows[0]["MainSection"] != null)
                        {
                            RegisteredNo += " قطعه " + dtPrjReq.Rows[0]["MainSection"].ToString();
                        }
                        if (dtPrjReq.Rows[0]["MainRegion"] != null)
                        {
                            RegisteredNo += " ناحیه " + dtPrjReq.Rows[0]["MainRegion"].ToString();
                        }
                        OwnerName = dtPrjReq.Rows[0]["OwnerName"].ToString() + RegisteredNo + MainSection + MainRegion;
                        OwnerSSN = dtPrjReq.Rows[0]["OwnerSSN"].ToString();
                        ProjectCitId = Convert.ToInt32(dtPrjReq.Rows[0]["CitId"]);
                        MunId = Convert.ToInt32(dtPrjReq.Rows[0]["MunId"]);
                    }
                    lblOwnerName.Text = lblOwnerName2.Text = OwnerName;
                    #endregion
                }
                else
                {
                    lblOwnerName.Text = lblOwnerName2.Text = AccountingManager[0]["FishPayerName"].ToString();
                }

                lblAccountNumber.Text = lblAccountNumber2.Text = TSP.DataManager.Utility.GetAccountingNumber(AccType, ProjectCitId);
                switch (AccType)
                {
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                        #region Observer
                        ProjectObserversManager.FindActivesByProjectId(ProjectId);
                        int ObsCount = ProjectObserversManager.Count;
                        lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString();
                        TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS(TSP.DataManager.TSAccountingAccType.ObserversFiche, ProjectId.ToString(), "99999");// ProjectId.ToString();
                        TafziliCodeProvince = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince(TSP.DataManager.TSAccountingAccType.ObserversFiche, AgentCodeForPaymentIdProvince, ProjectId.ToString(), "99999");
                        #endregion
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                        string ObsMeId = "";
                        string ObsName = "";
                        int ObserverCount = ProjectObserversManager.Count;
                        AccountingDetailManager.FindByAccountingId(AccountingId);
                        for (int i = 0; i < AccountingDetailManager.Count; i++)
                        {
                            ObsMeId = AccountingDetailManager[i]["FishPayerId"].ToString();
                            ObsName += " کد " + AccountingDetailManager[i]["FishPayerFilNo"].ToString() + " " + AccountingDetailManager[i]["FishPayerName"].ToString();
                            if (i != ObserverCount - 1)
                                ObsName += "\n";
                        }
                        lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString() + ObsName;

                        TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, ProjectId.ToString(), ObsMeId);// ObsMeId;
                        TafziliCodeProvince = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, AgentCodeForPaymentIdProvince, ProjectId.ToString(), "99999");
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType._5In1000:

                        lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString();

                        TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, ProjectId.ToString(), "99999");// ProjectId.ToString();
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType._2In1000:
                        #region Implementer
                        ProjectImplementerManager.FindImpMother(ProjectId);
                        if (ProjectImplementerManager.Count == 1)
                        {
                            lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString() + " کد " + ProjectImplementerManager[0]["No"].ToString() + " " + ProjectImplementerManager[0]["Name"].ToString();
                        }
                        TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, ProjectId.ToString(), "99999");
                        #endregion
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                    case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                    case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                        #region Designer
                        lblDesignerMesg.Visible =
                        lblDesignerMesg2.Visible = true;
                        int PrjDesignerId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
                        System.Data.DataTable dt = Designer_PlansManager.SelectTSDesignerPlansForDesignerFish(PrjDesignerId);
                        if (dt.Rows.Count >0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                            {
                                IsKardanPayment = true;
                                lblDesignerMesg.Text = lblDesignerMesg2.Text = "پرداخت وجه 6% به عهده کاردان طراح مربوطه می باشد";
                            }
                            lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString() + " کد " + dt.Rows[0]["FileNo"].ToString() + " " + dt.Rows[0]["DesignerName"].ToString();
                            string MeId = dt.Rows[0]["DesignerMeId"].ToString();
                            if (Convert.ToInt32(dt.Rows[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                            {
                                TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
                                OthpManager.FindByCode(Convert.ToInt32(dt.Rows[0]["OfficeEngOId"]));
                                MeId = OthpManager[0]["OtpCode"].ToString();
                                TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, ProjectId.ToString(), MeId);
                                TafziliCodeProvince = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, AgentCodeForPaymentIdProvince, ProjectId.ToString(), MeId);
                            }
                            else
                            {
                                if (MunId == 76)
                                {
                                    //**برای صدرا اول کدعضویت باید باشد بعد کد پروژه 
                                    TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, MeId, ProjectId.ToString());
                                }
                                else
                                {
                                    TafziliCode = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, ProjectId.ToString(), MeId);
                                }
                                TafziliCodeProvince = Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, AgentCodeForPaymentIdProvince, ProjectId.ToString(), MeId);
                            }
                        }

                        #endregion
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType.Entrance:
                    case (int)TSP.DataManager.TSAccountingAccType.Registeration:
                    case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                        int MReId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
                        TSP.DataManager.MemberRequestManager MemberRequestManager = new DataManager.MemberRequestManager();
                        MemberRequestManager.FindByCode(MReId);
                        if (MemberRequestManager.Count == 1)
                        {
                            TafziliCode = MemberRequestManager[0]["TMeId"].ToString();
                        }
                        MemberRegisterationType = 1;
                        lblPayerAddress.Text = lblPayerAddress2.Text = AccountingManager[0]["AccTypeName"].ToString();
                        break;
                }
                string PaymentId = Utility.OnlinePayment.GetPaymentId((DataManager.TSAccountingAccType)AccType, TafziliCode, MemberRegisterationType, MunId, IsKardanPayment);
                string PaymentIdPOS = "";
                if (AccType == (int)TSP.DataManager.TSAccountingAccType.Entrance ||
                   AccType == (int)TSP.DataManager.TSAccountingAccType.Registeration ||
                   AccType == (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance)
                {
                    PaymentIdPOS = PaymentId;
                    lblPaymentIdPOSTop.Text = lblPaymentIdPOSDown.Text = "شناسه کارت خوان: " + PaymentIdPOS;
                    if (AccountingManager[0]["PaymentIdPOS"] == null || AccountingManager[0]["PaymentIdPOS"].ToString() == "")
                        SavePaymentIdPOS(AccountingManager, AccountingId, PaymentIdPOS);
                    lblPaymentIdDown.Text = lblPaymentIdTop.Text = "شناسه پرداخت: " + PaymentId;
                }


                if (AccountingManager[0]["PaymentId"] == null || AccountingManager[0]["PaymentId"].ToString() == "")
                    SavePaymentId(AccountingManager, AccountingId, PaymentId);
                string PaymentIdProvnce = "";
                if (AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFiche ||
                  AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation ||
                  AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure ||
                  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent ||
                  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation ||
                  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure)
                {
                    PaymentIdProvnce = Utility.OnlinePayment.GetPaymentIdForProvince((DataManager.TSAccountingAccType)AccType, TafziliCodeProvince, MunId, IsKardanPayment);
                    lblPaymentIdProvinceTop.Text = lblPaymentIdProvinceDown.Text = "شناسه استان: " + PaymentIdProvnce;
                }
                else
                    lblPaymentIdProvinceTop.Text = lblPaymentIdProvinceDown.Text = "";



                if (HavePcPose)
                {
                    lblPaymentIdDown.Text = lblPaymentIdTop.Text = lblPaymentIdPOSTop.Text = lblPaymentIdPOSDown.Text = "";
                }

            }
            catch (Exception ex)
            {
                lblBranch.Text = ex.Message;



            }
        }



        private void Clear()
        {
            lblDate.Text = lblDate2.Text = "";
            lblAccountName.Text = lblAccountName2.Text = "";
            lblAccountNumber.Text = lblAccountNumber2.Text = "";
            lblAmount.Text = lblAmount2.Text = "";
            lblAmountAlph.Text = lblAmountAlph2.Text = "";
            lblPayerAddress.Text = lblPayerAddress2.Text = "";
            lblSSN.Text = lblSSN2.Text = "";
            lblOwnerName.Text = lblOwnerName2.Text = "";
        }

        private void SavePaymentId(TSP.DataManager.TechnicalServices.AccountingManager AccountingManager, int AccountingId, string PaymentId)
        {
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["PaymentId"] = PaymentId;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
        }

        private void SavePaymentIdPOS(TSP.DataManager.TechnicalServices.AccountingManager AccountingManager, int AccountingId, string PaymentIdPOS)
        {
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["PaymentIdPOS"] = PaymentIdPOS;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
        }

        //public static void SaveWebsiteError(Exception err)
        //{
        //    (new TSP.DataManager.WebsiteErrorsManager()).InsertError(err, HttpContext.Current.Request.Url.AbsoluteUri, GetCurrentUser_UserId());
        //}

    }
}

