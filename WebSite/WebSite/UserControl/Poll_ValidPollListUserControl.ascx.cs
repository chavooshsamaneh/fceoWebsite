using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DevExpress.Web;

public partial class UserControl_Poll_ValidPollListUserControl : System.Web.UI.UserControl
{
    #region Properties
    private TSP.DataManager.FormBuilder.DisplayLocationTypesManager.Types _DisplayLocationType;
    [Browsable(true), Category("TSP")]
    public TSP.DataManager.FormBuilder.DisplayLocationTypesManager.Types DisplayLocationType
    {
        get
        {
            return this._DisplayLocationType;
        }
        set
        {
            this._DisplayLocationType = value;
        }
    }



    private int _QuestionCountType;
    [Browsable(true), Category("TSP")]
    public int QuestionCountType
    {
        get
        {
            return this._QuestionCountType;
        }
        set
        {
            this._QuestionCountType = value;
        }
    }

    private int _DataviewColumnCount;
    [Browsable(true), Category("TSP")]
    public int DataviewColumnCount
    {
        get
        {
            return this._DataviewColumnCount;
        }
        set
        {
            this._DataviewColumnCount = value;
        }
    }

    private int _DataviewItemWidth;
    [Browsable(true), Category("TSP")]
    public int DataviewItemWidth
    {
        get
        {
            return this._DataviewItemWidth;
        }
        set
        {
            this._DataviewItemWidth = value;
        }
    }


    private int _DataviewItemHeight;
    [Browsable(true), Category("TSP")]
    public int DataviewItemHeight
    {
        get
        {
            return this._DataviewItemHeight;
        }
        set
        {
            this._DataviewItemHeight = value;
        }
    }
    private int _DataviewItemSpacing;
    [Browsable(true), Category("TSP")]
    public int DataviewItemSpacing
    {
        get
        {
            return this._DataviewItemSpacing;
        }
        set
        {
            this._DataviewItemSpacing = value;
        }
    }


    private string _NextPageURLPrefix;
    [Browsable(true), Category("TSP")]
    public string NextPageURLPrefix
    {
        get
        {
            return this._NextPageURLPrefix;
        }
        set
        {
            this._NextPageURLPrefix = value;
        }
    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {         
        LoadData();
    }

    protected void btnTitle_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        if (Utility.GetCurrentUser_LoginType() != -1)
            Response.Redirect("~/Poll/AnswerPollQuestion.aspx?PId=" + Utility.EncryptQS(lb.CommandArgument));
        else
            Response.Redirect("~/Poll/AnswerPollQuestionForHomePage.aspx?PId=" + Utility.EncryptQS(lb.CommandArgument));
    }


    protected void btnViewResualt_OnLoad(object sender, EventArgs e)
    {
        Boolean Visiblity = true;
        LinkButton btnResultView = (LinkButton)sender;
        int PollId = Convert.ToInt32(btnResultView.CommandArgument);
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.FindByCode(PollId);
        if (PollPollManager.Count == 1)
        {
            int ResualtPublice = Convert.ToInt32(PollPollManager[0]["IsResultPublic"]);
            if (Utility.GetCurrentUser_LoginType() == -1 && ResualtPublice != 2)
            {
                Visiblity = false;
            }
            else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member && ResualtPublice == 0)
            {
                Visiblity = false;
            }
        }
        else
            Visiblity = false;
        btnResultView.Visible = Visiblity;
    }
    #endregion

    #region Methods
    private void LoadData()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.SelectPollForShowingToUsers(Utility.GetDateOfToday(), (int)DisplayLocationType, QuestionCountType, Utility.GetCurrentUser_AgentId());
        if (PollPollManager.Count > 0)
        {
            RepeaterPoll.DataSource = PollPollManager.DataTable;
            RepeaterPoll.DataBind();
            divMultiPoll.Visible = true;              
        }
        else
            divMultiPoll.Visible = false;
    }

  
    #endregion


  
}