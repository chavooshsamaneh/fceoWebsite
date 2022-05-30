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
using System.IO;

public partial class NezamRegister_WizardMemberLicence : System.Web.UI.Page
{
    DataTable dtMadrak = new DataTable();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        #region Set Registration Menu
        ASPxMenu1.Items.FindByName("Madrak").Selected = true;
        if (Session["MemberMembership"] != null && (Boolean)Session["MemberMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Member"] != null && ((DataTable)Session["Member"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMadrak"] != null && ((DataTable)Session["TblOfMadrak"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Madrak").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Madrak").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Madrak").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblJob"] != null && ((DataTable)Session["TblJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblActivity"] != null && Session["TblActivity2"] != null && ((DataTable)Session["TblActivity"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Activity").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Activity").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblLanguage"] != null && ((DataTable)Session["TblLanguage"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Language").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Language").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Language").Image.Height = Unit.Pixel(15);
        }
        if (Session["MemberSummary"] != null && (Boolean)Session["MemberSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
        #endregion
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["License"] = null;
            Session["LicenseName"] = null;
            HiddenFieldUniValue.Set("Id", null);

            OdbCity.CacheDuration = Utility.GetCacheDuration();
            OdbCountry.CacheDuration = Utility.GetCacheDuration();
            ODBUniversity.CacheDuration = Utility.GetCacheDuration();

            ComboUniversity.DataBind();
            ComboUniversity.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            ComboCountry.DataBind();
            ComboCountry.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            SetIranForCountry();
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            #region Set MadrakDataTable
            if (Session["TblOfMadrak"] == null)
            {
                dtMadrak.Columns.Add("MlId");
                dtMadrak.Columns.Add("LiId");
                dtMadrak.Columns.Add("LiName");
                dtMadrak.Columns.Add("MajorId");
                dtMadrak.Columns.Add("MajorName");
                dtMadrak.Columns.Add("UniId");
                dtMadrak.Columns.Add("UniName");
                dtMadrak.Columns.Add("CitId");
                dtMadrak.Columns.Add("CityName");
                dtMadrak.Columns.Add("StartDate");
                dtMadrak.Columns.Add("EndDate");
                dtMadrak.Columns.Add("NumUnit");
                dtMadrak.Columns.Add("Avg");
                dtMadrak.Columns.Add("Description");
                dtMadrak.Columns.Add("DefaultValue");
                dtMadrak.Columns.Add("CounId");
                dtMadrak.Columns.Add("CounName");
                dtMadrak.Columns.Add("Thesis");

                dtMadrak.Columns.Add("TiId");
                dtMadrak.Columns.Add("TiName");
                dtMadrak.Columns.Add("LicenseUrl");
                dtMadrak.Columns.Add("LicenseUrlName");
                dtMadrak.Columns.Add("LicenceCode");
                dtMadrak.Columns.Add("Tiid");

                dtMadrak.Columns["MlId"].AutoIncrement = true;
                dtMadrak.Columns["MlId"].AutoIncrementSeed = 1;
                dtMadrak.Constraints.Add("PK_ID", dtMadrak.Columns["MlId"], true);


                Session["TblOfMadrak"] = dtMadrak;
            }
            #endregion

        }

        if (Session["License"] != null && Session["LicenseName"] != null)
        {
            HpLicense.ClientVisible = true;
            HpLicense.NavigateUrl = Session["License"].ToString();
            HDFlpLicense.Set("name", 1);
            imgEndUploadImgIdNo.ClientVisible = true;
            lblImgErr.ClientVisible = false;
        }
        else
        {
            HpLicense.NavigateUrl = "";
        }

        if (Session["TblOfMadrak"] != null)
        {
            dtMadrak = (DataTable)Session["TblOfMadrak"];
            GridViewMeLicence.DataSource = dtMadrak;
            GridViewMeLicence.DataBind();
        }

        if (ComboCountry.Value != null)
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
            ComboUniversity.DataBind();
        }
        SetCmbMajor();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (CheckHasKarshenasi() == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت حداقل یک مدرک تحصیلی با مقطع کارشناسی یا کارشناسی ناپیوسته الزامی می باشد";
            return;
        }
        if (!CheckKarshenasiNaPeyvasteCondition())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت مدرک کاردانی الزامی می باشد";
            return;
        }
        if (CheckNezamKardaniEstelam())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بارگذاری تصویر استعلام عدم عضویت در نظام کاردانی در فرم قبلی برای شما اجباری می باشد لطفا قبل از ادامه دادن فرایند در فرم قبل این تصویر را بارگذاری نمایید";
            return;
        }
        else
            Response.Redirect("WizardMemberJob.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCity.Text))
        {
            SetMessage("شهر را وارد نمایید");
            return;
        }
        int LicenceCode = -1;
        int Tiid = -1;

        DataTable dtMemberFileMajor;
        var ds = new DataSet();
        var dv = (DataView)ODBLicence.Select();
        if (dv != null && dv.Count > 0)
        {
            dtMemberFileMajor = dv.ToTable();
            dtMemberFileMajor.DefaultView.RowFilter = "LiId=" + drdLicence.Value;
            if (dtMemberFileMajor.DefaultView.Count == 1)
            {
                LicenceCode = Convert.ToInt32(dtMemberFileMajor.DefaultView[0]["LicenceCode"]);
            }
            else
            {
                SetMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                dtMemberFileMajor.DefaultView.RowFilter = "";
                return;
            }
            dtMemberFileMajor.DefaultView.RowFilter = "";
            TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
            int LiId = Convert.ToInt32(drdLicence.Value);
            LicenceManager.FindByCode(LiId);
            if (LicenceManager.Count == 1)
            {
                Tiid = Convert.ToInt32(LicenceManager[0]["Tiid"]);
            }
            else
            {
                SetMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return;
            }

            if (txtDescription.Text.Length > 255)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تعداد کاراکتر های فیلد توضیحات بیش از حد مجاز می باشد.بایستی حداکثر255کاراکتر می باشد.";
                return;
            }
            //if (cmbLicenceType.SelectedIndex == 0)
            //{
            //    if (LicenceCode == (int)TSP.DataManager.Licence.kardani)
            //    {
            //   SetMessage( "مدرک کاردانی را نمی توان به عنوان پیش فرض انتخاب کرد");
            //        return;
            //    }

            //}

            object Num = DBNull.Value;
            if (!string.IsNullOrEmpty(txtNumUnit.Text))
                Num = txtNumUnit.Text;

            object Avg = DBNull.Value;
            if (!string.IsNullOrEmpty(txtAvg.Text))
                Avg = txtAvg.Text;

            bool defaultValue = false;

            object UniId = DBNull.Value;

            string UniName = "";

            if (ComboUniversity.Value != null)
            {
                UniId = Convert.ToInt32(ComboUniversity.Value);
                UniName = ComboUniversity.SelectedItem.Text;
            }
            object CitId = DBNull.Value;
            string CitName = "";
            CitName = txtCity.Text;

            object CounId = DBNull.Value;
            string CounName = "";
            if (ComboCountry.Value != null)
            {
                CounId = ComboCountry.Value;
                CounName = ComboCountry.SelectedItem.Text;
            }

            string LicenseUrl = "";
            string LicenseUrlName = "";
            if (Session["License"] != null && Session["LicenseName"] != null)
            {
                LicenseUrl = Session["License"].ToString();
                LicenseUrlName = Session["LicenseName"].ToString();

            }
            bool check = false;

            if (GridViewMeLicence.VisibleRowCount > 0)
            {
                GridViewMeLicence.DataSource = (DataTable)Session["TblOfMadrak"];
                GridViewMeLicence.DataBind();

                for (int i = 0; i < GridViewMeLicence.VisibleRowCount; i++)
                {
                    DataRowView dr = (DataRowView)GridViewMeLicence.GetRow(i);
                    if ((dr["LiName"].ToString() == drdLicence.SelectedItem.Text) && (dr["MajorName"].ToString() == cmbMajor.SelectedItem.Text))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (!check)
            {
                int TiId = -1;
                string TiName = "";
                ((DataTable)Session["TblOfMadrak"]).Rows.Add(new object[] {null,drdLicence.Value, drdLicence.SelectedItem.Text,
                cmbMajor.Value, cmbMajor.SelectedItem.Text, UniId,
                UniName,CitId, CitName, txtStartDate.Text, txtEndDate.Text,
                Num , Avg, txtDescription.Text , defaultValue, CounId,CounName,
                txtThesis.Text,TiId,TiName,LicenseUrl,LicenseUrlName,LicenceCode,Tiid});
            }
            else
            {
                SetMessage("اطلاعات وارد شده تکراری می باشد");
                return;
            }

            GridViewMeLicence.DataSource = (DataTable)Session["TblOfMadrak"];
            GridViewMeLicence.DataBind();
            dtMadrak = (DataTable)Session["TblOfMadrak"];

            if (dtMadrak.Rows.Count > 0)
            {
                ASPxMenu1.Items.FindByName("Madrak").Image.Url = "~/Images/icons/button_ok.png";
                ASPxMenu1.Items.FindByName("Madrak").Image.Width = 15;
                ASPxMenu1.Items.FindByName("Madrak").Image.Height = 15;
            }
            ClearForm();

        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardMember.aspx");
    }

    protected void GridViewMeLicence_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        dtMadrak = (DataTable)Session["TblOfMadrak"];
        dtMadrak.Rows.Find(e.Keys[0]).Delete();

        Session["TblOfMadrak"] = dtMadrak;
        GridViewMeLicence.DataSource = (DataTable)Session["TblOfMadrak"];
        GridViewMeLicence.DataBind();
        dtMadrak = (DataTable)Session["TblOfMadrak"];

        ClearForm();
        if (dtMadrak.Rows.Count == 0)
        {
            ASPxMenu1.Items.FindByName("Madrak").Image.Url = "";

        }
    }

    protected void GridViewMeLicence_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] Parameters = e.Parameters.Split(';');
        if (Parameters.Length == 0)
            return;
        if (Parameters[0] == "Search")
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = Parameters[2];
            ComboUniversity.DataBind();
        }
        else
        {
            string country = Parameters[0];
            string UnName = Parameters[1];
            string ReType = Parameters[2];
            switch (ReType)
            {
                case "btnRefresh":
                    SetIranForCountry();
                    break;
                default:
                    FillUniversity(country, UnName);
                    break;
            }
        }
    }

    protected void flpLicense_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void CallbackPanelMajor_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (cmbParentMajor.SelectedIndex <= -1)
        {
            cmbMajor.Text = "";
            cmbMajor.ClientEnabled = false;
            return;
        }
        string[] parameter = e.Parameter.Split(';');
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        if (parameter[0] == "cmbChange")
        {
            int ParentId = Convert.ToInt32(parameter[1]);
            ODBMajor.SelectParameters["MjId"].DefaultValue = ParentId.ToString();
            cmbMajor.DataBind();
            cmbMajor.ClientEnabled = true;
            cmbMajor.Text = "";
        }
        else if (parameter[0] == "ComboCountry")
        {
            int CountId = Convert.ToInt32(parameter[1]);
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = CountId.ToString();
            ComboUniversity.DataBind();
            ComboUniversity.ClientEnabled = true;
            ComboUniversity.SelectedIndex = 0;
        }

        
    }
    #endregion

    #region Methods
    private void ClearForm()
    {
        btnAdd.ClientEnabled = true;
        drdLicence.DataBind();
        drdLicence.SelectedIndex = -1;
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = -1;
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = -1;
        cmbParentMajor.DataBind();
        cmbParentMajor.SelectedIndex = -1;
        cmbMajor.ClientEnabled = false;
        ComboUniversity.DataBind();
        ComboUniversity.SelectedIndex = -1;

        txtCity.Text = "";
        txtNumUnit.Text = "";
        txtAvg.Text = "";
        txtThesis.Text = "";

        imgEndUploadImgIdNo.ClientVisible = false;
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtStartDate.Text = "";
     
        ComboCountry.DataBind();
        ComboCountry.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

        SetIranForCountry();
        if (ComboCountry.Value != null)
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
        else
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = "-2";
        ComboUniversity.DataBind();

        Session["LicenseName"] = null;
        Session["License"] = null;
        HDFlpLicense.Set("name", 0);
        lblImgErr.ClientVisible = false;
        HpLicense.ClientVisible = false;
        HiddenFieldUniValue.Set("Id", null);
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["LicenseName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/License/") + ret) == true);
            string tempFileName = MapPath("~/Image/Members/License/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["License"] = ret;

        }
        return ret;
    }

    int FindMaxLiId(int NewValue)
    {
        int Max = NewValue;
        for (int i = 0; i < GridViewMeLicence.VisibleRowCount; i++)
        {
            DataRowView dr = (DataRowView)GridViewMeLicence.GetRow(i);
            if (Max < int.Parse(dr["LiId"].ToString()))
                Max = int.Parse(dr["LiId"].ToString());
        }
        return Max;
    }

    int FindLicenceCode(int LiId)
    {
        //---Find LicenceCode
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        LicenceManager.FindByCode(LiId);
        if (LicenceManager.Count == 1)
            return Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
        else return -1;
    }

    protected void FillUniversity(string country, String UnName)
    {
        if (string.IsNullOrEmpty(country)) return;
        ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = country;
        ComboUniversity.DataBind();
    }

    protected void SetIranForCountry()
    {
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = ComboCountry.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberlicence).ToString());
    }

    Boolean CheckDefaultMadrak()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToBoolean(dt.Rows[i]["DefaultValue"]) == true)
                return true;
        }

        return false;
    }

    protected Boolean CheckHasKarshenasi()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];
        Boolean flag = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.Karshenasi || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.MoadeleKarshenasi || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.ArshadPeybaste)
            {
                flag = true;
                break;
            }

        }

        return flag;
    }
    Boolean CheckKarshenasiNaPeyvasteCondition()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];
        Boolean IsNapeyvaste = false;
        Boolean HasKardani = false;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste)
            {
                IsNapeyvaste = true;
                break;
            }
        }
        if (IsNapeyvaste)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.kardani)
                {
                    HasKardani = true;
                    break;
                }
            }
        }
        if (IsNapeyvaste && !HasKardani)
            return false;
        return true;
    }

    protected Boolean CheckNezamKardaniEstelam()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];
        Boolean HaveKardani = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.kardani || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste)
            {
                HaveKardani = true;
                break;
            }

        }
        if (HaveKardani && Session["FileOfKardani"] == null)
        {
            return true;
        }

        return false;
    }
    private void SetCmbMajor()
    {
        if (cmbParentMajor.SelectedIndex > -1)
        {
            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            int ParentId = Convert.ToInt32(cmbParentMajor.Value);
            ODBMajor.SelectParameters["MjId"].DefaultValue = ParentId.ToString();
            cmbMajor.DataBind();
            cmbMajor.ClientEnabled = true;
        }
        else
        {
            cmbMajor.Text = "";
            cmbMajor.ClientEnabled = false;
        }
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
