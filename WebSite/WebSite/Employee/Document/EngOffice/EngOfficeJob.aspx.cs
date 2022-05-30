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

public partial class Employee_Document_EngOffice_EngOfficeJob : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermissionForEngOffice(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }
            try
            {
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();


            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            OdbGrid.SelectParameters[0].DefaultValue = EngOfId;

            this.ViewState["BtnView"] = btnView.Enabled;

        }

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = OdbGrid;
        Session["Title"] = "سوابق کاری دفتر";
        //Session["Header"] = "شرکت : " + lblOfName.Text;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "EngOffice":
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int JhId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                JhId = (int)row["JhId"];

            }
            if (JhId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                Response.Redirect("EngOfficeJobShow.aspx?JhId=" + Utility.EncryptQS(JhId.ToString()) + "&EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value);

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
        Response.Redirect("EngOfficeRegister.aspx?EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(EngOfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("EngOffice.aspx?PostId=" + EngOfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("EngOffice.aspx");
        }
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartCorporateDate" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartCorporateDate" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion
}
