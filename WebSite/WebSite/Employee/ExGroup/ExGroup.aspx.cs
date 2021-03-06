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

public partial class Employee_ExGroup_ExGroup : System.Web.UI.Page
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

        this.DivReport.Visible = true;
        this.DivReport.Style["Visibility"] = "hidden";
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            gridViewExGroup.JSProperties["cpError"] = 0;
            TSP.DataManager.Permission Per = TSP.DataManager.ExGroupManager.GetUserPermission(Utility.GetCurrentUser_UserId(),(TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDisActive2.Enabled = Per.CanDelete;
            btnDisActive.Enabled = Per.CanDelete;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnExportExcel.Enabled = Per.CanView;
            btnExportExcel2.Enabled = Per.CanView;
            gridViewExGroup.Visible = Per.CanView;

            this.ViewState["btnnew"] = btnNew.Enabled;
            this.ViewState["btnedit"] = btnEdit.Enabled;
            this.ViewState["btninactive"] = btnDisActive.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["btnnew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btninactive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["btninactive"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];

        string script = "<script language='javascript' type='text/javascript'> function ShowMessage(Message) {";
        script += "document.getElementById('" + DivReport.ClientID + @"').style.visibility = 'visible';";
        script += "document.getElementById('" + DivReport.ClientID + @"').style.display = 'inline';";
        script += "document.getElementById('" + LabelWarning.ClientID + @"').innerHTML = Message; }</script>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowMessage", script);

        Session["DataTable"] = gridViewExGroup.Columns;
        Session["DataSource"] = ObjectDataSourceExGroup;
        Session["Title"] = "گروه های تخصصی";
    } 
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ExGroups";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void gridViewExGroup_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;
        TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();

        int ExGroupId = Convert.ToInt32(e.Keys["ExGroupId"].ToString());
        exGroupManager.FindByCode(ExGroupId);
        if (exGroupManager.Count == 1)
        {
            try
            {
                if (!Utility.IsDBNullOrNullValue(exGroupManager[0]["ExGroupCode"]))
                {
                    if (Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.BoardDirectors)
                    {
                        ShowCallBackMessage("امکان ویرایش رکورد مورد نظر وجود ندارد");
                        return;
                    }
                }
                if (Convert.ToBoolean(exGroupManager[0]["InActive"]))
                {
                    ShowCallBackMessage("امکان ویرایش رکورد غیر فعال وجود ندارد");
                    return;
                }
                exGroupManager[0].BeginEdit();
                exGroupManager[0]["ExGroupName"] = e.NewValues["ExGroupName"];
                exGroupManager[0]["Description"] = e.NewValues["Description"];
                exGroupManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                exGroupManager[0]["ModifiedDate"] = DateTime.Now;
                exGroupManager[0].EndEdit();

                if (exGroupManager.Save() == 1)
                {
                    ShowCallBackMessage("ذخیره انجام شد");
                    gridViewExGroup.CancelEdit();
                }
            }
            catch (Exception err)
            {
                SetError(err);
                gridViewExGroup.CancelEdit();
            }
        }
    }
    protected void gridViewExGroup_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();
        TSP.DataManager.ExGroupManager exGroupManager2 = new TSP.DataManager.ExGroupManager();
        if (e.IsNewRow)
        {//--------insert--------
            //------check reapet---
            exGroupManager2.FindByName(e.NewValues["ExGroupName"].ToString(), 0);
            if (exGroupManager2.Count != 0)
            {
                e.RowError = "نام تکراری می باشد";
                return;
            }
        }
        else
        {//-----edit-----
            int ExGroupId = Convert.ToInt32(e.Keys["ExGroupId"].ToString());
            exGroupManager.FindByCode(ExGroupId);
            if (!Utility.IsDBNullOrNullValue(exGroupManager[0]["ExGroupCode"]))
            {
                if (Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.BoardDirectors)
                {
                    e.RowError = ("امکان ویرایش رکورد مورد نظر وجود ندارد");
                    return;
                }
            }
            if (e.NewValues["ExGroupName"].ToString() != exGroupManager[0]["ExGroupName"].ToString())
            {
                //------check reapet---
                exGroupManager2.FindByName(e.NewValues["ExGroupName"].ToString(), 0);
                if (exGroupManager2.Count != 0)
                {
                    e.RowError = "نام تکراری می باشد";
                    return;
                }
            }
            if (exGroupManager.Count == 1)
            {
                if (Convert.ToBoolean(exGroupManager[0]["InActive"]))
                {
                    e.RowError = "امکان ویرایش رکورد غیر فعال وجود ندارد";
                    return;
                }
            }
        }
        gridViewExGroup.DataBind();
    }
    protected void gridViewExGroup_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (IsPageRefresh)
            return;
        e.Cancel = true;
        if (Page.IsValid)
        {
            try
            {
                TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();
                DataRow dr = exGroupManager.NewRow();

                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                dr["ExGroupName"] = e.NewValues["ExGroupName"];
                dr["Description"] = e.NewValues["Description"];
                dr["InActive"] = 0;
                exGroupManager.AddRow(dr);
                if (exGroupManager.Save() > 0)
                {
                    ShowCallBackMessage("ذخیره انجام شد");
                    gridViewExGroup.CancelEdit();
                }
            }
            catch (Exception err)
            {
                SetError(err);
                gridViewExGroup.CancelEdit();
            }
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            DataRow row = gridViewExGroup.GetDataRow(gridViewExGroup.FocusedRowIndex);
            int id = (int)row["ExGroupId"];

            TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();
            exGroupManager.FindByCode(id);
            if (exGroupManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(exGroupManager[0]["ExGroupCode"]))
                {
                    if (Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.BoardDirectors
                        ||Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Memari
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Naghshebardaru
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Omran
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Shahrsazi
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Traffic
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Bargh
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Mechanic
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Welfare
                        )
                    {
                        ShowMessage("امکان حذف رکورد مورد نظر وجود ندارد");
                        return;
                    }
                }
                if (Convert.ToBoolean(exGroupManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                exGroupManager[0].Delete();
                int result = exGroupManager.Save();
                if (result == 1)
                {
                    gridViewExGroup.DataBind();
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
                    ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
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
    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int ExGroupId = -1;
        if (gridViewExGroup.FocusedRowIndex > -1)
        {
            DataRow dr = gridViewExGroup.GetDataRow(gridViewExGroup.FocusedRowIndex);
            ExGroupId = (int)dr["ExGroupId"];
        }

        if (ExGroupId == -1)
        {
            ShowMessage("لطفا ابتدا یک رکورد را انتخاب کنید");
        }
        else
        {
            TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();
            exGroupManager.FindByCode(ExGroupId);

            if (exGroupManager.Count == 1)
            {
                try
                {
                    if (!Utility.IsDBNullOrNullValue(exGroupManager[0]["ExGroupCode"]))
                    {
                        if (Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.BoardDirectors
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Memari
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Naghshebardaru
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Omran
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Shahrsazi
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Traffic
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Bargh
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Mechanic
                        || Convert.ToInt32(exGroupManager[0]["ExGroupCode"]) == (int)TSP.DataManager.ExGroupManager.Type.Welfare)
                        {
                            ShowMessage("امکان غیر فعال کردن برای رکورد مورد نظر وجود ندارد");
                            return;
                        }
                    }
                    if (Convert.ToBoolean(exGroupManager[0]["InActive"]))
                    {
                       ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                        return;
                    }
                    exGroupManager[0].BeginEdit();
                    exGroupManager[0]["InActive"] = 1;
                    exGroupManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    exGroupManager[0]["ModifiedDate"] = DateTime.Now;
                    exGroupManager[0].EndEdit();

                    if (exGroupManager.Save() == 1)
                    {
                        gridViewExGroup.DataBind();
                        ShowMessage("ذخیره انجام شد");
                    }
                }
                catch (Exception err)
                {
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
                            ShowMessage("خطایی در حذف انجام گرفته است");
                        }
                    }
                    else
                    {
                        ShowMessage("خطایی در حذف انجام گرفته است");
                    }
                }
            }
        }
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int ExGroupId = -1;
        if (gridViewExGroup.FocusedRowIndex > -1)
        {
            DataRow dr = gridViewExGroup.GetDataRow(gridViewExGroup.FocusedRowIndex);
            ExGroupId = (int)dr["ExGroupId"];
        }

        if (ExGroupId == -1)
        {
            ShowMessage("لطفا ابتدا یک رکورد را انتخاب کنید");
        }
        else
        {
            TSP.DataManager.ExGroupManager exGroupManager = new TSP.DataManager.ExGroupManager();
            TSP.DataManager.ExGroupManager exGroupManager2 = new TSP.DataManager.ExGroupManager();
            exGroupManager.FindByCode(ExGroupId);

            if (exGroupManager.Count == 1)
            {
                try
                {
                    if (!Convert.ToBoolean(exGroupManager[0]["InActive"]))
                    {
                        ShowMessage("رکورد مورد نظر فعال می باشد");
                        return;
                    }
                    //------check reapet------------
                    exGroupManager2.FindByName(exGroupManager[0]["ExGroupName"].ToString(), 0);
                    if (exGroupManager2.Count != 0)
                    {
                        ShowMessage("یک گروه تخصصی فعال با این نام وجود دارد");
                        return;
                    }
                    //------------------------------
                    exGroupManager[0].BeginEdit();
                    exGroupManager[0]["InActive"] = 0;
                    exGroupManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    exGroupManager[0]["ModifiedDate"] = DateTime.Now;
                    exGroupManager[0].EndEdit();
                    if (exGroupManager.Save() == 1)
                    {
                        gridViewExGroup.DataBind();
                        ShowMessage("ذخیره انجام شد");
                    }
                }
                catch (Exception err)
                {
                    SetError(err);
                }
            }
        }
    }
    protected void GridViewExGroup_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (e.Parameters == "print")
        {
            Session["DataTable"] = gridViewExGroup.Columns;
            Session["DataSource"] = ObjectDataSourceExGroup;
            Session["Title"] = "گروه های تخصصی";
            gridViewExGroup.DetailRows.CollapseAllRows();
            gridViewExGroup.JSProperties["cpDoPrint"] = 1;
        }
    }
    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ExGroups";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion

    #region Method
    void ShowCallBackMessage(string Msg)
    {
        gridViewExGroup.JSProperties["cpMsg"] = Msg;
        gridViewExGroup.JSProperties["cpError"] = 1;
    }
    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
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
                ShowCallBackMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowCallBackMessage("شماره تکراری می باشد");
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
    #endregion
}
