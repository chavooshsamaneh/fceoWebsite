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

public partial class Employee_News_ShowNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["NewsId"] = Server.HtmlDecode(Request.QueryString["NewsId"]).ToString();
        string s = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["NewsId"]).ToString());

        if (string.IsNullOrEmpty(s))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Session["NewsId"] = s;

        if (DataList1.Items.Count <= 0)
            DataList1.Visible = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string ss = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["NewsId"]).ToString());

            if (string.IsNullOrEmpty(ss))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            ObjectDataSource1.FilterParameters[0].DefaultValue = ss;

            try
            {
                if (!(string.IsNullOrEmpty(Request.QueryString["NewsId"])))
                {
                    if (!(string.IsNullOrEmpty(Request.QueryString["show"])))
                    {
                        string show = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["show"]).ToString());

                        if (string.IsNullOrEmpty(show))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        if (show == "1")
                        {
                            //  Panel1.Visible = false;
                        }
                    }
                    //if (Request.QueryString["show"] == "1")
                    //{
                    //    Panel1.Visible = false;

                    //}
                    TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
                    TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
                    // int NewsId = int.Parse(Server.HtmlEncode(Request.QueryString["NewsId"]).ToString());
                    string sId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["NewsId"]).ToString());

                    if (string.IsNullOrEmpty(sId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    int NewsId = int.Parse(sId);
                    NsManager.FindByCode(NewsId);
                    ImgManager.FindByNewsCode(NewsId);
                    lblAgent.Text = NsManager[0]["AgentName"].ToString();
                    lblBody.Text = NsManager[0]["Body"].ToString();
                    lblDate.Text = NsManager[0]["Date"].ToString();
                    //lblSub.Text = NsManager[0]["Name"].ToString();
                    lblTime.Text = NsManager[0]["StrTime"].ToString();
                    lblTitle.Text = NsManager[0]["Title"].ToString();
                    //lblImp.Text = NsManager[0]["ImpName"].ToString();
                    lblSub_Imp.Text = NsManager[0]["Name"] + " " + NsManager[0]["ImpName"];
                    lblCountOfRead.Text = NsManager[0]["CountOfRead"].ToString();
                    Rating1.CurrentRating = int.Parse(NsManager[0]["SumRate"].ToString());
                    if(!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
                    {
                        HyperLinkAttachment.Visible = true;
                        HyperLinkAttachment.NavigateUrl = NsManager[0]["AttachmentUrl"].ToString();
                    }
                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["url"]))
                    {
                        Image1.ImageUrl = NsManager[0]["url"].ToString();
                    }
                    //if (ImgManager.Count > 0)

                    //    img.ImageUrl = ImgManager[0]["ImgUrl"].ToString();
                    //else
                    //    img.ImageUrl = "~/images/noimage.gif/";

                }
               
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نمایش اطلاعات امکان پذیر نمی باشد";
            }

        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    AjaxControlToolkit.Rating rate = (AjaxControlToolkit.Rating)e.Row.Cells[7].Controls[1];
        //    rate.CurrentRating = int.Parse(rate.Tag);
        //    rate.BehaviorID = rate.ID + "_RatingExtender";
        //}
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < Panel1.Controls.Count; i++)
        //{
        //    try
        //    {
        //        ((TextBox)Panel1.Controls[i]).Text = "";

        //    }
        //    catch
        //    {

        //    }

        //}
    }

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        string show = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["show"]).ToString());
        if (string.IsNullOrEmpty(show))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        // if (Request.QueryString["show"] == "1")
        if (show == "1")
            Response.Redirect("News.aspx");

        else
            Response.Redirect("../akhbar.aspx");

    }
    protected void playButton_Click(object sender, ImageClickEventArgs e)
    {
        if (playButton.ImageUrl.Contains("new"))
            playButton.ImageUrl = "Image/puss.gif";
        else
            playButton.ImageUrl = "Image/newplay.gif";
    }

    protected void Rating3_DataBinding(object sender, EventArgs e)
    {
        AjaxControlToolkit.Rating rat = (AjaxControlToolkit.Rating)sender;
        rat.CurrentRating = int.Parse(rat.Tag);
    }
    protected void Rating3_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        //  Rating3.Focus();
    }
    protected void lbntDelete_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int IdeaId = int.Parse(lb.CommandArgument);

        TSP.DataManager.NewsIdeaManager IdeaManager = new TSP.DataManager.NewsIdeaManager();
        IdeaManager.FindByCode(IdeaId);
        if (IdeaManager.Count == 1)
            try
            {
                IdeaManager[0].Delete();
                int cnt = IdeaManager.Save();
                if (cnt == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    DataList1.DataBind();
                    if (DataList1.Items.Count < 1)
                        DataList1.Visible = false;


                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                }
            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در  حذف انجام گرفته است";
            }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین ردیفی وجود ندارد.مجددا بازخوانی نمایید";
        }
    }


    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //if (!(string.IsNullOrEmpty(Request.QueryString["NewsId"])))

        //    if (Request.QueryString["show"] == "1")
        //    {
        //        if (e.Item.Controls.Count > 12)

        //            e.Item.Controls[13].Visible = true;

        //    }
    }

}
