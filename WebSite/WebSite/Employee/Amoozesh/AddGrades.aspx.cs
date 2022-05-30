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
using System.IO;
public partial class Employee_Amoozesh_AddGrades : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        //btnEdit.Enabled = false;
        //btnEdit2.Enabled = false;
        //cmbMeId.DataBind();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TestId"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("TestAttender.aspx");
                return;
            }
            //chbInActive.Checked = false;
            try
            {
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                TestID.Value = Server.HtmlDecode(Request.QueryString["TestId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TestId = Utility.DecryptQS(TestID.Value);
            string PPID = Utility.DecryptQS(PeriodId.Value);


            if (string.IsNullOrEmpty(PPID))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            if (string.IsNullOrEmpty(TestId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            ODBTestAttender.FilterParameters[0].DefaultValue = TestId;

        }


    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if ((!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))) && (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["TestId"]).ToString())))
                Response.Redirect("TestAttender.aspx?TestId=" + Server.HtmlDecode(Request.QueryString["TestId"]).ToString() + "&PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }
  
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.TestAttenderManager manager = new TSP.DataManager.TestAttenderManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        tr.Add(manager);
        int cn;

        try
        {
            manager.FindByCode(int.Parse(e.Keys["TAId"].ToString()));
            if (manager.Count == 1)
            {
                manager[0].BeginEdit();
                manager[0]["Mark"] = e.NewValues["Mark"];
                manager[0]["Passed"] = e.NewValues["Passed"];
                manager[0]["Description"] = e.NewValues["Description"];
                manager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                manager[0].EndEdit();
            }
            cn = manager.Save();
            if (cn > 0)
            {

                e.Cancel = true;
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.DataBind();
                //manager.DataTable.AcceptChanges();
                TestID.Value = Utility.EncryptQS(manager[0]["TestId"].ToString());
                //PeriodId.Value = Utility.EncryptQS(manager[0]["PPId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام نشد.";
            }
        }

        catch (Exception err)
        {

            tr.CancelSave();
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

}
