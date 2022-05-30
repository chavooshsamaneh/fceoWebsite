using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_MembersRegister_MembersAgent : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!Page.IsPostBack)
        {
            GridViewMember.JSProperties["cpMsg"] = "";
            GridViewMember.JSProperties["cpError"] = 0;

            TSP.DataManager.Permission Per = TSP.DataManager.MemberManager.GetUserPermissionForChangeAgent(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = Per.CanEdit;
            BtnEdit2.Enabled = Per.CanEdit;
            btnExportExcel.Enabled = Per.CanView;
            btnExportExcel2.Enabled = Per.CanView;
            GridViewMember.Visible = Per.CanView;

            this.ViewState["btnedit"] = btnEdit.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["btnedit"] != null)
            this.btnEdit.Enabled = this.BtnEdit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        int MeId = -1;
        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            MeId = (int)row["MeId"];
        }
        if (MeId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
        {
            ShowMessage(Msg);
            return;
        }


        MemberRequestManager.FindByMemberId(MeId, 0, -1);
        if (MemberRequestManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده در واحد عضویت امکان تغییر نمایندگی برای این عضو وجود ندارد.");
            return;
        }
        if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(MeId))
        {
            ShowMessage("به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد");
            return;
        }
        string GridFilterString = GridViewMember.FilterExpression;
        //   string SearchFilterString = GenerateFilterString();
        Response.Redirect("MembersAgentInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                              + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)); //+ "&SrchFlt="+ Utility.EncryptQS(SearchFilterString));
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "Member";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void OnClick_btnSearch(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjectDataSourceMember.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjectDataSourceMember.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjectDataSourceMember.SelectParameters["FirstName"].DefaultValue = txtName.Text.Trim();
        else
            ObjectDataSourceMember.SelectParameters["FirstName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFamily.Text))
            ObjectDataSourceMember.SelectParameters["LastName"].DefaultValue = txtFamily.Text.Trim();
        else
            ObjectDataSourceMember.SelectParameters["LastName"].DefaultValue = "%";

        if (cmbAgent.Value != null)
            ObjectDataSourceMember.SelectParameters["AgentId"].DefaultValue = cmbAgent.Value.ToString();
        else
            ObjectDataSourceMember.SelectParameters["AgentId"].DefaultValue = "-1";
        GridViewMember.DataBind();
    }

    protected void GridViewMember_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (e.Parameters == "Print")
        {
            GridViewMember.DetailRows.CollapseAllRows();
            GridViewMember.JSProperties["cpDoPrint"] = 1;
        }
    }

    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["MemberId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MeId");
        int Index = GridViewMember.FindVisibleIndexByKeyValue((sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewMember.FocusedRowIndex = Index;
    }

    protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridViewRequest == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                string WFName = "";
                if (!Utility.IsDBNullOrNullValue(e.GetValue("WorkFlowName")))
                {
                    WFName = e.GetValue("WorkFlowName").ToString();
                }

                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "AnswerDate")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";

    }

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceMember.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceMember.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceMember.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceMember.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
}