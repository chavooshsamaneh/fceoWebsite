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

public partial class Employee_Management_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

       
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.RegionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Page.IsValid)
        {
            TSP.DataManager.RegionManager RegManager = new TSP.DataManager.RegionManager();
            try
            {
                DataRow row = RegManager.NewRow();
                row["Name"] = e.NewValues["Name"];
                row["Score"] = e.NewValues["Score"];
                row["UserId"] = Utility.GetCurrentUser_UserId();

                row["ModifiedDate"] = DateTime.Now;
                RegManager.AddRow(row);

                int cnt = RegManager.Save();
                if (cnt > 0)
                {
                    CustomAspxDevGridView1.CancelEdit();

                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            catch (Exception err)
            {
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
            e.Cancel = true;
        }
    }
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        TSP.DataManager.RegionManager RegManager = new TSP.DataManager.RegionManager();
        RegManager.Fill();
        DataRow row = RegManager.DataTable.Rows.Find(e.Keys["ReId"]);
        if (row != null)
        {
            try
            {
                row.BeginEdit();
                row["Name"] = e.NewValues["Name"];
                row["Score"] = e.NewValues["Score"];
                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                row.EndEdit();


                int cn = RegManager.Save();
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
        TSP.DataManager.RegionManager RegManager = new TSP.DataManager.RegionManager();

        DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);

        try
        {
            RegManager.FindByCode((int)row["ReId"]);
            if (RegManager.Count == 1)
            {
                RegManager[0].Delete();
                int cnt = RegManager.Save();
                if (cnt == 1)
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
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
  
  
}
