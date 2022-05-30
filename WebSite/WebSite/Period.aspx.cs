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

public partial class Period : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {      
        string InsId = "";
        string PPId = "";
        LinkButton lb = (LinkButton)sender;
        string[] Parameters = lb.CommandArgument.Split(';');
        PPId = Parameters[0].ToString();
        InsId = Parameters[1].ToString();
        string PType = Parameters[2].ToString();
       if(PType=="0") 
        Response.Redirect("PeriodsView.aspx?PPId=" + Utility.EncryptQS(PPId) + "&InsId=" + Utility.EncryptQS(InsId)+"&PrePg="+Utility.EncryptQS("Period"));
       else if(PType=="1")
           Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(PPId) + "&InsId=" + Utility.EncryptQS(InsId) + "&PrePg=" + Utility.EncryptQS("Period"));
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت ثبت نام در دوره از طریق این لینک بایستی از طریق پرتال اعضا وارد شوید.";
            return;
        }
        LinkButton lb = (LinkButton)sender;
        Response.Redirect("~/Members/Amoozesh/AddPeriodRegister.aspx?PPId=" + Utility.EncryptQS(lb.CommandArgument) + "&PgMd=" + Utility.EncryptQS("New"));
    }
    #endregion
}