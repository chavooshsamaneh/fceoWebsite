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

public partial class CityInsert : System.Web.UI.Page
{
    int CitId;
    String PageMode;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");



        if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) ||
            (string.IsNullOrEmpty(Request.QueryString["CitId"])))
        {
            Response.Redirect("City.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            HiddenFieldCity["PageMode"] = "";
            HiddenFieldCity["CitId"] = "";
            HiddenFieldCity["NewMode"] = Utility.EncryptQS("New");

            TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
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

    protected void CallbackPanelCity_Callback(object sender,
    DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameters = e.Parameter.Split(';');

        if (parameters[0] == "Country")
        {
            Populate_Province();
        }
        else if (parameters[0] == "Province")
        {
            Populate_Agent();
        }
        else if (parameters[0] == "Agent")
        {
            Populate_RegionOfCity();
        }
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        //string CitId = Utility.DecryptQS(HiddenFieldCity["CitId"].ToString());

        //if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(CitId))
        //{
        //    string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
        //    Response.Redirect("Default6.aspx?PostId=" + HiddenFieldCity["CitId"] + "&GrdFlt=" + GrdFlt);
        //}
        //else
        //{
        Response.Redirect("City.aspx");
        //}
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;


        HiddenFieldCity["PageMode"] = Utility.EncryptQS("Edit");
        EnableControls();
        RoundPanelRequest.HeaderText = "ویرایش";
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
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

        HiddenFieldCity["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldCity["CitId"] = "";

        ClearForm();
        EnableControls();
        RoundPanelRequest.HeaderText = "جدید";
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldCity["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                InsertCity();
                break;
            case "Edit":
                if (String.IsNullOrEmpty(HiddenFieldCity["CitId"].ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    CitId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldCity["CitId"].ToString()));
                    EditCity();
                }
                break;
        }
    }

    protected void comboAgent_DataBound(object sender, EventArgs e)
    {
        if (comboAgent.Items.Count > 0)
        {
            if (comboAgent.Items[0].Text != "-------------")
                comboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
        }
        else
        {
            comboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
            comboAgent.Text = "-------------";
        }
    }

    protected void comboProvince_DataBound(object sender, EventArgs e)
    {
        if (comboProvince.Items.Count > 0)
        {
            if (comboProvince.Items[0].Text != "-------------")
                comboProvince.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
        }
        else
        {
            comboProvince.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
            comboProvince.Text = "-------------";
        }
    }

    protected void comboRegionOfCity_DataBound(object sender, EventArgs e)
    {
        if (comboRegionOfCity.Items.Count > 0)
        {
            if (comboRegionOfCity.Items[0].Text != "-------------")
                comboRegionOfCity.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
        }
        else
        {
            comboRegionOfCity.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
            comboRegionOfCity.Text = "-------------";
        }
    }

    protected void comboRegion_DataBound(object sender, EventArgs e)
    {
        if (comboRegion.Items.Count > 0)
        {
            if (comboRegion.Items[0].Text != "-------------")
                comboRegion.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
        }
        else
        {
            comboRegion.Items.Insert(0, new DevExpress.Web.ListEditItem("-------------", null));
            comboRegion.Text = "-------------";
        }
    }
    #endregion

    #region Methods

    private void SetKeys()
    {
        HiddenFieldCity["CitId"] = Server.HtmlDecode(Request.QueryString["CitId"]);
        HiddenFieldCity["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"]);
        CitId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldCity["CitId"].ToString()));
        PageMode = Utility.DecryptQS(HiddenFieldCity["PageMode"].ToString());
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
        TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        checkboxCanObserverBeDesigner.Enabled =
        txtNValueInFunctionA.Enabled =
        textCitCode.Enabled =
        textCitName.Enabled =
        comboAgent.Enabled =
        ComboCountry.Enabled =
        comboProvince.Enabled =
        comboRegion.Enabled =
        comboRegionOfCity.Enabled = checkboxShowInTsWorkRequest.Enabled =checkboxIsPopulationUnder25000.Enabled=
          txtAccountNumberDesign.Enabled = txtAccountNmberObserving.Enabled = txtAccountNmberObserving5Percent.Enabled = txtAccountNmber5In1000.Enabled = txtPINCodeDesign.Enabled = txtTerminalDesign.Enabled = txtPINCodeObserver.Enabled = txtTerminalObserver.Enabled = false;

        if (String.IsNullOrEmpty(HiddenFieldCity["CitId"].ToString()))
        {
            Response.Redirect("Default6.aspx");
        }
        int CitId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldCity["CitId"].ToString()));
        FillForm(CitId);
        RoundPanelRequest.HeaderText = "مشاهده";
    }

    private void FillForm(int CitId)
    {
        TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
        cityManager.FindByCode(CitId);
        if (cityManager.Count == 1)
        {
            textCitName.Text = cityManager[0]["CitName"].ToString();
            textCitCode.Text = cityManager[0]["CitCode"].ToString();
            checkboxShowInTsWorkRequest.Checked = Convert.ToBoolean(cityManager[0]["ShowInTsWorkRequest"]);
            checkboxIsPopulationUnder25000.Checked = Convert.ToBoolean(cityManager[0]["IsPopulationUnder25000"]); 
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["AccountNumberDesign"].ToString()))
                txtAccountNumberDesign.Text = cityManager[0]["AccountNumberDesign"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["AccountNmberObserving"].ToString()))
                txtAccountNmberObserving.Text = cityManager[0]["AccountNmberObserving"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["AccountNmberObserving5Percent"].ToString()))
                txtAccountNmberObserving5Percent.Text = cityManager[0]["AccountNmberObserving5Percent"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["AccountNmber5In1000"].ToString()))
                txtAccountNmber5In1000.Text = cityManager[0]["AccountNmber5In1000"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["PINCodeDesign"].ToString()))
                txtPINCodeDesign.Text = cityManager[0]["PINCodeDesign"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["TerminalDesign"].ToString()))
                txtTerminalDesign.Text = cityManager[0]["TerminalDesign"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["PINCodeObserver"].ToString()))
                txtPINCodeObserver.Text = cityManager[0]["PINCodeObserver"].ToString();
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["TerminalObserver"].ToString()))
                txtTerminalObserver.Text = cityManager[0]["TerminalObserver"].ToString();


            if (!Utility.IsDBNullOrNullValue(cityManager[0]["NValueInFunctionA"].ToString()))
                txtNValueInFunctionA.Text = cityManager[0]["NValueInFunctionA"].ToString();
            checkboxCanObserverBeDesigner.Checked = Convert.ToBoolean(cityManager[0]["CanObserverBeDesigner"]);

            if (!Utility.IsDBNullOrNullValue(cityManager[0]["CounId"].ToString()))
            {
                ComboCountry.DataBind();
                ComboCountry.SelectedIndex = ComboCountry.Items.FindByValue(cityManager[0]["CounId"]).Index;
            }
            if (!Utility.IsDBNullOrNullValue(cityManager[0]["PrId"].ToString()))
            {
                Populate_Province();
                if (comboProvince.Items.Count > 1)
                    comboProvince.SelectedIndex = comboProvince.Items.FindByValue(cityManager[0]["PrId"]).Index;
                if (!Utility.IsDBNullOrNullValue(cityManager[0]["AgentId"]) && Convert.ToInt32(cityManager[0]["PrId"]) == Utility.GetCurrentProvinceId())
                {
                    comboAgent.DataBind();
                    if (comboAgent.Items.Count > 1)
                        comboAgent.SelectedIndex = comboAgent.Items.FindByValue(cityManager[0]["AgentId"]).Index;
                }
            }

            if (!Utility.IsDBNullOrNullValue(cityManager[0]["ReCitId"].ToString()))
            {
                if (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem))

                    ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue =
                                (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem.Value)) ? comboAgent.SelectedItem.Value.ToString() : "-1";
                else
                    ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue = "-1";
                comboRegionOfCity.DataBind();
                try
                {
                    if (comboRegionOfCity.Items.Count > 1)
                        comboRegionOfCity.SelectedIndex =
                            comboRegionOfCity.Items.FindByValue(cityManager[0]["ReCitId"]).Index;
                }
                catch
                {

                }
            }

            if (!Utility.IsDBNullOrNullValue(cityManager[0]["ReId"].ToString()))
            {
                comboRegion.DataBind();
                if (comboRegion.Items.Count > 1)
                    comboRegion.SelectedIndex =
                        comboRegion.Items.FindByValue(cityManager[0]["ReId"]).Index;
            }
        }
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        btnNew.Enabled = btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        ClearForm();
        RoundPanelRequest.HeaderText = "جدید";
    }

    private void ClearForm()
    {
        txtNValueInFunctionA.Text = txtAccountNumberDesign.Text = txtAccountNmberObserving.Text = txtAccountNmberObserving5Percent.Text = txtAccountNmber5In1000.Text = txtPINCodeDesign.Text = txtTerminalDesign.Text =
              txtPINCodeObserver.Text = txtTerminalObserver.Text =
          textCitCode.Text =
          textCitName.Text = String.Empty;
        ComboCountry.SelectedIndex = -1;
        comboProvince.SelectedIndex = -1;
        comboAgent.SelectedIndex = -1;
        comboRegion.SelectedIndex = -1;
        comboRegionOfCity.SelectedIndex = -1;
        checkboxCanObserverBeDesigner.Checked = checkboxShowInTsWorkRequest.Checked =checkboxIsPopulationUnder25000.Checked= false;
    }

    private void SetEditModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();

        if (string.IsNullOrEmpty(HiddenFieldCity["CitId"].ToString()))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        EnableControls();
        int CitId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldCity["CitId"].ToString()));
        FillForm(CitId);
        RoundPanelRequest.HeaderText = "ویرایش";
    }

    private void EnableControls()
    {
        textCitCode.Enabled =
        textCitName.Enabled =
        ComboCountry.Enabled =
        comboProvince.Enabled =
        comboAgent.Enabled =
        comboRegion.Enabled =
        comboRegionOfCity.Enabled = checkboxShowInTsWorkRequest.Enabled =checkboxIsPopulationUnder25000.Enabled=
        txtAccountNumberDesign.Enabled = txtAccountNmberObserving.Enabled = txtAccountNmberObserving5Percent.Enabled = txtAccountNmber5In1000.Enabled = txtPINCodeDesign.Enabled = txtTerminalDesign.Enabled
        = txtPINCodeObserver.Enabled = txtTerminalObserver.Enabled=checkboxCanObserverBeDesigner.Enabled=txtNValueInFunctionA.Enabled
        = true;
    }

    private void Populate_Province()
    {
        if (!Utility.IsDBNullOrNullValue(ComboCountry.SelectedItem))
            ObjectDataSourceProvince.SelectParameters["CounId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(ComboCountry.SelectedItem.Value)) ? ComboCountry.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceProvince.SelectParameters["CounId"].DefaultValue = "-1";

        comboProvince.DataBind();
        comboAgent.DataBind();

        if (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem))
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem.Value)) ? comboAgent.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue = "-1";

        comboRegionOfCity.DataBind();
    }

    private void Populate_Agent()
    {
        comboProvince.DataBind();

        if (!Utility.IsDBNullOrNullValue(comboProvince.SelectedItem))
            ObjectDataSourceAgent.SelectParameters["PrId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(comboProvince.SelectedItem.Value)) ? comboProvince.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceAgent.SelectParameters["PrId"].DefaultValue = "-1";

        comboAgent.DataBind();

        if (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem))
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem.Value)) ? comboAgent.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue = "-1";

        comboRegionOfCity.DataBind();
    }

    private void Populate_RegionOfCity()
    {
        comboProvince.DataBind();
        comboAgent.DataBind();

        if (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem))
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue =
                (!Utility.IsDBNullOrNullValue(comboAgent.SelectedItem.Value)) ? comboAgent.SelectedItem.Value.ToString() : "-1";
        else
            ObjectDataSourceRegionOfCity.SelectParameters["AgentId"].DefaultValue = "-1";
        comboRegionOfCity.DataBind();
    }

    private void EditCity()
    {
        String CitCode = textCitCode.Text;
        String CitName = textCitName.Text;

        comboProvince.DataBind();
        comboAgent.DataBind();
        comboRegion.DataBind();
        comboRegionOfCity.DataBind();

        Object PrId = (comboProvince.Value != null) ? comboProvince.SelectedItem.Value : DBNull.Value;
        Object AgentId;
        if (!Utility.IsDBNullOrNullValue(comboProvince.SelectedItem.Value))
        {
            if (comboProvince.SelectedItem.Value.ToString() == Utility.GetCurrentProvinceId().ToString())
            {
                AgentId = (comboAgent.SelectedItem.Value != null) ? comboAgent.SelectedItem.Value : DBNull.Value;
            }
            else
            {
                AgentId = DBNull.Value;
            }
        }
        else
        {
            AgentId = DBNull.Value;
        }

        Object CounId = (ComboCountry.SelectedItem.Value != null) ? ComboCountry.SelectedItem.Value : DBNull.Value;
        Object ReCitId = (comboRegionOfCity.SelectedItem.Value != null) ? comboRegionOfCity.SelectedItem.Value : DBNull.Value;
        Object ReId = (comboRegion.SelectedItem.Value != null) ? comboRegion.SelectedItem.Value : DBNull.Value;


        TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
        cityManager.FindByCode(CitId);

        if (cityManager.Count == 1)
        {
            try
            {
                cityManager[0].BeginEdit();
                cityManager[0]["CitCode"] = CitCode;
                cityManager[0]["CitName"] = CitName;
                cityManager[0]["PrId"] = PrId;
                cityManager[0]["AgentId"] = AgentId;
                cityManager[0]["CounId"] = CounId;
                cityManager[0]["ReCitId"] = ReCitId;
                cityManager[0]["ReId"] = ReId;
                cityManager[0]["AccountNumberDesign"] = txtAccountNumberDesign.Text;
                cityManager[0]["AccountNmberObserving"] = txtAccountNmberObserving.Text;
                cityManager[0]["AccountNmberObserving5Percent"] = txtAccountNmberObserving5Percent.Text;
                cityManager[0]["AccountNmber5In1000"] = txtAccountNmber5In1000.Text;
                cityManager[0]["PINCodeDesign"] = txtPINCodeDesign.Text;
                cityManager[0]["TerminalDesign"] = txtTerminalDesign.Text;
                cityManager[0]["PINCodeObserver"] = txtPINCodeObserver.Text;
                cityManager[0]["TerminalObserver"] = txtTerminalObserver.Text;
                cityManager[0]["ShowInTsWorkRequest"] = checkboxShowInTsWorkRequest.Checked;
                cityManager[0]["IsPopulationUnder25000"] = checkboxIsPopulationUnder25000.Checked;                
                cityManager[0]["CanObserverBeDesigner"] = checkboxCanObserverBeDesigner.Checked;
                cityManager[0]["NValueInFunctionA"] = txtNValueInFunctionA.Text;
                cityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                cityManager[0]["ModifiedDate"] = DateTime.Now;
                cityManager[0].EndEdit();

                if (cityManager.Save() == 1)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
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

    private void InsertCity()
    {
        try
        {
            TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
            DataRow dr = cityManager.NewRow();

            comboProvince.DataBind();
            comboAgent.DataBind();
            comboRegionOfCity.DataBind();

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["CitCode"] = textCitCode.Text;
            dr["CitName"] = textCitName.Text;
            dr["AccountNumberDesign"] = txtAccountNumberDesign.Text;
            dr["AccountNmberObserving"] = txtAccountNmberObserving.Text;
            dr["AccountNmberObserving5Percent"] = txtAccountNmberObserving5Percent.Text;
            dr["AccountNmber5In1000"] = txtAccountNmber5In1000.Text;
            dr["PINCodeDesign"] = txtPINCodeDesign.Text;
            dr["TerminalDesign"] = txtTerminalDesign.Text;
            dr["PINCodeObserver"] = txtPINCodeObserver.Text;
            dr["TerminalObserver"] = txtTerminalObserver.Text;

            dr["PrId"] = (comboProvince.Value != null) ? comboProvince.SelectedItem.Value : DBNull.Value;
            if (comboAgent.Enabled)
                dr["AgentId"] = (comboAgent.Value != null) ? comboAgent.SelectedItem.Value : DBNull.Value;
            else
                dr["AgentId"] = DBNull.Value;

            dr["CounId"] = (ComboCountry.Value != null) ? ComboCountry.SelectedItem.Value : DBNull.Value;
            dr["ReCitId"] = (comboRegionOfCity.Value != null) ? comboRegionOfCity.SelectedItem.Value : DBNull.Value;
            dr["ReId"] = (comboRegion.Value != null) ? comboRegion.SelectedItem.Value : DBNull.Value;
            dr["ShowInTsWorkRequest"] = checkboxIsPopulationUnder25000.Checked;            
            dr["CanObserverBeDesigner"] = checkboxCanObserverBeDesigner.Checked;
            dr["NValueInFunctionA"] = txtNValueInFunctionA.Text;
            dr["ModifiedDate"] = DateTime.Now;

            cityManager.AddRow(dr);
            if (cityManager.Save() <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            RoundPanelRequest.HeaderText = "ویرایش";
            HiddenFieldCity["PageMode"] = Utility.EncryptQS("Edit");
            HiddenFieldCity["CitId"] = Utility.EncryptQS(cityManager[cityManager.Count - 1]["CitId"].ToString());
            // textCitCode.Text = String.Empty;
            // textCitName.Text = String.Empty;       
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                if (se.Number == 2627)
                {
                    ShowMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
