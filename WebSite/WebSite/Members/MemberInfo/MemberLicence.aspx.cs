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
using System.Web.Services;




public partial class Members_MemberInfo_MemberLicence : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
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
            SetHelpAddress();
            Session["FillMeLicence"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");

            }
            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"].ToString());
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                Load_Page_TempMember(MeId, Mode);
            else
                Page_Load_Member(MeId, Mode);

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;


        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

        if (Session["FillMeLicence"] != null)
        {
            Grid_DataBind((DataTable)Session["FillMeLicence"]);
        }
        else
            FillGrid();
        if (!Utility.IsDBNullOrNullValue(Request["__EVENTTARGET"]) && Request["__EVENTTARGET"] == "btnInActive")
        {
            // Fire event
            btnInActive_Click(this, new EventArgs());
        }
    }

    void Page_Load_Member(String MeId, String Mode)
    {
        TSP.DataManager.MemberLicenceManager LicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        SetMenuItem();

        string MReId = Utility.DecryptQS(MemberRequest.Value);

        if (string.IsNullOrEmpty(MReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (!CheckPermitionForEdit(int.Parse(MReId)))
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnInActive.Enabled = false;
            btnInActive2.Enabled = false;
        }

        ReqManager.FindByCode(int.Parse(MReId));
        if (ReqManager.Count > 0)
        {
            if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;
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
            Session["FillMeLicence"] = LicenceManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, -1, 0);
        else
            Session["FillMeLicence"] = LicenceManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2, 0);
    }

    void Load_Page_TempMember(String MeId, String Mode)
    {
        TSP.DataManager.TempMemberLicenceManager LicenceManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
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

                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }

                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
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

                TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
                TempMemberLicenceManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Session["FillMeLicence"] = TempMemberLicenceManager.DataTable;
                break;
        }
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

    protected void CustomAspxDevGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLicenceInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("New") + "&Mode=" + HDMode.Value + "&MlId=" + Utility.EncryptQS("-1"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int MlId = -1;
            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeLicence"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                MlId = (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers) ? (int)row["TMlId"] : (int)row["MlId"];
            }
            if (MlId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Response.Redirect("MemberLicenceInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("View") + "&MlId=" + Utility.EncryptQS(MlId.ToString()) + "&Mode=" + HDMode.Value);
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MlId = -1;
        int MReId = -1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            if (Session["FillMeLicence"] != null)
            {
                Grid_DataBind((DataTable)Session["FillMeLicence"]);
            }
            else
                FillGrid();

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            MlId = (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers) ? (int)row["TMlId"] : (int)row["MlId"];
            MReId = (int)row["MReId"];
        }
        if (MlId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                TSP.DataManager.TempMemberLicenceManager LicenceManager = new TSP.DataManager.TempMemberLicenceManager();

                LicenceManager.FindByCode(MlId);
                if (LicenceManager.Count == 1)
                {
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    if (MReId == CurrentMReId)
                    {

                        if (CheckPermitionForEdit(MReId))
                            Response.Redirect("MemberLicenceInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("Edit") + "&MlId=" + Utility.EncryptQS(MlId.ToString()) + "&Mode=" + HDMode.Value);


                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد به توضیحات چگونگی انجام ویرایش دقت کنید.";
                    }


                }
            }
            else
            {
                TSP.DataManager.MemberLicenceManager LicenceManager = new TSP.DataManager.MemberLicenceManager();

                LicenceManager.FindByCode(MlId);
                if (LicenceManager.Count == 1)
                {
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    if (MReId == CurrentMReId)
                    {

                        if (CheckPermitionForEdit(MReId))
                            Response.Redirect("MemberLicenceInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("Edit") + "&MlId=" + Utility.EncryptQS(MlId.ToString()) + "&Mode=" + HDMode.Value);


                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد به توضیحات چگونگی انجام ویرایش دقت کنید.";
                    }

                }
            }
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
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;

        }
    }
    #endregion

    #region Methods


    void InActiveMember()
    {
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager == null || MemberManager.Count == 0)
        {
            this.DivReport.Visible = true;
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
            int MlId = -1;
            int MReId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeLicence"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                MlId = (int)row["MlId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
                return;
            }
            TSP.DataManager.MemberLicenceManager LicenceManager = new TSP.DataManager.MemberLicenceManager();
            LicenceManager.FindByCode(MlId);
            if (LicenceManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است.";
                return;
            }
            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            if (MReId == CurrentMReId)
            {
                LicenceManager[0].Delete();
                LicenceManager.Save();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                //LicenceManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);
                Session["FillMeLicence"] = LicenceManager.FindByMeRequest(MeId, MReId, -1, 2);
                Grid_DataBind((DataTable)Session["FillMeLicence"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                    return;
                }
                InsertInActive(MlId, CurrentMReId, MeId, LicenceManager);
            }
            CheckMenuImageCurrentPage(MeId, CurrentMReId);

        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }

    void InActiveTempMember()
    {
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager == null || MemberManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        try
        {
            int MlId = -1;
            int MReId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeLicence"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                MlId = (int)row["TMlId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                TSP.DataManager.TempMemberLicenceManager LicenceManager = new TSP.DataManager.TempMemberLicenceManager();

                LicenceManager.FindByCode(MlId);
                if (LicenceManager.Count == 1)
                {
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                    if (MReId == CurrentMReId)
                    {
                        LicenceManager[0].Delete();
                        LicenceManager.Save();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                        LicenceManager.FindByRequest(MeId, -1);
                        Session["FillMeLicence"] = LicenceManager.DataTable;
                        Grid_DataBind((DataTable)Session["FillMeLicence"]);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            return;
                        }
                        //InsertInActive(MlId, CurrentMReId, MeId, LicenceManager);

                        //if (Convert.ToBoolean(LicenceManager[0]["InActive"]))
                        //{
                        //    this.DivReport.Visible = true;
                        //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                        //    return;
                        //}
                        //else
                        //{
                        //    LicenceManager[0].BeginEdit();
                        //    LicenceManager[0]["InActive"] = 1;
                        //    LicenceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        //    LicenceManager[0].EndEdit();
                        //}
                    }
                    CheckMenuImageCurrentPage(MeId, CurrentMReId);


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                }


            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
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
        TSP.DataManager.MemberLicenceManager LicenceManager = new TSP.DataManager.MemberLicenceManager();
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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                    return;
                }
                if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                {
                    Grid_DataBind(LicenceManager.FindByMeRequest(int.Parse(MeId), -1, 1, -1, 0));
                }
                else
                {
                    Grid_DataBind(LicenceManager.FindByMeRequest(int.Parse(MeId), -1, -1, -1, 0));
                }


                break;

            case "Request":

                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Grid_DataBind(LicenceManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, -1, 0));
                else
                    Grid_DataBind(LicenceManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2, 0));
                break;

        }
        Session["FillMeLicence"] = CustomAspxDevGridView1.DataSource;

    }

    protected void FillGridTempMember()
    {
        TSP.DataManager.TempMemberLicenceManager LicenceManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Request":

                ReqManager.FindByCode(int.Parse(MReId));
                LicenceManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Grid_DataBind(LicenceManager.DataTable);
                break;

        }
        Session["FillMeLicence"] = CustomAspxDevGridView1.DataSource;

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

    protected void InsertInActive(int MlId, int MReId, int MeId, TSP.DataManager.MemberLicenceManager LicenceManager)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MlId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.MemberLicence;
        dr["ReqId"] = MReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();
        Session["FillMeLicence"] = LicenceManager.FindByMeRequest(MeId, MReId, -1, 2);
        Grid_DataBind((DataTable)Session["FillMeLicence"]);

        this.DivReport.Visible = true;
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
            CustomAspxDevGridView1.KeyFieldName = "MlId";
        else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CustomAspxDevGridView1.KeyFieldName = "TMlId";
        CustomAspxDevGridView1.DataBind();
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberLicence).ToString());
    }
    #endregion
}
