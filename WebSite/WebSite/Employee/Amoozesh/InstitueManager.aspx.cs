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

public partial class Employee_Amoozesh_InstitueManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
        {
            Response.Redirect("Institue.aspx");
            return;
        }
        
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            GridViewInsManager.Visible = per.CanView;
           // MenuTeacherInfo.Enabled = true;

            HiddenFieldInsManager["InsId"] = Request.QueryString["InsId"].ToString();
            HiddenFieldInsManager["PrePageMode"] = Request.QueryString["PgMd"];
           // HiddenFieldTeacherLicnce["IsMember"] = true;
            string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
            ObjdsInsManager.SelectParameters[0].DefaultValue = InsId;
            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));
            if (InstitueManager.Count > 0)
            {
                RoundPanelInsManager.HeaderText ="موسسه: "+ InstitueManager[0]["InsName"].ToString();
            }
            
            CheckWorkFlowPermission();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;


        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
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
        DataRow InsMngRow = GridViewInsManager.GetDataRow(GridViewInsManager.FocusedRowIndex);
        int InsMngId= int.Parse(InsMngRow["InsMngId"].ToString());
       // DeleteInsManager(InsMngId);
        Inactive(InsMngId);
        
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
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
    
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {        
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsId=" + HiddenFieldInsManager["InsId"].ToString() + "&PgMd=" + HiddenFieldInsManager["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "BasicInfo":
                Response.Redirect("AddInstitues.aspx?InsId=" + HiddenFieldInsManager["InsId"].ToString() + "&PageMode=" + HiddenFieldInsManager["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsId=" + HiddenFieldInsManager["InsId"].ToString() + "&PgMd=" + HiddenFieldInsManager["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case"InsTeacher":
                Response.Redirect("InstitueTeachers.aspx?InsId=" + HiddenFieldInsManager["InsId"].ToString() + "&PgMd=" + HiddenFieldInsManager["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
        }
    }

    #region Methods

    private void NextPage(string Mode)
    {
        int InsMngId = -1;
        int focucedIndex = GridViewInsManager.FocusedRowIndex;

        if (focucedIndex > -1)
        {

            DataRow row = GridViewInsManager.GetDataRow(focucedIndex);

            InsMngId = (int)row["InsMngId"];
        }
        if (InsMngId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                InsMngId = -1;
                Response.Redirect("AddInstitueManager.aspx?InsMngId=" + Utility.EncryptQS(InsMngId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldInsManager["PrePageMode"] + "&InsId=" + HiddenFieldInsManager["InsId"] + "&InsCId=" + Request.QueryString["InsCId"]);
            }
            else
            {
                Response.Redirect("AddInstitueManager.aspx?InsMngId=" + Utility.EncryptQS(InsMngId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&PrePageMode=" + HiddenFieldInsManager["PrePageMode"] + "&InsId=" + HiddenFieldInsManager["InsId"] + "&InsCId=" + Request.QueryString["InsCId"]);
            }
        }
    }

    private void DeleteInsManager(int InsMngId)
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();
        InstitueManagerManager.FindByCode(InsMngId);
        if (InstitueManagerManager.Count > 0)
        {
            InstitueManagerManager[0].Delete();
            int cn= InstitueManagerManager.Save();
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
        string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
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
                        btnView.Enabled = true;
                        btnView2.Enabled = true;
                        btnDelete.Enabled = true;
                        btnDelete2.Enabled = true;
                    }
                    else
                    {

                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                   //     btnView.Enabled = false;
                    //    btnView2.Enabled = false;
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
                 //   btnView.Enabled = false;
                 //   btnView2.Enabled = false;
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
             //   btnView.Enabled = false;
             //   btnView2.Enabled = false;
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
          //  btnView.Enabled = false;
           // btnView2.Enabled = false;
            btnDelete.Enabled = false;
            btnDelete2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnView"] = btnView.Enabled;
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

    private void Inactive(int InsMngId)
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();

        try
        {
            InstitueManagerManager.FindByCode(InsMngId);
            if (InstitueManagerManager.Count == 1)
            {
                InstitueManagerManager[0].BeginEdit();

                InstitueManagerManager[0]["InActive"] = 1;
                InstitueManagerManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueManagerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                InstitueManagerManager[0].EndEdit();

                int cn = InstitueManagerManager.Save();
                if (cn > 0)
                {
                    GridViewInsManager.DataBind();
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگر تغییر یافته است.";
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
