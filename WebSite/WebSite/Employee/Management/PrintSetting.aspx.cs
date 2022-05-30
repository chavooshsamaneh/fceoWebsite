using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_HomePage_PrintSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission Per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnnew.Enabled = Per.CanDelete;
            btnnew2.Enabled = Per.CanDelete;
            btnedit.Enabled = Per.CanEdit;
            btnedit2.Enabled = Per.CanEdit;
            btndel.Enabled = Per.CanNew;
            btndel2.Enabled = Per.CanNew;
            btnview.Enabled = Per.CanView;
            btnview2.Enabled = Per.CanView;
            btnExportExcel.Enabled = Per.CanView;
            btnExportExcel2.Enabled = Per.CanView;
            GridViewPrintSetting.Visible = Per.CanView;

            this.ViewState["btnnew"] = btnnew.Enabled;
            this.ViewState["btnedit"] = btnedit.Enabled;
            this.ViewState["btndel"] = btndel.Enabled;
            this.ViewState["btnview"] = btnview.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btndel"] != null)
            this.btndel.Enabled = this.btndel2.Enabled = (bool)this.ViewState["btndel"];
        if (this.ViewState["btnview"] != null)
            this.btnview.Enabled = this.btnview2.Enabled = (bool)this.ViewState["btnview"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPrintSetting.aspx?id=" + Utility.EncryptQS("-1") + "&mode=" + Utility.EncryptQS("insert"));
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanEdit) return;

        DataRow row = GridViewPrintSetting.GetDataRow(GridViewPrintSetting.FocusedRowIndex);
        int id = (int)row["PrtsId"];
        //------------check if this printsetting used or inactive-------
        TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
        PrintSettingManager.FindByCode(id);
        if (Convert.ToBoolean(PrintSettingManager[0]["InActive"]))
        {
            ShowMessage("رکورد موردنظر غیرفعال می باشد");
            return;
        }
        if (Convert.ToBoolean(PrintSettingManager[0]["IsUsed"]))
        {
            ShowMessage("این تنظیمات استفاده شده و قابل ویرایش نمی باشد");
            return;
        }
        //---------------------------------------------------------
        Response.Redirect("AddPrintSetting.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanView) return;
        DataRow row = GridViewPrintSetting.GetDataRow(GridViewPrintSetting.FocusedRowIndex);
        int id = (int)row["PrtsId"];
        Response.Redirect("AddPrintSetting.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("view"));
    }

    protected void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow row = GridViewPrintSetting.GetDataRow(GridViewPrintSetting.FocusedRowIndex);
            int id = (int)row["PrtsId"];

            //------------check if this printsetting used or inactive-------
            TSP.DataManager.PrintSettingManager PrintSettingManager2 = new TSP.DataManager.PrintSettingManager();
            PrintSettingManager2.FindByCode(id);
            if (Convert.ToBoolean(PrintSettingManager2[0]["InActive"]))
            {
                ShowMessage("رکورد موردنظر غیرفعال می باشد");
                return;
            }
            if (Convert.ToBoolean(PrintSettingManager2[0]["IsUsed"]))
            {
                ShowMessage("این تنظیمات استفاده شده و قابل ویرایش نمی باشد");
                return;
            }
            //---------------------------------------------------------

            TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
            TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new TSP.DataManager.PrintAssignerSettingManager();
            PrintAssignerSettingManager.FindByPrintSettingCode(id);
            int CntAssigner= PrintAssignerSettingManager.Count;
            for (int i = 0; i < CntAssigner; i++)
            {
                PrintAssignerSettingManager[0].Delete();                
            }
            
            PrintAssignerSettingManager.Save();
            PrintAssignerSettingManager.DataTable.AcceptChanges();

            PrintSettingManager.FindByCode(id);

            if (PrintSettingManager.Count == 1)
            {
                PrintSettingManager[0].Delete();
                int result = PrintSettingManager.Save();
                if (result == 1)
                {
                    GridViewPrintSetting.DataBind();
                    ShowMessage("حذف انجام شد");
                }
                else
                {
                    ShowMessage("خطایی در حذف انجام گرفته است");
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "PrintAssignerSetting";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewPrintSetting_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "print")
        {
            Session["DataTable"] = GridViewPrintSetting.Columns;
            Session["DataSource"] = ObjectDataSourcePrintSetting;
            Session["Title"] = "امضاکنندگان گواهینامه ها";
            GridViewPrintSetting.DetailRows.CollapseAllRows();
            GridViewPrintSetting.JSProperties["cpDoPrint"] = 1;
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
        DataRow Row = GridViewPrintSetting.GetDataRow(GridViewPrintSetting.FocusedRowIndex);
        try
        {
            PrintSettingManager.FindByCode((int)Row["PrtsId"]);
            if (PrintSettingManager.Count == 1)
            {
                if (Convert.ToBoolean(PrintSettingManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                else
                {
                    PrintSettingManager[0].BeginEdit();
                    PrintSettingManager[0]["InActive"] = 1;
                    PrintSettingManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                    PrintSettingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    PrintSettingManager[0]["ModifiedDate"] = DateTime.Now;
                    PrintSettingManager[0].EndEdit();
                    if (PrintSettingManager.Save() == 1)
                    {
                        GridViewPrintSetting.DataBind();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    }
                    else
                    {
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void GridViewPrintSetting_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Name == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewPrintSetting_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowMessage("شماره پرونده تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }
}