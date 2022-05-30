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

public partial class Employee_WorkFlow_WorkFlowTaskInsert : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["TskId"]) || string.IsNullOrEmpty(Request.QueryString["WId"]))
        {
            Response.Redirect("~/Employee/WorkFlow/WorkFlowTask.aspx");
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowTaskManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnSave.Enabled = Per.CanNew || Per.CanEdit;
            btnSave2.Enabled = Per.CanNew || Per.CanEdit;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTask["PageMode"].ToString());

        string TaskId = Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();
                //Response.Redirect("AddCourse.aspx?TeId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
            }
            else if (PageMode == "Edit")
            {
                if (string.IsNullOrEmpty(TaskId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(TaskId));
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (HiddenFieldTask["TaskId"] == null)
        {
            Response.Redirect("TaskDoer.aspx?TskId=" + HiddenFieldTask["TaskId"].ToString());
        }
        string TaskId = Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]) && !string.IsNullOrEmpty(TaskId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("WorkFlowTask.aspx?PostId=" + HiddenFieldTask["TaskId"].ToString() + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("WorkFlowTask.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Enable();
        HiddenFieldTask["PageMode"] = Utility.EncryptQS("Edit");

        TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowTaskManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = Per.CanEdit;
        btnSave2.Enabled = Per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        HiddenFieldTask["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
        // HiddenFieldTask["WFId"] = Server.HtmlDecode(Request.QueryString["DId"]).ToString();
        HiddenFieldTask["TaskId"] = Server.HtmlDecode(Request.QueryString["TskId"].ToString());
        WorkFlowTaskManager.FindByCode(int.Parse(Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString())));
        string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
        HiddenFieldTask["WorkFlowId"] = Utility.EncryptQS(WorkFlowId);

        try
        {
            string PageMode = Utility.DecryptQS(HiddenFieldTask["PageMode"].ToString());
            string TaskId = Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString());
            SetMode(PageMode);
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
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

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowTaskManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.ClientEnabled = false;
        btnSave2.ClientEnabled = false;
        if (Per.CanEdit)
        {
            btnEdit.ClientEnabled = true;
            btnEdit2.ClientEnabled = true;
        }

        Disable();

        this.ViewState["BtnSave"] = btnSave.ClientEnabled;
        this.ViewState["BtnEdit"] = btnEdit.ClientEnabled;
        int TaskId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString()));
        FillForm(TaskId);

        RoundPanelTask.HeaderText = "مشاهده";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowTaskManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        btnSave.ClientEnabled = Per.CanEdit;
        btnSave2.ClientEnabled = Per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.ClientEnabled;
        this.ViewState["BtnEdit"] = btnEdit.ClientEnabled;
        Enable();
        int TaskId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldTask["TaskId"].ToString()));
        FillForm(TaskId);
        RoundPanelTask.HeaderText = "ویرایش";
    }

    private void FillForm(int TaskId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByCode(TaskId);

        if (WorkFlowTaskManager.Count == 1)
        {
            txtWfName.Text = WorkFlowTaskManager[0]["WorkFlowName"].ToString();
            txtTaskOrder.Text = WorkFlowTaskManager[0]["TaskOrder"].ToString();
            txtTaskName.Text = WorkFlowTaskManager[0]["TaskName"].ToString();
            txtDescription.Text = WorkFlowTaskManager[0]["Description"].ToString();
            txtSMSBody.Text = WorkFlowTaskManager[0]["SmsBody"].ToString();
            chkSendSMS.Checked = Convert.ToBoolean(WorkFlowTaskManager[0]["IsSmsSend"]) ? true : false;
        }
        else
        {
            SetError("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است");
        }

    }

    private void ClearForm()
    {
        txtDescription.Text = "";
        txtTaskName.Text = "";
        txtTaskOrder.Text = "";
    }

    private void Insert()
    {
    }

    private void Edit(int TaskId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        try
        {
            WorkFlowTaskManager.FindByCode(TaskId);
            if (WorkFlowTaskManager.Count == 1)
            {
                int WorkFlowId = Convert.ToInt32(WorkFlowTaskManager[0]["WorkFlowId"]);
                int TaskOreder = Convert.ToInt32(txtTaskOrder.Text);
                int TaskType = Convert.ToInt32(WorkFlowTaskManager[0]["Type"]);// int.Parse(TaskRow["Type"].ToString());
                if ((TaskType == (int)TSP.DataManager.WorkFlowTaskType.StartingTask) && (TaskOreder != 1))
                {
                    SetError("اولویت عملیات انتخابی بایستی یک باشد.");
                    return;
                }

                DataTable dtTask = WorkFlowTaskManager.SelectByWorkId(WorkFlowId);
                dtTask.DefaultView.RowFilter = "TaskOrder=" + txtTaskOrder.Text + "and TaskId <>" + TaskId;
                if (dtTask.DefaultView.Count > 0)
                {
                    SetError("اولویت وارد شده تکراری می باشد.");
                    return;
                }
                //   dtTask.DefaultView.RowFilter = "";
                dtTask.DefaultView.RowFilter = "TaskName=" + "'" + txtTaskName.Text.Trim() + "'" + "and TaskId <>" + TaskId;
                if (dtTask.DefaultView.Count > 0)
                {
                    SetError("عنوان عملیات وارد شده تکراری می باشد.");
                    return;
                }
                WorkFlowTaskManager[0].BeginEdit();

                WorkFlowTaskManager[0]["TaskName"] = txtTaskName.Text;
                WorkFlowTaskManager[0]["TaskOrder"] = txtTaskOrder.Text;
                WorkFlowTaskManager[0]["Description"] = txtDescription.Text;
                WorkFlowTaskManager[0]["SmsBody"] = txtSMSBody.Text.Trim();
                WorkFlowTaskManager[0]["IsSmsSend"] = chkSendSMS.Checked ? 1 : 0;

                WorkFlowTaskManager[0].EndEdit();

                if (WorkFlowTaskManager.Save() > 0)
                {
                    //HiddenFieldTask["PageMode"] = Utility.EncryptQS("ٍEdit");
                    SetError("ذخیره انجام شد.");
                }
                else
                {
                    SetError("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetError("اطلاعات مورد نظر توسط کاربر دیگر تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                SetError("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetError("خطایی در حذف انجام گرفته است");
            }
        }
        else
        {
            SetError("خطایی در حذف انجام گرفته است");
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetError("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetError("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetError("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetError("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetError("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void Enable()
    {
        txtDescription.Enabled = true;
        txtTaskName.Enabled = true;
        txtTaskOrder.Enabled = true;
        txtWfName.Enabled = true;
        chkSendSMS.Enabled = true;
        txtSMSBody.Enabled = true;
    }

    private void Disable()
    {
        txtDescription.Enabled = false;
        txtTaskName.Enabled = false;
        txtTaskOrder.Enabled = false;
        txtWfName.Enabled = false;
        chkSendSMS.Enabled = false;
        txtSMSBody.Enabled = false;
    }

    private void SetError(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion
}
