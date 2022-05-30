using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Collections;

public partial class Employee_TechnicalServices_Project_PlansChooseControler : System.Web.UI.Page
{

    string ProjectId;
    string PrjReId;
    string PrjPgMode;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();

            TSP.DataManager.Permission perPlan = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnView.Enabled = perPlan.CanView;
            btnView2.Enabled = perPlan.CanView;
            GridViewPlans.Visible = perPlan.CanView;

            GridViewPlans.JSProperties["cpIsPostBack"] = 1;
            Session["SendBackDataTable_EmpPln"] = "";

            SetKeys();
            this.ViewState["btnView"] = btnView.Enabled;
        }
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];
    }

    #region btn Click
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldPlan["PageMode"].ToString());
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (PageMode == "ShowAll")
        {
            Response.Redirect("~/TechnicalServices/TsHome.aspx");
        }
        else
        {
            if (Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString()) == "-1")
                Response.Redirect("Project.aspx");

            string QS = "ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
                + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString()
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("ProjectInsert.aspx?" + QS);
        }
    }
    protected void btnControler_Click(object sender, EventArgs e)
    {
        ControllerNextPage();
    }


    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }

    protected void btnDesigner_Click(object sender, EventArgs e)
    {
        int PlansId = -1;
        if (GridViewPlans.FocusedRowIndex <= -1)
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            return;
        }
        DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
        PlansId = (int)row["PlansId"];

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        if (PlansId == -1)
        {
            SetLabelWarning("جهت مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        string QS = "ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
           + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
           + "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
           + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString()
           + "&PlanPageMode=" + Utility.EncryptQS("View")
           + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("PlanDesigner.aspx?" + QS);

    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HiddenFieldPlan["ProjectId"].ToString() +
                "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString() +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()) +
                "&PageMode=" + HiddenFieldPlan["PageMode"].ToString() +
                "&UrlReferrer=" + Utility.EncryptQS("Plans.aspx") +
                "&PlnPgMd=" + Utility.EncryptQS("View") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccountingDesigner.aspx?" + QS);

    }
    #endregion

    #region Grid
    protected void GridViewPlanSubRe_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PlansId"] = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewPlans_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewPlans.DataBind();
    }    
    #endregion

    #region Callbacks

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
            //if (e.Parameter == "Send")
            //{

            //    //SendDocToNextStep(PlansId, WfCode);
            //    WFUserControl.SendDocToNextStep(PlansId, WfCode, ProjectReTableType);
            //    GridViewPlans.DataBind();
            //}
            //else
            //{
            //    // SelectSendBackTask(ProjectReTableType, WfCode, PlansId);
            //    WFUserControl.SelectSendBackTask(ProjectReTableType, WfCode, PlansId);
            //}
            GridViewPlans.ExpandRow(focucedIndex);
        }

    }
    #endregion

    #endregion

    #region Methods
    /***************************************************************** NextPage**************************************************************/
    private void NextPage(string Mode)
    {
        int PlansId = -1;
        int focucedIndex = -1;

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (Mode != "New")
        {
            if (GridViewPlans.FocusedRowIndex > -1)
            {
                focucedIndex = GridViewPlans.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewPlans.GetDataRow(focucedIndex);
                    PlansId = (int)row["PlansId"];
                }
            }
        }

        if (PlansId == -1 && Mode != "New")
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        else
        {
            if (Mode == "New" && !CheckPermissionForNew())
            {
                SetLabelWarning("با توجه به در جریان بودن درخواست ثبت نقشه امکان ثبت مجدد نقشه وجود ندارد.");
                GridViewPlans.DataBind();
            }
            else
            {
                string QS = "PlnId=" + Utility.EncryptQS(PlansId.ToString())
                    + "&PrjId=" + HiddenFieldPlan["ProjectId"].ToString()
                    + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                    + "&PlnPgMd=" + Utility.EncryptQS(Mode)
                    + "&PrePgMd=" + HiddenFieldPlan["PageMode"].ToString()
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
                Response.Redirect("AddPlans.aspx?" + QS);
            }
        }
    }

    private void EditNextPage()
    {
        if (!CheckWorkFlowPermissionForEdit())
        {
            SetLabelWarning("در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.");
            return;
        }

        NextPage("Edit");
    }

    private void ControllerNextPage()
    {
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
            int PlansId = (int)row["PlansId"];
            int PrjReId = (int)row["PrjReId"];
            //if (CheckWorkFlowPermissionForControler(PrjReId, PlansId))
            //    NextPage("Controler");
            //else
            //    SetLabelWarning("شما قادر به انتخاب بازبین نقشه نمی باشید.");
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();

            string QS = "PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
                + "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            QS = QS + "&PrjId=" + HiddenFieldPlan["ProjectId"].ToString() + "&PrePgMd=" + HiddenFieldPlan["PageMode"].ToString() + "&PlnPgMd=" + Utility.EncryptQS("View");
            Response.Redirect("PlanControlers.aspx?" + QS);
        }
        else
            SetLabelWarning("لطفاً برای انتخاب بازبین نقشه ابتدا یک رکورد را انتخاب نمائید");


    }

    /****************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {          
            int AgentId = -1;
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
            {
                AgentId = Utility.GetCurrentUser_AgentId();
            }
            ObjdsPlans.SelectParameters["ProjectAgentId"].DefaultValue = AgentId.ToString();
            ObjdsPlans.SelectParameters["TaskCode"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan).ToString();
            GridViewPlans.DataBind();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return;
        }
    }
    
    #region Set Warning-Error Messages
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
    
    #endregion

    #region WorkFlow

    private void Tracing()
    {
        int focucedIndex = GridViewPlans.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            int PostId = int.Parse(GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex)["PlansId"].ToString());
            string GridFilterString = GridViewPlans.FilterExpression;

            DataRow row = GridViewPlans.GetDataRow(focucedIndex);
            int Status = Convert.ToInt16(row["Status"]);
            int TableId = Convert.ToInt32(row["PlansId"]);
            int TableType = (int)TSP.DataManager.TableCodes.TSPlans;
            int WorkFlowCode = GetWFCode(Status);


            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                        "&PostId=" + Utility.EncryptQS(PostId.ToString());

            string QS = "&ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
                + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString();

            Url = Url + QS;

            if (IsCallback)
                ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
            else
                Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));

            //  Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            SetLabelWarning("ردیفی انتخاب نشده است.");

        }
    }

    private int GetWFCode(int PlanStatus)
    {
        int WfCodePlan = -2;
        int SavePlanInfoTaskCode = -2;

        //PlansManager.FindByPlansId(PlansId);
        //if (PlansManager.Count == 1)
        //{
        //    PlanStatus = Convert.ToInt32(PlansManager[0]["Status"].ToString());
        //}

        switch (PlanStatus)
        {
            case (int)TSP.DataManager.TSPlanRequestType.ChangePlanAndDesignerBasically:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.PlanRevisingRequest:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlanRevisionConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanNewRevisionInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
                break;
            default:
                SavePlanInfoTaskCode = -2;
                WfCodePlan = -2;
                break;
        }

        return WfCodePlan;
    }

    #region WorkFlowPermission
    

    /****************************************************************** BtnEdit *************************************************************/
    private bool CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        TSP.DataManager.WFPermission PerPlan = CheckPlanWorkFlowPermissionForEdit();

        return (PerProject.BtnEdit || PerPlan.BtnEdit);
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

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    {
        //*****TableId
        PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());


        //*****TableId
        int PlansId = GetPlanId();

        WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit) && (PerPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave) && (PerPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew) && (PerPlan.BtnNew);

        return WFPer;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForEdit()
    {
        //*****TableId
        int PlansId = GetPlanId();

        ArrayList TaskCode = GetPlansTaskCode(PlansId);

        int WFCode = Convert.ToInt32(TaskCode[0]);

        //*******Editing Task Code
        int PlanTaskCode = Convert.ToInt32(TaskCode[1]);

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, Convert.ToInt32(PlansId), Utility.GetCurrentUser_UserId());

        return PerPlan;
    }

    /// <summary>
    /// ArrayList[0]=PlanWfCode, ArrayList[1]=PlanTaskCode
    /// </summary>
    private ArrayList GetPlansTaskCode(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        int PlanStatus = -1;

        int WfCodePlan = -2;
        int SavePlanInfoTaskCode = -2;

        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            PlanStatus = Convert.ToInt32(PlansManager[0]["Status"].ToString());
        }

        switch (PlanStatus)
        {
            case (int)TSP.DataManager.TSPlanRequestType.ChangePlanAndDesignerBasically:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.PlanRevisingRequest:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlanRevisionConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanNewRevisionInfo;
                break;
            //case (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming:
            //    WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
            //    SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
            //    break;
            default:
                SavePlanInfoTaskCode = -2;
                WfCodePlan = -2;
                break;
        }

        ArrayList TaskCode = new ArrayList();
        TaskCode.Add(WfCodePlan);
        TaskCode.Add(SavePlanInfoTaskCode);
        return TaskCode;
    }

    /****************************************************************** BtnDeleteReq ************************************************************/
    private bool CheckWFPermissionForDeleteRequest(int PlansId, int RequestType)
    {
        ArrayList TaskCode = GetPlansWfCode(RequestType);

        int WFCode = Convert.ToInt32(TaskCode[0]);

        //*******Editing Task Code
        int PlanTaskCode = Convert.ToInt32(TaskCode[1]);

        //TSP.DataManager.WorkFlowPermission WFPermission = new TSP.DataManager.WorkFlowPermission();
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(PlansId, WFCode, PlanTaskCode, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
    }

    private ArrayList GetPlansWfCode(int RequestType)
    {
        int WfCodePlan = -2;
        int SavePlanInfoTaskCode = -2;

        switch (RequestType)
        {
            case (int)TSP.DataManager.TSPlanRequestType.ChangePlanAndDesignerBasically:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.PlanRevisingRequest:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlanRevisionConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanNewRevisionInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
                break;
            default:
                SavePlanInfoTaskCode = -2;
                WfCodePlan = -2;
                break;
        }

        ArrayList TaskCode = new ArrayList();
        TaskCode.Add(WfCodePlan);
        TaskCode.Add(SavePlanInfoTaskCode);
        return TaskCode;
    }

    /****************************************************************** BtnController ***********************************************************/
    private bool CheckWorkFlowPermissionForControler(int PrjReId, int PlansId)
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForControler(PrjReId, PlansId);
        //TSP.DataManager.WFPermission PerPlan = CheckPlanWorkFlowPermissionForControler(PlansId);

        return (PerProject.BtnEdit && PerProject.BtnNew);// || (PerPlan.BtnEdit && PerPlan.BtnNew));
    }

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForControler(int PrjReId, int PlansId)
    {
        //*****TableId  PrjReId

        //int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        //int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        //int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        //int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        //int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        //TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        //TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        //TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        //TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());


        //*****TableId  PlansId
        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = PerPlan.BtnEdit;
        WFPer.BtnSave = PerPlan.BtnSave;
        WFPer.BtnNew = PerPlan.BtnNew;

        return WFPer;
    }

    //private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForControler(int PlansId)
    //{
    //    //*****TableId  PlansId

    //    int WFCode = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;

    //    //*******Editing Task Code
    //    int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToNewPlan;

    //    TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

    //    return PerPlan;
    //}

    /********************************************************************************************************************************************/
    private bool CheckPermissionForNew()
    {
        bool Per = false;
        PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        int PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
        int ProjectId = int.Parse(Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString()));
        PlansManager.SelectTSPlansByProjectAndRequest(ProjectId, PlansTypeId, 0,Convert.ToInt32( PrjReId));
        if (PlansManager.Count > 0) return false;

        PlansManager.SelectTSPlansByProjectAndRequest(ProjectId, PlansTypeId, 1, Convert.ToInt32(PrjReId));
        if (PlansManager.Count <= 0) return true;
        for (int i = 0; i < PlansManager.Count; i++)
        {
            int PlansId = Convert.ToInt32(PlansManager[i]["PlansId"]);
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans), PlansId);
            if (dtWfState.Rows.Count == 1)
            {
                if (Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask &&
                   Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    Per = false;
                    break;
                }
                else Per = true;
            }
        }
        return Per;
    }    
    #endregion
    #endregion

    private int GetPlanStatusAndTaskCode(string RequestType, string StatusORTaskCode)
    {
        int PlanStatus = -1;
        int TaskCode = -1;

        switch (RequestType)
        {
            case "ChangePlanAndDesignerBasically":
                PlanStatus = (int)TSP.DataManager.TSPlanRequestType.ChangePlanAndDesignerBasically;
                TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;
                break;

            case "PlanRevisingRequest":
                PlanStatus = (int)TSP.DataManager.TSPlanRequestType.PlanRevisingRequest;
                TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanNewRevisionInfo;
                break;
        }

        switch (StatusORTaskCode)
        {
            case "Status":
                return PlanStatus;

            case "TaskCode":
                return TaskCode;
        }

        return -2;
    }
    #endregion
}