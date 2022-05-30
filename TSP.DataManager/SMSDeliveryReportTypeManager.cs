using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class SMSDeliveryReportTypeManager : BaseObject
    {
        protected override void InitAdapter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    //this._dataTable = new DataManager.NezamFarsDataSet.tblSMSDeliveryReportTypeDataTable();
                   // this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
    }
}
