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

public partial class NewsShow : System.Web.UI.Page
{
    #region Property

    string _NewsId;
    string NewsId
    {
        get
        {
            return _NewsId;
        }
        set
        {
            _NewsId = value;
        }
    }
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

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request.QueryString["FromHomePage"] != null)
        {
            if (Utility.DecryptQS(Request.QueryString["FromHomePage"]) == "1")
                btnBack.Visible = btnBack2.Visible = false;
        }
        if (Request.QueryString["NewsId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        NewsId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["NewsId"]).ToString());

        if (string.IsNullOrEmpty(NewsId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            if (Request.QueryString["AgentId"] != null)
                AgentId = int.Parse(Utility.DecryptQS(Request.QueryString["AgentId"].ToString()));

            if (Request.QueryString["PageMode"] != null)
                PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());

            if (Request.QueryString["SubjectId"] != null)
                SubjectId = int.Parse(Utility.DecryptQS(Request.QueryString["SubjectId"].ToString()));



            if (string.IsNullOrEmpty(NewsId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            try
            {
                if (!(string.IsNullOrEmpty(Request.QueryString["NewsId"])))
                {
                    TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();

                    NsManager.FindByCode(int.Parse(NewsId));

                    try
                    {
                        NsManager[0].BeginEdit();
                        NsManager[0]["CountOfRead"] = int.Parse(NsManager[0]["CountOfRead"].ToString()) + 1;
                        NsManager[0].EndEdit();
                        NsManager.Save();
                        NsManager.DataTable.AcceptChanges();
                    }
                    catch (Exception) { }


                    lblAgent.InnerText = "نمایندگی:" + NsManager[0]["AgentName"].ToString();
                    lblBody.InnerHtml = NsManager[0]["Body"].ToString();
                    lblDate.InnerText = NsManager[0]["Date"].ToString();
                    lblSub.InnerText = NsManager[0]["Name"].ToString().Trim() + " " + NsManager[0]["ImpName"].ToString().Trim();
                    lblTime.InnerText = NsManager[0]["StrTime"].ToString();
                    lblTitle.InnerText = NsManager[0]["Title"].ToString();
                    lblCountOfRead.InnerText = "تعداد بازدید کنندگان :" + NsManager[0]["CountOfRead"].ToString();
                    Rating1.Value = int.Parse(NsManager[0]["SumRate"].ToString());
                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
                    {
                        HyperLinkAttachment.Visible = true;
                        HyperLinkAttachment.NavigateUrl = NsManager[0]["AttachmentUrl"].ToString(); ;
                    }

                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["url"]))
                    {
                        Image1.ImageUrl = NsManager[0]["url"].ToString();
                    }

                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["Summary"]))
                    {
                        HtmlMeta metadescription = new HtmlMeta();
                        metadescription.Attributes["property"] = "og:description";
                        metadescription.Content = NsManager[0]["Summary"].ToString();
                        Page.Header.Controls.Add(metadescription);
                    }
                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["Logourl"]))
                    {
                        HtmlMeta metaimage = new HtmlMeta();
                        metaimage.Attributes["property"] = "og:image";
                        metaimage.Content = NsManager[0]["Logourl"].ToString().Replace("~", "");
                        Page.Header.Controls.Add(metaimage);
                    }
                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["Title"]))
                    {
                        this.Title = NsManager[0]["Title"].ToString();
                    }
                }
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نمایش اطلاعات امکان پذیر نمی باشد";
            }

            HiddenNewsData["PrintAddress"] = "PrintNews.aspx?News=" + Request.QueryString["NewsId"];
        }

        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        ImgManager.FindByNewsCodeAndType(int.Parse(NewsId), 1);

        if (ImgManager.Count > 0)
        {
            DataViewOtherAttachments.DataSource = ImgManager.DataTable;
            DataViewOtherAttachments.DataBind();
            tblAtt.Visible = true;
        }
        else
            tblAtt.Visible = false;
    }


    protected void btnInsert_Click(object sender, EventArgs e)
    {


        if (String.IsNullOrEmpty(txtEmail.Text))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ایمیل وارد نشده است";
            return;
        }
        if (Utility.CheckEmailFormat(txtEmail.Text) == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ایمیل صحیح وارد نشده است";
            return;
        }

        if (String.IsNullOrEmpty(txtBody.Text))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "متن وارد نشده است";
            return;
        }

        TSP.DataManager.NewsIdeaManager IdeaManager = new TSP.DataManager.NewsIdeaManager();
        TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();

        DataRow dr = IdeaManager.NewRow();

        try
        {
            dr["NewsId"] = NewsId;
            dr["Name"] = txtName.Text;
            dr["LastName"] = txtLastName.Text;
            dr["Email"] = txtEmail.Text;
            dr["Body"] = txtBody.Text;
            dr["Date"] = Utility.GetDateOfToday();
            dr["Rate"] = Rating3.Value;
            dr["Time"] = DateTime.Now;
            dr["UserId"] = int.Parse(Session["Login"].ToString());
            dr["ModifiedDate"] = DateTime.Now;

            IdeaManager.AddRow(dr);
            int cnt = IdeaManager.Save();

            if (cnt > 0)
            {
                this.txtName.Text = "";
                this.txtLastName.Text = "";
                this.txtBody.Text = "";
                this.txtEmail.Text = "";
                FillNews(int.Parse(NewsId), NsManager);
                Rating3.Value = 0;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtBody.Text = "";
        txtEmail.Text = "";
        txtLastName.Text = "";
        txtName.Text = "";
        //for (int i = 0; i < ASPxPopupControl1.Controls.Count; i++)
        //{
        //    try
        //    {
        //        ((TextBox)ASPxPopupControl1.Controls[i]).Text = "";

        //    }
        //    catch
        //    {

        //    }

        //}
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(Request.QueryString["PageMode"]))
            Response.Redirect("Home.aspx");
        switch (PageMode)
        {
            case "AgentView":
                Response.Redirect("News.aspx?AgentId=" + Utility.EncryptQS(AgentId.ToString()) + "&PageMode=" + Utility.EncryptQS(PageMode));
                break;
            case "SubjectView":
                Response.Redirect("News.aspx?SubjectId=" + Utility.EncryptQS(SubjectId.ToString()) + "&PageMode=" + Utility.EncryptQS(PageMode));
                break;
            //case "HomePage":
            //    Response.Redirect("Home.aspx");
            //    break;
            case "Archive":
                Response.Redirect("News.aspx");
                break;
        }
    }

    //protected void playButton_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (playButton.ImageUrl.Contains("new"))
    //        playButton.ImageUrl = "Image/puss.gif";
    //    else
    //        playButton.ImageUrl = "Image/newplay.gif";
    //}

    protected void Rating2_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxRatingControl Rating = (DevExpress.Web.ASPxRatingControl)sender;
        Rating.Value = int.Parse(Rating.ToolTip);
        Rating.ToolTip = "";
    }

    private void FillNews(int NsId, TSP.DataManager.NewsManager NsManager)
    {
        NsManager.FindByCode(NsId);
        lblBody.InnerHtml = NsManager[0]["Body"].ToString();
        lblDate.InnerText = NsManager[0]["Date"].ToString();
        lblSub.InnerText = NsManager[0]["Name"].ToString().Trim() + " " + NsManager[0]["ImpName"].ToString().Trim();
        lblTime.InnerText = NsManager[0]["StrTime"].ToString();
        lblTitle.InnerText = NsManager[0]["Title"].ToString();
        lblCountOfRead.InnerText = "تعداد بازدید کنندگان :" + NsManager[0]["CountOfRead"].ToString();
        Rating1.Value = int.Parse(NsManager[0]["SumRate"].ToString());
    }
}
