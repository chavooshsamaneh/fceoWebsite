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

public partial class Employee_OfficeRegister_OfficeJobInsert : System.Web.UI.Page
{
    DataTable dtOfJob = null;
    Boolean IsPageRefresh = false;

  
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
                Response.Redirect("Office.aspx");
                return;
            }

            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            Session["IsEdited_OffJob"] = false;
            ODBJobCountry.CacheDuration = Utility.GetCacheDuration();

            TSP.DataManager.Permission per = FindPermissionClass();
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

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


            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            OfficeInfoUserControl.OfReId = int.Parse(OfReId);


            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

            if (PageMode != "New" && !per.CanView)
            {
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                        + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
            }

            switch (PageMode)
            {
                case "View":
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
                    FillForm(int.Parse(JhId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    if (string.IsNullOrEmpty(OfReId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    Enable();
                    ASPxMenu1.Items[0].Enabled = false;

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;

                case "Edit":
                    Enable();

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(JhId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";


                    break;

                //case "Judge":
                //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.ExpertConfirmingDocumentOff;
                //    int GradingImplementDocTaskId = -1;

                //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
                //    if (WorkFlowTaskManager.Count == 1)
                //    {
                //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                //    }

                //    if (GradingImplementDocTaskId == -1)
                //    {
                //        this.DivReport.Visible = true;
                //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                //        return;
                //    }
                //    int NmcId = FindNmcId(GradingImplementDocTaskId);                    
                //    if (NmcId == -1)
                //    {
                //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                //        return;
                //    }
                //    TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
                //    if (string.IsNullOrEmpty(JhId))
                //    {
                //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                //        return;
                //    }
                //    TrainingJudgmentManager.FindByNmcId(NmcId, int.Parse(JhId), 5);
                //    if (TrainingJudgmentManager.Count > 0)
                //    {
                //        string JudgeId = TrainingJudgmentManager[0][TrainingJudgmentManager.Count - 1].ToString();
                //        HDJudgeId.Value = Utility.EncryptQS(JudgeId);
                //        FillJugde(int.Parse(JudgeId));
                //    }
                //    Disable();


                //    btnEdit.Enabled = per.CanEdit;
                //    btnEdit2.Enabled = per.CanEdit;

                //    FillForm(int.Parse(JhId));

                //    ASPxRoundPanel2.HeaderText = "مشاهده";
                //    RoundPanelJudge.Visible = true;
                //    break;
            }

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "Document")
                CheckWorkFlowPermissionForDoc();
            else if (Department == "MemberShip")
                CheckWorkFlowPermissionForOffice();

            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if ((Convert.ToBoolean(ReqManager[0]["Requester"]) == false) || (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                   
                }
            }
            TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
            if (!string.IsNullOrEmpty(JhId))
            {
                JobManager.FindByCode(int.Parse(JhId));
                if (JobManager.Count == 1)
                {
                    if (JobManager[0]["TableId"].ToString() != OfReId)
                    {
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                      
                    }
                }
            }

            #region ModeComment
            //if (Mode == "Home")
            //{
            //    ReqManager.FindByOfficeId(int.Parse(OfId), -1, 0);
            //    if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //    {
            //        //btnEdit.Enabled = false;
            //        //btnEdit2.Enabled = false;
            //        //BtnNew.Enabled = false;
            //        //BtnNew2.Enabled = false;
            //        //btnSave.Enabled = false;
            //        //btnSave2.Enabled = false;

            //    }
            //    if (OfManager[0]["MrsId"].ToString() == "1")//تایید شده
            //    {
            //        btnEdit.Enabled = false;
            //        btnEdit2.Enabled = false;
            //        BtnNew.Enabled = false;
            //        BtnNew2.Enabled = false;
            //        btnSave.Enabled = false;
            //        btnSave2.Enabled = false;
            //    }
            //}
            //else if (Mode == "Request")
            //{
            //    btnEdit.Enabled = false;
            //    btnEdit2.Enabled = false;

            //    string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
            //    string TPType = Utility.DecryptQS(ReqestMode);
            //    if (!string.IsNullOrEmpty(TPType))
            //    {
            //        if (TPType == "0")//Menu
            //        {

            //            BtnNew.Enabled = false;
            //            BtnNew2.Enabled = false;

            //        }
            //        else
            //        {
            //            //TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            //            ReqManager.FindByCode(int.Parse(OfReId));
            //            if (ReqManager.Count > 0)
            //            {
            //                if (Convert.ToBoolean(ReqManager[0]["Requester"]) == false)//Request From Member
            //                {
            //                    BtnNew.Enabled = false;
            //                    BtnNew2.Enabled = false;
            //                    btnSave.Enabled = false;
            //                    btnSave2.Enabled = false;
            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
         
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

        Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
              + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
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

                TSP.DataManager.Permission per = FindPermissionClass();
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

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            switch (PageMode)
            {
                case "New":
                    Insert();
                    break;

                case "Edit":

                    if (string.IsNullOrEmpty(JhId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        Edit(int.Parse(JhId));

                    break;

                case "Judge":
                    string JudgeId = Utility.DecryptQS(HDJudgeId.Value);
                    if (string.IsNullOrEmpty(JudgeId))
                        InsertJudge();

                    else
                        EditJudge(int.Parse(JudgeId));

                    break;
            }



        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = FindPermissionClass();


        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        ASPxMenu1.Items[0].Enabled = false;
      
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

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobQuality":
                Response.Redirect("OfficeJobQuality.aspx?&aPageMode=" + PgMode.Value + "&OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&JhId=" + JobId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
              + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

                break;

        }
    }
   
    //***************************************Methods**********************************************************
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

                if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() 
                    || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                {
                    ASPxMenu1.Items[0].Enabled = true;

                }
                else
                    ASPxMenu1.Items[0].Enabled = false;

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
                CombojIsCorporate.Value = JhManager[0]["CorTypeId"];

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
        if (IsPageRefresh)
            return;
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
            CombojIsCorporate.DataBind();
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
                //dtOfJob = (DataTable)Session["TblOfJobQ"];
                //int JhId = int.Parse(MeJobManager[0]["JhId"].ToString());
                //if (dtOfJob.DefaultView.Count > 0)
                //{
                //    for (int i = 0; i < dtOfJob.DefaultView.Count; i++)
                //    {
                //        DataRow drQ = JobQualityManager.NewRow();
                //        drQ["JhId"] = JhId;
                //        drQ["OfdId"] = dtOfJob.Rows[i]["OfdId"].ToString();
                //        drQ["Mark"] = DBNull.Value;
                //        drQ["FilePath"] = dtOfJob.Rows[i]["FilePath"].ToString();
                //        drQ["CreateDate"] = Utility.GetDateOfToday();
                //        drQ["Description"] = dtOfJob.Rows[i]["Description"].ToString();
                //        drQ["UserId"] = Utility.GetCurrentUser_UserId();
                //        drQ["ModifiedDate"] = DateTime.Now;
                //        JobQualityManager.AddRow(drQ);
                //        int imgcnt = JobQualityManager.Save();
                //        JobQualityManager.DataTable.AcceptChanges();
                //        if (imgcnt == 1)
                //        {
                //            dtOfJob.Rows[i].BeginEdit();
                //            dtOfJob.Rows[i]["Code"] = JobQualityManager[JobQualityManager.Count - 1]["JhqId"].ToString();
                //            dtOfJob.Rows[i].EndEdit();

                //            if (!string.IsNullOrEmpty(dtOfJob.Rows[i]["FilePath"].ToString()))
                //            {

                //                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                //                string ImgTarget = Server.MapPath("~/Image/Office/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString()); //Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());
                //                File.Copy(ImgSoource, ImgTarget, true);


                //            }


                //        }
                //    }

                //}               
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    Session["IsEdited_OffJob"] = true;                 

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
                    if (ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.Implementer).ToString() || ComboPosition.Value.ToString() == ((int)TSP.DataManager.ProjectJobPosition.ImplementerAgent).ToString())
                    {
                        ASPxMenu1.Items[0].Enabled = true;
                      

                    }
                    else
                    {
                        ASPxMenu1.Items[0].Enabled = false;
                        

                    }

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

                    JobId.Value = Utility.EncryptQS(MeJobManager[0]["JhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                   
                    //ASPxMenu1.Enabled = true;
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
        if (IsPageRefresh)
            return;
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();

        trans.Add(JhManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(JobQualityManager);

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
                //dtOfJob = (DataTable)Session["TblOfJobQ"];

                //if (dtOfJob.GetChanges() != null)
                //{
                //    DataRow[] delRows = dtOfJob.Select("Mode='1'", null, DataViewRowState.Deleted);
                //    for (int i = 0; i < delRows.Length; i++)
                //    {
                //        JobQualityManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                //        JobQualityManager[0].Delete();
                //        JobQualityManager.Save();
                //    }
                //    JobQualityManager.DataTable.AcceptChanges();
                //    DataRow[] insRows = dtOfJob.Select(null, null, DataViewRowState.Added);

                //    if (insRows.Length > 0)
                //    {
                //        for (int i = 0; i < insRows.Length; i++)
                //        {
                //            DataRow drQ = JobQualityManager.NewRow();
                //            drQ["JhId"] = JhId;
                //            drQ["OfdId"] = dtOfJob.Rows[i]["OfdId"].ToString();
                //            drQ["Mark"] = DBNull.Value;
                //            drQ["FilePath"] = dtOfJob.Rows[i]["FilePath"].ToString();
                //            drQ["CreateDate"] = Utility.GetDateOfToday();
                //            drQ["Description"] = dtOfJob.Rows[i]["Description"].ToString();
                //            drQ["UserId"] = Utility.GetCurrentUser_UserId();
                //            drQ["ModifiedDate"] = DateTime.Now;
                //            JobQualityManager.AddRow(drQ);
                //            int imgcnt = JobQualityManager.Save();
                //            JobQualityManager.DataTable.AcceptChanges();
                //            if (imgcnt == 1)
                //            {

                //                if (!string.IsNullOrEmpty(dtOfJob.Rows[i]["FilePath"].ToString()))
                //                {
                //                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                //                    string ImgTarget = Server.MapPath("~/Image/Office/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString()); //Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());

                //                    //string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                //                    //string ImgTarget = Server.MapPath(dtOfJob.Rows[i]["FilePath"].ToString());
                //                    File.Copy(ImgSoource, ImgTarget, true);
                //                }

                //            }
                //        }
                //    }
                //}
                int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffJob"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
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
                        JobQualityManager.FindByJobCode(JhId);
                        for (int i = 0; i < JobQualityManager.Count; i++)
                        {
                            JobQualityManager[i].Delete();
                        }
                        JobQualityManager.Save();
                    }


                    trans.EndSave();

                    JobId.Value = Utility.EncryptQS(JhManager[0]["JhId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    Session["IsEdited_OffJob"] = true;

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

    private void CheckWorkFlowPermissionForDoc()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEditForDoc(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEditForDoc(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), TaskCode, Utility.GetCurrentUser_UserId());
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

    private void CheckWorkFlowPermissionForOffice()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEditForOffice(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEditForOffice(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), TaskCode, Utility.GetCurrentUser_UserId());
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

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    private void FillJugde(int JudgeId)
    {
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        TrainingJudgmentManager.FindByCode(JudgeId);
        if (TrainingJudgmentManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["JudgeViewPoint"]))
                txtViewPoint.Text = TrainingJudgmentManager[0]["JudgeViewPoint"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["JudgeGrade"]))
                txtGrade.Text = TrainingJudgmentManager[0]["JudgeGrade"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["MeetingDate"]))
                txtMeetingDate.Text = TrainingJudgmentManager[0]["MeetingDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["MeetingId"]))
                txtMeetingId.Text = TrainingJudgmentManager[0]["MeetingId"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["IsConfirmed"]))
                rdbtnIsConfirm.SelectedIndex = int.Parse(TrainingJudgmentManager[0]["IsConfirmed"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }

    private void InsertJudge()
    {
        //if (IsPageRefresh)
        //    return;
        //TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //try
        //{
        //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.ExpertConfirmingDocumentOff;
        //    int GradingImplementDocTaskId = -1;

        //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }

        //    if (GradingImplementDocTaskId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        //        return;
        //    }
        //    int NmcId = FindNmcId(GradingImplementDocTaskId);
        //    if (NmcId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "اطلاعات شما در چارت سازماني ثبت نشده است.";
        //        return;
        //    }

        //    int JhId = int.Parse(Utility.DecryptQS(JobId.Value));
        //    DataRow JudgeRow = TrainingJudgmentManager.NewRow();
        //    JudgeRow["PkId"] = JhId;
        //    JudgeRow["CreateDate"] = Utility.GetDateOfToday();
        //    JudgeRow["MeetingId"] = txtMeetingId.Text;
        //    JudgeRow["MeetingDate"] = txtMeetingDate.Text;
        //    JudgeRow["JudgeViewPoint"] = txtViewPoint.Text;
        //    JudgeRow["JudgeGrade"] = txtGrade.Text;
        //    //JudgeRow["EmpId"] = Utility.GetCurrentUser_MeId();
        //    //JudgeRow["UltId"] = 4;
        //    JudgeRow["NmcId"] = NmcId;
        //    JudgeRow["Type"] = 5;
        //    JudgeRow["IsConfirmed"] = rdbtnIsConfirm.SelectedItem.Value.ToString();
        //    JudgeRow["UserId"] = Utility.GetCurrentUser_UserId();
        //    JudgeRow["ModifiedDate"] = DateTime.Now;

        //    TrainingJudgmentManager.AddRow(JudgeRow);
        //    int cn = TrainingJudgmentManager.Save();
        //    if (cn > 0)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "ذخیره انجام شد.";
        //        HDJudgeId.Value = Utility.EncryptQS(TrainingJudgmentManager[0]["JudgeId"].ToString());
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        //    }
        //}
        //catch (Exception err)
        //{
        //    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        //    {
        //        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
        //        if (se.Number == 2601)
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "اطلاعات تکراری می باشد";
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //        }
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //    }
        //}
    }

    private void EditJudge(int JudgeId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        try
        {
            TrainingJudgmentManager.FindByCode(JudgeId);
            if (TrainingJudgmentManager.Count == 1)
            {
                TrainingJudgmentManager[0].BeginEdit();

                TrainingJudgmentManager[0]["MeetingId"] = txtMeetingId.Text;
                TrainingJudgmentManager[0]["MeetingDate"] = txtMeetingDate.Text;
                TrainingJudgmentManager[0]["JudgeViewPoint"] = txtViewPoint.Text;
                TrainingJudgmentManager[0]["JudgeGrade"] = txtGrade.Text;
                TrainingJudgmentManager[0]["IsConfirmed"] = rdbtnIsConfirm.SelectedItem.Value.ToString();

                TrainingJudgmentManager[0].EndEdit();
                int cn = TrainingJudgmentManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                    HDJudgeId.Value = Utility.EncryptQS(TrainingJudgmentManager[0]["JudgeId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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

    private TSP.DataManager.Permission FindPermissionClass()
    {
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {            
            return (TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        else if (Department == "Document")
        {
            return (TSP.DataManager.ProjectJobHistoryManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        return (TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
    }
  
}
