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

public partial class Institue_Amoozesh_InstitueTeachers : System.Web.UI.Page
{
    #region events
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
            HiddenFieldInstitueTeacher["InsId"] = Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());//Request.QueryString["InsId"];           
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            btnDisActive.Enabled = true;
            btnDisActive2.Enabled = true;
            btnView.Enabled = true;
            btnView2.Enabled = true;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;            
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnDisActive"];
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

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        int InsTeacherId = -1;
        if (GridViewInsTeacher.FocusedRowIndex > -1)
        {
            DataRow InsTeacherRow = GridViewInsTeacher.GetDataRow(GridViewInsTeacher.FocusedRowIndex);
            InsTeacherId = int.Parse(InsTeacherRow["InsTeacherId"].ToString());
            bool InActive = Convert.ToBoolean(InsTeacherRow["InActive"]);
            if (InActive)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                return;
            }
            DisAcStive(InsTeacherId);
            GridViewInsTeacher.DataBind();
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int InsTeacherId = -1;
        if (GridViewInsTeacher.FocusedRowIndex > -1)
        {
            DataRow InsTeacherRow = GridViewInsTeacher.GetDataRow(GridViewInsTeacher.FocusedRowIndex);
            InsTeacherId = int.Parse(InsTeacherRow["InsTeacherId"].ToString());
            bool InActive = Convert.ToBoolean(InsTeacherRow["InActive"]);
            if (!InActive)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکورد مورد نظر فعال می باشد";
                return;
            }
            Active(InsTeacherId);
            GridViewInsTeacher.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Institue/Amoozesh/InstitueHome.aspx");
    }

    protected void GridViewInsTeacher_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" )
            e.Cell.Style["direction"] = "ltr";
   
    }

    protected void GridViewInsTeacher_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" )
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int InsTeacherId = -1;
        int FocucedIndex = GridViewInsTeacher.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            string InstId = Utility.DecryptQS(HiddenFieldInstitueTeacher["InsId"].ToString());

            DataRow row = GridViewInsTeacher.GetDataRow(FocucedIndex);
            InsTeacherId = (int)(row["InsTeacherId"]);
        }
        if (InsTeacherId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                InsTeacherId = -1;
                Response.Redirect("AddInstitueTeacher.aspx?InsTeId=" + Utility.EncryptQS(InsTeacherId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldInstitueTeacher["InsId"]);
            }
            else
            {
                Response.Redirect("AddInstitueTeacher.aspx?InsTeId=" + Utility.EncryptQS(InsTeacherId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldInstitueTeacher["InsId"]);
            }
        }
    }

    private void DisAcStive(int InsTeacherId)
    {
        try
        {
            TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
            InstitueTeachersManager.FindByCode(InsTeacherId);
            if (InstitueTeachersManager.Count > 0)
            {
                InstitueTeachersManager[0].BeginEdit();

                InstitueTeachersManager[0]["InActive"] = 1;
                InstitueTeachersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();

                InstitueTeachersManager[0].EndEdit();

                int cn = InstitueTeachersManager.Save();
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
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Active(int InsTeacherId)
    {
        try
        {
            TSP.DataManager.InstitueTeachersManager InstitueTeachersManager = new TSP.DataManager.InstitueTeachersManager();
            InstitueTeachersManager.FindByCode(InsTeacherId);
            if (InstitueTeachersManager.Count > 0)
            {
                InstitueTeachersManager[0].BeginEdit();

                InstitueTeachersManager[0]["InActive"] = 0;
                InstitueTeachersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                InstitueTeachersManager[0].EndEdit();

                int cn = InstitueTeachersManager.Save();
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
            }
        }
        catch (Exception err)
        {
            SetError(err);
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
