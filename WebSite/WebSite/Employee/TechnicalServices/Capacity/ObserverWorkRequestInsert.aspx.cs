using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;
using WorkRequestsrvEngineerToOthersTest;

public partial class Employee_TechnicalServices_Capacity_ObserverWorkRequestInsert : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ObserverWorkRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Visible = btnSave2.Visible = BtnNew.Visible = BtnNew2.Visible = per.CanNew;
            SetKeys();

            this.ViewState["BtnNewVis"] = BtnNew.Visible;
            this.ViewState["BtnSaveVis"] = btnSave.Visible;
            this.ViewState["BtnSave"] = btnSave.ClientEnabled;

        }
        if (this.ViewState["BtnNewVis"] != null)
            this.BtnNew.Visible = this.BtnNew2.Visible = (bool)this.ViewState["BtnNewVis"];
        if (this.ViewState["BtnSaveVis"] != null)
            this.btnSave.Visible = this.btnSave2.Visible = (bool)this.ViewState["BtnSaveVis"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.ClientEnabled = (bool)this.ViewState["BtnSave"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        _PageMode = "New";
        _MeId = _ObsWorkReqChangeId = -2;
        txtMeId.Text = "";
        txtMeId.Enabled =
         CheckBoxSEndInfoToesup.Enabled = btnSave.Visible = btnSave2.Visible = true;
        WorkRequestInsertInfoUserControl.ResetPropertiesAndHidenFields();
        WorkRequestInsertInfoUserControl.SetKeys(_PageMode, _ObsWorkReqChangeId, _MeId);
        RoundPanelPage.HeaderText = WorkRequestInsertInfoUserControl._RoundPanelPageHeader;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        WorkRequestInsertInfoUserControl.btnSave_Click(_PageMode, CheckBoxSEndInfoToesup.Checked);
        btnSave.Enabled = btnSave2.Enabled = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("MemberCapacity");
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
           && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("ObserverWorkRequest.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("ObserverWorkRequest.aspx");
        }

    }

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
        if (_PageMode != "New")
        {
            return;
        }

        if (Utility.IsDBNullOrNullValue(txtMeId.Text.Trim()))
        {
            ShowMessage("لطفا کد عضویت را وارد نمائید.");
            return;
        }

        try
        {

            _MeId = Convert.ToInt32(txtMeId.Text);
            WorkRequestInsertInfoUserControl.ClearAndResetForm();
            WorkRequestInsertInfoUserControl.LoadMemberInfoForNewMode(_MeId);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازخوانی اطلاعات عضو انجام گرفته است");
        }
    }
    #endregion
    #region Methods
    private void SetKeys()
    {

        if (Request.QueryString["PgMd"] == null || Request.QueryString["ObsWChangId"] == null || Request.QueryString["MeId"] == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
        _ObsWorkReqChangeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ObsWChangId"]));
        _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]));
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
                txtMeId.Enabled =
                CheckBoxSEndInfoToesup.Enabled = false;
                txtMeId.Text = _MeId.ToString();
                btnSave.Visible = btnSave2.Visible = false;
                break;

            case "New":
                txtMeId.Text = "";
                break;

            case "Off":
                txtMeId.Enabled = false;
                txtMeId.Text = _MeId.ToString();
                break;
            case "Change":
                txtMeId.Enabled = false;
                txtMeId.Text = _MeId.ToString();
                break;
        }
    }

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }
    #endregion  

}