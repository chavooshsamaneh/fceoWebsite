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

public partial class Employee_Amoozesh_InstitueActivity : System.Web.UI.Page
{
    //int _InsCId
    //{
    //    get
    //    {
    //        return Convert.ToInt32(HiddenFieldInsActivity["InsCId"]);
    //    }
    //    set
    //    {
    //        HiddenFieldInsActivity["InsCId"] = value;
    //    }
    //}

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

            HiddenFieldInsActivity["InsId"] = Request.QueryString["InsId"].ToString();
            string InsId = Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString());
            HiddenFieldInsActivity["PrePageMode"] = Request.QueryString["PgMd"].ToString();
            ObjdsInsActivity.SelectParameters[0].DefaultValue = InsId;

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));
            if (InstitueManager.Count > 0)
            {
                RoundPanelInsActivity.HeaderText = "مؤسسه: "+InstitueManager[0]["InsName"].ToString();
              //  _InsCId = Convert.ToInt32(InstitueManager[0]["InsCId"]);
            }
            else
            {
                Response.Redirect("Institue.aspx");
                return;
            }
            
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.InstitueActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewInsActivity.Visible = per.CanView;

            CheckWorkFlowPermission();

            //CheckCertificatePermission(int.Parse(InsId));

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
       
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void GridViewInsActivity_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertInsActivity(e);
    }

    protected void GridViewInsActivity_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditInsActivity(e);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Institue.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int InsActId = -1;
        DataRow InsActRow = GridViewInsActivity.GetDataRow(GridViewInsActivity.FocusedRowIndex);
        InsActId = int.Parse(InsActRow["InsActId"].ToString());
        //DeleteInsActivity(InsActId);

        Inactive(InsActId);
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsId=" + HiddenFieldInsActivity["InsId"].ToString() + "&PgMd=" + HiddenFieldInsActivity["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "MainInfo":
                Response.Redirect("AddInstitues.aspx?InsId=" + HiddenFieldInsActivity["InsId"].ToString() + "&PageMode=" + HiddenFieldInsActivity["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsId=" + HiddenFieldInsActivity["InsId"].ToString() + "&PgMd=" + HiddenFieldInsActivity["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case"InsTeacher":
                Response.Redirect("InstitueTeachers.aspx?InsId=" + HiddenFieldInsActivity["InsId"].ToString() + "&PgMd=" + HiddenFieldInsActivity["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
        }
    }

    protected void GridViewInsActivity_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        string InsId = Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString());
        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(int.Parse(InsId),0);

        if (dtInsCert.Rows.Count <= 0)
        {
            e.RowError = "خطایی در ذخیره انجام گرفته است.";
        }
    }
    #endregion

    #region Method
    private void InsertInsActivity(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.InstitueActivityManager InstitueActivityManager = new TSP.DataManager.InstitueActivityManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        try
        {            
            DataRow InsActRow = InstitueActivityManager.NewRow();
            string InsId=Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString());
            DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(int.Parse(InsId),0);
            if (dtInsCert.Rows.Count > 0)
            {
                int InsCId = int.Parse(dtInsCert.Rows[0]["InsCId"].ToString());
                InsActRow["InsCId"] = InsCId;
            }
            InsActRow["InsId"] = int.Parse(InsId);
            InsActRow["InsActName"] = e.NewValues["InsActName"].ToString();           
            InsActRow["Description"] = e.NewValues["Description"];
            InsActRow["UserId"] = Utility.GetCurrentUser_UserId();
            InsActRow["ModifiedDate"] = DateTime.Now;

            InstitueActivityManager.AddRow(InsActRow);

            int cn = InstitueActivityManager.Save();
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
            GridViewInsActivity.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewInsActivity.CancelEdit();

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

    private void EditInsActivity(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.InstitueActivityManager InstitueActivityManager = new TSP.DataManager.InstitueActivityManager();
        try
        {
            InstitueActivityManager.FindByCode(int.Parse(e.Keys[0].ToString()));
            InstitueActivityManager[0].BeginEdit();
            InstitueActivityManager[0]["InsId"] = int.Parse(Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString()));
            InstitueActivityManager[0]["InsActName"] = e.NewValues["InsActName"].ToString();
            if(e.NewValues["Description"]==null)
                InstitueActivityManager[0]["Description"] ="";
            else
                 InstitueActivityManager[0]["Description"] = e.NewValues["Description"].ToString();
             InstitueActivityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            InstitueActivityManager[0]["ModifiedDate"] = DateTime.Now;

            InstitueActivityManager[0].EndEdit();

            int cn = InstitueActivityManager.Save();
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
            GridViewInsActivity.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewInsActivity.CancelEdit();

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

    private void DeleteInsActivity(int InsActId)
    {
        try
        {
            TSP.DataManager.InstitueActivityManager InstitueActivityManager = new TSP.DataManager.InstitueActivityManager();
            InstitueActivityManager.FindByCode(InsActId);
            if (InstitueActivityManager.Count > 0)
            {
                InstitueActivityManager[0].Delete();
                int cn = InstitueActivityManager.Save();
                if (cn > 0)
                {
                    GridViewInsActivity.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
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
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }

    }

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string InsId = Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString());       
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Institue;       

        int WFCode = (int)TSP.DataManager.WorkFlows.InstitueConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;        

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(InsId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        this.ViewState["BtnDelete"] = btnDelete.Enabled=btnDelete2.Enabled=WFPer.BtnInactive;
        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
    }
    #endregion

    //private void CheckWorkFlowPermission()
    //{
    //    CheckWorkFlowPermissionForSave();
    //}

    //private void CheckWorkFlowPermissionForSave()
    //{
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int CurrentTaskOrder = -1;
    //    int TaskOrder = -1;
    //    //****TableId
    //    string InsId = Utility.DecryptQS(HiddenFieldInsActivity["InsId"].ToString());
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.Institue;

    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(InsId));
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //    }
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }

    //    if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
    //    {
    //        WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        TaskDoerManager.FindByTaskId(TaskId);

    //        if (TaskDoerManager.Count > 0)
    //        {
    //            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
    //            NezamMemberChartManager.FindByNcId(NcId);

    //            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

    //            LoginManager.FindByMeIdUltId(EmpId, 4);
    //            if (LoginManager.Count > 0)
    //            {
    //                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
    //                int CurrentUserId = Utility.GetCurrentUser_UserId();
    //                if (CurrentUserId == userId)
    //                {
    //                    BtnNew.Enabled = true;
    //                    btnNew2.Enabled = true;
    //                    btnEdit.Enabled = true;
    //                    btnEdit2.Enabled = true;                      
    //                    btnDelete.Enabled = true;
    //                    btnDelete2.Enabled = true;
    //                }
    //                else
    //                {

    //                    BtnNew.Enabled = false;
    //                    btnNew2.Enabled = false;
    //                    btnEdit.Enabled = false;
    //                    btnEdit2.Enabled = false;                       
    //                    btnDelete.Enabled = false;
    //                    btnDelete2.Enabled = false;
    //                }
    //            }
    //            else
    //            {
    //                BtnNew.Enabled = false;
    //                btnNew2.Enabled = false;
    //                btnEdit.Enabled = false;
    //                btnEdit2.Enabled = false;                 
    //                btnDelete.Enabled = false;
    //                btnDelete2.Enabled = false;
    //            }
    //        }
    //        else
    //        {
    //            BtnNew.Enabled = false;
    //            btnNew2.Enabled = false;
    //            btnEdit.Enabled = false;
    //            btnEdit2.Enabled = false;              
    //            btnDelete.Enabled = false;
    //            btnDelete2.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        BtnNew.Enabled = false;
    //        btnNew2.Enabled = false;
    //        btnEdit.Enabled = false;
    //        btnEdit2.Enabled = false;           
    //        btnDelete.Enabled = false;
    //        btnDelete2.Enabled = false;
    //    }
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnDelete"] = btnDelete.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //}

    private void Inactive(int InsActId)
    {
        TSP.DataManager.InstitueActivityManager InstitueActivityManager = new TSP.DataManager.InstitueActivityManager();

        try
        {
            InstitueActivityManager.FindByCode(InsActId);
            if (InstitueActivityManager.Count == 1)
            {
                InstitueActivityManager[0].BeginEdit();

                InstitueActivityManager[0]["InActive"] = 1;
                InstitueActivityManager[0]["ModifiedDate"] = DateTime.Now;
                InstitueActivityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                InstitueActivityManager[0].EndEdit();

                int cn = InstitueActivityManager.Save();
                if (cn > 0)
                {
                    GridViewInsActivity.DataBind();
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

    private void Active(int InsActId)
    {
        TSP.DataManager.InstitueActivityManager InstitueActivityManager = new TSP.DataManager.InstitueActivityManager();

        try
        {
            InstitueActivityManager.FindByCode(InsActId);
            if (InstitueActivityManager.Count == 1)
            {
                InstitueActivityManager[0].BeginEdit();

                InstitueActivityManager[0]["InActive"] = 0;

                InstitueActivityManager[0].EndEdit();
                int cn = InstitueActivityManager.Save();
                if (cn > 0)
                {
                    GridViewInsActivity.DataBind();
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

    private int  FindInstitueCertificate(int InsId)
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
        if (CertType == 1 || CertType == 2)
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    #endregion

}
