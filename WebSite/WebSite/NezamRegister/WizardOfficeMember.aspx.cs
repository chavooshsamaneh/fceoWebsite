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
public partial class NezamRegister_WizardOfficeMember : System.Web.UI.Page
{
    DataTable dtOfMembers = new DataTable();
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }
        //lblImg.Text = "تصویر انتخاب شده باید " + Utility.VerRes + "*" + Utility.HorRes + "باشد";
        lblImgWarning.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";

        #region Set Menue
        ASPxMenu1.Items.FindByName("Member").Selected = true;

        if (Session["OfficeMembership"] != null && (Boolean)Session["OfficeMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Office"] != null && ((DataTable)Session["Office"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Office").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Office").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Office").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfAgent"] != null && ((DataTable)Session["TblOfAgent"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Agent").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Agent").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Agent").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfLetter"] != null && ((DataTable)Session["TblOfLetter"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Letter").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Letter").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Letter").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMember"] != null && ((DataTable)Session["TblOfMember"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfJob"] != null && ((DataTable)Session["TblOfJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["OfficeSummary"] != null && (Boolean)Session["OfficeSummary"] == true)
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
            Session["MemberEmza"] = null;
            Session["MemberImage"] = null;
            ODBPosition.FilterParameters[0].DefaultValue = "0";

            if (Cache["CachePersonMember"] == null)
                Cache["CachePersonMember"] = new object();

            #region Set DataTable
            if (Session["TblOfMember"] == null)
            {
                dtOfMembers.Columns.Add("ID");
                dtOfMembers.Columns.Add("MeId");
                dtOfMembers.Columns.Add("OtpCode");
                dtOfMembers.Columns.Add("HasEfficientGradeCode");
                dtOfMembers.Columns.Add("HasEfficientGradeName");
                dtOfMembers.Columns.Add("FirstName");
                dtOfMembers.Columns.Add("LastName");
                dtOfMembers.Columns.Add("FatherName");
                dtOfMembers.Columns.Add("IdNo");
                dtOfMembers.Columns.Add("SSN");
                dtOfMembers.Columns.Add("BirthDate");
                dtOfMembers.Columns.Add("BirthPlace");
                dtOfMembers.Columns.Add("OthType");
                dtOfMembers.Columns.Add("OthTypeName");
                dtOfMembers.Columns.Add("ImageUrl");
                dtOfMembers.Columns.Add("OfpId");
                dtOfMembers.Columns.Add("OfPosition");
                dtOfMembers.Columns.Add("OfmTypeId");//1:Member 2:Kardan 3:Other 4:Memar
                dtOfMembers.Columns.Add("OfmTypeName");
                dtOfMembers.Columns.Add("HasSignRight", typeof(Boolean));
                dtOfMembers.Columns.Add("SignUrl");
                dtOfMembers.Columns.Add("StartDate");
                dtOfMembers.Columns.Add("FileNo");
                dtOfMembers.Columns.Add("IsFullTime");
                dtOfMembers.Columns.Add("Description");
                dtOfMembers.Columns.Add("Tel");
                dtOfMembers.Columns.Add("MobileNo");
                dtOfMembers.Columns.Add("Address");
                dtOfMembers.Columns.Add("Tel_pre");
                dtOfMembers.Columns.Add("MfId");

                //for new kardan and memar
                dtOfMembers.Columns.Add("MjId");
                dtOfMembers.Columns.Add("MjName");
                dtOfMembers.Columns.Add("FileDate");
                dtOfMembers.Columns.Add("AgentId");
                dtOfMembers.Columns.Add("CitId");
                dtOfMembers.Columns.Add("LicenceImgUrl");

                dtOfMembers.Columns["ID"].AutoIncrement = true;
                dtOfMembers.Columns["ID"].AutoIncrementSeed = 1;
                dtOfMembers.Constraints.Add("PK_ID", dtOfMembers.Columns["ID"], true);

                Session["TblOfMember"] = dtOfMembers;
            }
            else
            {
                dtOfMembers = (DataTable)Session["TblOfMember"];
                GvMembers.DataSource = dtOfMembers;
                GvMembers.DataBind();
            }
            #endregion
            // flp_Emza.Visible = true;
            this.ViewState["btnAdd"] = btnAdd.Enabled;
        }

        if (this.ViewState["btnAdd"] != null)
            this.btnAdd.Enabled = (bool)this.ViewState["btnAdd"];

        Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");

        if (Session["MemberEmza"] != null)
        {
            imgEmza.ImageUrl = "~/Image/Temp/" + Session["MemberEmza"].ToString();

            imgEmza.ClientVisible = true;
        }

        if (Session["MemberImage"] != null)
        {
            imgMember.ImageUrl = "~/Image/Temp/" + Path.GetFileName(Session["MemberImage"].ToString());
        }

    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        if (this.ComboType.SelectedIndex != 0)
            return;
        if (string.IsNullOrEmpty(txtMeNo.Text)) return;
        SetControlVisible(false);

        this.ViewState["btnAdd"] = btnAdd.Enabled = true;

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        try
        {
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                int MeId = int.Parse(txtMeNo.Text);
                MeManager.FindByCode(MeId);
                if (MeManager.Count != 1)
                {
                    ClearForm();
                    // this.btnAdd.ClientEnabled = false;
                    //ClearForm();
                    ComboType.DataBind();
                    ComboType.SelectedIndex = 0;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد عضویت نا معتبر می باشد";
                    return;
                }
                ClearForm();
                txtMeNo.Text = MeId.ToString();
                string LockName = "";
                #region CheckLock
                if (Convert.ToBoolean(MeManager[0]["IsLock"]))
                {
                    LockName = FindLockers(MeId, (int)TSP.DataManager.LockMemberType.Member, 1);

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ";
                    ClearForm();
                    return;
                    //this.ViewState["btnAdd"] = btnAdd.Enabled = false; 
                }
                #endregion
                this.btnAdd.ClientEnabled = true;
                txtAddress.Text = "---";// MeManager[0]["HomeAdr"].ToString();
                txtBirthDate.Text = "1111/01/01";// MeManager[0]["BirhtDate"].ToString();
                txtBirthPlace.Text = "---";// MeManager[0]["BirthPlace"].ToString();
                txtFatherName.Text =  MeManager[0]["FatherName"].ToString();
                txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                txtFileDate.Text = MeManager[0]["FileDate"].ToString();
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                txtIdNo.Text = "---";//  MeManager[0]["IdNo"].ToString();
                txtLastName.Text = MeManager[0]["LastName"].ToString();
                txtMobile.Text = "---";//  MeManager[0]["MobileNo"].ToString();
                txtSSN.Text = "---";// MeManager[0]["SSN"].ToString();
                // txtTel.Text = MeManager[0]["HomeTel"].ToString();

                if (MeManager[0]["HomeTel"].ToString() != "")
                {
                    if (MeManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                    {
                        txtTel_pre.Text = "---";//  MeManager[0]["HomeTel"].ToString().Substring(0, MeManager[0]["HomeTel"].ToString().IndexOf("-"));
                        txtTel.Text = "---";//  MeManager[0]["HomeTel"].ToString().Substring(MeManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MeManager[0]["HomeTel"].ToString().Length - MeManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtTel.Text = "---";//  MeManager[0]["HomeTel"].ToString();
                    }
                }
                if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]))
                    imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();

                HD_img["name"] = 1;

                MeFileManager.SelectLastVersion(MeId, 0);
                if (MeFileManager.Count > 0)
                {
                    int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                    GridViewResponsblity.DataSource = MeFileDetailManager.SelectById(MfId, MeId, 0);
                    GridViewResponsblity.KeyFieldName = "MfdId";
                    GridViewResponsblity.DataBind();
                }

                if (MeManager[0]["MrsId"].ToString() != "1")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "وضعیت این عضو تایید نشده است";
                    this.btnAdd.ClientEnabled = false;
                    ClearForm();
                    return;
                    //this.ViewState["btnAdd"] = btnAdd.Enabled = false;

                }

                if ((bool)MeManager[0]["IsLock"] == true)
                {
                    TSP.DataManager.LockHistoryManager lockHistory = new TSP.DataManager.LockHistoryManager();
                    string lockers = lockHistory.FindLockers(MeId, 0, 1);
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت این عضو توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                    ClearForm();
                    return;
                    //  this.btnAdd.ClientEnabled = false;

                }


            }
            else
            {
                //  ClearForm();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");


        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات عضو مورد نظر رخ داده است";
        }
    }

    protected void txtOtpCode_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtOtpCode.Text)) return;
        TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        try
        {
            if (ComboType.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع عضو را مجدداً انتخاب نماييد";
                return;
            }
            if (string.IsNullOrEmpty(txtOtpCode.Text))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "كد عضويت کانون کاردانها را مجدداً وارد نماييد";
                return;
            }
            string type = ComboType.Value.ToString();

            switch (type)
            {
                case "1":
                    OthPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text, (int)TSP.DataManager.OtherPersonType.Kardan);
                    break;
                case "2":
                    OthPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text, (int)TSP.DataManager.OtherPersonType.Memar);
                    break;
                case "5":
                case "6":
                    HDOtpId.Value = txtOtpCode.Text.Trim();
                    return;
                    break;
            }

            if (OthPersonManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "چنين كد عضويتي وجود ندارد.كد عضويت را مجدداً وارد نماييد";
                ClearForm();
                SetComboType(int.Parse(type));
                return;
            }

            HDOtpId.Value = OthPersonManager[0]["OtpId"].ToString();
            if (Convert.ToBoolean(OthPersonManager[0]["InActive"]))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شخص مورد نظر غير فعال مي باشد";
                SetComboType(int.Parse(type));

                return;
            }
            string LockName = "";
            #region CheckLock
            if (Convert.ToBoolean(OthPersonManager[0]["IsLock"]))
            {
                if (type == "1")
                    LockName = FindLockers(int.Parse(HDOtpId.Value), (int)TSP.DataManager.LockMemberType.Kardan, 1);
                else if (type == "2")
                    LockName = FindLockers(int.Parse(HDOtpId.Value), (int)TSP.DataManager.LockMemberType.Memar, 1);

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.شخص مورد نظر توسط " + LockName + " قفل می باشد ";
                SetComboType(int.Parse(type));

                return;
            }
            #endregion
            txtAddress.Text = OthPersonManager[0]["Address"].ToString();
            txtBirthDate.Text = OthPersonManager[0]["BirthDate"].ToString();
            txtBirthPlace.Text = OthPersonManager[0]["BirthPlace"].ToString();
            txtFatherName.Text = OthPersonManager[0]["FatherName"].ToString();
            txtFileNo.Text = OthPersonManager[0]["FileNo"].ToString();
            txtFileDate.Text = OthPersonManager[0]["FileNoDate"].ToString();
            txtFirstName.Text = OthPersonManager[0]["FirstName"].ToString();
            txtIdNo.Text = OthPersonManager[0]["IdNo"].ToString();
            txtLastName.Text = OthPersonManager[0]["LastName"].ToString();
            txtMobile.Text = OthPersonManager[0]["MobileNo"].ToString();
            txtSSN.Text = OthPersonManager[0]["SSN"].ToString();

            if (OthPersonManager[0]["Tel"].ToString() != "")
            {
                if (OthPersonManager[0]["Tel"].ToString().IndexOf("-") > 0)
                {
                    txtTel_pre.Text = OthPersonManager[0]["Tel"].ToString().Substring(0, OthPersonManager[0]["Tel"].ToString().IndexOf("-"));
                    txtTel.Text = OthPersonManager[0]["Tel"].ToString().Substring(OthPersonManager[0]["Tel"].ToString().IndexOf("-") + 1, OthPersonManager[0]["Tel"].ToString().Length - OthPersonManager[0]["Tel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtTel.Text = OthPersonManager[0]["Tel"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(OthPersonManager[0]["ImageUrl"].ToString()))
                imgMember.ImageUrl = OthPersonManager[0]["ImageUrl"].ToString();

            GridViewResponsblity.DataSource = GradeManager.SelectGradeByOtpId(Convert.ToInt32(HDOtpId.Value));
            GridViewResponsblity.KeyFieldName = "MGId";
            GridViewResponsblity.DataBind();

            SetComboType(int.Parse(type));
            HD_img["name"] = 1;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطايي در بازخواني اطلاعات رخ داده است";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int MeOtpId = -1;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

        DocMemberFileManager.ClearBeforeFill = true;

        string pathAx = "", fileUrl_axK = "", fileUrl_emK = "", fileUrl_fileK = "";

        try
        {
            int ComboTypeSelectedValue = 0;
            int time = 1;
            int MemberFileId = -1;
            int MeId = -1;
            string ExpireDate = "";

            if (ComboTime.Value != null)
                time = int.Parse(ComboTime.Value.ToString());
            #region Check Condition
            if (Session["MemberImage"] == null && HD_img["name"].ToString() != "1")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تصویر را انتخاب نمایید";
                return;
            }

            if (chbHaghEmza.Checked == true)
                if (Session["MemberEmza"] == null)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تصویر امضا را انتخاب نمایید";
                    return;
                }
            #endregion

            int OfmTypeId = -1;
            string OfmTypeName = "";
            int OtpType = -1;
            string OtpTypeName = "";

            if (ComboType.Value == null)
            {
                ShowMessage("نوع عضو را انتخاب نمایید");
                return;
            }
            if (cmbHasEfficientGrade.Value == null)
            {
              
                ShowMessage("وضعیت امتیاز عضو را انتخاب نمایید");
                return;
            }
            ComboTypeSelectedValue = int.Parse(ComboType.Value.ToString());
            //OfmTypeId = int.Parse(ComboType.Value.ToString());
            OfmTypeName = ComboType.SelectedItem.Text;
            OtpTypeName = ComboType.SelectedItem.Text;

            switch (ComboTypeSelectedValue)
            {
                case 0:
                    #region Members
                    if (string.IsNullOrEmpty(txtMeNo.Text))
                    {
                        ShowMessage("کد عضویت را مجدداً وارد نمایید");
                        return;
                    }
                    MeOtpId = int.Parse(txtMeNo.Text);
                    SetControlVisible(false);
                    this.txtMeNo.ClientVisible = true;
                    this.txtOtpCode.ClientVisible = false;
                    MeId = int.Parse(txtMeNo.Text);
                    #region CheckMemberFile
                    //DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                    //if (dtMeFile.Rows.Count > 0)
                    //{
                    //    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                    //    ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();

                    //    if (Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) || dtMeFile.Rows[0]["IsConfirm"].ToString() != "1")
                    //    {
                    //        SetComboType(Value);
                    //        this.DivReport.Visible = true;
                    //        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.";
                    //        return;
                    //    }

                    //    if (!string.IsNullOrEmpty(ExpireDate))
                    //    {
                    //        if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                    //        {
                    //            SetComboType(Value);
                    //            this.DivReport.Visible = true;
                    //            this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                    //            return;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                    //    return;
                    //}
                    #endregion
                    DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
                    if (DocMemberFileManager.Count > 0 && Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int) TSP.DataManager.DocumentOfMemberRequestType.InActive)
                    {
                        //!!!!!!!!11SetComboType(ComboTypeSelectedValue);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.";
                        return;
                    }

                    OfmTypeId = (int)TSP.DataManager.OfficeMemberType.Member;
                    SetComboType(ComboTypeSelectedValue);
                    #endregion
                    break;
                case 1:
                    #region Kardan
                    //if (!string.IsNullOrEmpty(HDOtpId.Value))
                    //    MeOtpId = int.Parse(HDOtpId.Value);
                    if (string.IsNullOrEmpty(txtOtpCode.Text))
                    {
                        SetComboType(ComboTypeSelectedValue);
                        SetControlVisible(true);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد کاردان را مجدداً وارد نمایید";
                        return;
                    }
                    SetControlVisible(true);
                    //if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر به عنوان مدیر عامل شرکت وجود ندارد";
                    //    return;
                    //}

                    #region CheckMemberFile
                    //TechnicianRequestManager.FindLastVerion(MeOtpId, 1);
                    //if (TechnicianRequestManager.Count > 0)
                    //{
                    //    MemberFileId = Convert.ToInt32(TechnicianRequestManager[0]["TnReId"]);
                    //    ExpireDate = TechnicianRequestManager[0]["FileDate"].ToString();

                    //    if (!string.IsNullOrEmpty(ExpireDate))
                    //    {
                    //        if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                    //        {
                    //            SetComboType(Value);
                    //            this.DivReport.Visible = true;
                    //            this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                    //            return;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                    //    return;
                    //}
                    #endregion

                    OtherPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Kardan);
                    if (OtherPersonManager.Count <= 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد وارد شده جهت عضویت کانون کاردانها در سیستم ثبت نشده است.جهت ثبت این کاردان از لیست نوع عضو گزینه کاردان جدید را انتخاب کنید";
                        return;
                    }
                    MeOtpId = int.Parse(HDOtpId.Value);
                    OtpType = (int)TSP.DataManager.OtherPersonType.Kardan;
                    OfmTypeId = (int)TSP.DataManager.OfficeMemberType.Kardan;
                    #endregion
                    break;
                case 2:
                    #region Memar
                    //if (!string.IsNullOrEmpty(HDOtpId.Value))
                    //    MeOtpId = int.Parse(HDOtpId.Value);
                    if (string.IsNullOrEmpty(txtOtpCode.Text))
                    {
                        SetControlVisible(true);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد معمار را مجدداً وارد نمایید";
                        return;
                    }
                    SetControlVisible(true);
                    //if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر به عنوان مدیر عامل شرکت وجود ندارد";
                    //    return;
                    //}

                    #region CheckMemberFile
                    //TechnicianRequestManager.FindLastVerion(MeOtpId, 1);
                    //if (TechnicianRequestManager.Count > 0)
                    //{
                    //    MemberFileId = Convert.ToInt32(TechnicianRequestManager[0]["TnReId"]);
                    //    ExpireDate = TechnicianRequestManager[0]["FileDate"].ToString();

                    //    if (!string.IsNullOrEmpty(ExpireDate))
                    //    {
                    //        if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                    //        {
                    //            SetComboType(Value);
                    //            this.DivReport.Visible = true;
                    //            this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                    //            return;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                    //    return;
                    //}
                    #endregion

                    OtherPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Memar);
                    if (OtherPersonManager.Count <= 0)
                    {
                        SetControlVisible(true);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد وارد شده جهت عضویت معماران تجربی در سیستم ثبت نشده است.جهت ثبت این معمار از لیست نوع عضو گزینه معمار جدید را انتخاب کنید";
                        return;
                    }
                    SetControlVisible(true);
                    MeOtpId = int.Parse(HDOtpId.Value);
                    OtpType = (int)TSP.DataManager.OtherPersonType.Memar;
                    OfmTypeId = (int)TSP.DataManager.OfficeMemberType.Memar;
                    #endregion
                    break;
                case 3:
                    #region Other Person
                    SetControlVisible(true);
                    //if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                    //{
                    //    SetComboType(Value);
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر به عنوان مدیر عامل شرکت وجود ندارد";
                    //    return;

                    //}
                    OtpType = (int)TSP.DataManager.OtherPersonType.OtherPerson;
                    OfmTypeId = (int)TSP.DataManager.OfficeMemberType.Otherperson;
                    #endregion
                    break;
                case 5:
                    #region New Kardan
                    OtherPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Kardan);
                    if (OtherPersonManager.Count > 0)
                    {
                        SetControlVisible(true);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد کانون کاردانهای وارد شده بیش از این در سیستم ثبت شده است. جهت ثبت این کاردان از لیست نوع عضو گزینه کاردان را انتخاب کنید";
                        return;
                    }
                    SetControlVisible(true);
                    OtpType = (int)TSP.DataManager.OtherPersonType.Kardan;
                    OfmTypeId = 5;
                    #endregion
                    break;
                case 6:
                    #region New Memar
                    OtherPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Memar);
                    if (OtherPersonManager.Count > 0)
                    {
                        SetControlVisible(true);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد کانون کاردانهای وارد شده بیش از این در سیستم ثبت شده است. جهت ثبت این معمار از لیست نوع عضو گزینه معمار تجربی را انتخاب کنید";
                        return;
                    }
                    SetControlVisible(true);
                    OtpType = (int)TSP.DataManager.OtherPersonType.Memar;
                    OfmTypeId = 6;
                    #endregion
                    break;
            }


            //bool check = false;
            if (GvMembers.VisibleRowCount > 0)
            {
                for (int i = 0; i < GvMembers.VisibleRowCount; i++)
                {
                    GvMembers.DataSource = (DataTable)Session["TblOfMember"];
                    GvMembers.DataBind();

                    DataRowView dr = (DataRowView)GvMembers.GetRow(i);
                    if (dr["FirstName"].ToString() == txtFirstName.Text && dr["LastName"].ToString() == txtLastName.Text && dr["SSN"].ToString() == txtSSN.Text)
                    {
                        SetComboType(ComboTypeSelectedValue);
                        //check = true;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;

                    }
                    if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                    {
                        if (Convert.ToInt32(dr["OfpId"]) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(dr["OfpId"]) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                        {
                            SetComboType(ComboTypeSelectedValue);

                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "برای شرکت مورد نظر مدیر عامل ثبت شده است";
                            return;
                        }

                    }
                }
            }

            if (Session["MemberImage"] != null)
            {

                pathAx = Server.MapPath("~/Image/Temp/");

                imgMember.ImageUrl = "~/Image/Temp/" + Path.GetFileName(Session["MemberImage"].ToString());

                fileUrl_axK = "~/Image/Temp/" + Path.GetFileName(Session["MemberImage"].ToString());
            }

            if (Session["MemberEmza"] != null)
            {
                imgEmza.ImageUrl = "~/Image/Temp/" + Session["MemberEmza"].ToString();
                fileUrl_emK = "~/Image/Temp/" + Session["MemberEmza"].ToString();
                imgEmza.ImageUrl = "~/Image/Temp/" + Session["MemberEmza"].ToString();
            }

            if (Session["AttachCommit"] != null)
            {
                HpCommit.NavigateUrl = "~/Image/Temp/" + Session["AttachCommit"].ToString();
                fileUrl_fileK = "~/Image/Temp/" + Session["AttachCommit"].ToString();
                HpCommit.NavigateUrl = "~/Image/Temp/" + Session["AttachCommit"].ToString();
            }

            if (MemberFileId == -1 && ComboTypeSelectedValue == 0)
            {
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count == 1)
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            }

            ((DataTable)Session["TblOfMember"]).Rows.Add(new object[]{null,MeOtpId, txtOtpCode.Text,cmbHasEfficientGrade.Value,cmbHasEfficientGrade.Text, txtFirstName.Text, txtLastName.Text,txtFatherName.Text
                         , txtIdNo.Text, txtSSN.Text, txtBirthDate.Text, txtBirthPlace.Text,
                         OtpType, OtpTypeName, fileUrl_axK.ToString(), drdPosition.Value, drdPosition.SelectedItem.Text, OfmTypeId, OfmTypeName, chbHaghEmza.Checked, 
                         fileUrl_emK.ToString(), txtStartDate.Text, txtFileNo.Text, time,
                         txtDesc.Text,txtTel.Text,txtMobile.Text ,txtAddress.Text,txtTel_pre.Text,MemberFileId ,
                         ComboMjId.Value ,txtMjName.Text.Trim() ,  txtFileDate.Text , CmbAgent.Value , CmbCity.Value , fileUrl_fileK});

            #region Next Task Of Adding
            ClearForm();
            ComboType.DataBind();
            ComboType.SelectedIndex = 0;
            flp_Image.ClientVisible = false;
            imgMember.ClientVisible = false;
            lblMeNo.ClientVisible = true;
            txtMeNo.ClientVisible = true;
            lblOtpCode.ClientVisible = false;
            txtOtpCode.ClientVisible = false;
            GvMembers.DataSource = (DataTable)Session["TblOfMember"];
            GvMembers.DataBind();
            dtOfMembers = (DataTable)Session["TblOfMember"];
            PnlKardanMemarInfo.ClientVisible = false;
            if (dtOfMembers.Rows.Count > 0)
            {
                ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
                ASPxMenu1.Items.FindByName("Member").Image.Width = 15;
                ASPxMenu1.Items.FindByName("Member").Image.Height = 15;
            }
            #endregion
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
        }
    }

    protected void GvMembers_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //GvMembers.DataSource = (DataTable)Session["TblOfMember"];
        //GvMembers.DataBind();

        //int Id = -1;
        //if (GvMembers.FocusedRowIndex > -1)
        //{
        //    Id = GvMembers.FocusedRowIndex;
        //    //DataRow row = GrdvJob.GetDataRow(GrdvJob.FocusedRowIndex);
        //    //Id = (int)row["JhId"];
        //}
        //if (Id == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
        //    return;

        //}
        //  else
        // {

        dtOfMembers = (DataTable)Session["TblOfMember"];
        dtOfMembers.Rows.Find(e.Keys[0]).Delete();
        Session["TblOfMember"] = dtOfMembers;
        GvMembers.DataSource = (DataTable)Session["TblOfMember"];
        GvMembers.DataBind();
        dtOfMembers = (DataTable)Session["TblOfMember"];

        if (((DataTable)Session["TblOfMember"]).Rows.Count == 0 && dtOfMembers.Rows.Count == 0)
            ASPxMenu1.Items.FindByName("Member").Image.Url = "";

        ClearForm();
        ComboType.DataBind();
        ComboType.SelectedIndex = 0;
        // }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeAgent.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (GvMembers.VisibleRowCount == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت اعضای شرکت الزامی می باشد";
            return;
        }
        if (Func_IsManager() == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت مدیر عامل شرکت الزامی می باشد";
            return;
        }
        else if (Func_IsBoard() == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به جز مدیر عامل که بایستی عضو هیئت مدیره باشد،حداقل یک دیگر از اعضای شرکت دارای یکی از سمت های : رئیس هیئت مدیره و یا نائب رئیس هیئت مدیره و یا عضو هیئت مدیره باشد.";
            return;
        }
        else if (Func_MemberTypeCount() == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "کمینه دو نفر از اعضای شرکت بایستی عضو سازمان نظام مهندسی یا نظام کاردان ها باشند. ";
            return;
        }
        Response.Redirect("WizardOfficeJob.aspx");

    }

    protected void flp_Image_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void flpCommit_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageLicense(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }

    }

    protected void flp_Emza_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveEmza(e.UploadedFile);

        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void GridViewResponsblity_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Clear")
        {
            GridViewResponsblity.DataSource = null;
            GridViewResponsblity.DataBind();
        }
    }

    protected void btnRemoveFile_Click(object sender, EventArgs e)
    {
        Session["MemberEmza"] = null;
        imgEmza.ImageUrl = "";
    }
    #endregion

    #region Methods9
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private bool Func_IsManager()
    {
        if (Session["TblOfMember"] == null)
            return false;
        dtOfMembers = (DataTable)Session["TblOfMember"];
        for (int i = 0; i < dtOfMembers.Rows.Count; i++)
        {
            if (Convert.ToInt32(dtOfMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(dtOfMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                return true;
        }
        return false;
    }

    private bool Func_IsBoard()
    {
        dtOfMembers = (DataTable)Session["TblOfMember"];
        int Count = 0;
        for (int i = 0; i < dtOfMembers.Rows.Count; i++)
        {
            if (Convert.ToInt32(dtOfMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.Board
                || Convert.ToInt32(dtOfMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.ViceChairmanOfTheBoard
                                || Convert.ToInt32(dtOfMembers.Rows[i]["OfpId"]) == (int)TSP.DataManager.OfficePosition.ChairmanOfTheBoard)
                Count++;
        }
        if (Count >= 1)
            return true;
        return false;
    }

    private bool Func_MemberTypeCount()
    {
        dtOfMembers = (DataTable)Session["TblOfMember"];
        int MemberCount = 0;
        for (int i = 0; i < dtOfMembers.Rows.Count; i++)
        {
            if (Convert.ToInt32(dtOfMembers.Rows[i]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Member || Convert.ToInt32(dtOfMembers.Rows[i]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.Kardan || Convert.ToInt32(dtOfMembers.Rows[i]["OfmTypeId"]) == (int)TSP.DataManager.OfficeMemberType.NewKardan)
            {
                MemberCount++;
            }
        }
        //if (MemberCount <= 2)
        //    return false;
        return true;
    }

    private void SetControlVisible(bool visible)
    {
        this.txtMeNo.ClientVisible = !visible;
        this.txtOtpCode.ClientVisible = visible;
        lblMeNo.ClientVisible = !visible;
        this.lblOtpCode.ClientVisible = visible;

        this.txtIdNo.ClientEnabled = visible;
        this.txtLastName.ClientEnabled = visible;
        this.txtFileNo.ClientEnabled = visible;
        this.txtFatherName.ClientEnabled = visible;
        this.txtFirstName.ClientEnabled = visible;
        this.txtMobile.ClientEnabled = visible;
        this.txtSSN.ClientEnabled = visible;
        this.txtTel.ClientEnabled = visible;
        this.txtTel_pre.ClientEnabled = visible;
        this.txtBirthPlace.ClientEnabled = visible;
        this.txtAddress.ClientEnabled = visible;

        this.txtBirthDate.Enabled = visible;
        imgMember.ClientVisible = !visible;
        flp_Image.ClientVisible = visible;


        if (this.ComboType.SelectedIndex == 3)
        {
            this.txtOtpCode.ClientVisible = !visible;
            this.lblOtpCode.ClientVisible = !visible;
            flp_Image.ClientVisible = visible;
        }

        if (ComboType.SelectedIndex == 4 || ComboType.SelectedIndex == 5)
        {
            PnlKardanMemarInfo.ClientVisible = visible;
            txtFileDate.Enabled = visible;
            flp_Image.ClientVisible = visible;
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

            } while (File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.HorRes, Utility.VerRes);
            Session["MemberImage"] = tempFileName2;
            //Session["MemberImage"] = ret;

        }
        return ret;
    }

    protected string SaveEmza(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            // Session["ExPlaceUpload"] = tempFileName;
            Session["MemberEmza"] = ret;

        }
        return ret;
    }

    protected string SaveImageLicense(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachCommit"] = ret;

        }
        return ret;
    }

    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDesc.Text = "";
        txtFatherName.Text = "";
        txtFileNo.Text = "";
        txtFileDate.Text = "";
        txtFirstName.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtMobile.Text = "";
        txtOtpCode.Text = "";
        txtSSN.Text = "";
        txtStartDate.Text = "";
        txtTel.Text = "";
        txtTel_pre.Text = "";
        txtMeNo.Text = "";

        txtMjName.Text = "";

        drdPosition.DataBind();
        drdPosition.SelectedIndex = -1;
        ComboTime.DataBind();
        ComboTime.SelectedIndex = 1;

        CmbAgent.DataBind();
        CmbAgent.SelectedIndex = -1;
        CmbCity.DataBind();
        CmbCity.SelectedIndex = 1;
        ComboMjId.DataBind();
        ComboMjId.SelectedIndex = 1;

        cmbHasEfficientGrade.SelectedIndex = 1;

        chbHaghEmza.Checked = false;

        imgEmza.ImageUrl = "";
        imgMember.ImageUrl = "../Images/person.png";

        HpCommit.ClientVisible = false;

        GridViewResponsblity.DataSource = "";
        GridViewResponsblity.DataBind();

        Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> filebut.DoClick();</script>");

        HD_Emza["name"] = 0;
        HD_img["name"] = 0;
        HDFlpCommit["name"] = 0;
    }

    protected void SetComboType(int Value)
    {
        if (Value == 0)
        {
            lblMeNo.ClientVisible = true;
            txtMeNo.ClientVisible = true;
            lblOtpCode.ClientVisible = false;
            txtOtpCode.ClientVisible = false;

        }
        else if (Value == 1 || Value == 2)
        {
            lblMeNo.ClientVisible = false;
            txtMeNo.ClientVisible = false;
            lblOtpCode.ClientVisible = true;
            txtOtpCode.ClientVisible = true;

        }
        else if (Value == 3)
        {
            lblMeNo.ClientVisible = false;
            txtMeNo.ClientVisible = false;
            lblOtpCode.ClientVisible = false;
            txtOtpCode.ClientVisible = false;

        }
        else if (Value == 5 || Value == 6)
        {
            lblMeNo.ClientVisible = false;
            txtMeNo.ClientVisible = false;
            lblOtpCode.ClientVisible = true;
            txtOtpCode.ClientVisible = true;

        }
    }

    protected string FindLockers(int Id, int MemberTypeId, int IsLock)
    {
        TSP.DataManager.LockHistoryManager LockHistoryManager = new TSP.DataManager.LockHistoryManager();
        return LockHistoryManager.FindLockers(Id, MemberTypeId, IsLock);

    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeMember).ToString());
    }
    #endregion
}

