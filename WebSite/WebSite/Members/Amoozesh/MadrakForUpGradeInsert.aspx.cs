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

public partial class Members_Amoozesh_MadrakForUpGradeInsert : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            #region  !IsPostback
            Session["MdFile"] = null;
            HiddenFieldImg["ImgPeriod"] = 0;


            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["MdId"]))
            {
                Response.Redirect("MadrakForUpGrade.aspx");
                return;
            }
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

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    odbCourseName.SelectParameters["InActiveUpGradePoint"].DefaultValue = "-1";
                    FillForm(int.Parse(MdId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    SearchMe();
                    Enable();
                    odbCourseName.SelectParameters["InActiveUpGradePoint"].DefaultValue = "0";
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;

            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            #endregion
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "MadrakForUpGrade.aspx";
        SetControlVisible();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MadrakForUpGrade.aspx");

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
            HiddenFieldImg["ImgPeriod"]= 1;


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
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
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
            ComboPrId.SelectedIndex = ComboPrId.Items.IndexOfValue(manager[0]["ProvinceId"].ToString());
            comboRes.DataBind();
            comboRes.SelectedIndex = comboRes.Items.IndexOfValue(manager[0]["ResId"].ToString());
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
        ComboPrId.DataBind();
        ComboPrId.SelectedIndex = -1;
        comboRes.DataBind();
        comboRes.SelectedIndex = -1;
        
        ComboType.DataBind();
        ComboType.SelectedIndex = 0;
        txtDesc.Text = "";
        txtEndDate.Text = "";
        txtInsName.Text = "";
        txtInsRegDate.Text = "";
        txtInsRegNo.Text = "";
        txtLiDate.Text = "";
        txtLiNo.Text = "";
        txtMeNo.Text = Utility.GetCurrentUser_MeId().ToString();
        SearchMe();
        txtPPCode.Text = "";
        txtPPDuration.Text = "";
        txtPPTeName.Text = "";
        //txtSeName.Text = "";
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
        txtDesc.Enabled = 
        txtEndDate.Enabled = 
        txtInsName.Enabled = 
        txtInsRegDate.Enabled =
        txtInsRegNo.Enabled = 
        txtLiDate.Enabled =
        txtLiNo.Enabled = 
        txtMeNo.Enabled = 
        txtPPCode.Enabled = 
        txtPPDuration.Enabled = 
        txtPPTeName.Enabled = 
        txtSeTeName.Enabled =
        txtStartDate.Enabled =
        txtTeFileNo.Enabled = 
        txtTestDate.Enabled = 
        txtTimeMark.Enabled = 
        txtTotalMark.Enabled = 
        ComboCrsName.Enabled = 
        ComboPrId.Enabled =
        comboRes.Enabled=
        ComboType.ClientEnabled = false;
        flpImage.Enabled = false;
    }

    protected void Enable()
    {
        txtMeNo.ClientEnabled = false;
        ComboType.ClientEnabled = false;
        txtDesc.Enabled =
        txtEndDate.Enabled = 
        txtInsName.Enabled = 
        txtInsRegDate.Enabled =
        txtInsRegNo.Enabled = 
        txtLiDate.Enabled =
        txtLiNo.Enabled = 
        txtPPCode.Enabled =
        txtPPDuration.Enabled = 
        txtPPTeName.Enabled =
        txtSeTeName.Enabled =
        txtStartDate.Enabled =
        txtTeFileNo.Enabled =
        txtTestDate.Enabled =
        txtTimeMark.Enabled = 
        txtTotalMark.Enabled =
        ComboCrsName.Enabled =
        ComboPrId.Enabled =
        comboRes.Enabled=
        flpImage.Enabled = true;

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
            row["MdType"] = 0;
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
                //row["SeName"] = txtSeName.Text;
                //row["Duration"] = txtSeDuration.Text;
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

            if (comboRes.Value != null)
                row["ResId"] = comboRes.Value;
            else
                row["ResId"] = DBNull.Value;

            
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
            if (manager.Save() == 1)
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

    private void SearchMe()
    {
        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        try
        {
            txtMeNo.Text = Utility.GetCurrentUser_MeId().ToString();
            txtMeFirstName.Text = "";
            txtMeLastName.Text = "";
            if (!(string.IsNullOrEmpty(txtMeNo.Text)))
            {

                MemManager.FindByCode(Utility.GetCurrentUser_MeId());
                if (MemManager.Count > 0)
                {
                    txtMeFirstName.Text = MemManager[0]["FirstName"].ToString();
                    txtMeLastName.Text = MemManager[0]["LastName"].ToString();


                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطا در بازیابی اطلاعات رخ داده است";
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
        }
    }

    private void New()
    {
        btnSave.Enabled = btnSave2.Enabled = true;
        MadrakId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        SetControlVisible();
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