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

public partial class Members_MemberInfo_MemberInsertBaseInfo : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
    #region Properties
    private int _MeId
    {
        get
        {
            return Utility.GetCurrentUser_MeId();
        }

    }
    string _MeImgUpload
    {
        get
        {
            try { return HiddenFieldPage["MeImgUpload"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldPage["MeImgUpload"] = value;
        }
    }
    string _MeImageIdNo
    {
        get
        {
            try { return HiddenFieldPage["MeImageIdNo"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["MeImageIdNo"] = value;
        }
    }
    string _MeImageIdNoP2
    {
        get
        {
            try { return HiddenFieldPage["MeImageIdNoP2"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["MeImageIdNoP2"] = value;
        }
    }
    string _MeImageIdNoPDes
    {
        get
        {
            try { return HiddenFieldPage["MeImageIdNoDes"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["MeImageIdNoDes"] = value;
        }
    }
    string _MeImageSSN
    {
        get
        {
            try { return HiddenFieldPage["FileOfSSN"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["FileOfSSN"] = value;
        }
    }
    string _MeImageSSNBack
    {
        get
        {
            try { return HiddenFieldPage["FileOfSSNBack"].ToString(); }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["FileOfSSNBack"] = value;
        }
    }
    string _MeImageSol
    {
        get
        {
            try
            {
                return HiddenFieldPage["FileOfSol"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["FileOfSol"] = value;
        }
    }
    string _MeImageSolBack
    {
        get
        {
            try
            {
                return HiddenFieldPage["FileOfSolBack"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["FileOfSolBack"] = value;
        }
    }

    string _MeImageRes
    {
        get
        {
            try
            {
                return HiddenFieldPage["FileOfResident"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["FileOfResident"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxLabelImgWarning.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";

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

        if (!IsPostBack)
        {
            #region !IsPostBack
            HiddenFieldPage.Set("MeSSN", 0);
            HiddenFieldPage.Set("SSNBack", 0);
            HiddenFieldPage.Set("FlpMe", 0);
            HiddenFieldPage.Set("MeIdNo", 0);
            HiddenFieldPage.Set("MeSol", 0);
            HiddenFieldPage.Set("Resident", 0);
            
            _MeImageIdNo = _MeImageIdNoP2 = _MeImageIdNoPDes =
            _MeImageSSN = _MeImageSSNBack =
            _MeImageSol = _MeImageSolBack = _MeImageRes =
            _MeImgUpload = null;
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");
            }

            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            string PageMode = Utility.DecryptQS(PgMode.Value);

            switch (PageMode)
            {
                case "ChangeBaseInfo":
                    RoundPanelMain.Enabled = true;
                    RoundPanelMain.HeaderText = "ویرایش";
                    btnSave.Enabled = btnSave2.Enabled = true;
                    FillForm();
                    break;
                case "View":
                    PgMode.Value = Utility.EncryptQS("View");
                    // RoundPanelMain.Enabled = false;
                    disableControls();
                    RoundPanelMain.HeaderText = "مشاهده";
                    btnSave.Enabled = btnSave2.Enabled = false;
                    FillForm();
                    break;
            }

            #endregion
        }

        if (_MeImgUpload != null)
            imgMember.ImageUrl = _MeImgUpload;
        if (_MeImageIdNo != null)
            HpIdNo.NavigateUrl = _MeImageIdNo;
        if (_MeImageIdNoP2 != null)
            HIdNoP2.NavigateUrl = _MeImageIdNoP2;
        if (_MeImageIdNoPDes != null)
            HIdNoPDes.NavigateUrl = _MeImageIdNoPDes;
        if (_MeImageSol != null)
            HpSoldier.NavigateUrl = _MeImageSol;
        if (_MeImageSolBack != null)
            HpSoldierBack.NavigateUrl = _MeImageSolBack;
        if (_MeImageSSNBack != null)
            hssnBack.NavigateUrl = _MeImageSSNBack;
        if (_MeImageSSN != null)
            HpSSN.NavigateUrl = _MeImageSSN;
        if (_MeImageRes != null)
            HpResident.NavigateUrl = _MeImageRes;



        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        _MeImageIdNo = _MeImageIdNoP2 = _MeImageIdNoPDes =
        _MeImageSSN = _MeImageSSNBack =
        _MeImageSol = _MeImageSolBack = _MeImageRes =
        _MeImgUpload = null;
        string UrlRef = Utility.DecryptQS(Request.QueryString["UrlRef"]);
        switch (UrlRef)
        {
            case "MemberRequest":
                Response.Redirect("MemberRequest.aspx?MeId=" + _MeId);
                break;
            case "MemberHome":
                Response.Redirect("~/Members/MemberHome.aspx");
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_IsLock())
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            ShowMessage("به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.");
            return;
        }
        try
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            if (PageMode == "ChangeBaseInfo")
            {
                ChangeBaseInfo();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    #region Images
    protected void flpImage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {

            e.CallbackData = SaveImageMember(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpIdNo_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageIdNo(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpSSN_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSSN(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpSoldier_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSoldier(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void flpResident_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageResident(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion
    #endregion

    #region Methods
    private void disableControls()
    {
        txtEmail.Enabled = txtHomeAdr.Enabled = txtMobile.Enabled = txtSSN.Enabled = flpIdNo.ClientVisible = flpIdNoP2.ClientVisible = flpIdNoPDes.ClientVisible = flpImage.ClientVisible =
            flpSoldier.ClientVisible = flpSoldierBack.ClientVisible = flpSSN.ClientVisible = flpSSNBack.ClientVisible = flpResident.ClientVisible = false;
    }
    #region Insert
    protected void ChangeBaseInfo()
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager memberRequestManager = new TSP.DataManager.MemberRequestManager();

        #region Check Condition
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            ShowMessage("امکان ثبت درخواست جدید برای اعضای موقت وجود ندارد");
            return;
        }
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
        {
            MeManager.FindByCode(_MeId);
            if ((bool)MeManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(_MeId, 0, 1);
                ShowMessage("به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.");
                return;
            }
        }
        memberRequestManager.FindByMemberId(_MeId, 0, -1);
        if (memberRequestManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        #endregion

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberRequestManager MReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();


        trans.Add(MReqManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowTaskManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(MeManager);

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
        int WfCurrentTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        int MReId = -1;
        try
        {

            trans.BeginSave();
            String Message;
            MReId = InsertMemberRequest(_MeId, MReqManager, WfCurrentTaskId,trans);
            if (MReId == -1)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            #region Attachment
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
            if (_MeImageIdNo != null)
            {
                DataRow drAtt = attachManager.NewRow();
                drAtt["TtId"] = TableType;
                drAtt["RefTable"] = MReId;
                drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNo;
                drAtt["IsValid"] = 1;
                drAtt["FilePath"] = _MeImageIdNo;
                drAtt["FileName"] = _MeId.ToString();
                drAtt["Description"] = DBNull.Value;
                drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                drAtt["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAtt);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }


            if (_MeImageIdNoP2 != null)
            {
                DataRow drAtt = attachManager.NewRow();
                drAtt["TtId"] = TableType;
                drAtt["RefTable"] = MReId;
                drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNoP2;
                drAtt["IsValid"] = 1;
                drAtt["FilePath"] = _MeImageIdNoP2;
                drAtt["FileName"] = _MeId.ToString();
                drAtt["Description"] = DBNull.Value;
                drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                drAtt["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAtt);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }


            if (_MeImageIdNoPDes != null)
            {
                DataRow drAtt = attachManager.NewRow();
                drAtt["TtId"] = TableType;
                drAtt["RefTable"] = MReId;
                drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNoPDes;
                drAtt["IsValid"] = 1;
                drAtt["FilePath"] = _MeImageIdNoPDes;
                drAtt["FileName"] = _MeId.ToString();
                drAtt["Description"] = DBNull.Value;
                drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                drAtt["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAtt);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }

            if (_MeImageSSN != null)
            {
                DataRow drAttSSN = attachManager.NewRow();
                drAttSSN["TtId"] = TableType;
                drAttSSN["RefTable"] = MReId;
                drAttSSN["AttId"] = (int)TSP.DataManager.AttachType.SSN;
                drAttSSN["IsValid"] = 1;
                drAttSSN["FilePath"] = _MeImageSSN;
                drAttSSN["FileName"] = _MeId.ToString();
                drAttSSN["Description"] = DBNull.Value;
                drAttSSN["UserId"] = Utility.GetCurrentUser_UserId();
                drAttSSN["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAttSSN);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();

            }

            if (_MeImageSSNBack != null)
            {
                DataRow drAttSSN = attachManager.NewRow();
                drAttSSN["TtId"] = TableType;
                drAttSSN["RefTable"] = MReId;
                drAttSSN["AttId"] = (int)TSP.DataManager.AttachType.SSNBack;
                drAttSSN["IsValid"] = 1;
                drAttSSN["FilePath"] = _MeImageSSNBack;
                drAttSSN["FileName"] = _MeId.ToString();
                drAttSSN["Description"] = DBNull.Value;
                drAttSSN["UserId"] = Utility.GetCurrentUser_UserId();
                drAttSSN["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAttSSN);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }

            if (_MeImageRes != null)
            {
                DataRow drAttRes = attachManager.NewRow();
                drAttRes["TtId"] = TableType;
                drAttRes["RefTable"] = MReId;
                drAttRes["AttId"] = (int)TSP.DataManager.AttachType.ResidentDoc;
                drAttRes["IsValid"] = 1;
                drAttRes["FilePath"] = _MeImageRes;
                drAttRes["FileName"] = _MeId.ToString();
                drAttRes["Description"] = DBNull.Value;
                drAttRes["UserId"] = Utility.GetCurrentUser_UserId();
                drAttRes["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAttRes);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }

            if (_MeImageSol != null)
            {

                DataRow drAttSol = attachManager.NewRow();
                drAttSol["TtId"] = TableType;
                drAttSol["RefTable"] = MReId;
                drAttSol["AttId"] = (int)TSP.DataManager.AttachType.SoldierCard;
                drAttSol["IsValid"] = 1;
                drAttSol["FilePath"] = _MeImageSol;
                drAttSol["FileName"] = _MeId.ToString();
                drAttSol["Description"] = DBNull.Value;
                drAttSol["UserId"] = Utility.GetCurrentUser_UserId();
                drAttSol["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAttSol);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }

            if (_MeImageSolBack != null)
            {
                DataRow drAttSolBack = attachManager.NewRow();
                drAttSolBack["TtId"] = TableType;
                drAttSolBack["RefTable"] = MReId;
                drAttSolBack["AttId"] = (int)TSP.DataManager.AttachType.SoldierCardBack;
                drAttSolBack["IsValid"] = 1;
                drAttSolBack["FilePath"] = _MeImageSolBack;
                drAttSolBack["FileName"] = _MeId.ToString();
                drAttSolBack["Description"] = DBNull.Value;
                drAttSolBack["UserId"] = Utility.GetCurrentUser_UserId();
                drAttSolBack["ModfiedDate"] = DateTime.Now;
                attachManager.AddRow(drAttSolBack);
                attachManager.Save();
                attachManager.DataTable.AcceptChanges();
            }

            #endregion

            if (!InsertWorkFlow(MReId, _MeId, WorkFlowStateManager, WorkFlowTaskManager, MReqManager))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            trans.EndSave();
            ShowMessage("درخواست شما ذخیره شده و برای کارمند واحد عضویت ارسال شد. لطفا جهت پیگیری به منوی عضویت>مدیریت درخواست ها مراجعه نمائید");
            PgMode.Value = Utility.EncryptQS("View");
            // RoundPanelMain.Enabled = false;
            disableControls();
            RoundPanelMain.HeaderText = "مشاهده";
            btnSave.Enabled = btnSave2.Enabled = false;


        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

        //  CheckColor(_MeId);
    }

    int InsertMemberRequest(int MeId, TSP.DataManager.MemberRequestManager MemberRequestManager, int WfCurrentTaskId, TSP.DataManager.TransactionManager TransactionManager)
    {        
        int MReId = -1;
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        TransactionManager.Add(transferManager);
        ReqManager.FindLastReqByMemberId(_MeId, 0, 1);
        if (ReqManager.Count <= 0) return -1;
        int PreMReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        DataRow dr = MemberRequestManager.NewRow();
        dr["Email"] = txtEmail.Text.Trim();
        dr["MobileNo"] = txtMobile.Text.Trim();

        dr["SSN"] = txtSSN.Text.Trim();
        dr["HomeAdr"] = txtHomeAdr.Text;

        dr["MeId"] = _MeId;
        dr["BankAccNo"] = ReqManager[0]["BankAccNo"];
        dr["MeNo"] = ReqManager[0]["MeNo"];
        dr["FirstName"] = ReqManager[0]["FirstName"];
        dr["LastName"] = ReqManager[0]["LastName"];
        dr["FirstNameEn"] = ReqManager[0]["FirstNameEn"];
        dr["LastNameEn"] = ReqManager[0]["LastNameEn"];
        dr["ArchitectorCode"] = ReqManager[0]["ArchitectorCode"];
        dr["HomeTel"] = ReqManager[0]["HomeTel"];
        dr["HomePO"] = ReqManager[0]["HomePO"];
        dr["WorkAdr"] = ReqManager[0]["WorkAdr"];
        dr["WorkTel"] = ReqManager[0]["WorkTel"];
        dr["FaxNo"] = ReqManager[0]["FaxNo"];
        dr["WorkPO"] = ReqManager[0]["WorkPO"];
        dr["Website"] = ReqManager[0]["Website"];
        dr["FatherName"] = ReqManager[0]["FatherName"];
        dr["BirhtDate"] = ReqManager[0]["BirhtDate"];
        dr["BirthPlace"] = ReqManager[0]["BirthPlace"];
        dr["IdNo"] = ReqManager[0]["IdNo"];
        dr["IssuePlace"] = ReqManager[0]["IssuePlace"];
        dr["AgentId"] = ReqManager[0]["AgentId"];
        dr["NezamKardanConfirmURL"] = ReqManager[0]["NezamKardanConfirmURL"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SexId"]))
            dr["SexId"] = ReqManager[0]["SexId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MarId"]))
            dr["MarId"] = ReqManager[0]["MarId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SoId"]))
            dr["SoId"] = ReqManager[0]["SoId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["CitId"]))
            dr["CitId"] = ReqManager[0]["CitId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ComId"]))
            dr["ComId"] = ReqManager[0]["ComId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MsId"]))
            dr["MsId"] = ReqManager[0]["MsId"];

        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SignUrl"]))
            dr["SignUrl"] = ReqManager[0]["SignUrl"].ToString();

        dr["IsCreated"] = (int)TSP.DataManager.MemberRequestType.ChangeBaseInfo;
        dr["InActive"] = 0;
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["Requester"] = (int)TSP.DataManager.MembershipRequest.Member;
        dr["IsConfirm"] = (int)TSP.DataManager.MemberConfirmType.Pending;//معلق
        if (_MeImgUpload == null)
            dr["ImageUrl"] = imgMember.ImageUrl;
        else
        {
            dr["ImageUrl"] = _MeImgUpload;
            imgMember.ImageUrl = _MeImgUpload;
        }
        dr["WFCurrentTaskId"] = WfCurrentTaskId;
        MemberRequestManager.AddRow(dr);
        if (MemberRequestManager.Save() <= 0)
            return -1;
        MemberRequestManager.DataTable.AcceptChanges();
        MReId = int.Parse(MemberRequestManager[0]["MReId"].ToString());


        #region InsertTransfer//اگر در درخواست قبل اطلاعات انتقالی داشته است بایستی مجددا در این درخواست نیز ثبت شود

        transferManager.FindByMemberId(PreMReId, -1);
        if (transferManager.Count > 0)
        {
            DataRow drTransfer = transferManager.NewRow();
            drTransfer["PrId"] = transferManager[0]["PrId"].ToString();
            drTransfer["TransferDate"] = transferManager[0]["TransferDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["TransferType"]))
                drTransfer["TransferType"] = transferManager[0]["TransferType"];
            drTransfer["TableId"] = MReId;
            drTransfer["TtId"] = transferManager[0]["TtId"];
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["Body"]))
                drTransfer["Body"] = transferManager[0]["Body"].ToString();
            drTransfer["IsConfirmed"] = 0;
            drTransfer["MeNo"] = transferManager[0]["MeNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["FileNo"]))
                drTransfer["FileNo"] = transferManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["DocPrId"]))
                drTransfer["DocPrId"] = transferManager[0]["DocPrId"].ToString();

            if (!Utility.IsDBNullOrNullValue(transferManager[0]["FirstDocRegDate"]))
                drTransfer["FirstDocRegDate"] = transferManager[0]["FirstDocRegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocRegDate"]))
                drTransfer["CurrentDocRegDate"] = transferManager[0]["CurrentDocRegDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocExpDate"]))
                drTransfer["CurrentDocExpDate"] = transferManager[0]["CurrentDocExpDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(transferManager[0]["ImageUrl"]))
                drTransfer["ImageUrl"] = transferManager[0]["ImageUrl"].ToString();
            drTransfer["UserId"] = Utility.GetCurrentUser_UserId();
            drTransfer["ModifiedDate"] = DateTime.Now;

            transferManager.AddRow(drTransfer);
            if (transferManager.Save() <= 0)
                return -1;
            
        }
        #endregion        
        return MReId;
    }

    bool InsertWorkFlow(int MReId, int MeId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.MemberRequestManager MemberRequestManager)
    {
        int TaskId = -1;
        int CurrentUserId = Utility.GetCurrentUser_UserId();
        int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId;
        String Description1 = "آغاز گردش کار اتوماتیک سیستم جهت تغییر اطلاعات شخص حقیقی";
        String Description2 = "ارسال اتوماتیک درخواست به مرحله تایید کارمند واحد عضویت";
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming);
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        if (WorkFlowStateManager.InsertWorkFlowState(TableType, MReId, TaskId, Description1, MeId, NmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) <= 0)
            return false;

        WorkFlowStateManager.DataTable.AcceptChanges();

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        if (WorkFlowStateManager.InsertWorkFlowState(TableType, MReId, TaskId, Description2, MeId, NmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) <= 0)
            return false;

        if (MemberRequestManager.UpdateRequestTaskId(MReId, TaskId) != 0)
            return false;
        return true;
    }
    #endregion

    #region Images

    protected string SaveImageMember(UploadedFile uploadedFile)
    {
        string ret = "";
        //   string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + "MeId_" + _MeId.ToString() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/Person/Request/") + ret) == true);
            string tempFileName = "";// MapPath("~/Image/Temp/") + ret;
            string tempFileNameSave = tempFileName = "~/Image/Members/Person/Request/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileNameSave), true);
            Utility.FixedSize(MapPath(tempFileName), MapPath(tempFileNameSave), Utility.HorRes, Utility.VerRes);
            _MeImgUpload = tempFileNameSave;
        }
        return ret;
    }

    protected string SaveImageIdNo(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + "MeId_" + _MeId.ToString() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Members/IdNo/Request/") + ret) == true);
            string tempFileName = "~/Image/Members/IdNo/Request/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            if (id == "flpIdNo")
                _MeImageIdNo = tempFileName;
            if (id == "flpIdNoP2")
                _MeImageIdNoP2 = tempFileName;
            if (id == "flpIdNoPDes")
                _MeImageIdNoPDes = tempFileName;


        }
        return ret;
    }

    protected string SaveImageSSN(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + "MeId_" + _MeId.ToString() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/SSN/Request/") + ret) == true);
            string tempFileName = "~/image/Members/SSN/Request/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            if (id == "flpSSN")
                _MeImageSSN = tempFileName;

            if (id == "flpSSNBack")
                _MeImageSSNBack = tempFileName;

        }
        return ret;
    }

    protected string SaveImageSoldier(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + "MeId_" + _MeId.ToString() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Soldier/Request/") + ret) == true);
            string tempFileName = "~/image/Members/Soldier/Request/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            if (id == "flpSoldier")
                _MeImageSol = tempFileName;
            if (id == "flpSoldierBack")
                _MeImageSolBack = tempFileName;
        }
        return ret;
    }

    protected string SaveImageResident(UploadedFile uploadedFile, string id)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + "MeId_" + _MeId.ToString() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Resident/Request/") + ret) == true);
            string tempFileName = "~/image/Members/Resident/Request/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _MeImageRes = tempFileName;

        }
        return ret;
    }
    #endregion

    protected void FillForm()
    {
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        memberManager.FindByCode(_MeId);
        if (memberManager.Count > 0)
        {

            txtMobile.Text = memberManager[0]["MobileNo"].ToString();
            txtEmail.Text = memberManager[0]["Email"].ToString();
            txtSSN.Text = memberManager[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(memberManager[0]["HomeAdr"]))
                txtHomeAdr.Text = memberManager[0]["HomeAdr"].ToString();
            int MReId = -2;

            ReqManager.FindByMemberId(_MeId, 0, 1, -1);
            if (ReqManager.Count > 0)
                MReId = Convert.ToInt32(ReqManager[0]["MReId"]);


            if (!Utility.IsDBNullOrNullValue(memberManager[0]["SexId"]))
            {
                int SexId = Convert.ToInt32(memberManager[0]["SexId"]);
                if (SexId == (int)TSP.DataManager.SexManager.Sex.Male)
                {
                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                    if (attachManager.Count > 0)
                    {
                        HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
                        HiddenFieldPage.Set("MeSol", 1);
                    }

                    attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                    if (attachManager.Count > 0)
                    {
                        HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                        HiddenFieldPage.Set("SolBack", 1);
                    }

                    lblSolFile.ClientVisible = true;
                    flpSoldier.ClientVisible = true;

                    lblSolFileBack.ClientVisible = true;
                    flpSoldierBack.ClientVisible = true;
                }
                else
                {
                    lblSolFile.ClientVisible = false;
                    flpSoldier.ClientVisible = false;
                    HpSoldier.ClientVisible = false;
                    HiddenFieldPage.Set("MeSol", 1); //جهت چک کردن ولیدیشن ها در صفحه 

                    lblSolFileBack.ClientVisible = false;
                    flpSoldierBack.ClientVisible = false;
                    HpSoldierBack.ClientVisible = false;
                }

            }

            if (!Utility.IsDBNullOrNullValue(memberManager[0]["ImageUrl"]))
            {
                imgMember.ImageUrl = memberManager[0]["ImageUrl"].ToString();
                HiddenFieldPage.Set("FlpMe", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("MeIdNo", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
            if (attachManager.Count > 0)
            {
                HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("IdNoP2", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
            if (attachManager.Count > 0)
            {
                HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("IdNoPDes", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
            if (attachManager.Count > 0)
            {
                HpResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("Resident", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("MeSSN", 1);
            }

            attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);

            if (attachManager.Count > 0)
            {
                hssnBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                HiddenFieldPage.Set("SSNBack", 1);
            }

            //  CheckColor(_MeId);
        }
        else
        {
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است");
            return;
        }
    }

    //protected void CheckColor(int MeId)
    //{
    //    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
    //    TSP.DataManager.AttachmentsManager attachmentsManager = new TSP.DataManager.AttachmentsManager();
    //    MeManager.FindByCode(MeId);
    //    if (MeManager.Count > 0)
    //    {
    //        int MReId = -1;
    //        TSP.DataManager.MemberRequestManager memberRequestManager = new TSP.DataManager.MemberRequestManager();
    //        memberRequestManager.FindByMemberId(MeId, 1, -1);
    //        if (memberRequestManager.Count > 0)
    //            MReId = Convert.ToInt32(memberRequestManager[0]["MReId"].ToString());

    //        if (MReId != -1)
    //        {
    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
    //            if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HpIdNo.ForeColor = Color.Red;
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HpIdNo.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HpIdNo.ForeColor = Color.Red;
    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpIdNo.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HpIdNo.ForeColor = Color.Red;
    //            }


    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
    //            if (!Utility.IsDBNullOrNullValue(HIdNoP2.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HIdNoP2.ForeColor = Color.Red;
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HIdNoP2.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HIdNoP2.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HIdNoP2.ForeColor = Color.Red;
    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HIdNoP2.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HIdNoP2.ForeColor = Color.Red;
    //            }

    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
    //            if (!Utility.IsDBNullOrNullValue(HIdNoPDes.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HIdNoPDes.ForeColor = Color.Red;
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HIdNoPDes.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HIdNoPDes.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HIdNoPDes.ForeColor = Color.Red;
    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HIdNoPDes.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HIdNoPDes.ForeColor = Color.Red;
    //            }

    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
    //            if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HpSSN.ForeColor = Color.Red;
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HpSSN.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HpSSN.ForeColor = Color.Red;
    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSSN.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HpSSN.ForeColor = Color.Red;
    //            }



    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
    //            if (!Utility.IsDBNullOrNullValue(hssnBack.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                hssnBack.ForeColor = Color.Red;
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(hssnBack.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (hssnBack.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    hssnBack.ForeColor = Color.Red;
    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(hssnBack.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                hssnBack.ForeColor = Color.Red;
    //            }



    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
    //            if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HpSoldier.ForeColor = Color.Red;

    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HpSoldier.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HpSoldier.ForeColor = Color.Red;

    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSoldier.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HpSoldier.ForeColor = Color.Red;

    //            }


    //            attachmentsManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
    //            if (!Utility.IsDBNullOrNullValue(HpSoldierBack.NavigateUrl) && attachmentsManager.Count == 0)
    //            {
    //                HpSoldierBack.ForeColor = Color.Red;

    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSoldierBack.NavigateUrl) && !Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                if (HpSoldierBack.NavigateUrl != attachmentsManager[0]["FilePath"].ToString())
    //                {
    //                    HpSoldierBack.ForeColor = Color.Red;

    //                }
    //            }
    //            else if (!Utility.IsDBNullOrNullValue(HpSoldierBack.NavigateUrl) && Utility.IsDBNullOrNullValue(attachmentsManager[0]["FilePath"]))
    //            {
    //                HpSoldierBack.ForeColor = Color.Red;

    //            }


    //        }
    //        if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]) && !Utility.IsDBNullOrNullValue(imgMember.ImageUrl))
    //        {
    //            if (MeManager[0]["ImageUrl"].ToString() != imgMember.ImageUrl)
    //            {
    //                imgMember.Border.BorderColor = Color.Red;

    //            }
    //        }
    //        else if (!Utility.IsDBNullOrNullValue(imgMember.ImageUrl))
    //        {
    //            imgMember.Border.BorderColor = Color.Red;

    //        }
    //        //if (drdAgent.Value != null && !Utility.IsDBNullOrNullValue(MeManager[0]["AgentId"]))
    //        //{
    //        //    if (drdAgent.Value.ToString() != MeManager[0]["AgentId"].ToString())
    //        //    {
    //        //        drdAgent.ForeColor = Color.Red;

    //        //    }
    //        //}
    //        //else if (drdAgent.Value != null)
    //        //{
    //        //    drdAgent.ForeColor = Color.Red;

    //        //}
    //        if (txtEmail.Text != MeManager[0]["Email"].ToString())
    //        {
    //            txtEmail.ForeColor = Color.Red;
    //        }

    //        if (txtSSN.Text != MeManager[0]["SSN"].ToString())
    //        {
    //            txtSSN.ForeColor = Color.Red;
    //        }
    //    }
    //}

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion
}