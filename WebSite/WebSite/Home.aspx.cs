using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using DevExpress.Web;

public partial class Home : System.Web.UI.Page
{
    public string SwfFile1 = "";


    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
            Configuration configurationAd = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Admin");
            AppSettingsSection appSettingsSectionAd = (AppSettingsSection)configurationAd.GetSection("appSettings");
            if (appSettingsSectionAd != null)
            {
                RoundPanelMessage.Visible = Convert.ToBoolean(Convert.ToInt32(appSettingsSectionAd.Settings["Show"].Value));
                string Msg = "";
                if (string.IsNullOrWhiteSpace(appSettingsSectionAd.Settings["txtShow"].Value))
                    Msg = "پشتیبانی نرم افزار به پایان رسیده است.جهت جلوگیری از کاهش سرعت لطفا اقدامات لازم را انجام دهید";
                else
                    Msg = appSettingsSectionAd.Settings["txtShow"].Value;
                lblMessage.Text = Msg;
            }
            LoadImportantNews();
            LoadNotificationNews();
            LoadBoardDirectorsNews();
            LoadLatestNews();
            LoadVideos();
            LoadPodcasts();
            //ObjectDataSourceVideos.SelectParameters["ImageType"].DefaultValue = ((int)TSP.DataManager.SiteImageManager.SiteImageType.Video).ToString();
            //ObjectDataSourcePodcast.SelectParameters["ImageType"].DefaultValue = ((int)TSP.DataManager.SiteImageManager.SiteImageType.Voice).ToString();
            //if (((DataTable)ObjectDataSourceVideos.Select()).Rows.Count == 0)
            //ShowMessage(((System.Data.DataView)ObjectDataSourceVideos.Select()).Count.ToString());
         

