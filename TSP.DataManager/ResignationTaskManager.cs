using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class ResignationTaskManager:BaseObject 
    {

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblResignationTask";
            tableMapping.ColumnMappings.Add("ResignTaskId", "ResignTaskId");
            tableMapping.ColumnMappings.Add("ResignId", "ResignId");
            tableMapping.ColumnMappings.Add("TaskId", "TaskId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectResignationTask";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignTaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssignerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "AssignerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 10, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteResignationTask";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ResignTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertResignationTask";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateResignationTask";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ResignTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignTaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.NezamFarsDataSet.tblResignationTaskDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int ResignTaskId)
        {
            ResetAllParameters();
           // this.Adapter.SelectCommand.Parameters.Clear();
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignTaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
           // this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@ResignTaskId"].Value = ResignTaskId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByResignId(int ResignId)
        {
            //this.Adapter.SelectCommand.Parameters.Clear();
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignTaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ResignId"].Value = ResignId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByTaskId(int AssignerId, int TaskId,string Date)
        {
            //this.Adapter.SelectCommand.Parameters.Clear();
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignTaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AssignerId"].Value = AssignerId;
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = TaskId;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = Date;

            Fill();
        }
    }
}
