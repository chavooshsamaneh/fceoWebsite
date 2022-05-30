using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class ConditionalCapacityManager : BaseObject
    {
        public ConditionalCapacityManager()
            : base()
        {
        }
        public ConditionalCapacityManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSConditionalCapacity);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSConditionalCapacity";
            tableMapping.ColumnMappings.Add("ConditionalCapacityId", "ConditionalCapacityId");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("ReasonId", "ReasonId");
            tableMapping.ColumnMappings.Add("Capacity", "Capacity");
            tableMapping.ColumnMappings.Add("FromDate", "FromDate");
            tableMapping.ColumnMappings.Add("ToDate", "ToDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("MemberTypeId", "MemberTypeId");
            tableMapping.ColumnMappings.Add("MeOfficeEngOId", "MeOfficeEngOId");
            tableMapping.ColumnMappings.Add("ProjectIngridientTypeId", "ProjectIngridientTypeId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSConditionalCapacity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ConditionalCapacityId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeOfficeEngOId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Date", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectIngridientTypeId", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ReasonId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@StartDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@IsDecreased", System.Data.SqlDbType.NChar);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSConditionalCapacity";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ConditionalCapacityId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ConditionalCapacityId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSConditionalCapacity";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReasonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReasonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Capacity", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Capacity", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FromDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FromDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ToDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ToDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSConditionalCapacity";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReasonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReasonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Capacity", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Capacity", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FromDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FromDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ToDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ToDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ConditionalCapacityId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ConditionalCapacityId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConditionalCapacityId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ConditionalCapacityId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSConditionalCapacityDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByConditionalCapacityId(Int64 ConditionalCapacityId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ConditionalCapacityId"].Value = ConditionalCapacityId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMeOfficeEngOId(int MeOfficeEngOId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeEngOId"].Value = MeOfficeEngOId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMeOfficeEngOId(int MeOfficeEngOId, string Date, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeEngOId"].Value = MeOfficeEngOId;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = Date;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMeIdAndIsDecreased(int MeOfficeEngOId, string Date, int ProjectIngridientTypeId, int IsDecreased)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeEngOId"].Value = MeOfficeEngOId;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = Date;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = IsDecreased;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMeOfficeEngOId(int MeOfficeEngOId, string StartDate, string EndDate, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeEngOId"].Value = MeOfficeEngOId;
            this.Adapter.SelectCommand.Parameters["@StartDate"].Value = StartDate;
            this.Adapter.SelectCommand.Parameters["@EndDate"].Value = EndDate;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = 1;
            //this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindReasonIdAndToDate(int ReasonId, string ToDate)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ReasonId"].Value = ReasonId;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            Fill();
            return this.DataTable;
        }

        public void InsertGroupConditionalCapacity( string ReasonId, string Capacity, string FromDate, string ToDate,
                                                   string Description, int MemberTypeId, int UserId, string MjId, string ProjectIngridientTypeId
                                                    , int GrdObsId, int GrdImpId, int GrdDesId, int GrdUrbanismId, int GrdTrafficId, int GrdMappingId)
        {
            SqlCommand Command = new SqlCommand("spInsertGroupConditionalCapacity", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            //Command.Parameters.AddWithValue("@LetterNo", LetterNo);
            //Command.Parameters.AddWithValue("@LetterDate", LetterDate);
            Command.Parameters.AddWithValue("@ReasonId", ReasonId);
            Command.Parameters.AddWithValue("@Capacity", Capacity);
            Command.Parameters.AddWithValue("@FromDate", FromDate);
            Command.Parameters.AddWithValue("@ToDate", ToDate);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            Command.Parameters.AddWithValue("@UserId", UserId);
            Command.Parameters.AddWithValue("@MjId", MjId);
            Command.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            Command.Parameters.AddWithValue("@GrdObsId", GrdObsId);
            Command.Parameters.AddWithValue("@GrdImpId", GrdImpId);
            Command.Parameters.AddWithValue("@GrdDesId", GrdDesId);
            Command.Parameters.AddWithValue("@GrdUrbanismId", GrdUrbanismId);
            Command.Parameters.AddWithValue("@GrdTrafficId", GrdTrafficId);
            Command.Parameters.AddWithValue("@GrdMappingId", GrdMappingId);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception ex) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectGroupConditionalCapacity(int GrdImpId, int GrdObsId, int GrdDesId, int GrdUrbanismId, int GrdTrafficId, int GrdMappingId, string MjId, string ReasonId, string StartDate, string EndDate)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectGroupConditionalCapacity";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            adapter.SelectCommand.Parameters.AddWithValue("@ReasonId", ReasonId);
            adapter.SelectCommand.Parameters.AddWithValue("@StartDate", StartDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdImpId", GrdImpId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdObsId", GrdObsId);            
            adapter.SelectCommand.Parameters.AddWithValue("@GrdDesId", GrdDesId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdUrbanismId", GrdUrbanismId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdTrafficId", GrdTrafficId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdMappingId", GrdMappingId);

            adapter.Fill(dt);
            return (dt);
        }
        /// <summary>
        /// مجموع ظرفیتی که به شخص اضافه می شود را بر می گرداند
        /// میزان کسر شده را در پروسجر محاسبه کرده ایم
        /// </summary>
        /// <param name="MeOfficeEngOId"></param>
        /// <param name="Date"></param>
        /// <param name="ProjectIngridientTypeId"></param>
        /// <returns></returns>
        public  int SelectTSConditionalCapacitySum(int MeOfficeEngOId, string Date, int ProjectIngridientTypeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectTSConditionalCapacitySum";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeEngOId", MeOfficeEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.Fill(dt);
            return (dt.Rows.Count>0? Convert.ToInt32(dt.Rows[0]["SumCapacity"]):0);
        }
    }
}
