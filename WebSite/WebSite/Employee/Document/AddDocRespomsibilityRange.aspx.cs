using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Document_AddDocRespomsibilityRange : System.Web.UI.Page
{
    /// <summary>
    /// Get Real Data
    /// </summary>
    private string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldResRange["PgMd"].ToString());
        }
        set
        {
            HiddenFieldResRange["PgMd"] = Utility.EncryptQS(value.ToString());
        }
    }

    /// <summary>
    /// Get Real Data
    /// </summary>
    private int ResRangeId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldResRange["ResRangeId"].ToString()));
        }
        set
        {
            HiddenFieldResRange["ResRangeId"] = Utility.EncryptQS(value.ToString());
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
            HiddenFieldResRange["ResRangeName"] = "";
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

        ResRangeId = -1;
        PageMode = "New";
        RoundPanelResRange.HeaderText = "جدید";
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
                if (Utility.IsDBNullOrNullValue(ResRangeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(ResRangeId);
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DocRespomsibilityRange.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(ResRangeId))
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
                RoundPanelResRange.HeaderText = "ویرایش";
            }
        }
    }
    #endregion

    #region Methods
    private TSP.DataManager.Permission GetPermission()
    {
        return TSP.DataManager.DocResponsibilityRangeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["RgId"]))
        {
            Response.Redirect("DocRespomsibilityRange.aspx");
        }

        try
        {
            PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            ResRangeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["RgId"]));
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
        RoundPanelResRange.HeaderText = "جدید";
        ClearForm();
    }

    private void SetEditMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        if (Utility.IsDBNullOrNullValue(ResRangeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        FillForm(ResRangeId);
        RoundPanelResRange.Enabled = true;
        RoundPanelResRange.HeaderText = "ویرایش";
    }

    private void SetViewMode()
    {
        SetEnable(false);
        if (Utility.IsDBNullOrNullValue(ResRangeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.Permission per = GetPermission();
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        FillForm(ResRangeId);
        RoundPanelResRange.HeaderText = "مشاهده";
    }

    protected void ClearForm()
    {
        txtDescription.Text = "";
        txtResRangeName.Text = "";
        txtResRangeTextOnCard.Text = "";
        checkboxIsGradPrint.Checked = false;
    }

    protected void SetEnable(Boolean Enable)
    {
        txtDescription.Enabled = Enable;
        txtResRangeName.Enabled = Enable;
        txtResRangeTextOnCard.Enabled = Enable;
        checkboxIsGradPrint.Enabled = Enable;
    }

    protected void FillForm(int ResRangeId)
    {
        TSP.DataManager.DocResponsibilityRangeManager DocResponsibilityRangeManager = new TSP.DataManager.DocResponsibilityRangeManager();
        DocResponsibilityRangeManager.FindByCode(ResRangeId);
        if (DocResponsibilityRangeManager.Count == 1)
        {
            txtDescription.Text = DocResponsibilityRangeManager[0]["ResRangeDes"].ToString();
            txtResRangeName.Text = DocResponsibilityRangeManager[0]["ResRangeName"].ToString();
            txtResRangeTextOnCard.Text = DocResponsibilityRangeManager[0]["ResRangeTextOnCard"].ToString();
            HiddenFieldResRange["ResRangeName"] = DocResponsibilityRangeManager[0]["ResRangeName"].ToString();
            checkboxIsGradPrint.Checked = Convert.ToBoolean(DocResponsibilityRangeManager[0]["IsGradePrint"]);
        }
    }

    protected void Edit(int ResRangeId)
    {
        try
        {
            TSP.DataManager.DocResponsibilityRangeManager DocResponsibilityRangeManager = new TSP.DataManager.DocResponsibilityRangeManager();            
            if (HiddenFieldResRange["ResRangeName"].ToString() != txtResRangeName.Text.Trim())
            {
                DocResponsibilityRangeManager.FindByName(txtResRangeName.Text.Trim());
                if (DocResponsibilityRangeManager.Count > 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                    return;
                }
            }
            //DocResponsibilityRangeManager.DataTable.Clear();
            DocResponsibilityRangeManager.FindByCode(ResRangeId);
            if (DocResponsibilityRangeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            DocResponsibilityRangeManager[0].BeginEdit();
            DocResponsibilityRangeManager[0]["ResRangeName"] = txtResRangeName.Text;
            DocResponsibilityRangeManager[0]["ResRangeDes"] = txtDescription.Text;
            DocResponsibilityRangeManager[0]["ResRangeTextOnCard"] = txtResRangeTextOnCard.Text;
            DocResponsibilityRangeManager[0]["IsGradePrint"] = checkboxIsGradPrint.Checked;
            DocResponsibilityRangeManager[0]["ModifiedDate"] = DateTime.Now;            
            DocResponsibilityRangeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocResponsibilityRangeManager[0].EndEdit();
            DocResponsibilityRangeManager.Save();
            HiddenFieldResRange["ResRangeName"] = txtResRangeName.Text;
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
        try
        {
            TSP.DataManager.DocResponsibilityRangeManager DocResponsibilityRangeManager = new TSP.DataManager.DocResponsibilityRangeManager();
            DocResponsibilityRangeManager.FindByName(txtResRangeName.Text.Trim());
            if (DocResponsibilityRangeManager.Count > 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                return;
            }
            DataRow dr = DocResponsibilityRangeManager.NewRow();
            dr["ResRangeName"] = txtResRangeName.Text;
            dr["ResRangeDes"] = txtDescription.Text;
            dr["ResRangeTextOnCard"] = txtResRangeTextOnCard.Text;            
            dr["IsGradePrint"] = checkboxIsGradPrint.Checked;
            dr["ModifiedDate"] = DateTime.Now;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            DocResponsibilityRangeManager.AddRow(dr);
            DocResponsibilityRangeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            ResRangeId = Convert.ToInt32(DocResponsibilityRangeManager[DocResponsibilityRangeManager.Count - 1]["ResRangeId"]);
            HiddenFieldResRange["ResRangeName"] = txtResRangeName.Text;
            PageMode = "Edit";
            SetEditMode();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #endregion
}