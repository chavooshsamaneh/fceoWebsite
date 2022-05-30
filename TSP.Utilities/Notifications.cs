using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

public partial class Utility
{
    public class Notifications
    {
        public enum SendTypes
        {
            Email, Message, SMS, All
        }

        public enum NotificationTypes
        {
            PasswordForget, ResetPassword, GetMembershipNo, AnswerMessageFromPublicUsers, MemberRegisterData, SessionHolding,
            SessionChangeDateTime, SessionCancel, AutomaticSMS, Epayment, GetDocFile,ObsSelected,TSProjectOwner,TSProjectFishPayment,TSPlanConfirming,PeriodAttenderSendSMS
        }

        public Char Splitter { get { return '$'; } }

        private SendTypes SendType;
        private NotificationTypes NotificationType;

        private DataTable _NotificationData;
        public DataTable NotificationData
        {
            get { return _NotificationData; }
            set { _NotificationData = value; }
        }

        public Notifications(NotificationTypes NotificationType)
        {
            this.SendType = SendTypes.SMS;
            this.NotificationType = NotificationType;
            _NotificationData = new DataTable();
            setNotificationDataTable();
        }

        public Notifications(SendTypes SendType, NotificationTypes NotificationType)
        {
            this.SendType = SendType;
            this.NotificationType = NotificationType;
            _NotificationData = new DataTable();
            setNotificationDataTable();
        }

        private void setNotificationDataTable()
        {
            if (SendType == SendTypes.Message)
            {
                _NotificationData.Columns.Add("MessageMeId");
                _NotificationData.Columns.Add("MessageUltId");
            }
            else if (SendType == SendTypes.Email)
                _NotificationData.Columns.Add("EmailAddress");
            else if (SendType == SendTypes.SMS)
            {
                _NotificationData.Columns.Add("SMSMobileNo");
                _NotificationData.Columns.Add("SMSResult");
                _NotificationData.Columns.Add("SMSMeId");
                _NotificationData.Columns.Add("SMSUltId");
            }
            else
            {
                _NotificationData.Columns.Add("MessageMeId");
                _NotificationData.Columns.Add("MessageUltId");
                _NotificationData.Columns.Add("EmailAddress");
                _NotificationData.Columns.Add("SMSMobileNo");
                _NotificationData.Columns.Add("SMSResult");
                _NotificationData.Columns.Add("SMSMeId");
                _NotificationData.Columns.Add("SMSUltId");
            }


            switch (NotificationType)
            {
                case NotificationTypes.PasswordForget:
                    _NotificationData.Columns.Add("Url");
                    break;
                case NotificationTypes.ResetPassword:
                    _NotificationData.Columns.Add("Password");
                    break;
                case NotificationTypes.GetMembershipNo:
                    _NotificationData.Columns.Add("MeId");
                    _NotificationData.Columns.Add("Username");
                    break;
                case NotificationTypes.AnswerMessageFromPublicUsers:
                    _NotificationData.Columns.Add("SenderName");
                    _NotificationData.Columns.Add("TypeName");
                    _NotificationData.Columns.Add("GroupName");
                    _NotificationData.Columns.Add("SendMessageSubject");
                    _NotificationData.Columns.Add("AnswerBody");
                    _NotificationData.Columns.Add("SendMessageBody");
                    break;
                case NotificationTypes.MemberRegisterData:
                    _NotificationData.Columns.Add("FollowCode");
                    _NotificationData.Columns.Add("Username");
                    _NotificationData.Columns.Add("FullName");
                    _NotificationData.Columns.Add("Password");
                    break;
                case NotificationTypes.SessionHolding:
                    _NotificationData.Columns.Add("SessionTitle");
                    _NotificationData.Columns.Add("SessionLocation");
                    _NotificationData.Columns.Add("SessionDate");
                    _NotificationData.Columns.Add("SessionStartTime");
                    break;
                case NotificationTypes.SessionChangeDateTime:
                    _NotificationData.Columns.Add("SessionTitle");
                    _NotificationData.Columns.Add("SessionDate");
                    _NotificationData.Columns.Add("SessionStartTime");
                    break;
                case NotificationTypes.SessionCancel:
                    _NotificationData.Columns.Add("SessionTitle");
                    _NotificationData.Columns.Add("SessionDate");
                    break;

                case NotificationTypes.Epayment:
                    _NotificationData.Columns.Add("PaymentDescription");
                    break;
                case NotificationTypes.GetDocFile:
                case NotificationTypes.ObsSelected:
                    _NotificationData.Columns.Add("Description");
                    break;

            }
        }

