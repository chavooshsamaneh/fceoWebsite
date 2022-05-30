using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_HomePage_AddFormsType : System.Web.UI.Page
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
    private int FormTypeId
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

        FormTypeId = -1;
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
                if (Utility.IsDBNullOrNullValue(FormTypeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(FormTypeId);
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormsType.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(FormTypeId))
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

    #region Methods
    private TSP.DataManager.Permission GetPermission()
    {
        return TSP.DataManager.FormsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["FtId"]))
        {
            Response.Redirect("FormsType.aspx");
        }

        try
        {
            PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            FormTypeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["FtId"]));
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
        RoundPanelFormType.HeaderText = "جدید";
        ClearForm();
    }

    private void SetEditMode()
    {
        SetEnable(true);
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        if (Utility.IsDBNullOrNullValue(FormTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        FillForm(FormTypeId);
        RoundPanelFormType.Enabled = true;
        RoundPanelFormType.HeaderText = "ویرایش";
    }

    private void SetViewMode()
    {
        SetEnable(false);
        if (Utility.IsDBNullOrNullValue(FormTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.Permission per = GetPermission();
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        FillForm(FormTypeId);
        RoundPanelFormType.HeaderText = "مشاهده";
    }

    protected void ClearForm()
    {
        txtDescription.Text = "";
        txtFormTypeName.Text = "";
    }

    protected void SetEnable(Boolean Enable)
    {
        txtDescription.Enabled = Enable;
        txtFormTypeName.Enabled = Enable;
    }

    protected void FillForm(int FormTypeId)
    {
        TSP.DataManager.FormsTypeManager FormsTypeManager = new TSP.DataManager.FormsTypeManager();
        FormsTypeManager.FindByCode(FormTypeId);
        if (FormsTypeManager.Count == 1)
        {
            txtDescription.Text = FormsTypeManager[0]["Description"].ToString();
            txtFormTypeName.Text = FormsTypeManager[0]["FormTypeName"].ToString();
            HiddenFieldFormType["OriginTypeName"] = FormsTypeManager[0]["FormTypeName"].ToString();
        }
    }

    protected void Edit(int FoId)
    {
        try
        {
            TSP.DataManager.FormsTypeManager FormsTypeManager = new TSP.DataManager.FormsTypeManager();
            if (HiddenFieldFormType["OriginTypeName"].ToString() != txtFormTypeName.Text.Trim())
            {
                FormsTypeManager.FindByName(txtFormTypeName.Text.Trim());
                if (FormsTypeManager.Count > 0)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                    return;
                }
            }
            FormsTypeManager.FindByCode(FormTypeId);
            if (FormsTypeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            FormsTypeManager[0].BeginEdit();
            FormsTypeManager[0]["FormTypeName"] = txtFormTypeName.Text;
            FormsTypeManager[0]["Description"] = txtDescription.Text;
            FormsTypeManager[0]["ModifiedDate"] = DateTime.Now;
            FormsTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            FormsTypeManager[0].EndEdit();
            FormsTypeManager.Save();
            HiddenFieldFormType["OriginTypeName"] = txtFormTypeName.Text;
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
            TSP.DataManager.FormsTypeManager FormsTypeManager = new TSP.DataManager.FormsTypeManager();
            FormsTypeManager.FindByName(txtFormTypeName.Text.Trim());
            if (FormsTypeManager.Count > 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
                return;
            }
            DataRow dr = FormsTypeManager.NewRow();
            dr["FormTypeName"] = txtFormTypeName.Text;
            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            FormsTypeManager.AddRow(dr);
            FormsTypeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            FormTypeId = Convert.ToInt32(FormsTypeManager[FormsTypeManager.Count - 1]["FormTypeId"]);
            HiddenFieldFormType["OriginTypeName"] = txtFormTypeName.Text;
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