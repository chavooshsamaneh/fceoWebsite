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

public partial class Employee_Amoozesh_AddTeacherLicence : System.Web.UI.Page
{
    #region Private Members
    string TeLiId;
    string TeacherId;
    string PageMode;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        MenuTeacherInfo.Enabled = true;
        if (!IsPostBack)
        {
            Session["IsEdited_TeLicence"] = false;
            HiddenFieldTecherLicence["PageMode"] = "";
            HiddenFieldTecherLicence["PrePageMode"] = "";
            HiddenFieldTecherLicence["TeLiId"] = "";
            HiddenFieldTecherLicence["NewMode"] = Utility.EncryptQS("New");
            HiddenFieldTecherLicence["TeacherId"] = "";
            Session["AttachmentFile"] = "";
            Session["TeacherLiAttachFileAddress"] = "";
            //Check UserPermission
            TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            btnDeleteAttachment.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            // btnSaveJudgment.Enabled = per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["TeLiId"])) || (string.IsNullOrEmpty(Request.QueryString["PrePageMode"])))
            {
                Response.Redirect("TeachersLicence.aspx");
                return;
            }
            SetKeys();

            CheckWorkFlowPermission();
            TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
            CheckCertificatePermission(int.Parse(TeacherId));

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void MenuTeacherInfo_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTecherLicence["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTecherLicence["PrePageMode"]);
                break;


            case "Madrak":
                // string TeId = Utility.DecryptQS();
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTecherLicence["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTecherLicence["PrePageMode"]);
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTecherLicence["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTecherLicence["PrePageMode"]);
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTecherLicence["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTecherLicence["PrePageMode"]);
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldTecherLicence["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                EnableControls();

                TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                InsertTeacherLicence();

                break;
            case "Edit":
                if (string.IsNullOrEmpty(HiddenFieldTecherLicence["TeLiId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
                }
                EditTeacherLicence(int.Parse(TeLiId));
                break;
            case "Judge":
                if (string.IsNullOrEmpty(HiddenFieldTecherLicence["TeLiId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    int TableType = -1;
                    if (IsTeacherMember())
                        TableType = (int)(TSP.DataManager.TableCodes.MemberLicence);
                    else
                        TableType = (int)(TSP.DataManager.TableCodes.TeacherLicence);
                    string TeId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
                    TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
                    TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
                    DataTable dtTeacherJudgment = TeacherJudgmentManager.FindByResearchActivity(int.Parse(TeId), int.Parse(TeLiId), TableType);
                    if (dtTeacherJudgment.Rows.Count == 1)
                    {
                        int JudgeId = int.Parse(dtTeacherJudgment.Rows[0]["JudgeId"].ToString());
                        EditJudgment(JudgeId);
                    }
                    else
                        InsertJudgment();
                }
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
        DeleteTeacherLicence(int.Parse(TeLiId));

        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit || per.CanNew;
        btnSave2.Enabled = per.CanNew || per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        ClearForm();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTecherLicence["TeacherId"] + "&PageMode=" + HiddenFieldTecherLicence["PrePageMode"]);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        btnDeleteAttachment.Enabled = per.CanDelete;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
        DataTable dtAttach = AttachmentsManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherLicence), int.Parse(TeLiId));
        if (dtAttach.Rows.Count == 0)
        {
            btnAttachTechearLicenceFile.Visible = true;
            btnDeleteAttachment.Visible = false;
        }


        HiddenFieldTecherLicence["PageMode"] = Utility.EncryptQS("Edit");
        EnableControls();
        RoundPanelTeacherLicence.HeaderText = "ویرایش";

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnableControls();
        HiddenFieldTecherLicence["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldTecherLicence["TeLiId"] = Utility.EncryptQS("");
        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        linkAttachment.Visible = false;
        btnDeleteAttachment.Visible = false;
        btnAttachTechearLicenceFile.Visible = true;
        btnAttachTechearLicenceFile.Enabled = true;

        RoundPanelTeacherLicence.HeaderText = "جدید";
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    protected void btnDeleteAttachment_Click(object sender, EventArgs e)
    {
        TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
        DeleteTeacherAttachment(int.Parse(TeLiId));
        linkAttachment.Visible = false;
        btnDeleteAttachment.ClientVisible = false;
        // flp.Visible = true;
        btnAttachTechearLicenceFile.Visible = true;
    }

    protected void btnSaveAttach_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveJudgment_Click(object sender, EventArgs e)
    {
        //TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        //TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        //string TLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());

        //TeacherManager.FindByCode(int.Parse(TeacherId));
        //if (string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
        //{
        //    SaveTeacherLicenceJudge(int.Parse(TeLiId));
        //}
        //else
        //{
        //    SaveMemberLicenceJudge(int.Parse(TeLiId));
        //}
    }

    protected void CmbUniversity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbUniversity.SelectedIndex > -1)
        {
            CmbUniversity.DataBind();
            if (CmbUniversity.SelectedItem.Value != null)
            {
                int UnId = int.Parse(CmbUniversity.SelectedItem.Value.ToString());
                TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();
                UniversityManager.FindByCode(UnId);
                if (UniversityManager.Count == 1)
                {
                    string CounId = UniversityManager[0]["CounId"].ToString();
                    ObjdsCity.SelectParameters[0].DefaultValue = CounId;
                    cmbCity.DataBind();
                    cmbCity.SelectedIndex = 0;
                }
            }
        }
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldTecherLicence["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
        HiddenFieldTecherLicence["PrePageMode"] = Server.HtmlDecode(Request.QueryString["PrePageMode"].ToString());
        HiddenFieldTecherLicence["TeacherId"] = Server.HtmlDecode(Request.QueryString["TeacherId"]).ToString();
        HiddenFieldTecherLicence["TeLiId"] = Server.HtmlDecode(Request.QueryString["TeLiId"]).ToString();
        PageMode = Utility.DecryptQS(HiddenFieldTecherLicence["PageMode"].ToString());
        TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
        TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        linkAttachment.Visible = false;
        btnDeleteAttachment.ClientVisible = false;
        btnAttachTechearLicenceFile.Visible = false;
        SetMode();
    }

    private void SetMode()
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "Judge":
                SetJudgeModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        btnDeleteAttachment.Enabled = false;
        btnAttachTechearLicenceFile.Enabled = false;
        CmbLicence.Enabled = false;
        CmbMajor.Enabled = false;
        CmbUniversity.Enabled = false;
        txtAvg.Enabled = false;
        cmbCity.Enabled = false;
        txtNumUnit.Enabled = false;
        txtEndDate.Enabled = false;
        txtStartDate.Enabled = false;

        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeacherId));
        if (string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
        {
            FillFormTeacherLicence(int.Parse(TeLiId));
        }
        else
        {
            FillFormMemberLicence(int.Parse(TeLiId));
            BtnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }

        RoundPanelTeacherLicence.HeaderText = "مشاهده";

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;


        btnDeleteAttachment.Enabled = false;

        ClearForm();

        //  flp.Visible = true;
        linkAttachment.Visible = false;
        btnDeleteAttachment.Enabled = false;
        btnAttachTechearLicenceFile.Visible = true;
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeacherId));
        if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
        {

            BtnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        RoundPanelTeacherLicence.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        if (string.IsNullOrEmpty(TeLiId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        btnDeleteAttachment.Enabled = true;
        EnableControls();

        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeacherId));
        if (string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
        {
            FillFormTeacherLicence(int.Parse(TeLiId));
        }
        else
        {
            FillFormMemberLicence(int.Parse(TeLiId));

            BtnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;

        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        RoundPanelTeacherLicence.HeaderText = "ویرایش";
    }

    private void SetJudgeModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        BtnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnDeleteAttachment.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        CmbLicence.Enabled = false;
        CmbLicence.Enabled = false;
        CmbMajor.Enabled = false;
        CmbMajor.Enabled = false;
        CmbUniversity.Enabled = false;
        CmbUniversity.Enabled = false;
        txtAvg.Enabled = false;
        txtAvg.Enabled = false;
        cmbCity.Enabled = false;
        cmbCity.Enabled = false;
        txtDescription.Enabled = false;
        txtDescription.Enabled = false;
        txtStartDate.Enabled = false;
        txtEndDate.Enabled = false;
        txtNumUnit.Enabled = false;
        txtNumUnit.Enabled = false;

        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeacherId));
        if (string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
        {
            FillFormTeacherLicence(int.Parse(TeLiId));
        }
        else
        {
            FillFormMemberLicence(int.Parse(TeLiId));
        }

        RoundPanelTeacherLicence.HeaderText = "مشاهده";
    }

    private void EnableControls()
    {
        CmbLicence.Enabled = true;
        CmbMajor.Enabled = true;
        CmbUniversity.Enabled = true;
        txtAvg.Enabled = true;
        cmbCity.Enabled = true;
        txtDescription.Enabled = true;
        txtEndDate.Enabled = true;
        txtNumUnit.Enabled = true;
        // flp.Enabled = true;
        btnDeleteAttachment.Enabled = true;
        txtStartDate.Enabled = true;
    }

    private void FillFormTeacherLicence(int TeacherLicenceId)
    {
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
        TeacherLicenceManger.FindByCode(TeacherLicenceId);

        if (TeacherLicenceManger.Count > 0)
        {
            cmbCity.DataBind();
            CmbLicence.DataBind();
            CmbMajor.DataBind();
            CmbUniversity.DataBind();

            CmbLicence.SelectedIndex = CmbLicence.Items.IndexOfValue(TeacherLicenceManger[0]["LiId"].ToString());
            CmbMajor.SelectedIndex = CmbMajor.Items.IndexOfValue(TeacherLicenceManger[0]["MjId"].ToString());
            CmbUniversity.SelectedIndex = CmbUniversity.Items.IndexOfValue(TeacherLicenceManger[0]["UnId"].ToString());
            cmbCity.SelectedIndex = cmbCity.Items.IndexOfValue(TeacherLicenceManger[0]["CitId"].ToString());
            txtAvg.Text = Convert.ToDecimal(TeacherLicenceManger[0]["Avg"]).ToString("##.00"); //TeacherLicenceManger[0]["Avg"].ToString();            
            txtDescription.Text = TeacherLicenceManger[0]["Description"].ToString();
            txtEndDate.Text = TeacherLicenceManger[0]["EndDate"].ToString();
            txtNumUnit.Text = TeacherLicenceManger[0]["NumUnit"].ToString();
            txtStartDate.Text = TeacherLicenceManger[0]["StartDate"].ToString();

            TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
            LicenceManager.FindByCode(int.Parse(CmbLicence.SelectedItem.Value.ToString()));
            float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

            int TableType = -1;
            if (IsTeacherMember())
                TableType = (int)(TSP.DataManager.TableCodes.MemberLicence);
            else
                TableType = (int)(TSP.DataManager.TableCodes.TeacherLicence);
            int TableId = TeacherLicenceId;
            //TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
            //DataTable dtJudgment = TeacherJudgmentManager.FindByTableType(int.Parse(TeacherId), TableId, TableType);
            //if (dtJudgment.Rows.Count > 0)
            //{
            //    txtJudgeView.Text = dtJudgment.Rows[0]["JudgeViewPoint"].ToString();
            //    if (LicenceManager.Count > 0)
            //    {
            //        SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());
            //    }
            //    int JudgeGrade = int.Parse(dtJudgment.Rows[0]["JudgeGrade"].ToString());
            //    if (SystemGrade == JudgeGrade)
            //        rbtnGrade.SelectedIndex = 0;
            //    else
            //        rbtnGrade.SelectedIndex = 1;
            //}

            TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
            DataTable dtAttach = AttachmentsManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherLicence), TeacherLicenceId);
            if (dtAttach.Rows.Count > 0)
            {
                btnDeleteAttachment.ClientVisible = true;
                string filePath = dtAttach.Rows[0]["FilePath"].ToString();
                linkAttachment.Visible = true;
                linkAttachment.Text = Path.GetFileName(filePath);
                linkAttachment.NavigateUrl = filePath;
            }
            else
            {
                btnAttachTechearLicenceFile.Visible = true;
            }
        }
    }

    private void FillFormMemberLicence(int MeLiId)
    {
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.FindByCode(MeLiId);
        if (MemberLicenceManager.Count > 0)
        {
            cmbCity.DataBind();
            CmbLicence.DataBind();
            CmbMajor.DataBind();
            CmbUniversity.DataBind();

            CmbLicence.SelectedIndex = CmbLicence.Items.IndexOfValue(MemberLicenceManager[0]["LiId"].ToString());
            CmbMajor.SelectedIndex = CmbMajor.Items.IndexOfValue(MemberLicenceManager[0]["MjId"].ToString());
            CmbUniversity.SelectedIndex = CmbUniversity.Items.IndexOfValue(MemberLicenceManager[0]["UnId"].ToString());
            cmbCity.SelectedIndex = cmbCity.Items.IndexOfValue(MemberLicenceManager[0]["CitId"].ToString());
            if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["Avg"]))
                txtAvg.Text = Convert.ToDecimal(MemberLicenceManager[0]["Avg"]).ToString("##.00");
            txtDescription.Text = MemberLicenceManager[0]["Description"].ToString();
            txtEndDate.Text = MemberLicenceManager[0]["EndDate"].ToString();
            txtNumUnit.Text = MemberLicenceManager[0]["NumUnit"].ToString();
            txtStartDate.Text = MemberLicenceManager[0]["StartDate"].ToString();

            TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
            LicenceManager.FindByCode(int.Parse(CmbLicence.SelectedItem.Value.ToString()));
            float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

            int TableType = -1;
            if (IsTeacherMember())
                TableType = (int)(TSP.DataManager.TableCodes.MemberLicence);
            else
                TableType = (int)(TSP.DataManager.TableCodes.TeacherLicence);
            int TableId = MeLiId;
            //TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
            //DataTable dtJudgment = TeacherJudgmentManager.FindByTableType(int.Parse(TeacherId), TableId, TableType);
            //if (dtJudgment.Rows.Count > 0)
            //{
            //    txtJudgeView.Text = dtJudgment.Rows[0]["JudgeViewPoint"].ToString();
            //    if (LicenceManager.Count > 0)
            //    {
            //        SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());
            //    }
            //    int JudgeGrade = int.Parse(dtJudgment.Rows[0]["JudgeGrade"].ToString());
            //    if (SystemGrade == JudgeGrade)
            //        rbtnGrade.SelectedIndex = 0;
            //    else
            //        rbtnGrade.SelectedIndex = 1;
            //}
        }
    }

