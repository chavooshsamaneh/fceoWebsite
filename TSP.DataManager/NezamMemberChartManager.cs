using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{


    public class NezamMemberChartManager : BaseObject
    {
        // static NezamMemberChartManager()
        //{
        //    _tableId = TableType.NezamChart;
        //}
        private DataTable dtNezamChartTree;
        private DataTable dtFullNezamChart;
        public NezamMemberChartManager()
            : base()
        {
        }
        public NezamMemberChartManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.NezamChart);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblNezamMemberChart";
            tableMapping.ColumnMappings.Add("NmcId", "NmcId");
            tableMapping.ColumnMappings.Add("NcId", "NcId");
            tableMapping.ColumnMappings.Add("EmpId", "EmpId");
            tableMapping.ColumnMappings.Add("UltId", "UltId");
            tableMapping.ColumnMappings.Add("IsMaster", "IsMaster");
            tableMapping.ColumnMappings.Add("IsMasterPosition", "IsMasterPosition");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsExternal", "IsExternal");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectNezamMemberChart";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@NcId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@NmcId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@EmpId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@UltId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsExternal", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@IsMaster", System.Data.SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteNezamMemberChart";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertNezamMemberChart";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMasterPosition", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMasterPosition", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExternal", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExternal", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateNezamMemberChart";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMasterPosition", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMasterPosition", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExternal", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExternal", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblNezamMemberChartDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByNcId(int NcId, int InActive=-1)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@NcId"].Value = NcId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
        }

        public void FindByNcId(int NcId)
        {
            FindByNcId(NcId, -1);
        }

        public void FindByEmpId(int EmpId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            Fill();
        }

        public void FindByEmpId(int EmpId, int UltId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            Fill();
        }

        public void FindByMember(int EmpId, int UltId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            Fill();
        }

        public void FindByMember(int EmpId, int UltId,int InActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByIsExternal(Boolean IsExternal)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@IsExternal"].Value = IsExternal;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMasterForAutomation()
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@NcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = -1;           
            this.Adapter.SelectCommand.Parameters["@IsExternal"].Value = false;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@IsMaster"].Value = 1;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForAutomation(Boolean IsExternal)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamMemberChartForAutomation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@IsExternal", IsExternal);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForAutomation(Boolean IsExternal, int CurrentEmpId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamMemberChartForAutomation", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@IsExternal", IsExternal);
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentEmpId", CurrentEmpId);


            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamMemberChartForAutomation(Boolean IsExternal)
        {

            bool found = false;
            int CurrentNcId;
            int CurrentNmcId;


            DataTable dtNmC = new DataTable();
            DataRow d;
            dtNmC.Columns.Add("NmcId", Type.GetType("System.Int32"));
            dtNmC.Columns.Add("Id", Type.GetType("System.Int32"));
            dtNmC.Columns["Id"].AutoIncrement = true;
            dtNmC.Columns.Add("NcId", Type.GetType("System.Int32"));

            dtNmC.Columns.Add("ParentId", Type.GetType("System.Int32"));
            dtNmC.Columns.Add("NcName", Type.GetType("System.String"));
            dtNmC.Columns.Add("FirstName", Type.GetType("System.String"));
            dtNmC.Columns.Add("LastName", Type.GetType("System.String"));
            dtNmC.Columns.Add("FullName", Type.GetType("System.String"));
            dtNmC.Columns.Add("IsMaster", Type.GetType("System.Boolean"));
            dtNmC.Columns.Add("IsMasterPosition", Type.GetType("System.Boolean"));
            dtNmC.Columns.Add("InActive", Type.GetType("System.Boolean"));
            dtNmC.Columns.Add("NodeType", Type.GetType("System.Int32"));


            TSP.DataManager.NezamChartManager NcManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.NezamMemberChartManager NmcManager = new TSP.DataManager.NezamMemberChartManager();
            NmcManager.Fill();
            //DataTable dtnezamChart = NcManager.SelectAllPosition(IsExternal);
            DataTable dtnezamChart = NmcManager.SelectForAutomation(IsExternal);
            for (int i = 0; i < dtnezamChart.Rows.Count; i++)
            {
                d = dtNmC.NewRow();
                d["NodeType"] = 0;
                d["NmcId"] = -1;
                d["NcId"] = int.Parse(dtnezamChart.Rows[i]["NcId"].ToString());
                CurrentNcId = int.Parse(dtnezamChart.Rows[i]["NcId"].ToString());
                if (!String.IsNullOrEmpty(dtnezamChart.Rows[i]["ParentId"].ToString()))
                {
                    int j = 0;
                    found = false;
                    while ((!found) && (j < dtNmC.Rows.Count))
                    {
                        if (dtNmC.Rows[j]["NcId"].ToString() == dtnezamChart.Rows[i]["ParentId"].ToString())
                        {
                            d["ParentId"] = dtNmC.Rows[j]["Id"];//dtnezamChart.Rows[i]["ParentId"]
                            found = true;
                        }
                        j++;
                    }


                }
                else
                    d["ParentId"] = dtnezamChart.Rows[i]["ParentId"];
                d["NcName"] = dtnezamChart.Rows[i]["NcName"];
                dtNmC.Rows.Add(d);
                CurrentNmcId = int.Parse(dtNmC.Rows[(dtNmC.Rows.Count) - 1]["Id"].ToString());
                found = false;
                int k = 0;
                while (k < NmcManager.Count)
                {
                    if (NmcManager[k]["NcId"].ToString() == CurrentNcId.ToString())
                    {
                        d = dtNmC.NewRow();
                        d["NcId"] = int.Parse(NmcManager[k]["NcId"].ToString());
                        d["NmcId"] = int.Parse(NmcManager[k]["NmcId"].ToString());
                        d["ParentId"] = CurrentNmcId;
                        d["FirstName"] = NmcManager[k]["FirstName"];
                        d["LastName"] = NmcManager[k]["LastName"];
                        d["FullName"] = NmcManager[k]["FirstName"].ToString() + " " + NmcManager[k]["LastName"].ToString();
                        d["IsMaster"] = NmcManager[k]["IsMaster"];
                        d["IsMasterPosition"] = NmcManager[k]["IsMasterPosition"];
                        d["InActive"] = NmcManager[k]["InActive"];

                        d["NodeType"] = 1;
                        dtNmC.Rows.Add(d);
                    }
                    k++;
                }
            }

            return (dtNmC);

        }

        public void FindByNmcId(int NmcId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = NmcId;
            Fill();
        }
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        //public virtual int Delete(int Original_NmcId, byte[] Original_LastTimeStamp)
        //{
        //    this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_NmcId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
        //    if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.DeleteCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.DeleteCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        //public virtual int Insert(int NcId, int EmpId, string StartDate, string EndDate, string Description, int UserId, System.DateTime ModfiedDate)
        //{
        //    this.Adapter.InsertCommand.Parameters[1].Value = ((int)(NcId));
        //    this.Adapter.InsertCommand.Parameters[2].Value = ((int)(EmpId));
        //    if ((StartDate == null))
        //    {
        //        throw new System.ArgumentNullException("StartDate");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = ((string)(StartDate));
        //    }
        //    if ((EndDate == null))
        //    {
        //        throw new System.ArgumentNullException("EndDate");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[4].Value = ((string)(EndDate));
        //    }
        //    if ((Description == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[5].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[5].Value = ((string)(Description));
        //    }
        //    this.Adapter.InsertCommand.Parameters[6].Value = ((int)(UserId));
        //    this.Adapter.InsertCommand.Parameters[7].Value = ((System.DateTime)(ModfiedDate));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
        //    if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.InsertCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.InsertCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        //public virtual int Update(int NcId, int EmpId, string StartDate, string EndDate, string Description, int UserId, System.DateTime ModfiedDate, int Original_NmcId, byte[] Original_LastTimeStamp, int NmcId)
        //{
        //    this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(NcId));
        //    this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(EmpId));
        //    if ((StartDate == null))
        //    {
        //        throw new System.ArgumentNullException("StartDate");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(StartDate));
        //    }
        //    if ((EndDate == null))
        //    {
        //        throw new System.ArgumentNullException("EndDate");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(EndDate));
        //    }
        //    if ((Description == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[5].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(Description));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(UserId));
        //    this.Adapter.UpdateCommand.Parameters[7].Value = ((System.DateTime)(ModfiedDate));
        //    this.Adapter.UpdateCommand.Parameters[8].Value = ((int)(Original_NmcId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[9].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(NmcId));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
        //    if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.UpdateCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.UpdateCommand.Connection.Close();
        //        }
        //    }
        //}


        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectNezamMemberChart(int IsExternal)
        //{

        //    bool found = false;
        //    int CurrentNcId;
        //    int CurrentNmcId;


        //    DataTable dtNmC = new DataTable();
        //    DataRow d;
        //    dtNmC.Columns.Add("NmcId", Type.GetType("System.Int32"));
        //    dtNmC.Columns.Add("Id", Type.GetType("System.Int32"));
        //    dtNmC.Columns["Id"].AutoIncrement = true;
        //    dtNmC.Columns.Add("NcId", Type.GetType("System.Int32"));

        //    dtNmC.Columns.Add("ParentId", Type.GetType("System.Int32"));
        //    dtNmC.Columns.Add("NcName", Type.GetType("System.String"));
        //    dtNmC.Columns.Add("FirstName", Type.GetType("System.String"));
        //    dtNmC.Columns.Add("LastName", Type.GetType("System.String"));
        //    dtNmC.Columns.Add("FullName", Type.GetType("System.String"));
        //    dtNmC.Columns.Add("IsMaster", Type.GetType("System.Boolean"));
        //    dtNmC.Columns.Add("IsMasterPosition", Type.GetType("System.Boolean"));
        //    dtNmC.Columns.Add("InActive", Type.GetType("System.Boolean"));
        //    dtNmC.Columns.Add("NodeType", Type.GetType("System.Int32"));


        //    TSP.DataManager.NezamChartManager NcManager = new TSP.DataManager.NezamChartManager();
        //    TSP.DataManager.NezamMemberChartManager NmcManager = new TSP.DataManager.NezamMemberChartManager();
        //    NmcManager.Fill();
        //    DataTable dtnezamChart = NcManager.SelectAllPosition(IsExternal);

        //    for (int i = 0; i < dtnezamChart.Rows.Count; i++)
        //    {
        //        d = dtNmC.NewRow();
        //        d["NodeType"] = 0;
        //        d["NmcId"] = -1;
        //        d["NcId"] = int.Parse(dtnezamChart.Rows[i]["NcId"].ToString());
        //        CurrentNcId = int.Parse(dtnezamChart.Rows[i]["NcId"].ToString());
        //        if (!String.IsNullOrEmpty(dtnezamChart.Rows[i]["ParentId"].ToString()))
        //        {
        //            int j = 0;
        //            found = false;
        //            while ((!found) && (j < dtNmC.Rows.Count))
        //            {
        //                if (dtNmC.Rows[j]["NcId"].ToString() == dtnezamChart.Rows[i]["ParentId"].ToString())
        //                {
        //                    d["ParentId"] = dtNmC.Rows[j]["Id"];//dtnezamChart.Rows[i]["ParentId"]
        //                    found = true;
        //                }
        //                j++;
        //            }


        //        }
        //        else
        //            d["ParentId"] = dtnezamChart.Rows[i]["ParentId"];
        //        d["NcName"] = dtnezamChart.Rows[i]["NcName"];
        //        dtNmC.Rows.Add(d);
        //        CurrentNmcId = int.Parse(dtNmC.Rows[(dtNmC.Rows.Count) - 1]["Id"].ToString());
        //        found = false;
        //        int k = 0;
        //        while (k < NmcManager.Count)
        //        {
        //            if (NmcManager[k]["NcId"].ToString() == CurrentNcId.ToString())
        //            {
        //                d = dtNmC.NewRow();
        //                d["NcId"] = int.Parse(NmcManager[k]["NcId"].ToString());
        //                d["NmcId"] = int.Parse(NmcManager[k]["NmcId"].ToString());
        //                d["ParentId"] = CurrentNmcId;
        //                d["FirstName"] = NmcManager[k]["FirstName"];
        //                d["LastName"] = NmcManager[k]["LastName"];
        //                d["FullName"] = NmcManager[k]["FirstName"].ToString() + " " + NmcManager[k]["LastName"].ToString();
        //                d["IsMaster"] = NmcManager[k]["IsMaster"];
        //                d["IsMasterPosition"] = NmcManager[k]["IsMasterPosition"];
        //                d["InActive"] = NmcManager[k]["InActive"];

        //                d["NodeType"] = 1;
        //                dtNmC.Rows.Add(d);
        //            }
        //            k++;
        //        }
        //    }

        //    return (dtNmC);

        //}

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByIsMaster(Boolean IsMaster)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamMemberChartByMaster", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@IsMaster", SqlDbType.Bit, 1, "IsMaster").Value = IsMaster;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberChartMeEmp()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamMemberChartMe_Emp", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamMemberChartForContactUs()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamMemberChartForContactUs", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamMemberChart(int IsExternal,int InActive=-1)
        {
            dtNezamChartTree = new DataTable();
            dtFullNezamChart = new DataTable();
            dtNezamChartTree.Columns.Add("NmcId", Type.GetType("System.Int32"));
            dtNezamChartTree.Columns.Add("Id", Type.GetType("System.Int32"));
            dtNezamChartTree.Columns["Id"].AutoIncrement = true;
            dtNezamChartTree.Columns.Add("NcId", Type.GetType("System.Int32"));

            dtNezamChartTree.Columns.Add("ParentId", Type.GetType("System.Int32"));
            dtNezamChartTree.Columns.Add("NcName", Type.GetType("System.String"));
            dtNezamChartTree.Columns.Add("FirstName", Type.GetType("System.String"));
            dtNezamChartTree.Columns.Add("LastName", Type.GetType("System.String"));
            dtNezamChartTree.Columns.Add("FullName", Type.GetType("System.String"));
            dtNezamChartTree.Columns.Add("IsMaster", Type.GetType("System.Boolean"));
            dtNezamChartTree.Columns.Add("IsMasterPosition", Type.GetType("System.Boolean"));
            dtNezamChartTree.Columns.Add("InActive", Type.GetType("System.Boolean"));
            dtNezamChartTree.Columns.Add("NodeType", Type.GetType("System.Int32"));


            TSP.DataManager.NezamChartManager NcManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.NezamMemberChartManager NmcManager = new TSP.DataManager.NezamMemberChartManager();
            dtFullNezamChart = NcManager.SelectAllPosition(IsExternal,InActive,-1);

            FillTreeView("NULL", -1, NmcManager, NcManager,InActive);

            return (dtNezamChartTree);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamMemberChart(int IsExternal)
        {
            return SelectNezamMemberChart(IsExternal, -1);
        }

        private void FillTreeView(string ParentId,int Id, TSP.DataManager.NezamMemberChartManager NmcManager, TSP.DataManager.NezamChartManager NezamChartManager,int NmcInActive=-1)
        {
            DataRow d = dtNezamChartTree.NewRow();
            DataRow[] dataRow;
            if (ParentId == "NULL")
                dataRow = dtFullNezamChart.Select("ParentId is null");
            else
                dataRow = dtFullNezamChart.Select("ParentId = " + ParentId);

            foreach (DataRow NcRow in dataRow)
            {
                d = dtNezamChartTree.NewRow();
                d["NodeType"] = 0;
                d["NmcId"] = -1;
                d["NcId"] = int.Parse(NcRow["NcId"].ToString());
                int NextParentId = int.Parse(NcRow["NcId"].ToString());
                if (ParentId == "NULL")
                    d["ParentId"] = DBNull.Value;
                else
                    d["ParentId"] = Id;
                d["NcName"] = NcRow["NcName"];
                d["InActive"] = NcRow["InActive"];
                dtNezamChartTree.Rows.Add(d);

                NmcManager.FindByNcId(int.Parse(NcRow["NcId"].ToString()), NmcInActive);
                int CurrentNcId = int.Parse(dtNezamChartTree.Rows[(dtNezamChartTree.Rows.Count) - 1]["Id"].ToString());

                for (int i = 0; i < NmcManager.Count; i++)
                {
                    d = dtNezamChartTree.NewRow();
                    d["NcId"] = int.Parse(NmcManager[i]["NcId"].ToString());
                    d["NmcId"] = int.Parse(NmcManager[i]["NmcId"].ToString());
                    d["ParentId"] = CurrentNcId;
                    d["FirstName"] = NmcManager[i]["FirstName"];
                    d["LastName"] = NmcManager[i]["LastName"];
                    d["FullName"] = NmcManager[i]["FirstName"].ToString() + " " + NmcManager[i]["LastName"].ToString();
                    d["IsMaster"] = NmcManager[i]["IsMaster"];
                    d["IsMasterPosition"] = NmcManager[i]["IsMasterPosition"];
                    d["InActive"] = NmcManager[i]["InActive"];
                    d["NodeType"] = 1;
                    dtNezamChartTree.Rows.Add(d);
                }

                FillTreeView(NextParentId.ToString(),CurrentNcId, NmcManager, NezamChartManager,NmcInActive);
            }
        }
    }
}
