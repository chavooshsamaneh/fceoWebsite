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

public partial class Employee_Amoozesh_MemberResearchActivity : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session["SendBackDataTable_Research"] = "";

            TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanDelete;
            btnInActive2.Enabled = per.CanDelete;
            GridViewResearchAct.ClientVisible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = GridViewResearchAct.ClientVisible = (bool)this.ViewState["BtnView"];


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
        if (GridViewResearchAct.FocusedRowIndex > -1)
        {
            DataRow ResearchRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
            int MraId = int.Parse(ResearchRow["MraId"].ToString());
            TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
            DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(MraId);
            if (dtTrainingJudg.Rows.Count > 0)
            {
                int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
                int TrainingJudgmentTableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;

                int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
                int WorkflowCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;
                int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberResearchAct;
                int ComittieConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingMemberResearchAct;
                TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SaveInfoTaskCode, WorkflowCode, JudgeId, Utility.GetCurrentUser_UserId());
                TSP.DataManager.WFPermission WFPerCommitee = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ComittieConfirmingTaskCode, WorkflowCode, JudgeId, Utility.GetCurrentUser_UserId());
                if (WFPer.BtnEdit)
                    NextPage("Edit");
                else if (WFPerCommitee.BtnEdit)
                    NextPage("Judge");
                else
                {

                    DivReport.Visible = true;
                    LabelWarning.Text = "در این مرحله از گردش کار امکان ویرایش اطلاعات وجود ندارد.";
                    return;
                }
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "برای ردیف انتخاب شده درخواستی ایجاد نشده است.";
            }
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");

        //  if (GridViewResearchAct.FocusedRowIndex > -1)
        // {
        //DataRow ResearchRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
        //int MraId = int.Parse(ResearchRow["MraId"].ToString());
        //TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        //DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(MraId);
        //if (dtTrainingJudg.Rows.Count > 0)
        //{
        //    int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
        //    int TrainingJudgmentTableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;

        //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //    int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
        //    int WorkflowCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;
        //    int CommitteeConfirmingMemberResearchActTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingMemberResearchAct;
        //    int CommitteeConfirmingTaskId = -1;


        //    WorkFlowTaskManager.FindByTaskCode(CommitteeConfirmingMemberResearchActTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        CommitteeConfirmingTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastState(TableType, JudgeId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == CommitteeConfirmingTaskId)
        //        {
        //            NextPage("Judge");
        //        }
        //        else
        //        {
        //    NextPage("View");
        //     }
        //}
        //else
        //{
        //    DivReport.Visible = true;
        //    LabelWarning.Text = "جریان کاری برای پرونده انتخاب شده تعریف نشده است.";
        //}
        //    }
        //    else
        //    {
        //        DivReport.Visible = true;
        //        LabelWarning.Text = "برای ردیف انتخاب شده درخواستی ایجاد نشده است.";
        //    }
        //}

    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int MraId = -1;
        //    if (GridViewResearchAct.FocusedRowIndex > -1)
        //    {
        //        if (Session["FillMeResearch"] != null)
        //        {
        //            GridViewResearchAct.DataSource = (DataTable)Session["FillMeResearch"];
        //            GridViewResearchAct.DataBind();
        //        }
        //        else
        //            FillGrid();

        //        DataRow row = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
        //        MraId = (int)row["MraId"];
        //    }
        //    if (MraId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //    }
        //    else
        //    {
        //        TSP.DataManager.MemberResearchActivityManager ReManager = new TSP.DataManager.MemberResearchActivityManager();
        //        TSP.DataManager.MemberResearchActivityManager MeRaManager2 = new TSP.DataManager.MemberResearchActivityManager();


        //        ReManager.FindByCode(MraId);
        //        if (ReManager.Count == 1)
        //        {
        //            try
        //            {
        //                int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        //                ReManager[0].BeginEdit();
        //                ReManager[0]["InActive"] = 1;
        //                ReManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        //                ReManager[0].EndEdit();

        //                int cn = ReManager.Save();
        //                if (cn == 1)
        //                {
        //                    Session["FillMeResearch"] = MeRaManager2.FindByMeRequest(MeId, -1, -1);
        //                    GridViewResearchAct.DataSource = (DataTable)Session["FillMeResearch"];
        //                    GridViewResearchAct.DataBind();

        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "ذخیره انجام شد";


        //                }
        //                else
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

        //                }

        //            }
        //            catch (Exception err)
        //            {

        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //            }

        //        }
        //    }
        //}
        //catch (Exception)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        //}
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        // NextPage("ChangeRequest");
        if (GridViewResearchAct.FocusedRowIndex > -1)
        {
            DataRow ResearchRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
            int MraId = int.Parse(ResearchRow["MraId"].ToString());
            int MeId = int.Parse(ResearchRow["MeId"].ToString());
            TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
            TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
            DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(MraId);
            if (dtTrainingJudg.Rows.Count > 0)
            {
                int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
                int IsConfirm = -1;
                int JudgeGrade = -1;
                string JudgeViewPoint = "";
                if (!Utility.IsDBNullOrNullValue(dtTrainingJudg.Rows[0]["IsConfirmed"]))
                {
                    IsConfirm = int.Parse(dtTrainingJudg.Rows[0]["IsConfirmed"].ToString());
                    JudgeGrade = int.Parse(dtTrainingJudg.Rows[0]["JudgeGrade"].ToString());
                    JudgeViewPoint = dtTrainingJudg.Rows[0]["JudgeViewPoint"].ToString();
                    if (IsConfirm != 2)
                    {
                        int TrainingJudgmentTableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;

                        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
                        TransactionManager.Add(TrainingJudgmentManager);
                        TransactionManager.Add(WorkFlowStateManager);
                        WorkFlowTaskManager.ClearBeforeFill = true;
                        WorkFlowStateManager.ClearBeforeFill = true;
                        int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
                        int WorkflowCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;
                        int EndConfirmingMemberResearchActTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmMemberResearchActAndEndProccess;
                        int EndConfirmingTaskId = -1;

                        int RejectConfirmingMemberResearchActTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectMemberResearchActAndEndProcess;
                        int RejectConfirmingTaskId = -1;

                        int CommitteeConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingMemberResearchAct;
                        int CommitteeConfirmingTaskId = -1;

                        WorkFlowTaskManager.FindByTaskCode(CommitteeConfirmingTaskCode);
                        if (WorkFlowTaskManager.Count == 1)
                        {
                            CommitteeConfirmingTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }

                        WorkFlowTaskManager.FindByTaskCode(EndConfirmingMemberResearchActTaskCode);
                        if (WorkFlowTaskManager.Count == 1)
                        {
                            EndConfirmingTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }

                        WorkFlowTaskManager.FindByTaskCode(RejectConfirmingMemberResearchActTaskCode);
                        if (WorkFlowTaskManager.Count == 1)
                        {
                            RejectConfirmingTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }
                        int SaveInfoTaskId = -1;
                        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberResearchAct);
                        if (WorkFlowTaskManager.Count == 1)
                        {
                            SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                        }
                        DataTable dtWFState = WorkFlowStateManager.SelectLastState(TableType, JudgeId);
                        if (dtWFState.Rows.Count > 0)
                        {
                            int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
                            if (CurrentTskId == EndConfirmingTaskId || CurrentTskId == RejectConfirmingTaskId)
                            {
                                TransactionManager.BeginSave();

                                DataRow JudgeRow = TrainingJudgmentManager.NewRow();
                                JudgeRow["PkId"] = MraId;
                                JudgeRow["CreateDate"] = Utility.GetDateOfToday();
                                JudgeRow["JudgeGrade"] = JudgeGrade;
                                JudgeRow["IsConfirmed"] = 2;
                                JudgeRow["Type"] = 2;
                                JudgeRow["UserId"] = Utility.GetCurrentUser_UserId();
                                JudgeRow["ModifiedDate"] = DateTime.Now;

                                TrainingJudgmentManager.AddRow(JudgeRow);
                                if (TrainingJudgmentManager.Save() > 0)
                                {
                                    int NmcId = FindNmcId(SaveInfoTaskId);
                                    if (NmcId < 0)
                                    {
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "شما دسترسی گردش کار جهت ثبت اطلاعات را ندارید.";
                                        return;
                                    }
                                    // NextPage("Judge");
                                    DataRow WFStateRow = WorkFlowStateManager.NewRow();
                                    WFStateRow["TaskId"] = CommitteeConfirmingTaskId;
                                    WFStateRow["TableId"] = TrainingJudgmentManager[0]["JudgeId"];
                                    WFStateRow["NmcIdType"] = 0;
                                    WFStateRow["NmcId"] = NmcId;
                                    WFStateRow["SubOrder"] = 1;
                                    WFStateRow["StateType"] = 0;
                                    WFStateRow["Description"] = "درخواست بررسی مجدد تالیفات و تحقیقات";
                                    WFStateRow["Date"] = Utility.GetDateOfToday();
                                    WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                    WFStateRow["ModifiedDate"] = DateTime.Now;

                                    WorkFlowStateManager.AddRow(WFStateRow);
                                    int count = WorkFlowStateManager.Save();
                                    if (count > 0)
                                    {

                                        TransactionManager.EndSave();
                                        Response.Redirect("AddMemberResearchActivity.aspx?MraId=" + Utility.EncryptQS(MraId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
                                        //  DivReport.Visible = true;
                                        //  LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                                    }
                                    else
                                    {

                                        TransactionManager.CancelSave();
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    DivReport.Visible = true;
                                    LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                                }
                            }
                            //else if (CurrentTskId == RejectConfirmingTaskId)
                            //{

                            //}
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "جریان کاری برای پرونده انتخاب شده تعریف نشده است.";
                        }
                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "امکان درخواست بررسی مجدد وجود ندارد.وضعیت درخواست ردیف انتخاب شده در جریان می باشد.";
                        return;
                        //پرونده در جریان می باشد.
                    }

                }

            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "برای ردیف انتخاب شده درخواستی ایجاد نشده است.";
            }
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewResearchAct.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex)["MraId"].ToString());
            string GridFilterString = GridViewResearchAct.FilterExpression;
            int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
            DataRow ReseRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
            TSP.WebControls.CustomAspxDevGridView GridViewTrainingJudgment = (TSP.WebControls.CustomAspxDevGridView)GridViewResearchAct.FindDetailRowTemplateControl(GridViewResearchAct.FocusedRowIndex, "GridViewTrainingJudgment");
            if (GridViewTrainingJudgment != null)
            {
                DataRow JudgeRow = GridViewTrainingJudgment.GetDataRow(GridViewTrainingJudgment.FocusedRowIndex);
                if (JudgeRow != null)
                {

                    int TableId = int.Parse(JudgeRow["JudgeId"].ToString());
                    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;
                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                  "&PostId=" + Utility.EncryptQS(PostId.ToString());


                   // Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
                    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "درخواست مورد نظر را انتخاب نمایید.";
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }      
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "MembersReacherchActivity";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewResearchAct_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case"Print":

                    GridViewResearchAct.JSProperties["cpPrint"] = 1;
                    GridViewResearchAct.DetailRows.CollapseAllRows();
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewResearchAct.Columns;
                    Session["DataSource"] = OdbMemResearch;

                    Session["Title"] = "تحقیقات و تالیفات اعضاء";
                    break;
            }
        }
        else
        {
            GridViewResearchAct.DataBind();
            GridViewResearchAct.DetailRows.ExpandRow(GridViewResearchAct.FocusedRowIndex);
        }
    }

    protected void GridViewTrainingJudgment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["MraId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewResearchAct_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewResearchAct.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewTrainingJudgment_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "MeetingId" || e.DataColumn.FieldName == "MeetingDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewTrainingJudgment = (DevExpress.Web.ASPxGridView)GridViewResearchAct.FindDetailRowTemplateControl(GridViewResearchAct.FocusedRowIndex, "GridViewTrainingJudgment");
            if (GridViewTrainingJudgment == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewTrainingJudgment.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewTrainingJudgment.Columns["WFState"], "btnWFState");
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

    protected void GridViewTrainingJudgment_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "MeetingId" || e.Column.FieldName == "MeetingDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewResearchAct.FocusedRowIndex > -1)
        {
            DataRow ResearchRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);
            int MraId = int.Parse(ResearchRow["MraId"].ToString());
            TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
            DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(MraId);
            if (dtTrainingJudg.Rows.Count > 0)
            {
                int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
                int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;

                int WfCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;

                WFUserControl.PerformCallback(JudgeId, TableType, WfCode, e);
                GridViewResearchAct.DataBind();
            }
            else
            {
                WFUserControl.SetMsgText("برای ردیف انتخاب شده درخواستی ایجاد نشده است.");
            }
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int MraId = -1;
        int MeId = -1;
        int focucedIndex = GridViewResearchAct.FocusedRowIndex;
        //if (Mode == "View" || Mode == "Judge")
        //{
        //    if (GridViewResearchAct.FocusedRowIndex > -1)
        //    {
        //        DataRow row = GridViewResearchAct.GetDataRow(focucedIndex);
        //        MeId = (int)row["MeId"];
        //        TSP.WebControls.CustomAspxDevGridView GridViewTrainingJudgment = (TSP.WebControls.CustomAspxDevGridView)GridViewResearchAct.FindDetailRowTemplateControl(GridViewResearchAct.FocusedRowIndex, "GridViewTrainingJudgment");
        //        if (GridViewTrainingJudgment != null)
        //        {
        //            focucedIndex = GridViewTrainingJudgment.FocusedRowIndex;
        //            if (focucedIndex > -1)
        //            {
        //                DataRow rowJudge = GridViewTrainingJudgment.GetDataRow(focucedIndex);
        //               // int JudgeId = (int)rowJudge["JudgeId"];
        //                MraId = (int)rowJudge["PKId"];

        //            }
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
        //            return;
        //        }
        //    }
        //}
        //else
        //{
        if (focucedIndex > -1)
        {
            DataRow row = GridViewResearchAct.GetDataRow(focucedIndex);
            MraId = (int)row["MraId"];
            MeId = (int)row["MeId"];
        }
        // }
        if (MraId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                MraId = -1;
                Response.Redirect("AddMemberResearchActivity.aspx?MraId=" + Utility.EncryptQS(MraId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
            }
            else
            {
                Response.Redirect("AddMemberResearchActivity.aspx?MraId=" + Utility.EncryptQS(MraId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
            }
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
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;

        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
        //LoginManager.FindByCode(UserId);

        //if (LoginManager.Count > 0)
        //{
        //    int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
        //    int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
        //    NezamChartManager.FindByEmpId(EmpId, UltId);
        //    if (NezamChartManager.Count > 0)
        //    {
        //        NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
        //    }
        //    else
        //    {
        //        DivReport.Visible = true;
        //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
        //    }
        //}
        //else
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return (-1);
        //}
        //return (NmcId);
    }
    #endregion

}
