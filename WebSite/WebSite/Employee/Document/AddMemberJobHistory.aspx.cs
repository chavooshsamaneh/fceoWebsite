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

public partial class Employee_Document_AddMemberJobHistory : System.Web.UI.Page
{
    DataTable dtJobHistoryAttachment = null;
    DataTable dtOfJob = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MFId"] == null || Request.QueryString["DocType"] == null || Request.QueryString["PrePgMd"] == null || Request.QueryString["JHId"] == null || Request.QueryString["PgMd"] == null)
            {
                // Response.Redirect("MemberFile.aspx");
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            Session["IsEdited_EmpMeJob"] = false;
            Session["JobAttach"] = null;

            if (Session["JobAttach"] == null)
            {
                dtJobHistoryAttachment = new DataTable();
                dtJobHistoryAttachment.Columns.Add("Id");
                dtJobHistoryAttachment.Columns["Id"].AutoIncrement = true;
                dtJobHistoryAttachment.Columns["Id"].AutoIncrementSeed = 1;
                dtJobHistoryAttachment.Constraints.Add("PK_ID", dtJobHistoryAttachment.Columns["Id"], true);
                dtJobHistoryAttachment.Columns.Add("TtId");
                dtJobHistoryAttachment.Columns.Add("AttId");
                dtJobHistoryAttachment.Columns.Add("IsValid");
                dtJobHistoryAttachment.Columns.Add("Description");
                dtJobHistoryAttachment.Columns.Add("AtContent");
                dtJobHistoryAttachment.Columns.Add("FilePath");

                Session["JobAttach"] = dtJobHistoryAttachment;

            }
            else
                dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

            GridViewAttachment.DataSource = dtJobHistoryAttachment;
            GridViewAttachment.DataBind();

            Session["TblOfJobQ"] = null;

            if (Session["TblOfJobQ"] == null)
            {
                dtOfJob = new DataTable();
                dtOfJob.Columns.Add("OfdId");
                dtOfJob.Columns.Add("OfdName");
                dtOfJob.Columns.Add("Mark");
                dtOfJob.Columns.Add("FilePath");
                dtOfJob.Columns.Add("TempImgUrl");
                dtOfJob.Columns.Add("Mode");
                dtOfJob.Columns.Add("Code");
                dtOfJob.Columns.Add("Description");
                dtOfJob.Columns.Add("Id");
                dtOfJob.Columns["Id"].AutoIncrement = true;
                dtOfJob.Columns["Id"].AutoIncrementSeed = 1;

                Session["TblOfJobQ"] = dtOfJob;
            }
            else
                dtOfJob = (DataTable)Session["TblOfJobQ"];

            AspxGridFlp.DataSource = dtOfJob;
            AspxGridFlp.DataBind();

            GridViewAttachment.DataSource = dtJobHistoryAttachment;
            GridViewAttachment.DataBind();

            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            TSP.DataManager.Permission perAttach = TSP.DataManager.AttachmentsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = perAttach.CanNew;
            GridViewAttachment.Visible = perAttach.CanView;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSaveAtt"] = btnSaveAttachment.Enabled;
        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldJobHistory["MeId"]))
        {
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldJobHistory["MeId"].ToString()));
            MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSaveAtt"] != null)
            this.btnSaveAttachment.Enabled = (bool)this.ViewState["BtnSaveAtt"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
               && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("MemberJobHistory.aspx?PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("MemberJobHistory.aspx?PgMd=" + HiddenFieldJobHistory["PrePageMode"].ToString() + "&MFId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string JhId = Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString());

        if (string.IsNullOrEmpty(JhId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            Enable();

            GridViewAttachment.Enabled = true;

            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            TSP.DataManager.Permission perAtt = TSP.DataManager.AttachmentsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSaveAttachment.Enabled = per.CanNew;
            this.ViewState["BtnSaveAtt"] = btnSaveAttachment.Enabled;

            HiddenFieldJobHistory["PageMode"] = Utility.EncryptQS("Edit");
            RoundPanelJobHistory.HeaderText = "ویرایش";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldJobHistory["PageMode"].ToString());
        string JhId = Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            switch (PageMode)
            {
                case "New":
                    Insert();
                    break;
                case "Edit":
                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                    {
                        Edit(int.Parse(JhId));
                    }
                    break;              
            }

        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        HiddenFieldJobHistory["JHId"] = Utility.EncryptQS("");
        HiddenFieldJobHistory["PageMode"] = Utility.EncryptQS("New");
        RoundPanelJobHistory.HeaderText = "جدید";
        ClearForm();
        Enable();
    }

    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        string fileNameImg = "", pathAx = "", extension = "";
        if (Session["JobAttach"] != null)
        {
            dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

            DataRow dr = dtJobHistoryAttachment.NewRow();

            try
            {
                dr["TtId"] = (int)TSP.DataManager.TableCodes.DocMemberFile;
                dr["AttId"] = 1;
                dr["IsValid"] = 1;
                dr["Description"] = txtDescription.Text;

                try
                {
                    dr["AtContent"] = DBNull.Value;
                    //dr["FilePath"] = "~/Image/Members/DocumentAttachment/" + Path.GetFileName(Session["َAttachStatus"].ToString());
                    dr["FilePath"] = "~/Image/Temp/" + Path.GetFileName(Session["َAttachStatus"].ToString());
                }
                catch
                {
                }

                dtJobHistoryAttachment.Rows.Add(dr);
                GridViewAttachment.DataSource = dtJobHistoryAttachment;
                GridViewAttachment.DataBind();

                txtDescription.Text = "";
                Session["َAttachStatus"] = "";

            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void flpFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void GridViewAttachment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewAttachment.DataSource = (DataTable)Session["JobAttach"];
        GridViewAttachment.DataBind();

        int Id = -1;
        if (GridViewAttachment.FocusedRowIndex > -1)
        {
            Id = GridViewAttachment.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtJobHistoryAttachment = (DataTable)Session["JobAttach"];
            dtJobHistoryAttachment.Rows.Find(e.Keys["Id"]).Delete();
            //  dtJobHistoryAttachment.Rows[Id].Delete();
            Session["JobAttach"] = dtJobHistoryAttachment;
            GridViewAttachment.DataSource = (DataTable)Session["JobAttach"];
            GridViewAttachment.DataBind();
            dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

        }
    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {
        if (Session["TblOfJobQ"] != null)
        {
            dtOfJob = (DataTable)Session["TblOfJobQ"];
            AspxGridFlp.DataSource = dtOfJob;
            AspxGridFlp.DataBind();

            DataRow dr = dtOfJob.NewRow();

            try
            {

                if (CmbName.Value == null)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "نوع مطلوبیت کار را انتخاب نمایید";
                    return;

                }
                for (int i = 0; i < AspxGridFlp.VisibleRowCount; i++)
                {

                    DataRowView drgrid = (DataRowView)AspxGridFlp.GetRow(i);
                    if (drgrid["OfdId"].ToString() == CmbName.Value.ToString())
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "نوع مطلوبیت کار انتخاب شده تکراری می باشد";
                        return;
                    }
                }
                dr[0] = CmbName.Value;
                dr[1] = CmbName.SelectedItem.Text;
                dr[2] = ""; //txtMark.Text;
                if (Session["JobQUpload"] != null)
                {
                    dr[3] = "~/Image/Office/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
                    dr[4] = "~/Image/temp/" + Path.GetFileName(Session["JobQUpload"].ToString());
                }
                dr[7] = txtJhDesc.Text;


                dr[5] = 0;
                dtOfJob.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfJob;
                AspxGridFlp.DataBind();

                //Session["JobQUpload"] = null;
                HDFlpMember["name"] = 0;
                CmbName.DataBind();
                CmbName.SelectedIndex = -1;
                txtJhDesc.Text = "";
                imgEndUploadImg.ClientVisible = false;
                ComboPosition.Enabled = false;
                ASPxRoundPanel4.ClientVisible = true;

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

    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlp.DataSource = (DataTable)Session["TblOfJobQ"];
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

            dtOfJob = (DataTable)Session["TblOfJobQ"];
            dtOfJob.Rows[Id].Delete();
            Session["TblOfJobQ"] = dtOfJob;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfJobQ"];
            AspxGridFlp.DataBind();
            dtOfJob = (DataTable)Session["TblOfJobQ"];


        }

    }

    protected void GridViewAttachment_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string fileNameImg = "", pathAx = "", extension = "";
        if (Session["JobAttach"] != null)
        {
            dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

            DataRow dr = dtJobHistoryAttachment.NewRow();

            try
            {
                dr["TtId"] = (int)TSP.DataManager.TableCodes.DocMemberFile;
                //   dr["RefTable"] = Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString());
                dr["AttId"] = 1;
                dr["IsValid"] = 1;
                dr["Description"] = txtDescription.Text;

                try
                {
                    //  extension = Path.GetExtension(flpFile.FileName);
                    //  extension = extension.ToLower();
                    //   if (flpFile.HasFile)
                    //   {
                    //       fileNameImg = Utility.GenerateName(Path.GetExtension(flpFile.FileName));
                    //      pathAx = Server.MapPath("~/image/Temp/");
                    //       flpFile.SaveAs(pathAx + fileNameImg);
                    dr["AtContent"] = DBNull.Value;
                    // dr["FilePath"] = "~/Image/Members/DocumentAttachment/" + fileNameImg;
                    dr["FilePath"] = "~/Image/Members/DocumentAttachment/" + Path.GetFileName(Session["َAttachStatus"].ToString());
                    //    }
                }
                catch
                {
                }

                dtJobHistoryAttachment.Rows.Add(dr);
                GridViewAttachment.DataSource = dtJobHistoryAttachment;
                GridViewAttachment.DataBind();

                txtDescription.Text = "";
                Session["َAttachStatus"] = "";

            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void MenuJob_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (e.Item.Name == "Qualification")
        {
            Response.Redirect("MemberJobQualification.aspx?PgMd=" + HiddenFieldJobHistory["PageMode"].ToString() + "&MfId=" + HiddenFieldJobHistory["MFId"].ToString() + "&DocType=" + HiddenFieldJobHistory["DocType"].ToString() + "&PrePgMd=" + HiddenFieldJobHistory["PrePageMode"] + "&JhId=" + HiddenFieldJobHistory["JHId"].ToString());
        }
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldJobHistory["JHId"] = Request.QueryString["JHId"].ToString();
        HiddenFieldJobHistory["PageMode"] = Request.QueryString["PgMd"];
        HiddenFieldJobHistory["PrePageMode"] = Request.QueryString["PrePgMd"];
        HiddenFieldJobHistory["MFId"] = Request.QueryString["MFId"];
        HiddenFieldJobHistory["DocType"] = Request.QueryString["DocType"];

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MfId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString()));
        int DocType = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["DocType"].ToString()));
        if (DocType == 0)
        {
            DocMemberFileManager.FindByCode(MfId, DocType);
            int MeId = -1;
            if (DocMemberFileManager.Count == 1)
            {
                HiddenFieldJobHistory["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

        }
        else if (DocType == 1)
        {
            DocMemberFileManager.FindByCode(MfId, DocType);
            if (DocMemberFileManager.Count == 1)
            {
                int MemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                DocMemberFileManager.FindByCode(MemberFileId, 0);
                int MeId = -1;
                if (DocMemberFileManager.Count == 1)
                {
                    HiddenFieldJobHistory["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

        }
        string JHId = Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldJobHistory["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
        CheckWorkFlowPermission();
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
            case "Judge":
                int NmcId = FindNmcId();
                if (NmcId == -1)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                if (HiddenFieldJobHistory["JHId"] == null || string.IsNullOrEmpty(HiddenFieldJobHistory["JHId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                string JHId = Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString());
                TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
                if (string.IsNullOrEmpty(JHId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                Disable();

                TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;

                FillForm(int.Parse(JHId));

                RoundPanelJobHistory.HeaderText = "مشاهده";                
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }
        MenuJob.Enabled = true;

        GridViewAttachment.Enabled = false;
        btnSaveAttachment.Enabled = false;
        txtDescription.Enabled = false;
        flpFile.Enabled = false;

        txtjArea.Enabled = false;
        txtjCity.Enabled = false;
        txtjCoEndDate.Enabled = false;
        txtjCoStartDate.Enabled = false;
        txtjDesc.Enabled = false;
        txtjEmployer.Enabled = false;
        txtjEndStatus.Enabled = false;
        txtjFloor.Enabled = false;
        txtjPrName.Enabled = false;
        txtjPrVolume.Enabled = false;
        txtjStartDate.Enabled = false;
        txtjStartStatus.Enabled = false;
        CombojCountry.Enabled = false;
        CombojIsCorporate.Enabled = false;
        CombojPrType.Enabled = false;
        CombojSazeType.Enabled = false;
        ComboPosition.Enabled = false;

        if (HiddenFieldJobHistory["JHId"] == null || (string.IsNullOrEmpty(HiddenFieldJobHistory["JHId"].ToString())))
        {
            Response.Redirect("MemberJobHistory.aspx");
            return;
        }
        int JHId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString()));
        FillForm(JHId);

        RoundPanelJobHistory.HeaderText = "مشاهده";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnSaveAtt"] = btnSaveAttachment.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        MenuJob.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        Enable();
        ClearForm();
        RoundPanelJobHistory.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        MenuJob.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (HiddenFieldJobHistory["JHId"] == null || string.IsNullOrEmpty(HiddenFieldJobHistory["JHId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int JHId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["JHId"].ToString()));


        Enable();

        FillForm(JHId);

        RoundPanelJobHistory.HeaderText = "ویرایش";
    }

    protected void Enable()
    {
        //RoundPanelJobHistory.Enabled = true;

        txtjArea.Enabled = true;
        txtjCity.Enabled = true;
        txtjCoEndDate.Enabled = true;
        txtjCoStartDate.Enabled = true;
        txtjDesc.Enabled = true;
        txtjEmployer.Enabled = true;
        txtjEndStatus.Enabled = true;
        txtjFloor.Enabled = true;
        txtjPrName.Enabled = true;
        txtjPrVolume.Enabled = true;
        txtjStartDate.Enabled = true;
        txtjStartStatus.Enabled = true;
        CombojCountry.Enabled = true;
        CombojIsCorporate.Enabled = true;
        CombojPrType.Enabled = true;
        CombojSazeType.Enabled = true;
        ComboPosition.Enabled = true;

        txtDescription.Enabled = true;
        flpFile.Enabled = true;
    }

    protected void Disable()
    {
        //RoundPanelJobHistory.Enabled = false;
        txtjArea.Enabled = false;
        txtjCity.Enabled = false;
        txtjCoEndDate.Enabled = false;
        txtjCoStartDate.Enabled = false;
        txtjDesc.Enabled = false;
        txtjEmployer.Enabled = false;
        txtjEndStatus.Enabled = false;
        txtjFloor.Enabled = false;
        txtjPrName.Enabled = false;
        txtjPrVolume.Enabled = false;
        txtjStartDate.Enabled = false;
        txtjStartStatus.Enabled = false;
        CombojCountry.Enabled = false;
        CombojIsCorporate.Enabled = false;
        CombojPrType.Enabled = false;
        CombojSazeType.Enabled = false;
        ComboPosition.Enabled = false;

        txtDescription.Enabled = false;
        flpFile.Enabled = false;
    }

    protected void ClearForm()
    {
        for (int i = 0; i < RoundPanelJobHistory.Controls.Count; i++)
        {

            if (RoundPanelJobHistory.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)RoundPanelJobHistory.Controls[i];
                co.Text = "";
            }
            if (RoundPanelJobHistory.Controls[i] is DevExpress.Web.ASPxComboBox)
            {
                DevExpress.Web.ASPxComboBox co = (DevExpress.Web.ASPxComboBox)RoundPanelJobHistory.Controls[i];
                co.DataBind();
                co.SelectedIndex = -1;
            }

        }
        txtjDesc.Text = "";
        txtjCoEndDate.Text = "";
        txtjCoStartDate.Text = "";
        txtjStartDate.Text = "";

        dtJobHistoryAttachment = (DataTable)Session["JobAttach"];
        dtJobHistoryAttachment.Rows.Clear();
        Session["JobAttach"] = dtJobHistoryAttachment;

        GridViewAttachment.DataSource = (DataTable)Session["JobAttach"];
        GridViewAttachment.DataBind();

    }

    protected void FillForm(int JhId)
    {
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        JhManager.FindByCode(JhId);
        if (JhManager.Count > 0)
        {
            txtjCity.Text = JhManager[0]["CitName"].ToString();
            txtjCoEndDate.Text = JhManager[0]["EndCorporateDate"].ToString();
            txtjCoStartDate.Text = JhManager[0]["StartCorporateDate"].ToString();
            txtjDesc.Text = JhManager[0]["Description"].ToString();
            txtjEmployer.Text = JhManager[0]["Employer"].ToString();
            txtjEndStatus.Text = JhManager[0]["StatusOfEndDate"].ToString();
            //txtjPosition.Text = JhManager[0]["ProjectPosition"].ToString();
            txtjPrName.Text = JhManager[0]["ProjectName"].ToString();
            txtjPrVolume.Text = JhManager[0]["ProjectVolume"].ToString();
            txtjStartDate.Text = JhManager[0]["StartOriginalDate"].ToString();
            txtjStartStatus.Text = JhManager[0]["StatusOfStartDate"].ToString();

            if (!string.IsNullOrEmpty(JhManager[0]["PJPId"].ToString()))
            {
                ComboPosition.DataBind();
                ComboPosition.SelectedIndex = ComboPosition.Items.IndexOfValue(JhManager[0]["PJPId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CounId"].ToString()))
            {
                CombojCountry.DataBind();
                CombojCountry.SelectedIndex = CombojCountry.Items.IndexOfValue(JhManager[0]["CounId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CorTypeId"].ToString()))
            {
                CombojIsCorporate.DataBind();
                CombojIsCorporate.SelectedIndex = CombojIsCorporate.Items.IndexOfValue(JhManager[0]["CorTypeId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["PrTypeId"].ToString()))
            {
                if (JhManager[0]["PrTypeId"].ToString() == "1")
                {
                    ASPxLabel22.ClientVisible = true;
                    ASPxLabel23.ClientVisible = true;
                    txtjArea.ClientVisible = true;
                    txtjFloor.ClientVisible = true;
                    lblSazeType.ClientVisible = true;
                    CombojSazeType.ClientVisible = true;
                    txtjArea.Text = JhManager[0]["Area"].ToString();
                    txtjFloor.Text = JhManager[0]["Floors"].ToString();
                    txtjArea.Text = JhManager[0]["Area"].ToString();
                    txtjFloor.Text = JhManager[0]["Floors"].ToString();
                }
                CombojPrType.DataBind();
                CombojPrType.SelectedIndex = CombojPrType.Items.IndexOfValue(JhManager[0]["PrTypeId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["SazeTypeId"].ToString()))
            {
                CombojSazeType.DataBind();
                CombojSazeType.SelectedIndex = CombojSazeType.Items.IndexOfValue(JhManager[0]["SazeTypeId"].ToString());
            }
            #region Fill Attachment
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            DataTable dtAttach = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.ProjectJobHistory, JhId);

            dtJobHistoryAttachment = (DataTable)Session["JobAttach"];
            for (int i = 0; i < dtAttach.Rows.Count; i++)
            {
                DataRow dr = dtJobHistoryAttachment.NewRow();
                dr["TtId"] = dtAttach.Rows[i]["TtId"];
                dr["AttId"] = dtAttach.Rows[i]["AttId"];
                dr["IsValid"] = dtAttach.Rows[i]["IsValid"];
                dr["Description"] = dtAttach.Rows[i]["Description"];
                dr["AtContent"] = dtAttach.Rows[i]["AtContent"];
                dr["FilePath"] = dtAttach.Rows[i]["FilePath"];
                dr["Id"] = dtAttach.Rows[i]["AttachId"];
                dtJobHistoryAttachment.Rows.Add(dr);

            }
            dtJobHistoryAttachment.AcceptChanges();
            GridViewAttachment.DataSource = dtJobHistoryAttachment;
            GridViewAttachment.DataBind();
            #endregion
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است ";
            return;
        }
    }

    protected void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(MeJobManager);
        TransactionManager.Add(attManager);
        TransactionManager.Add(JobQualityManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldJobHistory["MeId"].ToString()));
            MeJobManager2.FindByMeId(MeId);

            for (int i = 0; i < MeJobManager2.Count; i++)
            {
                if (MeJobManager2[i]["ProjectName"].ToString() == txtjPrName.Text && MeJobManager2[i]["Employer"].ToString() == txtjEmployer.Text && MeJobManager2[i]["PrTypeId"].ToString() == CombojPrType.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            TransactionManager.BeginSave();

            DataRow drJob = MeJobManager.NewRow();

            drJob["MeId"] = MeId;
            drJob["Type"] = 0;//عضو

            drJob["RoeId"] = 2;//پروانه اشتغال به کار
            if (CombojPrType.Value != null)
                drJob["PrTypeId"] = int.Parse(CombojPrType.Value.ToString());
            if (CombojSazeType.Value != null)
                drJob["SazeTypeId"] = int.Parse(CombojSazeType.Value.ToString());
            drJob["ProjectName"] = txtjPrName.Text;
            drJob["Employer"] = txtjEmployer.Text;
            drJob["CitName"] = txtjCity.Text;
            if (CombojCountry.Value != null)
                drJob["CounId"] = int.Parse(CombojCountry.Value.ToString());
            if (ComboPosition.Value != null)
                drJob["PJPId"] = ComboPosition.Value;
            drJob["StartOriginalDate"] = txtjStartDate.Text;
            drJob["StartCorporateDate"] = txtjCoStartDate.Text;
            if (!string.IsNullOrEmpty(txtjStartStatus.Text))
                drJob["StatusOfStartDate"] = txtjStartStatus.Text;
            else
                drJob["StatusOfStartDate"] = DBNull.Value;
            drJob["EndCorporateDate"] = txtjCoEndDate.Text;
            if (!string.IsNullOrEmpty(txtjEndStatus.Text))
                drJob["StatusOfEndDate"] = txtjEndStatus.Text;
            else
                drJob["StatusOfEndDate"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjPrVolume.Text))
                drJob["ProjectVolume"] = txtjPrVolume.Text;
            else
                drJob["ProjectVolume"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjArea.Text))
                drJob["Area"] = txtjArea.Text;
            else
                drJob["Area"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjFloor.Text))
                drJob["Floors"] = txtjFloor.Text;
            else
                drJob["Floors"] = DBNull.Value;
            if (CombojIsCorporate.Value != null)
                drJob["CorTypeId"] = int.Parse(CombojIsCorporate.Value.ToString());
            drJob["ConfirmedByNezam"] = 0;
            drJob["Description"] = txtjDesc.Text;
            drJob["UserId"] = Utility.GetCurrentUser_UserId();
            drJob["ModifiedDate"] = DateTime.Now;

            string MFId = Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString());
            drJob["TableId"] = int.Parse(MFId);
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.DocMemberFile;

            drJob["CreateDate"] = Utility.GetDateOfToday();

            MeJobManager.AddRow(drJob);

            int cnt = MeJobManager.Save();
            if (cnt > 0)
            {
                #region SaveJobHistoryQuality
                dtOfJob = (DataTable)Session["TblOfJobQ"];
                int JhId = int.Parse(MeJobManager[0]["JhId"].ToString());
                if (dtOfJob.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfJob.DefaultView.Count; i++)
                    {
                        DataRow drQ = JobQualityManager.NewRow();
                        drQ["JhId"] = JhId;
                        drQ["OfdId"] = dtOfJob.Rows[i]["OfdId"].ToString();
                        drQ["Mark"] = DBNull.Value;
                        drQ["FilePath"] = dtOfJob.Rows[i]["FilePath"].ToString();
                        drQ["CreateDate"] = Utility.GetDateOfToday();
                        drQ["Description"] = dtOfJob.Rows[i]["Description"].ToString();
                        drQ["UserId"] = Utility.GetCurrentUser_UserId();
                        drQ["ModifiedDate"] = DateTime.Now;
                        JobQualityManager.AddRow(drQ);
                        int imgcnt = JobQualityManager.Save();
                        JobQualityManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfJob.Rows[i].BeginEdit();
                            dtOfJob.Rows[i]["Code"] = JobQualityManager[JobQualityManager.Count - 1]["JhqId"].ToString();
                            dtOfJob.Rows[i].EndEdit();

                            if (!string.IsNullOrEmpty(dtOfJob.Rows[i]["FilePath"].ToString()))
                            {

                                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                                string ImgTarget = Server.MapPath("~/Image/Office/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString()); //Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);
                            }
                        }
                    }

                }
                #endregion

                #region SaveAttachment

                dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

                if (dtJobHistoryAttachment.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtJobHistoryAttachment.DefaultView.Count; i++)
                    {
                        DataRow AttachmentRow = attManager.NewRow();
                        AttachmentRow["TtId"] = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                        AttachmentRow["RefTable"] = MeJobManager[0]["JhId"].ToString();
                        AttachmentRow["AttId"] = 1;
                        AttachmentRow["IsValid"] = 1;
                        AttachmentRow["Description"] = txtDescription.Text;
                        AttachmentRow["UserId"] = Utility.GetCurrentUser_UserId();
                        AttachmentRow["ModfiedDate"] = DateTime.Now;
                        AttachmentRow["AtContent"] = DBNull.Value;
                        AttachmentRow["FilePath"] = dtJobHistoryAttachment.Rows[i]["FilePath"].ToString();
                        try
                        {
                            string FilePath = dtJobHistoryAttachment.Rows[i]["FilePath"].ToString();
                            string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(FilePath);
                            string ImgTarget = Server.MapPath("~/image/Office/StatusChange/") + Path.GetFileName(FilePath);
                            File.Copy(ImgSoource, ImgTarget, true);
                        }
                        catch (Exception)
                        {
                        }

                        attManager.AddRow(AttachmentRow);

                        int count = attManager.Save();
                        attManager.DataTable.AcceptChanges();
                        if (count < 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            return;
                        }

                    }
                }
                #endregion


                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_EmpMeJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(MFId), UpdateTableType, "ویرایش سابقه کار", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_EmpMeJob"] = true;
                    if (CombojPrType.Value.ToString() == "1")
                    {
                        ASPxLabel22.ClientVisible = true;
                        ASPxLabel23.ClientVisible = true;
                        txtjArea.ClientVisible = true;
                        txtjFloor.ClientVisible = true;
                        lblSazeType.ClientVisible = true;
                        CombojSazeType.ClientVisible = true;
                    }
                    else
                    {
                        ASPxLabel22.ClientVisible = false;
                        ASPxLabel23.ClientVisible = false;
                        txtjArea.ClientVisible = false;
                        txtjFloor.ClientVisible = false;
                        lblSazeType.ClientVisible = false;
                        CombojSazeType.ClientVisible = false;

                    }
                    if (ComboPosition.Value.ToString() == "8" || ComboPosition.Value.ToString() == "9")
                        ASPxRoundPanel4.ClientVisible = true;
                    else
                        ASPxRoundPanel4.ClientVisible = false;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    MenuJob.Enabled = true;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;

                    HiddenFieldJobHistory["JHId"] = Utility.EncryptQS(MeJobManager[0]["JhId"].ToString());
                    HiddenFieldJobHistory["PageMode"] = Utility.EncryptQS("Edit");
                    RoundPanelJobHistory.HeaderText = "ویرایش";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
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

    protected void Edit(int JhId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(JhManager);
        TransactionManager.Add(AttachmentsManager);
        TransactionManager.Add(JobQualityManager);
        try
        {
            TransactionManager.BeginSave();

            JhManager.FindByCode(JhId);
            if (JhManager.Count == 1)
            {
                JhManager[0].BeginEdit();

                if (CombojPrType.Value != null)
                    JhManager[0]["PrTypeId"] = int.Parse(CombojPrType.Value.ToString());
                if (CombojSazeType.Value != null)
                    JhManager[0]["SazeTypeId"] = int.Parse(CombojSazeType.Value.ToString());
                JhManager[0]["ProjectName"] = txtjPrName.Text;
                JhManager[0]["Employer"] = txtjEmployer.Text;
                JhManager[0]["CitName"] = txtjCity.Text;
                if (CombojCountry.Value != null)
                    JhManager[0]["CounId"] = int.Parse(CombojCountry.Value.ToString());
                if (ComboPosition.Value != null)
                    JhManager[0]["PJPId"] = ComboPosition.Value;
                JhManager[0]["StartOriginalDate"] = txtjStartDate.Text;
                JhManager[0]["StartCorporateDate"] = txtjCoStartDate.Text;
                if (!string.IsNullOrEmpty(txtjStartStatus.Text))
                    JhManager[0]["StatusOfStartDate"] = txtjStartStatus.Text;
                else
                    JhManager[0]["StatusOfStartDate"] = DBNull.Value;
                JhManager[0]["EndCorporateDate"] = txtjCoEndDate.Text;
                if (!string.IsNullOrEmpty(txtjEndStatus.Text))
                    JhManager[0]["StatusOfEndDate"] = txtjEndStatus.Text;
                else
                    JhManager[0]["StatusOfEndDate"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtjPrVolume.Text))
                    JhManager[0]["ProjectVolume"] = txtjPrVolume.Text;
                else
                    JhManager[0]["ProjectVolume"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtjArea.Text))
                    JhManager[0]["Area"] = txtjArea.Text;
                else
                    JhManager[0]["Area"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtjFloor.Text))
                    JhManager[0]["Floors"] = txtjFloor.Text;
                else
                    JhManager[0]["Floors"] = DBNull.Value;
                if (CombojIsCorporate.Value != null)
                    JhManager[0]["CorTypeId"] = int.Parse(CombojIsCorporate.Value.ToString());
                JhManager[0]["ConfirmedByNezam"] = 0;
                JhManager[0]["Description"] = txtjDesc.Text;
                JhManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                JhManager[0]["ModifiedDate"] = DateTime.Now;

                JhManager[0].EndEdit();
                if (JhManager.Save() == 1)
                {
                    #region SaveJobAttachment
                    dtOfJob = (DataTable)Session["TblOfJobQ"];

                    if (dtOfJob.GetChanges() != null)
                    {
                        DataRow[] delRows = dtOfJob.Select("Mode='1'", null, DataViewRowState.Deleted);
                        for (int i = 0; i < delRows.Length; i++)
                        {
                            JobQualityManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                            JobQualityManager[0].Delete();
                            JobQualityManager.Save();
                        }
                        JobQualityManager.DataTable.AcceptChanges();
                        DataRow[] insRows = dtOfJob.Select(null, null, DataViewRowState.Added);

                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {
                                DataRow drQ = JobQualityManager.NewRow();
                                drQ["JhId"] = JhId;
                                drQ["OfdId"] = dtOfJob.Rows[i]["OfdId"].ToString();
                                drQ["Mark"] = DBNull.Value;
                                drQ["FilePath"] = dtOfJob.Rows[i]["FilePath"].ToString();
                                drQ["CreateDate"] = Utility.GetDateOfToday();
                                drQ["Description"] = dtOfJob.Rows[i]["Description"].ToString();
                                drQ["UserId"] = Utility.GetCurrentUser_UserId();
                                drQ["ModifiedDate"] = DateTime.Now;
                                JobQualityManager.AddRow(drQ);
                                int imgcnt = JobQualityManager.Save();
                                JobQualityManager.DataTable.AcceptChanges();
                                if (imgcnt == 1)
                                {

                                    if (!string.IsNullOrEmpty(dtOfJob.Rows[i]["FilePath"].ToString()))
                                    {
                                        string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                                        string ImgTarget = Server.MapPath("~/Image/Office/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString()); //Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());

                                        //string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                                        //string ImgTarget = Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());
                                        File.Copy(ImgSoource, ImgTarget, true);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Attachment

                    dtJobHistoryAttachment = (DataTable)Session["JobAttach"];

                    if (dtJobHistoryAttachment.GetChanges() != null)
                    {

                        DataRow[] delRows = dtJobHistoryAttachment.Select(null, null, DataViewRowState.Deleted);
                        DataRow[] insRows = dtJobHistoryAttachment.Select(null, null, DataViewRowState.Added);
                        //  DataRow[] EditRows = dtJobHistoryAttachment.Select(null, null, DataViewRowState.ModifiedCurrent);

                        if (delRows.Length > 0)
                        {
                            for (int i = 0; i < delRows.Length; i++)
                            {
                                AttachmentsManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                                if (AttachmentsManager.Count > 0)
                                {
                                    AttachmentsManager[0].Delete();

                                    int SaveDel = AttachmentsManager.Save();
                                    AttachmentsManager.DataTable.AcceptChanges();
                                    if (SaveDel < 0)
                                    {
                                        TransactionManager.CancelSave();
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                        return;
                                    }
                                }

                            }
                        }

                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {

                                DataRow drAttach = AttachmentsManager.NewRow();

                                drAttach["TtId"] = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                                drAttach["RefTable"] = JhManager[0]["JhId"].ToString();
                                drAttach["AttId"] = 1;
                                drAttach["IsValid"] = 1;
                                drAttach["Description"] = txtDescription.Text;
                                drAttach["AtContent"] = DBNull.Value;
                                drAttach["FilePath"] = insRows[i]["FilePath"].ToString(); //dtJobHistoryAttachment.Rows[i]["FilePath"].ToString();
                                try
                                {
                                    string FilePath = insRows[i]["FilePath"].ToString();
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(FilePath);
                                    string ImgTarget = Server.MapPath("~/image/Office/StatusChange/") + Path.GetFileName(FilePath);
                                    File.Copy(ImgSoource, ImgTarget, true);
                                }
                                catch (Exception)
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                    return;
                                }


                                drAttach["UserId"] = Utility.GetCurrentUser_UserId();
                                drAttach["ModfiedDate"] = DateTime.Now;

                                AttachmentsManager.AddRow(drAttach);

                                int count = AttachmentsManager.Save();
                                AttachmentsManager.DataTable.AcceptChanges();
                                if (count < 0)
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                    return;
                                }
                            }
                        }
                    }

                    #endregion

                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_EmpMeJob"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                        string MFId = Utility.DecryptQS(HiddenFieldJobHistory["MFId"].ToString());
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(MFId), UpdateTableType, "ویرایش سابقه کار", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_EmpMeJob"] = true;
                        if (CombojPrType.Value.ToString() == "1")
                        {
                            ASPxLabel22.ClientVisible = true;
                            ASPxLabel23.ClientVisible = true;
                            txtjArea.ClientVisible = true;
                            txtjFloor.ClientVisible = true;
                            lblSazeType.ClientVisible = true;
                            CombojSazeType.ClientVisible = true;

                        }
                        else
                        {
                            ASPxLabel22.ClientVisible = false;
                            ASPxLabel23.ClientVisible = false;
                            txtjArea.ClientVisible = false;
                            txtjFloor.ClientVisible = false;
                            lblSazeType.ClientVisible = false;
                            CombojSazeType.ClientVisible = false;

                        }
                        if (ComboPosition.Value.ToString() == "8" || ComboPosition.Value.ToString() == "9")
                            ASPxRoundPanel4.ClientVisible = true;
                        else
                            ASPxRoundPanel4.ClientVisible = false;

                        HiddenFieldJobHistory["JHId"] = Utility.EncryptQS(JhManager[0]["JhId"].ToString());
                        HiddenFieldJobHistory["PageMode"] = Utility.EncryptQS("Edit");
                        RoundPanelJobHistory.HeaderText = "ویرایش";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
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

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/DocumentAttachment/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["َAttachStatus"] = tempFileName;
        }
        return ret;
    }    

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldJobHistory["PageMode"].ToString());
            // CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int WorkFlowCode = -1;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //???????????????????????????
        DocMemberFileManager.FindByCode(int.Parse(MfId), 0);
        int TaskCode = -1;
        if (DocMemberFileManager.Count == 1)
        {
            if (DocMemberFileManager[0]["DocType"].ToString() == "0")
            {
                TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
                WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            }
            else
            {
                TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
                WorkFlowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            }
        }

        if (TaskCode > -1 && WorkFlowCode > -1)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            int Permisssion = -1;
            int PermisssionDocUnit = -1;
            int PermisssionDocUnitRes = -1;
            Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
            PermisssionDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, int.Parse(MfId), (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
            PermisssionDocUnitRes = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, int.Parse(MfId), (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());

            if (Permisssion > 0 || PermisssionDocUnit > 0 || PermisssionDocUnitRes>0 )
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
    #endregion
}
