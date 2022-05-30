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

public partial class Employee_Document_AddUpGradePoint : System.Web.UI.Page
{
    DataTable dtCourse = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            this.DivReport.Visible = true;
            if (string.IsNullOrEmpty(Request.QueryString["UpGrdPId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
            {
                Response.Redirect("UpGradePoint.aspx");
                return;
            }

            Session["Course"] = null;

            if (Session["Course"] == null)
            {
                dtCourse = new DataTable();
                dtCourse.Columns.Add("Id");
                dtCourse.Columns["Id"].AutoIncrement = true;
                dtCourse.Columns["Id"].AutoIncrementSeed = 1;
                dtCourse.Constraints.Add("PK_ID", dtCourse.Columns["Id"], true);
                dtCourse.Columns.Add("CrsName");
                dtCourse.Columns.Add("Type");
                dtCourse.Columns.Add("TypeName");
                dtCourse.Columns.Add("Description");
                dtCourse.Columns.Add("TrGrId");
                dtCourse.Columns.Add("PkId");
                dtCourse.Columns["PkId"].AutoIncrement = true;
                dtCourse.Columns["PkId"].AutoIncrementSeed = 1;
                Session["Course"] = dtCourse;
            }
            else
                dtCourse = (DataTable)Session["Course"];

            GridViewCourse.DataSource = dtCourse;
            GridViewCourse.DataBind();


            TSP.DataManager.Permission per = TSP.DataManager.DocUpGradePointManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;

            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKeys();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnAdd"] = btnAdd.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        if (this.ViewState["BtnAdd"] != null)
            this.btnAdd.Enabled = (bool)this.ViewState["BtnAdd"];

        if (Session["Course"] != null)
        {

            dtCourse = (DataTable)Session["Course"];
            GridViewCourse.DataSource = dtCourse;
            GridViewCourse.DataBind();
        }
    }

    protected void cmbIsTestNeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbIsTestNeed.SelectedIndex > -1)
        {
            if (Convert.ToInt16(cmbIsTestNeed.Value) == 1)//ندارد
              //  if (cmbIsTestNeed.SelectedIndex == 0)
            {
                txtMinPointSeminar.Visible = false;
                txtMinPointSeminar.ValidationSettings.RequiredField.IsRequired = false;
                txtMinPointPeriod.Visible = false;
                txtMinPointPeriod.ValidationSettings.RequiredField.IsRequired = false;
                txtTotalPoint.Visible = false;
                txtTotalPoint.ValidationSettings.RequiredField.IsRequired = false;
                txtMinPeriodNeed.Visible = false;
                txtMinPeriodNeed.ValidationSettings.RequiredField.IsRequired = false;

                lblMinPeriodNeed.Visible = false;
                lblMinPointPeriod.Visible = false;
                lblMinPointSeminar.Visible = false;
                lblTotalPoint.Visible = false;
            }
            if (Convert.ToInt16(cmbIsTestNeed.Value) == 0)//ندارد
             //   if (cmbIsTestNeed.SelectedIndex == 1)
            {
                txtMinPointSeminar.Visible = true;
                txtMinPointSeminar.ValidationSettings.RequiredField.IsRequired = true;
                txtMinPointPeriod.Visible = true;
                txtMinPointPeriod.ValidationSettings.RequiredField.IsRequired = true;
                txtTotalPoint.Visible = true;
                txtTotalPoint.ValidationSettings.RequiredField.IsRequired = true;
                txtMinPeriodNeed.Visible = true;
                txtMinPeriodNeed.ValidationSettings.RequiredField.IsRequired = true;

                lblMinPeriodNeed.Visible = true;
                lblMinPointPeriod.Visible = true;
                lblMinPointSeminar.Visible = true;
                lblTotalPoint.Visible = true;
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        dtCourse = (DataTable)Session["Course"];
        dtCourse.Rows.Clear();
        Session["Course"] = dtCourse;
        GridViewCourse.DataSource = dtCourse;
        GridViewCourse.DataBind();

        HiddenFieldUpGradePoint["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldUpGradePoint["UpGrdPId"] = "";
        ClearForm();
        EnableControls();
        GridViewCourse.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnAdd.Enabled = true;
        this.ViewState["BtnAdd"] = btnAdd.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldUpGradePoint["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertUpGradePoint();
            }
            else if (PageMode == "Edit")
            {
                string UpGrdPId = Utility.DecryptQS(HiddenFieldUpGradePoint["UpGrdPId"].ToString());

                if (string.IsNullOrEmpty(UpGrdPId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditUpGradePoint(int.Parse(UpGrdPId));
                }

            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpGradePoint.aspx");
    }

    protected void CmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbMajor.SelectedIndex > -1 && cmbUpGrade.SelectedIndex > -1)
        {
            TSP.DataManager.DocAcceptedUpGradeManager DocAcceptedUpGradeManager = new TSP.DataManager.DocAcceptedUpGradeManager();
            CmbMajor.DataBind();
            cmbUpGrade.DataBind();
            int UpGrdId = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            string MjId = CmbMajor.SelectedItem.Value.ToString();
            DocAcceptedUpGradeManager.FindByCode(UpGrdId);
            if (DocAcceptedUpGradeManager.Count == 1)
            {
                cmbResponsibility.Text = "";
                string GrdId = DocAcceptedUpGradeManager[0]["OriginGradeId"].ToString();
                ObjdsAcceptGrade.SelectParameters["MjParentId"].DefaultValue = MjId;
                ObjdsAcceptGrade.SelectParameters["GrdId"].DefaultValue = GrdId;
                cmbResponsibility.DataBind();
                cmbResponsibility.SelectedIndex = 0;
            }

        }
    }

    protected void cmbUpGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbMajor.SelectedIndex > -1 && cmbUpGrade.SelectedIndex > -1)
        {
            TSP.DataManager.DocAcceptedUpGradeManager DocAcceptedUpGradeManager = new TSP.DataManager.DocAcceptedUpGradeManager();
            CmbMajor.DataBind();
            cmbUpGrade.DataBind();
            int UpGrdId = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            string MjId = CmbMajor.SelectedItem.Value.ToString();
            DocAcceptedUpGradeManager.FindByCode(UpGrdId);
            if (DocAcceptedUpGradeManager.Count == 1)
            {
                cmbResponsibility.Text = "";
                string GrdId = DocAcceptedUpGradeManager[0]["OriginGradeId"].ToString();
                ObjdsAcceptGrade.SelectParameters[0].DefaultValue = MjId;
                ObjdsAcceptGrade.SelectParameters[1].DefaultValue = GrdId;
                cmbResponsibility.DataBind();
                cmbResponsibility.SelectedIndex = 0;
            }

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["Course"] != null)
        {
            dtCourse = (DataTable)Session["Course"];

            DataRow dr = dtCourse.NewRow();

            try
            {
                if (CmbCourse.SelectedItem != null)
                {
                    dr["PkId"] = CmbCourse.SelectedItem.Value;
                    dr["CrsName"] = CmbCourse.SelectedItem.Text;
                }
                dr["Type"] = 0;
                dr["TypeName"] = "دوره";
                dr["Description"] = txtCourseDescription.Text;

                dtCourse.Rows.Add(dr);
                GridViewCourse.DataSource = dtCourse;
                GridViewCourse.DataBind();

                txtCourseDescription.Text = "";
                CmbCourse.SelectedIndex = 0;

            }
            catch (Exception err)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void GridViewCourse_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //GridViewCourse.DataSource = (DataTable)Session["Course"];
        //GridViewCourse.DataBind();

        //int Id = -1;
        //if (GridViewCourse.FocusedRowIndex > -1)
        //{
        //    Id = GridViewCourse.FocusedRowIndex;
        //}
        //if (Id == -1)
        //{
        //    e.RowError = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
        //    return;
        //}
        //else
        //{
        //    dtCourse = (DataTable)Session["Course"];
        //    DataRow CourseRow = dtCourse.Rows.Find(e.Keys["Id"]);
        //    if (CourseRow["Type"].ToString() == "1")
        //    {
        //        e.RowError = "امکان حذف سمینار وجود ندارد.";
        //        return;
        //    }
        //}
    }

    protected void GridViewCourse_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewCourse.DataSource = (DataTable)Session["Course"];
        GridViewCourse.DataBind();

        int Id = -1;
        if (GridViewCourse.FocusedRowIndex > -1)
        {
            Id = GridViewCourse.FocusedRowIndex;
        }
        if (Id == -1)
        {
            GridViewCourse.JSProperties["cpErrorShow"] = "true";
            GridViewCourse.JSProperties["cpErrorMsg"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtCourse = (DataTable)Session["Course"];
            DataRow CourseRow = dtCourse.Rows.Find(e.Keys["Id"]);
            if (CourseRow["Type"].ToString() == "0")
            {
                dtCourse.Rows.Find(e.Keys["Id"]).Delete();
                Session["Course"] = dtCourse;
                GridViewCourse.DataSource = (DataTable)Session["Course"];
                GridViewCourse.DataBind();
                dtCourse = (DataTable)Session["Course"];
            }
            else
            {
                //this.DivReport.Attributes.Add("Style", "display:block");
                //this.LabelWarning.Text = "امکان حذف سمینار وجود ندارد.";
                GridViewCourse.JSProperties["cpErrorShow"] = "true";
                GridViewCourse.JSProperties["cpErrorMsg"] = "امکان حذف سمینار وجود ندارد.";
                return;
            }

        }
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldUpGradePoint["UpGrdPId"] = Request.QueryString["UpGrdPId"].ToString();
        HiddenFieldUpGradePoint["PageMode"] = Request.QueryString["PgMd"];

        string UpGrdPId = Utility.DecryptQS(HiddenFieldUpGradePoint["UpGrdPId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldUpGradePoint["PageMode"].ToString());

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
        TSP.DataManager.Permission per = TSP.DataManager.DocUpGradePointManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }


        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        DisableControls();
        GridViewCourse.Enabled = false;
        btnAdd.Enabled = false;

        if (HiddenFieldUpGradePoint["UpGrdPId"] == null || (string.IsNullOrEmpty(HiddenFieldUpGradePoint["UpGrdPId"].ToString())))
        {
            Response.Redirect("UpGradePoint.aspx");
            return;
        }
        int UpGrdPId = int.Parse(Utility.DecryptQS(HiddenFieldUpGradePoint["UpGrdPId"].ToString()));
        FillForm(UpGrdPId);
        RoundPanelUpGradePoint.HeaderText = "مشاهده";
        this.ViewState["BtnAdd"] = btnAdd.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();

        RoundPanelUpGradePoint.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocUpGradePointManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable

        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;


        if (HiddenFieldUpGradePoint["UpGrdPId"] == null || string.IsNullOrEmpty(HiddenFieldUpGradePoint["UpGrdPId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int UpGrdPId = int.Parse(Utility.DecryptQS(HiddenFieldUpGradePoint["UpGrdPId"].ToString()));

        EnableControls();

        FillForm(UpGrdPId);

        RoundPanelUpGradePoint.HeaderText = "ویرایش";
    }

    private void FillForm(int UpGrdPId)
    {
        TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
        DocUpGradePointManager.FindByCode(UpGrdPId);
        if (DocUpGradePointManager.Count == 1)
        {
            txtDescription.Text = DocUpGradePointManager[0]["Description"].ToString();
            txtJobDuration.Text = DocUpGradePointManager[0]["JobDuration"].ToString();
            txtMinPeriodNeed.Text = DocUpGradePointManager[0]["MinPeriodNeed"].ToString();
            txtMinPointPeriod.Text = DocUpGradePointManager[0]["MinPointPeriod"].ToString();
            txtMinPointSeminar.Text = DocUpGradePointManager[0]["MinPointSeminar"].ToString();
            txtTotalPoint.Text = DocUpGradePointManager[0]["TotalPoint"].ToString();
            txtTotalPointNeed.Text = DocUpGradePointManager[0]["TotalPointNeed"].ToString();
            cmbResponsibility.DataBind();

            cmbResponsibility.SelectedIndex = cmbResponsibility.Items.FindByValue(DocUpGradePointManager[0]["ResId"].ToString()).Index;
            CmbMajor.DataBind();
            CmbMajor.SelectedIndex = CmbMajor.Items.FindByValue(DocUpGradePointManager[0]["MjParentId"].ToString()).Index;
            cmbUpGrade.DataBind();
            cmbUpGrade.SelectedIndex = cmbUpGrade.Items.FindByValue(DocUpGradePointManager[0]["UpGrdId"].ToString()).Index;
            CmbStatus.SelectedIndex = CmbStatus.Items.FindByValue(DocUpGradePointManager[0]["Status"].ToString()).Index;
            cmbIsTestNeed.SelectedIndex = cmbIsTestNeed.Items.FindByValue(Convert.ToInt16( DocUpGradePointManager[0]["IsTestNeed"]).ToString()).Index;// Convert.ToInt32(DocUpGradePointManager[0]["IsTestNeed"]);
            cmbIsTestNeed_SelectedIndexChanged(this, new EventArgs());

            #region Course

            TSP.DataManager.TrainingAcceptedGradeManager TrainingAcceptedGradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();
            DataTable dtUpgradeCourse = TrainingAcceptedGradeManager.FindByUpGrdId(UpGrdPId);
            dtCourse = (DataTable)Session["Course"];
            for (int i = 0; i < dtUpgradeCourse.Rows.Count; i++)
            {
                DataRow dr = dtCourse.NewRow();
                dr["PkId"] = dtUpgradeCourse.Rows[i]["PkId"];
                dr["CrsName"] = dtUpgradeCourse.Rows[i]["CrsName"].ToString();
                dr["Type"] = dtUpgradeCourse.Rows[i]["Type"].ToString();
                dr["TypeName"] = dtUpgradeCourse.Rows[i]["TypeName"].ToString();
                dr["Description"] = dtUpgradeCourse.Rows[i]["Description"].ToString();
                dr["TrGrId"] = dtUpgradeCourse.Rows[i]["TrGrId"].ToString();
                dtCourse.Rows.Add(dr);

            }
            dtCourse.AcceptChanges();
            GridViewCourse.DataSource = dtCourse;
            GridViewCourse.DataBind();
            #endregion
        }
    }

    private void ClearForm()
    {
        txtJobDuration.Text = "";
        txtMinPeriodNeed.Text = "";
        txtMinPointPeriod.Text = "";
        txtMinPointSeminar.Text = "";
        txtTotalPoint.Text = "";
        txtTotalPointNeed.Text = "";
        txtDescription.Text = "";

        cmbIsTestNeed.SelectedIndex = 0;
        cmbIsTestNeed_SelectedIndexChanged(this, new EventArgs());
        cmbResponsibility.SelectedIndex = 0;
        CmbStatus.SelectedIndex = 0;
        cmbUpGrade.SelectedIndex = 0;
        CmbMajor.SelectedIndex = 0;
        CmbMajor_SelectedIndexChanged(this, new EventArgs());
        CmbCourse.SelectedIndex = 0;
    }

    private void EnableControls()
    {
        txtJobDuration.Enabled = true;
        txtMinPeriodNeed.Enabled = true;
        txtMinPointPeriod.Enabled = true;
        txtMinPointSeminar.Enabled = true;
        txtTotalPoint.Enabled = true;
        txtTotalPointNeed.Enabled = true;
        txtDescription.Enabled = true;

        cmbResponsibility.Enabled = true;
        cmbUpGrade.Enabled = true;
        CmbStatus.Enabled = true;
        CmbMajor.Enabled = true;
        cmbIsTestNeed.Enabled = true;
        cmbIsTestNeed_SelectedIndexChanged(this, new EventArgs());

        txtCourseDescription.Enabled = true;
        CmbCourse.Enabled = true;

    }

    private void DisableControls()
    {
        txtJobDuration.Enabled = false;
        txtMinPeriodNeed.Enabled = false;
        txtMinPointPeriod.Enabled = false;
        txtMinPointSeminar.Enabled = false;
        txtTotalPoint.Enabled = false;
        txtTotalPointNeed.Enabled = false;
        txtDescription.Enabled = false;

        cmbResponsibility.Enabled = false;
        cmbUpGrade.Enabled = false;
        CmbStatus.Enabled = false;
        CmbMajor.Enabled = false;
        cmbIsTestNeed.Enabled = false;
        cmbIsTestNeed_SelectedIndexChanged(this, new EventArgs());

        txtCourseDescription.Enabled = false;
        CmbCourse.Enabled = false;

    }

    private void InsertUpGradePoint()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
        TSP.DataManager.TrainingAcceptedGradeManager TrainingAcceptedGradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();
        TransactionManager.Add(DocUpGradePointManager);
        TransactionManager.Add(TrainingAcceptedGradeManager);

        try
        {
            TransactionManager.BeginSave();
            DataRow UpGrdPointRow = DocUpGradePointManager.NewRow();

            UpGrdPointRow["UpGrdId"] = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            //UpGrdPointRow["GMRId"] = int.Parse(cmbResponsibility.SelectedItem.Value.ToString());
            UpGrdPointRow["ResId"] = int.Parse(cmbResponsibility.SelectedItem.Value.ToString());

            UpGrdPointRow["MjParentId"] = int.Parse(CmbMajor.SelectedItem.Value.ToString());
            if (Convert.ToInt16( cmbIsTestNeed.Value) == 1)
            {
                UpGrdPointRow["IsTestNeed"] = 1;
                UpGrdPointRow["MinPointPeriod"] = 0;
                UpGrdPointRow["MinPointSeminar"] = 0;
                UpGrdPointRow["TotalPoint"] = 0;
                UpGrdPointRow["MinPeriodNeed"] = 0;
            }
            if (Convert.ToInt16(cmbIsTestNeed.Value) == 0)//ندارد
            {
                UpGrdPointRow["IsTestNeed"] = 0;
                UpGrdPointRow["MinPointPeriod"] = txtMinPointPeriod.Text;
                UpGrdPointRow["MinPointSeminar"] = txtMinPointSeminar.Text;
                UpGrdPointRow["TotalPoint"] = txtTotalPoint.Text;
                UpGrdPointRow["MinPeriodNeed"] = txtMinPeriodNeed.Text;
            }

            UpGrdPointRow["Status"] = CmbStatus.Value;
            UpGrdPointRow["JobDuration"] = txtJobDuration.Text;
            UpGrdPointRow["TotalPointNeed"] = txtTotalPointNeed.Text;
            UpGrdPointRow["Date"] = Utility.GetDateOfToday();
            UpGrdPointRow["Time"] = Utility.GetCurrentTime();
            UpGrdPointRow["InActive"] = 0;
            UpGrdPointRow["Description"] = txtDescription.Text;
            UpGrdPointRow["UserId"] = Utility.GetCurrentUser_UserId();
            UpGrdPointRow["ModifiedDate"] = DateTime.Now;

            DocUpGradePointManager.AddRow(UpGrdPointRow);
            int cn = DocUpGradePointManager.Save();
            if (cn <= 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            #region SaveCourse
            if (Session["Course"] != null)
            {
                dtCourse = (DataTable)Session["Course"];

                if (dtCourse.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtCourse.DefaultView.Count; i++)
                    {
                        DataRow CourseRow = TrainingAcceptedGradeManager.NewRow();

                        CourseRow["UpGrdPId"] = DocUpGradePointManager[0]["UpGrdPId"].ToString();

                        CourseRow["PkId"] = dtCourse.Rows[i]["PkId"].ToString();
                        CourseRow["Type"] = 0;
                        CourseRow["CreateDate"] = Utility.GetDateOfToday();
                        CourseRow["InActive"] = 0;
                        //  CourseRow["TypeName"] = "دوره";

                        CourseRow["UserId"] = Utility.GetCurrentUser_UserId();
                        CourseRow["ModifiedDate"] = DateTime.Now;
                        TrainingAcceptedGradeManager.AddRow(CourseRow);
                    }
                    int cnt = TrainingAcceptedGradeManager.Save();
                    TrainingAcceptedGradeManager.DataTable.AcceptChanges();
                    if (cnt <= 0)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Attributes.Add("Style", "display:block");
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                        return;
                    }
                }
            }
            TransactionManager.EndSave();
            HiddenFieldUpGradePoint["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldUpGradePoint["UpGrdPId"] = Utility.EncryptQS(DocUpGradePointManager[0]["UpGrdPId"].ToString());
            RoundPanelUpGradePoint.HeaderText = "ویرایش";
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "ذخیره انجام شد.";
            #endregion


        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void EditUpGradePoint(int UpGrdPId)
    {

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
        TSP.DataManager.TrainingAcceptedGradeManager TrainingAcceptedGradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();
        TransactionManager.Add(DocUpGradePointManager);
        TransactionManager.Add(TrainingAcceptedGradeManager);
        try
        {

            if (Session["Course"] == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ثبت حداقل یک مدرک تحصیلی الزامی می باشد";
                return;
            }
            TransactionManager.BeginSave();
            dtCourse = (DataTable)Session["Course"];
            #region Edit DocUpGradePointManager
            DocUpGradePointManager.FindByCode(UpGrdPId);
            if (DocUpGradePointManager.Count != 1)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = " اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                TransactionManager.CancelSave();
                return;
            }
            DocUpGradePointManager[0].BeginEdit();

            DocUpGradePointManager[0]["UpGrdId"] = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            DocUpGradePointManager[0]["ResId"] = int.Parse(cmbResponsibility.SelectedItem.Value.ToString());
            DocUpGradePointManager[0]["MjParentId"] = int.Parse(CmbMajor.SelectedItem.Value.ToString());
            if (Convert.ToInt16(cmbIsTestNeed.Value) == 1)
            {
                DocUpGradePointManager[0]["IsTestNeed"] = 1;
                DocUpGradePointManager[0]["MinPointPeriod"] = 0;
                DocUpGradePointManager[0]["MinPointSeminar"] = 0;
                DocUpGradePointManager[0]["TotalPoint"] = 0;
                DocUpGradePointManager[0]["MinPeriodNeed"] = 0;
            }
            if (Convert.ToInt16(cmbIsTestNeed.Value) == 0)//ندارد
            {
                DocUpGradePointManager[0]["IsTestNeed"] = 0;
                DocUpGradePointManager[0]["MinPointPeriod"] = txtMinPointPeriod.Text;
                DocUpGradePointManager[0]["MinPointSeminar"] = txtMinPointSeminar.Text;
                DocUpGradePointManager[0]["TotalPoint"] = txtTotalPoint.Text;
                DocUpGradePointManager[0]["MinPeriodNeed"] = txtMinPeriodNeed.Text;
            }

            DocUpGradePointManager[0]["Status"] = CmbStatus.Value;
            DocUpGradePointManager[0]["JobDuration"] = txtJobDuration.Text;
            DocUpGradePointManager[0]["TotalPointNeed"] = txtTotalPointNeed.Text;


            DocUpGradePointManager[0]["Date"] = Utility.GetDateOfToday();
            DocUpGradePointManager[0]["Time"] = Utility.GetCurrentTime();
            DocUpGradePointManager[0]["Description"] = txtDescription.Text;
            DocUpGradePointManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocUpGradePointManager[0]["ModifiedDate"] = DateTime.Now;

            DocUpGradePointManager[0].EndEdit();

            if (DocUpGradePointManager.Save() < 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                TransactionManager.CancelSave();
                return;
            }

            #endregion

            DataRow[] DelRows = dtCourse.Select(null, null, DataViewRowState.Deleted);
            DataRow[] InsertRows = dtCourse.Select(null, null, DataViewRowState.Added);
            if (DelRows.Length > 0)
            {
                for (int i = 0; i < DelRows.Length; i++)
                {
                    int TrGrId = -1;
                    if (!Utility.IsDBNullOrNullValue(DelRows[i]["TrGrId", DataRowVersion.Original].ToString()))
                        TrGrId = int.Parse(DelRows[i]["TrGrId", DataRowVersion.Original].ToString());
                    else continue;

                    if (TrGrId == -1)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است";
                        return;
                    }

                    TrainingAcceptedGradeManager.FindByCode(TrGrId);
                    if (TrainingAcceptedGradeManager.Count < 0)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است";
                        return;
                    }
                    TrainingAcceptedGradeManager[0].Delete();
                    TrainingAcceptedGradeManager.Save();
                    TrainingAcceptedGradeManager.DataSet.AcceptChanges();
                }
            }
            if (InsertRows.Length > 0)
            {
                for (int i = 0; i < dtCourse.DefaultView.Count; i++)
                {
                    if (Utility.IsDBNullOrNullValue(dtCourse.Rows[i]["TrGrId"]))
                    {
                        DataRow CourseRow = TrainingAcceptedGradeManager.NewRow();

                        CourseRow["UpGrdPId"] = DocUpGradePointManager[0]["UpGrdPId"].ToString();
                        CourseRow["PkId"] = dtCourse.Rows[i]["PkId"].ToString();
                        CourseRow["Type"] = 0;
                        CourseRow["CreateDate"] = Utility.GetDateOfToday();
                        CourseRow["InActive"] = 0;
                        CourseRow["UserId"] = Utility.GetCurrentUser_UserId();
                        CourseRow["ModifiedDate"] = DateTime.Now;
                        TrainingAcceptedGradeManager.AddRow(CourseRow);

                    }
                }

                if (TrainingAcceptedGradeManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    return;
                }
                TrainingAcceptedGradeManager.DataTable.AcceptChanges();
            }

            TransactionManager.EndSave();
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "ذخیره انجام شد.";
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
            Utility.SaveWebsiteError(err);
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
    #endregion

}
