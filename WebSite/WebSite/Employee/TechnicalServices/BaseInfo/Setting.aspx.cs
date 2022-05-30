using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_TechnicalServices_Project_Setting : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.SettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            BtnEdit.Enabled = per.CanEdit;
            BtnEdit2.Enabled = per.CanEdit;
            BtnView.Enabled = per.CanView;
            BtnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;

            btnExportExcel.Enabled = per.CanView;
            btnExportExcel2.Enabled = per.CanView;
            GridViewSetting.ClientVisible = per.CanView;

            this.ViewState["BtnView"] = BtnView.Enabled;
            this.ViewState["BtnEdit"] = BtnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["btnInActive"] = btnInActive.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["BtnView"] != null)
            this.BtnView.Enabled = this.BtnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.BtnEdit.Enabled = this.BtnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["btnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int SettingId = -1;
            if (GridViewSetting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSetting.GetDataRow(GridViewSetting.FocusedRowIndex);
                SettingId = (int)row["SettingId"];

            }
            if (SettingId == -1)
            {
                SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                return;
            }

            TSP.DataManager.TechnicalServices.SettingManager SettingManager = new TSP.DataManager.TechnicalServices.SettingManager();

            SettingManager.FindByCode(SettingId);
            if (SettingManager.Count == 1)
            {
                if (Convert.ToBoolean(SettingManager[0]["InActive"]))
                {
                    SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                else
                {
                    SettingManager[0].BeginEdit();
                    SettingManager[0]["InActive"] = 1;
                    SettingManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                    SettingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    SettingManager[0]["ModifiedDate"] = DateTime.Now;
                    SettingManager[0].EndEdit();

                    int cn = SettingManager.Save();
                    if (cn == 1)
                    {
                        GridViewSetting.DataBind();
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
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "Setting";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "Setting";
        GridViewExporter.WriteXlsToResponse(true);
    }

    private void NextPage(string Mode)
    {
        int SettingId = -1; bool InActive = false;
        if (GridViewSetting.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSetting.GetDataRow(GridViewSetting.FocusedRowIndex);
            SettingId = (int)row["SettingId"];
            InActive = (bool)row["InActive"];

        }
        if (SettingId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }

        if ((Mode == "Edit") && InActive)
        {
            SetLabelWarning("امکان ویرایش رکورد غیرفعال وجود ندارد");
            return;
        }

        Response.Redirect("AddSetting.aspx?SettingId=" + Utility.EncryptQS(SettingId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
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

    protected void GridViewSetting_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "InActiveDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewSetting_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "InActiveDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewSetting_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = GridViewSetting.Columns;
            Session["DataSource"] = ObjectDataSource1;
            Session["Title"] = "تنظیمات خدمات مهندسی";
            GridViewSetting.DetailRows.CollapseAllRows();
            GridViewSetting.JSProperties["cpDoPrint"] = 1;
        }
    }
}