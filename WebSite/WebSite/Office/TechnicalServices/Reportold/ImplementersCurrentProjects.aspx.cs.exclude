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

public partial class Employee_TechnicalServices_Report_ImplementersCurrentProjects : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetKeys();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        Summary();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../TsHome.aspx");
    }

    private void Print()
    {
        string FilterExp;
        string MeOfficeId;
        string MemberTypeId;

        FilterExp = CustomAspxDevGridView1.FilterExpression;
        MeOfficeId = ASPxTextBoxMeId.Text;
        MemberTypeId = ASPxComboBoxMemberType.Value.ToString();

        string Qs = "MeOfficeId=" + Utility.EncryptQS(MeOfficeId) + "&MemberTypeId=" + Utility.EncryptQS(MemberTypeId) + "&FilterExp=" + Utility.EncryptQS(FilterExp);
        Response.Redirect("~/ReportForms/TechnicalServices/ImplementersCurrentProjects.aspx?" + Qs);
    }

    private void Summary()
    {
        int ProjectId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ProjectId = (int)row["ProjectId"];
        }

        if (ProjectId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("../Report/Summary.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&CurPrj=" + Utility.EncryptQS("Imp"));
        }
    }

    private void SetKeys()
    {
        ObjectDataSourceMemberType.FilterExpression = "MemberTypeId=" + ((int)TSP.DataManager.TSMemberType.Member).ToString() + "OR MemberTypeId=" + ((int)TSP.DataManager.TSMemberType.Office).ToString() + "OR MemberTypeId=" + ((int)TSP.DataManager.TSMemberType.OtherPerson).ToString();

        ASPxComboBoxMemberType.DataBind();
        ASPxComboBoxMemberType.Value = ((int)TSP.DataManager.TSMemberType.Office).ToString();
        ASPxTextBoxMeId.Text = Utility.GetCurrentUser_MeId().ToString();

        Search();
    }    

    private void Search()
    {
        string MeId = ASPxTextBoxMeId.Text;
        string MemberType = (ASPxComboBoxMemberType.Value).ToString();

        ObjectDataSourceProject.SelectParameters["MeOfficeId"].DefaultValue = MeId;
        ObjectDataSourceProject.SelectParameters["MemberTypeId"].DefaultValue = MemberType;

        CustomAspxDevGridView1.DataBind();
    }

    

    /*************************************************************************************************************/
    
}