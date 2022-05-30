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

public partial class Members_ImplementDoc_FinancialStatus : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["MfId"]))
            {
                Response.Redirect("ImplementDoc.aspx");
                return;
            }
            try
            {
                HiddenFieldFinantialStatus["MfId"] = Server.HtmlDecode(Request.QueryString["MfId"]).ToString();
                HiddenFieldFinantialStatus["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string MFId = Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString());
            if (!CheckPermitionForEdit(int.Parse(MFId)))
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;
                //btnInActive.Enabled = false;
                //btnInActive2.Enabled = false;
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;


        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        // Response.Redirect("OfficeFinancialStausInsert.aspx?OfsId=" + Utility.EncryptQS("") + "&APageMode=" + Utility.EncryptQS("New") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]);
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
        //int OfsId = -1;

        //if (GridViewFinancialStatus.FocusedRowIndex > -1)
        //{
        //    if (Session["FillOfStatus"] != null)
        //    {
        //        GridViewFinancialStatus.DataSource = (DataTable)Session["FillOfStatus"];
        //        GridViewFinancialStatus.DataBind();
        //    }
        //    else
        //        FillGrid();

        //    DataRow row = GridViewFinancialStatus.GetDataRow(GridViewFinancialStatus.FocusedRowIndex);
        //    OfsId = (int)row["OfsId"];

        //}
        //if (OfsId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //}
        //else
        //{
        //    Response.Redirect("OfficeFinancialStausInsert.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&aPageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]);

        //}
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
        //int OfsId = -1;

        //if (GridViewFinancialStatus.FocusedRowIndex > -1)
        //{
        //    if (Session["FillOfStatus"] != null)
        //    {
        //        GridViewFinancialStatus.DataSource = (DataTable)Session["FillOfStatus"];
        //        GridViewFinancialStatus.DataBind();
        //    }
        //    else
        //        FillGrid();

        //    DataRow row = GridViewFinancialStatus.GetDataRow(GridViewFinancialStatus.FocusedRowIndex);
        //    OfsId = (int)row["OfsId"];

        //}
        //if (OfsId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        //}
        //else
        //{
        //    Response.Redirect("OfficeFinancialStausInsert.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]);


        //}
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImplementDoc.aspx");
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobHistory":
                Response.Redirect("~/Members/Documents/MemberJobHistory.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&DocType=" + Utility.EncryptQS("1") + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
                break;
            case "ImplDoc":
                Response.Redirect("~/Members/ImplementDoc/AddImplementDoc.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
                break;
        }
    }

    protected void GridViewFinancialStatus_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

        }
    }

    protected void GridViewFinancialStatus_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion

    #region Methods
    protected void FillGrid()
    {

        //TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        //TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        ////string Mode = Utility.DecryptQS(HDMode.Value);
        ////string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        ////string OfId = Utility.DecryptQS(OfficeId.Value);

        //OffManager.FindByCode(int.Parse(OfId));
        //if (OffManager.Count == 0)
        //{
        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        //    return;
        //}

        //switch (Mode)
        //{

        //    case "Home":

        //        if (OffManager[0]["MrsId"].ToString() == "1")
        //        {

        //            GridViewFinancialStatus.DataSource = FinManager.FindByOffRequest(int.Parse(OfId), -1, 1);
        //            GridViewFinancialStatus.DataBind();
        //        }

        //        else
        //        {

        //            GridViewFinancialStatus.DataSource = FinManager.FindByOffRequest(int.Parse(OfId), -1, -1);
        //            GridViewFinancialStatus.DataBind();
        //        }


        //        break;

        //    case "Request":

        //        GridViewFinancialStatus.DataSource = FinManager.FindByOffRequest(int.Parse(OfId), int.Parse(OfReId), -1);
        //        GridViewFinancialStatus.DataBind();

        //        break;

        //}
        //Session["FillOfStatus"] = GridViewFinancialStatus.DataSource;
    }

    private void NextPage(string Mode)
    {
        int OfsId = -1;
        int focucedIndex = GridViewFinancialStatus.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewFinancialStatus.GetDataRow(focucedIndex);
            OfsId = (int)row["OfsId"];
        }
        if (OfsId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                OfsId = -1;
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
            else
            {
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
        }
    }


    private Boolean CheckPermitionForEdit(int TableId)
    {
         TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
         TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortalImpDocument(TableId, "");
         BtnNew.Enabled = BtnNew2.Enabled
                 = btnEdit.Enabled = btnEdit2.Enabled= WFPermission.BtnNew;
         return WFPermission.BtnEdit;
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //WorkFlowStateManager.ClearBeforeFill = true;
        //int TaskOrder = -1;
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        //WorkFlowTaskManager.FindByTaskCode(TaskCode);
        //if (WorkFlowTaskManager.Count > 0)
        //{
        //    TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        //    if (TaskOrder != 0)
        //    {
        //        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        //        if (dtWorkFlowLastState.Rows.Count > 0)
        //        {
        //            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        //            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
        //            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
        //            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
        //            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
        //            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

        //            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
        //            {
        //                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        //                if (dtWorkFlowState.Rows.Count > 0)
        //                {
        //                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        //                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
        //                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
        //                    if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
        //                    {
        //                        if (FirstNmcIdType == 1)
        //                        {
        //                            if (FirstNmcId == Utility.GetCurrentUser_MeId())
        //                            {
        //                                return true;
        //                            }
        //                            else
        //                                return false;
        //                        }
        //                        else
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}

    }

    #endregion
}
