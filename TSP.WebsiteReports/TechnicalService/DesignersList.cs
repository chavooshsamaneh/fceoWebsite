using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.TechnicalService
{
    public partial class DesignersList : DevExpress.XtraReports.UI.XtraReport
    {
        public DesignersList(int ProjectId, string Sender, string PlansTypeId,int ProjectReqId)
        {
            InitializeComponent();

            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new DataManager.TechnicalServices.ProjectOfficeMembersManager();
            DataView dvProjOffMember = ProjectOfficeMembersManager.SelectProjectMembersWithCapacity(ProjectId, PlansTypeId, 0, ProjectReqId);

            if (dvProjOffMember.Count > 0)
            {
                xrTRowNum.DataBindings.Add("Text", dvProjOffMember, "RowNumber");
                xrTDesignerName.DataBindings.Add("Text", dvProjOffMember, "MemberName");
                xrTFileNo.DataBindings.Add("Text", dvProjOffMember, "MemberFileNo");
                xrTMajor.DataBindings.Add("Text", dvProjOffMember, "MjName");
                xrTPaye.DataBindings.Add("Text", dvProjOffMember, "DesGrade");

                xrTMunCode.DataBindings.Add("Text", dvProjOffMember, "ArchitectorCode");
                xrTCapacity.DataBindings.Add("Text", dvProjOffMember, "MemberCapacity");

                xrTFishNo.DataBindings.Add("Text", dvProjOffMember,"Number" );
                xrTMeter.DataBindings.Add("Text", dvProjOffMember, "Wage");

                this.DataSource = dvProjOffMember;
            }

            if (Sender == "Des")
            {
                lblCapacity.Visible = false;
                xrTCapacity.Visible = false;

                lblMunCode.Visible = false;
                xrTMunCode.Visible = false;

                lblFishNo.Visible = true;
                xrTFishNo.Visible = true;

                lblMeter.Visible = true;
                xrTMeter.Visible = true;

                xrTable1.DeleteColumn(lblCapacity);
                xrTable2.DeleteColumn(xrTCapacity);         

                lblFileNo.Width = 110;
                xrTFileNo.Width = 110;
               
                xrTable1.DeleteColumn(lblMunCode);
                xrTable2.DeleteColumn(xrTMunCode);         

                lblPaye.Width = 50;
                xrTPaye.Width = 50;

                lblFishNo.Width = 50;
                xrTFishNo.Width = 50;

                lblMeter.Width = 50;
                xrTMeter.Width = 50;
            }

            if (Sender == "Obs")
            {
                lblCapacity.Visible = true;
                xrTCapacity.Visible = true;

                xrTMunCode.Visible = true;
                lblMunCode.Visible = true;

                lblFishNo.Visible = false;
                xrTFishNo.Visible = false;

                lblMeter.Visible = false;
                xrTMeter.Visible = false;

                xrTable1.DeleteColumn(lblFishNo);
                xrTable2.DeleteColumn(xrTFishNo);

                xrTable1.DeleteColumn(lblMeter);
                xrTable2.DeleteColumn(xrTMeter); 


                lblCapacity.Width = 75;
                xrTCapacity.Width = 75;

                lblMunCode.Width = 110;
                xrTMunCode.Width = 110;
            
                lblFileNo.Width = 110;
                xrTFileNo.Width = 110;

                lblPaye.Width = 50;
                xrTPaye.Width = 50;
            }
        }
    }
}
