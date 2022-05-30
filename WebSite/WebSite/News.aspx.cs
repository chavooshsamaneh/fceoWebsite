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

public partial class News : System.Web.UI.Page
{
    public int AgentId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HdAgent["Id"].ToString()));
        }
        set
        {
            HdAgent["Id"] = Utility.EncryptQS(value.ToString());
        }
    }

    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HdAgent["PageMode"].ToString());
        }
        set
        {
            HdAgent["PageMode"] = Utility.EncryptQS(value.ToString());
        }
    }

    public int SubjectId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HdAgent["SubId"].ToString()));
        }
        set
        {
            HdAgent["SubId"] = Utility.EncryptQS(value.ToString());
        }
    }

    public int ExGroupCode
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HdAgent["ExGroupCode"].ToString()));
        }
        set
        {
            HdAgent["ExGroupCode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int IsNotification
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HdAgent["IsNotification"].ToString()));
        }
        set
        {
            HdAgent["IsNotification"] = Utility.EncryptQS(value.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            cmbSub.DataBind();
            cmbSub.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));
            cmbAgent.DataBind();
            cmbAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));

            try
            {
                if (Request.QueryString.Count != 0)
                {
                    #region

                    if (Request.QueryString["IsNotification"] != null)
                    {
                        CheckBoxIsNotification.Checked = Convert.ToBoolean(Convert.ToInt16( Utility.DecryptQS(Request.QueryString["IsNotification"].ToString())));
                      //  PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                        Search();
                    }
                    else if (Request.QueryString["Title"] != null)
                    {
                        PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                        txtSearchTitle.Text = Utility.DecryptQS(Request.QueryString["Title"].ToString());
                        Search();
                    }
                    else if (Request.QueryString["AgentId"] != null)  //--------agent view mode-----
                    {
                        AgentId = int.Parse(Utility.DecryptQS(Request.QueryString["AgentId"].ToString()));
                        PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                        if (AgentId != -1)
                        {
                            ObjdsNews.SelectParameters["AgentId"].DefaultValue = AgentId.ToString();

                            cmbAgent.DataBind();
                            cmbAgent.SelectedIndex = cmbAgent.Items.FindByValue(AgentId.ToString()).Index;
                            cmbAgent.Enabled = false;
                            MenuAgent.Visible = true;
                        }
                    }
                    else //------subject mode---------
                        if (Request.QueryString["SubjectId"] != null && !Utility.IsDBNullOrNullValue(Utility.DecryptQS(Request.QueryString["SubjectId"].ToString())))
                        {
                            SubjectId = int.Parse(Utility.DecryptQS(Request.QueryString["SubjectId"].ToString()));
                            PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                            ObjdsNews.SelectParameters["SubjectId"].DefaultValue = SubjectId.ToString();

                            cmbSub.DataBind();
                            cmbSub.SelectedIndex = cmbSub.Items.FindByValue(SubjectId.ToString()).Index;
                            cmbSub.Enabled = false;
                        }
                        else if (Request.QueryString["ImpId"] != null)
                        {
                            int ImpId = int.Parse(Utility.DecryptQS(Request.QueryString["ImpId"].ToString()));
                            PageMode = "View";

                            MenuAgent.Visible = false;
                            cmbImp.DataBind();
                            cmbImp.SelectedIndex = cmbImp.Items.FindByValue(ImpId.ToString()).Index;
                            if (cmbImp.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbImp.SelectedItem.Value))
                                ObjdsNews.SelectParameters["Importance"].DefaultValue = cmbImp.SelectedItem.Value.ToString();
                            else
                                ObjdsNews.SelectParameters["Importance"].DefaultValue = "-1";

                        }
                        else if (Request.QueryString["ExGroupCode"] != null)
                        {
                            MenuExpGroup.Visible = true;
                            ExGroupCode = int.Parse(Utility.DecryptQS(Request.QueryString["ExGroupCode"].ToString()));
                            TSP.DataManager.ExGroupManager ExGroupManager = new TSP.DataManager.ExGroupManager();
                            ExGroupManager.FindByExGroupCode(ExGroupCode);
                            if (ExGroupManager.Count==0)
                            {
                                return;
                            }
                            ObjdsNews.SelectParameters["ExGroupId"].DefaultValue = ExGroupManager[0]["ExGroupId"].ToString();
                            cmbExGroup.DataBind();
                            cmbExGroup.SelectedIndex = cmbExGroup.Items.FindByValue(ExGroupManager[0]["ExGroupId"].ToString()).Index;
                            switch (Convert.ToInt32(ExGroupCode))
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
                            DataViewNews.DataBind();
                        }
                    #endregion
                }
                else
                {
                    cmbAgent.Enabled = true;
                    cmbSub.Enabled = true;
                    AgentId = -1;
                    PageMode = "View";
                    MenuAgent.Visible = false;
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }

        //  Search();
        string script = "";
        script += @"<SCRIPT language='javascript'>
                function SetEmpty()
                {
                txtSearchBody.SetText('');
                txtSearchTitle.SetText('');               
                cmbSub.SetSelectedIndex(0);  
                cmbImp.SetSelectedIndex(0); 
                cmbAgent.SetSelectedIndex(0); 
                cmbExGroup.SetSelectedIndex(0); 
                document.getElementById('" + txtSearchFromDate.ClientID + @"').value = '';
                document.getElementById('" + txtSearchToDate.ClientID + @"').value = '';              
                } </SCRIPT>";
        script += @"<SCRIPT language='javascript'> function CheckSearch() { var FromDateSearch = document.getElementById('" + txtSearchFromDate.ClientID + "').value;";
        script += "var ToDateSearch = document.getElementById('" + txtSearchToDate.ClientID + "').value;";
        script += "if (FromDateSearch=='' && ToDateSearch=='' && txtSearchBody.GetText() == '' && txtSearchTitle.GetText() == '' && cmbSub.GetSelectedIndex() == 0 && cmbImp.GetSelectedIndex() == 0 && cmbAgent.GetSelectedIndex() == 0  && cmbExGroup.GetSelectedIndex() == 0 ) return 0; else return 1;  }</SCRIPT>";
       
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void MenuAgent_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(AgentId))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (e.Item.Name)
        {
            case "Agent":
                Response.Redirect("AgentView.aspx?AgentId=" + Utility.EncryptQS(AgentId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
                break;
            case "News":
                Response.Redirect("News.aspx?AgentId=" + Utility.EncryptQS(AgentId.ToString()) + "&PageMode=" + Utility.EncryptQS("AgentView"));
                break;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        Session["NewsId"] = lb.CommandArgument;
        if (Request.QueryString["PageMode"] != null)
        {
            if (PageMode == "AgentView")
                Response.Redirect("~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(lb.CommandArgument) + "&PageMode=" + Utility.EncryptQS(PageMode)
                    + "&AgentId=" + Utility.EncryptQS(AgentId.ToString()));
            else if (PageMode == "SubjectView")
                Response.Redirect("~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(lb.CommandArgument) + "&PageMode=" + Utility.EncryptQS(PageMode)
                    + "&SubjectId=" + Utility.EncryptQS(SubjectId.ToString()));
        }
        else
            Response.Redirect("~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(lb.CommandArgument) + "&PageMode=" + Utility.EncryptQS("Archive"));
    }

    protected void Rating1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxRatingControl Rating = (DevExpress.Web.ASPxRatingControl)sender;
        Rating.Value = int.Parse(Rating.ToolTip);
        Rating.ToolTip = "";
    }

    protected void Image2_DataBinding(object sender, EventArgs e)
    {
        HtmlImage img = (HtmlImage)sender;
        if (string.IsNullOrEmpty(img.Src))
            img.Src = "~/images/noimage.gif";
        else
            img.Src = img.Src;//"~/News/" + img.ImageUrl;
    }

    protected void lblBody_DataBinding(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl cont = new HtmlGenericControl("div");

        Label lbl = (Label)sender;
        string Stripped = System.Text.RegularExpressions.Regex.Replace(lbl.Text, @"<(.|\n)*?>", string.Empty);

        if (Stripped.Length >= 400)
            lbl.Text = Server.HtmlDecode(Stripped).Substring(1, 400);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtSearchTitle.Text))
            ObjdsNews.SelectParameters["Title"].DefaultValue = txtSearchTitle.Text;
        else
            ObjdsNews.SelectParameters["Title"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtSearchBody.Text))
            ObjdsNews.SelectParameters["Body"].DefaultValue = txtSearchBody.Text;
        else
            ObjdsNews.SelectParameters["Body"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtSearchFromDate.Text))
            ObjdsNews.SelectParameters["FromDate"].DefaultValue = txtSearchFromDate.Text;
        else
            ObjdsNews.SelectParameters["FromDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtSearchToDate.Text))
            ObjdsNews.SelectParameters["ToDate"].DefaultValue = txtSearchToDate.Text;
        else
            ObjdsNews.SelectParameters["ToDate"].DefaultValue = "2";

        if (cmbSub.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbSub.SelectedItem.Value))
            ObjdsNews.SelectParameters["SubjectId"].DefaultValue = cmbSub.SelectedItem.Value.ToString();
        else
            ObjdsNews.SelectParameters["SubjectId"].DefaultValue = "-1";

        if (cmbImp.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbImp.SelectedItem.Value))
            ObjdsNews.SelectParameters["Importance"].DefaultValue = cmbImp.SelectedItem.Value.ToString();
        else
            ObjdsNews.SelectParameters["Importance"].DefaultValue = "-1";

        if (cmbAgent.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbAgent.SelectedItem.Value))
            ObjdsNews.SelectParameters["AgentId"].DefaultValue = cmbAgent.SelectedItem.Value.ToString();
        else
            ObjdsNews.SelectParameters["AgentId"].DefaultValue = "-1";

        if (cmbExGroup.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbExGroup.SelectedItem.Value))
            ObjdsNews.SelectParameters["ExGroupId"].DefaultValue = cmbExGroup.SelectedItem.Value.ToString();
        else
            ObjdsNews.SelectParameters["ExGroupId"].DefaultValue = "-1";
        if (CheckBoxIsNotification.Checked)
            ObjdsNews.SelectParameters["IsNotification"].DefaultValue = (Convert.ToInt16(CheckBoxIsNotification.Checked)).ToString();
        else
            ObjdsNews.SelectParameters["IsNotification"].DefaultValue = "-1";


        DataViewNews.DataBind();
    }
    protected void MenuExpGroup_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "ExpInfo":
                Response.Redirect("~/Association/ExGroupPeriods.aspx?ExGrpCode=" + Request.QueryString["ExGroupCode"]);
                break;
        }
    }
}
