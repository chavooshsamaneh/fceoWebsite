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
using System.IO;
using DevExpress.Web;

public partial class Members_TechnicalServices_Project_PlanInfo : System.Web.UI.Page
{
    string ProjectId;
    string PrjReId;
    string PlansId;
    string PlanPageMode;
    string PrjPgMd;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["PrjId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PrePgMd"]))
            {
                Response.Redirect("Project.aspx?" + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansControlerViewPointManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            if (Request.QueryString["PlanPageMode"] == null || Request.QueryString["PlnId"] == null || !per.CanView)
            {
                string QS = "ProjectId=" + Request.QueryString["PrjId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PrePgMd"].ToString() +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
                Response.Redirect("Plans.aspx?" + QS);
            }

            SetKeys();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + HiddenFieldPrjDes["ProjectId"].ToString() +
                    "&PrjReId=" + HiddenFieldPrjDes["PrjReId"].ToString() +
                    "&PageMode=" + HiddenFieldPrjDes["PrjPgMd"].ToString()
                       + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("Plans.aspx?" + QS);
    }

    /****************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HiddenFieldPrjDes["PlansId"] = Request.QueryString["PlnId"].ToString();
            HiddenFieldPrjDes["PlnPgMd"] = Request.QueryString["PlanPageMode"];
            HiddenFieldPrjDes["ProjectId"] = Request.QueryString["PrjId"];
            HiddenFieldPrjDes["PrjReId"] = Request.QueryString["PrjReId"];
            HiddenFieldPrjDes["PrjPgMd"] = Request.QueryString["PrePgMd"];

            PrjReId = Utility.DecryptQS(HiddenFieldPrjDes["PrjReId"].ToString());
            PlansId = Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString());
            ProjectId = Utility.DecryptQS(HiddenFieldPrjDes["ProjectId"].ToString());
            PlanPageMode = Utility.DecryptQS(HiddenFieldPrjDes["PlnPgMd"].ToString());
            PrjPgMd = Utility.DecryptQS(HiddenFieldPrjDes["PrjPgMd"].ToString());

            if (string.IsNullOrEmpty(PlanPageMode) || string.IsNullOrEmpty(PrjPgMd) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PlansId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            FillProjectInfo(int.Parse(PrjReId));
            FillForm();
        }
        catch (Exception Err)
        {
            Utility.SaveWebsiteError(Err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    /****************************************************************************************************************************************/
    private void FillForm()
    {
        PlansId = Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString());
        FillPlan(Convert.ToInt32(PlansId));
        FillPlanAttachment(PlansId);
        FillDesignerPlans(PlansId);
        FillPlansControlerViewPoint(PlansId);
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    private void FillPlan(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            txtPlanDes.Text = PlansManager[0]["Description"].ToString();
            txtPlanNo.Text = PlansManager[0]["No"].ToString();
            txtPlanType.Text = PlansManager[0]["Title"].ToString();
        }
    }

    private void FillPlanAttachment(string PlansId)
    {
        ObjectDataSourceAttachments.SelectParameters["TableTypeId"].DefaultValue = PlansId;
        ObjectDataSourceAttachments.SelectParameters["TableType"].DefaultValue = ((int)TSP.DataManager.TableCodes.TSPlans).ToString();
        ObjectDataSourceAttachments.SelectParameters["AttachTypeId"].DefaultValue = "-1";
    }

    private void FillDesignerPlans(string PlansId)
    {
        ObjectDataSourceDesignerPlans.SelectParameters["PlansId"].DefaultValue = PlansId;
    }

    private void FillPlansControlerViewPoint(string PlansId)
    {
        ObjectDataSourcePlansControlerViewPoint.SelectParameters["PlansId"].DefaultValue = PlansId;
    }

    /****************************************************************************************************************************************/
    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    /****************************************************************************************************************************************/
    protected void MenuPlan_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "PrjReId=" + HiddenFieldPrjDes["PrjReId"].ToString() +
                    "&PlnId=" + HiddenFieldPrjDes["PlansId"].ToString()
                      + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (e.Item.Name)
        {
            case "Plan":
                QS = QS + "&PrjId=" + HiddenFieldPrjDes["ProjectId"].ToString() + "&PrePgMd=" + HiddenFieldPrjDes["PrjPgMd"].ToString()
                    + "&PlanPageMode=" + HiddenFieldPrjDes["PlnPgMd"].ToString();
                Response.Redirect("AddPlans.aspx?" + QS);
                break;

            case "PlanDes":
                QS = QS + "&ProjectId=" + HiddenFieldPrjDes["ProjectId"].ToString() + "&PageMode=" + HiddenFieldPrjDes["PrjPgMd"].ToString()
                    + "&PlanPageMode=" + HiddenFieldPrjDes["PlnPgMd"].ToString();
                Response.Redirect("PlanDesigner.aspx?" + QS);
                break;
        }
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = "مشاهده";
        hp.NavigateUrl = "~/Image/TechnicalServices/Plans/" + Path.GetFileName(hp.Value.ToString());
    }

    protected void GridViewDesigner_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewDesigner_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    /****************************************************************************************************************************************/

}
