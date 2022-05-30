using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectObserverDetailReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ProjectObserverDetailReport(int CurrrentProjectId)

            //string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
        //, int ProjectStatusId, string FromDateBuildingsLicenses, string ToDateBuildingsLicenses, string RegisteredNo, string ToDateObsPayed, string FromDateObsPayed
        //, int MeId, int IsPayed, int CurrrentProjectId)
        {
            InitializeComponent();
            this.DataSource = null;
            this.DataMember = null;
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new DataManager.TechnicalServices.AccountingManager();
            DataTable dtAcc = AccountingManager.SelectAccountingForProject(CurrrentProjectId, -1, (int)TSP.DataManager.TSProjectIngridientType.Observer);
            //this.DataSource = dtAcc;
            //this.DataMember = AccountingManager.DataTable.TableName;
            //DetailReport.DataSource
            DetailReport.DataSource = dtAcc;
            DetailReport.DataMember = AccountingManager.DataTable.TableName;
            //DetailReport.DataMember = AccountingManager.DataTable.TableName;

            if (dtAcc.Rows.Count > 0)
            {
                TRowFishNumber.DataBindings.Add("Text", dtAcc, "Number");
                TRowFishDate.DataBindings.Add("Text", dtAcc, "AccDate");
                TRowFishAmount.DataBindings.Add("Text", dtAcc, "Amount", "{0:#,#}");
                TRowAccTypeName.DataBindings.Add("Text", dtAcc, "AccTypeName");
                TRowAccCode.DataBindings.Add("Text", dtAcc, "AccCode");
                TRowAccPrjId.DataBindings.Add("Text", dtAcc, "ProjectId");
                //if (this.GetCurrentColumnValue("AccountingId") != null && this.GetCurrentColumnValue("AccountingId").ToString() != "")
                //{
                //    int CurrentAccountingId = Convert.ToInt32(this.GetCurrentColumnValue("AccountingId"));
                //    ProjectObserverDetailObserverListReport ProjectObserverDetailObserverListReport = 
                //        new ProjectObserverDetailObserverListReport(FromDate, ToDate, FromDateDecreased, ToDateDecreased, FromDateAccounting, ToDateAccounting
                //            , FromDateBuildingsLicenses, ToDateBuildingsLicenses, ProjectStatusId, 1, CurrrentProjectId, RegisteredNo
                //           , ToDateObsPayed, FromDateObsPayed, MeId, IsPayed, CurrentAccountingId);
                //xrSubreportObserverListDetail.ReportSource = ProjectObserverDetailObserverListReport;
                //}
            }
        }

        private void DetailObserver_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        //    if (this.GetCurrentColumnValue("AccountingId") != null && this.GetCurrentColumnValue("AccountingId").ToString() != "")
        //    {
        //        int CurrentAccountingId = Convert.ToInt32(this.GetCurrentColumnValue("AccountingId"));
        //        ProjectObserverDetailObserverListReport ProjectObserverDetailObserverListReport =
        //            new ProjectObserverDetailObserverListReport(CurrentAccountingId);
        //        xrSubreportObserverListDetail.ReportSource = ProjectObserverDetailObserverListReport;
        //    }
        }
    }
}
