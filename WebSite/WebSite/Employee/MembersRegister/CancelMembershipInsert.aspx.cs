using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_MembersRegister_CancelMembershipInsert : System.Web.UI.Page
{

    public int MeId
    {
        get
        {
            return int.Parse(HiddenFieldPage["MeId"].ToString());
        }
        set
        {
            HiddenFieldPage["MeId"] =value.ToString();
        }
    }


    public string PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString.Count != 0)
                {
                    MeId = int.Parse(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
                    PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
                    LoadMemberInfo();
                }
                else
                {
                    Response.Redirect("CancelMembership.aspx");
                }
            }
            catch
            {
                Response.Redirect("CancelMembership.aspx");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CancelMembership.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(trans);
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        trans.Add(transferManager);
        trans.Add(MemberManager);
        trans.Add(MemberRequestManager);

        if (Utility.IsDBNullOrNullValue(MeId))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        try
        {
            trans.BeginSave();
            MemberManager.FindByCode(MeId);
            switch (PageMode)
            {
                case "Cancel":
                    MemberRequestManager.DoAutomaticConfirmChangeMemberData(MeId, TSP.DataManager.MemberRequestType.CancelDebtorMember, TSP.DataManager.MembershipRequest.Employee
                        , "آغاز گردش کار اتوماتیک سیستم جهت قطع عضویت شخص حقیقی", "تایید و پایان بررسی قطع عضویت شخص حقیقی توسط سیستم"
                        , Utility.GetCurrentUser_UserId(), "", "", "", "", "", MemberManager, transferManager,"");
                    break;
                case "Activate":
                    MemberRequestManager.DoAutomaticConfirmChangeMemberData(MeId, TSP.DataManager.MemberRequestType.ActivateDebtorMember, TSP.DataManager.MembershipRequest.Employee
                 , "آغاز گردش کار اتوماتیک سیستم جهت فعالسازی عضویت شخص حقیقی", "تایید و پایان بررسی فعالسازی عضویت شخص حقیقی توسط سیستم"
                 , Utility.GetCurrentUser_UserId(), "", "", "", "", "", MemberManager, transferManager,"");
                    break;
            }
          
         
            trans.EndSave();
            ShowMessage("ذخیره انجام شد");
            btnSave.Enabled = btnSave2.Enabled = false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            trans.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void LoadMemberInfo()
    {
        if (Utility.IsDBNullOrNullValue(MeId))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "Cancel":
                RoundPanelPage.HeaderText = "درخواست قطع عضویت";
                break;
            case "Activate":
                RoundPanelPage.HeaderText = "درخواست فعالسازی عضویت";
                break;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count > 0)
        {
            lblFirstName.Text = MemberManager[0]["FirstName"].ToString();
            lblLastName.Text = MemberManager[0]["LastName"].ToString();
            lblMeId.Text = MemberManager[0]["MeId"].ToString();
            lblSSN.Text = MemberManager[0]["SSN"].ToString();
            if ((!string.IsNullOrEmpty(MemberManager[0]["ImageUrl"].ToString())))
            {
                imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            }
            //if (!Utility.IsDBNullOrNullValue(MemberManager[0]["BankAccNo"]))
            //{
            //    txtBanckNo.Text = MemberManager[0]["BankAccNo"].ToString();
            //}
            txtDebt.Text = TSP.DataManager.Utility.CheckMemberOfflineDebt(MeId) + "ریال";
        }
    }

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
}