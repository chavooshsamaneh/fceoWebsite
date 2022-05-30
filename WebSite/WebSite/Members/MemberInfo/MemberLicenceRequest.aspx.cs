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
public partial class Members_MemberInfo_MemberLicenceRequest : System.Web.UI.Page
{
    DataTable dtMadrak = new DataTable();
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["License"] = null;
            Session["LicenseName"] = null;
            Session["TblOfMadrak"] = null;
            HiddenFieldUniValue.Set("Id", null);

            OdbCity.CacheDuration = Utility.GetCacheDuration();
            OdbCountry.CacheDuration = Utility.GetCacheDuration();
            ODBUniversity.CacheDuration = Utility.GetCacheDuration();

            ComboUniversity.DataBind();
            ComboUniversity.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            ComboCountry.DataBind();
            ComboCountry.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            SetIranForCountry();
            txtEndDate.Text = "";
            txtStartDate.Text = "";

            CreateTblOfMadrak();

            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            string PageMode = Utility.DecryptQS(PgMode.Value);
            int MeId = Utility.GetCurrentUser_MeId();
            switch (PageMode)
            {
                case "NewReq":
                    RoundPanelMain.Enabled = true;
                    RoundPanelMain.HeaderText = "درخواست جدید";
                    btnSave.Enabled = btnSave2.Enabled = true;
                    FillGrid(MeId);
                    break;
            }
        }

        if (Session["License"] != null && Session["LicenseName"] != null)
        {
            HpLicense.ClientVisible = true;
            HpLicense.NavigateUrl = Session["License"].ToString();
            HDFlpLicense.Set("name", 1);
            imgEndUploadImgIdNo.ClientVisible = true;
            lblImgErr.ClientVisible = false;
        }
        else
        {
            HpLicense.NavigateUrl = "";
        }



        if (ComboCountry.Value != null)
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
            ComboUniversity.DataBind();
        }
        SetCmbMajor();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (CheckHasKarshenasi() == false)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت حداقل یک مدرک تحصیلی با مقطع کارشناسی یا کارشناسی ناپیوسته الزامی می باشد";
            return;
        }

        if (!CheckKarshenasiNaPeyvasteCondition())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت مدرک کاردانی الزامی می باشد";
            return;
        }

        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode == "NewReq")
        {
            InsertNewRequest();
        }
    }

    void Refresh()
    {
        btnAdd.ClientEnabled = true;
        drdLicence.DataBind();
        drdLicence.SelectedIndex = -1;
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = -1;
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = -1;
        cmbParentMajor.DataBind();
        cmbParentMajor.SelectedIndex = -1;
        cmbMajor.ClientEnabled = false;

        txtCity.Text = "";
        txtNumUnit.Text = "";
        txtAvg.Text = "";
        txtThesis.Text = "";

        imgEndUploadImgIdNo.ClientVisible = false;
      //  cmbLicenceType.SelectedIndex = -1;
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        ComboCountry.DataBind();
        ComboCountry.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
        ObjectDataSourceSearchUniversity.SelectParameters["UnName"].DefaultValue = "%";

        SetIranForCountry();
        if (ComboCountry.Value != null)
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
        else
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = "-2";
        ComboUniversity.DataBind();
        ComboUniversity.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

        Session["LicenseName"] = null;
        Session["License"] = null;
        HDFlpLicense.Set("name", 0);
        lblImgErr.ClientVisible = false;
        HpLicense.ClientVisible = false;
        HiddenFieldUniValue.Set("Id", null);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCity.Text))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شهر را وارد نمایید";
            return;
        }
        int LicenceCode = -1;
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        int LiId = Convert.ToInt32(drdLicence.Value);
        LicenceManager.FindByCode(LiId);
        if (LicenceManager.Count == 1)
        {
            LicenceCode = Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
            return;
        }
        object Num = DBNull.Value;
        if (!string.IsNullOrEmpty(txtNumUnit.Text))
            Num = txtNumUnit.Text;

        object Avg = DBNull.Value;
        if (!string.IsNullOrEmpty(txtAvg.Text))
            Avg = txtAvg.Text;

        bool defaultValue = false;
        object UniId = DBNull.Value;

        string UniName = "";

        if (ComboUniversity.Value != null)
        {
            UniId = Convert.ToInt32(ComboUniversity.Value);           
        }
        object CitId = DBNull.Value;
        string CitName = "";
        CitName = txtCity.Text;

        object CounId = DBNull.Value;
        string CounName = "";
        if (ComboCountry.Value != null)
        {
            CounId = ComboCountry.Value;
            CounName = ComboCountry.SelectedItem.Text;
        }

        string LicenseUrl = "";
        if (Session["License"] != null )
        {
            LicenseUrl = "~/image/Members/License/" + Session["License"].ToString();
        }
        bool check = false;

        if (CustomAspxDevGridView1.VisibleRowCount > 0)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfMadrak"];
            CustomAspxDevGridView1.DataBind();

            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
                if ((dr["LiName"].ToString() == drdLicence.SelectedItem.Text) && (dr["MjName"].ToString() == cmbMajor.SelectedItem.Text))
                {
                    check = true;
                    break;
                }
            }
        }
        if (!check)
        {
            if (Session["TblOfMadrak"] == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired);
                return;
            }
            dtMadrak = (DataTable)Session["TblOfMadrak"];

            DataRow drLicence = dtMadrak.NewRow();
            drLicence["LiId"] = drdLicence.Value;
            drLicence["LiName"] = drdLicence.SelectedItem.Text;
            drLicence["MjId"] = cmbMajor.Value;
            drLicence["MjName"] = cmbMajor.SelectedItem.Text;
            drLicence["UnId"] = UniId;
            drLicence["UnName"] = UniName;
            drLicence["CounId"] = CounId;
            drLicence["CounName"] = CounName;
            drLicence["CitId"] = CitId;
            drLicence["CitName"] = CitName;
            drLicence["Avg"] = Avg;
            drLicence["NumUnit"] = Num;
            drLicence["StartDate"] = txtStartDate.Text;
            drLicence["EndDate"] = txtEndDate.Text;
            drLicence["Description"] = txtDescription.Text;
            drLicence["DefaultValue"] = defaultValue;
            drLicence["Thesis"] = txtThesis.Text;

            drLicence["LicenseUrl"] = LicenseUrl;
            drLicence["LicenceCode"] = LicenceCode;

            dtMadrak.Rows.Add(drLicence);
            Session["TblOfMadrak"] = dtMadrak;
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
            return;
        }

        CustomAspxDevGridView1.DataSource = dtMadrak;
        CustomAspxDevGridView1.DataBind();
        Refresh();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string UrlRef = Utility.DecryptQS(Request.QueryString["UrlRef"]);
        switch (UrlRef)
        {
            case "MemberRequest":
                Response.Redirect("MemberRequest.aspx?MeId=" + Utility.GetCurrentUser_MeId());
                break;
            case "MemberHome":
                Response.Redirect("~/Members/MemberHome.aspx");
                break;
        }
    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        dtMadrak = (DataTable)Session["TblOfMadrak"];
        dtMadrak.Rows.Find(e.Keys["Id"]).Delete();
        Session["TblOfMadrak"] = dtMadrak;
        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfMadrak"];
        CustomAspxDevGridView1.DataBind();
        dtMadrak = (DataTable)Session["TblOfMadrak"];
    }    

    protected void flpLicense_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void CallbackPanelMajor_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (cmbParentMajor.SelectedIndex <= -1)
        {
            cmbMajor.Text = "";
            cmbMajor.ClientEnabled = false;
            return;
        }
        string[] parameter = e.Parameter.Split(';');
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        if (parameter[0] == "cmbChange")
        {
            int ParentId = Convert.ToInt32(parameter[1]);
            ODBMajor.SelectParameters["MjId"].DefaultValue = ParentId.ToString();
            cmbMajor.DataBind();
            cmbMajor.ClientEnabled = true;
            cmbMajor.Text = "";
        }

        else if (parameter[0] == "ComboCountry")
        {
            int CountId = Convert.ToInt32(parameter[1]);
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = CountId.ToString();
            ComboUniversity.DataBind();
            ComboUniversity.ClientEnabled = true;
            ComboUniversity.SelectedIndex = 0;
        }
    }
    protected void ComboCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboCountry.Value != null)
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
            ComboUniversity.DataBind();
            ComboUniversity.ClientEnabled = true;
            ComboUniversity.SelectedIndex = 0;
        }
    }
    #endregion

    #region Methods
    void InsertNewRequest()
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        #region Check Condition
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            ShowMessage("امکان ثبت درخواست جدید برای اعضای موقت وجود ندارد");
            return;
        }
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
        {
            MeManager.FindByCode(Utility.GetCurrentUser_MeId());
            if ((bool)MeManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                ShowMessage("به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.");
                return;
            }
        }
        ReqManager.FindByMemberId(Utility.GetCurrentUser_MeId(), 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        #endregion

        if (Session["TblOfMadrak"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت حداقل یک مدرک تحصیلی الزامی می باشد";
            return;
        }

        dtMadrak = (DataTable)Session["TblOfMadrak"];

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(transact);
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        

        transact.Add(MemberLicenceManager);
        transact.Add(ReqManager);
        transact.Add(WorkFlowStateManager);
        //transact.Add(AttachManager);
        transact.Add(RequestInActivesManager);

      

        try
        {
            int MeId = Utility.GetCurrentUser_MeId();
            int UserId = Utility.GetCurrentUser_UserId();
            int MReId = -1;
            bool IsDocMajor = false;

            transact.BeginSave();

            MReId = InsertMemberRequest(MeId, ReqManager,WorkFlowTaskManager,transact);



            if (MReId == -1)
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است");
                return;
            }

            #region InsertMemberLicence
            DataRow[] DelRows = dtMadrak.Select(null, null, DataViewRowState.Deleted);
            DataRow[] InsertRows = dtMadrak.Select(null, null, DataViewRowState.Added);

            if (DelRows.Length <= 0 && InsertRows.Length <= 0)
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("در مدارک تحصیلی تغییری جهت ذخیره انجام نشده است");
                return;
            }

            #region Delete
            if (DelRows.Length > 0)
            {
                for (int i = 0; i < DelRows.Length; i++)
                {
                    int MlId = -1;
                    if (!Utility.IsDBNullOrNullValue(DelRows[i]["MlId", DataRowVersion.Original].ToString()))
                        MlId = int.Parse(DelRows[i]["MlId", DataRowVersion.Original].ToString());
                    else continue;

                    if (MlId == -1)
                    {
                        transact.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است";
                        return;
                    }

                    MemberLicenceManager.FindByCode(MlId);
                    if (MemberLicenceManager.Count > 0)
                    {
                        //بدلیل اینکه فقط مدارک فعال را نمایش داده می شود نیازی به چک کرد این مورد نمی باشد.
                        // در صورتی که مدرک در درخواست تایید نشده غیرفعال شده باشد با این کد مشک پیدا می کنیم
                        ////////////////////RequestInActivesManager.FindByTableIdTableType(MlId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberLicence), -1);
                        ////////////////////if (RequestInActivesManager.Count > 0)
                        ////////////////////{                            
                        ////////////////////    transact.CancelSave();
                        ////////////////////    this.DivReport.Visible = true;
                        ////////////////////    this.LabelWarning.Text = ("مدرک تحصیلی مورد نظر غیر فعال می باشد");
                        ////////////////////    return;
                        ////////////////////}

                        InsertInActive(RequestInActivesManager, MlId, MReId, MeId);
                        if (IsDocFileMajor(MlId, transact))
                        {
                            IsDocMajor = true;
                        }
                        //else
                        //{
                        //    this.DivReport.Visible = true;
                        //    this.LabelWarning.Text = ("ذخیره انجام شد");
                        //}
                    }
                }
            }
            #endregion

            #region Insert
            if (InsertRows.Length > 0)
            {
                for (int i = 0; i < InsertRows.Length; i++)
                {
                    if (Utility.IsDBNullOrNullValue(InsertRows[i]["MlId"]))
                    {
                        DataRow drLicence = MemberLicenceManager.NewRow();
                        drLicence["MeId"] = MeId;
                        drLicence["LiId"] = InsertRows[i]["LiId"];
                        drLicence["MjId"] = InsertRows[i]["MjId"];
                        drLicence["UnId"] = InsertRows[i]["UnId"];
                        drLicence["UnName"] = InsertRows[i]["UnName"];
                        drLicence["CounId"] = InsertRows[i]["CounId"];
                        drLicence["CitId"] = InsertRows[i]["CitId"];
                        drLicence["CitName"] = InsertRows[i]["CitName"];
                        drLicence["Avg"] =  InsertRows[i]["Avg"];
                        drLicence["NumUnit"] = InsertRows[i]["NumUnit"];
                        drLicence["StartDate"] = InsertRows[i]["StartDate"];
                        drLicence["EndDate"] = InsertRows[i]["EndDate"];
                        drLicence["Description"] = InsertRows[i]["Description"];
                        drLicence["DefaultValue"] = InsertRows[i]["DefaultValue"];
                        drLicence["Thesis"] = InsertRows[i]["Thesis"];
                        drLicence["IsConfirm"] = 0;
                        drLicence["IsInquiry"] = 0;
                        drLicence["UserId"] = UserId;
                        drLicence["MReId"] = MReId;
                        //ret = _MeId.ToString() + "lic" + _MReId.ToString() + "lic" + Path.GetRandomFileName() + ImageType.Extension;
                        drLicence["ImageURL"] =InsertRows[i]["LicenseUrl"].ToString();   
                        drLicence["ModifiedDate"] = DateTime.Now;
                        MemberLicenceManager.AddRow(drLicence);
                        if (MemberLicenceManager.Save() > 0)
                        {
                            MemberLicenceManager.DataTable.AcceptChanges();

                            int Id = Convert.ToInt32(InsertRows[i]["Id"]);
                            dtMadrak.Rows.Find(Id).BeginEdit();
                            dtMadrak.Rows.Find(Id)["MlId"] = MemberLicenceManager[MemberLicenceManager.Count - 1]["MlId"].ToString();
                            dtMadrak.Rows.Find(Id).EndEdit();
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است");
                            transact.CancelSave();
                            return;
                        }

                        //InsertLicenseAttachment(AttachManager, Convert.ToInt32(MemberLicenceManager[MemberLicenceManager.Count - 1]["MlId"])
                        //      , InsertRows[i]["LicenseUrl"].ToString(), InsertRows[i]["LicenseURLName"].ToString(), UserId);
                    }
                }
            }
            #endregion
            #endregion

            if (!InsertWorkFlow(MReId, MeId, WorkFlowStateManager, WorkFlowTaskManager))
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است");
                return;
            }

          

                ReqManager.FindByCode(MReId);
                if (ReqManager[0]["IsCreated"].ToString() == "1")
                    TSP.DataManager.MemberManager.UpdateMeNo(MeId, transact);
                else
                    TSP.DataManager.MemberRequestManager.UpdateMeNo(MReId, transact);
          

           
            transact.EndSave();

            if (IsDocMajor)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("ذخیره انجام شد. هشدار : رشته حذف شده در واحد پروانه به عنوان رشته پروانه شما استفاده شده است. لطفا مراتب را به واحد پروانه اعلام نمایید ");
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("درخواست شما ذخیره شده و برای کارمند واحد عضویت ارسال شد. لطفا جهت پیگیری به منوی عضویت>مدیریت درخواست ها مراجعه نمائید");
            }
            PgMode.Value = Utility.EncryptQS("View");
            RoundPanelMain.HeaderText = "مشاهده";
            RoundPanelMain.Enabled = false;
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات انجام گرفته است";
        }
    }

    //protected void InsertLicenseAttachment(TSP.DataManager.AttachmentsManager AttachManager, int MlId, string LicenseUrl, string LicenseUrlName, int UserId)
    //{
    //    DataRow drAtt = AttachManager.NewRow();
    //    drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberLicence;
    //    drAtt["RefTable"] = MlId;
    //    drAtt["AttId"] = (int)TSP.DataManager.AttachType.MemberLicense;
    //    drAtt["IsValid"] = 1;
    //    drAtt["FilePath"] = "~/Image/Members/License/" + LicenseUrl;
    //    drAtt["FileName"] = LicenseUrlName;
    //    drAtt["Description"] = DBNull.Value;
    //    drAtt["UserId"] = UserId;
    //    drAtt["ModfiedDate"] = DateTime.Now;
    //    AttachManager.AddRow(drAtt);
    //    AttachManager.Save();
    //    AttachManager.DataTable.AcceptChanges();

    //    try
    //    {
    //        string ImgSoource = Server.MapPath("~/image/Temp/") + LicenseUrl;// Path.GetFileName(LicenseUrl);
    //        string ImgTarget = Server.MapPath("~/Image/Members/License/") + LicenseUrl;// System.IO.Path.GetFileName(Session["KardFileURL"].ToString());
    //        System.IO.File.Move(ImgSoource, ImgTarget);
    //    }
    //    catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    //}

    int InsertMemberRequest(int MeId, TSP.DataManager.MemberRequestManager MemberRequestManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        int MReId = -1;
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        TransactionManager.Add(transferManager);
        ReqManager.FindLastReqByMemberId(MeId, 0, 1);
        if (ReqManager.Count <= 0) return -1;
        int PreMReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        DataRow dr = MemberRequestManager.NewRow();
        dr["WFCurrentTaskId"] = TaskId;
        dr["MeId"] = MeId;
        dr["AgentId"] = ReqManager[0]["AgentId"];
        dr["Email"] = ReqManager[0]["Email"];
        dr["MobileNo"] = ReqManager[0]["MobileNo"];
        dr["BankAccNo"] = ReqManager[0]["BankAccNo"];
        dr["MeNo"] = ReqManager[0]["MeNo"];
        dr["FirstName"] = ReqManager[0]["FirstName"];
        dr["LastName"] = ReqManager[0]["LastName"];
        dr["FirstNameEn"] = ReqManager[0]["FirstNameEn"];
        dr["LastNameEn"] = ReqManager[0]["LastNameEn"];
        dr["HomeAdr"] = ReqManager[0]["HomeAdr"];
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
        dr["NezamKardanConfirmURL"] = ReqManager[0]["NezamKardanConfirmURL"];        
        dr["SSN"] = ReqManager[0]["SSN"];

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

        dr["IsCreated"] = (int)TSP.DataManager.MemberRequestType.ChangeLicence;
        dr["InActive"] = 0;
        dr["UserId"] = Utility.GetCurrentUser_UserId();
     
        dr["ModifiedDate"] = DateTime.Now;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["Requester"] = (int)TSP.DataManager.MembershipRequest.Member;
        dr["IsConfirm"] = (int)TSP.DataManager.MemberConfirmType.Pending;//معلق
        dr["ImageUrl"] = ReqManager[0]["ImageUrl"];
        MemberRequestManager.AddRow(dr);
        if (MemberRequestManager.Save() <= 0) return -1;
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

    bool InsertWorkFlow(int MReId, int MeId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
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

        return true;
    }

    protected void InsertInActive(TSP.DataManager.RequestInActivesManager Manager, int MlId, int MReId, int MeId)
    {
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MlId;
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberLicence);
        dr["ReqId"] = MReId;
        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();
        Manager.DataTable.AcceptChanges();
    }

    DataTable CreateTblOfMadrak()
    {
        if (Session["TblOfMadrak"] == null)
        {
            dtMadrak.Columns.Add("MlId");
            dtMadrak.Columns.Add("LiId");
            dtMadrak.Columns.Add("MjId");
            dtMadrak.Columns.Add("UnId");
            dtMadrak.Columns.Add("UnName");
            dtMadrak.Columns.Add("CitId");
            dtMadrak.Columns.Add("CitName");
            dtMadrak.Columns.Add("StartDate");
            dtMadrak.Columns.Add("EndDate");
            dtMadrak.Columns.Add("NumUnit");
            dtMadrak.Columns.Add("Avg");
            dtMadrak.Columns.Add("Description");
            dtMadrak.Columns.Add("DefaultValue");
            dtMadrak.Columns.Add("CounId");
            dtMadrak.Columns.Add("Thesis");

            dtMadrak.Columns.Add("CounName");//show
            dtMadrak.Columns.Add("MjName");//show
            dtMadrak.Columns.Add("LiName");//show

            dtMadrak.Columns.Add("LicenseUrl");
            dtMadrak.Columns.Add("LicenseUrlName");
            dtMadrak.Columns.Add("LicenceCode");

            dtMadrak.Columns.Add("Id");
            dtMadrak.Columns["Id"].AutoIncrement = true;
            dtMadrak.Columns["Id"].AutoIncrementSeed = 1;
            dtMadrak.Constraints.Add("PK_ID", dtMadrak.Columns["Id"], true);


            Session["TblOfMadrak"] = dtMadrak;
        }

        return (DataTable)Session["TblOfMadrak"];
    }

    void FillGrid(int MeId)
    {
        if (Session["TblOfMadrak"] == null)
            Session["TblOfMadrak"] = CreateTblOfMadrak();
        dtMadrak = (DataTable)Session["TblOfMadrak"];

        TSP.DataManager.MemberLicenceManager LicManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dtLic = LicManager.SelectMemberLicence(MeId, -1, -1, 0, -1);
        if (dtLic.Rows.Count > 0)
        {
            for (int i = 0; i < dtLic.Rows.Count; i++)
            {
                DataRow row = dtMadrak.NewRow();
                row["MlId"] = dtLic.Rows[i]["MlId"];
                row["LiId"] = dtLic.Rows[i]["LiId"];
                row["MjId"] = dtLic.Rows[i]["MjId"];
                row["UnId"] = dtLic.Rows[i]["UnId"];
                row["CitId"] = dtLic.Rows[i]["CitId"];
                row["CitName"] = dtLic.Rows[i]["CitName"];
                row["StartDate"] = dtLic.Rows[i]["StartDate"];
                row["EndDate"] = dtLic.Rows[i]["EndDate"];
                row["NumUnit"] = dtLic.Rows[i]["NumUnit"];
                row["Avg"] = dtLic.Rows[i]["Avg"];
                row["Description"] = dtLic.Rows[i]["Description"];
                row["DefaultValue"] = dtLic.Rows[i]["DefaultValue"];
                row["CounId"] = dtLic.Rows[i]["CounId"];
                row["Thesis"] = dtLic.Rows[i]["Thesis"];
                row["UnName"] = dtLic.Rows[i]["UnName"];
                row["LicenceCode"] = dtLic.Rows[i]["LicenceCode"];

                row["LiName"] = dtLic.Rows[i]["LiName"];
                row["MjName"] = dtLic.Rows[i]["MjName"];
                row["CounName"] = dtLic.Rows[i]["CounName"];
                row["LicenseUrl"] = dtLic.Rows[i]["ImageURL"];
                dtMadrak.Rows.Add(row);
            }
            dtMadrak.AcceptChanges();
            Session["TblOfMadrak"] = dtMadrak;
        }

        CustomAspxDevGridView1.DataSource = dtMadrak;
        CustomAspxDevGridView1.DataBind();
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);                
                ret = Utility.GetCurrentUser_MeId().ToString() + "lic" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/License/") + ret) == true);
            string tempFileName = MapPath("~/image/Members/License/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["License"] = ret;
        }
        return ret;
    }

    int FindMaxLiId(int NewValue)
    {
        int Max = NewValue;
        for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
        {
            DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
            if (Max < int.Parse(dr["LiId"].ToString()))
                Max = int.Parse(dr["LiId"].ToString());
        }
        return Max;
    }

    int FindLicenceCode(int LiId)
    {
        //---Find LicenceCode
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        LicenceManager.FindByCode(LiId);
        if (LicenceManager.Count == 1)
            return Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
        else return -1;
    }

    protected void SetIranForCountry()
    {
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = ComboCountry.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberlicence).ToString());
    }

    bool IsDocFileMajor(int MlId, TSP.DataManager.TransactionManager transact)
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        transact.Add(DocMemberFileMajorManager);
        DocMemberFileMajorManager.FindByMlId(MlId, 0);
        if (DocMemberFileMajorManager.Count > 0)
            return true;
        else
            return false;
    }

    protected Boolean CheckHasKarshenasi()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];
        Boolean flag = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i].RowState != DataRowState.Deleted)
                if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.ArshadPeybaste || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.Karshenasi || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste || Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.MoadeleKarshenasi)
            {
                flag = true;
                break;
            }

        }

        return flag;
    }

    Boolean CheckDefaultMadrak()
    {
        DataTable dt = (DataTable)Session["TblOfMadrak"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i].RowState != DataRowState.Deleted)
                if (Convert.ToBoolean(dt.Rows[i]["DefaultValue"]) == true)
                    return true;
        }

        return false;
    }

    Boolean CheckKarshenasiNaPeyvasteCondition()
    {
        //int LicenceCode = Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
        //       if (LicenceCode == (int)TSP.DataManager.Licence.kardani)
        //       {
        DataTable dt = (DataTable)Session["TblOfMadrak"];
        Boolean IsNapeyvaste = false;
        Boolean HasKardani = false;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i].RowState != DataRowState.Deleted)
            {
                if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste)
                {
                    IsNapeyvaste = true;
                    break;
                }
            }
        }
        if (IsNapeyvaste)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (Convert.ToInt32(dt.Rows[i]["LicenceCode"]) == (int)TSP.DataManager.Licence.kardani)
                    {
                        HasKardani = true;
                        break;
                    }
                }
            }
        }
        if (IsNapeyvaste && !HasKardani)
            return false;
        return true;
    }

    private void SetCmbMajor()
    {
        if (cmbParentMajor.SelectedIndex > -1)
        {
            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            int ParentId = Convert.ToInt32(cmbParentMajor.Value);
            ODBMajor.SelectParameters["MjId"].DefaultValue = ParentId.ToString();
            cmbMajor.DataBind();
            cmbMajor.ClientEnabled = true;
        }
        else
        {
            cmbMajor.Text = "";
            cmbMajor.ClientEnabled = false;
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}