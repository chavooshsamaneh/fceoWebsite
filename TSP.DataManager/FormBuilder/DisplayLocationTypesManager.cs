using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.FormBuilder
{
    public class DisplayLocationTypesManager : BaseObject
    {
        public enum Types
        {
            EmployeePortal = 1,
            MembersPortal = 2,
            OfficePortal = 3,
            TeacherPortal = 4,
            InstituePortal = 5,
            SettlementPortal = 6,
            MunicipalityPortal = 7,
            HomePage=8,
            AfterMemberLogin=9,
            Period=10
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DisplayLocationTypes";
            tableMapping.ColumnMappings.Add("TypeId", "TypeId");
            tableMapping.ColumnMappings.Add("TypeName", "TypeName");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UsageType", "UsageType");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectFormBuilderDisplayLocationTypes";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.FormBuilder.FormBuilderDataSet.DisplayLocationTypesDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDisplayLocationForPoll()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectFormBuilderDisplayLocationTypesForPoll";            
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(dt);
            return (dt);
        }
    }
}
