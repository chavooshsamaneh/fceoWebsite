using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class ProvinceManager:BaseObject
    {
        //static ProvinceManager()
        //{
        //    _tableId = TableType.Province;
        //}
        public ProvinceManager()
            : base()
        {
        }
        public ProvinceManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Province);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblProvince";
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("NezamCode", "NezamCode");
            tableMapping.ColumnMappings.Add("PrName", "PrName");
            tableMapping.ColumnMappings.Add("CounId", "CounId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("EditDate", "EditDate");
            tableMapping.ColumnMappings.Add("CreatDate", "CreatDate");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectProvince";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteProvince";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertProvince";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PrName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EditDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EditDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreatDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreatDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateProvince";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PrName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EditDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EditDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreatDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreatDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblProvinceDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_PrId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_PrId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(int NezamCode, string PrName, int CounId, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(NezamCode));
            if ((PrName == null))
            {
                throw new System.ArgumentNullException("PrName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(PrName));
            }
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(CounId));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[5].Value = ((System.DateTime)(ModifiedDate));
            System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int NezamCode, string PrName, int CounId, int UserId, System.DateTime ModifiedDate, int Original_PrId, byte[] Original_LastTimeStamp, int PrId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(NezamCode));
            if ((PrName == null))
            {
                throw new System.ArgumentNullException("PrName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(PrName));
            }
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(CounId));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(Original_PrId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[8].Value = ((int)(PrId));
            System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        public void FindByCode(int PrId)
        {
            this.Adapter.SelectCommand.Parameters["@PrId"].Value = PrId;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByCounId(int CounId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectProvinceByCounId";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@CounId", SqlDbType.Int, 4, "CounId").Value = CounId;
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            return dt;
        }
    }
}
