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

public partial class Members_ImplementDoc_AddFinancialStatus : System.Web.UI.Page
{
    DataTable dtOfImg = null;
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

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
            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["OfsId"]) || string.IsNullOrEmpty(Request.QueryString["MFId"]))
            {
                Response.Redirect("FinancialStatus.aspx");
                return;
            }

            Session["DocUpload"] = null;
            Session["TblOfImgd"] = null;

            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;

            btnSave.Enabled = true;
            btnSave2.Enabled = true;

            if (Session["TblOfImgd"] == null)
            {
                dtOfImg = new DataTable();
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns.Add("Description");
                dtOfImg.Columns.Add("Id");
                dtOfImg.Columns["Id"].AutoIncrement = true;
                dtOfImg.Columns["Id"].AutoIncrementSeed = 1;
                dtOfImg.Constraints.Add("PK_ID", dtOfImg.Columns["Id"], true);

                Session["TblOfImgd"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfImgd"];

            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();

            SetKey();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {

        if (Session["TblOfImgd"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImgd"];

            DataRow dr = dtOfImg.NewRow();

            try
            {

                if (Session["DocUpload"] != null)
                {

                    dr[0] = "~/Image/ImplementDoc/FinancialStatus/" + System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[2] = System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[1] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[5] = txtDescImg.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }

                dr[3] = 0;
                dtOfImg.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();

                Session["DocUpload"] = null;

                txtDescImg.Text = "";
                imgEndUploadImg.ClientVisible = false;


            }
            catch (Exception)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinancialStatus.aspx?MfId=" + HiddenFieldFinancial["MFId"].ToString() + "&PgMd=" + HiddenFieldFinancial["PrePagMode"].ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = int.Parse(Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString()));
        string PageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
        if (!CheckPermitionForEdit(MfId, PageMode))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.";
            return;
        }
        string pageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
        string OfsId = Utility.DecryptQS(HiddenFieldFinancial["OfsId"].ToString());

        if (string.IsNullOrEmpty(OfsId) || string.IsNullOrEmpty(pageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        else
        {

            Enable();

            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
            TblFile.Visible = true;
            lblWarning.Visible = true;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if ((bool)MemberManager[0]["IsLock"] == true)
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;

        }
        string PageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
        string OfsId = Utility.DecryptQS(HiddenFieldFinancial["OfsId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
            {

                Insert();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(OfsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(int.Parse(OfsId));
                }

            }

        }



    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {

        TblFile.Visible = true;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        HiddenFieldFinancial["OfsId"] = Utility.EncryptQS("");
        HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        lblWarning.Visible = true;
    }

    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlp.DataSource = (DataTable)Session["TblOfImgd"];
        AspxGridFlp.DataBind();

        int Id = -1;
        if (AspxGridFlp.FocusedRowIndex > -1)
        {
            Id = AspxGridFlp.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {
            dtOfImg = (DataTable)Session["TblOfImgd"];
            dtOfImg.Rows.Find(e.Keys["Id"]).Delete();
            // dtOfImg.Rows[Id].Delete();
            Session["TblOfImgd"] = dtOfImg;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImgd"];
            AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfImgd"];


        }

    }
    #endregion

    #region Methods

    private void SetKey()
    {
        try
        {

            HiddenFieldFinancial["PrePagMode"] = Server.HtmlDecode(Request.QueryString["PrePgMd"].ToString());
            HiddenFieldFinancial["OfsId"] = Server.HtmlDecode(Request.QueryString["OfsId"]).ToString();
            HiddenFieldFinancial["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldFinancial["MFId"] = Server.HtmlDecode(Request.QueryString["MFId"]).ToString();

        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
        string OfsId = Utility.DecryptQS(HiddenFieldFinancial["OfsId"].ToString());
        string PreMode = Utility.DecryptQS(HiddenFieldFinancial["PrePagMode"].ToString());
        string MfId = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        switch (PageMode)
        {
            case "View":
                Disable();

                if (string.IsNullOrEmpty(OfsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                TblFile.Visible = false;

                btnEdit.Enabled = true;
                btnEdit2.Enabled = true;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(int.Parse(OfsId));
                ASPxRoundPanel2.HeaderText = "مشاهده";
                lblWarning.Visible = false;
                break;


            case "New":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";

                ClearForm();
                lblWarning.Visible = true;
                break;

            case "Edit":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;

                if (string.IsNullOrEmpty(OfsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                FillForm(int.Parse(OfsId));
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                lblWarning.Visible = true;
                break;

        }

        ObjectDataSource1.FilterParameters[0].DefaultValue = "1";

        CheckPermitionForEdit(int.Parse(MfId), PageMode);

    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Office/FinancialStatus/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["DocUpload"] = tempFileName;
        }
        return ret;
    }

    protected void FillForm(int OfsId)
    {
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        FinManager.FindByType(OfsId, 1);
        if (FinManager.Count == 1)
        {

            CmbName.DataBind();            
            CmbName.SelectedIndex =CmbName.Items.FindByValue(FinManager[0]["OfdId"].ToString()).Index;
            //CmbName.Value = FinManager[0]["OfdId"].ToString();
            txtDesc.Text = FinManager[0]["Description"].ToString();
            decimal Cost = Convert.ToDecimal(FinManager[0]["Value"].ToString());
            txtValue.Text = Cost.ToString("#,#");
        }

        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.OfficeFinancialStatus, OfsId);
        dtOfImg = (DataTable)Session["TblOfImgd"];
        for (int i = 0; i < attachManager.Count; i++)
        {
            DataRow dr = dtOfImg.NewRow();
            dr[0] = attachManager[i]["FilePath"].ToString();
            dr[1] = attachManager[i]["FilePath"].ToString();
            dr[5] = attachManager[i]["Description"].ToString();

            dr[2] = Path.GetFileName(attachManager[0]["FilePath"].ToString());
            dr[3] = 1;
            dr[4] = attachManager[i][0];
            dtOfImg.Rows.Add(dr);

        }
        dtOfImg.AcceptChanges();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

    }

    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(FinManager);
        trans.Add(attachManager);


        try
        {

            DataRow dr = FinManager.NewRow();
            dr["Type"] = 1;
            dr["OfReId"] = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());

            if (CmbName.Value != null)
                dr["OfdId"] = CmbName.Value;
            dr["Value"] = txtValue.Text;

            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["CreateDate"] = Utility.GetDateOfToday();
            FinManager.AddRow(dr);
            trans.BeginSave();
            if (FinManager.Save() == 1)
            {

                dtOfImg = (DataTable)Session["TblOfImgd"];
                int OfsId = int.Parse(FinManager[0]["OfsId"].ToString());
                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                        drImg["RefTable"] = OfsId;
                        drImg["AttId"] = 1;
                        //drImg["AtContent"] = dtOfImg.Rows[i]["Image"];
                        drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        attachManager.AddRow(drImg);
                        int imgcnt = attachManager.Save();
                        attachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImg.Rows[i].BeginEdit();
                            dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                            dtOfImg.Rows[i].EndEdit();

                            if (!string.IsNullOrEmpty(dtOfImg.Rows[i]["ImgUrl"].ToString()))
                            {

                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);
                            }

                        }
                    }

                }
                trans.EndSave();

                btnEdit2.Enabled = true;
                btnEdit.Enabled = true;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                HiddenFieldFinancial["OfsId"] = Utility.EncryptQS(OfsId.ToString());
                HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }
            else
            {
                trans.CancelSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
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

    }

    protected void Edit(int OfsId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(FinManager);
        trans.Add(attachManager);

        try
        {
            FinManager.FindByType(OfsId, 1);
            if (FinManager.Count == 1)
            {
                FinManager[0].BeginEdit();
                if (CmbName.Value != null)
                    FinManager[0]["OfdId"] = CmbName.Value;
                FinManager[0]["Description"] = txtDesc.Text;
                FinManager[0]["Value"] = txtValue.Text;
                FinManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                FinManager[0]["ModifiedDate"] = DateTime.Now;
                FinManager[0].EndEdit();
                trans.BeginSave();
                if (FinManager.Save() == 1)
                {
                    dtOfImg = (DataTable)Session["TblOfImgd"];

                    if (dtOfImg.GetChanges() != null)
                    {
                        DataRow[] delRows = dtOfImg.Select("Mode='1'", null, DataViewRowState.Deleted);
                        for (int i = 0; i < delRows.Length; i++)
                        {
                            attachManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                            attachManager[0].Delete();
                            attachManager.Save();
                        }
                        attachManager.DataTable.AcceptChanges();
                        DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {
                                DataRow drImg = attachManager.NewRow();
                                drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                                drImg["RefTable"] = OfsId;
                                drImg["AttId"] = 1;
                                drImg["FilePath"] = insRows[i]["ImgUrl"].ToString();
                                drImg["IsValid"] = 1;
                                drImg["Description"] = insRows[i]["Description"].ToString();
                                drImg["UserId"] = Utility.GetCurrentUser_UserId();
                                drImg["ModfiedDate"] = DateTime.Now;
                                attachManager.AddRow(drImg);
                                int imgcnt = attachManager.Save();
                                attachManager.DataTable.AcceptChanges();
                                if (imgcnt == 1)
                                {
                                    if (!string.IsNullOrEmpty(insRows[i]["ImgUrl"].ToString()))
                                    {
                                        string ImgSoource = Server.MapPath("~/image/Temp/") + insRows[i]["fileName"].ToString();
                                        string ImgTarget = Server.MapPath(insRows[i]["ImgUrl"].ToString());
                                        File.Copy(ImgSoource, ImgTarget, true);
                                    }
                                }
                            }
                        }
                    }

                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";

            }
        }

        catch (Exception err)
        {
            trans.CancelSave();

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
    }

    protected void Enable()
    {
        CmbName.Enabled = true;
        txtDesc.Enabled = true;
        txtValue.Enabled = true;
        AspxGridFlp.Columns["clnDelete"].Visible = true;

    }

    protected void Disable()
    {
        CmbName.Enabled = false;
        txtDesc.Enabled = false;
        txtValue.Enabled = false;
        AspxGridFlp.Columns["clnDelete"].Visible = false;

    }

    protected void ClearForm()
    {
        CmbName.DataBind();
        CmbName.SelectedIndex = -1;
        txtDesc.Text = "";
        txtDescImg.Text = "";
        txtValue.Text = "";
        dtOfImg = (DataTable)Session["TblOfImgd"];
        dtOfImg.Rows.Clear();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

    }

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId, string PageMode)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortalImpDocument(TableId, PageMode);
        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
        btnEdit.Enabled = btnEdit2.Enabled = WFPermission.BtnEdit;
        BtnNew.Enabled = BtnNew2.Enabled = WFPermission.BtnNew;
        return WFPermission.BtnEdit;
    }
    #endregion

    //private Boolean CheckPermitionForEdit(int TableId)
    //{
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    WorkFlowStateManager.ClearBeforeFill = true;
    //    int TaskOrder = -1;
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //        if (TaskOrder != 0)
    //        {
    //            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
    //            DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
    //            if (dtWorkFlowLastState.Rows.Count > 0)
    //            {
    //                int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
    //                int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
    //                int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
    //                int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
    //                int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
    //                int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;

    //                if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
    //                {
    //                    DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
    //                    if (dtWorkFlowState.Rows.Count > 0)
    //                    {
    //                        int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
    //                        int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
    //                        int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
    //                        if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
    //                        {
    //                            if (FirstNmcIdType == 1)
    //                            {
    //                                if (FirstNmcId == Utility.GetCurrentUser_MeId())
    //                                {
    //                                    return true;
    //                                }
    //                                else
    //                                    return false;
    //                            }
    //                            else
    //                            {
    //                                return false;
    //                            }
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

    //private void CheckWorkFlowPermission()
    //{
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    int TaskOrder = -1;
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }
    //    if (TaskOrder != 0)
    //    {
    //        string PageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
    //        CheckWorkFlowPermissionForSave(PageMode);
    //        if (PageMode != "New")
    //            CheckWorkFlowPermissionForEdit(PageMode);
    //    }
    //}

    //private void CheckWorkFlowPermissionForSave(string PageMode)
    //{
    //    string MfId = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());
    //    if (CheckPermitionForEdit(int.Parse(MfId)))
    //    {
    //        BtnNew.Enabled = true;
    //        BtnNew2.Enabled = true;
    //        switch (PageMode)
    //        {
    //            case "New":
    //                btnSave.Enabled = true;
    //                btnSave2.Enabled = true;

    //                break;
    //            case "View":
    //                btnSave.Enabled = false;
    //                btnSave2.Enabled = false;
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        BtnNew.Enabled = false;
    //        BtnNew2.Enabled = false;
    //        btnSave.Enabled = false;
    //        btnSave2.Enabled = false;
    //        this.DivReport.Visible = true;
    //        //this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
    //        this.LabelWarning.Text = "در اين مرحله از جريان كار،امكان ويرايش اطلاعات براي شما وجود ندارد.";
    //    }
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;

    //}

    //private void CheckWorkFlowPermissionForEdit(string PageMode)
    //{
    //    string MfId = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());
    //    if (CheckPermitionForEdit(int.Parse(MfId)))
    //    {
    //        switch (PageMode)
    //        {
    //            case "Edit":
    //                btnSave.Enabled = true;
    //                btnSave2.Enabled = true;
    //                break;
    //            case "View":
    //                btnEdit.Enabled = true;
    //                btnEdit2.Enabled = true;
    //                btnSave.Enabled = true;
    //                btnSave2.Enabled = true;
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        switch (PageMode)
    //        {
    //            case "View":
    //                btnEdit.Enabled = false;
    //                btnEdit2.Enabled = false;
    //                break;
    //        }
    //        btnSave.Enabled = false;
    //        btnSave2.Enabled = false;
    //    }

    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;

    //}
    #endregion
}
