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

public partial class Settlement_OfficeDocument_OfficeFinancialStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("OfficeRequest.aspx");

            }

            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);


            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSource1.SelectParameters[0].DefaultValue = OfId;
            ObjectDataSource1.SelectParameters[1].DefaultValue = OfReId;

            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfsId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfsId = (int)row["OfsId"];

        }
        if (OfsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OfficeFinancialStatusShow.aspx?OfsId=" + Utility.EncryptQS(OfsId.ToString()) + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);


        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeRequestShow.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
    }
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (OfficeRequest.Value != null)
        {
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnOfReId = e.GetValue("OfReId").ToString();
            if (OfReId == CurretnOfReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }

    }
    protected void CustomAspxDevGridView1_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridView1.FocusedRowIndex = e.VisibleIndex;

    }
    protected void GridViewJudge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PKId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Office":
                Response.Redirect("OfficeRequestShow.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);// + "&SrchFlt=" + SrchFlt);
                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Agent":
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
      if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }
    protected void GridViewJudge_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {      
    }
    protected void GridViewJudge_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MeetingId" || e.DataColumn.FieldName == "MeetingDate")
            e.Cell.Style["direction"] = "ltr";
    }
}
