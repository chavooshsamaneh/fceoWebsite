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

public partial class Members_Documents_AddObservationDoc : System.Web.UI.Page
{
    DataTable dtDocCity = null;
    private bool IsPageRefresh = false;

    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


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

                dtDocCity.Columns.Add("CitId");
                dtDocCity.Columns["CitId"].AutoIncrement = true;
                dtDocCity.Columns["CitId"].AutoIncrementSeed = 1;

                Session["DocCity"] = dtDocCity;
            }
            else
                dtDocCity = (DataTable)Session["DocCity"];


            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            btnAddCity.Enabled = true;
            btnSearchCity.Enabled = true;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
            MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
            if (MemberManager.Count == 1)
            {
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
                    else
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            SetKeys();
            this.ViewState["btnAddCity"] = btnAddCity.Enabled;
            this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["btnAddCity"] != null)
            this.btnAddCity.Enabled = (bool)this.ViewState["btnAddCity"];
        if (this.ViewState["btnSearchCity"] != null)
            this.btnSearchCity.Enabled = (bool)this.ViewState["btnSearchCity"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnSearchCity.Enabled = true;
        btnAddCity.Enabled = true;
        GridViewCity.Columns[0].Visible = true;
        HiddenFieldDocMemberFile["MFId"] = "";
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("New");
        RoundPanelImplement.HeaderText = "جدید";
        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        this.ViewState["btnAddCity"] = btnAddCity.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        if (!CheckPermitionForEdit(MFId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "با توجه به سطوح دسترسی در جریان کار، شما قادر به ویرایش اطلاعات نمی باشید.";
            return;
        }
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnSearchCity.Enabled = true;
        btnAddCity.Enabled = true;
        GridViewCity.Columns[0].Visible = true;
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
        RoundPanelImplement.HeaderText = "ویرایش";
        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        this.ViewState["btnAddCity"] = btnAddCity.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());

        if (string.IsNullOrEmpty(MFId) && PageMode != "New")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (PageMode == "New")
        {
            InsertDocMemberFileObs();
        }
        else if (PageMode == "Edit")
        {
            Edit(int.Parse(MFId));
        }
        else if (PageMode == "Revival")
        {
            Revival(int.Parse(MFId));
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("ObservationDoc.aspx?PostId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("ObservationDoc.aspx");
        }
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        if (Session["DocCity"] != null)
        {
            dtDocCity = (DataTable)Session["DocCity"];
            if (dtDocCity.Rows.Count == 2)
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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

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
            //  dtDocCity.Rows[Id].Delete();
            Session["DocCity"] = dtDocCity;
            GridViewCity.DataSource = (DataTable)Session["DocCity"];
            GridViewCity.DataBind();
            dtDocCity = (DataTable)Session["DocCity"];
        }
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetObsDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
        HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];

        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
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
                SetRevivalModeKeys();
                lblWorkFlowState.Visible = false;
                break;
        }
    }

    private void SetNewModeKeys()
    {

        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        tblCity.Visible = true;
        txtCity.Text = "";
        lblWorkFlowState.Visible = false;
        txtLastRegDateObs.Enabled = true;
        txtExpDateObs.Enabled = true;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0);
        if (dtMeFile.Rows.Count > 0)
        {
            lblMFNo.Text = dtMeFile.Rows[0]["MFNo"].ToString();
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation);
                if (dtMeDetail.Rows.Count > 0)
                {
                    txtGradeObs.Text = dtMeDetail.Rows[0]["GrdName"].ToString();
                }
            }
        }
        cmbIsTemporary.SelectedIndex = 0;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetViewModeKeys()
    {
        tblCity.Visible = false;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        btnAddCity.Enabled = false;
        btnSearchCity.Enabled = false;
        GridViewCity.Columns[0].Visible = false;
        txtLastRegDateObs.Enabled = false;
        txtExpDateObs.Enabled = false;
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("ObservationDoc.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);
        RoundPanelImplement.HeaderText = "مشاهده";

        //if (!CheckPermitionForEdit(MFId))
        //{
        //    btnEdit.Enabled = false;
        //    btnEdit2.Enabled = false;
        //}
        this.ViewState["btnSearchCity"] = btnSearchCity.Enabled;
        this.ViewState["btnAddCity"] = btnAddCity.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetEditModeKeys()
    {
        tblCity.Visible = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        BtnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        txtLastRegDateObs.Enabled = true;
        txtExpDateObs.Enabled = true;
        GridViewCity.Columns[0].Visible = true;
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("ObservationDoc.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);
        RoundPanelImplement.HeaderText = "ویرایش";

        //if (!CheckPermitionForEdit(MFId))
        //{
        //    btnEdit.Enabled = false;
        //    btnEdit2.Enabled = false;
        //}

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetRevivalModeKeys()
    {
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));

        cmbIsTemporary.SelectedIndex = 0;
        SetObsDocDefualtExpireDate(Convert.ToInt32(cmbIsTemporary.Value));

        txtLastRegDateObs.Enabled = true;
        txtExpDateObs.Enabled = true;

        tblCity.Visible = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        BtnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        GridViewCity.Columns[0].Visible = true;
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("ObservationDoc.aspx");
            return;
        }
        FillForm(MFId);
        RoundPanelImplement.HeaderText = "درخواست تمدید";
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void FillForm(int MFId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileManager.ClearBeforeFill = true;

        DocMemberFileManager.FindByCode(MFId, 2);
        if (DocMemberFileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                txtExpDateObs.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                txtLastRegDateObs.Text = DocMemberFileManager[0]["RegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                txtMfNoObs.Text = DocMemberFileManager[0]["MFNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                txtSerialNoObs.Text = DocMemberFileManager[0]["SerialNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
                cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());


            #region City

            TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
            DataTable dtImpCity = DocImpDocCityManager.FindMfId(MFId);
            dtDocCity = (DataTable)Session["DocCity"];
            for (int i = 0; i < dtImpCity.Rows.Count; i++)
            {
                DataRow dr = dtDocCity.NewRow();
                dr["CitId"] = dtImpCity.Rows[i]["CitId"];
                dr["CitCode"] = dtImpCity.Rows[i]["CitCode"].ToString();
                dr["CitName"] = dtImpCity.Rows[i]["CitName"].ToString();
                dtDocCity.Rows.Add(dr);
            }
            dtDocCity.AcceptChanges();
            GridViewCity.DataSource = dtDocCity;
            GridViewCity.DataBind();
            #endregion

            int MemeberFileId = (int)DocMemberFileManager[0]["MeId"];
            DataTable dtDocMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemeberFileId, Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            if (dtDocMeDetail.Rows.Count > 0)
            {
                // DataTable dtMFMajor = DocMemberFileMajorManager.SelectMemberFileById(MemeberFileId, Utility.GetCurrentUser_MeId(), 0, 1);
                //  if (dtMFMajor.Rows.Count > 0)
                //  {
                //      int MasterMjId = (int)dtMFMajor.Rows[0]["FMjId"];
                //  if (dtDocMeDetail.Rows[0]["MjId"].ToString() == dtMFMajor.Rows[0]["FMjId"].ToString())
                //   {
                txtGradeObs.Text = "";
                for (int i = 0; i < dtDocMeDetail.Rows.Count; i++)
                {
                    txtGradeObs.Text += "رشته " + dtDocMeDetail.Rows[i]["MjName"].ToString() + ":" + dtDocMeDetail.Rows[i]["GrdName"].ToString() + " ; ";
                }
                // }
                // }
            }

            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
            {
                ProvinceManager.FindByCode(int.Parse(DocMemberFileManager[0]["PrId"].ToString()));
                if (ProvinceManager.Count > 0)
                {
                    txtProvinceNameObs.Text = ProvinceManager[0]["PrName"].ToString();
                }
            }

            DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(Utility.GetCurrentUser_MeId(), MFId);
            if (dtObsDoc.Rows.Count > 0)
            {
                txtRegDateObs.Text = dtObsDoc.Rows[0]["RegDate"].ToString();
            }

            DocMemberFileManager.FindByCode(MemeberFileId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
            }

        }
    }

    private void InsertDocMemberFileObs()
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocImpDocCityManager);
        TransactionManager.Add(LetterRelatedTablesManager);

        try
        {
            Boolean IsTransfer = false;
            string PreMFNo = "";
            string PrCode = "";
            PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();
            if (string.IsNullOrEmpty(PrCode))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("کد نظام مهندسی استان جاری مشخص نمی باشد");
                return;
            }

            int MeId = Utility.GetCurrentUser_MeId();
            if (!CheckConditions()) return;

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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("پیش از این برای شما مجوز نظارت تعریف شده است.");
                SetEditModeKeys();
                return;
            }

            DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (dtImpDoc.Rows.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("پیش از این برای شما مجوز اجرا تعریف شده است.");
                return;
            }


            TransactionManager.BeginSave();

            DataRow MemberFileRow = DocMemberFileManager.NewRow();
            MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ObservationDocument);

            MemberFileRow["MeId"] = Utility.DecryptQS(HiddenFieldDocMemberFile["LastMemberFileId"].ToString());//****پروانه اشتغال به کار عضو Id
            MemberFileRow["DocType"] = 2;//****مجوز ناظر حقیقی
            if (!Utility.IsDBNullOrNullValue(txtSerialNoObs.Text))
                MemberFileRow["SerialNo"] = txtSerialNoObs.Text;
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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                #endregion

                #region WorkFlow
                int TaskId = -1;
                int SaveInfoTaskId = -1;
                int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
                int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
                WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                DataRow WFStateRow = WorkFlowStateManager.NewRow();
                int NmcId = Utility.GetCurrentUser_MeId();
                if (NmcId > 0)
                {

                    WFStateRow["TaskId"] = SaveInfoTaskId;
                    WFStateRow["TableId"] = DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"];
                    WFStateRow["NmcIdType"] = Utility.GetCurrentUser_NmcIdType();
                    WFStateRow["NmcId"] = NmcId;
                    WFStateRow["SubOrder"] = 1;
                    WFStateRow["StateType"] = 0;
                    WFStateRow["Description"] = "شروع جریان کار صدور مجوز ناظر حقیقی";
                    WFStateRow["Date"] = Utility.GetDateOfToday();
                    WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                    WFStateRow["ModifiedDate"] = DateTime.Now;
                    WorkFlowStateManager.AddRow(WFStateRow);
                    int count = WorkFlowStateManager.Save();
                    if (count <= 0)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                        return;
                    }

                    HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                    HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(TableId.ToString());
                    RoundPanelImplement.HeaderText = "ویرایش";
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
                if (!Utility.IsDBNullOrNullValue(txtSerialNoObs.Text))
                    DocMemberFileManager[0]["SerialNo"] = txtSerialNoObs.Text;
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
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = (Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                                        return;
                                    }
                                    DocImpDocCityManager[0].Delete();

                                    int SaveDel = DocImpDocCityManager.Save();
                                    DocImpDocCityManager.DataTable.AcceptChanges();
                                    if (SaveDel < 0)
                                    {
                                        TransactionManager.CancelSave();
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = (Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
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
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = (Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
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

    private void Revival(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        DocMemberFileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);

        try
        {
            DocMemberFileManager.SelectObservationDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                int MemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectObservationDocAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmObservationDocAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {
                        DocMemberFileManager.SelectObservationDoc(-1, MfId);
                        if (DocMemberFileManager.Count == 1)
                        {
                            if (Convert.ToBoolean(int.Parse(DocMemberFileManager[0]["IsConfirm"].ToString())) == true)
                            {
                                string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                                Utility.Date objDate = new Utility.Date(CrtEndDate);
                                string LastMonth = objDate.AddMonths(-1);
                                string Today = Utility.GetDateOfToday();
                                int IsDocExp = string.Compare(Today, LastMonth);
                                if (IsDocExp > 0)
                                {
                                    TransactionManager.BeginSave();

                                    DataRow MeFileRow = DocMemberFileManager.NewRow();
                                    MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ObservationDocument);
                                    MeFileRow["MeId"] = MemberFileId;
                                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFSerialNo"]))
                                        MeFileRow["MFSerialNo"] = DocMemberFileManager[0]["MFSerialNo"].ToString();
                                    MeFileRow["RegDate"] = txtLastRegDateObs.Text.Trim();
                                    MeFileRow["ExpireDate"] = txtExpDateObs.Text.Trim();
                                    MeFileRow["SerialNo"] = txtSerialNoObs.Text.Trim();
                                    MeFileRow["DocType"] = 2;
                                    MeFileRow["Type"] = 1;
                                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                                        MeFileRow["PrId"] = DocMemberFileManager[0]["PrId"].ToString();
                                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                                        MeFileRow["MFNo"] = DocMemberFileManager[0]["MFNo"].ToString();
                                    MeFileRow["IsConfirm"] = 0;
                                    MeFileRow["InActive"] = 0;
                                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["Description"]))
                                        MeFileRow["Description"] = DocMemberFileManager[0]["Description"].ToString();
                                    MeFileRow["CreateDate"] = Utility.GetDateOfToday();
                                    MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
                                    MeFileRow["ModifiedDate"] = DateTime.Now;

                                    DocMemberFileManager.AddRow(MeFileRow);
                                    int cn = DocMemberFileManager.Save();
                                    if (cn > 0)
                                    {
                                        #region WorkFlow
                                        int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
                                        WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                                        if (WorkFlowTaskManager.Count > 0)
                                        {
                                            SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                                        }

                                        DataRow WFStateRow = WorkFlowStateManager.NewRow();
                                        int NmcId = Utility.GetCurrentUser_MeId();
                                        if (NmcId > 0)
                                        {
                                            WFStateRow["TaskId"] = SaveInfoTaskId;
                                            WFStateRow["TableId"] = DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"];
                                            WFStateRow["NmcIdType"] = Utility.GetCurrentUser_NmcIdType();
                                            WFStateRow["NmcId"] = NmcId;
                                            WFStateRow["SubOrder"] = 1;
                                            WFStateRow["StateType"] = 0;
                                            WFStateRow["Description"] = "شروع جریان کار تمدید مجوز ناظر حقیقی توسط عضو حقیقی";
                                            WFStateRow["Date"] = Utility.GetDateOfToday();
                                            WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                            WFStateRow["ModifiedDate"] = DateTime.Now;
                                            WorkFlowStateManager.AddRow(WFStateRow);
                                            int count = WorkFlowStateManager.Save();
                                            if (count <= 0)
                                            {
                                                TransactionManager.CancelSave();
                                                this.DivReport.Visible = true;
                                                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                                return;
                                            }

                                            HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                                            HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(TableId.ToString());
                                            RoundPanelImplement.HeaderText = "ویرایش";
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
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    DivReport.Visible = true;
                                    LabelWarning.Text = "تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.";
                                }
                            }
                            else
                            {
                                DivReport.Visible = true;
                                LabelWarning.Text = "امکان تمدید برای پروانه تایید نشده وجود ندارد.";
                            }
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                        }
                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست تمدید وجود ندارد.";
                    }

                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
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
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
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
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == 1)
                                {
                                    if (FirstNmcId == Utility.GetCurrentUser_MeId())
                                    {
                                        return true;
                                    }
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
        else
        {
            return false;
        }

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

    bool CheckConditions()
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        int MeId = Utility.GetCurrentUser_MeId();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
            if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                return false;
            }
            #region CheckAccounting
            if (Utility.CreateAccount())
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AccId"]))
                {
                    int AccId = int.Parse(MemberManager[0]["AccId"].ToString());
                    decimal Balance = AccManager.GetAccountBalance(AccId, Utility.GetDateOfToday());
                    if (Balance != 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("مانده حساب شما صفر نمی باشد.");
                        return false;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("حساب شما نامشخص می باشد.");
                    return false;
                }
            }
            #endregion

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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("امکان ثبت مجوز نظارت وجود ندارد.ابتدا باید پروانه اشتغال به کار تایید شده داشته باشید.");
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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("بدلیل نداشتن هیچ یک از صلاحیت های نظارت و نقشه برداری امکان درخواست مجوز نظارت برای شما وجود ندارد.");
                return false;
            }

            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            if (!string.IsNullOrEmpty(ExpireDate))
            {
                if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("بدلیل اتمام مدت اعتبار پروانه اشتغال امکان درخواست مجوز نظارت برای شما وجود ندارد.");
                    return false;
                }
            }

            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                if (DocMemberFileMajorManager.Count <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("رشته موضوع پروانه شما نامشخص است");
                    return false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("امکان ثبت مجوز نظارت وجود ندارد.وضعیت پروانه اشتغال شما نامشخص می باشد.");
                return false;
            }
            #endregion
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("کد عضویت وارد شده معتبر نمی باشد.");
            return false;
        }
        return true;
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
