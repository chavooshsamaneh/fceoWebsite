using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class UniversityManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.University);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblUniversity";
            tableMapping.ColumnMappings.Add("UnId", "UnId");
            tableMapping.ColumnMappings.Add("UnCode", "UnCode");
            tableMapping.ColumnMappings.Add("UnName", "UnName");
            tableMapping.ColumnMappings.Add("UnType", "UnType");
            tableMapping.ColumnMappings.Add("IsForeign", "IsForeign");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CounId", "CounId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            
            this.Adapter.TableMappings.Add(tableMapping);

          

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectUniversity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@UnId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@UnCode", System.Data.SqlDbType.VarChar, 15);
            this.Adapter.SelectCommand.Parameters.Add("@UnName", System.Data.SqlDbType.NVarChar, 80);
            this.Adapter.SelectCommand.Parameters.Add("@UnType", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@IsForeign", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@CounId", System.Data.SqlDbType.Int);           

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteUniversity";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UnId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertUniversity";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UnCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UnName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UnType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsForeign", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsForeign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateUniversity";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UnCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "UnName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UnType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsForeign", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsForeign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UnId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "UnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblUniversityDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        private void SetDefualtSelectCommantSetting()
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            ResetAllParameters();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectUniversity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@UnId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@UnCode", System.Data.SqlDbType.VarChar, 15);
            this.Adapter.SelectCommand.Parameters.Add("@UnName", System.Data.SqlDbType.NVarChar, 80);
            this.Adapter.SelectCommand.Parameters.Add("@UnType", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@IsForeign", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@CounId", System.Data.SqlDbType.Int);         
        }

        public void FindByCode(int UnId)
        {
            SetDefualtSelectCommantSetting();
            this.Adapter.SelectCommand.Parameters["@UnId"].Value = UnId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchUniversity(string UnCode, string UnName, short UnType, short IsForeign, int CounId)
        {
            SetDefualtSelectCommantSetting();
            if (string.IsNullOrEmpty(UnCode))
                UnCode = "%";
            this.Adapter.SelectCommand.Parameters["@UnCode"].Value = UnCode;
            if (string.IsNullOrEmpty(UnName))
                UnName = "%";
            this.Adapter.SelectCommand.Parameters["@UnName"].Value = UnName;
            if (string.IsNullOrEmpty(UnType.ToString()))
                UnType = -1;
            this.Adapter.SelectCommand.Parameters["@UnType"].Value = UnType;
            if (string.IsNullOrEmpty(IsForeign.ToString()))
                IsForeign = -1;
            this.Adapter.SelectCommand.Parameters["@IsForeign"].Value = IsForeign;
            if (string.IsNullOrEmpty(CounId.ToString()))
                CounId = -1;
            this.Adapter.SelectCommand.Parameters["@CounId"].Value = CounId;
            Fill();
            return DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedUniversity()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUniversityConfirmed", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        #region SelectConfirmedUniversityByCounId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedUniversityByCounId(int CounId)
        {
            SelectConfirmedUniversityByCounId(CounId, "%");
            return (this.DataTable);
        }     

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedUniversityByCounId(int CounId, String UnName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUniversityConfirmed", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CounId", CounId);
            adapter.SelectCommand.Parameters.AddWithValue("@UnName", UnName);

            adapter.Fill(DataTable);
            return (this.DataTable);
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedActiveUniversityByCounId(int CounId, String UnName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUniversityConfirmed", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CounId", CounId);
            adapter.SelectCommand.Parameters.AddWithValue("@UnName", UnName);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", 0);

            adapter.Fill(DataTable);
            return (this.DataTable);
        }

        public void UpdateMemberLicenceByUnId(int UnId)
        {
            SqlCommand Command = new SqlCommand("spUpdateMemberLicenceByUnId", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@UnId", UnId);
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

    }
}
