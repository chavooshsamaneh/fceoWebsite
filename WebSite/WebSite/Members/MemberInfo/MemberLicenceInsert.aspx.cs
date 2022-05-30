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

public partial class Members_MemberInfo_MemberLicenceInsert : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
    string _PageMode
    {
        get
        {
            return HiddenFieldLicence["PgMode"].ToString(); //PgMode.Value;
        }
        set
        {
            // PgMode.Value
            HiddenFieldLicence["PgMode"] = value;
        }
    }

    int _MeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldLicence["MeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            }
        }
        set
        {
            //  MemberId.Value
            HiddenFieldLicence["MeId"] = value.ToString();
        }
    }

    int _MReId
    {
        get
        {
            // return Convert.ToInt32(MemberRequest.Value);
            try
            {
                return Convert.ToInt32(HiddenFieldLicence["MReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
            }
        }
        set
        {
            //  MemberRequest.Value
            HiddenFieldLicence["MReId"] = value.ToString();
        }
    }

    int _MLId
    {
        get
        {
            // return Convert.ToInt32(LicenceId.Value);
            return Convert.ToInt32(HiddenFieldLicence["MlId"]);
        }
        set
        {
            //    LicenceId.Value
            HiddenFieldLicence["MlId"] = value.ToString();
        }
    }

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["License"] = null;

            Session["IsEdited_MeMadrak"] = false;
            HiddenFieldUniValue.Set("Id", null);
            OdbCountry.CacheDuration = Utility.GetCacheDuration();
            ComboCountry.DataBind();
            ComboCountry.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (ComboCountry.Value != null)
        {
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = ComboCountry.Value.ToString();
            ComboUniversity.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Request.QueryString["Mode"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_IsLock())
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.LockMemberType.Member, 1);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;

        }

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        #region CheckCondition
        if (cmbLicenceType.Value.ToString() == "1")
        {
            int LiId = Convert.ToInt32(drdLicence.Value);
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
                LicenceManager.FindByCode(LiId);
                if (LicenceManager.Count == 1)
                {
                    int LicenceCode = Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
                    if (LicenceCode == (int)TSP.DataManager.Licence.kardani)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "مدرک کاردانی را نمی توان به عنوان پیش فرض انتخاب کرد";
                        return;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
                    return;
                }
            }
            else
            {
                TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
                LicenceManager.FindByCode(LiId);
                if (LicenceManager.Count == 1)
                {
                    int LicenceCode = Convert.ToInt32(LicenceManager[0]["LicenceCode"]);
                    if (LicenceCode == (int)TSP.DataManager.Licence.kardani)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "مدرک کاردانی را نمی توان به عنوان پیش فرض انتخاب کرد";
                        return;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است";
                    return;
                }
            }
        }
        #endregion
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                if (_MLId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                Edit(_MLId);
                break;

        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MReId = -1;
        string Mode = Utility.DecryptQS(HDMode.Value);

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
            LiManager.FindByCode(_MLId);
            if (LiManager.Count > 0)
            {
                MReId = Convert.ToInt32(LiManager[0]["MReId"]);
            }
        }
        else
        {
            TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
            LiManager.FindByCode(_MLId);
            if (LiManager.Count > 0)
            {
                MReId = Convert.ToInt32(LiManager[0]["MReId"]);
            }
        }

        if (MReId == _MReId)
        {
            if (!CheckPermitionForEdit(MReId))
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
                return;
            }
        }
        else
        {
            ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
            return;
        }
        SetEditMode();
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
            ODBMajor.SelectParameters["InActive"].DefaultValue = "0";
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
    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #region FillForm

    protected void FillForm(int MlId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            FillFormTempMember(MlId);
        else
            FillFormMember(MlId);
    }

    protected void FillFormMember(int MlId)
    {
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();

        LiManager.FindByCode(MlId);
        if (LiManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            return;
        }
        if (Utility.IsDBNullOrNullValue(LiManager[0]["Avg"]) == false)
            txtAvg.Text = Convert.ToDecimal(LiManager[0]["Avg"]).ToString("##.00");
        txtDescription.Text = LiManager[0]["Description"].ToString();
        txtEndDate.Text = LiManager[0]["EndDate"].ToString();
        txtNumUnit.Text = LiManager[0]["NumUnit"].ToString();
        txtStartDate.Text = LiManager[0]["StartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Thesis"]))
            txtThesis.Text = LiManager[0]["Thesis"].ToString();
        if (Convert.ToBoolean(LiManager[0]["DefaultValue"]))
            cmbLicenceType.SelectedIndex = 0;
        else
            cmbLicenceType.SelectedIndex = 1;

        drdLicence.DataBind();
        drdLicence.SelectedIndex = drdLicence.Items.IndexOfValue(LiManager[0]["LiId"].ToString());
        ODBMajor.SelectParameters["MjId"].DefaultValue = LiManager[0]["MjParent"].ToString();
        ODBMajor.SelectParameters["InActive"].DefaultValue = "0";
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = cmbMajor.Items.IndexOfValue(LiManager[0]["MjId"].ToString());
        cmbParentMajor.DataBind();
        cmbParentMajor.SelectedIndex = cmbParentMajor.Items.IndexOfValue(LiManager[0]["MjParent"].ToString());
        if (LiManager[0]["CounId"] != DBNull.Value)
        {
            ComboCountry.DataBind();
            ComboCountry.SelectedIndex = ComboCountry.Items.IndexOfValue(LiManager[0]["CounId"].ToString());
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = LiManager[0]["CounId"].ToString();
        }
        else
        {
            ComboCountry.SelectedIndex = -1;
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = "-1";
        }

        if (LiManager[0]["UnId"] != DBNull.Value)
        {
            ComboUniversity.DataBind();
            try
            {
                ComboUniversity.SelectedIndex = ComboUniversity.Items.FindByValue(LiManager[0]["UnId"].ToString()).Index;
            }
            catch { }
        }
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CitName"]))
        {
            txtCity.Text = LiManager[0]["CitName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["ImageURL"]))
        {
            HpLicense.ClientVisible = true;
            HpLicense.NavigateUrl = LiManager[0]["ImageURL"].ToString();
            HDFlpLicense.Set("name", 1);
        }
        else
        {
            HpLicense.ClientVisible = false;
            HDFlpLicense.Set("name", 0);
        }
    }

    protected void FillFormTempMember(int MlId)
    {
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();

        LiManager.FindByCode(MlId);
        if (LiManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            return;
        }
        if (Utility.IsDBNullOrNullValue(LiManager[0]["Avg"]) == false)
            txtAvg.Text = Convert.ToDecimal(LiManager[0]["Avg"]).ToString("##.00");
        txtDescription.Text = LiManager[0]["Description"].ToString();
        txtEndDate.Text = LiManager[0]["EndDate"].ToString();
        txtNumUnit.Text = LiManager[0]["NumUnit"].ToString();
        txtStartDate.Text = LiManager[0]["StartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["Thesis"]))
            txtThesis.Text = LiManager[0]["Thesis"].ToString();
        //txtCity.Text = LiManager[0]["CitName"].ToString();
        if (Convert.ToBoolean(LiManager[0]["DefaultValue"]))
            cmbLicenceType.SelectedIndex = 0;
        else
            cmbLicenceType.SelectedIndex = 1;

        drdLicence.DataBind();
        drdLicence.SelectedIndex = drdLicence.Items.IndexOfValue(LiManager[0]["LiId"].ToString());


        ODBMajor.SelectParameters["MjId"].DefaultValue = LiManager[0]["MjParent"].ToString();
        ODBMajor.SelectParameters["InActive"].DefaultValue = "0";
        cmbMajor.DataBind();
        cmbMajor.SelectedIndex = cmbMajor.Items.IndexOfValue(LiManager[0]["MjId"].ToString());
        cmbParentMajor.DataBind();
        cmbParentMajor.SelectedIndex = cmbParentMajor.Items.IndexOfValue(LiManager[0]["MjParent"].ToString());
        if (LiManager[0]["CounId"] != DBNull.Value)
        {
            ComboCountry.DataBind();
            ComboCountry.SelectedIndex = ComboCountry.Items.IndexOfValue(LiManager[0]["CounId"].ToString());
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = LiManager[0]["CounId"].ToString();
        }
        else
        {
            ComboCountry.SelectedIndex = -1;
            ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = "-1";
        }

        if (LiManager[0]["UnId"] != DBNull.Value)
        {
            ComboUniversity.DataBind();
            ComboUniversity.SelectedIndex = ComboUniversity.Items.FindByValue(LiManager[0]["UnId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(LiManager[0]["CitName"]))
        {
            txtCity.Text = LiManager[0]["CitName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(LiManager[0]["ImageURL"]))
        {
            HpLicense.ClientVisible = true;
            HpLicense.NavigateUrl = LiManager[0]["ImageURL"].ToString();
            HDFlpLicense.Set("name", 1);
        }
        else
        {
            HpLicense.ClientVisible = false;
            HDFlpLicense.Set("name", 0);
        }

    }

    #endregion

    #region Insert-Update
    protected void Insert()
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            InsertTempMember();
        else
            InsertMember();
    }

    protected void InsertMember()
    {

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();


        trans.Add(LiManager);
        trans.Add(LiManager2);
        trans.Add(MemberRequestManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            trans.BeginSave();
            #region CheckCondition
            LiManager2.FindByMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                            LiManager2.DataTable.AcceptChanges();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == cmbMajor.Value.ToString()) && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }
            #endregion
            DataRow dr = LiManager.NewRow();

            if (drdLicence.Value != null)
                dr["LiId"] = int.Parse(drdLicence.Value.ToString());
            if (ComboCountry.Value != null)
                dr["CounId"] = int.Parse(ComboCountry.Value.ToString());
            else
                dr["CounId"] = DBNull.Value;
            dr["MeId"] = _MeId;
            if (cmbMajor.Value != null)
                dr["MjId"] = int.Parse(cmbMajor.Value.ToString());
            if (ComboUniversity.Value != null)
                dr["UnId"] = ComboUniversity.Value;
            dr["CitName"] = txtCity.Text;
            dr["CitId"] = DBNull.Value;

            if (txtAvg.Text != "")
                dr["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                dr["NumUnit"] = int.Parse(txtNumUnit.Text);
            dr["StartDate"] = txtStartDate.Text;
            dr["EndDate"] = txtEndDate.Text;
            dr["IsConfirm"] = 0;
            dr["IsInquiry"] = 0;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["Thesis"] = txtThesis.Text;
            dr["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = _MReId;
            if (Session["License"] != null)
            {
                dr["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.NavigateUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تصویر مدرک تحصیلی را انتخاب نمایید";
                return;
            }

            LiManager.AddRow(dr);

            if (LiManager.Save() <= 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int TableId = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "ثبت مدرک تحصیلی توسط عضو حقیقی", Utility.GetCurrentUser_UserId(), _MeId, 1);
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }

            //----------------   UpdateMeNo  -----------------//
            int MemberRequestId = _MReId;
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, trans);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, trans);

            Session["IsEdited_MeMadrak"] = true;

            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            _MLId = Convert.ToInt32(LiManager[0]["MlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";

            if (Session["MenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_MeId, _MReId);
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
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

    protected void InsertTempMember()
    {

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberLicenceManager LiManager2 = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);


        trans.Add(LiManager);
        trans.Add(LiManager2);
        trans.Add(WorkFlowStateManager);
        try
        {
            trans.BeginSave();

            LiManager2.FindByTMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == cmbMajor.Value.ToString()) && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }
            DataRow dr = LiManager.NewRow();

            if (drdLicence.Value != null)
                dr["LiId"] = int.Parse(drdLicence.Value.ToString());
            if (ComboCountry.Value != null)
                dr["CounId"] = int.Parse(ComboCountry.Value.ToString());
            else
                dr["CounId"] = DBNull.Value;
            dr["TMeId"] = _MeId;
            if (cmbMajor.Value != null)
                dr["MjId"] = int.Parse(cmbMajor.Value.ToString());
            if (ComboUniversity.Value != null)
                dr["UnId"] = ComboUniversity.Value;
            dr["CitName"] = txtCity.Text;
            dr["CitId"] = DBNull.Value;
            if (txtAvg.Text != "")
                dr["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                dr["NumUnit"] = int.Parse(txtNumUnit.Text);
            dr["StartDate"] = txtStartDate.Text;
            dr["EndDate"] = txtEndDate.Text;
            dr["IsConfirm"] = 0;
            dr["IsInquiry"] = 0;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["Thesis"] = txtThesis.Text;
            dr["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            dr["Description"] = txtDescription.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = _MReId;
            if (Session["License"] != null)
            {
                dr["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.NavigateUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;

            }
            LiManager.AddRow(dr);

            if (LiManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int TableId = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "ثبت مدرک تحصیلی توسط عضو حقیقی", Utility.GetCurrentUser_UserId(), _MeId, 1);
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }
            Session["IsEdited_MeMadrak"] = true;            

            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
            _MLId = Convert.ToInt32(LiManager[0]["TMlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";

            if (Session["MenuArrayList"] != null)
            {
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
            }
            else
            {
                CheckMenuImage(_MeId, _MReId);
                ArrayList arr = (ArrayList)Session["MenuArrayList"];
                arr[0] = 1;
                Session["MenuArrayList"] = arr;
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

    int FindMaxLiId_Member(int NewValue, TSP.DataManager.MemberLicenceManager LiManager)
    {
        LiManager.FindByMeId(_MeId);

        int Max = NewValue;
        for (int i = 0; i < LiManager.Count; i++)
        {
            if (Max < int.Parse(LiManager[i]["LiId"].ToString()))
                Max = int.Parse(LiManager[i]["LiId"].ToString());
        }
        return Max;
    }

    int FindMaxLiId_TempMember(int NewValue, TSP.DataManager.TempMemberLicenceManager LiManager)
    {
        LiManager.FindByTMeId(_MeId);

        int Max = NewValue;
        for (int i = 0; i < LiManager.Count; i++)
        {
            if (Max < int.Parse(LiManager[i]["LiId"].ToString()))
                Max = int.Parse(LiManager[i]["LiId"].ToString());
        }
        return Max;
    }

    protected void Edit(int MlId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            EditTempMember(MlId);
        else
            EditMember(MlId);
    }

    protected void EditMember(int MlId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(LiManager);
        trans.Add(LiManager2);

        try
        {
            trans.BeginSave();
            #region CheckCondition
            LiManager2.FindByMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true && Convert.ToInt32(LiManager2[i]["MlId"]) != MlId)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == cmbMajor.Value.ToString()) && Convert.ToInt32(LiManager2[i]["MlId"]) != MlId && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }
            #endregion
            LiManager.FindByCode(MlId);
            LiManager[0].BeginEdit();
            if (drdLicence.Value != null)
                LiManager[0]["LiId"] = int.Parse(drdLicence.Value.ToString());
            LiManager[0]["MeId"] = _MeId;
            if (cmbMajor.Value != null)
                LiManager[0]["MjId"] = int.Parse(cmbMajor.Value.ToString());
            if (ComboUniversity.Value != null)
                LiManager[0]["UnId"] = ComboUniversity.Value;

            LiManager[0]["Thesis"] = txtThesis.Text;
            LiManager[0]["CitName"] = txtCity.Text;
            if (txtAvg.Text != "")
                LiManager[0]["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                LiManager[0]["NumUnit"] = int.Parse(txtNumUnit.Text);
            LiManager[0]["StartDate"] = txtStartDate.Text;
            LiManager[0]["EndDate"] = txtEndDate.Text;


            LiManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            LiManager[0]["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            LiManager[0]["Description"] = txtDescription.Text;
            LiManager[0]["ModifiedDate"] = DateTime.Now;
            if (Session["License"] != null)
            {
                LiManager[0]["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.NavigateUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else if (Utility.IsDBNullOrNullValue(HpLicense.NavigateUrl))
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }
            LiManager[0].EndEdit();
            if (LiManager.Save() != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int TableId = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "ویرایش مدرک تحصیلی توسط عضو حقیقی", Utility.GetCurrentUser_UserId(), _MeId, 1);
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }
            trans.EndSave();
            _MLId = Convert.ToInt32(LiManager[0]["MlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            Session["IsEdited_MeMadrak"] = true;

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

    protected void EditTempMember(int MlId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLicenceManager LiManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberLicenceManager LiManager2 = new TSP.DataManager.TempMemberLicenceManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(LiManager);
        trans.Add(LiManager2);

        try
        {
            trans.BeginSave();
            LiManager2.FindByTMeId(_MeId);
            if (LiManager2.Count > 0)
            {
                if (cmbLicenceType.Value.ToString() == "1")
                {
                    for (int i = 0; i < LiManager2.Count; i++)
                    {
                        if (Convert.ToBoolean(LiManager2[i]["DefaultValue"]) == true && Convert.ToInt32(LiManager2[i]["TMlId"]) != MlId)
                        {
                            LiManager2[i].BeginEdit();
                            LiManager2[i]["DefaultValue"] = 0;
                            LiManager2[i]["UserId"] = Utility.GetCurrentUser_UserId();
                            LiManager2[i].EndEdit();
                            LiManager2.Save();
                        }
                    }
                }
                for (int i = 0; i < LiManager2.Count; i++)
                {
                    if ((LiManager2[i]["LiId"].ToString() == drdLicence.Value.ToString()) && (LiManager2[i]["MjId"].ToString() == cmbMajor.Value.ToString()) && Convert.ToInt32(LiManager2[i]["TMlId"]) != MlId && LiManager2[i]["InActiveName"].ToString() == "فعال")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            LiManager.FindByCode(MlId);
            LiManager[0].BeginEdit();
            if (drdLicence.Value != null)
                LiManager[0]["LiId"] = int.Parse(drdLicence.Value.ToString());

            LiManager[0]["TMeId"] = _MeId;
            if (cmbMajor.Value != null)
                LiManager[0]["MjId"] = int.Parse(cmbMajor.Value.ToString());
            if (ComboUniversity.Value != null)
                LiManager[0]["UnId"] = ComboUniversity.Value;

            LiManager[0]["Thesis"] = txtThesis.Text;
            LiManager[0]["CitName"] = txtCity.Text;
            if (txtAvg.Text != "")
                LiManager[0]["Avg"] = float.Parse(txtAvg.Text);
            if (txtNumUnit.Text != "")
                LiManager[0]["NumUnit"] = int.Parse(txtNumUnit.Text);
            LiManager[0]["StartDate"] = txtStartDate.Text;
            LiManager[0]["EndDate"] = txtEndDate.Text;


            LiManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            LiManager[0]["DefaultValue"] = Convert.ToInt32(cmbLicenceType.Value);

            LiManager[0]["Description"] = txtDescription.Text;
            LiManager[0]["ModifiedDate"] = DateTime.Now;

            if (Session["License"] != null)
            {
                LiManager[0]["ImageURL"] = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.NavigateUrl = "~/Image/Members/License/" + Path.GetFileName(Session["License"].ToString());
                HpLicense.ClientVisible = true;
                Session["License"] = null;
            }
            else if (Utility.IsDBNullOrNullValue(HpLicense.NavigateUrl))
            {
                trans.CancelSave();
                ShowMessage("تصویر مدرک تحصیلی را انتخاب نمایید");
                return;
            }

            LiManager[0].EndEdit();
            if (LiManager.Save() != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_MeMadrak"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLicence;
                int TableId = _MReId;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "ویرایش مدرک تحصیلی توسط عضو حقیقی", Utility.GetCurrentUser_UserId(), _MeId, 1);
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }


            trans.EndSave();
            _MLId = Convert.ToInt32(LiManager[0]["TMlId"]);
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            Session["IsEdited_MeMadrak"] = true;
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
    #endregion

    #region SetKey
    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["MeId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode2"]))
        {
            Response.Redirect("~/Members/MemberHome.aspx");
        }
        try
        {
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode2"].ToString());
            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
            _MLId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MlId"].ToString()));
            _MReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }

        string Mode = Utility.DecryptQS(HDMode.Value);


        if (string.IsNullOrEmpty(_PageMode) || string.IsNullOrEmpty(Mode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        switch (_PageMode)
        {

            case "New":
                SetNewMode();
                break;

            case "View":
                if (_MLId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                HpLicense.ClientVisible = true;

                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                SetEnabled(false);
                FillForm(_MLId);

                break;
            case "Edit":
                SetEditMode();
                break;
        }

        switch (Mode)
        {
            case "Home":
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnNew.Enabled = false;
                btnNew2.Enabled = false;
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                break;

            case "Request":
                TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                if (_MLId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                CheckWorkFlowPermission();

                ReqManager.FindByCode(_MReId);
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || (ReqManager[0]["IsConfirm"].ToString() != "0"))
                    {
                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                    }
                }

                break;
        }

    }

    private void SetNewMode()
    {

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        _PageMode = "New";
        _MLId = -1;
        ASPxRoundPanel2.HeaderText = "جدید";
        SetEnabled(true);
        ClearForm();
        //HDFlpLicense.Set("name", 0);
        HpLicense.ClientVisible = false;
        //HpLicense.NavigateUrl = "";
        //txtUniNameSearch.Text = "";
    }

    private void SetEditMode()
    {
        if (_MLId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetEnabled(true);
        FillForm(_MLId);
        _PageMode = "Edit";
        ASPxRoundPanel2.HeaderText = "ویرایش";
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void SetEnabled(Boolean Enable)
    {
        // ASPxRoundPanel2.Enabled = true;
        txtAvg.Enabled =
        txtCity.Enabled =
        txtDescription.Enabled =
       ComboUniversity.Enabled =
        txtEndDate.Enabled =
        txtNumUnit.Enabled =
        txtStartDate.Enabled =
        txtThesis.Enabled =
        drdLicence.Enabled =
        cmbMajor.ClientEnabled =
        cmbLicenceType.Enabled =
        ComboCountry.Enabled =
        flpLicense.Visible =
        cmbParentMajor.ClientEnabled = Enable;
    }

    protected void ClearForm()
    {
        drdLicence.SelectedIndex =
        ComboCountry.SelectedIndex =
        ComboUniversity.SelectedIndex = -1;
        SetIranForCountry();
        txtCity.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtNumUnit.Text = "";
        txtAvg.Text = "";
        txtThesis.Text = "";
        txtDescription.Text = "";
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        txtCity.Text = String.Empty;
        ObjectDataSourceSearchUniversity.SelectParameters["CounId"].DefaultValue = "-2";
        HDFlpLicense.Set("name", 0);
        cmbLicenceType.SelectedIndex = -1;
        cmbMajor.ClientEnabled = false;
        cmbMajor.SelectedIndex = -1;
        cmbParentMajor.SelectedIndex = -1;
        SetCmbMajor();
    }

    private void SetCmbMajor()
    {
        if (cmbParentMajor.SelectedIndex > -1)
        {
            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            int ParentId = Convert.ToInt32(cmbParentMajor.Value);
            ODBMajor.SelectParameters["MjId"].DefaultValue = ParentId.ToString();
            ODBMajor.SelectParameters["InActive"].DefaultValue = "0";
            cmbMajor.DataBind();
            cmbMajor.ClientEnabled = true;
        }
        else
        {
            cmbMajor.Text = "";
            cmbMajor.ClientEnabled = false;
        }
    }
    #endregion

    #region CheckPermission
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());

            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                return true;
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
            //string PageMode = Utility.DecryptQS(PgMode.Value);
            //CheckWorkFlowPermissionForSave(PageMode);
            if (_PageMode != "New")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        // string MeId = Utility.DecryptQS(MemberId.Value);
        //string MReId = Utility.DecryptQS(MemberRequest.Value);
        if (CheckPermitionForEdit(_MReId))
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
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;


    }
    #endregion

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = _MeId.ToString() + "lic" + _MReId.ToString() + "lic" + Path.GetRandomFileName() + ImageType.Extension;
                //Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/License/") + ret) == true);
            string tempFileName = MapPath("~/image/Members/License/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["License"] = ret;

        }
        return ret;
    }

    protected void CheckMenuImage(int MeId, int MReId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CheckMenuImageTempMember(MeId, MReId);
        else
            CheckMenuImageMember(MeId, MReId);
    }

    protected void CheckMenuImageMember(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberActivitySubjectManager.Count > 0)
        {
            arr[3] = 1;
        }
        MemberLanguageManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberLanguageManager.Count > 0)
        {
            arr[2] = 1;
        }

        MemberLicenceManager.FindByMeRequest(MeId, MReId, -1);
        if (MemberLicenceManager.Count > 0)
        {
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByMeRequest(MeId, MReId, -1, 0, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }

    protected void CheckMenuImageTempMember(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.TempMemberLanguageManager MemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLicenceManager MemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindByRequest(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            arr[3] = 1;
        }
        MemberLanguageManager.FindByRequest(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            arr[2] = 1;
        }

        MemberLicenceManager.FindByRequest(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByRequest(MeId, MReId);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }

    protected void SetIranForCountry()
    {
        ComboCountry.DataBind();
        ComboCountry.SelectedIndex = ComboCountry.Items.FindByValue(Utility.GetCurrentCounId().ToString()).Index;
    }
    #endregion

}
