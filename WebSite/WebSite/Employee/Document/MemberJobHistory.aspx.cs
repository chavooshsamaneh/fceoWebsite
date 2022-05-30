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

public partial class Employee_Document_MemberJobHistory : System.Web.UI.Page
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

        if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["DocType"] == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_UserId());
            BtnNew.Enabled =
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = 
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled =
            btnView2.Enabled =
            GridViewJobhistory.Visible = per.CanView;
            btnPrint.Enabled = btnPrint2.Enabled = per.CanView;

            HiddenFieldJobHistory["PrePageMode"] = Request.QueryString["PgMd"];
            HiddenFieldJobHistory["MFId"] = Request.QueryString["MFId"];
            HiddenFieldJobHistory["DocType"] = Request.QueryString["DocType"];

            string DocType = Utility.DecryptQS(HiddenFieldJobHistory["DocType"].ToString());
            string MFId = Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString());
            string MemberName = "";

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), int.Parse(DocType));
            string MeId = "";
            if (DocMemberFileManager.Count == 1)
            {
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DocType = DocMemberFileManager[0]["DocType"].ToString();
                if (DocType == "0")
                {
                    btnJudgment.Visible = false;
                    btnJudgment2.Visible = false;
                    MenuImpDoc.Visible = false;
                    MenuMemberFile.Visible = true;
                    MeId = DocMemberFileManager[0]["MeId"].ToString();
                    HiddenFieldJobHistory["MeId"] = Utility.EncryptQS(MeId);
                    MemberManager.FindByCode(int.Parse(MeId));
                    if (MemberManager.Count == 1)
                    {
                        //  RoundPanelJobHistory.HeaderText = "پروانه اشتغال به کار: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                        MemberName = "پروانه اشتغال به کار: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                    }
                    RoundPanelValues.Visible = false;
                    MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
                }
                else if (DocType == "1")
                {
                    btnJudgment.Visible = true;
                    btnJudgment2.Visible = true;
                    MenuImpDoc.Visible = true;
                    MenuMemberFile.Visible = false;
                    int MemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                    DocMemberFileManager.FindByCode(MemberFileId, 0);
                    if (DocMemberFileManager.Count == 1)
                    {
                        MeId = DocMemberFileManager[0]["MeId"].ToString();
                        HiddenFieldJobHistory["MeId"] = Utility.EncryptQS(MeId);
                        MemberManager.FindByCode(int.Parse(MeId));
                        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
                        if (MemberManager.Count == 1)
                        {
                            // RoundPanelJobHistory.HeaderText = "مجوز مجری حقیقی: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                            MemberName = "مجوز مجری حقیقی: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                        }
                    }
                    RoundPanelValues.Visible = true;
                    ObjectDataSourceFactorValues.SelectParameters[0].DefaultValue = MeId;

                }
            }
            else
            {
                Response.Redirect("MemberFile.aspx");
            }
            ObjdsJobHistory.SelectParameters["MeId"].DefaultValue = MeId;
            ObjdsJobHistory.SelectParameters["TableType"].DefaultValue = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile).ToString();
            ObjdsJobHistory.SelectParameters["TableId"].DefaultValue = MFId;
            ObjdsJobHistory.SelectParameters["ReqTableType"].DefaultValue = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ProjectJobHistory).ToString();

            CheckWorkFlowPermission();
            if (DocType == "1" && !CheckPermissionForImpJudge())
            {
                btnJudgment.Enabled = false;
                btnJudgment2.Enabled = false;
            }

            Session["DataTable"] = GridViewJobhistory.Columns;
            Session["DataSource"] = ObjdsJobHistory;
            Session["Title"] = "سابقه کار";
            Session["Header"] = MemberName;

            this.ViewState["BtnJudgment"] = btnJudgment.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            CheckMenueChange(int.Parse(MFId), int.Parse(MeId));      
        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldJobHistory["MeId"]))
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldJobHistory["MeId"].ToString()));

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnJudgment"] != null)
            this.btnJudgment.Enabled = this.btnJudgment2.Enabled = (bool)this.ViewState["BtnJudgment"];
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

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
        if (GridViewJobhistory.FocusedRowIndex > -1)
        {
            DataRow JobHistoryRow = GridViewJobhistory.GetDataRow(GridViewJobhistory.FocusedRowIndex);
            if (JobHistoryRow != null)
            {
                int JHId = int.Parse(JobHistoryRow["JhId"].ToString());
                int TableId = int.Parse(JobHistoryRow["TableId"].ToString());
                int TableType = int.Parse(JobHistoryRow["TableType"].ToString());
                int CurrentTType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString()));
                if (CurrentTType == TableType && TableId == MFId)
                {
                    NextPage("Edit");
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                }

            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمایید.";
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (HiddenFieldJobHistory["DocType"] != null && !string.IsNullOrEmpty(HiddenFieldJobHistory["DocType"].ToString()))
        {
            int DocType = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["DocType"].ToString()));
            if (DocType == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
                     && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
                    Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString()
                          + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                else
                    Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString());
            }
            else if (DocType == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp)
            {
                Response.Redirect("~/Employee/ImplementDoc/ImplementDoc.aspx");
            }
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewJobhistory.FocusedRowIndex > -1)
        {
            DataRow JobHistoryRow = GridViewJobhistory.GetDataRow(GridViewJobhistory.FocusedRowIndex);
            if (JobHistoryRow != null)
            {
                int JHId = int.Parse(JobHistoryRow["JhId"].ToString());
                int TableId = int.Parse(JobHistoryRow["TableId"].ToString());
                int TableType = int.Parse(JobHistoryRow["TableType"].ToString());
                int CurrentTType = -1;
                int DocType = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["DocType"].ToString()));
                if (DocType == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile)
                {
                    CurrentTType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                }
                else if (DocType == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp)
                {
                    CurrentTType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
                }
                else if (DocType == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileObs)
                {
                    CurrentTType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
                }

                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString()));
                if (CurrentTType == TableType && TableId == MFId)
                {
                    Delete(JHId);
                }
                else
                {
                    //*****Check Other Condition....
                    // Inactive(JHId);
                    InsertInActive(JHId, MFId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ProjectJobHistory), CurrentTType);
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمایید.";
        }
    }

    protected void btnJudgment_Click(object sender, EventArgs e)
    {
        //if (IsPageRefresh)
        //    return;
        //if (GridViewJobhistory.FocusedRowIndex > -1)
        //{
        //    DataRow row = GridViewJobhistory.GetDataRow(GridViewJobhistory.FocusedRowIndex);
        //    int JhId = (int)row["JhId"];

        //    int MfId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString()));
        //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
        //    int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.GradingImplementDoc;
        //    int GradingImplementDocTaskId = -1;

        //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode(WorkflowCode, MfId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == GradingImplementDocTaskId)
        //        {
        //            Response.Redirect("AddMemberJobHistory.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&PgMd=" + Utility.EncryptQS("Judge") + "&MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString() + "&PrePgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString());

        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "در این مرحله از جریان کار قادر به ثبت نظر کارشناسی نمی باشید.";
        //        }
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "جریان کار پروانه جاری نامشخص است.";
        //    }
        //}
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (IsPageRefresh)
            return;
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberConfirmJobHistory.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Accounting":
                Response.Redirect("DocumentAccounting.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&MeId=" + HiddenFieldJobHistory["MeId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void MenuImpDoc_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (IsPageRefresh)
            return;
        switch (e.Item.Name)
        {
            case "Financial":
                Response.Redirect("~/Employee/ImplementDoc/FinancialStatus.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString());
                break;
            case "ImplDoc":
                Response.Redirect("~/Employee/ImplementDoc/AddImplementDoc.aspx?MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString());
                break;
        }
    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewJudge_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MeetingDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJudge_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MeetingDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJobhistory_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "StartCorporateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "EndCorporateDate":
                //e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJobhistory_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "StartCorporateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "EndCorporateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }
    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int JhId = -1;
        int focucedIndex = GridViewJobhistory.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewJobhistory.GetDataRow(focucedIndex);
            JhId = (int)row["JhId"];
        }
        if (JhId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                JhId = -1;
                Response.Redirect("AddMemberJobHistory.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString() + "&PrePgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            else
            {
                Response.Redirect("AddMemberJobHistory.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString() + "&PrePgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
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

    //private void Inactive(int JHId)
    //{
    //    TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
    //    try
    //    {
    //        ProjectJobHistoryManager.FindByCode(JHId);
    //        if (ProjectJobHistoryManager.Count == 1)
    //        {
    //            ProjectJobHistoryManager[0].BeginEdit();

    //            ProjectJobHistoryManager[0]["Inactive"] = 1;
    //            ProjectJobHistoryManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            ProjectJobHistoryManager[0]["ModifiedDate"] = DateTime.Now;

    //            ProjectJobHistoryManager[0].EndEdit();
    //            int cn = ProjectJobHistoryManager.Save();
    //            if (cn > 0)
    //            {
    //                GridViewJobhistory.DataBind();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد.";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        SetError(err);
    //    }
    //}

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        Manager.FindByTableIdTableType(TableId, TableType, ReqId);
        if (Manager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("رکورد مورد نظر غیر فعال می باشد");
            return;
        }
        DataRow dr = Manager.NewRow();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        if (Manager.Save() > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";
        }
        GridViewJobhistory.DataBind();
    }

    private void Delete(int JHId)
    {
        try
        {
            TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
            ProjectJobHistoryManager.FindByCode(JHId);
            if (ProjectJobHistoryManager.Count == 1)
            {
                ProjectJobHistoryManager[0].Delete();

                int cn = ProjectJobHistoryManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
            GridViewJobhistory.DataBind();
        }
        catch (Exception err)
        {
            SetDeleteError(err);
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
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string MFId = Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        string DocType = Utility.DecryptQS(HiddenFieldJobHistory["DocType"].ToString());
        int TaskCode = -1;
        int WFCode = -1;

        if (int.Parse(DocType) == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        }
        else if (int.Parse(DocType) == (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        }

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerDocUnit = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerDocUnitRes = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPerDocUnit.BtnNew || WFPerDocUnitRes.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit = WFPerDocUnit.BtnEdit || WFPerDocUnitRes.BtnEdit;
        this.ViewState["btnActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || WFPerDocUnit.BtnInactive || WFPerDocUnitRes.BtnInactive;
    }

    private Boolean CheckPermissionForImpJudge()
    {
        return false;
        //int MfId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString()));
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //int WorkflowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.GradingImplementDoc;
        //int GradingImplementDocTaskId = -1;

        //WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //if (WorkFlowTaskManager.Count == 1)
        //{
        //    GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode(WorkflowCode, MfId);
        //    if (dtWFState.Rows.Count > 0)
        //    {
        //        int CurrentTskId = int.Parse(dtWFState.Rows[0]["TaskId"].ToString());
        //        if (CurrentTskId == GradingImplementDocTaskId)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //            //this.DivReport.Visible = true;
        //            //this.LabelWarning.Text = "در این مرحله از جریان کار قادر به ثبت نظر کارشناسی نمی باشید.";
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //        //this.DivReport.Visible = true;
        //        //this.LabelWarning.Text = "جریان کار پروانه جاری نامشخص است.";
        //    }
        //}
        //else
        //{
        //    return false;
        //}
    }

    private void BackToManagementPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFile.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFile.aspx");
        }
    }

    private void CheckMenueChange(int MfId, int MeId)
    {
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        ProjectJobHistoryManager.FindForDelete(0, MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Documents));
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Height = Utility.MenuImgSize;
        }
        DocMemberExamManager.SelectById(MfId, MeId);
        if (DocMemberExamManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Height = Utility.MenuImgSize;
        }
        DataTable dtRes = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
        if (dtRes.Rows.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Height = Utility.MenuImgSize;
        }

    }
    #endregion
}
