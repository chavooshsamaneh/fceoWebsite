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
using DevExpress.Web;

public partial class Employee_TechnicalServices_BaseInfo_DiscountPercent : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.DiscountPercentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.ClientVisible = per.CanView;
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int DiscountPercentId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                DiscountPercentId = (int)row["DiscountPercentId"];

            }
            if (DiscountPercentId == -1)
            {
                SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

            }
            else
            {
                if (DiscountPercentId == 1)
                {
                    SetLabelWarning("امکان غیر فعال کردن رکورد مورد نظر وجود ندارد");
                    return;
                }
                else
                {
                    TSP.DataManager.TechnicalServices.DiscountPercentManager PercentManager = new TSP.DataManager.TechnicalServices.DiscountPercentManager();

                    PercentManager.FindByDiscountPercentId(DiscountPercentId);
                    if (PercentManager.Count == 1)
                    {
                        if (Convert.ToBoolean(PercentManager[0]["InActive"]))
                        {
                            SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                            return;
                        }
                        else
                        {
                            PercentManager[0].BeginEdit();
                            PercentManager[0]["InActive"] = 1;
                            PercentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            PercentManager[0].EndEdit();

                            int cn = PercentManager.Save();
                            if (cn == 1)
                            {
                                CustomAspxDevGridView1.DataBind();
                                SetLabelWarning("ذخیره انجام شد");

                            }
                            else
                            {
                                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                            }
                        }
                    }
                    else
                    {
                        SetLabelWarning("خطایی در بازخوانی اطلاعات انجام گرفته است");
                    }
                }

            }
        }
        catch (Exception err)
        {
            SetError(err);
        }


    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddDiscountPercent.aspx");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "DiscountPercent";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "DiscountPercent";
        GridViewExporter.WriteXlsToResponse(true);
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "Date":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = CustomAspxDevGridView1.Columns;
            Session["DataSource"] = ObjectDataSource1;
            Session["Title"] = "درصد تخفیف برای پروژه های خاص";
            CustomAspxDevGridView1.DetailRows.CollapseAllRows();
            CustomAspxDevGridView1.JSProperties["cpDoPrint"] = 1;
        }
    }
}
