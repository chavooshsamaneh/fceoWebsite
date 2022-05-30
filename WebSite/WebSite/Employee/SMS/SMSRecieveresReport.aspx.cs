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
using DevExpress.Web;
using System.Linq;

public partial class Employee_SMS_SMSRecieveresReport : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HiddenFieldSMS["Timer"] = 1;
            if (Request.QueryString["SMSId"] == null)
            {
                Response.Redirect("ConfirmedSMS.aspx");
                if (HiddenFieldSMS["PageRefrence"] != null)
                    Response.Redirect(HiddenFieldSMS["PageRefrence"].ToString());
            }

            if (Request.UrlReferrer != null)
            {
                HiddenFieldSMS["PageRefrence"] = Request.UrlReferrer.ToString();
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            HiddenFieldSMS["SMSId"] = Request.QueryString["SMSId"];
            int SMSId = int.Parse(Utility.DecryptQS(HiddenFieldSMS["SMSId"].ToString()));
            // UpdateSMSDeliveryReport(SMSId);
            FillForm(SMSId);
            objdSMSReMe.SelectParameters["SMSId"].DefaultValue = SMSId.ToString();
            objdSMSReMe.SelectParameters["recievertype"].DefaultValue = ((int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member).ToString();
            grdSMSReciever.DataBind();

            objdsSMSReEmp.SelectParameters["SMSId"].DefaultValue = SMSId.ToString();
            objdsSMSReEmp.SelectParameters["recievertype"].DefaultValue = ((int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee).ToString();
            GridViewSMSReEmp.DataBind();

            objdSMSReManual.SelectParameters["SMSId"].DefaultValue = SMSId.ToString();
            objdSMSReManual.SelectParameters["recievertype"].DefaultValue = ((int)TSP.DataManager.SmsRecieverManager.RecieverTypes.ManualInsert).ToString();
            GridViewSMSReMaual.DataBind();

        }
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void cbSelectAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }

    protected void grdSMSReciever_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;

        Int32 start = grid.VisibleStartIndex;
        Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
        Int32 selectNumbers = 0;
        end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

        for (int i = start; i < end; i++)
            if (grid.Selection.IsRowSelected(i))
                selectNumbers++;

        e.Properties["cpSelectedRowsOnPage"] = selectNumbers;
        e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
    }

    protected void grdSMSReciever_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (String.IsNullOrEmpty(e.Parameters) == false)
        {
            String[] Param = e.Parameters.Split(';');
            if (Param.Length > 0)
            {
                if (Param[0] == "MultiSelect")
                    ((ASPxGridView)sender).Columns[0].Visible = Boolean.Parse(Param[1]);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (HiddenFieldSMS["PageRefrence"] != null)
            Response.Redirect(HiddenFieldSMS["PageRefrence"].ToString());
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        int ListType = cmbListType.SelectedIndex;
        switch (ListType)
        {
            case 0:
                GridViewExporterMember.FileName = "RecieverMemberList";
                GridViewExporterMember.WriteXlsToResponse(true);
                break;

            case 1:
                GridViewExporterEmployee.FileName = "RecieverEmployeeList";
                GridViewExporterEmployee.WriteXlsToResponse(true);
                break;

            case 2:
                GridViewExporterNumber.FileName = "RecieverNumberList";
                GridViewExporterNumber.WriteXlsToResponse(true);
                break;
        }
    }

    protected void CallbackSMS_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        switch (e.Parameter)
        {
            case "Timer":
                int SMSId = int.Parse(Utility.DecryptQS(HiddenFieldSMS["SMSId"].ToString()));

                int CurrentWebService = Utility.GetCurrentSMSWebService();
                if (CurrentWebService == (int)TSP.DataManager.SMSWebServiceType.Magfa)
                    UpdateMagfaSMSDeliveryReport(SMSId);
                else if (CurrentWebService == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
                    UpdateSMSDeliveryReport(SMSId);
                break;
        }
    }

    protected void grdSMSReciever_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "SMSDeliveryReId")
        {
            DevExpress.Web.ASPxImage btnReport = (DevExpress.Web.ASPxImage)grdSMSReciever.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)grdSMSReciever.Columns["ReportName"], "btnReport");
            if (btnReport != null)
            {
                SetGridImage(e, btnReport);
            }
        }
    }

    protected void GridViewSMSReEmp_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "SMSDeliveryReId")
        {
            DevExpress.Web.ASPxImage btnReport = (DevExpress.Web.ASPxImage)GridViewSMSReEmp.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewSMSReEmp.Columns["SMSDeliveryReId"], "btnReportEmp");
            if (btnReport != null)
            {
                SetGridImage(e, btnReport);
            }
        }
    }

    protected void GridViewSMSReMaual_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

        if (e.DataColumn.FieldName == "SMSDeliveryReId")
        {
            DevExpress.Web.ASPxImage btnReport = (DevExpress.Web.ASPxImage)GridViewSMSReMaual.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewSMSReMaual.Columns["RpName"], "btnReportManu");
            if (btnReport != null)
            {
                SetGridImage(e, btnReport);
            }
        }
    }
    #endregion

    #region Methods
    private void FillForm(int SMSId)
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        try
        {
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1)
            {
                txtSMSSubject.Text = SmsManager[0]["SmsSubject"].ToString();
                txtSenderName.Text = SmsManager[0]["SenderName"].ToString();
                txtSMSDate.Text = SmsManager[0]["SMSDate"].ToString();
                txtSMSTime.Text = SmsManager[0]["SMSTime"].ToString();
                txtSMSType.Text = SmsManager[0]["SmsTypeName"].ToString();
            }
        }
        catch
        {
        }
    }

    #region AFE Delivery Report
    /// <summary>
    ///بدست آوردن وضعیت کل پیامک ها- عصر فرا ارتباط
    /// </summary>
    /// <param name="SMSId"></param>
    private void UpdateSMSDeliveryReport(int SMSId)
    {
        string[] SMSInfo = new string[3];
        SMSInfo = Utility.GetSMSWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        string Number = SMSInfo[2];

        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        try
        {
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count != 1)
            {
                return;
            }
            if (Utility.IsDBNullOrNullValue(SmsManager[0]["SMSSendDate"]))
                return;
            string SMSSendDate = SmsManager[0]["SMSSendDate"].ToString();
            Utility.Date date = new Utility.Date(SMSSendDate);
            string ExpDate = date.AddDays(7);
            if (string.Compare(Utility.GetDateOfToday(), ExpDate) >= 0)
            {
                HiddenFieldSMS["Timer"] = 0;
                return;
            }
            ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
            DataTable dtSMSRe = SmsRecieverManager.FindBySMSId(SMSId, false, true);
            int DeliveryPackCount = 10;
            int SubDeliverReportCount = (dtSMSRe.Rows.Count / DeliveryPackCount) + 1;
            int CountUpdatedReciverd = 0;
            int EndPosition = DeliveryPackCount;
            string[] SubSMSResult = new string[DeliveryPackCount];
            for (int i = 0; i < SubDeliverReportCount; i++)
            {
                if (dtSMSRe.Rows.Count - CountUpdatedReciverd < DeliveryPackCount)
                    EndPosition = dtSMSRe.Rows.Count - CountUpdatedReciverd;
                for (int j = 0; j < EndPosition; j++)
                {
                    SubSMSResult[j] = dtSMSRe.Rows[j + CountUpdatedReciverd]["DeliverySMSID"].ToString();
                }
                CountUpdatedReciverd = SMSDeliveryReport(UserName, PassWord, SubSMSResult, dtSMSRe, CountUpdatedReciverd, BoxService, SmsRecieverManager);
                if (CountUpdatedReciverd == -1)
                {
                    return;
                }
                SubSMSResult = new string[DeliveryPackCount];
            }
            grdSMSReciever.DataBind();
            GridViewSMSReEmp.DataBind();
            GridViewSMSReMaual.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    private int SMSDeliveryReport(string UserName, string PassWord, string[] SMSID, DataTable dtRecievers, int CountUpdatedReciever
        , ir.afe.www.BoxService BoxService, TSP.DataManager.SmsRecieverManager SmsRecieverManager)
    {
        int DeliveryPackCount = 10;
        int SMSReId = -1;
        bool LostData = false;

        string[] DeliveryReport = new string[SMSID.Length];
        DeliveryReport = BoxService.GetMessagesStatus(UserName, PassWord, SMSID);
        int EndPosition = DeliveryPackCount;
        if (dtRecievers.Rows.Count - CountUpdatedReciever < DeliveryPackCount)
            EndPosition = dtRecievers.Rows.Count - CountUpdatedReciever;
        for (int i = 0; i < EndPosition; i++)
        {
            SMSReId = Convert.ToInt32(dtRecievers.Rows[i + CountUpdatedReciever]["SmsReId"]);
            SmsRecieverManager.FindByCode(SMSReId);
            if (SmsRecieverManager.Count != 1)
                return -1;
            if (Utility.IsDBNullOrNullValue(SmsRecieverManager[0]["DeliverySMSID"]))
                LostData = true;
            SmsRecieverManager[0].BeginEdit();
            switch (DeliveryReport[i])
            {
                case "Service is disabled":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.ServiceIsDisabled;
                    break;
                case "Username is null or empty":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UsernameIsNullOrEmpty;
                    break;
                case "Password is null or empty":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.PassWordIsNullOrEmpty;
                    break;
                case "User Not Enable":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UserNotEnable;
                    break;
                case "username or password is wrong":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UsernameOrPasswordWrong;
                    break;
                case "messageSendID Array is NULL":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MessageSendIDArrayIsNull;
                    break;
                case "more than 10 messageSendID":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MoreThan10MessageSendID;
                    break;
                case "Indeterminate":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.Indeterminate;
                    break;
                case "SentToMobile":
                    SmsRecieverManager[0]["IsDelivered"] = 1;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.SentToMobile;
                    break;
                case "FailedToMobile":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.FailedToMobile;
                    break;
                case "SendToComunicationCenter":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.SendToComunicationCenter;
                    break;
                case "FailedToComunicationCenter":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.FailedToComunicationCenter;
                    break;
                case "Pending":
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.Pending;
                    break;
                default:
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.UnKnown;
                    break;
            }

            SmsRecieverManager[0].EndEdit();
            if (SmsRecieverManager.Save() <= 0)
                return -1;
        }
        if (LostData)
            ShowCallBackMessage("به دلیل از بین رفتن اطلاعات سرویس دهنده، امکان دریافت وضعیت بعضی پیامک ها وجود ندارد");

        return (CountUpdatedReciever + EndPosition);
    }
    #endregion

    #region Magfa Delivery Report
    /// <summary>
    ///بدست آوردن وضعیت کل پیامک ها- مگفا
    /// </summary>
    /// <param name="SMSId"></param>
    private void UpdateMagfaSMSDeliveryReport(int SMSId)
    {
        string[] SMSInfo = new string[2];
        SMSInfo = Utility.GetMagfaWebServiceInformation();
        string UserName = SMSInfo[0];
        string PassWord = SMSInfo[1];
        string[] Number = new string[] { SMSInfo[2] };
        string Domain = SMSInfo[3];

        SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();

        ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
        ssq.PreAuthenticate = true;

        try
        {
            //Boolean PassOneDay = false;
            bool IsInSendDay = false, IsMoreThanOneDay = false, IsAfter24Hours = false;
            #region Check If SMS Pass One Day
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count != 1) return;
            if (Convert.ToBoolean(SmsManager[0]["IsDelivered"]) == false)
            {
                ShowCallBackMessage("پیامک انتخاب شده ارسال نشده است");
                return;
            }
            if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SMSSendDate"]))
            {
                if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SMSTime"]))
                {
                    string SMSSendDate = SmsManager[0]["SMSSendDate"].ToString();
                    string SMSTime = SmsManager[0]["SMSTime"].ToString();
                    Utility.Date date = new Utility.Date(SMSSendDate);
                    string ExpDate = date.AddDays(1);

                    //******* =-1 This day ---- =0 the day after ----- =1 some day after *******
                    int result = string.Compare(Utility.GetDateOfToday(), ExpDate);
                    if (result == 1)//بیش از یک روز گذشته باشد 
                        IsMoreThanOneDay = true;
                    else if (result == -1) //روز ارسال می باشد 
                        IsInSendDay = true;
                    else if (result == 0)// دقیقا یک روز بعد از ارسال است
                    {
                        string Hour = DateTime.Now.Hour.ToString();
                        if (Hour.Length == 1)
                            Hour = "0" + Hour;
                        string TimeNow = Hour + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        if (string.Compare(TimeNow, SMSTime) > 0)// <= 0)
                        {
                            IsAfter24Hours = true;
                        }
                    }

                    if (IsMoreThanOneDay)//بیش از یک روز گذشته باشد 
                    {
                        ShowCallBackMessage("به دلیل گذشتن 24 ساعت از ارسال پیامک ،امکان دریافت وضعیت پیامک ها وجود ندارد");
                        return;
                    }
                    if (IsAfter24Hours)//یا بیش از 24 ساعت گذشته باشد
                    {
                        ShowCallBackMessage("به دلیل گذشتن 24 ساعت از ارسال پیامک ،امکان دریافت وضعیت پیامک ها وجود ندارد");
                        return;
                    }


                }
            }
            #endregion

            DataTable dtSMSReUnknownDeliverySMSId = SmsRecieverManager.GetSMSRecieverByDeliverySMSId(SMSId, 1, 1, 0);
            int GetPackCount = 0;
            int Position = 0;
            int index = 0;
            int Total = dtSMSReUnknownDeliverySMSId.Rows.Count;
            int Sub = Total / 100 + 1;

            for (int i = 0; i < Sub; i++)
            {
                #region Get MessageIDs
                try
                {
                    GetPackCount = GetPackCount + 100;

                    if (GetPackCount > Total)
                        GetPackCount = Position + (Total - Position);

                    if (GetPackCount == 0) break;

                    for (index = Position; index < GetPackCount; index++)
                    {
                        if (Utility.IsDBNullOrNullValue(dtSMSReUnknownDeliverySMSId.Rows[index]["DeliverySMSID"])
                            || Convert.ToInt64(dtSMSReUnknownDeliverySMSId.Rows[index]["DeliverySMSID"]) < 1000)
                        {
                            //--------get magfa id from webservices-----
                            long CurrentSmsReId = Convert.ToInt64(dtSMSReUnknownDeliverySMSId.Rows[index]["SmsReId"]);
                            long CurrentMagfaId = ssq.getMessageId(Domain, CurrentSmsReId);
                            //----update smsreciever-----
                            SmsRecieverManager.FindByCode(CurrentSmsReId);
                            SmsRecieverManager[0].BeginEdit();
                            SmsRecieverManager[0]["DeliverySMSID"] = CurrentMagfaId;
                            SmsRecieverManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            SmsRecieverManager[0]["ModifiedDate"] = DateTime.Now;
                            SmsRecieverManager[0].EndEdit();
                            SmsRecieverManager.Save();
                        }
                    }
                    Position = index;
                }
                catch (Exception err)
                {
                    Utility.SaveWebsiteError(err);
                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Message == "The underlying connection was closed: The connection was closed unexpectedly.")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "در ارتباط با سرور مگفا خطایی انجام گرفته است";
                        }
                    }
                }
                #endregion
                #region Create List Of DeliverySMSID
                try
                {
                    MagfaSetDelivery(SmsRecieverManager, SMSId, ssq);
                }
                catch (Exception err)
                {
                    Utility.SaveWebsiteError(err);
                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Message == "The underlying connection was closed: The connection was closed unexpectedly.")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "در ارتباط با سرور مگفا خطایی انجام گرفته است";
                        }
                    }
                }
                #endregion
            }

            try
            {
                MagfaSetDelivery(SmsRecieverManager, SMSId, ssq);
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Message == "The underlying connection was closed: The connection was closed unexpectedly.")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "در ارتباط با سرور مگفا خطایی انجام گرفته است";
                    }
                }
            }

            grdSMSReciever.DataBind();
            GridViewSMSReEmp.DataBind();
            GridViewSMSReMaual.DataBind();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
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
        int EndPosition = DeliveryPackCount;
        long[] ArraySMSRe = new long[cntResult];
        long[] SubSMSResult = new long[DeliveryPackCount];
        ArraySMSRe = Array.ConvertAll<string, long>(dtSMSRe.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("DeliverySMSID")).ToArray(), long.Parse);

        for (int i = 0; i < SubDeliverReportCount; i++)
        {
            if (Total - CountUpdatedReciverd < DeliveryPackCount)
                EndPosition = Total - CountUpdatedReciverd;
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
        #region Update Delivery
        for (int i = 0; i < EndPosition; i++)
        {
            SMSReId = Convert.ToInt32(dtRecievers.Rows[i + CountUpdatedReciever]["SmsReId"]);
            SmsRecieverManager.FindByCode(SMSReId);
            if (SmsRecieverManager.Count != 1)
                return -1;

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


                if (Utility.IsDBNullOrNullValue(SmsRecieverManager[0]["RecieverCellPhone"]))
                {
                    SmsRecieverManager[0]["IsDelivered"] = 0;
                    SmsRecieverManager[0]["SMSDeliveryReId"] = (int)TSP.DataManager.ErrorSMSRequest.MobileNumberIsEmpty;
                }

                SmsRecieverManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                SmsRecieverManager[0]["ModifiedDate"] = DateTime.Now;
                SmsRecieverManager[0].EndEdit();
                if (SmsRecieverManager.Save() <= 0)
                    return -1;
            }
        }
        #endregion
        return (CountUpdatedReciever + EndPosition);
    }
    #endregion

    #region Set Mesessage
    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackSMS.JSProperties["cpMsg"] = Msg;
        CallbackSMS.JSProperties["cpError"] = 1;
    }
    #endregion

    private void SetGridImage(ASPxGridViewTableDataCellEventArgs e, DevExpress.Web.ASPxImage btnReport)
    {
        string ReportName = "";
        if (!Utility.IsDBNullOrNullValue(e.GetValue("DeliveryReportName")))
        {
            ReportName = e.GetValue("DeliveryReportName").ToString();
        }

        if (Utility.IsDBNullOrNullValue(e.GetValue("SMSDeliveryReId")))
        {
            btnReport.ToolTip = "تعریف نشده";
            btnReport.ImageUrl = "~/Images/WFUnNounState.png";
            return;
        }
        //*************************************************************
        if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.SentToMobile)
        {
            btnReport.ToolTip = ReportName;
            btnReport.ImageUrl = "~/Images/SMSSentToMobile.PNG";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.FailedToComunicationCenter)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSFailedToComunicationCenter.PNG";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.SendToComunicationCenter)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSSentToComunicationCenter.PNG";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.FailedToMobile)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSNotSent.png";
        }
        //*************************************************************
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.ErrorInSendingSMS)//عدم ارسال
        {
            btnReport.ToolTip = ReportName;
            btnReport.ImageUrl = "~/Images/SMSErrorInSending.png";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.UnKnown)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSUnKnown.png";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.MobileNumberIsEmpty)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSMobileNumberIsEmpty.png";
        }
        else if (int.Parse(e.GetValue("SMSDeliveryReId").ToString()) == (int)TSP.DataManager.ErrorSMSRequest.Pending)
        {
            btnReport.ToolTip = ReportName;

            btnReport.ImageUrl = "~/Images/SMSPending.png";
        }
        else
        {
            btnReport.ToolTip = "عدم ارسال";
            btnReport.ImageUrl = "~/Images/SMSErrorInSending.png";
        }

        if (!Utility.IsDBNullOrNullValue(e.GetValue("DeliverySMSID")))
        {
            long DeliverySMSID = Convert.ToInt64(e.GetValue("DeliverySMSID"));
            if (DeliverySMSID < 1000) //----خطا
            {
                btnReport.ToolTip = Utility.GetMagfaSmsResult(DeliverySMSID);
                btnReport.ImageUrl = "~/Images/SMSErrorInSending.png";
            }
        }
    }
    #endregion
}
