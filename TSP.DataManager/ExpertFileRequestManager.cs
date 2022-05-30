using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class ExpertFileRequestManager : BaseObject
    {
        public enum RequestType
        {
            New = 0, Change = 1, Invalid = 2
        }

        #region Private Managers
        TSP.DataManager.ExpertFileManager ExpertFileManager;
        #endregion

        #region Constructors
        public ExpertFileRequestManager()
        {
            ExpertFileManager = new ExpertFileManager();
        }

        public ExpertFileRequestManager(TransactionManager TransactionManager)
        {
            ExpertFileManager = new ExpertFileManager();
            if (TransactionManager != null)
            {
                TransactionManager.Add(ExpertFileManager);
            }
        }
        #endregion

        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Confirming ExpertFileConfirming
        /// </summary>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int EfReqId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(EfReqId);
            if (this.Count > 0)
            {
                //#region Copy ExpertRequest Into Expert
                int EfId = int.Parse(this[0]["EfId"].ToString());
                ExpertFileManager.FindByCode(EfId);
                if (ExpertFileManager.Count != 1)
                {
                    return (int)ErrorWFNextStep.Error;
                }

                ExpertFileManager[0].BeginEdit();

                ExpertFileManager[0]["MeId"] = this[0]["MeId"];
                ExpertFileManager[0]["FileNo"] = this[0]["FileNo"];
                ExpertFileManager[0]["IssueDate"] = this[0]["IssueDate"];
                ExpertFileManager[0]["ExpireDate"] = this[0]["ExpireDate"];
                ExpertFileManager[0]["DocumentFrontImageURL"] = this[0]["DocumentFrontImageURL"];
                ExpertFileManager[0]["DocumentBackImageURL"] = this[0]["DocumentBackImageURL"];
                ExpertFileManager[0]["Description"] = this[0]["Description"];
                if (Convert.ToInt32(this[0]["Type"]) == (int)ExpertFileRequestManager.RequestType.Invalid)
                    ExpertFileManager[0]["InActive"] = 2;
                else
                    ExpertFileManager[0]["InActive"] = 0;

                ExpertFileManager[0].EndEdit();
                if (ExpertFileManager.Save() <= 0)
                {
                    return (int)ErrorWFNextStep.Error;
                }

                this[0].BeginEdit();
                this[0]["IsConfirm"] = 1;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
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
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting ExpertFileConfirming
        /// </summary>

        /// <returns></returns>
        public int DoNextTaskOfRejecting(int EfReqId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(EfReqId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
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
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        public int UpdateWfState(int EfReqId, Int64 CurrentWFStateId)
        {
            int Per = 0;
            this.FindByCode(EfReqId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["CurrentWFStateId"] = CurrentWFStateId;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
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
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }
        #endregion
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExpertFileRequest);
        }
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "ExpertExpertFileRequest";
            tableMapping.ColumnMappings.Add("EfReqId", "EfReqId");
            tableMapping.ColumnMappings.Add("EfId", "EfId");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("IssueDate", "IssueDate");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("DocumentFrontImageURL", "DocumentFrontImageURL");
            tableMapping.ColumnMappings.Add("DocumentBackImageURL", "DocumentBackImageURL");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("CurrentWFStateId", "CurrentWFStateId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectExpertFileRequest";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EfReqId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@EfId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeletExpertFileRequest";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EfReqId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "EfReqId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertExpertFileRequest";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IssueDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IssueDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentFrontImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentFrontImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentBackImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentBackImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWFStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWFStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateExpertFileRequest";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IssueDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IssueDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentFrontImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentFrontImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentBackImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentBackImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWFStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWFStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EfReqId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EfReqId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EfReqId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "EfReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {

            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.ExpertDataSet.ExpertExpertFileRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        public void FindByCode(int EfReqId)
        {
            this.Adapter.SelectCommand.Parameters["@EfReqId"].Value = EfReqId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectExpertFileRequestForManagmantpage(int EfId, int EfReqId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectExpertFileRequestForManagmantpage", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EfId", EfId);
            adapter.SelectCommand.Parameters.AddWithValue("@EfReqId", EfReqId);

            adapter.Fill(dt);
            return (dt);
        }


        public void Search(int EfId, int IsConfirm)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EfId"].Value = EfId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            Fill();
        }
    }
}
