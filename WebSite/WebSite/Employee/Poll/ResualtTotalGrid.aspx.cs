using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class Employee_Poll_ResualtTotalGrid : System.Web.UI.Page
{
    #region Properties
    private int PollId
    {
        set
        {
            HiddenFieldPage["PollId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldPage["PollId"]);
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableWarning();
        if (!IsPostBack)
        {
            TSP.DataManager.Permission PerAnswer = TSP.DataManager.PollAnswerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            RoundPanelPoll.Visible = GridViewPollAnswer.Visible = PerAnswer.CanView;
            SetKey();
            this.ViewState["ListView"] = GridViewPollAnswer.Visible;
        }
        LoadPollQuestionData();

        if (this.ViewState["ListView"] != null)
            RoundPanelPoll.Visible = GridViewPollAnswer.Visible = (bool)this.ViewState["ListView"];

        Session["DataTable"] = GridViewPollAnswer.Columns;
        Session["DataSource"] = ObjdsbserverPollQuestion;
        Session["Title"] = RoundPanelPoll.HeaderText + "\n" + " " + lblMaxAnswerDate.Text;
        Session["Header"] ="";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Poll.aspx");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        //if (IsPageRefresh)
        //    return;
        ASPxGridViewExporter1.FileName = "Poll";
        ASPxGridViewExporter1.WriteXlsToResponse(true);
    }
    #endregion

    #region Methods
    private void SetWarningLableWarning()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void SetKey()
    {
        if (Request.QueryString["PId"] == null || Request.QueryString["PId"].ToString() == "")
        {
            Response.Redirect("Poll.aspx");
        }

        PollId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PId"]));
        ObjdsbserverPollQuestion.SelectParameters["PollId"].DefaultValue = PollId.ToString();
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.FindByCode(PollId);
        if (PollPollManager.Count == 1)
        {
            RoundPanelPoll.HeaderText = "نتایج نظرسنجی: " + PollPollManager[0]["Tittle"].ToString()   ;
            lblMaxAnswerDate.Text = " تاریخ آخرین پاسخ به نظرسنجی:" + PollPollManager[0]["MaxAnswerDate"].ToString();
        }
    }

    private void LoadPollQuestionData()
    {
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollQuestionManager.FindByPollId(PollId);
        DataViewQuestion.DataSource = PollQuestionManager.DataTable;
        DataViewQuestion.DataBind();
        TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
        DataTable dtReport = PollAnswerManager.SelectAnswerReport(PollId, -1);
        if (dtReport.Rows.Count > 0)
        {
            lblTotalAnswer.Text = "تعداد کل پاسخ ها : " + dtReport.Rows[0]["TotalAnswer"].ToString();
        }
    }
    #endregion
}