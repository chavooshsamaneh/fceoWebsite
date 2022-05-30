using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class EntezamiComplainManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EntezamiComplain);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "EntezamiComplain";
            tableMapping.ColumnMappings.Add("ClnId", "ClnId");
            tableMapping.ColumnMappings.Add("ClnCode", "ClnCode");
            tableMapping.ColumnMappings.Add("Subject", "Subject");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CsId", "CsId");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("Body", "Body");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("RegisterNo", "RegisterNo");
            tableMapping.ColumnMappings.Add("BuildingLicense", "BuildingLicense");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("Foundation", "Foundation");
            tableMapping.ColumnMappings.Add("MaxStage", "MaxStage");
            tableMapping.ColumnMappings.Add("ComputerCode", "ComputerCode");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectComplain";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ClnId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ClnCode", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@Subject", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@CsId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CitId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteComplain";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ClnId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertComplain";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CsId", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingLicense", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingLicense", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStage", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateComplain";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CsId", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //  this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingLicense", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingLicense", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ClnId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ClnId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStage", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
          
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.EntezamiDataSet.EntezamiComplainDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int ClnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTableType(int TableType, string ClnCode, string FromDate, string ToDate, string Subject, int CsId, int CitId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            this.Adapter.SelectCommand.Parameters["@ClnCode"].Value = ClnCode;
            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            this.Adapter.SelectCommand.Parameters["@Subject"].Value = Subject;
            this.Adapter.SelectCommand.Parameters["@CsId"].Value = CsId;
            this.Adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindForReport(int EcrId, int ClnId, int TableType, string ClnCode, string FromDate, string ToDate, string Subject, int CsId, int CitId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectComplainReport", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@EcrId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ClnId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ClnCode", SqlDbType.NVarChar);
            adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar);
            adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar);
            adapter.SelectCommand.Parameters.Add("@Subject", SqlDbType.NVarChar);
            adapter.SelectCommand.Parameters.Add("@CsId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int);

            adapter.SelectCommand.Parameters["@EcrId"].Value = EcrId;
            adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            adapter.SelectCommand.Parameters["@ClnCode"].Value = ClnCode;
            adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            adapter.SelectCommand.Parameters["@Subject"].Value = Subject;
            adapter.SelectCommand.Parameters["@CsId"].Value = CsId;
            adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
            adapter.Fill(dt);
            return (dt);
        }

        public string MoteshakiComplainCount(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("MoteshakiComplainCount", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            adapter.Fill(dt);
            string count = dt.Rows[0]["ComplainCount"].ToString();
            return (count);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEntezamiComplainMember(int TableType, int MeId, int UltId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEntezamiComplainMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            adapter.SelectCommand.Parameters.Add("@UltId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEntezamiAnnualPerformance(string Id, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEntezamiAnnualPerformance", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@Id", SqlDbType.VarChar);
            adapter.SelectCommand.Parameters["@Id"].Value = Id;

            adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar);
            adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;

            adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar);
            adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectComplainSessions(int TableType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSession_SessionsForTableType", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectComplainAgenda(int TableType, int SessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectComplainAgenda", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            adapter.SelectCommand.Parameters.Add("@SessionId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@SessionId"].Value = SessionId;
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// if complain is closed return true otherwise return false
        /// </summary>
        /// <param name="ClnId"></param>
        /// <returns></returns>
        public bool CheckIfComplainIsClosed(int ClnId)
        {
            this.FindByCode(ClnId);
            if (this.Count == 1)
            {
                int CsId = Convert.ToInt32(this[0]["CsId"]);
                if ((CsId == (int)ComplainStatus.Closed) || (CsId == (int)ComplainStatus.Cancel)
                    || (CsId == (int)ComplainStatus.Rejected))
                    return true;
                else return false;
            }
            else return true;
        }

        public bool IsFinishComplain(int ClnId)
        {
            this.FindByCode(ClnId);
            if (this.Count == 1)
            {
                int CsId = Convert.ToInt32(this[0]["CsId"]);
                if (CsId == (int)ComplainStatus.Closed)
                    return true;
                else return false;
            }
            else return true;
        }

        public bool IsInOrBeforeComplainStatus(ComplainStatus Status, int ClnId)
        {
            this.FindByCode(ClnId);
            if (this.Count == 1)
            {
                int CurrentStatus = int.Parse(this[0]["CsId"].ToString());
                if (CurrentStatus < (int)Status)
                    return true;
                else return false;
            }
            return false;
        }

        #region WF Methods
        public int ChangeComplainStatus(int EcrId, int CurrentUserId, ComplainStatus Status)
        {
            int Per = 0;
            EntezamiComplainRequestManager ComplainRequestManager = new EntezamiComplainRequestManager();
            int ClnId = ComplainRequestManager.FindClnId(EcrId);
            if (ClnId != -1)
            {
                this.FindByCode(ClnId);
                if (this.Count == 1)
                {
                    this[0].BeginEdit();
                    this[0]["CsId"] = (int)Status;
                    this[0]["UserId"] = CurrentUserId;
                    this[0]["ModifiedDate"] = DateTime.Now;
                    this[0].EndEdit();
                    if (this.Save() > 0)
                    {
                        this.DataTable.AcceptChanges();
                        Per = 0;
                    }
                    else
                    {
                        Per = (int)ErrorWFNextStep.Error;
                    }
                }
                else
                {
                    Per = (int)ErrorRequest.LoseRequestInfo;
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            return Per;
        }
        public int ChangeComplainStatus(int EcrId, int CurrentUserId, ComplainStatus Status
                 , EntezamiComplainRequestManager ComplainRequestManager)
        {
            int Per = 0;
            //  EntezamiComplainRequestManager ComplainRequestManager = new EntezamiComplainRequestManager();
            int ClnId = ComplainRequestManager.FindClnId(EcrId);
            if (ClnId != -1)
            {
                this.FindByCode(ClnId);
                if (this.Count == 1)
                {
                    this[0].BeginEdit();
                    this[0]["CsId"] = (int)Status;
                    this[0]["UserId"] = CurrentUserId;
                    this[0]["ModifiedDate"] = DateTime.Now;
                    this[0].EndEdit();
                    if (this.Save() > 0)
                    {
                        this.DataTable.AcceptChanges();
                        Per = 0;
                    }
                    else
                    {
                        Per = (int)ErrorWFNextStep.Error;
                    }
                }
                else
                {
                    Per = (int)ErrorRequest.LoseRequestInfo;
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            return Per;
        }
        public bool IsInWorkFlowTask(WorkFlowTask WFTask, int ClnId)
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            TSP.DataManager.EntezamiComplainRequestManager ComplainRequestManager = new TSP.DataManager.EntezamiComplainRequestManager();
            ComplainRequestManager.FindByTableId(ClnId, (int)TSP.DataManager.ComplainRequestType.SaveComplain, 0);
            if (ComplainRequestManager.Count == 1)
            {
                int EcrId = Convert.ToInt32(ComplainRequestManager[0]["EcrId"]);
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EntezamiComplainRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, EcrId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    if (CurrentTaskCode == (int)WFTask)
                        return true;
                    else return false;
                }
            }
            return false;
        }

        public int CheckBeforeConfirmConditions(int EcrId, EntezamiComplainRequestManager EntezamiComplainRequestManager)
        {
            int ClnId = -1;
            int Per = 0;
            //   EntezamiComplainRequestManager EntezamiComplainRequestManager = new DataManager.EntezamiComplainRequestManager();
            EntezamiComplainRequestManager.FindByCode(EcrId);
            if (EntezamiComplainRequestManager.Count == 1)
                ClnId = Convert.ToInt32(EntezamiComplainRequestManager[0]["TableId"]);
            else
            {
                Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                return Per;
            }
            Per = CheckSession(ClnId);
            if (Per != 0) return Per;
            Per = CheckMoteshakiOrders(ClnId);
            if (Per != 0) return Per;
            Per = CheckMoteshakiFinalOrders(ClnId);
            if (Per != 0) return Per;
            return Per;
        }
        public int CheckBeforeNextStepConditions(int EcrId, int CurrentTaskCode, EntezamiComplainRequestManager EntezamiComplainRequestManager)
        {
            int ClnId = -1;
            int Per = 0;
            EntezamiComplainRequestManager.FindByCode(EcrId);
            if (EntezamiComplainRequestManager.Count == 1)
                ClnId = Convert.ToInt32(EntezamiComplainRequestManager[0]["TableId"]);
            else
            {
                Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                return Per;
            }
            switch (CurrentTaskCode)
            {
                case (int)TSP.DataManager.WorkFlowTask.SavingDisciplinaryComplainVote:
                    Per = CheckMoteshakiOrders(ClnId);
                    break;
                case (int)TSP.DataManager.WorkFlowTask.InformOrderAndWaitingRivisionRequest:
                    Per = CheckMoteshakiFinalOrders(ClnId);
                    break;
                case (int)TSP.DataManager.WorkFlowTask.DisciplinaryComplainDefineSessionTimeAndAssignToSession:
                    Per = CheckSession(ClnId);
                    break;
            }
            return Per;
        }

        public int CheckMoteshakiOrders(int ClnId)
        {
            int Per = 0;
            bool result = false;

            TSP.DataManager.EntezamiMoteshakiManager EntezamiMoteshakiManager = new EntezamiMoteshakiManager();
            EntezamiComplainOrderManager ComplainOrderManager = new EntezamiComplainOrderManager();
            EntezamiMoteshakiManager.FindByComplainCode(ClnId);
            if (EntezamiMoteshakiManager.Count > 0)
            {
                foreach (DataRow row in EntezamiMoteshakiManager.DataTable.Rows)
                {
                    ComplainOrderManager.FindByCode(ClnId, Convert.ToInt32(row["MotId"]), 0);
                    if (ComplainOrderManager.Count <= 0)
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                    Per = (int)TSP.DataManager.ErrorWFNextStep.NoSaveOrder;
                else Per = 0;

            }
            else Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
            return Per;
        }
        public int CheckMoteshakiFinalOrders(int ClnId)
        {
            int Per = 0;
            bool result = false;
            TSP.DataManager.EntezamiMoteshakiManager EntezamiMoteshakiManager = new EntezamiMoteshakiManager();
            EntezamiComplainOrderManager ComplainOrderManager = new EntezamiComplainOrderManager();
            EntezamiMoteshakiManager.FindByComplainCode(ClnId);
            if (EntezamiMoteshakiManager.Count > 0)
            {
                foreach (DataRow row in EntezamiMoteshakiManager.DataTable.Rows)
                {
                    ComplainOrderManager.FindByCode(ClnId, Convert.ToInt32(row["MotId"]), 0);
                    if ((ComplainOrderManager.Count > 0) && (!Convert.ToBoolean(ComplainOrderManager[0]["IsFinal"])))
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                    Per = (int)TSP.DataManager.ErrorWFNextStep.NoSaveFinalOrder;
                else Per = 0;
            }
            else Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
            return Per;
        }
        public int CheckSession(int ClnId)
        {
            int Per = 0;
            Session.AgendaManager AgendaManager = new Session.AgendaManager();
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Complain);
            DataTable dt = AgendaManager.SelectAgendaByTableType(TableType, ClnId);
            if (dt.Rows.Count == 0)
                Per = (int)TSP.DataManager.ErrorWFNextStep.NoSaveComplainSession;
            return Per;
        }
        #endregion



        //---------------------------------------------------------------------------------------------------------------
        //#region Miss.Dordahan
        // [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        //public virtual int Delete(int Original_ClnId, byte[] Original_LastTimeStamp)
        //{
        //    this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_ClnId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
        //    if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.DeleteCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.DeleteCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        //public virtual int Insert(string ClnCode, string Subject, string Description, short CsId, int UserId, System.DateTime ModifiedDate)
        //{
        //    if ((ClnCode == null))
        //    {
        //        throw new System.ArgumentNullException("ClnCode");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[1].Value = ((string)(ClnCode));
        //    }
        //    if ((Subject == null))
        //    {
        //        throw new System.ArgumentNullException("Subject");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Subject));
        //    }
        //    if ((Description == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = ((string)(Description));
        //    }
        //    this.Adapter.InsertCommand.Parameters[4].Value = ((short)(CsId));
        //    this.Adapter.InsertCommand.Parameters[5].Value = ((int)(UserId));
        //    this.Adapter.InsertCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
        //    if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.InsertCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.InsertCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        //public virtual int Update(string ClnCode, string Subject, string Description, short CsId, int UserId, System.DateTime ModifiedDate, int Original_ClnId, byte[] Original_LastTimeStamp, int ClnId)
        //{
        //    if ((ClnCode == null))
        //    {
        //        throw new System.ArgumentNullException("ClnCode");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(ClnCode));
        //    }
        //    if ((Subject == null))
        //    {
        //        throw new System.ArgumentNullException("Subject");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Subject));
        //    }
        //    if ((Description == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(Description));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[4].Value = ((short)(CsId));
        //    this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(UserId));
        //    this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
        //    this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_ClnId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(ClnId));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
        //    if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.UpdateCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.UpdateCommand.Connection.Close();
        //        }
        //    }
        //}
        //public void FindByCode(int ClnId)
        //{
        //    this.Adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
        //    Fill();
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public System.Data.DataTable SearchComplain(string ClnCode,short Status,string ShakiName,string ShakiLastName,string MoteshakiName,string MoteshakiLastName,string OfName,string Subject,string FromDate,string ToDate,short Type,int CitId,string FollowCode)
        //{
        //    DataTable dt = new DataManager.EntezamiDataSet.EntezamiComplainDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSearchComplain", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add("@ClnCode",System.Data.SqlDbType.NVarChar,15);
        //    adapter.SelectCommand.Parameters.Add("@Status", System.Data.SqlDbType.SmallInt);
        //    adapter.SelectCommand.Parameters.Add("@ShakiName", System.Data.SqlDbType.NVarChar, 30);
        //    adapter.SelectCommand.Parameters.Add("@ShakiLastName", System.Data.SqlDbType.NVarChar, 50);
        //    adapter.SelectCommand.Parameters.Add("@MoteshakiName", System.Data.SqlDbType.NVarChar, 30);
        //    adapter.SelectCommand.Parameters.Add("@MoteshakiLastName", System.Data.SqlDbType.NVarChar, 50);
        //    adapter.SelectCommand.Parameters.Add("@OfName", System.Data.SqlDbType.NVarChar, 80);            
        //    adapter.SelectCommand.Parameters.Add("@Subject", System.Data.SqlDbType.NVarChar, 80);
        //    adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.VarChar, 10);
        //    adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.VarChar, 10);
        //    adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.SmallInt);
        //    adapter.SelectCommand.Parameters.Add("@CitId", System.Data.SqlDbType.Int);
        //    adapter.SelectCommand.Parameters.Add("@FollowCode", System.Data.SqlDbType.VarChar, 12);         

        //    if (string.IsNullOrEmpty(ClnCode))
        //        ClnCode = "%";
        //    adapter.SelectCommand.Parameters["@ClnCode"].Value = ClnCode;
        //    if (string.IsNullOrEmpty(Status.ToString()))
        //        Status = -1;
        //    adapter.SelectCommand.Parameters["@Status"].Value = Status;
        //    if (string.IsNullOrEmpty(ShakiName))
        //        ShakiName = "%";
        //    adapter.SelectCommand.Parameters["@ShakiName"].Value = ShakiName;
        //    if (string.IsNullOrEmpty(ShakiLastName))
        //        ShakiLastName = "%";
        //    adapter.SelectCommand.Parameters["@ShakiLastName"].Value = ShakiLastName;
        //    if (string.IsNullOrEmpty(MoteshakiName))
        //        MoteshakiName = "%";
        //    adapter.SelectCommand.Parameters["@MoteshakiName"].Value = MoteshakiName;
        //    if (string.IsNullOrEmpty(MoteshakiLastName))
        //        MoteshakiLastName = "%";
        //    adapter.SelectCommand.Parameters["@MoteshakiLastName"].Value = MoteshakiLastName;
        //    if (string.IsNullOrEmpty(OfName))
        //        OfName = "%";
        //    adapter.SelectCommand.Parameters["@OfName"].Value = OfName;
        //    if (string.IsNullOrEmpty(Subject))
        //        Subject = "%";
        //    adapter.SelectCommand.Parameters["@Subject"].Value = Subject;
        //    if (string.IsNullOrEmpty(FromDate))
        //        FromDate = "1";
        //    adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
        //    if (string.IsNullOrEmpty(ToDate))
        //        ToDate = "2";
        //    adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
        //    if (string.IsNullOrEmpty(Type.ToString()))
        //        Type = -1;
        //    adapter.SelectCommand.Parameters["@Type"].Value = Type;
        //    if (string.IsNullOrEmpty(CitId.ToString()))
        //        CitId = -1;
        //    adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
        //    if (string.IsNullOrEmpty(FollowCode))
        //        FollowCode = "%";
        //    adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;

        //    adapter.Fill(dt);
        //    return (dt);

        //}
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectUniqueLastName()
        //{
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectComplainUniqueFamily", this.Connection);

        //    adapter.Fill(dt);
        //    return (dt);



        //}
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectUniqueMoLastName()
        //{
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectComplainUniqueMoFamily", this.Connection);

        //    adapter.Fill(dt);
        //    return (dt);



        //}
        //#endregion
    }
}
