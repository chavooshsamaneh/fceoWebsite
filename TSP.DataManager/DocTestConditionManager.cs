using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
  public  class DocTestConditionManager:BaseObject
    {
      public static Permission GetUserPermission(int UserId, UserType ut)
      {
          return BaseObject.GetUserPermission(UserId, ut, TableType.DocTestCondition);
      }

        protected override void InitAdapter()
      {
          global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
          tableMapping.SourceTable = "Table";
          tableMapping.DataSetTable = "DocTestCondition";
          tableMapping.ColumnMappings.Add("TCondId", "TCondId");
          tableMapping.ColumnMappings.Add("Year", "Year");
          tableMapping.ColumnMappings.Add("MjId", "MjId");
          tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
          tableMapping.ColumnMappings.Add("Inactive", "Inactive");
          tableMapping.ColumnMappings.Add("UserId", "UserId");
          tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
          tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
          tableMapping.ColumnMappings.Add("TestValidDate", "TestValidDate");
          tableMapping.ColumnMappings.Add("Description", "Description");
          tableMapping.ColumnMappings.Add("Title", "Title");
          tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
          this.Adapter.TableMappings.Add(tableMapping);

          this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
          this.Adapter.SelectCommand.Connection = this.Connection;
          this.Adapter.SelectCommand.CommandText = "spSelectDocTestCondition";
          this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
          this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TCondId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TCondId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Inactive", System.Data.SqlDbType.SmallInt, 4, System.Data.ParameterDirection.Input, 0, 0, "Inactive", System.Data.DataRowVersion.Current, false, null, "", "", ""));

          this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
          this.Adapter.DeleteCommand.Connection = this.Connection;
          this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocTestCondition";
          this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
          this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TCondId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "TCondId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
          this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
          this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
          this.Adapter.InsertCommand.Connection = this.Connection;
          this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocTestCondition";
          this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 63, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MjId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Inactive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "Inactive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestValidDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "TestValidDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
          this.Adapter.UpdateCommand.Connection = this.Connection;
          this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocTestCondition";
          this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 63, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MjId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Inactive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "Inactive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TCondId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "TCondId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TCondId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "TCondId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestValidDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "TestValidDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
      }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.DocumentDataSet.DocTestConditionDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
      public void FindByCode(int TCondId)
        {
            this.Adapter.SelectCommand.Parameters["@TCondId"].Value = TCondId;
            Fill();
        }

      [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
      public DataTable SelectByMajor(int Inactive, int MjId,Boolean IsSortedByExpDate)
      {
          DataTable dt = new DataTable();
          SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocTestCondition", this.Connection);
          adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
          adapter.SelectCommand.Parameters.Add("@TCondId", SqlDbType.Int, 4, "TCondId").Value = -1;
          adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
          adapter.SelectCommand.Parameters.Add("@Inactive", SqlDbType.Int, 4, "Inactive").Value = Inactive;
          adapter.SelectCommand.Parameters.AddWithValue("@IsSortedByExpDate", IsSortedByExpDate);
          adapter.Fill(dt);
          return (dt);
      }


      [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
      public DataTable SelectByMajor(int Inactive, int MjId)
      {
          return SelectByMajor(Inactive, MjId, false);
      }
      
    }
}
