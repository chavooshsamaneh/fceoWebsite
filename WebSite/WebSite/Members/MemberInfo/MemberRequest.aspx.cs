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

public partial class Members_MemberInfo_MemberRequest : System.Web.UI.Page
{
    #region Events
    private string IsMeTemp
    {
        get
        {
            return Utility.DecryptQS(HDpage["IsMeTemp"].ToString());
        }
        set
        {
            HDpage["IsMeTemp"] = Utility.EncryptQS(value.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["SendBackDataTable_MeRq"] = "";
            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.MemberConfirming);
            if (WorkFlowManager.Count == 1)
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowManager[0]["WorkFlowId"].ToString();

            int MeId = Utility.GetCurrentUser_MeId();
            MemberId.Value = Utility.EncryptQS(MeId.ToString());
            if (string.IsNullOrEmpty(MeId.ToString()))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = MeId.ToString();
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                IsMeTemp = "1";
            else IsMeTemp = "0";
            ObjectDataSource1.SelectParameters["IsMeTemp"].DefaultValue = IsMeTemp;

            try
            {
                if (IsMeTemp == "0")
                {
                    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                    MeManager.FindByCode(MeId);
                    if ((bool)MeManager[0]["IsLock"] == true)
                    {
                        TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                        string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";

                    }
                    else
                    {
                        if (Convert.ToInt32(MeManager[0]["MrsId"]) != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
                        {
                            btnDelete.Enabled= btnDelete2.Enabled =
                            BtnNew.Enabled = btnNew2.Enabled = 
                           btnEdit.Enabled= btnEdit2.Enabled = false;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به دلیل در جریان بودن وضعیت عضویت شما امکان ایجاد درخواست تغییرات جدید وجود ندارد";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    #region btnClick
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            SetMessage(MeReqResult[1].ToString());
            return;
        }
      
        try
        {
            string MeId = Utility.DecryptQS(MemberId.Value);
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            if (IsMeTemp == "1")
            {
                SetMessage("امکان ثبت درخواست جدید برای اعضای موقت وجود ندارد");
                return;
            }
            if (IsMeTemp == "0")
            {
                TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                MeManager.FindByCode(int.Parse(MeId));
                if ((bool)MeManager[0]["IsLock"] == true)
                {
                    TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                    string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                    return;
                }
            }

            ReqManager.FindByMemberId(int.Parse(MeId), 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            //if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(int.Parse(MeId)))
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد";
            //    return;
            //}
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
            return;
        }
        Response.Redirect("MemberRequestInsert.aspx?MReId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&MeId=" + MemberId.Value, true);
        return;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //Response.Redirect("MemberRequestInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + Utility.EncryptQS("View"));
        int MReId = -1;
        if (GridViewMemberRequest.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberRequest.GetDataRow(GridViewMemberRequest.FocusedRowIndex);
            MReId = (int)row["MReId"];
        }
        if (MReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        Response.Redirect("MemberRequestInsert.aspx?MReId=" + Utility.EncryptQS(MReId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&MeId=" + MemberId.Value);
    }

    protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            SetMessage(MeReqResult[1].ToString());
            return;
        }
        if (Utility.IsDBNullOrNullValue(IsMeTemp))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (IsMeTemp == "1")
        {
            SetMessage("امکان ثبت درخواست جدید برای اعضای موقت وجود ندارد");
            return;
        }
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        if (IsMeTemp == "0")
        {
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(int.Parse(MeId));
            if ((bool)MeManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return;
            }
        }

        ReqManager.FindByMemberId(int.Parse(MeId), 0, -1);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }

        if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(int.Parse(MeId)))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد";
            return;
        }
        Response.Redirect("MemberInsertBaseInfo.aspx?PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&UrlRef=" + Utility.EncryptQS("MemberRequest"));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Members/MemberHome.aspx?MeId=" + MemberId.Value);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        int MReId = -1;
        if (GridViewMemberRequest.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberRequest.GetDataRow(GridViewMemberRequest.FocusedRowIndex);
            MReId = (int)row["MReId"];
        }
        if (MReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            int TaskId = -1;
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            DataTable dtWF = WorkFlowStateManager.SelectLastState(TableType, MReId);
            if (int.Parse(dtWF.Rows[0]["TaskId"].ToString()) != TaskId)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "در مرحله جاری  گردش کار امکان لغو درخواست وجود ندارد";
                return;
            }
            if (dtWF.Rows[0]["NmcIdType"].ToString() == "1" && int.Parse(dtWF.Rows[0]["NmcId"].ToString()) == MeId)
            {
                ReqManager.FindByCode(MReId);
                if (ReqManager.Count > 0)
                {
                    if (ReqManager[0]["IsCreated"].ToString() == "0")
                    {
                        if (Convert.ToBoolean(ReqManager[0]["Requester"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان لغو درخواست برای شما وجود ندارد";
                            return;
                        }


                        if (ReqManager[0]["IsConfirm"].ToString() == "0")
                        {
                            if (CheckPermitionForDelete(MReId))
                            {
                                Delete(MeId, MReId);
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از گردش کار برای شما وجود ندارد";
                            }
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "وضعیت درخواست مورد نظر مشخص شده است.امکان لغو درخواست وجود ندارد";

                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان لغو رکورد مورد نظر وجود ندارد";

                    }

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان لغو درخواست برای شما وجود ندارد";
            }

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            SetMessage(MeReqResult[1].ToString());
            return;
        }
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();


        int MReId = -1;
        if (GridViewMemberRequest.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberRequest.GetDataRow(GridViewMemberRequest.FocusedRowIndex);
            MReId = (int)row["MReId"];
        }
        if (MReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;

        }
        try
        {
            int TaskId = -1; int TaskId2 = -1;
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            //int SaveInfoTaskCode2 = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
            //WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode2);
            //if (WorkFlowTaskManager.Count > 0)
            //{
            //    TaskId2 = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            //}
            DataTable dtWF = WorkFlowStateManager.SelectLastState(TableType, MReId);
            if (dtWF.Rows.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای درخواست مورد نظر گردش کاری تعریف نشده است.";
                return;
            }
            if (int.Parse(dtWF.Rows[0]["TaskId"].ToString()) != TaskId && int.Parse(dtWF.Rows[0]["TaskId"].ToString()) != TaskId2)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "در مرحله جاری گردش کار امکان ویرایش درخواست وجود ندارد";
                return;
            }
            // if (dtWF.Rows[0]["NmcIdType"].ToString() == "1" && int.Parse(dtWF.Rows[0]["NmcId"].ToString()) == MeId)
            // {
            ReqManager.FindByCode(MReId);
            if (ReqManager.Count > 0)
            {
                //if (ReqManager[0]["IsCreated"].ToString() == "0")
                //{
                if (Convert.ToBoolean(ReqManager[0]["Requester"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                    return;
                }
                if (ReqManager[0]["IsConfirm"].ToString() == "0")
                {
                    if (CheckPermitionForEdit(MReId))
                        Response.Redirect("MemberRequestInsert.aspx?MReId=" + Utility.EncryptQS(MReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + MemberId.Value);

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش درخواست پاسخ داده شده وجود ندارد";
                }
                //}
                //else
                //{
                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "امکان ویرایش درخواست مورد نظر وجود ندارد";
                //}
            }
            //}
            //else
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
            //}
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        //return;
        if (GridViewMemberRequest.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            DataRow Row = GridViewMemberRequest.GetDataRow(GridViewMemberRequest.FocusedRowIndex);
            int TableId = int.Parse(Row["MReId"].ToString());

            Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }
    #endregion

    //protected void GridViewMemberRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate")
    //        e.Cell.Style["direction"] = "ltr";

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberRequest.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            string WFName = "";
    //            if (!Utility.IsDBNullOrNullValue(e.GetValue("WorkFlowName")))
    //            {
    //                WFName = e.GetValue("WorkFlowName").ToString();
    //            }

    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            }

    //            if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            }
    //            else
    //            {
    //            }
    //        }
    //    }
    //}

    protected void GridViewMemberRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "AnswerDate" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMemberRequest.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberRequest.GetDataRow(GridViewMemberRequest.FocusedRowIndex);
            //int MeId = int.Parse(MeFileRow["MeId"].ToString());
            int MReId = int.Parse(MeFileRow["MReId"].ToString());
            int IsCreated = int.Parse(MeFileRow["IsCreated"].ToString());
            //int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
            if (IsCreated == (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince)
                WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
            if (e.Parameter == "Send")
            {
                SendMemberFileDocToNextStep(MReId, WFCode);
                GridViewMemberRequest.DataBind();
            }
            else
            {
                SelectSendBackTask(WFCode, MReId);
            }
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }
    #endregion

    #region Methods
    //********************************************Methods**********************************************************************************************************************
    protected void Delete(int MeId, int MReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);
        try
        {
            trans.BeginSave();
            ReqManager.DeleteRequest(MReId, MeId);

            int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, MReId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }
            trans.EndSave();
            GridViewMemberRequest.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();

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
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
    }

    #region WorkFlow

    private void SelectSendBackTask(int WFCode, int TableId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        //TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        CallbackPanelWorkFlow.JSProperties["cpWfName"] = "";
        CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "";

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            int CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
            string CurrentTaskName = dtWorkFlowState.Rows[0]["TaskName"].ToString();

            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            if (WFCode == (int)TSP.DataManager.WorkFlows.MemberTransferConfirming)
                SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;

            if (WFCode == (int)TSP.DataManager.WorkFlows.MemberConfirming)
                WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.MemberConfirming);
            else if (WFCode == (int)TSP.DataManager.WorkFlows.MemberTransferConfirming)
                WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.MemberTransferConfirming);

            if (WorkFlowManager.Count > 0)
            {
                CallbackPanelWorkFlow.JSProperties["cpWfName"] = "گردش کار:" + WorkFlowManager[0]["WorkFlowName"].ToString();
                CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "وضعیت جاری درخواست:" + CurrentTaskName;
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "اطلاعات مربوط به گردش کار تغییر یافته است.";
                return;
            }
            if (CurrentTaskCode != SaveInfoTaskCode)
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                return;
            }
            if (!CheckPermitionForSendDoc(TableId))
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.درخواست مورد نظر توسط کارمند ایجاد شده است.";
                return;
            }

            if (WFCode == (int)TSP.DataManager.WorkFlows.MemberConfirming)
                WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
            else if (WFCode == (int)TSP.DataManager.WorkFlows.MemberTransferConfirming)
                WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMemberTransfer);
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است.";
            }

            if (WorkFlowTaskManager.Count == 1)
            {

                Session["SendBackDataTable_MeRq"] = WorkFlowTaskManager.DataTable;
                cmbSendBackTask.DataSource = WorkFlowTaskManager.DataTable;
                cmbSendBackTask.ValueField = "TaskId";
                cmbSendBackTask.TextField = "TaskName";
                cmbSendBackTask.DataBind();
                cmbSendBackTask.SelectedIndex = 0;
                PanelSaveSuccessfully.ClientVisible = false;
                PanelMain.Visible = true;
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "عملیات بعد در گردش کار نامشخص است.";
            }

        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده عضو انتخاب شده انجام نشده است.";//+"TableType="+TableType.ToString()+"TableId="+TableId.ToString();
        }
    }

    private void SendMemberFileDocToNextStep(int MReId, int WorkflowCode)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int NextStepTaskId = -1;
        if (WorkflowCode == (int)TSP.DataManager.WorkFlows.MemberConfirming)
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
        else if (WorkflowCode == (int)TSP.DataManager.WorkFlows.MemberTransferConfirming)
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMemberTransfer);

        if (WorkFlowTaskManager.Count != 1)
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مرحله بعد گردش کار نا مشخص است.";
            return;
        }
        int NextStepTaskCode = int.Parse(WorkFlowTaskManager[0]["TaskCode"].ToString());
        WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
        if (WorkFlowTaskManager.Count == 1)
        {
            NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        }
        if (Session["SendBackDataTable_MeRq"] == null)
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "اعتبار صفحه به پایان رسیده است.لطفا مجددا اقدام نمایید.";
            return;
        }

        DataTable dtSendBack = (DataTable)Session["SendBackDataTable_MeRq"];
        cmbSendBackTask.DataSource = dtSendBack;
        cmbSendBackTask.ValueField = "TaskId";
        cmbSendBackTask.TextField = "TaskName";
        cmbSendBackTask.DataBind();

        int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
        if (SelectedTaskId != NextStepTaskId)
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";
            return;
        }
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(MemberRequestManager);
        int NmcId = Utility.GetCurrentUser_MeId();
        int NmcIdType = -1;
        if (Utility.IsDBNullOrNullValue(IsMeTemp))
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مقادیر صفحه معتبر نمی باشد.";
            return;
        }
        if (IsMeTemp == "0")
            NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId;
        else
            NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TempMember;
        if (NmcId > 0)
        {
            TransactionManager.BeginSave();
            string Url = "<a href='../../Employee/MembersRegister/MemberRegister1.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";

            string MsgContent = "";
            int SendDoc = -4;
            if (chbIsSendMail.Checked)
                SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, MReId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url);
            else
                SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, MReId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId(), NmcIdType);
            switch (SendDoc)
            {
                case -6:
                    TransactionManager.CancelSave();
                    PanelSaveSuccessfully.Visible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله جاری وجود ندارد.";
                    break;
                case -4:
                    TransactionManager.CancelSave();
                    PanelSaveSuccessfully.Visible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                    break;
                case -5:
                    TransactionManager.CancelSave();
                    PanelSaveSuccessfully.Visible = true;
                    PanelMain.Visible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                    break;
                case -8:
                    TransactionManager.CancelSave();
                    PanelSaveSuccessfully.Visible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                    break;
                default:
                    MemberRequestManager.FindByCode(MReId);
                    if (MemberRequestManager.Count != 1)
                    {
                        PanelSaveSuccessfully.Visible = true;
                        PanelMain.ClientVisible = false;
                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                        lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                        return;
                    }
                    MemberRequestManager[0].BeginEdit();
                    MemberRequestManager[0]["WfCurrentTaskId"] = SelectedTaskId;
                    MemberRequestManager[0].EndEdit();
                    MemberRequestManager.Save();
                    TransactionManager.EndSave();
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
                    lblInstitueWarning.Text = "ذخیره انجام شد.";
                    PanelMain.ClientVisible = false;
                    PanelSaveSuccessfully.ClientVisible = true;
                    GridViewMemberRequest.DataBind();
                    break;
            }
        }
        else
        {
            PanelSaveSuccessfully.Visible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
        }


        GridViewMemberRequest.DataBind();
    }

    #endregion

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming)// || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo)
            {
                return true;

            }

        }
        return false;

    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        dtState.DefaultView.RowFilter = "StateType=0";
        if (dtState.DefaultView.Count == 1)
            if (dtState.Rows.Count == 1)
            {
                int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());

                if (CurrentNmcId == Utility.GetCurrentUser_MeId() && CurrentNmcIdType == 1)
                {
                    if (CurrentTaskCode == TaskCode)
                        return true;

                }

            }
        return false;


    }

    private Boolean CheckPermitionForSendDoc(int TableId)
    {
        TSP.DataManager.WFPermission WFPermission = new TSP.DataManager.WFPermission();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        return (WorkFlowPermission.CheckPermissionForSendDocToNextStepByUserForOtherPortals(TableId, WfCode, (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_NmcIdType()));
    }

    #endregion

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberRequest).ToString());
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
