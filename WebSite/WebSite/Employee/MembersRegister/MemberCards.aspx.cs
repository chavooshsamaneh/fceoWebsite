using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class Employee_MembersRegister_MemberCards : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Events
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

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["SendBackDataTable_MeCWF"] = "";
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming).ToString();

            TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit1.Enabled = per.CanEdit;
            btnTracing.Enabled = per.CanView;
            btnTracing2.Enabled = per.CanView;


            btnNew1.Enabled = per.CanNew;
            BtnNew.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewMemberCard.ClientVisible = per.CanView;


            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive2.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        SetPageFilter();

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = this.btnTracing.Enabled = this.btnTracing2.Enabled = GridViewMemberCard.ClientVisible = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive2.Enabled = this.btnDisActive.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        script += "var txtPrintDateFrom = document.getElementById('" + txtPrintDateFrom.ClientID + "').value;";
        script += "var txtPrintDateto = document.getElementById('" + txtPrintDateto.ClientID + "').value;";
        script += "if ( txtCreateDateFrom=='' && txtCreateDateTo=='' && txtPrintDateFrom=='' && txtPrintDateto=='' && txtMeId.GetText() == '' && txtName.GetText() == '' && txtFamily.GetText() == '' && txtName.GetText() == ''&& CmbIsPrinted.GetSelectedIndex() == 0 &&  CmbMeCrdType.GetSelectedIndex() == 0  ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtName.SetText(''); txtName.SetText(''); txtFamily.SetText(''); 
                    CmbIsPrinted.SetSelectedIndex(0); CmbMeCrdType.SetSelectedIndex(0);
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = ''; document.getElementById('" + txtPrintDateFrom.ClientID + "').value = ''; document.getElementById('" + txtPrintDateto.ClientID + "').value = '';";
        script += "document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    #region btn Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewMemberCard.FocusedRowIndex <= 1)
        {
            SetMessage("ردیفی انتخاب نشده است.");
            return;
        }
        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
        DataRow row = GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex);
        int MeCrdId = (int)row["MeCrdId"];
        MemberCardsManager.FindByCode(MeCrdId);
        if (MemberCardsManager.Count != 1)
        {
            SetMessage("اطلاعات کارت انتخاب شده توسط کاربر دیگری تغییر یافته است.");
            return;
        }
        if (MemberCardsManager[0]["IsConfirmed"].ToString() != "0")
        {
            SetMessage("امکان ویرایش اطلاعات وجود ندارد.گردش کار درخواست صدور کارت انتخاب شده به اتمام رسیده است.");
            return;
        }
        if (CheckPermitionForEdit(MeCrdId))
            NextPage("Edit");
        else
        {
            SetMessage("شما قادر به ویرایش این درخواست نمی باشید.سطح دسترسی گردش کار جهت ثبت اطلاعات کارت عضویت را ندارید و یا درخواست توسط کاربر دیگری ثبت شده است .");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (GridViewMemberCard.FocusedRowIndex <= 0)
        {
            SetMessage("لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید.");
        }
        DataRow row = GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex);
        int MeCrdId = (int)row["MeCrdId"];
        if (CheckPermitionForDelete(MeCrdId))
        {
            DeleteRequest(MeCrdId);
            GridViewMemberCard.DataBind();
        }
        else
        {
            SetMessage("شما قادر به لغو درخواست نمی باشید.سطح دسترسی گردش کار جهت این عملیات را ندارید و یا درخواست توسط کاربر دیگری ثبت شده است");
        }

    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewMemberCard.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
            DataRow MeCardRow = GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex);
            int TableId = int.Parse(MeCardRow["MeCrdId"].ToString());
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming;

            int PostId = int.Parse(GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex)["MeCrdId"].ToString());
            string GridFilterString = GridViewMemberCard.FilterExpression;
            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                    "&PostId=" + Utility.EncryptQS(PostId.ToString());

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString())
                + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            SetMessage("برای پیگیری پرونده ابتدا یک درخواست را انتخاب نمائید");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "MembersCard";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }
    #endregion
    //*******************************************************************************************************************8
    protected void GridViewMemberCard_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MeNo" || e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberCard.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberCard.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }

    protected void GridViewMemberCardRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewMemberCard.FindDetailRowTemplateControl(GridViewMemberCard.FocusedRowIndex, "GridViewMemberCardRequest");
            if (GridViewRequest != null)
            {
                DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
                if (btnWFState != null)
                {
                    if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                    {
                        btnWFState.ToolTip = "تعریف نشده";
                        btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                        return;
                    }

                    if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                    {
                        btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                        btnWFState.ImageUrl = "~/Images/WFStart.png";
                    }
                    else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                    {
                        btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                        btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                    }
                    else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                    {
                        btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                        btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                    }
                    else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                    {
                        btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                        btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                    }
                    else
                    {
                    }
                }
            }
        }
    }

    protected void GridViewMemberCard_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberCard.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberCard_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("WFTaskType") != null)
        {
            if (e.GetValue("WFTaskType").ToString() == "1" || e.GetValue("WFTaskType").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

    protected void GridViewMemberCard_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewMemberCard.DataBind();
    }

    protected void GridViewMemberCard_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();
    }

    protected void GridViewMemberCard_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "MeNo" || e.Column.FieldName == "FileNo" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMemberCard_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    GridViewMemberCard.JSProperties["cpPrint"] = 1;
                    GridViewMemberCard.DetailRows.CollapseAllRows();
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewMemberCard.Columns;
                    Session["DataSource"] = ObjdsMeCards;

                    Session["Title"] = "کارت های عضویت";
                    break;
            }
        }
        else
            GridViewMemberCard.DataBind();
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMemberCard.FocusedRowIndex > -1)
        {
            DataRow PeriodRegRow = GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex);
            int TableId = int.Parse(PeriodRegRow["MeCrdId"].ToString());
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberCards);// (int)TSP.DataManager.TableCodes.MemberCards;
            int WfCode = (int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming;
            WFUserControl.PerformCallback(TableId, TableType, WfCode, e);
            GridViewMemberCard.DataBind();
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
    }

    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int MeCrdId = -1;
        string GridFilterString = GridViewMemberCard.FilterExpression;
        if (GridViewMemberCard.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberCard.GetDataRow(GridViewMemberCard.FocusedRowIndex);
            MeCrdId = (int)row["MeCrdId"];
        }
        if (MeCrdId == -1 && Mode != "New")
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                MeCrdId = -1;
                Response.Redirect("AddMemberCards.aspx?MeCrdId=" + Utility.EncryptQS(MeCrdId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddMemberCards.aspx?MeCrdId=" + Utility.EncryptQS(MeCrdId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetMessage("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetDeleteError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                SetMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage("خطایی در حذف انجام گرفته است.");
            }
        }
        else
        {
            SetMessage("خطایی در حذف انجام گرفته است.");
        }
    }

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }

    private void DeleteRequest(int TableId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TransactionManager.Add(MemberCardsManager);
        TransactionManager.Add(WorkFlowStateManager);
        try
        {
            TransactionManager.BeginSave();
            MemberCardsManager.FindByCode(TableId);
            if (MemberCardsManager.Count == 1)
            {
                MemberCardsManager[0].Delete();
                if (MemberCardsManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    SetMessage("خطایی در حذف انجام گرفته است.");
                    return;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در حذف انجام گرفته است.");
                return;
            }

            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming, TableId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[i].Delete();
                }
                if (WorkFlowStateManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    SetMessage("خطایی در حذف انجام گرفته است.");
                    return;
                }
            }

            #endregion
            TransactionManager.EndSave();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int UserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());

                            if (UserId == Utility.GetCurrentUser_UserId())
                            {
                                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                if (Permission > 0)
                                    return true;
                                else
                                    return false;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
                int WfCode = (int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming;
                DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtState.Rows.Count == 1)
                {
                    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
                    // int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            int FirstUserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                if (FirstUserId == Utility.GetCurrentUser_UserId())
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMemberCard.FilterExpression = GrdFlt;
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

                int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));

                GridViewMemberCard.DataBind();
                Index = GridViewMemberCard.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewMemberCard.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewMemberCard.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewMemberCard.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberCards).ToString());
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void Search()
    {    
        if (CmbMeCrdType.SelectedItem != null && CmbMeCrdType.SelectedItem.Value != null)
            ObjdsMeCards.SelectParameters["MeCrdType"].DefaultValue = CmbMeCrdType.Value.ToString();
        else
            ObjdsMeCards.SelectParameters["MeCrdType"].DefaultValue = "-1";

        if (CmbIsPrinted.SelectedItem != null && CmbIsPrinted.SelectedItem.Value != null)
            ObjdsMeCards.SelectParameters["IsPrinted"].DefaultValue = CmbIsPrinted.Value.ToString();
        else
            ObjdsMeCards.SelectParameters["IsPrinted"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtFamily.Text))
            ObjdsMeCards.SelectParameters["LastName"].DefaultValue = txtFamily.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["LastName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjdsMeCards.SelectParameters["FirstName"].DefaultValue = txtName.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["FirstName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsMeCards.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtPrintDateto.Text))
            ObjdsMeCards.SelectParameters["PrintDateTo"].DefaultValue = txtPrintDateto.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["PrintDateTo"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtPrintDateFrom.Text))
            ObjdsMeCards.SelectParameters["PrintDateFrom"].DefaultValue = txtPrintDateFrom.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["PrintDateFrom"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            ObjdsMeCards.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["CreateDateTo"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            ObjdsMeCards.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text.Trim();
        else
            ObjdsMeCards.SelectParameters["CreateDateFrom"].DefaultValue = "9999/99/99";
        GridViewMemberCard.DataBind();

    }

    #endregion
}
