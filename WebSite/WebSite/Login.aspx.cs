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
using System.Web.Configuration;

public partial class login : System.Web.UI.Page
{

    string HostName = System.Web.HttpContext.Current.Request.UserHostName;
    string Address = System.Web.HttpContext.Current.Request.UserHostAddress;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated == true)
        {
            #region Set Info Sessions
            Session["ShowSecurityCode"] = false;
            int UserId = Utility.GetCurrentUser_UserId();
            int UltId = Utility.GetCurrentUser_LoginType();
            int MeId = Utility.GetCurrentUser_MeId();
            #endregion

            try
            {
                string ReferPage = Request.QueryString["ReferPage"];
                Response.Redirect(ReferPage);
            }
            catch (Exception err) { }

            String RedirectUrl = "";
            if (GetPortalUrl(ref RedirectUrl, UltId, MeId, UserId))
            {
                Response.Redirect(RedirectUrl);
            }
            else
            {
                FormsAuthentication.SignOut();
            }
        }

        if (!IsPostBack)
        {

            panelSecurityCode.Visible = false;
            ViewState["TryLogin"] = false;
        }

        if (Session["ShowSecurityCode"] != null)
        {
            if ((Boolean)Session["ShowSecurityCode"] == true)
            {
                panelSecurityCode.Visible = true;
            }
        }

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        txtUsername.Text = Server.HtmlEncode(txtUsername.Text);
        txtPass.Text = Server.HtmlEncode(txtPass.Text);

        ViewState["TryLogin"] = true;
        lblError.Visible = false;
        lblError.Text = "";
        bool HaveTempPass = false;
        bool TryLogin = false;

        TSP.DataManager.TraceManager TrManager = new TSP.DataManager.TraceManager();
        int Forbiden = TrManager.SelectAttemptPass(-1, -1, Address, HostName, (int)TSP.DataManager.TraceManager.Types.LoginForbiden, 20, "%");
        if (Forbiden > 0)
        {
            SaveTrace(txtUsername.Text);
            ShowInputError("بدلیل تلاش های مکرر برای ورود حداقل تا 20 دقیقه دیگر نمی توانید وارد سیستم شوید در صورت تلاش بیشتر این مدت زمان به صورت تصاعدی افزایش می یابد", true);
            return;
        }

        int Attampt = TrManager.SelectAttemptPass(-1, -1, Address, HostName, (int)TSP.DataManager.TraceManager.Types.LoginUnsuccessful, 5, "%");
        if (Attampt <= 5)
            TryLogin = true;

        if (CheckSecurityCode() == true)
        {
            #region check UserName&Pass
            Utility.Login objLogin = new Utility.Login(txtUsername.Text, txtPass.Text);
            if (objLogin.CheckLogin() == false)
            {
                if (TryLogin)
                    SaveTrace(txtUsername.Text);
                else
                    SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
                ShowInputError("نام کاربری یا رمز عبور اشتباه است", true);
                return;
            }

            TSP.DataManager.LoginManager LoginManage = new TSP.DataManager.LoginManager();
            LoginManage.FindByCode(objLogin.UserId);
            if (LoginManage.Count <= 0)
            {
                if (TryLogin)
                    SaveTrace(txtUsername.Text);
                else
                    SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
                ShowInputError("نام کاربری یا رمز عبور اشتباه است", true);
                return;
            }


            ViewState["TryLogin"] = false;
            DataTable LoginData = LoginManage.DataTable;

            int UltId = int.Parse(LoginData.Rows[0]["UltId"].ToString());
            int NmcIdType = -1;
            #region Set NmcIdType & HaveTempPass by UltId
            switch (UltId)
            {
                case (int)TSP.DataManager.UserType.Member: //"1":

                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassMeId"]);
                    break;
                case (int)TSP.DataManager.UserType.Office:// "2":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassOfficId"]);
                    break;
                case (int)TSP.DataManager.UserType.Employee:// "4":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassEmpId"]);
                    break;
                case (int)TSP.DataManager.UserType.Municipality://"8":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.Munipulicity;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassMunipulicityId"]);
                    break;
                case (int)TSP.DataManager.UserType.Institute:// "6":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassStlId"]);
                    break;
                case (int)TSP.DataManager.UserType.Teacher:// "7":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.Teacher;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassStlId"]);
                    break;
                case (int)TSP.DataManager.UserType.TemporaryMembers:// "9":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TempMember;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTempMemberId"]);
                    break;

                case (int)TSP.DataManager.UserType.TSProjectOwner:// "9":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TSProjectOwner;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTSProjectOwnerId"]);
                    break;

                case (int)TSP.DataManager.UserType.TSImplementerOffice:// "9":
                    NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TSImplementerOffice;
                    HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTSImplementerOffice"]);
                    break;
                    
                default:
                    NmcIdType = -1;
                    break;
            }
            #endregion
            #region Save tryLoing in tblTrace
            if (!objLogin.IsTspAdmin && HaveTempPass && objLogin.NeedTempPass == true)
            {
                if (Utility.IsDBNullOrNullValue(txtTempPass.Text))
                {
                    if (TryLogin)
                        SaveTrace(txtUsername.Text);
                    else
                        SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
                    ShowInputError("شما باید رمز یکبار عبور را وارد نمایید", true);
                    return;
                }

                //در جدول تریس برای این فرد در مدت زمان 10 دقیقه قبل جستجو کن که آیا در آخرین 
                //پارامترها باید طوری باشد که بهینه ترین حالت ممکن باشد و در ضمن ایندکس گذاری بر حسب پارامترها
                //و ایندکس ترکیبی حتما باید گذارده شود

                string TempPass = TrManager.SelectTempPass(int.Parse(LoginData.Rows[0]["MeId"].ToString()), int.Parse(LoginData.Rows[0]["UltId"].ToString()), LoginData.Rows[0]["Username"].ToString());

                if (TempPass != txtTempPass.Text)
                {
                    if (TryLogin)
                        SaveTrace(txtUsername.Text);
                    else
                        SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
                    ShowInputError("رمز یکبار عبور معتبر نمی باشد", true);
                    return;
                }

                //اگر خالی باشد که پیغام خطا بدهد
                // اگر خالی نبود حالا تازه چک کند که با مقدار وارد شده برابر است یا نه

            }
            #endregion

            if (!objLogin.IsTspAdmin)
            {
                SaveTrace(LoginData.Rows[0]["Username"].ToString(), int.Parse(LoginData.Rows[0]["UltId"].ToString()), int.Parse(LoginData.Rows[0]["MeId"].ToString()), TSP.DataManager.TraceManager.Types.Login);
            }

            #region WebsiteStatistics
            Application.Lock();
            Application["OnlineUsers"] = Utility.SetOnlineUsers(1);
            Application.UnLock();
            #endregion

            Session["ShowSecurityCode"] = false;
            int UserId = int.Parse(LoginData.Rows[0]["UserId"].ToString());

            int MeId = int.Parse(LoginData.Rows[0]["MeId"].ToString());
            int NmcId = -2;
            if (UltId.ToString() == "4")
            {
                TSP.DataManager.NezamMemberChartManager nezamMemberchart = new TSP.DataManager.NezamMemberChartManager();
                nezamMemberchart.FindByMember(MeId, 4, 0);
                if (nezamMemberchart.Count > 1)
                    nezamMemberchart.CurrentFilter = "IsMasterPosition=1";
                if (nezamMemberchart.Count == 1)
                    NmcId = Convert.ToInt32(nezamMemberchart[0]["NmcId"]);
            }
            string UserImageUrl = LoginData.Rows[0]["ImageURL"].ToString();
            String RedirectUrl = "";
            if (GetRedirectPageUrl(ref RedirectUrl, UltId, MeId, UserId))
            {
                if (NmcId == -2)
                    CreateLoginCookie(LoginData.Rows[0]["Username"].ToString(), LoginData.Rows[0]["UltId"].ToString(), UserId.ToString(), LoginData.Rows[0]["MeId"].ToString(), LoginData.Rows[0]["UserAgentId"].ToString(), LoginData.Rows[0]["FullName"].ToString(), NmcIdType.ToString(), objLogin.IsTspAdmin, UserImageUrl);
                else
                    CreateLoginCookie(LoginData.Rows[0]["Username"].ToString(), LoginData.Rows[0]["UltId"].ToString(), UserId.ToString(), LoginData.Rows[0]["MeId"].ToString(), LoginData.Rows[0]["UserAgentId"].ToString(), LoginData.Rows[0]["FullName"].ToString(), NmcId.ToString(), NmcIdType.ToString(), objLogin.IsTspAdmin, UserImageUrl);
                Response.Redirect(RedirectUrl);
            }
            else
            {
                if (UltId == (int)TSP.DataManager.UserType.Member && !Utility.IsDBNullOrNullValue(RedirectUrl))
                    Response.Redirect(RedirectUrl);
                else
                    Session["ShowSecurityCode"] = true;
            }
            #endregion

        }
        else
        {
            if (TryLogin)
                SaveTrace(txtUsername.Text);
            else
                SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
            ShowInputError("کد امنیتی وارد شده اشتباه است", true);
        }
        txtUsername.Text = "";
        txtPass.Text = "";
        //txtSecurityCode.Text = "";
    }

    protected void btnTempPass_Click(object sender, EventArgs e)
    {
        txtUsername.Text = Server.HtmlEncode(txtUsername.Text);
        txtPass.Text = Server.HtmlEncode(txtPass.Text);
        lblError.Visible = false;
        lblError.Text = "";
        string MobileNo = "";
        bool HaveTempPass = false;
        bool TryLogin = false;

        TSP.DataManager.TraceManager TrManager = new TSP.DataManager.TraceManager();
        int ForbidenLogin = TrManager.SelectAttemptPass(-1, -1, Address, HostName, (int)TSP.DataManager.TraceManager.Types.LoginForbiden, 20, "%");
        if (ForbidenLogin > 0)
        {
            SaveTrace(txtUsername.Text);
            ShowInputError("بدلیل تلاش های مکرر برای ورود حداقل تا 20 دقیقه دیگر نمی توانید وارد سیستم شوید در صورت تلاش بیشتر این مدت زمان به صورت تصاعدی افزایش می یابد", true);
            return;
        }

        int AttamptLogin = TrManager.SelectAttemptPass(-1, -1, Address, HostName, (int)TSP.DataManager.TraceManager.Types.LoginUnsuccessful, 5, "%");
        if (AttamptLogin <= 5)
            TryLogin = true;

        if (CheckSecurityCode() == false)
        {
            if (TryLogin)
                SaveTrace(txtUsername.Text);
            else
                SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
            ShowInputError("کد امنیتی وارد شده اشتباه است", true);
        }

        Utility.Login objLogin = new Utility.Login(txtUsername.Text, txtPass.Text);
        if (objLogin.CheckLogin() == false)
        {
            if (TryLogin)
                SaveTrace(txtUsername.Text);
            else
                SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
            ShowInputError("نام کاربری یا رمز عبور اشتباه است", true);
            return;
        }

        TSP.DataManager.LoginManager LoginManage = new TSP.DataManager.LoginManager();
        LoginManage.FindByCode(objLogin.UserId);
        if (LoginManage.Count <= 0)
        {
            if (TryLogin)
                SaveTrace(txtUsername.Text);
            else
                SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.LoginForbiden);
            ShowInputError("نام کاربری یا رمز عبور اشتباه است", true);
            return;
        }

        ViewState["TryLogin"] = false;

        DataTable LoginData = LoginManage.DataTable;


        switch (Convert.ToInt32(LoginData.Rows[0]["UltId"]))
        {
            case (int)TSP.DataManager.UserType.Member:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassMeId"]);
                break;
            case (int)TSP.DataManager.UserType.Office:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassOfficId"]);
                break;
            case (int)TSP.DataManager.UserType.Employee:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassEmpId"]);
                break;
            case (int)TSP.DataManager.UserType.Municipality:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassMunipulicityId"]);
                break;
            case (int)TSP.DataManager.UserType.Settlement:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassStlId"]);
                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTempMemberId"]);
                break;
            case (int)TSP.DataManager.UserType.TSProjectOwner:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTSProjectOwnerId"]);
                break;
            case (int)TSP.DataManager.UserType.TSImplementerOffice:
                HaveTempPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["HaveTempPassTSImplementerOffice"]);
                break;                
            default:
                break;
        }

        if (!HaveTempPass)
        {
            SaveTrace(txtUsername.Text);
            ShowInputError("در حال حاضر نیازی به ورود رمز یکبار عبور نمی باشد", false);
            return;
        }
        if (objLogin.NeedTempPass == false)
        {
            SaveTrace(txtUsername.Text);
            ShowInputError("ثبت نام رمز یکبار عبور برای شما صورت نگرفته است", false);
            return;
        }

        int ForbidenTemp = TrManager.SelectAttemptPass(-1, Convert.ToInt32(LoginData.Rows[0]["UltId"]), "%", "%", (int)TSP.DataManager.TraceManager.Types.PassSmsForbiden, 10, txtUsername.Text);
        if (ForbidenTemp > 0)
        {
            SaveTrace(txtUsername.Text);
            ShowInputError("هنوز مدت زمان ده دقیقه برای تولید رمز یکبار عبور سپری نشده است", true);
            return;
        }

        int AttamptTemp = TrManager.SelectAttemptPass(-1, Convert.ToInt32(LoginData.Rows[0]["UltId"]), "%", "%", (int)TSP.DataManager.TraceManager.Types.TempPass, 6, txtUsername.Text);
        if (AttamptTemp > 3)
        {
            SaveTraceForbiden(txtUsername.Text, TSP.DataManager.TraceManager.Types.PassSmsForbiden);
            ShowInputError("شما در بازه زمانی اخیر چندین بار رمز یکبار عبور گرفته اید برای شما تا ده دقیقه دیگر رمز یکبار عبور صادر نخواهد شد", true);
            return;
        }

        if (!objLogin.IsTspAdmin)
        {
            //create random digit and save in tblTrace
            Random random = new Random();
            string TempPass = random.Next(1000, 9999).ToString();
            //save in tblTrace 
            if (SaveTrace(LoginData.Rows[0]["Username"].ToString(), int.Parse(LoginData.Rows[0]["UltId"].ToString()), int.Parse(LoginData.Rows[0]["MeId"].ToString()), TSP.DataManager.TraceManager.Types.TempPass, TempPass))
            {
                DataTable dtRecivers = new DataTable();
                dtRecivers.Columns.Add("SMSMobileNo");
                dtRecivers.Columns.Add("SMSMeId");
                dtRecivers.Columns.Add("SMSUltId");

                //fech mobile number in spSelectLogin 
                //فعلا فقط برای کارمندان و مسکن و موسسات باشد و  موبایل ایشان استخراج شود اگر هرنوع دیگری بود بگوید سیستم یکبار رمز عبور فعلا برای شما فعال نیست
                // اگر موبایل نداشت باید پیام دهد شما شماره موبایل فعال ندارید
                //اگر موبایل داشت قسمتی از شماره موبایل را نشان دهد که به این شماره پیامک ارسال شد
                // برای اینجا بهتر است یک گزارش سریع بنویسیم که از روی جدول های مختلف فقط شماره تلفن و شماره آی دی آن را بازگرداند بر اساس همین نوع یوزر
                MobileNo = LoginData.Rows[0]["MobileNo"].ToString();
                if (Utility.IsDBNullOrNullValue(MobileNo))
                {
                    SaveTrace(txtUsername.Text);
                    ShowInputError(".شماره موبایل معتبری برای شما در سیستم موجود نیست", false);
                    return;
                }
                DataRow dr = dtRecivers.NewRow();
                dr["SMSMobileNo"] = MobileNo;
                dr["SMSMeId"] = LoginData.Rows[0]["MeId"].ToString();
                dr["SMSUltId"] = LoginData.Rows[0]["UltId"].ToString();

                dtRecivers.Rows.Add(dr);
                dtRecivers.AcceptChanges();

                //send sms contain TempPass
                SendSMSNotification SMSNotifications = new SendSMSNotification(Utility.Notifications.NotificationTypes.AutomaticSMS);
                SMSNotifications.SendSMSNote(dtRecivers, "سازمان نظام مهندسی ساختمان استان فارس - رمز یکبار عبور -" + TempPass);

                MobileNo = MobileNo.Substring(9, 2) + "******" + MobileNo.Substring(0, 3);
                ShowInputError("سیستم در حال ارسال پیامک به شماره" + MobileNo + "می باشد", false);
            }
        }

        txtTempPass.Text = "";

        //we must refresh scurity code

        //txtSecurityCode.Text = "";
    }

    #endregion

    #region Methods

    #region Create Coockie
    void CreateLoginCookie(String Username, String UltId, String UserId, String MeId, String AgentId, string FullName, string NmcIdType, Boolean IsTspAdmin, string UserImageUrl)
    {
        CreateLoginCookie(Username, UltId, UserId, MeId, AgentId, FullName, "", NmcIdType, IsTspAdmin, UserImageUrl);
    }

    void CreateLoginCookie(String Username, String UltId, String UserId, String MeId, String AgentId, string FullName, string NmcId, string NmcIdType, Boolean IsTspAdmin, string UserImageUrl)
    {
        // Create a new ticket used for authentication
        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
           1, // Ticket version
           Username, // Username associated with ticket
           DateTime.Now, // Date/time issued
           DateTime.Now.AddMinutes(Utility.GetLoginCookieTimeOut()), // Date/time to expire
           false, // "true" for a persistent user cookie
           UltId + "," //0
           + UserId + ","  //1
           + MeId + "," //2
           + AgentId + ","  //3
           + FullName + ","  //4
           + NmcId + ","  //5
           + NmcIdType + ","  //6
           + getLockStatus(int.Parse(MeId), int.Parse(UltId)).ToString() + "," //7
           + ((IsTspAdmin) ? "1" : "0") + ","  //8 // User-data, in this case the roles & UserId & MeId & AgentId
           + UserImageUrl, //9
           FormsAuthentication.FormsCookiePath);// Path cookie valid for

        string hash = FormsAuthentication.Encrypt(ticket);
        HttpCookie cookie = new HttpCookie(
           FormsAuthentication.FormsCookieName, // Name of auth cookie
           hash); // Hashed ticket

        Response.Cookies.Add(cookie);
    }
    #endregion

    Boolean GetRedirectPageUrl(ref String Url, int UltId, int MeId, int UserId)
    {
        try
        {
            //Return url
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl != null)
            {
                Url = returnUrl;
                return true;
            }
        }
        catch (Exception) { }
        return GetPortalUrl(ref Url, UltId, MeId, UserId);
    }

    Boolean GetPortalUrl(ref String Url, int UltId, int MeId, int UserId)
    {
        Session["PageMode"] = "Show";
        String ReferPage = Request.QueryString["ReferPage"];

        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        TSP.DataManager.LoginManager loginManager = new TSP.DataManager.LoginManager();
        Boolean ReturnValue = true;
        switch (UltId)
        {
            case (int)TSP.DataManager.UserType.Member:

                #region Members

                TSP.DataManager.MemberManager member = new TSP.DataManager.MemberManager();
                member.FindByCode(MeId);
                int MRsId = Convert.ToInt32(member[0]["MrsId"]);
                switch (MRsId)
                {
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed:
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Pending:
                        if (ReferPage != null && ReferPage.Contains("Members/"))
                        {
                            Url = ReferPage;
                        }
                        else
                        {
                            Url = "~/Members/MemberHome.aspx";//?MeId=" + Utility.EncryptQS(MeId.ToString());
                        }
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.NotConfirmed:
                        ShowInputError("عضویت این فرد در سازمان تائید نشده است", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Dead:
                        ShowInputError("این عضو فوت شده است", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince:
                        ShowInputError("این عضو به استان دیگری منتقل شده است", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Fired:
                        ShowInputError("این عضو از سازمان اخراج شده است", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Cancel:
                        ShowInputError("عضویت این فرد در سازمان لغو شده است", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember:
                        Url = "~/EPayment/DebtPayment.aspx?MeId=" + Utility.EncryptQS(MeId.ToString());
                        ReturnValue = false;
                        break;
                }

                #endregion
                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:

                #region TemporatyMembers


                TSP.DataManager.TempMemberManager Tempmember = new TSP.DataManager.TempMemberManager();
                Tempmember.FindByCode(MeId);

                if (ReferPage != null && ReferPage.Contains("Members/"))
                {
                    Url = ReferPage;
                }
                else
                {
                    Url = "~/Members/MemberHome.aspx";//?MeId=" + Utility.EncryptQS(MeId.ToString());
                }

                if (!CheckIsRegDateExpired(Tempmember[0]["CreateDate"].ToString()))
                {
                    if (!CheckIsPaid(MeId))
                    {
                        ReturnValue = false;
                        #region Expired
                        TSP.DataManager.TransactionManager transactionManager = new TSP.DataManager.TransactionManager();
                        TSP.DataManager.WorkFlowStateManager workFlowStateManager = new TSP.DataManager.WorkFlowStateManager(transactionManager);
                        TSP.DataManager.WorkFlowTaskManager workFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                        TSP.DataManager.MemberRequestManager memberRequestManager = new TSP.DataManager.MemberRequestManager();
                        transactionManager.Add(Tempmember);
                        transactionManager.Add(workFlowStateManager);
                        transactionManager.Add(workFlowTaskManager);
                        transactionManager.Add(memberRequestManager);
                        try
                        {

                            int RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectMemberAndEndProcess;
                            workFlowTaskManager.FindByTaskCode(RejectProccessTaskCode);
                            if (workFlowTaskManager.Count == 1)
                            {

                                int RejectProccessTaskId = int.Parse(workFlowTaskManager[0]["TaskId"].ToString());
                                transactionManager.BeginSave();
                                ShowInputError("شما نمی توانید با این نام کاربری وارد شوید.بدلیل به پایان رسیدن مهلت تحویل مدارک به سازمان،هم اکنون وضعیت عضویت شما به عدم تایید تغییر یافت.اکنون قادر به ثبت نام مجدد از طریق لینک''عضویت در سازمان'' می باشید.", true);
                                Tempmember[0].BeginEdit();
                                Tempmember[0]["MsId"] = (int)TSP.DataManager.TemporaryMemberStatus.Canceled;
                                Tempmember[0].EndEdit();
                                Tempmember.Save();

                                memberRequestManager.FindByMemberId((int)Tempmember[0]["TMeId"], 1, 0, 1);
                                if (memberRequestManager.Count == 1)
                                {
                                    memberRequestManager[0].BeginEdit();
                                    memberRequestManager[0]["IsConfirm"] = (int)TSP.DataManager.MemberConfirmType.Cancel;
                                    memberRequestManager[0]["WFCurrentTaskId"] = RejectProccessTaskId;
                                    memberRequestManager[0].EndEdit();
                                    memberRequestManager.Save();

                                    int MReId = int.Parse(memberRequestManager[0]["MReId"].ToString());
                                    int cn = workFlowStateManager.SendDocToNextStep((int)TSP.DataManager.TableCodes.MemberRequest, MReId, RejectProccessTaskId,
                                        "عدم تایید بعلت به پایان رسیدن مهلت تکمیل مدارک", 0,
                                        UserId, (int)TSP.DataManager.WorkFlowStateNmcIdType.System);
                                    if (cn > 0)
                                    {
                                        transactionManager.EndSave();
                                        ReturnValue = false;
                                    }

                                }
                                transactionManager.CancelSave();
                            }
                        }
                        catch (Exception err)
                        {
                            transactionManager.CancelSave();
                            Utility.SaveWebsiteError(err);
                            ShowInputError("خطایی در ورود به سیستم رخ داده است", true);
                        }
                        #endregion}
                    }
                }
                #endregion
                break;
            case (int)TSP.DataManager.UserType.Office:

                #region Office
                TSP.DataManager.OfficeManager Manager = new TSP.DataManager.OfficeManager();
                Manager.FindByCode(MeId);
                switch (Convert.ToInt32(Manager[0]["MrsId"]))
                {
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Pending:
                    case (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed:

                        if (ReferPage != null && ReferPage.Contains("Office/"))
                        {
                            Url = ReferPage;
                        }
                        else
                        {
                            Url = "~/Office/OfficeHome.aspx";
                        }
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.NotConfirmed:
                        ShowInputError("شما مجاز به ورود به سایت نمی باشید.عضویت شما تایید نشده می باشد", true);
                        ReturnValue = false;
                        break;
                    case (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval:
                        ShowInputError("شما مجاز به ورود به سایت نمی باشید.عضویت شما در وضعیت تایید مشروط می باشد", true);
                        ReturnValue = false;
                        break;
                }
                #endregion
                break;
            case (int)TSP.DataManager.UserType.Daftar:
                ShowInputError("شما نمی توانید با این نام کاربری وارد شوید", true);
                ReturnValue = false;
                break;
            case (int)TSP.DataManager.UserType.Employee:

                #region Employee
                TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
                EmployeeManager.FindByCode(int.Parse(MeId.ToString()));
                if (EmployeeManager.Count == 1)
                {
                    if (EmployeeManager[0]["EmpStatus"].ToString() == "1")
                    {
                        ShowInputError("شما نمی توانید با این نام کاربری وارد شوید", true);
                        ReturnValue = false;
                    }
                    else
                    {
                        if (ReferPage != null && ReferPage.Contains("Employee/"))
                        {
                            Url = ReferPage;
                            return true;
                        }
                        else
                        {
                            Url = "~/Employee/EmployeeHome.aspx";
                            return true;
                        }
                    }
                }
                else
                {
                    ShowInputError("شما نمی توانید با این نام کاربری وارد شوید", true);
                    ReturnValue = false;
                }
                #endregion
                break;
            case (int)TSP.DataManager.UserType.Teacher:

                #region Teachers
                TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
                DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(MeId);
                if (dtTeCert.Rows.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(dtTeCert.Rows[0]["IsConfirm"]))
                    {
                        int IsConfirm = int.Parse(dtTeCert.Rows[0]["IsConfirm"].ToString());
                        if (IsConfirm == 1)
                        {
                            if (ReferPage != null && ReferPage.Contains("Teachers/"))
                            {
                                Url = ReferPage;
                            }
                            else
                            {
                                Url = "~/Teachers/TeacherHome.aspx";
                            }
                        }
                        else if (IsConfirm == 2)
                        {
                            ShowInputError("بدلیل عدم تایید پروانه استاد ، شما نمی توانید با این نام کاربری وارد شوید", true);
                            ReturnValue = false;
                        }
                        else
                        {
                            ShowInputError("بدلیل نامشخص بودن وضعیت تایید پروانه استاد ، شما نمی توانید با این نام کاربری وارد شوید", true);
                            return false;
                        }
                    }
                    else
                    {
                        ShowInputError("بدلیل نامشخص بودن وضعیت پروانه استاد ، شما نمی توانید با این نام کاربری وارد شوید", true);
                        ReturnValue = false;
                    }
                }
                else
                {
                    ShowInputError("بدلیل نامشخص بودن وضعیت پروانه استاد ، شما نمی توانید با این نام کاربری وارد شوید", true);
                    ReturnValue = false;
                }
                #endregion
                break;
            case (int)TSP.DataManager.UserType.Institute:
                #region Institue
                if (ReferPage != null && ReferPage.Contains("Institue/"))
                {
                    Url = ReferPage;
                }
                else
                {
                    Url = "~/Institue/Amoozesh/InstitueHome.aspx" ;
                }

                #endregion
                break;
            case (int)TSP.DataManager.UserType.Settlement:
                #region Settlement
                TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
                SettlementAgentManager.FindByCode(MeId);
                if (SettlementAgentManager.Count == 1)
                {
                    if (Convert.ToBoolean(SettlementAgentManager[0]["InActive"]))
                    {
                        ShowInputError("شما نمی توانید با این نام کاربری وارد شوید", true);
                        ReturnValue = false;
                    }
                    else
                    {
                        if (ReferPage != null && ReferPage.Contains("Settlement/"))
                        {
                            Url = ReferPage;
                        }
                        else
                        {
                            Url = "~/Settlement/SettlmentHomePage.aspx";
                        }
                    }
                }

                #endregion
                break;
            case (int)TSP.DataManager.UserType.TSProjectOwner:
                #region TSProjectOwner

                if (ReferPage != null && ReferPage.Contains("Owner/"))
                {
                    Url = ReferPage;
                }
                else
                {
                    Url = "~/Owner/OwnerHome.aspx";
                }

                #endregion
                break;


            case (int)TSP.DataManager.UserType.TSImplementerOffice:
                #region TSImplementerOffice
                if (ReferPage != null && ReferPage.Contains("ImplementerOffice/"))
                {
                    Url = ReferPage;
                }
                else
                {
                    Url = "~/ImplementerOffice/ImplementerOfficeHome.aspx";
                }
                #endregion
                break;
            default:
                ShowInputError("اطلاعات این کاربر در سیستم ثبت نشده است", true);
                ReturnValue = false;
                break;
        }
        return ReturnValue;// false;
    }

    Boolean CheckSecurityCode()
    {
        if (panelSecurityCode.Visible == true)
        {
            return Captcha.IsValid;
            //return TSP.WebControls.CustomASPxCaptcha.Check(Captcha.Code, txtSecurityCode.Text);
        }
        else
            return true;
    }

    #region Save Trace

    Boolean SaveTraceForbiden(String Username, TSP.DataManager.TraceManager.Types Types)
    {
        return SaveTrace(Username, -1, -1, Types);
    }
    Boolean SaveTrace(String Username)
    {
        return SaveTrace(Username, -1, -1, TSP.DataManager.TraceManager.Types.LoginUnsuccessful);
    }

    Boolean SaveTrace(String Username, int UltId, int MeId, TSP.DataManager.TraceManager.Types TraceType)
    {
        return SaveTrace(Username, UltId, MeId, TraceType, "");
    }
    Boolean SaveTrace(String Username, int UltId, int MeId, TSP.DataManager.TraceManager.Types TraceType, String TempPass)
    {
        TSP.DataManager.TraceManager TrManager = new TSP.DataManager.TraceManager();
        return TrManager.SaveTrace(Username, UltId, MeId, TraceType, TempPass);
    }
    #endregion

    int getLockStatus(int Id, int UserType)
    {
        Boolean IsLock = false;
        switch (UserType)
        {
            case (int)TSP.DataManager.UserType.Employee:
                IsLock = false;
                break;
            case (int)TSP.DataManager.UserType.Institute:
                TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
                InstitueManager.FindByCode(Id);
                if (InstitueManager.Count > 0)
                    IsLock = Convert.ToBoolean(InstitueManager[0]["IsLock"]);
                else
                    IsLock = true;
                break;
            case (int)TSP.DataManager.UserType.Member:
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(Id);
                if (MemberManager.Count > 0)
                    IsLock = Convert.ToBoolean(MemberManager[0]["IsLock"]);
                else
                    IsLock = true;
                break;
            case (int)TSP.DataManager.UserType.Municipality:
                IsLock = false;
                break;
            case (int)TSP.DataManager.UserType.Office:
                TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
                OfficeManager.FindByCode(Id);
                if (OfficeManager.Count > 0)
                    IsLock = Convert.ToBoolean(OfficeManager[0]["IsLock"]);
                else
                    IsLock = true;
                break;
            case (int)TSP.DataManager.UserType.Settlement:
                IsLock = false;
                break;
            case (int)TSP.DataManager.UserType.Teacher:
                TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                TeacherManager.FindByCode(Id);
                if (TeacherManager.Count > 0)
                    IsLock = Convert.ToBoolean(TeacherManager[0]["IsLock"]);
                else
                    IsLock = true;
                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:
                IsLock = false;
                break;
        }

        int Lock = (IsLock) ? 1 : 0;
        return Lock;
    }

    void ShowInputError(String Error, Boolean VisiblepanelSecurity)
    {
        lblError.Text = "<img src='Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
        panelSecurityCode.Visible = VisiblepanelSecurity;
        Session["ShowSecurityCode"] = VisiblepanelSecurity;
    }

    private Boolean CheckIsRegDateExpired(string RegDate)
    {
        Utility.Date objDate = new Utility.Date(RegDate);
        string RegExpireDate = objDate.AddDays(Utility.GetMembershipRegTimeout());
        string Today = Utility.GetDateOfToday();
        int IsDocExp = string.Compare(Today, RegExpireDate);
        if (IsDocExp <= 0)
        {
            return true;
        }
        return false;
    }

    private Boolean CheckIsPaid(int MeId)
    {
        TSP.DataManager.TempMemberManager SelectStatusPayment = new TSP.DataManager.TempMemberManager();
        DataTable DTselectStatusPayment = SelectStatusPayment.SelectStatusPaymentForTemporaryMember(MeId);
        if (!Utility.IsDBNullOrNullValue(DTselectStatusPayment.Rows[0]["PaymentStatus"]))
        {
            int Status = Convert.ToInt32(DTselectStatusPayment.Rows[0]["PaymentStatus"].ToString());
            if (Status == 3)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}