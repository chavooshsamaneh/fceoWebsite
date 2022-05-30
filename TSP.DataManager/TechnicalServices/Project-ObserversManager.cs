using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class Project_ObserversManager : BaseObject
    {
        public Project_ObserversManager()
            : base()
        {

        }
        public Project_ObserversManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProject_Observers);
        }

        public static Permission GetUserPermissionObserverSelect(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectObserverSelected);
        }
        public static Permission GetUserPermissionObserverPayment(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ObserverPayment);
        }

        public static Permission GetUserPermissionPrintMunicipalityObsPermit(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintMunicipalityObsPermit);
        }

        public static Permission GetUserPermissionRejectObserverSelectByNezam(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.RejectObserverSelectByNezam);
        }
        public static Permission GetUserPermissionChooseCoordinatorObserver(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ChooseCoordinatorObserver);
        }
        public static Permission GetUserPermissionTSNewObserverKardan(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSNewObserverKardan);
        }
        public static Permission GetUserPermissionReportObserverWage(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSReport_ObserverWage);
        }

        public static Permission GetUserPermissionTSSaveObserverWithOutCondition(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSSaveObserverWithOutCondition);
        }
        public static Permission GetUserPermissionTSDeleteObSelect(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSObserverselectDelete);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectObservers";
            tableMapping.ColumnMappings.Add("ProjectObserversId", "ProjectObserversId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("PrjReId", "PrjReId");
            tableMapping.ColumnMappings.Add("MemberTypeId", "MemberTypeId");
            tableMapping.ColumnMappings.Add("MeOfficeOthPEngOId", "MeOfficeOthPEngOId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("InActiveDate", "InActiveDate");
            tableMapping.ColumnMappings.Add("ObserversTypeId", "ObserversTypeId");
            tableMapping.ColumnMappings.Add("Payed", "Payed");
            tableMapping.ColumnMappings.Add("IsMother", "IsMother");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("NezamShare", "NezamShare");
            tableMapping.ColumnMappings.Add("ObserverShare", "ObserverShare");
            tableMapping.ColumnMappings.Add("InsuranceShare", "InsuranceShare");
            tableMapping.ColumnMappings.Add("PriceArchiveItemDetailId", "PriceArchiveItemDetailId");
            tableMapping.ColumnMappings.Add("PriceArchiveId", "PriceArchiveId");
            tableMapping.ColumnMappings.Add("PayeDate", "PayeDate");
            tableMapping.ColumnMappings.Add("PayFivePercent", "PayFivePercent");
            tableMapping.ColumnMappings.Add("Year", "Year");
            tableMapping.ColumnMappings.Add("ObsWorkReqChangeId", "ObsWorkReqChangeId");
            tableMapping.ColumnMappings.Add("IsExteraFloor", "IsExteraFloor");
            tableMapping.ColumnMappings.Add("NezamKardanShare", "NezamKardanShare");



            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectObservers";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectObserversId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MemberTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@NotProjectStatusId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsMother", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@DateOfTody", Utility.GetDateOfToday());


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProjectObservers";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProjectObserversId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectObserversId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProjectObservers";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObserversTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObserversTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Payed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMother", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMother", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NezamShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NezamShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InsuranceShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InsuranceShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NezamKardanShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NezamKardanShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriceArchiveItemDetailId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveItemDetailId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriceArchiveId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PayeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayFivePercent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "PayFivePercent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Year", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObsWorkReqChangeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqChangeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExteraFloor", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExteraFloor", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProjectObservers";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MemberTypeId", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MemberTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeOfficeOthPEngOId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeOfficeOthPEngOId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObserversTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObserversTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payed", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Payed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMother", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMother", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProjectObserversId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectObserversId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectObserversId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ProjectObserversId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NezamShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NezamShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InsuranceShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InsuranceShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NezamKardanShare", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NezamKardanShare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriceArchiveItemDetailId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveItemDetailId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriceArchiveId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayeDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PayeDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayFivePercent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "PayFivePercent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Year", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObsWorkReqChangeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqChangeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExteraFloor", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExteraFloor", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectObserversDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByProjectObserversId(int ProjectObserversId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@ProjectObserversId"].Value = ProjectObserversId;
            this.Adapter.SelectCommand.Parameters["@DateOfTody"].Value = Utility.GetDateOfToday();
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@DateOfTody"].Value = Utility.GetDateOfToday();
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectIdAndRequestId(int ProjectId, int PrjReId, int InActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjReId"].Value = PrjReId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            this.Adapter.SelectCommand.Parameters["@DateOfTody"].Value = Utility.GetDateOfToday();

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectIdAndRequestId(int ProjectId, int PrjReId)
        {
            return FindByProjectIdAndRequestId(ProjectId, PrjReId, -1);
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
        public void FindByMemberIdTypeId(int MeOfficeOthPEngOId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByMemberIdTypeId(int ProjectId, int MeOfficeOthPEngOId, int MemberTypeId, int InActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByMemberIdTypeId(int ProjectId, int MeOfficeOthPEngOId, int MemberTypeId)
        {
            FindByMemberIdTypeId(ProjectId, MeOfficeOthPEngOId, MemberTypeId, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindNotEndByMemberIdTypeId(int MeOfficeOthPEngOId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeOfficeOthPEngOId"].Value = MeOfficeOthPEngOId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            this.Adapter.SelectCommand.Parameters["@NotProjectStatusId"].Value = (int)TSP.DataManager.TSProjectStatus.End;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPrjReId(int PrjReId, int InActive)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserversByPrjReId", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            if (PrjReId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            if (InActive != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
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

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectObserversForManagmentPage(int ProjectId, int PrjReId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserversForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", -1);
            adapter.Fill(this.DataTable);

            return this.DataTable;
        }
        public void FindObsMother(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@IsMother"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByProjectIdAndMemberTypeId(int ProjectId, int MemberTypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@MemberTypeId"].Value = MemberTypeId;
            Fill();
        }
        public DataView SelectProjectObserverReport(int ProjectId, int PrjReId, int MemberTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserversReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);

            return dt.DefaultView;
        }
        public DataView SelectProjectObserverReport(int ProjectId, int MemberTypeId)
        {
            return SelectProjectObserverReport(ProjectId, -1, MemberTypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="FromDateDecreased"></param>
        /// <param name="ToDateDecreased"></param>
        /// <param name="ProjectStatusId"></param>
        /// <param name="IsillInfo">0:Donot fill info,1:Fill info</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectObserverWageReport(int projectId, string RegisteredNo, int MeId, int ListNo, string FromDateDecreased, string ToDateDecreased,int AgentId)
        {
            DataTable dt = new System.Data.DataTable();
            if (FromDateDecreased == "1" && ToDateDecreased == "2" && projectId == -1 && RegisteredNo == "%" && MeId == -1 && ListNo == -1)
                return new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserverWageReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProjectObserver", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateDecreased", FromDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateDecreased", ToDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@RegisteredNo", RegisteredNo);
            adapter.SelectCommand.Parameters.AddWithValue("@projectId", projectId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ListNo", ListNo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectProjectObserverProjectsList(string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
            , string FromDateBuildingsLicenses, string ToDateBuildingsLicenses,
             int ProjectStatusId, int IsillInfo, int projectId, string RegisteredNo
            , string ToDateObsPayed, string FromDateObsPayed, int MeId, int IsPayed)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OwnerName");
            dt.Columns.Add("RegisteredNo");
            dt.Columns.Add("ProjectId");
            if (IsillInfo == 0)
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserverWageMaterReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateDecreased", FromDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateDecreased", ToDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateAccounting", FromDateAccounting);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateAccounting", ToDateAccounting);

            adapter.SelectCommand.Parameters.AddWithValue("@FromDateBuildingsLicenses", FromDateBuildingsLicenses);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateBuildingsLicenses", ToDateBuildingsLicenses);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectStatusId", ProjectStatusId);

            adapter.SelectCommand.Parameters.AddWithValue("@projectId", projectId);

            adapter.SelectCommand.Parameters.AddWithValue("@RegisteredNo", RegisteredNo);

            adapter.SelectCommand.Parameters.AddWithValue("@ToDateObsPayed", ToDateObsPayed);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateObsPayed", FromDateObsPayed);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsPayed", IsPayed);

            adapter.Fill(dt);
            return dt;
        }

        public void SetObserverCoordinator(int ProjectId, int ProjectObserversId, Boolean IsMother)
        {
            SqlCommand cmd = new SqlCommand("SetObserverCoordinator", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@ProjectObserversId", ProjectObserversId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@IsMother", Convert.ToInt32(IsMother));
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }

        }
        public int SelectTSProjectObserverCountForProjectUserControl(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverCountForProjectUserControl", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
                return 0;
            else
                return Convert.ToInt32(dt.Rows[0]["CountObservers"]);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectObserversForCapacityRelease(int ProjectId, int MeOfficeOthPEngOId, int InActive)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserversForCapacityRelease", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive); 
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.Fill(dt);

            return dt;
        }

        public Boolean CheckObserverWasInThePreRequest(int PrjReId, int MeOfficeOthPEngOId, int MemberTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("CheckObserverWasInThePreRequest", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.Fill(dt);

            if (dt.Rows.Count > 0) return true;
            else return false;
        }
    }
}
