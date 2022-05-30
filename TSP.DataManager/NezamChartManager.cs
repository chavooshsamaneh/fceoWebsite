using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class NezamChartManager : BaseObject
    {
        //static NezamChartManager()
        //{
        //    _tableId = TableType.NezamChart;
        //}
        public NezamChartManager()
            : base()
        {
        }
        public NezamChartManager(System.Data.DataSet ds)
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
            tableMapping.DataSetTable = "tblNezamChart";
            tableMapping.ColumnMappings.Add("NcId", "NcId");
            tableMapping.ColumnMappings.Add("NcName", "NcName");
            tableMapping.ColumnMappings.Add("ParentId", "ParentId");
            tableMapping.ColumnMappings.Add("IsExternal", "IsExternal");
            tableMapping.ColumnMappings.Add("IsDepartment", "IsDepartment");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectNezamChart";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add("@NcName", System.Data.SqlDbType.NVarChar);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteNezamChart";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertNezamChart";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NcName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ParentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExternal", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExternal", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDepartment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDepartment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateNezamChart";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NcName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ParentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsExternal", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsExternal", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDepartment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDepartment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblNezamChartDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamChart()
        {

            bool found = false;
            int CurrentNcId;
            int CurrentNmcId;


            DataTable dtNmC = new DataTable();
            DataRow d;
            dtNmC.Columns.Add("NmcId", Type.GetType("System.Int32"));
            dtNmC.Columns["NmcId"].AutoIncrement = true;
            dtNmC.Columns.Add("NcId", Type.GetType("System.Int32"));
            dtNmC.Columns.Add("ParentId", Type.GetType("System.Int32"));
            dtNmC.Columns.Add("NcName", Type.GetType("System.String"));


            //dtNmC.PrimaryKey = dtNmC.Columns["NmcId"];

            TSP.DataManager.NezamChartManager NcManager = new TSP.DataManager.NezamChartManager();
            NcManager.Fill();

            for (int i = 0; i < NcManager.Count; i++)
            {
                d = dtNmC.NewRow();
                d["NcId"] = int.Parse(NcManager[i]["NcId"].ToString());
                CurrentNcId = int.Parse(NcManager[i]["NcId"].ToString());
                if (!String.IsNullOrEmpty(NcManager[i]["ParentId"].ToString()))
                {
                    int j = 0;
                    found = false;
                    while ((!found) && (j < dtNmC.Rows.Count))
                    //for (int j = 0; j < dtNmC.Rows.Count; j++)
                    {
                        if (dtNmC.Rows[j]["NcId"].ToString() == NcManager[i]["ParentId"].ToString())
                        {
                            d["ParentId"] = dtNmC.Rows[j]["NmcId"];
                            found = true;
                        }
                        j++;
                    }


                }
                else
                    d["ParentId"] = NcManager[i]["ParentId"];
                d["NcName"] = NcManager[i]["NcName"];
                dtNmC.Rows.Add(d);
                CurrentNmcId = int.Parse(dtNmC.Rows[(dtNmC.Rows.Count) - 1]["NmcId"].ToString());
                found = false;

            }




            //TreeListNmChart.KeyFieldName = "NmcId";
            //TreeListNmChart.ParentFieldName = "ParentId";
            //TreeListNmChart.DataSource = dtNmC;
            //TreeListNmChart.DataBind();

            //TreeListNmChart.Columns["NcName"].Caption = "پست سازمانی";
            //TreeListNmChart.Columns["FirstName"].Caption = "نام";
            //TreeListNmChart.Columns["LastName"].Caption = " نام خانوادگی";
            //TreeListNmChart.Columns["StartDate"].Caption = " تاریخ شروع";
            //TreeListNmChart.Columns["EndDate"].Caption = " تاریخ پایان";
            //TreeListNmChart.Columns["NcId"].Visible = false;

            return (dtNmC);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int NcId)
        {
            this.Adapter.SelectCommand.Parameters["@NcId"].Value = NcId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
        }

        public void FindByNcName(string NcName)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@NcName"].Value = NcName;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectById(int NcId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamChart-MainInfo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@NcId", NcId);

            adapter.Fill(DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByEmpId(int EmpId, int UltId)
        {
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="UltId"></param>
        /// <param name="InActive">0:Active,1:InActive,-1:All</param>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByEmpId(int EmpId, int UltId, int InActive)
        {
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = UltId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="UltId"></param>
        /// <param name="InActive">0:Active,1:InActive,-1:All</param>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamChartActive()
        {
            DataTable dt = new DataTable();
            this.ResetAllParameters();            
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.Fill(DataTable);
            return DataTable;
        }

        public int FindNmcId(int CurrentUserId, TSP.DataManager.LoginManager LoginManager)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = this;
            LoginManager.FindByCode(CurrentUserId);
            int NmcId = -1;
            if (LoginManager.Count > 0)
            {
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                NezamChartManager.FindByEmpId(EmpId, UltId, 0);
                if (NezamChartManager.Count > 0)
                {
                    NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
                }
            }
            return (NmcId);
        }

        public int FindNmcId(int CurrentUserId, int TaskId, TSP.DataManager.LoginManager LoginManager)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = this;
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            TaskDoerManager.FindByTaskId(TaskId);
            if (TaskDoerManager.Count == 0)
            {
                return -1;
            }

            string[] DoerNcId = new string[TaskDoerManager.Count];
            for (int i = 0; i < TaskDoerManager.Count; i++)
            {
                DoerNcId[i] = TaskDoerManager[i]["NcId"].ToString();
            }

            LoginManager.FindByCode(CurrentUserId);
            int NmcId = -1;
            if (LoginManager.Count > 0)
            {
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                NezamChartManager.FindByEmpId(EmpId, UltId, 0);
                if (NezamChartManager.Count > 0)
                {
                    for (int j = 0; j < NezamChartManager.Count; j++)
                    {
                        for (int k = 0; k < DoerNcId.Length; k++)
                        {
                            if (DoerNcId[k] == NezamChartManager[j]["NcId"].ToString())
                            {
                                NmcId = int.Parse(NezamChartManager[j]["NmcId"].ToString());
                                break;
                            }
                        }
                    }
                    //NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
                }
            }
            return (NmcId);
        }

        public int FindNmcIdByNcId(int NcId, int CurrentUserId, TSP.DataManager.LoginManager LoginManager)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = this;
            LoginManager.FindByCode(CurrentUserId);
            int NmcId = -1;
            if (LoginManager.Count > 0)
            {
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                NezamChartManager.FindByEmpId(EmpId, UltId);
                if (NezamChartManager.Count > 0)
                {
                    if (NcId == int.Parse(NezamChartManager[0]["NcId"].ToString()))
                        NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
                }
            }
            return (NmcId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectChild()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamChartChild", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByWorkFlowTask(int TaskId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamChartByWFTask", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);

            adapter.Fill(dt);
            return (dt);
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAllPosition(int IsExternal, int InActive, int ParentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamChartAllPosition", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@IsExternal", IsExternal);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@ParentId", ParentId);

            adapter.Fill(dt);
            return (dt);
        }
         public DataTable SelectAllPosition(int IsExternal)
        {
            return SelectAllPosition(IsExternal, -1,-1);
        }


         public DataTable SelectAllPosition(int InActive, int ParentId)
         {
             return SelectAllPosition(-1, InActive, ParentId);
         }
        

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectNezamChartForSecretariat()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectNezamChartForSecretariat", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
