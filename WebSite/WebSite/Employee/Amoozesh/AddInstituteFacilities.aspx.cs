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

public partial class Employee_Amoozesh_AddInstituteFacilities : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PrePageMode"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
        {
            Response.Redirect("Institue.aspx");
            return;
        }

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;                    
            SetKeys();
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString()));
            CheckCertificatePermission(InsId);
            
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        HiddenFieldFacility["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldFacility["InsFacilityId"] = "";
        TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnableControls();
        TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldFacility["PageMode"].ToString());

        string InsFacilityId = Utility.DecryptQS(HiddenFieldFacility["InsFacilityId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertInstitueFacility();

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(InsFacilityId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditInstitueFacility(int.Parse(InsFacilityId));
                }

            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstituteFacilities.aspx?InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PgMd=" + HiddenFieldFacility["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
    }

    protected void cmbEquipmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbEquipmentType.SelectedIndex==0)
        {
            txtEquipment.Visible = true;
            txtEquipmentCount.Visible = true;
            lblEquipName.Visible = true;
            lblEquipCount.Visible = true;

            txtFacilityName.Visible = false;
            txtCapacity.Visible = false;
            lblFacilityName.Visible = false;
            lblCapacity.Visible = false;
        }
        else
        {
            txtEquipment.Visible = false;
            txtEquipmentCount.Visible = false;
            lblEquipName.Visible = false;
            lblEquipCount.Visible = false;

            txtFacilityName.Visible = true;
            txtCapacity.Visible = true;
            lblFacilityName.Visible = true;
            lblCapacity.Visible = true;
        }
    }
    #endregion

    #region Methods

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
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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
        txtDescription.Text = "";
        txtCapacity.Text = "";
        txtEquipment.Text = "";
        txtEquipmentCount.Text = "";
        txtFacilityName.Text = "";
        cmbEquipmentType.SelectedIndex = 0;
        cmbEquipmentType_SelectedIndexChanged(this, new EventArgs());
    }

    private void SetKeys()
    {
        HiddenFieldFacility["InsId"] = Request.QueryString["InsId"].ToString();
        HiddenFieldFacility["PrePageMode"] = Request.QueryString["PrePageMode"];
        HiddenFieldFacility["PageMode"] = Request.QueryString["PageMode"];
        HiddenFieldFacility["InsFacilityId"] = Request.QueryString["InsFacilityId"];
        string InsFacilityId = Utility.DecryptQS(HiddenFieldFacility["InsFacilityId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldFacility["PageMode"].ToString());
       
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        InstitueManager.FindByCode(int.Parse(Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString())));

        if (InstitueManager.Count > 0)
        {
            lblInsName.Text = "موسسه: " + InstitueManager[0]["InsName"].ToString();
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }      
        SetMode(PageMode);
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
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
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

        //Set Textboxe's & comboboxe's Enabled
        txtFacilityName.Enabled = false;
        txtEquipmentCount.Enabled = false;
        txtEquipment.Enabled = false;
        txtDescription.Enabled = false;
        txtCapacity.Enabled = false;
        cmbEquipmentType.Enabled = false;
        string InsFacilityId = Utility.DecryptQS(HiddenFieldFacility["InsFacilityId"].ToString());
        FillForm(int.Parse(InsFacilityId));

        RoundPanelFacility.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
      
        ClearForm();

        RoundPanelFacility.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        string InsFacilityId = Utility.DecryptQS(HiddenFieldFacility["InsFacilityId"].ToString());
        if (string.IsNullOrEmpty(InsFacilityId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();

        FillForm(int.Parse(InsFacilityId));

        RoundPanelFacility.HeaderText = "ویرایش";
    }

    //private void SetJudgeModeKeys()
    //{
    //    btnNew.Enabled = false;
    //    btnNew2.Enabled = false;
    //    btnEdit.Enabled = false;
    //    btnEdit2.Enabled = false;
    //    btnDelete.Enabled = false;
    //    btnDelete2.Enabled = false;
    //    btnSave.Enabled = true;
    //    btnSave2.Enabled = true;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnDelete"] = btnDelete.Enabled;
    //    this.ViewState["BtnNew"] = btnNew.Enabled;

    //    Set Textboxe's & comboboxe's Enabled
    //    txtDescription.Enabled = false;
    //    txtEndDate.Enabled = false;
    //    txtJobName.Enabled = false;
    //    txtJobPlace.Enabled = false;
    //    txtStartDate.Enabled = false;
    //    cmbCountry.Enabled = false;
    //    txtCity.Enabled = false;
    //    chbIsTeaching.Enabled = false;

    //    btnDeleteAttachment.Enabled = false;

    //    FillForm(int.Parse(TJobHistoryId));

    //    RoundPanelTeacherJob.HeaderText = "مشاهده";
    //    RoundPanelJudge.Visible = true;
    //}

    private void FillForm(int InsFacilityId)
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
        InstitueFacilityManager.FindByCode(InsFacilityId);

        if (InstitueFacilityManager.Count == 1)
        {
            Boolean IsEquipment = Boolean.Parse(InstitueFacilityManager[0]["IsEquipment"].ToString());
            if (IsEquipment)
            {
                cmbEquipmentType.SelectedIndex = 0;
                cmbEquipmentType_SelectedIndexChanged(this, new EventArgs());
                txtEquipment.Text = InstitueFacilityManager[0]["FacilityName"].ToString();
                txtEquipmentCount.Text = InstitueFacilityManager[0]["Capacity"].ToString();
                txtDescription.Text = InstitueFacilityManager[0]["Description"].ToString();
            }
            else
            {
                cmbEquipmentType.SelectedIndex = 1;
                cmbEquipmentType_SelectedIndexChanged(this, new EventArgs());
                txtFacilityName.Text = InstitueFacilityManager[0]["FacilityName"].ToString();
                txtCapacity.Text = InstitueFacilityManager[0]["Capacity"].ToString();
                txtDescription.Text = InstitueFacilityManager[0]["Description"].ToString();
            }
        }        
    }

    private void EnableControls()
    {
        if (cmbEquipmentType.SelectedIndex == 0)
        {
            cmbEquipmentType.Enabled = true;
            txtDescription.Enabled = true;
            txtEquipment.Enabled = true;
            txtEquipmentCount.Enabled = true;

        }
        else
        {
            cmbEquipmentType.Enabled = true;
            txtDescription.Enabled = true;
            txtFacilityName.Enabled = true;
            txtCapacity.Enabled = true;
        }
    }

    private void InsertInstitueFacility()
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        try
        {
            DataRow InsFacilityRow = InstitueFacilityManager.NewRow();

            InsFacilityRow["InsId"] = int.Parse(Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString()));
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString()));
            DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId,0);
            if (dtInsCert.Rows.Count > 0)
            {
                int InsCId = int.Parse(dtInsCert.Rows[0]["InsCId"].ToString());
                InsFacilityRow["InsCId"] = InsCId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            if (cmbEquipmentType.SelectedIndex == 0)
            {
                InsFacilityRow["FacilityName"] = txtEquipment.Text;
                InsFacilityRow["Capacity"] = txtEquipmentCount.Text;
                InsFacilityRow["IsEquipment"] = 1;
               // InsFacilityRow["Equipment"] = "";
            }
            else
            {
                InsFacilityRow["FacilityName"] = txtFacilityName.Text;
                InsFacilityRow["Capacity"] = txtCapacity.Text;
                InsFacilityRow["IsEquipment"] = 0;
            }

            InsFacilityRow["Description"] = txtDescription.Text;
            InsFacilityRow["UserId"] =Utility.GetCurrentUser_UserId();
            InsFacilityRow["ModifiedDate"] = DateTime.Now;


            InstitueFacilityManager.AddRow(InsFacilityRow);

            int cn= InstitueFacilityManager.Save();
            if (cn > 0)
            {
                HiddenFieldFacility["InsFacilityId"]= Utility.EncryptQS(InstitueFacilityManager[0]["InsFacilityId"].ToString());
                HiddenFieldFacility["PageMode"] = Utility.EncryptQS("Edit");
                TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                RoundPanelFacility.HeaderText = "ویرایش";
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void EditInstitueFacility(int InsFacilityId)
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();   
        try
        {
            InstitueFacilityManager.FindByCode(InsFacilityId);
            if (InstitueFacilityManager.Count == 1)
            {
                InstitueFacilityManager[0].BeginEdit();
                if (cmbEquipmentType.SelectedIndex==0)
                {
                    InstitueFacilityManager[0]["IsEquipment"] = 1;
                    InstitueFacilityManager[0]["FacilityName"] = txtEquipment.Text;
                    InstitueFacilityManager[0]["Capacity"] = txtEquipmentCount.Text;
                    //InstitueFacilityManager[0]["Equipment"] = e.NewValues["Equipment"];
                    InstitueFacilityManager[0]["Description"] = txtDescription.Text;
                    InstitueFacilityManager[0]["ModifiedDate"] = DateTime.Now;
                    InstitueFacilityManager[0]["UserId"] =Utility.GetCurrentUser_UserId();

                    InstitueFacilityManager[0].EndEdit();
                }
                else
                {
                    InstitueFacilityManager[0]["IsEquipment"] = 0;
                    InstitueFacilityManager[0]["FacilityName"] = txtFacilityName.Text;
                    InstitueFacilityManager[0]["Capacity"] = txtCapacity.Text;
                    //InstitueFacilityManager[0]["Equipment"] = e.NewValues["Equipment"];
                    InstitueFacilityManager[0]["Description"] = txtDescription.Text;
                    InstitueFacilityManager[0]["ModifiedDate"] = DateTime.Now;
                    InstitueFacilityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                    InstitueFacilityManager[0].EndEdit();
                }

                int cn = InstitueFacilityManager.Save();
                if (cn > 0)
                {
                    
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            CheckWorkFlowPermissionForSave();
            //string PageMode = Utility.DecryptQS(HiddenFieldTeacherJob["PageMode"].ToString());
            //if (PageMode == "View")
            //    CheckWorkFlowPermissionForEdit();
        }
    }

    private void CheckWorkFlowPermissionForSave()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Institue;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();


        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(InsId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
        {
            int SaveInstitueWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveInstitueWorkCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);

            if (TaskDoerManager.Count > 0)
            {
                int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
                NezamMemberChartManager.FindByNcId(NcId);

                int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

                LoginManager.FindByMeIdUltId(EmpId, 4);
                if (LoginManager.Count > 0)
                {
                    int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                    int CurrentUserId =Utility.GetCurrentUser_UserId();
                    string PageMode = Utility.DecryptQS(HiddenFieldFacility["PageMode"].ToString());
                    if (CurrentUserId == userId)
                    {
                        if (PageMode == "New")
                        {
                            btnNew.Enabled = false;
                            btnNew2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            btnSave.Enabled = true;
                            btnSave2.Enabled = true;
                        }
                        if (PageMode == "Edit")
                        {
                            btnNew.Enabled = true;
                            btnNew2.Enabled = true;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            btnSave.Enabled = true;
                            btnSave2.Enabled = true;
                        }
                        if (PageMode == "View")
                        {
                            btnNew.Enabled = true;
                            btnNew2.Enabled = true;
                            btnEdit.Enabled = true;
                            btnEdit2.Enabled = true;
                            btnSave.Enabled = false;
                            btnSave2.Enabled = false;
                        }
                    }
                    else
                    {

                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                    }
                }
                else
                {
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                }
            }
            else
            {
                btnNew.Enabled = false;
                btnNew2.Enabled = false;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave2.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Institue;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(InsId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        if (TaskOrder != 0 && CurrentTaskOrder != TaskOrder)
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }

        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private int FindInstitueCertificate(int InsId)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId,0);
        int InsCertType = -1;
        if (dtInsCert.Rows.Count > 0)
        {
            InsCertType = int.Parse(dtInsCert.Rows[0]["Type"].ToString());
        }
        return InsCertType;
    }

    private void CheckCertificatePermission(int InsId)
    {
        int CertType = FindInstitueCertificate(InsId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldFacility["PageMode"].ToString());
        if (CertType == 1 || CertType == 2)
        {
            switch (PageMode)
            {
                case"View":

                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    break;
                case "Edit":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }           
        }       
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion
}
