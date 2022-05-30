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

public partial class Employee_Document_AddMemeberJobQualification : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["JobQUpload"] = null;
            if (string.IsNullOrEmpty(Request.QueryString["JhId"]) || string.IsNullOrEmpty(Request.QueryString["JhqId"]) || string.IsNullOrEmpty(Request.QueryString["DocType"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["PrePgMd"]))
            {
                Response.Redirect("MemberJobHistory.aspx");
                return;
            }

            OdbFactorDocuments.FilterParameters[0].DefaultValue = "2";

            TSP.DataManager.Permission per = TSP.DataManager.DocOffJobHistoryQualityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (!string.IsNullOrEmpty(txtCmbName.Text))
        {
            CmbName.DataBind();
            CmbName.SelectedIndex = Convert.ToInt32(txtCmbName.Text);

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
        Response.Redirect("MemberJobQualification.aspx?PgMd=" + Request.QueryString["PgMd"] + "&PrePgMd=" + Request.QueryString["PrePgMd"] + "&MfId=" + HiddenFieldQualification["MfId"].ToString() + "&JhId=" + HiddenFieldQualification["JhId"].ToString() + "&DocType=" + HiddenFieldQualification["DocType"].ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string JhqId = Utility.DecryptQS(HiddenFieldQualification["JhqId"].ToString());

        if (string.IsNullOrEmpty(JhqId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            Enable();
            flp.Visible = true;

            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            HiddenFieldQualification["PageMode"] = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldQualification["PageMode"].ToString());
        string JhqId = Utility.DecryptQS(HiddenFieldQualification["JhqId"].ToString());

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
        TSP.DataManager.Permission per = TSP.DataManager.DocOffJobHistoryQualityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        JhQualityId.Value = Utility.EncryptQS("");
        HiddenFieldQualification["PageMode"] = Utility.EncryptQS("New");
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

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Office/JobQuality/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["JobQUpload"] = tempFileName;
        }
        return ret;
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocOffJobHistoryQualityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        try
        {
            // HiddenFieldQualification["JPageMode"] = Server.HtmlDecode(Request.QueryString["JPageMode"].ToString());
            HiddenFieldQualification["JhId"] = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
            HiddenFieldQualification["MfId"] = Server.HtmlDecode(Request.QueryString["MfId"]).ToString();
            HiddenFieldQualification["JhqId"] = Server.HtmlDecode(Request.QueryString["JhqId"]).ToString();
            HiddenFieldQualification["DocType"] = Server.HtmlDecode(Request.QueryString["DocType"]).ToString();
            HiddenFieldQualification["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldQualification["PageMode"].ToString());
        string JhId = Utility.DecryptQS(HiddenFieldQualification["JhId"].ToString());
        // string MfId = Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString());
        string JhqId = Utility.DecryptQS(HiddenFieldQualification["JhqId"].ToString());
       
        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(JhId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }


        switch (PageMode)
        {
            case "View":
                Disable();
                //tbl.Visible = false;
                flp.Visible = false;
                hpFilePath.Visible = true;

                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                //btnEdit.Enabled = per.CanEdit;
                //btnEdit2.Enabled = per.CanEdit;

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

            case "Judge":
                int NmcId = FindNmcId();
                if (NmcId == -1)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();

                TrainingJudgmentManager.FindByNmcId(NmcId, int.Parse(JhqId), 4);
                if (TrainingJudgmentManager.Count > 0)
                {
                    string JudgeId = TrainingJudgmentManager[0][TrainingJudgmentManager.Count - 1].ToString();
                    HDJudgeId.Value = Utility.EncryptQS(JudgeId);
                    FillJugde(int.Parse(JudgeId));
                }
                Disable();
                tbl.Visible = true;
                if (string.IsNullOrEmpty(JhqId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }

                btnEdit.Enabled = per.CanEdit;
                btnEdit2.Enabled = per.CanEdit;

                FillForm(int.Parse(JhqId));
                hpFilePath.Visible = true;

                ASPxRoundPanel2.HeaderText = "مشاهده";
                RoundPanelJudge.Visible = true;
                break;
        }
    }

    private int FindNmcId()
    {
        int NcId = -1;
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;

        //NmcId = NezamChartManager.FindNmcIdByNcId(NcId, UserId, LoginManager);
        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
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
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["FinancialValue"]))
                txtGrade.Text = TrainingJudgmentManager[0]["FinancialValue"].ToString();
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
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        try
        {
            int JhqId = int.Parse(Utility.DecryptQS(JhQualityId.Value));
            DataRow JudgeRow = TrainingJudgmentManager.NewRow();
            JudgeRow["PkId"] = JhqId;
            JudgeRow["CreateDate"] = Utility.GetDateOfToday();
            JudgeRow["MeetingId"] = txtMeetingId.Text;
            JudgeRow["MeetingDate"] = txtMeetingDate.Text;
            JudgeRow["JudgeViewPoint"] = txtViewPoint.Text;
            JudgeRow["FinancialValue"] = txtGrade.Text;
            //JudgeRow["EmpId"] = Utility.GetCurrentUser_MeId();
            //JudgeRow["UltId"] = 4;
            JudgeRow["NmcId"] = FindNmcId();
            JudgeRow["Type"] = 4;
            JudgeRow["IsConfirmed"] = rdbtnIsConfirm.SelectedItem.Value.ToString();
            JudgeRow["UserId"] = Utility.GetCurrentUser_UserId();
            JudgeRow["ModifiedDate"] = DateTime.Now;

            TrainingJudgmentManager.AddRow(JudgeRow);
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

    private void EditJudge(int JudgeId)
    {
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
                TrainingJudgmentManager[0]["FinancialValue"] = txtGrade.Text;
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
        txtCmbName.Text = "";
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
                txtCmbName.Text = CmbName.SelectedIndex.ToString();
                HiddenFieldQualification["SelectedOfdId"] = Utility.EncryptQS(JhqManager[0]["OfdId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(JhqManager[0]["FilePath"]))
            {
                hpFilePath.NavigateUrl = JhqManager[0]["FilePath"].ToString();
                HDFlpMember["name"] = 1;
                hpFilePath.ClientVisible = true;
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
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();

        try
        {

            int JhId = int.Parse(Utility.DecryptQS(HiddenFieldQualification["JhId"].ToString()));

            JobQualityManager.FindByJobCode(JhId);

            for (int i = 0; i < JobQualityManager.Count; i++)
            {
                if (JobQualityManager[i]["OfdId"].ToString() == CmbName.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            DataRow drQ = JobQualityManager.NewRow();
            drQ["JhId"] = JhId;
            if (CmbName.Value != null)
                drQ["OfdId"] = CmbName.Value;
            drQ["Mark"] = DBNull.Value;
            drQ["FilePath"] = "~/Image/ImplementDoc/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
            drQ["CreateDate"] = Utility.GetDateOfToday();
            drQ["Description"] = txtJhDesc.Text;
            drQ["UserId"] = Utility.GetCurrentUser_UserId();
            drQ["ModifiedDate"] = DateTime.Now;
            JobQualityManager.AddRow(drQ);
            int imgcnt = JobQualityManager.Save();
            JobQualityManager.DataTable.AcceptChanges();
            if (imgcnt == 1)
            {
                hpFilePath.Visible = true;
                hpFilePath.NavigateUrl = JobQualityManager[0]["FilePath"].ToString();
                JhQualityId.Value = Utility.EncryptQS(JobQualityManager[0]["JhqId"].ToString());
                HiddenFieldQualification["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldQualification["SelectedOfdId"] = Utility.EncryptQS(CmbName.Value.ToString());
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";

            }
            else
            {
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
        try
        {
            if (Session["JobQUpload"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/ImplementDoc/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

            }
        }
        catch (Exception)
        { }


    }

    protected void Edit(int JhqId)
    {
        TSP.DataManager.DocOffJobHistoryQualityManager JobQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();

        try
        {

            int JhId = int.Parse(Utility.DecryptQS(HiddenFieldQualification["JhId"].ToString()));

            JobQualityManager.FindByJobCode(JhId);
            if (HiddenFieldQualification["SelectedOfdId"] == null && string.IsNullOrEmpty(HiddenFieldQualification["SelectedOfdId"].ToString()))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string SelectedOfdId = Utility.DecryptQS(HiddenFieldQualification["SelectedOfdId"].ToString());
            for (int i = 0; i < JobQualityManager.Count; i++)
            {
                if (JobQualityManager[i]["OfdId"].ToString() == CmbName.Value.ToString() && SelectedOfdId != CmbName.Value.ToString())
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            JobQualityManager.FindByCode(JhqId);
            if (JobQualityManager.Count > 0)
            {
                JobQualityManager[0].BeginEdit();
                if (CmbName.Value != null)
                    JobQualityManager[0]["OfdId"] = CmbName.Value;
                if (Session["JobQUpload"] != null)
                    JobQualityManager[0]["FilePath"] = "~/Image/ImplementDoc/JobQuality/" + Path.GetFileName(Session["JobQUpload"].ToString());
                JobQualityManager[0]["Description"] = txtJhDesc.Text;
                JobQualityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                JobQualityManager[0]["ModifiedDate"] = DateTime.Now;
                JobQualityManager[0].EndEdit();
                int imgcnt = JobQualityManager.Save();
                if (imgcnt == 1)
                {
                    HiddenFieldQualification["SelectedOfdId"] = Utility.EncryptQS(CmbName.Value.ToString());
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";

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
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
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
        try
        {
            if (Session["JobQUpload"] != null)
            {

                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["JobQUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/ImplementDoc/JobQuality/") + Path.GetFileName(Session["JobQUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

            }
        }
        catch (Exception)
        { }

    }

    private void CheckWorkFlowPermission()
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
            string PageMode = Utility.DecryptQS(HiddenFieldQualification["PgMd"].ToString());
            //CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(HiddenFieldQualification["MfId"].ToString());
        int DocType = int.Parse(HiddenFieldQualification["DocType"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = -1;
        int WfCode = -1;
        if (DocType == 0)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            WfCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        }
        else if (DocType == 1)
        {
            TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        }
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
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
    #endregion
}
