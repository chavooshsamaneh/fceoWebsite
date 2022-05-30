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

public partial class Employee_OfficeRegister_OfficeAgent : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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
            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            TSP.DataManager.Permission per = FindPermissionClass();
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.Visible = per.CanView;

            //Session["FillOfAgent"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["OfReId"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }

            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();


            }
            catch (Exception)
            { }

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);


            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ODbOfAgent.SelectParameters[0].DefaultValue = OfId;
            ODbOfAgent.SelectParameters[1].DefaultValue = OfReId;


            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                Session["Header"] = "شرکت : " + OfManager[0]["OfName"].ToString();

            OfficeInfoUserControl.OfReId = int.Parse(OfReId);

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "Document")
                CheckWorkFlowPermissionForDoc();
            else if (Department == "MemberShip")
                CheckWorkFlowPermissionForOffice();

            //TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if (!Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromMemberORAnswered
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }
                if (ReqManager[0]["IsConfirm"].ToString() == "0") //Not Answered
                {
                    ODbOfAgent.SelectParameters[3].DefaultValue = "2";

                }
            }

            # region CheckModeComment
            //switch (Mode)
            //{

            //    case "Home":

            //        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            //        {
            //            Response.Redirect("Office1.aspx");
            //            return;
            //        }
            //        try
            //        {
            //            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());

            //            ReqManager.FindByOfficeId(int.Parse(OfId), -1, 0);
            //            if (ReqManager.Count > 0)
            //            {
            //                if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //                {
            //                    btnEdit.Enabled = false;
            //                    btnEdit2.Enabled = false;
            //                    BtnNew.Enabled = false;
            //                    BtnNew2.Enabled = false;
            //                    btnInActive.Enabled = false;
            //                    btnInActive2.Enabled = false;
            //                }
            //            }

            //            if (OfManager[0]["MrsId"].ToString() == "1")//تایید شده
            //            {
            //                btnInActive.Enabled = false;
            //                btnInActive2.Enabled = false;
            //                btnEdit.Enabled = false;
            //                btnEdit2.Enabled = false;
            //                BtnNew.Enabled = false;
            //                BtnNew2.Enabled = false;

            //                Session["FillOfAgent"] = AgentManager.FindByOffRequest(int.Parse(OfId), -1, 1);

            //            }
            //            else
            //            {
            //                Session["FillOfAgent"] = AgentManager.FindByOffRequest(int.Parse(OfId), -1, -1);

            //            }
            //        }
            //        catch (Exception)
            //        { }


            //        break;

            //    case "Request":
            //        try
            //        {
            //            ASPxMenu1.Items[0].Visible = false;
            //            ASPxMenu1.Items[1].Visible = false;
            //            ASPxMenu1.Items[6].Visible = false;


            //            string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
            //            string TPType = Utility.DecryptQS(ReqestMode);
            //            if (!string.IsNullOrEmpty(TPType))
            //            {
            //                if (TPType == "0")//Menu
            //                {
            //                    btnInActive.Enabled = false;
            //                    btnInActive2.Enabled = false;
            //                    BtnNew.Enabled = false;
            //                    BtnNew2.Enabled = false;
            //                    btnEdit.Enabled = false;
            //                    btnEdit2.Enabled = false;
            //                }
            //                else
            //                {
            //                    btnInActive.Enabled = false;
            //                    btnInActive2.Enabled = false;
            //                    btnEdit.Enabled = false;
            //                    btnEdit2.Enabled = false;


            //                    ReqManager.FindByCode(int.Parse(OfReId));
            //                    if (ReqManager.Count > 0)
            //                    {
            //                        if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //                        {
            //                            BtnNew.Enabled = false;
            //                            BtnNew2.Enabled = false;

            //                        }
            //                    }


            //                }
            //            }

            //            Session["FillOfAgent"] = AgentManager.FindByOffRequest(int.Parse(OfId), int.Parse(OfReId), -1);

            //        }
            //        catch (Exception err)
            //        {
            //        }


            //        break;

            //}
            #endregion

            SetMenuItem();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = ODbOfAgent;
        Session["Title"] = "شعبه ها";
        //Session["Header"] = "شرکت : " + lblOfName.Text;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeAgentInsert.aspx?OagId=" + Utility.EncryptQS("") + "&APageMode=" + Utility.EncryptQS("New") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
             + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OagId = -1;
        int OfReId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OagId = (int)row["OagId"];
            OfReId = (int)row["OfReId"];

        }
        if (OagId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
            AgentManager.FindByCode(OagId);
            if (AgentManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                if (OfReId == CurrentOfReId)
                {
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());

                    if ((Department == "Document" && CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && CheckPermitionForEditForOffice(OfReId)))
                        Response.Redirect("OfficeAgentInsert.aspx?OagId=" + Utility.EncryptQS(OagId.ToString()) + "&APageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                             + "&Dprt=" + HiddenFieldOffice["Department"].ToString());


                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                }


            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OagId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OagId = (int)row["OagId"];
        }
        if (OagId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OfficeAgentInsert.aspx?OagId=" + Utility.EncryptQS(OagId.ToString()) + "&APageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                 + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        switch (e.Item.Name)
        {
            case "Office":
                string Page = "";
                switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
                {
                    case "MemberShip":
                        Page = "OfficeInsert.aspx";
                        break;
                    default:
                        Page = "OfficeDocumentInsert.aspx";
                        break;
                }
                Response.Redirect(Page + "?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;

            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                     + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Page = "";
        switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
        {
            case "MemberShip":
                Page = "OfficeInsert.aspx";
                break;
            default:
                Page = "OfficeDocumentInsert.aspx";
                break;
        }
        Response.Redirect(Page + "?PageMode=" + PgMode.Value + "&OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
              + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        string PageName = "Office.aspx";
        switch (Dprt)
        {
            case "MemberShip":
                PageName = "Office.aspx";
                break;
            case "Document":
                PageName = "OfficeDocument.aspx";
                break;
        }
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?PostId=" + OfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {

            Response.Redirect(PageName);
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            string OfId = Utility.DecryptQS(OfficeId.Value);

            int OagId = -1;
            int OfReId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                OagId = (int)row["OagId"];
                OfReId = (int)row["OfReId"];
                InActiveName = row["InActiveName"].ToString();
            }
            if (OagId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();

                AgentManager.FindByCode(OagId);
                if (AgentManager.Count == 1)
                {

                    int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                    if (OfReId == CurrentOfReId)
                    {
                        AgentManager[0].Delete();
                        AgentManager.Save();
                        CustomAspxDevGridView1.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            return;
                        }

                        InsertInActive(OagId, CurrentOfReId);
                        //if (Convert.ToBoolean(AgentManager[0]["InActive"]))
                        //{
                        //    this.DivReport.Visible = true;
                        //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                        //    return;
                        //}
                        //else
                        //{
                        //    AgentManager[0].BeginEdit();
                        //    AgentManager[0]["InActive"] = 1;
                        //    AgentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        //    AgentManager[0].EndEdit();
                        //}
                    }
                    CheckMenuImageCurrentPage(CurrentOfReId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                }


            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (OfficeRequest.Value != null)
        {
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnOfReId = e.GetValue("OfReId").ToString();
            if (OfReId == CurretnOfReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Tel")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Tel")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
        }
    }

    //*************************Methods********************************************************************
    #region WF
    private void CheckWorkFlowPermissionForOffice()
    {
        CheckWorkFlowPermissionForSaveForOffice();
    }

    private void CheckWorkFlowPermissionForSaveForOffice()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == TaskCode2)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        if (FirstNmcIdType == 0)
                        {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode2, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        }
                    }
                }
            }
        }
        return false;

    }

    //*******************Office Doc**********************************

    private void CheckWorkFlowPermissionForDoc()
    {
        CheckWorkFlowPermissionForSaveForDoc();
    }

    private void CheckWorkFlowPermissionForSaveForDoc()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

            if (CurrentTaskCode == TaskCode || CurrentTaskCode == TaskCode2)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        if (FirstNmcIdType == 0)
                        {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode2, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        }
                    }
                }
            }
        }

        return false;

    }
    #endregion

    protected void InsertInActive(int OagId, int OfReId)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = OagId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.OfficeAgent;
        dr["ReqId"] = OfReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        CustomAspxDevGridView1.DataBind();

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "ذخیره انجام شد";
    }

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.OfficeManager officeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager officeRequestManager = new TSP.DataManager.OfficeRequestManager();



        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        arr.Add(0);//arr[7]-->Group


        officeRequestManager.FindByCode(OfReId);
        if (officeRequestManager.Count > 0)
        {
            int OfId = Convert.ToInt32(officeRequestManager[0]["OfId"]);
            officeManager.FindByCode(OfId);
            if (officeManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
            }
        }


        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        OffAgentManager.FindForDelete(OfReId);

        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffAgentManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
                arr[0] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "";
                arr[0] = 0;

            }
            Session["OffMenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(OfReId);
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffAgentManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
                arr[0] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "";
                arr[0] = 0;

            }
            Session["OffMenuArrayList"] = arr;

        }

    }

    protected void SetMenuItem()
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[6] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));

        }
    }

    private TSP.DataManager.Permission FindPermissionClass()
    {
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {
            return (TSP.DataManager.OfficeAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        else if (Department == "Document")
        {
            return (TSP.DataManager.OfficeAgentManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        return (TSP.DataManager.OfficeAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
    }

}
