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

public partial class Members_EngOffice_EngOfficeMemberInsert : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");

        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

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
            ODBPosition.FilterParameters[0].DefaultValue = "1";

            Session["IsEdited_OffAaza"] = false;

            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                OffMemberId.Value = Server.HtmlDecode(Request.QueryString["OfmId"]).ToString();
                OfPersonId.Value = Server.HtmlDecode(Request.QueryString["PersonId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            //TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            //OfManager.FindByCode(int.Parse(OfId));
            //if (OfManager.Count > 0)
            //    lblOfName.Text = OfManager[0]["OfName"].ToString();

            switch (PageMode)
            {
                case "View":
                    lblNote.Visible = false;

                    if (string.IsNullOrEmpty(OfmId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;

                    FillMember(int.Parse(OfmId), int.Parse(PersonId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    ASPxRoundPanel2.Enabled = false;


                    break;


                case "New":

                    ASPxRoundPanel2.HeaderText = "جدید";

                    break;

                case "Edit":

                    txtMeNo.ReadOnly = true;
                    if (string.IsNullOrEmpty(OfmId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }


                    FillMember(int.Parse(OfmId), int.Parse(PersonId));

                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    break;


            }

            CheckWorkFlowPermission();

            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(int.Parse(EOfId));
            if (FileManager.Count > 0)
            {
                if ((Convert.ToBoolean(FileManager[0]["Requester"])) || (FileManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
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
                    if (MemManager[0]["OfReId"].ToString() != EOfId)
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



            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave1.Enabled = (bool)this.ViewState["BtnSave"];

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        lblNote.Visible = true;
        txtMeNo.ReadOnly = false;

        OffMemberId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        ASPxRoundPanel2.Enabled = true;


        btnSave.Enabled = true;
        btnSave1.Enabled = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        lblNote.Visible = true;

        string pageMode = Utility.DecryptQS(PgMode.Value);
        string PersonId = Utility.DecryptQS(OfPersonId.Value);
        string OfmId = Utility.DecryptQS(OffMemberId.Value);


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
                btnSave.Enabled = true;
                btnSave1.Enabled = true;
                this.ViewState["BtnSave"] = btnSave.Enabled;


                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                ASPxRoundPanel2.Enabled = true;

            }

        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&EOfId=" + EngFileId.Value);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        try
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);

            TSP.DataManager.EngOfficeManager EngOfManager = new TSP.DataManager.EngOfficeManager();

            switch (PageMode)
            {
                case "Edit":
                    if (string.IsNullOrEmpty(OfmId) || string.IsNullOrEmpty(PersonId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    if (drdPosition.Value.ToString() == "4")
                    {
                        DataTable dt = EngOfManager.SelectEngOfficeManagerByOfId(int.Parse(EngOfId));
                        if (dt.Rows.Count != 0)
                        {
                            if (dt.Rows[0]["PersonId"].ToString() != PersonId)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "برای شرکت مورد نظر مدیر مسئول ثبت شده است";
                                return;
                            }
                        }
                    }
                    EditMember(int.Parse(OfmId));
                    break;
                case "New":
                    if (drdPosition.Value.ToString() == "4")
                    {
                        DataTable dt = EngOfManager.SelectEngOfficeManagerByOfId(int.Parse(EngOfId));
                        if (dt.Rows.Count != 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "برای شرکت مورد نظر مدیر مسئول ثبت شده است";
                            return;
                        }
                    }
                    InsertMember();

                    break;
            }

        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
        }
    }
    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        try
        {
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                int MeId = int.Parse(txtMeNo.Text);
                MeManager.FindByCode(MeId);
                if (MeManager.Count == 1)
                {
                    txtAddress.Text = MeManager[0]["HomeAdr"].ToString();
                    txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
                    txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
                    txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                    txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                    txtFileNoDate.Text = MeManager[0]["FileDate"].ToString();
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

                    if (!string.IsNullOrEmpty(MeManager[0]["ImageUrl"].ToString()))
                        imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();


                    if (!string.IsNullOrEmpty(MeManager[0]["LastMjId"].ToString()))
                    {
                        ComboMjId.DataBind();
                        ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(MeManager[0]["LastMjId"].ToString());
                        txtMjName.Text = MeManager[0]["LastLiName"].ToString();
                    }

                    MeFileManager.SelectLastVersion(MeId, 0);
                    if (MeFileManager.Count > 0)
                    {
                        int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                        CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, MeId, 0);
                        CustomAspxDevGridView1.DataBind();
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات عضو مورد نظر رخ داده است";
        }
    }

    protected void InsertMember()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOfficeManager EngOfManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);


        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileManager.ClearBeforeFill = true;

        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(WorkFlowStateManager);

        bool chSignImg = false;

        try
        {
            string pathAx = "";

            string PageMode = Utility.DecryptQS(PgMode.Value);
            //string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            int EngOfId = int.Parse(Utility.DecryptQS(EngOfficeId.Value));
            int EOfId = int.Parse(Utility.DecryptQS(EngFileId.Value));
            int MemberFileId = -1;

            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                int MeId = int.Parse(txtMeNo.Text);
                OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
                if (OfMeManager.Count > 0)
                {
                    int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                    if (EngOfIdMember != EngOfId)
                    {
                        DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
                        if (dtEngOffReq.Rows.Count > 0)
                        {
                            string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در دفتر ";
                            str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                            str += " مشغول به کار می باشد";
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = (str);
                            trans.CancelSave();
                            return;
                        }
                    }
                }
                OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
                if (OfMeManager.Count > 0)
                {
                    int OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                    DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfId, 0, MeId);
                    if (dtOffReq.Rows.Count > 0)
                    {
                        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در شرکت ";
                        str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = (str);
                        trans.CancelSave();
                        return;
                    }
                }

                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                    ///*******Comment*******/////
                    ///اگر نوع دفتر اجرا می توانست باشد کد تغییر می کند تا با نوع دفتر مجوز فرد چک شود
                    //////***************
                    if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
                    {
                        DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                        if (DocMemberFileMajorManager.Count <= 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("رشته موضوع پروانه شخص انتخاب شده نامشخص است");
                            return;
                        }
                        //اگر رشته موضوع پروانه نقشه برداری/ترافیک و یا شهرسازی باشد می توانند بدون داشتن صلاحیت طراحی عضو دفتر شوند
                        if (Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Mapping
                            && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Traffic
                            && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Urbanism)
                        {
                            DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                            if (dtMeDetail.Rows.Count == 0)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = ("امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت طراحی ندارد");
                                trans.CancelSave();
                                return;
                            }
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.");
                        trans.CancelSave();
                        return;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار تایید شده نمی باشد.");
                    trans.CancelSave();
                    return;
                }
                DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
                if (DocMemberFileManager.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.");
                    trans.CancelSave();
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت را مجدداً وارد نمایید";
                return;
            }

            DataRow drMembers = OfMeManager.NewRow();

            drMembers["OfReId"] = EOfId;
            drMembers["MfId"] = MemberFileId;
            drMembers["OfId"] = EngOfId;
            drMembers["OfKind"] = 1;
            drMembers["OfmType"] = 1;
            drMembers["PersonId"] = int.Parse(txtMeNo.Text);

            if (Session["MemberEmza2"] != null)
            {
                pathAx = Server.MapPath("~/image/Temp/");
                drMembers["SignUrl"] = "~/Image/EngOffice/Members/" + Session["MemberEmza2"].ToString();
                chSignImg = true;
            }


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

            drMembers["Description"] = txtDesc.Text;
            drMembers["UserId"] = Utility.GetCurrentUser_UserId();
            drMembers["ModifiedDate"] = DateTime.Now;
            OfMeManager.AddRow(drMembers);

            trans.BeginSave();
            if (OfMeManager.Save() == 1)
            {
                string OfmId = OfMeManager[0]["OfmId"].ToString();

                #region SetMFNo
                TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
                FileManager.FindByCode(EOfId);
                if (FileManager.Count == 1)
                {

                    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(txtMeNo.Text));
                    if (dtMj.Rows.Count > 0)
                    {

                        int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                        int i = -1;
                        string MFNo = FileManager[0]["FileNo"].ToString();
                        string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                        char[] Code = MFNoMajor[2].ToCharArray();

                        switch (MjId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = MjId.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;

                        }
                        if (i != -1)
                        {
                            // Code[i] = '1';

                            MFNoMajor[2] = new string(Code);//Code.ToString();
                            FileManager[0].BeginEdit();
                            FileManager[0]["FileNo"] = string.Join("-", MFNoMajor);
                            FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            FileManager[0].EndEdit();
                            FileManager.Save();
                        }
                    }


                }

                #endregion

                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, EOfId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_MeId(), 1);
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    Session["IsEdited_OffAaza"] = true;
                    PgMode.Value = Utility.EncryptQS("Edit");
                    EngFileId.Value = Utility.EncryptQS(OfmId);
                }
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
        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/EngOffice/Members/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception)
            {
            }
        }
    }
    protected void EditMember(int OfmId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        trans.Add(OfMeManager);
        trans.Add(WorkFlowStateManager);


        string pathAx = "";
        bool chSignImg = false;

        try
        {
            // OfMeManager.FindByCode(OfmId);
            OfMeManager.selectEngOfficeMember(-1, -1, OfmId);

            if (OfMeManager.Count == 1)
            {
                OfMeManager[0].BeginEdit();
                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");
                    OfMeManager[0]["SignUrl"] = "~/Image/EngOffice/Members/" + Session["MemberEmza2"].ToString();
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

                OfMeManager[0]["Description"] = txtDesc.Text;
                OfMeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OfMeManager[0]["ModifiedDate"] = DateTime.Now;
                OfMeManager[0].EndEdit();
                trans.BeginSave();
                if (OfMeManager.Save() > 0)
                {
                    int EOfId = int.Parse(Utility.DecryptQS(EngFileId.Value));
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeMember;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, EOfId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_MeId(), 1);
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        trans.EndSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                        Session["IsEdited_OffAaza"] = true;
                    }
                }
                else
                {
                    trans.CancelSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
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
        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/EngOffice/Members/") + Session["MemberEmza2"].ToString();
                System.IO.File.Copy(EmzaSoource, EmzaTarget, true);
            }
            catch (Exception)
            {
            }
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
                txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                txtMeNo.Text = PersonId.ToString();
                txtMobile.Text = MeManager[0]["MobileNo"].ToString();
                //txtTel.Text = MeManager[0]["HomeTel"].ToString();
                txtAddress.Text = MeManager[0]["HomeAdr"].ToString();

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

                if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]))
                    imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();

                if (!Utility.IsDBNullOrNullValue(MeManager[0]["SignUrl"]))
                    imgSign.ImageUrl = MeManager[0]["SignUrl"].ToString();

                if (!string.IsNullOrEmpty(MeManager[0]["LastMjId"].ToString()))
                {
                    ComboMjId.DataBind();
                    ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(MeManager[0]["LastMjId"].ToString());
                    txtMjName.Text = MeManager[0]["LastLiName"].ToString();
                }

                //OfMeManager.FindByCode(OfmId);
                OfMeManager.selectEngOfficeMember(-1, -1, OfmId);
                if (OfMeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["OfpId"]))
                    {
                        drdPosition.DataBind();
                        drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
                    }

                    ComboTime.DataBind();
                    if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                        ComboTime.SelectedIndex = 1;
                    else
                        ComboTime.SelectedIndex = 0;

                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    //txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();
                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    if (chbHaghEmza.Checked)
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
                MeFileManager.SelectLastVersion(PersonId, 0);
                if (MeFileManager.Count > 0)
                {
                    int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                    CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                    //CustomAspxDevGridView1.KeyFieldName = "MfdId";
                    CustomAspxDevGridView1.DataBind();
                }


            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";

            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر کرده است";
        }
    }
    protected void ClearForm()
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
        txtMeNo.Text = "";

        drdPosition.DataBind();
        drdPosition.SelectedIndex = -1;
        ComboTime.DataBind();
        ComboTime.SelectedIndex = 1;
        chbHaghEmza.Checked = false;

        imgEmza.ImageUrl = "";
        imgEmza.ClientVisible = false;

        imgMember.ImageUrl = "../../../Images/person.gif";
        imgSign.ImageUrl = "../../../Images/noimage.gif";
        Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");

        HD_Emza["name"] = 0;

        ComboMjId.DataBind();
        ComboMjId.SelectedIndex = -1;
        txtMjName.Text = "";

    }
    bool CheckLocker()
    {
        int Meid = Utility.GetCurrentUser_MeId();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Meid);
        if (Convert.ToBoolean(MemberManager[0]["IsLock"]))
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string MemberLockers = lockHistoryManager.FindLockers(Meid, 0, 1);

            string lockers = Utility.GetFormattedObject(MemberLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return false;
        }
        return true;
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

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            // CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string EOfId = Utility.DecryptQS(EngFileId.Value);
        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(EOfId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
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
}
