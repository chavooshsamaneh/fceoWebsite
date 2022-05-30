using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class PriceArchiveStructureItemsManager : TSP.DataManager.BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TS_PriceArchiveStructureItems);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TS.PriceArchiveStructureItems";
            tableMapping.ColumnMappings.Add("ItemId", "ItemId");
            tableMapping.ColumnMappings.Add("No", "No");
            tableMapping.ColumnMappings.Add("GroupId", "GroupId");
            tableMapping.ColumnMappings.Add("PriceArchiveId", "PriceArchiveId");
            tableMapping.ColumnMappings.Add("StepFrom", "StepFrom");
            tableMapping.ColumnMappings.Add("StepTo", "StepTo");
            tableMapping.ColumnMappings.Add("MaxCountUnits", "MaxCountUnits");
            tableMapping.ColumnMappings.Add("MaxInfrastructureArea", "MaxInfrastructureArea");
            tableMapping.ColumnMappings.Add("BuildCost", "BuildCost");
            tableMapping.ColumnMappings.Add("SumPercents", "SumPercents");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("CoordinatorPrice", "CoordinatorPrice");
            tableMapping.ColumnMappings.Add("StructureSkeletonId", "StructureSkeletonId");
            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSPriceArchiveStructureItems";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ItemId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GroupId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@PriceArchiveId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Step", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@StructureSkeletonId", System.Data.SqlDbType.Int, 4);
            
            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSPriceArchiveStructureItems";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ItemId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ItemId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSPriceArchiveStructureItems";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@No", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "No", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriceArchiveId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PriceArchiveId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StepFrom", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StepFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StepTo", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StepTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxCountUnits", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MaxCountUnits", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxInfrastructureArea", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MaxInfrastructureArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildCost", global::System.Data.SqlDbType.Money, 8, global::System.Data.ParameterDirection.Input, 19, 4, "BuildCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SumPercents", global::System.Data.SqlDbType.Decimal, 5, global::System.Data.ParameterDirection.Input, 5, 3, "SumPercents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CoordinatorPrice", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CoordinatorPrice", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSPriceArchiveStructureItems";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@No", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "No", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriceArchiveId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PriceArchiveId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StepFrom", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StepFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StepTo", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StepTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxCountUnits", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MaxCountUnits", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxInfrastructureArea", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MaxInfrastructureArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildCost", global::System.Data.SqlDbType.Money, 8, global::System.Data.ParameterDirection.Input, 19, 4, "BuildCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SumPercents", global::System.Data.SqlDbType.Decimal, 5, global::System.Data.ParameterDirection.Input, 5, 3, "SumPercents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CoordinatorPrice", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CoordinatorPrice", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ItemId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ItemId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ItemId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ItemId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet._TS_PriceArchiveStructureItemsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ItemId"].Value = Id;
            Fill();
        }

        public void FindByGroupId(int GroupId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@GroupId"].Value = GroupId;
            Fill();
        }

        public void FindByPriceArchiveId(int PriceArchiveId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = PriceArchiveId;
            Fill();
        }

        public void FindByPriceArchiveIdGroupIdStep(int PriceArchiveId, int GroupId, int Step)
        {
            FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, -1);
        }


        public void FindByPriceArchiveIdGroupIdStep(int PriceArchiveId, int GroupId, int Step,int StructureSkeletonId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = PriceArchiveId;
            this.Adapter.SelectCommand.Parameters["@GroupId"].Value = GroupId;
            this.Adapter.SelectCommand.Parameters["@Step"].Value = Step;
            this.Adapter.SelectCommand.Parameters["@StructureSkeletonId"].Value = StructureSkeletonId; 

            Fill();
        }
        public int GetGroupIdByFoundation(int PriceArchiveId, double Foundation)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = PriceArchiveId;
            Fill();

            this.CurrentFilter = Foundation + "<= MaxInfrastructureArea OR (StepTo is null AND MaxInfrastructureArea<" + Foundation+")";
            if (this.Count > 0)
                return Convert.ToInt32(this[0]["GroupId"]);

            return -1;
        }

        public int GetGroupIdByFoundationAndStage(int PriceArchiveId, int Stage, int Foundation)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = PriceArchiveId;
            Fill();

            this.CurrentFilter = Stage + "<= StepTo And " + Foundation + "<= MaxInfrastructureArea";
            if (this.Count > 0)
                return Convert.ToInt32(this[0]["GroupId"]);

            return -1;
        }

        public void FindByPriceArchiveIdAndGroupId(int PriceArchiveId, int GroupId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = PriceArchiveId;
            this.Adapter.SelectCommand.Parameters["@GroupId"].Value = GroupId;
            Fill();
        }

        //public int GetCurrentGroupIdByFoundation(double Foundation)
        //{
        //    int GroupId = -1;
        //    TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        //    int PriceArchiveId = PriceArchiveManager.FindCurrentPriceArchiveId();
        //    if (PriceArchiveId != -1)
        //        GroupId = this.GetGroupIdByFoundation(PriceArchiveId, Foundation);
        //    return GroupId;
        //}

        public int GetCurrentGroupIdByFoundation(double Foundation)
        {
            int GroupId = -1;
            TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
            int PriceArchiveId = PriceArchiveManager.FindCurrentPriceArchiveId();
            if (PriceArchiveId != -1)
                GroupId = this.GetGroupIdByFoundation(PriceArchiveId, Foundation);
            return GroupId;
        }

        /// <summary>
        /// ArrStepRange[0] = StepFrom , ArrStepRange[1] = StepTo
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ArrayList GetCurrentStepFromAndStepTo(int PrjReId)
        {
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();

            ArrayList ArrStepRange = new ArrayList();
            ArrStepRange.Add(-1);
            ArrStepRange.Add(-1);
            int GroupId = -1; double Foundation = -1;
       

            ProjectRequestManager.FindByCode(PrjReId);
            if (ProjectRequestManager.Count == 1)
            {
              
                Foundation = Convert.ToInt32(ProjectRequestManager[0]["Foundation"]);

                GroupId = this.GetCurrentGroupIdByFoundation(Foundation);
            }

            int PriceArchiveId = PriceArchiveManager.FindCurrentPriceArchiveId();

            if ((Foundation == -1) || (GroupId == -1) || (PriceArchiveId == -1))
                return ArrStepRange;

            this.FindByPriceArchiveIdAndGroupId(PriceArchiveId, GroupId);
            if (this.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(this[0]["StepFrom"]))
                    ArrStepRange[0] = Convert.ToInt32(this[0]["StepFrom"]);
                if (!Utility.IsDBNullOrNullValue(this[this.Count - 1]["StepTo"]))
                    ArrStepRange[1] = Convert.ToInt32(this[this.Count - 1]["StepTo"]);
                else ArrStepRange[1] = -2;
            }

            return ArrStepRange;
        }

       
    }
}
