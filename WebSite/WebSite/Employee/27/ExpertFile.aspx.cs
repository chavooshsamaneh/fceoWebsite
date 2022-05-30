using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
public partial class Employee_27_ExpertFile : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ExpertFileRequest).ToString();
            //CmbTask.DataBind();
            //CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد"));
            //CmbTask.SelectedIndex = 0;
            #region Permission and viewstates
            TSP.DataManager.Permission per = TSP.DataManager.ExpertFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = BtnNew2.Enabled = per.CanNew;
            btnReqView.Enabled = btnReqView2.Enabled = btnPrint.Enabled = btnPrint2.Enabled = GridViewExpertFile.Visible = per.CanView;


            this.ViewState["BtnRequset"] = btnInvalid.Enabled = btnInvalid2.Enabled =
            btnReqNew.Enabled = btnReqNew2.Enabled = CheckWorkFlowPermissionForChangeReq();

            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnReqView.Enabled;
            this.ViewState["BtnRequset"] = btnReqNew.Enabled;
            #endregion
            SetPageFilter();
            SetGridRowIndex();
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Expert";

        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewExpertFile.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewExpertFile.GetDataRow(GridViewExpertFile.FocusedRowIndex)["EfReqId"].ToString());
            string GridFilterString = GridViewExpertFile.FilterExpression;
            string SearchFilterString = GenerateFilterString();
            TSP.WebControls.CustomAspxDevGridView GridViewExpertFileRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewExpertFile.FindDetailRowTemplateControl(GridViewExpertFile.FocusedRowIndex, "GridViewExpertFileRequest");
            if (GridViewExpertFileRequest != null)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ExpertFileRequest);
                DataRow ExpertFileRequestRow = GridViewExpertFileRequest.GetDataRow(GridViewExpertFileRequest.FocusedRowIndex);
                int TableId = int.Parse(ExpertFileRequestRow["EfReqId"].ToString());
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;


                String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                    "&PostId=" + Utility.EncryptQS(PostId.ToString());
                Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }
    protected void btnReqNew_Click(object sender, EventArgs e)
    {
        NextPage("Change");
    }
    protected void btnReqView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }
    protected void btnReqEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }
    protected void btnInvalid_Click(object sender, EventArgs e)
    {
        NextPage("Invalid");
    }
    protected void GridViewExpertFile_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewExpertFile.DataBind();
        GridViewExpertFile.DetailRows.ExpandRow(GridViewExpertFile.FocusedRowIndex);
    }
    protected void GridViewExpertFileRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("EfId")))
            Session["EfId"] = (sender as ASPxGridView).GetMasterRowFieldValues("EfId");
        int Index = GridViewExpertFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewExpertFile.FocusedRowIndex = Index;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjectDataSourceexpertFile.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjectDataSourceexpertFile.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjectDataSourceexpertFile.SelectParameters["FirstName"].DefaultValue = txtName.Text.Trim();
        else
            ObjectDataSourceexpertFile.SelectParameters["FirstName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFamily.Text))
            ObjectDataSourceexpertFile.SelectParameters["LastName"].DefaultValue = txtFamily.Text.Trim();
        else
            ObjectDataSourceexpertFile.SelectParameters["LastName"].DefaultValue = "%";
        if (CmbTask.SelectedIndex != -1)
            ObjectDataSourceexpertFile.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            ObjectDataSourceexpertFile.SelectParameters["TaskId"].DefaultValue = "-1";
        GridViewExpertFile.DataBind();

    }
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewExpertFile.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewExpertFileRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewExpertFile.FindDetailRowTemplateControl(GridViewExpertFile.FocusedRowIndex, "GridViewExpertFileRequest");
            if (GridViewExpertFileRequest != null)
            {
                if (GridViewExpertFileRequest.FocusedRowIndex > -1)
                {

                    DataRow row = GridViewExpertFileRequest.GetDataRow(GridViewExpertFileRequest.FocusedRowIndex);
                    if (row == null || Utility.IsDBNullOrNullValue(row["EfReqId"]))
                        return;
                    int EfReqId = (int)row["EfReqId"];
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ExpertFileRequest);
                    int WFCode = (int)TSP.DataManager.WorkFlows.ExpertFileRequest;
                    WFUserControl.PerformCallback(EfReqId, TableType, WFCode, e);
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
    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int EfReqId = -1;
        int EfId = -1;
        int focucedIndex = -1;
        int PostId = -1;
        string SearchFilterString = GenerateFilterString();
        if (GridViewExpertFile.FocusedRowIndex > -1)
        {
            PostId = (int)GridViewExpertFile.GetDataRow(GridViewExpertFile.FocusedRowIndex)["EfReqId"];
        }
        if (Mode == "View" || Mode == "Edit")
        {
            if (GridViewExpertFile.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewExpertFileRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewExpertFile.FindDetailRowTemplateControl(GridViewExpertFile.FocusedRowIndex, "GridViewExpertFileRequest");
                if (GridViewExpertFileRequest != null)
                {
                    focucedIndex = GridViewExpertFileRequest.FocusedRowIndex;
                    if (focucedIndex > -1)
                    {
                        DataRow row = GridViewExpertFileRequest.GetDataRow(focucedIndex);
                        EfReqId = (int)row["EfReqId"];
                        EfId = (int)row["EfId"];
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
        }
        else
        {
            focucedIndex = GridViewExpertFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewExpertFile.GetDataRow(focucedIndex);
                EfId = (int)row["EfId"];
                if (Mode != "New" && Mode != "View" && Mode != "Edit")
                {
                    TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager = new TSP.DataManager.ExpertFileRequestManager();
                    ExpertFileRequestManager.Search(EfId, 0);
                    if (ExpertFileRequestManager.Count > 0)
                    {
                        ShowMessage("به دلیل وجود درخواست درجریان امکان ثبت درخواست جدید وجود ندارد.");
                        return;
                    }
                    ExpertFileRequestManager.Search(EfId, 1);
                    if (ExpertFileRequestManager.Count > 0)
                    {
                        EfReqId = (int)ExpertFileRequestManager[ExpertFileRequestManager.Count - 1]["EfReqId"];
                    }
                }

            }
        }

        if (EfReqId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            string GridFilterString = GridViewExpertFile.FilterExpression;
            if (Mode == "New")
            {
                EfId = -1;
                EfReqId = -1;
                Response.Redirect("ExpertFileInsert.aspx?EfReqId=" + Utility.EncryptQS(EfReqId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&EfId=" + Utility.EncryptQS(EfId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
            else
            {
                Response.Redirect("ExpertFileInsert.aspx?EfReqId=" + Utility.EncryptQS(EfReqId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&EfId=" + Utility.EncryptQS(EfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
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
                    GridViewExpertFile.FilterExpression = GrdFlt;
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
            ObjectDataSourceexpertFile.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "FirstName":
                        txtName.Text = Value;
                        break;
                    case "LastName":
                        txtFamily.Text = Value;
                        break;
                    case "TaskId":
                        CmbTask.DataBind();
                        CmbTask.SelectedIndex= CmbTask.Items.FindByValue(Value).Index;
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

                    GridViewExpertFile.DataBind();
                    Index = GridViewExpertFile.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewExpertFile.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewExpertFile.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewExpertFile.JSProperties["cpSelectedIndex"] = Index;
                        GridViewExpertFile.DetailRows.ExpandRow(Index);
                        GridViewExpertFile.FocusedRowIndex = Index;
                        GridViewExpertFile.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceexpertFile.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceexpertFile.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceexpertFile.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceexpertFile.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveExpertFileRequest;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ExpertFileRequest);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion
}