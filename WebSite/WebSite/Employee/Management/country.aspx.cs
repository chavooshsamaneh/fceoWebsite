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

public partial class Employee_Management_country : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.CountryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
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
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void ObjectDataSource1_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }
    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;

        if (Page.IsValid)
        {
            TSP.DataManager.CountryManager CounManager = new TSP.DataManager.CountryManager();

            try
            {
                DataRow d = CounManager.NewRow();
                d["CounCode"] = e.NewValues["CounCode"];
                d["CounName"] = e.NewValues["CounName"];
                d["Description"] = e.NewValues["Description"];
                d["UserId"] = Utility.GetCurrentUser_UserId();
                d["ModifiedDate"] = DateTime.Now;
                CounManager.AddRow(d);
                int cnt = CounManager.Save();

                if (cnt > 0)
                {
                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                CustomAspxDevGridView1.CancelEdit();


            }
            catch (Exception err)
            {
                CustomAspxDevGridView1.CancelEdit();

                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

            }
        }

    }
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        TSP.DataManager.CountryManager CounManager = new TSP.DataManager.CountryManager();
        CounManager.Fill();
        DataRow row = CounManager.DataTable.Rows.Find(e.Keys["CounId"]);
        if (row != null)
        {
            try
            {
                row.BeginEdit();
                row["CounCode"] = e.NewValues["CounCode"];
                row["CounName"] = e.NewValues["CounName"];
                row["Description"] = e.NewValues["Description"];

                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                row.EndEdit();


                int cn = CounManager.Save();
                if (cn > 0)
                {
                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                CustomAspxDevGridView1.CancelEdit();

            }
            catch (Exception err)
            {
                CustomAspxDevGridView1.CancelEdit();

                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.CountryManager CounManager = new TSP.DataManager.CountryManager();
        DataRow Row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
        try
        {
            CounManager.FindByCode((int)Row["CounId"]);
            if (CounManager.Count == 1)
            {
                CounManager[0].Delete();
                int cn = CounManager.Save();
                if (cn == 1)
                {
                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
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
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
            }
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
    protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }
}
