using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TSP.DataManager
{
    public class UserRightManager:BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.UserRight);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblUserRight";
            tableMapping.ColumnMappings.Add("UrId", "UrId");
            tableMapping.ColumnMappings.Add("TtId", "TtId");
            tableMapping.ColumnMappings.Add("ParentId", "ParentId");
            tableMapping.ColumnMappings.Add("TtName", "TtName");
            tableMapping.ColumnMappings.Add("LoginId", "LoginId");
            tableMapping.ColumnMappings.Add("CanView", "CanView");
            tableMapping.ColumnMappings.Add("CanEdit", "CanEdit");
            tableMapping.ColumnMappings.Add("CanNew", "CanNew");
            tableMapping.ColumnMappings.Add("CanDelete", "CanDelete");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectUserRight";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //this.Adapter.SelectCommand.Parameters.Add("@UrId", System.Data.SqlDbType.Int);           
            this.Adapter.SelectCommand.Parameters.Add("@LoginId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TTCode", System.Data.SqlDbType.VarChar);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteUserRight";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertUserRight";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TtId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LoginId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LoginId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanView", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanView", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanEdit", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanEdit", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanNew", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanNew", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanDelete", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanDelete", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateUserRight";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TtId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LoginId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LoginId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanView", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanView", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanEdit", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanEdit", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanNew", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanNew", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanDelete", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "CanDelete", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "UrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
         
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblUserRightDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByLoginId(int Id)
        {
            this.Adapter.SelectCommand.Parameters["@LoginId"].Value = Id;     
            Fill();
        }
       
        public void FindByCode(int Id)
        {
            this.Adapter.SelectCommand.Parameters["@UrId"].Value = Id;
            Fill();
        }
        public DataTable SearchUserRight(int LoginId)
        {
            DataTable dt = new DataManager.NezamFarsDataSet.tblUserRightDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSearchUserRight", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@LoginId", SqlDbType.Int);       

            if (string.IsNullOrEmpty(LoginId.ToString()))
                LoginId = -1;
           adapter.SelectCommand.Parameters["@LoginId"].Value =LoginId;
       
            adapter.Fill(dt);
           return (dt);

       }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUserHasPermission(string TableCode, Boolean CanNew, Boolean CanView, Boolean CanEdit, Boolean CanDelete)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserHasPermission", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TtCode", SqlDbType.VarChar).Value = TableCode;
            adapter.SelectCommand.Parameters.Add("@CanNew", SqlDbType.Bit).Value = CanNew;
            adapter.SelectCommand.Parameters.Add("@CanView", SqlDbType.Bit).Value = CanView;
            adapter.SelectCommand.Parameters.Add("@CanEdit", SqlDbType.Bit).Value = CanEdit;
            adapter.SelectCommand.Parameters.Add("@CanDelete", SqlDbType.Bit).Value = CanDelete;                  

            adapter.Fill(dt);
            return (dt);

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUserRightByParentId(int ParentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserRight2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ParentId", SqlDbType.Int);

            if (int.Parse(ParentId.ToString()) != -1)

                adapter.SelectCommand.Parameters["@ParentId"].Value = ParentId;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUserRightSelected(int LoginId)
        {
            //DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserRightSelected", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@LoginId", SqlDbType.Int);

            //if (int.Parse(LoginId.ToString()) != -1)

            adapter.SelectCommand.Parameters["@LoginId"].Value = LoginId;

            adapter.Fill(DataTable);
            return (DataTable);

        }

        public void FindByLoginIdAndTtCode(int Id, string TtCode)
        {
            this.Adapter.SelectCommand.Parameters["@LoginId"].Value = Id;
            this.Adapter.SelectCommand.Parameters["@TTCode"].Value = TtCode;

            Fill();
        }

        public DataTable SelectUserRightForNavigationBarAccess(int UserId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserRightForNavigationBarAccess", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            adapter.SelectCommand.Parameters.AddWithValue("@TtCode", -1);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        public DataTable SelectUserRightForNavigationBarSubTitleAccess(int UserId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserRightForNavigationBarSubTitleAccess", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            adapter.SelectCommand.Parameters.AddWithValue("@TtCode", -1);

            adapter.Fill(DataTable);
            return (DataTable);
        }
        public DataTable SelectUserRightForNavigationBarSubTitleAccess(int UserId, int TtCodeParent)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectUserRightForNavigationBarSubTitleAccess", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            adapter.SelectCommand.Parameters.AddWithValue("@TtCode", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@TtCodeParent", TtCodeParent);

            adapter.Fill(DataTable);
            return (DataTable);
        }
    }
}
