using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Members_Amoozesh_SeminarRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;


    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string SeId = "";
        LinkButton lb = (LinkButton)sender;
        string[] Parameters = lb.CommandArgument.Split(';');
        SeId = Parameters[0].ToString();
        Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(SeId) + "&RetPage=" + Utility.EncryptQS("PerReg"));
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            ShowMessage("تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در سمینارهای آموزشی می باشند.");
            return;
        }
        LinkButton lb = (LinkButton)sender;
        int SeId = Convert.ToInt32(lb.CommandArgument);
        if(TSP.DataManager.PeriodRegisterManager.CheckIfRepititiveRegister(SeId, Utility.GetCurrentUser_MeId()))        
        {
            ShowMessage("شما قبلا در این سمینار ثبت نام کرده اید.");
            return;
        }
        System.Collections.ArrayList HasCappacity = TSP.DataManager.SeminarManager.HasCapacity(SeId);
        if (!Convert.ToBoolean( HasCappacity[0]))
        {
            ShowMessage(HasCappacity[1].ToString());
            return;
        }

        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("SeminarRegister") + "&SeId=" + Utility.EncryptQS(SeId.ToString()));

    }

    #endregion

    #region Methods

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    } 

    #endregion
}