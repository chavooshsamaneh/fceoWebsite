using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace TSP.DataManager.TechnicalServices
{
    public class ImplementerOfficeManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSImplementerOffice);
        }
        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSImplementerOfficeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSImplementerOffice";
            tableMapping.ColumnMappings.Add("ImpOfficeId", "ImpOfficeId");
            tableMapping.ColumnMappings.Add("OfficeType", "OfficeType");
            tableMapping.ColumnMappings.Add("Name", "Name");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("RegisterNo", "RegisterNo");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("MeNo", "MeNo");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("DocFirstDate", "DocFirstDate");
            tableMapping.ColumnMappings.Add("DocDate", "DocDate");
            tableMapping.ColumnMappings.Add("DocExpireDate", "DocExpireDate");
            tableMapping.ColumnMappings.Add("ImpGradeId", "ImpGradeId");
            tableMapping.ColumnMappings.Add("DocImgUrlFront", "DocImgUrlFront");
            tableMapping.ColumnMappings.Add("DocImgUrlBack", "DocImgUrlBack");
            tableMapping.ColumnMappings.Add("PostalCode", "PostalCode");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("LastTimestamp", "LastTimestamp");
            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectTSImplementerOffice";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ImpOfficeId", SqlDbType.Int);
            
            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTSImplementerOffice";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ImpOfficeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImpOfficeId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimestamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimestamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimestamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimestamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertTSImplementerOffice";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OfficeType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OfficeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Name", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Name", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocFirstDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocFirstDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImpGradeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImpGradeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocImgUrlFront", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocImgUrlFront", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocImgUrlBack", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocImgUrlBack", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PostalCode", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PostalCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WfCurrentStatId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WfCurrentStatId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WfCurrentTaskId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WfCurrentTaskId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTSImplementerOffice";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OfficeType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OfficeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Name", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Name", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocFirstDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocFirstDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImpGradeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImpGradeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocImgUrlFront", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocImgUrlFront", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocImgUrlBack", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocImgUrlBack", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PostalCode", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PostalCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WfCurrentStatId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WfCurrentStatId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WfCurrentTaskId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WfCurrentTaskId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ImpOfficeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImpOfficeId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimestamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimestamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimestamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimestamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImpOfficeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ImpOfficeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }
        public void SelectById(int ImpOfficeId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ImpOfficeId"].Value = ImpOfficeId;
            this.Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSImplementerOfficeForManagmentPage(int ImpOfficeId, string Name, string FileNo,string MeNo, int TaskId)
        {
            if (ImpOfficeId == -1 && Name == "%" && Name == "%" && FileNo == "%" && MeNo=="%" && TaskId == -1)
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSImplementerOfficeForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ImpOfficeId", ImpOfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@Name", Name);
            adapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MeNo", MeNo); 
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);

            adapter.Fill(dt);

            return dt;
        }
    }
}
