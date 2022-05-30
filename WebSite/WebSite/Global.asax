<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">
    
    void Application_Start(object sender, EventArgs e)
    {
        DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
        DevExpress.Utils.UrlAccessSecurityLevelSetting.SecurityLevel = DevExpress.Utils.UrlAccessSecurityLevel.Unrestricted;

        // Code that runs on application startup
        //Session.Timeout = 15;
        Application["MemberRegTimeout"] = "30";
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern = 14;
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberNegativePattern = 0;
        BundleConfig.RegisterBundles(BundleTable.Bundles);
      
        //string cnn = TSP.DataManager.DBManager.CnnStr;
        ////*************فعلا برای تست سرعت کامنت شد**************
        //System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(cnn);
        //string[] tables ={ "tblCity", "tblCountry", "tblUniversity" };
        //// , "tblIntroduction"و"tblEmployee", "tblMemberRequest", "tblMember", "tblTempMember", "tblOffice", "tblEngOffice", "[Accounting.Account]"", "tblOfficeRequest", "tblOfficeMember", "[Doc.MemberFile]" ,"tblWorkFlowState","tblPeriodRegister"
        //System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(cnn, tables);
        //******************************************************
        //     System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyNegativePattern=14;// = new System.Globalization.CultureInfo("En-en");
        //System.Web.HttpContext.Current.


        #region WebsiteStatistics
       
        Application["OnlineVisitors"] = 0;
        Application["OnlineUsers"] = 0;
        #endregion
       
       
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

     
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //Server.Transfer("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started       
        Session["CurrentPage"] = "Home.aspx";
        Session["MeId"] = null;       
        Session["LoginName"] = "0";

        Session["Member"] = null;
        Session["Office"] = null;
        //////////Session["TblActivity"] = null;
        //////////Session["TblActivity2"] = null;
        //////////Session["TblResearch"] = null;

        ////////////Session["TblJob"] = null;
        ////////////Session["TblLanguage"] = null;
        ////////////Session["TblOfAgent"] = null;
        ////////////Session["TblOfMember"] = null;
        ////////////Session["TblOfKardan"] = null;
        ////////////Session["TblOfOthers"] = null;
        ////////////Session["TblOfLetter"] = null;
        ////////////Session["TblOfMadrak"] = null;

        Session["Password"] = null;
        Session["PasswordMe"] = null;
        //////////Session["ImgMember"] = null;
        //////////Session["FileOfMember"] = null;
        //////////Session["ImgMember2"] = null;
        //////////Session["FileOfMember2"] = null;

        //////////Session["ImgOfArm"] = null;
        //////////Session["ImgOfSign"] = null;
        //////////Session["PathOfArm"] = null;
        //////////Session["PathOfSign"] = null;
        //////////Session["ImgOfArm"] = null;
        //////////Session["ImgOfSign"] = null;
        //////////Session["fileOfArm"] = null;
        //////////Session["fileOfSign"] = null;
        //////////Session["TblOfShaki"] = null;
        //////////Session["TblOfMoteshaki"] = null;

        //////////////Session["TblOfImg"] = null;
        //////////////Session["TblOfImg1"] = null;
        //////////////Session["TblOfImg2"] = null;
        //////////////Session["TblOfImg3"] = null;
        //////////////Session["TblOfImg4"] = null;
        //////////////Session["TblOfImg5"] = null;
        //////////////Session["TblOfImg6"] = null;
        //////////////Session["TblOfImg7"] = null;
        //////////////Session["TblOfImg8"] = null;
        //////////////Session["TblOfImg9"] = null;
        //////////////Session["TblOfImg10"] = null;


        Session["CurrentTab"] = 0;
        Session["PrePages"] = null;
        ////////Session["NewsId"] = null;

        ////////Session["ٍCancelMode"] = null;
        ////////Session["PageMode"] = null;
        Session["ShowSecurityCode"] = null;

        ////////////Session["MfId"] = -1;


        //////////Session["SelectedMembers"] = null;
        //////////Session["SelectedMembers2"] = null;
        ////////Session["MsgId"] = null;
        ////////Session["MsgId2"] = null;
        ////////Session["MsgId3"] = null;
        Session["FutureStatus"] = null;
        Session["ExpireDay"] = null;
        //////////Session["tblSaleSrvInsert"] = null;
        //////////Session["OfficeMemberManager"] = null;
        //////////Session["LetterImg"] = null;
        Session["GrCheched"] = null;
        ////////Session["MeMsg"] = null;

        Session["GrMeCheched"] = null;
        Session["GrOfCheched"] = null;
        Session["RegionCheched"] = null;
        Session["Shortage"] = null;
        Session["Schedule"] = null;
        //////////Session["SendBckDataTablePeriod"] = null;
        //////////Session["SeminarUpload"] = null;
        //////////Session["TeacherUpload"] = null;
        //////////Session["SeminarSchedule"] = null;
        //////////Session["ExPlaceUpload"] = null;
        //////////Session["ExFileUpload"] = null;
        //////////Session["ExAnswerUpload"] = null;
        //////////Session["NewsUpload"] = null;
        //////////Session["MeAttachUpload"] = null;
        //////////Session["OffAttachUpload"] = null;
        //////////Session["question"] = null;
        //////////Session["PPTeacher"] = null;
        //////////Session["TrRuleUpload"] = null;
        //////////Session["SemTeacher"] = null;
        //////////Session["CourseGroup"] = null;
        //////////Session["TestAbsent"] = null;
        //////////Session["TestWrong"] = null;
        //////////Session["TestObserver"] = null;

        #region WebsiteStatistics
        ////مربوط به امار کل بازدید کنندگان
          //TSP.DataManager.WebsiteStatisticsManager.UpdateStatistics();
        Application.Lock();
        Application["OnlineVisitors"] = Utility.SetOnlineVisitors(1);
        Application["DailyCumVisitors"] = Utility.SetDailyVisitor();
        Application["WeeklyCumVisitors"] = Utility.SetWeeklyVisitor();
        Application["MonthlyCumVisitors"] = Utility.SetMonthlyVisitor();
        Application.UnLock();
        #endregion
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

      
        #region WebsiteStatistics
        Application.Lock();
        Application["OnlineVisitors"] = Utility.SetOnlineVisitors(-1);
        Application.UnLock();
        #endregion
    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                        (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] AllUserData = userData.Split(',');//Role Names & UserId & ...
                    string[] Roles = AllUserData[0].Split(';');//Role Names
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, Roles);
                }
            }
        }
    }

</script>
