using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Web.Optimization;


public partial class MasterPageWebsite : System.Web.UI.MasterPage
{
    public string EmbedSrc
    {
        get;
        set;
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            Metaurl.Content = Request.Url.AbsoluteUri;
            Metatitle.Content = Page.Title;

            if (Request.Url.LocalPath.ToString() == "/Home.aspx" || Request.Url.LocalPath.ToString() == "/Default.aspx")
            {
                DivSiteMap.Visible = false;
            }
            else
            {
                DivSiteMap.Visible = true;
            }
            CheckSettingMemberAfterLogin();
            MenulblUserFullName.InnerText = Utility.GetCurrentUser_FullName();
            string UserImage = Utility.GetCurrentUser_ImageUrl();
            if (!Utility.IsDBNullOrNullValue(UserImage))
                ImageUser.Src = Utility.GetCurrentUser_ImageUrl().Replace("~", "");

        }
        SetAuthenticatedUser();

        linkNewsMenu.HRef = linkNewsMenu2.HRef = "~/News.aspx?ImpId=" + Utility.EncryptQS("0");
        NewsArchImp2.HRef = NewsArchImp.HRef = linkHighlyImportanNewsMenu.HRef = "~/News.aspx?ImpId=" + Utility.EncryptQS("2");
        linkManagementcommittee.HRef = "/Association/ExGroupPeriods.aspx?ExGrpCode=" + Utility.EncryptQS(((int)TSP.DataManager.ExGroupManager.Type.Managementcommittee).ToString()).ToString();
        Load_Attachments();

        AgentLink();


