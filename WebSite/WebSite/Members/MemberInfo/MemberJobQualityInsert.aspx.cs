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

public partial class Employee_MembersRegister_MemberJobQualityInsert : System.Web.UI.Page
{
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
            Session["IsEdited_MeJobQ"] = false;

            Session["JobQUpload"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["JhId"]) || string.IsNullOrEmpty(Request.QueryString["JhqId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");

                return;
            }

            OdbFactorDocuments.FilterParameters[0].DefaultValue = "2";

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["JPageMode"].ToString());
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
                JhQualityId.Value = Server.HtmlDecode(Request.QueryString["JhqId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();


            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string MeId = Utility.DecryptQS(MemberId.Value);
            string JhId = Utility.DecryptQS(JobId.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);
            string JhqId = Utility.DecryptQS(JhQualityId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(JhId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                Page_Load_TempMember();
            else
                Page_Load_Member();

            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            switch (Mode)
            {
                case "Home":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

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
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
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
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];


    }

    void Page_Load_Member()
    {
        string JhqId = Utility.DecryptQS(JhQualityId.Value);
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if (MeManager.Count > 0)
        {
            lblSex.Text = MeManager[0]["SexName1"].ToString();
            lblT.Text = MeManager[0]["TiName"].ToString();
            lblOfName.Text = MeManager[0]["MeName"].ToString();
        }
        switch (PageMode)
        {
            case "View":
                Disable();
                flp.Visible = false;
                hpFilePath.Visible = true;

                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(int.Parse(JhqId));
                ASPxRoundPanel2.HeaderText = "مشاهده";

                break;

            case "New":

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                break;

            case "Edit":

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;

                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                hpFilePath.Visible = true;
                FillForm(int.Parse(JhqId));
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";


                break;


        }
    }

    void Page_Load_TempMember()
    {
        string JhqId = Utility.DecryptQS(JhQualityId.Value);
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if (MeManager.Count > 0)
        {
            lblSex.Text = MeManager[0]["SexName1"].ToString();
            lblT.Text = MeManager[0]["TiName"].ToString();
            lblOfName.Text = MeManager[0]["MeName"].ToString();
        }
        switch (PageMode)
        {
            case "View":
                Disable();
                flp.Visible = false;
                hpFilePath.Visible = true;

                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                FillForm(int.Parse(JhqId));
                ASPxRoundPanel2.HeaderText = "مشاهده";

                break;

            case "New":

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                ASPxRoundPanel2.HeaderText = "جدید";
                break;

            case "Edit":

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;

                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                hpFilePath.Visible = true;
                FillForm(int.Parse(JhqId));
                ASPxRoundPanel2.Enabled = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";


                break;


        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberJobQuality.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&aPageMode=" + Request.QueryString["aPageMode"] + "&MReId=" + MemberRequest.Value + "&JhId=" + JobId.Value + "&Mode=" + HDMode.Value);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string JhqId = Utility.DecryptQS(JhQualityId.Value);

        if (string.IsNullOrEmpty(JhqId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            Enable();
            flp.Visible = true;

            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string JhqId = Utility.DecryptQS(JhQualityId.Value);

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

                    if (string.IsNullOrEmpty(JhqId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        Edit(int.Parse(JhqId));

                    break;


            }

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

        JhQualityId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        flp.Visible = true;
        RoundPanelJudge.Visible = false;
        hpFilePath.Visible = false;
    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void Insert()
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            InsertTempMember();
        else
            InsertMember();
    }

    protected void InsertMember()
    {
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager2 = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(WorkFlowStateManager);
        trans.Add(JobQualityManager);

        try
        {

            int JhId = int.Parse(Utility.DecryptQS(JobId.Value));

            JobQualityManager2.FindByJobCode(JhId);

            for (int i = 0; i < JobQualityManager2.Count; i++)
            {
                if (JobQualityManager2[i]["OfdId"].ToString() == CmbName.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }
            trans.BeginSave();
            DataRow drQ = JobQualityManager.NewRow();
            drQ["JhId"] = JhId;
            if (CmbName.Value != null)
                drQ["OfdId"] = CmbName.Value;
            drQ["Mark"] = DBNull.Value;
            drQ["FilePath"] = "~/Image/Members/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
            drQ["CreateDate"] = Utility.GetDateOfToday();
            drQ["Description"] = txtJhDesc.Text;
            drQ["UserId"] = Utility.GetCurrentUser_UserId();
            drQ["ModifiedDate"] = DateTime.Now;
            JobQualityManager.AddRow(drQ);
            int imgcnt = JobQualityManager.Save();
            JobQualityManager.DataTable.AcceptChanges();
            if (imgcnt == 1)
            {
                int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJobQ"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobQuality;
                    int WfCode = -1;
                    WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;

                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, MReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                    hpFilePath.Visible = true;
                    hpFilePath.NavigateUrl = JobQualityManager[0]["FilePath"].ToString();
                    JhQualityId.Value = Utility.EncryptQS(JobQualityManager[0]["JhqId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    Session["IsEdited_MeJobQ"] = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    HDComboName.Value = CmbName.Value.ToString();
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
        try
        {
            if (Session["JobQUpload"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/Members/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

            }
        }
        catch (Exception)
        { }


    }

    protected void InsertTempMember()
    {
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager2 = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(WorkFlowStateManager);
        trans.Add(JobQualityManager);

        try
        {

            int JhId = int.Parse(Utility.DecryptQS(JobId.Value));

            JobQualityManager2.FindByJobCode(JhId);

            for (int i = 0; i < JobQualityManager2.Count; i++)
            {
                if (JobQualityManager2[i]["OfdId"].ToString() == CmbName.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }
            trans.BeginSave();
            DataRow drQ = JobQualityManager.NewRow();
            drQ["JhId"] = JhId;
            if (CmbName.Value != null)
                drQ["OfdId"] = CmbName.Value;
            drQ["Mark"] = DBNull.Value;
            drQ["FilePath"] = "~/Image/Members/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
            drQ["CreateDate"] = Utility.GetDateOfToday();
            drQ["Description"] = txtJhDesc.Text;
            drQ["UserId"] = Utility.GetCurrentUser_UserId();
            drQ["ModifiedDate"] = DateTime.Now;
            JobQualityManager.AddRow(drQ);
            int imgcnt = JobQualityManager.Save();
            JobQualityManager.DataTable.AcceptChanges();
            if (imgcnt == 1)
            {
                int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeJobQ"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobQuality;
                    int WfCode = -1;
                    WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;

                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, MReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                    hpFilePath.Visible = true;
                    hpFilePath.NavigateUrl = JobQualityManager[0]["FilePath"].ToString();
                    JhQualityId.Value = Utility.EncryptQS(JobQualityManager[0]["JhqId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    Session["IsEdited_MeJobQ"] = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    HDComboName.Value = CmbName.Value.ToString();
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
        try
        {
            if (Session["JobQUpload"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/Members/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

            }
        }
        catch (Exception)
        { }


    }

    protected void Edit(int JhqId)
    {
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager2 = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(WorkFlowStateManager);
        trans.Add(JobQualityManager);


        try
        {

            int JhId = int.Parse(Utility.DecryptQS(JobId.Value));
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            JobQualityManager2.FindByJobCode(JhId);
            if (HDComboName == null && string.IsNullOrEmpty(HDComboName.Value))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string SelectedOfdId = HDComboName.Value;
            for (int i = 0; i < JobQualityManager2.Count; i++)
            {
                if (JobQualityManager2[i]["OfdId"].ToString() == CmbName.Value.ToString() && SelectedOfdId != CmbName.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }
            trans.BeginSave();
            JobQualityManager.FindByCode(JhqId);
            if (JobQualityManager.Count > 0)
            {
                JobQualityManager[0].BeginEdit();
                if (CmbName.Value != null)
                    JobQualityManager[0]["OfdId"] = CmbName.Value;
                if (Session["JobQUpload"] != null)
                    JobQualityManager[0]["FilePath"] = "~/Image/Members/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
                JobQualityManager[0]["Description"] = txtJhDesc.Text;
                JobQualityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                JobQualityManager[0]["ModifiedDate"] = DateTime.Now;
                JobQualityManager[0].EndEdit();
                int imgcnt = JobQualityManager.Save();
                if (imgcnt == 1)
                {
                    int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_MeJobQ"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.ProjectJobQuality;

                        int WfCode = -1;
                        WfCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;


                        if (WfCode == -1)
                        {
                            trans.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, MReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                        HDComboName.Value = CmbName.Value.ToString();
                    }

                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
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
        try
        {
            if (Session["JobQUpload"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/Members/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

            }
        }
        catch (Exception)
        { }

    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/JobQuality/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["JobQUpload"] = tempFileName;
        }
        return ret;
    }


    protected void Enable()
    {
        txtJhDesc.Enabled = true;
        flp.Enabled = true;
        CmbName.Enabled = true;



    }

    protected void Disable()
    {
        txtJhDesc.Enabled = false;
        flp.Enabled = false;
        CmbName.Enabled = false;


    }

    protected void ClearForm()
    {
        txtJhDesc.Text = "";
        CmbName.DataBind();
        CmbName.SelectedIndex = -1;

        HDFlpMember.Set("name", "0");


    }

    protected void FillForm(int JhqId)
    {
        TSP.DataManager.DocOffJobHistoryQualityManager JhqManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        JhqManager.FindByCode(JhqId);
        if (JhqManager.Count > 0)
        {
            txtJhDesc.Text = JhqManager[0]["Description"].ToString();
            if (!Utility.IsDBNullOrNullValue(JhqManager[0]["OfdId"]))
            {
                CmbName.DataBind();
                CmbName.SelectedIndex = CmbName.Items.IndexOfValue(int.Parse(JhqManager[0]["OfdId"].ToString()));
                HDComboName.Value = JhqManager[0]["OfdId"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(JhqManager[0]["FilePath"]))
            {
                hpFilePath.NavigateUrl = JhqManager[0]["FilePath"].ToString();
                HDFlpMember["name"] = 1;

            }

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
        }
    }
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
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
                                return true;

                            }

                        }

                    }

                }

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

}
