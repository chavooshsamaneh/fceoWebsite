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

public partial class Employee_TechnicalServices_Project_RegisteredNoInsert : System.Web.UI.Page
{
    string PageMode;
    string ProjectId;
    string PrjReId;
    string RegisteredNoId;

    bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        // txtRegisteredNo.Attributes["onkeyup"] = "return ltr_override(event)";
        //ASPxTextBoxDivision.Attributes["onkeyup"] = "return ltr_override(event)";

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

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.RegisteredNoManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            if ((string.IsNullOrEmpty(Request.QueryString["MPgMode"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["RegisteredNoId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("RegisteredNo.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["MPgMode"]));
                return;
            }

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
                Insert();
                break;
            case "Edit":
                Update();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + MPgMode.Value
               + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("RegisteredNo.aspx?" + Qs);
    }
    #endregion

    #region Methods

    #region SetKey-Mode
    private void SetKeys()
    {
        try
        {
            MPgMode.Value = Server.HtmlDecode(Request.QueryString["MPgMode"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkRegisteredNo.Value = Server.HtmlDecode(Request.QueryString["RegisteredNoId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);
            string MPageMode = Utility.DecryptQS(MPgMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(RegisteredNoId) || string.IsNullOrEmpty(MPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            FillProjectInfo(int.Parse(PrjReId));
            SetMode();
            CheckWorkFlowPermission();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
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
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        CheckAccess();

        txtRegisteredNo.Enabled = true;
        ASPxTextBoxPostalCode.Enabled = true;
        cmbIsMain.Enabled = false;
        ASPxTextBoxMelkNorth.Enabled = true;
        ASPxTextBoxMelkEast.Enabled = true;
        ASPxTextBoxMelkSouth.Enabled = true;
        ASPxTextBoxMelkWest.Enabled = true;
        ASPxTextBoxLengthNorth.Enabled = true;
        ASPxTextBoxLengthEast.Enabled = true;
        ASPxTextBoxLengthSouth.Enabled = true;
        ASPxTextBoxLengthWest.Enabled = true;
        ASPxTextBoxTarizNorth.Enabled = true;
        ASPxTextBoxTarizEast.Enabled = true;
        ASPxTextBoxTarizSouth.Enabled = true;
        ASPxTextBoxTarizWest.Enabled = true;
        ASPxTextBoxRemainNorth.Enabled = true;
        ASPxTextBoxRemainEast.Enabled = true;
        ASPxTextBoxRemainSouth.Enabled = true;
        ASPxTextBoxRemainWest.Enabled = true;
        ASPxTextBoxPathWayWidthNorth.Enabled = true;
        ASPxTextBoxPathWayWidthEast.Enabled = true;
        ASPxTextBoxPathWayWidthSouth.Enabled = true;
        ASPxTextBoxPathWayWidthWest.Enabled = true;
        ASPxTextBoxLimitNorth.Enabled = true;
        ASPxTextBoxLimitEast.Enabled = true;
        ASPxTextBoxLimitSouth.Enabled = true;
        ASPxTextBoxLimitWest.Enabled = true;
        ASPxTextBoxDivision.Enabled = true;

        txtRegisteredNo.Text = "";
        ASPxTextBoxPostalCode.Text = "";

        cmbIsMain.SelectedIndex = cmbIsMain.Items.FindByValue(0).Index;
       
        ASPxTextBoxMelkNorth.Text = "";
        ASPxTextBoxMelkEast.Text = "";
        ASPxTextBoxMelkSouth.Text = "";
        ASPxTextBoxMelkWest.Text = "";
        ASPxTextBoxLengthNorth.Text = "";
        ASPxTextBoxLengthEast.Text = "";
        ASPxTextBoxLengthSouth.Text = "";
        ASPxTextBoxLengthWest.Text = "";
        ASPxTextBoxTarizNorth.Text = "";
        ASPxTextBoxTarizEast.Text = "";
        ASPxTextBoxTarizSouth.Text = "";
        ASPxTextBoxTarizWest.Text = "";
        ASPxTextBoxRemainNorth.Text = "";
        ASPxTextBoxRemainEast.Text = "";
        ASPxTextBoxRemainSouth.Text = "";
        ASPxTextBoxRemainWest.Text = "";
        ASPxTextBoxPathWayWidthNorth.Text = "";
        ASPxTextBoxPathWayWidthEast.Text = "";
        ASPxTextBoxPathWayWidthSouth.Text = "";
        ASPxTextBoxPathWayWidthWest.Text = "";
        ASPxTextBoxLimitNorth.Text = "";
        ASPxTextBoxLimitEast.Text = "";
        ASPxTextBoxLimitSouth.Text = "";
        ASPxTextBoxLimitWest.Text = "";
        ASPxTextBoxDivision.Text = "";

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        //btnDelete.Enabled = true;
        //btnDelete2.Enabled = true;
        CheckAccess();

        txtRegisteredNo.Enabled = true;
        ASPxTextBoxPostalCode.Enabled = true;
        cmbIsMain.Enabled = false;
        ASPxTextBoxMelkNorth.Enabled = true;
        ASPxTextBoxMelkEast.Enabled = true;
        ASPxTextBoxMelkSouth.Enabled = true;
        ASPxTextBoxMelkWest.Enabled = true;
        ASPxTextBoxLengthNorth.Enabled = true;
        ASPxTextBoxLengthEast.Enabled = true;
        ASPxTextBoxLengthSouth.Enabled = true;
        ASPxTextBoxLengthWest.Enabled = true;
        ASPxTextBoxTarizNorth.Enabled = true;
        ASPxTextBoxTarizEast.Enabled = true;
        ASPxTextBoxTarizSouth.Enabled = true;
        ASPxTextBoxTarizWest.Enabled = true;
        ASPxTextBoxRemainNorth.Enabled = true;
        ASPxTextBoxRemainEast.Enabled = true;
        ASPxTextBoxRemainSouth.Enabled = true;
        ASPxTextBoxRemainWest.Enabled = true;
        ASPxTextBoxPathWayWidthNorth.Enabled = true;
        ASPxTextBoxPathWayWidthEast.Enabled = true;
        ASPxTextBoxPathWayWidthSouth.Enabled = true;
        ASPxTextBoxPathWayWidthWest.Enabled = true;
        ASPxTextBoxLimitNorth.Enabled = true;
        ASPxTextBoxLimitEast.Enabled = true;
        ASPxTextBoxLimitSouth.Enabled = true;
        ASPxTextBoxLimitWest.Enabled = true;
        ASPxTextBoxDivision.Enabled = true;

        SetValues();

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        CheckAccess();


        txtRegisteredNo.Enabled = false;
        ASPxTextBoxPostalCode.Enabled = false;
        cmbIsMain.Enabled = false;
        ASPxTextBoxMelkNorth.Enabled = false;
        ASPxTextBoxMelkEast.Enabled = false;
        ASPxTextBoxMelkSouth.Enabled = false;
        ASPxTextBoxMelkWest.Enabled = false;
        ASPxTextBoxLengthNorth.Enabled = false;
        ASPxTextBoxLengthEast.Enabled = false;
        ASPxTextBoxLengthSouth.Enabled = false;
        ASPxTextBoxLengthWest.Enabled = false;
        ASPxTextBoxTarizNorth.Enabled = false;
        ASPxTextBoxTarizEast.Enabled = false;
        ASPxTextBoxTarizSouth.Enabled = false;
        ASPxTextBoxTarizWest.Enabled = false;
        ASPxTextBoxRemainNorth.Enabled = false;
        ASPxTextBoxRemainEast.Enabled = false;
        ASPxTextBoxRemainSouth.Enabled = false;
        ASPxTextBoxRemainWest.Enabled = false;
        ASPxTextBoxPathWayWidthNorth.Enabled = false;
        ASPxTextBoxPathWayWidthEast.Enabled = false;
        ASPxTextBoxPathWayWidthSouth.Enabled = false;
        ASPxTextBoxPathWayWidthWest.Enabled = false;
        ASPxTextBoxLimitNorth.Enabled = false;
        ASPxTextBoxLimitEast.Enabled = false;
        ASPxTextBoxLimitSouth.Enabled = false;
        ASPxTextBoxLimitWest.Enabled = false;
        ASPxTextBoxDivision.Enabled = false;

        SetValues();

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }
    #endregion

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    private void SetValues()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);

        if ((string.IsNullOrEmpty(RegisteredNoId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.RegisteredNoManager Manager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        Manager.FindByRegisteredNoId(int.Parse(RegisteredNoId));
        if (Manager.Count == 1)
        {
            HiddenFieldRegNo.Value = txtRegisteredNo.Text = Manager[0]["RegisteredNo"].ToString();

            ASPxTextBoxPostalCode.Text = Manager[0]["PostalCode"].ToString();
            if (!Utility.IsDBNullOrNullValue(Manager[0]["IsMain"]))
                cmbIsMain.SelectedIndex = cmbIsMain.Items.FindByValue(Convert.ToInt32(Manager[0]["IsMain"])).Index;
            ASPxTextBoxDivision.Text = Manager[0]["Division"].ToString();

            if (Convert.ToBoolean(Manager[0]["InActive"]))
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
            }

            SetDirections(int.Parse(RegisteredNoId));
        }
        else
        {
            SetLabelWarning("چنین رکوردی وجود ندارد");
        }
    }

    private void SetDirections(int RegisteredNoId)
    {
        TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager = new TSP.DataManager.TechnicalServices.DirectionsManager();
        DirectionsManager.FindByRegisteredNoId(RegisteredNoId);
        for (int i = 0; i < DirectionsManager.Count; i++)
        {
            switch (Convert.ToInt32(DirectionsManager[i]["DirectionTypeId"]))
            {
                case (int)TSP.DataManager.TSDirectionType.Dimension:
                    ASPxTextBoxMelkNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxMelkEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxMelkSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxMelkWest.Text = DirectionsManager[i]["West"].ToString();
                    break;

                case (int)TSP.DataManager.TSDirectionType.Length:
                    ASPxTextBoxLengthNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxLengthEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxLengthSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxLengthWest.Text = DirectionsManager[i]["West"].ToString();
                    break;

                case (int)TSP.DataManager.TSDirectionType.Wideness:
                    ASPxTextBoxTarizNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxTarizEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxTarizSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxTarizWest.Text = DirectionsManager[i]["West"].ToString();
                    break;

                case (int)TSP.DataManager.TSDirectionType.RemainDimension:
                    ASPxTextBoxRemainNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxRemainEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxRemainSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxRemainWest.Text = DirectionsManager[i]["West"].ToString();
                    break;

                case (int)TSP.DataManager.TSDirectionType.PathWayWidth:
                    ASPxTextBoxPathWayWidthNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxPathWayWidthEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxPathWayWidthSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxPathWayWidthWest.Text = DirectionsManager[i]["West"].ToString();
                    break;

                case (int)TSP.DataManager.TSDirectionType.Limit:
                    ASPxTextBoxLimitNorth.Text = DirectionsManager[i]["North"].ToString();
                    ASPxTextBoxLimitEast.Text = DirectionsManager[i]["East"].ToString();
                    ASPxTextBoxLimitSouth.Text = DirectionsManager[i]["South"].ToString();
                    ASPxTextBoxLimitWest.Text = DirectionsManager[i]["West"].ToString();
                    break;
            }
        }
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.RegisteredNoManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

        //if (btnDelete.Enabled == true)
        //{
        //    btnDelete.Enabled = per.CanDelete;
        //    btnDelete2.Enabled = per.CanDelete;
        //}
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
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    #region Insert-Update
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        //if (ASPxTextBoxDivision.Text == "0")
        //{
        //    SetLabelWarning("لطفاً شماره بخش ثبتی را وارد نمایید");
        //    return;
        //}

        if (!CheckMainRegisteredNo())
        {
            SetLabelWarning("نمی توان بیشتر از یک پلاک ثبتی اصلی ثبت کرد");
            return;
        }

        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager = new TSP.DataManager.TechnicalServices.DirectionsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        if (CheckIsRegNoExist(txtRegisteredNo.Text, RegisteredNoManager))
        {
            return;
        }
        transact.Add(RegisteredNoManager);
        transact.Add(DirectionsManager);

        try
        {

            transact.BeginSave();

            InsertRegisteredNo(RegisteredNoManager);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.Dimension);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.Length);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.Wideness);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.RemainDimension);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.PathWayWidth);
            InsertDirection(DirectionsManager, (int)TSP.DataManager.TSDirectionType.Limit);

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'I');
        }
    }

    private void InsertRegisteredNo(TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        DataRow rowRegisteredNo = RegisteredNoManager.NewRow();

        rowRegisteredNo.BeginEdit();
        rowRegisteredNo["ProjectId"] = ProjectId;
        rowRegisteredNo["RegisteredNo"] = HiddenFieldRegNo.Value = txtRegisteredNo.Text;
        rowRegisteredNo["PostalCode"] = ASPxTextBoxPostalCode.Text;
        if (cmbIsMain.SelectedItem != null)
            rowRegisteredNo["IsMain"] = cmbIsMain.SelectedItem.Value;
        rowRegisteredNo["InActive"] = 0;
        rowRegisteredNo["PrjReId"] = PrjReId;
        rowRegisteredNo["Division"] = ASPxTextBoxDivision.Text;
        rowRegisteredNo["UserId"] = Utility.GetCurrentUser_UserId();
        rowRegisteredNo["ModifiedDate"] = DateTime.Now;
        rowRegisteredNo.EndEdit();

        RegisteredNoManager.AddRow(rowRegisteredNo);
        RegisteredNoManager.Save();

        RegisteredNoManager.DataTable.AcceptChanges();
        RegisteredNoId = RegisteredNoManager[0]["RegisteredNoId"].ToString();
        PkRegisteredNo.Value = Utility.EncryptQS(RegisteredNoId.ToString());

    }

    private void InsertDirection(TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager, int DirectionTypeId)
    {
        RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);

        DataRow rowDirections = DirectionsManager.NewRow();

        rowDirections.BeginEdit();
        rowDirections["RegisteredNoId"] = RegisteredNoId;
        InsertDirectionFields(rowDirections, DirectionTypeId);
        rowDirections["DirectionTypeId"] = DirectionTypeId;
        rowDirections["InActive"] = 0;
        rowDirections["UserId"] = Utility.GetCurrentUser_UserId();
        rowDirections["ModifiedDate"] = DateTime.Now;
        rowDirections.EndEdit();

        DirectionsManager.AddRow(rowDirections);
        DirectionsManager.Save();
        DirectionsManager.DataTable.AcceptChanges();
    }

    private void InsertDirectionFields(DataRow rowDirections, int DirectionTypeId)
    {
        switch (DirectionTypeId)
        {
            case (int)TSP.DataManager.TSDirectionType.Dimension:
                rowDirections["North"] = ASPxTextBoxMelkNorth.Text;
                rowDirections["East"] = ASPxTextBoxMelkEast.Text;
                rowDirections["South"] = ASPxTextBoxMelkSouth.Text;
                rowDirections["West"] = ASPxTextBoxMelkWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Length:
                rowDirections["North"] = ASPxTextBoxLengthNorth.Text;
                rowDirections["East"] = ASPxTextBoxLengthEast.Text;
                rowDirections["South"] = ASPxTextBoxLengthSouth.Text;
                rowDirections["West"] = ASPxTextBoxLengthWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Wideness:
                rowDirections["North"] = ASPxTextBoxTarizNorth.Text;
                rowDirections["East"] = ASPxTextBoxTarizEast.Text;
                rowDirections["South"] = ASPxTextBoxTarizSouth.Text;
                rowDirections["West"] = ASPxTextBoxTarizWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.RemainDimension:
                rowDirections["North"] = ASPxTextBoxRemainNorth.Text;
                rowDirections["East"] = ASPxTextBoxRemainEast.Text;
                rowDirections["South"] = ASPxTextBoxRemainSouth.Text;
                rowDirections["West"] = ASPxTextBoxRemainWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.PathWayWidth:
                rowDirections["North"] = ASPxTextBoxPathWayWidthNorth.Text;
                rowDirections["East"] = ASPxTextBoxPathWayWidthEast.Text;
                rowDirections["South"] = ASPxTextBoxPathWayWidthSouth.Text;
                rowDirections["West"] = ASPxTextBoxPathWayWidthWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Limit:
                rowDirections["North"] = ASPxTextBoxLimitNorth.Text;
                rowDirections["East"] = ASPxTextBoxLimitEast.Text;
                rowDirections["South"] = ASPxTextBoxLimitSouth.Text;
                rowDirections["West"] = ASPxTextBoxLimitWest.Text;
                break;
        }
    }
    /************************************************************* Update ********************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        //if (ASPxTextBoxDivision.Text == "0")
        //{
        //    SetLabelWarning("لطفاً شماره بخش ثبتی را وارد نمایید");
        //    return;
        //}

        if (!CheckMainRegisteredNoForUpdate())
        {
            SetLabelWarning("نمی توان بیشتر از یک پلاک ثبتی اصلی ثبت کرد");
            return;
        }

        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager = new TSP.DataManager.TechnicalServices.DirectionsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        if (CheckIsRegNoExist(txtRegisteredNo.Text, RegisteredNoManager))
        {
            return;
        }
        transact.Add(RegisteredNoManager);
        transact.Add(DirectionsManager);

        try
        {
            transact.BeginSave();

            UpdateRegisteredNo(RegisteredNoManager);
            UpdateDirection(DirectionsManager);

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'U');
        }
    }

    private void UpdateRegisteredNo(TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager)
    {
        RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        if (string.IsNullOrEmpty(RegisteredNoId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        RegisteredNoManager.FindByRegisteredNoId(Convert.ToInt32(RegisteredNoId));

        if (RegisteredNoManager.Count >= 1)
        {
            RegisteredNoManager[0].BeginEdit();
            RegisteredNoManager[0]["ProjectId"] = ProjectId;
            RegisteredNoManager[0]["RegisteredNo"] = HiddenFieldRegNo.Value = txtRegisteredNo.Text;
            RegisteredNoManager[0]["PostalCode"] = ASPxTextBoxPostalCode.Text;
            if (cmbIsMain.SelectedItem != null)
                RegisteredNoManager[0]["IsMain"] = cmbIsMain.SelectedItem.Value;// ASPxCheckBoxIsMain.Checked;
            RegisteredNoManager[0]["InActive"] = 0;
            RegisteredNoManager[0]["PrjReId"] = PrjReId;
            RegisteredNoManager[0]["Division"] = ASPxTextBoxDivision.Text;
            RegisteredNoManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            RegisteredNoManager[0]["ModifiedDate"] = DateTime.Now;
            RegisteredNoManager[0].EndEdit();

            RegisteredNoManager.Save();

            RegisteredNoManager.DataTable.AcceptChanges();
            RegisteredNoId = RegisteredNoManager[0]["RegisteredNoId"].ToString();
            PkRegisteredNo.Value = Utility.EncryptQS(RegisteredNoId.ToString());
        }
    }

    private void UpdateDirection(TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager)
    {
        RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);
        DirectionsManager.FindByRegisteredNoId(Convert.ToInt32(RegisteredNoId));
        for (int i = 0; i < DirectionsManager.Count; i++)
        {
            DirectionsManager[i].BeginEdit();
            DirectionsManager[i]["RegisteredNoId"] = RegisteredNoId;
            UpdateDirectionFields(DirectionsManager[i]);
            //DirectionsManager[i]["DirectionTypeId"] = DirectionTypeId;
            DirectionsManager[i]["InActive"] = 0;
            DirectionsManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
            DirectionsManager[i]["ModifiedDate"] = DateTime.Now;
            DirectionsManager[i].EndEdit();
        }

        DirectionsManager.Save();
        DirectionsManager.DataTable.AcceptChanges();
    }

    private void UpdateDirectionFields(DataRow rowDirections)
    {
        switch (Convert.ToInt32(rowDirections["DirectionTypeId"]))
        {
            case (int)TSP.DataManager.TSDirectionType.Dimension:
                rowDirections["North"] = ASPxTextBoxMelkNorth.Text;
                rowDirections["East"] = ASPxTextBoxMelkEast.Text;
                rowDirections["South"] = ASPxTextBoxMelkSouth.Text;
                rowDirections["West"] = ASPxTextBoxMelkWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Length:
                rowDirections["North"] = ASPxTextBoxLengthNorth.Text;
                rowDirections["East"] = ASPxTextBoxLengthEast.Text;
                rowDirections["South"] = ASPxTextBoxLengthSouth.Text;
                rowDirections["West"] = ASPxTextBoxLengthWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Wideness:
                rowDirections["North"] = ASPxTextBoxTarizNorth.Text;
                rowDirections["East"] = ASPxTextBoxTarizEast.Text;
                rowDirections["South"] = ASPxTextBoxTarizSouth.Text;
                rowDirections["West"] = ASPxTextBoxTarizWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.RemainDimension:
                rowDirections["North"] = ASPxTextBoxRemainNorth.Text;
                rowDirections["East"] = ASPxTextBoxRemainEast.Text;
                rowDirections["South"] = ASPxTextBoxRemainSouth.Text;
                rowDirections["West"] = ASPxTextBoxRemainWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.PathWayWidth:
                rowDirections["North"] = ASPxTextBoxPathWayWidthNorth.Text;
                rowDirections["East"] = ASPxTextBoxPathWayWidthEast.Text;
                rowDirections["South"] = ASPxTextBoxPathWayWidthSouth.Text;
                rowDirections["West"] = ASPxTextBoxPathWayWidthWest.Text;
                break;

            case (int)TSP.DataManager.TSDirectionType.Limit:
                rowDirections["North"] = ASPxTextBoxLimitNorth.Text;
                rowDirections["East"] = ASPxTextBoxLimitEast.Text;
                rowDirections["South"] = ASPxTextBoxLimitSouth.Text;
                rowDirections["West"] = ASPxTextBoxLimitWest.Text;
                break;
        }
    }
    #endregion

    private bool HaveMainRegisteredNo()
    {
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        return RegisteredNoManager.HaveMain(Convert.ToInt32(ProjectId));
    }

    private bool CheckMainRegisteredNo()
    {
        if (cmbIsMain.SelectedItem != null && cmbIsMain.SelectedItem.Value.ToString() == "1"
            && HaveMainRegisteredNo())
            return false;
        else
            return true;
    }

    private TSP.DataManager.TechnicalServices.RegisteredNoManager GetMainRegisteredNo()
    {
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        RegisteredNoManager.GetMainRegisteredNo(Convert.ToInt32(ProjectId));
        return RegisteredNoManager;
    }

    private bool CheckMainRegisteredNoForUpdate()
    {
        RegisteredNoId = Utility.DecryptQS(PkRegisteredNo.Value);
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = GetMainRegisteredNo();

        if (RegisteredNoManager.Count > 0
            && cmbIsMain.SelectedItem != null && cmbIsMain.SelectedItem.Value == "1"
            && RegisteredNoManager[0]["RegisteredNoId"].ToString() != RegisteredNoId)
            return false;
        else
            return true;
    }

    private bool CheckIsRegNoExist(string RegNo, TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager)
    {
        RegisteredNoManager.FindByActiveRegisteredNo(RegNo);
        if (RegisteredNoManager.Count > 0)
        {
            // ProjectId = Utility.DecryptQS(PkProjectId.Value);
            // if ( ProjectId != RegisteredNoManager[0]["ProjectId"].ToString() )
            if(HiddenFieldRegNo.Value!=RegNo)
            {
                SetLabelWarning("پیش از این پلاک ثبتی وارد شده به عنوان پلاک ثبتی پروژه با کد " + RegisteredNoManager[0]["ProjectId"].ToString() + " ثبت شده است.");
                return true;
            }
        }
        return false;
    }

    #region Set Error
    private void SetError(Exception err, char Flag)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    #endregion

    #region WorkFlow Method

    private void CheckWorkFlowPermission()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    #endregion
    #endregion
}