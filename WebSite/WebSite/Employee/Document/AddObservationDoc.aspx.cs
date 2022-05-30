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
using System.Globalization;

public partial class Employee_Document_AddObservationDoc : System.Web.UI.Page
{
    DataTable dtDocCity = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("ObservationDoc.aspx");
            }

            Session["DocCity"] = null;

            if (Session["DocCity"] == null)
            {
                dtDocCity = new DataTable();
                dtDocCity.Columns.Add("Id");
                dtDocCity.Columns["Id"].AutoIncrement = true;
                dtDocCity.Columns["Id"].AutoIncrementSeed = 1;
                dtDocCity.Constraints.Add("PK_ID", dtDocCity.Columns["Id"], true);
                dtDocCity.Columns.Add("CitCode");
                dtDocCity.Columns.Add("CitName");
                dtDocCity.Columns.Add("ImpCitId");
                dtDocCity.Columns.Add("CitId");
                dtDocCity.Columns["CitId"].AutoIncrement = true;
                dtDocCity.Columns["CitId"].AutoIncrementSeed = 1;

                Session["DocCity"] = dtDocCity;
            }
            else
                dtDocCity = (DataTable)Session["DocCity"];

            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            TSP.DataManager.Permission perCity = TSP.DataManager.DocImpDocCityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnAddCity.Enabled = perCity.CanNew;
            btnSearchCity.Enabled = perCity.CanNew;
            GridViewCity.Columns[0].Visible = perCity.CanDelete;

            SetKeys();
            this.ViewState["btnAddCity"] = btnAddCity.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        }
        if (this.ViewState["btnSearchCity"] != null)
            this.btnSearchCity.Enabled = (bool)this.ViewState["btnSearchCity"];
        if (this.ViewState["btnAddCity"] != null)
            this.btnAddCity.Enabled = (bool)this.ViewState["btnAddCity"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

        if (Utility.IsDBNullOrNullValue(txtMeId.Text.Trim()))
        {
            ShowMessage("لطفا کد عضویت را وارد نمائید.");
            return;
        }

        try
        {

            int MeId = int.Parse(txtMeId.Text.Trim());
            HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(txtMeId.Text.Trim());
            ClearFormBeforFill();
            FillForm();
            if (!CheckConditions(MeId)) return;

            ProvinceManager.FindByCode((int)Utility.GetCurrentProvinceId());
            if (ProvinceManager.Count == 1)
            {
                txtProvinceNameObs.Text = ProvinceManager[0]["PrName"].ToString();
            }
            string Today = Utility.GetDateOfToday();
            txtLastRegDateObs.Text = Today;

            PersianCalendar FC = new PersianCalendar();
            DateTime DtAddYear = FC.AddYears(DateTime.Now.Date, 1);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            string ExpireDate = PDate.GetYear(DtAddYear) + "/" + PDate.GetMonth(DtAddYear).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYear).ToString().PadLeft(2, '0');
            txtExpDateObs.Text = ExpireDate;

            //*****CheckPermission

            DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
            if (dtObsDoc.Rows.Count > 0)
            {
                ShowMessage("بیش از این برای این عضو مجوز نظارت تعریف شده است.");
                HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(dtObsDoc.Rows[0]["MfId"].ToString());
                SetEditModeKeys();
                //*****CheckPermission        
            }

            DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (dtImpDoc.Rows.Count > 0)
            {
                ShowMessage("بیش از این برای این عضو مجوز اجرا تعریف شده است.");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازخوانی اطلاعات عضو انجام گرفته است");
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());

            if (string.IsNullOrEmpty(MFId) && PageMode != "New")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "New":
                    InsertDocMemberFileObs();
                    break;
                case "Edit":
                    Edit(int.Parse(MFId));
                    break;
                case "Revival":
                    InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.Revival);

                    break;
                case "Change":
                    InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.Change);
                    break;
                case "Invalid":
                    InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.InActive);
                    break;

            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && HiddenFieldDocMemberFile["MFId"] != null)
        {
            string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("ObservationDoc.aspx?PostId=" + MfId + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("ObservationDoc.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditModeKeys();
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        if (Session["DocCity"] != null)
        {
            dtDocCity = (DataTable)Session["DocCity"];
            if (dtDocCity.DefaultView.Count == 2)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شما قادر به انتخاب بیش از دو شهر نمی باشید.";
                return;
            }
            for (int i = 0; i < dtDocCity.Rows.Count; i++)
            {
                if (dtDocCity.Rows[i].RowState != DataRowState.Deleted && dtDocCity.Rows[i]["CitName"].ToString() == txtCity.Text.Trim())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "شهر انتخاب شده تکراری می باشد.";
                    return;
                }
            }
            DataRow dr = dtDocCity.NewRow();

            try
            {
                dr["CitId"] = HiddenFieldDocMemberFile["CitId"].ToString();
                dr["CitName"] = txtCity.Text;
                dr["CitCode"] = HiddenFieldDocMemberFile["CitCode"].ToString();

                dtDocCity.Rows.Add(dr);
                GridViewCity.DataSource = dtDocCity;
                GridViewCity.DataBind();
                txtCity.Text = "";
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    //*********************************************************************************************************

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetObsDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }

    //*********************************************************************************************************

    protected void GridViewCity_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewCity.DataSource = (DataTable)Session["DocCity"];
        GridViewCity.DataBind();

        int Id = -1;
        if (GridViewCity.FocusedRowIndex > -1)
        {
            Id = GridViewCity.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {
            dtDocCity = (DataTable)Session["DocCity"];
            dtDocCity.Rows.Find(e.Keys["Id"]).Delete();
            //  dtMemberFileMajor.Rows[Id].Delete();
            Session["DocCity"] = dtDocCity;
            GridViewCity.DataSource = (DataTable)Session["DocCity"];
            GridViewCity.DataBind();
            dtDocCity = (DataTable)Session["DocCity"];
        }
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
        HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];
        int ObsDocId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

        if (PageMode != "New")
        {
            DocMemberFileManager.SelectObservationDoc(-1, ObsDocId);
            if (DocMemberFileManager.Count == 1)
            {
                HiddenFieldDocMemberFile["MemberFileId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                int MfId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MemberFileId"].ToString()));

                DocMemberFileManager.FindByCode(MfId, 0);
                if (DocMemberFileManager.Count == 1)
                {
                    HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
            }
            // string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
        CheckWorkFlowPermission();
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectObservationDoc(-1, int.Parse(MFId));
        if (DocMemberFileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        else
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                lblWorkFlowState.Visible = false;
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "Revival":
                SetRequestModeKeys(PageMode);
                lblWorkFlowState.Visible = false;
                break;

            case "Change":
                SetRequestModeKeys(PageMode);
                lblWorkFlowState.Visible = false;
                break;

            case "Invalid":
                SetRequestModeKeys(PageMode);
                lblWorkFlowState.Visible = false;
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        GridViewCity.Columns[0].Visible = false;
        DisableControls();
        TblTransfer.Visible = false;
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm();

        RoundPanelMemberFile.HeaderText = "مشاهده";
        if (!CheckPermitionForEdit(MFId))
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, MFId);

        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        EnableControls();
        TSP.DataManager.Permission perCity = TSP.DataManager.DocImpDocCityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnAddCity.Enabled = perCity.CanNew;
        btnSearchCity.Enabled = perCity.CanNew;
        GridViewCity.Columns[0].Visible = perCity.CanDelete;

        this.ViewState["btnAddCity"] = btnAddCity.Enabled;
        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        HiddenFieldDocMemberFile["MeId"] = "";
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldDocMemberFile["MFId"] = "";

        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //ImgMember.Visible = false;
        txtFollowCode.Visible = false;
        lblFollowCode.Visible = false;
        tblRegion.Visible = true;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        ClearForm();
        txtMeId.Enabled = true;
        RoundPanelMemberFile.HeaderText = "جدید";
        TblTransfer.Visible = false;
        RoundPanelObsDoc.Enabled = true;
        dtDocCity.Rows.Clear();
        Session["DocCity"] = dtDocCity;
        GridViewCity.DataSource = dtDocCity;
        GridViewCity.DataBind();
    }

    private void SetEditModeKeys()
    {
        if (HiddenFieldDocMemberFile["MFId"] != null || !string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
            if (CheckPermitionForEdit(MFId))
            {
                HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                BtnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;

                TSP.DataManager.Permission perCity = TSP.DataManager.DocImpDocCityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnAddCity.Enabled = perCity.CanNew;
                btnSearchCity.Enabled = perCity.CanNew;
                GridViewCity.Columns[0].Visible = perCity.CanDelete;
                this.ViewState["btnAddCity"] = btnAddCity.Enabled;
                this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = BtnNew.Enabled;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                RoundPanelObsDoc.Enabled = true;
                EnableControls();
                txtMeId.Enabled = false;
                FillForm();
                InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.DocMemberFile, MFId);
                RoundPanelMemberFile.HeaderText = "ویرایش";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از گردش کار وجود ندارد.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات وجود ندارد.";
        }
    }

    private void SetRequestModeKeys(string PageMode)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;

        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }



        FillForm();

        txtFollowCode.Text = "";
        lblFollowCode.Visible = false;
        txtFollowCode.Visible = false;

        RoundPanelMemberFile.HeaderText = "ثبت درخواست جدید-";
        RoundPanelMemberFile.Enabled = true;
        switch (PageMode)
        {
            case "Invalid":
                RoundPanelMemberFile.HeaderText += "درخواست ابطال";

                RoundPanelObsDoc.Enabled = false;
                tblRegion.Visible = false;
                break;
            case "Change":
                RoundPanelMemberFile.HeaderText += "درخواست تغییرات";
                break;
            case "Revival":
                RoundPanelMemberFile.HeaderText += "درخواست تمدید";
                cmbIsTemporary.SelectedIndex = 0;
                SetObsDocDefualtExpireDate(Convert.ToInt32(cmbIsTemporary.Value));
                RoundPanelObsDoc.Enabled = true;
                break;
        }
        txtMeId.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void InsertDocMemberFileObs()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocImpDocCityManager);
        TransactionManager.Add(TransferMemberManager);

        try
        {
            Boolean IsTransfer = false;
            string PreMFNo = "";
            string PrCode = "";
            PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();
            if (string.IsNullOrEmpty(PrCode))
            {
                ShowMessage("کد نظام مهندسی استان جاری مشخص نمی باشد");
                return;
            }

            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            if (!CheckConditions(MeId)) return;

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
            if (dtObsDoc.Rows.Count > 0)
            {
                ShowMessage("پیش از این برای این عضو مجوز نظارت تعریف شده است.");
                SetEditModeKeys();
                txtMeId.Enabled = true;

                return;
            }

            DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (dtImpDoc.Rows.Count > 0)
            {
                ShowMessage("پیش از این برای این عضو مجوز اجرا تعریف شده است.");
                txtMeId.Enabled = true;
                return;
            }


            TransactionManager.BeginSave();

            DataRow MemberFileRow = DocMemberFileManager.NewRow();
            MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ObservationDocument);
            MemberFileRow["MeId"] = Utility.DecryptQS(HiddenFieldDocMemberFile["LastMemberFileId"].ToString());//****پروانه اشتغال به کار عضو Id
            MemberFileRow["DocType"] = 2;//****مجوز ناظر حقیقی
            if (!Utility.IsDBNullOrNullValue(txtSerialNo.Text))
                MemberFileRow["SerialNo"] = txtSerialNo.Text;
            MemberFileRow["RegDate"] = txtLastRegDateObs.Text;
            MemberFileRow["ExpireDate"] = txtExpDateObs.Text;
            MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();
            MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New; //*****صدور
            MemberFileRow["IsConfirm"] = 0;
            MemberFileRow["IsTemporary"] = 0;
            MemberFileRow["InActive"] = 0;
            MemberFileRow["Description"] = "";
            MemberFileRow["CreateDate"] = Utility.GetDateOfToday();
            MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberFileRow["ModifiedDate"] = DateTime.Now;

            //TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            //{
            //    #region TransferMember
            //    TransferMemberManager[0].BeginEdit();

            //    if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
            //        TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;

            //    TransferMemberManager[0].EndEdit();
            //    if (TransferMemberManager.Save() <= 0)
            //    {
            //        TransactionManager.CancelSave();
            //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            //        return;
            //    }
            //    if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
            //        MemberFileRow["PrIdOrigin"] = Convert.ToInt32(TransferMemberManager[0]["DocPrId"]);
            //    MemberFileRow["MFNoOrigin"] = TransferMemberManager[0]["FileNo"].ToString();
            //    PreMFNo = TransferMemberManager[0]["FileNo"].ToString();
            //    PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
            //    //if (cmbTransferType.Value != null)
            //    //    MemberFileRow["Type"] = Convert.ToInt32(cmbTransferType.Value);// TSP.DataManager.DocumentOfMemberRequestType.Transfer;//*****انتقالی
            //    IsTransfer = true;
            //    #endregion
            //}
            //else
            //{
            //    //*****صدور
            //    MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New;
            //}

            DocMemberFileManager.AddRow(MemberFileRow);
            int cn = DocMemberFileManager.Save();
            DocMemberFileManager.DataTable.AcceptChanges();
            if (cn > 0)
            {
                #region SaveCity

                dtDocCity = (DataTable)Session["DocCity"];

                if (dtDocCity.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtDocCity.DefaultView.Count; i++)
                    {
                        DataRow ImpDocCity = DocImpDocCityManager.NewRow();
                        ImpDocCity["MfId"] = DocMemberFileManager[DocMemberFileManager.Count - 1]["MFId"].ToString();

                        ImpDocCity["CitId"] = dtDocCity.Rows[i]["CitId"].ToString();

                        ImpDocCity["UserId"] = Utility.GetCurrentUser_UserId();
                        ImpDocCity["ModifiedDate"] = DateTime.Now;
                        DocImpDocCityManager.AddRow(ImpDocCity);

                        int cnt = DocImpDocCityManager.Save();
                        DocImpDocCityManager.DataTable.AcceptChanges();
                        if (cnt < 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            return;
                        }

                    }
                }
                #endregion

                #region Create MfNo
                string MfCode = "88";
                string ImpDocCode = "88";
                string MfSerialNo = DocMemberFileManager[0]["MFSerialNo"].ToString();
                int SerialLen = MfSerialNo.Length;
                int t = 5 - SerialLen;
                for (int i = 0; i < t; i++)
                {
                    MfSerialNo = "0" + MfSerialNo;
                }

                MfCode = ImpDocCode + "-" + PrCode + "-" + MfSerialNo;
                DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
                if (!IsTransfer)
                {
                    DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"] = MfCode;
                    DocMemberFileManager[DocMemberFileManager.Count - 1]["MFSerialNo"] = MfSerialNo;
                }
                else
                    DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"] = MfCode;
                DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
                if (DocMemberFileManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                #endregion

                #region WorkFlow
                int TaskId = -1;
                int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count != 1)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                int CurrentNmcId = FindNmcId(TaskId);
                if (CurrentNmcId == -1)
                {
                    TransactionManager.CancelSave();
                    return;
                }

                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
                if (WfStart > 0)
                {
                    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = per.CanEdit;
                    btnSave2.Enabled = per.CanEdit;
                    BtnNew.Enabled = per.CanNew;
                    btnNew2.Enabled = per.CanNew;

                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnNew"] = BtnNew.Enabled;
                    this.ViewState["BtnSave"] = btnSave.Enabled;

                    HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                    HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(TableId.ToString());
                    RoundPanelMemberFile.HeaderText = "ویرایش";

                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام گرفت.";
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
                #endregion
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TransactionManager.Add(DocImpDocCityManager);
        TransactionManager.Add(DocMemberFileManager);

        try
        {
            TransactionManager.BeginSave();
            DocMemberFileManager.SelectObservationDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["MailNo"] = "";
                DocMemberFileManager[0]["MailDate"] = "";
                if (!Utility.IsDBNullOrNullValue(txtSerialNo.Text))
                    DocMemberFileManager[0]["SerialNo"] = txtSerialNo.Text;
                DocMemberFileManager[0]["RegDate"] = txtLastRegDateObs.Text;
                DocMemberFileManager[0]["ExpireDate"] = txtExpDateObs.Text;
                DocMemberFileManager[0]["IsTemporary"] = cmbIsTemporary.SelectedItem.Value.ToString();
                DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;

                DocMemberFileManager[0].EndEdit();
                int cn = DocMemberFileManager.Save();
                if (cn > 0)
                {
                    #region City
                    if (Session["DocCity"] != null)
                    {
                        dtDocCity = (DataTable)Session["DocCity"];
                        if (dtDocCity.GetChanges() != null)
                        {
                            DataRow[] delRows = dtDocCity.Select(null, null, DataViewRowState.Deleted);
                            DataRow[] insRows = dtDocCity.Select(null, null, DataViewRowState.Added);

                            #region Deleted City
                            if (delRows.Length > 0)
                            {
                                for (int i = 0; i < delRows.Length; i++)
                                {
                                    DocImpDocCityManager.FindByCode(int.Parse(delRows[i]["ImpCitId", DataRowVersion.Original].ToString()));
                                    if (DocImpDocCityManager.Count <= 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                    DocImpDocCityManager[0].Delete();

                                    int SaveDel = DocImpDocCityManager.Save();
                                    DocImpDocCityManager.DataTable.AcceptChanges();
                                    if (SaveDel < 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                }
                            }
                            #endregion
                            #region Inserted City
                            if (insRows.Length > 0)
                            {
                                for (int i = 0; i < insRows.Length; i++)
                                {
                                    DataRow drCity = DocImpDocCityManager.NewRow();
                                    drCity["MfId"] = MfId;
                                    drCity["CitId"] = insRows[i]["CitId"].ToString();
                                    drCity["UserId"] = Utility.GetCurrentUser_UserId();
                                    drCity["ModifiedDate"] = DateTime.Now;
                                    DocImpDocCityManager.AddRow(drCity);
                                    if (DocImpDocCityManager.Save() <= 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                    DocImpDocCityManager.DataTable.AcceptChanges();
                                    if (dtDocCity.Rows[i].RowState != DataRowState.Deleted)
                                    {
                                        dtDocCity.Rows[i].BeginEdit();
                                        dtDocCity.Rows[i]["ImpCitId"] = DocImpDocCityManager[DocImpDocCityManager.Count - 1]["ImpCitId"].ToString();
                                        dtDocCity.Rows[i].EndEdit();
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    TransactionManager.CancelSave();
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                TransactionManager.CancelSave();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
            TransactionManager.CancelSave();
        }
    }

    private void FillForm()
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();

        int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));

        #region Member
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            txtMeId.Text = MemberManager[0]["MeId"].ToString();
            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
            txtImpName.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtDesName.Text = MemberManager[0]["DesGrdName"].ToString();
            txtObsName.Text = MemberManager[0]["ObsGrdName"].ToString();
            txtUrbenismName.Text = MemberManager[0]["UrbanismGrdName"].ToString();
            txtTrafficName.Text = MemberManager[0]["TrafficGrdName"].ToString();
            txtMappingName.Text = MemberManager[0]["MappingGrdName"].ToString();

            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                //ImgMember.Visible = true;
                ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            }
            else
            {
                ImgMember.ImageUrl = "~/Images/Person.png";
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["CitId"]))
            {
                CityManager.FindByCode((int)MemberManager[0]["CitId"]);
                if (CityManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(CityManager[0]["ReCitId"]))
                    {
                        HiddenFieldDocMemberFile["ReCitId"] = CityManager[0]["ReCitId"].ToString();
                        txtRegionOfCity.Text = CityManager[0]["ReCitName"].ToString();
                        ObjdsCity.SelectParameters[0].DefaultValue = CityManager[0]["ReCitId"].ToString();
                    }
                    else
                    {
                        HiddenFieldDocMemberFile["ReCitId"] = Utility.EncryptQS("-1");
                        ObjdsCity.SelectParameters[0].DefaultValue = "-1";
                    }

                }
            }

            #region Transfer
            TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            if (TransferMemberManager.Count > 0)
            {
                TblTransfer.Visible = true;
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                    lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
                    lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferDate"]))
                    lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                    lblPreProvince.Text = TransferMemberManager[0]["PrName"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
                {
                    ComboDocPr.DataBind();
                    ComboDocPr.SelectedIndex = ComboDocPr.Items.FindByValue(TransferMemberManager[0]["DocPrId"].ToString()).Index;
                }

                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["ImageUrl"]))
                    HyperLinkTransfer.NavigateUrl = TransferMemberManager[0]["ImageUrl"].ToString();
            }
            else
            {
                TblTransfer.Visible = false;
            }
            #endregion
        }
        #endregion

        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        if (dtMeFile.Rows.Count == 1)
        {
            txtExpireDateMember.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
            lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
        }

        DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
        if (DocMemberFileMajorManager.Count == 1)
        {
            txtMemberFileMajor.Text = DocMemberFileMajorManager[0]["MjName"].ToString();
        }

        #region DocMemberFile
        DocMemberFileManager.SelectObsDocLastVersion(MeId, -1);
        if (DocMemberFileManager.Count == 1)
        {

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                txtExpDateObs.Text = DocMemberFileManager[0]["ExpireDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                txtLastRegDateObs.Text = DocMemberFileManager[0]["RegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                txtRegDateObs.Text = DocMemberFileManager[0]["RegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                txtMfNoObs.Text = DocMemberFileManager[0]["MFNo"].ToString();

            string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["FollowCode"]))
            {
                txtFollowCode.Text = DocMemberFileManager[0]["FollowCode"].ToString();
            }
            else
            {
                lblFollowCode.Visible = false;
                txtFollowCode.Visible = false;
            }
            cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());

            int DocMemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
            int MfId = int.Parse(DocMemberFileManager[0]["MfId"].ToString());

            #region City
            TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
            DataTable dtImpCity = DocImpDocCityManager.FindMfId(MfId);
            dtDocCity = (DataTable)Session["DocCity"];
            for (int i = 0; i < dtImpCity.Rows.Count; i++)
            {
                DataRow dr = dtDocCity.NewRow();
                dr["ImpCitId"] = dtImpCity.Rows[i]["ImpCitId"];
                dr["CitId"] = dtImpCity.Rows[i]["CitId"];
                dr["CitCode"] = dtImpCity.Rows[i]["CitCode"].ToString();
                dr["CitName"] = dtImpCity.Rows[i]["CitName"].ToString();
                dtDocCity.Rows.Add(dr);
            }
            dtDocCity.AcceptChanges();
            GridViewCity.DataSource = dtDocCity;
            GridViewCity.DataBind();
            #endregion

            DataTable dtDocMeDetail = DocMemberFileDetailManager.FindByResponsibility(DocMemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            if (dtDocMeDetail.Rows.Count > 0)
            {
                DataTable dtMFMajor = DocMemberFileMajorManager.SelectMemberFileById(DocMemberFileId, MeId, 0, 1);
                if (dtMFMajor.Rows.Count > 0)
                {
                    int MasterMjId = (int)dtMFMajor.Rows[0]["FMjId"];

                    if (dtDocMeDetail.Rows[0]["MjId"].ToString() == dtMFMajor.Rows[0]["FMjId"].ToString())
                    {
                        txtGradeObs.Text = "";
                        for (int i = 0; i < dtDocMeDetail.Rows.Count; i++)
                        {
                            txtGradeObs.Text += "رشته " + dtDocMeDetail.Rows[i]["MjName"].ToString() + ":" + dtDocMeDetail.Rows[i]["GrdName"].ToString() + " ; ";
                        }
                    }
                }
            }

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
            {
                ProvinceManager.FindByCode(int.Parse(DocMemberFileManager[0]["PrId"].ToString()));
                if (ProvinceManager.Count > 0)
                {
                    txtProvinceNameObs.Text = ProvinceManager[0]["PrName"].ToString();
                }
            }
        }
        #endregion
    }

    bool CheckConditions(int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        MemberManager.FindByCode(MeId);
        HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(MeId.ToString());
        if (MemberManager.Count != 1)
        {
            ShowMessage("کد عضویت وارد شده معتبر نمی باشد.");
            return false;
        }
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
        {
            ShowMessage(Msg);
            return false;
        }

        //********************************افرادی که در شرکت هستند جهت کار نظارت مجوز نظارت بایستی داشته باشند؟؟؟؟؟؟******************************************************
        #region Check OfficeMember
        //OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
        //if (OfMeManager.Count > 0)
        //{
        //    int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
        //    DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
        //    if (dtEngOffReq.Rows.Count > 0)
        //    {
        //        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در دفتر ";
        //        str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
        //        str += " مشغول به کار می باشد";
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = (str);
        //        return false;
        //    }
        //}
        //OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
        //if (OfMeManager.Count > 0)
        //{
        //    int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
        //    DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfIdMember, 0, MeId);
        //    if (dtOffReq.Rows.Count > 0)
        //    {
        //        string str = "امکان ثبت عضو در این دفتر وجود ندارد.عضو مورد نظر در شرکت ";
        //        str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
        //        str += " مشغول به کار می باشد";
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = (str);
        //        return false;
        //    }
        //}
        #endregion

        #region CheckMemberFile
        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        if (dtMeFile.Rows.Count <= 0)
        {
            ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار تایید شده نمی باشد.");
            return false;
        }
        HiddenFieldDocMemberFile["LastMemberFileId"] = Utility.EncryptQS(dtMeFile.Rows[0]["MfId"].ToString());
        int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
        //Boolean HasObsResponsibility = true;
        //Boolean HasObsResponsibility = true;
        DataTable dtMeDetailObs = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
        DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
        if (dtMeDetailObs.Rows.Count <= 0 && dtMeDetailMapping.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نداشتن هیچ یک از صلاحیت های نظارت و نقشه برداری امکان درخواست مجوز نظارت برای عضو انتخاب شده وجود ندارد.");
            return false;
        }

        string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
        if (!string.IsNullOrEmpty(ExpireDate))
        {
            if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
            {
                ShowMessage("بدلیل اتمام مدت اعتبار پروانه اشتغال امکان درخواست مجوز نظارت برای عضو انتخاب شده وجود ندارد.");
                return false;
            }
        }

        if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
        {
            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count <= 0)
            {
                ShowMessage("رشته موضوع پروانه شخص انتخاب شده نامشخص است");
                return false;
            }
        }
        else
        {
            ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نامشخص می باشد.");
            return false;
        }
        #endregion

        return true;
    }

    private void ClearForm()
    {
        cmbIsTemporary.SelectedIndex = 0;
        txtSerialNo.Text = "";
        txtRegDateObs.Text = "";
        txtProvinceNameObs.Text = "";
        txtMfNoObs.Text = "";
        txtExpDateObs.Text = "";
        txtFollowCode.Text = "";
        txtGradeObs.Text = "";
        txtLastRegDateObs.Text = "";
        ImgMember.ImageUrl = "~/Images/Person.png";
        lblWorkFlowState.Visible = false;
        txtCity.Text = "";
        txtRegionOfCity.Text = "";
        txtDesName.Text = "";
        txtObsName.Text = "";
        txtImpName.Text = "";
        txtMemberFileMajor.Text = "";
        txtMeId.Text = "";
        lblMeName.Text = "";
        lblMeLastName.Text = "";
        lblMFNo.Text = "";
        txtExpireDateMember.Text = "";

        lblPreProvince.Text = "";
        lblTransferDate.Text = "";
        lblFileNo.Text = "";
        lblPreMeNo.Text = "";
        ComboDocPr.SelectedIndex = -1;
    }

    private void ClearFormBeforFill()
    {
        cmbIsTemporary.SelectedIndex = 0;
        txtSerialNo.Text = "";
        txtRegDateObs.Text = "";
        txtProvinceNameObs.Text = "";
        txtMfNoObs.Text = "";
        txtExpDateObs.Text = "";
        txtFollowCode.Text = "";
        txtGradeObs.Text = "";
        txtLastRegDateObs.Text = "";
        ImgMember.ImageUrl = "~/Images/Person.png";
        lblWorkFlowState.Visible = false;
        txtCity.Text = "";
        txtRegionOfCity.Text = "";
        txtDesName.Text = "";
        txtObsName.Text = "";
        txtImpName.Text = "";
        txtMemberFileMajor.Text = "";
        txtMeId.Text = "";

        lblMeName.Text = "";
        lblMeLastName.Text = "";
        lblMFNo.Text = "";
        txtExpireDateMember.Text = "";

        lblPreProvince.Text = "";
        lblTransferDate.Text = "";
        lblFileNo.Text = "";
        lblPreMeNo.Text = "";
        ComboDocPr.SelectedIndex = -1;
    }

    private void EnableControls()
    {
        RoundPanelMemberFile.Enabled = true;
    }

    private void DisableControls()
    {
        RoundPanelMemberFile.Enabled = false;
    }

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
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

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        //if (dtWorkFlowState.Rows.Count > 0)
                        //{
                        //int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        //int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                        //int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                        //if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                        //{
                        //if (FirstNmcIdType == 0)
                        //{
                        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        if (Permission > 0)
                            return true;
                        else
                            return false;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        //}
                        //else
                        //{
                        //    return false;
                        //}
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
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
                default:
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
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
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
                case "ReDuplicate":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "Revival":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "Change":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "UpGrade":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "Qualification":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
            }
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات انجام گرفته است";
            }
        }
        catch (Exception err)
        {
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
    }

    private void InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType DocumentOfMemberRequestType)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(DocImpDocCityManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            if (Utility.IsDBNullOrNullValue(MeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            DocMemberFileManager.SelectObsDocLastVersion(MeId, -1, 1);
            if (DocMemberFileManager.Count == 1)
            {
                #region Insert Request
                TransactionManager.BeginSave();
                DataRow MeFileRow = DocMemberFileManager.NewRow();
                if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Revival)
                {
                    string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                    Utility.Date objDate = new Utility.Date(CrtEndDate);
                    string LastMonth = objDate.AddMonths(-1);
                    string Today = Utility.GetDateOfToday();
                    int IsDocExp = string.Compare(Today, LastMonth);
                    if (IsDocExp <= 0)
                    {
                        TransactionManager.CancelSave();
                        ShowMessage("تاریخ اعتبار مجوز انتخاب شده به پایان نرسیده است.");
                        return;
                    }
                }

                MeFileRow["RegDate"] = txtLastRegDateObs.Text.Trim();
                MeFileRow["ExpireDate"] = txtExpDateObs.Text.Trim();
                MeFileRow["SerialNo"] = txtSerialNo.Text.Trim();

                MeFileRow["CreateDate"] = Utility.GetDateOfToday();
                MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ObservationDocument);
                MeFileRow["MailNo"] = "";
                MeFileRow["MailDate"] = "";
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
                    MeFileRow["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFSerialNo"]))
                    MeFileRow["MFSerialNo"] = DocMemberFileManager[0]["MFSerialNo"].ToString();
                MeFileRow["Type"] = (int)DocumentOfMemberRequestType;
                MeFileRow["DocType"] = 2;
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                    MeFileRow["PrId"] = DocMemberFileManager[0]["PrId"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrIdOrigin"]))
                    MeFileRow["PrIdOrigin"] = DocMemberFileManager[0]["PrIdOrigin"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    MeFileRow["MFNo"] = DocMemberFileManager[0]["MFNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNoOrigin"]))
                    MeFileRow["MFNoOrigin"] = DocMemberFileManager[0]["MFNoOrigin"].ToString();
                MeFileRow["IsConfirm"] = 0;
                MeFileRow["InActive"] = 0;
                MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
                MeFileRow["ModifiedDate"] = DateTime.Now;
                DocMemberFileManager.AddRow(MeFileRow);
                int cn = DocMemberFileManager.Save();
                #endregion
                if (cn > 0)
                {
                    #region  WF
                    int TableId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"]);
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count != 1)
                    {
                        TransactionManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        return;
                    }
                    int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                    int CurrentNmcId = FindNmcId(SaveInfoTaskId);
                    if (CurrentNmcId == -1)
                    {
                        TransactionManager.CancelSave();
                        return;
                    }
                    string Description = "شروع گردش کار درخواست جدید در مجوز ناظر حقیقی";
                    int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
                    if (WfStart <= 0)
                    {
                        TransactionManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                        return;
                    }
                    #endregion

                    #region City
                    if (Session["DocCity"] != null)
                    {
                        dtDocCity = (DataTable)Session["DocCity"];
                        if (dtDocCity.GetChanges() != null)
                        {
                            DataRow[] delRows = dtDocCity.Select(null, null, DataViewRowState.Deleted);
                            DataRow[] insRows = dtDocCity.Select(null, null, DataViewRowState.Added);

                            #region Deleted City
                            if (delRows.Length > 0)
                            {
                                for (int i = 0; i < delRows.Length; i++)
                                {
                                    DocImpDocCityManager.FindByCode(int.Parse(delRows[i]["ImpCitId", DataRowVersion.Original].ToString()));
                                    if (DocImpDocCityManager.Count <= 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                    DocImpDocCityManager[0].Delete();

                                    int SaveDel = DocImpDocCityManager.Save();
                                    DocImpDocCityManager.DataTable.AcceptChanges();
                                    if (SaveDel < 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                }
                            }
                            #endregion
                            #region Inserted City
                            if (insRows.Length > 0)
                            {
                                for (int i = 0; i < insRows.Length; i++)
                                {
                                    DataRow drCity = DocImpDocCityManager.NewRow();
                                    drCity["MfId"] = TableId;
                                    drCity["CitId"] = insRows[i]["CitId"].ToString();
                                    drCity["UserId"] = Utility.GetCurrentUser_UserId();
                                    drCity["ModifiedDate"] = DateTime.Now;
                                    DocImpDocCityManager.AddRow(drCity);
                                    if (DocImpDocCityManager.Save() <= 0)
                                    {
                                        TransactionManager.CancelSave();
                                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                    DocImpDocCityManager.DataTable.AcceptChanges();
                                    if (dtDocCity.Rows[i].RowState != DataRowState.Deleted)
                                    {
                                        dtDocCity.Rows[i].BeginEdit();
                                        dtDocCity.Rows[i]["ImpCitId"] = DocImpDocCityManager[DocImpDocCityManager.Count - 1]["ImpCitId"].ToString();
                                        dtDocCity.Rows[i].EndEdit();
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    TransactionManager.EndSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = ("ذخیره انجام شد.");

                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"]))
                        lblMFNo.Text = DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"].ToString();

                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[DocMemberFileManager.Count - 1]["FollowCode"]))
                    {
                        lblFollowCode.Visible = true;
                        txtFollowCode.Visible = true;
                        txtFollowCode.Text = DocMemberFileManager[DocMemberFileManager.Count - 1]["FollowCode"].ToString();
                    }

                    DocMemberFileManager.SelectObservationDoc(-1, (int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString())));
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                    {
                        lblWorkFlowState.Visible = true;
                        lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
                    }


                    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = per.CanEdit;
                    btnSave2.Enabled = per.CanEdit;
                    this.ViewState["BtnSave"] = btnSave.Enabled;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                    txtMeId.Enabled = false;
                    RoundPanelMemberFile.Enabled = true;
                    RoundPanelMemberFile.HeaderText = "ویرایش";
                }
                else
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                }
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    private void SetObsDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtLastRegDateObs.Text))
        {
            txtLastRegDateObs.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtLastRegDateObs.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {
            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                MonthCount = 12 * Utility.GetDefaultTemporaryObserverDocExpireDate();
                txtExpDateObs.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentObserverDocExpireDate();
                txtExpDateObs.Text = Date.AddMonths(MonthCount);
                break;
        }
    }
    #endregion
}
