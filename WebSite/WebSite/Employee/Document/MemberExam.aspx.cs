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

public partial class Employee_Document_MemberExam : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("MemberFile.aspx");
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberExamManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_UserId());
            BtnNew.Enabled =
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled =
            btnEdit2.Enabled =
            btnInActive.Enabled =
            btnInActive2.Enabled = per.CanEdit;
            btnView.Enabled =
            btnView2.Enabled =
            GridViewMemberExam.Visible =
            per.CanView; // btnPrint.Enabled = btnPrint2.Enabled =

            HiddenFieldExam["PrePageMode"] = Request.QueryString["PgMd"];
            HiddenFieldExam["MFId"] = Request.QueryString["MFId"];
            string MFId = Utility.DecryptQS(HiddenFieldExam["MFId"].ToString());
            //string PrintHeader = "";

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
            if (DocMemberFileManager.Count == 1)
            {
                string MeId = DocMemberFileManager[0]["MeId"].ToString();
                ObjdsExam.SelectParameters[0].DefaultValue = MFId;
                ObjdsExam.SelectParameters["MeId"].DefaultValue = MeId;
                HiddenFieldExam["MeId"] = Utility.EncryptQS(MeId);               
                CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
            }
            else
            {
                Response.Redirect("MemberFile.aspx");
            }

            CheckWorkFlowPermission();
            
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnActive"] = btnInActive.Enabled;
        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldExam["MeId"]))
        {
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldExam["MeId"].ToString()));
            MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["btnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];      
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = -1;
        int focucedIndex = GridViewMemberExam.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberExam.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];
            if (HiddenFieldExam["MFId"] != null)
            {
                int CurrentMFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                if (CurrentMFId == MfId)
                {
                    if (CheckPermitionForEdit(CurrentMFId))
                    {
                        NextPage("Edit");
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "شما قادر به ویرایش اطلاعات نمی باشید.";
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
        NextPage("View");
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {

        try
        {
            if (GridViewMemberExam.FocusedRowIndex > -1)
            {
                DataRow dr = GridViewMemberExam.GetDataRow(GridViewMemberExam.FocusedRowIndex);
                if (dr != null)
                {
                    if (!Convert.ToBoolean(dr["InactiveValue"]))
                    {
                        ShowMessage("رکورد انتخاب شده فعال می باشد");
                        return;
                    }
                    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                    int SelectedMfId = int.Parse(dr["MfId"].ToString());
                    int MExmDId = int.Parse(dr["MExmDId"].ToString());
                    Active(MExmDId, MFId);
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);

            ShowMessage("خطایی در ذخیره رخ داده است");
            if (Utility.ShowExceptionError())
            {
                this.LabelWarning.Text += err.Message;
            }

        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewMemberExam.FocusedRowIndex > -1)
        {
            DataRow MemberExamRow = GridViewMemberExam.GetDataRow(GridViewMemberExam.FocusedRowIndex);
            if (MemberExamRow != null)
            {
                if (Convert.ToBoolean(MemberExamRow["InactiveValue"]))
                {
                    ShowMessage("رکورد انتخاب شده غیرفعال می باشد");
                    return;
                }
                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                int MExmDId = int.Parse(MemberExamRow["MExmDId"].ToString());
                int SelectedMfId = int.Parse(MemberExamRow["MfId"].ToString());
                if (MFId == SelectedMfId)
                    Delete(MExmDId);
                else
                    InsertInActive(MExmDId, MFId,TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberExam),TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile));                        
            }
        }
        else
        {
           ShowMessage( "ردیفی انتخاب نشده است.");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
              && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString());
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberConfirmJobHistory.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Accounting":
                Response.Redirect("DocumentAccounting.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

        }
    }    

    protected void GridViewMemberExam_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (HiddenFieldExam["MFId"] != null)
        {
            string MFId = Utility.DecryptQS(HiddenFieldExam["MFId"].ToString());
            if (e.GetValue("MFId") == null)
                return;
            string CurretnMfId = e.GetValue("MFId").ToString();
            if (MFId == CurretnMfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
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
        int MExmDId = -1;
        int focucedIndex = GridViewMemberExam.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberExam.GetDataRow(focucedIndex);
            MExmDId = (int)row["MExmDId"];
        }
        if (MExmDId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                MExmDId = -1;
                Response.Redirect("AddMemberExam.aspx?MExmDId=" + Utility.EncryptQS(MExmDId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldExam["MFId"].ToString() + "&PrePgMd=" + HiddenFieldExam["PrePageMode"].ToString()
                      + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            else
            {
                Response.Redirect("AddMemberExam.aspx?MExmDId=" + Utility.EncryptQS(MExmDId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldExam["MFId"].ToString() + "&PrePgMd=" + HiddenFieldExam["PrePageMode"].ToString()
                      + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
        }
    }

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
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
        GridViewMemberExam.DataBind();
    }

    private void Delete(int MExmDId)
    {
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        try
        {
            DocMemberExamDetailManager.FindByCode(MExmDId);
            if (DocMemberExamDetailManager.Count == 1)
            {
                DocMemberExamDetailManager[0].Delete();                
                if (DocMemberExamDetailManager.Save() > 0)
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
            GridViewMemberExam.DataBind();
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }


    protected void Active(int TableId, int MfId)
    {
        switch (DeleteInActive(TableId, MfId))
        {
            case 0:
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                GridViewMemberExam.DataBind();
                break;
            case 1:
                ShowMessage("رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                break;
            case 2:
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                break;
        }
    }

    protected int DeleteInActive(int TableId, int ReqId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByTableIdTableType(TableId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberExam), ReqId);        
        if (RequestInActivesManager.Count == 1)
        {
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() > 0)
                result = 0;
            else result = 2;
        }
        else result = 1;

        return result;
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    //int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    //int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    //int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    //int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument)
                    {
                        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        int PermissionDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                        int PermissionDocUnitRes = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                        if (Permission > 0 || PermissionDocUnit > 0 || PermissionDocUnitRes > 0)
                            return true;
                        else
                            return false;
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

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string MFId = Utility.DecryptQS(HiddenFieldExam["MFId"].ToString());
        //    //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;

        int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerDocUnit = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerDocUnitRes = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || WFPerDocUnit.BtnNew || WFPerDocUnitRes.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerDocUnit.BtnEdit || WFPerDocUnitRes.BtnEdit;
        this.ViewState["btnActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || WFPerDocUnit.BtnInactive || WFPerDocUnitRes.BtnInactive;
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
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();      
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

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion
}
