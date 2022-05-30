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

public partial class Members_Amoozesh_Licence : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string MeId = Utility.GetCurrentUser_MeId().ToString();
            if (string.IsNullOrEmpty(MeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            OdbMadrak.SelectParameters["MeId"].DefaultValue = MeId;
 
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int ID = -1;
        int Type = -1;
        int PPId = -1;
        int MeId = -1;
        int InsId = -1;


        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ID = (int)row["ID"];//MdId Or PRId
            Type = (int)row["Type"];
            PPId = (int)row["PPId"];
            MeId = Convert.ToInt32( row["MeId"]);
            InsId = (int)row["InsId"];
            
        }
        if (ID == -1 || PPId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            if ((Type == 0) || (Type == 1))
                Response.Redirect("LicenceView.aspx?MdId=" + Utility.EncryptQS(ID.ToString()));
            else if (Type == 2)
                Response.Redirect("PeriodsView.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&InsId=" + Utility.EncryptQS(InsId.ToString()) + "&RetPage=" + Utility.EncryptQS("Madrak"));
            else if (Type == 3)
                Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(PPId.ToString()) + "&PRId=" + Utility.EncryptQS(ID.ToString()) + "&RetPage=" + Utility.EncryptQS("Madrak"));

        }
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
}
