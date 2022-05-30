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
using DevExpress.Web;

public partial class Employee_Amoozesh_TeacherCourse : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["TeId"]))
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
        if (!IsPostBack)
        {
            HiddenFieldTeacherCourse["TeId"] = Request.QueryString["TeId"];
            HiddenFieldTeacherCourse["PrePageMode"] = Request.QueryString["PrePgMd"];
            HiddenFieldTeacherCourse["PageMode"] = Request.QueryString["PgMd"];
            TSP.DataManager.Permission per = TSP.DataManager.TeachersCourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            GridViewTeacherCourse.Visible = per.CanView;

            int TeId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString()));
         //   CheckCertificatePermission(TeId);


            CheckWorkFlowPermissionForSave();


            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        }      
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive.Enabled = (bool)this.ViewState["BtnDisActive"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Teachers.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void GridViewTeacherCourse_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertTeacherCourse(e);
    }

    protected void GridViewTeacherCourse_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditTeacherCourse(e);
    }

    protected void MenuTeacherInfo_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "ResearchAct":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTeacherCourse["TeId"].ToString() + "&Pagemode=" + HiddenFieldTeacherCourse["PrePageMode"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherCourse["TeId"].ToString() + "&PageMode=" + HiddenFieldTeacherCourse["PrePageMode"].ToString());
                break;
            case "Attach":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + HiddenFieldTeacherCourse["TeId"].ToString() + "&PgMd=" + HiddenFieldTeacherCourse["PrePageMode"].ToString());
                break;
            case "licence":
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTeacherCourse["TeId"].ToString() + "&Pagemode=" + HiddenFieldTeacherCourse["PrePageMode"].ToString());
                break;
            case "Judge":
                if (GridViewTeacherCourse.FocusedRowIndex > -1)
                {
                    TSP.DataManager.TeacherCourseJudgmentManager TeacherCourseJudgmentManager = new TSP.DataManager.TeacherCourseJudgmentManager();
                    DataRow TeacherCourseRow = GridViewTeacherCourse.GetDataRow(GridViewTeacherCourse.FocusedRowIndex);
                    int TCrsJId = -1;
                    int TCrsId = int.Parse(TeacherCourseRow["TCrsId"].ToString());
                    int TableType = (int)TSP.DataManager.TableCodes.TeacherCourseJudgment;
                    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
                    LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                    int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                    DataTable dtTCourseJudgment = TeacherCourseJudgmentManager.SelectByEmpId(TCrsId, EmpId, TableType);
                    if (dtTCourseJudgment.Rows.Count == 1)
                    {
                        TCrsJId = int.Parse(dtTCourseJudgment.Rows[0]["TCrsJId"].ToString());
                        Response.Redirect("TeacherCourseJudgment.aspx?TCrsJId=" + Utility.EncryptQS(TCrsJId.ToString()) + "&TCrsId=" + Utility.EncryptQS(TCrsId.ToString()) + "&PgMd=" + Utility.EncryptQS("Edit"));
                    }
                    if (dtTCourseJudgment.Rows.Count == 0)
                    {
                        Response.Redirect("TeacherCourseJudgment.aspx?TCrsJId=" + Utility.EncryptQS(TCrsJId.ToString()) + "&TCrsId=" + Utility.EncryptQS(TCrsId.ToString()) + "&PgMd=" + Utility.EncryptQS("New"));
                    }
                }
                break;
        }
    }

    protected void GridViewTeacherCourseJudgment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        Session["TableType"] = (int)TSP.DataManager.TableCodes.TeacherCourseJudgment;
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void GridViewTeacherCourse_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString());
        DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(int.Parse(TeId));

        if (dtTeCert.Rows.Count <= 0)
        {
            e.RowError = "خطایی در ذخیره انجام گرفته است.";
        }
    }

    #endregion

    #region Methods
    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "New":
                SetNewMode();
                break;
            case "Changes":
                SetChangesMode();
                break;
        }
    }

    private void SetNewMode()
    {
        btnDisActive.Enabled = false;
        btnDisActive2.Enabled = false;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString());
        ObjdsTeacherCourse.SelectParameters[0].DefaultValue = TeId;
    }

    private void SetChangesMode()
    {
        string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString());
        ObjdsTeacherCourse.SelectParameters[0].DefaultValue = TeId;
        ObjdsTeacherCourse.SelectParameters[0].DefaultValue = "1";
    }

    private void NextPage(string Mode)
    {
        int TCrsId = -1;
        int focucedIndex = GridViewTeacherCourse.FocusedRowIndex;

        if (focucedIndex > -1)
        {

            DataRow row = GridViewTeacherCourse.GetDataRow(focucedIndex);

            TCrsId = (int)row["InsId"];
        }
        if (TCrsId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TCrsId = -1;
                Response.Redirect("AddTeacherCourse.aspx?TCrsId=" + Utility.EncryptQS(TCrsId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherCourse["PrePageMode"] + "&TeId=" + HiddenFieldTeacherCourse["TeId"]);
            }
            else
            {
                Response.Redirect("AddTeacherCourse.aspx?TCrsId=" + Utility.EncryptQS(TCrsId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherCourse["PrePageMode"] + "&TeId=" + HiddenFieldTeacherCourse["TeId"]);
            }
        }
    }

    private void InsertTeacherCourse(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
            TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

            DataRow TCourseRow = TeachersCourseManager.NewRow();
            if ((HiddenFieldTeacherCourse["TeId"] == null) && (string.IsNullOrEmpty(HiddenFieldTeacherCourse["TeId"].ToString())))
            {
                Response.Redirect("Teachers.aspx");
            }
            string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString());
            DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(int.Parse(TeId));
            if (dtTeCert.Rows.Count > 0)
            {
                int TeCId = int.Parse(dtTeCert.Rows[0]["TcId"].ToString());
                TCourseRow["TcId"] = TeCId;
            }
            TCourseRow["TeId"] = int.Parse(TeId);
            TCourseRow["CrsId"] = e.NewValues["CrsId"];
            TCourseRow["RequestDate"] = Utility.GetDateOfToday();
            TCourseRow["Type"] = 0;
            TCourseRow["IsConfirmed"] = 0;
            TCourseRow["Description"] = e.NewValues["CrsId"];
            TCourseRow["InActive"] = 0;
            TCourseRow["UserId"] =Utility.GetCurrentUser_UserId();
            TCourseRow["ModifiedDate"] = DateTime.Now;

            TeachersCourseManager.AddRow(TCourseRow);
            int cn = TeachersCourseManager.Save();
            if (cn > 0)
            {
                GridViewTeacherCourse.DataBind();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                GridViewTeacherCourse.CancelEdit();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
            }
            GridViewTeacherCourse.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewTeacherCourse.CancelEdit();

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

    private void EditTeacherCourse(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        
    }

    private void InActive(int TCrsId)
    {
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        try
        {
            TeachersCourseManager.FindByCode(TCrsId);
            if (TeachersCourseManager.Count == 1)
            {
                TeachersCourseManager[0].BeginEdit();

                TeachersCourseManager[0]["InActive"] =1;
                TeachersCourseManager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                TeachersCourseManager[0]["ModifiedDate"] = DateTime.Now;

                TeachersCourseManager[0].EndEdit();

                int cn = TeachersCourseManager.Save();
                if (cn > 0)
                {
                    GridViewTeacherCourse.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام گرفت.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {

        }
    }

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
            this.LabelWarning.Text = "وضعیت پرونده استاد انتخاب شده نامشخص است.";
            return;
        }
        if (CertType == 1 || CertType == 2)
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
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
        string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeacherId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Teachers;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();


        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(TeId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
        {
            int SaveTeacherWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveTeacherWorkCode);
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
                    int CurrentUserId = Utility.GetCurrentUser_UserId();
                    //  string PageMode = Utility.DecryptQS(HiddenFieldTeacher["PageMode"].ToString());
                    if (CurrentUserId == userId)
                    {
                        btnNew.Enabled = true;
                        btnNew2.Enabled = true;

                      //  btnDelete.Enabled = true;
                      //  btnDelete2.Enabled = true;
                    }
                    else
                    {

                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;

                       // btnDelete.Enabled = false;
                       // btnDelete2.Enabled = false;
                    }
                }
                else
                {
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;

                  //  btnDelete.Enabled = false;
                  //  btnDelete2.Enabled = false;
                }
            }
            else
            {
                btnNew.Enabled = false;
                btnNew2.Enabled = false;

              //  btnDelete.Enabled = false;
               // btnDelete2.Enabled = false;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;

          //  btnDelete.Enabled = false;
          //  btnDelete2.Enabled = false;
        }

       // this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion       
}
