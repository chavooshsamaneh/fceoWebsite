using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_TechnicalServices_Project_DesignerLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Utility.IsDBNullOrNullValue(txtProjectId.Text))
            {
                SetLabelWarning("کد پروژه را وارد نمایید");
                return;
            }
            TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
            ProjectManager.FindByProjectId(Convert.ToInt32(txtProjectId.Text));
            if (ProjectManager.Count == 0)
            {
                SetLabelWarning("کد پروژه وارد شده معتبر نمی باشد");
                return;
            }
            if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["TraceCode"]) && ProjectManager[0]["TraceCode"].ToString() == txtDesingerCode.Text)
            {
                int ProjectId = Convert.ToInt32(txtProjectId.Text);

                TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 0);
                if (ProjectRequestManager.Count <= 0)
                {
                    SetLabelWarning("امکان ثبت طراح جدید وجود ندارد برای پروژه انتخاب شده درخواست درجریان وجود ندارد");
                    return;
                }
                int PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
                if (!CheckProjectWorkFlowPermissionForDesigners(PrjReId))
                {
                    SetLabelWarning("با توجه به مرحله گردش کار پروژه انتخاب شده امکان ثبت طراح  در این مرحله وجود ندارد.");
                    return;
                }
                int PlansId = -1;
                int DesignerPlansId = -1;
                int PrjDesignerId = -1;
                string PgMd = "New";
                TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
                System.Data.DataTable dtDesingerCurrentMe = Designer_PlansManager.SelectActiveTSDesignerPlansForByProjectId(ProjectId, Utility.GetCurrentUser_MeId(), PrjReId);
                if (dtDesingerCurrentMe.Rows.Count > 0)
                {
                    PrjDesignerId = Convert.ToInt32(dtDesingerCurrentMe.Rows[0]["PrjDesignerId"]);
                    if (Convert.ToInt32(dtDesingerCurrentMe.Rows[0]["PlanConfirm"]) == 0)
                    {
                        PlansId = Convert.ToInt32(dtDesingerCurrentMe.Rows[0]["PlansId"]);
                        DesignerPlansId = Convert.ToInt32(dtDesingerCurrentMe.Rows[0]["DesignerPlansId"]);
                        PgMd = "Edit";// View
                    }
                    if (Convert.ToInt32(dtDesingerCurrentMe.Rows[0]["PlanConfirm"]) != 0)
                    {
                        PlansId = -1;
                        DesignerPlansId = -1;
                        PgMd = "NewPlanReq";
                    }
                    // 
                    //SetLabelWarning("پیش از این نام شما به عنوان طراح در این پروژه ثبت شده است.در صورت نیاز به ویرایش اطلاعات نقشه از طریق منوی مدیریت نقشه ها اقدام به ویرایش اطلاعات نمایید.");
                    //return;
                }
                // int PlanId = -1;
                string QS = "DsPId=" + Utility.EncryptQS(DesignerPlansId.ToString()) +
                "&PgMd=" + Utility.EncryptQS(PgMd) +
                "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
                "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
                "&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
                "&PlnId=" + Utility.EncryptQS(PlansId.ToString());

                Response.Redirect("AddPlanDesigner.aspx?" + QS, false);

            }
            else
            {
                SetLabelWarning("شناسه طراح وارد شده نادرست می باشد");
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Warning;
    }
    //private bool CheckProjectWorkFlowPermissionForDesigners(int PrjReId)
    //{
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

    //    return (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew);
    //}

    private Boolean CheckProjectWorkFlowPermissionForDesigners(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        System.Data.DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, TableId);
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
}