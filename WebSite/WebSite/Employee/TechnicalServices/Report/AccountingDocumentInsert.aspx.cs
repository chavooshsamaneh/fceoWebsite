using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_AccountingDocumentInsert : System.Web.UI.Page
{
    int _AccDocId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["AccDocId"]);
        }
        set
        {
            HiddenFieldPage["AccDocId"] = value.ToString();
        }
    }

    string _PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value.ToString();
        }
    }
    #region Methods
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            SetKey();
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "Edit":
                Edit(_AccDocId);
                break;
            case "New":
                Insert();
                break;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectObserverReportList.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditMode();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
    }
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (e.Item.Name == "AccDocDetail")
        {
            Response.Redirect("AccountingDocumentDetail.aspx?AccDocId=" + Utility.EncryptQS(_AccDocId.ToString()) + "&PrePgMd=" + Utility.EncryptQS(_PageMode));
        }
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["AccDocId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
            btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew || PerObserverReportList.CanEdit;

            _AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            switch (_PageMode)
            {
                case "New":
                    SetNewMode();
                    break;
                case "Edit":
                    SetViewMode();
                    break;
                case "View":
                    SetViewMode();
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در بازیابی اطلاعات ایجاد شده است");
        }
    }

    private void SetNewMode()
    {
        _AccDocId = -1;
        _PageMode = "New";
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = false;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew;
        RoundPanelPage.HeaderText = "جدید";
        ClearForm();
        SetEnable(true);
        MainMenu.Enabled = false;
    }
    private void SetViewMode()
    {
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanNew || PerObserverReportList.CanEdit;
        FillForm(_AccDocId);
        RoundPanelPage.HeaderText = "مشاهده";
        SetEnable(false);
        btnSave.Enabled = btnSave2.Enabled = false;
        MainMenu.Enabled = true;
    }
    private void SetEditMode()
    {
        _PageMode = "Edit";
        TSP.DataManager.Permission PerObserverReportList = TSP.DataManager.TechnicalServices.AccountingDocumentManager.GetUserPermission_ObserverReportList(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew2.Enabled = BtnNew.Enabled = PerObserverReportList.CanNew;
        btnEdit2.Enabled = btnEdit.Enabled = PerObserverReportList.CanEdit;
        btnSave2.Enabled = btnSave.Enabled = PerObserverReportList.CanEdit;
        FillForm(_AccDocId);
        RoundPanelPage.HeaderText = "ویرایش";
        SetEnable(true);
        MainMenu.Enabled = true;
    }
    private void ClearForm()
    {
        txtListName.Text = txtDescription.Text = "";
        txtStatusName.Text = txtListNo.Text =  txtListDate.Text = "---";
        CmbType.SelectedIndex = -1;
        TSP.DataManager.AccountingAgentManager AccountingAgentManager = new TSP.DataManager.AccountingAgentManager();
        AccountingAgentManager.FindByCode(Utility.GetCurrentUser_AgentId());
        if (AccountingAgentManager.Count != 0)
        {
            txtAgentName.Text = AccountingAgentManager[0]["Name"].ToString();
        }
    }

    private void FillForm(int AccDocId)
    {
        TSP.DataManager.TechnicalServices.AccountingDocumentManager AccountingDocumentManager = new TSP.DataManager.TechnicalServices.AccountingDocumentManager();
        AccountingDocumentManager.FindByCode(AccDocId);
        if (AccountingDocumentManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["Description"]))
            txtDescription.Text = AccountingDocumentManager[0]["Description"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListDate"]))
            txtListDate.Text = AccountingDocumentManager[0]["ListDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListName"]))
            txtListName.Text = AccountingDocumentManager[0]["ListName"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["ListNo"]))
            txtListNo.Text = AccountingDocumentManager[0]["ListNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["StatusName"]))
            txtStatusName.Text = AccountingDocumentManager[0]["StatusName"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["Type"]))
            CmbType.SelectedIndex = CmbType.Items.FindByValue(AccountingDocumentManager[0]["Type"].ToString()).Index;
        else
            CmbType.SelectedIndex = -1;
        if (!Utility.IsDBNullOrNullValue(AccountingDocumentManager[0]["AgentName"]))
            txtAgentName.Text = AccountingDocumentManager[0]["AgentName"].ToString();
    }

    private void SetEnable(Boolean Enabled)
    {
        txtDescription.Enabled = txtListName.Enabled = CmbType.Enabled = Enabled;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void Insert()
    {
        try
        {
            if (CmbType.SelectedItem == null)
            {
                SetMessage("نوع لیست را انتخاب نمایید");
                return;
            }
            int AccDocId = -1;
            AccDocId = TSP.DataManager.TechnicalServices.AccountingDocumentManager.InsertAccDocument(Utility.GetDateOfToday(), txtDescription.Text, Utility.GetCurrentUser_UserId(), txtListName.Text, Convert.ToInt16(CmbType.SelectedItem.Value), null, Utility.GetCurrentUser_AgentId());
            if (AccDocId < 0)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            _AccDocId = AccDocId;
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            SetEditMode();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void Edit(int AccDocId)
    {
        try
        {
            if (CmbType.SelectedItem == null)
            {
                SetMessage("نوع لیست را انتخاب نمایید");
                return;
            }
            AccDocId = TSP.DataManager.TechnicalServices.AccountingDocumentManager.EditAccDocument(_AccDocId, txtDescription.Text, Utility.GetCurrentUser_UserId(), txtListName.Text, Convert.ToInt16(CmbType.SelectedItem.Value));
            if (AccDocId < 0)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion

}