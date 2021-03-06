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

public partial class Members_ChengeUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            FillForm(Utility.GetCurrentUser_UserId());
        }

    }

    protected void FillForm(int id)
    {
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        LogManager.FindByCode(id);
        if (LogManager.Count == 1)
        {
            UserName.Text = LogManager[0]["UserName"].ToString();
        }

    }

    protected void btnOfUNRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindUserName(UserName.Text, FormsAuthentication.HashPasswordForStoringInConfigFile(OldPassword.Text, "sha1"));
            if (LogManager.Count == 1)
            {
                LogManager[0].BeginEdit();
                LogManager[0]["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text, "sha1");
                LogManager[0].EndEdit();
                int save = LogManager.Save();
                if (save == 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        string ReferPage = Server.HtmlEncode(Request.QueryString["ReferPage"]).ToString();
        if (ReferPage != null)
            Response.Redirect(ReferPage);

    }
}
