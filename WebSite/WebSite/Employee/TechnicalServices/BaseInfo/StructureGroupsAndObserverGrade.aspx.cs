using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_TechnicalServices_BaseInfo_StructureGroupsAndObserverGrade : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewStructureGroups.JSProperties["cpError"] = 2;
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.StructureGroupsManager.GetUserPermissionGetUserPermissionTSStructureGroupsAndObserverGrade(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            GridViewStructureGroups.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
           
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
     
        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }     

    protected void GridViewStructureGroups_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;


        TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager = new TSP.DataManager.TechnicalServices.StructureGroupsManager();
        StructureGroupsManager.FindByGroupId(Convert.ToInt32( e.Keys["GroupId"]));       
        GridViewStructureGroups.JSProperties["cpError"] = 1;
        try
        {
            StructureGroupsManager[0].BeginEdit();
            StructureGroupsManager[0]["Grade1"] = e.NewValues["Grade1"];
            StructureGroupsManager[0]["Grade2"] = e.NewValues["Grade2"];
            StructureGroupsManager[0]["Grade3"] = e.NewValues["Grade3"];
            StructureGroupsManager[0]["Grade4"] = e.NewValues["Grade4"];

            StructureGroupsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            StructureGroupsManager[0]["ModifiedDate"] = DateTime.Now;          

            StructureGroupsManager[0].EndEdit();

            e.Cancel = true;
            if (StructureGroupsManager.Save() > 0)
            {
                GridViewStructureGroups.JSProperties["cpMsg"] = "ذخیره انجام شد.";
            }
            else
            {
                GridViewStructureGroups.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
            }
            GridViewStructureGroups.CancelEdit();

            GridViewStructureGroups.DataBind();
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewStructureGroups.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    GridViewStructureGroups.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewStructureGroups.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }
        
}