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

public partial class Employee_Document_AddTestCondition : System.Web.UI.Page
{
    DataTable dtTestConditionDetail = null;
    Boolean IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Page Refresh
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

        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["TestConditionDetail"] = null;

            if (Request.QueryString["TCondId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("TestCondition.aspx");
            }


            Session["TestConditionDetail"] = CreateEmptyDataTableTestConditionDetial();
            Session["TestConditionResponsibility"] = CreateEmptyDatatableTestConditionResponsibility();

            TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            btnAddDetail.Enabled = per.CanNew;
            SetKeys();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnAddDetail"] = btnAddDetail.Enabled;
            this.ViewState["btnAddTestConditionResponsibility"] = btnAddTestConditionResponsibility.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnAddDetail"] != null)
            this.btnAddDetail.Enabled = (bool)this.ViewState["btnAddDetail"];
        if (this.ViewState["btnAddTestConditionResponsibility"] != null)
            this.btnAddTestConditionResponsibility.Enabled = (bool)this.ViewState["btnAddTestConditionResponsibility"];

        Load_GridViewTestCondition();
        Load_grdTestConditionResponsibility();
    }
    //****************************************************************Buttons************************************************************************************************
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        ClearForm();
        EnableControls();

        Session["TestConditionDetail"] = CreateEmptyDataTableTestConditionDetial();
        Load_GridViewTestCondition();
        Session["TestConditionResponsibility"] = CreateEmptyDatatableTestConditionResponsibility();
        Load_grdTestConditionResponsibility();

        HiddenFieldTestCondition["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldTestCondition["TCondId"] = "";
        RoundPanelTestCondition.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        EnableControls();
        cmbMajor.Enabled = false;
        HiddenFieldTestCondition["PageMode"] = Utility.EncryptQS("Edit");
        TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestCondition.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        string PageMode = Utility.DecryptQS(HiddenFieldTestCondition["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (((DataTable)Session["TestConditionDetail"]).Rows.Count == 0)
            {
                ShowMessage("زمینه آزمون وارد نشده است");
                return;
            }

            if (PageMode == "New")
            {
                Insert();
            }
            else if (PageMode == "Edit")
            {
                string TCondId = Utility.DecryptQS(HiddenFieldTestCondition["TCondId"].ToString());

                if (string.IsNullOrEmpty(TCondId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(TCondId));
                }

            }
        }
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (String.IsNullOrWhiteSpace(txtTestDate.Text))
        {
            ShowMessage("تاریخ آزمون انتخاب نشده است");
            return;
        }

        if (Session["TestConditionDetail"] != null)
        {
            dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];

            if (dtTestConditionDetail.Select("TTypeId=" + cmbTestType.SelectedItem.Value + " and GrdId=" + cmbGrade.SelectedItem.Value).Length > 0)
            {
                ShowMessage("اطلاعات وارد شده تکراری می باشد");
                return;
            }

            DataRow dr = dtTestConditionDetail.NewRow();

            try
            {
                dr["AcceptGrade"] = txtAcceptGrade.Text;
                dr["MaxDeductionGrade"] = txtMaxDeductionGrade.Text;
                dr["TTypeName"] = cmbTestType.SelectedItem.Text.ToString();
                dr["TTypeId"] = cmbTestType.SelectedItem.Value.ToString();
                dr["GrdName"] = cmbGrade.SelectedItem.Text.ToString();
                dr["GrdId"] = cmbGrade.SelectedItem.Value.ToString();
                dr["TestDate"] = txtTestDate.Text;
                dr["NeedFileUpload"] = checkboxNeedFileUpload.Checked;


                dtTestConditionDetail.Rows.Add(dr);
                GridViewTestCondition.DataSource = dtTestConditionDetail;
                GridViewTestCondition.DataBind();

                txtAcceptGrade.Text = "";
                txtTestDate.Text = "";
                txtMaxDeductionGrade.Text = "";
                cmbTestType.SelectedIndex = 0;
                cmbGrade.SelectedIndex = 0;

            }
            catch (Exception err)
            {
                ShowMessage("خطایی در اضافه کردن رخ داده است");
            }
        }
    }

