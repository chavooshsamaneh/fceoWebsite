using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_TechnicalServices_Capacity_CapacityRelease : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
           
            ObjectDataSourceCapacityRelease.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            ObjectDataSourceCapacityRelease.SelectParameters["MeIdType"].DefaultValue = Utility.GetCurrentUser_LoginType().ToString();
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("CapacityReleaseInsert.aspx?PgMd=" + Utility.EncryptQS("New") + "&CapRId=" + Utility.EncryptQS("-2") + "&PRj=" + Utility.EncryptQS("-2"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewCapacityRelease.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex);

        Response.Redirect("CapacityReleaseInsert.aspx?PgMd=" + Utility.EncryptQS("View") + "&CapRId=" + Utility.EncryptQS(Convert.ToInt32(row["CapRId"]).ToString()) + "&PRj=" + Utility.EncryptQS(Convert.ToInt32(row["ProjectId"]).ToString()));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //بررسی شود که در این حالت در چه مرحله ای است ایا می تواند ویرایش کند یا نه
        if (GridViewCapacityRelease.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex);
        if (!CheckEditConditions(Convert.ToInt32(row["CapRId"]), Convert.ToInt32(row["ProjectId"]), row["CreateDate"].ToString())) 
        {
            ShowMessage("با توجه به مرحله گردش کار مجاز به ویرایش این درخواست نیستید");
            return;
        }

        Response.Redirect("CapacityReleaseInsert.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&CapRId=" + Utility.EncryptQS(Convert.ToInt32(row["CapRId"]).ToString()) + "&PRj=" + Utility.EncryptQS(Convert.ToInt32(row["ProjectId"]).ToString()));
    }
    protected void btnTracing_Click(object sender, EventArgs e)
    {
        
        if (GridViewCapacityRelease.FocusedRowIndex > -1)
        {
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSCapacityRelease);
            DataRow Row = GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex);
            int TableId = int.Parse(Row["CapRId"].ToString());

            Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }
    #endregion

    #region Methods    
    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    bool CheckEditConditions(int CapRId,int ProjectId,string CreateDate)
    {
        int CurrentCapRTaskCode = -1;
        int CapRTaskId = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = new DataTable();
        dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSCapacityRelease, CapRId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
             CurrentCapRTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
             CapRTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
        }
        if (CurrentCapRTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveCapacityReleaseRequestInfo)
            return false;

        ////////TSP.DataManager.TechnicalServices.ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.ObserversManager();
        ////////DataTable dtObserverCapacityDecrementActive = ObserversManager.SelectObserverCapacityDecrementActiveByProjectId(ProjectId, Utility.GetCurrentUser_MeId(), 1, CreateDate, 0);
        ////////if (dtObserverCapacityDecrementActive.Rows.Count == 0)
        ////////    return false;

        ////////TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        ////////DataTable dtProjectRequest = ProjectRequestManager.SelectProjectRequestCount(ProjectId, 0);
        ////////if (Convert.ToInt32(dtProjectRequest.Rows[0]["RequestCount"]) != 0)
        ////////    return false;

        return true;
    }
    #endregion   



}