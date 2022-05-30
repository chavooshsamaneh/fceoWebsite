using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager.TechnicalServices
{
    public enum TSWorkRequestConditionErrorType
    {
        DocumentExipration = 0
    }

    public class ObserverWorkRequestManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSWorkRequest);
        }
        public static Permission GetUserPermissionSetShahraksanati(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSSetShahraksanati);
        }
        public static Permission GetUserPermissionSetShirazObsMunicipalityMeterForOtherAgents(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSSetShirazObsMunicipalityMeterForOtherAgents);
        }
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSObserverWorkRequest";
            tableMapping.ColumnMappings.Add("ObsWorkReqId", "ObsWorkReqId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MfId", "MfId");//تایید پروانه و ثبت ناظر وطراح
            tableMapping.ColumnMappings.Add("ObsId", "ObsId");//تایید پروانه و ثبت ناظر وطراح
            tableMapping.ColumnMappings.Add("ObsDate", "ObsDate");//تایید پروانه و ثبت ناظر وطراح
            tableMapping.ColumnMappings.Add("WantedWorkType", "WantedWorkType");
            tableMapping.ColumnMappings.Add("MembershipDate", "MembershipDate");
            tableMapping.ColumnMappings.Add("Group1", "Group1");
            tableMapping.ColumnMappings.Add("Group2", "Group2");
            tableMapping.ColumnMappings.Add("Group3", "Group3");
            tableMapping.ColumnMappings.Add("Group4", "Group4");
            tableMapping.ColumnMappings.Add("CapacityAssignmentId", "CapacityAssignmentId");
            tableMapping.ColumnMappings.Add("City1", "City1");
            tableMapping.ColumnMappings.Add("City2", "City2");
            tableMapping.ColumnMappings.Add("MeAgentId", "MeAgentId");
            tableMapping.ColumnMappings.Add("IsObserverOff", "IsObserverOff");
            tableMapping.ColumnMappings.Add("StartOffDate", "StartOffDate");
            tableMapping.ColumnMappings.Add("EndOffDate", "EndOffDate");
            tableMapping.ColumnMappings.Add("MfMjId", "MfMjId");//اصلا مقدار نمی گیرد
            tableMapping.ColumnMappings.Add("DocMeFileExpireDate", "DocMeFileExpireDate");//تایید پروانه و ثبت ناظر وطراح
            tableMapping.ColumnMappings.Add("MasterMfMjParentId", "MasterMfMjParentId");//کاربر انتخاب می کند
            tableMapping.ColumnMappings.Add("CurrentWfTasId", "CurrentWfTasId");
            tableMapping.ColumnMappings.Add("CurrentWfStateId", "CurrentWfStateId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("IsFullTimeWorker", "IsFullTimeWorker");
            tableMapping.ColumnMappings.Add("WantCharityWork", "WantCharityWork");
            tableMapping.ColumnMappings.Add("WantShahrakSanatiMeter", "WantShahrakSanatiMeter");
            tableMapping.ColumnMappings.Add("SuburbsMeter", "SuburbsMeter");
            tableMapping.ColumnMappings.Add("ShahrakSanatiMeter", "ShahrakSanatiMeter");
            tableMapping.ColumnMappings.Add("KhanZenyanObserveMeter", "KhanZenyanObserveMeter");//حد نصاب برای ثبت ناظر
            tableMapping.ColumnMappings.Add("LapooyObserveMeter", "LapooyObserveMeter");//حد نصاب برای ثبت ناظر
            tableMapping.ColumnMappings.Add("ZarghanObserveMeter", "ZarghanObserveMeter");//حد نصاب برای ثبت ناظر
            tableMapping.ColumnMappings.Add("DareyonObserveMeter", "DareyonObserveMeter");//حد نصاب برای ثبت ناظر

            tableMapping.ColumnMappings.Add("ShirazMunicipalityMeter", "ShirazMunicipalityMeter");
            tableMapping.ColumnMappings.Add("ShirazMunicipalityDesignMeter", "ShirazMunicipalityDesignMeter");
            tableMapping.ColumnMappings.Add("ShirazMunicipulityUrbenismTarh", "ShirazMunicipulityUrbenismTarh");
            tableMapping.ColumnMappings.Add("ShirazMunicipulityUrbenismEntebaghShahri", "ShirazMunicipulityUrbenismEntebaghShahri");
            ///////  مصرفی بنیاد
            tableMapping.ColumnMappings.Add("BonyadMaskan", "BonyadMaskan");
            tableMapping.ColumnMappings.Add("BonyadMaskanDesignMeter", "BonyadMaskanDesignMeter");
            #region  Used-Capacity Design/Obs/Urbenism  /////به منظور حفظ حد نصاب ها ظرفیت های شهرداری شهرهای مختلف به کار میروند
            //////مصرفی طراحی
            tableMapping.ColumnMappings.Add("UsedCapacity", "UsedCapacity");// ثبت ناظر وطراح//کل مصرفی 
            tableMapping.ColumnMappings.Add("UsedCapacityCharity", "UsedCapacityCharity");// ثبت ناظر وطراح استفاده در تابع ارجاع نظارت*

            /////////////////////////////////////////////////فقط بابت گزارش گیری در صفحات گزارشات استفاده شود////////////////////////////////////////////////////////
            tableMapping.ColumnMappings.Add("UsedCapacityDesShirazMun", "UsedCapacityDesShirazMun");// ثبت طراح
            tableMapping.ColumnMappings.Add("UsedCapacityDesOtherCities", "UsedCapacityDesOtherCities");// ثبت طراح

            tableMapping.ColumnMappings.Add("UsedCapacityObsShiraz", "UsedCapacityObsShiraz");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsSadra", "UsedCapacityObsSadra");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsLapooy", "UsedCapacityObsLapooy");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsKhanZenyan", "UsedCapacityObsKhanZenyan");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsDareyon", "UsedCapacityObsDareyon");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsZarghan", "UsedCapacityObsZarghan");// ثبت ناظر 
            tableMapping.ColumnMappings.Add("UsedCapacityObsOtherCities", "UsedCapacityObsOtherCities");// ثبت ناظر 

            ////شهرسازی
            tableMapping.ColumnMappings.Add("UsedCapacityUrbenismTarhShirazMun", "UsedCapacityUrbenismTarhShirazMun");// ثبت طراح 
            tableMapping.ColumnMappings.Add("UsedCapacityEntebaghShahriShirazMun", "UsedCapacityEntebaghShahriShirazMun");// ثبت طراح 
            tableMapping.ColumnMappings.Add("UsedCapacityUrbenismTarhOtherCities", "UsedCapacityUrbenismTarhOtherCities");// ثبت طراح 
            tableMapping.ColumnMappings.Add("UsedCapacityEntebaghShahriOtherCities", "UsedCapacityEntebaghShahriOtherCities");// ثبت طراح  
                                                                                                                              ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            ///////////////////////////
            #region Remain Capacity
            tableMapping.ColumnMappings.Add("RemainCapacity", "RemainCapacity");// ثبت ناظر وطراح//ظرفیت باقیمانده کل        
            tableMapping.ColumnMappings.Add("RemainCapacityDesign", "RemainCapacityDesign");// ثبت ناظر وطراح//ظرفیت باقیمانده طراحی
            ///////////
            tableMapping.ColumnMappings.Add("RemainCapacityObs", "RemainCapacityObs");// ثبت ناظر //ظرفیت باقیمانده نظارت  استفاده در تابع ارجاع نظارت
            tableMapping.ColumnMappings.Add("RemainCapacityObsReal", "RemainCapacityObsReal");// ثبت ناظر //ظرفیت باقیمانده نظارت واقعی  استفاده در تابع ارجاع نظارت//min((ObsCapacity-UsedObsCapacity),(totalCap-UsedObs-UsedDesign))

            #endregion
            //////////////
            tableMapping.ColumnMappings.Add("PercentOfCapacityUsage", "PercentOfCapacityUsage");// ثبت ناظر وطراح   //درصد استفاده//برای تابع ارجاع کار  
            //////
            tableMapping.ColumnMappings.Add("TotalCapacity", "TotalCapacity");//بالاترین ظرفیت نظارت و طراحی
            tableMapping.ColumnMappings.Add("CapacityObs", "CapacityObs");//بالاترین ظرفیت نظارت
            tableMapping.ColumnMappings.Add("CapacityDesign", "CapacityDesign");//بالاترین ظرفیت طراحی
                                                                                /////////////////////////////
            #region Count Work
            tableMapping.ColumnMappings.Add("CountUnder400MeterWork", "CountUnder400MeterWork");// ثبت ناظر //کار زیر400 نظارت
            tableMapping.ColumnMappings.Add("CountUnder400MeterWorkDesign", "CountUnder400MeterWorkDesign");// ثبت ناظر وطراح   //کار زیر 400 طراحی
            tableMapping.ColumnMappings.Add("CountRemainWorkCount", "CountRemainWorkCount");// ثبت ناظر وطراح//تعداد کارهای باقیمانده
            tableMapping.ColumnMappings.Add("CountWorks", "CountWorks");//تعداد کل کارهای مجاز
            tableMapping.ColumnMappings.Add("CountInproccesWorks", "CountInproccesWorks");// ثبت ناظر و اتمام کار پروژه//تعداد کار در دست ناظر
            tableMapping.ColumnMappings.Add("CountRandomSelected", "CountRandomSelected");//تعداد کار انتخاب تصادفی
            tableMapping.ColumnMappings.Add("CountRejectByObs", "CountRejectByObs");// رد کردن کار توسط ناظر
            //شاید دو فیلد زیر بدرد محاسبات نخورد-با تعداد کار نظارت در دست
            tableMapping.ColumnMappings.Add("CountInproccesWorksDesign", "CountInproccesWorksDesign");//تعداد کار در دست نظارت
            tableMapping.ColumnMappings.Add("CountInproccesWorksObs", "CountInproccesWorksObs");//تعداد کار در دست طراحی
            #endregion
            tableMapping.ColumnMappings.Add("UrlAttachment", "UrlAttachment");
            tableMapping.ColumnMappings.Add("HasGasCert", "HasGasCert");
            tableMapping.ColumnMappings.Add("CityLastChangedWorkYear", "CityLastChangedWorkYear");
            tableMapping.ColumnMappings.Add("WantEghdamMeliMaskan", "WantEghdamMeliMaskan");
            tableMapping.ColumnMappings.Add("UrlObserverCommitmentForm", "UrlObserverCommitmentForm");
            this.Adapter.TableMappings.Add(tableMapping);

            #region sp

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectTSObserverWorkRequest";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ObsWorkReqId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CapacityAssignmentId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DateOfToday", System.Data.SqlDbType.NChar);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTSObserverWorkRequest";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ObsWorkReqId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            //******دقت شود قیلدFirstRegisterDateTime در spInsert باید حذف شود
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertTSObserverWorkRequest";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantedWorkType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantedWorkType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MembershipDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group1", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group2", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group3", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group3", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group4", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group4", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeAgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeAgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverOff", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsObserverOff", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfMjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfMjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocMeFileExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocMeFileExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MasterMfMjParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfTasId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfTasId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            ////this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FirstRegisterDateTime", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FirstRegisterDateTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsFullTimeWorker", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFullTimeWorker", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantCharityWork", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantCharityWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantShahrakSanatiMeter", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SuburbsMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SuburbsMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShahrakSanatiMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KhanZenyanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "KhanZenyanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LapooyObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LapooyObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ZarghanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ZarghanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DareyonObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DareyonObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskanDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskanDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismTarh", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismTarh", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityDesShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityDesShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityDesOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityDesOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsShiraz", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsShiraz", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsSadra", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsSadra", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsLapooy", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsLapooy", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsKhanZenyan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsKhanZenyan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsDareyon", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsDareyon", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsZarghan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsZarghan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityUrbenismTarhShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityUrbenismTarhShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityEntebaghShahriShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityEntebaghShahriShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityUrbenismTarhOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityUrbenismTarhOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityEntebaghShahriOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityEntebaghShahriOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityCharity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityCharity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityObsReal", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityObsReal", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PercentOfCapacityUsage", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PercentOfCapacityUsage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRemainWorkCount", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRemainWorkCount", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountUnder400MeterWork", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountUnder400MeterWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountUnder400MeterWorkDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountUnder400MeterWorkDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRandomSelected", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRandomSelected", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRejectByObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRejectByObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlAttachment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlAttachment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasGasCert", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorksObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorksObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorksDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorksDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CityLastChangedWorkYear", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CityLastChangedWorkYear", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantEghdamMeliMaskan", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantEghdamMeliMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlObserverCommitmentForm", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlObserverCommitmentForm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTSObserverWorkRequest";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantedWorkType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantedWorkType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MembershipDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group1", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group2", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group3", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group3", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group4", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group4", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeAgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeAgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverOff", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsObserverOff", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfMjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfMjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocMeFileExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocMeFileExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MasterMfMjParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfTasId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfTasId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsFullTimeWorker", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFullTimeWorker", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantCharityWork", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantCharityWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantShahrakSanatiMeter", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SuburbsMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SuburbsMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShahrakSanatiMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KhanZenyanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "KhanZenyanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LapooyObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LapooyObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ZarghanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ZarghanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DareyonObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DareyonObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskanDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskanDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismTarh", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismTarh", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityDesShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityDesShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityDesOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityDesOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsShiraz", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsShiraz", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsSadra", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsSadra", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsLapooy", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsLapooy", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsKhanZenyan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsKhanZenyan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsDareyon", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsDareyon", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsZarghan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsZarghan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityObsOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityObsOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityUrbenismTarhShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityUrbenismTarhShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityEntebaghShahriShirazMun", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityEntebaghShahriShirazMun", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityUrbenismTarhOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityUrbenismTarhOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityEntebaghShahriOtherCities", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityEntebaghShahriOtherCities", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsedCapacityCharity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsedCapacityCharity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityObsReal", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityObsReal", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainCapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainCapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PercentOfCapacityUsage", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PercentOfCapacityUsage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRemainWorkCount", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRemainWorkCount", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountUnder400MeterWork", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountUnder400MeterWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountUnder400MeterWorkDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountUnder400MeterWorkDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRandomSelected", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRandomSelected", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRejectByObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRejectByObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlAttachment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlAttachment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasGasCert", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorksObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorksObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorksDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorksDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CityLastChangedWorkYear", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CityLastChangedWorkYear", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantEghdamMeliMaskan", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantEghdamMeliMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlObserverCommitmentForm", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlObserverCommitmentForm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ObsWorkReqId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            #endregion
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSObserverWorkRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int ObsWorkReqId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ObsWorkReqId"].Value = ObsWorkReqId;
            this.Adapter.SelectCommand.Parameters["@DateOfToday"].Value = Utility.GetDateOfToday();
            Fill();
        }

        public void FindByMeId(int MeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@DateOfToday"].Value = Utility.GetDateOfToday();
            Fill();
        }
        //public void FindByCapacityAssignmentId(int CapacityAssignmentId)
        //{
        //    ResetAllParameters();
        //    this.Adapter.SelectCommand.Parameters["@CapacityAssignmentId"].Value = CapacityAssignmentId;
        //    Fill();
        //}
        public void FindByCapacityAssignmentId(int CapacityAssignmentId, int MeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CapacityAssignmentId"].Value = CapacityAssignmentId;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@DateOfToday"].Value = Utility.GetDateOfToday();
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int MeId, string CreateDateFrom, string CreateDateTo, string MFNo, int CurrentAgentCode, int AgentId, int MJParentId, int TaskId)
        {
            if (MeId == -1 && MFNo == "%" && CreateDateFrom == "1" && CreateDateTo == "2" && AgentId == -1 && MJParentId == -1 && TaskId == -1)
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestFormanagmentPage", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@MFNo", MFNo);
            adapter.SelectCommand.Parameters.AddWithValue("@CurrentAgentCode", CurrentAgentCode);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@MJParentId", MJParentId);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);

            adapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="_CurrentCapacityEndate"></param>
        /// <param name="_CurrentCapacityAssignmentId"></param>
        /// <returns> 
        /// ArrayResult[0] =true/false
        /// ArrayResult[1] = Msg
        /// ArrayResult[2] = MemberFileId;
        /// ArrayResult[3] = ExpireDate;
        /// ArrayResult[4] =MjParentId;
        /// ArrayResult[5] = TSWorkRequestConditionErrorType;</returns>
        public System.Collections.ArrayList CheckConditionForNewObsWorkRequest(int MeId, string _CurrentCapacityEndate, int _CurrentCapacityAssignmentId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager MemberRestrictionForObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager();
            TSP.DataManager.ConflictManager ConflictManager = new ConflictManager();
            System.Collections.ArrayList ArrayResult = new System.Collections.ArrayList();
            ArrayResult.Add(true);
            ArrayResult.Add("");
            ArrayResult.Add(-2);
            ArrayResult.Add("");
            ArrayResult.Add("");
            ArrayResult.Add(-2);

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "کد عضویت وارد شده معتبر نمی باشد.";
                return ArrayResult;
            }
            ObserverWorkRequestManager.FindByCapacityAssignmentId(-1, MeId);
            if (ObserverWorkRequestManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "برای عضویت " + MeId.ToString() + "آماده بکاری ثبت شده است";
                return ArrayResult;
            }

            if (Utility.IsMemberMunicipulityTaxCheckForWorkRequest() && Convert.ToInt32(MemberManager[0]["AgentId"]) == Utility.GetCurrentAgentCode())
            {
                ObserverTaxManager ObserverTaxManager = new ObserverTaxManager();
                DataTable dtTax = ObserverTaxManager.FindObserverTaxByMeId(MeId, "%");
                if (dtTax.Rows.Count == 0)
                {
                    ArrayResult[0] = false;
                    ArrayResult[1] = "امکان ثبت آماده بکاری برای عضویت " + MeId.ToString() + " وجود ندارد.عوارض شهرداری توسط شما پرداخت نشده است.جهت پرداخت به سایت esup.shiraz.ir مراجعه نمایید.";
                    return ArrayResult;
                }
            }
            string Msg = "";
            if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
            {
                ArrayResult[0] = false;
                ArrayResult[1] = Msg;
                return ArrayResult;
            }
            ConflictManager.FindByMeId(MeId, (int)ConflictTypeCode.TSObserverWorkRequest, 0);
            if (ConflictManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "امکان ثبت آماده بکاری برای عضویت" + MeId.ToString() + " وجود ندارد.اعلام مغایرت ثبت شده توسط شما هنوز پاسخ داده نشده است";
                return ArrayResult;
            }
            MemberRestrictionForObserverWorkRequestManager.FindActiveMembersId(MeId);
            if (MemberRestrictionForObserverWorkRequestManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "بدلیل  " + MemberRestrictionForObserverWorkRequestManager[0]["TypeName"].ToString() + " امکان ثبت آماده بکاری برای عضویت" + MeId.ToString() + " وجود ندارد";
                return ArrayResult;
            }
            DataTable dtMemberOfImpOffice = OfficeMemberManager.IsMemberOfImpOffice(MeId);
            if (dtMemberOfImpOffice.Rows.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "به دلیل عضویت در مجری حقوقی امکان ثبت آماده به کاری وجود ندارد ";
                return ArrayResult;
            }

            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ArrayResult;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت " + MeId.ToString() + " دارای بدهی به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ArrayResult;
            }

            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ArrayResult;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت " + MeId.ToString() + " دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ArrayResult;
            }            
            DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, 1, 0);

            if (DocMemberFileManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "به دلیل داشتن مجوز اجرا امکان ثبت آماده به کاری برای عضویت " + MeId.ToString() + "وجود ندارد.";
                return ArrayResult;
            }
            #region CheckMemberFile
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت " + MeId.ToString() + " دارای پروانه اشتغال به کار تایید شده نمی باشد.";
                return ArrayResult;
            }
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

            ArrayResult[2] = MemberFileId;
            ArrayResult[3] = dtMeFile.Rows[0]["ExpireDate"].ToString();
            ArrayResult[4] = dtMeFile.Rows[0]["MjParentId"].ToString();
            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            if (!(string.Compare(ExpireDate, "1398/11/01") > 0 && string.Compare(ExpireDate, "1400/04/31") <= 0))
            {
                if (!string.IsNullOrEmpty(ExpireDate))
                {
                    if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                    {
                        ArrayResult[0] = false;
                        ArrayResult[1] = "بدلیل اتمام مدت اعتبار پروانه اشتغال امکان ثبت آماده به کاری  وجود ندارد.";
                        ArrayResult[5] = (int)TSWorkRequestConditionErrorType.DocumentExipration;
                        return ArrayResult;
                    }
                }
            }
            //ArrayResult
            if (ExpireDate.CompareTo(_CurrentCapacityEndate) < 0)
            {
                ArrayResult[1] = "هشدار:تاریخ اعتبار پروانه عضویت " + MeId.ToString() + " قبل از پایان این دوره از اختصاص ظرفیت(قبل از تاریخ " + _CurrentCapacityEndate + "به پایان می رسد.پس از پایان یافتن مهلت اعتبار پروانه فرآیند ارجاع کار نظارت از طریق سامانه نظام مهندسی ساختمان متوقف خواهد شد";

            }
            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                //DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                //if (DocMemberFileMajorManager.Count <= 0)
                //{
                //    ShowMessage("رشته موضوع پروانه شخص انتخاب شده نامشخص است");
                //    return false;
                //}
            }
            else
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "وضعیت پروانه اشتغال  عضویت " + MeId.ToString() + " نامشخص می باشد";
            }
            #endregion

            return ArrayResult;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="_CurrentCapacityEndate"></param>
        /// <param name="_CurrentCapacityAssignmentId"></param>
        /// <returns> 
        /// ArrayResult[0] =true/false
        /// ArrayResult[1] = Msg
        /// ArrayResult[2] = MemberFileId;
        /// ArrayResult[3] = ExpireDate;
        /// ArrayResult[4] =MjParentId;</returns>
        public System.Collections.ArrayList CheckConditionForChangeWorkRequest(int MeId, string _CurrentCapacityEndate)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager MemberRestrictionForObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager();
            TSP.DataManager.ConflictManager ConflictManager = new ConflictManager();
            System.Collections.ArrayList ArrayResult = new System.Collections.ArrayList();
            ArrayResult.Add(true);
            ArrayResult.Add("");
            ArrayResult.Add(-2);
            ArrayResult.Add("");
            ArrayResult.Add("");
            ArrayResult.Add("");
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "کد عضویت وارد شده معتبر نمی باشد.";
                return ArrayResult;
            }
            ObserverWorkRequestManager.FindByCapacityAssignmentId(-1, MeId);
            if (ObserverWorkRequestManager.Count <= 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "برای عضویت" + MeId.ToString() + " آماده بکاری ثبت نشده است.در صورت تمایل به ثبت آماده بکاری از طریق لینک اعلام آماده بکاری موجود در همین صفحه اقدام به ثبت آماده به کاری نمایید";
                return ArrayResult;
            }

            //if (Utility.IsMemberMunicipulityTaxCheckForWorkRequest() && Convert.ToInt32(MemberManager[0]["AgentId"]) == Utility.GetCurrentAgentCode())
            //{
            //    ObserverTaxManager ObserverTaxManager = new ObserverTaxManager();
            //    DataTable dtTax = ObserverTaxManager.FindObserverTaxByMeId(MeId, "%");
            //    if (dtTax.Rows.Count == 0)
            //    {
            //        ArrayResult[0] = false;
            //        ArrayResult[1] = "امکان ثبت آماده بکاری برای شما وجود ندارد.عوارض شهرداری توسط شما پرداخت نشده است.جهت پرداخت به سایت esup.shiraz.ir مراجعه نمایید.";
            //        return ArrayResult;
            //    }
            //}
            string Msg = "";
            if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
            {
                ArrayResult[0] = false;
                ArrayResult[1] = Msg;
                return ArrayResult;
            }
            ConflictManager.FindByMeId(MeId, (int)ConflictTypeCode.TSObserverWorkRequest, 0);
            if (ConflictManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "امکان ثبت آماده به کاری برای عضویت" + MeId.ToString() + " وجود ندارد.اعلام مغایرت ثبت شده توسط نامبرده هنوز پاسخ داده نشده است";
                return ArrayResult;
            }
            MemberRestrictionForObserverWorkRequestManager.FindActiveMembersId(MeId);
            if (MemberRestrictionForObserverWorkRequestManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "بدلیل  " + MemberRestrictionForObserverWorkRequestManager[0]["TypeName"].ToString() + " امکان ثبت آماده بکاری برای عضویت" + MeId.ToString() + " وجود ندارد";
                return ArrayResult;
            }
            DataTable dtMemberOfImpOffice = OfficeMemberManager.IsMemberOfImpOffice(MeId);
            if (dtMemberOfImpOffice.Rows.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "به دلیل عضویت در مجری حقوقی امکان ثبت آماده به کاری وجود ندارد ";
                return ArrayResult;
            }

            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ArrayResult;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت" + MeId.ToString() + " دارای بدهی به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ArrayResult;
            }

            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ArrayResult;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت" + MeId.ToString() + " دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ArrayResult;
            }

            DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, 1, 0);

            if (DocMemberFileManager.Count > 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "به دلیل داشتن مجوز اجرا امکان ثبت آماده به کاری برای شماوجود ندارد.";
                return ArrayResult;
            }
            #region CheckMemberFile
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "عضویت" + MeId.ToString() + " دارای پروانه اشتغال به کار تایید شده نمی باشد.";
                return ArrayResult;
            }
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            ArrayResult[2] = MemberFileId;
            ArrayResult[3] = dtMeFile.Rows[0]["ExpireDate"].ToString();
            ArrayResult[4] = dtMeFile.Rows[0]["MjParentId"].ToString();

            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            if (!(string.Compare(ExpireDate, "1398/11/01") > 0 && string.Compare(ExpireDate, "1400/04/31") <= 0))
            {
                if (!string.IsNullOrEmpty(ExpireDate))
                {
                    if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                    {
                        ArrayResult[0] = false;
                        ArrayResult[1] = "بدلیل اتمام مدت اعتبار پروانه اشتغال امکان ثبت درخواست تغییرات آماده به کاری  وجود ندارد.";
                        ArrayResult[5] = (int)TSP.DataManager.TechnicalServices.TSWorkRequestConditionErrorType.DocumentExipration;
                        return ArrayResult;
                    }
                }
            }
            //ArrayResult

            if (ExpireDate.CompareTo(_CurrentCapacityEndate) < 0)
            {
                ArrayResult[1] = "هشدار:تاریخ اعتبار پروانه عضویت" + MeId.ToString() + " قبل از پایان این دوره از اختصاص ظرفیت(قبل از تاریخ " + _CurrentCapacityEndate + "به پایان می رسد.پس از پایان یافتن مهلت اعتبار پروانه فرآیند ارجاع کار نظارت از طریق سامانه نظام مهندسی ساختمان متوقف خواهد شد";

            }
            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                //DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                //if (DocMemberFileMajorManager.Count <= 0)
                //{
                //    ShowMessage("رشته موضوع پروانه شخص انتخاب شده نامشخص است");
                //    return false;
                //}
            }
            else
            {
                ArrayResult[0] = false;
                ArrayResult[1] = "وضعیت پروانه اشتغال  شما نامشخص می باشد";
            }
            #endregion

            return ArrayResult;
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSObserverWorkRequestBasedFunctionA(int OwnerMeId, string DateOfToday, int ProjectId, int IsCharity, int ProjectCitId, int ProjectGroupId, string MajorList
            , int NearestGradId, int ProjectAgentId, Boolean CheckRemainCapacityObsRealNotZero, int ReturnRejectedObserver, Boolean IsShahrakSanati, Boolean IsEghdamMeliMaskan)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestBasedFunction", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@OwnerMeId", OwnerMeId);
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", DateOfToday);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsCharity", IsCharity);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectCitId", ProjectCitId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectGroupId", ProjectGroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@MajorList", MajorList);
            adapter.SelectCommand.Parameters.AddWithValue("@NearestGradId", NearestGradId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectAgentId", ProjectAgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@ReturnRejectedObserver", ReturnRejectedObserver);
            adapter.SelectCommand.Parameters.AddWithValue("@CheckRemainCapacityObsRealNotZero", Convert.ToInt32(CheckRemainCapacityObsRealNotZero));
            adapter.SelectCommand.Parameters.AddWithValue("@IsShahrakSanati", Convert.ToInt32(IsShahrakSanati));
            adapter.SelectCommand.Parameters.AddWithValue("@IsEghdamMeliMaskan", Convert.ToInt32(IsEghdamMeliMaskan));
            adapter.Fill(dt);

            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSObserverWorkRequestBasedFunctionA(int OwnerMeId, string DateOfToday, int ProjectId, int IsCharity, int ProjectCitId, int ProjectGroupId, string MajorList
         , int NearestGradId, int ProjectAgentId, Boolean CheckRemainCapacityObsRealNotZero, Boolean IsShahrakSanati, Boolean IsEghdamMeliMaskan)
        {
            return SelectTSObserverWorkRequestBasedFunctionA(OwnerMeId, DateOfToday, ProjectId, IsCharity, ProjectCitId, ProjectGroupId, MajorList
            , NearestGradId, ProjectAgentId, CheckRemainCapacityObsRealNotZero, -1, IsShahrakSanati, IsEghdamMeliMaskan);
        }

        public DataTable SelectTSObserverWorkRequestByMember(int MeId, TSP.DataManager.TSObserverWorkRequestStatus TSObserverWorkRequestStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestByMember", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@Status", (int)TSObserverWorkRequestStatus);

            adapter.Fill(dt);

            return dt;
        }

        public DataTable SelectTSObserverWorkRequestFullInfoByMember(int MeId, TSP.DataManager.TSObserverWorkRequestStatus TSObserverWorkRequestStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestFullInfoByMember", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@Status", (int)TSObserverWorkRequestStatus);

            adapter.Fill(dt);

            return dt;
        }

        public DataTable SelectForUpdateWorkRequest()
        {
            //   DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectForUpdateWorkRequest", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.Fill(this.DataTable);

            return this.DataTable;
        }

    }
}
