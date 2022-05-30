using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class TraceManager : BaseObject
    {
        public enum Types
        {
            Login = 1, LogOut = 2, LoginUnsuccessful = 3, TspAdmin = 4, TspAdmin_LoginUnsuccessful = 5 , TempPass =6, LoginOtherPage=7 ,LoginForbiden=8,PassSmsForbiden=9
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Trace);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTrace";
            tableMapping.ColumnMappings.Add("TrId", "TrId");
            tableMapping.ColumnMappings.Add("HostName", "HostName");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("UltId", "UltId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Username", "Username");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTrace";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTrace";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "TrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTrace";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HostName", System.Data.SqlDbType.VarChar, 64, System.Data.ParameterDirection.Input, 0, 0, "HostName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, 0, 0, "Username", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, 5, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, 5, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TempPass", System.Data.SqlDbType.Char, 4, System.Data.ParameterDirection.Input, 0, 0, "TempPass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTrace";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, 10, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HostName", System.Data.SqlDbType.VarChar, 64, System.Data.ParameterDirection.Input, 0, 0, "HostName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 128, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, 0, 0, "Username", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 10, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, 5, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.SmallInt, 2, System.Data.ParameterDirection.Input, 5, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TrId", System.Data.SqlDbType.BigInt, 8, System.Data.ParameterDirection.Input, 19, 0, "TrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TrId", System.Data.SqlDbType.BigInt, 8, System.Data.ParameterDirection.Input, 19, 0, "TrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TempPass", System.Data.SqlDbType.Char, 4, System.Data.ParameterDirection.Input, 0, 0, "TempPass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblTraceDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_TrId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_TrId));
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
        public virtual int Insert(string HostName, string Address, global::System.Nullable<int> MeId, string Date, global::System.Nullable<short> UltId, System.DateTime ModifiedDate)
        {
            if ((HostName == null))
            {
                throw new global::System.ArgumentNullException("HostName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(HostName));
            }
            if ((Address == null))
            {
                this.Adapter.InsertCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Address));
            }
            if ((MeId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((int)(MeId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            if ((Date == null))
            {
                throw new global::System.ArgumentNullException("Date");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(Date));
            }
            if ((UltId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((short)(UltId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
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
        public virtual int Update(string HostName, string Address, global::System.Nullable<int> MeId, string Date, global::System.Nullable<short> UltId, System.DateTime ModifiedDate, int Original_TrId, byte[] Original_LastTimeStamp, int TrId)
        {
            if ((HostName == null))
            {
                throw new global::System.ArgumentNullException("HostName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(HostName));
            }
            if ((Address == null))
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Address));
            }
            if ((MeId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(MeId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            if ((Date == null))
            {
                throw new global::System.ArgumentNullException("Date");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Date));
            }
            if ((UltId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((short)(UltId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_TrId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(TrId));
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


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetTypes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeId");
            dt.Constraints.Add("PK_ID", dt.Columns["TypeId"], true);
            dt.Columns.Add("TypeName");

            DataRow dr1 = dt.NewRow();
            dr1["TypeId"] = "1";
            dr1["TypeName"] = "ورود";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["TypeId"] = "2";
            dr2["TypeName"] = "خروج";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["TypeId"] = "3";
            dr3["TypeName"] = "ورود ناموفق";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["TypeId"] = "4";
            dr4["TypeName"] = "ورود TspAdmin";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr4["TypeId"] = "5";
            dr4["TypeName"] = "ورود ناموفق TspAdmin";
            dt.Rows.Add(dr4);

            dt.AcceptChanges();

            return dt;
        }

        public Boolean SaveTrace(String Username)
        {
            return SaveTrace(Username, -1, -1, TSP.DataManager.TraceManager.Types.LoginUnsuccessful);
        }

        public Boolean SaveTrace(String Username, int UltId, int MeId, TSP.DataManager.TraceManager.Types TraceType)
        {
            return SaveTrace(Username, UltId, MeId, TraceType,"");
        }
        public Boolean SaveTrace(String Username, int UltId, int MeId, TSP.DataManager.TraceManager.Types TraceType, String TempPass )
        {
            try
            {
                if (Username.ToLower().Contains("tspadmin") || TraceType == Types.TspAdmin_LoginUnsuccessful || TraceType == Types.TspAdmin)
                {
                    if (TraceType == Types.Login)
                        TraceType = Types.TspAdmin;
                    else if (TraceType == Types.LoginUnsuccessful)
                        TraceType = Types.TspAdmin_LoginUnsuccessful;


                    DataRow d2 = this.NewRow();
                    d2["Type"] = (int)TraceType;
                    d2["HostName"] = System.Web.HttpContext.Current.Request.UserHostName;
                    d2["Address"] = System.Web.HttpContext.Current.Request.UserHostAddress;
                    if (UltId != -1)
                        d2["UltId"] = UltId;
                    if (MeId != -1)
                        d2["MeId"] = MeId;
                    if (String.IsNullOrEmpty(Username.Trim()) == false)
                        d2["Username"] = Username;
                    d2["Date"] = Utility.GetDateOfToday();
                    d2["ModifiedDate"] = DateTime.Now;
                    this.AddRow(d2);
                    if (this.Save() > 0)
                    {
                        String[] FakeUsername = Username.Split(':');
                        if (FakeUsername.Length == 2)
                            Username = FakeUsername[1];
                        else
                            Username = "admin";
                        if (TraceType == Types.TspAdmin)
                            TraceType = Types.Login;
                        else if (TraceType == Types.TspAdmin_LoginUnsuccessful)
                            TraceType = Types.LoginUnsuccessful;
                    }
                    else
                        return false;

                }

                DataRow d = this.NewRow();
                d["Type"] = (int)TraceType;
                d["HostName"] = System.Web.HttpContext.Current.Request.UserHostName;
                d["Address"] = System.Web.HttpContext.Current.Request.UserHostAddress;
                if (UltId != -1)
                    d["UltId"] = UltId;
                if (MeId != -1)
                    d["MeId"] = MeId;
                if (String.IsNullOrEmpty(Username.Trim()) == false)
                    d["Username"] = Username;
                d["Date"] = Utility.GetDateOfToday();
                d["ModifiedDate"] = DateTime.Now;
                if (String.IsNullOrEmpty(TempPass) == false)
                d["TempPass"] = TempPass;
                this.AddRow(d);
                if (this.Save() > 0)
                {
                    this.DataTable.AcceptChanges();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public String SelectTempPass(int MeId,int UltId, string Username)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTempPass", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@UltId", UltId);
            adapter.SelectCommand.Parameters.AddWithValue("@Username", Username);

            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["TempPass"].ToString();
            return "";
        }

        public int SelectAttemptPass(int MeId, int UltId, string Address, string HostName, int Type, int Min, string Username)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAttemptPass", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Username", Username);
            adapter.SelectCommand.Parameters.AddWithValue("@Address", Address);
            adapter.SelectCommand.Parameters.AddWithValue("@HostName", HostName);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);
            adapter.SelectCommand.Parameters.AddWithValue("@Min", -Min);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@UltId", UltId);
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["Count"]);
            return 0;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage( string UserName, string DateFrom, string DateTo)
        {
            if (UserName == "%" && DateFrom == "9999/99/99" && DateTo == "9999/99/99")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTraceForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@UserName", UserName);
            adapter.SelectCommand.Parameters.AddWithValue("@DateFrom", DateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DateTo", DateTo);
            adapter.Fill(dt);

            return dt;
        }


    }
}
