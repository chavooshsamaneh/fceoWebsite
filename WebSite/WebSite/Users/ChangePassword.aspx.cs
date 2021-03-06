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
                MenuUserSettings.Items.FindByName("MemberPrivateInfoSetting").Visible = false;
                MenuUserSettings.Items.FindByName("RecieveMagazineSetting").Visible = false;                
            }
            txtUserName.Text = Utility.GetCurrentUser_Username();
        }
        txtOldPass.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindUserName(txtUserName.Text, Utility.EncryptPassword(txtOldPass.Text));

            if (LogManager.Count == 1)
            {
                LogManager[0].BeginEdit();
                LogManager[0]["Password"] = Utility.EncryptPassword(txtPassword.Text);
                LogManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
                LogManager[0]["ModifiedDate"] = DateTime.Now;
                LogManager[0].EndEdit();
                int save = LogManager.Save();
                if (save == 1)
                {
                    LogManager.DataTable.AcceptChanges();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    txtOldPass.Text = "";
                 
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
                this.LabelWarning.Text = "رمز عبور فعلی اشتباه است";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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

    #endregion

    #region Methods
    
    #endregion
}