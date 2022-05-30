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

public partial class Members_MemberInfo_MemberJobQuality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");

            }

            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);

            string MeId = Utility.DecryptQS(MemberId.Value);
            //string MReId = Utility.DecryptQS(MemberRequest.Value);
            string JhId = Utility.DecryptQS(JobId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode) || string.IsNullOrEmpty(JhId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            OdbFactorDocuments.SelectParameters[0].DefaultValue = JhId;


            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;

        }


        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = CustomGridJobQuality.ClientVisible = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    void Page_Load_Member()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        switch (Utility.DecryptQS(HDMode.Value))
        {
            case "Home":
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;


                break;
            case "Request":

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());


                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;

                }

                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]))//FromEmployee
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;

                    }
                    if (ReqManager[0]["IsConfirm"].ToString() != "0") //answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;

                    }
                }

                break;
        }
    }

    void Page_Load_TempMe()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        switch (Utility.DecryptQS(HDMode.Value))
        {
            //case "Home":
            //    btnEdit.Enabled = false;
            //    btnEdit2.Enabled = false;
            //    BtnNew.Enabled = false;
            //    BtnNew2.Enabled = false;


            //    break;
            case "Request":

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());


                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;

                }

                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]))//FromEmployee
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;

                    }
                    if (ReqManager[0]["IsConfirm"].ToString() != "0") //answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;

                    }
                }

                break;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS("") + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("New") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + "&Mode=" + HDMode.Value);

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
                if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                {
                    TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();
                    JobManager.FindByCode(JhId);
                    if (JobManager.Count == 1)
                    {
                        int MReId = int.Parse(JobManager[0]["TableId"].ToString());
                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                        if (MReId == CurrentMReId)
                        {
                            if (CheckPermitionForEdit(MReId))
                                Response.Redirect("MemberJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value
                                   + "&Mode=" + HDMode.Value);

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
                else
                {
                    TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
                    JobManager.FindByCode(JhId);
                    if (JobManager.Count == 1)
                    {
                        int MReId = int.Parse(JobManager[0]["TableId"].ToString());
                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                        if (MReId == CurrentMReId)
                        {
                            if (CheckPermitionForEdit(MReId))
                                Response.Redirect("MemberJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value
                                   + "&Mode=" + HDMode.Value);

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
                Response.Redirect("MemberJobQualityInsert.aspx?JhqId=" + Utility.EncryptQS(JhqId.ToString()) + "&aPageMode=" + Request.QueryString["aPageMode"] + "&JPageMode=" + Utility.EncryptQS("View") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value
                               + "&Mode=" + HDMode.Value);

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
        Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Request.QueryString["PageMode2"] + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value
            + "&Mode=" + HDMode.Value);

    }


    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewFinancialStatus_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        CustomGridJobQuality.FocusedRowIndex = e.VisibleIndex;
    }
    protected void CustomGridJobQuality_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomGridJobQuality_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void GridViewJudge_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "MeetingDate")
            e.Cell.Style["direction"] = "ltr";
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

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
                                return true;

                            }

                        }

                    }

                }

            }

        }
        return false;


    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Request.QueryString["PageMode2"] + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value
             + "&Mode=" + HDMode.Value);
                break;

        }
    }
}
