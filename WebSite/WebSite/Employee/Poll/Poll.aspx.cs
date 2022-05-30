using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Poll_Poll : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableWarning();
        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.PollPollManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission PerAnswer = TSP.DataManager.PollAnswerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnView.Enabled = btnView2.Enabled =GridViewPoll.Visible= Per.CanView;
            btnDelete2.Enabled = btnDelete.Enabled = Per.CanDelete;
            btnReportGrid.Enabled = btnReportGrid2.Enabled = PerAnswer.CanView;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnView"] = btnView.Enabled;
            this.ViewState["btnDelete"] = btnDelete.Enabled;
            this.ViewState["btnReportGrid"] = btnReportGrid.Enabled;

        }
        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = GridViewPoll.Visible = (bool)this.ViewState["btnView"];
        if (this.ViewState["btnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["btnDelete"];
        if (this.ViewState["btnReportGrid"] != null)
            this.btnReportGrid.Enabled = this.btnReportGrid2.Enabled = (bool)this.ViewState["btnReportGrid"];
       
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int PollId = -1;
        int focucedIndex = GridViewPoll.FocusedRowIndex;

        if (focucedIndex < 0)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        DataRow row = GridViewPoll.GetDataRow(focucedIndex);
        PollId = (int)row["PollId"];

        Delete(PollId);

    }

    protected void btnReportGrid_Click(object sender, EventArgs e)
    {
        int PollId = -1;
        int focucedIndex = GridViewPoll.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewPoll.GetDataRow(focucedIndex);
            PollId = (int)row["PollId"];
        }
        if (PollId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        Response.Redirect("ResualtTotalGrid.aspx?PId=" + Utility.EncryptQS(PollId.ToString()));
    }

    #endregion

    #region Metods
    private void NextPage(string Mode)
    {
        int PollId = -1;
        int focucedIndex = GridViewPoll.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewPoll.GetDataRow(focucedIndex);
            PollId = (int)row["PollId"];
        }
        if (PollId == -1 && Mode != "New")
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                PollId = -1;
            }

            Response.Redirect("PollInsert.aspx?PlId=" + Utility.EncryptQS(PollId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
        }
    }

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

    private void Delete(int PollId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        TSP.DataManager.PollChoiseManager PollChoiseManager = new TSP.DataManager.PollChoiseManager();
        TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
        TSP.DataManager.PollDisplayLocationsManager PollDisplayLocationsManager = new TSP.DataManager.PollDisplayLocationsManager();
        TSP.DataManager.pollAgentPoll pollAgentPoll = new TSP.DataManager.pollAgentPoll();
        TransactionManager.Add(PollPollManager);
        TransactionManager.Add(PollQuestionManager);
        TransactionManager.Add(PollChoiseManager);
        TransactionManager.Add(pollAgentPoll);        
        try
        {
            TransactionManager.BeginSave();
            PollAnswerManager.SearchPollAnswer(-1, -1, -1, PollId);
            if (PollAnswerManager.Count > 0)
            {
                SetMessage("بعلت ثبت پاسخ برای نظرسنجی انتخاب شده امکان حذف آن وجود ندارد.");
                return;
            }
            PollQuestionManager.FindByPollId(PollId);
            int QCount=PollQuestionManager.Count;
            for (int i = 0; i < QCount; i++)
            {
                int QuestionId = Convert.ToInt32(PollQuestionManager[0]["QuestionId"]);
                PollChoiseManager.FindByQuestionId(QuestionId);
                int ChoiseCnt= PollChoiseManager.Count;
                for (int j = 0; j < ChoiseCnt; j++)
                {
                    PollChoiseManager[0].Delete();
                    PollChoiseManager.Save();
                    PollChoiseManager.DataTable.AcceptChanges();
                }
                PollQuestionManager[0].Delete();
                PollQuestionManager.Save();
                PollQuestionManager.DataTable.AcceptChanges();
            }
            PollDisplayLocationsManager.FindByPollId(PollId);
            int DisCount = PollDisplayLocationsManager.Count;
            for (int i = 0; i < DisCount; i++)
            {
                PollDisplayLocationsManager[0].Delete();
                PollDisplayLocationsManager.Save();
                PollDisplayLocationsManager.DataTable.AcceptChanges();
            }

            pollAgentPoll.FindByPollId(PollId);
            int PollAgent = pollAgentPoll.Count;
            for (int i = 0; i < PollAgent; i++)
            {
                pollAgentPoll[0].Delete();
                pollAgentPoll.Save();
                pollAgentPoll.DataTable.AcceptChanges();
            }

            PollPollManager.FindByCode(PollId);
            if (PollPollManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                TransactionManager.CancelSave();
                return;
            }
            PollPollManager[0].Delete();
            PollPollManager.Save();
            TransactionManager.EndSave();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewPoll.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
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
    #endregion
}