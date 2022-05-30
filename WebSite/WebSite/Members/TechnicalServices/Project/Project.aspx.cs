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

public partial class Members_TechnicalServices_Project_Project : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSourceProject.SelectParameters[0].DefaultValue = (Utility.GetCurrentUser_MeId()).ToString();
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        Session["DataTable"] = GridViewProject.Columns;
        Session["DataSource"] = ObjectDataSourceProject;
        Session["Title"] = "پروژه ها";
        Session["Header"] = GetRepHeader();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../TsHome.aspx");
    }

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        Summary();
    }
    protected void btnContract_Click(object sender, EventArgs e)
    {
        int PrjImpObsDsgnId = -1;
        int ActivityIngridientTypeId = -1;
        int ProjectId = -1;
        if (GridViewProject.FocusedRowIndex <= -1)
        {
            SetMessage("برای مشاهده قرارد داد های نظارت ابتدا یک ردیف از جدول را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex);
        ProjectId = (int)row["ProjectId"];
        PrjImpObsDsgnId = (int)row["PrjImpObsDsgnId"];
        ActivityIngridientTypeId = (int)row["ActivityIngridientTypeId"];

        if (ActivityIngridientTypeId != (int)TSP.DataManager.TSProjectIngridientType.Observer)
        {
            SetMessage("تنها قادر به ثبت قرارداد نظارت برای پروژه هایی  را دارید که ناظر آن پروژه هستید");
            return;
        }

        if (ProjectId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Response.Redirect("Contract.aspx?PrId=" + Utility.EncryptQS(ProjectId.ToString()) + "&prObsId=" + Utility.EncryptQS(PrjImpObsDsgnId.ToString()));
        }
    }
    protected void btnNewObsContract_Click(object sender, EventArgs e)
    {
        int PrjImpObsDsgnId = -1;
        int ActivityIngridientTypeId = -1;
        int ProjectId = -1;
        if (GridViewProject.FocusedRowIndex <= -1)
        {
            SetMessage("برای مشاهده قرارد داد های نظارت ابتدا یک ردیف از جدول را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex);
        ProjectId = (int)row["ProjectId"];
        PrjImpObsDsgnId = (int)row["PrjImpObsDsgnId"];
        ActivityIngridientTypeId = (int)row["ActivityIngridientTypeId"];

        if (ActivityIngridientTypeId != (int)TSP.DataManager.TSProjectIngridientType.Observer)
        {
            SetMessage("تنها قادر به ثبت قرارداد نظارت برای پروژه هایی  را دارید که ناظر آن پروژه هستید");
            return;
        }

        if (ProjectId == -1)
        {
            SetMessage("لطفاً برای ثبت قرارداد جدید پروژه مورد نظر را از لیست انتخاب نمائید");
        }
        else
        {            
            Response.Redirect("ContractInsert.aspx?PrId=" + Utility.EncryptQS(ProjectId.ToString()) + "&CoId=" + Utility.EncryptQS("-1") + "&PMo=" + Utility.EncryptQS("New") + "&PrObsId=" + Utility.EncryptQS(PrjImpObsDsgnId.ToString()));
        }
    }
    protected void GridViewProject_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        GridViewProject.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewProjectRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        int ProjectId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        Session["ProjectId"] = ProjectId;
    }

    protected void btnSubmitDesign_Click(object sender, EventArgs e)
    {
        Response.Redirect("DesignerLogin.aspx");
    }
    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void NextPage(string Mode)
    {
        int ProjectId = -1;
        int PrjReId = -1;

        if (GridViewProject.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (GridViewProjectRequest != null)
            {
                if (GridViewProjectRequest.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewProjectRequest.GetDataRow(GridViewProjectRequest.FocusedRowIndex);
                    ProjectId = (int)row["ProjectId"];
                    PrjReId = (int)row["PrjReId"];
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }

        if ((PrjReId == -1 || ProjectId == -1))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("ProjectInsert.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = GridViewProject.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/ProjectReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    private void Summary()
    {
        int ProjectId = -1;

        if (GridViewProject.FocusedRowIndex > -1)
        {
            DataRow row = GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex);
            ProjectId = (int)row["ProjectId"];
        }

        if (ProjectId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("../Report/Summary.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()));
        }
    }
    /*************************************************************************************************************/
    private string GetRepHeader()
    {
        string AgentName = GetAgentName();
        return "نمایندگی : " + AgentName;
    }

    private string GetAgentName()
    {
        int AgentCode = Utility.GetCurrentUser_AgentId();
        TSP.DataManager.AccountingAgentManager Manager = new TSP.DataManager.AccountingAgentManager();
        Manager.FindByCode(AgentCode);
        if (Manager.Count > 0)
            return Manager[0]["Name"].ToString();
        else
            return "";
    }

    /*************************************************************************************************************/
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

    /*************************************************************************************************************/
    #endregion 
}