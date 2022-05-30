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

public partial class ChangeForgottenPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Id = -1, UserId = -1;
            try
            {
                Id = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["Id"]).ToString());
                UserId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["Usr"]).ToString());
            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            CheckRequest(UserId, Id);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    Boolean CheckRequest(int UserId, int Id)
    {
        try
        {
            if (UserId > 0 && Id > 0)
            {
                TSP.DataManager.ResetPasswordManager ResetPasswordManager = new TSP.DataManager.ResetPasswordManager();
                ResetPasswordManager.FindByCode(Id);
                if (int.Parse(ResetPasswordManager[0]["RequestUserId"].ToString()) != UserId)
                {
                    ShowError("این درخواست معتبر نمی باشد");
                    return false;
                }
                else
                {
                    if (Utility.IsEmailLinkTimeOut(long.Parse(ResetPasswordManager[0]["RequestDateTimeDetail"].ToString())) == true)
                    {
                        ShowError("مهلت اعتبار درخواست شما به پایان رسیده است");
                        return false;
                    }
                    else if ((Boolean)ResetPasswordManager[0]["IsChangePass"] == true)
                    {
                        ShowError("این درخواست قبلا انجام شده است");
                        return false;
                    }
                    else
                    {
                        TSP.DataManager.LoginManager LoginManage = new TSP.DataManager.LoginManager();
                        LoginManage.FindByCode(UserId);
                        txtUserName.Text = LoginManage[0]["UserName"].ToString();
                        return true;
                    }
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return false;
            }
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return false;
        }
        return false;
    }

    Boolean CheckSecurityCode()
    {
        return Captcha.IsValid;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Id = -1, UserId = -1;
        try
        {
            Id = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["Id"]).ToString());
            UserId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["Usr"]).ToString());
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (!CheckRequest(UserId, Id))
            return;

        if (CheckSecurityCode() == false)
        {
            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ResetPasswordManager ResetManager = new TSP.DataManager.ResetPasswordManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();

        trans.Add(ResetManager);
        trans.Add(LogManager);

        try
        {

            LogManager.FindByCode(UserId);
            if (LogManager.Count == 1)
            {
                ResetManager.FindByCode(Id);
                if (ResetManager.Count == 0)
                {
                    trans.CancelSave();
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                trans.BeginSave();

                DataRow dr = ResetManager.NewRow();
                ResetManager[0].BeginEdit();
                ResetManager[0]["IsChangePass"] = true;
                ResetManager[0]["ChangeIpAddress"] = Request.UserHostAddress;
                ResetManager[0]["ChangeDate"] = Utility.GetDateOfToday();
                ResetManager[0]["ChangeTime"] = Utility.GetCurrentTime();
                ResetManager[0]["ChangeDateTime"] = DateTime.Now;
                ResetManager[0]["ModifiedDate"] = DateTime.Now;
                ResetManager[0].EndEdit();
                if (ResetManager.Save() > 0)
                {
                    LogManager[0].BeginEdit();
                    LogManager[0]["Password"] = Utility.EncryptPassword(txtPassword.Text);
                    LogManager[0]["ModifiedDate"] = DateTime.Now;
                    LogManager[0].EndEdit();
                    int save = LogManager.Save();
                    if (save == 1)
                    {
                        trans.EndSave();
                        ShowMessage("تغییر رمز عبور با موفقیت انجام شد");
                        return;
                    }
                    else
                    {
                        trans.CancelSave();
                        ShowInputError("خطایی در ذخیره انجام گرفته است");
                    }
                }
                else
                {
                    trans.CancelSave();
                    ShowInputError("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();

            Utility.SaveWebsiteError(err);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowInputError("اطلاعات تکراری می باشد");
                }
                else
                {
                    ShowInputError("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowInputError("خطایی در ذخیره انجام گرفته است");
            }
        }

        txtPassword.Text = "";
        txtPassword2.Text = "";
    }

    void ShowError(String Error)
    {
        PanelChangePassword.Visible = false;
        PanelMessage.Visible = true;
      //  lblMessage.ForeColor = System.Drawing.Color.DarkRed;
        lblMessage.InnerText = Error;
    }

    void ShowMessage(String Error)
    {
        PanelChangePassword.Visible = false;
        PanelMessage.Visible = true;
     //   lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
        lblMessage.InnerText = Error;
    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
    }
}
