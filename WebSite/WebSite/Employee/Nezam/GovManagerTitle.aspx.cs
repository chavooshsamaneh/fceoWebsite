using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Nezam_GovManagerTitle : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
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

        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!Page.IsPostBack)
        {

            TSP.DataManager.Permission Per = TSP.DataManager.GovManagerTitleManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnnew.Enabled = Per.CanNew;
            btnnew2.Enabled = Per.CanNew;
            btnedit.Enabled = Per.CanEdit;
            btnedit2.Enabled = Per.CanEdit;
            btninactive.Enabled = Per.CanEdit;
            btninactive2.Enabled = Per.CanEdit;
            btnExportExcel.Enabled = Per.CanView;
            btnExportExcel2.Enabled = Per.CanView;
            GridViewGovManagerTitle.Visible = Per.CanView;
            btndel.Enabled =  btndel2.Enabled = Per.CanDelete;           

            this.ViewState["btnnew"] = btnnew.Enabled;
            this.ViewState["btnedit"] = btnedit.Enabled;
            this.ViewState["btninactive"] = btninactive.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btninactive"] != null)
            this.btninactive.Enabled = this.btninactive2.Enabled = (bool)this.ViewState["btninactive"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];

        string script = "<script language='javascript' type='text/javascript'> function ShowMessage(Message) {";
        script += "document.getElementById('" + DivReport.ClientID + @"').style.visibility = 'visible';";
        script += "document.getElementById('" + DivReport.ClientID + @"').style.display = 'inline';";
        script += "document.getElementById('" + LabelWarning.ClientID + @"').innerHTML = Message; }</script>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowMessage", script);

        Session["DataTable"] = GridViewGovManagerTitle.Columns;
        Session["DataSource"] = ObjectDataSource1;
        Session["Title"] = "سمت های استان/کشوری";
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "GovernmentManagerTitle";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridViewGovManagerTitle_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;
        if (Page.IsValid)
        {
            try
            {
                TSP.DataManager.GovManagerTitleManager gmt = new TSP.DataManager.GovManagerTitleManager();
                DataRow row = gmt.NewRow();
                row["GmtName"] = e.NewValues["GmtName"];
                row["CreateDate"] = Utility.GetDateOfToday();
                row["InActiveDate"] = "";
                row["Description"] = e.NewValues["Description"];
                row["InActive"] = 0;
                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                gmt.AddRow(row);
                if (gmt.Save() > 0)
                {
                    GridViewGovManagerTitle.DataBind();
                    ShowCallBackMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    GridViewGovManagerTitle.CancelEdit();
                }
            }
            catch (Exception err)
            {
                GridViewGovManagerTitle.CancelEdit();
                SetError(err);
            }
        }
    }
    protected void GridViewGovManagerTitle_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;
        TSP.DataManager.GovManagerTitleManager gmt = new TSP.DataManager.GovManagerTitleManager();
        gmt.FindByCode(int.Parse(e.Keys["GmtId"].ToString()));
        if (gmt.Count == 1)
        {
            try
            {
                if (Convert.ToBoolean(gmt[0]["InActive"]))
                {
                    ShowMessage("امکان ویرایش رکورد غیر فعال وجود ندارد");
                    return;
                }
                gmt[0].BeginEdit();
                gmt[0]["GmtName"] = e.NewValues["GmtName"];
                gmt[0]["Description"] = e.NewValues["Description"];
                gmt[0]["UserId"] = Utility.GetCurrentUser_UserId();
                gmt[0]["ModifiedDate"] = DateTime.Now;
                gmt[0].EndEdit();

                if (gmt.Save() > 0)
                {
                    GridViewGovManagerTitle.DataBind();
                    ShowCallBackMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
                else
                {
                    ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                }
                GridViewGovManagerTitle.CancelEdit();

            }
            catch (Exception err)
            {
                GridViewGovManagerTitle.CancelEdit();
                SetError(err);
            }
        }
        else
            ShowCallBackMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
    }
    protected void GridViewGovManagerTitle_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.GovManagerTitleManager gmt = new TSP.DataManager.GovManagerTitleManager();
        if (!e.IsNewRow)//-----edit
        {
            gmt.FindByCode(Convert.ToInt32(e.Keys["GmtId"]));
            if (gmt.Count == 1)
            {
                if (Convert.ToBoolean(gmt[0]["InActive"]))
                {
                    e.RowError = "امکان ویرایش رکورد غیر فعال وجود ندارد";
                    return;
                }
            }
            else
            {
                e.RowError = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return;
            }
        }
        else//--------insert
        {
            //------check reapet---
            DataTable dtcheck = gmt.FindByName(e.NewValues["GmtName"].ToString());
            if (dtcheck.Rows.Count != 0)
            {
                e.RowError = "نام تکراری می باشد";
                return;
            }
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.GovManagerTitleManager gmt = new TSP.DataManager.GovManagerTitleManager();
        DataRow Row = GridViewGovManagerTitle.GetDataRow(GridViewGovManagerTitle.FocusedRowIndex);
        try
        {
            gmt.FindByCode((int)Row["GmtId"]);
            if (gmt.Count == 1)
            {
                if (Convert.ToBoolean(gmt[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                else
                {
                    gmt[0].BeginEdit();
                    gmt[0]["InActive"] = 1;
                    gmt[0]["InActiveDate"] = Utility.GetDateOfToday();
                    gmt[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    gmt[0]["ModifiedDate"] = DateTime.Now;
                    gmt[0].EndEdit();
                    if (gmt.Save() == 1)
                    {
                        GridViewGovManagerTitle.DataBind();
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
    protected void btndel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            DataRow row = GridViewGovManagerTitle.GetDataRow(GridViewGovManagerTitle.FocusedRowIndex);
            int id = (int)row["GmtId"];

            TSP.DataManager.GovManagerTitleManager gmt = new TSP.DataManager.GovManagerTitleManager();
            gmt.FindByCode(id);
            if (gmt.Count == 1)
            {
                if (Convert.ToBoolean(gmt[0]["InActive"]))
                {
                    ShowMessage("امکان ویرایش رکورد غیر فعال وجود ندارد");
                    return;
                }
                gmt[0].Delete();
                int result = gmt.Save();
                if (result == 1)
                {
                    GridViewGovManagerTitle.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DeleteComplete));
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
    #endregion

    #region Functions
    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Message;
    }
    void ShowCallBackMessage(string Msg)
    {
        GridViewGovManagerTitle.JSProperties["cpMsg"] = Msg;
        GridViewGovManagerTitle.JSProperties["cpError"] = 1;
    }
    private void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowCallBackMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowCallBackMessage("شماره پرونده تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowCallBackMessage("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
        }
    } 
    protected void GridViewGovManagerTitle_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
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
                Session["DataTable"] = GridViewGovManagerTitle.Columns;
                Session["DataSource"] = ObjectDataSource1;
                Session["Title"] = "مدیریت سمت های استان/کشوری";
                GridViewGovManagerTitle.DetailRows.CollapseAllRows();
                GridViewGovManagerTitle.JSProperties["cpDoPrint"] = 1;
                break;
        }
    }
    #endregion
}