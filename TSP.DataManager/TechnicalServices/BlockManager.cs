using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager.TechnicalServices
{
    public class BlockManager : BaseObject
    {
        public BlockManager()
            : base()
        {

        }
        public BlockManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSBlock);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSBlock";
            tableMapping.ColumnMappings.Add("BlockId", "BlockId");
            tableMapping.ColumnMappings.Add("PlansMethodId", "PlansMethodId");
            tableMapping.ColumnMappings.Add("Foundation", "Foundation");
            tableMapping.ColumnMappings.Add("StageNum", "StageNum");
            tableMapping.ColumnMappings.Add("StructureSystemId", "StructureSystemId");
            tableMapping.ColumnMappings.Add("StructureSystem", "StructureSystem");
            tableMapping.ColumnMappings.Add("StructureSkeletonId", "StructureSkeletonId");
            tableMapping.ColumnMappings.Add("StructureSkeleton", "StructureSkeleton");
            tableMapping.ColumnMappings.Add("RoofTypeId", "RoofTypeId");
            tableMapping.ColumnMappings.Add("RoofType", "RoofType");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSBlock";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@BlockId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PlansMethodId", System.Data.SqlDbType.Int);
            //this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            //this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            //this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSBlock";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BlockId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "BlockId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSBlock";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansMethodId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansMethodId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Foundation", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Foundation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StageNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StageNum", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSystemId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSystemId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSystem", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSystem", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSkeletonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSkeleton", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSkeleton", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoofTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoofType", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RoofType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSBlock";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansMethodId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansMethodId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Foundation", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "Foundation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StageNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StageNum", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSystemId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSystemId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSystem", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSystem", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSkeletonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StructureSkeleton", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StructureSkeleton", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoofTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoofType", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RoofType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BlockId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "BlockId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BlockId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "BlockId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSBlockDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByBlockId(int BlockId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.CommandText = "spSelectTSBlock";
            this.Adapter.SelectCommand.Parameters["@BlockId"].Value= BlockId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPlansMethodId(int PlansMethodId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.CommandText = "spSelectTSBlock";
            this.Adapter.SelectCommand.Parameters["@PlansMethodId"].Value = PlansMethodId;
            Fill();
            return this.DataTable;
        }
 

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSBlockByProject(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSBlockByProject", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);
            return dt;
        }
        public int SelectTSBlockCountByProject(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSBlockByProject", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);
            return dt.Rows.Count;
        }
        
        public string SelectTSBlockStructureSkeleton(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSBlockStructureSkeleton", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["StructureSkeleton"].ToString();
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProjectAndPrjReId(int ProjectId, int PrjReId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSBlockByProjectAndPrjReIdFormanagmentPage", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);

            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSBlockByProjectAndPrjReId(int ProjectId, int PrjReId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSBlockByProjectAndPrjReId", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);

            adapter.Fill(dt);
            return dt;
        }
        public int SelectTSBlockCountByProjectAndPrjReId(int ProjectId, int PrjReId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSBlockCountByProjectAndPrjReId", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);

            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["cntBlock"]);
            }
        }
        public int GetMaxStageNumByRequest(int PrjReId)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "spSelectTSMaxStageNumByRequest";

            c.Parameters.AddWithValue("@PrjReId", PrjReId);

            c.Transaction = this.Transaction;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object MaxStageNum = c.ExecuteScalar();

            if (MaxStageNum == DBNull.Value)
                return 0;

            return Convert.ToInt32(MaxStageNum);
        }


        public int GetMaxStageNum(int ProjectId)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "spSelectTSMaxStageNum";

            c.Parameters.AddWithValue("@ProjectId", ProjectId);

            c.Transaction = this.Transaction;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object MaxStageNum = c.ExecuteScalar();

            if (MaxStageNum == DBNull.Value)
                return 0;

            return Convert.ToInt32(MaxStageNum);
        }


        public string CheckCurrentMaxStepByGroup(int PrjReId)
        {
            TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
            ArrayList ArrStepRange = new ArrayList();
            ArrStepRange = PriceArchiveStructureItemsManager.GetCurrentStepFromAndStepTo(PrjReId);
            int MaxStageNum = this.GetMaxStageNumByRequest(PrjReId);
            if (MaxStageNum < Convert.ToInt32(ArrStepRange[0]))
                return "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه بیشترین تعداد طبقات باید در محدوده "
                                        + ArrStepRange[0].ToString() + " تا " + ArrStepRange[1].ToString() + " طبقه باشد";

            if (Convert.ToInt32(ArrStepRange[1]) != -2)
            {
                if (MaxStageNum > Convert.ToInt32(ArrStepRange[1]))
                    return "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه بیشترین تعداد طبقات باید  "
                                 + ArrStepRange[0].ToString() + " به بالا باشد ";
            }
            return "";
        }

        public static Boolean DeleteBlock(int BlockId, TSP.DataManager.TransactionManager transact, Boolean IsTransactionComplete)
        {
            TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
            TSP.DataManager.TechnicalServices.BalconyManager BalconyManager = new TSP.DataManager.TechnicalServices.BalconyManager();
            TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = new TSP.DataManager.TechnicalServices.FoundationManager();
            TSP.DataManager.TechnicalServices.WallsManager WallsManager = new TSP.DataManager.TechnicalServices.WallsManager();
            TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = new TSP.DataManager.TechnicalServices.EntranceManager();
            transact.Add(BlockManager);
            transact.Add(BalconyManager);
            transact.Add(FoundationManager);
            transact.Add(WallsManager);
            transact.Add(EntranceManager);

            BlockManager.FindByBlockId(BlockId);
            if (BlockManager.Count != 1)
            {
                return false;
            }
            if (IsTransactionComplete)
                transact.BeginSave();
            EntranceManager.FindByBlockId(BlockId);
            if (EntranceManager.Count > 0)
            {
                int len = EntranceManager.Count;
                for (int i = 0; i < len; i++)
                    EntranceManager[0].Delete();
                EntranceManager.Save();
                EntranceManager.DataTable.AcceptChanges();
            }

            FoundationManager.FindByBlockId(BlockId);
            if (FoundationManager.Count > 0)
            {
                int len = FoundationManager.Count;
                for (int i = 0; i < len; i++)
                {
                    BalconyManager.FindByFoundationId(Convert.ToInt32(FoundationManager[0]["FoundationId"]));
                    if (BalconyManager.Count > 0)
                    {
                        int lenbalcony = BalconyManager.Count;
                        for (int j = 0; j < lenbalcony; j++)
                            BalconyManager[0].Delete();
                        BalconyManager.Save();
                        BalconyManager.DataTable.AcceptChanges();
                    }

                    FoundationManager[0].Delete();
                    FoundationManager.Save();
                    FoundationManager.DataTable.AcceptChanges();
                }
            }

            WallsManager.FindByBlockId(BlockId);
            if (WallsManager.Count > 0)
            {
                int len = WallsManager.Count;
                for (int i = 0; i < len; i++)
                    WallsManager[0].Delete();
                WallsManager.Save();
                WallsManager.DataTable.AcceptChanges();
            }

            BlockManager[0].Delete();
            BlockManager.Save();
            BlockManager.DataTable.AcceptChanges();
            if (IsTransactionComplete)
                transact.EndSave();
            return true;

        }
    }
}
