using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
   public  class TeacherManager: BaseObject 
   {
       
          public TeacherManager()
            : base()
        {
        }
       public TeacherManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Teacher);
        }
        public static Permission GetUserPermissionForLecturer(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Lecturer);
        }

        protected override void InitAdapter()
        {                                                                                                   
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTeacher";
            tableMapping.ColumnMappings.Add("TeId", "TeId");
            tableMapping.ColumnMappings.Add("Name", "Name");
            tableMapping.ColumnMappings.Add("Family", "Family");
            tableMapping.ColumnMappings.Add("TiId", "TiId");
            tableMapping.ColumnMappings.Add("Father", "Father");
            tableMapping.ColumnMappings.Add("BirthDate", "BirthDate");
            tableMapping.ColumnMappings.Add("IdNo", "IdNo");
            tableMapping.ColumnMappings.Add("SSN", "SSN");
            tableMapping.ColumnMappings.Add("Tel", "Tel");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("LiId", "LiId");
            tableMapping.ColumnMappings.Add("MjId", "MjId");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTeacher";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@LiId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MjId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@Family", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            
            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTeacher";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTeacher";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Name", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Family", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Family", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TiId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Father", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Father", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTeacher";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Name", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Family", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Family", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TiId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Father", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Father", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
       public void FindByCode(int TeId)
       {
           this.Adapter.SelectCommand.Parameters["@TeId"].Value = TeId;
           this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = -1;
           this.Adapter.SelectCommand.Parameters["@LiId"].Value = -1;
           this.Adapter.SelectCommand.Parameters["@MjId"].Value = -1;
           this.Adapter.SelectCommand.Parameters["@TaskId"].Value = -1;
           this.Adapter.SelectCommand.Parameters["@Name"].Value = "%";
           this.Adapter.SelectCommand.Parameters["@Family"].Value = "%";
           Fill();
       }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblTeacherDataTable ();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectConfirmedTeacher()
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherConfirmed", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TSP.DataManager.TableCodes.Teachers);
           adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", (int)TSP.DataManager.WorkFlowTask.ConfirmTeacherAndEndProccess);

           adapter.Fill(dt);
           return (dt);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByTaskCode(int TaskCode)
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacher", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value=-1;
           adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = TaskCode;
           adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)TSP.DataManager.WorkFlows.TeachersConfirming;
           adapter.SelectCommand.Parameters.Add("@LiId", SqlDbType.Int, 4, "LiId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4, "TaskId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.Int, 4, "Name");
           adapter.SelectCommand.Parameters.Add("@Family", SqlDbType.Int, 4, "Family");
           
           adapter.Fill(dt);
           return (dt);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectLecturer(int TeId)
       {
          // DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherLecturer", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId");
           adapter.SelectCommand.Parameters["@TeId"].Value = TeId;

           adapter.Fill(this.DataTable);
           return (this.DataTable);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SerachTeacher(int TeId, int TaskCode, int LiId, int MjId, int TaskId, string Name, string Family)
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacher", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = TaskCode;
           adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)TSP.DataManager.WorkFlows.TeachersConfirming;           
           adapter.SelectCommand.Parameters.Add("@LiId", SqlDbType.Int, 4, "LiId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4, "TaskId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar,100, "Name").Value = "%";
           adapter.SelectCommand.Parameters.Add("@Family", SqlDbType.NVarChar, 100, "Family").Value = "%";

           adapter.Fill(dt);
           return (dt);
       }


       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SearchTeacherByName(string Name, string Family)
       {
           return (SerachTeacher(-1,-1,-1,-1,-1,Name,Family));
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectTeacherBySSN(string SSN)
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherByInfo", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = -1;
           adapter.SelectCommand.Parameters.Add("@LiId", SqlDbType.Int, 4, "LiId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4, "TaskId").Value = -1;
           adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100, "Name").Value = "%";
           adapter.SelectCommand.Parameters.Add("@Family", SqlDbType.NVarChar, 100, "Family").Value = "%";
           adapter.SelectCommand.Parameters.AddWithValue("@SSN",SSN);

           adapter.Fill(dt);
           return (dt);
       }

       public void FindMeId(int MeId)
       {
           ResetAllParameters();
           this.Adapter.SelectCommand.Transaction = this.Transaction;
           this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
           Fill();
       }


       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectTeacher(int LiId, int MjId, string Name, string Family, string EndDateFrom, string EndDateTo)
       {
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacher", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;           
           adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.TeachersConfirming);
           adapter.SelectCommand.Parameters.AddWithValue("@LiId", LiId);
           adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
           adapter.SelectCommand.Parameters.AddWithValue("@Name", Name);
           adapter.SelectCommand.Parameters.AddWithValue("@Family", Family);
           adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom",EndDateFrom);
           adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo",EndDateTo);


           adapter.Fill(DataTable);
           return (DataTable);
       }

       //

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectTeacherAndLecturer()
       {
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherAndLecturer", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.TeachersConfirming);
           adapter.SelectCommand.Parameters.AddWithValue("@LiId", -1);
           adapter.SelectCommand.Parameters.AddWithValue("@MjId", -1);
           adapter.SelectCommand.Parameters.AddWithValue("@Name",  "%");
           adapter.SelectCommand.Parameters.AddWithValue("@Family", "%");
           adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", "1");
           adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", "2");
           adapter.Fill(DataTable);
           return (DataTable);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectActivTeachers()
       {
           SqlDataAdapter adapter = new SqlDataAdapter("SelectActivTeachers", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           DataTable dt = new System.Data.DataTable();
           adapter.Fill(dt);
           return (dt);
       }
    }
}

