using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.IO;
public partial class Employee_27_ExpertFileInsert : System.Web.UI.Page
{
    private string _PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value;
        }
    }

    private int _EfReqId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["EfReqId"]);
        }
        set
        {
            HiddenFieldPage["EfReqId"] = value.ToString();
        }
    }

    private int _EfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["EfId"]);
        }
        set
        {
            HiddenFieldPage["EfId"] = value.ToString();
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {

            Session["ImgOldDocFrontURL"] =
          Session["ImgOldDocBackURL"] = null;
            SetKeys();
        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
        {
            hpImgFrontOldDoc.ImageUrl = Session["ImgOldDocFrontURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
        {
            hpImgBackOldDoc.ImageUrl = Session["ImgOldDocBackURL"].ToString();
        }

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewModeKeys();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditModeKeys();

        CheckWorkFlowPermission();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Edit(_EfReqId);
                break;
            case "Change":
                InsertRequest(TSP.DataManager.ExpertFileRequestManager.RequestType.Change);
                break;
            case "Invalid":
                InsertRequest(TSP.DataManager.ExpertFileRequestManager.RequestType.Invalid);
                break;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
           && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("ExpertFile.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("ExpertFile.aspx");
        }
    }
    protected void lblMeId_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(lblMeId.Text))
        {
            ShowMessage("کد عضویت را وارد نمایید");
            return;
        }
        int MeId = Convert.ToInt32(lblMeId.Text);
        ClearFirm();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.ExpertFileManager ExpertFileManager = new TSP.DataManager.ExpertFileManager();
        MemberManager.FindByCode(MeId);

        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.MemberIsNotValid));
            return;
        }
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
        {
            ShowMessage(Msg);
            return;
        }

        if (Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
        {
            ShowMessage("عضویت انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
            return;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
        {
            ShowMessage("تاریخ اعتبار پروانه اشتغال به کار عضو انتخاب شده نامشخص می باشد.");
            return;
        }
        int compare = string.Compare(MemberManager[0]["FileDate"].ToString(), Utility.GetDateOfToday());
        if (compare < 0)
        {
            ShowMessage("تاریخ اعتبار پروانه اشتغال به کار عضو انتخاب شده به پایان رسیده است.");
            return;
        }
        ExpertFileManager.FindByMeId(MeId);
        if (ExpertFileManager.Count > 0)
        {
            ShowMessage("پیش از این برای عضو انتخاب شده درخواست کارشناس ماده 27 ثبت شده است.");
            return;
        }
        lblMeId.Text = MeId.ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
            lblFirstName.Text = MemberManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
            lblLastName.Text = MemberManager[0]["LastName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
        else
            imgMember.ImageUrl = "";
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
            lblFileNo.Text = MemberManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
            lblFileDate.Text = MemberManager[0]["FileDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FatherName"]))
            lblFatherName.Text = MemberManager[0]["FatherName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            lblSSN.Text = MemberManager[0]["SSN"].ToString();

    }
    protected void flpAttach_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageAttach(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }

    private void SetKeys()
    {
        try
        {
            _EfId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EfId"].ToString()));
            _EfReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["EfReqId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);

            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
            SetMode(_PageMode);
            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        //if (PageMode != "New")
        //{
        //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //    DocMemberFileManager.FindByCode(_MfId, 0);
        //    if (DocMemberFileManager.Count == 1)
        //    {
        //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
        //            lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
        //        else
        //            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        //    }
        //    else
        //    {
        //        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        //    }
        //    KardanImageVisible(PageMode, Convert.ToInt32(DocMemberFileManager[0]["Type"]));
        //}
        //else
        //{
        //    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        //    KardanImageVisible(PageMode, (int)TSP.DataManager.DocumentOfMemberRequestType.New);
        //}

        switch (PageMode)
        {
            case "New":
                SetNewModeKeys();
                lblWorkFlowState.Visible = false;
                break;

            case "View":
                SetViewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "Change":
            case "Invalid":
                SetRequestModeKeys();
                lblWorkFlowState.Visible = false;
                //SetRoundpanelRequestHeaderAndLabales((int)TSP.DataManager.DocumentOfMemberRequestType.Change);
                break;
        }
    }

    private void SetNewModeKeys()
    {
        ClearFirm();
        SetEnable(true);
        _PageMode = "New";
        _EfId = -1;
        _EfReqId = -1;
        RoundPanelPage.HeaderText = "جدید";
        btnEdit.Enabled = btnEdit2.Enabled = false;
        btnSave.Enabled = btnSave2.Enabled = true;
    }

    private void SetViewModeKeys()
    {
        _PageMode = "View";
        RoundPanelPage.HeaderText = "مشاهده";
        SetEnable(false);
        FillForm(_EfReqId);
        btnSave.Enabled = btnSave2.Enabled = false;
    }

    private void SetEditModeKeys()
    {
        _PageMode = "Edit";
        btnSave.Enabled = btnSave2.Enabled = true;
        RoundPanelPage.HeaderText = "ویرایش";
        SetEnable(true);
        FillForm(_EfReqId);
    }

    private void SetRequestModeKeys()
    {
        if (_PageMode == "Change")
            RoundPanelPage.HeaderText = "درخواست جدید-تغییرات";
        else if (_PageMode == "Invalid")
            RoundPanelPage.HeaderText = "درخواست جدید-ابطال";
        SetEnable(true);
        FillForm(_EfReqId);
    }

    private void SetEnable(Boolean Enabled)
    {
        flpBackOldDoc.ClientVisible = flpFrontOldDoc.ClientVisible =
            txtexpExpireDate.Enabled = txtExpFileNo.Enabled = txtexpIssueDate.Enabled = lblMeId.Enabled = Enabled;
    }

    private void ClearFirm()
    {
        txtexpExpireDate.Text = txtExpFileNo.Text = txtexpIssueDate.Text = lblMeId.Text = "";
        lblFatherName.Text
        = lblFileDate.Text = lblFileNo.Text = lblFirstName.Text = lblLastName.Text = lblSSN.Text = lblWorkFlowState.Text = "---";
        imgMember.ImageUrl = hpImgBackOldDoc.ImageUrl = hpImgFrontOldDoc.ImageUrl = "~/Images/noimage.gif";
    }

    private void FillForm(int EfReqId)
    {
        TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager = new TSP.DataManager.ExpertFileRequestManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        ExpertFileRequestManager.FindByCode(EfReqId);
        if (ExpertFileRequestManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["TaskName"]))
            lblWorkFlowState.Text = ExpertFileRequestManager[0]["TaskName"].ToString();
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["ExpireDate"]))
            txtexpExpireDate.Text = ExpertFileRequestManager[0]["ExpireDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["FileNo"]))
            txtExpFileNo.Text = ExpertFileRequestManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["IssueDate"]))
            txtexpIssueDate.Text = ExpertFileRequestManager[0]["IssueDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["DocumentFrontImageURL"]))
            hpImgFrontOldDoc.ImageUrl = ExpertFileRequestManager[0]["DocumentFrontImageURL"].ToString();
        if (!Utility.IsDBNullOrNullValue(ExpertFileRequestManager[0]["DocumentBackImageURL"]))
            hpImgBackOldDoc.ImageUrl = ExpertFileRequestManager[0]["DocumentBackImageURL"].ToString();
        lblMeId.Text = ExpertFileRequestManager[0]["MeId"].ToString();
        lblMeId.Enabled = false;
        MemberManager.FindByCode(Convert.ToInt32(ExpertFileRequestManager[0]["MeId"]));
        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
            lblFirstName.Text = MemberManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
            lblLastName.Text = MemberManager[0]["LastName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
        else
            imgMember.ImageUrl = "~/Images/noimage.gif";

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
            lblFileNo.Text = MemberManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
            lblFileDate.Text = MemberManager[0]["FileDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FatherName"]))
            lblFatherName.Text = MemberManager[0]["FatherName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            lblSSN.Text = MemberManager[0]["SSN"].ToString();

    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave(_PageMode);
        if (_PageMode != "New" && _PageMode != "Change" && _PageMode != "Invalid")
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveExpertFileRequest;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ExpertFileRequest);

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);

        BtnNew.Enabled = BtnNew2.Enabled = WFPermission.BtnNew;
        if (PageMode == "New")
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
        else if (PageMode == "Change" || PageMode == "Invalid")
        {
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnNewRequest;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {

        int TabelId = _EfReqId;// Utility.DecryptQS(Request.QueryString["EfReqId"].ToString());
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.ExpertFileRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveExpertFileRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.ExpertFileRequest;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, TabelId, Utility.GetCurrentUser_UserId(), PageMode);

        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave;
        btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده پرونده کارشناس ماده 27", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                ShowMessage("خطایی در مشاهده اطلاعات انجام گرفته است");
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
    #endregion

    private void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ExpertFileManager ExpertFileManager = new TSP.DataManager.ExpertFileManager();
        TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager = new TSP.DataManager.ExpertFileRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        try
        {
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveExpertFileRequest;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int CurrentNmcId = FindNmcId(int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString()));
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            TransactionManager.Add(ExpertFileManager);
            TransactionManager.Add(ExpertFileRequestManager);
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.BeginSave();
            DataRow drExpFile = ExpertFileManager.NewRow();
            drExpFile["MeId"] = Convert.ToInt32(lblMeId.Text);
            drExpFile["FileNo"] = txtExpFileNo.Text;
            drExpFile["IssueDate"] = txtexpIssueDate.Text;
            drExpFile["ExpireDate"] = txtexpExpireDate.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                drExpFile["DocumentFrontImageURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                drExpFile["DocumentBackImageURL"] = Session["ImgOldDocBackURL"].ToString();
            drExpFile["InActive"] = 0;
            //drExpFile["Description"]=;
            drExpFile["UserId"] = Utility.GetCurrentUser_UserId();
            drExpFile["ModifiedDate"] = DateTime.Now;
            ExpertFileManager.AddRow(drExpFile);
            if (ExpertFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            ExpertFileManager.DataTable.AcceptChanges();
            int EfId = Convert.ToInt32(ExpertFileManager[0]["EfId"]);
            DataRow drExpFileReq = ExpertFileRequestManager.NewRow();
            drExpFileReq["EfId"] = EfId;
            drExpFileReq["FileNo"] = txtExpFileNo.Text;
            drExpFileReq["IssueDate"] = txtexpIssueDate.Text;
            drExpFileReq["ExpireDate"] = txtexpExpireDate.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                drExpFileReq["DocumentFrontImageURL"] = Session["ImgOldDocFrontURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                drExpFileReq["DocumentBackImageURL"] = Session["ImgOldDocBackURL"].ToString();
            drExpFileReq["Type"] = (int)TSP.DataManager.ExpertFileRequestManager.RequestType.New;
            drExpFileReq["IsConfirm"] = 0;
            drExpFileReq["CreateDate"] = Utility.GetDateOfToday();
            drExpFileReq["Description"] = "";
            drExpFileReq["UserId"] = Utility.GetCurrentUser_UserId();
            drExpFileReq["ModifiedDate"] = DateTime.Now;
            drExpFileReq["CurrentWFStateId"] = -1;
            ExpertFileRequestManager.AddRow(drExpFileReq);
            if (ExpertFileRequestManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            ExpertFileRequestManager.DataTable.AcceptChanges();
            int EfReqId = Convert.ToInt32(ExpertFileRequestManager[0]["EfReqId"]);

            #region  WF
            string Description = "شروع گردش کار درخواست کارشناس ماده 27";
            int WfStartId = WorkFlowStateManager.StartWorkFlow(EfReqId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStartId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            ExpertFileRequestManager[0].BeginEdit();
            ExpertFileRequestManager[0]["CurrentWFStateId"] = WfStartId;
            ExpertFileRequestManager[0].EndEdit();
            ExpertFileRequestManager.Save();
            TransactionManager.EndSave();
            _PageMode = "Edit";
            _EfReqId = EfReqId;
            _EfId = EfId;
            SetEditModeKeys();
            ShowMessage("ذخیره با موفقیت انجام شد.");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }

    private void Edit(int EfReqId)
    {
        TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager = new TSP.DataManager.ExpertFileRequestManager();
        try
        {
            ExpertFileRequestManager.FindByCode(EfReqId);
            if (ExpertFileRequestManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            ExpertFileRequestManager[0].BeginEdit();
            ExpertFileRequestManager[0]["FileNo"] = txtExpFileNo.Text;
            ExpertFileRequestManager[0]["IssueDate"] = txtexpIssueDate.Text;
            ExpertFileRequestManager[0]["ExpireDate"] = txtexpExpireDate.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                ExpertFileRequestManager[0]["DocumentFrontImageURL"] = Session["ImgOldDocFrontURL"].ToString();
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                ExpertFileRequestManager[0]["DocumentBackImageURL"] = Session["ImgOldDocBackURL"].ToString();
            ExpertFileRequestManager[0]["Description"] = "";
            ExpertFileRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ExpertFileRequestManager[0]["ModifiedDate"] = DateTime.Now;
            ExpertFileRequestManager[0].EndEdit();
            ExpertFileRequestManager.Save();
            ShowMessage("ذخیره با موفقیت انجام شد.");
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }
    private void InsertRequest(TSP.DataManager.ExpertFileRequestManager.RequestType RequestType)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager = new TSP.DataManager.ExpertFileRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        try
        {
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveExpertFileRequest;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int CurrentNmcId = FindNmcId(int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString()));
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            TransactionManager.Add(ExpertFileRequestManager);
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.BeginSave();
            DataRow drExpFileReq = ExpertFileRequestManager.NewRow();
            drExpFileReq["EfId"] = _EfId;
            drExpFileReq["FileNo"] = txtExpFileNo.Text;
            drExpFileReq["IssueDate"] = txtexpIssueDate.Text;
            drExpFileReq["ExpireDate"] = txtexpExpireDate.Text;
            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocFrontURL"]))
                drExpFileReq["DocumentFrontImageURL"] = Session["ImgOldDocFrontURL"].ToString();
            else drExpFileReq["DocumentFrontImageURL"] = hpImgBackOldDoc.ImageUrl;

            if (!Utility.IsDBNullOrNullValue(Session["ImgOldDocBackURL"]))
                drExpFileReq["DocumentBackImageURL"] = Session["ImgOldDocBackURL"].ToString();
            else
                drExpFileReq["DocumentBackImageURL"] = hpImgFrontOldDoc.ImageUrl;
            drExpFileReq["Type"] = (int)RequestType;
            drExpFileReq["IsConfirm"] = 0;
            drExpFileReq["CreateDate"] = Utility.GetDateOfToday();
            drExpFileReq["Description"] = "";
            drExpFileReq["UserId"] = Utility.GetCurrentUser_UserId();
            drExpFileReq["ModifiedDate"] = DateTime.Now;
            drExpFileReq["CurrentWFStateId"] = -1;
            ExpertFileRequestManager.AddRow(drExpFileReq);
            if (ExpertFileRequestManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            ExpertFileRequestManager.DataTable.AcceptChanges();
            int EfReqId = Convert.ToInt32(ExpertFileRequestManager[0]["EfReqId"]);

            #region  WF
            string Description = "شروع گردش کار درخواست کارشناس ماده 27";
            int WfStartId = WorkFlowStateManager.StartWorkFlow(EfReqId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
            if (WfStartId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            ExpertFileRequestManager[0].BeginEdit();
            ExpertFileRequestManager[0]["CurrentWFStateId"] = WfStartId;
            ExpertFileRequestManager[0].EndEdit();
            ExpertFileRequestManager.Save();
            TransactionManager.EndSave();
            _PageMode = "Edit";
            _EfReqId = EfReqId;
            SetEditModeKeys();
            ShowMessage("ذخیره با موفقیت انجام شد.");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
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
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }
    protected string SaveImageAttach(UploadedFile uploadedFile, string id)
    {
        string ret = "";


        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId_" + lblMeId.Text + Path.GetRandomFileName() + ImageType.Extension;

            } while ((id == "flpFrontOldDoc" && File.Exists(MapPath("~/image/27/FileImage/") + ret) == true)
                  || (id == "flpBackOldDoc" && File.Exists(MapPath("~/image/27/FileImage/") + ret) == true));

            string FileName = "";


            if (id == "flpFrontOldDoc")
            {
                Session["ImgOldDocFrontURL"] = "~/image/27/FileImage/" + ret;
                FileName = MapPath("~/image/27/FileImage/") + ret;
            }

            else if (id == "flpBackOldDoc")
            {
                Session["ImgOldDocBackURL"] = "~/image/27/FileImage/" + ret;
                FileName = MapPath("~/image/27/FileImage/") + ret;
            }

            uploadedFile.SaveAs(FileName, true);
        }
        return ret;
    }
    #endregion
}