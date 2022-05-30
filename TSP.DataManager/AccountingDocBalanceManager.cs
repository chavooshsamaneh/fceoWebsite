using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public  class AccountingDocBalanceManager:BaseObject
    {
        //static AccountingDocBalanceManager()
        //{
        //    _tableId = TableType.AccountingDocBalance;
        //}

         public AccountingDocBalanceManager()
            : base()
        {
         
        }
        public AccountingDocBalanceManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingDocBalance);
        }
        protected override void InitAdapter()
        {



            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.DocBalance";
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("DocOperationId", "DocOperationId");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("DocDate", "DocDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("ImageURL", "ImageURL");
            tableMapping.ColumnMappings.Add("DirectAccess", "DirectAccess");
            tableMapping.ColumnMappings.Add("TTId", "TTId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("DocStatusId", "DocStatusId");

            
            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocBalance";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@DocBalanceId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocOperationId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.NChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.NChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocNumber", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@FromDocNo", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@ToDocNo", System.Data.SqlDbType.Int, 4);



            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocBalance";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocBalance";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocOperationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocOperationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Image", global::System.Data.SqlDbType.Image, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Image", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DirectAccess", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumber", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RefName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RefName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));                                  

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocBalance";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocOperationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocOperationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Image", global::System.Data.SqlDbType.Image, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Image", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DirectAccess", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocNumber", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RefName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RefName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));                                  

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingDocBalanceDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_DocBalanceId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_DocBalanceId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(int DocOperationId, string RegisterDate, string DocDate, string Description, bool DirectAccess, bool InActive, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(DocOperationId));
            if ((RegisterDate == null))
            {
                throw new global::System.ArgumentNullException("RegisterDate");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(RegisterDate));
            }
            if ((DocDate == null))
            {
                throw new global::System.ArgumentNullException("DocDate");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(DocDate));
            }
            if ((Description == null))
            {
                this.Adapter.InsertCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(Description));
            }
            this.Adapter.InsertCommand.Parameters[5].Value = ((bool)(DirectAccess));
            this.Adapter.InsertCommand.Parameters[6].Value = ((bool)(InActive));
            this.Adapter.InsertCommand.Parameters[7].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[8].Value = ((System.DateTime)(ModifiedDate));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int DocOperationId, string RegisterDate, string DocDate, string Description,bool DirectAccess, bool InActive, int UserId, System.DateTime ModifiedDate, int Original_DocBalanceId, byte[] Original_LastTimeStamp, int DocBalanceId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(DocOperationId));
            if ((RegisterDate == null))
            {
                throw new global::System.ArgumentNullException("RegisterDate");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(RegisterDate));
            }
            if ((DocDate == null))
            {
                throw new global::System.ArgumentNullException("DocDate");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(DocDate));
            }
            if ((Description == null))
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[5].Value = ((bool)(DirectAccess));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((bool)(InActive));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[8].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(Original_DocBalanceId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[11].Value = ((int)(DocBalanceId));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int DocOperationId, string RegisterDate, string DocDate, string Description,bool DirectAccess, bool InActive, int UserId, System.DateTime ModifiedDate, int Original_DocBalanceId, byte[] Original_LastTimeStamp)
        {
            return this.Update(DocOperationId, RegisterDate, DocDate, Description, DirectAccess,InActive, UserId, ModifiedDate, Original_DocBalanceId, Original_LastTimeStamp, Original_DocBalanceId);
        }



        public void FindById(int Id)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(Id.ToString()))

                Id = -1;

            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = Id;
           
            Fill();
        
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentId(int DocOperationId)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(DocOperationId.ToString()))
                                   
                DocOperationId = -1;

            this.Adapter.SelectCommand.Parameters["@DocOperationId"].Value = DocOperationId;
            Fill();
            return this.DataTable;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentIdWithDate(int DocOperationId,string FromDate,string ToDate,int InActive)
        {
            ResetAllParameters();
            
            if (string.IsNullOrEmpty(DocOperationId.ToString()))

                DocOperationId = -1;
            this.Adapter.SelectCommand.Parameters["@DocOperationId"].Value = DocOperationId;
            
            if (!string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(ToDate))
                ToDate = FromDate;

            if (string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                FromDate = ToDate;

            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            
            if (string.IsNullOrEmpty(InActive.ToString()))
                InActive = 0;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
            return this.DataTable;            
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindInActiveByParentId(int DocOperationId, int InActive)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(DocOperationId.ToString()))
                DocOperationId = -1;

            if (string.IsNullOrEmpty(InActive.ToString()))
                InActive = 1;

            this.Adapter.SelectCommand.Parameters["@DocOperationId"].Value = DocOperationId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public string SelectLastDocBalanceDate()
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "[spLastDocBalanceDate]";

            sc.Connection.Open();

            sc.Parameters.Add("@Max", System.Data.SqlDbType.VarChar,10);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;

            sc.ExecuteNonQuery();
            sc.Connection.Close();

            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return ((sc.Parameters["@Max"].Value).ToString());
            else
                return "";
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public string SelectFirstDocBalanceDate()
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "[spLastDocBalanceDate]";

            sc.Connection.Open();

            sc.Parameters.Add("@Max", System.Data.SqlDbType.VarChar,10);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;

            sc.Parameters.AddWithValue("@Flag", 1);
            
            sc.ExecuteNonQuery();
            sc.Connection.Close();

            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return ((sc.Parameters["@Max"].Value).ToString());
            else
                return "";
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodDocBalance(int PeriodId,int DocOperationId)
        {
            if (PeriodId != -1)
            {
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]"; 
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = "SELECT" + DocBalance + ".*, " + DocOperation + ".DocStatusId,InActiveName=case when "+DocBalance+".InActive=0 then N'فعال' else N'باطل شده' end	FROM" + DocBalance + " inner join  " + DocOperation + " ON " + DocBalance + ".DocOperationId=" + DocOperation + ".DocOperationId WHERE " + DocOperationId + "=-1 OR" + DocBalance + ".DocOperationId=" + DocOperationId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int DocBalanceId)
        {
            if (PeriodId != -1)
            {
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = "SELECT" + DocBalance + ".*, " + DocOperation + ".DocStatusId,InActiveName=case when " + DocBalance + ".InActive=0 then N'فعال' else N'باطل شده' end	FROM" + DocBalance + " inner join  " + DocOperation + " ON " + DocBalance + ".DocOperationId=" + DocOperation + ".DocOperationId WHERE " + DocBalanceId + "=-1 OR" + DocBalance + ".DocBalanceId=" + DocBalanceId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public int  GetNewDocNumber(int AgentId)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            sqlAdapter.SelectCommand = new SqlCommand();
            sqlAdapter.SelectCommand.Connection = this.Connection;
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.CommandText = "spSelectAccountingAccountNewNumber";
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            DataTable dt = new DataTable();
            sqlAdapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                return (Convert.ToInt32(dt.Rows[0]["DocNumber"]));
            }
            return -1;
        }

        public void FindActiveDocByDocNumber(int AgentId, int DocNumber)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = DocNumber;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;

            Fill();

        }

        public DataTable FindActiveDocByDocNumber(int AgentId, int FromDocNo,int ToDocNo)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@FromDocNo"].Value = FromDocNo;
            this.Adapter.SelectCommand.Parameters["@ToDocNo"].Value = ToDocNo;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;

            Fill();
            return (this.DataTable);
        }


        public void UpdateDocumentNumberByDate(int AgentId, int PeriodId)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = new SqlCommand();
            sqlDataAdapter.SelectCommand.Connection = this.Connection;
            sqlDataAdapter.SelectCommand.CommandText = "spUpdateAccountingDocumentNumberByDate";
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Transaction = this.Transaction;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PeriodId", PeriodId);
            sqlDataAdapter.Fill(this.DataTable);            
        }


    }
}
