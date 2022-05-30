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
using DevExpress.Web;

public partial class Employee_Amoozesh_AddTeacherJobHistory : System.Web.UI.Page
{
    string TJobHistoryId;
    string PageMode;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeacherId"]))
        {
            Response.Redirect("Teachers.aspx");
            return;
        }

        if (!IsPostBack)
        {
            Session["TeacherLiAttachFileAddress"] = "";            
            Session["IsEdited_TeJobHistoryJudg"] = false;
            Session["IsEdited_TeJobHistory"] = false;
            cmbCountry.SelectedIndex = 0;

            TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            TSP.DataManager.Permission perAttach = TSP.DataManager.AttachmentsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnAttachTechearFile.Enabled = perAttach.CanNew;
            btnDeleteAttachment.Enabled = perAttach.CanDelete;

            SetKeys();

            CheckWorkFlowPermission();
            string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
            CheckCertificatePermission(int.Parse(TeId));

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["AddAttach"] = btnAttachTechearFile.Enabled;
            this.ViewState["DeleteAttach"] = btnDeleteAttachment.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["AddAttach"] != null)
            this.btnAttachTechearFile.Enabled = (bool)this.ViewState["AddAttach"];
        if (this.ViewState["DeleteAttach"] != null)
            this.btnDeleteAttachment.Enabled = (bool)this.ViewState["DeleteAttach"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTeacherJob["TeacherId"] + "&PageMode=" + HiddenFieldTeacherJob["PrePageMode"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTeacherJob["PageMode"].ToString());

        string TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTeacherJobHistory();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TJobHistoryId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditTeacherJobHistory(int.Parse(TJobHistoryId));
                }

            }
            else
            {
                if (PageMode == "Judge")
                {
                    if (string.IsNullOrEmpty(TJobHistoryId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                    {
                        int TableType = -1;
                        TableType = (int)(TSP.DataManager.TableCodes.TeacherJobHistory);
                        string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
                        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
                        DataTable dtTeacherJudgment = TeacherJudgmentManager.FindByResearchActivity(int.Parse(TeId), int.Parse(TJobHistoryId), TableType);
                        if (dtTeacherJudgment.Rows.Count == 1)
                        {
                            int JudgeId = int.Parse(dtTeacherJudgment.Rows[0]["JudgeId"].ToString());
                            EditJudgment(JudgeId);
                        }
                        else
                            InsertJudgment();
                    }
                }
            }

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());

        DeleteTeacherJobHistory(int.Parse(TJobHistoryId));
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();

        EnableControls();

        HiddenFieldTeacherJob["PageMode"] = Utility.EncryptQS("New");

        HiddenFieldTeacherJob["TJobHistoryId"] = Utility.EncryptQS("");

        linkAttachment.Visible = false;
        btnDeleteAttachment.Visible = false;
        btnAttachTechearFile.Visible = true;
        btnAttachTechearFile.Enabled = true;
        TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnableControls();
        HiddenFieldTeacherJob["PageMode"] = Utility.EncryptQS("Edit");
        RoundPanelTeacherJob.HeaderText = "ویرایش";

        TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        TSP.DataManager.Permission perAttach = TSP.DataManager.AttachmentsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnDeleteAttachment.Enabled = perAttach.CanDelete;
        btnAttachTechearFile.Enabled = perAttach.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
        DataTable dtAttach = AttachmentsManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherJobHistory), int.Parse(TJobHistoryId));
        if (dtAttach.Rows.Count == 0)
        {
            btnAttachTechearFile.Visible = true;
            btnDeleteAttachment.Visible = false;
        }
        this.ViewState["AddAttach"] = btnAttachTechearFile.Enabled;
        this.ViewState["DeleteAttach"] = btnDeleteAttachment.Enabled;
    }

    protected void btnDeleteAttachment_Click(object sender, EventArgs e)
    {
        TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
        DeleteTeacherAttachment(int.Parse(TJobHistoryId));
        linkAttachment.Visible = false;
        btnDeleteAttachment.Visible = false;
        // flp.Visible = true;
        lblAttachment.Visible = true;
        btnAttachTechearFile.Visible = true;
    }

    protected void btnSaveJudgment_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();

        try
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
            TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
            float SysGrade = -1;
            DataTable dtTGrade = TeachingGradeManager.SelectByInActive(false);
            if (dtTGrade.Rows.Count > 0)
            {
                //if (chbIsTeaching.Checked)
                if (cmbIsTeaching.SelectedIndex == 0)
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeTeaching"].ToString());
                else
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeRelateJob"].ToString());
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل نامشخص بودن حاکثر امتیاز تایید شده توسط سیستم امکان اعمال نظر کارشناسی وجود ندارد.";
                return;
            }

            if (float.Parse(txtGrade.Text) > SysGrade)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امتیاز بایستی کوچکتر یا مساوی" + SysGrade.ToString() + " " + "باشد.";
                return;
            }
            TeacherJobHistoryManager.FindByCode(int.Parse(TJobHistoryId));

            if (TeacherJobHistoryManager.Count > 0)
            {
                TeacherJobHistoryManager[0].BeginEdit();

                TeacherJobHistoryManager[0]["JudgeViewPoint"] = txtJudgeView.Text;
                TeacherJobHistoryManager[0]["JudgeGrade"] = txtGrade.Text;
                // TeacherJobHistoryManager[0]["MeetingId"] = "";
                TeacherJobHistoryManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherJobHistoryManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherJobHistoryManager[0].EndEdit();

                int cn = TeacherJobHistoryManager.Save();

                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است.";
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

    #endregion

    #region Methods
    private void InsertTeacherJobHistory()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        TransactionManager.Add(AttachmentsManager);
        TransactionManager.Add(TeacherJobHistoryManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            DataRow TJobRow = TeacherJobHistoryManager.NewRow();

            string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());

            DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(int.Parse(TeId));
            if (dtTeCert.Rows.Count > 0)
            {
                int TcId = int.Parse(dtTeCert.Rows[0]["TcId"].ToString());
                TJobRow["TcId"] = TcId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پرونده ای برای این استاد تشکیل نشده است.";
                return;
            }

            TJobRow["TJobPlace"] = txtJobPlace.Text;
            TJobRow["TJobName"] = txtJobName.Text;
            TJobRow["TeId"] = int.Parse(TeId);
            TJobRow["CounId"] = int.Parse(cmbCountry.SelectedItem.Value.ToString());
            TJobRow["CitName"] = txtCity.Text;
            TJobRow["StartDate"] = txtStartDate.Text;
            TJobRow["EndDate"] = txtStartDate.Text;
            if (cmbIsTeaching.SelectedIndex == 0)
                TJobRow["IsTeaching"] = 1;
            else
                TJobRow["IsTeaching"] = 0;
            TJobRow["Description"] = txtDescription.Text;
            TJobRow["UserId"] = Utility.GetCurrentUser_UserId();
            TJobRow["ModifiedDate"] = DateTime.Now;

            TeacherJobHistoryManager.AddRow(TJobRow);

            TransactionManager.BeginSave();
            int cn = TeacherJobHistoryManager.Save();
            InsertTeacherAttachment(int.Parse(TeacherJobHistoryManager[0]["TJobHistoryId"].ToString()), AttachmentsManager, TransactionManager);
            if (cn > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_TeJobHistory"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherJobHistory;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_TeJobHistory"] = true;
                    HiddenFieldTeacherJob["PageMode"] = Utility.EncryptQS("Edit");
                    HiddenFieldTeacherJob["TJobHistoryId"] = Utility.EncryptQS(TeacherJobHistoryManager[0]["TJobHistoryId"].ToString());
                    RoundPanelTeacherJob.HeaderText = "ویرایش";
                    TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnNew.Enabled = per.CanNew;
                    btnNew2.Enabled = per.CanNew;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnNew"] = btnNew.Enabled;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }


    }

    private void EditTeacherJobHistory(int TJHistoryId)
    {
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TransactionManager.Add(TeacherJobHistoryManager);
        TransactionManager.Add(AttachmentsManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            TransactionManager.BeginSave();

            TeacherJobHistoryManager.FindByCode(TJHistoryId);

            if (TeacherJobHistoryManager.Count > 0)
            {
                string teId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());

                TeacherJobHistoryManager[0]["TJobPlace"] = txtJobPlace.Text;
                TeacherJobHistoryManager[0]["TJobName"] = txtJobName.Text;
                TeacherJobHistoryManager[0]["TeId"] = int.Parse(teId);
                TeacherJobHistoryManager[0]["CounId"] = int.Parse(cmbCountry.SelectedItem.Value.ToString());
                TeacherJobHistoryManager[0]["CitName"] = txtCity.Text;
                TeacherJobHistoryManager[0]["StartDate"] = txtStartDate.Text;
                TeacherJobHistoryManager[0]["EndDate"] = txtStartDate.Text;
                if (cmbIsTeaching.SelectedIndex == 0)
                    TeacherJobHistoryManager[0]["IsTeaching"] = 1;
                else
                    TeacherJobHistoryManager[0]["IsTeaching"] = 0;
                TeacherJobHistoryManager[0]["Description"] = txtDescription.Text;
                TeacherJobHistoryManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherJobHistoryManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherJobHistoryManager[0].EndEdit();

                int cn = TeacherJobHistoryManager.Save();
                if (cn > 0)
                {
                    if (!string.IsNullOrEmpty(Session["TeacherLiAttachFileAddress"].ToString()))
                    {
                        InsertTeacherAttachment((int)(TeacherJobHistoryManager[0]["TJobHistoryId"]), AttachmentsManager, TransactionManager);
                    }
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_TeJobHistory"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherJobHistory;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(teId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        HiddenFieldTeacherJob["PageMode"] = Utility.EncryptQS("Edit");
                        HiddenFieldTeacherJob["TJobHistoryId"] = Utility.EncryptQS(TeacherJobHistoryManager[0]["TJobHistoryId"].ToString());
                        Session["IsEdited_TeJobHistory"] = true;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                }
                else
                {
                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                TransactionManager.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.EndSave();
            SetError(err);
        }
    }

    private void DeleteTeacherJobHistory(int TJobHistoryId)
    {
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();
        TeacherJobHistoryManager.FindByCode(TJobHistoryId);
        if (TeacherJobHistoryManager.Count > 0)
        {
            TeacherJobHistoryManager[0].Delete();

            int cn = TeacherJobHistoryManager.Save();
            if (cn > 0)
            {
                HiddenFieldTeacherJob["PageMode"] = Utility.EncryptQS("New");
                HiddenFieldTeacherJob["TJobHistoryId"] = Utility.EncryptQS("");
                ClearForm();
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
        txtEndDate.Text = "";
        txtJobName.Text = "";
        txtJobPlace.Text = "";
        txtStartDate.Text = "";
        txtCity.Text = "";
        //cmbCountry.SelectedIndex = 0;
        int CurrentCountry = Utility.GetCurrentCounId();
        cmbCountry.DataBind();
        if (!Utility.IsDBNullOrNullValue(cmbCountry.Items.FindByValue(CurrentCountry.ToString())))
            cmbCountry.SelectedIndex = cmbCountry.Items.FindByValue(CurrentCountry.ToString()).Index;
        // cmbCity.SelectedIndex = 0;
    }

    private void SetKeys()
    {
        HiddenFieldTeacherJob["TeacherId"] = Request.QueryString["TeacherId"].ToString();
        HiddenFieldTeacherJob["PrePageMode"] = Request.QueryString["PrePageMode"];
        HiddenFieldTeacherJob["PageMode"] = Request.QueryString["PageMode"];
        HiddenFieldTeacherJob["TJobHistoryId"] = Request.QueryString["TJobHistoryId"];
        string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
        PageMode = Utility.DecryptQS(HiddenFieldTeacherJob["PageMode"].ToString());
        TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherManager.FindByCode(int.Parse(TeId));

        if (TeacherManager.Count > 0)
        {
            RoundPanelTeacherJob.HeaderText = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
        }
        else
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        linkAttachment.Visible = false;
        lblAttachment.Visible = false;
        btnAttachTechearFile.Visible = false;
        btnDeleteAttachment.Visible = false;
        SetMode();
    }

    private void SetMode()
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
            case "Judge":
                SetJudgeModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        txtDescription.Enabled = false;
        txtEndDate.Enabled = false;
        txtJobName.Enabled = false;
        txtJobPlace.Enabled = false;
        txtStartDate.Enabled = false;
        cmbCountry.Enabled = false;
        txtCity.Enabled = false;
        cmbIsTeaching.Enabled = false;
        btnDeleteAttachment.Enabled = false;
        btnAttachTechearFile.Enabled = false;

        FillForm(int.Parse(TJobHistoryId));

        RoundPanelTeacherJob.HeaderText = "مشاهده";
        this.ViewState["AddAttach"] = btnAttachTechearFile.Enabled;
        this.ViewState["DeleteAttach"] = btnDeleteAttachment.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        btnAttachTechearFile.Visible = true;
        lblAttachment.Visible = true;
        ClearForm();

        RoundPanelTeacherJob.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        btnDeleteAttachment.Enabled = true;
        if (string.IsNullOrEmpty(TJobHistoryId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();

        FillForm(int.Parse(TJobHistoryId));

        RoundPanelTeacherJob.HeaderText = "ویرایش";
    }

    private void SetJudgeModeKeys()
    {
        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        this.ViewState["BtnNew"] = btnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        txtDescription.Enabled = false;
        txtEndDate.Enabled = false;
        txtJobName.Enabled = false;
        txtJobPlace.Enabled = false;
        txtStartDate.Enabled = false;
        cmbCountry.Enabled = false;
        txtCity.Enabled = false;
        cmbIsTeaching.Enabled = false;

        btnDeleteAttachment.Enabled = false;

        FillForm(int.Parse(TJobHistoryId));

        RoundPanelTeacherJob.HeaderText = "مشاهده";
        RoundPanelJudge.Visible = true;
    }

    private void FillForm(int TJobHistoryId)
    {
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TeacherJobHistoryManager.FindByCode(TJobHistoryId);

        if (TeacherJobHistoryManager.Count > 0)
        {
            txtDescription.Text = TeacherJobHistoryManager[0]["Description"].ToString();
            txtEndDate.Text = TeacherJobHistoryManager[0]["EndDate"].ToString();
            txtJobName.Text = TeacherJobHistoryManager[0]["TJobName"].ToString();
            txtJobPlace.Text = TeacherJobHistoryManager[0]["TJobPlace"].ToString();
            txtStartDate.Text = TeacherJobHistoryManager[0]["StartDate"].ToString();
            cmbCountry.DataBind();
            txtCity.Text = TeacherJobHistoryManager[0]["CitName"].ToString();
            cmbCountry.SelectedIndex = cmbCountry.Items.IndexOfValue(TeacherJobHistoryManager[0]["CounId"].ToString());
            if (Boolean.Parse(TeacherJobHistoryManager[0]["IsTeaching"].ToString()))
                cmbIsTeaching.SelectedIndex = 0;
            else
                cmbIsTeaching.SelectedIndex = 1;
            //chbIsTeaching.Checked = Boolean.Parse(TeacherJobHistoryManager[0]["IsTeaching"].ToString());
            TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
            DataTable dtAttach = AttachmentsManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherJobHistory), TJobHistoryId);
            if (dtAttach.Rows.Count > 0)
            {
                btnDeleteAttachment.Visible = true;
                string filePath = dtAttach.Rows[0]["FilePath"].ToString();
                linkAttachment.Visible = true;
                lblAttachment.Visible = true;
                linkAttachment.Text = Path.GetFileName(filePath);
                linkAttachment.NavigateUrl = filePath;
            }
            else
            {
                btnAttachTechearFile.Visible = true;
                btnDeleteAttachment.Visible = false;
                linkAttachment.Visible = false;
            }

            float SysGrade = -1;
            DataTable dtTGrade = TeachingGradeManager.SelectByInActive(false);
            if (dtTGrade.Rows.Count > 0)
            {
                //if (chbIsTeaching.Checked)
                if (cmbIsTeaching.SelectedIndex == 0)
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeTeaching"].ToString());
                else
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeRelateJob"].ToString());
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل نامشخص بودن حداکثر امتیاز تایید شده توسط سیستم امکان اعمال نظر کارشناسی وجود ندارد.";
                return;
            }

            int TableType = -1;
            TableType = (int)(TSP.DataManager.TableCodes.TeacherJobHistory);
            int TableId = TJobHistoryId;
            // TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
            string TeacherId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
            DataTable dtJudgment = TeacherJudgmentManager.FindByTableType(int.Parse(TeacherId), TableId, TableType);
            if (dtJudgment.Rows.Count > 0)
            {
                txtJudgeView.Text = dtJudgment.Rows[dtJudgment.Rows.Count-1]["JudgeViewPoint"].ToString();

                int JudgeGrade = int.Parse(dtJudgment.Rows[dtJudgment.Rows.Count-1]["JudgeGrade"].ToString());
                txtGrade.Text = JudgeGrade.ToString();
            }
        }

    }

    private void EnableControls()
    {
        cmbCountry.Enabled = true;
        txtCity.Enabled = true;

        txtDescription.Enabled = true;
        txtEndDate.Enabled = true;
        txtJobName.Enabled = true;
        txtJobPlace.Enabled = true;
        txtStartDate.Enabled = true;
        cmbIsTeaching.Enabled = true;
    }

    private void DeleteTeacherAttachment(int TJobHistoryId)
    {
        // int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
        DataTable dtAttach = attManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherJobHistory), TJobHistoryId);

        if (dtAttach.Rows.Count == 1)
        {
            attManager.ClearBeforeFill = true;
            int AttachId = int.Parse(dtAttach.Rows[0]["AttachId"].ToString());
            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        //  flp.
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
                {

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 547)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }
            }
        }
    }

    private void InsertTeacherAttachment(int RefTableId, TSP.DataManager.AttachmentsManager attManager, TSP.DataManager.TransactionManager TransactionManager)
    {

        if (Session["TeacherLiAttachFileAddress"] != null && !string.IsNullOrEmpty(Session["TeacherLiAttachFileAddress"].ToString()))
        {
            string fileNameImg = "", pathAx = "", extension = "";
            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.TeacherJobHistory;
            dr["RefTable"] = RefTableId;
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDescription.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            extension = Path.GetExtension(Session["TeacherLiAttachFileAddress"].ToString());
            extension = extension.ToLower();

            fileNameImg = System.IO.Path.GetFileName(Session["TeacherLiAttachFileAddress"].ToString());//Utility.GenerateName(Path.GetExtension(Session["TeacherLiAttachFileAddress"].ToString()));
            pathAx = Server.MapPath("~/image/Temp/");
            dr["AtContent"] = DBNull.Value;
            dr["FilePath"] = "~/Image/Employee/Amoozesh/Attachments/" + fileNameImg;


            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + fileNameImg;
                string ImgTarget = Server.MapPath("~/image/Employee/Amoozesh/Attachments/") + fileNameImg;
                File.Copy(ImgSoource, ImgTarget, true);
                btnDeleteAttachment.Visible = true;
                linkAttachment.Text = fileNameImg;
                linkAttachment.Visible = true;
                linkAttachment.NavigateUrl = dr["FilePath"].ToString();
                btnAttachTechearFile.Visible = false;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
        }
    }

    protected void UploaderImage_OnUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveTeacherJobHistoryFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected string SaveTeacherJobHistoryFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Employee/Amoozesh/Attachments/") + ret) == true || System.IO.File.Exists(MapPath("~/Images/Uploaded/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TeacherLiAttachFileAddress"] = tempFileName;
            Session["TeacherLiAttachFileAddressChange"] = 1;
        }
        return ret;
    }

    private void InsertJudgment()
    {
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(TeacherJudgmentManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {

            float SysGrade = -1;
            DataTable dtTGrade = TeachingGradeManager.SelectByInActive(false);
            if (dtTGrade.Rows.Count > 0)
            {
                if (cmbIsTeaching.SelectedIndex == 0)
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeTeaching"].ToString());
                else
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeRelateJob"].ToString());
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل نامشخص بودن حداکثر امتیاز تایید شده توسط سیستم امکان اعمال نظر کارشناسی وجود ندارد.";
                return;
            }

            if (float.Parse(txtGrade.Text) > SysGrade)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امتیاز بایستی کوچکتر یا مساوی" + SysGrade.ToString() + " " + "باشد.";
                return;
            }
            TransactionManager.BeginSave();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
            DataRow JudgmentRow = TeacherJudgmentManager.NewRow();
            string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
            JudgmentRow["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherJobHistory);
            JudgmentRow["TableId"] = int.Parse(TJobHistoryId);
            JudgmentRow["TeId"] = int.Parse(TeId);
            JudgmentRow["JudgeGrade"] = txtGrade.Text;
            JudgmentRow["JudgeViewPoint"] = txtJudgeView.Text;
            JudgmentRow["EmpId"] = EmpId;
            //JudgmentRow["MeetingId"] = "";
            JudgmentRow["UserId"] = Utility.GetCurrentUser_UserId();
            JudgmentRow["ModifiedDate"] = DateTime.Now;

            TeacherJudgmentManager.AddRow(JudgmentRow);
            int cn = TeacherJudgmentManager.Save();
            if (cn > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_TeJobHistoryJudg"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherJudgment;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_TeJobHistoryJudg"] = true;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.EndSave();
            SetError(err);
        }
    }

    private void EditJudgment(int JudgeId)
    {
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

        TeacherJudgmentManager.FindByCode(JudgeId);

        if (TeacherJudgmentManager.Count > 0)
        {
            float SysGrade = -1;
            DataTable dtTGrade = TeachingGradeManager.SelectByInActive(false);
            if (dtTGrade.Rows.Count > 0)
            {
                if (cmbIsTeaching.SelectedIndex == 0)
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeTeaching"].ToString());
                else
                    SysGrade = float.Parse(dtTGrade.Rows[0]["MinGradeRelateJob"].ToString());
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بدلیل نامشخص بودن حداکثر امتیاز تایید شده توسط سیستم امکان اعمال نظر کارشناسی وجود ندارد.";
                return;
            }

            if (float.Parse(txtGrade.Text) > SysGrade)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امتیاز بایستی کوچکتر یا مساوی" + SysGrade.ToString() + " " + "باشد.";
                return;
            }
            TransactionManager.BeginSave();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            TJobHistoryId = Utility.DecryptQS(HiddenFieldTeacherJob["TJobHistoryId"].ToString());
            DataRow JudgmentRow = TeacherJudgmentManager.NewRow();
            string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());

            TeacherJudgmentManager[0].BeginEdit();
            TeacherJudgmentManager[0]["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherJobHistory);
            TeacherJudgmentManager[0]["TeId"] = int.Parse(TeId);
            TeacherJudgmentManager[0]["JudgeGrade"] = txtGrade.Text;
            TeacherJudgmentManager[0]["EmpId"] = EmpId;
            TeacherJudgmentManager[0]["JudgeViewPoint"] = txtJudgeView.Text;
            //JudgmentRow["MeetingId"] = "";
            TeacherJudgmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TeacherJudgmentManager[0]["ModifiedDate"] = DateTime.Now;

            TeacherJudgmentManager[0].EndEdit();

            int cn = TeacherJudgmentManager.Save();
            if (cn > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_TeJobHistoryJudg"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherJudgment;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_TeJobHistoryJudg"] = true;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
            }
            else
            {
                TransactionManager.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }


    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTeacherJob["PageMode"].ToString());
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string TeId = Utility.DecryptQS(HiddenFieldTeacherJob["TeacherId"].ToString());
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        int ComissionGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.ComissionGradingTeacher;
        int CommitteeGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeGradingTeacher;

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);

        TSP.DataManager.WFPermission WFPerComissionGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ComissionGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerCommitteeGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(CommitteeGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);

        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerComissionGrading.BtnEdit || WFPerCommitteeGrading.BtnEdit;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave || WFPerComissionGrading.BtnSave || WFPerCommitteeGrading.BtnSave;
        btnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || WFPerComissionGrading.BtnNew || WFPerCommitteeGrading.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion

    private int FindTeacherCertificate(int TeId)
    {
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeId);
        int TeCertType = -1;
        if (dtTeCert.Rows.Count > 0)
        {
            TeCertType = int.Parse(dtTeCert.Rows[0]["Type"].ToString());
        }
        return TeCertType;


    }

    private void CheckCertificatePermission(int TeId)
    {
        int CertType = FindTeacherCertificate(TeId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldTeacherJob["PageMode"].ToString());
        if (CertType == 1 || CertType == 2)
        {
            switch (PageMode)
            {
                case "View":

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
