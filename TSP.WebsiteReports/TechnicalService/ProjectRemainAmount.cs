using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectRemainAmount : DevExpress.XtraReports.UI.XtraReport
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
        #endregion

        public ProjectRemainAmount(int AccDocId)
        {
            InitializeComponent();
            AccDocIdPage = AccDocId;

            this.DataSource = null;
            this.DataMember = null;

            lblSumRemain.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);

            lblPrintDate.Text = "تاریخ صدور:" + TSP.DataManager.Utility.GetDateOfToday();

            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new DataManager.TechnicalServices.AccountingDocumentDetailManager();
            TSP.DataManager.TechnicalServices.AccountingDocumentManager AccountingDocumentManager = new DataManager.TechnicalServices.AccountingDocumentManager();
            AccountingDocumentManager.FindByCode(AccDocId);
            if (AccountingDocumentManager.Count != 1)
                return;
            if (Convert.ToInt32(AccountingDocumentManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingdocumentStatus.SendReportToAccountingUnit && AccountingDocumentManager[0]["ListDate"] != null)
                lblTitle.Text = "گزارش مانده واریزی پروژه ها مورخ " + AccountingDocumentManager[0]["ListDate"].ToString();

            DataTable dtObserverProjects = AccountingDocumentDetailManager.SelectAccountingDocumentDetailProjectsList(AccDocId);

            this.DataSource = dtObserverProjects;
            this.DataMember = dtObserverProjects.TableName;
            if (dtObserverProjects.Rows.Count > 0)
            {
                TRowOwnere.DataBindings.Add("Text", dtObserverProjects, "OwnerName");
                TRowRegisteredNo.DataBindings.Add("Text", dtObserverProjects, "RegisteredNo");
                TRowProjectId.DataBindings.Add("Text", dtObserverProjects, "ProjectId");
                TRowRemainValue.DataBindings.Add("Text", dtObserverProjects, "RemainAmount", "{0:#,#}");
                
                this.DataSource = dtObserverProjects;
                lblSumRemain.DataBindings.Add("Text", dtObserverProjects, "RemainAmount", "{0:#,#}");
            }

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
    }
}
