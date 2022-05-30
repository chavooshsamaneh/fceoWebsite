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

public partial class Employee_TechniciansManagement_OtherPersonInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["OthPic"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["OtpId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("OtherPerson.aspx");
                return;
            }


            TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew1.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnInsert.Enabled = per.CanNew || per.CanEdit;
            btnInsert1.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                OtherPersonId.Value = Server.HtmlDecode(Request.QueryString["OtpId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OtpId = Utility.DecryptQS(OtherPersonId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":

                    if (string.IsNullOrEmpty(OtpId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    flpImg.Visible = false;
                    btnInsert.Enabled = false;
                    btnInsert1.Enabled = false;
                    FillForm(int.Parse(OtpId));
                    ASPxRoundPanel1.HeaderText = "مشاهده";
                    ASPxRoundPanel1.Enabled = false;


                    break;


                case "New":

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel1.HeaderText = "جدید";

                 
                    break;

                case "Edit":

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(OtpId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(OtpId));
                    ASPxRoundPanel1.HeaderText = "ویرایش";

                    break;


            }


            this.ViewState["BtnSave"] = btnInsert.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnInsert.Enabled = this.btnInsert1.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void btnNew1_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnInsert.Enabled = per.CanNew;
        btnInsert1.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnInsert.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        OtherPersonId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel1.HeaderText = "جدید";
        ASPxRoundPanel1.Enabled = true;
        ClearForm();
        flpImg.Visible = true;

    }
    protected void btnEdit2_Click(object sender, EventArgs e)
    {
        ASPxRoundPanel1.Enabled = true;

        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnInsert.Enabled = per.CanEdit;
        btnInsert1.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnInsert.Enabled;

        PgMode.Value = Utility.EncryptQS("Edit");

        flpImg.Visible = true;
        ASPxRoundPanel1.HeaderText = "ویرایش";
    }
    protected void btnInsert_Click(object sender, EventArgs e)
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OtherPerson.aspx");
    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtDesc.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtFatherName.Text = "";
        txtFirstName.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtMobileNo.Text = "";
        txtSSN.Text = "";
        txtTel.Text = "";
        Img1.ImageUrl = "";
        Img1.ImageUrl = "~/Images/Person.png";
    }
    protected void FillForm(int OtpId)
    {
        TSP.DataManager.OtherPersonManager Manager = new TSP.DataManager.OtherPersonManager();
        try
        {
            Manager.FindByCode(OtpId);
            if (Manager.Count == 1)
            {
                txtAddress.Text = Manager[0]["Address"].ToString();
                txtDesc.Text = Manager[0]["Description"].ToString();
                txtBirthDate.Text = Manager[0]["BirthDate"].ToString();
                txtBirthPlace.Text = Manager[0]["BirthPlace"].ToString();
                txtFatherName.Text = Manager[0]["FatherName"].ToString();
                txtFirstName.Text = Manager[0]["FirstName"].ToString();
                txtIdNo.Text = Manager[0]["IdNo"].ToString();
                txtLastName.Text = Manager[0]["LastName"].ToString();
                txtMobileNo.Text = Manager[0]["MobileNo"].ToString();
                txtSSN.Text = Manager[0]["SSN"].ToString();
                txtTel.Text = Manager[0]["Tel"].ToString();
                if (!Utility.IsDBNullOrNullValue(Manager[0]["ImageUrl"]))
                {
                    Img1.ImageUrl = Manager[0]["ImageUrl"].ToString();
                   
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امكان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطايى در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void Insert()
    {
        TSP.DataManager.OtherPersonManager manager = new TSP.DataManager.OtherPersonManager();
        try
        {
            DataRow dr = manager.NewRow();
            dr["OtpCode"] = DBNull.Value;
            dr["FirstName"] = txtFirstName.Text;
            dr["LastName"] = txtLastName.Text;
            dr["FatherName"] = txtFatherName.Text;
            dr["IdNo"] = txtIdNo.Text;
            dr["SSN"] = txtSSN.Text;
            dr["BirthPlace"] = txtBirthPlace.Text;
            dr["BirthDate"] = txtBirthDate.Text;
            dr["OtpType"] = (int)TSP.DataManager.OtherPersonType.OtherPerson;
            dr["FileNo"] = DBNull.Value;
            dr["Description"] = txtDesc.Text;
            dr["Address"] = txtAddress.Text;
            dr["Tel"] = txtTel.Text;
            dr["MobileNo"] = txtMobileNo.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

       
            if (Session["OthPic"] != null)
            {
                dr["ImageUrl"] = "~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString());
            }

            manager.AddRow(dr);
            int cnt = manager.Save();
            if (cnt > 0)
            {
                PgMode.Value = Utility.EncryptQS("Edit");
                OtherPersonId.Value = Utility.EncryptQS(manager[0]["OtpId"].ToString());

                TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnInsert.Enabled = per.CanEdit;
                btnInsert1.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnInsert.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
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
        try
        {
            if (Session["OthPic"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["OthPic"].ToString());
                string ImgTarget = Server.MapPath("~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString()));
                File.Copy(ImgSoource, ImgTarget, true);
                Img1.ImageUrl = "~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString());
                Session["OthPic"] = null;
            }
        }
        catch (Exception)
        { }

      
    }
    protected void Edit(int OtpId)
    {
        TSP.DataManager.OtherPersonManager Manager = new TSP.DataManager.OtherPersonManager();
        try
        {
            Manager.FindByCode(OtpId);
            if (Manager.Count > 0)
            {
                Manager[0].BeginEdit();

                Manager[0]["FirstName"] = txtFirstName.Text;
                Manager[0]["LastName"] = txtLastName.Text;
                Manager[0]["FatherName"] = txtFatherName.Text;
                Manager[0]["IdNo"] = txtIdNo.Text;
                Manager[0]["SSN"] = txtSSN.Text;
                Manager[0]["BirthPlace"] = txtBirthPlace.Text;
                Manager[0]["BirthDate"] = txtBirthDate.Text;
                Manager[0]["OtpType"] = (int)TSP.DataManager.OtherPersonType.OtherPerson;
                Manager[0]["FileNo"] = DBNull.Value;
                Manager[0]["Description"] = txtDesc.Text;
                Manager[0]["Address"] = txtAddress.Text;
                Manager[0]["Tel"] = txtTel.Text;
                Manager[0]["MobileNo"] = txtMobileNo.Text;
                Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                Manager[0]["ModifiedDate"] = DateTime.Now;


                if (Session["OthPic"] != null)
                {

                    if ((!string.IsNullOrEmpty(Manager[0]["ImageUrl"].ToString())) && (File.Exists(Server.MapPath(Manager[0]["ImageUrl"].ToString()))))
                    {
                        File.Delete(Server.MapPath(Manager[0]["ImageUrl"].ToString()));

                        Manager[0]["ImageUrl"] = "~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString());
                        Img1.ImageUrl = Server.MapPath("~/image/Temp") + Path.GetFileName(Session["OthPic"].ToString());
                        

                    }
                    else
                    {

                        Manager[0]["ImageUrl"] = "~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString());
                        Img1.ImageUrl = Server.MapPath("~/image/Temp") + Path.GetFileName(Session["OthPic"].ToString());
                        

                    }

                }
                Manager[0].EndEdit();
                Manager.Save() ;
                
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
               
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                return;
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
        try
        {
            if (Session["OthPic"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["OthPic"].ToString());
                string ImgTarget = Server.MapPath("~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString()));
                File.Copy(ImgSoource, ImgTarget, true);
                Img1.ImageUrl = "~/image/OtherPerson/Person/" + Path.GetFileName(Session["OthPic"].ToString());
                Session["OthPic"] = null;
            }
        }
        catch (Exception)
        { }
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
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/OtherPerson/Person/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.HorRes, Utility.VerRes);

            Session["OthPic"] = tempFileName2;

        }
        return ret;
    }
}
