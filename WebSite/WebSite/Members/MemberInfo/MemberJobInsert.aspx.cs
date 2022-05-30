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

public partial class Members_MemberInfo_MemberJobInsert : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["IsEdited_MeJob"] = false;

            ODBJobCountry.CacheDuration = Utility.GetCacheDuration();
            if (string.IsNullOrEmpty(Request.QueryString["MeId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode2"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");


            }
            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"].ToString());
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"].ToString());
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"].ToString());
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());

            }
            catch (Exception)
            { }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);
            string PageMode2 = Utility.DecryptQS(PgMode.Value);
            string JhId = Utility.DecryptQS(JobId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (string.IsNullOrEmpty(PageMode2) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            //TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();

            switch (PageMode2)
            {

                case "New2":
                    ASPxRoundPanel2.HeaderText = "جدید";
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    ASPxMenu1.Items[0].Enabled = false;

                    break;

                case "View2":
                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    Disable();
                    FillForm(int.Parse(JhId));

                    break;
                case "Edit2":
                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }

                    FillForm(int.Parse(JhId));
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    break;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            switch (Mode)
            {
                case "Home":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;

                    break;

                case "Request":


                    if (string.IsNullOrEmpty(MReId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    CheckWorkFlowPermission();

                    ReqManager.FindByCode(int.Parse(MReId));
                    if (ReqManager.Count > 0)
                    {
                        if (Convert.ToBoolean(ReqManager[0]["Requester"]) || (ReqManager[0]["IsConfirm"].ToString() != "0"))
                        {
                            btnNew.Enabled = false;
                            btnNew2.Enabled = false;
                            btnSave.Enabled = false;
                            btnSave2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                        }
                    }

                    break;
            }

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
    void FillForm(int JhId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            FillForm_TempMe(JhId);
        else
            FillFormMember(JhId);
    }
    protected void FillFormMember(int JhId)
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

                if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                {
                    ASPxMenu1.Items[0].Enabled = true;
                }
                else
                {
                    ASPxMenu1.Items[0].Enabled = false;


                }
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CounId"].ToString()))
            {
                CombojCountry.DataBind();
                CombojCountry.SelectedIndex = CombojCountry.Items.IndexOfValue(JhManager[0]["CounId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CorTypeId"].ToString()))
            {
                CombojIsCorporate.DataBind();
                CombojIsCorporate.SelectedIndex = CombojIsCorporate.Items.IndexOfValue(JhManager[0]["CorTypeId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["PrTypeId"].ToString()))
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

                    if (!string.IsNullOrEmpty(JhManager[0]["SazeTypeId"].ToString()))
                    {
                        CombojSazeType.DataBind();
                        CombojSazeType.SelectedIndex = CombojSazeType.Items.IndexOfValue(JhManager[0]["SazeTypeId"].ToString());
                    }
                }
                CombojPrType.DataBind();
                CombojPrType.SelectedIndex = CombojPrType.Items.IndexOfValue(JhManager[0]["PrTypeId"].ToString());
            }


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
            return;
        }
    }
    protected void FillForm_TempMe(int JhId)
    {
        TSP.DataManager.TempMemberJobHistoryManager JhManager = new TSP.DataManager.TempMemberJobHistoryManager();
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

                if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                {
                    ASPxMenu1.Items[0].Enabled = true;
                }
                else
                {
                    ASPxMenu1.Items[0].Enabled = false;


                }
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CounId"].ToString()))
            {
                CombojCountry.DataBind();
                CombojCountry.SelectedIndex = CombojCountry.Items.IndexOfValue(JhManager[0]["CounId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["CorTypeId"].ToString()))
            {
                CombojIsCorporate.DataBind();
                CombojIsCorporate.SelectedIndex = CombojIsCorporate.Items.IndexOfValue(JhManager[0]["CorTypeId"].ToString());
            }
            if (!string.IsNullOrEmpty(JhManager[0]["PrTypeId"].ToString()))
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

                    if (!string.IsNullOrEmpty(JhManager[0]["SazeTypeId"].ToString()))
                    {
                        CombojSazeType.DataBind();
                        CombojSazeType.SelectedIndex = CombojSazeType.Items.IndexOfValue(JhManager[0]["SazeTypeId"].ToString());
                    }
                }
                CombojPrType.DataBind();
                CombojPrType.SelectedIndex = CombojPrType.Items.IndexOfValue(JhManager[0]["PrTypeId"].ToString());
            }


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + Request.QueryString["Mode"]);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
        }
        else
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            //if ((bool)MemberManager[0]["IsLock"] == true)
            if (Utility.GetCurrentUser_IsLock())
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.LockMemberType.Member, 1);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return;

            }
        }

        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            switch (PageMode)
            {
                case "New2":
                    if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                        InsertTempMe();
                    else
                        Insert();
                    break;
                case "Edit2":
                    string JhId = Utility.DecryptQS(JobId.Value);
                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    else
                    {
                        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                            EditTempMe(int.Parse(JhId));
                        else
                            Edit(int.Parse(JhId));
                    }
                    break;

            }


        }

    }
    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.ProjectJobHistoryManager MeJobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();


        trans.Add(WorkFlowStateManager);
        trans.Add(MeJobManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            MeJobManager2.FindByMeId(MeId);

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

            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));


            drJob["MeId"] = MeId;
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
            drJob["TableId"] = MReId;
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drJob["CreateDate"] = Utility.GetDateOfToday();
            MeJobManager.AddRow(drJob);
            trans.BeginSave();

            int cnt = MeJobManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    int TableId = MReId;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    Session["IsEdited_MeJob"] = true;
                    trans.EndSave();

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

                    if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                    {
                        ASPxMenu1.Items[0].Enabled = true;


                    }
                    else
                    {
                        ASPxMenu1.Items[0].Enabled = false;


                    }

                    JobId.Value = Utility.EncryptQS(MeJobManager[0]["JhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit2");
                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    if (Session["MenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
                    else
                    {
                        CheckMenuImage(MeId, MReId);
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
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
    protected void InsertTempMe()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager2 = new TSP.DataManager.TempMemberJobHistoryManager();


        trans.Add(WorkFlowStateManager);
        trans.Add(MeJobManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            MeJobManager2.FindByTMeId(MeId);

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

            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));


            drJob["TMeId"] = MeId;
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
            drJob["TableId"] = MReId;
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drJob["CreateDate"] = Utility.GetDateOfToday();
            MeJobManager.AddRow(drJob);
            trans.BeginSave();

            int cnt = MeJobManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    int TableId = MReId;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    Session["IsEdited_MeJob"] = true;
                    trans.EndSave();

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

                    if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                    {
                        ASPxMenu1.Items[0].Enabled = true;


                    }
                    else
                    {
                        ASPxMenu1.Items[0].Enabled = false;


                    }

                    JobId.Value = Utility.EncryptQS(MeJobManager[0]["TMJhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit2");
                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    if (Session["MenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
                    else
                    {
                        CheckMenuImage(MeId, MReId);
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
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
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(JhManager);


        try
        {
            string MeId = Utility.DecryptQS(MemberId.Value);

            MeJobManager2.FindByMeId(int.Parse(MeId));

            for (int i = 0; i < MeJobManager2.Count; i++)
            {
                if (MeJobManager2[i]["ProjectName"].ToString() == txtjPrName.Text && MeJobManager2[i]["Employer"].ToString() == txtjEmployer.Text && MeJobManager2[i]["PrTypeId"].ToString() == CombojPrType.Value.ToString() && Convert.ToInt32(MeJobManager2[i]["JhId"]) != JhId && MeJobManager2[i]["InActiveName"].ToString() == "فعال")
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
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), int.Parse(MeId), 1);

                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
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

                    if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                    {
                        ASPxMenu1.Items[0].Enabled = true;


                    }
                    else
                    {
                        ASPxMenu1.Items[0].Enabled = false;


                    }

                    trans.EndSave();
                    JobId.Value = Utility.EncryptQS(JhManager[0]["JhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit2");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    Session["IsEdited_MeJob"] = true;
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
    protected void EditTempMe(int JhId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberJobHistoryManager JhManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager2 = new TSP.DataManager.TempMemberJobHistoryManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(JhManager);


        try
        {
            string MeId = Utility.DecryptQS(MemberId.Value);

            MeJobManager2.FindByTMeId(int.Parse(MeId));

            for (int i = 0; i < MeJobManager2.Count; i++)
            {
                if (MeJobManager2[i]["ProjectName"].ToString() == txtjPrName.Text && MeJobManager2[i]["Employer"].ToString() == txtjEmployer.Text && MeJobManager2[i]["PrTypeId"].ToString() == CombojPrType.Value.ToString() && Convert.ToInt32(MeJobManager2[i]["TMJhId"]) != JhId && MeJobManager2[i]["InActiveName"].ToString() == "فعال")
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
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), int.Parse(MeId), 1);

                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
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

                    if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                    {
                        ASPxMenu1.Items[0].Enabled = true;


                    }
                    else
                    {
                        ASPxMenu1.Items[0].Enabled = false;


                    }

                    trans.EndSave();
                    JobId.Value = Utility.EncryptQS(JhManager[0]["TMJhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit2");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    Session["IsEdited_MeJob"] = true;
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
    protected void Enable()
    {
        ASPxRoundPanel2.Enabled = true;


    }
    protected void Disable()
    {
        ASPxRoundPanel2.Enabled = false;
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
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming)
            {
                return true;
            }

        }
        return false;
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            //CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        // string MeId = Utility.DecryptQS(MemberId.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        if (CheckPermitionForEdit(int.Parse(MReId)))
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;

                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            btnNew.Enabled = true;
            btnNew2.Enabled = true;

        }
        else
        {
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;


    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        ASPxMenu1.Items[0].Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        JobId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
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
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobQuality":
                Response.Redirect("MemberJobQuality.aspx?&PageMode2=" + PgMode.Value + "&MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value + "&Mode=" + Request.QueryString["Mode"]);

                break;

        }
    }
    void CheckMenuImage(int MeId, int MReId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CheckMenuImage_TempMe(MeId, MReId);
        else
            CheckMenuImageMember(MeId, MReId);
    }
    protected void CheckMenuImageMember(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberActivitySubjectManager.Count > 0)
        {
            arr[3] = 1;
        }
        MemberLanguageManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberLanguageManager.Count > 0)
        {
            arr[2] = 1;
        }

        MemberLicenceManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberLicenceManager.Count > 0)
        {
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByMeRequest(MeId, MReId, -1, 0, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }
    protected void CheckMenuImage_TempMe(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.TempMemberLanguageManager MemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLicenceManager MemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindByRequest(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            arr[3] = 1;
        }
        MemberLanguageManager.FindByRequest(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            arr[2] = 1;
        }

        MemberLicenceManager.FindByRequest(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByRequest(MeId, MReId);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }
}
