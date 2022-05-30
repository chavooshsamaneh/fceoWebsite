using System;
using System.Data;
using System.Linq;

public partial class Employee_SMS_SendSMS : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            int SMSId = 0;
            try
            {
                if (Request.QueryString["SMSId"] == null || String.IsNullOrWhiteSpace(Request.QueryString["SMSId"]))
                    Response.Redirect("ConfirmedSMS.aspx");
                SMSId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SMSId"]));
                if (SMSId <= 0)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            if (CheckWorkFlowPermissionForSendSMS() == false)
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());

            Load_Data(SMSId);
            btnSave.Visible = false;

            //ViewState["DataTableSMSPackets"] = "";
            ViewState["SMSId"] = SMSId;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        btnCancel.ClientEnabled = true;
        btnSend.ClientEnabled = true;
        btnSave.Visible = false;

        int SMSId = -1;
        if (ViewState["SMSId"] != null)
            SMSId = Convert.ToInt32(ViewState["SMSId"]);
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        if (SMSId != -1)
        {
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1)
            {
                if (Convert.ToBoolean(SmsManager[0]["IsDelivered"]) == true)
                {
                    lblConfirmSendSMS.Text = "";
                    lblConfirmSendSMS.Text += "به دلیل وجود مشکل در برقراری ارتباط با سرور مگفا، تعدادی از پیامک ها ارسال نشده است"
                        + "<br><br>" + "برای ذخیره وضعیت پیامک های ارسالی، برروی دکمه ذخیره کلیک نمایید" + "<br><br>";
                    lblConfirmSendSMS.Text += "هشدار: تنها تا 24 ساعت پس از ارسال پیامک امکان دریافت وضعیت آنها وجود دارد";
                    btnSave.Visible = true;
                    btnSend.Visible = false;
                }
            }
        }

        String WebServiceMessage = "", StrReId_NotSend = "", StrMobileNo_NotSend = "";
        switch (Utility.GetCurrentSMSWebService())
        {
            case (int)TSP.DataManager.SMSWebServiceType.Magfa:
                if (SendSMSByMagfa(ref WebServiceMessage, ref StrReId_NotSend, ref StrMobileNo_NotSend) == false)
                {
                    if (String.IsNullOrWhiteSpace(WebServiceMessage))
                        ShowMessage("خطایی در ارسال پیامک به سرور ایجاد گردیده است", System.Drawing.Color.Red);
                    else
                    {
                        ShowMessage(WebServiceMessage, System.Drawing.Color.Red);
                    }
                }
                else
                {
                    btnSend.Visible = false;
                    btnSave.Visible = true;
                }
                break;
            case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
                if (SendSMSByAFE(ref WebServiceMessage, ref StrReId_NotSend, ref StrMobileNo_NotSend) == false)
                {
                    if (String.IsNullOrWhiteSpace(WebServiceMessage))
                        ShowMessage("خطایی در ارسال پیامک به سرور ایجاد گردیده است", System.Drawing.Color.Red);
                    else
                        ShowMessage(WebServiceMessage, System.Drawing.Color.Red);
                }
                else
                {
                    btnSend.Visible = false;
                    btnSave.Visible = true;
                }
                break;
            case (int)TSP.DataManager.SMSWebServiceType.Prdco:
                if (SendSMSByPrdco(ref WebServiceMessage, ref StrReId_NotSend, ref StrMobileNo_NotSend) == false)
                {
                    if (String.IsNullOrWhiteSpace(WebServiceMessage))
                        ShowMessage("خطایی در ارسال پیامک به سرور ایجاد گردیده است", System.Drawing.Color.Red);
                    else
                        ShowMessage(WebServiceMessage, System.Drawing.Color.Red);
                }
                else
                {
                    btnSend.Visible = false;
                    btnSave.Visible = true;
                }
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnCancel.ClientEnabled = true;
        btnSave.ClientEnabled = true;
        switch (Utility.GetCurrentSMSWebService())
        {
            case (int)TSP.DataManager.SMSWebServiceType.Magfa:
                if (SaveMagfaSMSDelivery() == false)
                    ShowMessage("خطایی در ذخیره ایجاد گردیده است", System.Drawing.Color.Red);
                break;
            case (int)TSP.DataManager.SMSWebServiceType.Prdco:
                if (SaveMagfaSMSDelivery() == false)
                    ShowMessage("خطایی در ذخیره ایجاد گردیده است", System.Drawing.Color.Red);
                break;
            case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
                if (SaveSMSDelivery() == false)
                    ShowMessage("خطایی در ذخیره ایجاد گردیده است", System.Drawing.Color.Red);
                break;
        }
    }
    #endregion

    #region Methods
    private Boolean CheckWorkFlowPermissionForSendSMS()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS;
        int TableType = (int)TSP.DataManager.TableCodes.SMS;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    void Load_Data(int SMSId)
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count == 1 && !(Convert.ToBoolean(SmsManager[0]["InActive"])))
        {
            ////if (Convert.ToInt32(SmsManager[0]["IsDelivered"]) == 1)
            ////{
            ////    ShowMessage("پیامک انتخاب شده، ارسال شده است", System.Drawing.Color.Red);
            ////    return;
            ////}
            //// else
            {
                String SMSExpireDate = SmsManager[0]["ExpireDate"].ToString();
                String DateNow = Utility.GetDateOfToday();
                int IsExpired = String.Compare(SMSExpireDate, DateNow);
                if (String.Compare(SMSExpireDate, DateNow) < 0)
                {
                    ShowMessage("مهلت اعتبار پیامک به پایان رسیده است", System.Drawing.Color.Red);
                    return;
                }
            }
        }
        else
        {
            ShowMessage("خطایی در خواندن اطلاعات ایجاد گردیده است", System.Drawing.Color.Red);
            return;
        }

        try
        {
            lblSmsCost.Text = SmsManager[0]["SmsCost"].ToString() + " ریال";
            lblSmsSubject.Text = SmsManager[0]["SmsSubject"].ToString();
            lblSmsBody.Text = SmsManager[0]["SmsBody"].ToString();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = "خطایی در ایجاد بسته های پیامک ایجاد گردیده است";
            if (Utility.ShowExceptionError())
                Error += "<br>" + err.Message;
            ShowMessage(Error, System.Drawing.Color.Red);
            return;
        }
        LoadCredit();
        CreateSMSPackets(SMSId);
    }

    DataTable CreateDataTableSMSPackets()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReIds");
        dt.Columns.Add("MobileNos");
        dt.Columns.Add("SMSResult");
        return dt;
    }

    void CreateSMSPackets(int SMSId)
    {
        try
        {
            TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
            DataTable dtRecievers = SmsRecieverManager.FindBySMSId_StringMode(SMSId);
            if (dtRecievers.Rows.Count > 0)
            {
                String[] arrRecieverCellPhones = dtRecievers.Rows[0]["RecieverCellPhones"].ToString().Split(';');
                String[] arrSmsReIds = dtRecievers.Rows[0]["SmsReIds"].ToString().Split(';');

                if (arrSmsReIds.Length > 0)
                {
                    int RecieverCount = arrSmsReIds.Length;
                    int PackCount = -1;

                    if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
                        PackCount = Utility.GetMagfaSMSPacketSize();
                    if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Prdco || Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.PrdcoAsync)
                        PackCount = Utility.GetPrdcoSMSPacketSize();
                    if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
                        PackCount = Utility.GetSMSPacketSize();

                    int SubRecieverCount = (PackCount != -1) ? (RecieverCount / PackCount) + 1 : RecieverCount;
                    int StartPosition = 0;
                    int EndPosition = 0;
                    int RemainSMSCount = RecieverCount;
                    int SendedCount = 0;
                    DataTable dtSMSPackets = CreateDataTableSMSPackets();

                    for (int j = 0; j < SubRecieverCount; j++)
                    {
                        if (RemainSMSCount < PackCount)
                            EndPosition = RemainSMSCount;
                        else
                            EndPosition = PackCount;

                        DataRow dr = dtSMSPackets.NewRow();
                        dr["ReIds"] = String.Join(";", arrSmsReIds, StartPosition, EndPosition);
                        dr["MobileNos"] = String.Join(";", arrRecieverCellPhones, StartPosition, EndPosition);
                        dr["SMSResult"] = "";
                        dtSMSPackets.Rows.Add(dr);

                        StartPosition = StartPosition + PackCount;
                        SendedCount = SendedCount + EndPosition;
                        RemainSMSCount = RecieverCount - SendedCount;
                    }

                    dtSMSPackets.AcceptChanges();
                    ViewState["DataTableSMSPackets"] = dtSMSPackets;

                    lblConfirmSendSMS.Text = "با ارسال " + RecieverCount + " پیامک موافق هستید؟";
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = "خطایی در ایجاد بسته های پیامک ایجاد گردیده است";
            if (Utility.ShowExceptionError())
                Error += "<br>" + err.Message;
            ShowMessage(Error, System.Drawing.Color.Red);
        }
    }

    Boolean SendSMSByMagfa(ref String WebServiceMessage, ref String StrReId_NotSend, ref String StrMobileNo_NotSend)
    {
        SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        int CountSmsForReport = 0;
        int SMSId = 0;
        Boolean SmsSend = false;
        Boolean SendStatus = false; // Check Sms send, for SmsManager
        try
        {
            #region  Create DataTableSMSPackets
            if (ViewState["DataTableSMSPackets"] == null)
                return false;
            if (ViewState["SMSId"] == null)
                return false;
            SMSId = Convert.ToInt32(ViewState["SMSId"]);

            DataTable dtSMSPackets = (DataTable)ViewState["DataTableSMSPackets"];

            String[] ErrorInSendingSMS = new String[Utility.GetMagfaSMSPacketSize()];
            for (int i = 0; i < Utility.GetMagfaSMSPacketSize(); i++)
                ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();
            #endregion

            if (dtSMSPackets.Rows.Count == 0)
                return false;

            #region Define Magfa Array Parameters
            //*****SendType=0 SMS don't save In MemoryCard,SendType=1 SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
            int[] SendType = new int[] { 1 };

            long[] SMSResultMagfa = new long[Utility.GetMagfaSMSPacketSize()];
            long[] MessageIDs = new long[Utility.GetMagfaSMSPacketSize()];

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
            #endregion

            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count != 1 || Convert.ToBoolean(SmsManager[0]["InActive"]))
                return false;
            string[] SMSBody = new string[] { SmsManager[0]["SmsBody"].ToString() };

            int SentPacketAfterLastThreadSleep = 0;

            for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
            {
                #region For
                if (SentPacketAfterLastThreadSleep * Utility.GetMagfaSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                {
                    SentPacketAfterLastThreadSleep = 0;
                    System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                }

                MessageIDs = Array.ConvertAll<string, long>(dtSMSPackets.Rows[i]["ReIds"].ToString().Split(';'), long.Parse);

                int Counter = 0;
                do
                {
                    try
                    {
                        SMSResultMagfa = ssq.enqueue(Domain, SMSBody, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';'),
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
                    CountSmsForReport += SMSResultMagfa.Length;
                    dtSMSPackets.Rows[i].BeginEdit();
                    dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResultMagfa);
                    dtSMSPackets.Rows[i].EndEdit();
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(StrReId_NotSend) == false)
                    {
                        StrReId_NotSend += ";";
                        StrMobileNo_NotSend += ";";
                    }
                    StrMobileNo_NotSend += dtSMSPackets.Rows[i]["MobileNos"].ToString();
                    StrReId_NotSend += dtSMSPackets.Rows[i]["ReIds"].ToString();

                    dtSMSPackets.Rows[i].BeginEdit();
                    dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';').Length);
                    dtSMSPackets.Rows[i].EndEdit();
                }
                SentPacketAfterLastThreadSleep++;

                #region //****چک می شود آیا حداقل یک پیامک ارسال شده باشد
                if (!SendStatus)
                {
                    if (SMSResultMagfa.Where(item => item > 1000).Count() > 0)
                        SendStatus = true;
                    else
                    {
                        long[] ArrayNotSend = SMSResultMagfa.Where(item => item < 1000).ToArray();
                        string Result = Utility.GetMagfaSmsResult(Convert.ToInt32(ArrayNotSend[0]));
                        WebServiceMessage = Result;
                    }
                }
                #endregion
                #endregion
            }//---for---

            dtSMSPackets.AcceptChanges();
            ViewState["DataTableSMSPackets"] = dtSMSPackets;

            if (SendStatus)
            {
                SmsManager.FindByCode(SMSId);
                if (SmsManager.Count == 0)
                {
                    return false;
                }
                SmsManager[0].BeginEdit();
                SmsManager[0]["SMSSendDate"] = Utility.GetDateOfToday();
                SmsManager[0]["IsDelivered"] = 1;
                SmsManager[0]["SenderId"] = Utility.GetCurrentUser_UserId();
                SmsManager[0].EndEdit();
                if (SmsManager.Save() <= 0)
                    return false;
            }
            else return false;

        }
        catch (Exception err)
        {
            #region catch
            Utility.SaveWebsiteError(err);
            if (SendStatus)
            {
                SmsManager.FindByCode(SMSId);
                if (SmsManager.Count == 0)
                {
                    return false;
                }
                SmsManager[0].BeginEdit();
                SmsManager[0]["SMSSendDate"] = Utility.GetDateOfToday();
                SmsManager[0]["IsDelivered"] = 1;
                SmsManager[0]["SenderId"] = Utility.GetCurrentUser_UserId();
                SmsManager[0].EndEdit();
                if (SmsManager.Save() <= 0)
                    return false;
            }
            else
                return false;
            #endregion
        }

        lblConfirmSendSMS.Text = "";
        if (String.IsNullOrWhiteSpace(StrMobileNo_NotSend) == false)
            lblConfirmSendSMS.Text = StrMobileNo_NotSend.Split(';').Length + " پیامک به دلیل وجود مشکل در برقراری ارتباط باسرور ، ارسال نشد<br><br>";

        lblConfirmSendSMS.Text += CountSmsForReport + " پیامک به سرور پیام کوتاه ارسال شد" + "<br><br>" + "برای ذخیره وضعیت پیامک های ارسالی، برروی دکمه ذخیره کلیک نمایید" + "<br><br>";
        lblConfirmSendSMS.Text += "هشدار: تنها تا 24 ساعت پس از ارسال پیامک امکان دریافت وضعیت آنها وجود دارد";
        btnSave.Visible = true;
        btnSend.Visible = false;
        return true;
    }

    Boolean SendSMSByAFE(ref String WebServiceMessage, ref String StrReId_NotSend, ref String StrMobileNo_NotSend)
    {
        int CountSmsForReport = 0;
        int SMSId = 0;
        try
        {
            if (ViewState["DataTableSMSPackets"] == null)
                return false;
            if (ViewState["SMSId"] == null)
                return false;
            SMSId = Convert.ToInt32(ViewState["SMSId"]);

            DataTable dtSMSPackets = (DataTable)ViewState["DataTableSMSPackets"];

            String[] ErrorInSendingSMS = new String[Utility.GetSMSPacketSize()];
            for (int i = 0; i < Utility.GetSMSPacketSize(); i++)
                ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();

            if (dtSMSPackets.Rows.Count == 0)
                return false;

            //*****SendType=0 SMS don't save In MemoryCard,SendType=1 SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
            string SendType = "1";
            string[] SMSResultAFE = new String[Utility.GetSMSPacketSize()];
            string[] SMSInfo = new string[3];
            SMSInfo = Utility.GetSMSWebServiceInformation();
            string AFEUserName = SMSInfo[0];
            string AFEPassword = SMSInfo[1];
            string AFENumber = SMSInfo[2];
            ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();

            TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1 && !(Convert.ToBoolean(SmsManager[0]["InActive"])))
            {
                Boolean SendStatus = false; // Check Sms send, for SmsManager
                string SMSBody = SmsManager[0]["SmsBody"].ToString();

                int SentPacketAfterLastThreadSleep = 0;

                for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
                {
                    if (SentPacketAfterLastThreadSleep * Utility.GetSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
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
                            SMSResultAFE = BoxService.SendMessage(AFEUserName, AFEPassword, AFENumber,
                                dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';'), SMSBody, SendType);
                            SmsSend = true;
                        }
                        catch (Exception err)
                        {
                            SmsSend = false;
                            Utility.SaveWebsiteError(err);
                            lblConfirmSendSMS.Text = "";
                            if (String.IsNullOrWhiteSpace(StrMobileNo_NotSend) == false)
                                lblConfirmSendSMS.Text = StrMobileNo_NotSend.Split(';').Length + " پیامک به دلیل وجود مشکل در برقراری ارتباط با وب سریس، ارسال نشد<br><br>";
                            lblConfirmSendSMS.Text += CountSmsForReport + " پیامک به سرور پیام کوتاه ارسال شد" + "<br><br>" + "برای ذخیره وضعیت پیامک های ارسالی، برروی دکمه ذخیره کلیک نمایید";
                            btnSave.Visible = true;
                            btnSend.Visible = false;
                        }
                        Counter++;
                    } while (SmsSend == false && Counter <= 5);

                    if (SmsSend)
                    {
                        CountSmsForReport += SMSResultAFE.Length;
                        if (SendStatus == false)
                        {
                            for (int j = 0; j < SMSResultAFE.Length; j++)
                            {
                                if (String.IsNullOrEmpty(SMSResultAFE[j].ToString().Trim()) == false)
                                {
                                    String Result = Utility.GetAFESmsResult(SMSResultAFE[j]).Trim();
                                    if (String.IsNullOrEmpty(Result))
                                    {
                                        SendStatus = true;
                                        break;
                                    }
                                    else
                                    {
                                        WebServiceMessage = Result;
                                        return false;
                                    }
                                }
                            }
                        }

                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResultAFE);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(StrReId_NotSend) == false)
                        {
                            StrReId_NotSend += ";";
                            StrMobileNo_NotSend += ";";
                        }
                        StrMobileNo_NotSend += dtSMSPackets.Rows[i]["MobileNos"].ToString();
                        StrReId_NotSend += dtSMSPackets.Rows[i]["ReIds"].ToString();

                        dtSMSPackets.Rows[i].BeginEdit();
                        dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';').Length);
                        dtSMSPackets.Rows[i].EndEdit();
                    }
                    SentPacketAfterLastThreadSleep++;
                }
                dtSMSPackets.AcceptChanges();
                ViewState["DataTableSMSPackets"] = dtSMSPackets;

                if (SendStatus)
                {
                    SmsManager.FindByCode(SMSId);
                    if (SmsManager.Count == 0)
                    {
                        return false;
                    }
                    SmsManager[0].BeginEdit();
                    SmsManager[0]["SMSSendDate"] = Utility.GetDateOfToday();
                    SmsManager[0]["IsDelivered"] = 1;
                    SmsManager[0]["SenderId"] = Utility.GetCurrentUser_UserId();
                    SmsManager[0].EndEdit();
                    if (SmsManager.Save() <= 0)
                        return false;
                }
                else
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

        lblConfirmSendSMS.Text = "";
        if (String.IsNullOrWhiteSpace(StrMobileNo_NotSend) == false)
            lblConfirmSendSMS.Text = StrMobileNo_NotSend.Split(';').Length + " پیامک به دلیل وجود مشکل در برقراری ارتباط با وب سریس، ارسال نشد<br><br>";
        lblConfirmSendSMS.Text += CountSmsForReport + " پیامک به سرور پیام کوتاه ارسال شد" + "<br><br>" + "برای ذخیره وضعیت پیامک های ارسالی، برروی دکمه ذخیره کلیک نمایید";
        btnSave.Visible = true;
        btnSend.Visible = false;
        return true;
    }

    Boolean SendSMSByPrdco(ref String WebServiceMessage, ref String StrReId_NotSend, ref String StrMobileNo_NotSend)
    {
        SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        int CountSmsForReport = 0;
        int SMSId = 0;
        Boolean SmsSend = false;
        Boolean SendStatus = false; // Check Sms send, for SmsManager

        try
        {

            #region  Create DataTableSMSPackets
            if (ViewState["DataTableSMSPackets"] == null)
                return false;
            if (ViewState["SMSId"] == null)
                return false;
            SMSId = Convert.ToInt32(ViewState["SMSId"]);

            DataTable dtSMSPackets = (DataTable)ViewState["DataTableSMSPackets"];

            String[] ErrorInSendingSMS = new String[Utility.GetPrdcoSMSPacketSize()];
            for (int i = 0; i < Utility.GetPrdcoSMSPacketSize(); i++)
                ErrorInSendingSMS[i] = ((int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS).ToString();
            #endregion

            if (dtSMSPackets.Rows.Count == 0)
                return false;

            #region Define Prdco Array Parameters
            string[] SMSInfo = new string[2];
            SMSInfo = Utility.GetPrdcoWebServiceInformation();
            string UserName = SMSInfo[0];
            string PassWord = SMSInfo[1];
            string Number = SMSInfo[2];
            #endregion

            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count != 1 || Convert.ToBoolean(SmsManager[0]["InActive"]))
                return false;


            string SMSBody = SmsManager[0]["SmsBody"].ToString();

            int SentPacketAfterLastThreadSleep = 0;

            String[] SMSResult = new String[Utility.GetSMSPacketSize()];

            //*****SendType=true SMS don't save In MemoryCard,SendType=false SMS Save In MemoryCard , SendType=2 SMS Save In SimCard                
            Boolean SendType = false;

            for (int i = 0; i < dtSMSPackets.Rows.Count; i++)
            {
                if (SentPacketAfterLastThreadSleep * Utility.GetPrdcoSMSPacketSize() >= Utility.GetNoOfSMSPacketSendBeforeThreadSleep())
                {
                    SentPacketAfterLastThreadSleep = 0;
                    System.Threading.Thread.Sleep(Utility.GetSMSThreadSleepTime());
                }

                SMSPrdcoAsync.ArrayOfString SMSResultPrdco = new SMSPrdcoAsync.ArrayOfString();
                long[] MessageIDsPrdco = new long[Utility.GetPrdcoSMSPacketSize()];
                SmsSend = false;

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
                    CountSmsForReport += SMSResultPrdco.Count;
                    dtSMSPackets.Rows[i].BeginEdit();
                    dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", SMSResultPrdco);
                    dtSMSPackets.Rows[i].EndEdit();
                }
             
                else
                {
                    if (String.IsNullOrWhiteSpace(StrReId_NotSend) == false)
                    {
                        StrReId_NotSend += ";";
                        StrMobileNo_NotSend += ";";
                    }
                    StrMobileNo_NotSend += dtSMSPackets.Rows[i]["MobileNos"].ToString();
                    StrReId_NotSend += dtSMSPackets.Rows[i]["ReIds"].ToString();

                    dtSMSPackets.Rows[i].BeginEdit();
                    dtSMSPackets.Rows[i]["SMSResult"] = String.Join(";", ErrorInSendingSMS, 0, dtSMSPackets.Rows[i]["MobileNos"].ToString().Split(';').Length);
                    dtSMSPackets.Rows[i].EndEdit();
                }

                SentPacketAfterLastThreadSleep++;
            }
            dtSMSPackets.AcceptChanges();
            ViewState["DataTableSMSPackets"] = dtSMSPackets;

            if (SendStatus)
            {
                SmsManager.FindByCode(SMSId);
                if (SmsManager.Count == 0)
                {
                    return false;
                }
                SmsManager[0].BeginEdit();
                SmsManager[0]["SMSSendDate"] = Utility.GetDateOfToday();
                SmsManager[0]["IsDelivered"] = 1;
                SmsManager[0]["SenderId"] = Utility.GetCurrentUser_UserId();
                SmsManager[0].EndEdit();
                if (SmsManager.Save() <= 0)
                    return false;
            }
            else return false;
        }
        catch (Exception err)
        {
            #region catch
            Utility.SaveWebsiteError(err);
            if (SendStatus)
            {
                SmsManager.FindByCode(SMSId);
                if (SmsManager.Count == 0)
                {
                    return false;
                }
                SmsManager[0].BeginEdit();
                SmsManager[0]["SMSSendDate"] = Utility.GetDateOfToday();
                SmsManager[0]["IsDelivered"] = 1;
                SmsManager[0]["SenderId"] = Utility.GetCurrentUser_UserId();
                SmsManager[0].EndEdit();
                if (SmsManager.Save() <= 0)
                    return false;
            }
            else
                return false;
            #endregion
        }

        lblConfirmSendSMS.Text = "";
        if (String.IsNullOrWhiteSpace(StrMobileNo_NotSend) == false)
            lblConfirmSendSMS.Text = StrMobileNo_NotSend.Split(';').Length + " پیامک به دلیل وجود مشکل در برقراری ارتباط باسرور ، ارسال نشد<br><br>";

        lblConfirmSendSMS.Text += CountSmsForReport + " پیامک به سرور پیام کوتاه ارسال شد" + "<br><br>" + "برای ذخیره وضعیت پیامک های ارسالی، برروی دکمه ذخیره کلیک نمایید" + "<br><br>";
        lblConfirmSendSMS.Text += "هشدار: تنها تا 24 ساعت پس از ارسال پیامک امکان دریافت وضعیت آنها وجود دارد";
        btnSave.Visible = true;
        btnSend.Visible = false;
        return true;

    }



    Boolean SaveSMSDelivery()
    {
        DataTable dtSMSPackets = null;
        int SMSId = 0;
        try
        {
            if (ViewState["DataTableSMSPackets"] == null)
                return false;
            if (ViewState["SMSId"] == null)
                return false;
            SMSId = Convert.ToInt32(ViewState["SMSId"]);

            dtSMSPackets = (DataTable)ViewState["DataTableSMSPackets"];

            if (dtSMSPackets == null || dtSMSPackets.Rows.Count == 0)
                return false;

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }

        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        Transaction.Add(SmsRecieverManager);
        Transaction.BeginSave();

        try
        {
            //Merging Data
            String DeliverySMSIDs = "";
            String ReIds = "";
            int MaxLength = 8000;//Max Length of Varchar(max)
            System.Collections.ArrayList ArrayReIds = new System.Collections.ArrayList();
            System.Collections.ArrayList ArrayDeliverySMSIDs = new System.Collections.ArrayList();

            for (int Packet = 0; Packet < dtSMSPackets.Rows.Count; Packet++)
            {
                String SplitterReId = "";
                SplitterReId = (String.IsNullOrEmpty(ReIds)) ? "" : ";";
                String tmpReId = dtSMSPackets.Rows[Packet]["ReIds"].ToString();

                String SplitterDeliverySMSID = "";
                SplitterDeliverySMSID = (String.IsNullOrEmpty(DeliverySMSIDs)) ? "" : ";";
                String tmpDeliverySMSID = dtSMSPackets.Rows[Packet]["SMSResult"].ToString();

                if ((ReIds + SplitterReId + tmpReId).Length >= MaxLength)
                {
                    ArrayReIds.Add(ReIds);
                    ReIds = tmpReId;

                    ArrayDeliverySMSIDs.Add(DeliverySMSIDs);
                    DeliverySMSIDs = tmpDeliverySMSID;
                }
                else
                {
                    ReIds += SplitterReId + tmpReId;
                    DeliverySMSIDs += SplitterDeliverySMSID + tmpDeliverySMSID;
                }
            }

            if (ArrayReIds.Count != 0)
            {
                for (int i = 0; i < ArrayReIds.Count; i++)
                    SmsRecieverManager.UpdateSMSRecieverDelivery(ArrayReIds[i].ToString(), ArrayDeliverySMSIDs[i].ToString());
                Transaction.EndSave();
                ShowMessage("ذخیره اطلاعات با موفقیت انجام شد", System.Drawing.Color.DarkGreen);
                return true;
            }
            else
            {
                Transaction.CancelSave();
                return false;
            }
        }
        catch (Exception err)
        {
            Transaction.CancelSave();
            Utility.SaveWebsiteError(err);
            return false;
        }
    }

    Boolean SaveMagfaSMSDelivery()
    {
        DataTable dtSMSPackets = null;
        int SMSId = 0;
        #region Check ViewState
        try
        {
            if (ViewState["DataTableSMSPackets"] == null)
                return false;
            if (ViewState["SMSId"] == null)
                return false;
            SMSId = Convert.ToInt32(ViewState["SMSId"]);

            dtSMSPackets = (DataTable)ViewState["DataTableSMSPackets"];

            if (dtSMSPackets == null || dtSMSPackets.Rows.Count == 0)
                return false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }
        #endregion

        #region Defin Managers
        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        Transaction.Add(SmsRecieverManager);
        Transaction.BeginSave();
        #endregion
        try
        {
            //Merging Data
            String DeliverySMSIDs = "";
            String ReIds = "";
            bool UseArray = false;
            int MaxLength = 8000;//Max Length of Varchar(max)
            System.Collections.ArrayList ArrayReIds = new System.Collections.ArrayList();
            System.Collections.ArrayList ArrayDeliverySMSIDs = new System.Collections.ArrayList();

            for (int Packet = 0; Packet < dtSMSPackets.Rows.Count; Packet++)
            {
                String SplitterReId = "";
                SplitterReId = (String.IsNullOrEmpty(ReIds)) ? "" : ";";
                String tmpReId = dtSMSPackets.Rows[Packet]["ReIds"].ToString();

                String SplitterDeliverySMSID = "";
                SplitterDeliverySMSID = (String.IsNullOrEmpty(DeliverySMSIDs)) ? "" : ";";
                String tmpDeliverySMSID = dtSMSPackets.Rows[Packet]["SMSResult"].ToString();

                if ((ReIds + SplitterReId + tmpReId).Length >= MaxLength)
                {
                    ArrayReIds.Add(ReIds);
                    ReIds = tmpReId;

                    ArrayDeliverySMSIDs.Add(DeliverySMSIDs);
                    DeliverySMSIDs = tmpDeliverySMSID;

                    UseArray = true;
                }
                else
                {
                    ReIds += SplitterReId + tmpReId;
                    DeliverySMSIDs += SplitterDeliverySMSID + tmpDeliverySMSID;
                }
            }


            //-------------update sms reciever-----------------------
            if (UseArray)
            {
                if (ArrayDeliverySMSIDs.Count != 0)
                {
                    //******Save SMSDeliveryId In our DB
                    for (int i = 0; i < ArrayDeliverySMSIDs.Count; i++)
                        SmsRecieverManager.UpdateSMSRecieverDelivery(ArrayReIds[i].ToString(), ArrayDeliverySMSIDs[i].ToString(), SMSId, (int)TSP.DataManager.ErrorSMSRequest.MobileNumberIsEmpty);
                    //*************************************
                    Transaction.EndSave();
                }
                else
                {
                    Transaction.CancelSave();
                    ShowMessage("خطایی در بازیابی اطلاعات به وجود آمده است", System.Drawing.Color.Red);
                    return false;
                }
            }
            else
            {
                if (DeliverySMSIDs != "")
                {

                    //******Save SMSDeliveryId In our DB
                    SmsRecieverManager.UpdateSMSRecieverDelivery(ReIds, DeliverySMSIDs, SMSId, (int)TSP.DataManager.ErrorSMSRequest.MobileNumberIsEmpty);
                    //*************************************
                    Transaction.EndSave();
                }
                else
                {
                    Transaction.CancelSave();
                    ShowMessage("خطایی در بازیابی اطلاعات به وجود آمده است", System.Drawing.Color.Red);
                    return false;
                }
            }

        }
        catch (Exception err)
        {
            Transaction.CancelSave();
            Utility.SaveWebsiteError(err);
            return false;
        }

        if (UpdateMagfaSMSDeliveryReport(SMSId))
        {
            ShowMessage("ذخیره اطلاعات با موفقیت انجام شد", System.Drawing.Color.DarkGreen);
            return true;
        }
        else return false;
    }

    private Boolean UpdateMagfaSMSDeliveryReport(int SMSId)
    {
        string[] SMSInfo = new string[2];
        SMSInfo = Utility.GetMagfaWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        string[] Number = new string[] { SMSInfo[2] };
        string Domain = SMSInfo[3];
        bool IsInSendDay = false, IsMoreThanOneDay = false;

        long[] MagfaSMSIds = new long[Utility.GetMagfaSMSPacketSize()];

        SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();

        ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
        ssq.PreAuthenticate = true;

        try
        {
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count != 1) return false;
            if (Convert.ToBoolean(SmsManager[0]["IsDelivered"]) == false)
            {
                ShowMessage("پیامک انتخاب شده ارسال نشده است", System.Drawing.Color.Red);
                return false;
            }
            if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SMSSendDate"]))
            {
                if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SMSTime"]))
                {
                    string SMSSendDate = SmsManager[0]["SMSSendDate"].ToString();
                    string SMSTime = SmsManager[0]["SMSTime"].ToString();
                    Utility.Date date = new Utility.Date(SMSSendDate);
                    string ExpDate = date.AddDays(1);

                    int result = string.Compare(Utility.GetDateOfToday(), ExpDate);
                    if (result == 1)//بیش از یک روز گذشته باشد 
                        IsMoreThanOneDay = true;
                    else if (result == -1) //روز ارسال می باشد 
                        IsInSendDay = true;

                    if (IsMoreThanOneDay)//بیش از یک روز گذشته باشد 
                    {
                        ShowMessage("به دلیل گذشتن 24 ساعت از ارسال پیامک ،امکان دریافت وضعیت پیامک ها وجود ندارد", System.Drawing.Color.Red);
                        return false;
                    }
                }
            }

            #region در صورت عدم دریافت بعضی از وضعیت ها از صفحه گیرندگان استفاده کنند این زمانبر است
            //if (IsInSendDay)
            //{
            //    #region Get MessageIDs
            //    DataTable dtSMSReUnknownDeliverySMSId = SmsRecieverManager.GetSMSRecieverByDeliverySMSId(SMSId, 1, 1, 0);
            //    for (int k = 0; k < dtSMSReUnknownDeliverySMSId.Rows.Count; k++)
            //    {
            //        if (Utility.IsDBNullOrNullValue(dtSMSReUnknownDeliverySMSId.Rows[k]["DeliverySMSID"])
            //            || Convert.ToInt64(dtSMSReUnknownDeliverySMSId.Rows[k]["DeliverySMSID"]) < 1000)
            //        {
            //            //--------get magfa id from webservices-----
            //            long CurrentSmsReId = Convert.ToInt64(dtSMSReUnknownDeliverySMSId.Rows[k]["SmsReId"]);
            //            long CurrentMagfaId = ssq.getMessageId(Domain, CurrentSmsReId);
            //            //----update smsreciever-----
            //            SmsRecieverManager.FindByCode(CurrentSmsReId);
            //            SmsRecieverManager[0].BeginEdit();
            //            SmsRecieverManager[0]["DeliverySMSID"] = CurrentMagfaId;
            //            SmsRecieverManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //            SmsRecieverManager[0]["ModifiedDate"] = DateTime.Now;
            //            SmsRecieverManager[0].EndEdit();
            //            SmsRecieverManager.Save();
            //        }
            //    }
            //    #endregion
            //}
            #endregion

            #region Create List Of DeliverySMSID
            MagfaSetDelivery(SmsRecieverManager, SMSId, ssq);
            #endregion
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره وضعیت ارسال بعضی از پیامک ها ایجاد شده است", System.Drawing.Color.Red);
            return false;
        }
        return true;
    }

    private void MagfaSetDelivery(TSP.DataManager.SmsRecieverManager SmsRecieverManager, int SMSId, SMSMagfa.SoapSmsQueuableImplementationService ssq)
    {
        DataTable dtSMSRe = SmsRecieverManager.GetSMSRecieverByDeliverySMSId(SMSId, 1, 0, 1);
        if (dtSMSRe.Rows.Count == 0) return;
        int cntResult = dtSMSRe.Rows.Count;

        int Total = dtSMSRe.Rows.Count;
        int DeliveryPackCount = Utility.GetMagfaSMSPacketSize();
        int SubDeliverReportCount = (Total / DeliveryPackCount) + 1;
        int CountUpdatedReciverd = 0;
        //  int EndPosition = DeliveryPackCount;
        long[] ArraySMSRe = new long[cntResult];
        long[] SubSMSResult = new long[DeliveryPackCount];
        ArraySMSRe = Array.ConvertAll<string, long>(dtSMSRe.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("DeliverySMSID")).ToArray(), long.Parse);

        for (int i = 0; i < SubDeliverReportCount; i++)
        {
            // if (Total - CountUpdatedReciverd < DeliveryPackCount)
            //     EndPosition = Total - CountUpdatedReciverd;
            //************Get Saved SMSId Of Magfa***************          
            SubSMSResult = ArraySMSRe.Where((x, k) => k >= CountUpdatedReciverd && k <= DeliveryPackCount + CountUpdatedReciverd).ToArray();
            //***************************************************

            CountUpdatedReciverd = MagfaSMSDeliveryReport(SubSMSResult, dtSMSRe, CountUpdatedReciverd, ssq, SmsRecieverManager);
            if (CountUpdatedReciverd == -1) return;
            SubSMSResult = new long[DeliveryPackCount];
        }
    }

    private int MagfaSMSDeliveryReport(long[] SMSID, DataTable dtRecievers, int CountUpdatedReciever
    , SMSMagfa.SoapSmsQueuableImplementationService ssq, TSP.DataManager.SmsRecieverManager SmsRecieverManager)
    {
        int DeliveryPackCount = Utility.GetMagfaSMSPacketSize();
        int SMSReId = -1;
        int[] DeliveryReport = new int[SMSID.Length];

        DeliveryReport = ssq.getRealMessageStatuses(SMSID);
        int EndPosition = DeliveryPackCount;
        if (dtRecievers.Rows.Count - CountUpdatedReciever < DeliveryPackCount)
            EndPosition = dtRecievers.Rows.Count - CountUpdatedReciever;
        for (int i = 0; i < EndPosition; i++)
        {
            SMSReId = Convert.ToInt32(dtRecievers.Rows[i + CountUpdatedReciever]["SmsReId"]);
            SmsRecieverManager.FindByCode(SMSReId);
            if (SmsRecieverManager.Count != 1)
                return -1;

            if (Utility.IsDBNullOrNullValue(SmsRecieverManager[0]["RecieverCellPhone"]))
                continue;

            if (Convert.ToInt32(SmsRecieverManager[0]["SMSDeliveryReId"]) !=
                (int)TSP.DataManager.ErrorSMSRequest.SentToMobile)
            {
                SmsRecieverManager[0].BeginEdit();
                switch (DeliveryReport[i])
                {
                    case -1://پیامک بیش از یک روز پیش ارسال شده
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UnKnown;
                        break;
                    case 1:
                        SmsRecieverManager[0]["IsDelivered"] = 1;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.SentToMobile;
                        break;
                    case 2:
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.FailedToMobile;
                        break;
                    case 8:
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.SendToComunicationCenter;
                        break;
                    case 16:
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.FailedToComunicationCenter;
                        break;
                    case 0:
                        SmsRecieverManager[0]["IsDelivered"] = 0;
                        SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.Pending;
                        break;
                    default:
                        if (Convert.ToInt32(SmsRecieverManager[0]["SMSDeliveryReId"]) != 1)
                        {
                            SmsRecieverManager[0]["IsDelivered"] = 0;
                            SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UnKnown;
                        }
                        break;
                }

                SmsRecieverManager[0].EndEdit();
                if (SmsRecieverManager.Save() <= 0)
                    return -1;
            }
        }
        return (CountUpdatedReciever + EndPosition);
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
    #region Set Error
    //void ShowMessage(String Error)
    //{
    //    RoundPanelSendSMS.Visible = false;
    //    RoundPanelMessage.Visible = true;

    //    lblMessage.ForeColor = System.Drawing.Color.DarkRed;
    //    lblMessage.Text = Error;
    //}

    void ShowMessage(String Message, System.Drawing.Color Color)
    {
        RoundPanelSendSMS.Visible = false;
        RoundPanelMessage.Visible = true;

        lblMessage.ForeColor = Color;// System.Drawing.Color.DarkGreen;
        lblMessage.Text = Message;
    }
    #endregion
    void LoadCredit()
    {
        if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
        {
            try
            {
                string[] SmsInfo = new string[4];
                SmsInfo = Utility.GetMagfaWebServiceInformation();
                string UserName = SmsInfo[0];
                string PassWord = SmsInfo[1];
                string DomainName = SmsInfo[3];
                lblCurrentWebService.Text = "مگفا";
                SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
                ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
                ssq.PreAuthenticate = true;
                double MagfaRemainingCredit = ssq.getCredit(DomainName);
                if (MagfaRemainingCredit > 0)
                    txtRemainingCredit.Text = MagfaRemainingCredit.ToString("N") + " ریال";
                else
                    txtRemainingCredit.Text = "اتمام اعتبار";
            }
            catch
            {
                txtRemainingCredit.Text = "خطا در ارتباط با وب سرویس";
            }
        }
        else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
        {
            try
            {
                ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
                string[] SmsInfo = new string[2];
                SmsInfo = Utility.GetSMSWebServiceInformation();
                string UserName = SmsInfo[0];
                string Password = SmsInfo[1];
                lblCurrentWebService.Text = "عصر فرا ارتباط";
                string arc = BoxService.GetRemainingCredit(UserName, Password);
                double AFERemainingCredit = Convert.ToDouble(arc);
                if (AFERemainingCredit > 0)
                    txtRemainingCredit.Text = AFERemainingCredit.ToString("N") + " ریال";
                else
                    txtRemainingCredit.Text = "اتمام اعتبار";
            }
            catch
            {
                txtRemainingCredit.Text = "خطا در ارتباط با وب سرویس";
            }
        }

        else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Prdco || Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.PrdcoAsync)
        {
            try
            {
            string[] SmsInfoPrdco = new string[2];
            SmsInfoPrdco = Utility.GetPrdcoWebServiceInformation();
            string UserNamePrdco = SmsInfoPrdco[0];
            string PasswordPrdco = SmsInfoPrdco[1];

            lblCurrentWebService.Text = "پویا رایانه دنا";

            SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();

            double PrdcoRemainingCredit = sendSoapClient.Credit(UserNamePrdco, PasswordPrdco);

            if (PrdcoRemainingCredit > 0)
                txtRemainingCredit.Text = PrdcoRemainingCredit.ToString("N") + " ریال";
            else
                txtRemainingCredit.Text = "اتمام اعتبار";
            }
            catch
            {
                txtRemainingCredit.Text = "خطا در ارتباط با وب سرویس";
            }

        }

    }
    #endregion
}