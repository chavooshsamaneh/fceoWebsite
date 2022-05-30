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

public partial class Institue_Amoozesh_CourseRegister : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            //if (string.IsNullOrEmpty(Request.QueryString["InsId"]))
            //{
            //    Response.Redirect("InstitueHome.aspx");
            //    return;
            //}

            string InsId = Utility.GetCurrentUser_MeId().ToString();
            HiddenFieldCourseRegister["InsId"] = Utility.EncryptQS(InsId);
            InsId = Utility.DecryptQS(HiddenFieldCourseRegister["InsId"].ToString());
            ObjdsPeriodRegister.SelectParameters[3].DefaultValue = InsId;
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnView.Enabled = true;
            btnView2.Enabled = true;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }
      
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
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
        Delete();
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueHome.aspx");
    }

    protected void GridViewCourseRegister_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "RegisterDate")
            e.Cell.Style["direction"] = "ltr";


    }

    protected void GridViewCourseRegister_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "RegisterDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int PRId = -1;
        int FocucedIndex = GridViewCourseRegister.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            string InstId = Utility.DecryptQS(HiddenFieldCourseRegister["InsId"].ToString());

            DataRow row = GridViewCourseRegister.GetDataRow(FocucedIndex);
            PRId = (int)(row["PRId"]);
        }
        if (PRId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                PRId = -1;
                Response.Redirect("AddCourseRegister.aspx?PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldCourseRegister["InsId"]);
            }
            else
            {
                Response.Redirect("AddCourseRegister.aspx?PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldCourseRegister["InsId"]);
            }
        }
    }

    private void Delete()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TransactionManager.Add(PeriodRegisterManager);
        TransactionManager.Add(OtherPersonManager);
        try
        {
            int PRId = -1;

            if (GridViewCourseRegister.FocusedRowIndex > -1)
            {
                TransactionManager.BeginSave();

                DataRow row = GridViewCourseRegister.GetDataRow(GridViewCourseRegister.FocusedRowIndex);
                PRId = (int)(row["PRId"]);
                PeriodRegisterManager.FindByCode(PRId);
                if (PeriodRegisterManager.Count != 1)
                {
                    TransactionManager.CancelSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است.";
                    return;
                }
                if (Convert.ToInt32(PeriodRegisterManager[0]["IsMember"]) == 0)
                {
                    OtherPersonManager.FindByCode(Convert.ToInt32(PeriodRegisterManager[0]["MeId"]));
                    if (OtherPersonManager.Count != 1)
                    {
                        TransactionManager.CancelSave();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در بازیابی اطلاعات بوجود آمده است.";
                        return;
                    }
                    OtherPersonManager[0].Delete();
                    OtherPersonManager.Save();
                }
                PeriodRegisterManager[0].Delete();
                if (PeriodRegisterManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره با موفیت انجام شد.";
                    TransactionManager.EndSave();
                    GridViewCourseRegister.DataBind();
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکوردی انتخاب نشده است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
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
