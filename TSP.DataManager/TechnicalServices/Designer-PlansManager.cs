using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class Designer_PlansManager : BaseObject
    {
        public Designer_PlansManager()
            : base()
        {

        }
        public Designer_PlansManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSDesigner_Plans);
        }
        public static Permission GetUserPermissionChooseWorkYearForObserverAndDesign(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectChooseWorkYearForObserverAndDesign);
        }
        public static Permission GetUserPermissionTSSaveDesignerWithOutCondition(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSSaveDesignerWithOutCondition);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSDesignerPlans";
            tableMapping.ColumnMappings.Add("DesignerPlansId", "DesignerPlansId");
            tableMapping.ColumnMappings.Add("PrjDesignerId", "PrjDesignerId");
            tableMapping.ColumnMappings.Add("PlansId", "PlansId");
            tableMapping.ColumnMappings.Add("IsMaster", "IsMaster");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.[spSelectTSDesigner-Plans]";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@DesignerPlansId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjDesignerId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PlansId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MemberTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsMaster", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PlansTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProject_Designer", TableTypeManager.FindTtId(TableType.TSProject_Designer));
            this.Adapter.SelectCommand.Parameters.Add("@DesignerInAcive", System.Data.SqlDbType.Int);




            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.[spDeleteTSDesigner-Plans]";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DesignerPlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DesignerPlansId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.[spInsertTSDesigner-Plans]";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjDesignerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjDesignerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.[spUpdateTSDesigner-Plans]";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjDesignerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjDesignerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DesignerPlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DesignerPlansId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DesignerPlansId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "DesignerPlansId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSDesignerPlansDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByDesignerPlansId(int DesignerPlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DesignerPlansId"].Value = DesignerPlansId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByRequest(int ProjectId, int PrjReId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjReId"].Value = PrjReId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByDesignerId(int PrjDesignerId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrjDesignerId"].Value = PrjDesignerId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByPlansId(int PlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectDesignerByMajor(int ProjectId, int MjId, int IsMaster)
        {
            //  DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSDesigner-PlansByMajor";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@IsMaster", SqlDbType.Int, 4, "IsMaster").Value = IsMaster;

            adapter.Fill(DataTable);
            //  return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDesignerByPlansId(int PlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindActivesByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindActivesByPlansId(int PlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        public void FindPlansMaster(int PlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            this.Adapter.SelectCommand.Parameters["@IsMaster"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
        }

        public void FindByPrjDesignerIdAndPlansId(int PlansId, int PrjDesignerId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            this.Adapter.SelectCommand.Parameters["@PrjDesignerId"].Value = PrjDesignerId;
            Fill();
        }

        public DataTable FindMasterByLastVersion(int ProjectId, int PlansTypeId)
        {
            this.DataTable.Clear();
            PlansManager PlansManager = new PlansManager();
            PlansManager.SelectMaxVersion(ProjectId, 0, PlansTypeId, 1);
            if (PlansManager.Count > 0)
                FindPlansMaster(Convert.ToInt32(PlansManager[0]["PlansId"]));
            return this.DataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectDesignerByPlansId(int PlansId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansForByBlansId", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PlansId", PlansId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProject_Designer", TableTypeManager.FindTtId(TableType.TSProject_Designer));

            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectAndPlansTypeId(int ProjectId, int PlansTypeId, int PrjDesignerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansForDesignerPage", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjDesignerId", PrjDesignerId);

            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectAndPlansTypeId(int ProjectId, int PlansTypeId)
        {
            return FindByProjectAndPlansTypeId(ProjectId, PlansTypeId, -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectAndPlansTypeId(int PrjDesignerId)
        {
            return FindByProjectAndPlansTypeId(-1, -1, PrjDesignerId);
        }

        public DataTable FindPrjDesignerIdByDesignerPlansId(int DesignerPlansId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansByDesignerPlansId", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@DesignerPlansId", DesignerPlansId);

            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForDesignerPage(int ProjectId, int PrjReId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansForDesignerPage", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansByProjectDesigner(int PrjDesignerId, int ProjectId, int PrjReId,int PlansTypeId,int PlansId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSDesigner-PlansByProjectDesigner", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjDesignerId", PrjDesignerId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansId", PlansId);

            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// جهت استفاده در چاپ فیش مالی
        /// </summary>
        /// <param name="PrjDesignerId"></param>
        /// <returns></returns>
      [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForDesignerFish(int PrjDesignerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansForPrintFish", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjDesignerId", PrjDesignerId);
            adapter.Fill(dt);
            return dt;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForDesignerFish(int PlansId, int PrjDesignerId, int PlansTypeId, int ProjectId, int PrjReId, string PlansTypeIdList)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesigner-PlansForDesignerFish", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjDesignerId", PrjDesignerId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansId", PlansId);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeIdList", PlansTypeIdList);
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForDesignerFish(int ProjectId, int PlansTypeId, string PlansTypeIdList)
        {
            return SelectTSDesignerPlansForDesignerFish(-1, -1, PlansTypeId, ProjectId, -1, PlansTypeIdList);
        }
        #region SelectTSDesignerPlansForByPlanId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForByPlanId(int PlansId, int PrjDesignerId, int DesignerInAcive, int ProjectId, int DesignerMeId, int PrjReId)
        {
            if (PlansId <= 0 && ProjectId < 0)
            {
                return new DataTable();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSDesigner-PlansByPlan", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjDesignerId", PrjDesignerId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@DesignerInAcive", DesignerInAcive);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansId", PlansId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@DesignerMeId", DesignerMeId);

            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForByPlanId(int PlansId)
        {
            if (PlansId <= 0)
            {
                return new DataTable();
            }
            return SelectTSDesignerPlansForByPlanId(PlansId, -1, -1, -1,-1,-1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectActiveTSDesignerPlansForByPlanId(int PlansId)
        {
            if (PlansId <= 0)
            {
                return new DataTable();
            }
            return SelectTSDesignerPlansForByPlanId(PlansId, -1, 0, -1,-1,-1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectActiveTSDesignerPlansForByProjectId(int ProjectId, int MeId, int PrjReId)
        {
            return SelectTSDesignerPlansForByPlanId(-1, -1, 0, ProjectId, MeId, PrjReId);
        }
        #endregion
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansForDesignerFish(int PrjDesignerId, int PlansTypeId, int ProjectId, int PrjReId)
        {
            return SelectTSDesignerPlansForDesignerFish(-1, PrjDesignerId, PlansTypeId, ProjectId, PrjReId, "");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSDesignerPlansByPlanAndAccountingInfo(int PlansId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSDesigner-PlansByPlanAndAccountingInfo", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@DesignerInAcive", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansId", PlansId);

            adapter.Fill(dt);
            return dt;
        }


        public int DoNextTaskOfBankReply(int TableId, int UltId, int NmcId, int NmcIdType, int UserId, TransactionManager Trans)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new DataManager.WorkFlowStateManager();
            Trans.Add(WorkFlowStateManager);
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans); ;
            int NextTaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;
            int NextStepTaskId = -1;
            WorkFlowTaskManager.FindByTaskCode(NextTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                return -1;
            }
            NextStepTaskId = int.Parse(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskId"].ToString());
            string Url = "";
            string MsgContent = "";
            return WorkFlowStateManager.SendDocToNextStep(TableType, TableId, NextStepTaskId, "ارسال اتوماتیک نقشه توسط عضو جهت انتصاب بازبین", NmcId, NmcIdType, UserId, MsgContent, Url);
        }
    }
}
