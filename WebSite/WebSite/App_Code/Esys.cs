using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;

/// <summary>
/// Summary description for Esys
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Esys : System.Web.Services.WebService
{

    private string _UserName
    {
        get
        {
            return "SNMFEsysInfo";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFEsysInfo";
        }
    }
    public Esys()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    #region GetMemberInfo
    private class MeInfo
    {
        public string Status, Message, MeNo, MembershipDate, FirstName, LastName, TitleName, FatherName, CityName,
            MeliCardNo, ShenasnamehNo, BirhtDate, MaritalStatusName, MemberStatus, SoldirStatus, BirthPlace, AgentName,
            AgentId, SexName, BankAccNo, Email, FileNo, ImpGrade, ImpGradeDate, ObsGrade, ObsGradeDate, DesGrade, DesGradeDate,
            UrbanismGrade, UrbanismGradeDate, MappingGrade, MappingGradeDate, TrafficGrade, TrafficGradeDate, LicenceNameMemberMaster,
            LicenceIdMemberMaster, MajorNameMemberMaster, UniversityNameMemberMaster, GraduateDateMemberMaster, MjParentName, MjParentId,
            MajorNameDocumentMaster, DocumntFirstRegister, DocumntExpireDate, DocumentRevivalDate, ImageUrl, MobileNo, WorkFlowName, DocMajorParentName
            , LicenceCount, HasImplementCertificate;
    }
    [WebMethod]
    public string GetMemberInfo(Int32 MeId, string UserName, string PassWord)
    {
        MeInfo meInfo = new MeInfo();

        if (UserName != _UserName || PassWord != _PassWord)
        {
            meInfo.Status = "-3";
            meInfo.Message = "UserNameC or PassWordC is not valid!";
            return new JavaScriptSerializer().Serialize(meInfo);
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMemberManager = MemberManager.SelectMemberInfoForEsys(MeId);
        int Count = dtMemberManager.Rows.Count;

        if (Count < 1)
        {
            meInfo.Status = "-2";
            meInfo.Message = "The Member's UserName Is not valid";
            return new JavaScriptSerializer().Serialize(meInfo);

        }
        if (Count > 1)
        {
            meInfo.Status = "-1";
            meInfo.Message = "Member's information is corrupted.Please Call DB administrator.";
            return new JavaScriptSerializer().Serialize(meInfo);

        }
        if (Count == 1)
        {
            #region
            meInfo.Status = "0";
            meInfo.Message = "OK!";
            meInfo.MeNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeNo"])) ? dtMemberManager.Rows[0]["MeNo"].ToString() : "";
            meInfo.MembershipDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MembershipDate"])) ? dtMemberManager.Rows[0]["MembershipDate"].ToString() : "";
            meInfo.FirstName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FirstName"])) ? dtMemberManager.Rows[0]["FirstName"].ToString() : "";
            meInfo.LastName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LastName"])) ? dtMemberManager.Rows[0]["LastName"].ToString() : "";
            meInfo.TitleName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TitleName"])) ? dtMemberManager.Rows[0]["TitleName"].ToString() : "";
            meInfo.FatherName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FatherName"])) ? dtMemberManager.Rows[0]["FatherName"].ToString() : "";
            //   meInfo.CityName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["CityName"])) ? dtMemberManager.Rows[0]["CityName"].ToString() : "";
            meInfo.MeliCardNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeliCardNo"])) ? dtMemberManager.Rows[0]["MeliCardNo"].ToString() : "";
            meInfo.ShenasnamehNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ShenasnamehNo"])) ? dtMemberManager.Rows[0]["ShenasnamehNo"].ToString() : "";
            meInfo.BirhtDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BirhtDate"])) ? dtMemberManager.Rows[0]["BirhtDate"].ToString() : "";
            //meInfo.MaritalStatusName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MaritalStatusName"])) ? dtMemberManager.Rows[0]["MaritalStatusName"].ToString() : "";
            meInfo.MemberStatus = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MemberStatus"])) ? dtMemberManager.Rows[0]["MemberStatus"].ToString() : "";
            //meInfo.SoldirStatus = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["soldirStatus"])) ? dtMemberManager.Rows[0]["soldirStatus"].ToString() : "";
            //meInfo.BirthPlace = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BirthPlace"])) ? dtMemberManager.Rows[0]["BirthPlace"].ToString() : "";
            meInfo.AgentName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentName"])) ? dtMemberManager.Rows[0]["AgentName"].ToString() : "";
            meInfo.AgentId = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentId"])) ? dtMemberManager.Rows[0]["AgentId"].ToString() : "";
            meInfo.SexName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SexName"])) ? dtMemberManager.Rows[0]["SexName"].ToString() : "";
            //meInfo.BankAccNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BankAccNo"])) ? dtMemberManager.Rows[0]["BankAccNo"].ToString() : "";
            meInfo.Email = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["Email"])) ? dtMemberManager.Rows[0]["Email"].ToString() : "";
            meInfo.FileNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FileNo"])) ? dtMemberManager.Rows[0]["FileNo"].ToString() : "";
            meInfo.ImpGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGrade"])) ? dtMemberManager.Rows[0]["ImpGrade"].ToString() : "";
            //meInfo.ImpGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGradeDate"])) ? dtMemberManager.Rows[0]["ImpGradeDate"].ToString() : "";
            meInfo.ObsGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGrade"])) ? dtMemberManager.Rows[0]["ObsGrade"].ToString() : "";
            //meInfo.ObsGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGradeDate"])) ? dtMemberManager.Rows[0]["ObsGradeDate"].ToString() : "";
            meInfo.DesGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGrade"])) ? dtMemberManager.Rows[0]["DesGrade"].ToString() : "";
            //meInfo.DesGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGradeDate"])) ? dtMemberManager.Rows[0]["DesGradeDate"].ToString() : "";
            meInfo.UrbanismGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGrade"])) ? dtMemberManager.Rows[0]["UrbanismGrade"].ToString() : "";
            //meInfo.UrbanismGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGradeDate"])) ? dtMemberManager.Rows[0]["UrbanismGradeDate"].ToString() : "";
            meInfo.MappingGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGrade"])) ? dtMemberManager.Rows[0]["MappingGrade"].ToString() : "";
            //meInfo.MappingGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGradeDate"])) ? dtMemberManager.Rows[0]["MappingGradeDate"].ToString() : "";
            meInfo.TrafficGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGrade"])) ? dtMemberManager.Rows[0]["TrafficGrade"].ToString() : "";
            // meInfo.TrafficGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGradeDate"])) ? dtMemberManager.Rows[0]["TrafficGradeDate"].ToString() : "";
            // meInfo.LicenceNameMemberMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceNameMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceNameMemberMaster"].ToString() : "";
            // meInfo.LicenceIdMemberMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceIdMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceIdMemberMaster"].ToString() : "";
            meInfo.MajorNameMemberMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameMemberMaster"])) ? dtMemberManager.Rows[0]["MajorNameMemberMaster"].ToString() : "";
            // meInfo.UniversityNameMemberMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UniversityNameMemberMaster"])) ? dtMemberManager.Rows[0]["UniversityNameMemberMaster"].ToString() : "";
            // meInfo.GraduateDateMemberMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["GraduateDateMemberMaster"])) ? dtMemberManager.Rows[0]["GraduateDateMemberMaster"].ToString() : "";
            meInfo.MjParentName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentName"])) ? dtMemberManager.Rows[0]["MjParentName"].ToString() : "";
            meInfo.MjParentId = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentId"])) ? dtMemberManager.Rows[0]["MjParentId"].ToString() : "";
            meInfo.MajorNameDocumentMaster = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameDocumentMaster"])) ? dtMemberManager.Rows[0]["MajorNameDocumentMaster"].ToString() : "";
            meInfo.DocumntFirstRegister = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntFirstRegister"])) ? dtMemberManager.Rows[0]["DocumntFirstRegister"].ToString() : "";
            meInfo.DocumntExpireDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntExpireDate"])) ? dtMemberManager.Rows[0]["DocumntExpireDate"].ToString() : "";
            meInfo.DocumentRevivalDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumentRevivalDate"])) ? dtMemberManager.Rows[0]["DocumentRevivalDate"].ToString() : "";
            meInfo.DocMajorParentName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocMajorParentName"])) ? dtMemberManager.Rows[0]["DocMajorParentName"].ToString() : "";
            meInfo.ImageUrl = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImageUrl"])) ? dtMemberManager.Rows[0]["ImageUrl"].ToString() : "";
            meInfo.MobileNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MobileNo"])) ? dtMemberManager.Rows[0]["MobileNo"].ToString() : "";
            meInfo.WorkFlowName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["WorkFlowName"])) ? dtMemberManager.Rows[0]["WorkFlowName"].ToString() : "";
            meInfo.LicenceCount = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceCount"])) ? dtMemberManager.Rows[0]["LicenceCount"].ToString() : "";
            meInfo.HasImplementCertificate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["HasImplementCertificate"])) ? dtMemberManager.Rows[0]["HasImplementCertificate"].ToString() : "";
            DataTable dtMemberMajorCount = MemberManager.SelectMemberDocMajorCountForEsys(MeId);
            meInfo.LicenceCount = dtMemberMajorCount.Rows.Count.ToString();
            #endregion
        }
        return new JavaScriptSerializer().Serialize(meInfo);

    }
    #endregion

    #region GeEngtOfficeInfo
    private class MeEngOffInfo
    {
        public string Status, Message, EngOfficeName, EngOfficeExpireDate, EngOfficeStatus, EngOfficeWorkFlowState, MemberStatus, MemberGradeInEngOffice
        , LastRequestTypeName, LastRequestType, lastRequestconfirmstatus;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <param name="MeCode"></param>
    /// <returns>LastRequestType= when 0 then 'صدور' when 1 then 'تمدید' when 2 then 'تغییرات' when 3 then 'المثنی' when 4 then 'ابطال'  when 5 then 'صدور سیستم قدیم' when 6 then 'تغییرات اطلاعات پایه'  when 7 then 'احیاء مجدد' </returns>
    [WebMethod]
    public string GeMemberEngOfficeInfo(string UserName, string PassWord, string MeCode)
    {
        int meId = -2;
        MeEngOffInfo meEngOffInfo = new MeEngOffInfo();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            meEngOffInfo.Status = "-3";
            meEngOffInfo.Message = "UserName or PassWord is not valid!";
            return new JavaScriptSerializer().Serialize(meEngOffInfo);
        }
        int.TryParse(MeCode, out meId);
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        //TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtEngOffice = OfMeManager.FindEngOfficeMemberForWebServiceEsys(meId);

        if (dtEngOffice.Rows.Count == 1)
        {
            int MaxEOfId = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["MaxEOfId"])) ? Convert.ToInt32(dtEngOffice.Rows[0]["MaxEOfId"]) : -1;
            if (MaxEOfId != -1)
            {
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
                meEngOffInfo.EngOfficeWorkFlowState = WorkFlowStateManager.SelectEngOfficeWorkflowstateForWebServiceEsys(MaxEOfId);
            }
            meEngOffInfo.Status = "1";
            meEngOffInfo.Message = "OK";
            meEngOffInfo.EngOfficeName = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["EngOfficeName"])) ? dtEngOffice.Rows[0]["EngOfficeName"].ToString() : "";
            meEngOffInfo.EngOfficeExpireDate = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["EngOfficeExpireDate"])) ? dtEngOffice.Rows[0]["EngOfficeExpireDate"].ToString() : "";
            meEngOffInfo.EngOfficeStatus = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["EngOfficeStatus"])) ? dtEngOffice.Rows[0]["EngOfficeStatus"].ToString() : "";
            meEngOffInfo.MemberStatus = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["MemberStatus"])) ? dtEngOffice.Rows[0]["MemberStatus"].ToString() : "";
            meEngOffInfo.MemberGradeInEngOffice = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["MemberGradeInEngOffice"])) ? dtEngOffice.Rows[0]["MemberGradeInEngOffice"].ToString() : "";

            meEngOffInfo.LastRequestTypeName = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["LastRequestTypeName"])) ? dtEngOffice.Rows[0]["LastRequestTypeName"].ToString() : "";
            meEngOffInfo.LastRequestType = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["LastRequestType"])) ? dtEngOffice.Rows[0]["LastRequestType"].ToString() : "";
            meEngOffInfo.lastRequestconfirmstatus = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["lastRequestconfirmstatus"])) ? dtEngOffice.Rows[0]["lastRequestconfirmstatus"].ToString() : "";
        }
        else
        {
            meEngOffInfo.Status = "0";
            meEngOffInfo.Message = "No Data";
        }
        return new JavaScriptSerializer().Serialize(meEngOffInfo);
    }
    #endregion

    #region GetOfficeInfo
    private class MemberOfficeInfo
    {
        public string Status, Message, OfficeName, OfficeExpireDate, OfficeStatus, OfficeWorkFlowState, MemberStatus, MemberGradeInOffice
        , LastRequestTypeName, LastRequestType, lastRequestconfirmstatus, OfficeType
            ;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <param name="MeCode"></param>
    /// <returns> LastRequestType= 0:صدور
    /// 1:تمدید
    /// 2:تغییرات
    /// 3:المثنی
    /// 4:ابطال
    /// 5:صدور سیستم قدیم
    /// 6:تغییرات اطلاعات پایه
    /// 7:احیاء مجدد
    /// OfficeStatus=وضعیت پروانه شرکت
    /// </returns>
    [WebMethod]
    public string GetMemberOfficeInfo(string UserName, string PassWord, string MeCode)
    {
        int meId = -2;
        MemberOfficeInfo memberOfficeInfo = new MemberOfficeInfo();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            memberOfficeInfo.Status = "-3";
            memberOfficeInfo.Message = "UserName or PassWord is not valid!";
            return new JavaScriptSerializer().Serialize(memberOfficeInfo);
        }
        int.TryParse(MeCode, out meId);
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        //TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtOfficeMember = OfMeManager.FindOfficeMemberForWebServiceEsys(meId);

        if (dtOfficeMember.Rows.Count == 1)
        {
            int MaxOfReId = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["MaxOfReId"])) ? Convert.ToInt32(dtOfficeMember.Rows[0]["MaxOfReId"]) : -1;
            if (MaxOfReId != -1)
            {
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
                memberOfficeInfo.OfficeWorkFlowState = WorkFlowStateManager.SelectOfficeWorkflowstateForWebServiceEsys(MaxOfReId);
            }
            memberOfficeInfo.Status = "1";
            memberOfficeInfo.Message = "OK";
            memberOfficeInfo.OfficeName = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["OfficeName"])) ? dtOfficeMember.Rows[0]["OfficeName"].ToString() : "";
            memberOfficeInfo.OfficeExpireDate = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["OfficeExpireDate"])) ? dtOfficeMember.Rows[0]["OfficeExpireDate"].ToString() : "";
            memberOfficeInfo.OfficeStatus = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["OfficeStatus"])) ? dtOfficeMember.Rows[0]["OfficeStatus"].ToString() : "";

            memberOfficeInfo.MemberStatus = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["MemberStatus"])) ? dtOfficeMember.Rows[0]["MemberStatus"].ToString() : "";
            memberOfficeInfo.MemberGradeInOffice = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["MemberGradeInOffice"])) ? dtOfficeMember.Rows[0]["MemberGradeInOffice"].ToString() : "";

            memberOfficeInfo.LastRequestTypeName = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["LastRequestTypeName"])) ? dtOfficeMember.Rows[0]["LastRequestTypeName"].ToString() : "";
            memberOfficeInfo.LastRequestType = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["LastRequestType"])) ? dtOfficeMember.Rows[0]["LastRequestType"].ToString() : "";
            memberOfficeInfo.lastRequestconfirmstatus = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["lastRequestconfirmstatus"])) ? dtOfficeMember.Rows[0]["lastRequestconfirmstatus"].ToString() : "";
            memberOfficeInfo.OfficeType = (!Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[0]["TypeName"])) ? dtOfficeMember.Rows[0]["TypeName"].ToString() : "";
        }
        else
        {
            memberOfficeInfo.Status = "0";
            memberOfficeInfo.Message = "No Data";
        }
        return new JavaScriptSerializer().Serialize(memberOfficeInfo);
    }
    #endregion

    /// <summary>
    /// Status:return 0 if there is an Error
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetMemberGrade(Int32 MeId, string UserName, string PassWord)
    {


        DataTable dt = new DataTable();

        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Message", typeof(string));
        // dt.Columns.Add("MeId", typeof(string));

        dt.Columns.Add("GradeObservation", typeof(string));
        dt.Columns.Add("GradeDesign", typeof(string));
        dt.Columns.Add("GradeImplement", typeof(string));
        dt.Columns.Add("GradeUrbanism", typeof(string));
        dt.Columns.Add("GradeTraffic", typeof(string));
        dt.Columns.Add("GradeMapping", typeof(string));
        dt.Columns.Add("GradeGas", typeof(string));
        dt.Columns.Add("MjName", typeof(string));
        dt.Columns.Add("MjParentName", typeof(string));


        dt.TableName = "MemberGrade";


        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow drError = dt.NewRow();
            drError["Status"] = "0";
            drError["Message"] = "Incorrect UserName Or Password";
            dt.Rows.Add(drError);
            return dt;
        }

        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtMemberManager = DocMemberFileDetailManager.SelectDocMemberFileDetailForEsys(MeId);

        if (dtMemberManager.Rows.Count == 0)
        {
            DataRow drError = dt.NewRow();
            drError["Status"] = "0";
            drError["Message"] = "There is no data";
            dt.Rows.Add(drError);
            return dt;
        }
        DataRow[] dtArchitecture = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Architecture));
        if (dtArchitecture.Length != 0)
        {
            DataRow drArchitecture = dt.NewRow();
            drArchitecture["Status"] = "1";
            drArchitecture["Message"] = "OK";
            drArchitecture["GradeObservation"] = "0";
            drArchitecture["GradeDesign"] = "0";
            drArchitecture["GradeImplement"] = "0";
            drArchitecture["GradeUrbanism"] = "0";
            drArchitecture["GradeTraffic"] = "0";
            drArchitecture["GradeMapping"] = "0";
            drArchitecture["GradeGas"] = "0";
            for (int i = 0; i < dtArchitecture.Length; i++)
            {
                #region AddRow

                drArchitecture["MjName"] = (!Utility.IsDBNullOrNullValue(dtArchitecture[i]["MjName"])) ? dtArchitecture[i]["MjName"].ToString() : "";
                drArchitecture["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtArchitecture[i]["MjParentName"])) ? dtArchitecture[i]["MjParentName"].ToString() : "";
                int GrdCode = (!Utility.IsDBNullOrNullValue(dtArchitecture[i]["GrdCode"])) ? Convert.ToInt32(dtArchitecture[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtArchitecture[i]["ResId"])) ? Convert.ToInt32(dtArchitecture[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drArchitecture["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drArchitecture["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drArchitecture["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drArchitecture["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drArchitecture["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drArchitecture["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drArchitecture["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drArchitecture);
            dt.AcceptChanges();
        }
        DataRow[] dtCivil = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Civil));
        if (dtCivil.Length != 0)
        {
            DataRow drCivil = dt.NewRow();
            drCivil["Status"] = "1";
            drCivil["Message"] = "OK";
            drCivil["GradeObservation"] = "0";
            drCivil["GradeDesign"] = "0";
            drCivil["GradeImplement"] = "0";
            drCivil["GradeUrbanism"] = "0";
            drCivil["GradeTraffic"] = "0";
            drCivil["GradeMapping"] = "0";
            drCivil["GradeGas"] = "0";
            for (int i = 0; i < dtCivil.Length; i++)
            {
                #region AddRow

                drCivil["MjName"] = (!Utility.IsDBNullOrNullValue(dtCivil[i]["MjName"])) ? dtCivil[i]["MjName"].ToString() : "";
                drCivil["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtCivil[i]["MjParentName"])) ? dtCivil[i]["MjParentName"].ToString() : "";
                int GrdCode = (!Utility.IsDBNullOrNullValue(dtCivil[i]["GrdCode"])) ? Convert.ToInt32(dtCivil[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtCivil[i]["ResId"])) ? Convert.ToInt32(dtCivil[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drCivil["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drCivil["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drCivil["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drCivil["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drCivil["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drCivil["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drCivil["GradeGas"] = GradeCode.ToString();
                        break;
                }

                #endregion
            }
            dt.Rows.Add(drCivil);
            dt.AcceptChanges();
        }
        DataRow[] dtElectronic = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Electronic));
        if (dtElectronic.Length != 0)
        {
            DataRow drElectronic = dt.NewRow();
            drElectronic["Status"] = "1";
            drElectronic["Message"] = "OK";
            drElectronic["GradeObservation"] = "0";
            drElectronic["GradeDesign"] = "0";
            drElectronic["GradeImplement"] = "0";
            drElectronic["GradeUrbanism"] = "0";
            drElectronic["GradeTraffic"] = "0";
            drElectronic["GradeMapping"] = "0";
            drElectronic["GradeGas"] = "0";
            for (int i = 0; i < dtElectronic.Length; i++)
            {
                #region AddRow

                drElectronic["MjName"] = (!Utility.IsDBNullOrNullValue(dtElectronic[i]["MjName"])) ? dtElectronic[i]["MjName"].ToString() : "";
                drElectronic["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtElectronic[i]["MjParentName"])) ? dtElectronic[i]["MjParentName"].ToString() : "";

                int GrdCode = (!Utility.IsDBNullOrNullValue(dtElectronic[i]["GrdCode"])) ? Convert.ToInt32(dtElectronic[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtElectronic[i]["ResId"])) ? Convert.ToInt32(dtElectronic[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drElectronic["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drElectronic["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drElectronic["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drElectronic["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drElectronic["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drElectronic["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drElectronic["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drElectronic);
            dt.AcceptChanges();
        }
        DataRow[] dtMapping = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Mapping));
        if (dtMapping.Length != 0)
        {
            DataRow drMapping = dt.NewRow();
            drMapping["Status"] = "1";
            drMapping["Message"] = "OK";
            drMapping["GradeObservation"] = "0";
            drMapping["GradeDesign"] = "0";
            drMapping["GradeImplement"] = "0";
            drMapping["GradeUrbanism"] = "0";
            drMapping["GradeTraffic"] = "0";
            drMapping["GradeMapping"] = "0";
            drMapping["GradeGas"] = "0";
            for (int i = 0; i < dtMapping.Length; i++)
            {
                #region AddRow
                drMapping["MjName"] = (!Utility.IsDBNullOrNullValue(dtMapping[i]["MjName"])) ? dtMapping[i]["MjName"].ToString() : "";
                drMapping["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtMapping[i]["MjParentName"])) ? dtMapping[i]["MjParentName"].ToString() : "";

                int GrdCode = (!Utility.IsDBNullOrNullValue(dtMapping[i]["GrdCode"])) ? Convert.ToInt32(dtMapping[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtMapping[i]["ResId"])) ? Convert.ToInt32(dtMapping[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drMapping["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drMapping["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drMapping["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drMapping["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drMapping["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drMapping["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drMapping["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drMapping);
            dt.AcceptChanges();
        }
        DataRow[] dtMechanic = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Mechanic));
        if (dtMechanic.Length != 0)
        {
            DataRow drMechanic = dt.NewRow();
            drMechanic["Status"] = "1";
            drMechanic["Message"] = "OK";
            drMechanic["GradeObservation"] = "0";
            drMechanic["GradeDesign"] = "0";
            drMechanic["GradeImplement"] = "0";
            drMechanic["GradeUrbanism"] = "0";
            drMechanic["GradeTraffic"] = "0";
            drMechanic["GradeMapping"] = "0";
            drMechanic["GradeGas"] = "0";
            for (int i = 0; i < dtMechanic.Length; i++)
            {
                #region AddRow

                drMechanic["MjName"] = (!Utility.IsDBNullOrNullValue(dtMechanic[i]["MjName"])) ? dtMechanic[i]["MjName"].ToString() : "";
                drMechanic["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtMechanic[i]["MjParentName"])) ? dtMechanic[i]["MjParentName"].ToString() : "";
                int GrdCode = (!Utility.IsDBNullOrNullValue(dtMechanic[i]["GrdCode"])) ? Convert.ToInt32(dtMechanic[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtMechanic[i]["ResId"])) ? Convert.ToInt32(dtMechanic[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drMechanic["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drMechanic["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drMechanic["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drMechanic["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drMechanic["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drMechanic["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drMechanic["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drMechanic);
            dt.AcceptChanges();
        }
        DataRow[] dtTraffic = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Traffic));
        if (dtTraffic.Length != 0)
        {
            DataRow drTraffic = dt.NewRow();
            drTraffic["Status"] = "1";
            drTraffic["Message"] = "OK";
            drTraffic["GradeObservation"] = "0";
            drTraffic["GradeDesign"] = "0";
            drTraffic["GradeImplement"] = "0";
            drTraffic["GradeUrbanism"] = "0";
            drTraffic["GradeTraffic"] = "0";
            drTraffic["GradeMapping"] = "0";
            drTraffic["GradeGas"] = "0";

            for (int i = 0; i < dtTraffic.Length; i++)
            {
                #region AddRow

                drTraffic["MjName"] = (!Utility.IsDBNullOrNullValue(dtTraffic[i]["MjName"])) ? dtTraffic[i]["MjName"].ToString() : "";
                drTraffic["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtTraffic[i]["MjParentName"])) ? dtTraffic[i]["MjParentName"].ToString() : "";
                int GrdCode = (!Utility.IsDBNullOrNullValue(dtTraffic[i]["GrdCode"])) ? Convert.ToInt32(dtTraffic[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtTraffic[i]["ResId"])) ? Convert.ToInt32(dtTraffic[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drTraffic["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drTraffic["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drTraffic["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drTraffic["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drTraffic["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drTraffic["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drTraffic["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drTraffic);
            dt.AcceptChanges();
        }
        DataRow[] dtUrbanism = dtMemberManager.Select("MjParentId=" + ((int)TSP.DataManager.MainMajors.Urbanism));
        if (dtUrbanism.Length != 0)
        {
            DataRow drUrbanism = dt.NewRow();
            drUrbanism["Status"] = "1";
            drUrbanism["Message"] = "OK";
            drUrbanism["GradeObservation"] = "0";
            drUrbanism["GradeDesign"] = "0";
            drUrbanism["GradeImplement"] = "0";
            drUrbanism["GradeUrbanism"] = "0";
            drUrbanism["GradeTraffic"] = "0";
            drUrbanism["GradeMapping"] = "0";
            drUrbanism["GradeGas"] = "0";

            for (int i = 0; i < dtUrbanism.Length; i++)
            {
                #region AddRow

                drUrbanism["MjName"] = (!Utility.IsDBNullOrNullValue(dtUrbanism[i]["MjName"])) ? dtUrbanism[i]["MjName"].ToString() : "";
                drUrbanism["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtUrbanism[i]["MjParentName"])) ? dtUrbanism[i]["MjParentName"].ToString() : "";
                int GrdCode = (!Utility.IsDBNullOrNullValue(dtUrbanism[i]["GrdCode"])) ? Convert.ToInt32(dtUrbanism[i]["GrdCode"]) : 0;
                int ResId = (!Utility.IsDBNullOrNullValue(dtUrbanism[i]["ResId"])) ? Convert.ToInt32(dtUrbanism[i]["ResId"]) : 0;
                int GradeCode = 0;
                switch (GrdCode)
                {
                    case 0: GradeCode = 0;
                        break;
                    case 1: GradeCode = 4;
                        break;
                    case 2: GradeCode = 1;
                        break;
                    case 3: GradeCode = 2;
                        break;
                    case 4: GradeCode = 3;
                        break;
                }
                switch (ResId)
                {
                    case 1:
                        drUrbanism["GradeObservation"] = GradeCode.ToString();
                        break;
                    case 2:
                        drUrbanism["GradeDesign"] = GradeCode.ToString();
                        break;
                    case 3:
                        drUrbanism["GradeImplement"] = GradeCode.ToString();
                        break;
                    case 6:
                        drUrbanism["GradeMapping"] = GradeCode.ToString();
                        break;
                    case 7:
                        drUrbanism["GradeTraffic"] = GradeCode.ToString();
                        break;
                    case 8:
                        drUrbanism["GradeUrbanism"] = GradeCode.ToString();
                        break;
                    case 9:
                        drUrbanism["GradeGas"] = GradeCode.ToString();
                        break;
                }
                #endregion
            }
            dt.Rows.Add(drUrbanism);
            dt.AcceptChanges();
        }
        return dt;
    }

    #region GeImpementInfo
    private class MeImpDoc
    {
        public string Status, Message, MFNo, LastRequestTypeName, LastRequestConfirmStatus, LastRequestConfirmStatusName, WorkFlowName, LastRequestExpireDate, LastConfirmedRequestExpireDate;
    }
    [WebMethod]
    public string GeMemberImplementDocumentInfo(string UserName, string PassWord, string MeCode)
    {
        int meId = -2;
        MeImpDoc MeImpDoc = new MeImpDoc();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            MeImpDoc.Status = "-3";
            MeImpDoc.Message = "UserName or PassWord is not valid!";
            return new JavaScriptSerializer().Serialize(MeImpDoc);
        }
        int.TryParse(MeCode, out meId);
        TSP.DataManager.DocMemberFileManager DocMeFileManager = new TSP.DataManager.DocMemberFileManager();
        //TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtImpDoc = DocMeFileManager.SelectImpDocForWebServiceEsys(meId);

        if (dtImpDoc.Rows.Count == 1)
        {
            //int MaxEOfId = (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["MaxEOfId"])) ? Convert.ToInt32(dtEngOffice.Rows[0]["MaxEOfId"]) : -1;
            //if (MaxEOfId != -1)
            //{
            //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            //    MeImpDoc.EngOfficeWorkFlowState = WorkFlowStateManager.SelectEngOfficeWorkflowstateForWebServiceEsys(MaxEOfId);
            //}
            MeImpDoc.Status = "1";
            MeImpDoc.Message = "OK";
            MeImpDoc.MFNo = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["MFNo"])) ? dtImpDoc.Rows[0]["MFNo"].ToString() : "";
            MeImpDoc.LastRequestTypeName = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["LastReqTypeName"])) ? dtImpDoc.Rows[0]["LastReqTypeName"].ToString() : "";
            MeImpDoc.LastRequestConfirmStatus = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["LastRequestConfirmStatus"])) ? dtImpDoc.Rows[0]["LastRequestConfirmStatus"].ToString() : "";
            MeImpDoc.LastRequestConfirmStatusName = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["LastRequestConfirmStatusName"])) ? dtImpDoc.Rows[0]["LastRequestConfirmStatusName"].ToString() : "";
            MeImpDoc.WorkFlowName = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["TaskName"])) ? dtImpDoc.Rows[0]["TaskName"].ToString() : "";
            MeImpDoc.LastRequestExpireDate = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["LastExpireDate"])) ? dtImpDoc.Rows[0]["LastExpireDate"].ToString() : "";
            MeImpDoc.LastConfirmedRequestExpireDate = (!Utility.IsDBNullOrNullValue(dtImpDoc.Rows[0]["LastConfirmedExpireDate"])) ? dtImpDoc.Rows[0]["LastConfirmedExpireDate"].ToString() : "";
        }
        else
        {
            MeImpDoc.Status = "0";
            MeImpDoc.Message = "No Data";
        }
        return new JavaScriptSerializer().Serialize(MeImpDoc);
    }
    #endregion

}