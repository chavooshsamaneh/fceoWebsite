using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TSP.DataManager
{
    public class EmployeeRequestManager : BaseObject
    {
        #region Utility Methods        
        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        private static Boolean IsDBNullOrNullValue(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return true;
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            return false;
        }
        #endregion

        #region Private Managers
        TSP.DataManager.EmployeeManager EmpManager;
        #endregion

        #region Constructors
        public EmployeeRequestManager()
        {
            EmpManager = new EmployeeManager();
        }

        public EmployeeRequestManager(TransactionManager TransactionManager)
        {
            EmpManager = new EmployeeManager();
            if (TransactionManager != null)
            {
                TransactionManager.Add(EmpManager);
            }
        }
        #endregion

        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Confirming EmployeeRequestConfirming
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int EmpReId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(EmpReId);
            if (this.Count > 0)
            {
                //#region Copy EmpRequest Into Emp
                int EmpId = int.Parse(this[0]["EmpId"].ToString());
                EmpManager.FindByCode(EmpId);
                if (EmpManager.Count != 1)
                {
                    return (int)ErrorWFNextStep.Error;
                }

                EmpManager[0].BeginEdit();

                EmpManager[0]["FirstName"] = this[0]["FirstName"].ToString();
                EmpManager[0]["LastName"] = this[0]["LastName"].ToString();
                EmpManager[0]["FirstNameEn"] = this[0]["FirstNameEn"].ToString();
                EmpManager[0]["LastNameEn"] = this[0]["LastNameEn"].ToString();
                EmpManager[0]["FatherName"] = this[0]["FatherName"].ToString();
                EmpManager[0]["BirthDate"] = this[0]["BirthDate"].ToString();
                EmpManager[0]["BirthPlace"] = this[0]["BirthPlace"].ToString();
                EmpManager[0]["IdNo"] = this[0]["IdNo"].ToString();
                EmpManager[0]["SSN"] = this[0]["SSN"].ToString();
                if (!IsDBNullOrNullValue(this[0]["SexId"]))
                    EmpManager[0]["SexId"] = this[0]["SexId"].ToString();
                else
                    EmpManager[0]["SexId"] = DBNull.Value;

                if (!IsDBNullOrNullValue(this[0]["MarId"]))
                    EmpManager[0]["MarId"] = this[0]["MarId"].ToString();
                else
                    EmpManager[0]["MarId"] = DBNull.Value;

                EmpManager[0]["Address"] = this[0]["Address"].ToString();
                EmpManager[0]["Tel"] = this[0]["Tel"].ToString();
                EmpManager[0]["MobileNo"] = this[0]["MobileNo"].ToString();
                EmpManager[0]["WebSite"] = this[0]["WebSite"].ToString();
                EmpManager[0]["Email"] = this[0]["Email"].ToString();
                EmpManager[0]["Nationality"] = this[0]["Nationality"].ToString();
                if (!IsDBNullOrNullValue(this[0]["RelId"]))
                    EmpManager[0]["RelId"] = this[0]["RelId"].ToString();
                else
                    EmpManager[0]["RelId"] = DBNull.Value;
                //createdate
                EmpManager[0]["EmpStatus"] = this[0]["EmpStatus"].ToString();
                if (!IsDBNullOrNullValue(this[0]["PartId"]))
                    EmpManager[0]["PartId"] = this[0]["PartId"].ToString();
                else
                    EmpManager[0]["PartId"] = DBNull.Value;
                if (!IsDBNullOrNullValue(this[0]["AgentId"]))
                    EmpManager[0]["AgentId"] = this[0]["AgentId"].ToString();
                else
                    EmpManager[0]["AgentId"] = DBNull.Value;
                EmpManager[0]["Description"] = this[0]["Description"].ToString();

                //EmpManager[0]["EmpCode"] = this[0]["EmpCode"].ToString();

                EmpManager[0]["ImgUrl"] = this[0]["ImgUrl"].ToString();
                EmpManager[0]["SignUrl"] = this[0]["SignUrl"].ToString();



                if (!Utility.IsDBNullOrNullValue(this[0]["PropertyCodePC"]))
                    EmpManager[0]["PropertyCodePC"] = this[0]["PropertyCodePC"].ToString();
                else
                    EmpManager[0]["PropertyCodePC"] = "";

                if (!Utility.IsDBNullOrNullValue(this[0]["TerminalIdPcPos"]))
                    EmpManager[0]["TerminalIdPcPos"] = this[0]["TerminalIdPcPos"].ToString();
                else
                    EmpManager[0]["TerminalIdPcPos"] = "";
                if (!Utility.IsDBNullOrNullValue(this[0]["ComPortPcPos"]))
                    EmpManager[0]["ComPortPcPos"] = this[0]["ComPortPcPos"].ToString();
                else
                    EmpManager[0]["ComPortPcPos"] = "";
                if (!Utility.IsDBNullOrNullValue(this[0]["AcceptorIdPcPos"]))
                    EmpManager[0]["AcceptorIdPcPos"] = this[0]["AcceptorIdPcPos"].ToString();
                else
                    EmpManager[0]["AcceptorIdPcPos"] = "";

                if (!Utility.IsDBNullOrNullValue(this[0]["SerialNoPcPos"]))
                    EmpManager[0]["SerialNoPcPos"] = this[0]["SerialNoPcPos"].ToString();
                else
                    EmpManager[0]["SerialNoPcPos"] = "";

                if (!Utility.IsDBNullOrNullValue(this[0]["TerminalIdPcPos2"]))
                    EmpManager[0]["TerminalIdPcPos2"] = this[0]["TerminalIdPcPos2"].ToString();
                else
                    EmpManager[0]["TerminalIdPcPos2"] = "";
                if (!Utility.IsDBNullOrNullValue(this[0]["ComPortPcPos2"]))
                    EmpManager[0]["ComPortPcPos2"] = this[0]["ComPortPcPos2"].ToString();
                else
                    EmpManager[0]["ComPortPcPos2"] = "";
                if (!Utility.IsDBNullOrNullValue(this[0]["AcceptorIdPcPos2"]))
                    EmpManager[0]["AcceptorIdPcPos2"] = this[0]["AcceptorIdPcPos2"].ToString();
                else
                    EmpManager[0]["AcceptorIdPcPos2"] = "";
                if (!Utility.IsDBNullOrNullValue(this[0]["SerialNoPcPos2"]))
                    EmpManager[0]["SerialNoPcPos2"] = this[0]["SerialNoPcPos2"].ToString();
                else
                    EmpManager[0]["SerialNoPcPos2"] = "";                

              EmpManager[0].EndEdit();
                if (EmpManager.Save() <= 0)
                {
                    return (int)ErrorWFNextStep.Error;
                }

                this[0].BeginEdit();
                this[0]["IsConfirmed"] = 1;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting EmployeeRequestConfirming
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int EmpReId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(EmpReId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirmed"] = 2;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EmployeeRequest);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblEmployeeRequest";
            tableMapping.ColumnMappings.Add("EmpReId", "EmpReId");
            tableMapping.ColumnMappings.Add("EmpId", "EmpId");
            tableMapping.ColumnMappings.Add("Type", "Type");
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
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Tel", "Tel");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("WebSite", "WebSite");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("Nationality", "Nationality");
            tableMapping.ColumnMappings.Add("RelId", "RelId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("EmpStatus", "EmpStatus");
            tableMapping.ColumnMappings.Add("PartId", "PartId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("ImgUrl", "ImgUrl");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("SecretariatId", "SecretariatId");
            tableMapping.ColumnMappings.Add("MailDate", "MailDate");
            tableMapping.ColumnMappings.Add("MailNo", "MailNo");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
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
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectEmployeeRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteEmployeeRequest";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EmpReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertEmployeeRequest";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
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
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecretariatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecretariatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyCodePC", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PropertyCodePC", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateEmployeeRequest";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
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
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecretariatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SecretariatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EmpReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EmpReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyCodePC", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PropertyCodePC", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNoPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNoPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AcceptorIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AcceptorIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalIdPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalIdPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComPortPcPos2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ComPortPcPos2", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblEmployeeRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int EmpReId)
        {
            this.Adapter.SelectCommand.Parameters["@EmpReId"].Value = EmpReId;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TSP.DataManager.TableCodes.EmployeeRequest;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByEmpolyee(int EmpId)
        {
            this.Adapter.SelectCommand.Parameters["@EmpReId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = EmpId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TSP.DataManager.TableCodes.EmployeeRequest;

            this.Adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="IsConfirmed">0:unNoun , 1:Was Confirmed , 2:Was not Confirmed</param>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectLastVersion(int EmpId, int IsConfirmed)
        {
            this.Adapter.SelectCommand.CommandText = "spSelectEmployeeRequestLastVersion";
            this.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.AddWithValue("@EmpId", EmpId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@IsConfirmed", IsConfirmed);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TSP.DataManager.TableCodes.EmployeeRequest);

            Fill();
        }
    }
}
