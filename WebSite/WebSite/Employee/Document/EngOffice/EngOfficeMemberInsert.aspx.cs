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

public partial class Employee_Document_EngOffice_EngOfficeMemberInsert : System.Web.UI.Page
{
    string _PageMode
    {
        get
        {
            return PgMode.Value;
        }
        set
        {
            PgMode.Value = value;
        }
    }
    int _OfmId
    {
        get
        {
            try
            {
                return Convert.ToInt32(OffMemberId.Value);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["OfmId"].ToString()));
            }
        }
        set
        {

            OffMemberId.Value = value.ToString();

        }
    }
    int _PersonId
    {
        get
        {
            return Convert.ToInt32(OfPersonId.Value);
        }
        set
        {
            OfPersonId.Value = value.ToString();
        }
    }
    int _EngFileId
    {
        get
        {
            try
            {
                return Convert.ToInt32(EngFileId.Value);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EOfId"].ToString()));
            }
        }
        set
        {
            EngFileId.Value = value.ToString();
        }
    }
    int _EngOfficeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(EngOfficeId.Value);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EngOfId"].ToString()));
            }
        }
        set
        {
            EngOfficeId.Value = value.ToString();

        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            HD_Emza["Url"] = "";
            ODBPosition.FilterParameters[0].DefaultValue = "1";
            Session["IsEdited_OffAaza"] = false;

            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOfficeMember.aspx");
                return;
            }

            #region Check Permissions
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForEngOffice(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (!per.CanView)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
                return;
            }
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave1.Enabled = per.CanNew || per.CanEdit;
            #endregion

            #region SetKeys
            try
            {
                _PageMode = Utility.DecryptQS(Request.QueryString["aPageMode"].ToString());
                _EngOfficeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EngOfId"].ToString()));
                _OfmId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["OfmId"].ToString()));
                _PersonId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PersonId"].ToString()));
                _EngFileId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EOfId"].ToString()));

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            #endregion

            #region SetPageModes
            switch (_PageMode)
            {
                case "View":
                    #region SetViewMode
                    lblNote.Visible = false;

                    if (_OfmId == null || _OfmId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;

                    FillMember(_OfmId, _PersonId);
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    ASPxRoundPanel2.Enabled = false;
                    #endregion
                    break;
                case "New":
                    #region SetNewMode
                    ASPxRoundPanel2.HeaderText = "جدید";
                    #endregion
                    break;
                case "Edit":
                    #region SetEditeMode
                    txtMeNo.ReadOnly = true;
                    if (_OfmId == null || _OfmId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    FillMember(_OfmId, _PersonId);
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    #endregion
                    break;
            }

            CheckWorkFlowPermission();
            #endregion

            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(_EngFileId);
            if (FileManager.Count > 0)
            {
                if ((Convert.ToBoolean(FileManager[0]["Requester"]) == false) || (FileManager[0]["IsConfirm"].ToString() != "0")
                    || (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo))//Request From Member
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
            if (_OfmId != null && _OfmId != -1)
            {
                MemManager.selectEngOfficeMember(_OfmId);
                if (MemManager.Count == 1)
                {
                    if (Convert.ToInt32(MemManager[0]["OfReId"]) != _EngFileId && !CheckIfCanEditOldInformations(Convert.ToInt32(MemManager[0]["OfReId"])))
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

        imgEmza.ImageUrl = HD_Emza["Url"].ToString();
        if (Session["Selfreported"] != null)
            ImageSelfreported.NavigateUrl = Session["Selfreported"].ToString();
    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        try
        {
            if (string.IsNullOrEmpty(txtMeNo.Text))
            {
                ShowMessage("کد عضویت را مجدداً وارد نمایید");
                return;
            }
            int MeId = int.Parse(txtMeNo.Text);
            ClearForm();
            txtMeNo.Text = MeId.ToString();
            MeManager.FindByCode(MeId);
            if (MeManager.Count != 1)
            {
                ShowMessage("کد عضویت وارد شده معتبر نمی باشد");
                return;
            }

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
                GridViewMemberDocRes.DataSource = MeFileDetailManager.SelectById(MfId, MeId, 0);
                GridViewMemberDocRes.DataBind();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات عضو مورد نظر رخ داده است");
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        lblNote.Visible = true;
        txtMeNo.ReadOnly = false;

        _OfmId = -1;
        _PageMode = "New";
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        ASPxRoundPanel2.Enabled = true;

        TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForEngOffice(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanNew;
        btnSave1.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblNote.Visible = true;

            //string pageMode = Utility.DecryptQS(PgMode.Value);            
            int OriginalEOfId = Convert.ToInt32(Utility.DecryptQS(EngFileIdOrigin.Value));

            if (_OfmId != -1)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    if (_EngFileId != OriginalEOfId && (!CheckIfCanEditOldInformations(OriginalEOfId)))
                    {
                        ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
                        return;
                    }

                    if (!CheckPermitionForEdit(_EngFileId))
                    {
                        ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.");
                        return;
                    }


                    TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForEngOffice(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    txtMeNo.ReadOnly = true;
                    btnSave.Enabled = per.CanEdit;
                    btnSave1.Enabled = per.CanEdit;
                    this.ViewState["BtnSave"] = btnSave.Enabled;

                    _PageMode = "Edit";
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    ASPxRoundPanel2.Enabled = true;

                }

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;

        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMember.aspx?EngOfId=" + Utility.EncryptQS(_EngOfficeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&EOfId=" + Utility.EncryptQS(_EngFileId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //string PageMode = Utility.DecryptQS(PgMode.Value);                        
            TSP.DataManager.EngOfficeManager EngOfManager = new TSP.DataManager.EngOfficeManager();
            switch (_PageMode)
            {
                case "New":
                    if (drdPosition.Value.ToString() == "4")
                    {
                        DataTable dt = EngOfManager.SelectEngOfficeManagerByOfId(_EngOfficeId);
                        if (dt.Rows.Count != 0)
                        {
                            ShowMessage("پیش از این برای دفتر مورد نظر مدیر مسئول ثبت شده است");
                            return;
                        }
                    }
                    InsertMember();
                    break;
                case "Edit":
                    if (_OfmId == -1 || _PersonId == -1)
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    if (drdPosition.Value.ToString() == "4")
                    {
                        DataTable dt = EngOfManager.SelectEngOfficeManagerByOfId(_EngOfficeId);
                        if (dt.Rows.Count != 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["PersonId"]) != _PersonId)
                            {
                                ShowMessage("پیش از این برای دفتر مورد نظر مدیر مسئول ثبت شده است");
                                return;
                            }
                        }
                    }
                    EditMember(_OfmId);
                    break;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    protected void flp_Emza_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveEmza(e.UploadedFile, "Emza");

        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void FileUploadSelfreported_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {

        try
        {
            e.CallbackData = SaveEmza(e.UploadedFile, "Selfreported");

        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
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
        chbHasGasCert.Checked = false;

        imgEmza.ImageUrl = "";
        imgEmza.ClientVisible = false;

        imgMember.ImageUrl = "../../../Images/person.png";
        imgSign.ImageUrl = "../../../Images/noimage.gif";
        ImageSelfreported.NavigateUrl = "";
        // Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");

        HD_Emza["name"] = 0;
        HD_Emza["Url"] = "";

        ComboMjId.DataBind();
        ComboMjId.SelectedIndex = -1;
        txtMjName.Text = "";

        GridViewMemberDocRes.DataSource = "";
        GridViewMemberDocRes.DataBind();
    }

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();        
        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
        int PermissionEmpDoc = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || PermissionEmpDoc>0)
            return true;
        else
            return false;

    }

    private void CheckWorkFlowPermission()
    {
        if (_PageMode != "New")
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1; int PermisssionDocEmp = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, (int)TSP.DataManager.WorkFlows.EngOfficeConfirming, _EngFileId, (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
        PermisssionDocEmp = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, (int)TSP.DataManager.WorkFlows.EngOfficeConfirming,_EngFileId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || PermisssionDocEmp>0)
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

    protected string SaveEmza(UploadedFile uploadedFile, string UploadName)
    {
        string ret = "";
        switch (UploadName)
        {
            case "Emza":
                #region
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
                #endregion
                break;
            case "Selfreported":
                #region
                if (uploadedFile.IsValid)
                {
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = _EngOfficeId + "slfRep" + _OfmId + "slfRep" + Path.GetRandomFileName() + ImageType.Extension;
                    } while (File.Exists(MapPath("~/Image/EngOffice/Selfreported/") + ret) == true);
                    string tempFileName = "~/Image/EngOffice/Selfreported/" + ret;
                    uploadedFile.SaveAs(MapPath(tempFileName), true);
                    Session["Selfreported"] = tempFileName;


                    //do
                    //{
                    //    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    //    ret = _EngOfficeId.ToString() + "InqofMe" + _EngFileId.ToString() + "InqofMe" + Path.GetRandomFileName() + ImageType.Extension;
                    //} while (File.Exists(MapPath("~/image/EngOffice/InqueryMembers/") + ret) == true);
                    //tempFileName = "~/image/EngOffice/InqueryMembers/" + ret;
                    //uploadedFile.SaveAs(MapPath(tempFileName), true);
                    //Session["ImgInqueryMembers"] = tempFileName;
                }


                break;

                #endregion
        }
        return ret;
    }

    protected void InsertMember()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(WorkFlowStateManager);
        string Alert = "";
        bool chSignImg = false;

        try
        {
            string pathAx = "";

            if (string.IsNullOrEmpty(txtMeNo.Text))
            {
                ShowMessage("کد عضویت را مجدداً وارد نمایید");
                return;
            }
            int MeId = int.Parse(txtMeNo.Text);
            //********* Check Conditions*********
            //***********************************
            ArrayList Result = TSP.DataManager.OfficeMemberManager.CheckEngOfficeMembershipCondition(MeId, _EngOfficeId);
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return;
            }
            Alert = Result[1].ToString();
            int MemberFileId = int.Parse(Result[2].ToString());
            if (MemberFileId <= 0)
            {
                ShowMessage("خطایی در بازخوانی اطلاعات پروانه شخص ایجاد شده است");
                return;
            }
            //***********************************
            //***********************************

            trans.BeginSave();
            DataRow drMembers = OfMeManager.NewRow();
            drMembers["OfReId"] = _EngFileId;
            drMembers["MfId"] = MemberFileId;
            drMembers["OfId"] = _EngOfficeId;
            drMembers["OfKind"] = 1;
            drMembers["OfmType"] = 1;
            drMembers["PersonId"] = int.Parse(txtMeNo.Text);


            if (Session["MemberEmza2"] != null)
            {
                pathAx = Server.MapPath("~/image/Temp/");
                drMembers["SignUrl"] = "~/Image/EngOffice/Members/" + Session["MemberEmza2"].ToString();
                chSignImg = true;
            }

            if (Session["Selfreported"] != null)
            {
                drMembers["SelfreportedImageURL"] = Session["Selfreported"].ToString();
                ImageSelfreported.NavigateUrl = Session["Selfreported"].ToString();
            }

            if (drdPosition.Value != null)
                drMembers["OfpId"] = int.Parse(drdPosition.Value.ToString());

            drMembers["StartDate"] = txtStartDate.Text;
            //drMembers["EndDate"] = txtMeEndDate.Text;
            drMembers["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);

            drMembers["HasGasCert"] = Convert.ToByte(chbHasGasCert.Checked);

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

            if (OfMeManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            int OfmId = Convert.ToInt32(OfMeManager[0]["OfmId"]);

            #region SetMFNo
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            FileManager.FindByCode(_EngFileId);
            if (FileManager.Count == 1)
            {

                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(txtMeNo.Text));
                if (dtMj.Rows.Count > 0)
                {

                    int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                    //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                    int i = -1;
                    string MFNo = FileManager[0]["FileNo"].ToString();
                    if (string.IsNullOrEmpty(MFNo))
                    {
                        ShowMessage("شماره پروانه دفتر نامشخص می باشد");
                        trans.CancelSave();
                        return;
                    }
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
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);//  (int)TSP.DataManager.TableCodes.EngOffFile;
                int UpdateTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);//(int)TSP.DataManager.TableCodes.OfficeMember;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, _EngFileId, UpdateTableType, "افزودن عضو به دفتر", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete) + " " + Alert);
            Session["IsEdited_OffAaza"] = true;
            _PageMode = "Edit";
            _OfmId = OfmId;
            ASPxRoundPanel2.HeaderText = "ویرایش";
            Session["Selfreported"] = null;
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
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }

        }
        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/EngOffice/Members/") + Session["MemberEmza2"].ToString();
                System.IO.File.Move(EmzaSoource, EmzaTarget);
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
            OfMeManager.selectEngOfficeMember(OfmId);

            if (OfMeManager.Count == 1)
            {
                OfMeManager[0].BeginEdit();
                if (Session["MemberEmza2"] != null)
                {
                    pathAx = Server.MapPath("~/image/Temp/");
                    OfMeManager[0]["SignUrl"] = "~/Image/EngOffice/Members/" + Session["MemberEmza2"].ToString();
                    chSignImg = true;
                }
                if (Session["Selfreported"] != null)
                {
                    OfMeManager[0]["SelfreportedImageURL"] = Session["Selfreported"].ToString();
                    ImageSelfreported.NavigateUrl = Session["Selfreported"].ToString();
                }

                if (drdPosition.Value != null)
                {
                    OfMeManager[0]["OfpId"] = int.Parse(drdPosition.Value.ToString());
                }
                OfMeManager[0]["StartDate"] = txtStartDate.Text; ;
                OfMeManager[0]["HasSignRight"] = Convert.ToByte(chbHaghEmza.Checked);
                //******************
                //Plz check Validation for membership in other Gas Com
                //******************
                OfMeManager[0]["HasGasCert"] = Convert.ToByte(chbHasGasCert.Checked);
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

                OfMeManager[0]["Description"] = txtDesc.Text;
                OfMeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OfMeManager[0]["ModifiedDate"] = DateTime.Now;
                OfMeManager[0].EndEdit();
                trans.BeginSave();
                if (OfMeManager.Save() > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_OffAaza"].ToString())))
                    {
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);// (int)TSP.DataManager.TableCodes.EngOffFile;
                        int UpdateTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);// (int)TSP.DataManager.TableCodes.OfficeMember;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, _EngFileId, UpdateTableType, "ویرایش اعضای دفتر", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    }
                    else
                    {
                        trans.EndSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                        Session["IsEdited_OffAaza"] = true;
                    }
                }
                else
                {
                    trans.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
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
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        if (chSignImg == true)
        {
            try
            {
                string EmzaSoource = Server.MapPath("~/image/Temp/") + Session["MemberEmza2"].ToString();
                string EmzaTarget = Server.MapPath("~/image/EngOffice/Members/") + Session["MemberEmza2"].ToString();
                System.IO.File.Move(EmzaSoource, EmzaTarget);
            }
            catch (Exception)
            {
            }
        }
        Session["Selfreported"] = null;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    protected void FillMember(int OfmId, int PersonId)
    {

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        //TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
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
                txtFileNoDate.Text = MeManager[0]["FileDate"].ToString();
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
                OfMeManager.selectEngOfficeMember(OfmId);
                if (OfMeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["SelfreportedImageURL"]))
                        ImageSelfreported.NavigateUrl = OfMeManager[0]["SelfreportedImageURL"].ToString();
                    EngFileIdOrigin.Value = Utility.EncryptQS(OfMeManager[0]["OfReId"].ToString());
                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["OfpId"]))
                    {
                        drdPosition.DataBind();
                        drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
                    }

                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["IsFullTime"]))
                    {
                        ComboTime.DataBind();
                        if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                            ComboTime.SelectedIndex = 1;
                        else
                            ComboTime.SelectedIndex = 0;
                    }

                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    //txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();

                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["HasGasCert"]))
                        chbHasGasCert.Checked = (bool)OfMeManager[0]["HasGasCert"];

                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    if (chbHaghEmza.Checked)
                    {
                        imgEmza.ClientVisible = true;
                        imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();
                        HD_Emza["name"] = 1;
                        HD_Emza["Url"] = OfMeManager[0]["SignUrl"].ToString();
                    }
                    else
                    {
                        imgEmza.ClientVisible = false;

                    }

                    txtDesc.Text = OfMeManager[0]["Description"].ToString();

                }
                //MeFileManager.SelectLastVersion(PersonId, 0);
                //if (MeFileManager.Count > 0)
                if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["MfId"]))
                {
                    int MfId = int.Parse(OfMeManager[0]["MfId"].ToString());
                    GridViewMemberDocRes.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                    //GridViewMemberDocRes.KeyFieldName = "MfdId";
                    GridViewMemberDocRes.DataBind();
                }


            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                ShowMessage("امکان مشاهده اطلاعات وجود ندارد");

            }
        }
        else
        {
            ShowMessage("اطلاعات توسط کاربر دیگری تغییر کرده است");
        }
    }

    private Boolean CheckIfCanEditOldInformations(int EOfId)
    {
        TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
        EngOffFileManager.FindByCode(EOfId);
        if (EngOffFileManager.Count != 1)
            return false;
        if (Convert.ToInt32(EngOffFileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.OldDocument)
        {
            int EngOfId = Convert.ToInt32(EngOffFileManager[0]["EngOfId"]);
            EngOffFileManager.FindByEngOffCode(EngOfId, 1, -1);
            if (EngOffFileManager.Count > 1)
                return false;
            else
                return true;

        }
        return false;
    }
    #endregion
}