    private void ClearForm()
    {
        CmbLicence.SelectedIndex = 0;
        CmbMajor.SelectedIndex = 0;
        CmbUniversity.SelectedIndex = 0;
        CmbUniversity_SelectedIndexChanged(this, new EventArgs());
        //  cmbCity.SelectedIndex = 0;

        txtAvg.Text = "";
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtNumUnit.Text = "";
        txtStartDate.Text = "";
    }

    private void InsertTeacherLicence()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TransactionManager.Add(TeacherLicenceManger);
        TransactionManager.Add(AttachmentsManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            TransactionManager.BeginSave();

            DataRow TeacherLiRow = TeacherLicenceManger.NewRow();
            TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());

            DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(int.Parse(TeacherId));
            if (dtTeCert.Rows.Count > 0)
            {
                int TcId = int.Parse(dtTeCert.Rows[0]["TcId"].ToString());
                TeacherLiRow["TcId"] = TcId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پرونده ای برای این استاد تشکیل نشده است.";
                return;
            }
            TeacherLiRow["TeId"] = int.Parse(TeacherId);
            TeacherLiRow["LiId"] = int.Parse(CmbLicence.SelectedItem.Value.ToString());
            TeacherLiRow["UnId"] = int.Parse(CmbUniversity.SelectedItem.Value.ToString());
            TeacherLiRow["MjId"] = int.Parse(CmbMajor.SelectedItem.Value.ToString());
            TeacherLiRow["CitId"] = int.Parse(cmbCity.SelectedItem.Value.ToString());
            if (!Utility.IsDBNullOrNullValue(txtAvg.Text))
                TeacherLiRow["Avg"] = float.Parse(txtAvg.Text);
            if (!Utility.IsDBNullOrNullValue(txtNumUnit.Text))
                TeacherLiRow["NumUnit"] = txtNumUnit.Text;
            TeacherLiRow["StartDate"] = txtStartDate.Text;
            TeacherLiRow["EndDate"] = txtEndDate.Text;
            TeacherLiRow["Description"] = txtDescription.Text;
            //  TeacherLiRow["JudgeGrade"] = 0;
            TeacherLiRow["UserId"] = Utility.GetCurrentUser_UserId();
            TeacherLiRow["ModifiedDate"] = DateTime.Now;

