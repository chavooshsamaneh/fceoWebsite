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


public partial class Members_EngOfficeView_EngOfficeMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
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


            if (string.IsNullOrEmpty(EngOfId) || string.IsNullOrEmpty(EOfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            ObjectDataSource1.SelectParameters[0].DefaultValue = EOfId;
            ObjectDataSource1.SelectParameters[1].DefaultValue = EngOfId;


        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {


            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("EngOfficeMemberShow.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);


        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeShowInfo.aspx?PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&EOfId=" + EngFileId.Value);
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        switch (e.Item.Name)
        {

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
            case "EngOffice":
                Response.Redirect("EngOfficeShowInfo.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ConfirmDate")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ConfirmDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (EngFileId.Value != null)
        {
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnEOfId = e.GetValue("OfReId").ToString();
            if (EOfId == CurretnEOfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
}