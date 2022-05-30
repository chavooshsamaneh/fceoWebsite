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

public partial class Employee_Amoozesh_TeacherJobHistory : System.Web.UI.Page
{
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
            TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewTeacherJob.Visible = per.CanView;

            MenuTeacherInfo.Enabled = true;
            HiddenFieldTeacher["TeacherId"] = Request.QueryString["TeacherId"].ToString();
            HiddenFieldTeacher["PrePageMode"] = Request.QueryString["PageMode"];
            HiddenFieldTeacher["IsMember"] = true;
            string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());            
            ObjdsTeacherJobHistory.SelectParameters[0].DefaultValue = TeId;

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

            #region Insert WorkFlowState View           
            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            InsertWorkFlowStateView(TableType, int.Parse(TeId));
            #endregion

            CheckWorkFlowPermission();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnJudge"] = btnJudge.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnJudge"] != null)
            this.btnJudge.Enabled = this.btnJudge2.Enabled = (bool)this.ViewState["BtnJudge"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void MenuTeacherInfo_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "ResearchAct":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTeacher["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTeacher["PrePageMode"].ToString());
                break;
            case"BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacher["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTeacher["PrePageMode"].ToString());
                break;
            case"Attach":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + HiddenFieldTeacher["TeacherId"].ToString() + "&PgMd=" + HiddenFieldTeacher["PrePageMode"].ToString());
                break;
            case "licence":
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTeacher["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTeacher["PrePageMode"].ToString());
                break;
            case"Judge":
                NextPage("Judge");
                break;

        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());
        int CertType = FindTeacherCertificate(int.Parse(TeId));
        if (CertType == 0)
        {
            NextPage("Edit");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در وضعیت تمدید و تغییرات پرونده امکان ویرایش اطلاعات وجود ندارد.";
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int TJobHistoryId = -1;
        if (GridViewTeacherJob.FocusedRowIndex > -1)
        {
            DataRow TJobRow = GridViewTeacherJob.GetDataRow(GridViewTeacherJob.FocusedRowIndex);

            TJobHistoryId = int.Parse(TJobRow["TJobHistoryId"].ToString());
           // DeleteTeacherJobHistory(TJobHistoryId);
            InActive(TJobHistoryId);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Teachers.aspx");
    }

    protected void btnJudge_Click(object sender, EventArgs e)
    {
        NextPage("Judge");

    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());
        Session["TeId"] = TeId;
        int TableType = -1;       
        TableType = (int)(TSP.DataManager.TableCodes.TeacherJobHistory);
        Session["TableType"] = TableType;       
    }

    protected void GridViewTeacherJob_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewTeacherJob_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }
  
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int TJobHistoryId = -1;
        int FocucedIndex = GridViewTeacherJob.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            string TeacherId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());
           
            DataRow row = GridViewTeacherJob.GetDataRow(FocucedIndex);
            TJobHistoryId = (int)(row["TJobHistoryId"]);
          
        }
        if (TJobHistoryId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TJobHistoryId = -1;
                Response.Redirect("AddTeacherJobHistory.aspx?TJobHistoryId=" + Utility.EncryptQS(TJobHistoryId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacher["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacher["TeacherId"]);
            }
            else
            {
                Response.Redirect("AddTeacherJobHistory.aspx?TJobHistoryId=" + Utility.EncryptQS(TJobHistoryId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacher["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacher["TeacherId"]);
            }
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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره با موفقیت انجام شد.";
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
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگر تغییر یافته است.";
        }
    }

    private void CheckUserPermission()
    {
        //****Check table permission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnView.Enabled = per.CanView;
        btnView2.Enabled = per.CanView;
        btnDelete.Enabled = per.CanDelete;
        btnDelete2.Enabled = per.CanDelete;
        //GridViewTeacherJob.Visible = per.CanView;

        //btnDelete.Enabled = per.CanDelete;
        //btnDelete2.Enabled = per.CanDelete;
       // btnSave.Enabled = per.CanNew || per.CanEdit;
      //  btnSave2.Enabled = per.CanNew || per.CanEdit;


    }

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());        
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Teachers;

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        int CommitteeGradingCode = (int)TSP.DataManager.WorkFlowTask.CommitteeGradingTeacher;
        int ComissionGradingCode = (int)TSP.DataManager.WorkFlowTask.ComissionGradingTeacher;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPerCommitteeGrading = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(CommitteeGradingCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerComissionGrading = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ComissionGradingCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnJudge"] = btnJudge.Visible = btnJudge2.Visible = (WFPerCommitteeGrading.BtnNew || WFPerComissionGrading.BtnNew);

        this.ViewState["BtnNew"] = btnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
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
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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

    #endregion

    private void InActive(int TJobHistoryId)
    {
        TSP.DataManager.TeacherJobHistoryManager TeacherJobHistoryManager = new TSP.DataManager.TeacherJobHistoryManager();
        try
        {
            TeacherJobHistoryManager.FindByCode(TJobHistoryId);
            if (TeacherJobHistoryManager.Count == 1)
            {
                TeacherJobHistoryManager[0].BeginEdit();

                TeacherJobHistoryManager[0]["InActive"] = 1;
                TeacherJobHistoryManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherJobHistoryManager[0]["ModifiedDate"] = DateTime.Now;                

                TeacherJobHistoryManager[0].EndEdit();
                int cn = TeacherJobHistoryManager.Save();
                if (cn > 0)
                {
                    GridViewTeacherJob.DataBind();
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسطکاربر دیگر تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetError(err);
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
    #endregion
    
}
