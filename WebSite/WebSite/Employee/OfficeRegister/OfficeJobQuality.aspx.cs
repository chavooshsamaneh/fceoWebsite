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

public partial class Employee_OfficeRegister_OfficeJobQuality : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            TSP.DataManager.Permission per = TSP.DataManager.DocOffJobHistoryQualityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            //btnInActive.Enabled = per.CanEdit;
            //btnInActive2.Enabled = per.CanEdit;
            CustomGridJobQuality.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["JhId"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }
           
            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            string JhId = Utility.DecryptQS(JobId.Value);

            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId) || string.IsNullOrEmpty(JhId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            OdbFactorDocuments.SelectParameters[0].DefaultValue = JhId;

            OfficeInfoUserControl.OfReId = int.Parse(OfReId);

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "Document")
            {
                btnOpinion.Visible = true;
                btnOpinion1.Visible = true;
                CheckWorkFlowPermissionForDoc();
            }
            else if (Department == "MemberShip")
            {
                btnOpinion.Visible = false;
                btnOpinion1.Visible = false;
                CheckWorkFlowPermissionForOffice();
            }

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if (!Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromMember
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    //btnInActive.Enabled = false;
                    //btnInActive2.Enabled = false;
                }
              
                TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
                if (!string.IsNullOrEmpty(JhId))
                {
                    JhManager.FindByCode(int.Parse(JhId));
                    if (JhManager.Count == 1)
                    {
                        if (JhManager[0]["TableId"].ToString() != OfReId)
                        {
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;

                        }
                    }
                }
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            //this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnOpinion"] = btnOpinion.Enabled;

        }


        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnInActive"] != null)
        //    this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnOpinion"] != null)
            this.btnOpinion.Enabled = this.btnOpinion1.Enabled = (bool)this.ViewState["BtnOpinion"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS("") + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("New") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
             + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int JhqId = -1;
            int JhId = int.Parse(Utility.DecryptQS(JobId.Value));

            if (CustomGridJobQuality.FocusedRowIndex > -1)
            {

                DataRow row = CustomGridJobQuality.GetDataRow(CustomGridJobQuality.FocusedRowIndex);
                JhqId = (int)row["JhqId"];

            }
            if (JhqId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
                JobManager.FindByCode(JhId);
                if (JobManager.Count == 1)
                {
                    int OfReId = int.Parse(JobManager[0]["TableId"].ToString());
                    int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                    if (OfReId == CurrentOfReId)
                    {

                        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());                       
                        if ((Department == "Document" && CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && CheckPermitionForEditForOffice(OfReId)))
                            Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                                 + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                }

            }
        }
        catch (Exception)
        {

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int JhqId = -1;

            if (CustomGridJobQuality.FocusedRowIndex > -1)
            {

                DataRow row = CustomGridJobQuality.GetDataRow(CustomGridJobQuality.FocusedRowIndex);
                JhqId = (int)row["JhqId"];

            }
            if (JhqId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeJobInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&aPageMode=" + Request.QueryString["aPageMode"] + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
             + "&Dprt=" + HiddenFieldOffice["Department"].ToString());


    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        string PageName = "Office.aspx";
        switch (Dprt)
        {
            case "MemberShip":
                PageName = "Office.aspx";
                break;
            case "Document":
                PageName = "OfficeDocument.aspx";
                break;
        }
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?PostId=" + OfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {

            Response.Redirect(PageName);
        }
    }

    protected void btnOpinion_Click(object sender, EventArgs e)
    {

        //if (CustomGridJobQuality.FocusedRowIndex > -1)
        //{
        //    DataRow row = CustomGridJobQuality.GetDataRow(CustomGridJobQuality.FocusedRowIndex);
        //    int JhqId = (int)row["JhqId"];

        //    int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

        //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //    //int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;

        //    int WorkflowCode = -1;
        //    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        //    if (Department == "Document")
        //        WorkflowCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        //    else if (Department == "MemberShip")
        //       WorkflowCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        //   if (WorkflowCode == -1)
        //   {
        //       this.DivReport.Visible = true;
        //       this.LabelWarning.Text = "جریان کار پروانه جاری نامشخص است.";
        //       return;
        //   }
        //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.ExpertConfirmingDocumentOff;
        //    int GradingImplementDocTaskId = -1;

        //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode(WorkflowCode, OfReId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == GradingImplementDocTaskId)
        //        {
        //            Response.Redirect("OfficeJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("Judge") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

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
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "ردیفی انتخاب نشده است";
        //}

    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewFinancialStatus_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        CustomGridJobQuality.FocusedRowIndex = e.VisibleIndex;
    }

    protected void CustomGridJobQuality_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";

    }
    protected void CustomGridJobQuality_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewJudge_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MeetingId" || e.DataColumn.FieldName == "MeetingDate")
            e.Cell.Style["direction"] = "ltr";

    }
    #endregion
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("OfficeJobInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&aPageMode=" + Request.QueryString["aPageMode"] + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
            + "&Dprt=" + HiddenFieldOffice["Department"].ToString());


                break;

        }
    }
    #region Methods
    private void CheckWorkFlowPermissionForDoc()
    {
        CheckWorkFlowPermissionForSaveForDoc();
    }

    private void CheckWorkFlowPermissionForSaveForDoc()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(OfReId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
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
                        //btnInActive.Enabled = true;
                        //btnInActive2.Enabled = true;

                    }
                    else
                    {

                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        //btnInActive.Enabled = false;
                        //btnInActive2.Enabled = false;

                    }
                }
                else
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    //btnInActive.Enabled = false;
                    //btnInActive2.Enabled = false;

                }
            }
            else
            {
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                //btnInActive.Enabled = false;
                //btnInActive2.Enabled = false;

            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            //btnInActive.Enabled = false;
            //btnInActive2.Enabled = false;

        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        //this.ViewState["BtnActive"] = btnInActive.Enabled;

    }

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;

                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
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
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
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


    private void CheckWorkFlowPermissionForOffice()
    {
        CheckWorkFlowPermissionForSaveForOffice();
    }

    private void CheckWorkFlowPermissionForSaveForOffice()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, int.Parse(OfReId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
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
                        //btnInActive.Enabled = true;
                        //btnInActive2.Enabled = true;

                    }
                    else
                    {

                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        //btnInActive.Enabled = false;
                        //btnInActive2.Enabled = false;

                    }
                }
                else
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    //btnInActive.Enabled = false;
                    //btnInActive2.Enabled = false;

                }
            }
            else
            {
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                //btnInActive.Enabled = false;
                //btnInActive2.Enabled = false;

            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            //btnInActive.Enabled = false;
            //btnInActive2.Enabled = false;

        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        //this.ViewState["BtnActive"] = btnInActive.Enabled;

    }

    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
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
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                if (FirstNmcIdType == 0)
                                {
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
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

    #endregion
}
