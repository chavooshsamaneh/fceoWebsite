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

public partial class Office_OfficeInfo_OfficeMemberShow : System.Web.UI.Page
{
    DataTable dtGrade = null;
    Boolean IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");

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

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            ODBPosition.FilterParameters[0].DefaultValue = "0";
            HDOtpId.Value = null;

            Session["MemberEmza2"] = null;
            Session["MemberImage2"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("~/Office/OfficeMembershipRequest.aspx");
                return;
            }


            try
            {
                #region SetKey
                Session["IsImgMember"] = 0;
                Session["ImgMember_offMe"] = "";
                PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OffMemberId.Value = Server.HtmlDecode(Request.QueryString["OfmId"]).ToString();
                OffMeType.Value = Server.HtmlDecode(Request.QueryString["OfmType"]).ToString();
                OfPersonId.Value = Server.HtmlDecode(Request.QueryString["PersonId"]).ToString();
                //HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();



                string PageMode = Utility.DecryptQS(PgMode.Value);
                string OfId = Utility.DecryptQS(OfficeId.Value);
                string OfmId = Utility.DecryptQS(OffMemberId.Value);
                string OfmType = Utility.DecryptQS(OffMeType.Value);
                string PersonId = Utility.DecryptQS(OfPersonId.Value);
                //string Mode = Utility.DecryptQS(HDMode.Value);
                string OfReId = Utility.DecryptQS(OfficeRequest.Value);

                if (string.IsNullOrEmpty(PageMode))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                //TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
                //OfManager.FindByCode(int.Parse(OfId));
                //if (OfManager.Count > 0)
                //    lblOfName.Text = OfManager[0]["OfName"].ToString();


                #endregion

                #region SetMode
                switch (PageMode)
                {
                    case "View":
                        #region View
                        // Disable();
                        //lblNote.Visible = false;

                        if (string.IsNullOrEmpty(OfmId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }

                        btnSave.Enabled = false;
                        btnSave1.Enabled = false;

                        FillForm(int.Parse(OfmId), int.Parse(OfmType), int.Parse(PersonId));
                        ASPxRoundPanel2.HeaderText = "مشاهده";
                        ASPxRoundPanel2.Enabled = false;
                        flp_Emza.ClientVisible = false;

                        #endregion
                        break;

                    case "New":
                        #region New
                        ASPxRoundPanel2.HeaderText = "جدید";
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        #endregion
                        break;

                    case "Edit":
                        #region EditMode
                        txtMeNo.ReadOnly = true;
                        txtOtpCode.ReadOnly = true;

                        if (string.IsNullOrEmpty(OfmId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        if (string.IsNullOrEmpty(OfmType))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }

                        FillForm(int.Parse(OfmId), int.Parse(OfmType), int.Parse(PersonId));
                        ASPxRoundPanel2.HeaderText = "ویرایش";
                        ComboType.ClientEnabled = false;
                        #endregion
                        break;
                }

                #endregion

                TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                ReqManager.FindByCode(int.Parse(OfReId));
                if (ReqManager.Count > 0)
                {
                    if ((Convert.ToBoolean(ReqManager[0]["Requester"]) == true) || (ReqManager[0]["IsConfirm"].ToString() != "0")
                          || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)//Request From Member
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
                if (!string.IsNullOrEmpty(OfmId))
                {
                    MemManager.FindByCode(int.Parse(OfmId));
                    if (MemManager.Count == 1)
                    {
                        if (MemManager[0]["OfReId"].ToString() != OfReId)
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

        ComboType.DataBind();
        if (ComboType.SelectedItem != null && ComboType.SelectedItem.Value != null && Convert.ToInt32(ComboType.SelectedItem.Value) != 3)
        {
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtTel.ValidationSettings.RequiredField.IsRequired = false;
            txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "";
        }
        else
        {
            txtTel.ValidationSettings.RequiredField.IsRequired = true;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "\\d{5,8}";
            txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "(0)\\d{2,3}";
        }
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

        //if (!chbHaghEmza.Checked)
        //    Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");

    }

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
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                int MeId = int.Parse(txtMeNo.Text);

                txtAddress.Text = "";
                txtBirthDate.Text = "";
                txtBirthPlace.Text = "";
                txtDesc.Text = "";
                txtFatherName.Text = "";
                txtFileNo.Text = "";
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
                CustomAspxDevGridView1.DataSource = "";
                CustomAspxDevGridView1.DataBind();
                imgMember.ImageUrl = "";

                MeManager.FindByCode(MeId);
                if (MeManager.Count == 1)
                {
                    string LockName = "";
                    #region CheckLock
                    if (Convert.ToBoolean(MeManager[0]["IsLock"]))
                    {
                        LockName = FindLockers(MeId, (int)TSP.DataManager.LockMemberType.Member, 1);

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ";
                        // return;
                        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = false;

                    }
                    #endregion

                    txtAddress.Text = MeManager[0]["HomeAdr"].ToString();
                    txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
                    txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
                    txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                    txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                    txtFileNoDate.Text = MeManager[0]["FileDate"].ToString();
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                    if (dtMeFile.Rows.Count > 0)
                    {

                        string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
                        txtFileNoDate.Text = ExpireDate;
                    }
                    txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                    txtIdNo.Text = MeManager[0]["IdNo"].ToString();
                    txtLastName.Text = MeManager[0]["LastName"].ToString();
                    txtMobile.Text = MeManager[0]["MobileNo"].ToString();
                    txtSSN.Text = MeManager[0]["SSN"].ToString();
                    // txtTel.Text = MeManager[0]["HomeTel"].ToString();

                    if (MeManager[0]["HomeTel"].ToString() != "")
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
                        //string ret = Path.GetRandomFileName() + ImageType.Extension;
                        //string EmzaSoource = MeManager[0]["SignUrl"].ToString();
                        //string EmzaTarget = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                        //System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
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
                        ASPxRoundPanelGrade.ClientVisible = true;
                        int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                        CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, MeId, 0);
                        CustomAspxDevGridView1.KeyFieldName = "MfdId";
                        CustomAspxDevGridView1.DataBind();
                    }

                    HD_img["name"] = 1;
                    SetMember();
                    //ASPxRoundPanelGrade.ClientVisible = true;
                    //tblgr.Visible = false;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "چنين كد عضويتي وجود ندارد.كد عضويت را مجدداً وارد نماييد";
                    txtAddress.Text = "";
                    txtBirthDate.Text = "";
                    txtBirthPlace.Text = "";
                    txtDesc.Text = "";
                    txtFatherName.Text = "";
                    txtFileNo.Text = "";
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
                    CustomAspxDevGridView1.DataSource = "";
                    CustomAspxDevGridView1.DataBind();
                    imgMember.ImageUrl = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");


        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات عضو مورد نظر رخ داده است";
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
       
        //tblgr.Visible = true;
        txtMeNo.ReadOnly = false;
        txtOtpCode.ReadOnly = false;
        ComboType.ClientEnabled = true;
        OffMemberId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();

        ASPxRoundPanel2.Enabled = true;
        CustomAspxDevGridView1.Columns[CustomAspxDevGridView1.Columns.Count - 1].Visible = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
       

        string pageMode = Utility.DecryptQS(PgMode.Value);
        int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
        string PersonId = Utility.DecryptQS(OfPersonId.Value);
        string OfmId = Utility.DecryptQS(OffMemberId.Value);
        int OfmType = int.Parse(Utility.DecryptQS(OffMeType.Value));

        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
            return;

        if (string.IsNullOrEmpty(OfmId))
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
                txtMeNo.ReadOnly = true;
                txtOtpCode.ReadOnly = true;
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                ASPxRoundPanel2.Enabled = true;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["MemberImage2"] = null;
        Session["MemberEmza2"] = null;
        Session["MeGrade"] = null;
        Response.Redirect("OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
            + "&Dprt=" + HiddenFieldOffice["Department"].ToString());//+ "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"]

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        try
        {
            bool changeImgEmza = false;
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
            String imgFolderName = "";

            if (ComboType.Value != null)
            {
                int type = int.Parse(ComboType.Value.ToString());
                switch (type)
                {
                    case 0:
                        SetMember();
                        break;
                    case 1:
                        SetKardanMemar();
                        break;
                    case 2:
                        SetKardanMemar();
                        break;
                    case 3:
                        SetOther();
                        break;
                }
            }


            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            if (cmbHasEfficientGrade.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "وضعیت امتیاز عضو را انتخاب نمایید";

                return;
            }
            if (PageMode == "Edit")
            {
                string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                    return;
           
                if (string.IsNullOrEmpty(OfmId) || string.IsNullOrEmpty(PersonId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                {
                    DataTable dt = OfManager.SelectOfficeManagerByOfId(int.Parse(Utility.DecryptQS(OfficeId.Value)), 0, OfReId);
                    if (dt.Rows.Count != 0)
                    {
                        if (dt.Rows[0]["PersonId"].ToString() != PersonId)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "برای شرکت مورد نظر مدیر عامل ثبت شده است";
                            return;
                        }
                    }
                }
                if (ComboType.Value != null)
                {
                    int type = int.Parse(ComboType.Value.ToString());
                    switch (type)
                    {
                        case 0:
                            changeImgEmza = EditMember(int.Parse(OfmId));
                            imgFolderName = "~/Image/Office/Members/Emza/";
                            break;
                        case 1:
                            changeImgEmza = EditKardanAndMemar(int.Parse(OfmId), int.Parse(PersonId), type);
                            imgFolderName = "~/Image/Office/Kardan/Emza/";
                            break;
                        case 2:
                            changeImgEmza = EditKardanAndMemar(int.Parse(OfmId), int.Parse(PersonId), type);
                            imgFolderName = "~/Image/Office/Kardan/Emza/";
                            break;
                        case 3:
                            changeImgEmza = EditOther(int.Parse(OfmId), int.Parse(PersonId), type);
                            imgFolderName = "~/Image/Office/Other/Emza/";
                            break;
                    }
                    if (Session["MemberEmza2"] != null && !String.IsNullOrEmpty(imgFolderName) && changeImgEmza)
                    {
                        imgEmza.ClientVisible = true;
                        imgEmza.ImageUrl = imgFolderName + Session["MemberEmza2"].ToString();
                    }

                }

            }
            else if (PageMode == "New")
            {
                if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
                {
                    DataTable dt = OfManager.SelectOfficeManagerByOfId(int.Parse(Utility.DecryptQS(OfficeId.Value)), 0, OfReId);
                    if (dt.Rows.Count != 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای شرکت مورد نظر مدیر عامل ثبت شده است";
                        return;
                    }
                }

                if (ComboType.Value != null)
                {
                    int type = int.Parse(ComboType.Value.ToString());
                    switch (type)
                    {
                        case 0:
                            changeImgEmza = InsertMember();
                            imgFolderName = "~/Image/Office/Members/Emza/";
                            break;
                        case 1:
                            changeImgEmza = InsertKardanAndMemar();
                            imgFolderName = "~/Image/Office/Kardan/Emza/";
                            break;
                        case 2:
                            changeImgEmza = InsertKardanAndMemar();
                            imgFolderName = "~/Image/Office/Kardan/Emza/";
                            break;
                        case 3:
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
                        CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));
                        ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                        arr[1] = 1;
                        Session["OffMenuArrayList"] = arr;
                    }
                }
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["MeGrade"] != null)
        {
            dtGrade = (DataTable)Session["MeGrade"];

            DataRow dr = dtGrade.NewRow();

            try
            {
                if (ComboResp.Value == null)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "صلاحیت را انتخاب نمایید";
                    return;
                }
                if (ComboGrade.Value == null)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "پایه را انتخاب نمایید";
                    return;
                }

                CustomAspxDevGridView1.DataSource = dtGrade;
                CustomAspxDevGridView1.DataBind();

                if (ComboType.Value != null)
                    if (ComboType.Value.ToString() == "1")//کاردان
                        if (ComboResp.Value.ToString() == ((int)TSP.DataManager.DocumentResponsibilityType.Design).ToString())
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت صلاحیت طراحی برای کاردان وجود ندارد";
                            return;
                        }

                for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
                {
                    DataRowView drv = (DataRowView)CustomAspxDevGridView1.GetRow(i);

                    if (drv["ResName"].ToString() == ComboResp.SelectedItem.Text)//&& drv["GrdName"].ToString() == ComboGrade.SelectedItem.Text)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "صلاحیت وارد شده تکراری می باشد";
                        return;
                    }
                }

                dr["ResId"] = ComboResp.Value;
                dr["ResName"] = ComboResp.SelectedItem.Text;
                dr["GrdId"] = ComboGrade.Value;
                dr["GrdName"] = ComboGrade.SelectedItem.Text;
                if (ComboMjId.Value != null)
                    dr["MjName"] = ComboMjId.SelectedItem.Text;

                dr[4] = 0;
                dtGrade.Rows.Add(dr);
                CustomAspxDevGridView1.DataSource = dtGrade;
                CustomAspxDevGridView1.DataBind();

                //Session["MeGrade"] = null;

                ComboGrade.SelectedIndex = -1;
                ComboResp.SelectedIndex = -1;
                //ASPxRoundPanelGrade.ClientVisible = true;
                SetKardanMemar();
                if (HDOtpId.Value != null)
                {
                    txtAddress.ClientEnabled = false;
                    txtBirthDate.Enabled = false;
                    txtBirthPlace.ClientEnabled = false;
                    txtFatherName.ClientEnabled = false;
                    txtFirstName.ClientEnabled = false;
                    txtIdNo.ClientEnabled = false;
                    txtLastName.ClientEnabled = false;
                    txtMobile.ClientEnabled = false;
                    txtOtpCode.ClientEnabled = false;
                    txtSSN.ClientEnabled = false;
                    txtTel.ClientEnabled = false;
                    txtTel_pre.ClientEnabled = false;
                    txtFileNo.ClientEnabled = false;
                }
            }
            catch (Exception)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
    }

