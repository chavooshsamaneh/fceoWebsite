using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager.TechnicalServices
{
   public class DevelopmentObserversManager :BaseObject
    {
       public DevelopmentObserversManager()
            : base()
        {

        }
       public DevelopmentObserversManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSDevelopmentObservers);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSDevelopmentObservers";
            tableMapping.ColumnMappings.Add("DevObsId", "DevObsId");
            tableMapping.ColumnMappings.Add("DevelopmentPercentId", "DevelopmentPercentId");
            tableMapping.ColumnMappings.Add("PrjObsMeId", "PrjObsMeId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("IsAgent", "IsAgent");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("ConfirmDate", "ConfirmDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("InActiveDate", "InActiveDate");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSDevelopmentObservers";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@DevObsId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DevelopmentPercentId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.TinyInt);
            this.Adapter.SelectCommand.Parameters.Add("@IsAgent", SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSDevelopmentObservers";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DevObsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DevObsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSDevelopmentObservers";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DevelopmentPercentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DevelopmentPercentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjObsMeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjObsMeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsAgent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsAgent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSDevelopmentObservers";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DevelopmentPercentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DevelopmentPercentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjObsMeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjObsMeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsAgent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsAgent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DevObsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DevObsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DevObsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "DevObsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
      
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSDevelopmentObserversDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
       public void FindByDevObsId(int DevObsId)
       {
           ResetAllParameters();
           this.Adapter.SelectCommand.Parameters["@DevObsId"].Value = DevObsId;
           Fill();
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable FindByDevelopmentPercentId(int DevelopmentPercentId)
       {
           ResetAllParameters();
           this.Adapter.SelectCommand.Parameters["@DevelopmentPercentId"].Value = DevelopmentPercentId;

           Fill();
           return this.DataTable;
       }


       public void FindDevelopmentPercentAgent(int DevelopmentPercentId, int IsAgent)
       {
           ResetAllParameters();
           this.Adapter.SelectCommand.Parameters["@DevelopmentPercentId"].Value = DevelopmentPercentId;
           this.Adapter.SelectCommand.Parameters["@IsAgent"].Value = IsAgent;

           Fill();

       }
    }
}
