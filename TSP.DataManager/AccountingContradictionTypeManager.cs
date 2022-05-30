using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class AccountingContradictionTypeManager : BaseObject
    {
        public AccountingContradictionTypeManager()
            : base()
        {
        }

        public AccountingContradictionTypeManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingContradictionType);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "AccountingContradictionType";
            tableMapping.ColumnMappings.Add("CTypeId", "CTypeId");
            tableMapping.ColumnMappings.Add("CTypeName", "CTypeName");
            this.Adapter.TableMappings.Add(tableMapping);
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                }

                return this._dataTable;
            }
        }
    }
}
