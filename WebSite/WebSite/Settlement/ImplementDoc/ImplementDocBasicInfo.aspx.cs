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
using System.Globalization;

public partial class Settlement_ImplementDoc_ImplementDocBasicInfo : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("ImplementDoc.aspx");
            }

            //TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
            btnSave.Enabled = true;
            btnSave2.Enabled = true;
            SetKeys();
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());

            if (string.IsNullOrEmpty(MFId) && PageMode != "New")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else if (PageMode == "Edit")
            {
                Edit(int.Parse(MFId));
            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("ImplementDoc.aspx?PostId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("ImplementDoc.aspx");
        }
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobHistory":
                Response.Redirect("~/Settlement/MemberDocument/MemberJobHistory.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS(((int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp).ToString()));
                break;
            case "Financial":
                Response.Redirect("FinancialStatus.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS(((int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp).ToString()));
                break;
        }
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
        HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];
        int ImpDocId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.ClearBeforeFill = true;

        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

        if (PageMode != "New")
        {
            DocMemberFileManager.SelectImplementDoc(-1, ImpDocId);
            if (DocMemberFileManager.Count == 1)
            {
                HiddenFieldDocMemberFile["MemberFileId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                int MfId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MemberFileId"].ToString()));

                DocMemberFileManager.FindByCode(MfId, 0);
                if (DocMemberFileManager.Count == 1)
                {
                    HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                }
            }
            // string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
        CheckWorkFlowPermission();
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectImplementDoc(-1, int.Parse(MFId));
        if (DocMemberFileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        else
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
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

    private void SetViewModeKeys()
    {
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        MenuMemberFile.Enabled = true;
        DisableControls();
        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);

        RoundPanelMemberFile.HeaderText = "مشاهده";
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        //TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (HiddenFieldDocMemberFile["MFId"] == null || string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));

        MenuMemberFile.Enabled = true;

        EnableControls();
        FillForm(MFId);
        if (string.IsNullOrEmpty(txtExpireDate.Text) && string.IsNullOrEmpty(txtLastRegDateImp.Text))
        {
            string Today = Utility.GetDateOfToday();
            txtLastRegDateImp.Text = Today;

            PersianCalendar FC = new PersianCalendar();
            // DateTime EndDateMiladi = Utility.Date.ShamsiToMiladi(int.Parse(Today.Substring(0, 4)), int.Parse(Today.Substring(5, 2)), int.Parse(Today.Substring(8, 2)));
            DateTime DtAddYear = FC.AddYears(DateTime.Now.Date, 3);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            string ExpireDate = PDate.GetYear(DtAddYear) + "/" + PDate.GetMonth(DtAddYear).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYear).ToString().PadLeft(2, '0');
            txtExpireDate.Text = ExpireDate;
        }
        RoundPanelMemberFile.HeaderText = "ویرایش";
    }

    private void Edit(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        try
        {
            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["SerialNo"] = txtSerialNo.Text;
                DocMemberFileManager[0]["RegDate"] = txtLastRegDateImp.Text;
                DocMemberFileManager[0]["ExpireDate"] = txtExpireDate.Text;
                DocMemberFileManager[0]["IsTemporary"] = cmbIsTemporary.SelectedItem.Value;
                DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;
                DocMemberFileManager[0]["MailNo"] = "";
                DocMemberFileManager[0]["MailDate"] = "";
                DocMemberFileManager[0].EndEdit();
                int cn = DocMemberFileManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = ("اطلاعات توسط کاربر دیگری تغییر یافته است.");
                return;
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    //private void FillForm(int MfId)
    //{
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
    //    TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
    //    // TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
    //    TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
    //    DocMemberFileManager.ClearBeforeFill = true;

    //    int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
    //    MemberManager.FindByCode(MeId);
    //    if (MemberManager.Count == 1)
    //    {
    //        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeId"]))
    //            txtMeId.Text = MemberManager[0]["MeId"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
    //            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
    //            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
    //            ImgMemeber.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
    //        else
    //            ImgMemeber.ImageUrl = "../../Images/Person.png";
    //    }

    //    DocMemberFileManager.FindByCode(MfId, 1);

    //    if (DocMemberFileManager.Count == 1)
    //    {
    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
    //            cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());
    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
    //            txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();

    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
    //            txtExpireDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
    //            txtLastRegDateImp.Text = DocMemberFileManager[0]["RegDate"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
    //            txtMfNoImp.Text = DocMemberFileManager[0]["MFNo"].ToString();
    //        if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MailNo"]))
    //        {
    //            RoundPanelRequest.Visible = true;
    //            txtMailNo.Text = DocMemberFileManager[0]["MailNo"].ToString();
    //            txtMailDate.Text = DocMemberFileManager[0]["MailDate"].ToString();
    //            TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();
    //            LettersManager.FindByLetterNumber(txtMailNo.Text);
    //            if (LettersManager.Count == 1)
    //            {
    //                txtMailTitle.Text = LettersManager[0]["Title"].ToString();
    //            }
    //        }
    //        else
    //            RoundPanelRequest.Visible = false;
    //        int DocMemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());

    //        DataTable dtDocMeDetail = DocMemberFileDetailManager.FindByResponsibility(DocMemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
    //        if (dtDocMeDetail.Rows.Count > 0)
    //        {
    //            DataTable dtMFMajor = DocMemberFileMajorManager.SelectMemberFileById(DocMemberFileId, MeId, 0, 1);
    //            if (dtMFMajor.Rows.Count > 0)
    //            {
    //                int MasterMjId = (int)dtMFMajor.Rows[0]["FMjId"];
    //                if (dtDocMeDetail.Rows[0]["MjId"].ToString() == dtMFMajor.Rows[0]["FMjId"].ToString())
    //                {
    //                    txtGradeImp.Text = dtDocMeDetail.Rows[0]["GrdName"].ToString();
    //                }
    //            }
    //        }

    //        //if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
    //        //{
    //        //    ProvinceManager.FindByCode(int.Parse(DocMemberFileManager[0]["PrId"].ToString()));
    //        //    if (ProvinceManager.Count > 0)
    //        //    {
    //        //        txtProvinceNameImp.Text = ProvinceManager[0]["PrName"].ToString();
    //        //    }
    //        //}

    //        //DataTable dtImpDoc = DocMemberFileManager.SelectImplementDoc(MeId, MfId);
    //        //if (dtImpDoc.Rows.Count > 0)
    //        //{
    //        //    txtRegDateImp.Text = dtImpDoc.Rows[0]["RegDate"].ToString();
    //        //}

    //        DocMemberFileManager.FindByCode(DocMemberFileId, 0);
    //        if (DocMemberFileManager.Count == 1)
    //        {
    //            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
    //                lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
    //        }
    //    }
    //}

    private void FillForm(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MeId"]))
                    txtMeId.Text = MemberManager[0]["MeId"].ToString();
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                    lblMeName.Text = MemberManager[0]["FirstName"].ToString();
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                    lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                    ImgMemeber.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
                else
                    ImgMemeber.ImageUrl = "../../Images/Person.png";
            }

            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                    txtExpireDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                    txtLastRegDateImp.Text = DocMemberFileManager[0]["RegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    txtMfNoImp.Text = DocMemberFileManager[0]["MFNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
                    cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                    txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
                //if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MailNo"]))
                //{
                //    RoundPanelRequest.Enabled = true;
                    //txtMailNo.Text = DocMemberFileManager[0]["MailNo"].ToString();
                    //txtMailDate.Text = DocMemberFileManager[0]["MailDate"].ToString();
                //}

                lblWorkFlowState.Visible = true;
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";


                int DocMemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                DataTable dtDocMeDetail = DocMemberFileDetailManager.FindByResponsibility(DocMemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
                if (dtDocMeDetail.Rows.Count > 0)
                {
                    DataTable dtMFMajor = DocMemberFileMajorManager.SelectMemberFileById(DocMemberFileId, MeId, 0, 1);
                    if (dtMFMajor.Rows.Count > 0)
                    {
                        int MasterMjId = (int)dtMFMajor.Rows[0]["FMjId"];
                        if (dtDocMeDetail.Rows[0]["MjId"].ToString() == dtMFMajor.Rows[0]["FMjId"].ToString())
                        {
                            txtGradeImp.Text = dtDocMeDetail.Rows[0]["GrdName"].ToString();
                        }
                    }
                }

                DocMemberFileManager.FindByCode(DocMemberFileId, 0);
                if (DocMemberFileManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                        lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    private void ClearForm()
    {

    }

    private void EnableControls()
    {
        cmbIsTemporary.Enabled = true;
        txtExpireDate.Enabled = true;
        txtLastRegDateImp.Enabled = true;
        txtSerialNo.Enabled = true;
    }

    private void DisableControls()
    {
        cmbIsTemporary.Enabled = false;
        txtExpireDate.Enabled = false;
        txtLastRegDateImp.Enabled = false;
        txtSerialNo.Enabled = false;
    }

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    private void SetError(Exception err)
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                if (Permission > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
            // CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            // BtnNew.Enabled = true;
            //  btnNew2.Enabled = true;
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
            //  BtnNew.Enabled = false;
            //  btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //   this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingImplementDoc;
        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
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
                    //    btnEdit.Enabled = true;
                    //   btnEdit2.Enabled = true;
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    //  btnEdit.Enabled = false;
                    // btnEdit2.Enabled = false;
                    break;
            }
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    #endregion
}
