using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_HomePage_AddRulesType : System.Web.UI.Page
{
    /// <summary>
    /// Get Real Data
    /// </summary>
    private string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldFormType["PgMd"].ToString());
        }
        set
        {
            HiddenFieldFormType["PgMd"] = Utility.EncryptQS(value.ToString());
        }
    }

    /// <summary>
    /// Get Real Data
    /// </summary>
    private int RulesTypeId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldFormType["FtId"].ToString()));
        }
        set
        {
            HiddenFieldFormType["FtId"] = Utility.EncryptQS(value.ToString());
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
            HiddenFieldFormType["OriginTypeName"] = "";
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

        RulesTypeId = -1;
        PageMode = "New";
        RoundPanelFormType.HeaderText = "جدید";
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
                if (Utility.IsDBNullOrNullValue(RulesTypeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(RulesTypeId);
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RulsType.aspx?GrdFlt=" + Request.QueryString["GrdFlt"]);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (Utility.IsDBNullOrNullValue(RulesTypeId))
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
                RoundPanelFormType.HeaderText = "ویرایش";
            }
        }
    }
    #endregion

    #region Method

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private TSP.DataManager.Permission GetPermission()
    {
        return TSP.DataManager.RulesTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    }
    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["FtId"]))
        {
            Response.Redirect("RulsType.aspx");
        }

        try
        {
            PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            RulesTypeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["FtId"]));
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

    protected void SetEnable(Boolean Enable)
    {
        txtDescription.Enabled = Enable;
        txtRoleTypeName.Enabled = Enable;
    }

    protected void ClearForm()
    {
        txtDescription.Text = "";
        txtRoleTypeName.Text = "";
    }

    protected void FillForm(int RulesTypeId)
    {
        TSP.DataManager.RulesTypeManager RulesTypeManager = new TSP.DataManager.RulesTypeManager();
        RulesTypeManager.FindByCode(RulesTypeId);
        if (RulesTypeManager.Count == 1)
        {
            txtDescription.Text = RulesTypeManager[0]["Description"].ToString();
            txtRoleTypeName.Text = RulesTypeManager[0]["RuleTypeName"].ToString();
            HiddenFieldFormType["OriginTypeName"] = RulesTypeManager[0]["RuleTypeName"].ToString();
        }
    }

    private void SetNewMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelFormType.HeaderText = "جدید";
        ClearForm();
    }

    private void SetEditMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        if (Utility.IsDBNullOrNullValue(RulesTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        FillForm(RulesTypeId);
        RoundPanelFormType.Enabled = true;
        RoundPanelFormType.HeaderText = "ویرایش";
    }

    private void SetViewMode()
    {
        SetEnable(false);
        if (Utility.IsDBNullOrNullValue(RulesTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.Permission per = GetPermission();
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        FillForm(RulesTypeId);
        RoundPanelFormType.HeaderText = "مشاهده";
    }

    private void CheckTablePermissions()
    {
        TSP.DataManager.Permission per = GetPermission();
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
    }

    protected void Edit(int FoId)
    {
        try
        {
            TSP.DataManager.RulesTypeManager RulesTypeManager = new TSP.DataManager.RulesTypeManager();
            if (HiddenFieldFormType["OriginTypeName"].ToString() != txtRoleTypeName.Text.Trim())
            {
                RulesTypeManager.FindByName(txtRoleTypeName.Text.Trim());
                if (RulesTypeManager.Count > 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                    return;
                }
            }
            RulesTypeManager.FindByCode(RulesTypeId);
            if (RulesTypeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            RulesTypeManager[0].BeginEdit();
            RulesTypeManager[0]["RuleTypeName"] = txtRoleTypeName.Text;
            RulesTypeManager[0]["Description"] = txtDescription.Text;
            RulesTypeManager[0]["ModifiedDate"] = DateTime.Now;
            RulesTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            RulesTypeManager[0].EndEdit();
            RulesTypeManager.Save();
            HiddenFieldFormType["OriginTypeName"] = txtRoleTypeName.Text;
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
            TSP.DataManager.RulesTypeManager RulesTypeManager = new TSP.DataManager.RulesTypeManager();
            RulesTypeManager.FindByName(txtRoleTypeName.Text.Trim());
            if (RulesTypeManager.Count > 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                return;
            }
            DataRow dr = RulesTypeManager.NewRow();
            dr["RuleTypeName"] = txtRoleTypeName.Text;
            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            RulesTypeManager.AddRow(dr);
            RulesTypeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            RulesTypeId = Convert.ToInt32(RulesTypeManager[RulesTypeManager.Count - 1]["RuleTypeId"]);
            HiddenFieldFormType["OriginTypeName"] = txtRoleTypeName.Text;
            PageMode = "Edit";
            SetEditMode();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}