        //ScriptManager.ScriptResourceMapping.AddDefinition("MsAjaxBundle", new ScriptResourceDefinition
        //{
        //    Path = "~/bundles/MsAjaxJs",
        //    CdnPath = "http://ajax.aspnetcdn.com/ajax/4.5/6/MsAjaxBundle.js",
        //    LoadSuccessExpression = "window.Sys",
        //    CdnSupportsSecureConnection = true
        //});
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjax.js", "window.Sys && Sys._Application && Sys.Observer");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxCore.js", "window.Type && Sys.Observer");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxGlobalization.js", "window.Sys && Sys.CultureInfo");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxSerialization.js", "window.Sys && Sys.Serialization");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxComponentModel.js", "window.Sys && Sys.CommandEventArgs");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxNetwork.js", "window.Sys && Sys.Net && Sys.Net.WebRequestExecutor");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxHistory.js", "window.Sys && Sys.HistoryEventArgs");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxWebServices.js", "window.Sys && Sys.Net && Sys.Net.WebServiceProxy");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxTimer.js", "window.Sys && Sys.UI && Sys.UI._Timer");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxWebForms.js", "window.Sys && Sys.WebForms");
        //PreApplicationStartCode.AddMsAjaxMapping("MicrosoftAjaxApplicationServices.js", "window.Sys && Sys.Services");

    }
    protected void btnLogOut_ServerClick(object sender, EventArgs e)
    {
        SaveTrace(Utility.GetCurrentUser_Username(), Utility.GetCurrentUser_LoginType(), Utility.GetCurrentUser_MeId());

        if (Session["Login"] != null && Session["Login"].ToString() != "0")
        {
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindByCode(int.Parse(Session["Login"].ToString()));
            LogManager[0]["IsSignIn"] = false;
            LogManager.Save();
        }
        Session.Abandon();
        Session.Clear();

        #region WebsiteStatistics
        Application.Lock();
        Application["OnlineUsers"] = Utility.SetOnlineUsers(-1);
        Application.UnLock();
        #endregion

        FormsAuthentication.SignOut();

        Response.Redirect("~/login.aspx");

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
    #endregion

    #region Methods
    Boolean SaveTrace(String Username, int UltId, int MeId)
    {
        TSP.DataManager.TraceManager TrManager = new TSP.DataManager.TraceManager();
        return TrManager.SaveTrace(Username, UltId, MeId, TSP.DataManager.TraceManager.Types.LogOut);
    }

    void AgentLink()
    {
        TSP.DataManager.AccountingAgentManager AccountingAgentManager = new TSP.DataManager.AccountingAgentManager();
        DataTable dt = AccountingAgentManager.GetData();
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        DataTable dt4 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 4 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 4 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 4 == 2) { dt3.Rows.Add(dr.ItemArray); }
            if (count % 4 == 3) { dt4.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterAgent1.DataSource = dt1;
            RepeaterAgent1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterAgent2.DataSource = dt2;
            RepeaterAgent2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterAgent3.DataSource = dt3;
            RepeaterAgent3.DataBind();
        }
        if (dt4.Rows.Count != 0)
        {
            RepeaterAgent4.DataSource = dt4;
            RepeaterAgent4.DataBind();
        }
    }

    /// <summary>
    /// تنظیمات مربوط به پرتال اعضا در صورت پرنکردن اطلاعات اجباری در واحد عضویت
    /// </summary>
    void CheckSettingMemberAfterLogin()
    {
       
        if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
            return;

        Boolean Flag = false;
        int MeId = Utility.GetCurrentUser_MeId();
        String RedirectPageName = "";
        String RedirectPageAddress = "";

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 0)
            return;

        if (Utility.IsDBNullOrNullValue(MemberManager[0]["Email"]) || Utility.IsDBNullOrNullValue(MemberManager[0]["MobileNo"]))
        {
            Flag = true;
            RedirectPageName = "ChangeMemberData.aspx".ToLower();
            RedirectPageAddress = "~/Members/ChangeMemberData.aspx";
        }
        else if (Utility.IsDBNullOrNullValue(MemberManager[0]["UserInfoType"]) || Convert.ToInt32(MemberManager[0]["UserInfoType"]) == (int)TSP.DataManager.MemberPrivateInfoSettingType.UnSelected)
        {
            Flag = true;
            RedirectPageName = "MemberPrivateInfoSetting.aspx".ToLower();
            RedirectPageAddress = "~/Users/MemberPrivateInfoSetting.aspx";
        }
        else if (Utility.IsDBNullOrNullValue(MemberManager[0]["RecieveMagazine"]) || Convert.ToInt32(MemberManager[0]["RecieveMagazine"]) == (int)TSP.DataManager.MemberPrivateInfoSettingType.UnSelected)
        {
            Flag = true;
            RedirectPageName = "RecieveMagazineSetting.aspx".ToLower();
            RedirectPageAddress = "~/Users/RecieveMagazineSetting.aspx";
        }


        if (Flag == false)
            return;
        else if (GetCurrentPageAddress() != RedirectPageName)
            Response.Redirect(RedirectPageAddress);
    }

    String GetCurrentPageAddress()
    {
        return System.IO.Path.GetFileName(Request.PhysicalPath).ToLower();
    }

    /// <summary>
    /// مسیر صفحه اصلی برای هر یک از پرتال ها بر اساس نوع کاربر جاری را بدست می آورد
    /// </summary>
    /// <returns></returns>
    String GetPortalUrl()
    {
        String Url = "";

        switch (Utility.GetCurrentUser_LoginType())
        {
            case (int)TSP.DataManager.UserType.Member:// 1:
                Url = "~/Members/MemberHome.aspx";// "/Members/TechnicalServices/Capacity/QueueListMunicipality.aspx";
                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:// 1:
                Url = "~/Members/MemberHome.aspx";
                break;
            case (int)TSP.DataManager.UserType.Office://2:
                Url = "~/Office/OfficeHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case 3:
                break;
            case (int)TSP.DataManager.UserType.Employee://4:
                Url = "~/Employee/EmployeeHome.aspx";
                break;
            case (int)TSP.DataManager.UserType.Teacher://5:
                Url = "~/Teachers/TeacherHome.aspx";
                break;
            case (int)TSP.DataManager.UserType.Institute://6:
                Url = "~/Institue/Amoozesh/InstitueHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.Settlement://7:
                Url = "~/Settlement/SettlmentHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.Municipality://8:
                Url = "~/Municipality/MunHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.TSProjectOwner:
                Url = "~/Owner/OwnerHome.aspx";

                break;
            default:
                Url = "~/ErrorPage.aspx";
                break;
        }
        return Url;
    }

    void SetAuthenticatedUser()
    {
        if (Page.User.Identity.IsAuthenticated == true)
        {
            dropdownMeInfo.Visible = true;
            btnLogin.Visible = false;
            //if (Session["Login"] == null || Session["Login"].ToString() == "0")
            //{
            //    Session["Login"] = Utility.GetCurrentUser_UserId();
            //    Session["LoginName"] = Utility.GetCurrentUser_Username();
            //    Session["LoginType"] = Utility.GetCurrentUser_LoginType();
            //    Session["MeId"] = Utility.GetCurrentUser_MeId();
            //}
            //if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            //    PanelMasterSearch.Visible = true;

            //PanelLoginInfo.Visible = false;
            //ASPxMenu_Top.Visible = false;
            //if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee || Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            //    ASPxMenu_Top_Users.Items.FindByName("Help").Visible = true;
            //else
            //    ASPxMenu_Top_Users.Items.FindByName("Help").Visible = false;
            //ASPxMenu_Top_Users.Items.FindByName("UserInfo").Text = Utility.GetCurrentUser_FullName();
            btngotoPortal.HRef = GetPortalUrl();
            //ASPxMenu_Top_Users.Visible = true;
        }
        else
        {
            dropdownMeInfo.Visible = false;
            btnLogin.Visible = true;
            //PanelLoginInfo.Visible = true;
            //PanelLoginUser.Visible = false;
            //ASPxMenu_Top.Visible = true;
            //ASPxMenu_Top_Users.Visible = false;
        }
    }

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

        //            }
        //            else
        //            {
        //                PanelFlashFile.Visible = false;
        //                EmbedSrc = File.Item(Index).InnerText.Replace("~/", "");
        //            }
        //        }
        //        else
        //        {
        //            PanelFlashFile.Visible = false;
        //            //objFlashFile1.src= "";
        //        }
        //    }
        //    else
        //    {
        //        PanelFlashFile.Visible = false;
        //        //objFlashFile1.src = "";
        //    }
        //    #endregion


        //}
        //catch (Exception err)
        //{
        //    Utility.SaveWebsiteError(err);
        //    return;
        //}
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
            Utility.SaveWebsiteError(err);
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
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }
    private void ShowMessage(string Message)
    {
        //this.DivReport.Visible = true;
        //this.LabelWarning.Text = Message;
    }
    #endregion
}
