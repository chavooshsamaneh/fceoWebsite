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
public partial class Employee_Amoozesh_AddOtherPersons : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PAId"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("AddPeriodAttenders.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDisActive.Enabled = false;
            btnDisActive2.Enabled = false;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            PgMode.Value = Utility.EncryptQS("New");          

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnDisActiveImg"] != null)
            this.btnDisActive.Image.Url = this.btnDisActive2.Image.Url = (string)this.ViewState["btnDisActiveImg"];
    }  

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        OtherPersonId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();

        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnDisActive.Enabled = false;
        btnDisActive2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string OtpId = Utility.DecryptQS(OtherPersonId.Value);

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

                //Response.Redirect("AddCourse.aspx?OtpId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(OtpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(OtpId));
                }

            }

        }



    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OtpId = Utility.DecryptQS(OtherPersonId.Value);

        if (string.IsNullOrEmpty(OtpId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "Edit" && (!string.IsNullOrEmpty(OtpId)))
            {
                Delete(int.Parse(OtpId));
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if ((!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))) && (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PAId"].ToString()))) && (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()))))
                Response.Redirect("AddPeriodAttenders.aspx?PAId=" + Server.HtmlDecode(Request.QueryString["PAId"].ToString()) + "&PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string OtpId = Utility.DecryptQS(OtherPersonId.Value);

        if (string.IsNullOrEmpty(OtpId))
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
                TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }            
        }

    }

    protected void chbIsMember_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chbInActive_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.ObserverManager ObserverManager = new TSP.DataManager.ObserverManager();
        string ObsId = Utility.DecryptQS(OtherPersonId.Value.ToString());
        ObserverManager.FindByCode(int.Parse(ObsId));
        if (Convert.ToBoolean(ObserverManager[0]["InActive"].ToString()))
        {
            Active(int.Parse(ObsId));
        }
        else
        {
            InActive(int.Parse(ObsId));
        }
    }
    #endregion 

    #region Methods
    protected void FillForm(int OtpId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);



        TSP.DataManager.OtherPersonManager manager = new TSP.DataManager.OtherPersonManager();
        manager.FindByCode(OtpId);
        if (manager.Count == 1)
        {
            txtName.Text = manager[0]["FirstName"].ToString();
            txtFamily.Text = manager[0]["LastName"].ToString();
            txtFatherName.Text = manager[0]["FatherName"].ToString();
            txtBrithDate.Text = manager[0]["BirthDate"].ToString();
            txtBirthPlace.Text = manager[0]["BirthPlace"].ToString();
            txtIdNo.Text = manager[0]["IdNo"].ToString();
            txtSSN.Text = manager[0]["SSN"].ToString();
            txtTel.Text = manager[0]["Tel"].ToString();
            txtMobileNo.Text = manager[0]["MobileNo"].ToString();
            txtAddress.Text = manager[0]["Address"].ToString();
            txtEmail.Text = manager[0]["Email"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();
            Image.ImageUrl = manager[0]["ImageUrl"].ToString();          
        }
    }

    protected void ClearForm()
    {
        Image.ImageUrl = "";
        txtBrithDate.Text = "";
        txtDesc.Text = "";
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {

            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }

        }


    }

    protected void Disable()
    {


        txtName.Enabled = false;
        txtFamily.Enabled = false;
        txtFatherName.Enabled = false;
        txtBrithDate.Enabled = false;
        txtBirthPlace.Enabled = false;
        txtIdNo.Enabled = false;
        txtSSN.Enabled = false;
        txtTel.Enabled = false;
        txtMobileNo.Enabled = false;
        txtAddress.Enabled = false;
        txtEmail.Enabled = false;
        txtDesc.Enabled = false;

    }

    protected void Enable()
    {
        txtName.Enabled = true;
        txtFamily.Enabled = true;
        txtFatherName.Enabled = true;
        txtBrithDate.Enabled = true;
        txtBirthPlace.Enabled = true;
        txtIdNo.Enabled = true;
        txtSSN.Enabled = true;
        txtTel.Enabled = true;
        txtMobileNo.Enabled = true;
        txtAddress.Enabled = true;
        txtEmail.Enabled = true;
        txtDesc.Enabled = true;
    }

    protected void Edit(int OtpId)
    {
        string fileNameSign = "", pathAx = "", pathEm = "", extension = "", fileNameAx = "";
        byte[] img = null;
        byte[] imgAx = null;

        TSP.DataManager.OtherPersonManager manager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        tr.Add(manager);

        manager.FindByCode(OtpId);
        if (manager.Count == 1)
        {

            try
            {

                manager[0].BeginEdit();
                manager[0]["FirstName"] = txtName.Text;
                manager[0]["LastName"] = txtFamily.Text;
                manager[0]["FatherName"] = txtFatherName.Text;
                manager[0]["BirthDate"] = txtBrithDate.Text;
                manager[0]["BirthPlace"] = txtBirthPlace.Text;
                manager[0]["IdNo"] = txtIdNo.Text;
                manager[0]["SSN"] = txtSSN.Text;
                manager[0]["Tel"] = txtTel.Text;
                manager[0]["MobileNo"] = txtMobileNo.Text;
                manager[0]["Address"] = txtAddress.Text;
                //manager[0]["Email"] = txtEmail.Text;
                manager[0]["Description"] = txtDesc.Text;
                manager[0]["OtpType"] = 2;
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                #region editKaAxImage
                bool chImgAxEdit = false;
                DevExpress.Web.ASPxUploadControl flpGvKaAx = new DevExpress.Web.ASPxUploadControl();
                flpGvKaAx = ((DevExpress.Web.ASPxUploadControl)flpImage);
                if (flpGvKaAx.HasFile)
                {
                    if ((!string.IsNullOrEmpty(manager[0]["Image"].ToString())) && (!string.IsNullOrEmpty(manager[0]["ImageUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(manager[0]["ImageUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(manager[0]["ImageUrl"].ToString()));
                            extension = Path.GetExtension(flpGvKaAx.FileName);
                            extension = extension.ToLower();
                            if (extension == ".jpg" || extension == ".gif")
                            {
                                if (flpGvKaAx.HasFile)
                                {
                                    try
                                    {
                                        imgAx = flpGvKaAx.FileBytes;
                                        fileNameAx = Utility.GenerateName(Path.GetExtension(flpGvKaAx.FileName));
                                        pathAx = Server.MapPath("~/image/Temp/");
                                        flpGvKaAx.SaveAs(pathAx + fileNameAx);
                                        manager[0]["Image"] = imgAx;
                                        manager[0]["ImageUrl"] = "~/Image/OtherPerson/Person/" + fileNameAx;
                                        chImgAxEdit = true;
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
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }

                    }
                    else
                    {
                        try
                        {
                            extension = Path.GetExtension(flpGvKaAx.FileName);
                            extension = extension.ToLower();
                            if (extension == ".jpg" || extension == ".gif")
                            {
                                if (flpGvKaAx.HasFile)
                                {
                                    try
                                    {
                                        imgAx = flpGvKaAx.FileBytes;
                                        fileNameAx = Utility.GenerateName(Path.GetExtension(flpGvKaAx.FileName));
                                        pathAx = Server.MapPath("~/image/Temp/");
                                        flpGvKaAx.SaveAs(pathAx + fileNameAx);
                                        manager[0]["Image"] = imgAx;
                                        manager[0]["ImageUrl"] = "~/Image/OtherPerson/Person/" + fileNameAx;
                                        chImgAxEdit = true;
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
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }
                    }
                }
                #endregion

                tr.BeginSave();
                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {
                    if (flpGvKaAx.HasFile)
                    {
                        if (chImgAxEdit == true)
                        {
                            string ImgSoource = Server.MapPath("~/image/Temp/") + fileNameAx;
                            string ImgTarget = Server.MapPath("~/Image/OtherPerson/Person/") + fileNameAx;
                            System.IO.File.Move(ImgSoource, ImgTarget);
                        }
                    }


                    OtherPersonId.Value = Utility.EncryptQS(manager[0]["OtpId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    tr.EndSave();
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    btnDisActive.Enabled = true; ;
                    btnDisActive2.Enabled =true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
                }
                else
                {
                    tr.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
                }
            }

            catch (Exception err)
            {
                tr.CancelSave();

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


    }

    protected void Insert()
    {
        string fileNameImg = "", fileNameSign = "", pathAx = "", extension = "";
        byte[] img = null;
        TSP.DataManager.OtherPersonManager manager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        tr.Add(manager);
        try
        {
            DataRow row = manager.NewRow();
            row["FirstName"] = txtName.Text;
            row["LastName"] = txtFamily.Text;
            row["FatherName"] = txtFatherName.Text;
            row["BirthDate"] = txtBrithDate.Text;
            row["BirthPlace"] = txtBirthPlace.Text;
            row["IdNo"] = txtIdNo.Text;
            row["SSN"] = txtSSN.Text;
            row["Tel"] = txtTel.Text;
            row["MobileNo"] = txtMobileNo.Text;
            row["Address"] = txtAddress.Text;
            //row["Email"] = txtEmail.Text;
            row["Description"] = txtDesc.Text;
            row["OtpType"] = 2;
            row["UserId"] =Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            bool chImg = false;
            if (flpImage.HasFile)
            {
                extension = Path.GetExtension(flpImage.FileName);
                extension = extension.ToLower();


                //if (extension == ".jpg" || extension == ".gif")
                //{

                    img = flpImage.FileBytes;
                    fileNameImg = Utility.GenerateName(Path.GetExtension(flpImage.FileName));
                    pathAx = Server.MapPath("~/image/Temp/");
                    flpImage.SaveAs(pathAx + fileNameImg);
                    row["Image"] = img;
                    row["ImageUrl"] = "~/Image/OtherPerson/Person/" + fileNameImg;
                    chImg = true;

                //}
                //else
                //{
                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "فرمت وارد شده برای فایل نامعتبر است";
                //}
            }
            manager.AddRow(row);
            tr.BeginSave();
            int cn = manager.Save();
            if (cn == 1)
            {
                //CrId = int.Parse(manager[0]["OtpId"].ToString());
                if (chImg == true)
                {
                    string EmzaSoource = Server.MapPath("~/image/Temp/") + fileNameImg;
                    string EmzaTarget = Server.MapPath("~/image/OtherPerson/Person/") + fileNameImg;
                    System.IO.File.Move(EmzaSoource, EmzaTarget);
                }
                OtherPersonId.Value = Utility.EncryptQS(manager[0]["OtpId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                tr.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnDisActive.Enabled = true;
                btnDisActive2.Enabled = true;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
            }
            else
            {
                tr.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام نشد";
            }
        }
        catch (Exception err)
        {
            tr.CancelSave();
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

    protected void Delete(int OtpId)
    {

        TSP.DataManager.OtherPersonManager managerEdit = new TSP.DataManager.OtherPersonManager();
        managerEdit.FindByCode(OtpId);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //TeacherId.Value = managerEdit[0]["OtpId"].ToString();
                    OtherPersonId.Value = Utility.EncryptQS("");
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

    protected void Active(int OtpId)
    {

        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        OtherPersonManager.FindByCode(OtpId);
        if (OtherPersonManager.Count == 1)
        {

            try
            {

                OtherPersonManager[0].BeginEdit();
                OtherPersonManager[0]["InActive"] = 0;
                OtherPersonManager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
                OtherPersonManager[0].EndEdit();
                int cn = OtherPersonManager.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(false);
                    OtherPersonId.Value = Utility.EncryptQS(OtherPersonManager[0]["OtpId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }

    protected void InActive(int ObId)
    {

        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        OtherPersonManager.FindByCode(ObId);
        if (OtherPersonManager.Count == 1)
        {

            try
            {

                OtherPersonManager[0].BeginEdit();
                OtherPersonManager[0]["InActive"] = 1;
                OtherPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
                OtherPersonManager[0].EndEdit();
                int cn = OtherPersonManager.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(true);
                    OtherPersonId.Value = Utility.EncryptQS(OtherPersonManager[0]["OtpId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }

    private void ChangeDisableButtonIcon(Boolean InActive)
    {
        if (InActive)
        {
            btnDisActive.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive.ToolTip = "فعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive2.ToolTip = "فعال کردن";
        }
        else
        {
            btnDisActive.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive.ToolTip = "غیرفعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive2.ToolTip = "غیرفعال کردن";
        }
        this.ViewState["btnDisActiveImg"] = this.btnDisActive.Image.Url;
    }

    #endregion
}
