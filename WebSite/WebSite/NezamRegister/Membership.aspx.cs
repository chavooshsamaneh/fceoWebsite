using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web;

public partial class NezamRegister_Membership : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        ResetMemberSession();
        Response.Redirect("~/NezamRegister/WizardMember_Membership.aspx");
    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        ResetOfficeSession();
        Response.Redirect("~/NezamRegister/WizardOffice_Membership.aspx");        
    }

    private void ResetMemberSession()
    {
        Session["Member"] = null;
        Session["TblOfMadrak"] = null;
        Session["TblJob"] = null;
        Session["TblActivity"] = null;
        Session["TblLanguage"] = null;

        Session["FileOfMember"] = null;
        Session["FileMeSign"] = null;
        Session["FileOfIdNo"] = null;
        Session["FileOfSSN"] = null;
        Session["FileOfSol"] = null;

    }

    private void ResetOfficeSession()
    {
        Session["Office"] = null;
        Session["TblOfAgent"] = null;
        Session["TblOfLetter"] = null;
        Session["TblOfMember"] = null;
        Session["TblOfJob"] = null;
        Session["fileOfSign"] = null;
        Session["fileOfArm"] = null;
    }


    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}
