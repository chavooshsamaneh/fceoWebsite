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

public partial class Institue_Amoozesh_Seminar : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowId;
            }
            Session["SendBackDataTable_Seminar"] = null;
            ObjectDataSource1.FilterParameters[0].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddSeminar.aspx?SeId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&SeReqId=" + Utility.EncryptQS("-1"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int SeReqId = -1;
        try
        {

            int SeId = -1;
            string GridFilterString = GridViewSeminar.FilterExpression;

            if (GridViewSeminar.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
                SeId = (int)row["SeId"];
            }
            if (SeId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا درخواست یک سمینار را انتخاب نمائید";
                return;
            }
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                        Response.Redirect("AddSeminar.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }

        }
        catch (Exception err) { Utility.SaveWebsiteError(err); }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        int SeReqId = -1;
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            string GridFilterString = GridViewSeminar.FilterExpression;
            if (GridViewSeminar.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
                SeId = (int)row["SeId"];
            }
            if (SeId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
                return;
            }

            SeReqId = -2;
            TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
            SeminarRequestManager.SelectSeminarRequest(SeId, -1, 0);
            if (SeminarRequestManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            SeminarRequestManager.SelectSeminarRequest(SeId, -1, 1);
            if (SeminarRequestManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل عدم وجود درخواست تایید شده برای این سمینار، امکان ثبت درخواست جدید وجود ندارد.";
                return;
            }
            SeReqId = Convert.ToInt32(SeminarRequestManager[SeminarRequestManager.Count-1]["SeReqId"]);
            Response.Redirect("AddSeminar.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change")
                 + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()), false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int SeReqId = -1;
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest == null)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.VisibleRowCount <= 0)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.FocusedRowIndex == -1)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            SeReqId = int.Parse(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["SeReqId"].ToString());
            TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
            SeminarRequestManager.FindByCode(SeReqId);
            if (SeminarRequestManager.Count != 1)
            {
                ShowMessage("خطا در بازخوانی اطلاعات");
                return;
            }
            if (Convert.ToInt32(SeminarRequestManager[0]["IsConfirm"]) != 0)
            {
                ShowMessage("امکان حذف درخواست تایید شده وجود ندارد");
                return;
            }

            int SeId = Convert.ToInt32(SeminarRequestManager[0]["SeId"]);
            if (Convert.ToInt32(SeminarRequestManager[0]["SeId"]) == (int)TSP.DataManager.SeminarRequestType.SaveInfo)
            {
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                DataTable dtPeriodRegister = PeriodRegisterManager.SelectPeriodRegisterForSeminar(-1, SeId, -1);
                if (dtPeriodRegister.Rows.Count > 0)
                {
                    ShowMessage("امکان حذف درخواست وجود ندارد .اعضا در این سمینار ثبت نام نموده اند ");
                    return;
                }
            }
            SeminarRequestManager.DeleteSeminarRequestInfo(SeReqId);
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.SeminarConfirming, SeReqId);
            if (WorkFlowStateManager.Count == 0)
            {
                SetMessage("خطایی در حذف انجام گرفته است.");
                return;
            }
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[0].Delete();
                    if (WorkFlowStateManager.Save() <= 0)
                    {
                        SetMessage("خطایی در حذف انجام گرفته است.");
                        return;
                    }
                    WorkFlowStateManager.DataTable.AcceptChanges();
                }
            }
            ShowMessage("حذف درخواست با موفقیت انجام شد");
            GridViewSeminar.DataBind();





        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در حذف درخواست انجام گرفته است");
        }
        ////////int SeId = -1;
        ////////if (GridViewSeminar.FocusedRowIndex > -1)
        ////////{
        ////////    DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
        ////////    SeId = (int)row["SeId"];
        ////////}
        ////////if (SeId == -1)
        ////////{
        ////////    this.DivReport.Visible = true;
        ////////    this.LabelWarning.Text = "جهت لغو درخواست ابتدا یک رکورد را انتخاب نمائید";
        ////////    return;
        ////////}
        ////////TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        ////////TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        ////////TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
        ////////TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        ////////TransactionManager.Add(SeminarManager);
        ////////TransactionManager.Add(WorkFlowStateManager);
        ////////try
        ////////{
        ////////    TransactionManager.BeginSave();
        ////////    SeminarManager.FindByCode(SeId);
        ////////    if (SeminarManager.Count != 1)
        ////////    {
        ////////        SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        ////////        TransactionManager.CancelSave();
        ////////        return;
        ////////    }
        ////////    SeminarManager[0].Delete();
        ////////    SeminarManager.Save();

        ////////    #region Delete WFState
        ////////    WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.SeminarConfirming, SeId);
        ////////    if (WorkFlowStateManager.Count == 0)
        ////////    {
        ////////        TransactionManager.CancelSave();
        ////////        SetMessage("خطایی در حذف انجام گرفته است.");
        ////////        return;
        ////////    }
        ////////    if (WorkFlowStateManager.Count > 0)
        ////////    {
        ////////        int count = WorkFlowStateManager.Count;
        ////////        for (int i = 0; i < count; i++)
        ////////        {
        ////////            WorkFlowStateManager[0].Delete();
        ////////            if (WorkFlowStateManager.Save() <= 0)
        ////////            {
        ////////                TransactionManager.CancelSave();
        ////////                SetMessage("خطایی در حذف انجام گرفته است.");
        ////////                return;
        ////////            }
        ////////            WorkFlowStateManager.DataTable.AcceptChanges();
        ////////        }
        ////////    }

        ////////    #endregion

        ////////    TransactionManager.EndSave();
        ////////    GridViewSeminar.DataBind();
        ////////}
        ////////catch (Exception ex)
        ////////{
        ////////    TransactionManager.CancelSave();
        ////////    SetDeleteError(ex);
        ////////}
        //else
        //    Response.Redirect("AddSeminar.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));

        //if (GridViewMemberFile.FocusedRowIndex > -1)
        //{
        //    DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        //    if (MeFileRow != null)
        //    {
        //        int MeId = int.Parse(MeFileRow["MeId"].ToString());
        //        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
        //        if (dtDocMeFile.Rows.Count > 0)
        //        {
        //            int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        //            if (CheckPermitionForDelete(MfId))
        //            {
        //                DeleteRequest(MfId, MeId);
        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "برای شما امکان لغو درخواست وجود ندارد.";
        //            }
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان لغو درخواست وجود ندارد.";
        //        }
        //    }
        //}

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SeId = -1;
        int SeReqId = -2;
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            SeId = (int)row["SeId"];
        }
        if (SeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
        if (GridRequest != null)
        {
            if (GridRequest.VisibleRowCount > 0)
            {
                int index0 = GridRequest.FocusedRowIndex;
                if (index0 != -1)
                {
                    SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                    CheckWorkFlowPermission(SeId, SeReqId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewSeminar.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.Seminar;
            DataRow SeminarRow = GridViewSeminar.GetDataRow(GridViewSeminar.FocusedRowIndex);
            int TableId = int.Parse(SeminarRow["SeId"].ToString());

            string PgName = Utility.EncryptQS("Seminar.aspx");
            Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                  + "&PgName=" + PgName);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {

        int SeReqId = -1;
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        SeReqId = int.Parse(GridRequest.GetDataRow(index0)["SeReqId"].ToString());
                        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
                        if (e.Parameter == "Send")
                        {
                            SendSeminarDocToNextStep(SeReqId);
                        }
                        else
                        {
                            SelectSendBackTask(TableType, SeReqId);
                        }
                    }
                    else
                    {
                        lblError.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                        return;
                    }
                }
                else
                {
                    lblError.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                lblError.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            lblError.Text = "خطایی در بازیابی اطلاعات رخ داده است.";
        }
    }

    protected void GridViewSeminar_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewSeminar.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewSeminar.Columns["WFState"], "btnWFState");
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

    protected void GridViewSeminar_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["SeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("SeId");
        int Index = GridViewSeminar.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewSeminar.FocusedRowIndex = Index;
    }

    protected void GridViewRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewSeminar.FindDetailRowTemplateControl(GridViewSeminar.FocusedRowIndex, "GridViewRequest");
            if (GridViewRequest == null)
                return;
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
    #endregion

    #region Methods
    private void SetDeleteError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                ShowMessage("خطایی در حذف انجام گرفته است.");
            }
        }
        else
        {
            ShowMessage("خطایی در حذف انجام گرفته است.");
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #region WorkflowMethods
    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.SeminarRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    private void CheckWorkFlowPermission(int SeId, int SeReqId)
    {
        int PermissionEdit = -1;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        PermissionEdit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, (int)TSP.DataManager.WorkFlows.SeminarConfirming, SeReqId, TaskCode, Utility.GetCurrentUser_UserId());
        if (PermissionEdit > 0)
        {
            Response.Redirect("AddSeminar.aspx?SeId=" + Utility.EncryptQS(SeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&SeReqId=" + Utility.EncryptQS(SeReqId.ToString()));
        }
        else
        {
            SetMessage("تنها در مرحله تقاضای برگزاری سمینار و در صورت داشتن دسترسی گردش کار قادر به ویرایش اطلاعات  می باشید ");
        }

    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SelectSendBackTask(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
            WorkFlowManager.FindByTableType(TableType, CurrentWorkFlowCode);
            if (WorkFlowManager.Count > 0)
            {
                int SendBackTask = WorkFlowStateManager.CalculateSendBackTask(TableType, TableId, Utility.GetCurrentUser_UserId());
                switch (SendBackTask)
                {
                    case -1:
                        PanelSaveSuccessfully.Visible = true;
                        PanelMain.Visible = false;
                        lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
                        lblPeriodWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                        break;
                    case -2:
                        PanelSaveSuccessfully.Visible = true;
                        PanelMain.Visible = false;
                        lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
                        lblPeriodWarning.Text = "جریان کار پرونده سمینار انتخاب شده به اتمام رسیده است.";
                        break;
                    case -3:
                        PanelSaveSuccessfully.Visible = true;
                        PanelMain.Visible = false;
                        lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
                        lblPeriodWarning.Text = "عملیاتی برای پرونده سمینار انتخاب شده انجام نشده است.";
                        break;
                    default:
                        int WorkFlowId = int.Parse(WorkFlowManager[0]["WorkFlowId"].ToString());
                        DataTable dtSendBackTask = WorkFlowStateManager.SelectSendBackTask(SendBackTask, WorkFlowId);
                        if (dtSendBackTask.Rows.Count > 0)
                        {
                            Session["SendBackDataTable_Seminar"] = dtSendBackTask;
                            cmbSendBackTask.DataSource = dtSendBackTask;
                            cmbSendBackTask.ValueField = "TaskId";
                            cmbSendBackTask.TextField = "TaskName";
                            cmbSendBackTask.DataBind();
                            cmbSendBackTask.SelectedIndex = 0;
                            PanelSaveSuccessfully.Visible = false;
                            PanelMain.Visible = true;
                        }
                        break;
                }
            }
            else
            {
                PanelSaveSuccessfully.Visible = true;
                PanelMain.Visible = false;
                lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
                lblPeriodWarning.Text = "اطلاعات جریان کار تغییر یافته است.";
            }
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.Visible = false;
            lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
            lblPeriodWarning.Text = "عملیاتی برای پرونده سمینار انتخاب شده انجام نشده است.";
        }
    }

    private void SendSeminarDocToNextStep(int SeReqId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.SeminarConfirming;
        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        int RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectSeminarAndEndProccess;
        int ConfirmProccessTaskId = -1;
        int NextTaskId = -1;
        int RejectProccessTaskId = -1;

        DataTable dt = WorkFlowTaskManager.SelectNextSteps(TableType, SaveInfoTaskCode, WorkflowCode);
        if (dt.Rows.Count > 0)
        {
            NextTaskId = int.Parse(dt.Rows[0]["TaskId"].ToString());
        }
        int ConfirmProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmSeminarAndEndProccess;
        WorkFlowTaskManager.FindByTaskCode(ConfirmProccessTaskCode);
        if (WorkFlowTaskManager.Count == 1)
        {
            ConfirmProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        }
        WorkFlowTaskManager.FindByTaskCode(RejectProccessTaskCode);
        if (WorkFlowTaskManager.Count == 1)
        {
            RejectProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        }
        DataTable dtSendBack = (DataTable)Session["SendBackDataTable_Seminar"];
        cmbSendBackTask.DataSource = dtSendBack;
        cmbSendBackTask.ValueField = "TaskId";
        cmbSendBackTask.TextField = "TaskName";
        cmbSendBackTask.DataBind();
        int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
        if (SelectedTaskId != ConfirmProccessTaskId && SelectedTaskId != RejectProccessTaskId && SelectedTaskId == NextTaskId)
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            int NmcId = FindNmcId();

            int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, SeReqId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId());
            switch (SendDoc)
            {
                case -6:
                    DivReport.Visible = true;
                    LabelWarning.Text = "امکان ارسال پرونده سمینار به مرحله جاری وجود ندارد.";
                    break;
                case -4:
                    lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "خطایی در ذخیره انجام شد.";
                    break;
                case -5:
                    DivReport.Visible = true;
                    LabelWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                    break;
                default:
                    lblPeriodWarning.ForeColor = System.Drawing.Color.Green;
                    lblError.Text = "ذخیره انجام شد.";
                    PanelMain.ClientVisible = false;
                    PanelSaveSuccessfully.ClientVisible = true;
                    break;
            }
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.Visible = false;
            lblPeriodWarning.ForeColor = System.Drawing.Color.Red;
            lblPeriodWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";

        }
        GridViewSeminar.DataBind();
    }

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }
    #endregion
    #endregion
}
