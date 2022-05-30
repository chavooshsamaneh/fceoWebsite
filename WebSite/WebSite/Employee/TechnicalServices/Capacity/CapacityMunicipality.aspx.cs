using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_TechnicalServices_Capacity_CapacityMunicipality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewCapMunicipality.JSProperties["cpError"] = 2;
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            //btnEdit.Enabled = per.CanEdit;
            //btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            GridViewCapMunicipality.Visible = per.CanView;

            //this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        //if (this.ViewState["BtnEdit"] != null)
        //    this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void GridViewCapMunicipality_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager CapacityInMunicipalityManager = new TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager();
        GridViewCapMunicipality.JSProperties["cpError"] = 1;
        try
        {
            DataRow row = CapacityInMunicipalityManager.NewRow();

            row["MjId"] = e.NewValues["MjId"];
            row["CreateDate"] = Utility.GetDateOfToday();
            row["InActive"] = 0;
            if (!Utility.IsDBNullOrNullValue(e.NewValues["GrdObsId"]))
                row["GrdObsId"] = e.NewValues["GrdObsId"];
            else
                row["GrdObsId"] = DBNull.Value;
            if (!Utility.IsDBNullOrNullValue(e.NewValues["GrdDesId"]))
                row["GrdDesId"] = e.NewValues["GrdDesId"];
            else
                row["GrdDesId"] = DBNull.Value;
            if (!Utility.IsDBNullOrNullValue(e.NewValues["GrdUrbanismId"]))
                row["GrdUrbanismId"] = e.NewValues["GrdUrbanismId"];
            else
                row["GrdUrbanismId"] = DBNull.Value;
            if (!Utility.IsDBNullOrNullValue(e.NewValues["MaxDesCapacity"]))
                row["MaxDesCapacity"] = e.NewValues["MaxDesCapacity"];
            else
                row["MaxDesCapacity"] = 0;

            if (!Utility.IsDBNullOrNullValue(e.NewValues["MaxObsCapacity"]))
                row["MaxObsCapacity"] = e.NewValues["MaxObsCapacity"];
            else
                row["MaxObsCapacity"] = 0;

            row["MaxObsCapacity"] = e.NewValues["MaxObsCapacity"];

            if (!Utility.IsDBNullOrNullValue(e.NewValues["MaxUrbenismTarhShahrsazi"]))
                row["MaxUrbenismTarhShahrsazi"] = e.NewValues["MaxUrbenismTarhShahrsazi"];
            else
                row["MaxUrbenismTarhShahrsazi"] = 0;
            if (!Utility.IsDBNullOrNullValue(e.NewValues["MaxUrbenismEntebaghShahri"]))
                row["MaxUrbenismEntebaghShahri"] = e.NewValues["MaxUrbenismEntebaghShahri"];
            else
                row["MaxUrbenismEntebaghShahri"] = 0;

            row["Description"] = e.NewValues["Description"];
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            CapacityInMunicipalityManager.AddRow(row);
            CapacityInMunicipalityManager.Save();
            CapacityInMunicipalityManager.DataTable.AcceptChanges();


            GridViewCapMunicipality.JSProperties["cpMsg"] = "ذخیره انجام گرفت.";

            GridViewCapMunicipality.CancelEdit();
            GridViewCapMunicipality.DataBind();
        }
        catch (Exception err)
        {
            e.Cancel = true;

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewCapMunicipality.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewCapMunicipality.JSProperties["cpMsg"] = "کد شهر تکراری می باشد.";
                }
                else
                {
                    GridViewCapMunicipality.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewCapMunicipality.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void GridViewCapMunicipality_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) { }

    protected void GridViewCapMunicipality_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewCapMunicipality.JSProperties["cpError"] = 1;
        TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager CapacityInMunicipalityManager = new TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager();
        try
        {
            CapacityInMunicipalityManager.FindByMunCapacityId(Convert.ToInt32(e.Keys["MunCapacityId"]));
            if (CapacityInMunicipalityManager.Count == 1 && !Convert.ToBoolean(CapacityInMunicipalityManager[0]["InActive"]))
            {
                CapacityInMunicipalityManager[0].BeginEdit();

                CapacityInMunicipalityManager[0]["InActive"] = 1;
                CapacityInMunicipalityManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                CapacityInMunicipalityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                //CapacityInMunicipalityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                CapacityInMunicipalityManager[0]["ModifiedDate"] = DateTime.Now;

                CapacityInMunicipalityManager[0].EndEdit();
                if (CapacityInMunicipalityManager.Save() > 0)
                {
                    GridViewCapMunicipality.JSProperties["cpMsg"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewCapMunicipality.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                }
                GridViewCapMunicipality.CancelEdit();

                GridViewCapMunicipality.DataBind();

            }
            else
            {
                GridViewCapMunicipality.JSProperties["cpMsg"] = "قبلا این مورد غیرفعال شده است.";
            }

        }
        catch (Exception err)
        {
            throw;
        }
    }
}