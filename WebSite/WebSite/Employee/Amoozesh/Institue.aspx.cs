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

public partial class Employee_Amoozesh_Institue : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DivReport.Visible = true;

            Session["SendBackDataTable_Insetitue"] = "";
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.InstitueConfirming).ToString();

            TSP.DataManager.Permission per = TSP.DataManager.InstitueManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewInstitue.Visible = per.CanView;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        //        string Script = "  <script language=''javascript''>function SetError(result) {if(result!= null){";
        //        Script += "document.getElementById(''<%=DivReport.ClientID%>'').style.visibility='visible';";
        //        Script += "document.getElementById(''<%=DivReport.ClientID%>'').style.display='block';  //='visible';";
        //        Script += @"document.getElementById('<%=LabelWarning.ClientID%>').innerText= result;
        //                }}</script>";

        //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetError", Script);

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    #region //********************Buttons****************************
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddInstitues.aspx?InsId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New")+ "&InsId="+Utility.EncryptQS("-1") + "&InsCId=" + Utility.EncryptQS("-1"));

        //Response.Redirect("AddInstitue.aspx?PageMode=" +Utility.EncryptQS("New"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int InsId = -1; int InsCId = -2;
        if (GridViewInstitue.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewInsCertificate = (TSP.WebControls.CustomAspxDevGridView)GridViewInstitue.FindDetailRowTemplateControl(GridViewInstitue.FocusedRowIndex, "GridViewInsCertificate");
            if (GridViewInsCertificate != null)
            {
                if (GridViewInsCertificate.FocusedRowIndex > -1)
                {
                    DataRow rowCert = GridViewInsCertificate.GetDataRow(GridViewInsCertificate.FocusedRowIndex);
                    InsCId = (int)rowCert["InsCId"];
                }
            }
            else
            {
                SetMessage("ابتدا یک درخواست را انتخاب نمائید");
                return;
            }

            DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            InsId = (int)row["InsId"];
            InsCId = int.Parse(row["InsCId"].ToString());
        }
        if (InsId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;

        }
        CheckWorkFlowPermission(InsCId, InsId);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int InsId = -1; int InsCId = -2;
        if (GridViewInstitue.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewInsCertificate = (TSP.WebControls.CustomAspxDevGridView)GridViewInstitue.FindDetailRowTemplateControl(GridViewInstitue.FocusedRowIndex, "GridViewInsCertificate");
            if (GridViewInsCertificate != null)
            {
                if (GridViewInsCertificate.FocusedRowIndex > -1)
                {
                    DataRow rowCert = GridViewInsCertificate.GetDataRow(GridViewInsCertificate.FocusedRowIndex);
                    InsCId = (int)rowCert["InsCId"];
                }
            }
            else
            {
                SetMessage("ابتدا یک درخواست را انتخاب نمائید");
                return;
            }

            DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            InsId = (int)row["InsId"];
            InsCId = int.Parse(row["InsCId"].ToString());
        }
        if (InsId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;

        }
        Response.Redirect("AddInstitues.aspx?InsId=" + Utility.EncryptQS(InsId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&InsCId=" + Utility.EncryptQS(InsCId.ToString()));

    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("style", "display:block");

        if (GridViewInstitue.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.Institue;
            DataRow InstitueRow = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            int TableId = int.Parse(InstitueRow["InsId"].ToString());

            string GridFilterString = GridViewInstitue.FilterExpression;

            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                "&PostId=" + Utility.EncryptQS(TableId.ToString());

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                   + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        try
        {
            int InsId = -1;
            if (GridViewInstitue.FocusedRowIndex > -1)
            {
                DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
                InsId = (int)row["InsId"];
            }
            if (InsId == -1)
            {
                SetMessage("لطفاً برای تمدید گواهینامه استاد ابتدا یک رکورد را انتخاب نمائید.");
            }

            TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
            DataTable dtInsCertificate = InstitueCertificateManager.SelectLastVersion(InsId, -1);
            if (dtInsCertificate.Rows.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            if (Convert.ToInt32(dtInsCertificate.Rows[0]["IsConfirmed"]) == 0)
            {
                SetMessage("امکان ثبت درخواست جدید وجود ندارد.برای موسسه انتخاب شده درخواست در جریان وجود دارد");
                return;
            }
            if (Utility.IsDBNullOrNullValue(dtInsCertificate.Rows[0]["EndDate"]))
            {
                string CrtEndDate = dtInsCertificate.Rows[0]["EndDate"].ToString();
                Utility.Date objDate = new Utility.Date(CrtEndDate);
                string LastMonth = objDate.AddMonths(-1);
                string Today = Utility.GetDateOfToday();
                int IsDocExp = string.Compare(Today, LastMonth);
                if (IsDocExp <= 0)
                {
                    SetMessage("تاریخ اعتبار گواهینامه انتخاب شده به پایان نرسیده است.");
                    return;
                }
            }

            Response.Redirect("AddInstitues.aspx?InsId=" + Utility.EncryptQS(InsId.ToString()) + "&PageMode=" + Utility.EncryptQS("Revival") + "&InsCId=" + Utility.EncryptQS(dtInsCertificate.Rows[0]["InsCId"].ToString()));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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
                    this.LabelWarning.Text = "خطایی انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی انجام گرفته است";
            }
        }
    }

    protected void btnChangeCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            int InsId = -1;
            if (GridViewInstitue.FocusedRowIndex > -1)
            {
                DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
                InsId = (int)row["InsId"];
            }
            if (InsId == -1)
            {
                SetMessage("لطفاً برای تمدید گواهینامه استاد ابتدا یک رکورد را انتخاب نمائید.");
            }

            TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
            DataTable dtInsCertificate = InstitueCertificateManager.SelectLastVersion(InsId, -1);
            if (dtInsCertificate.Rows.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            if (Convert.ToInt32(dtInsCertificate.Rows[0]["IsConfirmed"]) == 0)
            {
                SetMessage("امکان ثبت درخواست جدید وجود ندارد.برای موسسه انتخاب شده درخواست در جریان وجود دارد");
                return;
            }
            Response.Redirect("AddInstitues.aspx?InsId=" + Utility.EncryptQS(InsId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change") + "&InsCId=" + Utility.EncryptQS(dtInsCertificate.Rows[0]["InsCId"].ToString()));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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
                    this.LabelWarning.Text = "خطایی انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی انجام گرفته است";
            }
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int InsId = -1;
        string RsType = "";

        if (GridViewInstitue.FocusedRowIndex > -1)
        {
            DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            InsId = (int)row["InsId"];
            RsType = ((int)TSP.DataManager.ResetPasswordType.Institue).ToString();

        }
        if (InsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(InsId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Institue";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnDeleteReq_Click(object sender, EventArgs e)
    {
        int InsCId = -1;
        int InsId = -1;
        if (GridViewInstitue.FocusedRowIndex < 0)
        {
            SetMessage("ابتدا یک موسسه را انتخاب نمایید.");
            return;
        }
        DataRow InsRow = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
        if (InsRow == null)
        {
            return;
        }

        TSP.WebControls.CustomAspxDevGridView GridInsCertificate = (TSP.WebControls.CustomAspxDevGridView)GridViewInstitue.FindDetailRowTemplateControl(GridViewInstitue.FocusedRowIndex, "GridViewInsCertificate");
        if (GridInsCertificate == null)
        {
            SetMessage("ابتدا یک درخواست را انتخاب نمایید.");
            return;
        }
        if (GridInsCertificate.VisibleRowCount < 0)
        {
            SetMessage("ابتدا یک درخواست را انتخاب نمایید.");
            return;
        }
        int index0 = GridInsCertificate.FocusedRowIndex;
        if (index0 < 0)
        {
            SetMessage("ابتدا یک درخواست را انتخاب نمایید.");
            return;
        }
        InsCId = int.Parse(GridInsCertificate.GetDataRow(index0)["InsCId"].ToString());
        InsId = int.Parse(GridInsCertificate.GetDataRow(index0)["InsId"].ToString());
        if (Convert.ToInt32(GridInsCertificate.GetDataRow(index0)["IsConfirmed"]) != 0)
        {
            SetMessage("امکان حذف درخواست پاسخ داده شده وجود ندارد.");
            return;
        }
        if (CheckPermitionForDelete(InsCId))
        {
            DeleteRequest(InsCId, InsId);
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        SetActiveStaus(true);
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        SetActiveStaus(false);
    }
    #endregion

    #region //***************************Grid*******************************************************

    protected void GridViewInstitue_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    GridViewInstitue.JSProperties["cpPrint"] = 1;
                    GridViewInstitue.DetailRows.CollapseAllRows();
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewInstitue.Columns;
                    Session["DataSource"] = OdbInstitue;

                    Session["Title"] = "موسسات";
                    break;
                case "Search":
                    if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
                        OdbInstitue.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
                    if (!string.IsNullOrEmpty(txtEndDateTo.Text))
                        OdbInstitue.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
                    if (!string.IsNullOrEmpty(txtInsName.Text))
                        OdbInstitue.SelectParameters["InsName"].DefaultValue = txtInsName.Text.Trim();
                    if (!string.IsNullOrEmpty(txtRegNo.Text))
                        OdbInstitue.SelectParameters["InsRegNo"].DefaultValue = txtRegNo.Text.Trim();
                    GridViewInstitue.DataBind();
                    break;
                case "Clear":
                    txtEndDateFrom.Text = "";
                    txtEndDateTo.Text = "";
                    txtInsName.Text = "";
                    txtRegNo.Text = "";
                    OdbInstitue.SelectParameters["EndDateFrom"].DefaultValue = "1";
                    OdbInstitue.SelectParameters["EndDateTo"].DefaultValue = "2";
                    OdbInstitue.SelectParameters["InsName"].DefaultValue = "%";
                    OdbInstitue.SelectParameters["InsRegNo"].DefaultValue = "%";
                    GridViewInstitue.DataBind();
                    break;
            }
        }
        else
            GridViewInstitue.DataBind();
    }

    protected void CustomAspxDevGridView1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["InsId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewInstitue_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewInstitue.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewInstitue_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("TaskCode") != null)
        {
            if (!string.IsNullOrEmpty(e.GetValue("TaskCode").ToString()))
            {
                int TaskCode = int.Parse(e.GetValue("TaskCode").ToString());
                int TaskCodeConfirmInstitue = (int)TSP.DataManager.WorkFlowTask.ConfirmInstitueAndEndProccess;
                int TaskCodeRejectInstitue = (int)TSP.DataManager.WorkFlowTask.RejectInstitueAndEndProccess;
                int TaskCodesettlementAgent = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfiringInstitue;
                if (TaskCode != TaskCodeConfirmInstitue && TaskCode != TaskCodeRejectInstitue && TaskCode != TaskCodesettlementAgent)
                    e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
            }
        }
    }

    protected void GridViewInstitue_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        #region IsExpired
        if (e.DataColumn.Name == "IsExpired")
        {

            //btnIsExpired
            DevExpress.Web.ASPxImage btnIsExpired = (DevExpress.Web.ASPxImage)GridViewInstitue.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewInstitue.Columns["IsExpired"], "btnIsExpired");
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

        #region WF
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewInstitue.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewInstitue.Columns["WFState"], "btnWFState");
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

    protected void GridViewInsCertificate_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewInsCertificate_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region//**********************************Call Backs************************************************

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewInstitue.FocusedRowIndex > -1)
        {
            DataRow InstitueRow = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            int TableId = int.Parse(InstitueRow["InsCId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.Institue;
            int WfCode = (int)TSP.DataManager.WorkFlows.InstitueConfirming;
            WFUserControl.PerformCallback(TableId, TableType, WfCode, e);
            GridViewInstitue.DataBind();
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        switch (e.Parameter)
        {
            case "Clear":
                txtEndDateFrom.Text = "";
                txtEndDateTo.Text = "";
                txtInsName.Text = "";
                txtRegNo.Text = "";
                OdbInstitue.SelectParameters["EndDateFrom"].DefaultValue = "1";
                OdbInstitue.SelectParameters["EndDateTo"].DefaultValue = "2";
                OdbInstitue.SelectParameters["InsName"].DefaultValue = "%";
                OdbInstitue.SelectParameters["InsRegNo"].DefaultValue = "%";
                GridViewInstitue.DataBind();
                break;
        }

    }
    #endregion

    #endregion

    #region Methods

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    private void CheckWorkFlowPermission(int InsCId, int InsId)
    {
        CheckWorkFlowPermissionForSave(InsCId, InsId);
    }

    private void CheckWorkFlowPermissionForSave(int InsCId, int InsId)
    {
        //****TableId
        //int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Institue;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        int CurrentTaskCode = -1;
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, InsCId);
        if (dtWorkFlowState.Rows.Count <= 0)
        {
            SetMessage("امکان ویرایش اطلاعات وجود ندارد");
        }
        CurrentTaskCode = Convert.ToInt32(dtWorkFlowState.Rows[0]["TaskCode"]);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        if (CurrentTaskCode != TaskCode)
        {
            SetMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد");
            return;
        }
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, InsCId, TaskCode, Utility.GetCurrentUser_UserId());
        if (Permission <= 0)
        {
            SetMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد");
            return;
        }
        Response.Redirect("AddInstitues.aspx?InsId=" + Utility.EncryptQS(InsId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&InsCId=" + Utility.EncryptQS(InsCId.ToString()));
        //int SaveInstitueWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        //WorkFlowTaskManager.FindByTaskCode(SaveInstitueWorkCode);
        //TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //TaskDoerManager.FindByTaskId(TaskId);

        //if (TaskDoerManager.Count > 0)
        //{
        //int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
        //NezamMemberChartManager.FindByNcId(NcId);

        //int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

        //LoginManager.FindByMeIdUltId(EmpId, 4);
        //if (LoginManager.Count > 0)
        //{
        //  int userId = int.Parse(LoginManager[0]["UserId"].ToString());
        //  int CurrentUserId = Utility.GetCurrentUser_UserId();
        //  string PageMode = Utility.DecryptQS(HiddenFieldTeacher["PageMode"].ToString());
        // if (CurrentUserId == userId)
        //{
        
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد";
        //}
        //}
        //else
        //{
        //    this.DivReport.Visible = true;

        //    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد";
        //}
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد";
        //}

    }

    private void SetMessage(string Message)
    {
        //this.DivReport.Visible = true;
        this.DivReport.Attributes.Add("style", "display:block");
        this.LabelWarning.Text = Message;
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        int TableType = (int)TSP.DataManager.TableCodes.Institue;
        int WfCode = (int)TSP.DataManager.WorkFlows.InstitueConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        dtState.DefaultView.RowFilter = "StateType=" + ((int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep).ToString();
        if (dtState.DefaultView.Count != 1)
        {
            SetMessage("تنها درخواست هایی که به سایر مراحل گردش کار ارسال نشده باشند قابل حذف می باشند");
            return false;
        }
        int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        int CurrentUserId = int.Parse(dtState.Rows[0]["UserId"].ToString());

        if (CurrentUserId != Utility.GetCurrentUser_UserId())
        {
            SetMessage("تنها ثبت کننده درخواست قادر به حذف درخواست می باشد.");
            return false;
        }
        if (CurrentTaskCode != TaskCode)
        {
            SetMessage("تنها در مرحله ثبت اطلاعات قادر به حذف درخواست می باشید.");
            return false;
        }
        return true;
    }

    private void DeleteRequest(int InsCId, int InsId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TransactionManager.Add(InstitueManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            TransactionManager.BeginSave();
            InstitueManager.DeleteRequest(InsCId, InsId);

            int WfCode = (int)TSP.DataManager.WorkFlows.InstitueConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, InsCId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }
            TransactionManager.EndSave();
            GridViewInstitue.DataBind();
            SetMessage("حذف درخواست با موفقیت انجام شد");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            Utility.SaveWebsiteError(err);
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
                SetMessage("خطایی در حذف انجام گرفته است");
            }
        }
    }
    private void SetActiveStaus(Boolean InActive)
    {
        try
        {
            if (GridViewInstitue.FocusedRowIndex <= -1)
            {
                SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            DataRow row = GridViewInstitue.GetDataRow(GridViewInstitue.FocusedRowIndex);
            if (Convert.ToInt16(row["IsConfirmed"]) == 0)
            {
                SetMessage("به دلیل وجود درخواست در جریان برای موسسه انتخاب شده ، امکان غیرفعال کردن وجود ندارد.");
                return;
            }
            int InsId = (int)row["InsId"];

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(InsId);
            if (InstitueManager.Count != 1)
            {
                SetMessage("خطایی در ذخیره انجام شد");
            }
            InstitueManager[0].BeginEdit();
            InstitueManager[0]["InActive"] = InActive;
            InstitueManager[0].EndEdit();
            InstitueManager.Save();
            SetMessage("ذخیره انجام شد");
            GridViewInstitue.DataBind();
        }
        catch (Exception err)
        {
            SetMessage("خطایی در ذخیره انجام شد");
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion

}
