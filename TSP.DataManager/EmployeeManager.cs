using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager
{
    public class EmployeeManager : BaseObject
    {
        //static EmployeeManager()            
        //{
        //    _tableId = TableType.Employee;
        //}
        public EmployeeManager()
            : base()
        {
        }
        public EmployeeManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Employee);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblEmployee";
            tableMapping.ColumnMappings.Add("EmpId", "EmpId");
            tableMapping.ColumnMappings.Add("EmpCode", "EmpCode");
            tableMapping.ColumnMappings.Add("FirstName", "FirstName");
            tableMapping.ColumnMappings.Add("LastName", "LastName");
            tableMapping.ColumnMappings.Add("FirstNameEn", "FirstNameEn");
            tableMapping.ColumnMappings.Add("LastNameEn", "LastNameEn");
            tableMapping.ColumnMappings.Add("FatherName", "FatherName");
            tableMapping.ColumnMappings.Add("BirthDate", "BirthDate");
            tableMapping.ColumnMappings.Add("BirthPlace", "BirthPlace");
            tableMapping.ColumnMappings.Add("IdNo", "IdNo");
            tableMapping.ColumnMappings.Add("SSN", "SSN");
            tableMapping.ColumnMappings.Add("SexId", "SexId");
            tableMapping.ColumnMappings.Add("MarId", "MarId");
            tableMapping.ColumnMappings.Add("Tel", "Tel");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("WebSite", "WebSite");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("Nationality", "Nationality");
            tableMapping.ColumnMappings.Add("RelId", "RelId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("EmpStatus", "EmpStatus");
            tableMapping.ColumnMappings.Add("PartId", "PartId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("ImgUrl", "ImgUrl");
            tableMapping.ColumnMappings.Add("MunId", "MunId");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("SecretariatId", "SecretariatId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("SerialNoPcPos", "SerialNoPcPos");
            tableMapping.ColumnMappings.Add("AcceptorIdPcPos", "AcceptorIdPcPos");
            tableMapping.ColumnMappings.Add("TerminalIdPcPos", "TerminalIdPcPos");
            tableMapping.ColumnMappings.Add("ComPortPcPos", "ComPortPcPos");
            tableMapping.ColumnMappings.Add("PropertyCodePC", "PropertyCodePC");
            tableMapping.ColumnMappings.Add("SerialNoPcPos2", "SerialNoPcPos2");
            tableMapping.ColumnMappings.Add("AcceptorIdPcPos2", "AcceptorIdPcPos2");
            tableMapping.ColumnMappings.Add("TerminalIdPcPos2", "TerminalIdPcPos2");
            tableMapping.ColumnMappings.Add("ComPortPcPos2", "ComPortPcPos2");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectEmployee";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EmpId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@EmpCode", System.Data.SqlDbType.VarChar, 20);
            this.Adapter.SelectCommand.Parameters.Add("@SecretariatId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteEmployee";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertEmployee";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MunId", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MunId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecretariatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecretariatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PhysicalAddressPC", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PhysicalAddressPC", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyCodePC", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PropertyCodePC", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateEmployee";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MunId", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MunId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecretariatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecretariatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyCodePC", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PropertyCodePC", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TableCodes.EmployeeRequest;
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblEmployeeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_EmpId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_EmpId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(
                    string EmpCode,
                    string FirstName,
                    string LastName,
                    string FirstNameEn,
                    string LastNameEn,
                    string FatherName,
                    string BirthDate,
                    string BirthPlace,
                    string IdNo,
                    string SSN,
                    System.Nullable<short> SexId,
                    System.Nullable<short> MarId,
                    string Tel,
                    string MobileNo,
                    string WebSite,
                    string Email,
                    string Nationality,
                    System.Nullable<short> RelId,
                    string CreateDate,
                    byte EmpStatus,
                    int PartId,
                    int UserId,
                    string Description,
                    System.DateTime ModifiedDate)
        {
            if ((EmpCode == null))
            {
                throw new System.ArgumentNullException("EmpCode");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(EmpCode));
            }
            if ((FirstName == null))
            {
                throw new System.ArgumentNullException("FirstName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(FirstName));
            }
            if ((LastName == null))
            {
                throw new System.ArgumentNullException("LastName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(LastName));
            }
            if ((FirstNameEn == null))
            {
                this.Adapter.InsertCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(FirstNameEn));
            }
            if ((LastNameEn == null))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(LastNameEn));
            }
            if ((FatherName == null))
            {
                this.Adapter.InsertCommand.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[6].Value = ((string)(FatherName));
            }
            if ((BirthDate == null))
            {
                this.Adapter.InsertCommand.Parameters[7].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[7].Value = ((string)(BirthDate));
            }
            if ((BirthPlace == null))
            {
                this.Adapter.InsertCommand.Parameters[8].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[8].Value = ((string)(BirthPlace));
            }
            if ((IdNo == null))
            {
                this.Adapter.InsertCommand.Parameters[9].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(IdNo));
            }
            if ((SSN == null))
            {
                this.Adapter.InsertCommand.Parameters[10].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[10].Value = ((string)(SSN));
            }
            if ((SexId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[11].Value = ((short)(SexId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[11].Value = System.DBNull.Value;
            }
            if ((MarId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[12].Value = ((short)(MarId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[12].Value = System.DBNull.Value;
            }
            if ((Tel == null))
            {
                this.Adapter.InsertCommand.Parameters[13].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[13].Value = ((string)(Tel));
            }
            if ((MobileNo == null))
            {
                this.Adapter.InsertCommand.Parameters[14].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[14].Value = ((string)(MobileNo));
            }
            if ((WebSite == null))
            {
                this.Adapter.InsertCommand.Parameters[15].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[15].Value = ((string)(WebSite));
            }
            if ((Email == null))
            {
                this.Adapter.InsertCommand.Parameters[16].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[16].Value = ((string)(Email));
            }
            if ((Nationality == null))
            {
                this.Adapter.InsertCommand.Parameters[17].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[17].Value = ((string)(Nationality));
            }
            if ((RelId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[18].Value = ((short)(RelId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[18].Value = System.DBNull.Value;
            }
            if ((CreateDate == null))
            {
                throw new System.ArgumentNullException("CreateDate");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[19].Value = ((string)(CreateDate));
            }
            this.Adapter.InsertCommand.Parameters[20].Value = ((byte)(EmpStatus));
            this.Adapter.InsertCommand.Parameters[21].Value = ((int)(PartId));
            this.Adapter.InsertCommand.Parameters[22].Value = ((int)(UserId));
            if ((Description == null))
            {
                this.Adapter.InsertCommand.Parameters[23].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[23].Value = ((string)(Description));
            }
            this.Adapter.InsertCommand.Parameters[24].Value = ((System.DateTime)(ModifiedDate));
            System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    string EmpCode,
                    string FirstName,
                    string LastName,
                    string FirstNameEn,
                    string LastNameEn,
                    string FatherName,
                    string BirthDate,
                    string BirthPlace,
                    string IdNo,
                    string SSN,
                    System.Nullable<short> SexId,
                    System.Nullable<short> MarId,
                    string Tel,
                    string MobileNo,
                    string WebSite,
                    string Email,
                    string Nationality,
                    System.Nullable<short> RelId,
                    string CreateDate,
                    byte EmpStatus,
                    int PartId,
                    int UserId,
                    string Description,
                    System.DateTime ModifiedDate,
                    int Original_EmpId,
                    byte[] Original_LastTimeStamp,
                    int EmpId)
        {
            if ((EmpCode == null))
            {
                throw new System.ArgumentNullException("EmpCode");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(EmpCode));
            }
            if ((FirstName == null))
            {
                throw new System.ArgumentNullException("FirstName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(FirstName));
            }
            if ((LastName == null))
            {
                throw new System.ArgumentNullException("LastName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(LastName));
            }
            if ((FirstNameEn == null))
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(FirstNameEn));
            }
            if ((LastNameEn == null))
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(LastNameEn));
            }
            if ((FatherName == null))
            {
                this.Adapter.UpdateCommand.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((string)(FatherName));
            }
            if ((BirthDate == null))
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(BirthDate));
            }
            if ((BirthPlace == null))
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(BirthPlace));
            }
            if ((IdNo == null))
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(IdNo));
            }
            if ((SSN == null))
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((string)(SSN));
            }
            if ((SexId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[11].Value = ((short)(SexId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[11].Value = System.DBNull.Value;
            }
            if ((MarId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[12].Value = ((short)(MarId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[12].Value = System.DBNull.Value;
            }
            if ((Tel == null))
            {
                this.Adapter.UpdateCommand.Parameters[13].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[13].Value = ((string)(Tel));
            }
            if ((MobileNo == null))
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((string)(MobileNo));
            }
            if ((WebSite == null))
            {
                this.Adapter.UpdateCommand.Parameters[15].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[15].Value = ((string)(WebSite));
            }
            if ((Email == null))
            {
                this.Adapter.UpdateCommand.Parameters[16].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[16].Value = ((string)(Email));
            }
            if ((Nationality == null))
            {
                this.Adapter.UpdateCommand.Parameters[17].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[17].Value = ((string)(Nationality));
            }
            if ((RelId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[18].Value = ((short)(RelId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[18].Value = System.DBNull.Value;
            }
            if ((CreateDate == null))
            {
                throw new System.ArgumentNullException("CreateDate");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[19].Value = ((string)(CreateDate));
            }
            this.Adapter.UpdateCommand.Parameters[20].Value = ((byte)(EmpStatus));
            this.Adapter.UpdateCommand.Parameters[21].Value = ((int)(PartId));
            this.Adapter.UpdateCommand.Parameters[22].Value = ((int)(UserId));
            if ((Description == null))
            {
                this.Adapter.UpdateCommand.Parameters[23].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[23].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[24].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[25].Value = ((int)(Original_EmpId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[26].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[27].Value = ((int)(EmpId));
            System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        public void FindByCode(int EmpId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            Fill();
        }

        public void FindByEmpCode(string EmpCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EmpCode"].Value = EmpCode;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchEmployee(string EmpCode, string FirstName, string LastName, string IdNo, string SSN, int Part)
        {
            DataTable dt = new NezamFarsDataSet.tblEmployeeDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSearchEmployee", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@EmpCode", SqlDbType.VarChar, 20);
            adapter.SelectCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@IdNo", SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@SSN", SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@Part", SqlDbType.Int, 4);

            if (string.IsNullOrEmpty(EmpCode))
                EmpCode = "%";
            adapter.SelectCommand.Parameters["@EmpCode"].Value = EmpCode;
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;
            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;
            if (string.IsNullOrEmpty(IdNo))
                IdNo = "%";
            adapter.SelectCommand.Parameters["@IdNo"].Value = IdNo;
            if (string.IsNullOrEmpty(SSN))
                SSN = "%";
            adapter.SelectCommand.Parameters["@SSN"].Value = SSN;
            if (string.IsNullOrEmpty(Part.ToString()))
                Part = -1;
            adapter.SelectCommand.Parameters["@Part"].Value = Part;

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUniqueLastName()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEmployeeUniqueFamily", this.Connection);

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectFirstNameAndLastNameExeptUser(int EmpId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEmployeeNameAndFamilyExceptUser", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@EmpId", SqlDbType.Int, 4, "EmpId").Value = EmpId;

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPartId(int PartID)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            adapter.SelectCommand.Connection = Connection;
            adapter.SelectCommand.CommandText = "spSelectEmployeeByPartition";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PartId", System.Data.SqlDbType.Int).Value = PartID;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySecretariatId(int SecretariatId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(SecretariatId.ToString()))
                SecretariatId = -1;

            this.Adapter.SelectCommand.Parameters["@SecretariatId"].Value = SecretariatId;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectWithNoSecretariatId()
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@SecretariatId"].Value = 0;

            Fill();
            return this.DataTable;
        }


        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int GetCount()
        {
            SqlCommand cmd = new SqlCommand("spSelectEmployeeCount",
                this.Connection);
            cmd.Connection.Open();
            int Count = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
            return Count;
        }

        //startRowIndex
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByRowIndex(int startRowIndex, int maximumRows)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            adapter.SelectCommand.Connection = Connection;
            adapter.SelectCommand.CommandText = "spSelectEmployeeSelectByRowIndex";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@StartIndex", System.Data.SqlDbType.Int).Value = startRowIndex;
            adapter.SelectCommand.Parameters.Add("@MaximumRows", System.Data.SqlDbType.Int).Value = maximumRows;

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// ArrayList[0]: CountWfTask, ArrayList[1]: CountAutomationLetter, ArrayList[2]: CountUnRead, ArrayList[3]: CountPublicMsg
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="UltId"></param>
        /// <param name="IsReceiverPart"></param>
        /// <param name="CartableGroup"></param>
        /// <returns></returns>
        public ArrayList FindCountOfUserTaskAndMessage(int EmpId, int UltId, int IsReceiverPart, int CartableGroup)
        {
            ArrayList Count = new ArrayList();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spCountOfUserTaskAndMessage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("EmpId", EmpId);
            adapter.SelectCommand.Parameters.AddWithValue("UltId", UltId);
            adapter.SelectCommand.Parameters.AddWithValue("IsReceiverPart", IsReceiverPart);
            adapter.SelectCommand.Parameters.AddWithValue("CartableGroup", CartableGroup);

            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Count.Add(dt.Rows[0]["CountWfTask"]);
                Count.Add(dt.Rows[0]["CountAutomationLetter"]);
                Count.Add(dt.Rows[0]["CountUnRead"]);
                Count.Add(dt.Rows[0]["CountPublicMsg"]);                
            }
            return (Count);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEmployeeWithNezamChart()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            adapter.SelectCommand.Connection = Connection;
            adapter.SelectCommand.CommandText = "spSelectEmployeeWithNezamChart";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);
        }

        public String SelectEmployeeWithoutMobileNoForSMS(String EmpIds)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter("spSelectEmployeeWithoutMobileNoForSMS", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EmpIds", EmpIds);

            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["EmpIds"].ToString();
            return "";
        }

        #region MunicipalityEmployee
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMunicipalityEmployee()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMunEmployee", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.Fill(this.DataTable);
            return this.DataTable;
        }
        public void FindMunEmpByEmpId(int EmpId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMunEmployee", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("EmpId", EmpId);
            adapter.Fill(this.DataTable);

        }
        #endregion
    }
}
