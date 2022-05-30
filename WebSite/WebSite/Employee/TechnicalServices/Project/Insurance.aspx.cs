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
using DevExpress.Web;
using System.IO;

public partial class Employee_TechnicalServices_Project_Insurance : System.Web.UI.Page
{
    string PageMode;
    string ProjectId;
    string InsuranceId;
    string PrjReId;
    string MPageMode;

    bool IsPageRefresh = false;
    #region Events
    #region btn Click
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        ASPxTextBoxInsuranceNo.Attributes["onkeyup"] = "return ltr_override(event)";

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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.InsuranceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            Session["InsuranceAttachNameRnd"] = null;
            Session["InsuranceAttachName"] = null;

            SetKeys();
            SetProjectMenuEnabled();
            SetProjectMainMenuEnabled();

            if ((!per.CanView && Utility.DecryptQS(PgMode.Value) != "New"))
            {
                string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + MPgMode.Value;
                Response.Redirect("ProjectInsert.aspx?" + Qs);
            }

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
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        PkProjectId.Value = Utility.EncryptQS("-1");
        SetProjectMenuEnabled();
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetProjectMenuEnabled();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteInsurance();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Utility.DecryptQS(PkProjectId.Value) == "-1")
            Response.Redirect("Project.aspx");

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + MPgMode.Value
               + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("ProjectInsert.aspx?" + Qs);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            Response.Redirect("Project.aspx?PostId=" + PkProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }
    #endregion

    /****************************************************** upload *****************************************************************/
    protected void flpOfInsurance_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageInsurance(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #region Menu Click
    /***************************************************** ProjectMainMenu ***********************************************************/
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = PkPrjReId.Value;
        MPageMode = MPgMode.Value;

        PrjMainMenu MainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, MPageMode, GrdFlt, SrchFlt));
    }

    /******************************************************* ProjectMenu *************************************************************/
    protected void ProjectMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = PkPrjReId.Value;
        MPageMode = MPgMode.Value;

        PrjMenu PrjMenu = new PrjMenu("Insurance", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(PrjMenu.GetRedirectLink(e.Item.Name, PrjReId, MPageMode, GrdFlt, SrchFlt));
    }
    #endregion
    #endregion

    #region Methods

    #region Set Key-Mod
    private void SetKeys()
    {
        try
        {
            MPgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            MPageMode = Utility.DecryptQS(MPgMode.Value);
            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            PrjReId = Utility.DecryptQS(PkPrjReId.Value);


            if (string.IsNullOrEmpty(MPageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetPK();
            SetMode();
            CheckMenueViewPermission();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetPK()
    {
        MPageMode = Utility.DecryptQS(MPgMode.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        InsuranceManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (InsuranceManager.Count == 1)
        {
            if (MPageMode == "View")
                PgMode.Value = Utility.EncryptQS("View");
            else
                PgMode.Value = Utility.EncryptQS("Edit");
            PkInsuranceId.Value = Utility.EncryptQS(InsuranceManager[0]["InsuranceId"].ToString());
        }
        else
        {
            PgMode.Value = Utility.EncryptQS("New");
            PkInsuranceId.Value = Utility.EncryptQS("-2");
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
        btnDelete.Enabled = false;
        btnDelete2.Enabled = false;
        CheckAccess();


        RegDate.Enabled = true;
        ASPxTextBoxInsuranceNo.Enabled = true;
        ASPxTextBoxInsName.Enabled = true;
        ASPxTextBoxInsCode.Enabled = true;
        ASPxTextBoxAmount.Enabled = true;
        TextBoxAddress.Enabled = true;
        txtDescription.Enabled = true;
        flpOfInsurance.Enabled = true;

        CreateDate.DateValue = DateTime.Now;
        RegDate.Text = "";
        ASPxTextBoxInsuranceNo.Text = "";
        ASPxTextBoxInsName.Text = "";
        ASPxTextBoxInsCode.Text = "";
        ASPxTextBoxAmount.Text = "";
        TextBoxAddress.Text = "";
        txtDescription.Text = "";

        ASPxHyperLinkInsurance.ClientVisible = false;
        ASPxHyperLinkInsurance.NavigateUrl = "";
        ASPxHyperLinkInsurance.Text = "";
        ASPxHiddenFieldInsurance["name"] = 0;

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if ((string.IsNullOrEmpty(ProjectId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        btnDelete.Enabled = true;
        btnDelete2.Enabled = true;
        CheckAccess();

        RegDate.Enabled = true;
        ASPxTextBoxInsuranceNo.Enabled = true;
        ASPxTextBoxInsName.Enabled = true;
        ASPxTextBoxInsCode.Enabled = true;
        ASPxTextBoxAmount.Enabled = true;
        TextBoxAddress.Enabled = true;
        txtDescription.Enabled = true;
        flpOfInsurance.Enabled = true;

        SetValues();

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if ((string.IsNullOrEmpty(ProjectId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        btnNew.Enabled = false;
        btnNew2.Enabled = false;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        btnDelete.Enabled = false;
        btnDelete2.Enabled = false;
        CheckAccess();

        RegDate.Enabled = false;
        ASPxTextBoxInsuranceNo.Enabled = false;
        ASPxTextBoxInsName.Enabled = false;
        ASPxTextBoxInsCode.Enabled = false;
        ASPxTextBoxAmount.Enabled = false;
        TextBoxAddress.Enabled = false;
        txtDescription.Enabled = false;
        flpOfInsurance.Enabled = false;

        SetValues();

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }
    #endregion

    private void SetValues()
    {
        InsuranceId = Utility.DecryptQS(PkInsuranceId.Value);

        if ((string.IsNullOrEmpty(InsuranceId)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.InsuranceManager Manager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        Manager.FindByInsuranceId(Convert.ToInt32(InsuranceId));
        if (Manager.Count == 1)
        {
            CreateDate.Text = Manager[0]["CreateDate"].ToString();
            RegDate.Text = Manager[0]["RegDate"].ToString();
            ASPxTextBoxInsuranceNo.Text = Manager[0]["InsuranceNo"].ToString();
            ASPxTextBoxInsName.Text = Manager[0]["InsName"].ToString();
            ASPxTextBoxInsCode.Text = Manager[0]["InsCode"].ToString();
            ASPxTextBoxAmount.Text = Convert.ToDecimal(Manager[0]["Amount"]).ToString("#,#");
            TextBoxAddress.Text = Manager[0]["Address"].ToString();
            txtDescription.Text = Manager[0]["Description"].ToString();

            AttachmentsManager.FindByTableTypeId(Convert.ToInt32(InsuranceId), (int)TSP.DataManager.TableCodes.TSInsurance, (int)TSP.DataManager.TSAttachType.Insurance);
            if (AttachmentsManager.Count > 0)
            {
                ASPxHyperLinkInsurance.ClientVisible = true;
                ASPxHyperLinkInsurance.NavigateUrl = AttachmentsManager[0]["FilePath"].ToString();
                ASPxHyperLinkInsurance.Text = AttachmentsManager[0]["FileName"].ToString();
                ASPxHiddenFieldInsurance["name"] = 1;
            }
        }
        else
        {
            SetLabelWarning("چنین رکوردی وجود ندارد");
        }
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.InsuranceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

    #region Insert-Update
    /*************************************************************** Insert ********************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(InsuranceManager);
        transact.Add(AttachManager);

        try
        {
            transact.BeginSave();

            InsertInsurance(InsuranceManager);
            if (!InsertAttachments(AttachManager))
            {
                transact.CancelSave();
                SetLabelWarning("لطفا فایل بیمه نامه را دوباره انتخاب کنید");
                return;
            }

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err, 'I');
        }
    }

    private void InsertInsurance(TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        DataRow rowInsurance = InsuranceManager.NewRow();

        rowInsurance.BeginEdit();
        rowInsurance["ProjectId"] = ProjectId;
        rowInsurance["InsuranceNo"] = ASPxTextBoxInsuranceNo.Text;
        rowInsurance["InsName"] = ASPxTextBoxInsName.Text;
        rowInsurance["InsCode"] = ASPxTextBoxInsCode.Text;
        rowInsurance["Address"] = TextBoxAddress.Text;
        rowInsurance["RegDate"] = RegDate.Text;
        rowInsurance["Amount"] = ASPxTextBoxAmount.Text;
        rowInsurance["CreateDate"] = CreateDate.Text;
        rowInsurance["Description"] = txtDescription.Text;
        rowInsurance["InActive"] = 0;
        rowInsurance["UserId"] = Utility.GetCurrentUser_UserId();
        rowInsurance["ModifiedDate"] = DateTime.Now;
        rowInsurance.EndEdit();

        InsuranceManager.AddRow(rowInsurance);

        InsuranceManager.Save();

        InsuranceManager.DataTable.AcceptChanges();
        InsuranceId = InsuranceManager[0]["InsuranceId"].ToString();
        PkInsuranceId.Value = Utility.EncryptQS(InsuranceId.ToString());
    }

    private bool InsertAttachments(TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager)
    {
        InsuranceId = Utility.DecryptQS(PkInsuranceId.Value);

        if (Session["InsuranceAttachNameRnd"] != null && Session["InsuranceAttachName"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();

            drAtt.BeginEdit();
            drAtt["TableTypeId"] = InsuranceId;
            drAtt["TableType"] = (int)TSP.DataManager.TableCodes.TSInsurance;
            drAtt["AttachTypeId"] = (int)TSP.DataManager.TSAttachType.Insurance;
            drAtt["FilePath"] = "~/Image/TechnicalServices/Insurance/" + Path.GetFileName(Session["InsuranceAttachNameRnd"].ToString());
            drAtt["FileName"] = Session["InsuranceAttachName"];
            drAtt["UserId"] = Utility.GetCurrentUser_UserId();
            drAtt["ModifiedDate"] = DateTime.Now;
            drAtt.EndEdit();

            AttachManager.AddRow(drAtt);
            AttachManager.Save();

            string ImgSoource = Session["InsuranceAttachNameRnd"].ToString();
            string ImgTarget = Server.MapPath("~/Image/TechnicalServices/Insurance/") + Path.GetFileName(Session["InsuranceAttachNameRnd"].ToString());
            File.Copy(ImgSoource, ImgTarget, true);
            ASPxHyperLinkInsurance.ClientVisible = true;
            ASPxHyperLinkInsurance.NavigateUrl = ImgTarget; //ImgSoource;

            Session["InsuranceAttachNameRnd"] = null;
            Session["InsuranceAttachName"] = null;

            return true;
        }

        return false;
    }

    /******************************************************** Update ***************************************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(InsuranceManager);
        transact.Add(AttachManager);

        try
        {
            transact.BeginSave();

            UpdateInsurance(InsuranceManager);
            if (!UpdateAttachments(AttachManager))
            {
                transact.CancelSave();
                SetLabelWarning("لطفا فایل بیمه نامه را دوباره انتخاب کنید");
                return;
            }

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err, 'U');
        }
    }

    private void UpdateInsurance(TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager)
    {
        InsuranceId = Utility.DecryptQS(PkInsuranceId.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if (string.IsNullOrEmpty(InsuranceId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        InsuranceManager.FindByInsuranceId(Convert.ToInt32(InsuranceId));

        if (InsuranceManager.Count >= 1)
        {
            InsuranceManager[0].BeginEdit();
            InsuranceManager[0]["ProjectId"] = ProjectId;
            InsuranceManager[0]["InsuranceNo"] = ASPxTextBoxInsuranceNo.Text;
            InsuranceManager[0]["InsName"] = ASPxTextBoxInsName.Text;
            InsuranceManager[0]["InsCode"] = ASPxTextBoxInsCode.Text;
            InsuranceManager[0]["Address"] = TextBoxAddress.Text;
            InsuranceManager[0]["RegDate"] = RegDate.Text;
            InsuranceManager[0]["Amount"] = ASPxTextBoxAmount.Text;
            InsuranceManager[0]["CreateDate"] = CreateDate.Text;
            InsuranceManager[0]["Description"] = txtDescription.Text;
            InsuranceManager[0]["InActive"] = 0;
            InsuranceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            InsuranceManager[0]["ModifiedDate"] = DateTime.Now;
            InsuranceManager[0].EndEdit();

            InsuranceManager.Save();

            InsuranceManager.DataTable.AcceptChanges();
            InsuranceId = InsuranceManager[0]["InsuranceId"].ToString();
            PkInsuranceId.Value = Utility.EncryptQS(InsuranceId.ToString());
        }
    }

    private bool UpdateAttachments(TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager)
    {
        InsuranceId = Utility.DecryptQS(PkInsuranceId.Value);

        if (Session["InsuranceAttachNameRnd"] != null && Session["InsuranceAttachName"] != null)
        {
            AttachManager.FindByTableTypeId(Convert.ToInt32(InsuranceId), (int)TSP.DataManager.TableCodes.TSInsurance, (int)TSP.DataManager.TSAttachType.Insurance);
            if (AttachManager.Count > 0)
            {
                AttachManager[0].BeginEdit();
                if ((!string.IsNullOrEmpty(AttachManager[0]["FilePath"].ToString())) && (File.Exists(Server.MapPath(AttachManager[0]["FilePath"].ToString()))))
                {
                    File.Delete(Server.MapPath(AttachManager[0]["FilePath"].ToString()));

                    AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/Insurance/" + Path.GetFileName(Session["InsuranceAttachNameRnd"].ToString());
                    AttachManager[0]["FileName"] = Session["InsuranceAttachName"];
                }
                else
                {
                    AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/Insurance/" + Path.GetFileName(Session["InsuranceAttachNameRnd"].ToString());
                    AttachManager[0]["FileName"] = Session["InsuranceAttachName"];
                }
                AttachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AttachManager[0].EndEdit();
                AttachManager.Save();

                string ImgSoource = Session["InsuranceAttachNameRnd"].ToString();
                string ImgTarget = Server.MapPath("~/Image/TechnicalServices/Insurance/") + Path.GetFileName(Session["InsuranceAttachNameRnd"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

                ASPxHyperLinkInsurance.ClientVisible = true;
                ASPxHyperLinkInsurance.NavigateUrl = ImgTarget; //ImgSoource;
                Session["InsuranceAttachNameRnd"] = null;
                Session["InsuranceAttachName"] = null;

                return true;
            }
            return InsertAttachments(AttachManager);
        }
        return true;
    }

    /******************************************************************************************************************************/
    #endregion

    private void DeleteInsurance()
    {
        InsuranceId = Utility.DecryptQS(PkInsuranceId.Value);
        if (string.IsNullOrEmpty(InsuranceId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        InsuranceManager.FindByInsuranceId(Convert.ToInt32(InsuranceId));

        if (InsuranceManager.Count == 1)
        {
            try
            {
                InsuranceManager[0].Delete();
                int cn = InsuranceManager.Save();
                if (cn == 1)
                {
                    InsuranceManager.DataTable.AcceptChanges();
                    PkInsuranceId.Value = Utility.EncryptQS("-2");
                    PgMode.Value = Utility.EncryptQS("New");
                    SetNewModeKeys();

                    SetLabelWarning("حذف انجام شد");
                }
                else
                {
                    SetLabelWarning("خطایی در حذف انجام گرفته است");
                }
            }
            catch (Exception err)
            {
                SetError(err, 'D');
            }
        }
    }

    #region Warning Methods
    /******************************************************************************************************************************/
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

    protected string SaveImageInsurance(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["InsuranceAttachName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetFileNameWithoutExtension(uploadedFile.PostedFile.FileName) + "_" + Utility.GenRandomNum() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/Insurance/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);

            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["InsuranceAttachNameRnd"] = tempFileName;
        }
        return ret;
    }

    #region Set Menu

    private void SetProjectMainMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Project").Selected = true; //Project
        //MainMenu.Items.FindByName("StatusAnnouncement").Enabled = PrjMainMenu.GetEnabled("StatusAnnouncement");
        //MainMenu.Items.FindByName("BuildingsLicense").Enabled = PrjMainMenu.GetEnabled("BuildingsLicense");
        //MainMenu.Items.FindByName("Timing").Enabled = PrjMainMenu.GetEnabled("Timing");
        MainMenu.Items.FindByName("Contract").Enabled = PrjMainMenu.GetEnabled("Contract");
        MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");
    }

    private void SetProjectMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMenu PrjMenu = new PrjMenu("Insurance", Convert.ToInt32(ProjectId));
        ProjectMenu.Items.FindByName("Insurance").Selected = true; //Insurance
        ProjectMenu.Items.FindByName("Insurance").Enabled = PrjMenu.GetEnabled("Insurance");
        ProjectMenu.Items.FindByName("Block").Enabled = PrjMenu.GetEnabled("Block");
        ProjectMenu.Items.FindByName("PlansMethod").Enabled = PrjMenu.GetEnabled("PlansMethod");
        ProjectMenu.Items.FindByName("RegisteredNo").Enabled = PrjMenu.GetEnabled("RegisteredNo");
        ProjectMenu.Items.FindByName("BaseInfo").Enabled = PrjMenu.GetEnabled("BaseInfo");
    }

    private void CheckMenueViewPermission()
    {
        PrjMenu.ProjectMenusViewPermission PrjMenuPer = PrjMenu.CheckProjectMenusViewPermission();
        ProjectMenu.Items.FindByName("Insurance").Visible = PrjMenuPer.CanViewInsurance;
        ProjectMenu.Items.FindByName("Block").Visible = PrjMenuPer.CanViewBlock;
        ProjectMenu.Items.FindByName("PlansMethod").Visible = PrjMenuPer.CanViewPlansMethod;
        ProjectMenu.Items.FindByName("RegisteredNo").Visible = PrjMenuPer.CanViewRegisteredNo;
        ProjectMenu.Items.FindByName("BaseInfo").Visible = PrjMenuPer.CanViewBaseInfo;

        PrjMainMenu.ProjectMainMenusViewPermission PrjMainMenuPer = PrjMainMenu.CheckProjectMenusViewPermission();
        // MainMenu.Items.FindByName("StatusAnnouncement").Visible = PrjMainMenuPer.CanViewStatusAnnouncement;
        //MainMenu.Items.FindByName("BuildingsLicense").Visible = PrjMainMenuPer.CanViewBuildingsLicense;
        //MainMenu.Items.FindByName("Timing").Visible = PrjMainMenuPer.CanViewTiming;
        MainMenu.Items.FindByName("Contract").Visible = PrjMainMenuPer.CanViewContract;
        MainMenu.Items.FindByName("Implementer").Visible = PrjMainMenuPer.CanViewImplementer;
        MainMenu.Items.FindByName("Observers").Visible = PrjMainMenuPer.CanViewObservers;
        MainMenu.Items.FindByName("Plans").Visible = PrjMainMenuPer.CanViewPlans;
        MainMenu.Items.FindByName("Owner").Visible = PrjMainMenuPer.CanViewOwner;
        MainMenu.Items.FindByName("Project").Visible = PrjMainMenuPer.CanViewProject;
        MainMenu.Items.FindByName("Accounting").Visible = PrjMainMenuPer.CanViewTSAccounting;
        MainMenu.Items.FindByName("Designer").Visible = PrjMainMenuPer.CanViewDesigner;
    }
    #endregion

    #endregion

}