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

public partial class Employee_TechnicalServices_BaseInfo_MemberImpCapacityInsert : System.Web.UI.Page
{
    string PageMode;
    string EnIqId;

    bool IsPageRefresh = false;

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
            TSP.DataManager.Permission per = TSP.DataManager.DocOffEngOfficeImpQualificationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["EnIqId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("MemberImpCapacity.aspx");
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
        Response.Redirect("MemberImpCapacity.aspx");
    }

    /*******************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkEnIqId.Value = Server.HtmlDecode(Request.QueryString["EnIqId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            EnIqId = Utility.DecryptQS(PkEnIqId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(EnIqId))
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
        ASPxTextBoxMaxFloor.Enabled = Enable;
        ASPxTextBoxMaxJobCapacity.Enabled = Enable;
        ASPxTextBoxMaxUnitCount.Enabled = Enable;
    }

    private void ClearForm()
    {
        ASPxComboBoxGrde.DataBind();
        ASPxComboBoxGrde.SelectedIndex = -1;

        ASPxTextBoxMaxFloor.Text = "";
        ASPxTextBoxMaxJobCapacity.Text = "";
        ASPxTextBoxMaxUnitCount.Text = "";
        CreateDate.DateValue = DateTime.Now;
        ASPxTextBoxInActivStatus.Text = "فعال";
    }

    private void FillForm()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        EnIqId = Utility.DecryptQS(PkEnIqId.Value);

        if ((string.IsNullOrEmpty(EnIqId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.DocOffEngOfficeImpQualificationManager DocOffEngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
        DocOffEngOfficeImpQualificationManager.FindByCode(Convert.ToInt32(EnIqId));
        if (DocOffEngOfficeImpQualificationManager.Count == 1)
        {
            ASPxComboBoxGrde.DataBind();
            ASPxComboBoxGrde.Value = DocOffEngOfficeImpQualificationManager[0]["GrdId"];

            ASPxTextBoxMaxFloor.Text = DocOffEngOfficeImpQualificationManager[0]["MaxFloor"].ToString();
            ASPxTextBoxMaxJobCapacity.Text = DocOffEngOfficeImpQualificationManager[0]["MaxJobCapacity"].ToString();
            ASPxTextBoxMaxUnitCount.Text = DocOffEngOfficeImpQualificationManager[0]["MaxUnitCount"].ToString();
            CreateDate.Text = DocOffEngOfficeImpQualificationManager[0]["CreateDate"].ToString();
            ASPxTextBoxInActivStatus.Text = DocOffEngOfficeImpQualificationManager[0]["InActivStatus"].ToString();
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
        TSP.DataManager.DocOffEngOfficeImpQualificationManager DocOffEngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();

        DataRow RowDocOffEngOfficeImpQualification = DocOffEngOfficeImpQualificationManager.NewRow();

        RowDocOffEngOfficeImpQualification.BeginEdit();
        RowDocOffEngOfficeImpQualification["GrdId"] = ASPxComboBoxGrde.Value;
        RowDocOffEngOfficeImpQualification["MaxFloor"] = ASPxTextBoxMaxFloor.Text;
        RowDocOffEngOfficeImpQualification["MaxJobCapacity"] = ASPxTextBoxMaxJobCapacity.Text;
        RowDocOffEngOfficeImpQualification["MaxUnitCount"] = ASPxTextBoxMaxUnitCount.Text;
        RowDocOffEngOfficeImpQualification["CreateDate"] = CreateDate.Text;
        RowDocOffEngOfficeImpQualification["InActive"] = 0;        
        RowDocOffEngOfficeImpQualification["UserId"] = Utility.GetCurrentUser_UserId();
        RowDocOffEngOfficeImpQualification["ModifiedDate"] = DateTime.Now;
        RowDocOffEngOfficeImpQualification.EndEdit();

        DocOffEngOfficeImpQualificationManager.AddRow(RowDocOffEngOfficeImpQualification);
        DocOffEngOfficeImpQualificationManager.Save();

        EnIqId = DocOffEngOfficeImpQualificationManager[0]["EnIqId"].ToString();
        PkEnIqId.Value = Utility.EncryptQS(EnIqId.ToString());
    }

    private bool CheckIfGradeExist()
    {
        TSP.DataManager.DocOffEngOfficeImpQualificationManager DocOffEngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();

        int GradeId = Convert.ToInt32(ASPxComboBoxGrde.Value);
        DocOffEngOfficeImpQualificationManager.FindByGrdId(GradeId);
        if (DocOffEngOfficeImpQualificationManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckCapacityAndMaxJobCount()
    {
        if (ASPxTextBoxMaxFloor.Text == "0")
        {
            SetLabelWarning("لطفا حداکثر تعداد طبقات را وارد کنید.");
            return false;
        }

        if (ASPxTextBoxMaxJobCapacity.Text == "0")
        {
            SetLabelWarning("لطفا حداکثر ظرفیت اشتغال را وارد کنید.");
            return false;
        }

        if (ASPxTextBoxMaxUnitCount.Text == "0")
        {
            SetLabelWarning("لطفا حداکثر تعداد واحد ساختمانی همزمان را وارد کنید.");
            return false;
        }

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

    /*****************************************************************************************************************************/
}