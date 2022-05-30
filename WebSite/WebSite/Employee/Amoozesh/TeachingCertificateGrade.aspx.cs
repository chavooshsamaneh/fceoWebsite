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

public partial class Employee_Amoozesh_TeachingCertificateGrade : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeacherId"]))
        //{
        //    Response.Redirect("Teachers.aspx");
        //    return;
        //}

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TeachingGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnView.Enabled = per.CanDelete;
            btnView2.Enabled = per.CanDelete;
            GridViewTeachingGrade.Visible = per.CanView;

            //  HiddenFieldTeachingGrade["MinGrade"] = Request.QueryString["MinGrade"].ToString();
            //  HiddenFieldTeachingGrade["PageMode"] = Request.QueryString["PageMode"];

            //  string TeId = Utility.DecryptQS(HiddenFieldTeachingGrade["TeacherId"].ToString());          
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewTeachingGrade.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeachingGrade.GetDataRow(GridViewTeachingGrade.FocusedRowIndex);
            if (!Convert.ToBoolean(row["InActive"]))
                NextPage("Edit");
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش رکور غیرفعال وجود ندارد.";
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (GridViewTeachingGrade.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeachingGrade.GetDataRow(GridViewTeachingGrade.FocusedRowIndex);
            int TGradeId = (int)row["TGradeId"];
            Active(TGradeId, true);
        }
        else
        {            
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "یک رکورد را انتخاب نمائید";         
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewTeachingGrade.FocusedRowIndex > -1)
        {
            DataRow row = GridViewTeachingGrade.GetDataRow(GridViewTeachingGrade.FocusedRowIndex);
            int TGradeId = (int)row["TGradeId"];
            Active(TGradeId, false);
            
        }
        else
        {            
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "یک رکورد را انتخاب نمائید";         
        }
    }

    protected void GridViewTeachingGrade_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "TGradeDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewTeachingGrade_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "TGradeDate")
            e.Editor.Style["direction"] = "ltr";
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int TGradeId = -1;
        int focucedIndex = GridViewTeachingGrade.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewTeachingGrade.GetDataRow(focucedIndex);
            TGradeId = (int)row["TGradeId"];
        }
        if (TGradeId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TGradeId = -1;
                Response.Redirect("AddTeachingCertificateGrade.aspx?TGdrId=" + Utility.EncryptQS(TGradeId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddTeachingCertificateGrade.aspx?TGdrId=" + Utility.EncryptQS(TGradeId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
        }
    }


    protected void Active(int TGradeId, Boolean Active)
    {

        TSP.DataManager.TeachingGradeManager TeachingGradeManager = new TSP.DataManager.TeachingGradeManager();
        TeachingGradeManager.FindByCode(TGradeId);
        if (TeachingGradeManager.Count == 1)
        {

            try
            {

                TeachingGradeManager[0].BeginEdit();
                if (Active)
                    TeachingGradeManager[0]["InActive"] = 0;
                else
                    TeachingGradeManager[0]["InActive"] = 1;
                TeachingGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeachingGradeManager[0]["ModifiedDate"] = DateTime.Now;
                TeachingGradeManager[0].EndEdit();
                if (TeachingGradeManager.Save() == 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                    GridViewTeachingGrade.DataBind();
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            catch (Exception err)
            {


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
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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
    #endregion


}