    //****************************************************************************************************************************************************************
    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbMajor.SelectedIndex > -1)
        {
            cmbMajor.DataBind();
            string SelectedMjId = cmbMajor.SelectedItem.Value.ToString();
            ObjdsTestType.SelectParameters[0].DefaultValue = SelectedMjId;
            cmbTestType.DataBind();
            cmbTestType.SelectedIndex = 0;
            objDocAcceptedGrade.SelectParameters[0].DefaultValue = SelectedMjId;
            cmbAcceptedGrade.DataBind();
            cmbAcceptedGrade.SelectedIndex = 0;            
        }
    }
    //***************************************************GridTestCondition*************************************************************************************************************
    protected void GridViewTestCondition_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewTestCondition.DataSource = (DataTable)Session["TestConditionDetail"];
        GridViewTestCondition.DataBind();

        int Id = -1;
        if (GridViewTestCondition.FocusedRowIndex > -1)
        {
            Id = GridViewTestCondition.FocusedRowIndex;
        }
        if (Id == -1)
        {
            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
            return;

        }
        else
        {

            dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];
            dtTestConditionDetail.Rows.Find(e.Keys["Id"]).Delete();
            Session["TestConditionDetail"] = dtTestConditionDetail;
            GridViewTestCondition.DataSource = (DataTable)Session["TestConditionDetail"];
            GridViewTestCondition.DataBind();
            dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];

        }
    }

    //**********************************************************GridTestConditionResponsibility******************************************************************************************************
    protected void grdTestConditionResponsibility_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        grdTestConditionResponsibility.JSProperties["cpMessage"] = "";
        grdTestConditionResponsibility.JSProperties["cpSaveComplete"] = 0;
        String[] InputData = e.Parameters.Split('$');
        if (Session["TestConditionResponsibility"] != null)
        {
            try
            {
                DataTable dt = (DataTable)Session["TestConditionResponsibility"];

                if (dt.Select("TTypeId=" + InputData[1] + " and GMRId=" + InputData[3]).Length > 0)
                {
                    grdTestConditionResponsibility.JSProperties["cpMessage"] = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }


                DataRow dr = dt.NewRow();
                dr["TTypeId"] = InputData[1];
                dr["TTypeName"] = InputData[2];
                dr["GMRId"] = InputData[3];
                dr["GrdResName"] = InputData[4];
                dt.Rows.Add(dr);
                Load_grdTestConditionResponsibility();
                grdTestConditionResponsibility.JSProperties["cpSaveComplete"] = 1;
            }
            catch (Exception)
            {
                grdTestConditionResponsibility.JSProperties["cpMessage"] = "خطایی در اضافه کردن سایر مشخصات ایجاد گردیده است";
            }
        }
    }

    protected void grdTestConditionResponsibility_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        if (Session["TestConditionResponsibility"] != null)
        {
            try
            {
                DataTable dt = (DataTable)Session["TestConditionResponsibility"];
                dt.Rows.Find(e.Keys["Id"]).Delete();
            }
            catch (Exception) { }
        }
        Load_grdTestConditionResponsibility();
    }
    #endregion

    #region Methods
    #region Set TestConditionDetialGrid
    DataTable CreateEmptyDataTableTestConditionDetial()
    {
        dtTestConditionDetail = new DataTable();
        dtTestConditionDetail.Columns.Add("Id");
        dtTestConditionDetail.Columns["Id"].AutoIncrement = true;
        dtTestConditionDetail.Columns["Id"].AutoIncrementSeed = 1;
        dtTestConditionDetail.Constraints.Add("PK_ID", dtTestConditionDetail.Columns["Id"], true);
        dtTestConditionDetail.Columns.Add("AcceptGrade");
        dtTestConditionDetail.Columns.Add("MaxDeductionGrade");
        dtTestConditionDetail.Columns.Add("TTypeName");
        dtTestConditionDetail.Columns.Add("GrdName");
        dtTestConditionDetail.Columns.Add("TestDate");
        dtTestConditionDetail.Columns.Add("TTypeId");        
        dtTestConditionDetail.Columns.Add("NeedFileUpload");
        dtTestConditionDetail.Columns["TTypeId"].AutoIncrement = true;
        dtTestConditionDetail.Columns["TTypeId"].AutoIncrementSeed = 1;
        dtTestConditionDetail.Columns.Add("GrdId");
        dtTestConditionDetail.Columns["GrdId"].AutoIncrement = true;
        dtTestConditionDetail.Columns["GrdId"].AutoIncrementSeed = 1;
        return dtTestConditionDetail;
    }

    void Load_GridViewTestCondition()
    {
        if (Session["TestConditionDetail"] != null)
        {
            GridViewTestCondition.DataSource = (DataTable)Session["TestConditionDetail"];
            GridViewTestCondition.DataBind();
        }
    }
    #endregion

    #region Set TestConditionResponsibility Grid
    DataTable CreateEmptyDatatableTestConditionResponsibility()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns["Id"].AutoIncrement = true;
        dt.Columns["Id"].AutoIncrementSeed = 1;
        dt.Constraints.Add("PK_ID", dt.Columns["Id"], true);
        dt.Columns.Add("TTypeId");
        dt.Columns.Add("GMRId");
        dt.Columns.Add("TTypeName");
        dt.Columns.Add("GrdResName");
        dt.Columns.Add("TCondRId");

        return dt;
    }



    void Load_grdTestConditionResponsibility()
    {
        if (Session["TestConditionResponsibility"] != null)
        {
            grdTestConditionResponsibility.DataSource = (DataTable)Session["TestConditionResponsibility"];
            grdTestConditionResponsibility.DataBind();
        }
    }
    #endregion

    #region Setmode-Keys
    private void SetKeys()
    {
        HiddenFieldTestCondition["TCondId"] = Request.QueryString["TCondId"].ToString();
        HiddenFieldTestCondition["PageMode"] = Request.QueryString["PgMd"];

        string TCondId = Utility.DecryptQS(HiddenFieldTestCondition["TCondId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldTestCondition["PageMode"].ToString());

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
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

        DisableControls();

        if (HiddenFieldTestCondition["TCondId"] == null || (string.IsNullOrEmpty(HiddenFieldTestCondition["TCondId"].ToString())))
        {
            Response.Redirect("TestCondition.aspx");
            return;
        }
        int TCondId = int.Parse(Utility.DecryptQS(HiddenFieldTestCondition["TCondId"].ToString()));
        FillForm(TCondId);
        cmbGrade.SelectedIndex = 0;
        cmbTestType.SelectedIndex = 0;
        RoundPanelTestCondition.HeaderText = "مشاهده";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnAddDetail"] = btnAddDetail.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        Session["TestConditionDetail"] = CreateEmptyDataTableTestConditionDetial();
        Load_GridViewTestCondition();
        Session["TestConditionResponsibility"] = CreateEmptyDatatableTestConditionResponsibility();
        Load_grdTestConditionResponsibility();

        ClearForm();
        RoundPanelTestCondition.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (HiddenFieldTestCondition["TCondId"] == null || string.IsNullOrEmpty(HiddenFieldTestCondition["TCondId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int TCondId = int.Parse(Utility.DecryptQS(HiddenFieldTestCondition["TCondId"].ToString()));

        EnableControls();
        cmbMajor.Enabled = false;
        FillForm(TCondId);
        cmbGrade.SelectedIndex = 0;
        cmbTestType.SelectedIndex = 0;
        RoundPanelTestCondition.HeaderText = "ویرایش";
    }
    #endregion

    #region setControls
    private void ClearForm()
    {
        txtExamValidDate.Text = "";
        txtAcceptGrade.Text = "";
        txtExpireDate.Text = "";
        txtMaxDeductionGrade.Text = "";
        //txtYear.Text = "";
        txtTitle.Text = "";

        cmbGrade.SelectedIndex = 0;
        cmbMajor.SelectedIndex = -1;
        //cmbMajor_SelectedIndexChanged(this, new EventArgs());
    }

    private void EnableControls()
    {
        txtExamValidDate.Enabled = true;
        txtAcceptGrade.Enabled = true;
        txtExpireDate.Enabled = true;
        txtMaxDeductionGrade.Enabled = true;
        txtTitle.Enabled = true;
        cmbGrade.Enabled = true; PanelTestConditionDetails.Visible = true;
        cmbMajor.Enabled = true;
        cmbTestType.Enabled = true;

        GridViewTestCondition.Enabled = true;
        btnAddDetail.Enabled = true;
        grdTestConditionResponsibility.Enabled = true;
        btnAddTestConditionResponsibility.Enabled = true;
        GridViewTestCondition.Columns["Delete"].Visible = true;
        grdTestConditionResponsibility.Columns["Delete"].Visible = true;

        PanelInputTestConditionResponsibility.Visible = true;
        PanelTestConditionDetails.Visible = true;
        this.ViewState["btnAddDetail"] = btnAddDetail.Enabled;
        this.ViewState["btnAddTestConditionResponsibility"] = btnAddTestConditionResponsibility.Enabled;
    }

    private void DisableControls()
    {
        txtTitle.Enabled = false;
        txtAcceptGrade.Enabled = false;
        txtExpireDate.Enabled = false;
        txtMaxDeductionGrade.Enabled = false;
        cmbGrade.Enabled = false;
        cmbMajor.Enabled = false;
        cmbTestType.Enabled = false;
        GridViewTestCondition.Enabled = false;
        txtExamValidDate.Enabled = false;
        btnAddDetail.Enabled = false;
        GridViewTestCondition.Columns["Delete"].Visible = false;
        grdTestConditionResponsibility.Columns["Delete"].Visible = false;
        PanelInputTestConditionResponsibility.Visible = false;
        PanelTestConditionDetails.Visible = false;
        this.ViewState["btnAddDetail"] = btnAddDetail.Enabled;
        this.ViewState["btnAddTestConditionResponsibility"] = btnAddTestConditionResponsibility.Enabled;
    }
    #endregion

    private void FillForm(int TCondId)
    {
        TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
        DocTestConditionManager.FindByCode(TCondId);
        if (DocTestConditionManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocTestConditionManager[0]["ExpireDate"]))
                txtExpireDate.Text = DocTestConditionManager[0]["ExpireDate"].ToString();
            //if (!Utility.IsDBNullOrNullValue(DocTestConditionManager[0]["Year"]))
            //    txtYear.Text = DocTestConditionManager[0]["Year"].ToString().Trim();
            if (!Utility.IsDBNullOrNullValue(DocTestConditionManager[0]["Title"]))
                txtTitle.Text = DocTestConditionManager[0]["Title"].ToString().Trim();
            cmbMajor.DataBind();
            if (!Utility.IsDBNullOrNullValue(DocTestConditionManager[0]["MjId"]))
            {
                cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(DocTestConditionManager[0]["MjId"].ToString()).Index;
                HiddenFieldTestCondition["SelectedMjId"] = DocTestConditionManager[0]["MjId"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(DocTestConditionManager[0]["TestValidDate"]))
                txtExamValidDate.Text = DocTestConditionManager[0]["TestValidDate"].ToString();
            HiddenFieldTestCondition["Year"] = DocTestConditionManager[0]["Year"].ToString().Trim();

            #region TestConditionDetail
            TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
            DataTable dtDetail = DocTestConditionDetailManager.SelectByTestCondition(TCondId);
            dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                DataRow dr = dtTestConditionDetail.NewRow();
                dr["TTypeId"] = dtDetail.Rows[i]["TTypeId"];
                dr["GrdId"] = dtDetail.Rows[i]["GrdId"].ToString();
                dr["AcceptGrade"] = dtDetail.Rows[i]["AcceptGrade"].ToString();
                dr["MaxDeductionGrade"] = dtDetail.Rows[i]["DeductionGrade"].ToString();
                dr["TTypeName"] = dtDetail.Rows[i]["TTypeName"].ToString();
                dr["GrdName"] = dtDetail.Rows[i]["GrdName"].ToString();
                dr["Id"] = dtDetail.Rows[i]["TCondDId"].ToString();
                dr["TestDate"] = dtDetail.Rows[i]["TestDate"].ToString();
                dr["NeedFileUpload"] = dtDetail.Rows[i]["NeedFileUpload"]; 
                dtTestConditionDetail.Rows.Add(dr);

            }
            dtTestConditionDetail.AcceptChanges();
            GridViewTestCondition.DataSource = dtTestConditionDetail;
            GridViewTestCondition.DataBind();
            Session["TestConditionDetail"] = dtTestConditionDetail;
            #endregion

            #region TestConditionResponsibility
            TSP.DataManager.DocTestConditionResponsibilityManager DocTestConditionResponsibilityManager = new TSP.DataManager.DocTestConditionResponsibilityManager();
            DocTestConditionResponsibilityManager.FindByTestCondition(TCondId);
            DataTable dt = (DataTable)Session["TestConditionResponsibility"];
            for (int i = 0; i < DocTestConditionResponsibilityManager.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TTypeId"] = DocTestConditionResponsibilityManager[i]["TTypeId"];
                dr["GMRId"] = DocTestConditionResponsibilityManager[i]["GMRId"].ToString();
                cmbDocTestType.DataBind();
                dr["TTypeName"] = cmbDocTestType.Items.FindByValue(DocTestConditionResponsibilityManager[i]["TTypeId"].ToString()).Text;
                cmbAcceptedGrade.DataBind();
                dr["GrdResName"] = cmbAcceptedGrade.Items.FindByValue(DocTestConditionResponsibilityManager[i]["GMRId"].ToString()).Text;
                dr["TCondRId"] = DocTestConditionResponsibilityManager[i]["TCondRId"].ToString();
                dt.Rows.Add(dr);

            }
            dt.AcceptChanges();
            grdTestConditionResponsibility.DataSource = dt;
            grdTestConditionResponsibility.DataBind();
            #endregion
            cmbMajor_SelectedIndexChanged(this, new EventArgs());
        }
    }

    #region Insert-Edit
    private void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
        TSP.DataManager.DocTestConditionResponsibilityManager DocTestConditionResponsibilityManager = new TSP.DataManager.DocTestConditionResponsibilityManager();
        TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();

        TransactionManager.Add(DocTestConditionManager);
        TransactionManager.Add(DocTestConditionDetailManager);
        TransactionManager.Add(DocTestConditionResponsibilityManager);

        DataTable dtTestConditionResponsibility = null;

        try
        {
            /*DocTestConditionManager.Fill();
            DocTestConditionManager1.CurrentFilter = "Year= '" + txtYear.Text.Trim() + "'" + " and " + "MjId=" + int.Parse(cmbMajor.SelectedItem.Value.ToString()) + " and " + "InActive=" + "0";
            if (DocTestConditionManager1.Count >= 1)
            {
                ShowMessage("آزمون وارد شده تکراری می باشد.");
                return;
            }*/
            TransactionManager.BeginSave();
            DataRow TestConditionRow = DocTestConditionManager.NewRow();
            //TestConditionRow["Year"] = txtYear.Text.Trim();
            TestConditionRow["Title"] = txtTitle.Text.Trim();
            TestConditionRow["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());
            TestConditionRow["ExpireDate"] = txtExpireDate.Text;
            TestConditionRow["TestValidDate"] = txtExamValidDate.Text;
            TestConditionRow["Description"] = txtDescription.Text;
            TestConditionRow["Inactive"] = 0;
            TestConditionRow["UserId"] = Utility.GetCurrentUser_UserId();
            TestConditionRow["ModifiedDate"] = DateTime.Now;

            DocTestConditionManager.AddRow(TestConditionRow);

            int cn = DocTestConditionManager.Save();
            if (cn > 0)
            {
                int TCondId = Convert.ToInt32(DocTestConditionManager[0]["TCondId"].ToString());

                #region SaveDetail

                dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];

                for (int i = 0; i < dtTestConditionDetail.DefaultView.Count; i++)
                {
                    DataRow TCondDetailRow = DocTestConditionDetailManager.NewRow();
                    TCondDetailRow["TCondId"] = TCondId;

                    TCondDetailRow["TTypeId"] = dtTestConditionDetail.Rows[i]["TTypeId"].ToString();
                    TCondDetailRow["GrdId"] = dtTestConditionDetail.Rows[i]["GrdId"].ToString();
                    TCondDetailRow["AcceptGrade"] = dtTestConditionDetail.Rows[i]["AcceptGrade"].ToString();
                    TCondDetailRow["DeductionGrade"] = dtTestConditionDetail.Rows[i]["MaxDeductionGrade"].ToString();
                    TCondDetailRow["TestDate"] = dtTestConditionDetail.Rows[i]["TestDate"].ToString();
                    TCondDetailRow["NeedFileUpload"] = dtTestConditionDetail.Rows[i]["NeedFileUpload"];
                    TCondDetailRow["UserId"] = Utility.GetCurrentUser_UserId();
                    TCondDetailRow["ModifiedDate"] = DateTime.Now;
                    DocTestConditionDetailManager.AddRow(TCondDetailRow);



                }
                int cnt = DocTestConditionDetailManager.Save();
                DocTestConditionDetailManager.DataTable.AcceptChanges();
                if (cnt <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }

                #endregion

                #region SaveTestConditionResponsibility
                dtTestConditionResponsibility = (DataTable)Session["TestConditionResponsibility"];

                for (int i = 0; i < dtTestConditionResponsibility.Rows.Count; i++)
                {
                    DataRow dr = DocTestConditionResponsibilityManager.NewRow();
                    dr["TCondId"] = TCondId;
                    dr["TTypeId"] = dtTestConditionResponsibility.Rows[i]["TTypeId"].ToString();
                    dr["GMRId"] = dtTestConditionResponsibility.Rows[i]["GMRId"].ToString();
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["ModifiedDate"] = DateTime.Now;
                    DocTestConditionResponsibilityManager.AddRow(dr);
                    if (DocTestConditionResponsibilityManager.Save() > 0)
                    {
                        DocTestConditionResponsibilityManager.DataTable.AcceptChanges();
                        dtTestConditionResponsibility.Rows[i].BeginEdit();
                        dtTestConditionResponsibility.Rows[i]["TCondRId"] = DocTestConditionResponsibilityManager[DocTestConditionResponsibilityManager.Count - 1]["TCondRId"].ToString();
                        dtTestConditionResponsibility.Rows[i].EndEdit();
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        return;
                    }
                }

                #endregion

                TransactionManager.EndSave();
                ShowMessage("ذخیره انجام شد.");

                dtTestConditionResponsibility.AcceptChanges();
                dtTestConditionDetail.AcceptChanges();

                TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                BtnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;

                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = BtnNew.Enabled;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                HiddenFieldTestCondition["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldTestCondition["TCondId"] = Utility.EncryptQS(DocTestConditionManager[0]["TCondId"].ToString());
                RoundPanelTestCondition.HeaderText = "مشاهده";

            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Edit(int TCondId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
        TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
        TSP.DataManager.DocTestConditionManager DocTestConditionManager1 = new TSP.DataManager.DocTestConditionManager();
        TSP.DataManager.DocTestConditionResponsibilityManager DocTestConditionResponsibilityManager = new TSP.DataManager.DocTestConditionResponsibilityManager();
        TransactionManager.Add(DocTestConditionDetailManager);
        TransactionManager.Add(DocTestConditionManager);
        TransactionManager.Add(DocTestConditionResponsibilityManager);

        DataTable dtTestConditionResponsibility = null;
        try
        {
          
            TransactionManager.BeginSave();

            DocTestConditionManager.FindByCode(TCondId);
            if (DocTestConditionManager.Count == 1)
            {
                DocTestConditionManager[0].BeginEdit();
                
                DocTestConditionManager[0]["Title"] = txtTitle.Text.Trim();
                DocTestConditionManager[0]["MjId"] = cmbMajor.SelectedItem.Value.ToString();
                DocTestConditionManager[0]["ExpireDate"] = txtExpireDate.Text;
                DocTestConditionManager[0]["TestValidDate"] = txtExamValidDate.Text;
                DocTestConditionManager[0]["Description"] = txtDescription.Text;
                DocTestConditionManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocTestConditionManager[0]["ModifiedDate"] = DateTime.Now;

                DocTestConditionManager[0].EndEdit();

                int cn = DocTestConditionManager.Save();
                if (cn > 0)
                {
                    #region TestConditionDetail

                    dtTestConditionDetail = (DataTable)Session["TestConditionDetail"];

                    if (dtTestConditionDetail.GetChanges() != null)
                    {

                        DataRow[] delRows = dtTestConditionDetail.Select(null, null, DataViewRowState.Deleted);
                        DataRow[] insRows = dtTestConditionDetail.Select(null, null, DataViewRowState.Added);

                        if (delRows.Length > 0)
                        {
                            for (int i = 0; i < delRows.Length; i++)
                            {
                                DocTestConditionDetailManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                                if (DocTestConditionDetailManager.Count > 0)
                                {
                                    DocTestConditionDetailManager[0].Delete();

                                    DocTestConditionDetailManager.Save();
                                    DocTestConditionDetailManager.DataTable.AcceptChanges();
                                }

                            }
                        }

                        int count = 0;
                        if (insRows.Length > 0)
                        {
                            for (int i = 0; i < insRows.Length; i++)
                            {
                                DataRow drTestCondDetail = DocTestConditionDetailManager.NewRow();
                                drTestCondDetail["TCondId"] = TCondId;
                                drTestCondDetail["TTypeId"] = insRows[i]["TTypeId"].ToString();
                                drTestCondDetail["GrdId"] = insRows[i]["GrdId"].ToString();
                                drTestCondDetail["AcceptGrade"] = insRows[i]["AcceptGrade"].ToString();
                                drTestCondDetail["DeductionGrade"] = insRows[i]["MaxDeductionGrade"].ToString();
                                drTestCondDetail["TestDate"] = insRows[i]["TestDate"].ToString();
                                drTestCondDetail["NeedFileUpload"] = insRows[i]["NeedFileUpload"];
                                drTestCondDetail["UserId"] = Utility.GetCurrentUser_UserId();
                                drTestCondDetail["ModifiedDate"] = DateTime.Now;

                                DocTestConditionDetailManager.AddRow(drTestCondDetail);

                                DocTestConditionDetailManager.Save();

                                DocTestConditionDetailManager.DataTable.AcceptChanges();

                            }
                        }
                    }
                    #endregion

                    #region DocTestConditionResponsibilityManager
                    dtTestConditionResponsibility = (DataTable)Session["TestConditionResponsibility"];
                    DataRow[] drInserted = dtTestConditionResponsibility.Select(null, null, DataViewRowState.Added);
                    DataRow[] drDeleted = dtTestConditionResponsibility.Select(null, null, DataViewRowState.Deleted);

                    for (int i = 0; i < drInserted.Length; i++)
                    {
                        DataRow dr = DocTestConditionResponsibilityManager.NewRow();
                        dr["TCondId"] = TCondId;
                        dr["TTypeId"] = drInserted[i]["TTypeId"].ToString();
                        dr["GMRId"] = drInserted[i]["GMRId"].ToString();
                        dr["UserId"] = Utility.GetCurrentUser_UserId();
                        dr["ModifiedDate"] = DateTime.Now;
                        DocTestConditionResponsibilityManager.AddRow(dr);

                        if (DocTestConditionResponsibilityManager.Save() > 0)
                        {
                            DocTestConditionResponsibilityManager.DataTable.AcceptChanges();
                            drInserted[i].BeginEdit();
                            drInserted[i]["TCondRId"] = DocTestConditionResponsibilityManager[DocTestConditionResponsibilityManager.Count - 1]["TCondRId"].ToString();
                            drInserted[i].EndEdit();
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                            return;
                        }
                    }

                    for (int i = 0; i < drDeleted.Length; i++)
                    {
                        DocTestConditionResponsibilityManager.FindByCode(Convert.ToInt32(drDeleted[i]["TCondRId", DataRowVersion.Original].ToString()));
                        DocTestConditionResponsibilityManager[i].Delete();
                        DocTestConditionResponsibilityManager.Save();
                        DocTestConditionResponsibilityManager.DataTable.AcceptChanges();
                    }
                    #endregion

                    TransactionManager.EndSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));


                    dtTestConditionResponsibility.AcceptChanges();
                    dtTestConditionDetail.AcceptChanges();
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                }
            }
            else
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));

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

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
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

    void ShowMessage(String Message)
    {
        //this.DivReport.Visible = true;
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }
    #endregion
}
