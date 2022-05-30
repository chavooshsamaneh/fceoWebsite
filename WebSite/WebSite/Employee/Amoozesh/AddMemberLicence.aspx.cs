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

public partial class Employee_Amoozesh_AddMemberLicence : System.Web.UI.Page
{
    // int MdId = -1;
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
            #region  !IsPostback
            Session["MdFile"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["MdId"]))
            {
                Response.Redirect("MemberLicence.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.MadrakManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                MadrakId.Value = Server.HtmlDecode(Request.QueryString["MdId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string MdId = Utility.DecryptQS(MadrakId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetControlVisible();

            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(MdId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(MdId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    //if (!this.Page.IsStartupScriptRegistered("Key"))
                    //    this.Page.RegisterStartupScript("Key", "<script>SetPeriod();</script>");


                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;
                case "Edit":
                    Enable();
                    txtMeNo.Enabled = false;
                    ComboType.ClientEnabled = false;

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(MdId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(MdId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";


                    break;

            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            #endregion
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "MemberLicence.aspx";
        ASPxButton6.PostBackUrl = "MemberLicence.aspx";
        SetControlVisible();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLicence.aspx");

    }

    protected void flpImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Amoozesh/Madrak/") + ret) == true);
            string tempFileName = "~/image/Amoozesh/Madrak/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Session["MdFile"] = tempFileName;

        }
        return ret;
    }

    protected void CallbackPanelLicence_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        switch (e.Parameter)
        {
            case "Search":
                SearchMe();
                break;
            case "Save":
                Save();
                break;
            case "New":
                New();
                break;
            case "Edit":
                Edit();
                break;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Edit();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        New();
    }
    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        SearchMe();
        SetControlVisible();
    }
    #endregion

    #region Methods

    protected void FillForm(int MdId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.MadrakManager manager = new TSP.DataManager.MadrakManager();
        manager.FindByCode(MdId);
        if (manager.Count == 1)
        {
            txtLiNo.Text = manager[0]["LicenceNo"].ToString();
            txtLiDate.Text = manager[0]["LicenceDate"].ToString();
            txtMeFirstName.Text = manager[0]["FirstName"].ToString();
            txtMeLastName.Text = manager[0]["LastName"].ToString();
            txtMeNo.Text = manager[0]["MeId"].ToString();
            HpFile.ClientVisible = true;
            HpFile.NavigateUrl = manager[0]["FilePath"].ToString();

            ComboType.DataBind();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(manager[0]["Type"].ToString());
            ComboMdType.DataBind();
            ComboMdType.SelectedIndex = ComboMdType.Items.IndexOfValue(manager[0]["MdType"].ToString());
            ComboPrId.DataBind();
            ComboPrId.SelectedIndex = ComboPrId.Items.IndexOfValue(manager[0]["ProvinceId"].ToString());

            if (ComboType.Value.ToString() == "0")//period
            {
                if (!this.Page.IsStartupScriptRegistered("Key"))
                    this.Page.RegisterStartupScript("Key", "<script>SetPeriod();</script>");


                ComboCrsName.DataBind();
                ComboCrsName.SelectedIndex = ComboCrsName.Items.IndexOfValue(manager[0]["CrsId"]);

                txtPPDuration.Text = manager[0]["Duration"].ToString();
                txtPPTeName.Text = manager[0]["TeName"].ToString();
                txtTeFileNo.Text = manager[0]["TeFileNo"].ToString();
                txtInsRegNo.Text = manager[0]["InsRegNo"].ToString();
                txtInsRegDate.Text = manager[0]["InsRegDate"].ToString();
                txtTestDate.Text = manager[0]["TestDate"].ToString();
                txtPPCode.Text = manager[0]["PPCode"].ToString();
                txtTimeMark.Text = manager[0]["TimeMark"].ToString();
                txtTotalMark.Text = manager[0]["TotalMark"].ToString();
            }
            else
            {
                if (!this.Page.IsStartupScriptRegistered("Key1"))
                    this.Page.RegisterStartupScript("Key1", "<script>SetSeminar();</script>");

                txtSeName.Text = manager[0]["SeName"].ToString();
                txtSeDuration.Text = manager[0]["Duration"].ToString();
                txtSeTeName.Text = manager[0]["TeName"].ToString();

            }
            txtStartDate.Text = manager[0]["StartDate"].ToString();
            txtEndDate.Text = manager[0]["EndDate"].ToString();
            txtInsName.Text = manager[0]["InsName"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();

        }
    }
    protected void ClearForm()
    {
        ComboCrsName.DataBind();
        ComboCrsName.SelectedIndex = -1;
        ComboMdType.DataBind();
        ComboMdType.SelectedIndex = -1;
        ComboPrId.DataBind();
        ComboPrId.SelectedIndex = -1;

        ComboType.DataBind();
        ComboType.SelectedIndex = 0;
        txtDesc.Text = "";
        txtEndDate.Text = "";
        txtInsName.Text = "";
        txtInsRegDate.Text = "";
        txtInsRegNo.Text = "";
        txtLiDate.Text = "";
        txtLiNo.Text = "";
        txtMeFirstName.Text = "";
        txtMeLastName.Text = "";
        txtMeNo.Text = "";
        txtPPCode.Text = "";
        txtPPDuration.Text = "";
        txtPPTeName.Text = "";
        txtSeDuration.Text = "";
        txtSeName.Text = "";
        txtSeTeName.Text = "";
        txtStartDate.Text = "";
        txtTeFileNo.Text = "";
        txtTestDate.Text = "";
        txtTimeMark.Text = "";
        txtTotalMark.Text = "";
        HpFile.ClientVisible = false;
        HpFile.NavigateUrl = "";
    }

    protected void Disable()
    {
        txtDesc.Enabled = false;
        txtEndDate.Enabled = false;
        txtInsName.Enabled = false;
        txtInsRegDate.Enabled = false;
        txtInsRegNo.Enabled = false;
        txtLiDate.Enabled = false;
        txtMeFirstName.Enabled = false;
        txtMeLastName.Enabled = false;
        txtLiNo.Enabled = false;
        txtMeNo.Enabled = false;
        txtPPCode.Enabled = false;
        txtPPDuration.Enabled = false;
        txtPPTeName.Enabled = false;
        txtSeDuration.Enabled = false;
        txtSeName.Enabled = false;
        txtSeTeName.Enabled = false;
        txtStartDate.Enabled = false;
        txtTeFileNo.Enabled = false;
        txtTestDate.Enabled = false;
        txtTimeMark.Enabled = false;
        txtTotalMark.Enabled = false;
        ComboCrsName.Enabled = false;
        ComboMdType.Enabled = false;
        ComboPrId.Enabled = false;
        ComboType.ClientEnabled = false;
        flpImage.Enabled = false;
    }

    protected void Enable()
    {
        txtDesc.Enabled = true;
        txtEndDate.Enabled = true;
        txtInsName.Enabled = true;
        txtInsRegDate.Enabled = true;
        txtInsRegNo.Enabled = true;
        txtLiDate.Enabled = true;
        txtMeFirstName.Enabled = true;
        txtMeLastName.Enabled = true;
        txtLiNo.Enabled = true;
        txtMeNo.Enabled = true;
        txtPPCode.Enabled = true;
        txtPPDuration.Enabled = true;
        txtPPTeName.Enabled = true;
        txtSeDuration.Enabled = true;
        txtSeName.Enabled = true;
        txtSeTeName.Enabled = true;
        txtStartDate.Enabled = true;
        txtTeFileNo.Enabled = true;
        txtTestDate.Enabled = true;
        txtTimeMark.Enabled = true;
        txtTotalMark.Enabled = true;
        ComboCrsName.Enabled = true;
        ComboMdType.Enabled = true;
        ComboPrId.Enabled = true;
        ComboType.ClientEnabled = true;
        flpImage.Enabled = true;

    }

    protected void Edit(int MdId)
    {

        TSP.DataManager.MadrakManager manager = new TSP.DataManager.MadrakManager();
        manager.FindByCode(MdId);
        if (manager.Count == 1)
        {
            try
            {
                manager[0].BeginEdit();

                if (ComboCrsName.Value != null)
                    manager[0]["CrsId"] = ComboCrsName.Value;
                manager[0]["LicenceNo"] = txtLiNo.Text;
                manager[0]["LicenceDate"] = txtLiDate.Text;
                if (ComboMdType.Value != null)
                    manager[0]["MdType"] = ComboMdType.Value;

                if (ComboType.Value.ToString() == "0")//period
                {

                    manager[0]["Duration"] = txtPPDuration.Text;
                    manager[0]["TeName"] = txtPPTeName.Text;
                    manager[0]["TeFileNo"] = txtTeFileNo.Text;
                    manager[0]["InsRegNo"] = txtInsRegNo.Text;
                    manager[0]["InsRegDate"] = txtInsRegDate.Text;
                    manager[0]["TestDate"] = txtTestDate.Text;
                    manager[0]["PPCode"] = txtPPCode.Text;
                    if (!string.IsNullOrEmpty(txtTimeMark.Text))
                        manager[0]["TimeMark"] = txtTimeMark.Text;
                    if (!string.IsNullOrEmpty(txtTotalMark.Text))
                        manager[0]["TotalMark"] = txtTotalMark.Text;

                }
                else
                {
                    manager[0]["SeName"] = txtSeName.Text;
                    manager[0]["Duration"] = txtSeDuration.Text;
                    manager[0]["TeName"] = txtSeTeName.Text;

                }
                if (ComboPrId.Value != null)
                    manager[0]["ProvinceId"] = ComboPrId.Value;
                manager[0]["StartDate"] = txtStartDate.Text;
                manager[0]["EndDate"] = txtEndDate.Text;
                manager[0]["InsName"] = txtInsName.Text;
                manager[0]["Description"] = txtDesc.Text;
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                if (Session["MdFile"] != null)
                    manager[0]["FilePath"] = Session["MdFile"].ToString();
                else
                    manager[0]["FilePath"] = DBNull.Value;
              manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {
                    HpFile.ClientVisible = true;
                    HpFile.NavigateUrl = manager[0]["FilePath"].ToString();

                    MadrakId.Value = Utility.EncryptQS(manager[0]["MdId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    txtMeNo.Enabled = false;
                    ComboType.ClientEnabled = false;
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }


    }

    protected void Insert()
    {
        TSP.DataManager.MadrakManager manager = new TSP.DataManager.MadrakManager();
        try
        {
            DataRow row = manager.NewRow();
            if (ComboType.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع مدرک را انتخاب نمایید";
                return;
            }
            if (!string.IsNullOrEmpty(txtMeNo.Text))
                row["MeId"] = int.Parse(txtMeNo.Text);
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را وارد نمایید";
                return;
            }

            row["LicenceNo"] = txtLiNo.Text;
            row["LicenceDate"] = txtLiDate.Text;
            if (ComboMdType.Value != null)
                row["MdType"] = ComboMdType.Value;
            row["Type"] = ComboType.Value;
            if (ComboType.Value.ToString() == "0")//period
            {
                if (ComboCrsName.Value != null)
                    row["CrsId"] = ComboCrsName.Value;
                row["SeName"] = DBNull.Value;
                row["Duration"] = txtPPDuration.Text;
                row["TeName"] = txtPPTeName.Text;
                row["TeFileNo"] = txtTeFileNo.Text;
                row["InsRegNo"] = txtInsRegNo.Text;
                row["InsRegDate"] = txtInsRegDate.Text;
                row["TestDate"] = txtTestDate.Text;
                row["PPCode"] = txtPPCode.Text;
                if (!string.IsNullOrEmpty(txtTimeMark.Text))
                    row["TimeMark"] = txtTimeMark.Text;
                else
                    row["TimeMark"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtTotalMark.Text))
                    row["TotalMark"] = txtTotalMark.Text;
                else
                    row["TotalMark"] = DBNull.Value;
            }
            else
            {
                row["CrsId"] = DBNull.Value;
                row["SeName"] = txtSeName.Text;
                row["Duration"] = txtSeDuration.Text;
                row["TeName"] = txtSeTeName.Text;
                row["TeFileNo"] = DBNull.Value;
                row["InsRegNo"] = DBNull.Value;
                row["InsRegDate"] = DBNull.Value;
                row["PPCode"] = DBNull.Value;

                row["TimeMark"] = DBNull.Value;
                row["TotalMark"] = DBNull.Value;
            }
            if (ComboPrId.Value != null)
                row["ProvinceId"] = ComboPrId.Value;
            else
                row["ProvinceId"] = DBNull.Value;
            row["StartDate"] = txtStartDate.Text;
            row["EndDate"] = txtEndDate.Text;
            row["InsName"] = txtInsName.Text;
            row["Place"] = DBNull.Value;
            row["CreateDate"] = Utility.GetDateOfToday();
            row["Description"] = txtDesc.Text;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            if (Session["MdFile"] != null)
            {
                row["FilePath"] = Session["MdFile"].ToString();
            }
            else
                row["FilePath"] = DBNull.Value;
            manager.AddRow(row);

            int cn = manager.Save();
            if (cn == 1)
            {
                HpFile.ClientVisible = true;
                HpFile.NavigateUrl = manager[0]["FilePath"].ToString();
                MadrakId.Value = Utility.EncryptQS(manager[0]["MdId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                txtMeNo.Enabled = false;
                ComboType.ClientEnabled = false;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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

    private void SearchMe()
    {
        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        try
        {
            txtMeFirstName.Text = "";
            txtMeLastName.Text = "";
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
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کدعضویت را مجددا وارد نمایید";

            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی رخ داده است. مجدداً وارد نمایید";
        }

        //SetComboType();
    }

    private void Save()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string MdId = Utility.DecryptQS(MadrakId.Value);

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

                if (string.IsNullOrEmpty(MdId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(int.Parse(MdId));
                }
            }
        }
    }

    private void New()
    {
        TSP.DataManager.Permission per = TSP.DataManager.MadrakManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;


        MadrakId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        SetControlVisible();
    }

    private void Edit()
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string MdId = Utility.DecryptQS(MadrakId.Value);

        if (string.IsNullOrEmpty(MdId))
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
                txtMeNo.Enabled = false;
                ComboType.ClientEnabled = false;
                //SetComboType();
                TSP.DataManager.Permission per = TSP.DataManager.MadrakManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }
        }
    }

    private void SetControlVisible()
    {
        if (ComboType.Value.ToString() == "0")//period
        {
            if (!this.Page.IsStartupScriptRegistered("Key"))
                this.Page.RegisterStartupScript("Key", "<script>SetPeriod();</script>");
        }
        else if (ComboType.Value.ToString() == "1")//Seminar
        {
            if (!this.Page.IsStartupScriptRegistered("Key1"))
                this.Page.RegisterStartupScript("Key1", "<script>SetSeminar();</script>");
        }
      
    }
    #endregion
}
