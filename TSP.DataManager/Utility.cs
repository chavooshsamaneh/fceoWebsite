using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace TSP.DataManager
{
    public class Utility
    {
        public static Boolean IsDBNullOrNullValue(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return true;
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            return false;
        }

        /// <summary>
        /// Get Persian Date of Today in format: YYYY/MM/DD
        /// </summary>
        /// <returns></returns>
        public static string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        /// <summary>
        /// Get Persian Year of Today in format: YYYY
        /// </summary>
        /// <returns></returns>
        public static string GetYearOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today).ToString();
            return PersianDate;
        }

        /// <summary>
        /// اضافه کردن ماه به تاریخ شمسی، بازگرداندن تاریخ شمسی به فرمت روز/ماه/سال 
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public static String AddMonths(String PersianDate, int Months)
        {
            PersianCalendar FC;
            DateTime DT;
            String[] str = PersianDate.Split('/');
            DT = ShamsiToMiladi(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
            FC = new PersianCalendar();
            DateTime DtAddMonths = FC.AddMonths(DT, Months);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            return PDate.GetYear(DtAddMonths) + "/" + PDate.GetMonth(DtAddMonths).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddMonths).ToString().PadLeft(2, '0');
        }

        public static DateTime ShamsiToMiladi(int Year, int Month, int Day)
        {
            PersianCalendar FC = new PersianCalendar();
            return FC.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }

        public static System.Data.DataTable GetListOfYears(int CurrentYear)
        {
            System.Data.SqlClient.SqlConnection objConnection = new System.Data.SqlClient.SqlConnection(DBManager.CnnStr);
            System.Data.DataTable dt = new System.Data.DataTable(); ;
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter("GetListOfYears", objConnection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@thisyear", CurrentYear);
            adapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Get Current time in format: HH:MM
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
        }

        public static int GetCurrentProvinceId()
        {
            int PrId = 2;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceId"]))
                PrId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentProvinceId"]);
            return PrId;
        }

        public static Boolean IsZeroInvoiceCheck()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"] == null)
                return true;
            else
                return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"]));
        }

        public static Boolean CreateAccount()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["CreateAccount"] == null)
                return false;
            else
                return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CreateAccount"]));
        }

        public static Boolean IsOfficeMemberConfirmRequestNeeded()
        {
            try
            {
                int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsOfficeMemberConfirmRequestNeeded"]);
                return Convert.ToBoolean(State);
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static Boolean IsEngOfficeMemberConfirmRequestNeeded()
        {
            try
            {
                int State = int.Parse(System.Configuration.ConfigurationManager.AppSettings["IsEngOfficeMemberConfirmRequestNeeded"]);
                return Convert.ToBoolean(State);
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static Boolean TSProject_IsBasedOnStep()
        {
            bool IsBasedOnStep = false;
            if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["TSProjectIsBasedOnStep"]))
                IsBasedOnStep = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["TSProjectIsBasedOnStep"]));
            return IsBasedOnStep;
        }

        public static Boolean IsMemberMunicipulityTaxCheckForWorkRequest()
        {
            bool IsMemberMunicipulityTaxCheckForWorkRequest = true;
            if (!Utility.IsDBNullOrNullValue(System.Configuration.ConfigurationManager.AppSettings["WorkTax"]))
                IsMemberMunicipulityTaxCheckForWorkRequest = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["WorkTax"]));
            return IsMemberMunicipulityTaxCheckForWorkRequest;
        }
        public static string GetCorrectText(object o)
        {
            if (o == null || o == DBNull.Value)
                return "";
            return o.ToString();
        }

        public static string GetCurrentProvinceName()
        {
            return "فارس";
        }

        public static int GetCurrentAgentCode()
        {
            try
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CurrentAgentCode"]);
            }
            catch (Exception) { }
            return 1;
        }

        public static string GetAccountingOwnerName()
        {
            string AccountingOwnerName = "";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AccountingOwnerName"]))
                AccountingOwnerName = System.Configuration.ConfigurationManager.AppSettings["AccountingOwnerName"].ToString();
            return AccountingOwnerName;
        }

        public static string GetAccountingNumber(int AccType = -1, int CitId = -2)
        {
            //,AccountNumberDesign,AccountNmberObserving
            string AccountingNumber = "";

            TSP.DataManager.CityManager CityManager = new DataManager.CityManager();
            if (CitId != -2)
            {
                CityManager.FindByCode(CitId);
                if (CityManager.Count != 1) return "خطا در جستجو شماره حساب";
            }

            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                    if (CityManager.Count == 1)
                    {
                        AccountingNumber = CityManager[0]["AccountNmberObserving"].ToString();
                    }
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    if (CityManager.Count == 1)
                    {
                        AccountingNumber = CityManager[0]["AccountNmberObserving5Percent"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType._5In1000:
                    if (CityManager.Count == 1)
                    {
                        AccountingNumber = CityManager[0]["AccountNmber5In1000"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType._2In1000:
                    if (CityManager.Count == 1)
                    {
                        AccountingNumber = CityManager[0]["AccountNmber2In1000"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                    if (CityManager.Count == 1)
                    {
                        AccountingNumber = CityManager[0]["AccountNumberDesign"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.Registeration:
                case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                case (int)TSP.DataManager.TSAccountingAccType.Entrance:
                    AccountingNumber = "5784010009";
                    break;
                default:
                    if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AccountingNumber"]))
                        AccountingNumber = System.Configuration.ConfigurationManager.AppSettings["AccountingNumber"].ToString();
                    break;
            }
            return AccountingNumber;
        }

        private static string[] yekan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };


        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yekan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }

        public static string ConvertNumberToPersianNumber(string snum)
        {
            string stotal = "";
            if (snum == "0")
            {
                return yekan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <returns>0 ,greather than 1 ,-1:error </returns>
        public static string CheckMemberOfflineDebt(int MeId)
        {

            CheckOfflineDebt.CheckOfflineDebtSoapClient CheckOfflineDebt = new CheckOfflineDebt.CheckOfflineDebtSoapClient();

            CheckOfflineDebt.AuthSoapHd AuthenticationCheckOfflineDebt = new CheckOfflineDebt.AuthSoapHd();
            AuthenticationCheckOfflineDebt.strUserName = "SNMOfflineDebtUser";
            AuthenticationCheckOfflineDebt.strPassword = "SNMOfflineDebtPASS";
            string dept = CheckOfflineDebt.GetSumDebt(AuthenticationCheckOfflineDebt, MeId.ToString());

            if (dept == "-1")
            {
                return "-1";
            }
            else
            { return dept; }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <returns>0 ,greather than 1 ,-1:error </returns>
        public static string CheckMemberLoanDebt(int MeId)
        {

            LoanWebService.LoanWebServiceSoapClient LoanWebService = new LoanWebService.LoanWebServiceSoapClient();
            LoanWebService.AuthSoapHd AuthenticationLoanWebService = new LoanWebService.AuthSoapHd();
            AuthenticationLoanWebService.strUserName = "SNMLoanDebtUser";
            AuthenticationLoanWebService.strPassword = "SNMLoanDebtPASS";
            string loan = LoanWebService.GetSumLoanDebt(AuthenticationLoanWebService, MeId.ToString());
            if (loan == "-1")
            {
                return "-1";
            }
            else
            { return loan; }

        }

        /// </summary>
        /// <param name="MeId"></param>
        /// <returns>0 ,greather than 1 ,-1:error </returns>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">کد عضویت</param>
        /// <param name="Name">نام</param>
        /// <param name="LastName">نام خانوادگی</param>
        /// <param name="Amount">مبلغ پرداختی</param>
        /// <param name="PaymentId">شناسه پرداخت</param>
        /// <param name="referenceId">کد ارجاع</param>
        /// <returns></returns>
        public static string OfflineDebtAddPayment(int MeId, string Name, string LastName, string Amount, string PaymentId, string referenceId)
        {

            CheckOfflineDebt.CheckOfflineDebtSoapClient CheckOfflineDebt = new CheckOfflineDebt.CheckOfflineDebtSoapClient();

            CheckOfflineDebt.AuthSoapHd AuthenticationCheckOfflineDebt = new CheckOfflineDebt.AuthSoapHd();
            AuthenticationCheckOfflineDebt.strUserName = "SNMOfflineDebtUserPayment";
            AuthenticationCheckOfflineDebt.strPassword = "SNMOfflineDebtPASSPayment";
            return CheckOfflineDebt.AddPayment(AuthenticationCheckOfflineDebt, MeId.ToString(), Name, LastName, Amount, PaymentId, DateTime.Now, referenceId);

        }

        public static string OfflineDebtUpdatePayment(string Id)
        {

            CheckOfflineDebt.CheckOfflineDebtSoapClient CheckOfflineDebt = new CheckOfflineDebt.CheckOfflineDebtSoapClient();

            CheckOfflineDebt.AuthSoapHd AuthenticationCheckOfflineDebt = new CheckOfflineDebt.AuthSoapHd();
            AuthenticationCheckOfflineDebt.strUserName = "SNMOfflineDebtUserPayment";
            AuthenticationCheckOfflineDebt.strPassword = "SNMOfflineDebtPASSPayment";
            return CheckOfflineDebt.UpdatePayment(AuthenticationCheckOfflineDebt, Id);

        }


        /// <summary>
        /// result[0]:boolean result[1]:string(Message)
        /// </summary>
        /// <returns>true:InvisibleLinks</returns>
        public static System.Collections.ArrayList CheckMemberRequestVisibility()
        {
            System.Collections.ArrayList Result = new System.Collections.ArrayList(2);
            Result.Add(false);
            Result.Add("");
            string Date = Utility.GetDateOfToday();
            string Time = Utility.GetCurrentTime();
            if (Date == "1399/12/26" && string.Compare(Time, "14:15") >= 0)
            {
                Result[0] = true;
            }
            else if (string.Compare(Date, "1399/12/26") > 0 && string.Compare(Date, "1400/01/07") <= 0)
            {
                Result[0] = true;
                if (Date == "1400/01/07" && string.Compare(Time, "07:30") > 0)
                {
                    Result[0] = false;
                }
            }
            //else if (Date == "1398/01/11" && string.Compare(Time, "14:15") >= 0)
            //{
            //    Result[0] = true;
            //}
            //else if (string.Compare(Date, "1398/01/11") > 0 && string.Compare(Date, "1398/01/14") <= 0)
            //{
            //    Result[0] = true;
            //    if (Date == "1398/01/15" && string.Compare(Time, "07:30") > 0)
            //    {
            //        Result[0] = false;
            //    }
            //}
            if (Convert.ToBoolean(Result[0]))
            {
                Result[1] = "در حال حاضر دسترسی به ثبت درخواست در بخش های عضویت و پروانه اشتغال در سایت سازمان امکان پذیر نمی باشد";
                //"ضمن آرزوی سلامتی،با توجه به شیوع بیماری کرونا و محدودیت های ایجاد شده در خدمات رسانی، دسترسی به ثبت درخواست در بخش های عضویت و پروانه اشتغال در سایت سازمان از تاریخ 1398/12/25  الی 1399/02/31 مقدور نخواهد بود";
            }
            return Result;
        }

    }
}
