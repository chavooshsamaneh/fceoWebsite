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

public partial class Employee_TechnicalServices_BaseInfo_ProjectIngridientMajorsInsert : System.Web.UI.Page
{
    string PageMode;
    string ProjectIngridientMajorsId;

    bool IsPageRefresh = false;
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //btnDelete.Enabled = per.CanDelete;
            //btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PIMId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("ProjectIngridientMajors.aspx");
                return;
            }

            ObjectdatasourceProjectIngridient.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString() + "OR ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            //this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnDelete"] != null)
        //    this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Update();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectIngridientMajors.aspx");
    }

    protected void ASPxComboBoxProjectIngridientType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjectdatasourceObserversPlansType.SelectParameters[0].DefaultValue = ASPxComboBoxProjectIngridientType.Value.ToString();
        ASPxComboBoxType.DataBind();
        if (!Utility.IsDBNullOrNullValue(ASPxComboBoxProjectIngridientType.Value) && Convert.ToInt32( ASPxComboBoxProjectIngridientType.Value)==(int)TSP.DataManager.TSProjectIngridientType.Observer)
        {
            lblObserverGroup.Visible = lblPercent.Visible = cmbObserverGroup.Visible = txtPercent.Visible = true;
        }
        else
        {
            lblObserverGroup.Visible = lblPercent.Visible = cmbObserverGroup.Visible = txtPercent.Visible = false;
        }
    }
    #endregion
    #region Methods
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkPIMId.Value = Server.HtmlDecode(Request.QueryString["PIMId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            ProjectIngridientMajorsId = Utility.DecryptQS(PkPIMId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectIngridientMajorsId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }
    }

    private void SetMode()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

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

    private void SetNewModeKeys()
    {
        btnNew.Enabled =
        btnNew2.Enabled =
        btnSave.Enabled =
        btnSave2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        CheckAccess();

        ObjectdatasourceProjectIngridient.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString() + "OR ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();

        SetEnabled(true);

        ASPxComboBoxProjectIngridientType.DataBind();
        ASPxComboBoxProjectIngridientType.SelectedIndex = -1;
        ASPxComboBoxType.DataBind();
        ASPxComboBoxType.SelectedIndex = -1;
        ASPxComboBoxMajors.DataBind();
        ASPxComboBoxMajors.SelectedIndex = -1;
        cmbGrad.DataBind();
        cmbGrad.SelectedIndex = -1;
        cmbGrad.Items.Insert(0, new ListEditItem("-------------", -1));
        ASPxComboBoxStructureGroups.DataBind();
        ASPxComboBoxStructureGroups.SelectedIndex = -1;
        ASPxComboBoxStructureGroups.DataBind();
        ASPxComboBoxStructureGroups.Items.Insert(0, new ListEditItem("<همه موارد>", -1));
        ASPxTextBoxStep.Text =
        txtFoundationMin.Text =
       txtFoundationMax.Text =
       txtPercent.Text = "";
        cmbObserverGroup.SelectedIndex = -1;
        ObjectdatasourceStructureSkeleton.DataBind();
        ComboBoxStructureSkeleton.Items.Insert(0, new ListEditItem("<همه موارد>", -1));
        ComboBoxStructureSkeleton.SelectedIndex = -1;
        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();

        ObjectdatasourceProjectIngridient.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString() + "OR ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();

        SetEnabled(true);

        SetValues();

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();

        ObjectdatasourceProjectIngridient.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString() + "OR ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();

        SetEnabled(false);

        SetValues();

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetValues()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        ProjectIngridientMajorsId = Utility.DecryptQS(PkPIMId.Value);

        if ((string.IsNullOrEmpty(ProjectIngridientMajorsId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager Manager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
        Manager.FindByProjectIngridientMajorsId(Convert.ToInt32(ProjectIngridientMajorsId));
        if (Manager.Count == 1)
        {
            ASPxComboBoxProjectIngridientType.DataBind();
            ASPxComboBoxProjectIngridientType.Value = Convert.ToInt32(Manager[0]["ProjectIngridientTypeId"]);
            ObjectdatasourceObserversPlansType.SelectParameters[0].DefaultValue = ASPxComboBoxProjectIngridientType.Value.ToString();

            if (!Utility.IsDBNullOrNullValue(ASPxComboBoxProjectIngridientType.Value) && Convert.ToInt32(ASPxComboBoxProjectIngridientType.Value) == (int)TSP.DataManager.TSProjectIngridientType.Observer)
            {
                lblObserverGroup.Visible = lblPercent.Visible = cmbObserverGroup.Visible = txtPercent.Visible = true;
            }
            else
            {
                lblObserverGroup.Visible = lblPercent.Visible = cmbObserverGroup.Visible = txtPercent.Visible = false;
            }
            ASPxComboBoxType.DataBind();
            ASPxComboBoxType.SelectedIndex = ASPxComboBoxType.Items.FindByValue(Manager[0]["ObserversPlansTypeId"]).Index;
            ASPxComboBoxMajors.DataBind();
            ASPxComboBoxMajors.SelectedIndex = ASPxComboBoxMajors.Items.FindByValue(Manager[0]["MjId"]).Index;
            cmbGrad.DataBind();
            cmbGrad.Items.Insert(0, new ListEditItem("----------", -1));
            cmbGrad.SelectedIndex = Utility.IsDBNullOrNullValue(Manager[0]["GrdId"]) ? -1 : cmbGrad.Items.FindByValue(Manager[0]["GrdId"]).Index;

            ASPxTextBoxStep.Text = Manager[0]["Step"].ToString();

            ASPxComboBoxStructureGroups.DataBind();
            ASPxComboBoxStructureGroups.Items.Insert(0, new ListEditItem("<همه موارد>", -1));
            ASPxComboBoxStructureGroups.SelectedIndex = Utility.IsDBNullOrNullValue(Manager[0]["GroupId"]) ? -1 : ASPxComboBoxStructureGroups.Items.FindByValue(Manager[0]["GroupId"]).Index;

            if (ASPxComboBoxStructureGroups.SelectedIndex == -1)
                ASPxComboBoxStructureGroups.SelectedIndex = 0;
            txtFoundationMin.Text = Utility.IsDBNullOrNullValue(Manager[0]["FoundationMin"]) ? "" : Manager[0]["FoundationMin"].ToString();
            txtFoundationMax.Text = Utility.IsDBNullOrNullValue(Manager[0]["FoundationMax"]) ? "" : Manager[0]["FoundationMax"].ToString();
            txtPercent.Text = Utility.IsDBNullOrNullValue(Manager[0]["Percent"]) ? "" : Manager[0]["Percent"].ToString();
            cmbObserverGroup.SelectedIndex = Utility.IsDBNullOrNullValue(Manager[0]["ObserverGroup"]) ? -1 : cmbObserverGroup.Items.FindByValue(Manager[0]["ObserverGroup"]).Index;

            ComboBoxStructureSkeleton.DataBind();
            ComboBoxStructureSkeleton.Items.Insert(0, new ListEditItem("<همه موارد>", -1));
            ComboBoxStructureSkeleton.SelectedIndex = Utility.IsDBNullOrNullValue(Manager[0]["StructureSkeletonId"]) ? 0  : ComboBoxStructureSkeleton.Items.FindByValue(Manager[0]["StructureSkeletonId"]).Index;

        }
        else
        {
            SetLabelWarning("چنین رکوردی وجود ندارد");
        }
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        //if (btnDelete.Enabled == true)
        //{
        //    btnDelete.Enabled = per.CanDelete;
        //    btnDelete2.Enabled = per.CanDelete;
        //}
        if (Utility.DecryptQS(PgMode.Value) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(PgMode.Value) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    private void SetEnabled(Boolean Enable)
    {

        ASPxComboBoxProjectIngridientType.Enabled =
        ASPxComboBoxType.Enabled =
        ASPxComboBoxMajors.Enabled =
        cmbGrad.Enabled =
        ASPxComboBoxStructureGroups.Enabled =
        ASPxTextBoxStep.Enabled =
        txtFoundationMin.Enabled =
        txtFoundationMax.Enabled =
        txtPercent.Enabled =
        cmbObserverGroup.Enabled =
        ComboBoxStructureSkeleton.Enabled = Enable;
    }

    /************************************************************ Insert *******************************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        try
        {
            InsertProjectIngridientMajors();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            SetError(err, 'I');
        }
    }

    private void InsertProjectIngridientMajors()
    {
        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();

        DataRow rowProjectIngridientMajors = ProjectIngridientMajorsManager.NewRow();

        rowProjectIngridientMajors.BeginEdit();
        rowProjectIngridientMajors["ProjectIngridientTypeId"] = ASPxComboBoxProjectIngridientType.Value;
        rowProjectIngridientMajors["ObserversPlansTypeId"] = ASPxComboBoxType.Value;
        rowProjectIngridientMajors["MjId"] = ASPxComboBoxMajors.Value;

        if (cmbGrad.SelectedIndex != -1)
            rowProjectIngridientMajors["GrdId"] = cmbGrad.Value;
        else
            rowProjectIngridientMajors["GrdId"] = DBNull.Value;

        if (ASPxTextBoxStep.Text != "0")
            rowProjectIngridientMajors["Step"] = ASPxTextBoxStep.Text;
        else
            rowProjectIngridientMajors["Step"] = DBNull.Value;

        if (ASPxComboBoxStructureGroups.SelectedIndex != -1 && Convert.ToInt32(ASPxComboBoxStructureGroups.Value) > 0)
            rowProjectIngridientMajors["GroupId"] = ASPxComboBoxStructureGroups.Value;
        else
            rowProjectIngridientMajors["GroupId"] = DBNull.Value;

        rowProjectIngridientMajors["FoundationMin"] = txtFoundationMin.Text;
        rowProjectIngridientMajors["FoundationMax"] = txtFoundationMax.Text;
        rowProjectIngridientMajors["Percent"] = !string.IsNullOrWhiteSpace(txtPercent.Text) ? txtPercent.Text :"0" ;
        if (cmbObserverGroup.SelectedIndex != -1)
            rowProjectIngridientMajors["ObserverGroup"] = cmbObserverGroup.Value;
        else
            rowProjectIngridientMajors["ObserverGroup"] = DBNull.Value;
        if (ComboBoxStructureSkeleton.SelectedIndex != -1)
            rowProjectIngridientMajors["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        else
            rowProjectIngridientMajors["StructureSkeletonId"] = DBNull.Value;



        rowProjectIngridientMajors["UserId"] = Utility.GetCurrentUser_UserId();
        rowProjectIngridientMajors["ModifiedDate"] = DateTime.Now;
        rowProjectIngridientMajors.EndEdit();

        ProjectIngridientMajorsManager.AddRow(rowProjectIngridientMajors);
        ProjectIngridientMajorsManager.Save();

        ProjectIngridientMajorsId = ProjectIngridientMajorsManager[0]["ProjectIngridientMajorsId"].ToString();
        PkPIMId.Value = Utility.EncryptQS(ProjectIngridientMajorsId.ToString());

    }

    /************************************************************* Update *********************************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();

        try
        {
            UpdateProjectIngridientMajors(ProjectIngridientMajorsManager);

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            SetError(err, 'U');
        }
    }

    private void UpdateProjectIngridientMajors(TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager)
    {
        ProjectIngridientMajorsId = Utility.DecryptQS(PkPIMId.Value);

        if (string.IsNullOrEmpty(ProjectIngridientMajorsId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        ProjectIngridientMajorsManager.FindByProjectIngridientMajorsId(Convert.ToInt32(ProjectIngridientMajorsId));

        if (ProjectIngridientMajorsManager.Count >= 1)
        {
            ProjectIngridientMajorsManager[0].BeginEdit();

            ProjectIngridientMajorsManager[0]["ProjectIngridientTypeId"] = ASPxComboBoxProjectIngridientType.Value;
            ProjectIngridientMajorsManager[0]["ObserversPlansTypeId"] = ASPxComboBoxType.Value;
            ProjectIngridientMajorsManager[0]["MjId"] = ASPxComboBoxMajors.Value;

            if (cmbGrad.SelectedIndex != -1)
                ProjectIngridientMajorsManager[0]["GrdId"] = cmbGrad.Value;
            else
                ProjectIngridientMajorsManager[0]["GrdId"] = DBNull.Value;

            if (ASPxTextBoxStep.Text != "0")
                ProjectIngridientMajorsManager[0]["Step"] = ASPxTextBoxStep.Text;
            else
                ProjectIngridientMajorsManager[0]["Step"] = DBNull.Value;

            if (ASPxComboBoxStructureGroups.SelectedIndex != -1 && Convert.ToInt32(ASPxComboBoxStructureGroups.Value) > 0)
                ProjectIngridientMajorsManager[0]["GroupId"] = ASPxComboBoxStructureGroups.Value;
            else
                ProjectIngridientMajorsManager[0]["GroupId"] = DBNull.Value;

            ProjectIngridientMajorsManager[0]["FoundationMin"] = txtFoundationMin.Text;
            ProjectIngridientMajorsManager[0]["FoundationMax"] = txtFoundationMax.Text;
            if (!string.IsNullOrEmpty(txtPercent.Text))
                ProjectIngridientMajorsManager[0]["Percent"] = txtPercent.Text;
            else
                ProjectIngridientMajorsManager[0]["Percent"] = DBNull.Value;
            if (cmbObserverGroup.SelectedIndex != -1)
                ProjectIngridientMajorsManager[0]["ObserverGroup"] = cmbObserverGroup.Value;
            else
                ProjectIngridientMajorsManager[0]["ObserverGroup"] = DBNull.Value;
            if (ComboBoxStructureSkeleton.SelectedIndex != -1)
                ProjectIngridientMajorsManager[0]["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
            else
                ProjectIngridientMajorsManager[0]["StructureSkeletonId"] = DBNull.Value;

            ProjectIngridientMajorsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ProjectIngridientMajorsManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectIngridientMajorsManager[0].EndEdit();

            ProjectIngridientMajorsManager.Save();

            ProjectIngridientMajorsManager.DataTable.AcceptChanges();
            ProjectIngridientMajorsId = ProjectIngridientMajorsManager[0]["ProjectIngridientMajorsId"].ToString();
            PkPIMId.Value = Utility.EncryptQS(ProjectIngridientMajorsId.ToString());
        }
    }

    /*************************************************************************************************************/
    private void DeleteProjectIngridientMajors()
    {
        ProjectIngridientMajorsId = Utility.DecryptQS(PkPIMId.Value);

        if (string.IsNullOrEmpty(ProjectIngridientMajorsId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
        ProjectIngridientMajorsManager.FindByProjectIngridientMajorsId(Convert.ToInt32(ProjectIngridientMajorsId));

        if (ProjectIngridientMajorsManager.Count == 1)
        {
            try
            {
                ProjectIngridientMajorsManager[0].Delete();
                int cn = ProjectIngridientMajorsManager.Save();
                if (cn == 1)
                {
                    ProjectIngridientMajorsManager.DataTable.AcceptChanges();
                    PkPIMId.Value = Utility.EncryptQS("-1");
                    PgMode.Value = Utility.EncryptQS("New");
                    SetNewModeKeys();

                    SetLabelWarning("حذف انجام شد");
                }
                else
                {
                    SetLabelWarning("خطایی در حذف انجام گرفته است");
                }
            }
            catch (Exception err)
            {
                SetError(err, 'D');
            }
        }
    }


    /*************************************************************************************************************/
    private void SetError(Exception err, char Flag)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    #endregion
}