using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Poll_QuestionInsert : System.Web.UI.Page
{
    #region Properties
    string PageMode
    {
        set
        {
            HiddenFieldPoll["PageMode"] = value;
        }
        get
        {
            return HiddenFieldPoll["PageMode"].ToString();
        }
    }
    int PlQId
    {
        set
        {
            HiddenFieldPoll["PlQId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldPoll["PlQId"]);
        }
    }
    int PlId
    {
        set
        {
            HiddenFieldPoll["PollId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldPoll["PollId"]);
        }
    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        SetWarningLableWarning();

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.PollQuestionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission Perchoise = TSP.DataManager.PollChoiseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnSave.Enabled = btnSave2.Enabled = Per.CanNew || Per.CanEdit;
            btnAddChoise.Enabled = Perchoise.CanNew || Perchoise.CanEdit;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
            this.ViewState["btnAddChoise"] = btnAddChoise.Enabled;

            Session["PollChoiseManager"] = GetChoiseManger();
            SetKey();
            SetMode();
        }

        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        if (this.ViewState["btnAddChoise"] != null)
            this.btnAddChoise.Enabled = (bool)this.ViewState["btnAddChoise"];
        BindChoiseGrid();
        CheckQuestionType();

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!CheckCondition())
        {
            return;
        }

            PageMode = "New";
            SetMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PageMode = "Edit";
        SetMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckExistchoise())
        {
            return; 
        }
        if (PageMode == "New")
            Insert();
        else if (PageMode == "Edit")
        {
            Update(PlQId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PollQuestion.aspx?PlId=" + Request.QueryString["PlId"] + "&PrePgMd=" + Request.QueryString["PrePgMd"]);
    }

    protected void btnAddChoise_Click(object sender, EventArgs e)
    {
        AddChoise();
    }

    protected void GridViewChoise_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        if (Session["PollChoiseManager"] == null)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return ;
        }
        TSP.DataManager.PollChoiseManager PollChoiseManager = (TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"];
        DataRow dr = PollChoiseManager.DataTable.Rows.Find(e.Keys["ChoiseId"]);
        dr.Delete();
        e.Cancel = true;
        GridViewChoise.CancelEdit();
        BindChoiseGrid();

    }


    #endregion

    #region Method

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
        if (Request.QueryString["PgMd"] == null || Request.QueryString["PlQId"] == null)
        {
            Response.Redirect("Poll.aspx");
            return;
        }
        PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
        PlQId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlQId"].ToString()));
        PlId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlId"].ToString()));

    }

    private void SetMode()
    {
        switch (PageMode)
        {
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
            case "View":
                SetViweMode();
                break;
        }
    }

    private void SetNewMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "جدید";
        //Set ControlEnable
        SetControlEnable(true);
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        btnAddChoise.Enabled = true;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        this.ViewState["btnAddChoise"] = btnAddChoise.Enabled;
        //Clear Form
        ClearForm();
    }

    private void SetEditMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "ویرایش";
        //Set ControlEnable
        SetControlEnable(true);
        ////FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        btnAddChoise.Enabled = true;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        this.ViewState["btnAddChoise"] = btnAddChoise.Enabled;

    }

    private void SetViweMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "مشاهده";
        //FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = false;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = true;
        btnAddChoise.Enabled = false;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        this.ViewState["btnAddChoise"] = btnAddChoise.Enabled;
        //Set ControlEnable
        SetControlEnable(false);
    }

    private void ClearForm()
    {
        txtQuestion.Text = "";
        chbIsCompulsory.Checked = true;
        txtChoice.Text = "";
        Session["PollChoiseManager"] = GetChoiseManger();
        BindChoiseGrid();


    }

    private void SetControlEnable(Boolean Enabled)
    {
        txtQuestion.Enabled = Enabled;
        chbIsCompulsory.Enabled = Enabled;
        txtChoice.Enabled = Enabled;
        GridViewChoise.Columns["clmnDelete"].Visible = Enabled;
    }

    private TSP.DataManager.PollChoiseManager GetChoiseManger()
    {
        return new TSP.DataManager.PollChoiseManager();
    }

    private void AddChoise()
    {
        try
        {
            if (Session["PollChoiseManager"] == null)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
                return;
            }

            TSP.DataManager.PollChoiseManager PollChoiseManager = (TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"];
            DataRow dr = PollChoiseManager.NewRow();
            if (txtChoice.Text != null)
            {
                dr["ChoiseName"] = txtChoice.Text;
                dr["QuestionId"] = -1;
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
            }
            PollChoiseManager.AddRow(dr);
            //GridViewChoise.DataSource = PollChoiseManager.DataTable;
            //GridViewChoise.DataBind();
            Session["PollChoiseManager"] = PollChoiseManager;
            BindChoiseGrid();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در اضافه کردن به لیست ایجاد شد");
        }
        txtChoice.Text = "";
    }

    private void BindChoiseGrid()
    {
        if (Session["PollChoiseManager"] == null)
            return;
        GridViewChoise.DataSource = ((TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"]).DataTable;
        GridViewChoise.DataBind();
    }

    private bool CheckCondition()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollPollManager.FindByCode(PlId);
        PollQuestionManager.FindByPollId(PlId);
        if (PollPollManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        int countType = Convert.ToInt32(PollPollManager[0]["QuestionCountType"]);
        if (countType == 0 && PollQuestionManager.Count > 0)
        { 
            SetMessage("بر اساس تنظیمات برای این نظر سنجی بیش از یک سوال نمی توان تعریف کرد");
            return false;
           
        }
        else
        {
            return true;
        }
    }

    private bool CheckExistchoise()
    {
        if (Session["PollChoiseManager"] == null)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return false;
        }
        TSP.DataManager.PollChoiseManager PollChoiseManager = (TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"];
        if ( PollChoiseManager.Count < 1)
        {
            SetMessage("گزینه های سوال را تعریف نمایید");
            return false;
        }
        else return true;
    }
    private void CheckQuestionType()
{
    TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
    PollPollManager.FindByCode(PlId);
    if (PollPollManager.Count != 1)
    {
        SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        
    }
    int countType = Convert.ToInt32(PollPollManager[0]["QuestionCountType"]);
    if (countType == 0 )
    {

        chbIsCompulsory.Enabled = false;
    }

}
    #region Viwe
    private void FillForm()
    {
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollQuestionManager.FindByCode(PlQId);
        if (PollQuestionManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(PollQuestionManager[0]["Question"]))
            txtQuestion.Text = PollQuestionManager[0]["Question"].ToString();
        if (!Utility.IsDBNullOrNullValue(PollQuestionManager[0]["Compulsory"]))
            chbIsCompulsory.Checked = Convert.ToBoolean(PollQuestionManager[0]["Compulsory"]);

        #region Choise viwe
        TSP.DataManager.PollChoiseManager PollChoiseManager = new TSP.DataManager.PollChoiseManager();
        PollChoiseManager.FindByQuestionId(PlQId);
        Session["PollChoiseManager"] = PollChoiseManager;
        BindChoiseGrid();

        #endregion
    }

    #endregion

    #region Insert-Update

    private void Insert()
    {
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        TSP.DataManager.PollChoiseManager PollChoiseManager = (TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"];
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(PollQuestionManager);
        TransactionManager.Add(PollChoiseManager);
        try
        {
            TransactionManager.BeginSave();

            if (PollChoiseManager.Count < 2)
            {
                SetMessage("حداقل تعداد گزینه ها بایستی دو مورد باشد");
                TransactionManager.CancelSave();
                return;
            }
            DataRow dr = PollQuestionManager.NewRow();
            if (txtQuestion.Text != null)
                dr["Question"] = txtQuestion.Text;
            dr["Compulsory"] = chbIsCompulsory.Checked;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["PollId"] = PlId;

            PollQuestionManager.AddRow(dr);
            PollQuestionManager.Save();
            PollQuestionManager.DataTable.AcceptChanges();
            int QuestionId = Convert.ToInt32(PollQuestionManager[PollQuestionManager.Count - 1]["QuestionId"]);
            for (int i = 0; i < PollChoiseManager.Count; i++)
            {
                PollChoiseManager[i]["QuestionId"] = QuestionId;
            }
            PollChoiseManager.Save();
            PollChoiseManager.DataTable.AcceptChanges();


            TransactionManager.EndSave();
            SetEditMode();

            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void Update(int QuestionId)
    {
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        TSP.DataManager.PollChoiseManager PollChoiseManager = (TSP.DataManager.PollChoiseManager)Session["PollChoiseManager"];
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(PollQuestionManager);
        TransactionManager.Add(PollChoiseManager);
        try
        {
            TransactionManager.BeginSave();
            if (PollChoiseManager.Count < 2)
            {
                SetMessage("حداقل تعداد گزینه ها بایستی دو مورد باشد");
                TransactionManager.CancelSave();
                return;
            }
            PollQuestionManager.FindByCode(QuestionId);
            if (PollQuestionManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            PollQuestionManager[0].BeginEdit();

            if (txtQuestion.Text != null)
                PollQuestionManager[0]["Question"] = txtQuestion.Text;
            PollQuestionManager[0]["Compulsory"] = chbIsCompulsory.Checked;
            PollQuestionManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PollQuestionManager[0]["ModifiedDate"] = DateTime.Now;
            PollQuestionManager[0]["PollId"] = PlId;
            PollQuestionManager[0].EndEdit();

            PollQuestionManager.Save();
            PollQuestionManager.DataTable.AcceptChanges();

            #region Choise Edit

            //  int QuestionId = Convert.ToInt32(PollQuestionManager[PollQuestionManager.Count - 1]["QuestionId"]);
            for (int i = 0; i < PollChoiseManager.Count; i++)
            {
                PollChoiseManager[i].BeginEdit();
                PollChoiseManager[i]["QuestionId"] = QuestionId;
                PollChoiseManager[i].EndEdit();
            }

            PollChoiseManager.Save();
            PollChoiseManager.DataTable.AcceptChanges();

            #endregion

            TransactionManager.EndSave();

            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    #endregion
    #endregion
}
