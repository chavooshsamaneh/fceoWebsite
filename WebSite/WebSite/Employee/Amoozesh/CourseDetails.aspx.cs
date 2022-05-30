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

public partial class Employee_Amoozesh_CourseDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.CourseDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            CustomAspxDevGridView1.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["CrsId"]))
            {
                Response.Redirect("Course.aspx");
                return;
            }

            try
            {
                CourseId.Value = Server.HtmlDecode(Request.QueryString["CrsId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string CrsId= Utility.DecryptQS(CourseId.Value);
            ObjectDataSource1.FilterParameters[0].DefaultValue = CrsId;
            TSP.DataManager.CourseManager CourseManager = new TSP.DataManager.CourseManager();
            CourseManager.FindByCode(int.Parse(CrsId));
            if (CourseManager.Count == 1)
            {
                RoundPanelCourseDetail.HeaderText = CourseManager[0]["TypeName"].ToString()+"-" + CourseManager[0]["CrsName"].ToString();
            }
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourseDetails.aspx?CdId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New") + "&CrsId=" + CourseId.Value);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int CdId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CdId = (int)row["CdId"];
        }
        if (CdId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddCourseDetails.aspx?CdId=" + Utility.EncryptQS(CdId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&CrsId=" + CourseId.Value);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Course.aspx");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int CdId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CdId = (int)row["CdId"];
        }
        if (CdId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddCourseDetails.aspx?CdId=" + Utility.EncryptQS(CdId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&CrsId=" + CourseId.Value);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int CdId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CdId = (int)row["CdId"];
        }
        if (CdId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.CourseDetailManager managerEdit = new TSP.DataManager.CourseDetailManager();
            managerEdit.FindByCode(CdId);
            if (managerEdit.Count == 1)
            {
                try
                {
                    managerEdit[0].Delete();


                    int cn = managerEdit.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }
                }
                catch (Exception err)
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

            }
        }
    }
}

