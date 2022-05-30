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

public partial class Office_OfficeInfo_OfficeJobShow : System.Web.UI.Page
{

    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
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
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("OfficeJob.aspx");
                return;
            }

            ODBJobCountry.CacheDuration = Utility.GetCacheDuration();
            OdbFactorDocuments.FilterParameters[0].DefaultValue = "2";

            SetKeys();
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["JobQUpload"] = null;
        Session["TblOfJobQ"] = null;
        Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] 
            + "&OfReId=" + OfficeRequest.Value + "&Dprt=" + HDMode.Value);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string JhId = Utility.DecryptQS(JobId.Value);

        if (string.IsNullOrEmpty(JhId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            Enable();

            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string JhId = Utility.DecryptQS(JobId.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        OfManager.FindByCode(int.Parse(OfId));
        if ((bool)OfManager[0]["IsLock"] == true)
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string OfficeLockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 1, 1);

            string lockers = Utility.GetFormattedObject(OfficeLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;
        }

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
                string Department = Utility.DecryptQS(HDMode.Value);
                if (Utility.IsDBNullOrNullValue(JhId) || Utility.IsDBNullOrNullValue(OfReId) || Utility.IsDBNullOrNullValue(Department))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                    return;
                Edit(int.Parse(JhId));
                break;
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
     


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        JobId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();

    }

    void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());
            OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
            JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
            OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Dprt"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string JhId = Utility.DecryptQS(JobId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        string Department = Utility.DecryptQS(HDMode.Value);


        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfReId) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(JhId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode, Department, int.Parse(JhId));
    }
    void SetMode(string PageMode, string Department, int JhId)
    {
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        switch (PageMode)
        {
            case "View":
                Disable();
                btnEdit.Enabled = true;
                btnEdit2.Enabled = true;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(JhId);
                ASPxRoundPanel2.HeaderText = "مشاهده";
                break;
            case "New":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                ClearForm();
                break;
            case "Edit":
                Enable();
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                FillForm(JhId);
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                break;
        }

        switch (Department)
        {
            case "Home":
                SetEnabled(false);
                break;

            case "Document":
                if (string.IsNullOrEmpty(OfReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                if (!CheckPermitionForEditForDoc(int.Parse(OfReId)))
                    SetEnabled(false);
                else SetEnabled(true);

                ReqManager.FindByCode(int.Parse(OfReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || (ReqManager[0]["IsConfirm"].ToString() != "0"))
                        SetEnabled(false);
                    else SetEnabled(true);
                }
                TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
                JobManager.FindByCode(JhId);
                if (JobManager.Count == 1)
                {
                    if (JobManager[0]["TableId"].ToString() != OfReId)
                        SetEnabled(false);
                    else SetEnabled(true);
                }
                break;
            case "MemberShip":
                if (string.IsNullOrEmpty(OfReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if (!CheckPermitionForEditForOffice(int.Parse(OfReId)))
                    SetEnabled(false);
                else SetEnabled(true);
                break;
        }


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    void SetEnabled(bool Enabled)
    {
        btnEdit.Enabled = Enabled;
        btnEdit2.Enabled = Enabled;
        btnSave.Enabled = Enabled;
        btnSave2.Enabled = Enabled;
        BtnNew.Enabled = Enabled;
        BtnNew2.Enabled = Enabled;
    }
    protected void Enable()
    {
        //ASPxRoundPanel2.Enabled = true;
        //ASPxRoundPanel4.Enabled = true;
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {

            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Enabled = true;
            }
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxComboBox)
            {
                DevExpress.Web.ASPxComboBox co = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel2.Controls[i];
                co.Enabled = true;
            }

        }
        txtjCoEndDate.Enabled = true;
        txtjCoStartDate.Enabled = true;
        txtjStartDate.Enabled = true;
        txtjDesc.Enabled = true;


    }
    protected void Disable()
    {
        //ASPxRoundPanel2.Enabled = false;
        //ASPxRoundPanel4.Enabled = true;
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {

            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Enabled = false;
            }
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxComboBox)
            {
                DevExpress.Web.ASPxComboBox co = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel2.Controls[i];
                co.Enabled = false;
            }

        }
        txtjCoEndDate.Enabled = false;
        txtjCoStartDate.Enabled = false;
        txtjStartDate.Enabled = false;
        txtjDesc.Enabled = false;


    }
    protected void ClearForm()
    {
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {

            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxComboBox)
            {
                DevExpress.Web.ASPxComboBox co = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel2.Controls[i];
                co.DataBind();
                co.SelectedIndex = -1;
            }

        }
        txtjDesc.Text = "";
        txtjCoEndDate.Text = "";
        txtjCoStartDate.Text = "";
        txtjStartDate.Text = "";


    }
    protected void FillForm(int JhId)
    {
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        JhManager.FindByCode(JhId);
        if (JhManager.Count > 0)
        {
            txtjCity.Text = JhManager[0]["CitName"].ToString();
            txtjCoEndDate.Text = JhManager[0]["EndCorporateDate"].ToString();
            txtjCoStartDate.Text = JhManager[0]["StartCorporateDate"].ToString();
            txtjDesc.Text = JhManager[0]["Description"].ToString();
            txtjEmployer.Text = JhManager[0]["Employer"].ToString();
            txtjEndStatus.Text = JhManager[0]["StatusOfEndDate"].ToString();
            //txtjPosition.Text = JhManager[0]["ProjectPosition"].ToString();
            txtjPrName.Text = JhManager[0]["ProjectName"].ToString();
            txtjPrVolume.Text = JhManager[0]["ProjectVolume"].ToString();
            txtjStartDate.Text = JhManager[0]["StartOriginalDate"].ToString();
            txtjStartStatus.Text = JhManager[0]["StatusOfStartDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(JhManager[0]["PJPId"]))
            {
                ComboPosition.DataBind();
                ComboPosition.SelectedIndex = ComboPosition.Items.IndexOfValue(JhManager[0]["PJPId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["CounId"]))
            {
                CombojCountry.DataBind();
                CombojCountry.SelectedIndex = CombojCountry.Items.IndexOfValue(JhManager[0]["CounId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["CorTypeId"]))
            {
                CombojIsCorporate.DataBind();
                CombojIsCorporate.SelectedIndex = CombojIsCorporate.Items.IndexOfValue(JhManager[0]["CorTypeId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["PrTypeId"]))
            {
                if (JhManager[0]["PrTypeId"].ToString() == "1")
                {
                    ASPxLabel22.ClientVisible = true;
                    ASPxLabel23.ClientVisible = true;
                    txtjArea.ClientVisible = true;
                    txtjFloor.ClientVisible = true;
                    ASPxLabel10.ClientVisible = true;
                    CombojSazeType.ClientVisible = true;
                    txtjArea.Text = JhManager[0]["Area"].ToString();
                    txtjFloor.Text = JhManager[0]["Floors"].ToString();

                }
                CombojPrType.DataBind();
                CombojPrType.SelectedIndex = CombojPrType.Items.IndexOfValue(JhManager[0]["PrTypeId"].ToString());

                if (!Utility.IsDBNullOrNullValue(JhManager[0]["SazeTypeId"]))
                {
                    CombojSazeType.DataBind();
                    CombojSazeType.SelectedIndex = CombojSazeType.Items.IndexOfValue(JhManager[0]["SazeTypeId"].ToString());
                }
            }


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
        }
    }

    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(MeJobManager);
        trans.Add(WorkFlowStateManager);


        try
        {

            TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();

            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            MeJobManager2.FindByMeId(OfId);

            for (int i = 0; i < MeJobManager2.Count; i++)
            {
                if (MeJobManager2[i]["ProjectName"].ToString() == txtjPrName.Text && MeJobManager2[i]["Employer"].ToString() == txtjEmployer.Text && MeJobManager2[i]["PrTypeId"].ToString() == CombojPrType.Value.ToString() && MeJobManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            DataRow drJob = MeJobManager.NewRow();

            drJob["MeId"] = OfId;
            drJob["RoeId"] = 1;//ثبت عضویت
            if (CombojPrType.Value != null)
                drJob["PrTypeId"] = int.Parse(CombojPrType.Value.ToString());
            if (CombojSazeType.Value != null)
                drJob["SazeTypeId"] = int.Parse(CombojSazeType.Value.ToString());
            drJob["ProjectName"] = txtjPrName.Text;
            drJob["Employer"] = txtjEmployer.Text;
            drJob["CitName"] = txtjCity.Text;
            if (CombojCountry.Value != null)
                drJob["CounId"] = int.Parse(CombojCountry.Value.ToString());
            if (ComboPosition.Value != null)
                drJob["PJPId"] = ComboPosition.Value;
            drJob["StartOriginalDate"] = txtjStartDate.Text;
            drJob["StartCorporateDate"] = txtjCoStartDate.Text;
            if (!string.IsNullOrEmpty(txtjStartStatus.Text))
                drJob["StatusOfStartDate"] = txtjStartStatus.Text;
            else
                drJob["StatusOfStartDate"] = DBNull.Value;
            drJob["EndCorporateDate"] = txtjCoEndDate.Text;
            if (!string.IsNullOrEmpty(txtjEndStatus.Text))
                drJob["StatusOfEndDate"] = txtjEndStatus.Text;
            else
                drJob["StatusOfEndDate"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjPrVolume.Text))
                drJob["ProjectVolume"] = txtjPrVolume.Text;
            else
                drJob["ProjectVolume"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjArea.Text))
                drJob["Area"] = txtjArea.Text;
            else
                drJob["Area"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjFloor.Text))
                drJob["Floors"] = txtjFloor.Text;
            else
                drJob["Floors"] = DBNull.Value;
            if (CombojIsCorporate.Value != null)
                drJob["CorTypeId"] = int.Parse(CombojIsCorporate.Value.ToString());
            drJob["ConfirmedByNezam"] = 0;
            drJob["Description"] = txtjDesc.Text;
            drJob["UserId"] = Utility.GetCurrentUser_UserId();
            drJob["ModifiedDate"] = DateTime.Now;
            //drJob["MReId"] = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            drJob["TableId"] = OfReId;

            //drJob["TableId"] = Utility.DecryptQS(MemberId.Value);
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
            drJob["CreateDate"] = Utility.GetDateOfToday();
            drJob["Type"] = 1;

            MeJobManager.AddRow(drJob);

            trans.BeginSave();

            int cnt = MeJobManager.Save();
            if (cnt > 0)
            {
                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                if (CombojPrType.Value.ToString() == "1")
                {
                    ASPxLabel22.ClientVisible = true;
                    ASPxLabel23.ClientVisible = true;
                    txtjArea.ClientVisible = true;
                    txtjFloor.ClientVisible = true;
                    ASPxLabel10.ClientVisible = true;
                    CombojSazeType.ClientVisible = true;
                }
                else
                {
                    ASPxLabel22.ClientVisible = false;
                    ASPxLabel23.ClientVisible = false;
                    txtjArea.ClientVisible = false;
                    txtjFloor.ClientVisible = false;
                    ASPxLabel10.ClientVisible = false;
                    CombojSazeType.ClientVisible = false;

                }

                JobId.Value = Utility.EncryptQS(MeJobManager[0]["JhId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

                if (Session["OffMenuArrayList"] != null)
                {
                    ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                    arr[3] = 1;
                    Session["OffMenuArrayList"] = arr;
                }
                else
                {
                    CheckMenuImage(OfReId);
                    ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                    arr[3] = 1;
                    Session["OffMenuArrayList"] = arr;
                }
            }
            else
            {
                trans.CancelSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();

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
    protected void Edit(int JhId)
    {
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(JhManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

            JhManager.FindByMeId(OfId);
            for (int i = 0; i < JhManager.Count; i++)
            {
                if (JhManager[i]["ProjectName"].ToString() == txtjPrName.Text && JhManager[i]["Employer"].ToString() == txtjEmployer.Text && JhManager[i]["PrTypeId"].ToString() == CombojPrType.Value.ToString() && Convert.ToInt32(JhManager[i]["JhId"]) != JhId && JhManager[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            JhManager.FindByCode(JhId);
            JhManager[0].BeginEdit();

            if (CombojPrType.Value != null)
                JhManager[0]["PrTypeId"] = int.Parse(CombojPrType.Value.ToString());
            if (CombojSazeType.Value != null)
                JhManager[0]["SazeTypeId"] = int.Parse(CombojSazeType.Value.ToString());
            JhManager[0]["ProjectName"] = txtjPrName.Text;
            JhManager[0]["Employer"] = txtjEmployer.Text;
            JhManager[0]["CitName"] = txtjCity.Text;
            if (CombojCountry.Value != null)
                JhManager[0]["CounId"] = int.Parse(CombojCountry.Value.ToString());
            if (ComboPosition.Value != null)
                JhManager[0]["PJPId"] = ComboPosition.Value;
            JhManager[0]["StartOriginalDate"] = txtjStartDate.Text;
            JhManager[0]["StartCorporateDate"] = txtjCoStartDate.Text;
            if (!string.IsNullOrEmpty(txtjStartStatus.Text))
                JhManager[0]["StatusOfStartDate"] = txtjStartStatus.Text;
            else
                JhManager[0]["StatusOfStartDate"] = DBNull.Value;
            JhManager[0]["EndCorporateDate"] = txtjCoEndDate.Text;
            if (!string.IsNullOrEmpty(txtjEndStatus.Text))
                JhManager[0]["StatusOfEndDate"] = txtjEndStatus.Text;
            else
                JhManager[0]["StatusOfEndDate"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjPrVolume.Text))
                JhManager[0]["ProjectVolume"] = txtjPrVolume.Text;
            else
                JhManager[0]["ProjectVolume"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjArea.Text))
                JhManager[0]["Area"] = txtjArea.Text;
            else
                JhManager[0]["Area"] = DBNull.Value;
            if (!string.IsNullOrEmpty(txtjFloor.Text))
                JhManager[0]["Floors"] = txtjFloor.Text;
            else
                JhManager[0]["Floors"] = DBNull.Value;
            if (CombojIsCorporate.Value != null)
                JhManager[0]["CorTypeId"] = int.Parse(CombojIsCorporate.Value.ToString());
            JhManager[0]["ConfirmedByNezam"] = 0;
            JhManager[0]["Description"] = txtjDesc.Text;
            JhManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            JhManager[0]["ModifiedDate"] = DateTime.Now;

            JhManager[0].EndEdit();
            trans.BeginSave();
            if (JhManager.Save() == 1)
            {
                int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                if (CombojPrType.Value.ToString() == "1")
                {
                    ASPxLabel22.ClientVisible = true;
                    ASPxLabel23.ClientVisible = true;
                    txtjArea.ClientVisible = true;
                    txtjFloor.ClientVisible = true;
                    ASPxLabel10.ClientVisible = true;
                    CombojSazeType.ClientVisible = true;
                }
                else
                {
                    ASPxLabel22.ClientVisible = false;
                    ASPxLabel23.ClientVisible = false;
                    txtjArea.ClientVisible = false;
                    txtjFloor.ClientVisible = false;
                    ASPxLabel10.ClientVisible = false;
                    CombojSazeType.ClientVisible = false;
                }

                trans.EndSave();
                JobId.Value = Utility.EncryptQS(JhManager[0]["JhId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }


        }

        catch (Exception err)
        {
            trans.CancelSave();

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

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobQuality":
                Response.Redirect("OfficeJobQuality.aspx?&aPageMode=" + PgMode.Value + "&OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&Dprt=" + HDMode.Value);
                break;
        }
    }
    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();


        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office

        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
}
