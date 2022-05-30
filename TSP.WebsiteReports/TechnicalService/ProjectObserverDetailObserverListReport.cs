using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ProjectObserverDetailObserverListReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ProjectObserverDetailObserverListReport( int CurrentAccountingId)
            //string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
            //, string FromDateBuildingsLicenses, string ToDateBuildingsLicenses,
            // int ProjectStatusId, int IsillInfo, int projectId, string RegisteredNo
            //, string ToDateObsPayed, string FromDateObsPayed, int MeId, int IsPayed, int CurrentAccountingId)
        {
            InitializeComponent();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtObserver = Project_ObserversManager.SelectProjectObserverWageReport("1", "2", "1", "2", "1", "2", -1, 1, -1, "%","1", "2", -1, -1, CurrentAccountingId);
            this.DataSource = dtObserver;
            this.DataMember = Project_ObserversManager.DataTable.TableName;
            if (dtObserver.Rows.Count > 0)
            {
                TRowBankAccNo.DataBindings.Add("Text", dtObserver, "BankAccNo");
                TRowMeId.DataBindings.Add("Text", dtObserver, "MeOfficeOthPEngOId");
                TRowNezamPrice.DataBindings.Add("Text", dtObserver, "NezamPrice", "{0:#,#}");
                TRowObserverInsurancePrice.DataBindings.Add("Text", dtObserver, "ObserverInsurancePrice", "{0:#,#}");
                TRowObserverName.DataBindings.Add("Text", dtObserver, "ObserverName");
                TRowObserverWagePrice.DataBindings.Add("Text", dtObserver, "ObserverWagePrice", "{0:#,#}");                
                TRowPrjId.DataBindings.Add("Text", dtObserver, "ObserverWagePrice", "{0:#,#}");
            }
        }

    }
}
