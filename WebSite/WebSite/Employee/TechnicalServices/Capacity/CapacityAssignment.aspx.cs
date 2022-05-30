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

public partial class Employee_TechnicalServices_BaseInfo_CapacityAssignment : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.CapacityAssignmentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
                CustomAspxDevGridViewCapacityAssignment.Visible = false;
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
        Delete();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "CapacityAssignment";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "CapacityAssignment";
        GridViewExporter.WriteXlsToResponse(true);
    }


    /*************************************************************************************************************/
    private void NextPage(string Mode)
    {
        int CapacityAssignmentId = -1;

        if (CustomAspxDevGridViewCapacityAssignment.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridViewCapacityAssignment.GetDataRow(CustomAspxDevGridViewCapacityAssignment.FocusedRowIndex);
            CapacityAssignmentId = (int)row["CapacityAssignmentId"];
            //bool IsAssigned = (bool)row["IsAssigned"];
            //if (Mode == "Edit" && IsAssigned)
            //{
            //    SetLabelWarning("این مرحله اختصاص داده شده و قابل ویرایش نمی باشد");
            //    return;
            //}
        }

        if (Mode == "New" && !CheckPrcntsSum()) return;

        if (CapacityAssignmentId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Response.Redirect("CapacityAssignmentInsert.aspx?CapacityAssignmentId=" + Utility.EncryptQS(CapacityAssignmentId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = CustomAspxDevGridViewCapacityAssignment.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/CapacityAssignmentReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    /*************************************************************** Delete ******************************************************************/
    private void Delete()
    {
        int CapacityAssignmentId = -1;
        try
        {
            if (CustomAspxDevGridViewCapacityAssignment.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridViewCapacityAssignment.GetDataRow(CustomAspxDevGridViewCapacityAssignment.FocusedRowIndex);
                CapacityAssignmentId = (int)row["CapacityAssignmentId"];

                Inactive(CapacityAssignmentId);
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

    private void Inactive(int CapacityAssignmentId)
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        CapacityAssignmentManager.FindByCapacityAssignmentId(Convert.ToInt32(CapacityAssignmentId));
        if (CapacityAssignmentManager.Count == 1 && !Convert.ToBoolean(CapacityAssignmentManager[0]["IsAssigned"]))
        {
            CapacityAssignmentManager[0].BeginEdit();
            CapacityAssignmentManager[0]["InActive"] = 1;
            CapacityAssignmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            CapacityAssignmentManager[0]["ModifiedDate"] = DateTime.Now;
            CapacityAssignmentManager[0].EndEdit();

            CapacityAssignmentManager.Save();

            CustomAspxDevGridViewCapacityAssignment.DataBind();
            SetLabelWarning("حذف انجام شد");
        }
        else if (Convert.ToBoolean(CapacityAssignmentManager[0]["IsAssigned"]))
        {
            SetLabelWarning("این مرحله اختصاص داده شده است و قابل حذف کردن نمی باشد");
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

    /// <summary>
    /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
    /// </summary> 
    private ArrayList GetPrcntsSum()
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        return CapacityAssignmentManager.GetPrcntsSum(Utility.GetYearOfToday());
    }

    private bool CheckPrcntsSum()
    {
        //SumArr ----> ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum

        ArrayList SumArr = GetPrcntsSum();
        if (SumArr.Count > 0)
        {
            int CapacityPrcntSum = Convert.ToInt32(SumArr[0]);
            int JobCountPrcntSum = Convert.ToInt32(SumArr[1]);

            if (CapacityPrcntSum == 100 && JobCountPrcntSum == 100)
            {
                SetLabelWarning("حاصل جمع مقدار ظرفیت و مقدار تعداد کار برابر 100 می باشد و نمی توان مرحله جدید تعریف کرد");
                return false;
            }

            if (CapacityPrcntSum == 100)
            {
                SetLabelWarning("حاصل جمع مقدار ظرفیت برابر 100 می باشد و نمی توان مقدار جدید برای آن تعریف کرد");
                return false;
            }

            if (JobCountPrcntSum == 100)
            {
                SetLabelWarning("حاصل جمع مقدار تعداد کار برابر 100 می باشد و نمی توان مقدار جدید برای آن تعریف کرد");
                return false;
            }
        }
        return true;
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
    protected void CustomAspxDevGridViewCapacityAssignment_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "AssignmentDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridViewCapacityAssignment_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "AssignmentDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridViewCapacityAssignment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            ArrayList DeletedColumnsName = new ArrayList();
            DeletedColumnsName.Add("RemainIsWaste");
            DeletedColumnsName.Add("IsAssigned");

            Session["DeletedColumnsName"] = DeletedColumnsName;
            Session["DataTable"] = CustomAspxDevGridViewCapacityAssignment.Columns;
            Session["DataSource"] = ObjectDataSourceCapacityAssignment;
            Session["Title"] = "اختصاص ظرفیت";
            Session["Header"] = GetRepHeader();
            CustomAspxDevGridViewCapacityAssignment.DetailRows.CollapseAllRows();
            CustomAspxDevGridViewCapacityAssignment.JSProperties["cpDoPrint"] = 1;
        }
    }

    /************************************************ WorkFlow **************************************************/


}
