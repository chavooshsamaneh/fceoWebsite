using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Collections;
public partial class Members_TechnicalServices_Project_MemberPlans : System.Web.UI.Page
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
            ObjdsPlans.SelectParameters["DesignerInAcive"].DefaultValue = "0";
            LoadWfHelp();
        }
    }
    protected void btnSubmitDesign_Click(object sender, EventArgs e)
    {
        Response.Redirect("DesignerLogin.aspx");
    }
    protected void btnEditDesign_Click(object sender, EventArgs e)
    {
        int PlansId = -2;
        int PrjReId = -2;
        int ProjectId = -2;
        int DesignerPlansId = -1;
        int PrjDesignerId = -1;
        string Mode = "Edit";
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            if (GridViewPlans.FocusedRowIndex > -1)
            {
                DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
                PlansId = (int)row["PlansId"];
                PrjReId = (int)row["PrjReId"];
                ProjectId = (int)row["ProjectId"];
                DesignerPlansId = (int)row["DesignerPlansId"];
                PrjDesignerId = (int)row["PrjDesignerId"];
            }
        }
        else
        {
            SetLabelWarning("لطفاً برای ویرایش اطلاعات ابتدا یک ردیف را انتخاب نمائید");
        }
        if (!CheckWorkFlowPermissionForEdit(PrjReId))
        {
            SetLabelWarning("در این مرحله از گردش کار پروژه قادر به ویرایش اطلاعات نمی باشید.");
            return;
        }

        if (!CheckPlanWorkFlowPermissionForEdit())
        {
            SetLabelWarning("در این مرحله از گردش کار نقشه قادر به ویرایش اطلاعات نمی باشید.");
            return;
        }

        string QS = "DsPId=" + Utility.EncryptQS(DesignerPlansId.ToString()) +
            "&PgMd=" + Utility.EncryptQS(Mode) +
            "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
            "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
            "&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
            "&PlnId=" + Utility.EncryptQS(PlansId.ToString());


        Response.Redirect("AddPlanDesigner.aspx?" + QS);

    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        int PlansId = -2;
        int PrjReId = -2;
        int ProjectId = -2;
        int DesignerPlansId = -1;
        int PrjDesignerId = -1;
        string Mode = "View";
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            if (GridViewPlans.FocusedRowIndex > -1)
            {
                DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
                PlansId = (int)row["PlansId"];
                PrjReId = (int)row["PrjReId"];
                ProjectId = (int)row["ProjectId"];
                DesignerPlansId = (int)row["DesignerPlansId"];
                PrjDesignerId = (int)row["PrjDesignerId"];
            }
        }
        else
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک ردیف را انتخاب نمائید");
        }

        string QS = "DsPId=" + Utility.EncryptQS(DesignerPlansId.ToString()) +
            "&PgMd=" + Utility.EncryptQS(Mode) +
            "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
            "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +           
            "&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
            "&PlnId=" + Utility.EncryptQS(PlansId.ToString());


        Response.Redirect("AddPlanDesigner.aspx?" + QS);
    }
    protected void GridViewPlanSubRe_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PlansId"] = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (GridViewPlans.FocusedRowIndex <= -1)
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            return;
        }
        focucedIndex = GridViewPlans.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(focucedIndex);
            int PrjReId = (int)row["PrjReId"];
            int PlansId = (int)row["PlansId"];
            int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
            int WfCode = (int)row["WorkFlowCode"];
            WFUserControl.PerformCallback(PlansId, ProjectReTableType, WfCode, e);
            GridViewPlans.ExpandRow(focucedIndex);
        }

    }
    #endregion

    #region Methods

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    private Boolean CheckWorkFlowPermissionForEdit(int PrjReId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        System.Data.DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, PrjReId);
        if (dtWorkFlowLastState.Rows.Count <= 0)
        {
            return false;
        }
        int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
            || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
            || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
            || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
            || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private Boolean CheckPlanWorkFlowPermissionForEdit()
    {
        int PlansId = GetPlanId();        
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        DataTable dtPlan = PlansManager.SelectById(PlansId, -1, -1, (int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
        if (dtPlan.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }
    private int GetPlanId()
    {
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
            int PlansId = (int)row["PlansId"];
            return PlansId;
        }
        else
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        return -2;
    }
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

}