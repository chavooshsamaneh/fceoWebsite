using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Management_FAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.IntroductionManager.GetUserPermissionFAQ(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = Per.CanNew;
            btnSave2.Enabled = Per.CanNew;

            TSP.DataManager.IntroductionManager introductionManager = new TSP.DataManager.IntroductionManager();
            introductionManager.FindByType((int)TSP.DataManager.IntroductionManager.Type.FAQ);
            if (introductionManager.Count > 0)
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
            introductionManager.FindByType((int)TSP.DataManager.IntroductionManager.Type.FAQ);
            if (introductionManager.Count > 0)
            {
                introductionManager[0].BeginEdit();
                introductionManager[0]["IntText"] = txtIntText.Html;
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
                System.Data.DataRow dr = introductionManager.NewRow();
                dr["Type"] = (int)TSP.DataManager.IntroductionManager.Type.FAQ;
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
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
}