using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class Employee_TechnicalServices_Project_ImplementerOffice : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = btnNew2.Enabled = per.CanNew;
            btnView.Enabled = btnView2.Enabled = btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            btnDelete.Enabled = btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            btnInActive.Enabled = btnInActive2.Enabled = btnChangeRequest.Enabled = btnChangeRequest2.Enabled = CheckWorkFlowPermissionForChangeReq();
            LoadWfHelp();

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming).ToString();
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNewVis"] = btnNew.Visible;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnDelete"] = btnDelete.Enabled;
            this.ViewState["btnInActive"] = btnInActive.Enabled;
            this.ViewState["btnChangeRequest"] = btnChangeRequest.Visible;
        }

        SetPageFilter();
        SetGridRowIndex();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["btnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["btnInActive"];

        if (this.ViewState["btnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["btnDelete"];
        if (this.ViewState["btnChangeRequest"] != null)
            this.btnChangeRequest.Visible = this.btnChangeRequest.Visible = (bool)this.ViewState["btnChangeRequest"];


        string script = @"<SCRIPT language='javascript'> function CheckSearch() {";

        script += "if ( txtImpOfficeId.GetText() == '' &&  txtMeNo.GetText() == '' && txtMFNo.GetText() == ''   && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); ";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }
    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        NextPage("Change");
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        NextPage("InActive");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int ImpOfficeId = -1;
            int ImOfficeReqId = -1;

            if (GridViewObserverImplementerOffice.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewObserverImplementerOfficeRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverImplementerOffice.FindDetailRowTemplateControl(GridViewObserverImplementerOffice.FocusedRowIndex, "GridViewObserverImplementerOfficeRequest");
                if (GridViewObserverImplementerOfficeRequest != null)
                {
                    if (GridViewObserverImplementerOfficeRequest.FocusedRowIndex > -1)
                    {
                        DataRow row = GridViewObserverImplementerOfficeRequest.GetDataRow(GridViewObserverImplementerOfficeRequest.FocusedRowIndex);
                        ImpOfficeId = (int)row["ImpOfficeId"];
                        ImOfficeReqId = (int)row["ImOfficeReqId"];
                        DeleteRequest(ImpOfficeId, ImOfficeReqId, (int)row["Type"]);
                    }
                }
                else
                {
                    ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    return;
                }
            }
            else
            {
                ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewObserverImplementerOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewObserverImplementerOfficeRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverImplementerOffice.FindDetailRowTemplateControl(GridViewObserverImplementerOffice.FocusedRowIndex, "GridViewObserverImplementerOfficeRequest");
            if (GridViewObserverImplementerOfficeRequest != null)
            {
                if (GridViewObserverImplementerOfficeRequest.FocusedRowIndex > -1)
                {

                    DataRow row = GridViewObserverImplementerOfficeRequest.GetDataRow(GridViewObserverImplementerOfficeRequest.FocusedRowIndex);
                    if (row == null || Utility.IsDBNullOrNullValue(row["ImOfficeReqId"]))
                        return;
                    int TableId = (int)row["ImOfficeReqId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSImplementerOffice);
                    int WFCode = (int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming;
                    WFUserControl.PerformCallback(TableId, TableType, WFCode, e);
                    GridViewObserverImplementerOfficeRequest.DataBind();
                    GridViewObserverImplementerOfficeRequest.ExpandRow(GridViewObserverImplementerOfficeRequest.FocusedRowIndex);
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
    protected void GridViewObserverImplementerOfficeRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("ImpOfficeId")))
            Session["ImpOfficeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("ImpOfficeId");
        int Index = GridViewObserverImplementerOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewObserverImplementerOffice.FocusedRowIndex = Index;
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Office";
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
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementOffice;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSImplementerOffice);
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
                    GridViewObserverImplementerOffice.FilterExpression = GrdFlt;
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
            ObjectDataSourceImplementerOffice.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "ImpOfficeId":
                        if (Value != "-1")
                            txtImpOfficeId.Text = Value;
                        break;
                    case "Name":
                        if (Value != "%")
                            txtName.Text = Value;
                        break;
                    case "FileNo":
                        if (Value != "%")
                            txtMFNo.Text = Value;
                        break;
                    case "MeNo":
                        if (Value != "%")
                            txtMeNo.Text = Value;
                        break;
                    case "TaskId":
                        if (Value != "-1")
                        {
                            CmbTask.DataBind();
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
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

                    GridViewObserverImplementerOffice.DataBind();
                    Index = GridViewObserverImplementerOffice.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewObserverImplementerOffice.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewObserverImplementerOffice.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewObserverImplementerOffice.JSProperties["cpSelectedIndex"] = Index;
                        GridViewObserverImplementerOffice.DetailRows.ExpandRow(Index);
                        GridViewObserverImplementerOffice.FocusedRowIndex = Index;
                        GridViewObserverImplementerOffice.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }
    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceImplementerOffice.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceImplementerOffice.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceImplementerOffice.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceImplementerOffice.SelectParameters[i].DefaultValue + "&";
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
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming));
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
            if (!string.IsNullOrEmpty(txtImpOfficeId.Text))
                ObjectDataSourceImplementerOffice.SelectParameters["ImpOfficeId"].DefaultValue = txtImpOfficeId.Text;
            else
                ObjectDataSourceImplementerOffice.SelectParameters["ImpOfficeId"].DefaultValue = "-1";

            if (!string.IsNullOrEmpty(txtName.Text))
                ObjectDataSourceImplementerOffice.SelectParameters["Name"].DefaultValue = txtName.Text;
            else
                ObjectDataSourceImplementerOffice.SelectParameters["Name"].DefaultValue = "%";

            if (!string.IsNullOrEmpty(txtMFNo.Text))
                ObjectDataSourceImplementerOffice.SelectParameters["FileNo"].DefaultValue = txtMFNo.Text;
            else
                ObjectDataSourceImplementerOffice.SelectParameters["FileNo"].DefaultValue = "%";
            if (!string.IsNullOrEmpty(txtMeNo.Text))
                ObjectDataSourceImplementerOffice.SelectParameters["MeNo"].DefaultValue = txtMeNo.Text;
            else
                ObjectDataSourceImplementerOffice.SelectParameters["MeNo"].DefaultValue = "%";

            if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
                ObjectDataSourceImplementerOffice.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
            else
                ObjectDataSourceImplementerOffice.SelectParameters["TaskId"].DefaultValue = "-1";
            GridViewObserverImplementerOffice.DataBind();
        }
        catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    }

    private void Tracing()
    {
        int focucedIndex = -1;

        if (GridViewObserverImplementerOffice.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewObserverImplementerOffice.GetDataRow(GridViewObserverImplementerOffice.FocusedRowIndex)["ObsWorkReqId"].ToString());
            string GridFilterString = GridViewObserverImplementerOffice.FilterExpression;

            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewObserverImplementerOffice.FindDetailRowTemplateControl(GridViewObserverImplementerOffice.FocusedRowIndex, "GridViewObserverImplementerOfficeRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);

                    int TableId = (int)row["ObsWorkReqChangeId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSWorkRequest);
                    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;

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

    private void NextPage(string PageMode)
    {
        try
        {
            int PostId = -1; int ImpOfficeId = -1; int ImOfficeReqId = -1;
            string Filter = "";
            if (PageMode != "New")
            {
                string SearchFilterString = GenerateFilterString();
                string GridFilterString = GridViewObserverImplementerOffice.FilterExpression;
                if (GridViewObserverImplementerOffice.FocusedRowIndex <= -1)
                {
                    ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
                    return;
                }
                System.Data.DataRow row = GridViewObserverImplementerOffice.GetDataRow(GridViewObserverImplementerOffice.FocusedRowIndex);
                PostId = ImpOfficeId = Convert.ToInt32(row["ImpOfficeId"]);
                TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();

                System.Data.DataTable dt = ImplementerOfficeRequest.SelectLastRequest(-1, ImpOfficeId);
                if (dt.Rows.Count == 0)
                {
                    ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
                    return;
                }
                if (PageMode != "View" && PageMode != "Edit" && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
                {
                    ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
                    return;
                }
                ImOfficeReqId = Convert.ToInt32(dt.Rows[0]["ImOfficeReqId"]);
                Filter = "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);
            }
            Response.Redirect("ImplementerOfficeInsert.aspx?PgMd=" + Utility.EncryptQS(PageMode) + "&ReqId=" + Utility.EncryptQS(ImOfficeReqId.ToString()) + "&ImpOfficeId=" + Utility.EncryptQS(ImpOfficeId.ToString()) + Filter, false);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    private void DeleteRequest(int ImpOfficeId, int ImOfficeReqId, int RequestType)
    {
        TSP.DataManager.TechnicalServices.ImplementerOfficeRequest ImplementerOfficeRequest = new TSP.DataManager.TechnicalServices.ImplementerOfficeRequest();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(ImplementerOfficeRequest);
        try
        {
            transact.BeginSave();
            
            int WfCode = (int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming;
            ImplementerOfficeRequest.DeleteImplementerOfficeRequest(ImpOfficeId, ImOfficeReqId, RequestType == (int)TSP.DataManager.TechnicalServices.ImplementerOfficeRequestType.New ? true : false, WfCode);

            transact.EndSave();
            GridViewObserverImplementerOffice.DataBind();
            ShowMessage("حذف انجام شد");
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion   

}