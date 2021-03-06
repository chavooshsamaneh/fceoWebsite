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

public partial class Employee_Management_Partition : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.PartitionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnInActive.Enabled =   btnInActive1.Enabled =
            btnEdit.Enabled =btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled =  btnNew2.Enabled =  per.CanNew;
            CustomAspxDevGridView1.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive1.Enabled = this.btnInActive.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }

    protected void ObjectDataSource1_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }

    protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.Cancel = true;
    }

    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;

        if (Page.IsValid)
        {
            TSP.DataManager.PartitionManager PartManager = new TSP.DataManager.PartitionManager();

            try
            {
                DataRow d = PartManager.NewRow();
                //d["ComId"] = 1;
                d["PartName"] = e.NewValues["PartName"];
                d["AgentId"] = e.NewValues["AgentId"];
                d["UserId"] = Utility.GetCurrentUser_UserId();
                d["ModifiedDate"] = DateTime.Now;
                PartManager.AddRow(d);
                int cnt = PartManager.Save();

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

        TSP.DataManager.PartitionManager PartManager = new TSP.DataManager.PartitionManager();
        TSP.DataManager.PartitionManager PartManager2 = new TSP.DataManager.PartitionManager();

        PartManager.Fill();
        DataRow row = PartManager.DataTable.Rows.Find(e.Keys["PartId"]);
        if (row != null)
        {
            try
            {
                PartManager2.FindByCode(int.Parse(row["PartId"].ToString()));
                if (PartManager2.Count == 1)
                {
                    if (Convert.ToBoolean(PartManager2[0]["InActive"]))
                    {
                        CustomAspxDevGridView1.CancelEdit();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش رکورد غیر فعال وجود ندارد";
                        return;
                    }
                }
                row.BeginEdit();
                row["PartName"] = e.NewValues["PartName"];
                row["AgentId"] = e.NewValues["AgentId"];
                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                row.EndEdit();


                int cn = PartManager.Save();
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

    protected void CustomAspxDevGridView1_OnRowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!e.IsNewRow)
        {
            TSP.DataManager.PartitionManager PartManager = new TSP.DataManager.PartitionManager();

            PartManager.FindByCode(Convert.ToInt32(e.Keys["PartId"]));
            //DataRow row = PartManager.DataTable.Rows.Find(e.Keys["PartId"]);
            if (PartManager.Count == 1)
            {
                if (Convert.ToBoolean(PartManager[0]["InActive"]))
                {
                    e.RowError = "امکان ویرایش رکورد غیر فعال وجود ندارد";
                    // CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }
            else
            {
                e.RowError = "خطایی در بازیابی اطلاعات ایجاد شده است";
                // CustomAspxDevGridView1.CancelEdit();
                return;
            }
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.PartitionManager PartManager = new TSP.DataManager.PartitionManager();
        DataRow Row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
        try
        {
            PartManager.FindByCode((int)Row["PartId"]);
            if (PartManager.Count == 1)
            {
                if (Convert.ToBoolean(PartManager[0]["InActive"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                    return;
                }
                else
                {
                    PartManager[0].BeginEdit();
                    PartManager[0]["InActive"] = 1;
                    PartManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                    PartManager[0].EndEdit();
                    int cn = PartManager.Save();
                    if (cn == 1)
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }
    #endregion
}
