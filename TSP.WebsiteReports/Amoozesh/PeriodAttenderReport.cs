using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TSP.WebsiteReports.Amoozesh
{
    public partial class PeriodAttenderReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PeriodAttenderReport(int PPId)
        {
            InitializeComponent();
            lblEndDate.Text = lblHours.Text = lblInsName.Text = lblInsNameManager.Text = lblPeriodName.Text = lblStartDate.Text = lblTeacher.Text =
                lblTestDate.Text = "";
            TSP.DataManager.PeriodPresentManager PeriodPresentManager = new DataManager.PeriodPresentManager();
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count <= 0) return;
            lblPeriodName.Text = PeriodPresentManager[0]["CrsName"].ToString();
            lblInsName.Text = PeriodPresentManager[0]["InsName"].ToString();
            lblInsNameManager.Text = "ریاست " + PeriodPresentManager[0]["InsName"].ToString();
            lblStartDate.Text = PeriodPresentManager[0]["StartDate"].ToString();
            lblEndDate.Text = PeriodPresentManager[0]["EndDate"].ToString();
            lblTestDate.Text = PeriodPresentManager[0]["TestDate"].ToString();
            lblHours.Text = PeriodPresentManager[0]["Duration"].ToString();

            TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
            MarkManager.SelectPeriodMarks(PPId);
            if (MarkManager.Count > 0)
            {
                xrLblTotalMark.Text = MarkManager[0]["TotalMark"].ToString();

            }
            TSP.DataManager.TrainingTeachersManager TrainingTeachersManager = new DataManager.TrainingTeachersManager();
            DataTable dtTe = TrainingTeachersManager.spReportPeriodTeachers(PPId);
            for (int i = 0; i < dtTe.Rows.Count; i++)
            {
                lblTeacher.Text += dtTe.Rows[i]["TeName"].ToString() + " ";
            }
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new DataManager.PeriodRegisterManager();
            DataTable dt = PeriodRegisterManager.SelectCourseDocReport(PPId, -1);
            if (dt.Rows.Count <= 0) return;
            xrTRowNo.DataBindings.Add("Text", dt, "RowNo");
            xrTMeName.DataBindings.Add("Text", dt, "MeName");
            xrTMeId.DataBindings.Add("Text", dt, "MeId");
            xrTTPresent.DataBindings.Add("Text", dt, "TotalTimePresent");
            xrTMark.DataBindings.Add("Text", dt, "Mark");
            xrTMarkStatus.DataBindings.Add("Text", dt, "teststatusNamePreview");
            xrTMarkStatus.DataBindings.Add("NavigateUrl", dt, "PreviewURL");
            this.DataSource = dt;

        }

    }
}

