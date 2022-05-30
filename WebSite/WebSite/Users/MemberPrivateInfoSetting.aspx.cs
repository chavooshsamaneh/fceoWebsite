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

public partial class Users_MemberPrivateInfoSetting : System.Web.UI.Page
{
    #region Methods
    private void FillInfo()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        if (MemberManager.GetUserInfoType(Utility.GetCurrentUser_MeId()) == (int)TSP.DataManager.MemberPrivateInfoSettingType.UnSelected)
            rbtListUserInfoType.SelectedIndex = rbtListUserInfoType.Items.FindByValue(((int)TSP.DataManager.MemberPrivateInfoSettingType.NonOfTheme).ToString()).Index;
        else
            rbtListUserInfoType.SelectedIndex = rbtListUserInfoType.Items.FindByValue(Convert.ToInt32(MemberManager[0]["UserInfoType"]).ToString()).Index;
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
        if (rbtListUserInfoType.SelectedItem.Value == null)
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
            TransactManager.BeginSave();
            int ChsId = Convert.ToInt32(rbtListUserInfoType.SelectedItem.Value);
            int UserId = Utility.GetCurrentUser_UserId();
            MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
            int OldUserInfoType = Convert.ToInt32(MemberManager[0]["UserInfoType"]);
            if (MemberManager.Count != 1)
            {
                TransactManager.CancelSave();
                SetlblWarningMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            MemberManager[0].BeginEdit();
            MemberManager[0]["UserInfoType"] = ChsId;
            MemberManager[0].EndEdit();
            if (MemberManager.Save() > 0)
            {
                //---hp-----insert pollanswer-----
                switch (Convert.ToInt32(rbtListUserInfoType.SelectedItem.Value))
                {
                    case 1:
                        ChsId = (int)TSP.DataManager.PollChoisePrivateInfoSetting.AddressAndTell;
                        break;
                    case 2:
                        ChsId = (int)TSP.DataManager.PollChoisePrivateInfoSetting.Tell;
                        break;
                    case 3:
                        ChsId = (int)TSP.DataManager.PollChoisePrivateInfoSetting.Address;
                        break;
                    case 4:
                        ChsId = (int)TSP.DataManager.PollChoisePrivateInfoSetting.None;
                        break;
                }
                if (!PollAnswerManager.Insert(ChsId, UserId))
                {
                    TransactManager.CancelSave();
                    SetlblWarningMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
                //--------------------------------

                MemberManager.DataTable.AcceptChanges();
                PollAnswerManager.DataTable.AcceptChanges();
                TransactManager.EndSave();

                if (OldUserInfoType == (int)TSP.DataManager.MemberPrivateInfoSettingType.UnSelected)
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
        catch (Exception err)
        {
            TransactManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    #endregion
}
