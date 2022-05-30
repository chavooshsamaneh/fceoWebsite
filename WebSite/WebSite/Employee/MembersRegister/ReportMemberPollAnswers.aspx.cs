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


public partial class Employee_MembersRegister_ReportMemberPollAnswers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetKey();
            SetPermission();
            SetMenuItem();

        }

        FillMemberName();
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    #region Properties
    public string HDMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldAnswer["HDMode"].ToString());
        }
        set
        {
            HiddenFieldAnswer["HDMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int MemberId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldAnswer["MemberId"].ToString()));
        }
        set
        {
            HiddenFieldAnswer["MemberId"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int MReId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldAnswer["MReId"].ToString()));
        }
        set
        {
            HiddenFieldAnswer["MReId"] = Utility.EncryptQS(value.ToString());
        }
    }
    public string PgMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldAnswer["PgMode"].ToString());
        }
        set
        {
            HiddenFieldAnswer["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    void SetKey()
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                MemberId = int.Parse(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
                MReId = int.Parse(Utility.DecryptQS(Request.QueryString["MReId"].ToString()));
                PgMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
                HDMode = Utility.DecryptQS(Request.QueryString["Mode"].ToString());
                objAnswer.SelectParameters["MeId"].DefaultValue = MemberId.ToString();
                objAnswer.SelectParameters["UltId"].DefaultValue = Convert.ToInt32(TSP.DataManager.UserType.Member).ToString();
                objAnswer.DataBind();
            }
            else
            {
                Response.Redirect("Members.aspx");
            }
        }
        catch
        {
            Response.Redirect("Members.aspx");
        }
    }

    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.PollAnswerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnPrint2.Enabled = btnPrint.Enabled = per.CanView;
        btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
        this.ViewState["btnPrint"] = btnPrint.Enabled;
        this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
    }

    private string FillMemberName()
    {
        MemberInfoUserControl1.MeId = Convert.ToInt32(MemberId);
        if (HDMode == "TempMe")
        {
            MemberInfoUserControl1.IsMeTemp = true;
        }
        MemberInfoUserControl1.MReId = MReId;
        return MemberInfoUserControl1.MeName;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "MemberPollAnswers";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewAnswer_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    string MemberName = FillMemberName();
                    GridViewAnswer.JSProperties["cpDoPrint"] = 1;
                    Session["DataTable"] = GridViewAnswer.Columns;
                    Session["DataSource"] = objAnswer;
                    Session["Title"] = "گزارش پاسخ های نظرسنجی";
                    Session["Header"] = "نام : " + MemberName + " " + "کد عضویت : " + MemberId;
                    break;
            }
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        if (HDMode == "Request")
            TempMe = "0";
        else if (HDMode == "TempMe")
            TempMe = "1";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            //case "Research":
            //    Response.Redirect("MemberResearch1.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]);
            //    break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Member":
                Response.Redirect("MemberRegister.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"] +
                    "&TMe=" + Utility.EncryptQS(TempMe) +
                    "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(PgMode) +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) 
                    + "&MReId=" + Request.QueryString["MReId"]
                    + "&Mode=" + Request.QueryString["Mode"]
                    + "&TP=" + Request.QueryString["TP"]
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] 
                    + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void GridViewAnswer_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Name == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewAnswer_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string TempMe = "";
        if (HDMode == "Request")
            TempMe = "0";
        else if (HDMode == "TempMe")
            TempMe = "1";
        if (HDMode == "Home")
        {
            Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&PageMode=" + Utility.EncryptQS(PgMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        }
        else
            Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&MReId=" + Request.QueryString["MReId"] + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS("View")
                + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
         + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));

    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
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
            CheckMenuImage(MemberId, MReId);

        }
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

    private void BackToManagementPage()
    {
        string Mode = Utility.DecryptQS(HDMode);
        string UserName = "";
        if (Mode == "Request")
            UserName = MemberId.ToString();
        else if (Mode == "TempMe")
            UserName = "M" + MemberId.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Utility.EncryptQS(MReId.ToString()));
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + Utility.EncryptQS(MReId.ToString()));
        }
    }


}