using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ObserverPermitted : DevExpress.XtraReports.UI.XtraReport
    {
        public ObserverPermitted(int ProjectId, int AccountingId,int HasText)
        {
            InitializeComponent();
            LoadData(ProjectId, AccountingId, HasText);           
        }

        public ObserverPermitted(int ProjectId, int AccountingId)
        {
            InitializeComponent();
            LoadData(ProjectId, AccountingId, 0);
        }

        private void LoadData(int ProjectId, int AccountingId, int HasText)
        {
            try
            {
                if (HasText == 1)
                    lblTextNazer2.Visible = true;
                TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new DataManager.TechnicalServices.ProjectManager();
                TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
                TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();

                TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                int PrjReId = -2;
                ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, -1);
                if (ProjectRequestManager.Count <= 0)
                    return;
                PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
                txtPrjId.Text = "کد پروژه:" + ProjectId.ToString();
                DataTable dtProject = ProjectRequestManager.GetProjectInfo(PrjReId);

                if (dtProject.Rows.Count != 1) return;
                lblRegisteredNo.Text = dtProject.Rows[0]["RegisteredNo"].ToString();
                lblMunName.Text = dtProject.Rows[0]["MunName"].ToString();
                lblOwnerName.Text = dtProject.Rows[0]["OwnerName"].ToString();
                lblFondation.Text = dtProject.Rows[0]["Foundation"].ToString();
                //lblStep.Text = BlockManager.GetMaxStageNum(ProjectId).ToString();
                lblStep.Text = BlockManager.GetMaxStageNumByRequest(PrjReId).ToString();

                AccountingManager.FindByAccountingId(AccountingId);
                lblFishNumber.Text = AccountingManager[0]["Number"].ToString();
                lblFishDate.Text = AccountingManager[0]["Date"].ToString();
                int FishType = Convert.ToInt32(AccountingManager[0]["Type"]);
                if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.Fiche) lblFishType.Text = "فیش نقدی";
                else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.Cheque) lblFishType.Text = "چک";
                else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.POS) lblFishType.Text = "دستگاه کارتخوان";
                else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.EPayment) lblFishType.Text = "پرداخت الکترونیکی";

                ObserverList ObserverList = new ObserverList(ProjectId);
                xrSubreportObservers.ReportSource = ObserverList;

                TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
                PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.TsObserverconfirmationLetter);
                if (PrintAssignerSettingManager.Count > 0)
                {
                    //lblGovName.Text = PrintAssignerSettingManager[0]["GmnName"].ToString();
                    lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();                   
                }
            }
            catch (Exception err)
            {

            }
        }

    }
}
