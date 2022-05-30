using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class PollAnswerManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PollAnswer);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Poll.Answer";
            tableMapping.ColumnMappings.Add("AnswerId", "AnswerId");
            tableMapping.ColumnMappings.Add("ChoiseId", "ChoiseId");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("TableType", "TableType");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("AnswerTime", "AnswerTime");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Description", "Description");
            Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectPollAnswer";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@UltId", SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@PollId", SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@QuestionId", SqlDbType.Int);

            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeletePollAnswer";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AnswerId", global::System.Data.SqlDbType.BigInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertPollAnswer";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ChoiseId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ChoiseId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", "")); Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerTime", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));


            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdatePollAnswer";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ChoiseId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ChoiseId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerTime", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AnswerId", global::System.Data.SqlDbType.BigInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerId", global::System.Data.SqlDbType.BigInt, 8, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.PollDataSet.PollAnswerDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable FindByMeId(int MeId, int UltId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            Fill();
            return this.DataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchPollAnswer(int MeId = -1, int UltId = -1, int UserId = -1, int PollId = -1)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            this.Adapter.SelectCommand.Parameters["@UserId"].Value = UserId;
            this.Adapter.SelectCommand.Parameters["@PollId"].Value = PollId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPollAnswerByQuestionId(int QuestionId = -1)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@QuestionId"].Value = QuestionId;
            Fill();
            return this.DataTable;
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAnswerReport(int PollId = -1, int QuestionId=-1)
        {
            DataTable dtReport = new System.Data.DataTable();
            dtReport.Columns.Add("PollId");
            dtReport.Columns.Add("ChoiseId");
            dtReport.Columns.Add("Question");
            dtReport.Columns.Add("ChoiseName");
            dtReport.Columns.Add("CountAnswer");
            dtReport.Columns.Add("TotalAnswer");

            SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("spSelectPollAnswerReport", this.Connection);
            SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PollId", PollId);
            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@QuestionId", QuestionId);
            SqlDataAdapter.Fill(dtReport);
            return dtReport;
        }

        public bool Insert(int ChoiseId, int UserId)
        {
            DataRow row = this.DataTable.NewRow();
            row["ChoiseId"] = ChoiseId;
            row["AnswerDate"] = Utility.GetDateOfToday();
            row["AnswerTime"] = Utility.GetCurrentTime();
            row["UserId"] = UserId;
            row["ModifiedDate"] = DateTime.Now;
            this.AddRow(row);
            if (this.Save() > 0)
                return true;
            else return false;
        }

        public int SelectCountOfAnswerPollForPeriodPresent(int TableType, int TableId, int UserId)
        {
            DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("count");
            SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("SelectCountOfAnswerPollForPeriodPresent", this.Connection);
            SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TableId", TableId);
            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            SqlDataAdapter.Fill(dt);
            return Convert.ToInt32(dt.Rows[0]["Count"]);
            
        }

    }
}
