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

public partial class Employee_Nezam_AddNezamMemberChart : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");        
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Request.QueryString["NcId"] == null || Request.QueryString["NmcId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("NezamMemberChart.aspx");
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKey();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("NezamMemberChart.aspx");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew || per.CanEdit;
        btnSave2.Enabled = per.CanNew || per.CanEdit;
        ClearForm();
        Enable();
        HiddenFieldNezamChart["NmcId"] = "";
        HiddenFieldNezamChart["PageMode"] = Utility.DecryptQS("Edit");
        RoundPanelMemberChart.HeaderText = "جدید";

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int NmcId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["NmcId"].ToString()));
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        NezamMemberChartManager.FindByNmcId(NmcId);
        if (NezamMemberChartManager.Count == 1)
        {
            if (Convert.ToBoolean(NezamMemberChartManager[0]["InActive"]))
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "سمت انتخاب شده غیر فعال می باشد.امکان ویرایش اطلاعات سمت غیرفعال وجود ندارد.";
                return;
            }
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        HiddenFieldNezamChart["PageMode"] = Utility.EncryptQS("Edit");
        TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        txtDesc.Enabled = true;
        //TextBoxEmpCode.Enabled = true;
        //txtEndDate.Enabled = true;
        txtStartDate.Enabled = true;
        chbIsMasterPosition.Enabled = true;
        chbIsMaster.Enabled = true;
        RoundPanelMemberChart.HeaderText = "ویرایش";
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldNezamChart["PageMode"].ToString());

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
                string NmcId = Utility.DecryptQS(HiddenFieldNezamChart["NmcId"].ToString());
                if (string.IsNullOrEmpty(NmcId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(NmcId));
                }

            }

        }


    }

    protected void TextBoxEmpCode_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        if (!(string.IsNullOrEmpty(TextBoxEmpCode.Text)))
        {
            EmpManager.FindByEmpCode(TextBoxEmpCode.Text.Trim());
            if (EmpManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["FirstName"]))
                    txtName.Text = EmpManager[0]["FirstName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["LastName"]))
                    txtLastName.Text = EmpManager[0]["LastName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["FatherName"]))
                    txtFatherName.Text = EmpManager[0]["FatherName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["IdNo"]))
                    txtIdNo.Text = EmpManager[0]["IdNo"].ToString();
                HiddenFieldNezamChart["EmpId"] = Utility.EncryptQS(EmpManager[0]["EmpId"].ToString());
            }
            else
            {
                txtName.Text = "";
                txtLastName.Text = "";
                txtFatherName.Text = "";
                txtIdNo.Text = "";
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "کد کارمند وارد شده معتبر نمی باشد.";
            }
        }
        else
        {
            txtName.Text = "";
            txtLastName.Text = "";

        }
    }

    protected void CallbackPanelNezamChart_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        if (!(string.IsNullOrEmpty(TextBoxEmpCode.Text)))
        {
            EmpManager.FindByEmpCode(TextBoxEmpCode.Text.Trim());
            if (EmpManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["FirstName"]))
                    txtName.Text = EmpManager[0]["FirstName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["LastName"]))
                    txtLastName.Text = EmpManager[0]["LastName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["FatherName"]))
                    txtFatherName.Text = EmpManager[0]["FatherName"].ToString();
                if (!Utility.IsDBNullOrNullValue(EmpManager[0]["IdNo"]))
                    txtIdNo.Text = EmpManager[0]["IdNo"].ToString();
                //HiddenFieldNezamChart["EmpId"] = Utility.EncryptQS(EmpManager[0]["EmpId"].ToString());
                CallbackPanelNezamChart.JSProperties["cpEmpId"] = Utility.EncryptQS(EmpManager[0]["EmpId"].ToString());
            }
            else
            {
                txtName.Text = "";
                txtLastName.Text = "";
                txtFatherName.Text = "";
                txtIdNo.Text = "";                
                CallbackPanelNezamChart.JSProperties["cpError"] = 1;
                CallbackPanelNezamChart.JSProperties["cpMsgError"] = "کد کارمند وارد شده معتبر نمی باشد.";
            }
        }
        else
        {
            txtName.Text = "";
            txtLastName.Text = "";

        }
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            HiddenFieldNezamChart["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldNezamChart["NmcId"] = Server.HtmlDecode(Request.QueryString["NmcId"]).ToString();
            HiddenFieldNezamChart["NcId"] = Server.HtmlDecode(Request.QueryString["NcId"]).ToString();
            int NcId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["NcId"].ToString()));
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            NezamChartManager.FindByCode(NcId);
            if (NezamChartManager.Count == 1)
            {
                lblNcName.Text = "پست سازمانی: " + NezamChartManager[0]["NcName"].ToString();
            }
            //else
            //{
            //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            //}
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldNezamChart["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);
        }
    }

    private void SetMode(string Mode)
    {
        TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        string NmcId = Utility.DecryptQS(HiddenFieldNezamChart["NmcId"].ToString());
        switch (Mode)
        {
            case "View":
                Disable();
                if (string.IsNullOrEmpty(NmcId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(int.Parse(NmcId));
                RoundPanelMemberChart.HeaderText = "مشاهده";
                break;
            case "New":
                Enable();
                btnSave2.Enabled = true;
                btnSave.Enabled = true;
                BtnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                RoundPanelMemberChart.HeaderText = "جدید";
                ClearForm();
                break;
            case "Edit":
                Enable();

                if (string.IsNullOrEmpty(NmcId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillForm(int.Parse(NmcId));
                RoundPanelMemberChart.HeaderText = "ویرایش";
                break;
        }
    }

    protected void FillForm(int NmcId)
    {
        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        NezamMemberChartManager.FindByNmcId(NmcId);
        if (NezamMemberChartManager.Count > 0)
        {
            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
            int UltId = int.Parse(NezamMemberChartManager[0]["UltId"].ToString());
            txtDesc.Text = NezamMemberChartManager[0]["Description"].ToString();
            txtEndDate.Text = NezamMemberChartManager[0]["EndDate"].ToString();
            txtStartDate.Text = NezamMemberChartManager[0]["StartDate"].ToString();
            chbIsMasterPosition.Checked = Convert.ToBoolean(NezamMemberChartManager[0]["IsMasterPosition"]);
            chbIsMaster.Checked = Convert.ToBoolean(NezamMemberChartManager[0]["IsMaster"]);
            HiddenFieldNezamChart["chbIsMasterPosition"] = Convert.ToBoolean(NezamMemberChartManager[0]["IsMasterPosition"]);
            HiddenFieldNezamChart["chbIsMaster"] = Convert.ToBoolean(NezamMemberChartManager[0]["IsMaster"]);

            if (UltId == 4)
            {
                EmployeeManager.FindByCode(EmpId);
                if (EmployeeManager.Count == 1)
                {
                    txtName.Text = EmployeeManager[0]["FirstName"].ToString();
                    txtLastName.Text = EmployeeManager[0]["LastName"].ToString();
                    TextBoxEmpCode.Text = EmployeeManager[0]["EmpCode"].ToString();
                    HiddenFieldNezamChart["EmpId"] = Utility.EncryptQS(EmployeeManager[0]["EmpId"].ToString());
                }
                else
                {

                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
            }
        }
        else
        {

            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
    }

    protected void ClearForm()
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        TextBoxEmpCode.Text = "";
        txtDesc.Text = "";
        txtLastName.Text = "";
        txtName.Text = "";
    }

    protected void Disable()
    {
        TextBoxEmpCode.Enabled = false;
        txtDesc.Enabled = false;
        txtEndDate.Enabled = false;
        // txtLastName.Enabled = false;
        // txtName.Enabled = false;
        txtStartDate.Enabled = false;
        chbIsMaster.Enabled = false;
        chbIsMasterPosition.Enabled = false;
    }

    protected void Enable()
    {
        TextBoxEmpCode.Enabled = true;
        txtDesc.Enabled = true;
        //txtEndDate.Enabled = true;
        txtLastName.Enabled = true;
        txtName.Enabled = true;
        txtStartDate.Enabled = true;
        chbIsMaster.Enabled = true;
        //chbIsMasterPosition.Enabled = true;
    }

    private void Insert()
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        try
        {
            NezamMemberChartManager.Fill();
            int NcId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["NcId"].ToString()));
            int EmpId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["EmpId"].ToString()));
            NezamMemberChartManager.CurrentFilter = "EmpId=" + EmpId.ToString() + " and " + "UltId=" + ((int)TSP.DataManager.UserType.Employee).ToString() + " and " + "NcId=" + NcId.ToString();
            if (NezamMemberChartManager.Count > 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "پست سازمانی انتخاب شده برای کارمند انتخاب شده تکراری است.";
                return;
            }
            NezamMemberChartManager.CurrentFilter = "EmpId=" + EmpId.ToString() + " and " + "UltId=" + ((int)TSP.DataManager.UserType.Employee).ToString() + " and " + "IsMasterPosition=" + "true";
            if (chbIsMasterPosition.Checked == true && NezamMemberChartManager.Count > 0)
            {
                for (int i = 0; i < NezamMemberChartManager.Count; i++)
                {
                    NezamMemberChartManager[i].BeginEdit();

                    NezamMemberChartManager[i]["IsMasterPosition"] = 0;
                    NezamMemberChartManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                    NezamMemberChartManager[i]["ModifiedDate"] = DateTime.Now;

                    NezamMemberChartManager[i].EndEdit();
                    if (NezamMemberChartManager.Save() <= 0)
                    {
                        this.DivReport.Attributes.Add("Style", "display:block");
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                        return;
                    }
                }
            }

            if (chbIsMasterPosition.Checked == false && NezamMemberChartManager.Count == 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "هر فرد بایستی حداقل یک پست سازمانی را به عنوان پست سازمانی اصلی انتخاب نمایید .";
                return;
            }

            DataRow NcRow = NezamMemberChartManager.NewRow();
            NcRow["NcId"] = NcId;
            NcRow["EmpId"] = EmpId;
            NcRow["UltId"] = 4;
            NcRow["IsMaster"] = chbIsMaster.Checked;
            NcRow["IsMasterPosition"] = chbIsMasterPosition.Checked;
            NcRow["StartDate"] = txtStartDate.Text;
            NcRow["EndDate"] = txtEndDate.Text;
            NcRow["Description"] = txtDesc.Text;
            NcRow["IsExternal"] = 0;
            NcRow["UserId"] = Utility.GetCurrentUser_UserId();
            NcRow["ModifiedDate"] = DateTime.Now;

            NezamMemberChartManager.AddRow(NcRow);
            if (NezamMemberChartManager.Save() > 0)
            {
                TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                BtnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                HiddenFieldNezamChart["chbIsMasterPosition"] = chbIsMasterPosition.Checked;
                HiddenFieldNezamChart["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldNezamChart["NmcId"] = Utility.EncryptQS(NezamMemberChartManager[0]["NmcId"].ToString());
                RoundPanelMemberChart.HeaderText = "ویرایش";
                TextBoxEmpCode.Enabled = false;

                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Edit(int NmcId)
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager2 = new TSP.DataManager.NezamMemberChartManager();

        try
        {
            int NcId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["NcId"].ToString()));
            int EmpId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["EmpId"].ToString()));
            Boolean PreIsMasterPosition = false;
            if (HiddenFieldNezamChart["chbIsMasterPosition"] != null)
                PreIsMasterPosition = Convert.ToBoolean(HiddenFieldNezamChart["chbIsMasterPosition"]);
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                return;
            }

            if (PreIsMasterPosition != chbIsMasterPosition.Checked)
            {
                NezamMemberChartManager2.Fill();

                NezamMemberChartManager2.CurrentFilter = "EmpId=" + EmpId.ToString() + " and " + "UltId=" + ((int)TSP.DataManager.UserType.Employee).ToString() + " and " + "IsMasterPosition=" + "true";
                if ((chbIsMasterPosition.Checked == false && NezamMemberChartManager2.Count == 0) || (chbIsMasterPosition.Checked == false && NezamMemberChartManager2.Count == 1 && NezamMemberChartManager2[0]["NmcId"].ToString() == NmcId.ToString()))
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "هر فرد بایستی حداقل یک پست سازمانی را به عنوان پست سازمانی اصلی انتخاب نمایید .";
                    return;
                }
            }
            NezamMemberChartManager.FindByNmcId(NmcId);
            if (NezamMemberChartManager.Count > 0)
            {
                //int NcId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["NcId"].ToString()));
                //int EmpId = int.Parse(Utility.DecryptQS(HiddenFieldNezamChart["EmpId"].ToString()));

                NezamMemberChartManager[0].BeginEdit();

                NezamMemberChartManager[0]["NcId"] = NcId;
                NezamMemberChartManager[0]["EmpId"] = EmpId;
                NezamMemberChartManager[0]["UltId"] = 4;
                NezamMemberChartManager[0]["IsMaster"] = 0;
                NezamMemberChartManager[0]["StartDate"] = txtStartDate.Text;
                NezamMemberChartManager[0]["EndDate"] = txtEndDate.Text;
                NezamMemberChartManager[0]["Description"] = txtDesc.Text;
                NezamMemberChartManager[0]["IsExternal"] = 0;
                NezamMemberChartManager[0]["IsMaster"] = chbIsMaster.Checked;
                NezamMemberChartManager[0]["IsMasterPosition"] = chbIsMasterPosition.Checked;
                NezamMemberChartManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                NezamMemberChartManager[0]["ModifiedDate"] = DateTime.Now;

                NezamMemberChartManager[0].EndEdit();
                if (NezamMemberChartManager.Save() > 0)
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
                }
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
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
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
    #endregion
}
