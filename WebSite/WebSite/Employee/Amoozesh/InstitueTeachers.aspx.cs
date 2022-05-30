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

public partial class Employee_Amoozesh_InstitueTeachers : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("Institue.aspx");
                return;
            }
            TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;         
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
            HiddenFieldInsTeacher["InsId"]=Request.QueryString["InsId"].ToString();
            HiddenFieldInsTeacher["PrePageMode"] = Request.QueryString["PgMd"];

            string InsId = Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString());
            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));
            if (InstitueManager.Count == 1)
            {
                RoundPanelInsTeacher.HeaderText = "مؤسسه: " + InstitueManager[0]["InsName"].ToString();

            }
            ObjdsInsTeacher.SelectParameters[0].DefaultValue = InsId;
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Institue.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {  
         NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string InsId = Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString());
        int CertType = FindInstitueCertificate(int.Parse(InsId));
        if (CertType == 0)
        {
            NextPage("Edit");
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در وضعیت تمدید و تغییرات پرونده امکان ویرایش اطلاعات وجود ندارد.";
        }
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
            DisAcStive(InsTeacherId);
            GridViewInsTeacher.DataBind();
        }
    }
    protected void GridViewInsTeacher_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" )
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewInsTeacher_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" )
            e.Editor.Style["direction"] = "ltr";
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facilities":
                Response.Redirect("InstituteFacilities.aspx?InsId=" + HiddenFieldInsTeacher["InsId"].ToString() + "&PgMd=" + HiddenFieldInsTeacher["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "BasicInfo":
                Response.Redirect("AddInstitues.aspx?InsId=" + HiddenFieldInsTeacher["InsId"].ToString() + "&PageMode=" + HiddenFieldInsTeacher["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsId=" + HiddenFieldInsTeacher["InsId"].ToString() + "&PgMd=" + HiddenFieldInsTeacher["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsId=" + HiddenFieldInsTeacher["InsId"].ToString() + "&PgMd=" + HiddenFieldInsTeacher["PrePageMode"].ToString() + "&InsCId=" + Request.QueryString["InsCId"]);
                break;
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int InsTeacherId = -1;
        int FocucedIndex = GridViewInsTeacher.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            string InsId = Utility.DecryptQS(HiddenFieldInsTeacher["InsId"].ToString());

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
                Response.Redirect("AddInstitueTeachers.aspx?InsTeId=" + Utility.EncryptQS(InsTeacherId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldInsTeacher["InsId"] + "&InsCId=" + Request.QueryString["InsCId"]
                   + "&PrPgMd=" + HiddenFieldInsTeacher["PrePageMode"]);
            }
            else
            {
                Response.Redirect("AddInstitueTeachers.aspx?InsTeId=" + Utility.EncryptQS(InsTeacherId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&InsId=" + HiddenFieldInsTeacher["InsId"] + "&InsCId=" + Request.QueryString["InsCId"]
                    + "&PrPgMd=" + HiddenFieldInsTeacher["PrePageMode"]);
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
                InstitueTeachersManager[0]["ModifiedDate"] = DateTime.Now;

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

    private int FindInstitueCertificate(int InsId)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId,0);
        int InsCertType = -1;
        if (dtInsCert.Rows.Count > 0)
        {
            InsCertType = int.Parse(dtInsCert.Rows[0]["Type"].ToString());
        }
        return InsCertType;
    }
    #endregion

}
