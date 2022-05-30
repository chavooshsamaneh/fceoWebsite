using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Summary description for MemberMarkazi
/// </summary>
[WebService(Namespace = "https://fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MemberMarkazi : System.Web.Services.WebService
{
    private string _UserName
    {
        get
        {
            return "Markazi";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMF@MeInfoMarkazi_XP#";
        }
    }
    public MemberMarkazi()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetListMohandesinKol(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        DataTable dt= memberMarkaziLogManager.SelectMemberForMarkaziWebServise(0, 0, 1);
        jsonString = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonString);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void TaeedListMohandesinInsert(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(2, 2, 1);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void TaeedListMohandesinUpdate(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(2, 3, 1);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void TaeedListMohandesinDelete(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(2, 4, 1);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetListMohandesinInsert(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(1, 2, 2);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetListMohandesinUpdate(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(1, 3, 2);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetListMohandesinDelete(string userName, string passWord)
    {
        string jsonString = string.Empty;
        if (userName != _UserName || passWord != _PassWord)
            return;
        TSP.DataManager.MemberMarkaziLogManager memberMarkaziLogManager = new TSP.DataManager.MemberMarkaziLogManager();
        memberMarkaziLogManager.UpdateMemberMarkaziLogBySwich(1, 4, 2);
    }



}

