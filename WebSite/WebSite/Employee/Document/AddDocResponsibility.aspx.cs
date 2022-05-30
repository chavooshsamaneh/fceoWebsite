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

public partial class Employee_Document_AddDocResponsibility : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MFId"] == null || Request.QueryString["PrePgMd"] == null || Request.QueryString["MfdId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("DocResponsibility.aspx");
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


        if (!IsPostBack)
        {
            HiddenFieldMeFileDetail["TCondId"] = null;
            //  Session["IsEdited_EmpDocRes"] = null;
            Session["IsEdited_EmpDocRes"] = false;
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKeys();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

        }

        if (cmbMajor.SelectedItem != null && cmbMajor.SelectedItem.Value != null)
        {
            int cmbGradeIndex = cmbAcceptedGrd.SelectedIndex;
            string cmbGradeText = cmbAcceptedGrd.Text;
            int MFMjId = int.Parse(cmbMajor.SelectedItem.Value.ToString());
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            int MfId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            SetResponsblityGrades(MeId, MfId, MFMjId);
            string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());
            if (PageMode != "New")
            {
                if (cmbAcceptedGrd.SelectedIndex == -1)
                    cmbAcceptedGrd.Text = cmbGradeText;
                else
                    cmbAcceptedGrd.SelectedIndex = cmbGradeIndex;
            }
        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldMeFileDetail["MeId"]))
        {
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

    }

    #region btn Click
    protected void btnNew_Click(object sender, EventArgs e)
    {

        HiddenFieldMeFileDetail["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldMeFileDetail["MfdId"] = "";
        RoundPanelMeFileDetail.HeaderText = "جدید";
        cmbMajor.Enabled = true;
        ClearForm();
        EnableControls();
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int CurrentMfId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            int MfId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MfIdOrigin"].ToString()));
            if (CurrentMfId != MfId && (!CheckIfDocResponsblityBelongToOldSystem(MfId)))
            {
                ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
                return;
            }

            if (!CheckPermitionForEdit(CurrentMfId))
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.");
                return;
            }

            EnableControls();
            SetControlEnableForOldData();
            HiddenFieldMeFileDetail["PageMode"] = Utility.EncryptQS("Edit");
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
                  && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("DocResponsibility.aspx?PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&MFId=" + HiddenFieldMeFileDetail["MFId"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("DocResponsibility.aspx?PgMd=" + HiddenFieldMeFileDetail["PrePageMode"].ToString() + "&MFId=" + HiddenFieldMeFileDetail["MFId"].ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();
            }
            else if (PageMode == "Edit")
            {
                string MfdId = Utility.DecryptQS(HiddenFieldMeFileDetail["MfdId"].ToString());

                if (string.IsNullOrEmpty(MfdId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    if (Convert.ToBoolean(HiddenFieldMeFileDetail["IsOldCanEdit"]))
                        EditOldData(int.Parse(MfdId));
                    else
                        Edit(int.Parse(MfdId));
                }

            }
        }
    }
    #endregion

    protected void CallbackRes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] Parameters = e.Parameter.Split(';');
        int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
        int MfId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
        int MFMjId = -1;
        switch (Parameters[0])
        {
            case "Major":
                if (!string.IsNullOrWhiteSpace(Parameters[1]))
                {
                    MFMjId = Convert.ToInt32(Parameters[1]);
                    SetResponsblityGrades(MeId, MfId, MFMjId);
                }
                break;
            case "ActType":
                if (cmbMajor.SelectedItem != null && cmbMajor.SelectedItem.Value != null)
                {
                    MFMjId = Convert.ToInt32(cmbMajor.SelectedItem.Value);
                    SetResponsblityGrades(MeId, MfId, MFMjId);
                }
                break;
        }
    }
    #endregion

    #region Methods
    #region SetKey-SetModes
    private void SetKeys()
    {
        try
        {
            HiddenFieldMeFileDetail["IsOldCanEdit"] = false;
            HiddenFieldMeFileDetail["MfdId"] = Request.QueryString["MfdId"].ToString();
            HiddenFieldMeFileDetail["PageMode"] = Request.QueryString["PgMd"];
            HiddenFieldMeFileDetail["PrePageMode"] = Request.QueryString["PrePgMd"];
            HiddenFieldMeFileDetail["MFId"] = Request.QueryString["MFId"];

            string MfdId = Utility.DecryptQS(HiddenFieldMeFileDetail["MfdId"].ToString());
            string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());
            string PrePageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PrePageMode"].ToString());
            string MFId = Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString());

            ResetAcceptGradeObjd();
            cmbAcceptedGrd.DataBind();
            //cmbDocMeAcceptType.Items.Insert(0, new DevExpress.Web.ListEditItem("------------------------", -1));
            //cmbDocMeAcceptType.SelectedIndex = 0;
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            int DocRequestType = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
            if (DocMemberFileManager.Count == 1)
            {
                HiddenFieldMeFileDetail["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                ObjdsMemberFileMajor.SelectParameters["MFId"].DefaultValue = DocMemberFileManager[0]["MfId"].ToString();
                ObjdsMemberFileMajor.SelectParameters["MeId"].DefaultValue = DocMemberFileManager[0]["MeId"].ToString();
                SetInactiveForObjectdsMajor(PageMode);
                //if (PageMode == "View")
                //    ObjdsMemberFileMajor.SelectParameters["InActive"].DefaultValue = "-1";
                //else
                //    ObjdsMemberFileMajor.SelectParameters["InActive"].DefaultValue = "0";
                cmbMajor.DataBind();
                DocRequestType = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }


            SetMode(PageMode);
            CheckWorkFlowPermission();
            ArrayList PerByRequest = CheckPermissionByRequestType(DocRequestType);
            btnNew.Enabled = btnNew2.Enabled = Convert.ToBoolean(PerByRequest[0]);
            btnEdit.Enabled = btnEdit2.Enabled = Convert.ToBoolean(PerByRequest[1]);
            btnSave.Enabled = btnSave2.Enabled = Convert.ToBoolean(PerByRequest[2]);
          

            if (PageMode == "New")
                cmbDocResRange.SelectedIndex = 0;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    private void SetMode(string PageMode)
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
        }
    }

    private void SetViewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }
        if (!per.CanView)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            return;
        }
        DisabledControls();
        txtResDate.Enabled = false;
        if (HiddenFieldMeFileDetail["MfdId"] == null || (string.IsNullOrEmpty(HiddenFieldMeFileDetail["MfdId"].ToString())))
        {
            Response.Redirect("TestCondition.aspx");
            return;
        }
        int MfdId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MfdId"].ToString()));
        FillForm(MfdId);
        RoundPanelMeFileDetail.HeaderText = "مشاهده";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        EnableControls();
        ClearForm();
        RoundPanelMeFileDetail.HeaderText = "جدید";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        if (!per.CanView)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            return;
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (HiddenFieldMeFileDetail["MfdId"] == null || string.IsNullOrEmpty(HiddenFieldMeFileDetail["MfdId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int MfdId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MfdId"].ToString()));

        EnableControls();
        FillForm(MfdId);
        SetControlEnableForOldData();
        RoundPanelMeFileDetail.HeaderText = "ویرایش";
    }
    #endregion

    #region Fill Info
    private void FillForm(int MfdId)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DocMemberFileDetailManager.FindByCode(MfdId);
        if (DocMemberFileDetailManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        int MfId = int.Parse(DocMemberFileDetailManager[0]["MfId"].ToString());
        HiddenFieldMeFileDetail["MfIdOrigin"] = Utility.EncryptQS(MfId.ToString());
        int MeId = int.Parse(DocMemberFileDetailManager[0]["MeId"].ToString());

        int MfIdOrigin = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MfIdOrigin"].ToString()));
        HiddenFieldMeFileDetail["IsOldCanEdit"] = CheckIfDocResponsblityBelongToOldSystem(MfIdOrigin);

        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        int InActive = 0;
        if (PageMode == "View" || (Convert.ToBoolean(HiddenFieldMeFileDetail["IsOldCanEdit"])))
        {
            SetInactiveForObjectdsMajor(PageMode);
            cmbMajor.DataBind();
            InActive = -1;
        }
        DataTable dtMeDocMajor = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, InActive);
        dtMeDocMajor.DefaultView.RowFilter = "FMjId =" + DocMemberFileDetailManager[0]["MjId"].ToString();
        if (dtMeDocMajor.DefaultView.Count > 0)
        {

            int MFMjId = Convert.ToInt32(dtMeDocMajor.Rows[0]["MfMjId"]);
            if (cmbMajor.Items.FindByValue(dtMeDocMajor.Rows[0]["MfMjId"].ToString()) != null)
            {
                cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(dtMeDocMajor.Rows[0]["MfMjId"].ToString()).Index;
            }

            SetResponsblityGrades(MeId, MfId, MFMjId);

            if (!Utility.IsDBNullOrNullValue(DocMemberFileDetailManager[0]["GMRId"]))
            {
                if (cmbAcceptedGrd.Items.Count == 0 || cmbAcceptedGrd.Items.FindByValue(DocMemberFileDetailManager[0]["GMRId"].ToString()) == null)
                    cmbAcceptedGrd.Text = DocMemberFileDetailManager[0]["GMRName"].ToString();
                else if (cmbAcceptedGrd.Items.FindByValue(DocMemberFileDetailManager[0]["GMRId"].ToString()) != null)
                {
                    cmbAcceptedGrd.SelectedIndex = cmbAcceptedGrd.Items.FindByValue(DocMemberFileDetailManager[0]["GMRId"].ToString()).Index;
                }
            }
            if (!Utility.IsDBNullOrNullValue(DocMemberFileDetailManager[0]["ActTypeId"]))
            {
                cmbDocMeAcceptType.DataBind();
                cmbDocMeAcceptType.SelectedIndex = cmbDocMeAcceptType.Items.FindByValue(DocMemberFileDetailManager[0]["ActTypeId"].ToString()).Index;
            }
            if (!Utility.IsDBNullOrNullValue(DocMemberFileDetailManager[0]["ResRangeId"]))
            {
                cmbDocResRange.DataBind();
                cmbDocResRange.SelectedIndex = cmbDocResRange.Items.FindByValue(DocMemberFileDetailManager[0]["ResRangeId"].ToString()).Index;
            }
            if (!Utility.IsDBNullOrNullValue(DocMemberFileDetailManager[0]["Date"]))
            {
                txtResDate.Text = DocMemberFileDetailManager[0]["Date"].ToString();
            }
            HiddenFieldMeFileDetail["GMRId"] = Utility.EncryptQS(DocMemberFileDetailManager[0]["GMRId"].ToString());
        }
    }

    private int FillMemberLicence(int MFMjId)
    {
        int MjId = -1;
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileMajorManager.FindByCode(MFMjId);
        if (DocMemberFileMajorManager.Count == 1)
        {
            if (Convert.ToInt16(DocMemberFileMajorManager[0]["InActiveState"]) == 1)
            {
                lblWarningInActiveMajor.Text = "رشته موضوع پروانه انتخاب شده غیرفعال شده است و صلاحیت تعریف شده در گواهینامه چاپ نمی شود";
                lblWarningInActiveMajor.Visible = true;
            }
            else
                lblWarningInActiveMajor.Visible = false;
            //********رشته موضوع پروانه***********
            MjId = Convert.ToInt32(DocMemberFileMajorManager[0]["FMjId"]);
            //**************************************
            txtMeLicence.Text = DocMemberFileMajorManager[0]["MlName"].ToString();
            if (Convert.ToBoolean(DocMemberFileMajorManager[0]["IsMaster"]))
            {
                lblIsMasterMajor.ClientVisible = true;
            }
            else
            {
                lblIsMasterMajor.ClientVisible = false;
            }
        }
        return MjId;
    }
    #endregion

    #region Set Contrtols
    private void ClearForm()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());
        SetInactiveForObjectdsMajor(PageMode);
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = 0;
        cmbDocMeAcceptType.SelectedIndex = -1;
        cmbAcceptedGrd.SelectedIndex = -1;
        cmbDocResRange.SelectedIndex = 0;
        cmbAcceptedGrd.Text = "";
        txtResDate.Text = "";
        if (cmbMajor.SelectedItem != null && cmbMajor.SelectedItem.Value != null)
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            int MfId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            int MfMjId = Convert.ToInt32(cmbMajor.SelectedItem.Value);

            SetResponsblityGrades(MeId, MfId, MfMjId);
        }
    }

    private void EnableControls()
    {
        cmbAcceptedGrd.Enabled = true;
        cmbMajor.Enabled = true;
        cmbDocMeAcceptType.Enabled = true;
        txtResDate.Enabled = true;
        cmbDocResRange.Enabled = true;
    }

    private void DisabledControls()
    {
        cmbAcceptedGrd.Enabled = false;
        cmbMajor.Enabled = false;
        cmbDocMeAcceptType.Enabled = false;
        cmbDocResRange.Enabled = false;
    }
    #endregion

    #region Insert-Updates
    private void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager1 = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocAcceptedUpGradeManager DocAcceptedUpGradeManager = new TSP.DataManager.DocAcceptedUpGradeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileDetailManager);
        try
        {
            if (cmbAcceptedGrd.SelectedItem == null || Utility.IsDBNullOrNullValue(cmbAcceptedGrd.SelectedItem.Value))
            {
                ShowMessage("پایه و صلاحیت را انتخاب نمایید");
                return;
            }
            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            int GMRId = Convert.ToInt32(cmbAcceptedGrd.SelectedItem.Value);
            DocMemberFileDetailManager1.SearchMeFileDetail(MFId, MeId, GMRId, 0);
            if (DocMemberFileDetailManager1.Count > 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                return;
            }
            TransactionManager.BeginSave();

            DataRow MeFileDetailRow = DocMemberFileDetailManager.NewRow();
            MeFileDetailRow["MfId"] = MFId;
            if (cmbAcceptedGrd.SelectedItem == null)
            {
                ShowMessage("پایه-صلاحیت را انتخاب نمایید");
                TransactionManager.CancelSave();
                return;
            }
            MeFileDetailRow["GMRId"] = cmbAcceptedGrd.SelectedItem.Value.ToString();
            if (cmbDocMeAcceptType.SelectedItem == null)
            {
                ShowMessage("شیوه اخذ مدرک را انتخاب نمایید");
                TransactionManager.CancelSave();
                return;
            }
            if (cmbDocMeAcceptType.SelectedItem != null && Convert.ToInt32(cmbDocMeAcceptType.SelectedItem.Value) != -1)
                MeFileDetailRow["ActTypeId"] = cmbDocMeAcceptType.SelectedItem.Value.ToString();

            if (cmbDocResRange.SelectedItem != null && Convert.ToInt32(cmbDocResRange.SelectedItem.Value) != -1)
                MeFileDetailRow["ResRangeId"] = cmbDocResRange.SelectedItem.Value.ToString();
            // if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["CreateDate"]))
            if (!string.IsNullOrWhiteSpace(txtResDate.Text))
                MeFileDetailRow["Date"] = txtResDate.Text; // DocMemberFileManager[0]["CreateDate"].ToString();
            MeFileDetailRow["UserId"] = Utility.GetCurrentUser_UserId();
            MeFileDetailRow["ModifiedDate"] = DateTime.Now;


            DocMemberFileDetailManager.AddRow(MeFileDetailRow);

            int cn = DocMemberFileDetailManager.Save();
            if (cn > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_EmpDocRes"].ToString())))
                {
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                    int UpdateTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileDetail);
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, MFId, UpdateTableType, "اضافه کردن پایه-صلاحیت", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
                else
                {
                    TransactionManager.EndSave();
                    Session["IsEdited_EmpDocRes"] = true;
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    RoundPanelMeFileDetail.HeaderText = "ویرایش";
                    HiddenFieldMeFileDetail["GMRId"] = Utility.EncryptQS(DocMemberFileDetailManager[DocMemberFileDetailManager.Count - 1]["GMRId"].ToString());
                    HiddenFieldMeFileDetail["MfdId"] = Utility.EncryptQS(DocMemberFileDetailManager[DocMemberFileDetailManager.Count - 1]["MfdId"].ToString());
                    HiddenFieldMeFileDetail["PageMode"] = Utility.EncryptQS("Edit");
                    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnNew.Enabled = per.CanNew;
                    btnNew2.Enabled = per.CanNew;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = per.CanEdit;
                    btnSave2.Enabled = per.CanEdit;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["btnNew"] = btnNew.Enabled;
                    this.ViewState["BtnSave"] = btnSave.Enabled;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int MfdId)
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager1 = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileDetailManager);
        try
        {
            if (cmbAcceptedGrd.SelectedItem == null || Utility.IsDBNullOrNullValue(cmbAcceptedGrd.SelectedItem.Value))
            {
                ShowMessage("پایه و صلاحیت را انتخاب نمایید");
                return;
            }
            int CurrentGMRId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["GMRId"].ToString()));
            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            int GMRId = Convert.ToInt32(cmbAcceptedGrd.SelectedItem.Value);
            if (CurrentGMRId != GMRId)
            {
                DocMemberFileDetailManager1.SearchMeFileDetail(MFId, MeId, GMRId, 0);
                if (DocMemberFileDetailManager1.Count > 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                    return;
                }
            }
            DocMemberFileDetailManager.FindByCode(MfdId);
            if (DocMemberFileDetailManager.Count == 1)
            {
                TransactionManager.BeginSave();
                DocMemberFileDetailManager[0].BeginEdit();

                DocMemberFileDetailManager[0]["MfId"] = MFId;

                if (cmbAcceptedGrd.SelectedItem == null)
                {
                    ShowMessage("پایه-صلاحیت را انتخاب نمایید");
                    TransactionManager.CancelSave();
                    return;
                }
                DocMemberFileDetailManager[0]["GMRId"] = cmbAcceptedGrd.SelectedItem.Value.ToString();
                if (cmbDocMeAcceptType.SelectedItem == null)
                {
                    ShowMessage("شیوه اخذ صلاحیت را انتخاب نمایید");
                    TransactionManager.CancelSave();
                    return;
                }

                if (cmbDocMeAcceptType.SelectedItem != null && Convert.ToInt32(cmbDocMeAcceptType.SelectedItem.Value) != -1)
                    DocMemberFileDetailManager[0]["ActTypeId"] = cmbDocMeAcceptType.SelectedItem.Value.ToString();

                if (cmbDocResRange.SelectedItem != null && Convert.ToInt32(cmbDocResRange.SelectedItem.Value) != -1)
                    DocMemberFileDetailManager[0]["ResRangeId"] = cmbDocResRange.SelectedItem.Value.ToString();

                if (!string.IsNullOrWhiteSpace(txtResDate.Text))
                    DocMemberFileDetailManager[0]["Date"] = txtResDate.Text;
                DocMemberFileDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileDetailManager[0]["ModifiedDate"] = DateTime.Now;


                DocMemberFileDetailManager[0].EndEdit();
                int cn = DocMemberFileDetailManager.Save();
                if (cn > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_EmpDocRes"].ToString())))
                    {
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                        int UpdateTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileDetail);
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, MFId, UpdateTableType, "ویرایش کردن پایه-صلاحیت", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_EmpDocRes"] = true;
                        HiddenFieldMeFileDetail["GMRId"] = Utility.EncryptQS(GMRId.ToString());
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }


            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void EditOldData(int MfdId)
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileDetailManager);
        try
        {
            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MFId"].ToString()));
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldMeFileDetail["MeId"].ToString()));
            DocMemberFileDetailManager.FindByCode(MfdId);
            if (DocMemberFileDetailManager.Count == 1)
            {
                TransactionManager.BeginSave();
                DocMemberFileDetailManager[0].BeginEdit();

                if (cmbDocMeAcceptType.SelectedItem != null && Convert.ToInt32(cmbDocMeAcceptType.SelectedItem.Value) != -1)
                    DocMemberFileDetailManager[0]["ActTypeId"] = cmbDocMeAcceptType.SelectedItem.Value.ToString();

                if (cmbDocResRange.SelectedItem != null && Convert.ToInt32(cmbDocResRange.SelectedItem.Value) != -1)
                    DocMemberFileDetailManager[0]["ResRangeId"] = cmbDocResRange.SelectedItem.Value.ToString();

                if (!string.IsNullOrWhiteSpace(txtResDate.Text))
                    DocMemberFileDetailManager[0]["Date"] = txtResDate.Text;
                DocMemberFileDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileDetailManager[0]["ModifiedDate"] = DateTime.Now;

                DocMemberFileDetailManager[0].EndEdit();
                int cn = DocMemberFileDetailManager.Save();
                if (cn > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_EmpDocRes"].ToString())))
                    {
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                        int UpdateTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileDetail);
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, MFId, UpdateTableType, "ویرایش کردن پایه-صلاحیت انتقال یافته از سیستم قدیم", Utility.GetCurrentUser_UserId());
                    }
                    if (UpdateState == -4)
                    {
                        TransactionManager.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    }
                    else
                    {
                        TransactionManager.EndSave();
                        Session["IsEdited_EmpDocRes"] = true;
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }


            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    #endregion

    #region WF Permission
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                int PermissionDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                int PermissionDocUnitrRsp = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
                if (Permission > 0 || PermissionDocUnit > 0 || PermissionDocUnitrRsp>0 )
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldMeFileDetail["PageMode"].ToString());
            //CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
        int PermisssionDocUni = -1;
        PermisssionDocUni = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, int.Parse(MfId), (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
        int PermisssionDocUniRes = -1;
        PermisssionDocUniRes = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WFCode, int.Parse(MfId), (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || PermisssionDocUni > 0 || PermisssionDocUniRes>0 )
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    break;
            }
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        else
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

    }
    #endregion

    #region Errors-Warning Methods
    private void SetError(Exception err)
    {
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

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
    #region CheckConditions Methods
    /// <summary>
    /// تنها در درخواست صدور- ارتقاء پایه-انتقالی-درج صلاحیت جدید امکان تغییر پایه و صلاحیت وجود دارد
    /// </summary>
    /// <param name="DocType"></param>
    /// <returns> ArrayListPer[0] : CanNew ; ArrayListPer[1] : CanEdit; ArrayListPer[2] : CanInActive </returns>    
    private ArrayList CheckPermissionByRequestType(int DocType)
    {
        ArrayList Per = new ArrayList();
        Boolean CanNew = false;
        Boolean CanEdit = false;
        Boolean CanSave = false;
        Per.Add(CanNew);
        Per.Add(CanEdit);
        Per.Add(CanSave);
        string RequestComment = "";
        switch (DocType)
        {
            case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.UpGrade:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                //RequestComment = "در درخواست المثنی پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                CanNew = CanEdit = CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                //   RequestComment = "در درخواست تمدید پروانه اشتغال شما قادر به تغییر پایه-صلاحیت پروانه نمی باشید";
                CanNew = CanEdit = 
                CanSave = true;
                break;
            case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                CanNew =
                CanEdit =
                CanSave = true;
                break;
        }
        if (!string.IsNullOrEmpty(RequestComment))
        {
            txtRequestComment.Visible = true;
            txtRequestComment.Text = RequestComment;
        }
        else
            txtRequestComment.Visible = false;
        Per[0] = CanNew;
        Per[1] = CanEdit;
        Per[2] = CanSave;
        return Per;
    }

    private Boolean CheckIfDocResponsblityBelongToOldSystem(int SelectedResponsblityMfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(SelectedResponsblityMfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count != 1)
        {
            return false;
        }
        int MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
        int Type = Convert.ToInt32(DocMemberFileManager[0]["Type"]);
        if (Type != (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument
            && Type != (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew)
        {
            return false;
        }
        //درخواست چاپ شده تایید شده داشته باشد
        DocMemberFileManager.FindDocumentByRequestType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, 1, -1, 1);
        if (DocMemberFileManager.Count > 0)
            return false;

        if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocument)
        {
            DocMemberFileManager.FindDocumentByRequestType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile, -1, (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew, 0);
            if (DocMemberFileManager.Count > 0)
                return false;
            else
                return true;
        }
        else if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.OldDocumentRenew)
        {
            return true;
        }
        return false;
    }
    #endregion
    /// <summary>
    /// پر کردن کمبوباکس پایه-صلاحیت بر اساس رشته انتخاب شده و پایه های قبلی شخص
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="MfId"></param>
    /// <param name="MFMjId"></param>
    private void SetResponsblityGrades(int MeId, int MfId, int MFMjId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MjId = FillMemberLicence(MFMjId);
        int ObsGrdId = -1;
        int DesGrdId = -1;
        int ImpGrdId = -1;
        int MappingGrdId = -1;
        int TrafficGrdId = -1;
        int UrbanismGrdId = -1;
        int GasGrdId = -1;
        DocMemberFileManager.FindByCode(MfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        ResetAcceptGradeObjd();
        int ReqType = int.Parse(DocMemberFileManager[0]["Type"].ToString());
        int Grade3Id = -1;
        if (cmbDocMeAcceptType.SelectedItem != null && cmbDocMeAcceptType.SelectedItem.Value != null)
        {
            if (Convert.ToInt32(cmbDocMeAcceptType.SelectedItem.Value) == -1)
            {
                Grade3Id = (int)TSP.DataManager.DocumentGrads.Grade3;
            }
            else
            {
                TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
                DocMemberFileAcceptTypeManager.FindByCode(Convert.ToInt32(cmbDocMeAcceptType.SelectedItem.Value));
                if (DocMemberFileAcceptTypeManager.Count == 1
                    && Convert.ToInt32(DocMemberFileAcceptTypeManager[0]["ActTypeCode"]) != (int)TSP.DataManager.DocumentMemberFileAcceptType.GradeJumping)
                {
                    Grade3Id = (int)TSP.DataManager.DocumentGrads.Grade3;
                }
            }
        }
        else
            Grade3Id = (int)TSP.DataManager.DocumentGrads.Grade3;
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        //****ماکزیمم پایه هر صلاحیت در یک رشته مشخص را برای فرد به دست می آورد
        DataTable dtMeFileDetailObs = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Observation, MjId);
        DataTable dtMeFileDetailDes = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Design, MjId);
        DataTable dtMeFileDetailImp = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Implement, MjId);
        DataTable dtMeFileDetailMapping = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Mapping, MjId);
        DataTable dtMeFileDetailTraffic = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Traffic, MjId);
        DataTable dtMeFileDetailUrbanism = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, MjId);
        DataTable dtMeFileDetailGas = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(MfId, MeId, 0, (int)TSP.DataManager.DocumentResponsibilityType.Gas, MjId);
        if (dtMeFileDetailObs.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailObs.Rows[0]["MaxGradeId"]))
        {
            ObsGrdId = Convert.ToInt32(dtMeFileDetailObs.Rows[0]["MaxGradeId"]);
            ObsGrdId = ObsGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailObs.Rows[0]["MaxGradeId"]))
            ObsGrdId = Grade3Id;
        //**************************************************************
        if (dtMeFileDetailDes.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailDes.Rows[0]["MaxGradeId"]))
        {
            DesGrdId = Convert.ToInt32(dtMeFileDetailDes.Rows[0]["MaxGradeId"]);
            DesGrdId = DesGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailDes.Rows[0]["MaxGradeId"]))
            DesGrdId = Grade3Id;
        //**************************************************************
        if (dtMeFileDetailImp.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailImp.Rows[0]["MaxGradeId"]))
        {
            ImpGrdId = Convert.ToInt32(dtMeFileDetailImp.Rows[0]["MaxGradeId"]);
            ImpGrdId = ImpGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailImp.Rows[0]["MaxGradeId"]))
            ImpGrdId = Grade3Id;
        //**************************************************************
        if (dtMeFileDetailMapping.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailMapping.Rows[0]["MaxGradeId"]))
        {
            MappingGrdId = Convert.ToInt32(dtMeFileDetailMapping.Rows[0]["MaxGradeId"]);
            MappingGrdId = MappingGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailMapping.Rows[0]["MaxGradeId"]))
            MappingGrdId = Grade3Id;
        //**********************************************************
        if (dtMeFileDetailTraffic.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailTraffic.Rows[0]["MaxGradeId"]))
        {
            TrafficGrdId = Convert.ToInt32(dtMeFileDetailTraffic.Rows[0]["MaxGradeId"]);
            TrafficGrdId = TrafficGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailTraffic.Rows[0]["MaxGradeId"]))
            TrafficGrdId = Grade3Id;
        //**************************************************************
        if (dtMeFileDetailUrbanism.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailUrbanism.Rows[0]["MaxGradeId"]))
        {
            UrbanismGrdId = Convert.ToInt32(dtMeFileDetailUrbanism.Rows[0]["MaxGradeId"]);
            UrbanismGrdId = UrbanismGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailUrbanism.Rows[0]["MaxGradeId"]))
            UrbanismGrdId = Grade3Id;
        //***************************************************************
        if (dtMeFileDetailGas.Rows.Count == 1 && !Utility.IsDBNullOrNullValue(dtMeFileDetailGas.Rows[0]["MaxGradeId"]))
        {
            GasGrdId = Convert.ToInt32(dtMeFileDetailGas.Rows[0]["MaxGradeId"]);
            GasGrdId = GasGrdId - 1;
        }
        else if (Utility.IsDBNullOrNullValue(dtMeFileDetailGas.Rows[0]["MaxGradeId"]))
            GasGrdId = Grade3Id;
        //***************************************************************
        if (ReqType == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival)
        {
            ObsGrdId = -1;
            DesGrdId = -1;
            ImpGrdId = -1;
            MappingGrdId = -1;
            TrafficGrdId = -1;
            UrbanismGrdId = -1;
        }
        ObjdsAcceptGrad.SelectParameters["MjId"].DefaultValue = MjId.ToString();
        ObjdsAcceptGrad.SelectParameters["ObsGrdId"].DefaultValue = ObsGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["DesGrdId"].DefaultValue = DesGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["ImpGrdId"].DefaultValue = ImpGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["MappingGrdId"].DefaultValue = MappingGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["TrafficGrdId"].DefaultValue = TrafficGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["UrbanismGrdId"].DefaultValue = UrbanismGrdId.ToString();
        ObjdsAcceptGrad.SelectParameters["GasGrdId"].DefaultValue = GasGrdId.ToString();
        cmbAcceptedGrd.DataBind();
    }

    private void ResetAcceptGradeObjd()
    {
        ObjdsAcceptGrad.SelectParameters["MjId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["ObsGrdId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["DesGrdId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["ImpGrdId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["MappingGrdId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["TrafficGrdId"].DefaultValue = "-2";
        ObjdsAcceptGrad.SelectParameters["UrbanismGrdId"].DefaultValue = "-2";

    }

    private void SetControlEnableForOldData()
    {
        if (Convert.ToBoolean(HiddenFieldMeFileDetail["IsOldCanEdit"]))
        {
            txtMeLicence.Enabled = false;
            cmbAcceptedGrd.Enabled = false;
            cmbMajor.Enabled = false;
            txtComment.Text = "تنها قادر به ویرایش تاریخ و شیوه اخذ صلاحیت های انتقال یافته از سیستم قدیم هستید";
            txtComment.Visible = true;
        }
        else
            txtComment.Visible = false;
    }

    private void SetInactiveForObjectdsMajor(string PageMode)
    {
        if (PageMode == "View")
            ObjdsMemberFileMajor.SelectParameters["InActive"].DefaultValue = "-1";
        else
            ObjdsMemberFileMajor.SelectParameters["InActive"].DefaultValue = "0";
    }
    #endregion
}
