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

public partial class Employee_MembersRegister_MemberResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Cache["CachePersonMember"] == null)
                Cache["CachePersonMember"] = new object();
        

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        int MeId = -1;
        string IdNo = "";

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            MeId = (int)row["MeId"];
            IdNo = row["IdNo"].ToString();

        }
        if (MeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است";
            return;
        }
        else
        {
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            try
            {
                LogManager.FindByMeId(MeId);
                if (LogManager.Count > 0)
                {
                    LogManager[0].BeginEdit();
                    LogManager[0]["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(IdNo, "sha1");
                    LogManager[0].EndEdit();
                    if (LogManager.Save() > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "بازیابی رمز عبور با موفقیت انجام شد";
                        return;
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                        return;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی رخ داده است";
                    return;
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

    }
}
