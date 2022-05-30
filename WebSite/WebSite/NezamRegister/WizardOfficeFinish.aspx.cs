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
using System.IO;
public partial class NezamRegister_WizardOfficeFinish : System.Web.UI.Page
{
    DataTable dtOffice = new DataTable();
    DataTable dtLetters = new DataTable();
    DataTable dtMembers = new DataTable();
    DataTable dtAgents = new DataTable();
    DataTable dtJob = new DataTable();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
            string MemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice).ToString();
            lblMemberShipCost.Text = "فیش بانکی مربوط به پرداخت ورودیه به مبلغ " + MemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی";

            string YearMemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice).ToString();
            lblYearMemberShipCost.Text = "فیش بانکی مربوط به پرداخت حق عضویت سالانه به مبلغ " + YearMemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی";
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Session["Office"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مدت زمان اعتبار صفحه به پایان رسیده است.مجدداً اقدام نمایید";
            return;
        }

        ASPxMenu1.Items.FindByName("End").Selected = true;

        if (Session["OfficeMembership"] != null && (Boolean)Session["OfficeMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Office"] != null && ((DataTable)Session["Office"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Office").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Office").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Office").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfAgent"] != null && ((DataTable)Session["TblOfAgent"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Agent").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Agent").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Agent").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfLetter"] != null && ((DataTable)Session["TblOfLetter"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Letter").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Letter").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Letter").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMember"] != null && ((DataTable)Session["TblOfMember"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfJob"] != null && ((DataTable)Session["TblOfJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["OfficeSummary"] != null && (Boolean)Session["OfficeSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }



        if (!IsPostBack)
        {
            ViewState["Login"] = -1;
            HiddenPrintDetial["PrintUserInfo"] = "";
            HiddenPrintDetial["PrintUserData"] = "";

            if (!Utility.IsDBNullOrNullValue(Session["OfIsPaid"]) && Convert.ToBoolean(Session["OfIsPaid"]) == true)
            {
                //btnFinish.Visible = false;

            }
            else
            {
                if (Session["OfIsPaid"] == null || Convert.ToBoolean(Session["OfIsPaid"]) == false)
                {
                    if (string.IsNullOrEmpty(this.Request["paymentId"]) && string.IsNullOrEmpty(this.Request["resultCode"]))//Next
                    {
                        Func_Payment();
                    }
                    else//Bank
                    {
                        if (this.Request["resultCode"] == TSP.Utility.OnlinePayment.PaymentSuccessCode.ToString() && !string.IsNullOrEmpty(this.Request["referenceId"]))
                        {
                            Insert();

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = TSP.Utility.OnlinePayment.PaymentResultCode(Convert.ToInt32(this.Request["resultCode"]));

                            Func_Payment();
                        }
                    }
                }
                //else
                //    btnFinish.PostBackUrl = "";
            }
        }

        //btnFinish.Visible = false;
    }  

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeSummary.aspx");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["Office"] = null;
        //Session["TblOfAgent"] = null;
        //Session["TblOfLetter"] = null;
        //Session["TblOfMember"] = null;
        //Session["FileOfSign"] = null;
        //Session["FileOfArm"] = null;
        //Session["TblOfJob"] = null;
        ClearSessions();

        Response.Redirect("~/Default.aspx");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ShowMessage(MeReqResult[1].ToString());
                return;
            }
            if (Session["OfficeMembership"] == null || (Boolean)Session["OfficeMembership"] == false)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - چهارچوب شئون حرفه ای مهندسی مورد موافقت قرار نگرفته است");
                return;
            }
            if (Session["Office"] == null || ((DataTable)Session["Office"]).Rows.Count == 0)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - مشخصات شرکت وارد نشده است");
                return;
            }

            if (Session["TblOfMember"] == null || ((DataTable)Session["TblOfMember"]).Rows.Count == 0)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - مشخصات اعضای شرکت وارد نشده است");
                return;
            }

            if (Session["TblOfLetter"] == null || ((DataTable)Session["TblOfLetter"]).Rows.Count == 0)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - آکهی رسمی برای شرکت وارد نشده است");
                return;
            }
            if (Session["OfficeSummary"] == null || (Boolean)Session["OfficeSummary"] == false)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - اطلاعات ثبت نام در مرحله خلاصه اطلاعات، تایید نشده است");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return;
        }
        Insert();
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (ViewState["Login"] != null)
        {
            ClearSessions();
            //Response.Redirect("~/Office/OfficeHome.aspx?MeId=" + HDOfficeId.Value);

            Session["LoginFromOtherPage"] = true;
            Response.Redirect("~/Login.aspx?LId=" + Utility.EncryptQS(ViewState["Login"].ToString()) + "&qto=" + Utility.EncryptQS(DateTime.Now.ToFileTime().ToString()) + "&tsp=" + Utility.EncryptQS("0"));
        }
        else
            Response.Redirect("~/Login.aspx");
    }
    #endregion

    #region Methods
    private decimal GetYearlyMembershipCost()
    {
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    protected long CheckPayment()
    {
        net.sabapardazesh.pg.verifyRequest verify = new net.sabapardazesh.pg.verifyRequest();
        net.sabapardazesh.pg.MerchantService merchant = new net.sabapardazesh.pg.MerchantService();

        verify.merchantId = TSP.Utility.OnlinePayment.GetNezamMerchantId();
        verify.referenceNumber = this.Request["referenceId"];
        long lresult = merchant.verify(verify);

        return lresult;

    }

    protected void Insert()
    {
        if (Session["Office"] != null)
        {
            dtOffice = (DataTable)Session["Office"];

            if (dtOffice.Rows.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است.مجدداً اقدام نمایید";
                return;
            }

            //if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["MFType"]))
            //{
            //    if (Convert.ToInt32(dtOffice.Rows[0]["MFType"]) != (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
            //    {
            //        dtMembers = (DataTable)Session["TblOfMember"];
            //        for (int i = 0; i < dtMembers.Rows.Count; i++)
            //        {
            //            if (Convert.ToInt32(dtMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.Manager ||
            //                Convert.ToInt32(dtMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
            //            {
            //                if (Convert.ToInt32(dtMembers.Rows[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Otherperson)
            //                {
            //                    this.DivReport.Visible = true;
            //                    this.LabelWarning.Text = "امکان ثبت " + dtMembers.Rows[i]["FullName"].ToString() + " به عنوان مدیرعامل شرکت وجود ندارد";
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است.مجدداً اقدام نمایید";
            return;
        }

        dtAgents = (DataTable)Session["TblOfAgent"];
        dtLetters = (DataTable)Session["TblOfLetter"];
        dtMembers = (DataTable)Session["TblOfMember"];
        dtJob = (DataTable)Session["TblOfJob"];

        #region Define Managers
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager logManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeMemberManager ofMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.ProjectJobHistoryManager JobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        //TSP.DataManager.MemberStatusChangeManager StatusManager = new TSP.DataManager.MemberStatusChangeManager();

        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
      

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(transact);
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        transact.Add(logManager);
        transact.Add(OfManager);
        transact.Add(ofMeManager);
        transact.Add(LetterManager);
        transact.Add(AgentManager);
        transact.Add(ReqManager);
        transact.Add(OthManager);
        //transact.Add(StatusManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(JobHistoryManager);
        transact.Add(TechnicianRequestManager);
        #endregion

        string PerDate = Utility.GetDateOfToday();
        Boolean SaveComplete = false;

        try
        {
            int AccId = -1, ParentAccId = -1, MembershipEarningsAccId = -1, MainBankAccId = -1;
            string Des1 = "", Des2 = "";
            decimal Amount = 0;
            String Password = Utility.GeneratePassword();

            #region User


            DataRow drLogin = logManager.NewRow();
            drLogin["UserName"] = dtOffice.Rows[0]["RegOfNo"].ToString().GetHashCode().ToString();
            drLogin["Password"] = Utility.EncryptPassword(Password);
            drLogin["UltId"] = 2;
            drLogin["Email"] = dtOffice.Rows[0]["Email"].ToString();
            drLogin["IsValid"] = 1;
            drLogin["ModifiedDate"] = DateTime.Now;
            drLogin["MeId"] = -1;
            logManager.AddRow(drLogin);
            transact.BeginSave();
            logManager.Save();

            logManager.DataTable.AcceptChanges();

            int UserId = int.Parse(logManager[0]["UserId"].ToString());
            #endregion
            #region Office
            DataRow drOffice = OfManager.NewRow();
            drOffice["OfId"] = 0;
            drOffice["OfName"] = dtOffice.Rows[0]["OfName"];
            drOffice["OfNameEn"] = dtOffice.Rows[0]["OfNameEn"];
            drOffice["PrefixCode"] = dtOffice.Rows[0]["OtId"];
            drOffice["OtId"] = dtOffice.Rows[0]["OtId"];
            drOffice["MembershipRequstType"] = dtOffice.Rows[0]["MembershipRequstType"];
            // bool chkOfType = false;
            //int OatId = 0;
            //for (int i = 0; i < drdOFAtType.Items.Count; i++)
            //{
            //    if (drdOFAtType.Items[i].Selected)
            //        OatId = OatId + int.Parse(drdOFAtType.Items[i].Value.ToString());

            //}
            //if (OatId > 0)
            //    drOffice["OatId"] = OatId;
            //else
            drOffice["OatId"] = dtOffice.Rows[0]["OatId"];
            drOffice["Tel1"] = dtOffice.Rows[0]["Tel1"];
            drOffice["Tel2"] = dtOffice.Rows[0]["Tel2"];
            drOffice["Fax"] = dtOffice.Rows[0]["Fax"];
            drOffice["MobileNo"] = dtOffice.Rows[0]["MobileNo"];
            drOffice["Email"] = dtOffice.Rows[0]["Email"];
            drOffice["Website"] = dtOffice.Rows[0]["Website"];
            drOffice["Address"] = dtOffice.Rows[0]["Address"];
            drOffice["Subject"] = dtOffice.Rows[0]["Subject"];
            drOffice["RegDate"] = dtOffice.Rows[0]["RegDate"];
            drOffice["RegOfNo"] = dtOffice.Rows[0]["RegOfNo"];
            drOffice["RegPlace"] = dtOffice.Rows[0]["RegPlace"];
            drOffice["Stock"] = dtOffice.Rows[0]["Stock"];
            //  drOffice["ActivityType"] = dtOffice.Rows[0]["ActivityType"];
            drOffice["VolumeInvest"] = dtOffice.Rows[0]["VolumeInvest"];
            drOffice["MeNo"] = "1";
            drOffice["FileNo"] = dtOffice.Rows[0]["FileNo"];
            if (Session["fileOfSign"] != null)
            {
                drOffice["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign"].ToString());
            }
            if (Session["fileOfArm"] != null)
            {
                drOffice["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm"].ToString());
            }
            drOffice["CreateDate"] = dtOffice.Rows[0]["CreateDate"];
            drOffice["Description"] = dtOffice.Rows[0]["Description"];
            drOffice["MrsId"] = 2;
            drOffice["UserId"] = UserId;
            drOffice["ModifiedDate"] = DateTime.Now;
            OfManager.AddRow(drOffice);
            OfManager.Save();

            int OfficeId = int.Parse(OfManager[0]["OfId"].ToString());
            HDOfficeId.Value = Utility.EncryptQS(OfficeId.ToString());

           
            OfManager.DataTable.AcceptChanges();
            if (Utility.CreateAccount())
                drOffice["AccId"] = AccId;
            else
                drOffice["AccId"] = DBNull.Value;
            OfManager.Save();

            logManager[0]["UserName"] = "com" + OfficeId.ToString();
            logManager[0]["MeId"] = OfficeId;
            logManager.Save();


            Session["LoginName"] = logManager[0]["UserName"].ToString();
            Session["LoginType"] = logManager[0]["UltId"].ToString();
            Session["MeId"] = OfficeId;

            #endregion
            #region OfficeRequest
            DataRow drReq = ReqManager.NewRow();
            drReq["OfId"] = OfficeId;
            drReq["MembershipRequstType"] = dtOffice.Rows[0]["MembershipRequstType"];
            drReq["OfName"] = dtOffice.Rows[0]["OfName"];
            drReq["OfNameEn"] = dtOffice.Rows[0]["OfNameEn"];
            drReq["Tel1"] = dtOffice.Rows[0]["Tel1"];
            drReq["Tel2"] = dtOffice.Rows[0]["Tel2"];
            drReq["Fax"] = dtOffice.Rows[0]["Fax"];
            drReq["MobileNo"] = dtOffice.Rows[0]["MobileNo"];
            drReq["Email"] = dtOffice.Rows[0]["Email"];
            drReq["Website"] = dtOffice.Rows[0]["Website"];
            drReq["Address"] = dtOffice.Rows[0]["Address"];
            drReq["OtId"] = dtOffice.Rows[0]["OtId"];
            drReq["Subject"] = dtOffice.Rows[0]["Subject"];
            drReq["RegOfDate"] = dtOffice.Rows[0]["RegDate"];
            drReq["RegOfNo"] = dtOffice.Rows[0]["RegOfNo"];
            drReq["RegOfPlace"] = dtOffice.Rows[0]["RegPlace"];
            drReq["Stock"] = dtOffice.Rows[0]["Stock"];
            // drReq["ActivityType"] = dtOffice.Rows[0]["ActivityType"];
            drReq["VolumeInvest"] = dtOffice.Rows[0]["VolumeInvest"];
            if (Session["fileOfSign"] != null)
            {
                drReq["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign"].ToString());
            }
            if (Session["fileOfArm"] != null)
            {
                drReq["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm"].ToString());
            }
            drReq["CreateDate"] = dtOffice.Rows[0]["CreateDate"];
            drReq["UserId"] = UserId;
            drReq["IsConfirm"] = 0;
            drReq["Type"] = (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo;
            drReq["Requester"] = 0;
            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

            drReq["ModifiedDate"] = DateTime.Now;

            ReqManager.AddRow(drReq);
            int rct = ReqManager.Save();
            if (rct <= 0)
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
                return;
            }
            int OfReId = int.Parse(ReqManager[0]["OfReId"].ToString());

            #endregion
            #region OfficeAgent
            if (dtAgents.Rows.Count > 0)
            {
                for (int i = 0; i < dtAgents.Rows.Count; i++)
                {
                    DataRow drAgent = AgentManager.NewRow();
                    drAgent["OfId"] = OfficeId;
                    drAgent["OfReId"] = OfReId;
                    drAgent["OagName"] = dtAgents.Rows[0]["OagName"];
                    drAgent["Tel"] = dtAgents.Rows[0]["Tel"];
                    drAgent["Fax"] = dtAgents.Rows[0]["Fax"];
                    drAgent["Email"] = dtAgents.Rows[0]["Email"];
                    drAgent["Website"] = dtAgents.Rows[0]["Website"];
                    drAgent["Address"] = dtAgents.Rows[0]["Address"];
                    drAgent["Responsible"] = dtAgents.Rows[0]["Responsible"];
                    drAgent["UserId"] = UserId;
                    drAgent["ModifiedDate"] = DateTime.Now;
                    AgentManager.AddRow(drAgent);


                }
            }
            #endregion
            #region OfficeMeMembers
            if (dtMembers.Rows.Count > 0)
            {
                for (int k = 0; k < dtMembers.Rows.Count; k++)
                {
                    switch (Convert.ToInt32(dtMembers.Rows[k]["OfmTypeId"]))
                    {
                        case (int)TSP.DataManager.OfficeMemberType.Member:
                            #region Member
                            DataRow drMembers = ofMeManager.NewRow();
                            //DataRow dr = dt.NewRow();
                            //dr = dt.Rows[k];
                            drMembers["OfId"] = OfficeId;
                            drMembers["OfReId"] = OfReId;
                            drMembers["MfId"] = dtMembers.Rows[k]["MfId"];
                            drMembers["PersonId"] = dtMembers.Rows[k]["MeId"];
                            drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
                            drMembers["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            drMembers["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            drMembers["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            drMembers["HasEfficientGrade"] = Convert.ToBoolean( dtMembers.Rows[k]["HasEfficientGradeCode"]);
                            
                            if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                                drMembers["IsFullTime"] = false;
                            else
                                drMembers["IsFullTime"] = true;
                            drMembers["Description"] = dtMembers.Rows[k]["Description"];


                            if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["SignUrl"])))
                            {
                                // drMembers["SignUrl"] = dtMembers.Rows[k]["SignUrl"];
                                drMembers["SignUrl"] = "~/Image/Office/Members/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());

                            }
                            if (!Utility.IsOfficeMemberConfirmRequestNeeded())
                            {
                                drMembers["IsConfirm"] = 1;
                                drMembers["ConfirmDate"] = Utility.GetDateOfToday();
                            }
                            drMembers["UserId"] = UserId;
                            drMembers["ModifiedDate"] = DateTime.Now;
                            ofMeManager.AddRow(drMembers);
                            #endregion
                            break;

                        case (int)TSP.DataManager.OfficeMemberType.Kardan:
                            #region Insert Kardan
                            DataRow drMeKardan = ofMeManager.NewRow();
                            drMeKardan["OfId"] = OfficeId;
                            drMeKardan["OfReId"] = OfReId;
                            drMeKardan["MfId"] = dtMembers.Rows[k]["MfId"];
                            drMeKardan["PersonId"] = dtMembers.Rows[k]["MeId"];
                            drMeKardan["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Kardan;
                            drMeKardan["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            drMeKardan["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            drMeKardan["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            drMeKardan["HasEfficientGrade"] = Convert.ToBoolean(dtMembers.Rows[k]["HasEfficientGradeCode"]);
                            if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                                drMeKardan["IsFullTime"] = false;
                            else
                                drMeKardan["IsFullTime"] = true;
                            drMeKardan["Description"] = dtMembers.Rows[k]["Description"];
                            if ((!string.IsNullOrEmpty(dtMembers.Rows[k]["SignUrl"].ToString())))
                            {
                                // drMeKardan["SignUrl"] = dtMembers.Rows[k]["SignUrl"];
                                drMeKardan["SignUrl"] = "~/Image/Office/Kardan/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());
                            }
                            drMeKardan["UserId"] = UserId;
                            drMeKardan["ModifiedDate"] = DateTime.Now;
                            ofMeManager.AddRow(drMeKardan);
                            #endregion
                            break;

                        case (int)TSP.DataManager.OfficeMemberType.Memar:
                            #region Memar
                            DataRow drMeMemar = ofMeManager.NewRow();
                            drMeMemar["OfId"] = OfficeId;
                            drMeMemar["OfReId"] = OfReId;
                            drMeMemar["MfId"] = dtMembers.Rows[k]["MfId"];
                            drMeMemar["PersonId"] = dtMembers.Rows[k]["MeId"];
                            drMeMemar["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Memar;
                            drMeMemar["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            drMeMemar["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            drMeMemar["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            drMeMemar["HasEfficientGrade"] = Convert.ToBoolean(dtMembers.Rows[k]["HasEfficientGradeCode"]);
                            if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                                drMeMemar["IsFullTime"] = false;
                            else
                                drMeMemar["IsFullTime"] = true;
                            drMeMemar["Description"] = dtMembers.Rows[k]["Description"];
                            if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["SignUrl"])))
                            {
                                //drMeMemar["SignUrl"] = dtMembers.Rows[k]["SignUrl"];
                                drMeMemar["SignUrl"] = "~/Image/Office/Memar/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());

                            }

                            drMeMemar["UserId"] = UserId;
                            drMeMemar["ModifiedDate"] = DateTime.Now;
                            ofMeManager.AddRow(drMeMemar);
                            #endregion
                            break;

                        case (int)TSP.DataManager.OfficeMemberType.Otherperson:
                            #region Otherperson
                            DataRow drOthers = OthManager.NewRow();
                            drOthers["FirstName"] = dtMembers.Rows[k]["FirstName"];
                            drOthers["LastName"] = dtMembers.Rows[k]["LastName"];
                            drOthers["FatherName"] = dtMembers.Rows[k]["FatherName"];
                            drOthers["IdNo"] = dtMembers.Rows[k]["IdNo"];
                            drOthers["SSN"] = dtMembers.Rows[k]["SSN"];
                            drOthers["BirthPlace"] = dtMembers.Rows[k]["BirthPlace"];
                            drOthers["BirthDate"] = dtMembers.Rows[k]["BirthDate"];
                            drOthers["OtpType"] = (int)TSP.DataManager.OtherPersonType.OtherPerson;
                    
                            if ((!string.IsNullOrEmpty(dtMembers.Rows[k]["ImageUrl"].ToString())))
                            {
                                //drOthers["ImageUrl"] = dtMembers.Rows[k]["ImageUrl"];
                                drOthers["ImageUrl"] = "~/Image/Office/Other/Ax/" + Path.GetFileName(dtMembers.Rows[k]["ImageUrl"].ToString());


                            }
                            drOthers["Description"] = dtMembers.Rows[k]["Description"];
                            drOthers["Tel"] = dtMembers.Rows[k]["Tel_pre"].ToString() + "-" + dtMembers.Rows[k]["Tel"].ToString();

                            drOthers["MobileNo"] = dtMembers.Rows[k]["MobileNo"];
                            drOthers["Address"] = dtMembers.Rows[k]["Address"];

                            drOthers["UserId"] = UserId;
                            drOthers["ModifiedDate"] = DateTime.Now;
                            OthManager.AddRow(drOthers);
                            OthManager.Save();
                            OthManager.DataTable.AcceptChanges();

                            int personId = int.Parse(OthManager[OthManager.Count - 1]["OtpId"].ToString()); //int.Parse(OthManager[0]["OtpId"].ToString());

                            DataRow drMeOther = ofMeManager.NewRow();

                            drMeOther["OfId"] = OfficeId;
                            drMeOther["OfReId"] = OfReId;
                            drMeOther["PersonId"] = personId;
                            drMeOther["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Otherperson;
                            drMeOther["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            drMeOther["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            drMeOther["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            drMeOther["HasEfficientGrade"] = Convert.ToBoolean(dtMembers.Rows[k]["HasEfficientGradeCode"]);
                            if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                                drMeOther["IsFullTime"] = false;
                            else
                                drMeOther["IsFullTime"] = true;
                            drMeOther["Description"] = dtMembers.Rows[k]["Description"];
                            if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["SignUrl"])))
                            {
                                //drMeOther["SignUrl"] = dtMembers.Rows[k]["SignUrl"];
                                drMeOther["SignUrl"] = "~/Image/Office/Other/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());

                            }

                            drMeOther["UserId"] = UserId;
                            drMeOther["ModifiedDate"] = DateTime.Now;
                            ofMeManager.AddRow(drMeOther);
                            #endregion
                            break;

                        case 5: //new kardan
                            //#region other person
                            //DataRow drKardan = OthManager.NewRow();
                            //drKardan["OtpCode"] = dtMembers.Rows[k]["OtpCode"];
                            //drKardan["FirstName"] = dtMembers.Rows[k]["FirstName"];
                            //drKardan["LastName"] = dtMembers.Rows[k]["LastName"];
                            //drKardan["FatherName"] = dtMembers.Rows[k]["FatherName"];
                            //drKardan["IdNo"] = dtMembers.Rows[k]["IdNo"];
                            //drKardan["SSN"] = dtMembers.Rows[k]["SSN"];
                            //drKardan["BirthPlace"] = dtMembers.Rows[k]["BirthPlace"];
                            //drKardan["BirthDate"] = dtMembers.Rows[k]["BirthDate"];
                            //drKardan["OtpType"] = (int)TSP.DataManager.OtherPersonType.Kardan;
                            //drKardan["MjId"] = dtMembers.Rows[k]["MjId"];
                            //drKardan["MjName"] = dtMembers.Rows[k]["MjName"];
                            //drKardan["AgentId"] = dtMembers.Rows[k]["AgentId"];
                            //drKardan["CitId"] = dtMembers.Rows[k]["CitId"];
                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["LicenceImgUrl"])))
                            //    drKardan["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(dtMembers.Rows[k]["LicenceImgUrl"].ToString());

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["ImageUrl"])))
                            //{
                            //    drKardan["ImageUrl"] = "~/Image/Office/Kardan/Ax/" + Path.GetFileName(dtMembers.Rows[k]["ImageUrl"].ToString());
                            //}

                            //drKardan["Description"] = dtMembers.Rows[k]["Description"];
                            //if (Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["Tel_pre"]))
                            //    drKardan["Tel"] = dtMembers.Rows[k]["Tel"].ToString();
                            //else
                            //    drKardan["Tel"] = dtMembers.Rows[k]["Tel_pre"].ToString() + "-" + dtMembers.Rows[k]["Tel"].ToString();
                            //drKardan["MobileNo"] = dtMembers.Rows[k]["MobileNo"];
                            //drKardan["Address"] = dtMembers.Rows[k]["Address"];
                            //drKardan["FileNo"] = dtMembers.Rows[k]["FileNo"];
                            //drKardan["FileNoDate"] = dtMembers.Rows[k]["FileDate"];
                            //drKardan["UserId"] = UserId;
                            //drKardan["ModifiedDate"] = DateTime.Now;
                            //OthManager.AddRow(drKardan);
                            //OthManager.Save();
                            //OthManager.DataTable.AcceptChanges();

                            //int OtpId = int.Parse(OthManager[OthManager.Count - 1]["OtpId"].ToString());
                            //#endregion

                            //#region TechnicianRequest
                            //DataRow TechReqRow = TechnicianRequestManager.NewRow();
                            //TechReqRow["OtpId"] = OtpId;
                            //TechReqRow["Status"] = 0;
                            //TechReqRow["IsConfirmed"] = 0;
                            //TechReqRow["InActive"] = 0;
                            //TechReqRow["OtpCode"] = dtMembers.Rows[k]["OtpCode"];
                            //TechReqRow["MjId"] = dtMembers.Rows[k]["MjId"];
                            //TechReqRow["MjName"] = dtMembers.Rows[k]["MjName"];
                            //TechReqRow["FileNo"] = dtMembers.Rows[k]["FileNo"];
                            //TechReqRow["FileDate"] = dtMembers.Rows[k]["FileDate"];
                            //TechReqRow["AgentId"] = dtMembers.Rows[k]["AgentId"];
                            //TechReqRow["CitId"] = dtMembers.Rows[k]["CitId"];
                            //TechReqRow["FirstName"] = dtMembers.Rows[k]["FirstName"];
                            //TechReqRow["FatherName"] = dtMembers.Rows[k]["FatherName"];
                            //TechReqRow["LastName"] = dtMembers.Rows[k]["LastName"];
                            //TechReqRow["IdNo"] = dtMembers.Rows[k]["IdNo"];
                            //TechReqRow["SSN"] = dtMembers.Rows[k]["SSN"];
                            //TechReqRow["BirthPlace"] = dtMembers.Rows[k]["BirthPlace"];
                            //TechReqRow["BirthDate"] = dtMembers.Rows[k]["BirthDate"];
                            //if (Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["Tel_pre"]))
                            //    TechReqRow["Tel"] = dtMembers.Rows[k]["Tel"].ToString();
                            //else
                            //    TechReqRow["Tel"] = dtMembers.Rows[k]["Tel_pre"].ToString() + "-" + dtMembers.Rows[k]["Tel"].ToString();
                            //TechReqRow["MobileNo"] = dtMembers.Rows[k]["MobileNo"];
                            //TechReqRow["Address"] = dtMembers.Rows[k]["Address"];

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["LicenceImgUrl"])))
                            //    TechReqRow["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(dtMembers.Rows[k]["LicenceImgUrl"].ToString());

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["ImageUrl"])))
                            //    TechReqRow["ImageUrl"] = "~/Image/Office/Kardan/Ax/" + Path.GetFileName(dtMembers.Rows[k]["ImageUrl"].ToString());

                            //TechReqRow["UserId"] = UserId;
                            //TechReqRow["ModifiedDate"] = DateTime.Now;
                            //TechnicianRequestManager.AddRow(TechReqRow);
                            //TechnicianRequestManager.Save();
                            //TechnicianRequestManager.DataTable.AcceptChanges();

                            //int TableId = int.Parse(TechnicianRequestManager[TechnicianRequestManager.Count - 1]["TnReId"].ToString());
                            //int TaskCodeTech = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
                            //WorkFlowStateManager.StartWorkFlow(TableId, TaskCodeTech, OfficeId, UserId, 2);
                            //WorkFlowStateManager.DataTable.AcceptChanges();
                            //#endregion

                            //#region Insert Kardan
                            //DataRow drOfMeKardan = ofMeManager.NewRow();
                            //drOfMeKardan["OfId"] = OfficeId;
                            //drOfMeKardan["OfReId"] = OfReId;
                            //drOfMeKardan["PersonId"] = OtpId;
                            //drOfMeKardan["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Kardan;
                            //drOfMeKardan["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            //drOfMeKardan["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            //drOfMeKardan["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            //drOfMeKardan["HasEfficientGrade"] = dtMembers.Rows[k]["HasEfficientGradeCode"];
                            //if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                            //    drOfMeKardan["IsFullTime"] = false;
                            //else
                            //    drOfMeKardan["IsFullTime"] = true;
                            //drOfMeKardan["Description"] = dtMembers.Rows[k]["Description"];
                            //if ((!string.IsNullOrEmpty(dtMembers.Rows[k]["SignUrl"].ToString())))
                            //{
                            //    // drMeKardan["SignUrl"] = dtMembers.Rows[k]["SignUrl"];
                            //    drOfMeKardan["SignUrl"] = "~/Image/Office/Kardan/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());
                            //}
                            //drOfMeKardan["UserId"] = UserId;
                            //drOfMeKardan["ModifiedDate"] = DateTime.Now;
                            //ofMeManager.AddRow(drOfMeKardan);
                            //#endregion
                            break;

                        case 6: //new memar
                            //#region other person
                            //DataRow drMemar = OthManager.NewRow();
                            //drMemar["OtpCode"] = dtMembers.Rows[k]["OtpCode"];
                            //drMemar["FirstName"] = dtMembers.Rows[k]["FirstName"];
                            //drMemar["LastName"] = dtMembers.Rows[k]["LastName"];
                            //drMemar["FatherName"] = dtMembers.Rows[k]["FatherName"];
                            //drMemar["IdNo"] = dtMembers.Rows[k]["IdNo"];
                            //drMemar["SSN"] = dtMembers.Rows[k]["SSN"];
                            //drMemar["BirthPlace"] = dtMembers.Rows[k]["BirthPlace"];
                            //drMemar["BirthDate"] = dtMembers.Rows[k]["BirthDate"];
                            //drMemar["OtpType"] = (int)TSP.DataManager.OtherPersonType.Memar;
                            //drMemar["MjId"] = dtMembers.Rows[k]["MjId"];
                            //drMemar["MjName"] = dtMembers.Rows[k]["MjName"];
                            //drMemar["AgentId"] = dtMembers.Rows[k]["AgentId"];
                            //drMemar["CitId"] = dtMembers.Rows[k]["CitId"];
                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["LicenceImgUrl"])))
                            //    drMemar["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(dtMembers.Rows[k]["LicenceImgUrl"].ToString());

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["ImageUrl"])))
                            //{
                            //    drMemar["ImageUrl"] = "~/Image/Office/Memar/Ax/" + Path.GetFileName(dtMembers.Rows[k]["ImageUrl"].ToString());
                            //}
                            //drMemar["Description"] = dtMembers.Rows[k]["Description"];
                            //if (Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["Tel_pre"]))
                            //    drMemar["Tel"] = dtMembers.Rows[k]["Tel"].ToString();
                            //else
                            //    drMemar["Tel"] = dtMembers.Rows[k]["Tel_pre"].ToString() + "-" + dtMembers.Rows[k]["Tel"].ToString();
                            //drMemar["MobileNo"] = dtMembers.Rows[k]["MobileNo"];
                            //drMemar["Address"] = dtMembers.Rows[k]["Address"];
                            //drMemar["FileNo"] = dtMembers.Rows[k]["FileNo"];
                            //drMemar["FileNoDate"] = dtMembers.Rows[k]["FileDate"];
                            //drMemar["UserId"] = UserId;
                            //drMemar["ModifiedDate"] = DateTime.Now;
                            //OthManager.AddRow(drMemar);
                            //OthManager.Save();
                            //OthManager.DataTable.AcceptChanges();

                            //int OtpIdMemar = int.Parse(OthManager[OthManager.Count - 1]["OtpId"].ToString());
                            //#endregion

                            //#region TechnicianRequest
                            //DataRow TechReqRowMemar = TechnicianRequestManager.NewRow();
                            //TechReqRowMemar["OtpId"] = OtpIdMemar;
                            //TechReqRowMemar["Status"] = 0;
                            //TechReqRowMemar["IsConfirmed"] = 0;
                            //TechReqRowMemar["InActive"] = 0;
                            //TechReqRowMemar["OtpCode"] = dtMembers.Rows[k]["OtpCode"];
                            //TechReqRowMemar["MjId"] = dtMembers.Rows[k]["MjId"];
                            //TechReqRowMemar["MjName"] = dtMembers.Rows[k]["MjName"];
                            //TechReqRowMemar["FileNo"] = dtMembers.Rows[k]["FileNo"];
                            //TechReqRowMemar["FileDate"] = dtMembers.Rows[k]["FileDate"];
                            //TechReqRowMemar["AgentId"] = dtMembers.Rows[k]["AgentId"];
                            //TechReqRowMemar["CitId"] = dtMembers.Rows[k]["CitId"];
                            //TechReqRowMemar["FirstName"] = dtMembers.Rows[k]["FirstName"];
                            //TechReqRowMemar["FatherName"] = dtMembers.Rows[k]["FatherName"];
                            //TechReqRowMemar["LastName"] = dtMembers.Rows[k]["LastName"];
                            //TechReqRowMemar["IdNo"] = dtMembers.Rows[k]["IdNo"];
                            //TechReqRowMemar["SSN"] = dtMembers.Rows[k]["SSN"];
                            //TechReqRowMemar["BirthPlace"] = dtMembers.Rows[k]["BirthPlace"];
                            //TechReqRowMemar["BirthDate"] = dtMembers.Rows[k]["BirthDate"];
                            //if (Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["Tel_pre"]))
                            //    TechReqRowMemar["Tel"] = dtMembers.Rows[k]["Tel"].ToString();
                            //else
                            //    TechReqRowMemar["Tel"] = dtMembers.Rows[k]["Tel_pre"].ToString() + "-" + dtMembers.Rows[k]["Tel"].ToString();
                            //TechReqRowMemar["MobileNo"] = dtMembers.Rows[k]["MobileNo"];
                            //TechReqRowMemar["Address"] = dtMembers.Rows[k]["Address"];

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["LicenceImgUrl"])))
                            //    TechReqRowMemar["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(dtMembers.Rows[k]["LicenceImgUrl"].ToString());

                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["ImageUrl"])))
                            //    TechReqRowMemar["ImageUrl"] = "~/Image/Office/Memar/Ax/" + Path.GetFileName(dtMembers.Rows[k]["ImageUrl"].ToString());

                            //TechReqRowMemar["UserId"] = UserId;
                            //TechReqRowMemar["ModifiedDate"] = DateTime.Now;
                            //TechnicianRequestManager.AddRow(TechReqRowMemar);
                            //TechnicianRequestManager.Save();
                            //TechnicianRequestManager.DataTable.AcceptChanges();

                            //int TableIdMemar = int.Parse(TechnicianRequestManager[TechnicianRequestManager.Count - 1]["TnReId"].ToString());
                            //int TaskCodeTech1 = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
                            //WorkFlowStateManager.StartWorkFlow(TableIdMemar, TaskCodeTech1, OfficeId, UserId, 2);
                            //WorkFlowStateManager.DataTable.AcceptChanges();
                            //#endregion

                            //#region Memar
                            //DataRow drOfMeMemar = ofMeManager.NewRow();
                            //drOfMeMemar["OfId"] = OfficeId;
                            //drOfMeMemar["OfReId"] = OfReId;
                            //drOfMeMemar["PersonId"] = OtpIdMemar;
                            //drOfMeMemar["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Memar;
                            //drOfMeMemar["OfpId"] = dtMembers.Rows[k]["OfpId"];
                            //drOfMeMemar["StartDate"] = dtMembers.Rows[k]["StartDate"];
                            //drOfMeMemar["HasSignRight"] = dtMembers.Rows[k]["HasSignRight"];
                            //drOfMeMemar["HasEfficientGrade"] = dtMembers.Rows[k]["HasEfficientGradeCode"];
                            //if (dtMembers.Rows[k]["IsFullTime"].ToString() == "0")
                            //    drOfMeMemar["IsFullTime"] = false;
                            //else
                            //    drOfMeMemar["IsFullTime"] = true;
                            //drOfMeMemar["Description"] = dtMembers.Rows[k]["Description"];
                            //if ((!Utility.IsDBNullOrNullValue(dtMembers.Rows[k]["SignUrl"])))
                            //{
                            //    drOfMeMemar["SignUrl"] = "~/Image/Office/Memar/Emza/" + Path.GetFileName(dtMembers.Rows[k]["SignUrl"].ToString());
                            //}

                            //drOfMeMemar["UserId"] = UserId;
                            //drOfMeMemar["ModifiedDate"] = DateTime.Now;
                            //ofMeManager.AddRow(drOfMeMemar);
                           // #endregion
                            break;
                    }
                }
                // OthManager.Save();
            }
            #endregion
            #region OfficeLetters
            if (dtLetters.Rows.Count > 0)
            {

                for (int z = 0; z < dtLetters.Rows.Count; z++)
                {
                    DataRow drLetters = LetterManager.NewRow();

                    drLetters["OfId"] = OfficeId;
                    drLetters["OfReId"] = OfReId;
                    drLetters["LetterNo"] = dtLetters.Rows[0]["LeNo"];
                    drLetters["PageNo"] = short.Parse(dtLetters.Rows[0]["LePageNo"].ToString());
                    drLetters["Date"] = dtLetters.Rows[0]["LeDate"];
                    drLetters["Description"] = dtLetters.Rows[0]["LeDesc"];
                    drLetters["UserId"] = UserId;
                    drLetters["ModifiedDate"] = DateTime.Now;
                    LetterManager.AddRow(drLetters);

                }
            }
            #endregion
            #region OfficeJob

            if (dtJob.Rows.Count > 0)
            {
                for (int i = 0; i < dtJob.Rows.Count; i++)
                {
                    DataRow drJob = JobHistoryManager.NewRow();
                    drJob.BeginEdit();

                    drJob["MeId"] = OfficeId;
                    drJob["RoeId"] = 1;//ثبت عضویت
                    drJob["PrTypeId"] = dtJob.Rows[i]["PrTypeId"];
                    drJob["SazeTypeId"] = dtJob.Rows[i]["SazeTypeId"];
                    drJob["ProjectName"] = dtJob.Rows[i]["ProjectName"].ToString();
                    drJob["Employer"] = dtJob.Rows[i]["Employer"].ToString();
                    drJob["CitName"] = dtJob.Rows[i]["CitName"].ToString();
                    drJob["CounId"] = dtJob.Rows[i]["CounId"];
                    drJob["PJPId"] = dtJob.Rows[i]["PJPId"].ToString();
                    drJob["StartOriginalDate"] = dtJob.Rows[i]["StartOriginalDate"].ToString();
                    drJob["StartCorporateDate"] = dtJob.Rows[i]["StartCorporateDate"].ToString();
                    drJob["StatusOfStartDate"] = dtJob.Rows[i]["StatusOfStartDate"].ToString();
                    drJob["EndCorporateDate"] = dtJob.Rows[i]["EndCorporateDate"].ToString();
                    drJob["StatusOfEndDate"] = dtJob.Rows[i]["StatusOfEndDate"].ToString();
                    drJob["ProjectVolume"] = dtJob.Rows[i]["ProjectVolume"].ToString();
                    if (!string.IsNullOrEmpty(dtJob.Rows[i]["Area"].ToString()))
                        drJob["Area"] = dtJob.Rows[i]["Area"];
                    else
                        drJob["Area"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(dtJob.Rows[i]["Floors"].ToString()))
                        drJob["Floors"] = dtJob.Rows[i]["Floors"];
                    else
                        drJob["Floors"] = DBNull.Value;
                    drJob["CorTypeId"] = dtJob.Rows[i]["CorTypeId"];
                    drJob["ConfirmedByNezam"] = 0;
                    drJob["Description"] = dtJob.Rows[i]["Description"].ToString();
                    drJob["UserId"] = UserId;
                    drJob["ModifiedDate"] = DateTime.Now;
                    //drJob["MReId"] = MReId;
                    //drJob["TableId"] = MemberId;

                    drJob["TableId"] = OfReId;
                    drJob["TableType"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    drJob["Type"] = 1;
                    drJob["CreateDate"] = Utility.GetDateOfToday();

                    drJob.EndEdit();
                    JobHistoryManager.AddRow(drJob);

                }
            }
            #endregion
            #region StatusCOMMENT
            //DataRow drSt = StatusManager.NewRow();
            //drSt["MeId"] = OfficeId;
            //drSt["MsId"] = 2;//در جریان
            //drSt["Date"] = PerDate;
            //drSt["Type"] = 1;//office
            //drSt["UserId"] = UserId;
            //drSt["ModifiedDate"] = DateTime.Now;
            //StatusManager.AddRow(drSt);
            #endregion

            AgentManager.Save();
            ofMeManager.Save();
            LetterManager.Save();
            JobHistoryManager.Save();

            //StatusManager.Save();

            #region WorkFlow
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;//DocumentOfOfficeConfirmingSaveInfo;
            int StartWF = WorkFlowStateManager.StartWorkFlow(OfReId, TaskCode, OfficeId, UserId, 1);
            if (StartWF > 0)
            {
                WorkFlowStateManager.DataTable.AcceptChanges();
                int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                int WorkflowCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;//.OfficeConfirming;
                int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;//DocumentOfOfficeConfirmingSaveInfo;
                int NextStepTaskId = -1;

                DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, SaveInfoTaskCode, WorkflowCode);
                if (dtNextTopTask.Rows.Count > 0)
                {
                    int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
                    WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
                    if (WorkFlowTaskManager.Count == 1)
                    {
                        NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    //DataTable dtSendBack = (DataTable)Session["SendBackDataTable_SMS"];
                    //cmbSendBackTask.DataSource = dtSendBack;
                    //cmbSendBackTask.ValueField = "TaskId";
                    //cmbSendBackTask.TextField = "TaskName";
                    //cmbSendBackTask.DataBind();

                    //int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
                    //if (SelectedTaskId == NextStepTaskId)
                    //{
                    //  TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
                    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

                    //   TransactionManager.Add(WorkFlowStateManager);

                    int NmcId = OfficeId;
                    int NmcIdType = 2;
                    if (NmcId > 0)
                    {
                        //   TransactionManager.BeginSave();
                        string Url = "<a href='../Employee/OfficeRegister/OfficeRegister1.aspx?OfId=" + Utility.EncryptQS(OfficeId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
                        string MsgContent = "";
                        int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, OfReId, NextStepTaskId, "شروع جریان کار عضویت توسط عضو", NmcId, NmcIdType, UserId, MsgContent, Url);
                        switch (SendDoc)
                        {
                            case -6:
                                transact.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان ارسال پرونده پروانه به مرحله جاری وجود ندارد.";
                                break;
                            case -4:
                                transact.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                break;
                            case -5:
                                transact.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                                break;
                            case -8:
                                transact.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                                break;
                            default:


                                    transact.EndSave();
                                    TSP.DataManager.OfficeManager.UpdateMeNo(OfficeId);
                                    ViewState["Login"] = UserId;
                                    Session["OfIsPaid"] = true;
                                    SaveComplete = true;


                                    tblUser.Visible = true;
                                    ASUserName.Text = "com" + OfficeId.ToString();
                                    ASEmailUser.Text = dtOffice.Rows[0]["Email"].ToString();
                                    ASPassword.Text = Password;
                                    lblFollowCode.Text = ReqManager[0]["FollowCode"].ToString();

                                    //btnFinish.Visible = false;
                                    btnPre.Visible = false;
                                    btnCancel.Visible = false;
                                    btnContinue.Visible = true;
                                    btnPrint.Visible = true;
                                    btnPrintUserInfo.Visible = true;
                                    HiddenPrintDetial["PrintUserInfo"] = "../ReportForms/UserInfoReport.aspx?UId=" + Utility.EncryptQS(logManager[0]["UserId"].ToString()) + "&P=" + Utility.EncryptQS(Password) + "&C=" + Utility.EncryptQS(ReqManager[0]["FollowCode"].ToString());
                                    HiddenPrintDetial["PrintUserData"] = "WizardOfficePrint.aspx?UId=" + Utility.EncryptQS(logManager[0]["UserId"].ToString()) + "&P=" + Utility.EncryptQS(Password) + "&C=" + Utility.EncryptQS(ReqManager[0]["FollowCode"].ToString());
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "پیش ثبت نام با موفقیت انجام شد.";                                
                               

                                break;
                        }
                    }
                    else
                    {
                        transact.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
                    }


                }
                else
                {
                    transact.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "مرحله بعد جریان کار نا مشخص است.";
                }
            }
            #endregion


        }
        catch (Exception err)
        {

            transact.CancelSave();
            Func_Payment();

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
            Utility.SaveWebsiteError(err);
        }

        #region MoveImage
        try
        {
            string SignSource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign"].ToString());
            string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign"].ToString());
            System.IO.File.Copy(SignSource, SignTarget, true);
            //System.IO.File.Delete(SignSource);
        }
        catch (Exception)
        {
        }
        try
        {
            string ArmSource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm"].ToString());
            string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm"].ToString());
            System.IO.File.Copy(ArmSource, ArmTarget, true);
            //System.IO.File.Delete(ArmSource);


        }
        catch (Exception)
        {
        }
        for (int q = 0; q < dtMembers.Rows.Count; q++)
        {
            try
            {
                if (Convert.ToInt32(dtMembers.Rows[q]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Member)//Member
                {

                    string EmSource = dtMembers.Rows[q]["SignUrl"].ToString();
                    //string EmSource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(((Image)GvMembers.Rows[q].Cells[6].Controls[1]).ImageUrl.ToString());
                    string EmTarget = Server.MapPath("~/Image/Office/Members/Emza/") + Path.GetFileName(dtMembers.Rows[q]["SignUrl"].ToString());
                    System.IO.File.Copy(Server.MapPath(EmSource), EmTarget, true);
                    //System.IO.File.Delete(EmSource);

                }
                if (Convert.ToInt32(dtMembers.Rows[q]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Kardan)//Kardan
                {
                    string EmSource = dtMembers.Rows[q]["SignUrl"].ToString();
                    string EmTarget = Server.MapPath("~/Image/Office/Kardan/Emza/") + Path.GetFileName(dtMembers.Rows[q]["SignUrl"].ToString());
                    System.IO.File.Copy(Server.MapPath(EmSource), EmTarget, true);


                    //string EmSource2 = dtMembers.Rows[q]["ImageUrl"].ToString();
                    //string EmTarget2 = Server.MapPath("~/Image/Office/Kardan/Ax/") + Path.GetFileName(dtMembers.Rows[q]["ImageUrl"].ToString());
                    //System.IO.File.Copy(Server.MapPath(EmSource2), EmTarget2, true);
                }
                if (Convert.ToInt32(dtMembers.Rows[q]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Otherperson)//Other
                {
                    string EmSource = dtMembers.Rows[q]["SignUrl"].ToString();
                    string EmTarget = Server.MapPath("~/Image/Office/Other/Emza/") + Path.GetFileName(dtMembers.Rows[q]["SignUrl"].ToString());
                    System.IO.File.Copy(Server.MapPath(EmSource), EmTarget, true);


                    string EmSource2 = dtMembers.Rows[q]["ImageUrl"].ToString();
                    string EmTarget2 = Server.MapPath("~/Image/Office/Other/Ax/") + Path.GetFileName(dtMembers.Rows[q]["ImageUrl"].ToString());
                    System.IO.File.Copy(Server.MapPath(EmSource2), EmTarget2, true);
                }
                if (Convert.ToInt32(dtMembers.Rows[q]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Memar)//Memar
                {
                    string EmSource = dtMembers.Rows[q]["SignUrl"].ToString();
                    string EmTarget = Server.MapPath("~/Image/Office/Memar/Emza/") + Path.GetFileName(dtMembers.Rows[q]["SignUrl"].ToString());
                    System.IO.File.Copy(Server.MapPath(EmSource), EmTarget, true);


                    //string EmSource2 = dtMembers.Rows[q]["ImageUrl"].ToString();
                    //string EmTarget2 = Server.MapPath("~/Image/Office/Memar/Ax/") + Path.GetFileName(dtMembers.Rows[q]["ImageUrl"].ToString());
                    //System.IO.File.Copy(Server.MapPath(EmSource2), EmTarget2, true);
                }
            }
            catch (Exception)
            {

            }
        }


        #endregion

        if (SaveComplete)
        {
            ClearSessions();
            btnSave.Visible = false;
        }
    }

    void ClearSessions()
    {
        Session["Office"] = null;
        Session["TblOfAgent"] = null;
        Session["TblOfLetter"] = null;
        Session["TblOfMember"] = null;
        Session["FileOfSign"] = null;
        Session["FileOfArm"] = null;
        Session["TblOfJob"] = null;
    }

    #region Accounting
    protected void Func_Payment()
    {
        //merchantId.Visible = true;
        //paymentId.Visible = true;
        //amount.Visible = true;
        //revertURL.Visible = true;
        //customerId.Visible = true;

        //btnFinish.PostBackUrl = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
        //merchantId.Value = TSP.Utility.OnlinePayment.GetNezamMerchantId();
        //revertURL.Value = this.Request.Url.AbsoluteUri;
        Utility.KeyGenerator grkey = new Utility.KeyGenerator();
        //paymentId.Value = grkey.Generate(12, Utility.KeyGenerator.CharacterTypes.LettersAndNumbers);
        Session["OfPaymentId"] = grkey.Generate(12, Utility.KeyGenerator.CharacterTypes.Numbers);
        //amount.Value = GetYearlyMembershipCost().ToString();
        Session["OfPrice"] = Convert.ToInt64(GetYearlyMembershipCost());
    }

    /*********************************************************************************************************************************************************************/
    private int GetParentAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembersCurrentAccountOffice.ToString(), 1, "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetAccCode()
    {
        string AccCode = Utility.DecryptQS(HDOfficeId.Value);
        while (AccCode.Length < TSP.DataManager.AccountingAccountManager.TafziliLength)
            AccCode = "0" + AccCode;
        return AccCode;
    }

    private string GetAccName(DataRow Member)
    {
        string Name = Member["LastName"].ToString() + " " + Member["FirstName"].ToString();
        return Name;
    }

    private int GetMembershipEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembershipEarnings.ToString(), 1, "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), 1, "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString(), 1);
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetDes1(DataRow Of, decimal Amount)
    {
        string Des = "جهت حق عضویت جدید شرکت " + " " + Of["OfName"].ToString() + " " + "به مبلغ" + " " + Amount.ToString("#,#") + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    private string GetDes2(DataRow Of)
    {
        Utility.Date Date = new Utility.Date();
        string Des = "واریز حق عضویت جدید شرکت " + Of["OfName"].ToString() + " " + "جهت سال" + " " + Date.Year.ToString();
        return Des;
    }
    /*********************************************************************************************************************************************************************/
    #endregion

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeFinish).ToString());
    }
    #endregion
}
