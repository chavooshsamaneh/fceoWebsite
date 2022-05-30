using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;

/// <summary>
/// Summary description for SmartPhoneApp
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmartPhoneApp : System.Web.Services.WebService {

    private string _UserName
    {
        get
        {
            return "PhoneApp2016";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMF@MeInfoPhoneApp#";
        }
    }
    public SmartPhoneApp () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)] 
    public string MemberInfo(string UserNameC, string PassWordC, string MeCode)
    {
        int meId=-1;
        Person person = new Person();
        if (UserNameC != _UserName || PassWordC != _PassWord)
        {
            person.Status = "-3";
            person.Message = "UserNameC or PassWordC is not valid!";
            return new JavaScriptSerializer().Serialize(person); 
        }
        int.TryParse(MeCode,out meId);
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dt = MemberManager.SelectMemberInfoForPhoneApp(meId);
        if (dt.Rows.Count < 1)
        {
            person.Status = "-2";
            person.Message = "The Member's UserName Is not valid";
            return new JavaScriptSerializer().Serialize(person); 
        }
        if (dt.Rows.Count > 1)
        {
            person.Status = "-1";
            person.Message = "Member's information is corrupted.Please Call DB administrator.";// "By this UserName Can not return specifid Person";
            return new JavaScriptSerializer().Serialize(person); 
        }
        if (dt.Rows.Count == 1)
        {
            person.Status = "0";
            person.Message = "OK!";
            person.MemberMajor = dt.Rows[0]["MjParentName"].ToString();
            person.MemberNum = dt.Rows[0]["MeNo"].ToString();
            person.FirstName = dt.Rows[0]["FirstName"].ToString();
            person.LastName = dt.Rows[0]["LastName"].ToString();
            person.ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
        }

        
        return new JavaScriptSerializer().Serialize(person); 
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string MemberPhoneInfo(string UserNameC, string PassWordC, string MeCode)
    {
        int meId = -1;
        Attentication attentication = new Attentication();
        if (UserNameC != _UserName || PassWordC != _PassWord)
        {
            attentication.Status = "-3";
            attentication.Message = "UserNameC Or PassWordC is not valid!";
            return new JavaScriptSerializer().Serialize(attentication);
        }
        int.TryParse(MeCode, out meId);
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dt = MemberManager.SelectMemberInfoForPhoneApp(meId);
        if (dt.Rows.Count < 1)
        {
            attentication.Status = "-2";
            attentication.Message = "The Member's UserName Is not valid";
            return new JavaScriptSerializer().Serialize(attentication);
        }
        if (dt.Rows.Count > 1)
        {
            attentication.Status = "-1";
            attentication.Message = "Member's information is corrupted.Please Call DB administrator.";//"By this UserName Can not return specifid Person";
            return new JavaScriptSerializer().Serialize(attentication);
        }
        if (dt.Rows.Count == 1)
        {
            attentication.Status = "0";
            attentication.Message = "OK!";
            attentication.PhoneNum = dt.Rows[0]["MobileNo"].ToString();
          
        }


        return new JavaScriptSerializer().Serialize(attentication); 
    }

    
}





public class Person
{
   public string Status,Message,MemberMajor,MemberNum,FirstName,LastName,ImageUrl;
}

public class Attentication
{
    public string Status, Message, PhoneNum;
}