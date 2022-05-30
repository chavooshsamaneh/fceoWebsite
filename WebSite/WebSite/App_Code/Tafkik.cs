using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Tafkik
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Tafkik : System.Web.Services.WebService {

    private string _UserName
    {
        get
        {
            return "SNMFWebservice";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMF0205#Webservice";
        }
    }

    public Tafkik () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private class MemberInfo
    {
        public string Status, Message, MeId, MeNo, FirstName, LastName, SexName, MemberStatus, SSN, MobileNo, ImageUrl, FileNo
            , ImpGrade, ImpGradeDate, ObsGrade, ObsGradeDate, DesGrade, DesGradeDate,
            UrbanismGrade, UrbanismGradeDate, MappingGrade, MappingGradeDate, TrafficGrade, TrafficGradeDate, DocumntFirstRegister, DocumntExpireDate,LicenceImageURL, BankAccNo;

                //, LicenceNameMemberMaster,
          //  LicenceIdMemberMaster, MajorNameMemberMaster, UniversityNameMemberMaster, GraduateDateMemberMaster, MjParentName, MjParentId,
          //  MajorNameDocumentMaster, DocumentRevivalDate,  WorkFlowName, DocMajorParentName;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MajorId"></param>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <returns></returns>
    [WebMethod]
    public System.Data. DataTable GetMemberInformation(Int32 MajorId, string UserName, string PassWord)
    {
       // MemberInfo meInfo = new MemberInfo();
        System.Data.DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Message", typeof(string));

        if (UserName != _UserName || PassWord != _PassWord)
        {
            System.Data.DataRow dr = dt.NewRow();
            dr["Status"] = "0";
            dr["Message"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        System.Data.  DataTable dtMemberManager = MemberManager.SelectMemberListByMajorForTafkik(MajorId);
        int Count = dtMemberManager.Rows.Count;

        if (Count ==0)
        {
            System.Data.DataRow dr = dt.NewRow();
            dr["Status"] = "0";
            dr["Message"] = "Cannot find Data";
            dt.Rows.Add(dr);
            return dt;

        }
        dtMemberManager.TableName = "MemberInfo";
        return dtMemberManager;
        //if (Count > 1)
        //{
        //    meInfo.Status = "0";
        //    meInfo.Message = "Member's information is corrupted.Please Call DB administrator.";
        //    return new JavaScriptSerializer().Serialize(meInfo);

        //}
        //if (Count == 1)
        //{         
        //    #region
        //    meInfo.Status = "1";
        //    meInfo.Message = "OK!";
        //    meInfo.MeId = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeId"])) ? dtMemberManager.Rows[0]["MeId"].ToString() : "";   
        //    meInfo.MeNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MeNo"])) ? dtMemberManager.Rows[0]["MeNo"].ToString() : "";            
        //    meInfo.FirstName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FirstName"])) ? dtMemberManager.Rows[0]["FirstName"].ToString() : "";
        //    meInfo.LastName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LastName"])) ? dtMemberManager.Rows[0]["LastName"].ToString() : "";          
        //    meInfo.MemberStatus = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MemberStatus"])) ? dtMemberManager.Rows[0]["MemberStatus"].ToString() : "";          
        //    meInfo.SexName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SexName"])) ? dtMemberManager.Rows[0]["SexName"].ToString() : "";
        //    meInfo.SSN = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["SSN"])) ? dtMemberManager.Rows[0]["SSN"].ToString() : "";                        
        //    meInfo.FileNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["FileNo"])) ? dtMemberManager.Rows[0]["FileNo"].ToString() : "";
        //    meInfo.ImpGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGrade"])) ? dtMemberManager.Rows[0]["ImpGrade"].ToString() : "";
        //    meInfo.ImpGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImpGradeDate"])) ? dtMemberManager.Rows[0]["ImpGradeDate"].ToString() : "";
        //    meInfo.ObsGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGrade"])) ? dtMemberManager.Rows[0]["ObsGrade"].ToString() : "";
        //    meInfo.ObsGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ObsGradeDate"])) ? dtMemberManager.Rows[0]["ObsGradeDate"].ToString() : "";
        //    meInfo.DesGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGrade"])) ? dtMemberManager.Rows[0]["DesGrade"].ToString() : "";
        //    meInfo.DesGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DesGradeDate"])) ? dtMemberManager.Rows[0]["DesGradeDate"].ToString() : "";
        //    meInfo.UrbanismGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGrade"])) ? dtMemberManager.Rows[0]["UrbanismGrade"].ToString() : "";
        //    meInfo.UrbanismGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["UrbanismGradeDate"])) ? dtMemberManager.Rows[0]["UrbanismGradeDate"].ToString() : "";
        //    meInfo.MappingGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGrade"])) ? dtMemberManager.Rows[0]["MappingGrade"].ToString() : "";
        //    meInfo.MappingGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MappingGradeDate"])) ? dtMemberManager.Rows[0]["MappingGradeDate"].ToString() : "";
        //    meInfo.TrafficGrade = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGrade"])) ? dtMemberManager.Rows[0]["TrafficGrade"].ToString() : "";
        //    meInfo.TrafficGradeDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["TrafficGradeDate"])) ? dtMemberManager.Rows[0]["TrafficGradeDate"].ToString() : "";
        //    meInfo.LicenceImageURL = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["LicenceImageURL"])) ? dtMemberManager.Rows[0]["LicenceImageURL"].ToString() : "";
        //    meInfo.DocumntFirstRegister = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntFirstRegister"])) ? dtMemberManager.Rows[0]["DocumntFirstRegister"].ToString() : "";
        //    meInfo.DocumntExpireDate = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocumntExpireDate"])) ? dtMemberManager.Rows[0]["DocumntExpireDate"].ToString() : "";            
        //    //meInfo.DocMajorParentName = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["DocMajorParentName"])) ? dtMemberManager.Rows[0]["DocMajorParentName"].ToString() : "";            
        //    meInfo.ImageUrl = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["ImageUrl"])) ? dtMemberManager.Rows[0]["ImageUrl"].ToString() : "";
        //    meInfo.MobileNo = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[0]["MobileNo"])) ? dtMemberManager.Rows[0]["MobileNo"].ToString() : "";            
        //    #endregion
        //}
        //return new JavaScriptSerializer().Serialize(meInfo);
    }
    
}
