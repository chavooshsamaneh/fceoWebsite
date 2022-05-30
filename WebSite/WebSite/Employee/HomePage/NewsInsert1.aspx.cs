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

public partial class Employee_News_NewsInsert1 : System.Web.UI.Page
{
    DataTable dtOfImg = null;
    DataTable dtOfOthAtt = null;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!CheckAgent())
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (!IsPostBack)
        {
            #region Reset Sessions
            Session["NewsUpload"] = null;
            Session["TblOfImg2"] = null;

            Session["NewsAttach"] = null;

            Session["OtherAttachDataTable"] = null;
            Session["OtherAttachUpload"] = null;
            #endregion

            #region CheckPermission
            TSP.DataManager.Permission per = TSP.DataManager.NewsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnAddImg.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            #endregion

            #region Create DataTables
            if (Session["TblOfImg2"] == null)
            {
                dtOfImg = new DataTable();
                //dtOfImg.Columns.Add("Image",typeof(byte[]));
                dtOfImg.Columns.Add("ImgId");
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("Desc");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns["ImgId"].AutoIncrement = true;
                dtOfImg.Columns["ImgId"].AutoIncrementSeed = 1;
                dtOfImg.Columns.Add("Type");
                Session["TblOfImg2"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfImg2"];

            CustomAspxDevGridView1.DataSource = dtOfImg;
            CustomAspxDevGridView1.DataBind();


            if (Session["OtherAttachDataTable"] == null)
            {
                dtOfOthAtt = new DataTable();
                dtOfOthAtt.Columns.Add("ImgId");
                dtOfOthAtt.Columns.Add("ImgUrl");
                dtOfOthAtt.Columns.Add("TempImgUrl");
                dtOfOthAtt.Columns.Add("Desc");
                dtOfOthAtt.Columns.Add("fileName");
                dtOfOthAtt.Columns["ImgId"].AutoIncrement = true;
                dtOfOthAtt.Columns["ImgId"].AutoIncrementSeed = 1;
                dtOfOthAtt.Columns.Add("Type");
                dtOfOthAtt.Columns.Add("Code");
                Session["OtherAttachDataTable"] = dtOfOthAtt;
            }
            else
                dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];

            GridViewOtherAttachment.DataSource = dtOfOthAtt;
            GridViewOtherAttachment.DataBind();
            #endregion

            #region SetKey
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                HDNewsId.Value = Server.HtmlDecode(Request.QueryString["NewsId"]).ToString();
            }
            catch
            {
                Response.Redirect("News.aspx");
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string NewsId = Utility.DecryptQS(HDNewsId.Value);

            cmbExGroup.DataBind();
            cmbExGroup.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));
            #endregion

            #region SetMode
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "New":
                    #region New
                    DateTime dt = new DateTime();
                    dt = DateTime.Now;
                    txtDate.DateValue = dt;
                    txtExpireDate.Text = (new Utility.Date()).AddDays(Utility.GetDefaultNewsExpireDate());

                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel1.HeaderText = "جدید";

                    cmbAgent.DataBind();
                    cmbAgent.SelectedIndex = cmbAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Index;
                    #endregion
                    break;
                case "Edit":
                    #region Edit
                    lblTime.Visible = true;
                    txtTime.Visible = true;
                    txtTime.Enabled = false;
                    txtDate.Enabled = false;
                    txtExpireDate.Enabled = true;

                    txtDesc.Enabled = true;
                    txtSummary.Enabled = true;
                    txtTitle.Enabled = true;
                    drpImp.Enabled = true;
                    drpSub.Enabled = true;

                    btnDelete.Enabled = per.CanDelete;
                    btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(NewsId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }

                    FillForm(int.Parse(NewsId));
                    ASPxRoundPanel1.Enabled = true;
                    ASPxRoundPanel1.HeaderText = "ویرایش";
                    #endregion
                    break;
            }
            #endregion

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfImg2"];
        CustomAspxDevGridView1.DataBind();

        int Id = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            Id = CustomAspxDevGridView1.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;
        }
        else
        {
            dtOfImg = (DataTable)Session["TblOfImg2"];
            dtOfImg.Rows[Id].Delete();
            Session["TblOfImg2"] = dtOfImg;
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfImg2"];
            CustomAspxDevGridView1.DataBind();
            dtOfImg = (DataTable)Session["TblOfImg2"];
        }
    }

    protected void GridViewOtherAttachment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewOtherAttachment.DataSource = (DataTable)Session["OtherAttachDataTable"];
        GridViewOtherAttachment.DataBind();

        int Id = -1;
        if (GridViewOtherAttachment.FocusedRowIndex > -1)
        {
            Id = GridViewOtherAttachment.FocusedRowIndex;

        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;
        }
        else
        {
            dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
            dtOfOthAtt.Rows[Id].Delete();
            Session["OtherAttachDataTable"] = dtOfOthAtt;
            GridViewOtherAttachment.DataSource = (DataTable)Session["OtherAttachDataTable"];
            GridViewOtherAttachment.DataBind();
            dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        txtDate.DateValue = dt;
        txtExpireDate.Text = (new Utility.Date()).AddDays(Utility.GetDefaultNewsExpireDate());

        ASPxRoundPanel1.HeaderText = "جدید";

        ClearForm();

        TSP.DataManager.Permission per = TSP.DataManager.NewsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnDelete.Enabled = false;
        btnDelete2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        HDNewsId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel1.HeaderText = "جدید";

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;

        txtExpireDate.Enabled = true;
        txtDate.Enabled = true;
        txtDesc.Enabled = true;
        txtSummary.Enabled = true;
        txtTitle.Enabled = true;
        lblTime.Visible = false;
        txtTime.Visible = false;
        drpSub.Enabled = true;
        drpImp.Enabled = true;
        imgEndAttachment.ClientVisible = false;
        CustomAspxDevGridView1.Columns[4].Visible = true;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string NewsId = Utility.DecryptQS(HDNewsId.Value);

        if (string.IsNullOrEmpty(NewsId))
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
                txtDesc.Enabled = true;
                txtSummary.Enabled = true;
                txtTitle.Enabled = true;

                lblTime.Visible = true;
                txtTime.Visible = true;
                txtTime.Enabled = false;
                txtDate.Enabled = false;
                txtExpireDate.Enabled = true;

                drpSub.Enabled = true;
                drpImp.Enabled = true;
                CustomAspxDevGridView1.Columns[4].Visible = true;

                TSP.DataManager.Permission per = TSP.DataManager.NewsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnDelete.Enabled = per.CanDelete;
                btnDelete2.Enabled = per.CanDelete;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;

                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnDelete"] = btnDelete.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel1.HeaderText = "ویرایش";

            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string NewsId = Utility.DecryptQS(HDNewsId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
                Insert();
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(NewsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(NewsId));
                }

            }

        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string NewsId = Utility.DecryptQS(HDNewsId.Value);

        if (string.IsNullOrEmpty(NewsId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(pageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (pageMode == "Edit" && (!string.IsNullOrEmpty(NewsId)))
            {
                Delete(int.Parse(NewsId));
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("News.aspx");
    }

    //protected void btnRefresh_Click(object sender, EventArgs e)
    //{
    //    ClearForm();
    //}

    protected void btnRemoveFile_Click(object sender, EventArgs e)
    {
        Session["NewsAttach"] = null;
        HyperLinkAttachment.NavigateUrl = "";
    }

    protected void flpImg_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void btnAddImg_Click(object sender, EventArgs e)
    {
        if (Session["TblOfImg2"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImg2"];

            DataRow dr = dtOfImg.NewRow();

            try
            {
                if (Session["NewsUpload"] != null)
                {
                    dr["ImgUrl"] = "~/Image/News/" + System.IO.Path.GetFileName(Session["NewsUpload"].ToString());
                    dr["fileName"] = System.IO.Path.GetFileName(Session["NewsUpload"].ToString());
                    dr["TempImgUrl"] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["NewsUpload"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;

                }

                dr["Desc"] = txtDesc.Text;
                dr["Mode"] = 0;
                dr["Type"] = (int)TSP.DataManager.NewsAttachmentType.Image;
                dtOfImg.Rows.Add(dr);
                CustomAspxDevGridView1.DataSource = dtOfImg;
                CustomAspxDevGridView1.DataBind();

                txtDesc.Text = "";
                Session["NewsUpload"] = null;

                imgEndUploadImg.ClientVisible = false;
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
    }

    protected void btnAddOtherAttachments_Click(object sender, EventArgs e)
    {
        if (Session["OtherAttachDataTable"] != null)
        {
            dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
            DataRow dr = dtOfOthAtt.NewRow();

            try
            {
                if (Session["OtherAttachUpload"] != null)
                {
                    dr["ImgUrl"] = "~/Image/News/" + System.IO.Path.GetFileName(Session["OtherAttachUpload"].ToString());
                    dr["fileName"] = System.IO.Path.GetFileName(Session["OtherAttachUpload"].ToString());
                    dr["TempImgUrl"] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["OtherAttachUpload"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }

                dr["Desc"] = txtOthAttTitle.Text;
                dr["Type"] = (int)TSP.DataManager.NewsAttachmentType.Attachment;
                dtOfOthAtt.Rows.Add(dr);
                GridViewOtherAttachment.DataSource = dtOfOthAtt;
                GridViewOtherAttachment.DataBind();

                txtOthAttTitle.Text = "";
                Session["OtherAttachUpload"] = null;

                imgEndOtherAttachment.ClientVisible = false;
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void flpcAttachment_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveAttachment(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpcOtherAttachment_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveOtherAttachment(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    protected void FillForm(int NewsId)
    {
        try
        {
            TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
            TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();

            NsManager.FindByCode(NewsId);
            txtNewsBody.Html = NsManager[0]["Body"].ToString();
            txtDate.Text = NsManager[0]["Date"].ToString();
            txtTime.Text = NsManager[0]["StrTime"].ToString();
            txtTitle.Text = NsManager[0]["Title"].ToString();
            txtSummary.Text = NsManager[0]["Summary"].ToString();
            txtExpireDate.Text = NsManager[0]["ExpireDate"].ToString();

            if (!string.IsNullOrEmpty(NsManager[0]["SubjectId"].ToString()))
            {
                drpSub.DataBind();
                drpSub.SelectedIndex = drpSub.Items.IndexOfValue(NsManager[0]["SubjectId"].ToString());
            }
            if (!string.IsNullOrEmpty(NsManager[0]["Importance"].ToString()))
            {
                drpImp.DataBind();
                drpImp.SelectedIndex = drpImp.Items.IndexOfValue(NsManager[0]["Importance"].ToString());
            }
            if (!string.IsNullOrEmpty(NsManager[0]["UserLoginType"].ToString()))
            {
                cmbUserLoginType.DataBind();
                cmbUserLoginType.SelectedIndex = cmbUserLoginType.Items.IndexOfValue(NsManager[0]["UserLoginType"].ToString());
            }
            else cmbUserLoginType.SelectedIndex = -1;
            if (!string.IsNullOrEmpty(NsManager[0]["AgentId"].ToString()))
            {
                cmbAgent.DataBind();
                cmbAgent.SelectedIndex = cmbAgent.Items.IndexOfValue(NsManager[0]["AgentId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
            {
                HyperLinkAttachment.NavigateUrl = NsManager[0]["AttachmentUrl"].ToString();
                Session["NewsAttach"] = NsManager[0]["AttachmentUrl"].ToString();
            }
            if (!string.IsNullOrEmpty(NsManager[0]["ExGroupId"].ToString()))
            {
                cmbExGroup.DataBind();
                cmbExGroup.SelectedIndex = cmbAgent.Items.IndexOfValue(NsManager[0]["ExGroupId"].ToString());
            }
            else
            {
                cmbExGroup.DataBind();
                cmbExGroup.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));
                cmbExGroup.SelectedIndex = 0;

            }
            if (!string.IsNullOrEmpty(NsManager[0]["IsNotification"].ToString()))
            {
                CheckBoxIsNotification.Checked = Convert.ToBoolean(NsManager[0]["IsNotification"]);
            }
            #region Fill Images
            ImgManager.FindByNewsCodeAndType(NewsId, (int)TSP.DataManager.NewsAttachmentType.Image);
            //ImgManager.FindByNewsCode(NewsId);
            //ImgManager.DataTable.DefaultView.RowFilter = "Type = 0";
            dtOfImg = (DataTable)Session["TblOfImg2"];
            for (int i = 0; i < ImgManager.Count; i++)
            {
                DataRow dr = dtOfImg.NewRow();

                dr[1] = ImgManager[i]["ImgUrl"].ToString();
                dr[2] = ImgManager[i]["ImgUrl"].ToString();
                dr[3] = ImgManager[i]["Description"].ToString();

                string fileName = Path.GetFileName(ImgManager[0]["ImgUrl"].ToString());
                dr[4] = fileName;
                dr[5] = 1;
                dr[6] = ImgManager[i][0];
                dtOfImg.Rows.Add(dr);
            }
            dtOfImg.AcceptChanges();
            CustomAspxDevGridView1.DataSource = dtOfImg;
            CustomAspxDevGridView1.DataBind();
            #endregion

            #region Fill OtherAttachment
            ImgManager.FindByNewsCodeAndType(NewsId, (int)TSP.DataManager.NewsAttachmentType.Attachment);
            //ImgManager.FindByNewsCode(NewsId);
            //ImgManager.DataTable.DefaultView.RowFilter = "Type = 1";
            dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
            for (int i = 0; i < ImgManager.Count; i++)
            {
                DataRow dr = dtOfOthAtt.NewRow();
                dr["ImgUrl"] = ImgManager[i]["ImgUrl"].ToString();
                dr["TempImgUrl"] = ImgManager[i]["ImgUrl"].ToString();
                dr["Desc"] = ImgManager[i]["Description"].ToString();

                string fileName = Path.GetFileName(ImgManager[0]["ImgUrl"].ToString());
                dr["fileName"] = fileName;
                dr["Code"] = ImgManager[i]["ImgNewsId"];
                dtOfOthAtt.Rows.Add(dr);
            }
            dtOfOthAtt.AcceptChanges();
            GridViewOtherAttachment.DataSource = dtOfOthAtt;
            GridViewOtherAttachment.DataBind();
            #endregion
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "نمایش اطلاعات امکان پذیر نمی باشد";
        }
    }

    protected void ClearForm()
    {
        //txtExpireDate.Text = "";
        txtSummary.Text = "";
        txtDesc.Text = "";
        txtTime.Text = "";
        txtTitle.Text = "";
        txtNewsBody.Html = "";
        drpImp.Value = "0";
        drpSub.DataBind();
        drpSub.SelectedIndex = -1;

        dtOfImg = (DataTable)Session["TblOfImg2"];
        dtOfImg.Rows.Clear();
        CustomAspxDevGridView1.DataSource = dtOfImg;
        CustomAspxDevGridView1.DataBind();
        Session["NewsAttach"] = null;

        dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
        dtOfOthAtt.Rows.Clear();
        GridViewOtherAttachment.DataSource = dtOfOthAtt;
        GridViewOtherAttachment.DataBind();
        Session["OtherAttachUpload"] = null;

        cmbUserLoginType.SelectedIndex = 0;

        cmbAgent.DataBind();
        cmbAgent.SelectedIndex = cmbAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Index;
        cmbExGroup.DataBind();
        cmbExGroup.Items.Insert(0, new DevExpress.Web.ListEditItem("------------", null));
        cmbExGroup.SelectedIndex = 0;
        CheckBoxIsNotification.Checked = false;
    }

    protected void Delete(int NewsId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        TSP.DataManager.NewsIdeaManager IdeaManager = new TSP.DataManager.NewsIdeaManager();

        trans.Add(ImgManager);
        trans.Add(IdeaManager);
        trans.Add(NsManager);

        NsManager.FindByCode(NewsId);
        ImgManager.FindByNewsCode(NewsId);
        IdeaManager.FindByNewsCode(NewsId);
        string ImgUrl = "";

        if (NsManager.Count == 1)
            try
            {
                trans.BeginSave();
                if (ImgManager.Count > 0)
                {
                    int y = ImgManager.Count;
                    for (int i = 0; i < y; i++)
                    {
                        ImgManager[0].Delete();
                    }
                    ImgManager.Save();
                }

                if (IdeaManager.Count > 0)
                {
                    int z = IdeaManager.Count;
                    for (int i = 0; i < z; i++)
                    {
                        IdeaManager[0].Delete();
                    }
                    IdeaManager.Save();
                }

                ImgUrl = Server.MapPath(NsManager[0]["AttachmentUrl"].ToString());
                NsManager[0].Delete();

                int cnt = NsManager.Save();
                trans.EndSave();

                if (cnt == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    HDNewsId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    ASPxRoundPanel1.HeaderText = "جدید";
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnDelete"] = btnDelete.Enabled;

                    txtDate.Enabled = true;
                    txtDesc.Enabled = true;
                    txtSummary.Enabled = true;
                    txtTitle.Enabled = true;
                    lblTime.Visible = false;
                    txtTime.Visible = false;
                    drpSub.Enabled = true;
                    drpImp.Enabled = true;

                    ClearForm();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                }
            }
            catch (Exception err)
            {
                trans.CancelSave();

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
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین ردیفی وجود ندارد.مجددا بازخوانی نمایید";
        }


        try
        {
            if (ImgUrl == "") return;
            if (File.Exists(ImgUrl))
                File.Delete(ImgUrl);
        }
        catch (Exception ex)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(ex);
            string Message = "ذخیره انجام شد. خطایی در حذف فایل انجام گرفته است";
            if (Utility.ShowExceptionError())
            {
                Message = Message + ex.Message;
            }
            ShowErrorMessage(Message);
        }
    }

    protected void Insert()
    {
        int? SubId = null;
        int? ImpId = null;
        int? ULtId = null;
        int? AgentId = null;
        int? ExGroupId = null;
        if (!string.IsNullOrEmpty(drpSub.Value.ToString()))
            SubId = int.Parse(drpSub.Value.ToString());
        if (!string.IsNullOrEmpty(drpImp.Value.ToString()))
            ImpId = int.Parse(drpImp.Value.ToString());
        if (!string.IsNullOrEmpty(cmbUserLoginType.Value.ToString()))
            ULtId = int.Parse(cmbUserLoginType.Value.ToString());
        if (!string.IsNullOrEmpty(cmbAgent.Value.ToString()))
            AgentId = int.Parse(cmbAgent.Value.ToString());
        if (cmbExGroup.SelectedItem != null && cmbExGroup.Value != null)
            ExGroupId = int.Parse(cmbExGroup.Value.ToString());


        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        trans.Add(NsManager);
        trans.Add(ImgManager);

        try
        {
            string summary = "";
            DataRow drNews = NsManager.NewRow();
            drNews["SubjectId"] = SubId;
            drNews["Title"] = txtTitle.Text;
            drNews["Body"] = txtNewsBody.Html;
            drNews["Date"] = txtDate.Text;
            drNews["ExpireDate"] = txtExpireDate.Text;
            if (txtSummary.Text.Length > 350)
                summary = txtSummary.Text.Substring(0, 350);
            else
                summary = txtSummary.Text;
            drNews["Summary"] = summary;
            drNews["TimeHour"] = DateTime.Now;
            //drNews["TimeMin"] = DateTime.Now.Minute;
            drNews["CountOfRead"] = 0;
            drNews["Importance"] = ImpId;
            drNews["UserLoginType"] = ULtId;
            drNews["AgentId"] = AgentId;
            if (ExGroupId != null)
                drNews["ExGroupId"] = ExGroupId;
            else
                drNews["ExGroupId"] = DBNull.Value;

            drNews["IsNotification"] = CheckBoxIsNotification.Checked;

            if (Session["NewsAttach"] != null)
            {
                string FileName = System.IO.Path.GetFileName(Session["NewsAttach"].ToString());
                drNews["AttachmentUrl"] = "~/Image/News/" + FileName;
                HyperLinkAttachment.NavigateUrl = Server.MapPath("~/Image/News/") + FileName;
            }

            drNews["UserId"] = Utility.GetCurrentUser_UserId();
            drNews["ModifiedDate"] = DateTime.Now;
            NsManager.AddRow(drNews);
            trans.BeginSave();
            int cnt = NsManager.Save();

            if (cnt == 1)
            {
                int NewsId = int.Parse(NsManager[0]["NewsId"].ToString());
                dtOfImg = (DataTable)Session["TblOfImg2"];
                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = ImgManager.NewRow();
                        drImg["NewsId"] = NewsId;
                        drImg["Image"] = DBNull.Value;
                        drImg["ImgUrl"] = "~/Image/News/" + dtOfImg.Rows[i]["fileName"].ToString(); ;//dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["Description"] = dtOfImg.Rows[i]["Desc"].ToString();
                        drImg["Type"] = dtOfImg.Rows[i]["Type"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModifiedDate"] = DateTime.Now;
                        ImgManager.AddRow(drImg);
                        int imgcnt = ImgManager.Save();
                        ImgManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            if (!string.IsNullOrEmpty(dtOfImg.Rows[i]["ImgUrl"].ToString()))
                            {
                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                if (File.Exists(ImgSoource))
                                {
                                    string ImgTarget = Server.MapPath("~/Image/News/") + dtOfImg.Rows[i]["fileName"].ToString();
                                    File.Move(ImgSoource, ImgTarget);
                                }
                            }
                        }
                    }
                }



                dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
                if (dtOfOthAtt.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfOthAtt.DefaultView.Count; i++)
                    {
                        DataRow drImg = ImgManager.NewRow();
                        drImg["NewsId"] = NewsId;
                        drImg["Image"] = DBNull.Value;
                        drImg["ImgUrl"] = "~/Image/News/" + dtOfOthAtt.Rows[i]["fileName"].ToString(); ;//dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["Description"] = dtOfOthAtt.Rows[i]["Desc"].ToString();
                        drImg["Type"] = dtOfOthAtt.Rows[i]["Type"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModifiedDate"] = DateTime.Now;
                        ImgManager.AddRow(drImg);
                        int imgcnt = ImgManager.Save();
                        ImgManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            if (!string.IsNullOrEmpty(dtOfOthAtt.Rows[i]["ImgUrl"].ToString()))
                            {
                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfOthAtt.Rows[i]["fileName"].ToString();
                                if (File.Exists(ImgSoource))
                                {
                                    string ImgTarget = Server.MapPath("~/Image/News/") + dtOfOthAtt.Rows[i]["fileName"].ToString();
                                    File.Move(ImgSoource, ImgTarget);
                                }
                            }
                        }
                    }
                }
                trans.EndSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                TSP.DataManager.Permission per = TSP.DataManager.NewsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnDelete.Enabled = per.CanDelete;
                btnDelete2.Enabled = per.CanDelete;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnDelete"] = btnDelete.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                HDNewsId.Value = Utility.EncryptQS(NsManager[0]["NewsId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel1.HeaderText = "ویرایش";

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }
        catch (Exception err)
        {
            trans.CancelSave();
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
            this.LabelWarning.Text += err.Message;

        }


        if (Session["NewsAttach"] != null)
        {
            try
            {
                string FileName = System.IO.Path.GetFileName(Session["NewsAttach"].ToString());
                string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                if (File.Exists(ImgSoource))
                {
                    string ImgTarget = Server.MapPath("~/Image/News/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    Session["NewsAttach"] = null;
                }
            }
            catch (Exception ex)
            {
                trans.CancelSave();
                Utility.SaveWebsiteError(ex);
                string Message = "ذخیره انجام شد. خطایی در انتقال فایل انجام گرفته است";
                if (Utility.ShowExceptionError())
                {
                    Message = Message + ex.Message;
                }
                ShowErrorMessage(Message);
            }
        }
    }

    protected void Edit(int NewsId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        trans.Add(NsManager);
        trans.Add(ImgManager);

        int? SubId = null;
        int? ImpId = null;
        int? ULtId = null;
        int? AgentId = null;
        int? ExGroupId = null;
        System.Collections.ArrayList DeleteFiles = new System.Collections.ArrayList();
        if (!string.IsNullOrEmpty(drpSub.Value.ToString()))
            SubId = int.Parse(drpSub.Value.ToString());
        if (!string.IsNullOrEmpty(drpImp.Value.ToString()))
            ImpId = int.Parse(drpImp.Value.ToString());
        if (!string.IsNullOrEmpty(cmbUserLoginType.Value.ToString()))
            ULtId = int.Parse(cmbUserLoginType.Value.ToString());
        if (!string.IsNullOrEmpty(cmbAgent.Value.ToString()))
            AgentId = int.Parse(cmbAgent.Value.ToString());
        if (cmbExGroup.SelectedItem != null && cmbExGroup.Value != null)
            ExGroupId = Convert.ToInt32(cmbExGroup.Value);

        NsManager.FindByCode(NewsId);
        if (Session["TblOfImg2"] != null)
            dtOfImg = (DataTable)Session["TblOfImg2"];
        if (NsManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations);
            return;
        }
        if (Session["OtherAttachDataTable"] != null)
            dtOfOthAtt = (DataTable)Session["OtherAttachDataTable"];
        try
        {
            string summary = "";
            trans.BeginSave();
            NsManager[0].BeginEdit();

            NsManager[0]["SubjectId"] = SubId;
            NsManager[0]["Title"] = txtTitle.Text;
            NsManager[0]["Body"] = txtNewsBody.Html;
            NsManager[0]["ExpireDate"] = txtExpireDate.Text;
            if (txtSummary.Text.Length > 350)
                summary = txtSummary.Text.Substring(0, 350);
            else
                summary = txtSummary.Text;
            NsManager[0]["Summary"] = summary;
            NsManager[0]["Importance"] = ImpId;
            NsManager[0]["UserLoginType"] = ULtId;
            NsManager[0]["AgentId"] = AgentId;
            if (ExGroupId != null)
                NsManager[0]["ExGroupId"] = ExGroupId;
            else
                NsManager[0]["ExGroupId"] = DBNull.Value;
            NsManager[0]["IsNotification"] = CheckBoxIsNotification.Checked;

            if (Session["NewsAttach"] != null)
            {
                string FileName = System.IO.Path.GetFileName(Session["NewsAttach"].ToString());
                if (!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    if (File.Exists(ImgSoource))//------doesnt have attach---
                    {
                        string Target = MapPath(NsManager[0]["AttachmentUrl"].ToString());
                        if (File.Exists(Target))
                            File.Delete(Target);
                    }
                }
                NsManager[0]["AttachmentUrl"] = "~/Image/News/" + FileName;
                HyperLinkAttachment.NavigateUrl = NsManager[0]["AttachmentUrl"].ToString();
            }
            else
            {
                if (!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
                {

                    string Target = MapPath(NsManager[0]["AttachmentUrl"].ToString());
                    File.Delete(Target);

                    NsManager[0]["AttachmentUrl"] = "";
                }
            }
            NsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            NsManager[0]["ModifiedDate"] = DateTime.Now;

            NsManager[0].EndEdit();
            int cnt = NsManager.Save();
            if (cnt == 1)
            {
                #region Save Images

                if (dtOfImg != null && dtOfImg.GetChanges() != null)
                {
                    DataRow[] delRows = dtOfImg.Select("Mode='1'", null, DataViewRowState.Deleted);
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        ImgManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                        DeleteFiles.Add(ImgManager[0]["ImgUrl"].ToString());
                        ImgManager[0].Delete();
                        ImgManager.Save();
                    }
                    ImgManager.DataTable.AcceptChanges();
                    dtOfImg.DefaultView.RowFilter = "Mode='0'";
                    if (dtOfImg.DefaultView.Count > 0)
                    {
                        for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                        {
                            DataRow drImg = ImgManager.NewRow();
                            drImg["NewsId"] = NewsId;
                            drImg["ImgUrl"] = "~/image/News/" + dtOfImg.DefaultView[i]["fileName"].ToString();
                            drImg["Description"] = dtOfImg.DefaultView[i]["Desc"];
                            drImg["Type"] = dtOfImg.Rows[i]["Type"].ToString();
                            drImg["UserId"] = Utility.GetCurrentUser_UserId();
                            drImg["ModifiedDate"] = DateTime.Now;
                            ImgManager.AddRow(drImg);

                            int imgcnt = ImgManager.Save();
                            ImgManager.DataTable.AcceptChanges();
                            if (imgcnt == 1)
                            {
                                if (!string.IsNullOrEmpty(dtOfImg.DefaultView[i]["ImgUrl"].ToString()))
                                {
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.DefaultView[i]["fileName"].ToString();
                                    if (File.Exists(ImgSoource))
                                    {
                                        string ImgTarget = Server.MapPath("~/image/News/") + dtOfImg.DefaultView[i]["fileName"].ToString();//Server.MapPath(dtOfImg.DefaultView[i]["ImgUrl"].ToString());
                                        File.Move(ImgSoource, ImgTarget);
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Save OtherAttachments

                if (dtOfOthAtt != null && dtOfOthAtt.GetChanges() != null)
                {
                    DataRow[] delRows = dtOfOthAtt.Select(null, null, DataViewRowState.Deleted);
                    DataRow[] InsertRows = dtOfOthAtt.Select(null, null, DataViewRowState.Added);

                    //DataRow[] delRows = dtOfOthAtt.Select("Mode='1'", null, DataViewRowState.Deleted);
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        ImgManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                        DeleteFiles.Add(ImgManager[0]["ImgUrl"].ToString());
                        ImgManager[0].Delete();
                        ImgManager.Save();
                    }
                    ImgManager.DataTable.AcceptChanges();
                    // dtOfOthAtt.DefaultView.RowFilter = "Mode='0'";
                    if (InsertRows.Length > 0)
                    {
                        for (int i = 0; i < InsertRows.Length; i++)
                        {
                            DataRow drImg = ImgManager.NewRow();
                            drImg["NewsId"] = NewsId;
                            drImg["ImgUrl"] = "~/image/News/" + InsertRows[i]["fileName"].ToString();
                            drImg["Description"] = InsertRows[i]["Desc"];
                            drImg["Type"] = InsertRows[i]["Type"].ToString();
                            drImg["UserId"] = Utility.GetCurrentUser_UserId();
                            drImg["ModifiedDate"] = DateTime.Now;
                            ImgManager.AddRow(drImg);

                            int imgcnt = ImgManager.Save();
                            ImgManager.DataTable.AcceptChanges();
                            if (imgcnt == 1)
                            {
                                if (!string.IsNullOrEmpty(InsertRows[i]["ImgUrl"].ToString()))
                                {
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + InsertRows[i]["fileName"].ToString();
                                    if (File.Exists(ImgSoource))
                                    {
                                        string ImgTarget = Server.MapPath("~/image/News/") + InsertRows[i]["fileName"].ToString();
                                        File.Move(ImgSoource, ImgTarget);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                trans.EndSave();
                HDNewsId.Value = Utility.EncryptQS(NsManager[0]["NewsId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel1.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
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


        if (Session["NewsAttach"] != null)
        {
            try
            {
                string FileName = System.IO.Path.GetFileName(Session["NewsAttach"].ToString());
                string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                if (File.Exists(ImgSoource))
                {
                    string ImgTarget = Server.MapPath("~/Image/News/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    Session["NewsAttach"] = null;
                }
            }
            catch (Exception ex)
            {
                trans.CancelSave();
                Utility.SaveWebsiteError(ex);
                string Message = "ذخیره انجام شد. خطایی در انتقال فایل انجام گرفته است";
                if (Utility.ShowExceptionError())
                {
                    Message = Message + ex.Message;
                }
                ShowErrorMessage(Message);
            }
        }

        //------------delete file-------------
        if (!Utility.IsDBNullOrNullValue(DeleteFiles))
        {
            for (int i = 0; i < DeleteFiles.Count; i++)
            {
                try
                {
                    string filename = Server.MapPath(DeleteFiles[i].ToString());
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);
                }
                catch
                {
                }
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
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/News/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["NewsUpload"] = tempFileName;
        }
        return ret;
    }

    protected string SaveAttachment(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/News/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["NewsAttach"] = tempFileName;
        }
        return ret;
    }

    protected string SaveOtherAttachment(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/News/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["OtherAttachUpload"] = tempFileName;
        }
        return ret;
    }

    private void ShowErrorMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    bool CheckAgent()
    {
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
        {
            cmbAgent.Enabled = false;
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
}
