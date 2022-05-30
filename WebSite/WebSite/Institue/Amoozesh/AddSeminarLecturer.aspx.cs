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

public partial class Institue_Amoozesh_AddSeminarLecturer : System.Web.UI.Page
{
    DataTable dtOfImgTeacher = new DataTable();
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["TeacherUpload"] = null;

            if (Session["TblOfImg7"] == null)
            {
                dtOfImgTeacher = new DataTable();
                dtOfImgTeacher.Columns.Add("Image", typeof(byte[]));
                dtOfImgTeacher.Columns.Add("ImgUrl");
                dtOfImgTeacher.Columns.Add("TempImgUrl");
                dtOfImgTeacher.Columns.Add("fileName");
                dtOfImgTeacher.Columns.Add("Mode");
                dtOfImgTeacher.Columns.Add("Code");
                dtOfImgTeacher.Columns.Add("Description");
                dtOfImgTeacher.Columns.Add("Id");
                dtOfImgTeacher.Columns.Add("TeId");

                dtOfImgTeacher.Columns["Id"].AutoIncrement = true;
                dtOfImgTeacher.Columns["Id"].AutoIncrementSeed = 1;

                Session["TblOfImg7"] = dtOfImgTeacher;
            }
            else
                dtOfImgTeacher = (DataTable)Session["TblOfImg7"];

            AspxGridFlpTeacher.DataSource = dtOfImgTeacher;
            AspxGridFlpTeacher.DataBind();

            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;

