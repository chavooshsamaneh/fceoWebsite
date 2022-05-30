using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager
{
    public class ResetPasswordManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ResetPassword);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblResetPassword";
            tableMapping.ColumnMappings.Add("RpId", "RpId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("RequestIpAddress", "RequestIpAddress");
            tableMapping.ColumnMappings.Add("RequestDate", "RequestDate");
            tableMapping.ColumnMappings.Add("RequestTime", "RequestTime");
            tableMapping.ColumnMappings.Add("RequestDateTime", "RequestDateTime");
            tableMapping.ColumnMappings.Add("RequestDateTimeDetail", "RequestDateTimeDetail");
            tableMapping.ColumnMappings.Add("ChangeDate", "ChangeDate");
            tableMapping.ColumnMappings.Add("ChangeTime", "ChangeTime");
            tableMapping.ColumnMappings.Add("ChangeDateTime", "ChangeDateTime");
            tableMapping.ColumnMappings.Add("ChangeIpAddress", "ChangeIpAddress");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("RequestUserId", "RequestUserId");
            tableMapping.ColumnMappings.Add("IsChangePass", "IsChangePass");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectResetPassword";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.TinyInt);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RpId", SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteResetPassword";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "RpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertResetPassword";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, 3, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestUserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "RequestUserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestIpAddress", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "RequestIpAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDate", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "RequestDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestTime", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "RequestTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDateTime", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "RequestDateTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDateTimeDetail", System.Data.SqlDbType.BigInt, 8, System.Data.ParameterDirection.Input, 19, 0, "RequestDateTimeDetail", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsChangePass", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsChangePass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeDate", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "ChangeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeTime", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "ChangeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeDateTime", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ChangeDateTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeIpAddress", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "ChangeIpAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateResetPassword";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, 3, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestUserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "RequestUserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestIpAddress", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "RequestIpAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDate", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "RequestDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestTime", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "RequestTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDateTime", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "RequestDateTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDateTimeDetail", System.Data.SqlDbType.BigInt, 8, System.Data.ParameterDirection.Input, 19, 0, "RequestDateTimeDetail", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsChangePass", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsChangePass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeDate", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "ChangeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeTime", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, 0, 0, "ChangeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeDateTime", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ChangeDateTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChangeIpAddress", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "ChangeIpAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "RpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "RpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblResetPasswordDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public DataTable FindByCode(int RpId)
        {
            this.Adapter.SelectCommand.Parameters["@RpId"].Value = RpId;

            Fill();
            return (this.DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByType(byte Type, int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
            return (this.DataTable);
        }
    }
}
