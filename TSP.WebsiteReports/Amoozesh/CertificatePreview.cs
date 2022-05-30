using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace TSP.WebsiteReports.Amoozesh
{
    public partial class CertificatePreview : DevExpress.XtraReports.UI.XtraReport
    {
        public CertificatePreview(int PPId, int MeId)
        {
            InitializeComponent();
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new DataManager.PeriodRegisterManager();
            System.Data.DataTable dtPeriodRegister = PeriodRegisterManager.SelectCourseDocReport(PPId, MeId);
            ClearAllControls();
            if (dtPeriodRegister.Rows.Count <= 0)
                return;
            this.DataSource = dtPeriodRegister;
            SetRowSource(dtPeriodRegister, PPId);


        }
        private void ClearAllControls()
        {
            xrLabelTeachers.Text = "";
            for (int i = 0; i < this.xrTableTeachersFile.Rows.Count; i++)
                for (int j = 0; j < this.xrTableTeachersFile.Rows[i].Cells.Count; j++)
                    xrTableTeachersFile.Rows[i].Cells[j].Text = "";

        }

        public void SetRowSource(System.Data.DataTable dtPeriodRegister, int PPId)
        {
            ClearAllControls();
            int TaskCode = -1;
            int WorkFlowCode = -1;

            if (dtPeriodRegister.Rows[0]["TaskCode"] != null && !string.IsNullOrEmpty(dtPeriodRegister.Rows[0]["TaskCode"].ToString()))
                TaskCode = Convert.ToInt32(dtPeriodRegister.Rows[0]["TaskCode"]);
            if (dtPeriodRegister.Rows[0]["WorkFlowCode"] != null && !string.IsNullOrEmpty(dtPeriodRegister.Rows[0]["WorkFlowCode"].ToString()))
                WorkFlowCode = Convert.ToInt32(dtPeriodRegister.Rows[0]["WorkFlowCode"]);
            ///////
            TSP.DataManager.PeriodPresentManager PeriodPresentManager = new DataManager.PeriodPresentManager();
            System.Data.DataTable PeriodPresentWorkflow = PeriodPresentManager.SelectPeriodPresentWorkflow(PPId);
            int NmcIdEndConfirm = -2; int WfNmcIdRiasat = -2;
            string WfRiasatDate = DataManager.Utility.GetDateOfToday();
            string WFDateEndConfirm = "";
            int HasRiasatNmcd = -2;
            if (PeriodPresentWorkflow.Rows.Count != 0)
            {
                NmcIdEndConfirm = PeriodPresentWorkflow.Rows[0]["NmcIdEndConfirm"] == DBNull.Value ? -2 : Convert.ToInt32(PeriodPresentWorkflow.Rows[0]["NmcIdEndConfirm"]);
                WfNmcIdRiasat = PeriodPresentWorkflow.Rows[0]["NmcIdRaees"] == DBNull.Value ? -2 : Convert.ToInt32(PeriodPresentWorkflow.Rows[0]["NmcIdRaees"]);
                WfRiasatDate = PeriodPresentWorkflow.Rows[0]["WfRiasatDate"] == DBNull.Value ? DataManager.Utility.GetDateOfToday() : PeriodPresentWorkflow.Rows[0]["WfRiasatDate"].ToString();
                WFDateEndConfirm = PeriodPresentWorkflow.Rows[0]["WFDateEndConfirm"] == DBNull.Value ? "" : PeriodPresentWorkflow.Rows[0]["WFDateEndConfirm"].ToString();
                HasRiasatNmcd= PeriodPresentWorkflow.Rows[0]["HasRiasatNmcd"] == DBNull.Value ? -2 : Convert.ToInt32(PeriodPresentWorkflow.Rows[0]["HasRiasatNmcd"]);
            }

            ///////////////
            //**************MemberInfo
            if (HasRiasatNmcd == -2)
            {
                //****SerialNo برای هر دوره از 1 شروع می شود و مقدارهی آن در جدول ثبت نام دورها است
                this.xrLabelLicenceNo.Text ="ق/"+ dtPeriodRegister.Rows[0]["SerialNo"].ToString();           
            }
            else
            {

                this.xrLabelLicenceNo.Text = dtPeriodRegister.Rows[0]["SerialNo"].ToString();
               // this.xrLabelDate.Text = WFDateEndConfirm;
            }
            this.xrLabelDate.Text = dtPeriodRegister.Rows[0]["TestDate"].ToString();     
            this.xrLabelPersonName.Text = dtPeriodRegister.Rows[0]["MeName"].ToString();
            this.xrLabelFather.Text = dtPeriodRegister.Rows[0]["FatherName"].ToString();
            this.xrLabelSSN.Text = dtPeriodRegister.Rows[0]["SSN"].ToString();
            this.xrLabelDocNo.Text = dtPeriodRegister.Rows[0]["FileNo"].ToString();
            this.xrLabelMajor.Text = dtPeriodRegister.Rows[0]["LastMjName"].ToString();

            if (Convert.ToInt32(dtPeriodRegister.Rows[0]["IsMember"]) == 1)
                this.xrLabelGrade.Text = FindGrade(dtPeriodRegister);// GetResGrade(memberView) + "-" + GetMaxGradeName(memberView);
            else if (Convert.ToInt32(dtPeriodRegister.Rows[0]["IsMember"]) == 0)
                this.xrLabelGrade.Text = dtPeriodRegister.Rows[0]["GradName"].ToString();

            this.xrLabelProvince.Text = dtPeriodRegister.Rows[0]["PrName"].ToString();

            this.xrLabelMemberNo.Text = dtPeriodRegister.Rows[0]["MeId"].ToString();
            //****************Course Info
            System.Data.DataTable spReportPeriods = PeriodPresentManager.spReportPeriods(PPId);
            if (spReportPeriods.Rows.Count == 1)
            {
                this.xrLabelCourseName.Text = spReportPeriods.Rows[0]["CrsTitle"].ToString();
                this.xrLabelCourseCode.Text = spReportPeriods.Rows[0]["PPCode"].ToString();
                this.xrLabelFromDate.Text = spReportPeriods.Rows[0]["StartDate"].ToString();
                this.xrLabelToDate.Text = spReportPeriods.Rows[0]["EndDate"].ToString();
                this.xrLabelDuration.Text = spReportPeriods.Rows[0]["Duration"].ToString();
                this.xrLabelOfficeName.Text = spReportPeriods.Rows[0]["InsName"].ToString();
                xrLabelLeraningPermissionNo1.Text =
                //xrLabelLeraningPermissionNo2.Text =
                xrLabelLeraningPermissionNo3.Text =
                this.xrLabelLeraningPermissionNo.Text = "";
                try
                {
                    string[] InsFileNoPart = spReportPeriods.Rows[0]["InsFileNo"].ToString().Split('/');
                    if (InsFileNoPart[0] == "ص")
                    {
                        xrLabelLeraningPermissionNo1.Text = InsFileNoPart[0] + "/";
                        xrLabelLeraningPermissionNo3.Text = InsFileNoPart[2];
                        xrLabelLeraningPermissionNo.Text = "/" + InsFileNoPart[1];
                    }
                    else
                    {
                        this.xrLabelLeraningPermissionNo.Text = spReportPeriods.Rows[0]["InsFileNo"].ToString();
                    }
                }
                catch
                {
                    this.xrLabelLeraningPermissionNo.Text = spReportPeriods.Rows[0]["InsFileNo"].ToString();
                }
                this.xrLabelLearningPermissionDate.Text = spReportPeriods.Rows[0]["InsFileDate"].ToString();
                this.xrLabelGraduateDate.Text = spReportPeriods.Rows[0]["TestDate"].ToString();

            }
            //****************Teachers Info
            System.Data.DataTable dtTeachers = PeriodPresentManager.spReportPeriodTeachers(PPId);
            if (dtTeachers.Rows.Count == 1)
            {
                xrLabelTeachers.Text = dtTeachers.Rows[0]["TeName"].ToString();
                xrTableTeachersFile.Rows[0].Cells[0].Text = dtTeachers.Rows[0]["FileNo"].ToString();
            }
            else
            {
                string TeachersFamily = "";
                for (int i = 0; i < dtTeachers.Rows.Count; i++)
                {
                    TeachersFamily += dtTeachers.Rows[i]["TeFamily"].ToString() + "/";
                    //xrTableTeachersFile.Rows[i].Cells[0].Text = "";
                }
                if (!string.IsNullOrEmpty(TeachersFamily))
                {
                    TeachersFamily = TeachersFamily.Remove(TeachersFamily.Length - 1);
                }
                xrLabelTeachers.Text = TeachersFamily;
            }
            #region  Assigner's Info
            TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new TSP.DataManager.PrintAssignerSettingManager();
            PictureBoxSign1.Visible = PictureBoxSign2.Visible = false;
            this.PictureBoxSign2.ImageUrl = this.PictureBoxSign1.ImageUrl = "";
            this.xrLabelSettelmentManager.Text = this.xrLabelNezamManager.Text = this.xrLabelLearningResponsible.Text =
            this.xrLabelMaskanResponsible.Text = "";



            if (WfNmcIdRiasat == -2 && NmcIdEndConfirm == -2)
                return;
            PrintAssignerSettingManager.FindByPrintTypeIdAndDate((int)TSP.DataManager.PrintType.PrePrintPeriodPrinting, WfRiasatDate);
            if (PrintAssignerSettingManager.Count == 0)
                return;
            #region First Assigner
            if (TaskCode > (int)(TSP.DataManager.WorkFlowTask.PeriodConfirmingByRiasatSazemanAndSign)
                 || WorkFlowCode == (int)(TSP.DataManager.WorkFlows.PrindPeriodCertificates))
            {
                //if (!DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[0]["NmcId"]) && WfNmcIdRiasat == Convert.ToInt32(PrintAssignerSettingManager[0]["NmcId"]))
                if (DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[0]["NmcId"]) || WfNmcIdRiasat == Convert.ToInt32(PrintAssignerSettingManager[0]["NmcId"]))
                {
                    xrLabelLearningResponsible.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
                    this.xrLabelNezamManager.Text = PrintAssignerSettingManager[0]["GmnName"].ToString();
                    if (!string.IsNullOrEmpty(PrintAssignerSettingManager[0]["SignUrl"].ToString()))
                    {
                        PictureBoxSign1.ImageUrl = PrintAssignerSettingManager[0]["SignUrl"].ToString();
                        PictureBoxSign1.Visible = true; xrLabelNezamManagerSign.Visible = false;
                    }
                }
                //else if (DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[0]["NmcId"]) || WfNmcIdRiasat != Convert.ToInt32(PrintAssignerSettingManager[0]["NmcId"]))
                else if (WfNmcIdRiasat != Convert.ToInt32(PrintAssignerSettingManager[0]["NmcId"]))
                {
                    xrLabelLearningResponsible.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
                    this.xrLabelNezamManager.Text = "از طرف " + PrintAssignerSettingManager[0]["GmnName"].ToString();
                    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new DataManager.NezamMemberChartManager();
                    NezamMemberChartManager.FindByNmcId(WfNmcIdRiasat);
                    if (NezamMemberChartManager.Count == 1 && !string.IsNullOrEmpty(NezamMemberChartManager[0]["SignUrl"].ToString()))
                    {
                        PictureBoxSign1.ImageUrl = NezamMemberChartManager[0]["SignUrl"].ToString();
                        PictureBoxSign1.Visible = xrLabelNezamManagerSign.Visible = true;
                        if (NezamMemberChartManager[0]["FullName"] != null)
                        {
                            xrLabelNezamManagerSign.Text = NezamMemberChartManager[0]["FullName"].ToString();
                        }

                    }
                }
            }
            #endregion
            #region Second Assigner
            if (TaskCode == (int)(TSP.DataManager.WorkFlowTask.ConfirmPeriodAndEndProccess)
                || WorkFlowCode == (int)(TSP.DataManager.WorkFlows.PrindPeriodCertificates))
            {
                //if (!DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[1]["NmcId"]) && NmcIdEndConfirm == Convert.ToInt32(PrintAssignerSettingManager[1]["NmcId"]))
                if (DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[1]["NmcId"]) || NmcIdEndConfirm == Convert.ToInt32(PrintAssignerSettingManager[1]["NmcId"]))
                {
                    this.xrLabelMaskanResponsible.Text = PrintAssignerSettingManager[1]["GmtName"].ToString();
                    xrLabelSettelmentManager.Text = PrintAssignerSettingManager[1]["GmnName"].ToString();
                    if (!string.IsNullOrEmpty(PrintAssignerSettingManager[1]["SignUrl"].ToString()))
                    {
                        PictureBoxSign2.ImageUrl = PrintAssignerSettingManager[1]["SignUrl"].ToString();
                        PictureBoxSign2.Visible = true;
                    }
                }
                //else if (DataManager.Utility.IsDBNullOrNullValue(PrintAssignerSettingManager[1]["NmcId"]) || NmcIdEndConfirm != Convert.ToInt32(PrintAssignerSettingManager[1]["NmcId"]))
                else if (NmcIdEndConfirm != Convert.ToInt32(PrintAssignerSettingManager[1]["NmcId"]))
                {
                    this.xrLabelMaskanResponsible.Text = PrintAssignerSettingManager[1]["GmtName"].ToString();
                    xrLabelSettelmentManager.Text = "از طرف " + PrintAssignerSettingManager[1]["GmnName"].ToString();
                    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new DataManager.NezamMemberChartManager();
                    NezamMemberChartManager.FindByNmcId(NmcIdEndConfirm);
                    if (NezamMemberChartManager.Count == 1 && !string.IsNullOrEmpty(NezamMemberChartManager[0]["SignUrl"].ToString()))
                    {
                        PictureBoxSign2.ImageUrl = NezamMemberChartManager[0]["SignUrl"].ToString();
                        PictureBoxSign2.Visible = true;
                        if (NezamMemberChartManager[0]["FullName"] != null)
                        {
                            xrLabelSettelmentManagerSigner.Text = NezamMemberChartManager[0]["FullName"].ToString();
                            xrLabelSettelmentManagerSigner.Visible = true;
                        }
                    }
                }
            }
            else if (TaskCode <= (int)(TSP.DataManager.WorkFlowTask.PeriodConfirmingByRiasatSazemanAndSign))
            {
                this.xrLabelSettelmentManager.Text =
                this.xrLabelNezamManager.Text =
                this.xrLabelLearningResponsible.Text =
                this.xrLabelMaskanResponsible.Text =
                //xrLabelSettelmentManagerSigner.Text= xrLabelNezamManagerSign.Text=
                this.PictureBoxSign2.ImageUrl = this.PictureBoxSign1.ImageUrl = "";
            }
            #endregion
            #endregion
        }

        private string FindGrade(System.Data.DataTable dtPeriodRegister)
        {
            string Res = "";
            Boolean HasPreRes = false;
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["ObsName"])))
            {
                Res += dtPeriodRegister.Rows[0]["ObsName"].ToString() + " نظارت";
                HasPreRes = true;
            }
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["DesName"])))
            {
                HasPreRes = true;
                if (HasPreRes)
                    Res += "-" + dtPeriodRegister.Rows[0]["DesName"].ToString() + " طراحی";
                else
                    Res += dtPeriodRegister.Rows[0]["DesName"].ToString() + " طراحی";
                HasPreRes = true;
            }
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["ImpName"])))
            {
                if (HasPreRes)
                    Res += "-" + dtPeriodRegister.Rows[0]["ImpName"].ToString() + " اجرا";
                else
                    Res += dtPeriodRegister.Rows[0]["ImpName"].ToString() + " اجرا";
                HasPreRes = true;
            }
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["UrbanismName"])))
            {
                if (HasPreRes)
                    Res += "-" + dtPeriodRegister.Rows[0]["UrbanismName"].ToString() + " شهرسازی";
                else
                    Res += dtPeriodRegister.Rows[0]["UrbanismName"].ToString() + " شهرسازی";
            }
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["TrafficName"])))
            {
                if (HasPreRes)
                    Res += "-" + dtPeriodRegister.Rows[0]["TrafficName"].ToString() + " ترافیک";
                else
                    Res += dtPeriodRegister.Rows[0]["TrafficName"].ToString() + " ترافیک";
                HasPreRes = true;
            }
            if (!DataManager.Utility.IsDBNullOrNullValue((dtPeriodRegister.Rows[0]["MappingName"])))
            {
                if (HasPreRes)
                    Res += "-" + dtPeriodRegister.Rows[0]["MappingName"].ToString() + " نقشه برداری";
                else
                    Res += dtPeriodRegister.Rows[0]["MappingName"].ToString() + " نقشه برداری";
            }
            return Res;
        }

    }
}
