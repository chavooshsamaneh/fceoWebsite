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

public partial class Members_Documents_WizardDocJobConfirm : System.Web.UI.Page
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
            Session["JobFileURL"] = null;
            Session["JobGrdURL"] = null;
            SetMenueImage();
            SetHelpAddress();
            CreateJobConfirmDataTable();
            // SetVisible();
        }

        if (Session["WizardDocJobConfirm"] != null)
        {
            dtJobConfirm = (DataTable)Session["WizardDocJobConfirm"];
            GrdvJobCon.DataSource = dtJobConfirm;
            GrdvJobCon.DataBind();
        }

        if (Session["JobFileURL"] != null)
        {
            ImageConf.ImageUrl = Session["JobFileURL"].ToString();
        }
        if (Session["JobGrdURL"] != null)
        {
            ImageGrd.ImageUrl = Session["JobGrdURL"].ToString();
        }

    }

    protected void btnJob_Click(object sender, EventArgs e)
    {
        bool check = false;

        if (GrdvJobCon.VisibleRowCount > 0)
        {
            GrdvJobCon.DataSource = (DataTable)Session["WizardDocJobConfirm"];
            GrdvJobCon.DataBind();

            for (int i = 0; i < GrdvJobCon.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)GrdvJobCon.GetRow(i);
                if (dr["MeId"].ToString() == txtMeId1.Text && int.Parse(dr["ConfirmTypeId"].ToString()) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
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

        dtJobConfirm = (DataTable)Session["WizardDocJobConfirm"];

        dtJobConfirm.Rows.Find(e.Keys[0]).Delete();

        Session["WizardDocJobConfirm"] = dtJobConfirm;
        GrdvJobCon.DataSource = (DataTable)Session["WizardDocJobConfirm"];
        GrdvJobCon.DataBind();
        dtJobConfirm = (DataTable)Session["WizardDocJobConfirm"];

        if (dtJobConfirm.Rows.Count == 0)
            MenuSteps.Items.FindByName("Job").Image.Url = "";

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
        if (Session["WizardDocJobConfirm"] != null && ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count > 0)
        {
            for (int i = 0; i < ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count; i++)
            {
                if (int.Parse(((DataTable)Session["WizardDocJobConfirm"]).Rows[i]["ConfirmTypeId"].ToString()) == (int)TSP.DataManager.DocumentJobConfirmType.TwoMembers)
                    towMember++;
            }
            if (towMember == 0 || towMember >= 2)
            {
                Response.Redirect("WizardDocSummary.aspx");
            }
            else
            {
                SetMessage("حداقل دو نفر عضو حقیقی باید به عنوان تایید کننده معرفی شوند");
            }

        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardAccConfirm.aspx");
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
        int MeId=0;
        lblMeName1.Text = "";
        lblMeFileNo1.Text = "";
        lblLicenseDate.Text = "";
        SetJobConfirmVisible((int)TSP.DataManager.DocumentJobConfirmType.TwoMembers);
        HiddenFieldJobConfirm["Conf1"] = 0;
   
        if (string.IsNullOrEmpty(txtMeId1.Text))
        {
            SetMessage("کد عضویت تایید کننده را وارد نمایید");
            return;
        }
        int.TryParse(txtMeId1.Text,out MeId);

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
               DataTable dtDocMe= DocMemberFileManager.SelectMainRequest(MeId, 0);
               if (dtDocMe.Rows.Count > 0)
               {
                   if (Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["TaskCode"]) 
                       ||( Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument ))
                       //&& Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) != (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess))
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
        if (Session["WizardDocOath"] != null && (Boolean)Session["WizardDocOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["HseFileURL"] != null || (Session["ImgTaxOfficeLetter"] != null || CbIAgree()))
        {
            MenuSteps.Items.FindByName("AccConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("AccConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("AccConfirm").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocExam"] != null && ((DataTable)Session["WizardDocExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        //if (Session["WizardDocMajor"] != null && ((DataTable)Session["WizardDocMajor"]).Rows.Count > 0)
        //{
        //    MenuSteps.Items.FindByName("Major").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("Major").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("Major").Image.Height = Unit.Pixel(15);
        //}
        if (Session["WizardDocJob"] != null && ((DataTable)Session["WizardDocJob"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocJobConfirm"] != null && ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }
        //if (Session["WizardDocResposblity"] != null && ((DataTable)Session["WizardDocResposblity"]).Rows.Count > 0)
        //{
        //    MenuSteps.Items.FindByName("DocRes").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("DocRes").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("DocRes").Image.Height = Unit.Pixel(15);
        //}
        //if (Session["WizardDocPeriods"] != null && (Boolean)Session["WizardDocPeriods"] == true)
        //{
        //    MenuSteps.Items.FindByName("Periods").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("Periods").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("Periods").Image.Height = Unit.Pixel(15);
        //}
        if (Session["WizardDocSummary"] != null && (Boolean)Session["WizardDocSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
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
        if (Convert.ToInt32( cmbConfirmType.Value) ==(int)TSP.DataManager.DocumentJobConfirmType.TwoMembersOtherPrv)
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
        if (Session["WizardDocExam"] == null && Session["WizardDocSummary"] == null && Session["WizardDocOath"] == null
        && Session["ACCFileURL"] == null && Session["WizardDocJobConfirm"] == null
            //&& Session["WizardDocMajor"] == null && Session["WizardDocJob"] == null
            // && Session["WizardDocResposblity"] == null && Session["WizardDocPeriods"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocOath"] == null)
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
                ret = "MeId_" + Utility.GetCurrentUser_MeId().ToString()+"_" + Path.GetRandomFileName() + ImageType.Extension;


            } while ((id == "flpConfAttach" && File.Exists(MapPath("~/image/DocMeFile/JobConfirm/") + ret) == true)
            || (id == "flpGrdAttach" && File.Exists(MapPath("~/image/DocMeFile/OfficeGrade/") + ret) == true)
              );

            //Session["JobFileURL"] = "~/Image/Temp/" + ret;

            if (id == "flpConfAttach")
            {
                Session["JobFileURL"] = "~/Image/DocMeFile/JobConfirm/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/JobConfirm/") + ret;
            }
            else if (id == "flpGrdAttach")
            {
                Session["JobGrdURL"] = "~/Image/DocMeFile/OfficeGrade/" + ret;
                tempFileName = MapPath("~/Image/DocMeFile/OfficeGrade/") + ret;
            }

            uploadedFile.SaveAs(tempFileName, true);

        }
        return ret;
    }

    private void CreateJobConfirmDataTable()
    {
        if (Session["WizardDocJobConfirm"] == null)
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
            //dtJobConfirm.Columns.Add("Position");
            //dtJobConfirm.Columns.Add("FromDate");
            //dtJobConfirm.Columns.Add("ToDate");
            dtJobConfirm.Columns.Add("Position");
            Session["WizardDocJobConfirm"] = dtJobConfirm;
        }
    }

    //private void FillJobConfirmInfo(DataTable dtJobConfirm)
    //{
    //    if (dtJobConfirm.Rows.Count == 0)
    //        return;
    //    if (dtJobConfirm.Rows.Count == 1)
    //    {
    //        cmbConfirmType.SelectedIndex = cmbConfirmType.Items.FindByValue(dtJobConfirm.Rows[0]["ConfirmType"].ToString()).Index;
    //        txtDescription.Text = dtJobConfirm.Rows[0]["Description"].ToString();
    //        txtOfficeName.Text = dtJobConfirm.Rows[0]["Name"].ToString();
    //        txtOfficeMfNo.Text = dtJobConfirm.Rows[0]["MFNo"].ToString();
    //        Session["JobFileURL"] = dtJobConfirm.Rows[0]["FileURL"].ToString();
    //        HpflpConfAttach.NavigateUrl = dtJobConfirm.Rows[0]["FileURL"].ToString();
    //        HiddenFieldJobConfirm["name"] = 1;
    //    }
    //    else if (dtJobConfirm.Rows.Count == 2)
    //    {
    //        cmbConfirmType.SelectedIndex = cmbConfirmType.Items.FindByValue(dtJobConfirm.Rows[0]["ConfirmType"].ToString()).Index;
    //        txtMeId1.Text = dtJobConfirm.Rows[0]["MeId"].ToString();
    //        lblMeName1.Text = dtJobConfirm.Rows[0]["Name"].ToString();
    //        lblMeFileNo1.Text = dtJobConfirm.Rows[0]["MFNo"].ToString();
    //        HiddenFieldJobConfirm["Conf1"] = 1;
    //        //txtMeId2.Text = dtJobConfirm.Rows[1]["MeId"].ToString();
    //        //lblMeName2.Text = dtJobConfirm.Rows[1]["Name"].ToString();
    //        //lblMeMfNo2.Text = dtJobConfirm.Rows[1]["MFNo"].ToString();
    //        HiddenFieldJobConfirm["Conf2"] = 1;


    //        Session["JobFileURL"] = dtJobConfirm.Rows[0]["FileURL"].ToString();
    //        HpflpConfAttach.NavigateUrl = dtJobConfirm.Rows[0]["FileURL"].ToString();
    //        HiddenFieldJobConfirm["name"] = 1;
    //    }
    //    SetJobConfirmVisible(Convert.ToInt32(dtJobConfirm.Rows[0]["ConfirmType"]));
    //}

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
        if (Session["WizardDocJobConfirm"] == null)
        {
            SetMessage("اطلاعات تایید کننده سابقه کار تکمیل نشده است");
            return false;
        }
        if (Session["JobFileURL"] == null)
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
            if (Convert.ToInt32(cmbConfirmType.Value) == (int)TSP.DataManager.DocumentJobConfirmType.Office)
            {
                #region
                if (Session["JobGrdURL"] == null)
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
                if (Session["JobFileURL"] != null)
                {
                    dr["FileURL"] = Session["JobFileURL"].ToString();
                    Session.Remove("JobFileURL");
                    Session["JobFileURL"] = null;
                }
                if (Session["JobGrdURL"] != null)
                {
                    dr["GradeURL"] = Session["JobGrdURL"].ToString();
                    Session.Remove("JobGrdURL");
                    Session["JobGrdURL"] = null;
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
                dr["DocExpired"] = HiddenFieldJobConfirm["DocExpired"] ;
                if (Session["JobFileURL"] != null)
                {
                    dr["FileURL"] = Session["JobFileURL"].ToString();
                    Session.Remove("JobFileURL");
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
                if (Session["JobFileURL"] != null)
                {
                    dr["FileURL"] = Session["JobFileURL"].ToString();
                    Session.Remove("JobFileURL");
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
                dr["Name"] =ComboProvince.Text;
                if (Session["JobFileURL"] != null)
                {
                    dr["FileURL"] = Session["JobFileURL"].ToString();
                    Session.Remove("JobFileURL");
                }
                dtJobConfirm.Rows.Add(dr);
                #endregion
            }
            Session["WizardDocJobConfirm"] = dtJobConfirm;
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

    private Boolean CbIAgree()
    {
        if (Session["chbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["chbIAgree"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #endregion
}