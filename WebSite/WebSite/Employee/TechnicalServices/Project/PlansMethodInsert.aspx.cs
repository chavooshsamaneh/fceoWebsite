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

public partial class Employee_TechnicalServices_Project_PlansMethodInsert : System.Web.UI.Page
{
    string PageMode;
    string ProjectId;
    string PrjReId;
    string PlansMethodId;

    bool IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        ASPxTextBoxPlansMethodNo.Attributes["onkeyup"] = "return ltr_override(event)";

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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansMethodManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PlansMethodId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("PlansMethod.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["MPgMode"]));
                return;
            }

            Session["PlansMethodAttachName"] = null;
            Session["PlansMethodAttachNameRnd"] = null;

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
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + MPgMode.Value +
                     "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("PlansMethod.aspx?" + Qs);
    }


    protected void flpOfPlansMethod_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImagePlansMethod(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    #region SetKey-Modes
    private void SetKeys()
    {
        try
        {
            MPgMode.Value = Server.HtmlDecode(Request.QueryString["MPgMode"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkPlansMethodId.Value = Server.HtmlDecode(Request.QueryString["PlansMethodId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            PlansMethodId = Utility.DecryptQS(PkPlansMethodId.Value);
            string MPageMode = Utility.DecryptQS(MPgMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PlansMethodId) || string.IsNullOrEmpty(MPageMode))
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

        ASPxTextBoxPlansMethodNo.Enabled = true;
        PlansMethodDate.Enabled = true;
        ASPxTextBoxTarakom.Enabled = true;
        ASPxTextBoxEshghalSurface.Enabled = true;
        ASPxComboBoxStructureBuiltPlace.Enabled = true;
        ASPxTextBoxAllowableHeight.Enabled = true;
        ASPxTextBoxCommercialLimitation.Enabled = true;
        ASPxTextBoxBlockNum.Enabled = true;
        flpOfPlansMethod.Enabled = true;
        ASPxTextBoxLocationCriterion.Enabled = true;
        ASPxTextBoxMantelet.Enabled = true;

        ASPxTextBoxPlansMethodNo.Text = "";
        PlansMethodDate.Text = "";
        ASPxTextBoxTarakom.Text = "";
        ASPxTextBoxEshghalSurface.Text = "";
        ASPxComboBoxStructureBuiltPlace.DataBind();
        ASPxComboBoxStructureBuiltPlace.SelectedIndex = -1;
        ASPxTextBoxAllowableHeight.Text = "";
        ASPxTextBoxCommercialLimitation.Text = "";
        ASPxTextBoxBlockNum.Text = "";
        ASPxTextBoxLocationCriterion.Text = "";
        ASPxTextBoxMantelet.Text = "";

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

        ASPxTextBoxPlansMethodNo.Enabled = true;
        PlansMethodDate.Enabled = true;
        ASPxTextBoxTarakom.Enabled = true;
        ASPxTextBoxEshghalSurface.Enabled = true;
        ASPxComboBoxStructureBuiltPlace.Enabled = true;
        ASPxTextBoxAllowableHeight.Enabled = true;
        ASPxTextBoxCommercialLimitation.Enabled = true;
        ASPxTextBoxBlockNum.Enabled = true;
        flpOfPlansMethod.Enabled = true;
        ASPxTextBoxLocationCriterion.Enabled = true;
        ASPxTextBoxMantelet.Enabled = true;

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


        ASPxTextBoxPlansMethodNo.Enabled = false;
        PlansMethodDate.Enabled = false;
        ASPxTextBoxTarakom.Enabled = false;
        ASPxTextBoxEshghalSurface.Enabled = false;
        ASPxComboBoxStructureBuiltPlace.Enabled = false;
        ASPxTextBoxAllowableHeight.Enabled = false;
        ASPxTextBoxCommercialLimitation.Enabled = false;
        ASPxTextBoxBlockNum.Enabled = false;
        flpOfPlansMethod.Enabled = false;
        ASPxTextBoxLocationCriterion.Enabled = false;
        ASPxTextBoxMantelet.Enabled = false;

        SetValues();

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }
    #endregion
    private void SetValues()
    {
        PlansMethodId = Utility.DecryptQS(PkPlansMethodId.Value);

        if ((string.IsNullOrEmpty(PlansMethodId)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.PlansMethodManager Manager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        Manager.FindByPlansMethodId(int.Parse(PlansMethodId));
        if (Manager.Count == 1)
        {
            ASPxTextBoxPlansMethodNo.Text = Manager[0]["PlansMethodNo"].ToString();
            PlansMethodDate.Text = Manager[0]["RegisteredDate"].ToString();
            ASPxTextBoxTarakom.Text = Manager[0]["Tarakom"].ToString();
            ASPxTextBoxEshghalSurface.Text = Manager[0]["EshghalSurface"].ToString();
            ASPxComboBoxStructureBuiltPlace.DataBind();
            ASPxComboBoxStructureBuiltPlace.SelectedIndex = ASPxComboBoxStructureBuiltPlace.Items.IndexOfValue(Manager[0]["StructureBuiltPlaceId"]);
            ASPxTextBoxAllowableHeight.Text = Manager[0]["AllowableHeight"].ToString();
            ASPxTextBoxCommercialLimitation.Text = Manager[0]["CommercialLimitation"].ToString();
            ASPxTextBoxBlockNum.Text = Manager[0]["BlockNum"].ToString();
            ASPxTextBoxLocationCriterion.Text = Manager[0]["LocationCriterion"].ToString();
            ASPxTextBoxMantelet.Text = Manager[0]["Mantelet"].ToString();

            if (Convert.ToBoolean(Manager[0]["InActive"]))
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                //btnDelete.Enabled = false;
                //btnDelete2.Enabled = false;
            }

            AttachmentsManager.FindByTableTypeId(Convert.ToInt32(PlansMethodId), (int)TSP.DataManager.TableCodes.TSPlansMethod, (int)TSP.DataManager.TSAttachType.PlansMethod);
            if (AttachmentsManager.Count > 0)
            {
                ASPxHyperLinkPlansMethod.ClientVisible = true;
                ASPxHyperLinkPlansMethod.NavigateUrl = AttachmentsManager[0]["FilePath"].ToString();
                ASPxHyperLinkPlansMethod.Text = AttachmentsManager[0]["FileName"].ToString();
                ASPxHiddenFieldPlansMethod["name"] = 1;
            }
        }
        else
        {
            SetLabelWarning("چنین رکوردی وجود ندارد");
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    public void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansMethodManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

        if (CheckActivePlansMethod())
        {
            SetLabelWarning("برای این پروژه دستور نقشه ثبت شده است");
            return;
        }

        if (ASPxTextBoxEshghalSurface.Text == "0")
        {
            SetLabelWarning("لطفا سطح اشغال را وارد کنید");
            return;
        }

        if (ASPxTextBoxTarakom.Text == "0")
        {
            SetLabelWarning("لطفا درصد تراکم را وارد کنید");
            return;
        }

        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(PlansMethodManager);
        transact.Add(AttachManager);

        try
        {
            transact.BeginSave();

            InsertPlansMethod(PlansMethodManager);
            if (!InsertAttachments(AttachManager))
            {
                transact.CancelSave();
                SetLabelWarning("لطفا فایل دستور نقشه را دوباره انتخاب کنید");
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

    private void InsertPlansMethod(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        DataRow rowPlansMethod = PlansMethodManager.NewRow();

        rowPlansMethod.BeginEdit();

        rowPlansMethod["ProjectId"] = ProjectId;
        rowPlansMethod["PlansMethodNo"] = ASPxTextBoxPlansMethodNo.Text;
        rowPlansMethod["RegisteredDate"] = PlansMethodDate.Text;
        rowPlansMethod["StructureBuiltPlaceId"] = ASPxComboBoxStructureBuiltPlace.Value;
        rowPlansMethod["EshghalSurface"] = ASPxTextBoxEshghalSurface.Text;
        rowPlansMethod["Tarakom"] = ASPxTextBoxTarakom.Text;
        rowPlansMethod["AllowableHeight"] = ASPxTextBoxAllowableHeight.Text;
        if (ASPxTextBoxCommercialLimitation.Text != "")
            rowPlansMethod["CommercialLimitation"] = ASPxTextBoxCommercialLimitation.Text;
        rowPlansMethod["BlockNum"] = ASPxTextBoxBlockNum.Text;
        rowPlansMethod["InActive"] = 0;
        rowPlansMethod["PrjReId"] = PrjReId;
        rowPlansMethod["LocationCriterion"] = ASPxTextBoxLocationCriterion.Text;
        if (ASPxTextBoxMantelet.Text != "")
            rowPlansMethod["Mantelet"] = ASPxTextBoxMantelet.Text;
        rowPlansMethod["UserId"] = Utility.GetCurrentUser_UserId();
        rowPlansMethod["ModifiedDate"] = DateTime.Now;

        rowPlansMethod.EndEdit();

        PlansMethodManager.AddRow(rowPlansMethod);
        PlansMethodManager.Save();

        PlansMethodManager.DataTable.AcceptChanges();
        PlansMethodId = PlansMethodManager[0]["PlansMethodId"].ToString();
        PkPlansMethodId.Value = Utility.EncryptQS(PlansMethodId.ToString());
    }

    private bool InsertAttachments(TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager)
    {
        PlansMethodId = Utility.DecryptQS(PkPlansMethodId.Value);

        if (Session["PlansMethodAttachNameRnd"] != null && Session["PlansMethodAttachName"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();

            drAtt.BeginEdit();
            drAtt["TableTypeId"] = PlansMethodId;
            drAtt["TableType"] = (int)TSP.DataManager.TableCodes.TSPlansMethod;
            drAtt["AttachTypeId"] = (int)TSP.DataManager.TSAttachType.PlansMethod;
            drAtt["FilePath"] = "~/Image/TechnicalServices/PlansMethod/" + Path.GetFileName(Session["PlansMethodAttachNameRnd"].ToString());
            drAtt["FileName"] = Session["PlansMethodAttachName"];
            drAtt["UserId"] = Utility.GetCurrentUser_UserId();
            drAtt["ModifiedDate"] = DateTime.Now;
            drAtt.EndEdit();

            AttachManager.AddRow(drAtt);
            AttachManager.Save();

            string ImgSoource = Session["PlansMethodAttachNameRnd"].ToString();
            string ImgTarget = Server.MapPath("~/Image/TechnicalServices/PlansMethod/") + Path.GetFileName(Session["PlansMethodAttachNameRnd"].ToString());
            File.Copy(ImgSoource, ImgTarget, true);
            ASPxHyperLinkPlansMethod.ClientVisible = true;
            ASPxHyperLinkPlansMethod.NavigateUrl = ImgTarget; //ImgSoource;

            Session["PlansMethodAttachNameRnd"] = null;
            Session["PlansMethodAttachName"] = null;

            return true;
        }

        return false;
    }

    private bool CheckActivePlansMethod()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        PlansMethodManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (PlansMethodManager.Count > 0)
            return true;
        else
            return false;
    }

    /************************************************************* Update ***************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(PlansMethodManager);
        transact.Add(AttachManager);

        try
        {
            transact.BeginSave();

            UpdatePlansMethod(PlansMethodManager);
            if (!UpdateAttachments(AttachManager))
            {
                transact.CancelSave();
                SetLabelWarning("لطفا فایل دستور نقشه را دوباره انتخاب کنید");
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

    private void UpdatePlansMethod(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        PlansMethodId = Utility.DecryptQS(PkPlansMethodId.Value);
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        if (string.IsNullOrEmpty(PlansMethodId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        PlansMethodManager.FindByPlansMethodId(Convert.ToInt32(PlansMethodId));


        if (PlansMethodManager.Count >= 1)
        {
            PlansMethodManager[0].BeginEdit();
            PlansMethodManager[0]["ProjectId"] = ProjectId;
            PlansMethodManager[0]["PlansMethodNo"] = ASPxTextBoxPlansMethodNo.Text;
            PlansMethodManager[0]["RegisteredDate"] = PlansMethodDate.Text;
            PlansMethodManager[0]["StructureBuiltPlaceId"] = ASPxComboBoxStructureBuiltPlace.Value;
            PlansMethodManager[0]["EshghalSurface"] = ASPxTextBoxEshghalSurface.Text;
            PlansMethodManager[0]["Tarakom"] = ASPxTextBoxTarakom.Text;
            PlansMethodManager[0]["AllowableHeight"] = ASPxTextBoxAllowableHeight.Text;
            if (ASPxTextBoxCommercialLimitation.Text != "")
                PlansMethodManager[0]["CommercialLimitation"] = ASPxTextBoxCommercialLimitation.Text;
            else
                PlansMethodManager[0]["CommercialLimitation"] = DBNull.Value;
            PlansMethodManager[0]["LocationCriterion"] = ASPxTextBoxLocationCriterion.Text;
            if (ASPxTextBoxMantelet.Text != "")
                PlansMethodManager[0]["Mantelet"] = ASPxTextBoxMantelet.Text;
            else
                PlansMethodManager[0]["Mantelet"] = DBNull.Value;
            PlansMethodManager[0]["BlockNum"] = ASPxTextBoxBlockNum.Text;
            PlansMethodManager[0]["InActive"] = 0;
            PlansMethodManager[0]["PrjReId"] = PrjReId;
            PlansMethodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansMethodManager[0]["ModifiedDate"] = DateTime.Now;
            PlansMethodManager[0].EndEdit();

            PlansMethodManager.Save();

            PlansMethodManager.DataTable.AcceptChanges();
            PlansMethodId = PlansMethodManager[0]["PlansMethodId"].ToString();
            PkPlansMethodId.Value = Utility.EncryptQS(PlansMethodId.ToString());
        }
    }

    private bool UpdateAttachments(TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager)
    {
        PlansMethodId = Utility.DecryptQS(PkPlansMethodId.Value);

        if (Session["PlansMethodAttachNameRnd"] != null && Session["PlansMethodAttachName"] != null)
        {
            AttachManager.FindByTableTypeId(Convert.ToInt32(PlansMethodId), (int)TSP.DataManager.TableCodes.TSPlansMethod, (int)TSP.DataManager.TSAttachType.PlansMethod);
            if (AttachManager.Count > 0)
            {
                AttachManager[0].BeginEdit();
                if ((!string.IsNullOrEmpty(AttachManager[0]["FilePath"].ToString())) && (File.Exists(Server.MapPath(AttachManager[0]["FilePath"].ToString()))))
                {
                    File.Delete(Server.MapPath(AttachManager[0]["FilePath"].ToString()));

                    AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/PlansMethod/" + Path.GetFileName(Session["PlansMethodAttachNameRnd"].ToString());
                    AttachManager[0]["FileName"] = Session["PlansMethodAttachName"];

                }
                else
                {
                    AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/PlansMethod/" + Path.GetFileName(Session["PlansMethodAttachNameRnd"].ToString());
                    AttachManager[0]["FileName"] = Session["PlansMethodAttachName"];

                }
                AttachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AttachManager[0].EndEdit();
                AttachManager.Save();

                string ImgSoource = Session["PlansMethodAttachNameRnd"].ToString();
                string ImgTarget = Server.MapPath("~/Image/TechnicalServices/PlansMethod/") + Path.GetFileName(Session["PlansMethodAttachNameRnd"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

                ASPxHyperLinkPlansMethod.ClientVisible = true;
                ASPxHyperLinkPlansMethod.NavigateUrl = ImgTarget; //ImgSoource;
                Session["PlansMethodAttachNameRnd"] = null;
                Session["PlansMethodAttachName"] = null;

                return true;
            }
            return InsertAttachments(AttachManager);
        }
        return true;
    }
    #endregion

    #region SetError-Warning
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

    /****************************************************** upload ***************************************************/

    protected string SaveImagePlansMethod(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["PlansMethodAttachName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetFileNameWithoutExtension(uploadedFile.PostedFile.FileName) + "_" + Utility.GenRandomNum() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/PlansMethod/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);

            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["PlansMethodAttachNameRnd"] = tempFileName;
        }
        return ret;
    }

    #region WorkFlow Methods

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