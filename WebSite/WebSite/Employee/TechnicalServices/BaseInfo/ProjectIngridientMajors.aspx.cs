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

public partial class Employee_TechnicalServices_BaseInfo_ProjectIngridientMajors : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            btnExportExcel.Enabled = per.CanView;
            btnExportExcel2.Enabled = per.CanView;
            
            if (!per.CanView)
                CustomAspxDevGridView1.Visible = false;
            
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnPrint"] = btnPrint.Enabled;
            this.ViewState["BtnExportExcel"] = btnExportExcel.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnPrint"];
        if (this.ViewState["BtnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["BtnExportExcel"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteProjectIngridientMajors();
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = CustomAspxDevGridView1.Columns;
            Session["DataSource"] = ObjectDataSourceProjectIngridientMajors;
            Session["Title"] = "رشته های مجاز عوامل پروژه";
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "ProjectIngridientMajors";
        GridViewExporter.WriteXlsToResponse(true);
    }

    /*************************************************************************************************************/
    private void NextPage(string Mode)
    {
        int ProjectIngridientMajorsId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1 && Mode != "New")
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ProjectIngridientMajorsId = (int)row["ProjectIngridientMajorsId"];
        }

        if (ProjectIngridientMajorsId == -1 && Mode != "New")
        {
            SetLabelWarning( "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string QS = "PIMId=" + Utility.EncryptQS(ProjectIngridientMajorsId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode);
            Response.Redirect("ProjectIngridientMajorsInsert.aspx?" + QS);
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = CustomAspxDevGridView1.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/BlockReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    private void DeleteProjectIngridientMajors()
    {
        int ProjectIngridientMajorsId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ProjectIngridientMajorsId = (int)row["ProjectIngridientMajorsId"];
        }

        if (ProjectIngridientMajorsId == -1)
        {
            SetLabelWarning( "لطفاًابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
            ProjectIngridientMajorsManager.FindByProjectIngridientMajorsId(Convert.ToInt32(ProjectIngridientMajorsId));

            if (ProjectIngridientMajorsManager.Count == 1)
            {
                try
                {
                    ProjectIngridientMajorsManager[0].Delete();
                    int cn = ProjectIngridientMajorsManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();
                        SetLabelWarning( "حذف انجام شد");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در حذف انجام گرفته است");
                    }
                }
                catch (Exception err)
                {
                    SetError(err);
                }
            }
        }
    }

    /*************************************************************************************************************/
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
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
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

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
}