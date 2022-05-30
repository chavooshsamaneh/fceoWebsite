using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Capacity_QueueListMunicipality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.TSQueueListMunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
          btnExportExcel.Visible=btnExportExcel2.Visible=  GridViewQueue.Visible = per.CanView;
            this.ViewState["btnExportExcel"] = btnExportExcel.ClientEnabled;

        }
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Visible = this.btnExportExcel2.Visible = (bool)this.ViewState["btnExportExcel"];
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        GridViewQueue.DataBind();
        GridViewExporter.FileName = "ReportQueue";
        GridViewExporter.WriteXlsToResponse(true);
    }
}