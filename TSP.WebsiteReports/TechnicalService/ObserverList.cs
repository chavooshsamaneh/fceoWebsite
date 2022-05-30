using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class ObserverList : DevExpress.XtraReports.UI.XtraReport
    {
        public ObserverList(int ProjectId)
        {  
            //xrTCapacity.Styles.Style. = "return ltr_override(event)";
            InitializeComponent();

            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new DataManager.TechnicalServices.Project_ObserversManager();
            DataView dtObs = Project_ObserversManager.SelectProjectObserverReport(ProjectId, (int)TSP.DataManager.TSMemberType.Member);

            if (dtObs.Count > 0)
            {
                xrTRowNum.DataBindings.Add("Text", dtObs, "RowNumber");
                // xrTIsMother.DataBindings.Add("Text", dtObs, "IsMotherName");
                xrTObserverName.DataBindings.Add("Text", dtObs, "Name");
                xrTFileNo.DataBindings.Add("Text", dtObs, "No");
                xrTMajor.DataBindings.Add("Text", dtObs, "MjName");
                xrTPaye.DataBindings.Add("Text", dtObs, "ObsGrdName");
                xrTMunCode.DataBindings.Add("Text", dtObs, "ArchitectorCode");
                xrTCapacity.DataBindings.Add("Text", dtObs, "MemberCapacity");                
                this.DataSource = dtObs;
            }

            #region SelectOfficeEngOffice
            // TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new DataManager.TechnicalServices.Designer_PlansManager();
            // TSP.DataManager.TechnicalServices.ConsultantCompanyManager ConsultantCompanyManager = new DataManager.TechnicalServices.ConsultantCompanyManager();
            //TSP.DataManager.OfficeManager OfficeManager = new DataManager.OfficeManager();
            //TSP.DataManager.EngOfficeManager EngOfficeManager = new DataManager.EngOfficeManager();
            //Designer_PlansManager.FindActivesByProjectId(ProjectId);
            //if (Designer_PlansManager.Count <= 0) return;

            //xrTRowNum.DataBindings.Add("Text", Designer_PlansManager.DataTable, "RowNumber");
            //xrTDesignerName.DataBindings.Add("Text", Designer_PlansManager.DataTable, "DesignerName");
            //xrTMajor.DataBindings.Add("Text", Designer_PlansManager.DataTable, "MjName");
            //xrTFileNo.DataBindings.Add("Text", Designer_PlansManager.DataTable, "FileNo");

            //int OfficeEngOId = Convert.ToInt32(Designer_PlansManager[0]["OfficeEngOId"]);
            //int MemberTypeId = Convert.ToInt32(Designer_PlansManager[0]["MemberTypeId"]);
            //switch (MemberTypeId)
            //{
            //    case (int)TSP.DataManager.TSMemberType.Office:
            //        OfficeManager.FindByCode(OfficeEngOId);
            //        xrTFileNo.DataBindings.Add("Text", OfficeManager.DataTable, "FileNo");
            //        break;
            //    case (int)TSP.DataManager.TSMemberType.EngOffice:
            //        EngOfficeManager.FindByCode(OfficeEngOId);
            //        xrTFileNo.DataBindings.Add("Text", EngOfficeManager.DataTable, "FileNo");
            //        break;
            //    case (int)TSP.DataManager.TSMemberType.ConsultantCompany:
            //        ConsultantCompanyManager.FindByCode(OfficeEngOId);
            //        xrTFileNo.DataBindings.Add("Text", ConsultantCompanyManager.DataTable, "FileNo");
            //        break;
            //}
            // this.DataSource = Designer_PlansManager.DataTable;
            #endregion
        }

    }
}
