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

public partial class Employee_Amoozesh_PeriodTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("Periods.aspx");
                return;
            }

            string PPID = "";
            try
            {
                PPId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
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

           ObjectDataSource1.FilterParameters[0].DefaultValue = PPID;

           TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
           BtnNew2.Enabled = per.CanNew;
           btnEdit.Enabled = per.CanEdit;
           btnEdit2.Enabled = per.CanEdit;
           btnView.Enabled = per.CanView;
           btnView2.Enabled = per.CanView;
           btnDelete.Enabled = per.CanDelete;
           btnDelete2.Enabled = per.CanDelete;
           CustomAspxDevGridView1.Visible = per.CanView;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPeriodTime.aspx?PTTId=" + Utility.EncryptQS("") + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("New"));

        //Response.Redirect("AddObserver.aspx?PageMode=" +Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PTTId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PTTId = (int)row["PTTId"];
        }
        if (PTTId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            //Session["CancelMode"] = cancelMode;
            Response.Redirect("AddPeriodTime.aspx?PTTId=" + Utility.EncryptQS(PTTId.ToString()) + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int PTTId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PTTId = (int)row["PTTId"];
        }
        if (PTTId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.PeriodTimeTableManager managerEdit = new TSP.DataManager.PeriodTimeTableManager();
            managerEdit.FindByCode(PTTId);
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
        Response.Redirect("Period.aspx");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int PTTId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PTTId = (int)row["PTTId"];
        }
        if (PTTId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {           
            Response.Redirect("AddPeriodTime.aspx?PTTId=" + Utility.EncryptQS(PTTId.ToString()) + "&PPId=" + (PPId.Value) + "&PageMode=" + Utility.EncryptQS("View"));
        }

    }
  
}
