﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class TeacherResearchActivityManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TeacherResearchActivity);
        }

        protected override void InitAdapter()
        {                      
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTeacherResearchActivity";
            tableMapping.ColumnMappings.Add("TResearchId", "TResearchId");
            tableMapping.ColumnMappings.Add("TcId", "TcId");
            tableMapping.ColumnMappings.Add("RaId", "RaId");
            tableMapping.ColumnMappings.Add("TeId", "TeId");
            tableMapping.ColumnMappings.Add("ResearchName", "ResearchName");
            tableMapping.ColumnMappings.Add("ResearchDate", "ResearchDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTeacherResearchActivity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TResearchId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TResearchId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTeacherResearchActivity";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TResearchId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TResearchId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTeacherResearchActivity";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RaId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RaId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResearchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ResearchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResearchDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ResearchDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTeacherResearchActivity";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RaId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RaId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResearchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ResearchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResearchDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ResearchDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TResearchId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TResearchId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TResearchId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TResearchId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblTeacherResearchActivityDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int TResearchId)
        {
            this.Adapter.SelectCommand.Parameters["@TResearchId"].Value = TResearchId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByTeacher(int TecherId, int TcId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherResearchActivityByTeacher", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int).Value = TecherId;
            adapter.SelectCommand.Parameters.Add("@TcId", SqlDbType.Int).Value = TcId;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
