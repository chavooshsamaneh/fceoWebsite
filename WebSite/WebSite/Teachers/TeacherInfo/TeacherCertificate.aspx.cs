using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class Teachers_TeacherInfo_TeacherCertificate : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            int TeId = Utility.GetCurrentUser_MeId();
            ObjdsTeCertificate.SelectParameters[0].DefaultValue = TeId.ToString();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewInsCertificate.FocusedRowIndex > -1)
        {
            DataRow TeCertRow = GridViewInsCertificate.GetDataRow(GridViewInsCertificate.FocusedRowIndex);
            string TcId = TeCertRow["TcId"].ToString();
            Response.Redirect("TeacherBasicInfo.aspx?TcId=" + Utility.EncryptQS(TcId));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Institue/InstitueHome.aspx");
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        int TeId = Utility.GetCurrentUser_MeId();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TeacherCertificateManager);
        try
        {
            TransactionManager.BeginSave();
            TeacherCertificateManager.ClearBeforeFill = true;
            DataTable dtTeCertificate = TeacherCertificateManager.SelectLastVersion(TeId);
            if (dtTeCertificate.Rows.Count > 0)
            {
                int IsConfirm = int.Parse(dtTeCertificate.Rows[0]["IsConfirm"].ToString());
                if (IsConfirm == 0)
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    this.LabelWarning.Text = "وضعیت تایید گواهینامه انتخاب شده نامشخص می باشد.";
                }
                else
                {
                    if (IsConfirm == 1)
                    {
                        string CrtEndDate = dtTeCertificate.Rows[0]["EndDate"].ToString();
                        Utility.Date objDate = new Utility.Date(CrtEndDate);
                        string LastMonth = objDate.AddMonths(-1);
                        string Today = Utility.GetDateOfToday();
                        int IsDocExp = string.Compare(Today, LastMonth);
                        if (IsDocExp > 0)
                        {
                            if (!string.IsNullOrEmpty(dtTeCertificate.Rows[0]["StartDate"].ToString()))
                            {
                                DataRow TeCertificateRow = TeacherCertificateManager.NewRow();
                                TeCertificateRow["Type"] = 1;
                                TeCertificateRow["TeId"] = TeId;
                                TeCertificateRow["FileNo"] = dtTeCertificate.Rows[0]["FileNo"].ToString();
                                TeCertificateRow["SerialNo"] = dtTeCertificate.Rows[0]["SerialNo"].ToString();
                                //InsCertificateRow["StartDate"] = dtTeacherCertificate.Rows[0]["EndDate"].ToString();
                                TeCertificateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                TeCertificateRow["ModifiedDate"] = DateTime.Now;

                                TeacherCertificateManager.AddRow(TeCertificateRow);
                                int cn = TeacherCertificateManager.Save();
                                if (cn > 0)
                                {
                                    int SaveTeInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
                                    WorkFlowTaskManager.FindByTaskCode(SaveTeInfoTaskCode);
                                    if (WorkFlowTaskManager.Count > 0)
                                    {
                                        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                                        int NmcId = FindNmcId();
                                        DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
                                        WorkflowStateRow["TaskId"] = TaskId;
                                        WorkflowStateRow["TableId"] = TeId;
                                        WorkflowStateRow["NmcId"] = NmcId;
                                        WorkflowStateRow["SubOrder"] = 1;
                                        WorkflowStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                        WorkflowStateRow["ModifiedDate"] = DateTime.Now;

                                        WorkFlowStateManager.AddRow(WorkflowStateRow);
                                        int cnt = WorkFlowStateManager.Save();
                                        if (cnt > 0)
                                        {
                                            TransactionManager.EndSave();
                                            DivReport.Visible = true;
                                            this.LabelWarning.Text = "ذخیره انجام شد.";
                                        }
                                        else
                                        {
                                            TransactionManager.CancelSave();
                                            DivReport.Visible = true;
                                            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                        }
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        DivReport.Visible = true;
                                        this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    DivReport.Visible = true;
                                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                DivReport.Visible = true;
                                this.LabelWarning.Text = "تمدید گواهینامه انتخاب شده در دست بررسی می باشد.";
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            DivReport.Visible = true;
                            this.LabelWarning.Text = "تاریخ اعتبار گواهینامه انتخاب شده به پایان نرسیده است.";
                        }
                    }
                    else
                    {
                        DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان تمدید گواهینامه انتخاب شده وجود ندارد.";
                    }
                }
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                this.LabelWarning.Text = "امکان تمدید گواهینامه انتخاب شده وجود ندارد.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void btnChangeCertificate_Click(object sender, EventArgs e)
    {
        int TeId = Utility.GetCurrentUser_MeId();

        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TeacherCertificateManager);
        try
        {
            TransactionManager.BeginSave();
            TeacherCertificateManager.ClearBeforeFill = true;
            DataTable dtTeCertificate = TeacherCertificateManager.SelectLastVersion(TeId);
            if (dtTeCertificate.Rows.Count == 1)
            {
                int IsConfirm = int.Parse(dtTeCertificate.Rows[0]["IsConfirm"].ToString());
                if (IsConfirm == 0)
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    this.LabelWarning.Text = "وضعیت تایید گواهینامه انتخاب شده نامشخص می باشد.";
                }
                else
                {
                    if (IsConfirm == 1)
                    {
                        DataRow TeCertificateRow = TeacherCertificateManager.NewRow();
                        TeCertificateRow["Type"] = 2;
                        TeCertificateRow["TeId"] = TeId;
                        TeCertificateRow["FileNo"] = dtTeCertificate.Rows[0]["FileNo"].ToString();
                        TeCertificateRow["SerialNo"] = dtTeCertificate.Rows[0]["SerialNo"].ToString();
                        TeCertificateRow["StartDate"] = dtTeCertificate.Rows[0]["StartDate"].ToString();
                        TeCertificateRow["EndDate"] = dtTeCertificate.Rows[0]["EndDate"].ToString();
                        TeCertificateRow["UserId"] = Utility.GetCurrentUser_UserId();
                        TeCertificateRow["ModifiedDate"] = DateTime.Now;

                        TeacherCertificateManager.AddRow(TeCertificateRow);
                        int cn = TeacherCertificateManager.Save();
                        if (cn > 0)
                        {
                            int SaveTeInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
                            WorkFlowTaskManager.FindByTaskCode(SaveTeInfoTaskCode);
                            if (WorkFlowTaskManager.Count > 0)
                            {
                                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                                int NmcId = FindNmcId();
                                DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
                                WorkflowStateRow["TaskId"] = TaskId;
                                WorkflowStateRow["TableId"] = TeId;
                                WorkflowStateRow["NmcId"] = NmcId;
                                WorkflowStateRow["SubOrder"] = 1;
                                WorkflowStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                WorkflowStateRow["ModifiedDate"] = DateTime.Now;

                                WorkFlowStateManager.AddRow(WorkflowStateRow);
                                int cnt = WorkFlowStateManager.Save();
                                if (cnt > 0)
                                {
                                    TransactionManager.EndSave();
                                    DivReport.Visible = true;
                                    this.LabelWarning.Text = "ذخیره انجام شد.";
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    DivReport.Visible = true;
                                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                        }
                    }
                    else
                    {
                        DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان تمدید گواهینامه انتخاب شده وجود ندارد.";
                    }
                }
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                this.LabelWarning.Text = "امکان تغییر گواهینامه انتخاب شده وجود ندارد.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    #endregion

    #region Methods

    private int FindNmcId()
    {
        int UserId = (int)Session["Login"];
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }
    protected void GridViewInsCertificate_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";


    }

    protected void GridViewInsCertificate_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

}
