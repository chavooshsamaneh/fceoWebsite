using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data;
using System.Data.SqlClient;
 
namespace TSP.DataManager
{
  public  class TrainingAcceptedGradeManager :BaseObject
    {

      public static Permission GetUserPermission(int UserId, UserType ut)
      {
          return BaseObject.GetUserPermission(UserId, ut, TableType.TrainingAcceptedGrade);
      }
        protected override void InitAdapter()
        {           
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTrainingAcceptedGrade";
            tableMapping.ColumnMappings.Add("TrGrId", "TrGrId");
            tableMapping.ColumnMappings.Add("UpGrdPId", "UpGrdPId");
            tableMapping.ColumnMappings.Add("PkId", "PkId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTrainingAcceptedGrade";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.TinyInt);
            this.Adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TrGrId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@UpGrdPId", SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTrainingAcceptedGrade";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TrGrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TrGrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTrainingAcceptedGrade";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpGrdPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UpGrdPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PkId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PkId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTrainingAcceptedGrade";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpGrdPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UpGrdPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PkId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PkId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TrGrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TrGrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TrGrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TrGrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
      
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblTrainingAcceptedGradeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

      [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
      public DataTable FindByPKCode(int PkId,byte Type)
      {
          ResetAllParameters();
          this.Adapter.SelectCommand.Parameters["@PkId"].Value = PkId;
          this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;

          Fill();
          return this.DataTable;
      }

      public void FindByCode(int TrGrId)
      {
          ResetAllParameters();
          this.Adapter.SelectCommand.Parameters["@TrGrId"].Value = TrGrId;

          Fill();
      }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectActivePeriodsForUpgrad(int MjId,int ResId, int GrdIdOrigin)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingAcceptedGrade", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PkId",-1);
            adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.TinyInt, 4, "Type");
            adapter.SelectCommand.Parameters.AddWithValue("@TrGrId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@UpGrdPId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@InactiveUpgrade", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@ResId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdIdOrigin", -1);


            adapter.Fill(dt);
            return (dt);
        }

        public DataTable FindByUpGrdId(int UpGrdPId)
      {
          DataTable dt = new DataTable();
          SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingAcceptedGrade", this.Connection);
          adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
          adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId");
          adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4, "Type");
          adapter.SelectCommand.Parameters.Add("@TrGrId", SqlDbType.Int, 4, "TrGrId");
          adapter.SelectCommand.Parameters.Add("@UpGrdPId", SqlDbType.Int, 4, "UpGrdPId").Value = UpGrdPId;

          adapter.Fill(dt);
          return (dt);
      }
    }
}

