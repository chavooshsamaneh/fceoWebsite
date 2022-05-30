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

public partial class Employee_WorkFlow_WorkFlowTask : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //  cmbWorkFlow.DataBind();
        // if (cmbWorkFlow.SelectedItem != null && (cmbWorkFlow.SelectedItem.Value != null))
        //      ObjdsWorkFlowTask.SelectParameters["WorkFlowId"].DefaultValue = cmbWorkFlow.SelectedItem.Value.ToString();

        if (!IsPostBack)
        {
            GridViewWorkFlowTask.JSProperties["cpSelectedIndex"] = 0;
            GridViewWorkFlowTask.JSProperties["cpIsReturn"] = 0;
            TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnView.Enabled = btnView2.Enabled = Per.CanView;
            GridViewWorkFlowTask.ClientVisible = Per.CanView;
            btnSearch.Enabled = Per.CanView;
            TSP.DataManager.Permission PerDoer = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnTaskDoer.Enabled = PerDoer.CanNew;
            btnTaskDoer2.Enabled = PerDoer.CanNew;
            GridViewWorkFlowTask.SettingsDetail.ShowDetailRow = PerDoer.CanView;
            cmbNezamChart.Enabled = PerDoer.CanView;
            //cmbWorkFlow.DataBind();
            //cmbWorkFlow.SelectedIndex = 0;

            //if (cmbWorkFlow.SelectedItem.Value != null)
            //    ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = cmbWorkFlow.SelectedItem.Value.ToString();
            GridViewWorkFlowTask.Columns["OrderChange"].Visible = false;
            cmbNezamChart.DataBind();
            cmbNezamChart.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbNezamChart.SelectedIndex = 0;

            cmbWorkFlow.DataBind();
            cmbWorkFlow.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbWorkFlow.SelectedIndex = 0;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnTaskDoer"] = btnTaskDoer.Enabled;
            this.ViewState["btnSearch"] = btnSearch.Enabled;
            this.ViewState["ShowDetailRow"] = GridViewWorkFlowTask.SettingsDetail.ShowDetailRow;

        }

        if (IsPostBack)
        {
            if (cmbWorkFlow.SelectedIndex > 0)
                ObjdsWorkFlowTask.SelectParameters["WorkFlowId"].DefaultValue = cmbWorkFlow.SelectedItem.Value.ToString();
            else
                ObjdsWorkFlowTask.SelectParameters["WorkFlowId"].DefaultValue = "-1";

            if (cmbNezamChart.SelectedIndex > 0)
                ObjdsWorkFlowTask.SelectParameters["NcId"].DefaultValue = cmbNezamChart.SelectedItem.Value.ToString();
            else
                ObjdsWorkFlowTask.SelectParameters["NcId"].DefaultValue = "-1";
            GridViewWorkFlowTask.DataBind();
        }

        SetPageFilter();
        SetGridRowIndex();

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnTaskDoer"] != null)
            this.btnTaskDoer.Enabled = this.btnTaskDoer2.Enabled = (bool)this.ViewState["BtnTaskDoer"];
        if (this.ViewState["btnSearch"] != null)
            this.btnSearch.Enabled = (bool)this.ViewState["btnSearch"];
        if (this.ViewState["ShowDetailRow"] != null)
            GridViewWorkFlowTask.SettingsDetail.ShowDetailRow = (bool)this.ViewState["ShowDetailRow"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        // GridViewWorkFlowTask.DataBind();
    }

    protected void GridViewWorkFlowTask_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditWorkFlowTask(e);
    }

    protected void cmbWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridViewWorkFlowTask_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        //DevExpress.Web.ASPxComboBox cmbNezamChart = (DevExpress.Web.ASPxComboBox)GridViewWorkFlowTask.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewWorkFlowTask.Columns["TaskDoer"], "cmbNezamChart");

        //TaskDoerManager.FindByTaskId(int.Parse(e.EditingKeyValue.ToString()));
        //if (TaskDoerManager.Count > 0)
        //{
        //    int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
        //    cmbNezamChart.DataBind();
        //    cmbNezamChart.SelectedIndex = cmbNezamChart.Items.IndexOfValue(NcId.ToString());
        //}
        //else
        //{
        //    cmbNezamChart.SelectedIndex = 0;
        //}
    }

    protected void btnTaskDoer_Click(object sender, EventArgs e)
    {
        if (GridViewWorkFlowTask.FocusedRowIndex > -1)
        {
            DataRow TaskRow = GridViewWorkFlowTask.GetDataRow(GridViewWorkFlowTask.FocusedRowIndex);
            if (TaskRow != null)
            {
                if (Convert.ToInt32(TaskRow["Type"]) ==(int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask || Convert.ToInt32(TaskRow["Type"]) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    SetMessage("امکان انجام تنظیمات مراحل قابل ارسال برای مراحل پایانی گردش کار وجود ندارد");
                    return;
                }
                string GridFilterString = GridViewWorkFlowTask.FilterExpression;
                string SearchFilterString = GenerateFilterString();
                string TaskId = TaskRow["TaskId"].ToString();
                Response.Redirect("TaskDoer.aspx?TskId=" + Utility.EncryptQS(TaskId) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void GridViewWorkFlowTask_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        DataRow TaskRow = GridViewWorkFlowTask.GetDataRow(GridViewWorkFlowTask.FocusedRowIndex);
        int WorkFlowId = int.Parse(TaskRow["WorkFlowId"].ToString());
        int TaskOreder = int.Parse(e.NewValues["TaskOrder"].ToString());
        DataTable dtTask = WorkFlowTaskManager.SelectByTaskOrder(WorkFlowId, TaskOreder);
        int CurrentTaskId = int.Parse(TaskRow["TaskId"].ToString());
        int TaskId = -1;
        if (dtTask.Rows.Count > 0)
            TaskId = int.Parse(dtTask.Rows[0]["TaskId"].ToString());
        int TaskType = int.Parse(TaskRow["Type"].ToString());
        if ((TaskType == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask) || (TaskType == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask))
        {
            e.RowError = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
            return;
        }
        if ((TaskType == (int)TSP.DataManager.WorkFlowTaskType.StartingTask) && (TaskOreder != 1))
        {
            e.RowError = "اولویت عملیات انتخابی بایستی یک باشد.";
            return;
        }
        if (!(CurrentTaskId == TaskId || TaskId == -1 || TaskOreder == 0))
        {
            WorkFlowTaskManager.FindByCode(int.Parse(e.Keys[0].ToString()));
            if (WorkFlowTaskManager.Count != 1)
            {
                e.RowError = "خطایی در ذخیره انجام گرفته است";
            }
            e.RowError = "اولویت وارد شده تکراری می باشد.";
        }

    }

    protected void GridViewWorkFlowTask_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(WorkFlowTaskManager);
        try
        {
            string[] Parameters = e.Parameters.Split(new char[] { ';' });
            string ButtonId = Parameters[1];
            string VisibleIndex = Parameters[0];
            TransactionManager.BeginSave();
            #region UP
            if (ButtonId == "Up")
            {
                DataRow TaskRow = GridViewWorkFlowTask.GetDataRow(int.Parse(VisibleIndex));
                if (TaskRow != null)
                {
                    int CurrentRowTaskId = int.Parse(TaskRow["TaskId"].ToString());
                    int CurrentRowTaskOrder = int.Parse(TaskRow["TaskOrder"].ToString());
                    int WorkFlowId = int.Parse(TaskRow["WorkFlowId"].ToString());
                    int CurrentTaskType = int.Parse(TaskRow["Type"].ToString());
                    int PrevieceRowTaskOrder = CurrentRowTaskOrder - 1;
                    WorkFlowTaskManager.ClearBeforeFill = true;
                    DataTable dtAllTasks = WorkFlowTaskManager.SelectByWorkId(WorkFlowId, -1);
                    int TaskCount = dtAllTasks.Rows.Count;
                    int LastOrder = int.Parse(dtAllTasks.Rows[TaskCount - 1]["TaskOrder"].ToString());
                    if (CurrentRowTaskOrder != 0 && CurrentRowTaskOrder != 1)// && CurrentRowTaskOrder != LastOrder)
                    {
                        DataTable dtTask = WorkFlowTaskManager.SelectByTaskOrder(WorkFlowId, PrevieceRowTaskOrder);
                        if (dtTask.Rows.Count > 0)
                        {
                            //*******Change Previece Order********
                            int PrevieceRowTaskId = int.Parse(dtTask.Rows[0]["TaskId"].ToString());

                            WorkFlowTaskManager.FindByCode(PrevieceRowTaskId);
                            if (WorkFlowTaskManager.Count > 0)
                            {
                                int NextTaskType = -1;

                                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["Type"]))
                                {
                                    NextTaskType = int.Parse(WorkFlowTaskManager[0]["Type"].ToString());
                                    if (NextTaskType != (int)TSP.DataManager.WorkFlowTaskType.StartingTask && CurrentTaskType == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                                    {
                                        WorkFlowTaskManager[0].BeginEdit();

                                        WorkFlowTaskManager[0]["TaskOrder"] = CurrentRowTaskOrder;
                                        WorkFlowTaskManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                        WorkFlowTaskManager[0]["ModifiedDate"] = DateTime.Now;

                                        WorkFlowTaskManager[0].EndEdit();

                                        int cn = WorkFlowTaskManager.Save();
                                        if (cn > 0)
                                        {
                                            WorkFlowTaskManager.ClearBeforeFill = true;
                                            WorkFlowTaskManager.FindByCode(CurrentRowTaskId);
                                            if (WorkFlowTaskManager.Count > 0)
                                            {
                                                WorkFlowTaskManager[0].BeginEdit();

                                                WorkFlowTaskManager[0]["TaskOrder"] = PrevieceRowTaskOrder;
                                                WorkFlowTaskManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                                WorkFlowTaskManager[0]["ModifiedDate"] = DateTime.Now;

                                                WorkFlowTaskManager[0].EndEdit();

                                                int cnt = WorkFlowTaskManager.Save();
                                                if (cnt > 0)
                                                {
                                                    TransactionManager.EndSave();
                                                    e.Result = "ذخیره انجام شد.";
                                                    GridViewWorkFlowTask.DataBind();
                                                }
                                                else
                                                {
                                                    TransactionManager.CancelSave();
                                                    e.Result = "خطایی در ذخیره صورت گرفته است.";
                                                }
                                            }
                                            else
                                            {
                                                TransactionManager.CancelSave();
                                                e.Result = "خطایی در ذخیره صورت گرفته است.";
                                            }
                                        }
                                        else
                                        {
                                            TransactionManager.CancelSave();
                                            e.Result = "خطایی در ذخیره صورت گرفته است.";
                                        }
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                    }
                }
            }
            #endregion
            else
            #region Down
            {
                DataRow TaskRow = GridViewWorkFlowTask.GetDataRow(int.Parse(VisibleIndex));
                if (TaskRow != null)
                {
                    int CurrentRowTaskId = int.Parse(TaskRow["TaskId"].ToString());
                    int CurrentRowTaskOrder = int.Parse(TaskRow["TaskOrder"].ToString());
                    int CurrentType = int.Parse(TaskRow["Type"].ToString());
                    int WorkFlowId = int.Parse(TaskRow["WorkFlowId"].ToString());
                    int NextRowTaskOrder = CurrentRowTaskOrder + 1;
                    WorkFlowTaskManager.ClearBeforeFill = true;
                    DataTable dtAllTasks = WorkFlowTaskManager.SelectByWorkId(WorkFlowId, -1);
                    int TaskCount = dtAllTasks.Rows.Count;
                    int LastOrder = int.Parse(dtAllTasks.Rows[TaskCount - 1]["TaskOrder"].ToString());
                    DataTable dtTask = WorkFlowTaskManager.SelectByTaskOrder(WorkFlowId, NextRowTaskOrder);
                    if (CurrentRowTaskOrder != 1 && CurrentRowTaskOrder != 0)// && NextRowTaskOrder != LastOrder)
                    {
                        if (dtTask.Rows.Count > 0)
                        {
                            //*******Change Next Order********
                            int NextRowTaskId = int.Parse(dtTask.Rows[0]["TaskId"].ToString());
                            WorkFlowTaskManager.ClearBeforeFill = true;
                            WorkFlowTaskManager.FindByCode(NextRowTaskId);


                            if (WorkFlowTaskManager.Count > 0)
                            {
                                int NextTaskType = int.Parse(WorkFlowTaskManager[0]["Type"].ToString());
                                if (CurrentType == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask && NextTaskType != (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask && NextTaskType != (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                                {
                                    WorkFlowTaskManager[0].BeginEdit();

                                    WorkFlowTaskManager[0]["TaskOrder"] = CurrentRowTaskOrder;
                                    WorkFlowTaskManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                    WorkFlowTaskManager[0]["ModifiedDate"] = DateTime.Now;

                                    WorkFlowTaskManager[0].EndEdit();

                                    int cn = WorkFlowTaskManager.Save();
                                    if (cn > 0)
                                    {
                                        WorkFlowTaskManager.ClearBeforeFill = true;
                                        WorkFlowTaskManager.FindByCode(CurrentRowTaskId);
                                        if (WorkFlowTaskManager.Count > 0)
                                        {
                                            WorkFlowTaskManager[0].BeginEdit();

                                            WorkFlowTaskManager[0]["TaskOrder"] = NextRowTaskOrder;
                                            WorkFlowTaskManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                            WorkFlowTaskManager[0]["ModifiedDate"] = DateTime.Now;

                                            WorkFlowTaskManager[0].EndEdit();

                                            int cnt = WorkFlowTaskManager.Save();
                                            if (cnt > 0)
                                            {
                                                TransactionManager.EndSave();
                                                e.Result = "ذخیره انجام شد.";
                                                GridViewWorkFlowTask.DataBind();
                                            }
                                            else
                                            {
                                                TransactionManager.CancelSave();
                                                e.Result = "خطایی در ذخیره صورت گرفته است.";
                                            }
                                        }
                                        else
                                        {
                                            TransactionManager.CancelSave();
                                            e.Result = "خطایی در ذخیره صورت گرفته است.";
                                        }
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        e.Result = "خطایی در ذخیره صورت گرفته است.";
                                    }
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                            }
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        e.Result = "امکان تغییر اولویت عملیات انتخابی وجود ندارد.";
                    }
                }
            }
            #endregion
            GridViewWorkFlowTask.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    protected void GridViewWorkFlowTask_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        cmbNezamChart.DataBind();
        cmbWorkFlow.DataBind();

        if (cmbWorkFlow.SelectedIndex > -1)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowId"].DefaultValue = cmbWorkFlow.SelectedItem.Value.ToString();
            GridViewWorkFlowTask.Columns["OrderChange"].Visible = true;
        }
        else
            GridViewWorkFlowTask.Columns["OrderChange"].Visible = false;
        if (cmbNezamChart.SelectedIndex > -1)
            ObjdsWorkFlowTask.SelectParameters["NcId"].DefaultValue = cmbNezamChart.SelectedItem.Value.ToString();
        GridViewWorkFlowTask.DataBind();
    }

    protected void GridViewTaskDoer_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TaskId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewWorkFlowTask.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewWorkFlowTask.FocusedRowIndex = Index;
    }

    protected void GridViewWorkFlowTask_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewWorkFlowTask.FocusedRowIndex = e.VisibleIndex;
    }
    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        string GridFilterString = GridViewWorkFlowTask.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        int TaskId = -1;
        int WfId = -1;
        if (GridViewWorkFlowTask.FocusedRowIndex > -1)
        {
            DataRow row = GridViewWorkFlowTask.GetDataRow(GridViewWorkFlowTask.FocusedRowIndex);
            TaskId = (int)row["TaskId"];
            WfId = (int)row["WorkFlowId"];
        }
        if (TaskId == -1 && Mode != "New")
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                TaskId = -1;
            }

            Response.Redirect("WorkFlowTaskInsert.aspx?TskId=" + Utility.EncryptQS(TaskId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&WId=" + Utility.EncryptQS(WfId.ToString())
             + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

        }
    }

    private void EditWorkFlowTask(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        try
        {
            DataRow TaskRow = GridViewWorkFlowTask.GetDataRow(GridViewWorkFlowTask.FocusedRowIndex);

            int WorkFlowId = int.Parse(TaskRow["WorkFlowId"].ToString());
            int TaskOreder = int.Parse(e.NewValues["TaskOrder"].ToString());
            DataTable dtTask = WorkFlowTaskManager.SelectByTaskOrder(WorkFlowId, TaskOreder);
            int CurrentTaskId = int.Parse(TaskRow["TaskId"].ToString());
            int TaskId = -1;
            if (dtTask.Rows.Count > 0)
                TaskId = int.Parse(dtTask.Rows[0]["TaskId"].ToString());
            int TaskType = int.Parse(TaskRow["Type"].ToString());
            if ((TaskType == (int)TSP.DataManager.WorkFlowTaskType.StartingTask) && (TaskOreder == 1))
            {
                SetMessage("اولویت عملیات انتخابی بایستی یک باشد.");
            }
            else
            {
                if (CurrentTaskId == TaskId || TaskId == -1 || TaskOreder == 0)
                {
                    WorkFlowTaskManager.FindByCode(int.Parse(e.Keys[0].ToString()));
                    if (WorkFlowTaskManager.Count == 1)
                    {
                        WorkFlowTaskManager[0].BeginEdit();

                        WorkFlowTaskManager[0]["TaskOrder"] = TaskOreder;

                        WorkFlowTaskManager[0].EndEdit();

                        int cn = WorkFlowTaskManager.Save();
                        if (cn > 0)
                        {
                            SetMessage( "ذخیره انجام شد");
                        }
                        else
                        {
                            SetMessage("خطایی در ذخیره انجام گرفته است");
                        }
                    }
                    else
                    {
                        SetMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
                else
                {
                    SetMessage("اولویت وارد شده تکراری می باشد.");
                }
            }
            GridViewWorkFlowTask.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewWorkFlowTask.CancelEdit();
            SetError(err);
        }
    }

    private void InserTaskDoer(int TaskId, int NcId, TSP.DataManager.TransactionManager TransactionManager, TSP.DataManager.TaskDoerManager TaskDoerManager)
    {
        DataRow TaskDoerRow = TaskDoerManager.NewRow();
        TaskDoerRow["TaskId"] = TaskId;
        TaskDoerRow["NcId"] = NcId;
        TaskDoerRow["Description"] = "";
        TaskDoerRow["UserId"] = Utility.GetCurrentUser_UserId();
        TaskDoerRow["ModifiedDate"] = DateTime.Now;
        TaskDoerRow["DoerOrder"] = 0;

        TaskDoerManager.AddRow(TaskDoerRow);
        int cn = TaskDoerManager.Save();
        if (cn < 0)
        {
            TransactionManager.CancelSave();
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return;
        }
    }

    private void EditTaskDoer(int DoerId, int TaskId, int NcId, TSP.DataManager.TransactionManager TransactionManager, TSP.DataManager.TaskDoerManager TaskDoerManager)
    {
        TaskDoerManager.FindByCode(DoerId);
        if (TaskDoerManager.Count > 0)
        {
            TaskDoerManager[0].BeginEdit();

            TaskDoerManager[0]["TaskId"] = TaskId;
            TaskDoerManager[0]["NcId"] = NcId;
            TaskDoerManager[0]["Description"] = "";
            TaskDoerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TaskDoerManager[0]["DoerOrder"] = 0;
            TaskDoerManager[0]["ModifiedDate"] = DateTime.Now;

            TaskDoerManager[0].EndEdit();

            int cn = TaskDoerManager.Save();
            if (cn < 0)
            {
                TransactionManager.CancelSave();
                return;
            }
        }
        else
        {
            TransactionManager.CancelSave();
            return;
        }

    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetMessage("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                SetMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage("خطایی در حذف انجام گرفته است");
            }
        }
        else
        {
            SetMessage("خطایی در حذف انجام گرفته است");
        }
    }
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(SrchFlt))
                {
                    FilterObjdsByValue(SrchFlt);
                    GridViewWorkFlowTask.Columns["OrderChange"].Visible = true;
                }
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewWorkFlowTask.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {
                string PostId = Utility.DecryptQS(Request.QueryString["PostId"].ToString());
                if (!string.IsNullOrEmpty(PostId))
                {
                    int PostKeyValue = int.Parse(PostId);

                    GridViewWorkFlowTask.DataBind();
                    Index = GridViewWorkFlowTask.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewWorkFlowTask.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewWorkFlowTask.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewWorkFlowTask.JSProperties["cpSelectedIndex"] = Index;
                        GridViewWorkFlowTask.DetailRows.ExpandRow(Index);
                        GridViewWorkFlowTask.FocusedRowIndex = Index;
                        GridViewWorkFlowTask.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsWorkFlowTask.SelectParameters.Count; i++)
        {
            if (ObjdsWorkFlowTask.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsWorkFlowTask.SelectParameters[i].Name + "&";
                FilterString += ObjdsWorkFlowTask.SelectParameters[i].DefaultValue + "&";
            }
        }
        if (FilterString.Length > 0)
            FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjdsWorkFlowTask.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "WorkFlowId":
                        cmbWorkFlow.DataBind();
                        if (Value == "-1")
                        {
                            cmbWorkFlow.DataBind();
                            cmbWorkFlow.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbWorkFlow.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbWorkFlow.DataBind();
                            cmbWorkFlow.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbWorkFlow.SelectedIndex = cmbWorkFlow.Items.FindByValue(Value).Index;
                        }
                        break;

                    case "NcId":
                        cmbNezamChart.DataBind();
                        if (Value == "-1")
                        {
                            cmbNezamChart.DataBind();
                            cmbNezamChart.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbNezamChart.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbNezamChart.DataBind();
                            cmbNezamChart.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            cmbNezamChart.SelectedIndex = cmbNezamChart.Items.FindByValue(Value).Index;
                        }
                        break;
                }
            }
        }
    }
    #endregion
}
