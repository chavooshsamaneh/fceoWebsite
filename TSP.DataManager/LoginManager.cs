using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class LoginManager : BaseObject
    {
        //static LoginManager()
        //{
        //    _tableId = TableType.Login;
        //}
        //public LoginManager(System.Data.DataSet ds)
        //    : base(ds)
        //{
        //}
        //public static Permission GetUserPermission(int UserId, UserType ut)
        //{
        //    return BaseObject.GetUserPermission(UserId, ut, TableType.Login);
        //}
        public LoginManager()
            : base()
        {
        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblLoginDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblLogin";
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("UserName", "UserName");
            tableMapping.ColumnMappings.Add("Password", "Password");
            tableMapping.ColumnMappings.Add("IsSignIn", "IsSignIn");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("UltId", "UltId");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("IsValid", "IsValid");
            tableMapping.ColumnMappings.Add("UserId2", "UserId2");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("NeedTempPass", "NeedTempPass");
            tableMapping.ColumnMappings.Add("IsAdmin", "IsAdmin");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectLogin";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UserName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Password", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Password", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add("@UserId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@UltId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@Status", System.Data.SqlDbType.TinyInt);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteLogin";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertLogin";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UserName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Password", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Password", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSignIn", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSignIn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsValid", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsValid", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NeedTempPass", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "NeedTempPass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateLogin";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UserName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Password", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Password", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSignIn", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSignIn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsValid", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsValid", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NeedTempPass", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "NeedTempPass", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public void FindUserName(string
            userName, string password)
        {
            this.Adapter.SelectCommand.Parameters[0].Value = userName;
            this.Adapter.SelectCommand.Parameters[1].Value = password;
            Fill();
        }

        public void FindUserNameById(int UserId)
        {
            this.Adapter.SelectCommand.Parameters[2].Value = UserId;

            Fill();
        }
        public void FindUserName(string
           userName)
        {
            this.Adapter.SelectCommand.Parameters[0].Value = userName;

            Fill();
        }



        public void FindByCode(int Id)
        {
            this.Adapter.SelectCommand.Parameters["@UserId"].Value = Id;
            Fill();
        }

        public void FindByMeId(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            Fill();
        }

        public void FindByMeIdUltId(int MeId, int UltId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            Fill();
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]

        public virtual int Delete(int Original_UserId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_UserId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]

        public virtual int Insert(string UserName, string Password, System.Nullable<int> MeId, short UltId, string Email, byte IsValid, System.Nullable<int> UserId2, System.DateTime ModifiedDate)
        {
            if ((UserName == null))
            {
                throw new System.ArgumentNullException("UserName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(UserName));
            }
            if ((Password == null))
            {
                throw new System.ArgumentNullException("Password");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Password));
            }
            if ((MeId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((int)(MeId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = System.DBNull.Value;
            }
            this.Adapter.InsertCommand.Parameters[4].Value = ((short)(UltId));
            if ((Email == null))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(Email));
            }
            this.Adapter.InsertCommand.Parameters[6].Value = ((byte)(IsValid));
            if ((UserId2.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[7].Value = ((int)(UserId2.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[7].Value = System.DBNull.Value;
            }
            this.Adapter.InsertCommand.Parameters[8].Value = ((System.DateTime)(ModifiedDate));
            System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]

        public virtual int Update(string UserName, string Password, System.Nullable<int> MeId, short UltId, string Email, byte IsValid, System.Nullable<int> UserId2, System.DateTime ModifiedDate, int Original_UserId, byte[] Original_LastTimeStamp, int UserId)
        {
            if ((UserName == null))
            {
                throw new System.ArgumentNullException("UserName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(UserName));
            }
            if ((Password == null))
            {
                throw new System.ArgumentNullException("Password");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Password));
            }
            if ((MeId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(MeId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[4].Value = ((short)(UltId));
            if ((Email == null))
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(Email));
            }
            this.Adapter.UpdateCommand.Parameters[6].Value = ((byte)(IsValid));
            if ((UserId2.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(UserId2.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[8].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(Original_UserId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[11].Value = ((int)(UserId));
            System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchLogin(string UserName, string Email, byte Status)
        {
            if (string.IsNullOrEmpty(UserName))
                UserName = "%";
            this.Adapter.SelectCommand.Parameters["@UserName"].Value = UserName;
            if (string.IsNullOrEmpty(Email))
                Email = "%";
            this.Adapter.SelectCommand.Parameters["@Email"].Value = Email;
            if (string.IsNullOrEmpty(Status.ToString()))
                Status = 2;
            this.Adapter.SelectCommand.Parameters["@Status"].Value = Status;
            Fill();
            return DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLoginByUltId(int UltId)
        {
            if (string.IsNullOrEmpty(UltId.ToString()))
                UltId = -1;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;

            Fill();
            return DataTable;
        }

        public DataTable LoginUser(String Username, String Password)
        {
            SqlCommand objCommand = new SqlCommand("spSelectLoginUser", this.Connection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@Username", Username);
            objCommand.Parameters.AddWithValue("@Password", Password);
            DataTable dt = new DataTable();
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(dt);
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public DataTable FindByUsername(String Username)
        {
            SqlCommand objCommand = new SqlCommand("spSelectLoginByUsername", this.Connection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@Username", Username);
            DataTable dt = new DataTable();
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(dt);
            }
            catch (Exception)
            {
            }
            return dt;
        }
    }
}
