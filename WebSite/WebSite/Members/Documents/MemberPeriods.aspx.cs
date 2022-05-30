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

public partial class Members_Documents_MemberPeriods : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MeId"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["MFId"] == null)
        {
            Response.Redirect("MemberFiles.aspx");
        }

        if (!IsPostBack)
        {            
            HiddenFieldMePeriods["PageMode"] = Request.QueryString["PgMd"];
            HiddenFieldMePeriods["MeId"] = Request.QueryString["MeId"];
            HiddenFieldMePeriods["MFId"] = Request.QueryString["MfId"];
            string MeId = Utility.DecryptQS(HiddenFieldMePeriods["MeId"].ToString());
            string MFId = Utility.DecryptQS(HiddenFieldMePeriods["MFId"].ToString());

            OdbMadrak.SelectParameters[0].DefaultValue = MeId;
            CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
        }
        MemberInfoUserControl1.MeId = Utility.GetCurrentUser_MeId();
        MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"]))        
        //Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&MeId=" + HiddenFieldMePeriods["MeId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString()
        //       + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&MeId=" + HiddenFieldMePeriods["MeId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&MeId=" + HiddenFieldMePeriods["MeId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobConfirmition":
                Response.Redirect("MemberJobConfirm.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&MeId=" + HiddenFieldMePeriods["MeId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldMePeriods["MFId"].ToString() + "&MeId=" + HiddenFieldMePeriods["MeId"].ToString() + "&PgMd=" + HiddenFieldMePeriods["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    private void BackToManagementPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFiles.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFiles.aspx");
        }
    }


    private void CheckMenueChange(int MfId, int MeId)
    {
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        ProjectJobHistoryManager.FindForDelete(0, MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Documents));
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Height = Utility.MenuImgSize;
        }
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
