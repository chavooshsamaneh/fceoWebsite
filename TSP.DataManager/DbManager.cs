using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class DBManager
    {
        private static string _cnnStr = null;
        private static int commandTimeOut = 3600;
        private static int primaeyCommandTimeOut = 120;
        private static string primaryCnn = "Connect Timeout=3600;Pooling=true; Data Source =192.168.0.214; Initial Catalog =NezamFars; User Id =WebsiteUser; Password =snmfwebsite;Connect Timeout=120";

        public static string CnnStr
        {
            get
            {
                if (_cnnStr == null)
                {
                    try
                    {
                        ///****سرور سازمان
                        //_cnnStr = "Connect Timeout=3600;Pooling=true; Data Source =192.168.0.214; Initial Catalog =NezamFars; User Id =WebsiteUser; Password =snmfwebsite;Connect Timeout=120";
                        _cnnStr = "Connect Timeout=6000;Pooling=true; Data Source =193.19.145.173; Initial Catalog =NezamFars; User Id =tspadmin; Password =1234567890";                       
                    }
                    catch
                    {

                        ///****سرور سازمان
                        //_cnnStr = "Connect Timeout=3600;Pooling=true; Data Source =192.168.0.214; Initial Catalog =NezamFars; User Id =WebsiteUser; Password =snmfwebsite;Connect Timeout=120";

                    }
                }
                return _cnnStr;
            }
        }
        public static int DbConnectTimeout
        {
            get
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DbConnectTimeout"]);
                }
                catch (Exception) { }
                return 30;
            }
        }

        #region Methods
        public static bool ExistDb(string dbName)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(primaryCnn);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "use master;Select count(*) from sysdatabases where Name='" + dbName + "'";
            int cnt = (int)command.ExecuteScalar();
            if (cnt == 0)
                return false;
            return true;
        }
        public static bool AttachDb(string dbName, string[] fileNames)
        {
            if (fileNames == null)
                throw new ArgumentNullException();
            if (ExistDb(dbName))
                return false;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(primaryCnn);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            command.CommandTimeout = primaeyCommandTimeOut;
            command.Connection = connection;
            connection.Open();
            command.Parameters.Clear();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@dbName", dbName);

            for (int i = 1; i <= fileNames.Length; i++)
            {
                command.Parameters.AddWithValue("@filename" + i.ToString(), fileNames[i - 1]);
            }
            command.CommandText = "sp_attach_db ";
            command.ExecuteNonQuery();
            return true;
        }
        public void Attach(string[] filenames, string dbName)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.CommandTimeout = primaeyCommandTimeOut;
                command.Connection = connection;
                if (filenames.Length > 16)
                    throw new Exception("تعداد فايلها بيشر از 16 مي باشد");
#if !NETWORK_VERSION
                command.CommandText = "if exists (select * from sysdatabases where name =N'SaamicAcc')  EXEC sp_detach_db 'SaamicAcc'";
#else
                command.CommandText = "if exists (select * from sysdatabases where name =N'SaamicAccNet')  EXEC sp_detach_db 'SaamicAccNet'";
#endif
                connection.Open();
                command.ExecuteNonQuery();



                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dbName", dbName);
                for (byte i = 0; i < filenames.Length; i++)
                {
                    command.Parameters.AddWithValue("@filename" + (i + 1).ToString(), filenames[i]);
                }
                //command.Parameters.AddWithValue("@filename2", curDir + "saamicacc_log.ldf");
                command.CommandText = "use master;sp_attach_db ";
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public void Backup(string fileName, string dbName, string password)
        {

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);

            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();

                command.CommandTimeout = primaeyCommandTimeOut;
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();

                string Desc = "1.0.0.0";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@DbName", dbName);
                command.Parameters.AddWithValue("@backupName", dbName + "Psd_backup");
                command.Parameters.AddWithValue("@physName", fileName);
                command.Parameters.AddWithValue("@Description", Desc);
                command.Parameters.AddWithValue("@Password", password);
                command.CommandText = "use master; Backup Database SaamicAcc to disk=@physName  with Description=@Description,Password=@Password,Name=@backupName,Init ";//,Format,Init";


            }
            finally
            {
                connection.Close();
                connection = null;
            }
        }

        private void KillCurrentUser()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(primaryCnn);
            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand();
            sc.CommandTimeout = primaeyCommandTimeOut;
            sc.CommandText = "exec sp_who '" + Environment.MachineName + "\\" + Environment.UserName + "'";
            //  sc.Parameters.Add("@login_name", System.Data.SqlDbType.NVarChar, 64);
            // sc.Parameters[0].Value = Environment.MachineName + "\\" + Environment.UserName;
            // sc.CommandType = System.Data.CommandType.StoredProcedure;
            sda.SelectCommand = sc;
            sc.Connection = connection;
            sda.Fill(dt);
            string dbname = "SaamicAcc";
