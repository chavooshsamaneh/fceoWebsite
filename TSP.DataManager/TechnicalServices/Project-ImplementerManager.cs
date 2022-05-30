using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace TSP.DataManager.TechnicalServices
{
    public class Project_ImplementerManager : BaseObject
    {
        public Project_ImplementerManager()
            : base()
        {

        }
        public Project_ImplementerManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProject_Implementer);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectImplementer";
            tableMapping.ColumnMappings.Add("PrjImpId", "PrjImpId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("PrjReId", "PrjReId");
            tableMapping.ColumnMappings.Add("MemberTypeId", "MemberTypeId");
            tableMapping.ColumnMappings.Add("MeOfficeId", "MeOfficeId");
            tableMapping.ColumnMappings.Add("IsOwner", "IsOwner");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("InActiveDate", "InActiveDate");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("IsMother", "IsMother");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.[spSelectTSProject-Implementer]";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PrjImpId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeOfficeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MemberTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@NotProjectStatusId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.[spDeleteTSProject-Implementer]";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrjImpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.[spInsertTSProject-Implementer]";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsOwner", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsOwner", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMother", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMother", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.[spUpdateTSProject-Implementer]";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsOwner", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsOwner", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMother", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMother", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PrjImpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjImpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjImpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PrjImpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectImplementerDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByPrjImpId(int PrjImpId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrjImpId"].Value = PrjImpId;
            Fill();
        }

        //public void FindByProjectAndImpId(int ProjectId, int ImplementerId)
        //{
        //    ResetAllParameters();
        //    this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
        //    this.Adapter.SelectCommand.Parameters["@ImplementerId"].Value = ImplementerId;

        //    Fill();
        //}

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            Fill();
            return this.DataTable;
        }

        public DataTable FindActivesByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectIdAndRequestId(int ProjectId, int PrjReId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjReId"].Value = PrjReId;

            Fill();
            return this.DataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByMemberIdTypeId(int MeOfficeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeId"].Value = MeOfficeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByMemberIdTypeId(int ProjectId, int MeOfficeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@MeOfficeId"].Value = MeOfficeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindNotEndByMemberIdTypeId(int MeOfficeId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeId"].Value = MeOfficeId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@NotProjectStatusId"].Value = (int)TSP.DataManager.TSProjectStatus.End;

            Fill();
        }

        public void FindImpMother(int ProjectId)
        {
            ResetAllParameters();

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProject-ImplementerMother", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(this.DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPrjReId(int PrjReId, int InActive)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProject-ImplementerByPrjReId", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            if (PrjReId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            if (InActive != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive); ;
            adapter.Fill(this.DataTable);

            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindConfirmedByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = 1;
            Fill();
            return this.DataTable;
        }
    }
}
