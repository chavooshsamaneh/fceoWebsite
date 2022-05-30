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

public partial class Employee_Amoozesh_AddConfPersonTeachers : System.Web.UI.Page
{
    string PageMode;
    string ConfPerId;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Request.QueryString["MjId"] == null || Request.QueryString["ConfPerId"] == null || Request.QueryString["PageMode"] == null)
        {
            Response.Redirect("ConfirmPersonTeachers.aspx");
            return;
        }
        if (!IsPostBack)
        {
            SetKeys();
           
        }
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();
        int TableType= (int)(TSP.DataManager.TableCodes.Teachers);
        int TableId=int.Parse(Utility.DecryptQS(HiddenFieldConfirm["TableId"].ToString()));
        DataTable dtConfPer = ConfirmPersonManager.SelectByTableType(TableType,TableId);
        int Priority=dtConfPer.Rows.Count+1;
        txtPriority.Text = Priority.ToString();
        txtPriority.ClientEnabled = false;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int ConfPerId = int.Parse(Utility.DecryptQS(HiddenFieldConfirm["ConfPerId"].ToString()));
        Delete(ConfPerId);
        ClearForm();
        CmbNezamChartName.SelectedIndex = 0;
        CmbNezamChartName_SelectedIndexChanged(this, new EventArgs());
        CmbNezamChartName.ClientEnabled = true;
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldConfirm["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
               
                BtnDelete.ClientEnabled = true;
                btnDelete2.ClientEnabled = true;
                btnEdit.ClientEnabled = false;
                btnEdit2.ClientEnabled = false;
                InsertConfirmPerson();

                break;
            case "Edit":
                if (string.IsNullOrEmpty(HiddenFieldConfirm["ConfPerId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    ConfPerId = Utility.DecryptQS(HiddenFieldConfirm["ConfPerId"].ToString());
                }
                EditConfirmPerson(int.Parse(ConfPerId));
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConfirmPersonTeachers.aspx");
    }

    protected void CmbNezamChartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        NezamMemberChartManager.FindByNcId(int.Parse(CmbNezamChartName.SelectedItem.Value.ToString()));
        if (NezamMemberChartManager.Count > 0)
        {
            txtbName.Text = NezamMemberChartManager[0]["FirstName"].ToString();
            txtbFamily.Text = NezamMemberChartManager[0]["LastName"].ToString();
        }
        CmbNezamChartName.ClientEnabled = true;
        txtbDescription.ClientEnabled = true;
    }

    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldConfirm["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
        HiddenFieldConfirm["ConfPerId"] = Server.HtmlDecode(Request.QueryString["ConfPerId"]).ToString();
        //*****TableId is MjId
        HiddenFieldConfirm["TableId"] = Server.HtmlDecode(Request.QueryString["MjId"]).ToString();

        PageMode = Utility.DecryptQS(HiddenFieldConfirm["PageMode"].ToString());
        ConfPerId = Utility.DecryptQS(HiddenFieldConfirm["ConfPerId"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode();
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
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.ConfirmPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.ClientEnabled = false;
        btnSave2.ClientEnabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.ClientEnabled = true;
            btnEdit2.ClientEnabled = true;
        }
        if (per.CanDelete)
        {
            BtnDelete.ClientEnabled = true;
            btnDelete2.ClientEnabled = true;
        }

        //Set Textboxe's & comboboxe's Enabled
        txtbDescription.ClientEnabled = false;
        txtbFamily.ClientEnabled = false;
        txtbName.ClientEnabled = false;
        CmbNezamChartName.ClientEnabled = false;


        FillForm(int.Parse(ConfPerId));

        CmbNezamChartName.ClientEnabled = false;
        txtbDescription.ClientEnabled = false;
        // RoundPanelConfirmPerson.HeaderText = "مشاهده";
        SetRoundPanelHeader("مشاهده");
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        BtnDelete.ClientEnabled = false;
        btnDelete2.ClientEnabled = false;

        ClearForm();
        CmbNezamChartName.SelectedIndex = 0;
        CmbNezamChartName.DataBind();
        CmbNezamChartName_SelectedIndexChanged(this, new EventArgs());
        cmbPrioityType.SelectedIndex = 0;
        SetRoundPanelHeader("جدید");

    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.ConfirmPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        if (per.CanDelete)
        {
            BtnDelete.ClientEnabled = true;
            btnDelete2.ClientEnabled = true;
        }
        btnEdit.ClientEnabled = false;
        btnEdit2.ClientEnabled = false;
        btnSave.ClientEnabled = true;
        btnSave2.ClientEnabled = true;

        if (string.IsNullOrEmpty(ConfPerId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnabaleControls();
        FillForm(int.Parse(ConfPerId));
        CmbNezamChartName.ClientEnabled = false;
        //RoundPanelConfirmPerson.HeaderText = "ویرایش";
        SetRoundPanelHeader("ویرایش");
    }

    private void FillForm(int ConfirmPersonId)
    {
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();
        // TSP.DataManager.SmsConfirmManager SmsConfirmManager = new TSP.DataManager.SmsConfirmManager();
        ConfirmPersonManager.FindByCode(ConfirmPersonId);
        if (ConfirmPersonManager.Count > 0)
        {
            txtbDescription.Text = ConfirmPersonManager[0]["Description"].ToString();
            //  CmbNezamChartName.SelectedIndex = int.Parse(SmsConfirmPersonManager[0]["NcId"].ToString());//CmbNezamChartName.Items.IndexOfValue(int.Parse(SmsConfirmPersonManager[0]["NcId"].ToString()));
            ChbHasPrePriority.Checked = Boolean.Parse(ConfirmPersonManager[0]["HasPrePriority"].ToString());
            CmbNezamChartName.DataBind();
            CmbNezamChartName.SelectedIndex = CmbNezamChartName.Items.IndexOfValue(ConfirmPersonManager[0]["NcId"].ToString());

            CmbNezamChartName_SelectedIndexChanged(this, new EventArgs());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
        }

    }

    private void ClearForm()
    {
        txtbName.Text = "";
        txtbFamily.Text = "";
        txtbDescription.Text = "";
        txtPriority.Text = "";
        CmbNezamChartName.SelectedIndex = -1;
        cmbPrioityType.SelectedIndex = 0;
    }

    private void EnabaleControls()
    {
        txtbDescription.ClientEnabled = true;
        txtbFamily.ClientEnabled = false;
        txtbName.ClientEnabled = false;
        txtPriority.ClientEnabled = true;
        ChbHasPrePriority.ClientEnabled = true;
        cmbPrioityType.ClientEnabled = true;
        CmbNezamChartName.ClientEnabled = true;
    }

    private void SetRoundPanelHeader(string PageMode)
    {

        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();

        if (!String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]))))
        {
            string TypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]));
           // ObjdsSmsConfirm.SelectParameters[0].DefaultValue = TypeId;
            if (!String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]))))
            {
                string EmpId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]));
                string TypeName = "";
                string EmpName = "";
                EmployeeManager.FindByCode(int.Parse(EmpId));
                if (EmployeeManager.Count > 0)
                {
                    EmpName = EmployeeManager[0]["FirstName"].ToString() + " " + EmployeeManager[0]["LastName"].ToString();
                }
                MajorManager.FindByCode(int.Parse(TypeId));
                if (MajorManager.Count > 0)
                {
                    TypeName = MajorManager[0]["SmsTypeName"].ToString();
                }
                RoundPanelConfirmPerson.HeaderText = PageMode;
                if (TypeName != "" && EmpName != "")
                    lblConfirmDetails.Text =EmpName+" _ "+ "تایید کننده پرونده استاد برای رشته " + TypeName;
                else
                    lblConfirmDetails.Text = "";
            }
        }
    }

    private void InsertConfirmPerson()
    {
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();
        TSP.DataManager.ConfirmManager ConfirmManager = new TSP.DataManager.ConfirmManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ConfirmManager);
        TransactionManager.Add(ConfirmPersonManager);

        try
        {
            TransactionManager.BeginSave();
            DataRow ConfPerRow = ConfirmPersonManager.NewRow();
            DataRow ConfRow = ConfirmManager.NewRow();
            int ConfId = -1;
            int TableType= (int)(TSP.DataManager.TableCodes.Teachers);
            int TableId=int.Parse(Utility.DecryptQS(HiddenFieldConfirm["TableId"].ToString()));
            DataTable dtConfPer = ConfirmPersonManager.SelectByTableType(TableType,TableId);
            if (dtConfPer.Rows.Count > 0)
                ConfId = int.Parse(dtConfPer.Rows[0]["ConfId"].ToString());

            if (ConfId < 0)
            {
                ConfRow["TableType"] = TableType;
                ConfRow["TableId"] = TableId;
                ConfRow["UserId"] = Utility.GetCurrentUser_UserId();
                ConfRow["ModifiedDate"] = DateTime.Now;

                ConfirmManager.AddRow(ConfRow);

                int cn = ConfirmManager.Save();
                if (cn <= 0)
                {
                    TransactionManager.CancelSave();
                }
            }

            ConfPerRow["ConfId"] = int.Parse( ConfirmManager[0]["ConfId"].ToString());
            ConfPerRow["NcId"] = int.Parse(CmbNezamChartName.SelectedItem.Value.ToString());
            ConfPerRow["Priority"] = txtPriority.Text;
            ConfPerRow["HasPrePriority"] = ChbHasPrePriority.Checked;
            ConfPerRow["IsConfirmer"] =cmbPrioityType.SelectedIndex;
            ConfPerRow["Description"] = txtbDescription.Text;
            ConfPerRow["UserId"] = Utility.GetCurrentUser_UserId();
            ConfPerRow["ModifiedDate"] = DateTime.Now;

            ConfirmPersonManager.AddRow(ConfPerRow);

            int cnt = ConfirmPersonManager.Save();

            if (cnt > 0)
            {
                HiddenFieldConfirm["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldConfirm["ConfPerId"] = Utility.EncryptQS(ConfirmPersonManager[0]["ConfPerId"].ToString());
                TransactionManager.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره با موفقیت انجام شد.";
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();

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

    private void EditConfirmPerson(int ConfPerId)
    {
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();

       // int ConfPerId = int.Parse(Utility.DecryptQS(HiddenFieldConfirm["ConfPerId"].ToString()));
        try
        {
            ConfirmPersonManager.FindByCode(ConfPerId);
            if (ConfirmPersonManager.Count > 0)
            {
                ConfirmPersonManager[0].BeginEdit();

                ConfirmPersonManager[0]["NcId"] = int.Parse(CmbNezamChartName.SelectedItem.Value.ToString());
                ConfirmPersonManager[0]["Priority"] = txtPriority.Text;
                ConfirmPersonManager[0]["HasPrePriority"] = ChbHasPrePriority.Checked;
                ConfirmPersonManager[0]["IsConfirmer"] = cmbPrioityType.SelectedIndex;
                ConfirmPersonManager[0]["Description"] = txtbDescription.Text;
                ConfirmPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ConfirmPersonManager[0]["ModifiedDate"] = DateTime.Now;

                ConfirmPersonManager[0].EndEdit();
                int cn = ConfirmPersonManager.Save();

                if (cn > 0)
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
    private void Delete(int ConfPerId)
    {
        TSP.DataManager.ConfirmManager ConfirmManager = new TSP.DataManager.ConfirmManager();
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();
        ConfirmPersonManager.FindByCode(ConfPerId);

        if (ConfirmPersonManager.Count > 0)
        {
            int ConfId = int.Parse(ConfirmPersonManager[0]["ConfId"].ToString());
            ConfirmPersonManager[0].Delete();
            int cn = ConfirmPersonManager.Save();
            if (cn < 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در حذف انجام شد.";
                return;
            }
            else
            {
                ConfirmPersonManager.ClearBeforeFill = true;
                ConfirmPersonManager.FindByConfId(ConfId);
                if (ConfirmPersonManager.Count == 0)
                {
                    ConfirmManager.FindByCode(ConfId);
                    if (ConfirmManager.Count > 0)
                    {
                        ConfirmManager[0].Delete();
                        cn = ConfirmManager.Save();
                        if (cn > 0)
                        {
                            HiddenFieldConfirm["PageMode"] = Utility.EncryptQS("New");
                            HiddenFieldConfirm["ConfPerId"] ="";
                            DivReport.Visible = true;
                            LabelWarning.Text = "حذف با موفقیت انجام شد.";
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "خطایی در حذف انجام شد.";
                        }
                    }
                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "حذف با موفقیت انجام شد.";
                }
            }
        }
    }
    #endregion

}
