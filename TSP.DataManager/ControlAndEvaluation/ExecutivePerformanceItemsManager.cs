using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.TechnicalServices
{
    public class ExecutivePerformanceItemsManager : BaseObject
    {
        public ExecutivePerformanceItemsManager()
            : base()
        {

        }
        public ExecutivePerformanceItemsManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSExecutivePerformanceItems";
            tableMapping.ColumnMappings.Add("ExePerfItemsId", "ExePerfItemsId");
            tableMapping.ColumnMappings.Add("Item", "Item");
            tableMapping.ColumnMappings.Add("FirstExePerfTitleId", "FirstExePerfTitleId");
            tableMapping.ColumnMappings.Add("SecondExePerfTitleId", "SecondExePerfTitleId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            this.Adapter.TableMappings.Add(tableMapping);
            
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSExecutivePerformanceItems";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ExePerfItemsId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSExecutivePerformanceItems";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ExePerfItemsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ExePerfItemsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Item", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Item", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstExePerfTitleId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSExecutivePerformanceItems";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExePerfItemsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ExePerfItemsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Item", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Item", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstExePerfTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
           
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSExecutivePerformanceItems";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExePerfItemsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ExePerfItemsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Item", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Item", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstExePerfTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ExePerfItemsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ExePerfItemsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Item", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Item", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstExePerfTitleId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondExePerfTitleId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecondExePerfTitleId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Original, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.ControlAndEvaluation.ControlAndEvaluationDataSet.TSExecutivePerformanceItemsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByExePerfItemsId(int ExePerfItemsId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ExePerfItemsId"].Value = ExePerfItemsId;
            Fill();
        }

    }
}
