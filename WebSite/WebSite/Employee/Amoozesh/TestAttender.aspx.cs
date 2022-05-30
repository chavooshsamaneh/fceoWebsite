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

public partial class Employee_Amoozesh_Observer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["TestId"]))
            {
                Response.Redirect("Test.aspx");
                return;
            }
            string PPID = "";
            try
            {
                PPID = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"]).ToString());
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            if (string.IsNullOrEmpty(PPID))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string TestID = "";
            try
            {
                PPId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
                TestID = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TestId"]).ToString());
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            
            if (string.IsNullOrEmpty(TestID))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                try
                {
                    TestId.Value = Server.HtmlDecode(Request.QueryString["TestId"]).ToString();
                }
                catch
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                OdbTestAttender.FilterParameters[0].DefaultValue = TestID;

            }

            TSP.DataManager.Permission per = TSP.DataManager.TestAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnGrade.Enabled = per.CanView;
            btnGrade1.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTestAttenders.aspx?TAId=" + Utility.EncryptQS("")+"&TestId="+(TestId.Value)  + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("New"));

        //Response.Redirect("AddObserver.aspx?PageMode=" +Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int TAId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TAId = (int)row["TAId"];
        }
        if (TAId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddTestAttenders.aspx?TAId=" + Utility.EncryptQS(TAId.ToString()) + "&TestId=" + (TestId.Value) + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int TAId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TAId = (int)row["TAId"];
        }
        if (TAId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.TestAttenderManager managerEdit = new TSP.DataManager.TestAttenderManager();
            managerEdit.FindByCode(TAId);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Test.aspx?PPId="  + (PPId.Value));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        int TAId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TAId = (int)row["TAId"];
        }
        if (TAId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddTestAttenders.aspx?TAId=" + Utility.EncryptQS(TAId.ToString()) + "&TestId=" + (TestId.Value) + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }

    protected void btnGrade_Click(object sender, EventArgs e)
    {
        int TAId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TAId = (int)row["TAId"];
        }
        if (TAId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ثبت نمرات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddGrades.aspx?TAId=" + Utility.EncryptQS(TAId.ToString()) + "&TestId=" + (TestId.Value) + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
}
