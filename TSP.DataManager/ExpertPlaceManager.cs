using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace TSP.DataManager
{
    public class ExpertPlaceManager:BaseObject
    {
        //static ExpertPlaceManager()
        //{
        //    _tableId = TableType.ExpertPlace;
        //}
        public ExpertPlaceManager()
            : base()
        {
        }
        public ExpertPlaceManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExpertPlace);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Expert.ExpertPlace";
            tableMapping.ColumnMappings.Add("EpId", "EpId");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("EpCode", "EpCode");
            tableMapping.ColumnMappings.Add("PersonId", "PersonId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("SecNo", "SecNo");
            tableMapping.ColumnMappings.Add("PlakNo", "PlakNo");
            tableMapping.ColumnMappings.Add("Area", "Area");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("Distant", "Distant");
            tableMapping.ColumnMappings.Add("Importance", "Importance");
            tableMapping.ColumnMappings.Add("EpType", "EpType");
            tableMapping.ColumnMappings.Add("SubjectId", "SubjectId");
            tableMapping.ColumnMappings.Add("Requester", "Requester");
            tableMapping.ColumnMappings.Add("ExpCount", "ExpCount");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("LockDate", "LockDate");
            tableMapping.ColumnMappings.Add("DeliveryDate", "DeliveryDate");
            tableMapping.ColumnMappings.Add("Deliverer", "Deliverer");
            tableMapping.ColumnMappings.Add("DeliverCreateDate", "DeliverCreateDate");
            tableMapping.ColumnMappings.Add("SendLetterDate", "SendLetterDate");
            tableMapping.ColumnMappings.Add("SendLetterNo", "SendLetterNo");
            tableMapping.ColumnMappings.Add("SendCreateDate", "SendCreateDate");
            tableMapping.ColumnMappings.Add("ExpertSalary", "ExpertSalary");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectExpertPlace";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EpId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ExReqName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@ExpName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EpCode", System.Data.SqlDbType.VarChar);
            this.Adapter.SelectCommand.Parameters.Add("@CitName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@DateFrom", System.Data.SqlDbType.Char);
            this.Adapter.SelectCommand.Parameters.Add("@DateTo", System.Data.SqlDbType.Char);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteExpertPlace";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertExpertPlace";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EpCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EpCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PersonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PersonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.Char,0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SecNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlakNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PlakNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Area", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Distant", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Distant", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Importance", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Importance", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EpType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EpType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SubjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SubjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpCount", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LockDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LockDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliveryDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Deliverer", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Deliverer", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliverCreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliverCreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendLetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendLetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendLetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SendLetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendCreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendCreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpertSalary", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpertSalary", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateExpertPlace";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EpCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EpCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PersonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PersonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@followCode", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SecNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlakNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PlakNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Area", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Distant", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Distant", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Importance", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Importance", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EpType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EpType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SubjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SubjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpCount", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LockDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LockDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliveryDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Deliverer", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Deliverer", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliverCreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "DeliverCreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendLetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendLetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendLetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SendLetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendCreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendCreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpertSalary", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpertSalary", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.ExpertDataSet.ExpertExpertPlaceDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int Id)
        {
            this.Adapter.SelectCommand.Parameters["@EpId"].Value = Id;
            Fill();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_EpId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_EpId));
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
        public virtual int Insert(string EpCode, int CitId, string SecNo, string PlakNo, double Area, string Address, byte Status, string Description, int UserId, System.DateTime ModifiedDate)
        {
            if ((EpCode == null))
            {
                throw new System.ArgumentNullException("EpCode");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(EpCode));
            }
            this.Adapter.InsertCommand.Parameters[2].Value = ((int)(CitId));
            if ((SecNo == null))
            {
                this.Adapter.InsertCommand.Parameters[3].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(SecNo));
            }
            if ((PlakNo == null))
            {
                this.Adapter.InsertCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(PlakNo));
            }
            this.Adapter.InsertCommand.Parameters[5].Value = ((double)(Area));
            if ((Address == null))
            {
                this.Adapter.InsertCommand.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[6].Value = ((string)(Address));
            }
            this.Adapter.InsertCommand.Parameters[7].Value = ((byte)(Status));
            if ((Description == null))
            {
                this.Adapter.InsertCommand.Parameters[8].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[8].Value = ((string)(Description));
            }
            this.Adapter.InsertCommand.Parameters[9].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[10].Value = ((System.DateTime)(ModifiedDate));
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
        public virtual int Update(string EpCode, int CitId, string SecNo, string PlakNo, double Area, string Address, byte Status, string Description, int UserId, System.DateTime ModifiedDate, int Original_EpId, byte[] Original_LastTimeStamp, int EpId)
        {
            if ((EpCode == null))
            {
                throw new System.ArgumentNullException("EpCode");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(EpCode));
            }
            this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(CitId));
            if ((SecNo == null))
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(SecNo));
            }
            if ((PlakNo == null))
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(PlakNo));
            }
            this.Adapter.UpdateCommand.Parameters[5].Value = ((double)(Area));
            if ((Address == null))
            {
                this.Adapter.UpdateCommand.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((string)(Address));
            }
            this.Adapter.UpdateCommand.Parameters[7].Value = ((byte)(Status));
            if ((Description == null))
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[10].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[11].Value = ((int)(Original_EpId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[12].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[13].Value = ((int)(EpId));
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
        public DataTable SearchExpertPlace(string EpCode,string FollowCode,int CitId,string SecNo,string PlakNo,float Area,byte Status)
        {
            DataTable dt = new DataManager.ExpertDataSet.ExpertExpertPlaceDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSearchExpertPlace", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@EpCode", SqlDbType.VarChar,20);
            adapter.SelectCommand.Parameters.Add("@FollowCode", SqlDbType.VarChar, 12);
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SecNo", SqlDbType.VarChar, 15);
            adapter.SelectCommand.Parameters.Add("@PlakNo", SqlDbType.VarChar, 15);
            adapter.SelectCommand.Parameters.Add("@Area", SqlDbType.Float);
            adapter.SelectCommand.Parameters.Add("@Status", SqlDbType.TinyInt);            


            if (string.IsNullOrEmpty(EpCode))
                EpCode = "%";
            adapter.SelectCommand.Parameters["@EpCode"].Value = EpCode;

            if (string.IsNullOrEmpty(FollowCode))
                FollowCode = "%";
            adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;
        
            if (string.IsNullOrEmpty(CitId.ToString()))
                CitId = -1;
            adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
            if (string.IsNullOrEmpty(SecNo))
                SecNo = "%";
            adapter.SelectCommand.Parameters["@SecNo"].Value = SecNo;
            if (string.IsNullOrEmpty(PlakNo))
                PlakNo = "%";
            adapter.SelectCommand.Parameters["@PlakNo"].Value = PlakNo;
            if (string.IsNullOrEmpty(Area.ToString()))
                Area = -1;
            adapter.SelectCommand.Parameters["@Area"].Value = Area;
            if (string.IsNullOrEmpty(Status.ToString()))
                Status = 0;
            adapter.SelectCommand.Parameters["@Status"].Value = Status;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByExpertMeId(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchExpertPlaceByEpId(int MeId, string ExReqName, string ExpName, string EpCode, string CitName, string DateFrom, string DateTo)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            if (string.IsNullOrEmpty(ExReqName))
                ExReqName = "%";
            this.Adapter.SelectCommand.Parameters["@ExReqName"].Value = ExReqName;
            if (string.IsNullOrEmpty(ExpName))
                ExpName = "%";
            this.Adapter.SelectCommand.Parameters["@ExpName"].Value = ExpName;
            if (string.IsNullOrEmpty(EpCode))
                EpCode = "%";
            this.Adapter.SelectCommand.Parameters["@EpCode"].Value = EpCode;
            if (string.IsNullOrEmpty(CitName))
                CitName = "%";
            this.Adapter.SelectCommand.Parameters["@CitName"].Value = CitName;
            if (string.IsNullOrEmpty(DateFrom))
                DateFrom = "1";
            this.Adapter.SelectCommand.Parameters["@DateFrom"].Value = DateFrom;
            if (string.IsNullOrEmpty(DateTo))
                DateTo = "3";
            this.Adapter.SelectCommand.Parameters["@DateTo"].Value = DateTo;

            //if (MeId != -2)
            //    adapter.Fill(dt);
            //return (dt);
            Fill();
            return this.DataTable;
        }

    }
}
