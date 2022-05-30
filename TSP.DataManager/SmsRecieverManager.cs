using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class SmsRecieverManager : BaseObject
    {
        public enum RecieverTypes
        {
            Member = 1, OtherPerson = 2, Employee = 3, ManualInsert = 4, Teacher = 5, Institute = 6, Office = 7, EngOffcie = 8,TSPlanControler=9
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.SMSReciever);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblSmsReciever";
            tableMapping.ColumnMappings.Add("SmsReId", "SmsReId");
            tableMapping.ColumnMappings.Add("SmsId", "SmsId");
            tableMapping.ColumnMappings.Add("RecieverId", "RecieverId");
            tableMapping.ColumnMappings.Add("RecieverType", "RecieverType");
            tableMapping.ColumnMappings.Add("RecieverCellPhone", "RecieverCellPhone");
            tableMapping.ColumnMappings.Add("IsDelivered", "IsDelivered");
            tableMapping.ColumnMappings.Add("SMSDeliveryReId", "SMSDeliveryReId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("DeliverySMSID", "DeliverySMSID");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSmsReciever";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsReId", System.Data.SqlDbType.BigInt, 4, System.Data.ParameterDirection.Input, 0, 0, "SmsReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "RecieverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteSmsReciever";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SmsReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertSmsReciever";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverCellPhone", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverCellPhone", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelivered", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDelivered", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDeliveryReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDeliveryReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliverySMSID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliverySMSID", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateSmsReciever";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieverCellPhone", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieverCellPhone", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelivered", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDelivered", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDeliveryReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDeliveryReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SmsReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsReId", System.Data.SqlDbType.BigInt, 4, System.Data.ParameterDirection.Input, 0, 0, "SmsReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliverySMSID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliverySMSID", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.SMSDataSet.tblSmsRecieverDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void InsertSmsRecieverForMember(int SmsId, String RecieverIds, int RecieverType, int UserId)
        {
            SqlCommand Command = new SqlCommand("spInsertSmsRecieverForMember", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@SmsId", SmsId);
            Command.Parameters.AddWithValue("@RecieverIds", RecieverIds);
            Command.Parameters.AddWithValue("@RecieverType", RecieverType);
            Command.Parameters.AddWithValue("@UserId", UserId);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public void InsertSmsRecieverForMember(int SmsId, String RecieverIds, int RecieverType)
        {
            InsertSmsRecieverForMember(SmsId, RecieverIds, RecieverType, 1);
        }

        public void UpdateSmsRecieverForMember(int SmsId, String RecieverIds, int RecieverType, int UserId)
        {
            //System.Data.SqlClient.SqlDataAdapter Adp = new SqlDataAdapter();
            //Adp.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            //Adp.UpdateCommand.Transaction = this.Transaction;
            //Adp.UpdateCommand.Connection = this.Connection;
            //Adp.UpdateCommand.CommandText = "spUpdateSmsRecieverForMember";
            //Adp.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //Adp.UpdateCommand.Parameters.AddWithValue("@SmsId", SmsId);
            //Adp.UpdateCommand.Parameters.AddWithValue("@RecieverIds", RecieverIds);
            //Adp.UpdateCommand.Parameters.AddWithValue("@RecieverType", RecieverType);
            //Adp.UpdateCommand.Parameters.AddWithValue("@UserId", UserId);
            //Adp.Update(this.DataTable);
            //DataTable.AcceptChanges();

            SqlCommand Command = new SqlCommand("spUpdateSmsRecieverForMember", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@SmsId", SmsId);
            Command.Parameters.AddWithValue("@RecieverIds", RecieverIds);
            Command.Parameters.AddWithValue("@RecieverType", RecieverType);
            Command.Parameters.AddWithValue("@UserId", UserId);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public void UpdateSmsRecieverForMember(int SmsId, String RecieverIds, int RecieverType)
        {
            UpdateSmsRecieverForMember(SmsId, RecieverIds, RecieverType, 1);
        }

        public void UpdateSMSRecieverDelivery(String SmsReIds, String DeliverySmsIds)
        {
            SqlCommand Command = new SqlCommand("spUpdateSMSRecieverDelivery", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@SmsReIds", SmsReIds);
            Command.Parameters.AddWithValue("@DeliverySmsIds", DeliverySmsIds);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public void UpdateSMSRecieverDelivery(String SmsReIds, String DeliverySmsIds, int SmsId, int ErrorSendingSms)
        {
            SqlCommand Command = new SqlCommand("spUpdateSMSRecieverDelivery", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@SmsReIds", SmsReIds);
            Command.Parameters.AddWithValue("@DeliverySmsIds", DeliverySmsIds);
            Command.Parameters.AddWithValue("@SmsId", SmsId);
            Command.Parameters.AddWithValue("@ErrorSendingSms", ErrorSendingSms);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public void FindByCode(int RecieverId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@SmsReId"].Value = RecieverId;
            Fill();
        }

        public void FindByCode(long SmsReId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@SmsReId"].Value = SmsReId;
            Fill();
        }

        public void FindRecieverBySMSId(int SmsId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@SmsId"].Value = SmsId;

            Fill();
        }

        public void FindByRecieverId(int SmsId, int RecieverId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@RecieverId"].Value = RecieverId;
            this.Adapter.SelectCommand.Parameters["@SmsId"].Value = SmsId;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySMSId(int SMSId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverBySMSId", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            adapter.SelectCommand.Transaction = this.Transaction;
            //  adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 4, "ReceiverId").Value = SMSId;

            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySMSId(int SMSId, Boolean IsDelivered)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverBySMSId", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            int Delivered = Convert.ToInt32(IsDelivered);
            adapter.SelectCommand.Parameters.AddWithValue("@IsDelivered", Delivered);

            adapter.SelectCommand.Transaction = this.Transaction;
            //  adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 4, "ReceiverId").Value = SMSId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySMSId(int SMSId, Boolean IsDelivered, Boolean ReturnEmptyNumber)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverBySMSId", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            int Delivered = Convert.ToInt32(IsDelivered);
            adapter.SelectCommand.Parameters.AddWithValue("@IsDelivered", Delivered);
            int RetEmptyNumber = Convert.ToInt32(ReturnEmptyNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@ReturnEmptyNumber", RetEmptyNumber);
            adapter.SelectCommand.Transaction = this.Transaction;
            //  adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 4, "ReceiverId").Value = SMSId;

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// ReturnUnknownDeliverySMSID و ReturnValidDeliverySMSID نباید همزمان یک باشند
        /// </summary>
        /// <param name="SMSId"></param>
        /// <param name="ReturnEmptyNumber">اگر 1 باشد شماره موبایل های خالی برگردانده نمی شوند</param>
        /// <param name="ReturnUnknownDeliverySMSID">برای برگرداندن شناسه هایی که خالی هستند یا زیر 1000 هستند 1 می شود</param>
        /// <param name="ReturnValidDeliverySMSID">برای برگرداندن شناسه هایی که خالی نباشند و بزرگتر از 1000 باشند 1 می شود</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetSMSRecieverByDeliverySMSId(int SMSId, int ReturnEmptyNumber, int ReturnUnknownDeliverySMSID, int ReturnValidDeliverySMSID)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverWithUnknownDeliverySMSID", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            adapter.SelectCommand.Parameters.AddWithValue("@IsDelivered", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@ReturnEmptyNumber", ReturnEmptyNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@ReturnUnknownDeliverySMSID", ReturnUnknownDeliverySMSID);
            adapter.SelectCommand.Parameters.AddWithValue("@ReturnValidDeliverySMSID", ReturnValidDeliverySMSID);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySMSId(int SMSId, int recievertype)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverBySMSId", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            adapter.SelectCommand.Parameters.Add("@recievertype", SqlDbType.Int, 4, "recievertype").Value = recievertype;

            adapter.SelectCommand.Transaction = this.Transaction;
            //  adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 4, "ReceiverId").Value = SMSId;

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable FindBySMSId_StringMode(int SMSId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsRecieverBySMSId_StringMode", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable FindTotalRecieversBySMSId_StringMode(int SMSId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsTotalRecievers", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        public void SelectSmsByDate(string StartDate, string EndDate)
        {
            // DataTable dt = new DataTable();         
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSmsReport", this.Connection);
            //adapter.SelectCommand.CommandText = "spSelectSmsReport";
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@StartDate", StartDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(DataTable);
            // return (dt);
        }

    }
}
