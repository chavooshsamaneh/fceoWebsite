using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DBAdmin_DBAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_IsTspAdmin() == false)
            Response.Redirect("~/Login.aspx");
    }

    //protected void rdbQuery_CheckedChanged(object sender, EventArgs e)
    //{
    //    btnCommand.Enabled = false;
    //    txtCommand.Enabled = false;
    //    btnQuery.Enabled = true;
    //    txtQuery.Enabled = true;
    //}

    //protected void rdbCommand_CheckedChanged(object sender, EventArgs e)
    //{
    //    btnCommand.Enabled = true;
    //    txtCommand.Enabled = true;
    //    btnQuery.Enabled = false;
    //    txtQuery.Enabled = false;
    //}

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        //if (rdbQuery.Checked == true)
        ExecuteQuery();
        //else
        // ExecuteCommand();
    }
    void ExecuteQuery()
    {
        SqlConnection ObjConnection = null;

        try
        {
            lblError.Visible = false;
            lblResult.Visible = true;
            GridView1.Visible = false;
            lblCommandSuccessfull.Visible = false;

            //ObjConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["NezamFarsConnectionString1"].ConnectionString);
            ObjConnection = new SqlConnection(TSP.DataManager.DBManager.CnnStr);

            SqlCommand ObjCommand = new SqlCommand(txtQuery.Text, ObjConnection);
            SqlDataAdapter ObjDataAdapter = new SqlDataAdapter(ObjCommand);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = new DataSet();

            if (ObjConnection.State == ConnectionState.Closed)
                ObjConnection.Open();
            ObjDataAdapter.Fill(objDataSet);
            if (objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = objDataSet.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;

                if (objDataSet.Tables.Count > 1)
                {
                    GridView2.DataSource = objDataSet.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                }

                if (objDataSet.Tables.Count > 2)
                {
                    GridView3.DataSource = objDataSet.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                }

                lblCommandSuccessfull.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView3.Visible = false;
            }
            lblCommandSuccessfull.Visible = true;
        }
        catch (Exception err)
        {
            lblError.Visible = true;
            lblError.Text = "<div align=center>Error!<br>" + err.Message + "</div>";
        }
        finally
        {
            if (ObjConnection != null && ObjConnection.State == ConnectionState.Open)
                ObjConnection.Close();
        }
    }

    //void ExecuteCommand()
    //{
    //    SqlConnection ObjConnection = null;

    //    try
    //    {
    //        lblError.Visible = false;
    //        lblResult.Visible = true;
    //        GridView1.Visible = false;
    //        lblCommandSuccessfull.Visible = false;

    //        ObjConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["NezamFarsConnectionString1"].ConnectionString);

    //        SqlCommand ObjCommand = new SqlCommand(txtQuery.Text, ObjConnection);

    //        if (ObjConnection.State == ConnectionState.Closed)
    //            ObjConnection.Open();
    //        ObjCommand.ExecuteNonQuery();
    //        lblCommandSuccessfull.Visible = true;
    //    }
    //    catch (Exception err)
    //    {
    //        lblError.Visible = true;
    //        lblError.Text = "<div align=center>Error!<br>" + err.Message + "</div>";
    //    }
    //    finally
    //    {
    //        if (ObjConnection != null && ObjConnection.State == ConnectionState.Open)
    //            ObjConnection.Close();
    //    }
    //}
}
