using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_SMS_SMSTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        string[] SmsInfo = new string[4];
        SmsInfo = Utility.GetMagfaWebServiceInformation();
        string UserName = SmsInfo[0];
        string PassWord = SmsInfo[1];
        string[] senderNumber = new string[] { SmsInfo[2] };
        string DomainName = SmsInfo[3];
        SMSMagfa.SoapSmsQueuableImplementationService sq = new SMSMagfa.SoapSmsQueuableImplementationService();
        sq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
        sq.PreAuthenticate = true;
        long[] results;

        string[] messages;
        string[] mobiles;
        string[] origs;

        int[] encodings;
        string[] UDH;
        int[] mclass;
        int[] priorities;
        long[] checkingIds;
        int count = 1;
        messages = new string[count];
        mobiles = new string[count];
        origs = new string[count];

        encodings = new int[count];
        UDH = new string[count];
        mclass = new int[count];
        priorities = new int[count];
        checkingIds = new long[count];
        /*
        encodings = null;
        UDH = null;
        mclass = null;
        priorities = null;
        checkingIds = null;
        */
        for (int i = 0; i < count; i++)
        {
            messages[i] = txtBody.Text;
            mobiles[i] = txtReceiver.Text;
            origs[i] = senderNumber[0];

            encodings[i] = -1;
            UDH[i] = "";
            mclass[i] = -1;
            priorities[i] = -1;
            checkingIds[i] = 200 + i;
        }

        long[] Result = sq.enqueue(DomainName, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds);
    }

    protected void btnSendNotification_Click(object sender, EventArgs e)
    {
        SendSMSNotification(Utility.Notifications.NotificationTypes.AnswerMessageFromPublicUsers, "test by new service", "09171123560", "756");
    }
    protected void btnTestSending15000SMS_Click(object sender, EventArgs e)
    {
        try
        {
            string[] SmsInfo = new string[4];
            SmsInfo = Utility.GetMagfaWebServiceInformation();
            string UserName = SmsInfo[0];
            string PassWord = SmsInfo[1];
            string[] senderNumber = new string[] { SmsInfo[2] };
            string DomainName = SmsInfo[3];
            SMSMagfa.SoapSmsQueuableImplementationService sq = new SMSMagfa.SoapSmsQueuableImplementationService();
            sq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
            sq.PreAuthenticate = true;
            long[] results;

            string[] messages;
            string[] mobiles;
            string[] origs;

            int[] encodings;
            string[] UDH;
            int[] mclass;
            int[] priorities;
            long[] checkingIds;
            int count = 90;
            messages = new string[count];
            mobiles = new string[count];
            origs = new string[count];

            encodings = new int[count];
            UDH = new string[count];
            mclass = new int[count];
            priorities = new int[count];
            checkingIds = new long[count];
            /*
            encodings = null;
            UDH = null;
            mclass = null;
            priorities = null;
            checkingIds = null;
            */
            for (int i = 0; i < count; i++)
            {
                messages[i] = txtBody.Text;
                mobiles[i] = ""; //txtReceiver.Text;
                origs[i] = senderNumber[0];

                encodings[i] = -1;
                UDH[i] = "";
                mclass[i] = -1;
                priorities[i] = -1;
                checkingIds[i] = 200 + i;
            }

            for (int j = 0; j <= 200; j++)
            {
                long[] Result = sq.enqueue(DomainName, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds);
            }
            lblWarning.Text = "Succed!!!!!";
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    protected void btnGetDelivery_Click(object sender, EventArgs e)
    {
        try
        {
            string[] SmsInfo = new string[4];
            SmsInfo = Utility.GetMagfaWebServiceInformation();
            string UserName = SmsInfo[0];
            string PassWord = SmsInfo[1];
            string[] senderNumber = new string[] { SmsInfo[2] };
            string DomainName = SmsInfo[3];
            SMSMagfa.SoapSmsQueuableImplementationService sq = new SMSMagfa.SoapSmsQueuableImplementationService();
            sq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
            sq.PreAuthenticate = true;
            long[] results;

            string[] messages;
            string[] mobiles;
            string[] origs;

            int[] encodings;
            string[] UDH;
            int[] mclass;
            int[] priorities;
            long[] checkingIds;
            int count = 90;
            messages = new string[count];
            mobiles = new string[count];
            origs = new string[count];

            encodings = new int[count];
            UDH = new string[count];
            mclass = new int[count];
            priorities = new int[count];
            checkingIds = new long[count];
            long SMSId = sq.getMessageId(DomainName, Convert.ToInt64(176337));
            long[] SMSReId = new long[1];
            SMSReId[0] = SMSId;
            int[] ResultDelivery = sq.getRealMessageStatuses(SMSReId);
            lblWarningDelivery.Text = ResultDelivery[0].ToString();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }


    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)TSP.DataManager.UserType.Member).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
}