using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class InstitueManager : BaseObject
    {
        public InstitueManager()
            : base()
        {
        }
        public InstitueManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Institue);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblInstitue";
            tableMapping.ColumnMappings.Add("InsId", "InsId");
            tableMapping.ColumnMappings.Add("InsName", "InsName");
            tableMapping.ColumnMappings.Add("Manager", "Manager");
            tableMapping.ColumnMappings.Add("RegNo", "RegNo");
            tableMapping.ColumnMappings.Add("RegDate", "RegDate");
            tableMapping.ColumnMappings.Add("RegPlace", "RegPlace");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Tel1", "Tel1");
            tableMapping.ColumnMappings.Add("Tel2", "Tel2");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("WebSite", "WebSite");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectInstitue";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@InsId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CitId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateFrom", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateTo", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@InsName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@InsRegNo", System.Data.SqlDbType.NVarChar);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteInstitue";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertInstitue";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InsName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Manager", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Manager", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlace", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateInstitue";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InsName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Manager", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Manager", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlace", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        public void FindByCode(int InsId)
        {
            this.Adapter.SelectCommand.Parameters["@InsId"].Value = InsId;
            this.Adapter.SelectCommand.Parameters["@CitId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.InstitueConfirming;


            Fill();
        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblInstitueDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInstitueJoinWorkFlow()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectlInstitueJoinWorkFlow", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInstitueByCity(int CitId, int InActive)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectInstitue", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4, "InsId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = -1;
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int, 4, "CitId").Value = CitId;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)TSP.DataManager.WorkFlows.InstitueConfirming;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInstitueByCity(int CitId)
        {
            return SelectInstitueByCity(CitId, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInstitueByTaskCode(int TaskCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectInstitue", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4, "InsId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int, 4, "CitId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)TSP.DataManager.WorkFlows.InstitueConfirming;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchInstitue(int InsId, int TaskCode, int CitId, string InsName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectInstitueByInfo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4, "InsId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = -1;
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int, 4, "CitId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InsName", SqlDbType.NVarChar, 4, "InsName").Value = InsName;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)TSP.DataManager.WorkFlows.InstitueConfirming;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchInstitueByName(string InsName)
        {
            return (SearchInstitue(-1, -1, -1, InsName));
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInstitue(string EndDateFrom, string EndDateTo, string InsRegNo, string InsName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectInstitue", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@InsRegNo", InsRegNo);
            adapter.SelectCommand.Parameters.AddWithValue("@InsName", InsName);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.InstitueConfirming);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        public void DeleteRequest(int InsCId, int InsId)
        {
            SqlCommand cmd = new SqlCommand("spDeleteInstituRequest", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@InsCId", InsCId);
                cmd.Parameters.AddWithValue("@InsId", InsId);
                //cmd.Parameters.AddWithValue("@TableType", (int)TableCodes.Institue);
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }
    }
}
