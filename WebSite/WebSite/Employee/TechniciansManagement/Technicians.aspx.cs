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

public partial class Employee_TechniciansManagement_Technicians : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.MemberConfirming).ToString();
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming).ToString();
            TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive1.Enabled = per.CanEdit;
            GridViewTechnician.ClientVisible = per.CanView;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            btnTracing.Enabled = per.CanView;
            btnTracing2.Enabled = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;

        }
        if (this.ViewState["BtnActive"] != null)
            this.btnActive1.Enabled = this.btnActive.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = this.btnPrint.Enabled = this.btnPrint2.Enabled = GridViewTechnician.ClientVisible =
               this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");
        DeletedColumnsName.Add("MjId");

        Session["DeletedColumnsName"] = DeletedColumnsName;

        Session["DataTable"] = GridViewTechnician.Columns;
        Session["DataSource"] = ObjdsOtherPerson;
        Session["Title"] = "کاردان ها و معماران تجربی";
        //Session["Header"] = "شرکت : " + lblOfName.Text;
        SetPageFilter();
        SetGridRowIndex();

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        // NextPage("New");
        //  Response.Redirect("TechnicianInsert.aspx?OtpId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
        Response.Redirect("TechnicianInsert.aspx?OtpId=" + Utility.EncryptQS("-1") + "&TnReId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New"));

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
            DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
            int OtpId = (int)row["OtpId"];
            TechnicianRequestManager.FindLastVerion(OtpId, 0);
            if (TechnicianRequestManager.Count == 1)
            {
                int TnReId = (int)TechnicianRequestManager[0]["TnReId"];
                if (CheckPermitionForEdit(TnReId))
                    NextPage("Edit");
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش در این مرحله از گردش کار وجود ندارد";
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
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int TnReId = -1;
        int OtpId = -1;
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
            if (row != null)
                OtpId = Convert.ToInt32(row["OtpId"]);
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
        }
        if (OtpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewTechnician.
                FindDetailRowTemplateControl(GridViewTechnician.FocusedRowIndex, "GridViewTechnicianRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    if (GridRequest.FocusedRowIndex > -1)
                    {
                        TnReId = Convert.ToInt32(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["TnReId"].ToString());
                        TSP.DataManager.TechnicianRequestManager technicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
                        technicianRequestManager.FindByCode(TnReId);
                        if (technicianRequestManager.Count > 0)
                        {
                            //if (Convert.ToInt32(technicianRequestManager[0]["Status"]) == (int)TSP.DataManager.TechnicianRequestType.SaveInfo)
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "امکان حذف درخواست اولیه ثبت نام وجود ندارد";
                            //    return;
                            //}
                            if (technicianRequestManager[0]["IsConfirmed"].ToString() != "0")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            if (TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TnReId, (int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming,
                                (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo, Utility.GetCurrentUser_UserId(),
                                (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId))
                            {
                                if (Convert.ToInt32(technicianRequestManager[0]["Status"]) == (int)TSP.DataManager.TechnicianRequestType.SaveInfo)
                                    Delete(TnReId, Convert.ToInt32(technicianRequestManager[0]["OtpId"]), true);
                                else
                                    Delete(TnReId, Convert.ToInt32(technicianRequestManager[0]["OtpId"]), false);
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از گردش کار برای شما وجود ندارد";
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

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
            int OtpId = (int)row["OtpId"];
            if (CheckPermissionForRequest(OtpId))
                NextPage("InActive");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }

        //int OtpId = -1;

        //if (GridViewTechnician.FocusedRowIndex > -1)
        //{

        //    DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
        //    OtpId = (int)row["OtpId"];

        //}
        //if (OtpId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //}
        //else
        //{
        //    InActive(OtpId);

        //}

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Technicians";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewTechnicianRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["OtpId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewTechnician.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewTechnician.FocusedRowIndex = Index;
    }

    protected void GridViewTechnician_DataBinding(object sender, EventArgs e)
    {
        // GridViewTechnician.DataBind();
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            if (GridViewTechnician.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewTechnicianRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewTechnician.FindDetailRowTemplateControl(GridViewTechnician.FocusedRowIndex, "GridViewTechnicianRequest");
                if (GridViewTechnicianRequest != null)
                {
                    DataRow row = GridViewTechnicianRequest.GetDataRow(GridViewTechnicianRequest.FocusedRowIndex);
                    int TableId = (int)row["TnReId"];
                    int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
                    int WfCode = (int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming;
                    if (e.Parameter == "Send")
                    {
                        SendDocToNextStep(TableId, WfCode, TableType);
                    }
                    else
                    {
                        SelectSendBackTask(TableType, WfCode, TableId);
                    }
                    GridViewTechnician.DataBind();
                    GridViewTechnicianRequest.DataBind();
                    GridViewTechnician.ExpandRow(GridViewTechnician.FocusedRowIndex);
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblWFWarning.ForeColor = System.Drawing.Color.Red;
                    lblWFWarning.Text = "برای ارسال پرونده به مرحله بعد ابتدا یک درخواست را انتخاب نمائید";

                }
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                lblWFWarning.Text = "ردیفی انتخاب نشده است.";
            }
        }
        catch
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblWFWarning.ForeColor = System.Drawing.Color.Red;
            lblWFWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است.";
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewTechnicianRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewTechnician.FindDetailRowTemplateControl(GridViewTechnician.FocusedRowIndex, "GridViewTechnicianRequest");
            if (GridViewTechnicianRequest != null)
            {
                int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
                DataRow EmpReRow = GridViewTechnicianRequest.GetDataRow(GridViewTechnicianRequest.FocusedRowIndex);
                int TableId = int.Parse(EmpReRow["TnReId"].ToString());
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming;

                int PostId = int.Parse(GridViewTechnicianRequest.GetDataRow(GridViewTechnicianRequest.FocusedRowIndex)["OtpId"].ToString());
                string GridFilterString = GridViewTechnicianRequest.FilterExpression;
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
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnChangeReq_Click(object sender, EventArgs e)
    {
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
            int OtpId = (int)row["OtpId"];
            if (CheckPermissionForRequest(OtpId))
                NextPage("Change");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void GridViewTechnician_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewTechnician.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewTechnician.Columns["WFState"], "btnWFState");
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

    protected void GridViewTechnicianRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewTechnician.FindDetailRowTemplateControl(GridViewTechnician.FocusedRowIndex, "GridViewTechnicianRequest");
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

    protected void GridViewTechnician_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("WFTaskType") != null)
        {
            if (e.GetValue("WFTaskType").ToString() == "1" || e.GetValue("WFTaskType").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

    protected void GridViewTechnician_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!String.IsNullOrEmpty(e.Parameters) && e.Parameters == "Print")
        {
            GridViewTechnician.DetailRows.CollapseAllRows();
            GridViewTechnician.JSProperties["cpDoPrint"] = 1;
        }
        GridViewTechnician.DataBind();
    }

    protected void GridViewTechnicianRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void GridViewTechnician_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        int OtpId = -1;
        int TnReId = -1;
        string GridFilterString = GridViewTechnician.FilterExpression;
        if (GridViewTechnician.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
            OtpId = (int)row["OtpId"];
            switch (Mode)
            {
                case "Edit":
                    TechnicianRequestManager.FindLastVerion(OtpId, 0);
                    if (TechnicianRequestManager.Count > 0)
                    {
                        TnReId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کاردان/معمارتجربی انتخاب شده دارای درخواست با وضعیت معلق نمی باشد.جهت ویرایش اطلاعات درخواست تغییرات بدهید.";
                        return;
                    }
                    break;
                case "Change":
                    TechnicianRequestManager.FindLastVerion(OtpId, 1);
                    if (TechnicianRequestManager.Count > 0)
                    {
                        TnReId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                    }
                    else
                    {
                        TnReId = -1;
                    }
                    break;
                case "InActive":
                    TechnicianRequestManager.FindLastVerion(OtpId, 1);
                    if (TechnicianRequestManager.Count > 0)
                    {
                        TnReId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                    }
                    else
                    {
                        TnReId = -1;
                    }
                    break;
                case "New":
                    break;
                case "View":
                    TSP.WebControls.CustomAspxDevGridView GridViewTechnicanRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewTechnician.FindDetailRowTemplateControl(GridViewTechnician.FocusedRowIndex, "GridViewTechnicianRequest");

                    if (GridViewTechnicanRequest != null)
                    {//***Was Expanded
                        DataRow rowReq = GridViewTechnicanRequest.GetDataRow(GridViewTechnicanRequest.FocusedRowIndex);
                        if (rowReq == null)
                        {
                            TechnicianRequestManager.FindLastVerion(OtpId, 1);
                            if (TechnicianRequestManager.Count > 0)
                            {
                                TnReId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                            }
                            else
                            {
                                TnReId = -1;
                            }
                        }
                        else
                        {
                            TnReId = (int)rowReq["TnReId"];
                        }
                    }
                    else
                    {//***Was not expanded                          
                        DataRow rowEmp = GridViewTechnician.GetDataRow(GridViewTechnician.FocusedRowIndex);
                        TechnicianRequestManager.FindLastVerion(OtpId, 1);
                        if (TechnicianRequestManager.Count > 0)
                        {
                            TnReId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                        }
                        else
                        {
                            TnReId = -1;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (OtpId == -1 && Mode != "New")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
                return;
            }
            else
            {
                if (Mode == "New")
                {
                    OtpId = -1;
                    TnReId = -1;
                }
                else if (Mode == "Change" || Mode == "InActive")
                {
                    TnReId = -1;
                }

                Response.Redirect("TechnicianInsert.aspx?OtpId=" + Utility.EncryptQS(OtpId.ToString()) + "&TnReId=" + Utility.EncryptQS(TnReId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

            }
        }
        else
        {

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void InActive(int OtpId)
    {
        TSP.DataManager.OtherPersonManager OtpManager = new TSP.DataManager.OtherPersonManager();
        try
        {
            OtpManager.FindByCode(OtpId);
            if (OtpManager.Count == 1)
            {
                if (Convert.ToBoolean(OtpManager[0]["InActive"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                    return;
                }
                else
                {
                    OtpManager[0].BeginEdit();
                    OtpManager[0]["InActive"] = 1;
                    OtpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    OtpManager[0].EndEdit();
                    if (OtpManager.Save() > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
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
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";

            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void SelectSendBackTask(int TableType, int WfCode, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();

        CallbackPanelWorkFlow.JSProperties["cpWfName"] = "";
        CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "";
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            int CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            string CurrentTaskName = dtWorkFlowState.Rows[0]["TaskName"].ToString();
            int Permission = 0;

            WorkFlowManager.FindByTableType(-1, WfCode);
            if (WorkFlowManager.Count > 0)
            {
                CallbackPanelWorkFlow.JSProperties["cpWfName"] = "گردش کار:" + WorkFlowManager[0]["WorkFlowName"].ToString();
                CallbackPanelWorkFlow.JSProperties["cpWfStateName"] = "وضعیت جاری درخواست:" + CurrentTaskName;
                if (!WorkFlowPermission.IsCurrentUserIsRequestStarter(WfCode, Utility.GetCurrentUser_LoginType(), CurrentNmcIdType, CurrentTaskCode))
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblWFWarning.ForeColor = System.Drawing.Color.Red;
                    lblWFWarning.Text = WorkFlowPermission.FindRequestErrorMsg((int)TSP.DataManager.ErrorRequest.YouCanNotSentDocToNextStep);
                    return;
                }

                Permission = WorkFlowPermission.CheckSelectSendBackTaskPermissionForSpecificWF(WfCode, TableId, CurrentTaskCode, Utility.GetCurrentUser_UserId());

                switch (Permission)
                {
                    case 0:
                        int SendBackTask = WorkFlowStateManager.CalculateSendBackTaskByWfCode(WfCode, TableId, Utility.GetCurrentUser_UserId());
                        switch (SendBackTask)
                        {
                            case (int)TSP.DataManager.WorkFlowStateManager.Errors.YouCanNotSend:
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                                lblWFWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                                break;
                            case (int)TSP.DataManager.WorkFlowStateManager.Errors.ProcessEnd:
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                                lblWFWarning.Text = "گردش کار پرونده انتخاب شده به اتمام رسیده است.";
                                break;
                            case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoTaskFind:
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                                lblWFWarning.Text = "عملیاتی برای پرونده انتخاب شده انجام نشده است.";
                                break;
                            default:
                                int WorkFlowId = int.Parse(WorkFlowManager[0]["WorkFlowId"].ToString());
                                DataTable dtSendBackTask = WorkFlowStateManager.SelectSendBackTask(SendBackTask, WorkFlowId);
                                dtSendBackTask = WorkFlowPermission.FilterSendBackDataTable(WfCode, TableId, dtSendBackTask);
                                if (dtSendBackTask.Rows.Count > 0)
                                {
                                    Session["SendBackDataTable_EmpWF"] = dtSendBackTask;
                                    cmbSendBackTask.DataSource = dtSendBackTask;
                                    cmbSendBackTask.ValueField = "TaskId";
                                    cmbSendBackTask.TextField = "TaskName";
                                    cmbSendBackTask.DataBind();
                                    cmbSendBackTask.SelectedIndex = 0;
                                    PanelSaveSuccessfully.Visible = false;
                                    PanelMain.Visible = true;
                                }
                                break;
                        }


                        break;

                    default:

                        string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(Permission);
                        if (!string.IsNullOrEmpty(ErrorMsg))
                        {
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblWFWarning.ForeColor = System.Drawing.Color.Red;
                            lblWFWarning.Text = ErrorMsg;
                        }
                        else
                        {
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblWFWarning.ForeColor = System.Drawing.Color.Red;
                            lblWFWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                        }
                        break;
                }
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                lblWFWarning.Text = "اطلاعات گردش کار تغییر یافته است.";
            }
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblWFWarning.ForeColor = System.Drawing.Color.Red;
            lblWFWarning.Text = "عملیاتی برای پرونده انتخاب شده انجام نشده است.";
        }
    }

    private void SendDocToNextStep(int TableId, int WfCode, int TableType)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        string Url = "";
        string MsgContent = "";

        DataTable dtSendBack = (DataTable)Session["SendBackDataTable_EmpWF"];
        cmbSendBackTask.DataSource = dtSendBack;
        cmbSendBackTask.ValueField = "TaskId";
        cmbSendBackTask.TextField = "TaskName";
        cmbSendBackTask.DataBind();

        int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission(WfCode, Utility.GetCurrentUser_AgentId(), TransactionManager);

        //TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        //TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        //if (WfCode == (int)TSP.DataManager.WorkFlows.SMSConfirming)
        //{
        //    TransactionManager.Add(SmsManager);
        //    TransactionManager.Add(SmsRecieverManager);
        //}

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        int CurrentTaskId = -1;
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
        }

        TransactionManager.Add(WorkFlowStateManager);

        int NmcId = FindNmcId(CurrentTaskId);
        if (NmcId > 0)
        {
            try
            {
                TransactionManager.BeginSave();
                int SendDoc = -4;
                if (chbIsSendMail.Checked)
                    SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId(), MsgContent, Url);
                else
                    SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, SelectedTaskId, txtDescription.Text, NmcId, Utility.GetCurrentUser_UserId());
                switch (SendDoc)
                {
                    case (int)TSP.DataManager.WorkFlowStateManager.Errors.CannotSendToCurrentState:
                        TransactionManager.CancelSave();
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblWFWarning.ForeColor = System.Drawing.Color.Red;
                        lblWFWarning.Text = "امکان ارسال پرونده پروژه به مرحله جاری وجود ندارد.";
                        break;
                    case (int)TSP.DataManager.WorkFlowStateManager.Errors.Erorr:
                        TransactionManager.CancelSave();
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblWFWarning.ForeColor = System.Drawing.Color.Red;
                        lblWFWarning.Text = "خطایی در ذخیره انجام شد.";
                        break;
                    case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoStateFound:
                        TransactionManager.CancelSave();
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblWFWarning.ForeColor = System.Drawing.Color.Red;
                        lblWFWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                        break;
                    case (int)TSP.DataManager.WorkFlowStateManager.Errors.NoTaskDoerFind:
                        TransactionManager.CancelSave();
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblWFWarning.ForeColor = System.Drawing.Color.Red;
                        lblWFWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                        break;
                    default:
                        int DoNextTask = 0;
                        DoNextTask = WorkFlowPermission.CheckAndDoSendDocToNextStepConditions(WfCode, TableId, SelectedTaskId, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(), Utility.GetCurrentUser_AgentId(), -1);
                        string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(DoNextTask);
                        if (DoNextTask == 0)
                        {
                            TransactionManager.EndSave();
                            lblWFWarning.ForeColor = System.Drawing.Color.Green;
                            lblWFWarning.Text = "ذخیره انجام شد.";
                            PanelMain.ClientVisible = false;
                            PanelSaveSuccessfully.ClientVisible = true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ErrorMsg))
                            {
                                TransactionManager.CancelSave();
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                                lblWFWarning.Text = ErrorMsg;
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                                lblWFWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            }
                        }
                        break;
                }
            }
            catch (Exception err)
            {
                TransactionManager.CancelSave();
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblWFWarning.ForeColor = System.Drawing.Color.Red;
                lblWFWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblWFWarning.ForeColor = System.Drawing.Color.Red;
            lblWFWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
        }
        GridViewTechnician.DataBind();
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

    private Boolean CheckPermissionForRequest(int OtpId)
    {
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TechnicianRequestManager.FindLastVerion(OtpId, 0);
        if (TechnicianRequestManager.Count == 0)
        {
            return true;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.کاردان/معمار تجربی انتخاب شده دارای درخواست معلق می باشد.";
            return false;
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
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
                            //int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            //int UserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());

                            //if (UserId == Utility.GetCurrentUser_UserId())
                            //{
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

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))// && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                //string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                //if (!string.IsNullOrEmpty(SrchFlt))
                //    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewTechnician.FilterExpression = GrdFlt;
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

                    GridViewTechnician.DataBind();
                    Index = GridViewTechnician.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewTechnician.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewTechnician.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewTechnician.JSProperties["cpSelectedIndex"] = Index;
                        GridViewTechnician.DetailRows.ExpandRow(Index);
                        GridViewTechnician.FocusedRowIndex = Index;
                    }
                }
            }
        }
        return Index;
    }

    private void Delete(int TnReId,int OtpId, Boolean SaveRequest)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicianRequestManager technicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TechniciansActivityAreasManager AreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        
        trans.Add(AreasManager);
        trans.Add(GradeManager);
        trans.Add(RequestInActivesManager);
        trans.Add(technicianRequestManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OtherPersonManager);


        try
        {
            trans.BeginSave();
            #region DeleteInActives
            RequestInActivesManager.FindByReqId(TnReId,TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechnicianRequest));
            int cntInActive = RequestInActivesManager.Count;
            for (int i = 0; i < cntInActive; i++)
            {
                RequestInActivesManager[0].Delete();
                RequestInActivesManager.Save();
                RequestInActivesManager.DataTable.AcceptChanges();
            }
            #endregion
            int WfCode = (int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, TnReId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }
            //delete all related record in tblTechniciansActivityAreas-tblTechniciansActivityAreas-[DocOff.MemberAcceptedGrade]-[tblAttachments]-tblTechnicianRequest
            technicianRequestManager.DeleteAll(TnReId);
            if (SaveRequest)
            {
                OtherPersonManager.FindByCode(OtpId);
                if (OtherPersonManager.Count != 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    trans.CancelSave();
                    return;
                }
                OtherPersonManager[0].Delete();
                OtherPersonManager.Save();
            }
            trans.EndSave();
            GridViewTechnician.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
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


    #endregion

}
