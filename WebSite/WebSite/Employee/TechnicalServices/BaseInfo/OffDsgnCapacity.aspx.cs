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

public partial class Employee_TechnicalServices_BaseInfo_OffDsgnCapacity : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.DocOffIncreaseJobCapacityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            //btnEdit.Enabled = per.CanEdit;
            //btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanDelete;
            btnInActive2.Enabled = per.CanDelete;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            btnExportExcel.Enabled = per.CanView;
            btnExportExcel2.Enabled = per.CanView;

            if (!per.CanView)
                CustomAspxDevGridView1.Visible = false;

            ObjectDataSourceDocOffIncreaseJobCapacity.SelectParameters["Type"].DefaultValue = ((int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office).ToString();

            this.ViewState["BtnView"] = btnView.Enabled;
            //this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnPrint"] = btnPrint.Enabled;
            this.ViewState["BtnExportExcel"] = btnExportExcel.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        //if (this.ViewState["BtnEdit"] != null)
        //    this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnPrint"];
        if (this.ViewState["BtnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["BtnExportExcel"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = ObjectDataSourceDocOffIncreaseJobCapacity;
        Session["Title"] = "مدیریت ظرفیت اشتغال طراحی/نظارت دفاتر طراحی";
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

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        InActive();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "OffDsgnCapacity";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ComplainsList";
        GridViewExporter.WriteXlsToResponse(true);
    }

    /*************************************************************************************************************/
    private void NextPage(string Mode)
    {
        int InJId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1 && Mode != "New")
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            InJId = (int)row["InJId"];
        }

        if (InJId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string QS = "InJId=" + Utility.EncryptQS(InJId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode);
            Response.Redirect("OffDsgnCapacityInsert.aspx?" + QS);
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = CustomAspxDevGridView1.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/BlockReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    private void InActive()
    {
        int InJId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            InJId = (int)row["InJId"];
        }
        if (InJId == -1)
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            TSP.DataManager.DocOffIncreaseJobCapacityManager DocOffIncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            DocOffIncreaseJobCapacityManager.FindByCode(Convert.ToInt32(InJId));

            if (DocOffIncreaseJobCapacityManager.Count == 1)
            {
                try
                {
                    DocOffIncreaseJobCapacityManager[0].BeginEdit();
                    DocOffIncreaseJobCapacityManager[0]["InActive"] = 1;
                    DocOffIncreaseJobCapacityManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                    DocOffIncreaseJobCapacityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    DocOffIncreaseJobCapacityManager[0]["ModifiedDate"] = DateTime.Now;
                    DocOffIncreaseJobCapacityManager[0].EndEdit();

                    int cn = DocOffIncreaseJobCapacityManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();
                        SetLabelWarning("ذخیره انجام شد");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است");
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

    /*************************************************************************************************************/
    protected void MenuDsgn_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("MemberDsgnCapacity.aspx");
                break;

            case "EngOff":
                Response.Redirect("EngOffDsgnCapacity.aspx");
                break;

            case "Office":
                Response.Redirect("OffDsgnCapacity.aspx");
                break;
        }
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = CustomAspxDevGridView1.Columns;
            Session["DataSource"] = ObjectDataSourceDocOffIncreaseJobCapacity;
            Session["Title"] = "مدیریت ظرفیت اشتغال طراحی/نظارت شخص حقوقی";
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
        }
    }
}