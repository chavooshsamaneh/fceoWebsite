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

public partial class Employee_MembersRegister_MemberLanguage : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.MemberLanguageManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            //btnView.Enabled = per.CanView;
            //btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.Visible = per.CanView;

            Session["FillMeLanguage"] = null;
            ViewState["PMode"] = "";
            Session["IsEdited_MeLanguage"] = false;

            if (string.IsNullOrEmpty(Request.QueryString["Mode"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }

            try
            {
                MemberId.Value = Request.QueryString["MeId"].ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();

            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))//|| string.IsNullOrEmpty(MReId)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(int.Parse(MeId));

            TSP.DataManager.MemberLanguageManager LanManager = new TSP.DataManager.MemberLanguageManager();
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();


            CheckWorkFlowPermission();

            switch (Mode)
            {

                case "Home":

                    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Visible = false;

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

                        if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                            Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                        else
                            Session["FillMeLanguage"] = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);


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


                                //TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
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

                        TSP.DataManager.TempMemberLanguageManager TempMemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
                        TempMemberLanguageManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                        Session["FillMeLanguage"] = TempMemberLanguageManager.DataTable;
                    }
                    catch (Exception)
                    {
                    }

                    break;
                    #endregion
            }

            //this.ViewState["BtnSave"] = btnSave.Enabled;
            //this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        if (Session["FillMeLanguage"] != null)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
            string Mode = Utility.DecryptQS(HDMode.Value);
            if (Mode == "Request")
                CustomAspxDevGridView1.KeyFieldName = "MlanId";
            else if (Mode == "TempMe")
                CustomAspxDevGridView1.KeyFieldName = "TMlanId";
            CustomAspxDevGridView1.DataBind();
        }
        else
            FillGrid();

        FillMemberName();
        //if (this.ViewState["BtnSave"] != null)
        //this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        //if (this.ViewState["BtnView"] != null)
        //this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = true;
        // Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
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
            Response.Redirect("MemberRegister.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

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

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            int MlanId = -1;
            int MReId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);
            
            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLanguage"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    MlanId = (int)row["MlanId"];
                else if (Mode == "TempMe")
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
                if (Mode == "Request")
                {
                    #region MeLang
                    TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();
                    TSP.DataManager.MemberLanguageManager LnManager2 = new TSP.DataManager.MemberLanguageManager();



                    LnManager.FindByCode(MlanId);
                    if (LnManager.Count == 1)
                    {
                        try
                        {
                            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                            if (MReId == CurrentMReId)
                            {
                                LnManager[0].Delete();
                                LnManager.Save();
                                Session["FillMeLanguage"] = LnManager2.FindByMeRequest(MeId, -1, -1, 2);
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                                CustomAspxDevGridView1.KeyFieldName = "MlanId";
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

                                InsertInActive(MlanId, CurrentMReId, MeId, LnManager2);

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
                else if (Mode == "TempMe")
                {
                    #region TempMeLang
                    TSP.DataManager.TempMemberLanguageManager LnManager = new TSP.DataManager.TempMemberLanguageManager();
                    TSP.DataManager.TempMemberLanguageManager LnManager2 = new TSP.DataManager.TempMemberLanguageManager();

                    LnManager.FindByCode(MlanId);
                    if (LnManager.Count == 1)
                    {
                        try
                        {
                            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

                            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                            if (MReId == CurrentMReId)
                            {
                                LnManager[0].Delete();
                                LnManager.Save();
                                LnManager2.FindByRequest(MeId, CurrentMReId);
                                DataTable dtLang = LnManager2.DataTable;
                                Session["FillMeLanguage"] = dtLang;
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                                CustomAspxDevGridView1.KeyFieldName = "TMlanId";
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

        TSP.DataManager.MemberLanguageManager LnManager = new TSP.DataManager.MemberLanguageManager();
        try
        {
            int MlanId = -1;
            string InActiveName = "";
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                if (Session["FillMeLanguage"] != null)
                {
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                    CustomAspxDevGridView1.DataBind();
                }
                else
                    FillGrid();

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                if (Mode == "Request")
                    MlanId = (int)row["MlanId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlanId == -1)
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
                    int per = DeleteInActive(MlanId, CurrentMReId);
                    switch (per)
                    {
                        case 0:
                            IsPageRefresh = true;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("ذخیره انجام شد");
                            Session["FillMeLanguage"] = LnManager.FindByMeRequest(MeId, -1, -1, 2);
                            CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                            CustomAspxDevGridView1.KeyFieldName = "MlanId";
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

    protected int DeleteInActive(int MlanId, int MReId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByReqIdAndTableId(MReId, MlanId);
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
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
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
                Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                 + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
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

    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {

            FillGrid();
            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));
            int MlanId = -1;
            string Mode = Utility.DecryptQS(HDMode.Value);
            if (Mode == "Request")
                MlanId = (int)row["MlanId"];
            else if (Mode == "TempMe")
                MlanId = (int)row["TMlanId"];

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
                //btnSave.Visible = false;
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }


        }

    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            Insert(e);
        else if (Mode == "TempMe")
            InsertTempMeLanguage(e);
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            Edit(e);
        else if (Mode == "TempMe")
            EditTempMeLanguage(e);
    }

    protected void CustomAspxDevGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

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
            CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
    }

    #endregion

    #region Methods
 
    protected void FillGrid()
    {

        TSP.DataManager.MemberLanguageManager LanManager = new TSP.DataManager.MemberLanguageManager();
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

                    CustomAspxDevGridView1.DataSource = LanManager.FindByMeRequest(int.Parse(MeId), -1, 1);
                    CustomAspxDevGridView1.DataBind();
                }

                else
                {

                    CustomAspxDevGridView1.DataSource = LanManager.FindByMeRequest(int.Parse(MeId), -1, -1);
                    CustomAspxDevGridView1.DataBind();
                }


                break;

            case "Request":
                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    CustomAspxDevGridView1.DataSource = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                else
                    CustomAspxDevGridView1.DataSource = LanManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);
                CustomAspxDevGridView1.KeyFieldName = "MlanId";
                CustomAspxDevGridView1.DataBind();

                break;

            case "TempMe":
                TSP.DataManager.TempMemberLanguageManager TempMemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
                TempMemberLanguageManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                DataTable dtLan = TempMemberLanguageManager.DataTable;
                CustomAspxDevGridView1.DataSource = dtLan;
                CustomAspxDevGridView1.KeyFieldName = "TMlanId";
                CustomAspxDevGridView1.DataBind();

                break;

        }
        Session["FillMeLanguage"] = CustomAspxDevGridView1.DataSource;
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

    protected void InsertInActive(int LanId, int MReId, int MeId, TSP.DataManager.MemberLanguageManager LanManager)
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

        Session["FillMeLanguage"] = LanManager.FindByMeRequest(MeId, -1, -1, 2);
        CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
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
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        MemberLanguageManager.FindForDelete(MeId, MReId);

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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "بروز رسانی",Utility.GetCurrentUser_UserId());
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
                    Session["FillMeLanguage"] = MeLlanManager.FindByMeRequest(MeId, -1, -1, 2);
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                    CustomAspxDevGridView1.KeyFieldName = "MlanId";
                    CustomAspxDevGridView1.DataBind();                    
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
    private void InsertTempMeLanguage(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "بروزرسانی", Utility.GetCurrentUser_UserId());
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
                    MeLlanManager.FindByRequest(MeId, int.Parse(Utility.DecryptQS(MemberRequest.Value)));
                    DataTable dtLang = MeLlanManager.DataTable;
                    Session["FillMeLanguage"] = dtLang;
                    CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                    CustomAspxDevGridView1.KeyFieldName = "TMlanId";
                    CustomAspxDevGridView1.DataBind();
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
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "بروزرسانی", Utility.GetCurrentUser_UserId());
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

                                Session["FillMeLanguage"] = LnManager2.FindByMeRequest(MeId, -1, -1, 2);
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                                CustomAspxDevGridView1.KeyFieldName = "MlanId";
                                CustomAspxDevGridView1.DataBind();
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
    }
    private void EditTempMeLanguage(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
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
                                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "بروزرسانی", Utility.GetCurrentUser_UserId());
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

                                LnManager2.FindByRequest(MeId, CurrentMReId);
                                DataTable dtLang = LnManager2.DataTable;
                                Session["FillMeLanguage"] = dtLang;
                                CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeLanguage"];
                                CustomAspxDevGridView1.KeyFieldName = "TMlanId";
                                CustomAspxDevGridView1.DataBind();
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
