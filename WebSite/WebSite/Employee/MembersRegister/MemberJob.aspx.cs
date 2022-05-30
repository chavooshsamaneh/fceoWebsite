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

public partial class Employee_MembersRegister_MemberJob : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
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

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["Mode"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }
            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            Session["FillMeJob"] = null;


            string Mode = Utility.DecryptQS(HDMode.Value);
            string MeId = Utility.DecryptQS(MemberId.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))//|| string.IsNullOrEmpty(MReId)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            CheckWorkFlowPermission();

            switch (Mode)
            {
                case "Request":
                    #region Request
                    SetMenuItem();
                    try
                    {
                        if (string.IsNullOrEmpty(MReId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        //ASPxMenu1.Items[0].Visible = false;
                        //ASPxMenu1.Items[1].Visible = false;
                        //ASPxMenu1.Items[6].Visible = false;


                        string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
                        string TPType = Utility.DecryptQS(ReqestMode);
                        if (!string.IsNullOrEmpty(TPType))
                        {
                            if (TPType == "0")//Menu
                            {
                                btnInActive.Enabled = false;
                                btnInActive2.Enabled = false;
                                BtnNew.Enabled = false;
                                BtnNew2.Enabled = false;
                                btnEdit.Enabled = false;
                                btnEdit2.Enabled = false;
                            }
                            else
                            {

                                ReqManager.FindByCode(int.Parse(MReId));
                                if (ReqManager.Count > 0)
                                {
                                    if ((ReqManager[0]["IsConfirm"].ToString() != "0"))// answer request
                                    {
                                        BtnNew.Enabled = false;
                                        BtnNew2.Enabled = false;
                                        btnInActive.Enabled = false;
                                        btnInActive2.Enabled = false;
                                        btnEdit.Enabled = false;
                                        btnEdit2.Enabled = false;

                                    }
                                }


                            }
                        }

                        int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());
                        // Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0,TableType);
                        if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                            Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType);
                        else
                            Session["FillMeJob"] = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType, 2);


                        //RequestMode.Value = Server.HtmlDecode(Request.QueryString["ReqMode"]).ToString();
                        //string ReqMode = Utility.DecryptQS(RequestMode.Value);
                        //if (string.IsNullOrEmpty(RequestMode))
                        //{
                        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        //}


                    }
                    catch (Exception err)
                    {
                    }

                    break;
                #endregion
                case "TempMe":
                    #region Request
                    SetMenuItem();
                    try
                    {
                        if (string.IsNullOrEmpty(MReId))
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }



                        string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
                        string TPType = Utility.DecryptQS(ReqestMode);
                        if (!string.IsNullOrEmpty(TPType))
                        {
                            if (TPType == "0")//Menu
                            {
                                btnInActive.Enabled = false;
                                btnInActive2.Enabled = false;
                                BtnNew.Enabled = false;
                                BtnNew2.Enabled = false;
                                btnEdit.Enabled = false;
                                btnEdit2.Enabled = false;
                            }
                            else
                            {

                                ReqManager.FindByCode(int.Parse(MReId));
                                if (ReqManager.Count > 0)
                                {
                                    //if ((Convert.ToBoolean(ReqManager[0]["Requester"]) == false) || (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member or answer request
                                    //{
                                    if ((ReqManager[0]["IsConfirm"].ToString() != "0"))// answer request
                                    {
                                        BtnNew.Enabled = false;
                                        BtnNew2.Enabled = false;
                                        btnInActive.Enabled = false;
                                        btnInActive2.Enabled = false;
                                        btnEdit.Enabled = false;
                                        btnEdit2.Enabled = false;

                                    }
                                }


                            }
                        }

                        TSP.DataManager.TempMemberJobHistoryManager TempMemberJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();
                        TempMemberJobHistoryManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                        Session["FillMeJob"] = TempMemberJobHistoryManager.DataTable;
                    }
                    catch (Exception err)
                    {
                    }

                    break;
                    #endregion
            }


            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;


        }
        if (Session["FillMeJob"] != null)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
            string Mode = Utility.DecryptQS(HDMode.Value);
            if (Mode == "Request")
                CustomAspxDevGridView1.KeyFieldName = "JhId";
            else if (Mode == "TempMe")
                CustomAspxDevGridView1.KeyFieldName = "TMJhId";
            CustomAspxDevGridView1.DataBind();
        }
        else
            FillGrid();


        string MemberName = FillMemberName();
        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = Session["FillMeJob"];
        Session["Title"] = "سوابق کاری";
        Session["Header"] = "نام : " + MemberName;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {

        Response.Redirect("MemberJobInsert.aspx?JhId=" + Utility.EncryptQS("") + "&aPageMode=" + Utility.EncryptQS("New") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int JhId = -1;
            int MReId = -1;
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeJob"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                string Mode = Utility.DecryptQS(HDMode.Value);
                if (Mode == "Request")
                    JhId = (int)row["JhId"];
                else if (Mode == "TempMe")
                    JhId = (int)row["TMJhId"];
                MReId = (int)row["TableId"];

            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                if (MReId == CurrentMReId)
                {
                    if (CheckPermitionForEdit(MReId))
                        Response.Redirect("MemberJobInsert.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&aPageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
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
        catch (Exception)
        {

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
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
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                string Mode = Utility.DecryptQS(HDMode.Value);
                if (Mode == "Request")
                    JhId = (int)row["JhId"];
                else if (Mode == "TempMe")
                    JhId = (int)row["TMJhId"];
            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Response.Redirect("MemberJobInsert.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string TempMe = "";
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        if (Mode == "Home")
        {
            Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        }
        else
            Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + Request.QueryString["MReId"] + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS("View")
                + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
         + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));

    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";

        switch (e.Item.Name)
        {
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                 + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        try
        {
            int JhId = -1;
            int MReId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeJob"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    JhId = (int)row["JhId"];
                else if (Mode == "TempMe")
                    JhId = (int)row["TMJhId"];
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
                if (Mode == "Request")
                {
                    #region MeJob
                    TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
                    TSP.DataManager.ProjectJobHistoryManager JhManager2 = new TSP.DataManager.ProjectJobHistoryManager();


                    JhManager.FindByCode(JhId);
                    if (JhManager.Count == 1)
                    {

                        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            JhManager[0].Delete();
                            JhManager.Save();
                            Session["FillMeJob"] = JhManager2.FindByMeRequest(MeId, -1, -1, 0, -1, 2);
                            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                            CustomAspxDevGridView1.KeyFieldName = "JhId";
                            CustomAspxDevGridView1.DataBind();

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
                            InsertInActive(JhId, CurrentMReId, MeId, JhManager2);
                        }
                        CheckMenuImageCurrentPage(MeId, CurrentMReId);
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                    }
                    #endregion
                }
                else if (Mode == "TempMe")
                {
                    #region TempMeJob
                    TSP.DataManager.TempMemberJobHistoryManager JhManager = new TSP.DataManager.TempMemberJobHistoryManager();
                    TSP.DataManager.TempMemberJobHistoryManager JhManager2 = new TSP.DataManager.TempMemberJobHistoryManager();


                    JhManager.FindByCode(JhId);
                    if (JhManager.Count == 1)
                    {

                        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                        if (MReId == CurrentMReId)
                        {
                            JhManager[0].Delete();
                            JhManager.Save();
                            JhManager2.FindByRequest(MeId, CurrentMReId);
                            DataTable dtJob = JhManager2.DataTable;
                            Session["FillMeJob"] = dtJob;
                            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                            CustomAspxDevGridView1.KeyFieldName = "TMJhId";
                            CustomAspxDevGridView1.DataBind();

                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                    }
                    #endregion
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف اطلاعات رخ داده است";
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        try
        {
            int JhId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeJob"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    JhId = (int)row["JhId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            else
            {
                if (Mode == "Request")
                {
                    if (!string.IsNullOrEmpty(InActiveName) && InActiveName == "فعال")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("رکورد مورد نظر  فعال می باشد");
                        return;
                    }
                    int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
                    int per = DeleteInActive(JhId, CurrentMReId);
                    switch (per)
                    {
                        case 0:
                            IsPageRefresh = true;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("ذخیره انجام شد");
                            int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());
                            Session["FillMeJob"] = JhManager.FindByMeRequest(MeId, CurrentMReId, -1, 0, TableType);
                            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
                            CustomAspxDevGridView1.KeyFieldName = "JhId";
                            CustomAspxDevGridView1.DataBind();
                            break;
                        case 1:
                            this.DivReport.Visible = true;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("رکورد انتخاب شده فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                            break;
                        case 2:
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است");
                            break;
                    }
                    CustomAspxDevGridView1.DataBind();
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);

            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("خطایی در ذخیره رخ داده است");
            if (Utility.ShowExceptionError())
            {
                this.LabelWarning.Text += err.Message;
            }

        }
    }

    protected int DeleteInActive(int JhId, int MReId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByReqIdAndTableId(MReId, JhId);
        if (RequestInActivesManager.Count == 1)
        {
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() > 0)
                result = 0;
            else result = 2;
        }
        else result = 1;

        return result;
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartCorporateDate" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartCorporateDate" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (Utility.DecryptQS(HDMode.Value) == "Request")
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

    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
        }
    }

    #endregion

    #region Methods
    protected void FillGrid()
    {
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager.Count == 0)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (Mode)
        {

            case "Home":

                if (MeManager[0]["MrsId"].ToString() == "1")
                {

                    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), -1, 1, 0, -1);
                    CustomAspxDevGridView1.DataBind();
                }

                else
                {

                    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), -1, -1, 0, -1);
                    CustomAspxDevGridView1.DataBind();
                }


                break;

            case "Request":
                int TableType = int.Parse(((int)TSP.DataManager.TableCodes.MemberRequest).ToString());

                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType);
                else
                    CustomAspxDevGridView1.DataSource = JobManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 0, TableType, 2);
                CustomAspxDevGridView1.KeyFieldName = "JhId";
                CustomAspxDevGridView1.DataBind();
                break;

            case "TempMe":
                TSP.DataManager.TempMemberJobHistoryManager TempMemberJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();
                TempMemberJobHistoryManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                DataTable dtJob = TempMemberJobHistoryManager.DataTable;
                CustomAspxDevGridView1.DataSource = dtJob;
                CustomAspxDevGridView1.KeyFieldName = "TMJhId";
                CustomAspxDevGridView1.DataBind();
                break;

        }
        Session["FillMeJob"] = CustomAspxDevGridView1.DataSource;
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

                    if (CurrentTaskCode == TaskCode)
                    {
                        return true;
                        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        //if (dtWorkFlowState.Rows.Count > 0)
                        //{
                        //    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        //    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                        //    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                        //    if (FirstTaskCode == TaskCode)
                        //    {
                        //        if (FirstNmcIdType == 0)
                        //        {
                        //            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        //            if (Permission > 0)
                        //                return true;
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
        return false;

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
        CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeJob"];
        CustomAspxDevGridView1.DataBind();

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "ذخیره انجام شد";
    }


    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Attachment
        arr.Add(0);//arr[5]-->Request

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
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);

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
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));

        }
    }

    private string FillMemberName()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        MemberInfoUserControl.MeId = Convert.ToInt32(MeId);
        if (Mode == "TempMe")
        {
            MemberInfoUserControl.IsMeTemp = true;
        }
        MemberInfoUserControl.MReId = Convert.ToInt32(Utility.DecryptQS(MemberRequest.Value));
        return MemberInfoUserControl.MeName;
    }

    private void BackToManagementPage()
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string UserName = "";
        if (Mode == "Request")
            UserName = Utility.DecryptQS(MemberId.Value);
        else if (Mode == "TempMe")
            UserName = "M" + Utility.DecryptQS(MemberId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + MemberRequest.Value);
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + MemberRequest.Value);
        }
    }
    #endregion
}

