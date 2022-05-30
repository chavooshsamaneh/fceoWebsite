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

public partial class Employee_ImplementDoc_AddFinancialStatus : System.Web.UI.Page
{
    DataTable dtOfImg = null;

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
            Session["IsEdited_EmpImpFinan"] = false;
            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["OfsId"]) || string.IsNullOrEmpty(Request.QueryString["MFId"]))
            {
                Response.Redirect("FinancialStatus.aspx");
                return;
            }
            HiddenFieldFinancial["JudgeId"] = "";

            Session["DocUpload"] = null;
            Session["TblOfImgd"] = null;

            TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

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

            TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
            TblFile.Visible = true;

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
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
            else if (PageMode == "Judge")
            {
                string JudgeId = Utility.DecryptQS(HiddenFieldFinancial["JudgeId"].ToString());
                if (string.IsNullOrEmpty(JudgeId))
                {
                    InsertJudge();
                } 
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        TblFile.Visible = true;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        HiddenFieldFinancial["OfsId"] = Utility.EncryptQS("");
        HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
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
            Session["TblOfImgd"] = dtOfImg;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImgd"];
            AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfImgd"];


        }

    }

    protected void CmbName_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            int OfdId = Convert.ToInt32(e.Parameter);
            TSP.DataManager.DocOffOfficeFactorDocumentsManager DocOffOfficeFactorDocumentsManager = new TSP.DataManager.DocOffOfficeFactorDocumentsManager();
            DocOffOfficeFactorDocumentsManager.FindByCode(OfdId, (int)TSP.DataManager.DocOffOfficeFactorDocumentsType.FinancialStatus);
            if (DocOffOfficeFactorDocumentsManager.Count > 0)
            {
                txtNameValue.Text = DocOffOfficeFactorDocumentsManager[0]["Value"].ToString();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region Methods

    private void SetKey()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
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

                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(int.Parse(OfsId));
                ASPxRoundPanel2.HeaderText = "مشاهده";                
                break;


            case "New":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";

                ClearForm();                
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
                break;
            case "Judge":
                int NmcId = FindNmcId();
                if (NmcId == -1)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                TrainingJudgmentManager.FindByNmcId(NmcId, int.Parse(Utility.DecryptQS(HiddenFieldFinancial["OfsId"].ToString())), 3);
                if (TrainingJudgmentManager.Count > 0)
                {
                    HiddenFieldFinancial["JudgeId"] = Utility.EncryptQS(TrainingJudgmentManager[0][TrainingJudgmentManager.Count - 1].ToString());
                    string JudgeId = Utility.DecryptQS(HiddenFieldFinancial["JudgeId"].ToString());                  
                }
                Disable();

                if (string.IsNullOrEmpty(OfsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                TblFile.Visible = false;
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                //  btnSave.Enabled = false;
                //  btnSave2.Enabled = false;
                FillForm(int.Parse(OfsId));
                ASPxRoundPanel2.HeaderText = "مشاهده";                
                break;

        }

        ObjectDataSource1.FilterParameters[0].DefaultValue = "1";
        CheckWorkFlowPermission();

    }

    private string SaveImage(UploadedFile uploadedFile)
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

    private void FillForm(int OfsId)
    {
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        FinManager.FindByType(OfsId, 1);
        if (FinManager.Count == 1)
        {

            CmbName.DataBind();
            CmbName.SelectedIndex = CmbName.Items.FindByValue(FinManager[0]["OfdId"].ToString()).Index;
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

    private void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        trans.Add(WorkFlowStateManager);
        trans.Add(FinManager);
        trans.Add(attachManager);


        try
        {
            string MfId = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());
            DataRow dr = FinManager.NewRow();
            dr["Type"] = 1;
            dr["OfReId"] = MfId;

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

                                //string ImgSoource = Session["PlanAttachStatus"].ToString();
                                //string ImgTarget = Server.MapPath("~/Image/TechnicalServices/Plans/") + Path.GetFileName(Session["PlanAttachStatus"].ToString());
                                //File.Copy(ImgSoource, ImgTarget, true);
                            }

                        }
                    }

                }

                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_EmpImpFinan"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(MfId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnEdit2.Enabled = per.CanEdit;
                    btnEdit.Enabled = per.CanEdit;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    Session["IsEdited_EmpImpFinan"] = true;
                    HiddenFieldFinancial["OfsId"] = Utility.EncryptQS(OfsId.ToString());
                    HiddenFieldFinancial["PageMode"] = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                }
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

    private void Edit(int OfsId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        trans.Add(WorkFlowStateManager);
        trans.Add(FinManager);
        trans.Add(attachManager);

        try
        {
            string MfId = Utility.DecryptQS(HiddenFieldFinancial["MFId"].ToString());
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
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_EmpImpFinan"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(MfId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        trans.EndSave();
                        Session["IsEdited_EmpImpFinan"] = true;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
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

    private void InsertJudge()
    {
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        try
        {
            
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
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    private void Enable()
    {
        CmbName.Enabled = true;
        txtDesc.Enabled = true;
        txtValue.Enabled = true;
        AspxGridFlp.Columns[2].Visible = true;

    }

    private void Disable()
    {
        CmbName.Enabled = false;
        txtDesc.Enabled = false;
        txtValue.Enabled = false;
        AspxGridFlp.Columns[2].Visible = false;

    }

    private void ClearForm()
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

    private int FindNmcId()
    {
        int NcId = -1;
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;

        //NmcId = NezamChartManager.FindNmcIdByNcId(NcId, UserId, LoginManager);
        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
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

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldFinancial["PageMode"].ToString());
            // CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
        }
        else
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }
    #endregion
  
}
