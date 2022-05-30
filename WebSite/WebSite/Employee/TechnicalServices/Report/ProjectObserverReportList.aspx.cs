using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_TechnicalServices_Report_ProjectObserverReportList : System.Web.UI.Page
{
    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        if (!IsPostBack)
        {            
            TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perObserverWage = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverWage(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission PerProjectRemainAmount = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ProjectRemainAmount(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnView.Enabled = btnView2.Enabled = GridViewAccDocument.Visible = PerObserverReportList.CanView;
            BtnNew.Enabled = btnNew2.Enabled = PerObserverReportList.CanNew;
            btnPrintObservaerWage.Enabled = btnPrintObservaerWage1.Enabled = perObserverWage.CanView;
            btnPrjRemain.Enabled = btnPrjRemain2.Enabled = PerProjectRemainAmount.CanView;
            TSP.DataManager.Permission PerRemove = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_RemoveObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete2.Visible = btnDelete.Visible = perObserverWage.CanDelete || perObserverWage.CanEdit || perObserverWage.CanNew || perObserverWage.CanView;
            this.ViewState["btnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["btnPrintObservaerWage"] = btnPrintObservaerWage.Enabled;
            this.ViewState["btnPrjRemain"] = btnPrjRemain.Enabled;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjdObserveAccDocument.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjdObserveAccDocument.SelectParameters["AgentId"].DefaultValue = "-1";
            }
        }
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (this.ViewState["btnPrintObservaerWage"] != null)
            this.btnPrintObservaerWage.Enabled = this.btnPrintObservaerWage1.Enabled = (bool)this.ViewState["btnPrintObservaerWage"];

        if (this.ViewState["btnPrjRemain"] != null)
            this.btnPrjRemain.Enabled = this.btnPrjRemain2.Enabled = (bool)this.ViewState["btnPrjRemain"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountingDocumentInsert.aspx?PgMd=" + Utility.EncryptQS("New") + "&AccDocId=" + Utility.EncryptQS("-1"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountingDocumentInsert.aspx?PgMd=" + Utility.EncryptQS("View") + "&AccDocId=" + Utility.EncryptQS(((GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex))["AccDocId"]).ToString()));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountingDocumentInsert.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&AccDocId=" + Utility.EncryptQS(((GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex))["AccDocId"]).ToString()));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridViewAccDocument.FocusedRowIndex < 0)
            {
                SetMessage("ابتدا یک لیست را انتخاب نمایید");
                return;
            }
            System.Data.DataRow dr = GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex);
            int AccDocId = Convert.ToInt32(dr["AccDocId"]);
            if (Convert.ToInt32(dr["Status"]) == (int)TSP.DataManager.TSAccountingdocumentStatus.SendReportToAccountingUnit)
            {
                SetMessage("امکان حذف لیست ارسال شده به واحد مالی وجود ندارد");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingDocumentManager.DeleteReportList(AccDocId);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewAccDocument.DataBind();
        }
        catch (Exception err)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }

    protected void btnChangeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridViewAccDocument.FocusedRowIndex < 0)
            {
                SetMessage("ابتدا یک لیست را انتخاب نمایید");
                return;
            }
            System.Data.DataRow dr = GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex);
            int AccDocId = Convert.ToInt32(dr["AccDocId"]);
            if (TSP.DataManager.TechnicalServices.AccountingDocumentManager.EditAccDocument(AccDocId, (int)TSP.DataManager.TSAccountingdocumentStatus.SendReportToAccountingUnit))
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                GridViewAccDocument.DataBind();
            }
            else
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
        catch (Exception err)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }

    protected void GridViewAccDocument_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        switch (e.Parameters)
        {

            case "PrintObservaerWage":

                GridViewAccDocument.JSProperties["cpPrint"] = 1;
                GridViewAccDocument.JSProperties["cpUrl"] = "../../../ReportForms/TechnicalServices/ProjectObserverWageByDocuments.aspx?AccDocId=" + Utility.EncryptQS(((GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex))["AccDocId"]).ToString());
                break;
            case "PrintProjectRemain":
                GridViewAccDocument.JSProperties["cpPrint"] = 1;
                GridViewAccDocument.JSProperties["cpUrl"] = "../../../ReportForms/TechnicalServices/ProjectRemainAmount.aspx?AccDocId=" + Utility.EncryptQS(((GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex))["AccDocId"]).ToString());
                break;
            case "PrintProjectRemainSnap":
                GridViewAccDocument.JSProperties["cpPrint"] = 1;
                GridViewAccDocument.JSProperties["cpUrl"] = "../../../ReportForms/TechnicalServices/ProjectRemainAmountSnap.aspx?AccDocId=" + Utility.EncryptQS(((GridViewAccDocument.GetDataRow(GridViewAccDocument.FocusedRowIndex))["AccDocId"]).ToString());
                break;
        }
    }
    #endregion

    #region Methods

    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}