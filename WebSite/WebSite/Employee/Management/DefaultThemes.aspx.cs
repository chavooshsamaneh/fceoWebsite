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

public partial class Employee_Management_DefaultThemes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission Per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),(TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnnew.Enabled = Per.CanDelete;
            btnnew2.Enabled = Per.CanDelete;
            btnedit.Enabled = Per.CanEdit;
            btnedit2.Enabled = Per.CanEdit;
            btndel.Enabled = Per.CanNew;
            btndel2.Enabled = Per.CanNew;
            btnview.Enabled = Per.CanView;
            btnview2.Enabled = Per.CanView;
            GridViewDefaultTheme.Visible = Per.CanView;
            
            this.ViewState["btnnew"] = btnnew.Enabled;
            this.ViewState["btnedit"] = btnedit.Enabled;
            this.ViewState["btndel"] = btndel.Enabled;
            this.ViewState["btnview"] = btnview.Enabled;
        }

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btndel"] != null)
            this.btndel.Enabled = this.btndel2.Enabled = (bool)this.ViewState["btndel"];
        if (this.ViewState["btnview"] != null)
            this.btnview.Enabled = this.btnview2.Enabled = (bool)this.ViewState["btnview"];
    }

    public bool DeleteFile(TSP.DataManager.DefaultThemesManager dtm)
    {
        string filename = Server.MapPath(dtm[0]["FileUrl"].ToString());
        if (System.IO.File.Exists(filename))
        {
            System.IO.File.Delete(filename);
            return true;
        }
        else return false;
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddDefaultThemes.aspx?id=" + Utility.EncryptQS("-1") + "&mode=" + Utility.EncryptQS("insert"));
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow row = GridViewDefaultTheme.GetDataRow(GridViewDefaultTheme.FocusedRowIndex);
            int id = (int)row["DtId"];

            TSP.DataManager.DefaultThemesManager DefaultThemesManager = new TSP.DataManager.DefaultThemesManager();
            DefaultThemesManager.FindByCode(id);
            DefaultThemesManager[0].BeginEdit();
            DefaultThemesManager[0]["InActive"] = true;
            DefaultThemesManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DefaultThemesManager[0]["ModifiedDate"] = DateTime.Now;
            DefaultThemesManager[0].EndEdit();
            if (DefaultThemesManager.Save() > 0)
            {
                GridViewDefaultTheme.DataBind();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DeleteComplete));
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
            }
        }

    }
    /*
    protected void btndel_Click(object sender, EventArgs e)
    {

        try
        {
            DataRow row = GridViewDefaultTheme.GetDataRow(GridViewDefaultTheme.FocusedRowIndex);
            int id = (int)row["DtId"];

            TSP.DataManager.DefaultThemesManager dtm = new TSP.DataManager.DefaultThemesManager();
            dtm.FindByCode(id);
            if (dtm.Count == 1)
            {
                if (DeleteFile(dtm))
                {
                    dtm[0].Delete();
                    int result = dtm.Save();
                    if (result == 1)
                    {
                        GridViewDefaultTheme.DataBind();
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
                    this.LabelWarning.Text = "خطایی در حذف فایل انجام گرفته است";
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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد تکراری می باشد";
                }
                else if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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
     */ 
    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanEdit) return;

        if (GridViewDefaultTheme.FocusedRowIndex == -1) return;

        DataRow row = GridViewDefaultTheme.GetDataRow(GridViewDefaultTheme.FocusedRowIndex);
        int id = (int)row["DtId"];
        Response.Redirect("AddDefaultThemes.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()); 
        if (!Per.CanView) return;

        if (GridViewDefaultTheme.FocusedRowIndex == -1) return;

        DataRow row = GridViewDefaultTheme.GetDataRow(GridViewDefaultTheme.FocusedRowIndex);
        int id = (int)row["DtId"];
        Response.Redirect("AddDefaultThemes.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("view"));
    }
    protected void GridViewDefaultTheme_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewDefaultTheme_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}