    protected void txtSSN_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        try
        {
            if (!string.IsNullOrEmpty(txtSSN.Text))
            {
                OthManager.FindBySSN(txtSSN.Text);
                if (OthManager.Count > 0)
                {
                    HDOtpId.Value = OthManager[0]["OtpId"].ToString();

                    if (ComboType.Value != null)
                    {
                        if (ComboType.Value.ToString() == "1" || ComboType.Value.ToString() == "2")
                        {
                            SetKardanMemar();
                        }
                        else if (ComboType.Value.ToString() == "3")
                            SetOther();
                    }
                    else
                        return;
                    txtAddress.ClientEnabled = false;
                    txtBirthDate.Enabled = false;
                    txtBirthPlace.ClientEnabled = false;
                    txtFatherName.ClientEnabled = false;
                    txtFirstName.ClientEnabled = false;
                    txtIdNo.ClientEnabled = false;
                    txtLastName.ClientEnabled = false;
                    txtMobile.ClientEnabled = false;
                    txtOtpCode.ClientEnabled = false;
                    txtSSN.ClientEnabled = false;
                    txtTel.ClientEnabled = false;
                    txtTel_pre.ClientEnabled = false;
                    txtFileNo.ClientEnabled = false;
                    txtFileNoDate.ClientEnabled = false;

                    txtAddress.Text = OthManager[0]["Address"].ToString();
                    txtBirthDate.Text = OthManager[0]["BirthDate"].ToString();
                    txtBirthPlace.Text = OthManager[0]["BirthPlace"].ToString();
                    txtFatherName.Text = OthManager[0]["FatherName"].ToString();
                    txtFileNo.Text = OthManager[0]["FileNo"].ToString();
                    txtFileNoDate.Text = OthManager[0]["FileNoDate"].ToString();

                    txtFirstName.Text = OthManager[0]["FirstName"].ToString();
                    txtIdNo.Text = OthManager[0]["IdNo"].ToString();
                    txtLastName.Text = OthManager[0]["LastName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(OthManager[0]["MjName"]))
                        txtMjName.Text = OthManager[0]["MjName"].ToString();
                    txtMobile.Text = OthManager[0]["MobileNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(OthManager[0]["OtpCode"]))
                        txtOtpCode.Text = OthManager[0]["OtpCode"].ToString();

                    if (!Utility.IsDBNullOrNullValue(OthManager[0]["Tel"]))
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
                    if (!Utility.IsDBNullOrNullValue(OthManager[0]["MjId"]))
                    {
                        ComboMjId.DataBind();
                        ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthManager[0]["MjId"].ToString());
                    }
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد ملی را وارد نمایید";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        CustomAspxDevGridView1.DataSource = (DataTable)Session["MeGrade"];
        CustomAspxDevGridView1.DataBind();

        int Id = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            Id = CustomAspxDevGridView1.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtGrade = (DataTable)Session["MeGrade"];
            dtGrade.Rows.Find(e.Keys["Id"]).Delete();

            Session["MeGrade"] = dtGrade;
            CustomAspxDevGridView1.DataSource = (DataTable)Session["MeGrade"];
            CustomAspxDevGridView1.DataBind();
            dtGrade = (DataTable)Session["MeGrade"];


        }
    }

    protected void txtOtpCode_TextChanged(object sender, EventArgs e)
    {
        //imgEmza.ClientVisible = false;
        // flp_Emza.ClientVisible = true;
        TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        try
        {
            this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = true;

            if (ComboType.Value != null)
            {
                if (!string.IsNullOrEmpty(txtOtpCode.Text))
                {
                    txtAddress.Text = "";
                    txtBirthDate.Text = "";
                    txtBirthPlace.Text = "";
                    txtDesc.Text = "";
                    txtFatherName.Text = "";
                    txtFileNo.Text = "";
                    txtFileNoDate.Text = "";
                    txtFirstName.Text = "";
                    txtIdNo.Text = "";
                    txtLastName.Text = "";
                    txtMobile.Text = "";
                    txtSSN.Text = "";
                    txtStartDate.Text = "";
                    txtTel.Text = "";
                    txtTel_pre.Text = "";
                    ComboMjId.DataBind();
                    ComboMjId.SelectedIndex = -1;
                    txtMjName.Text = "";
                    imgMember.ImageUrl = "../../Images/person.png";

                    string type = ComboType.Value.ToString();
                    if (type == "1")
                        OthPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text, (int)TSP.DataManager.OtherPersonType.Kardan);
                    else if (type == "2")
                        OthPersonManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text, (int)TSP.DataManager.OtherPersonType.Memar);

                    if (OthPersonManager.Count == 1)
                    {
                        HDOtpId.Value = OthPersonManager[0]["OtpId"].ToString();
                        if (Convert.ToBoolean(OthPersonManager[0]["InActive"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "شخص مورد نظر غير فعال مي باشد";
                            SetKardanMemar();
                            //return;                            
                            this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = false;
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
                            SetKardanMemar();
                            //return;
                            this.ViewState["BtnSave"] = btnSave.Enabled = btnSave1.Enabled = false;

                        }
                        #endregion
                        txtAddress.Text = OthPersonManager[0]["Address"].ToString();
                        txtBirthDate.Text = OthPersonManager[0]["BirthDate"].ToString();
                        txtBirthPlace.Text = OthPersonManager[0]["BirthPlace"].ToString();
                        txtFatherName.Text = OthPersonManager[0]["FatherName"].ToString();
                        txtFileNo.Text = OthPersonManager[0]["FileNo"].ToString();
                        txtFileNoDate.Text = OthPersonManager[0]["FileNoDate"].ToString();
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
                        if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["MjId"]))
                        {
                            ComboMjId.DataBind();
                            ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthPersonManager[0]["MjId"].ToString());
                        }
                        txtMjName.Text = OthPersonManager[0]["MjName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["ImageUrl"].ToString()))
                            imgMember.ImageUrl = OthPersonManager[0]["ImageUrl"].ToString();

                        //if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["LicenceImgUrl"]))
                        //{
                        //    imgEmza.ClientVisible = true;
                        //     flp_Emza.ClientVisible = false;
                        //    imgEmza.ImageUrl = OthPersonManager[0]["LicenceImgUrl"].ToString();
                        //    Session["MemberEmza2"] = Path.GetFileName(OthPersonManager[0]["LicenceImgUrl"].ToString());
                        //}
                        //else
                        //{
                        //     flp_Emza.ClientVisible = true;
                        //    imgEmza.ClientVisible = false;
                        //    Session["MemberEmza2"] = null;
                        //}

                        CustomAspxDevGridView1.DataSource = GradeManager.SelectGradeByOtpId(Convert.ToInt32(HDOtpId.Value));
                        CustomAspxDevGridView1.KeyFieldName = "MGId";
                        CustomAspxDevGridView1.DataBind();

                        SetKardanMemar();
                        HD_img["name"] = 1;
                    }
                    else
                    {
                        txtAddress.Text = "";
                        txtBirthDate.Text = "";
                        txtBirthPlace.Text = "";
                        txtDesc.Text = "";
                        txtFatherName.Text = "";
                        txtFileNo.Text = "";
                        txtFileNoDate.Text = "";
                        txtFirstName.Text = "";
                        txtIdNo.Text = "";
                        txtLastName.Text = "";
                        txtMobile.Text = "";
                        txtSSN.Text = "";
                        txtStartDate.Text = "";
                        txtTel.Text = "";
                        txtTel_pre.Text = "";
                        ComboMjId.DataBind();
                        ComboMjId.SelectedIndex = -1;
                        txtMjName.Text = "";
                        imgMember.ImageUrl = "../../Images/person.png";

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "چنين كد عضويتي وجود ندارد.كد عضويت را مجدداً وارد نماييد";
                        SetKardanMemar();

                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "كد عضويت را مجدداً وارد نماييد";
                    SetKardanMemar();

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع عضو را مجدداً انتخاب نماييد";
                SetKardanMemar();

            }
        }
        catch (Exception err)
        {
            SetKardanMemar();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطايي در بازخواني اطلاعات رخ داده است";
        }
    }

    protected void CallbackMeType_Callback1(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ComboType.DataBind();
        if (ComboType.SelectedItem != null && ComboType.SelectedItem.Value != null && Convert.ToInt32(ComboType.SelectedItem.Value) != 3)
        {
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtTel.ValidationSettings.RequiredField.IsRequired = false;
            txtTel.ValidationSettings.RegularExpression.ErrorText = "";
            txtTel_pre.ValidationSettings.RegularExpression.ErrorText = "";
            txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtTel.ClientEnabled = false;
            txtTel_pre.ClientEnabled = false;
            txtTel_pre.Text = "";
            txtTel.Text = "";
            txtTel.IsValid = true;
            txtTel_pre.IsValid = true;
        }
        else
        {
            txtTel.ValidationSettings.RequiredField.IsRequired = true;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "\\d{5,8}";
            txtTel_pre.ValidationSettings.RegularExpression.ValidationExpression = "(0)\\d{2,3}";
            txtTel.ValidationSettings.RegularExpression.ErrorText = "این شماره صحیح نیست";
            txtTel_pre.ValidationSettings.RegularExpression.ErrorText = "این شماره صحیح نیست";
            txtTel.ClientEnabled = true;
            txtTel_pre.ClientEnabled = true;
            txtTel_pre.Text = "";
            txtTel.Text = "";
        }
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        CustomAspxDevGridView1.DataSource = "";
        CustomAspxDevGridView1.DataBind();
    }
    #endregion

    #region Methods

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

    protected void ClearForm()
    {
        Session["IsImgMember"] = 0;
        Session["ImgMember_offMe"] = "";
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDesc.Text = "";
        txtFatherName.Text = "";
        txtFileNo.Text = "";
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
        txtMeNo.Text = "";
        txtOtpCode.Text = "";
        ComboType.DataBind();
        ComboType.SelectedIndex = 0;
        lblOtpCode.ClientVisible = false;
        lblMeNo.ClientVisible = true;


        drdPosition.DataBind();
        drdPosition.SelectedIndex = -1;
        ComboTime.DataBind();
        ComboTime.SelectedIndex = 1;
        chbHaghEmza.Checked = false;

        imgEmza.ImageUrl = "";
        imgEmza.ClientVisible = false;

        imgMember.ImageUrl = "../../Images/person.png";

        //Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");
        flp_Emza.ClientVisible = true;

        HD_Emza["name"] = 0;
        HD_img["name"] = 0;

        ComboMjId.DataBind();
        ComboMjId.SelectedIndex = -1;
        txtMjName.Text = "";


        //dtGrade = (DataTable)Session["MeGrade"];
        //dtGrade.Rows.Clear();
        DataTable dt = new DataTable();
        dt.Rows.Clear();
        CustomAspxDevGridView1.DataSource = dt;
        CustomAspxDevGridView1.DataBind();

        ASPxRoundPanelGrade.ClientVisible = false;

        cmbHasEfficientGrade.SelectedIndex = 1;
    }

    #region FILL
    protected void FillForm(int OfmId, int OfmType, int PersonId)
    {
        switch (OfmType)
        {

            case 1://member
                ComboType.SelectedIndex = 0;
                SetMember();
                FillMember(OfmId, PersonId);
                break;
            case 2://kardan
                ComboType.SelectedIndex = 1;
                SetKardanMemar();
                FillOthers(OfmId, PersonId);
                break;
            case 3://Other
                ComboType.SelectedIndex = 3;
                SetOther();
                FillOthers(OfmId, PersonId);
                break;
            case 4://Memar
                ComboType.SelectedIndex = 2;
                SetKardanMemar();
                FillOthers(OfmId, PersonId);
                break;
        }
    }

    protected void FillMember(int OfmId, int PersonId)
    {

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        MeManager.FindByCode(PersonId);

        if (MeManager.Count == 1)
        {
            try
            {
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                txtLastName.Text = MeManager[0]["LastName"].ToString();
                txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                txtIdNo.Text = MeManager[0]["IdNo"].ToString();
                txtSSN.Text = MeManager[0]["SSN"].ToString();
                txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
                txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
                txtMeNo.Text = PersonId.ToString();
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
                OfMeManager.FindByCode(OfmId);
                if (OfMeManager.Count == 1)
                {

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
                    {
                        imgEmza.ClientVisible = false;

                    }
                    txtDesc.Text = OfMeManager[0]["Description"].ToString();
                }


                //MeFileManager.SelectLastVersion(PersonId, 0);
                //if (MeFileManager.Count > 0)
                //{
                //int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["MfId"]))
                {
                    int MfId = Convert.ToInt32(OfMeManager[0]["MfId"]);

                    MeFileManager.FindByCode(MfId, 0);
                    txtFileNo.Text = MeFileManager[0]["MFNo"].ToString();
                    txtFileNoDate.Text = MeFileManager[0]["ExpireDate"].ToString();

                    CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                    CustomAspxDevGridView1.KeyFieldName = "MfdId";
                    CustomAspxDevGridView1.DataBind();
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";

            }
        }
    }

    protected void FillOthers(int OfmId, int PersonId)
    {
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();

        OthManager.FindByCode(PersonId);

        if (OthManager.Count == 1)
        {
            try
            {
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

                //txtDesc.Text = OthManager[0]["Description"].ToString();
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

                OfMeManager.FindByCode(OfmId);
                if (OfMeManager.Count == 1)
                {

                    ComboTime.DataBind();
                    if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                        ComboTime.SelectedIndex = 1;
                    else
                        ComboTime.SelectedIndex = 0;
                    cmbHasEfficientGrade.SelectedIndex = cmbHasEfficientGrade.Items.FindByValue(Convert.ToInt32(OfMeManager[0]["HasEfficientGrade"]).ToString()).Index;

                    drdPosition.DataBind();
                    drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    // txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();
                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    if (chbHaghEmza.Checked)
                    {
                        //imgEmza.ClientVisible = true;
                        //lbEmza.ClientVisible = true;
                        imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();
                        HD_Emza["name"] = 1;
                        imgEmza.ClientVisible = true;
                    }
                    else
                    {
                        //imgEmza.ClientVisible = true;
                        // lbEmza.ClientVisible = true;
                    }

                    txtDesc.Text = OfMeManager[0]["Description"].ToString();

                }
                if (Convert.ToInt32(OthManager[0]["OtpType"]) == (int)TSP.DataManager.OtherPersonType.Kardan || Convert.ToInt32(OthManager[0]["OtpType"]) == (int)TSP.DataManager.OtherPersonType.Memar)
                {
                    ASPxRoundPanelGrade.ClientVisible = true;
                    //tblgr.Visible = false;

                    TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
                    CustomAspxDevGridView1.DataSource = GradeManager.SelectGradeByOtpId(PersonId);
                    CustomAspxDevGridView1.KeyFieldName = "MGId";
                    CustomAspxDevGridView1.DataBind();

                    //GradeManager.FindByOtpId(PersonId);
                    //dtGrade = (DataTable)Session["MeGrade"];
                    //for (int i = 0; i < GradeManager.Count; i++)
                    //{
                    //    DataRow dr = dtGrade.NewRow();
                    //    dr["GrdId"] = GradeManager[i]["GrdId"];
                    //    dr["GrdName"] = GradeManager[i]["GrdName"].ToString();
                    //    dr["ResId"] = GradeManager[i]["ResId"].ToString();
                    //    dr["ResName"] = GradeManager[i]["ResName"].ToString();
                    //    dr["MjName"] = OthManager[0]["MajorName"].ToString();


                    //    dr[4] = 1;
                    //    dr[5] = GradeManager[i][0];
                    //    dtGrade.Rows.Add(dr);

                    //}
                    //dtGrade.AcceptChanges();
                    //CustomAspxDevGridView1.DataSource = dtGrade;
                    //CustomAspxDevGridView1.DataBind();
                }

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";

            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }
    #endregion

    #region EDIT
    protected bool EditOther(int OfmId, int PersonId, int type)
    {
        bool functionReturn = true;
        string pathAx = "";

        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(tr);

        tr.Add(OthManager);
        tr.Add(OfMeManager);
        tr.Add(WorkFlowStateManager);


        bool chAxImg = false;
        bool chSignImg = false;

        try
        {

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

            tr.BeginSave();

            if (OthManager.Save() > 0)
            {
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

                if (OfMeManager.Save() == 1)
                {
                    int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                    tr.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                }
                else
                {
                    tr.CancelSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return false;

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return false;
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
                string EmzaTarget = Server.MapPath("~/image/Office/Other/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }

    protected bool EditKardanAndMemar(int OfmId, int PersonId, int type)
    {
        if (cmbHasEfficientGrade.Value == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت امتیاز عضو را انتخاب نمایید.";
            return false;
        }
        bool functionReturn = true;
        string pathAx = "";

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(tr);

        tr.Add(OfMeManager);
        tr.Add(WorkFlowStateManager);

        bool chSignImg = false;

        try
        {

            tr.BeginSave();

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

            if (OfMeManager.Save() == 1)
            {
                int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                tr.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
            }
            else
            {
                tr.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return false;
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
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }

    protected bool EditMember(int OfmId)
    {
       
        bool functionReturn = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(ReqManager);

        string pathAx = "";
        bool chSignImg = false;

        try
        {
            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count == 1)
            {
                OfMeManager[0].BeginEdit();
                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");
                    OfMeManager[0]["SignUrl"] = "~/Image/Office/Members/Emza/" + Session["MemberEmza2"].ToString();
                    chSignImg = true;
                }


                if (drdPosition.Value != null)
                {
                    OfMeManager[0]["OfpId"] = int.Parse(drdPosition.Value.ToString());
                }
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
                trans.BeginSave();
                if (OfMeManager.Save() > 0)
                {
                    int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
                    int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                    ReqManager.FindByCode(OfReId);
                    if (ReqManager.Count == 1)
                    {
                        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                        {
                            int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
                            if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
                            {
                                ReqManager[0].BeginEdit();
                                ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(OfId, OfReId);
                                ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                ReqManager[0]["ModifiedDate"] = DateTime.Now;
                                ReqManager[0].EndEdit();
                                ReqManager.Save();
                                ReqManager.DataTable.AcceptChanges();
                            }
                        }
                    }

                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    trans.CancelSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                    functionReturn = false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                functionReturn = false;
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
                string EmzaTarget = Server.MapPath("~/image/Office/Members/Emza/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception)
            {
            }
        }
        return functionReturn;
    }
    #endregion

    #region INSERT
    protected bool InsertOther()
    {
        bool functionReturn = true;
        //TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();   
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        //TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(tr);

        tr.Add(OthManager);
        tr.Add(OfMeManager);
        //tr.Add(GradeManager);
        tr.Add(WorkFlowStateManager);

        string pathAx = "";
        bool chAxImg = false;
        bool chSignImg = false;
        int OfmType = -1;
        int OfmId = -1;
        int PersonId = -1;

        try
        {
            DataRow drOthers = OthManager.NewRow();
            DataRow drMembers = OfMeManager.NewRow();

            //if (drdPosition.Value != null)
            //{
            //    if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
            //    {
            //        SetOther();
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = "امکان ثبت شخص مورد نظر به عنوان مدیر عامل شرکت وجود ندارد";
            //        return false;

            //    }
            //}

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
                PersonId = int.Parse(OthManager[0]["OtpId"].ToString());
                int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                drMembers["OfId"] = int.Parse(Utility.DecryptQS(OfficeId.Value));
                drMembers["PersonId"] = int.Parse(OthManager[0]["OtpId"].ToString());
                drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
                drMembers["OfReId"] = OfReId;
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
                    OfmId = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString());
                    OfmType = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmType"].ToString());

                    tr.EndSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    //ClearForm();
                    SetOther();
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    PgMode.Value = Utility.EncryptQS("Edit");

                    OffMemberId.Value = Utility.EncryptQS(OfmId.ToString());
                    //txtOtpCode.ReadOnly = true;
                    ComboType.ClientEnabled = false;
                    OffMeType.Value = Utility.EncryptQS(OfmType.ToString());
                    OfPersonId.Value = Utility.EncryptQS(PersonId.ToString());
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

    protected bool InsertMember()
    {
        if (cmbHasEfficientGrade.Value == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت امتیاز عضو را انتخاب نمایید.";
            return false;
        }
        bool functionReturn = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DocMemberFileManager.ClearBeforeFill = true;

        trans.Add(OfMeManager);
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);

        bool chSignImg = false;

        try
        {
            string pathAx = "";
            int MemberFileId = -1;
            int MeId = -1;
            //    string ExpireDate = "";

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
            int OfmType = -1;
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                MeId = int.Parse(txtMeNo.Text);

                #region Check MemberConditions
                OfMeManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Member, 0, -1);
                for (int i = 0; i < OfMeManager.Count; i++)
                {
                    if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == MeId && OfMeManager[i]["Active"].ToString() == "فعال")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return false;
                    }
                }

                OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
                if (OfMeManager.Count > 0)
                {
                    int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                    DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
                    if (dtEngOffReq.Rows.Count > 0)
                    {
                        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در دفتر ";
                        str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = (str);
                        trans.CancelSave();
                        return false;
                    }
                }
                OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
                if (OfMeManager.Count > 0)
                {
                    int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                    if (OfIdMember != OfId)
                    {
                        DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfIdMember, 0, MeId);
                        if (dtOffReq.Rows.Count > 0)
                        {
                            string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در شرکت ";
                            str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                            str += " مشغول به کار می باشد";
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = (str);
                            trans.CancelSave();
                            return false;
                        }
                    }
                }


                DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
                if (DocMemberFileManager.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.";
                    return false;
                }
                //DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId);
                //if (DocMemberFileManager.Count > 0)
                //{
                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز نظارت می باشد.";
                //    return;
                //}
                #endregion

                #region Check MemberFileConditions
                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازیابی اطلاعات صورت گرفته است.";
                    return false;
                }
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                {
                    int OffType = Convert.ToInt32(ReqManager[0]["MFType"]);
                    if (OffType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//tarah o nazer
                    {
                        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                        if (dtMeFile.Rows.Count > 0)
                        {
                            MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
                            ///*******Comment*******/////
                            ///اگر نوع شرکت اجرا می توانست باشد کد تغییر می کند تا با نوع شرکت مجوز فرد چک شود.در ارسال به مرحله بعد چک می شود
                            //////***************

                            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
                            {
                                string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                                if (Department == "Document")
                                {
                                    Boolean HasDes = true;
                                    Boolean HasObs = true;
                                    DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                                    if (dtMeDetail.Rows.Count == 0)
                                    {
                                        HasDes = false;
                                        //this.DivReport.Visible = true;
                                        //this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت طراحی ندارد  ";
                                        //return;
                                    }
                                    DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
                                    if (dtMeDetail2.Rows.Count == 0)
                                    {
                                        HasObs = false;
                                        //this.DivReport.Visible = true;
                                        //this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت نظارت ندارد  ";
                                        //return;
                                    }
                                    if (HasObs == false && HasDes == false)
                                    {
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر بایستی حداقل یکی از صلاحیت های نظارت یا طراحی را دارا باشد.";
                                        return false;
                                    }
                                }
                                //else if (OffType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//ejra
                                //{
                                //    DataTable dtMeDetail3 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
                                //    if (dtMeDetail3.Rows.Count == 0)
                                //    {
                                //        this.DivReport.Visible = true;
                                //        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت اجرا ندارد  ";
                                //        return false;
                                //    }
                                //}

                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.";
                                return false;
                            }

                            if (!string.IsNullOrEmpty(ExpireDate))
                            {
                                if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                                    return false;
                                }
                            }

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                            return false;
                        }
                    }
                }
                #endregion
                //--------------------------------------------
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
                return false;
            }


            DataRow drMembers = OfMeManager.NewRow();

            #region Comment
            //if (ComboType.Value != null)
            //{
            //    int type = int.Parse(ComboType.Value.ToString());
            //    if (type == 0)//Member
            //    {

            //    }
            //    else if (type == 1)//Kardan
            //    {
            //        drMembers["OfmType"] = 2;
            //        drMembers["PersonId"] = int.Parse(txtMeNo.Text);

            //        if (Session["MemberEmza2"] != null)
            //        {
            //            pathAx = Server.MapPath("~/image/Temp/");
            //            drMembers["SignUrl"] = "~/Image/Office/Kardan/Emza/" + Session["MemberEmza2"].ToString();
            //            chSignImg = true;
            //        }

            //    }
            //    else if (type == 2)//Memar
            //    {
            //        drMembers["OfmType"] = 4;
            //        drMembers["PersonId"] = int.Parse(txtMeNo.Text);

            //        if (Session["MemberEmza2"] != null)
            //        {
            //            pathAx = Server.MapPath("~/image/Temp/");
            //            drMembers["SignUrl"] = "~/Image/Office/Memar/Emza/" + Session["MemberEmza2"].ToString();
            //            //chSignImg = true;
            //        }

            //    }
            //    else if (type == 3)//Other
            //    {
            //        drMembers["OfmType"] = 3;
            //        drMembers["PersonId"] = int.Parse(txtMeNo.Text);

            //        if (Session["MemberEmza2"] != null)
            //        {
            //            pathAx = Server.MapPath("~/image/Temp/");
            //            drMembers["SignUrl"] = "~/Image/Office/Other/Emza/" + Session["MemberEmza2"].ToString();
            //            chSignImg = true;
            //        }
            //    }

            //}
            #endregion

            drMembers["OfReId"] = OfReId;

            if (MemberFileId == -1)
            {
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count == 1)
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            }
            drMembers["MfId"] = MemberFileId;

            drMembers["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
            drMembers["PersonId"] = int.Parse(txtMeNo.Text);
            drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
            if (Session["MemberEmza2"] != null)
            {
                pathAx = Server.MapPath("~/image/Temp/");
                drMembers["SignUrl"] = "~/Image/Office/Members/Emza/" + Session["MemberEmza2"].ToString();
                chSignImg = true;
            }

            drMembers["OfId"] = OfId;

            if (drdPosition.Value != null)
            {
                //if (drdPosition.Value.ToString() == "1" || drdPosition.Value.ToString() == "8")
                //{
                //    DataTable dt = OfManager.SelectOfficeManagerByOfId(OfId);
                //    if (dt.Rows.Count != 0)
                //    {
                //        this.DivReport.Visible = true;
                //        this.LabelWarning.Text = "برای شرکت مورد نظر مدیر مسئول ثبت شده است";
                //        return;
                //    }
                //    else
                //        drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());
                //}
                //else
                drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());
            }
            drMembers["StartDate"] = txtStartDate.Text;
            //drMembers["EndDate"] = txtMeEndDate.Text;
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

            trans.BeginSave();
            if (OfMeManager.Save() == 1)
            {
                OfmId = OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString();
                OfmType = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmType"].ToString());

                #region SetMFNo
                TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count == 1)
                {
                    #region SaveImpGrade
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                    {
                        int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
                        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
                        {
                            ReqManager[0].BeginEdit();
                            ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(OfId, OfReId);
                            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            ReqManager[0]["ModifiedDate"] = DateTime.Now;
                            ReqManager[0].EndEdit();
                            ReqManager.Save();
                            ReqManager.DataTable.AcceptChanges();
                        }
                    }
                    #endregion

                    int RequestType = Convert.ToInt32(ReqManager[0]["Type"]);
                    if (RequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
                        || RequestType == (int)TSP.DataManager.OfficeRequestType.Revival
                        || RequestType == (int)TSP.DataManager.OfficeRequestType.Change)//صدور-تمدید-تغییرات
                    {
                        DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(txtMeNo.Text));
                        if (dtMj.Rows.Count > 0)
                        {

                            int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
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

                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                //ClearForm();
                ASPxRoundPanel2.HeaderText = "ویرایش";
                PgMode.Value = Utility.EncryptQS("Edit");

                OffMemberId.Value = Utility.EncryptQS(OfmId);
                txtMeNo.ReadOnly = true;
                ComboType.ClientEnabled = false;
                OffMeType.Value = Utility.EncryptQS(OfmType.ToString());
                OfPersonId.Value = Utility.EncryptQS(txtMeNo.Text);
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                functionReturn = false;
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
        int OfmType = -1;

        try
        {

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            //if (drdPosition.Value != null)
            //{
            //    if (Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.Manager || Convert.ToInt32(drdPosition.Value) == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)
            //    {
            //        SetKardanMemar();
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = "امکان ثبت شخص مورد نظر به عنوان مدیر عامل شرکت وجود ندارد";
            //        return false;
            //    }
            //}
            if (ComboType.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع عضو را مجدداً وارد نماييد";
                return false;
            }
            type = ComboType.Value.ToString();
            if (!string.IsNullOrEmpty(HDOtpId.Value))
            {
                int OtpId = int.Parse(HDOtpId.Value);

                if (type == "1")//----kardan
                {
                    OfMeManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Kardan, 0, -1);
                    for (int i = 0; i < OfMeManager.Count; i++)
                    {
                        if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == OtpId && OfMeManager[i]["Active"].ToString() == "فعال")
                        {
                            SetKardanMemar();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                            return false;
                        }
                    }
                }
                else if (type == "2")//-----memar
                {
                    OfMeManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Memar, 0, -1);
                    for (int i = 0; i < OfMeManager.Count; i++)
                    {
                        if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == OtpId && OfMeManager[i]["Active"].ToString() == "فعال")
                        {
                            SetKardanMemar();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                            return false;
                        }
                    }
                }


                OfMeManager.FindOffMemberByPersonId(OtpId, 1);
                if (OfMeManager.Count > 0)
                {
                    SetKardanMemar();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت عضو در در این شرکت وجود ندارد.عضو مورد نظر در شرکت ";
                    this.LabelWarning.Text += Utility.IsDBNullOrNullValue(OfMeManager[0]["OfName"]) ? "دیگری " : OfMeManager[0]["OfName"].ToString();
                    this.LabelWarning.Text += " مشغول به کار می باشد";
                    return false;
                }

                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازیابی اطلاعات صورت گرفته است.";
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
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت شخص مورد نظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                        return false;
                    }
                }

                DataRow drMembers = OfMeManager.NewRow();

                drMembers["OfReId"] = OfReId;
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

                drMembers["PersonId"] = int.Parse(HDOtpId.Value);
                drMembers["HasEfficientGrade"] = Convert.ToInt32(cmbHasEfficientGrade.Value);
                drMembers["OfId"] = OfId;

                if (drdPosition.Value != null)
                    drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());

                drMembers["StartDate"] = txtStartDate.Text;
                //drMembers["EndDate"] = txtMeEndDate.Text;
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

                trans.BeginSave();
                if (OfMeManager.Save() == 1)
                {
                    OfmId = OfMeManager[OfMeManager.Count - 1]["OfmId"].ToString();
                    OfmType = int.Parse(OfMeManager[OfMeManager.Count - 1]["OfmType"].ToString());

                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    //ClearForm();
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    PgMode.Value = Utility.EncryptQS("Edit");

                    OffMemberId.Value = Utility.EncryptQS(OfmId);
                    txtOtpCode.ReadOnly = true;
                    ComboType.ClientEnabled = false;
                    OffMeType.Value = Utility.EncryptQS(OfmType.ToString());
                    OfPersonId.Value = Utility.EncryptQS(HDOtpId.Value.ToString());
                }
                else
                {
                    SetKardanMemar();
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    functionReturn = false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
                return false;
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

    #region SET
    protected void SetMember()
    {
        lblMeNo.ClientVisible = true;
        txtMeNo.ClientVisible = true;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

        txtFirstName.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtFileNo.ClientEnabled = false;
        txtFileNoDate.ClientEnabled = false;

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
        ASPxRoundPanelGrade.ClientVisible = true;
        //tblgr.Visible = false;
        flp_Image.ClientVisible = false;
    }

    protected void SetKardanMemar()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = true;
        lblOtpCode.ClientVisible = true;

        txtFirstName.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtFileNo.ClientEnabled = false;
        txtFileNoDate.ClientEnabled = false;

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

        ASPxRoundPanelGrade.ClientVisible = true;
        //tblgr.Visible = true;

    }

    protected void SetOther()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

        txtFirstName.ClientEnabled = true;
        txtLastName.ClientEnabled = true;
        txtFatherName.ClientEnabled = true;
        txtBirthDate.Enabled = true;
        txtBirthPlace.ClientEnabled = true;
        txtSSN.ClientEnabled = true;
        txtIdNo.ClientEnabled = true;
        txtFileNo.ClientEnabled = true;
        txtFileNoDate.ClientEnabled = true;

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
        //document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker",true);

    }
    #endregion

    #region WF Permission
    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                    return true;
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }

    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                    return true;
                              
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
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
    #endregion
}
