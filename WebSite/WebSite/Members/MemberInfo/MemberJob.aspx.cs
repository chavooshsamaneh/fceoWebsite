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

public partial class Members_MemberInfo_MemberJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["FillMeJob"] = null;

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
            catch (Exception)
            { }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                Page_Load_TempMember();
            else
                Page_Load_Member();

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

        if (Session["FillMeJob"] != null)
        {
            Grid_DataBind((DataTable)Session["FillMeJob"]);
        }
        else
            FillGrid();
    }

    void Page_Load_Member()
    {
        String MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        switch (Utility.DecryptQS(HDMode.Value))
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
                    //TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                    //MeManager.FindByCode(int.Parse(MeId));
                    if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                    {
                        Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), -1, 1, 0, -1);
                    }
                    else
                    {
                        Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), -1, -1, 0, -1);
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

                // Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType);

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
                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType);
                else
                    Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType, 2);

                break;
        }
    }

    void Page_Load_TempMember()
    {
        String MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager == null || MeManager.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        switch (Utility.DecryptQS(HDMode.Value))
        {
            case "Request":

                SetMenuItem();

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                // Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType);

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
                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                {
                    JobManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                    Session["FillMeJob"] = JobManager.DataTable;
                }
                else
                {
                    JobManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                    Session["FillMeJob"] = JobManager.DataTable;
                }
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
        Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("New2") + "&Mode=" + HDMode.Value + "&JhId=" + Utility.EncryptQS(""));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int JhId = -1;
            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeJob"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeJob"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                //JhId = (int)row["JhId"];

                if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
                    JhId = (int)row["JhId"];
                else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                    JhId = (int)row["TMJhId"];
            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("View2") + "&JhId=" + Utility.EncryptQS(JhId.ToString()) + "&Mode=" + HDMode.Value);
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int JhId = -1;
        int MReId = -1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            if (Session["FillMeJob"] != null)
            {
                Grid_DataBind((DataTable)Session["FillMeJob"]);
            }
            else
                FillGrid();

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            JhId = (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers) ? (int)row["TMJhId"] : (int)row["JhId"];
            MReId = (int)row["TableId"];

        }
        if (JhId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();
                JobManager.FindByCode(JhId);
                if (JobManager.Count == 1)
                {
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    if (MReId == CurrentMReId)
                    {

                        if (CheckPermitionForEdit(MReId))
                            Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("Edit2") + "&JhId=" + Utility.EncryptQS(JhId.ToString()) + "&Mode=" + HDMode.Value);


                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                    }


                }
            }
            else
            {
                TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
                JobManager.FindByCode(JhId);
                if (JobManager.Count == 1)
                {
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    if (MReId == CurrentMReId)
                    {

                        if (CheckPermitionForEdit(MReId))
                            Response.Redirect("MemberJobInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("Edit2") + "&JhId=" + Utility.EncryptQS(JhId.ToString()) + "&Mode=" + HDMode.Value);


                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                    }


                }
            }
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int JhId = -1;
            int MReId = -1;
            string InActiveName = "";

            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeJob"] != null)
                {
                    Grid_DataBind((DataTable)Session["FillMeJob"]);
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                JhId = (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers) ? (int)row["TMJhId"] : (int)row["JhId"];
                MReId = (int)row["TableId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {

                if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                {
                    TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();

                    JobManager.FindByCode(JhId);
                    if (JobManager.Count == 1)
                    {

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            JobManager[0].Delete();
                            JobManager.Save();
                            FillGrid();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد";

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                return;
                            }
                            JobManager[0].Delete();
                            JobManager.Save();
                            //if (Convert.ToBoolean(JobManager[0]["InActive"]))
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            //    return;
                            //}
                            //else
                            //{
                            //    JobManager[0].BeginEdit();
                            //    JobManager[0]["InActive"] = 1;
                            //    JobManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            //    JobManager[0].EndEdit();
                            //}
                        }
                        CheckMenuImageCurrentPageTempMe(MeId, CurrentMReId);

                        FillGridTempMe();
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                    }
                }
                else
                {
                    TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();

                    JobManager.FindByCode(JhId);
                    if (JobManager.Count == 1)
                    {

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            JobManager[0].Delete();
                            JobManager.Save();
                            FillGrid();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد";

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                return;
                            }
                            InsertInActive(JhId, CurrentMReId, MeId, JobManager);
                            //if (Convert.ToBoolean(JobManager[0]["InActive"]))
                            //{
                            //    this.DivReport.Visible = true;
                            //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            //    return;
                            //}
                            //else
                            //{
                            //    JobManager[0].BeginEdit();
                            //    JobManager[0]["InActive"] = 1;
                            //    JobManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            //    JobManager[0].EndEdit();
                            //}
                        }
                        CheckMenuImageCurrentPage(MeId, CurrentMReId);
                        FillGridMember();
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
                    }
                }

            }
        }
        catch (Exception err)
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
                    if (e.GetValue("TableId") == null)
                        return;
                    string CurretnMReId = e.GetValue("TableId").ToString();
                    if (MReId == CurretnMReId)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        switch (e.Item.Name)
        {
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
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
    protected void InsertInActive(int JhId, int MReId, int MeId, TSP.DataManager.ProjectJobHistoryManager JhManager)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = JhId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.ProjectJobHistory;
        dr["ReqId"] = MReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        Session["FillMeJob"] = JhManager.FindByMeRequest(MeId, -1, -1, 0, -1, 2);
        Grid_DataBind((DataTable)Session["FillMeJob"]);

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "ذخیره انجام شد";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartCorporateDate" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartCorporateDate" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";


    }

    #region Methods
    protected void FillGrid()
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            FillGridTempMe();
        else
            FillGridMember();
    }
    protected void FillGridMember()
    {
        TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Home":

                TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                MeManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
                if (MeManager == null || MeManager.Count == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                    return;
                }
                if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                {
                    Grid_DataBind(JobManager.FindByMeRequest(int.Parse(MeId), -1, 1, 0, -1));
                }
                else
                {
                    Grid_DataBind(JobManager.FindByMeRequest(int.Parse(MeId), -1, -1, 0, -1));
                }

                break;
            case "Request":

                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());


                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Grid_DataBind(JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType));
                else
                    Grid_DataBind(JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType, 2));

                break;

            //case "New":

            //    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1,0);
            //    CustomAspxDevGridView1.DataBind();

            //    break;

            //case "View":

            //    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1,0);
            //    CustomAspxDevGridView1.DataBind();

            //    break;
        }
        Session["FillMeJob"] = CustomAspxDevGridView1.DataSource;

    }
    protected void FillGridTempMe()
    {
        TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Request":

                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());


                ReqManager.FindByCode(int.Parse(MReId));
                JobManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Grid_DataBind(JobManager.DataTable);
                break;

            //case "New":

            //    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1,0);
            //    CustomAspxDevGridView1.DataBind();

            //    break;

            //case "View":

            //    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1,0);
            //    CustomAspxDevGridView1.DataBind();

            //    break;
        }
        Session["FillMeJob"] = CustomAspxDevGridView1.DataSource;

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

    protected void CheckMenuImage(int MeId, int MReId)
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
    protected void CheckMenuImageTempMe(int MeId, int MReId)
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
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        ProjectJobHistoryManager.FindByMeRequest(MeId, MReId, -1, 0, (int)TSP.DataManager.TableCodes.MemberRequest);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (ProjectJobHistoryManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "";
                arr[1] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (ProjectJobHistoryManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "";
                arr[1] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }
    protected void CheckMenuImageCurrentPageTempMe(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();
        ProjectJobHistoryManager.FindByRequest(MeId, MReId);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (ProjectJobHistoryManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "";
                arr[1] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (ProjectJobHistoryManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "";
                arr[1] = 0;

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
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                CheckMenuImageTempMe(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
            else
                CheckMenuImage(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
        }
    }

    void Grid_DataBind(DataTable DataSource)
    {
        CustomAspxDevGridView1.DataSource = DataSource;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            CustomAspxDevGridView1.KeyFieldName = "JhId";
        else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CustomAspxDevGridView1.KeyFieldName = "TMJhId";
        CustomAspxDevGridView1.DataBind();
    }
    #endregion
}
