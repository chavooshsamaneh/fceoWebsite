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

public partial class Employee_Document_MemberDocCapacity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MeId"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["MFId"] == null)
        {
            Response.Redirect("MemberFile.aspx");
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewCapacity.Visible = per.CanView;

            HiddenFieldMeCapacity["PageMode"] = Request.QueryString["PgMd"];
            HiddenFieldMeCapacity["MeId"] = Request.QueryString["MeId"];
            HiddenFieldMeCapacity["MFId"] = Request.QueryString["MfId"];
        }

        if (!Utility.IsDBNullOrNullValue(HiddenFieldMeCapacity["MeId"]))
        {
            MemberInfoUserControl1.MeId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldMeCapacity["MeId"].ToString()));
            MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;
        }
        CheckMenueChange(Convert.ToInt32(Utility.DecryptQS(HiddenFieldMeCapacity["MFId"].ToString())), Convert.ToInt32(Utility.DecryptQS(HiddenFieldMeCapacity["MeId"].ToString())));
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberConfirmJobHistory.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&MeId=" + HiddenFieldMeCapacity["MeId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&MeId=" + HiddenFieldMeCapacity["MeId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Accounting":
                Response.Redirect("DocumentAccounting.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&MeId=" + HiddenFieldMeCapacity["MeId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&MeId=" + HiddenFieldMeCapacity["MeId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString()
                 + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMeCapacity["MFId"].ToString() + "&MeId=" + HiddenFieldMeCapacity["MeId"].ToString() + "&PgMd=" + HiddenFieldMeCapacity["PageMode"].ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    private void BackToManagementPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFile.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFile.aspx");
        }
    }

    private void CheckMenueChange(int MfId, int MeId)
    {
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();      
        DocMemberExamManager.SelectById(MfId, MeId);
        if (DocMemberExamManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Height = Utility.MenuImgSize;
        }
        DataTable dtRes = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
        if (dtRes.Rows.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Height = Utility.MenuImgSize;
        }

    }
}
