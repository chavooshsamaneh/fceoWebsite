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

public partial class Employee_Amoozesh_TeachersLicence : System.Web.UI.Page
{
    #region Private Members

    #endregion

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
            TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            GridViewTeacherLicence.Visible = per.CanView;

            MenuTeacherInfo.Enabled = true;

            Session["TachearLicenceObjdss"] = null;

            HiddenFieldTeacherLicnce["TeacherId"] = Request.QueryString["TeacherId"].ToString();
            HiddenFieldTeacherLicnce["PrePageMode"] = Request.QueryString["PageMode"];
            HiddenFieldTeacherLicnce["IsMember"] = true;
            string TeId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
            SetObjectDataSource(int.Parse(TeId));

            #region Insert WorkFlowState View
            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            InsertWorkFlowStateView(TableType, int.Parse(TeId));
            #endregion

            CheckWorkFlowPermission();



            if (IsTeacherMember())
            {
                btnNew.Enabled = false;
                BtnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnDelete.Enabled = false;
                btnDelete2.Enabled = false;
            }


            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnJudge"] = btnJudge.Visible;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
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
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherLicnce["TeacherId"] + "&PageMode=" + HiddenFieldTeacherLicnce["PrePageMode"]);
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTeacherLicnce["TeacherId"] + "&PageMode=" + HiddenFieldTeacherLicnce["PrePageMode"]);
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTeacherLicnce["TeacherId"] + "&PageMode=" + HiddenFieldTeacherLicnce["PrePageMode"]);
                break;
            case "Attachment":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + HiddenFieldTeacherLicnce["TeacherId"] + "&PgMd=" + HiddenFieldTeacherLicnce["PrePageMode"]);
                break;
            case "Judge":
                NextPage("Judge");
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherLicnce["TeacherId"] + "&PageMode=" + HiddenFieldTeacherLicnce["PrePageMode"]);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TeId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
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
        int TeLiId = -1;

        if (GridViewTeacherLicence.FocusedRowIndex > -1)
        {
            int Index = GridViewTeacherLicence.FocusedRowIndex;
            GridViewTeacherLicence.DataSource = (ObjectDataSource)Session["TachearLicenceObjdss"];
            GridViewTeacherLicence.DataBind();
            DataRow row = GridViewTeacherLicence.GetDataRow(Index);
            TeLiId = (int)row["TLiId"];

            InActive(TeLiId);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnJudge_Click(object sender, EventArgs e)
    {
        NextPage("Judge");

    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
        Session["TeId"] = TeId;
        int TableType = -1;
        if (IsTeacherMember())
        {
            TableType = (int)(TSP.DataManager.TableCodes.MemberLicence);
            Session["TableType"] = TableType;

        }
        else
        {
            TableType = (int)(TSP.DataManager.TableCodes.TeacherLicence);
            Session["TableType"] = TableType;
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int TeLiId = -1;
        int focucedIndex = GridViewTeacherLicence.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            string TeacherId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
            SetObjectDataSource(int.Parse(TeacherId));
            DataRow row = GridViewTeacherLicence.GetDataRow(focucedIndex);
            if (!Boolean.Parse(HiddenFieldTeacherLicnce["IsMember"].ToString()))
                TeLiId = (int)row["TLiId"];
            else
                TeLiId = (int)row["MLId"];

        }
        if (TeLiId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TeLiId = -1;
                Response.Redirect("AddTeacherLicence.aspx?TeLiId=" + Utility.EncryptQS(TeLiId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherLicnce["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacherLicnce["TeacherId"]);
            }
            else
            {
                Response.Redirect("AddTeacherLicence.aspx?TeLiId=" + Utility.EncryptQS(TeLiId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldTeacherLicnce["PrePageMode"] + "&TeacherId=" + HiddenFieldTeacherLicnce["TeacherId"]);
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

    private void DeleteTecherLicence(int TeLiId)
    {
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();

        DataRow TeLiRow = GridViewTeacherLicence.GetDataRow(GridViewTeacherLicence.FocusedRowIndex);

        if (TeLiRow != null)
        {
            TeacherLicenceManger.FindByCode(int.Parse(TeLiRow["TLiId"].ToString()));

            if (TeacherLicenceManger.Count > 0)
            {
                TeacherLicenceManger[0].Delete();
                int cn = TeacherLicenceManger.Save();

                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف با موفقیت انجام شد.";
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
    }

    private void SetObjectDataSource(int TeacherId)
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherManager.FindByCode(TeacherId);
        if (TeacherManager.Count > 0)
        {
            string TeacherName = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
            RoundPanelTeacherLicence.HeaderText = TeacherName;
            if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            {
                HiddenFieldTeacherLicnce["IsMember"] = true;
                GridViewTeacherLicence.DataSource = ObjdsMemberLicence;
                Session["TachearLicenceObjdss"] = ObjdsMemberLicence;
                GridViewTeacherLicence.KeyFieldName = "MlId";
                ObjdsMemberLicence.DataBind();
                ObjdsMemberLicence.SelectParameters[0].DefaultValue = TeacherManager[0]["MeId"].ToString();
                GridViewTeacherLicence.DataBind();
                btnDelete.Enabled = false;
                btnDelete2.Enabled = false;
                btnNew.Enabled = false;
                BtnNew2.Enabled = false;
                //btnView.ClientEnabled = false;
                //btnView2.ClientEnabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnDelete"] = btnDelete.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;

            }
            else
            {
                HiddenFieldTeacherLicnce["IsMember"] = false;
                GridViewTeacherLicence.DataSource = ObjdsTeacherLicence;
                GridViewTeacherLicence.KeyFieldName = "TLiId";
                ObjdsTeacherLicence.DataBind();
                Session["TachearLicenceObjdss"] = ObjdsTeacherLicence;
                ObjdsTeacherLicence.SelectParameters[0].DefaultValue = TeacherId.ToString();
                GridViewTeacherLicence.DataBind();
            }
        }
        else
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
    }

    private Boolean IsTeacherMember()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
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
        string TeId = Utility.DecryptQS(HiddenFieldTeacherLicnce["TeacherId"].ToString());
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

        this.ViewState["BtnNew"] = btnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew;
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
    private void InActive(int TLiId)
    {
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
        try
        {
            TeacherLicenceManger.FindByCode(TLiId);
            if (TeacherLicenceManger.Count == 1)
            {
                TeacherLicenceManger[0].BeginEdit();

                TeacherLicenceManger[0]["InActive"] = 1;
                TeacherLicenceManger[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherLicenceManger[0]["ModifiedDate"] = DateTime.Now;

                TeacherLicenceManger[0].EndEdit();
                int cn = TeacherLicenceManger.Save();
                if (cn > 0)
                {
                    GridViewTeacherLicence.DataBind();
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
