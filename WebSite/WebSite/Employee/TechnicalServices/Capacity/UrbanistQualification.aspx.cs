using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_TechnicalServices_Capacity_UrbanistQualification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewUrbQul.JSProperties["cpError"] = 2;
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.UrbanistQualificationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            //btnEdit.Enabled = per.CanEdit;
            //btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            GridViewUrbQul.Visible = per.CanView;

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
    protected void GridViewUrbQul_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TechnicalServices.UrbanistQualificationManager UrbanistQualificationManager = new TSP.DataManager.TechnicalServices.UrbanistQualificationManager();
        GridViewUrbQul.JSProperties["cpError"] = 1;
        try
        {
            DataRow row = UrbanistQualificationManager.NewRow();

            row["Grade"] = e.NewValues["Grade"];
            row["CreateDate"] = Utility.GetDateOfToday();
            row["InActive"] = 0;
            row["QualificationMeter"] = e.NewValues["QualificationMeter"];
            row["QualificationType"] = e.NewValues["QualificationType"];
            row["Limit"] = e.NewValues["Limit"];


            row["Count"] = e.NewValues["Count"];


            //row["Description"] = e.NewValues["Description"];
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            UrbanistQualificationManager.AddRow(row);
            UrbanistQualificationManager.Save();
            UrbanistQualificationManager.DataTable.AcceptChanges();


            GridViewUrbQul.JSProperties["cpMsg"] = "ذخیره انجام گرفت.";

            GridViewUrbQul.CancelEdit();
            GridViewUrbQul.DataBind();
        }
        catch (Exception err)
        {
            e.Cancel = true;

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewUrbQul.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewUrbQul.JSProperties["cpMsg"] = "کد شهر تکراری می باشد.";
                }
                else
                {
                    GridViewUrbQul.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewUrbQul.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void GridViewUrbQul_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) { }
    protected void GridViewUrbQul_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewUrbQul.JSProperties["cpError"] = 1;
        TSP.DataManager.TechnicalServices.UrbanistQualificationManager UrbanistQualificationManager = new TSP.DataManager.TechnicalServices.UrbanistQualificationManager();
        try
        {

            UrbanistQualificationManager.FindByUrbQulId(Convert.ToInt32(e.Keys["UrbQulId"]));
            if (UrbanistQualificationManager.Count == 1 && !Convert.ToBoolean(UrbanistQualificationManager[0]["InActive"]))
            {
                UrbanistQualificationManager[0].BeginEdit();

                UrbanistQualificationManager[0]["InActive"] = 1;
                UrbanistQualificationManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                UrbanistQualificationManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                UrbanistQualificationManager[0]["ModifiedDate"] = DateTime.Now;

                UrbanistQualificationManager[0].EndEdit();
                if (UrbanistQualificationManager.Save() > 0)
                {
                    GridViewUrbQul.JSProperties["cpMsg"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewUrbQul.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                }
                GridViewUrbQul.CancelEdit();

                GridViewUrbQul.DataBind();

            }
            else
            {
                GridViewUrbQul.JSProperties["cpMsg"] = "قبلا این مورد غیرفعال شده است.";
            }

        }
        catch (Exception err)
        {
            throw;
        }
    }
}