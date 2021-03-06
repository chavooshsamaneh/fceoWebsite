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

public partial class Accounting_Users_AgentInsert : System.Web.UI.Page
{
    string PageMode;
    string AgentId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.AccountingAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["AgentId"])) || (!per.CanView))
            {
                Response.Redirect("Agent.aspx");
                return;
            }

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        switch (PageMode)
        {
            case "New":
                InsertAgent();
                break;
            case "Edit":
                UpdateAgent();
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteAgent();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agent.aspx");
    }

    private void InsertAgent()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();
        DataRow rowAgent = AgentManager.NewRow();
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        trans.Add(AgentManager);
        trans.Add(TelManager);

        try
        {
            trans.BeginSave();
            rowAgent.BeginEdit();

            rowAgent["Name"] = txtName.Text;
            rowAgent["Address"] = txtAddress.Text;
            rowAgent["PermissionDate"] = txtDate.Text;
            rowAgent["Email"] = txtEmail.Text;
            rowAgent["MobileNo"] = txtMobileNo.Text;
            rowAgent["PermissionNo"] = txtPerNo.Text;
            rowAgent["Website"] = txtWebsite.Text;
            rowAgent["CreateDate"] = PerDate;
            rowAgent["AgentCode"] = txtAgentCode.Text;
            rowAgent["Description"] = txtDescription.Html;
            rowAgent["UserId"] = Utility.GetCurrentUser_UserId();
            rowAgent["ModifiedDate"] = DateTime.Now;

            rowAgent["TaxOfficeName"] = txtTaxOfficeName.Text;
            rowAgent["TaxOfficeTell"] = txtTaxOfficeTell.Text;
            rowAgent["TaxOfficeAdress"] = txtTaxOfficeAdress.Text;

            rowAgent.EndEdit();

            AgentManager.AddRow(rowAgent);

            int cn = AgentManager.Save();
            if (cn == 1)
            {
                AgentManager.DataTable.AcceptChanges();
                AgentId = AgentManager[0]["AgentId"].ToString();

                DataRow drTel = TelManager.NewRow();
                drTel["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                drTel["TtId"] = AgentId;
                drTel["Kind"] = 0;
                drTel["Number"] = txtTel.Text;
                drTel["InActive"] = 0;
                drTel["UserId"] = Utility.GetCurrentUser_UserId();
                drTel["ModifiedDate"] = DateTime.Now;
                TelManager.AddRow(drTel);

                DataRow drFax = TelManager.NewRow();
                drFax["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                drFax["TtId"] = AgentId;
                drFax["Kind"] = 1;
                drFax["Number"] = txtFax.Text;
                drFax["InActive"] = 0;
                drFax["UserId"] = Utility.GetCurrentUser_UserId();
                drFax["ModifiedDate"] = DateTime.Now;
                TelManager.AddRow(drFax);

                TelManager.Save();

                trans.EndSave();
                PkAgentId.Value = Utility.EncryptQS(AgentId.ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                SetEditModeKeys();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err, 'I');
        }
    }

    private void UpdateAgent()
    {
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if (string.IsNullOrEmpty(AgentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();

        trans.Add(TelManager);
        trans.Add(AgentManager);

        AgentManager.FindByCode(Convert.ToInt32(AgentId));

        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        if (AgentManager.Count >= 1)
        {
            try
            {
                trans.BeginSave();

                AgentManager[0].BeginEdit();

                //AgentManager[0]["AgentId"] = Convert.ToInt32(AgentId);

                AgentManager[0]["Address"] = txtAddress.Text;
                AgentManager[0]["PermissionDate"] = txtDate.Text;
                AgentManager[0]["Email"] = txtEmail.Text;
                AgentManager[0]["MobileNo"] = txtMobileNo.Text;
                AgentManager[0]["PermissionNo"] = txtPerNo.Text;
                AgentManager[0]["Website"] = txtWebsite.Text;
                AgentManager[0]["CreateDate"] = PerDate;

                AgentManager[0]["Name"] = txtName.Text;
                AgentManager[0]["AgentCode"] = txtAgentCode.Text;
                AgentManager[0]["Description"] = txtDescription.Html;
                AgentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AgentManager[0]["ModifiedDate"] = DateTime.Now;

                AgentManager[0]["TaxOfficeName"] = txtTaxOfficeName.Text;
                AgentManager[0]["TaxOfficeTell"] = txtTaxOfficeTell.Text;
                AgentManager[0]["TaxOfficeAdress"] = txtTaxOfficeAdress.Text;
                AgentManager[0].EndEdit();

                if (AgentManager.Save() == 1)
                {
                    AgentManager.DataTable.AcceptChanges();
                    AgentId = AgentManager[0]["AgentId"].ToString();

                    TelManager.FindByTablePrimaryKey(int.Parse(AgentId), (int)TSP.DataManager.TableCodes.Agent);
                    if (TelManager.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(txtTel.Text))
                        {
                            DataRow drTel = TelManager.NewRow();
                            drTel["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                            drTel["TtId"] = AgentId;
                            drTel["Kind"] = 0;
                            drTel["Number"] = txtTel.Text;
                            drTel["InActive"] = 0;
                            drTel["UserId"] = Utility.GetCurrentUser_UserId();
                            drTel["ModifiedDate"] = DateTime.Now;
                            TelManager.AddRow(drTel);
                        }
                        if (!string.IsNullOrEmpty(txtFax.Text))
                        {
                            DataRow drFax = TelManager.NewRow();
                            drFax["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                            drFax["TtId"] = AgentId;
                            drFax["Kind"] = 1;
                            drFax["Number"] = txtFax.Text;
                            drFax["InActive"] = 0;
                            drFax["UserId"] = Utility.GetCurrentUser_UserId();
                            drFax["ModifiedDate"] = DateTime.Now;
                            TelManager.AddRow(drFax);
                        }
                        TelManager.Save();
                    }
                    else
                    {
                        Boolean IsTelUpdated = false;
                        Boolean IsFaxUpdated = false;
                        for (int i = 0; i < TelManager.Count; i++)
                        {

                            if (TelManager[i]["Kind"].ToString() == "0")
                            {
                                TelManager[i].BeginEdit();

                                TelManager[i]["Number"] = txtTel.Text;
                                TelManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                                TelManager[i]["ModifiedDate"] = DateTime.Now;
                                TelManager[i].EndEdit();
                                IsTelUpdated = true;
                            }
                            else if (TelManager[i]["Kind"].ToString() == "1")
                            {
                                TelManager[i].BeginEdit();

                                TelManager[i]["Number"] = txtFax.Text;
                                TelManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                                TelManager[i]["ModifiedDate"] = DateTime.Now;
                                TelManager[i].EndEdit();
                                IsFaxUpdated = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(txtTel.Text) && !IsTelUpdated)
                        {
                            DataRow drTel = TelManager.NewRow();
                            drTel["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                            drTel["TtId"] = AgentId;
                            drTel["Kind"] = 0;
                            drTel["Number"] = txtTel.Text;
                            drTel["InActive"] = 0;
                            drTel["UserId"] = Utility.GetCurrentUser_UserId();
                            drTel["ModifiedDate"] = DateTime.Now;
                            TelManager.AddRow(drTel);
                        }
                        if (!string.IsNullOrEmpty(txtFax.Text) && !IsFaxUpdated)
                        {
                            DataRow drFax = TelManager.NewRow();
                            drFax["TtType"] = (int)TSP.DataManager.TableCodes.Agent;
                            drFax["TtId"] = AgentId;
                            drFax["Kind"] = 1;
                            drFax["Number"] = txtFax.Text;
                            drFax["InActive"] = 0;
                            drFax["UserId"] = Utility.GetCurrentUser_UserId();
                            drFax["ModifiedDate"] = DateTime.Now;
                            TelManager.AddRow(drFax);
                        }

                        TelManager.Save();
                    }


                    trans.EndSave();

                    PkAgentId.Value = Utility.EncryptQS(AgentId.ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    SetEditModeKeys();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }

            catch (Exception err)
            {
                trans.CancelSave();
                SetError(err, 'U');
            }
        }
    }

    private void DeleteAgent()
    {
        AgentId = Utility.DecryptQS(PkAgentId.Value);
        if (string.IsNullOrEmpty(AgentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();

        trans.Add(AgentManager);
        trans.Add(TelManager);

        AgentManager.FindByCode(Convert.ToInt32(AgentId));

        if (AgentManager.Count == 1)
        {
            try
            {
                trans.BeginSave();

                TelManager.FindByTablePrimaryKey(int.Parse(AgentId), (int)TSP.DataManager.TableCodes.Agent);
                if (TelManager.Count > 0)
                {
                    int c = TelManager.Count;
                    for (int i = 0; i < c; i++)
                        TelManager[0].Delete();

                    TelManager.Save();
                }


                AgentManager[0].Delete();
                int cn = AgentManager.Save();
                if (cn == 1)
                {
                    trans.EndSave();
                    AgentManager.DataTable.AcceptChanges();
                    PkAgentId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    SetNewModeKeys();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            catch (Exception err)
            {
                trans.CancelSave();
                SetError(err, 'D');
            }
        }
    }


    /*************************************************************************************************************/
    private void SetError(Exception err, char Flag)
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
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وابسته معتبر نمی باشد";
                }
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

    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkAgentId.Value = Server.HtmlDecode(Request.QueryString["AgentId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        PageMode = Utility.DecryptQS(PgMode.Value);
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        SetMode();
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
        Enable(true);
       
        btnEdit.Enabled = btnEdit2.Enabled = btnDelete.Enabled = btnDelete2.Enabled = false;
      
        CheckAccess();

        txtName.Text = "";
        txtAgentCode.Text = "";
        txtDescription.Html = "";
        txtAddress.Text = "";
        txtDate.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";
        txtMobileNo.Text = "";
        txtPerNo.Text = "";
        txtTel.Text = "";
        txtWebsite.Text = "";
        txtTaxOfficeAdress.Text = "";
        txtTaxOfficeName.Text = "";
        txtTaxOfficeTell.Text = "";

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if ((string.IsNullOrEmpty(AgentId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        Enable(true);
        CheckAccess();
        Fill();

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if ((string.IsNullOrEmpty(AgentId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        Enable(false);
        btnEdit.Enabled = btnEdit2.Enabled = btnNew.Enabled = btnNew2.Enabled = true;
       
        CheckAccess();
        Fill();
        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void Fill()
    {
        TSP.DataManager.AccountingAgentManager Manager = new TSP.DataManager.AccountingAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();
        try
        {
            Manager.FindByCode(int.Parse(AgentId));
            if (Manager.Count == 1)
            {
                txtName.Text = Manager[0]["Name"].ToString();
                txtAgentCode.Text = Manager[0]["AgentCode"].ToString();
                txtDescription.Html = Manager[0]["Description"].ToString();
                txtAddress.Text = Manager[0]["Address"].ToString();
                txtDate.Text = Manager[0]["PermissionDate"].ToString();
                txtEmail.Text = Manager[0]["Email"].ToString();
                txtMobileNo.Text = Manager[0]["MobileNo"].ToString();
                txtPerNo.Text = Manager[0]["PermissionNo"].ToString();
                txtWebsite.Text = Manager[0]["Website"].ToString();

                if (!Utility.IsDBNullOrNullValue(Manager[0]["TaxOfficeName"]))
                    txtTaxOfficeName.Text = Manager[0]["TaxOfficeName"].ToString();

                if (!Utility.IsDBNullOrNullValue(Manager[0]["TaxOfficeTell"]))
                    txtTaxOfficeTell.Text = Manager[0]["TaxOfficeTell"].ToString();

                if (!Utility.IsDBNullOrNullValue(Manager[0]["TaxOfficeAdress"]))
                    txtTaxOfficeAdress.Text = Manager[0]["TaxOfficeAdress"].ToString();

                TelManager.FindByTablePrimaryKey(int.Parse(AgentId), (int)TSP.DataManager.TableCodes.Agent);
                if (TelManager.Count > 0)
                {
                    for (int i = 0; i < TelManager.Count; i++)
                    {
                        if (TelManager[i]["Kind"].ToString() == "0")
                            txtTel.Text = TelManager[i]["Number"].ToString();
                        else if (TelManager[i]["Kind"].ToString() == "1")
                            txtFax.Text = TelManager[i]["Number"].ToString();

                    }

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "چنین رکوردی وجود ندارد";
            }
        }
        catch
        {

        }
    }

    private void Enable(bool Enable)
    {
        btnNew.Enabled = Enable;
        btnNew2.Enabled = Enable;
        btnSave.Enabled = Enable;
        btnSave2.Enabled = Enable;
        btnEdit.Enabled = Enable;
        btnEdit2.Enabled = Enable;
        btnDelete.Enabled = Enable;
        btnDelete2.Enabled = Enable;


        txtName.Enabled = Enable;
        txtAgentCode.Enabled = Enable;
        txtDescription.Enabled = Enable;
       
        txtAddress.Enabled = Enable;
        txtDate.Enabled = Enable;
        txtEmail.Enabled = Enable;
        txtFax.Enabled = Enable;
        txtMobileNo.Enabled = Enable;
        txtPerNo.Enabled = Enable;
        txtTel.Enabled = Enable;
        txtWebsite.Enabled = Enable;
        txtTaxOfficeName.Enabled = Enable;
        txtTaxOfficeTell.Enabled = Enable;
        txtTaxOfficeAdress.Enabled = Enable;
    }
    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.AccountingAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (btnDelete.Enabled == true)
        {
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
        }
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
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
}