            //CheckLinks();
            //LoadNewsSubject();
            PeriodsSection.Visible = false;
            LoadPeriods();
            LoadSeminar();
            LoadExpertGroup();
            LoadAssociationInfo();
            LoadImageGallary();
            Poll_ValidPollListUserControl.NextPageURLPrefix = "../";
            LoadCondolence();
            LoadMagazin();
            LoadPollInfo();
            SetClassDynamicCol();
            LoadAmoozeshAttach();
            LoadWelfareScheduleList();
        }

    }
    protected void btnPeriodView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        String[] Param = lb.CommandArgument.Split(';');
        //*****0:PPId ; 1:InsId ; 2: PType
        //****PType=0==>Periods ;PType=1==>Seminar
        if (Convert.ToInt32(Param[2]) == 0)
        {
            Response.Redirect("~/PeriodsView.Aspx?PPId=" + Utility.EncryptQS(Param[0]) + "&InsId=" + Utility.EncryptQS(Param[1]) + "&PrePg=" + Utility.EncryptQS("Home"));
        }
        else
        {
            Response.Redirect("~/SeminarView.aspx?SeId=" + Utility.EncryptQS(Param[0]));

        }

    }
    protected void btnTraceDocument_Click(object sender, EventArgs e)
    {
        //Response.Redirect("TraceDocuments/TraceDocuments.aspx?No=" + Utility.EncryptQS(txtDocumentNo.Text));
    }
    ////*****مربوط به قسمت انتخابات تشکل ها
    //protected void btnAssociation_Click(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    Response.Redirect("~/Association/ManagersCandidate.aspx?ExGgPrId=" + Utility.EncryptQS(lb.CommandArgument));
    //    lb.ToolTip = "";
    //}
    //protected void grdNews_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "Date")
    //    {
    //        e.Cell.Style["direction"] = "ltr";
    //    }
    //}


    protected void btnSavePollAnswer_OnLoad(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxButton btnSavePollAnswer = sender as DevExpress.Web.ASPxButton;
        DataViewItemTemplateContainer container = btnSavePollAnswer.NamingContainer as DataViewItemTemplateContainer;
        //btnSavePollAnswer.ClientSideEvents.Click = string.Format("function(s, e){{if (ASPxClientEdit.ValidateGroup('PollChoise')) DataViewPoll.PerformCallback('Save;{0}')}}", container.ItemIndex);
        btnSavePollAnswer.ClientSideEvents.Click = string.Format("function(s, e){{ DataViewPoll.PerformCallback('Save;{0}')}}", container.ItemIndex);
    }

    protected void btnResultView_OnLoad(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxButton btnResultView = sender as DevExpress.Web.ASPxButton;
        DataViewItemTemplateContainer container = btnResultView.NamingContainer as DataViewItemTemplateContainer;
        // btnResultView.ClientSideEvents.Click = string.Format(function(s, e){{"Response.Redirect("Poll/PollResultMain.aspx?PId=" + Utility.EncryptQS(PollId.ToString()))}};", container.ItemIndex);
        btnResultView.ClientSideEvents.Click = string.Format("function(s, e){{ DataViewPoll.PerformCallback('ViewReport;{0}')}}", container.ItemIndex);
        DevExpress.Web.ASPxLabel lblIsResualtPublic = (DataViewPoll.FindItemControl("lblIsResualtPublic", DataViewPoll.Items[container.ItemIndex]) as DevExpress.Web.ASPxLabel);
        if (lblIsResualtPublic.Text == "2")
            btnResultView.ClientVisible = true;
        else
            btnResultView.ClientVisible = false;
    }

    protected void OnCustomCallback_DataViewPoll(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] Parameters = e.Parameter.Split(';');
        int SelectedIndex = Convert.ToInt32(Parameters[1].ToString());
        DataViewItemTemplateContainer container = DataViewPoll.NamingContainer as DataViewItemTemplateContainer;
        switch (Parameters[0])
        {
            case "Save":
                #region Save
                TSP.DataManager.PollChoiseManager PollChoiseManager = new TSP.DataManager.PollChoiseManager();
                TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
                DevExpress.Web.ASPxRadioButtonList rdbChoise = (DataViewPoll.FindItemControl("rdbChoise", DataViewPoll.Items[SelectedIndex]) as DevExpress.Web.ASPxRadioButtonList);

                if (rdbChoise != null)
                {
                    try
                    {
                        DevExpress.Web.ASPxLabel lblPollReport = (DataViewPoll.FindItemControl("lblPollReport", DataViewPoll.Items[SelectedIndex]) as DevExpress.Web.ASPxLabel);
                        if (rdbChoise.SelectedItem == null)
                        {
                            lblPollReport.Text = "گزینه را انتخاب نمایید.";
                            return;
                        }
                        DataRow dr = PollAnswerManager.NewRow();
                        dr["ChoiseId"] = rdbChoise.SelectedItem.Value;
                        dr["AnswerDate"] = Utility.GetDateOfToday();
                        dr["AnswerTime"] = Utility.GetCurrentTime();
                        dr["UserId"] = Utility.GetCurrentUser_UserId();
                        dr["ModifiedDate"] = DateTime.Now;
                        dr["Description"] = "ثبت نظر از صفحه اصلی سایت";
                        PollAnswerManager.AddRow(dr);
                        PollAnswerManager.Save();
                        PollAnswerManager.DataTable.AcceptChanges();
                        rdbChoise.SelectedItem = null;
                        lblPollReport.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
                    }
                    catch (Exception err)
                    {
                        Utility.SaveWebsiteError(err);
                        // SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    }

                }
                #endregion
                break;
            case "ViewReport":
                DevExpress.Web.ASPxLabel lblPollId = (DataViewPoll.FindItemControl("lblPollId", DataViewPoll.Items[SelectedIndex]) as DevExpress.Web.ASPxLabel);
                ASPxWebControl.RedirectOnCallback("Poll/PollResultMain.aspx?PId=" + Utility.EncryptQS(lblPollId.Text));
                break;
        }
    }
    protected void btnNewDoc_ServerClick(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            Response.Redirect("/login.aspx");
            return;
        }
        if (CheckConditions())
        {
            ResetDocSessions();
            Response.Redirect("/Members/Documents/WizardDocOath.aspx");
        }
    }
    protected void btnQualification_ServerClick(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            Response.Redirect("/login.aspx");
            return;
        }
        if (CheckDocQualificationConditions())
        {
            ResetQualification();

            Response.Redirect("Documents/WizardQualificationOath.aspx");
        }
    }

    protected void btnRevival_ServerClick(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            Response.Redirect("/login.aspx");
            return;
        }
        if (CheckDocRevivalConditions())
        {
            ResetRevival();

            Response.Redirect("Documents/WizardRevivalOath.aspx");
        }
    }

    protected void btnSearchNews_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("/Search.aspx?Title=" + Utility.EncryptQS(txtSearchNews.Value) + "#SearchSection");
    }

    protected void btnDocumentTrace_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("/TraceDocuments/TraceDocuments.aspx?No=" + Utility.EncryptQS(txtDocumentTrace.Value));
    }
    #endregion

    #region Methods
    private void LoadImportantNews()
    {
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        DataTable dt = NewsManager.SelectImportantTopNews(0);

        DataTable dtNews = new DataTable();
        dtNews.Columns.Add("NewsId");
        dtNews.Columns.Add("Date");
        dtNews.Columns.Add("NewsHeader");
        dtNews.Columns.Add("Summary");
        dtNews.Columns.Add("Imageurl");
        dtNews.Columns.Add("NewsContentURL");
        dtNews.Columns.Add("Id");
        dtNews.Columns["Id"].AutoIncrement = true;
        dtNews.Columns["Id"].AutoIncrementSeed = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtNews.NewRow();
            dr["NewsId"] = dt.Rows[i]["NewsId"];
            dr["Date"] = dt.Rows[i]["Date"];
            dr["NewsHeader"] = dt.Rows[i]["NewsHeader"];
            dr["Summary"] = dt.Rows[i]["Summary"];
            if (!Utility.IsDBNullOrNullValue(dt.Rows[i]["Imageurl"]))
            {
                string imgUrl = dt.Rows[i]["Imageurl"].ToString();
                if (imgUrl.Length > 2)
                {
                    int len = imgUrl.Length - 1;
                    dr["Imageurl"] = imgUrl.Substring(1, len);
                }
            }
            dr["NewsContentURL"] = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(dt.Rows[i]["NewsId"].ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView")
                  + "&SubjectId=" + Utility.EncryptQS(dt.Rows[i]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");

            dtNews.Rows.Add(dr);
        }
        if (dtNews.Rows.Count != 0)
        {
            RepeaterImportantNews.DataSource = dtNews;
            RepeaterImportantNews.DataBind();

        }
    }

    private void LoadNotificationNews()
    {
        linkMoreNotificationNews.HRef = "~/News.aspx?IsNotification=" + Utility.EncryptQS("1");
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        DataTable dt = NewsManager.SelectNotificationNews();
        DataTable dtNews = new DataTable();
        dtNews.Columns.Add("NewsId");
        dtNews.Columns.Add("Date");
        dtNews.Columns.Add("NewsHeader");
        dtNews.Columns.Add("Summary");
        dtNews.Columns.Add("Imageurl");
        dtNews.Columns.Add("NewsContentURL");
        dtNews.Columns.Add("Id");
        dtNews.Columns["Id"].AutoIncrement = true;
        dtNews.Columns["Id"].AutoIncrementSeed = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtNews.NewRow();
            dr["NewsId"] = dt.Rows[i]["NewsId"];
            dr["Date"] = dt.Rows[i]["Date"];
            dr["NewsHeader"] = dt.Rows[i]["NewsHeader"];
            dr["Summary"] = dt.Rows[i]["Summary"];
            if (!Utility.IsDBNullOrNullValue(dt.Rows[i]["Imageurl"]))
            {
                string imgUrl = dt.Rows[i]["Imageurl"].ToString();
                if (imgUrl.Length > 2)
                {
                    int len = imgUrl.Length - 1;
                    dr["Imageurl"] = imgUrl.Substring(1, len);
                }
            }
            dr["NewsContentURL"] = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(dt.Rows[i]["NewsId"].ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView")
                  + "&SubjectId=" + Utility.EncryptQS(dt.Rows[i]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");

            dtNews.Rows.Add(dr);
        }
        if (dtNews.Rows.Count != 0)
        {
            RepeaterNewsNotification.DataSource = dtNews;
            RepeaterNewsNotification.DataBind();
        }
        else
        {
            tablist1Tab2.Visible = tablist1Panel2.Visible = false;
        }
    }
    private void LoadBoardDirectorsNews()
    {   
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        DataTable dt = NewsManager.SelectBoardDirectorsNews();
        DataTable dtNews = new DataTable();
        dtNews.Columns.Add("NewsId");
        dtNews.Columns.Add("Date");
        dtNews.Columns.Add("NewsHeader");
        dtNews.Columns.Add("Summary");
        dtNews.Columns.Add("Imageurl");
        dtNews.Columns.Add("NewsContentURL");
        dtNews.Columns.Add("Id");
        dtNews.Columns["Id"].AutoIncrement = true;
        dtNews.Columns["Id"].AutoIncrementSeed = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtNews.NewRow();
            dr["NewsId"] = dt.Rows[i]["NewsId"];
            dr["Date"] = dt.Rows[i]["Date"];
            dr["NewsHeader"] = dt.Rows[i]["NewsHeader"];
            dr["Summary"] = dt.Rows[i]["Summary"];
            if (!Utility.IsDBNullOrNullValue(dt.Rows[i]["Imageurl"]))
            {
                string imgUrl = dt.Rows[i]["Imageurl"].ToString();
                if (imgUrl.Length > 2)
                {
                    int len = imgUrl.Length - 1;
                    dr["Imageurl"] = imgUrl.Substring(1, len);
                }
            }
            dr["NewsContentURL"] = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(dt.Rows[i]["NewsId"].ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView")
                  + "&SubjectId=" + Utility.EncryptQS(dt.Rows[i]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");

            dtNews.Rows.Add(dr);
        }
        if (dtNews.Rows.Count != 0)
        {
            RepeaterNewsBoardOfDirectors.DataSource = dtNews;
            RepeaterNewsBoardOfDirectors.DataBind();
        }
        else
        {
            tablist1Tab6.Visible = tablist1Panel6.Visible = false;
        }
    }


    
    private void LoadLatestNews()
    {
        linkMoreNews.HRef = "~/News.aspx";//?ImpId=" + Utility.EncryptQS("0");
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        DataTable dt = NewsManager.selectLatestNews(-1,0); //(0);

        DataTable dtNews = new DataTable();
        dtNews.Columns.Add("NewsId");
        dtNews.Columns.Add("Date");
        dtNews.Columns.Add("NewsHeader");
        dtNews.Columns.Add("Summary");
        dtNews.Columns.Add("Imageurl");
        dtNews.Columns.Add("NewsContentURL");
        dtNews.Columns.Add("Id");
        dtNews.Columns["Id"].AutoIncrement = true;
        dtNews.Columns["Id"].AutoIncrementSeed = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtNews.NewRow();
            dr["NewsId"] = dt.Rows[i]["NewsId"];
            dr["Date"] = dt.Rows[i]["Date"];
            dr["NewsHeader"] = dt.Rows[i]["NewsHeader"];
            dr["Summary"] = dt.Rows[i]["Summary"];
            if (!Utility.IsDBNullOrNullValue(dt.Rows[i]["Imageurl"]))
            {
                string imgUrl = dt.Rows[i]["Imageurl"].ToString();
                if (imgUrl.Length > 2)
                {
                    int len = imgUrl.Length - 1;
                    dr["Imageurl"] = imgUrl.Substring(1, len);
                }
            }
            dr["NewsContentURL"] = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(dt.Rows[i]["NewsId"].ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView")
                  + "&SubjectId=" + Utility.EncryptQS(dt.Rows[i]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");

            dtNews.Rows.Add(dr);
        }
        if (dtNews.Rows.Count != 0)
        {
            RepeaterLatestNews.DataSource = dtNews;
            RepeaterLatestNews.DataBind();

        }
        else
        {
            tablist1Tab1.Visible = tablist1Panel1.Visible = false;
        }

    }
    private void LoadImageGallary()
    {
        TSP.DataManager.GalleryAlbumsManager GalleryAlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
        DataTable dtImageGallery = GalleryAlbumsManager.SelectViewGalleryAlbums(Utility.GetDateOfToday());
        if (dtImageGallery.Rows.Count != 0)
        {
            RepeaterImageGallary.DataSource = dtImageGallery;
            RepeaterImageGallary.DataBind();
        }
        else
        {
            tablist1Tab3.Visible = tablist1Panel3.Visible = false;
        }
        //"ImageGalleryFrame.aspx?album=" + Utility.EncryptQS(btnViewGallery.ToolTip);
    }

    //private void CheckLinks()
    //{
    //    TSP.DataManager.LinksManager LinksManager = new TSP.DataManager.LinksManager();
    //    DataTable dt = LinksManager.SelectForHomePage();
    //    if (dt.Rows.Count != 0)
    //    {
    //        RepeaterLinks.DataSource = dt;
    //        RepeaterLinks.DataBind();
    //    }
    //}

    private void LoadMagazin()
    {
        linkMagazinArchive.HRef = "~/News.aspx?SubjectId=" + Utility.EncryptQS(((int)TSP.DataManager.NewsSubjectId.Magazin).ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView");

        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        DataTable dtMagazin = NewsManager.SelectNewsForMagazin();
        if (dtMagazin.Rows.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(dtMagazin.Rows[0]["Imageurl"]))
                ImageMagazin.Src = dtMagazin.Rows[0]["Imageurl"].ToString();
            LinkMagazin.HRef = "~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(dtMagazin.Rows[0]["NewsId"].ToString()) + "&SubjectId=" + Utility.EncryptQS(dtMagazin.Rows[0]["SubjectId"].ToString());
            //lblMagazinTitle.InnerText = "فصل نامه گزارش" + " - " + dtMagazin.Rows[0]["Title"].ToString();
            lblMagazinTitle2.InnerText = dtMagazin.Rows[0]["Title"].ToString();




            if (!Utility.IsDBNullOrNullValue(dtMagazin.Rows[1]["Imageurl"]))
                ImageMagazinPre.Src = dtMagazin.Rows[1]["Imageurl"].ToString();
            LinkMagazinPre.HRef = "~/NewsShow.aspx?NewsId=" + Utility.EncryptQS(dtMagazin.Rows[1]["NewsId"].ToString()) + "&SubjectId=" + Utility.EncryptQS(dtMagazin.Rows[1]["SubjectId"].ToString());
            lblMagazinTitlePre.InnerText = dtMagazin.Rows[1]["Title"].ToString();
        }
    }
    //private void LoadNewsSubject()
    //{
    //    TSP.DataManager.NewsSubjectManager NewsSubjectManager = new TSP.DataManager.NewsSubjectManager();
    //    NewsSubjectManager.FindByCode(-1);
    //    RepeaterNewsSubject.DataSource = NewsSubjectManager.DataTable;
    //    RepeaterNewsSubject.DataBind();

    //    //if (NewsSubjectManager.Count <= 0)
    //    //{
    //    //    divNewsSubject.Visible = false;
    //    //    RoundPanelNewsSubject.Visible = false;
    //    //    imgNewsSubject.Visible = false;
    //    //    return;
    //    //}

    //    //divNewsSubject.Visible = true;
    //    //RoundPanelNewsSubject.Visible = true;
    //    //imgNewsSubject.Visible = true;
    //    //StringBuilder sb = new StringBuilder();
    //    //sb.Append("<table width='100%' class='NewsSubject'>");
    //    //foreach (DataRow row in NewsSubjectManager.DataTable.Rows)
    //    //{
    //    //    string ImageUrl = "Images/DefaultNewsSubject.png";
    //    //    if (!Utility.IsDBNullOrNullValue(row["ImageUrl"]))
    //    //        ImageUrl = row["ImageUrl"].ToString().Replace("~/", "");

    //    //    sb.Append("<tr><td valign='top' class='MenuItem'><img alt='' width='20px' height='20px' src='" +
    //    //                        ImageUrl + "' /><a href='News.aspx?SubjectId=" + Utility.EncryptQS(row["SubId"].ToString())
    //    //                        + "&PageMode=" + Utility.EncryptQS("SubjectView") + "'>" + row["Name"].ToString() + "</a></td></tr>");

    //    //    //<div class='<%# Container.ItemIndex == 0 ? "item active" : "item" %>'>
    //    //    //                <dxe:ASPxImage CssClass="img-responsive" ID="ImageNews" ToolTip='' runat="server" Width="100%" ImageUrl='<%# Bind("Imageurl") %>'
    //    //    //                    Height="100%">
    //    //    //                    <EmptyImage Height="100px" Width="300px" Url="~/Images/NoImage3DPeople.png">
    //    //    //                    </EmptyImage>
    //    //    //                </dxe:ASPxImage>
    //    //    //            </div>
    //    //}

    //    //sb.Append("</table>");
    //    //divNewsSubject.InnerHtml = sb.ToString();
    //}
    private void LoadVideos()
    {
        TSP.DataManager.SiteImageManager SiteImageManager = new TSP.DataManager.SiteImageManager();        
        DataTable dt = SiteImageManager.SelectSiteVideos(0,-1);
        if (dt.Rows.Count != 0)
        {
            RepeaterVideos.DataSource = dt;
            RepeaterVideos.DataBind();
           
        }
        else
        {
            tablist1Tab4.Visible =
            tablist1Panel4.Visible = false;
        }
    }
    private void LoadPodcasts()
    {
        TSP.DataManager.SiteImageManager SiteImageManager = new TSP.DataManager.SiteImageManager();
        int PType = 3;
        DataTable dt = SiteImageManager.SelectSitePodcasts(0,-1);
        if (dt.Rows.Count != 0)
        {
            RepeaterPodcast.DataSource = dt;
            RepeaterPodcast.DataBind();

        }
        else
        {
            tablist1Tab6.Visible =
            tablist1Panel6.Visible = false;
        }
    }
    private void LoadPeriods()
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        int PType = 0;
        DataTable dt = PeriodPresentManager.FindMembersByDate(PType);
        if (dt.Rows.Count != 0)
        {
            DataViewPeriods.DataSource = dt;
            DataViewPeriods.DataBind();
            PeriodsSection.Visible = true;
        }
        else
        {
            tablist2Tab1.Visible =
            tablist2Panel1.Visible = false;
        }
    }

    private void LoadSeminar()
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        int PType = 1;
        DataTable dt = PeriodPresentManager.FindMembersByDate(PType);
        if (dt.Rows.Count != 0)
        {
            DataViewSeminar.DataSource = dt;
            DataViewSeminar.DataBind();
            PeriodsSection.Visible = true;
        }
        else
        {
            tablist2Tab2.Visible =
            tablist2Panel2.Visible = false;
        }
    }
    private void LoadExpertGroup()
    {
        linkExpMapping.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Naghshebardaru).ToString()).ToString();
        linkExpArchitecture.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Memari).ToString()).ToString();
        linkExpConstruction.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Omran).ToString()).ToString();
        linkExpElectronic.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Bargh).ToString()).ToString();
        linkExpMechanic.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Mechanic).ToString()).ToString();
        linkExpTraffic.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Traffic).ToString()).ToString();
        linkExpUrbenism.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Shahrsazi).ToString()).ToString();
        linkExpWelfare.HRef = "~/News.aspx?ExGroupCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Welfare).ToString()).ToString();
    }
    void LoadAssociationInfo()
    {
        TSP.DataManager.ExGroupPeriodManager ExGroupPeriodManager = new TSP.DataManager.ExGroupPeriodManager();
        DataTable dtPeriods = ExGroupPeriodManager.GetActivePeriodByDate(Utility.GetDateOfToday());
        if (dtPeriods.Rows.Count > 0)
        {
            DataViewAssociation.DataSource = dtPeriods;
            DataViewAssociation.DataBind();
            DataViewAssociation.Visible = true;
        }
        else
            DataViewAssociation.Visible = false;
    }
    private void LoadPollInfo()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.SelectPollForHomePage(Utility.GetDateOfToday());
        if (PollPollManager.Count > 0)
        {
            DataViewPoll.DataSource = PollPollManager.DataTable;
            DataViewPoll.DataBind();
            DataViewPoll.Visible = true;
            divPollUniq.Visible = true;
        }
        else
        {
            DataViewPoll.Visible = false;
            divPollUniq.Visible = false;
        }

    }
    //int FindXmlItemIndex(System.Xml.XmlDocument XmlDoc, String ItemName)
    //{
    //    System.Xml.XmlNodeList Items = XmlDoc.GetElementsByTagName("Name");
    //    for (int i = 0; i < Items.Count; i++)
    //    {
    //        if (Items.Item(i).InnerText == ItemName)
    //            return i;
    //    }
    //    return -1;
    //}

    private void Load_Attachments()
    {
        //try
        //{
        //    System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
        //    XmlDoc.Load(MapPath("~/App_Data/OtherAttachments.xml"));
        //    int Index = 0;
        //    System.Xml.XmlNodeList File = XmlDoc.GetElementsByTagName("File");
        //    System.Xml.XmlNodeList Enabled = XmlDoc.GetElementsByTagName("Enabled");
        //    System.Xml.XmlNodeList Text = XmlDoc.GetElementsByTagName("Text");

        //    #region AttachInformationFlash
        //    Index = FindXmlItemIndex(XmlDoc, "AttachInformationFlash");
        //    if (Index > -1)
        //    {
        //        if (Enabled.Item(Index).InnerText == "1")
        //        {
        //            if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
        //            {
        //                PanelFlashFile.Visible = true;
        //                SwfFile1 = File.Item(Index).InnerText.Replace("~/", "");
        //            }
        //            else
        //            {
        //                PanelFlashFile.Visible = false;
        //                SwfFile1 = "";
        //            }
        //        }
        //        else
        //        {
        //            PanelFlashFile.Visible = false;
        //            SwfFile1 = "";
        //        }
        //    }
        //    else
        //    {
        //        PanelFlashFile.Visible = false;
        //        SwfFile1 = "";
        //    }
        //    #endregion

        //    #region AttachEvents
        //    Index = FindXmlItemIndex(XmlDoc, "AttachEvents");
        //    if (Index > -1)
        //    {
        //        if (Enabled.Item(Index).InnerText == "1")
        //        {
        //            if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
        //            {
        //                PanelEvents.Visible = true;
        //                imgEvents.ImageUrl = File.Item(Index).InnerText;
        //                imgEvents.ToolTip = Text.Item(Index).InnerText;
        //            }
        //            else
        //            {
        //                PanelEvents.Visible = false;
        //                imgEvents.ImageUrl = "";
        //                imgEvents.ToolTip = "";
        //            }
        //        }
        //        else
        //        {
        //            PanelEvents.Visible = false;
        //            imgEvents.ToolTip = "";
        //            imgEvents.ImageUrl = "";
        //        }
        //    }
        //    else
        //    {
        //        PanelEvents.Visible = false;
        //        imgEvents.ToolTip = "";
        //        imgEvents.ImageUrl = "";
        //    }
        //    #endregion
        //}
        //catch (Exception err)
        //{
        //    Utility.SaveWebsiteError(err);
        //    return;
        //}
    }

    private void SetClassDynamicCol()
    {
        int count = 0;
        if (divCondolence.Visible)
            count++;
        if (divPollUniq.Visible)
            count++;
        if (count % 2 == 0)
        {
            divCondolence.Attributes["Class"] = "col-md-6 col-sm-6";
            divPollUniq.Attributes["Class"] = "col-md-6 col-sm-6";
            divMap.Attributes["Class"] = "col-md-6 col-sm-6";
            divMag.Attributes["Class"] = "col-md-6 col-sm-6";
        }
        else
        {
            divCondolence.Attributes["Class"] = "col-md-4 col-sm-4";
            divPollUniq.Attributes["Class"] = "col-md-4 col-sm-4";
            divMap.Attributes["Class"] = "col-md-4 col-sm-4";
            divMag.Attributes["Class"] = "col-md-4 col-sm-4";
        }
    }

    private void LoadCondolence()
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        DataTable dtCondolence = CondolenceManager.SelectAvailableCondolence(Utility.GetDateOfToday(), -1);
        if (dtCondolence.Rows.Count == 0)
        {
            divCondolence.Visible = false;
        }
        else
        {
            RepeaterCondolence.DataSource = dtCondolence;
            RepeaterCondolence.DataBind();
        }
        //TSP.DataManager.CondolenceManager CondolenceManager2 = new TSP.DataManager.CondolenceManager();
        //DataTable dt2 = CondolenceManager2.SelectAvailableCondolence(Utility.GetDateOfToday(), 1);
        //if (dt2.Rows.Count == 0)
        //{
        //    imgCongratulations.Visible = false;
        //    //RoundPanelCongratulations.Visible = false;
        //}
        //else
        //{
        //    imgCongratulations.Visible = true;
        //    //RoundPanelCongratulations.Visible = true;

        //}
    }
    private void LoadWelfareScheduleList(){
        linkWelfareSchedule.HRef = "~/News.aspx?SubjectId=" + Utility.EncryptQS(((int)TSP.DataManager.NewsSubjectId.WelfareScheduleList).ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView");
    }
    
    //String GetPortalUrl()
    //{
    //    String Url = "";

    //    switch (Utility.GetCurrentUser_LoginType())
    //    {
    //        case (int)TSP.DataManager.UserType.Member:// 1:
    //            Url = "~/Members/MemberHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        case (int)TSP.DataManager.UserType.TemporaryMembers:// 1:
    //            Url = "~/Members/MemberHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        case (int)TSP.DataManager.UserType.Office://2:
    //            Url = "~/Office/OfficeHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        case 3:
    //            break;
    //        case (int)TSP.DataManager.UserType.Employee://4:
    //            Url = "~/Employee/EmployeeHome.aspx";
    //            break;
    //        case (int)TSP.DataManager.UserType.Teacher://5:
    //            Url = "~/Teachers/TeacherHome.aspx";
    //            break;
    //        case (int)TSP.DataManager.UserType.Institute://6:
    //            Url = "~/Institue/Amoozesh/InstitueHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        case (int)TSP.DataManager.UserType.Settlement://7:
    //            Url = "~/Settlement/SettlmentHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        case (int)TSP.DataManager.UserType.Municipality://8:
    //            Url = "~/Municipality/MunHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
    //            break;
    //        default:
    //            Url = "~/ErrorPage.aspx";
    //            break;
    //    }
    //    return Url;
    //}

    //Boolean SaveTrace(String Username)
    //{
    //    return SaveTrace(Username, -1, -1, TSP.DataManager.TraceManager.Types.LoginUnsuccessful);
    //}

    //Boolean SaveTrace(String Username, int UltId, int MeId, TSP.DataManager.TraceManager.Types TraceType)
    //{
    //    TSP.DataManager.TraceManager TrManager = new TSP.DataManager.TraceManager();
    //    return TrManager.SaveTrace(Username, UltId, MeId, TraceType);
    //}





    //private void SetNewsDataSource(string SelectedMenuName)
    //{
    //    switch (SelectedMenuName)
    //    {
    //        case "ImportantNews":
    //            CheckAndBindImportantNews();
    //            break;
    //        case "News":
    //            CheckAndBindNews();
    //            break;
    //    }
    //}

    //private void CheckAndBindNews()
    //{
    //    TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
    //    DataTable dt = NewsManager.selectTenNews(-1, "%", "%", "1", "2", -1);
    //    //NavigateUrlField="NewsId" NavigateUrlFormatString="javascript:void('{0}');" NameField="NewsId"
    //    //                         DateField="Date" HeaderTextField="NewsHeader" TextField="Summary" ImageUrlField="Imageurl"
    //    DataTable dtNews = new DataTable();
    //    dtNews.Columns.Add("NewsId");
    //    dtNews.Columns.Add("Date");
    //    dtNews.Columns.Add("NewsHeader");
    //    dtNews.Columns.Add("Summary");
    //    dtNews.Columns.Add("Imageurl");
    //    dtNews.Columns.Add("NewsContentURL");
    //    dtNews.Columns.Add("Id");
    //    dtNews.Columns["Id"].AutoIncrement = true;
    //    dtNews.Columns["Id"].AutoIncrementSeed = 1;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        DataRow dr = dtNews.NewRow();
    //        dr["NewsId"] = dt.Rows[i]["NewsId"];
    //        dr["Date"] = dt.Rows[i]["Date"];
    //        dr["NewsHeader"] = dt.Rows[i]["NewsHeader"];
    //        dr["Summary"] = dt.Rows[i]["Summary"];
    //        dr["Imageurl"] = dt.Rows[i]["Imageurl"];
    //        dr["NewsContentURL"] = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(dt.Rows[i]["NewsId"].ToString()) + "&PageMode=" + Utility.EncryptQS("SubjectView")
    //              + "&SubjectId=" + Utility.EncryptQS(dt.Rows[i]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");
    //        dtNews.Rows.Add(dr);
    //    }
    //    if (dtNews.Rows.Count != 0)
    //    {
    //        //NewControlNews.DataSource = dtNews;
    //        //NewControlNews.DataBind();
    //        DevDataViewNews.DataSource = dtNews;
    //        DevDataViewNews.DataBind();
    //        MenuNews.Items.FindByName("News").Visible = true;
    //    }
    //    else
    //    {
    //        MenuNews.Items.FindByName("News").Visible = false;
    //    }
    //}



    //private string SetNewContent(string NewsId)//, DevExpress.Web.CallbackEventArgsBase e)
    //{
    //    string URL = "";
    //    TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
    //    NewsManager.FindByCode(int.Parse(NewsId));
    //    if (NewsManager.Count != 1)
    //    {
    //        CallbackPanelNews.JSProperties["cpNewsContent"] = "";
    //        return URL;
    //    }
    //    CallbackPanelNews.JSProperties["cpNewsContent"] = URL = "NewsShow.aspx?NewsId=" + Utility.EncryptQS(NewsId) + "&PageMode=" + Utility.EncryptQS("SubjectView")
    //              + "&SubjectId=" + Utility.EncryptQS(NewsManager[0]["SubjectId"].ToString()) + "&FromHomePage=" + Utility.EncryptQS("1");

    //    return URL;
    //}

    //private void SetNewsAndBind()
    //{
    //    switch (MenuNews.SelectedItem.Name)
    //    {
    //        case "ImportantNews":
    //            CheckAndBindImportantNews();
    //            break;
    //        case "News":
    //            CheckAndBindNews();
    //            break;
    //    }
    //}


    private void ResetDocSessions()
    {
        Session["WizardDocOath"] =
        Session["WizardDocExam"] =
        Session["WizardDocMajor"] =
        Session["WizardDocResposblity"] =
        Session["WizardDocPeriods"] =
        Session["WizardDocJob"] =
        Session["WizardDocSummary"] =
        Session["JobFileURL"] =
        Session["WizardDocJobConfirm"] =
        Session["JobGrdURL"] =
        Session["ACCFileURL"] =
        Session["FishFileURL"] =
        Session["chbIAgree"] =
        Session["ExamFileImgURLSoource"] =
        Session["ImgTaxOfficeLetter"] = null;
    }
    private void ResetQualification()
    {
        Session["WizardDocQualificationExam"] =
        Session["WizardDocQualificationSummary"] =
        Session["WizardDocQualificationOath"] =
        Session["WizardQualificationJobConfirm"] =
        Session["DocQualificationJobFileURL"] =
        Session["DocQualificationJobGrdURL"] =
        Session["WizardQualificationchbIAgree"] =
        Session["WizardQualificationImgTaxOfficeLetter"] =
        Session["WizardQualificationImgfrontDoc"] =
Session["WizardQualificationImgBackDoc"] =
        null;
    }
    private void ResetRevival()
    {
        Session["WizardDocRevivalSummary"] =
        Session["WizardDocRevivalOath"] =
        Session["WizardRevivalJobConfirm"] =
        Session["DocRevivalJobFileURL"] =
        Session["DocRevivalJobGrdURL"] =
        Session["WizardRevivalImgTaxOfficeLetter"] =
        Session["WizardRevivalImgfrontDoc"] =
        Session["WizardRevivalImgBackDoc"] =
        Session["ImgPeriodImage"] =
        Session["ImgTaxOfficeLetter"] =
        Session["WizardRevivalCivilLicence"] = null;
    }

    private Boolean CheckConditions()
    {
        try
        {
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForNewDocument(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }
    private Boolean CheckDocQualificationConditions()
    {
        try
        {
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentQualification(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckDocRevivalConditions()
    {
        try
        {
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentRevival(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    void LoadAmoozeshAttach()
    {
        try
        {
            System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
            XmlDoc.Load(MapPath("~/App_Data/OtherAttachments.xml"));
            System.Xml.XmlNodeList File = XmlDoc.GetElementsByTagName("File");
            System.Xml.XmlNodeList Enabled = XmlDoc.GetElementsByTagName("Enabled");
            System.Xml.XmlNodeList Text = XmlDoc.GetElementsByTagName("Text");
            int Index = -1;
            #region HelpLMS
            Index = FindXmlItemIndex(XmlDoc, "HelpLMS");
            if (Index > -1)
            {

                if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
                {
                 linkLMSHelp.HRef= File.Item(Index).InnerText;
                }
            }

            #endregion

            #region AmoozeshSchedule
            Index = FindXmlItemIndex(XmlDoc, "AmoozeshSchedule");
            if (Index > -1)
            {

                if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
                {
                   linkSchedule.HRef= File.Item(Index).InnerText;
                    //imgEndUploadSchedule.ToolTip = Text.Item(Index).InnerText;
                }
            }

            #endregion
        }
        catch (Exception)
        {
         
        }
    }

    int FindXmlItemIndex(System.Xml.XmlDocument XmlDoc, String ItemName)
    {
        System.Xml.XmlNodeList Items = XmlDoc.GetElementsByTagName("Name");
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items.Item(i).InnerText == ItemName)
                return i;
        }
        return -1;
    }
    #endregion
}