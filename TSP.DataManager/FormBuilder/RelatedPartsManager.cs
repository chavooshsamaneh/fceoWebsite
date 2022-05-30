using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager.FormBuilder
{
    public class RelatedPartsManager : BaseObject
    {
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "RelatedParts";
            tableMapping.ColumnMappings.Add("PartId", "PartId");
            tableMapping.ColumnMappings.Add("TtCode", "TtCode");
            tableMapping.ColumnMappings.Add("PartName", "PartName");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectFormBuilderRelatedParts";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TtCode", System.Data.SqlDbType.VarChar, 4);

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.FormBuilder.FormBuilderDataSet.RelatedPartsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByTtCode(String TtCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TtCode"].Value = TtCode;
            Fill();
        }
    }
}
