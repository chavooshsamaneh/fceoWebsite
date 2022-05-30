using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager.Session
{
    public class SessionDeclareTypesManager : BaseObject
    {
        public enum DeclareTypes
        {
            SMS = 1,
            Email = 2,
            Message = 4
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "SessionDeclareTypes";
            tableMapping.ColumnMappings.Add("TypeId", "TypeId");
            tableMapping.ColumnMappings.Add("TypeName", "TypeName");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSession_SessionDeclareTypes";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Session.SessionDataSet.SessionDeclareTypesDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
    }
}
