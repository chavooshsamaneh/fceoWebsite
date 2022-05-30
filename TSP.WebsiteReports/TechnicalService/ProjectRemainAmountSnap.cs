using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectRemainAmountSnap : DevExpress.XtraReports.UI.XtraReport
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

        public ProjectRemainAmountSnap(int AccDocId,int CurrentUserId)
        {
            InitializeComponent();
            AccDocIdPage = AccDocId;

            this.DataSource = null;
            this.DataMember = null;

            lblSum.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);

            lblPrintDate.Text = "تاریخ صدور:" + TSP.DataManager.Utility.GetDateOfToday();

            TSP.DataManager.TechnicalServices.ProjectUserRightManager ProjectUserRightManager = new TSP.DataManager.TechnicalServices.ProjectUserRightManager();

            DataTable dtList = ProjectUserRightManager.SelectByUser(CurrentUserId);
            string MunParentIdList = "(-2)";
            if (dtList.Rows.Count != 0 && dtList.Rows[0]["MunParentIdList"] != null && !string.IsNullOrWhiteSpace( dtList.Rows[0]["MunParentIdList"].ToString()))
            {
               // ULAlarm.Visible = false;
                MunParentIdList = dtList.Rows[0]["MunParentIdList"].ToString();
            }

            TSP.DataManager.TechnicalServices.ProjectManager AccountingDocumentDetailManager = new DataManager.TechnicalServices.ProjectManager();

            DataTable dtProjectsRemainVal = AccountingDocumentDetailManager.SelectTSAccountingDocumentReportRemainValue(AccDocId, MunParentIdList);

            this.DataSource = dtProjectsRemainVal;
            this.DataMember = dtProjectsRemainVal.TableName;
            if (dtProjectsRemainVal.Rows.Count > 0)
            {
                TRowOwnere.DataBindings.Add("Text", dtProjectsRemainVal, "OwnerName");
                TRowRegisteredNo.DataBindings.Add("Text", dtProjectsRemainVal, "RegisteredNo");
                TRowProjectId.DataBindings.Add("Text", dtProjectsRemainVal, "ProjectId");
                TRowSumAmountValue.DataBindings.Add("Text", dtProjectsRemainVal, "SumAmount", "{0:#,#}");
                TRowRemainValue.DataBindings.Add("Text", dtProjectsRemainVal, "SumObserverShare", "{0:#,#}");
                TRowArchive.DataBindings.Add("Text", dtProjectsRemainVal, "ArchiveNo", "{0:#,#}");

                this.DataSource = dtProjectsRemainVal;
                lblSum.DataBindings.Add("Text", dtProjectsRemainVal, "SumAmount", "{0:#,#}");


                this.DataSource = dtProjectsRemainVal;
                lblSumRemain.DataBindings.Add("Text", dtProjectsRemainVal, "SumObserverShare", "{0:#,#}");

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
