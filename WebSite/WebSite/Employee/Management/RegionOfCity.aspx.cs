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

public partial class Employee_Management_RegionOfCity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            TSP.DataManager.Permission per = TSP.DataManager.RegionOfCityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            CustomAspxDevGridView1.Visible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.RegionOfCityManager RegionOfCityManager = new TSP.DataManager.RegionOfCityManager();
        DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
        try
        {
            RegionOfCityManager.FindByCode((int)row["ReCitId"]);
            if (RegionOfCityManager.Count == 1)
            {
                RegionOfCityManager[0].Delete();
                int cnt = RegionOfCityManager.Save();
                if (cnt == 1)
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }

            }
            CustomAspxDevGridView1.DataBind();
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.RegionOfCityManager RegionOfCityManager = new TSP.DataManager.RegionOfCityManager();
        CustomAspxDevGridView1.JSProperties["cpError"] = 1;
        try
        {
            DataRow row = RegionOfCityManager.NewRow();
            row["ReCitCode"] = e.NewValues["ReCitCode"];
            row["ReCitName"] = e.NewValues["ReCitName"];
            if (e.NewValues["PrId"] != null)
                row["PrId"] = e.NewValues["PrId"];
            else
                e.NewValues["PrId"] = DBNull.Value;
            if (e.NewValues["ReId"] != null)
                row["ReId"] = e.NewValues["ReId"];
            else
                e.NewValues["ReId"] = DBNull.Value;

            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;

            if (e.NewValues["AgentId"] != null)
                row["AgentId"] = e.NewValues["AgentId"];
            else row["AgentId"] = DBNull.Value;

            RegionOfCityManager.AddRow(row);
            int cnt = RegionOfCityManager.Save();
            if (cnt > 0)
            {
               
                CustomAspxDevGridView1.DataBind();
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "ذخیره انجام شد";               
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.DataBind();
        }
        catch (Exception err)
        {
            e.Cancel = true;
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    CustomAspxDevGridView1.JSProperties["cpMsg"] = "کد شهر تکراری می باشد.";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        TSP.DataManager.RegionOfCityManager RegionOfCityManager = new TSP.DataManager.RegionOfCityManager();
        RegionOfCityManager.Fill();
        DataRow row = RegionOfCityManager.DataTable.Rows.Find(e.Keys["ReCitId"]);
        CustomAspxDevGridView1.JSProperties["cpError"] = 1;
        try
        {
            row.BeginEdit();
            row["ReCitCode"] = e.NewValues["ReCitCode"];
            row["ReCitName"] = e.NewValues["ReCitName"];
            if (e.NewValues["PrId"] != null)
                row["PrId"] = e.NewValues["PrId"];
            else
                e.NewValues["PrId"] = DBNull.Value;
            if (e.NewValues["ReId"] != null)
                row["ReId"] = e.NewValues["ReId"];
            else
                e.NewValues["ReId"] = DBNull.Value;

            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;

            if (e.NewValues["AgentId"] != null)
                row["AgentId"] = e.NewValues["AgentId"];
            else row["AgentId"] = DBNull.Value;

            row.EndEdit();

            e.Cancel = true;
            if (RegionOfCityManager.Save() >0)
            {
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "ذخیره انجام شد.";
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
            }
            CustomAspxDevGridView1.CancelEdit();

            CustomAspxDevGridView1.DataBind();
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        //if (e.Column.Name == "AgentId")
        //    e.Editor.Style["dir"] = "ltr";
        //e.Editor.Attributes.Add("direction", "ltr");
    }

    protected void CustomAspxDevGridView1_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        // e.Editor.Attributes.Add("direction", "ltr");
    }

}
