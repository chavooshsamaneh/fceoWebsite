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

public partial class Users_ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ResetPasswordManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //BtnNew.Enabled = per.CanNew;
            //btnNew1.Enabled = per.CanNew;
            //btnResetSave.Enabled = per.CanNew;

            btnChangePassword.Enabled = per.CanNew;
            btnChangePassword2.Enabled = per.CanNew;
            //btnPrint.Enabled = per.CanView;
            //btnPrint2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;

            btnPrint.Enabled = false;
            btnPrint2.Enabled = false;
            CustomAspxDevGridView1.JSProperties["cpPrint"] = "";
            CustomAspxDevGridView1.JSProperties["cpPrintPath"] = "";

            // ViewState["UserName"] = "";
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            HiddenUserData["UId"] = "";
            HiddenUserData["P"] = "";
            this.ViewState["GridView"] = CustomAspxDevGridView1.Visible;
            this.ViewState["BtnChangePassword"] = btnChangePassword.Enabled;

            if (string.IsNullOrEmpty(Request.QueryString["Type"]) || string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            try
            {
                HDType.Value = Server.HtmlDecode(Request.QueryString["Type"]).ToString();
                HDId.Value = Server.HtmlDecode(Request.QueryString["ID"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            string Type = Utility.DecryptQS(HDType.Value);
            string ID = Utility.DecryptQS(HDId.Value);

            ShowHeaderText();
            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Type))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSourceResetPass.SelectParameters[0].DefaultValue = GetUltId(Convert.ToInt32(Type)).ToString();
            ObjectDataSourceResetPass.SelectParameters[1].DefaultValue = ID;
        }

        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["GridView"] != null)
            this.CustomAspxDevGridView1.Visible = (bool)this.ViewState["GridView"];
        if (this.ViewState["BtnChangePassword"] != null)
            this.btnChangePassword.Enabled = this.btnChangePassword2.Enabled = (bool)this.ViewState["BtnChangePassword"];
    }

    private void ShowHeaderText()
    {
        int Type = int.Parse(Utility.DecryptQS(HDType.Value));
        String HdId = Utility.DecryptQS(HDId.Value);
        if (string.IsNullOrEmpty(HdId) || string.IsNullOrEmpty(Type.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            int ID = Convert.ToInt32(Utility.DecryptQS(HDId.Value));
            switch (Type)
            {
                case (int)TSP.DataManager.ResetPasswordType.Member:
                    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                    MeManager.FindByCode(ID);
                    if (MeManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = MeManager[0]["FirstName"].ToString();
                        String LastName = MeManager[0]["LastName"].ToString();
                        SetHeaderText("شخص حقیقی", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TempMember:
                    TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
                    TempMemberManager.FindByCode(ID);
                    if (TempMemberManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = TempMemberManager[0]["FirstName"].ToString();
                        String LastName = TempMemberManager[0]["LastName"].ToString();
                        SetHeaderText("شخص حقیقی", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Office:
                    TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
                    OffManager.FindByCode(ID);
                    if (OffManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;

                    }
                    else
                    {
                        String OfName = OffManager[0]["OfName"].ToString();
                        SetHeaderText("شخص حقوقی", OfName);
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Teacher:
                    TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                    TeacherManager.FindByCode(ID);
                    if (TeacherManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;

                    }
                    else
                    {
                        String Name = TeacherManager[0]["Name"].ToString();
                        String Family = TeacherManager[0]["Family"].ToString();
                        SetHeaderText("استاد", String.Format("{0} {1}", Name, Family));
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Institue:
                    TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
                    InsManager.FindByCode(ID);
                    if (InsManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String InsName = InsManager[0]["InsName"].ToString();
                        SetHeaderText("موسسه", InsName);
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Employee:
                    TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
                    EmpManager.FindByCode(ID);
                    if (EmpManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = EmpManager[0]["FirstName"].ToString();
                        String LastName = EmpManager[0]["LastName"].ToString();
                        SetHeaderText("کارمند", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.MunEmployee:
                    TSP.DataManager.EmployeeManager EmpManager2 = new TSP.DataManager.EmployeeManager();
                    EmpManager2.FindMunEmpByEmpId(ID);
                    if (EmpManager2.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = EmpManager2[0]["FirstName"].ToString();
                        String LastName = EmpManager2[0]["LastName"].ToString();
                        SetHeaderText("کارمند شهرداری", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Settlement:
                    TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
                    SettlementAgentManager.FindByCode(ID);
                    if (SettlementAgentManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = SettlementAgentManager[0]["Name"].ToString();
                        String LastName = SettlementAgentManager[0]["Family"].ToString();
                        SetHeaderText("نماینده مسکن", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;

                case (int)TSP.DataManager.ResetPasswordType.TsProjectOwner:
                    TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
                    OwnerManager.FindOwnerAgent(ID);
                    if (OwnerManager.Count == 0)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        String FirstName = OwnerManager[0]["FirstName"].ToString();
                        String LastName = OwnerManager[0]["LastName"].ToString();
                        SetHeaderText("مالک پروژه ساختمانی", String.Format("{0} {1}", FirstName, LastName));
                    }
                    break;
            }
        }
    }

    private void SetHeaderText(String Des1, String Des2)
    {
        RoundPanelResetPassword.HeaderText = String.Format("{0} : {1}", Des1, Des2);
        //ViewState["UserName"] = Des2;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        int Type = int.Parse(Utility.DecryptQS(HDType.Value));
        if (!string.IsNullOrEmpty(Type.ToString()))
        {
            switch (Type)
            {
                case (int)TSP.DataManager.ResetPasswordType.Member:
                    Response.Redirect("~/Employee/MembersRegister/Members.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TempMember:
                    Response.Redirect("~/Employee/MembersRegister/Members.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Office:
                    Response.Redirect("~/Employee/OfficeRegister/Office.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Teacher:
                    Response.Redirect("~/Employee/Amoozesh/Teachers.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Institue:
                    Response.Redirect("~/Employee/Amoozesh/Institue.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Employee:
                    Response.Redirect("~/Employee/Employee/Employee.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.MunEmployee:
                    Response.Redirect("~/Employee/Management/MunEmployee.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Settlement:
                    Response.Redirect("~/Employee/Management/SettlementAgent.aspx");
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TsProjectOwner:
                    Response.Redirect("~/Employee/TechnicalServices/Project/Project.aspx");
                    break;
            }


        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpPrint"] = "";
        CustomAspxDevGridView1.JSProperties["cpPrintPath"] = "";
        String[] Parameters = e.Parameters.Split(';');
        if (Parameters[0] == "Print")
        {
            int TypeUser = int.Parse(Utility.DecryptQS(HDType.Value));
            int UltId = GetUltId(TypeUser);
            int MeId = int.Parse(Utility.DecryptQS(HDId.Value));
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindByMeIdUltId(MeId, UltId);
            CustomAspxDevGridView1.JSProperties["cpPrint"] = "1";
            CustomAspxDevGridView1.JSProperties["cpPrintPath"] = "../ReportForms/UserInfoReport.aspx?UId=" + Parameters[1] + "&P=" + Parameters[2];
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        int TypeUser = int.Parse(Utility.DecryptQS(HDType.Value));
        int MeId = int.Parse(Utility.DecryptQS(HDId.Value));
        String Password = (new Random().Next(0, 1000000)).ToString();

        string Email = "";
        string MobileNo = "";
        int UltId = GetUltId(TypeUser);
        //String IdNo =
        GetIdNoAndEmail(TypeUser, MeId, ref Email, ref MobileNo);
        string Name = "";
        //if (IdNo == null || UltId == -1)
        //{
        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        //    return;
        //}

        TSP.DataManager.ResetPasswordManager ResetManager = new TSP.DataManager.ResetPasswordManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();

        trans.Add(LogManager);
        trans.Add(ResetManager);

        try
        {
            LogManager.FindByMeIdUltId(MeId, UltId);
            if (LogManager.Count > 0)
            {
                trans.BeginSave();

                DataRow dr = ResetManager.NewRow();
                dr["MeId"] = MeId;
                dr["Type"] = UltId;
                //dr["LetterNo"] = txtLeNo.Text;
                //dr["LetterDate"] = txtLeDate.Text;
                dr["RequestUserId"] = LogManager[0]["UserId"].ToString();
                dr["RequestIpAddress"] = Request.UserHostAddress;
                dr["RequestDate"] = Utility.GetDateOfToday();
                dr["RequestTime"] = Utility.GetCurrentTime();
                dr["RequestDateTime"] = DateTime.Now;
                dr["RequestDateTimeDetail"] = DateTime.Now.ToFileTime();
                dr["IsChangePass"] = true;
                dr["ChangeDate"] = Utility.GetDateOfToday();
                dr["ChangeTime"] = Utility.GetCurrentTime();
                dr["ChangeDateTime"] = DateTime.Now;
                dr["ChangeIpAddress"] = Request.UserHostAddress;
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                ResetManager.AddRow(dr);
                if (ResetManager.Save() > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(LogManager[0]["UserName"]))
                        Name = LogManager[0]["UserName"].ToString();
                    LogManager[0].BeginEdit();
                    LogManager[0]["Password"] = Utility.EncryptPassword(Password);
                    LogManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
                    LogManager[0]["ModifiedDate"] = DateTime.Now;
                    LogManager[0].EndEdit();
                    if (LogManager.Save() > 0)
                    {
                        trans.EndSave();
                        HiddenUserData["UId"] = Utility.EncryptQS(LogManager[0]["UserId"].ToString());
                        HiddenUserData["P"] = Utility.EncryptQS(Password);
                        ViewState["btnPrint"] = true;
                        btnPrint.Enabled = true;
                        btnPrint2.Enabled = true;
                        //txtLeDate.Text = "";
                        //txtLeNo.Text = "";
                        this.DivReport.Visible = true;
                        //this.LabelWarning.Text = "بازیابی رمز عبور با موفقیت انجام شد.";
                        this.LabelWarning.Text = "  رمز عبور به " + Password + "  تغییر یافت";

                        try
                        {
                            if (string.IsNullOrEmpty(Email.Trim()) == false)
                                SendEmail(Email, Password);
                        }
                        catch (Exception ex)
                        {
                            Utility.SaveWebsiteError(ex);
                        }                        
                        try
                        {
                            if (!string.IsNullOrEmpty(MobileNo))
                                SendSMSNotification(Utility.Notifications.NotificationTypes.ResetPassword, "نام کاربری: " + Name + "\n" + "رمز عبور: " + Password, MobileNo, MeId.ToString(), UltId);
                        }
                        catch (Exception ex)
                        {
                            Utility.SaveWebsiteError(ex);
                        }
                        return;
                    }
                    else
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        if (Utility.ShowExceptionError())
                            this.LabelWarning.Text = "Save to login failed";
                        return;
                    }
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    if (Utility.ShowExceptionError())
                        this.LabelWarning.Text = "Save to Reset failed";
                    return;
                }
            }
            else
            {
                trans.CancelSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                if (Utility.ShowExceptionError())
                    this.LabelWarning.Text = "Fiailed to find member";
                return;
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
            if (Utility.ShowExceptionError())
                this.LabelWarning.Text = err.Message;
        }


    }

    void GetIdNoAndEmail(int Type, int ID, ref String Email, ref string MobileNo)
    {
        try
        {
            switch (Type)
            {
                case (int)TSP.DataManager.ResetPasswordType.Member:
                    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                    MeManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(MeManager[0]["Email"]) == false)
                        Email = MeManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(MeManager[0]["MobileNo"]) == false)
                        MobileNo = MeManager[0]["MobileNo"].ToString();

                    break;
                case (int)TSP.DataManager.ResetPasswordType.TempMember:
                    TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
                    TempMemberManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(TempMemberManager[0]["Email"]) == false)
                        Email = TempMemberManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(TempMemberManager[0]["MobileNo"]) == false)
                        MobileNo = TempMemberManager[0]["MobileNo"].ToString();
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Office:
                    TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
                    OffManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(OffManager[0]["Email"]) == false)
                        Email = OffManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(OffManager[0]["MobileNo"]) == false)
                        MobileNo = OffManager[0]["MobileNo"].ToString();
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Teacher:
                    TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                    TeacherManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(TeacherManager[0]["Email"]) == false)
                        Email = TeacherManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(TeacherManager[0]["MobileNo"]) == false)
                        MobileNo = TeacherManager[0]["MobileNo"].ToString();
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Institue:
                    TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
                    InsManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(InsManager[0]["Email"]) == false)
                        Email = InsManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(InsManager[0]["MobileNo"]) == false)
                        MobileNo = InsManager[0]["MobileNo"].ToString();
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Employee:
                    TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
                    EmpManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(EmpManager[0]["Email"]) == false)
                        Email = EmpManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(EmpManager[0]["MobileNo"]) == false)
                        MobileNo = EmpManager[0]["MobileNo"].ToString();
                    break;
                //case (int)TSP.DataManager.ResetPasswordType.MunEmployee:
                //    TSP.DataManager.EmployeeManager MunEmpManager = new TSP.DataManager.EmployeeManager();
                //    MunEmpManager.FindMunEmpByEmpId(ID);
                //    if (Utility.IsDBNullOrNullValue(MunEmpManager[0]["Email"]) == false)
                //        Email = MunEmpManager[0]["Email"].ToString();
                //    break;
                case (int)TSP.DataManager.ResetPasswordType.Settlement:
                    TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
                    SettlementAgentManager.FindByCode(ID);
                    if (Utility.IsDBNullOrNullValue(SettlementAgentManager[0]["Email"]) == false)
                        Email = SettlementAgentManager[0]["Email"].ToString();
                    if (Utility.IsDBNullOrNullValue(SettlementAgentManager[0]["MobileNo"]) == false)
                        MobileNo = SettlementAgentManager[0]["MobileNo"].ToString();
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TsProjectOwner:
                    TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
                    OwnerManager.FindOwnerAgent(ID);
                    if (OwnerManager.Count != 0)
                    {
                        MobileNo = OwnerManager[0]["MobileNo"].ToString();
                    }
                    Email = "";
                    break;
            }
        }
        catch (Exception) { }
    }

    int GetUltId(int Type)
    {
        int UserType = -1;
        try
        {
            switch (Type)
            {
                case (int)TSP.DataManager.ResetPasswordType.Member:
                    UserType = (int)TSP.DataManager.UserType.Member;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TempMember:
                    UserType = (int)TSP.DataManager.UserType.TemporaryMembers;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Office:
                    UserType = (int)TSP.DataManager.UserType.Office;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Teacher:
                    UserType = (int)TSP.DataManager.UserType.Teacher;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Institue:
                    UserType = (int)TSP.DataManager.UserType.Institute;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Employee:
                    UserType = (int)TSP.DataManager.UserType.Employee;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.MunEmployee:
                    UserType = (int)TSP.DataManager.UserType.Municipality;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.Settlement:
                    UserType = (int)TSP.DataManager.UserType.Settlement;
                    break;
                case (int)TSP.DataManager.ResetPasswordType.TsProjectOwner:
                    UserType = (int)TSP.DataManager.UserType.TSProjectOwner;
                    break;
            }
        }
        catch (Exception) { }
        return UserType;
    }

    Boolean SendEmail(String Email, String Password)
    {
        Utility.Notifications Notification = new Utility.Notifications(Utility.Notifications.SendTypes.Email, Utility.Notifications.NotificationTypes.ResetPassword);

        DataRow dr = Notification.NotificationData.NewRow();
        dr["EmailAddress"] = Email;
        dr["Password"] = Password;
        Notification.NotificationData.Rows.Add(dr);
        Notification.NotificationData.AcceptChanges();

        return Notification.Send();
    }    

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId, int SMSUltId)
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
            dr["SMSUltId"] = SMSUltId.ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
}
