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

public partial class Agents : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    NextPage("View");
    //}
    #endregion

    #region Methods
    //private void NextPage(string Mode)
    //{
    //    //int AgentId = -1;
    //    //if (CustomAspxDevGridView1.FocusedRowIndex > -1)
    //    //{
    //    //    DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
    //    //    AgentId = (int)row["AgentId"];
    //    //}

    //    //if (AgentId == -1 && Mode != "New")
    //    //{
    //    //    this.DivReport.Visible = true;
    //    //    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
    //    //}
    //    //else
    //    //{
    //    //    Response.Redirect("AgentView.aspx?AgentId=" + Utility.EncryptQS(AgentId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
    //    //}
    //}

    /*************************************************************************************************************/
    protected void btnView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int AgentId = Convert.ToInt32(lb.CommandArgument);

        if (Utility.IsDBNullOrNullValue(AgentId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("AgentView.aspx?AgentId=" + Utility.EncryptQS(AgentId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
    #endregion
}
