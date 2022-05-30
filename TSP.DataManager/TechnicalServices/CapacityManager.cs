using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.TechnicalServices
{
    public class CapacityManager : BaseObject
    {
        public CapacityManager()
            : base()
        {

        }
        public CapacityManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TsCapacity);
        }

        protected override void InitAdapter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DataTable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
    }
}
