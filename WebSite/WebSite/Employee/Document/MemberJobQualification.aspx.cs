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

public partial class Employee_Document_MemberJobQualification : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocOffJobHistoryQualityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            //btnInActive.Enabled = per.CanEdit;
            //btnInActive2.Enabled = per.CanEdit;
            GridViewJobQuality.Visible = per.CanView;
            if (string.IsNullOrEmpty(Request.QueryString["MfId"]) || string.IsNullOrEmpty(Request.QueryString["DocType"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["JhId"]) || string.IsNullOrEmpty(Request.QueryString["PrePgMd"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            try
            {
                HiddenFieldQualification["MfId"] = Server.HtmlDecode(Request.QueryString["MfId"]).ToString();
                HiddenFieldQualification["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"]).ToString();
                HiddenFieldQualification["PrePageMode"] = Server.HtmlDecode(Request.QueryString["PrePgMd"]).ToString();
                HiddenFieldQualification["JhId"] = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
                HiddenFieldQualification["DocType"] = Server.HtmlDecode(Request.QueryString["DocType"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(HiddenFieldQualification["PageMode"].ToString());
            string MfId = Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString());
            string JhId = Utility.DecryptQS(HiddenFieldQualification["JhId"].ToString());

            if (string.IsNullOrEmpty(MfId) || string.IsNullOrEmpty(MfId) || string.IsNullOrEmpty(JhId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            OdbFactorDocuments.SelectParameters[0].DefaultValue = JhId;

            int DocType = int.Parse(Utility.DecryptQS(HiddenFieldQualification["DocType"].ToString()));
            if (DocType == 0)
            {
                MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
                btnOpinion.Visible = false;
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.FindByCode(int.Parse(MfId), DocType);
                if (DocMemberFileManager.Count == 1)
                {
                    int MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                    HiddenFieldQualification["MeId"] = Utility.EncryptQS(MeId.ToString());
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
                        //  RoundPanelQualification.HeaderText = "پروانه اشتغال به کار: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();                        
                    }
                    else
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

            }
            else if (DocType == 1)
            {
                btnOpinion.Visible = true;

                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.FindByCode(int.Parse(MfId), DocType);
                if (DocMemberFileManager.Count == 1)
                {
                    int MemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                    DocMemberFileManager.FindByCode(MemberFileId, 0);
                    if (DocMemberFileManager.Count == 1)
                    {
                        int MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                        HiddenFieldQualification["MeId"] = Utility.EncryptQS(MeId.ToString());
                        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                        MemberManager.FindByCode(MeId);
                        if (MemberManager.Count == 1)
                        {
                            HiddenFieldQualification["MeId"] = Utility.EncryptQS(MeId.ToString());
                            //    RoundPanelQualification.HeaderText = "مجوز مجری حقیقی: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                        }
                    }
                    else
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
            }

            CheckWorkFlowPermission();
            if (DocType == 1 && !CheckPermissionForImpJudge())
            {
                btnOpinion.Enabled = false;
                btnOpinion1.Enabled = false;
            }
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            //this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnOpinion"] = btnOpinion.Enabled;
            this.ViewState["VisiblebtnOpinion"] = btnOpinion.Visible;

        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldQualification["MeId"]))
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldQualification["MeId"].ToString()));
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnInActive"] != null)
        //    this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnOpinion"] != null)
            this.btnOpinion.Enabled = this.btnOpinion1.Enabled = (bool)this.ViewState["btnOpinion"];
        if (this.ViewState["VisiblebtnOpinion"] != null)
            this.btnOpinion.Visible = this.btnOpinion1.Visible = (bool)this.ViewState["VisiblebtnOpinion"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        // Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS("") + "&PgMd=" + Request.QueryString["PageMode"] + "&JPgMd=" + Utility.EncryptQS("New") +  "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value);
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
        //try
        //{
        //    int JhqId = -1;
        //    int JhId = int.Parse(Utility.DecryptQS(JobId.Value));

        //    if (GridViewJobQuality.FocusedRowIndex > -1)
        //    {

        //        DataRow row = GridViewJobQuality.GetDataRow(GridViewJobQuality.FocusedRowIndex);
        //        JhqId = (int)row["JhqId"];

        //    }
        //    if (JhqId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //    }
        //    else
        //    {
        //        TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
        //        JobManager.FindByCode(JhId);
        //        if (JobManager.Count == 1)
        //        {
        //            int OfReId = int.Parse(JobManager[0]["TableId"].ToString());
        //            int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
        //            if (OfReId == CurrentOfReId)
        //            {

        //                if (CheckPermitionForEdit(OfReId))
        //                    Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value);



        //                else
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
        //                }


        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        //            }


        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        //        }

        //    }
        //}
        //catch (Exception)
        //{

        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        //}
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
        //try
        //{
        //    int JhqId = -1;

        //    if (GridViewJobQuality.FocusedRowIndex > -1)
        //    {

        //        DataRow row = GridViewJobQuality.GetDataRow(GridViewJobQuality.FocusedRowIndex);
        //        JhqId = (int)row["JhqId"];

        //    }
        //    if (JhqId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //    }
        //    else
        //    {
        //        Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value);

        //    }
        //}
        //catch (Exception)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        //}

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddMemberJobHistory.aspx?MFId=" + HiddenFieldQualification["MfId"].ToString() + "&DocType=" + HiddenFieldQualification["DocType"].ToString() + "&JHId=" + HiddenFieldQualification["JhId"].ToString() + "&PrePgMd=" + HiddenFieldQualification["PrePageMode"].ToString() + "&PgMd=" + HiddenFieldQualification["PageMode"].ToString());
    }

    protected void btnOpinion_Click(object sender, EventArgs e)
    {
        //if (GridViewJobQuality.FocusedRowIndex > -1)
        //{
        //    DataRow row = GridViewJobQuality.GetDataRow(GridViewJobQuality.FocusedRowIndex);
        //    int JhqId = (int)row["JhqId"];

        //    int MfId = int.Parse(Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString()));
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
        //            Response.Redirect("AddMemberJobHistory.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&PrePgMd=" + Request.QueryString["PrePgMd"] + "&JPageMode=" + Utility.EncryptQS("Judge") + "&PgMd=" + HiddenFieldQualification["PageMode"].ToString() + "&MfId=" + HiddenFieldQualification["MfId"].ToString() + "&JhId=" + HiddenFieldQualification["JhId"].ToString());

        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "در این مرحله از جریان کار شما قادر به ثبت نظر کارشناسی نمی باشید.";
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
        GridViewJobQuality.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewJobQuality_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJobQuality_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void MenuJob_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (e.Item.Name == "Job")
        {
            Response.Redirect("AddMemberJobHistory.aspx?MFId=" + HiddenFieldQualification["MfId"].ToString() + "&DocType=" + HiddenFieldQualification["DocType"].ToString() + "&JHId=" + HiddenFieldQualification["JhId"].ToString() + "&PrePgMd=" + HiddenFieldQualification["PrePageMode"].ToString() + "&PgMd=" + HiddenFieldQualification["PageMode"].ToString());

        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int JhqId = -1;
        int focucedIndex = GridViewJobQuality.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewJobQuality.GetDataRow(focucedIndex);
            JhqId = (int)row["JhqId"];
        }
        if (JhqId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                JhqId = -1;
                Response.Redirect("AddMemeberJobQualification.aspx?JhId=" + HiddenFieldQualification["JhId"].ToString() + "&JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&MfId=" + HiddenFieldQualification["MfId"].ToString() + "&DocType=" + HiddenFieldQualification["DocType"].ToString() + "&PgMd=" + Utility.EncryptQS(Mode) + "&PrePgMd=" + HiddenFieldQualification["PrePageMode"]);
            }
            else
            {
                Response.Redirect("AddMemeberJobQualification.aspx?JhId=" + HiddenFieldQualification["JhId"].ToString() + "&JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&MfId=" + HiddenFieldQualification["MfId"].ToString() + "&DocType=" + HiddenFieldQualification["DocType"].ToString() + "&PgMd=" + Utility.EncryptQS(Mode) + "&PrePgMd=" + HiddenFieldQualification["PrePageMode"]);
            }
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    //private void CheckWorkFlowPermissionForSave()
    //{
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int CurrentTaskOrder = -1;
    //    int TaskOrder = -1;
    //    //****TableId
    //    string MfId = Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString());
    //    string DocType = Utility.DecryptQS(HiddenFieldQualification["DocType"].ToString());
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //    int WfCode = -1;
    //    int TaskCode = -1;
    //    if (DocType == "0")
    //    {
    //        WfCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
    //        TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
    //    }
    //    else if (DocType == "1")
    //    {
    //        WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //        TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
    //    }

    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, int.Parse(MfId));
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //    }

    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }

    //    if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
    //    {
    //        WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        TaskDoerManager.FindByTaskId(TaskId);

    //        if (TaskDoerManager.Count > 0)
    //        {
    //            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
    //            NezamMemberChartManager.FindByNcId(NcId);

    //            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

    //            LoginManager.FindByMeIdUltId(EmpId, 4);
    //            if (LoginManager.Count > 0)
    //            {
    //                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
    //                int CurrentUserId = Utility.GetCurrentUser_UserId();
    //                if (CurrentUserId == userId)
    //                {
    //                    BtnNew.Enabled = true;
    //                    BtnNew2.Enabled = true;
    //                    btnEdit.Enabled = true;
    //                    btnEdit2.Enabled = true;
    //                    //btnInActive.Enabled = true;
    //                    //btnInActive2.Enabled = true;

    //                }
    //                else
    //                {

    //                    BtnNew.Enabled = false;
    //                    BtnNew2.Enabled = false;
    //                    btnEdit.Enabled = false;
    //                    btnEdit2.Enabled = false;
    //                    //btnInActive.Enabled = false;
    //                    //btnInActive2.Enabled = false;

    //                }
    //            }
    //            else
    //            {
    //                BtnNew.Enabled = false;
    //                BtnNew2.Enabled = false;
    //                btnEdit.Enabled = false;
    //                btnEdit2.Enabled = false;
    //                //btnInActive.Enabled = false;
    //                //btnInActive2.Enabled = false;

    //            }
    //        }
    //        else
    //        {
    //            BtnNew.Enabled = false;
    //            BtnNew2.Enabled = false;
    //            btnEdit.Enabled = false;
    //            btnEdit2.Enabled = false;
    //            //btnInActive.Enabled = false;
    //            //btnInActive2.Enabled = false;

    //        }
    //    }
    //    else
    //    {
    //        BtnNew.Enabled = false;
    //        BtnNew2.Enabled = false;
    //        btnEdit.Enabled = false;
    //        btnEdit2.Enabled = false;
    //        //btnInActive.Enabled = false;
    //        //btnInActive2.Enabled = false;

    //    }
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    //this.ViewState["BtnActive"] = btnInActive.Enabled;

    //}

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string MFId = Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        string DocType = Utility.DecryptQS(HiddenFieldQualification["DocType"].ToString());
        int TaskCode = -1;
        int WFCode = -1;

        if (int.Parse(DocType) == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        }
        else if (int.Parse(DocType) == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        }

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        // this.ViewState["btnActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = -1;
        int WfCode = -1;
        int DocType = int.Parse(Utility.DecryptQS(HiddenFieldQualification["DocType"].ToString()));
        if (DocType == 0)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
            WfCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        }
        else if (DocType == 1)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        }
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
                    if (CurrentTaskCode == TaskCode)
                    {
                        //????????????????/
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                if (FirstNmcIdType == 0)
                                {
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                    if (Permission > 0)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;

    }

    private Boolean CheckPermissionForImpJudge()
    {
        return false;
        //int MfId = int.Parse(Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString()));
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
