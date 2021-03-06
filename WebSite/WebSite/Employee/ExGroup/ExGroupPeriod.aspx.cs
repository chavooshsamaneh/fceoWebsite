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

public partial class Employee_ExGroup_ExGroupPeriod : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {         
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission Per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
            gridViewExGroupPeriod.Visible = Per.CanView;

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

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS("-1") + "&mode=" + Utility.EncryptQS("insert"));
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanEdit) return;

        DataRow row = gridViewExGroupPeriod.GetDataRow(gridViewExGroupPeriod.FocusedRowIndex);
        int id = (int)row["ExGroupPeriodId"];
          TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
            egpManager.FindByCode(id);
            if (egpManager.Count == 1)
            {
                if (Convert.ToBoolean(egpManager[0]["InActive"]))
                {
                    ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                    return;
                }
            }
        Response.Redirect("ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanView) return;

        DataRow row = gridViewExGroupPeriod.GetDataRow(gridViewExGroupPeriod.FocusedRowIndex);
        int id = (int)row["ExGroupPeriodId"];
        Response.Redirect("ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("view"));
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            DataRow row = gridViewExGroupPeriod.GetDataRow(gridViewExGroupPeriod.FocusedRowIndex);
            int id = (int)row["ExGroupPeriodId"];

            TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
            TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
            egpManager.FindByCode(id);
            if (egpManager.Count == 1)
            {
                CandidateManager.FindByExGroupPeriodId(Convert.ToInt32(egpManager[0]["ExGroupPeriodId"]));
                if (CandidateManager.Count != 0)
                    if (Convert.ToBoolean(egpManager[0]["InActive"]))
                    {
                        ShowMessage("امکان حذف رکورد غیرفعال وجود ندارد");
                        return;
                    }
                egpManager[0].Delete();
                int result = egpManager.Save();
                if (result == 1)
                {
                    gridViewExGroupPeriod.DataBind();
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
                    ShowMessage("کد تکراری می باشد");
                }
                else if (se.Number == 547)
                {
                    ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
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
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
        DataRow Row = gridViewExGroupPeriod.GetDataRow(gridViewExGroupPeriod.FocusedRowIndex);
        try
        {
            egpManager.FindByCode((int)Row["ExGroupPeriodId"]);
            if (egpManager.Count == 1)
            {
                if (Convert.ToBoolean(egpManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                else
                {
                    egpManager[0].BeginEdit();
                    egpManager[0]["InActive"] = 1;
                    egpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    egpManager[0]["ModifiedDate"] = DateTime.Now;
                    egpManager[0].EndEdit();
                    if (egpManager.Save() == 1)
                    {
                        gridViewExGroupPeriod.DataBind();
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
                    ShowMessage("کد تکراری می باشد");
                }
                else if (se.Number == 547)
                {
                    ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
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
    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ExGroupPeriods";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void gridViewExGroupPeriod_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        switch (e.Parameters)
        {
            case "delete":
                btndel_Click(this, null);
                break;
            case "inactive":
                btnInActive_Click(this, null);
                break;
            case "print":
            Session["DataTable"] = gridViewExGroupPeriod.Columns;
            Session["DataSource"] = ObjectDataSourceExGroupPeriod;
            Session["Title"] = "لیست دوره های انتخاباتی";
            gridViewExGroupPeriod.DetailRows.CollapseAllRows();
            gridViewExGroupPeriod.JSProperties["cpDoPrint"] = 1;
                break;
        }

        if (e.Parameters == "print")
        {

        }
    }
    protected void gridViewExGroupPeriod_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Name == "Date")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void gridViewExGroupPeriod_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "Date")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void GridViewCandidate_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["ExGroupPeriodId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

}
