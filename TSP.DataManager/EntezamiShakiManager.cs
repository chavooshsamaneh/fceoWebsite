using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class EntezamiShakiManager : BaseObject
    {
        //public enum ShakiType
        //{
        //    member=0,
        //    Office=1,
        //}
        //static EntezamiShakiManager()
        //{
        //    _tableId = TableType.EntezamiShaki;
        //}
        // public EntezamiShakiManager()
        //    : base()
        //{
        //}
        //public EntezamiShakiManager(System.Data.DataSet ds)
        //    : base(ds)
        //{
        //}
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EntezamiShaki);
        }
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Entezami.Shaki";
            tableMapping.ColumnMappings.Add("ShId", "ShId");
            tableMapping.ColumnMappings.Add("ClnId", "ClnId");
            tableMapping.ColumnMappings.Add("EcrId", "EcrId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("UltId", "UltId");
            tableMapping.ColumnMappings.Add("Name", "Name");
            tableMapping.ColumnMappings.Add("Career", "Career");
            tableMapping.ColumnMappings.Add("Tel", "Tel");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectShaki";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ClnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
            Adapter.SelectCommand.Parameters.Add("@Tel", SqlDbType.NVarChar);
            Adapter.SelectCommand.Parameters.Add("@Address", SqlDbType.NVarChar);
            Adapter.SelectCommand.Parameters.Add("@Career", SqlDbType.NVarChar);
            Adapter.SelectCommand.Parameters.Add("@UltId", SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@ClnCode", SqlDbType.NVarChar);
            Adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar);
            Adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar);

            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeleteShaki";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ShId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertShaki";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UltId", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UltId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Name", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Name", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Career", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Career", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Tel", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Tel", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EcrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EcrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdateShaki";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UltId", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UltId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Name", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Name", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Career", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Career", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Tel", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Tel", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ShId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ShId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EcrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EcrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
      
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.EntezamiDataSet.EntezamiShakiDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByComplainCode(int ClnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            Fill();
        }

        public void FindByCode(int ShId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ShId"].Value = ShId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByCode(int ShId, int ClnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ShId"].Value = ShId;
            this.Adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Find(int UltId, string Name, string Tel, string Address, string Career, string ClnCode, string FromDate, string ToDate)
        {
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            this.Adapter.SelectCommand.Parameters["@Name"].Value = Name;
            this.Adapter.SelectCommand.Parameters["@Tel"].Value = Tel;
            this.Adapter.SelectCommand.Parameters["@Address"].Value = Address;
            this.Adapter.SelectCommand.Parameters["@Career"].Value = Career;
            this.Adapter.SelectCommand.Parameters["@ClnCode"].Value = ClnCode;
            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectShakiName(int ClnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectShakiOfComplain", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ClnId", System.Data.SqlDbType.Int);
            if (string.IsNullOrEmpty(ClnId.ToString()))
                ClnId = -1;
            adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectByFilterString(string SelectStr)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SelectStr, this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.Text;
            adapter.Fill(dt);
            return (dt);
        }
    }
}
