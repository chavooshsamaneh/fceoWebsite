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

public partial class Employee_TechnicalServices_Report_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        if (!IsPostBack)
        {
            Session["FoundationId"] = null;
            Session["WallsId"] = null;
            Session["EntranceId"] = null;
            Session["ImpId"] = null;

            SetKeys();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Back();
    }


    private void Print()
    {
        int ProjectId = -2;

        if (ASPxTextBoxProjectId.Text != "")
            ProjectId = Convert.ToInt32(ASPxTextBoxProjectId.Text);

        Response.Redirect("~/ReportForms/TechnicalServices/Summary.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()));
    }

    private void Back()
    {
        if (!(string.IsNullOrEmpty(Request.QueryString["ProjectId"])) && !(string.IsNullOrEmpty(Request.QueryString["CurPrj"])))
        {
            string CurPrj = "";

            try
            {
                CurPrj = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["CurPrj"]).ToString());
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            switch (CurPrj)
            {
                case "Imp":
                    Response.Redirect("ImplementersCurrentProjects.aspx");
                    break;
                case "Obs":
                    Response.Redirect("ObserversCurrentProjects.aspx");
                    break;
                case "Des":
                    Response.Redirect("DesignersCurrentProjects.aspx");
                    break;

                default:
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    break;
            }
        }

        Response.Redirect("../Project/Project.aspx");
    }

    /*******************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            ASPxTextBoxProjectId.Text = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString());
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        Search();
    }
    
    private void Search()
    {
        int ProjectId = Convert.ToInt32(ASPxTextBoxProjectId.Text);
        if (!SetProjectFileds(ProjectId))
        {
            Clear();
            return;
        }
        ObjdsPlans.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        ObjectDataSourceRegisteredNo.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        SetPlansMethodFileds(ProjectId);
        ObjectDataSourceBlock.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        ObjectDataSourceOwner.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        ObjectDataSourceDesignerPlans.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        ObjectDataSourceProjectImplementer.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
        GridViewPlanSubRe.DataBind();
        
        
    }

    private bool SetProjectFileds(int ProjectId)
    {
        ObjectDataSourcePlansControlerViewPoint.SelectParameters["PlansId"].DefaultValue = "-2";
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

        ProjectManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (ProjectManager.Count == 1)
        {
            ASPxTextBoxCode.Text = ProjectId.ToString();
            RegDate.Text = ProjectManager[0]["Date"].ToString();
            ASPxTextBoxProjectName.Text = ProjectManager[0]["ProjectName"].ToString();
            ASPxComboBoxProjectStatus.Value = Convert.ToInt32(ProjectManager[0]["ProjectStatusId"]);
            ASPxTextBoxFileNo.Text = ProjectManager[0]["FileNo"].ToString();
            FileDate.Text = ProjectManager[0]["FileDate"].ToString();
            ASPxComboBoxCity.DataBind();
            ASPxComboBoxCity.Value = Convert.ToInt32(ProjectManager[0]["CitId"]);
            ASPxComboBoxMunicipality.DataBind();
            ASPxComboBoxMunicipality.Value = Convert.ToInt32(ProjectManager[0]["MunId"]);
            ASPxTextBoxReconstructionCode.Text = ProjectManager[0]["ReconstructionCode"].ToString();
            ASPxTextBoxComputerCode.Text = ProjectManager[0]["ComputerCode"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["UsageId"]))
            {
                ASPxComboBoxUsage.DataBind();
                ASPxComboBoxUsage.Value = Convert.ToInt32(ProjectManager[0]["UsageId"]);
            }
            ASPxComboBoxStructureGroups.DataBind();
            ASPxComboBoxStructureGroups.Value = Convert.ToInt32(ProjectManager[0]["GroupId"]);
            ASPxTextBoxFoundation.Text = ProjectManager[0]["Foundation"].ToString();
            ASPxTextBoxArea.Text = ProjectManager[0]["Area"].ToString();
            ASPxTextBoxRecessArea.Text = ProjectManager[0]["RecessArea"].ToString();
            ASPxTextBoxRemainArea.Text = ProjectManager[0]["RemainArea"].ToString();
            ASPxTextBoxDocumentArea.Text = ProjectManager[0]["DocumentArea"].ToString();
            TextBoxAddress.Text = ProjectManager[0]["Address"].ToString();            

            //BlockManager.FindByProjectId(Convert.ToInt32(ProjectId));
            //if (BlockManager.Count > 0)
            //{
            //    ASPxComboBoxStructureGroups.Visible = true;
            //    LblGroup.Visible = true;
            //}
            //else
            //{
            //    ASPxComboBoxStructureGroups.Visible = false;
            //    LblGroup.Visible = false;
            //}

            return true;
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین رکوردی وجود ندارد";
            return false;
        }
    }

    private void SetPlansMethodFileds(int ProjectId)
    {
        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();

        PlansMethodManager.FindConfirmedByProjectId(ProjectId);
        if (PlansMethodManager.Count == 1)
        {
            ASPxTextBoxPlansMethodNo.Text = PlansMethodManager[0]["PlansMethodNo"].ToString();
            PlansMethodDate.Text = PlansMethodManager[0]["RegisteredDate"].ToString();
            ASPxTextBoxTarakom.Text = PlansMethodManager[0]["Tarakom"].ToString();
            ASPxTextBoxEshghalSurface.Text = PlansMethodManager[0]["EshghalSurface"].ToString();
            ASPxComboBoxStructureBuiltPlace.DataBind();
            ASPxComboBoxStructureBuiltPlace.SelectedIndex = ASPxComboBoxStructureBuiltPlace.Items.IndexOfValue(PlansMethodManager[0]["StructureBuiltPlaceId"]);
            ASPxTextBoxAllowableHeight.Text = PlansMethodManager[0]["AllowableHeight"].ToString();
            ASPxTextBoxCommercialLimitation.Text = PlansMethodManager[0]["CommercialLimitation"].ToString();
            ASPxTextBoxBlockNum.Text = PlansMethodManager[0]["BlockNum"].ToString();
            ASPxTextBoxLocationCriterion.Text = PlansMethodManager[0]["LocationCriterion"].ToString();
            ASPxTextBoxMantelet.Text = PlansMethodManager[0]["Mantelet"].ToString();
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }

    private void Clear()
    {
        ClearProject();
        ObjectDataSourceRegisteredNo.SelectParameters["ProjectId"].DefaultValue = "-2";
        ClearPlansMethod();
        ObjectDataSourceBlock.SelectParameters["ProjectId"].DefaultValue = "-2";
        ObjectDataSourceOwner.SelectParameters["ProjectId"].DefaultValue = "-2";
        ObjectDataSourceDesignerPlans.SelectParameters["ProjectId"].DefaultValue = "-2";
        ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = "-2";
        ObjectDataSourceImplementerAgent.SelectParameters["ProjectId"].DefaultValue = "-2";
    }

    private void ClearProject()
    {
        ASPxTextBoxCode.Text = "";
        RegDate.Text = "";
        ASPxTextBoxProjectName.Text = "";
        ASPxComboBoxProjectStatus.DataBind();
        ASPxComboBoxProjectStatus.SelectedIndex = -1;
        ASPxTextBoxFileNo.Text = "";
        FileDate.Text = "";
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.SelectedIndex = -1;
        ASPxComboBoxCity.DataBind();
        ASPxComboBoxCity.SelectedIndex = -1;
        ASPxTextBoxReconstructionCode.Text = "";
        ASPxTextBoxComputerCode.Text = "";
        ASPxComboBoxUsage.DataBind();
        ASPxComboBoxUsage.SelectedIndex = -1;
        ASPxComboBoxStructureGroups.DataBind();
        ASPxComboBoxStructureGroups.SelectedIndex = -1;
        ASPxTextBoxFoundation.Text = "";
        ASPxTextBoxArea.Text = "";
        ASPxTextBoxRecessArea.Text = "";
        ASPxTextBoxRemainArea.Text = "";
        ASPxTextBoxDocumentArea.Text = "";
        TextBoxAddress.Text = "";
    }

    private void ClearPlansMethod()
    {
        ASPxTextBoxPlansMethodNo.Text = "";
        PlansMethodDate.Text = "";
        ASPxTextBoxTarakom.Text = "";
        ASPxTextBoxEshghalSurface.Text = "";
        ASPxComboBoxStructureBuiltPlace.DataBind();
        ASPxComboBoxStructureBuiltPlace.SelectedIndex = -1;
        ASPxTextBoxAllowableHeight.Text = "";
        ASPxTextBoxCommercialLimitation.Text = "";
        ASPxTextBoxBlockNum.Text = "";
        ASPxTextBoxLocationCriterion.Text = "";
        ASPxTextBoxMantelet.Text = "";
    }


    /*****************************************************************************************************************************/
    protected void CustomAspxDevGridViewBlock_FocusedRowChanged(object sender, EventArgs e)
    {
        if (CustomAspxDevGridViewBlock.FocusedRowIndex != -1 && CustomAspxDevGridViewBlock.SettingsDetail.ShowDetailRow)
            CustomAspxDevGridViewBlock.DetailRows.ExpandRow(CustomAspxDevGridViewBlock.FocusedRowIndex);
    }

    protected void CustomAspxDevGridViewBlock_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridViewBlock.FocusedRowIndex = e.VisibleIndex;
    }

    protected void CustomAspxDevGridViewFoundation_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["FoundationId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void CustomAspxDevGridViewWall_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["WallsId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void CustomAspxDevGridViewEntrance_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["EntranceId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void CustomAspxDevGridViewImplementer_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridViewImplementer.FocusedRowIndex = e.VisibleIndex;
    }
    protected void CustomAspxDevGridViewImplementer_FocusedRowChanged(object sender, EventArgs e)
    {
        if (CustomAspxDevGridViewImplementer.FocusedRowIndex != -1 && CustomAspxDevGridViewImplementer.SettingsDetail.ShowDetailRow)
            CustomAspxDevGridViewImplementer.DetailRows.ExpandRow(CustomAspxDevGridViewImplementer.FocusedRowIndex);
    }
    protected void CustomAspxDevGridViewImplementerAgent_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["ImpId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

}