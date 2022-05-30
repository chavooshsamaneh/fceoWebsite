using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
   public class ExpertCostAuditManager :BaseObject
    {
       public static Permission GetUserPermission(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.ExpertCostAuditList);
       }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "ExpertExpertCostAudit";
            tableMapping.ColumnMappings.Add("CaId", "CaId");
            tableMapping.ColumnMappings.Add("CaListId", "CaListId");
            tableMapping.ColumnMappings.Add("EvaluationAmount", "EvaluationAmount");
            tableMapping.ColumnMappings.Add("Surplus", "Surplus");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("Total", "Total");

            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectExpertCostAudit";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CaId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CaListId", SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteExpertCostAudit";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CaId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CaId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertExpertCostAudit";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CaListId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CaListId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EvaluationAmount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "EvaluationAmount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Surplus", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Surplus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateExpertCostAudit";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CaListId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CaListId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EvaluationAmount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "EvaluationAmount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Surplus", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Surplus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CaId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CaId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CaId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CaId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
   
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.ExpertDataSet.ExpertExpertCostAuditDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
       public void FindByCode(int CaId)
       {
           this.Adapter.SelectCommand.Parameters["@CaId"].Value = CaId;
           Fill();
       }
       public void FindByCaListId(int CaListId)
       {
           this.Adapter.SelectCommand.Parameters["@CaListId"].Value = CaListId;
           Fill();
       }
       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectTotalValue(decimal EvaluationAmount, int CaListId)
       {
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectExpertCostAuditFormule", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@EvaluationAmount", SqlDbType.Money);
           adapter.SelectCommand.Parameters.Add("@CaListId", SqlDbType.Int);
           if (this.Transaction != null)
               adapter.SelectCommand.Transaction = this.Transaction;
           if (EvaluationAmount <= 0)
               EvaluationAmount = -1;
           adapter.SelectCommand.Parameters["@EvaluationAmount"].Value = EvaluationAmount;

           if (CaListId <= 0)
               CaListId = -1;
           adapter.SelectCommand.Parameters["@CaListId"].Value = CaListId;

           adapter.Fill(DataTable);
           return (DataTable);

       }
    }
}
