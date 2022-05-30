using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.TechnicalServices
{
    public class RepeatFactorsManager : BaseObject
    {
        public RepeatFactorsManager()
            : base()
        {

        }
        public RepeatFactorsManager(System.Data.DataSet ds)
            : base(ds)
        {

        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSRepeatFactors";
            tableMapping.ColumnMappings.Add("RepeatId", "RepeatId");
            tableMapping.ColumnMappings.Add("RepeatCount", "RepeatCount");
            tableMapping.ColumnMappings.Add("RepeatFactor", "RepeatFactor");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSRepeatFactors";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RepeatId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSRepeatFactors";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatCount", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatFactor", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatFactor", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSRepeatFactors";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatFactor", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatFactor", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSRepeatFactors";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RepeatFactor", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatFactor", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatCount", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RepeatFactor", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "RepeatFactor", System.Data.DataRowVersion.Original, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSRepeatFactorsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByRepeatId(int RepeatId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RepeatId"].Value = RepeatId;
            Fill();
        }
    }
}
