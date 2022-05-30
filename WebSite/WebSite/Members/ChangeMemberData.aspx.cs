using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_ChangeMemberData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (IsPostBack == false)
        {
            LoadData();
        }
    }

    void LoadData()
    {
        Boolean Flag = false;
        try
        {
            lblSetMemberDataDescription.Text = "کاربر گرامی، جهت برقراری ارتباط با شما از طریق سیستم پیام کوتاه و پیام الکترونیکی، دارا بودن شماره تلفن همراه و آدرس پست الکترونیکی الزامی می باشد :";

            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(Utility.GetCurrentUser_MeId());

            if (Utility.IsDBNullOrNullValue(MeManager[0]["Email"]))
            {
                Flag = true;
                txtEmail.Focus();
            }
            else
            {
                txtEmail.Text = MeManager[0]["Email"].ToString();
                lblSetMemberDataDescription.Text = "کاربر گرامی، جهت برقراری ارتباط با شما از طریق سیستم پیام کوتاه، دارا بودن شماره تلفن همراه الزامی می باشد :";
                PanelEmail.Visible = false;
            }

            if (Utility.IsDBNullOrNullValue(MeManager[0]["MobileNo"]))
            {
                Flag = true;
                txtMobileNo.Focus();
            }
            else
            {
                txtMobileNo.Text = MeManager[0]["MobileNo"].ToString();
                lblSetMemberDataDescription.Text = "کاربر گرامی، جهت برقراری ارتباط با شما از طریق پیام الکترونیکی، دارا بودن آدرس پست الکترونیکی الزامی می باشد :";
                PanelMobileNo.Visible = false;
            }
        }
        catch (Exception) { }

        if (Flag == false)
            Response.Redirect("~/Login.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        try
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(Transaction);
            TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
            Transaction.Add(TransferMemberManager);
            Transaction.Add(LoginManager);
            Transaction.Add(MemberManager);
            Transaction.Add(MemberRequestManager);

            int CurrentMeReqId = -2;
            string Email = txtEmail.Text.Trim();
            string MobileNo = txtMobileNo.Text.Trim();
            int MeId = Utility.GetCurrentUser_MeId();

            Transaction.BeginSave();

            MemberManager.FindByCode(MeId);
            MemberManager[0].BeginEdit();
            if (PanelEmail.Visible)
                if (String.IsNullOrEmpty(txtEmail.Text.Trim()) == false)
                    MemberManager[0]["Email"] = Email;
            if (PanelMobileNo.Visible)
                if (String.IsNullOrEmpty(txtMobileNo.Text.Trim()) == false)
                    MemberManager[0]["MobileNo"] = MobileNo;
            MemberManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            MemberManager[0]["ModifiedDate"] = DateTime.Now;
            MemberManager[0].EndEdit();
            if (MemberManager.Save() <= 0)
            {
                ShowMessage("خطایی در ذخیره اطلاعات انجام گرفته است");
                Transaction.CancelSave();
                return;
            }
            //------------update last not confirm member request--------
            MemberRequestManager.FindByMemberId(MeId, 0, -1);
            if (MemberRequestManager.Count == 1)
            {
                MemberRequestManager[0].BeginEdit();

                if (PanelEmail.Visible)
                    if (String.IsNullOrEmpty(txtEmail.Text.Trim()) == false)
                        MemberRequestManager[0]["Email"] = Email;

                if (PanelMobileNo.Visible)
                    if (String.IsNullOrEmpty(txtMobileNo.Text.Trim()) == false)
                        MemberRequestManager[0]["MobileNo"] = MobileNo;

                MemberRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MemberRequestManager[0]["ModifiedDate"] = DateTime.Now;
                MemberRequestManager[0].EndEdit();
                if (MemberRequestManager.Save() <= 0)
                {
                    ShowMessage("خطایی در ذخیره اطلاعات انجام گرفته است");
                    Transaction.CancelSave();
                    return;
                }
            }else
            if (MemberRequestManager.DoAutomaticConfirmChangeMemberData(Utility.GetCurrentUser_MeId(), TSP.DataManager.MemberRequestType.Request, TSP.DataManager.MembershipRequest.Member, "آغاز گردش کار اتوماتیک سیستم جهت تغییر تغییر اطلاعات شخص حقیقی", "تایید و پایان بررسی تغییر اطلاعات شخص حقیقی توسط سیستم"
                 , Utility.GetCurrentUser_UserId(), "", "", Email, MobileNo, "", MemberManager, TransferMemberManager, "", true, ref CurrentMeReqId, TSP.DataManager.WorkFlowTask.ConfirmMemberAndEndProccess) == 0)
            {
                if (PanelEmail.Visible)
                {
                    LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                    LoginManager[0].BeginEdit();
                    LoginManager[0]["Email"] = Email;
                    LoginManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
                    LoginManager[0]["ModifiedDate"] = DateTime.Now;
                    LoginManager[0].EndEdit();
                    if (LoginManager.Save() <= 0)
                    {
                        ShowMessage("خطایی در ذخیره اطلاعات انجام گرفته است");
                        Transaction.CancelSave();
                        return;
                    }
                }
                
            }
            Transaction.EndSave();
        }
        catch (Exception err)
        {
            Transaction.CancelSave();
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
            return;
        }

        Response.Redirect("~/Login.aspx");
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}