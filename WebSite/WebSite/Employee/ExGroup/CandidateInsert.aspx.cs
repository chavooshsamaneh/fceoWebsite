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

public partial class Employee_ExGroup_CandidateInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Properties
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldModeID["PgMode"].ToString());
        }
        set
        {
            HiddenFieldModeID["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int ID
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldModeID["Id"].ToString()));
        }
        set
        {
            HiddenFieldModeID["Id"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int ExGroupPeriodId
    {
        get
        {
            try
            {
                return int.Parse(Utility.DecryptQS(HiddenFieldModeID["ExGroupPeriodId"].ToString()));
            }
            catch
            {
                return int.Parse(Utility.DecryptQS(Request.QueryString["ExGroupPeriodId"].ToString()));
            }
        }
        set
        {
            HiddenFieldModeID["ExGroupPeriodId"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int ExGroupCode
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldModeID["ExGroupCode"].ToString()));
        }
        set
        {
            HiddenFieldModeID["ExGroupCode"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (!Page.IsPostBack)
        {
            Session["AssociationAttach"] = null;
            SetKeys();
            SetPermission();
            SetMode();
            SelectModePage(); //----for entezami mode-----
        }

        KeepPageState();
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btnsave"] != null)
            this.btnsave.Enabled = this.btnsave2.Enabled = (bool)this.ViewState["btnsave"];
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.Permission per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            PageMode = "new";
            SetMode();
        }

    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (PageMode != "view")
        {
            ShowMessage("کاندیدی انتخاب نشده است");
            return;
        }
        if (Utility.IsDBNullOrNullValue(ID))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        CandidateManager.FindByCode(ID);
        if (CandidateManager.Count == 1)
        {
            if (Convert.ToBoolean(CandidateManager[0]["InActive"]))
            {
                ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                return;
            }
        }
        TSP.DataManager.Permission per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
        {
            PageMode = "edit";
            SetMode();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (PageMode == "new")
        {
            if (ExGroupCode == (int)TSP.DataManager.ExGroupType.Candid)
            {
                if (CheckRepeatCandidate())
                {
                    ShowMessage("این عضو قبلا به عنوان کاندید این دوره ثبت شده است");
                    return;
                }
            }
            Insert();
        }
        else if (PageMode == "edit")//-------------------------------------------
        {
            if (Utility.IsDBNullOrNullValue(ID))
            {
                Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            Edit(ID);
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(ExGroupPeriodId))
            Response.Redirect("Candidate.aspx?ExGroupPeriodId=" + Utility.EncryptQS(ExGroupPeriodId.ToString()));
        else Response.Redirect("ExGroupPeriod.aspx");
    }

    protected void CallbackPanelCandidate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] Parameters = e.Parameter.Split(';');
        switch (Parameters[1])
        {
            case "FindMe":
                ResetForm();
                txtMeId.Text = Parameters[0].ToString();
                if (!FindMember(Convert.ToInt32(Parameters[0])))
                    ShowCallBackMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
                lblMajor2.ClientVisible = false;
                cmbMajor2.ClientVisible = false;
                cmbStatus.SelectedIndex = 0;
                break;
            //case "FindLicence":
            //    FillMeLicenceInfo();
            //    if (!FindMember(Convert.ToInt32(txtMeId.Text)))
            //        ShowCallBackMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
            //    break;
            case "FindParentMajor":
                FillMeLicenceInfo();
                if (!Utility.IsDBNullOrNullValue(txtMeId.Text))
                {
                    if (!FindMember(Convert.ToInt32(txtMeId.Text)))
                        ShowCallBackMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
                    //   ObjdsMemberLicence2.SelectParameters["MemberId"].DefaultValue = txtMeId.Text.Trim();
                    //   cmbMajor2.DataBind();
                }
                //lblMajor2.ClientVisible = true;
                //cmbMajor2.ClientVisible = true;
                break;
        }
    }

    protected void flpcAttachment_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveAttachment(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMeLicenceInfo();
        if (!FindMember(Convert.ToInt32(txtMeId.Text)))
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
    }

    #endregion

    #region FormMethods
    protected string SaveAttachment(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Association/Candidate/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AssociationAttach"] = tempFileName;
        }
        return ret;
    }

    void SetKeys()
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                cmbIsManager.DataBind();
                cmbIsManager.Items.Insert(0, new DevExpress.Web.ListEditItem("------------------------", -1));
                cmbIsManager.SelectedIndex = 0;
                ID = int.Parse(Utility.DecryptQS(Request.QueryString["id"].ToString()));
                ExGroupPeriodId = int.Parse(Utility.DecryptQS(Request.QueryString["ExGroupPeriodId"].ToString()));
                PageMode = Utility.DecryptQS(Request.QueryString["mode"].ToString());
                ExGroupCode = int.Parse(Utility.DecryptQS(Request.QueryString["ExGroupCode"].ToString()));
                LoadExGroupPeriod();
            }
            else
            {
                Response.Redirect("ExGroupPeriod.aspx");
            }
        }
        catch
        {
            Response.Redirect("ExGroupPeriod.aspx");
        }
    }

    void SetMode()
    {
        switch (PageMode)
        {
            case "view":
                DisableControls();
                FillControls(ID);
                PageMode = "view";
                RoundPanelMain.HeaderText = "مشاهده";
                SetButtoms(true, true, false);
                break;
            case "edit":
                PageMode = "edit";
                EnableControls();
                FillControls(ID);
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                break;
            case "new":
                PageMode = "new";
                EnableControls();
                ResetForm();
                cmbStatus.SelectedIndex = 0;
                lblMajor2.ClientVisible = false;
                cmbMajor2.ClientVisible = false;
                txtMeId.Focus();
                RoundPanelMain.HeaderText = "جدید";
                SetButtoms(true, false, true);
                break;
        }
        this.ViewState["btnnew"] = btnnew.Enabled;
        this.ViewState["btnedit"] = btnedit.Enabled;
        this.ViewState["btnsave"] = btnsave.Enabled;
    }

    public void SelectModePage()
    {
        switch (ExGroupCode)
        {
            case (int)TSP.DataManager.ExGroupType.Entezami:
                labelMeId.Visible = false;
                txtMeId.Visible = false;

                txtFirstName.ReadOnly = false;
                txtLastName.ReadOnly = false;
                break;
            case (int)TSP.DataManager.ExGroupType.Candid:
                labelMeId.Visible = true;
                txtMeId.Visible = true;

                txtFirstName.ReadOnly = true;
                txtLastName.ReadOnly = true;
                break;
        }
    }

    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
            (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnsave.Enabled = per.CanEdit || per.CanNew;
        btnsave2.Enabled = per.CanEdit || per.CanNew;
        btnnew.Enabled = per.CanNew;
        btnedit.Enabled = per.CanEdit;
        btnnew2.Enabled = per.CanNew;
        btnedit2.Enabled = per.CanEdit;
    }

    void ResetForm()
    {
        txtMeId.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtPosition.Text = string.Empty;
        txtVoteCount.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtElectionCode.Text = string.Empty;
        txtFileno.Text = string.Empty;
        txtImpName.Text = string.Empty;
        txtObsName.Text = string.Empty;
        txtDesName.Text = string.Empty;
        txtUrbonism.Text = string.Empty;
        txtMapping.Text = string.Empty;
        txtTraffic.Text = string.Empty;
        cmbIsManager.SelectedIndex = 0;
        cmbMajor.SelectedIndex = -1;
        SetMeLicenceObjectDataSource(-2);
        ID = -1;
        RoundPanelMain.HeaderText = "";
        txtResume.Html = "";
        PageMode = "new";
        Session["AssociationAttach"] = null;
    }

    void DisableControls()
    {
        //RoundPanelMain.Enabled = false;
        SetControlEnables(false);
    }

    void EnableControls()
    {
        //RoundPanelMain.Enabled = true;
        SetControlEnables(true);
    }

    void SetControlEnables(Boolean Enable)
    {
        txtDescription.Enabled = Enable;
        txtMeId.Enabled = Enable;
        txtResume.Enabled = Enable;
        txtVoteCount.Enabled = Enable;
        cmbIsManager.Enabled = Enable;
        cmbStatus.Enabled = Enable;
        flpcAttachment.Enabled = Enable;
        txtElectionCode.Enabled = Enable;
        cmbMajor.Enabled = Enable;
        cmbMajor2.Enabled = Enable;
    }

    void FillControls(int id)
    {
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();

        //---------------fill main data
        CandidateManager.FindByCode(id);
        if (CandidateManager.Count == 1)
        {
            txtMeId.Text = CandidateManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["CandidateStatus"]))
                cmbStatus.SelectedIndex = Convert.ToInt32(CandidateManager[0]["CandidateStatus"]);
            if (Convert.ToInt32(CandidateManager[0]["CandidateStatus"]) == 2)
            {
                if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["SubstituteMajorId"]))
                {
                    cmbMajor2.DataBind();
                    cmbMajor2.SelectedIndex = cmbMajor2.Items.FindByValue(CandidateManager[0]["SubstituteMajorId"].ToString()).Index;
                }
                //   Convert.ToInt32(CandidateManager[0]["SubstituteMajorId"]);
                lblMajor2.ClientVisible = true;
                cmbMajor2.ClientVisible = true;
            }
            else
            {
                lblMajor2.ClientVisible = false;
                cmbMajor2.ClientVisible = false;
            }

            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["IsManager"]))
            {
                cmbIsManager.SelectedIndex = cmbIsManager.Items.FindByValue(CandidateManager[0]["IsManager"]).Index;
                if (Convert.ToInt32(CandidateManager[0]["IsManager"]) == 2)
                {
                    txtPosition.ClientVisible = true;
                    labelPosition.ClientVisible = true;
                    txtPosition.Text = CandidateManager[0]["Position"].ToString();
                }
                else
                {
                    txtPosition.ClientVisible = false;
                    labelPosition.ClientVisible = false;
                }
            }
            txtVoteCount.Text = CandidateManager[0]["VoteCount"].ToString();
            txtDescription.Text = CandidateManager[0]["Description"].ToString();
            //   txtMeId.Text = CandidateManager[0]["MeId"].ToString();
            txtResume.Html = CandidateManager[0]["Resume"].ToString();
            txtElectionCode.Text = CandidateManager[0]["CandidateCode"].ToString();
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["Attachment"]))
                HyperLinkAttachment.NavigateUrl = CandidateManager[0]["Attachment"].ToString();
            SetMeLicenceObjectDataSource(Convert.ToInt32(CandidateManager[0]["MeId"]));
            try
            {
                if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["MlId"]))
                {
                    cmbMajor.DataBind();
                    cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(CandidateManager[0]["MlId"].ToString()).Index;
                    FillMeLicenceInfo(Convert.ToInt32(CandidateManager[0]["MlId"]));
                }
            }
            catch (Exception)
            {

            }
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["MlId"]))
            {
                FillMeLicenceInfo(Convert.ToInt32(CandidateManager[0]["MlId"]));
            }
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            MemberManager.FindByCode(Convert.ToInt32(CandidateManager[0]["MeId"]));
            if (MemberManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            txtFirstName.Text = MemberManager[0]["FirstName"].ToString();
            txtLastName.Text = MemberManager[0]["LastName"].ToString();
            txtFileno.Text = MemberManager[0]["FileNo"].ToString();
            txtImpName.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtDesName.Text = MemberManager[0]["DesGrdName"].ToString();
            txtObsName.Text = MemberManager[0]["ObsGrdName"].ToString();
            txtUrbonism.Text = MemberManager[0]["UrbanismGrdName"].ToString();
            txtTraffic.Text = MemberManager[0]["TrafficGrdName"].ToString();
            txtMapping.Text = MemberManager[0]["MappingGrdName"].ToString();
            MemberRequestManager.FindByMemberId(Convert.ToInt32(CandidateManager[0]["MeId"]), -1, -1);
            if (MemberRequestManager.Count > 0)
                ImgMember.ImageUrl = MemberRequestManager[0]["ImageUrl"].ToString();
            //TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            //DocMemberFileMajorManager.SelectMemberMasterMajor(Convert.ToInt32(CandidateManager[0]["MeId"]));
            //if (DocMemberFileMajorManager.Count > 0)
            //{

            //    if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["FMjFullName"]))
            //        txtMajor.Text = DocMemberFileMajorManager[0]["FMjFullName"].ToString();
            //    if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["UnName"]))
            //        txtUniversity.Text = DocMemberFileMajorManager[0]["UnName"].ToString();
            //}
        }
    }

    void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
          (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if ((!per.CanNew) && (newb))
            newb = false;
        if ((!per.CanEdit) && (editb))
            editb = false;

        btnnew.Enabled = newb;
        btnnew2.Enabled = newb;
        btnedit.Enabled = editb;
        btnedit2.Enabled = editb;
        btnsave.Enabled = saveb;
        btnsave2.Enabled = saveb;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelCandidate.JSProperties["cpMsg"] = Msg;
        CallbackPanelCandidate.JSProperties["cpError"] = 1;
    }

    void KeepPageState()
    {
        if (Convert.ToInt32(cmbIsManager.Value) == 2)
        {
            txtPosition.ClientVisible = true;
            labelPosition.ClientVisible = true;
        }
        else
        {
            txtPosition.ClientVisible = false;
            labelPosition.ClientVisible = false;
        }

        if (IsCallback)
        {
            if (Convert.ToInt32(cmbStatus.Value) == 2)
            {
                lblMajor2.ClientVisible = true;
                cmbMajor2.ClientVisible = true;
                cmbStatus.SelectedIndex = 2;
            }
        }
    }

    #endregion

    #region DataMethods
    public void Insert()
    {
        TSP.DataManager.TransactionManager TransactManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TransactManager.Add(CandidateManager);
        TransactManager.Add(OtherPersonManager);
        try
        {
            TransactManager.BeginSave();
            DataRow row = CandidateManager.NewRow();
            row["ExGroupPeriodId"] = ExGroupPeriodId;
            if (txtMeId.Text != string.Empty)
                row["MeId"] = txtMeId.Text.Trim();
            if (txtVoteCount.Text != string.Empty)
                row["VoteCount"] = txtVoteCount.Text.Trim();
            row["Position"] = txtPosition.Text != string.Empty ? txtPosition.Text.Trim() : null;
            row["Description"] = txtDescription.Text != string.Empty ? txtDescription.Text.Trim() : null;
            if (cmbIsManager.SelectedItem != null
                && Convert.ToInt32(cmbIsManager.SelectedItem.Value) > -1)
                row["IsManager"] = cmbIsManager.SelectedItem.Value;
            else
                row["IsManager"] = DBNull.Value;
            row["CandidateStatus"] = cmbStatus.Value != null ? cmbStatus.SelectedItem.Value : DBNull.Value;
            row["SubstituteMajorId"] = cmbMajor2.Value != null ? cmbMajor2.SelectedItem.Value : DBNull.Value;
            row["Resume"] = txtResume.Html;
            row["InActive"] = 0;
            row["CandidateCode"] = txtElectionCode.Text.Trim();
            if (cmbMajor.SelectedItem != null)
            {
                row["MlId"] = cmbMajor.SelectedItem.Value;
            }
            if (Session["AssociationAttach"] != null)
            {
                try
                {
                    string FileName = System.IO.Path.GetFileName(Session["AssociationAttach"].ToString());
                    row["Attachment"] = "~/Image/Association/Candidate/" + FileName;
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Association/Candidate/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    HyperLinkAttachment.NavigateUrl = ImgTarget;
                    Session["AssociationAttach"] = null;
                }
                catch (Exception ex)
                {
                    TransactManager.CancelSave();
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره فایل انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    ShowMessage(Message);
                    return;
                }
            }

            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            CandidateManager.AddRow(row);
            if (CandidateManager.Save() > 0)
            {
                ID = Convert.ToInt32(CandidateManager[0]["CandidateId"].ToString());

                if (ExGroupCode == (int)TSP.DataManager.ExGroupType.Entezami) //-----insert otherperson--
                {
                    DataRow opmrow = OtherPersonManager.NewRow();
                    opmrow["FirstName"] = txtFirstName.Text.Trim();
                    opmrow["LastName"] = txtLastName.Text.Trim();
                    opmrow["OtpType"] = 6; //---ghazi---
                    opmrow["OtpCode"] = ID;
                    opmrow["InActive"] = 0;
                    opmrow["IsLock"] = 0;
                    opmrow["UserId"] = Utility.GetCurrentUser_UserId();
                    opmrow["ModifiedDate"] = DateTime.Now;
                    OtherPersonManager.AddRow(opmrow);
                    if (OtherPersonManager.Save() > 0)
                    {
                        OtherPersonManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                        TransactManager.CancelSave();
                        return;
                    }
                }

                TransactManager.EndSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                PageMode = "edit";
                RoundPanelMain.HeaderText = "ویرایش";
                FillControls(ID);
                SetButtoms(true, false, true);
            }
            else
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
        catch (Exception err)
        {
            TransactManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    public void Edit(int id)
    {
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        try
        {
            string PreAttachment = "";
            CandidateManager.FindByCode(id);
            if (CandidateManager.Count == 1)
            {
                if (Convert.ToBoolean(CandidateManager[0]["InActive"]))
                {
                    ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                    return;
                }
                CandidateManager[0].BeginEdit();
                if (txtMeId.Text != string.Empty)
                    CandidateManager[0]["MeId"] = txtMeId.Text.Trim();
                if (txtVoteCount.Text != string.Empty)
                    CandidateManager[0]["VoteCount"] = txtVoteCount.Text.Trim();
                CandidateManager[0]["Position"] = txtPosition.Text != string.Empty ? txtPosition.Text.Trim() : null;
                CandidateManager[0]["Description"] = txtDescription.Text != string.Empty ? txtDescription.Text.Trim() : null;
                //CandidateManager[0]["IsManager"] = cmbIsManager.Value != null ? cmbIsManager.SelectedItem.Value : DBNull.Value;
                if (cmbIsManager.SelectedItem != null
                    && Convert.ToInt32(cmbIsManager.SelectedItem.Value) > -1)
                    CandidateManager[0]["IsManager"] = cmbIsManager.SelectedItem.Value;
                else
                    CandidateManager[0]["IsManager"] = DBNull.Value;
                CandidateManager[0]["CandidateStatus"] = cmbStatus.Value != null ? cmbStatus.SelectedItem.Value : DBNull.Value;
                CandidateManager[0]["SubstituteMajorId"] = cmbMajor2.Value != null ? cmbMajor2.SelectedItem.Value : DBNull.Value;
                CandidateManager[0]["Resume"] = txtResume.Html;
                CandidateManager[0]["CandidateCode"] = txtElectionCode.Text.Trim();
                if (cmbMajor.SelectedItem != null)
                {
                    CandidateManager[0]["MlId"] = cmbMajor.SelectedItem.Value;
                }
                if (Session["AssociationAttach"] != null)
                {
                    try
                    {
                        if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["Attachment"]))
                        {
                            PreAttachment = CandidateManager[0]["Attachment"].ToString();
                        }
                        string FileName = System.IO.Path.GetFileName(Session["AssociationAttach"].ToString());
                        CandidateManager[0]["Attachment"] = "~/Image/Association/Candidate/" + FileName;
                        string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                        string ImgTarget = Server.MapPath("~/Image/Association/Candidate/") + FileName;
                        File.Move(ImgSoource, ImgTarget);
                        HyperLinkAttachment.NavigateUrl = ImgTarget;
                        Session["AssociationAttach"] = null;
                    }
                    catch (Exception ex)
                    {
                        Utility.SaveWebsiteError(ex);
                        string Message = "خطایی در ذخیره فایل انجام گرفته است";
                        if (Utility.ShowExceptionError())
                        {
                            Message = Message + ex.Message;
                        }
                        ShowMessage(Message);
                        return;
                    }
                }
                CandidateManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                CandidateManager[0]["ModifiedDate"] = DateTime.Now;
                CandidateManager[0].EndEdit();
                if (CandidateManager.Save() > 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(PreAttachment))
                        {
                            File.Delete(MapPath(PreAttachment));
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("خطایی در حذف فایل قبلی ایجاد شده است");
                        Utility.SaveWebsiteError(ex);
                    }
                }
                else
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    public bool FindMember(int MeId)
    {
        TSP.DataManager.MemberManager member = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        member.FindByCode(MeId);
        if (member.Count != 0)
        {
            txtFirstName.Text = member[0]["FirstName"].ToString();
            txtLastName.Text = member[0]["LastName"].ToString();
            txtFileno.Text = member[0]["FileNo"].ToString();
            txtImpName.Text = member[0]["ImpGrdName"].ToString();
            txtDesName.Text = member[0]["DesGrdName"].ToString();
            txtObsName.Text = member[0]["ObsGrdName"].ToString();
            txtUrbonism.Text = member[0]["UrbanismGrdName"].ToString();
            txtTraffic.Text = member[0]["TrafficGrdName"].ToString();
            txtMapping.Text = member[0]["MappingGrdName"].ToString();
            MemberRequestManager.FindByMemberId(MeId, -1, -1);
            if (MemberRequestManager.Count > 0)
                ImgMember.ImageUrl = MemberRequestManager[0]["ImageUrl"].ToString();
            SetMeLicenceObjectDataSource(MeId);
            return true;
        }
        else return false;
    }

    void LoadExGroupPeriod()
    {
        TSP.DataManager.ExGroupPeriodManager epgManager = new TSP.DataManager.ExGroupPeriodManager();
        epgManager.FindByCode(ExGroupPeriodId);
        if (epgManager.Count == 1)
        {
            txtExGroupName.Text = epgManager[0]["ExGroupName"].ToString();
            txtStartDate.Text = epgManager[0]["StartDate"].ToString();
            txtEndDate.Text = epgManager[0]["EndDate"].ToString();
        }
    }

    private void FillMeLicenceInfo()
    {
        int MlId = -1;
        if (cmbMajor.Value != null)
            MlId = Convert.ToInt32(cmbMajor.Value);
        else
            return;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.FindByCode(MlId);
        if (MemberLicenceManager.Count != 1)
            return;
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
            txtUniversity.Text = MemberLicenceManager[0]["UnName"].ToString();
    }

    private void FillMeLicenceInfo(int MlId)
    {
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.FindByCode(MlId);
        if (MemberLicenceManager.Count != 1)
            return;
        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
            txtUniversity.Text = MemberLicenceManager[0]["UnName"].ToString();
    }

    private void SetMeLicenceObjectDataSource(int MeId)
    {
        ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = MeId.ToString();
        cmbMajor.DataBind();
    }

    private void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowMessage("شماره تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    bool CheckRepeatCandidate()
    {
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        CandidateManager.FindByMeId(ExGroupPeriodId, Convert.ToInt32(txtMeId.Text.Trim()), 0);
        if (CandidateManager.Count != 0)
            return true;
        else return false;
    }
    #endregion
}
