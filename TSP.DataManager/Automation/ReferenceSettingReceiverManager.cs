using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager.Automation
{
    public class ReferenceSettingReceiverManager : BaseObject
    {
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "ReferenceSettingReceiver";
            tableMapping.ColumnMappings.Add("RefSetReId", "RefSetReId");
            tableMapping.ColumnMappings.Add("RefSetId", "RefSetId");
            tableMapping.ColumnMappings.Add("ReceiverType", "ReceiverType");
            tableMapping.ColumnMappings.Add("ReceiverId", "ReceiverId");
            tableMapping.ColumnMappings.Add("RefType", "RefType");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAutomationReferenceSettingReceiver";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RefSetReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RefSetId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RefType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ReceiverType", System.Data.SqlDbType.Int);



            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAutomationReferenceSettingReceiver";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RefSetReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAutomationReferenceSettingReceiver";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAutomationReferenceSettingReceiver";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RefSetReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "RefSetReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Automation.AutomationDataSet.ReferenceSettingReceiverDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }

        public DataTable FindByCode(int RefSetReId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetReId"].Value = RefSetReId;
            this.Fill();
            return this.DataTable;
        }


        public DataTable FindByRefTypeReceiverType(int RefType, int ReceiverType, int RefSetId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefType"].Value = RefType;
            this.Adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;
            this.Adapter.SelectCommand.Parameters["@RefSetId"].Value = RefSetId;
            this.Fill();
            return this.DataTable;
        }

        public DataTable FindByRefSetId(int RefSetId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetId"].Value = RefSetId;
            this.Fill();
            return this.DataTable;
        }

    }
}
