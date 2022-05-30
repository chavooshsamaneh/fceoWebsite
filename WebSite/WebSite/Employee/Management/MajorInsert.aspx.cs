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

public partial class Employee_Management_MajorInsert : System.Web.UI.Page
{
    int majorId;
    String PageMode;
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");



        if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) ||
            (string.IsNullOrEmpty(Request.QueryString["MjId"])))
        {
            Response.Redirect("Major.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {

            HiddenFieldMajor["PageMode"] = "";
            HiddenFieldMajor["MjId"] = "";
            HiddenFieldMajor["NewMode"] = Utility.EncryptQS("New");

            TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    private void EditMajor()
    {
        String majorCode = textmjCode.Text;
        String majorName = txtmjname.Text;
        cmbMajor.DataBind();


        Object ReId = (cmbMajor.SelectedItem.Value != null) ? cmbMajor.SelectedItem.Value : DBNull.Value;


        TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();
        majorManager.FindByCode(majorId);

        if (majorManager.Count == 1)
        {
            try
            {
                majorManager[0].BeginEdit();
                majorManager[0]["MjCode"] = textmjCode.Text;
                majorManager[0]["MjName"] = txtmjname.Text;
                majorManager[0]["IsMaster"] = chemajor.Checked;
                majorManager[0]["Description"] = txtmjdes.Text;
                //cmbMajor.DataBind();
                if (!Utility.IsDBNullOrNullValue(cmbMajor.SelectedItem))
                    majorManager[0]["ParentId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());
                majorManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                majorManager[0]["ModifiedDate"] = DateTime.Now;
                majorManager[0].EndEdit();

                if (majorManager.Save() == 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            catch (Exception err)
            {
                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    }
                    else if (se.Number == 2627)
                    {
                        this.DivReport.Visible = true;
                        if (err.Message.Contains("IX_tblCity_1"))
                            this.LabelWarning.Text = "نام شهر تکراری است";
                        else
                            this.LabelWarning.Text = "اطلاعات تکراری است";
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
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
        }

    }

    private void Insertmajor()
    {
        try
        {
            TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();
            DataRow dr = majorManager.NewRow();

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["MjCode"] = textmjCode.Text;
            dr["MjName"] = txtmjname.Text;
            if (chemajor.Checked == true)
            {
                dr["IsMaster"] = true;

            }
            else
            {
                dr["IsMaster"] = false;

            }
            dr["Description"] = txtmjdes.Text;
            if (cmbMajor.SelectedItem.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "زیر شاخه را انتخاب نمایید.";
                return;
            }
            else
            {
                dr["ParentId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());
            }

            dr["ModifiedDate"] = DateTime.Now;

            majorManager.AddRow(dr);
            int count = majorManager.Save();
            if (count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                RoundPanelRequest.HeaderText = "ویرایش";
                // txtmjname.Text = String.Empty;
                //txtmjdes.Text = String.Empty;
                //textmjCode.Text = String.Empty;
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                if (se.Number == 2627)
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
    }

    private void SetKeys()
    {

        HiddenFieldMajor["MjId"] = Server.HtmlDecode(Request.QueryString["MjId"]);
        HiddenFieldMajor["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"]);
        majorId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMajor["MjId"].ToString()));
        PageMode = Utility.DecryptQS(HiddenFieldMajor["PageMode"].ToString());
        if (String.IsNullOrEmpty(PageMode))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
    }

    private void SetMode(String PageMode)
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
        TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        txtmjdes.Enabled = false;
        txtmjname.Enabled = false;
        textmjCode.Enabled = false;
        cmbMajor.Enabled = false;
        chemajor.Enabled = false;



        if (String.IsNullOrEmpty(HiddenFieldMajor["MjId"].ToString()))
        {
            Response.Redirect("Default6.aspx");
        }
        int majorId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMajor["MjId"].ToString()));
        FillForm(majorId);
        RoundPanelRequest.HeaderText = "مشاهده";
    }

    private void FillForm(int majorid)
    {

        TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();
        majorManager.FindByCode(majorid);
        if (majorManager.Count == 1)
        {
            textmjCode.Text = majorManager[0]["MjCode"].ToString();
            txtmjname.Text = majorManager[0]["MjName"].ToString();
            txtmjdes.Text = majorManager[0]["Description"].ToString();
            chemajor.Checked = Convert.ToBoolean(majorManager[0]["IsMaster"]);
            if (!Utility.IsDBNullOrNullValue(majorManager[0]["ParentId"].ToString()))
            {
                int parentId = Convert.ToInt32(majorManager[0]["ParentId"].ToString());

                if (parentId > 0)
                {
                    cmbMajor.DataBind();
                    cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(majorManager[0]["ParentId"]).Index;
                }
                else
                {
                    chemajor.Checked = true;
                }
            }

            else
            {
                LabelWarning.Text = "error";
            }

        }
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        btnNew.Enabled = btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        textmjCode.Enabled = true;
        ClearForm();
        RoundPanelRequest.HeaderText = "جدید";
    }

    private void ClearForm()
    {
        txtmjname.Text = String.Empty;
        txtmjdes.Text = String.Empty;
        textmjCode.Text = String.Empty;
        cmbMajor.SelectedIndex = -1;
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();

        if (string.IsNullOrEmpty(HiddenFieldMajor["MjId"].ToString()))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        EnableControls();

        textmjCode.Enabled = false;
        int majorId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMajor["MjId"].ToString()));
        FillForm(majorId);
        RoundPanelRequest.HeaderText = "ویرایش";
    }

    private void EnableControls()
    {
        txtmjdes.Enabled = true;
        txtmjname.Enabled = true;
        textmjCode.Enabled = true;
        cmbMajor.Enabled = true;
        chemajor.Enabled = true;


    }

    private void Populate_Province()
    {
        if (!Utility.IsDBNullOrNullValue(cmbMajor.SelectedItem))
            ObjectDataSourceDropDown.SelectParameters["ParentId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(cmbMajor.SelectedItem.Value)) ? cmbMajor.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceDropDown.SelectParameters["ParentId"].DefaultValue = "-1";

        cmbMajor.DataBind();

    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Major.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        int MjId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMajor["MjId"].ToString()));
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        MajorManager.FindByCode(MjId);
        if (MajorManager.Count == 1)
        {
            int parentid = Convert.ToInt32(MajorManager[0]["ParentId"].ToString());
            if (parentid == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش رشته های اصلی وجود ندارد.";
            }
            else
            {

                TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;


                HiddenFieldMajor["PageMode"] = Utility.EncryptQS("Edit");
                EnableControls();
                RoundPanelRequest.HeaderText = "ویرایش";
                this.ViewState["BtnNew"] = btnNew.Enabled;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }
        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        HiddenFieldMajor["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldMajor["MjId"] = "";

        ClearForm();
        EnableControls();
        RoundPanelRequest.HeaderText = "جدید";
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldMajor["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                Insertmajor();
                break;
            case "Edit":
                if (String.IsNullOrEmpty(HiddenFieldMajor["MjId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    majorId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMajor["MjId"].ToString()));
                    EditMajor();
                }
                break;
        }
    }
    protected void cmbMajor_DataBound(object sender, EventArgs e)
    {
        if (cmbMajor.Items.Count > 0)
        {
            if (cmbMajor.Items[0].Text != "-------------")
                cmbMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
        }
        else
        {
            cmbMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
            cmbMajor.Text = "-------------";
        }
    }
    protected void CallbackPanelMajor_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameters = e.Parameter.Split(';');

        if (parameters[0] == "ParentId")
        {
            Populate_Province();
        }

    }

}
