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

public partial class Members_MemberInfo_MemberLanguage : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

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
        this.DivReport.Visible = true;
        // Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        DivReport.Style["visibility"] = "hidden";

        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["IsEdited_MeLanguage"] = false;
            Session["FillMeLanguage"] = null;

            ViewState["PMode"] = "";

            if (string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");


            }
            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"].ToString());
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();

            }
            catch (Exception)
            { }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                Page_Load_TempMember();
            else
                Page_Load_Member();

            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;

        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (Session["FillMeLanguage"] != null)
        {
            Grid_DataBind((DataTable)Session["FillMeLanguage"]);
        }
        else
            FillGrid();
    }

    void Page_Load_Member()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);

        TSP.DataManager.MemberLanguageManager LanManager = new TSP.DataManager.MemberLanguageManager();

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        switch (Mode)
        {

            case "Home":
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnInActive.Enabled = false;
                btnInActive2.Enabled = false;
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;

                try
                {

                    if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                    {
                        Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), -1, 1);

                    }
                    else
                    {
                        Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), -1, -1);

                    }
                }
                catch (Exception)
                { }

                break;
            case "Request":

                SetMenuItem();

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                //  Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);

                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }

                TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee//answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew2.Enabled = false;
                        BtnNew.Enabled = false;
                        btnInActive.Enabled = false;
                        btnInActive2.Enabled = false;
                    }

                    if (Convert.ToInt32(ReqManager[0]["MsId"].ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                    {
                        BtnNew.Enabled = BtnNew2.Enabled = false;
                        btnEdit.Enabled = btnEdit2.Enabled = false;
                        btnInActive.Enabled = btnInActive2.Enabled = false;
                    }
                }
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                else
                    Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);
                break;
        }
    }

    void Page_Load_TempMember()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);

        TSP.DataManager.TempMemberLanguageManager LanManager = new TSP.DataManager.TempMemberLanguageManager();

        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        switch (Mode)
        {
            case "Request":

                SetMenuItem();

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                //  Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);

                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }

                TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee//answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew2.Enabled = false;
                        BtnNew.Enabled = false;
                        btnInActive.Enabled = false;
                        btnInActive2.Enabled = false;
                    }

                    if (Convert.ToInt32(ReqManager[0]["MsId"].ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                    {
                        BtnNew.Enabled = BtnNew2.Enabled = false;
                        btnEdit.Enabled = btnEdit2.Enabled = false;
                        btnInActive.Enabled = btnInActive2.Enabled = false;
                    }
                }
                TSP.DataManager.TempMemberLanguageManager TempMemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
                TempMemberLanguageManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Session["FillMeLicence"] = TempMemberLanguageManager.DataTable;
                break;
        }
    }
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming)
            {
                return true;
            }

        }
        return false;
    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Home")
        {
            Response.Redirect("~/Members/MemberHome.aspx?MeId=" + MemberId.Value);
        }
        else

            Response.Redirect("MemberRequestInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + Request.QueryString["PageMode"]);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
        }
        else
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            //if ((bool)MemberManager[0]["IsLock"] == true)
            if (Utility.GetCurrentUser_IsLock())
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.LockMemberType.Member, 1);
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return;

            }
        }

        string PageMode = Utility.DecryptQS(ViewState["PMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
            {

                Insert();

            }
            else if (PageMode == "Edit")
            {
                int MlanId = -1;
                if (CustomAspxDevGridView1.FocusedRowIndex > -1)
                {
                    FillGrid();
                    DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);

                    if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                        MlanId = (int)row["TMlanId"];
                    else
                        MlanId = (int)row["MlanId"];
                }

                if (string.IsNullOrEmpty(MlanId.ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(MlanId);
                }

            }

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
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLanguageManager MeLlanManager = new TSP.DataManager.MemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeLlanManager);

        try
        {

            DataRow dr = MeLlanManager.NewRow();

            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            if (drdLanName.Value != null)
                dr["LanId"] = int.Parse(drdLanName.Value.ToString());
            dr["MeId"] = MeId;
            if (drdLanQuality.Value != null)
                dr["LqId"] = int.Parse(drdLanQuality.Value.ToString());
            dr["UserId"] = Utility.GetCurrentUser_UserId();

            dr["Description"] = txtDesc.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = MReId;

            MeLlanManager.AddRow(dr);
            trans.BeginSave();
            int cnt = MeLlanManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                    int TableId = MReId;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                    Session["IsEdited_MeLanguage"] = true;
                    FillGrid();
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
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
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLanguageManager MeLlanManager = new TSP.DataManager.TempMemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeLlanManager);

        try
        {

            DataRow dr = MeLlanManager.NewRow();

            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            if (drdLanName.Value != null)
                dr["LanId"] = int.Parse(drdLanName.Value.ToString());
            dr["TMeId"] = MeId;
            if (drdLanQuality.Value != null)
                dr["LqId"] = int.Parse(drdLanQuality.Value.ToString());
            dr["UserId"] = Utility.GetCurrentUser_UserId();

            dr["Description"] = txtDesc.Text;
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = MReId;

            MeLlanManager.AddRow(dr);
            trans.BeginSave();
            int cnt = MeLlanManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                    int TableId = MReId;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                    Session["IsEdited_MeLanguage"] = true;
                    FillGrid();
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
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
    protected void Edit(int MlanId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            EditTempMember(MlanId);
        else
            EditMember(MlanId);
    }
    protected void EditMember(int MlanId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLanguageManager LnManager2 = new TSP.DataManager.MemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(LnManager);
        try
        {
            LnManager.FindByCode(MlanId);
            if (LnManager.Count == 1)
            {

                int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
                LnManager[0].BeginEdit();
                if (drdLanName.Value != null)
                    LnManager[0]["LanId"] = int.Parse(drdLanName.Value.ToString());
                if (drdLanQuality.Value != null)
                    LnManager[0]["LqId"] = int.Parse(drdLanQuality.Value.ToString());
                LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                LnManager[0]["Description"] = txtDesc.Text;
                LnManager[0]["ModifiedDate"] = DateTime.Now;
                LnManager[0].EndEdit();
                trans.BeginSave();
                int cnt = LnManager.Save();
                if (cnt > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                        int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                        Session["IsEdited_MeLanguage"] = true;
                        FillGrid();
                    }
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
            }
            else
            {
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

    }
    protected void EditTempMember(int MlanId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLanguageManager LnManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLanguageManager LnManager2 = new TSP.DataManager.TempMemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(LnManager);
        try
        {
            LnManager.FindByCode(MlanId);
            if (LnManager.Count == 1)
            {

                int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
                LnManager[0].BeginEdit();
                if (drdLanName.Value != null)
                    LnManager[0]["LanId"] = int.Parse(drdLanName.Value.ToString());
                if (drdLanQuality.Value != null)
                    LnManager[0]["LqId"] = int.Parse(drdLanQuality.Value.ToString());
                LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                LnManager[0]["Description"] = txtDesc.Text;
                LnManager[0]["ModifiedDate"] = DateTime.Now;
                LnManager[0].EndEdit();
                trans.BeginSave();
                int cnt = LnManager.Save();
                if (cnt > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                        int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);
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
                        Session["IsEdited_MeLanguage"] = true;
                        FillGrid();
                    }
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
            }
            else
            {
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

    }

    protected void CustomAspxDevGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (!string.IsNullOrEmpty(Mode))
        {
            if (Mode == "Request")
            {
                if (e.RowType != DevExpress.Web.GridViewRowType.Data)
                    return;
                if (MemberRequest.Value != null)
                {
                    string MReId = Utility.DecryptQS(MemberRequest.Value);
                    if (e.GetValue("MReId") == null)
                        return;
                    string CurretnMReId = e.GetValue("MReId").ToString();
                    if (MReId == CurretnMReId)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }
    }

    protected void FillGrid()
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            FillGridTempMember();
        else
            FillGridMember();
    }
    protected void FillGridMember()
    {

        TSP.DataManager.MemberLanguageManager LanManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Home":

                TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                MeManager.FindByCode(int.Parse(MeId));
                if (MeManager == null || MeManager.Count == 0)
                {
                    DivReport.Style["visibility"] = "block";
                    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                    return;
                }
                if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                {
                    Grid_DataBind(LanManager.FindByMeRequest(int.Parse(MeId), -1, 1));
                }
                else
                {
                    Grid_DataBind(LanManager.FindByMeRequest(int.Parse(MeId), -1, -1));
                }

                break;
            case "Request":
                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Grid_DataBind(LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1));
                else
                    Grid_DataBind(LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2));
                break;
        }

        Session["FillMeLanguage"] = CustomAspxDevGridView1.DataSource;

    }
    protected void FillGridTempMember()
    {

        TSP.DataManager.TempMemberLanguageManager LanManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Request":

                LanManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Grid_DataBind(LanManager.DataTable);
                break;
        }

        Session["FillMeLanguage"] = CustomAspxDevGridView1.DataSource;

    }
    protected void ClearForm()
    {
        txtDesc.Text = "";
        drdLanName.DataBind();
        drdLanName.SelectedIndex = -1;
        drdLanQuality.DataBind();
        drdLanQuality.SelectedIndex = -1;
    }
    //protected void BtnNew_Click(object sender, EventArgs e)
    //{

    //    btnSave.Visible = true;
    //    ViewState["PMode"] = Utility.EncryptQS("New");
    //    ASPxPopupControl1.HeaderText = "جدید";
    //    ClearForm();
    //}
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TSP.DataManager.MemberLanguageManager LanManager = new TSP.DataManager.MemberLanguageManager();
    //        string MeId = Utility.DecryptQS(MemberId.Value);

    //        if (string.IsNullOrEmpty(MeId))
    //        {
    //            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //            return;
    //        }

    //        else
    //        {
    //            int MlanId = -1;
    //            int MReId = -1;

    //            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
    //            {
    //                FillGrid();

    //                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
    //                MlanId = (int)row["MlanId"];
    //                MReId = (int)row["MReId"];

    //            }
    //            if (MlanId == -1)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

    //            }
    //            else
    //            {
    //                int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
    //                if (MReId == CurrentMReId)
    //                {
    //                    if (CheckPermitionForEdit(int.Parse(MeId)))
    //                    {
    //                        btnSave.Visible = true;
    //                        FillForm(MlanId);
    //                        ViewState["PMode"] = Utility.EncryptQS("Edit"); ;
    //                        ASPxPopupControl1.HeaderText = "ویرایش";
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
    //                    }

    //                }
    //                else
    //                {

    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
    //                }


    //            }

    //        }
    //    }
    //    catch (Exception)
    //    {

    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
    //    }
    //}
    protected void FillForm(int MlanId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            FillGridTempMember();
        else
            FillGridMember();
    }
    protected void FillFormMember(int MlanId)
    {
        TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();
        try
        {
            LnManager.FindByCode(MlanId);
            if (LnManager.Count > 0)
            {
                txtDesc.Text = LnManager[0]["Description"].ToString();
                drdLanName.DataBind();
                drdLanName.SelectedIndex = drdLanName.Items.IndexOfValue(LnManager[0]["LanId"].ToString());
                drdLanQuality.DataBind();
                drdLanQuality.SelectedIndex = drdLanQuality.Items.IndexOfValue(LnManager[0]["LqId"].ToString());
            }
            else
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
                return;

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
            return;
        }
    }
    protected void FillFormTempMember(int MlanId)
    {
        TSP.DataManager.TempMemberLanguageManager LnManager = new TSP.DataManager.TempMemberLanguageManager();
        try
        {
            LnManager.FindByCode(MlanId);
            if (LnManager.Count > 0)
            {
                txtDesc.Text = LnManager[0]["Description"].ToString();
                drdLanName.DataBind();
                drdLanName.SelectedIndex = drdLanName.Items.IndexOfValue(LnManager[0]["LanId"].ToString());
                drdLanQuality.DataBind();
                drdLanQuality.SelectedIndex = drdLanQuality.Items.IndexOfValue(LnManager[0]["LqId"].ToString());
            }
            else
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
                return;

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد.";
            return;
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            InActiveTempMember();
        else
            InActiveMember();
    }
    void InActiveMember()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(int.Parse(MeId));
        if (MemberManager == null || MemberManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if (Utility.GetCurrentUser_IsLock())
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.LockMemberType.Member, 1);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;

        }

        try
        {
            int MlanId = -1;
            int MReId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLanguage"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeLanguage"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                MlanId = (int)row["MlanId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlanId == -1)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();

                LnManager.FindByCode(MlanId);
                if (LnManager.Count == 1)
                {
                    try
                    {
                        //int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            LnManager[0].Delete();
                            LnManager.Save();
                            Session["FillMeLanguage"] = LnManager.FindByMeRequest(int.Parse(MeId), -1, -1, 2);
                            Grid_DataBind((DataTable)Session["FillMeLanguage"]);


                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "ذخیره انجام شد";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                            {
                                DivReport.Style["visibility"] = "block";
                                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                return;
                            }

                            InsertInActive(MlanId, CurrentMReId, int.Parse(MeId), LnManager);

                            //if (Convert.ToBoolean(LnManager[0]["InActive"]))
                            //{
                            //    DivReport.Style["visibility"] = "block";
                            //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            //    return;
                            //}
                            //else
                            //{
                            //    LnManager[0].BeginEdit();
                            //    LnManager[0]["InActive"] = 1;
                            //    LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            //    LnManager[0].EndEdit();
                            //}
                        }

                        CheckMenuImageCurrentPage(int.Parse(MeId), CurrentMReId);


                    }
                    catch (Exception err)
                    {
                        Utility.SaveWebsiteError(err);
                        DivReport.Style["visibility"] = "block";
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                    }

                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    void InActiveTempMember()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
        MemberManager.FindByCode(int.Parse(MeId));
        if (MemberManager == null || MemberManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        try
        {
            int MlanId = -1;
            int MReId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLanguage"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeLanguage"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                MlanId = (int)row["TMlanId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlanId == -1)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                TSP.DataManager.TempMemberLanguageManager LnManager = new TSP.DataManager.TempMemberLanguageManager();

                LnManager.FindByCode(MlanId);
                if (LnManager.Count == 1)
                {
                    try
                    {

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            LnManager[0].Delete();
                            LnManager.Save();
                            LnManager.FindByRequest(int.Parse(MeId), -1);
                            Session["FillMeLanguage"] = LnManager.DataTable;
                            Grid_DataBind((DataTable)Session["FillMeLanguage"]);


                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "ذخیره انجام شد";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                            {
                                DivReport.Style["visibility"] = "block";
                                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                return;
                            }

                            //InsertInActive(MlanId, CurrentMReId, MeId, LnManager);

                            //if (Convert.ToBoolean(LnManager[0]["InActive"]))
                            //{
                            //    DivReport.Style["visibility"] = "block";
                            //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            //    return;
                            //}
                            //else
                            //{
                            //    LnManager[0].BeginEdit();
                            //    LnManager[0]["InActive"] = 1;
                            //    LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            //    LnManager[0].EndEdit();
                            //}
                        }

                        CheckMenuImageCurrentPage(int.Parse(MeId), CurrentMReId);


                    }
                    catch (Exception err)
                    {
                        Utility.SaveWebsiteError(err);
                        DivReport.Style["visibility"] = "block";
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                    }

                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {
            int MlanId = -1;
            FillGrid();
            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                MlanId = (int)row["TMlanId"];
            else
                MlanId = (int)row["MlanId"];

            int MReId = (int)row["MReId"];


            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            if (MReId == CurrentMReId)
            {
                if (!CheckPermitionForEdit(MReId))
                {

                    e.Result = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    CustomAspxDevGridView1.JSProperties["cpShow"] = 0;

                }

            }
            else
            {
                btnSave.Visible = false;
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }

            FillGrid();
        }
    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            RowInsertingTempMember(e);
        else
            RowInsertingMember(e);
    }
    void RowInsertingTempMember(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLanguageManager MeLlanManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLanguageManager MeLlanManager2 = new TSP.DataManager.TempMemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeLlanManager);

        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            MeLlanManager2.FindByTMeId(MeId);

            for (int i = 0; i < MeLlanManager2.Count; i++)
            {
                if (MeLlanManager2[i]["LanId"].ToString() == e.NewValues["LanId"].ToString() && MeLlanManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    FillGrid();

                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }



            DataRow dr = MeLlanManager.NewRow();
            dr["LanId"] = int.Parse(e.NewValues["LanId"].ToString());
            dr["TMeId"] = MeId;
            dr["LqId"] = int.Parse(e.NewValues["LqId"].ToString());
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            MeLlanManager.AddRow(dr);
            trans.BeginSave();
            int cnt = MeLlanManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                    CustomAspxDevGridView1.CancelEdit();

                }
                else
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                    CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                    Session["IsEdited_MeLanguage"] = true;
                    FillGrid();

                }

            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                CustomAspxDevGridView1.CancelEdit();

                return;
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            CustomAspxDevGridView1.CancelEdit();
            Utility.SaveWebsiteError(err);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    void RowInsertingMember(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLanguageManager MeLlanManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLanguageManager MeLlanManager2 = new TSP.DataManager.MemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(MeLlanManager);

        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            MeLlanManager2.FindByMeId(MeId);

            for (int i = 0; i < MeLlanManager2.Count; i++)
            {
                if (MeLlanManager2[i]["LanId"].ToString() == e.NewValues["LanId"].ToString() && MeLlanManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    FillGrid();

                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }



            DataRow dr = MeLlanManager.NewRow();
            dr["LanId"] = int.Parse(e.NewValues["LanId"].ToString());
            dr["MeId"] = MeId;
            dr["LqId"] = int.Parse(e.NewValues["LqId"].ToString());
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            MeLlanManager.AddRow(dr);
            trans.BeginSave();
            int cnt = MeLlanManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                    CustomAspxDevGridView1.CancelEdit();

                }
                else
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                    CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                    Session["IsEdited_MeLanguage"] = true;
                    FillGrid();

                }

            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                CustomAspxDevGridView1.CancelEdit();

                return;
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            CustomAspxDevGridView1.CancelEdit();
            Utility.SaveWebsiteError(err);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            RowUpdatingTempMember(e);
        else
            RowUpdatingMember(e);
    }
    void RowUpdatingMember(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLanguageManager LnManager2 = new TSP.DataManager.MemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(LnManager);

        e.Cancel = true;

        int MlanId = int.Parse(e.Keys["MlanId"].ToString());
        //int MReId = int.Parse(e.NewValues["MReId"].ToString());
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        LnManager.FindByCode(MlanId);
        int MReId = int.Parse(LnManager[0]["MReId"].ToString());


        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        if (MReId == CurrentMReId)
        {
            if (CheckPermitionForEdit(MReId))
            {
                try
                {
                    LnManager.FindByCode(MlanId);
                    if (LnManager.Count == 1)
                    {

                        LnManager[0].BeginEdit();
                        LnManager[0]["LanId"] = int.Parse(e.NewValues["LanId"].ToString());
                        LnManager[0]["LqId"] = int.Parse(e.NewValues["LqId"].ToString());
                        LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        if (e.NewValues["Description"] != null)
                            LnManager[0]["Description"] = e.NewValues["Description"].ToString();
                        LnManager[0]["ModifiedDate"] = DateTime.Now;
                        LnManager[0].EndEdit();
                        trans.BeginSave();
                        int cnt = LnManager.Save();
                        if (cnt > 0)
                        {
                            int UpdateState = -1;
                            if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                            {
                                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                                int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                            }
                            if (UpdateState == -4)
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                                CustomAspxDevGridView1.CancelEdit();

                            }
                            else
                            {
                                trans.EndSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";

                                Session["IsEdited_MeLanguage"] = true;
                                FillGrid();
                            }
                        }
                        else
                        {
                            trans.CancelSave();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                            CustomAspxDevGridView1.CancelEdit();

                        }
                    }
                    else
                    {

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        CustomAspxDevGridView1.CancelEdit();

                    }
                    CustomAspxDevGridView1.CancelEdit();

                }
                catch (Exception err)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.CancelEdit();
                    Utility.SaveWebsiteError(err);

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 2601)
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                        }
                        else
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }
                }
            }
            else
            {
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
            }
        }
        else
        {
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        }
        FillGrid();
    }
    void RowUpdatingTempMember(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.TempMemberLanguageManager LnManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLanguageManager LnManager2 = new TSP.DataManager.TempMemberLanguageManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(LnManager);

        e.Cancel = true;

        int MlanId = int.Parse(e.Keys["TMlanId"].ToString());
        //int MReId = int.Parse(e.NewValues["MReId"].ToString());
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        LnManager.FindByCode(MlanId);
        int MReId = int.Parse(LnManager[0]["MReId"].ToString());


        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        if (MReId == CurrentMReId)
        {
            if (CheckPermitionForEdit(MReId))
            {
                try
                {
                    LnManager.FindByCode(MlanId);
                    if (LnManager.Count == 1)
                    {

                        LnManager[0].BeginEdit();
                        LnManager[0]["LanId"] = int.Parse(e.NewValues["LanId"].ToString());
                        LnManager[0]["LqId"] = int.Parse(e.NewValues["LqId"].ToString());
                        LnManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        if (e.NewValues["Description"] != null)
                            LnManager[0]["Description"] = e.NewValues["Description"].ToString();
                        LnManager[0]["ModifiedDate"] = DateTime.Now;
                        LnManager[0].EndEdit();
                        trans.BeginSave();
                        int cnt = LnManager.Save();
                        if (cnt > 0)
                        {
                            int UpdateState = -1;
                            if (!(Convert.ToBoolean(Session["IsEdited_MeLanguage"].ToString())))
                            {
                                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberLanguage;
                                int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                            }
                            if (UpdateState == -4)
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                                CustomAspxDevGridView1.CancelEdit();

                            }
                            else
                            {
                                trans.EndSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";

                                Session["IsEdited_MeLanguage"] = true;
                                FillGrid();
                            }
                        }
                        else
                        {
                            trans.CancelSave();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                            CustomAspxDevGridView1.CancelEdit();

                        }
                    }
                    else
                    {

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        CustomAspxDevGridView1.CancelEdit();

                    }
                    CustomAspxDevGridView1.CancelEdit();

                }
                catch (Exception err)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.CancelEdit();
                    Utility.SaveWebsiteError(err);

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 2601)
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                        }
                        else
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }
                }
            }
            else
            {
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
            }
        }
        else
        {
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        }
        FillGrid();
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;
            case "Member":
                if (Mode == "Home")
                {
                    Response.Redirect("~/Members/MemberHome.aspx?MeId=" + MemberId.Value);
                }
                else

                    Response.Redirect("MemberRequestInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + Request.QueryString["PageMode"]);

                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;

        }
    }
    protected void InsertInActive(int LanId, int MReId, int MeId, TSP.DataManager.MemberLanguageManager LnManager)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = LanId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.MemberLanguage;
        dr["ReqId"] = MReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();
        Session["FillMeLanguage"] = LnManager.FindByMeRequest(MeId, -1, -1, 2);
        Grid_DataBind((DataTable)Session["FillMeLanguage"]);

        DivReport.Style["visibility"] = "block";
        this.LabelWarning.Text = "ذخیره انجام شد";
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

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        //ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        //if (ProjectJobHistoryManager.Count > 0)
        //{
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
        //    arr[1] = 1;
        //}

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
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindByRequest(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindByRequest(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByRequest(MeId, MReId);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CheckMenuImageCurrentPageTempMember(MeId, MReId);
        else
            CheckMenuImageCurrentPageMember(MeId, MReId);
    }
    protected void CheckMenuImageCurrentPageMember(int MeId, int MReId)
    {
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        MemberLanguageManager.FindByMeRequest(MeId, MReId, -1);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLanguageManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "";
                arr[2] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLanguageManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "";
                arr[2] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }
    protected void CheckMenuImageCurrentPageTempMember(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberLanguageManager MemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
        MemberLanguageManager.FindByRequest(MeId, MReId);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLanguageManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "";
                arr[2] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLanguageManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
                arr[2] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "";
                arr[2] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }

    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
            Session["MenuArrayList"] = arr;
        }
        else
            CheckMenuImageCurrentPage(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
    }
    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }

        }
        else
        {
            CheckMenuImage(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));

        }
    }

    void Grid_DataBind(DataTable DataSource)
    {
        CustomAspxDevGridView1.DataSource = DataSource;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            CustomAspxDevGridView1.KeyFieldName = "MlanId";
        else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CustomAspxDevGridView1.KeyFieldName = "TMlanId";
        CustomAspxDevGridView1.DataBind();
    }
}
