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

public partial class Members_Documents_ObservationDoc : System.Web.UI.Page
{
    #region Events
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
        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["SendBackDataTable_ObsDoc"] = "";

            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnView.Enabled = true;
            btnView2.Enabled = true;

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.ClearBeforeFill = true;
            ObjdsMemberFile.SelectParameters[0].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            DataTable dtObsLastVersion = DocMemberFileManager.SelectObsDocLastVersionByMeId(Utility.GetCurrentUser_MeId());
            if (dtObsLastVersion.Rows.Count > 0)
            {
                int MfId = int.Parse(dtObsLastVersion.Rows[0]["MeId"].ToString());
                DocMemberFileManager.FindByDocumentType(MfId, 2);
                if (DocMemberFileManager.Count > 0)
                {
                    BtnNew.Enabled = false;
                    btnNew2.Enabled = false;
                }
                else
                {
                    BtnNew.Enabled = true;
                    btnNew2.Enabled = true;
                }
                // ObjdsMemberFile.SelectParameters[0].DefaultValue = MfId.ToString();
            }
            // else
            //   ObjdsMemberFile.SelectParameters[0].DefaultValue = "-2";

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming).ToString();
            SetPageFilter();
            SetGridRowIndex();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
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
        Session["Title"] = "مجوز ناظر حقیقی";
        Session["Header"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString() + " (" + "کد عضویت: " + MemberManager[0]["MeId"].ToString() + ")";

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        // TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        //MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        //if (MemberManager.Count == 1)
        //{
        //if ((bool)MemberManager[0]["IsLock"] == true)
        //{
        //    TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
        //    string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
        //}
        //else
        //{
        if (!CheckConditions()) return;

        int MeId = Utility.GetCurrentUser_MeId();
        DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
        if (dtObsDoc.Rows.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("پیش از این برای شما مجوز نظارت تعریف شده است.");
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
        //int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        //if (MRsId == 1)
        //{
        //    DataTable dtDocMe = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0);
        //    if (dtDocMe.Rows.Count > 0)
        //    {
        //int LastMfId = int.Parse(dtDocMe.Rows[0]["MfId"].ToString());
        //if (!Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["IsConfirm"]) && dtDocMe.Rows[0]["IsConfirm"].ToString() == "1")
        //{
        //    DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(LastMfId, Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation);
        //    if (dtMeDetail.Rows.Count > 0)
        //    {
        //NextPage("New");
        // InsertDocMemberFileObs();
        //    }
        //    else
        //    {

        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "بدلیل نداشتن صلاحیت نظارت امکان درخواست مجوز نظارت برای شما وجود ندارد.";
        //    }
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "امکان درخواست مجوز نظارت برای شما وجود ندارد.وضعیت پروانه اشتغال شما عدم تایید می باشد.";
        //}
        //    }
        //    else
        //    {

        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "بدلیل نداشتن پروانه اشتغال،امکان درخواست مجوز نظارت برای شما وجود ندارد.";
        //    }
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "امکان درخواست مجوز نظارت برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
        //}
        //  }
        //  }
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        //}
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int MfId = -1;
        int focucedIndex = GridViewMemberFile.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];

            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
            if (MemberManager.Count == 1)
            {
                if ((bool)MemberManager[0]["IsLock"] == true)
                {
                    TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                    string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";

                }
                else
                {
                    int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                    if (MRsId == 1)
                    {
                        if (CheckPermitionForEdit(MfId))
                        {
                            NextPage("Edit");
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                    }
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت ویرایش اطلاعات یک  رکورد را انتخاب نمایید.";
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
            DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
            int PostId = int.Parse(GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"].ToString());
            string GridFilterString = GridViewMemberFile.FilterExpression;
            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" +
                "&PostId=" + Utility.EncryptQS(PostId.ToString());
            Response.Redirect("../WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک درخواست را انتخاب نمایید";
        }
    }

    protected void btnReDuplicate_Click(object sender, EventArgs e)
    {
        //TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        //MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        //if (MemberManager.Count == 1)
        //{
        //    int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        //    if (MRsId == 1)
        //    {
        //        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 1);
        //        if (dtDocMeFile.Rows.Count > 0)
        //        {
        //            int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        //            ReDuplicate(MfId);
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
        //        }
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان درخواست صدور المثنی برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
        //    }
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        //}
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(-1, MfId);
                if (dtObsDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtObsDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtObsDoc.Rows[0]["MemberId"].ToString());
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                        if (MRsId == 1)
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersionByMeFileId(MeFileId);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                int LastMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                                Revival(LastMfId, MeId);
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
                            }

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(-1, MfId);
                if (dtObsDoc.Rows.Count == 1)
                {
                    if (CheckPermitionForDelete(MfId))
                    {
                        DeleteRequest(MfId);
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "بر اساس قوانین گردش کار امکان لغو این درخواست وجود ندارد.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewMemberFile.Columns["WFState"].Visible = false;
        GridViewExporter.FileName = "ObservationDocument";

        GridViewExporter.WriteXlsToResponse(true);
        GridViewMemberFile.Columns["WFState"].Visible = true;
    }

    protected void GridViewMemberFile_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewMemberFile.DataBind();
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int MfId = int.Parse(MeFileRow["MfId"].ToString());
            int MeFileTableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
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

    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberFile_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();
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
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int ObsMfId = -1;
        int MeId = -1;
        int MfId = -1;
        int focucedIndex = -1;
        if (Mode == "View")
        {
            if (GridViewMemberFile.FocusedRowIndex > -1)
            {

                focucedIndex = GridViewMemberFile.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                    //***ObsDocId
                    ObsMfId = (int)row["MfId"];
                    //***MfId
                    MfId = (int)row["MeId"];
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }

        }
        else
        {
            focucedIndex = GridViewMemberFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                //***MfId
                MfId = (int)row["MeId"];
                if (Mode != "New" || Mode != "View")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DocMemberFileManager.FindByCode(MfId, 0);
                    if (DocMemberFileManager.Count == 1)
                    {
                        MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است.";
                        return;
                    }
                    DataTable dtDocMeFile = new DataTable();
                    if (Mode == "Revival")
                        dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersion(MeId, -1, 1);
                    else
                        dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersion(MeId, -1, 0);

                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        //***ObsDocId
                        ObsMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }
            }
        }

        if (ObsMfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            string GridFilterString = GridViewMemberFile.FilterExpression;
            if (Mode == "New")
            {
                ObsMfId = -1;
                Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(ObsMfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(ObsMfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
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
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

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
                            DocMemberFileManager.FindByCode(TableId, 2);
                            if (DocMemberFileManager.Count == 1)
                            {
                                DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, DocMeFileSaveInfoTaskCode, CurrentWorkFlowCode);
                                if (dtNextTopTask.Rows.Count > 0)
                                {
                                    Session["SendBackDataTable_ObsDoc"] = dtNextTopTask;
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
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
        int SaveObservationDocInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        int NextStepTaskId = -1;

        DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, SaveObservationDocInfoTaskCode, (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming);
        if (dtNextTopTask.Rows.Count > 0)
        {
            int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
            WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            DataTable dtSendBack = (DataTable)Session["SendBackDataTable_ObsDoc"];
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
                    string Url = "<a href='~/Members/Documents/AddObservationDoc.aspx?MeId=" + "" + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
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

    private void Revival(int MfId, int MeId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        DocMemberFileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);

        try
        {
            // int MfId = int.Parse(DocMeFileRow["MfId"].ToString());
            // int MeId = int.Parse(DocMeFileRow["MeId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
            int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
            DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
            if (dtWfState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectObservationDocAndEndProcess;
                int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmObservationDocAndEndProccess;
                // int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument;

                int RejectTaskId = -1;
                int ConfirmTaskId = -1;
                //   int DocumentUnitConfirmTaskId = -1;

                //    WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
                //    if (WorkFlowTaskManager.Count > 0)
                //    {
                //        DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                //    }


                WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                {
                    DocMemberFileManager.SelectObservationDoc(-1, MfId);
                    if (DocMemberFileManager.Count == 1)
                    {
                        if (Convert.ToBoolean(DocMemberFileManager[0]["IsConfirm"]) == true)
                        {
                            string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                            Utility.Date objDate = new Utility.Date(CrtEndDate);
                            string LastMonth = objDate.AddMonths(-1);
                            string Today = Utility.GetDateOfToday();
                            int IsDocExp = string.Compare(Today, LastMonth);
                            if (IsDocExp > 0)
                            {
                                NextPage("Revival");
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.";

                            }
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان تمدید برای مجوز تایید نشده وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار مجوز انتخاب شده امکان درخواست تمدید وجود ندارد.";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مجوز انتخاب شده جریان کاری تعریف نشده است.";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
        }
    }
    private void InsertDocMemberFileObs()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            TransactionManager.BeginSave();

            DataTable dtLastMeFileVersion = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0);
            if (dtLastMeFileVersion.Rows.Count > 0)
            {

                MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
                if (MemberManager.Count == 1)
                {
                    DataRow MemberFileRow = DocMemberFileManager.NewRow();

                    MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ObservationDocument);
                    MemberFileRow["MeId"] = dtLastMeFileVersion.Rows[0]["MfId"].ToString();
                    MemberFileRow["DocType"] = 2;//****مجوز ناظر حقیقی
                    // MemberFileRow["SerialNo"] = "";
                    //MemberFileRow["RegDate"] = "";
                    // MemberFileRow["ExpireDate"] = "";              
                    MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();
                    MemberFileRow["Type"] = 0; //*****صدور                    
                    MemberFileRow["IsConfirm"] = 0;
                    MemberFileRow["IsTemporary"] = 0;
                    MemberFileRow["InActive"] = 0;
                    MemberFileRow["Description"] = "";
                    MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
                    MemberFileRow["ModifiedDate"] = DateTime.Now;

                    DocMemberFileManager.AddRow(MemberFileRow);
                    int cn = DocMemberFileManager.Save();
                    DocMemberFileManager.DataTable.AcceptChanges();
                    if (cn > 0)
                    {
                        string MfCode = "88";
                        string ImpDocCode = "88";
                        string PrCode = "";
                        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
                        ProvinceManager.FindByCode(Utility.GetCurrentProvinceId());
                        if (ProvinceManager.Count == 1)
                        {
                            PrCode = ProvinceManager[0]["NezamCode"].ToString();
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                        }
                        string MfSerialNo = DocMemberFileManager[0]["MFSerialNo"].ToString();
                        int SerialLen = MfSerialNo.Length;
                        int t = 5 - SerialLen;
                        for (int i = 0; i < t; i++)
                        {
                            MfSerialNo = "0" + MfSerialNo;
                        }

                        MfCode = ImpDocCode + "-" + PrCode + "-" + MfSerialNo;
                        DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
                        DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"] = MfCode;
                        DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
                        if (DocMemberFileManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            return;
                        }

                        int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
                        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
                        int MeId = Utility.GetCurrentUser_MeId();
                        int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, MeId, Utility.GetCurrentUser_UserId(), 1);
                        if (WfStart > 0)
                        {
                            TransactionManager.EndSave();
                            Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString()) + "&PgMd=" + Utility.EncryptQS("View"));
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "جهت دریافت مجوز مجری حقیقی دارا بودن پروانه اشتغال به کار الزامی است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }
    private void DeleteRequest(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TransactionManager.Add(DocImpDocCityManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        try
        {
            TransactionManager.BeginSave();
            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming, MfId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[0].Delete();
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

            #region City
            DataTable dtImpCity = DocImpDocCityManager.FindMfId(MfId);
            if (dtImpCity.Rows.Count > 0)
            {
                for (int i = 0; i < dtImpCity.Rows.Count; i++)
                    DocImpDocCityManager[0].Delete();
                if (DocImpDocCityManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete Letter
            LetterRelatedTablesManager.FindByTableIdTableType(MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs));
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
            DocMemberFileManager.SelectObservationDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].Delete();
                if (DocMemberFileManager.Save() > 0)
                {
                    TransactionManager.EndSave();
                    IsPageRefresh = true;
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
                int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

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
                                if (FirstNmcIdType == 1)
                                {
                                    if (FirstNmcId == Utility.GetCurrentUser_MeId())
                                    {
                                        return true;
                                    }
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
    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
                int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtState.Rows.Count == 1)
                {
                    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

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
                                if (FirstNmcId == Utility.GetCurrentUser_MeId() && FirstNmcIdType == Utility.GetCurrentUser_NmcIdType())
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
    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.PortalObserverDocument).ToString());
    }
    bool CheckConditions()
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        int MeId = Utility.GetCurrentUser_MeId();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
            if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
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
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("مانده حساب شما صفر نمی باشد.");
                        return false;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("حساب شما نامشخص می باشد.");
                    return false;
                }
            }
            #endregion

            //********************************افرادی که در شرکت هستند جهت کار نظارت مجوز نظارت بایستی داشته باشند؟؟؟؟؟؟******************************************************
            #region Check OfficeMember
            //OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
            //if (OfMeManager.Count > 0)
            //{
            //    int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
            //    DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
            //    if (dtEngOffReq.Rows.Count > 0)
            //    {
            //        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در دفتر ";
            //        str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
            //        str += " مشغول به کار می باشد";
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = (str);
            //        return false;
            //    }
            //}
            //OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
            //if (OfMeManager.Count > 0)
            //{
            //    int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
            //    DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfIdMember, 0, MeId);
            //    if (dtOffReq.Rows.Count > 0)
            //    {
            //        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در شرکت ";
            //        str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
            //        str += " مشغول به کار می باشد";
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = (str);
            //        return false;
            //    }
            //}
            #endregion

            #region CheckMemberFile
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("امکان ثبت مجوز نظارت وجود ندارد.ابتدا باید پروانه اشتغال به کار تایید شده داشته باشید.");
                return false;
            }

            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            //Boolean HasObsResponsibility = true;
            //Boolean HasObsResponsibility = true;
            DataTable dtMeDetailObs = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
            if (dtMeDetailObs.Rows.Count <= 0 && dtMeDetailMapping.Rows.Count <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("بدلیل نداشتن هیچ یک از صلاحیت های نظارت و نقشه برداری امکان درخواست مجوز نظارت برای شما وجود ندارد.");
                return false;
            }

            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            if (!string.IsNullOrEmpty(ExpireDate))
            {
                if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("بدلیل اتمام مدت اعتبار پروانه اشتغال امکان درخواست مجوز نظارت برای شما وجود ندارد.");
                    return false;
                }
            }

            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                if (DocMemberFileMajorManager.Count <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("رشته موضوع پروانه شما نامشخص است");
                    return false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("امکان ثبت مجوز نظارت وجود ندارد.وضعیت پروانه اشتغال شما نامشخص می باشد.");
                return false;
            }
            #endregion
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("کد عضویت وارد شده معتبر نمی باشد.");
            return false;
        }
        return true;
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
    #endregion
}
