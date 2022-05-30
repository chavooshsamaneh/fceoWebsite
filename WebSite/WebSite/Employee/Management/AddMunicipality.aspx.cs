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

public partial class Employee_Management_AddMunicipality : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            HiddenFieldMunipality["CitId"] = "";
            if (Request.QueryString["MunId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("Municipality.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanNew;
            btnEdit2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanView;
            btnSave2.Enabled = per.CanView;
            SetKeys();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Municipality.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldMunipality["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            string MunId = Utility.DecryptQS(HiddenFieldMunipality["MunId"].ToString());

            if (string.IsNullOrEmpty(MunId) && PageMode != "New")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (PageMode == "New")
            {
                Insert();
            }
            else if (PageMode == "Edit")
            {
                Edit(int.Parse(MunId));
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        HiddenFieldMunipality["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldMunipality["MunId"] = "";
        ClearForm();
        EnableControls();
        RoundPanelMunipality.HeaderText = "جدید";
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        HiddenFieldMunipality["PageMode"] = Utility.EncryptQS("Edit");

        EnableControls();
        RoundPanelMunipality.HeaderText = "ویرایش";
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void GridViewCity_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (GridViewCity.JSProperties["cpSetObjds"].ToString() == "1")
        {
            if (HiddenFieldMunipality["CitId"] != null)
                ObjdsMun.SelectParameters[0].DefaultValue = HiddenFieldMunipality["CitId"].ToString();
            GridViewCity.JSProperties["cpSetObjds"] = 2;
        }
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldMunipality["MunId"] = Request.QueryString["MunId"].ToString();
        HiddenFieldMunipality["PageMode"] = Request.QueryString["PgMd"];
        int MunId = int.Parse(Utility.DecryptQS(HiddenFieldMunipality["MunId"].ToString()));

        string PageMode = Utility.DecryptQS(HiddenFieldMunipality["PageMode"].ToString());

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
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;


        if (HiddenFieldMunipality["MunId"] == null || (string.IsNullOrEmpty(HiddenFieldMunipality["MunId"].ToString())))
        {
            Response.Redirect("Municipality.aspx");
            return;
        }
        int ControlerId = int.Parse(Utility.DecryptQS(HiddenFieldMunipality["MunId"].ToString()));
        FillForm(ControlerId);

        RoundPanelMunipality.HeaderText = "مشاهده";
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        RoundPanelMunipality.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (HiddenFieldMunipality["MunId"] == null || string.IsNullOrEmpty(HiddenFieldMunipality["MunId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int MunId = int.Parse(Utility.DecryptQS(HiddenFieldMunipality["MunId"].ToString()));

        EnableControls();
        FillForm(MunId);
        RoundPanelMunipality.HeaderText = "ویرایش";
    }

    private void FillForm(int MunId)
    {
        TSP.DataManager.TechnicalServices.MunicipalityManager MunicipalityManager = new TSP.DataManager.TechnicalServices.MunicipalityManager();
        MunicipalityManager.FindByCode(MunId);
        if (MunicipalityManager.Count == 1)
        {
            txtCity.Text = MunicipalityManager[0]["CitName"].ToString();
            txtDescription.Text = MunicipalityManager[0]["Description"].ToString();
            if (!Utility.IsDBNullOrNullValue(MunicipalityManager[0]["ParentId"]))
            {
                txtMunicipality.Text = MunicipalityManager[0]["ParentName"].ToString();
                HiddenFieldMunipality["ParentId"] = MunicipalityManager[0]["ParentId"].ToString();
            }
            txtMunName.Text = MunicipalityManager[0]["MunName"].ToString();
            HiddenFieldMunipality["CitId"] = MunicipalityManager[0]["CitId"].ToString();
            if (HiddenFieldMunipality["CitId"] != null)
                ObjdsMun.SelectParameters[0].DefaultValue = HiddenFieldMunipality["CitId"].ToString();
            GridViewCity.JSProperties["cpSetObjds"] = 2;
        }
    }

    private void ClearForm()
    {
        txtCity.Text = "";
        txtDescription.Text = "";
        txtMunicipality.Text = "";
        txtMunName.Text = "";
    }

    private void EnableControls()
    {
        txtCity.Enabled = true;
        txtDescription.Enabled = true;
        txtMunicipality.Enabled = true;
        txtMunName.Enabled = true;
    }

    private void DisableControls()
    {
        txtCity.Enabled = false;
        txtDescription.Enabled = false;
        txtMunicipality.Enabled = false;
        txtMunName.Enabled = false;
    }

    private void Insert()
    {
        TSP.DataManager.TechnicalServices.MunicipalityManager MunicipalityManager = new TSP.DataManager.TechnicalServices.MunicipalityManager();
        try
        {

            DataRow MunRow = MunicipalityManager.NewRow();
            MunRow["MunName"] = txtMunName.Text;
            if (HiddenFieldMunipality["CitId"] != null && !string.IsNullOrEmpty(HiddenFieldMunipality["CitId"].ToString()))
                MunRow["CitId"] = HiddenFieldMunipality["CitId"].ToString();
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            if (!string.IsNullOrEmpty(txtMunicipality.Text))
                if (HiddenFieldMunipality["ParentId"] != null && !string.IsNullOrEmpty(HiddenFieldMunipality["ParentId"].ToString()))
                    MunRow["ParentId"] = HiddenFieldMunipality["ParentId"].ToString();
            MunRow["Description"] = txtDescription.Text;
            MunRow["InActive"] = 0;
            MunRow["UserId"] = Utility.GetCurrentUser_UserId();
            MunRow["ModifiedDate"] = DateTime.Now;

            MunicipalityManager.AddRow(MunRow);
            if (MunicipalityManager.Save() > 0)
            {
                HiddenFieldMunipality["MunId"] = Utility.EncryptQS(MunicipalityManager[0]["MunId"].ToString());
                HiddenFieldMunipality["PageMode"] = Utility.EncryptQS("Edit");

                this.ViewState["BtnNew"] = BtnNew.Enabled;
                //this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["btnSave"] = btnSave.Enabled;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Edit(int MunId)
    {
        TSP.DataManager.TechnicalServices.MunicipalityManager MunicipalityManager = new TSP.DataManager.TechnicalServices.MunicipalityManager();
        try
        {
            MunicipalityManager.FindByCode(MunId);
            if (MunicipalityManager.Count == 1)
            {
                MunicipalityManager[0].BeginEdit();

                MunicipalityManager[0]["MunName"] = txtMunName.Text;
                if (HiddenFieldMunipality["CitId"] != null && !string.IsNullOrEmpty(HiddenFieldMunipality["CitId"].ToString()))
                    MunicipalityManager[0]["CitId"] = HiddenFieldMunipality["CitId"].ToString();
                //if (!string.IsNullOrEmpty(txtMunicipality.Text))
                if (HiddenFieldMunipality["ParentId"] != null && !string.IsNullOrEmpty(HiddenFieldMunipality["ParentId"].ToString()))
                    MunicipalityManager[0]["ParentId"] = HiddenFieldMunipality["ParentId"].ToString();
                MunicipalityManager[0]["Description"] = txtDescription.Text;
                MunicipalityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MunicipalityManager[0]["ModifiedDate"] = DateTime.Now;

                MunicipalityManager[0].EndEdit();
                if (MunicipalityManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    #endregion


}
