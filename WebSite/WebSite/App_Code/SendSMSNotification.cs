using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPrdcoAsync;

public class SendSMSNotification
{
    private Utility.Notifications.NotificationTypes NotificationType;
    private Utility.Notifications Notification;
    private DataTable dtSMSPackets;

    public DataTable NotificationData
    {
        get { return Notification.NotificationData; }
        set { Notification.NotificationData = value; }
    }

    public String SMSBodyFromWorkFlow = "";
    //{
    //    get { return SMSBodyFromWorkFlow; }
    //    set { SMSBodyFromWorkFlow = value; }
    //}

    public Char Splitter { get { return Notification.Splitter; } }

    public SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType)
    {
        this.NotificationType = NotificationType;
        Notification = new Utility.Notifications(NotificationType);
        dtSMSPackets = CreateDataTableSMSPackets();
    }

    public void SendSMSNote(DataTable dtReceivers, string SMSBody)
    {

        if (Utility.IsSmsOff())
            return;
        // SendSMSNotification SMSNotifications = new SendSMSNotification(Utility.Notifications.NotificationTypes.AutomaticSMS);

        if (dtReceivers.Rows.Count <= 0) return;

        String SMSMeId = "", SMSUltId = "", SMSMobileNo = "", SMSResult = "";
        for (int j = 0; j < dtReceivers.Rows.Count; j++)
        {
            if (!Utility.IsDBNullOrNullValue(dtReceivers.Rows[j]["SMSMobileNo"]) && !String.IsNullOrWhiteSpace(dtReceivers.Rows[j]["SMSMobileNo"].ToString().Trim()))
            {
                if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
                {
                    SMSMobileNo += Splitter;
                    SMSMeId += Splitter;
                    SMSUltId += Splitter;
                    SMSResult += Splitter;
                }
                SMSMeId += dtReceivers.Rows[j]["SMSMeId"].ToString();
                SMSUltId += dtReceivers.Rows[j]["SMSUltId"].ToString();
                SMSMobileNo += dtReceivers.Rows[j]["SMSMobileNo"].ToString();
            }
        }



        if (string.IsNullOrEmpty(SMSBody)) return;
        SMSBodyFromWorkFlow = SMSBody;

        if (!String.IsNullOrWhiteSpace(SMSMobileNo))
        {
            DataRow dr = NotificationData.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSResult"] = SMSResult;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = SMSUltId;

            NotificationData.Rows.Add(dr);
            NotificationData.AcceptChanges();

            switch (Utility.GetCurrentSMSWebService())
            {
                case (int)TSP.DataManager.SMSWebServiceType.Magfa:
                    SendSMSByMagfa();
                    break;
                case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
                    SendSMS();
                    break;
                case (int)TSP.DataManager.SMSWebServiceType.Prdco:
                    SendSMSByPrdco();
                    break;
            }
        }
    }

    private DataTable CreateDataTableSMSPackets()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReIds");
        dt.Columns.Add("MobileNos");
        dt.Columns.Add("SMSResult");
        return dt;
    }

    private Boolean CreateSMSPackets(int NotificationDataIndex)
    {
        String[] arrRecieverCellPhones = NotificationData.Rows[NotificationDataIndex]["SMSMobileNo"].ToString().Split(Notification.Splitter);

        if (arrRecieverCellPhones.Length > 0)
        {
            int RecieverCount = arrRecieverCellPhones.Length;
            int PackCount = Utility.GetSMSPacketSize();
            int SubRecieverCount = (PackCount != -1) ? (RecieverCount / PackCount) + 1 : RecieverCount;
            int StartPosition = 0;
            int EndPosition = 0;
            int RemainSMSCount = RecieverCount;
            int SendedCount = 0;

            for (int j = 0; j < SubRecieverCount; j++)
            {
                if (RemainSMSCount < PackCount)
                    EndPosition = RemainSMSCount;
                else
                    EndPosition = PackCount;

                DataRow dr = dtSMSPackets.NewRow();
                dr["MobileNos"] = String.Join(";", arrRecieverCellPhones, StartPosition, EndPosition);
                dr["SMSResult"] = "";
                dtSMSPackets.Rows.Add(dr);

                StartPosition = StartPosition + PackCount;
                SendedCount = SendedCount + EndPosition;
                RemainSMSCount = RecieverCount - SendedCount;
            }

            dtSMSPackets.AcceptChanges();
            return true;
        }
        return false;
    }

    public Boolean SendSMS()
    {
        try
        {
            Boolean SendStatus = false; // Check Sms send, for SmsManager

            for (int Index = 0; Index < Notification.NotificationData.Rows.Count; Index++)
            {
                if (CreateSMSPackets(Index) == false)
                    return false;

                String[] ErrorInSendingSMS = new String[Utility.GetSMSPacketSize()];
                for (int i = 0; i < Utility.GetSMSPacketSize(); i++)
                    ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();

                if (dtSMSPackets.Rows.Count == 0)
                    return false;

                string[] SMSInfo = new string[3];
                SMSInfo = Utility.GetSMSWebServiceInformation();
                string UserName = SMSInfo[0];
                string Password = SMSInfo[1];
                string Number = SMSInfo[2];

                //*****SendType=0 SMS don't save In MemoryCard,SendType=1 SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
                string SendType = "1";

                string SMSBody = "";
                if (string.IsNullOrEmpty(SMSBodyFromWorkFlow))
                    SMSBody = Notification.getSMSBody(Index);
                else
                    SMSBody = SMSBodyFromWorkFlow;
                //   string SMSBody = Notification.getSMSBody(Index);

                ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
                int SentPacketAfterLastThreadSleep = 0;

                for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
                {
                    if (SentPacketAfterLastThreadSleep * Utility.GetSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                    {
                        SentPacketAfterLastThreadSleep = 0;
                        System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                    }

                    string[] SMSResult = new String[Utility.GetSMSPacketSize()];
                    Boolean SmsSend = false;
                    int Counter = 0;
                    do
                    {
                        try
                        {
                            SMSResult = BoxService.SendMessage(UserName, Password, Number, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(Notification.Splitter), SMSBody, SendType);
                            SmsSend = true;
                        }
                        catch (Exception err)
                        {
                            SmsSend = false;
                            Utility.SaveWebsiteError(err);
                        }
                        Counter++;
                    } while (SmsSend == false && Counter <= 5);

                    if (SmsSend)
                    {
                        if (SendStatus == false)
                        {
                            for (int j = 0; j < SMSResult.Length; j++)
                            {
                                if (String.IsNullOrEmpty(SMSResult[j].ToString().Trim()) == false)
                                {
                                    String Result = Utility.GetAFESmsResult(SMSResult[j]).Trim();
                                    if (String.IsNullOrEmpty(Result))
                                    {
                                        SendStatus = true;
                                        break;
                                    }
                                    else
                                    {
                                        //WebServiceMessage = Result;
                                        return false;
                                    }
                                }
                            }
                        }

                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResult);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    else
                    {
                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(Notification.Splitter).Length);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    SentPacketAfterLastThreadSleep++;
                }
                dtSMSPackets.AcceptChanges();
            }

            if (SendStatus)
            {
                if (SaveSMS() == false)
                    return false;
            }
            else
                return false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }

        return true;
    }

    public Boolean SendSMSByMagfa()
    {
        try
        {
            Boolean SendStatus = false; // Check Sms send, for SmsManager

            for (int Index = 0; Index < Notification.NotificationData.Rows.Count; Index++)
            {
                if (CreateSMSPackets(Index) == false)
                    return false;

                String[] ErrorInSendingSMS = new String[Utility.GetSMSPacketSize()];
                for (int i = 0; i < Utility.GetSMSPacketSize(); i++)
                    ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();

                if (dtSMSPackets.Rows.Count == 0)
                    return false;

                SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
                string[] SMSInfo = new string[2];
                SMSInfo = Utility.GetMagfaWebServiceInformation();
                string UserName = SMSInfo[0];
                string PassWord = SMSInfo[1];
                string[] Number = new string[] { SMSInfo[2] };
                string Domain = SMSInfo[3];

                int[] encodings;
                string[] UDH;
                int[] mclass;
                encodings = new int[1];
                UDH = new string[1];
                mclass = new int[1];

                ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
                ssq.PreAuthenticate = true;

                //*****SendType=0 SMS don't save In MemoryCard,SendType=1 SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
                int[] SendType = new int[] { 1 };

                string SMSBody = "";
                if (string.IsNullOrEmpty(SMSBodyFromWorkFlow))
                    SMSBody = Notification.getSMSBody(Index);
                else
                    SMSBody = SMSBodyFromWorkFlow;

                string[] ArrSMSBody = new string[] { SMSBody };

                int SentPacketAfterLastThreadSleep = 0;

                for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
                {
                    if (SentPacketAfterLastThreadSleep * Utility.GetMagfaSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                    {
                        SentPacketAfterLastThreadSleep = 0;
                        System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                    }

                    long[] SMSResultMagfa = new long[Utility.GetMagfaSMSPacketSize()];
                    long[] MessageIDs = new long[Utility.GetMagfaSMSPacketSize()];
                    Boolean SmsSend = false;

                    //  MessageIDs = Array.ConvertAll<string, long>(dtSMSPackets.Rows[i]["ReIds"].ToString().Split(';'), long.Parse);

                    int Counter = 0;
                    do
                    {
                        try
                        {
                            SMSResultMagfa = ssq.enqueue(Domain, ArrSMSBody, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';'),
                                                Number, encodings, UDH, SendType, mclass, MessageIDs);
                            SmsSend = true;
                        }
                        catch (Exception err)
                        {
                            SmsSend = false;
                            Utility.SaveWebsiteError(err);
                        }
                        Counter++;
                    } while (SmsSend == false && Counter <= 5);

                    if (SmsSend)
                    {
                        if (!SendStatus)
                        {
                            for (int j = 0; j < SMSResultMagfa.Length; j++)
                            {
                                if (!String.IsNullOrEmpty(SMSResultMagfa[j].ToString().Trim()))
                                {
                                    if (SMSResultMagfa.Where(item => item > 1000).Count() > 0)
                                        SendStatus = true;
                                    else
                                    {
                                        long[] ArrayNotSend = SMSResultMagfa.Where(item => item < 1000).ToArray();
                                        string Result = Utility.GetMagfaSmsResult(Convert.ToInt32(ArrayNotSend[0]));
                                    }
                                }
                            }
                        }

                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResultMagfa);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    else
                    {
                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(Notification.Splitter).Length);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    SentPacketAfterLastThreadSleep++;
                }
                dtSMSPackets.AcceptChanges();
            }

            if (SendStatus)
            {
                if (SaveSMS() == false)
                    return false;
            }
            else
                return false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }

        return true;
    }

    public Boolean SendSMSByPrdco()
    {
        try
        {
            Boolean SendStatus = false; // Check Sms send, for SmsManager

            for (int Index = 0; Index < Notification.NotificationData.Rows.Count; Index++)
            {
                if (CreateSMSPackets(Index) == false)
                    return false;

                String[] ErrorInSendingSMS = new String[Utility.GetSMSPacketSize()];
                String[] SMSResult = new String[Utility.GetSMSPacketSize()];
                for (int i = 0; i < Utility.GetSMSPacketSize(); i++)
                    ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();

                if (dtSMSPackets.Rows.Count == 0)
                    return false;

                SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();
                string[] SMSInfo = new string[2];
                SMSInfo = Utility.GetPrdcoWebServiceInformation();
                string UserName = SMSInfo[0];
                string PassWord = SMSInfo[1];
                string Number = SMSInfo[2];

                //*****SendType=true SMS don't save In MemoryCard,SendType=false SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
                Boolean SendType = false;

                string SMSBody = "";
                if (string.IsNullOrEmpty(SMSBodyFromWorkFlow))
                    SMSBody = Notification.getSMSBody(Index);
                else
                    SMSBody = SMSBodyFromWorkFlow;



                int SentPacketAfterLastThreadSleep = 0;

                for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
                {
                    if (SentPacketAfterLastThreadSleep * Utility.GetPrdcoSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                    {
                        SentPacketAfterLastThreadSleep = 0;
                        System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                    }

                    SMSPrdcoAsync.ArrayOfString SMSResultPrdco = new SMSPrdcoAsync.ArrayOfString();
                    long[] MessageIDsPrdco = new long[Utility.GetPrdcoSMSPacketSize()];
                    Boolean SmsSend = false;

                    int Counter = 0;
                    do
                    {
                        try
                        {
                            SMSResultPrdco = sendSoapClient.SendSms(UserName, PassWord, Number, GetAllMobileNo(dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';')), SMSBody, SendType);

                            SmsSend = true;
                        }
                        catch (Exception err)
                        {
                            SmsSend = false;
                            Utility.SaveWebsiteError(err);
                        }
                        Counter++;
                    } while (SmsSend == false && Counter <= 5);

                    if (SmsSend)
                    {
                        if (!SendStatus)
                        {
                            for (int j = 0; j < SMSResultPrdco.Count; j++)
                            {
                                if (!String.IsNullOrEmpty(SMSResultPrdco[j].ToString().Trim()))
                                {
                                    SMSResult[j] = SMSResultPrdco[j].ToString().Trim();
                                    if (SMSResultPrdco.Where(item => Convert.ToInt64(item) > 0).Count() > 0)
                                        SendStatus = true;
                                    else
                                    {
                                        string[] ArrayNotSend = SMSResultPrdco.Where(item => Convert.ToInt64(item) < 0).ToArray();

                                    }
                                }
                            }
                        }

                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResult);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    else
                    {
                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(Notification.Splitter).Length);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    SentPacketAfterLastThreadSleep++;
                }
                dtSMSPackets.AcceptChanges();
            }

            if (SendStatus)
            {
                if (SaveSMS() == false)
                    return false;
            }
            else
                return false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }

        return true;

    }
    public Boolean SendSMSByPrdcoAsync()
    {
        try
        {
            //  Boolean SendStatus = false; // Check Sms send, for SmsManager

            for (int Index = 0; Index < Notification.NotificationData.Rows.Count; Index++)
            {
                if (CreateSMSPackets(Index) == false)
                    return false;

                if (dtSMSPackets.Rows.Count == 0)
                    return false;

                string[] SMSInfo = new string[2];
                SMSInfo = Utility.GetPrdcoWebServiceInformation();
                string UserName = SMSInfo[0];
                string PassWord = SMSInfo[1];
                string Number = SMSInfo[2];


                string SMSBody = "";
                if (string.IsNullOrEmpty(SMSBodyFromWorkFlow))
                    SMSBody = Notification.getSMSBody(Index);
                else
                    SMSBody = SMSBodyFromWorkFlow;


                int SentPacketAfterLastThreadSleep = 0;

                for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
                {
                    if (SentPacketAfterLastThreadSleep * Utility.GetPrdcoSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                    {
                        SentPacketAfterLastThreadSleep = 0;
                        System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                    }

                    Boolean SmsSend = false;

                    int Counter = 0;
                    do
                    {
                        try
                        {
                            SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();
                            sendSoapClient.SendSmsCompleted += new EventHandler<SMSPrdcoAsync.SendSmsCompletedEventArgs>((sender, e) => GetResultSendSmsPrdco(sender, e, Notification));
                            sendSoapClient.SendSmsAsync(UserName, PassWord, Number, GetAllMobileNo(dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';')), SMSBody, false);

                            SmsSend = true;
                        }
                        catch (Exception err)
                        {
                            SmsSend = false;
                            Utility.SaveWebsiteError(err);
                        }
                        Counter++;
                    } while (SmsSend == false && Counter <= 5);

                    SentPacketAfterLastThreadSleep++;
                }

            }

            if (SaveSMS() == false)
                return false;

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }

        return true;
    }

    private void GetResultSendSmsPrdco(object sender, SendSmsCompletedEventArgs e, Utility.Notifications Notification)
    {
        SMSPrdcoAsync.ArrayOfString SMSResultPrdco = new SMSPrdcoAsync.ArrayOfString();
        SMSResultPrdco = e.Result;
        DataTable dt = Notification.NotificationData;

        //        for (int j = 0; j < SMSResultPrdco.Length; j++)
        //        {
        //            if (!String.IsNullOrEmpty(SMSResultPrdco[j].ToString().Trim()))
        //            {
        //                if (SMSResultPrdco.Where(item => item > 0).Count() > 0)
        //                    SendStatus = true;
        //                else
        //                {
        //                    long[] ArrayNotSend = SMSResultPrdco.Where(item => item < 0).ToArray();
        //                
        //                }
        //            }
        //        }


        //    dtSMSPackets.Rows[i].BeginEdit();
        //    dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResultPrdco);
        //    dtSMSPackets.Rows[i].EndEdit();

        //   dtSMSPackets.AcceptChanges();
    }

    private Boolean SaveSMS()
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(SmsManager);
        TransactionManager.Add(SmsRecieverManager);

        TransactionManager.BeginSave();
        try
        {
            for (int Index = 0; Index < Notification.NotificationData.Rows.Count; Index++)
            {
                int CurrentUserId = Utility.GetCurrentUser_MeId();
                int CurrentMeId = Utility.GetCurrentUser_UserId();
                String[] MeIds = Notification.NotificationData.Rows[Index]["SMSMeId"].ToString().Split(Notification.Splitter);
                String[] UltIds = Notification.NotificationData.Rows[Index]["SMSUltId"].ToString().Split(Notification.Splitter);
                String[] MobileNos = Notification.NotificationData.Rows[Index]["SMSMobileNo"].ToString().Split(Notification.Splitter);
                if (CurrentUserId <= 0)
                {
                    CurrentMeId = Convert.ToInt32(MeIds[0]);
                    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
                    LoginManager.FindByMeIdUltId(CurrentMeId, Convert.ToInt32(UltIds[0]));
                    if (LoginManager.Count <= 0)
                    {
                        TransactionManager.CancelSave();
                        return false;
                    }
                    CurrentUserId = Convert.ToInt32(LoginManager[0]["UserId"]);
                }
                DataRow dtSMSRow = SmsManager.NewRow();
                string SMSBodey = "";
                if (string.IsNullOrEmpty(SMSBodyFromWorkFlow))
                    SMSBodey = Notification.getSMSBody(Index);
                else
                    SMSBodey = SMSBodyFromWorkFlow;

                dtSMSRow["ExpireDate"] = Utility.GetDateOfToday();
                dtSMSRow["SMSDate"] = Utility.GetDateOfToday();
                dtSMSRow["SMSTime"] = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                dtSMSRow["IsFarsi"] = 1;
                int SMSBodyLenght = SMSBodey.Length;
                if (SMSBodyLenght > 0)
                {
                    dtSMSRow["SmsCount"] = Math.Ceiling((double)((double)(SMSBodey.Length) / 70));
                }
                else
                {
                    TransactionManager.CancelSave();
                    return false;
                }

                int CostId = 0;
                double smsCost = CalculateCost(Index, SMSBodyLenght, ref CostId);
                if (smsCost == 0 || CostId == 0)
                {
                    TransactionManager.CancelSave();
                    return false;
                }
                dtSMSRow["CostId"] = CostId;

                dtSMSRow["SmsCost"] = (int.Parse(dtSMSRow["SmsCount"].ToString())) * (smsCost);
                dtSMSRow["SmsTypeId"] = 4;
                dtSMSRow["SenderId"] = CurrentMeId;
                dtSMSRow["SmsSubject"] = Notification.getSMSSubject();
                dtSMSRow["SmsBody"] = SMSBodey;
                dtSMSRow["IsDelivered"] = 0;
                dtSMSRow["InActive"] = 0;
                dtSMSRow["ModifiedDate"] = DateTime.Now;
                dtSMSRow["UserId"] = CurrentUserId;

                SmsManager.AddRow(dtSMSRow);
                if (SmsManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    return false;
                }
                SmsManager.DataTable.AcceptChanges();
                int SMSId = Convert.ToInt32(SmsManager[0]["SmsId"]);

                for (int i = 0; i < MeIds.Length; i++)
                {
                    DataRow dtSMSRecRow = SmsRecieverManager.NewRow();
                    dtSMSRecRow["SmsId"] = SMSId;
                    dtSMSRecRow["RecieverId"] = MeIds[i];
                    if (String.IsNullOrWhiteSpace(MobileNos[i]) == false)
                    {
                        dtSMSRecRow["RecieverCellPhone"] = Notification.NotificationData.Rows[Index]["SMSMeId"].ToString();
                    }
                    if (Convert.ToInt32(UltIds[i]) == (int)TSP.DataManager.UserType.Employee)
                        dtSMSRecRow["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee;
                    else if (Convert.ToInt32(UltIds[i]) == (int)TSP.DataManager.UserType.Member)
                        dtSMSRecRow["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member;
                    else
                        dtSMSRecRow["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.OtherPerson;
                    dtSMSRecRow["InActive"] = 0;
                    dtSMSRecRow["ModifiedDate"] = DateTime.Now;
                    dtSMSRecRow["UserId"] = CurrentUserId;
                    dtSMSRecRow["IsDelivered"] = 0;
                    SmsRecieverManager.AddRow(dtSMSRecRow);
                }

                int cnt = SmsRecieverManager.Save();
                if (cnt < 0)
                {
                    TransactionManager.CancelSave();
                    return false;
                }

            }

            TransactionManager.EndSave();
            return true;
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            return false;
        }

    }

    private double CalculateCost(int NotificationDataIndex, int SMSBodyLenght, ref int CostId)
    {
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
        double SmsCosts = 0;

        int SMSTypeId = 1;
        DataTable dtSMSType = SmsTypeModifiedManager.SelectByLastModified(SMSTypeId);
        if (dtSMSType.Rows.Count > 0)
        {
            if (bool.Parse(dtSMSType.Rows[0]["HasCost"].ToString()))
            {
                int ReceiverCount = Notification.NotificationData.Rows[NotificationDataIndex]["SMSMobileNo"].ToString().Split(Notification.Splitter).Length;

                DataTable dtCost = SmsCostManager.SelectActiveSMSCost();
                if (dtCost.Rows.Count > 0)
                {
                    CostId = Convert.ToInt32(dtCost.Rows[0]["CostId"]);
                    double SMSCount = 0;

                    if (SMSBodyLenght > 0)
                    {
                        SMSCount = Math.Ceiling((double)((double)(SMSBodyLenght) / 70));
                    }
                    SmsCosts = ((float.Parse(dtCost.Rows[0]["CostFr"].ToString())) * ReceiverCount * SMSCount);
                }
                else
                {
                    SmsCosts = 0;
                }
            }
            else
            {
                SmsCosts = 0;
            }
            return SmsCosts;

        }
        return ((float)(0));
    }
    private SMSPrdcoAsync.ArrayOfString GetAllMobileNo(string[] MobileNos)
    {
        SMSPrdcoAsync.ArrayOfString AllMobileNo = new SMSPrdcoAsync.ArrayOfString();
        for (int i = 0; i < MobileNos.Length; i++)
        {
            AllMobileNo.Add(MobileNos[i]);
        }
        return AllMobileNo;
    }
}
