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

public partial class Employee_Amoozesh_TeacherResearchAct : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeacherId"]))
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            MenuTeacherInfo.Enabled = true;

            TSP.DataManager.Permission per = TSP.DataManager.TeacherResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;        
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewTeacherResearch.Visible = per.CanView;

            HiddenFieldTeacherResearchAct["TeacherId"] = Request.QueryString["TeacherId"].ToString();
            HiddenFieldTeacherResearchAct["PageMode"] = Request.QueryString["PageMode"].ToString();
            HiddenFieldTeacherResearchAct["TeacherId"] = Request.QueryString["TeacherId"].ToString();
            HiddenFieldTeacherResearchAct["PrePageMode"] = Request.QueryString["PageMode"];
            HiddenFieldTeacherResearchAct["IsMember"] = true;

            string TeacherId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
            TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
            TeacherManager.FindByCode(int.Parse(TeacherId));
            if (TeacherManager.Count > 0)
            {
                RoundPanelTeacherResearch.HeaderText = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
            }
            else
            {
                Response.Redirect("Teachers.aspx");
                return;
            }

            #region Insert WorkFlowState View
            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            InsertWorkFlowStateView(TableType, int.Parse(TeacherId));
            #endregion
            CheckWorkFlowPermission();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnJudge"] = btnJudge.Enabled;

        }
        SetObjectDataSource();
              
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnJudge"] != null)
            this.btnJudge.Enabled = this.btnJudge2.Enabled = (bool)this.ViewState["BtnJudge"];
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DataRow TResearchRow = GridViewTeacherResearch.GetDataRow(GridViewTeacherResearch.FocusedRowIndex);
        int TResearchId = int.Parse(TResearchRow["TResearchId"].ToString());
        //DeleteTeacherResearch(TResearchId);
        InActive(TResearchId);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherResearchAct["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTeacherResearchAct["PageMode"].ToString());
    }

    protected void btnJudge_Click(object sender, EventArgs e)
    {
        NextPage("Judge");
    }

    protected void GridViewTeacherResearch_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertTeacherResearch(e);
    }

    protected void GridViewTeacherResearch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditTeacherResearch(e);
    }

    protected void MenuTeacherInfo_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "licence":              
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTeacherResearchAct["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTeacherResearchAct["PageMode"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherResearchAct["TeacherId"] + "&PageMode=" + HiddenFieldTeacherResearchAct["PageMode"]);
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTeacherResearchAct["TeacherId"] + "&PageMode=" + HiddenFieldTeacherResearchAct["PageMode"]);
                break;          
            case "Attachment":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + HiddenFieldTeacherResearchAct["TeacherId"] + "&PgMd=" + HiddenFieldTeacherResearchAct["PageMode"]);
                break;          
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
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

    protected void GridViewTeacherResearch_BeforePerformDataSelect(object sender, EventArgs e)
    {
     
    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TResearchId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
        Session["TeId"] = TeId;
        int TableType = -1;
        if (IsTeacherMember())
        {
            TableType = (int)(TSP.DataManager.TableCodes.MemberResearchActivity);
            Session["TableType"] = TableType;
        }
        else
        {
            TableType = (int)(TSP.DataManager.TableCodes.TeacherResearchActivity);
            Session["TableType"] = TableType;
        }
    }

    protected void GridViewTeacherResearch_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ResearchDate" )
            e.Cell.Style["direction"] = "ltr";


    }

    protected void GridViewTeacherResearch_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ResearchDate")
            e.Editor.Style["direction"] = "ltr";
    }

    #endregion

    #region Methods
    private void InsertTeacherResearch(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();       
        try
        {
            DataRow ResearchRow = TeacherResearchActivityManager.NewRow();

            DevExpress.Web.ASPxComboBox cmbResearchAct = (DevExpress.Web.ASPxComboBox)GridViewTeacherResearch.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewTeacherResearch.Columns["ResearchType"], "cmbResearchActivity");
            ResearchRow["RaId"] = int.Parse(cmbResearchAct.SelectedItem.Value.ToString());
            string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
            ResearchRow["TeId"] = int.Parse(TeId);
            ResearchRow["ResearchName"] = e.NewValues["Name"];
            PersianDateControls.PersianDateTextBox PersianDateTextBox = (PersianDateControls.PersianDateTextBox)GridViewTeacherResearch.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewTeacherResearch.Columns["ResearchDate"], "txtResearchDate");
            ResearchRow["ResearchDate"] = PersianDateTextBox.Text;
            ResearchRow["Description"] = e.NewValues["Description"];
            ResearchRow["UserId"] = Utility.GetCurrentUser_UserId();
            ResearchRow["ModifiedDate"] = DateTime.Now;

            TeacherResearchActivityManager.AddRow(ResearchRow);

            int cn = TeacherResearchActivityManager.Save();
            if (cn > 0)
            {
                GridViewTeacherResearch.DataBind();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

            GridViewTeacherResearch.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewTeacherResearch.CancelEdit();

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

    private void EditTeacherResearch(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();       
        try
        {
            TeacherResearchActivityManager.Fill();
            DataRow TResearchRow = TeacherResearchActivityManager.DataTable.Rows.Find(e.Keys[0]);
            if (TResearchRow != null)
            {
                TResearchRow.BeginEdit();

                DevExpress.Web.ASPxComboBox cmbResearchAct = (DevExpress.Web.ASPxComboBox)GridViewTeacherResearch.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewTeacherResearch.Columns["ResearchType"], "cmbResearchActivity");
                TResearchRow["RaId"] = int.Parse(cmbResearchAct.SelectedItem.Value.ToString());
                string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
                TResearchRow["TeId"] = int.Parse(TeId);
                TResearchRow["ResearchName"] = e.NewValues["ResearchName"];
                PersianDateControls.PersianDateTextBox PersianDateTextBox = (PersianDateControls.PersianDateTextBox)GridViewTeacherResearch.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewTeacherResearch.Columns["ResearchDate"], "txtResearchDate");
                TResearchRow["ResearchDate"] = PersianDateTextBox.Text;
                TResearchRow["Description"] = e.NewValues["Description"];
                TResearchRow["UserId"] = Utility.GetCurrentUser_UserId();
                TResearchRow["ModifiedDate"] = DateTime.Now;

                TResearchRow.EndEdit();

                int cn = TeacherResearchActivityManager.Save();

                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

                GridViewTeacherResearch.CancelEdit();

            }
        }
        catch (Exception err)
        {
            GridViewTeacherResearch.CancelEdit();

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

    private void DeleteTeacherResearch(int TResearchId)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();

        try
        {
            TeacherResearchActivityManager.FindByCode(TResearchId);
            if (TeacherResearchActivityManager.Count > 0)
            {
                TeacherResearchActivityManager[0].Delete();

                int cn = TeacherResearchActivityManager.Save();

                if (cn > 0)
                {
                    GridViewTeacherResearch.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
            GridViewTeacherResearch.CancelEdit();

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

    private void SetObjectDataSource()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeId));

        if (TeacherManager.Count > 0)
        {
            if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            {
                ObjdsMemberResearchActivity.DataBind();
                GridViewTeacherResearch.DataSource = ObjdsMemberResearchActivity;
                GridViewTeacherResearch.KeyFieldName = "MraId";               
                ObjdsMemberResearchActivity.SelectParameters[0].DefaultValue = TeacherManager[0]["MeId"].ToString();
                GridViewTeacherResearch.DataBind();
                btnDelete.Enabled = false;
                btnDelete2.Enabled = false;
                btnNew.Enabled = false;
                btnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                HiddenFieldTeacherResearchAct["IsMember"] = true;

               
            }
            else
            {
                ObjdsTeacherResearch.DataBind();
                GridViewTeacherResearch.DataSource = ObjdsTeacherResearch;
                GridViewTeacherResearch.KeyFieldName = "TResearchId";
                ObjdsTeacherResearch.SelectParameters[0].DefaultValue = TeId;
                GridViewTeacherResearch.DataBind();
                HiddenFieldTeacherResearchAct["IsMember"] = false;
            }
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }
    }

    private void NextPage(string Mode)
    {
        SetObjectDataSource();
        int TResearchId = -1;
        int FocucedIndex = GridViewTeacherResearch.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            string TeacherId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());

            DataRow row = GridViewTeacherResearch.GetDataRow(FocucedIndex);
            TResearchId = (int)(row["TResearchId"]);

        }
        if (TResearchId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TResearchId = -1;
                Response.Redirect("AddTeacherResearches.aspx?TResearchId=" + Utility.EncryptQS(TResearchId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherResearchAct["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacherResearchAct["TeacherId"]);
            }
            else
            {
                Response.Redirect("AddTeacherResearches.aspx?TResearchId=" + Utility.EncryptQS(TResearchId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherResearchAct["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacherResearchAct["TeacherId"]);
            }
        }
    }

    private Boolean IsTeacherMember()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeId));

        if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            return true;
        else
            return false;
    }

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string TeId = Utility.DecryptQS(HiddenFieldTeacherResearchAct["TeacherId"].ToString());        
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

    private void InActive(int TResearchId)
    {
        TSP.DataManager.TeacherResearchActivityManager TeacherResearchActivityManager = new TSP.DataManager.TeacherResearchActivityManager();
        try
        {
            TeacherResearchActivityManager.FindByCode(TResearchId);
            if (TeacherResearchActivityManager.Count == 1)
            {
                TeacherResearchActivityManager[0].BeginEdit();

                TeacherResearchActivityManager[0]["InActive"] = 1;
                TeacherResearchActivityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherResearchActivityManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherResearchActivityManager[0].EndEdit();
                int cn = TeacherResearchActivityManager.Save();
                if (cn > 0)
                {
                    GridViewTeacherResearch.DataBind();
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
}
