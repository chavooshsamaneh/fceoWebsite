using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager
{
    public class ImageReportManager:BaseObject
    {
         public ImageReportManager()
            : base()
        {
        }
        public ImageReportManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        //public static Permission GetUserPermission(int UserId, UserType ut)
        //{
        //    return BaseObject.GetUserPermission(UserId, ut, TableType.ImageReport);
        //}
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblImageReport";
            tableMapping.ColumnMappings.Add("RId", "RId");
            tableMapping.ColumnMappings.Add("Subject", "Subject");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("ImageUrl", "ImageUrl");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectImageReport";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Subject", System.Data.SqlDbType.NVarChar,50);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.Char,10);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.Char,10);
           
            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteImageReport";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertImageReport";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateImageReport";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "RId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblImageReportDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
            [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
            public virtual int Delete(int Original_RId, byte[] Original_LastTimeStamp)
            {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_RId));
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
            public virtual int Insert(string Subject, string Description, System.DateTime Date, string ImageUrl, int UserId, System.DateTime ModifiedDate)
            {
                if ((Subject == null))
                {
                    throw new global::System.ArgumentNullException("Subject");
                }
                else
                {
                    this.Adapter.InsertCommand.Parameters[1].Value = ((string)(Subject));
                }
                if ((Description == null))
                {
                    this.Adapter.InsertCommand.Parameters[2].Value = global::System.DBNull.Value;
                }
                else
                {
                    this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Description));
                }
                this.Adapter.InsertCommand.Parameters[3].Value = ((System.DateTime)(Date));
                if ((ImageUrl == null))
                {
                    throw new global::System.ArgumentNullException("ImageUrl");
                }
                else
                {
                    this.Adapter.InsertCommand.Parameters[4].Value = ((string)(ImageUrl));
                }
                this.Adapter.InsertCommand.Parameters[5].Value = ((int)(UserId));
                this.Adapter.InsertCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
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
            public virtual int Update(string Subject, string Description, System.DateTime Date, string ImageUrl, int UserId, System.DateTime ModifiedDate, int Original_RId, byte[] Original_LastTimeStamp, int RId)
            {
                if ((Subject == null))
                {
                    throw new global::System.ArgumentNullException("Subject");
                }
                else
                {
                    this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(Subject));
                }
                if ((Description == null))
                {
                    this.Adapter.UpdateCommand.Parameters[2].Value = global::System.DBNull.Value;
                }
                else
                {
                    this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Description));
                }
                this.Adapter.UpdateCommand.Parameters[3].Value = ((System.DateTime)(Date));
                if ((ImageUrl == null))
                {
                    throw new global::System.ArgumentNullException("ImageUrl");
                }
                else
                {
                    this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(ImageUrl));
                }
                this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(UserId));
                this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
                this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_RId));
                if ((Original_LastTimeStamp == null))
                {
                    throw new global::System.ArgumentNullException("Original_LastTimeStamp");
                }
                else
                {
                    this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
                }
                this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(RId));
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
        public void FindByCode(int RId)
        {
            this.Adapter.SelectCommand.Parameters["@RId"].Value = RId;
            this.Fill();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchImgReport(string Subject, string FromDate , string ToDate)
        {
            if (string.IsNullOrEmpty(Subject))
                Subject = "%";
            this.Adapter.SelectCommand.Parameters["@Subject"].Value = Subject;
            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";
            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            
            Fill();
            return DataTable;
        }
    }
}
