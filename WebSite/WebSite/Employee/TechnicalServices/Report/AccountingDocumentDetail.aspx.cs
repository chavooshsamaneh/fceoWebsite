using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_AccountingDocumentDetail : System.Web.UI.Page
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

    string _PageModeDocument
    {
        get
        {
            return HiddenFieldPage["PageModeDocument"].ToString();
        }
        set
        {
            HiddenFieldPage["PageModeDocument"] = value.ToString();
        }
    }
    string _PageModeList
    {
        get
        {
            return HiddenFieldPage["PageModeList"].ToString();
        }
        set
        {
            HiddenFieldPage["PageModeList"] = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
            if (!IsPostBack)
            {
                SetKey();
            }
            if (this.ViewState["BtnDelete"] != null)
                this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
            if (this.ViewState["BtnView"] != null)
                this.btnView2.Enabled = this.btnView.Enabled = (bool)this.ViewState["BtnView"];
            if (this.ViewState["BtnNew"] != null)
                this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountingDocumentDetailInsert.aspx?AccDocId=" + Utility.EncryptQS(_AccDocId.ToString())
            + "&AccDocDetailId=" + Utility.EncryptQS("-1")
            + "&PgMd=" + Utility.EncryptQS("New")
            + "&PrePgMd=" + Utility.EncryptQS(_PageModeDocument));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewAccountingDocumentDetail.FocusedRowIndex < 0)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        Response.Redirect("AccountingDocumentDetailInsert.aspx?AccDocId=" + Utility.EncryptQS(_AccDocId.ToString())
            + "&AccDocDetailId=" + Utility.EncryptQS(((GridViewAccountingDocumentDetail.GetDataRow(GridViewAccountingDocumentDetail.FocusedRowIndex))["AccDocDetailId"]).ToString())
            + "&PgMd=" + Utility.EncryptQS("View") + "&PrePgMd=" + Utility.EncryptQS(_PageModeDocument));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridViewAccountingDocumentDetail.FocusedRowIndex < 0)
            {
                SetMessage("ابتدا یک ردیف را انتخاب نمایید");
                return;
            }
            int AccDocDetailId = Convert.ToInt32((GridViewAccountingDocumentDetail.GetDataRow(GridViewAccountingDocumentDetail.FocusedRowIndex))["AccDocDetailId"]);
            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
            AccountingDocumentDetailManager.FindByCode(AccDocDetailId);
            if (AccountingDocumentDetailManager.Count == 0)
            {
                SetMessage("خطا در بازخوانی اطلاعات ایجاد شده است");
                return;
            }
            AccountingDocumentDetailManager[0].Delete();
            if (AccountingDocumentDetailManager.Save() <= 0)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DeleteComplete));
            GridViewAccountingDocumentDetail.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (e.Item.Name == "AccDoc")
        {
            Response.Redirect("AccountingDocumentInsert.aspx?AccDocId=" + Utility.EncryptQS(_AccDocId.ToString()) + "&PgMd=" + Utility.EncryptQS(_PageModeDocument));
        }
    }

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void SetKey()
    {

        _PageModeDocument = Utility.DecryptQS(Request.QueryString["PrePgMd"]);
        _AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
        ObjdAccountingDocumentDetail.SelectParameters["AccDocId"].DefaultValue = _AccDocId.ToString();

        FillForm(_AccDocId);
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
    }
    #endregion
}