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

public partial class Employee_Amoozesh_TecheasSalary : System.Web.UI.Page
{
    #region Private Members
    #endregion 

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.TeachersSalaryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;            
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewSalary.ClientVisible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;

        }
      
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int SalaryId = -1;
        if (GridViewSalary.FocusedRowIndex > -1)
        {
            DataRow SalaryRow = GridViewSalary.GetDataRow(GridViewSalary.FocusedRowIndex);
            SalaryId = (int)(SalaryRow["SalaryId"]);
            DeleteTeacherSalary(SalaryId);
            GridViewSalary.DataBind();
        }
    }

    protected void GridViewSalary_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertTeacherSalary(e);
        }
    }

    protected void GridViewSalary_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditTeacherSalary(e);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
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

    protected void GridViewSalary_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" )
            e.Cell.Style["direction"] = "ltr";


    }

    protected void GridViewSalary_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" )
            e.Editor.Style["direction"] = "ltr";
    }
  
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int SalaryId = -1;
        int focucedIndex = GridViewSalary.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewSalary.GetDataRow(focucedIndex);
            SalaryId = (int)row["SalaryId"];
        }
        if (SalaryId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                SalaryId = -1;
                Response.Redirect("AddTeacherSalary.aspx?SalaryId=" + Utility.EncryptQS(SalaryId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddTeacherSalary.aspx?SalaryId=" + Utility.EncryptQS(SalaryId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) );
            }
        }
    }

    private void InsertTeacherSalary(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {            
            TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();                      
            DataRow SalaryRow = TeachersSalaryManager.NewRow();
            DevExpress.Web.ASPxComboBox cmbLicences = (DevExpress.Web.ASPxComboBox)GridViewSalary.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSalary.Columns["Licence"], "cmbLicence");
            SalaryRow["LiId"] = int.Parse(cmbLicences.SelectedItem.Value.ToString());
            SalaryRow["SalaryPractical"] = e.NewValues["SalaryPractical"];
            SalaryRow["SalaryNonPractical"] = e.NewValues["SalaryNonPractical"];
            SalaryRow["SalaryNonPractical"] = e.NewValues["SalaryNonPractical"];
            SalaryRow["SalaryWorkroom"] = e.NewValues["SalaryWorkroom"];
            PersianDateControls.PersianDateTextBox PersianDateTextBox = (PersianDateControls.PersianDateTextBox)GridViewSalary.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSalary.Columns["StartDate"], "txtStartDate");
            
            SalaryRow["StartDate"] = PersianDateTextBox.Text;
            SalaryRow["UserId"] = Utility.GetCurrentUser_UserId();
            SalaryRow["ModifiedDate"] = DateTime.Now;
            TeachersSalaryManager.AddRow(SalaryRow);
            int cn = TeachersSalaryManager.Save();
            if (cn > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
            GridViewSalary.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewSalary.CancelEdit();

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

    private void EditTeacherSalary(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            if (LoginManager.Count < 0)
            {
                Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                return;
            }
            TeachersSalaryManager.Fill();
            DataRow SalaryRow = TeachersSalaryManager.DataTable.Rows.Find(e.Keys[0]);
            if (SalaryRow != null)
            {
                SalaryRow.BeginEdit();
                DevExpress.Web.ASPxComboBox cmbLicences = (DevExpress.Web.ASPxComboBox)GridViewSalary.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSalary.Columns["Licence"], "cmbLicence");
                SalaryRow["LiId"] = int.Parse(cmbLicences.SelectedItem.Value.ToString());
                SalaryRow["SalaryPractical"] = e.NewValues["SalaryPractical"];
                SalaryRow["SalaryNonPractical"] = e.NewValues["SalaryNonPractical"];
                SalaryRow["SalaryNonPractical"] = e.NewValues["SalaryNonPractical"];
                SalaryRow["SalaryWorkroom"] = e.NewValues["SalaryWorkroom"];
                PersianDateControls.PersianDateTextBox PersianDateTextBox = (PersianDateControls.PersianDateTextBox)GridViewSalary.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewSalary.Columns["StartDate"], "txtStartDate");

                SalaryRow["StartDate"] = PersianDateTextBox.Text;
                SalaryRow["UserId"] = (int)(LoginManager[0]["MeId"]);
                SalaryRow["ModifiedDate"] = DateTime.Now;
                SalaryRow.EndEdit();
               // TeachersSalaryManager.AddRow(SalaryRow);
                int cn = TeachersSalaryManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره با انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
            GridViewSalary.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewSalary.CancelEdit();

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

    private void DeleteTeacherSalary(int SalaryId)
    {
        TSP.DataManager.TeachersSalaryManager TeachersSalaryManager = new TSP.DataManager.TeachersSalaryManager();
        TeachersSalaryManager.FindByCode(SalaryId);
        if (TeachersSalaryManager.Count > 0)
        {
            TeachersSalaryManager[0].Delete();
            int cn = TeachersSalaryManager.Save();

            if (cn > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }
    #endregion
  
}
