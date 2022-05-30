using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingCostSettingsManager : BaseObject
    {
        public AccountingCostSettingsManager()
            : base()
        {
        }

        public AccountingCostSettingsManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.CostSettings);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "AccountingCostSettings";
            tableMapping.ColumnMappings.Add("CSId", "CSId");
            tableMapping.ColumnMappings.Add("SData", "SData");
            tableMapping.ColumnMappings.Add("SValue", "SValue");
            tableMapping.ColumnMappings.Add("SName", "SName");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectCostSettings";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CSId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@SData", System.Data.SqlDbType.NVarChar);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteCostSettings";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertCostSettings";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SData", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SData", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SValue", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "SValue", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateCostSettings";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SData", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SData", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SValue", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "SValue", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CSId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.AccountingDataSet.AccountingCostSettingsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCSId(int CSId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CSId"].Value = CSId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentId(int AgentId)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        public void FindBySData(string SData, int AgentId)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(SData))
                SData = "%";

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@SData"].Value = SData;
            Fill();
        }

        public static Decimal FindCostByType(TSP.DataManager.CostSettingsSData CostType)
        {
            TSP.DataManager.AccountingCostSettingsManager AccountingCostSettingsManager = new DataManager.AccountingCostSettingsManager();
            Decimal Cost = 0;
            AccountingCostSettingsManager.FindBySData(CostType.ToString(), Utility.GetCurrentAgentCode());
            if (AccountingCostSettingsManager.Count != 1)
                return 0;
            Cost = Convert.ToDecimal(AccountingCostSettingsManager[0]["SValue"]);
            return Cost;
        }

        public static Decimal FindCostByTypePersian(TSP.DataManager.CostSettingsSData CostType)
        {
            TSP.DataManager.AccountingCostSettingsManager AccountingCostSettingsManager = new DataManager.AccountingCostSettingsManager();
            AccountingCostSettingsManager.FindBySData(CostType.ToString(), Utility.GetCurrentAgentCode());
            if (AccountingCostSettingsManager.Count != 1)
                return 0;
            string Cost = Convert.ToDecimal(AccountingCostSettingsManager[0]["SValue"]).ToString("#,#").Replace(".", String.Empty);
            return Convert.ToDecimal(Cost);
        }
    }
}
