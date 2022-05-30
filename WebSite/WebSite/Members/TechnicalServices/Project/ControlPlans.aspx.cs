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

public partial class Members_TechnicalServices_Project_ControlPlans : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();
            ObjdsPlans.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            ObjdsPlans.SelectParameters["InActiveControler"].DefaultValue = "0";
            LoadWfHelp();
          
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

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            focucedIndex = GridViewPlans.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewPlans.GetDataRow(focucedIndex);
                int PrjReId = (int)row["PrjReId"];
                int PlansId = (int)row["PlansId"];
                int PlanReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
                int WfCode = (int)row["WorkFlowCode"];
                WFUserControl.PerformCallback(PlansId, PlanReTableType, WfCode, e);
                GridViewPlans.ExpandRow(focucedIndex);
            }
        }
        else
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
        }
    }


  
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int PlansId = -1;
        int ProjectId = -1;
        int PrjReId = -1;
        int focucedIndex = -1;

        if (Mode != "New")
        {
            if (GridViewPlans.FocusedRowIndex > -1)
            {
                focucedIndex = GridViewPlans.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewPlans.GetDataRow(focucedIndex);
                    PlansId = (int)row["PlansId"];
                    ProjectId = (int)row["ProjectId"];
                    PrjReId = (int)row["PrjReId"];
                }
            }
        }

        if (PlansId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        if (Mode == "Edit")
        {
            if (!CheckWorkFlowPermissionForEdit(PlansId))
            {
                SetLabelWarning("در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.");
                return;
            }
        }

        string QS = "PlnId=" + Utility.EncryptQS(PlansId.ToString())
            + "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString())
            + "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
            + "&PageMode=" + Utility.EncryptQS(Mode)
            + "&UrlReferrer=" + Utility.EncryptQS("ControlPlans");

        Response.Redirect("PlanInfo.aspx?" + QS);
    }


    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    #region WorkFlowPermission

    /****************************************************************** BtnEdit *************************************************************/
    private bool CheckWorkFlowPermissionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int ControlerConfirmingPlanTask = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan;

                    if (CurrentTaskCode == ControlerConfirmingPlanTask)
                        return true;
                }
            }
        }
        return false;
    }

    //private int GetPlanId()
    //{
    //    if (GridViewPlans.FocusedRowIndex > -1)
    //    {
    //        DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
    //        int PlansId = (int)row["PlansId"];
    //        return PlansId;
    //    }
    //    else
    //        SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
    //    return -2;
    //}

    //private int GetPrjReqId()
    //{
    //    if (GridViewPlans.FocusedRowIndex > -1)
    //    {
    //        DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
    //        int PrjReId = (int)row["PrjReId"];
    //        return PrjReId;
    //    }
    //    else
    //        SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
    //    return -2;
    //}

    //private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    //{
    //    //*****TableId
    //    int PrjReId = GetPrjReqId();

    //    int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

    //    //*******Editing Task Code
    //    int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
    //    int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
    //    int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
    //    int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

    //    TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
    //    TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
    //    TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
    //    TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());


    //    //*****TableId
    //    int PlansId = GetPlanId();

    //    WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

    //    //*******Editing Task Code
    //    int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan;

    //    TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

    //    TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
    //    WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit) && (PerPlan.BtnEdit);
    //    WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave) && (PerPlan.BtnSave);
    //    WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew) && (PerPlan.BtnNew);

    //    return WFPer;
    //}

    //private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForEdit()
    //{
    //    //*****TableId
    //    int PlansId = GetPlanId();

    //    int WFCode = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;

    //    //*******Editing Task Code
    //    int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingNewPlan;

    //    TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, Convert.ToInt32(PlansId), Utility.GetCurrentUser_UserId());

    //    return PerPlan;
    //}

    void LoadWfHelp()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSPlansConfirming));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHep1.DataSource = dt1;
            RepeaterWfHep1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHep2.DataSource = dt2;
            RepeaterWfHep2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHep3.DataSource = dt3;
            RepeaterWfHep3.DataBind();
        }
    }
    #endregion
    #endregion
}
