using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.ServiceModel.Channels;
using System.ServiceModel;

/// <summary>
/// Summary description for ObserverTax
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ObserverTax : System.Web.Services.WebService
{
    private string _UserName
    {
        get
        {
            return "SNMFObserver";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMF#Tax#890214";
        }
    }
    public ObserverTax()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable InsertObserverTax(Int32 MeId, string PaymentDate, string PaymentTime, string Year, string Description, string UserName, string PassWord)
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Message", typeof(string));
        dt.Columns.Add("ObserverTaxId", typeof(string));
        dt.Columns.Add("ModifiedDate", typeof(string));
        dt.TableName = "tblObserverTax";

        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "0";
            dr["Message"]= "UserName Or Password was incorrect";
            dt.Rows.Add(dr);
            return dt;
        }
        //we must controll all digit of parameter date time and year
        if (PaymentDate.Length!=10 || PaymentTime.Length!=5 || Year.Length!=4)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "1";
            dr["Message"] = "one or more parameters (PaymentDate, PaymentTime, Year) were not in a correct format";
            dt.Rows.Add(dr);
            return dt;
        }

        int YPayment = 0;
        string YPaymentDate = PaymentDate.Substring(0, 4);
        int.TryParse(YPaymentDate, out YPayment);
        int MPayment = 0;
        string MPaymentDate = PaymentDate.Substring(5, 2);
        int.TryParse(MPaymentDate, out MPayment);
        int DPayment = 0;
        string DPaymentDate = PaymentDate.Substring(8, 2);
        int.TryParse(DPaymentDate, out DPayment);
        if (YPayment<1395 || YPayment>1400 || MPayment>12 || MPayment<1 || DPayment>31 || DPayment<1)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "2";
            dr["Message"] = "PaymentDate paramete Was not in a correct format or rang.It must is between 1395/01/01 to 1400/12/30.";
            dt.Rows.Add(dr);
            return dt;
        }
        int HPaymentT = 0;
        string HPaymentTime = PaymentTime.Substring(0, 2);
        int.TryParse(HPaymentTime, out HPaymentT);

        int MPaymentT = 0;
        string MPaymentTime = PaymentTime.Substring(3, 2);
        int.TryParse(MPaymentTime, out MPaymentT);

        if (HPaymentT<0 || HPaymentT>23 || MPaymentT<0 || MPaymentT>59)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "3";
            dr["Message"] = "PaymentTime paramete Was not in a correct format or rang.It must is between 00:00 to 23:59.";
            dt.Rows.Add(dr);
            return dt;
        }
        int IYear = 0;
        int.TryParse(Year, out IYear);
        if (IYear<1395 || IYear>1400)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "4";
            dr["Message"] = "PaymentTime Year Was not in a correct format or rang.It must is between 1395 to 1400.";
            dt.Rows.Add(dr);
            return dt;
        }
        TSP.DataManager.TechnicalServices.ObserverTaxManager observerTaxManager = new TSP.DataManager.TechnicalServices.ObserverTaxManager();
        DataTable dtCount = observerTaxManager.CountByMeId(MeId, Year);
        if (Convert.ToInt32(dtCount.Rows[0]["Count"].ToString())>0)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "5";
            dr["Message"] = "پیش از این برای این عضویت و با این سال کاری اطلاعات ثبت شده است";
            dt.Rows.Add(dr);
            return dt;
        }
      


        try
        {
            DataRow drObserverTax = observerTaxManager.NewRow();
            drObserverTax["MeId"] = MeId;
            drObserverTax["PaymentDate"] = PaymentDate;
            drObserverTax["PaymentTime"] = PaymentTime;
            drObserverTax["Year"] = Year;
            drObserverTax["Description"] = Description;
            drObserverTax["CreateDate"] = Utility.GetDateOfToday();
            drObserverTax["ModifiedDate"] = DateTime.Now;

            observerTaxManager.AddRow(drObserverTax);
            observerTaxManager.Save();
            observerTaxManager.DataTable.AcceptChanges();
            string observerTaxId= observerTaxManager[0]["ObserverTaxId"].ToString(); 
            string modifiedDate = observerTaxManager[0]["ModifiedDate"].ToString();

    DataRow dr = dt.NewRow();
            dr["Status"] = "6";
            dr["Message"] = "sucsess";
            dr["ObserverTaxId"] = observerTaxId;
            dr["ModifiedDate"] = modifiedDate;
            dt.Rows.Add(dr);
            return dt;
        }
        catch (Exception ex)
        {

            Utility.SaveWebsiteError(ex);
            throw;
        }
      
    }

    [WebMethod]
    public DataTable SelectObserverTaxByMeId(Int32 MeId, string UserName, string PassWord) {
        DataTable dt = new DataTable();
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Message", typeof(string));
        dt.TableName = "tblMessage";

        DataTable dtObserverTax = new DataTable();
        dtObserverTax.Columns.Add("ObserverTaxId", typeof(string));
        dtObserverTax.Columns.Add("MeId", typeof(string));
        dtObserverTax.Columns.Add("PaymentDate", typeof(string));
        dtObserverTax.Columns.Add("PaymentTime", typeof(string));
        dtObserverTax.Columns.Add("Year", typeof(string));
        dtObserverTax.Columns.Add("CreateDate", typeof(string));
        dtObserverTax.Columns.Add("ModifiedDate", typeof(string));
        dtObserverTax.Columns.Add("Description", typeof(string));
        dtObserverTax.TableName = "tblObserverTax";
      

        if (UserName != _UserName || PassWord != _PassWord)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "0";
            dr["Message"] = "UserName Or Password is incorrect";
            dt.Rows.Add(dr);
            return dt;
        }
        TSP.DataManager.TechnicalServices.ObserverTaxManager observerTaxManager = new TSP.DataManager.TechnicalServices.ObserverTaxManager();
         DataTable dtObserver= observerTaxManager.FindObserverTaxByMeId(MeId, "%");
        if (dtObserver.Rows.Count==0)
        {
            DataRow dr = dt.NewRow();
            dr["Status"] = "1";
            dr["Message"] = "There is not data";
            dt.Rows.Add(dr);
            return dt;
        }
        for (int i = 0; i < dtObserver.Rows.Count; i++)
        {
            DataRow drObserver = dtObserverTax.NewRow();
            drObserver = dtObserver.Rows[i];
            dtObserverTax.ImportRow(drObserver);
        }
        
        return dtObserverTax;
    }
}
