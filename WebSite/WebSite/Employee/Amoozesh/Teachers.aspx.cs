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
using System.IO;
using DevExpress.Web;

public partial class Employee_Amoozesh_Teachers : System.Web.UI.Page
{
    #region Events
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        if (!IsPostBack)
        {
            GridViewTeacher.JSProperties["cpPrint"] = 0;
            this.DivReport.Visible = true;

            Session["SendBackDataTable_Teacher"] = "";
            CheckPermission();

            SetHelpAddress();

            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowId;
            }
            // SelectSendBackTask();

            this.ViewState["BtnHelp"] = btnHelp.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;

        }
        //        string Script = "  <script language=''javascript''>function SetError(result) {if(result!= null){";
        //        Script += "document.getElementById(''<%=DivReport.ClientID%>'').style.visibility='visible';";
        //        Script += "document.getElementById(''<%=DivReport.ClientID%>'').style.display='block';  //='visible';";
        //        Script += @"document.getElementById('<%=LabelWarning.ClientID%>').innerText= result;
        //                }}</script>";

        //        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetError", Script);

        if (this.ViewState["BtnHelp"] != null)
            this.btnHelp.Enabled = this.btnHelp2.Enabled = (bool)this.ViewState["BtnHelp"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

        // Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");

        //this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("style", "display:none");

        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    //**********************Buttons*******************************
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTeachers.aspx?TeId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New") + "&TcId=" + Utility.EncryptQS("-1"));

        //Response.Redirect("AddTeacher.aspx?PageMode=" +Utility.EncryptQS("New"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int TcId = -1;
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewTeacherCertificate = (TSP.WebControls.CustomAspxDevGridView)GridViewTeacher.FindDetailRowTemplateControl(GridViewTeacher.FocusedRowIndex, "GridViewTeacherCertificate");
            if (GridViewTeacherCertificate != null)
            {
                if (GridViewTeacherCertificate.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewTeacherCertificate.GetDataRow(GridViewTeacherCertificate.FocusedRowIndex);
                    TcId = (int)row["TcId"];
                }
            }
            else
            {
             SetMessage( "برای ویرایش  اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }

        int TeId = -1;
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            TeId = (int)row["TeId"];
        }
        if (TeId == -1)
        {
            SetMessage("برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            if (CheckWorkFlowPermission(TeId))
                Response.Redirect("AddTeachers.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&TcId=" + Utility.EncryptQS(TcId.ToString()));
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int TcId = -1;
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewTeacherCertificate = (TSP.WebControls.CustomAspxDevGridView)GridViewTeacher.FindDetailRowTemplateControl(GridViewTeacher.FocusedRowIndex, "GridViewTeacherCertificate");
            if (GridViewTeacherCertificate != null)
            {
                if (GridViewTeacherCertificate.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewTeacherCertificate.GetDataRow(GridViewTeacherCertificate.FocusedRowIndex);
                    TcId = (int)row["TcId"];
                }
            }
            else
            {
                 SetMessage( "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }

        int TeId = -1;
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            TeId = (int)row["TeId"];
        }
        if (TeId == -1)
        {
             SetMessage( "برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            Response.Redirect("AddTeachers.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&TcId=" + Utility.EncryptQS(TcId.ToString()));
        }

    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            DataRow TeacherRow = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            int TableId = int.Parse(TeacherRow["TeId"].ToString());

            string GridFilterString = GridViewTeacher.FilterExpression;

            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                "&PostId=" + Utility.EncryptQS(TableId.ToString());


            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                 + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
           SetMessage("ردیفی انتخاب نشده است.");
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        try
        {
            int TeId = -1;
            if (GridViewTeacher.FocusedRowIndex > -1)
            {
                DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
                TeId = (int)row["TeId"];
            }
            if (TeId == -1)
            {
                SetMessage("لطفاً برای تمدید گواهینامه استاد ابتدا یک رکورد را انتخاب نمائید.");
            }

            TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
            DataTable dtTeacherCertificate = TeacherCertificateManager.SelectLastVersion(TeId);
            if (dtTeacherCertificate.Rows.Count != 1)
            {
                SetMessage( Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            if (Convert.ToInt32(dtTeacherCertificate.Rows[0]["IsConfirm"]) == 0)
            {
              SetMessage("امکان ثبت درخواست جدید وجود ندارد.برای استاد انتخاب شده درخواست درجریان وجود دارد");
                return;
            }
            Response.Redirect("AddTeachers.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("RevivalRequest") + "&TcId=" + Utility.EncryptQS(dtTeacherCertificate.Rows[0]["TcId"].ToString()));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    protected void btnCertificateChange_Click(object sender, EventArgs e)
    {
        try
        {

            int TeId = -1;
            if (GridViewTeacher.FocusedRowIndex > -1)
            {
                DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
                TeId = (int)row["TeId"];
            }
            if (TeId == -1)
            {
               SetMessage("لطفاً برای تمدید گواهینامه استاد ابتدا یک رکورد را انتخاب نمائید.");
            }

            TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
            DataTable dtTeacherCertificate = TeacherCertificateManager.SelectLastVersion(TeId);
            if (dtTeacherCertificate.Rows.Count != 1)
            {
               SetMessage( Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            if (Convert.ToInt32(dtTeacherCertificate.Rows[0]["IsConfirm"]) == 0)
            {
            SetMessage( "امکان ثبت درخواست جدید وجود ندارد.برای استاد انتخاب شده درخواست درجریان وجود دارد");
                return;
            }
            Response.Redirect("AddTeachers.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("changeRequest") + "&TcId=" + Utility.EncryptQS(dtTeacherCertificate.Rows[0]["TcId"].ToString()));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int TeId = -1;
        string RsType = "";

        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            TeId = (int)row["TeId"];
            RsType = ((int)TSP.DataManager.ResetPasswordType.Teacher).ToString();

        }
        if (TeId == -1)
        {
          SetMessage( "ردیفی انتخاب نشده است");
            return;
        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(TeId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }

    protected void btnActive_Click(object sendet, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            int TeId = (int)row["TeId"];
            ChandeActiveStatus(TeId, true);
            GridViewTeacher.DataBind();

        }
        else
        {
          SetMessage( "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
    }

    protected void btnInActive_Click(object sendet, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            int TeId = (int)row["TeId"];
            ChandeActiveStatus(TeId, false);
            GridViewTeacher.DataBind();
        }
        else
        {
            SetMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Teachers";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnClearsearch_Click(object sender, EventArgs e)
    {
        ClearSearch();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    //**************************************Grid*********************************
    protected void GridViewTeacher_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    GridViewTeacher.JSProperties["cpPrint"] = 1;
                    GridViewTeacher.DetailRows.CollapseAllRows();
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewTeacher.Columns;
                    Session["DataSource"] = OdbTeacher;

                    Session["Title"] = "اساتید";
                    break;
            }
        }
        else
            GridViewTeacher.DataBind();
    }

    protected void GridViewTeacherCertificate_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewTeacher_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewTeacher.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewTeacher_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("TaskCode") != null)
        {
            if (!string.IsNullOrEmpty(e.GetValue("TaskCode").ToString()))
            {
                int TaskCode = int.Parse(e.GetValue("TaskCode").ToString());
                int TaskCodeConfirmTeacher = (int)TSP.DataManager.WorkFlowTask.ConfirmTeacherAndEndProccess;
                int TaskCodeRejectTeacher = (int)TSP.DataManager.WorkFlowTask.RejectTeacherAndEndProccess;
                int TaskCodesettlementAgent = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringTeacher;
                if (TaskCode != TaskCodeConfirmTeacher && TaskCode != TaskCodeRejectTeacher && TaskCode != TaskCodesettlementAgent)
                    e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
            }
        }
    }

    protected void GridViewTeacherCertificate_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";


    }

    protected void GridViewTeacherCertificate_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewTeacher_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        #region IsExpired
        if (e.DataColumn.Name == "IsExpired")
        {

            //btnIsExpired
            DevExpress.Web.ASPxImage btnIsExpired = (DevExpress.Web.ASPxImage)GridViewTeacher.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewTeacher.Columns["IsExpired"], "btnIsExpired");
            if (btnIsExpired != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("EndDate")))
                {
                    btnIsExpired.ToolTip = "نامشخص";
                    btnIsExpired.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }
                else
                {
                    string Today = Utility.GetDateOfToday();
                    string EndDate = e.GetValue("EndDate").ToString();
                    int IsExpired = string.Compare(EndDate, Today);
                    if (IsExpired >= 0)
                    {
                        btnIsExpired.ToolTip = "دارای اعتبار";
                        btnIsExpired.ImageUrl = "~/Images/CertificateValid.png";
                        //  btnIsExpired.Value = 0;
                        return;
                    }
                    else
                    {
                        btnIsExpired.ToolTip = "پایان اعتبار";
                        btnIsExpired.ImageUrl = "~/Images/CertificateExpired.png";
                        //btnIsExpired.Value = 1;
                        return;
                    }
                }
            }
        }
        #endregion

        #region  WF
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewTeacher.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewTeacher.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
        #endregion
    }

    //********************CallBacks*************************************

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewTeacher.FocusedRowIndex > -1)
        {
            DataRow TeacherRow = GridViewTeacher.GetDataRow(GridViewTeacher.FocusedRowIndex);
            int TeacherId = int.Parse(TeacherRow["TeId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            int WfCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;
            WFUserControl.PerformCallback(TeacherId, TableType, WfCode, e);
            GridViewTeacher.DataBind();
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    #endregion

    #region Methods
    private void CheckPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(int.Parse(Utility.GetCurrentUser_UserId().ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnView.Enabled = per.CanView;
        btnView2.Enabled = per.CanView;
        GridViewTeacher.Visible = per.CanView;
        btnHelp.Enabled = per.CanView;
        btnHelp2.Enabled = per.CanView;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.Teachers).ToString());
    }

    private Boolean CheckWorkFlowPermission(int TeId)
    {

        int PermissionEdit = -1;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        PermissionEdit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TeId, TaskCode, int.Parse(Utility.GetCurrentUser_UserId().ToString()));
        if (PermissionEdit > 0)
        {
            return true;
        }
        else
        {
      SetMessage( "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد");
            return false;
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
            SetMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage("خطایی در حذف انجام گرفته است");
            }
        }
        else
        {
          SetMessage( "خطایی در حذف انجام گرفته است");
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
         SetMessage( "اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
          SetMessage( "کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
              SetMessage( "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
            }
            else
            {
                SetMessage( "خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
           SetMessage( "خطایی در ذخیره انجام گرفته است");
        }
    }

    private void ChandeActiveStatus(int TeId, Boolean Active)
    {

        TSP.DataManager.TeacherManager managerEdit = new TSP.DataManager.TeacherManager();
        managerEdit.FindByCode(TeId);
        if (managerEdit.Count == 1)
        {

            try
            {

                managerEdit[0].BeginEdit();
                if (Active)
                    managerEdit[0]["InActive"] = 0;
                else
                    managerEdit[0]["InActive"] = 1;
                managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
                managerEdit[0]["ModifiedDate"] = DateTime.Now;
                managerEdit[0].EndEdit();
                if (managerEdit.Save() > 0)
                {
                   SetMessage( "ذخیره انجام شد");

                }
                else
                {
              SetMessage( "خطایی در ذخیره انجام گرفته است");
                }
            }
            catch (Exception err)
            {


                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                     SetMessage( "اطلاعات تکراری می باشد");
                    }
                    else
                    {
                      SetMessage( "خطایی در ذخیره انجام گرفته است");
                    }
                }
                else
                {
                     SetMessage( "خطایی در ذخیره انجام گرفته است");
                }
            }
        }
        else
        {
    SetMessage( "خطایی در بازیابی اطلاعات بوجود آمده است");
        }
    }

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            OdbTeacher.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbTeacher.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            OdbTeacher.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            OdbTeacher.SelectParameters["EndDateTo"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtName.Text))
            OdbTeacher.SelectParameters["Name"].DefaultValue = txtName.Text;
        else
            OdbTeacher.SelectParameters["Name"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtFamily.Text))
            OdbTeacher.SelectParameters["Family"].DefaultValue = txtFamily.Text;
        else
            OdbTeacher.SelectParameters["Family"].DefaultValue = "%";
        if (!Utility.IsDBNullOrNullValue(cmbMajor.Value))
            OdbTeacher.SelectParameters["MjId"].DefaultValue = cmbMajor.Value.ToString();
        else
            OdbTeacher.SelectParameters["MjId"].DefaultValue = "-1";
        if (!Utility.IsDBNullOrNullValue(cmbLicence.Value))
            OdbTeacher.SelectParameters["LiId"].DefaultValue = cmbLicence.Value.ToString();
        else
            OdbTeacher.SelectParameters["LiId"].DefaultValue = "-1";
        GridViewTeacher.DataBind();
    }

    private void ClearSearch()
    {
        txtEndDateFrom.Text = "";
        txtEndDateTo.Text = "";
        txtFamily.Text = "";
        txtName.Text = "";
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = -1;
        cmbLicence.DataBind();
        cmbLicence.SelectedIndex = -1;

        Search();
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.LabelWarning.Text = Message;
    }
    #endregion
}
