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

public partial class Teachers_Amoozesh_TrainingQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int TrQuId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TrQuId = (int)row["TrQuId"];
        }
        if (TrQuId == -1)
        {
            SetMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS(TrQuId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherHome.aspx");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int TrQuId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TrQuId = (int)row["TrQuId"];
        }
        if (TrQuId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {

            Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS(TrQuId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }

    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}
