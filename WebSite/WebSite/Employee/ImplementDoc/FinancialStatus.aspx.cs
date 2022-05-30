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

public partial class Employee_ImplementDoc_FinancialStatus : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewFinancialStatus.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["MfId"]))
            {
                Response.Redirect("ImplementDoc.aspx");
                return;
            }
            try
            {
                HiddenFieldFinantialStatus["MfId"] = Server.HtmlDecode(Request.QueryString["MfId"]).ToString();
                HiddenFieldFinantialStatus["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"]).ToString();
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DocMemberFileManager.ClearBeforeFill = true;
                int MfId = int.Parse(Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString()));
                DataTable dtDoMeFile = DocMemberFileManager.SelectImplementDoc(-1, MfId);
                if (dtDoMeFile.Rows.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(dtDoMeFile.Rows[0]["FullName"]))
                        RoundPanelFinancial.HeaderText = "مجوز فعالیت مجری حقیقی : " + dtDoMeFile.Rows[0]["FullName"].ToString();
                }

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            CheckWorkFlowPermission();

            if (!CheckPermissionForJudge())
            {
                btnJudgment.Enabled = false;
                btnJudgment2.Enabled = false;
            }
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnJudgment"] = btnJudgment.Enabled;

        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnJudgment"] != null)
            this.btnJudgment.Enabled = this.btnJudgment2.Enabled = (bool)this.ViewState["btnJudgment"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImplementDoc.aspx");
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobHistory":
                Response.Redirect("~/Employee/Document/MemberJobHistory.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
                break;
            case "ImplDoc":
                Response.Redirect("AddImplementDoc.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
                break;
        }
    }

    protected void btnJudgment_Click(object sender, EventArgs e)
    {
        //if (GridViewFinancialStatus.FocusedRowIndex > -1)
        //{            
        //    int MfId = int.Parse(Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString()));            
        //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //    int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.GradingImplementDoc;
        //    int GradingImplementDocTaskId = -1;

        //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode(WorkflowCode, MfId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == GradingImplementDocTaskId)
        //        {
        //            NextPage("Judge");
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "در این مرحله از جریان کار قادر به ثبت نظر کارشناسی نمی باشید.";
        //        }
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "جریان کار پروانه جاری نامشخص است.";
        //    }
        //}
    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as ASPxGridView).GetMasterRowKeyValue();        
    }

    protected void GridViewFinancialStatus_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewFinancialStatus.FocusedRowIndex = e.VisibleIndex;
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewFinancialStatus.FocusedRowIndex > -1)
        {
            DataRow FinancialRow = GridViewFinancialStatus.GetDataRow(GridViewFinancialStatus.FocusedRowIndex);
            if (FinancialRow != null)
            {
                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString()));
                int OfsId = int.Parse(FinancialRow["OfsId"].ToString());
                int SelectedMfId = int.Parse(FinancialRow["PKId"].ToString());
                if (MFId == SelectedMfId)
                    Delete(OfsId);
                //else
                //    Inactive(MExmId);
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void GridViewFinancialStatus_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
         
        }
    }

    protected void GridViewFinancialStatus_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int OfsId = -1;
        int focucedIndex = GridViewFinancialStatus.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewFinancialStatus.GetDataRow(focucedIndex);
            OfsId = (int)row["OfsId"];
        }
        if (OfsId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                OfsId = -1;
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
            else
            {
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
        }
    }

    private void Delete(int OfsId)
    {
        try
        {
            TSP.DataManager.DocOffOfficeFinancialStatusManager DocOffOfficeFinancialStatusManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
            DocOffOfficeFinancialStatusManager.FindByCode(OfsId);
            if (DocOffOfficeFinancialStatusManager.Count == 1)
            {
                DocOffOfficeFinancialStatusManager[0].Delete();

                int cn = DocOffOfficeFinancialStatusManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
            GridViewFinancialStatus.DataBind();
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == 0)
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
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string MFId = Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, int.Parse(MFId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
        {
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);

            if (TaskDoerManager.Count > 0)
            {
                int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
                NezamMemberChartManager.FindByNcId(NcId);

                int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

                LoginManager.FindByMeIdUltId(EmpId, 4);
                if (LoginManager.Count > 0)
                {
                    int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                    int CurrentUserId = Utility.GetCurrentUser_UserId();
                    if (CurrentUserId == userId)
                    {
                        BtnNew.Enabled = true;
                        BtnNew2.Enabled = true;
                        btnEdit.Enabled = true;
                        btnEdit2.Enabled = true;
                        btnInActive.Enabled = true;
                        btnInActive2.Enabled = true;
                        //  btnDelete.Enabled = true;
                        //  btnDelete2.Enabled = true;
                    }
                    else
                    {

                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        btnInActive.Enabled = false;
                        btnInActive2.Enabled = false;
                        //  btnDelete.Enabled = false;
                        //  btnDelete2.Enabled = false;
                    }
                }
                else
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                    // btnDelete.Enabled = false;
                    //  btnDelete2.Enabled = false;
                }
            }
            else
            {
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnInActive.Enabled = false;
                btnInActive2.Enabled = false;
                // btnDelete.Enabled = false;
                //btnDelete2.Enabled = false;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnInActive.Enabled = false;
            btnInActive2.Enabled = false;
            //  btnDelete.Enabled = false;
            //  btnDelete2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private Boolean CheckPermissionForJudge()
    {
        return false;
        //int MfId = int.Parse(Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString()));
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.GradingImplementDoc;
        //int GradingImplementDocTaskId = -1;

        //WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //if (WorkFlowTaskManager.Count == 1)
        //{
        //    GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode(WorkflowCode, MfId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == GradingImplementDocTaskId)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //            //this.DivReport.Visible = true;
        //            //this.LabelWarning.Text = "در این مرحله از جریان کار قادر به ثبت نظر کارشناسی نمی باشید.";
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //        //this.DivReport.Visible = true;
        //        //this.LabelWarning.Text = "جریان کار پروانه جاری نامشخص است.";
        //    }
        //}
        //else
        //{
        //    return false;
        //}      
    }

    #endregion             
}
