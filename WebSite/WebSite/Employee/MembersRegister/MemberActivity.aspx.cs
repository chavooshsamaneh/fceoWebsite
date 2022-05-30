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


public partial class Employee_MembersRegister_MemberActivity : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.MemberActivitySubjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.Visible = per.CanView;

            ViewState["PMode"] = "";
            Session["FillMeActivity"] = null;
            Session["IsEdited_MeActivity"] = false;


            if (string.IsNullOrEmpty(Request.QueryString["MeId"]) || string.IsNullOrEmpty(Request.QueryString["Mode"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }

            MemberId.Value = Request.QueryString["MeId"].ToString();
            MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();


            string MeId = Utility.DecryptQS(MemberId.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(int.Parse(MeId));

            CheckWorkFlowPermission();

            switch (Mode)
            {

                case "Home":

                    MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Visible = false;

                    if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
                    {
                        Response.Redirect("Members.aspx");
                        return;
                    }

                    try
                    {
                        ReqManager.FindByMemberId(int.Parse(MeId), -1, 1);
                        if ((ReqManager[0]["IsConfirm"].ToString() != "0"))//FromMember
                        {
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnInActive2.Enabled = false;
                            btnInActive.Enabled = false;
                        }

                        if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                        {
                            btnEdit.Enabled = false;
                            btnEdit2.Enabled = false;
                            BtnNew.Enabled = false;
                            BtnNew2.Enabled = false;
                            btnInActive2.Enabled = false;
                            btnInActive.Enabled = false;

                            Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), -1, 1);

                        }

                        else
                        {
                            Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), -1, -1);

                        }
                    }
                    catch (Exception)
                    { }

                    //view asli filter ba meid
                    break;

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
                        //MenuTop.Items[0].Visible = false;
                        //MenuTop.Items[1].Visible = false;
                        //MenuTop.Items[2].Visible = false;
                        //   MenuTop.Items[MenuTop.Items.IndexOfName("Member")].Visible = false;



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
                                //btnSave.Enabled = false;

                            }
                            else
                            {


                                //TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                                ReqManager.FindByCode(int.Parse(MReId));
                                if (ReqManager.Count > 0)
                                {
                                    if ((ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
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

                        // MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
                        //MReId = Utility.DecryptQS(MemberRequest.Value);
                        if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                            Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                        else
                            Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);

                    }
                    catch (Exception)
                    {
                    }

                    break;
                    #endregion
                case "TempMe":
                    #region TempMe
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
                                //btnSave.Enabled = false;

                            }
                            else
                            {
                                ReqManager.FindByCode(int.Parse(MReId));
                                if (ReqManager.Count > 0)
                                {
                                    //if (Convert.ToBoolean(ReqManager[0]["Requester"]) == false || (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
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
                        TSP.DataManager.TempMemberActivitySubjectManager TempMemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
                        TempMemberActivitySubjectManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                        Session["FillMeActivity"] = TempMemberActivitySubjectManager.DataTable;

                    }
                    catch (Exception)
                    {
                    }

                    break;
                    #endregion
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (Session["FillMeActivity"] != null)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
            string Mode = Utility.DecryptQS(HDMode.Value);
            if (Mode == "Request")
                CustomAspxDevGridView1.KeyFieldName = "MasId";
            else if (Mode == "TempMe")
                CustomAspxDevGridView1.KeyFieldName = "TMasId";
            CustomAspxDevGridView1.DataBind();
        }
        else
            FillGrid();

        FillMemberName();
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        DivReport.Style["visibility"] = "hidden";
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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
            Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

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

    protected void MenuTop_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value +
                    "&PageMode=" + Request.QueryString["PageMode"] +
                    "&MReId=" + MemberRequest.Value +
                    "&Mode=" + HDMode.Value +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"] +
                    "&TMe=" + Utility.EncryptQS(TempMe) +
                    "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
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
            int MasId = -1;
            int MReId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeActivity"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    MasId = (int)row["MasId"];
                else if (Mode == "TempMe")
                    MasId = (int)row["TMasId"];

                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MasId == -1)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "لطفا ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                if (Mode == "Request")
                {
                    #region MeActivity
                    TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                    TSP.DataManager.MemberActivitySubjectManager ActManager2 = new TSP.DataManager.MemberActivitySubjectManager();

                    ActManager.FindByCode(MasId);
                    if (ActManager.Count == 1)
                    {
                        try
                        {
                            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));


                            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                            if (MReId == CurrentMReId)
                            {
                                ActManager[0].Delete();
                                ActManager.Save();
                                Session["FillMeActivity"] = ActManager2.FindByMeRequest(MeId, -1, -1, 2);
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                                CustomAspxDevGridView1.DataBind();

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
                                InsertInActive(MasId, CurrentMReId, MeId, ActManager2);

                                //if (Convert.ToBoolean(ActManager[0]["InActive"]))
                                //{
                                //    DivReport.Style["visibility"] = "block";
                                //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                //    return;
                                //}
                                //else
                                //{
                                //    ActManager[0].BeginEdit();
                                //    ActManager[0]["InActive"] = 1;
                                //    ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                //    ActManager[0].EndEdit();
                                //}
                            }

                            CheckMenuImageCurrentPage(MeId, CurrentMReId);

                        }
                        catch (Exception)
                        {

                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                        }

                    }
                    #endregion
                }
                else if (Mode == "TempMe")
                {
                    #region TempMeActivity
                    TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
                    TSP.DataManager.TempMemberActivitySubjectManager ActManager2 = new TSP.DataManager.TempMemberActivitySubjectManager();

                    ActManager.FindByCode(MasId);
                    if (ActManager.Count == 1)
                    {
                        try
                        {
                            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));


                            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                            if (MReId == CurrentMReId)
                            {
                                ActManager[0].Delete();
                                ActManager.Save();
                                ActManager2.FindByRequest(MeId, CurrentMReId);
                                DataTable dtAct = ActManager2.DataTable;
                                Session["FillMeActivity"] = dtAct;
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                                CustomAspxDevGridView1.DataBind();

                                DivReport.Style["visibility"] = "block";
                                this.LabelWarning.Text = "ذخیره انجام شد";


                            }

                            CheckMenuImageCurrentPage(MeId, CurrentMReId);

                        }
                        catch (Exception err)
                        {
                            Utility.SaveWebsiteError(err);
                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                        }

                    }
                    #endregion
                }
            }
            CustomAspxDevGridView1.FocusedRowIndex = 0;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        try
        {
            int MasId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeActivity"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    MasId = (int)row["MasId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MasId == -1)
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
                    int per = DeleteInActive(MasId, CurrentMReId);
                    switch (per)
                    {
                        case 0:
                            IsPageRefresh = true;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("ذخیره انجام شد");
                            Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                            CustomAspxDevGridView1.KeyFieldName = "MasId";
                            CustomAspxDevGridView1.DataBind();
                            break;
                        case 1:
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

    protected int DeleteInActive(int MasId, int MReId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByReqIdAndTableId(MReId, MasId);
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

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == DevExpress.Web.GridViewRowType.Data || e.RowType == DevExpress.Web.GridViewRowType.Filter)
        //{
        //    e.Row.Cells[1].Style["direction"] = "ltr";
        //    e.Row.Cells[3].Style["direction"] = "ltr";


        //}
        if (Utility.DecryptQS(HDMode.Value) == "Request")
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

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            Insert(e);
        else if (Mode == "TempMe")
            InsertTempMeActivity(e);
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            Edit(e);
        else if (Mode == "TempMe")
            EditTempMeActivity(e);
    }

    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        string Mode = Utility.DecryptQS(HDMode.Value);
        int MasId = -1;
        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {


            FillGrid();
            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));
            if (Mode == "Request")
                MasId = (int)row["MasId"];
            else if (Mode == "TempMe")
                MasId = (int)row["TMasId"];

            int MReId = (int)row["MReId"];


            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            if (MReId == CurrentMReId)
            {
                if (!CheckPermitionForEdit(MReId))
                {
                    //e.Result = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    //CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    //    Enable();
                    //    btnSave.Visible = true;
                    //    FillForm(MasId);
                    //    ViewState["PMode"] = Utility.EncryptQS("Edit"); ;
                    //    ASPxPopupControl1.HeaderText = "ویرایش";                       

                    //}
                    //else
                    //{
                    e.Result = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    CustomAspxDevGridView1.JSProperties["cpShow"] = 0;

                }

            }
            else
            {
                //btnSave.Visible = false;
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }


        }


    }

    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = 15;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = 15;
            arr[3] = 1;
            Session["MenuArrayList"] = arr;
        }
        else
            CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
    }

    #endregion

    #region Methods
    protected void FillGrid()
    {

        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

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

                    CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), -1, 1);
                    CustomAspxDevGridView1.DataBind();
                }

                else
                {

                    CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), -1, -1);
                    CustomAspxDevGridView1.DataBind();
                }


                break;

            case "Request":
                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                else
                    CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);

                CustomAspxDevGridView1.DataBind();


                break;

        }
        Session["FillMeActivity"] = CustomAspxDevGridView1.DataSource;
    }
    protected void InsertInActive(int MasId, int MReId, int MeId, TSP.DataManager.MemberActivitySubjectManager ActManager)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MasId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.MemberActivity;
        dr["ReqId"] = MReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
        CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
        CustomAspxDevGridView1.DataBind();

        DivReport.Style["visibility"] = "block";
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
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }
    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        MemberActivitySubjectManager.FindForDelete(MeId, MReId);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }

    #region WF Permissions
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    //private void CheckWorkFlowPermissionForSave()
    //{
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int CurrentTaskOrder = -1;
    //    int TaskOrder = -1;
    //    //****TableId
    //   // string MeId = Utility.DecryptQS(MemberId.Value);
    //    string MReId = Utility.DecryptQS(MemberRequest.Value);
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;

    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(MReId));
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //    }
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }

    //    if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
    //    {
    //        WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        TaskDoerManager.FindByTaskId(TaskId);

    //        if (TaskDoerManager.Count > 0)
    //        {
    //            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
    //            NezamMemberChartManager.FindByNcId(NcId);

    //            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

    //            LoginManager.FindByMeIdUltId(EmpId, 4);
    //            if (LoginManager.Count > 0)
    //            {
    //                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
    //                int CurrentUserId = Utility.GetCurrentUser_UserId();
    //                if (CurrentUserId == userId)
    //                {
    //                    BtnNew.Enabled = true;
    //                    BtnNew2.Enabled = true;
    //                    btnEdit.Enabled = true;
    //                    btnEdit2.Enabled = true;
    //                    btnInActive.Enabled = true;
    //                    btnInActive2.Enabled = true;

    //                }
    //                else
    //                {

    //                    BtnNew.Enabled = false;
    //                    BtnNew2.Enabled = false;
    //                    btnEdit.Enabled = false;
    //                    btnEdit2.Enabled = false;
    //                    btnInActive.Enabled = false;
    //                    btnInActive2.Enabled = false;

    //                }
    //            }
    //            else
    //            {
    //                BtnNew.Enabled = false;
    //                BtnNew2.Enabled = false;
    //                btnEdit.Enabled = false;
    //                btnEdit2.Enabled = false;
    //                btnInActive.Enabled = false;
    //                btnInActive2.Enabled = false;

    //            }
    //        }
    //        else
    //        {
    //            BtnNew.Enabled = false;
    //            BtnNew2.Enabled = false;
    //            btnEdit.Enabled = false;
    //            btnEdit2.Enabled = false;
    //            btnInActive.Enabled = false;
    //            btnInActive2.Enabled = false;

    //        }
    //    }
    //    else
    //    {
    //        BtnNew.Enabled = false;
    //        BtnNew2.Enabled = false;
    //        btnEdit.Enabled = false;
    //        btnEdit2.Enabled = false;
    //        btnInActive.Enabled = false;
    //        btnInActive2.Enabled = false;

    //    }
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnInActive"] = btnInActive.Enabled;

    //}

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

    #endregion

    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));

        }
    }

    private void FillMemberName()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
        if (Mode == "TempMe")
        {
            MemberInfoUserControl1.IsMeTemp = true;
        }
        MemberInfoUserControl1.MReId = Convert.ToInt32(Utility.DecryptQS(MemberRequest.Value));
    }

    private void Insert(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberActivitySubjectManager ActManager2 = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(ActManager);
        trans.Add(WorkFlowStateManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            ActManager2.FindByMeId(MeId);

            for (int i = 0; i < ActManager2.Count; i++)
            {
                if (ActManager2[i]["AsId"].ToString() == e.NewValues["AsId"].ToString() && ActManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    //this.DivReport.Visible = true;
                    //this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }

            DataRow dr = ActManager.NewRow();
            //if (drdAtSubj.Value != null)
            dr["AsId"] = int.Parse(e.NewValues["AsId"].ToString());

            dr["MeId"] = MeId;
            dr["UserId"] = Utility.GetCurrentUser_UserId();

            if (e.NewValues["AsPercent"] != null)
                dr["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());
            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = MReId;

            ActManager.AddRow(dr);
            trans.BeginSave();
            int cnt = ActManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeActivity"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";

                    //this.DivReport.Visible = true;
                    //this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                    Session["IsEdited_MeActivity"] = true;

                    Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                    CustomAspxDevGridView1.DataBind();

                    CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                }

            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            CustomAspxDevGridView1.CancelEdit();

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
    private void InsertTempMeActivity(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.TempMemberActivitySubjectManager ActManager2 = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(ActManager);
        trans.Add(WorkFlowStateManager);
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
            int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

            ActManager2.FindByTMeId(MeId);

            for (int i = 0; i < ActManager2.Count; i++)
            {
                if (ActManager2[i]["AsId"].ToString() == e.NewValues["AsId"].ToString() && ActManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    //this.DivReport.Visible = true;
                    //this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }

            DataRow dr = ActManager.NewRow();
            //if (drdAtSubj.Value != null)
            dr["AsId"] = int.Parse(e.NewValues["AsId"].ToString());

            dr["TMeId"] = MeId;
            dr["UserId"] = Utility.GetCurrentUser_UserId();

            if (e.NewValues["AsPercent"] != null)
                dr["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());
            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["ModifiedDate"] = DateTime.Now;
            dr["MReId"] = MReId;

            ActManager.AddRow(dr);
            trans.BeginSave();
            int cnt = ActManager.Save();
            if (cnt > 0)
            {
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_MeActivity"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                    Session["IsEdited_MeActivity"] = true;
                    ActManager.FindByRequest(MeId, MReId);
                    DataTable dtAct = ActManager.DataTable;
                    Session["FillMeActivity"] = dtAct;
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                    CustomAspxDevGridView1.DataBind();

                    CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                }

            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            CustomAspxDevGridView1.CancelEdit();

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

    private void Edit(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        int MasId = int.Parse(e.Keys["MasId"].ToString());
        //int MReId = int.Parse(e.NewValues["MReId"].ToString());
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        ActManager.FindByCode(MasId);
        int MReId = int.Parse(ActManager[0]["MReId"].ToString());


        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        if (MReId == CurrentMReId)
        {
            if (CheckPermitionForEdit(MReId))
            {
                TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
                //TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                TSP.DataManager.MemberActivitySubjectManager ActManager2 = new TSP.DataManager.MemberActivitySubjectManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
                trans.Add(ActManager);
                trans.Add(WorkFlowStateManager);
                try
                {

                    ActManager.FindByCode(int.Parse(e.Keys["MasId"].ToString()));
                    if (ActManager.Count == 1)
                    {

                        ActManager[0].BeginEdit();
                        //if (drdAtSubj.Value != null)
                        ActManager[0]["AsId"] = int.Parse(e.NewValues["AsId"].ToString());
                        //if (txtPercent.Text != "")
                        if (e.NewValues["AsPercent"] != null)
                            ActManager[0]["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());

                        ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        if (e.NewValues["Description"] != null)
                            ActManager[0]["Description"] = e.NewValues["Description"].ToString();
                        ActManager[0]["ModifiedDate"] = DateTime.Now;
                        ActManager[0].EndEdit();
                        trans.BeginSave();
                        int cnt = ActManager.Save();
                        if (cnt > 0)
                        {
                            int UpdateState = -1;
                            if (!(Convert.ToBoolean(Session["IsEdited_MeActivity"].ToString())))
                            {
                                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                                int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                            }
                            if (UpdateState == -4)
                            {
                                trans.CancelSave();
                                // this.DivReport.Visible = true;
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                                //this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                            }
                            else
                            {
                                trans.EndSave();
                                //this.DivReport.Visible = true;
                                //this.DivReport.Attributes.Add("Style", "display:block");
                                //this.LabelWarning.Text = " ذخیره انجام شد";
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                                Session["IsEdited_MeActivity"] = true;

                                Session["FillMeActivity"] = ActManager2.FindByMeRequest(MeId, -1, -1, 2);
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                                CustomAspxDevGridView1.DataBind();
                                CustomAspxDevGridView1.CancelEdit();

                            }
                        }
                        else
                        {
                            trans.CancelSave();
                            CustomAspxDevGridView1.CancelEdit();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.CancelEdit();

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }

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
    }
    private void EditTempMeActivity(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        int MasId = int.Parse(e.Keys["TMasId"].ToString());
        //int MReId = int.Parse(e.NewValues["MReId"].ToString());
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        ActManager.FindByCode(MasId);
        int MReId = int.Parse(ActManager[0]["MReId"].ToString());


        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        if (MReId == CurrentMReId)
        {
            if (CheckPermitionForEdit(MReId))
            {
                TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
                //TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                TSP.DataManager.TempMemberActivitySubjectManager ActManager2 = new TSP.DataManager.TempMemberActivitySubjectManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
                trans.Add(ActManager);
                trans.Add(WorkFlowStateManager);
                try
                {

                    ActManager.FindByCode(int.Parse(e.Keys["TMasId"].ToString()));
                    if (ActManager.Count == 1)
                    {

                        ActManager[0].BeginEdit();
                        //if (drdAtSubj.Value != null)
                        ActManager[0]["AsId"] = int.Parse(e.NewValues["AsId"].ToString());
                        //if (txtPercent.Text != "")
                        if (e.NewValues["AsPercent"] != null)
                            ActManager[0]["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());

                        ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        if (e.NewValues["Description"] != null)
                            ActManager[0]["Description"] = e.NewValues["Description"].ToString();
                        ActManager[0]["ModifiedDate"] = DateTime.Now;
                        ActManager[0].EndEdit();
                        trans.BeginSave();
                        int cnt = ActManager.Save();
                        if (cnt > 0)
                        {
                            int UpdateState = -1;
                            if (!(Convert.ToBoolean(Session["IsEdited_MeActivity"].ToString())))
                            {
                                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                                int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                            }
                            if (UpdateState == -4)
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";
                            }
                            else
                            {
                                trans.EndSave();
                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                                Session["IsEdited_MeActivity"] = true;
                                ActManager2.FindByRequest(MeId, CurrentMReId);
                                DataTable dtAct = ActManager2.DataTable;
                                Session["FillMeActivity"] = dtAct;
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeActivity"];
                                CustomAspxDevGridView1.DataBind();
                                CustomAspxDevGridView1.CancelEdit();

                            }
                        }
                        else
                        {
                            trans.CancelSave();
                            CustomAspxDevGridView1.CancelEdit();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                            return;
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.CancelEdit();

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }

                }
                catch (Exception err)
                {
                    trans.CancelSave();
                    Utility.SaveWebsiteError(err);
                    CustomAspxDevGridView1.CancelEdit();

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
