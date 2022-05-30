using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
   public class TicketingManager:BaseObject
    {
       public override System.Data.DataTable DataTable
       {
           get
           {
               if ((this._dataTable == null))
               {

                   this._dataTable = new DataManager.NezamFarsDataSet.tblTicketingDataTable();
                   this.DataSet.Tables.Add(this._dataTable);
               }

               return this._dataTable;
           }
       }

       protected override void InitAdapter()
       {
           
           global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
           tableMapping.SourceTable = "Table";
           tableMapping.DataSetTable = "tblTicketing";
           tableMapping.ColumnMappings.Add("TicketId", "TicketId");
           tableMapping.ColumnMappings.Add("Status", "Status");
           tableMapping.ColumnMappings.Add("PartId", "PartId");
           tableMapping.ColumnMappings.Add("PageLink", "PageLink");
           tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
           tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
           tableMapping.ColumnMappings.Add("UserId", "UserId");
           tableMapping.ColumnMappings.Add("ParentId", "ParentId");
           tableMapping.ColumnMappings.Add("PriorityDev", "PriorityDev");
           tableMapping.ColumnMappings.Add("Type", "Type");
           tableMapping.ColumnMappings.Add("ScaleDev", "ScaleDev");
           tableMapping.ColumnMappings.Add("SchedulingStartDate", "SchedulingStartDate");
           tableMapping.ColumnMappings.Add("ActualStartDate", "ActualStartDate");
           tableMapping.ColumnMappings.Add("ActualTime", "ActualTime");
           tableMapping.ColumnMappings.Add("Subject", "Subject");
           tableMapping.ColumnMappings.Add("Dscription", "Dscription");
           tableMapping.ColumnMappings.Add("PriorityUser", "PriorityUser");
           tableMapping.ColumnMappings.Add("AttachmentUrl", "AttachmentUrl");
           tableMapping.ColumnMappings.Add("Keywords", "Keywords");
           tableMapping.ColumnMappings.Add("TypeUser", "TypeUser");
           tableMapping.ColumnMappings.Add("WorkflowsStateId", "WorkflowsStateId");
           this.Adapter.TableMappings.Add(tableMapping);
           this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
           this.Adapter.SelectCommand.Connection = this.Connection;
           this.Adapter.SelectCommand.CommandText = "dbo.SelectTicketing";
           this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
  
           this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
           this.Adapter.DeleteCommand.Connection = this.Connection;
           this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTicketing";
           this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PartId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PartId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_PriorityDev", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriorityDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Type", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ScaleDev", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ScaleDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_SchedulingStartDate", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_SchedulingStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ActualStartDate", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ActualStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ActualTime", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ActualTime", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriorityUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityUser", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_AttachmentUrl", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AttachmentUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Keywords", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Keywords", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_TypeUser", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TypeUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
           this.Adapter.InsertCommand.Connection = this.Connection;
           this.Adapter.InsertCommand.CommandText = "dbo.InsertTicketing";
           this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PartId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PartId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PageLink", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PageLink", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ScaleDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SchedulingStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ActualStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ActualTime", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Dscription", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Dscription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityUser", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AttachmentUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Keywords", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TypeUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
           this.Adapter.UpdateCommand.Connection = this.Connection;
           this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTicketing";
           this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PartId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PartId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PageLink", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PageLink", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ScaleDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SchedulingStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ActualStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ActualTime", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Dscription", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Dscription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityUser", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AttachmentUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Keywords", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TypeUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PartId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PartId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_PriorityDev", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriorityDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityDev", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Type", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ScaleDev", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ScaleDev", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ScaleDev", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_SchedulingStartDate", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_SchedulingStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SchedulingStartDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ActualStartDate", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ActualStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualStartDate", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_ActualTime", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ActualTime", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ActualTime", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Subject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Subject", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriorityUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityUser", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_AttachmentUrl", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AttachmentUrl", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AttachmentUrl", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Keywords", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Keywords", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Keywords", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_TypeUser", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TypeUser", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TypeUser", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_WorkflowsStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkflowsStateId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
           this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TicketId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
       }
    }
}
