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
using System.Collections;
using DevExpress.Web;



public partial class Employee_SMS_NewSms : System.Web.UI.Page
{
    #region Private Members
    private System.Collections.ArrayList arlMembers = new ArrayList();
    private System.Collections.ArrayList arlEmployees = new ArrayList();
    private System.Collections.ArrayList arlManualNos = new ArrayList();
    private System.Collections.ArrayList arlGroups = new ArrayList();
    private System.Collections.ArrayList arlConfPerson = new ArrayList();
    #endregion

    int _SMSId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldSMSDetail["SMSId"]);
        }
        set
        {
            HiddenFieldSMSDetail["SMSId"] = value.ToString();
        }
    }
    #region Evetns

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


        if (Request.QueryString["SMSID"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["PgName"] == null)
        {
            Response.Redirect("ConfirmedSMS.aspx");
        }

        if (!IsPostBack)
        {
            CallbackPanelSMSBody.JSProperties["cpCostId"] = "";
            CallbackPanelSMSBody.JSProperties["cpError"] = "";
            CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] = "";
            hiddenRecieverManualNo["MobileNo"] = "";
            //hiddenRecieverEmployee["NcName"] = "";
            hiddenRecieverEmployee["Id"] = "";
            hiddenRecieverMembers["Members"] = "";
            hiddenRecieverMembers["MembersNoNum"] = "";
            HiddenFieldSMSDetail["PgName"] = Request.QueryString["PgName"];

            TSP.DataManager.Permission Per = TSP.DataManager.SmsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = Per.CanNew;
            btnSave2.Enabled = Per.CanNew;

            //cmbSMSType.DataBind();
            //cmbSMSType.Items.Insert(0, new ListEditItem("----------", null));
            //cmbSMSType.SelectedIndex = 0;

            HiddenFieldSMSDetail["Cost"] = "";
            HiddenFieldSMSDetail["CostID"] = "";
            // Session["RecieverCount"] = "";
            SetKeys(true);

            this.ViewState["BtnSend"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnRecievers"] = btnRecievers.Visible;

        }
        lstNezamChartReciever.DataBind();
        if (this.ViewState["BtnSend"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSend"];
        if (this.ViewState["btnRecievers"] != null)
            this.btnRecievers.Visible = (bool)this.ViewState["btnRecievers"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string DateNow = Utility.GetDateOfToday();
        if (string.Compare(txtbExipreDate.Text, DateNow) < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "پایان مهلت ارسال پیام کوتاه نمی تواند از تاریخ قبل از امروز باشد.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldSMSDetail["PageMode"].ToString());

        if (PageMode=="New" && string.Compare(txtbSMSDotoDate.Text, DateNow) < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شروع مهلت ارسال پیام کوتاه نمی تواند از تاریخ قبل از امروز باشد.";
            return;
        }

        if (string.Compare(txtbExipreDate.Text, txtbSMSDotoDate.Text) < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تاریخ پایان مهلت ارسال پیام کوتاه نمی تواند قبل از تاریخ شروع مهلت ارسال باشد.";
            return;
        }
        Utility.Date Date = new Utility.Date(txtbSMSDotoDate.Text);
        if (string.Compare(Date.AddDays(7), txtbExipreDate.Text) < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تاریخ پایان مهلت ارسال پیام کوتاه نمی تواند بیش از 7 روز پس از تاریخ شروع مهلت ارسال باشد.";
            return;
        }
        switch (PageMode)
        {
            case "New":
                InsertSMS();
                break;
            case "Edit":
                if (Utility.IsDBNullOrNullValue(_SMSId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                EditSMS(_SMSId);
                break;
        }
    }

    protected void CallbackPanelSMSBody_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelSMSBody.JSProperties["cpError"] = "";
        CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] = "";
        CallbackPanelSMSBody.JSProperties["cpCostId"] = "";
        string[] parameter = e.Parameter.Split('#');
        if (parameter[1] == "Reciever")
            SetRecieverWithoutTel(parameter[0]);

        if (Convert.ToInt32(txtbRecieverCount.Text) > 0)
        {
            String Error = "";
            CalculateCost(IsLanguageEnglish(), ref Error);
            CallbackPanelSMSBody.JSProperties["cpError"] = Error;
        }
        else
            txtbSMSCost.Text = "0";

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string PageName = Utility.DecryptQS(HiddenFieldSMSDetail["PgName"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&PostId=" + Utility.EncryptQS(_SMSId.ToString()));
        }
        else
            Response.Redirect(PageName);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewSms.aspx?SMSId=" + Request.QueryString["SMSId"] + "&PgMd=" + Utility.EncryptQS("Edit") + "&PgName=" + Request.QueryString["PgName"]);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewSms.aspx?SMSId=" + Request.QueryString["SMSId"] + "&PgMd=" + Utility.EncryptQS("New") + "&PgName=" + Request.QueryString["PgName"]);
    }

    protected void WFUserControl_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Utility.IsDBNullOrNullValue(_SMSId))
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("ابتدا پیام کوتاه را ذخیره نمایید");
            return;
        }

        int SMSTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.SMS);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(SMSTableType, _SMSId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        string Qs = "~/Employee/SMS/NewSms.aspx?" + "PgMd=" + HiddenFieldSMSDetail["PageMode"].ToString()
            + "&PgName=" + Request.QueryString["PgName"]
            + "&SMSId=" + Utility.EncryptQS(_SMSId.ToString());

        WFUserControl.QueryStringForRedirect = Qs;
        WFUserControl.PerformCallback(_SMSId, SMSTableType, WfCode, e);
        SetKeys(false);
    }
    #endregion

    #region Methods
    private void SetEnabled(Boolean Enabled)
    {
        cmbPartition.Enabled = Enabled;

        //txtbSMSBody.Disabled = !Enabled;
        if (Enabled)
            txtbSMSBody.Attributes.Remove("readonly");
        else
            txtbSMSBody.Attributes.Add("readonly", "true");

        cmbSMSType.Enabled = Enabled;
        txtbExipreDate.Enabled = Enabled;
        txtbSMSDotoDate.Enabled = Enabled;
        btnRecievers.Visible = Enabled;
        txtbSubject.Enabled = Enabled;
    }

    private void SetKeys(Boolean SetParameters = true)
    {
        if (SetParameters)
        {
            _SMSId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SMSId"].ToString()));
            HiddenFieldSMSDetail["PageMode"] = Request.QueryString["PgMd"];
        }
        string PageMode = Utility.DecryptQS(HiddenFieldSMSDetail["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
        //if (PageMode != "New")
        CheckWorkFlowPermission();
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        btnEdit2.Enabled = per.CanEdit;
        btnEdit.Enabled = per.CanEdit;

        if (Utility.IsDBNullOrNullValue(_SMSId))
        {
            Response.Redirect("ConfirmedSMS.aspx");
            return;
        }
        FillForm(_SMSId);
        SetEnabled(false);
        this.ViewState["BtnSend"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnRecievers"] = btnRecievers.Visible;
        RoundPanelSMS.HeaderText = "مشاهده";
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        if (Utility.IsDBNullOrNullValue(_SMSId))
        {
            Response.Redirect("OutBox.aspx");
            return;
        }
        FillForm(_SMSId);
        SetEnabled(true);
        this.ViewState["BtnSend"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnRecievers"] = btnRecievers.Visible;
        RoundPanelSMS.HeaderText = "ویرایش";
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        ClearForm();
        LoadCredit();
        SetEnabled(true);
        RoundPanelSMS.HeaderText = "جدید";
        this.ViewState["BtnSend"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnRecievers"] = btnRecievers.Visible;
    }

    private void FillForm(int SMSId)
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();

        SmsManager.FindByCode(SMSId);
        if (SmsManager.Count == 1)
        {
            txtbSubject.Text = SmsManager[0]["SmsSubject"].ToString();
            txtbSMSBody.Value = SmsManager[0]["SmsBody"].ToString();
            txtbExipreDate.Text = SmsManager[0]["ExpireDate"].ToString();
            txtbSMSDotoDate.Text = SmsManager[0]["SMSDotoDate"].ToString();
            cmbSMSType.DataBind();
            if (!Utility.IsDBNullOrNullValue(SmsManager[0]["SmsTypeId"]))
            {
                SmsTypeManager.FindByCode((int)SmsManager[0]["SmsTypeId"]);
                if (SmsTypeManager.Count == 1)
                    cmbSMSType.SelectedIndex = cmbSMSType.Items.FindByValue(SmsManager[0]["SmsTypeId"].ToString()).Index;
                else
                    cmbSMSType.SelectedIndex = -1;
            }
            if (!Utility.IsDBNullOrNullValue(SmsManager[0]["PartId"]))
            {
                cmbPartition.DataBind();
                cmbPartition.SelectedIndex = cmbPartition.Items.FindByValue(SmsManager[0]["PartId"].ToString()).Index;
            }
            DataTable dtSMSReciever = SmsRecieverManager.FindTotalRecieversBySMSId_StringMode(SMSId);

            if (dtSMSReciever.Rows.Count > 0)
            {
                string Recievers = "";
                string RecieverNoNumber_Member = "", RecieverNoNumber_Employee = "";
                int CountRecieverNoNumeber = 0;

                #region Reciever_Member
                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["SmsReIdsMe"]))
                    hiddenRecieverMembers["Members"] = dtSMSReciever.Rows[0]["SmsReIdsMe"].ToString();
                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["SmsReIdsMeNull"]))
                    RecieverNoNumber_Member = dtSMSReciever.Rows[0]["SmsReIdsMeNull"].ToString();

                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["SmsReIdsEmp"]))
                    hiddenRecieverEmployee["Id"] = dtSMSReciever.Rows[0]["SmsReIdsEmp"].ToString();
                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["SmsReIdsEmpNull"]))
                    RecieverNoNumber_Employee = dtSMSReciever.Rows[0]["SmsReIdsEmpNull"].ToString();

                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["RecieverManuCellPhones"]))
                    hiddenRecieverManualNo["MobileNo"] = dtSMSReciever.Rows[0]["RecieverManuCellPhones"].ToString();
                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["CountNull"]))
                    CountRecieverNoNumeber = Convert.ToInt32(dtSMSReciever.Rows[0]["CountNull"]);
                #endregion

                //for (int i = 0; i < dtSMSReciever.Rows.Count; i++)
                //{
                //    if (Convert.ToInt32(dtSMSReciever.Rows[i]["RecieverType"]) == (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member)
                //    {
                //        #region Reciever_Member
                //        if (hiddenRecieverMembers["Members"].ToString() != "")
                //            hiddenRecieverMembers["Members"] = hiddenRecieverMembers["Members"] += ";";
                //        hiddenRecieverMembers["Members"] += dtSMSReciever.Rows[i]["RecieverId"].ToString();
                //        if (Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[i]["RecieverCellPhone"]))
                //        {
                //            if (String.IsNullOrEmpty(RecieverNoNumber_Member) == false)
                //                RecieverNoNumber_Member += ";";
                //            RecieverNoNumber_Member += dtSMSReciever.Rows[i]["RecieverId"].ToString();
                //            CountRecieverNoNumeber++;
                //        }
                //        #endregion
                //    }
                //    else if (Convert.ToInt32(dtSMSReciever.Rows[i]["RecieverType"]) == (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee)
                //    {
                //        #region Reciever_Employee
                //        if (hiddenRecieverEmployee["Id"].ToString() != "")
                //            hiddenRecieverEmployee["Id"] = hiddenRecieverEmployee["Id"] += ";";
                //        hiddenRecieverEmployee["Id"] += dtSMSReciever.Rows[i]["RecieverId"].ToString();
                //        if (Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[i]["RecieverCellPhone"]))
                //        {
                //            if (String.IsNullOrEmpty(RecieverNoNumber_Employee) == false)
                //                RecieverNoNumber_Employee += ";";
                //            RecieverNoNumber_Employee += dtSMSReciever.Rows[i]["RecieverId"].ToString();
                //            CountRecieverNoNumeber++;
                //        }
                //        #endregion
                //    }
                //    else if (Convert.ToInt32(dtSMSReciever.Rows[i]["RecieverType"]) == (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.ManualInsert)
                //    {
                //        #region Reciever_ManualNo
                //        if (hiddenRecieverManualNo["MobileNo"].ToString() != "")
                //            hiddenRecieverManualNo["MobileNo"] = hiddenRecieverManualNo["MobileNo"] += ";";
                //        hiddenRecieverManualNo["MobileNo"] += dtSMSReciever.Rows[i]["RecieverCellPhone"].ToString();
                //        #endregion
                //    }
                //}

                #region Recievers
                if (hiddenRecieverMembers["Members"].ToString() != "")
                {
                    txtRecievers.Text = "اعضای حقیقی : " + hiddenRecieverMembers["Members"];
                    txtMembers_Reciever.Text = hiddenRecieverMembers["Members"].ToString();
                }
                if (hiddenRecieverEmployee["Id"].ToString() != "")
                {
                    String[] Employees = hiddenRecieverEmployee["Id"].ToString().Split(';');
                    String EmplyeeNames = "";
                    lstNezamChartReciever.DataBind();
                    for (int i = 0; i < Employees.Length; i++)
                    {
                        EmplyeeNames += (String.IsNullOrEmpty(EmplyeeNames)) ? "" : ";";
                        ListEditItem tmpListItem = lstNezamChartReciever.Items.FindByValue(Employees[i].ToString());
                        EmplyeeNames += tmpListItem.Text.ToString();
                        tmpListItem.Selected = true;
                    }
                    if (String.IsNullOrEmpty(txtRecievers.Text.Trim()) == false)
                        txtRecievers.Text += "\n";
                    txtRecievers.Text += "کارمندان : " + EmplyeeNames;
                }
                if (hiddenRecieverManualNo["MobileNo"].ToString() != "")
                {
                    if (String.IsNullOrEmpty(txtRecievers.Text.Trim()) == false)
                        txtRecievers.Text += "\n";
                    txtRecievers.Text += "شماره های دستی : " + hiddenRecieverManualNo["MobileNo"];
                    txtOtherMobileNo.Text = hiddenRecieverManualNo["MobileNo"].ToString();
                }
                #endregion

                #region RecieverWithoutTel
                txtbRecieverWithoutTel.Text = "";
                if (String.IsNullOrEmpty(RecieverNoNumber_Member) == false)
                    txtbRecieverWithoutTel.Text = "اعضای حقیقی : " + RecieverNoNumber_Member;

                if (String.IsNullOrEmpty(RecieverNoNumber_Employee) == false)
                {
                    if (String.IsNullOrEmpty(txtbRecieverWithoutTel.Text) == false)
                        txtbRecieverWithoutTel.Text += "\n";
                    String[] EmployeeNoNumber = RecieverNoNumber_Employee.Split(';');
                    String RecieverNoNumber_EmployeeName = "";
                    for (int i = 0; i < EmployeeNoNumber.Length; i++)
                    {
                        RecieverNoNumber_EmployeeName += (String.IsNullOrEmpty(RecieverNoNumber_EmployeeName)) ? "" : ";";
                        RecieverNoNumber_EmployeeName += lstNezamChartReciever.Items.FindByValue(EmployeeNoNumber[i]).Text.ToString();
                    }
                    txtbRecieverWithoutTel.Text += "کارمندان : " + RecieverNoNumber_EmployeeName;
                }
                #endregion

                txtbRecieverCountWithoutTel.Text = CountRecieverNoNumeber.ToString();
                //*************************************
                if (!Utility.IsDBNullOrNullValue(dtSMSReciever.Rows[0]["Counts"]))
                    txtbRecieverCount.Text = dtSMSReciever.Rows[0]["Counts"].ToString();
                //  txtbRecieverCount.Text = dtSMSReciever.Rows.Count.ToString();
            }

            String Error = "";
            CalculateCost(IsLanguageEnglish(), ref Error);
            LoadCredit();
            if (String.IsNullOrEmpty(Error) == false)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Error;
            }
        }
    }

    private void ClearForm()
    {
        cmbPartition.SelectedIndex = -1;
        cmbSMSType.SelectedIndex = 0;
        HiddenFieldSMSDetail["Cost"] =
        HiddenFieldSMSDetail["CostID"] = "";
        _SMSId = -2;
        txtbSMSBody.Value =
        txtRecievers.Text =
        txtbExipreDate.Text = txtbSMSDotoDate.Text =
        txtbRecieverWithoutTel.Text =
        txtbSubject.Text =
        txtMembers_Reciever.Text =
        txtOtherMobileNo.Text = "";
        lstNezamChartReciever.UnselectAll();
        CallbackPanelSMSBody.JSProperties["cpError"] = "";
        CallbackPanelSMSBody.JSProperties["cpCostId"] = "";
        CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] = "";
        hiddenRecieverManualNo["MobileNo"] = "";
        hiddenRecieverEmployee["Id"] = "";
        hiddenRecieverMembers["Members"] = "";
        hiddenRecieverMembers["MembersNoNum"] = "";
        txtbRecieverCount.Text =
        txtbRecieverCountWithoutTel.Text =
        txtbSMSCost.Text =
        txtRemainingCredit.Text = "0";
        HiddenFieldSMSDetail["PgName"] = Request.QueryString["PgName"];
    }

    void SetRecieverWithoutTel(String Recievers)
    {
        String[] arrRecievers = Recievers.Split('&');
        int RecieverNoNumber = 0;

        try
        {
            #region Members
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            String MemberWithoutMobileNo = MemberManager.SelectMemberWithoutMobileNoForSMS(arrRecievers[0]);
            if (MemberWithoutMobileNo != "")
            {
                CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] += "اعضای حقیقی : " + MemberWithoutMobileNo;
                RecieverNoNumber += MemberWithoutMobileNo.Split(';').Length;
            }
            #endregion

            #region Employees
            TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
            String EmployeeWithoutMobileNo = EmployeeManager.SelectEmployeeWithoutMobileNoForSMS(arrRecievers[1]);
            if (EmployeeWithoutMobileNo != "")
            {
                if (String.IsNullOrEmpty(CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"].ToString().Trim()) == false)
                    CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] += "\n";
                CallbackPanelSMSBody.JSProperties["cpRecieverWithoutTel"] += "کارمندان : " + EmployeeWithoutMobileNo;
                RecieverNoNumber += EmployeeWithoutMobileNo.Split(';').Length;
            }
            #endregion

            if (RecieverNoNumber != 0)
            {
                int RecieverCount = Convert.ToInt32(txtbRecieverCount.Text);
                txtbRecieverCount.Text = (RecieverCount - RecieverNoNumber).ToString();
            }

            txtbRecieverCountWithoutTel.Text = RecieverNoNumber.ToString();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    private double CalculateCost(Boolean IsEnglish, ref String Error)
    {
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
        cmbSMSType.DataBind();
        double SmsCosts = 0;

        if (cmbSMSType.SelectedIndex <= -1)
        {
            Error = "اطلاعات نوع پیام توسط کاربر دیگری تغییر یافته است.";
            return ((float)(0));
        }
        int SMSTypeId = int.Parse(cmbSMSType.SelectedItem.Value.ToString());
        DataTable dtSMSType = SmsTypeModifiedManager.SelectByLastModified(SMSTypeId);
        if (dtSMSType.Rows.Count <= 0)
        {
            Error = "اطلاعات نوع پیام توسط کاربر دیگری تغییر یافته است.";
            return ((float)(0));
        }
        if (bool.Parse(dtSMSType.Rows[0]["HasCost"].ToString()))
        {
            //  int ReceiverCount = Convert.ToInt32(txtbRecieverCount.Text) - Convert.ToInt32(txtbRecieverCountWithoutTel.Text);
            int ReceiverCount = Convert.ToInt32(txtbRecieverCount.Text);

            DataTable dtCost = new DataTable();
            if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
                dtCost = SmsCostManager.FindByWebServiceType(1, 1);
            else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
                dtCost = SmsCostManager.FindByWebServiceType(0, 1);
            else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Prdco || Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.PrdcoAsync)
                dtCost = SmsCostManager.FindByWebServiceType(2, 1);
            if (dtCost.Rows.Count > 0)
            {
                if (IsCallback)
                    CallbackPanelSMSBody.JSProperties["cpCostId"] = Utility.EncryptQS(dtCost.Rows[0]["CostId"].ToString());
                else
                    HiddenFieldSMSDetail["CostID"] = Utility.EncryptQS(dtCost.Rows[0]["CostId"].ToString());

                int SMSBodyLenght = txtbSMSBody.Value.Length;
                double SMSCount = 0;

                if (IsEnglish)
                {
                    if (SMSBodyLenght > 0)
                    {
                        SMSCount = Math.Ceiling((double)((double)(SMSBodyLenght) / 160));
                    }
                    SmsCosts = ((float.Parse(dtCost.Rows[0]["CostEn"].ToString())) * ReceiverCount * SMSCount);
                }
                else
                {
                    if (SMSBodyLenght > 0)
                    {
                        SMSCount = Math.Ceiling((double)((double)(SMSBodyLenght) / 70));
                    }
                    SmsCosts = ((float.Parse(dtCost.Rows[0]["CostFr"].ToString())) * ReceiverCount * SMSCount);
                }
            }
            else
            {
                Error = "بدلیل نامشخص بودن هزینه پیام کوتاه،امکان ارسال پیام وجود ندارد";
                SmsCosts = 0;
            }
        }
        else
        {
            SmsCosts = 0;
        }

        txtbSMSCost.Text = SmsCosts.ToString("N0") + " ریال";

        return SmsCosts;




    }

    private Boolean IsLanguageEnglish()
    {
        string SMSBody = this.txtbSMSBody.Value;
        char[] arraysmsbody = new char[SMSBody.Length];
        Boolean isEnglish = true;
        if (SMSBody.Length > 1)
        {
            arraysmsbody = SMSBody.ToCharArray();
            for (int i = 0; i < SMSBody.Length; i++)
            {
                if (arraysmsbody[i] > 128)
                {
                    isEnglish = false;
                    break;
                }
            }
        }
        return (isEnglish);
        //  SMSFirstChar = char.Parse(SMSBody);     
    }

    private void InsertSMS()
    {
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();

        //TSP.DataManager.MessageManager MessageManager = new TSP.DataManager.MessageManager();
        //TSP.DataManager.MessageReceiverManager MessageReceiverManager = new TSP.DataManager.MessageReceiverManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(SmsManager);
        TransactionManager.Add(SmsRecieverManager);
        //TransactionManager.Add(MessageManager);
        //TransactionManager.Add(MessageReceiverManager);
        TransactionManager.Add(WorkFlowStateManager);

        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        //TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        //TSP.DataManager.GroupDetailManager GroupDetailManager = new TSP.DataManager.GroupDetailManager();
        //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        try
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSMSInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            TransactionManager.BeginSave();
            DataRow dtSMSRow = SmsManager.NewRow();

            dtSMSRow["PartId"] = cmbPartition.SelectedItem.Value;
            dtSMSRow["ExpireDate"] = txtbExipreDate.Text;
            dtSMSRow["SMSDotoDate"] = txtbSMSDotoDate.Text;
            dtSMSRow["SMSDate"] = Utility.GetDateOfToday();
            dtSMSRow["SMSTime"] = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            if (IsLanguageEnglish())
            {
                dtSMSRow["IsFarsi"] = 0;
                int SMSBodyLenght = txtbSMSBody.Value.Length;
                if (SMSBodyLenght > 0)
                {
                    dtSMSRow["SmsCount"] = Math.Ceiling((double)((double)(SMSBodyLenght) / 160));
                }
                else
                {
                    TransactionManager.CancelSave();
                    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }
            else
            {
                dtSMSRow["IsFarsi"] = 1;
                int SMSBodyLenght = txtbSMSBody.Value.Length;
                if (SMSBodyLenght > 0)
                {
                    dtSMSRow["SmsCount"] = Math.Ceiling((double)((double)(SMSBodyLenght) / 70));
                }
                else
                {
                    TransactionManager.CancelSave();

                    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }

            String CostError = "";
            double smsCost = CalculateCost(IsLanguageEnglish(), ref CostError);
            if (String.IsNullOrEmpty(CostError) == false)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = CostError;
                return;
            }
            if (HiddenFieldSMSDetail["CostID"] == null)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            dtSMSRow["CostId"] = int.Parse(Utility.DecryptQS(HiddenFieldSMSDetail["CostID"].ToString()));

            dtSMSRow["SmsCost"] = smsCost;

            dtSMSRow["SmsTypeId"] = int.Parse(cmbSMSType.SelectedItem.Value.ToString());
            dtSMSRow["SenderId"] = Utility.GetCurrentUser_MeId();
            dtSMSRow["SmsSubject"] = txtbSubject.Text;
            dtSMSRow["SmsBody"] = txtbSMSBody.Value;
            dtSMSRow["IsDelivered"] = 0;
            dtSMSRow["InActive"] = 0;
            dtSMSRow["ModifiedDate"] = DateTime.Now;
            dtSMSRow["UserId"] = Utility.GetCurrentUser_UserId();

            SmsManager.AddRow(dtSMSRow);
            int cn = SmsManager.Save();
            SmsManager.DataTable.AcceptChanges();
            if (cn <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            //***Insert Into SMS Reciever      
            // if (Session["SelectedMembers"] != null)
            //    arlMembers = (ArrayList)(Session["SelectedMembers"]);
            //   if (Session["SelectedGroups"] != null)
            //      arlGroups = (ArrayList)(Session["SelectedGroups"]);
            ////**************
            #region Reciever_Member
            if (hiddenRecieverMembers.Contains("Members") && hiddenRecieverMembers["Members"] != null && String.IsNullOrEmpty(hiddenRecieverMembers["Members"].ToString().Trim()) == false)
            {
                SmsRecieverManager.InsertSmsRecieverForMember(Convert.ToInt32(SmsManager[0]["SmsId"]), hiddenRecieverMembers["Members"].ToString().Trim(), (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member, Utility.GetCurrentUser_UserId());
            }

            #endregion
            #region Reciever_Employee

            if (hiddenRecieverEmployee.Contains("Id") && hiddenRecieverEmployee["Id"] != null && String.IsNullOrEmpty(hiddenRecieverEmployee["Id"].ToString().Trim()) == false)
            {
                String[] Employees = hiddenRecieverEmployee["Id"].ToString().Trim().Split(';');
                for (int i = 0; i < Employees.Length && String.IsNullOrEmpty(Employees[i]) == false; i++)
                {
                    EmployeeManager.FindByCode(int.Parse(Employees[i].ToString()));
                    if (EmployeeManager.Count > 0)
                    {
                        DataRow dtSMSRecRow = SmsRecieverManager.NewRow();
                        arlEmployees.Add(Employees[i]);
                        dtSMSRecRow["SmsId"] = SmsManager[0]["SmsId"];
                        dtSMSRecRow["RecieverId"] = Employees[i];
                        if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["MobileNo"]) || !string.IsNullOrEmpty(EmployeeManager[0]["MobileNo"].ToString()))
                        {
                            dtSMSRecRow["RecieverCellPhone"] = EmployeeManager[0]["MobileNo"];
                        }
                        dtSMSRecRow["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee;
                        dtSMSRecRow["InActive"] = 0;
                        dtSMSRecRow["ModifiedDate"] = DateTime.Now;
                        dtSMSRecRow["IsDelivered"] = 0;
                        dtSMSRecRow["UserId"] = Utility.GetCurrentUser_UserId();
                        SmsRecieverManager.AddRow(dtSMSRecRow);
                    }
                }
            }
            #endregion
            #region Reciever_ManualNo
            if (hiddenRecieverManualNo.Contains("MobileNo") && hiddenRecieverManualNo["MobileNo"] != null && String.IsNullOrEmpty(hiddenRecieverManualNo["MobileNo"].ToString().Trim()) == false)
            {
                String[] ManualNos = hiddenRecieverManualNo["MobileNo"].ToString().Trim().Split(';');
                for (int i = 0; i < ManualNos.Length && String.IsNullOrEmpty(ManualNos[i]) == false; i++)
                {
                    DataRow dtSMSRecRow = SmsRecieverManager.NewRow();
                    arlManualNos.Add(ManualNos[i]);
                    dtSMSRecRow["SmsId"] = SmsManager[0]["SmsId"];
                    dtSMSRecRow["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.ManualInsert;
                    dtSMSRecRow["RecieverId"] = -1;
                    dtSMSRecRow["RecieverCellPhone"] = ManualNos[i];
                    dtSMSRecRow["InActive"] = 0;
                    dtSMSRecRow["ModifiedDate"] = DateTime.Now;
                    dtSMSRecRow["IsDelivered"] = 0;
                    dtSMSRecRow["UserId"] = Utility.GetCurrentUser_UserId();
                    SmsRecieverManager.AddRow(dtSMSRecRow);
                }
            }
            #endregion

            int cnt = SmsRecieverManager.Save();
            if (cnt < 0)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            CostError = "";
            smsCost = CalculateCost(IsLanguageEnglish(), ref CostError);
            if (String.IsNullOrEmpty(CostError) == false)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = CostError;
                return;
            }
            // SmsManager[0][""] = smsCost;
            SmsManager[0].BeginEdit();
            SmsManager[0]["SmsCost"] = (int.Parse(dtSMSRow["SmsCount"].ToString())) * (smsCost);
            SmsManager[0].EndEdit();
            int cnSave = SmsManager.Save();
            if (SmsManager.Count > 0)
            {
                int TableId = int.Parse(SmsManager[0]["SmsId"].ToString());
                int MeId = Utility.GetCurrentUser_MeId();
                int NmcId = FindNmcId(TaskId);
                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0);
                if (WfStart > 0)
                {
                    TransactionManager.EndSave();
                    _SMSId = Convert.ToInt32(SmsManager[0]["SmsId"]);
                    HiddenFieldSMSDetail["PageMode"] = Utility.EncryptQS("Edit");
                    this.ViewState["BtnSend"] = btnSave.Enabled;
                    SetMessage("ذخیره انجام شد.");
                    SetEditModeKeys();
                }
                else
                {
                    TransactionManager.CancelSave();
                    SetMessage("در ارسال پیام کوتاه خطا ایجاد شد");
                    return;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                SetMessage("در ارسال پیام کوتاه خطا ایجاد شد");
                return;
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    SetMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave)); ;
                }
            }
            else
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
            return;
        }
    }

    private void EditSMS(int SMSId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        SmsRecieverManager.ClearBeforeFill = true;

        TransactionManager.Add(SmsManager);
        TransactionManager.Add(SmsRecieverManager);
        TransactionManager.Add(EmployeeManager);
        TransactionManager.Add(MemberManager);

        try
        {
            string HasNoNumber = "";
            SmsManager.FindByCode(SMSId);
            if (SmsManager.Count == 1)
            {
                TransactionManager.BeginSave();

                SmsManager[0].BeginEdit();
                SmsManager[0]["PartId"] = cmbPartition.SelectedItem.Value;
                SmsManager[0]["ExpireDate"] = txtbExipreDate.Text;
                SmsManager[0]["SMSDotoDate"] = txtbSMSDotoDate.Text;
                if (IsLanguageEnglish())
                {
                    SmsManager[0]["IsFarsi"] = 0;

                    int SMSBodyLenght = txtbSMSBody.Value.Length;
                    if (SMSBodyLenght > 0)
                    {
                        SmsManager[0]["SmsCount"] = Math.Ceiling((double)((double)(SMSBodyLenght) / 160));
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "در ارسال پیام کوتاه خطا ایجاد شد";
                        return;
                    }
                }
                else
                {
                    SmsManager[0]["IsFarsi"] = 1;
                    int SMSBodyLenght = txtbSMSBody.Value.Length;
                    if (SMSBodyLenght > 0)
                    {
                        SmsManager[0]["SmsCount"] = Math.Ceiling((double)((double)(SMSBodyLenght) / 70));
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "در ارسال پیام کوتاه خطا ایجاد شد";
                        return;
                    }
                }

                String CostError = "";
                double smsCost = CalculateCost(IsLanguageEnglish(), ref CostError);
                if (String.IsNullOrEmpty(CostError) == false)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = CostError;
                    return;
                }
                if (HiddenFieldSMSDetail["CostID"] != null)
                {
                    SmsManager[0]["CostId"] = int.Parse(Utility.DecryptQS(HiddenFieldSMSDetail["CostID"].ToString()));
                    SmsManager[0]["SmsCost"] = smsCost;
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "در ارسال پیام کوتاه خطا ایجاد شد";
                    return;
                }
                SmsManager[0]["SmsTypeId"] = int.Parse(cmbSMSType.SelectedItem.Value.ToString());
                SmsManager[0]["SenderId"] = Utility.GetCurrentUser_MeId();
                SmsManager[0]["SmsSubject"] = txtbSubject.Text;
                SmsManager[0]["SmsBody"] = txtbSMSBody.Value;
                SmsManager[0]["IsDelivered"] = 0;
                SmsManager[0]["InActive"] = 0;
                SmsManager[0]["ModifiedDate"] = DateTime.Now;
                SmsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                SmsManager[0].EndEdit();

                int cn = SmsManager.Save();
                SmsManager.DataTable.AcceptChanges();
                if (cn > 0)
                {
                    //string[] arr = txtRecievers.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    // String[] MembersNoNum = hiddenRecieverMembers["MembersNoNum"].ToString().Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    #region Reciever_Member

                    if (hiddenRecieverMembers.Contains("Members") && hiddenRecieverMembers["Members"] != null && String.IsNullOrEmpty(hiddenRecieverMembers["Members"].ToString().Trim()) == false)
                    {
                        SmsRecieverManager.UpdateSmsRecieverForMember(Convert.ToInt32(SmsManager[0]["SmsId"]), hiddenRecieverMembers["Members"].ToString().Trim(), (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member, Utility.GetCurrentUser_UserId());
                    }

                    #endregion

                    Boolean ReChange = false;
                    SmsRecieverManager.DataTable.Columns.Add("IsExist", typeof(bool));
                    SmsRecieverManager.FindRecieverBySMSId(SMSId);

                    #region Reciever_Employee
                    String[] arrEmployee = hiddenRecieverEmployee["Id"].ToString().Split(';');
                    for (int i = 0; i < arrEmployee.Length && String.IsNullOrEmpty(arrEmployee[i]) == false; i++)
                    {
                        SmsRecieverManager.CurrentFilter = "RecieverId=" + arrEmployee[i] + " and RecieverType=" + (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee + " and SmsId = " + SMSId;
                        if (SmsRecieverManager.Count == 0)
                        {
                            DataRow drSmsRe = SmsRecieverManager.NewRow();
                            drSmsRe["SmsId"] = SmsManager[0]["SmsId"];
                            drSmsRe["RecieverId"] = arrEmployee[i];
                            EmployeeManager.FindByCode(int.Parse(arrEmployee[i].ToString()));
                            if (EmployeeManager.Count == 1)
                            {
                                if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["MobileNo"]))
                                    drSmsRe["RecieverCellPhone"] = EmployeeManager[0]["MobileNo"].ToString();
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                                return;
                            }
                            drSmsRe["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Employee;
                            drSmsRe["InActive"] = 0;
                            drSmsRe["IsDelivered"] = 0;
                            drSmsRe["ModifiedDate"] = DateTime.Now;
                            drSmsRe["IsExist"] = true;
                            drSmsRe["UserId"] = Utility.GetCurrentUser_UserId();

                            SmsRecieverManager.AddRow(drSmsRe);
                            ReChange = true;
                        }
                        else
                        {
                            SmsRecieverManager[0]["IsExist"] = true;
                            SmsRecieverManager[0].AcceptChanges();

                        }
                    }
                    #endregion
                    #region Reciever_ManualNo
                    String[] arrManualNo = hiddenRecieverManualNo["MobileNo"].ToString().Split(';');
                    for (int i = 0; i < arrManualNo.Length && String.IsNullOrEmpty(arrManualNo[i]) == false; i++)
                    {
                        SmsRecieverManager.CurrentFilter = "RecieverId=-1 and RecieverCellPhone='" + arrManualNo[i] + "' and RecieverType=" + (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.ManualInsert + " and SmsId = " + SMSId;
                        if (SmsRecieverManager.Count == 0)
                        {
                            DataRow drSmsRe = SmsRecieverManager.NewRow();
                            drSmsRe["SmsId"] = SmsManager[0]["SmsId"];
                            drSmsRe["RecieverId"] = -1;
                            drSmsRe["RecieverType"] = (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.ManualInsert;
                            drSmsRe["RecieverCellPhone"] = arrManualNo[i];
                            drSmsRe["InActive"] = 0;
                            drSmsRe["IsDelivered"] = 0;
                            drSmsRe["ModifiedDate"] = DateTime.Now;
                            drSmsRe["IsExist"] = true;
                            drSmsRe["UserId"] = Utility.GetCurrentUser_UserId();

                            SmsRecieverManager.AddRow(drSmsRe);
                            ReChange = true;
                        }
                        else
                        {
                            SmsRecieverManager[0]["IsExist"] = true;
                            SmsRecieverManager[0].AcceptChanges();
                        }
                    }
                    #endregion
                    SmsRecieverManager.CurrentFilter = "IsExist is null  and RecieverType<>" + (int)TSP.DataManager.SmsRecieverManager.RecieverTypes.Member + " and SmsId = " + SMSId;
                    if (SmsRecieverManager.Count > 0)
                    {
                        int c = SmsRecieverManager.Count;
                        for (int i = 0; i < c; i++)
                        {
                            SmsRecieverManager[0].Delete();
                        }
                        ReChange = true;
                    }

                    if (SmsRecieverManager.Save() < 0 && ReChange == true)
                    {

                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    else
                    {
                        CostError = "";
                        smsCost = CalculateCost(IsLanguageEnglish(), ref CostError);
                        if (String.IsNullOrEmpty(CostError) == false)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = CostError;
                            return;
                        }

                        SmsManager[0].BeginEdit();
                        SmsManager[0]["SmsCost"] = (int.Parse(SmsManager[0]["SmsCount"].ToString())) * (smsCost);
                        SmsManager[0].EndEdit();
                        int cnSave = SmsManager.Save();
                        if (cnSave > 0)
                        {
                            TransactionManager.EndSave();
                            //SmsRecieverManager.FindByRecieverId(SMSId, -1);
                            //if (SmsRecieverManager.Count > 0)
                            //{
                            //    SmsRecieverManager.CurrentFilter = "RecieverCellPhone is null ";
                            //    for (int k = 0; k < SmsRecieverManager.Count; k++)
                            //    {
                            //        HasNoNumber += SmsRecieverManager[0]["RecieverId"].ToString() + ";";
                            //    }
                            //}

                            //if (HasNoNumber.Length > 0)
                            //    HasNoNumber = HasNoNumber.Remove(HasNoNumber.Length - 1);
                            //hiddenRecieverMembers["MembersNoNum"] = HasNoNumber;
                            //string[] MembersNoNum = hiddenRecieverMembers["MembersNoNum"].ToString().Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            //txtbRecieverCountWithoutTel.Text = MembersNoNum.Length.ToString();
                            //txtbRecieverWithoutTel.Text = HasNoNumber;

                            // HiddenFieldSMSDetail["SMSId"] = Utility.EncryptQS(GrManager[0]["GrId"].ToString());
                            HiddenFieldSMSDetail["PageMode"] = Utility.EncryptQS("Edit");
                            RoundPanelSMS.HeaderText = "ویرایش";
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد";
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                return;
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private string GenerateConfirmMessageBody(string SMSSubject, string SMSBody, string SMSCount, string RecieverCount, string SMSCost)
    {
        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        EmployeeManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (EmployeeManager.Count > 0)
        {
            string EmpName = EmployeeManager[0]["FirstName"] + " " + EmployeeManager[0]["LastName"];
            string Body = "<p style=\"margin: 0px; text-align: right\">با سلام </p><p style=\"margin: 0px; text-align: right\">";
            Body += " آقا/خانم:" + EmpName;
            Body += "</p><p style=\"margin: 0px; text-align: right\">متقاضی ارسال پیام کوتاه با مشخصات زیر را می باشد</p><p style=\"margin: 0px; text-align: right\"><br/></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">دلایل مطرح شده :</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">";
            Body += SMSSubject;
            Body += "</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\"></span>&nbsp;</p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">متن پیام:</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">";
            Body += SMSBody;
            Body += "</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\"></span>&nbsp;</p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">تعداد پیام کوتاه:</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">";
            Body += SMSCost;
            Body += "</span>&nbsp;</p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">تعداد گیرندگان پیام کوتاه:</span></p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">";
            Body += "</span>&nbsp;</p><p style=\"margin: 0px; text-align: right\"><span style=\"font-weight: bold\">هزینه ارسال پیام کوتاه:</span></p>";
            Body += SMSCost;
            return (Body);
        }
        return "";
    }

    private string[] SendMessage(string UserName, string PassWord, long Number, string[] MobilesNumbers, string Body, string sendType)
    {
        ir.afe.www.WebService WebService = new ir.afe.www.WebService();
        string[] SMSRerult = WebService.SendMessage(UserName, PassWord, Number, MobilesNumbers, Body, sendType);
        return SMSRerult;
    }

    private void SMSDeliveryReport(TSP.DataManager.SmsManager SmsManager, string ReturnedValue, int SMSId)
    {
        switch (ReturnedValue)
        {
            case "Send Successfully":
                SmsManager.FindByCode(SMSId);
                if (SmsManager.Count > 0)
                {
                    SmsManager[0].BeginEdit();
                    SmsManager[0]["IsDelivered"] = 1;
                    SmsManager[0].EndEdit();
                    int coun = SmsManager.Save();
                    if (coun > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = " پیام کوتاه با موفقیت ارسال شد";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "در ارسال پیام کوتاه خطا ایجاد شد";
                    }
                }
                break;

            case "Mobile Number is Empty":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پارامتر شماره موبایل خالی است و حاوی هیچ مقداری نمی باشد.";
                break;

            case "Virtual Number is Empty":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پارامتر شماره اختصاصی خالی است و حاوی هیچ مقداری نمی باشد.";
                break;

            case "Message Body is Invalid":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "متن پیام کوتاه معتبر نمی باشد.";
                break;

            case "Message Type is Invalid":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "مقدار پارامتر Message Typeمعتبر نمی باشد.";
                break;

            case "Message is UnKnow":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پیام معتبر نیست.";
                break;

            case "Mobile Array is Empty":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لیست شماره موبایل ها خالی است.";
                break;


            case "Message is too Long":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".متن پیام طولانی تر از حد مجاز است";
                break;
            case "User Not Enable":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نام کاربری غیر فعال است.";
                break;

            case "No Credit":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اعتبار شما به اتمام رسیده است.";
                break;

            case "Quota Full":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "محددیت مصرف روزانه شما به اتمام رسیده است.";
                break;
            case "Wrong Number":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شماره اختصاصی اشتباه است.";
                break;
            case "Username or Password Wrong":
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نام کاربری یا رمز عبور اشتباه است.";
                break;
        }


    }

    //private int FindNmcId()
    //{
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();

    //    int NmcId = -1;
    //    NezamChartManager.FindByEmpId(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
    //    if (NezamChartManager.Count > 0)
    //    {
    //        NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
    //    }
    //    else
    //    {
    //        DivReport.Visible = true;
    //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //    }
    //    return (NmcId);
    //}

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }
    void LoadCredit()
    {
        try
        {
            if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Magfa)
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

                    txtRemainingCredit.Text = MagfaRemainingCredit.ToString("N0") + " ریال";

                else
                    txtRemainingCredit.Text = "اتمام اعتبار";

            }
            else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat)
            {
                ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
                string[] SmsInfo = new string[2];
                SmsInfo = Utility.GetSMSWebServiceInformation();
                string UserName = SmsInfo[0];
                string Password = SmsInfo[1];
                lblCurrentWebService.Text = "عصر فرا ارتباط";
                string arc = BoxService.GetRemainingCredit(UserName, Password);
                double AFERemainingCredit = 0;
                Double.TryParse(arc,out AFERemainingCredit);
                if (AFERemainingCredit > 0)
                    txtRemainingCredit.Text = AFERemainingCredit.ToString("N0") + " ریال";
                else
                    txtRemainingCredit.Text = "اتمام اعتبار";

            }
            else if (Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.Prdco || Utility.GetCurrentSMSWebService() == (int)TSP.DataManager.SMSWebServiceType.PrdcoAsync)
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
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            txtRemainingCredit.Text = "خطا در ارتباط با وب سرویس";
        }
    }

    #region Check WF Permissions
    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSMSInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldSMSDetail["PageMode"].ToString());
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.SMS;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.SaveSMSInfo, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermission2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermission2.BtnSave;

        this.ViewState["BtnSend"] = btnSave.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {

        int TableType = (int)TSP.DataManager.TableCodes.SMS;
        int WFCode = (int)TSP.DataManager.WorkFlows.SMSConfirming;
        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.SaveSMSInfo, WFCode, _SMSId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermission2 = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.ITManagerConfirmingSMS, WFCode, _SMSId, Utility.GetCurrentUser_UserId(), PageMode);
        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermission2.BtnSave;
        btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit || WFPermission2.BtnEdit;

        this.ViewState["BtnSend"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }
    #endregion
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
