using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Security;

/// <summary>
/// Summary description for MembersOfExpert27Info
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MembersOfExpert27Info : System.Web.Services.WebService
{
    private string _UserName
    {
        get
        {
            return "SNMFExpert27";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFExpert27";
        }
    }
    public MembersOfExpert27Info()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<BaseMemberExpert27Info> GetMemberExpert27Info(string UserName, string PassWord,int MeId,int EfId)
    {
        List<BaseMemberExpert27Info> ListBaseMemberExpert27Info = new List<BaseMemberExpert27Info>();
       

        if (UserName != _UserName || PassWord != _PassWord)
        {
            BaseMemberExpert27Info baseMemberExpert27Info = new BaseMemberExpert27Info();
            baseMemberExpert27Info.Status = "0";
            baseMemberExpert27Info.Description=  "UserName or Password are incorect";
            ListBaseMemberExpert27Info.Add(baseMemberExpert27Info);
            return ListBaseMemberExpert27Info;
        }
        TSP.DataManager.ExpertFileManager ExpertFileManager = new TSP.DataManager.ExpertFileManager();
        DataTable dtExpert= ExpertFileManager.SelectExpertFileForWebService(MeId, EfId);
        if (dtExpert.Rows.Count==0)
        {
            BaseMemberExpert27Info baseMemberExpert27Info = new BaseMemberExpert27Info();
            baseMemberExpert27Info.Status = "1";
            baseMemberExpert27Info.Description = "There is not data";
            ListBaseMemberExpert27Info.Add(baseMemberExpert27Info);
            return ListBaseMemberExpert27Info;
        }
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        for (int i = 0; i < dtExpert.Rows.Count; i++)
        {
            BaseMemberExpert27Info baseMemberExpert27Info = new BaseMemberExpert27Info();
            baseMemberExpert27Info.ListMemberExpert27InfoFileMajor = new List<MemberExpert27InfoFileMajor>();
            baseMemberExpert27Info.Status = "2";
            baseMemberExpert27Info.Description = "Sucsess";
            baseMemberExpert27Info.MeId = dtExpert.Rows[i]["MeId"].ToString();

            baseMemberExpert27Info.ExpertFileNo = dtExpert.Rows[i]["ExpertFileNo"].ToString();
            baseMemberExpert27Info.IssueDate = dtExpert.Rows[i]["IssueDate"].ToString();
            baseMemberExpert27Info.FirstIsuueDate = dtExpert.Rows[i]["FirstIsuueDate"].ToString();
            
            baseMemberExpert27Info.ExpireDate = dtExpert.Rows[i]["ExpireDate"].ToString();
            baseMemberExpert27Info.FirstName = dtExpert.Rows[i]["FirstName"].ToString();
            baseMemberExpert27Info.LastName = dtExpert.Rows[i]["LastName"].ToString();
            baseMemberExpert27Info.FatherName = dtExpert.Rows[i]["FatherName"].ToString();
            baseMemberExpert27Info.ImageUrl = dtExpert.Rows[i]["ImageUrl"].ToString();
            baseMemberExpert27Info.SSN = dtExpert.Rows[i]["SSN"].ToString();
            baseMemberExpert27Info.FileNo = dtExpert.Rows[i]["FileNo"].ToString();
            baseMemberExpert27Info.MeNo = dtExpert.Rows[i]["MeNo"].ToString();
            baseMemberExpert27Info.BirhtDate = dtExpert.Rows[i]["BirhtDate"].ToString();
            baseMemberExpert27Info.IdNo = dtExpert.Rows[i]["IdNo"].ToString();
            baseMemberExpert27Info.MobileNo = dtExpert.Rows[i]["MobileNo"].ToString();
            baseMemberExpert27Info.SexId = dtExpert.Rows[i]["SexId"].ToString();
            baseMemberExpert27Info.Sex = dtExpert.Rows[i]["Sex"].ToString();
            baseMemberExpert27Info.AgentCode = dtExpert.Rows[i]["AgentCode"].ToString();
            baseMemberExpert27Info.MemberStatus = dtExpert.Rows[i]["MemberStatus"].ToString();
            baseMemberExpert27Info.ObsGrade = dtExpert.Rows[i]["ObsGrade"].ToString();
            baseMemberExpert27Info.DesGrade = dtExpert.Rows[i]["DesGrade"].ToString();
            baseMemberExpert27Info.UrbanismGrade = dtExpert.Rows[i]["UrbanismGrade"].ToString();
            baseMemberExpert27Info.MappingGrade = dtExpert.Rows[i]["MappingGrade"].ToString();
            baseMemberExpert27Info.TrafficGrade = dtExpert.Rows[i]["TrafficGrade"].ToString();
            baseMemberExpert27Info.TaskName = dtExpert.Rows[i]["TaskName"].ToString();
            baseMemberExpert27Info.UrbanismGrade = dtExpert.Rows[i]["UrbanismGrade"].ToString(); 
            baseMemberExpert27Info.TiName = dtExpert.Rows[i]["TiName"].ToString();

            baseMemberExpert27Info.TaskCode = dtExpert.Rows[i]["TaskCode"].ToString();

            baseMemberExpert27Info.FileRegisterDate = dtExpert.Rows[i]["DocMeRegDate"].ToString();

            baseMemberExpert27Info.FileExpireDate = dtExpert.Rows[i]["DocMeExpireDate"].ToString();

            DataTable dt= DocMemberFileMajorManager.SelectDocMemberFileMajorForTSWorkRequest(-1, Convert.ToInt32(dtExpert.Rows[i]["MeId"]));
            if (dt.Rows.Count>0)
            {
                           
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                MemberExpert27InfoFileMajor MemberExpert27InfoFileMajor = new MemberExpert27InfoFileMajor();
                MemberExpert27InfoFileMajor.MajorParentId = dt.Rows[j]["MajorParentId"].ToString();
                MemberExpert27InfoFileMajor.MajorParentName = dt.Rows[j]["MajorParentName"].ToString();
                baseMemberExpert27Info.ListMemberExpert27InfoFileMajor.Add(MemberExpert27InfoFileMajor);
            }
            }
            ListBaseMemberExpert27Info.Add(baseMemberExpert27Info);
        }
        return ListBaseMemberExpert27Info;
    }

}

public class BaseMemberExpert27Info
{
   public string Status,Description,MeId, ExpertFileNo, IssueDate, FirstIsuueDate, ExpireDate, FirstName, LastName, FatherName, ImageUrl, SSN, FileNo, FileRegisterDate, FileExpireDate, MeNo, BirhtDate, IdNo,  MobileNo, SexId, Sex, AgentCode, MemberStatus, ObsGrade, DesGrade, UrbanismGrade, MappingGrade, TrafficGrade, TaskName, TiName, TaskCode;
   public List<MemberExpert27InfoFileMajor> ListMemberExpert27InfoFileMajor;
}
public class MemberExpert27InfoFileMajor
{
   public string MajorParentId, MajorParentName;
}
