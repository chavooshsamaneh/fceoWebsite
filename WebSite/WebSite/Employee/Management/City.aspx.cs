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

public partial class City : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission Per = TSP.DataManager.CityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = Per.CanDelete;
            btnDelete2.Enabled = Per.CanDelete;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            BtnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnView.Enabled = Per.CanView;
            btnView2.Enabled = Per.CanView;
            GridViewCity.Visible = Per.CanView;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewCity.FocusedRowIndex > -1)
        {
            TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
            DataRow dr = GridViewCity.GetDataRow(GridViewCity.FocusedRowIndex);
            int CitId = (int)dr["CitId"];
            NextPage("Edit");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteCity();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Report";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion
    #region Methods
    private void DeleteCity()
    {
        int CitId = -1;
        if (GridViewCity.FocusedRowIndex > -1)
        {
            DataRow dr = GridViewCity.GetDataRow(GridViewCity.FocusedRowIndex);
            CitId = (int)dr["CitId"];
        }

        if (CitId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفا ابتدا یک رکورد را انتخاب کنید";
        }
        else
        {
            TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
            cityManager.FindByCode(CitId);

            if (cityManager.Count == 1)
            {
                try
                {
                    cityManager[0].Delete();
                    int cn = cityManager.Save();
                    if (cn == 1)
                    {
                        GridViewCity.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
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
            }
        }
    }

    private void NextPage(string Mode)
    {
        TSP.DataManager.CityManager cityManager = new TSP.DataManager.CityManager();
        String GridFilterString = GridViewCity.FilterExpression;
        int CitId = -1;


        if (GridViewCity.FocusedRowIndex > -1)
        {
            DataRow dr = GridViewCity.GetDataRow(GridViewCity.FocusedRowIndex);
            CitId = (int)dr["CitId"];
            switch (Mode)
            {
                case "Edit":
                    DataRow row = GridViewCity.GetDataRow(GridViewCity.FocusedRowIndex);
                    CitId = (int)row["CitId"];
                    break;
                case "New":
                    break;
                case "View":
                    DataRow row2 = GridViewCity.GetDataRow(GridViewCity.FocusedRowIndex);
                    CitId = (int)row2["CitId"];
                    break;
            }
        }

        if (CitId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        else
        {
            if (Mode == "New")
            {
                CitId = -1;
                Response.Redirect("CityInsert.aspx?CitId=" + Utility.EncryptQS(CitId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(Mode) +
                    "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
            else
            {
                Response.Redirect("CityInsert.aspx?CitId=" + Utility.EncryptQS(CitId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(Mode) +
                    "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
        }
    }
    #endregion
}
