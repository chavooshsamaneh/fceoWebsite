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

public partial class Employee_HomePage_AddRule : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.RulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["RuId"]))
            {
                Response.Redirect("Rules.aspx");
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                RuleId.Value = Server.HtmlDecode(Request.QueryString["RuId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string RuId = Utility.DecryptQS(RuleId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(RuId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    HpLink.Visible = true;
                    HpLinkImg.Visible = true;
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(RuId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    HpLink.Visible = false;
                    HpLinkImg.Visible = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;
                case "Edit":
                    Enable();
                    HpLink.Visible = true;
                    HpLinkImg.Visible = true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(RuId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(RuId));
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
        HpLinkImg.Visible = false;


        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        TSP.DataManager.Permission per = TSP.DataManager.RulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        RuleId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string RuId = Utility.DecryptQS(RuleId.Value);

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

                if (string.IsNullOrEmpty(RuId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(RuId));
                }

            }

        }



    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rules.aspx?");

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string RuId = Utility.DecryptQS(RuleId.Value);

        if (string.IsNullOrEmpty(RuId))
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
                HpLinkImg.Visible = true;

                TSP.DataManager.Permission per = TSP.DataManager.RulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
    protected void FillForm(int RuId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.RulesManager manager = new TSP.DataManager.RulesManager();
        manager.FindByCode(RuId);
        if (manager.Count == 1)
        {
            txtDesc.Text = manager[0]["Description"].ToString();
            txtRuName.Text = manager[0]["RuName"].ToString();
            HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();
            HpLinkImg.NavigateUrl = manager[0]["ImageUrl"].ToString();
            cmbRulesType.DataBind();
            cmbRulesType.SelectedIndex = cmbRulesType.Items.FindByValue(manager[0]["RulesTypeId"].ToString()).Index;
        }
    }

    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtRuName.Text = "";
        cmbRulesType.SelectedIndex = -1;

    }
    protected void Disable()
    {
        txtDesc.Enabled = false;
        txtRuName.Enabled = false;
        flp.Enabled = false;
        flpImg.Enabled = false;
        cmbRulesType.Enabled = false;

    }
    protected void Enable()
    {
        txtDesc.Enabled = true;
        txtRuName.Enabled = true;
        flp.Enabled = true;
        flpImg.Enabled = true;
        cmbRulesType.Enabled = true;

    }
    protected void Edit(int RuId)
    {
        string fileName = "", pathAx = "", extension = "";
        string fileName1 = "", pathAx1 = "", extension1 = "";

        TSP.DataManager.RulesManager manager = new TSP.DataManager.RulesManager();
        manager.FindByCode(RuId);
        if (manager.Count == 1)
        {
            try
            {
                manager[0].BeginEdit();
                manager[0]["RuName"] = txtRuName.Text;
                manager[0]["Description"] = txtDesc.Text;
                if (cmbRulesType.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbRulesType.SelectedItem.Value))
                {
                    manager[0]["RulesTypeId"] = cmbRulesType.SelectedItem.Value;
                }
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
                            //if (extension == ".pdf")
                            //{
                            if (flp.HasFile)
                            {
                                try
                                {
                                    fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx = Server.MapPath("~/image/Temp/");
                                    flp.SaveAs(pathAx + fileName);

                                    manager[0]["PdfUrl"] = "~/image/Pdf/Rules/" + fileName;
                                    imgEdit = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                            //}
                            //else
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "فرمت وارد شده برای فایل نامعتبر است";
                            //}
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
                            //if (extension == ".pdf")
                            //{
                            if (flp.HasFile)
                            {
                                try
                                {
                                    fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx = Server.MapPath("~/image/Temp/");
                                    flp.SaveAs(pathAx + fileName);
                                    manager[0]["PdfUrl"] = "~/image/Pdf/Rules/" + fileName;
                                    imgEdit = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                            //}
                            //else
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "فرمت وارد شده برای فایل نامعتبر است";
                            //}
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

                bool imgEdit1 = false;
                if (flpImg.HasFile)
                {
                    if ((!string.IsNullOrEmpty(manager[0]["ImageUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(manager[0]["ImageUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(manager[0]["ImageUrl"].ToString()));
                            extension1 = Path.GetExtension(flpImg.FileName);
                            extension1 = extension1.ToLower();
                            //if (extension1 == ".gif" || extension1 == ".jpg" || extension1 == ".jpeg" || extension1 == ".png")
                            //{
                            if (flpImg.HasFile)
                            {
                                try
                                {
                                    fileName1 = Utility.GenerateName(Path.GetExtension(flpImg.FileName));
                                    pathAx1 = Server.MapPath("~/image/Temp/");
                                    flpImg.SaveAs(pathAx1 + fileName1);

                                    manager[0]["ImageUrl"] = "~/image/Rules/" + fileName1;
                                    imgEdit1 = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                            //}
                            //else
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "فرمت وارد شده برای عکس نامعتبر است";
                            //}
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
                            extension1 = Path.GetExtension(flpImg.FileName);
                            extension1 = extension.ToLower();
                            //if (extension1 == ".gif" || extension1 == ".jpg" || extension1 == ".jpeg" || extension1 == ".png")
                            //{
                            if (flpImg.HasFile)
                            {
                                try
                                {
                                    fileName1 = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx1 = Server.MapPath("~/image/Temp/");
                                    flpImg.SaveAs(pathAx1 + fileName1);
                                    manager[0]["ImageUrl"] = "~/image/Rules/" + fileName1;
                                    imgEdit1 = true;
                                }
                                catch
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                }
                            }
                            //}
                            //else
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "فرمت وارد شده برای عکس نامعتبر است";
                            //}
                        }
                        catch (Exception err)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }
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
                            string ImgTarget = Server.MapPath("~/image/Pdf/Rules/") + fileName;
                            System.IO.File.Move(ImgSoource, ImgTarget);

                        }

                    }
                    if (flpImg.HasFile)
                    {
                        if (imgEdit1 == true)
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + fileName1;
                            string ImgTarget = Server.MapPath("~/image/Rules/") + fileName1;
                            System.IO.File.Move(ImgSoource, ImgTarget);

                        }

                    }

                    RuleId.Value = Utility.EncryptQS(manager[0]["RuId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();
                    HpLinkImg.NavigateUrl = manager[0]["ImageUrl"].ToString();

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
        TSP.DataManager.RulesManager manager = new TSP.DataManager.RulesManager();

        try
        {
            if (flp.HasFile)
            {
                string extension = Path.GetExtension(flp.FileName);
                extension = extension.ToLower();
                string fileName = flp.FileName;

                //if (extension == ".pdf")
                //{
                //    string fileName = flp.FileName;
                //}
                //else
                //{

                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "فرمت وارد شده برای فایل نامعتبر است";
                //    return;
                //}
            }
            if (flpImg.HasFile)
            {
                string extension1 = Path.GetExtension(flpImg.FileName);
                extension1 = extension1.ToLower();
                string fileName1 = flpImg.FileName;

                //if (extension1 == ".gif" || extension1 == ".jpg" || extension1 == ".jpeg" || extension1 == ".png")
                //{
                //    string fileName1 = flpImg.FileName;
                //}
                //else
                //{

                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "فرمت وارد شده برای عکس نامعتبر است";
                //    return;
                //}
            }
            
            DataRow row = manager.NewRow();
            row["RuName"] = txtRuName.Text;
            row["Description"] = txtDesc.Text;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            if (cmbRulesType.SelectedItem != null && !Utility.IsDBNullOrNullValue(cmbRulesType.SelectedItem.Value))
            {
                row["RulesTypeId"] = cmbRulesType.SelectedItem.Value;
            }
            string path = null;
            string p = null;
            if (flp.HasFile)
            {
                path = Server.MapPath("~/image/Pdf/Rules/");
                p = Utility.GenerateName(Path.GetExtension(flp.FileName));

                row["PdfUrl"] = "~/image/Pdf/Rules/" + p;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "فایل را انتخاب نمایید";
                return;
            }
            string path1 = null;
            string p1 = null;
            if (flpImg.HasFile)
            {
                path1 = Server.MapPath("~/image/Rules/");
                p1 = Utility.GenerateName(Path.GetExtension(flpImg.FileName));

                row["ImageUrl"] = "~/image/Rules/" + p1;
            }
            manager.AddRow(row);

            int cn = manager.Save();
            if (cn == 1)
            {
                if (flp.HasFile)
                    flp.SaveAs(path + p);
                if (flpImg.HasFile)
                    flpImg.SaveAs(path1 + p1);

                RuleId.Value = Utility.EncryptQS(manager[0]["RuId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                HpLink.Visible = true;
                HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();
                HpLinkImg.Visible = true;
                HpLinkImg.NavigateUrl = manager[0]["ImageUrl"].ToString();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                TSP.DataManager.Permission per = TSP.DataManager.RulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
        }


    }

    protected void Delete(int RuId)
    {
        TSP.DataManager.RulesManager managerEdit = new TSP.DataManager.RulesManager();
        managerEdit.FindByCode(RuId);
        if (managerEdit.Count == 1)
        {
            try
            {
                string url = managerEdit[0]["PdfUrl"].ToString();
                string imgurl = managerEdit[0]["ImageUrl"].ToString();

                managerEdit[0].Delete();

                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    if ((!string.IsNullOrEmpty(url)) && (File.Exists(Server.MapPath(url))))
                        File.Delete(Server.MapPath(url));

                    if ((!string.IsNullOrEmpty(imgurl)) && (File.Exists(Server.MapPath(imgurl))))
                        File.Delete(Server.MapPath(imgurl));

                    RuleId.Value = Utility.EncryptQS("");
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
    #endregion

}