            TeacherLicenceManger.AddRow(TeacherLiRow);

            int cn = TeacherLicenceManger.Save();
            InsertTeacherAttachment(int.Parse(TeacherLicenceManger[0]["TLiId"].ToString()), AttachmentsManager, TransactionManager);

            if (cn > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_TeLicence"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherLicence;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeacherId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_TeLicence"] = true;
                    HiddenFieldTecherLicence["PageMode"] = Utility.EncryptQS("Edit");
                    HiddenFieldTecherLicence["TeLiId"] = TeacherLicenceManger[0]["TLiId"].ToString();
                    RoundPanelTeacherLicence.HeaderText = "ویرایش";
                    TSP.DataManager.Permission per = TSP.DataManager.TeacherLicenceManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    BtnNew.Enabled = per.CanNew;
                    btnNew2.Enabled = per.CanNew;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnNew"] = BtnNew.Enabled;
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }

    }

    private void EditTeacherLicence(int TeLiId)
    {
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

        TransactionManager.Add(TeacherLicenceManger);
        TransactionManager.Add(AttachmentsManager);
        TransactionManager.Add(WorkFlowStateManager);

        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
        if (LoginManager.Count < 0)
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }

        try
        {
            TeacherLicenceManger.Fill();
            TeacherLicenceManger.FindByCode(TeLiId);
            if (TeacherLicenceManger.Count > 0)
            {
                TransactionManager.BeginSave();
                TeacherLicenceManger[0].BeginEdit();

                TeacherId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());

                TeacherLicenceManger[0]["TeId"] = int.Parse(TeacherId);
                TeacherLicenceManger[0]["LiId"] = int.Parse(CmbLicence.SelectedItem.Value.ToString());
                TeacherLicenceManger[0]["UnId"] = int.Parse(CmbUniversity.SelectedItem.Value.ToString());
                TeacherLicenceManger[0]["MjId"] = int.Parse(CmbMajor.SelectedItem.Value.ToString());
                TeacherLicenceManger[0]["CitId"] = int.Parse(cmbCity.SelectedItem.Value.ToString());
                TeacherLicenceManger[0]["Avg"] = float.Parse(txtAvg.Text);
                TeacherLicenceManger[0]["NumUnit"] = txtNumUnit.Text;
                TeacherLicenceManger[0]["StartDate"] = txtStartDate.Text;
                TeacherLicenceManger[0]["EndDate"] = txtEndDate.Text;
                TeacherLicenceManger[0]["Description"] = txtDescription.Text;
                TeacherLicenceManger[0]["UserId"] = (int)(LoginManager[0]["MeId"]);
                TeacherLicenceManger[0]["ModifiedDate"] = DateTime.Now;

                TeacherLicenceManger[0].EndEdit();
                int cn = TeacherLicenceManger.Save();
                if (!string.IsNullOrEmpty(Session["TeacherLiAttachFileAddress"].ToString()))
                {
                    InsertTeacherAttachment((int)(TeacherLicenceManger[0]["TLiId"]), AttachmentsManager, TransactionManager);
                }
                if (cn > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_TeLicence"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherLicence;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeacherId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_TeLicence"] = true;
                        HiddenFieldTecherLicence["PageMode"] = Utility.EncryptQS("Edit");
                        HiddenFieldTecherLicence["TeLiId"] = Utility.EncryptQS(TeacherLicenceManger[0]["TLiId"].ToString());
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {

            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);

        }
    }

    private void DeleteTeacherLicence(int TeacherLicenceId)
    {
        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
        TeacherLicenceManger.FindByCode(TeacherLicenceId);
        if (TeacherLicenceManger.Count > 0)
        {
            TeacherLicenceManger[0].Delete();
            int cn = TeacherLicenceManger.Save();
            if (cn > 0)
            {
                HiddenFieldTecherLicence["TeLiId"] = "";
                HiddenFieldTecherLicence["PageMode"] = Utility.EncryptQS("New");


                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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

    private void InsertTeacherAttachment(int RefTableId, TSP.DataManager.AttachmentsManager attManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        if (!string.IsNullOrEmpty(Session["TeacherLiAttachFileAddress"].ToString()))
        {
            string fileName = "", pathAx = "", extension = "";
            // byte[] img = null;
            //  bool AxImg = false;
            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.TeacherLicence;
            dr["RefTable"] = RefTableId;
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDescription.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            extension = Path.GetExtension(Session["TeacherLiAttachFileAddress"].ToString());
            extension = extension.ToLower();

            fileName = System.IO.Path.GetFileName(Session["TeacherLiAttachFileAddress"].ToString());
            //Utility.GenerateName(Path.GetExtension(Session["TeacherLiAttachFileAddress"].ToString()));
            pathAx = Server.MapPath("~/image/Temp/");
            // flp.SaveAs(pathAx + fileNameImg);
            dr["AtContent"] = DBNull.Value;
            dr["FilePath"] = "~/Image/Employee/Amoozesh/Attachments/" + fileName;
            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {
                //if (AxImg == true)
                //{           
                string ImgSoource = Session["TeacherLiAttachFileAddress"].ToString();//Server.MapPath("~/image/Temp/" + fileNameImg);
                string ImgTarget = Server.MapPath("~/image/Employee/Amoozesh/Attachments/" + fileName);
                File.Move(ImgSoource, ImgTarget);
                //}
                btnDeleteAttachment.ClientVisible = true;
                // flp.Visible = false;
                linkAttachment.Text = fileName;
                linkAttachment.Visible = true;
                linkAttachment.NavigateUrl = dr["FilePath"].ToString();
                btnAttachTechearLicenceFile.Visible = false;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
        }
    }

    private void DeleteTeacherAttachment(int TtId)
    {
        // int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
        DataTable dtAttach = attManager.FindByTablePrimaryKey((int)(TSP.DataManager.TableCodes.TeacherLicence), TtId);

        if (dtAttach.Rows.Count == 1)
        {
            attManager.ClearBeforeFill = true;
            int AttachId = int.Parse(dtAttach.Rows[0]["AttachId"].ToString());
            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        Session["TeacherLiAttachFileAddress"] = "";
                        //  flp.
                        //  this.DivReport.Visible = true;
                        //  this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
                {

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 547)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }
            }
        }
    }

    protected void UploaderImage_OnUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveTeacherLicenceFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected string SaveTeacherLicenceFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Employee/Amoozesh/Attachments/") + ret) == true || System.IO.File.Exists(MapPath("~/Images/Uploaded/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TeacherLiAttachFileAddress"] = tempFileName;
            Session["TeacherLiAttachFileAddressChange"] = 1;
        }
        return ret;
    }

    //private void SaveTeacherLicenceJudge(int TLiId)
    //{
    //    try
    //    {
    //        TSP.DataManager.TeacherLicenceManger TeacherLicenceManger = new TSP.DataManager.TeacherLicenceManger();
    //        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();

    //        LicenceManager.FindByCode(int.Parse(CmbLicence.SelectedItem.Value.ToString()));
    //        float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

    //        TeacherLicenceManger.FindByCode(TLiId);

    //        if (TeacherLicenceManger.Count == 1)
    //        {
    //            TeacherLicenceManger[0].BeginEdit();
    //            if (rbtnGrade.SelectedIndex == 0)
    //                TeacherLicenceManger[0]["JudgeGrade"] = SystemGrade;
    //            else
    //                TeacherLicenceManger[0]["JudgeGrade"] = 0;
    //            TeacherLicenceManger[0]["JudgeViewPoint"] = txtJudgeView.Text;
    //            TeacherLicenceManger[0]["MeetingId"] = 1;
    //            TeacherLicenceManger[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            TeacherLicenceManger[0]["Description"] = DateTime.Now;

    //            TeacherLicenceManger[0].EndEdit();

    //            int cn = TeacherLicenceManger.Save();

    //            if (cn > 0)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد.";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        SetError(err);
    //    }
    //}

    //private void SaveMemberLicenceJudge(int MeLiId)
    //{
    //    try
    //    {
    //        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
    //        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();

    //        LicenceManager.FindByCode(int.Parse(CmbLicence.SelectedItem.Value.ToString()));
    //        float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

    //        MemberLicenceManager.FindByCode(MeLiId);

    //        if (MemberLicenceManager.Count == 1)
    //        {
    //            MemberLicenceManager[0].BeginEdit();
    //            if (rbtnGrade.SelectedIndex == 0)
    //                MemberLicenceManager[0]["JudgeGrade"] = SystemGrade;
    //            else
    //                MemberLicenceManager[0]["JudgeGrade"] = 0;
    //            MemberLicenceManager[0]["JudgeViewPoint"] = txtJudgeView.Text;
    //            MemberLicenceManager[0]["MeetingId"] = 1;
    //            MemberLicenceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            MemberLicenceManager[0]["Description"] = DateTime.Now;

    //            MemberLicenceManager[0].EndEdit();

    //            int cn = MemberLicenceManager.Save();

    //            if (cn > 0)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد.";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        SetError(err);
    //    }
    //}

    private void InsertJudgment()
    {
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

        TransactionManager.Add(TeacherJudgmentManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            TransactionManager.BeginSave();

            int LiId = int.Parse(CmbLicence.SelectedItem.Value.ToString());
            LicenceManager.FindByCode(LiId);
            if (LicenceManager.Count > 0)
            {
                float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

                LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                string TeLiId = Utility.DecryptQS(HiddenFieldTecherLicence["TeLiId"].ToString());
                DataRow JudgmentRow = TeacherJudgmentManager.NewRow();
                string TeId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
                if (IsTeacherMember())
                    JudgmentRow["TableType"] = (int)(TSP.DataManager.TableCodes.MemberLicence);
                else
                    JudgmentRow["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherLicence);
                JudgmentRow["TableId"] = int.Parse(TeLiId);
                JudgmentRow["TeId"] = int.Parse(TeId);
                //if (rbtnGrade.SelectedIndex == 0)
                //    JudgmentRow["JudgeGrade"] = SystemGrade;
                //else
                //    JudgmentRow["JudgeGrade"] = 0;
                //JudgmentRow["JudgeViewPoint"] = txtJudgeView.Text;
                JudgmentRow["EmpId"] = EmpId;
                //JudgmentRow["MeetingId"] = "";
                JudgmentRow["UserId"] = Utility.GetCurrentUser_UserId();
                JudgmentRow["ModifiedDate"] = DateTime.Now;

                TeacherJudgmentManager.AddRow(JudgmentRow);
                int cn = TeacherJudgmentManager.Save();
                if (cn > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_TeLicence"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherLicence;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_TeLicence"] = true;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    private void EditJudgment(int JudgeId)
    {
        TSP.DataManager.TeacherJudgmentManager TeacherJudgmentManager = new TSP.DataManager.TeacherJudgmentManager();
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

        TeacherJudgmentManager.FindByCode(JudgeId);

        if (TeacherJudgmentManager.Count > 0)
        {
            TransactionManager.BeginSave();

            int LiId = int.Parse(CmbLicence.SelectedItem.Value.ToString());
            LicenceManager.FindByCode(LiId);
            if (LicenceManager.Count > 0)
            {
                float SystemGrade = float.Parse(LicenceManager[0]["Grade"].ToString());

                LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());

                TeacherJudgmentManager[0].BeginEdit();

                string TeId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
                if (IsTeacherMember())
                    TeacherJudgmentManager[0]["TableType"] = (int)(TSP.DataManager.TableCodes.MemberLicence);
                else
                    TeacherJudgmentManager[0]["TableType"] = (int)(TSP.DataManager.TableCodes.TeacherLicence);
                TeacherJudgmentManager[0]["TeId"] = int.Parse(TeId);
                //if (rbtnGrade.SelectedIndex == 0)
                //    TeacherJudgmentManager[0]["JudgeGrade"] = SystemGrade;
                //else
                //    TeacherJudgmentManager[0]["JudgeGrade"] = 0;
                TeacherJudgmentManager[0]["EmpId"] = EmpId;
                //TeacherJudgmentManager[0]["JudgeViewPoint"] = txtJudgeView.Text;
                //JudgmentRow["MeetingId"] = "";
                TeacherJudgmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherJudgmentManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherJudgmentManager[0].EndEdit();

                int cn = TeacherJudgmentManager.Save();
                if (cn > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_TeLicence"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.TeacherLicence;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, int.Parse(TeId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_TeLicence"] = true;
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        }
        else
        {
            TransactionManager.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }

    private Boolean IsTeacherMember()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeId));

        if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            return true;
        else
            return false;
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        }
    }

    private int FindTeacherCertificate(int TeId)
    {
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeId);
        int TeCertType = -1;
        if (dtTeCert.Rows.Count > 0)
        {
            TeCertType = int.Parse(dtTeCert.Rows[0]["Type"].ToString());
        }
        return TeCertType;


    }

    private void CheckCertificatePermission(int TeId)
    {
        int CertType = FindTeacherCertificate(TeId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده استاد انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldTecherLicence["PageMode"].ToString());
        if (CertType == 1 || CertType == 2)
        {
            switch (PageMode)
            {
                case "View":

                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    break;
                case "Edit":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTecherLicence["PageMode"].ToString());
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string TeId = Utility.DecryptQS(HiddenFieldTecherLicence["TeacherId"].ToString());
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        int ComissionGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.ComissionGradingTeacher;
        int CommitteeGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.CommitteeGradingTeacher;

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);

        TSP.DataManager.WFPermission WFPerComissionGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ComissionGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerCommitteeGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(CommitteeGradingTaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId(), PageMode);

        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerComissionGrading.BtnEdit || WFPerCommitteeGrading.BtnEdit;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave || WFPerComissionGrading.BtnSave || WFPerCommitteeGrading.BtnSave;
        BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || WFPerComissionGrading.BtnNew || WFPerCommitteeGrading.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    #endregion
    #endregion
}
