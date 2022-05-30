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

public partial class Members_Office_OfficeJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        if (!IsPostBack)
        {

            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

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
            ObjectDataSource1.SelectParameters[4].DefaultValue = ((int)TSP.DataManager.TableCodes.OfficeRequest).ToString();

            ObjectDataSourceFactorValues.SelectParameters[0].DefaultValue = OfId;

            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();
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
                Response.Redirect("OfficeJobShow.aspx?JhId=" + Utility.EncryptQS(JhId.ToString())  + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value );

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
        Response.Redirect("OfficeShowInfo.aspx?OfId=" + OfficeId.Value   + "&OfReId=" + OfficeRequest.Value);
       
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("OfficeMember.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
            case "Office":
                Response.Redirect("OfficeShowInfo.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
        }

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName=="StartCorporateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "StartCorporateDate")
            e.Cell.Style["direction"] = "ltr";
    }
   
}
