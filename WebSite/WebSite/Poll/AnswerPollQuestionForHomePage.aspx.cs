using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class Poll_AnswerPollQuestionForHomePage : System.Web.UI.Page
{
    private int PollId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["PollId"]);
        }
        set
        {
            HiddenFieldPage["PollId"] = value;
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableWarning();
        if (!IsPostBack)
        {
            if (Request.QueryString["PId"] == null || Request.QueryString["PId"].ToString() == "")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), true);
                return;
            }
            PollId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PId"]));
            LoadPollInfo();
            SetPanelVisible(true);
        }
    }

    protected void btnSavePollAnswer_OnLoad(object sender, EventArgs e)
    {
        //DevExpress.Web.ASPxButton btnSavePollAnswer = sender as DevExpress.Web.ASPxButton;
        //DataViewItemTemplateContainer container = btnSavePollAnswer.NamingContainer as DataViewItemTemplateContainer;        
        //btnSavePollAnswer.ClientSideEvents.Click = string.Format("function(s, e){{ DataViewPollQuestion.PerformCallback('Save;{0}')}}", container.ItemIndex);
    }

    protected void btnSavePollAnswer_OnClick(object sender, EventArgs e)
    {
        try
        {
            DevExpress.Web.ASPxRadioButtonList rdbChoise;
            DevExpress.Web.ASPxMemo txtDescription;
            TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
            if (Utility.GetCurrentUser_UserId() != -1)
            {
                PollAnswerManager.SearchPollAnswer(-1, -1, Utility.GetCurrentUser_UserId(), PollId);
                if (PollAnswerManager.Count > 0)
                {
                    SetMessage("شما پیش از این به این نظرسنجی پاسخ داده اید.");
                    return;
                }
            }

            for (int i = 0; i < DataViewPollQuestion.Items.Count; i++)
            {
                rdbChoise = (DataViewPollQuestion.FindItemControl("rdbChoise", DataViewPollQuestion.Items[i]) as DevExpress.Web.ASPxRadioButtonList);
                if (rdbChoise.SelectedItem == null)
                    continue;
                txtDescription = (DataViewPollQuestion.FindItemControl("txtDescription", DataViewPollQuestion.Items[i]) as DevExpress.Web.ASPxMemo);
                DataRow dr = PollAnswerManager.NewRow();
                dr["ChoiseId"] = rdbChoise.SelectedItem.Value;
                dr["AnswerDate"] = Utility.GetDateOfToday();
                dr["AnswerTime"] = Utility.GetCurrentTime();
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                if (txtDescription != null)
                    dr["Description"] = txtDescription.Text;
                PollAnswerManager.AddRow(dr);
                PollAnswerManager.Save();
                rdbChoise.SelectedItem = null;
            }
            //  SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));            
            SetPanelVisible(false);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
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

    private void LoadPollInfo()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.FindByCode(PollId);
        if (PollPollManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        RoundPanelPollAnswer.HeaderText = "نظرسنجی: " + PollPollManager[0]["Tittle"].ToString();
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollQuestionManager.FindByPollId(PollId);
        if (PollQuestionManager.Count > 0)
        {
            DataViewPollQuestion.DataSource = PollQuestionManager.DataTable;
            // DataViewPollQuestion.DataBind();

        }
        //ObjdsPollQuestion.SelectParameters["PollId"].DefaultValue = Utility.DecryptQS(Request.QueryString["PId"]);
        DataViewPollQuestion.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Visible">PanelQuestionsVisible</param>
    private void SetPanelVisible(Boolean Visible)
    {
        PanelQuestions.Visible = Visible;
        PanelSucces.Visible = !Visible;
    }
    #endregion
}