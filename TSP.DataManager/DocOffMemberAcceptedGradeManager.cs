using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class DocOffMemberAcceptedGradeManager : BaseObject
    {
        public DocOffMemberAcceptedGradeManager()
            : base()
        {

        }
        public DocOffMemberAcceptedGradeManager(System.Data.DataSet ds)
            : base(ds)
        {

        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DocOffMemberAcceptedGrade";
            tableMapping.ColumnMappings.Add("MGId", "MGId");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            tableMapping.ColumnMappings.Add("ResId", "ResId");
            tableMapping.ColumnMappings.Add("TnReId", "TnReId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Status", "Status");
            this.Adapter.TableMappings.Add(tableMapping);
            
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocOffMemberAcceptedGrade";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MGId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = (int)TableCodes.OfficememberAcceptedGrade;
            this.Adapter.SelectCommand.Parameters.Add("@OtpId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@JustConfirm", SqlDbType.Int);
            

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocOffMemberAcceptedGrade";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MGId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MGId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocOffMemberAcceptedGrade";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TnReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TnReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocOffMemberAcceptedGrade";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TnReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TnReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MGId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MGId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MGId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MGId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.DocOffMemberAcceptedGradeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int MGId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MGId"].Value = MGId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOtpId(int OtpId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OtpId"].Value = OtpId;
            Fill();
            return this.DataTable;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOtpId(int OtpId, int InActive)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OtpId"].Value = OtpId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
            return this.DataTable;
        }
        

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTnReIdForTechnicianRequest(int TnReId, int OtpId,int JustConfirm)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOffMemberAcceptedGradeForTechnicianRequest", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OtpId", OtpId);
            adapter.SelectCommand.Parameters.AddWithValue("@TnReId", TnReId);
            adapter.SelectCommand.Parameters.AddWithValue("@JustConfirm", JustConfirm);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TableCodes.OfficememberAcceptedGrade);

            adapter.Fill(dt);
            return dt;
        }

        public DataTable FindByOtpIdAndResId(int OtpId, int ResId, int InActive)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOtherPersonAcceptedGrade", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OtpId", OtpId);
            adapter.SelectCommand.Parameters.AddWithValue("@ResId", ResId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.Fill(dt);
            return dt;
        }

        public int GetGradeId(int OtpId, int ResId)
        {
            DataTable dt = FindByOtpIdAndResId(OtpId, ResId, 0);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["GrdId"]);
            }
                
            else
                return 0;
        }

        public int GetMaxGradeId(int OtpId)
        {
            int Imp = GetGradeId(OtpId, (int)DocumentResponsibilityType.Implement);
            int Obs = GetGradeId(OtpId, (int)DocumentResponsibilityType.Observation);
            if (Obs == 0)
                return Imp;
            else if (Imp < Obs && Imp != 0)
                return Imp;
            else
                return Obs;

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectGradeByOtpId(int OtpId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocOffMemberAcceptedGradeByOtpId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OtpId", OtpId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TableCodes.OfficememberAcceptedGrade);
            adapter.Fill(dt);
            return dt;
        }
    }
}
