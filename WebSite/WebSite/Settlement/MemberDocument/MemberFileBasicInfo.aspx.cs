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
using System.Globalization;

public partial class Settlement_MemberDocument_MemberFileBasicInfo : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MeId"] == null || Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("MemberFile.aspx");
            }

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
            if (PageMode == "Edit")
            {
                string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());

                if (string.IsNullOrEmpty(MFId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditMemberFile(int.Parse(MFId));
                }

            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("MemberFile.aspx?PostId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("MemberFile.aspx");
        }
    }  

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtRegisterDate(Convert.ToInt32(e.Parameter));
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
        HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];
        HiddenFieldDocMemberFile["MeId"] = Request.QueryString["MeId"];

        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
        string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());
     
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
    }

    private void SetMode(string PageMode)
    {
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
            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        //MenuMemberFile.Enabled = true;

        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);
        txtRegDate.Enabled = false;
        txtSerialNo.Enabled = false;
        txtExpDate.Enabled = false;
        cmbIsTemporary.Enabled = false;
        RoundPanelMemberFile.HeaderText = "مشاهده";
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetEditModeKeys()
    {
        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (HiddenFieldDocMemberFile["MFId"] == null || string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        //MenuMemberFile.Enabled = true;
        EnableControls();
        FillForm(MFId);
        RoundPanelMemberFile.HeaderText = "ویرایش";
    }

    private void FillForm(int MFId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        DocMemberFileManager.ClearBeforeFill = true;

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
                ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            else
                ImgMember.ImageUrl = "../../Images/Person.png";
        }

        TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
        if (TransferMemberManager.Count > 0)
        {
            tblProvince.Visible = true;
            tblProvince.Visible = true;
            lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
            lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
            lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                lblPreProvince.Text = TransferMemberManager[0]["PrName"].ToString();
            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FirstDocRegDate"]))
                txtFirstDocRegDate.Text = TransferMemberManager[0]["FirstDocRegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocRegDate"]))
                txtCurrentDocRegDate.Text = TransferMemberManager[0]["CurrentDocRegDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["CurrentDocExpDate"]))
                txtCurrentDocExpDate.Text = TransferMemberManager[0]["CurrentDocExpDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrName"]))
            {
                txtDocPrName.Text = TransferMemberManager[0]["DocPrName"].ToString();
            }

        }
        else
            tblProvince.Visible = false;
        DataTable dtDocMe = DocMemberFileManager.SelectMainRequest(MeId, 0);
        if (dtDocMe.Rows.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["RegDate"]))
                lblRegDate.Text = DocMemberFileManager[0]["RegDate"].ToString();
        }
        else
        {
            Response.Redirect("MemberFile.aspx");
        }
        DocMemberFileManager.FindByCode(MFId, 0);
        if (DocMemberFileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                txtExpDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                txtSerialNo.Text = DocMemberFileManager[0]["SerialNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                txtRegDate.Text = DocMemberFileManager[0]["RegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
                cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());

         
        }
        if (string.IsNullOrWhiteSpace(txtExpDate.Text) && string.IsNullOrWhiteSpace(txtExpDate.Text))
        {
            SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
        }
    }

    private void ClearForm()
    {
        cmbIsTemporary.SelectedIndex = 0;
        txtExpDate.Text = "";
        txtRegDate.Text = "";
        txtSerialNo.Text = "";
    }

    private void EnableControls()
    {
        txtSerialNo.Enabled = true;
        txtExpDate.Enabled = true;
        txtRegDate.Enabled = true;
        cmbIsTemporary.Enabled = true;
    }

    private void EditMemberFile(int MfId)
    {
        if (CheckSerialNo())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شماره سریال وارد شده تکراری می باشد.";
            return;
        }
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        try
        {
            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].BeginEdit();

                DocMemberFileManager[0]["SerialNo"] = txtSerialNo.Text;
                DocMemberFileManager[0]["RegDate"] = txtRegDate.Text;
                DocMemberFileManager[0]["ExpireDate"] = txtExpDate.Text;
                DocMemberFileManager[0]["IsTemporary"] = cmbIsTemporary.SelectedItem.Value;
                DocMemberFileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileManager[0]["ModifiedDate"] = DateTime.Now;

                DocMemberFileManager[0].EndEdit();

                if (DocMemberFileManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
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
            SetError(err);
        }
    }

    bool CheckSerialNo()
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DataTable dt = DocMemberFileManager.SelectDocMemberFileBySerialNo(Convert.ToInt32(txtSerialNo.Text.Trim()));
        if (dt.Rows.Count > 0)
            return true;
        return false;
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
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, int.Parse(Session["Login"].ToString()));
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
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
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

    private void SetMeDocDefualtRegisterDate(int DocumentSetExpireDateType)
    {
        txtRegDate.Text = Utility.GetDateOfToday();
        Utility.Date Date = new Utility.Date();
        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    #endregion
}
