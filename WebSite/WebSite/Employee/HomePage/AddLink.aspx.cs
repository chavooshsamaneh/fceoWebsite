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

public partial class Employee_HomePage_AddLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.LinksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["LiId"]))
            {
                Response.Redirect("Links.aspx");
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                LinkId.Value = Server.HtmlDecode(Request.QueryString["LiId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string LiId = Utility.DecryptQS(LinkId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ComboType.DataBind();
            ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            ComboType.SelectedIndex = -1;


            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(LiId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(LiId));
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

                    if (string.IsNullOrEmpty(LiId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(LiId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";


                    break;

            }

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
    protected void FillForm(int LiId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.LinksManager manager = new TSP.DataManager.LinksManager();
        manager.FindByCode(LiId);
        if (manager.Count == 1)
        {
            txtDesc.Text = manager[0]["Description"].ToString();
            txtLiName.Text = manager[0]["LiName"].ToString();
            txtLinkAddress.Text = manager[0]["LinkAddress"].ToString();
            if (!Utility.IsDBNullOrNullValue(manager[0]["ImageUrl"]))
            {
                ImgLink.ClientVisible = true;
                ImgLink.ImageUrl = manager[0]["ImageUrl"].ToString();
                Session["filename"] = manager[0]["ImageUrl"].ToString();
            }
            else
            {
                ImgLink.ClientVisible = false;
                Session["filename"] = "";
            }
            if (Convert.ToBoolean(manager[0]["ShowInHomePage"]))
                chkShow.Checked = true;
            else chkShow.Checked = false;

            ComboType.DataBind();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(manager[0]["TypeId"].ToString());
        }
    }

    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtLiName.Text = "";
        txtLinkAddress.Text = "";
        ComboType.DataBind();
        ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
        ComboType.SelectedIndex = -1;
        chkShow.Checked = false;
        ImgLink.ImageUrl = "";

    }
    protected void Disable()
    {
        txtDesc.Enabled = false;
        txtLinkAddress.Enabled = false;
        txtLiName.Enabled = false;
        ComboType.Enabled = false;
        FileUploadimg.Enabled = false;
        chkShow.Enabled = false;
    }
    protected void Enable()
    {
        txtDesc.Enabled = true;
        txtLinkAddress.Enabled = true;
        txtLiName.Enabled = true;
        ComboType.Enabled = true;
        FileUploadimg.Enabled = true;
        chkShow.Enabled = true;
    }
    protected void Edit(int LiId)
    {
        TSP.DataManager.LinksManager manager = new TSP.DataManager.LinksManager();
        manager.FindByCode(LiId);
        if (manager.Count == 1)
        {
            try
            {
                bool ShowInHomePage = Convert.ToBoolean(manager[0]["ShowInHomePage"]);
                manager[0].BeginEdit();
                manager[0]["LinkAddress"] = txtLinkAddress.Text;
                manager[0]["LiName"] = txtLiName.Text;
                manager[0]["TypeId"] = ComboType.Value;
                manager[0]["Description"] = txtDesc.Text;
                if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                    manager[0]["ImageUrl"] = "~/Image/HomePage/Links/" + System.IO.Path.GetFileName(Session["filename"].ToString());
                if (chkShow.Checked)
                {
                    manager[0]["ShowInHomePage"] = 1;
                    if (!ShowInHomePage)
                    {
                        int MaxOrderCode = manager.FindMaxOrderCode();
                        manager[0]["OrderCode"] = MaxOrderCode + 1;
                    }
                }
                else manager[0]["ShowInHomePage"] = 0;
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;

                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {

                    LinkId.Value = Utility.EncryptQS(manager[0]["LiId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    else
                        this.LabelWarning.Text = "ذخیره انجام شد. برای نمایش لینک در صفحه اول سایت بهتر است تصویری برای آن انتخاب کنید";
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
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
        else
        {
        }
        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/Links/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            Session["filename"] = MapPath("~/Image/HomePage/Links/") + System.IO.Path.GetFileName(Session["filename"].ToString());
            ImgLink.ImageUrl = Session["filename"].ToString();
        }
        catch
        {
        }

    }
    protected void Insert()
    {
        TSP.DataManager.LinksManager manager = new TSP.DataManager.LinksManager();

        try
        {

            DataRow row = manager.NewRow();
            row["LinkAddress"] = txtLinkAddress.Text;
            row["LiName"] = txtLiName.Text;
            row["TypeId"] = ComboType.Value;
            row["Description"] = txtDesc.Text;
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                row["ImageUrl"] = "~/Image/HomePage/Links/" + System.IO.Path.GetFileName(Session["filename"].ToString());
            if (chkShow.Checked)
            {
                row["ShowInHomePage"] = 1;
                int MaxOrderCode = manager.FindMaxOrderCode();
                row["OrderCode"] = MaxOrderCode + 1;
            }
            else row["ShowInHomePage"] = 0;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;

            manager.AddRow(row);

            int cn = manager.Save();
            if (cn == 1)
            {

                LinkId.Value = Utility.EncryptQS(manager[0]["LiId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                TSP.DataManager.Permission per = TSP.DataManager.LinksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

            }


            else
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام نشد";
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
            return;
        }

        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/Links/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            ImgLink.ImageUrl = MapPath("~/Image/HomePage/Links/") + System.IO.Path.GetFileName(Session["filename"].ToString());
            Session["filename"] = ImgLink.ImageUrl;
        }
        catch
        {
        }


    }
    protected void Delete(int LiId)
    {

        TSP.DataManager.LinksManager managerEdit = new TSP.DataManager.LinksManager();
        managerEdit.FindByCode(LiId);
        if (managerEdit.Count == 1)
        {
            try
            {

                managerEdit[0].Delete();

                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    LinkId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    ASPxRoundPanel2.HeaderText = "جدید";
                    ClearForm();
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
        else
        {
        }

    }
    public string SavePostedFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Entezami/ReplyDocuments/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["filename"] = tempFileName;
        }
        return ret;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        TSP.DataManager.Permission per = TSP.DataManager.LinksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        LinkId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string LiId = Utility.DecryptQS(LinkId.Value);

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
                if (string.IsNullOrEmpty(LiId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(int.Parse(LiId));
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Links.aspx?");

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string LiId = Utility.DecryptQS(LinkId.Value);

        if (string.IsNullOrEmpty(LiId))
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

                TSP.DataManager.Permission per = TSP.DataManager.LinksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());



                btnSave.Enabled = per.CanNew;
                btnSave2.Enabled = per.CanNew;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }

        }

    }

    protected void FileUploadimg_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SavePostedFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
}


