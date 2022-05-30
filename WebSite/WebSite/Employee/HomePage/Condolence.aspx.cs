using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_HomePage_Condolence : System.Web.UI.Page
{
    /// <summary>
    /// ------------Hossein Pour-------------------------
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission Per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),(TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnnew.Enabled = Per.CanDelete;
            btnnew2.Enabled = Per.CanDelete;
            btnedit.Enabled = Per.CanEdit;
            btnedit2.Enabled = Per.CanEdit;
            btndel.Enabled = Per.CanNew;
            btndel2.Enabled = Per.CanNew;
            btnview.Enabled = Per.CanView;
            btnview2.Enabled = Per.CanView;
            GridViewCondolence.Visible = Per.CanView;

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
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCondolence.aspx?id=" + Utility.EncryptQS("-1") + "&mode=" + Utility.EncryptQS("insert"));
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanEdit) return;

        if (GridViewCondolence.FocusedRowIndex == -1)
        {
            ShowMessage("لطفا ابتدا یک ردیف را انتخاب نمایید");
            return;
        }

        DataRow row = GridViewCondolence.GetDataRow(GridViewCondolence.FocusedRowIndex);
        int id = (int)row["CdlId"];
        //-------------check inactive----
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        CondolenceManager.FindByCode(id);
        if (CondolenceManager.Count == 1)
        {
            if (Convert.ToBoolean(CondolenceManager[0]["InActive"]))
            {
                ShowMessage("رکورد مورد نظر غیر فعال بوده و قابل ویرایش نمی باشد");
                return;
            }
        }
        //-----------------------------------
        Response.Redirect("AddCondolence.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission Per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (!Per.CanView) return;

        if (GridViewCondolence.FocusedRowIndex == -1)
        {
            ShowMessage("لطفا ابتدا یک ردیف را انتخاب نمایید");
            return;
        }

        DataRow row = GridViewCondolence.GetDataRow(GridViewCondolence.FocusedRowIndex);
        int id = (int)row["CdlId"];
        Response.Redirect("AddCondolence.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&mode=" + Utility.EncryptQS("view"));
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        if (GridViewCondolence.FocusedRowIndex == -1)
        {
            ShowMessage("لطفا ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        try
        {
            DataRow row = GridViewCondolence.GetDataRow(GridViewCondolence.FocusedRowIndex);
            int id = (int)row["CdlId"];
            TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
            CondolenceManager.FindByCode(id);
            if (CondolenceManager.Count == 1)
            {
                //----------check inactive---------
                if (Convert.ToBoolean(CondolenceManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال بوده و قابل حذف نمی باشد");
                    return;
                }
                //---------------------------------
                 if (DeleteImage(id))
                    {
                CondolenceManager[0].Delete();
                int result = CondolenceManager.Save();
                if (result == 1)
                {
                    GridViewCondolence.DataBind();
                    ShowMessage("حذف انجام شد");
                }
                else
                {
                    ShowMessage("خطایی در حذف انجام گرفته است");
                }
                    }
                 else
                 {
                     this.DivReport.Visible = true;
                     this.LabelWarning.Text = "خطایی در حذف جزئیات انجام گرفته است";
                 }
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        DataRow Row = GridViewCondolence.GetDataRow(GridViewCondolence.FocusedRowIndex);
        try
        {
            CondolenceManager.FindByCode((int)Row["CdlId"]);
            if (CondolenceManager.Count == 1)
            {
                if (Convert.ToBoolean(CondolenceManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                else
                {
                    CondolenceManager[0].BeginEdit();
                    CondolenceManager[0]["InActive"] = 1;
                    CondolenceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    CondolenceManager[0]["ModifiedDate"] = DateTime.Now;
                    CondolenceManager[0].EndEdit();
                    if (CondolenceManager.Save() == 1)
                    {
                        GridViewCondolence.DataBind();
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
    public bool DeleteImage(int CdlId)
    {
        try
        {
            TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
            CondolenceManager.FindByCode(CdlId);
            if (CondolenceManager.Count == 1)
            {
                string filename = Server.MapPath(CondolenceManager[0]["CdlImage"].ToString());
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
            }
            else return false;
            return true;
        }
        catch
        {
            return false;
        }
    }
}