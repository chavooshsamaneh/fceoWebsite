using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager.TechnicalServices
{
    public class ProjectOfficeMembersManager : BaseObject
    {
        public ProjectOfficeMembersManager()
            : base()
        {

        }
        public ProjectOfficeMembersManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectOfficeMembers);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectOfficeMembers";
            tableMapping.ColumnMappings.Add("ProjectOfficeMembersId", "ProjectOfficeMembersId");
            tableMapping.ColumnMappings.Add("ProjectIngridientTypeId", "ProjectIngridientTypeId");
            tableMapping.ColumnMappings.Add("PrjImpObsDsgnId", "PrjImpObsDsgnId");
            tableMapping.ColumnMappings.Add("MemberTypeId", "MemberTypeId");
            tableMapping.ColumnMappings.Add("MeOthPId", "MeOthPId");
            tableMapping.ColumnMappings.Add("CapacityDecrement", "CapacityDecrement");
            tableMapping.ColumnMappings.Add("Wage", "Wage");
            tableMapping.ColumnMappings.Add("IsFree", "IsFree");
            tableMapping.ColumnMappings.Add("FreeDate", "FreeDate");
            tableMapping.ColumnMappings.Add("IsDecreased", "IsDecreased");
            tableMapping.ColumnMappings.Add("DecreasedDate", "DecreasedDate");
            tableMapping.ColumnMappings.Add("OfficeMemberTypeId", "OfficeMemberTypeId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectOfficeMembers";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectOfficeMembersId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectIngridientTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjImpObsDsgnId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeOthPId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsFree", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsDecreased", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Date", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@MemberTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectCount", System.Data.SqlDbType.Bit);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProjectOfficeMembers";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProjectOfficeMembersId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectOfficeMembersId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProjectOfficeMembers";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjImpObsDsgnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpObsDsgnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOthPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOthPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityDecrement", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityDecrement", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Wage", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Wage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFree", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFree", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FreeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FreeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDecreased", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDecreased", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DecreasedDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DecreasedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeMemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeMemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProjectOfficeMembers";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjImpObsDsgnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpObsDsgnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOthPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOthPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityDecrement", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityDecrement", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Wage", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Wage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFree", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFree", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FreeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FreeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDecreased", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDecreased", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DecreasedDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "DecreasedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeMemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeMemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProjectOfficeMembersId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectOfficeMembersId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectOfficeMembersId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ProjectOfficeMembersId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectOfficeMembersDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByProjectOfficeMembersId(int ProjectOfficeMembersId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectOfficeMembersId"].Value = ProjectOfficeMembersId;
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

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByIngridientTypeAndPrjImpObsDsgnId(int ProjectIngridientTypeId, int PrjImpObsDsgnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            Fill();
            return this.DataTable;
        }


        public void FindByIngridientTypeAndPrjImpObsDsgnId(int ProjectIngridientTypeId, int PrjImpObsDsgnId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@PrjImpObsDsgnId"].Value = PrjImpObsDsgnId;
            Fill();
        }

        public void FindUsedCapacity(int MeOthPId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOthPId"].Value = MeOthPId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 1;
            Fill();
        }

        public void FindUsedCapacityPerStage(int MeOfficeOthPEngOId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSOfficeMembersUsedCapacity", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeOthPId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.Fill(this.DataTable);
        }

        public void FindUsedCapacityByDate(int MeOthPId, int ProjectIngridientTypeId, int MemberTypeId, string StartDate, string EndDate)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOthPId"].Value = MeOthPId;
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

        public void FindReservedCapacity(int MeOthPId, int ProjectIngridientTypeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOthPId"].Value = MeOthPId;
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@IsFree"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsDecreased"].Value = 0;
            Fill();
        }

        public void SelectProjectMembers(int ProjectId, int ProjectIngridientTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectMembers", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.Fill(this.DataTable);
        }

        public void SelectProjectMembersByPrjImpObsDsgnId(int ProjectIngridientTypeId, int PrjImpObsDsgnId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectMembers", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjImpObsDsgnId", PrjImpObsDsgnId);
            adapter.Fill(this.DataTable);
        }

        public DataView SelectProjectMembersWithCapacity(int ProjectId, string PlansTypeId, int InActive, int PrjReId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectDesignerByExec", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProject_Designer", TableTypeManager.FindTtId(TableType.TSProject_Designer));
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.Fill(dt);
            return dt.DefaultView;
        }

        public DataView SelectProjectMembersWithCapacity(int ProjectId, string PlansTypeId)
        {
            return SelectProjectMembersWithCapacity(ProjectId, PlansTypeId, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportDesignerWage(int MeId,string FromDate,string ToDate,int CittId,string FirstName,string LastName)
        {
            DataTable dt= new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("ReportSearchTsDesignerWage", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@CittId", CittId);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.Fill(dt);
            return dt;
        }
         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportMemberWageByCity(int MeId,int ProjectIngridientTypeId)
        {
              DataTable dt= new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForMembersCapacityByMunicipality", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.Fill(dt);
            return dt; 
             //CitId, CitName,ProjectIngridientResName,SumWage
         }
      
    }
}
