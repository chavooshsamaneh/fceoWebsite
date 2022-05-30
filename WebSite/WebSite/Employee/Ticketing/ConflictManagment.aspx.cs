using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Ticketing_ConflictManagment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewConflictManagment.JSProperties["cpError"] = 2;
           
            TSP.DataManager.Permission per = TSP.DataManager.ConflictManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //btnDelete.Enabled = per.CanDelete;
            //btnDelete2.Enabled = per.CanDelete;
            ////btnEdit.Enabled = per.CanEdit;
            ////btnEdit2.Enabled = per.CanEdit;
            ////BtnNew.Enabled = per.CanNew;
            ////BtnNew2.Enabled = per.CanNew;
            //GridViewConflictManagment.Visible = per.CanView;

            //this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            //this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        //if (this.ViewState["BtnEdit"] != null)
        //    this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        //if (this.ViewState["BtnNew"] != null)
        //    this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }


    protected void GridViewConflictManagment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
    
        GridViewConflictManagment.JSProperties["cpError"] = 1;
        TSP.DataManager.ConflictManager ConflictManager = new TSP.DataManager.ConflictManager();
        try
        {
            ConflictManager.FindByConfId(Convert.ToInt32(e.Keys["ConfId"]));
            if (ConflictManager.Count == 1 && Convert.ToBoolean(ConflictManager[0]["Satisfaied"]))
            {
                ConflictManager[0].BeginEdit();

                ConflictManager[0]["Satisfaied"] = 0;
                ConflictManager[0]["SatisfaiedDate"] = DBNull.Value;
                ConflictManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                //ConflictManager[0]["Description"] = e.NewValues["Description"].ToString() +"  "+ Utility.GetDateOfToday()+" تغییر وضعیت جهت بررسی مجدد ";
               //ConflictManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
               ConflictManager[0]["ModifiedDate"] = DateTime.Now;

                ConflictManager[0].EndEdit();
                if (ConflictManager.Save() > 0)
                {
                    GridViewConflictManagment.JSProperties["cpMsg"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewConflictManagment.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                }
                GridViewConflictManagment.CancelEdit();

                GridViewConflictManagment.DataBind();

            }
            else
            {
                GridViewConflictManagment.JSProperties["cpMsg"] = "وضعیت این مورد هنوز در دست بررسی است.";
            }

        }
        catch (Exception err)
        {
            throw;
        }
    }

    protected void GridViewConflictManagment_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        GridViewConflictManagment.JSProperties["cpError"] = 1;
        TSP.DataManager.ConflictManager ConflictManager = new TSP.DataManager.ConflictManager();
        try
        {
            ConflictManager.FindByConfId(Convert.ToInt32(e.Keys["ConfId"]));
            if (ConflictManager.Count == 1 && !Convert.ToBoolean(ConflictManager[0]["Satisfaied"]))
            {
                ConflictManager[0].BeginEdit();

                ConflictManager[0]["Satisfaied"] = 1;
                ConflictManager[0]["SatisfaiedDate"] = Utility.GetDateOfToday();
                ConflictManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ConflictManager[0]["Description"] = e.NewValues["Description"];
                //.ToString() + "  " + Utility.GetDateOfToday()


                ConflictManager[0]["ModifiedDate"] = DateTime.Now;

                ConflictManager[0].EndEdit();
                if (ConflictManager.Save() > 0)
                {
                    GridViewConflictManagment.JSProperties["cpMsg"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewConflictManagment.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                }
                GridViewConflictManagment.CancelEdit();

                GridViewConflictManagment.DataBind();

            }
            else
            {
                GridViewConflictManagment.JSProperties["cpMsg"] = " این مورد قبلا بسته شده است.";
            }

        }
        catch (Exception err)
        {
            throw;
        }
    }
}