using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public enum CitIdForTsObsWorkRequest
    {
        Khanzenyan = 162, Kharameh = 166, Dariyoon = 187, Zarghan = 255, Sarvesran = 269, Shiraz = 317, Lapoe = 438, Sepidan = 518, Beyza = 524,SoltanShahr=536, Sadra = 541
    }
    public class CityManager : BaseObject
    {

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblCityDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.City);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblCity";
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("CitCode", "CitCode");
            tableMapping.ColumnMappings.Add("CitName", "CitName");
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("ReId", "ReId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("CounId", "CounId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ReCitId", "ReCitId");
            tableMapping.ColumnMappings.Add("ShowInTsWorkRequest", "ShowInTsWorkRequest");
            tableMapping.ColumnMappings.Add("AccountNumberDesign", "AccountNumberDesign");
            tableMapping.ColumnMappings.Add("AccountNmberObserving", "AccountNmberObserving");
            tableMapping.ColumnMappings.Add("AccountNmberObserving5Percent", "AccountNmberObserving5Percent");
            tableMapping.ColumnMappings.Add("AccountNmber5In1000", "AccountNmber5In1000");
            tableMapping.ColumnMappings.Add("AccountNmber2In1000", "AccountNmber2In1000");
            tableMapping.ColumnMappings.Add("PINCodeDesign", "PINCodeDesign");
            tableMapping.ColumnMappings.Add("TerminalDesign", "TerminalDesign");
            tableMapping.ColumnMappings.Add("PINCodeObserver", "PINCodeObserver");
            tableMapping.ColumnMappings.Add("TerminalObserver", "TerminalObserver");
            tableMapping.ColumnMappings.Add("CanObserverBeDesigner", "CanObserverBeDesigner");
            tableMapping.ColumnMappings.Add("NValueInFunctionA", "NValueInFunctionA");
            tableMapping.ColumnMappings.Add("IsPopulationUnder25000", "IsPopulationUnder25000");
            
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectCity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CitId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CounId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteCity";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertCity";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CitCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CitName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReCitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReCitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShowInTsWorkRequest", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "ShowInTsWorkRequest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNumberDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNumberDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmberObserving", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmberObserving", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmberObserving5Percent", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmberObserving5Percent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmber5In1000", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmber5In1000", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmber2In1000", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmber2In1000", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PINCodeDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PINCodeDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PINCodeObserver", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PINCodeObserver", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalObserver", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalObserver", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanObserverBeDesigner", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "CanObserverBeDesigner", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NValueInFunctionA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NValueInFunctionA", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPopulationUnder25000", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsPopulationUnder25000", System.Data.DataRowVersion.Current, false, null, "", "", "")); 
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateCity";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CitCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CitName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CounId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReCitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReCitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShowInTsWorkRequest", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "ShowInTsWorkRequest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNumberDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNumberDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmberObserving", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmberObserving", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmberObserving5Percent", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmberObserving5Percent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmber5In1000", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmber5In1000", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNmber2In1000", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNmber2In1000", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PINCodeDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PINCodeDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalDesign", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalDesign", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PINCodeObserver", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PINCodeObserver", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalObserver", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalObserver", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CanObserverBeDesigner", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "CanObserverBeDesigner", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NValueInFunctionA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NValueInFunctionA", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPopulationUnder25000", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, 1, 0, "IsPopulationUnder25000", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public void FindByCode(int CitId)
        {
            this.Adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchCity(string CitCode, string CitName, int PrId)
        {
            DataTable dt = new NezamFarsDataSet.tblCityDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSearchCity", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@CitCode", SqlDbType.VarChar, 15);
            adapter.SelectCommand.Parameters.Add("@CitName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@PrId", SqlDbType.Int, 4);

            if (string.IsNullOrEmpty(CitCode))
                CitCode = "%";
            adapter.SelectCommand.Parameters["@CitCode"].Value = CitCode;
            if (string.IsNullOrEmpty(CitName))
                CitName = "%";
            adapter.SelectCommand.Parameters["@CitName"].Value = CitName;
            if (string.IsNullOrEmpty(PrId.ToString()))
                PrId = -1;
            adapter.SelectCommand.Parameters["@PrId"].Value = PrId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByAgent(int AgentId, int ShowInTsWorkRequest, int CitId, int CitIdExeption, string CitIdList, string CitIdExeptionList)
        {
            DataTable dt = new NezamFarsDataSet.tblCityDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectCityByAgent", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@AgentId", SqlDbType.Int, 4, "AgentId").Value = AgentId;
            adapter.SelectCommand.Parameters.AddWithValue("@ShowInTsWorkRequest", ShowInTsWorkRequest);
            adapter.SelectCommand.Parameters.AddWithValue("@CitId", CitId);
            adapter.SelectCommand.Parameters.AddWithValue("@CitIdExeption", CitIdExeption);
            adapter.SelectCommand.Parameters.AddWithValue("@CitIdList", CitIdList);
            adapter.SelectCommand.Parameters.AddWithValue("@CitIdExeptionList", CitIdExeptionList);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByAgent(int AgentId)
        {
            return SelectByAgent(AgentId, -1, -1, -1,"","");
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByCountry(int CounId)
        {
            DataTable dt = new NezamFarsDataSet.tblCityDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectCity", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@CitId", SqlDbType.Int, 4, "CitId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@CounId", SqlDbType.Int, 4, "CounId").Value = CounId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByReCitId(int ReCitId)
        {
            DataTable dt = new NezamFarsDataSet.tblCityDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectCityByReCitId", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ReCitId", SqlDbType.Int, 4, "ReCitId").Value = ReCitId;
            if (ReCitId > -1)
                adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByPrId(int PrId)
        {
            this.Adapter.SelectCommand.Parameters["@PrId"].Value = PrId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByCounId(int CounId)
        {
            this.Adapter.SelectCommand.Parameters["@CounId"].Value = CounId;
            Fill();
            return this.DataTable;
        }

    }
}
