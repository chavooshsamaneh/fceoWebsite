using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class WebsiteStatisticsManager
    {
        private int _TotalVisitors;
        public int TotalVisitors
        {
            get { return _TotalVisitors; }
        }

        public WebsiteStatisticsManager()
        {
            _TotalVisitors = 0;
        }

        public static Boolean UpdateStatistics()
        {
            SqlConnection objConnection = new SqlConnection(DBManager.CnnStr);
            SqlCommand objCommand = new SqlCommand("spUpdateWebsiteStatistics", objConnection);
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                    objConnection.Open();
                objCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception) { }
            finally
            {
                objConnection.Close();
            }
            return false;
        }

        public Boolean SelectStatistics()
        {
            SqlConnection objConnection = new SqlConnection(DBManager.CnnStr);
            SqlCommand objCommand = new SqlCommand("spSelectWebsiteStatistics", objConnection);
            SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
            System.Data.DataTable objDataTable = new System.Data.DataTable();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                    objConnection.Open();
                objDataAdapter.Fill(objDataTable);
                if (objDataTable.Rows.Count > 0)
                {
                    _TotalVisitors = Convert.ToInt32(objDataTable.Rows[0]["TotalVisitors"]);
                    return true;
                }
            }
            catch (Exception) { }
            finally
            {
                objConnection.Close();
            }
            return false;
        }
    }
}
