using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_Reports_EngOfficePrintingHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission perCardRequest = TSP.DataManager.MemberManager.GetUserPermissionForEngOfficeDocumentPrintReport(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnPrint2.Enabled = btnPrint.Enabled = perCardRequest.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = perCardRequest.CanView;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        objdsPrintingHistory.SelectParameters["TableType"].DefaultValue = ((int)TSP.DataManager.TableType.EngOffFile).ToString();
        objdsPrintingHistory.SelectParameters["TableTypeMe"].DefaultValue = ((int)TSP.DataManager.TableType.MemberCardRequestPrint).ToString();
        objdsPrintingHistory.SelectParameters["TableTypeEngOff"].DefaultValue = ((int)TSP.DataManager.TableType.EngOffFile).ToString();
        objdsPrintingHistory.SelectParameters["TableTypeOffice"].DefaultValue = ((int)TSP.DataManager.TableType.OfficeRequest).ToString();
        GridViewMemberRequest.DataBind();
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "EngOfficeDocumentReport";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridViewMemberRequest_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    GridViewMemberRequest.JSProperties["cpPrint"] = 1;

                    Session["DataTable"] = GridViewMemberRequest.Columns;
                    Session["DataSource"] = objdsPrintingHistory;

                    Session["Title"] = "گزارش چاپ پروانه اشتغال به کار دفاتر مهندسی";
                    break;
            }
        }
    }
}