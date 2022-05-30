using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for AutomationWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AutomationWebService : System.Web.Services.WebService
{

    public AutomationWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(true)]
    public string InsertRecFax(string userName, string pass,byte[] img,int sec,string fileName)
    {
        if(img==null)
            return "تصویری ارسال نشده است";
        int userId=-1;
        TSP.DataManager.LoginManager log = new TSP.DataManager.LoginManager();
        System.Data.DataTable dt = log.LoginUser(userName, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "sha1"));
        if (dt.Rows.Count != 1)
            return "نام کاربری اشتباه می باشد";
            
            userId = (int)dt.Rows[0]["UserId"];
            string path = Server.MapPath("~/Image/Automation/Letters/")+fileName;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
                fs.Write(img, 0, img.Length);
                fs.Close();
                fs.Dispose();
            }
            catch(Exception err)
            {
                return err.Message;
            }
            return Utility.InsertInLetter(new string[] { "~/Image/Automation/Letters/"+fileName }, sec, userId);
    }
    [WebMethod(true)]
    public bool IsUserInSecretariat(int SecId, string UserName)
    {
        TSP.DataManager.LoginManager log = new TSP.DataManager.LoginManager();
        System.Data.DataTable dt= log.FindByUsername(UserName);
        if (dt.Rows.Count == 0)
            return false;
        TSP.DataManager.Automation.SecretariatNezamChartManager nezamChart = new TSP.DataManager.Automation.SecretariatNezamChartManager();
        return nezamChart.IsUserInSecretariat(SecId, (int)dt.Rows[0]["MeId"]);
    }
    [WebMethod(true)]
    public System.Data.DataSet GetSecretariat()
    {
        TSP.DataManager.Automation.SecretariatManager sec = new TSP.DataManager.Automation.SecretariatManager();
        sec.Fill();
        return sec.DataSet;
    }
    [WebMethod(true)]
    public bool CheckUserName(string userName,string pass)
    {
        TSP.DataManager.LoginManager log = new TSP.DataManager.LoginManager();
        System.Data.DataTable dt= log.LoginUser(userName,System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "sha1"));
        if (dt.Rows.Count != 1)
            return false;
     
        return true;
    }

  
}

