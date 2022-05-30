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

public partial class Employee_Management_OrganizationInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OrgId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Organization.aspx");
                return;
            }


            TSP.DataManager.Permission per = TSP.DataManager.OrganizationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                OrganizationId.Value = Server.HtmlDecode(Request.QueryString["OrgId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OrgId = Utility.DecryptQS(OrganizationId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":

                    if (string.IsNullOrEmpty(OrgId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(OrgId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    ASPxRoundPanel2.Enabled = false;


                    break;


                case "New":

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;

                case "Edit":

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(OrgId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(OrgId));
                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    break;


            }


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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Organization.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ASPxRoundPanel2.Enabled = true;

        TSP.DataManager.Permission per = TSP.DataManager.OrganizationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        PgMode.Value = Utility.EncryptQS("Edit");
        ASPxRoundPanel2.HeaderText = "ویرایش";

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OrgId = Utility.DecryptQS(OrganizationId.Value);

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

                if (string.IsNullOrEmpty(OrgId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(int.Parse(OrgId));
                }

            }

        }

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.OrganizationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        OrganizationId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ASPxRoundPanel2.Enabled = true;
        ClearForm();

    }
    protected void ClearForm()
    {
        txtAddress.Text = "";
        txtDesc.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";
        txtMobileNo.Text = "";
        txtManagerName.Text = "";
        txtOrgName.Text = "";
        txtOrgNameEn.Text = "";
        txtTel.Text = "";
        txtWebsite.Text = "";
        txtOrgPO.Text = "";
        txtPostalCode.Text = "";

    }
    protected void FillForm(int OrgId)
    {
        TSP.DataManager.OrganizationManager Manager = new TSP.DataManager.OrganizationManager();
        try
        {
            Manager.FindByCode(OrgId);
            if (Manager.Count == 1)
            {
                txtAddress.Text = Manager[0]["Address"].ToString();
                txtDesc.Text = Manager[0]["Description"].ToString();
                txtFax.Text = Manager[0]["Fax"].ToString();
                txtEmail.Text = Manager[0]["Email"].ToString();
                txtMobileNo.Text = Manager[0]["MobileNo"].ToString();
                txtManagerName.Text = Manager[0]["ManagerName"].ToString();
                txtOrgName.Text = Manager[0]["OrgName"].ToString();
                txtOrgNameEn.Text = Manager[0]["OrgNameEn"].ToString();
                txtTel.Text = Manager[0]["Tel"].ToString();
                txtWebsite.Text = Manager[0]["WebSite"].ToString();
                txtOrgPO.Text = Manager[0]["OrgPO"].ToString();
                if (Utility.IsDBNullOrNullValue(Manager[0]["PostalCode"]))
                    txtPostalCode.Text = Manager[0]["PostalCode"].ToString();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امكان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطايى در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void Insert()
    {
        TSP.DataManager.OrganizationManager Manager = new TSP.DataManager.OrganizationManager();
        try
        {
            DataRow dr = Manager.NewRow();
            dr["OrgName"] = txtOrgName.Text;
            dr["OrgNameEn"] = txtOrgNameEn.Text;
            dr["ManagerName"] = txtManagerName.Text;
            dr["Tel"] = txtTel.Text;
            dr["Fax"] = txtFax.Text;
            dr["MobileNo"] = txtMobileNo.Text;
            dr["Email"] = txtEmail.Text;
            dr["WebSite"] = txtWebsite.Text;
            dr["Address"] = txtAddress.Text;
            dr["Description"] = txtDesc.Text;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            dr["Type"] = 0;
            if (!string.IsNullOrEmpty(txtOrgPO.Text))
                dr["OrgPO"] = txtOrgPO.Text;
            else
                dr["OrgPO"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtPostalCode.Text))
                dr["PostalCode"] = txtPostalCode.Text;
            else
                dr["PostalCode"] = DBNull.Value;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            Manager.AddRow(dr);
            if (Manager.Save() > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                PgMode.Value = Utility.EncryptQS("Edit");
                OrganizationId.Value = Utility.EncryptQS(Manager[0]["OrgId"].ToString());
                ASPxRoundPanel2.HeaderText = "ويرايش";
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
    protected void Edit(int OrgId)
    {
        TSP.DataManager.OrganizationManager Manager = new TSP.DataManager.OrganizationManager();
        try
        {
            Manager.FindByCode(OrgId);
            if (Manager.Count > 0)
            {
                Manager[0].BeginEdit();
                Manager[0]["OrgName"] = txtOrgName.Text;
                Manager[0]["OrgNameEn"] = txtOrgNameEn.Text;
                Manager[0]["ManagerName"] = txtManagerName.Text;
                Manager[0]["Tel"] = txtTel.Text;
                Manager[0]["Fax"] = txtFax.Text;
                Manager[0]["MobileNo"] = txtMobileNo.Text;
                Manager[0]["Email"] = txtEmail.Text;
                Manager[0]["WebSite"] = txtWebsite.Text;
                Manager[0]["Address"] = txtAddress.Text;
                Manager[0]["Description"] = txtDesc.Text;
                if (!string.IsNullOrEmpty(txtOrgPO.Text))
                    Manager[0]["OrgPO"] = txtOrgPO.Text;
                if (!string.IsNullOrEmpty(txtPostalCode.Text))
                    Manager[0]["PostalCode"] = txtPostalCode.Text;
                Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                Manager[0].EndEdit();
                if (Manager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
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
                this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                return;
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

}
