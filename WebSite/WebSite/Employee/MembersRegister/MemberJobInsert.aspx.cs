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

public partial class Employee_MembersRegister_MemberJobInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
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
            if (string.IsNullOrEmpty(Request.QueryString["MReId"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }

            Session["IsEdited_MeJob"] = false;
            ODBJobCountry.CacheDuration = Utility.GetCacheDuration();

            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
           
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());

                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();

                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string Mode = Utility.DecryptQS(HDMode.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);

            string MeId = Utility.DecryptQS(MemberId.Value);
            string JhId = Utility.DecryptQS(JobId.Value);
          
            string MReId = Utility.DecryptQS(MemberRequest.Value);


            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(int.Parse(MeId));
           
            if (string.IsNullOrEmpty(Mode) || string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            try
            {
                switch (PageMode)
                {
                    case "View":
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);                           
                        }
                        Disable();

                        if (string.IsNullOrEmpty(JhId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }

                        btnEdit.Enabled = per.CanEdit;
                        btnEdit2.Enabled = per.CanEdit;

                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;

                        if (Mode == "Request")
                            FillForm(int.Parse(JhId));                           
                        else if (Mode == "TempMe")
                            FillFormTempMeJob(int.Parse(JhId));                            

                        ASPxRoundPanel2.HeaderText = "مشاهده";


                        break;


                    case "New":
                        if (string.IsNullOrEmpty(MReId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        Enable();

                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;
                        ASPxRoundPanel2.HeaderText = "جدید";

                        ClearForm();
                        break;

                    case "Edit":
                        if (!per.CanView)
                        {
                            Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                        }
                        Enable();

                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;

                        if (string.IsNullOrEmpty(JhId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        if (Mode == "Request")
                            FillForm(int.Parse(JhId));
                        else if (Mode == "TempMe")
                            FillFormTempMeJob(int.Parse(JhId)); 
                        ASPxRoundPanel2.Enabled = true;
                        ASPxRoundPanel2.HeaderText = "ویرایش";


                        break;


                }
                CheckWorkFlowPermission();

                if (Mode == "Home")
                {
                    ReqManager.FindByMemberId(int.Parse(MeId), -1, 1);
                    if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                       
                    }
                    if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                    }
                }
                else if (Mode == "Request")
                {

                    string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
                    string TPType = Utility.DecryptQS(ReqestMode);
                    if (!string.IsNullOrEmpty(TPType))
                    {
                        if (TPType == "0")//Menu
                        {

                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            btnSave.Enabled = false;
                            btnSave2.Enabled = false;
                          

                        }
                        else
                        {
                            ReqManager.FindByCode(int.Parse(MReId));
                            if (ReqManager.Count > 0)
                            {
                                if ( (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
                                {
                                    BtnNew.Enabled = false;
                                    BtnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    btnEdit.Enabled = false;
                                    btnEdit2.Enabled = false;
                                }

                                TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
                                if (!string.IsNullOrEmpty(JhId))
                                {
                                    JhManager.FindByCode(int.Parse(JhId));
                                    if (JhManager.Count == 1)
                                    {
                                        if (JhManager[0]["TableId"].ToString() != MReId)
                                        {
                                            BtnNew.Enabled = false;
                                            BtnNew2.Enabled = false;
                                            btnSave.Enabled = false;
                                            btnSave2.Enabled = false;
                                            btnEdit.Enabled = false;
                                            btnEdit2.Enabled = false;
                                            //btnJhQuality.Enabled = false;
                                            //btnJhQuality2.Enabled = false;
                                        }
                                    }
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception)
            { }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
           

        }

        FillMemberName();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
      
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
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
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();

                TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

           
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string JhId = Utility.DecryptQS(JobId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                if (Mode == "Request")
                    Insert();                  
                else if (Mode == "TempMe")
                    InsertTempMeJob();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(JhId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    if (Mode == "Request")
                        Edit(int.Parse(JhId));                       
                    else if (Mode == "TempMe")
                        EditTempMeJob(int.Parse(JhId));                        
                }
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());        
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        JobId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }    

    #endregion

    #region Methods
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
        CombojCountry.DataBind();
        CombojCountry.SelectedIndex = CombojCountry.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;

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
            return;
        }
    }
    protected void FillFormTempMeJob(int JhId)
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
            return;
        }
    }

    protected void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.ProjectJobHistoryManager MeJobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeJobManager);

        try
        {           
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

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
            drJob["TableId"] = TableId;

            //drJob["TableId"] = Utility.DecryptQS(MemberId.Value);
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drJob["CreateDate"] = Utility.GetDateOfToday();
            drJob["Type"] = 0;

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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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

                    if (Session["MenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
                    else
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }

                    TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
    protected void InsertTempMeJob()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager2 = new TSP.DataManager.TempMemberJobHistoryManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeJobManager);

        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

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
            drJob["TableId"] = TableId;

            //drJob["TableId"] = Utility.DecryptQS(MemberId.Value);
            drJob["TableType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drJob["CreateDate"] = Utility.GetDateOfToday();
            drJob["Type"] = 0;

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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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

                    if (Session["MenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }
                    else
                    {
                        ArrayList arr = (ArrayList)Session["MenuArrayList"];
                        arr[1] = 1;
                        Session["MenuArrayList"] = arr;
                    }

                    TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
                    JobId.Value = Utility.EncryptQS(MeJobManager[0]["TMJhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
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
            Utility.SaveWebsiteError(err);
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
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.ProjectJobHistoryManager MeJobManager2 = new TSP.DataManager.ProjectJobHistoryManager();
        
        trans.Add(WorkFlowStateManager);
        trans.Add(JhManager);


        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            MeJobManager2.FindByMeId(MeId);

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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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
                    trans.EndSave();
                    JobId.Value = Utility.EncryptQS(JhManager[0]["JhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
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
    protected void EditTempMeJob(int JhId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberJobHistoryManager JhManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager2 = new TSP.DataManager.TempMemberJobHistoryManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(JhManager);


        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            MeJobManager2.FindByTMeId(MeId);

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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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
                    trans.EndSave();
                    JobId.Value = Utility.EncryptQS(JhManager[0]["TMJhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
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

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        //string MeId = Utility.DecryptQS(MemberId.Value);
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(MReId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
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
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {
           
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;


    }

    private void FillMemberName()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
        MemberInfoUserControl1.MReId = MReId;
        if (Mode == "TempMe")
        {
            MemberInfoUserControl1.IsMeTemp = true;
        }
    }
    #endregion
}



