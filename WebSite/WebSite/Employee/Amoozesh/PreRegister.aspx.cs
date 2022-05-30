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

public partial class Employee_Amoozesh_PreRegister : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Cache["CachePersonMember"] == null)
                Cache["CachePersonMember"] = new object();

            TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewPreRegister.Visible = per.CanView;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));                            
        }

        Session["DataTable"] = GridViewPreRegister.Columns;
        Session["DataSource"] = ObjdsPreRegister;
        Session["Title"] = "پیش ثبت نام دروس";

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtDateFrom = document.getElementById('" + txtDateFrom.ClientID + "').value;";
        script += "var txtDateTo = document.getElementById('" + txtDateTo.ClientID + "').value;";

        script += "if (txtDateFrom==''&& txtDateTo=='' && cmbCourse.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                   cmbCourse.SetSelectedIndex(0);
                    document.getElementById('" + txtDateFrom.ClientID + "').value = ''; document.getElementById('" + txtDateTo.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int FocusedRowIndex = GridViewPreRegister.FocusedRowIndex;
        if (GridViewPreRegister.FocusedRowIndex > -1)
        {
            //ObjdsPreRegister.SelectParameters[0].DefaultValue = txtMeNo.Text;
            GridViewPreRegister.DataBind();

            DataRow PreRegisterRow = GridViewPreRegister.GetDataRow(FocusedRowIndex);
            DeletePreRegister(int.Parse(PreRegisterRow["PRegisterId"].ToString()));
            GridViewPreRegister.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void GridViewPreRegister_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

        //string MeId = e.Parameters;
        //ObjdsPreRegister.SelectParameters[0].DefaultValue = MeId;
        //GridViewPreRegister.DataBind();
        switch (e.Parameters)
        {
            case "Print":
                Session["DataTable"] = GridViewPreRegister.Columns;
                Session["DataSource"] = ObjdsPreRegister;
                Session["Title"] = "پیش ثبت نام دروس";
                GridViewPreRegister.JSProperties["cpPrint"] = 1;
                break;
        }
    }

    protected void GridViewPreRegister_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "RegisteringDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewPreRegister_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "RegisteringDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporterPreReg.FileName = "PreRegister";

        GridViewExporterPreReg.WriteXlsToResponse(true);
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int PRegisterId = -1;
        int FocucedIndex = GridViewPreRegister.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            //string TeacherId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());           
            // ObjdsPreRegister.SelectParameters[0].DefaultValue = txtMeNo.Text;
            GridViewPreRegister.DataBind();
            DataRow row = GridViewPreRegister.GetDataRow(FocucedIndex);
            PRegisterId = (int)(row["PRegisterId"]);

        }
        if (PRegisterId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            // string MeId = txtMeNo.Text;
            if (Mode == "New")
            {
                PRegisterId = -1;
                Response.Redirect("AddPreRegister.aspx?PRegisterId=" + Utility.EncryptQS(PRegisterId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));//+"&MeId="+Utility.EncryptQS(MeId));
            }
            else
            {
                Response.Redirect("AddPreRegister.aspx?PRegisterId=" + Utility.EncryptQS(PRegisterId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));// + "&MeId=" + Utility.EncryptQS(MeId));
            }
        }
    }

    private void SetError(Exception err)
    {
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

    private void SetDeleteError(Exception err)
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

    private void DeletePreRegister(int PRegisterId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = new TSP.DataManager.CourseHoursManager();
        TransactionManager.Add(PreRegisterManager);
        TransactionManager.Add(CourseHoursManager);
        try
        {
            TransactionManager.BeginSave();

            CourseHoursManager.FindByPRegisterId(PRegisterId);

            if (CourseHoursManager.Count > 0)
            {
                int CourseCount = CourseHoursManager.Count;
                for (int i = 0; i < CourseCount; i++)
                {
                    CourseHoursManager[0].Delete();
                }
                int cn = CourseHoursManager.Save();
                if (cn < 0)
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
            PreRegisterManager.FindByCode(PRegisterId);
            if (PreRegisterManager.Count > 0)
            {
                PreRegisterManager[0].Delete();

                int cnt = PreRegisterManager.Save();
                if (cnt > 0)
                {
                    TransactionManager.EndSave();
                    GridViewPreRegister.DataBind();
                    DivReport.Visible = true;
                    LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
        }
    }

    private void Search() {
        if (!string.IsNullOrEmpty(txtDateFrom.Text))
            ObjdsPreRegister.SelectParameters["RegisteringDateFrom"].DefaultValue = txtDateFrom.Text;
        else
            ObjdsPreRegister.SelectParameters["RegisteringDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtDateTo.Text))
            ObjdsPreRegister.SelectParameters["RegisteringDateTo"].DefaultValue = txtDateTo.Text;
        else
            ObjdsPreRegister.SelectParameters["RegisteringDateTo"].DefaultValue = "2";

        if (cmbCourse.SelectedItem != null && cmbCourse.SelectedItem.Value!=null)
            ObjdsPreRegister.SelectParameters["CrsId"].DefaultValue = cmbCourse.SelectedItem.Value.ToString();
        else
            ObjdsPreRegister.SelectParameters["CrsId"].DefaultValue = "-1";
        GridViewPreRegister.DataBind();
    }

    #endregion
}
