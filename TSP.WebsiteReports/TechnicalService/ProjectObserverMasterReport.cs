using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectObserverMasterReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region Properties
        private string _FromDateAccountingPage;
        private string FromDateAccountingPage
        {
            get
            {
                return _FromDateAccountingPage;
            }
            set
            {
                _FromDateAccountingPage = value;
            }
        }
        private string _ToDateAccountingPage;
        private string ToDateAccountingPage
        {

            get
            {
                return _ToDateAccountingPage;
            }
            set
            {
                _ToDateAccountingPage = value;
            }
        }
        private string _FromDatePage;
        private string FromDatePage
        {

            get
            {
                return _FromDatePage;
            }
            set
            {
                _FromDatePage = value;
            }
        }
        private string _ToDatePage;
        private string ToDatePage
        {

            get
            {
                return _ToDatePage;
            }
            set
            {
                _ToDatePage = value;
            }
        }
        private string _FromDateDecreasedPage;
        private string FromDateDecreasedPage
        {

            get
            {
                return _FromDateDecreasedPage;
            }
            set
            {
                _FromDateDecreasedPage = value;
            }
        }
        private string _ToDateDecreasedPage;
        private string ToDateDecreasedPage
        {

            get
            {
                return _ToDateDecreasedPage;
            }
            set
            {
                _ToDateDecreasedPage = value;
            }
        }
        private int _ProjectStatusIdPage;
        private int ProjectStatusIdPage
        {
            get
            {
                return _ProjectStatusIdPage;
            }
            set
            {
                _ProjectStatusIdPage = value;
            }
        }
        private string _FromDateBuildingsLicensesPage;
        private string FromDateBuildingsLicensesPage
        {
            get
            {
                return _FromDateBuildingsLicensesPage;
            }
            set
            {
                _FromDateBuildingsLicensesPage = value;
            }
        }

        private string _ToDateBuildingsLicensesPage;
        private string ToDateBuildingsLicensesPage
        {
            get
            {
                return _ToDateBuildingsLicensesPage;
            }
            set
            {
                _ToDateBuildingsLicensesPage = value;
            }
        }
        private int _projectIdPage;
        private int projectIdPage
        {
            get
            {
                return _projectIdPage;
            }
            set
            {
                _projectIdPage = value;
            }
        }
        private string _RegisteredNoPage;
        private string RegisteredNoPage
        {
            get
            {
                return _RegisteredNoPage;
            }
            set
            {
                _RegisteredNoPage = value;
            }
        }
        private string _ToDateObsPayedPage;
        private string ToDateObsPayedPage
        {
            get
            {
                return _ToDateObsPayedPage;
            }
            set
            {
                _ToDateObsPayedPage = value;
            }
        }
        private string _FromDateObsPayedPage;
        private string FromDateObsPayedPage
        {
            get
            {
                return _FromDateObsPayedPage;
            }
            set
            {
                _FromDateObsPayedPage = value;
            }
        }
        private int _MeIdPage;
        private int MeIdPage
        {
            get
            {
                return _MeIdPage;
            }
            set
            {
                _MeIdPage = value;
            }
        }
        private int _IsPayedPage;
        private int IsPayedPage
        {
            get
            {
                return _IsPayedPage; ;
            }
            set
            {
                _IsPayedPage = value;
            }
        }
        #endregion

        double totalUnits = 0;
        double pack = 15;
        //static double totalInsuranceAndNezamShare = 0;
        double totalInsurance = 0;
        double totalNezamShare = 0;
        public ProjectObserverMasterReport(string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
            , int ProjectStatusId, string FromDateBuildingsLicenses, string ToDateBuildingsLicenses, int projectId, string RegisteredNo, string ToDateObsPayed, string FromDateObsPayed
            , int MeId, int IsPayed)
        {

            InitializeComponent();
            FromDateAccountingPage = FromDateAccounting;
            ToDateAccountingPage = ToDateAccounting;
            FromDatePage = FromDate; ToDatePage = ToDate;
            FromDateDecreasedPage = FromDateDecreased; ToDateDecreasedPage = ToDateDecreased;
            ProjectStatusIdPage = ProjectStatusId;
            FromDateBuildingsLicensesPage = FromDateBuildingsLicenses;
            ToDateBuildingsLicensesPage = ToDateBuildingsLicenses;
            projectIdPage = projectId;
            RegisteredNoPage = RegisteredNo;
            ToDateObsPayedPage = ToDateObsPayed;
            FromDateObsPayedPage = FromDateObsPayed;
            MeIdPage = MeId;
            IsPayedPage = IsPayed;


            this.DataSource = null;
            this.DataMember = null;
            lblSumObserverValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumInsuranceShareValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblSumNezamShareValue.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            //lblSumNezamAndInsurance.Summary = SetSummary(DevExpress.XtraReports.UI.SummaryFunc.Sum);
            lblPrintDate.Text = "تاریخ صدور:" + TSP.DataManager.Utility.GetDateOfToday();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtObserverProjects = Project_ObserversManager.SelectProjectObserverProjectsList(FromDate, ToDate, FromDateDecreased, ToDateDecreased, FromDateAccounting, ToDateAccounting
                , FromDateBuildingsLicenses, ToDateBuildingsLicenses, ProjectStatusId, 1, projectId, RegisteredNo
                , ToDateObsPayed, FromDateObsPayed, MeId, IsPayed);

            TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new DataManager.TechnicalServices.ProjectManager();

            this.DataSource = dtObserverProjects;
            this.DataMember = ProjectManager.DataTable.TableName;
            if (dtObserverProjects.Rows.Count > 0)
            {
                TRowOwnere.DataBindings.Add("Text", dtObserverProjects, "OwnerName");
                TRowRegisteredNo.DataBindings.Add("Text", dtObserverProjects, "RegisteredNo");
                TRowProjectId.DataBindings.Add("Text", dtObserverProjects, "ProjectId");
                this.DataSource = dtObserverProjects;
                lblTitle.Text = "لیست پرداخت حق الزحمه نظارت پرونده های تکمیلی";
                if (!string.IsNullOrEmpty(FromDateObsPayed) && FromDateObsPayed != "1")
                    lblTitle.Text += " از تاریخ " + FromDateObsPayed;
                if (!string.IsNullOrEmpty(ToDateObsPayed) && ToDateObsPayed != "2")
                    lblTitle.Text += " تا تاریخ " + ToDateObsPayed;
                lblSumInsuranceShareValue.DataBindings.Add(new XRBinding("Text", dtObserverProjects, "InsuranceShare", "مجموع مبلغ:{0:#,#}"));
                lblSumNezamShareValue.DataBindings.Add(new XRBinding("Text", dtObserverProjects, "NezamShare", "مجموع مبلغ:{0:#,#}"));
                lblSumObserverValue.DataBindings.Add(new XRBinding("Text", dtObserverProjects, "ObserverShare", "مجموع مبلغ:{0:#,#}"));
            }

        }

        private void DetailReportFish_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int CurrrentProjectId = Convert.ToInt32(this.GetCurrentColumnValue("ProjectId"));
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new DataManager.TechnicalServices.AccountingManager();
            //DataTable dtAcc = AccountingManager.SelectAccountingForProject(CurrrentProjectId, -1, (int)TSP.DataManager.TSProjectIngridientType.Observer,FromDateAccountingPage,ToDateAccountingPage);

            //TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtAcc = AccountingManager.SelectAccountingForObserverReport(FromDatePage, ToDatePage, FromDateDecreasedPage, ToDateDecreasedPage, FromDateAccountingPage, ToDateAccountingPage, FromDateBuildingsLicensesPage
                , ToDateBuildingsLicensesPage, ProjectStatusIdPage, 1, CurrrentProjectId, RegisteredNoPage, ToDateObsPayedPage, FromDateObsPayedPage, MeIdPage, IsPayedPage, -1);
            DetailReportFish.DataSource = dtAcc;
            DetailReportFish.DataMember = AccountingManager.DataTable.TableName;
            if (dtAcc.Rows.Count > 0)
            {
                TRowFishNumber.DataBindings.Add("Text", dtAcc, "Number");
                TRowFishDate.DataBindings.Add("Text", dtAcc, "AccDate");
                TRowFishAmount.DataBindings.Add("Text", dtAcc, "Amount", "{0:#,#}");
                TRowAccTypeName.DataBindings.Add("Text", dtAcc, "AccTypeName");
                TRowAccCode.DataBindings.Add("Text", dtAcc, "AccCode");
                ////lblSumObservarShare.DataBindings.Add("Text", dtAcc, "SumObserverInsurancePrice");
            }
            ((DetailReportBand)sender).FilterString = string.Format("[ProjectId] = {0}", CurrrentProjectId);

        }

        private void DetailReportObserver_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int CurrentAccountingId = Convert.ToInt32(DetailReportFish.GetCurrentColumnValue("AccountingId"));
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtObserver = Project_ObserversManager.SelectProjectObserverWageReport(FromDatePage, ToDatePage, FromDateDecreasedPage, ToDateDecreasedPage, FromDateAccountingPage, ToDateAccountingPage, FromDateBuildingsLicensesPage
                , ToDateBuildingsLicensesPage, ProjectStatusIdPage, 1, projectIdPage, RegisteredNoPage, ToDateObsPayedPage, FromDateObsPayedPage, MeIdPage, IsPayedPage, CurrentAccountingId);
            DetailReportObserver.DataSource = dtObserver;
            DetailReportObserver.DataMember = Project_ObserversManager.DataTable.TableName;
            if (dtObserver.Rows.Count > 0)
            {
                TRowBankAccNo.DataBindings.Add("Text", dtObserver, "BankAccNo");
                TRowMeId.DataBindings.Add("Text", dtObserver, "MeOfficeOthPEngOId");
                TRowNezamPrice.DataBindings.Add("Text", dtObserver, "NezamPrice", "{0:#,#}");
                TRowObserverInsurancePrice.DataBindings.Add("Text", dtObserver, "ObserverInsurancePrice", "{0:#,#}");
                TRowObserverName.DataBindings.Add("Text", dtObserver, "ObserverName");
                TRowObserverWagePrice.DataBindings.Add("Text", dtObserver, "ObserverWagePrice", "{0:#,#}");

                lblSumInsuranceShareValue.DataBindings.Add("Text", dtObserver, "InsuranceShare");
                lblSumNezamShareValue.DataBindings.Add("Text", dtObserver, "NezamShare");
                lblSumObserverValue.DataBindings.Add("Text", dtObserver, "ObserverShare");
            }
            ((DetailReportBand)sender).FilterString = string.Format("[AccountingId] = {0}", CurrentAccountingId);

        }

        public XRSummary SetSummary(DevExpress.XtraReports.UI.SummaryFunc SummaryFunc)
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            xrSummary1.Func = SummaryFunc;

            return xrSummary1;

        }

        //private void lblSumInsuranceShareValue_SummaryCalculated(object sender, TextFormatEventArgs e)
        //{
        //    if (e.Value != null)
        //        totalInsurance += Convert.ToDouble(e.Value);
        //    //lblSumNezamAndInsurance.Text = totalInsuranceAndNezamShare.ToString();
        //}

        //private void lblSumNezamShareValue_SummaryCalculated(object sender, TextFormatEventArgs e)
        //{
        //    if (e.Value != null)

        //        totalNezamShare += Convert.ToDouble(e.Value);
        //    //lblSumNezamAndInsurance.Text = totalInsuranceAndNezamShare.ToString();
        //}

        private void lblSumNezamAndInsurance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double summary1 = Convert.ToInt32(lblSumInsuranceShareValue.Summary.GetResult());
            double summary2 = Convert.ToInt32(lblSumNezamShareValue.Summary.GetResult());
            lblSumNezamAndInsurance.Text = (summary1 + summary2).ToString();
         
        }

        private void lblSumNezamAndObserver_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblSumNezamAndObserver.Text = ( Convert.ToInt32(lblSumNezamShareValue.Summary.GetResult())
                +  Convert.ToInt32(lblSumObserverValue.Summary.GetResult())
                +  Convert.ToInt32(lblSumInsuranceShareValue.Summary.GetResult())).ToString();
        }

        private void lblSumNezamAndObserver1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblSumNezamAndObserver1.Text = (Convert.ToInt32(lblSumNezamShareValue.Summary.GetResult())
                + Convert.ToInt32(lblSumObserverValue.Summary.GetResult())
                + Convert.ToInt32(lblSumInsuranceShareValue.Summary.GetResult())).ToString(); 
        }
    }
}
