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

public partial class RecievedSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Exception ex= new Exception("SMSReciveddddddddddddddddd!!!!!!!!!!!!!!!!!1");

        // Utility.SaveWebsiteError(ex);
        if (Request.QueryString["TO"] == null && Request.QueryString["FROM"] == null && Request.QueryString["TEXT"] == null)
        {
            return;
        }
        int CurrentUserId = 1;
        string To = Request.QueryString["TO"];
        string From = Request.QueryString["FROM"];
        string Message = Request.QueryString["TEXT"];
        if (string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Message) || string.IsNullOrEmpty(From))
        {
            return;
        }
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TransactionManager.Add(SmsManager);
        TransactionManager.Add(SmsRecieverManager);
        TransactionManager.Add(MemberManager);

        try
        {
            DataTable dtMe = MemberManager.SearchMemberByMobileNo(From);
            if (dtMe.Rows.Count > 0)//Receiver is Member
            {
                int MeId = (int)dtMe.Rows[0]["MeId"];
                string SMSBody = GenerateSMSBody(Message.Trim(), MeId);

                TransactionManager.BeginSave();
                #region Save RecievedSMS
                DataRow SmsRow = SmsManager.NewRow();
                SmsRow["SMSDate"] = Utility.GetDateOfToday();
                SmsRow["SMSTime"] = Utility.GetCurrentTime();
                SmsRow["ExpireDate"] = Utility.GetDateOfToday();
                SmsRow["IsFarsi"] = 0;
                SmsRow["SmsCount"] = 0;
                SmsRow["CostId"] = 0;
                SmsRow["SmsTypeId"] = 0;
                SmsRow["SenderId"] = MeId;
                SmsRow["SmsSubject"] = "پیام کوتاه دریافت شده";
                SmsRow["SmsBody"] = Message;
                SmsRow["SmsCost"] = 0;
                SmsRow["IsDelivered"] = 1;
                SmsRow["InActive"] = 0;
                SmsRow["UserId"] = CurrentUserId;
                SmsRow["ModifiedDate"] = DateTime.Now;
                SmsManager.AddRow(SmsRow);
                if (SmsManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                SmsManager.DataTable.AcceptChanges();
                DataRow SmsReRow = SmsRecieverManager.NewRow();
                SmsReRow["SmsId"] = SmsManager[0]["SmsId"].ToString();
                SmsReRow["RecieverId"] = 0;
                SmsReRow["RecieverCellPhone"] = To;
                SmsReRow["InActive"] = 0;
                SmsReRow["UserId"] = CurrentUserId;
                SmsReRow["ModifiedDate"] = DateTime.Now;
                SmsReRow["IsDelivered"] = 0;

                SmsRecieverManager.AddRow(SmsReRow);
                if (SmsRecieverManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                string[] SmsCost = CalculateCost(false, SMSBody.Length);
                float Cost = 0;
                int CostId = -1;
                if (!string.IsNullOrEmpty(SmsCost[0]))
                    Cost = float.Parse(SmsCost[0]);
                else
                {
                    TransactionManager.CancelSave();
                    return;
                }

                if (!string.IsNullOrEmpty(SmsCost[1]))
                    CostId = int.Parse(SmsCost[1]);
                else
                {
                    TransactionManager.CancelSave();
                    return;
                }

                SmsRecieverManager.DataTable.AcceptChanges();
                SmsRow = SmsManager.NewRow();
                SmsRow["SMSDate"] = Utility.GetDateOfToday();
                SmsRow["SMSTime"] = Utility.GetCurrentTime();
                SmsRow["ExpireDate"] = Utility.GetDateOfToday();
                SmsRow["IsFarsi"] = 1;
                SmsRow["SmsCount"] = Math.Ceiling((double)((double)(SMSBody.Length) / 70));// Math.Ceiling((double)((double)(SmsLen) / 160));
                SmsRow["CostId"] = CostId;
                SmsRow["SmsCost"] = Cost;
                SmsRow["SmsTypeId"] = (int)TSP.DataManager.SMSType.AnswerOfRecieved;
                SmsRow["SenderId"] = 0;
                SmsRow["SmsSubject"] = "پاسخ پیام کوتاه دریافت شده";
                SmsRow["SmsBody"] = SMSBody;
                SmsRow["IsDelivered"] = 0;
                SmsRow["InActive"] = 0;
                SmsRow["ModifiedDate"] = DateTime.Now;
                SmsRow["UserId"] = CurrentUserId;
                SmsManager.AddRow(SmsRow);
                if (SmsManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                SmsReRow = SmsRecieverManager.NewRow();
                SmsReRow["SmsId"] = SmsManager[SmsManager.Count - 1]["SmsId"].ToString();
                SmsReRow["RecieverId"] = MeId;
                SmsReRow["RecieverCellPhone"] = From;
                SmsReRow["InActive"] = 0;
                SmsReRow["ModifiedDate"] = DateTime.Now;
                SmsReRow["IsDelivered"] = 0;
                SmsReRow["UserId"] = CurrentUserId;

                SmsRecieverManager.AddRow(SmsReRow);
                if (SmsRecieverManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                int SMSId = (int)SmsManager[SmsManager.Count - 1]["SmsId"];
                if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
                    SendSMSByMagfa(MeId, From, SMSBody, SMSId, SmsManager, TransactionManager);
                else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
                    SendSMS(MeId, From, SMSBody, SMSId, SmsManager, TransactionManager);

                #endregion
            }
            else//Receiver is not Member
            {
                TransactionManager.BeginSave();
                #region Save RecievedSMS
                DataRow SmsRow = SmsManager.NewRow();
                SmsRow["SMSDate"] = Utility.GetDateOfToday();
                SmsRow["SMSTime"] = Utility.GetCurrentTime();
                SmsRow["ExpireDate"] = Utility.GetDateOfToday();
                SmsRow["IsFarsi"] = 0;
                SmsRow["SmsCount"] = 0;
                SmsRow["CostId"] = 0;
                SmsRow["SmsTypeId"] = 0;
                SmsRow["SenderId"] = 0;
                SmsRow["SmsSubject"] = "پیام کوتاه دریافت شده";
                SmsRow["SmsBody"] = Message;
                SmsRow["SmsCost"] = 0;
                SmsRow["IsDelivered"] = 1;
                SmsRow["InActive"] = 0;
                SmsRow["UserId"] = CurrentUserId;
                SmsRow["ModifiedDate"] = DateTime.Now;
                SmsManager.AddRow(SmsRow);
                if (SmsManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                SmsManager.DataTable.AcceptChanges();
                DataRow SmsReRow = SmsRecieverManager.NewRow();
                SmsReRow["SmsId"] = SmsManager[0]["SmsId"].ToString();
                SmsReRow["RecieverId"] = 0;
                SmsReRow["RecieverCellPhone"] = From;
                SmsReRow["InActive"] = 0;
                SmsReRow["UserId"] = CurrentUserId;
                SmsReRow["ModifiedDate"] = DateTime.Now;
                SmsReRow["IsDelivered"] = 0;

                SmsRecieverManager.AddRow(SmsReRow);
                if (SmsRecieverManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                TransactionManager.EndSave();
                #endregion
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            return;
        }
    }

    private string GenerateSMSBody(string Messgae, int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        string SMSBody = "";
        //int MeId=-1;
        //if (Type.Trim().EndsWith("#R"))
        //    Type = "OverallTime";
        string Type = "";
        string[] TypePart = Messgae.Split('*');
        if (TypePart.Length == 2)
        {
            ///////کد عضویت # R
            Type = TypePart[1].Trim();
        }
        else
        {
            Type = TypePart[0].Trim();
        }
        switch (Type.Trim())
        {
            case "0":
                SMSBody = GenerateSMSHelp();
                //SMSBody += "0:انواع پیام کوتاه";
                //SMSBody += "\n";
                //SMSBody += "1:اطلاعات عضویت";
                //SMSBody += "\n";
                //SMSBody += "2:اطلاعات پروانه";
                //SMSBody += "\n";
                break;
            case "1":
                #region Mf Info
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count == 1)
                {
                    string FirstName = MemberManager[0]["FirstName"].ToString();
                    string LastName = MemberManager[0]["LastName"].ToString();
                    string MeNo = MemberManager[0]["MeNo"].ToString();
                    // string FileNo = MemberManager[0]["FileNo"].ToString();
                    SMSBody += "نام:";
                    // SMSBody += "\n";
                    SMSBody += FirstName;
                    SMSBody += "\n";
                    SMSBody += "نام خانوادگی:";
                    //SMSBody += "\n";
                    SMSBody += LastName;
                    SMSBody += "\n";
                    SMSBody += "شماره عضویت:";
                    SMSBody += MemberManager[0]["MeNo"].ToString();
                    SMSBody += "\n";
                    SMSBody += "کد عضویت:";
                    SMSBody += MemberManager[0]["MeId"].ToString();
                    SMSBody += "\n";
                    SMSBody += "وضعیت عضویت:";
                    SMSBody += MemberManager[0]["MrsName"].ToString();
                }
                else
                {
                    SMSBody = "اطلاعاتی برای شما ثبت نگردیده است";
                }
                #endregion
                break;
            case "2":
                #region Doc Info
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count == 1)
                {
                    string FileNo = MemberManager[0]["FileNo"].ToString();
                    string ImpGrdName = MemberManager[0]["ImpGrdName"].ToString();
                    string ObsGrdName = MemberManager[0]["ObsGrdName"].ToString();
                    string DesGrdName = MemberManager[0]["DesGrdName"].ToString();
                    SMSBody += "شماره پروانه اشتغال:";
                    SMSBody += FileNo;
                    SMSBody += "\n";
                    SMSBody += "صلاحیت اجرا:";
                    SMSBody += ImpGrdName;
                    SMSBody += "\n";
                    SMSBody += "صلاحیت نظارت:";
                    SMSBody += ObsGrdName;
                    SMSBody += "\n";
                    SMSBody += "صلاحیت طراحی:";
                    SMSBody += DesGrdName;
                }
                else
                {
                    SMSBody = "اطلاعاتی برای شما ثبت نگردیده است";
                }
                #endregion
                break;
            case "R":
            case "r":
                string MsgMemberId = TypePart[0].ToString();
                int MemberId = -1;
                if (int.TryParse(MsgMemberId, out MemberId))
                {
                    if (MemberId == MeId)
                    {
                        MemberManager.FindByCode(MeId);
                        if (MemberManager.Count == 1)
                        {
                            string MeName = MemberManager[0]["MeFullName"].ToString();
                            SMSBody = MeName + " اطلاعات شما در سیستم ثبت گردید.تاریخ و زمان مراجعه به سازمان تعیین و توسط پیامک به شما ارسال خواهد شد.";
                        }
                        else
                            SMSBody = "اطلاعاتی برای شما در سیستم ثبت نگردیده است";
                    }
                    else
                        SMSBody = "کد عضویت ثبت شده در سیستم برای شماره همراه استفاده شده،با کدعضویت ارسال شده در پیامک مطابقت ندارد.جهت دریافت کد عضویت خود عدد 1 را ارسال نمایید..";
                }
                else
                    SMSBody = "کد وارد شده تعریف نشده می باشد.";
                break;
            default:
                SMSBody = "پیام کوتاه شما در سیستم ثبت گردید.";
                //SMSBody += "\n";
                //SMSBody += GenerateSMSHelp();
                break;
        }
        return SMSBody;
    }

    private string GenerateSMSHelp()
    {
        string Help = "";
        Help += "0:انواع پیام کوتاه";
        Help += "\n";
        Help += "1:اطلاعات عضویت";
        Help += "\n";
        Help += "2:اطلاعات پروانه";
        Help += "\n";
        Help += "کد عضویت*r";
        return Help;

    }
    private void SendSMS(int MeId, string MobileNo, string SMSBody, int SMSId, TSP.DataManager.SmsManager SmsManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        string[] SMSInfo = new string[3];
        SMSInfo = Utility.GetSMSWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        string Number = SMSInfo[2];
        ArrayList arlMobileSMS = new ArrayList();
        string[] MobileNumbers = new string[1];
        MobileNumbers[0] = MobileNo;
        string SendType = "1";
        ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();

        string[] SMSResult = SendMessage(BoxService, UserName, PassWord, Number, MobileNumbers, SMSBody, SendType);
        SMSDeliveryReport(BoxService, UserName, PassWord, SMSResult[0], SMSId, SmsManager, TransactionManager);
    }

    private string[] SendMessage(ir.afe.www.BoxService BoxService, string UserName, string PassWord, string Number, string[] MobilesNumbers, string Body, string sendType)
    {
        //ir.afe.www.WebService WebService = new ir.afe.www.WebService();
        // ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
        string[] SMSRerult = BoxService.SendMessage(UserName, PassWord, Number, MobilesNumbers, Body, sendType);
        return SMSRerult;
    }

    private void SendSMSByMagfa(int MessageID, string MobileNo, string SMSBody, int SMSId, TSP.DataManager.SmsManager SmsManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
        string[] SMSInfo = new string[4];
        SMSInfo = Utility.GetMagfaWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        string[] Number = new string[] { SMSInfo[2] };
        string Domain = SMSInfo[3];
        ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
        ssq.PreAuthenticate = true;

        string[] MobileNumbers = new string[1];
        MobileNumbers[0] = MobileNo;

        string[] SMSBodys = new string[1];
        SMSBodys[0] = SMSBody;

        long[] MessageIDs = new long[1];
        MessageIDs[0] = MessageID;

        int[] SendType = new int[1] { 1 };

        int[] encodings;
        string[] UDH;
        int[] mclass;
        encodings = new int[1];
        UDH = new string[1];
        mclass = new int[1];

        long[] SMSResult = ssq.enqueue(Domain, SMSBodys, MobileNumbers, Number, encodings, UDH, SendType, mclass, MessageIDs);
        MagfaSMSDeliveryReport(ssq, UserName, PassWord, SMSResult, SMSId, SmsManager, TransactionManager);
    }

    private void SMSDeliveryReport(ir.afe.www.BoxService BoxService, string UserName, string PassWord, string ReturnedValue, int SMSId, TSP.DataManager.SmsManager SmsManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        Boolean IsSMSSended = false;
        string DeliveryRep = BoxService.GetMessageStatus(UserName, PassWord, ReturnedValue);
        switch (DeliveryRep)
        {
            case "Service is disabled":
                // (int)TSP.DataManager.ErrorSMSRequest.ServiceIsDisabled;
                break;
            case "Username is null or empty":
                // (int)TSP.DataManager.ErrorSMSRequest.UsernameIsNullOrEmpty;
                break;
            case "Password is null or empty":
                //(int)TSP.DataManager.ErrorSMSRequest.PassWordIsNullOrEmpty;
                break;
            case "User Not Enable":
                // (int)TSP.DataManager.ErrorSMSRequest.UserNotEnable;
                break;
            case "username or password is wrong":
                // (int)TSP.DataManager.ErrorSMSRequest.UsernameOrPasswordWrong;
                break;
            case "messageSendID Array is NULL":
                // (int)TSP.DataManager.ErrorSMSRequest.MessageSendIDArrayIsNull;
                break;
            case "more than 10 messageSendID":
                // (int)TSP.DataManager.ErrorSMSRequest.MoreThan10MessageSendID;
                break;
            case "Indeterminate":
                // (int)TSP.DataManager.ErrorSMSRequest.Indeterminate;
                break;
            case "SentToMobile":
                IsSMSSended = true;
                // (int)TSP.DataManager.ErrorSMSRequest.SentToMobile;
                break;
            case "FailedToMobile":
                IsSMSSended = true;
                // (int)TSP.DataManager.ErrorSMSRequest.FailedToMobile;
                break;
            case "SendToComunicationCenter":
                IsSMSSended = true;
                // (int)TSP.DataManager.ErrorSMSRequest.SendToComunicationCenter;
                break;
            case "FailedToComunicationCenter":
                IsSMSSended = true;
                //(int)TSP.DataManager.ErrorSMSRequest.FailedToComunicationCenter;
                break;
            case "Pending":
                // (int)TSP.DataManager.ErrorSMSRequest.Pending;
                IsSMSSended = true;
                break;
            default:
                // (int)TSP.DataManager.ErrorSMSRequest.UnKnown;
                break;
        }

        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count > 0)
        {
            SmsManager[0].BeginEdit();
            if (IsSMSSended)
                SmsManager[0]["IsDelivered"] = 1;
            else
                SmsManager[0]["IsDelivered"] = 0;
            SmsManager[0].EndEdit();
            int coun = SmsManager.Save();
            if (coun > 0)
            {
                TransactionManager.EndSave();
            }
            else
            {
                TransactionManager.CancelSave();
                //"در ارسال پیام کوتاه خطا ایجاد شد";
            }
        }
    }

    private void MagfaSMSDeliveryReport(SMSMagfa.SoapSmsQueuableImplementationService ssq, string UserName, string PassWord, long[] ReturnedValue, int SMSId, TSP.DataManager.SmsManager SmsManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        Boolean IsSMSSended = false;
        int[] DeliveryReport = new int[1];
        if (ReturnedValue[0] > 1000)
        {
            DeliveryReport = ssq.getRealMessageStatuses(ReturnedValue);
            switch (DeliveryReport[0])
            {
                case 1:
                    IsSMSSended = true;
                    break;
                case 2:
                    IsSMSSended = true;
                    break;
                case 8:
                    IsSMSSended = true;
                    break;
                case 16:
                    IsSMSSended = true;
                    break;
                case 0:
                    IsSMSSended = true;
                    break;
            }
        }

        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count > 0)
        {
            SmsManager[0].BeginEdit();
            if (IsSMSSended)
                SmsManager[0]["IsDelivered"] = 1;
            else
                SmsManager[0]["IsDelivered"] = 0;
            SmsManager[0].EndEdit();
            int coun = SmsManager.Save();
            if (coun > 0)
            {
                TransactionManager.EndSave();
            }
            else
            {
                TransactionManager.CancelSave();
                //"در ارسال پیام کوتاه خطا ایجاد شد";
            }
        }
    }


    /// <summary>
    /// CalculateCost[0] = Cost , CalculateCost[1]=CostId
    /// </summary>
    /// <param name="IsEnglish"></param>
    /// <param name="SmsLen"></param>
    /// <returns></returns>
    private string[] CalculateCost(Boolean IsEnglish, int SmsLen)
    {
        string[] SMSCost = new string[2];
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
        DataTable dtSMSType = SmsTypeModifiedManager.SelectByLastModified((int)TSP.DataManager.SMSType.AnswerOfRecieved);
        if (dtSMSType.Rows.Count > 0)
        {
            if (bool.Parse(dtSMSType.Rows[0]["HasCost"].ToString()))
            {
                int ReceiverCount = 1;

                DataTable dtCost = new DataTable();
                if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
                    dtCost = SmsCostManager.FindByWebServiceType(1, 1);
                else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
                    dtCost = SmsCostManager.FindByWebServiceType(0, 1);

                if (dtCost.Rows.Count > 0)
                {
                    double SMSCount = 0;
                    if (IsEnglish)
                    {
                        if (SmsLen > 0)
                        {
                            SMSCount = Math.Ceiling((double)((double)(SmsLen) / 160));
                        }
                        SMSCost[0] = Convert.ToString(((float.Parse(dtCost.Rows[0]["CostEn"].ToString())) * ReceiverCount * SMSCount));
                    }
                    else
                    {
                        if (SmsLen > 0)
                        {
                            SMSCount = Math.Ceiling((double)((double)(SmsLen) / 70));
                        }
                        SMSCost[0] = Convert.ToString(((float.Parse(dtCost.Rows[0]["CostFr"].ToString())) * ReceiverCount * SMSCount));
                    }

                    SMSCost[1] = dtCost.Rows[0]["CostId"].ToString();
                }
                else
                {
                    // this.DivReport.Visible = true;
                    //  this.LabelWarning.Text = "بدلیل نامشخص بودن هزینه پیام کوتاه،امکان ارسال پیام وجود ندارد";
                    SMSCost[0] = "";
                }
            }
            else
            {
                SMSCost[0] = "0";
            }
            return SMSCost;

        }
        else
        {
            //this.DivReport.Visible = true;
            // this.LabelWarning.Text = "اطلاعات نوع پیام توسط کاربر دیگری تغییر یافته است.";
            // SMSCost = "0";
            SMSCost[0] = "0";
            return SMSCost;
        }

    }
}
