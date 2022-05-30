using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TSP.DataManager
{
    public class EntezamiComplainRequestManager : BaseObject
    {
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Entezami.ComplainRequest";
            tableMapping.ColumnMappings.Add("EcrId", "EcrId");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ClnCode", "ClnCode");
            tableMapping.ColumnMappings.Add("Subject", "Subject");
            tableMapping.ColumnMappings.Add("DescriptionComplain", "DescriptionComplain");
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
            Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectComplainRequest";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add("@EcrId", System.Data.SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@TableId", System.Data.SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);
            Adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int);

            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeleteComplainRequest";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EcrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EcrId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertComplainRequest";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DescriptionComplain", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
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


            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdateComplainRequest";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EcrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EcrId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EcrId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "EcrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStage", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ClnCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ClnCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DescriptionComplain", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CsId", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //  this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingLicense", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingLicense", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.EntezamiDataSet.EntezamiComplainRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int EcrId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EcrId"].Value = EcrId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTableId(int TableId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TableId"].Value = TableId;
            Fill();
            return this.DataTable;
        }

        public int FindClnId(int EcrId)
        {
            int ClnId = -1;
            this.FindByCode(EcrId);
            if (this.Count == 1)
                ClnId = Convert.ToInt32(this[0]["TableId"]);
            return ClnId;
        }

        public void FindByTableId(int TableId, int Type, int IsConfirm)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TableId"].Value = TableId;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            Fill();
        }

        public int Insert(int TableId, int IsConfirm, int Type, string LetterNo, string LetterDate,
            string Description, int UserId)
        {
            int id = -1;
            DataRow Row = this.NewRow();
            Row["TableId"] = TableId;
            Row["IsConfirm"] = IsConfirm;
            Row["Type"] = Type;
            Row["LetterNo"] = LetterNo;
            Row["LetterDate"] = LetterDate;
            Row["Description"] = Description;
            Row["CreateDate"] = Utility.GetDateOfToday();
            Row["UserId"] = UserId;
            Row["ModifiedDate"] = DateTime.Now;
            this.AddRow(Row);
            if (this.Save() > 0)
            {
                id = Convert.ToInt32(this[0]["EcrId"].ToString());
                return id;
            }
            else return -1;
        }

        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Reject or Confirm Complain
        /// </summary>
        /// <param name="ClnId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskForComplain(int EcrId, int CurrentUserId, ComplainStatus Status, int IsConfirm, EntezamiComplainManager EntezamiComplainManager, EntezamiComplainOrderManager EntezamiComplainOrderManager)
        {
            //TransactionManager TransactManager = new TransactionManager();
            //TransactManager.Add(this);
            //TransactManager.Add(ComplainRequestManager);
            int Per = 0;
            //  TransactManager.BeginSave();
            int ClnId = this.FindClnId(EcrId);
            int ProjectId = -1;
            if (ClnId != -1)
            {
                EntezamiComplainManager.FindByCode(ClnId);
                if (EntezamiComplainManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(EntezamiComplainManager[0]["ProjectId"]))
                        ProjectId = Convert.ToInt32(EntezamiComplainManager[0]["ProjectId"]);
                    EntezamiComplainManager[0].BeginEdit();
                    EntezamiComplainManager[0]["CsId"] = (int)Status;
                    EntezamiComplainManager[0]["UserId"] = CurrentUserId;
                    EntezamiComplainManager[0]["ModifiedDate"] = DateTime.Now;
                    EntezamiComplainManager[0].EndEdit();
                    if (EntezamiComplainManager.Save() > 0)
                    {
                        //---------update request------
                        //   ComplainRequestManager.FindByTableId(ClnId, (int)ComplainRequestType.SaveComplain, 0);
                        this.FindByCode(EcrId);
                        if (this.Count == 1)
                        {
                            this[0].BeginEdit();
                            this[0]["IsConfirm"] = IsConfirm;
                            this[0]["UserId"] = CurrentUserId;
                            this[0]["ModifiedDate"] = DateTime.Now;
                            this[0].EndEdit();
                            if (this.Save() > 0)
                            {
                                //   TransactManager.EndSave();
                                int Type = Convert.ToInt32(this[0]["Type"]);
                                if (Type == (int)ComplainRequestType.SaveComplain)
                                    Per = DecreasedCapacity(ClnId, CurrentUserId, ProjectId, EntezamiComplainOrderManager);
                            }
                            else
                            {
                                //  TransactManager.CancelSave();
                                Per = (int)ErrorWFNextStep.Error;
                            }
                        }
                        else
                        {
                            //  TransactManager.CancelSave();
                            Per = (int)ErrorWFNextStep.Error;
                        }
                    }
                    else
                    {
                        //   TransactManager.CancelSave();
                        Per = (int)ErrorWFNextStep.Error;
                    }
                }
                else
                {
                    //   TransactManager.CancelSave();
                    Per = (int)ErrorRequest.LoseRequestInfo;
                }
            }
            else
            {
                //  TransactManager.CancelSave();
                Per = (int)ErrorRequest.Error;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of cancel Confirm Complain
        /// </summary>
        /// <param name="ClnId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfCancelConfirming(int EcrId, int CurrentUserId, EntezamiComplainManager EntezamiComplainManager,
            WorkFlowStateManager WorkFlowStateManager, WorkFlowTaskManager WorkFlowTaskManager)
        {
            int Per = 0;

            int ClnId = this.FindClnId(EcrId);
            if (ClnId != -1)
            {
                EntezamiComplainManager.FindByCode(ClnId);
                if (EntezamiComplainManager.Count == 1)
                {
                    //------update complain status--------
                    EntezamiComplainManager[0].BeginEdit();
                    EntezamiComplainManager[0]["CsId"] = (int)ComplainStatus.Cancel;
                    EntezamiComplainManager[0]["UserId"] = CurrentUserId;
                    EntezamiComplainManager[0]["ModifiedDate"] = DateTime.Now;
                    EntezamiComplainManager[0].EndEdit();
                    if (EntezamiComplainManager.Save() > 0)
                    {
                        //---------update request--------
                        this.FindByCode(EcrId);
                        if (this.Count == 1)
                        {
                            this[0].BeginEdit();
                            this[0]["IsConfirm"] = 1;//------confirm cancel----
                            this[0]["UserId"] = CurrentUserId;
                            this[0]["ModifiedDate"] = DateTime.Now;
                            this[0].EndEdit();
                            if (this.Save() > 0)
                            {
                                this.DataTable.AcceptChanges();
                                //----update complain request----------------                     
                                int TaskId = -1;
                                int EcrIdOfComplain = -1;
                                this.FindByTableId(ClnId, (int)ComplainRequestType.SaveComplain, 0);
                                if (this.Count == 1)
                                {
                                    EcrIdOfComplain = Convert.ToInt32(this[0]["EcrId"]);
                                    this[0].BeginEdit();
                                    this[0]["IsConfirm"] = 2;//------not confirm complain------
                                    this[0]["UserId"] = CurrentUserId;
                                    this[0]["ModifiedDate"] = DateTime.Now;
                                    this[0].EndEdit();
                                    if (this.Save() > 0)
                                    {
                                        if (EcrIdOfComplain == -1)
                                        {
                                            return (int)ErrorWFNextStep.Error;

                                        }
                                        //----workflow---------------- 
                                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EntezamiComplainRequest);
                                        WorkFlowTaskManager.FindByTaskCode((int)WorkFlowTask.RejectDisciplinaryComplainAndEndProcess);
                                        if (WorkFlowTaskManager.Count == 1)
                                            TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
                                        if (TaskId == -1)
                                        {
                                            return (int)ErrorWFNextStep.Error;
                                        }
                                        //if (WorkFlowStateManager.SendDocToNextStep(TableType, EcrIdOfComplain, TaskId, "عدم تایید شکوائیه", CurrentNmcId,
                                        //    CurrentUserId, (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId) > 0)
                                        int PId = Automation.PriorityManager.FintPId(Automation.PriorityManager.Priority.Normal);
                                        if (PId == -1)
                                        {
                                            return (int)ErrorWFNextStep.Error;
                                        }
                                        if (WorkFlowStateManager.InsertWorkFlowState(TableType, EcrIdOfComplain, TaskId,
                                           "عدم تایید شکوائیه", 0, (int)WorkFlowStateNmcIdType.System, CurrentUserId, PId,
                                            Utility.GetDateOfToday()) > 0)
                                        {
                                            Per = 0;
                                        }
                                        else
                                        {
                                            return (int)ErrorWFNextStep.Error;
                                        }
                                    }
                                    else
                                    {
                                        return (int)ErrorWFNextStep.Error;
                                    }
                                }
                                else
                                {
                                    return (int)ErrorWFNextStep.Error;
                                }
                            }
                            else
                            {
                                return (int)ErrorWFNextStep.Error;
                            }
                        }
                        else
                        {
                            return (int)ErrorWFNextStep.Error;
                        }

                    }
                    else
                    {
                        return (int)ErrorWFNextStep.Error;
                    }
                }
                else
                {
                    return (int)ErrorRequest.LoseRequestInfo;
                }
            }
            else
            {
                return (int)ErrorRequest.Error;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of cancel Rejecting Complain
        /// </summary>
        /// <param name="ClnId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfCancelRejecting(int EcrId, int CurrentUserId)
        {
            int Per = 0;

            //---------update request--------
            this.FindByCode(EcrId);
            if (this.Count == 1)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of reject order
        /// </summary>
        /// <param name="ClnId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejectOrderConfirming(int EcrId, int CurrentUserId, EntezamiComplainManager EntezamiComplainManager,
            WorkFlowStateManager WorkFlowStateManager, WorkFlowTaskManager WorkFlowTaskManager, EntezamiComplainOrderManager ComplainOrderManager)
        {
            int Per = 0;

            int ClnId = this.FindClnId(EcrId);
            if (ClnId != -1)
            {
                EntezamiComplainManager.FindByCode(ClnId);
                if (EntezamiComplainManager.Count == 1)
                {
                    //------update complain status--------
                    EntezamiComplainManager[0].BeginEdit();
                    EntezamiComplainManager[0]["CsId"] = (int)ComplainStatus.RejectedOrder;
                    EntezamiComplainManager[0]["UserId"] = CurrentUserId;
                    EntezamiComplainManager[0]["ModifiedDate"] = DateTime.Now;
                    EntezamiComplainManager[0].EndEdit();
                    if (EntezamiComplainManager.Save() > 0)
                    {
                        //---------update request--------
                        this.FindByCode(EcrId);
                        if (this.Count == 1)
                        {
                            this[0].BeginEdit();
                            this[0]["IsConfirm"] = 1;
                            this[0]["UserId"] = CurrentUserId;
                            this[0]["ModifiedDate"] = DateTime.Now;
                            this[0].EndEdit();
                            if (this.Save() > 0)
                            {
                                //-----------update orders of complain-------
                                ComplainOrderManager.FindByComplainCode(ClnId);
                                if (ComplainOrderManager.Count > 0)
                                {
                                    for (int i = 0; i < ComplainOrderManager.DataTable.Rows.Count; i++)
                                    {
                                        ComplainOrderManager[i].BeginEdit();
                                        ComplainOrderManager[i]["IsRejected"] = 1;
                                        ComplainOrderManager[i]["InActive"] = 1;
                                        ComplainOrderManager[i]["UserId"] = CurrentUserId;
                                        ComplainOrderManager[i]["ModifiedDate"] = DateTime.Now;
                                        ComplainOrderManager[i].EndEdit();
                                    }
                                    if (ComplainOrderManager.Save() > 0)
                                        Per = 0;
                                }
                                else
                                    Per = (int)ErrorWFNextStep.Error;
                                //-------------------------------------------
                            }
                            else
                            {
                                Per = (int)ErrorWFNextStep.Error;
                            }
                        }
                        else
                        {
                            Per = (int)ErrorWFNextStep.Error;
                        }

                    }
                    else
                    {
                        return (int)ErrorRequest.LoseRequestInfo;
                    }
                }
                else
                {
                    return (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                return (int)ErrorWFNextStep.Error;
            }
            return Per;
        }


        /// <summary>
        /// Perform the next tasks of reject order rejecting
        /// </summary>
        /// <param name="ClnId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejectOrderRejecting(int EcrId, int CurrentUserId)
        {
            int Per = 0;

            //---------update request--------
            this.FindByCode(EcrId);
            if (this.Count == 1)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }

            return Per;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WFTask"> مرحله گردش کار که قرار است با مرحله فعلی مقایسه شود </param>
        /// <param name="ClnId"></param>
        /// <returns></returns>
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

        private int DecreasedCapacity(int ClnId, int UserId, int ProjectId, EntezamiComplainOrderManager EntezamiComplainOrderManager)
        {
            int Per = 0;
            EntezamiComplainSubjectManager EntezamiComplainSubjectManager = new DataManager.EntezamiComplainSubjectManager();
            TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
            EntezamiComplainOrderManager.FindByComplain(ClnId, 1, 0);
            if (EntezamiComplainOrderManager.Count <= 0) return (int)ErrorWFNextStep.NoSaveFinalOrder;

            EntezamiComplainSubjectManager.FindByComplainCode(ClnId);
            if (EntezamiComplainSubjectManager.Count <= 0) return (int)ErrorRequest.Error;
            for (int j = 0; j < EntezamiComplainSubjectManager.Count; j++)
            {
                for (int i = 0; i < EntezamiComplainOrderManager.Count; i++)
                {
                    int ReduceCapacity = Convert.ToInt32(EntezamiComplainOrderManager[i]["ReduceCapacity"]);

                    DataRow rowConditionalCapacity = ConditionalCapacityManager.NewRow();
                    rowConditionalCapacity["ReasonId"] = 1; //حکم شورای انتظامی
                    rowConditionalCapacity["Capacity"] = ReduceCapacity;
                    rowConditionalCapacity["FromDate"] = EntezamiComplainOrderManager[i]["CreateDate"];
                    if (!Utility.IsDBNullOrNullValue(EntezamiComplainOrderManager[i]["ValidDate"]))
                        rowConditionalCapacity["ToDate"] = EntezamiComplainOrderManager[i]["ValidDate"];
                    else rowConditionalCapacity["ToDate"] = "2";
                    rowConditionalCapacity["MemberTypeId"] = (int)TSMemberType.Member;
                    rowConditionalCapacity["MeOfficeEngOId"] = EntezamiComplainOrderManager[i]["MeId"];
                    rowConditionalCapacity["ProjectIngridientTypeId"] = FindProjectIngridientTypeId(Convert.ToInt32(EntezamiComplainSubjectManager[i]["SbtId"]));
                    if (ProjectId != -1)
                        rowConditionalCapacity["ProjectId"] = ProjectId;
                    rowConditionalCapacity["InActive"] = 0;
                    rowConditionalCapacity["IsConfirmed"] = 1;
                    rowConditionalCapacity["UserId"] = UserId;
                    rowConditionalCapacity["ModifiedDate"] = DateTime.Now;

                    ConditionalCapacityManager.AddRow(rowConditionalCapacity);
                    if (ConditionalCapacityManager.Save() > 0)
                    {
                        ConditionalCapacityManager.DataTable.AcceptChanges();
                    }
                    else return (int)ErrorRequest.Error;
                }
            }

            return Per;
        }

        private int FindProjectIngridientTypeId(int SubjectType)
        {
            int Result = -1;
            switch (SubjectType)
            {
                case 2:
                    Result = (int)TSProjectIngridientType.Observer;
                    break;
                case 3:
                    Result = (int)TSProjectIngridientType.Designer;
                    break;
                case 4:
                    Result = (int)TSProjectIngridientType.Implementer;
                    break;
            }
            return Result;
        }
        #endregion
    }
}
