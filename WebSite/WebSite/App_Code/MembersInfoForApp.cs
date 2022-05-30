using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;

/// <summary>
/// Summary description for MembersInfoForApp
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MembersInfoForApp : System.Web.Services.WebService
{
    private string _UserName
    {
        get
        {
            return "MesInfoApp2019";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMF@MesInfoApp#";
        }
    }
    public MembersInfoForApp()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public string MemberInfoForObservers(string UserNameC, string PassWordC, string HasFileNo, string MajorFildCode, string AgentCode)
    //{
    //    int _HasFileNo = -1;
    //    int _MjCode = -1;
    //    int _AgentCode = -1;
    //    ListOfMemberForApp ListOfMemberForApp = new ListOfMemberForApp();
    //    if (UserNameC != _UserName || PassWordC != _PassWord)
    //    {
    //        ListOfMemberForApp.Status = "-3";
    //        ListOfMemberForApp.Message = "UserNameC or PassWordC is not valid!";
    //        return new JavaScriptSerializer().Serialize(ListOfMemberForApp);

    //    }
    //    int.TryParse(HasFileNo, out _HasFileNo);
    //    int.TryParse(MajorFildCode, out _MjCode);
    //    int.TryParse(AgentCode, out _AgentCode);



    //    return new JavaScriptSerializer().Serialize(ListOfMemberForApp);
    //}
    /// <summary>

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
    /// <returns></returns>
    [WebMethod]
    public DataTable GetMemberInfo(Int32 MeId, int HasFileNo, int MajorParentCode, int AgentCode, string UserName, string PassWord)
    {


        #region Define DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("MeNo", typeof(string));

        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName", typeof(string)); 
        dt.Columns.Add("FatherName", typeof(string));
        dt.Columns.Add("TitleName", typeof(string));

        dt.Columns.Add("MeliCardNo", typeof(string));
        dt.Columns.Add("ShenasnamehNo", typeof(string));
        dt.Columns.Add("BirhtDate", typeof(string));

        dt.Columns.Add("MemberStatus", typeof(string));

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


        dt.Columns.Add("MjParentName", typeof(string));
        dt.Columns.Add("MajorNameDocumentMaster", typeof(string));

        dt.Columns.Add("DocumntExpireDate", typeof(string));



        dt.Columns.Add("ImageUrl", typeof(string));

        dt.Columns.Add("MobileNo", typeof(string));



        dt.Columns.Add("AgentCode", typeof(string));
        dt.Columns.Add("LicenceIdMemberMaster", typeof(string));

        dt.Columns.Add("MjParentId", typeof(string));
        dt.Columns.Add("DocMajorParentName", typeof(string));

        dt.TableName = "MemberInfo";
        #endregion
      
        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "Incorrect UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }
        if ((MeId == -1  && MajorParentCode == -1 && AgentCode == -1)|| HasFileNo == -1)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "Parameters is not Valid";
            dt.Rows.Add(dr);
            return dt;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMemberManager = MemberManager.SelectMemberAndDocInfoForWebService(MeId, HasFileNo, MajorParentCode, AgentCode);
        int Count = dtMemberManager.Rows.Count;

        if (Count < 1)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "There is no data";
            dt.Rows.Add(dr);
            return dt;
        }

        for (int i = 0; i < Count; i++)
        {

            DataRow dr = dt.NewRow();
        dr["Status"] = "There is Valid";
        dr["MeNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MeNo"])) ? dtMemberManager.Rows[i]["MeNo"].ToString() : "";
        dr["FirstName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["FirstName"])) ? dtMemberManager.Rows[i]["FirstName"].ToString() : "";
        dr["LastName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["LastName"])) ? dtMemberManager.Rows[i]["LastName"].ToString() : "";
        dr["TitleName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["TitleName"])) ? dtMemberManager.Rows[i]["TitleName"].ToString() : "";
        dr["FatherName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["FatherName"])) ? dtMemberManager.Rows[i]["FatherName"].ToString() : "";
        dr["MeliCardNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MeliCardNo"])) ? dtMemberManager.Rows[i]["MeliCardNo"].ToString() : "";
        dr["ShenasnamehNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["ShenasnamehNo"])) ? dtMemberManager.Rows[i]["ShenasnamehNo"].ToString() : "";
        dr["BirhtDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["BirhtDate"])) ? dtMemberManager.Rows[i]["BirhtDate"].ToString() : "";
        dr["ImageUrl"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["ImageUrl"])) ? dtMemberManager.Rows[i]["ImageUrl"].ToString() : "";
        dr["MobileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MobileNo"])) ? dtMemberManager.Rows[i]["MobileNo"].ToString() : "";
        dr["MemberStatus"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MemberStatus"])) ? dtMemberManager.Rows[i]["MemberStatus"].ToString() : "";

        dr["AgentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["AgentName"])) ? dtMemberManager.Rows[i]["AgentName"].ToString() : "";
        dr["AgentCode"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["AgentCode"])) ? dtMemberManager.Rows[i]["AgentCode"].ToString() : "";

        dr["SexName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["SexName"])) ? dtMemberManager.Rows[i]["SexName"].ToString() : "";
        dr["Email"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["Email"])) ? dtMemberManager.Rows[i]["Email"].ToString() : "";
        dr["FileNo"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["FileNo"])) ? dtMemberManager.Rows[i]["FileNo"].ToString() : "";

        dr["ImpGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["ImpGrade"])) ? dtMemberManager.Rows[i]["ImpGrade"].ToString() : "";
        dr["ObsGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["ObsGrade"])) ? dtMemberManager.Rows[i]["ObsGrade"].ToString() : "";
        dr["DesGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["DesGrade"])) ? dtMemberManager.Rows[i]["DesGrade"].ToString() : "";
        dr["UrbanismGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["UrbanismGrade"])) ? dtMemberManager.Rows[i]["UrbanismGrade"].ToString() : "";
        dr["MappingGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MappingGrade"])) ? dtMemberManager.Rows[i]["MappingGrade"].ToString() : "";
        dr["TrafficGrade"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["TrafficGrade"])) ? dtMemberManager.Rows[i]["TrafficGrade"].ToString() : "";


        dr["LicenceNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["LicenceNameMemberMaster"])) ? dtMemberManager.Rows[i]["LicenceNameMemberMaster"].ToString() : "";
        dr["LicenceIdMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["LicenceIdMemberMaster"])) ? dtMemberManager.Rows[i]["LicenceIdMemberMaster"].ToString() : "";
        dr["MajorNameMemberMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MajorNameMemberMaster"])) ? dtMemberManager.Rows[i]["MajorNameMemberMaster"].ToString() : "";

        dr["MjParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MjParentName"])) ? dtMemberManager.Rows[i]["MjParentName"].ToString() : "";
        dr["MajorNameDocumentMaster"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MajorNameDocumentMaster"])) ? dtMemberManager.Rows[i]["MajorNameDocumentMaster"].ToString() : "";
        dr["DocumntExpireDate"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["DocumntExpireDate"])) ? dtMemberManager.Rows[i]["DocumntExpireDate"].ToString() : "";



        dr["MjParentId"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["MjParentId"])) ? dtMemberManager.Rows[i]["MjParentId"].ToString() : "";
        dr["DocMajorParentName"] = (!Utility.IsDBNullOrNullValue(dtMemberManager.Rows[i]["DocMajorParentName"])) ? dtMemberManager.Rows[i]["DocMajorParentName"].ToString() : "";


        dt.Rows.Add(dr);
        dt.AcceptChanges();
        }


        return dt;
    }



}

