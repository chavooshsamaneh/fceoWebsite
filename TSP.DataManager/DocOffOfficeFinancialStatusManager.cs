using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
   public class DocOffOfficeFinancialStatusManager:BaseObject
    {
       public DocOffOfficeFinancialStatusManager()
            : base()
        {

        }
       public DocOffOfficeFinancialStatusManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
       public static Permission GetUserPermission(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeFinancialStatus);
       }

       public static Permission GetUserPermissionForOffDoc(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeFinancialStatusDocument);
       }

        protected override void InitAdapter()
        {                     
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DocOffOfficeFinancialStatus";
            tableMapping.ColumnMappings.Add("OfsId", "OfsId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("OfdId", "OfdId");
            tableMapping.ColumnMappings.Add("OfReId", "OfReId");
            tableMapping.ColumnMappings.Add("Value", "Value");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocOffOfficeFinancialStatus";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@OfsId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@JustActive", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = (int)TableCodes.OfficeFinancialStatus;

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocOffOfficeFinancialStatus";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocOffOfficeFinancialStatus";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Value", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Value", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocOffOfficeFinancialStatus";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Value", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Value", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "OfsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.DocOffOfficeFinancialStatusDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

       public DataTable FindByOffRequest(int OfId, int OfReId, short IsConfirm, short JustActive)
       {
           this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
           this.Adapter.SelectCommand.Parameters["@OfReId"].Value = OfReId;
           this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
           this.Adapter.SelectCommand.Parameters["@JustActive"].Value = JustActive;


           Fill();
           return this.DataTable;

       }
       public void FindForDelete(int OfReId)
       {

           SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOffOfficeFinancialStatusDelete", this.Connection);
           adapter.SelectCommand.Transaction = this.Transaction;
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int, 4, "OfReId").Value = OfReId;
           adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = (int)TableCodes.OfficeFinancialStatus;           

           adapter.Fill(DataTable);


       }
       public void FindByCode(int OfsId)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocOffOfficeFinancialStatus";
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@OfsId", SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@OfReId", System.Data.SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.SmallInt);


           this.Adapter.SelectCommand.Parameters["@OfsId"].Value = OfsId;

           Fill();
       }
       public void FindByOfCode(int OfId)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocOffOfficeFinancialStatus";
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@OfsId", SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@OfReId", System.Data.SqlDbType.Int);
           this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.SmallInt);


           this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;

           Fill();
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectForImplementDoc(int OfReId)
       {
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOffOfficeFinancialStatusForImplementDoc", this.Connection);
           adapter.SelectCommand.Transaction = this.Transaction;
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@OfsId", SqlDbType.Int, 4).Value = -1;
           adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int, 4).Value = OfReId;
           adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4).Value = -1;           

           adapter.Fill(DataTable);
           return (DataTable);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public void FindByType(int OfsId, int Type)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "spSelectDocOffOfficeFinancialStatusByType";
           //SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOffOfficeFinancialStatusByType", this.Connection);
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@OfsId", SqlDbType.Int, 4).Value = OfsId;
           this.Adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int, 4).Value = -1;
           this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4).Value = Type;

           Fill();
           //adapter.Fill(DataTable);
           //return (DataTable);
       }
    }
}
