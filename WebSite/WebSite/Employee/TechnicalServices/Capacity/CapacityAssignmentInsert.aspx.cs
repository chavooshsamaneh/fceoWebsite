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

public partial class Employee_TechnicalServices_Capacity_CapacityAssignmentInsert : System.Web.UI.Page
{
    string PageMode;
    string CapacityAssignmentId;

    bool IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.CapacityAssignmentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["CapacityAssignmentId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("CapacityAssignment.aspx");
                return;
            }

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
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
        Response.Redirect("CapacityAssignment.aspx?");
    }

    #endregion

    #region Methods
    #region Set Key-Modes
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkCapacityAssignmentId.Value = Server.HtmlDecode(Request.QueryString["CapacityAssignmentId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            CapacityAssignmentId = Utility.DecryptQS(PkCapacityAssignmentId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(CapacityAssignmentId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            SetMode();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

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

    private void SetNewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        CheckAccess();
        SetEnabled(true);

        ASPxTextBoxYear.Text = Utility.GetYearOfToday();
        ASPxTextBoxCapacityPrcnt.Text =
        txtAssignmentDate.Text =
        txtEndDate.Text =
        txtWorkCountMainAgent.Text =
        txtWorkCountUnder400MainAgent.Text =
        txtAssignmentDateOtherAgent.Text =
            txtEndDateOtherAgents.Text =
            txtWorkCountOtherAgents.Text =
            txtWorkCountUnder400OtherAgents.Text = txtStopmandatoryFileUploadingMainAgent.Text = txtStopmandatoryFileUploadingOtherAgent.Text = "";
        CheckBoxFreeObsCapacityMainAgent.Checked =
            CheckBoxFreeDesCapacityMainAgent.Checked =
            CheckBoxFreeObsCapacityOtherAgents.Checked =
            CheckBoxFreeDesCapacityOtherAgents.Checked =false;
        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        SetEnabled(true);

        SetValues();
        ASPxRoundPanel2.HeaderText = "ویرایش";

    }

    private void SetViewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        SetEnabled(false);

        SetValues();

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetEnabled(Boolean Enabled)
    {
        ASPxTextBoxYear.Enabled = false;
        ASPxTextBoxCapacityPrcnt.Enabled =
        txtAssignmentDate.Enabled =
        txtEndDate.Enabled =
        txtWorkCountMainAgent.Enabled =
        txtWorkCountUnder400MainAgent.Enabled =
        txtAssignmentDateOtherAgent.Enabled =
            txtEndDateOtherAgents.Enabled =
            txtWorkCountOtherAgents.Enabled =
            txtWorkCountUnder400OtherAgents.Enabled =
        CheckBoxFreeObsCapacityMainAgent.Enabled =
            CheckBoxFreeDesCapacityMainAgent.Enabled =
            CheckBoxFreeObsCapacityOtherAgents.Enabled =
            CheckBoxFreeDesCapacityOtherAgents.Enabled = txtStopmandatoryFileUploadingMainAgent.Enabled = txtStopmandatoryFileUploadingOtherAgent.Enabled = Enabled;
    }
    #endregion

    private void SetValues()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        CapacityAssignmentId = Utility.DecryptQS(PkCapacityAssignmentId.Value);

        if ((string.IsNullOrEmpty(CapacityAssignmentId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.CapacityAssignmentManager Manager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        Manager.FindByCapacityAssignmentId(Convert.ToInt32(CapacityAssignmentId));
        if (Manager.Count == 1)
        {
            txtAssignmentDate.Enabled = false;
            ASPxTextBoxYear.Text = Manager[0]["Year"].ToString();
            ASPxTextBoxCapacityPrcnt.Text = Manager[0]["CapacityPrcnt"].ToString();
            if (!Utility.IsDBNullOrNullValue(Manager[0]["AssignmentDate"]))
            {
                txtAssignmentDate.Text = Manager[0]["AssignmentDate"].ToString();
                txtAssignmentDate.Enabled = false;
            }
            else
            {
                txtAssignmentDate.Text = "";
            }
            txtEndDate.Text = Manager[0]["EndDate"].ToString();
            txtWorkCountMainAgent.Text = Manager[0]["WorkCountMainAgent"].ToString();
            txtWorkCountUnder400MainAgent.Text = Manager[0]["WorkCountUnder400MainAgent"].ToString();
            if (!Utility.IsDBNullOrNullValue(Manager[0]["AssignmentDateOtherAgent"]))
            {
                txtAssignmentDateOtherAgent.Text = Manager[0]["AssignmentDateOtherAgent"].ToString();
                txtAssignmentDateOtherAgent.Enabled = false;
            }
            else
            {
                txtAssignmentDateOtherAgent.Text = "";
            }
            txtEndDateOtherAgents.Text = Manager[0]["EndDateOtherAgents"].ToString();
            txtWorkCountOtherAgents.Text = Manager[0]["WorkCountOtherAgents"].ToString();
            txtWorkCountUnder400OtherAgents.Text = Manager[0]["WorkCountUnder400OtherAgents"].ToString();
            if (Convert.ToBoolean(Manager[0]["FreeObsCapacityMainAgent"]))
            {
                CheckBoxFreeObsCapacityMainAgent.Enabled = false;
            }
            CheckBoxFreeObsCapacityMainAgent.Checked = Convert.ToBoolean(Manager[0]["FreeObsCapacityMainAgent"]);
            if (Convert.ToBoolean(Manager[0]["FreeDesCapacityMainAgent"]))
            {
                CheckBoxFreeDesCapacityMainAgent.Enabled = false;
            }
            CheckBoxFreeDesCapacityMainAgent.Checked = Convert.ToBoolean(Manager[0]["FreeDesCapacityMainAgent"]);
            if (Convert.ToBoolean(Manager[0]["FreeObsCapacityOtherAgents"]))
            {
                CheckBoxFreeObsCapacityOtherAgents.Enabled = false;
            }
            CheckBoxFreeObsCapacityOtherAgents.Checked = Convert.ToBoolean(Manager[0]["FreeObsCapacityOtherAgents"]);
            if (Convert.ToBoolean(Manager[0]["FreeDesCapacityOtherAgents"]))
            {
                CheckBoxFreeDesCapacityOtherAgents.Enabled = false;
            }
            CheckBoxFreeDesCapacityOtherAgents.Checked = Convert.ToBoolean(Manager[0]["FreeDesCapacityOtherAgents"]);
            if (!Utility.IsDBNullOrNullValue(Manager[0]["StopmandatoryFileUploadingMainAgent"]))
            {
                txtStopmandatoryFileUploadingMainAgent.Text = Manager[0]["StopmandatoryFileUploadingMainAgent"].ToString();
            }
            else
                txtStopmandatoryFileUploadingMainAgent.Text = "";
            if (!Utility.IsDBNullOrNullValue(Manager[0]["StopmandatoryFileUploadingOtherAgent"]))
            {
                txtStopmandatoryFileUploadingOtherAgent.Text = Manager[0]["StopmandatoryFileUploadingOtherAgent"].ToString();
            }
            else
                txtStopmandatoryFileUploadingOtherAgent.Text = "";            

        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.CapacityAssignmentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
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
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    /*******************************************************************************************************************************************/
    private int GetNextStageNum()
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        return CapacityAssignmentManager.GetNextStageNum(Utility.GetYearOfToday());
    }


    private void SetError(Exception err, char Flag)
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
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
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

    #region Insert-Update
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }


        Capacity Cpcty = new Capacity();
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager RepayConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(CapacityAssignmentManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ConditionalCapacityManager);
        transact.Add(RepayConditionalCapacityManager);

        try
        {
            transact.BeginSave();

            InsertCapacityAssignment(CapacityAssignmentManager);
            if(CheckBoxFreeDesCapacityOtherAgents.Checked)
            {

            }
            if (CheckBoxFreeObsCapacityOtherAgents.Checked)
            {

            }
            if (CheckBoxFreeDesCapacityMainAgent.Checked)
            {

            }
            if (CheckBoxFreeObsCapacityMainAgent.Checked)
            {

            }
            //*** فقط کار طراحی آزاد می شود و کار نظارت با اتمام پروژه آزاد می شود
            //   Cpcty.ToFreeObsCapacity(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, Utility.GetCurrentUser_UserId());
            //****
            //Cpcty.ToFreeDsgnCapacity(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, Utility.GetCurrentUser_UserId());
            SetRepay(ConditionalCapacityManager);
            SetLackRecovery(ConditionalCapacityManager, RepayConditionalCapacityManager);
            //InsertProjectRequest(ProjectRequestManager);
            //InsertWorkFlowState(WorkFlowStateManager, NezamChartManager, LoginManager, WorkFlowTaskManager);

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'I');
        }
    }

    private void InsertCapacityAssignment(TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager)
    {
        DataRow rowCapacityAssignment = CapacityAssignmentManager.NewRow();

        rowCapacityAssignment.BeginEdit();
        rowCapacityAssignment["Stage"] = 1;
        rowCapacityAssignment["JobCountPrcnt"] = 100;
        rowCapacityAssignment["RemainIsWaste"] = 1;
        rowCapacityAssignment["IsAssigned"] = 1;
        rowCapacityAssignment["CurrentJ"] = 1;
        rowCapacityAssignment["CapacityPrcnt"] = ASPxTextBoxCapacityPrcnt.Text;
        rowCapacityAssignment["Year"] = ASPxTextBoxYear.Text;

        rowCapacityAssignment["AssignmentDate"] = txtAssignmentDate.Text;
        rowCapacityAssignment["EndDate"] = txtEndDate.Text;
        rowCapacityAssignment["EndDateOtherAgents"] =txtAssignmentDate.Text;
        rowCapacityAssignment["AssignmentDateOtherAgent"] = txtAssignmentDateOtherAgent.Text;
        rowCapacityAssignment["WorkCountUnder400OtherAgents"] = txtWorkCountUnder400OtherAgents.Text;
        rowCapacityAssignment["WorkCountOtherAgents"] = txtWorkCountOtherAgents.Text;
        rowCapacityAssignment["FreeDesCapacityOtherAgents"] = CheckBoxFreeDesCapacityOtherAgents.Checked;
        rowCapacityAssignment["FreeObsCapacityOtherAgents"] =CheckBoxFreeObsCapacityOtherAgents.Checked;
        rowCapacityAssignment["WorkCountUnder400MainAgent"] = txtWorkCountUnder400MainAgent.Text;
        rowCapacityAssignment["WorkCountMainAgent"] = txtWorkCountMainAgent.Text;
        rowCapacityAssignment["FreeDesCapacityMainAgent"] = CheckBoxFreeDesCapacityMainAgent.Checked;
        rowCapacityAssignment["FreeObsCapacityMainAgent"] = CheckBoxFreeObsCapacityMainAgent.Checked;
        rowCapacityAssignment["StopmandatoryFileUploadingMainAgent"] = txtStopmandatoryFileUploadingMainAgent.Text;
        rowCapacityAssignment["StopmandatoryFileUploadingOtherAgent"] = txtStopmandatoryFileUploadingOtherAgent.Text;       

        rowCapacityAssignment["InActive"] = 0;
        rowCapacityAssignment["UserId"] = Utility.GetCurrentUser_UserId();
        rowCapacityAssignment["ModifiedDate"] = DateTime.Now;
        rowCapacityAssignment.EndEdit();

        CapacityAssignmentManager.AddRow(rowCapacityAssignment);
        CapacityAssignmentManager.Save();

        CapacityAssignmentManager.DataTable.AcceptChanges();
        CapacityAssignmentId = CapacityAssignmentManager[0]["CapacityAssignmentId"].ToString();
        PkCapacityAssignmentId.Value = Utility.EncryptQS(CapacityAssignmentId.ToString());

    }

    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        Capacity Cpcty = new Capacity();
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager RepayConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();

        //TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(CapacityAssignmentManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ConditionalCapacityManager);
        transact.Add(RepayConditionalCapacityManager);
        //transact.Add(ProjectRequestManager);       

        try
        {
            transact.BeginSave();

            UpdateCapacityAssignment(CapacityAssignmentManager);

            Cpcty.ToFreeObsCapacity(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, Utility.GetCurrentUser_UserId());
            Cpcty.ToFreeDsgnCapacity(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, Utility.GetCurrentUser_UserId());
            SetRepay(ConditionalCapacityManager);
            SetLackRecovery(ConditionalCapacityManager, RepayConditionalCapacityManager);

            //UpdateProjectRequest(ProjectRequestManager);

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err, 'U');
        }
    }

    private void UpdateCapacityAssignment(TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager)
    {
        CapacityAssignmentId = Utility.DecryptQS(PkCapacityAssignmentId.Value);

        if (string.IsNullOrEmpty(CapacityAssignmentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        CapacityAssignmentManager.FindByCapacityAssignmentId(Convert.ToInt32(CapacityAssignmentId));

        if (CapacityAssignmentManager.Count >= 1)
        {
            CapacityAssignmentManager[0].BeginEdit();
            CapacityAssignmentManager[0]["Stage"] = 1;
            CapacityAssignmentManager[0]["JobCountPrcnt"] = 100;
            CapacityAssignmentManager[0]["RemainIsWaste"] = 1;
            CapacityAssignmentManager[0]["IsAssigned"] = 1;
            CapacityAssignmentManager[0]["CapacityPrcnt"] = ASPxTextBoxCapacityPrcnt.Text;
            CapacityAssignmentManager[0]["Year"] = ASPxTextBoxYear.Text;

            CapacityAssignmentManager[0]["AssignmentDate"] = txtAssignmentDate.Text;
            CapacityAssignmentManager[0]["EndDate "] = txtEndDate.Text;
            CapacityAssignmentManager[0]["EndDateOtherAgents"] = txtAssignmentDate.Text;
            CapacityAssignmentManager[0]["AssignmentDateOtherAgent"] = txtAssignmentDateOtherAgent.Text;
            CapacityAssignmentManager[0]["WorkCountUnder400OtherAgents"] = txtWorkCountUnder400OtherAgents.Text;
            CapacityAssignmentManager[0]["WorkCountOtherAgents"] = txtWorkCountOtherAgents.Text;
            CapacityAssignmentManager[0]["FreeDesCapacityOtherAgents"] = CheckBoxFreeDesCapacityOtherAgents.Checked;
            CapacityAssignmentManager[0]["FreeObsCapacityOtherAgents"] = CheckBoxFreeObsCapacityOtherAgents.Checked;
            CapacityAssignmentManager[0]["WorkCountUnder400MainAgent"] = txtWorkCountUnder400MainAgent.Text;
            CapacityAssignmentManager[0]["WorkCountMainAgent"] = txtWorkCountMainAgent.Text;
            CapacityAssignmentManager[0]["FreeDesCapacityMainAgent"] = CheckBoxFreeDesCapacityMainAgent.Checked;
            CapacityAssignmentManager[0]["FreeObsCapacityMainAgent"] = CheckBoxFreeObsCapacityMainAgent.Checked;
            CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"] = txtStopmandatoryFileUploadingMainAgent.Text;
            CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"] = txtStopmandatoryFileUploadingOtherAgent.Text;

            CapacityAssignmentManager[0]["InActive"] = 0;
            CapacityAssignmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            CapacityAssignmentManager[0]["ModifiedDate"] = DateTime.Now;
            CapacityAssignmentManager[0].EndEdit();

            CapacityAssignmentManager.Save();

            CapacityAssignmentManager.DataTable.AcceptChanges();
            CapacityAssignmentId = CapacityAssignmentManager[0]["CapacityAssignmentId"].ToString();
            PkCapacityAssignmentId.Value = Utility.EncryptQS(CapacityAssignmentId.ToString());
        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }
    #endregion

    #region  Assignment 


    private void SetRepay(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager)
    {
        ConditionalCapacityManager.FindReasonIdAndToDate((int)TSP.DataManager.TSReason.Repay, "2");
        for (int i = 0; i < ConditionalCapacityManager.Count; i++)
        {
            ConditionalCapacityManager[i].BeginEdit();
            ConditionalCapacityManager[i]["ToDate"] = Utility.GetDateOfToday();
            ConditionalCapacityManager[i]["InActive"] = 1;
            ConditionalCapacityManager[i].EndEdit();
        }
        ConditionalCapacityManager.Save();
        ConditionalCapacityManager.DataTable.AcceptChanges();
    }

    private void SetLackRecovery(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager, TSP.DataManager.TechnicalServices.ConditionalCapacityManager RepayConditionalCapacityManager)
    {
        ConditionalCapacityManager.FindReasonIdAndToDate((int)TSP.DataManager.TSReason.LackRecovery, "2");
        for (int i = 0; i < ConditionalCapacityManager.Count; i++)
        {
            ConditionalCapacityManager[i].BeginEdit();
            ConditionalCapacityManager[i]["ToDate"] = Utility.GetDateOfToday();
            if (!Convert.ToBoolean(ConditionalCapacityManager[i]["IsConfirmed"]))
                ConditionalCapacityManager[i]["InActive"] = 1;
            ConditionalCapacityManager[i].EndEdit();

            if (Convert.ToBoolean(ConditionalCapacityManager[i]["IsConfirmed"]))
                InsertRepay(RepayConditionalCapacityManager, ConditionalCapacityManager[i]);
        }
        ConditionalCapacityManager.Save();
        ConditionalCapacityManager.DataTable.AcceptChanges();
    }

    private void InsertRepay(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager, DataRow ConditionalCapacityRow)
    {
        DataRow rowConditionalCapacity = ConditionalCapacityManager.NewRow();

        rowConditionalCapacity.BeginEdit();
        rowConditionalCapacity["LetterNo"] = ConditionalCapacityRow["LetterNo"];
        rowConditionalCapacity["LetterDate"] = ConditionalCapacityRow["LetterDate"];
        rowConditionalCapacity["ReasonId"] = (int)TSP.DataManager.TSReason.Repay;
        rowConditionalCapacity["Capacity"] = Convert.ToInt32(ConditionalCapacityRow["Capacity"]) * -1;
        rowConditionalCapacity["FromDate"] = Utility.GetDateOfToday();
        rowConditionalCapacity["ToDate"] = "2";
        rowConditionalCapacity["Description"] = ConditionalCapacityRow["Description"];
        rowConditionalCapacity["MemberTypeId"] = ConditionalCapacityRow["MemberTypeId"];
        rowConditionalCapacity["MeOfficeEngOId"] = ConditionalCapacityRow["MeOfficeEngOId"];
        rowConditionalCapacity["ProjectIngridientTypeId"] = ConditionalCapacityRow["ProjectIngridientTypeId"];
        rowConditionalCapacity["ProjectId"] = ConditionalCapacityRow["ProjectId"];
        rowConditionalCapacity["InActive"] = 0;
        rowConditionalCapacity["IsConfirmed"] = 1;
        rowConditionalCapacity["UserId"] = Utility.GetCurrentUser_UserId();
        rowConditionalCapacity["ModifiedDate"] = DateTime.Now;
        rowConditionalCapacity.EndEdit();

        ConditionalCapacityManager.AddRow(rowConditionalCapacity);
        ConditionalCapacityManager.Save();
        ConditionalCapacityManager.DataTable.AcceptChanges();
    }
    #endregion       
    #endregion
}