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

public partial class Settlement_ImplementDoc_FinancialStatus : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewFinancialStatus.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["MfId"]))
            {
                Response.Redirect("ImplementDoc.aspx");
                return;
            }
            try
            {
                HiddenFieldFinantialStatus["MfId"] = Server.HtmlDecode(Request.QueryString["MfId"]).ToString();
                HiddenFieldFinantialStatus["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"]).ToString();
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DocMemberFileManager.ClearBeforeFill = true;
                int MfId = int.Parse(Utility.DecryptQS(HiddenFieldFinantialStatus["MfId"].ToString()));
                DataTable dtDoMeFile = DocMemberFileManager.SelectImplementDoc(-1, MfId);
                if (dtDoMeFile.Rows.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(dtDoMeFile.Rows[0]["FullName"]))
                        GridViewFinancialStatus.Caption = "مجوز فعالیت مجری حقیقی : " + dtDoMeFile.Rows[0]["FullName"].ToString();
                }

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImplementDoc.aspx");
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobHistory":
                Response.Redirect("~/Settlement/MemberDocument/MemberJobHistory.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS(((int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp).ToString()));
                break;
            case "ImplDoc":
                Response.Redirect("ImplementDocBasicInfo.aspx?MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS(((int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp).ToString()));
                break;
        }
    }

    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewFinancialStatus_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

        }
    }

    protected void GridViewFinancialStatus_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJudge_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MeetingDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewJudge_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {        
        switch (e.Column.FieldName)
        {
            case "MeetingDate":
                e.Editor.Style["direction"] = "ltr";
                break;

        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int OfsId = -1;
        int focucedIndex = GridViewFinancialStatus.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewFinancialStatus.GetDataRow(focucedIndex);
            OfsId = (int)row["OfsId"];
        }
        if (OfsId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                OfsId = -1;
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
            else
            {
                Response.Redirect("AddFinancialStatus.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldFinantialStatus["MfId"].ToString() + "&PrePgMd=" + HiddenFieldFinantialStatus["PageMode"].ToString());
            }
        }
    }
    #endregion     
   
   
}
