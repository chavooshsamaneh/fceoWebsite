using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingMemberInfoManager : BaseObject
    {

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.MemberDataSet.tblMemberDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        protected override void InitAdapter()
        {

            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMember";
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("FirstName", "FirstName");
            tableMapping.ColumnMappings.Add("LastName", "LastName");
            tableMapping.ColumnMappings.Add("FirstNameEn", "FirstNameEn");
            tableMapping.ColumnMappings.Add("LastNameEn", "LastNameEn");
            tableMapping.ColumnMappings.Add("TiId", "TiId");
            tableMapping.ColumnMappings.Add("FatherName", "FatherName");

            tableMapping.ColumnMappings.Add("IdNo", "IdNo");
            tableMapping.ColumnMappings.Add("IssuePlace", "IssuePlace");
            tableMapping.ColumnMappings.Add("SSN", "SSN");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("HomeAdr", "HomeAdr");

            tableMapping.ColumnMappings.Add("BankAccNo", "BankAccNo");
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("LoanPayAccId", "LoanPayAccId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");

            tableMapping.ColumnMappings.Add("MsId", "MsId");
            tableMapping.ColumnMappings.Add("MrsId", "MrsId");
            tableMapping.ColumnMappings.Add("SexId", "SexId");
            tableMapping.ColumnMappings.Add("MarId", "MarId");
            tableMapping.ColumnMappings.Add("MeNo", "MeNo");

            tableMapping.ColumnMappings.Add("ImpId", "ImpId");
            tableMapping.ColumnMappings.Add("ObsId", "ObsId");
            tableMapping.ColumnMappings.Add("DesId", "DesId");
            tableMapping.ColumnMappings.Add("RelId", "RelId");
            tableMapping.ColumnMappings.Add("ComId", "ComId");
            tableMapping.ColumnMappings.Add("AtId", "AtId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Nationality", "Nationality");
            tableMapping.ColumnMappings.Add("Website", "Website");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("ImageUrl", "ImageUrl");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("UserInfoType", "UserInfoType");
            tableMapping.ColumnMappings.Add("MembershipDate", "MembershipDate");
            tableMapping.ColumnMappings.Add("RecieveMagazine", "RecieveMagazine");
            tableMapping.ColumnMappings.Add("UrbanismId", "UrbanismId");
            tableMapping.ColumnMappings.Add("TrafficId", "TrafficId");
            tableMapping.ColumnMappings.Add("MappingId", "MappingId");
            tableMapping.ColumnMappings.Add("MasterMlId", "MasterMlId");
            tableMapping.ColumnMappings.Add("MasterMFMjId", "MasterMFMjId");
            tableMapping.ColumnMappings.Add("ArchitectorCode", "ArchitectorCode");
            tableMapping.ColumnMappings.Add("TMeId", "TMeId");
            tableMapping.ColumnMappings.Add("GasId", "GasId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectMemberForWebService";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 30);
            this.Adapter.SelectCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@IdNo", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@MobileNo", System.Data.SqlDbType.VarChar, 20);
            this.Adapter.SelectCommand.Parameters.Add("@SoId", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@MarId", System.Data.SqlDbType.SmallInt, 2);
            //this.Adapter.SelectCommand.Parameters.Add("@GrId", System.Data.SqlDbType.Int);

        }

        public void FindByCode(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
        }

    }
}
