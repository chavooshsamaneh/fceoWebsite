using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class Members_TechnicalServices_Capacity_ObserverWorkRequestInsert : System.Web.UI.Page
{
    #region Properties
    string _PageMode
    {
        get
        {
            return HiddenFieldForm["PgMd"].ToString();
        }
        set
        {
            HiddenFieldForm["PgMd"] = value.ToString();
        }
    }
    int _ObsWorkReqChangeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldForm["ObsWChangId"]);
        }
        set
        {
            HiddenFieldForm["ObsWChangId"] = value.ToString();
        }
    }
    int _MeId
    {
        get
        {

            return Convert.ToInt32(HiddenFieldForm["MeId"]);

        }
        set
        {
            HiddenFieldForm["MeId"] = value.ToString();
        }
    }
    #endregion
    #region Events

    protected void btnConflict_Click(object sender, EventArgs e)
    {
        try
        {
            if (ComboBoxConflictType.SelectedItem == null)
            {
                ShowMessage("نوع مغایرت را انتخاب نمایید");
                return;
            }
            TSP.DataManager.ConflictManager ConflictManager = new TSP.DataManager.ConflictManager();
            DataRow dr = ConflictManager.NewRow();
            dr["ConfTypeId"] = ComboBoxConflictType.SelectedItem.Value;
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["RegisterDate"] = Utility.GetDateOfToday();
            dr["Satisfaied"] = 0;
            dr["SatisfaiedDate"] = "";
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["Description"] = txtConflictDescription.Text;
            ConflictManager.AddRow(dr);
            if (ConflictManager.Save() > 0)
            {
                RoundPanelSaveConflict.ClientVisible = true;
                RoundPanelSaveConflict.ClientEnabled =
                btnConflict.ClientEnabled = btnSave.ClientEnabled = false;
                ShowMessage("اعلام مغایرت با موفقیت ذخیره شد");
            }
            else
                ShowMessage("خطا در ذخیره صورت گرفته است");
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطا در ذخیره صورت گرفته است");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReportRequestInsert.Visible = false;
        this.DivReportRequestInsert.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReportRequestInsert.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            SetKeys();
            ObjectDataSourceConfilict.SelectParameters["TypeCode"].DefaultValue = ((int)TSP.DataManager.ConflictTypeCode.TSObserverWorkRequest).ToString();
            ObjectDataSourceConfilict.SelectParameters["InActive"].DefaultValue = "0";
            this.ViewState["BtnSave"] = btnSave.ClientEnabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.ClientEnabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("MemberCapacity");
        Response.Redirect("ObserverWorkRequest.aspx");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (WorkRequestInsertInfoUserControl.btnSave_Click(_PageMode, true))
        {
            btnSave.ClientEnabled = false;
            RoundPanelNoConfilict.Enabled = false;
            CheckBoxNoConfilict.SelectedIndex = 0;
        }
    }
    #endregion

    #region Methods

    private void SetKeys()
    {

        if (Request.QueryString["PgMd"] == null || Request.QueryString["ObsWChangId"] == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);        
        _ObsWorkReqChangeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ObsWChangId"]));
        _MeId = Utility.GetCurrentUser_MeId();
        WorkRequestInsertInfoUserControl.ResetPropertiesAndHidenFields();
        WorkRequestInsertInfoUserControl.SetKeys(_PageMode, _ObsWorkReqChangeId, _MeId);
        RoundPanelPage.HeaderText = WorkRequestInsertInfoUserControl._RoundPanelPageHeader;
        SetMode(_PageMode);
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
                RoundPanelNoConfilict.Enabled = false;
                CheckBoxNoConfilict.SelectedIndex = 0;
                break;

            case "New":
                RoundPanelNoConfilict.Enabled = true;
                CheckBoxNoConfilict.SelectedIndex = -1;
                break;

            case "Off":
                RoundPanelNoConfilict.Visible = false;
                btnSave.Text = "ذخیره مرخصی";
                break;
            case "Change":
                RoundPanelNoConfilict.Visible = true;
                CheckBoxNoConfilict.SelectedIndex = -1;
                break;
        }
    }

    void ShowMessage(string str)
    {
        this.DivReportRequestInsert.Visible = true;
        this.LabelWarning.Text = str;
    }

    #endregion


}