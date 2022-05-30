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

public partial class Employee_Management_MunEmployeeInsert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblImg.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";
     
        if ( (string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["EmpId"])))
        {
            Response.Redirect("MunEmployee.aspx");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            Session["EmpImg"] = null;
            Session["EmpSign"] = null;


            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            try
            {
                HDEmpId.Value = Server.HtmlDecode(Request.QueryString["EmpId"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EmpId = Utility.DecryptQS(HDEmpId.Value);
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(EmpId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(EmpId));
                    RoundPanelEmlpoyee.HeaderText = "مشاهده";


                    break;


                case "New":
                 
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    RoundPanelEmlpoyee.HeaderText = "جدید";

                    ClearForm();
                    break;

                case "Edit":
                    
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(EmpId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(EmpId));
                    RoundPanelEmlpoyee.HeaderText = "ویرایش";
                    break;


            }



            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }

        if (Session["EmpImg"] != null)
        {
            imgPic.ClientVisible = true;
            imgPic.ImageUrl = "~/image/Temp/" + Session["EmpImg"].ToString();
        }
        if (Session["EmpSign"] != null)
        {
            HpSign.ClientVisible = true;
            HpSign.NavigateUrl = "~/image/Temp/" + Session["EmpSign"].ToString();
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void Enable()
    {
        txtAddress.Enabled = true;
        txtBirthDate.Enabled = true;
        txtBirthPlace.Enabled = true;
        txtDescription.Enabled = true;
        txtEmail.Enabled = true;
        txtFatherName.Enabled = true;
        txtFirstName.Enabled = true;
        txtFirstNameEn.Enabled = true;
        txtIdNo.Enabled = true;
        txtLastName.Enabled = true;
        txtLastNameEn.Enabled = true;
        txtMobileNo.Enabled = true;
        txtSSN.Enabled = true;
        txtTel.Enabled = true;
        txtWebSite.Enabled = true;
        cmbMarId.Enabled = true;
        cmbMun.Enabled = true;
        cmbSexId.Enabled = true;
        flpImg.ClientVisible = true;
        flpSign.ClientVisible = true;
        lblImg.Visible = true;

    }
    protected void Disable()
    {
        //RoundPanelEmlpoyee.Enabled = false;
        txtAddress.Enabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.Enabled = false;
        txtDescription.Enabled = false;
        txtEmail.Enabled = false;
        txtFatherName.Enabled = false;
        txtFirstName.Enabled = false;
        txtFirstNameEn.Enabled = false;
        txtIdNo.Enabled = false;
        txtLastName.Enabled = false;
        txtLastNameEn.Enabled = false;
        txtMobileNo.Enabled = false;
        txtSSN.Enabled = false;
        txtTel.Enabled = false;
        txtWebSite.Enabled = false;
        cmbMarId.Enabled = false;
        cmbMun.Enabled = false;
        cmbSexId.Enabled = false;
        flpImg.ClientVisible = false;
        flpSign.ClientVisible = false;
        lblImg.Visible = false;
    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDescription.Text = "";
        txtEmail.Text = "";
        txtFatherName.Text = "";
        txtFirstName.Text = "";
        txtFirstNameEn.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtLastNameEn.Text = "";
        txtMobileNo.Text = "";
        txtSSN.Text = "";
        txtTel.Text = "";
        txtWebSite.Text = "";
        cmbMarId.DataBind();
        cmbMarId.SelectedIndex = -1;
        cmbMun.DataBind();
        cmbMun.SelectedIndex = -1;
        cmbSexId.DataBind();
        cmbSexId.SelectedIndex = -1;
        imgPic.ImageUrl = "";
        HpSign.NavigateUrl = "";
        HpSign.ClientVisible = false;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
      
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        Enable();
        PgMode.Value = Utility.EncryptQS("Edit");
        RoundPanelEmlpoyee.HeaderText = "ویرایش";
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        PgMode.Value = Utility.EncryptQS("New");
        HDEmpId.Value = Utility.EncryptQS("");
        RoundPanelEmlpoyee.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cmbMun.Value == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شهرداری را انتخاب نمایید";
            return;
        }
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string EmpId = Utility.DecryptQS(HDEmpId.Value);

        switch (PageMode)
        {
            case "New":
                InsertEmployee();

                break;
            case "Edit":
                if (string.IsNullOrEmpty(EmpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    EditEmloyee(int.Parse(EmpId));
                }
                break;
         
        }
    }
    private void FillForm(int EmpId)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        EmpManager.FindMunEmpByEmpId(EmpId);
        if (EmpManager.Count == 1)
        {
            cmbMun.DataBind();
            cmbMun.SelectedIndex = cmbMun.Items.IndexOfValue(EmpManager[0]["MunId"].ToString());
            
            txtAddress.Text = EmpManager[0]["Address"].ToString();
            txtBirthDate.Text = EmpManager[0]["BirthDate"].ToString();
            txtBirthPlace.Text = EmpManager[0]["BirthPlace"].ToString();
            txtDescription.Text = EmpManager[0]["Description"].ToString();
            txtEmail.Text = EmpManager[0]["Email"].ToString();
            txtFatherName.Text = EmpManager[0]["FatherName"].ToString();
            txtFirstName.Text = EmpManager[0]["FirstName"].ToString();
            txtFirstNameEn.Text = EmpManager[0]["FirstNameEn"].ToString();
            txtIdNo.Text = EmpManager[0]["IdNo"].ToString();
            txtLastName.Text = EmpManager[0]["LastName"].ToString();
            txtLastNameEn.Text = EmpManager[0]["LastNameEn"].ToString();
            txtMobileNo.Text = EmpManager[0]["MobileNo"].ToString();
            txtSSN.Text = EmpManager[0]["SSN"].ToString();
            txtTel.Text = EmpManager[0]["Tel"].ToString();
            txtWebSite.Text = EmpManager[0]["WebSite"].ToString();

            if (!Utility.IsDBNullOrNullValue(EmpManager[0]["ImgUrl"]))
            {
                imgPic.ImageUrl = EmpManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();

            }
            else
                imgPic.ImageUrl = "~/images/person.gif";

            if (!Utility.IsDBNullOrNullValue(EmpManager[0]["SignUrl"]))
            {
                HpSign.ClientVisible = true;
                HpSign.NavigateUrl = EmpManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();

            }

            cmbMarId.DataBind();
            cmbSexId.DataBind();

            cmbMarId.SelectedIndex = cmbMarId.Items.IndexOfValue(EmpManager[0]["MarId"].ToString());
            cmbSexId.SelectedIndex = cmbSexId.Items.IndexOfValue(EmpManager[0]["SexId"].ToString());
          
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
        }
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

            } while (File.Exists(MapPath("~/image/MunEmployee/Pic/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Session["EmpImg"] = ret;
            Utility.FixedSize(tempFileName, tempFileName2, Utility.HorRes, Utility.VerRes);

        }
        return ret;
    }
    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveSign(UploadedFile uploadedFile)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
               
            } while (File.Exists(MapPath("~/image/MunEmployee/Sign/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;

            uploadedFile.SaveAs(tempFileName, true);
            Session["EmpSign"] = ret;

        }
        return ret;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["EmpImg"] = null;
        Response.Redirect("MunEmployee.aspx");
    }
    private void InsertEmployee()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
       
        trans.Add(EmpManager);
        trans.Add(LogManager);

        try
        {
          
            DataRow dr = EmpManager.NewRow();
            dr["Address"] = txtAddress.Text;
            dr["BirthDate"] = txtBirthDate.Text;
            dr["BirthPlace"] = txtBirthPlace.Text;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["Description"] = txtDescription.Text;
            dr["Email"] = txtEmail.Text;
            dr["EmpCode"] = Utility.GenRandomNum();
            dr["FatherName"] = txtFatherName.Text;
            dr["FirstName"] = txtFirstName.Text;
            dr["FirstNameEn"] = txtFirstNameEn.Text;
            dr["IdNo"] = txtIdNo.Text;
            dr["LastName"] = txtLastName.Text;
            dr["LastNameEn"] = txtLastNameEn.Text;
            dr["MobileNo"] = txtMobileNo.Text;
            dr["Nationality"] = DBNull.Value;
            dr["SSN"] = txtSSN.Text;
            dr["Tel"] = txtTel.Text;
            dr["WebSite"] = txtWebSite.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            if (cmbSexId.Value != null)
                dr["SexId"] = cmbSexId.Value;
            else
                dr["SexId"] = DBNull.Value;
            if (cmbMarId.Value != null)
                dr["MarId"] = cmbMarId.Value;
            else
                dr["MarId"] = DBNull.Value;
            dr["PartId"] = DBNull.Value;

            dr["RelId"] = DBNull.Value;
            dr["MunId"] = cmbMun.Value;
            dr["EmpStatus"] = 0;
            dr["AgentId"] = Utility.GetMainAgentId();
            EmpManager.AddRow(dr);
            trans.BeginSave();
            int cnt = EmpManager.Save();

            if (cnt > 0)
            {
                string EmpId = EmpManager[0]["EmpId"].ToString();
                EmpManager.DataTable.AcceptChanges();
                EmpManager[0].BeginEdit();
                if (Session["EmpImg"] != null)
                    EmpManager[0]["ImgUrl"] = "~/image/MunEmployee/Pic/" + EmpId + Path.GetExtension(Session["EmpImg"].ToString());
                if (Session["EmpSign"] != null)
                    EmpManager[0]["SignUrl"] = "~/image/MunEmployee/Sign/" + EmpId + Path.GetExtension(Session["EmpSign"].ToString());
               
                EmpManager[0]["EmpCode"] = EmpManager[0]["EmpId"];
                EmpManager[0].EndEdit();
                EmpManager.Save();

                DataRow logRow = LogManager.NewRow();
                logRow.BeginEdit();
                logRow["UserName"] = "emp" + EmpId;
                if (!string.IsNullOrEmpty(EmpManager[0]["IdNo"].ToString()))
                    logRow["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(EmpManager[0]["IdNo"].ToString(), "sha1");
                logRow["UltId"] = (int)TSP.DataManager.UserType.Municipality;
                logRow["MeId"] = EmpId;
                logRow["Email"] = EmpManager[0]["Email"].ToString();
                logRow["UserId2"] = Utility.GetCurrentUser_UserId();
                logRow["IsValid"] = 1;
                logRow["ModifiedDate"] = DateTime.Now;
                logRow.EndEdit();
                LogManager.AddRow(logRow);
                LogManager.Save();

                trans.EndSave();

                HDEmpId.Value = Utility.EncryptQS(EmpId);
                PgMode.Value = Utility.EncryptQS("Edit");
                RoundPanelEmlpoyee.HeaderText = "ویرایش";

                imgPic.ImageUrl = EmpManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
                HpSign.NavigateUrl = EmpManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
              
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "  ذخیره با نام کاربری " + "emp" + EmpId + "و رمز عبور  " + EmpManager[0]["IdNo"].ToString() + " انجام شد";

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
                if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کدملی تکراری می باشد";
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
            if (Session["EmpImg"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Session["EmpImg"].ToString();
                string ImgTarget = Server.MapPath("~/image/MunEmployee/Pic/") + EmpManager[0]["EmpId"].ToString() + Path.GetExtension(Session["EmpImg"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                //File.Delete(ImgSoource);
                Session["EmpImg"] = null;

            }
            if (Session["EmpSign"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Session["EmpSign"].ToString();
                string ImgTarget = Server.MapPath("~/image/MunEmployee/Sign/") + EmpManager[0]["EmpId"].ToString() + Path.GetExtension(Session["EmpSign"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                //File.Delete(ImgSoource);
                Session["EmpSign"] = null;

            }
        }
        catch (Exception)
        { }
    }
    private void EditEmloyee(int EmpId)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();

        try
        {
            EmpManager.FindMunEmpByEmpId(EmpId);
            if (EmpManager.Count == 1)
            {
                EmpManager[0].BeginEdit();
                EmpManager[0]["Address"] = txtAddress.Text;
                EmpManager[0]["BirthDate"] = txtBirthDate.Text;
                EmpManager[0]["BirthPlace"] = txtBirthPlace.Text;
                EmpManager[0]["Description"] = txtDescription.Text;
                EmpManager[0]["Email"] = txtEmail.Text;
                EmpManager[0]["FatherName"] = txtFatherName.Text;
                EmpManager[0]["FirstName"] = txtFirstName.Text;
                EmpManager[0]["FirstNameEn"] = txtFirstNameEn.Text;
                EmpManager[0]["IdNo"] = txtIdNo.Text;
                EmpManager[0]["LastName"] = txtLastName.Text;
                EmpManager[0]["LastNameEn"] = txtLastNameEn.Text;
                EmpManager[0]["MobileNo"] = txtMobileNo.Text;
                EmpManager[0]["SSN"] = txtSSN.Text;
                EmpManager[0]["Tel"] = txtTel.Text;
                EmpManager[0]["WebSite"] = txtWebSite.Text;

                if (cmbSexId.Value != null)
                    EmpManager[0]["SexId"] = cmbSexId.Value;
             
                if (cmbMarId.Value != null)
                    EmpManager[0]["MarId"] = cmbMarId.Value;
              
                EmpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                EmpManager[0]["ModifiedDate"] = DateTime.Now;
             
                if (Session["EmpImg"] != null)
                {
                    if ((!string.IsNullOrEmpty(EmpManager[0]["ImgUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(EmpManager[0]["ImgUrl"].ToString()))))
                    {

                        System.IO.File.Delete(Server.MapPath(EmpManager[0]["ImgUrl"].ToString()));
                    }

                    EmpManager[0]["ImgUrl"] = "~/image/MunEmployee/Pic/" + EmpId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());

                }
                if (Session["EmpSign"] != null)
                {
                    if ((!string.IsNullOrEmpty(EmpManager[0]["SignUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(EmpManager[0]["SignUrl"].ToString()))))
                    {

                        System.IO.File.Delete(Server.MapPath(EmpManager[0]["SignUrl"].ToString()));
                    }

                    EmpManager[0]["SignUrl"] = "~/image/MunEmployee/Sign/" + EmpId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());

                }
                imgPic.ImageUrl = EmpManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
                HpSign.NavigateUrl = EmpManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();


                EmpManager[0].EndEdit();
                EmpManager.Save();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
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
            if (Session["EmpImg"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Session["EmpImg"].ToString();
                string ImgTarget = Server.MapPath("~/image/MunEmployee/Pic/") + EmpId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
               // File.Delete(ImgSoource);
                //Session["EmpImg"] = null;


            }
            if (Session["EmpSign"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Session["EmpSign"].ToString();
                string ImgTarget = Server.MapPath("~/image/MunEmployee/Sign/") + EmpId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                //File.Delete(ImgSoource);
                // Session["EmpSign"] = null;

            }
        }
        catch (Exception)
        { }

    }
   
}
