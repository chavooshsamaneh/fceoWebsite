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

public partial class Employee_Employee_Employee : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        #region PageRefresh
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
        #endregion

        if (Utility.GetCurrentUser_AgentId() != Utility.GetMainAgentId())
        {
            ObjdsEmployee.FilterExpression = "AgentId=" + Utility.GetCurrentUser_AgentId();
        }
        GridViewEmployee.JSProperties["cpIsPostBack"] = 0;
        GridViewEmployee.JSProperties["cpSelectedIndex"] = 0;

        if (!IsPostBack)
        {
            Session["SendBackDataTable_EmpWF"] = null;
            GridViewEmployee.JSProperties["cpIsPostBack"] = 1;
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming).ToString();

            ObjdsEmployee.CacheDuration = Utility.GetCacheDuration();
            CheckTablePermission();
            CheckWorkFlowPermissionForChangeReq();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive2.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnUserRight"] = btnUserRight.Enabled;
            this.ViewState["GridView"] = GridViewEmployee.ClientVisible;
            this.ViewState["BtnChange"] = btnChangeReq.Enabled;

            GridViewEmployee.JSProperties["cpIsReturn"] = 0;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = this.btnPrint.Enabled = this.btnPrint2.Enabled = this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = this.btnReset.Enabled = this.btnReset1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive2.Enabled = this.btnDisActive.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["GridView"] != null)
            this.GridViewEmployee.ClientVisible = (bool)this.ViewState["GridView"];
        if (this.ViewState["BtnChange"] != null)
            this.btnChangeReq.Enabled = this.btnChangeReq2.Enabled = (bool)this.ViewState["BtnChange"];

        SetPageFilter();
        SetGridRowIndex();

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");
        DeletedColumnsName.Add("LastName");
        DeletedColumnsName.Add("PartId");
        
        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewEmployee.Columns;
        Session["DataSource"] = ObjdsEmployee;
        Session["Title"] = "كارمندان";
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            int EmpId = (int)row["EmpId"];
            EmployeeRequestManager.SelectLastVersion(EmpId, 0);
            if (EmployeeRequestManager.Count == 1)
            {
                int EmpReId = (int)EmployeeRequestManager[0]["EmpReId"];
                if (CheckPermitionForEdit(EmpReId))
                    NextPage("Edit");
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "با توجه به سطح دسترسی گردش کار و مرحله گردش کار قادر به ویرایش اطلاعات نمی باشید.";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کارمند انتخاب شده دارای درخواست با وضعیت معلق نمی باشد.جهت ویرایش اطلاعات درخواست تغییرات بدهید.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnUserRight_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        string GridFilterString = GridViewEmployee.FilterExpression;

        int EmpId = -1;
        int UserId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
            UserId = (int)row["LoginUserId"];

        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else

            Response.Redirect("~/Employee/Employee/UserRight1.aspx?UserId=" + Utility.EncryptQS(UserId.ToString()) + "&EmpId=" + Utility.EncryptQS(EmpId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        try
        {
            int EmpId = -1;
            if (GridViewEmployee.FocusedRowIndex > -1)
            {
                DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
                EmpId = (int)row["EmpId"];
            }
            if (EmpId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
            }
            else
            {
                EmpManager.FindByCode(EmpId);
                if (EmpManager.Count == 1)
                {
                    if (EmpManager[0]["EmpStatus"].ToString() == "1")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                        return;
                    }
                    else
                    {
                        EmpManager[0].BeginEdit();
                        EmpManager[0]["EmpStatus"] = 1;
                        EmpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        EmpManager[0]["ModifiedDate"] = DateTime.Now;
                        EmpManager[0].EndEdit();
                        if (EmpManager.Save() > 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = " ذخیره انجام شد";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                }
            }

        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    protected void btnTempPass_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        try
        {
            int EmpId = -1;
            
         DevExpress.Web.ASPxButton f = (DevExpress.Web.ASPxButton)sender;
            if (GridViewEmployee.FocusedRowIndex > -1)
            {
                DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
                EmpId = (int)row["EmpId"];
            }
            if (EmpId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
            }
            else
            {
                LoginManager.FindByMeIdUltId(EmpId,(int)TSP.DataManager.UserType.Employee);
                if (LoginManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(LoginManager[0]["NeedTempPass"]) && Convert.ToBoolean(LoginManager[0]["NeedTempPass"]) && (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2"))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "قبلا برای این کارمند رمز یکبار عبور فعال شده است";
                        return;
                    }
                    if (!Utility.IsDBNullOrNullValue(LoginManager[0]["NeedTempPass"]) && !Convert.ToBoolean(LoginManager[0]["NeedTempPass"]) && (f.ID == "btnInActiveTempPass" || f.ID == "btnInActiveTempPass2"))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای این کارمند رمز یکبار عبور غیر فعال است";
                        return;
                    }
                    if ( Utility.IsDBNullOrNullValue(LoginManager[0]["MobileNo"]) && (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2") )
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = " برای این کارمند شماره تلفن همراه معتبر ثبت نشده است";
                        return;
                    }
                    else
                    {
                        LoginManager[0].BeginEdit();
                        if (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2")
                        LoginManager[0]["NeedTempPass"] = true;
                        if (f.ID == "btnInActiveTempPass" || f.ID == "btnInActiveTempPass2")
                            LoginManager[0]["NeedTempPass"] = false;
                        LoginManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
                        LoginManager[0]["ModifiedDate"] = DateTime.Now;
                        LoginManager[0].EndEdit();
                        if (LoginManager.Save() > 0)
                        {
                            GridViewEmployee.DataBind();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد. برای شماره تلفن همراه" + LoginManager[0]["MobileNo"].ToString() ;
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                }
            }

        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int EmpId = -1;
        string RsType = "";

        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
            RsType = ((int)TSP.DataManager.ResetPasswordType.Employee).ToString();

        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(EmpId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;     
        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewRequest =
                (TSP.WebControls.CustomAspxDevGridView)GridViewEmployee.FindDetailRowTemplateControl(GridViewEmployee.FocusedRowIndex, "GridViewRequest");
            if (GridViewRequest != null)
            {
                if (GridViewRequest.FocusedRowIndex > -1)
                {
                    int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
                    DataRow EmpReRow = GridViewRequest.GetDataRow(GridViewRequest.FocusedRowIndex);
                    int TableId = int.Parse(EmpReRow["EmpReId"].ToString());
                    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming;

                    int PostId = int.Parse(GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex)["EmpId"].ToString());
                    string GridFilterString = GridViewEmployee.FilterExpression;
                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                    "&PostId=" + Utility.EncryptQS(PostId.ToString());

                    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                        + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                        + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString())
                        + "&UrlReferrer=" + Utility.EncryptQS(Url));
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "برای پیگیری پرونده ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای پیگیری پرونده ابتدا یک درخواست را انتخاب نمائید";

            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnChangeReq_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            int EmpId = (int)row["EmpId"];
            if (CheckPermissionForRequest(EmpId))
                NextPage("Change");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnAutoUserRight_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewEmployee.FilterExpression;

        int EmpId = -1;
        int UserId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
            UserId = (int)row["LoginUserId"];
        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else

            Response.Redirect("~/Employee/Employee/AutomationUserRight.aspx?EmpId=" + Utility.EncryptQS(EmpId.ToString()) + "&UserId=" + Utility.EncryptQS(UserId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        ASPxGridViewExporter1.FileName = "Employee";
        ASPxGridViewExporter1.WriteXlsToResponse(true);
    }

    protected void btnReqDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int EmpId = -1;
        int EmpReId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEmployee.FindDetailRowTemplateControl(GridViewEmployee.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        EmpReId = int.Parse(GridRequest.GetDataRow(index0)["EmpReId"].ToString());
                        TSP.DataManager.EmployeeRequestManager ReqManager = new TSP.DataManager.EmployeeRequestManager();
                        ReqManager.FindByCode(EmpReId);
                        if (ReqManager.Count > 0)
                        {
                            if (ReqManager[0]["Type"].ToString() == "0")//درخواست اولیه
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست اولیه ثبت کارمند وجود ندارد";
                                return;
                            }
                            if (ReqManager[0]["IsConfirmed"].ToString() != "0")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            if (CheckPermitionForDelete(EmpReId))
                                Delete(EmpReId);
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از جریان کار برای شما وجود ندارد";
                            }
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            }
        }
    }
    #endregion    

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewEmployee.FocusedRowIndex <= -1)
        {
            WFUserControl.SetMsgText(" لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        DataRow EmpFileRow = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
        int EmpId = int.Parse(EmpFileRow["EmpId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming;
        int EmpReId = -1;
        TSP.WebControls.CustomAspxDevGridView GridViewRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEmployee.FindDetailRowTemplateControl(GridViewEmployee.FocusedRowIndex, "GridViewRequest");
        if (GridViewRequest == null)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید ");
            return;
        }
        if (GridViewRequest.VisibleRowCount <= 0)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        int index0 = GridViewRequest.FocusedRowIndex;
        if (index0 == -1)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        EmpReId = int.Parse(GridViewRequest.GetDataRow(index0)["EmpReId"].ToString());

        WFUserControl.PerformCallback(EmpReId, TableType, WFCode, e);
        GridViewEmployee.DataBind();
        GridViewEmployee.ExpandRow(GridViewEmployee.FocusedRowIndex);

    }

    protected void CallbackPanelPage_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {      
        CallbackPanelPage.JSProperties["cpDoPrint"] = 0;

        String[] Parameter = e.Parameter.Split(';');
        string ReqName = Parameter[0];
        switch (ReqName)
        {
            case "Print":                
                GridViewEmployee.DetailRows.CollapseAllRows();
                CallbackPanelPage.JSProperties["cpDoPrint"] = 1;
                break;          
        }
    }

    #region Grd Employee
    protected void GridViewEmployee_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewEmployee.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewEmployee.Columns["WFState"], "btnWFState");
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
    }

    protected void GridViewEmployee_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("WFTaskType") != null)
        {
            if (e.GetValue("WFTaskType").ToString() == "1" || e.GetValue("WFTaskType").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

 //   protected void GridViewEmployee_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
  //  {
        // GridViewEmployee.DataBind();
  //  }

    protected void GridViewEmployee_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewEmployee.JSProperties["cpIsPostBack"] = 1;
    }

    protected void GridViewEmployee_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();
    }

    protected void GridViewEmployee_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Grd EmployeeRequest
    protected void GridViewRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";


        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewEmployee.FindDetailRowTemplateControl(GridViewEmployee.FocusedRowIndex, "GridViewRequest");
            if (GridViewRequest != null)
            {
                DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
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
        }
    }

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["EmpId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewEmployee.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewEmployee.FocusedRowIndex = Index;
    }

    protected void GridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        string GridFilterString = GridViewEmployee.FilterExpression;

        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
        int EmpId = -1;
        int EmpReId = -1;
        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
            switch (Mode)
            {
                case "Edit":
                    EmployeeRequestManager.SelectLastVersion(EmpId, 0);
                    if (EmployeeRequestManager.Count > 0)
                    {
                        EmpReId = int.Parse(EmployeeRequestManager[0]["EmpReId"].ToString());
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کارمند انتخاب شده دارای درخواست با وضعیت معلق نمی باشد.جهت ویرایش اطلاعات درخواست تغییرات بدهید.";
                        return;
                    }
                    break;
                case "Change":
                    EmployeeRequestManager.SelectLastVersion(EmpId, 1);
                    if (EmployeeRequestManager.Count > 0)
                    {
                        EmpReId = int.Parse(EmployeeRequestManager[0]["EmpReId"].ToString());
                    }
                    else
                    {
                        EmpReId = -1;
                    }
                    break;
                case "New":
                    break;
                case "View":
                    TSP.WebControls.CustomAspxDevGridView GridViewRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEmployee.FindDetailRowTemplateControl(GridViewEmployee.FocusedRowIndex, "GridViewRequest");

                    if (GridViewRequest != null)
                    {//***Was Expanded
                        DataRow rowReq = GridViewRequest.GetDataRow(GridViewRequest.FocusedRowIndex);
                        if (rowReq == null)
                        {
                            EmployeeRequestManager.SelectLastVersion(EmpId, 1);
                            if (EmployeeRequestManager.Count > 0)
                            {
                                EmpReId = int.Parse(EmployeeRequestManager[0]["EmpReId"].ToString());
                            }
                            else
                            {
                                EmpReId = -1;
                                //this.DivReport.Visible = true;
                                //this.LabelWarning.Text = "اطلاعات مربوط به درخواست توسط کاربر دیگری تغییر یافته است.";
                                //return;
                            }
                        }
                        else
                        {
                            EmpReId = (int)rowReq["EmpReId"];
                        }
                    }
                    else
                    {//***Was not expanded                          
                        DataRow rowEmp = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
                        EmployeeRequestManager.SelectLastVersion(EmpId, 1);
                        if (EmployeeRequestManager.Count > 0)
                        {
                            EmpReId = int.Parse(EmployeeRequestManager[0]["EmpReId"].ToString());
                        }
                        else
                        {
                            EmpReId = -1;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (EmpId == -1 && Mode != "New")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
                return;
            }
            else
            {
                if (Mode == "New")
                {
                    EmpId = -1;
                    EmpReId = -1;
                    Response.Redirect("EmployeeInsert.aspx?EmpId=" + Utility.EncryptQS(EmpId.ToString()) + "&EmpReId=" + Utility.EncryptQS(EmpReId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
                }
                else if (Mode == "Change")
                {
                    EmpReId = -1;
                    Response.Redirect("EmployeeInsert.aspx?EmpId=" + Utility.EncryptQS(EmpId.ToString()) + "&EmpReId=" + Utility.EncryptQS(EmpReId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
                }
                else
                {
                    Response.Redirect("EmployeeInsert.aspx?EmpId=" + Utility.EncryptQS(EmpId.ToString()) + "&EmpReId=" + Utility.EncryptQS(EmpReId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
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

    #region WF
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

    private Boolean CheckPermissionForRequest(int EmpId)
    {
        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
        EmployeeRequestManager.SelectLastVersion(EmpId, 0);
        if (EmployeeRequestManager.Count == 0)
        {
            return true;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.کارمند انتخاب شده دارای درخواست معلق می باشد.";
            return false;
        }
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming
         , (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);

        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        //int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
        //int WfCode = (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming;
        //DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //dtState.DefaultView.RowFilter = "StateType=0";
        //if (dtState.DefaultView.Count == 1)
        //{
        //    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        //    // int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

        //    if ((Utility.GetCurrentUser_MeId() == Convert.ToInt32(dtState.Rows[0]["EmpId"]) ||  CurrentNmcId == FindNmcId(CurrentTaskId)) && CurrentNmcIdType == 0)
        //    {
        //        if (CurrentTaskCode == TaskCode)
        //            return true;

        //    }
        //}
        //return false;

    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                          //  int UserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());

                           // if (UserId == Utility.GetCurrentUser_UserId())
                           // {
                                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                if (Permission > 0)
                                    return true;
                                else
                                    return false;
                            //}
                            //else
                            //{
                            //    return false;
                            //}
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermissionForChangeReq()
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            btnChangeReq.Enabled = true;
            btnChangeReq2.Enabled = true;
        }
        else
        {
            btnChangeReq.Enabled = false;
            btnChangeReq2.Enabled = false;
        }

        this.ViewState["BtnNew"] = btnEdit.Enabled;
    }
    #endregion

    protected void Delete(int EmpReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EmployeeRequestManager ReqManager = new TSP.DataManager.EmployeeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);
        try
        {
            ReqManager.FindByCode(EmpReId);
            if (ReqManager.Count == 1)
            {
                trans.BeginSave();

                ReqManager[0].Delete();
                ReqManager.Save();

                int WfCode = (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming;
                WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, EmpReId);
                if (WorkFlowStateManager.Count > 0)
                {
                    int c = WorkFlowStateManager.Count;
                    for (int i = 0; i < c; i++)
                        WorkFlowStateManager[0].Delete();

                    WorkFlowStateManager.Save();
                }
                trans.EndSave();
                GridViewEmployee.DataBind();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }


        }
        catch (Exception err)
        {
            trans.CancelSave();

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

    private void CheckTablePermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnDisActive.Enabled = per.CanEdit;
        btnDisActive2.Enabled = per.CanEdit;
        btnEdit.Enabled = per.CanEdit;
        btnEdit1.Enabled = per.CanEdit;
        btnUserRight.Enabled = per.CanView;
        btnUserRight1.Enabled = per.CanView;
        btnNew1.Enabled = per.CanNew;
        BtnNew.Enabled = per.CanNew;
        btnView.Enabled = per.CanView;
        btnView2.Enabled = per.CanView;
        btnReset.Enabled = per.CanEdit;
        btnReset1.Enabled = per.CanEdit;
        GridViewEmployee.ClientVisible = per.CanView;
        //btnAutoUserRight.Enabled = per.CanView;
      //  btnAutoUserRight2.Enabled = per.CanView;
    }

    #region FilteringMethod
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewEmployee.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {
                string PostId = Utility.DecryptQS(Request.QueryString["PostId"].ToString());
                if (!string.IsNullOrEmpty(PostId))
                {
                    int PostKeyValue = int.Parse(PostId);

                    GridViewEmployee.DataBind();
                    Index = GridViewEmployee.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewEmployee.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewEmployee.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewEmployee.JSProperties["cpSelectedIndex"] = Index;
                        GridViewEmployee.DetailRows.ExpandRow(Index);
                        GridViewEmployee.FocusedRowIndex = Index;
                        GridViewEmployee.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    #endregion
    #endregion

}
