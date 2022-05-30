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
using System.IO;

public partial class Members_Documents_DocumentAttachment : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["ImgAttachmentURL"] = null;
            Session["IsEdited_EmpDocAtch"] = false;
            if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("MemberFiles.aspx");
            }

            HiddenFieldDocumentAttach["MFId"] = Request.QueryString["MFId"].ToString();
            HiddenFieldDocumentAttach["PrePageMode"] = Request.QueryString["PgMd"].ToString();
            string MFId = Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString());
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
                    HiddenFieldDocumentAttach["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
            }
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, int.Parse(MFId));
            GridViewAttachment.DataBind();

            CheckPermitionForEdit(int.Parse(MFId));
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            string MeId = Utility.DecryptQS(HiddenFieldDocumentAttach["MeId"].ToString());
            CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
        }
        MemberInfoUserControl1.MeId = Utility.GetCurrentUser_MeId();
        MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;

        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberJobConfirm.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&MeId=" + HiddenFieldDocumentAttach["MeId"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&MeId=" + HiddenFieldDocumentAttach["MeId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&MeId=" + HiddenFieldDocumentAttach["MeId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertMemberFileAttachment();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteMemberFileAttachment();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
              && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&MeId=" + HiddenFieldDocumentAttach["MeId"].ToString()
                + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldDocumentAttach["MFId"].ToString() + "&PgMd=" + HiddenFieldDocumentAttach["PrePageMode"].ToString() + "&MeId=" + HiddenFieldDocumentAttach["MeId"].ToString());
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void flpAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageAttach(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    #region Methods
    private void InsertMemberFileAttachment()
    {
        if (Utility.IsDBNullOrNullValue(Session["ImgAttachmentURL"]))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " فایل مورد نظر را انتخاب نمایید";
            return;
        }

        string fileNameImg = "", pathAx = "", extension = "";
        // byte[] img = null;
        //  bool AxImg = false;
        try
        {
            string MfId = Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString());
            TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.Add(attManager);

            TransactionManager.BeginSave();
            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.DocMemberFile;
            dr["RefTable"] = MfId;
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDescription.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;
            if (!Utility.IsDBNullOrNullValue(Session["ImgAttachmentURL"]))
                dr["FilePath"] = Session["ImgAttachmentURL"].ToString();
            //try
            //{

            //    extension = Path.GetExtension(flp.FileName);
            //    extension = extension.ToLower();
            //    if (flp.HasFile)
            //    {
            //        fileNameImg = Utility.GenerateName(Path.GetExtension(flp.FileName));
            //        pathAx = Server.MapPath("~/image/Temp/");
            //        flp.SaveAs(pathAx + fileNameImg);
            //        dr["AtContent"] = DBNull.Value;
            //        dr["FilePath"] = "~/Image/Members/DocumentAttachment/" + fileNameImg;
            //        // AxImg = true;
            //    }
            //}
            //catch
            //{
            //    TransactionManager.CancelSave();
            //}

            attManager.AddRow(dr);
            if (attManager.Save() != 1)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            //int UpdateState = -1;
            //if (!(Convert.ToBoolean(Session["IsEdited_EmpDocAtch"].ToString())))
            //{
            //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
            //    int UpdateTableType = (int)TSP.DataManager.TableCodes.Attachment;
            //    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(MfId), UpdateTableType, "ثبت فایل پیوست", Utility.GetCurrentUser_UserId());
            //}
            //if (UpdateState == -4)
            //{
            //    TransactionManager.CancelSave();
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
            //}
            //else
            //{
            TransactionManager.EndSave();
            Session["IsEdited_EmpDocAtch"] = true;
            Session["ImgAttachmentURL"] = null;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            txtDescription.Text = "";
            GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, int.Parse(Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString())));
            GridViewAttachment.DataBind();
            //}
        }
        catch (Exception err)
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
                    txtDescription.Text = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                txtDescription.Text = "";
            }
        }

    }

    private void DeleteMemberFileAttachment()
    {
        int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, int.Parse(Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString())));
        GridViewAttachment.DataBind();

        if (GridViewAttachment.FocusedRowIndex > -1)
        {
            DataRow row = GridViewAttachment.GetDataRow(GridViewAttachment.FocusedRowIndex);
            AttachId = (int)row["AttachId"];
        }
        if (AttachId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, int.Parse(Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString())));
                        GridViewAttachment.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
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
        }
    }

    //private void CheckWorkFlowPermission()
    //{
    //    CheckWorkFlowPermissionForSave();
    //}

    //private void CheckWorkFlowPermissionForSave()
    //{
    //    //****TableId
    //    string MFId = Utility.DecryptQS(HiddenFieldDocumentAttach["MFId"].ToString());
    //    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;

    //    int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

    //    TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MFId), Utility.GetCurrentUser_UserId());
    //    this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
    //    // this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
    //    this.ViewState["BtnDelete"] = btnDelete.Enabled = btnDelete2.Enabled = WFPer.BtnInactive;
    //}

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

    #region CheckWFPermissions
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortal(TableId, "");
        BtnNew.Enabled = btnNew2.Enabled
         = WFPermission.BtnNew;
        btnDelete.Enabled = btnDelete2.Enabled = WFPermission.BtnEdit;
        return WFPermission.BtnEdit;
    }
    #endregion

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        do
        {
            System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
            ret = Path.GetRandomFileName() + ImageType.Extension;
        } while ((File.Exists(MapPath("~/Image/Members/DocumentAttachment/") + ret) == true));

        string FileName = "";
        Session["ImgAttachmentURL"] = "~/Image/Members/DocumentAttachment/" + ret;
        FileName = MapPath("~/Image/Members/DocumentAttachment/") + ret;

        uploadedFile.SaveAs(FileName, true);
        return ret;
    }
    #endregion

}
