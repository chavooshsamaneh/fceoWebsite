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

public partial class Employee_MembersRegister_AddMemberCards : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        if ((string.IsNullOrEmpty(Request.QueryString["MeCrdId"])) || (string.IsNullOrEmpty(Request.QueryString["PgMd"])))
        {
            Response.Redirect("MemberCards.aspx");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            //Check UserPermission
            TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = per.CanEdit || per.CanNew;
            TSP.DataManager.Permission perAutoConfirm = TSP.DataManager.MemberCardsManager.GetUserPermissionForAutoConfirm(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSaveAndAutoConfirm.Visible = btnSaveAndAutoConfirm2.Visible = perAutoConfirm.CanView;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnAutoSaveVisible"] = btnSaveAndAutoConfirm.Visible;

        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSaveAndAutoConfirm2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (this.ViewState["BtnAutoSaveVisible"] != null)
            btnSaveAndAutoConfirm.Visible = btnSaveAndAutoConfirm2.Visible = (bool)this.ViewState["BtnAutoSaveVisible"];
        
    }

    #region BtnClick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldMemberCards["MeCrdId"] = "";
        RoundPanelMeCards.HeaderText = "جدید";
        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        EnableControls();
        TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMemberCards["PageMode"].ToString());
        string MeCrdId = Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString());

        if (string.IsNullOrEmpty(MeCrdId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(PageMode) && PageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                if (!CheckPermitionForEdit(int.Parse(MeCrdId)))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
                    return;
                }
                //Check UserPermission
                TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = per.CanEdit;


                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                EnableControls();

                TSP.DataManager.MemberCardsManager memberCardsManager = new TSP.DataManager.MemberCardsManager();
                memberCardsManager.FindByCode(Convert.ToInt32(MeCrdId));
                if (memberCardsManager.Count == 1)
                {
                    int MeId = Convert.ToInt32(memberCardsManager[0]["MeId"].ToString());
                    TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
                    memberManager.FindByCode(MeId);
                    if (memberManager.Count == 1)
                    {
                        int MrsId = Convert.ToInt32(memberManager[0]["MrsId"].ToString());
                        if (MrsId == (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
                            cmbCardType.Enabled = false;

                    }
                }

                HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("Edit");
                RoundPanelMeCards.HeaderText = "ویرایش";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save(false);
    }

    protected void btnSaveAndAutoConfirm_Click(object sender, EventArgs e)
    {
        Save(true);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("MemberCards.aspx?GrdFlt=" + Utility.EncryptQS(GrdFlt) + "&PostId=" + HiddenFieldMemberCards["MeCrdId"].ToString());
        }
        else
        {
            Response.Redirect("MemberCards.aspx");
        }
    }
    #endregion

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
     
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
        TSP.DataManager.ExpertFileManager ExpertFileManager = new TSP.DataManager.ExpertFileManager();
        lblErrorInMemberData.Visible = false;
        btnSave.Enabled = true;

        try
        {
            txtFamily.Text =
            txtFamilyNameEn.Text = 
            txtFirstNameEn.Text =
            txtFileNo.Text = 
            txtMeNo.Text = 
            txtName.Text = 
            txtSSN.Text = 
            txtMajor.Text =txtExertFilNo.Text= "";
            if (!string.IsNullOrEmpty(txtMeId.Text.Trim()))
            {
                int MeId = int.Parse(txtMeId.Text.Trim());
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count != 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد عضویت وارد شده معتبر نمی باشد.";
                    return;
                }

          
                    int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                    if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                        return;
                    }

                        if (MRsId == (int) TSP.DataManager.MembershipRegistrationStatus.Confirmed)
                    {
                        cmbCardType.ClientEnabled = true;
                        cmbCardType.SelectedItem = cmbCardType.Items.FindByValue("-1");
                    }
                    else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
                    {
                        cmbCardType.ClientEnabled = false;
                        cmbCardType.SelectedItem = cmbCardType.Items.FindByValue("6");
                        //cmbCardType.SelectedIndex = 5;
                    }


                        this.ViewState["BtnSave"] = btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = true;
                        MemberCardsManager.FindByMeId(MeId, 0);
                        if (MemberCardsManager.Count > 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست مجدد صدور کارت وجود ندارد.برای عضو انتخاب شده درخواست صدور کارت ، در جریان می باشد.";
                            this.ViewState["BtnSave"] = btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = false;

                        }

                        MemberCardsManager.FindPrintedMe(MeId, 0,1);
                        if (MemberCardsManager.Count > 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست مجدد صدور کارت وجود ندارد.برای عضو انتخاب شده درخواست کارت چاپ نشده وجود دارد.";
                            this.ViewState["BtnSave"] = btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = false;

                        }
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                            txtName.Text = MemberManager[0]["FirstName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                            txtFamily.Text = MemberManager[0]["LastName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstNameEn"]))
                            txtFirstNameEn.Text = MemberManager[0]["FirstNameEn"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastNameEn"]))
                            txtFamilyNameEn.Text = MemberManager[0]["LastNameEn"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
                            txtSSN.Text = MemberManager[0]["SSN"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastMjName"]))
                            txtMajor.Text = MemberManager[0]["LastMjName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
                            txtFileNo.Text = MemberManager[0]["FileNo"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeNo"]))
                            txtMeNo.Text = MemberManager[0]["MeNo"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                            ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
                        else
                            ImgMember.ImageUrl = "~/Images/Person.png";
                        ExpertFileManager.FindByMeId(MeId);
                        if (ExpertFileManager.Count == 1 && Convert.ToInt32( ExpertFileManager[0]["InActive"])==0)
                        {
                            txtExertFilNo.Text = ExpertFileManager[0]["FileNo"].ToString();
                        }
                        CheckMemberData(MemberManager);
            }
        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در جستجو انجام گرفته است.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    Boolean CheckMemberData(TSP.DataManager.MemberManager MemberManager)
    {
        String Error = "";
        Boolean Check = true;

        if (Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]) || String.IsNullOrWhiteSpace(MemberManager[0]["FirstName"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "نام ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]) || String.IsNullOrWhiteSpace(MemberManager[0]["LastName"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "نام خانوادگی ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["FirstNameEn"]) || String.IsNullOrWhiteSpace(MemberManager[0]["FirstNameEn"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "نام انگلیسی ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["LastNameEn"]) || String.IsNullOrWhiteSpace(MemberManager[0]["LastNameEn"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "نام خانوادگی انگلیسی ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]) || String.IsNullOrWhiteSpace(MemberManager[0]["SSN"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "کد ملی ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["LastMjName"]) || String.IsNullOrWhiteSpace(MemberManager[0]["LastMjName"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "رشته ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["MeNo"]) || String.IsNullOrWhiteSpace(MemberManager[0]["MeNo"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "شماره عضویت ثبت نشده است";
            Check = false;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]) || String.IsNullOrWhiteSpace(MemberManager[0]["ImageUrl"].ToString()))
        {
            Error += (String.IsNullOrWhiteSpace(Error)) ? "" : "<br>";
            Error += "&nbsp;&nbsp;*&nbsp;" + "تصویر ثبت نشده است";
            Check = false;
        }

        if (Check == false)
        {
            lblErrorInMemberData.Visible = true;
            lblErrorInMemberData.Text = "اطلاعات زیر برای این عضو ثبت نشده است :<br>";
            lblErrorInMemberData.Text += Error + "<br><br>";
            btnSave.Enabled = false;
        }

        return Check;
    }

    #endregion

    #region Methods

    private void Save(Boolean AutoConfirm)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMemberCards["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                Insert(AutoConfirm);
                break;
            case "Edit":
                int MeCrdId = -1;
                if (string.IsNullOrEmpty(HiddenFieldMemberCards["MeCrdId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    MeCrdId = int.Parse(Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString()));
                }
                Edit(MeCrdId, AutoConfirm);

                break;
        }
    }

    private void Insert(Boolean AutoConfirm)
    {
        if (IsPageRefresh)
        {
            return;
        }
        if(cmbCardType.SelectedItem.Value.ToString() == "-1")
        {
            SetMessage("علت درخواست را انتخاب نمایید");
            return;
        }
        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
        TSP.DataManager.MemberCardsManager MemberCardsManager1 = new TSP.DataManager.MemberCardsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();        
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(MemberCardsManager);
        TransactionManager.Add(WorkFlowStateManager);        

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        int TaskIdSaveInfo = -1;
        int CurrentNmcId = -2;
        try
        {
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                return;
            }

            TaskIdSaveInfo = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            MemberCardsManager1.FindByMeId(int.Parse(txtMeId.Text.Trim()), 1);
            MemberCardsManager1.CurrentFilter = "MeCrdType=" + "0";// +" and " + "IsConfirmed=" + "1";
            if (MemberCardsManager1.Count == 0 && cmbCardType.SelectedItem.Value.ToString() != "0" && cmbCardType.SelectedItem.Value.ToString() != "5")
            {
                SetMessage("برای عضو انتخاب شده تاکنون کارت عضویت صادر نشده است.برای عضو انتخاب شده تنها قادر به درخواست صدور کارت جدید می باشید.");
                return;
            }
            if (MemberCardsManager1.Count != 0 && cmbCardType.SelectedItem.Value.ToString() == "0")
            {
                SetMessage("علت درخواست انتخاب شده نا معتبر می باشد.عضو انتخاب شده دارای کارت عضویت می باشد.");
                return;
            }
            if (cmbCardType.SelectedItem.Value.ToString() == "6" && string.IsNullOrEmpty(txtFileNo.Text.Trim()))
            {
                SetMessage("عضو انتخاب شده دارای پروانه اشتغال نمی باشد.");
                return;
            }
            if (cmbCardType.SelectedItem.Value.ToString() == "7" && string.IsNullOrEmpty(txtExertFilNo.Text.Trim()))
            {
                SetMessage("عضو انتخاب شده کارشناس ماده 27 نمی باشد.");
                return;
            }
            CurrentNmcId = FindNmcId(TaskIdSaveInfo);

            TransactionManager.BeginSave();
            DataRow MeCardRow = MemberCardsManager.NewRow();
            MeCardRow["MeId"] = txtMeId.Text.Trim();
            MeCardRow["MeCrdType"] = cmbCardType.SelectedItem.Value.ToString();
            MeCardRow["CreateDate"] = Utility.GetDateOfToday();
            MeCardRow["MailNo"] = "";
            MeCardRow["MailDate"] = "";
            if (AutoConfirm)
            {
                MeCardRow["IsConfirmed"] = 1;
            }
            else
            {
                MeCardRow["IsConfirmed"] = 0;
            }
           
            MeCardRow["IsPrinted"] = 0;
            MeCardRow["UserId"] = Utility.GetCurrentUser_UserId();
            MeCardRow["ModifiedDate"] = DateTime.Now;
            MemberCardsManager.AddRow(MeCardRow);
            if (MemberCardsManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            int TableId = int.Parse(MemberCardsManager[0]["MeCrdId"].ToString());            

            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            if (AutoConfirm)
            {
                WorkFlowStateManager.DataTable.AcceptChanges();
                if (WorkFlowStateManager.StartWorkFlow(TableId, (int)TSP.DataManager.WorkFlowTask.ConfirmingMemberCardAndEndProccess, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0,"تایید اتوماتیک گردش کار درخواست چاپ کارت عضویت") <= 0)
                {
                    TransactionManager.CancelSave();
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }
            TransactionManager.EndSave();
            HiddenFieldMemberCards["MeCrdId"] = Utility.EncryptQS(MemberCardsManager[0]["MeCrdId"].ToString());
            HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("Edit");
            RoundPanelMeCards.HeaderText = "ویرایش";
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                }
            }
            if (AutoConfirm)
                SetViewModeKeys();
            SetMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int MeCrdId, Boolean AutoConfirm)
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
        TSP.DataManager.MemberCardsManager MemberCardsManager1 = new TSP.DataManager.MemberCardsManager();
        int CurrentNmcId = -2;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        try
        {
            MemberCardsManager1.FindByMeId(int.Parse(txtMeId.Text.Trim()), 1);
            MemberCardsManager1.CurrentFilter = "MeId=" + txtMeId.Text.Trim() + " and " + "MeCrdType=" + "0";
            if (MemberCardsManager1.Count == 0 && cmbCardType.SelectedItem.Value.ToString() != "0" && cmbCardType.SelectedItem.Value.ToString() != "5")
            {
                SetMessage("برای عضو انتخاب شده تاکنون کارت عضویت صادر نشده است.برای عضو انتخاب شده تنها قادر به درخواست صدور کارت جدید می باشید.");
                return;
            }

            if (MemberCardsManager1.Count != 0 && cmbCardType.SelectedItem.Value.ToString() == "0")
            {
                SetMessage("علت درخواست انتخاب شده نا معتبر می باشد.عضو انتخاب شده دارای کارت عضویت می باشد.");
                return;
            }
   

        if (AutoConfirm)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                return;
            }
            CurrentNmcId = FindNmcId(int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString()));
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.Add(MemberCardsManager);
            TransactionManager.BeginSave();
        }
      

            MemberCardsManager.FindByCode(MeCrdId);
            if (MemberCardsManager.Count != 1)
            {
                if (AutoConfirm)
                    TransactionManager.CancelSave();
                SetMessage("اطلاعات کارت انتخاب شده توسط کاربر دیگری تغییر یافته است.");
                return;
            }
            MemberCardsManager[0].BeginEdit();
            MemberCardsManager[0]["MeCrdType"] = cmbCardType.SelectedItem.Value.ToString();
            MemberCardsManager[0]["MailNo"] ="";
            MemberCardsManager[0]["MailDate"] ="";
            MemberCardsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            MemberCardsManager[0]["ModifiedDate"] = DateTime.Now;
            if (AutoConfirm)
            {
                MemberCardsManager[0]["IsConfirmed"] = 1;
            }
            else
            {
                MemberCardsManager[0]["IsConfirmed"] = 0;
            }
            MemberCardsManager[0].EndEdit();
            if (MemberCardsManager.Save() <= 0)
            {
                if (AutoConfirm)
                    TransactionManager.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            if (AutoConfirm)
            {
                if (WorkFlowStateManager.StartWorkFlow(int.Parse(MemberCardsManager[0]["MeCrdId"].ToString()), (int)TSP.DataManager.WorkFlowTask.ConfirmingMemberCardAndEndProccess, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0,"تایید اتوماتیک گردش کار درخواست چاپ کارت عضویت") <= 0)
                {
                    TransactionManager.CancelSave();
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
                TransactionManager.EndSave();
            }

            SetMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            if (AutoConfirm)
                TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void SetKeys()
    {
        HiddenFieldMemberCards["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
        HiddenFieldMemberCards["MeCrdId"] = Server.HtmlDecode(Request.QueryString["MeCrdId"]).ToString();
        string PageMode = Utility.DecryptQS(HiddenFieldMemberCards["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        int MeCrdId = int.Parse(Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString()));
        if (MeCrdId != -1)
        {
            TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();
            MemberCardsManager.FindByCode(MeCrdId);
            if (MeCrdId != -1 && MemberCardsManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + MemberCardsManager[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
            else
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";

            }
        }
        else
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";

        }
        SetMode(PageMode);
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
        HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("View");
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        DisableControls();
        int MeCrdId = int.Parse(Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString()));
        FillForm(MeCrdId);

        RoundPanelMeCards.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("New");
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        ClearForm();
        RoundPanelMeCards.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        HiddenFieldMemberCards["PageMode"] = Utility.EncryptQS("Edit");
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberCardsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave.Enabled = btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        EnableControls();
        txtMeId.Enabled = false;
        int MeCrdId = int.Parse(Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString()));
        FillForm(MeCrdId);

        RoundPanelMeCards.HeaderText = "ویرایش";
    }

    private void SetError(Exception err)
    {
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
    }

    private void ClearForm()
    {
        txtFamily.Text =
        txtFileNo.Text = 
        txtMajor.Text =
        txtMeId.Text = 
        txtMeNo.Text = 
        txtName.Text = 
        txtSSN.Text =
        ImgMember.ImageUrl = 
        txtFirstNameEn.Text =
        txtFamilyNameEn.Text =txtExertFilNo.Text= "";
        cmbCardType.SelectedItem = cmbCardType.Items.FindByValue("-1");
 
    }

    private void EnableControls()
    {        
        txtMeId.Enabled = true;
        cmbCardType.Enabled = true;
    }

    private void DisableControls()
    {        
        txtMeId.Enabled = false;
        cmbCardType.Enabled = false;
    }

    private void FillForm(int MeCrdId)
    {
        TSP.DataManager.MemberCardsManager MemberCardsManager = new TSP.DataManager.MemberCardsManager();        
        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.ExpertFileManager ExpertFileManager = new TSP.DataManager.ExpertFileManager();
        MemberCardsManager.FindByCode(MeCrdId);
        if (MemberCardsManager.Count == 1)
        {           
            int MeId = Convert.ToInt32(MemberCardsManager[0]["MeId"].ToString());
            memberManager.FindByCode(MeId);
            int MrsId = Convert.ToInt32(memberManager[0]["MrsId"].ToString());
            if (MrsId == 2)
                cmbCardType.Enabled = false;

            txtMeId.Text = MemberCardsManager[0]["MeId"].ToString();
            cmbCardType.SelectedItem = cmbCardType.Items.FindByValue(MemberCardsManager[0]["MeCrdType"].ToString());
            //cmbCardType.SelectedIndex = int.Parse(MemberCardsManager[0]["MeCrdType"].ToString());
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["FirstName"]))
                txtName.Text = MemberCardsManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["LastName"]))
                txtFamily.Text = MemberCardsManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["FirstNameEn"]))
                txtFirstNameEn.Text = MemberCardsManager[0]["FirstNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["LastNameEn"]))
                txtFamilyNameEn.Text = MemberCardsManager[0]["LastNameEn"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["SSN"]))
                txtSSN.Text = MemberCardsManager[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["LastMjName"]))
                txtMajor.Text = MemberCardsManager[0]["LastMjName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["FileNo"]))
                txtFileNo.Text = MemberCardsManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["MeNo"]))
                txtMeNo.Text = MemberCardsManager[0]["MeNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberCardsManager[0]["ImageUrl"]))
                ImgMember.ImageUrl = MemberCardsManager[0]["ImageUrl"].ToString();
            else
                ImgMember.ImageUrl = "~/Images/Person.png";
            ExpertFileManager.FindByMeId(MeId);
            if (ExpertFileManager.Count == 1 && Convert.ToInt32(ExpertFileManager[0]["InActive"]) == 0)
            {
                txtExertFilNo.Text = ExpertFileManager[0]["FileNo"].ToString();
            }
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    //private int FindNmcId()
    //{
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int NmcId = -1;

    //    NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
    //    if (NmcId > 0)
    //    {
    //        return NmcId;
    //    }
    //    else
    //    {
    //        DivReport.Visible = true;
    //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //        return (-1);
    //    }
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
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            int FirstUserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                if (FirstUserId == Utility.GetCurrentUser_UserId())
                                {
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                    if (Permission > 0)
                                        return true;
                                    else
                                        return false;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldMemberCards["PageMode"].ToString());
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
                    break;
                case "View":
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = false;
                    break;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = false;
      
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی گردش کار جهت ثبت کارت عضویت را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnEdit.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MeCrdId = Utility.DecryptQS(HiddenFieldMemberCards["MeCrdId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.MemberCards;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberCardInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(MeCrdId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = false;
                    break;

                case "Change":
                    btnSaveAndAutoConfirm2.Enabled = btnSaveAndAutoConfirm.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
     
                    break;
            }
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات انجام گرفته است";
            }
        }
        catch (Exception err)
        {
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
        }
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }  
    #endregion
}
