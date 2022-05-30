using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Poll_PollResultMain : System.Web.UI.Page
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
            SetKey();
        }
        LoadPollQuestionData();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Poll.aspx");
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
        //    ObjdsbserverPollQuestion.SelectParameters["PollId"].DefaultValue = PollId.ToString();
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.FindByCode(PollId);
        if (PollPollManager.Count == 1)
        {
            RoundPanelPoll.HeaderText = "نتایج نظرسنجی: " + PollPollManager[0]["Tittle"].ToString();
        }
    }

    private void LoadPollQuestionData()
    {
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollQuestionManager.FindByPollId(PollId);
        DataViewQuestion.DataSource = PollQuestionManager.DataTable;
        DataViewQuestion.DataBind();
        if (PollQuestionManager.Count > 0)
        {
            int ResualtPublice=Convert.ToInt32( PollQuestionManager[0]["IsResultPublic"]);
            if (Utility.GetCurrentUser_LoginType() == -1 && ResualtPublice != 2)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString(),true);
                return;
            }
            else if(Utility.GetCurrentUser_LoginType()!=(int)TSP.DataManager.UserType.Member  &&Utility.GetCurrentUser_LoginType()!=(int)TSP.DataManager.UserType.Employee
                && Utility.GetCurrentUser_LoginType()!=(int)TSP.DataManager.UserType.Member && ResualtPublice ==0)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString(), true);
            }
        }
        TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
        DataTable dtReport = PollAnswerManager.SelectAnswerReport(PollId, -1);
        if (dtReport.Rows.Count > 0)
        {
            lblTotalAnswer.Text = "تعداد کل پاسخ ها : " + dtReport.Rows[0]["TotalAnswer"].ToString();
        }
    }
    #endregion
}