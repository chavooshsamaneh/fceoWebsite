using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Poll_PollQuestion : System.Web.UI.Page
{

    #region property
  int  PlId
    {
get
{
 return  Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlId"]));
}
}

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDelete();
        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.PollQuestionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());            

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnView.Enabled = btnView2.Enabled = Per.CanView;
            btnDelete2.Enabled = btnDelete.Enabled = Per.CanDelete;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnView"] = btnView.Enabled;
            this.ViewState["btnDelete"] = btnDelete.Enabled;

            if (Request.QueryString["PlId"] == null || Request.QueryString["PrePgMd"] == null)
            {
                Response.Redirect("Poll.aspx");
            }
            ObjdsbserverPollQuestion.SelectParameters["PollId"].DefaultValue = Utility.DecryptQS(Request.QueryString["PlId"]);
        }
        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];
        if (this.ViewState["btnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["btnDelete"];
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!CheckCondition())
        {
            return;  
        }
        NextPage("New");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int QuestionId = -1;
        int focucedIndex = GridViewPollQuestion.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewPollQuestion.GetDataRow(focucedIndex);
            QuestionId = (int)row["QuestionId"];
        }
        if (QuestionId == -1)
        {

            SetMessage("ابتدا یک رکورد را انتخاب نمائید");
            return;

        }
        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        TSP.DataManager.PollChoiseManager PollChoiseManager = new TSP.DataManager.PollChoiseManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(PollQuestionManager);
        TransactionManager.Add(PollChoiseManager);
        try
        {
            TransactionManager.BeginSave();

            PollQuestionManager.FindByCode(QuestionId);
            PollChoiseManager.FindByQuestionId(QuestionId);
            if (PollQuestionManager.Count != 1)
            {
                SetMessage("مقادیر صفحه نامعتبر است");
                TransactionManager.CancelSave();
                return;
            }
            int cnt = PollChoiseManager.Count;
            for (int i = 0; i < cnt; i++)
            {
                PollChoiseManager[0].Delete();
                PollChoiseManager.Save();
                PollChoiseManager.DataTable.AcceptChanges();
            }

            PollQuestionManager[0].Delete();
            PollQuestionManager.Save();
            PollQuestionManager.DataTable.AcceptChanges();


            TransactionManager.EndSave();
            GridViewPollQuestion.DataBind();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DeleteComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PollInsert.aspx?PlId=" + Request.QueryString["PlId"] + "&PgMd=" + Request.QueryString["PrePgMd"]);
    }

    protected void MenuDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "BaseInfo":
                Response.Redirect("PollInsert.aspx?PlId=" + Request.QueryString["PlId"] + "&PgMd=" + Request.QueryString["PrePgMd"]);
                break;

        }

    }
    #endregion

    #region Metods
    private void NextPage(string Mode)
    {
        int QuestionId = -1;
        int focucedIndex = GridViewPollQuestion.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewPollQuestion.GetDataRow(focucedIndex);
            QuestionId = (int)row["QuestionId"];
        }
        if (QuestionId == -1 && Mode != "New")
        {

            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                QuestionId = -1;
            }

            Response.Redirect("QuestionInsert.aspx?PlQId=" + Utility.EncryptQS(QuestionId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&PlId=" + Request.QueryString["PlId"] + "&PrePgMd=" + Request.QueryString["PrePgMd"]);
        }
    }

    private void SetWarningLableDelete()
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
    #endregion
}