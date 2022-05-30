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
//using DevExpress.Web.ASPxUploadControl;

public partial class Employee_OfficeRegister_OfficeMemberInsert : System.Web.UI.Page
{
    DataTable dtGrade = null;
    Boolean IsPageRefresh = false;

    #region Property

    string _PageMode
    {
        set
        {
            HiddenFieldOffice["PageMode"] = value;
        }
        get
        {
            return HiddenFieldOffice["PageMode"].ToString();
        }

    }

    int _OfId
    {
        set
        {
            HiddenFieldOffice["OfId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldOffice["OfId"].ToString());
        }
    }

    int _OfmId
    {
        set
        {
            HiddenFieldOffice["OfmId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldOffice["OfmId"].ToString());
        }
    }

    int _PersonId
    {
        set
        {
            HiddenFieldOffice["PersonId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldOffice["PersonId"].ToString());
        }
    }

    int _OfReId
    {
        set
        {
            HiddenFieldOffice["OfReId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldOffice["OfReId"].ToString());
        }
    }

    string _Dprt
    {
        set
        {
            HiddenFieldOffice["Department"] = value;
        }

        get
        {
            return HiddenFieldOffice["Department"].ToString();
        }
    }

    int _ReqType
    {
        set
        {
            HiddenFieldOffice["ReqType"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldOffice["ReqType"].ToString());
        }
    }

    #endregion

    enum OfficeMemberType
    {
        Member = 0,
        Kardan = 1,
        Memar = 2,
        OtherPerson = 3
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");
        #region Refresh
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
        #endregion
        if (!IsPostBack)
        {
            try
            {
                SetKey();
                SetControlsEnabledByMemberType();
                SetMode();

                CheckPermission();
                if (_Dprt == "MemberShip")
                    CheckWorkFlowPermissionForOffice();
                else if (_Dprt == "Document")
                    CheckWorkFlowPermissionForDoc();


                TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                ReqManager.FindByCode(_OfReId);
                if (ReqManager.Count > 0)
                {
                    _ReqType=Convert.ToInt32(ReqManager[0]["Type"]);
                    //if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
                    //{
                    //    drdPosition.Enabled = false;
                    //    if (_PageMode == "New")
                    //    {
                    //        drdPosition.DataBind();
                    //        drdPosition.SelectedIndex = drdPosition.Items.FindByValue(((int)TSP.DataManager.OfficePosition.ShareHolder).ToString()).Index;
                    //    }
                    //}
                    SetShareHolderRequestSetting();
                    if ((ReqManager[0]["IsConfirm"].ToString() != "0")
                          || _ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)//Request From Member
                    {
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave1.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                    }
                }
                TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
                if (!Utility.IsDBNullOrNullValue(_OfmId))
                {
                    MemManager.FindByCode(_OfmId);
                    if (MemManager.Count == 1)
                    {
                        if (int.Parse(MemManager[0]["OfReId"].ToString()) != _OfReId)
                        {
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnSave.Enabled = false;
                            btnSave1.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        //ComboType.DataBind();
        //if (ComboType.SelectedItem != null && ComboType.SelectedItem.Value != null && Convert.ToInt32(ComboType.SelectedItem.Value) != 3)
        //    SetTellValidation(false);
        //else
        //    SetTellValidation(true);

        if (Session["MemberEmza2"] != null)
        {
            imgEmza.ClientVisible = true;
            imgEmza.ImageUrl = "~/Image/Office/Other/Emza/" + Session["MemberEmza2"].ToString();
        }

        if (Session["MemberImage2"] != null)
        {
            imgMember.ImageUrl = "~/Image/Office/Other/Ax/" + Path.GetFileName(Session["MemberImage2"].ToString());
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave1.Enabled = (bool)this.ViewState["BtnSave"];

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (_OfReId != null)
            OfficeInfoUserControl.OfReId = _OfReId;
    }

    #region txt changed
    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        // System.Diagnostics.eve
        imgEmza.ClientVisible = false;
        flp_Emza.ClientVisible = true;
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = true;

        try
        {
            if (string.IsNullOrEmpty(txtMeNo.Text))
            {
                SetMessage("کد عضویت را مجدداً وارد نمایید");
                return;
            }
            int MeId = int.Parse(txtMeNo.Text);
            ClearFormRefresh();
            //if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
            //{
            //    drdPosition.Enabled = false;
            //    if (_PageMode == "New")
            //    {
            //        drdPosition.DataBind();
            //        drdPosition.SelectedIndex = drdPosition.Items.FindByValue(((int)TSP.DataManager.OfficePosition.ShareHolder).ToString()).Index;
            //    }
            //}
            SetShareHolderRequestSetting();
            txtFileNo.Text = "";
            _PersonId = -2;

            MeManager.FindByCode(MeId);
            if (MeManager.Count != 1)
            {
                SetMessage("چنين كد عضويتي وجود ندارد.كد عضويت را مجدداً وارد نماييد");
                return;
            }
            _PersonId = MeId;
            string LockName = "";

            #region CheckLock
            if (Convert.ToBoolean(MeManager[0]["IsLock"]))
            {
                LockName = FindLockers(MeId, (int)TSP.DataManager.LockMemberType.Member, 1);
                SetMessage("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
                this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = false;
                return;
            }
            #endregion
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeAdr"]))
                txtAddress.Text = MeManager[0]["HomeAdr"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["BirhtDate"]))
                txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["BirthPlace"]))
                txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FatherName"]))
                txtFatherName.Text = MeManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FileNo"]))
                txtFileNo.Text = MeManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FileDate"]))
                txtFileNoDate.Text = MeManager[0]["FileDate"].ToString();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["ExpireDate"]))
                    txtFileNoDate.Text = dtMeFile.Rows[0]["ExpireDate"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FirstName"]))
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["IdNo"]))
                txtIdNo.Text = MeManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["LastName"]))
                txtLastName.Text = MeManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["MobileNo"]))
                txtMobile.Text = MeManager[0]["MobileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["SSN"]))
                txtSSN.Text = MeManager[0]["SSN"].ToString();
            // txtTel.Text = MeManager[0]["HomeTel"].ToString();

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeTel"]))
            {
                if (MeManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                {
                    txtTel_pre.Text = MeManager[0]["HomeTel"].ToString().Substring(0, MeManager[0]["HomeTel"].ToString().IndexOf("-"));
                    txtTel.Text = MeManager[0]["HomeTel"].ToString().Substring(MeManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MeManager[0]["HomeTel"].ToString().Length - MeManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtTel.Text = MeManager[0]["HomeTel"].ToString();
                }
            }

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"].ToString()))
                imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();
            else
                imgMember.ImageUrl = "";

            if (!Utility.IsDBNullOrNullValue(MeManager[0]["SignUrl"]))
            {
                imgEmza.ClientVisible = true;
                flp_Emza.ClientVisible = false;
                imgEmza.ImageUrl = MeManager[0]["SignUrl"].ToString();
                Session["IsImgMember"] = 1;
                Session["ImgMember_offMe"] = MeManager[0]["SignUrl"].ToString();
                Session["MemberEmza2"] = Path.GetFileName(MeManager[0]["SignUrl"].ToString());

            }
            else
            {
                flp_Emza.ClientVisible = true;
                imgEmza.ClientVisible = false;
                Session["MemberEmza2"] = null;
            }

            MeFileManager.SelectLastVersion(MeId, 0);
            if (MeFileManager.Count > 0)
            {
                int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                GridViewGrade.DataSource = MeFileDetailManager.SelectById(MfId, MeId, 0);
                GridViewGrade.KeyFieldName = "MfdId";
                GridViewGrade.DataBind();
            }

            HD_img["name"] = 1;
            SetMember();

            Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در مشاهده اطلاعات عضو مورد نظر رخ داده است");

        }
    }

    protected void txtFileNo_TextChanged(object sender, EventArgs e)
    {
        if (ComboType.Value == null || Convert.ToInt32(ComboType.Value) != (int)OfficeMemberType.Memar)
            return;

        TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        try
        {
            ClearFormRefresh();
            if (string.IsNullOrEmpty(txtFileNo.Text))
            {
                SetMessage("شماره پروانه را مجدداً وارد نماييد");
                return;
            }

            OthPersonManager.FindMemarByFileNo(txtFileNo.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Memar,0);

            if (OthPersonManager.Count != 1)
            {
                SetMessage("چنين شماره پروانه اشتغالی وجود ندارد.شماره پروانه اشتغال را مجدداً وارد نماييد");
                return;
            }

            if (Convert.ToBoolean(OthPersonManager[0]["InActive"]))
            {
                SetMessage("شخص مورد نظر غير فعال مي باشد");
                return;
            }
            string LockName = "";
            #region CheckLock
            if (Convert.ToBoolean(OthPersonManager[0]["IsLock"]))
            {
                LockName = FindLockers(_PersonId, (int)TSP.DataManager.LockMemberType.Memar, 1);
                SetMessage("امکان ثبت شخص مورد نظر وجود ندارد.شخص مورد نظر توسط " + LockName + " قفل می باشد ");
                return;
            }
            #endregion

            _PersonId = Convert.ToInt32(OthPersonManager[0]["OtpId"]);
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["Address"]))
                txtAddress.Text = OthPersonManager[0]["Address"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["BirthDate"]))
                txtBirthDate.Text = OthPersonManager[0]["BirthDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["BirthPlace"]))
                txtBirthPlace.Text = OthPersonManager[0]["BirthPlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FatherName"]))
                txtFatherName.Text = OthPersonManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FileNoDate"]))
                txtFileNoDate.Text = OthPersonManager[0]["FileNoDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FirstName"]))
                txtFirstName.Text = OthPersonManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["IdNo"]))
                txtIdNo.Text = OthPersonManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["LastName"]))
                txtLastName.Text = OthPersonManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MobileNo"]))
                txtMobile.Text = OthPersonManager[0]["MobileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["SSN"]))
                txtSSN.Text = OthPersonManager[0]["SSN"].ToString();

            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["Tel"]))
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
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MjId"]))
            {
                ComboMjId.DataBind();
                ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthPersonManager[0]["MjId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MjName"]))
                txtMjName.Text = OthPersonManager[0]["MjName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["ImageUrl"]))
            {
                imgMember.ImageUrl = OthPersonManager[0]["ImageUrl"].ToString();
                HD_img["name"] = 1;
            }

            GridViewGrade.DataSource = GradeManager.FindByOtpId(_PersonId, 0);
            GridViewGrade.KeyFieldName = "MGId";
            GridViewGrade.DataBind();


        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطايي در بازخواني اطلاعات رخ داده است");
        }
    }

    protected void txtOtpCode_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        try
        {
            if (ComboType.Value == null)
            {
                SetMessage("نوع عضو را مجدداً انتخاب نماييد");
                return;
            }
            if (string.IsNullOrEmpty(txtOtpCode.Text))
            {

                SetMessage("كد عضويت را مجدداً وارد نماييد");
                return;
            }
            string OtpCode = txtOtpCode.Text;
            ClearFormRefresh();
            txtFileNo.Text = "";
            _PersonId = -2;
            txtOtpCode.Text = OtpCode;
            OthPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text, (int)TSP.DataManager.OtherPersonType.Kardan);

            if (OthPersonManager.Count != 1)
            {
                SetMessage("چنين كد عضويتي وجود ندارد.كد عضويت را مجدداً وارد نماييد");
                return;
            }

            if (Convert.ToBoolean(OthPersonManager[0]["InActive"]))
            {
                SetMessage("شخص مورد نظر غير فعال مي باشد");

                return;
            }

            string LockName = "";
            #region CheckLock
            if (Convert.ToBoolean(OthPersonManager[0]["IsLock"]))
            {
                LockName = FindLockers(_PersonId, (int)TSP.DataManager.LockMemberType.Kardan, 1);

                SetMessage("امکان ثبت شخص مورد نظر وجود ندارد.شخص مورد نظر توسط " + LockName + " قفل می باشد ");
                return;
            }
            #endregion

            _PersonId = Convert.ToInt32(OthPersonManager[0]["OtpId"]);
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["Address"]))
                txtAddress.Text = OthPersonManager[0]["Address"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["BirthDate"]))
                txtBirthDate.Text = OthPersonManager[0]["BirthDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["BirthPlace"]))
                txtBirthPlace.Text = OthPersonManager[0]["BirthPlace"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FatherName"]))
                txtFatherName.Text = OthPersonManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FileNo"]))
                txtFileNo.Text = OthPersonManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FileNoDate"]))
                txtFileNoDate.Text = OthPersonManager[0]["FileNoDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FirstName"]))
                txtFirstName.Text = OthPersonManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["IdNo"]))
                txtIdNo.Text = OthPersonManager[0]["IdNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["LastName"]))
                txtLastName.Text = OthPersonManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MobileNo"]))
                txtMobile.Text = OthPersonManager[0]["MobileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["SSN"]))
                txtSSN.Text = OthPersonManager[0]["SSN"].ToString();

            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["Tel"]))
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
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MjId"]))
            {
                ComboMjId.DataBind();
                ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthPersonManager[0]["MjId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MjName"]))
                txtMjName.Text = OthPersonManager[0]["MjName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["ImageUrl"]))
            {
                imgMember.ImageUrl = OthPersonManager[0]["ImageUrl"].ToString();
                HD_img["name"] = 1;
            }

            GridViewGrade.DataSource = GradeManager.SelectGradeByOtpId(_PersonId);
            GridViewGrade.KeyFieldName = "MGId";
            GridViewGrade.DataBind();
            SetShareHolderRequestSetting();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطايي در بازخواني اطلاعات رخ داده است");

        }
    }
    #endregion

    protected void ComboType_IndexChenge(object sender, EventArgs e)
    {
        if (ComboType.Value != null)
        {
            ClearFormRefresh();
            txtMeNo.Text = "";
            txtOtpCode.Text = "";
            txtFileNo.Text = "";
            SetControlsEnabledByMemberType();
            //if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
            //{
            //    drdPosition.Enabled = false;
            //    if (_PageMode == "New")
            //    {
            //        drdPosition.DataBind();
            //        drdPosition.SelectedIndex = drdPosition.Items.FindByValue(((int)TSP.DataManager.OfficePosition.ShareHolder).ToString()).Index;
            //    }
            //}
            SetShareHolderRequestSetting();
        }
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

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        txtMeNo.ReadOnly = false;
        txtOtpCode.ReadOnly = false;
        SetKey();
        _OfmId = 0;
        _PageMode = "New";

        ClearFormAll();
        ComboType.ClientEnabled = true;
        ComboType.SelectedIndex = 0;
        SetControlsEnabledByMemberType();
        SetMode();

        if (_Dprt == "MemberShip")
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave1.Enabled = per.CanNew;
        }
        else
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave1.Enabled = per.CanNew;
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(_OfmId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (_Dprt == "MemberShip")
            {
                TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave1.Enabled = per.CanEdit;
            }
            else
            {
                TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave1.Enabled = per.CanEdit;
            }
            txtMeNo.ReadOnly = true;
            txtOtpCode.ReadOnly = true;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            //****** GridViewGrade.Columns[3].Visible = true;               

            _PageMode = "Edit";
            RoundPanelOfficeMember.HeaderText = "ویرایش";
            RoundPanelOfficeMember.Enabled = true;

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["MemberImage2"] = null;
        Session["MemberEmza2"] = null;
        Session["MeGrade"] = null;
        Response.Redirect("OfficeMembers.aspx?OfId=" + Utility.EncryptQS(_OfId.ToString())
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&OfReId=" + Utility.EncryptQS(_OfReId.ToString())
            + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
            + "&Dprt=" + Utility.EncryptQS(_Dprt));

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        SetControlsEnabledByMemberType();
        if (_PageMode == "Edit")
            Edit();
        else if (_PageMode == "New")
            Insert();
    }

    #endregion

    #endregion

    #region Methods

    private void SetKey()
    {

        ODBPosition.FilterParameters[0].DefaultValue = "0";
        Session["IsEdited_OffAaza"] = false;
        Session["MemberEmza2"] = null;
        Session["MemberImage2"] = null;
        Session["IsImgMember"] = 0;
        Session["ImgMember_offMe"] = "";

        _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["aPageMode"]));
        _OfId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfId"]).ToString()));
        _OfmId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfmId"]).ToString()));
        _OfReId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfReId"]).ToString()));
        _Dprt = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Dprt"]));

        if (Utility.IsDBNullOrNullValue(_Dprt))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (Utility.IsDBNullOrNullValue(_OfReId) || Utility.IsDBNullOrNullValue(_OfId))
        {
            if (_Dprt == "MemberShip")
            {
                Response.Redirect("Office.aspx");
                return;
            }
            else
            {
                Response.Redirect("OfficeDocument.aspx");
                return;
            }
        }
    }

    private void SetMode()
    {

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (_PageMode)
        {
            case "New":
                #region New
                RoundPanelOfficeMember.HeaderText = "جدید";
                RoundPanelOfficeMember.Enabled = true;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                #endregion
                break;
            case "View":
                #region View
                if (Utility.IsDBNullOrNullValue(_OfmId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
                FillForm(_OfmId);
                RoundPanelOfficeMember.HeaderText = "مشاهده";
                RoundPanelOfficeMember.Enabled = false;
                flp_Emza.ClientVisible = false;
                #endregion
                break;
            case "Edit":
                #region EditMode
                txtMeNo.ReadOnly = true;
                txtOtpCode.ReadOnly = true;

                if (Utility.IsDBNullOrNullValue(_OfmId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                FillForm(_OfmId);
                RoundPanelOfficeMember.HeaderText = "ویرایش";
                ComboType.ClientEnabled = false;
                #endregion
                break;
        }
    }

    #region SAVE IMAGE
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
            Session["MemberImage2"] = ret;
            //Session["MemberImage2"] = ret;

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
            Session["MemberEmza2"] = ret;

        }
        return ret;
    }
    #endregion

    #region  Clear
    protected void ClearFormAll()
    {
        ClearFormRefresh();

        ComboType.DataBind();
        ComboType.SelectedIndex = 0;

        txtMeNo.Text = "";
        txtOtpCode.Text = "";
        txtFileNo.Text = "";
    }

    protected void ClearFormRefresh()
    {
        Session["IsImgMember"] = 0;
        Session["ImgMember_offMe"] = "";
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDesc.Text = "";
        txtFatherName.Text = "";
        txtFileNoDate.Text = "";
        txtFirstName.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtMobile.Text = "";
        txtOtpCode.Text = "";
        txtSSN.Text = "";
        txtStartDate.Text = "";
        txtTel.Text = "";
        txtTel_pre.Text = "";

        drdPosition.DataBind();
        drdPosition.SelectedIndex = -1;
        ComboTime.DataBind();
        ComboTime.SelectedIndex = 1;
        chbHaghEmza.Checked = false;

        ComboMjId.DataBind();
        ComboMjId.SelectedIndex = -1;
        txtMjName.Text = "";

        imgEmza.ImageUrl = "";
        imgEmza.ClientVisible = false;
        imgMember.ImageUrl = "../../Images/person.png";
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");
        flp_Emza.ClientVisible = true;
        HD_Emza["name"] = 0;
        HD_img["name"] = 0;

        GridViewGrade.DataSource = "";
        GridViewGrade.DataBind();

        cmbHasEfficientGrade.SelectedIndex = 1;

        //Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        //dtGrade = (DataTable)Session["MeGrade"];
        //dtGrade.Rows.Clear();

    }
    #endregion

    #region FILL
    protected void FillForm(int OfmId)
    {
        try
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }

            drdPosition.DataBind();
            drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
            ComboTime.DataBind();
            if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                ComboTime.SelectedIndex = 1;
            else
                ComboTime.SelectedIndex = 0;
            cmbHasEfficientGrade.SelectedIndex = cmbHasEfficientGrade.Items.FindByValue(Convert.ToInt32(OfMeManager[0]["HasEfficientGrade"]).ToString()).Index;

            txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
            chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
            if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["SignUrl"]))
            {
                imgEmza.ClientVisible = true;
                imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();
                HD_Emza["name"] = 1;
            }
            else
                imgEmza.ClientVisible = false;
            txtDesc.Text = OfMeManager[0]["Description"].ToString();
            _PersonId = Convert.ToInt32(OfMeManager[0]["PersonId"]);
            switch (Convert.ToInt32(OfMeManager[0]["OfmType"]))// OfmType)
            {

                case (int)TSP.DataManager.OfficeMemberType.Member: // 1://member
                    ComboType.SelectedIndex = 0;
                    SetMember();
                    #region SetFileDetail
                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["MfId"]))
                    {
                        int MfId = Convert.ToInt32(OfMeManager[0]["MfId"]);

                        MeFileManager.FindByCode(MfId, 0);
                        txtFileNo.Text = MeFileManager[0]["MFNo"].ToString();
                        txtFileNoDate.Text = MeFileManager[0]["ExpireDate"].ToString();

                        GridViewGrade.DataSource = MeFileDetailManager.SelectById(MfId, Convert.ToInt32(OfMeManager[0]["PersonId"]), 0);
                        GridViewGrade.KeyFieldName = "MfdId";
                        GridViewGrade.DataBind();
                    }
                    #endregion
                    FillMember(Convert.ToInt32(OfMeManager[0]["PersonId"]));
                    break;
                case (int)TSP.DataManager.OfficeMemberType.Kardan:// 2://kardan
                    ComboType.SelectedIndex = 1;
                    SetKardanMemar();
                    FillOthers(Convert.ToInt32(OfMeManager[0]["PersonId"]));
                    break;
                case (int)TSP.DataManager.OfficeMemberType.Otherperson:// 3://Other
                    ComboType.SelectedIndex = 3;
                    SetOther();
                    FillOthers(Convert.ToInt32(OfMeManager[0]["PersonId"]));
                    break;
                case (int)TSP.DataManager.OfficeMemberType.Memar://4://Memar
                    ComboType.SelectedIndex = 2;
                    SetMemar();
                    FillOthers(Convert.ToInt32(OfMeManager[0]["PersonId"]));
                    break;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    protected void FillMember(int MeId)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(MeId);

        if (MeManager.Count != 1)
        {
            return;
        }
        txtFirstName.Text = MeManager[0]["FirstName"].ToString();
        txtLastName.Text = MeManager[0]["LastName"].ToString();
        txtFatherName.Text = MeManager[0]["FatherName"].ToString();
        txtIdNo.Text = MeManager[0]["IdNo"].ToString();
        txtSSN.Text = MeManager[0]["SSN"].ToString();
        txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
        txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
        txtMeNo.Text = MeId.ToString();
        txtMobile.Text = MeManager[0]["MobileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeTel"]))
        {
            if (MeManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
            {
                txtTel_pre.Text = MeManager[0]["HomeTel"].ToString().Substring(0, MeManager[0]["HomeTel"].ToString().IndexOf("-"));
                txtTel.Text = MeManager[0]["HomeTel"].ToString().Substring(MeManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MeManager[0]["HomeTel"].ToString().Length - MeManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtTel.Text = MeManager[0]["HomeTel"].ToString();
            }
        }

        txtAddress.Text = MeManager[0]["HomeAdr"].ToString();

        if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]))
        {
            imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();
            HD_img["name"] = 1;

        }
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["SignUrl"]))
        {
            imgEmza.ClientVisible = true;
            flp_Emza.ClientVisible = false;
        }
    }

    protected void FillOthers(int PersonId)
    {
        // TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();

        OthManager.FindByCode(PersonId);

        if (OthManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }

        txtOtpCode.Text = OthManager[0]["OtpCode"].ToString();
        txtFirstName.Text = OthManager[0]["FirstName"].ToString();
        txtLastName.Text = OthManager[0]["LastName"].ToString();
        txtFatherName.Text = OthManager[0]["FatherName"].ToString();
        txtIdNo.Text = OthManager[0]["IdNo"].ToString();
        txtSSN.Text = OthManager[0]["SSN"].ToString();
        txtBirthPlace.Text = OthManager[0]["BirthPlace"].ToString();
        txtBirthDate.Text = OthManager[0]["BirthDate"].ToString();
        txtFileNo.Text = OthManager[0]["FileNo"].ToString();
        txtFileNoDate.Text = OthManager[0]["FileNoDate"].ToString();
        if (OthManager[0]["Tel"].ToString() != "")
        {
            if (OthManager[0]["Tel"].ToString().IndexOf("-") > 0)
            {
                txtTel_pre.Text = OthManager[0]["Tel"].ToString().Substring(0, OthManager[0]["Tel"].ToString().IndexOf("-"));
                txtTel.Text = OthManager[0]["Tel"].ToString().Substring(OthManager[0]["Tel"].ToString().IndexOf("-") + 1, OthManager[0]["Tel"].ToString().Length - OthManager[0]["Tel"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtTel.Text = OthManager[0]["Tel"].ToString();
            }
        }
        txtMobile.Text = OthManager[0]["MobileNo"].ToString();
        txtAddress.Text = OthManager[0]["Address"].ToString();
        if (!Utility.IsDBNullOrNullValue(OthManager[0]["ImgUrl"]))
        {
            imgMember.ClientVisible = true;
            imgMember.ImageUrl = OthManager[0]["ImgUrl"].ToString();
            HD_img["name"] = 1;
        }

        if (!Utility.IsDBNullOrNullValue(OthManager[0]["MjId"]))
        {
            ComboMjId.DataBind();
            ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthManager[0]["MjId"].ToString());

        }
        txtMjName.Text = OthManager[0]["MjName"].ToString();
        if (Convert.ToInt32(OthManager[0]["OtpType"]) == (int)TSP.DataManager.OtherPersonType.Kardan
              || Convert.ToInt32(OthManager[0]["OtpType"]) == (int)TSP.DataManager.OtherPersonType.Memar)
        {
            //ASPxRoundPanelGrade.ClientVisible = true;
            TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
            GridViewGrade.DataSource = GradeManager.SelectGradeByOtpId(PersonId);
            GridViewGrade.KeyFieldName = "MGId";
            GridViewGrade.DataBind();
        }
        //if (Convert.ToInt32(OthManager[0]["OtpType"]) == (int)TSP.DataManager.OtherPersonType.OtherPerson)
        //{
        //    ASPxRoundPanelGrade.ClientVisible = false;
        //}
    }
    #endregion

    #region EDIT
    private void Edit()
    {
        bool changeImgEmza = false;
        String imgFolderName = "";
        #region Check Conditions
        try
        {
            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            if (Utility.IsDBNullOrNullValue(_OfmId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager
                || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
            {
                DataTable dt = OfManager.SelectOfficeManagerByOfId(_OfId, 0, _OfReId);
                if (dt.Rows.Count != 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["PersonId"]) != _PersonId)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای شرکت مورد نظر مدیر عامل ثبت شده است";
                        return;
                    }
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        #endregion
        if (ComboType.Value == null)
        {
            SetMessage("نوع عضو را انتخاب نمایید.");
            return;
        }
        if (cmbHasEfficientGrade.Value == null)
        {
            SetMessage("وضعیت امتیاز عضو را انتخاب نمایید.");
            return;
        }
        int type = Convert.ToInt32(ComboType.Value);
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        try
        {
            switch (type)
            {
                case (int)OfficeMemberType.Member:
                    changeImgEmza = EditMember(_OfmId, trans);
                    imgFolderName = "~/Image/Office/Members/Emza/";
                    break;
                case (int)OfficeMemberType.Kardan:
                    changeImgEmza = EditKardanAndMemar(_OfmId, _PersonId, type, trans);
                    imgFolderName = "~/Image/Office/Kardan/Emza/";
                    break;
                case (int)OfficeMemberType.Memar:
                    changeImgEmza = EditKardanAndMemar(_OfmId, _PersonId, type, trans);
                    imgFolderName = "~/Image/Office/Kardan/Emza/";
                    break;
                case (int)OfficeMemberType.OtherPerson:
                    changeImgEmza = EditOther(_OfmId, _PersonId, type, trans);
                    imgFolderName = "~/Image/Office/Other/Emza/";
                    break;
            }
            if (!changeImgEmza)
            {
                trans.CancelSave();
                return;
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    SetMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
            return;
        }
        if (Session["MemberEmza2"] != null && !String.IsNullOrEmpty(imgFolderName) && changeImgEmza)
        {
            imgEmza.ClientVisible = true;
            imgEmza.ImageUrl = imgFolderName + Session["MemberEmza2"].ToString();
        }
    }

    protected bool EditMember(int OfmId, TSP.DataManager.TransactionManager trans)
    {
        // bool functionReturn = true;
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        //////TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);
        //////trans.Add(ReqManager);
        int MemberFileId = -1;
  
        int MeId = int.Parse(txtMeNo.Text);
        string Alert = "";
        try
        {          
            //***********************Check MemberConditions**************
            //***********************************************************
            if (Convert.ToInt32(drdPosition.SelectedItem.Value) != (int)TSP.DataManager.OfficePosition.ShareHolder)
            {
                ArrayList Result = TSP.DataManager.OfficeMemberManager.CheckOfficeMembershipcondition(MeId, _OfId, _OfReId,false);
                if (!Convert.ToBoolean(Result[0]))
                {
                    SetMessage(Result[1].ToString());
                    return false;
                }
                Alert = Result[1].ToString();
                MemberFileId = int.Parse(Result[2].ToString());
            }
            //***********************************************************
            //***********************************************************
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        string pathAx = "";
        bool chSignImg = false;

        trans.BeginSave();
        OfMeManager.FindByCode(OfmId);
        if (OfMeManager.Count != 1)
        {
            this.DivReport.Visible = true;
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        OfMeManager[0].BeginEdit();
        if (Session["MemberEmza2"] != null)
        {
            pathAx = Server.MapPath("~/image/Temp/");
            OfMeManager[0]["SignUrl"] = "~/Image/Office/Members/Emza/" + Session["MemberEmza2"].ToString();
            chSignImg = true;
        }

        if (drdPosition.Value != null)
            OfMeManager[0]["OfpId"] = int.Parse(drdPosition.Value.ToString());
        OfMeManager[0]["StartDate"] = txtStartDate.Text; ;
        OfMeManager[0]["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);

        if (ComboTime.Value != null)
        {
            if (ComboTime.Value.ToString() == "1")
                OfMeManager[0]["IsFullTime"] = 1;
            else
                OfMeManager[0]["IsFullTime"] = 0;
        }
        if (!Utility.IsOfficeMemberConfirmRequestNeeded())
        {
            OfMeManager[0]["IsConfirm"] = 1;
            OfMeManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
        }
        OfMeManager[0]["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
        OfMeManager[0]["Description"] = txtDesc.Text;
        OfMeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        OfMeManager[0]["ModifiedDate"] = DateTime.Now;
        OfMeManager[0].EndEdit();

        if (OfMeManager.Save() <= 0)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }

        #region   //با توجه به اختصاص پایه به صورت دستی این قسمت کامنت شد
        //////////ReqManager.FindByCode(_OfReId);
        //////////if (ReqManager.Count == 1)
        //////////{
        //////////    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
        //////////    {
        //////////        int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
        //////////        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
        //////////        {
        //////////            ReqManager[0].BeginEdit();
        //////////            ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(_OfId, _OfReId);
        //////////            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        //////////            ReqManager[0]["ModifiedDate"] = DateTime.Now;
        //////////            ReqManager[0].EndEdit();
        //////////            ReqManager.Save();
        //////////            ReqManager.DataTable.AcceptChanges();
        //////////        }
        //////////    }
        //////////}
        #endregion

        int UpdateState = -1;
        if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
        {
            int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
            int WfCode = -1;
            if (_Dprt == "Document")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
            else if (_Dprt == "MemberShip")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
            if (WfCode == -1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return false;
            }
            UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "ویرایش عضوحقیقی شرکت", Utility.GetCurrentUser_UserId());
        }
        if (UpdateState == -4)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }

        trans.EndSave();
        SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        Session["IsEdited_OffAaza"] = true;

        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/Office/Members/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        return true;
    }

    protected bool EditOther(int OfmId, int PersonId, int type, TSP.DataManager.TransactionManager trans)
    {
        //  bool functionReturn = true;
        string pathAx = "";

        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        //TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OthManager);
        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);


        bool chAxImg = false;
        bool chSignImg = false;
        trans.BeginSave();
        OthManager.FindByCode(PersonId);
        OthManager[0].BeginEdit();
        OthManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        if (Session["MemberImage2"] != null)
        {
            pathAx = Server.MapPath("~/image/Temp/");
            OthManager[0]["ImageUrl"] = "~/Image/Office/Other/Ax/" + Path.GetFileName(Session["MemberImage2"].ToString());
            chAxImg = true;


        }
        OthManager[0]["FirstName"] = txtFirstName.Text;
        OthManager[0]["FatherName"] = txtFatherName.Text;
        OthManager[0]["LastName"] = txtLastName.Text;
        OthManager[0]["IdNo"] = txtIdNo.Text;
        OthManager[0]["SSN"] = txtSSN.Text;
        OthManager[0]["BirthPlace"] = txtBirthPlace.Text;
        OthManager[0]["BirthDate"] = txtBirthDate.Text;
        //OthManager[0]["FileNo"] = txtFileNo.Text;
        //OthManager[0]["FileNoDate"] = txtFileNoDate.Text;

        OthManager[0]["Description"] = txtDesc.Text;
        if (txtTel.Text != "" && txtTel_pre.Text != "")
            OthManager[0]["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
        else if (txtTel.Text != "")
            OthManager[0]["Tel"] = txtTel.Text;
        OthManager[0]["MobileNo"] = txtMobile.Text;
        OthManager[0]["Address"] = txtAddress.Text;
        OthManager[0].EndEdit();

        if (OthManager.Save() <= 0)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return false;
        }
        OfMeManager.FindByCode(OfmId);
        OfMeManager[0].BeginEdit();

        if (Session["MemberEmza2"] != null)
        {
            pathAx = Server.MapPath("~/image/Temp/");

            OfMeManager[0]["SignUrl"] = "~/Image/Office/Other/Emza/" + Session["MemberEmza2"].ToString();
            chSignImg = true;

        }

        if (drdPosition.Value != null)
        {
            OfMeManager[0]["OfpId"] = int.Parse(drdPosition.Value.ToString());
        }


        OfMeManager[0]["StartDate"] = txtStartDate.Text;
        OfMeManager[0]["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);
        if (ComboTime.Value != null)
        {
            if (ComboTime.Value.ToString() == "1")
                OfMeManager[0]["IsFullTime"] = 1;
            else
                OfMeManager[0]["IsFullTime"] = 0;
        }
        if (!Utility.IsOfficeMemberConfirmRequestNeeded())
        {
            OfMeManager[0]["IsConfirm"] = 1;
            OfMeManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
        }
        OfMeManager[0]["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
        OfMeManager[0]["Description"] = txtDesc.Text;
        OfMeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        OfMeManager[0]["ModifiedDate"] = DateTime.Now;
        OfMeManager[0].EndEdit();

        if (OfMeManager.Save() != 1)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return false;

        }
        int UpdateState = -1;
        if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
        {
            //  int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
            int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
            int WfCode = -1;
            if (_Dprt == "Document")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
            else if (_Dprt == "MemberShip")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
            if (WfCode == -1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return false;
            }
            UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
        }
        if (UpdateState == -4)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return false;
        }
        trans.EndSave();
        SetMessage("ذخیره انجام شد");
        Session["IsEdited_OffAaza"] = true;

        ///////////////

        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/Office/Other/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        if (chAxImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["MemberImage2"].ToString());
                string EmzaTarget = Server.MapPath("~/image/Office/Other/Ax/") + Path.GetFileName(Session["MemberImage2"].ToString());
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);

            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        return true;
    }

    protected bool EditKardanAndMemar(int OfmId, int PersonId, int type, TSP.DataManager.TransactionManager trans)
    {
        string pathAx = "";

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);

        bool chSignImg = false;

        trans.BeginSave();

        OfMeManager.FindByCode(OfmId);
        OfMeManager[0].BeginEdit();
        if (Session["MemberEmza2"] != null)
        {
            pathAx = Server.MapPath("~/image/Temp/");

            OfMeManager[0]["SignUrl"] = "~/Image/Office/Kardan/Emza/" + Session["MemberEmza2"].ToString();
            chSignImg = true;

        }

        if (drdPosition.Value != null)
        {
            OfMeManager[0]["OfpId"] = int.Parse(drdPosition.Value.ToString());
        }


        OfMeManager[0]["StartDate"] = txtStartDate.Text;
        OfMeManager[0]["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);
        if (ComboTime.Value != null)
        {
            if (ComboTime.Value.ToString() == "1")
                OfMeManager[0]["IsFullTime"] = 1;
            else
                OfMeManager[0]["IsFullTime"] = 0;
        }
        if (!Utility.IsOfficeMemberConfirmRequestNeeded())
        {
            OfMeManager[0]["IsConfirm"] = 1;
            OfMeManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
        }
        OfMeManager[0]["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
        OfMeManager[0]["Description"] = txtDesc.Text;
        OfMeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        OfMeManager[0]["ModifiedDate"] = DateTime.Now;
        OfMeManager[0].EndEdit();

        if (OfMeManager.Save() != 1)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return false;
        }
        int UpdateState = -1;
        if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
        {
            // int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
            int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
            int WfCode = -1;
            if (_Dprt == "Document")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
            else if (_Dprt == "MemberShip")
                WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
            if (WfCode == -1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return false;
            }
            UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
        }
        if (UpdateState == -4)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return false;
        }
        trans.EndSave();

        SetMessage("ذخیره انجام شد");
        Session["IsEdited_OffAaza"] = true;

        if (chSignImg == true)
        {
            try
            {
                if (type == 1)
                {
                    string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                    string EmzaTarget = Server.MapPath("~/image/Office/Kardan/Emza/") + Session["MemberEmza2"].ToString();
                    System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                }
                else if (type == 2)
                {
                    string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                    string EmzaTarget = Server.MapPath("~/image/Office/Memar/Emza/") + Session["MemberEmza2"].ToString();
                    System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                }

            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }
        }
        return true;
    }

    #endregion

    #region INSERT
    private void Insert()
    {
        bool changeImgEmza = false;
        String imgFolderName = "";
        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();

        #region Check Conditions
        if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager
            || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
        {
            DataTable dt = OfManager.SelectOfficeManagerByOfId(_OfId, 0, _OfReId);
            if (dt.Rows.Count != 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای شرکت مورد نظر مدیر عامل ثبت شده است";
                return;
            }
        }
        #endregion
        if (ComboType.Value == null)
        {
            SetMessage("نوع عضو را انتخاب نمایید.");
            return;
        }
        if (cmbHasEfficientGrade.Value == null)
        {
            SetMessage("وضعیت امتیاز عضو را انتخاب نمایید.");
            return;
        }
        int type = int.Parse(ComboType.Value.ToString());
        switch (Convert.ToInt32(ComboType.Value))
        {
            case (int)OfficeMemberType.Member:
                changeImgEmza = InsertMember();
                imgFolderName = "~/Image/Office/Members/Emza/";
                break;
            case (int)OfficeMemberType.Memar:
                changeImgEmza = InsertKardanAndMemar();
                imgFolderName = "~/Image/Office/Kardan/Emza/";
                break;
            case (int)OfficeMemberType.Kardan:
                changeImgEmza = InsertKardanAndMemar();
                imgFolderName = "~/Image/Office/Kardan/Emza/";
                break;
            case (int)OfficeMemberType.OtherPerson:
                changeImgEmza = InsertOther();
                imgFolderName = "~/Image/Office/Other/Emza/";
                break;
        }
        if (!Utility.IsDBNullOrNullValue(Session["MemberEmza2"]) && !String.IsNullOrEmpty(imgFolderName) && changeImgEmza)
        {
            imgEmza.ClientVisible = true;
            imgEmza.ImageUrl = imgFolderName + Session["MemberEmza2"].ToString();
        }

        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            arr[1] = 1;
            Session["OffMenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(_OfReId);
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            arr[1] = 1;
            Session["OffMenuArrayList"] = arr;
        }
    }

    protected bool InsertMember()
    {
        bool functionReturn = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        //DocMemberFileManager.ClearBeforeFill = true;

        trans.Add(OfMeManager);
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);

        bool chSignImg = false;
        string pathAx = "";
        int MemberFileId = -1;
        if (string.IsNullOrEmpty(txtMeNo.Text))
        {
            SetMessage("کد عضویت را مجدداً وارد نمایید");
            return false;
        }
        int MeId = int.Parse(txtMeNo.Text);
        string Alert = "";
        try
        {
            //***********************Check MemberConditions**************
            //***********************************************************
            if (Convert.ToInt32(drdPosition.SelectedItem.Value) != (int)TSP.DataManager.OfficePosition.ShareHolder)
            {
                ArrayList Result = TSP.DataManager.OfficeMemberManager.CheckOfficeMembershipcondition(MeId, _OfId, _OfReId);
                if (!Convert.ToBoolean(Result[0]))
                {
                    SetMessage(Result[1].ToString());
                    return false;
                }
                Alert = Result[1].ToString();
                MemberFileId = int.Parse(Result[2].ToString());
            }
            //***********************************************************
            //***********************************************************
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }

        try
        {
            trans.BeginSave();
            DataRow drMembers = OfMeManager.NewRow();

            drMembers["OfReId"] = _OfReId;

            ///////////////////////////
            //if (MemberFileId == -1)
            //{
            //    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            //    if (dtMeFile.Rows.Count == 1)
            //        MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            //}
            /////
            if (MemberFileId != -1)
                drMembers["MfId"] = MemberFileId;
            else
                drMembers["MfId"] = DBNull.Value;

            drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
            drMembers["PersonId"] = int.Parse(txtMeNo.Text);

            drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);

            if (Session["MemberEmza2"] != null)
            {
                pathAx = Server.MapPath("~/image/Temp/");
                drMembers["SignUrl"] = "~/Image/Office/Members/Emza/" + Session["MemberEmza2"].ToString();
                chSignImg = true;
            }

            drMembers["OfId"] = _OfId;

            if (drdPosition.Value != null)
            {
                drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());
            }
            drMembers["StartDate"] = txtStartDate.Text;
            drMembers["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);

            if (ComboTime.Value != null)
            {
                if (ComboTime.Value.ToString() == "1")
                    drMembers["IsFullTime"] = 1;
                else
                    drMembers["IsFullTime"] = 0;
            }

            if (!Utility.IsOfficeMemberConfirmRequestNeeded())
            {
                drMembers["IsConfirm"] = 1;
                drMembers["ConfirmDate"] = Utility.GetDateOfToday();
            }

            drMembers["Description"] = txtDesc.Text;
            drMembers["UserId"] = Utility.GetCurrentUser_UserId();
            drMembers["ModifiedDate"] = DateTime.Now;
            OfMeManager.AddRow(drMembers);


            if (OfMeManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                functionReturn = false;
            }
            _OfmId = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString());

            #region SetMFNo
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            ReqManager.FindByCode(_OfReId);
            if (ReqManager.Count == 1)
            {
                #region   //با توجه به اختصاص پایه به صورت دستی این قسمت کامنت شد
                ////////////#region SaveImpGrade
                ////////////if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                ////////////{
                ////////////    int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
                ////////////    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
                ////////////    {
                ////////////        ReqManager[0].BeginEdit();
                ////////////        ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(_OfId, _OfReId);
                ////////////        ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ////////////        ReqManager[0]["ModifiedDate"] = DateTime.Now;
                ////////////        ReqManager[0].EndEdit();
                ////////////        ReqManager.Save();
                ////////////        ReqManager.DataTable.AcceptChanges();
                ////////////    }
                ////////////}
                ////////////#endregion
                #endregion
                int RequestType = Convert.ToInt32(ReqManager[0]["Type"]);
                if (RequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
                    || RequestType == (int)TSP.DataManager.OfficeRequestType.Revival
                    || RequestType == (int)TSP.DataManager.OfficeRequestType.Change)//صدور-تمدید-تغییرات
                {
                    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(txtMeNo.Text));
                    if (dtMj.Rows.Count > 0)
                    {

                        //int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                        int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                        int i = -1;
                        string MFNo = ReqManager[0]["MFNo"].ToString();
                        string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                        char[] Code = MFNoMajor[2].ToCharArray();

                        switch (ParentId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;

                        }
                        if (i != -1)
                        {
                            // Code[i] = '1';

                            MFNoMajor[2] = new string(Code);//Code.ToString();
                            ReqManager[0].BeginEdit();
                            ReqManager[0]["MFNo"] = string.Join("-", MFNoMajor);
                            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            ReqManager[0].EndEdit();
                            ReqManager.Save();
                        }
                    }

                }
            }

            #endregion

            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
            {
                int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
                int WfCode = -1;
                if (_Dprt == "Document")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                else if (_Dprt == "MemberShip")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                if (WfCode == -1)
                {
                    trans.CancelSave();
                    SetMessage(".خطایی در ذخیره انجام گرفته است");
                    return false;
                }
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                SetMessage(".خطایی در ذخیره انجام گرفته است");
                functionReturn = false;
            }
            else
            {
                trans.EndSave();
                SetMessage(" ذخیره انجام شد" + Alert);
                Session["IsEdited_OffAaza"] = true;
                RoundPanelOfficeMember.HeaderText = "ویرایش";
                _PageMode = "Edit";
                txtMeNo.ReadOnly = true;
                ComboType.ClientEnabled = false;
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
                    functionReturn = false;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    functionReturn = false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                functionReturn = false;
            }

        }
        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                if (Session["IsImgMember"].ToString() == "1")
                    EmzaSoource = Server.MapPath(Session["ImgMember_offMe"].ToString());
                //Session["MemberEmza2"] = Path.GetFileName(MeManager[0]["SignUrl"].ToString());
                //string ret = Path.GetRandomFileName() + ImageType.Extension;
                //string EmzaSoource = MeManager[0]["SignUrl"].ToString();
                //string EmzaTarget = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                //System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                string EmzaTarget = Server.MapPath("~/image/Office/Members/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                // Session["IsImgMember"] = 0;
            }
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }

    protected bool InsertOther()
    {
        bool functionReturn = true;
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(tr);

        tr.Add(OthManager);
        tr.Add(OfMeManager);
        //tr.Add(GradeManager);
        tr.Add(WorkFlowStateManager);

        string pathAx = "";
        bool chAxImg = false;
        bool chSignImg = false;
        _OfmId = -1;
        _PersonId = -1;

        try
        {
            DataRow drOthers = OthManager.NewRow();
            DataRow drMembers = OfMeManager.NewRow();


            drOthers["OtpId"] = 0;
            drOthers["FirstName"] = txtFirstName.Text;
            drOthers["FatherName"] = txtFatherName.Text;
            drOthers["LastName"] = txtLastName.Text;
            drOthers["IdNo"] = txtIdNo.Text;
            drOthers["SSN"] = txtSSN.Text;
            drOthers["BirthPlace"] = txtBirthPlace.Text;
            drOthers["BirthDate"] = txtBirthDate.Text;
            drOthers["FileNo"] = txtFileNo.Text;
            drOthers["FileNoDate"] = txtFileNoDate.Text;

            drOthers["Description"] = txtDesc.Text;
            if (txtTel.Text != "" && txtTel_pre.Text != "")
                drOthers["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
            else if (txtTel.Text != "")
                drOthers["Tel"] = txtTel.Text;
            drOthers["MobileNo"] = txtMobile.Text;
            drOthers["Address"] = txtAddress.Text;
            drOthers["UserId"] = Utility.GetCurrentUser_UserId();
            drOthers["ModifiedDate"] = DateTime.Now;
            drOthers["OtpCode"] = DBNull.Value;
            drOthers["OtpType"] = (int)TSP.DataManager.OtherPersonType.OtherPerson;
            drOthers["MjId"] = DBNull.Value;
            drOthers["MjName"] = DBNull.Value;

            if (Session["MemberImage2"] != null)
            {
                pathAx = Server.MapPath("~/image/Temp/");

                drOthers["ImageUrl"] = "~/Image/Office/Other/Ax/" + Path.GetFileName(Session["MemberImage2"].ToString());
                chAxImg = true;

            }

            OthManager.AddRow(drOthers);

            tr.BeginSave();

            if (OthManager.Save() > 0)
            {
                _PersonId = int.Parse(OthManager[0]["OtpId"].ToString());

                drMembers["OfId"] = _OfId;
                drMembers["PersonId"] = int.Parse(OthManager[0]["OtpId"].ToString());
                drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
                drMembers["OfReId"] = _OfReId;
                drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Otherperson; //3;

                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");

                    drMembers["SignUrl"] = "~/Image/Office/Other/Emza/" + Session["MemberEmza2"].ToString();
                    chSignImg = true;

                }


                if (drdPosition.Value != null)
                {

                    drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());
                }


                drMembers["StartDate"] = txtStartDate.Text;
                //drMembers["EndDate"] = txtEndDate_k.Text;
                drMembers["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);
                //if (ComboTime.Value != null)
                //    drMembers["IsFullTime"] = ComboTime.Value;
                if (ComboTime.Value != null)
                {
                    if (ComboTime.Value.ToString() == "1")
                        drMembers["IsFullTime"] = 1;
                    else
                        drMembers["IsFullTime"] = 0;
                }
                drMembers["Description"] = txtDesc.Text;

                if (!Utility.IsOfficeMemberConfirmRequestNeeded())
                {
                    drMembers["IsConfirm"] = 1;
                    drMembers["ConfirmDate"] = Utility.GetDateOfToday();
                }

                drMembers["UserId"] = Utility.GetCurrentUser_UserId();
                drMembers["ModifiedDate"] = DateTime.Now;
                OfMeManager.AddRow(drMembers);

                if (OfMeManager.Save() == 1)
                {
                    _OfmId = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString());

                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
                    {
                        //  int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
                        int WfCode = -1;
                        if (_Dprt == "Document")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                        else if (_Dprt == "MemberShip")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                        if (WfCode == -1)
                        {
                            SetOther();
                            tr.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                            return false;
                        }
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        SetOther();
                        tr.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        functionReturn = false;
                    }
                    else
                    {
                        tr.EndSave();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = " ذخیره انجام شد";
                        Session["IsEdited_OffAaza"] = true;
                        //ClearForm();
                        SetOther();
                        RoundPanelOfficeMember.HeaderText = "ویرایش";
                        _PageMode = "Edit";
                        //txtOtpCode.ReadOnly = true;
                        ComboType.ClientEnabled = false;

                    }

                }
                else
                {
                    tr.CancelSave();
                    SetOther();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return false;

                }
            }
            else
            {
                tr.CancelSave();
                SetOther();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return false;
            }

        }
        catch (Exception err)
        {
            tr.CancelSave();
            SetOther();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    functionReturn = false;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    functionReturn = false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                functionReturn = false;
            }

        }

        // if (chSignImg == true)
        // {
        try
        {
            if (Session["MemberEmza2"] != null)
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/Office/Other/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                //imgEmza.ImageUrl = Server.MapPath("~/image/Office/Other/Emza/") + Session["MemberEmza2"].ToString();
            }
        }
        catch (Exception)
        {
        }
        // }
        if (chAxImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["MemberImage2"].ToString());
                string EmzaTarget = Server.MapPath("~/image/Office/Other/Ax/") + Path.GetFileName(Session["MemberImage2"].ToString());
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);

            }
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }

    protected bool InsertKardanAndMemar()
    {
        bool functionReturn = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);

        bool chSignImg = false;
        string pathAx = "";
        string type = "-1";
        string ExpireDate = "";
        int TnReId = -1;
        string alert = "";
        #region Check Condition
        try
        {
            if (ComboType.Value == null)
            {
                SetMessage("نوع عضو را مجدداً وارد نماييد");
                return false;
            }
            type = ComboType.Value.ToString();
            if (_PersonId == -2)
            {
                SetMessage("کد عضویت را مجدداً وارد نمایید");
                return false;
            }
            int OtpId = _PersonId;

            if (type == "1")//----kardan
            {
                OfMeManager.FindOfficeActiveMembers(_OfId, (int)TSP.DataManager.OfficeMemberType.Kardan, 0, -1);
                for (int i = 0; i < OfMeManager.Count; i++)
                {
                    if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == OtpId && OfMeManager[i]["Active"].ToString() == "فعال")
                    {
                        SetMessage("اطلاعات وارد شده تکراری می باشد");
                        return false;
                    }
                }
            }
            else if (type == "2")//-----memar
            {
                OfMeManager.FindOfficeActiveMembers(_OfId, (int)TSP.DataManager.OfficeMemberType.Memar, 0, -1);
                for (int i = 0; i < OfMeManager.Count; i++)
                {
                    if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == OtpId && OfMeManager[i]["Active"].ToString() == "فعال")
                    {

                        SetMessage("اطلاعات وارد شده تکراری می باشد");
                        return false;
                    }
                }
            }


            OfMeManager.FindOffMemberByPersonId(OtpId, 1);
            if (OfMeManager.Count > 0)
            {
                string Msg = "";
                Msg = "عضو مورد نظر در شرکت ";
                Msg += Utility.IsDBNullOrNullValue(OfMeManager[0]["OfName"]) ? "دیگری " : OfMeManager[0]["OfName"].ToString();
                Msg += " مشغول به کار می باشد.در هنگام تایید این درخواست درصورت عدم خروج از شرکت نامبرده امکان تایید وجود نخواهد داشت";
                alert = Msg;
            }

            ReqManager.FindByCode(_OfReId);
            if (ReqManager.Count == 0)
            {
                SetMessage("خطایی در بازیابی اطلاعات صورت گرفته است.");
                return false;
            }
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
            {
                int OffType = Convert.ToInt32(ReqManager[0]["MFType"]);
                if (OffType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//tarah o nazer
                {
                    TechnicianRequestManager.FindLastVerion(OtpId, 1);
                    if (TechnicianRequestManager.Count == 1)
                    {
                        TnReId = Convert.ToInt32(TechnicianRequestManager[0]["TnReId"]);
                        ExpireDate = TechnicianRequestManager[0]["FileDate"].ToString();

                        if (!string.IsNullOrEmpty(ExpireDate))
                        {
                            if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                            {
                                SetMessage("امکان ثبت شخص مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        SetMessage("امکان ثبت شخص مورد نظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
                        return false;
                    }
                }

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return false;
        }
        #endregion
        try
        {
            trans.BeginSave();
            DataRow drMembers = OfMeManager.NewRow();

            drMembers["OfReId"] = _OfReId;
            drMembers["MfId"] = TnReId;

            if (type == "1")
            {
                drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Kardan;
                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");
                    drMembers["SignUrl"] = "~/Image/Office/Kardan/Emza/" + Session["MemberEmza2"].ToString();
                    chSignImg = true;
                }
            }
            else if (type == "2")
            {
                drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Memar;
                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");
                    drMembers["SignUrl"] = "~/Image/Office/Memar/Emza/" + Session["MemberEmza2"].ToString();
                    chSignImg = true;
                }
            }

            drMembers["PersonId"] = _PersonId;
            drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
            drMembers["OfId"] = _OfId;

            if (drdPosition.Value != null)
                drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());

            drMembers["StartDate"] = txtStartDate.Text;
            drMembers["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);

            if (ComboTime.Value != null)
            {
                if (ComboTime.Value.ToString() == "1")
                    drMembers["IsFullTime"] = 1;
                else
                    drMembers["IsFullTime"] = 0;
            }

            if (!Utility.IsOfficeMemberConfirmRequestNeeded())
            {
                drMembers["IsConfirm"] = 1;
                drMembers["ConfirmDate"] = Utility.GetDateOfToday();
            }

            drMembers["Description"] = txtDesc.Text;
            drMembers["UserId"] = Utility.GetCurrentUser_UserId();
            drMembers["ModifiedDate"] = DateTime.Now;
            drMembers["IsConfirm"] = 1;
            drMembers["ConfirmDate"] = Utility.GetDateOfToday();
            OfMeManager.AddRow(drMembers);


            if (OfMeManager.Save() <= 0)
            {
                SetKardanMemar();
                trans.CancelSave();
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return false;
            }
            _OfmId = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString());

            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
            {
                //  int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
                int WfCode = -1;
                if (_Dprt == "Document")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                else if (_Dprt == "MemberShip")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                if (WfCode == -1)
                {
                    SetKardanMemar();
                    trans.CancelSave();
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                    return false;
                }
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, _OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    SetMessage(".خطایی در ذخیره انجام گرفته است");
                    return false;
                }
            }


            trans.EndSave();
            SetMessage(" ذخیره انجام شد" + alert);
            Session["IsEdited_OffAaza"] = true;
            //ClearForm();
            RoundPanelOfficeMember.HeaderText = "ویرایش";
            _PageMode = "Edit";
            txtOtpCode.ReadOnly = true;
            ComboType.ClientEnabled = false;


        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    functionReturn = false;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    functionReturn = false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                functionReturn = false;
            }
        }
        if (chSignImg == true)
        {
            try
            {
                if (type == "1")
                {
                    string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                    string EmzaTarget = Server.MapPath("~/image/Office/Kardan/Emza/") + Session["MemberEmza2"].ToString();
                    System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                }
                else if (type == "2")
                {
                    string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                    string EmzaTarget = Server.MapPath("~/image/Office/Memar/Emza/") + Session["MemberEmza2"].ToString();
                    System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
                }
            }
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }
    #endregion

    protected void SetFNoValidation(Boolean Enable)
    {
        if (Enable == false)
        {
            txtFileNo.ValidationSettings.RequiredField.IsRequired = false;
            txtFileNo.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtFileNo.ValidationSettings.RegularExpression.ErrorText = "";
            txtFileNo.IsValid = true;
        }
        else
        {
            txtFileNo.ValidationSettings.RequiredField.IsRequired = true;
            txtFileNo.ValidationSettings.RequiredField.ErrorText = "شماره پروانه را وارد نمایید";
            txtFileNo.ValidationSettings.RegularExpression.ValidationExpression = "^\\d{1}-\\d{2}-\\d{3,5}";
            txtFileNo.ValidationSettings.RegularExpression.ErrorText = "شماره را با فرمت *****-**-*وارد نمایید";
        }
    }

    //protected void SetTellValidation(Boolean Enable)
    //{
    //    if (Enable == false)
    //    {
    //        txtMobile.ValidationSettings.RequiredField.IsRequired = false;
    //        txtMobile.ValidationSettings.RegularExpression.ValidationExpression = "";
    //        txtMobile.ValidationSettings.RegularExpression.ErrorText = "";
    //        txtMobile.IsValid = true;

        
    //        //txtTel_pre.ValidationSettings.RegularExpression.ErrorText = "";
    //        //txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "";
    //        //txtTel_pre.IsValid = true;

    //    }
    //    else
    //    {
    //        txtMobile.ValidationSettings.RequiredField.IsRequired = true;
    //        txtMobile.ValidationSettings.RequiredField.ErrorText = "شماره همراه را وارد نمایید";
    //        txtMobile.ValidationSettings.RegularExpression.ValidationExpression = "(0)\\d{10}";// "\\d{5,8}";
    //        txtMobile.ValidationSettings.RegularExpression.ErrorText = "این شماره صحیح نیست";


    //        //txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "(0)\\d{2,3}";
    //        //txtTel_pre.ValidationSettings.RegularExpression.ErrorText = "این شماره صحیح نیست";
         
    //    }
    //}

    //protected void SetValidationForOtherperson(Boolean Enable)
    //{
    //    if (Enable == false)
    //    {
    //        txtFatherName.ValidationSettings.RequiredField.IsRequired=
    //            txtBirthPlace.ValidationSettings.RequiredField.IsRequired
    //            = false;
    //        //PersianDateValidatorBirthDate.ControlToValidate = "";


    //    }
    //    else
    //    {
    //        txtFatherName.ValidationSettings.RequiredField.IsRequired =
    //             txtBirthPlace.ValidationSettings.RequiredField.IsRequired
    //             = true;
    //        //PersianDateValidatorBirthDate.ControlToValidate = "txtBirthDate";

    //        txtFatherName.ValidationSettings.RequiredField.ErrorText = "نام پدر را وارد نمایید";
    //    }
    //}

    #region SET Person
    private void SetControlsEnabledByMemberType()
    {
        if (ComboType.Value == null)
        {
            return;
        }
        int type = Convert.ToInt32(ComboType.Value);
        switch (type)
        {
            case (int)OfficeMemberType.Member: //0:
                SetMember();
                break;
            case (int)OfficeMemberType.Kardan: //1:
                SetKardanMemar();
                break;
            case (int)OfficeMemberType.Memar: //2:
                SetMemar();
                break;
            case (int)OfficeMemberType.OtherPerson: //3:
                SetOther();
                break;
        }
    }

    protected void SetMember()
    {
        lblMeNo.ClientVisible = true;
        txtMeNo.ClientVisible = true;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

        lblFileNo.ClientVisible = true;
        txtFileNo.ClientVisible = true;
        lblFileDate.ClientVisible = true;
        txtFileNoDate.Visible = true;
        lblFileNo.Text = "شماره پروانه اشتغال";

        txtFirstName.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtFileNo.ClientEnabled = false;
        txtFileNoDate.Enabled = false;

        txtTel.ClientEnabled = false;
        txtTel_pre.ClientEnabled = false;
        txtMobile.ClientEnabled = false;
        txtAddress.ClientEnabled = false;
        flp_Image.ClientVisible = false;
        ASPxLabel5.ClientVisible = false;
        ASPxLabel6.ClientVisible = false;
        ComboMjId.ClientVisible = false;
        txtMjName.ClientVisible = false;
        //ValidatorEnable(document.getElementById('<%=PersianDateValidator1.ClientID%>'),false);        
        flp_Image.ClientVisible = false;
        //SetTellValidation(false);
        SetFNoValidation(false);
        ASPxRoundPanelGrade.Visible = true;
    }

    protected void SetKardanMemar()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = true;
        lblOtpCode.ClientVisible = true;

        lblFileNo.ClientVisible = true;
        txtFileNo.ClientVisible = true;
        lblFileDate.ClientVisible = true;
        txtFileNoDate.Visible = true;
        lblFileNo.Text = "شماره پروانه اشتغال";

        txtFirstName.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtFileNo.ClientEnabled = false;
        txtFileNoDate.Enabled = false;

        txtTel.ClientEnabled = false;
        txtTel_pre.ClientEnabled = false;
        txtMobile.ClientEnabled = false;
        txtAddress.ClientEnabled = false;
        flp_Image.ClientVisible = false;

        ASPxLabel5.ClientVisible = true;
        ASPxLabel6.ClientVisible = true;
        ComboMjId.ClientVisible = true;
        txtMjName.ClientVisible = true;
        ComboMjId.ClientEnabled = false;
        txtMjName.ClientEnabled = false;
        //document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker",true);        
        //SetTellValidation(false);
        SetFNoValidation(false);
        ASPxRoundPanelGrade.Visible = true;

    }

    protected void SetMemar()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

        lblFileNo.ClientVisible = true;
        txtFileNo.ClientVisible = true;
        lblFileDate.ClientVisible = true;
        txtFileNoDate.Visible = true;

        txtFirstName.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtFileNo.ClientEnabled = true;
        txtFileNoDate.Enabled = false;
        lblFileNo.Text = "شماره پروانه اشتغال*";

        txtTel.ClientEnabled = false;
        txtTel_pre.ClientEnabled = false;
        txtMobile.ClientEnabled = false;
        txtAddress.ClientEnabled = false;
        flp_Image.ClientVisible = false;

        ASPxLabel5.ClientVisible = true;
        ASPxLabel6.ClientVisible = true;
        ComboMjId.ClientVisible = true;
        txtMjName.ClientVisible = true;
        ComboMjId.ClientEnabled = false;
        txtMjName.ClientEnabled = false;
        //document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker",true);                
        //SetTellValidation(false);
        SetFNoValidation(true);
        ASPxRoundPanelGrade.Visible = true;

    }

    protected void SetOther()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;
        lblFileNo.ClientVisible = false;
        txtFileNo.ClientVisible = false;
        lblFileDate.ClientVisible = false;
        txtFileNoDate.Visible = false;


        txtFirstName.ClientEnabled = true;
        txtLastName.ClientEnabled = true;
        txtFatherName.ClientEnabled = true;
        txtBirthDate.Enabled = true;
        txtBirthPlace.ClientEnabled = true;
        txtSSN.ClientEnabled = true;
        txtIdNo.ClientEnabled = true;

        txtTel.ClientEnabled = true;
        txtTel_pre.ClientEnabled = true;
        txtMobile.ClientEnabled = true;
        txtAddress.ClientEnabled = true;
        flp_Image.ClientVisible = true;
        ASPxLabel5.ClientVisible = true;
        ASPxLabel6.ClientVisible = true;
        ComboMjId.ClientVisible = true;
        txtMjName.ClientVisible = true;
        ComboMjId.ClientEnabled = true;
        txtMjName.ClientEnabled = true;
        ASPxRoundPanelGrade.Visible = false;
        //document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker",true);

        //SetTellValidation(true);
        SetFNoValidation(false);
    }
    #endregion

    #region WF Permission
    private void CheckWorkFlowPermissionForOffice()
    {
        if (_PageMode != "New")
            CheckWorkFlowPermissionForEditForOffice(_PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForOffice(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت ثبت اطلاعات اعضاي شركت را در جريان كار ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForOffice(string PageMode)
    {
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, _OfReId, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, _OfReId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2 > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForDoc()
    {
        if (_PageMode != "New")
            CheckWorkFlowPermissionForEditForDoc(_PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForDoc(string PageMode)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForDoc(string PageMode)
    {
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, _OfReId, (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, _OfReId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2 > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    #endregion

    protected string FindLockers(int Id, int MemberTypeId, int IsLock)
    {
        TSP.DataManager.LockHistoryManager LockHistoryManager = new TSP.DataManager.LockHistoryManager();
        return LockHistoryManager.FindLockers(Id, MemberTypeId, IsLock);

    }

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();


        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office

        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    private void CheckPermission()
    {
        if (_Dprt == "MemberShip")
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave1.Enabled = per.CanNew || per.CanEdit;
            if (_PageMode != "New" && !per.CanView)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            }
        }
        else if (_Dprt == "Document")
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave1.Enabled = per.CanNew || per.CanEdit;
            if (_PageMode != "New" && !per.CanView)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            }
        }

    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetShareHolderRequestSetting()
    {
         if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
            {
                drdPosition.Enabled = false;
                if (_PageMode == "New")
                {
                    drdPosition.DataBind();
                    drdPosition.SelectedIndex = drdPosition.Items.FindByValue(((int)TSP.DataManager.OfficePosition.ShareHolder).ToString()).Index;
                }
            }
    }
    #endregion

}
