using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager
{
    public class LinksManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Links);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblLinks";
            tableMapping.ColumnMappings.Add("LiId", "LiId");
            tableMapping.ColumnMappings.Add("LiName", "LiName");
            tableMapping.ColumnMappings.Add("TypeId", "TypeId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("LinkAddress", "LinkAddress");
            tableMapping.ColumnMappings.Add("ImageUrl", "ImageUrl");
            tableMapping.ColumnMappings.Add("ShowInHomePage", "ShowInHomePage");
            tableMapping.ColumnMappings.Add("OrderCode", "OrderCode");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectLinks";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@LiId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@LiName", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@TypeId", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@OrderCode", SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteLinks";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LiId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertLinks";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LiName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LiName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TypeId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LinkAddress", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LinkAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShowInHomePage", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "ShowInHomePage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OrderCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateLinks";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LiName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LiName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TypeId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LinkAddress", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LinkAddress", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShowInHomePage", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "ShowInHomePage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OrderCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LiId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LiId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "LiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.WebSiteHomePageDataSet.tblLinksDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }
        public void FindByCode(int LiId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@LiId"].Value = LiId;
            Fill();
        }

        public void FindByOrderCode(int OrderCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OrderCode"].Value = OrderCode;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Search(string LiName, short TypeId)
        {
            this.Adapter.SelectCommand.Parameters["@LiName"].Value = LiName;
            this.Adapter.SelectCommand.Parameters["@TypeId"].Value = TypeId;
            Fill();
            return DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForHomePage()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectHomePageLinks", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForHomePageTopLinks()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectHomePageTopLinks", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(dt);
            return (dt);
        }
 
        public int FindMaxOrderCode()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMaxOrderCodeLinks", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(dt);
            int max = 0;
            if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["MaxOrderCode"]))
                max = Convert.ToInt32(dt.Rows[0]["MaxOrderCode"]);
            return (max);
        }

        public bool UpdateLinksOrderCode(int LastOrderCode)
        {
            //try
            //{
            SqlCommand Command = new SqlCommand("spUpdateLinksOrderCode", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@LastOrderCode", LastOrderCode);
            //   Command.ExecuteNonQuery();
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
            {
                Command.ExecuteNonQuery();
                return true;
            }
        }

    }
}
