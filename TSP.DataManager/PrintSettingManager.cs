using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager
{
    public class PrintSettingManager:BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintSetting);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPrintSetting";
            tableMapping.ColumnMappings.Add("PrtsId", "PrtsId");
            tableMapping.ColumnMappings.Add("PrtTypeId", "PrtTypeId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("InActiveDate", "InActiveDate");
            tableMapping.ColumnMappings.Add("IsUsed", "IsUsed");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            
            Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectPrintSetting";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeletePrintSetting";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrtsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
        
            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertPrintSetting";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActiveDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsUsed", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsUsed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            


            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdatePrintSetting";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActiveDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));                       
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsUsed", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsUsed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrtsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblPrintSettingDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int PrtsId)
        {
            this.Adapter.SelectCommand.Parameters["@PrtsId"].Value = PrtsId;
            Fill();
        }

        public DataTable SelectByPrintType(int PrtTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPrintSettingByPrintType", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PrtTypeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@PrtTypeId"].Value = PrtTypeId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectUsedPrintSettings(int PrtsId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUsedPrintSetting", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PrtsId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@PrtsId"].Value = PrtsId;
            adapter.Fill(dt);
            return (dt);
        }
    }
}
