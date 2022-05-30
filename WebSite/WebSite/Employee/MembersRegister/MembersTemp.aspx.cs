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
using System.Data;

using System.Collections.Generic;
public partial class Employee_MembersRegister_MembersTemp : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        GridViewMember.JSProperties["cpIsPostBack"] = 0;
        if (!IsPostBack)
        {
            SetHelpAddress();
            #region Reset JSProperties
            CallbackPanelPage.JSProperties["cpDoPrintCard"] = "";
            CallbackPanelPage.JSProperties["cpPrintCardPath"] = "";
            CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 0;
            CallbackPanelPage.JSProperties["cpPrintRequestCardPath"] = "";
            //  CallbackPanelPage.JSProperties["cpCall"] = 1;
            GridViewMember.JSProperties["cpIsPostBack"] = 1;
            GridViewMember.JSProperties["cpSelectedIndex"] = 0;
            GridViewMember.JSProperties["cpIsReturn"] = 0;
            #endregion
            hiddenFieldIsVisible["IsVisible"] = 0;
            #region Reset Sessions
            //Session["DeletedDetailColumnsName"] = null;
            //Session["DataTableDetail"] = null;
            //Session["DataSourceDetail"] = null;
            //Session["GridDetailName"] = null;
            Session["SendBackDataTable_MeConf"] = "";
            #endregion

            #region Check User Permission
            TSP.DataManager.Permission per = TSP.DataManager.MemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission per2 = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            TSP.DataManager.Permission perMeExport = TSP.DataManager.MemberManager.GetUserPermissionForExportExcelMembers(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perMePrint = TSP.DataManager.MemberManager.GetUserPermissionForPrintMemberSearch(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perTempMeCardPrint = TSP.DataManager.MemberManager.GetUserPermissionForPrintTempMemberCardPrint(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perCardRequest = TSP.DataManager.MemberManager.GetUserPermissionForMemberCardRequest(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnPrintCard.Enabled = btnPrintCard2.Enabled = perTempMeCardPrint.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = perMeExport.CanView;
            btnPrint.Enabled = btnPrint2.Enabled = perMePrint.CanView;
            btnPrintCardRequest.Enabled = btnPrintCardRequest2.Enabled = perCardRequest.CanView;

            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnReqEdit.Enabled = btnReqEdit1.Enabled = per.CanEdit;

            btnShowInputForm.Enabled = per.CanView;
            btnShowInputForm.Enabled = per.CanView;

            btnReset.Enabled = per.CanNew;
            btnReset1.Enabled = per.CanNew;
            GridViewMember.ClientVisible = per.CanView;
            btnSearch.Enabled = per.CanView;
            btnTracing.Enabled = per.CanView;
            btnTracing2.Enabled = per.CanView;
            btnReqNew.Enabled = per2.CanNew;
            btnReqNew1.Enabled = per2.CanNew;
            btnReqView.Enabled = per.CanView;
            btnReqView1.Enabled = per.CanView;
            btnReqDelete.Enabled = per.CanDelete;
            btnReqDelete1.Enabled = per.CanDelete;
            #endregion
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.MemberConfirming).ToString();

            drdMajor.DataBind();
            drdMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            this.ViewState["BtnShowInputForm"] = btnShowInputForm.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnTracing"] = btnTracing.Enabled;
            this.ViewState["GridView"] = GridViewMember.ClientVisible;
            this.ViewState["BtnReset"] = btnReset.Enabled;

            this.ViewState["BtnNewReq"] = btnReqNew.Enabled;
            this.ViewState["BtnViewReq"] = btnReqView.Enabled;
            this.ViewState["BtnEditReq"] = btnReqEdit.Enabled;
            this.ViewState["BtnDeleteReq"] = btnReqDelete.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        string script = "<script> function CheckDate() { var FromDate = document.getElementById('" + txtFromDate.ClientID + "').value;";
        script += "var ToDate = document.getElementById('" + txtToDate.ClientID + "').value;";
        script += " if(ToDate<FromDate && ToDate!='') return -1;  else     return 1;} </script>";

        script += @"<SCRIPT language='javascript'>
                function SetEmpty()
                {             
                txtFirstName.SetText('');
                txtLastName.SetText('');
                txtMobileNo.SetText('');
                drdMajor.SetSelectedIndex(0);
                CmbTask.SetSelectedIndex(0);
                CmbReqType.SetSelectedIndex(0);
                document.getElementById('" + txtFromDate.ClientID + @"').value = '';
                document.getElementById('" + txtToDate.ClientID + @"').value = '';
                document.getElementById('" + txtReqCreateDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtReqCreateDateTo.ClientID + @"').value = '';
                }
                </SCRIPT>";
        script += @"<SCRIPT language='javascript'> function CheckSearch() { var FromDateSearch = document.getElementById('" + txtFromDate.ClientID + "').value;";
        script += "var ToDateSearch = document.getElementById('" + txtToDate.ClientID + "').value;";
        script += "var ReqFromDateSearch = document.getElementById('" + txtReqCreateDateFrom.ClientID + "').value;";
        script += "var ReqToDateSearch = document.getElementById('" + txtReqCreateDateTo.ClientID + "').value;";
        script += "if (txtMeId.GetText() == '' && txtFirstName.GetText() == '' && txtLastName.GetText() == '' && txtMobileNo.GetText() == '' && drdMajor.GetSelectedIndex() == 0 && CmbReqType.GetSelectedIndex() == 0 && CmbRequester.GetSelectedIndex() == 0 && txtFollowCode.GetText() == '' && FromDateSearch == '' && ToDateSearch == ''  && ReqFromDateSearch=='' && ReqToDateSearch=='' && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtFirstName.SetText(''); txtLastName.SetText(''); txtMobileNo.SetText('');
                    drdMajor.SetSelectedIndex(0); CmbReqType.SetSelectedIndex(0); CmbRequester.SetSelectedIndex(0); txtFollowCode.SetText(''); CmbTask.SetSelectedIndex();
                    document.getElementById('" + txtFromDate.ClientID + "').value = '';";
        script += "document.getElementById('" + txtReqCreateDateFrom.ClientID + "').value = '';";
        script += "document.getElementById('" + txtReqCreateDateTo.ClientID + "').value = '';";
        script += "document.getElementById('" + txtToDate.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);

        SetPageFilter();
        SetGridRowIndex();



        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnShowInputForm"] != null)
            this.btnShowInputForm.Enabled = this.btnShowInputForm2.Enabled = (bool)this.ViewState["BtnShowInputForm"];
        if (this.ViewState["BtnView"] != null)
            this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnTracing"];
        if (this.ViewState["GridView"] != null)
            this.GridViewMember.ClientVisible = (bool)this.ViewState["GridView"];
        if (this.ViewState["BtnReset"] != null)
            this.btnReset.Enabled = this.btnReset1.Enabled = (bool)this.ViewState["BtnReset"];
        if (this.ViewState["BtnNewReq"] != null)
            this.btnReqNew.Enabled = this.btnReqNew1.Enabled = (bool)this.ViewState["BtnNewReq"];
        if (this.ViewState["BtnViewReq"] != null)
            this.btnReqView.Enabled = this.btnReqView1.Enabled = (bool)this.ViewState["BtnViewReq"];
        if (this.ViewState["BtnEditReq"] != null)
            this.btnReqEdit.Enabled = this.btnReqEdit1.Enabled = (bool)this.ViewState["BtnEditReq"];
        if (this.ViewState["BtnDeleteReq"] != null)
            this.btnReqDelete.Enabled = this.btnReqDelete1.Enabled = (bool)this.ViewState["BtnDeleteReq"];
        if (this.ViewState["btnExportExcel"] != null)
            btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    #region btn Click
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("MemberRegister.aspx?MeId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

        Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS("")
            + "&PageMode=" + Utility.EncryptQS("NewMe") + "&Pt=" + Utility.EncryptQS("2") + "&TMe=" + Utility.EncryptQS("1")
            /////////////// + "&TP=" + Utility.EncryptQS("1")
            + "&MeId=" + Utility.EncryptQS("-1"));

    }

    protected void btnShowInputForm_Click(object sender, EventArgs e)
    {
        int MeId = -1;
        int IsMeTemp = -1;
        int UltId = -1;

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            if (row != null)
            {
                MeId = (int)row["MeId"];
                if (!Utility.IsDBNullOrNullValue(row["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                else
                    IsMeTemp = 0;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطايي در صفحه رخ داده است.صفحه را دوباره بارگذاري نماييد.";
                return;
            }

            UltId = (IsMeTemp < 1) ? (int)TSP.DataManager.UserType.Member : (int)TSP.DataManager.UserType.TemporaryMembers;

        }
        int UserId = 0;
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByMeIdUltId(MeId, UltId);
        UserId = Convert.ToInt32(LoginManager[0]["UserId"]);

        Response.Redirect("~/Employee/Management/FormBuilder/InputFormsRelatedParts.aspx?Tt=" + Utility.EncryptQS(((int)TSP.DataManager.TableType.MemberManagement).ToString())
            + "&User=" + Utility.EncryptQS(UserId.ToString()) + "&UrlReferrer=~/Employee/MembersRegister/Members.aspx");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Member";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        int MeId = -1;
        int IsMeTemp = -1;
        string RsType = "";

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            if (row != null)
            {
                MeId = (int)row["MeId"];
                if (!Utility.IsDBNullOrNullValue(row["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                else
                    IsMeTemp = 0;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }

            RsType = (IsMeTemp < 1) ? ((int)TSP.DataManager.ResetPasswordType.Member).ToString() : ((int)TSP.DataManager.ResetPasswordType.TempMember).ToString();

        }
        if (MeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(MeId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));
    

    }

    protected void btnTracing_Click()
    {
        if (GridViewMember.FocusedRowIndex > -1)
        {
            string GridFilterString = GridViewMember.FilterExpression;
            string SearchFilterString = GenerateFilterString();

            TSP.WebControls.CustomAspxDevGridView GridRequest =
                (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataRow Row = GridRequest.GetDataRow(GridRequest.FocusedRowIndex);
                int TableId = int.Parse(Row["MReId"].ToString());
                int MeId = int.Parse(Row["MeId"].ToString());

                string UserName = "";
                int IsMeTemp = 0;
                if (!Utility.IsDBNullOrNullValue(Row["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(Row["IsMeTemp"]);

                if (IsMeTemp == 0)
                    UserName = MeId.ToString();
                else if (IsMeTemp == 1)
                {
                    UserName = "M" + MeId.ToString();

                }

                String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                    "&PostId=" + Utility.EncryptQS(UserName);

                //  CallbackPanelPage.JSProperties["cpCall"] = 1;
                ASPxWebControl.RedirectOnCallback("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" +
                     Utility.EncryptQS(TableId.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید.";
            }
        }
        else
        {
            //CallbackPanelPage.JSProperties["cpCall"] = 0;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید.";
        }
        GridViewMember.DataBind();
    }
    #endregion

    #region Grids
    #region GridViewRequest
    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["MemberId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MeId");// (sender as ASPxGridView).GetMasterRowKeyValue();
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("IsMeTemp")))
            Session["IsMeTemp"] = (sender as ASPxGridView).GetMasterRowFieldValues("IsMeTemp");
       // else
         //   Session["IsMeTemp"] = -1;
        int Index = GridViewMember.FindVisibleIndexByKeyValue((sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewMember.FocusedRowIndex = Index;
    }

    //protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "MembershipDate" || e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate" || e.DataColumn.FieldName == "MeNo")
    //        e.Cell.Style["direction"] = "ltr";

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
    //        if (GridViewRequest == null)
    //            return;
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            string WFName = "";
    //            if (!Utility.IsDBNullOrNullValue(e.GetValue("WorkFlowName")))
    //            {
    //                WFName = e.GetValue("WorkFlowName").ToString();
    //            }

    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            }

    //            if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            }
    //            else
    //            {
    //            }
    //        }
    //    }
    //}

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "AnswerDate")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "MeNo")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "MembershipDate")
            e.Editor.Style["direction"] = "ltr";

    }
    #endregion

    #region GridViewMember
    protected void GridViewMember_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        GridViewMember.ExpandRow(GridViewMember.FocusedRowIndex);

        GridViewMember.DataBind();
    }

    protected void GridViewMember_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("PendingRequestCount") != null)
        {
            if (Convert.ToInt32(e.GetValue("PendingRequestCount").ToString()) != 0)
            {
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
            }
        }
        //if (e.GetValue("IsConfirm") != null)
        //{
        //    if (e.GetValue("IsConfirm").ToString() == "0")
        //    {
        //        e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        //    }
        //}
        if (e.GetValue("MrsId") != null)
        {
            switch (Convert.ToInt32(e.GetValue("MrsId")))
            {
                case (int)TSP.DataManager.MembershipRegistrationStatus.Pending:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                case (int)TSP.DataManager.MembershipRegistrationStatus.Dead:
                    e.Row.ForeColor = System.Drawing.Color.DarkOrange;

                    break;

                case (int)TSP.DataManager.MembershipRegistrationStatus.NotConfirmed:
                    e.Row.ForeColor = System.Drawing.Color.Gray;

                    break;

                case (int)TSP.DataManager.MembershipRegistrationStatus.ReturnToCurrentProvince:
                    e.Row.ForeColor = System.Drawing.Color.Black;

                    break;

                case (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince:
                    e.Row.ForeColor = System.Drawing.Color.Green;

                    break;
                case (int)TSP.DataManager.MembershipRegistrationStatus.Fired:
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    break;

                case (int)TSP.DataManager.MembershipRegistrationStatus.Cancel:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;

            }
        }
        //if (e.Row. == null)
        //{
        //    if (e.RowType == GridViewRowType.Data || e.RowType == GridViewRowType.Filter)
        //    {
        //        e.Row.Cells[25].Style["direction"] = "ltr";
        //        e.Row.Cells[8].Style["direction"] = "ltr";
        //        e.Row.Cells[9].Style["direction"] = "ltr";


        //    }
        //}


    }

    //protected void GridViewMember_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "MeNo")
    //        e.Cell.Style["direction"] = "ltr";

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMember.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMember.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            string WFName = "";
    //            if (!Utility.IsDBNullOrNullValue(e.GetValue("WorkFlowName")))
    //            {
    //                WFName = e.GetValue("WorkFlowName").ToString();
    //            }

    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            }

    //            if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = WFName + ": " + e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            }
    //            else
    //            {
    //            }
    //        }
    //    }
    //}

    protected void GridViewMember_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";

        if (e.Column.FieldName == "MeNo")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMember_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewMember.JSProperties["cpIsPostBack"] = 1;
        GridViewMember.JSProperties["cpIsVisible"] = 0;
        hiddenFieldIsVisible["IsVisible"] = 0;
        GridViewMember.DetailRows.CollapseAllRows();
    }
    #endregion
    #endregion

    #region Callbacks
    protected void CallbackPanelPage_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelPage.JSProperties["cpDoPrintCard"] = "";
        CallbackPanelPage.JSProperties["cpPrintCardPath"] = "";

        CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 0;
        CallbackPanelPage.JSProperties["cpPrintRequestCardPath"] = "";
        CallbackPanelPage.JSProperties["cpDoPrint"] = 0;

        String[] Parameter = e.Parameter.Split(';');
        string ReqName = Parameter[0];
        switch (ReqName)
        {
            case "btnReqNew":
                btnReqNew_Click();
                break;
            case "btnReqView":
                btnReqView_Click();
                break;
            case "btnReqEdit":
                btnReqEdit_Click();
                break;
            case "btnTracing":
                btnTracing_Click();
                break;
            case "btnReqDelete":
                btnReqDelete_Click();
                GridViewMember.DataBind();
                break;
            case "Print":
                Search();
                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("WFState");
                DeletedColumnsName.Add("MrsId");
                DeletedColumnsName.Add("AgentId");

                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewMember.Columns;
                Session["DataSource"] = ObjdsMembers;
                Session["Title"] = "اعضای حقیقی موقت";
                GridViewMember.DetailRows.CollapseAllRows();
                CallbackPanelPage.JSProperties["cpDoPrint"] = 1;
                break;
            case "PrintCard":
                #region PrintCard
                try
                {
                    DataRow row = GridViewMember.GetDataRow(Convert.ToInt32(Parameter[1]));
                    int MeId = (int)row["MeId"];
                    int IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                    if (IsMeTemp == 1)
                    {
                        //   CallbackPanelPage.JSProperties["cpCall"] = 0;
                        ShowMessage("امکان چاپ وجود ندارد. فرد انتخاب شده از اعضای موقت می باشد");
                        return;
                    }
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                    TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        int MrsId = Convert.ToInt32(row["MrsId"]);
                        if (MrsId != (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
                        {
                            ShowMessage("تنها امکان درخواست چاپ کارت برای اعضای درجریان وجود دارد");
                            return;
                        }

                        //MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
                        //{
                        //    if (MemberRequestManager.Count >0)
                        //    {
                        //        ShowMessage("امکان چاپ وجود ندارد.تنها در وضعیت تایید نشده امکان چاپ کارت ورود به جلسه وجود دارد.");
                        //        return;
                        //    }
                        //    //if (Convert.ToInt32(MemberRequestManager[0]["IsCreated"]) == 0)
                        //    //{
                        //    //    ShowMessage("امکان چاپ وجود ندارد.تنها در وضعیت تایید نشده امکان چاپ کارت ورود به جلسه وجود دارد.");
                        //    //    return;
                        //    //}
                        //    //if (Convert.ToInt32(MemberRequestManager[0]["IsConfirm"]) != 0)
                        //    //{
                        //    //    ShowMessage("امکان چاپ وجود ندارد.تنها در وضعیت تایید نشده امکان چاپ کارت ورود به جلسه وجود دارد.");
                        //    //    return;
                        //    //}
                        //}



                        if (Utility.IsZeroInvoiceCheck())
                        {
                            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AccId"]))
                            {
                                int AccId = int.Parse(MemberManager[0]["AccId"].ToString());
                                decimal Balance = AccManager.GetAccountBalance(AccId, Utility.GetDateOfToday());
                                if (Balance != 0)
                                {
                                    ShowMessage("امکان چاپ وجود ندارد.مانده حساب عضو مورد نظر صفر نمی باشد.");
                                    return;
                                }
                            }
                            else
                            {
                                ShowMessage(" امکان چاپ وجود ندارد.حساب عضو انتخاب شده نامشخص می باشد.");
                                return;
                            }
                        }

                        if ((bool)MemberManager[0]["IsLock"] == true)
                        {
                            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                            string Lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                            ShowMessage("امکان چاپ وجود ندارد.عضو انتخاب شده توسط " + Lockers + " قفل می باشد");
                            return;
                        }
                    }
                    CallbackPanelPage.JSProperties["cpDoPrintCard"] = 1;
                    CallbackPanelPage.JSProperties["cpPrintCardPath"] = "../../ReportForms/MemberTemporaryCardReport.aspx?MeId=" + Utility.EncryptQS(MeId.ToString());
                }
                catch (Exception err)
                {
                    CallbackPanelPage.JSProperties["cpDoPrintCard"] = 0;
                    Utility.SaveWebsiteError(err);
                }
                #endregion
                break;
            case "PrintRequestCard":
                #region PrintRequestCard
                try
                {
                    //****Parameter[1]:FocusedRowIndex
                    DataRow row = GridViewMember.GetDataRow(Convert.ToInt32(Parameter[1]));
                    int MeId = (int)row["MeId"];
                    int MrsId = Convert.ToInt32(row["MrsId"]);
                    //-----------card request-----------
                    if (MrsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
                    {
                        //  CallbackPanelPage.JSProperties["cpCall"] = 0;
                        CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 0;
                        ShowMessage("تنها امکان درخواست صدور کارت برای اعضای تایید شده وجود دارد");
                        return;
                    }
                    //----------------check estelam------------------
                    TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
                    LiManager.FindByIsInquiry(MeId, 1, 1);
                    if (LiManager.Count == 0)
                    {
                        ShowMessage("امکان درخواست صدور کارت برای مدرک استعلام نشده وجود ندارد");
                        CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 0;
                        return;
                    }

                    CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 1;
                    CallbackPanelPage.JSProperties["cpPrintRequestCardPath"] = "../../ReportForms/MemberCardRequestReport.aspx?MeId=" + Utility.EncryptQS(MeId.ToString());
                }
                catch
                {
                    CallbackPanelPage.JSProperties["cpDoPrintRequestCard"] = 0;
                }
                #endregion
                break;
            case "SetGridData":
                //ClearFilter();
                break;
            case "btnAccFish":
                btnAccFish_Click();
                break;
            case "PrintAcc":
                btnPrintAccFish_Click(Convert.ToInt32(Parameter[1]));
                break;
            case "InActiveGroup":
                try
                {
                    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.RejectMemberAndEndProcess);
                    if (WorkFlowTaskManager.Count <= 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطا در ذخیره رخ داده است";
                        return;
                    }
                    int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                     
                    Array arrayofMReId = GridViewMember.GetSelectedFieldValues("MReId").ToArray();
                    List<object> s = GridViewMember.GetSelectedFieldValues("MReId");

                    //  string ListOfMReId = string.Concat(",", arrayofMReId);
                    string ListOfMReId = string.Join(",", s);
                    int NmcId = FindNmcId();// ((int)TSP.DataManager.WorkFlowTask.RejectMemberAndEndProcess);
                    if (NmcId == -1)
                        return;
                    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                    MemberRequestManager.RejectGroupofTempMember(ListOfMReId, NmcId, Utility.GetCurrentUser_UserId(),TaskId);
                    
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره با موفقیت انجام شد";
                    GridViewMember.DataBind();
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطا در ذخیره رخ داده است";
                }
                break;
        }
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            if (MeFileRow == null || Utility.IsDBNullOrNullValue(MeFileRow["MeId"]))
                return;
            int MeId = int.Parse(MeFileRow["MeId"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
            int MReId = -1;
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                        if (!Utility.IsDBNullOrNullValue(GridRequest.GetDataRow(index0)["MsId"]))
                        {
                            if (int.Parse(GridRequest.GetDataRow(index0)["MsId"].ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                                WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;

                            WFUserControl.PerformCallback(MReId, TableType, WFCode, e);
                            GridViewMember.DataBind();
                            GridViewMember.ExpandRow(GridViewMember.FocusedRowIndex);
                        }
                        else
                        {
                            WFUserControl.SetMsgText("نوع درخواست نامشخص می باشد.");
                            return;
                        }
                    }
                    else
                    {
                        WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                        return;
                    }

                }
                else
                {
                    WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    return;
                }

            }
            else
            {
                WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید ");
                return;
            }

        }
        else
        {
            WFUserControl.SetMsgText(" لطفاً ابتدا یک درخواست را انتخاب نمائید");
        }
    }
    #endregion

    protected void cbSelectAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }

    #endregion

    #region Methods

    #region btnClick
    protected void btnReqView_Click()
    {
        string PageURL = string.Empty;
        int MeId = -1;
        int IsMeTemp = -1;
        int MReId = -1;
        string GridFilterString = GridViewMember.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            if (row != null)
            {
                MeId = (int)row["MeId"];
                if (!Utility.IsDBNullOrNullValue(row["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                else
                    IsMeTemp = 0;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
        }

        if (MeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        if (IsMeTemp == 0)
        {
            TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
            memberManager.FindByCode(MeId);
            if ((bool)memberManager[0]["IsLock"] == true)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن عضو انتخاب شده، امکان ایجاد درخواست جدید وجود ندارد";
                return;
            }
        }

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (GridRequest != null)//******Grid is Epanded
        {
            if (GridRequest.VisibleRowCount > 0)
            {
                int index0 = GridRequest.FocusedRowIndex;
                if (index0 != -1)
                {
                    MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        else//******Grid isn't Expanded
        {
            MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
            if (MemberRequestManager.Count > 0)
            {
                MReId = Convert.ToInt32(MemberRequestManager[0]["MReId"]);
            }
            else
            {
                MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, 1);
                if (MemberRequestManager.Count == 1)
                {
                    MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                }
                else
                {
                    MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 0, 1);
                    if (MemberRequestManager.Count == 1)
                    {
                        MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                    }
                }
            }

        }
        if (MReId != -1)
            PageURL = "MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString())
                + "&MReId=" + Utility.EncryptQS(MReId.ToString())
                ///////// + "&TP=" + Utility.EncryptQS("1")
                + "&PageMode=" + Utility.EncryptQS("View")
                + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)
                + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                + "&Pt=" + Utility.EncryptQS("2")
                + "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString());
        else
            PageURL = "MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                + "&Pt=" + Utility.EncryptQS("1") + "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString());
        if (string.IsNullOrEmpty(PageURL))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
            return;
        }
        ASPxWebControl.RedirectOnCallback(PageURL);
    }

    protected void btnReqNew_Click()
    {
        int MeId = -1;
        int IsMeTemp = -1;
        int isconfirm = 0;
        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            MeId = (int)row["MeId"];
            if (!Utility.IsDBNullOrNullValue(row["IsMeTemp"]))
            {
                IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                if (!Utility.IsDBNullOrNullValue(row["IsConfirm"]) && IsMeTemp == 1 && (Convert.ToInt32(row["IsConfirm"]) == 2 || Convert.ToInt32(row["IsConfirm"]) == 3))
                {
                    ShowMessage("امکان ثبت درخواست جدید برای عضو موقت تایید نشده وجود ندارد.");
                    return;
                }
            }
        }
        if (MeId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        memberManager.FindByCode(MeId);
        if ((bool)memberManager[0]["IsLock"] == true)
        {
            ShowMessage("به دلیل قفل بودن عضو انتخاب شده، امکان ایجاد درخواست جدید وجود ندارد");
            return;
        }

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, IsMeTemp, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ReqManager.FindByMemberId(MeId, IsMeTemp, 0, 1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل عدم پاسخ درخواست اولیه امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ASPxWebControl.RedirectOnCallback("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString())
            ///////////////+ "&TP=" + Utility.EncryptQS("1")
            + "&PageMode=" + Utility.EncryptQS("New") + "&Pt=" + Utility.EncryptQS("2") + "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString()));

    }

    protected void btnReqEdit_Click()
    {
        int MeId = -1;
        int MReId = -1;
        int MsId = -1;
        int IsMeTemp = -1;

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            MeId = (int)row["MeId"];
            IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);

        }
        if (MeId == -1)
        {
            //CallbackPanelPage.JSProperties["cpCall"] = 0;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            try
            {
                TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
                if (GridRequest != null)
                {
                    if (GridRequest.VisibleRowCount > 0)
                    {
                        int index0 = GridRequest.FocusedRowIndex;
                        if (index0 != -1)
                        {
                            MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                            MsId = int.Parse(GridRequest.GetDataRow(index0)["MsId"].ToString());

                            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                            ReqManager.FindByCode(MReId);
                            if (ReqManager.Count > 0)
                            {
                                //*************************************************************************
                                //if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
                                //{
                                //    CallbackPanelPage.JSProperties["cpCall"] = 0;
                                //    this.DivReport.Visible = true;
                                //    this.LabelWarning.Text = "امکان ویرایش اطلاعات برای شما وجود ندارد";
                                //    return;
                                //}
                                //*************************************************************************
                                if (ReqManager[0]["IsConfirm"].ToString() != "0")
                                {
                                    //CallbackPanelPage.JSProperties["cpCall"] = 0;
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ویرایش اطلاعات برای درخواست پاسخ داده شده وجود ندارد";
                                    return;
                                }
                            }
                            if (MsId != (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince && CheckPermitionForEdit(MReId))
                            {
                                string GridFilterString = GridViewMember.FilterExpression;
                                string SearchFilterString = GenerateFilterString();
                                // CallbackPanelPage.JSProperties["cpCall"] = 1;
                                ASPxWebControl.RedirectOnCallback("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString())
                                    ////////////////+ "&TP=" + Utility.EncryptQS("1")
                                    + "&PageMode=" + Utility.EncryptQS("Edit")
                                    + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                                    + "&Pt=" + Utility.EncryptQS("2") + "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString()));
                            }
                            else if (MsId == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince && CheckPermitionForEditTransferRequest(MReId))
                            {
                                string GridFilterString = GridViewMember.FilterExpression;
                                string SearchFilterString = GenerateFilterString();
                                //  CallbackPanelPage.JSProperties["cpCall"] = 1;
                                ASPxWebControl.RedirectOnCallback("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString())
                                    //////////////// + "&TP=" + Utility.EncryptQS("1") 
                                    + "&PageMode=" + Utility.EncryptQS("Edit")
                                     + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                                     + "&Pt=" + Utility.EncryptQS("2") + "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString()));
                            }
                            else
                            {
                                //   CallbackPanelPage.JSProperties["cpCall"] = 0;
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد";
                            }
                        }
                        else
                        {
                            //    CallbackPanelPage.JSProperties["cpCall"] = 0;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                        }
                    }
                    else
                    {
                        //   CallbackPanelPage.JSProperties["cpCall"] = 0;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    // CallbackPanelPage.JSProperties["cpCall"] = 0;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            catch (Exception err)
            {
                //  CallbackPanelPage.JSProperties["cpCall"] = 0;
                Utility.SaveWebsiteError(err);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ویرایش اطلاعات رخ داده است";
            }
        }
    }

    protected void btnReqDelete_Click()
    {
        //   CallbackPanelPage.JSProperties["cpCall"] = 0;
        int MeId = -1;
        int MReId = -1;
        int MsId = -1;

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            MeId = (int)row["MeId"];
        }
        if (MeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                        MsId = int.Parse(GridRequest.GetDataRow(index0)["MsId"].ToString());

                        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                        ReqManager.FindByCode(MReId);
                        if (ReqManager.Count > 0)
                        {
                            if (!Convert.ToBoolean(ReqManager[0]["Requester"]))
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف برای درخواست صادر شده توسط عضو حقیقی وجود ندارد";
                                return;
                            }
                            if (ReqManager[0]["IsCreated"].ToString() == "1")//درخواست اولیه
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست اولیه ثبت نام وجود ندارد";
                                return;
                            }
                            if (ReqManager[0]["IsConfirm"].ToString() != "0")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            if (MsId != (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince && CheckPermitionForDelete(MReId))
                            {
                                Delete(MeId, MReId);
                            }
                            else if (MsId == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince && CheckPermitionForDeleteForTransfer(MReId))
                            {
                                Delete(MeId, MReId);
                            }
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

    private void btnPrintAccFish_Click(int SelectedRoeIndex)
    {
        DataRow Merow = GridViewMember.GetDataRow(SelectedRoeIndex);
        int MeId = (int)Merow["MeId"];
        int IsMeTemp = Convert.ToInt32(Merow["IsMeTemp"]);
        int MReId = -1;

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (GridRequest != null)//******Grid is Expanded
        {
            if (GridRequest.VisibleRowCount > 0)
            {
                int index0 = GridRequest.FocusedRowIndex;
                if (index0 != -1)
                {
                    MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        else//******Grid isn't Expanded
        {
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
            if (MemberRequestManager.Count > 0)
            {
                MReId = Convert.ToInt32(MemberRequestManager[0]["MReId"]);
            }
            else
            {
                MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, 1);
                if (MemberRequestManager.Count == 1)
                {
                    MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                }
                else
                {
                    MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 0, 1);
                    if (MemberRequestManager.Count == 1)
                    {
                        MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                    }
                }
            }
        }

        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        int AccountingId = -1;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Member);
        AccountingManager.FindByTableTypeId(MReId, TableType);
        if (AccountingManager.Count == 0)
        {
            if (GridRequest == null)
            {
                ShowMessage("برای آخرین درخواست تایید شده شخص فیش مالی ثبت نشده است.");
                return;
            }
            else
            {
                ShowMessage("برای درخواست انتخاب شده فیش مالی ثبت نشده است.");
                return;
            }
        }
        else
        {
            AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
        }

        CallbackPanelPage.JSProperties["cpDoPrintCard"] = 1;
        CallbackPanelPage.JSProperties["cpPrintCardPath"] = "../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(AccountingId.ToString());
    }

    private void btnAccFish_Click()
    {
        string PageURL = string.Empty;
        int MeId = -1;
        int IsMeTemp = -1;
        int MReId = -1;
        string GridFilterString = GridViewMember.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        if (GridViewMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMember.GetDataRow(GridViewMember.FocusedRowIndex);
            if (row != null)
            {
                MeId = (int)row["MeId"];
                if (!Utility.IsDBNullOrNullValue(row["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(row["IsMeTemp"]);
                else
                    IsMeTemp = 0;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
        }

        if (MeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        //TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        //memberManager.FindByCode(MeId);
        //if ((bool)memberManager[0]["IsLock"] == true)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل قفل بودن عضو انتخاب شده، امکان ایجاد درخواست جدید وجود ندارد";
        //    return;
        //}

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMember.FindDetailRowTemplateControl(GridViewMember.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (GridRequest != null)//******Grid is Expanded
        {
            if (GridRequest.VisibleRowCount > 0)
            {
                int index0 = GridRequest.FocusedRowIndex;
                if (index0 != -1)
                {
                    MReId = int.Parse(GridRequest.GetDataRow(index0)["MReId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        else//******Grid isn't Expanded
        {
            MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, -1);
            if (MemberRequestManager.Count > 0)
            {
                MReId = Convert.ToInt32(MemberRequestManager[0]["MReId"]);
            }
            else
            {
                MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 1, 1);
                if (MemberRequestManager.Count == 1)
                {
                    MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                }
                else
                {
                    MemberRequestManager.FindByMemberId(MeId, IsMeTemp, 0, 1);
                    if (MemberRequestManager.Count == 1)
                    {
                        MReId = Convert.ToInt32(MemberRequestManager[MemberRequestManager.Count - 1]["MReId"]);
                    }
                }
            }
        }
        string Mode = "Request";
        if (IsMeTemp == 1)
            Mode = "TempMe";
        if (MReId != -1)
        {
            ASPxWebControl.RedirectOnCallback("MembersAccounting.aspx?MeId=" + Utility.EncryptQS(MeId.ToString())
                + "&MReId=" + Utility.EncryptQS(MReId.ToString())
                + "&Mode=" + Utility.EncryptQS(Mode)
                //////////////+ "&TP="+ Utility.EncryptQS("1") 
                + "&PageMode="
                + Utility.EncryptQS("View") + "&GrdFlt="
                + Utility.EncryptQS(GridFilterString)
                + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
            return;
        }
    }
    #endregion

    protected void Delete(int MeId, int MReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);
        try
        {
            trans.BeginSave();
            ReqManager.DeleteRequest(MReId, MeId);

            int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, MReId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }
            trans.EndSave();
            GridViewMember.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
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

    #region SetGridIndex

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMember.FilterExpression = GrdFlt;
            }
        }
    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            Utility.SetGridRowIndex(GridViewMember, Request.QueryString["PostId"], ref Index);
            hiddenFieldIsVisible["IsVisible"] = 1;
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsMembers.SelectParameters.Count; i++)
        {
            if (ObjdsMembers.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsMembers.SelectParameters[i].Name + "&";
                FilterString += ObjdsMembers.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjdsMembers.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    //case "MeId":
                    //    if (Value != "-1")
                    //        txtMeId.Text = Value;
                    //    break;
                    case "FirstName":
                        txtFirstName.Text = Value;
                        break;
                    case "LastName":
                        txtLastName.Text = Value;
                        break;
                    case "MobileNo":
                        txtMobileNo.Text = Value;
                        break;
                    case "MjId":
                        drdMajor.DataBind();
                        if (Value == "-1")
                        {
                            drdMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            drdMajor.SelectedIndex = 0;
                        }
                        else
                        {
                            drdMajor.SelectedIndex = drdMajor.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "DateFrom":
                        if (Value != "1")
                            txtFromDate.Text = Value;
                        break;
                    case "DateTo":
                        if (Value != "2")
                            txtToDate.Text = Value;
                        break;
                    case "IsConfirm":
                        int CmbReqTypeIndex = int.Parse(Value) + 1;
                        CmbReqType.SelectedIndex = CmbReqTypeIndex;
                        break;
                    case "TaskId":
                        CmbTask.DataBind();
                        if (Value == "-1")
                        {
                            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                            CmbTask.SelectedIndex = 0;
                        }
                        else
                        {
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "ReqCreateDateFrom":
                        if (Value != "1")
                            txtReqCreateDateFrom.Text = Value;
                        break;
                    case "ReqCreateDateTo":
                        if (Value != "3")
                            txtReqCreateDateTo.Text = Value;
                        break;
                    case "Requester":
                        //int CmbReqTypeIndex = int.Parse(Value) + 1;
                        CmbRequester.SelectedIndex = int.Parse(Value) + 1;// CmbReqTypeIndex;
                        break;
                }
            }
        }
    }


    #endregion

    #region Accounting
    /********************************Document**************************************************************************************************************************/
    private int GetParentAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembersCurrentAccount.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetAccName(DataRow Member)
    {
        string Name = Member["LastName"].ToString() + " " + Member["FirstName"].ToString();
        return Name;
    }

    private int GetMembershipEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembershipEarnings.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetDes1(DataRow Member, decimal Amount)
    {
        string Des = "جهت حق عضویت جدید آقای/خانم " + Member["FirstName"].ToString() + " " + Member["LastName"].ToString() + " " + "به مبلغ" + " " + Amount.ToString("#,#") + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    /*********************************************************************************************************************************************************************/
    #endregion

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

                    if (CurrentTaskCode == TaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember)
                    {
                        return true;

              
                    }
                }
            }
        }
        return false;

    }

    private Boolean CheckPermitionForEditTransferRequest(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;

                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WFCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

                    if (CurrentTaskCode == TaskCode)
                    {
                        return true;

                        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        //if (dtWorkFlowState.Rows.Count > 0)
                        //{
                        //    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        //    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                        //    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                        //    if (FirstTaskCode == TaskCode)
                        //    {
                        //        if (FirstNmcIdType == 0)
                        //        {
                        //            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        //            if (Permission > 0)
                        //                return true;
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
        return false;

    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        dtState.DefaultView.RowFilter = "StateType=0";
        if (dtState.DefaultView.Count == 1)
        {
            int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
            int CurrentUserId = int.Parse(dtState.Rows[0]["UserId"].ToString());

            // int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

            //if (CurrentNmcId == FindNmcId() && CurrentNmcIdType == 0)
            if (CurrentUserId == Utility.GetCurrentUser_UserId())
            {
                if (CurrentTaskCode == TaskCode)
                    return true;

            }
        }
        return false;

    }

    private Boolean CheckPermitionForDeleteForTransfer(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        dtState.DefaultView.RowFilter = "StateType=0";
        if (dtState.DefaultView.Count == 1)
        {
            int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
            int CurrentUserId = int.Parse(dtState.Rows[0]["UserId"].ToString());

            // int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

            //if (CurrentNmcId == FindNmcId() && CurrentNmcIdType == 0)
            if (CurrentUserId == Utility.GetCurrentUser_UserId())
            {
                if (CurrentTaskCode == TaskCode)
                    return true;

            }
        }
        return false;

    }
    #endregion

    private int FindNmcId()// FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);//, TaskId
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

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.Members).ToString());
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsMembers.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            ObjdsMembers.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtFirstName.Text))
            ObjdsMembers.SelectParameters["FirstName"].DefaultValue = txtFirstName.Text;
        else
            ObjdsMembers.SelectParameters["FirstName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtLastName.Text))
            ObjdsMembers.SelectParameters["LastName"].DefaultValue = txtLastName.Text;
        else
            ObjdsMembers.SelectParameters["LastName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtMobileNo.Text))
            ObjdsMembers.SelectParameters["MobileNo"].DefaultValue = txtMobileNo.Text;
        else
            ObjdsMembers.SelectParameters["MobileNo"].DefaultValue = "%";

        if (drdMajor.Value != null)
            ObjdsMembers.SelectParameters["MjId"].DefaultValue = drdMajor.Value.ToString();
        else
            ObjdsMembers.SelectParameters["MjId"].DefaultValue = "-1";

        ObjdsMembers.SelectParameters["FileNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFromDate.Text))
            ObjdsMembers.SelectParameters["DateFrom"].DefaultValue = txtFromDate.Text;
        else
            ObjdsMembers.SelectParameters["DateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtToDate.Text))
            ObjdsMembers.SelectParameters["DateTo"].DefaultValue = txtToDate.Text;
        else
            ObjdsMembers.SelectParameters["DateTo"].DefaultValue = "3";

        if (CmbReqType.Value != null)
            ObjdsMembers.SelectParameters["IsConfirm"].DefaultValue = CmbReqType.Value.ToString();
        else
            ObjdsMembers.SelectParameters["IsConfirm"].DefaultValue = "-1";
        if (CmbRequester.Value != null)
            ObjdsMembers.SelectParameters["Requester"].DefaultValue = CmbRequester.Value.ToString();
        else
            ObjdsMembers.SelectParameters["Requester"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            ObjdsMembers.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text.Trim();
        else
            ObjdsMembers.SelectParameters["FollowCode"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtReqCreateDateFrom.Text))
            ObjdsMembers.SelectParameters["ReqCreateDateFrom"].DefaultValue = txtReqCreateDateFrom.Text;
        else
            ObjdsMembers.SelectParameters["ReqCreateDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtReqCreateDateTo.Text))
            ObjdsMembers.SelectParameters["ReqCreateDateTo"].DefaultValue = txtReqCreateDateTo.Text;
        else
            ObjdsMembers.SelectParameters["ReqCreateDateTo"].DefaultValue = "3";

        if (CmbTask.Value != null)
            ObjdsMembers.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            ObjdsMembers.SelectParameters["TaskId"].DefaultValue = "-1";
        GridViewMember.DataBind();
    }
    #endregion
}