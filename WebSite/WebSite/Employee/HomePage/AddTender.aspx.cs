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

public partial class Employee_HomePage_AddTender : System.Web.UI.Page
{
    string PageMode;
    string TeId;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["TeId"])))
            {
                Response.Redirect("Tender.aspx");
                return;
            }

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            this.ViewState["BtnNew"] = btnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        switch (PageMode)
        {
            case "New":
                InsertTender();
                break;
            case "Edit":
                UpdateTender();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tender.aspx");
    }

    private void InsertTender()
    {
        TSP.DataManager.TenderManager TenderManager = new TSP.DataManager.TenderManager();

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

            DataRow rowTender = TenderManager.NewRow();
            rowTender.BeginEdit();

            rowTender["TeName"] = txtTenderName.Text;
            rowTender["Date"] = Date.Text;
            rowTender["UserId"] = Utility.GetCurrentUser_UserId();
            rowTender["ModifiedDate"] = DateTime.Now;

            string path = null;
            string p = null;

            if (flp.HasFile)
            {
                path = Server.MapPath("~/image/Pdf/Tender/");
                p = Utility.GenerateName(Path.GetExtension(flp.FileName));

                rowTender["PdfUrl"] = "~/image/Pdf/Tender/" + p;
            }

            rowTender.EndEdit();

            TenderManager.AddRow(rowTender);

            int cn = TenderManager.Save();
            if (cn == 1)
            {
                if (flp.HasFile)
                    flp.SaveAs(path + p);

                TenderManager.DataTable.AcceptChanges();
                TeId = TenderManager[0]["TeId"].ToString();
                PkTeId.Value = Utility.EncryptQS(TeId.ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                HpLink.Visible = true;
                HpLink.NavigateUrl = TenderManager[0]["PdfUrl"].ToString();
                SetEditModeKeys();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            SetError(err, 'I');
        }
    }

    private void UpdateTender()
    {
        TeId = Utility.DecryptQS(PkTeId.Value);
        string fileName = "", pathAx = "", extension = "";

        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TenderManager TenderManager = new TSP.DataManager.TenderManager();
        TenderManager.FindByCode(Convert.ToInt32(TeId));


        if (TenderManager.Count >= 1)
        {
            try
            {
                TenderManager[0].BeginEdit();

                TenderManager[0]["TeName"] = txtTenderName.Text;
                TenderManager[0]["Date"] = Date.Text;
                TenderManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TenderManager[0]["ModifiedDate"] = DateTime.Now;

                bool imgEdit = false;
                if (flp.HasFile)
                {
                    if ((!string.IsNullOrEmpty(TenderManager[0]["PdfUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(TenderManager[0]["PdfUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(TenderManager[0]["PdfUrl"].ToString()));
                            extension = Path.GetExtension(flp.FileName);
                            extension = extension.ToLower();
                            if (flp.HasFile)
                            {
                                try
                                {
                                    fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                    pathAx = Server.MapPath("~/image/Temp/");
                                    flp.SaveAs(pathAx + fileName);

                                    TenderManager[0]["PdfUrl"] = "~/image/Pdf/Tender/" + fileName;
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
                            if (extension == ".pdf")
                            {
                                if (flp.HasFile)
                                {
                                    try
                                    {
                                        fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                                        pathAx = Server.MapPath("~/image/Temp/");
                                        flp.SaveAs(pathAx + fileName);
                                        TenderManager[0]["PdfUrl"] = "~/image/Pdf/Tender/" + fileName;
                                        imgEdit = true;
                                    }
                                    catch
                                    {
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = "امکان ذخیره فایل نمی باشد.";
                                    }
                                }
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "فرمت وارد شده برای فایل نامعتبر است";
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
                    if ((string.IsNullOrEmpty(TenderManager[0]["PdfUrl"].ToString())))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                        return;
                    }
                }

                TenderManager[0].EndEdit();

                int cn = TenderManager.Save();
                if (cn == 1)
                {
                    if (flp.HasFile)
                    {
                        if (imgEdit == true)
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + fileName;
                            string ImgTarget = Server.MapPath("~/image/Pdf/Tender/") + fileName;
                            System.IO.File.Move(ImgSoource, ImgTarget);

                        }

                    }

                    TenderManager.DataTable.AcceptChanges();
                    TeId = TenderManager[0]["TeId"].ToString();
                    PkTeId.Value = Utility.EncryptQS(TeId.ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    HpLink.NavigateUrl = TenderManager[0]["PdfUrl"].ToString();
                    SetEditModeKeys();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }

            catch (Exception err)
            {
                SetError(err, 'U');
            }
        }
    }

    private void DeleteTender()
    {
        TeId = Utility.DecryptQS(PkTeId.Value);
        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TenderManager TenderManager = new TSP.DataManager.TenderManager();
        TenderManager.FindByCode(Convert.ToInt32(TeId));

        if (TenderManager.Count == 1)
        {
            try
            {
                string url = TenderManager[0]["PdfUrl"].ToString();

                TenderManager[0].Delete();
                int cn = TenderManager.Save();
                if (cn == 1)
                {
                    if ((!string.IsNullOrEmpty(url)) && (File.Exists(Server.MapPath(url))))
                        File.Delete(Server.MapPath(url));

                    TenderManager.DataTable.AcceptChanges();
                    PkTeId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    SetNewModeKeys();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            catch (Exception err)
            {
                SetError(err, 'D');
            }
        }
    }

    /*************************************************************************************************************/
    private void SetError(Exception err, char Flag)
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
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وابسته معتبر نمی باشد";
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkTeId.Value = Server.HtmlDecode(Request.QueryString["TeId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        PageMode = Utility.DecryptQS(PgMode.Value);
        TeId = Utility.DecryptQS(PkTeId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        SetMode();
    }

    private void SetMode()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

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
        }
    }

    private void SetNewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        HpLink.Visible = false;
        txtTenderName.Enabled = true;
        Date.Enabled = true;
        flp.Enabled = true;

        txtTenderName.Text = "";
        Date.DateValue = DateTime.Now;

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        TeId = Utility.DecryptQS(PkTeId.Value);

        if ((string.IsNullOrEmpty(TeId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        HpLink.Visible = true;
        txtTenderName.Enabled = true;
        Date.Enabled = true;
        flp.Enabled = true;

        FillForm(Convert.ToInt32(TeId));

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        TeId = Utility.DecryptQS(PkTeId.Value);

        if ((string.IsNullOrEmpty(TeId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        HpLink.Visible = true;
        txtTenderName.Enabled = false;
        Date.Enabled = false;
        flp.Enabled = false;

        FillForm(Convert.ToInt32(TeId));

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    protected void FillForm(int TenderId)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.TenderManager manager = new TSP.DataManager.TenderManager();
        manager.FindByCode(TenderId);
        if (manager.Count == 1)
        {
            txtTenderName.Text = manager[0]["TeName"].ToString();
            Date.Text = manager[0]["Date"].ToString();
            HpLink.NavigateUrl = manager[0]["PdfUrl"].ToString();

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین رکوردی وجود ندارد";
        }
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }


    }

}
