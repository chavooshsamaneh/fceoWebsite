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
using System.IO;
using DevExpress.Web;

public partial class Members_TechnicalServices_Project_PlanInfo : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    string ProjectId;
    string PrjReId;
    string PlansId;
    string PageMode;
    int _PlansControlerId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansControlerId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansControlerId"] = value;
        }
    }
    DataTable dtViewPoint = null;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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
            if (Request.QueryString["ProjectId"] == null || Request.QueryString["PlnId"] == null || Request.QueryString["PageMode"] == null || Request.QueryString["PrjReId"] == null)
            {
                Response.Redirect("ControlPlans.aspx");
            }

            Session["PlansControlerViewPointManager"] = null;
            CreateViwPointDataTable();

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

        }
        if (Session["PlansControlerViewPointManager"] != null)
        {
            dtViewPoint = (DataTable)Session["PlansControlerViewPointManager"];
            GridViewViewPoint.DataSource = dtViewPoint;
            GridViewViewPoint.DataBind();
        }
        if (this.ViewState["BtnSave"] != null)
            btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        HiddenFieldPrjDes["PageMode"] = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldPrjDes["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (PageMode)
        {
            case "Edit":
                Update(TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan);
                break;
        }
    }

    protected void btnSaveAndonfirm_Click(object sender, EventArgs e)
    {
        Update(TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess);
    }

    protected void btnSaveAndSendToDesigner_Click(object sender, EventArgs e)
    {
        Update(TSP.DataManager.WorkFlowTask.SavePlanInfo);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["UrlReferrer"] == null)
            Response.Redirect("Project.aspx");

        string UrlReferrer = Utility.DecryptQS(Request.QueryString["UrlReferrer"].ToString());
        string Url = "Project.aspx";
        switch (UrlReferrer)
        {
            case "Plans":
                Url = "Plans.aspx?ProjectId=" + HiddenFieldPrjDes["ProjectId"].ToString()
                + "&PrjReId=" + HiddenFieldPrjDes["PrjReId"].ToString()
                + "&PageMode=" + HiddenFieldPrjDes["PageMode"].ToString();
                break;
            case "ControlPlans":
                Url = "ControlPlans.aspx";
                break;
        }
        Response.Redirect(Url);
    }

    protected void btnAddViewPoint_Click(object sender, EventArgs e)
    {
        ViewPointRowInserting();
        txtSubject.Focus();
    }

    protected void flpControlerAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageAttach(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    #region Set Key-Mode
    private void SetKeys()
    {
        HiddenFieldPrjDes["PlansId"] = Request.QueryString["PlnId"].ToString();
        HiddenFieldPrjDes["PageMode"] = Request.QueryString["PageMode"];
        HiddenFieldPrjDes["ProjectId"] = Request.QueryString["ProjectId"];
        HiddenFieldPrjDes["PrjReId"] = Request.QueryString["PrjReId"];

        PrjReId = Utility.DecryptQS(HiddenFieldPrjDes["PrjReId"].ToString());
        PlansId = Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString());
        ProjectId = Utility.DecryptQS(HiddenFieldPrjDes["ProjectId"].ToString());
        PageMode = Utility.DecryptQS(HiddenFieldPrjDes["PageMode"].ToString());
        HiddenFieldPrjDes["MsgAgrrement"] = "بدینوسیله متعهد می گردم؛ به عنوان مهندس بازبین پروژه با کد" + ProjectId.ToString() + "  موارد مندرج در ماده ۴ بند ۴ - ۵ از مبحث دوم مقررات ملی ساختمان را رعایت نموده و مشخصا در دفتر مهندسی طراحی و طراح حقوقی تهیه کننده طرح مذکور به هیچ شکل ذی نفع نمی باشم.در غیر اینصورت، مسئولیت تمام عواقب قانونی بر عهده اینجانب بوده و سازمان مجاز به برخورد برابر ضوابط و مقررات می باشد.";
        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PlansId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        FillProjectInfo(Convert.ToInt32(PrjReId));
        SetMode(PageMode);

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int ViewState = WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSPlansConfirming, Convert.ToInt32(PlansId), -1, "مشاهده اطلاعات نقشه توسط بازبین", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.ViewInfo);
        if (ViewState == -4)
        {
            SetLabelWarning("خطایی در بازیابی اطلاعات ایجاد شده است");
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetEditModeKeys()
    {
        btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled =
           btnSave.Enabled = btnSave2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        SetVisible(true);
        RoundPanelPlans.HeaderText = "ویرایش";
        FillForm();

        CheckWorkFlowPermission();
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void SetViewModeKeys()
    {
        btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled =
        btnSave.Enabled = btnSave2.Enabled = false;
        //GridViewViewPoint.Columns["Delete"].Visible = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        SetVisible(false);

        RoundPanelPlans.HeaderText = "مشاهده";
        FillForm();
        CheckWorkFlowPermission();
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    #endregion
    private void SetVisible(bool Visible)
    {
        //  GridViewViewPoint.Columns["Delete"].Visible =
        tblTdAddViewPoint.Visible =
        tblTRControlerDes.Visible =
        tblTrViewPoint.Visible = tblflpViewPoint.Visible = Visible;
    }
    #region Fill
    private void FillForm()
    {
        PlansId = Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString());
        FillPlan(Convert.ToInt32(PlansId));
        FillPlanAttachment(PlansId);
        FillPlansControlerViewPoint(Convert.ToInt32(PlansId));
        TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
        PlansControlerManager.FindActiveControlerByPlansId(Convert.ToInt32(PlansId));
        if (PlansControlerManager.Count > 1)
        {
            lblWarning.Text = "این نقشه دارای " + PlansControlerManager.Count.ToString() + " بازبین می باشد.";
            lblWarning.Visible = true;
            PlansControlerManager.FindByPlansControlerId(_PlansControlerId);
            if (PlansControlerManager.Count == 0)
            {
                SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                return;
            }
            if (Convert.ToInt32(PlansControlerManager[0]["IsPlanConfirmed"]) == 1)
            {
                lblWarning.Text += "این نقشه قبلا از سوی شما تایید شده است و در انتظار تایید توسط بازبین دوم می باشد.";
                return;
            }
        }
        else lblWarning.Visible = false;


    }

    protected void FillProjectInfo(int id)
    {
        prjInfo.Fill(id);
    }

    private void FillPlan(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            txtPlanDes.Text = PlansManager[0]["Description"].ToString();
            txtPlanNo.Text = PlansManager[0]["No"].ToString();
            txtPlanType.Text = PlansManager[0]["Title"].ToString();
        }
    }

    private void FillPlanAttachment(string PlansId)
    {
        ObjectDataSourceAttachments.SelectParameters["TableTypeId"].DefaultValue = PlansId;
        ObjectDataSourceAttachments.SelectParameters["AttachTypeId"].DefaultValue = "-1";
    }


    private void FillPlansControlerViewPoint(int PlansId)
    {
        dtViewPoint.Clear();
        TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
        TSP.DataManager.TechnicalServices.PlansControlerViewPointManager PlansControlerViewPointManager = new TSP.DataManager.TechnicalServices.PlansControlerViewPointManager();
        PlansControlerManager.FindPlanOfControler(Utility.GetCurrentUser_MeId(), PlansId, 0);
        if (PlansControlerManager.Count != 0)
        {
            _PlansControlerId = Convert.ToInt32(PlansControlerManager[0]["PlansControlerId"].ToString());
            if (Session["PlansControlerViewPointManager"] == null)
                CreateViwPointDataTable();
            PlansControlerViewPointManager.FindByPlansId(PlansId);
            for (int i = 0; i < PlansControlerViewPointManager.Count; i++)
            {
                DataRow dr = dtViewPoint.NewRow();
                dr["ControllerName"] = PlansControlerViewPointManager[i]["ControllerName"];
                dr["PlansControlerId"] = PlansControlerViewPointManager[i]["PlansControlerId"];
                dr["Date"] = PlansControlerViewPointManager[i]["Date"];
                dr["SheetNo"] = PlansControlerViewPointManager[i]["SheetNo"];
                dr["ViewPoint"] = PlansControlerViewPointManager[i]["ViewPoint"];
                dr["Subject"] = PlansControlerViewPointManager[i]["Subject"];
                dr["FileUrl"] = PlansControlerViewPointManager[i]["FileUrl"];
                dtViewPoint.Rows.Add(dr);
            }
            dtViewPoint.AcceptChanges();
            GridViewViewPoint.DataSource = dtViewPoint;
            GridViewViewPoint.DataBind();
        }

    }

    private void CreateViwPointDataTable()
    {
        if (Session["PlansControlerViewPointManager"] == null)
        {
            dtViewPoint = new DataTable();
            dtViewPoint.Columns.Add("Id");
            dtViewPoint.Columns["Id"].AutoIncrement = true;
            dtViewPoint.Columns["Id"].AutoIncrementSeed = 1;
            dtViewPoint.Constraints.Add("PK_ID", dtViewPoint.Columns["Id"], true);
            dtViewPoint.Columns.Add("PlansControlerId");
            dtViewPoint.Columns.Add("Date");
            dtViewPoint.Columns.Add("SheetNo");
            dtViewPoint.Columns.Add("ViewPoint");
            dtViewPoint.Columns.Add("Subject");
            dtViewPoint.Columns.Add("FileUrl");
            dtViewPoint.Columns.Add("ControllerName");
            Session["PlansControlerViewPointManager"] = dtViewPoint;
        }
        else
            dtViewPoint = (DataTable)Session["PlansControlerViewPointManager"];

        GridViewViewPoint.DataSource = dtViewPoint;
        GridViewViewPoint.DataBind();
    }
    #endregion
    private void ViewPointRowInserting()
    {
        try
        {
            if (Session["PlansControlerViewPointManager"] == null)
                CreateViwPointDataTable();

            DataRow ViewPointRow = dtViewPoint.NewRow();
            ViewPointRow["ControllerName"] = Utility.GetCurrentUser_FullName();
            ViewPointRow["PlansControlerId"] = _PlansControlerId;
            ViewPointRow["Date"] = Utility.GetDateOfToday();
            ViewPointRow["SheetNo"] = txtSheetNo.Text.Trim();
            ViewPointRow["ViewPoint"] = txtViewPoint.Text.Trim();
            ViewPointRow["Subject"] = txtSubject.Text.Trim();
            if (Session["ControlerFileURL"] != null)
            {
                ViewPointRow["FileUrl"] = Session["ControlerFileURL"];
                Session["ControlerFileURL"] = null;
            }
            dtViewPoint.Rows.Add(ViewPointRow);
            GridViewViewPoint.DataSource = dtViewPoint;
            GridViewViewPoint.KeyFieldName = "Id";
            GridViewViewPoint.DataBind();
            ClearViewPoint();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            SetLabelWarning("خطا در اضافه به لیست ایجاد شده است");
        }
    }



    private void ClearViewPoint()
    {
        txtViewPoint.Text = "";
        txtSubject.Text = "";
        txtSheetNo.Text = "";
    }

    protected string SaveImageAttach(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "PlnId_"+ Utility.DecryptQS(Request.QueryString["PlnId"].ToString()) + "ProjectId_" + Utility.DecryptQS(Request.QueryString["ProjectId"].ToString()) + "PrjReId_"+ Utility.DecryptQS(Request.QueryString["PrjReId"].ToString()) +  Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/TechnicalServices/Plans/PlanControler/") + ret) == true);

            string FileName = "";
            Session["ControlerFileURL"] = "~/image/TechnicalServices/Plans/PlanControler/" + ret;
            FileName = MapPath("~/image/TechnicalServices/Plans/PlanControler/") + ret;

            uploadedFile.SaveAs(FileName, true);
        }
        return ret;
    }
    /****************************************************************************************************************************************/
    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
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
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
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

    #region Update
    private void Update(TSP.DataManager.WorkFlowTask WorkFlowTask)
    {
        if (Session["PlansControlerViewPointManager"] == null)
            CreateViwPointDataTable();
        if (IsPageRefresh)
        {
            return;
        }
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.PlansControlerViewPointManager PlansControlerViewPointManager = new TSP.DataManager.TechnicalServices.PlansControlerViewPointManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
        //PlansControlerManager.FindByPlansControlerId(_PlansControlerId);
        //if (PlansControlerManager.Count == 0)
        //{
        //    SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
        //    return;
        //}
        //if (Convert.ToInt32(PlansControlerManager[0]["IsPlanConfirmed"]) == 1)
        //{
        //    SetLabelWarning("این نقشه قبلا از سوی شما تایید شده است و در انتظار تایید توسط بازبین دوم می باشد.");
        //    return;
        //}
        if (WorkFlowTask != TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess && ((DataTable)Session["PlansControlerViewPointManager"]).Rows.Count == 0)
        {

            SetLabelWarning("نواقص نقشه را وارد نمایید.");
            return;
        }
        try
        {
            Boolean IsAllControlerConfirmedPlas = true;
            PlansId = Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString());
            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan)
            {
                int ViewState = WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSPlansConfirming, Convert.ToInt32(PlansId), -1, "ذخیره نواقص نقشه توسط بازبین", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
                if (ViewState == -4)
                {
                    SetLabelWarning("خطا در ثبت تاریخچه ثبت اطلاعات ایجاد شده است");
                    return;
                }
            }
            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.SavePlanInfo || WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
            {
                transact.Add(PlansControlerViewPointManager);
                transact.Add(WorkFlowStateManager);
                transact.Add(WorkFlowTaskManager);
                transact.Add(PlansManager);
                transact.Add(PlansControlerManager);
                transact.BeginSave();
                if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
                {
                    PlansControlerManager.FindActiveControlerByPlansId(Convert.ToInt32(PlansId));
                    if (PlansControlerManager.Count > 1)
                    {
                        for (int i = 0; i < PlansControlerManager.Count; i++)
                        {
                            if (Convert.ToInt16(PlansControlerManager[i]["IsPlanConfirmed"]) != 1 && Convert.ToInt32(PlansControlerManager[i]["PlansControlerId"]) != _PlansControlerId)
                                IsAllControlerConfirmedPlas = false;
                        }
                    }

                    if (IsAllControlerConfirmedPlas)
                    {
                        PlansManager.FindByPlansId(Convert.ToInt32(PlansId));
                        if (PlansManager.Count <= 0)
                        {
                            transact.CancelSave();
                            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                            return;
                        }

                        PlansManager[0].BeginEdit();
                        PlansManager[0]["IsConfirmed"] = (int)TSP.DataManager.TSPlansConfirming.Confirmed;
                        PlansManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        PlansManager[0].EndEdit();
                        if (PlansManager.Save() <= 0)
                        {
                            transact.CancelSave();
                            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                            return;
                        }
                        PlansManager.DataTable.AcceptChanges();
                    }

                }
                if (IsAllControlerConfirmedPlas)
                    InsertWF(Convert.ToInt32(PlansId), WorkFlowStateManager, WorkFlowTaskManager, WorkFlowTask, PlansManager);

                #region PlansControlerManager بروزرسانی 
                PlansControlerManager.FindByPlansControlerId(_PlansControlerId);
                if (PlansControlerManager.Count == 0)
                {
                    transact.CancelSave();
                    SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                    return;
                }
                PlansControlerManager[0].BeginEdit();
                if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
                    PlansControlerManager[0]["IsPlanConfirmed"] = 1;
                else
                    PlansControlerManager[0]["IsPlanConfirmed"] = 0;
                PlansControlerManager[0].EndEdit();
                PlansControlerManager.Save();
                #endregion
            }
            #region PlansControlerViewPoint

            dtViewPoint = (DataTable)Session["PlansControlerViewPointManager"];

            if (dtViewPoint.GetChanges() != null)
            {

                DataRow[] delRows = dtViewPoint.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtViewPoint.Select(null, null, DataViewRowState.Added);

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drViewPoint = PlansControlerViewPointManager.NewRow();
                        drViewPoint["PlansControlerId"] = insRows[i]["PlansControlerId"];
                        drViewPoint["SheetNo"] = insRows[i]["SheetNo"];
                        drViewPoint["ViewPoint"] = insRows[i]["ViewPoint"];
                        drViewPoint["Subject"] = insRows[i]["Subject"];
                        drViewPoint["FileUrl"] = insRows[i]["FileUrl"];
                        drViewPoint["Date"] = Utility.GetDateOfToday();
                        drViewPoint["UserId"] = Utility.GetCurrentUser_UserId();
                        drViewPoint["ModifiedDate"] = DateTime.Now;

                        PlansControlerViewPointManager.AddRow(drViewPoint);

                        if (PlansControlerViewPointManager.Save() < 0)
                        {
                            transact.CancelSave();
                            SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                            return;
                        }
                        PlansControlerViewPointManager.DataTable.AcceptChanges();

                    }

                }


            }
            #endregion

            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.SavePlanInfo || WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
            {
                transact.EndSave();
                SetViewModeKeys();
            }
            else
                SetEditModeKeys();
            string SMSError = "";
            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.SavePlanInfo || WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
            {
                #region Send SMS
                try
                {
                    TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                    DataTable dtExtraInfo = WorkFlowPermission.GetMemberInfoForSMSByWFCode((int)TSP.DataManager.WorkFlows.TSPlansConfirming, Convert.ToInt32(PlansId), (int)WorkFlowTask, Utility.GetCurrentUser_LoginType());
                    if (dtExtraInfo.Rows.Count > 0)
                    {
                        string SMSBody = dtExtraInfo.Rows[0]["SMSBody"].ToString();
                        if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess && IsAllControlerConfirmedPlas == false)
                            SMSBody += "قابل ذکر است این نقشه دارای بیش از یک بازبین می باشد و در صورت تایید تمامی بازبین ها، نقشه تایید نهایی می شود.";
                        SendSMSNotification(Utility.Notifications.NotificationTypes.TSPlanConfirming, SMSBody, dtExtraInfo.Rows[0]["SMSMobileNo"].ToString(), dtExtraInfo.Rows[0]["SMSMeId"].ToString(), (TSP.DataManager.UserType)(Convert.ToInt32(dtExtraInfo.Rows[0]["SMSUltId"])));
                    }
                    else SMSError = "اطلاعات جهت ارسال پیامک یافت نشد";
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    SMSError = "خطا در ارسال پیامک رخ داده است";
                }
                #endregion
            }
            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess && IsAllControlerConfirmedPlas == false)
                SetLabelWarning("تایید نقشه توسط شما در سیستم ثبت شد.نقشه بجز شما دارای بازبین دیگری می باشد و پس از تایید وی  نقشه تایید نهایی می شود " + SMSError);
            else
                SetLabelWarning("ذخیره انجام شد." + SMSError);
            HiddenFieldPrjDes["PageMode"] = Utility.EncryptQS("Edit");
        }
        catch (Exception err)
        {
            if (WorkFlowTask == TSP.DataManager.WorkFlowTask.SavePlanInfo || WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
            {
                transact.CancelSave();
            }
            SetError(err);
        }
    }
    private bool InsertWF(int TableId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.WorkFlowTask WorkFlowTask, TSP.DataManager.TechnicalServices.PlansManager PlansManager)
    {
        int TaskId = -1;
        int TaskCode = (int)WorkFlowTask;

        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
            return false;
        }
        TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

        int CurrentNmcId = Utility.GetCurrentUser_MeId();
        string Desc = "ارسال اتوماتیک گردش کار نقشه توسط بازبین";
        if (WorkFlowTask == TSP.DataManager.WorkFlowTask.SavePlanInfo)
            Desc = "ارسال اتوماتیک گردش کار نقشه توسط بازبین به طراح جهت اصلاح نقشه";
        if (WorkFlowTask == TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess)
            Desc = "تایید گردش کار نقشه توسط بازبین";
        int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, (int)TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Desc);
        if (WfStart > 0)
        {
            if (PlansManager.UpdateRequestTaskId(TableId, TaskId, WfStart) == 0)
                return true;
        }

        SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
        return false;
    }
    #endregion

    #region WorkFlow Permission Methods

    private void CheckWorkFlowPermission()
    {
        PageMode = Utility.DecryptQS(HiddenFieldPrjDes["PageMode"].ToString());
        if (PageMode != "New")
            CheckWorkFlowPermissionForEdit();
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        PageMode = Utility.DecryptQS(HiddenFieldPrjDes["PageMode"].ToString());
        int TableId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString()));
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int ControlerConfirmingPlanTask = (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan;

                    if (CurrentTaskCode == ControlerConfirmingPlanTask)
                    {
                        if (PageMode == "View")
                        {

                            this.ViewState["BtnSave"] = btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled
                          = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled =
                          btnSave.Enabled = btnSave2.Enabled = false;
                            this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = true;
                        }
                        if (PageMode == "Edit")
                        {
                            this.ViewState["BtnSave"] = btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled
                          = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled =
                          btnSave.Enabled = btnSave2.Enabled = true;
                            this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = false;
                        }

                        return;
                    }
                }
            }
        }
        this.ViewState["BtnSave"] = btnSaveAndSendToDesigner.Enabled = btnSaveAndSendToDesigner2.Enabled
            = btnSaveAndonfirm.Enabled = btnSaveAndonfirm2.Enabled
            = btnSave.Enabled = btnSave2.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = false;
        //this.ViewState["ControlerDelete"] = GridViewViewPoint.Columns["Delete"].Visible = false;
    }

    #endregion
    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string SMSBody, string SMSMobileNo, string SMSMeId, TSP.DataManager.UserType SMSUltId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)SMSUltId).ToString();
            dr["Description"] = SMSBody;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, SMSBody);
        }
    }
    #endregion

}
