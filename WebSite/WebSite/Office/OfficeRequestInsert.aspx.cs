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
using System.Drawing;

public partial class Office_OfficeInfo_OfficeRequestInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    DataTable dtOfImg = null;
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Refresh
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
        #endregion
        //btnEdit.Enabled = false;
        //btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
        {
            Response.Redirect("OfficeMembershipRequest.aspx");
            return;
        }

        if (!IsPostBack)
        {
            #region Reset Sessions
            Session["TblOfReImg"] = null;
            Session["MeReqUpload"] = null;
            Session["FileOfArm2"] = null;
            Session["FileOfSign2"] = null;
            Session["DesObsGrade"] = null;
            Session["OffMenuArrayList"] = null;

            #endregion

            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            #region Set DataTable
            if (Session["TblOfReImg"] == null)
            {
                dtOfImg = new DataTable();
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns.Add("Description");
                dtOfImg.Columns.Add("Id");
                dtOfImg.Columns["Id"].AutoIncrement = true;
                dtOfImg.Columns["Id"].AutoIncrementSeed = 1;

                Session["TblOfReImg"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfReImg"];

            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
            #endregion

            SetKey();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        #region Capacity
        //??????????????SetCapacity();
        #endregion

        //************مربوط به پنل سند**********
        RoundPanelDocument.Visible = false;
        //****************************************

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    //*********************************Buttons*****************************************************************************************
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        string PageName = "OfficeMembershipRequest.aspx";
        switch (Dprt)
        {
            case "MemberShip":
                PageName = "OfficeMembershipRequest.aspx";
                break;
            case "Document":
                PageName = "OfficeRequest.aspx";
                break;
        }
        Response.Redirect(PageName + "?PostId=" + OfficeId.Value);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);

        if (string.IsNullOrEmpty(OfReId) || string.IsNullOrEmpty(OfId))
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
                //if (!CheckPermitionForEdit(int.Parse(OfReId)))
                //{
                //    this.DivReport.Visible = true;
                //    this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
                //    return;
                //}
                //if (CheckCanEditFishForEdit(Convert.ToInt32(OfReId)))
                //{
                //    PanelAccountingInserting.Visible = true;
                //    GridViewAccounting.Columns["Delete"].Visible = true;
                //}
                //else
                //{
                //    PanelAccountingInserting.Visible = false;
                //    GridViewAccounting.Columns["Delete"].Visible = false;
                //}

                Enable();
                txtFileNo.Enabled = true;
                ComboDocType.Enabled = true;
                TblFile.Visible = true;

                imgOfArm.ClientVisible = true;
                imgOfSign.ClientVisible = true;

                PgMode.Value = Utility.EncryptQS("Edit");
                RoundPanelOffice.HeaderText = "ویرایش";
                MenuDetails.Enabled = true;


            }

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (PageMode != "New")
        {
            if (string.IsNullOrEmpty(OfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            if (PageMode != "NewReqMembership" && PageMode != "NewReq")
            {
                if (string.IsNullOrEmpty(OfReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
            }
        }

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                ReqManager.FindByCode(int.Parse(OfReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)//درخواست اولیه یا تغییرات عضویت
                        Edit(int.Parse(OfId), int.Parse(OfReId));
                    else//درخواست ها
                        EditRequest(int.Parse(OfReId), Convert.ToInt32(ReqManager[0]["Type"]));
                }
                break;
            case "Change":
                InsertNewRequest(int.Parse(OfReId), int.Parse(OfId), TSP.DataManager.OfficeRequestType.Change);
                break;
            case "ChangeBaseInfo":
                string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                switch (Dprt)
                {
                    case "MemberShip":
                        InsertMembershipRequest(int.Parse(OfId), (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo);
                        break;
                    case "Document":
                        InsertNewRequest(int.Parse(OfReId), int.Parse(OfId), TSP.DataManager.OfficeRequestType.ChangeBaseInfo);
                        break;
                }
                break;
            case "Revival":
                InsertNewRequest(int.Parse(OfReId), int.Parse(OfId), TSP.DataManager.OfficeRequestType.Revival);
                break;

            case "Reduplicate":
                InsertNewRequest(int.Parse(OfReId), int.Parse(OfId), TSP.DataManager.OfficeRequestType.Reduplicate);
                break;

            case "NewReq":
                InsertReqDocument(int.Parse(OfId));
                break;

            case "NewReqMembership":
                InsertMembershipRequest(int.Parse(OfId), (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset);
                break;
        }

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        imgOfArm.ClientVisible = false;
        imgOfSign.ClientVisible = false;


        OfficeId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;

        ClearForm();
        Enable();
        RoundPanelFileAttachment.Visible = true;
        TblFile.Visible = true;

        ASPxTextBoxFicheCode.Enabled = true;
        RoundPanelOfficeGrade.Visible = false;

    }

    protected void MenuDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        string OfReId = OfficeRequest.Value;
        if (string.IsNullOrEmpty(OfReId))
        {
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByOfficeId(int.Parse(Utility.DecryptQS(OfficeId.Value)), 0, 0);
            if (ReqManager.Count > 0)
                OfReId = Utility.EncryptQS(ReqManager[0]["OfReId"].ToString());//درخواست "ثبت اولیه" برای ثبت اطلاعات
            else
            {
                ReqManager.FindByOfficeId(int.Parse(Utility.DecryptQS(OfficeId.Value)), -1, -1);
                if (ReqManager.Count > 0)
                    OfReId = Utility.EncryptQS(ReqManager[0]["OfReId"].ToString());
            }
        }

        switch (e.Item.Name)
        {

            case "Agent":
                Response.Redirect("~/Office/OfficeInfo/OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());//+ "&Mode=" + Utility.EncryptQS("Home")
                break;
            case "Member":
                Response.Redirect("~/Office/OfficeInfo/OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Letters":
                Response.Redirect("~/Office/OfficeInfo/OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Financial":
                Response.Redirect("~/Office/OfficeInfo/OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Attach":
                Response.Redirect("~/Office/OfficeInfo/OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Group":
                Response.Redirect("~/Office/OfficeInfo/OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Job":
                Response.Redirect("~/Office/OfficeInfo/OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfReId
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    //*********************************FileUploads*****************************************************************************************

    protected void flp_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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

    protected void flpOfArm_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageArm(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {
        if (Session["TblOfReImg"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfReImg"];

            DataRow dr = dtOfImg.NewRow();

            try
            {
                if (Session["MeReqUpload"] != null)
                {

                    dr[0] = "~/Image/Office/OffRequest/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[2] = Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[1] = "~/Image/temp/" + Path.GetFileName(Session["MeReqUpload"].ToString());
                    dr[5] = txtDescImg.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }


                dr[3] = 0;
                dtOfImg.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();

                Session["MeReqUpload"] = null;

                txtDescImg.Text = "";
                ASPxImage2.ClientVisible = false;


            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }

    }

    protected void flpOfSign_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    //**************************************CallBack's***************************************************************
    protected void Callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        Callback.JSProperties["cpPrint"] = 0;
        switch (e.Parameter)
        {
            case "Print":
                Callback.JSProperties["cpPrint"] = 1;
                Callback.JSProperties["cpURL"] = "../ReportForms/OfficeReport.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value;
                //Response.Redirect("~/ReportForms/OfficeReport.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
        }
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }

    #endregion

    #region Methods
    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Sign/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfSign2"] = tempFileName2;
            //Session["FileOfSign2"] = ret;

        }
        return ret;
    }

    protected string SaveImageArm(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Arm/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfArm2"] = tempFileName2;
            //Session["FileOfArm2"] = ret;

        }
        return ret;
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
            } while (File.Exists(MapPath("~/Image/Office/OffRequest/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["MeReqUpload"] = tempFileName;

        }
        return ret;
    }

    #region Accounting
    /*********************************************************************************************************************************************************************/
    private int GetParentAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembersCurrentAccountOffice.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetAccCode()
    {
        string AccCode = Utility.DecryptQS(OfficeId.Value);
        while (AccCode.Length < TSP.DataManager.AccountingAccountManager.TafziliLength)
            AccCode = "0" + AccCode;
        return AccCode;
    }

    private string GetAccName(DataRow Office)
    {
        string Name = Office["OfName"].ToString();
        return Name;
    }

    private int GetMembershipEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembershipEarnings.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetCurrentUser_AgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private string GetDes1(DataRow Of, decimal Amount)
    {
        string Des = "جهت حق عضویت جدید شرکت " + " " + Of["OfName"].ToString() + " " + "به مبلغ" + " " + Amount.ToString("#,#") + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    private string GetDes2(DataRow Of)
    {
        Utility.Date Date = new Utility.Date();
        string Des = "واریز حق عضویت جدید شرکت " + Of["OfName"].ToString() + " " + "جهت سال" + " " + Date.Year.ToString();
        return Des;
    }

    /*********************************************************************************************************************************************************************/
    #endregion

    #region SetKey
    private void SetKey()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
            OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
            HiddenFieldOffice["Department"] = Server.HtmlDecode(Request.QueryString["Dprt"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);

        // ObjectDataSourceGrade.SelectParameters[0].DefaultValue = OfId;

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        ASPxTextBoxAmount.Text = GetFirstMembershipCost(CostSettingsManager).ToString("#,#");
        // TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        // SetCapacity(OffManager, ReqManager, OfReId, OfId);

        SetMode(PageMode);

        if (string.IsNullOrEmpty(OfReId))
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        else
        {
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
            else
            {
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
            }
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewMode();
                break;

            case "New"://صدور عضو حقوقی جدید
                SetNewMode();
                break;

            case "Edit":
                SetEditMode();
                break;

            case "NewReq"://درخواست پروانه
                SetNewReqMode();
                break;

            case "Change":
                SetChangeMode();
                break;

            case "ChangeBaseInfo":
                SetChangeBaseInfoMode();
                break;

            case "Revival":
                SetRevivalMode();
                break;

            case "Reduplicate":
                SetReduplicateMode();
                break;

            case "NewReqMembership"://درخواست تغییرات عضویت
                SetNewReqMembershipMode();
                break;
        }
    }

    private void SetViewMode()
    {
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        btnSave.Enabled = btnSave2.Enabled = false;      
        btnEdit2.Enabled = btnEdit.Enabled = true;
      
        Disable();

        txtFileNo.Enabled = false;

        ComboDocType.Enabled = false;
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;

        if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillRequest(int.Parse(OfId), int.Parse(OfReId));

        ReqManager.FindByCode(int.Parse(OfReId));
        if (ReqManager.Count > 0)
        {
            //if (ReqManager[0]["IsConfirm"].ToString() == "0")
            //    InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.OfficeRequest, int.Parse(OfReId));

            if ((ReqManager[0]["IsConfirm"].ToString() != "0"))//**************پاسخ داده شده   // || (!Convert.ToBoolean(ReqManager[0]["Requester"])))//FromMember
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;

            }

            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست تغییرات عضویت و یادرخواست اولیه
            {
                RoundPanelFileAttachment.Visible = true;
                RoundPanelDocument.Visible = false;
                RoundPanelOfficeGrade.Visible = false;
                TblFile.Visible = false;
                RoundPanelDocumentBasicInfo.Visible = false;

                cmbActivityType.ClientVisible = false;
                lblActivityType.ClientVisible = false;
            }
            else
            {
                //cmbActivityType.ClientVisible = true;
                //lblActivityType.ClientVisible = true;
            }
        }

        RoundPanelOffice.HeaderText = "مشاهده";
    }

    private void SetNewMode()
    {
        RoundPanelDocumentBasicInfo.Visible = false;
        Enable();
        RoundPanelOfficeGrade.Visible = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;
        ClearForm();
        cmbActivityType.ClientVisible = false;
        lblActivityType.ClientVisible = false;
    }

    private void SetEditMode()
    {
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        Enable();
        ASPxTextBoxFicheCode.Enabled = false;

        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillRequest(int.Parse(OfId), int.Parse(OfReId));

        ReqManager.FindByCode(int.Parse(OfReId));
        if (ReqManager.Count > 0)
        {
            //if (ReqManager[0]["IsConfirm"].ToString() == "0")
            //    InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.OfficeRequest, int.Parse(OfReId));

            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
                || Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)
            {
                RoundPanelFileAttachment.Visible = true;
                RoundPanelDocument.Visible = false;
                RoundPanelOfficeGrade.Visible = false;
                RoundPanelDocumentBasicInfo.Visible = false;
                cmbActivityType.ClientVisible = false;
                lblActivityType.ClientVisible = false;
            }
        }

        RoundPanelOffice.Enabled = true;
        RoundPanelOffice.HeaderText = "ویرایش";
        TblFile.Visible = true;
    }

    private void SetNewReqMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        DisableForReq();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        OfficeRequestManager.FindByOfficeId(Convert.ToInt32(OfId), 1, -1);
        int OfReId = -1;
        if (OfficeRequestManager.Count > 0)
        {
            OfReId = Convert.ToInt32(OfficeRequestManager[0]["OfReId"]);
        }
        FillRequest(int.Parse(OfId), OfReId);
        FillDocumentReq(OfReId);

        ClearDocumentInfo();
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "جدید";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
        RoundPanelDocument.Visible = false;
        TblFile.Visible = true;
    }

    private void SetChangeMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        DisableForReq();
        // FillForm(int.Parse(OfId));
        // FillDocumentReq(int.Parse(OfReId));
        FillRequest(int.Parse(OfId), int.Parse(OfReId));

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "درخواست تغییرات پروانه";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
        RoundPanelDocument.Visible = false;
        TblFile.Visible = true;
    }

    private void SetChangeBaseInfoMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        // DisableForReq();
        // FillForm(int.Parse(OfId));
        // FillDocumentReq(int.Parse(OfReId));
        FillRequest(int.Parse(OfId), int.Parse(OfReId));

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "درخواست تغییرات اطلاعات پایه";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
        RoundPanelFileAttachment.Enabled = false;
        RoundPanelDocumentBasicInfo.Enabled = false;
        RoundPanelOfficeGrade.Enabled = false;

        RoundPanelDocument.Visible = false;
        TblFile.Visible = true;

        DisableForChangeBaseInfo();

        //cmbActivityType.ClientVisible = true;
        //lblActivityType.ClientVisible = true;
    }

    private void SetRevivalMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        DisableForReq();
        // FillForm(int.Parse(OfId));
        // FillDocumentReq(int.Parse(OfReId));
        FillRequest(int.Parse(OfId), int.Parse(OfReId));
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "درخواست تمدید پروانه";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
        RoundPanelDocument.Visible = false;
        TblFile.Visible = true;
        //cmbActivityType.ClientVisible = true;
        //lblActivityType.ClientVisible = true;
    }

    private void SetReduplicateMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        DisableForReq();
        // FillForm(int.Parse(OfId));
        // FillDocumentReq(int.Parse(OfReId));
        FillRequest(int.Parse(OfId), int.Parse(OfReId));
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        RoundPanelOffice.HeaderText = "درخواست صدور المثنی";
        MenuDetails.Enabled = false;
        RoundPanelFileAttachment.Visible = true;
        ComboDocType.Enabled = false;
        RoundPanelDocument.Visible = false;
        TblFile.Visible = true;
        //cmbActivityType.ClientVisible = true;
        //lblActivityType.ClientVisible = true;
    }

    private void SetNewReqMembershipMode()
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        imgOfArm.ClientVisible = true;
        imgOfSign.ClientVisible = true;
        DisableForReq();
        FillForm(int.Parse(OfId));

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        RoundPanelOffice.HeaderText = "درخواست تغییرات عضویت";

        MenuDetails.Enabled = false;

        RoundPanelFileAttachment.Visible = true;
        RoundPanelDocument.Visible = false;
        RoundPanelOfficeGrade.Visible = false;
        RoundPanelDocumentBasicInfo.Visible = false;

        TblFile.Visible = true;

        cmbActivityType.ClientVisible = false;
        lblActivityType.ClientVisible = false;
    }
    #endregion

    #region Fill
    protected void FillDocumentReq(int OfReId)
    {
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();

        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        RoundPanelFileAttachment.Visible = true;
        txtOfId.Text = ReqManager[0]["OfId"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
            txtMeNo.Text = ReqManager[0]["MeNo"].ToString();


        //***********Document GRade*****************
        if (ReqManager[0]["MFType"].ToString() == "1")//شرکت طراح و ناظر
        {
            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
            cmbActivityType.SelectedIndex = -1;
            CustomAspxDevGridViewGrade.Visible = true;
            TableImp.Visible = false;
            lblActivityType.ClientVisible = false;
            cmbActivityType.ClientVisible = false;
            SetOfficeDesObsGrades(int.Parse(Utility.DecryptQS(OfficeId.Value)), OfReId);
         //   RoundPanelOfficeGrade.HeaderText = "مشخصات پایه";

        }
        else if (ReqManager[0]["MFType"].ToString() == "2")//شرکت اجرا
        {
            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
            lblActivityType.ClientVisible = true;
            cmbActivityType.ClientVisible = true;

            CustomAspxDevGridViewGrade.Visible = false;
            TableImp.Visible = true;
            GetOfficeImpCapacity(int.Parse(Utility.DecryptQS(OfficeId.Value)), OfReId);
          //  RoundPanelOfficeGrade.HeaderText = "مشخصات پایه-ظرفیت";
        }

        txtFileNo.Text = ReqManager[0]["MFNo"].ToString();

        txtdExpDate.Visible = true;
        txtdLastRegDate.Visible = true;
        txtdSerialNo.Visible = true;
        cmbdIsTemporary.Visible = true;

        txtdExpDate.Text = ReqManager[0]["ExpireDate"].ToString();
        txtdLastRegDate.Text = ReqManager[0]["RegDate"].ToString();
        txtdSerialNo.Text = ReqManager[0]["SerialNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
        {
            if (Convert.ToBoolean(ReqManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = 1;
            else
                cmbdIsTemporary.SelectedIndex = 0;
        }
    }

    protected void FillRequest(int OfId, int OfReId)
    {

        TSP.DataManager.AccountingDocOperationManager DocManager = new TSP.DataManager.AccountingDocOperationManager();
        ASPxTextBoxFicheCode.Text = DocManager.GetBankDocNum(OfId, (int)TSP.DataManager.AccountingTT.MembershipConfirmation);

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();

        string Tel1 = "", Tel1_Pre = "", Tel2 = "", Tel2_Pre = "", Fax = "", Fax_Pre = "";
        try
        {
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            txtOfId.Text = ReqManager[0]["OfId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MeNo"]))
                txtMeNo.Text = ReqManager[0]["MeNo"].ToString();
            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست های مربوط به پروانه باشد
            {
                DisableForReq();
                FillDocumentReq(OfReId);

            }
            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
            {
                RoundPanelFileAttachment.Visible = true;
                RoundPanelFileAttachment.Enabled = false;
                RoundPanelDocumentBasicInfo.Enabled = false;
                RoundPanelOfficeGrade.Enabled = false;
                DisableForChangeBaseInfo();
            }
            txtOfName.Text = ReqManager[0]["OfName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["OfNameEn"]))
                txtOfNameEn.Text = ReqManager[0]["OfNameEn"].ToString();

            if (ReqManager[0]["Tel1"].ToString() != "")
            {
                if (ReqManager[0]["Tel1"].ToString().IndexOf("-") > 0)
                {
                    txtOfTel1_pre.Text = ReqManager[0]["Tel1"].ToString().Substring(0, ReqManager[0]["Tel1"].ToString().IndexOf("-"));
                    txtOfTel1.Text = ReqManager[0]["Tel1"].ToString().Substring(ReqManager[0]["Tel1"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel1"].ToString().Length - ReqManager[0]["Tel1"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfTel1.Text = ReqManager[0]["Tel1"].ToString();
                }
            }
            if (ReqManager[0]["Tel2"].ToString() != "")
            {
                if (ReqManager[0]["Tel2"].ToString().IndexOf("-") > 0)
                {
                    txtOfTel2_pre.Text = ReqManager[0]["Tel2"].ToString().Substring(0, ReqManager[0]["Tel2"].ToString().IndexOf("-"));
                    txtOfTel2.Text = ReqManager[0]["Tel2"].ToString().Substring(ReqManager[0]["Tel2"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel2"].ToString().Length - ReqManager[0]["Tel2"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfTel2.Text = ReqManager[0]["Tel2"].ToString();
                }
            }
            if (ReqManager[0]["Fax"].ToString() != "")
            {
                if (ReqManager[0]["Fax"].ToString().IndexOf("-") > 0)
                {
                    txtOfFax_pre.Text = ReqManager[0]["Fax"].ToString().Substring(0, ReqManager[0]["Fax"].ToString().IndexOf("-"));
                    txtOfFax.Text = ReqManager[0]["Fax"].ToString().Substring(ReqManager[0]["Fax"].ToString().IndexOf("-") + 1, ReqManager[0]["Fax"].ToString().Length - ReqManager[0]["Fax"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtOfFax.Text = ReqManager[0]["Fax"].ToString();
                }
            }
            txtOfMobile.Text = ReqManager[0]["MobileNo"].ToString();
            txtOfEmail.Text = ReqManager[0]["Email"].ToString();
            txtOfWebsite.Text = ReqManager[0]["Website"].ToString();
            txtOfAddress.Text = ReqManager[0]["Address"].ToString();
            if ((!Utility.IsDBNullOrNullValue(ReqManager[0]["ArmUrl"])))
            {
                imgOfArm.ImageUrl = ReqManager[0]["ArmUrl"].ToString();
                HDFlpArm["name"] = 1;

            }
            else
            {
                imgOfArm.ImageUrl = "~/images/noimage.gif/";
            }

            if ((!Utility.IsDBNullOrNullValue(ReqManager[0]["SignUrl"])))
            {
                imgOfSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();
                HDFlpSign["name"] = 1;

            }
            else
            {
                imgOfSign.ImageUrl = "~/images/noimage.gif/";
            }

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ActivityType"]))
            {
                cmbActivityType.SelectedIndex = Convert.ToBoolean(ReqManager[0]["ActivityType"]) == true ? 1 : 0;
            }
            drdOfType.DataBind();
            drdOfType.Value = ReqManager[0]["OtId"].ToString();
            txtOfSubject.Text = ReqManager[0]["Subject"].ToString();
            txtOfRegDate.Text = ReqManager[0]["RegOfDate"].ToString();
            txtOfRegNo.Text = ReqManager[0]["RegOfNo"].ToString();
            txtOfRegPlace.Text = ReqManager[0]["RegOfPlace"].ToString();
            txtOfStock.Text = ReqManager[0]["Stock"].ToString();
            if (!string.IsNullOrEmpty(ReqManager[0]["VolumeInvest"].ToString()))
                txtOfValue.Text = Decimal.Parse(ReqManager[0]["VolumeInvest"].ToString()).ToString("##");

            OffManager.FindByCode(OfId);
            if (OffManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            txtOfDesc.Text = OffManager[0]["Description"].ToString();

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]))
            {
                if (OffManager[0]["Tel1"].ToString().IndexOf("-") > 0)
                {
                    Tel1_Pre = OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-"));
                    Tel1 = OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    Tel1 = OffManager[0]["Tel1"].ToString();
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]))
            {
                if (OffManager[0]["Tel2"].ToString().IndexOf("-") > 0)
                {
                    Tel2_Pre = OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-"));
                    Tel2 = OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    Tel2 = OffManager[0]["Tel2"].ToString();
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]))
            {
                if (OffManager[0]["Fax"].ToString().IndexOf("-") > 0)
                {
                    Fax_Pre = OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-"));
                    Fax = OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    Fax = OffManager[0]["Fax"].ToString();
                }
            }

            #region attach
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
            //AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfReImg"];
            for (int i = 0; i < attachManager.Count; i++)
            {
                DataRow dr = dtOfImg.NewRow();
                dr[0] = attachManager[i]["FilePath"];
                dr[1] = attachManager[i]["FilePath"].ToString();
                dr[5] = attachManager[i]["Description"].ToString();

                string fileName = Path.GetFileName(attachManager[0]["FilePath"].ToString());
                dr[2] = fileName;
                dr[3] = 1;
                dr[6] = attachManager[i][0];
                dtOfImg.Rows.Add(dr);

            }

            dtOfImg.AcceptChanges();
            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();
            #endregion
            string PageMode = Utility.DecryptQS(PgMode.Value);
            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo
                && PageMode != "NewReq")
            {
                CheckColor(OfId);
            }

            ComboDocType.DataBind();
            ComboDocType.SelectedIndex = ComboDocType.Items.IndexOfValue(ReqManager[0]["MFType"].ToString());
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void FillForm(int OfId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.AccountingDocOperationManager DocManager = new TSP.DataManager.AccountingDocOperationManager();
        ASPxTextBoxFicheCode.Text = DocManager.GetBankDocNum(OfId, (int)TSP.DataManager.AccountingTT.MembershipConfirmation);

        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        txtOfId.Text = OfId.ToString();
        if (!Utility.IsDBNullOrNullValue(OffManager[0]["MeNo"]))
            txtMeNo.Text = OffManager[0]["MeNo"].ToString();
        txtOfName.Text = OffManager[0]["OfName"].ToString();
        if (!Utility.IsDBNullOrNullValue(OffManager[0]["OfNameEn"]))
            txtOfNameEn.Text = OffManager[0]["OfNameEn"].ToString();
        drdOfType.Value = OffManager[0]["OtId"].ToString();

        if (OffManager[0]["Tel1"].ToString() != "")
        {
            if (OffManager[0]["Tel1"].ToString().IndexOf("-") > 0)
            {
                txtOfTel1_pre.Text = OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-"));
                txtOfTel1.Text = OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfTel1.Text = OffManager[0]["Tel1"].ToString();
            }
        }
        if (OffManager[0]["Tel2"].ToString() != "")
        {
            if (OffManager[0]["Tel2"].ToString().IndexOf("-") > 0)
            {
                txtOfTel2_pre.Text = OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-"));
                txtOfTel2.Text = OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfTel2.Text = OffManager[0]["Tel2"].ToString();
            }
        }
        if (OffManager[0]["Fax"].ToString() != "")
        {
            if (OffManager[0]["Fax"].ToString().IndexOf("-") > 0)
            {
                txtOfFax_pre.Text = OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-"));
                txtOfFax.Text = OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1);
            }
            else
            {
                txtOfFax.Text = OffManager[0]["Fax"].ToString();
            }
        }

        if (!Utility.IsDBNullOrNullValue(OffManager[0]["ActivityType"]))
        {
            cmbActivityType.SelectedIndex = Convert.ToBoolean(OffManager[0]["ActivityType"]) == true ? 1 : 0;
        }

        txtOfMobile.Text = OffManager[0]["MobileNo"].ToString();
        txtOfEmail.Text = OffManager[0]["Email"].ToString();
        txtOfWebsite.Text = OffManager[0]["Website"].ToString();
        txtOfAddress.Text = OffManager[0]["Address"].ToString();
        txtOfSubject.Text = OffManager[0]["Subject"].ToString();
        txtOfRegDate.Text = OffManager[0]["RegDate"].ToString();
        txtOfRegNo.Text = OffManager[0]["RegOfNo"].ToString();
        txtOfRegPlace.Text = OffManager[0]["RegPlace"].ToString();
        txtOfStock.Text = OffManager[0]["Stock"].ToString();
        if (!string.IsNullOrEmpty(OffManager[0]["VolumeInvest"].ToString()))
            txtOfValue.Text = Decimal.Parse(OffManager[0]["VolumeInvest"].ToString()).ToString("##");
        txtOfDesc.Text = OffManager[0]["Description"].ToString();

        if ((!Utility.IsDBNullOrNullValue(OffManager[0]["ArmUrl"])))
        {
            imgOfArm.ImageUrl = OffManager[0]["ArmUrl"].ToString();
            HDFlpArm["name"] = 1;

        }
        else
        {
            imgOfArm.ImageUrl = "~/images/noimage.gif/";
        }

        if ((!Utility.IsDBNullOrNullValue(OffManager[0]["SignUrl"])))
        {
            imgOfSign.ImageUrl = OffManager[0]["SignUrl"].ToString();
            HDFlpSign["name"] = 1;

        }
        else
        {
            imgOfSign.ImageUrl = "~/images/noimage.gif/";
        }
    }
    #endregion

    #region Insert-Update
    protected void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.MemberStatusChangeManager ChManager = new TSP.DataManager.MemberStatusChangeManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        TSP.DataManager.AccountingProjectManager ProjectManager = new TSP.DataManager.AccountingProjectManager();
        TSP.DataManager.AccountingCostCenterManager CostCenterManager = new TSP.DataManager.AccountingCostCenterManager();
        ProjectManager.Fill();
        CostCenterManager.Fill();
        TSP.DataManager.AccountingDocument Document2 = new TSP.DataManager.AccountingDocument(trans, Utility.GetCurrentUser_AgentId(), Convert.ToInt32(ProjectManager[0]["PrjId"]), Convert.ToInt32(CostCenterManager[0]["CCId"]));
        TSP.DataManager.AccountingAccount Account = new TSP.DataManager.AccountingAccount(trans, Utility.GetCurrentUser_AgentId());
        TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();

        trans.Add(OffManager);
        trans.Add(LogManager);
        trans.Add(ReqManager);
        trans.Add(ChManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(SettingsManager);
        trans.Add(CostSettingsManager);

        string pagemode = Utility.DecryptQS(PgMode.Value);

        string pathAx = "", Password = "";

        try
        {
            int AccId = -1, ParentAccId = -1, MembershipEarningsAccId = -1, MainBankAccId = -1;
            string Des2 = "";
            decimal Amount = 0;

            #region Create Accounting
            if (Utility.CreateAccount())
            {
                ParentAccId = GetParentAccId(SettingsManager);
                if (ParentAccId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفا حساب جاری اعضا را در قسمت تنظیم حسابها انتخاب نمایید";
                    return;
                }

                MembershipEarningsAccId = GetMembershipEarningsAccId(SettingsManager);
                if (MembershipEarningsAccId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفا حساب درآمد عضویت را در قسمت تنظیم حسابها انتخاب نمایید";
                    return;
                }

                MainBankAccId = GetMainBankAccId(SettingsManager);
                if (MainBankAccId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفا حساب بانک اصلی را در قسمت تنظیم حسابها انتخاب نمایید";
                    return;
                }

                Amount = GetFirstMembershipCost(CostSettingsManager);
                if (Amount == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفا ورودیه عضویت را در قسمت تنظیم هزینه های دریافتی وارد نمایید";
                    return;
                }
            }
            #endregion

            string PerDate = Utility.GetDateOfToday();
            trans.BeginSave();
            DataRow drOffice = OffManager.NewRow();
            drOffice["OfId"] = 0;
            drOffice["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drOffice["OfNameEn"] = txtOfNameEn.Text;
            drOffice["PrefixCode"] = DBNull.Value;
            if (drdOfType.Value != null)
                drOffice["OtId"] = int.Parse(drdOfType.Value.ToString());
            drOffice["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            drOffice["OatId"] = DBNull.Value;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drOffice["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drOffice["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drOffice["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drOffice["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drOffice["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drOffice["Fax"] = txtOfFax.Text;
            drOffice["MobileNo"] = txtOfMobile.Text;
            drOffice["Email"] = txtOfEmail.Text;
            drOffice["Website"] = txtOfWebsite.Text;
            drOffice["Address"] = txtOfAddress.Text;
            drOffice["Subject"] = txtOfSubject.Text;
            drOffice["RegDate"] = txtOfRegDate.Text;
            drOffice["RegOfNo"] = txtOfRegNo.Text;
            drOffice["RegPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drOffice["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drOffice["VolumeInvest"] = txtOfValue.Text;
            drOffice["MeNo"] = DBNull.Value;
            drOffice["FileNo"] = DBNull.Value;
            drOffice["MrsId"] = 2;

            if (Session["FileOfArm2"] != null)
            {
                pathAx = Server.MapPath("~/Image/Temp/");
                imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                drOffice["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }
            //else
            //{
            //this.DivReport.Visible = true;
            //this.LabelWarning.Text = "تصویر آرم شرکت را انتخاب نمایید";
            //return;
            //}

            if (Session["FileOfSign2"] != null)
            {
                pathAx = Server.MapPath("~/Image/Temp/");
                imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                drOffice["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
            }
            //else
            //{
            //this.DivReport.Visible = true;
            //this.LabelWarning.Text = "تصویر مهر شرکت را انتخاب نمایید";
            //return;
            //}
            drOffice["IsLock"] = 0;
            drOffice["CreateDate"] = PerDate;
            drOffice["Description"] = txtOfDesc.Text;
            drOffice["UserId"] = Utility.GetCurrentUser_UserId();
            drOffice["ModifiedDate"] = DateTime.Now;
            OffManager.AddRow(drOffice);
            if (OffManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            OffManager.DataTable.AcceptChanges();
            int OfId = Convert.ToInt32(OffManager[0]["OfId"]);
            OfficeId.Value = Utility.EncryptQS(OffManager[0]["OfId"].ToString());

            #region Login
            Password = InsertLogin(LogManager, OfId, OffManager[0]["RegOfNo"].ToString(), OffManager[0]["Email"].ToString());
            #endregion

            #region Request
            InsertRequest(ReqManager, OfId, OffManager[0]["ArmUrl"].ToString(), OffManager[0]["SignUrl"].ToString());
            OfficeRequest.Value = Utility.EncryptQS(ReqManager[0]["OfReId"].ToString());

            #endregion

            #region Document
            if (Utility.CreateAccount())
            {
                AccId = Account.InsertAccount(ParentAccId, GetAccCode(), GetAccName(OffManager[0]), Utility.GetCurrentUser_UserId());

                Des2 = GetDes2(OffManager[0]);

                OffManager[0]["AccId"] = AccId;
            }
            else
            {
                OffManager[0]["AccId"] = DBNull.Value;
            }
            OffManager.Save();

            if (Utility.CreateAccount())
            {
                Document2.Insert(MainBankAccId, AccId, Amount, Des2, OfId, ASPxTextBoxFicheCode.Text, TSP.DataManager.AccountingTT.MembershipConfirmation, Utility.GetCurrentUser_UserId());
            }

            #endregion


            #region StartWorkFlow
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            int CurrentNmcId = Utility.GetCurrentUser_MeId();
            int CurrentNmcIdType = Utility.GetCurrentUser_NmcIdType();
            if (Utility.IsDBNullOrNullValue(CurrentNmcId))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            WorkFlowStateManager.StartWorkFlow(Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value)), TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), CurrentNmcIdType);
            #endregion
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                }
            }
            #region StatusChangeComment

            //DataRow drCh = ChManager.NewRow();
            ////drCh.BeginEdit();
            //drCh["MeId"] = OffManager[0]["OfId"];
            //drCh["MsId"] = 2;//در جریان
            //drCh["Date"] = PerDate;
            //drCh["Type"] = 1;

            //drCh["UserId"] = Session["Login"];
            //drCh["ModifiedDate"] = DateTime.Now;

            //ChManager.AddRow(drCh);
            //ChManager.Save();

            //#endregion

            //#region StartWorkFlow
            //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            //TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            //int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LogManager);//FindNmcId();
            //WorkFlowStateManager.StartWorkFlow(MeId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0);
            #endregion

            trans.EndSave();
            TSP.DataManager.OfficeManager.UpdateMeNo(OfId);
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "  ذخیره با نام کاربری " + "com" + OffManager[0]["OfId"].ToString() + "و رمز عبور " + Password + " انجام شد";
            txtOfId.Text = OfId.ToString();
            OffManager.FindByCode(OfId);
            if (OffManager.Count == 1 && !Utility.IsDBNullOrNullValue(OffManager[0]["MeNo"]))
                txtMeNo.Text = OffManager[0]["MeNo"].ToString();

            PgMode.Value = Utility.EncryptQS("Edit");
            RoundPanelOffice.HeaderText = "ویرایش";
            MenuDetails.Enabled = true;

            if ((!Utility.IsDBNullOrNullValue(OffManager[0]["ArmUrl"])))
            {
                imgOfArm.ClientVisible = true;
                imgOfArm.ImageUrl = OffManager[0]["ArmUrl"].ToString();
            }

            if ((!Utility.IsDBNullOrNullValue(OffManager[0]["SignUrl"])))
            {
                imgOfSign.ClientVisible = true;
                imgOfSign.ImageUrl = OffManager[0]["SignUrl"].ToString();
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
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                Session["FileOfSign2"] = null;

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                Session["FileOfArm2"] = null;


            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected string InsertLogin(TSP.DataManager.LoginManager LogManager, int OfId, string RegOfNo, string Email)
    {
        String Password = Utility.GeneratePassword();
        DataRow logRow = LogManager.NewRow();
        logRow.BeginEdit();
        logRow["UserName"] = "com" + OfId.ToString();
        if (!string.IsNullOrEmpty(RegOfNo))
            logRow["Password"] = Utility.EncryptPassword(Password);
        logRow["UltId"] = 2;
        logRow["MeId"] = OfId;
        logRow["Email"] = Email;
        logRow["IsValid"] = 1;
        logRow["UserId2"] = Utility.GetCurrentUser_UserId();
        logRow["ModifiedDate"] = DateTime.Now;
        logRow.EndEdit();
        LogManager.AddRow(logRow);
        LogManager.Save();
        return Password;
    }

    protected void InsertRequest(TSP.DataManager.OfficeRequestManager ReqManager, int OfId, string ArmUrl, string SignUrl)
    {
        DataRow drReq = ReqManager.NewRow();
        drReq["OfId"] = OfId;
        drReq["OfName"] = txtOfName.Text;
        if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
            drReq["OfNameEn"] = txtOfNameEn.Text;
        if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
            drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
        else if (txtOfTel1.Text != "")
            drReq["Tel1"] = txtOfTel1.Text;
        if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
            drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
        else if (txtOfTel2.Text != "")
            drReq["Tel2"] = txtOfTel2.Text;
        if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
            drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
        else if (txtOfFax.Text != "")
            drReq["Fax"] = txtOfFax.Text;
        drReq["MobileNo"] = txtOfMobile.Text;
        drReq["Email"] = txtOfEmail.Text;
        drReq["Website"] = txtOfWebsite.Text;
        drReq["Address"] = txtOfAddress.Text;
        if (drdOfType.Value != null)
            drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
        drReq["Subject"] = txtOfSubject.Text;
        drReq["RegOfDate"] = txtOfRegDate.Text;
        drReq["RegOfNo"] = txtOfRegNo.Text;
        drReq["RegOfPlace"] = txtOfRegPlace.Text;
        if (txtOfStock.Text != "")
            drReq["Stock"] = int.Parse(txtOfStock.Text);
        if (txtOfValue.Text != "")
            drReq["VolumeInvest"] = txtOfValue.Text;

        drReq["CreateDate"] = Utility.GetDateOfToday();
        drReq["Type"] = (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo;//درخواست ثبت نام اولیه
        drReq["Requester"] = 0;
        drReq["UserId"] = Utility.GetCurrentUser_UserId();
        drReq["ModifiedDate"] = DateTime.Now;
        if (!string.IsNullOrEmpty(ArmUrl))
            drReq["ArmUrl"] = ArmUrl;

        if (!string.IsNullOrEmpty(SignUrl))
            drReq["SignUrl"] = SignUrl;

        drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);
        drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;

        ReqManager.AddRow(drReq);
        ReqManager.Save();
    }

    protected void Edit(int OfId, int OfReId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        // TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(ReqManager);
        trans.Add(attachManager);

        string pathAx = "";

        bool chArmEdit = false;
        bool chSignEdit = false;

        try
        {
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count == 1)
            {
                ReqManager[0].BeginEdit();
                ReqManager[0]["OfName"] = txtOfName.Text;
                if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                    ReqManager[0]["OfNameEn"] = txtOfNameEn.Text;
                if (drdOfType.Value != null)
                    ReqManager[0]["OtId"] = int.Parse(drdOfType.Value.ToString());

                if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                    ReqManager[0]["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
                else if (txtOfTel1.Text != "")
                    ReqManager[0]["Tel1"] = txtOfTel1.Text;
                if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                    ReqManager[0]["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
                else if (txtOfTel2.Text != "")
                    ReqManager[0]["Tel2"] = txtOfTel2.Text;
                if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                    ReqManager[0]["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
                else if (txtOfFax.Text != "")
                    ReqManager[0]["Fax"] = txtOfFax.Text;
                ReqManager[0]["MobileNo"] = txtOfMobile.Text;
                ReqManager[0]["Email"] = txtOfEmail.Text;
                ReqManager[0]["Website"] = txtOfWebsite.Text;
                ReqManager[0]["Address"] = txtOfAddress.Text;
                ReqManager[0]["Subject"] = txtOfSubject.Text;
                ReqManager[0]["RegDate"] = txtOfRegDate.Text;
                ReqManager[0]["RegOfNo"] = txtOfRegNo.Text;
                ReqManager[0]["RegOfPlace"] = txtOfRegPlace.Text;
                if (txtOfStock.Text != "")
                    ReqManager[0]["Stock"] = int.Parse(txtOfStock.Text);
                if (txtOfValue.Text != "")
                    ReqManager[0]["VolumeInvest"] = txtOfValue.Text;

                #region editArmImage
                if (Session["FileOfArm2"] != null)
                {
                    if ((!string.IsNullOrEmpty(ReqManager[0]["ArmUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()));
                            pathAx = Server.MapPath("~/Image/Temp/");
                            imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                            ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                            chArmEdit = true;

                        }
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }

                    }
                    else
                    {
                        try
                        {
                            pathAx = Server.MapPath("~/Image/Temp/");
                            imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());
                            ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                            chArmEdit = true;


                        }
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        }
                    }
                }
                #endregion
                #region editSignImage
                if (Session["FileOfSign2"] != null)
                {
                    if ((!string.IsNullOrEmpty(ReqManager[0]["SignUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["SignUrl"].ToString()))))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(ReqManager[0]["SignUrl"].ToString()));
                            pathAx = Server.MapPath("~/Image/Temp/");
                            imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                            ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                            chSignEdit = true;



                        }
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                            return;
                        }

                    }
                    else
                    {
                        try
                        {

                            pathAx = Server.MapPath("~/Image/Temp/");
                            imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());
                            ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                            chSignEdit = true;


                        }
                        catch
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                            return;
                        }
                    }
                }
                #endregion
                ReqManager[0]["RequestDesc"] = txtOfDesc.Text;
                ReqManager[0]["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
                ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ReqManager[0]["ModifiedDate"] = DateTime.Now;
                ReqManager[0].EndEdit();
                trans.BeginSave();
                ReqManager.Save();

                dtOfImg = (DataTable)Session["TblOfReImg"];

                if (dtOfImg.GetChanges() != null)
                {
                    DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                    if (insRows.Length > 0)
                    {
                        for (int i = 0; i < insRows.Length; i++)
                        {
                            DataRow drImg = attachManager.NewRow();
                            drImg["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                            drImg["RefTable"] = OfReId;
                            drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                            drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                            drImg["IsValid"] = 1;
                            drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                            drImg["UserId"] = Utility.GetCurrentUser_UserId();
                            drImg["ModfiedDate"] = DateTime.Now;
                            attachManager.AddRow(drImg);
                            int imgcnt = attachManager.Save();
                            attachManager.DataTable.AcceptChanges();
                            if (imgcnt == 1)
                            {
                                dtOfImg.Rows[i].BeginEdit();
                                dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                                dtOfImg.Rows[i].EndEdit();

                                if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                                {
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                    string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                    File.Copy(ImgSoource, ImgTarget, true);

                                }

                            }
                        }

                    }

                }
                trans.EndSave();
                PgMode.Value = Utility.EncryptQS("Edit");
                RoundPanelOffice.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)
                    CheckColor(OfId);

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخواانی اطلاعات رخ داده است";
                return;
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
        if (Session["FileOfArm2"] != null)
        {
            if (chArmEdit == true)
            {
                try
                {
                    string ArmSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    System.IO.File.Copy(ArmSoource, ArmTarget, true);
                    imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                    Session["FileOfArm2"] = null;

                }
                catch (Exception)
                {
                }
            }

        }
        if (Session["FileOfSign2"] != null)
        {
            if (chSignEdit == true)
            {
                try
                {
                    string SignSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    System.IO.File.Copy(SignSoource, SignTarget, true);
                    imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                    Session["FileOfSign2"] = null;
                }
                catch (Exception)
                {
                }

            }

        }
    }

    protected void EditRequest(int OfReId, int ReqType)
    {
        if (IsPageRefresh)
            return;


        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        trans.Add(ReqManager);
        trans.Add(attachManager);

        string pathAx = "";
        byte[] img = null;
        bool chArmEdit = false;
        bool chSignEdit = false;

        int PrId = Utility.GetCurrentProvinceId();
        ProvinceManager.FindByCode(PrId);
        string PrCode = "";
        string MFCode = "";
        string MFMjCode = "0000000";

        if (ProvinceManager.Count > 0)
        {
            PrCode = ProvinceManager[0]["NezamCode"].ToString();
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
            return;
        }


        try
        {
            ReqManager.FindByCode(OfReId);
            ReqManager[0].BeginEdit();
            int OfId = Convert.ToInt32(ReqManager[0]["OfId"]);
            ReqManager[0]["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                ReqManager[0]["OfNameEn"] = txtOfNameEn.Text;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                ReqManager[0]["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                ReqManager[0]["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                ReqManager[0]["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                ReqManager[0]["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                ReqManager[0]["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                ReqManager[0]["Fax"] = txtOfFax.Text;
            ReqManager[0]["MobileNo"] = txtOfMobile.Text;
            ReqManager[0]["Email"] = txtOfEmail.Text;
            ReqManager[0]["Website"] = txtOfWebsite.Text;
            ReqManager[0]["Address"] = txtOfAddress.Text;
            if (drdOfType.Value != null)
                ReqManager[0]["OtId"] = int.Parse(drdOfType.Value.ToString());
            ReqManager[0]["Subject"] = txtOfSubject.Text;
            ReqManager[0]["RegOfDate"] = txtOfRegDate.Text;
            ReqManager[0]["RegOfNo"] = txtOfRegNo.Text;
            ReqManager[0]["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                ReqManager[0]["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                ReqManager[0]["VolumeInvest"] = txtOfValue.Text;

            #region editArmImage
            if (Session["FileOfArm2"] != null)
            {
                if ((!string.IsNullOrEmpty(ReqManager[0]["ArmUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()))))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(ReqManager[0]["ArmUrl"].ToString()));

                        //img = flpOfArm.FileBytes;
                        // fileNameArm = Utility.GenerateName(Path.GetExtension(flpOfArm.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        // flpOfArm.SaveAs(pathAx + fileNameArm);
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());

                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;

                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                    }

                }
                else
                {
                    try
                    {


                        //img = flpOfArm.FileBytes;
                        // fileNameArm = Utility.GenerateName(Path.GetExtension(flpOfArm.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        // flpOfArm.SaveAs(pathAx + fileNameArm);
                        imgOfArm.ImageUrl = pathAx + Path.GetFileName(Session["FileOfArm2"].ToString());

                        ReqManager[0]["ArmImage"] = img;
                        ReqManager[0]["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                        chArmEdit = true;


                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                    }
                }
            }
            #endregion
            #region editSignImage
            if (Session["FileOfSign2"] != null)
            {
                if ((!string.IsNullOrEmpty(ReqManager[0]["SignUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(ReqManager[0]["SignUrl"].ToString()))))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(ReqManager[0]["SignUrl"].ToString()));

                        /// img = flpOfSign.FileBytes;
                        //fileNameSign = Utility.GenerateName(Path.GetExtension(flpOfSign.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        //  flpOfSign.SaveAs(pathAx + fileNameSign);
                        imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());

                        ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                        chSignEdit = true;



                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        return;
                    }

                }
                else
                {
                    try
                    {

                        //img = flpOfSign.FileBytes;
                        //fileNameSign = Utility.GenerateName(Path.GetExtension(flpOfSign.FileName));
                        pathAx = Server.MapPath("~/Image/Temp/");
                        //flpOfSign.SaveAs(pathAx + fileNameSign);
                        imgOfSign.ImageUrl = pathAx + Path.GetFileName(Session["FileOfSign2"].ToString());

                        ReqManager[0]["SignImage"] = img;
                        ReqManager[0]["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                        chSignEdit = true;


                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش فایل نمی باشد.";
                        return;
                    }
                }
            }
            #endregion
            ReqManager[0]["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            if (ComboDocType.Value != null)
                ReqManager[0]["MFType"] = ComboDocType.Value;

            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
            {
                if (ComboDocType.Value != null)
                {
                    if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                        MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                    else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
                        MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                    if (!CheckMembersByOfficeType(OfId, Convert.ToInt32(ComboDocType.Value)))
                        return;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "نوع پروانه را انتخاب نمایید";
                    return;
                }
            }

            #region SetMFNo
            TSP.DataManager.OfficeMemberManager OffMemManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            DataTable dtOfMe = OffMemManager.SelectOfficeMember(OfId, 1, -1);//return membe
            if (dtOfMe.Rows.Count > 0)
            {
                for (int m = 0; m < dtOfMe.Rows.Count; m++)
                {
                    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
                    if (dtMj.Rows.Count > 0)
                    {
                        int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                        int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                        int i = -1;
                        char[] Code = MFMjCode.ToCharArray();

                        switch (ParentId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = ParentId.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;

                        }
                        if (i != -1)
                        {
                            //MFMjCode = Code.ToString();
                            MFMjCode = new string(Code);
                        }
                    }
                    dtMj.Clear();
                }
            }



            #endregion

            string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
            while (MFSerialNo.Length < 5)
            {
                MFSerialNo = "0" + MFSerialNo;
            }
            ReqManager[0]["MFNo"] = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;

            ReqManager[0]["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                ReqManager[0]["SerialNo"] = txtdSerialNo.Text;
            ReqManager[0]["ExpireDate"] = txtdExpDate.Text;
            if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                ReqManager[0]["IsTemp"] = 0;
            else
                ReqManager[0]["IsTemp"] = 1;

            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ReqManager[0].EndEdit();
            trans.BeginSave();
            if (ReqManager.Save() == 1)
            {
                dtOfImg = (DataTable)Session["TblOfReImg"];

                if (dtOfImg.GetChanges() != null)
                {
                    DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                    if (insRows.Length > 0)
                    {
                        for (int i = 0; i < insRows.Length; i++)
                        {
                            DataRow drImg = attachManager.NewRow();
                            drImg["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                            drImg["RefTable"] = OfReId;
                            drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                            drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                            drImg["IsValid"] = 1;
                            drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                            drImg["UserId"] = Utility.GetCurrentUser_UserId();
                            drImg["ModfiedDate"] = DateTime.Now;
                            attachManager.AddRow(drImg);
                            int imgcnt = attachManager.Save();
                            attachManager.DataTable.AcceptChanges();
                            if (imgcnt == 1)
                            {
                                dtOfImg.Rows[i].BeginEdit();
                                dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                                dtOfImg.Rows[i].EndEdit();
                                if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                                {
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                    string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                    File.Copy(ImgSoource, ImgTarget, true);
                                }
                            }
                        }
                    }
                }
                txtFileNo.Text = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
                PgMode.Value = Utility.EncryptQS("Edit");
                RoundPanelOffice.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                trans.EndSave();
                CheckColor(OfId);


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
        if (Session["FileOfArm2"] != null)
        {
            if (chArmEdit == true)
            {
                try
                {
                    string ArmSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                    System.IO.File.Copy(ArmSoource, ArmTarget, true);
                    imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                    Session["FileOfArm2"] = null;

                }
                catch (Exception)
                {
                }
            }

        }
        if (Session["FileOfSign2"] != null)
        {
            if (chSignEdit == true)
            {
                try
                {
                    string SignSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                    System.IO.File.Copy(SignSoource, SignTarget, true);
                    imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                    Session["FileOfSign2"] = null;
                }
                catch (Exception)
                {
                }

            }

        }
    }
    //******************************
    protected void InsertInActtiveOfficeMembers(TSP.DataManager.OfficeMemberManager OffMemberManager, TSP.DataManager.DocMemberFileManager DocMemberFileManager, TSP.DataManager.RequestInActivesManager ReqInActiveManager, int OfId, int OfReId)
    {
        int MemberFileId = -1;
        int MeId = -1;
        int OfmMfId = -1;
        OffMemberManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Member, 0, 1);
        if (OffMemberManager.Count > 0)
        {
            for (int i = 0; i < OffMemberManager.Count; i++)
            {
                MeId = Convert.ToInt32(OffMemberManager[i]["PersonId"]);
                OfmMfId = Convert.ToInt32(OffMemberManager[i]["MfId"]);

                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                    if (OfmMfId != MemberFileId)
                    {
                        DataRow drOfm = OffMemberManager.NewRow();
                        drOfm["OfReId"] = OfReId;
                        drOfm["MfId"] = MemberFileId;
                        drOfm["OfmType"] = (int)TSP.DataManager.OfficeMemberType.Member;
                        drOfm["PersonId"] = MeId; ;
                        drOfm["SignUrl"] = OffMemberManager[i]["SignUrl"];
                        drOfm["OfId"] = OfId;
                        drOfm["OfpId"] = OffMemberManager[i]["OfpId"];
                        drOfm["StartDate"] = OffMemberManager[i]["StartDate"];
                        drOfm["HasSignRight"] = OffMemberManager[i]["HasSignRight"];
                        drOfm["IsFullTime"] = OffMemberManager[i]["IsFullTime"];
                        drOfm["Description"] = OffMemberManager[i]["Description"];
                        drOfm["UserId"] = Utility.GetCurrentUser_UserId();
                        drOfm["ModifiedDate"] = DateTime.Now;
                        OffMemberManager.AddRow(drOfm);

                        DataRow dr = ReqInActiveManager.NewRow();
                        dr["TableId"] = OffMemberManager[i]["OfmId"];
                        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
                        dr["ReqId"] = OfReId;
                        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                        dr["InActive"] = 1;
                        dr["SysInActive"] = 1;
                        dr["CreateDate"] = Utility.GetDateOfToday();
                        dr["UserId"] = Utility.GetCurrentUser_UserId();
                        dr["ModifiedDate"] = DateTime.Now;
                        ReqInActiveManager.AddRow(dr);

                    }
                }
            }
            OffMemberManager.Save();
            ReqInActiveManager.Save();
        }

    }

    protected void InsertMembershipRequest(int OfId, int OfficeRequestType)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        trans.Add(LetterRelatedTablesManager);


        try
        {
            ReqManager.FindByOfficeId(OfId, 1, -1);
            if (ReqManager.Count == 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            DataRow drReq = ReqManager.NewRow();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["PrId"]))
                drReq["PrId"] = ReqManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegDate"]))
                drReq["RegDate"] = ReqManager[0]["RegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ExpireDate"]))
                drReq["ExpireDate"] = ReqManager[0]["ExpireDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegPlaceId"]))
                drReq["RegPlaceId"] = ReqManager[0]["RegPlaceId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
                drReq["MFNo"] = ReqManager[0]["MFNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                drReq["MFType"] = ReqManager[0]["MFType"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
                drReq["IsTemp"] = ReqManager[0]["IsTemp"].ToString();

            drReq["OfId"] = OfId;
            drReq["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drReq["OfNameEn"] = txtOfNameEn.Text;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax.Text;
            drReq["MobileNo"] = txtOfMobile.Text;
            drReq["Email"] = txtOfEmail.Text;
            drReq["Website"] = txtOfWebsite.Text;
            drReq["Address"] = txtOfAddress.Text;
            if (drdOfType.Value != null)
                drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
            drReq["Subject"] = txtOfSubject.Text;
            drReq["RegOfDate"] = txtOfRegDate.Text;
            drReq["RegOfNo"] = txtOfRegNo.Text;
            drReq["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drReq["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drReq["VolumeInvest"] = txtOfValue.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Type"] = OfficeRequestType;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;

            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

            drReq["Requester"] = 0;//شرکت

            if (Session["FileOfArm2"] != null)
            {
                imgOfArm.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                drReq["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }
            else
                drReq["ArmUrl"] = imgOfArm.ImageUrl;

            if (Session["FileOfSign2"] != null)
            {
                imgOfSign.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                drReq["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());

            }
            else
                drReq["SignUrl"] = imgOfSign.ImageUrl;

            drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;

            ReqManager.AddRow(drReq);
            trans.BeginSave();
            if (ReqManager.Save() > 0)
            {
                ReqManager.DataTable.AcceptChanges();

                OfficeRequest.Value = Utility.EncryptQS(ReqManager[ReqManager.Count - 1]["OfReId"].ToString());

                dtOfImg = (DataTable)Session["TblOfReImg"];

                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                        drImg["RefTable"] = ReqManager[ReqManager.Count - 1]["OfReId"];
                        drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                        drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        attachManager.AddRow(drImg);
                        int imgcnt = attachManager.Save();
                        attachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImg.Rows[i].BeginEdit();
                            dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                            dtOfImg.Rows[i].EndEdit();

                            if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                            {
                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);

                            }

                        }
                    }
                }
                int TableId = int.Parse(ReqManager[ReqManager.Count - 1]["OfReId"].ToString());
                int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
                WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                if (WorkFlowTaskManager.Count != 1)
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                int CurrentNmcId = Utility.GetCurrentUser_MeId();
                int CurrentNmcIdType = Utility.GetCurrentUser_NmcIdType();
                if (Utility.IsDBNullOrNullValue(CurrentNmcId))
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), CurrentNmcIdType);
                if (WfStart > 0)
                {
                    InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);
                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"]))
                        {
                            lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
                        }
                    }

                    trans.EndSave();


                    MenuDetails.Enabled = true;
                    PgMode.Value = Utility.EncryptQS("Edit");
                    RoundPanelOffice.HeaderText = "ویرایش";

                    DisableForReq();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    CheckColor(OfId);

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
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                //imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Session["FileOfSign2"].ToString();

            }
            catch (Exception)
            {
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                //imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Session["FileOfArm2"].ToString();

            }
            catch (Exception)
            {
            }
        }
    }

    protected void InsertReqDocument(int OfId)
    {
        if (IsPageRefresh)
            return;

        #region Define Manager
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        trans.Add(LetterRelatedTablesManager);
        #endregion
        try
        {


            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = "";
            //string MFMjCode = "0000000";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return;
            }

            #region Insert New Request
            DataRow drReq = ReqManager.NewRow();
            drReq["OfId"] = OfId;
            drReq["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drReq["OfNameEn"] = txtOfNameEn.Text;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax.Text;
            drReq["MobileNo"] = txtOfMobile.Text;
            drReq["Email"] = txtOfEmail.Text;
            drReq["Website"] = txtOfWebsite.Text;
            drReq["Address"] = txtOfAddress.Text;
            if (drdOfType.Value != null)
                drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
            drReq["Subject"] = txtOfSubject.Text;
            drReq["RegOfDate"] = txtOfRegDate.Text;
            drReq["RegOfNo"] = txtOfRegNo.Text;
            drReq["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drReq["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drReq["VolumeInvest"] = txtOfValue.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Type"] = (int)TSP.DataManager.OfficeRequestType.SaveFileDocument;//درخواست صدور پروانه
            drReq["UserId"] = Utility.GetCurrentUser_UserId();

            drReq["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                drReq["SerialNo"] = txtdSerialNo.Text;
            drReq["ExpireDate"] = txtdExpDate.Text;
            if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                drReq["IsTemp"] = 0;
            else
                drReq["IsTemp"] = 1;

            drReq["ModifiedDate"] = DateTime.Now;
            drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            if (ComboDocType.Value != null)
            {
                drReq["MFType"] = ComboDocType.Value;
                if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                    MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
                    MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                if (!CheckMembersByOfficeType(OfId, Convert.ToInt32(ComboDocType.Value)))
                    return;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع پروانه را انتخاب نمایید";
                return;
            }

            drReq["RegPlaceId"] = Utility.GetCurrentProvinceId();//استان فارس
            drReq["PrId"] = Utility.GetCurrentProvinceId();//استان فارس

            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

            drReq["Requester"] = 0;//شرکت

            if (Session["FileOfArm2"] != null)
            {
                imgOfArm.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                drReq["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }
            else
                drReq["ArmUrl"] = imgOfArm.ImageUrl;

            if (Session["FileOfSign2"] != null)
            {
                imgOfSign.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                drReq["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());

            }
            else
                drReq["SignUrl"] = imgOfSign.ImageUrl;

            ReqManager.AddRow(drReq);
            trans.BeginSave();
            if (ReqManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            ReqManager.DataTable.AcceptChanges();
            #endregion

            OfficeRequest.Value = Utility.EncryptQS(ReqManager[0]["OfReId"].ToString());

            string MFNo = SetOfficeMfNo(OfId, int.Parse(ReqManager[0]["OfReId"].ToString()), PrCode, MFCode, ReqManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }
            #region SaveImg
            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                    drImg["RefTable"] = ReqManager[0]["OfReId"];
                    drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                    drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                    drImg["IsValid"] = 1;
                    drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                    drImg["UserId"] = Utility.GetCurrentUser_UserId();
                    drImg["ModfiedDate"] = DateTime.Now;
                    attachManager.AddRow(drImg);
                    int imgcnt = attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                    if (imgcnt == 1)
                    {
                        dtOfImg.Rows[i].BeginEdit();
                        dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                        dtOfImg.Rows[i].EndEdit();

                        if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                        {
                            string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);
                        }
                    }
                }
            }
            #endregion

            #region Insert WF
            int TableId = int.Parse(ReqManager[0]["OfReId"].ToString());
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            int CurrentNmcId = Utility.GetCurrentUser_MeId();
            int CurrentNmcIdType = Utility.GetCurrentUser_NmcIdType();
            if (Utility.IsDBNullOrNullValue(CurrentNmcId))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), CurrentNmcIdType);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion
            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"]))
                {
                    lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
                }
            }

            #region Do Next Task Of Insert


            MenuDetails.Enabled = true;
            txtFileNo.Text = MFNo;// MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
            PgMode.Value = Utility.EncryptQS("Edit");
            RoundPanelOffice.HeaderText = "ویرایش";
            RoundPanelFileAttachment.Visible = true;
            DisableForReq();
            trans.EndSave();
            ShowMessage("ذخیره انجام شد");
            CheckColor(OfId);
            #endregion
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
        #region Move Images
        if (Session["FileOfSign2"] != null)
        {
            try
            {
                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                //imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Session["FileOfSign2"].ToString();

            }
            catch (Exception)
            {
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                //imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Session["FileOfArm2"].ToString();

            }
            catch (Exception)
            {
            }
        }
        #endregion
    }

    private void InsertNewRequest(int OfReId, int OfId, TSP.DataManager.OfficeRequestType OfficeRequestType)
    {
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeRequestManager ReqManager2 = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.RequestInActivesManager ReqInActiveManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();

        ReqManager.ClearBeforeFill = true;
        trans.Add(WorkFlowStateManager);
        trans.Add(ReqManager);
        trans.Add(attachManager);
        trans.Add(ReqManager2);
        trans.Add(OffMemberManager);
        trans.Add(DocMemberFileManager);
        trans.Add(ReqInActiveManager);
        #endregion
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = "";
            string MFNo = "";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return;
            }


            ReqManager2.FindByOfficeId(OfId, 0, -1);
            if (ReqManager2.Count > 0)
            {
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }

            ReqManager2.FindByOfficeId(OfId, -1, -1);//return last OfReId
            if (ReqManager2.Count > 0)
            {
                if (Convert.ToInt32(ReqManager2[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid)//ابطال
                {
                    ShowMessage("امکان درخواست جدید وجود ندارد.پروانه عضو مورد باطل شده است");
                    return;
                }
            }

            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count != 1)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return;
            }
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);// TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);      
            #region Insert New Request
            trans.BeginSave();
            DataRow drReq = ReqManager2.NewRow();
            drReq["OfId"] = OfId;
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFSerialNo"]))
                drReq["MFSerialNo"] = ReqManager[0]["MFSerialNo"].ToString();
            if (!string.IsNullOrWhiteSpace(txtdSerialNo.Text))
                drReq["SerialNo"] = txtdSerialNo.Text;
            if (!string.IsNullOrWhiteSpace(txtdLastRegDate.Text))
                drReq["RegDate"] = txtdLastRegDate.Text;
            if (!string.IsNullOrWhiteSpace(txtdExpDate.Text))
                drReq["ExpireDate"] = txtdExpDate.Text;
            drReq["Type"] = (int)OfficeRequestType;
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["PrId"]))
                drReq["PrId"] = ReqManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegPlaceId"]))
                drReq["RegPlaceId"] = ReqManager[0]["RegPlaceId"].ToString();

            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
            {
                MFNo = ReqManager[0]["MFNo"].ToString();
                string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string Code = MFNoMajor[0];

                if (ComboDocType.Value != null)
                {
                    drReq["MFType"] = ComboDocType.Value;

                    if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                    {
                        MFNoMajor[0] = MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();
                        drReq["ActivityType"] = DBNull.Value;
                    }
                    else if (Convert.ToInt32(ComboDocType.Value) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
                    {
                        MFNoMajor[0] = MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
                        drReq["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
                    }


                    drReq["MFNo"] = MFNo;

                    MFNo = string.Join("-", MFNoMajor);

                    drReq["MFNo"] = MFNo;
                }

                else if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
                {
                    drReq["MFNo"] = ReqManager[0]["MFNo"];
                    drReq["MFType"] = ReqManager[0]["MFType"];

                }
            }

            drReq["IsConfirm"] = 0;
            drReq["InActive"] = 0;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;
            drReq["OfName"] = txtOfName.Text;
            if (!Utility.IsDBNullOrNullValue(txtOfNameEn.Text))
                drReq["OfNameEn"] = txtOfNameEn.Text;
            if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
            else if (txtOfTel1.Text != "")
                drReq["Tel1"] = txtOfTel1.Text;
            if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
            else if (txtOfTel2.Text != "")
                drReq["Tel2"] = txtOfTel2.Text;
            if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax_pre.Text + "-" + txtOfFax.Text;
            else if (txtOfFax.Text != "")
                drReq["Fax"] = txtOfFax.Text;
            drReq["MobileNo"] = txtOfMobile.Text;
            drReq["Email"] = txtOfEmail.Text;
            drReq["Website"] = txtOfWebsite.Text;
            drReq["Address"] = txtOfAddress.Text;
            if (drdOfType.Value != null)
                drReq["OtId"] = int.Parse(drdOfType.Value.ToString());
            drReq["Subject"] = txtOfSubject.Text;
            drReq["RegOfDate"] = txtOfRegDate.Text;
            drReq["RegOfNo"] = txtOfRegNo.Text;
            drReq["RegOfPlace"] = txtOfRegPlace.Text;
            if (txtOfStock.Text != "")
                drReq["Stock"] = int.Parse(txtOfStock.Text);
            if (txtOfValue.Text != "")
                drReq["VolumeInvest"] = txtOfValue.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Requester"] = 0;//شرکت
            drReq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.OfficeRequest);

            if (Session["FileOfArm2"] != null)
            {
                imgOfArm.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                drReq["ArmUrl"] = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
            }
            else
                drReq["ArmUrl"] = imgOfArm.ImageUrl;

            if (Session["FileOfSign2"] != null)
            {
                imgOfSign.ImageUrl = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                drReq["SignUrl"] = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());

            }
            else
                drReq["SignUrl"] = imgOfSign.ImageUrl;

            ReqManager2.AddRow(drReq);
            int cn = ReqManager2.Save();
            if (cn <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion
            int TableId = Convert.ToInt32(ReqManager2[ReqManager2.Count - 1]["OfReId"]);
            txtFileNo.Text = ReqManager2[0]["MFNo"].ToString();
            #region Attachments
            dtOfImg = (DataTable)Session["TblOfReImg"];

            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                    drImg["RefTable"] = TableId;
                    drImg["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
                    drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                    drImg["IsValid"] = 1;
                    drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                    drImg["UserId"] = Utility.GetCurrentUser_UserId();
                    drImg["ModfiedDate"] = DateTime.Now;
                    attachManager.AddRow(drImg);
                    int imgcnt = attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                    if (imgcnt == 1)
                    {
                        dtOfImg.Rows[i].BeginEdit();
                        dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                        dtOfImg.Rows[i].EndEdit();

                        if (!Utility.IsDBNullOrNullValue(dtOfImg.Rows[i]["ImgUrl"]))
                        {
                            string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);

                        }

                    }
                }
            }
            #endregion

            #region  WF
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            int CurrentNmcId = Utility.GetCurrentUser_MeId();
            int CurrentNmcIdType = Utility.GetCurrentUser_NmcIdType();
            if (Utility.IsDBNullOrNullValue(CurrentNmcId))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            string Description = GenerateWFDescription(OfficeRequestType);
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), CurrentNmcIdType, Description);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion

            InsertInActtiveOfficeMembers(OffMemberManager, DocMemberFileManager, ReqInActiveManager, OfId, TableId);
            MFNo = SetOfficeMfNo(OfId, TableId, PrCode, MFCode, ReqManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
            }
            ReqManager2.FindByCode(TableId);
            if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["TaskName"]))
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت درخواست: " + ReqManager2[0]["TaskName"].ToString();
            }

            #region Do Next Task Of Inserting
            trans.EndSave();
            txtFileNo.Text = MFNo;
            OfficeRequest.Value = Utility.EncryptQS(TableId.ToString());
            PgMode.Value = Utility.EncryptQS("Edit");
            ShowMessage("ذخیره انجام شد.");

            RoundPanelOffice.HeaderText = "ویرایش";
            RoundPanelFileAttachment.Visible = true;
            DisableForReq();
            MenuDetails.Enabled = true;
            CheckColor(OfId);
            #endregion
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
        #region Images
        if (Session["FileOfSign2"] != null)
        {
            try
            {

                string SignSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                string SignTarget = Server.MapPath("~/Image/Office/Sign/") + Path.GetFileName(Session["FileOfSign2"].ToString());
                System.IO.File.Move(SignSoource, SignTarget);
                imgOfSign.ImageUrl = "~/Image/Office/Sign/" + Path.GetFileName(Session["FileOfSign2"].ToString());
                Session["FileOfSign2"] = null;

            }
            catch (Exception)
            {
            }
        }
        if (Session["FileOfArm2"] != null)
        {
            try
            {
                string ArmSoource = Server.MapPath("~/Image/Temp/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                string ArmTarget = Server.MapPath("~/Image/Office/Arm/") + Path.GetFileName(Session["FileOfArm2"].ToString());
                System.IO.File.Move(ArmSoource, ArmTarget);
                imgOfArm.ImageUrl = "~/Image/Office/Arm/" + Path.GetFileName(Session["FileOfArm2"].ToString());
                Session["FileOfArm2"] = null;


            }
            catch (Exception)
            {
            }
        }
        #endregion
    }

    #endregion

    private Boolean CheckMembersByOfficeType(int OfId, int MFType)
    {
        #region CheckMembers

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DocMemberFileManager.ClearBeforeFill = true;

        int min = 0;
        bool IsDes = false;
        bool IsObs = false;
        bool IsImp = false;
        DataTable dtOffMembers = OfMeManager.FindOfficeActiveMember(OfId);
        for (int i = 0; i < dtOffMembers.Rows.Count; i++)
        {
            int MeId = int.Parse(dtOffMembers.Rows[i]["PersonId"].ToString());
            int OfpId = Convert.ToInt32(dtOffMembers.Rows[i]["OfpId"]);

            if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
            {
                if (OfpId == (int)TSP.DataManager.OfficePosition.Manager || OfpId == (int)TSP.DataManager.OfficePosition.ManagerAndBoard)//مدیر عامل
                {
                    if (Convert.ToInt32(dtOffMembers.Rows[i]["OfmType"]) != (int)TSP.DataManager.OfficeMemberType.Member)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.مدیرعامل شرکت باید تنها از بین اعضا انتخاب شود";
                        return false;
                    }
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                    if (dtMeFile.Rows.Count > 0)
                    {
                        int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

                        DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                        if (dtMeDetail.Rows.Count == 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت طراحی ندارد";
                            return false;
                        }
                        else
                            min += 1;
                        DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
                        if (dtMeDetail2.Rows.Count == 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت نظارت ندارد";
                            return false;
                        }
                        else
                            min += 1;

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.مدیر عامل شرکت دارای پروانه اشتغال به کار نمی باشد";
                        return false;
                    }
                }
                else if (OfpId == (int)TSP.DataManager.OfficePosition.Board)//عضو هیئت مدیره
                {
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                    if (dtMeFile.Rows.Count > 0)
                    {

                        int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                        DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                        if (dtMeDetail.Rows.Count > 0)
                            IsDes = true;
                        DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
                        if (dtMeDetail2.Rows.Count > 0)
                            IsObs = true;
                    }

                }
            }
            else if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
            {
                if (Convert.ToInt32(dtOffMembers.Rows[i]["OfmType"]) != (int)TSP.DataManager.OfficeMemberType.Member)
                    continue;
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
                    {
                        min += 1;
                    }
                }
            }


        }
        //if (MFType == 1)
        //{
        //    if (IsDes == false || IsObs == false)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
        //        return false;
        //    }
        //}
        //else if (MFType == 2)
        //{
        //    if (IsImp == false)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
        //        return false;
        //    }
        //}
        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
        {
            if (min < 2)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 2 نفر از اعضای شرکت دارای صلاحیت پروانه مورد تقاضا باشند";
                return false;
            }
        }

        return true;
        #endregion
    }

    #region SetEnables
    protected void Enable()
    {
        txtOfAddress.Enabled = true;
        txtOfDesc.Enabled = true;
        txtOfEmail.Enabled = true;
        txtOfFax.Enabled = true;
        txtOfFax_pre.Enabled = true;
        //txtOfFileNo.Enabled = true;
        txtOfMobile.Enabled = true;
        txtOfName.Enabled = true;
        txtOfNameEn.Enabled = true;
        txtOfRegDate.Enabled = true;
        txtOfRegNo.Enabled = true;
        txtOfRegPlace.Enabled = true;
        txtOfStock.Enabled = true;
        txtOfSubject.Enabled = true;
        txtOfTel1.Enabled = true;
        txtOfTel1_pre.Enabled = true;
        txtOfTel2.Enabled = true;
        txtOfTel2_pre.Enabled = true;
        txtOfValue.Enabled = true;
        txtOfWebsite.Enabled = true;
        //drdMrsId.Enabled = true;
        drdOfType.Enabled = true;
        //aspxcmbAttype.Enabled = true;
        //ASPxTextBoxFicheCode.Enabled = true;
        flpOfArm.ClientVisible = true;
        flpOfSign.ClientVisible = true;

        txtdExpDate.Enabled = true;
        txtdLastRegDate.Enabled = true;
        txtdSerialNo.Enabled = true;
        cmbdIsTemporary.Enabled = true;
        cmbActivityType.Enabled = true;
    }

    protected void DisableForReq()
    {
        txtOfDesc.Enabled = false;
        //txtOfRegDate.Enabled = false;
        //txtOfRegNo.Enabled = false;
        //txtOfRegPlace.Enabled = false;
        //txtOfStock.Enabled = false;
        //txtOfSubject.Enabled = false;
        //txtOfValue.Enabled = false;
        //drdOfType.Enabled = false;
        ASPxTextBoxAmount.Enabled = false;
        ASPxTextBoxFicheCode.Enabled = false;

        TblFile.Visible = false;
        //ASPxRoundPanelAttReq.Visible = true;
        //RoundPanelDocument.Visible = false;
    }

    protected void Disable()
    {
        txtOfAddress.Enabled = false;
        txtOfDesc.Enabled = false;
        txtOfEmail.Enabled = false;
        txtOfFax.Enabled = false;
        txtOfFax_pre.Enabled = false;
        //txtOfFileNo.Enabled = false;
        txtOfMobile.Enabled = false;
        txtOfName.Enabled = false;
        txtOfNameEn.Enabled = false;
        txtOfRegDate.Enabled = false;
        txtOfRegNo.Enabled = false;
        txtOfRegPlace.Enabled = false;
        txtOfStock.Enabled = false;
        txtOfSubject.Enabled = false;
        txtOfTel1.Enabled = false;
        txtOfTel1_pre.Enabled = false;
        txtOfTel2.Enabled = false;
        txtOfTel2_pre.Enabled = false;
        txtOfValue.Enabled = false;
        txtOfWebsite.Enabled = false;
        //drdMrsId.Enabled = false;
        drdOfType.Enabled = false;
        //aspxcmbAttype.Enabled = false;
        ASPxTextBoxFicheCode.Enabled = false;

        flpOfArm.ClientVisible = false;
        flpOfSign.ClientVisible = false;

        txtdExpDate.Enabled = false;
        txtdLastRegDate.Enabled = false;
        txtdSerialNo.Enabled = false;
        cmbdIsTemporary.Enabled = false;
        cmbActivityType.Enabled = false;
    }

    protected void DisableForChangeBaseInfo()
    {
        // txtOfAddress.Enabled = false;
        txtOfDesc.Enabled = false;
        //  txtOfEmail.Enabled = false;
        //  txtOfFax.Enabled = false;
        //  txtOfFax_pre.Enabled = false;
        //   txtOfMobile.Enabled = false;
        txtOfName.Enabled = false;
        // txtOfNameEn.Enabled = false;
        txtOfRegDate.Enabled = false;
        txtOfRegNo.Enabled = false;
        txtOfRegPlace.Enabled = false;
        //   txtOfStock.Enabled = false;
        //  txtOfSubject.Enabled = false;
        //  txtOfTel1.Enabled = false;
        //  txtOfTel1_pre.Enabled = false;
        //   txtOfTel2.Enabled = false;
        //   txtOfTel2_pre.Enabled = false;
        //  txtOfValue.Enabled = false;
        //   txtOfWebsite.Enabled = false;
        drdOfType.Enabled = false;
        ASPxTextBoxFicheCode.Enabled = false;

        //   flpOfArm.ClientVisible = false;
        //  flpOfSign.ClientVisible = false;

        txtdExpDate.Enabled = false;
        txtdLastRegDate.Enabled = false;
        txtdSerialNo.Enabled = false;
        cmbdIsTemporary.Enabled = false;
        cmbActivityType.Enabled = false;
    }
    #endregion

    protected void ClearForm()
    {
        txtOfId.Text = "";
        txtMeNo.Text = "";
        txtOfAddress.Text = "";
        txtOfDesc.Text = "";
        txtOfEmail.Text = "";
        txtOfFax.Text = "";
        txtOfFax_pre.Text = "";
        txtOfMobile.Text = "";
        txtOfName.Text = "";
        txtOfNameEn.Text = "";
        txtOfRegDate.Text = "";
        txtOfRegNo.Text = "";
        txtOfRegPlace.Text = "";
        txtOfStock.Text = "";
        txtOfSubject.Text = "";
        txtOfTel1.Text = "";
        txtOfTel1_pre.Text = "";
        txtOfTel2.Text = "";
        txtOfTel2_pre.Text = "";
        txtOfValue.Text = "";
        txtOfWebsite.Text = "";

        //  txtReRequestDesc.Text = "";

        txtOfRegDate.Text = "";
        txtOfAddress.Text = "";
        txtOfDesc.Text = "";
        imgOfArm.ImageUrl = "";
        imgOfSign.ImageUrl = "";
        ASPxTextBoxFicheCode.Text = "";

        HDFlpArm.Set("name", "0");
        HDFlpSign.Set("name", "0");


        txtdExpDate.Text = "";
        txtdLastRegDate.Text = "";
        txtdSerialNo.Text = "";
        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
        ComboDocType.DataBind();
        ComboDocType.SelectedIndex = -1;
        cmbActivityType.SelectedIndex = -1;
        txtFileNo.Text = "";

        dtOfImg = (DataTable)Session["TblOfReImg"];
        dtOfImg.Rows.Clear();
        Session["TblOfReImg"] = dtOfImg;
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

        Session["OffMenuArrayList"] = null;
        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        Session["OffMenuArrayList"] = arr;

        for (int i = 0; i < MenuDetails.Items.Count; i++)
        {
            MenuDetails.Items[i].Image.Url = "";
        }

    }

    private void ClearDocumentInfo()
    {
        ComboDocType.DataBind();
        ComboDocType.SelectedIndex = -1;
        txtFileNo.Text = "";
        txtdSerialNo.Text = "";
        txtdLastRegDate.Text = "";
        txtdExpDate.Text = "";
        cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(((int)TSP.DataManager.DocumentSetExpireDateType.Permanent).ToString()).Index;
        SetMeDocDefualtRegisterDate((int)TSP.DataManager.DocumentSetExpireDateType.Permanent);
    }

    #region Capacity-Grade
    //private void SetCapacity()
    //{
    //    string PageMode = Utility.DecryptQS(PgMode.Value);
    //    if (Utility.DecryptQS(PgMode.Value) == "New" || Utility.DecryptQS(PgMode.Value) == "NewReqMembership")
    //        return;
    //    if (OfficeRequest.Value == null)
    //        return;
    //    string OfReId = Utility.DecryptQS(OfficeRequest.Value);
    //    if (string.IsNullOrEmpty(OfReId))
    //        return;
    //    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
    //    ReqManager.FindByCode(int.Parse(OfReId));
    //    if (ReqManager.Count <= 0)
    //        return;
    //    if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo && Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)
    //    {
    //        RoundPanelOfficeGrade.Visible = true;
    //        if (HDMFType.Value == ((int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign).ToString())
    //        {
    //            if (Session["Capacity"] == null)
    //            {
    //                Session["Capacity"] = GetOfficeDsgnAndObsCapacity(int.Parse(Utility.DecryptQS(OfficeId.Value)));

    //            }
    //            else
    //            {
    //                CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["Capacity"];
    //                CustomAspxDevGridViewGrade.DataBind();

    //            }
    //        }
    //    }
    //    else
    //        RoundPanelOfficeGrade.Visible = false;




    //    //if (PageMode != "New" && PageMode != "NewReqMembership")
    //    //{
    //    //    if (!string.IsNullOrEmpty(OfReId))
    //    //    {

    //    //        ReqManager.FindByCode(int.Parse(OfReId));
    //    //        if (ReqManager.Count > 0)
    //    //        {
    //    //            if (Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo && Convert.ToInt32(ReqManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)
    //    //            {
    //    //                RoundPanelOfficeGrade.Visible = true;

    //    //                OffManager.FindByCode(int.Parse(OfId));
    //    //                if (!Utility.IsDBNullOrNullValue(OffManager[0]["MFType"]))
    //    //                {
    //    //                    if ((Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign) || (Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement))
    //    //                    {
    //    //                        Session["Capacity"] = GetOfficeDsgnAndObsCapacity(int.Parse(OfId));
    //    //                        HDMFType.Value = ((int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign).ToString();
    //    //                    }
    //    //                    else
    //    //                    {
    //    //                        //Session["Capacity"] = "Imp";
    //    //                        CustomAspxDevGridViewGrade.Visible = false;
    //    //                        TableImp.Visible = true;
    //    //                        GetOfficeImpCapacity(int.Parse(OfId));
    //    //                        HDMFType.Value = ((int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement).ToString();

    //    //                    }
    //    //                }
    //    //            }
    //    //            else
    //    //                RoundPanelOfficeGrade.Visible = false;

    //    //        }

    //    //        CheckMenuImage(int.Parse(OfReId));
    //    //    }
    //    //}
    //}

    private void SetOfficeDesObsGrades(int OfId, int OfReId)
    {
        if (Session["DesObsGrade"] == null)
            Session["DesObsGrade"] = GetOfficeDsgnAndObsGrade(OfId, OfReId);
        CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["DesObsGrade"];
        CustomAspxDevGridViewGrade.DataBind();
    }
    protected DataTable GetOfficeDsgnAndObsGrade(int OfId, int OfReId)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dt = OfficeMemberManager.FindOfficeDsngAndObsGrade(OfId);
        #region Commented Capacity
        //double ArchCapacity = 0;
        //double CivilCapacity = 0;
        //double ElecCapacity = 0;
        //double MapCapacity = 0;
        //double MechCapacity = 0;
        //double TrCapacity = 0;
        //double UrbCapacity = 0;


        //int MjId = 0;
        //ArrayList arr = new ArrayList();

        //Capacity Capacity = new Capacity();
        //arr = Capacity.GetOfficeMembersDsgObsCapacity(OfId, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
        //if (arr != null)
        //{
        //    for (int i = 0; i < arr.Count; i++)
        //    {
        //        MjId = Convert.ToInt32(((ArrayList)arr[i])[5]);
        //        if (MjId == (int)TSP.DataManager.MainMajors.Architecture)
        //        {
        //            ArchCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Civil)
        //        {
        //            CivilCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Electronic)
        //        {
        //            ElecCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Mapping)
        //        {
        //            MapCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Mechanic)
        //        {
        //            MechCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Traffic)
        //        {
        //            TrCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Urbanism)
        //        {
        //            UrbCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
        //        }
        //    }
        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Architecture;
        //    dt.DefaultView[0]["DsgnCapacity"] = ArchCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Civil;
        //    dt.DefaultView[0]["DsgnCapacity"] = CivilCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Electronic;
        //    dt.DefaultView[0]["DsgnCapacity"] = ElecCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mapping;
        //    dt.DefaultView[0]["DsgnCapacity"] = MapCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mechanic;
        //    dt.DefaultView[0]["DsgnCapacity"] = MechCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Traffic;
        //    dt.DefaultView[0]["DsgnCapacity"] = TrCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Urbanism;
        //    dt.DefaultView[0]["DsgnCapacity"] = UrbCapacity;
        //}
        //ArchCapacity = 0;
        //CivilCapacity = 0;
        //ElecCapacity = 0;
        //MapCapacity = 0;
        //MechCapacity = 0;
        //TrCapacity = 0;
        //UrbCapacity = 0;

        //arr = Capacity.GetOfficeMembersDsgObsCapacity(OfId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
        //if (arr != null)
        //{
        //    for (int i = 0; i < arr.Count; i++)
        //    {
        //        MjId = Convert.ToInt32(((ArrayList)arr[i])[5]);
        //        if (MjId == (int)TSP.DataManager.MainMajors.Architecture)
        //        {
        //            ArchCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Civil)
        //        {
        //            CivilCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Electronic)
        //        {
        //            ElecCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Mapping)
        //        {
        //            MapCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Mechanic)
        //        {
        //            MechCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Traffic)
        //        {
        //            TrCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //        if (MjId == (int)TSP.DataManager.MainMajors.Urbanism)
        //        {
        //            UrbCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
        //        }
        //    }
        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Architecture;
        //    dt.DefaultView[0]["ObsCapacity"] = ArchCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Civil;
        //    dt.DefaultView[0]["ObsCapacity"] = CivilCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Electronic;
        //    dt.DefaultView[0]["ObsCapacity"] = ElecCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mapping;
        //    dt.DefaultView[0]["ObsCapacity"] = MapCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mechanic;
        //    dt.DefaultView[0]["ObsCapacity"] = MechCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Traffic;
        //    dt.DefaultView[0]["ObsCapacity"] = TrCapacity;

        //    dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Urbanism;
        //    dt.DefaultView[0]["ObsCapacity"] = UrbCapacity;

        //}

        //dt.DefaultView.RowFilter = "";
        #endregion
        return dt;
    }

    protected void GetOfficeImpCapacity(int OfId, int OfReId)
    {
        Capacity Capacity = new Capacity();
        Capacity.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Office, OfId, true);
        txtImpCapacity.Text = Capacity.IngridientMaxJobCapacity.ToString();

        txtImpGrdId.Text = GetOfficeGradeName(OfReId);
    }

    protected string GetOfficeGradeName(int OfReId)
    {
        string GrdName = "";
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        OfficeRequestManager.FindByCode(OfReId);
        if (OfficeRequestManager.Count == 1)
        {
            GrdName = OfficeRequestManager[0]["GrdName"].ToString();
        }
        return GrdName;
    }
    #endregion

    protected void CheckColor(int OfId)
    {
        bool change = false;

        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count == 1)
        {
            if (txtOfName.Text != OffManager[0]["OfName"].ToString())
                txtOfName.ForeColor = Color.Red;
            if (txtOfNameEn.Text != OffManager[0]["OfNameEn"].ToString())
                txtOfNameEn.ForeColor = Color.Red;

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && OffManager[0]["Tel1"].ToString().Contains("-"))
            {
                if (txtOfTel1.Text != OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1))
                {
                    txtOfTel1.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfTel1_pre.Text != OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-")))
                {
                    txtOfTel1_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !OffManager[0]["Tel1"].ToString().Contains("-"))
            {
                if (txtOfTel1.Text != OffManager[0]["Tel1"].ToString())
                {
                    txtOfTel1.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !string.IsNullOrEmpty(txtOfTel1.Text))
            {
                txtOfTel1.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel1"]) && !string.IsNullOrEmpty(txtOfTel1_pre.Text))
            {
                txtOfTel1_pre.ForeColor = Color.Red;
                change = true;
            }

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && OffManager[0]["Tel2"].ToString().Contains("-"))
            {
                if (txtOfTel2.Text != OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1))
                {
                    txtOfTel2.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfTel2_pre.Text != OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-")))
                {
                    txtOfTel2_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !OffManager[0]["Tel2"].ToString().Contains("-"))
            {
                if (txtOfTel2.Text != OffManager[0]["Tel2"].ToString())
                {
                    txtOfTel2.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !string.IsNullOrEmpty(txtOfTel2.Text))
            {
                txtOfTel2.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Tel2"]) && !string.IsNullOrEmpty(txtOfTel2_pre.Text))
            {
                txtOfTel2_pre.ForeColor = Color.Red;
                change = true;
            }

            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && OffManager[0]["Fax"].ToString().Contains("-"))
            {
                if (txtOfFax.Text != OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1))
                {
                    txtOfFax.ForeColor = Color.Red;
                    change = true;
                }
                if (txtOfFax_pre.Text != OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-")))
                {
                    txtOfFax_pre.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (!Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !OffManager[0]["Fax"].ToString().Contains("-"))
            {
                if (txtOfFax.Text != OffManager[0]["Fax"].ToString())
                {
                    txtOfFax.ForeColor = Color.Red;
                    change = true;
                }
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !string.IsNullOrEmpty(txtOfFax.Text))
            {
                txtOfFax.ForeColor = Color.Red;
                change = true;
            }
            if (Utility.IsDBNullOrNullValue(OffManager[0]["Fax"]) && !string.IsNullOrEmpty(txtOfFax_pre.Text))
            {
                txtOfFax_pre.ForeColor = Color.Red;
                change = true;
            }

            if (txtOfMobile.Text != OffManager[0]["MobileNo"].ToString())
            {
                txtOfMobile.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfEmail.Text != OffManager[0]["Email"].ToString())
            {
                txtOfEmail.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfWebsite.Text != OffManager[0]["Website"].ToString())
            {
                txtOfWebsite.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfAddress.Text != OffManager[0]["Address"].ToString())
            {
                txtOfAddress.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegDate.Text != OffManager[0]["RegDate"].ToString())
            {
                txtOfRegDate.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegNo.Text != OffManager[0]["RegOfNo"].ToString())
            {
                txtOfRegNo.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfRegPlace.Text != OffManager[0]["RegPlace"].ToString())
            {
                txtOfRegPlace.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfStock.Text != OffManager[0]["Stock"].ToString())
            {
                txtOfStock.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfSubject.Text != OffManager[0]["Subject"].ToString())
            {
                txtOfSubject.ForeColor = Color.Red;
                change = true;
            }
            if (txtOfValue.Text != OffManager[0]["VolumeInvest"].ToString())
            {
                txtOfValue.ForeColor = Color.Red;
                change = true;
            }
            if (drdOfType.Value != null && !Utility.IsDBNullOrNullValue(OffManager[0]["OtId"]))
            {
                if (Convert.ToInt32(drdOfType.Value) != Convert.ToInt32(OffManager[0]["OtId"]))
                {
                    drdOfType.ForeColor = Color.Red;
                    change = true;
                }
            }

        }
        if (change == true)
        {
            if (Session["OffMenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
                Session["OffMenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));
                ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
                Session["OffMenuArrayList"] = arr;
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
        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.OfficeManager officeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager officeRequestManager = new TSP.DataManager.OfficeRequestManager();



        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        arr.Add(0);//arr[7]-->Group


        officeRequestManager.FindByCode(OfReId);
        if (officeRequestManager.Count > 0)
        {
            int OfId = Convert.ToInt32(officeRequestManager[0]["OfId"]);
            officeManager.FindByCode(OfId);
            if (officeManager.Count > 0)
            {
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                MenuDetails.Items[MenuDetails.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
            }
        }


        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            MenuDetails.Items[MenuDetails.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    private void SetMeDocDefualtRegisterDate(int DocumentSetExpireDateType)
    {
        txtdLastRegDate.Text = Utility.GetDateOfToday();
        Utility.Date Date = new Utility.Date();
        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void SetMeDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtdLastRegDate.Text))
        {
            txtdLastRegDate.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtdLastRegDate.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                // Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtdExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    /// <summary>
    /// برای درخواست صدور-تمدید-المثنی ورود فیش الزامی می باشد
    /// </summary>
    /// <param name="MfId"></param>
    /// <returns></returns>
    private Boolean CheckCanEditFishForEdit(int OfReId)
    {
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        OfficeRequestManager.FindByCode(OfReId);
        if (OfficeRequestManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }

        int OfficeRequestType = Convert.ToInt32(OfficeRequestManager[0]["Type"]);
        if (OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.SaveFileDocument
            || OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Reduplicate
            || OfficeRequestType == (int)TSP.DataManager.OfficeRequestType.Revival)
        {
            return true;
        }
        return false;
    }

    private string GenerateWFDescription(TSP.DataManager.OfficeRequestType OfficeRequestType)
    {
        string Des = "";
        switch (OfficeRequestType)
        {
            case TSP.DataManager.OfficeRequestType.Change:
                Des = "شروع گردش کار درخواست تغییرات پروانه شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.DocumentInvalid:
                Des = "شروع گردش کار درخواست ابطال پروانه شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.Invalid:
                Des = "شروع گردش کار درخواست ابطال عضویت شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.Reduplicate:
                Des = "شروع گردش کار درخواست صدور المثنی پروانه شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.Revival:
                Des = "شروع گردش کار درخواست تمدید پروانه شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.SaveFileDocument:
                Des = "شروع گردش کار درخواست صدور پروانه شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.SaveMembershipRequset:
                Des = "شروع گردش کار درخواست تغییرات عضویت شخص حقوقی"; ;
                break;
            case TSP.DataManager.OfficeRequestType.SaveRequestInfo:
                Des = "شروع گردش کار درخواست ثبت عضویت شخص حقوقی"; ;
                break;
        }
        return Des;
    }

    private string SetOfficeMfNo(int OfId, int OfReId, string PrCode, string MFCode, TSP.DataManager.OfficeRequestManager ReqManager)
    {
        string MFNo = "";
        string MFMjCode = "0000000";
        #region SetMFNo
        TSP.DataManager.OfficeMemberManager OffMemManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        DataTable dtOfMe = OffMemManager.SelectOfficeMember(OfId, 1, -1);//return member
        if (dtOfMe.Rows.Count > 0)
        {
            for (int m = 0; m < dtOfMe.Rows.Count; m++)
            {
                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
                if (dtMj.Rows.Count > 0)
                {
                    int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    int i = -1;
                    char[] Code = MFMjCode.ToCharArray();

                    switch (MjId)
                    {
                        case (int)TSP.DataManager.MainMajors.Architecture:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Civil:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Electronic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mapping:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mechanic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Traffic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                            Code[i] = MjId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Urbanism:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                            Code[i] = MjId.ToString()[0];
                            break;
                        default:
                            i = -1;
                            break;
                    }
                    if (i != -1)
                    {
                        MFMjCode = new string(Code);
                    }
                }
                dtMj.Clear();
            }
        }
        #endregion
        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count != 1)
            return "";
        string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
        while (MFSerialNo.Length < 5)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        ReqManager[0]["MFNo"] = MFNo = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
        ReqManager.Save();
        return MFNo;
    }
    #endregion
}
