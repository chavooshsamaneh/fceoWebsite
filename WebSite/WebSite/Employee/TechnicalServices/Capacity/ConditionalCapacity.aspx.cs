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

public partial class Employee_TechnicalServices_Capacity_ConditionalCapacity : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ConditionalCapacityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
            {
                CustomAspxDevGridViewConditionalCapacity.Visible = false;
            }

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
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("Edit");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("New");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        Print();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        Delete();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ConditionalCapacity";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ConditionalCapacity";
        GridViewExporter.WriteXlsToResponse(true);
    }

    /*************************************************************************************************************/
    private void NextPage(string Mode)
    {
        Int64 ConditionalCapacityId = -1;

        if (CustomAspxDevGridViewConditionalCapacity.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridViewConditionalCapacity.GetDataRow(CustomAspxDevGridViewConditionalCapacity.FocusedRowIndex);
            ConditionalCapacityId = Convert.ToInt64(row["ConditionalCapacityId"]);
        }

        if (ConditionalCapacityId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Response.Redirect("ConditionalCapacityInsert.aspx?ConditionalCapacityId=" + Utility.EncryptQS(ConditionalCapacityId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = CustomAspxDevGridViewConditionalCapacity.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/ConditionalCapacityReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    /*************************************************************** Delete ******************************************************************/
    private void Delete()
    {
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        Int64 ConditionalCapacityId = -1;
        try
        {
            if (CustomAspxDevGridViewConditionalCapacity.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridViewConditionalCapacity.GetDataRow(CustomAspxDevGridViewConditionalCapacity.FocusedRowIndex);
                ConditionalCapacityId = Convert.ToInt64(row["ConditionalCapacityId"]);

                ConditionalCapacityManager.FindByConditionalCapacityId(Convert.ToInt64(ConditionalCapacityId));
                if (ConditionalCapacityManager.Count == 1 && !Convert.ToBoolean(ConditionalCapacityManager[0]["IsConfirmed"]))
                {
                    ConditionalCapacityManager[0].Delete();
                    ConditionalCapacityManager.Save();

                    CustomAspxDevGridViewConditionalCapacity.DataBind();
                    SetLabelWarning("حذف انجام شد");
                }
                else if (Convert.ToBoolean(ConditionalCapacityManager[0]["IsConfirmed"]))
                {
                    SetLabelWarning("کاهش/افزایش ظرفیت تایید شده است و قابل حذف کردن نمی باشد");
                }
                else
                {
                    SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
                }
            }
            else
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Inactive(Int64 ConditionalCapacityId)
    {
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        ConditionalCapacityManager.FindByConditionalCapacityId(Convert.ToInt64(ConditionalCapacityId));
        if (ConditionalCapacityManager.Count == 1 && !Convert.ToBoolean(ConditionalCapacityManager[0]["IsConfirmed"]))
        {
            ConditionalCapacityManager[0].BeginEdit();
            ConditionalCapacityManager[0]["InActive"] = 1;
            ConditionalCapacityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ConditionalCapacityManager[0]["ModifiedDate"] = DateTime.Now;
            ConditionalCapacityManager[0].EndEdit();

            ConditionalCapacityManager.Save();

            CustomAspxDevGridViewConditionalCapacity.DataBind();
            SetLabelWarning("حذف انجام شد");
        }
        else if (Convert.ToBoolean(ConditionalCapacityManager[0]["IsConfirmed"]))
        {
            SetLabelWarning("کاهش/افزایش ظرفیت تایید شده است و قابل حذف کردن نمی باشد");
        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    /*************************************************************************************************************/
    private string GetRepHeader()
    {
        //string AgentName = GetAgentName();
        //return "نمایندگی : " + AgentName;
        return "";
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
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("کد تکراری می باشد");
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
    /*************************************************************************************************************/
    protected void CustomAspxDevGridViewConditionalCapacity_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        try
        {
            string ToDate = e.GetValue("ToDate").ToString();

            if (String.Compare(ToDate, Utility.GetDateOfToday()) < 0)
            {
                e.Row.ForeColor = System.Drawing.Color.Gray;
                e.Row.Cells[8].Text = "پایان اعتبار";
            }

            bool InActive = Convert.ToBoolean(e.GetValue("InActive"));
            if (InActive)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[8].Text = "غیر فعال";
            }

        }
        catch
        {
        }

    }

    protected void CustomAspxDevGridViewConditionalCapacity_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = CustomAspxDevGridViewConditionalCapacity.Columns;
            Session["DataSource"] = ObjectDataSourceConditionalCapacity;
            Session["Title"] = "کاهش/افزایش ظرفیت";
            Session["Header"] = GetRepHeader();
            CustomAspxDevGridViewConditionalCapacity.DetailRows.CollapseAllRows();
            CustomAspxDevGridViewConditionalCapacity.JSProperties["cpDoPrint"] = 1;
        }
    }

    /************************************************ WorkFlow **************************************************/



}