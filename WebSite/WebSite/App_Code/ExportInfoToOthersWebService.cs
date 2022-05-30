using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Security;

/// <summary>
/// Summary description for ExportInfoToOthersWebService
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
    
public class ExportInfoToOthersWebService : System.Web.Services.WebService
{
    
    public ExportInfoToOthersWebService()
    {


        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private string _UserName
    {
        get
        {
            return "SNMFInfo";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFInfo";
        }
    }

    private string _UserNameObs
    {
        get
        {
            return "SNMFInfo971107";
        }
    }

    private string _PassWordObs
    {
        get
        {
            return "SNMFInfo971107";
        }
    }

    [WebMethod]
    public DataTable GetMemberInfo(Int32 MeId, string UserName, string PassWord)
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("MeNo", typeof(string));
        dt.Columns.Add("MembershipDate", typeof(string));
        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName", typeof(string));
        dt.Columns.Add("TitleName", typeof(string));
        dt.Columns.Add("FatherName", typeof(string));
        dt.Columns.Add("CityName", typeof(string));
        dt.Columns.Add("MeliCardNo", typeof(string));
        dt.Columns.Add("ShenasnamehNo", typeof(string));
        dt.Columns.Add("BirhtDate", typeof(string));
        dt.Columns.Add("MaritalStatusName", typeof(string));
        dt.Columns.Add("MemberStatus", typeof(string));
        dt.Columns.Add("soldirStatus", typeof(string));
        dt.Columns.Add("BirthPlace", typeof(string));
        dt.Columns.Add("AgentName", typeof(string));

        dt.Columns.Add("SexName", typeof(string));
        dt.Columns.Add("BankAccNo", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        dt.Columns.Add("FileNo", typeof(string));
        dt.Columns.Add("ImpGrade", typeof(string));

        dt.Columns.Add("ObsGrade", typeof(string));
        dt.Columns.Add("DesGrade", typeof(string));
        dt.Columns.Add("UrbanismGrade", typeof(string));
        dt.Columns.Add("MappingGrade", typeof(string));
        dt.Columns.Add("TrafficGrade", typeof(string));
        dt.Columns.Add("LicenceNameMemberMaster", typeof(string));

        dt.Columns.Add("MajorNameMemberMaster", typeof(string));
        dt.Columns.Add("UniversityNameMemberMaster", typeof(string));
        dt.Columns.Add("GraduateDateMemberMaster", typeof(string));
        dt.Columns.Add("MjParentName", typeof(string));
        dt.Columns.Add("MajorNameDocumentMaster", typeof(string));
        dt.Columns.Add("DocumntFirstRegister", typeof(string));
        dt.Columns.Add("DocumntExpireDate", typeof(string));
        dt.Columns.Add("DocumentRevivalDate", typeof(string));

        dt.Columns.Add("IDImageP1", typeof(string));
        dt.Columns.Add("IDImageP2", typeof(string));
        dt.Columns.Add("IDImagePDes", typeof(string));
        dt.Columns.Add("SSNImageF", typeof(string));
        dt.Columns.Add("SSNImageB", typeof(string));
        dt.Columns.Add("SoldirImageF", typeof(string));
        dt.Columns.Add("SoldirImageB", typeof(string));
        dt.Columns.Add("MemberMasterLicenceImage", typeof(string));
        dt.Columns.Add("ImageUrl", typeof(string));
        dt.Columns.Add("UniversityIdMemberMaster", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));

        dt.Columns.Add("WorkFlowName", typeof(string));
        dt.Columns.Add("ImpGradeDate", typeof(string));
        dt.Columns.Add("ObsGradeDate", typeof(string));
        dt.Columns.Add("DesGradeDate", typeof(string));
        dt.Columns.Add("UrbanismGradeDate", typeof(string));
        dt.Columns.Add("MappingGradeDate", typeof(string));
        dt.Columns.Add("TrafficGradeDate", typeof(string));
        dt.Columns.Add("AgentId", typeof(string));
        dt.Columns.Add("LicenceIdMemberMaster", typeof(string));
        dt.Columns.Add("CalculationGrade", typeof(string));
        dt.Columns.Add("MjParentId", typeof(string));
        dt.Columns.Add("DocMajorParentName", typeof(string));
        
        dt.TableName = "MemberInfo";

        DataRow dr = dt.NewRow();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            dr["Status"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMemberManager = MemberManager.SelectMemberInfoForTSWebService(MeId);
        int Count = dtMemberManager.Rows.Count;

        if (Count != 1)
        {

            dr["Status"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }


        dr["Status"] = "There is Valid";
        dr["MeNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeNo"])) ? dtMemberManager.Rows[0]["MeNo"].ToString() : "";
        dr["MembershipDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MembershipDate"])) ? dtMemberManager.Rows[0]["MembershipDate"].ToString() : "";
        dr["FirstName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FirstName"])) ? dtMemberManager.Rows[0]["FirstName"].ToString() : "";
        dr["LastName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LastName"])) ? dtMemberManager.Rows[0]["LastName"].ToString() : "";
        dr["TitleName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TitleName"])) ? dtMemberManager.Rows[0]["TitleName"].ToString() : "";
        dr["FatherName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FatherName"])) ? dtMemberManager.Rows[0]["FatherName"].ToString() : "";
        dr["CityName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["CityName"])) ? dtMemberManager.Rows[0]["CityName"].ToString() : "";
        dr["MeliCardNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeliCardNo"])) ? dtMemberManager.Rows[0]["MeliCardNo"].ToString() : "";
        dr["ShenasnamehNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ShenasnamehNo"])) ? dtMemberManager.Rows[0]["ShenasnamehNo"].ToString() : "";
        dr["BirhtDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BirhtDate"])) ? dtMemberManager.Rows[0]["BirhtDate"].ToString() : "";
        dr["MaritalStatusName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MaritalStatusName"])) ? dtMemberManager.Rows[0]["MaritalStatusName"].ToString() : "";
        dr["MemberStatus"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MemberStatus"])) ? dtMemberManager.Rows[0]["MemberStatus"].ToString() : "";
        dr["soldirStatus"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["soldirStatus"])) ? dtMemberManager.Rows[0]["soldirStatus"].ToString() : "";
        dr["BirthPlace"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BirthPlace"])) ? dtMemberManager.Rows[0]["BirthPlace"].ToString() : "";
        dr["AgentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentName"])) ? dtMemberManager.Rows[0]["AgentName"].ToString() : "";
        dr["AgentId"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentId"])) ? dtMemberManager.Rows[0]["AgentId"].ToString() : "";
        dr["SexName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SexName"])) ? dtMemberManager.Rows[0]["SexName"].ToString() : "";
        dr["BankAccNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BankAccNo"])) ? dtMemberManager.Rows[0]["BankAccNo"].ToString() : "";
        dr["Email"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["Email"])) ? dtMemberManager.Rows[0]["Email"].ToString() : "";
        dr["FileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FileNo"])) ? dtMemberManager.Rows[0]["FileNo"].ToString() : "";
        dr["ImpGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGrade"])) ? dtMemberManager.Rows[0]["ImpGrade"].ToString() : "";
        dr["ImpGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGradeDate"])) ? dtMemberManager.Rows[0]["ImpGradeDate"].ToString() : "";
        dr["ObsGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGrade"])) ? dtMemberManager.Rows[0]["ObsGrade"].ToString() : "";
        dr["ObsGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGradeDate"])) ? dtMemberManager.Rows[0]["ObsGradeDate"].ToString() : "";
        dr["DesGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGrade"])) ? dtMemberManager.Rows[0]["DesGrade"].ToString() : "";
        dr["DesGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGradeDate"])) ? dtMemberManager.Rows[0]["DesGradeDate"].ToString() : "";
        dr["UrbanismGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGrade"])) ? dtMemberManager.Rows[0]["UrbanismGrade"].ToString() : "";
        dr["UrbanismGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGradeDate"])) ? dtMemberManager.Rows[0]["UrbanismGradeDate"].ToString() : "";
        dr["MappingGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGrade"])) ? dtMemberManager.Rows[0]["MappingGrade"].ToString() : "";
        dr["MappingGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGradeDate"])) ? dtMemberManager.Rows[0]["MappingGradeDate"].ToString() : "";
        dr["TrafficGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGrade"])) ? dtMemberManager.Rows[0]["TrafficGrade"].ToString() : "";
        dr["TrafficGradeDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGradeDate"])) ? dtMemberManager.Rows[0]["TrafficGradeDate"].ToString() : "";
        dr["LicenceNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceNameMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceNameMemberMaster"].ToString() : "";
        dr["LicenceIdMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceIdMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceIdMemberMaster"].ToString() : "";
        dr["MajorNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameMemberMaster"])) ? dtMemberManager.Rows[0]["MajorNameMemberMaster"].ToString() : "";
        dr["UniversityNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UniversityNameMemberMaster"])) ? dtMemberManager.Rows[0]["UniversityNameMemberMaster"].ToString() : "";
        dr["GraduateDateMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["GraduateDateMemberMaster"])) ? dtMemberManager.Rows[0]["GraduateDateMemberMaster"].ToString() : "";
        dr["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentName"])) ? dtMemberManager.Rows[0]["MjParentName"].ToString() : "";
        dr["MajorNameDocumentMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameDocumentMaster"])) ? dtMemberManager.Rows[0]["MajorNameDocumentMaster"].ToString() : "";
        dr["DocumntFirstRegister"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntFirstRegister"])) ? dtMemberManager.Rows[0]["DocumntFirstRegister"].ToString() : "";
        dr["DocumntExpireDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntExpireDate"])) ? dtMemberManager.Rows[0]["DocumntExpireDate"].ToString() : "";
        dr["DocumentRevivalDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumentRevivalDate"])) ? dtMemberManager.Rows[0]["DocumentRevivalDate"].ToString() : "";

        dr["IDImageP1"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["IDImageP1"])) ? dtMemberManager.Rows[0]["IDImageP1"].ToString() : "";
        dr["IDImageP2"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["IDImageP2"])) ? dtMemberManager.Rows[0]["IDImageP2"].ToString() : "";
        dr["IDImagePDes"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["IDImagePDes"])) ? dtMemberManager.Rows[0]["IDImagePDes"].ToString() : "";
        dr["SSNImageF"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SSNImageF"])) ? dtMemberManager.Rows[0]["SSNImageF"].ToString() : "";
        dr["SSNImageB"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SSNImageB"])) ? dtMemberManager.Rows[0]["SSNImageB"].ToString() : "";
        dr["SoldirImageF"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SoldirImageF"])) ? dtMemberManager.Rows[0]["SoldirImageF"].ToString() : "";
        dr["SoldirImageB"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SoldirImageB"])) ? dtMemberManager.Rows[0]["SoldirImageB"].ToString() : "";
        dr["MemberMasterLicenceImage"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MemberMasterLicenceImage"])) ? dtMemberManager.Rows[0]["MemberMasterLicenceImage"].ToString() : "";
        dr["ImageUrl"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImageUrl"])) ? dtMemberManager.Rows[0]["ImageUrl"].ToString() : "";
        dr["UniversityIdMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UniversityIdMemberMaster"])) ? dtMemberManager.Rows[0]["UniversityIdMemberMaster"].ToString() : "";
        dr["MobileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MobileNo"])) ? dtMemberManager.Rows[0]["MobileNo"].ToString() : "";
        dr["WorkFlowName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["WorkFlowName"])) ? dtMemberManager.Rows[0]["WorkFlowName"].ToString() : "";
        dr["MjParentId"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentId"])) ? dtMemberManager.Rows[0]["MjParentId"].ToString() : "";
        dr["DocMajorParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocMajorParentName"])) ? dtMemberManager.Rows[0]["DocMajorParentName"].ToString() : "";         
        
        if (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentId"]) && !Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGrade"]))
        {
            if (Convert.ToInt32(dtMemberManager.Rows[0]["MjParentId"]) == 3)
                dr["CalculationGrade"] = dtMemberManager.Rows[0]["DesGrade"];
        }
        dt.Rows.Add(dr);
        return dt;
    }

    /// <summary>
    /// اگر مقدار کلیه پارامتراها برابر -1 باشد مقدار نامعتبر را بر می گرداند
    /// </summary>
    /// <param name="MeId">کد عضویت</param>
    /// <param name="HasFileNo">0:فاقد پروانه
    /// 1:دارای پروانه</param>
    /// <param name="MajorParentCode">100-200-300-400-500-600-700</param>
    /// <param name="AgentCode"></param>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    ///// <returns></returns>
    //[WebMethod]    
    //public DataTable GetMemberInfo(Int32 MeId, int HasFileNo, int MajorParentCode, int AgentCode, string UserName, string PassWord)
    //{


    //    #region Define DataTable
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Status", typeof(string));
    //    dt.Columns.Add("MeNo", typeof(string));
   
    //    dt.Columns.Add("FirstName", typeof(string));
    //    dt.Columns.Add("LastName", typeof(string));
    //    dt.Columns.Add("TitleName", typeof(string));

    //    dt.Columns.Add("MeliCardNo", typeof(string));
    //    dt.Columns.Add("ShenasnamehNo", typeof(string));
    //    dt.Columns.Add("BirhtDate", typeof(string));
  
    //    dt.Columns.Add("MemberStatus", typeof(string));

    //    dt.Columns.Add("AgentName", typeof(string));

    //    dt.Columns.Add("SexName", typeof(string));
    //    dt.Columns.Add("BankAccNo", typeof(string));
    //    dt.Columns.Add("Email", typeof(string));
    //    dt.Columns.Add("FileNo", typeof(string));
    //    dt.Columns.Add("ImpGrade", typeof(string));

    //    dt.Columns.Add("ObsGrade", typeof(string));
    //    dt.Columns.Add("DesGrade", typeof(string));
    //    dt.Columns.Add("UrbanismGrade", typeof(string));
    //    dt.Columns.Add("MappingGrade", typeof(string));
    //    dt.Columns.Add("TrafficGrade", typeof(string));

    //    dt.Columns.Add("LicenceNameMemberMaster", typeof(string));

    //    dt.Columns.Add("MajorNameMemberMaster", typeof(string));

     
    //    dt.Columns.Add("MjParentName", typeof(string));
    //    dt.Columns.Add("MajorNameDocumentMaster", typeof(string));

    //    dt.Columns.Add("DocumntExpireDate", typeof(string));
    

    
    //    dt.Columns.Add("ImageUrl", typeof(string));

    //    dt.Columns.Add("MobileNo", typeof(string));

  

    //    dt.Columns.Add("AgentId", typeof(string));
    //    dt.Columns.Add("LicenceIdMemberMaster", typeof(string));
   
    //    dt.Columns.Add("MjParentId", typeof(string));
    //    dt.Columns.Add("DocMajorParentName", typeof(string));

    //    dt.TableName = "MemberInfo";
    //    #endregion
    //    DataRow dr = dt.NewRow();
    //    if (UserName != _UserNameObs || PassWord != _PassWordObs)
    //    {
    //        dr["Status"] = "Incorrect UserName Or Password";
    //        dt.Rows.Add(dr);
    //        return dt;
    //    }
    //    if(MeId==-1 && HasFileNo == -1 && MajorParentCode == -1 && AgentCode == -1)
    //    {
    //        dr["Status"] = "Parameters is not Valid";
    //        dt.Rows.Add(dr);
    //        return dt;
    //    }
    //    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
    //    DataTable dtMemberManager = MemberManager.SelectMemberAndDocInfoForWebService(MeId,HasFileNo, MajorParentCode,  AgentCode);
    //    int Count = dtMemberManager.Rows.Count;

    //    if (Count != 1)
    //    {

    //        dr["Status"] = "There is no data";
    //        dt.Rows.Add(dr);
    //        return dt;
    //    }


    //    dr["Status"] = "There is Valid";
    //    dr["MeNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeNo"])) ? dtMemberManager.Rows[0]["MeNo"].ToString() : "";
    //    dr["FirstName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FirstName"])) ? dtMemberManager.Rows[0]["FirstName"].ToString() : "";
    //    dr["LastName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LastName"])) ? dtMemberManager.Rows[0]["LastName"].ToString() : "";
    //    dr["TitleName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TitleName"])) ? dtMemberManager.Rows[0]["TitleName"].ToString() : "";
    //    dr["FatherName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FatherName"])) ? dtMemberManager.Rows[0]["FatherName"].ToString() : "";
    //    dr["MeliCardNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeliCardNo"])) ? dtMemberManager.Rows[0]["MeliCardNo"].ToString() : "";
    //    dr["ShenasnamehNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ShenasnamehNo"])) ? dtMemberManager.Rows[0]["ShenasnamehNo"].ToString() : "";
    //    dr["BirhtDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["BirhtDate"])) ? dtMemberManager.Rows[0]["BirhtDate"].ToString() : "";
    //    dr["ImageUrl"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImageUrl"])) ? dtMemberManager.Rows[0]["ImageUrl"].ToString() : "";
    //    dr["MobileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MobileNo"])) ? dtMemberManager.Rows[0]["MobileNo"].ToString() : "";
    //    dr["MemberStatus"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MemberStatus"])) ? dtMemberManager.Rows[0]["MemberStatus"].ToString() : "";

    //    dr["AgentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentName"])) ? dtMemberManager.Rows[0]["AgentName"].ToString() : "";
    //    dr["AgentId"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["AgentId"])) ? dtMemberManager.Rows[0]["AgentId"].ToString() : "";

    //    dr["SexName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SexName"])) ? dtMemberManager.Rows[0]["SexName"].ToString() : "";
    //    dr["Email"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["Email"])) ? dtMemberManager.Rows[0]["Email"].ToString() : "";
    //    dr["FileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FileNo"])) ? dtMemberManager.Rows[0]["FileNo"].ToString() : "";

    //    dr["ImpGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGrade"])) ? dtMemberManager.Rows[0]["ImpGrade"].ToString() : "";
    //    dr["ObsGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGrade"])) ? dtMemberManager.Rows[0]["ObsGrade"].ToString() : "";
    //    dr["DesGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGrade"])) ? dtMemberManager.Rows[0]["DesGrade"].ToString() : "";
    //    dr["UrbanismGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGrade"])) ? dtMemberManager.Rows[0]["UrbanismGrade"].ToString() : "";
    //    dr["MappingGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGrade"])) ? dtMemberManager.Rows[0]["MappingGrade"].ToString() : "";
    //    dr["TrafficGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGrade"])) ? dtMemberManager.Rows[0]["TrafficGrade"].ToString() : "";


    //    dr["LicenceNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceNameMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceNameMemberMaster"].ToString() : "";
    //    dr["LicenceIdMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceIdMemberMaster"])) ? dtMemberManager.Rows[0]["LicenceIdMemberMaster"].ToString() : "";
    //    dr["MajorNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameMemberMaster"])) ? dtMemberManager.Rows[0]["MajorNameMemberMaster"].ToString() : "";

    //    dr["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentName"])) ? dtMemberManager.Rows[0]["MjParentName"].ToString() : "";
    //    dr["MajorNameDocumentMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MajorNameDocumentMaster"])) ? dtMemberManager.Rows[0]["MajorNameDocumentMaster"].ToString() : "";
    //    dr["DocumntExpireDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntExpireDate"])) ? dtMemberManager.Rows[0]["DocumntExpireDate"].ToString() : "";


        
    //    dr["MjParentId"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MjParentId"])) ? dtMemberManager.Rows[0]["MjParentId"].ToString() : "";
    //    dr["DocMajorParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocMajorParentName"])) ? dtMemberManager.Rows[0]["DocMajorParentName"].ToString() : "";

     
    //    dt.Rows.Add(dr);
    //    return dt;
    //}

    [WebMethod]
    public DataTable GetMemberGrade(Int32 MeId, string UserName, string PassWord)
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("GrdName", typeof(string));
        dt.Columns.Add("ResName", typeof(string));
        dt.Columns.Add("GradeFullName", typeof(string));
        dt.Columns.Add("MjName", typeof(string));
        dt.Columns.Add("Date", typeof(string));


        dt.TableName = "MemberGrade";


        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }

        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DataTable dtMemberManager = DocMemberFileDetailManager.SelectDocMemberFileDetailForSafaRayanehWebservice(MeId);

        if (dtMemberManager.Rows.Count == 0)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }
        for (int i = 0; i < dtMemberManager.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "There is Valid";
            dr["GrdName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["GrdName"])) ? dtMemberManager.Rows[i]["GrdName"].ToString() : "";
            dr["ResName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["ResName"])) ? dtMemberManager.Rows[i]["ResName"].ToString() : "";
            dr["GradeFullName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["GradeFullName"])) ? dtMemberManager.Rows[i]["GradeFullName"].ToString() : "";
            dr["MjName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MjName"])) ? dtMemberManager.Rows[i]["MjName"].ToString() : "";
            dr["Date"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["Date"])) ? dtMemberManager.Rows[i]["Date"].ToString() : "";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        return dt;
    }

    [WebMethod]
    public DataTable GetEngOfficeInfo(Int32 EngOfId, string UserName, string PassWord)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("OfficeName", typeof(string));
        dt.Columns.Add("OfficeTypeName", typeof(string));
        dt.Columns.Add("ManagerMeId", typeof(string));
        dt.Columns.Add("ManagerMeName", typeof(string));
        dt.Columns.Add("ManagerLastName", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        dt.Columns.Add("FileNo", typeof(string));
        dt.Columns.Add("CreateDate", typeof(string));
        dt.Columns.Add("ParticipateLetterNo", typeof(string));
        dt.Columns.Add("daftarAsnadNo", typeof(string));
        dt.Columns.Add("DaftarasnadName", typeof(string));
        dt.Columns.Add("MembershipStatus", typeof(string));
        dt.Columns.Add("FirstRegDate", typeof(string));
        dt.Columns.Add("OfficeExpireDate", typeof(string));
        dt.Columns.Add("MeCount", typeof(string));

        dt.TableName = "EngOfficeInfo";

        DataRow dr = dt.NewRow();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            dr["Status"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }

        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        DataTable dtEngOfficeManager = EngOfficeManager.SelectEngOfficeInfoForTsWebservice(EngOfId);
        int Count = dtEngOfficeManager.Rows.Count;

        if (Count != 1)
        {

            dr["Status"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }


        dr["Status"] = "There is Valid";
        dr["OfficeName"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["OfficeName"])) ? dtEngOfficeManager.Rows[0]["OfficeName"].ToString() : "";
        dr["OfficeTypeName"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["OfficeTypeName"])) ? dtEngOfficeManager.Rows[0]["OfficeTypeName"].ToString() : "";
        dr["ManagerMeId"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["ManagerMeId"])) ? dtEngOfficeManager.Rows[0]["ManagerMeId"].ToString() : "";
        dr["ManagerMeName"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["ManagerMeName"])) ? dtEngOfficeManager.Rows[0]["ManagerMeName"].ToString() : "";
        dr["ManagerLastName"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["ManagerLastName"])) ? dtEngOfficeManager.Rows[0]["ManagerLastName"].ToString() : "";
        dr["Email"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["Email"])) ? dtEngOfficeManager.Rows[0]["Email"].ToString() : "";
        dr["FileNo"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["FileNo"])) ? dtEngOfficeManager.Rows[0]["FileNo"].ToString() : "";
        dr["CreateDate"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["CreateDate"])) ? dtEngOfficeManager.Rows[0]["CreateDate"].ToString() : "";
        dr["ParticipateLetterNo"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["ParticipateLetterNo"])) ? dtEngOfficeManager.Rows[0]["ParticipateLetterNo"].ToString() : "";
        dr["daftarAsnadNo"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["daftarAsnadNo"])) ? dtEngOfficeManager.Rows[0]["daftarAsnadNo"].ToString() : "";
        dr["DaftarasnadName"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["DaftarasnadName"])) ? dtEngOfficeManager.Rows[0]["DaftarasnadName"].ToString() : "";
        dr["MembershipStatus"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["MembershipStatus"])) ? dtEngOfficeManager.Rows[0]["MembershipStatus"].ToString() : "";
        dr["FirstRegDate"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["FirstRegDate"])) ? dtEngOfficeManager.Rows[0]["FirstRegDate"].ToString() : "";
        dr["OfficeExpireDate"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["OfficeExpireDate"])) ? dtEngOfficeManager.Rows[0]["OfficeExpireDate"].ToString() : "";
        dr["MeCount"] = (!Utility.IsDBNullOrNullValue(dtEngOfficeManager.Rows[0]["MeCount"])) ? dtEngOfficeManager.Rows[0]["MeCount"].ToString() : "";


        dt.Rows.Add(dr);
        return dt;

    }

    [WebMethod]
    public DataTable GetOfficeInfo(Int32 OfId, string UserName, string PassWord)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("OfName", typeof(string));
        dt.Columns.Add("officeTypeName", typeof(string));
        dt.Columns.Add("ManagerName", typeof(string));
        dt.Columns.Add("OfficeMembershipStatus", typeof(string));
        dt.Columns.Add("Subject", typeof(string));
        dt.Columns.Add("MeNo", typeof(string));
        dt.Columns.Add("ExpireDate", typeof(string));
        dt.Columns.Add("LastRevivalDate", typeof(string));
        dt.Columns.Add("RegisterDate", typeof(string));
        dt.Columns.Add("RegisterNo", typeof(string));
        dt.Columns.Add("FileNo", typeof(string));
        dt.Columns.Add("DocumentStatusName", typeof(string));
        dt.Columns.Add("CreateDate", typeof(string));
        dt.Columns.Add("MFTypeName", typeof(string));


        dt.TableName = "OfficeInfo";

        DataRow dr = dt.NewRow();
        if (UserName != _UserName || PassWord != _PassWord)
        {
            dr["Status"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }

        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        DataTable dtOfficeManager = OfficeManager.SelectOfficeInfoForTSWebService(OfId);
        int Count = dtOfficeManager.Rows.Count;

        if (Count != 1)
        {

            dr["Status"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }


        dr["Status"] = "There is Valid";
        dr["OfName"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["OfName"])) ? dtOfficeManager.Rows[0]["OfName"].ToString() : "";
        dr["officeTypeName"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["officeTypeName"])) ? dtOfficeManager.Rows[0]["officeTypeName"].ToString() : "";
        dr["ManagerName"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["ManagerName"])) ? dtOfficeManager.Rows[0]["ManagerName"].ToString() : "";
        dr["OfficeMembershipStatus"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["OfficeMembershipStatus"])) ? dtOfficeManager.Rows[0]["OfficeMembershipStatus"].ToString() : "";
        dr["Subject"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["Subject"])) ? dtOfficeManager.Rows[0]["Subject"].ToString() : "";
        dr["MeNo"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["MeNo"])) ? dtOfficeManager.Rows[0]["MeNo"].ToString() : "";
        dr["ExpireDate"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["ExpireDate"])) ? dtOfficeManager.Rows[0]["ExpireDate"].ToString() : "";
        dr["LastRevivalDate"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["LastRevivalDate"])) ? dtOfficeManager.Rows[0]["LastRevivalDate"].ToString() : "";
        dr["RegisterDate"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["RegisterDate"])) ? dtOfficeManager.Rows[0]["RegisterDate"].ToString() : "";
        dr["RegisterNo"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["RegisterNo"])) ? dtOfficeManager.Rows[0]["RegisterNo"].ToString() : "";
        dr["FileNo"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["FileNo"])) ? dtOfficeManager.Rows[0]["FileNo"].ToString() : "";
        dr["DocumentStatusName"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["DocumentStatusName"])) ? dtOfficeManager.Rows[0]["DocumentStatusName"].ToString() : "";
        dr["CreateDate"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["CreateDate"])) ? dtOfficeManager.Rows[0]["CreateDate"].ToString() : "";
        dr["MFTypeName"] = (!Utility.IsDBNullOrNullValue(dtOfficeManager.Rows[0]["MFTypeName"])) ? dtOfficeManager.Rows[0]["MFTypeName"].ToString() : "";


        dt.Rows.Add(dr);
        return dt;
    }  

}

