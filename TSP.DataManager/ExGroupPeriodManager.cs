using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class ExGroupPeriodManager : BaseObject
    {

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExGroupPeriod);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblExGroupPeriod";
            tableMapping.ColumnMappings.Add("ExGroupPeriodId", "ExGroupPeriodId");
            tableMapping.ColumnMappings.Add("ExGroupId", "ExGroupId");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("StartDatePropagation", "StartDatePropagation");
            tableMapping.ColumnMappings.Add("EndDatePropagation", "EndDatePropagation");
            tableMapping.ColumnMappings.Add("Attachment", "Attachment");
            tableMapping.ColumnMappings.Add("PeriodName", "PeriodName");
            tableMapping.ColumnMappings.Add("IsGrouping", "IsGrouping");
            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectExGroupPeriod";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ExGroupPeriodId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ExGroupId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Bit);
            this.Adapter.SelectCommand.Parameters.Add("@CurrentDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@ExGroupCode", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteExGroupPeriod";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ExGroupPeriodId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "ExGroupPeriodId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertExGroupPeriod";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExGroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "ExGroupId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsGrouping", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsGrouping", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 250, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDatePropagation", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "StartDatePropagation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDatePropagation", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "EndDatePropagation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Attachment", System.Data.SqlDbType.NVarChar, 255, System.Data.ParameterDirection.Input, 0, 0, "Attachment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodName", System.Data.SqlDbType.NVarChar, 255, System.Data.ParameterDirection.Input, 0, 0, "PeriodName", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateExGroupPeriod";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExGroupId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "ExGroupId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsGrouping", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsGrouping", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 250, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ExGroupPeriodId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "ExGroupPeriodId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExGroupPeriodId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "ExGroupPeriodId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDatePropagation", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "StartDatePropagation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDatePropagation", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "EndDatePropagation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Attachment", System.Data.SqlDbType.NVarChar, 255, System.Data.ParameterDirection.Input, 0, 0, "Attachment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodName", System.Data.SqlDbType.NVarChar, 255, System.Data.ParameterDirection.Input, 0, 0, "PeriodName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
     
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.ExpertGroupDataSet.tblExGroupPeriodDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }

        public void FindByCode(int ExGroupPeriodId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ExGroupPeriodId"].Value = ExGroupPeriodId;
            Fill();
        }


        public void FindByExGroupId(int ExGroupId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ExGroupId"].Value = ExGroupId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetActive(bool InActive)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
            return this.DataTable;
        }
         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectActiveExpertGroupByCode(int ExGroupCode)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@ExGroupCode"].Value = ExGroupCode;
            Fill();
            return this.DataTable;
        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetActivePeriodByDate(string CurrentDate)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = false;
            this.Adapter.SelectCommand.Parameters["@CurrentDate"].Value = CurrentDate;
            Fill();
            return this.DataTable;
        }
    }
}
