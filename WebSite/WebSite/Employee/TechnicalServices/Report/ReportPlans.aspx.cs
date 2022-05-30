using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Data;

public partial class Employee_TechnicalServices_Report_ReportPlans : System.Web.UI.Page
{


    private string _PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldPlan["PageMode"].ToString());
        }
        set
        {
            HiddenFieldPlan["PageMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();

            TSP.DataManager.Permission perPlan = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermissionTSReportPlans(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Enabled= btnExportExcel2.Enabled= GridViewPlans.Visible = perPlan.CanView;

            GridViewPlans.JSProperties["cpIsPostBack"] = 1;
            Session["SendBackDataTable_EmpPln"] = "";

            SetKeys();
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";

        script += "if ( txtCreateDateTo=='' && txtCreateDateFrom=='' && ComboAgent.GetSelectedIndex() == 0 && CmbTask.GetSelectedIndex() == 0 ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                   ComboAgent.SetSelectedIndex(0);
                   CmbTask.SetSelectedIndex(0);
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = ''; document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Search", script);
    }

    #region btn Click

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewPlans.DataBind();
        GridViewExporter.FileName = "Report";
        GridViewExporter.WriteXlsToResponse(true);
    }

    #endregion

    #region Grid

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

    private void SetKeys()
    {
        try
        {
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                ComboAgent.ClientEnabled = true;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = 0;
            }
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
            if (!Utility.IsDBNullOrNullValue(Request.QueryString["PgMd"]))
            {
                _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
                if (_PageMode == "Confirmed")
                {
                    Utility.Date objDate = new Utility.Date(Utility.GetDateOfToday());
                    string LastMonth = objDate.AddDays(-30);
                    txtCreateDateFrom.Text = LastMonth;
                    txtCreateDateTo.Text = Utility.GetDateOfToday();

                    CmbTask.SelectedIndex = CmbTask.Items.FindByValue(((int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess).ToString()).Index;
                }
            }
            Search();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return;
        }
    }

    private void Search()
    {
        try
        {
            //int AgentId = -1;
            //if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
            //{
            //    AgentId = Utility.GetCurrentUser_AgentId();
            //}
            //ObjdsPlans.SelectParameters["ProjectAgentId"].DefaultValue = AgentId.ToString();
            //GridViewPlans.DataBind();

            if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
                ObjdsPlans.SelectParameters["TaskCode"].DefaultValue = CmbTask.Value.ToString();
            else
                ObjdsPlans.SelectParameters["TaskCode"].DefaultValue = "-1";


            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
                ObjdsPlans.SelectParameters["WfStateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
            else
                ObjdsPlans.SelectParameters["WfStateDateFrom"].DefaultValue = "1";

            if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
                ObjdsPlans.SelectParameters["WfStateDateTo"].DefaultValue = txtCreateDateTo.Text;
            else
                ObjdsPlans.SelectParameters["WfStateDateTo"].DefaultValue = "2";


            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjdsPlans.SelectParameters["ProjectAgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                if (ComboAgent.SelectedItem.Value != null)
                    ObjdsPlans.SelectParameters["ProjectAgentId"].DefaultValue = ComboAgent.Value.ToString();
                else
                    ObjdsPlans.SelectParameters["ProjectAgentId"].DefaultValue = "-1";
            }
            GridViewPlans.DataBind();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
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

    #endregion

    #endregion
}