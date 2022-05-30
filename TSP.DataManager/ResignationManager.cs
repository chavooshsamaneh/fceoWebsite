using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class ResignationManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Resignation);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblResignation";
            tableMapping.ColumnMappings.Add("ResignId", "ResignId");
            tableMapping.ColumnMappings.Add("TableType", "TableType");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("AssignerId", "AssignerId");
            tableMapping.ColumnMappings.Add("ReceptiveId", "ReceptiveId");
            tableMapping.ColumnMappings.Add("ResignStartDate", "ResignStartDate");
            tableMapping.ColumnMappings.Add("ResignEndDate", "ResignEndDate");
            tableMapping.ColumnMappings.Add("ResignStartTime", "ResignStartTime");
            tableMapping.ColumnMappings.Add("ResignEndTime", "ResignEndTime");
            tableMapping.ColumnMappings.Add("IsLimited", "IsLimited");
            tableMapping.ColumnMappings.Add("IsAccepted", "IsAccepted");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("AccessStatus", "AccessStatus");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("AcceptionStatus", "AcceptionStatus");
            tableMapping.ColumnMappings.Add("AssignerFirstName", "AssignerFirstName");
            tableMapping.ColumnMappings.Add("AssignerLastName", "AssignerLastName");
            tableMapping.ColumnMappings.Add("ReceptiveFirstName", "ReceptiveFirstName");
            tableMapping.ColumnMappings.Add("ReceptiveLastName", "ReceptiveLastName");
            tableMapping.ColumnMappings.Add("TtName", "TtName");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectResignation";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteResignation";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertResignation";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssignerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AssignerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceptiveId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceptiveId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignStartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignStartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignEndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignEndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignStartTime", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignStartTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignEndTime", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignEndTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsAccepted", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsAccepted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccessStatus", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccessStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateResignation";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssignerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AssignerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceptiveId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceptiveId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignStartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignStartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignEndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignEndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignStartTime", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignStartTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignEndTime", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignEndTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsAccepted", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsAccepted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccessStatus", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccessStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblResignationDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

       
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int ResignId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@ResignId"].Value = ResignId;
            Fill();
        }

        /***************************************************************** SMS *************************************************************************/
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySenderId(int AssignerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectResignationByAssignerId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySenderIdAndTableType(int AssignerId, int TableType, int TableId,Boolean IsAccepted)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.CommandText = "spSelectResignationByAssignerIdAndTableType";

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.SelectCommand.Parameters.Clear();

            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.Bit, 1, "IsAccepted").Value = IsAccepted;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySenderIdForSMS(int AssignerId, int SmsTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectResignationByAssignerIdForSMS", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;
            adapter.SelectCommand.Parameters.Add("@SmsTypeId", SqlDbType.Int, 4, "SmsTypeId").Value = SmsTypeId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType");

            adapter.Fill(dt);
            return (dt);
        }

        /************************************************************** Automation **********************************************************************/
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAssignerIdForAutomation(int AssignerId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAutomationResignation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TSP.DataManager.TableCodes.AutomationCartables;

            adapter.Fill(this.DataTable);
            return this.DataTable;
        }

        public DataTable FindActivesByAssignerIdForAutomation(int AssignerId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAutomationResignation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TSP.DataManager.TableCodes.AutomationCartables;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Bit).Value = 0;
            adapter.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.TinyInt).Value = (int)ResignationAcceptanceStatus.Pending;
            adapter.SelectCommand.Parameters.Add("@IsAccepted2", SqlDbType.TinyInt).Value = (int)ResignationAcceptanceStatus.Confiremd;

            adapter.Fill(this.DataTable);
            return this.DataTable;
        }

        public DataTable FindActivesForAutomation()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAutomationResignation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TSP.DataManager.TableCodes.AutomationCartables;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Bit).Value = 0;
            adapter.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.TinyInt).Value = (int)ResignationAcceptanceStatus.Pending;
            adapter.SelectCommand.Parameters.Add("@IsAccepted2", SqlDbType.TinyInt).Value = (int)ResignationAcceptanceStatus.Confiremd;

            adapter.Fill(this.DataTable);
            return this.DataTable;
        }

        public DataTable FindConfiremdActivesForAutomation()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAutomationResignation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TSP.DataManager.TableCodes.AutomationCartables;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Bit).Value = 0;
            adapter.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.TinyInt).Value = (int)ResignationAcceptanceStatus.Confiremd;

            adapter.Fill(this.DataTable);
            return this.DataTable;
        }

        public void SetExpiredAndEndedResignations(string Date,string Time)
        {
            SqlCommand sc = new SqlCommand("spSetExpiredResignation", this.Connection);
            sc.Transaction = this.Transaction;
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            if (sc.Connection.State != ConnectionState.Open)
                sc.Connection.Open();

            sc.Parameters.Add("@Date", System.Data.SqlDbType.Char).Value = Date;

            sc.ExecuteNonQuery();


            SqlCommand sqc = new SqlCommand("spSetEndedResignation", this.Connection);
            sqc.Transaction = this.Transaction;
            sqc.Connection = this.Connection;
            sqc.CommandType = CommandType.StoredProcedure;
            if (sqc.Connection.State != ConnectionState.Open)
                sqc.Connection.Open();

            sqc.Parameters.Add("@Date", System.Data.SqlDbType.Char).Value = Date;
            sqc.Parameters.Add("@Time", System.Data.SqlDbType.Char).Value = Time;

            sqc.ExecuteNonQuery();
        }

        public DataTable SelectValidResignationForAutomation(int AssignerId, int CreationType)
        {
            if (this.ClearBeforeFill)
                this.DataTable.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectValidResignation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.Add("@AssignerId", SqlDbType.Int, 4, "AssignerId").Value = AssignerId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TSP.DataManager.TableCodes.AutomationCartables;
            adapter.SelectCommand.Parameters.Add("@CreationType", SqlDbType.TinyInt).Value = CreationType;

            adapter.Fill(this.DataTable);
            return this.DataTable;
        }
    }
}
