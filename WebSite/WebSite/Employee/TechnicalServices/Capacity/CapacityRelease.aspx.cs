using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Capacity_CapacityRelease : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.CapacityReleaseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Visible =btnNew2.Visible = per.CanNew;
            btnView.Enabled = btnView2.Enabled = btnExportExcel.Enabled = btnExportExcel2.Enabled=GridViewCapacityRelease.Visible = per.CanView;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            LoadWfHelp();
            AgentControl();

            if (!IsPostBack)
            {
                ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSCapacityRelease).ToString();
                this.ViewState["BtnNew"] = btnNew.Enabled;
            }
            if (this.ViewState["BtnNew"] != null)
                this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
 }
        SetPageFilter();
        SetGridRowIndex();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnNewVis"] != null)
            this.btnNew.Visible = this.btnNew2.Visible = (bool)this.ViewState["BtnNewVis"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit.Enabled = (bool)this.ViewState["btnEdit"];


        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
            script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";

            script += "if ( txtCreateDateTo=='' && txtCreateDateFrom==''  && txtMeId.GetText() == '' && txtMFNo.GetText() == '' && ComboAgent.GetSelectedIndex() == 0   && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
            script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); 
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = '';ComboAgent.SetSelectedIndex(0);CmbTask.SetSelectedIndex(0); document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';";
            script += "}</SCRIPT>";

            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
       

    }
    protected void btnNew_Click(object sender, EventArgs e)
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
            ShowMessage("با توجه به مرحله گردش کار مجاز به ویرایش این درخواست نمی باشید");
            return;
        }

        Response.Redirect("CapacityReleaseInsert.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&CapRId=" + Utility.EncryptQS(Convert.ToInt32(row["CapRId"]).ToString()) + "&PRj=" + Utility.EncryptQS(Convert.ToInt32(row["ProjectId"]).ToString()));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewCapacityRelease.FocusedRowIndex > -1)
        {
            DataRow row = GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex);
            if (row == null || Utility.IsDBNullOrNullValue(row["CapRId"]))
                return;
            int TableId = (int)row["CapRId"];
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSCapacityRelease);
            int WFCode = (int)TSP.DataManager.WorkFlows.TSCapacityRelease;
            WFUserControl.PerformCallback(TableId, TableType, WFCode, e);
            GridViewCapacityRelease.DataBind();            
        }
        else
        {
            WFUserControl.SetMsgText("ردیف مورد نظر را انتخاب نمایید.");
            WFUserControl.PerformCallback(-2, -2, -2, e);
        }

    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "CapacityRelease";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion

    #region Methods   
    void AgentControl()
    {
        if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
        {
            ObjectDataSourceCapacityRelease.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            ObjectdatasourceAgent.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            ComboAgent.Enabled = false;
            lblAgent.Visible = false;
            ComboAgent.Visible = false;
        }
    }
    private void Search()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtMeId.Text))
                ObjectDataSourceCapacityRelease.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
            else
                ObjectDataSourceCapacityRelease.SelectParameters["MeId"].DefaultValue = "-1";   
                ObjectDataSourceCapacityRelease.SelectParameters["MeIdType"].DefaultValue = "-1";
            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
                ObjectDataSourceCapacityRelease.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
            else
                ObjectDataSourceCapacityRelease.SelectParameters["CreateDateFrom"].DefaultValue = "1";
            if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
                ObjectDataSourceCapacityRelease.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
            else
                ObjectDataSourceCapacityRelease.SelectParameters["CreateDateTo"].DefaultValue = "2";
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectDataSourceCapacityRelease.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                if (ComboAgent.SelectedItem != null && ComboAgent.SelectedItem.Value != null)
                    ObjectDataSourceCapacityRelease.SelectParameters["AgentId"].DefaultValue = ComboAgent.Value.ToString();
                else
                    ObjectDataSourceCapacityRelease.SelectParameters["AgentId"].DefaultValue = "-1";
            }

            if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
                ObjectDataSourceCapacityRelease.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
            else
                ObjectDataSourceCapacityRelease.SelectParameters["TaskId"].DefaultValue = "-1";
            GridViewCapacityRelease.DataBind();
        }
        catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    }
    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    bool CheckEditConditions(int CapRId, int ProjectId, string CreateDate)
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

    private void Tracing()
    {
        if (GridViewCapacityRelease.FocusedRowIndex > -1)
        {
            DataRow Row = GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex);
            int PostId = int.Parse(GridViewCapacityRelease.GetDataRow(GridViewCapacityRelease.FocusedRowIndex)["CapRId"].ToString());
            string GridFilterString = GridViewCapacityRelease.FilterExpression;

            int TableId = int.Parse(Row["CapRId"].ToString());
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSCapacityRelease);
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;

            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                        "&PostId=" + Utility.EncryptQS(PostId.ToString());

            Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }

    void LoadWfHelp()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSCapacityRelease));
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
            RepeaterWfHelp1.DataSource = dt1;
            RepeaterWfHelp1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHelp2.DataSource = dt2;
            RepeaterWfHelp2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHelp3.DataSource = dt3;
            RepeaterWfHelp3.DataBind();
        }
    }


    #region Set Grid Index
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewCapacityRelease.FilterExpression = GrdFlt;
                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
            }
        }

    }
    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjectDataSourceCapacityRelease.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "CreateDateFrom":
                        if (Value != "1")
                            txtCreateDateFrom.Text = Value;
                        break;
                    case "CreateDateTo":
                        if (Value != "2")
                            txtCreateDateTo.Text = Value;
                        break;
                    case "TaskId":
                        if (Value != "-1")
                        {
                            CmbTask.DataBind();
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;// int.Parse(Value);// + 1;
                        }
                        break;
                    case "AgentId":
                        if (Value != "-1")
                        {
                            ComboAgent.DataBind();
                            ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "Type":
                        if (Value != "-1")
                        {
                            cmbReasonType.DataBind();
                            cmbReasonType.SelectedIndex = cmbReasonType.Items.FindByValue(Value).Index;
                        }
                        break;
                }
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

                    GridViewCapacityRelease.DataBind();
                    Index = GridViewCapacityRelease.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewCapacityRelease.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewCapacityRelease.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewCapacityRelease.JSProperties["cpSelectedIndex"] = Index;
                        GridViewCapacityRelease.DetailRows.ExpandRow(Index);
                        GridViewCapacityRelease.FocusedRowIndex = Index;
                        GridViewCapacityRelease.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }
    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceCapacityRelease.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceCapacityRelease.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceCapacityRelease.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceCapacityRelease.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion
    #endregion   
}