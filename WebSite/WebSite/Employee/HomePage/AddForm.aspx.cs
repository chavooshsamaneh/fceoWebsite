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

public partial class Employee_HomePage_AddForm : System.Web.UI.Page
{
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
            TSP.DataManager.Permission per = TSP.DataManager.FormsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["FoId"]))
            {
                Response.Redirect("Forms.aspx");
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                FormId.Value = Server.HtmlDecode(Request.QueryString["FoId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string FoId = Utility.DecryptQS(FormId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(FoId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    HpLink.Visible = true;
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(FoId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    HpLink.Visible = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;
                case "Edit":
                    Enable();
                    HpLink.Visible = true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(FoId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(FoId));
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
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HpLink.Visible = false;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        TSP.DataManager.Permission per = TSP.DataManager.FormsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        FormId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string FoId = Utility.DecryptQS(FormId.Value);

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

                if (string.IsNullOrEmpty(FoId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(FoId));
                }

            }

        }



    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forms.aspx?");

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string FoId = Utility.DecryptQS(FormId.Value);

        if (string.IsNullOrEmpty(FoId))
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
                HpLink.Visible = true;

                TSP.DataManager.Permission per = TSP.DataManager.FormsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


                btnSave.Enabled = per.CanNew;
                btnSave2.Enabled = per.CanNew;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }

        }

    }
    #endregion

    #region Methods
    protected void FillForm(int FoId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.FormsManager manager = new TSP.DataManager.FormsManager();
        manager.FindByCode(FoId);
        if (manager.Count == 1)
        {
            txtDesc.Text = manager[0]["Description"].ToString();
            txtFoCode.Text = manager[0]["FoCode"].ToString();
            txtFoName.Text = manager[0]["FoName"].ToString();
            // txtGroupName.Text = manager[0]["GroupName"].ToString();
            cmbFormType.DataBind();
            cmbFormType.SelectedIndex = cmbFormType.Items.FindByValue(manager[0]["FormTypeId"].ToString()).Index;
            HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();

        }
    }
    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtFoCode.Text = "";
        txtFoName.Text = "";
        cmbFormType.SelectedIndex = -1;
    }
    protected void Disable()
    {
        txtDesc.Enabled = false;
        txtFoCode.Enabled = false;
        txtFoName.Enabled = false;
        cmbFormType.Enabled = false;
        flp.Enabled = false;

    }
    protected void Enable()
    {
        txtDesc.Enabled = true;
        txtFoCode.Enabled = true;
        txtFoName.Enabled = true;
        cmbFormType.Enabled = true;
        flp.Enabled = true;
    }
    protected void Edit(int FoId)
    {
        string fileName = "", pathAx = "", extension = "";

        TSP.DataManager.FormsManager manager = new TSP.DataManager.FormsManager();
        manager.FindByCode(FoId);
        if (manager.Count == 1)
        {
            try
            {
                manager[0].BeginEdit();
                manager[0]["FoCode"] = txtFoCode.Text;
                manager[0]["FoName"] = txtFoName.Text;
                if (cmbFormType.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbFormType.SelectedItem.Value))
                {
                    manager[0]["FormTypeId"] = cmbFormType.SelectedItem.Value;
                    manager[0]["GroupName"] = cmbFormType.SelectedItem.Text;
                }
                manager[0]["Description"] = txtDesc.Text;

                bool imgEdit = false;
                if (flp.HasFile)
                {
                    if ((!string.IsNullOrEmpty(manager[0]["PdfUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(manager[0]["PdfUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(manager[0]["PdfUrl"].ToString()));
                            extension = Path.GetExtension(flp.FileName);
                            extension = extension.ToLower();
                            if (flp.HasFile)
                            {
                                try
                                {
                                    fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx = Server.MapPath("~/image/Temp/");
                                    flp.SaveAs(pathAx + fileName);

                                    manager[0]["PdfUrl"] = "~/image/Pdf/Forms/" + fileName;
                                    imgEdit = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }
                    }
                    else
                    {
                        try
                        {
                            extension = Path.GetExtension(flp.FileName);
                            extension = extension.ToLower();
                            if (flp.HasFile)
                            {
                                try
                                {
                                    fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx = Server.MapPath("~/image/Temp/");
                                    flp.SaveAs(pathAx + fileName);
                                    manager[0]["PdfUrl"] = "~/image/Pdf/Forms/" + fileName;
                                    imgEdit = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }
                    }
                }
                else
                {
                    if ((string.IsNullOrEmpty(manager[0]["PdfUrl"].ToString())))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                        return;
                    }
                }
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;

                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {
                    if (flp.HasFile)
                    {
                        if (imgEdit == true)
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + fileName;
                            string ImgTarget = Server.MapPath("~/image/Pdf/Forms/") + fileName;
                            System.IO.File.Move(ImgSoource, ImgTarget);

                        }

                    }

                    FormId.Value = Utility.EncryptQS(manager[0]["FoId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
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
                    else if (se.Number == 2627)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد فرم تکراری می باشد.";
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


    }
    protected void Insert()
    {
        TSP.DataManager.FormsManager manager = new TSP.DataManager.FormsManager();

        try
        {
            if (flp.HasFile)
            {
                string extension = Path.GetExtension(flp.FileName);
                extension = extension.ToLower();
                string fileName = flp.FileName;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                return;
            }

            DataRow row = manager.NewRow();
            row["FoCode"] = txtFoCode.Text;
            row["FoName"] = txtFoName.Text;
              
            if (cmbFormType.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbFormType.SelectedItem.Value))
            {
                row["FormTypeId"] = cmbFormType.SelectedItem.Value;
                row["GroupName"] = cmbFormType.SelectedItem.Text;
            }
            row["Description"] = txtDesc.Text;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;

            string path = null;
            string p = null;
            if (flp.HasFile)
            {
                path = Server.MapPath("~/image/Pdf/Forms/");
                p = Utility.GenerateName(Path.GetExtension(flp.FileName));

                row["PdfUrl"] = "~/image/Pdf/Forms/" + p;
            }

            manager.AddRow(row);

            int cn = manager.Save();
            if (cn == 1)
            {
                if (flp.HasFile)
                    flp.SaveAs(path + p);

                FormId.Value = Utility.EncryptQS(manager[0]["FoId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                HpLink.Visible = true;
                HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                TSP.DataManager.Permission per = TSP.DataManager.FormsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد فرم تکراری می باشد.";
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

