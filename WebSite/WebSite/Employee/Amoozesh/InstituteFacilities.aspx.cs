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

public partial class Employee_Amoozesh_InstituteFacilities : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
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
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            GridViewFacility.Visible = per.CanView;

            HiddenFieldFacility["InsId"] = Request.QueryString["InsId"].ToString();
            HiddenFieldFacility["PageMode"] = Request.QueryString["PgMd"].ToString();

            string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
            ObjdsInstitueFacility.SelectParameters[0].DefaultValue = InsId;

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));

            if (InstitueManager.Count > 0)
            {
                RoundPanelFacility.HeaderText ="موسسه: "+ InstitueManager[0]["InsName"].ToString();
            }
            CheckWorkFlowPermission();          
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }

      
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }  

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Institue.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridViewFacility.FocusedRowIndex > -1)
        {
            DataRow FacilityRow = GridViewFacility.GetDataRow(GridViewFacility.FocusedRowIndex);
            if (FacilityRow != null)
            {              
                    string InsFacilityId = FacilityRow["InsFacilityId"].ToString();
                   // DeleteInstitueFacility(int.Parse(InsFacilityId));
                    InActive(int.Parse(InsFacilityId));
                    GridViewFacility.DataBind();              
            }
        }
       
    }

    protected void GridViewFacility_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertInstitueFacility(e);
    }

    protected void GridViewFacility_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditInstitueFacility(e);
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PgMd=" + HiddenFieldFacility["PageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "BasicInfo":
                Response.Redirect("AddInstitues.aspx?InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PageMode=" + HiddenFieldFacility["PageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PgMd=" + HiddenFieldFacility["PageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case"InsTeacher":
                Response.Redirect("InstitueTeachers.aspx?InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PgMd=" + HiddenFieldFacility["PageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
          NextPage("New");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
        int CertType = FindInstitueCertificate(int.Parse(InsId));
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
    protected void GridViewFacility_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.KeyValue != null)
        {
            TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
            InstitueFacilityManager.FindByCode(int.Parse(e.KeyValue.ToString()));
            if (InstitueFacilityManager.Count == 1)
            {
                Boolean IsEquipment = Boolean.Parse(InstitueFacilityManager[0]["IsEquipment"].ToString());
                DevExpress.Web.ASPxLabel lblFacilityType = GridViewFacility.FindRowCellTemplateControl(e.VisibleIndex, null, "lblFacilityType") as DevExpress.Web.ASPxLabel;
                if (lblFacilityType != null)
                {
                    if (IsEquipment)
                    {
                        lblFacilityType.Text = "تجهیزات";
                    }
                    else
                    {
                        lblFacilityType.Text = "فضای آموزشی";
                    }
                }
            }
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int InsFacilityId = -1;
        int focucedIndex = GridViewFacility.FocusedRowIndex;

        if (focucedIndex > -1)
        {            
            DataRow row = GridViewFacility.GetDataRow(focucedIndex);
            InsFacilityId = (int)row["InsFacilityId"];
        }
        if (InsFacilityId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                InsFacilityId = -1;
                Response.Redirect("AddInstituteFacilities.aspx?InsFacilityId=" + Utility.EncryptQS(InsFacilityId.ToString()) + "&InsId=" + HiddenFieldFacility["InsId"] + "&PrePageMode=" + HiddenFieldFacility["PageMode"] + "&PageMode=" + Utility.EncryptQS(Mode) + "&InsCId=" + Request.QueryString["InsCId"]);
            }
            else
            {
                Response.Redirect("AddInstituteFacilities.aspx?InsFacilityId=" + Utility.EncryptQS(InsFacilityId.ToString()) + "&InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PrePageMode=" + HiddenFieldFacility["PageMode"] + "&PageMode=" + Utility.EncryptQS(Mode) + "&InsCId=" + Request.QueryString["InsCId"]);
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

    private void DeleteInstitueFacility(int InsFacilityId)
    {
        try
        {
            TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
            InstitueFacilityManager.FindByCode(InsFacilityId);

            if (InstitueFacilityManager.Count > 0)
            {
                InstitueFacilityManager[0].Delete();

                int cn = InstitueFacilityManager.Save();

                if (cn > 0)
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "حذف انجام شد.";
                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "خطایی در حذف صورت گرفته است.";
                }
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

    private void InsertInstitueFacility(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
        if (LoginManager.Count < 0)
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }
        try
        {
            DataRow InsFasilityRow = InstitueFacilityManager.NewRow();
            string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
            InsFasilityRow["InsId"] = int.Parse(InsId);
            InsFasilityRow["FacilityName"] = e.NewValues["FacilityName"];
            InsFasilityRow["Capacity"] = e.NewValues["Capacity"];
            InsFasilityRow["Equipment"] = e.NewValues["Equipment"];
            InsFasilityRow["Description"] = e.NewValues["Description"];
            InsFasilityRow["UserId"] = Utility.GetCurrentUser_UserId();
            InsFasilityRow["ModifiedDate"] = DateTime.Now;

            InstitueFacilityManager.AddRow(InsFasilityRow);

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
            GridViewFacility.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewFacility.CancelEdit();
            SetError(err);
        }
    }

    private void EditInstitueFacility(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
        if (LoginManager.Count < 0)
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }

        try
        {
           InstitueFacilityManager.FindByCode(int.Parse(e.Keys[0].ToString()));
           if (InstitueFacilityManager.Count == 1)
           {
               InstitueFacilityManager[0].BeginEdit();

               InstitueFacilityManager[0]["FacilityName"] = e.NewValues["FacilityName"];
               InstitueFacilityManager[0]["Capacity"] = e.NewValues["Capacity"];
               InstitueFacilityManager[0]["Equipment"] = e.NewValues["Equipment"];
               InstitueFacilityManager[0]["Description"] = e.NewValues["Description"];
               InstitueFacilityManager[0]["ModifiedDate"] = DateTime.Now;
               InstitueFacilityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

               InstitueFacilityManager[0].EndEdit();

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
           GridViewFacility.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewFacility.CancelEdit();
            SetError(err);
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
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
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
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
                    if (CurrentUserId == userId)
                    {
                        btnNew.Enabled = true;
                        btnNew2.Enabled = true;
                        btnEdit.Enabled = true;
                        btnEdit2.Enabled = true;
                        btnDelete.Enabled = true;
                        btnDelete2.Enabled = true;
                    }
                    else
                    {

                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;                        
                        btnDelete.Enabled = false;
                        btnDelete2.Enabled = false;
                    }
                }
                else
                {
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                }
            }
            else
            {
                btnNew.Enabled = false;
                btnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnDelete.Enabled = false;
                btnDelete2.Enabled = false;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnDelete.Enabled = false;
            btnDelete2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
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

    private void InActive(int InsFacilityId)
    {
        TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
        try
        {
            InstitueFacilityManager.FindByCode(InsFacilityId);
            if (InstitueFacilityManager.Count == 1)
            {
                InstitueFacilityManager[0].BeginEdit();

                InstitueFacilityManager[0]["InActive"] = 1;
                InstitueFacilityManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueFacilityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                InstitueFacilityManager[0].EndEdit();
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
}
