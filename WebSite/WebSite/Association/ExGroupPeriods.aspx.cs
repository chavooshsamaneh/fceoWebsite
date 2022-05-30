using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["ExGrpCode"] != null)
            {
                string ExGrpCode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ExGrpCode"]).ToString());
                MenuExpGroup.Visible = true;
                OdbExGroupPeriod.SelectParameters["ExGroupCode"].DefaultValue = ExGrpCode;
                switch (Convert.ToInt32(ExGrpCode))
                {
                    case (int)TSP.DataManager.ExGroupManager.Type.Bargh:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی برق";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی برق";                        
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Mechanic:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی مکانیک";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی مکانیک";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Memari:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی معماری";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی معماری";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Naghshebardaru:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی نقشه برداری";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی نقشه برداری";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Omran:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی عمران";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی عمران";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Shahrsazi:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی شهرسازی";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی شهرسازی";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Traffic:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "گروه تخصصی ترافیک";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار گروه تخصصی ترافیک";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Welfare:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "فرهنگی رفاهی";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار فرهنگی رفاهی";    
                        break;
                    case (int)TSP.DataManager.ExGroupManager.Type.Managementcommittee:
                        MenuExpGroup.Items.FindByName("ExpInfo").Text = "هیئت مدیره";
                        MenuExpGroup.Items.FindByName("News").Text = "اخبار هیئت مدیره";    
                        break;
                }
            }
            else
            {
                MenuExpGroup.Visible = false;
                OdbExGroupPeriod.SelectParameters["ExGroupCode"].DefaultValue = "-1";

            }
        }

    }
    protected void btnCandids_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        if (Request.QueryString["ExGrpCode"] != null)
            Response.Redirect("~/Association/CandidateList.aspx?ExGroupPeriodId=" + Utility.EncryptQS(lb.CommandArgument) + "&ExGrpCode=" + Server.HtmlDecode(Request.QueryString["ExGrpCode"]).ToString());
        else
            Response.Redirect("~/Association/CandidateList.aspx?ExGroupPeriodId=" + Utility.EncryptQS(lb.CommandArgument));

    }
    protected void MenuExpGroup_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "News":
                Response.Redirect("~/News.aspx?ExGroupCode=" + Server.HtmlDecode(Request.QueryString["ExGrpCode"]).ToString());
                break;

        }
    }
}