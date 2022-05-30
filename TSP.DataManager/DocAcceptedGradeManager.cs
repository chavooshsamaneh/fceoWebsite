using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class DocAcceptedGradeManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.GradeMajorResponsibility);
        }

        protected override void InitAdapter()
        {


            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DocAcceptedGrade";
            tableMapping.ColumnMappings.Add("GMRId", "GMRId");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            tableMapping.ColumnMappings.Add("MjId", "MjId");
            tableMapping.ColumnMappings.Add("ResId", "ResId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocAcceptedGrade";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GMRId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ResId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocAcceptedGrade";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_GMRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocAcceptedGrade";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocAcceptedGrade";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_GMRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GMRId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.DocumentDataSet.DocAcceptedGradeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int GMRId)
        {
            this.Adapter.SelectCommand.Parameters["@GMRId"].Value = GMRId;
            this.Adapter.SelectCommand.Parameters["@GrdId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@MjId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@ResId"].Value = -1;
            Fill();
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedGrade(int MjId, int GrdId, int ResId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocAcceptedGrade", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrdId", SqlDbType.Int, 4, "GrdId").Value = GrdId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@Inactive", SqlDbType.Int, 4, "Inactive").Value = -1;
            
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectConfirmedGrade(int MjId, int GrdId, int ResId, int Inactive)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocAcceptedGrade", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrdId", SqlDbType.Int, 4, "GrdId").Value = GrdId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@Inactive", SqlDbType.Int, 4, "Inactive").Value = Inactive;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocAcceptedGradeByMajorParent(int MjParentId, int GrdId, int ResId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocAcceptedGradeByMajorParent", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrdId", SqlDbType.Int, 4, "GrdId").Value = GrdId;
            adapter.SelectCommand.Parameters.Add("@MjParentId", SqlDbType.Int, 4, "MjParentId").Value = MjParentId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@Inactive", SqlDbType.Int, 4, "Inactive").Value = 0;

            adapter.Fill(dt);
            return (dt);
        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMajor(int MjId, int GrdId)
        {
           return SelectConfirmedGrade(MjId, GrdId, -1);
           
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMajor(int MjId, int GrdId,int Inactive)
        {
            return SelectConfirmedGrade(MjId, GrdId, -1,Inactive);

        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMajor(int MjId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MjId"].Value = MjId;
            Fill();
            return this.DataTable;

        }

        /// <summary>
        /// برای ارتقاء و تغییر پایه و صلاحیت از این تابع استفاده می شود.
        /// </summary>
        /// <param name="MjId"></param>
        /// <param name="ObsGrdId">یک پایه بالاتر از پایه جاری فرد</param>
        /// <param name="DesGrdId">یک پایه بالاتر از پایه جاری فرد</param>
        /// <param name="ImpGrdId">یک پایه بالاتر از پایه جاری فرد</param>
        /// <returns>یک پایه بالاتر را نسبت به صلاحیت بر می گرداند</returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectGradForUpgrade(int MjId, int ObsGrdId, int DesGrdId, int ImpGrdId, int MappingGrdId, int TrafficGrdId, int UrbanismGrdId, int GasGrdId)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocAcceptedGradeForUpGrade", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.AddWithValue("@ObsGrdId", ObsGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@DesGrdId", DesGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@ImpGrdId", ImpGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@MappingGradeId", MappingGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@TrafficGradeId", TrafficGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@UrbanismGradeId", UrbanismGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@GasGrdId", GasGrdId);

            adapter.SelectCommand.Parameters.AddWithValue("@ResObsId", (int)DocumentResponsibilityType.Observation);
            adapter.SelectCommand.Parameters.AddWithValue("@ResDesId", (int)DocumentResponsibilityType.Design);
            adapter.SelectCommand.Parameters.AddWithValue("@ResImpId", (int)DocumentResponsibilityType.Implement);
            adapter.SelectCommand.Parameters.AddWithValue("@ResMappingId", (int)DocumentResponsibilityType.Mapping);
            adapter.SelectCommand.Parameters.AddWithValue("@ResTrafficId", (int)DocumentResponsibilityType.Traffic);
            adapter.SelectCommand.Parameters.AddWithValue("@ResUrbanismId", (int)DocumentResponsibilityType.Urbanism);
            adapter.SelectCommand.Parameters.AddWithValue("@ResGasId", (int)DocumentResponsibilityType.Gas);
            

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectGradForUpgrade(int MjId, int ObsGrdId, int DesGrdId, int ImpGrdId, int MappingGrdId, int TrafficGrdId, int UrbanismGrdId)
        {
            return SelectGradForUpgrade(MjId, ObsGrdId, DesGrdId, ImpGrdId, MappingGrdId, TrafficGrdId, UrbanismGrdId, -1);
        }
    }
}
