using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
public class UsedCapacityProjectCapacityDecrement
{
    public double UsedCapacityCountProject = 0;
    public double UsedCapacitySumCapacityDecrement = 0;
}
namespace TSP.DataManager.TechnicalServices
{

    public class ProjectCapacityDecrementManager : BaseObject
    {

        public ProjectCapacityDecrementManager()
            : base()
        {

        }
        public ProjectCapacityDecrementManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectCapacityDecrement);
        }
        public static Permission GetUserPermissionKardanReport(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSKardanReport);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectCapacityDecrement";
            tableMapping.ColumnMappings.Add("PrjCapacityDecrementId", "PrjCapacityDecrementId");
            tableMapping.ColumnMappings.Add("CapacityDecrement", "CapacityDecrement");
            tableMapping.ColumnMappings.Add("Wage", "Wage");
            tableMapping.ColumnMappings.Add("ProjectIngridientTypeId", "ProjectIngridientTypeId");
            tableMapping.ColumnMappings.Add("PrjImpObsDsgnId", "PrjImpObsDsgnId");
            tableMapping.ColumnMappings.Add("IsFree", "IsFree");
            tableMapping.ColumnMappings.Add("FreeDate", "FreeDate");
            tableMapping.ColumnMappings.Add("IsDecreased", "IsDecreased");
            tableMapping.ColumnMappings.Add("DecreasedDate", "DecreasedDate");
            tableMapping.ColumnMappings.Add("OfficeId", "OfficeId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("MeOfficeOthPEngOId", "MeOfficeOthPEngOId");
            tableMapping.ColumnMappings.Add("MeOfficeOthPEngOTypeId", "MeOfficeOthPEngOTypeId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("ActivityIngridientTypeId", "ActivityIngridientTypeId");
            tableMapping.ColumnMappings.Add("IsFine", "IsFine");
            tableMapping.ColumnMappings.Add("FineExpireDate", "FineExpireDate");
            tableMapping.ColumnMappings.Add("IsWorkFree", "IsWorkFree");
            tableMapping.ColumnMappings.Add("WorkFreeDate", "WorkFreeDate");
            tableMapping.ColumnMappings.Add("SaveWithOutCondition", "SaveWithOutCondition");


            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectCapacityDecrement";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PrjCapacityDecrementId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectIngridientTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjImpObsDsgnId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsFree", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@IsDecreased", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfficeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Date", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@MemberTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectCount", System.Data.SqlDbType.Bit);
            this.Adapter.SelectCommand.Parameters.Add("@ActivityIngridientTypeId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProjectCapacityDecrement";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrjCapacityDecrementId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjCapacityDecrementId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProjectCapacityDecrement";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityDecrement", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityDecrement", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Wage", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Wage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjImpObsDsgnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpObsDsgnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFree", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFree", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FreeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FreeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDecreased", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDecreased", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DecreasedDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DecreasedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityIngridientTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFine", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFine", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FineExpireDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FineExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsWorkFree", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsWorkFree", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkFreeDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkFreeDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SaveWithOutCondition", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SaveWithOutCondition", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProjectCapacityDecrement";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityDecrement", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityDecrement", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Wage", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Wage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjImpObsDsgnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpObsDsgnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFree", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFree", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FreeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FreeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDecreased", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDecreased", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DecreasedDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DecreasedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrjCapacityDecrementId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjCapacityDecrementId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjCapacityDecrementId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PrjCapacityDecrementId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityIngridientTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFine", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFine", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FineExpireDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FineExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsWorkFree", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsWorkFree", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkFreeDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkFreeDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SaveWithOutCondition", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SaveWithOutCondition", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectCapacityDecrementDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByPrjCapacityDecrementId(int PrjCapacityDecrementId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrjCapacityDecrementId"].Value = PrjCapacityDecrementId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByProjectIngridientTypeId(int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindNotFreeByProjectIngridientTypeId(int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindNotFreeByPrjImpObsDsgnId(int PrjImpObsDsgnId, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByProjectIdAndIngridientTypeId(int ProjectId, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByProjectIdAndPrjImpObsDsgnId(int ProjectId, int PrjImpObsDsgnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            Fill();
        }

        public void FindByPrjImpObsDsgnAndIngridientTypeId(int ProjectId, int PrjImpObsDsgnId, int ProjectIngridientTypeId, int ActivityIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@ActivityIngridientTypeId"].Value = ActivityIngridientTypeId;
            Fill();
        }

        public void FindByPrjImpObsDsgnIdAndIngridientTypeId(int PrjImpObsDsgnId, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;

            Fill();
        }

        public void FindByOfficeId(int OfficeId, int ProjectIngridientTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfficeId"].Value = OfficeId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            Fill();
        }

        public void FindUsedCapacity(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            Fill();
        }

        /// <summary>
        /// محاسبه مجموع کارکرد شخص
        /// </summary>
        /// <param name="MeOfficeOthPEngOId"></param>
        /// <param name="ProjectIngridientTypeId">برای بدست آوردن متراژ کارکرد</param>
        /// <param name="MeOfficeOthPEngOTypeId">برای بدست آوردن تعداد کار</param>
        /// <param name="PrjImpObsDsgnId"></param>
        /// <param name="FromDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="IsFree">ظرفیت آزاد نشده:0</param>
        /// <param name="IsDecreased">کسر شده:1</param>
        /// <returns></returns>
        public UsedCapacityProjectCapacityDecrement FindSumUsedCapacity(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int ActivityIngridientTypeIdForCounntWork, int MeOfficeOthPEngOTypeId, int PrjImpObsDsgnId, string FromDate, string EndDate, int IsFree, int IsDecreased, int Under400Meter, int ProjectCitId, int DiscountPercentCode, int DiscountPercentCodeException)
        {
            //*************متراژ کارکرد************
            UsedCapacityProjectCapacityDecrement UsedCap = new UsedCapacityProjectCapacityDecrement();
            UsedCap.UsedCapacityCountProject = 0;
            UsedCap.UsedCapacitySumCapacityDecrement = 0;
            DataTable dtResult = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("GetTSProjectCapacityDecrementSumationCapacity", this.Connection);//GetTSProjectCapacityDecrementSumation
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOTypeId", MeOfficeOthPEngOTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsFree", IsFree);
            adapter.SelectCommand.Parameters.AddWithValue("@IsDecreased", IsDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjImpObsDsgnId", PrjImpObsDsgnId);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Under400Meter", Under400Meter);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectCitId", ProjectCitId);
            adapter.SelectCommand.Parameters.AddWithValue("@DiscountPercentCode", DiscountPercentCode);
            adapter.SelectCommand.Parameters.AddWithValue("@DiscountPercentCodeException", DiscountPercentCodeException);


            adapter.Fill(dtResult);
            if (dtResult.Rows.Count != 0)
            {
                UsedCap.UsedCapacitySumCapacityDecrement = Convert.ToDouble(dtResult.Rows[0]["SumCapacityDecrement"]);
            }
            //************تعداد کار**************
            DataTable dtResultPrjCount = new DataTable();
            SqlDataAdapter adapterPrjCount = new SqlDataAdapter("GetTSProjectCapacityDecrementCountProject", this.Connection);
            adapterPrjCount.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapterPrjCount.SelectCommand.Transaction = this.Transaction;
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@ActivityIngridientTypeId", ActivityIngridientTypeIdForCounntWork);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOTypeId", MeOfficeOthPEngOTypeId);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@IsWorkFree", IsFree);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@IsDecreased", IsDecreased);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@PrjImpObsDsgnId", PrjImpObsDsgnId);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@Date", FromDate);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@Under400Meter", Under400Meter);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@ProjectCitId", ProjectCitId);
            adapterPrjCount.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());


            adapterPrjCount.Fill(dtResultPrjCount);
            if (dtResultPrjCount.Rows.Count != 0)
            {
                UsedCap.UsedCapacityCountProject = Convert.ToDouble(dtResultPrjCount.Rows[0]["CountProject"]);
            }

            return UsedCap;
        }

        public void FindUsedCapacityPerStage(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSUsedCapacity", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.Fill(this.DataTable);
        }

        public void FindUsedCapacityByDate(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId, string StartDate, string EndDate)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = StartDate;
            this.Adapter.SelectCommand.Parameters["@EndDate"].Value = EndDate;
            Fill();
        }

        public int FindProjectNumByDate(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId, string StartDate, string EndDate)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@Date"].Value = StartDate;
            this.Adapter.SelectCommand.Parameters["@EndDate"].Value = EndDate;
            this.Adapter.SelectCommand.Parameters["@ProjectCount"].Value = 1;
            Fill();
            return this.DataTable.Rows.Count;
        }

        public void FindReservedCapacity(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 0;
            Fill();
        }

        public void FindOfficeCapacity(int OfficeId, int ProjectIngridientTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectCapacityDecrementByOffice", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@OfficeId", OfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsFree", 0);
            adapter.Fill(this.DataTable);
        }

        public void FindOfficeCapacity(int OfficeId, int ProjectIngridientTypeId, string Date, string EndDate)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectCapacityDecrementByOffice", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@OfficeId", OfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.Fill(this.DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportProjectCapacityDecrementForKardan(int AgentId, int ProjectId, string OtpCode, string FirstName, string LastName)
        {
            if (AgentId == -1 && ProjectId == -1 && OtpCode == "%" && FirstName == "%" && LastName == "%")
                return new DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("ReportProjectCapacityDecrementForKardan", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@OtpCode", OtpCode);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectCapacityDecrementByMember(int MeId, int ProjectId, int ActivityIngridientTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectCapacityDecrementByMember", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ActivityIngridientTypeId", ActivityIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOTypeId", (int)TSP.DataManager.TSMemberType.Member);


            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectDesignerForFreeWorkCount(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("selectDesignerForFreeWorkCount", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);


            adapter.Fill(dt);
            return dt;
        }


    }
}
