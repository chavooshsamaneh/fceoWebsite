using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_HomePage_SubSystems : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.SubSystemManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            GridViewSubSystem.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            GridViewSubSystem.DataBind();

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnActive"] != null)
            this.btnActive.Enabled = this.btnActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        GridViewSubSystem.JSProperties["cpMessage"] = "";

        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int SubSysId = -1;
        if (GridViewSubSystem.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewSubSystem.GetDataRow(GridViewSubSystem.FocusedRowIndex);
                SubSysId = Convert.ToInt32(Row["SubSysId"]);
                TSP.DataManager.SubSystemManager SubSystemManager = new TSP.DataManager.SubSystemManager();
                SubSystemManager.FindByCode(SubSysId);
                if (SubSystemManager.Count > 0)
                {
                    DataRow EditRow = SubSystemManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = true;
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        int cn = SubSystemManager.Save();
                        if (cn > 0)
                        {
                            GridViewSubSystem.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int SubSysId = -1;
        if (GridViewSubSystem.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewSubSystem.GetDataRow(GridViewSubSystem.FocusedRowIndex);
                SubSysId = Convert.ToInt32(Row["SubSysId"]);
                TSP.DataManager.SubSystemManager SubSystemManager = new TSP.DataManager.SubSystemManager();
                SubSystemManager.FindByCode(SubSysId);
                if (SubSystemManager.Count > 0)
                {
                    DataRow EditRow = SubSystemManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = false;
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        int cn = SubSystemManager.Save();
                        if (cn > 0)
                        {
                            GridViewSubSystem.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void GridViewSubSystem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertSubsystem(e);
        }
    }

    protected void GridViewSubSystem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditSubSystem(e);
    }
    #endregion

    #region Methods
    private void InsertSubsystem(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.SubSystemManager SubSystemManager = new TSP.DataManager.SubSystemManager();
        try
        {
            DataRow Row = SubSystemManager.NewRow();
            Row["SubSysName"] = e.NewValues["SubSysName"].ToString();
            Row["SubSysLink"] = e.NewValues["SubSysLink"].ToString();
            Row["InActive"] = false;
            Row["UserId"] = Utility.GetCurrentUser_UserId();
            Row["ModifiedDate"] = DateTime.Now;
            Row["CreateDate"] = Utility.GetDateOfToday();
            SubSystemManager.AddRow(Row);
            int cn = SubSystemManager.Save();
            if (cn > 0)
            {
                GridViewSubSystem.JSProperties["cpMessage"] = "ذخیره انجام شد.";
            }
            else
            {
                GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
            GridViewSubSystem.CancelEdit();
            GridViewSubSystem.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewSubSystem.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }

    private void EditSubSystem(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.SubSystemManager SubSystemManager = new TSP.DataManager.SubSystemManager();
            SubSystemManager.FindByCode(Convert.ToInt32(e.Keys[0]));
            DataRow Row = SubSystemManager[0];

            if (Row != null)
            {
                Row.BeginEdit();
                Row["SubSysName"] = e.NewValues["SubSysName"];
                Row["SubSysLink"] = e.NewValues["SubSysLink"];
                Row["UserId"] = Utility.GetCurrentUser_UserId();
                Row["ModifiedDate"] = DateTime.Now;
                Row.EndEdit();
                int cn = SubSystemManager.Save();
                if (cn > 0)
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                }

            }
            GridViewSubSystem.CancelEdit();
            GridViewSubSystem.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewSubSystem.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewSubSystem.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}