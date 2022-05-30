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

public partial class Employee_TechnicalServices_Project_OwnerInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    string PageMode;
    string ProjectId;
    string OwnerId;
    string PrjReId;

    #region Events
    #region btnClick

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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

        if (!IsPostBack)
        {

            Session["SendBackDataTable_EmpPrjOwnerIns"] = "";
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.OwnerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
            {
                //String QueryValue = Utility.DecryptQS(Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName]);
                String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
                if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            //else if (per.CanView == false)
            //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (string.IsNullOrEmpty(Request.QueryString["OwnerId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
                Response.Redirect("Owner.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString()
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString()
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        SetControlBycmbType();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];        
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HDOwnerId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        HDOwnerId.Value = Utility.EncryptQS("-1");
        SetControlsNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetControlsEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Update();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Owner.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void btnObservers_Click(object sender, EventArgs e)
    {
        int ProjectId =Convert.ToInt32( Utility.DecryptQS(HDProjectId.Value));
        string GridFilterString = Request.QueryString["GrdFlt"].ToString();
        string SearchFilterString = Request.QueryString["SrchFlt"].ToString();
       
        int PrjReId =Convert.ToInt32( Utility.DecryptQS(RequestId.Value));

        if (!CheckProjectWorkFlowPermissionForObservers(PrjReId))
        {
            SetLabelWarning("در این مرحله از گردش کار امکان ثبت ناظر وجود ندارد.");
            return;
        }

        string QS = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString())
                  + "&PrjObsId=" + Utility.EncryptQS("-1")
                  + "&PageMode=" + Utility.EncryptQS("View")
                  + "&PageMode2=" + Utility.EncryptQS("New")
                  + "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
                  + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);
        Response.Redirect("ObserverInsert.aspx?" + QS);

    }

    protected void btnDesigners_Click(object sender, EventArgs e)
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        string GridFilterString = Request.QueryString["GrdFlt"].ToString();
        string SearchFilterString = Request.QueryString["SrchFlt"].ToString();

      
        //TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        //ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 0);
        //if (ProjectRequestManager.Count <= 0)
        //{
        //    SetLabelWarning("امکان ثبت طراح جدید وجود ندارد برای پروژه انتخاب شده درخواست معلق وجود ندارد");
        //    return;
        //}
        int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        if (!CheckProjectWorkFlowPermissionForDesigners(PrjReId))
        {
            SetLabelWarning("در این مرحله از گردش کار امکان ثبت طراح وجود ندارد.");
            return;
        }

        string QS = "DsPId=" + Utility.EncryptQS("-1") +
            "&PgMd=" + Utility.EncryptQS("New") +
            "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
            "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
            "&PageMode=" + Utility.EncryptQS("View") +
            "&PrjDesignerId=" + Utility.EncryptQS("-1") +
            "&PlnId=" + Utility.EncryptQS("-1") +
            "&PageSender=" + Utility.EncryptQS("Designer") +
            "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);

        Response.Redirect("AddPlanDesigner.aspx?" + QS);


    }

    #endregion

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, ProjectReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        WFUserControl.QueryStringForRedirect = "~/Employee/TechnicalServices/Project/OwnerInsert.aspx?" + "ProjectId=" + HDProjectId.Value + "&OwnerId=" + HDOwnerId.Value
            + "&PageMode=" + PgMode.Value
            + "&PageMode2=" + Request.QueryString["PageMode2"]
            + "&PrjReId=" + RequestId.Value
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        WFUserControl.PerformCallback(ProjectReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"].ToString());
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            HDOwnerId.Value = Server.HtmlDecode(Request.QueryString["OwnerId"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            ProjectId = Utility.DecryptQS(HDProjectId.Value);
            OwnerId = Utility.DecryptQS(HDOwnerId.Value);
            PrjReId = Utility.DecryptQS(RequestId.Value);
            string MPageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(OwnerId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(MPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            FillProjectInfo(int.Parse(PrjReId));

            SetMode(PageMode);
            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode(string PageMode)
    {
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

    private void SetNewModeKeys()
    {
        SetControlsNewMode();
    }

    private void SetEditModeKeys()
    {
        FillForm();
        SetControlsEditMode();
    }

    private void SetViewModeKeys()
    {
        FillForm();
        SetControlsViewMode();
    }

    private void SetControlsNewMode()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        SetEnable(true);
        ChbAgent.Checked = false;
        ChbAgent.Enabled = false;
        ClearForm();
        SetHaveLawyerFalse();
        SetOtherPerson();
        CmbType.Enabled = true;
        RoundPanelInfo.HeaderText = "جدید";
    }

    private void SetControlsEditMode()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        SetEnable(true);
        ChbAgent.Enabled = false;
        CmbType.Enabled = false;

        RoundPanelInfo.HeaderText = "ویرایش";
    }

    private void SetControlsViewMode()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();

        SetEnable(false);

        RoundPanelInfo.HeaderText = "مشاهده";
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.OwnerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (Utility.DecryptQS(PgMode.Value) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(PgMode.Value) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    /***************************************************************************************************************************************/
    protected void FillForm()
    {
        int OwnerId = Convert.ToInt32(Utility.DecryptQS(HDOwnerId.Value));

        try
        {
            TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
            OwnerManager.FindByOwnerId(OwnerId);
            if (OwnerManager.Count == 1)
            {
                CmbType.DataBind();
                CmbType.SelectedIndex = CmbType.Items.IndexOfValue(OwnerManager[0]["Type"].ToString());
                if (OwnerManager[0]["Type"].ToString() == "1")//otherperson
                {
                    SetOtherPerson();
                    txtoBirthPlace.Text = OwnerManager[0]["BirthPlace"].ToString();
                    txtoFatherName.Text = OwnerManager[0]["FMName"].ToString();
                    txtoFirstName.Text = OwnerManager[0]["FirstName"].ToString();
                    txtoIdNo.Text = OwnerManager[0]["IdNo"].ToString();
                    txtoLastName.Text = OwnerManager[0]["LastName"].ToString();
                    txtoSSN.Text = OwnerManager[0]["SSN"].ToString();
                }
                else //organization
                {
                    SetOrganization();
                    txtoManager.Text = OwnerManager[0]["FMName"].ToString();
                    txtoOrgName.Text = OwnerManager[0]["Name"].ToString();

                }
                txtoAddress.Text = OwnerManager[0]["Address"].ToString();
                txtoMobileNo.Text = OwnerManager[0]["MobileNo"].ToString();
                txtoTel.Text = OwnerManager[0]["Tel"].ToString();

                if (!Utility.IsDBNullOrNullValue(OwnerManager[0]["IsAgent"]))
                {
                    ChbAgent.DataBind();
                    ChbAgent.Checked = Convert.ToBoolean(OwnerManager[0]["IsAgent"]);
                }
                if (!Utility.IsDBNullOrNullValue(OwnerManager[0]["HaveLawyer"]))
                {
                    ChbHaveLawyer.DataBind();
                    ChbHaveLawyer.Checked = Convert.ToBoolean(OwnerManager[0]["HaveLawyer"]);
                }

                if (ChbHaveLawyer.Checked == true)
                {
                    SetHaveLawyerTrue();

                    txtlAddess.Text = OwnerManager[0]["LAddress"].ToString();
                    txtlBirthPlace.Text = OwnerManager[0]["LBirthPlace"].ToString();
                    txtlFatherName.Text = OwnerManager[0]["LFatherName"].ToString();
                    txtlFirstName.Text = OwnerManager[0]["LFirstName"].ToString();
                    txtlIdNo.Text = OwnerManager[0]["LIdNo"].ToString();
                    txtlLastName.Text = OwnerManager[0]["LLastName"].ToString();
                    txtlMobileNo.Text = OwnerManager[0]["LMobileNo"].ToString();
                    txtlSSN.Text = OwnerManager[0]["LSSN"].ToString();
                    txtlTel.Text = OwnerManager[0]["LTel"].ToString();
                }

            }
            else
            {
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }


    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    /***************************************************************************************************************************************/
    protected void SetEnable(bool Enabled)
    {
        RoundPanelInfo.Enabled = Enabled;
    }

    private void ClearForm()
    {
        for (int i = 0; i < RoundPanelInfo.Controls.Count; i++)
        {

            if (RoundPanelInfo.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)RoundPanelInfo.Controls[i];
                txt.Text = "";
            }
            if (RoundPanelInfo.Controls[i] is DevExpress.Web.ASPxMemo)
            {
                DevExpress.Web.ASPxMemo mo = (DevExpress.Web.ASPxMemo)RoundPanelInfo.Controls[i];
                mo.Text = "";
            }

        }
        ChbHaveLawyer.Checked = false;
        CmbType.SelectedIndex = 0;
    }

    private void SetOtherPerson()
    {
        //txtoAddress.ClientVisible = true;
        //txtoMobileNo.ClientVisible = true;
        //txtoTel.ClientVisible = true;
        txtoBirthPlace.ClientVisible = true;
        txtoFatherName.ClientVisible = true;
        txtoFirstName.ClientVisible = true;
        txtoIdNo.ClientVisible = true;
        txtoLastName.ClientVisible = true;
        txtoSSN.ClientVisible = true;
        txtoManager.ClientVisible = false;
        txtoOrgName.ClientVisible = false;

        ASPxlbloBirthPlace.ClientVisible = true;
        ASPxlbloFatherName.ClientVisible = true;
        ASPxlbloFirstName.ClientVisible = true;
        ASPxlbloIdNo.ClientVisible = true;
        ASPxlbloLastName.ClientVisible = true;
        ASPxlbloSSN.ClientVisible = true;
        ASPxlbloManager.ClientVisible = false;
        ASPxlbloOrgName.ClientVisible = false;

    }

    private void SetOrganization()
    {
        //txtoAddress.ClientVisible = true;
        //txtoMobileNo.ClientVisible = true;
        //txtoTel.ClientVisible = true;
        txtoBirthPlace.ClientVisible = false;
        txtoFatherName.ClientVisible = false;
        txtoFirstName.ClientVisible = false;
        txtoIdNo.ClientVisible = false;
        txtoLastName.ClientVisible = false;
        txtoSSN.ClientVisible = false;
        txtoManager.ClientVisible = true;
        txtoOrgName.ClientVisible = true;

        ASPxlbloBirthPlace.ClientVisible = false;
        ASPxlbloFatherName.ClientVisible = false;
        ASPxlbloFirstName.ClientVisible = false;
        ASPxlbloIdNo.ClientVisible = false;
        ASPxlbloLastName.ClientVisible = false;
        ASPxlbloSSN.ClientVisible = false;
        ASPxlbloManager.ClientVisible = true;
        ASPxlbloOrgName.ClientVisible = true;
    }

    private void SetHaveLawyerTrue()
    {
        txtlAddess.ClientVisible = true;
        txtlBirthPlace.ClientVisible = true;
        txtlFatherName.ClientVisible = true;
        txtlFirstName.ClientVisible = true;
        txtlIdNo.ClientVisible = true;
        txtlLastName.ClientVisible = true;
        txtlMobileNo.ClientVisible = true;
        txtlSSN.ClientVisible = true;
        txtlTel.ClientVisible = true;

        ASPxlblAddess.ClientVisible = true;
        ASPxlblBirthPlace.ClientVisible = true;
        ASPxlblFatherName.ClientVisible = true;
        ASPxlblFirstName.ClientVisible = true;
        ASPxlblIdNo.ClientVisible = true;
        ASPxlblLastName.ClientVisible = true;
        ASPxlblMobileNo.ClientVisible = true;
        ASPxlblSSN.ClientVisible = true;
        ASPxlblTel.ClientVisible = true;
    }

    private void SetHaveLawyerFalse()
    {
        txtlAddess.ClientVisible = false;
        txtlBirthPlace.ClientVisible = false;
        txtlFatherName.ClientVisible = false;
        txtlFirstName.ClientVisible = false;
        txtlIdNo.ClientVisible = false;
        txtlLastName.ClientVisible = false;
        txtlMobileNo.ClientVisible = false;
        txtlSSN.ClientVisible = false;
        txtlTel.ClientVisible = false;

        ASPxlblAddess.ClientVisible = false;
        ASPxlblBirthPlace.ClientVisible = false;
        ASPxlblFatherName.ClientVisible = false;
        ASPxlblFirstName.ClientVisible = false;
        ASPxlblIdNo.ClientVisible = false;
        ASPxlblLastName.ClientVisible = false;
        ASPxlblMobileNo.ClientVisible = false;
        ASPxlblSSN.ClientVisible = false;
        ASPxlblTel.ClientVisible = false;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    /***************************************************************************************************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();

        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OrganizationManager OrganizationManager = new TSP.DataManager.OrganizationManager();

        trans.Add(OwnerManager);
        trans.Add(OtherPersonManager);
        trans.Add(OrganizationManager);

        if (!CheckOwnerAgent())
            return;

        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع مالک را انتخاب نمایید");
            return;
        }

        try
        {
            trans.BeginSave();

            if (CmbType.Value.ToString() == "1")//OtherPerson            
                InsertOtherPerson(OtherPersonManager);
            else // Organization            
                InsertOrganization(OrganizationManager);

            if (ChbHaveLawyer.Checked == true)
                InsertLawyer(OtherPersonManager);

            InsertOwner(OwnerManager);

            trans.EndSave();

            SetLabelWarning("ذخیره انجام شد");
            PgMode.Value = Utility.EncryptQS("Edit");
            SetControlsEditMode();
        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err);
        }

        if (ChbHaveLawyer.Checked == true)
            SetHaveLawyerTrue();
        else
            SetHaveLawyerFalse();

        SetControlBycmbType();
    }

    private void InsertOwner(TSP.DataManager.TechnicalServices.OwnerManager OwnerManager)
    {
        DataRow drOwner = OwnerManager.NewRow();

        drOwner.BeginEdit();
        drOwner["ProjectId"] = Utility.DecryptQS(HDProjectId.Value);
        drOwner["OtherPersOrgId"] = Utility.DecryptQS(HFOtherPersOrgId.Value);
        drOwner["Type"] = CmbType.Value;
        drOwner["HaveLawyer"] = ChbHaveLawyer.Checked;
        if (ChbHaveLawyer.Checked)
            drOwner["LawyerId"] = Utility.DecryptQS(HFLawyerId.Value);
        else
            drOwner["LawyerId"] = DBNull.Value;
        if (!Utility.IsDBNullOrNullValue(txtoSSN.Text) && CheckOwnerIsMemmberOfNezam(txtoSSN.Text) != -1)
            drOwner["MeId"] = CheckOwnerIsMemmberOfNezam(txtoSSN.Text);
        else
            drOwner["MeId"]= DBNull.Value;

        drOwner["IsAgent"] = ChbAgent.Checked;
        drOwner["PrjReId"] = Utility.DecryptQS(RequestId.Value);
        drOwner["CreateDate"] = Utility.GetDateOfToday();
        drOwner["UserId"] = Utility.GetCurrentUser_UserId();
        drOwner["ModifiedDate"] = DateTime.Now;
        drOwner.EndEdit();

        OwnerManager.AddRow(drOwner);
        OwnerManager.Save();

        OwnerManager.DataTable.AcceptChanges();
        OwnerId = OwnerManager[0]["OwnerId"].ToString();
        HDOwnerId.Value = Utility.EncryptQS(OwnerId);
    }

    private void InsertOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager)
    {
        DataRow drOth = OtherPersonManager.NewRow();

        drOth.BeginEdit();
        drOth["Address"] = txtoAddress.Text;
        drOth["BirthPlace"] = txtoBirthPlace.Text;
        drOth["FatherName"] = txtoFatherName.Text;
        drOth["FirstName"] = txtoFirstName.Text;
        drOth["IdNo"] = txtoIdNo.Text;
        drOth["LastName"] = txtoLastName.Text;
        drOth["MobileNo"] = txtoMobileNo.Text;
        drOth["SSN"] = txtoSSN.Text;
        drOth["Tel"] = txtoTel.Text;
        drOth["OtpType"] = TSP.DataManager.OtherPersonType.Owner;
        drOth["UserId"] = Utility.GetCurrentUser_UserId();
        drOth["ModifiedDate"] = DateTime.Now;
        drOth.EndEdit();

        OtherPersonManager.AddRow(drOth);
        OtherPersonManager.Save();

        OtherPersonManager.DataTable.AcceptChanges();
        string OtherPersOrgId = OtherPersonManager[0]["OtpId"].ToString();
        HFOtherPersOrgId.Value = Utility.EncryptQS(OtherPersOrgId);
    }

    private void InsertOrganization(TSP.DataManager.OrganizationManager OrganizationManager)
    {
        DataRow drOrg = OrganizationManager.NewRow();

        drOrg.BeginEdit();
        drOrg["ManagerName"] = txtoManager.Text;
        drOrg["OrgName"] = txtoOrgName.Text;
        drOrg["Tel"] = txtoTel.Text;
        drOrg["MobileNo"] = txtoMobileNo.Text;
        drOrg["Address"] = txtoAddress.Text;
        drOrg["CreateDate"] = Utility.GetDateOfToday();
        drOrg["UserId"] = Utility.GetCurrentUser_UserId();
        drOrg["ModifiedDate"] = DateTime.Now;
        drOrg["Type"] = 1;
        drOrg.EndEdit();

        OrganizationManager.AddRow(drOrg);
        OrganizationManager.Save();

        OrganizationManager.DataTable.AcceptChanges();
        string OtherPersOrgId = OrganizationManager[0]["OrgId"].ToString();
        HFOtherPersOrgId.Value = Utility.EncryptQS(OtherPersOrgId);
    }

    private void InsertLawyer(TSP.DataManager.OtherPersonManager OtherPersonManager)
    {
        DataRow drOth = OtherPersonManager.NewRow();

        drOth.BeginEdit();
        drOth["Address"] = txtlAddess.Text;
        drOth["BirthPlace"] = txtlBirthPlace.Text;
        drOth["FatherName"] = txtlFatherName.Text;
        drOth["FirstName"] = txtlFirstName.Text;
        drOth["IdNo"] = txtlIdNo.Text;
        drOth["LastName"] = txtlLastName.Text;
        drOth["MobileNo"] = txtlMobileNo.Text;
        drOth["SSN"] = txtlSSN.Text;
        drOth["Tel"] = txtlTel.Text;
        drOth["OtpType"] = TSP.DataManager.OtherPersonType.OwnerLawyer;
        drOth["UserId"] = Utility.GetCurrentUser_UserId();
        drOth["ModifiedDate"] = DateTime.Now;
        drOth.EndEdit();

        OtherPersonManager.AddRow(drOth);
        OtherPersonManager.Save();

        OtherPersonManager.DataTable.AcceptChanges();
        string OtherPersId = OtherPersonManager[0]["OtpId"].ToString();
        HFLawyerId.Value = Utility.EncryptQS(OtherPersId);
    }

    private bool CheckOwnerAgent()
    {
        if (ChbAgent.Checked)
        {
            ProjectId = Utility.DecryptQS(HDProjectId.Value);
            OwnerId = Utility.DecryptQS(HDOwnerId.Value);

            TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
            OwnerManager.FindByProjectId(Convert.ToInt32(ProjectId));
            for (int i = 0; i < OwnerManager.Count; i++)
            {
                if (Convert.ToBoolean(OwnerManager[i]["IsAgent"]) && OwnerManager[i]["OwnerId"].ToString() != OwnerId)
                {
                    SetLabelWarning("نماینده مالک قبلاً انتخاب شده است");
                    return false;
                    //OwnerManager[i].BeginEdit();
                    //OwnerManager[i]["IsAgent"] = 0;
                    //OwnerManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                    //OwnerManager[i].EndEdit();
                    //OwnerManager.Save();
                }
            }
        }
        return true;
    }

    #region Update
    private void Update()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();

        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OrganizationManager OrganizationManager = new TSP.DataManager.OrganizationManager();

        trans.Add(OwnerManager);
        trans.Add(OtherPersonManager);
        trans.Add(OrganizationManager);

        if (!CheckOwnerAgent())
            return;

        if (CmbType.Value == null)
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
            return;
        }

        try
        {
            trans.BeginSave();

            OwnerId = Utility.DecryptQS(HDOwnerId.Value);
            OwnerManager.FindByOwnerId(Convert.ToInt32(OwnerId));

            if (OwnerManager.Count > 0)
            {
                if (CmbType.Value.ToString() == "1") //OtherPerson            
                    UpdateOtherPerson(OtherPersonManager, Convert.ToInt32(OwnerManager[0]["OtherPersOrgId"].ToString()));
                else // Organization
                    UpdateOrganization(OrganizationManager, Convert.ToInt32(OwnerManager[0]["OtherPersOrgId"].ToString()));

                if (ChbHaveLawyer.Checked)
                {
                    if (Convert.ToBoolean(OwnerManager[0]["HaveLawyer"]) && !Utility.IsDBNullOrNullValue(OwnerManager[0]["LawyerId"]))
                        UpdateLawyer(OtherPersonManager, Convert.ToInt32(OwnerManager[0]["LawyerId"].ToString()));
                    else
                        InsertLawyer(OtherPersonManager);
                }

                UpdateOwner(OwnerManager);

                trans.EndSave();
                SetLabelWarning("ذخیره انجام شد");
            }
            else
            {
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err);
        }

        if (ChbHaveLawyer.Checked == true)
            SetHaveLawyerTrue();
        else
            SetHaveLawyerFalse();

        SetControlBycmbType();
    }

    private void UpdateOwner(TSP.DataManager.TechnicalServices.OwnerManager OwnerManager)
    {
        if (OwnerManager.Count > 0)
        {
            OwnerManager[0].BeginEdit();
            OwnerManager[0]["ProjectId"] = Utility.DecryptQS(HDProjectId.Value);
            OwnerManager[0]["Type"] = CmbType.Value;
            OwnerManager[0]["HaveLawyer"] = ChbHaveLawyer.Checked;
            if (ChbHaveLawyer.Checked)
                OwnerManager[0]["LawyerId"] = Utility.DecryptQS(HFLawyerId.Value);
            else
                OwnerManager[0]["LawyerId"] = DBNull.Value;

            if (!Utility.IsDBNullOrNullValue(txtoSSN.Text) && CheckOwnerIsMemmberOfNezam(txtoSSN.Text) != -1)
                OwnerManager[0]["MeId"] = CheckOwnerIsMemmberOfNezam(txtoSSN.Text);
           else
                OwnerManager[0]["MeId"] = DBNull.Value;
            OwnerManager[0]["IsAgent"] = ChbAgent.Checked;
            OwnerManager[0]["PrjReId"] = Utility.DecryptQS(RequestId.Value);
            OwnerManager[0]["CreateDate"] = Utility.GetDateOfToday();
            OwnerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OwnerManager[0]["ModifiedDate"] = DateTime.Now;
            OwnerManager[0].EndEdit();

            OwnerManager.Save();
        }
    }

    private void UpdateOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager, int OtherPersOrgId)
    {
        OtherPersonManager.FindByCode(OtherPersOrgId);
        if (OtherPersonManager.Count == 1)
        {
            OtherPersonManager[0].BeginEdit();
            OtherPersonManager[0]["Address"] = txtoAddress.Text;
            OtherPersonManager[0]["BirthPlace"] = txtoBirthPlace.Text;
            OtherPersonManager[0]["FatherName"] = txtoFatherName.Text;
            OtherPersonManager[0]["FirstName"] = txtoFirstName.Text;
            OtherPersonManager[0]["IdNo"] = txtoIdNo.Text;
            OtherPersonManager[0]["LastName"] = txtoLastName.Text;
            OtherPersonManager[0]["MobileNo"] = txtoMobileNo.Text;
            OtherPersonManager[0]["SSN"] = txtoSSN.Text;
            OtherPersonManager[0]["Tel"] = txtoTel.Text;
            OtherPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
            OtherPersonManager[0].EndEdit();

            OtherPersonManager.Save();
            OtherPersonManager.DataTable.AcceptChanges();

            HFOtherPersOrgId.Value = Utility.EncryptQS(OtherPersOrgId.ToString());
        }
    }

    private void UpdateOrganization(TSP.DataManager.OrganizationManager OrganizationManager, int OtherPersOrgId)
    {
        OrganizationManager.FindByCodeForTS(OtherPersOrgId);
        if (OrganizationManager.Count == 1)
        {
            OrganizationManager[0].BeginEdit();
            OrganizationManager[0]["ManagerName"] = txtoManager.Text;
            OrganizationManager[0]["OrgName"] = txtoOrgName.Text;
            OrganizationManager[0]["Tel"] = txtoTel.Text;
            OrganizationManager[0]["MobileNo"] = txtoMobileNo.Text;
            OrganizationManager[0]["Address"] = txtoAddress.Text;
            OrganizationManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OrganizationManager[0]["ModifiedDate"] = DateTime.Now;
            OrganizationManager[0].EndEdit();

            OrganizationManager.Save();
            OrganizationManager.DataTable.AcceptChanges();

            HFOtherPersOrgId.Value = Utility.EncryptQS(OtherPersOrgId.ToString());
        }
    }

    private void UpdateLawyer(TSP.DataManager.OtherPersonManager OtherPersonManager, int LawyerId)
    {
        OtherPersonManager.FindByCode(LawyerId);
        if (OtherPersonManager.Count == 1)
        {
            OtherPersonManager[0].BeginEdit();
            OtherPersonManager[0]["Address"] = txtlAddess.Text;
            OtherPersonManager[0]["BirthPlace"] = txtlBirthPlace.Text;
            OtherPersonManager[0]["FatherName"] = txtlFatherName.Text;
            OtherPersonManager[0]["FirstName"] = txtlFirstName.Text;
            OtherPersonManager[0]["IdNo"] = txtlIdNo.Text;
            OtherPersonManager[0]["LastName"] = txtlLastName.Text;
            OtherPersonManager[0]["MobileNo"] = txtlMobileNo.Text;
            OtherPersonManager[0]["SSN"] = txtlSSN.Text;
            OtherPersonManager[0]["Tel"] = txtlTel.Text;
            OtherPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
            OtherPersonManager[0].EndEdit();

            OtherPersonManager.Save();
            OtherPersonManager.DataTable.AcceptChanges();

            HFLawyerId.Value = Utility.EncryptQS(LawyerId.ToString());
        }
    }

    #endregion

    #region WorkFlow 
    private void CheckWorkFlowPermission()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        BtnNew.Enabled = WFPer.BtnNew;
        BtnNew2.Enabled = WFPer.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private bool CheckProjectWorkFlowPermissionForObservers(int PrjReId)
    {
        //****TableType
        int ProjectWFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSObserverChangesConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectObserverRequestInfo;

        TSP.DataManager.WFPermission SaveWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SaveTaskCode, ProjectWFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ChangeTaskCode, ChangeWFCode, PrjReId, Utility.GetCurrentUser_UserId());

        return SaveWFPer.BtnNew || ChangeWFPer.BtnNew;
    }

    private bool CheckProjectWorkFlowPermissionForDesigners(int PrjReId)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());

        return (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew);
    }
    #endregion
    private void SetControlBycmbType()
    {
        if (CmbType.Value.ToString() == "1")
            SetOtherPerson();
        else
            SetOrganization();
    }

    private int CheckOwnerIsMemmberOfNezam(string SSN)
    {
        int MeId = -1;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.SelectActiveMembers(MeId, SSN);
        if (MemberManager.Count > 0)
        {
            MeId = Convert.ToInt32(MemberManager[0]["MeId"].ToString());
        }
        return MeId;
    }
    #endregion

}