            btnSave.Enabled = true;
            btnSave2.Enabled = true;


            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeId"]))
            {
                Response.Redirect("SeminarLecturer.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                TeacherId.Value = Server.HtmlDecode(Request.QueryString["TeId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TeId = Utility.DecryptQS(TeacherId.Value);


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":

                    if (string.IsNullOrEmpty(TeId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;

                    FillForm(int.Parse(TeId));
                   
                    Disable();
                    AspxGridFlpTeacher.Columns[2].Visible = false;
                    tblTeacherFile.Rows[0].Visible = false;
                    tblTeacherFile.Rows[1].Visible = false;
                    tblTeacherFile.Rows[2].Visible = false;

                    ASPxRoundPanel1.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                   
                    ASPxRoundPanel1.HeaderText = "جدید";

                    break;

                case "Edit" :

                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    if (string.IsNullOrEmpty(TeId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                   
                    FillForm(int.Parse(TeId));
                    Enable();

                  
                    ASPxRoundPanel1.HeaderText = "ویرایش";
                    break;
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
     
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void FillForm(int TeId)
    {
        TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
        try
        {
            DataTable dt = TeManager.SelectLecturer(TeId);
            if (TeManager.Count == 1)
            {
                txtTeAddress.Text = TeManager[0]["Address"].ToString();
                txtTeDate.Text = TeManager[0]["BirthDate"].ToString();
                txtTeEmail.Text = TeManager[0]["Email"].ToString();
                txtTeFamily.Text = TeManager[0]["Family"].ToString();
                txtTeFatherName.Text = TeManager[0]["Father"].ToString();
                txtTeIdNo.Text = TeManager[0]["IdNo"].ToString();
                txtTeMobileNo.Text = TeManager[0]["MobileNo"].ToString();
                txtTeName.Text = TeManager[0]["Name"].ToString();
                txtTeSSN.Text = TeManager[0]["SSN"].ToString();
                txtTeTel.Text = TeManager[0]["Tel"].ToString();
                txtTeDesc.Text = TeManager[0]["Description"].ToString();

                cmbTeMajor.DataBind();
                cmbTeLicence.DataBind();
                cmbTeLicence.SelectedIndex = cmbTeLicence.Items.IndexOfValue(dt.Rows[0]["LiId"].ToString());
                cmbTeMajor.SelectedIndex = cmbTeMajor.Items.IndexOfValue(dt.Rows[0]["MjId"].ToString());

                if (Convert.ToBoolean(TeManager[0]["InActive"]))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;

                }

                #region AttachmentFile
                TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
              
                attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, TeId);
                dtOfImgTeacher = (DataTable)Session["TblOfImg7"];
                for (int i = 0; i < attachManager.Count; i++)
                {
                    DataRow dr = dtOfImgTeacher.NewRow();
                    dr[0] = attachManager[i]["AtContent"];
                    dr[1] = attachManager[i]["FilePath"].ToString();
                    dr[2] = attachManager[i]["FilePath"].ToString();
                    dr[6] = attachManager[i]["Description"].ToString();

                    string fileName = Path.GetFileName(attachManager[0]["FilePath"].ToString());
                    dr[3] = fileName;
                    dr[4] = 1;
                    dr[5] = attachManager[i][0];
                    dtOfImgTeacher.Rows.Add(dr);

                }
                dtOfImgTeacher.AcceptChanges();
                AspxGridFlpTeacher.DataSource = dtOfImgTeacher;
                AspxGridFlpTeacher.DataBind();
                #endregion
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {        
        
        TeacherId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel1.HeaderText = "جدید";
        ClearForm();
        Enable();

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        AspxGridFlpTeacher.Columns[2].Visible = true;
        tblTeacherFile.Rows[0].Visible = true;
        tblTeacherFile.Rows[1].Visible = true;
        tblTeacherFile.Rows[2].Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string TeId = Utility.DecryptQS(TeacherId.Value);

        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
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
               
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel1.HeaderText = "ویرایش";
            }
        }
        AspxGridFlpTeacher.Columns[2].Visible = true;
        tblTeacherFile.Rows[0].Visible = true;
        tblTeacherFile.Rows[1].Visible = true;
        tblTeacherFile.Rows[2].Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string TeId = Utility.DecryptQS(TeacherId.Value);
       

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

                if (string.IsNullOrEmpty(TeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(TeId));
                }

            }

        }
    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfImg7"] = null;
        Response.Redirect("SeminarLecturer.aspx");

    }
    protected void flpTeacher_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageTeacher(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void btnAddTeFlp_Click(object sender, EventArgs e)
    {
        byte[] img = null;

        if (Session["TblOfImg7"] != null)
        {
            dtOfImgTeacher = (DataTable)Session["TblOfImg7"];

            DataRow dr = dtOfImgTeacher.NewRow();

            try
            {
                if (Session["TeacherUpload"] != null)
                {

                    dr[0] = img;
                    dr[1] = "~/Image/Seminar/TeacherHistory/" + System.IO.Path.GetFileName(Session["TeacherUpload"].ToString());
                    dr[3] = System.IO.Path.GetFileName(Session["TeacherUpload"].ToString());
                    dr[2] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["TeacherUpload"].ToString());
                    dr[6] = txtDescTeImg.Text;


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }

                //if (dtOfImgTeacher.Rows.Count == 0)
                //    dr["TeId"] = 1;
                //else
                //    dr["TeId"] = int.Parse(dtOfImgTeacher.Rows[dtOfImgTeacher.Rows.Count - 1]["Id"].ToString()) + 1;


                dr[4] = 0;
                dtOfImgTeacher.Rows.Add(dr);
                AspxGridFlpTeacher.DataSource = dtOfImgTeacher;
                AspxGridFlpTeacher.DataBind();

                Session["TeacherUpload"] = null;

                txtDescTeImg.Text = "";
                ImgFlpTeacher.ClientVisible = false;

            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
    }
  
    protected void AspxGridFlpTeacher_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlpTeacher.DataSource = (DataTable)Session["TblOfImg7"];
        AspxGridFlpTeacher.DataBind();

        int Id = -1;
        if (AspxGridFlpTeacher.FocusedRowIndex > -1)
        {
            Id = AspxGridFlpTeacher.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfImgTeacher = (DataTable)Session["TblOfImg7"];
            dtOfImgTeacher.Rows[Id].Delete();
            Session["TblOfImg7"] = dtOfImgTeacher;
            AspxGridFlpTeacher.DataSource = (DataTable)Session["TblOfImg7"];
            AspxGridFlpTeacher.DataBind();
            dtOfImgTeacher = (DataTable)Session["TblOfImg7"];


        }
    }
    #endregion

    #region Methods
    protected void Enable()
    {
        txtDescTeImg.Enabled = true;
        txtTeAddress.Enabled = true;
        txtTeDate.Enabled = true;
        txtTeEmail.Enabled = true;
        txtTeFamily.Enabled = true;
        txtTeFatherName.Enabled = true;
        txtTeIdNo.Enabled = true;
        txtTeMobileNo.Enabled = true;
        txtTeName.Enabled = true;
        txtTeSSN.Enabled = true;
        txtTeTel.Enabled = true;
        cmbTeLicence.Enabled = true;
        cmbTeMajor.Enabled = true;
        txtTeDesc.Enabled = true;

    }
    protected void Disable()
    {
        txtDescTeImg.Enabled = false;
        txtTeAddress.Enabled = false;
        txtTeDate.Enabled = false;
        txtTeEmail.Enabled = false;
        txtTeFamily.Enabled = false;
        txtTeFatherName.Enabled = false;
        txtTeIdNo.Enabled = false;
        txtTeMobileNo.Enabled = false;
        txtTeName.Enabled = false;
        txtTeSSN.Enabled = false;
        txtTeTel.Enabled = false;
        cmbTeLicence.Enabled = false;
        cmbTeMajor.Enabled = false;
        txtTeDesc.Enabled = false;
    }
    protected string SaveImageTeacher(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Seminar/TeacherHistory/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TeacherUpload"] = tempFileName;
        }
        return ret;
    }

    protected void ClearForm()
    {
        txtTeDate.Text = "";
        txtTeAddress.Text = "";
        txtTeDesc.Text = "";

        //for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        //{
        //    if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
        //    {
        //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
        //        txt.Text = "";
        //    }
        //    else if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxComboBox)
        //    {
        //        DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel2.Controls[i];
        //        cmb.DataBind();
        //        cmb.SelectedIndex = -1;
        //    }

        //}
        txtTeAddress.Text = txtTeDate.Text = txtTeDesc.Text = txtTeEmail.Text =
            txtTeFamily.Text = txtTeFatherName.Text = txtTeIdNo.Text = txtTeMobileNo.Text =
            txtTeName.Text = txtTeSSN.Text = txtTeTel.Text = "";            
        dtOfImgTeacher = (DataTable)Session["TblOfImg7"];
        dtOfImgTeacher.Rows.Clear();
        AspxGridFlpTeacher.DataSource = dtOfImgTeacher;
        AspxGridFlpTeacher.DataBind();
    }
    protected void Edit(int TeId)
    {
        TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(TeManager);
        trans.Add(attachManager);

        try
        {
            TeManager.SelectLecturer(TeId);
            if (TeManager.Count == 1)
            {
                TeManager[0].BeginEdit();
                TeManager[0]["Name"] = txtTeName.Text;
                TeManager[0]["Family"] = txtTeFamily.Text;
                TeManager[0]["Father"] = txtTeFatherName.Text;
                TeManager[0]["BirthDate"] = txtTeDate.Text;
                TeManager[0]["IdNo"] = txtTeIdNo.Text;
                TeManager[0]["SSN"] = txtTeSSN.Text;
                TeManager[0]["MobileNo"] = txtTeMobileNo.Text;
                TeManager[0]["Email"] = txtTeEmail.Text;
                TeManager[0]["LiId"] = cmbTeLicence.Value;
                TeManager[0]["MjId"] = cmbTeMajor.Value;
                TeManager[0]["Address"] = txtTeAddress.Text;
                TeManager[0]["Description"] = txtTeDesc.Text;
                TeManager[0]["Tel"] = txtTeTel.Text;
                TeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeManager[0].EndEdit();

                trans.BeginSave();
                if (TeManager.Save() > 0)
                {
                    dtOfImgTeacher = (DataTable)Session["TblOfImg7"];

                    //edit attchament
                    if (dtOfImgTeacher.GetChanges() != null)
                    {
                        DataRow[] delRows = dtOfImgTeacher.Select("Mode='1'", null, DataViewRowState.Deleted);
                        for (int i = 0; i < delRows.Length; i++)
                        {
                            attachManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                            attachManager[0].Delete();
                            attachManager.Save();
                        }
                        attachManager.DataTable.AcceptChanges();
                        //dtOfImg.DefaultView.RowFilter = "Mode='0'";
                        DataRow[] insRows = dtOfImgTeacher.Select(null, null, DataViewRowState.Added);

                        //if (dtOfImg.DefaultView.Count > 0)
                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {
                                DataRow drImg = attachManager.NewRow();
                                drImg["TtId"] = (int)TSP.DataManager.TableCodes.Teachers;
                                drImg["RefTable"] = Utility.DecryptQS(TeacherId.Value);
                                drImg["AttId"] = 1;
                                //drImg["AtContent"] = dtOfImg.DefaultView[i]["Image"];
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
                            //  int imgcnt = ImgManager.Save();
                        }
                    }

                    trans.EndSave();
                    ASPxRoundPanel1.HeaderText = "ویرایش";
                    PgMode.Value = Utility.EncryptQS("Edit");

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش اطلاعات وجود ندارد";
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
    protected void Insert()
    {
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();

        trans.Add(attachManager);
        trans.Add(TeManager);

        try
        {
            DataRow dr = TeManager.NewRow();
            dr["Name"] = txtTeName.Text;
            dr["Family"] = txtTeFamily.Text;
            dr["Father"] = txtTeFatherName.Text;
            dr["BirthDate"] = txtTeDate.Text;
            dr["IdNo"] = txtTeIdNo.Text;
            dr["SSN"] = txtTeSSN.Text;
            dr["MobileNo"] = txtTeMobileNo.Text;
            dr["Email"] = txtTeEmail.Text;
            dr["LiId"] = cmbTeLicence.Value;
            dr["MjId"] = cmbTeMajor.Value;
            dr["Address"] = txtTeAddress.Text;
            dr["Description"] = txtTeDesc.Text;
            dr["Tel"] = txtTeTel.Text;
            dr["Type"] = 1;//Lecturer

            dr["InActive"] = 0;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            TeManager.AddRow(dr);
            trans.BeginSave();
            int cnt = TeManager.Save();
            if (cnt > 0)
            {
                TeacherId.Value = Utility.EncryptQS(TeManager[0]["TeId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");

                #region AttachmentTeacher
                dtOfImgTeacher = (DataTable)Session["TblOfImg7"];

                if (dtOfImgTeacher.DefaultView.Count > 0)
                {
                    for (int j = 0; j < dtOfImgTeacher.DefaultView.Count; j++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.Teachers;
                        drImg["RefTable"] = TeManager[0]["TeId"].ToString();
                        drImg["AttId"] = 1;
                        drImg["FilePath"] = dtOfImgTeacher.Rows[j]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImgTeacher.Rows[j]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        attachManager.AddRow(drImg);
                        int imgcnt = attachManager.Save();
                        attachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImgTeacher.Rows[j].BeginEdit();
                            dtOfImgTeacher.Rows[j]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                            dtOfImgTeacher.Rows[j].EndEdit();

                            if (!string.IsNullOrEmpty(dtOfImgTeacher.Rows[j]["ImgUrl"].ToString()))
                            {

                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImgTeacher.Rows[j]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImgTeacher.Rows[j]["ImgUrl"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);
                                // grdv_Img.Columns[1].Visible = true;
                            }

                        }
                    }

                }
                #endregion

                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                ASPxRoundPanel1.HeaderText = "ویرایش";
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
    #endregion
}
