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

public partial class Employee_TechnicalServices_BaseInfo_OffDsgnCapacityInsert : System.Web.UI.Page
{
    string PageMode;
    string InJId;

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
            TSP.DataManager.Permission per = TSP.DataManager.DocOffIncreaseJobCapacityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["InJId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("OffDsgnCapacity.aspx");
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
        Response.Redirect("OffDsgnCapacity.aspx");
    }

    /*******************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkInJId.Value = Server.HtmlDecode(Request.QueryString["InJId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            InJId = Utility.DecryptQS(PkInJId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(InJId))
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
        ASPxComboBoxDocOffMajorNum.Enabled = Enable;
        ASPxTextBoxDesignIncPer.Enabled = Enable;
        ASPxTextBoxSameGradeIncPer.Enabled = Enable;
        ASPxTextBoxMajorIncPer.Enabled = Enable;
    }

    private void ClearForm()
    {
        ASPxComboBoxDocOffMajorNum.DataBind();
        ASPxComboBoxDocOffMajorNum.SelectedIndex = -1;

        ASPxTextBoxDesignIncPer.Text = "";
        ASPxTextBoxSameGradeIncPer.Text = "";
        ASPxTextBoxMajorIncPer.Text = "";
        CreateDate.DateValue = DateTime.Now;
        ASPxTextBoxInActivStatus.Text = "فعال";
    }

    private void FillForm()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        InJId = Utility.DecryptQS(PkInJId.Value);

        if ((string.IsNullOrEmpty(InJId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.DocOffIncreaseJobCapacityManager DocOffIncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
        DocOffIncreaseJobCapacityManager.FindByCode(Convert.ToInt32(InJId));
        if (DocOffIncreaseJobCapacityManager.Count == 1)
        {
            ASPxComboBoxDocOffMajorNum.DataBind();
            ASPxComboBoxDocOffMajorNum.Value = DocOffIncreaseJobCapacityManager[0]["MNumId"];

            ASPxTextBoxDesignIncPer.Text = DocOffIncreaseJobCapacityManager[0]["DesignIncPer"].ToString();
            ASPxTextBoxSameGradeIncPer.Text = DocOffIncreaseJobCapacityManager[0]["SameGradeIncPer"].ToString();
            ASPxTextBoxMajorIncPer.Text = DocOffIncreaseJobCapacityManager[0]["MajorIncPer"].ToString();
            CreateDate.Text = DocOffIncreaseJobCapacityManager[0]["CreateDate"].ToString();
            ASPxTextBoxInActivStatus.Text = DocOffIncreaseJobCapacityManager[0]["InActivStatus"].ToString();
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
            SetLabelWarning("برای ترکیب رشته مورد نظر رکورد فعال موجود است.");
            return;
        }

        if (!CheckFileds())
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
        TSP.DataManager.DocOffIncreaseJobCapacityManager DocOffIncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

        DataRow RowIncreaseJobCapacity = DocOffIncreaseJobCapacityManager.NewRow();

        RowIncreaseJobCapacity.BeginEdit();
        RowIncreaseJobCapacity["MNumId"] = ASPxComboBoxDocOffMajorNum.Value;
        RowIncreaseJobCapacity["Construction"] = ASPxComboBoxDocOffMajorNum.Text;
        RowIncreaseJobCapacity["DesignIncPer"] = ASPxTextBoxDesignIncPer.Text;
        RowIncreaseJobCapacity["SameGradeIncPer"] = ASPxTextBoxSameGradeIncPer.Text;
        RowIncreaseJobCapacity["MajorIncPer"] = ASPxTextBoxMajorIncPer.Text;
        RowIncreaseJobCapacity["Type"] = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
        RowIncreaseJobCapacity["InActive"] = 0;
        RowIncreaseJobCapacity["CreateDate"] = CreateDate.Text;
        RowIncreaseJobCapacity["UserId"] = Utility.GetCurrentUser_UserId();
        RowIncreaseJobCapacity["ModifiedDate"] = DateTime.Now;
        RowIncreaseJobCapacity.EndEdit();

        DocOffIncreaseJobCapacityManager.AddRow(RowIncreaseJobCapacity);
        DocOffIncreaseJobCapacityManager.Save();

        InJId = DocOffIncreaseJobCapacityManager[0]["InJId"].ToString();
        PkInJId.Value = Utility.EncryptQS(InJId.ToString());
    }

    private bool CheckIfGradeExist()
    {
        TSP.DataManager.DocOffIncreaseJobCapacityManager DocOffIncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

        int MNumId = Convert.ToInt32(ASPxComboBoxDocOffMajorNum.Value);
        DocOffIncreaseJobCapacityManager.FindByMNumId(MNumId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice);
        if (DocOffIncreaseJobCapacityManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckFileds()
    {
        if (ASPxTextBoxDesignIncPer.Text == "0")
        {
            SetLabelWarning("لطفا درصد افزایش دفتر مهندسی را وارد کنید.");
            return false;
        }

        if (ASPxTextBoxSameGradeIncPer.Text == "0")
        {
            SetLabelWarning("لطفا درصد درصد افزایش در صورت همپایه بودن پروانه اشتغال را وارد کنید.");
            return false;
        }

        if (ASPxTextBoxMajorIncPer.Text == "0")
        {
            SetLabelWarning("لطفا درصد درصد افزایش درصد افزایش در صورت حضور بیش از یک نفر را وارد کنید.");
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