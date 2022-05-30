using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_MembersRegister_RejectMembership : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMeNo.Text))
        {
            ShowMessage("لیست اعضا را وارد نمایید");
            return;
        }
        string[] MeList = txtMeNo.Text.Split(',');

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(trans);
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager= new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager= new TSP.DataManager.WorkFlowTaskManager();
        trans.Add(transferManager);
        trans.Add(MemberManager);
        trans.Add(MemberRequestManager);
        trans.Add(DocMemberFileManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(WorkFlowTaskManager);
        int MeId;
        string Complete = "";
        string InComplete = "";
        try
        {
            for (int i = 0; i < MeList.Length; i++)
            {
                MeId = Convert.ToInt32(MeList[i]);
                trans.BeginSave();
                DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
                if (DocMemberFileManager.Count > 0)
                {
                    InComplete += MeList[i]+",";
                    continue;
                }

                MemberManager.FindByCode(MeId);

                MemberRequestManager.DoAutomaticConfirmChangeMemberData(MeId, TSP.DataManager.MemberRequestType.Cancel, TSP.DataManager.MembershipRequest.Employee
                    , "آغاز گردش کار اتوماتیک سیستم جهت لغو عضویت شخص حقیقی", "تایید و پایان بررسی لغو عضویت شخص حقیقی توسط سیستم"
                    , Utility.GetCurrentUser_UserId(), "", "", "", "", "", MemberManager, transferManager, "");
                if (DocMemberFileManager.DoAutomaticConfirmChangeMemberFile(WorkFlowStateManager, WorkFlowTaskManager, MeId,
                        Utility.GetCurrentUser_UserId()) == -1)
                {

                    InComplete += MeList[i] + ",";
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
                Complete += MeList[i] + ",";
                trans.EndSave();

            }
            ShowMessage("ذخیره انجام شد");
            btnSave.Enabled = false;
            txtError.Text = InComplete;
            txtComplete.Text = Complete;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            trans.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }


    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
}