using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for MemberWebService
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MemberWebService : System.Web.Services.WebService
{


    private string _UserName
    {
        get
        {
            return "SNMFMemberInfo";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFMemberInfo";
        }
    }


    public MemberWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    /// <returns></returns>
    [WebMethod]  
    public DataTable GetMembersLicence(Int32 MeId, string UserName, string PassWord)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("MLId", typeof(string));
        dt.Columns.Add("UnName", typeof(string));
        dt.Columns.Add("MjName", typeof(string));
        dt.Columns.Add("LiName", typeof(string));
        dt.Columns.Add("MeLicenceNamertl", typeof(string));
        dt.Columns.Add("CitName", typeof(string));
        dt.TableName = "Memberlicence";

        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "Wrong UserName Or Password";
            dt.Rows.Add(dr);
            return dt;
        }
        
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.SelectMemberLicence(MeId, -1, -1, 0, -1);
        int Count = MemberLicenceManager.Count;
        if (Count == 0)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }
        
        for (int i = 0; i < Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "There is Valid";
            dr["MLId"] = MemberLicenceManager[i]["MLId"].ToString();
            dr["UnName"] = MemberLicenceManager[i]["UnName"].ToString();
            dr["MjName"] = MemberLicenceManager[i]["MjName"].ToString();
            dr["LiName"] = MemberLicenceManager[i]["LiName"].ToString();
            dr["MeLicenceNamertl"] = MemberLicenceManager[i]["MeLicenceNamertl"].ToString();
            dr["CitName"] = MemberLicenceManager[i]["CitName"].ToString();
            dt.Rows.Add(dr);
        }

        return dt;
    }

    [WebMethod]
    public string[] GetMembersLicenceInfo(Int32 MeId, Int32 MLId, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return Info;
        }

        Info = new string[8];
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            Info[0] = MemberManager[0]["MeName"].ToString();
            Info[1] = MemberManager[0]["IdNo"].ToString();
            Info[2] = MemberManager[0]["SSN"].ToString();
            Info[3] = MemberManager[0]["IssuePlace"].ToString();
            Info[4] = MemberManager[0]["FatherName"].ToString();
        }

        MemberLicenceManager.FindByCode(MLId);
        if (MemberLicenceManager.Count == 1)
        {
            Info[5] = MemberLicenceManager[0]["MeLicenceNamertl"].ToString();
            Info[6] = MemberLicenceManager[0]["UnName"].ToString();
            Info[7] = MemberLicenceManager[0]["EndDate"].ToString();
        }
        return Info;
    }


}
