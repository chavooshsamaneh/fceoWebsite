using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class DesignerPermitted : DevExpress.XtraReports.UI.XtraReport
    {
        public DesignerPermitted(int ProjectId, string PlansTypeId)
        {
            InitializeComponent();

            TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new DataManager.TechnicalServices.ProjectManager();
            TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            int PrjReId = -2;
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, -1);
            if (ProjectRequestManager.Count <= 0)
                return;
            txtPrjId.Text = "کد پروژه:" + ProjectId.ToString();
            PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            DataTable dtProject = ProjectRequestManager.GetProjectInfo(PrjReId);
            //DataTable dtProject = ProjectManager.GetProjectInfo(ProjectId);
            if (dtProject.Rows.Count != 1) return;
            lblPelak.Text = dtProject.Rows[0]["RegisteredNo"].ToString();
            lblArea.Text = dtProject.Rows[0]["MunName"].ToString();
            lblOwnerName.Text = dtProject.Rows[0]["OwnerName"].ToString();
            lblMetraj.Text = dtProject.Rows[0]["Foundation"].ToString();
            lblStep.Text = BlockManager.GetMaxStageNum(ProjectId).ToString();
         
            DesignersList DesignersList = new DesignersList(ProjectId, "Des", PlansTypeId, PrjReId);
            xrSubreportDesigners.ReportSource = DesignersList;  
            lblDesignerType.Text = PlansTypeName(PlansTypeId);

            TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
            PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.TsDesignerconfirmationLetter);
            if (PrintAssignerSettingManager.Count > 0)
            {
                //lblGovName.Text = PrintAssignerSettingManager[0]["GmnName"].ToString();
                lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();                
            }
        }

        private string PlansTypeName(string PlansTypeId)
        {
            string PlansTypeName = "";
            Boolean IsTasisatAdded = false;
            PlansTypeId = PlansTypeId.Substring(1, PlansTypeId.Length - 2);
            string[] ArrayPlanId = PlansTypeId.Split(',');
            for(int i=0;i<ArrayPlanId.Length;i++)
            {
                switch(int.Parse(ArrayPlanId[i].ToString()))
                {
                    case (int)TSP.DataManager.TSPlansType.Memari:
                        if (!string.IsNullOrEmpty(PlansTypeName))
                            PlansTypeName += " و";
                        PlansTypeName += " معماری";
                        break;
                    case (int)TSP.DataManager.TSPlansType.Sazeh:
                        if (!string.IsNullOrEmpty(PlansTypeName))
                            PlansTypeName += " و";
                        PlansTypeName += " سازه";
                        break;
                    case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                        if (!string.IsNullOrEmpty(PlansTypeName))
                            PlansTypeName += " و";
                        PlansTypeName += " شهرسازی";
                        break;
                    case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                    case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                        if (!IsTasisatAdded)
                        {
                            if (!string.IsNullOrEmpty(PlansTypeName))
                                PlansTypeName += " و";
                            PlansTypeName += " تاسیسات";
                            IsTasisatAdded = true;
                        }
                        break;
                        
                }
            }

            return PlansTypeName;
        }


    }
}
