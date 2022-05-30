using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class Employee_TechnicalServices_Capacity_ObserverWorkRequest : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ObserverWorkRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Visible = per.CanNew;
            btnNew2.Visible = per.CanNew;
            btnView.Enabled = btnView2.Enabled = btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            btnChangeRequest.Visible = btnChangeRequest2.Visible = CheckWorkFlowPermissionForChangeReq();
            btnInActive.Enabled = btnInActive2.Enabled = per.CanDelete;
            LoadWfHelp();
            comboMjParent.DataBind();
            comboMjParent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            comboMjParent.SelectedIndex = 0;
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming).ToString();
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnNewVis"] = btnNew.Visible;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnInActive"] = btnInActive.Enabled;
            this.ViewState["btnChangeRequest"] = btnChangeRequest.Visible;

            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                ComboAgent.ClientEnabled = true;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = 0;
            }
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
        if (this.ViewState["btnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled= (bool)this.ViewState["btnInActive"];
        if (this.ViewState["btnChangeRequest"] != null)
            this.btnChangeRequest.Visible = this.btnChangeRequest.Visible = (bool)this.ViewState["btnChangeRequest"];


        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";

        script += "if ( txtCreateDateTo=='' && txtCreateDateFrom==''  && txtMeId.GetText() == '' && txtMFNo.GetText() == '' && ComboAgent.GetSelectedIndex() == 0  && comboMjParent.GetSelectedIndex() == 0  && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); 
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = '';ComboAgent.SetSelectedIndex(0);comboMjParent.SetSelectedIndex(0);CmbTask.SetSelectedIndex(0); document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("New") + "&ObsWChangId=" + Utility.EncryptQS("-2") + "&MeId=" + Utility.EncryptQS("-2"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);
        TSP.WebControls.CustomAspxDevGridView GridViewObserverWorkRequestChange = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverWorkRequest.FindDetailRowTemplateControl(GridViewObserverWorkRequest.FocusedRowIndex, "GridViewObserverWorkRequestChange");
        if (GridViewObserverWorkRequestChange == null)
        {
            ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        if (GridViewObserverWorkRequestChange.FocusedRowIndex > -1)
        {
            int PostId = -1;
            string SearchFilterString = GenerateFilterString();
            if (GridViewObserverWorkRequest.FocusedRowIndex > -1)
            {
                PostId = (int)GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex)["ObsWorkReqId"];
            }
            string GridFilterString = GridViewObserverWorkRequestChange.FilterExpression;
            DataRow rowReqChange = GridViewObserverWorkRequestChange.GetDataRow(GridViewObserverWorkRequestChange.FocusedRowIndex);
            Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("View") + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(rowReqChange["ObsWorkReqChangeId"]).ToString()) + "&MeId=" + Utility.EncryptQS(Convert.ToInt32(row["MeId"]).ToString())
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        }
    }
    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        try
        {

            if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
            {
                ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
                return;
            }
            System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);
            int MeId = Convert.ToInt32(row["MeId"]);
            TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();

            System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(-1, MeId);
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
            {
                ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
                return;
            }
            int ObsWorkReqChangeId = Convert.ToInt32(dt.Rows[0]["ObsWorkReqChangeId"]);
            int MeAgentId = Convert.ToInt32(dt.Rows[0]["MeAgentId"]);
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (MeAgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            int _CurrentCapacityAssignmentId = -2;
            string _CurrentCapacityEndate = "";
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
                _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
            }
            System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForChangeWorkRequest(MeId, _CurrentCapacityEndate);

            if (!Convert.ToBoolean(Result[0]) && (Utility.IsDBNullOrNullValue(Result[5]) ? true : Convert.ToInt32(Result[5]) != (int)TSP.DataManager.TechnicalServices.TSWorkRequestConditionErrorType.DocumentExipration))
            {
                ShowMessage(Result[1].ToString());
                return;
            }
            else
                Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Change") + "&ObsWChangId=" + Utility.EncryptQS(ObsWorkReqChangeId.ToString()) + "&MeId=" + Utility.EncryptQS(Convert.ToInt32(row["MeId"]).ToString()), false);
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);
        int MeId = Convert.ToInt32(row["MeId"]);
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        try
        {
            int MjParentId = -2;
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 0)
            {
                ShowMessage("اطلاعات یافت نشد.");
                return;
            }
            if (Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                ShowMessage("نمایندگی شخص نامشص است.");
                return;
            }
            int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("برای این کد عضویت آماده بکاری وجود ندارد.");
                return;
            }
            int LastMfId = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.");
                return;
            }
            LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            TransactionManager.BeginSave();
            int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست غیرفعال آماده بکاری", "تایید اتوماتیک درخواست غیرفعال آماده بکاری" + "-نام کاربر" + Utility.GetCurrentUser_FullName()
               , TSP.DataManager.TSObserverWorkRequestChangeType.InActive, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), AgentId, TransactionManager);
            if (per != 0)
            {
                TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                ShowMessage(ErrorMsg);
                return;
            }
            TransactionManager.EndSave();
            ShowMessage("بروزرسانی انجام شد");
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }


    }
    protected void btnOffDate_Click(object sender, EventArgs e)
    {

        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);

        Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Off") + "&ObsWId=" + Utility.EncryptQS(Convert.ToInt32(row["ObsWorkReqId"]).ToString()) + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(row["ObsWChangId"]).ToString()));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewObserverWorkRequest.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewObserverWorkRequestChange = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverWorkRequest.FindDetailRowTemplateControl(GridViewObserverWorkRequest.FocusedRowIndex, "GridViewObserverWorkRequestChange");
            if (GridViewObserverWorkRequestChange != null)
            {
                if (GridViewObserverWorkRequestChange.FocusedRowIndex > -1)
                {

                    DataRow row = GridViewObserverWorkRequestChange.GetDataRow(GridViewObserverWorkRequestChange.FocusedRowIndex);
                    if (row == null || Utility.IsDBNullOrNullValue(row["ObsWorkReqChangeId"]))
                        return;
                    int TableId = (int)row["ObsWorkReqChangeId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSWorkRequest);
                    int WFCode = (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;
                    WFUserControl.PerformCallback(TableId, TableType, WFCode, e);
                    GridViewObserverWorkRequestChange.DataBind();
                    GridViewObserverWorkRequestChange.ExpandRow(GridViewObserverWorkRequestChange.FocusedRowIndex);
                }
            }
            else
            {
                WFUserControl.SetMsgText("برای ارسال پرونده به مرحله بعد ابتدا یک درخواست را انتخاب نمائید");
                WFUserControl.PerformCallback(-2, -2, -2, e);

            }
        }
        else
        {
            WFUserControl.SetMsgText("ردیف مورد نظر را انتخاب نمایید.");
            WFUserControl.PerformCallback(-2, -2, -2, e);
        }

    }

    protected void GridViewObserverWorkRequestChange_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("ObsWorkReqId")))
            Session["ObsWorkReqId"] = (sender as ASPxGridView).GetMasterRowFieldValues("ObsWorkReqId");
        int Index = GridViewObserverWorkRequest.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewObserverWorkRequest.FocusedRowIndex = Index;
        //ObjectDataSourceObsWorkChange
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "WorkRequest";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion
    #region Methods

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
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
                    GridViewObserverWorkRequest.FilterExpression = GrdFlt;
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
            ObjectDataSourceWorkRequest.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "MFNo":
                        txtMFNo.Text = Value;
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
                    case "MJParentId":
                        if (Value != "-1")
                        {
                            comboMjParent.DataBind();
                            comboMjParent.SelectedIndex = comboMjParent.Items.FindByValue(Value).Index;
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

                    GridViewObserverWorkRequest.DataBind();
                    Index = GridViewObserverWorkRequest.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewObserverWorkRequest.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewObserverWorkRequest.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewObserverWorkRequest.JSProperties["cpSelectedIndex"] = Index;
                        GridViewObserverWorkRequest.DetailRows.ExpandRow(Index);
                        GridViewObserverWorkRequest.FocusedRowIndex = Index;
                        GridViewObserverWorkRequest.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }
    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceWorkRequest.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceWorkRequest.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceWorkRequest.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceWorkRequest.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion
    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    void LoadWfHelp()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming));
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
    private void Search()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
                ObjectDataSourceWorkRequest.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
            else
                ObjectDataSourceWorkRequest.SelectParameters["CreateDateFrom"].DefaultValue = "1";

            if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
                ObjectDataSourceWorkRequest.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
            else
                ObjectDataSourceWorkRequest.SelectParameters["CreateDateTo"].DefaultValue = "2";

            if (!string.IsNullOrEmpty(txtMeId.Text))
                ObjectDataSourceWorkRequest.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
            else
                ObjectDataSourceWorkRequest.SelectParameters["MeId"].DefaultValue = "-1";
            if (!string.IsNullOrEmpty(txtMFNo.Text))
                ObjectDataSourceWorkRequest.SelectParameters["MFNo"].DefaultValue = txtMFNo.Text;
            else
                ObjectDataSourceWorkRequest.SelectParameters["MFNo"].DefaultValue = "%";

            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectDataSourceWorkRequest.SelectParameters["CurrentAgentCode"].DefaultValue = "-1";// Utility.GetCurrentAgentCode().ToString();
                ObjectDataSourceWorkRequest.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                ObjectDataSourceWorkRequest.SelectParameters["CurrentAgentCode"].DefaultValue = "-1";
                if (ComboAgent.SelectedItem != null && ComboAgent.SelectedItem.Value != null)
                    ObjectDataSourceWorkRequest.SelectParameters["AgentId"].DefaultValue = ComboAgent.Value.ToString();
                else
                    ObjectDataSourceWorkRequest.SelectParameters["AgentId"].DefaultValue = "-1";
            }
            if (comboMjParent.SelectedItem != null && comboMjParent.SelectedItem.Value != null)
                ObjectDataSourceWorkRequest.SelectParameters["MJParentId"].DefaultValue = comboMjParent.Value.ToString();
            else
                ObjectDataSourceWorkRequest.SelectParameters["MJParentId"].DefaultValue = "-1";
            if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
                ObjectDataSourceWorkRequest.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
            else
                ObjectDataSourceWorkRequest.SelectParameters["TaskId"].DefaultValue = "-1";
            GridViewObserverWorkRequest.DataBind();
        }
        catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    }

    private void Tracing()
    {
        int focucedIndex = -1;

        if (GridViewObserverWorkRequest.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex)["ObsWorkReqId"].ToString());
            string GridFilterString = GridViewObserverWorkRequest.FilterExpression;

            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverWorkRequest.FindDetailRowTemplateControl(GridViewObserverWorkRequest.FocusedRowIndex, "GridViewObserverWorkRequestChange");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);

                    int TableId = (int)row["ObsWorkReqChangeId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSWorkRequest);
                    int WorkFlowCode =(int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;

                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                                "&PostId=" + Utility.EncryptQS(PostId.ToString());

                    if (IsCallback)
                        ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    else
                        Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    

                }
            }
            else
            {
                ShowMessage("ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است.");
        }
    }
    #endregion
}