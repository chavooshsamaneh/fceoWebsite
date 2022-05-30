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

public partial class Members_Documents_DocResponsibility : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
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

        if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("MemberFiles.aspx");
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["IsEdited_EmpDocRes"] = false;


            HiddenFieldMeFileDetail["PrePageMode"] = Request.QueryString["PgMd"];
            HiddenFieldMeFileDetail["MFId"] = Request.QueryString["MFId"];
            string MFId = Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString());
            int DocRequestType = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
            if (DocMemberFileManager.Count == 1)
            {
                ObjdsMemberFileDetail.SelectParameters["MfId"].DefaultValue = MFId;
                ObjdsMemberFileDetail.SelectParameters["MeId"].DefaultValue = DocMemberFileManager[0]["MeId"].ToString();
                HiddenFieldMeFileDetail["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                DocRequestType = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
            }
            string MeId = Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString());
            CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
        }

        MemberInfoUserControl1.MeId = Utility.GetCurrentUser_MeId();
        MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberJobConfirm.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&MeId=" + HiddenFieldMeFileDetail["MeId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&MeId=" + HiddenFieldMeFileDetail["MeId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&MeId=" + HiddenFieldMeFileDetail["MeId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    #region Btn Click
    //protected void BtnNew_Click(object sender, EventArgs e)
    //{
    //    NextPage("New");
    //}

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    int MfId = -1;
    //    int focucedIndex = GridViewMeFiledetail.FocusedRowIndex;

    //    if (focucedIndex > -1)
    //    {
    //        DataRow row = GridViewMeFiledetail.GetDataRow(focucedIndex);
    //        MfId = (int)row["MfId"];
    //        if (HiddenFieldMeFileDetail["MFId"] != null)
    //        {
    //            int CurrentMFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
    //            if (CurrentMFId == MfId)
    //            {
    //                if (CheckPermitionForEdit(CurrentMFId))
    //                {
    //                    NextPage("Edit");
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "شما قادر به ویرایش اطلاعات نمی باشید.";
    //                }
    //            }
    //            else
    //            {
    //                if (CheckIfDocResponsblityBelongToOldSystem(MfId))
    //                {
    //                    if (CheckPermitionForEdit(CurrentMFId))
    //                    {
    //                        NextPage("Edit");
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "شما قادر به ویرایش اطلاعات نمی باشید.";
    //                    }
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
    //                }
    //            }
    //        }
    //    }
    //}

    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    NextPage("View");
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&MeId=" + HiddenFieldMeFileDetail["MeId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString()
                 + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&MeId=" + HiddenFieldMeFileDetail["MeId"].ToString() + "&PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString());
    }

    //protected void btnInActive_Click(object sender, EventArgs e)
    //{
    //    if (GridViewMeFiledetail.FocusedRowIndex > -1)
    //    {
    //        DataRow dr = GridViewMeFiledetail.GetDataRow(GridViewMeFiledetail.FocusedRowIndex);
    //        if (dr != null)
    //        {
    //            if (Convert.ToBoolean(dr["InActive"]))
    //            {
    //                ShowMessage("رکورد انتخاب شده غیر فعال می باشد");
    //                return;
    //            }
    //            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
    //            int SelectedMfId = int.Parse(dr["MfId"].ToString());
    //            int MfdId = int.Parse(dr["MfdId"].ToString());
    //            if (MFId == SelectedMfId)
    //                Delete(MfdId);
    //            else
    //                InsertInActive(MfdId, MFId, (int)TSP.DataManager.TableCodes.DocMemberFileDetail, (int)TSP.DataManager.TableCodes.DocMemberFile);
    //        }
    //    }
    //}

    //protected void btnActive_Click(object sender, EventArgs e)
    //{
    //    if (IsPageRefresh)
    //        return;
    //    try
    //    {
    //        if (GridViewMeFiledetail.FocusedRowIndex > -1)
    //        {
    //            DataRow dr = GridViewMeFiledetail.GetDataRow(GridViewMeFiledetail.FocusedRowIndex);
    //            if (dr != null)
    //            {
    //                if (!Convert.ToBoolean(dr["InActive"]))
    //                {
    //                    ShowMessage("رکورد انتخاب شده فعال می باشد");
    //                    return;
    //                }
    //                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
    //                int SelectedMfId = int.Parse(dr["MfId"].ToString());
    //                int MfdId = int.Parse(dr["MfdId"].ToString());
    //                Active(MfdId, MFId);
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);

    //        ShowMessage("خطایی در ذخیره رخ داده است");
    //        if (Utility.ShowExceptionError())
    //        {
    //            this.LabelWarning.Text += err.Message;
    //        }

    //    }
    //}

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }
    #endregion

    #region Grid Events
    protected void GridViewMeFiledetail_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (HiddenFieldMeFileDetail["MFId"] != null)
        {
            string MFId = Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString());
            if (e.GetValue("MfId") != null)
            {
                string CurretnMfId = e.GetValue("MfId").ToString();
                if (MFId == CurretnMfId)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
    }

    protected void GridViewMeFiledetail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
        {
            e.Editor.Style["direction"] = "ltr";
        }
    }

    protected void GridViewMeFiledetail_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }
    #endregion

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int MfdId = -1;
        int focucedIndex = GridViewMeFiledetail.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMeFiledetail.GetDataRow(focucedIndex);
            MfdId = (int)row["MfdId"];
        }
        if (MfdId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                MfdId = -1;
                Response.Redirect("AddDocResponsibility.aspx?MfdId=" + Utility.EncryptQS(MfdId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PrePgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString()
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            else
            {
                Response.Redirect("AddDocResponsibility.aspx?MfdId=" + Utility.EncryptQS(MfdId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldMeFileDetail["MFId"].ToString() + "&PrePgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString()
                     + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
        }
    }

    #region CheckWFPermissions
    //private Boolean CheckPermitionForEdit(int TableId)
    //{
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortal(TableId, "");
    //    BtnNew.Enabled = btnNew2.Enabled
    //      = btnEdit.Enabled = btnEdit2.Enabled
    //      = btnInActive.Enabled = btnInActive2.Enabled = btnActive.Enabled = btnActive2.Enabled = WFPermission.BtnNew;
    //    return WFPermission.BtnEdit;
    //}
    #endregion

    #region InActive-Delete
    private void Delete(int MfdId)
    {
        try
        {
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            DocMemberFileDetailManager.FindByCode(MfdId);
            if (DocMemberFileDetailManager.Count == 1)
            {
                DocMemberFileDetailManager[0].Delete();

                int cn = DocMemberFileDetailManager.Save();
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
            GridViewMeFiledetail.DataBind();
        }
        catch (Exception err)
        {
            SetDeleteError(err);
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
        GridViewMeFiledetail.DataBind();
    }

    protected void Active(int MfdId, int MfId)
    {
        switch (DeleteInActive(MfdId, MfId))
        {
            case 0:
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                GridViewMeFiledetail.DataBind();
                break;
            case 1:
                ShowMessage("رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                break;
            case 2:
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                break;
        }
    }

    protected int DeleteInActive(int MfdId, int MfId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByReqIdAndTableId(MfId, MfdId);
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
    #endregion

    #region Error-Warning Methods
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
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
    #endregion

    #region CheckPermission Methods
    /// <summary>
    /// تنها در درخواست صدور- ارتقاء پایه-انتقالی-درج صلاحیت جدید امکان تغییر پایه و صلاحیت وجود دارد
    /// </summary>
    /// <param name="DocType"></param>
    /// <returns> ArrayListPer[0] : CanNew ; ArrayListPer[1] : CanEdit; ArrayListPer[2] : CanInActive </returns>
    /// 
    private ArrayList CheckPermissionByRequestType(int DocType)
    {
        ArrayList Per = new ArrayList();
        Boolean CanNew = false;
        Boolean CanEdit = false;
        Boolean CanInActive = false;
        Per.Add(CanNew);
        Per.Add(CanEdit);
        Per.Add(CanInActive);
        string RequestComment = "";
        switch (DocType)
        {
            case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                RequestComment = "در درخواست المثنی پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                RequestComment = "در درخواست تمدید پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                CanEdit = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                CanNew = true;
                CanEdit = true;
                CanInActive = true;
                break;
        }
        if (!string.IsNullOrEmpty(RequestComment))
        {
            txtRequestComment.Visible = true;
            txtRequestComment.Text = RequestComment;
        }
        else
            txtRequestComment.Visible = false;
        Per[0] = CanNew;
        Per[1] = CanEdit;
        Per[2] = CanInActive;
        return Per;
    }

    private Boolean CheckIfDocResponsblityBelongToOldSystem(int SelectedResponsblityMfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(SelectedResponsblityMfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count != 1)
        {
            return false;
        }
        int MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
        int Type = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
        //Boolean IsConfirm=Convert.ToBoolean(DocMemberFileManager[0]["IsConfirm"]);
        //Boolean HasSerialNo = false;
        //if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
        //    HasSerialNo = true;
        if (Type != (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument
            && Type != (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew)
        {
            return false;
        }
        //درخواست چاپ شده تایید شده داشته باشد
        DocMemberFileManager.FindDocumentByRequestType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, 1, -1, 1);
        if (DocMemberFileManager.Count > 0)
            return false;

        if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument)
        {
            DocMemberFileManager.FindDocumentByRequestType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, -1, (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew, 0);
            if (DocMemberFileManager.Count > 0)
                return false;
            else
                return true;
        }
        else if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew)
        {
            return true;
        }
        return false;
    }
    #endregion

    private void BackToManagementPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFiles.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFiles.aspx");
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