        public Boolean Send(Boolean IsPv)
        {
            if (SendType == SendTypes.Email)
                return SendEmail(IsPv);
            else if (SendType == SendTypes.Message)
                return SendMessage();
            else
                return (SendEmail(IsPv) == true && SendMessage() == true);
        }

        public Boolean Send()
        {
            return Send(false);
        }

        #region Email
        private Boolean SendEmail(Boolean IsPv)
        {
            IsPv = true;
            if (_NotificationData.Rows.Count == 0)
                return false;

            Boolean State = true;
            try
            {  
                //int SMPT_Port = -1;// Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_Port"]);
               // int SMPT_Port = 587;
                //***FceoMailserver
                //String Email_ID = Utility.GetEmailFceo();
                //String Email_Pass = Utility.GetEmailPasswordFceo();
                //String SMPT_Name = "mail.fceo.ir";
                //int SMPT_UseDefaultCredentials = 1;
                //int SMPT_EnableSsl = 0;
                //int SMPT_Port = 25;
                //****Gmail
                String Email_ID = "fceo.shiraz@gmail.com";
                String Email_Pass = "fceo#gmail";
                String SMPT_Name = "smtp.gmail.com";
                int SMPT_UseDefaultCredentials = 0;
                int SMPT_EnableSsl = 1;
                int SMPT_Port = 587;
                //SmtpClient client = new SmtpClient();
                //client.Host = "smtp.gmail.com";
                //client.Port = 587;
                //client.EnableSsl = true;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("YourMail@gmail.com", "YourPassword");

                //*****GMX 
                //String Email_ID = Utility.GetEmail();
                //String Email_Pass = Utility.GetEmailPassword();
                //String SMPT_Name = "mail.gmx.com";
                //int SMPT_UseDefaultCredentials = 1;
                //int SMPT_EnableSsl = 0;
                //int SMPT_Port = -1;
                //String Email_ID = Utility.GetEmail();
                //if (IsPv)
                //    Email_ID = Utility.GetEmail();
                //String Email_Pass = Utility.GetEmailPassword();
                //if (IsPv)
                //    Email_Pass = Utility.GetEmailPassword();              
                //String SMPT_Name = System.Configuration.ConfigurationManager.AppSettings["SMPT_Name"];
                //if (IsPv)
                //    SMPT_Name = System.Configuration.ConfigurationManager.AppSettings["SMPT_Name"];
                String Email_DisplayName = System.Configuration.ConfigurationManager.AppSettings["Email_DisplayName"];
                //int SMPT_UseDefaultCredentials = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_UseDefaultCredentials"]);
                //int SMPT_EnableSsl = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_EnableSsl"]);
                ////int SMPT_Port =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMPT_Port"]);
             
                //if (IsPv)
                //    SMPT_Port = -1;
                for (int i = 0; i < _NotificationData.Rows.Count; i++)
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    String[] EmailAddress = _NotificationData.Rows[i]["EmailAddress"].ToString().Split(Splitter);
                    foreach (String Email in EmailAddress)
                    {
                        if (EmailAddress.Length == 1)
                            mail.To.Add(Email);
                        else
                            mail.Bcc.Add(Email);
                    }
                    mail.Subject = getEmailSubject();
                    mail.From = new System.Net.Mail.MailAddress(Email_ID, Email_DisplayName);
                    mail.IsBodyHtml = true;
                    mail.Body = GetEmailHeader(getEmailSubject());
                    mail.Body += "<div dir=rtl align=right style='border: double Medium gray; font-family:Tahoma; font-size:10pt; padding:5px 5px 5px 5px' width='100%'>" + getEmailBody(i) + "</div>";
                    mail.Body += GetEmailFooter();

                    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(Email_ID, Email_Pass);
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SMPT_Name);
                    //////smtp.UseDefaultCredentials = false;
                    smtp.UseDefaultCredentials =  (SMPT_UseDefaultCredentials == 0) ? false : true;
                    //////smtp.EnableSsl = false;
                    smtp.EnableSsl = (SMPT_EnableSsl == 0) ? false : true;
                    smtp.Credentials = cred;
                    if (SMPT_Port != -1)
                    {
                        smtp.Port = SMPT_Port;
                    }

                    smtp.Send(mail);


                }
            }
            catch (Exception err)
            {
                State = false;
                Utility.SaveWebsiteError(err);
            }
            return State;
        }

        private String getEmailBody(int NotificationDataIndex)
        {
            String EmailBody = "<br><div dir=\"rtl\" align=\"right\" style=\"font-family:tahoma\">";

            switch (NotificationType)
            {
                case NotificationTypes.PasswordForget:
                    String url = _NotificationData.Rows[NotificationDataIndex]["Url"].ToString();
                    EmailBody += "کاربر گرامی " + "<br><br>";
                    EmailBody += "برای تغییر رمز عبور خود بر روی لینک زیر کلیک کنید یا آن را در آدرس بار مرورگر خود کپی نمایید:";
                    EmailBody += "<br><br><div align=left dir=ltr><a href=\"" + url + "\">" + url + "</a></div>";
                    int EmailLinkTimeOut = (int)(Utility.GetEmailLinkTimeOut() / 1440);
                    if (EmailLinkTimeOut > 0)
                        EmailBody += "<br><br>مدت اعتبار این لینک <b>" + EmailLinkTimeOut + "</b> روز می باشد.";
                    EmailBody += "<br><br>لطفاً در حفظ مشخصات کاربری خود کوشا بوده و هیچ گاه این مشخصات را در اختیار دیگران قرار ندهید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.ResetPassword:
                    String Password = _NotificationData.Rows[NotificationDataIndex]["Password"].ToString();
                    EmailBody += "کاربر گرامی" + "<br><br>";
                    EmailBody += "رمز عبور شما در تاریخ " + "<span dir=ltr>" + GetDateOfToday() + "</span>" + " به <b>" + Password + "</b> تغییر یافت.";
                    EmailBody += "<br><br>لطفاً در حفظ مشخصات کاربری خود کوشا بوده و هیچ گاه این مشخصات را در اختیار دیگران قرار ندهید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.GetMembershipNo:
                    String MeId = _NotificationData.Rows[NotificationDataIndex]["MeId"].ToString();
                    String Username = _NotificationData.Rows[NotificationDataIndex]["Username"].ToString();
                    EmailBody += "کاربر گرامی" + "<br><br>";
                    EmailBody += "با توجه به قوانین سازمان نظام مهندسی ساختمان، پس از بررسی درخواست عضویت شما، اطلاعات شما به صورت زیر تغییر یافته است:" + "<br><br>";
                    EmailBody += "<table><tr><td width='20px'></td><td width='100px' align=right style='font-family:Tahoma; font-size:10pt;'>کد عضویت :</td><td align=right><b>" + MeId + "</b></td></tr>";
                    EmailBody += "<tr><td></td><td align=right style='font-family:Tahoma; font-size:10pt;'>نام کاربری :</td><td align=right><b>" + Username + "</b></td></tr></table><br>";
                    EmailBody += "از این پس می توانید با " + "<b>" + "نام کاربری جدید" + "</b> و <b>" + "رمز عبور قبلی" + "</b>" + " خود وارد سایت شوید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.AnswerMessageFromPublicUsers:
                    String SenderName = _NotificationData.Rows[NotificationDataIndex]["SenderName"].ToString();
                    String TypeName = _NotificationData.Rows[NotificationDataIndex]["TypeName"].ToString();
                    String GroupName = _NotificationData.Rows[NotificationDataIndex]["GroupName"].ToString();
                    String SendMessageSubject = _NotificationData.Rows[NotificationDataIndex]["SendMessageSubject"].ToString();
                    String AnswerBody = _NotificationData.Rows[NotificationDataIndex]["AnswerBody"].ToString();
                    String SendMessageBody = _NotificationData.Rows[NotificationDataIndex]["SendMessageBody"].ToString();

                    EmailBody += "<div style=\"font-size:9pt;\">";
                    EmailBody += "<p>نام : " + SenderName + "</p>";
                    EmailBody += "<p>نوع پیغام : " + TypeName + "</p>";
                    EmailBody += "<p>بخش گیرنده : " + GroupName + "</p>";
                    if (String.IsNullOrEmpty(SendMessageSubject.Trim()) == false)
                        EmailBody += "<p>موضوع : " + SendMessageSubject + "</p></div>";
                    EmailBody += "<hr><p style=\"font-size:9pt\"><b>پاسخ نامه شما :</b></p>";
                    EmailBody += "<p style=\"BACKGROUND-COLOR: aliceblue; font-size:10pt\">" + AnswerBody.Replace("\n", "<br>") + "</p>";
                    EmailBody += "<hr><p style=\"font-size:9pt;\"><b>متن نامه ارسال شده : </b><br><div style=\"BACKGROUND-COLOR: whitesmoke; font-size:8pt\">";
                    EmailBody += SendMessageBody.Replace("\n", "<br>") + "</div></p>";
                    EmailBody += "<hr><div style=\"BACKGROUND-COLOR: snow; font-size:9pt\"><br> ***  کاربر گرامی، لطفا از طریق صفحه \"تماس با ما\" از خود سایت برای پاسخ به این نامه اقدام نمایید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br></div>";
                    break;
                case NotificationTypes.MemberRegisterData:
                    String MemberRegisterData_FollowCode = _NotificationData.Rows[NotificationDataIndex]["FollowCode"].ToString();
                    String MemberRegisterData_Username = _NotificationData.Rows[NotificationDataIndex]["Username"].ToString();
                    String MemberRegisterData_FullName = _NotificationData.Rows[NotificationDataIndex]["FullName"].ToString();
                    String MemberRegisterData_Password = _NotificationData.Rows[NotificationDataIndex]["Password"].ToString();

                    EmailBody += "کاربر گرامی، " + MemberRegisterData_FullName + "<br><br>";
                    EmailBody += "پیش ثبت نام  شما با موفقیت انجام شد. کد رهگیری شما <b>" + MemberRegisterData_FollowCode + "</b> می باشد.";
                    EmailBody += "<br><br>اطلاعات کاربری شما به صورت زیر می باشد:<br>";
                    EmailBody += "نام کاربری : <b>" + MemberRegisterData_Username + "</b><br>";
                    EmailBody += "رمز عبور : <b>" + MemberRegisterData_Password + "</b>";
                    EmailBody += "<br><br>لطفاً در حفظ مشخصات کاربری خود کوشا بوده و هیچ گاه این مشخصات را در اختیار دیگران قرار ندهید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.SessionHolding:
                    String SessionTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();
                    String SessionLocation = _NotificationData.Rows[NotificationDataIndex]["SessionLocation"].ToString();

                    EmailBody += "با سلام و احترام" + "<br><br>";
                    EmailBody += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle + " مورخ " + SessionDate + " ساعت " + SessionStartTime + " در محل " + SessionLocation;
                    EmailBody += " تشکیل خواهد شد ." + "<br><br>" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.SessionChangeDateTime:
                    String SessionChangeTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionChangeStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionChangeDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    EmailBody += "با سلام و احترام" + "<br><br>";
                    EmailBody += "بدینوسیله به استحضار می رساند زمان برگزاری جلسه " + SessionChangeTitle + " به تاریخ " + SessionChangeDate + " ساعت " + SessionChangeStartTime + " تغییر یافته است.";
                    EmailBody += "<br><br>" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
                case NotificationTypes.SessionCancel:
                    String SessionTitle2 = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionDate2 = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    EmailBody += "با سلام و احترام" + "<br><br>";
                    EmailBody += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle2 + " مورخ " + SessionDate2 + " لغو شد.";
                    EmailBody += "<br><br>با تشکر" + "<br><br>";
                    break;
            }

            return EmailBody + "</div>";
        }

        private String getEmailSubject()
        {
            String Subject = "";

            switch (NotificationType)
            {
                case NotificationTypes.PasswordForget:
                    Subject = "فراموشی رمز عبور کاربر";
                    break;
                case NotificationTypes.ResetPassword:
                    Subject = "تغییر رمز عبور کاربر";
                    break;
                case NotificationTypes.GetMembershipNo:
                    Subject = "دریافت کد عضویت";
                    break;
                case NotificationTypes.AnswerMessageFromPublicUsers:
                    Subject = "پاسخ نامه کاربر";
                    break;
                case NotificationTypes.MemberRegisterData:
                    Subject = "مشخصات کاربری";
                    break;
                case NotificationTypes.SessionHolding:
                    Subject = "جلسه";
                    break;
                case NotificationTypes.SessionChangeDateTime:
                    Subject = "تغییر زمان برگزاری جلسه";
                    break;
                case NotificationTypes.SessionCancel:
                    Subject = "لغو جلسه";
                    break;
            }

            return Subject;
        }

        private String GetEmailHeader(String Subject)
        {
            string Header = "<div dir=rtl align=right style='background-color: gainsboro; border: double Medium gray; font-family:Tahoma; font-size:10pt; padding:5px 5px 5px 5px' width='100%'>";
            Header += "<table width='100%'><tr>";
            Header += "<td width='85%'><table width='100%'><tr>";
            Header += "<td width='50px' style='font-family:Tahoma; font-size:10pt;'>تاریخ :</td>";
            Header += "<td><span dir=ltr>" + Utility.GetDateOfToday() + "</span>   " + Utility.GetCurrentTime() + "</td></tr>";
            Header += "<tr><td colspan=2><br></td></tr>";
            Header += "<tr><td style='font-family:Tahoma; font-size:10pt;'>عنوان :</td>";
            Header += "<td style='font-family:Tahoma; font-size:10pt;'><b>" + Subject + "</b></td></tr></table>";
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

        private String GetEmailFooter()
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

        #region Message
        private Boolean SendMessage()
        {
            if (_NotificationData.Rows.Count == 0)
                return false;

            int CurrentUserId = Utility.GetCurrentUser_UserId();
            TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
            TSP.DataManager.MessageManager MessageManager = new TSP.DataManager.MessageManager();
            TSP.DataManager.MessageReceiverManager MessageReceiverManager = new TSP.DataManager.MessageReceiverManager();
            Transaction.Add(MessageManager);
            Transaction.Add(MessageReceiverManager);
            Transaction.BeginSave();

            try
            {
                for (int i = 0; i < _NotificationData.Rows.Count; i++)
                {
                    DataRow MsgRow = MessageManager.NewRow();

                    MsgRow["SenderId"] = 0;//***System Create Msg
                    MsgRow["SenderType"] = 0;//***System Create Msg
                    MsgRow["IsSenderPart"] = 0;
                    MsgRow["MsgTypeId"] = 1;//***Type = Send Msg
                    MsgRow["NeedConfirm"] = 0;
                    MsgRow["MsgSubject"] = getMessageSubject();
                    MsgRow["MsgBody"] = getMessageBody(i);
                    MsgRow["Date"] = Utility.GetDateOfToday();
                    //  MsgRow["TableType"] = "";
                    // MsgRow["TableId"] = "";
                    MsgRow["Priority"] = 0;
                    MsgRow["InActive"] = 0;
                    MsgRow["UserId"] = CurrentUserId;
                    MsgRow["ModifiedDate"] = DateTime.Now;

                    MessageManager.AddRow(MsgRow);
                    if (MessageManager.Save() > 0)
                    {
                        MessageManager.DataTable.AcceptChanges();
                        int MessageId = Convert.ToInt32(MessageManager[0]["MsgId"]);
                        String[] MeIds = _NotificationData.Rows[i]["MessageMeId"].ToString().Split(Splitter);
                        String[] UltIds = _NotificationData.Rows[i]["MessageUltId"].ToString().Split(Splitter);

                        for (int j = 0; j < MeIds.Length; j++)
                        {
                            DataRow MsgRecRow = MessageReceiverManager.NewRow();
                            MsgRecRow["MsgId"] = MessageId;
                            MsgRecRow["IsRead"] = 0;
                            MsgRecRow["ReceiverId"] = MeIds[j];
                            MsgRecRow["ReceiverType"] = UltIds[j];
                            MsgRecRow["IsReceiverPart"] = 0;
                            MsgRecRow["Answer"] = 0;
                            MsgRecRow["IsResignation"] = 0;
                            MsgRecRow["InActive"] = 0;
                            MsgRecRow["UserId"] = CurrentUserId;
                            MsgRecRow["ModifiedDate"] = DateTime.Now;
                            MessageReceiverManager.AddRow(MsgRecRow);
                        }

                        int count = MessageReceiverManager.Save();
                        if (count > 0)
                        {
                            MessageReceiverManager.DataTable.AcceptChanges();
                        }
                        else
                        {
                            Transaction.CancelSave();
                            return false;
                        }
                    }
                    else
                    {
                        Transaction.CancelSave();
                        return false;
                    }
                }

                Transaction.EndSave();
                return true;
            }
            catch (Exception err)
            {
                Transaction.CancelSave();
                Utility.SaveWebsiteError(err);
                return false;
            }

            return false;
        }

        private String getMessageBody(int NotificationDataIndex)
        {
            String Message = "<div dir=\"rtl\" align=\"right\" style=\"font-family:tahoma\">";

            switch (NotificationType)
            {
                case NotificationTypes.SessionHolding:
                    String SessionTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();
                    String SessionLocation = _NotificationData.Rows[NotificationDataIndex]["SessionLocation"].ToString();

                    Message += "با سلام و احترام" + "<br>";
                    Message += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle + " مورخ " + SessionDate + " ساعت " + SessionStartTime + " در محل " + SessionLocation;
                    Message += " تشکیل خواهد شد ." + "<br><br>" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    Message += "<br><br>با تشکر";
                    break;
                case NotificationTypes.SessionChangeDateTime:
                    String SessionChangeTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionChangeStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionChangeDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    Message += "با سلام و احترام" + "<br><br>";
                    Message += "بدینوسیله به استحضار می رساند زمان برگزاری جلسه " + SessionChangeTitle + " به تاریخ " + SessionChangeDate + " ساعت " + SessionChangeStartTime + " تغییر یافته است.";
                    Message += "<br><br>" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    Message += "<br><br>با تشکر";
                    break;
                case NotificationTypes.SessionCancel:
                    String SessionTitle2 = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionDate2 = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    Message += "با سلام و احترام" + "<br><br>";
                    Message += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle2 + " مورخ " + SessionDate2 + " لغو شد.";
                    Message += "<br><br>با تشکر" + "<br><br>";
                    break;
            }

            return Message + "</div>";
        }

        private String getMessageSubject()
        {
            String Subject = "";

            switch (NotificationType)
            {
                case NotificationTypes.SessionHolding:
                    Subject = "جلسه";
                    break;
                case NotificationTypes.SessionCancel:
                    Subject = "لغو جلسه";
                    break;
            }

            return Subject;
        }
        #endregion

        #region SMS
        public String getSMSBody(int NotificationDataIndex)
        {
            String SMSBody = "";

            switch (NotificationType)
            {
                case NotificationTypes.MemberRegisterData:
                    String MemberRegisterData_FollowCode = _NotificationData.Rows[NotificationDataIndex]["FollowCode"].ToString();
                    String MemberRegisterData_Username = _NotificationData.Rows[NotificationDataIndex]["Username"].ToString();
                    String MemberRegisterData_FullName = _NotificationData.Rows[NotificationDataIndex]["FullName"].ToString();
                    String MemberRegisterData_Password = _NotificationData.Rows[NotificationDataIndex]["Password"].ToString();

                    SMSBody += "کاربر گرامی، " + MemberRegisterData_FullName + "\n" + "پیش ثبت نام  شما با موفقیت انجام شد." + "\n";
                    SMSBody += "کد رهگیری: " + MemberRegisterData_FollowCode + "\n";
                    SMSBody += "نام کاربری: " + MemberRegisterData_Username;
                    break;
                case NotificationTypes.SessionHolding:
                    String SessionTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();
                    String SessionLocation = _NotificationData.Rows[NotificationDataIndex]["SessionLocation"].ToString();

                    SMSBody += "با سلام و احترام" + "\n";
                    SMSBody += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle + " مورخ " + SessionDate + " ساعت " + SessionStartTime + " در محل " + SessionLocation;
                    SMSBody += " تشکیل خواهد شد ." + "\n" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    break;
                case NotificationTypes.SessionChangeDateTime:
                    String SessionChangeTitle = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionChangeStartTime = _NotificationData.Rows[NotificationDataIndex]["SessionStartTime"].ToString();
                    String SessionChangeDate = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    SMSBody += "با سلام و احترام" + "\n";
                    SMSBody += "بدینوسیله به استحضار می رساند زمان برگزاری جلسه " + SessionChangeTitle + " به تاریخ " + SessionChangeDate + " ساعت " + SessionChangeStartTime + " تغییر یافته است.";
                    SMSBody += "\n" + "خواهشمند است راس ساعت مقرر حضور بهم رسانید.";
                    break;
                case NotificationTypes.SessionCancel:
                    String SessionTitle2 = _NotificationData.Rows[NotificationDataIndex]["SessionTitle"].ToString();
                    String SessionDate2 = _NotificationData.Rows[NotificationDataIndex]["SessionDate"].ToString();

                    SMSBody += "با سلام و احترام" + "\n";
                    SMSBody += "بدینوسیله به استحضار می رساند جلسه " + SessionTitle2 + " مورخ " + SessionDate2 + " لغو شد.";
                    break;
                case NotificationTypes.Epayment:
                    SMSBody = _NotificationData.Rows[NotificationDataIndex]["PaymentDescription"].ToString();
                    break;
                case NotificationTypes.GetDocFile:
                    SMSBody = _NotificationData.Rows[NotificationDataIndex]["Description"].ToString();
                    break;

            }

            if (String.IsNullOrWhiteSpace(SMSBody) == false)
                SMSBody += "\n" + "سازمان نظام مهندسی ساختمان استان فارس";

            return SMSBody;
        }

        public String getSMSSubject()
        {
            String Subject = "";

            switch (NotificationType)
            {
                case NotificationTypes.MemberRegisterData:
                    Subject = "پیش ثبت نام";
                    break;
                case NotificationTypes.SessionHolding:
                    Subject = "جلسه";
                    break;
                case NotificationTypes.SessionCancel:
                    Subject = "لغو جلسه";
                    break;
                case NotificationTypes.AutomaticSMS:
                    Subject = "پیامک اتوماتیک";
                    break;
            }

            return Subject;
        }
        #endregion
    }
}
