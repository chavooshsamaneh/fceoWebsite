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

public partial class Members_EngOffice_EngOfficeJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        if (!IsPostBack)
        {
          
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

         
        }

       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "EngOffice":
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
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
        Response.Redirect("EngOfficeRegister.aspx?EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value);


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
}
