using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_RecieveMagazineSetting : System.Web.UI.Page
{
    #region Methods
    private void FillInfo()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        if (MemberManager.GetRecieveMagazineType(Utility.GetCurrentUser_MeId()) == (int)TSP.DataManager.RecieveMagazineType.UnSelected)
            rbtListRecieveMagazineType.SelectedIndex = rbtListRecieveMagazineType.Items.FindByValue(((int)TSP.DataManager.RecieveMagazineType.Yes).ToString()).Index;
        else if (Utility.IsDBNullOrNullValue(MemberManager[0]["RecieveMagazine"]) == false)
            rbtListRecieveMagazineType.SelectedIndex = rbtListRecieveMagazineType.Items.FindByValue(Convert.ToInt32(MemberManager[0]["RecieveMagazine"]).ToString()).Index;
    }

    private void SetlblWarningMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;// "خطایی در ذخیره انجام گرفته است";
    }

    private void SetError(Exception err)
    {
        String Error = Utility.Messages.GetExceptionError(err);
        if (String.IsNullOrWhiteSpace(Error) == false)
        {
            SetlblWarningMessage(Error);
        }
        else
        {
            SetlblWarningMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
            {
                Response.Redirect("~/Login.aspx");
            }
            FillInfo();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (rbtListRecieveMagazineType.SelectedItem.Value == null)
        {
            SetlblWarningMessage("ابتدا یک گزینه را انتخاب نمایید");
            return;
        }

        TSP.DataManager.TransactionManager TransactManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
        TransactManager.Add(MemberManager);
        TransactManager.Add(PollAnswerManager);
        try
        {
            int ChsId = -1;
            int UserId = Utility.GetCurrentUser_UserId();
            TransactManager.BeginSave();
            MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
            int OldRecieveMagazineType = Convert.ToInt32(MemberManager[0]["RecieveMagazine"]);
            if (MemberManager.Count == 1)
            {
                MemberManager[0].BeginEdit();
                MemberManager[0]["RecieveMagazine"] = rbtListRecieveMagazineType.SelectedItem.Value;
                MemberManager[0].EndEdit();
                if (MemberManager.Save() > 0)
                {
                    //---hp-----insert pollanswer---------           
                    if (Convert.ToInt32(rbtListRecieveMagazineType.SelectedItem.Value) == 1)
                        ChsId = (int)TSP.DataManager.PollChoiseRecieveMagazine.Yes;
                    else ChsId = (int)TSP.DataManager.PollChoiseRecieveMagazine.No;
                    if (!PollAnswerManager.Insert(ChsId, UserId))
                    {
                        TransactManager.CancelSave();
                        SetlblWarningMessage("خطایی در ذخیره انجام گرفته است");
                        return;
                    }
                    //-------------------------------------
                    MemberManager.DataTable.AcceptChanges();
                    PollAnswerManager.DataTable.AcceptChanges();
                    TransactManager.EndSave();

                    if (OldRecieveMagazineType == (int)TSP.DataManager.RecieveMagazineType.UnSelected)
                        Response.Redirect("~/Login.aspx", false);
                    else
                    {
                        SetlblWarningMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    }
                }
                else
                {
                    TransactManager.CancelSave();
                    SetlblWarningMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }

        }
        catch (Exception err)
        {
            TransactManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    #endregion
}