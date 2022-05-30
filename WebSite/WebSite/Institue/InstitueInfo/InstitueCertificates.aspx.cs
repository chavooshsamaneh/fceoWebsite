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

public partial class Institue_InstitueInfo_InstitueCertificates : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            int InsId = Utility.GetCurrentUser_MeId();
            ObjdsInsCertificate.SelectParameters[0].DefaultValue = InsId.ToString();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewInsCertificate.FocusedRowIndex > -1)
        {
            DataRow InsCertRow = GridViewInsCertificate.GetDataRow(GridViewInsCertificate.FocusedRowIndex);
            string InsCId = InsCertRow["InsCId"].ToString();
            Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + Utility.EncryptQS(InsCId));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        int InsId = Utility.GetCurrentUser_MeId();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(InstitueCertificateManager);
        try
        {
            TransactionManager.BeginSave();
            InstitueCertificateManager.ClearBeforeFill = true;
            DataTable dtInsCertificate = InstitueCertificateManager.SelectLastVersion(InsId, -1);
            if (dtInsCertificate.Rows.Count > 0)
            {
                int IsConfirm = int.Parse(dtInsCertificate.Rows[0]["IsConfirmed"].ToString());
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
                        string CrtEndDate = dtInsCertificate.Rows[0]["EndDate"].ToString();
                        Utility.Date objDate = new Utility.Date(CrtEndDate);
                        string LastMonth = objDate.AddMonths(-1);
                        string Today = Utility.GetDateOfToday();
                        int IsDocExp = string.Compare(Today, LastMonth);
                        if (IsDocExp > 0)
                        {
                            if (!string.IsNullOrEmpty(dtInsCertificate.Rows[0]["StartDate"].ToString()))
                            {
                                DataRow InsCertificateRow = InstitueCertificateManager.NewRow();
                                InsCertificateRow["Type"] = 1;
                                InsCertificateRow["InsId"] = InsId;
                                InsCertificateRow["FileNo"] = dtInsCertificate.Rows[0]["FileNo"].ToString();
                                InsCertificateRow["SerialNo"] = dtInsCertificate.Rows[0]["SerialNo"].ToString();
                                //InsCertificateRow["StartDate"] = dtTeacherCertificate.Rows[0]["EndDate"].ToString();
                                InsCertificateRow["UserId"] = (int)Session["Login"];
                                InsCertificateRow["ModifiedDate"] = DateTime.Now;

                                InstitueCertificateManager.AddRow(InsCertificateRow);
                                int cn = InstitueCertificateManager.Save();
                                if (cn > 0)
                                {
                                    int SaveInsInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
                                    WorkFlowTaskManager.FindByTaskCode(SaveInsInfoTaskCode);
                                    if (WorkFlowTaskManager.Count > 0)
                                    {
                                        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                                        int NmcId = FindNmcId();
                                        DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
                                        WorkflowStateRow["TaskId"] = TaskId;
                                        WorkflowStateRow["TableId"] = InsId;
                                        WorkflowStateRow["NmcId"] = NmcId;
                                        WorkflowStateRow["SubOrder"] = 1;
                                        WorkflowStateRow["UserId"] = Session["Login"];
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
        int InsId = Utility.GetCurrentUser_MeId();

        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(InstitueCertificateManager);
        try
        {
            TransactionManager.BeginSave();
            InstitueCertificateManager.ClearBeforeFill = true;
            DataTable dtInsCertificate = InstitueCertificateManager.SelectLastVersion(InsId, -1);
            if (dtInsCertificate.Rows.Count == 1)
            {
                int IsConfirm = int.Parse(dtInsCertificate.Rows[0]["IsConfirmed"].ToString());
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
                        DataRow InsCertificateRow = InstitueCertificateManager.NewRow();
                        InsCertificateRow["Type"] = 2;
                        InsCertificateRow["InsId"] = InsId;
                        InsCertificateRow["FileNo"] = dtInsCertificate.Rows[0]["FileNo"].ToString();
                        InsCertificateRow["SerialNo"] = dtInsCertificate.Rows[0]["SerialNo"].ToString();
                        InsCertificateRow["StartDate"] = dtInsCertificate.Rows[0]["StartDate"].ToString();
                        InsCertificateRow["EndDate"] = dtInsCertificate.Rows[0]["EndDate"].ToString();
                        InsCertificateRow["UserId"] = Session["Login"];
                        InsCertificateRow["ModifiedDate"] = DateTime.Now;

                        InstitueCertificateManager.AddRow(InsCertificateRow);
                        int cn = InstitueCertificateManager.Save();
                        if (cn > 0)
                        {
                            int SaveInsInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
                            WorkFlowTaskManager.FindByTaskCode(SaveInsInfoTaskCode);
                            if (WorkFlowTaskManager.Count > 0)
                            {
                                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                                int NmcId = FindNmcId();
                                DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
                                WorkflowStateRow["TaskId"] = TaskId;
                                WorkflowStateRow["TableId"] = InsId;
                                WorkflowStateRow["NmcId"] = NmcId;
                                WorkflowStateRow["SubOrder"] = 1;
                                WorkflowStateRow["UserId"] = Session["Login"];
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
    #endregion

}
