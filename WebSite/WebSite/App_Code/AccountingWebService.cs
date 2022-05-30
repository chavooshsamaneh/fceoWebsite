using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
using System.Data;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for AccountingWebService
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AccountingWebService : System.Web.Services.WebService
{
    private string _UserName
    {
        get
        {
            return "SNMFAccounting";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFAccounting";
        }
    }

    public enum MemberType
    {
        Temporary = 1
        , Permanent = 0
    }

    public AccountingWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="MemberType"></param>
    /// <returns>کد عضویت دائم، کد عضویت موقت، نام، نام خانوادگی، نام پدر، مقطع تحصیلی، کد ملی، کد پستی محل سکونت، شماره همراه، شماره حساب، شماره پروانه اشتغال، رشته </returns>
    [WebMethod(Description = "this func get a Member id & member type and returns string list include new member ids that registerd after it")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetMemberInfoByCode(Int32 MeId, MemberType MemberType, string UserName, string PassWord)
    {

        string[] Info = new string[12];
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        if (MemberType == AccountingWebService.MemberType.Permanent)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            DataTable dtMember = MemberManager.SelectMemberForWebservice(MeId);
            if (dtMember.Rows.Count > 0)
            {
                Info[0] = dtMember.Rows[0]["MeId"].ToString();
                Info[1] = dtMember.Rows[0]["TMeId"].ToString();
                Info[2] = dtMember.Rows[0]["FirstName"].ToString();
                Info[3] = dtMember.Rows[0]["LastName"].ToString();
                Info[4] = dtMember.Rows[0]["FatherName"].ToString();
                Info[5] = dtMember.Rows[0]["LiName"].ToString();
                Info[6] = dtMember.Rows[0]["SSN"].ToString();
                Info[7] = dtMember.Rows[0]["HomePO"].ToString();
                Info[8] = dtMember.Rows[0]["MobileNo"].ToString();
                Info[9] = dtMember.Rows[0]["BankAccNo"].ToString();
                Info[10] = dtMember.Rows[0]["FileNo"].ToString();
                Info[11] = dtMember.Rows[0]["MjName"].ToString();
            }
        }
        else if (MemberType == AccountingWebService.MemberType.Temporary)
        {
            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            DataTable dtTempMember = TempMemberManager.SelectTempMemberForWebservice(MeId);
            if (dtTempMember.Rows.Count > 0)
            {
                Info[0] = "0";
                Info[1] = dtTempMember.Rows[0]["TMeId"].ToString();
                Info[2] = dtTempMember.Rows[0]["FirstName"].ToString();
                Info[3] = dtTempMember.Rows[0]["LastName"].ToString();
                Info[4] = dtTempMember.Rows[0]["FatherName"].ToString();
                Info[5] = dtTempMember.Rows[0]["LiName"].ToString();
                Info[6] = dtTempMember.Rows[0]["SSN"].ToString();
                Info[7] = dtTempMember.Rows[0]["HomePO"].ToString();
                Info[8] = dtTempMember.Rows[0]["MobileNo"].ToString();
                Info[9] = dtTempMember.Rows[0]["BankAccNo"].ToString();
                Info[10] = dtTempMember.Rows[0]["FileNo"].ToString();
                Info[11] = dtTempMember.Rows[0]["MjName"].ToString();
            }
        }
        return new JavaScriptSerializer().Serialize(Info);
    }

    [WebMethod(Description = "this func get a Member id and returns string list include new member ids that registerd after it")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetNewMembersCode(Int32 MeId, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMember = MemberManager.SelectNewMembersCodeForWebService(MeId);
        Info = new string[dtMember.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dtMember.Rows)
        {
            Info[i] = dr["MeId"].ToString();
            i++;
        }

        return new JavaScriptSerializer().Serialize(Info);
    }


    [WebMethod(Description = "this func get a Member id & member type and returns string list include new member ids that registerd after it")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetNewMembersCodeByType(Int32 MeId, MemberType MemberType, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        if (MemberType == AccountingWebService.MemberType.Permanent)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            DataTable dtMember = MemberManager.SelectNewMembersCodeForWebService(MeId);
            Info = new string[dtMember.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dtMember.Rows)
            {
                Info[i] = dr["MeId"].ToString();
                i++;
            }
            return new JavaScriptSerializer().Serialize(Info);
        }
        else if (MemberType == AccountingWebService.MemberType.Temporary)
        {
            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            DataTable dtMember = TempMemberManager.SelectNewTempMembersCodeForWebService(MeId);
            Info = new string[dtMember.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dtMember.Rows)
            {
                Info[i] = dr["TMeId"].ToString();
                i++;
            }
            return new JavaScriptSerializer().Serialize(Info);
        }
        else
        {
            Info = new string[1];
            Info[0] = "MemberType is not valid";
            return new JavaScriptSerializer().Serialize(Info);
        }
    }
    /// <summary>
    /// this func get an project id and returns string array include project info
    /// </summary>
    /// <param name="ProjectId"></param>
    /// <returns>کد پروژه، نام پروژه، نام مالک، کد ملی مالک، زیربنا، بیشترین تعداد طبقه، پلاك ثبتی، پروانه ساخت، گروه ساختمانی، شماره پرونده،شماره همراه مالک</returns>
    [WebMethod(Description = "this func get a project id and returns string array include project info")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetProjectInfo(Int32 ProjectId, string UserName, string PassWord)
    {
        string[] Info = new string[12];
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        DataTable dtProject = ProjectManager.SelectTSProjectForWebService(ProjectId);
        if (dtProject.Rows.Count > 0)
        {
            Info[0] = dtProject.Rows[0]["ProjectId"].ToString();
            Info[1] = dtProject.Rows[0]["ProjectName"].ToString();
            Info[2] = dtProject.Rows[0]["OwnerName"].ToString();
            Info[3] = dtProject.Rows[0]["OwnerSSID"].ToString();
            Info[4] = dtProject.Rows[0]["Foundation"].ToString();
            Info[5] = dtProject.Rows[0]["MaxStageNum"].ToString();
            Info[6] = dtProject.Rows[0]["RegisteredNo"].ToString();
            Info[7] = dtProject.Rows[0]["LicenseNo"].ToString();
            Info[8] = dtProject.Rows[0]["GroupName"].ToString();
            Info[9] = dtProject.Rows[0]["FileNo"].ToString();
            Info[10] = dtProject.Rows[0]["OwnerMobile"].ToString();
            Info[11] = dtProject.Rows[0]["CityName"].ToString();
            

        }
        return new JavaScriptSerializer().Serialize(Info);
    }

    /// <summary>
    /// this func get an project id and returns string array include project info
    /// </summary>
    /// <param name="ProjectId"></param>
    /// <returns>کد پروژه، نام پروژه، نام مالک، کد ملی مالک، زیربنا، بیشترین تعداد طبقه، پلاك ثبتی، پروانه ساخت، گروه ساختمانی، شماره پرونده</returns>
    [WebMethod(Description = "this func get a project id and returns string array include project info")]

    public ProjectInfo GetProjectInfoXML(Int32 ProjectId, string UserName, string PassWord)
    {
        ProjectInfo project = new ProjectInfo();

        if (UserName != _UserName || PassWord != _PassWord)
        {
            project.Status = "-3";
            project.Message = "UserNameC or PassWordC is not valid!";
            return project;
        }

        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        DataTable dtProject = ProjectManager.SelectTSProjectForWebService(ProjectId);
        if (dtProject.Rows.Count < 1)
        {
            project.Status = "-2";
            project.Message = "The project Id Is not valid";
            return project;
        }
        if (dtProject.Rows.Count > 1)
        {
            project.Status = "-1";
            project.Message = "Project's information is corrupted.Please Call DB administrator.";// "By this UserName Can not return specifid Person";
            return project;
        }
        if (dtProject.Rows.Count > 0)
        {
            project.Status = "0";
            project.Message = "OK!";
            project.ProjectId = dtProject.Rows[0]["ProjectId"].ToString();
            project.ProjectName = dtProject.Rows[0]["ProjectName"].ToString();
            project.OwnerName = dtProject.Rows[0]["OwnerName"].ToString();
            project.OwnerSSID = dtProject.Rows[0]["OwnerSSID"].ToString();
            project.Foundation = dtProject.Rows[0]["Foundation"].ToString();
            project.MaxStageNum = dtProject.Rows[0]["MaxStageNum"].ToString();
            project.RegisteredNo = dtProject.Rows[0]["RegisteredNo"].ToString();
            project.LicenseNo = dtProject.Rows[0]["LicenseNo"].ToString();
            project.GroupName = dtProject.Rows[0]["GroupName"].ToString();
            project.FileNo = dtProject.Rows[0]["FileNo"].ToString();
            project.OwnerMobileNo = dtProject.Rows[0]["OwnerMobile"].ToString();

        }
        return project;
    }

    [WebMethod(Description = "this func get a project id and Agent Id of  this project so returns string list include new project ids that registerd after this project in this agent ")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetNewProjectsCode(Int32 ProjectCode, Int32 AgentId, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        DataTable dtProject = ProjectManager.SelectNewTSProjectIdForWebService(ProjectCode, AgentId);
        Info = new string[dtProject.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dtProject.Rows)
        {
            Info[i] = dr["ProjectId"].ToString();
            i++;
        }

        return new JavaScriptSerializer().Serialize(Info);

    }


    /// <summary>
    /// this func returns string array include office info
    /// </summary>
    /// <param name="OfficeId"></param>
    /// <returns>  کد شرکت، نام شرکت، نوع شرکت، مدیر مسئول، شماره ثبت، تاریخ ثبت، تاریخ ثبت در سیستم، تاریخ آخرین تمدید، تاریخ اعتبار پروانه، شماره تلفن، شماره عضویت، شماره همراه، وضعیت عضویت</returns>
    [WebMethod(Description = "this func get an office id and returns string array include office info")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetCoInfoByCode(Int32 OfficeId, string UserName, string PassWord)
    {
        string[] Info = new string[13];
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        DataTable dtOffice = OfficeManager.SelectOfficeForWebService(OfficeId);
        if (dtOffice.Rows.Count > 0)
        {
            Info[0] = dtOffice.Rows[0]["OfId"].ToString();
            Info[1] = dtOffice.Rows[0]["OfName"].ToString();
            Info[2] = dtOffice.Rows[0]["OtName"].ToString();
            Info[3] = dtOffice.Rows[0]["MName"].ToString();
            Info[4] = dtOffice.Rows[0]["RegOfNo"].ToString();
            Info[5] = dtOffice.Rows[0]["RegDate"].ToString();
            Info[6] = dtOffice.Rows[0]["CreateDate"].ToString();
            Info[7] = dtOffice.Rows[0]["LastRegDate"].ToString();
            Info[8] = dtOffice.Rows[0]["FileDate"].ToString();
            Info[9] = dtOffice.Rows[0]["Tel1"].ToString();
            Info[10] = dtOffice.Rows[0]["MeNo"].ToString();
            Info[11] = dtOffice.Rows[0]["MobileNo"].ToString();
            Info[12] = dtOffice.Rows[0]["OffStatus"].ToString();
        }
        return new JavaScriptSerializer().Serialize(Info);
    }
    /// <summary>
    /// this func returns string array include office info
    /// </summary>
    /// <param name="OfficeId"></param>
    /// <returns>  کد شرکت، نام شرکت، نوع شرکت، مدیر مسئول، شماره ثبت، تاریخ ثبت، تاریخ ثبت در سیستم، تاریخ آخرین تمدید، تاریخ اعتبار پروانه، شماره تلفن، شماره عضویت، شماره همراه، وضعیت عضویت</returns>
    [WebMethod(Description = "this func get an office id and returns string array include office info")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetOfficeInfoByCode(DateTime ConfirmDateTime, string UserName, string PassWord)
    {
        string[] Info = new string[13];
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        DataTable dtOffice = OfficeManager.SelectOfficeForWebServiceBasedOnConfirmDateTime(-1, ConfirmDateTime);
        if (dtOffice.Rows.Count > 0)
        {
            Info[0] = dtOffice.Rows[0]["OfId"].ToString();
            Info[1] = dtOffice.Rows[0]["OfName"].ToString();
            Info[2] = dtOffice.Rows[0]["OtName"].ToString();
            Info[3] = dtOffice.Rows[0]["MName"].ToString();
            Info[4] = dtOffice.Rows[0]["RegOfNo"].ToString();
            Info[5] = dtOffice.Rows[0]["RegDate"].ToString();
            Info[6] = dtOffice.Rows[0]["CreateDate"].ToString();
            Info[7] = dtOffice.Rows[0]["LastRegDate"].ToString();
            Info[8] = dtOffice.Rows[0]["FileDate"].ToString();
            Info[9] = dtOffice.Rows[0]["Tel1"].ToString();
            Info[10] = dtOffice.Rows[0]["MeNo"].ToString();
            Info[11] = dtOffice.Rows[0]["MobileNo"].ToString();
            Info[12] = dtOffice.Rows[0]["OffStatus"].ToString();
        }
        return new JavaScriptSerializer().Serialize(Info);
    }




    [WebMethod(Description = "this func get an office id and returns string list include new office ids that registerd after it")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetNewCoCode(Int32 OfficeId, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        DataTable dtOffice = OfficeManager.SelectNewOfficeIdForWebService(OfficeId);
        Info = new string[dtOffice.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dtOffice.Rows)
        {
            Info[i] = dr["OfId"].ToString();
            i++;
        }
        return new JavaScriptSerializer().Serialize(Info);
    }



    [WebMethod(Description = "this func get DateTime and returns string list include new office ids that is registerd after that Time")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string GetNewOfficeCode(DateTime ConfirmDateTime, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return new JavaScriptSerializer().Serialize(Info);
        }
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        DataTable dtOffice = OfficeManager.SelectNewOfficeIdForWebServiceBasedOnConfirmDateTime(-1, ConfirmDateTime);
        Info = new string[dtOffice.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dtOffice.Rows)
        {
            Info[i] = dr["OfId"].ToString();
            i++;
        }
        return new JavaScriptSerializer().Serialize(Info);
    }

}
public class OfficeInfo
{
    public string Status, Message, OfId, OfName, OtName, MName, RegOfNo, RegDate, CreateDate, LastRegDate, FileDate, Tel1, MeNo, MobileNo, OffStatus;
}
public class ProjectInfo
{
    public string ProjectId, ProjectName, OwnerName, OwnerSSID, Foundation, MaxStageNum, RegisteredNo, LicenseNo, GroupName, FileNo, Status, Message, OwnerMobileNo;
}
public class MemberInfo
{
    public string Status, Message, MeId, TMeId, FirstName, LastName, FatherName, LiName, SSN, HomePO, MobileNo, BankAccNo, FileNo, MjName;
}
