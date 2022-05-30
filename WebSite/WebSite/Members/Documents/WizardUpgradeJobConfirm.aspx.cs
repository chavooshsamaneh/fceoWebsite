using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;
using System.Collections;

public partial class Members_Documents_WizardUpgradeJobConfirm : System.Web.UI.Page
{
    DataTable dtJobConfirm = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        CheckTsTimeOut();

        if (!IsPostBack)
        {
            HiddenFieldJobConfirm["Confname"] = 0;
            HiddenFieldJobConfirm["Conf1"] = 0;
            HiddenFieldJobConfirm["Conf2"] = 0;
            HiddenFieldJobConfirm["DocExpired"] = 0;
            Session["DocUpgradeJobFileURL"] = null;
            Session["DocUpgradeJobGrdURL"] = null;
            SetMenueImage();
            SetHelpAddress();
            CreateJobConfirmDataTable();
            ObjectDataSourceJobConfirm.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        }

        if (Session["WizardUpgradeJobConfirm"] != null)
        {
            dtJobConfirm = (DataTable)Session["WizardUpgradeJobConfirm"];
            GrdvJobCon.DataSource = dtJobConfirm;
            GrdvJobCon.DataBind();
        }

        if (Session["DocUpgradeJobFileURL"] != null)
        {
            ImageConf.ImageUrl = Session["DocUpgradeJobFileURL"].ToString();
        }
        if (Session["DocUpgradeJobGrdURL"] != null)
        {
            ImageGrd.ImageUrl = Session["DocUpgradeJobGrdURL"].ToString();
        }

    }

    protected void btnJob_Click(object sender, EventArgs e)
    {
        bool check = false;

        if (GrdvJobCon.VisibleRowCount > 0)
        {
            GrdvJobCon.DataSource = (DataTable)Session["WizardUpgradeJobConfirm"];
            GrdvJobCon.DataBind();

            for (int i = 0; i < GrdvJobCon.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)GrdvJobCon.GetRow(i);
                if (dr["MeId"].ToString() == txtMeId1.Text
                    && dr["DateFrom"].ToString() == txtDateFrom.Text
                    && dr["DateTo"].ToString() == txtDateTo.Text
                    && int.Parse(dr["ConfirmTypeId"].ToString()) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
                {
                    check = true;
                    break;
                }
            }
        }


        if (!check)
        {
            InsertJobConfirm();
        }
        else
        {

            SetMessage("اطلاعات وارد شده تکراری می باشد");
            return;

        }

        if (dtJobConfirm.Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = 15;
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = 15;
        }

    }

    protected void GrdvJobCon_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        dtJobConfirm = (DataTable)Session["WizardUpgradeJobConfirm"];

        dtJobConfirm.Rows.Find(e.Keys[0]).Delete();

        Session["WizardUpgradeJobConfirm"] = dtJobConfirm;
        GrdvJobCon.DataSource = (DataTable)Session["WizardUpgradeJobConfirm"];
        GrdvJobCon.DataBind();
        dtJobConfirm = (DataTable)Session["WizardUpgradeJobConfirm"];

        if (dtJobConfirm.Rows.Count == 0)
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "";

        btnJobRefresh_Click(this, new EventArgs());
        //}
    }

    protected void btnJobRefresh_Click(object sender, EventArgs e)
    {
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        txtPosition.Text = "";
        txtDescription.Text = "";
        if (cmbConfirmType.SelectedIndex == 1)
        {
            cmbConfirmType.SelectedIndex = 1;
        }
        else
        {
            cmbConfirmType.SelectedIndex = 0;
        }
        txtDescription.Text = "";
        txtMeId1.Text = "";
        txtOfficeMfNo.Text = "";
        txtOfficeName.Text = "";
        ImageConf.ImageUrl = "~/Images/person.png";
        ImageGrd.ImageUrl = "~/Images/person.png";
        lblMeName1.Text = "- - -";
        lblMeFileNo1.Text = "- - -";
        lblLicenseDate.Text = "- - -";
        SetVisible();

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int towMember = 0;
        //if (Session["WizardUpgradeJobConfirm"] != null && ((DataTable)Session["WizardUpgradeJobConfirm"]).Rows.Count > 0)
        //{
        if (!CheckTotalDate())
            return;
        for (int i = 0; i < ((DataTable)Session["WizardUpgradeJobConfirm"]).Rows.Count; i++)
        {
            if (int.Parse(((DataTable)Session["WizardUpgradeJobConfirm"]).Rows[i]["ConfirmTypeId"].ToString()) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
                towMember++;
        }
        if (towMember == 0 || towMember >= 2)
        {
            Response.Redirect("WizardUpgradeDocSummary.aspx");
        }
        else
        {
            SetMessage("حداقل دو نفر عضو حقیقی باید به عنوان تایید کننده معرفی شوند");
        }
        //}
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardUpgradeUploaddocs.aspx");
    }

    protected void flpConfAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageAttach(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void txtMeId1_TextChanged(object sender, EventArgs e)
    {
        HiddenFieldJobConfirm["DocExpired"] = 0;
        int MeId = 0;
        lblMeName1.Text = "";
        lblMeFileNo1.Text = "";
        lblLicenseDate.Text = "";
        SetJobConfirmVisible((int)TSP.DataManager.DocumentJobConfirmType.TwoMembers);
        HiddenFieldJobConfirm["Conf1"] = 0;
        //if (txtMeId1.Text == txtMeId2.Text)
        //{
        //    SetMessage("پیش از این عضو وارد شده را به عنوان تایید کننده وارد نموده اید");
        //    return;
        //}
        if (string.IsNullOrEmpty(txtMeId1.Text))
        {
            SetMessage("کد عضویت تایید کننده را وارد نمایید");
            return;
        }
        int.TryParse(txtMeId1.Text, out MeId);
        if (MeId == Utility.GetCurrentUser_MeId())
        {
            SetMessage("کد عضویت تایید کننده نمی تواند با کد عضویت متقاضی یکی باشد");
            return;
        }

        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            SetMessage("کد عضویت وارد شده معتبر نمی باشد");
            return;
        }

        DataTable dtMemberLicenceManager = MemberLicenceManager.SelectByMemberId(MeId, 0);
        dtMemberLicenceManager.DefaultView.RowFilter = "LicenceCode <> " + (int)TSP.DataManager.Licence.kardani;
        int Count = dtMemberLicenceManager.DefaultView.Count;
        bool CanAccept = false;
        if (Count > 0)
        {
            for (int i = 0; i < Count; i++)
            {
                string LicEndDate = dtMemberLicenceManager.DefaultView[i]["EndDate"].ToString();
                Utility.Date objDate = new Utility.Date(LicEndDate);
                string TenYearsAgo = objDate.AddYears(10);
                string Today = Utility.GetDateOfToday();
                int IsDocExp = string.Compare(Today, TenYearsAgo);
                if (IsDocExp >= 0)
                {
                    CanAccept = true;
                    lblLicenseDate.Text = dtMemberLicenceManager.DefaultView[0]["EndDate"].ToString();
                    break;
                }
            }
            if (!CanAccept)
            {
                SetMessage("عضو وارد شده نمی تواند سابقه کار شما را تایید نماید.باید از مدرک فارغ التحصیلی تایید کننده ده سال گذشته باشد");
                return;
            }

        }


        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeName"]))
        {
            lblMeName1.Text = MemberManager[0]["MeName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
        {
            lblMeFileNo1.Text = MemberManager[0]["FileNo"].ToString();
        }
        else
        {
            SetMessage("عضو وارد شده نمی تواند سابقه کار شما را تایید نماید.این عضو دارای پروانه اشتغال به کار نمی باشد");
            return;
        }

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
        {
            string FileDate = MemberManager[0]["FileDate"].ToString();
            if (FileDate.CompareTo(Utility.GetDateOfToday()) <= 0)
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

                //string FileDateReq=
                DataTable dtDocMe = DocMemberFileManager.SelectMainRequest(MeId, 0);
                if (dtDocMe.Rows.Count > 0)
                {
                    if (Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["TaskCode"])
                        || (Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument))
                    // && Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess))
                    {
                        HiddenFieldJobConfirm["DocExpired"] = 1;
                        SetMessage("تاریخ اعتبار پروانه عضو وارد شده به اتمام رسیده است. عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");

                        return;
                    }
                }
                else
                {
                    HiddenFieldJobConfirm["DocExpired"] = 1;
                    SetMessage("تاریخ اعتبار پروانه عضو وارد شده به اتمام رسیده است. عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");

                    return;
                }
            }
        }
        else
        {
            HiddenFieldJobConfirm["DocExpired"] = 1;
            SetMessage("تاریخ اعتبار پروانه عضو وارد شده مشخص نمی باشد.عضو دیگری جهت  تایید سابقه کار خود انتخاب نمایید");
            return;
        }
        HiddenFieldJobConfirm["Conf1"] = 1;
    }

    #region Methods

    private void SetWarningLableDisable()
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberJob).ToString());
    }

    private void SetMenueImage()
    {
        if (Session["WizardDocUpgradeOath"] != null && (Boolean)Session["WizardDocUpgradeOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardUpgradeJobConfirm"] != null && ((DataTable)Session["WizardUpgradeJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocUpgradeSummary"] != null && (Boolean)Session["WizardDocUpgradeSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardUpgradeImgfrontDoc"] != null || Session["WizardUpgradeImgTaxOfficeLetter"] != null)
        {
            MenuSteps.Items.FindByName("Kardan").Visible = true;
            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }
    }

    private void SetVisible()
    {
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
        {
            lblDescription.ClientVisible = true;
            txtDescription.ClientVisible = true;
            lblOfficeName.ClientVisible = true;
            txtOfficeName.ClientVisible = true;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = true;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = true;
            flpGrdAttach.ClientVisible = true;
            ImageGrd.ClientVisible = true;
            ImageGrd.ImageUrl = "~/Images/person.png";
            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
        {
            lblDescription.ClientVisible = false;
            txtDescription.ClientVisible = false;
            lblOfficeName.ClientVisible = false;
            txtOfficeName.ClientVisible = false;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = false;
            RoundPanelconfMember1.ClientVisible = true;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";
            HiddenFieldJobConfirm["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.GovCom)
        {
            lblDescription.ClientVisible = true;
            txtDescription.ClientVisible = true;
            lblOfficeName.ClientVisible = true;
            txtOfficeName.ClientVisible = true;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = true;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";
            HiddenFieldJobConfirm["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = false;
            ComboProvince.ClientVisible = false;

        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
        {
            lblDescription.ClientVisible = false;
            txtDescription.ClientVisible = false;
            lblOfficeName.ClientVisible = false;
            txtOfficeName.ClientVisible = false;
            lblOfficeMfNo.ClientVisible = false;
            txtOfficeMfNo.ClientVisible = false;
            lblHelpDes.ClientVisible = false;
            RoundPanelconfMember1.ClientVisible = false;
            lblGrid.ClientVisible = false;
            flpGrdAttach.ClientVisible = false;
            imgEndUploadGrd.ClientVisible = false;
            lblValidationGrd.ClientVisible = false;
            ImageGrd.ImageUrl = "";
            HiddenFieldJobConfirm["Grdname"] = 0;
            ImageGrd.ClientVisible = false;
            lblProvince.ClientVisible = true;
            ComboProvince.ClientVisible = true;
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocUpgradeSummary"] == null && Session["WizardDocUpgradeOath"] == null
         && Session["WizardUpgradeJobConfirm"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocUpgradeOath"] == null)
        {
            SetMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }

    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        string tempFileName = "";


        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId_" + Utility.GetCurrentUser_MeId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;


            } while ((id == "flpConfAttach" && File.Exists(MapPath("~/image/DocMeFile/JobConfirm/") + ret) == true)
            || (id == "flpGrdAttach" && File.Exists(MapPath("~/image/DocMeFile/OfficeGrade/") + ret) == true)
              );

            if (id == "flpConfAttach")
            {
                Session["DocUpgradeJobFileURL"] = "~/Image/DocMeFile/JobConfirm/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/JobConfirm/") + ret;
            }
            if (id == "flpGrdAttach")
            {
                Session["DocUpgradeJobGrdURL"] = "~/Image/DocMeFile/OfficeGrade/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OfficeGrade/") + ret;
            }

            uploadedFile.SaveAs(tempFileName, true);
        }
        return ret;
    }

    private void CreateJobConfirmDataTable()
    {
        if (Session["WizardUpgradeJobConfirm"] == null)
        {
            dtJobConfirm.Columns.Add("JobConfId");
            dtJobConfirm.Columns["JobConfId"].AutoIncrement = true;
            dtJobConfirm.Columns["JobConfId"].AutoIncrementSeed = 1;
            dtJobConfirm.Constraints.Add("PK_ID", dtJobConfirm.Columns["JobConfId"], true);
            dtJobConfirm.Columns.Add("ConfirmTypeId");
            dtJobConfirm.Columns.Add("ConfirmTypeName");
            dtJobConfirm.Columns.Add("MeId");
            dtJobConfirm.Columns.Add("Name");
            dtJobConfirm.Columns.Add("MFNo");
            dtJobConfirm.Columns.Add("FileURL");
            dtJobConfirm.Columns.Add("GradeURL");
            dtJobConfirm.Columns.Add("Description");
            dtJobConfirm.Columns.Add("DocExpired");
            dtJobConfirm.Columns.Add("DateFrom");
            dtJobConfirm.Columns.Add("DateTo");
            dtJobConfirm.Columns.Add("Position");
            Session["WizardUpgradeJobConfirm"] = dtJobConfirm;
        }
    }

    private void SetJobConfirmVisible(int ConfirmType)
    {
        switch (ConfirmType)
        {
            case (int)TSP.DataManager.DocumentJobConfirmType.Office:
                lblDescription.ClientVisible = true;
                txtDescription.ClientVisible = true;
                lblOfficeName.ClientVisible = true;
                txtOfficeName.ClientVisible = true;
                lblOfficeMfNo.ClientVisible = true;
                txtOfficeMfNo.ClientVisible = true;
                lblHelpDes.ClientVisible = true;
                RoundPanelconfMember1.ClientVisible = false;
                //RoundPanelconfMember2.ClientVisible = false;
                break;
            case (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers:
                lblDescription.ClientVisible = false;
                txtDescription.ClientVisible = false;
                lblOfficeName.ClientVisible = false;
                txtOfficeName.ClientVisible = false;
                lblOfficeMfNo.ClientVisible = false;
                txtOfficeMfNo.ClientVisible = false;
                lblHelpDes.ClientVisible = false;
                RoundPanelconfMember1.ClientVisible = true;
                //RoundPanelconfMember2.ClientVisible = true;
                lblGrid.ClientVisible = false;
                flpGrdAttach.ClientVisible = false;
                imgEndUploadGrd.ClientVisible = false;
                lblValidationGrd.ClientVisible = false;
                ImageGrd.ImageUrl = "";
                HiddenFieldJobConfirm["Grdname"] = 0;
                ImageGrd.ClientVisible = false;

                break;
        }
    }

    private Boolean InsertJobConfirm()
    {
        if (Session["WizardUpgradeJobConfirm"] == null)
        {
            SetMessage("اطلاعات تایید کننده سابقه کار تکمیل نشده است");
            return false;
        }
        if (Session["DocUpgradeJobFileURL"] == null)
        {
            SetMessage("تصویر فرم سابقه کار ثبت نشده است");
            return false;
        }
        if (cmbConfirmType.Value == null)
        {
            SetMessage("نوع تایید کننده را انتخاب نمایید");
            return false;
        }
        if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
        {
            if (HiddenFieldJobConfirm["Conf1"].ToString() != "1")
            {
                SetMessage("اطلاعات تایید کننده معتبر نمی باشد");
                return false;
            }
        }

        try
        {

            if (!CheckJobDate(txtDateFrom.Text, txtDateTo.Text))
                return false;
            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
            {
                #region
                if (Session["DocUpgradeJobGrdURL"] == null)
                {
                    SetMessage("تصویر پروانه یا گواهی رتبه بندی ثبت نشده است");
                    return false;
                }
                DataRow dr = dtJobConfirm.NewRow();

                dr["DateFrom"] = txtDateFrom.Text;
                dr["DateTo"] = txtDateTo.Text;
                dr["Position"] = txtPosition.Text;

                dr["ConfirmTypeId"] = (int)TSP.DataManager.DocumentJobConfirmType.Office;
                dr["ConfirmTypeName"] = cmbConfirmType.Text;
                dr["Name"] = txtOfficeName.Text;
                dr["MFNo"] = txtOfficeMfNo.Text;
                dr["Description"] = txtDescription.Text;
                if (Session["DocUpgradeJobFileURL"] != null)
                {
                    dr["FileURL"] = Session["DocUpgradeJobFileURL"].ToString();
                    Session.Remove("DocUpgradeJobFileURL");
                    Session["DocUpgradeJobFileURL"] = null;
                }
                if (Session["DocUpgradeJobGrdURL"] != null)
                {
                    dr["GradeURL"] = Session["DocUpgradeJobGrdURL"].ToString();
                    Session.Remove("DocUpgradeJobGrdURL");
                    Session["DocUpgradeJobGrdURL"] = null;
                }
                dtJobConfirm.Rows.Add(dr);
                #endregion

            }
            else if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
            {
                #region
                DataRow dr = dtJobConfirm.NewRow();

                dr["DateFrom"] = txtDateFrom.Text;
                dr["DateTo"] = txtDateTo.Text;
                dr["Position"] = txtPosition.Text;

                dr["ConfirmTypeId"] = (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers;
                dr["ConfirmTypeName"] = cmbConfirmType.Text;
                dr["MeId"] = txtMeId1.Text;
                dr["Name"] = lblMeName1.Text;
                dr["MFNo"] = lblMeFileNo1.Text;
                dr["DocExpired"] = HiddenFieldJobConfirm["DocExpired"];
                if (Session["DocUpgradeJobFileURL"] != null)
                {
                    dr["FileURL"] = Session["DocUpgradeJobFileURL"].ToString();
                    Session.Remove("DocUpgradeJobFileURL");
                }
                dtJobConfirm.Rows.Add(dr);
                //DataRow drSecondConfirm = dtJobConfirm.NewRow();
                //drSecondConfirm["ConfirmType"] = (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers;
                //drSecondConfirm["MeId"] = txtMeId2.Text;
                //drSecondConfirm["Name"] = lblMeName2.Text;
                //drSecondConfirm["MFNo"] = lblMeMfNo2.Text;
                //if (Session["JobFileURL"] != null)
                //    drSecondConfirm["FileURL"] = Session["JobFileURL"];
                //dtJobConfirm.Rows.Add(drSecondConfirm);
                #endregion

            }
            else if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.GovCom)
            {
                #region
                DataRow dr = dtJobConfirm.NewRow();

                dr["DateFrom"] = txtDateFrom.Text;
                dr["DateTo"] = txtDateTo.Text;
                dr["Position"] = txtPosition.Text;

                dr["ConfirmTypeId"] = (int)TSP.DataManager.DocumentJobConfirmType.GovCom;
                dr["ConfirmTypeName"] = cmbConfirmType.Text;
                dr["Name"] = txtOfficeName.Text;
                dr["MFNo"] = txtOfficeMfNo.Text;
                dr["Description"] = txtDescription.Text;
                if (Session["DocUpgradeJobFileURL"] != null)
                {
                    dr["FileURL"] = Session["DocUpgradeJobFileURL"].ToString();
                    Session.Remove("DocUpgradeJobFileURL");
                }
                dtJobConfirm.Rows.Add(dr);
                #endregion

            }
            else if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
            {
                #region
                DataRow dr = dtJobConfirm.NewRow();

                dr["DateFrom"] = txtDateFrom.Text;
                dr["DateTo"] = txtDateTo.Text;
                dr["Position"] = txtPosition.Text;

                dr["ConfirmTypeId"] = (int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv;
                dr["ConfirmTypeName"] = cmbConfirmType.Text;
                if (Utility.IsDBNullOrNullValue(ComboProvince.Text))
                {
                    GrdvJobCon.JSProperties["cpMessage"] = "استان را انتخاب نمایید";
                    return false;
                }
                dr["Name"] = ComboProvince.Text;
                if (Session["DocUpgradeJobFileURL"] != null)
                {
                    dr["FileURL"] = Session["DocUpgradeJobFileURL"].ToString();
                    Session.Remove("DocUpgradeJobFileURL");
                }
                dtJobConfirm.Rows.Add(dr);
                #endregion
            }
            Session["WizardUpgradeJobConfirm"] = dtJobConfirm;
            GrdvJobCon.DataSource = dtJobConfirm;
            GrdvJobCon.DataBind();
            GrdvJobCon.JSProperties["cpSaveComplete"] = "1";

            btnJobRefresh_Click(this, new EventArgs());
            return true;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GrdvJobCon.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است";
            return false;
        }

    }


    private bool CheckJobDate(string DateFrom, string DateTo)
    {
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        DataTable dtJobConfirm = DocMemberFileJobConfirmationManager.SelctActiveJob(Utility.GetCurrentUser_MeId());
        for (int i = 0; i < dtJobConfirm.Rows.Count; i++)
        {
            if (string.Compare(DateFrom, dtJobConfirm.Rows[i]["ToDate"].ToString()) < 0 && string.Compare(DateTo, dtJobConfirm.Rows[i]["FromDate"].ToString()) > 0)
            {
                SetMessage("بازه تاریخی انتخاب شده با سوابق کاری ثبت شده در درخواست های تایید شما همپوشانی دارد");
                return false;
            }
        }
        return true;
    }

    private Boolean CheckTotalDate()
    {

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        System.Collections.ArrayList Result = DocMemberFileManager.CheckUpgradeConditionsDateAndPeriodsForUpgrade(Utility.GetCurrentUser_MeId(),Utility.GetDateOfToday());
        if (!Convert.ToBoolean(Result[0]))
        {
            SetMessage(Result[1].ToString());
            return false;
        }
        int CountNeeded;
        System.Collections.ArrayList ArrayMonth = CountOfMonthNeeded();
        if (string.IsNullOrWhiteSpace(Result[2].ToString()))
        {
            SetMessage("با توجه به پایه های شما ، پایه های قابل ارتقا در سیستم تعریف نشده است.لطفا به واحد فناوری و اطلاعات سازمان تماس بگیرید");
            return false;
        }
        string[] listUpGrdId = (Result[2].ToString().Remove(Result[2].ToString().Length - 1, 1)).Split(';');
        if (listUpGrdId.Contains(Convert.ToInt16(TSP.DataManager.AcceptedUpGrade.TwoToOne).ToString()))
            CountNeeded = Convert.ToInt32((ArrayMonth[1]));
        else
            CountNeeded = Convert.ToInt32((ArrayMonth[0]));
        int CountPreviouseJobs = 0;

        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        DataTable dtJobConfirm = DocMemberFileJobConfirmationManager.SelctActiveJob(Utility.GetCurrentUser_MeId());
        for (int i = 0; i < dtJobConfirm.Rows.Count; i++)
        {
            CountPreviouseJobs += Utility.Date.TotalMonthsBetween2PersianDates(dtJobConfirm.Rows[i]["FromDate"].ToString(), dtJobConfirm.Rows[i]["ToDate"].ToString());
        }
        if (CountPreviouseJobs < CountNeeded)
        {
            if (Session["WizardUpgradeJobConfirm"] == null)
            {
                SetMessage("اطلاعات سابقه کار ثبت نشده است یا مدت اعتبار صفحه به پایان رسیده است");
                return false;
            }
            //   DataTable dtjobs = ((DataTable)Session["WizardUpgradeJobConfirm"]);
            int Months = 0;
            DataView dv = ((DataTable)Session["WizardUpgradeJobConfirm"]).DefaultView;
            dv.Sort = "DateFrom desc";
            string PreSubmitDate = "";
            DataTable dtjobs = dv.ToTable();

            for (int i = 0; i < dtjobs.Rows.Count; i++)
            {
                //if (int.Parse(dtjobs.Rows[i]["ConfirmTypeId"].ToString()) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers && PreSubmitDate != dtjobs.Rows[i]["DateFrom"].ToString())
                if (PreSubmitDate != dtjobs.Rows[i]["DateFrom"].ToString())
                {
                    PreSubmitDate = dtjobs.Rows[i]["DateFrom"].ToString();

                    Months += Utility.Date.TotalMonthsBetween2PersianDates(dtjobs.Rows[i]["DateFrom"].ToString(), dtjobs.Rows[i]["DateTo"].ToString());

                }
            }
            if (CountPreviouseJobs + Months < CountNeeded)
            {
                SetMessage("مجموع بازه های زمانی ثبت شده با سابقه کارهای ارائه شده در درخواست های قبل کمتر از تعداد سال مورد نیاز شما جهت ارتقا پایه می باشد.");
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 7:3==>2 اگر رشته کارشناسی و کارشناسی ارشد یکی باشد،یک سال تخفیف داردو یا کارشناسی و ارشد و دکتری یکی باشد دو سال تخفیف دارد
    /// 12:2==>1 اگر رشته کارشناسی و کارشناسی ارشد یکی باشد،یک سال تخفیف داردو یا کارشناسی و ارشد و دکتری یکی باشد دو سال تخفیف دارد
    /// </summary>
    /// <returns>ArrayList[0] :3to2 ArrayList[1]:2To1</returns>
    private System.Collections.ArrayList CountOfMonthNeeded()
    {
        System.Collections.ArrayList Result = new ArrayList(2);
        int Count3To2 = 84;
        int Count2To1 = 144;
        Result.Add(Count3To2);
        Result.Add(Count2To1);
        Boolean BachloreAndMaster = false;
        Boolean MasterAndPHD = false;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtMasterLicence = MemberLicenceManager.SelectActiveLicenceByLicenceType(Utility.GetCurrentUser_MeId(), 1, -1);
        for (int i = 0; i < dtMasterLicence.Rows.Count; i++)
        {
            if (!BachloreAndMaster)
            {
                DataTable dtBachelorLicence = MemberLicenceManager.SelectActiveLicenceByLicenceType(Utility.GetCurrentUser_MeId(), 0, Convert.ToInt32(dtMasterLicence.Rows[i]["MjParentId"]));
                if (dtBachelorLicence.Rows.Count > 0)
                    BachloreAndMaster = true;
            }
            if (!MasterAndPHD)
            {
                DataTable dtPHDLicence = MemberLicenceManager.SelectActiveLicenceByLicenceType(Utility.GetCurrentUser_MeId(), 2, Convert.ToInt32(dtMasterLicence.Rows[i]["MjParentId"]));
                if (dtPHDLicence.Rows.Count > 0)
                    MasterAndPHD = true;
            }
        }
        if (BachloreAndMaster)
        {
            Count3To2 = Count3To2 - 12;
            Count2To1 = Count2To1 - 12;
        }
        if (MasterAndPHD)
        {
            Count3To2 = Count3To2 - 24;
            Count2To1 = Count2To1 - 24;
        }
        Result[0] = Count3To2;
        Result[1] = Count2To1;
        return Result;
    }
    #endregion
}