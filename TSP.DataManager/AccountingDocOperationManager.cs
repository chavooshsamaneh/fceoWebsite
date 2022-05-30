using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace TSP.DataManager
{
    public  class AccountingDocOperationManager:BaseObject
    {
        //static AccountingDocOperationManager()
           
        //{
        //    _tableId = TableType.AccountingDocOperation;
        //}

         public AccountingDocOperationManager()
            : base()
        {
            
        }
        public AccountingDocOperationManager(System.Data.DataSet ds)
            : base(ds)
        {
            
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingDocOperation);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.DocOperation";
            tableMapping.ColumnMappings.Add("DocOperationId", "DocOperationId");
            tableMapping.ColumnMappings.Add("DocNumber", "DocNumber");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("DocStatusId", "DocStatusId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("DocNumberFinaly", "DocNumberFinaly");
            tableMapping.ColumnMappings.Add("RefName", "RefName");
            tableMapping.ColumnMappings.Add("ConvertedID", "ConvertedID");
            tableMapping.ColumnMappings.Add("IsMerged","IsMerged");
	        tableMapping.ColumnMappings.Add("RelatedId","RelatedId");
	        tableMapping.ColumnMappings.Add("IsBalanced","IsBalanced");
            tableMapping.ColumnMappings.Add("OpCode", "OpCode");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("DocStatusName", "DocStatusName");
            tableMapping.ColumnMappings.Add("TableTypeId", "TableTypeId");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocOperation";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@DocOperationId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocNumber", System.Data.SqlDbType.NVarChar ,50);
            this.Adapter.SelectCommand.Parameters.Add("@DocNumberFinaly", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocStatusId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.NChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.NChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@AccId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@PrjId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            //this.Adapter.SelectCommand.Parameters.Add("@ReturnDocFlage", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocOperation";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocOperationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocOperationId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocOperation";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumber", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumberFinaly", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumberFinaly", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RefName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RefName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ConvertedID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ConvertedID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsMerged", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsMerged", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RelatedId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RelatedId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsBalanced", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsBalanced", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OpCode", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OpCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocOperation";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumber", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumberFinaly", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumberFinaly", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RefName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RefName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ConvertedID", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ConvertedID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsMerged", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsMerged", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RelatedId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RelatedId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsBalanced", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsBalanced", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OpCode", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OpCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocOperationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocOperationId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocOperationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "DocOperationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.AccountingDataSet.AccountingDocOperationDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocOperationId"].Value = Id;
            Fill();
        }

        public void FindByIdInAll(int Id)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@DocOperationId"].Value = Id;
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Movaghat;

            Fill();
        }


        public void FindByDocNumber(string docnumber)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = docnumber;
            Fill();
        }


        public void FindByDocNumberInAll(string docnumber)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = docnumber;
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Movaghat;


            Fill();
        }

        public void FindByDocNumber(string docnumber,int AgentId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = docnumber;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            Fill();
        }

        public void FindByDocNumberInAll(string docnumber, int AgentId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = docnumber;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Movaghat;
            
            Fill();
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable  FillAllDocNumberWithoutDaem()
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Movaghat;
            Fill();
            return this.DataTable;
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FillAllDocNumberWithoutDaem(int AgentId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Movaghat;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FillAllDocNumberDraft()
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.PishNevis;
            Fill();
            return this.DataTable;
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FillAllDocNumberDaem()
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Daem;
            Fill();
            return this.DataTable;
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FillAllDocNumberDaem(int AgentId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = (int)DocumentStatusType.Daem;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;

        }       

        public void FindByDocNumberFinaly(string docnumber)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocNumberFinaly"].Value = docnumber;
            Fill();
        }

        public void FindByDocNumberFinaly(string docnumber, int AgentId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@DocNumberFinaly"].Value = docnumber;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            Fill();
        }

        public void FindByAgentId( int AgentId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
        }

        public void FindByDocStatus(int DocStatusId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = DocStatusId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectInActiveDocOp(int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectInActiveDocOperation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMovaghatActiveDocOp(int AgentId,char Flag)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocoperationMovahat", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@Flag", Flag);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDraftActiveDocOp(int AgentId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOperationDraft", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Connection = this.Connection;
            adapter.SelectCommand.Transaction = this.Transaction;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDraftByDocOperationId(int DocOperationId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOperationDraft", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocOperationId.ToString()))
                DocOperationId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocOperationId", DocOperationId);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchDocOperation(string DocNumber,string DocNumberFinaly, int AgentId, int DocStatusId, string FromDate, string ToDate, int AccId, int PrjId)
        {            
            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = DocNumber;

            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumberFinaly"].Value = DocNumber;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId; 

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = DocStatusId;

            if (!string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(ToDate))
                ToDate = FromDate;

            if (string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                FromDate = ToDate;

            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;

            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;
            this.Adapter.SelectCommand.Parameters["@AccId"].Value = AccId;

            if (string.IsNullOrEmpty(PrjId.ToString()))
                PrjId = -1;
            this.Adapter.SelectCommand.Parameters["@PrjId"].Value = PrjId;

            Fill();

            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByAgent(int AgentId)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchDocOperationWithAccCode(string DocNumber, string DocNumberFinaly, int AgentId, int DocStatusId, string FromDate, string ToDate, string  AccCode, int PrjId)
        {
            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = DocNumber;

            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumberFinaly"].Value = DocNumber;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;
            this.Adapter.SelectCommand.Parameters["@DocStatusId"].Value = DocStatusId;

            if (!string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(ToDate))
                ToDate = FromDate;

            if (string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                FromDate = ToDate;

            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;

            if (string.IsNullOrEmpty(AccCode))
                AccCode = "%";
            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;

            if (string.IsNullOrEmpty(PrjId.ToString()))
                PrjId = -1;
            this.Adapter.SelectCommand.Parameters["@PrjId"].Value = PrjId;

            Fill();

            return this.DataTable;
        }


        public string GetMaxDocNumber(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spMaxDocNumber]";

            c.Parameters.Add("@AgentId", SqlDbType.Int, 4);
            c.Parameters.Add("@DocNumber", SqlDbType.NVarChar, 50);

            c.Parameters["@AgentId"].Direction = ParameterDirection.Input;
            c.Parameters["@DocNumber"].Direction = ParameterDirection.Output;

            c.Parameters["@AgentId"].Value = AgentCdoe;

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();

            if (c.Parameters["@DocNumber"].Value == null)
                return "";
            else
                return c.Parameters["@DocNumber"].Value.ToString();
        }

        public int GetLastDocNumber(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetLastDocNumber]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);

            c.Transaction = this.Transaction;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object DocNumber = c.ExecuteScalar();

            if (DocNumber == DBNull.Value)
                return 1;
            return int.Parse(DocNumber.ToString());
        }

        public void UpdateLastDocNumber(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spUpdateTableCodeDocNumber]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);
            c.Transaction = this.Transaction;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
            c.ExecuteNonQuery();

            //object DocNumber = c.ExecuteScalar();

            //if (DocNumber == DBNull.Value)
            //    return 0;
            //return int.Parse(DocNumber.ToString());
        }

        public int GetLastDocNumberFinally(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetLastDocNumberFinally]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object DocNumber = c.ExecuteScalar();

            if (DocNumber == DBNull.Value)
                return 1;
            return int.Parse(DocNumber.ToString());
        }

        public void UpdateLastDocNumberFinally(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spUpdateTableCodeDocNumberFinally]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);
            c.Transaction = this.Transaction;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
            c.ExecuteNonQuery();

            //object DocNumber = c.ExecuteScalar();

            //if (DocNumber == DBNull.Value)
            //    return 0;
            //return int.Parse(DocNumber.ToString());
        }

        public string GetMaxDocNumberFinaly(int AgentCode)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spMaxDocNumberFinaly]";

            c.Parameters.Add("@AgentId", SqlDbType.Int, 4);
            c.Parameters.Add("@DocNumberFinaly", SqlDbType.NVarChar, 50);
            c.Parameters["@AgentId"].Direction = ParameterDirection.Input;
            c.Parameters["@DocNumberFinaly"].Direction = ParameterDirection.Output;
            c.Parameters["@AgentId"].Value = AgentCode;

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();

            if (c.Parameters["@DocNumberFinaly"].Value == null)
                return "";
            else
                return c.Parameters["@DocNumberFinaly"].Value.ToString();
        }


        public void AfterDocOperationUndo(int AgentCode,string DocNumberFinaly)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spAfterDocOperationUndo]";

            c.Parameters.Add("@AgentId", SqlDbType.Int, 4);
            c.Parameters.Add("@DocNumberFinaly", SqlDbType.NVarChar, 50);
            c.Parameters["@AgentId"].Direction = ParameterDirection.Input;
            c.Parameters["@DocNumberFinaly"].Direction = ParameterDirection.Input;

            c.Parameters["@AgentId"].Value = AgentCode;
            c.Parameters["@DocNumberFinaly"].Value = DocNumberFinaly;

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int SelectMaxId(int AgentId)
        {          
            SqlCommand sc=new SqlCommand();
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "[spMaxDocOperation]";

            if(sc.Connection.State!=ConnectionState.Open)
                sc.Connection.Open();
            sc.Parameters.Add("@AgentId", System.Data.SqlDbType.NVarChar);
            sc.Parameters["@AgentId"].Value = AgentId;

            sc.Parameters.Add("@Max", System.Data.SqlDbType.SmallInt);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;           

            sc.ExecuteNonQuery();
            sc.Connection.Close();
            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return (Convert.ToInt32(sc.Parameters["@Max"].Value));
            else
                return 0;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int SelectDraftMaxId(int AgentId)
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "[spMaxDocOperationDraft]";

            if (sc.Connection.State != ConnectionState.Open)
                sc.Connection.Open();
            sc.Parameters.Add("@AgentId", System.Data.SqlDbType.NVarChar);
            sc.Parameters["@AgentId"].Value = AgentId;

            sc.Parameters.Add("@Max", System.Data.SqlDbType.SmallInt);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;

            sc.ExecuteNonQuery();
            sc.Connection.Close();
            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return (Convert.ToInt32(sc.Parameters["@Max"].Value));
            else
                return 0;
        }     

        public int SelectMaxId_ForTransaction(int AgentId)
        {
            SqlCommand sc = new SqlCommand("[spMaxDocOperation]", this.Connection);
            sc.Transaction = this.Transaction;
            sc.CommandType = CommandType.StoredProcedure;

            sc.Parameters.Add("@AgentId", System.Data.SqlDbType.NVarChar);
            sc.Parameters["@AgentId"].Value = AgentId;

            sc.Parameters.Add("@Max", System.Data.SqlDbType.SmallInt);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;

            sc.ExecuteNonQuery();
            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return (Convert.ToInt32(sc.Parameters["@Max"].Value));
            else
                return 0;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectChequeDocOp(int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectChequeDocOp", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectLastSubmitOpertaion()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOperation_LastSubmitOpertaion", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                adapter.Fill(dt);
            }
            catch (Exception) { }

            return (dt);
        }

        public DataTable SelectDocOperationForSubmit(String SubmitDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOperationForSubmit", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@SubmitDate", SubmitDate);

            try
            {
                adapter.Fill(dt);
            }
            catch (Exception) { }

            return (dt);
        }

        public DataTable SelectMaxDaem(int AgentId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMaxDaem", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Connection = this.Connection;
            adapter.SelectCommand.Transaction = this.Transaction;

            try
            {
                adapter.Fill(DataTable);
            }
            catch (Exception) { }

            return (DataTable);
        }

        public string GetBankDocNum(int TableTypeId, int TTId)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[SelectBankDocNum]";

            c.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            c.Parameters.AddWithValue("@TTId", TTId);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object BankDocNum = c.ExecuteScalar();

            if (BankDocNum == DBNull.Value || BankDocNum == null)

                return "";
            return BankDocNum.ToString();
        }

        public decimal GetExpertPlaceBed(int TableTypeId, int TTId,int AccId)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[SelectExpertPlaceBed]";

            c.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            c.Parameters.AddWithValue("@TTId", TTId);
            c.Parameters.AddWithValue("@AccId", AccId);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object Amount = c.ExecuteScalar();

            if (Amount == DBNull.Value || Amount == null)

                return 0;
            return Convert.ToDecimal(Amount);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodDocOperation(int PeriodId,int AgentId)
        {
            if (PeriodId != -1)
            {
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT   distinct " + DocOperation + ".*,[Accounting.DocStatus].DocStatusName " +
                                 " FROM " + DocOperation +
                                 " inner join [Accounting.DocStatus] ON " + DocOperation + ".DocStatusId=[Accounting.DocStatus].DocStatusId " +
                                 " WHERE " + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)"+
                                 " AND (" + DocOperation + ".InActive=0)";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int DocOperationId)
        {
            if (PeriodId != -1)
            {
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT   distinct " + DocOperation + ".*,[Accounting.DocStatus].DocStatusName " +
                                 " FROM " + DocOperation +
                                 " inner join [Accounting.DocStatus] ON " + DocOperation + ".DocStatusId=[Accounting.DocStatus].DocStatusId " +
                                 " WHERE " + DocOperationId + "=-1 OR " + DocOperation + ".DocOperationId=" + DocOperationId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodSelectInActiveDocOp(int PeriodId, int AgentId)
        {
            if (PeriodId != -1)
            {
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = " SELECT distinct  " + DocOperation + ".*,[Accounting.DocStatus].DocStatusName,[Accounting.Agent].Name " +
                                " FROM " + DocOperation +
                                " inner join [Accounting.DocStatus] ON " + DocOperation + ".DocStatusId=[Accounting.DocStatus].DocStatusId " +
                                " inner join " + DocBalance + " ON  " + DocOperation + ".DocOperationId=" + DocBalance + ".DocOperationId " +
                                " inner join " + DocBalanceDetail + " ON  " + DocBalance + ".DocBalanceId=" + DocBalanceDetail + ".DocBalanceId " +
                                " inner join [Accounting.Account] ON  " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId " +
                                " inner join [Accounting.Agent] ON " + DocOperation + ".AgentId=[Accounting.Agent].AgentId " +
                                " WHERE (" + DocOperation + ".InActive=1) " +
                                " AND(" + AgentId + "=-1 or " + DocOperation + ".AgentId=" + AgentId + ")";
                    
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }
    }
}
