using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace TSP.DataManager
{
    public class PrintAssignerSettingManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintAssignerSetting);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPrintAssignerSetting";
            tableMapping.ColumnMappings.Add("PrtAsId", "PrtAsId");
            tableMapping.ColumnMappings.Add("PrtsId", "PrtsId");
            tableMapping.ColumnMappings.Add("AssignerOrder", "AssignerOrder");
            tableMapping.ColumnMappings.Add("GmtId", "GmtId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectPrintAssignerSetting";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtAsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrtAsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignerOrder", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignerOrder", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmnId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GmnId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.SelectCommand.Parameters.AddWithValue("@Date", "");

            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeletePrintAssignerSetting";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrtAsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtAsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertPrintAssignerSetting";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignerOrder", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignerOrder", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GmtId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdatePrintAssignerSetting";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignerOrder", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignerOrder", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GmtId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrtAsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrtAsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrtAsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrtAsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblPrintAssignerSettingDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int PrtAsId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrtAsId"].Value = PrtAsId;
            Fill();
        }



        public void FindByPrintTypeId(int PrtTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrtTypeId"].Value = PrtTypeId;
            Fill();
        }


        public void FindByPrintTypeIdAndDate(int PrtTypeId, string Date)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrtTypeId"].Value = PrtTypeId;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = Date;
            Fill();
            

        }
        public void FindByPrintSettingCode(int PrtsId, int AssignerOrder)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrtsId"].Value = PrtsId;
            this.Adapter.SelectCommand.Parameters["@AssignerOrder"].Value = AssignerOrder;
            Fill();
        }

        public void FindByPrintSettingCode(int PrtsId)
        {
            FindByPrintSettingCode(PrtsId, -1);
        }
       
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindGovermentManager(int GmnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@GmnId"].Value = GmnId;
            Fill();
            return this.DataTable;
        }
    }
}
