using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public enum NewsSubjectId
    {
        Magazin=24,
        WelfareScheduleList=45
    }
    public class NewsManager : BaseObject
    {        
        public NewsManager()
            : base()
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.NewsArchive);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "News";
            tableMapping.ColumnMappings.Add("NewsId", "NewsId");
            tableMapping.ColumnMappings.Add("SubjectId", "SubjectId");
            tableMapping.ColumnMappings.Add("Title", "Title");
            tableMapping.ColumnMappings.Add("Body", "Body");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("TimeHour", "TimeHour");
            tableMapping.ColumnMappings.Add("CountOfRead", "CountOfRead");
            tableMapping.ColumnMappings.Add("Importance", "Importance");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Summary", "Summary");
            tableMapping.ColumnMappings.Add("AttachmentUrl", "AttachmentUrl");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("UserLoginType", "UserLoginType");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("ExGroupId", "ExGroupId");
            tableMapping.ColumnMappings.Add("IsNotification", "IsNotification");            
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectNews";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@NewsId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@SubjectId", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 80);
            this.Adapter.SelectCommand.Parameters.Add("@Body", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@Importance", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteNews";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NewsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "NewsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertNews";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubjectId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SubjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 80, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 1073741823, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Summary", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Summary", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserLoginType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserLoginType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TimeHour", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "TimeHour", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountOfRead", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CountOfRead", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Importance", global::System.Data.SqlDbType.TinyInt, 1, global::System.Data.ParameterDirection.Input, 3, 0, "Importance", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AttachmentUrl", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExGroupId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ExGroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNotification", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsNotification", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateNews";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubjectId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SubjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 80, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 1073741823, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Summary", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Summary", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TimeHour", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "TimeHour", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserLoginType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserLoginType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AttachmentUrl", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountOfRead", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CountOfRead", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Importance", global::System.Data.SqlDbType.TinyInt, 1, global::System.Data.ParameterDirection.Input, 3, 0, "Importance", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NewsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "NewsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NewsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "NewsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExGroupId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ExGroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNotification", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsNotification", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.WebSiteHomePageDataSet.NewsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int NewsId)
        {
            this.Adapter.SelectCommand.Parameters["@NewsId"].Value = NewsId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentCode(int AgentId)
        {
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this._dataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForManagmentPage(int AgentId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNewsForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchNews(short SubjectId, string Title, string Body, string FromDate, string ToDate, short Importance, int AgentId, int ExGroupId, int IsNotification, int? InActive = 0)
        {

            if (string.IsNullOrEmpty(SubjectId.ToString()))
                SubjectId = -1;
            if (string.IsNullOrEmpty(Title))
                Title = "%";
            if (string.IsNullOrEmpty(Body))
                Body = "%";
            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";
            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";
            if (string.IsNullOrEmpty(Importance.ToString()))
                Importance = -1;
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            if (string.IsNullOrEmpty(ExGroupId.ToString()))
                ExGroupId = -1;
            if (string.IsNullOrEmpty(IsNotification.ToString()))
                IsNotification = -1;            
            DataTable dt = new System.Data.DataTable();
            //if (SubjectId == -1 && Title == "%" && Body == "%" && FromDate == "1" && ToDate == "2" && Importance == -1 && AgentId == -1 && ExGroupId == -1 && IsNotification==-1)
            //    return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("selectSearchNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@SubjectId", SubjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@Title", Title);
            adapter.SelectCommand.Parameters.AddWithValue("@Body", Body);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Importance", Importance);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@ExGroupId", ExGroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsNotification", IsNotification);    
            adapter.Fill(dt);
            // Fill();
            return dt;
        }
         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchNews(short SubjectId, string Title, string Body, string FromDate, string ToDate, short Importance, int AgentId, int? InActive = 0)
        {
            if (string.IsNullOrEmpty(SubjectId.ToString()))
                SubjectId = -1;
            if (string.IsNullOrEmpty(Title))
                Title = "%";
            if (string.IsNullOrEmpty(Body))
                Body = "%";
            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";
            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";
            if (string.IsNullOrEmpty(Importance.ToString()))
                Importance = -1;
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            DataTable dt = new System.Data.DataTable();
            if (SubjectId == -1 && Title == "%" && Body == "%" && FromDate == "1" && ToDate == "2" && Importance == -1 && AgentId == -1)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("selectSearchNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@SubjectId", SubjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@Title", Title);
            adapter.SelectCommand.Parameters.AddWithValue("@Body", Body);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Importance", Importance);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            //adapter.SelectCommand.Parameters.AddWithValue("@ExGroupId", ExGroupId);
            adapter.Fill(dt);
            // Fill();
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
         public DataTable selectTenNews(short SubjectId, string Title, string Body, string FromDate, string ToDate, short Importance, int IsNotification)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTenNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SubjectId", System.Data.SqlDbType.SmallInt, 2);
            adapter.SelectCommand.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 80);
            adapter.SelectCommand.Parameters.Add("@Body", System.Data.SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@Importance", System.Data.SqlDbType.SmallInt, 2);
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentDate", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.AddWithValue("@IsNotification", IsNotification);

            if (string.IsNullOrEmpty(SubjectId.ToString()))
                SubjectId = -1;
            adapter.SelectCommand.Parameters["@SubjectId"].Value = SubjectId;
            if (string.IsNullOrEmpty(Title))
                Title = "%";
            adapter.SelectCommand.Parameters["@Title"].Value = Title;
            if (string.IsNullOrEmpty(Body))
                Body = "%";
            adapter.SelectCommand.Parameters["@Body"].Value = Body;
            if (string.IsNullOrEmpty(FromDate))
                FromDate = "%";
            adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            if (string.IsNullOrEmpty(ToDate))
                ToDate = "%";
            adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            if (string.IsNullOrEmpty(Importance.ToString()))
                Importance = -1;
            adapter.SelectCommand.Parameters["@Importance"].Value = Importance;
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImportantNews()
        {
            DataTable dt = new System.Data.DataTable();// WebSiteHomePageDataSet.NewsDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectImportantNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentDate", Utility.GetDateOfToday());
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImportantTopNews(int IsNotification)
        {
            DataTable dt = new System.Data.DataTable();// WebSiteHomePageDataSet.NewsDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectImportantTopNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentDate", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.AddWithValue("@IsNotification", IsNotification);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectLatestNews(short Importance, int IsNotification)
        {
            return selectTenNews(-1, "%", "%", "1", "2", Importance, IsNotification);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNotificationNews()
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNotificationNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentDate", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.AddWithValue("@IsNotification", 1);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectBoardDirectorsNews()
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectBoardDirectorsNews", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentDate", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.AddWithValue("@ExGroupId", ((int)TSP.DataManager.ExGroupManager.Type.BoardDirectors));
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectYear()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("year");

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNewsDate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNewsForMagazin()
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNewsForMagazin", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(dt);
            return dt;

        }

    }
}
