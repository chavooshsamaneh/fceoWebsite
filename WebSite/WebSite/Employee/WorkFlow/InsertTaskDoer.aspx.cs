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

public partial class Employee_WorkFlow_InserTaskDoer : System.Web.UI.Page
{
    string DoerId;
    string PageMode;
    private System.Collections.ArrayList arlSendBackTask = new ArrayList();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
      
        if (Request.QueryString["PageMode"] == null || Request.QueryString["DId"] == null || (string.IsNullOrEmpty(Request.QueryString["PageMode"])) || Request.QueryString["TaskId"] == null)
        {
            Response.Redirect("~/Employee/WorkFlow/TaskDoer.aspx");
        }
       //
        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;           
            btnSave.Enabled = Per.CanNew || Per.CanEdit;
            btnSave2.Enabled = Per.CanNew || Per.CanEdit;

            string TaskId = Utility.DecryptQS(Request.QueryString["TaskId"].ToString());
            ObjdsNezamChart.SelectParameters[0].DefaultValue = TaskId;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowTaskManager.FindByCode(int.Parse(TaskId));
            if (WorkFlowTaskManager.Count > 0)
            {
                WorkFlowManager.FindByCode(int.Parse(WorkFlowTaskManager[0]["WorkflowId"].ToString()));
                if (WorkFlowManager.Count > 0)
                    //lblTaskName.Text = WorkFlowManager[0]["WorkFlowName"].ToString() + " > " + WorkFlowTaskManager[0]["TaskName"].ToString() + " _ " + "اولویت:" + WorkFlowTaskManager[0]["TaskOrder"].ToString();
                    //RoundPanelTaskDoerInfo.InnerText
                    RoundPanelTaskDoerInfoHeader.InnerText += "مشخصات انجام دهنده عملیات " + WorkFlowManager[0]["WorkFlowName"].ToString() + " > " + WorkFlowTaskManager[0]["TaskName"].ToString() + " _ " + "اولویت:" + WorkFlowTaskManager[0]["TaskOrder"].ToString();
                else
                {
                    Response.Redirect("~/Employee/WorkFlow/WorkFlowTask.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Employee/WorkFlow/TaskDoer.aspx");
            }
            //CmbNezamChartName.DataBind();
            //CmbNezamChartName.SelectedIndex = 0;
            //CmbNezamChartName_SelectedIndexChanged(this, new EventArgs());
            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;            
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        //????????????????/
        FillGrid();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];        
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTaskDoer["PageMode"].ToString());

        string DoerId = Utility.DecryptQS(HiddenFieldTaskDoer["DoerId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertTaskDoer();
                //Response.Redirect("AddCourse.aspx?TeId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
            }
            else if (PageMode == "Edit")
            {
                if (string.IsNullOrEmpty(DoerId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditTaskDoer(int.Parse(DoerId));
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (HiddenFieldTaskDoer["TaskId"] == null)
        {
            Response.Redirect("TaskDoer.aspx?TskId=" + HiddenFieldTaskDoer["TaskId"].ToString());            
        }
        string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]) && !string.IsNullOrEmpty(TaskId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("TaskDoer.aspx?TskId=" + HiddenFieldTaskDoer["TaskId"].ToString() + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("TaskDoer.aspx?TskId=" + HiddenFieldTaskDoer["TaskId"].ToString());
        }
    }

    //protected void CmbNezamChartName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (CmbNezamChartName.SelectedIndex > -1)
    //    {
    //        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
    //        NezamMemberChartManager.FindByNcId(int.Parse(CmbNezamChartName.SelectedItem.Value.ToString()),0);
    //        txtbName.Text = "";
    //        if (NezamMemberChartManager.Count > 0)
    //        {
    //            for (int i = 0; i < NezamMemberChartManager.Count; i++)
    //            {
    //                txtbName.Text += NezamMemberChartManager[i]["FirstName"].ToString() + " " + NezamMemberChartManager[i]["LastName"].ToString() + ";";
    //            }
    //        }
    //        CmbNezamChartName.ClientEnabled = true;
    //        //txtDoerOrder.ClientEnabled = true;
    //    }
    //    // txtbDescription.ClientEnabled = true;
    //}

    protected void GridViewSendBackTask_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTaskDoer["PageMode"].ToString());
        if (PageMode == "View")
        {
            e.Row.Enabled = false;
        }
        else
        {
            e.Row.Enabled = true;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnabaleControls();
        HiddenFieldTaskDoer["PageMode"] = Utility.EncryptQS("Edit");

        TSP.DataManager.Permission Per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = Per.CanNew;
        btnNew2.Enabled = Per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;        
        btnSave.Enabled = Per.CanEdit;
        btnSave2.Enabled = Per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;        
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //txtbName.Text = "";
        //CmbNezamChartName.Enabled = true;
        CmbNezamChartName.SelectedIndex = -1;
        CmbNezamChartName.ClientEnabled = true;
        //txtDoerOrder.Enabled = true;
        HiddenFieldTaskDoer["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldTaskDoer["DoerId"] = "";
        GridViewSendBackTask.Selection.UnselectAll();

        TSP.DataManager.Permission Per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = Per.CanNew;
        btnNew2.Enabled = Per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = Per.CanNew;
        btnSave2.Enabled = Per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        HiddenFieldTaskDoer["New"] = Utility.EncryptQS("New");
        HiddenFieldTaskDoer["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
        HiddenFieldTaskDoer["DoerId"] = Server.HtmlDecode(Request.QueryString["DId"]).ToString();
        HiddenFieldTaskDoer["TaskId"] = Server.HtmlDecode(Request.QueryString["TaskId"].ToString());
        WorkFlowTaskManager.FindByCode(int.Parse(Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString())));
        string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
        HiddenFieldTaskDoer["WorkFlowId"] = Utility.EncryptQS(WorkFlowId);

        Session["WorkFlowTaskDataTable"] = CreateWorkTaskDataTable();

        PageMode = Utility.DecryptQS(HiddenFieldTaskDoer["PageMode"].ToString());
        DoerId = Utility.DecryptQS(HiddenFieldTaskDoer["DoerId"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
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
        }
    }

    private void SetViewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());      
        btnSave.ClientEnabled = false;
        btnSave2.ClientEnabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.ClientEnabled = true;
            btnEdit2.ClientEnabled = true;
        }
        CmbNezamChartName.ClientEnabled = false;

        this.ViewState["BtnSave"] = btnSave.ClientEnabled;
        this.ViewState["BtnEdit"] = btnEdit.ClientEnabled;        
        this.ViewState["BtnNew"] = btnNew.Enabled;

        FillForm(int.Parse(DoerId));

        RoundPanelTaskDoer.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        this.ViewState["BtnEdit"] = btnEdit.ClientEnabled;        
        ClearForm();
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //int TaskId = int.Parse(Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString()));  
        FillGrid();
        RoundPanelTaskDoer.HeaderText = "جدید";

    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());      
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        btnSave.ClientEnabled = per.CanEdit;
        btnSave2.ClientEnabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.ClientEnabled;
        this.ViewState["BtnEdit"] = btnEdit.ClientEnabled;

        if (string.IsNullOrEmpty(DoerId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        EnabaleControls();
        FillForm(int.Parse(DoerId));       
        RoundPanelTaskDoer.HeaderText = "ویرایش";
    }

    private void FillForm(int DoerId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TaskDoerManager.FindByCode(DoerId);

        if (TaskDoerManager.Count > 0)
        {
            //txtDoerOrder.Text = TaskDoerManager[0]["DoerOrder"].ToString();
            CmbNezamChartName.DataBind();
            CmbNezamChartName.SelectedIndex = CmbNezamChartName.Items.IndexOfValue(TaskDoerManager[0]["NcId"].ToString());
            HiddenFieldTaskDoer["NcId"] = Utility.EncryptQS(TaskDoerManager[0]["NcId"].ToString());
            //txtbName.Text = TaskDoerManager[0]["FirstName"].ToString() + " " + TaskDoerManager[0]["LastName"].ToString();
                      
            FillGrid();
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
        }

    }

    private void ClearForm()
    {
        CmbNezamChartName.DataBind();
        CmbNezamChartName.SelectedIndex = -1;
        GridViewSendBackTask.Selection.UnselectAll();
    }

    private void EnabaleControls()
    {
        CmbNezamChartName.ClientEnabled = true;
        GridViewSendBackTask.Enabled = true;
    }

    private void SetRoundPanelHeader(string PageMode)
    {

        //TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        //TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();

        //if (!String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]))))
        //{
        //    string TypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]));
        //   // ObjdsSmsConfirm.SelectParameters[0].DefaultValue = TypeId;
        //    if (!String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]))))
        //    {
        //        string EmpId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]));
        //        string TypeName = "";
        //        string EmpName = "";
        //        EmployeeManager.FindByCode(int.Parse(EmpId));
        //        if (EmployeeManager.Count > 0)
        //        {
        //            EmpName = EmployeeManager[0]["FirstName"].ToString() + " " + EmployeeManager[0]["LastName"].ToString();
        //        }
        //        SmsTypeManager.FindByCode(int.Parse(TypeId));
        //        if (SmsTypeManager.Count > 0)
        //        {
        //            TypeName = SmsTypeManager[0]["SmsTypeName"].ToString();
        //        }
        //        RoundPanelConfirmPerson.HeaderText = PageMode;
        //        if (TypeName != "" && EmpName != "")
        //            lblTaskName.Text = "نوع پیام کوتاه " + TypeName + " برای  " + EmpName;
        //        else
        //            lblTaskName.Text = "";
        //    }
        //}
    }

    private DataTable CreateWorkTaskDataTable()
    {
        DataTable dtWorkFlowTask = new DataTable();

        TSP.DataManager.TaskDoerManager manager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
        WorkFlowTaskManager.FindByCode(int.Parse(TaskId));
        if (WorkFlowTaskManager.Count > 0)
        {
            int workId = int.Parse(Utility.DecryptQS(HiddenFieldTaskDoer["WorkFlowId"].ToString()));
            string TaskOrder = WorkFlowTaskManager[0]["TaskOrder"].ToString();
           // int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskOrder"]);
            dtWorkFlowTask = WorkFlowTaskManager.SelectByWorkId(workId, -1, -1, int.Parse(TaskId), 1);
        }
        //PRegisterId = Utility.DecryptQS(HiddenFieldPreRegister["PRegisterId"].ToString());
        //manager.FindByPRegisterId(int.Parse(PRegisterId));

        return dtWorkFlowTask;
    }

    private void FillGrid()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int WorkFlowId = int.Parse(Utility.DecryptQS(HiddenFieldTaskDoer["WorkFlowId"].ToString()));

        DataTable dtTask = (DataTable)(Session["WorkFlowTaskDataTable"]);
        GridViewSendBackTask.DataSource = dtTask;
        GridViewSendBackTask.KeyFieldName = "TaskId";
        GridViewSendBackTask.DataBind();
        string PageMode = Utility.DecryptQS(HiddenFieldTaskDoer["PageMode"].ToString());
        if (PageMode != "New" && !string.IsNullOrEmpty(DoerId) )
            TaskDoerManager.FindByCode(int.Parse(DoerId));
        int SendBackTask = 0;
        if (TaskDoerManager.Count > 0)
            SendBackTask = int.Parse(TaskDoerManager[0]["SendBackTask"].ToString());
        if (SendBackTask != 0)
        {
            DataRow CurrentRow;
            for (int i = 0; i < GridViewSendBackTask.VisibleRowCount; i++)
            {
                CurrentRow = GridViewSendBackTask.GetDataRow(i);
                int TCode = Convert.ToInt32(CurrentRow["TCode"]);

                if ((TCode &= SendBackTask) == int.Parse(CurrentRow["TCode"].ToString()))
                    GridViewSendBackTask.Selection.SetSelection(i, true);
            }
        }
    }

    private void InsertTaskDoer()
    {
        try
        {
            int SumSendBack = SelectSendBack();
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(int.Parse(TaskId));
            TaskDoerManager.CurrentFilter = "NcId=" + CmbNezamChartName.SelectedItem.Value.ToString();
            if (TaskDoerManager.DataTable.DefaultView.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "سمت انتخاب شده تکراری می باشد.";
                return;
            }
            DataRow DoerRow = TaskDoerManager.NewRow();
            DoerRow["TaskId"] = int.Parse(TaskId);
            DoerRow["SendBackTask"] = SumSendBack;
            DoerRow["NcId"] = int.Parse(CmbNezamChartName.SelectedItem.Value.ToString());
            DoerRow["DoerOrder"] = 1;// txtDoerOrder.Text;
            DoerRow["UserId"] = Utility.GetCurrentUser_UserId();
            DoerRow["ModifiedDate"] = DateTime.Now;

            TaskDoerManager.AddRow(DoerRow);
            int cn = TaskDoerManager.Save();
            if (cn > 0)
            {
                HiddenFieldTaskDoer["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldTaskDoer["DoerId"] = Utility.EncryptQS(TaskDoerManager[0]["DoerId"].ToString());
                HiddenFieldTaskDoer["NcId"] = Utility.EncryptQS(CmbNezamChartName.SelectedItem.Value.ToString());
                RoundPanelTaskDoer.HeaderText = "ویرایش";
                TSP.DataManager.Permission per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void EditTaskDoer(int DoerId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        try
        {
            string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());            
            int NcIdOrigin =int.Parse( Utility.DecryptQS(HiddenFieldTaskDoer["NcId"].ToString()));
            if (NcIdOrigin != int.Parse(CmbNezamChartName.SelectedItem.Value.ToString()))
            {
                TaskDoerManager.FindByTaskId(int.Parse(TaskId));
                TaskDoerManager.CurrentFilter = "NcId=" + CmbNezamChartName.SelectedItem.Value.ToString();
                if (TaskDoerManager.DataTable.DefaultView.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "سمت انتخاب شده تکراری می باشد.";
                    return;
                }
            }
            TaskDoerManager.CurrentFilter = "";
            TaskDoerManager.FindByCode(DoerId);
            if (TaskDoerManager.Count > 0)
            {
                TaskDoerManager[0].BeginEdit();

                int SumSendBack = SelectSendBack();
                DataRow DoerRow = TaskDoerManager.NewRow();
                TaskDoerManager[0]["TaskId"] = int.Parse(TaskId);
                TaskDoerManager[0]["SendBackTask"] = SumSendBack;
                TaskDoerManager[0]["NcId"] = int.Parse(CmbNezamChartName.SelectedItem.Value.ToString());
                TaskDoerManager[0]["DoerOrder"] = 1;// txtDoerOrder.Text;
                TaskDoerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TaskDoerManager[0]["ModifiedDate"] = DateTime.Now;

                TaskDoerManager[0].EndEdit();
                int cn = TaskDoerManager.Save();
                if (cn > 0)
                {
                    HiddenFieldTaskDoer["PageMode"] = Utility.EncryptQS("ٍEdit");
                    //HiddenFieldTaskDoer["TaskId"] = Utility.EncryptQS(TaskDoerManager[0]["TaskId"].ToString());
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
            SetError(err);
        }
    }

    private int SelectSendBack()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int SumSendBack = 0;
        object[] array = null;
        array = GridViewSendBackTask.GetSelectedFieldValues("TaskId").ToArray();
        arlSendBackTask.Clear();
        for (int i = 0; i < array.Length; i++)
        {
            //  arlSendBackTask.Add(int.Parse(array[i].ToString()));
            WorkFlowTaskManager.FindByCode(int.Parse(array[i].ToString()));
            int TCode = int.Parse(WorkFlowTaskManager[0]["TCode"].ToString());

            SumSendBack += TCode;
        }
        return SumSendBack;
    }    

    private void SetDeleteError(Exception err)
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
    #endregion
}
