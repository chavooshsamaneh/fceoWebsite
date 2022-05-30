using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Document_AddConfirmedGrade : System.Web.UI.Page
{

    private string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldPage["PgMd"].ToString());
        }
        set
        {
            HiddenFieldPage["PgMd"] = Utility.EncryptQS(value.ToString());
        }
    }

    private int GMRId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldPage["GMRId"].ToString()));
        }
        set
        {
            HiddenFieldPage["GMRId"] = Utility.EncryptQS(value.ToString());
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            CheckTablePermissions();
            SetKey();
            SetMode();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        TSP.DataManager.Permission per = GetPermission();
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        GMRId = -1;
        PageMode = "New";
        RoundPanelMain.HeaderText = "جدید";
        ClearForm();
        SetEnable(true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
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
                if (Utility.IsDBNullOrNullValue(GMRId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(GMRId);
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConfirmedGrade.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(GMRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(PageMode) && PageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                SetEnable(true);
                TSP.DataManager.Permission per = GetPermission();
                btnSave.Enabled = per.CanNew;
                btnSave2.Enabled = per.CanNew;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                PageMode = "Edit";
                RoundPanelMain.HeaderText = "ویرایش";
            }
        }
    }
    #endregion

    #region Methods
    private TSP.DataManager.Permission GetPermission()
    {
        return TSP.DataManager.DocAcceptedGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    }

    private void CheckTablePermissions()
    {
        TSP.DataManager.Permission per = GetPermission();
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
    }

    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["GMRId"]) || string.IsNullOrEmpty(Request.QueryString["pgMd"]))
        {
            Response.Redirect("ConfirmedGrade.aspx");
        }

        try
        {
            PageMode = Utility.DecryptQS(Request.QueryString["pgMd"]);
            GMRId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["GMRId"]));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
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
                SetViewMode();
                break;
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
        }
    }

    private void SetNewMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelMain.HeaderText = "جدید";
        ClearForm();
    }

    private void SetEditMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        if (Utility.IsDBNullOrNullValue(GMRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        FillForm(GMRId);
        RoundPanelMain.Enabled = true;
        RoundPanelMain.HeaderText = "ویرایش";
    }

    private void SetViewMode()
    {
        SetEnable(false);
        if (Utility.IsDBNullOrNullValue(GMRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.Permission per = GetPermission();
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        FillForm(GMRId);
        RoundPanelMain.HeaderText = "مشاهده";
    }

    protected void ClearForm()
    {
        comboGrade.SelectedIndex = -1;
        comboMajor.SelectedIndex = -1;
        comboResponsblity.SelectedIndex = -1;
    }

    protected void SetEnable(Boolean Enable)
    {
        comboGrade.Enabled = comboMajor.Enabled = comboResponsblity.Enabled = Enable;
    }

    protected void FillForm(int GMRId)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        DocAcceptedGradeManager.FindByCode(GMRId);
        if (DocAcceptedGradeManager.Count == 1)
        {
            comboGrade.DataBind();
            comboGrade.SelectedIndex = comboGrade.Items.FindByValue(DocAcceptedGradeManager[0]["GrdId"].ToString()).Index;
            comboMajor.DataBind();
            comboMajor.SelectedIndex = comboGrade.Items.FindByValue(DocAcceptedGradeManager[0]["MjId"].ToString()).Index;
            comboResponsblity.DataBind();
            comboResponsblity.SelectedIndex = comboGrade.Items.FindByValue(DocAcceptedGradeManager[0]["ResId"].ToString()).Index;
        }
    }

    protected void Edit(int GMRId)
    {
        try
        {
            TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
            TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager1 = new TSP.DataManager.DocAcceptedGradeManager();
            DocAcceptedGradeManager.FindByCode(GMRId);
            if (DocAcceptedGradeManager.Count != 1)
            {
                ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                return;
            }

            int GrdId = Convert.ToInt32(comboGrade.Value);
            int MjId = Convert.ToInt32(comboMajor.Value);
            int ResId = Convert.ToInt32(comboResponsblity.Value);
            DataTable dtAccGrd = DocAcceptedGradeManager1.SelectConfirmedGrade(MjId, GrdId, ResId);
            if (dtAccGrd.Rows.Count >= 1)
            {
                ShowMessage("اطلاعات وارد شده تکراری می باشد.");
                return;
            }
            DocAcceptedGradeManager.FindByCode(GMRId);
            if (DocAcceptedGradeManager.Count > 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                return;
            }
            DocAcceptedGradeManager[0].BeginEdit();
            DocAcceptedGradeManager[0]["GrdId"] = int.Parse(comboGrade.SelectedItem.Value.ToString());
            DocAcceptedGradeManager[0]["MjId"] = int.Parse(comboMajor.SelectedItem.Value.ToString());
            DocAcceptedGradeManager[0]["ResId"] = int.Parse(comboResponsblity.SelectedItem.Value.ToString());

            DocAcceptedGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocAcceptedGradeManager[0]["ModifiedDate"] = DateTime.Now;
            DocAcceptedGradeManager[0].EndEdit();

            if (DocAcceptedGradeManager.Save() <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            SetEditMode();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }

    protected void Insert()
    {

        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager1 = new TSP.DataManager.DocAcceptedGradeManager();
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            DataRow GradeMajorResRow = DocAcceptedGradeManager.NewRow();

            int GrdId = Convert.ToInt32(comboGrade.Value);
            int MjId = Convert.ToInt32(comboMajor.Value);
            int ResId = Convert.ToInt32(comboResponsblity.Value);
            DataTable dtAccGrd = DocAcceptedGradeManager1.SelectConfirmedGrade(MjId, GrdId, ResId);
            if (dtAccGrd.Rows.Count >= 1)
            {
                ShowMessage("اطلاعات وارد شده تکراری می باشد.");
                return;
            }
            GradeMajorResRow["GrdId"] = GrdId;
            GradeMajorResRow["MjId"] =MjId;
            GradeMajorResRow["ResId"] = ResId;
            GradeMajorResRow["InActive"] = 0;
            GradeMajorResRow["UserId"] = Utility.GetCurrentUser_UserId();
            GradeMajorResRow["ModifiedDate"] = DateTime.Now;

            DocAcceptedGradeManager.AddRow(GradeMajorResRow);
            if (DocAcceptedGradeManager.Save() > 0)
            {
                ShowMessage("ذخیره انجام شد.");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #endregion
}