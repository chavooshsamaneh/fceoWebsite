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

public partial class Members_ImplementDoc_ImplementDoc : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        BtnNew.Visible = btnNew2.Visible = btnEdit.Visible = btnEdit2.Visible = btnChange.Visible = btnChange2 .Visible= btnDelete.Visible = btnDelete2.Visible = btnReDuplicate.Visible
            = btnReDuplicate2.Visible = btnRevival.Visible = btnRevival2.Visible = btnSendNextStep.Visible = btnSendNextStep2.Visible = false;
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
            Session["SendBackDataTable_ImpDoc"] = "";
            ObjdsMemberFile.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            SetPageFilter();
            SetGridRowIndex();
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming).ToString();
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count == 0)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");

        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewMemberFile.Columns;
        Session["DataSource"] = ObjdsMemberFile;
        Session["Title"] = "مجوز مجری حقیقی";
        Session["Header"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString() + " (" + "کد عضویت: " + MemberManager[0]["MeId"].ToString() + ")";

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

        if (!CheckConditions(Utility.GetCurrentUser_MeId()))
            return;

        int MeId = Utility.GetCurrentUser_MeId();
        DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
        if (dtObsDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("بدلیل داشتن مجوز نظارت،قادر به دریافت مجوز اجرا نمی باشید");
            return;
        }

        DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        if (dtImpDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("پیش از این برای شما مجوز اجرا تعریف شده است.");
            return;
        }
        NextPage("New");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = -1;
        int focucedIndex = GridViewMemberFile.FocusedRowIndex;

        if (focucedIndex <= -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت ویرایش اطلاعات یک  رکورد را انتخاب نمایید.";
            return;
        }
        DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
        MfId = (int)row["MfId"];
        int RequesterType = int.Parse(row["RequesterType"].ToString());
        int IsConfirm = int.Parse(row["IsConfirm"].ToString());
        if (IsConfirm != 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بدلیل پایان روند بررسی درخواست انتخاب شده امکان ویرایش اطلاعات مربوط به آن وجود ندارد";
            return;
        }
        if (RequesterType != (int)TSP.DataManager.DocumentRequesterType.Member)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بدلیل ثبت درخواست توسط کارمندان سازمان شما قادر به ویرایش اطلاعات نمی باشید.";
            return;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            return;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
            return;
        }
        if (!CheckPermitionForEdit(MfId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.";
            return;
        }
        NextPage("Edit");
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);// (int)TSP.DataManager.TableCodes.DocMemberFileImp;
            DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            int PostId = int.Parse(GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"].ToString());
            string GridFilterString = GridViewMemberFile.FilterExpression;
            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" +
                "&PostId=" + Utility.EncryptQS(PostId.ToString());
            Response.Redirect("../WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
            ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
    }

    protected void btnReDuplicate_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

        int MeId = Utility.GetCurrentUser_MeId();
        DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
        if (dtObsDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("بدلیل داشتن مجوز نظارت،قادر به دریافت مجوز اجرا نمی باشید");
            return;
        }

        DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        if (dtImpDoc.Rows.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما دارای مجوز اجرا نمی باشید.";
            return;
        }
        NextPage("ReDuplicate");
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MeId = Utility.GetCurrentUser_MeId();
        DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
        if (dtObsDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("بدلیل داشتن مجوز نظارت،قادر به دریافت مجوز اجرا نمی باشید");
            return;
        }

        DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        if (dtImpDoc.Rows.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما دارای مجوز اجرا نمی باشید.";
            return;
        }
        NextPage("Revival");
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MeId = Utility.GetCurrentUser_MeId();
        DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
        if (dtObsDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("بدلیل داشتن مجوز نظارت،قادر به دریافت مجوز اجرا نمی باشید");
            return;
        }

        DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        if (dtImpDoc.Rows.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای مجوز اجرا نمی باشید.");
            return;
        }
        NextPage("Change");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if ((bool)MemberManager[0]["IsLock"] == true)
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;
        }
        int MfId = -1;
        int focucedIndex = GridViewMemberFile.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];
            if (CheckPermitionForDelete(MfId))
            {
                DeleteRequest(MfId);
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان لغو درخواست برای شما وجود ندارد.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت  لغو درخواست یک  رکورد را انتخاب نمایید.";
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        // GridViewMemberFile.Columns["WFState"].Visible = false;
        GridViewExporter.FileName = "ImplementDocument";

        GridViewExporter.WriteXlsToResponse(true);
        // GridViewMemberFile.Columns["WFState"].Visible = true;
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int MfId = int.Parse(MeFileRow["MfId"].ToString());
            int MeFileTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);// (int)TSP.DataManager.TableCodes.DocMemberFile;
            if (e.Parameter == "Send")
            {
                SendMemberFileDocToNextStep(MfId);
                GridViewMemberFile.DataBind();
            }
            else
            {
                SelectSendBackTask(MeFileTableType, MfId);
            }
        }
        else
        {
            lblError.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
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

    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberFile_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();

    }
    #endregion

    #region Methods
    //private void NextPage(string Mode)
    //{
    //    int MfId = -1;
    //    int focucedIndex = GridViewMemberFile.FocusedRowIndex;

    //    if (focucedIndex > -1)
    //    {
    //        DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
    //        MfId = (int)row["MfId"];
    //    }
    //    if (MfId == -1 && Mode != "New")
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
    //    }
    //    else
    //    {
    //        string GridFilterString = GridViewMemberFile.FilterExpression;

    //        if (Mode == "New")
    //        {
    //            MfId = -1;
    //            Response.Redirect("DocumentsRules.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
    //            //  Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
    //        }
    //        else
    //        {
    //            Response.Redirect("AddImplementDoc.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
    //        }
    //    }
    //}

    private void NextPage(string Mode)
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1 && (Mode == "View" || Mode == "Edit"))
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        int MfId = -1;
        int MeId = -1;
        if (Mode != "New")
        {
            int focucedIndex = GridViewMemberFile.FocusedRowIndex;
            DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
            //***ImpDocId
            MfId = (int)row["MfId"];
            //***MfId
            MeId = (int)row["MeId"];
        }
        if (MfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New") MfId = -1;
            Response.Redirect("AddImplementDoc.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
        }
    }

    private void SelectSendBackTask(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskOrder = int.Parse(dtWorkFlowLastState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowLastState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());

                    if (FirstNmcIdType == 1)
                    {
                        if (FirstNmcId == Utility.GetCurrentUser_MeId())
                        {
                            DocMemberFileManager.FindByCode(TableId, 1);
                            if (DocMemberFileManager.Count == 1)
                            {
                                DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, DocMeFileSaveInfoTaskCode, CurrentWorkFlowCode);
                                if (dtNextTopTask.Rows.Count > 0)
                                {
                                    Session["SendBackDataTable_ImpDoc"] = dtNextTopTask;
                                    cmbSendBackTask.DataSource = dtNextTopTask;
                                    cmbSendBackTask.ValueField = "TaskId";
                                    cmbSendBackTask.TextField = "TaskName";
                                    cmbSendBackTask.DataBind();
                                    cmbSendBackTask.SelectedIndex = 0;
                                    PanelSaveSuccessfully.Visible = false;
                                    PanelMain.Visible = true;
                                }
                                else
                                {
                                    PanelSaveSuccessfully.ClientVisible = true;
                                    PanelMain.ClientVisible = false;
                                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                    lblInstitueWarning.Text = "عملیات بعد در جریان کار نامشخص است.";
                                }
                            }
                            else
                            {
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                lblInstitueWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                            }
                        }
                        else
                        {
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                        }
                    }
                    else
                    {
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                        lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                    }
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
                }
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            }
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
        }
    }

    private void SendMemberFileDocToNextStep(int MfId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp); //(int)TSP.DataManager.TableCodes.DocMemberFile;
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        int MeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int NextStepTaskId = -1;

        DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, MeFileSaveInfoTaskCode, (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming);
        if (dtNextTopTask.Rows.Count > 0)
        {
            int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
            WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            DataTable dtSendBack = (DataTable)Session["SendBackDataTable_ImpDoc"];
            cmbSendBackTask.DataSource = dtSendBack;
            cmbSendBackTask.ValueField = "TaskId";
            cmbSendBackTask.TextField = "TaskName";
            cmbSendBackTask.DataBind();

            int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
            if (SelectedTaskId == NextStepTaskId)
            {
                TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
                //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

                TransactionManager.Add(WorkFlowStateManager);
                //  TransactionManager.Add(DocMemberFileManager);
                // DocMemberFileManager.ClearBeforeFill = true;

                int NmcId = Utility.GetCurrentUser_MeId();
                int NmcIdType = 1;
                if (NmcId > 0)
                {
                    TransactionManager.BeginSave();
                    string Url = "<a href='../Document/AddMemberFile.aspx?MeId=" + "" + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
                    string MsgContent = "";
                    int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, MfId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url);
                    switch (SendDoc)
                    {
                        case -6:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده پروانه به مرحله جاری وجود ندارد.";
                            break;
                        case -4:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                            break;
                        case -5:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                            break;
                        case -8:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                            break;
                        default:
                            TransactionManager.EndSave();
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
                            lblInstitueWarning.Text = "ذخیره انجام شد.";
                            PanelMain.ClientVisible = false;
                            PanelSaveSuccessfully.ClientVisible = true;
                            GridViewMemberFile.DataBind();
                            break;
                    }
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
                }

            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";

            }
            GridViewMemberFile.DataBind();
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مرحله بعد جریان کار نا مشخص است.";
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
        if (WorkFlowTaskManager.Count <= 0)
            return false;
        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        if (TaskOrder == 0)
            return false;
        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count <= 0)
            return false;
        int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
        int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
        int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
        int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
        if (CurrentTaskCode != SaveInfoTaskCode)
        {
            return false;
        }
        return true;
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
                DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtState.Rows.Count == 1)
                {
                    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;

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
                                if (FirstNmcId == Utility.GetCurrentUser_MeId() && FirstNmcIdType == 1)
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

    //private void DeleteRequest(int MfId)
    //{
    //    TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
    //    TSP.DataManager.DocOffJobHistoryQualityManager DocOffJobHistoryQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
    //    TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
    //    TransactionManager.Add(FinManager);
    //    TransactionManager.Add(DocMemberFileManager);
    //    TransactionManager.Add(WorkFlowStateManager);
    //    TransactionManager.Add(attachManager);
    //    TransactionManager.Add(ProjectJobHistoryManager);
    //    TransactionManager.Add(DocOffJobHistoryQualityManager);
    //    try
    //    {
    //        TransactionManager.BeginSave();

    //        #region Delete JobHistory
    //        ProjectJobHistoryManager.FindForDelete(0, MfId, (int)TSP.DataManager.TableCodes.DocMemberFile);
    //        if (ProjectJobHistoryManager.Count > 0)
    //        {
    //            int C = ProjectJobHistoryManager.Count;
    //            for (int i = 0; i < C; i++)
    //            {
    //                int JhId = (int)ProjectJobHistoryManager[i]["JhId"];
    //                DataTable dtJobQuality = DocOffJobHistoryQualityManager.FindByJobCode(JhId);
    //                if (dtJobQuality.Rows.Count > 0)
    //                {
    //                    for (int j = 0; j < dtJobQuality.Rows.Count; i++)
    //                    {
    //                        int JhqId = (int)dtJobQuality.Rows[j]["JhqId"];
    //                        DocOffJobHistoryQualityManager.FindByCode(JhqId);
    //                        if (DocOffJobHistoryQualityManager.Count == 1)
    //                        {
    //                            DocOffJobHistoryQualityManager[0].Delete();
    //                        }
    //                        if (DocOffJobHistoryQualityManager.Save() <= 0)
    //                        {
    //                            TransactionManager.CancelSave();
    //                            this.DivReport.Visible = true;
    //                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
    //                            return;
    //                        }
    //                    }
    //                }
    //                ProjectJobHistoryManager[i].Delete();
    //            }
    //            if (ProjectJobHistoryManager.Save() < 0)
    //            {
    //                TransactionManager.CancelSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
    //                return;
    //            }
    //        }
    //        #endregion

    //        #region Delete Financial Status
    //        FinManager.SelectForImplementDoc(MfId);
    //        if (FinManager.Count > 0)
    //        {
    //            int c = FinManager.Count;
    //            for (int i = 0; i < c; i++)
    //                FinManager[0].Delete();

    //            FinManager.Save();
    //        }
    //        #endregion

    //        #region Delete Attachment
    //        attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, MfId);
    //        if (attachManager.Count > 0)
    //        {
    //            int c = attachManager.Count;
    //            for (int i = 0; i < c; i++)
    //                attachManager[0].Delete();

    //            attachManager.Save();
    //        }
    //        #endregion

    //        #region Delete WFState
    //        WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming, MfId);
    //        if (WorkFlowStateManager.Count > 0)
    //        {
    //            int count = WorkFlowStateManager.Count;
    //            for (int i = 0; i < count; i++)
    //            {
    //                WorkFlowStateManager[i].Delete();
    //            }
    //            if (WorkFlowStateManager.Save() <= 0)
    //            {
    //                TransactionManager.CancelSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
    //                return;
    //            }
    //        }

    //        #endregion

    //        #region Delete MemeberFile
    //        DocMemberFileManager.FindByCode(MfId, 1);
    //        if (DocMemberFileManager.Count == 1)
    //        {
    //            DocMemberFileManager[0].Delete();
    //            if (DocMemberFileManager.Save() > 0)
    //            {
    //                TransactionManager.EndSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "لغو در خواست انجام گرفت.";
    //            }
    //            else
    //            {
    //                TransactionManager.CancelSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            TransactionManager.CancelSave();
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
    //            return;
    //        }
    //        #endregion
    //        GridViewMemberFile.DataBind();
    //    }
    //    catch (Exception err)
    //    {
    //        TransactionManager.CancelSave();
    //        SetDeleteError(err);
    //    }
    //}

    private void DeleteRequest(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocOffJobHistoryQualityManager DocOffJobHistoryQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TransactionManager.Add(FinManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(attachManager);
        TransactionManager.Add(ProjectJobHistoryManager);
        TransactionManager.Add(DocOffJobHistoryQualityManager);
        try
        {
            TransactionManager.BeginSave();

            #region Delete JobHistory
            ProjectJobHistoryManager.FindForDelete(0, MfId, (int)TSP.DataManager.TableCodes.DocMemberFile);
            if (ProjectJobHistoryManager.Count > 0)
            {
                int C = ProjectJobHistoryManager.Count;
                for (int i = 0; i < C; i++)
                {
                    int JhId = (int)ProjectJobHistoryManager[i]["JhId"];
                    DataTable dtJobQuality = DocOffJobHistoryQualityManager.FindByJobCode(JhId);
                    if (dtJobQuality.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtJobQuality.Rows.Count; i++)
                        {
                            int JhqId = (int)dtJobQuality.Rows[j]["JhqId"];
                            DocOffJobHistoryQualityManager.FindByCode(JhqId);
                            if (DocOffJobHistoryQualityManager.Count == 1)
                            {
                                DocOffJobHistoryQualityManager[0].Delete();
                            }
                            if (DocOffJobHistoryQualityManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    ProjectJobHistoryManager[i].Delete();
                }
                if (ProjectJobHistoryManager.Save() < 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete Financial Status
            FinManager.SelectForImplementDoc(MfId);
            if (FinManager.Count > 0)
            {
                int c = FinManager.Count;
                for (int i = 0; i < c; i++)
                    FinManager[0].Delete();

                FinManager.Save();
            }
            #endregion

            #region Delete Attachment
            attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, MfId);
            if (attachManager.Count > 0)
            {
                int c = attachManager.Count;
                for (int i = 0; i < c; i++)
                    attachManager[0].Delete();

                attachManager.Save();
            }
            #endregion

            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming, MfId);
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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }

            #endregion

            #region Delete Letter
            LetterRelatedTablesManager.FindByTableIdTableType(MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp));
            if (LetterRelatedTablesManager.Count > 0)
            {
                int count = LetterRelatedTablesManager.Count;
                for (int i = 0; i < count; i++)
                {
                    LetterRelatedTablesManager[0].Delete();
                }
                if (LetterRelatedTablesManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete MemeberFile
            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].Delete();
                if (DocMemberFileManager.Save() > 0)
                {
                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لغو در خواست انجام گرفت.";
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                return;
            }
            #endregion
            GridViewMemberFile.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
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

    #region Set Grid Index

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMemberFile.FilterExpression = GrdFlt;
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

                GridViewMemberFile.DataBind();
                Index = GridViewMemberFile.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewMemberFile.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewMemberFile.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewMemberFile.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }
    #endregion

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    private bool CheckConditions(int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("کد عضویت وارد شده معتبر نمی باشد.");
            return false;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
            return false;
        }
        #region CheckAccounting
        if (Utility.CreateAccount())
        {
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AccId"]))
            {
                int AccId = int.Parse(MemberManager[0]["AccId"].ToString());
                decimal Balance = AccManager.GetAccountBalance(AccId, Utility.GetDateOfToday());
                if (Balance != 0)
                {
                    ShowMessage("مانده حساب عضو مورد نظر صفر نمی باشد.");
                    return false;
                }
            }
            else
            {
                ShowMessage("حساب عضو انتخاب شده نامشخص می باشد.");
                return false;
            }
        }
        #endregion

        #region Check OfficeMember
        OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
        if (OfMeManager.Count > 0)
        {
            int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
            DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
            if (dtEngOffReq.Rows.Count > 0)
            {
                string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در دفتر ";
                str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                str += " مشغول به کار می باشد";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = (str);
                return false;
            }
        }
        OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
        if (OfMeManager.Count > 0)
        {
            int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
            DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfIdMember, 0, MeId);
            if (dtOffReq.Rows.Count > 0)
            {
                string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در شرکت ";
                str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                str += " مشغول به کار می باشد";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = (str);
                return false;
            }
        }
        #endregion

        #region CheckMemberFile
        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        if (dtMeFile.Rows.Count <= 0)
        {
            ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.شما دارای پروانه اشتغال به کار تایید شده نمی باشد.");
            return false;
        }
        int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

        DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
        if (dtMeDetail.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نداشتن صلاحیت اجرا امکان درخواست مجوز اجرا برای شما وجود ندارد.");
            return false;
        }

        string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
        if (!string.IsNullOrEmpty(ExpireDate))
        {
            if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
            {
                ShowMessage("بدلیل اتمام مدت اعتبار پروانه اشتغال امکان درخواست مجوز اجرا برای شما وجود ندارد.");
                return false;
            }
        }

        if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
        {
            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count <= 0)
            {
                ShowMessage("رشته موضوع پروانه شما نامشخص است");
                return false;
            }
        }
        else
        {
            ShowMessage("امکان درخواست وجود ندارد.وضعیت پروانه اشتغال شما نامشخص می باشد.");
            return false;
        }
        #endregion

        return true;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.PortalImplementDocument).ToString());
    }
    #endregion
}
