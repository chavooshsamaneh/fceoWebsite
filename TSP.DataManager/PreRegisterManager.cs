using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class PreRegisterManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PreRegister);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPreRegister";
            tableMapping.ColumnMappings.Add("PRegisterId", "PRegisterId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("CrsId", "CrsId");
            tableMapping.ColumnMappings.Add("TeId", "TeId");
            tableMapping.ColumnMappings.Add("InsId", "InsId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("RegisteringDate", "RegisteringDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectPreRegister";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRegisterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PRegisterId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CrsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add("@RegisteringDateFrom", SqlDbType.Char);
            this.Adapter.SelectCommand.Parameters.Add("@RegisteringDateTo", SqlDbType.Char);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeletePreRegister";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PRegisterId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PRegisterId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertPreRegister";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisteringDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisteringDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdatePreRegister";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisteringDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisteringDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PRegisterId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PRegisterId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRegisterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PRegisterId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblPreRegisterDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForManagmentPage(int CrsId, string RegisteringDateFrom, string RegisteringDateTo)
        {
            if (CrsId == -1 && RegisteringDateFrom == "1" && RegisteringDateTo == "2")
                return new System.Data.DataTable();
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CrsId"].Value = CrsId;
            this.Adapter.SelectCommand.Parameters["@RegisteringDateFrom"].Value = RegisteringDateFrom;
            this.Adapter.SelectCommand.Parameters["@RegisteringDateTo"].Value = RegisteringDateTo;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int PRegisterId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@CrsId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@PRegisterId"].Value = PRegisterId;
            Fill();
        }

        public void FindByMeId(int MeId, int CrsId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@CrsId"].Value = CrsId;
            this.Adapter.SelectCommand.Parameters["@PRegisterId"].Value = -1;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMeId(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPreRegisterByMeId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
