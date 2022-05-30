using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_MembersRegister_MembersBankAccNoInsert : System.Web.UI.Page
{

    public int MeId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldPage["MeId"].ToString()));
        }
        set
        {
            HiddenFieldPage["MeId"] = Utility.EncryptQS(value.ToString());
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
                    LoadMemberInfo();
                }
                else
                {
                    Response.Redirect("MembersBankAccNo.aspx");
                }
            }
            catch
            {
                Response.Redirect("MembersBankAccNo.aspx");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MembersBankAccNo.aspx");
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
            string Msg = "";
            if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
            {
                ShowMessage(Msg);
                return;
            }
            trans.BeginSave();
            MemberManager.FindByCode(MeId);
            MemberRequestManager.DoAutomaticConfirmChangeMemberData(MeId, Utility.GetCurrentUser_UserId(),TSP.DataManager.MemberRequestType.BankAccNoChange,txtBanckNo.Text.Trim(), MemberManager,transferManager);
            trans.EndSave();
            ShowMessage("ذخیره انجام شد");
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
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["BankAccNo"]))
            {
                txtBanckNo.Text = MemberManager[0]["BankAccNo"].ToString();
            }
        }
    }

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
}