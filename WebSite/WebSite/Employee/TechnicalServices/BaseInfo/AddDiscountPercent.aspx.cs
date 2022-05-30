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

public partial class Employee_TechnicalServices_BaseInfo_AddDiscountPercent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.DiscountPercentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
            this.ViewState["BtnSave"] = btnSave.Enabled;

        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Insert();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DiscountPercent.aspx");
    }

    private void Insert()
    {
        TSP.DataManager.TechnicalServices.DiscountPercentManager PercentManager = new TSP.DataManager.TechnicalServices.DiscountPercentManager();
        try
        {
            DataRow dr = PercentManager.NewRow();
            dr["Title"] = txtTitle.Text;
            dr["DecrementPercent"] = txtDecrementPercent.Text;
            dr["WagePercent"] = txtWagePercent.Text;
            dr["Date"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            dr["DiscountPercentCode"] = CmbType.Value;
            
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            PercentManager.AddRow(dr);
            if (PercentManager.Save() > 0)
            {
                SetLabelWarning("ذخیره انجام شد");
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
}
