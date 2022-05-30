using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml;

/// <summary>
/// Summary description for Utility
/// </summary>
/// 

public partial class Utility
{
    const int MaxFileSize = 150;
    private static System.Security.Cryptography.RijndaelManaged symAlg = new System.Security.Cryptography.RijndaelManaged();
    private static byte[] keys = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    private static byte[] iv = { 40, 80, 70, 213, 154, 178, 21, 102, 24, 78, 62, 34, 98, 145, 102, 36 };

    public enum FollowType
    {
        Entezami, ExpertPlace, AutomationLetters, MemberDocument, ImplementDocument, MemberRequest,
        OfficeRequest, ObservationDocument, EngOffice, TSPlan, TSProjectRequest, Session, EPayment
    }

    public Utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Usefull Methods

    private string ConvByteArrToStr(byte[] b)
    {
        string s = null;
        for (int i = 0; i < b.Length; i++)
            s += Convert.ToChar(b[i]).ToString();
        return s;
    }

    private byte[] ConvStrToByteArr(string s)
    {
        byte[] b = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            b[i] = Convert.ToByte(s[i]);
        }
        return b;
    }

    public static string EncryptQS(string item)
    {
        try
        {
            symAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            symAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
            symAlg.Key = keys;
            symAlg.IV = iv;
            byte[] input = System.Text.Encoding.Unicode.GetBytes(item);   //new byte[unicode.GetByteCount(item)];
            input = symAlg.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(input);
        }
        catch
        {
            return null;
        }
    }

    public static string DecryptQS(string item)
    {
        try
        {
            symAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            symAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
            symAlg.Key = keys;
            symAlg.IV = iv;
            item = item.Replace(" ", "+");
            byte[] input = Convert.FromBase64String(item);
            input = symAlg.CreateDecryptor(keys, iv).TransformFinalBlock(input, 0, input.Length);
            return System.Text.Encoding.Unicode.GetString(input);//unicode.GetString(input,0,input.Length);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Generate password
    /// </summary>
    /// <returns>Generated password</returns>
    public static String GeneratePassword()
    {
        return (new KeyGenerator(8, KeyGenerator.CharacterTypes.SmallLettersAndNumbers)).Generate();
    }

    /// <summary>
    /// Encrypt password - SHA1
    /// </summary>
    /// <param name="Password">Password used for encryption</param>
    /// <returns>Encrypted password</returns>
    public static String EncryptPassword(String Password)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1");
    }

    /// <summary>
    /// Encrypt password - MD5
    /// </summary>
    /// <param name="Password">Password used for encryption</param>
    /// <returns>Encrypted password</returns>
    public static String EncryptPassword2(String Password)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5");
    }

    public static string GenFollowCode(FollowType type)
    {
        //string guid = Guid.NewGuid().ToString();
        //if (type == FollowType.Entezami)
        //    return "100" + guid.Substring(0, 8) + guid[guid.Length - 1];
        //if (type == FollowType.ExpertPlace)
        //    return "200" + guid.Substring(0, 8) + guid[guid.Length - 1];
        //return "";

        String Key = "";
        KeyGenerator objGenerator = new KeyGenerator(KeyGenerator.CharacterTypes.Numbers);
        switch (type)
        {
            case FollowType.AutomationLetters:
                Key = "A" + objGenerator.GenerateWithYear(9);
                break;
            case FollowType.Entezami:
                Key = "B" + objGenerator.Generate(9);
                break;
            case FollowType.ExpertPlace:
                Key = "B" + objGenerator.Generate(9);
                break;
            case FollowType.MemberDocument:
                Key = "D" + objGenerator.Generate(9);
                break;
            case FollowType.ImplementDocument:
                Key = "I" + objGenerator.Generate(9);
                break;
            case FollowType.MemberRequest:
                Key = "M" + objGenerator.Generate(9);
                break;
            case FollowType.OfficeRequest:
                Key = "O" + objGenerator.Generate(9);
                break;
            case FollowType.ObservationDocument:
                Key = "C" + objGenerator.Generate(9);
                break;
            case FollowType.EngOffice:
                Key = "E" + objGenerator.Generate(9);
                break;
            case FollowType.TSPlan:
                Key = "P" + objGenerator.Generate(9);
                break;
            case FollowType.TSProjectRequest:
                Key = "T" + objGenerator.Generate(9);
                break;
            case FollowType.Session:
                Key = "S" + objGenerator.Generate(9);
                break;
            case FollowType.EPayment:
                Key = "E" + objGenerator.Generate(11);
                break;
        }
        return Key;
    }

    public static string GenerateName(string suffix)
    {
        return (System.Guid.NewGuid().ToString() + suffix);
    }

    //public static string GetCurrentTime()
    //{
    //    return string.Format("{0:hh:mm:ss}", DateTime.Now);
    //   //DateTime.Now.ToString("T", CultureInfo.CreateSpecificCulture("hr-HR"));
    //}
    #endregion

    public static System.Drawing.Image GetImage(byte[] buffer)
    {
        System.Drawing.Image bitmap = null;
        try
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer))
            {
                bitmap = new System.Drawing.Bitmap(ms);
            }
        }
        catch
        {
            return null;
        }
        return bitmap;
    }

    public static void SaveImage(string fileName, byte[] buffer)
    {
        using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
        {
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }
    }

    public static byte[] GetFileBytes(string fileName)
    {
        if (!System.IO.File.Exists(fileName))
            return null;
        try
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            fs.Dispose();
            return buffer;
        }
        catch
        {
            return null;
        }
    }

    public static Boolean CheckEmailFormat(String Email)
    {
        /*string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
    (com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv||ir|[a-zA-Z]{2})$";
        string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
        bool valid = false;

        if (string.IsNullOrEmpty(Email))
        {
            valid = false;
        }
        else
        {
            valid = check.IsMatch(Email);
        }
        return valid;*/

        string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
        Match match = Regex.Match(Email.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Check if the time of Request for the page has been finish. 
    /// Return True if Time out,Otherwise return False
    /// </summary>
    /// <param name="PageRequestTime">Time of Page Request(in FileTime(long) Format)</param>
    /// <returns>Return True if Time out,Otherwise return False</returns>
    public static Boolean IsPageRequestTimeOut(long RequestTime)
    {
        try
        {
            Double TimeOut = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["QueryTimeOut"]);
            return IsPageRequestTimeOut(RequestTime, TimeOut);
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        return true;
    }

    /// <summary>
    /// Check if the time of Request for the page has been finish. 
    /// Return True if Time out,Otherwise return False
    /// </summary>
    /// <param name="PageRequestTime">Time of Page Request(in FileTime(long) Format)</param>
    /// <param name="TimeOut">Time out in minute</param>
    /// <returns>Return True if Time out,Otherwise return False</returns>
    public static Boolean IsPageRequestTimeOut(long RequestTime, Double TimeOut)
    {
        try
        {
            DateTime PageRequestTime = DateTime.FromFileTime(RequestTime);
            TimeSpan ts = DateTime.Now.Subtract(PageRequestTime);
            if (ts.TotalMinutes > TimeOut)
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        return true;
    }

    #region Email
    /// <summary>
    /// Get Link timout
    /// For links that would send to user like Forgot password
    /// </summary>
    /// <returns></returns>
    public static int GetEmailLinkTimeOut()
    {
        try
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailLinkTimeOut"]);
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        return 0;
    }

    /// <summary>
    /// Check if the time of Request for the Email link has been finish. 
    /// Return True if Time out,Otherwise return False
    /// </summary>
    /// <param name="PageRequestTime">Time of Page Request(in FileTime(long) Format)</param>
    /// <returns>Return True if Time out,Otherwise return False</returns>
    public static Boolean IsEmailLinkTimeOut(long RequestTime)
    {
        try
        {
            Double TimeOut = Double.Parse(GetEmailLinkTimeOut().ToString());
            DateTime dtRequestTime = DateTime.FromFileTime(RequestTime);
            TimeSpan ts = DateTime.Now.Subtract(dtRequestTime);
            if (ts.TotalMinutes > TimeOut)
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        return true;
    }

    public static string GetEmailPassword()
    {
        return "snmf#987654321";
    }

    public static string GetEmail()
    {
        return "fceo@gmx.com";
    }

    public static string GetEmailPasswordFceo()
    {
        return "Zaq12wsx@";
    }

    public static string GetEmailFceo()
    {
        return "eservice@fceo.com";
    }

    /// <summary>
    /// Send Email to user use SMTP
    /// </summary>
    /// <param name="RecieverEmail"></param>
    /// <param name="EmailSubject"></param>
    /// <param name="EmailBody"></param>
    /// <param name="IsBodyHtml"></param>
    /// <returns></returns>
    public static Boolean SendEmailToUser(String RecieverEmail, String EmailSubject, String EmailBody, Boolean IsBodyHtml)
    {
        Boolean State = true;
        try
        {
            String Email_ID = GetEmail();// System.Configuration.ConfigurationManager.AppSettings["Email_ID"];
            String Email_Pass = GetEmailPassword();// System.Configuration.ConfigurationManager.AppSettings["Email_Pass"];
            String Email_DisplayName = System.Configuration.ConfigurationManager.AppSettings["Email_DisplayName"];
            String SMPT_Name = System.Configuration.ConfigurationManager.AppSettings["SMPT_Name"];
            int SMPT_UseDefaultCredentials = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_UseDefaultCredentials"]);
            int SMPT_EnableSsl = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_EnableSsl"]);
            int SMPT_Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_Port"]);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(RecieverEmail);
            mail.Subject = EmailSubject;
            mail.From = new System.Net.Mail.MailAddress(Email_ID, Email_DisplayName);
            mail.IsBodyHtml = IsBodyHtml;
            mail.Body = GetEmailHeader(EmailSubject);
            mail.Body += "<div dir=rtl align=right style='border: double Medium gray; font-family:Tahoma; font-size:10pt; padding:5px 5px 5px 5px' width='100%'>" + EmailBody + "</div>";
            mail.Body += GetEmailFooter();

            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(Email_ID, Email_Pass);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SMPT_Name);
            smtp.UseDefaultCredentials = (SMPT_UseDefaultCredentials == 0) ? false : true;
            smtp.EnableSsl = (SMPT_EnableSsl == 0) ? false : true;
            smtp.Credentials = cred;
            if (SMPT_Port != -1)
            {
                smtp.Port = SMPT_Port;
            }

            smtp.Send(mail);
        }
        catch
        {
            State = false;
        }
        return State;
    }

    private static String GetEmailHeader(String Subject)
    {
        string Header = "<div dir=rtl align=right style='background-color: gainsboro; border: double Medium gray; font-family:Tahoma; font-size:10pt; padding:5px 5px 5px 5px' width='100%'>";
        Header += "<table width='100%'><tr>";
        Header += "<td width='85%'><table width='100%'><tr>";
        Header += "<td width='50px'>تاریخ :</td>";
        Header += "<td><span dir=ltr>" + Utility.GetDateOfToday() + "</span>   " + Utility.GetCurrentTime() + "</td></tr>";
        Header += "<tr><td colspan=2><br></td></tr>";
        Header += "<tr><td>عنوان :</td>";
        Header += "<td><b>" + Subject + "</b></td></tr></table>";
        Header += "</td><td width='15%'>";

        //String Arm = "";
        //TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();
        //NezamManager.Fill();
        //if (NezamManager.Count > 0)
        //{
        //    Arm = NezamManager[NezamManager.Count - 1]["SignUrl"].ToString();
        //}

        //Header += (String.IsNullOrEmpty(Arm.Trim())) ? "" : "<img src='" + GetWebSiteAddress() + Arm.Replace("~", "") + "' />";// width='64' height='48' />";
        Header += "<img src='" + GetWebSiteAddress() + "/Images/arm_email.png" + "' />";// width='80' height='76s' />";
        Header += "</td></tr></table></div>";
        return Header + "<br>";
    }

    private static String GetEmailFooter()
    {
        String Footer = "<br><div dir=rtl align=right style='background-color: gainsboro; border: double Medium gray; font-family:Tahoma; font-size:10pt; padding:5px 5px 5px 5px; line-height: 15pt;' width='100%'>";

        TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();
        NezamManager.Fill();
        if (NezamManager.Count > 0)
        {
            int index = NezamManager.Count - 1;
            if (String.IsNullOrEmpty(NezamManager[index]["Address"].ToString()) == false)
                Footer += "آدرس : " + NezamManager[index]["Address"];
            if (String.IsNullOrEmpty(NezamManager[index]["Tel1"].ToString()) == false)
            {
                Footer += "<br>" + "تلفن : " + NezamManager[index]["Tel1"];
                if (String.IsNullOrEmpty(NezamManager[index]["Tel2"].ToString()) == false)
                    Footer += " - " + NezamManager[index]["Tel2"];
            }
            if (String.IsNullOrEmpty(NezamManager[index]["Fax"].ToString()) == false)
                Footer += "<br>" + "فاکس : " + NezamManager[index]["Fax"];
            if (String.IsNullOrEmpty(NezamManager[index]["Email"].ToString()) == false)
                Footer += "<br>" + "پست الکترونیکی : " + "<a href='mailto:" + NezamManager[index]["Email"] + "'>" + NezamManager[index]["Email"] + "</a>";
        }

        Footer += "</div>";
        return Footer;
    }
    #endregion

    #region Get CurrentDateTime Info
    /// <summary>
    /// Get Persian Date of Today in format: YYYY/MM/DD
    /// </summary>
    /// <returns></returns>
    public static string GetDateOfToday()
    {
        System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
        String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
        return PersianDate;
    }

    /// <summary>
    /// Get Persian Year of Today in format: YYYY
    /// </summary>
    /// <returns></returns>
    public static string GetYearOfToday()
    {
        System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
        String PersianDate = PDate.GetYear(DateTime.Today).ToString();
        return PersianDate;
    }

    /// <summary>
    /// Get Current time in format: HH:MM
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentTime()
    {
        return string.Format("{0:hh:mm:ss}", DateTime.Now);
        //DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
    }
    #endregion

    #region Get Info from App.Config
    /// <summary>
    /// Shows we have project or not
    /// </summary>
    /// <returns></returns>
    public static Boolean ShowProjectAccount()
    {
        try
        {
            int state = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ShowProject"]);
            if (state == 1)
                return true;
        }
        catch (Exception) { }
        return false;
    }

    public static Boolean ShowCostCenterAccount()
    {
        try
        {
            int state = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ShowCostCenter"]);
            if (state == 1)
                return true;
        }
        catch (Exception) { }
        return false;
    }

    public static string GenRandomNum()
    {
        Random ran = new Random();
        int n = ran.Next(1000000, 9999999);
        return n.ToString();
    }

    #region SMS Methods
    /// <summary>
    /// Return info in array: array[0]=SMSUsername , array[1]=SMSPassWord , array[2]=SMSNumber
    /// </summary>
    /// <returns></returns>
    public static string[] GetSMSWebServiceInformation()
    {
        string[] SMSInfo = new string[3];
        try
        {
            SMSInfo[0] = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
            SMSInfo[1] = "8275064"; //System.Configuration.ConfigurationManager.AppSettings["SMSPassWord"];
            SMSInfo[2] = System.Configuration.ConfigurationManager.AppSettings["SMSNumber"];
        }
        catch (Exception) { }
        return SMSInfo;
    }

    public static string[] GetMagfaWebServiceInformation()
    {
        string[] SMSInfo = new string[4];
        try
        {
            SMSInfo[0] = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSUserName"];
            SMSInfo[1] = "SnjOAG7G0HDaqXRd";/* "WSVd0bzY8XA#EjDl";*/ //System.Configuration.ConfigurationManager.AppSettings["MagfaSMSPassWord"];
            SMSInfo[2] = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSNumber"];
            SMSInfo[3] = System.Configuration.ConfigurationManager.AppSettings["MagfaSMSDomain"];
        }
        catch (Exception) { }
        return SMSInfo;
    }

    public static string[] GetPrdcoWebServiceInformation()
    {
        string[] SMSInfo = new string[4];
        try
        {
            SMSInfo[0] = System.Configuration.ConfigurationManager.AppSettings["PrdcoSMSUserName"];
            SMSInfo[1] = "fceo.ir@147"; //System.Configuration.ConfigurationManager.AppSettings["PrdcoSMSPassWord"];
            SMSInfo[2] = System.Configuration.ConfigurationManager.AppSettings["PrdcoSMSNumber"];
        
        }
        catch (Exception) { }
        return SMSInfo;
    }
    public static int GetSMSPacketSize()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMSPacketSize"]);
        }
        catch (Exception) { }
        return -1;
    }

    public static int GetMagfaSMSPacketSize()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MagfaSMSPacketSize"]);
        }
        catch (Exception) { }
        return -1;
    }

    public static int GetPrdcoSMSPacketSize()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PrdcoSMSPacketSize"]);
        }
        catch (Exception) { }
        return -1;
    }
    public static int GetSMSThreadSleepTime()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMSThreadSleepTime"]);
        }
        catch (Exception) { }
        return 500;
    }

    /// <summary>
    /// نتیجه ارسال پیامک، درصورتیکه خروجی این متد خالی باشد، پیامک ارسال شده در غیر اینصورت خطای آن را مشخص می کند
    /// </summary>
    /// <param name="Result">نتیجه پیامک ارسال شده به سرور</param>
    /// <returns></returns>
    public static String GetAFESmsResult(String Result)
    {
        String Message = "";
        switch (Result)
        {
            case "Mobile Number is Empty":
                Message = "پارامتر شماره موبایل خالی است و حاوی هیچ مقداری نمی باشد.";
                break;

            case "Virtual Number is Empty":
                Message = "پارامتر شماره اختصاصی خالی است و حاوی هیچ مقداری نمی باشد.";
                break;

            case "Message Body is Invalid":
                Message = "متن پیام کوتاه معتبر نمی باشد.";
                break;

            case "Message Type is Invalid":
                Message = "مقدار پارامتر Message Typeمعتبر نمی باشد.";
                break;

            case "Message is UnKnow":
                Message = "پیام معتبر نیست.";
                break;

            case "Mobile Array is Empty":
                Message = "لیست شماره موبایل ها خالی است.";
                break;


            case "Message is too Long":
                Message = ".متن پیام طولانی تر از حد مجاز است";
                break;

            case "User Not Enable":
                Message = "نام کاربری غیر فعال است.";
                break;

            case "No Credit":
                Message = "اعتبار کافی برای ارسال پیامک وجود ندارد";
                break;

            case "Quota Full":
                Message = "محدودیت مصرف روزانه شما به اتمام رسیده است.";
                break;

            case "Wrong Number":
                Message = "شماره اختصاصی اشتباه است.";
                break;

            case "Username or Password Wrong":
                Message = "نام کاربری یا رمز عبور اشتباه است.";
                break;

            case "Username or Password is Null":
                Message = "نام کاربری یا رمز عبور نامشخص می باشد.";
                break;
        }
        return Message;
    }

    public static String GetMagfaSmsResult(long Result)
    {
        String Message = "";
        switch (Result)
        {
            case 1:
                Message = "شماره گیرنده نامعتبر است";
                break;

            case 2:
                Message = "شماره فرستنده نامعتبر است";
                break;

            case 3:
                Message = "پارامتر رمزگذاری نامعتبر است";
                break;

            case 4:
                Message = "پارامتر نوع پیام نامعتبر است";
                break;

            case 6:
                Message = "پارامتر UDH نامعتبر است";
                break;

            case 10:
                Message = "پارامتر اولویت بندی نامعتبر است";
                break;

            case 13:
                Message = ".متن پیام خالی است";
                break;

            case 14:
                Message = "حساب اعتبار ریالی مورد نیاز را دارا نمی باشد";
                break;

            case 15:
                Message = "سرور در هنگام ارسال پیام مشغول برطرف نمودن ایراد داخلی بوده است. پیام ها دوباره ارسال شوند";
                break;

            case 16:
                Message = "حساب غیرفعال می باشد";
                break;

            case 17:
                Message = "حساب منقضی شده است";
                break;

            case 18:
                Message = "نام کاربری یا رمز عبور نامعتبر است.";
                break;

            case 19:
                Message = "درخواست معتبر نمی باشد. نام کاربری یا رمز عبور یا نام دامنه اشتباه می باشد";
                break;

            case 20:
                Message = "شماره اختصاصی با نام کاربر حساب تطبیق ندارد";
                break;

            case 22:
                Message = "نوع سرویس درخواستی نامعتبر است";
                break;

            case 23:
                Message = "به دلیل ترافیک بالا، سرور آمادگی دریافت پیام جدید را ندارد دوباره سعی کنید";
                break;

            case 24:
                Message = "شناسه پیامک معتبر نمی باشد یا بیش از یک روز از ارسال آن گذشته است";
                break;

            case 25:
                Message = "نام سرویس درخواستی معتبر نمی باشد";
                break;

            case 27:
                Message = "شماره گیرنده در لیست غیرفعال شرکت همراه اول قرار دارد ارسال پیامک های تبلیغاتی برای این شماره امکانپذیر نیست";
                break;

            case 101:
                Message = "تعداد متن های پیام با تعداد گیرندگان تطابق ندارد";
                break;

            case 102:
                Message = "تعداد نوع پیام با تعداد گیرندگان تطابق ندارد";
                break;

            case 103:
                Message = "تعداد ارسال کنندگان با تعداد دریافت کنندگان تطابق ندارد";
                break;

            case 104:
                Message = "تعداد پارامتر UDH با تعداد گیرندگان تطابق ندارد";
                break;

            case 105:
                Message = "تعداد پارامتر اولویت بندی با تعداد گیرندگان تطابق ندارد";
                break;

            case 106:
                Message = "لیست گیرندگان خالی می باشد";
                break;

            case 107:
                Message = "تعداد لیست گیرندگان بیشتر از طول مجاز است";
                break;

            case 108:
                Message = "لیست فرستندگان خالی می باشد";
                break;

            case 109:
                Message = "تعداد پارامتر اولویت بندی با تعداد گیرندگان تطابق ندارد";
                break;

            case 110:
                Message = "تعداد پارامتر CheckingMessageId با تعداد گیرندگان تطابق ندارد";
                break;

            default:
                Message = "خطایی در ارتباط با سرویس دهنده پیامک به وجود آمده است";
                break;
        }
        return Message;
    }
    #endregion
   
    public static Boolean IsZeroInvoiceCheck()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"] == null)
            return true;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"]));
    }

    public static Boolean ShowExceptionError()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["ShowExceptionError"] == null)
            return false;
        else
            return System.Configuration.ConfigurationManager.AppSettings["ShowExceptionError"] == "1";
    }

    public static int GetLetterActionDuration()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["LetterActionDuration"] == null)
            return 1;
        else
            return (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["LetterActionDuration"]));
    }

    public static int GetWFExpireDateDuration()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["WFExpireDateDuration"] == null)
            return 1;
        else
            return (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WFExpireDateDuration"]));
    }

    public static Boolean IsAmoozeshConditionChecked()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["IdAmoozeshConditionsChecked"] == null)
            return true;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IdAmoozeshConditionsChecked"]));
    }

    public static Boolean IsMemberSearchAccessible()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["IsMemberSearchAccessible"] == null)
            return true;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsMemberSearchAccessible"]));
    }

  
    /// <summary>
    /// Check whether uploaded images,Save in DataBase or not.
    /// </summary>
    /// <returns></returns>
    public static Boolean SavePicInDataBase()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SavePicInDataBase"]);
            return State == 1;
        }
        catch (Exception) { }
        return false;
    }

    public static Boolean ShowAccountsForGoods()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowAccountsForGoods"]);
            return State == 1;
        }
        catch (Exception) { }
        return false;
    }

    public static int GetDefaultNewsExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultNewsExpireDate"]);
        }
        catch (Exception) { }
        return 3;
    }

    public static String GetWebSiteAddress()
    {
        try
        {
            return System.Configuration.ConfigurationManager.AppSettings["NezamFarsWebsiteAddress"];
        }
        catch (Exception) { }
        return "";
    }

    public static Boolean WebsiteIsNotAccessible()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["WebsiteIsNotAccessible"]);
            return State == 1;
        }
        catch (Exception) { }
        return false;
    }

    public static Boolean ShowAccountsForStorage()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowAccountsForGoods"]);
            return State == 0;
        }
        catch (Exception) { }
        return false;
    }

    public static int GetLoginCookieTimeOut()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LoginCookieTimeOut"]);
            return State;
        }
        catch (Exception) { }
        return 30; // Default value
    }

    public static int GetNoOfSMSPacketSendBeforeThreadSleep()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NoOfSMSPacketSendBeforeThreadSleep"]);
        }
        catch (Exception) { }
        return 1000;
    }

    public static int GetDefaultPermanentMemberDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPermanentMemberDocExpireDate"]);
        }
        catch (Exception) { }
        return 3;
    }

    public static int GetDefaultTemporaryMemberDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultTemporaryMemberDocExpireDate"]);
        }
        catch (Exception) { }
        return 1;
    }

    public static int GetDefaultPermanentImplementerDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPermanentImplementerDocExpireDate"]);
        }
        catch (Exception) { }
        return 3;
    }

    public static int GetDefaultTemporaryImplementerDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultTemporaryImplementerDocExpireDate"]);
        }
        catch (Exception) { }
        return 1;
    }

    public static int GetDefaultPermanentObserverDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPermanentObserverDocExpireDate"]);
        }
        catch (Exception) { }
        return 3;
    }

    public static int GetDefaultTemporaryObserverDocExpireDate()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultTemporaryObserverDocExpireDate"]);
        }
        catch (Exception) { }
        return 1;
    }
    /// <summary>
    /// It retutns Shiraz AgentId Not AgentCod
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentAgentCode()
    {
        try
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CurrentAgentCode"]);
        }
        catch (Exception) { }
        return 1;
    }

    /// <summary>
    /// وضعیت چک کردن مدت زمان اعتبار مدرک پروانه اشتغال به کار اگر 0 بود اخطار دهد اگر یک بود خطا دهد
    /// </summary>
    /// <returns></returns>
    public static Boolean IsJobDurationNeedForMeFileChecked()
    {
        try
        {
            int result = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IsJobDurationNeedForMeFileChecked"]);
            return Convert.ToBoolean(result);
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// وضعیت چک کردن شرایط کمسیون هم ارزی رشته ها برای مدرک پروانه اشتغال به کار
    /// </summary>
    /// <returns></returns>
    public static Boolean IsDocMemberFileMajorCheckInComission()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsDocMemberFileMajorCheckInComission"]);
            return Convert.ToBoolean(State);
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    public static Boolean IsEngOfficeMemberConfirmRequestNeeded()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsEngOfficeMemberConfirmRequestNeeded"]);
            return Convert.ToBoolean(State);
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    public static Boolean IsOfficeMemberConfirmRequestNeeded()
    {
        try
        {
            int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsOfficeMemberConfirmRequestNeeded"]);
            return Convert.ToBoolean(State);
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    public static int EntezamiReplySaveTime()
    {
        try
        {
            int result = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EntezamiReplySaveTime"]);
            return Convert.ToInt32(result);
        }
        catch (Exception)
        {
            return -1;
        }
        return -1;
    }

    public static int EntezamiRivisionSaveTime()
    {
        try
        {
            int result = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EntezamiRivisionSaveTime"]);
            return Convert.ToInt32(result);
        }
        catch (Exception)
        {
            return -1;
        }
        return -1;
    }

    public static int GetCacheDuration()
    {
        int Duration = 3600;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CacheDuration"]))
            Duration = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CacheDuration"]);
        return Duration;
    }

    public static Boolean CheckMunPermission()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["CheckMunPermission"] == null)
            return false;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CheckMunPermission"]));
    }

    public static int GetCurrentCounId()
    {
        int CounId = 3;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentCounId"]))
            CounId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentCounId"]);

        return CounId;
    }

    public static int GetCurrentCitId()
    {
        int CitId = 1;

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentCitId"]))
            CitId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentCitId"]);

        return CitId;
    }

    public static string GetAccountSepratorChar()
    {
        string seprator = "-";

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AccountSepratorChar"]))
            seprator = (System.Configuration.ConfigurationManager.AppSettings["AccountSepratorChar"].ToString());

        return seprator;
    }
    public static Boolean CreateAccount()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["CreateAccount"] == null)
            return false;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CreateAccount"]));
    }
    /// <summary>
    /// وب سرویس جاری برای ارسال پیام کوتاه را مشخص می کند
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentSMSWebService()
    {
        int WebService = 1;

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentSMSWebService"]))
            WebService = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentSMSWebService"]);

        return WebService;
    }

    public static int GetMembershipRegTimeout()
    {
        int MembershipRegTimeout = 10;

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MembershipRegTimeout"]))
            MembershipRegTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MembershipRegTimeout"]);

        return MembershipRegTimeout;
    }

    public static string GetAccountingOwnerName()
    {
        string AccountingOwnerName = "";

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AccountingOwnerName"]))
            AccountingOwnerName = System.Configuration.ConfigurationManager.AppSettings["AccountingOwnerName"].ToString();

        return AccountingOwnerName;
    }

    public static string GetAccountingNumber()
    {
        string AccountingNumber = "";

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AccountingNumber"]))
            AccountingNumber = System.Configuration.ConfigurationManager.AppSettings["AccountingNumber"].ToString();

        return AccountingNumber;
    }

    public static int GetLicenceInqueryRequestAsignerId()
    {
        int GmtId = 2;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["LicenceInqueryRequestAsigner"]))
            GmtId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LicenceInqueryRequestAsigner"]);
        return GmtId;
    }

    #region TS
    public static Boolean TSProject_IsBasedOnStep()
    {
        bool IsBasedOnStep = false;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["TSProjectIsBasedOnStep"]))
            IsBasedOnStep = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["TSProjectIsBasedOnStep"]));
        return IsBasedOnStep;
    }

    public static Boolean IsObserverDocExpireDateChecked()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["IsObserverDocExpireDateChecked"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["IsObserverDocExpireDateChecked"]));
        return IsObserverDocExpireDateChecked;
    }

    public static Boolean IsWorkRequestOtheAgentOpen()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["Openo"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["Openo"]));
        return IsObserverDocExpireDateChecked;
    }
    public static Boolean IsWorkRequestMainAgent()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["Openm"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["Openm"]));
        return IsObserverDocExpireDateChecked;
    }
    public static Boolean IsesupTestServerUse()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["esupTest"] == null)
            return true;
        else
            return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["esupTest"]));
    }
    public static int GettestMemberId()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["mtest"] == null)
            return -2;
        else
            return (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["mtest"]));
    }

    public static Boolean IsSmsOff()
    {
        bool IsSmsOff = false;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["MsgOff"]))
            IsSmsOff = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["MsgOff"]));
        return IsSmsOff;
    }

    public static Boolean IsSmsToProjectOwnerOff()
    {
        bool IsSmsOff = false;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["MsgOwnerOff"]))
            IsSmsOff = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["MsgOwnerOff"]));
        return IsSmsOff;
    }
    public static Boolean IsWorkREquestInfoSendToShahrdari()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["sendesup"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["sendesup"]));
        return IsObserverDocExpireDateChecked;
    }

    public static string TSQueueOpenDate()
    {
        string date = "";
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["DateO"]))
            date = (System.Configuration.ConfigurationManager.AppSettings["DateO"]).ToString();
        return date;
    }
    public static Boolean IsCapacityBasedOnWorkRequestOtherAgentOpen()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["CapWreqOth"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CapWreqOth"]));
        return IsObserverDocExpireDateChecked;
    }
    public static Boolean IsCapacityBasedOnWorkRequestMainAgent()
    {
        bool IsObserverDocExpireDateChecked = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["CapWreqM"]))
            IsObserverDocExpireDateChecked = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CapWreqM"]));
        return IsObserverDocExpireDateChecked;
    }
    
    public static Boolean IsMaxJobCapacityCheched()
    {
        bool IsMaxJobCapacityCheched = true;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["IsMaxJobCapacityCheched"]))
            IsMaxJobCapacityCheched = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["IsMaxJobCapacityCheched"]));
        return IsMaxJobCapacityCheched;
    }


    public static int ProjectMaxFoundationForDesignerWithOutDocumentQualification()
    {
        int ProjectMaxFoundationForDesignerWithOutDocumentQualification = 700;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["ProjectMaxFoundationForDesignerWithOutDocumentQualification"]))
            ProjectMaxFoundationForDesignerWithOutDocumentQualification = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ProjectMaxFoundationForDesignerWithOutDocumentQualification"]);
        return ProjectMaxFoundationForDesignerWithOutDocumentQualification;
    }
    #endregion
    public static Boolean IsEPaymentTest()
    {
        bool IsEPaymentTest = false;
        if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["EPaymentTest"]))
            IsEPaymentTest = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["EPaymentTest"]));
        return IsEPaymentTest;
    }

    public static string GetAdminConfirm()
    {
        string AdminConfirm = "";

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceValueAdId"]))
            AdminConfirm = System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceValueAdId"].ToString();

        return AdminConfirm;
    }

    public static string GetUrgenAdminConfirm()
    {
        return "";
    }
    #endregion

    #region Image's Size
    private static int _MenuImgSize = 10;

    public static int MenuImgSize
    {
        get { return _MenuImgSize; }

    }
    private static int _dpi = 300;

    public static int dpi
    {
        get { return _dpi; }

    }

    private static int _VerRes = 920;

    public static int VerRes
    {
        get { return _VerRes; }

    }
    private static int _HorRes = 720;

    public static int HorRes
    {
        get { return _HorRes; }

    }
    public static int GetSSN_HorRes()
    {
        return 315;

    }
    public static int GetSSN_VerRes()
    {
        return 215;

    }
    public static int GetIdNo_HorRes()
    {
        return 600;

    }
    public static int GetIdNo_VerRes()
    {
        return 400;

    }
    public static int GetSoldierCard_HorRes()
    {
        return 315;

    }
    public static int GetSoldierCard_VerRes()
    {
        return 215;

    }
    public static int GetOfficeSign_HorRes()
    {
        return 113;

    }
    public static int GetOfficeSign_VerRes()
    {
        return 113;

    }

    public static int GetMeSign_HorRes()
    {
        return 591;

    }
    public static int GetMeSign_VerRes()
    {
        return 472;

    }

    public static void FixedSize(String InputFileName, String SaveFileName, int Width, int Height)
    {
        System.Drawing.Image imgInput = Utility.GetImage(Utility.GetFileBytes(InputFileName));
        System.Drawing.Graphics gInput = System.Drawing.Graphics.FromImage(imgInput);
        System.Drawing.Imaging.ImageFormat ImageFormat = imgInput.RawFormat;

        System.Drawing.Image imgPhoto = ScaleImageToFixedSize(imgInput, Width, Height);
        imgPhoto.Save(SaveFileName, ImageFormat);
        imgPhoto.Dispose();
    }

    private static System.Drawing.Image ScaleImageToFixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }
    #endregion

    /*
    public static void FixedSize(string InputFileName, string SaveFileName, int HorRes, int VerRes)
    {
        int Width = HorRes;
        int Height = VerRes;

        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(InputFileName);
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                      System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
             System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        bmPhoto.Save(SaveFileName);
        bmPhoto.Dispose();
    }
    */
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static Boolean IsDBNullOrNullValue(object obj)
    {
        if (obj == DBNull.Value || obj == null)
            return true;
        if (string.IsNullOrEmpty(obj.ToString()))
            return true;
        return false;
    }

    public static string ConvertDBNullToString(object obj)
    {
        if (obj == DBNull.Value || obj == null)
            return "";
        else
            return obj.ToString();
    }

    /// <summary>
    /// Check if User is Administratore of site
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public static Boolean IsUserAdministratore(int UserId)
    {
        try
        {
            String[] Id = System.Configuration.ConfigurationManager.AppSettings["AdminUserId"].Split(',');
            for (int i = 0; i < Id.Length; i++)
                if (int.Parse(Id[i]) == UserId)
                    return true;
        }
        catch (Exception) { }
        return false;
    }

    #region CurrentUserInfo
    private static String[] GetCurrentUserInfo()
    {
        try
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
                        return ticket.UserData.Split(',');
                    }
                }
            }
        }
        catch (Exception) { }
        return null;
    }

    /// <summary>
    /// Get LoginType(UltId) of current user, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_LoginType()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[0];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return -1;
    }

    /// <summary>
    /// Get NmcId of current EMpoyee Nezam member Chart, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_NezamChartId()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[5];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return -1;
    }


    /// <summary>
    /// Get NmcId of current Employee NmcIdType, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_NmcIdType()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[6];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return -1;
    }

    /// <summary>
    /// Get FullName of current User
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentUser_FullName()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return "";

        try
        {
            String FullName = GetCurrentUserInfo()[4];
            if (String.IsNullOrEmpty(FullName))
                return "";
            else
                return FullName;
        }
        catch (Exception) { }
        return "";
    }


    /// <summary>
    /// Get UserId of current user, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_UserId()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[1];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return -1;
    }

    /// <summary>
    /// Get MeId of current user, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_MeId()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[2];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return -1;
    }

    /// <summary>
    /// Get AgentId of current user, Return -1 if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentUser_AgentId()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return -1;

        try
        {
            String Id = GetCurrentUserInfo()[3];
            if (String.IsNullOrEmpty(Id))
                return -1;
            else
                return int.Parse(Id);
        }
        catch (Exception) { }
        return 1;
    }

    /// <summary>
    /// Get Lock status of current user
    /// </summary>
    /// <returns></returns>
    public static Boolean GetCurrentUser_IsLock()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return true;

        try
        {
            String Lock = GetCurrentUserInfo()[7];
            if (String.IsNullOrEmpty(Lock))
                return true;
            else
                return int.Parse(Lock) == 1;
        }
        catch (Exception) { }
        return true;
    }

    /// <summary>
    /// Check if TspAdmin has login
    /// </summary>
    /// <returns></returns>
    public static Boolean GetCurrentUser_IsTspAdmin()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return false;

        try
        {
            String IsTspAdmin = GetCurrentUserInfo()[8];
            if (String.IsNullOrEmpty(IsTspAdmin))
                return false;
            else
                return int.Parse(IsTspAdmin) == 1;
        }
        catch (Exception) { }
        return false;
    }
    /// <summary>
    /// Get FullName of current User
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentUser_ImageUrl()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            return "";

        try
        {
            String Image = GetCurrentUserInfo()[9];
            if (String.IsNullOrEmpty(Image))
                return "";
            else
                return Image;
        }
        catch (Exception) { }
        return "";
    }

    public static int GetMainAgentId()
    {
        return 1;
    }

    public static int GetMainSecretariat()
    {
        TSP.DataManager.Automation.SecretariatManager SecretariatManager = new TSP.DataManager.Automation.SecretariatManager();
        SecretariatManager.GetMainSecretariat();
        if (SecretariatManager.Count > 0)
            return Convert.ToInt32(SecretariatManager[0]["SId"]);
        else
            return -1;
    }

    /// <summary>
    /// Get Username(LoginName) of current user, Return null if the user is not authenticated
    /// </summary>
    /// <returns></returns>
    public static String GetCurrentUser_Username()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == true)
            return HttpContext.Current.User.Identity.Name;
        else
            return null;
    }
    #endregion

    #region ConvertFarsi
    internal static string Read12(string s)
    {
        string[] digit1 = { "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        string[] digit2 = { "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] digit3 = { "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        if (s.Length == 3 || s.Length == 2)
            if (s[0] == '0')
                s = int.Parse(s).ToString();
        if (s.Length == 0 || (s.Length == 1 && s[0] == '0'))
            return "";
        if (s.Length == 1)
            return digit1[int.Parse(s) - 1];
        if (s.Length == 2)
        {
            if (int.Parse(s[0].ToString()) == 1 && int.Parse(s[1].ToString()) > 0)
                return digit2[int.Parse(s[1].ToString()) - 1];
            if (int.Parse(s[1].ToString()) == 0)
                return digit3[int.Parse(s[0].ToString()) - 1];
            return (digit3[int.Parse(s[0].ToString()) - 1] + " و " + digit1[int.Parse(s[1].ToString()) - 1]);
        }
        return "";
    }
    internal static string ReadStr(string s)
    {
        if (s.Length == 3 || s.Length == 2)
            if (s[0] == '0')
                s = int.Parse(s).ToString();
        string[] digit4 = { "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        /*   if (s.Length == 0 || (s.Length == 1 && s[0] == '0'))
               return "";
           if (s.Length == 1)
               return digit1[int.Parse(s) - 1];
           if (s.Length == 2)
           {
               if(int.Parse(s[0].ToString()==1) &&int.Parse(s[1].ToString()>0))
                   return digit2[int.Parse(s[1].ToString())-1];
                if(int.Parse(s[1].ToString()==0))
                    return digit3[int.Parse(s[0].ToString()) - 1];
                return (digit1[int.Parse(s[1]) - 1] + "و" + digit3[int.Parse(s[0].ToString()) - 1]);
           }*/
        if (s.Length == 3)
        {
            string s3 = digit4[int.Parse(s[0].ToString()) - 1];
            string s2 = Read12(s.Substring(1));
            if (s2 != "")
                return s3 + " و " + s2;
            return s3;
        }
        if (s.Length == 1 || s.Length == 2)
            return Read12(s);
        return "";
    }
    public static string ConvertPriceToAlphba(string number)
    {
        number = number.Replace(",", "");
        number = number.Trim();
        System.Collections.ArrayList ar = new System.Collections.ArrayList();
        string s = number;
        string retS = "";
        string[] pasvand = { "", "هزار", "میلیون", "میلیارد" };
        int cnt = s.Length / 3;
        if (s.Length % 3 != 0)
            cnt++;
        string temp = "";
        for (int i = 0; i < cnt; i++)
        {
            if (s.Length > 3)
            {
                ar.Add(s.Substring(s.Length - 3));
                s = s.Remove(s.Length - 3);
            }
            else
                ar.Add(s);
        }
        bool flag = false;
        for (int j = ar.Count - 1; j >= 0; j--)
        {
            if (int.Parse(ar[j].ToString()) != 0)
            {
                if (j != 0)
                    retS += ReadStr(ar[j].ToString()) + " " + pasvand[j] + " و ";
                else
                {
                    retS += ReadStr(ar[j].ToString()) + " " + pasvand[j];
                    flag = true;
                }
            }
        }

        if (!flag && retS.LastIndexOf('و') == retS.Length - 2)
            retS = retS.Remove(retS.LastIndexOf('و'));
        return retS;
    }
    #endregion

    #region CurrentProvince
    public static int GetCurrentProvinceId()
    {
        int PrId = 2;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceId"]))
            PrId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceId"]);
        return PrId;
    }

    public static int GetCurrentProvinceNezamCode()
    {
        int NezamCode = 17;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceNezamCode"]))
            NezamCode = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceNezamCode"]);
        return NezamCode;
    }

    public static int GetCurrentProvinceNezamCodeFromTblProvince()
    {
        int NezamCode = 17;
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        ProvinceManager.FindByCode(GetCurrentProvinceId());
        if (ProvinceManager.Count == 1)
        {
            if (!IsDBNullOrNullValue(ProvinceManager[0]["NezamCode"]))
                return (Convert.ToInt32(ProvinceManager[0]["NezamCode"]));
        }
        return NezamCode;
    }
    #endregion

    //public static int GetInstitueParentNcId()
    //{
    //    int NcId = 7;
    //    return NcId;
    //}

    public static int GetInstitueParentNcId()
    {
        int NcId = 3;
        return NcId;
    }

    public static int GetSettelmentParentNcId()
    {
        int NcId = 2;
        return NcId;
    }

    public static string GetFormattedObject(object o)
    {
        if (Utility.IsDBNullOrNullValue(o))
            return "";
        return o.ToString();
    }

    public static Boolean IsCurrentUserFromMainAgentId()
    {
        if (Utility.GetCurrentUser_AgentId() == Utility.GetMainAgentId())
            return true;
        else
            return false;
    }
    public static Boolean IsCurrentUserFromSpecificAgent(int AgentId)
    {
        if (Utility.GetCurrentUser_AgentId() == AgentId)
            return true;
        else
            return false;
    }
    #region Automation Methods
    public static string InsertInLetter(string[] ImgArr, int SecretariatId, int UserId)
    {

        TSP.DataManager.Automation.InsertInLetter InsertInLetter = new TSP.DataManager.Automation.InsertInLetter(ImgArr, SecretariatId, UserId, GetLetterSerialNumber());
        return InsertInLetter.Insert();
    }

    private static String GetLetterSerialNumber()
    {
        TSP.DataManager.Automation.LettersManager LetterManager = new TSP.DataManager.Automation.LettersManager();
        String SerialNumber = "";
        do
        {
            SerialNumber = GenFollowCode(FollowType.AutomationLetters);
        } while (LetterManager.SelectAutomationLettersSerialNumber(SerialNumber).Rows.Count > 0);
        return SerialNumber;
    }
    #endregion

    public static void SetGridRowIndex(TSP.WebControls.CustomAspxDevGridView customAspxDevGridView, String PostId, ref int Index)
    {
        if (!string.IsNullOrEmpty(PostId))
        {
            PostId = Utility.DecryptQS(PostId);
            {
                if (!string.IsNullOrEmpty(PostId))
                {
                    //int PostKeyValue = int.Parse(PostId);
                    object PostKeyValue = PostId;
                    customAspxDevGridView.DataBind();
                    Index = customAspxDevGridView.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / customAspxDevGridView.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        customAspxDevGridView.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        customAspxDevGridView.JSProperties["cpIsVisible"] = 1;
                        customAspxDevGridView.JSProperties["cpSelectedIndex"] = Index;
                        customAspxDevGridView.DetailRows.ExpandRow(Index);
                        customAspxDevGridView.FocusedRowIndex = Index;
                        customAspxDevGridView.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
    }

    #region NavigationBar User Permissions Methods

    public static void SetNavigationBarUserAccess(TSP.WebControls.CustomASPxNavBar CustomASPxNavBar)
    {
        int UserId = GetCurrentUser_UserId();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByCode(UserId);
        if (LoginManager.Count == 1)
        {
            Boolean IsAdmin = false;
            if (!Utility.IsDBNullOrNullValue(LoginManager[0]["IsAdmin"]) && Convert.ToBoolean(LoginManager[0]["IsAdmin"]))
                IsAdmin = true;
            if (UserId == 1 || IsAdmin)
                return;
        }
        TSP.DataManager.UserRightManager UserRightManager = new TSP.DataManager.UserRightManager();
        DataTable dt = UserRightManager.SelectUserRightForNavigationBarAccess(GetCurrentUser_UserId());
        DataTable dtSubTitle = UserRightManager.SelectUserRightForNavigationBarSubTitleAccess(GetCurrentUser_UserId());
        if (dt.Rows.Count == 0)
        {
            CustomASPxNavBar.Visible = false;
            return;
        }
        foreach (DevExpress.Web.NavBarGroup grp in CustomASPxNavBar.Groups)
        {
            if (!string.IsNullOrEmpty(grp.Target))
            {
                // int CurrentTtCode = Convert.ToInt32(grp.Target);
                DataRow[] dr = dt.Select("TtCode='" + grp.Target + "'");
                if (dr.Length == 0)
                {
                    grp.Visible = false;
                }
                else
                    grp.Visible = true;
                foreach (DevExpress.Web.NavBarItem item in grp.Items)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        DataRow[] dritem = dtSubTitle.Select("TtCode='" + item.Name + "'");
                        if (dritem.Length == 0)
                        {
                            item.Visible = false;
                        }
                        else
                            item.Visible = true;
                    }
                }
            }
            else
                grp.Visible = true;
        }
    }

    /// <summary>
    /// برای مواردی که به صورت جداگانه دارای منوی بالا می باشند.
    /// </summary>
    /// <param name="MainCustomASPxNavBar"></param>
    /// <param name="SubCustomASPxNavBar"></param>
    /// <param name="TtCode">TtCode Of Selected</param>
    public static void SetNavigationBarUserAccess(TSP.WebControls.CustomASPxNavBar MainCustomASPxNavBar, TSP.WebControls.CustomASPxNavBar SubCustomASPxNavBar, int TtCode)
    {
        if (GetCurrentUser_UserId() == 1)
            return;
        TSP.DataManager.UserRightManager UserRightManager = new TSP.DataManager.UserRightManager();
        DataTable dt = UserRightManager.SelectUserRightForNavigationBarAccess(GetCurrentUser_UserId());
        if (dt.Rows.Count == 0)
        {
            MainCustomASPxNavBar.Visible = false;
            return;
        }
        foreach (DevExpress.Web.NavBarGroup grp in MainCustomASPxNavBar.Groups)
        {
            if (!string.IsNullOrEmpty(grp.Target))
            {
                // int CurrentTtCode = Convert.ToInt32(grp.Target);
                DataRow[] dr = dt.Select("TtCode='" + grp.Target + "'");
                if (Convert.ToInt32(grp.Target) == TtCode && dr.Length == 0)
                {
                    SubCustomASPxNavBar.Visible = false;
                }
                if (dr.Length == 0)
                {
                    grp.Visible = false;
                }
                else
                    grp.Visible = true;
            }
            else
                grp.Visible = true;
        }

        // SetNavigationBarUserAccess(SubCustomASPxNavBar);
    }
    #endregion

    /// <summary>
    /// Save error information
    /// </summary>
    /// <param name="err">Error exception</param>
    public static void SaveWebsiteError(Exception err)
    {
        (new TSP.DataManager.WebsiteErrorsManager()).InsertError(err, HttpContext.Current.Request.Url.AbsoluteUri, GetCurrentUser_UserId());       
    }

    public static Boolean WorkBasedOnWorkRequest()
    {
        if (GetCurrentUser_AgentId() == GetCurrentAgentCode() && IsCapacityBasedOnWorkRequestMainAgent())
            return true;
        if (GetCurrentUser_AgentId() != GetCurrentAgentCode() && IsCapacityBasedOnWorkRequestOtherAgentOpen())
            return true;
        return false;
    }

    
    public static void SaveWebsiteError(Exception err, TSP.DataManager.TransactionManager TransactionManager)
    {
        TSP.DataManager.WebsiteErrorsManager WebsiteErrorsManager = new TSP.DataManager.WebsiteErrorsManager();
        TransactionManager.Add(WebsiteErrorsManager);
        WebsiteErrorsManager.InsertError(err, HttpContext.Current.Request.Url.AbsoluteUri, GetCurrentUser_UserId());
    }
    #region WebsiteStatistics
    //public static int SetWeeklyVisitor()
    //{
    //    Utility.GetYearOfToday
    //}
    public static int SetDailyVisitor()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteDailyStatistic.xml"));

        XmlNodeList dayList = xmlDoc.GetElementsByTagName("Day");
        string count = dayList.Count.ToString();
        XmlNode day = xmlDoc.DocumentElement.SelectSingleNode("descendant::Day[Id='" + count + "']");
        string date = day.SelectSingleNode("Date").InnerText;
        int qty;
        int.TryParse(day.SelectSingleNode("Qty").InnerText, out qty);
        string curentDate = GetDateOfToday();
        if (date == curentDate)
        {
            qty++;
            day.SelectSingleNode("Qty").InnerText = qty.ToString();

        }
        else
        {
            qty = 1;
            string Id = (dayList.Count + 1).ToString();
            XmlNode xmlDay = xmlDoc.CreateNode(XmlNodeType.Element, "Day", null);
            XmlNode xmlId = xmlDoc.CreateElement("Id");
            xmlId.InnerText = Id;
            XmlNode xmlDate = xmlDoc.CreateElement("Date");
            xmlDate.InnerText = curentDate;
            XmlNode xmlQty = xmlDoc.CreateElement("Qty");
            xmlQty.InnerText = qty.ToString();
            xmlDay.AppendChild(xmlId);
            xmlDay.AppendChild(xmlDate);
            xmlDay.AppendChild(xmlQty);
            XmlNode NewDay = xmlDoc.DocumentElement.SelectSingleNode("//Days");
            NewDay.InsertAfter(xmlDay, day);
        }

        xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteDailyStatistic.xml"));
        return qty;
    }
    public static int GetDailyVisitor()
    {
        try
        {
            int DailyVisitors = Convert.ToInt32(HttpContext.Current.Application["DailyCumVisitors"]);
            if (DailyVisitors > 0)
                return DailyVisitors;
        }
        catch (Exception) { }
        return 1;
    }

    public static int SetWeeklyVisitor()
    {
        System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteWeeklyStatistic.xml"));

        XmlNodeList dayList = xmlDoc.GetElementsByTagName("Week");
        string count = dayList.Count.ToString();
        XmlNode week = xmlDoc.DocumentElement.SelectSingleNode("descendant::Week[Id='" + count + "']");
        int no;
        int.TryParse(week.SelectSingleNode("No").InnerText, out no);
        int qty;
        int.TryParse(week.SelectSingleNode("Qty").InnerText, out qty);

        int curentNoWeek = PDate.GetWeekOfYear(DateTime.Today, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Saturday);
        if (no == curentNoWeek)
        {
            qty++;
            week.SelectSingleNode("Qty").InnerText = qty.ToString();

        }
        else
        {
            qty = 1;
            string Id = (dayList.Count + 1).ToString();
            XmlNode xmlWeek = xmlDoc.CreateNode(XmlNodeType.Element, "Week", null);
            XmlNode xmlId = xmlDoc.CreateElement("Id");
            xmlId.InnerText = Id;
            XmlNode xmlNo = xmlDoc.CreateElement("No");
            xmlNo.InnerText = curentNoWeek.ToString();
            XmlNode xmlCreateDate = xmlDoc.CreateElement("CreateDate");
            xmlCreateDate.InnerText = GetDateOfToday();
            XmlNode xmlQty = xmlDoc.CreateElement("Qty");
            xmlQty.InnerText = qty.ToString();
            xmlWeek.AppendChild(xmlId);
            xmlWeek.AppendChild(xmlCreateDate);
            xmlWeek.AppendChild(xmlNo);
            xmlWeek.AppendChild(xmlQty);
            XmlNode NewWeek = xmlDoc.DocumentElement.SelectSingleNode("//Weeks");
            NewWeek.InsertAfter(xmlWeek, week);
        }

        xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteWeeklyStatistic.xml"));
        return qty;
    }

    public static int GetWeeklyVisitor()
    {
        try
        {
            int WeeklyVisitors = Convert.ToInt32(HttpContext.Current.Application["WeeklyCumVisitors"]);
            if (WeeklyVisitors > 0)
                return WeeklyVisitors;
        }
        catch (Exception) { }
        return 1;
    }

    public static int SetMonthlyVisitor()
    {
        System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteMonthlyStatistic.xml"));

        XmlNodeList dayList = xmlDoc.GetElementsByTagName("Month");
        string count = dayList.Count.ToString();
        XmlNode month = xmlDoc.DocumentElement.SelectSingleNode("descendant::Month[Id='" + count + "']");
        int no;
        int.TryParse(month.SelectSingleNode("MonthNo").InnerText, out no);
        int qty;
        int.TryParse(month.SelectSingleNode("Qty").InnerText, out qty);
        int curentNoWeek = PDate.GetMonth(DateTime.Today);
        if (no == curentNoWeek)
        {
            qty++;
            month.SelectSingleNode("Qty").InnerText = qty.ToString();
        }
        else
        {
            qty = 1;
            string Id = (dayList.Count + 1).ToString();
            XmlNode xmlMonth = xmlDoc.CreateNode(XmlNodeType.Element, "Month", null);
            XmlNode xmlId = xmlDoc.CreateElement("Id");
            xmlId.InnerText = Id;
            XmlNode xmlMonthNo = xmlDoc.CreateElement("MonthNo");
            xmlMonthNo.InnerText = curentNoWeek.ToString();
            XmlNode xmlCreateDate = xmlDoc.CreateElement("CreateDate");
            xmlCreateDate.InnerText = GetDateOfToday();
            XmlNode xmlQty = xmlDoc.CreateElement("Qty");
            xmlQty.InnerText = qty.ToString();
            xmlMonth.AppendChild(xmlId);
            xmlMonth.AppendChild(xmlCreateDate);
            xmlMonth.AppendChild(xmlMonthNo);
            xmlMonth.AppendChild(xmlQty);
            XmlNode NewWeek = xmlDoc.DocumentElement.SelectSingleNode("//Months");
            NewWeek.InsertAfter(xmlMonth, month);
        }

        xmlDoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebSiteMonthlyStatistic.xml"));
        return qty;
    }

    public static int GetMonthlyVisitor()
    {
        try
        {
            int MonthlyVisitors = Convert.ToInt32(HttpContext.Current.Application["MonthlyCumVisitors"]);
            if (MonthlyVisitors > 0)
                return MonthlyVisitors;
        }
        catch (Exception) { }
        return 1;
    }

    public static int SetOnlineVisitors(int Value)
    {
        try
        {
            int OnlineVisitors = Convert.ToInt32(HttpContext.Current.Application["OnlineVisitors"]);
            if (OnlineVisitors > 0)
                return OnlineVisitors + Value;
        }
        catch (Exception) { }

        //if (Value > 0)
        //    return 1;
        //else
        return 1;
    }

    public static int GetOnlineVisitors()
    {
        try
        {
            int OnlineVisitors = Convert.ToInt32(HttpContext.Current.Application["OnlineVisitors"]);
            if (OnlineVisitors > 0)
                return OnlineVisitors;
        }
        catch (Exception) { }
        return 1;
    }

    public static int SetOnlineUsers(int Value)
    {
        try
        {
            int OnlineUsers = Convert.ToInt32(HttpContext.Current.Application["OnlineUsers"]);
            if (OnlineUsers > 0)
                return OnlineUsers + Value;
        }
        catch (Exception) { }

        if (Value > 0)
            return 1;
        else
            return 0;
    }

    public static int GetOnlineUsers()
    {
        try
        {
            int OnlineUsers = Convert.ToInt32(HttpContext.Current.Application["OnlineUsers"]);
            if (OnlineUsers > 0)
                return OnlineUsers;
        }
        catch (Exception) { }
        return 0;
    }

    public static int GetGuestVisitors()
    {
        try
        {
            int GuestVisitors = Utility.GetOnlineVisitors();// -Utility.GetOnlineUsers();
            if (GuestVisitors > 0)
                return GuestVisitors;
        }
        catch (Exception) { }
        return 0;
    }

    public static int GetTotalVisitors()
    {
        try
        {
            TSP.DataManager.WebsiteStatisticsManager objWebsiteStatistics = new TSP.DataManager.WebsiteStatisticsManager();
            if (objWebsiteStatistics.SelectStatistics())
                if (objWebsiteStatistics.TotalVisitors > 0)
                    return objWebsiteStatistics.TotalVisitors;
        }
        catch (Exception) { }
        return 0;
    }

   
    #endregion


}

