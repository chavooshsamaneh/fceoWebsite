using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class ProjectIngridientMajorsManager : BaseObject
    {
        public ProjectIngridientMajorsManager()
            : base()
        {

        }
        public ProjectIngridientMajorsManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TsProjectIngridientMajors);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";

            tableMapping.DataSetTable = "TS.ProjectIngridientMajors";
            tableMapping.ColumnMappings.Add("ProjectIngridientMajorsId", "ProjectIngridientMajorsId");
            tableMapping.ColumnMappings.Add("ProjectIngridientTypeId", "ProjectIngridientTypeId");
            tableMapping.ColumnMappings.Add("ObserversPlansTypeId", "ObserversPlansTypeId");
            tableMapping.ColumnMappings.Add("MjId", "MjId");
            tableMapping.ColumnMappings.Add("GroupId", "GroupId");
            tableMapping.ColumnMappings.Add("Step", "Step");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("StructureSkeletonId", "StructureSkeletonId");
            tableMapping.ColumnMappings.Add("FoundationMin", "FoundationMin");
            tableMapping.ColumnMappings.Add("FoundationMax", "FoundationMax");
            tableMapping.ColumnMappings.Add("Percent", "Percent");
            tableMapping.ColumnMappings.Add("ObserverGroup", "ObserverGroup");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectIngridientMajors";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectIngridientMajorsId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectIngridientTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ObserversPlansTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@GroupId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MjId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@GrdId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProjectIngridientMajors";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectIngridientMajorsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientMajorsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProjectIngridientMajors";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserversPlansTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserversPlansTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Step", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Step", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMin", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMin", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMax", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMax", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Percent", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Percent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverGroup", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrdId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrdId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProjectIngridientMajors";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectIngridientTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserversPlansTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserversPlansTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Step", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Step", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMin", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMin", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMax", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMax", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Percent", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Percent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverGroup", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrdId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrdId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectIngridientMajorsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientMajorsId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectIngridientMajorsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectIngridientMajorsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectIngridientMajorsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByProjectIngridientMajorsId(int ProjectIngridientMajorsId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientMajorsId"].Value = ProjectIngridientMajorsId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPrjIngTypeIdAndObserversPlansTypeId(int ProjectIngridientTypeId, int ObserversPlansTypeId)
        {
            return FindByPrjIngTypeIdAndObserversPlansTypeId(ProjectIngridientTypeId, ObserversPlansTypeId, -1, -1,-1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPrjIngTypeIdAndObserversPlansTypeId(int ProjectIngridientTypeId, int ObserversPlansTypeId, int GroupId, int MjId, int GrdId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectIngridientTypeId"].Value = ProjectIngridientTypeId;
            this.Adapter.SelectCommand.Parameters["@ObserversPlansTypeId"].Value = ObserversPlansTypeId;
            this.Adapter.SelectCommand.Parameters["@GroupId"].Value = GroupId;
            this.Adapter.SelectCommand.Parameters["@GrdId"].Value = MjId;
            this.Adapter.SelectCommand.Parameters["@MjId"].Value = GrdId;
   
            Fill();
            return this.DataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectIngridientMajorsById(int ProjectIngridientTypeId, int ObserversPlansTypeId, int GroupId, int MjId,int GrdId,int StructureSkeletonId,int Foundation,int MjParentId)
        {
            ResetAllParameters();

            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectIngridientMajorsById", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObserversPlansTypeId", ObserversPlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@GroupId", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdId", GrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@StructureSkeletonId", StructureSkeletonId);
            adapter.SelectCommand.Parameters.AddWithValue("@Foundation", Foundation);
            adapter.SelectCommand.Parameters.AddWithValue("@MjParentId", MjParentId); 
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectIngridientMajorsById(int ProjectIngridientTypeId, int ObserversPlansTypeId, int GroupId, int MjId, int GrdId, int StructureSkeletonId, int Foundation)
        {
            return SelectTSProjectIngridientMajorsById(ProjectIngridientTypeId, ObserversPlansTypeId, GroupId, MjId, GrdId, StructureSkeletonId, Foundation,-1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectIngridientById(int ProjectIngridientTypeId, int GroupId, int StructureSkeletonId, int Foundation, int MjParentId)
        {
            ResetAllParameters();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectIngridientById", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientTypeId", ProjectIngridientTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObserversPlansTypeId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@GroupId", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@StructureSkeletonId", StructureSkeletonId);
            adapter.SelectCommand.Parameters.AddWithValue("@Foundation", Foundation);
            adapter.SelectCommand.Parameters.AddWithValue("@MjParentId", MjParentId);
            adapter.Fill(dt);
            return dt;
        }

        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetObserversPlansTypeDT(int ProjectIngridientTypeId)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ObserversPlansTypeId");
            dt.Columns.Add("Title");

            switch (ProjectIngridientTypeId)
            {
                case (int)TSProjectIngridientType.Observer:
                    ObserversTypeManager obsTypeManager = new ObserversTypeManager();
                    obsTypeManager.Fill();
                    dt = ObserversPlansTypeInsert(dt, obsTypeManager.DataTable, ProjectIngridientTypeId);
                    break;

                case (int)TSProjectIngridientType.Designer:
                    PlansTypeManager PlnTypeManager = new PlansTypeManager();
                    PlnTypeManager.Fill();
                    dt = ObserversPlansTypeInsert(dt, PlnTypeManager.DataTable, ProjectIngridientTypeId);
                    break;
            }

            return dt;
        }

        private DataTable ObserversPlansTypeInsert(DataTable dt, DataTable ManagerDT, int ProjectIngridientTypeId)
        {
            for (int i = 0; i < ManagerDT.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                if (ProjectIngridientTypeId == (int)TSProjectIngridientType.Observer)
                    dr["ObserversPlansTypeId"] = ManagerDT.Rows[i]["ObserversTypeId"];
                else if (ProjectIngridientTypeId == (int)TSProjectIngridientType.Designer)
                    dr["ObserversPlansTypeId"] = ManagerDT.Rows[i]["PlansTypeId"];
                dr["Title"] = ManagerDT.Rows[i]["Title"];

                dt.Rows.Add(dr);
            }
            return dt;
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectObserverMajorByProjectInfo(int GroupId, int ProjectFoundation, int StructureSkeletonId, string ExecptionMajorIdList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ObserverGroup");
            dt.Columns.Add("MajorList");
            dt.Columns.Add("MajorIdList");

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserverMajorByProjectInfo", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@GroupId", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectFoundation", ProjectFoundation);
            adapter.SelectCommand.Parameters.AddWithValue("@StructureSkeletonId", StructureSkeletonId);
            adapter.SelectCommand.Parameters.AddWithValue("@ExecptionMajorIdList", ExecptionMajorIdList); 
            adapter.Fill(dt);
            return dt;
        }

    }
}
