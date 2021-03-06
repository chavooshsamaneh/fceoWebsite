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

public partial class Employee_Management_IntroductionInsert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.IntroductionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = Per.CanNew;
            btnSave2.Enabled = Per.CanNew;

            TSP.DataManager.IntroductionManager introductionManager =
                new TSP.DataManager.IntroductionManager();
            if (introductionManager.Fill() > 0)
            {
                txtIntText.Html = introductionManager[0]["IntText"].ToString().Replace("<br/>", "\n");
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            TSP.DataManager.IntroductionManager introductionManager =
                new TSP.DataManager.IntroductionManager();
            if (introductionManager.Fill() > 0)
            {
                introductionManager[0].BeginEdit();
                introductionManager[0]["IntText"] = txtIntText.Html;//.Replace("\n", "<br/>");
                introductionManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                introductionManager[0]["ModifiedDate"] = DateTime.Now;
                introductionManager[0].EndEdit();

                if (introductionManager.Save() == 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                DataRow dr = introductionManager.NewRow();
                dr["IntText"] = txtIntText.Html;
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                introductionManager.DataTable.Rows.Add(dr);
                if (introductionManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
}
