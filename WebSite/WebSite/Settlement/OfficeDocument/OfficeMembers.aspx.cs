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

public partial class Settlement_OfficeDocument_OfficeMembers : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["OfId"]))
                {
                    Response.Redirect("OfficeRequest.aspx");
                    return;
                }

                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();


                string OfId = Utility.DecryptQS(OfficeId.Value);
                string OfReId = Utility.DecryptQS(OfficeRequest.Value);


                ObjectDataSourceOffice.SelectParameters[0].DefaultValue = OfId;
                ObjectDataSourceOffice.SelectParameters[1].DefaultValue = OfReId;
                ObjectDataSourceOffice.FilterExpression = "SysInActive=0";
                

                TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
                OfManager.FindByCode(int.Parse(OfId));
                if (OfManager.Count > 0)
                    lblOfName.Text = OfManager[0]["OfName"].ToString();
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeRequestShow.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        byte OfmType = 0;
        int PersonId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            OfmType = Convert.ToByte(row["OfmType"]);
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            Response.Redirect("OfficeMemberShow.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&OfmType=" + Utility.EncryptQS(OfmType.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);

        }

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

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Office":
                Response.Redirect("OfficeRequestShow.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);// + "&SrchFlt=" + SrchFlt);
                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Agent":
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }

    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";
    }
    #endregion
}