#if NETWORK_VERSION
            dbname = "SaamicAccNet";
#endif
            System.Data.DataRow[] rows = dt.Select("dbname='" + dbname + "'");

            sc.CommandType = System.Data.CommandType.Text;
            sc.Parameters.Clear();
            for (int i = 0; i < rows.Length; i++)
            {
                sc.CommandText = "kill " + rows[i]["spid"].ToString();
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException err)
                {
                    if (err.Number != 6101)
                        throw err;
                }
            }

            connection.Close();
        }
        public void Restore(string fileName, string dbName, string password)
        {

            System.Data.SqlClient.SqlConnection.ClearAllPools();
            KillCurrentUser();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = primaeyCommandTimeOut;
                string suffix = System.IO.Path.GetExtension(fileName);
                if (suffix.ToLower() == ".bdf")
                {

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@DbName", dbName);
                    command.Parameters.AddWithValue("@physName", fileName);
                    command.Parameters.AddWithValue("@Password", password);// cryp.Decrypt(System.Reflection.Assembly.GetExecutingAssembly().FullName.Substring(0, 6)));
                    command.CommandText = "use master; Restore Database @DbName from disk=@physName with Password=@Password,Replace";
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                else
                    throw new Exception("Invalid File Name");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static bool ExistTb(string TbName)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "Select count(*) from [sysobjects] where Name='" + TbName + "' And Type='U'";
            int cnt = (int)command.ExecuteScalar();
            connection.Close();
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool CreatTb(string NewTbName, string TbName, TransactionManager transact)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();

            try
            {
                command.Connection = connection;
                if (transact != null)
                {
                    command.Connection = transact.Transaction.Connection;
                    command.Transaction = transact.Transaction;

                    //command.Connection.Open();
                }
                if (transact == null)
                    connection.Open();
                command.CommandText = "Select  *  into " + NewTbName + " from " + TbName;
                command.ExecuteNonQuery();
                if (transact == null)
                    connection.Close();
                //   else
                // command.Connection.Close();
                return true;
            }
            catch (Exception err)
            {
                // if (transact != null)
                //   command.Connection.Close();
                //else
                connection.Close();
                throw err;
                return false;
            }

            //System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            //command.CommandText = "Select  *  into " + NewTbName + " from " + TbName;
            //return Transaction(command);
        }

        public static bool Transaction(SqlCommand Command)
        {
            Boolean state = true;
            try
            {
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
                SqlTransaction Transact;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                Transact = connection.BeginTransaction();
                Command.Transaction = Transact;
                Command.Connection = connection;
                try
                {
                    Command.ExecuteNonQuery();
                    Transact.Commit();
                }
                catch (Exception)
                {
                    state = false;
                    //try
                    //{
                    Transact.Rollback();
                    //}
                    //catch
                    //{
                    //}
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch
            {
                state = false;
            }
            return state;
        }


        public static bool ClearAccTables(TransactionManager transact)
        {
            //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CnnStr);
            System.Data.SqlClient.SqlConnection connection = transact.Transaction.Connection;
            System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand();

            try
            {
                sc.Connection = connection;
                if (transact != null)
                {
                    sc.Connection = transact.Transaction.Connection;
                    sc.Transaction = transact.Transaction;

                    //command.Connection.Open();
                }
                if (transact == null)
                    connection.Open();

                //SqlCommand sc = new SqlCommand();
                //try
                //{
                //sc.Connection = new System.Data.SqlClient.SqlConnection(CnnStr);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandText = "[spClearAccTables]";
                if (sc.Connection.State != ConnectionState.Open)
                    sc.Connection.Open();
                sc.ExecuteNonQuery();
                if (transact == null)
                    sc.Connection.Close();

                return true;
            }
            catch (Exception err)
            {
                sc.Connection.Close();
                throw err;
                return false;
            }
        }
        #endregion
    }
}

