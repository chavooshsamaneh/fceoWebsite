using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectObserverWage : DevExpress.XtraReports.UI.XtraReport
    {
        public ProjectObserverWage(string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
            , int ProjectStatusId, string FromDateBuildingsLicenses, string ToDateBuildingsLicenses, int projectId, string RegisteredNo, string ToDateObsPayed, string FromDateObsPayed
            ,int  MeId,int IsPayed)
        {
            InitializeComponent();
            lblPrintDate.Text = "تاریخ صدور:" + TSP.DataManager.Utility.GetDateOfToday();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtObserver = Project_ObserversManager.SelectProjectObserverWageReport(FromDate, ToDate, FromDateDecreased, ToDateDecreased, FromDateAccounting, ToDateAccounting, FromDateBuildingsLicenses, ToDateBuildingsLicenses, ProjectStatusId, 1, projectId, RegisteredNo
                , ToDateObsPayed, FromDateObsPayed, MeId, IsPayed);
            if (dtObserver.Rows.Count > 0)
            {
                TRowBankAccNo.DataBindings.Add("Text", dtObserver, "BankAccNo");
                TRowFishAmount.DataBindings.Add("Text", dtObserver, "Amount", "{0:#,#}");
                TRowFishDate.DataBindings.Add("Text", dtObserver, "AccDate");
                TRowFishNumber.DataBindings.Add("Text", dtObserver, "Number");
                TRowMeId.DataBindings.Add("Text", dtObserver, "MeOfficeOthPEngOId");
                TRowNezamPrice.DataBindings.Add("Text", dtObserver, "NezamPrice", "{0:#,#}");
                TRowObserverInsurancePrice.DataBindings.Add("Text", dtObserver, "ObserverInsurancePrice", "{0:#,#}");
                TRowObserverName.DataBindings.Add("Text", dtObserver, "ObserverName");
                TRowObserverWagePrice.DataBindings.Add("Text", dtObserver, "ObserverWagePrice", "{0:#,#}");
                TRowOwnere.DataBindings.Add("Text", dtObserver, "OwnerName");
                TRowRegisteredNo.DataBindings.Add("Text", dtObserver, "RegisteredNo");
                this.DataSource = dtObserver;
                lblTitle.Text = "لیست پرداخت حق الزحمه نظارت پرونده های تکمیلی";
                if (!string.IsNullOrEmpty(FromDateObsPayed) && FromDateObsPayed != "1")
                    lblTitle.Text += " از تاریخ " + FromDateObsPayed;
                if (!string.IsNullOrEmpty(ToDateObsPayed) && ToDateObsPayed != "2")
                    lblTitle.Text += " تا تاریخ " + ToDateObsPayed;

                //xrTPrevRestBed.DataBindings[0].FormatString = "{0:#,#}";
            }
        }

    }
}
