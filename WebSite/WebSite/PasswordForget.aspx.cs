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
using System.Linq;

public partial class PasswordForget : System.Web.UI.Page
{

    #region Properties

    string _MeId
    {
        get
        {
            try
            {
                return HiddenFieldInfo["MeId"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["MeId"] = value;
        }
    }
    string _UltId
    {
        get
        {
            try
            {
                return HiddenFieldInfo["UltId"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["UltId"] = value;
        }
    }
    string _Email
    {
        get
        {
            try
            {
                return HiddenFieldInfo["Email"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["Email"] = value;
        }
    }
    string _UserId {
        get
        {
            try
            {
                return HiddenFieldInfo["UserId"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["UserId"] = value;
        }
    }

    string _RpId
    {
        get
        {
            try
            {
                return HiddenFieldInfo["RpId"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["RpId"] = value;
        }
    }
    string _HashEmail
    {
        get
        {
            try
            {
                return HiddenFieldInfo["HashEmail"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["HashEmail"] = value;
        }
    }
    string _MobileNo
    {
        get
        {
            try
            {
                return HiddenFieldInfo["MobileNo"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["MobileNo"] = value;
        }
    }
    string _HashMobileNo 
    {
        get
        {
            try
            {
                return HiddenFieldInfo["HashMobileNo"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["HashMobileNo"] = value;
        }
    }
    string _HashSMSBody
    {
        get
        {
            try
            {
                return HiddenFieldInfo["HashSMSBody"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            HiddenFieldInfo["HashSMSBody"] = value;
        }
    }

#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
                Response.Redirect("Login.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    Boolean CheckSecurityCode()
    {
        return Captcha.IsValid;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();

        try
        {
            if (CheckSecurityCode() == false)
            {
                return;
            }

            LogManager.FindUserName(txtUserName.Text);
            if (LogManager.Count != 1)
            {
                ShowInputError("اطلاعات وارد شده معتبر نمی باشند");
                return;
            }
            if (!CheckCondition(Convert.ToInt32(LogManager[0]["MeId"].ToString()), Convert.ToInt32(LogManager[0]["UltId"].ToString())))
            {
                return;
            }

            if (LogManager[0]["IsValid"].ToString() == "0")
            {
                ShowError("نام کاربری مورد نظر غیر فعال می باشد. لطفاً به دفتر سازمان مراجعه نمایید ");
                return;
            }

            //get email and mobile phone

            ArrayList list = GetInfo(Convert.ToInt32(LogManager[0]["MeId"].ToString()), Convert.ToInt32(LogManager[0]["UltId"].ToString()));
            if (Utility.IsDBNullOrNullValue(list[0]) && Utility.IsDBNullOrNullValue(list[1]))
            {

                ShowError("برای نام کاربری مورد نظر ایمیل یا شماره همراه ثبت نشده است. لطفاً به دفتر سازمان مراجعه نمایید ");
                return;
            }
             lblUserName.InnerHtml = txtUserName.Text;
             _MeId = LogManager[0]["MeId"].ToString();
             _UltId = Utility.EncryptQS(LogManager[0]["UltId"].ToString());
             _UserId = Utility.EncryptQS(LogManager[0]["UserId"].ToString());
             if (!Utility.IsDBNullOrNullValue(list[0]))
             _Email = list[0].ToString();
             if (!Utility.IsDBNullOrNullValue(list[1]))
             _MobileNo = list[1].ToString();

#region elementcontrol
             txtUserName.Enabled = false;
             if (!Utility.IsDBNullOrNullValue(list[1]))
             {
                 QuestionPhone.Visible = true;
                 btnTextMessage.Visible = true;
                 _HashMobileNo=lblMobileNo.InnerHtml = _MobileNo.Substring(0, 4) + "*****" + _MobileNo.Substring(9, _MobileNo.Length - 9);
             }
                
             if (!Utility.IsDBNullOrNullValue(list[0]))
             {
                 QuestionEmail.Visible = true;
                 btnSendEmail.Visible = true;
                 _HashEmail = lblEmail.InnerHtml = _Email.Substring(0, 3) + "********" + _Email.Substring(_Email.IndexOf("@"), _Email.Length - _Email.IndexOf("@"));
                
             }
             txtUserName.Visible = false;
             Captcha.Visible = false;
             btnNext.Visible = false;
           
#endregion

        }
        catch (Exception err)
        {
           
            Utility.SaveWebsiteError(err);
            ShowError("خطایی در بازیابی اطلاعات رخ داده است");
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {

        string SMSBody = (new Random().Next(0, 1000000)).ToString();
        TSP.DataManager.ResetPasswordManager ResetManager = new TSP.DataManager.ResetPasswordManager();

        try
        {
            DataRow dr = ResetManager.NewRow();
            dr["MeId"] = _MeId;
            dr["Type"] = Utility.DecryptQS( _UltId);
            dr["LetterNo"] = SMSBody;
            dr["RequestUserId"] = Utility.DecryptQS(_UserId);
            dr["RequestIpAddress"] = Request.UserHostAddress;
            dr["RequestDate"] = Utility.GetDateOfToday();
            dr["RequestTime"] = Utility.GetCurrentTime();
            dr["RequestDateTime"] = DateTime.Now;
            dr["RequestDateTimeDetail"] = DateTime.Now.ToFileTime();
            dr["IsChangePass"] = false;
            dr["ModifiedDate"] = DateTime.Now;
            ResetManager.AddRow(dr);

            if (ResetManager.Save() > 0)
            {
               _RpId= Utility.EncryptQS(ResetManager[0]["RpId"].ToString());

               //if (SendSMSNotification(_MeId, Utility.DecryptQS( _UltId), _MobileNo, SMSBody)== false)
               //{
               //    ShowError("امکان ارسال کلید اعتبار سنجی به تلفن همراه شما وجود ندارد. لطفاً به دفتر سازمان مراجعه نمایید");
               //    return;
               //}

                SendSMSNotification(Utility.Notifications.NotificationTypes.PasswordForget, SMSBody, _MobileNo, _MeId,Convert.ToInt32( Utility.DecryptQS(_UltId)));


            }
            btnSendEmail.Visible = false;
            btnTextMessage.Visible = false;
            QuestionPhone.Visible = false;
            QuestionEmail.Visible = false;

            lblValidationKey.Visible = true;
            btnVerifyValKey.Visible = true;
            txtValidationKey.Visible = true;
            lblMobileNo3.Visible = true;
            lblMobileNo2.InnerHtml = _HashMobileNo;
            _HashSMSBody = Utility.EncryptQS(SMSBody);
          
        }
        catch (Exception err)
        {
           
            Utility.SaveWebsiteError(err);
            ShowError("امکان ارسال کلید اعتبار سنجی به تلفن همراه شما وجود ندارد. لطفاً به دفتر سازمان مراجعه نمایید");
        }

     
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
             TSP.DataManager.ResetPasswordManager ResetManager = new TSP.DataManager.ResetPasswordManager();

        try
        {
            //get email and mobile phone

            DataRow dr = ResetManager.NewRow();
            dr["MeId"] = _MeId;
            dr["Type"] = Utility.DecryptQS(_UltId);
            dr["RequestUserId"] = Utility.DecryptQS(_UserId);
            dr["RequestIpAddress"] = Request.UserHostAddress;
            dr["RequestDate"] = Utility.GetDateOfToday();
            dr["RequestTime"] = Utility.GetCurrentTime();
            dr["RequestDateTime"] = DateTime.Now;
            dr["RequestDateTimeDetail"] = DateTime.Now.ToFileTime();
            dr["IsChangePass"] = false;
            dr["ModifiedDate"] = DateTime.Now;
            ResetManager.AddRow(dr);

            if (ResetManager.Save() > 0)
            {
                if (SendEmail(_Email, ResetManager[0]["RpId"].ToString(), Utility.DecryptQS(_UserId)) == false)
                {
                    ShowError("امکان ارسال رمز عبور به پست الکترونیکی شما وجود ندارد. لطفاً به دفتر سازمان مراجعه نمایید");
                    return;
                }
                ShowMessage("اطلاعات مربوط به تغییر رمز عبور به پست الکترونیکی شما ارسال شد");
            }
            else
            {
                ShowError("امکان ارسال رمز عبور به پست الکترونیکی شما وجود ندارد. لطفاً به دفتر سازمان مراجعه نمایید");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowError("امکان ارسال رمز عبور به پست الکترونیکی شما وجود ندارد. لطفاً به دفتر سازمان مراجعه نمایید");
        }

    }
    protected void btnVerifyValKey_Click(object sender, EventArgs e)
    {
        if (txtValidationKey.Text.ToLower() == Utility.DecryptQS(_HashSMSBody).ToLower())
        {
            Response.Redirect(Utility.GetWebSiteAddress() + "/ChangeForgottenPassword.aspx?Id=" + _RpId + "&Usr=" + _UserId);
        }
        else
        {
            ShowError("کلید معتبر نمی باشد");
            return;
        }
    
    }
    
    Boolean CheckCondition(int MeId, int UltId)
    {
        switch (UltId)
        {
            case (int)TSP.DataManager.UserType.Member:
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count > 0 && Convert.ToInt32(MemberManager[0]["MrsId"]) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                {
                    ShowInputError("امکان بازیابی رمز عبور برای اعضایی که به سایر استان ها انتقال یافته اند ، وجود ندارد.");
                    return false;
                }
                break;
        }
        return true;
    }

    System.Collections.ArrayList GetInfo(int MeId, int UltId)
    {
        ArrayList List = new ArrayList();
        List.Add("");
        List.Add("");

        switch (UltId)
        {
            case (int)TSP.DataManager.UserType.Member:
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(MemberManager[0]["Email"]))
                        List[0] = MemberManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MobileNo"]))
                        List[1] = MemberManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:
                TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
                TempMemberManager.FindByCode(MeId);
                if (TempMemberManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["Email"]))
                        List[0] = TempMemberManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TempMemberManager[0]["MobileNo"]))
                        List[1] = TempMemberManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.Office:
                TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
                OfficeManager.FindByCode(MeId);
                if (OfficeManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["Email"]))
                        List[0] = OfficeManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["MobileNo"]))
                        List[1] = OfficeManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.Employee:
                TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
                EmployeeManager.FindByCode(MeId);
                if (EmployeeManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["Email"]))
                        List[0] = EmployeeManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["MobileNo"]))
                        List[1] = EmployeeManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.Teacher:
                TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                TeacherManager.FindByCode(MeId);
                if (TeacherManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(TeacherManager[0]["Email"]))
                        List[0] = TeacherManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TeacherManager[0]["MobileNo"]))
                        List[1] = TeacherManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.Institute:
                TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
                InstitueManager.FindByCode(MeId);
                if (InstitueManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(InstitueManager[0]["Email"]))
                        List[0] = InstitueManager[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(InstitueManager[0]["MobileNo"]))
                        List[1] = InstitueManager[0]["MobileNo"].ToString();
                }

                break;
            case (int)TSP.DataManager.UserType.TSProjectOwner:
                TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
                OwnerManager.FindOwnerAgent(MeId);
                if (OwnerManager.Count == 1)
                {
                        List[0] = "";
                    if (!Utility.IsDBNullOrNullValue(OwnerManager[0]["MobileNo"]))
                        List[1] = OwnerManager[0]["MobileNo"].ToString();
                }
                break;
        }
        return List;
    }

    Boolean SendEmail(String Email, String ID, String UserId)
    {
        string url = Utility.GetWebSiteAddress() + "/ChangeForgottenPassword.aspx?Id=" + Utility.EncryptQS(ID) + "&Usr=" + Utility.EncryptQS(UserId);

        Utility.Notifications Notification = new Utility.Notifications(Utility.Notifications.SendTypes.Email, Utility.Notifications.NotificationTypes.PasswordForget);

        DataRow dr = Notification.NotificationData.NewRow();
        dr["Url"] = url;
        dr["EmailAddress"] = Email;
        Notification.NotificationData.Rows.Add(dr);
        Notification.NotificationData.AcceptChanges();

        return Notification.Send();
    }

    //private Boolean SendSMSNotification(string SMSMeId, string SMSUltId, string SMSMobileNo, string SMSBody)
    //{

    //    SendSMSNotification SMSNotifications = new SendSMSNotification(Utility.Notifications.NotificationTypes.AutomaticSMS);

    //    String  SMSResult = "";
    //    SMSBody =  SMSBody.Trim();

    //    if (string.IsNullOrEmpty(SMSBody)) return false;
    //    SMSNotifications.SMSBodyFromWorkFlow = SMSBody;

    //    if (String.IsNullOrWhiteSpace(SMSMobileNo))return false;
       
    //        DataRow dr = SMSNotifications.NotificationData.NewRow();
    //        dr["SMSMobileNo"] = SMSMobileNo;
    //        dr["SMSResult"] = SMSResult;
    //        dr["SMSMeId"] = SMSMeId;
    //        dr["SMSUltId"] = SMSUltId;

    //        SMSNotifications.NotificationData.Rows.Add(dr);
    //        SMSNotifications.NotificationData.AcceptChanges();

    //        switch (Utility.GetCurrentSMSWebService())
    //        {
    //            case (int)TSP.DataManager.SMSWebServiceType.Magfa:
    //               return SMSNotifications.SendSMSByMagfa();
                   
    //            case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
    //                return SMSNotifications.SendSMS();
    //        }
    //        return false;
    //}


    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId,int SMSUltId)
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
            dr["SMSUltId"] = SMSUltId;// ((int)TSP.DataManager.UserType.Member).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
    void ShowError(String Error)
    {
        ControlVisible(false);
        lblMessage.Visible = true;
        //lblMessage.ForeColor = System.Drawing.Color.DarkRed;
        lblMessage.InnerHtml = Error;
    }

    void ShowMessage(String Error)
    {
        ControlVisible(false);
        lblMessage.Visible = true;
        //lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
        lblMessage.InnerHtml = Error;
    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
    }
    void ControlVisible(bool Visible)
    {
        lblMessage.Visible=
        txtUserName.Visible = 
        QuestionPhone.Visible = 
        QuestionEmail.Visible = 
        txtValidationKey.Visible = 
        lblMobileNo3.Visible = 
        Captcha.Visible =
        btnVerifyValKey.Visible =
        btnTextMessage.Visible=
        btnSendEmail.Visible=
        btnNext.Visible= Visible;
    }
}
