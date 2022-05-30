using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_BaseInfo_AddSetting : System.Web.UI.Page
{
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldModeID["PgMode"].ToString());
        }
        set
        {
            HiddenFieldModeID["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int SettingId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldModeID["SettingId"].ToString()));
        }
        set
        {
            HiddenFieldModeID["SettingId"] = Utility.EncryptQS(value.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.SettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            BtnEdit.Enabled = per.CanEdit;
            BtnEdit2.Enabled = per.CanEdit;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnEdit"] = BtnEdit.Enabled;
            SetKeys();
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.BtnEdit.Enabled = this.BtnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(SettingId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.TechnicalServices.SettingManager SettingManager = new TSP.DataManager.TechnicalServices.SettingManager();
        SettingManager.FindByCode(SettingId);
        if (SettingManager.Count == 1)
        {
            if (Convert.ToBoolean(SettingManager[0]["InActive"]))
            {
                SetLabelWarning("امکان ویرایش رکورد غیرفعال وجود ندارد");
                return;
            }
        }

        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        switch (PageMode)
        {
            case "Edit":
                Edit();
                break;
            case "New":
                Insert();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Setting.aspx");
    }

    private void SetKeys()
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                SettingId = int.Parse(Utility.DecryptQS(Request.QueryString["SettingId"].ToString()));
                PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                SetMode();
            }
            else
            {
                Response.Redirect("Setting.aspx");
            }
        }
        catch
        {
            Response.Redirect("Setting.aspx");
        }
    }

    void SetMode()
    {
        if (Utility.IsDBNullOrNullValue(PageMode) || Utility.IsDBNullOrNullValue(SettingId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        switch (PageMode)
        {
            case "View":
                SetViewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
            case "New":
                SetNewMode();
                break;
        }

        this.ViewState["btnsave"] = btnSave.Enabled;
    }

    void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.SettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
          (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if ((!per.CanNew) && (newb))
            newb = false;
        if ((!per.CanEdit) && (editb))
            editb = false;

        btnSave.Enabled = btnSave2.Enabled = saveb;
        BtnNew.Enabled = BtnNew2.Enabled = newb;
        BtnEdit.Enabled = BtnEdit2.Enabled = editb;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = BtnEdit.Enabled;
    }

    void ClearForm()
    {
        txtFoundation.Text = string.Empty;
        txtStep.Text = string.Empty;
    }

    void SetNewMode()
    {
        PageMode = "New";
        SettingId = -1;
        ClearForm();
        SetButtoms(false, false, true);
        RoundPanelMain.HeaderText = "جدید";
        RoundPanelMain.Enabled = true;
    }

    void SetEditMode()
    {
        PageMode = "Edit";
        FillForm();
        SetButtoms(true, false, true);
        RoundPanelMain.HeaderText = "ویرایش";
        RoundPanelMain.Enabled = true;
    }

    void SetViewMode()
    {
        PageMode = "View";
        FillForm();
        SetButtoms(true, true, false);
        RoundPanelMain.HeaderText = "مشاهده";
        RoundPanelMain.Enabled = false;
    }

    void FillForm()
    {
        if (Utility.IsDBNullOrNullValue(SettingId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.TechnicalServices.SettingManager SettingManager = new TSP.DataManager.TechnicalServices.SettingManager();
        SettingManager.FindByCode(SettingId);
        if (SettingManager.Count == 1)
        {
            txtFoundation.Text = SettingManager[0]["Foundation"].ToString();
            txtStep.Text = SettingManager[0]["Step"].ToString();
            bool IsNeed5In1000Fish = Convert.ToBoolean(SettingManager[0]["IsNeed5In1000Fish"]);
            chkIsNeed5In1000.Checked = IsNeed5In1000Fish;
            if (IsNeed5In1000Fish)
            {
                lblFoundation.ClientEnabled = true;
                lblStep.ClientEnabled = true;
                txtFoundation.ClientEnabled = true;
                txtStep.ClientEnabled = true;
            }
            else
            {
                lblFoundation.ClientEnabled = false;
                lblStep.ClientEnabled = false;
                txtFoundation.ClientEnabled = false;
                txtStep.ClientEnabled = false;
            }
        }
        else
            SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
    }

    private void Insert()
    {
        TSP.DataManager.TechnicalServices.SettingManager SettingManager = new TSP.DataManager.TechnicalServices.SettingManager();

        SettingManager.FindByInActive(0);
        if (SettingManager.Count > 0)
        {
            for (int i = 0; i < SettingManager.Count; i++)
            {
                SettingManager[i].BeginEdit();
                SettingManager[i]["InActive"] = 1;
                SettingManager[i]["InActiveDate"] = Utility.GetDateOfToday();
                SettingManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                SettingManager[i]["ModifiedDate"] = DateTime.Now;
                SettingManager[i].EndEdit();
            }
            SettingManager.Save();
        }

        try
        {
            DataRow dr = SettingManager.NewRow();
            dr["Foundation"] = txtFoundation.Text;
            dr["Step"] = txtStep.Text;
            dr["IsNeed5In1000Fish"] = chkIsNeed5In1000.Checked ? 1 : 0;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            SettingManager.AddRow(dr);
            if (SettingManager.Save() > 0)
            {
                SettingId = Convert.ToInt32(SettingManager[SettingManager.Count - 1]["SettingId"]);
                SetLabelWarning("ذخیره انجام شد");
                SetEditMode();
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Edit()
    {
        if (Utility.IsDBNullOrNullValue(SettingId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.TechnicalServices.SettingManager SettingManager = new TSP.DataManager.TechnicalServices.SettingManager();
        SettingManager.FindByCode(SettingId);
        if (SettingManager.Count != 1)
        {
            SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            return;
        }

        if (Convert.ToBoolean(SettingManager[0]["InActive"]))
        {
            SetLabelWarning("امکان ویرایش رکورد غیرفعال وجود ندارد");
            return;
        }

        try
        {
            SettingManager[0].BeginEdit();
            SettingManager[0]["Foundation"] = txtFoundation.Text;
            SettingManager[0]["Step"] = txtStep.Text;
            SettingManager[0]["IsNeed5In1000Fish"] = chkIsNeed5In1000.Checked ? 1 : 0;
            SettingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            SettingManager[0]["ModifiedDate"] = DateTime.Now;
            SettingManager[0].EndEdit();
            if (SettingManager.Save() > 0)
            {
                SetLabelWarning("ذخیره انجام شد");
                SetEditMode();
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void SetError(Exception err)
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
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
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
}