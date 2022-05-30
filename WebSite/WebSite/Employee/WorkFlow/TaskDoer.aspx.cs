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

public partial class Employee_WorkFlow_TaskDoer : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
               if (string.IsNullOrEmpty(Request.QueryString["TskId"]) || string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) || string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            Response.Redirect("WorkFlowTask.aspx");
            return;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.TaskDoerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnDelete.Enabled = Per.CanDelete;
            btnDelete2.Enabled = Per.CanDelete;
            btnView.Enabled = Per.CanView;
            btnView2.Enabled = Per.CanView;
            GridViewTaskDoer.Visible = Per.CanView;

            HiddenFieldTaskDoer["TaskId"] = Request.QueryString["TskId"];
            HiddenFieldTaskDoer["GrdFlt"] = Request.QueryString["GrdFlt"];
            HiddenFieldTaskDoer["SrchFlt"] = Request.QueryString["SrchFlt"];
            string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
            ObjdsTaskDoer.SelectParameters[0].DefaultValue = TaskId;

          //  string TaskId = Utility.DecryptQS(Request.QueryString["TaskId"].ToString());
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByCode(int.Parse(TaskId));
            //lblTaskName.Text = WorkFlowTaskManager[0]["TaskName"].ToString() + " _ " + "اولویت:" + WorkFlowTaskManager[0]["TaskOrder"].ToString();
            int WFId = int.Parse(WorkFlowTaskManager[0]["WorkFlowId"].ToString());
            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByCode(WFId);
            if (WorkFlowManager.Count == 1)
            {
                RoundPanelTaskDoer.HeaderText = "گردش کار " + WorkFlowManager[0]["WorkFlowName"].ToString() + " > " + WorkFlowTaskManager[0]["TaskName"].ToString() + " _ " + "اولویت:" + WorkFlowTaskManager[0]["TaskOrder"].ToString();
            }
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridViewTaskDoer.FocusedRowIndex > -1)
        {
            DataRow TaskDoerRow = GridViewTaskDoer.GetDataRow(GridViewTaskDoer.FocusedRowIndex);
            int DoerId = int.Parse(TaskDoerRow["DoerId"].ToString());
            DeleteTaskDoer(DoerId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (HiddenFieldTaskDoer["TaskId"] == null)
        {
            Response.Redirect("WorkFlowTask.aspx");
        }
        string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]) && !string.IsNullOrEmpty(TaskId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("WorkFlowTask.aspx?PostId=" + HiddenFieldTaskDoer["TaskId"].ToString() + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("WorkFlowTask.aspx");
        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        string GridFilterString = Request.QueryString["GrdFlt"];
        string SearchFilterString = Request.QueryString["SrchFlt"];

        int DoerId = -1;
        int FocucedIndex = GridViewTaskDoer.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            DataRow row = GridViewTaskDoer.GetDataRow(FocucedIndex);
            DoerId = (int)(row["DoerId"]);

        }
        if (DoerId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                DoerId = -1;
                Response.Redirect("InsertTaskDoer.aspx?DId=" + Utility.EncryptQS(DoerId.ToString())+"&TaskId="+HiddenFieldTaskDoer["TaskId"].ToString() + "&PageMode=" + Utility.EncryptQS(Mode)
                + "&GrdFlt=" + GridFilterString + "&SrchFlt=" + SearchFilterString);
            }
            else
            {
                Response.Redirect("InsertTaskDoer.aspx?DId=" + Utility.EncryptQS(DoerId.ToString()) + "&TaskId=" + HiddenFieldTaskDoer["TaskId"].ToString() + "&PageMode=" + Utility.EncryptQS(Mode)
                + "&GrdFlt=" + GridFilterString + "&SrchFlt=" + SearchFilterString);
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

    private void DeleteTaskDoer(int DoerId)
    {
        //TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
       // TransactionManager.Add(TaskDoerManager);
        try
        {
          //  TransactionManager.BeginSave();
            TaskDoerManager.FindByCode(DoerId);
            if (TaskDoerManager.Count > 0)
            {
                TaskDoerManager[0].Delete();
                if (TaskDoerManager.Save() > 0)
                {
                    //string TaskId = Utility.DecryptQS(HiddenFieldTaskDoer["TaskId"].ToString());
                   // TaskDoerManager.ClearBeforeFill = true;
                    //TaskDoerManager.FindByTaskId(int.Parse(TaskId));
                    //int DoerCount = TaskDoerManager.Count;
                    //if (DoerCount > 0)
                    //{
                    //    int j = 0;
                    //    for (int i = 0; i < DoerCount; i++)
                    //    {
                    //        j = i + 1;
                    //        TaskDoerManager[i]["DoerOrder"] = j;
                    //    }
                    //    int cnt = TaskDoerManager.Save();
                    //    if (cnt > 0)
                    //    {
                            //TransactionManager.EndSave();
                            GridViewTaskDoer.DataBind();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "حذف انجام شد.";
                    //    }
                    //    else
                    //    {
                    //        TransactionManager.CancelSave();
                    //        this.DivReport.Visible = true;
                    //        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    //    }
                    //}
                    //else
                    //{
                    //    TransactionManager.EndSave();
                    //    GridViewTaskDoer.DataBind();
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "حذف انجام شد.";
                    //}
                }
                else
                {
                    //TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
               // TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }   

    #endregion    
}
