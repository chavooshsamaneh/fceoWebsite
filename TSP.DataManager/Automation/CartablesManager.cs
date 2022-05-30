using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.Automation
{
    public class CartablesManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AutomationCartables);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Cartables";
            tableMapping.ColumnMappings.Add("CartableId", "CartableId");
            tableMapping.ColumnMappings.Add("CartableUserId", "CartableUserId");
            tableMapping.ColumnMappings.Add("CartableSecretariatId", "CartableSecretariatId");
            tableMapping.ColumnMappings.Add("LetterId", "LetterId");
            tableMapping.ColumnMappings.Add("CartableLetterType", "CartableLetterType");
            tableMapping.ColumnMappings.Add("CartableGroup", "CartableGroup");
            tableMapping.ColumnMappings.Add("ViewState", "ViewState");
            tableMapping.ColumnMappings.Add("ViewDate", "ViewDate");
            tableMapping.ColumnMappings.Add("ViewTime", "ViewTime");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("LetterInReference", "LetterInReference");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectAutomationCartables";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CartableId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@CartableUserId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@CartableGroup", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@LetterId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@CartableSecretariatId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteAutomationCartables";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CartableId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertAutomationCartables";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableUserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableUserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableSecretariatId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableSecretariatId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterInReference", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterInReference", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableLetterType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableLetterType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableGroup", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewState", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "ViewState", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ViewDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewTime", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ViewTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateAutomationCartables";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableUserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableUserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableSecretariatId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableSecretariatId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterInReference", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterInReference", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableLetterType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableLetterType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableGroup", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewState", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "ViewState", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ViewDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ViewTime", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ViewTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CartableId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CartableId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CartableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.Automation.AutomationDataSet.CartablesDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {            
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@CartableId"].Value = Id;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByCartableUserId(int UserId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableUserId"].Value = UserId;
            Fill();
            return this.DataTable;
        }

        public void FindByCartableUserIdAndLetter(int UserId, int LetterId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableUserId"].Value = UserId;
            this.Adapter.SelectCommand.Parameters["@LetterId"].Value = LetterId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByCartableUserIdAndGroup(int UserId, int GroupId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableUserId"].Value = UserId;
            this.Adapter.SelectCommand.Parameters["@CartableGroup"].Value = GroupId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByCartableSecretariatIdAndGroup(int SId, int GroupId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableSecretariatId"].Value = SId;
            this.Adapter.SelectCommand.Parameters["@CartableGroup"].Value = GroupId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSecretariatCartables(int SId, int GroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectSecretariatCartables", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@CartableGroup", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableSecretariatId", SId);

            adapter.Fill(dt);
            return (dt);
        }

        public void FindByCartableSecretariatAndLetter(int SId, int LetterId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableSecretariatId"].Value = SId;
            this.Adapter.SelectCommand.Parameters["@LetterId"].Value = LetterId;
            Fill();
        }

        public int SelectCountSecretariatAndLetter(int SId, int LetterId)
        {
            FindByCartableSecretariatAndLetter(SId, LetterId);
            return this.DataTable.Rows.Count;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchSecretariatCartables(int SId, int GroupId, string LetterSerialNumber, string LetterNumber, string FromLetterDate, string ToLetterDate,
            int Secretariat, int Type, int CreationType, int TitleType, int SendRecieveType, string IndexNo, string FromIndexDate, string ToIndexDate,
            int SenderType, int SenderMeId, string SenderName, string SenderLastName, int SenderOrgId, int RecieverType, int RecieverMeId,
            string RecieverName, string RecieverLastName, int RecieverOrgId, int RecieverNcId, int RefRecieverNcId, int RefRecieverEmpId,
            string RefRecieverName, string RefRecieverLastName, int RefSenderNcId, int RefSenderEmpId, string RefSenderName, string RefSenderLastName, string Title, int SenderNcId,
            int CreatorNcId, string CreatorFirstName, string CreatorLastName, int CreatorEmpId, string KeyWords, int LetterId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("AutomationSecretariatCartableSearch", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@TtId", (int)TableCodes.AutomationLetters);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterId", LetterId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableGroup", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableSecretariatId", SId);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterSerialNumber", LetterSerialNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterNumber", LetterNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@FromLetterDate", FromLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToLetterDate", ToLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Secretariat", Secretariat);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);
            adapter.SelectCommand.Parameters.AddWithValue("@CreationType", CreationType);
            adapter.SelectCommand.Parameters.AddWithValue("@TitleType", TitleType);
            adapter.SelectCommand.Parameters.AddWithValue("@SendRecieveType", SendRecieveType);
            adapter.SelectCommand.Parameters.AddWithValue("@IndexNo", IndexNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FromIndexDate", FromIndexDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToIndexDate", ToIndexDate);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderType", SenderType);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderMeId", SenderMeId);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderName", SenderName);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderLastName", SenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderOrgId", SenderOrgId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverType", RecieverType);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverMeId", RecieverMeId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverName", RecieverName);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverLastName", RecieverLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverOrgId", RecieverOrgId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverNcId", RecieverNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverNcId", RefRecieverNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverEmpId", RefSenderEmpId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverName", RefRecieverName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverLastName", RefSenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderNcId", RefSenderNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderEmpId", RefSenderEmpId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderName", RefSenderName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderLastName", RefSenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@Title", Title);
            adapter.SelectCommand.Parameters.AddWithValue("@KeyWords", KeyWords);            
            adapter.SelectCommand.Parameters.AddWithValue("@SenderNcId", SenderNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@CreatorNcId", CreatorNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@CreatorFirstName", CreatorFirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@CreatorLastName", CreatorLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@CreatorEmpId", CreatorEmpId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchCartables(int UserId, int GroupId, string LetterSerialNumber, string LetterNumber, string FromLetterDate, string ToLetterDate,
            int Secretariat, int Type, int CreationType, int TitleType, int SendRecieveType, string IndexNo, string FromIndexDate, string ToIndexDate,
            int SenderType, int SenderMeId, string SenderName, string SenderLastName, int SenderOrgId, int RecieverType, int RecieverMeId,
            string RecieverName, string RecieverLastName, int RecieverOrgId, int RecieverNcId, int RefRecieverNcId, int RefRecieverEmpId,
            string RefRecieverName, string RefRecieverLastName, int RefSenderNcId, int RefSenderEmpId, string RefSenderName, string RefSenderLastName, string Title,
            int PId, int CartableLetterTypes, int UsePassword, int LockState, int ViewState, int ActionRerResult, int SenderNcId, string KeyWords,int DivId,int LetterId
            , string FromNumLetterDate, string ToNumLetterDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAutomationCartablesSearch", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@LetterId", LetterId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableUserId", UserId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableGroup",GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@TtId", (int)TableCodes.AutomationLetters);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterSerialNumber", LetterSerialNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterNumber", LetterNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@FromLetterDate", FromLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToLetterDate", ToLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@FromNumLetterDate", FromNumLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToNumLetterDate", ToNumLetterDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Secretariat", Secretariat);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);
            adapter.SelectCommand.Parameters.AddWithValue("@CreationType", CreationType);
            adapter.SelectCommand.Parameters.AddWithValue("@TitleType", TitleType);
            adapter.SelectCommand.Parameters.AddWithValue("@SendRecieveType", SendRecieveType);
            adapter.SelectCommand.Parameters.AddWithValue("@IndexNo", IndexNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FromIndexDate", FromIndexDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToIndexDate", ToIndexDate);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderType", SenderType);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderMeId", SenderMeId);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderName", SenderName);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderLastName", SenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderOrgId", SenderOrgId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverType", RecieverType);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverMeId", RecieverMeId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverName", RecieverName);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverLastName", RecieverLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverOrgId", RecieverOrgId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverNcId", RecieverNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverNcId", RefRecieverNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverEmpId", RefSenderEmpId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverName", RefRecieverName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefRecieverLastName", RefSenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderNcId", RefSenderNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderEmpId", RefSenderEmpId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderName", RefSenderName);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSenderLastName", RefSenderLastName);
            adapter.SelectCommand.Parameters.AddWithValue("@Title", Title);
            adapter.SelectCommand.Parameters.AddWithValue("@PId", PId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableLetterTypes", CartableLetterTypes);
            adapter.SelectCommand.Parameters.AddWithValue("@UsePassword", UsePassword);
            adapter.SelectCommand.Parameters.AddWithValue("@LockState", LockState);
            adapter.SelectCommand.Parameters.AddWithValue("@ViewState", ViewState);
            adapter.SelectCommand.Parameters.AddWithValue("@ActionRerResult", ActionRerResult);
            adapter.SelectCommand.Parameters.AddWithValue("@SenderNcId", SenderNcId);
            adapter.SelectCommand.Parameters.AddWithValue("@KeyWords", KeyWords);
            adapter.SelectCommand.Parameters.AddWithValue("@DivId", DivId);

            adapter.Fill(dt);
            return (dt);
        }

    }
}
