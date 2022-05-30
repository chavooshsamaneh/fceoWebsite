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


public partial class Employee_Amoozesh_AddTrainingRule : System.Web.UI.Page
{
    DataTable dtOfImg = null;

    protected void Page_Load(object sender, EventArgs e)
    {


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TrId"]))
            {
                Response.Redirect("TrainingRules.aspx");
                return;
            }

            Session["TrRuleUpload"] = null;

            if (Session["TblOfImg8"] == null)
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

                Session["TblOfImg8"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfImg8"];

            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();


            TSP.DataManager.Permission per = TSP.DataManager.TrainingRulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                RuleId.Value = Server.HtmlDecode(Request.QueryString["TrId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TrId = Utility.DecryptQS(RuleId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(TrId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnSearch1.Enabled = false;
                    TblFile.Visible = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(TrId));
                    AspxGridFlp.Columns[2].Visible = false;
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                    lbl3.Visible = false;
                    lbl4.Visible = false;
                    txtReviewDate.Visible = false;
                    txtSessionDate.Visible = false;
                    txtSessionNo.Visible = false;
                    txtResultDesc.Visible = false;
                    ASPxRoundPanel2.HeaderText = "جدید";
                  
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void Enable()
    {
        txtBroacher.Enabled = true;
        txtDate.Enabled = true;
        txtDesc.Enabled = true;
        txtMeFirstName.Enabled = true;
        txtMeLastName.Enabled = true;
        txtMeNo.Enabled = true;
        txtResultDesc.Enabled = true;
        txtReviewDate.Enabled = true;
        txtSessionDate.Enabled = true;
        txtSessionNo.Enabled = true;
        txtSubject.Enabled = true;
        cmbPeriod.Enabled = true;
        cmbRuleType.Enabled = true;
    }
    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        try
        {
            if (!(string.IsNullOrEmpty(txtMeNo.Text)))
            {
                MemManager.FindByCode(int.Parse(txtMeNo.Text));
                if (MemManager.Count > 0)
                {

                    txtMeFirstName.Text = MemManager[0]["FirstName"].ToString();
                    txtMeLastName.Text = MemManager[0]["LastName"].ToString();

                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "چنین کدعضویتی وجود ندارد.مجددا وارد نمایید";
                    return;
                }
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی رخ داده است. مجدداً کدعضویت را وارد نمایید";
        }
    }
    protected void Disable()
    {
        txtBroacher.Enabled = false;
        txtDate.Enabled = false;
        txtDesc.Enabled = false;
        txtMeFirstName.Enabled = false;
        txtMeLastName.Enabled = false;
        txtMeNo.Enabled = false;
        txtResultDesc.Enabled = false;
        txtReviewDate.Enabled = false;
        txtSessionDate.Enabled = false;
        txtSessionNo.Enabled = false;
        txtSubject.Enabled = false;
        cmbPeriod.Enabled = false;
        cmbRuleType.Enabled = false;
    }
    protected void ClearForm()
    {
        txtBroacher.Text = "";
        txtBroacher.Text = "";
        txtDate.Text = "";
        txtDesc.Text = "";
        txtMeFirstName.Text = "";
        txtMeLastName.Text = "";
        txtMeNo.Text = "";
        txtResultDesc.Text = "";
        txtReviewDate.Text = "";
        txtSessionDate.Text = "";
        txtSessionNo.Text = "";
        txtSubject.Text = "";
        cmbPeriod.Text = "";
        cmbRuleType.Text = "";
        cmbPeriod.DataBind();
        cmbRuleType.DataBind();
        cmbRuleType.SelectedIndex = -1;
        cmbPeriod.SelectedIndex = -1;

        dtOfImg = (DataTable)Session["TblOfImg8"];
        dtOfImg.Rows.Clear();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();
        
    }
    protected void FillForm(int TrId)
    {
        TSP.DataManager.TrainingRulesManager Manager = new TSP.DataManager.TrainingRulesManager();
        try
        {
            Manager.FindByCode(TrId);
            if (Manager.Count == 1)
            {
                txtBroacher.Text = Manager[0]["Broacher"].ToString();
                txtDate.Text = Manager[0]["Date"].ToString();
                txtDesc.Text = Manager[0]["Description"].ToString();
                txtMeNo.Text = Manager[0]["MeId"].ToString();
                txtMeFirstName.Text = Manager[0]["FirstName"].ToString();
                txtMeLastName.Text = Manager[0]["LastName"].ToString();
                txtResultDesc.Text = Manager[0]["ResultDesc"].ToString();
                txtReviewDate.Text = Manager[0]["ReviewDate"].ToString();
                txtSessionDate.Text = Manager[0]["SessionDate"].ToString();
                txtSessionNo.Text = Manager[0]["SessionNo"].ToString();
                txtSubject.Text = Manager[0]["Subject"].ToString();
                cmbPeriod.DataBind();
                cmbPeriod.SelectedIndex = cmbPeriod.Items.IndexOfValue(Manager[0]["PeriodId"].ToString());
                cmbRuleType.DataBind();
               // cmbRuleType.Text = Manager[0]["RuleType"].ToString();
                cmbRuleType.SelectedIndex = cmbRuleType.Items.IndexOfValue(Manager[0]["RuleType"].ToString());
                
                TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
                attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.TrainingRule, TrId);
                dtOfImg = (DataTable)Session["TblOfImg8"];
                for (int i = 0; i < attachManager.Count; i++)
                {
                    DataRow dr = dtOfImg.NewRow();
                    dr[0] = attachManager[i]["FilePath"].ToString();
                    dr[1] = attachManager[i]["FilePath"].ToString();
                    dr[5] = attachManager[i]["Description"].ToString();

                    string fileName = System.IO.Path.GetFileName(attachManager[0]["FilePath"].ToString());
                    dr[2] = fileName;
                    dr[3] = 1;
                    dr[4] = attachManager[i][0];
                    dtOfImg.Rows.Add(dr);

                }
                dtOfImg.AcceptChanges();
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Insert();
    }
    protected void Insert()
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        TSP.DataManager.TrainingRulesManager RuleManager = new TSP.DataManager.TrainingRulesManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(RuleManager);
        trans.Add(AttachManager);

        try
        {
            DataRow dr = RuleManager.NewRow();
            dr["Subject"] = txtSubject.Text;
            dr["MeId"] = txtMeNo.Text;
            dr["PeriodId"] = cmbPeriod.Value;
            dr["Date"] = txtDate.Text;
            dr["Broacher"] = txtBroacher.Text;
            dr["TrType"] = 1;
            if (cmbRuleType.Value != null)
                dr["RuleType"] = cmbRuleType.Value;
            else
                dr["RuleType"] = DBNull.Value;
            dr["Description"] = txtDesc.Text;
            dr["CreateDate"] = PerDate;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            RuleManager.AddRow(dr);
            trans.BeginSave();
            if (RuleManager.Save() > 0)
            {
                RuleId.Value = Utility.EncryptQS(RuleManager[0]["TrId"].ToString());
                PgMode.Value = Utility.EncryptQS("View");

                dtOfImg = (DataTable)Session["TblOfImg8"];
                int TrId = int.Parse(RuleManager[0]["TrId"].ToString());
                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = AttachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.TrainingRule;
                        drImg["RefTable"] = TrId;
                        drImg["AttId"] = 1;
                        drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        AttachManager.AddRow(drImg);
                        int imgcnt = AttachManager.Save();
                        AttachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImg.Rows[i].BeginEdit();
                            dtOfImg.Rows[i]["Code"] = AttachManager[AttachManager.Count - 1]["AttachId"].ToString();
                            dtOfImg.Rows[i].EndEdit();

                            if (!string.IsNullOrEmpty(dtOfImg.Rows[i]["ImgUrl"].ToString()))
                            {

                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                                // grdv_Img.Columns[1].Visible = true;
                            }

                        }
                    }

                }
                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                ASPxRoundPanel2.HeaderText = "مشاهده";
                TblFile.Visible = false;
                AspxGridFlp.Columns[2].Visible = false;
                Disable();
                btnSearch1.Enabled = false;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;

            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
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
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TrainingRulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
       
        this.ViewState["BtnSave"] = btnSave.Enabled;
        btnSearch1.Enabled = true;

        RuleId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        
        TblFile.Visible = true;
        lbl1.Visible = false;
        lbl2.Visible = false;
        lbl3.Visible = false;
        lbl4.Visible = false;
        txtReviewDate.Visible = false;
        txtSessionDate.Visible = false;
        txtSessionNo.Visible = false;
        txtResultDesc.Visible = false;
        AspxGridFlp.Columns[2].Visible = true;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfImg8"] = null;
        Session["TrRuleUpload"] = null;
        Response.Redirect("TrainingRules.aspx"); 
    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {
     
        if (Session["TblOfImg8"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImg8"];

            DataRow dr = dtOfImg.NewRow();

            try
            {               
                if (Session["TrRuleUpload"] != null)
                {
                    dr[0] = "~/Image/TrainingRule/" + System.IO.Path.GetFileName(Session["TrRuleUpload"].ToString());
                    dr[2] = System.IO.Path.GetFileName(Session["TrRuleUpload"].ToString());
                    dr[1] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["TrRuleUpload"].ToString());
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

                Session["TrRuleUpload"] = null;

                txtDescImg.Text = "";
                imgEndUploadImg.ClientVisible = false;


            }
            catch
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
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO. FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/TrainingRule/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TrRuleUpload"] = tempFileName;
        }
        return ret;
    }
    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlp.DataSource = (DataTable)Session["TblOfImg8"];
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

            dtOfImg = (DataTable)Session["TblOfImg8"];
            dtOfImg.Rows[Id].Delete();
            Session["TblOfImg8"] = dtOfImg;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImg8"];
            AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfImg8"];

        }

    }
    protected void GridMember_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridMember.DataSource = ObjectDataSource1;

    }
}

