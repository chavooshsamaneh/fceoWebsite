using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectObserverWageByDocuments : DevExpress.XtraReports.UI.XtraReport
    {
        #region Properties
        private int _AccDocId;
        private int AccDocIdPage
        {
            get
            {
                return _AccDocId; ;
            }
            set
            {
                _AccDocId = value;
            }
        }

        private int _RowCount;
        private int RowCount
        {
            get
            {
                return _RowCount; ;
            }
            set
            {
                _RowCount = value;
            }
        }



        private Boolean _shoulePageBreak;
        private Boolean shoulePageBreak
        {
            get
            {
                return _shoulePageBreak; ;
            }
            set
            {
                _shoulePageBreak = value;
            }
        }
        #endregion

        public ProjectObserverWageByDocuments(int AccDocId, int AgentId)
        {
            InitializeComponent();
            if (AgentId != -2)
            {
                lblTSControl.Visible = lblTsManager.Visible = false;
                lblRaees.Text = "رئیس دفتر نمایندگی";
            }
            else
            {
                lblTSControl.Visible = lblTsManager.Visible = true;
                lblRaees.Text = "رئیس سازمان";
            }
            RowCount = 0;
            AccDocIdPage = AccDocId;

            this.DataSource = null;
            this.DataMember = null;

            lblSumObserverValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumInsuranceShareValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumNezamShareValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumAmount.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumNezamKardan.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);

            lblPrintDate.Text = "تاریخ صدور:" + TSP.DataManager.Utility.GetDateOfToday();

            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new DataManager.TechnicalServices.AccountingDocumentDetailManager();
            TSP.DataManager.TechnicalServices.AccountingDocumentManager AccountingDocumentManager = new DataManager.TechnicalServices.AccountingDocumentManager();
            AccountingDocumentManager.FindByCode(AccDocId);
            if (AccountingDocumentManager.Count != 1)
                return;
            if (Convert.ToInt32(AccountingDocumentManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingdocumentStatus.SendReportToAccountingUnit && AccountingDocumentManager[0]["ListDate"] != null && AccountingDocumentManager[0]["ListNo"] != null)
                lblTitle.Text = "لیست پرداخت حق الزحمه ناظرین مورخ " + AccountingDocumentManager[0]["ListDate"].ToString() + " شماره لیست: " + AccountingDocumentManager[0]["ListNo"].ToString();

            DataTable dtObserverProjects = AccountingDocumentDetailManager.SelectAccountingDocumentDetailProjectsList(AccDocId);

            this.DataSource = dtObserverProjects;
            this.DataMember = dtObserverProjects.TableName;
            if (dtObserverProjects.Rows.Count > 0)
            {

                TRowOwnere.DataBindings.Add("Text", dtObserverProjects, "OwnerName");
                TRowRegisteredNo.DataBindings.Add("Text", dtObserverProjects, "RegisteredNo");
                TRowProjectId.DataBindings.Add("Text", dtObserverProjects, "ProjectId");
                TRowRemainAmount.DataBindings.Add("Text", dtObserverProjects, "RemainAmount", "{0:#,#}");
                TRowSumAmount.DataBindings.Add("Text", dtObserverProjects, "SumAmount", "{0:#,#}");
                lblSumRemain.DataBindings.Add("Text", dtObserverProjects, "SumRemainAmount", "{0:#,#}");
                TRowFishNumber.DataBindings.Add("Text", dtObserverProjects, "FishNumber");
                TRowFishDate.DataBindings.Add("Text", dtObserverProjects, "FishDate");
                this.DataSource = dtObserverProjects;

                //lblSumInsuranceShareValue.DataBindings.Add("Text", dtObserverProjects, "InsuranceShare", "{0:#,#}");


                //lblSumAmount.DataBindings.Add("Text", dtObserverProjects, "Amount", "{0:#,#}");
                //lblSumNezamShareValue.DataBindings.Add("Text", dtObserverProjects, "NezamShare", "{0:#,#}");
                //lblSumObserverValue.DataBindings.Add("Text", dtObserverProjects, "ObserverShare", "{0:#,#}");
                //lblSumNezamKardan.DataBindings.Add("Text", dtObserverProjects, "NezamKardanShare", "{0:#,#}");



            }


        }

        private void DetailReportObserver_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int CurrentProjectId = Convert.ToInt32(this.GetCurrentColumnValue("ProjectId"));

            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new DataManager.TechnicalServices.AccountingDocumentDetailManager();


            AccountingDocumentDetailManager.SearchAccountingDocDetail(AccDocIdPage, CurrentProjectId);

            DetailReportObserver.DataSource = AccountingDocumentDetailManager.DataTable;
            DetailReportObserver.DataMember = AccountingDocumentDetailManager.DataTable.TableName;

            if (AccountingDocumentDetailManager.DataTable.Rows.Count > 0)
            {
                TRowBankAccNo.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "BankAccNo");
                TRowMeId.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "MeId");
                TRowObserverName.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "ObserverName");
                TRowNezamPrice.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "NezamShare", "{0:#,#}");
                TRowObserverInsurancePrice.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "InsuranceShare", "{0:#,#}");
                TRowObserverWagePrice.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "ObserverShare", "{0:#,#}");
                TRowAmountPayment.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "Amount", "{0:#,#}");
                TRowNezamKardanShare.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "NezamKardanShare", "{0:#,#}");
                TRowMeType.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "MeType");


                lblSumInsuranceShareValue.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "InsuranceShare", "{0:#,#}");
                lblSumNezamShareValue.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "NezamShare", "{0:#,#}");
                lblSumObserverValue.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "ObserverShare", "{0:#,#}");
                lblSumNezamKardan.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "NezamKardanShare", "{0:#,#}");
                lblSumAmount.DataBindings.Add("Text", AccountingDocumentDetailManager.DataTable, "Amount", "{0:#,#}");

            }
            SetPageBreak(false, AccountingDocumentDetailManager.DataTable.Rows.Count);
            ((DetailReportBand)sender).FilterString = string.Format("[ProjectId] = {0}", CurrentProjectId);
        }

        private void DetailReportObserver_AfterPrint(object sender, EventArgs e)
        {
            // SetPageBreak(false);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetPageBreak(true, 1);
        }

        public XRSummary SetSummary(DevExpress.XtraReports.UI.SummaryFunc SummaryFunc)
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            xrSummary1.Func = SummaryFunc;
            xrSummary1.FormatString = "{0:#,#}";
            return xrSummary1;

        }

        private void SetRowCount(int TableRowCount)
        {
            RowCount += TableRowCount;
        }

        private void SetPageBreak(Boolean IsPojectPageBreak, int TableRowCount)
        {
            DetailReportPageBreakObservers.Visible = false;
            DetailReportPageBreakProject.Visible = false;
            if (RowCount < 15 && RowCount + TableRowCount >= 18)  // if (RowCount >= 15)
            {
                if (IsPojectPageBreak && RowCount + TableRowCount > 20)
                    DetailReportPageBreakProject.Visible = true;
                else
                    DetailReportPageBreakObservers.Visible = true;
                RowCount = 0;
            }
            RowCount += TableRowCount;
        }

    }
}
