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

public partial class Employee_TechnicalServices_Project_JobHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx?GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
            }

            TSP.DataManager.Permission per = TSP.DataManager.ProjectJobHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            CustomAspxDevGridView1.Visible = per.CanView;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (string.IsNullOrEmpty(Request.QueryString["PrjImpId"])))
            {
                Response.Redirect("Implementer.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"])
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"])
                       + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
                return;
            }

            SetKeys();

            this.ViewState["GridView"] = CustomAspxDevGridView1.Visible;
        }

        if (this.ViewState["GridView"] != null)
            CustomAspxDevGridView1.Visible = (bool)this.ViewState["GridView"];
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int JhId = -1;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            JhId = (int)row["JhId"];
        }
        if (JhId == -1)
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Response.Redirect("JobHistoryShow.aspx?ProjectId=" + HDProjectId.Value
                + "&PrjImpId=" + HDImpId.Value + "&PageMode=" + Request.QueryString["PageMode"]
                + "&PageMode2=" + PgMode.Value + "&PrjReId=" + RequestId.Value
                + "&JhId=" + Utility.EncryptQS(JhId.ToString())
                   + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        //Response.Redirect("ImplementerInsert.aspx?ProjectId=" + HDProjectId.Value + "&PrjImpId=" + HDImpId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&PageMode2=" + PgMode.Value + "&PrjReId=" + RequestId.Value);
        Response.Redirect("Implementer.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    /******************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();
            HDImpId.Value = Server.HtmlDecode(Request.QueryString["PrjImpId"]).ToString();

            string ImpId = Utility.DecryptQS(HDImpId.Value);
            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string MPageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"]).ToString());


            if (string.IsNullOrEmpty(ImpId) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(MPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetDataSource(Convert.ToInt32(ImpId));
            SetMenuImpEnabled();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    private void SetDataSource(int ImpId)
    {
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindByPrjImpId(ImpId);
        if (ProjectImpManager.Count > 0)
        {
            int MemberTypeId = int.Parse(ProjectImpManager[0]["MemberTypeId"].ToString());
            string MeOfficeId = ProjectImpManager[0]["MeOfficeId"].ToString();

            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
            {
                ObjectDataSource1.SelectParameters[0].DefaultValue = MeOfficeId;
                ObjectDataSource1.SelectParameters[4].DefaultValue = ((int)TSP.DataManager.TableCodes.MemberRequest).ToString();
            }
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
            {
                ObjectDataSource1.SelectParameters[0].DefaultValue = MeOfficeId;
                ObjectDataSource1.SelectParameters[4].DefaultValue = ((int)TSP.DataManager.TableCodes.OfficeRequest).ToString();
            }
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private bool CheckIsMother()
    {
        int PrjImpId = Convert.ToInt32(Utility.DecryptQS(HDImpId.Value));
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindImpMother(ProjectId);
        if (ProjectImpManager.Count > 0 && Convert.ToInt32(ProjectImpManager[0]["PrjImpId"]) == PrjImpId)
            return true;
        return false;
    }

    private string GetMemberTypeId()
    {
        int PrjImpId = Convert.ToInt32(Utility.DecryptQS(HDImpId.Value));

        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindByPrjImpId(PrjImpId);
        if (ProjectImpManager.Count > 0)
            return ProjectImpManager[0]["MemberTypeId"].ToString();
        else
            return "-1";
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    /******************************************************************************************************************************************/
    protected void MenuImp_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + HDProjectId.Value
            + "&PrjReId=" + RequestId.Value
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjImpId=" + HDImpId.Value
            + "&PageMode2=" + PgMode.Value
             + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("JobHistory.aspx?" + QS);
                break;

            case "Entezami":
                break;

            case "Control":
                Response.Redirect("ProjectQC.aspx?" + QS + "&MemberTypeId=" + Utility.EncryptQS(GetMemberTypeId()));
                break;

            case "Imp":
                Response.Redirect("ImplementerInsert.aspx?" + QS);
                break;
        }
    }

    private void SetMenuImpEnabled()
    {
        if (CheckIsMother())
            MenuImp.Items[3].Enabled = true;
        else
            MenuImp.Items[3].Enabled = false;
    }

    /******************************************************************************************************************************************/
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "StartCorporateDate":
                e.Cell.Style["direction"] = "ltr";
                break;

            case "EndCorporateDate":
                e.Cell.Style["direction"] = "ltr";
                break;

            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "StartCorporateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "EndCorporateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

}
