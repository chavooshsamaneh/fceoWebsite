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

public partial class Employee_TechnicalServices_BaseInfo_OffImpCapacityInsert : System.Web.UI.Page
{
    string PageMode;
    string OfmqId;

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
            TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeMembersQualificationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["OfmqId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("OffImpCapacity.aspx");
                return;
            }

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        switch (PageMode)
        {
            case "New":
                Insert();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OffImpCapacity.aspx");
    }
    #endregion
    /*******************************************************************************************************************************************/
    #region Methods
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkOfmqId.Value = Server.HtmlDecode(Request.QueryString["OfmqId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            OfmqId = Utility.DecryptQS(PkOfmqId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfmqId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
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
        SetControlsNewMode();
    }

    private void SetEditModeKeys()
    {
        SetControlsEditMode();
        FillForm();
    }

    private void SetViewModeKeys()
    {
        SetControlsViewMode();
        FillForm();
    }

    private void SetControlsNewMode()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        CheckAccess();

        SetEnable(true);
        ClearForm();

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetControlsEditMode()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        CheckAccess();

        SetEnable(true);

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetControlsViewMode()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        CheckAccess();

        SetEnable(false);

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetEnable(bool Enable)
    {
        ASPxComboBoxGrde.Enabled = Enable;
        ASPxTextBoxMinMeCount.Enabled = Enable;
        ASPxComboBoxType.Enabled = Enable;
        ASPxComboBoxMinGradeId.Enabled = Enable;
        ASPxComboBoxCivilGrdId.Enabled = Enable;
        ASPxTextBoxMaxFloor.Enabled = Enable;
        ASPxTextBoxMaxCapacity.Enabled = Enable;
        ASPxTextBoxMaxJobCount.Enabled = Enable;
        ASPxTextBoxPointsLimitation.Enabled = Enable;
        cmbActivityType.Enabled = Enable;
    }

    private void ClearForm()
    {
        ASPxComboBoxGrde.DataBind();
        ASPxComboBoxGrde.SelectedIndex = -1;

        ASPxComboBoxType.DataBind();
        ASPxComboBoxType.SelectedIndex = -1;

        ASPxComboBoxMinGradeId.DataBind();
        ASPxComboBoxMinGradeId.SelectedIndex = -1;

        ASPxComboBoxCivilGrdId.DataBind();
        ASPxComboBoxCivilGrdId.SelectedIndex = -1;
        cmbActivityType.DataBind();
        cmbActivityType.SelectedIndex = -1;

        ASPxTextBoxMinMeCount.Text = "";
        ASPxTextBoxMaxFloor.Text = "";
        ASPxTextBoxMaxCapacity.Text = "";
        ASPxTextBoxMaxJobCount.Text = "";
        ASPxTextBoxPointsLimitation.Text = "";
        CreateDate.DateValue = DateTime.Now;
        ASPxTextBoxInActivStatus.Text = "فعال";
    }

    private void FillForm()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        OfmqId = Utility.DecryptQS(PkOfmqId.Value);

        if ((string.IsNullOrEmpty(OfmqId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.DocOffOfficeMembersQualificationManager DocOffOfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();
        DocOffOfficeMembersQualificationManager.FindByCode(Convert.ToInt32(OfmqId));
        if (DocOffOfficeMembersQualificationManager.Count == 1)
        {
            ASPxComboBoxGrde.DataBind();
            ASPxComboBoxGrde.Value = DocOffOfficeMembersQualificationManager[0]["GrdId"];

            ASPxComboBoxType.DataBind();
            //ASPxComboBoxType.Value = DocOffOfficeMembersQualificationManager[0]["Type"];
            ASPxComboBoxType.SelectedIndex = ASPxComboBoxType.Items.IndexOfValue(Convert.ToInt32(DocOffOfficeMembersQualificationManager[0]["Type"]));

            ASPxComboBoxMinGradeId.DataBind();
            ASPxComboBoxMinGradeId.Value = DocOffOfficeMembersQualificationManager[0]["MinGradeId"];

            ASPxComboBoxCivilGrdId.DataBind();
            ASPxComboBoxCivilGrdId.Value = DocOffOfficeMembersQualificationManager[0]["CivilGrdId"];
            
            cmbActivityType.DataBind();
            cmbActivityType.Value = DocOffOfficeMembersQualificationManager[0]["ActivityType"];

            ASPxTextBoxMinMeCount.Text = DocOffOfficeMembersQualificationManager[0]["MinMeCount"].ToString();
            ASPxTextBoxMaxFloor.Text = DocOffOfficeMembersQualificationManager[0]["MaxFloor"].ToString();
            ASPxTextBoxMaxCapacity.Text = DocOffOfficeMembersQualificationManager[0]["MaxCapacity"].ToString();
            ASPxTextBoxMaxJobCount.Text = DocOffOfficeMembersQualificationManager[0]["MaxJobCount"].ToString();
            ASPxTextBoxPointsLimitation.Text = DocOffOfficeMembersQualificationManager[0]["PointsLimitation"].ToString();
            CreateDate.Text = DocOffOfficeMembersQualificationManager[0]["CreateDate"].ToString();
            ASPxTextBoxInActivStatus.Text = DocOffOfficeMembersQualificationManager[0]["InActivStatus"].ToString();
        }
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeMembersQualificationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
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
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    /************************************************************ Insert *******************************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        if (!CheckCivilGrdId())
            return;

        if (CheckIfGradeExist())
        {
            SetLabelWarning("برای پایه مورد نظر ظرفیت اشتغال فعال موجود است.");
            return;
        }

        if (!CheckCapacityAndMaxJobCount())
            return;

        try
        {
            InsertMemberCapacity();

            PgMode.Value = Utility.EncryptQS("View");
            SetControlsViewMode();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            SetError(err, 'I');
        }
    }

    private void InsertMemberCapacity()
    {
        TSP.DataManager.DocOffOfficeMembersQualificationManager DocOffOfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

        DataRow RowDocOffOfficeMembersQualification = DocOffOfficeMembersQualificationManager.NewRow();

        RowDocOffOfficeMembersQualification.BeginEdit();
        RowDocOffOfficeMembersQualification["GrdId"] = ASPxComboBoxGrde.Value;

        //ASPxComboBoxType.DataBind();
        RowDocOffOfficeMembersQualification["Type"] = ASPxComboBoxType.Value;

        //ASPxComboBoxMinGradeId.DataBind();
        RowDocOffOfficeMembersQualification["MinGradeId"] = ASPxComboBoxMinGradeId.Value;

        //ASPxComboBoxCivilGrdId.DataBind();
        if (ASPxComboBoxCivilGrdId.SelectedIndex != -1)
            RowDocOffOfficeMembersQualification["CivilGrdId"] = ASPxComboBoxCivilGrdId.Value;

        if(cmbActivityType.SelectedIndex!=-1)
            RowDocOffOfficeMembersQualification["ActivityType"] = cmbActivityType.Value;  

        RowDocOffOfficeMembersQualification["MinMeCount"] = ASPxTextBoxMinMeCount.Text;
        if (ASPxTextBoxMaxFloor.Text != "0")
            RowDocOffOfficeMembersQualification["MaxFloor"] = ASPxTextBoxMaxFloor.Text;
        else
            RowDocOffOfficeMembersQualification["MaxFloor"] = -1;
        RowDocOffOfficeMembersQualification["MaxCapacity"] = ASPxTextBoxMaxCapacity.Text;
        if (ASPxTextBoxMaxJobCount.Text != "0")
            RowDocOffOfficeMembersQualification["MaxJobCount"] = ASPxTextBoxMaxJobCount.Text;
        else
            RowDocOffOfficeMembersQualification["MaxJobCount"] = -1;
        RowDocOffOfficeMembersQualification["PointsLimitation"] = ASPxTextBoxPointsLimitation.Text;
        RowDocOffOfficeMembersQualification["CreateDate"] = CreateDate.Text;
        RowDocOffOfficeMembersQualification["InActive"] = 0;
        RowDocOffOfficeMembersQualification["UserId"] = Utility.GetCurrentUser_UserId();
        RowDocOffOfficeMembersQualification["ModifiedDate"] = DateTime.Now;
        RowDocOffOfficeMembersQualification.EndEdit();

        DocOffOfficeMembersQualificationManager.AddRow(RowDocOffOfficeMembersQualification);
        DocOffOfficeMembersQualificationManager.Save();

        OfmqId = DocOffOfficeMembersQualificationManager[0]["OfmqId"].ToString();
        PkOfmqId.Value = Utility.EncryptQS(OfmqId.ToString());
    }

    private bool CheckCivilGrdId()
    {
        int Type = Convert.ToInt32(ASPxComboBoxType.Value);
        switch (Type)
        {
            case (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan:
                if (ASPxComboBoxCivilGrdId.SelectedIndex == -1)
                {
                    SetLabelWarning("لطفا پایه پروانه اشتغال معمار یا عمران را انتخاب نمایید.");
                    return false;
                }
                break;

            default:
                if (ASPxComboBoxCivilGrdId.SelectedIndex != -1)
                {
                    SetLabelWarning("پایه پروانه اشتغال معمار یا عمران را فقط برای نوع کاردان - کاردان می توانید انتخاب نمایید.");
                    return false;
                }
                break;
        }

        return true;
    }

    private bool CheckIfGradeExist()
    {
        TSP.DataManager.DocOffOfficeMembersQualificationManager DocOffOfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

        int GradeId = Convert.ToInt32(ASPxComboBoxGrde.Value);

        int? CivilGrdId;
        if (ASPxComboBoxCivilGrdId.SelectedIndex == -1)
            CivilGrdId = null;
        else
            CivilGrdId = Convert.ToInt32(ASPxComboBoxCivilGrdId.Value);

        DocOffOfficeMembersQualificationManager.FindByGrdId(GradeId, Convert.ToInt32(ASPxComboBoxType.Value), CivilGrdId);
        DocOffOfficeMembersQualificationManager.CurrentFilter = "ActivityType="+ cmbActivityType.SelectedItem.Value.ToString();
        if (DocOffOfficeMembersQualificationManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckCapacityAndMaxJobCount()
    {
        if (ASPxTextBoxMinMeCount.Text == "0")
        {
            SetLabelWarning("لطفا حداقل تعداد دارندگان پروانه اشتغال را وارد کنید.");
            return false;
        }

        //if (ASPxTextBoxMaxFloor.Text == "0")
        //{
        //    SetLabelWarning("لطفا حداکثر تعداد طبقات را وارد کنید.");
        //    return false;
        //}

        if (ASPxTextBoxMaxCapacity.Text == "0")
        {
            SetLabelWarning("لطفا حداکثر ظرفیت اشتغال را وارد کنید.");
            return false;
        }

        //if (ASPxTextBoxMaxJobCount.Text == "0")
        //{
        //    SetLabelWarning("لطفا حداکثر تعداد کار را وارد کنید.");
        //    return false;
        //}

        return true;
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
    /*****************************************************************************************************************************/
}