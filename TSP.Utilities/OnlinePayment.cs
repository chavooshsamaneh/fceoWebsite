using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TSP
{
    public partial class Utility
    {
        private static string  GetDocumentMemberRequestTypeCode(TSP.DataManager.DocumentOfMemberRequestType DocumentOfMemberRequestTyp)
        {
            string DocumentMemberRequestTypeCode = "0000";

            switch (DocumentOfMemberRequestTyp)
            {
                case DataManager.DocumentOfMemberRequestType.New:
                    DocumentMemberRequestTypeCode = "0001";
                    break;
                case DataManager.DocumentOfMemberRequestType.UpGrade:
                    DocumentMemberRequestTypeCode = "0002";
                    break;
                case DataManager.DocumentOfMemberRequestType.Qualification:
                    DocumentMemberRequestTypeCode = "0003";
                    break;
                case DataManager.DocumentOfMemberRequestType.Revival:
                    DocumentMemberRequestTypeCode = "0004";
                    break;
                case DataManager.DocumentOfMemberRequestType.Change:
                    DocumentMemberRequestTypeCode = "0005";//***درخواست تغییرات فیش ندارد.بدلیل وجود کد بابت در مستندات در اینجا تعریف شد
                    break;
            }
            return DocumentMemberRequestTypeCode;
        }
        /// <summary>
        /// Payment Data and Settings
        /// </summary>
        public class OnlinePayment
        {
            #region Properties
            private static int _PaymentSuccessCode = 100;
            /// <summary>
            /// کد موفقیت تراکنش
            /// </summary>
            public static int PaymentSuccessCode
            {
                get { return _PaymentSuccessCode; }
            }

            private static int[] CheckDigitArray
            {
                get
                {
                    int[] _CheckDigitArray = { 17, 16, 15, 14, 13, 12, 11, 10, 9, 1, 2, 3, 4, 5, 6, 7, 8 };
                    return _CheckDigitArray;
                }
            }

            private static int[] CheckDigitPOSArray
            {
                get
                {
                    int[] _CheckDigitArray = { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83 };
                    return _CheckDigitArray;
                }
            }
            #endregion

            #region Usefull Methods
            private static System.Security.Cryptography.RijndaelManaged symAlg = new System.Security.Cryptography.RijndaelManaged();
            private static byte[] keys = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            private static byte[] iv = { 40, 80, 70, 213, 154, 178, 21, 102, 24, 78, 62, 34, 98, 145, 102, 36 };
            private static string EncryptQS(string item)
            {
                try
                {
                    symAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    symAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
                    symAlg.Key = keys;
                    symAlg.IV = iv;
                    byte[] input = System.Text.Encoding.Unicode.GetBytes(item);   //new byte[unicode.GetByteCount(item)];
                    input = symAlg.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);
                    return Convert.ToBase64String(input);
                }
                catch
                {
                    return null;
                }
            }
            #endregion

            public static string GetResultCodeTextForParsian(int resultCode)
            {
                switch (resultCode)
                {
                    case 0:
                        return "پرداخت با موفقیت انجام شد";
                    case -32768:
                        return "خطاي ناشناخته رخ داده است";
                    case -1552:
                        return "برگشت تراکنش مجاز نمی باشد";
                    case -1551:
                        return "برگشت تراکنش قبلا اًنجام شده است";
                    case -1550:
                        return "برگشت تراکنش در وضعیت جاري امکان پذیر";
                    case -1549:
                        return "زمان مجاز براي درخواست برگشت تراکنش به اتمام رسیده است";
                    case -1548:
                        return "فراخوانی سرویس درخواست پرداخت قبض ناموفق بود";
                    case -1540:
                        return "تایید تراکنش ناموفق می باشد";
                    case -1536:
                        return "فراخوانی سرویس درخواست شارژ تاپ آپ ناموفق بود";
                    case -1533:
                        return "تراکنش قبلاً تایید شده است";
                    case -1532:
                        return "تراکنش از سوي پذیرنده تایید شد";
                    case -1531:
                        return "تایید تراکنش ناموفق امکان پذیر نمی باشد";
                    case -1530:
                        return "پذیرنده مجاز به تایید این تراکنش نمی باشد";
                    case -1528:
                        return "اطلاعات پرداخت یافت نشد";
                    case -1527:
                        return "انجام عملیات درخواست پرداخت تراکنش خرید ناموفق بود";
                    case -1507:
                        return "تراکنش برگشت به سوئیچ ارسال شد";
                    case -1505:
                        return "تایید تراکنش توسط پذیرنده انجام شد";
                    case -138:
                        return "عملیات پرداخت توسط کاربر لغو شد";
                    case -132:
                        return "مبلغ تراکنش کمتر از حداقل مجاز میباشد";
                    case -131:
                        return "نامعتبر می باشد Token";
                    case -130:
                        return "زمان منقضی شده است Token";
                    case -128:
                        return "معتبر نمی باشدIP";
                    case -127:
                        return "آدرس اینترنتی معتبر نمی باشد";
                    case -126:
                        return "کد شناسایی پذیرنده معتبر نمی باشد";
                    case -121:
                        return "رشته داده شده بطور کامل عددي نمی باشد";
                    case -120:
                        return "طول داده ورودي معتبر نمی باشد";
                    case -119:
                        return "سازمان نامعتبر می باشد";
                    case -118:
                        return "مقدار ارسال شده عدد نمی باشد";
                    case -117:
                        return "طول رشته کم تر از حد مجاز می باشد";
                    case -116:
                        return "طول رشته بیش از حد مجاز می باشد";
                    case -115:
                        return "شناسه پرداخت نامعتبر می باشد";
                    case -114:
                        return "شناسه قبض نامعتبر می باشد";
                    case -113:
                        return "پارامتر ورودي خالی می باشد";
                    case -112:
                        return "شماره سفارش تکراري است";
                    case -111:
                        return "ممبلغ تراکنش بیش از حد مجاز پذیرنده می";
                    case -108:
                        return "قابلیت برگشت تراکنش براي پذیرنده غیر فعال می باشد";
                    case -107:
                        return "قابلیت ارسال تاییده تراکنش براي پذیرنده غیر فعال می باشد";
                    case -106:
                        return "قابلیت شارژ براي پذیرنده غیر فعال می باشد";
                    case -105:
                        return "قابلیت تاپ آپ براي پذیرنده غیر فعال می باشد";
                    case -104:
                        return "قابلیت پرداخت قبض براي پذیرنده غیر فعال می باشد";
                    case -103:
                        return "قابلیت خرید براي پذیرنده غیر فعال می باشد";
                    case -102:
                        return "تراکنش با موفقیت برگشت داده شد";
                    case -101:
                        return "پذیرنده اهراز هویت نشد";
                    case -100:
                        return "پذیرنده غیرفعال می باشد";
                    case -1:
                        return "خطاي سرور";
                    case 1:
                        return "صادرکننده ي کارت از انجام تراکنش صرف نظر کرد";
                    case 2:
                        return "عملیات تاییدیه این تراکنش قبلا باموفقیت صورت پذیرفته است";
                    case 3:
                        return "پذیرنده ي فروشگاهی نامعتبر می باشد";
                    case 5:
                        return "از انجام تراکنش صرف نظر شد";
                    case 6:
                        return "بروز خطایی ناشناخته";
                    case 8:
                        return "باتشخیص هویت دارنده ي کارت، تراکنش موفق می باشد";
                    case 9:
                        return "درخواست رسیده در حال پی گیري و انجام است";
                    case 10:
                        return "تراکنش با مبلغی پایین تر از مبلغ درخواستی )کمبود حساب مشتري(پذیرفته شده است";
                    case 12:
                        return "تراکنش نامعتبر است";
                    case 13:
                        return "مبلغ تراکنش نادرست است";
                    case 14:
                        return "شماره کارت ارسالی نامعتبر است";
                    case 15:
                        return "صادرکننده ي کارت نامعتبراست";
                    case 17:
                        return "مشتري درخواست کننده حذف شده است";
                    case 20:
                        return "در موقعیتی که سوئیچ جهت پذیرش تراکنش نیازمند پرس و جو از کارت است ممکن است";
                    case 21:
                        return "در صورتی که پاسخ به در خواست ترمینا ل نیازمند هیچ پاسخ خاص یا عملکردي نباشیم این پیام را خواهیم داشت";
                    case 22:
                        return "تراکنش مشکوك به بد عمل کردن ( کارت ، ترمینال ، دارنده کارت ) بوده است لذاپذیرفته نشده است";
                    case 30:
                        return "قالب پیام داراي اشکال است";
                    case 31:
                        return "پذیرنده توسط سوئی پشتیبانی نمی شود";
                    case 32:
                        return "تراکنش به صورت غیر قطعی کامل شده است";
                    case 33:
                        return "تاریخ انقضاي کارت سپري شده است";
                    case 38:
                        return "تعداد دفعات ورود رمزغلط بیش از حدمجاز است.کارت توسط دستگاه ضبط شود";
                    case 39:
                        return "کارت حساب اعتباري ندارد";
                    case 40:
                        return "عملیات درخواستی پشتیبانی نمی گردد";
                    case 41:
                        return "کارت مفقودي می باشد";
                    case 43:
                        return "کارت مسروقه می باشد";
                    case 45:
                        return "قبض قابل پرداخت نمی باشد";
                    default:
                        return "کد : " + resultCode;
                }
            }


            public static Boolean SetepaymentAmountForTest()
            {
                return false;
            }
            public static int GetDigitLenght()
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DigitLenght"]);
                }
                catch (Exception) { }
                return 0;
            }

            public static int GetDigitLenghtPOS()
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DigitLenghtPOS"]);
                }
                catch (Exception) { }
                return 0;
            }

            public static int GetDigitLenghtMembership()
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DigitLenghtMembership"]);
                }
                catch (Exception) { }
                return 0;
            }


            public static int GetTafziliProjectLenghtForTSCheckDigit()
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TafziliProjectLenght"]);
                }
                catch (Exception) { }
                return 0;
            }

            public static int GetTafziliMemberLenghtForTSCheckDigit()
            {
                try
                {
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TafziliMemberLenght"]);
                }
                catch (Exception) { }
                return 0;
            }

            #region MerchentId Methods
            /// <summary>
            /// مشخصه پرداخت الکترونیکی نظام مهندسی
            /// </summary>
            /// <returns></returns>
            public static String GetNezamMerchantId()
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["NezamMerchantId"];
                }
                catch (Exception) { }
                return "";
            }

            /// <summary>
            /// مشخصه پرداخت الکترونیکی نظام مهندسی
            /// </summary>
            /// <returns></returns>
            public static String GetNezamMerchantId(TSP.DataManager.TSAccountingAccType AccType)
            {
                string MerchantId = "";
                try
                {
                    switch (AccType)
                    {
                        case DataManager.TSAccountingAccType.DocMemberFile:
                            MerchantId = "A096";// System.Configuration.ConfigurationManager.AppSettings["EpayDocMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.Registeration_Entrance:
                            MerchantId = "J5F1";//System.Configuration.ConfigurationManager.AppSettings["EpayMemberMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.Registeration:
                            MerchantId = "J5F1";// System.Configuration.ConfigurationManager.AppSettings["EpayMemberMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.Entrance:
                            MerchantId = "J5F1";//System.Configuration.ConfigurationManager.AppSettings["EpayMemberMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.PeriodRegister:
                            MerchantId = "C090";//"AB6E";//"ADEE";//System.Configuration.ConfigurationManager.AppSettings["EpayAmoozeshMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.SeminarRegister:
                            MerchantId = "C090";//"ADEE";//System.Configuration.ConfigurationManager.AppSettings["EpayAmoozeshMerchantId"];
                            break;
                        case DataManager.TSAccountingAccType.MemberDebpt:
                            MerchantId = "AC49";
                            break;

                    }
                }
                catch (Exception) { }
                return MerchantId;
            }
            #endregion

            /// <summary>
            /// نتیجه پرداخت
            /// </summary>
            /// <param name="Code">کد نتیجه</param>
            /// <returns></returns>
            public static String PaymentResultCode(int Code)
            {
                String Result = "";
                switch (Code)
                {
                    case 100:
                        Result = "موفقیت تراکنش";
                        break;
                    case 110:
                        Result = "انصراف دارنده کارت";
                        break;
                    case 120:
                        Result = "موجودی حصاب کافی نیست";
                        break;
                    case 130:
                        Result = "اطلاعات کارت اشتباه است";
                        break;
                    case 131:
                        Result = "رمز کارت اشتباه است";
                        break;
                    case 132:
                        Result = "کارت مسدود شده است";
                        break;
                    case 133:
                        Result = "کارت منقضی شده است";
                        break;
                    case 140:
                        Result = "زمان مورد نظر به پایان رسیده است";
                        break;
                    case 150:
                        Result = "خطای داخلی بانک";
                        break;
                    case 160:
                        Result = "خطا در اطلاعات CVV2 یا ExpDate";
                        break;
                    case 166:
                        Result = "بانک صادرکننده کارت شما مجوز انجام تراکنش را صادر نکرده است";
                        break;
                    case 200:
                        Result = "مبلغ تراکنش بیشتر از سقف مجاز برای هر تراکنش می باشد";
                        break;
                    case 201:
                        Result = "مبلغ تراکنش بیشتر از سقف مجاز برای هر تراکنش می باشد";
                        break;
                    case 202:
                        Result = "مبلغ تراکنش بیشتر از سقف مجاز در ماه می باشد";
                        break;
                    default:
                        Result = "خطایی در پرداخت انجام گرفته است";
                        break;
                }
                return Result;
            }

            /// <summary>
            /// خطای تراکنش پرداخت 
            /// </summary>
            /// <param name="Code">کد خطا</param>
            /// <returns></returns>
            public static String PaymentTransactionConfirmErrorCode(int Code)
            {
                String Result = "";
                switch (Code)
                {
                    case -20:
                        Result = "وجود کاراکتر های غیر مجاز در درخواست";
                        break;
                    case -30:
                        Result = "تراکنش قبلا برگشت خورده است";
                        break;
                    case -50:
                        Result = "طول رشته درخواست غیر مجاز است";
                        break;
                    case -51:
                        Result = "خطا در درخواست";
                        break;
                    case -80:
                        Result = "تراکنش مورد نظر یافت نشد";
                        break;
                    case -81:
                        Result = "خطای داخلی بانک";
                        break;
                    case -90:
                        Result = "تراکنش قبلا تایید شده است";
                        break;
                    default:
                        Result = "خطایی در پرداخت انجام گرفته است";
                        break;
                }
                return Result;
            }

            #region Payment URL Methods
            /// <summary>
            /// آدرس وب سایت پرداخت الکترونیکی
            /// </summary>
            /// <returns></returns>
            public static String GetOnlinePaymentWebSiteAddress()
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["OnlinePaymentWebsite"];
                }
                catch (Exception) { }
                return "";
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="AmountPayment">مبلغ قابل پرداخت</param>
            /// <param name="CustomerIdPayment">شناسه پیگیری</param>
            /// <param name="RevertURLPayment"></param>
            /// <param name="PaymentId">AccountingId:From table [Ts.Accounting]</param>
            /// <returns></returns>
            //public static string FindBankURL(string AmountPayment, string CustomerIdPayment, string PaymentIdFish, TSP.DataManager.EpaymentType EpaymentType, TSP.DataManager.TSAccountingAccType AccountType, int TableId)
            //{

            //    string merchantId = GetNezamMerchantId(AccountType);
            //    TSP.Utility.OnlinePayment OnlinePayment = new Utility.OnlinePayment();
            //    string RevertURLPayment = OnlinePayment.GetRevertURL(EpaymentType, AccountType, TableId);             

            //    string BankURL = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
            //    BankURL += "";
            //    BankURL = BankURL + "?merchantId=" + merchantId + "&amount=" + AmountPayment + "&paymentId=" + PaymentIdFish + "&customerId=" + CustomerIdPayment + "&revertURL=" + RevertURLPayment;

            //    return BankURL;
            //}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="AmountPayment">مبلغ قابل پرداخت</param>
            /// <param name="CustomerIdPayment">شناسه پیگیری</param>
            /// <param name="RevertURLPayment"></param>
            /// <param name="PaymentId">AccountingId:From table [Ts.Accounting]</param>
            /// <returns></returns>
            public static string FindBankURL(string AmountPayment, string InvoiceNumber, string SpecialPaymentId, string PaymentId, TSP.DataManager.EpaymentType EpaymentType, TSP.DataManager.TSAccountingAccType AccountType, int TableId, string Description, ref string Token)
            {
                string merchantId = GetNezamMerchantId(AccountType);

                TSP.Utility.OnlinePayment OnlinePayment = new Utility.OnlinePayment();
                string RevertURLPayment = OnlinePayment.GetRevertURL(EpaymentType, AccountType, TableId);

                TSP.Utilities.EpaymentToken.TokensClient TokensClient = new Utilities.EpaymentToken.TokensClient();
                TSP.Utilities.EpaymentToken.tokenResponse tokenResponse = TokensClient.MakeToken(AmountPayment, merchantId, InvoiceNumber, PaymentId, SpecialPaymentId, RevertURLPayment, Description);
                Token = tokenResponse.token;
                string BankURL = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
                BankURL += "";
                BankURL = BankURL + "?Token=" + tokenResponse.token + "&merchantId=" + merchantId; //BankURL + "?merchantId=" + merchantId + "&amount=" + AmountPayment + "&paymentId=" + PaymentIdFish + "&customerId=" + CustomerIdPayment + "&revertURL=" + RevertURLPayment;

                return BankURL;
            }

            public static string GetRevertURL(TSP.DataManager.EpaymentType EpaymentType, TSP.DataManager.TSAccountingAccType AccountType, int TableId)
            {
                string URL = GetWebSiteAddress();
                string QS = TableId.ToString();
                switch (EpaymentType)
                {
                    case DataManager.EpaymentType.WizardNewMemberDoc:
                        URL += "/Members/Documents/WizardDocFinish.aspx?QS=" + QS;
                        break;
                    case DataManager.EpaymentType.WizardMemberRegistration:
                        URL += "/NezamRegister/WizardMemberFinish.aspx?QS=" + QS;
                        break;
                    case DataManager.EpaymentType.EpaymentForAllSite:
                        URL += "/Epayment/EpaymentVerify.aspx";
                        break;
                    case DataManager.EpaymentType.ParsianGetWay:
                        URL += "/Epayment/EpaymentVerifyParsian.aspx";
                        break;

                }
                return URL;
            }
            public static string ParsianIPGPageUrl
            {
                get
                {
                    return "https://pec.shaparak.ir/NewIPG/?token={0}";
                }
            }
            #endregion

            #region CheckCondition

            public static string CheckUserCanPayOnline(int AccType, int AccountingId, int FishPayerId = -2)
            {
                string Err = "";
                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new DataManager.TechnicalServices.AccountingManager();
                AccountingManager.FindByAccountingId(AccountingId);
                if (AccountingManager.Count != 1)
                {
                    return "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }

                if (Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
                {
                    return "فیش انتخاب شده پیش از این پرداخت شده است.";
                }
                TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new DataManager.TechnicalServices.AccountingDetailManager();
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new DataManager.PeriodRegisterManager();
                AccountingDetailManager.FindByAccountingId(AccountingId);
                switch (AccType)
                {
                    case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
                        #region
                        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new DataManager.PeriodPresentManager();
                        AccountingDetailManager.FindByAccountingId(AccountingId);
                        for (int i = 0; i < AccountingDetailManager.Count; i++)
                        {
                            PeriodRegisterManager.FindByCode(Convert.ToInt32(AccountingDetailManager[i]["TableId"]));
                            if (PeriodRegisterManager.Count != 1)
                            {
                                Err = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                                break;
                            }
                            if (Convert.ToInt32(PeriodRegisterManager[0]["InActive"]) == 1)
                            {
                                Err = "امکان پرداخت وجود ندارد.ثبت نام شما در دوره توسط مسئول واحد آموزش لغو شده است.";
                                break;
                            }
                            DataTable dtPP = PeriodPresentManager.SelectPeriodPresentForManagmentPage(Convert.ToInt32(PeriodRegisterManager[0]["PPId"]), -1, -1, -1);
                            if (dtPP.Rows.Count != 1)
                            {
                                Err = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                                break;
                            }
                            if (Convert.ToInt32(dtPP.Rows[0]["RemainCapacity"]) == 0)
                            {
                                Err = "ظرفیت " + dtPP.Rows[0]["PeriodTitle"].ToString() + " به پایان رسیده است.امکان پرداخت هزینه و تکمیل ثبت نام وجود ندارد.در صورتی که فیش مورد نظر مربوط به ثبت نام چندین دوره می باشد جهت پرداخت دوره ها بایستی مجددا از طریق لینک واحد آموزش>>ثبت نام دوره های آموزشی اقدام نمایید.";
                                break;
                            }

                            if (Convert.ToInt32(dtPP.Rows[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
                            {
                                if (Convert.ToInt32(PeriodRegisterManager[0]["RegisterType"]) == (int)TSP.DataManager.PeriodRegisterType.OnlyExam)
                                {
                                    if (Convert.ToInt32(dtPP.Rows[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.StartTest)
                                    {
                                        Err = "دوره" + dtPP.Rows[0]["PeriodTitle"].ToString() + "به پایان رسیده است.امکان پرداخت هزینه و تکمیل ثبت نام وجود ندارد.";
                                        break;
                                    }
                                }
                                else
                                {
                                    Err = "مهلت ثبت نام دوره" + dtPP.Rows[0]["PeriodTitle"].ToString() + "به پایان رسیده است.امکان پرداخت هزینه و تکمیل ثبت نام وجود ندارد.";
                                    break;
                                }
                            }

                        }
                        #endregion
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType.SeminarRegister:
                        #region
                        TSP.DataManager.SeminarManager SeminarManager = new DataManager.SeminarManager();
                        for (int i = 0; i < AccountingDetailManager.Count; i++)
                        {
                            PeriodRegisterManager.FindByCode(Convert.ToInt32(AccountingDetailManager[i]["TableId"]));
                            if (PeriodRegisterManager.Count != 1)
                            {
                                Err = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                                break;
                            }
                            if (Convert.ToInt32(PeriodRegisterManager[0]["InActive"]) == 1)
                            {
                                Err = "امکان پرداخت وجود ندارد.ثبت نام شما در سمینار توسط مسئول واحد آموزش لغو شده است.";
                                break;
                            }
                            SeminarManager.FindByCode(Convert.ToInt32(PeriodRegisterManager[0]["PPId"]));
                            if (SeminarManager.Count != 1)
                            {
                                Err = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                                break;
                            }

                            if (string.Compare(SeminarManager[0]["EndRegisterDate"].ToString(), GetDateOfToday()) < 0)
                            {
                                Err = "مهلت ثبت نام سمینار" + SeminarManager[0]["Subject"].ToString() + "به پایان رسیده است.امکان پرداخت هزینه و تکمیل ثبت نام وجود ندارد.";
                                break;
                            }

                        }
                        #endregion
                        break;
                    case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
                        DataTable dtDocMe = DocMemberFileManager.SelectMainRequest(FishPayerId, 0);
                        if (dtDocMe.Rows.Count > 0)
                        {
                            if ((dtDocMe.Rows[0]["TaskCode"] != null)
                               && (Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument
                               || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.NezamEmployeeInSettlementConfirmingDocument
                               || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.RoadAndurbanismConfirmingDocument
                               || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.PrintDocumentByNezamEmployee))
                            {
                                return "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان پرداخت فیش مربوطه وجود ندارد";
                            }
                            if ((dtDocMe.Rows[0]["TaskCode"] != null) && (Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess))
                            {
                                return "پروانه اشتغال به کار شما تایید شده می باشد،امکان پرداخت فیش مربوطه وجود ندارد";
                            }
                        }


                        break;
                }
                return Err;
            }
            #endregion

            public static String GetWebSiteAddress()
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["NezamFarsWebsiteAddress"];
                }
                catch (Exception) { }
                return "";
            }

            /// <summary>
            /// بدست آوردن شناسه پرداخت برای ارسال به بانک
            /// </summary>
            /// <param name="AccType"></param>
            /// <param name="TafziliCode">
            /// پنج درصد طراحی :شماره عضویت طراح
            /// صد در صد نظارت: شماره پروژه
            /// پنج درصد نظارت ساختمان و تاسیسات : شماره عضویت ناظر
            /// دو در هزار اجرا: شماره عضویت مجری
            /// وجوه پنج در هزار شناسه فنی ملکی : شماره پروژه
            /// </param>
            /// <param name="MemberRegisterationType">
            /// 0 : ثبت عضویت نمی باشد
            /// 1: عضویت موقت
            /// 2: عضویت دائم
            /// </param>
            /// <returns></returns>
            public static string GetPaymentId(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, int MemberRegisterationType = 0, int MunId = -2, Boolean KardanDesigner = false)
            {
                string PaymentId = "";
                string AccCode = "";
                if (MunId == 76)
                {
                    switch (AccType)
                    {
                        case TSP.DataManager.TSAccountingAccType.ObserversFiche:
                        case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                        case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                            AccCode = "3033";
                            break;
                        case TSP.DataManager.TSAccountingAccType.Designing5Percent:
                            if (KardanDesigner)
                                AccCode = "3040";
                            else
                                AccCode = "6181";
                            break;
                        case TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                            if (KardanDesigner)
                                AccCode = "3040";
                            else
                                AccCode = "6183";
                            break;
                        case TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                            if (KardanDesigner)
                                AccCode = "3040";
                            else
                                AccCode = "6182";
                            break;

                    }
                }
                else
                {
                    TSP.DataManager.TechnicalServices.AccTypeManager AccTypeManager = new DataManager.TechnicalServices.AccTypeManager();
                    AccTypeManager.FindByCode((int)AccType);
                    if (AccTypeManager.Count != 1) return "";
                    if (AccTypeManager[0]["AccCode"] == null || AccTypeManager[0]["AccCode"].ToString() == "")
                        return "";
                    AccCode = AccTypeManager[0]["AccCode"].ToString();
                    if (KardanDesigner)
                    {
                        switch (AccType)
                        {
                            case TSP.DataManager.TSAccountingAccType.Designing5Percent:
                            case TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                            case TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                                AccCode = "3040";
                                break;
                        }
                    }
                }


                int DigitLenght = 0;
                string CorrectTafzili = TafziliCode;
                if (MemberRegisterationType == 1 || MemberRegisterationType == 2)
                {
                    DigitLenght = GetDigitLenghtMembership();
                    if (DigitLenght <= 0)
                        return "";
                    if (TafziliCode.Length > DigitLenght)
                        return "";
                    else if (TafziliCode.Length < DigitLenght)
                    {
                        int DiffLenght = DigitLenght - TafziliCode.Length;
                        for (int i = 0; i < DiffLenght; i++)
                        {
                            CorrectTafzili = "0" + CorrectTafzili;
                        }
                    }
                }
                else
                    DigitLenght = GetDigitLenght();

                //****Check Digit********
                string PaymentSting = AccCode + CorrectTafzili;
                if (PaymentSting.Length > 17)
                    return "";
                int difLenght = 17 - PaymentSting.Length;
                for (int i = 0; i < difLenght; i++)
                {
                    PaymentSting = "0" + PaymentSting;
                }
                //PaymentSting = "13783566293615049";
                //PaymentId = "13783566293615049" + GetCheckDigit(PaymentSting);
                PaymentId = AccCode + CorrectTafzili + GetCheckDigit(PaymentSting);
                if (MemberRegisterationType == 2)
                    PaymentId = "";
                return PaymentId;
            }
            /// <summary>
            /// بدست آوردن شناسه پرداخت
            /// </summary>
            /// <param name="AccType"></param>
            /// <param name="TafziliCode"></param>
            /// <param name="MemberRegisterationType"></param>
            /// <returns></returns>
            public static string GetPaymentId(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, int MemberRegisterationType = 0)
            {
                return GetPaymentId(AccType, TafziliCode, MemberRegisterationType, -2, false);
            }
            public static string GetPaymentId(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, int MemberRegisterationType = 0, int MunId = -2)
            {
                return GetPaymentId(AccType, TafziliCode, MemberRegisterationType, MunId, false);
            }
            #region PaymentId PCPos
            /// <summary>
            /// 
            /// </summary>
            /// <param name="AccType"></param>
            /// <param name="TafziliCode">
            /// پنج درصد طراحی :شماره عضویت طراح
            /// صد در صد نظارت: شماره پروژه
            /// پنج درصد نظارت ساختمان و تاسیسات : شماره عضویت ناظر
            /// دو در هزار اجرا: شماره عضویت مجری
            /// وجوه پنج در هزار شناسه فنی ملکی : شماره پروژه
            /// </param>
            /// <param name="MemberRegisterationType">
            /// 0 : ثبت عضویت نمی باشد
            /// 1: عضویت موقت
            /// 2: عضویت دائم
            /// </param>
            /// <returns></returns>
            public static string GetPaymentIdForPOS(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, string TafziliCodeProvince, Boolean IsProvinceCodeGenerate, int MemberRegisterationType = 0, int MunId = -2, Boolean KardanDesigner = false)
            {
                if (!IsProvinceCodeGenerate)
                {
                    #region شناسه کارت خوان
                    string PaymentId = "";
                    string AccCode = "";
                    if (KardanDesigner)
                    {
                        AccCode = "3040";
                    }
                    else
                    {
                        if (MunId == 76)
                        {
                            switch (AccType)
                            {
                                case TSP.DataManager.TSAccountingAccType.ObserversFiche:
                                case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                                case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                                    AccCode = "3033";
                                    break;
                                case TSP.DataManager.TSAccountingAccType.Designing5Percent:
                                    AccCode = "6181";
                                    break;
                                case TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                                    AccCode = "6183";
                                    break;
                                case TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                                    AccCode = "6182";
                                    break;

                            }
                        }
                        else
                        {
                            TSP.DataManager.TechnicalServices.AccTypeManager AccTypeManager = new DataManager.TechnicalServices.AccTypeManager();
                            AccTypeManager.FindByCode((int)AccType);
                            if (AccTypeManager.Count != 1) return "";
                            if (AccTypeManager[0]["AccCode"] == null || AccTypeManager[0]["AccCode"].ToString() == "")
                                return "";
                            AccCode = AccTypeManager[0]["AccCode"].ToString();
                        }
                    }
                    int DigitLenght = 0;
                    string CorrectTafzili = TafziliCode;
                    if (MemberRegisterationType == 1 || MemberRegisterationType == 2)
                    {
                        AccCode = "0000" + AccCode;
                        DigitLenght = GetDigitLenghtMembership();
                        if (DigitLenght <= 0)
                            return "";
                        if (TafziliCode.Length > DigitLenght)
                            return "";
                        else if (TafziliCode.Length < DigitLenght)
                        {
                            int DiffLenght = DigitLenght - TafziliCode.Length;
                            for (int i = 0; i < DiffLenght; i++)
                            {
                                CorrectTafzili = "0" + CorrectTafzili;
                            }
                        }
                    }
                    else
                        DigitLenght = GetDigitLenghtPOS();

                    //****Check Digit********
                    string PaymentSting = AccCode + CorrectTafzili;
                    if (PaymentSting.Length > 19)
                        return "";
                    int LenPaymentString = PaymentSting.Length;
                    int difLenght = 20 - PaymentSting.Length;
                    for (int i = 0; i < difLenght; i++)
                    {
                        PaymentSting = "0" + PaymentSting;
                    }
                    // LenPaymentString = 19;
                    // PaymentSting = "05805301234567890123";
                    // PaymentId = "13783566293615049" + GetCheckDigit(PaymentSting);
                    PaymentId = AccCode + CorrectTafzili + GetCheckDigitPOS(PaymentSting, LenPaymentString);
                    if (MemberRegisterationType == 2)
                        PaymentId = "";
                    return PaymentId;
                    #endregion
                }
                else
                {
                    return GetPaymentIdForProvince(AccType, TafziliCodeProvince, MunId, KardanDesigner);

                }
            }
            //public static string GetPaymentIdForPOS(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode,string TafziliCodeProvince, int MemberRegisterationType = 0, int MunId = -2)
            //{
            //    return GetPaymentIdForPOS(AccType, TafziliCode, TafziliCodeProvince, MemberRegisterationType, MunId, false);
            //}
            //public static string GetPaymentIdForPOS(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode,string TafziliCodeProvince, int MemberRegisterationType = 0)
            //{
            //    return GetPaymentIdForPOS(AccType, TafziliCode, TafziliCodeProvince, MemberRegisterationType, -2);
            //}

            public static string GetPaymentIdForPOS(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, string TafziliCodeProvince, Boolean IsProvinceCodeGenerate)
            {
                return GetPaymentIdForPOS(AccType, TafziliCode, TafziliCodeProvince, IsProvinceCodeGenerate, 0, -2);
            }
            #endregion
            #region PaymentId Province
            public static string GetPaymentIdForProvince(TSP.DataManager.TSAccountingAccType AccType, string TafziliCode, int MunId = -2, Boolean KardanDesigner = false)
            {
                string PaymentId = "";
                string AccCode = "";
                if (KardanDesigner)
                {
                    switch (AccType)
                    {
                        case TSP.DataManager.TSAccountingAccType.Designing5Percent:
                        case TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                        case TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                            AccCode = "3040";
                            break;
                        case TSP.DataManager.TSAccountingAccType.ObserversFiche:
                        case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                        case TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                            AccCode = "3030";
                            break;
                    }
                }
                else
                {
                    if (MunId == 76 &&
                        (AccType == TSP.DataManager.TSAccountingAccType.ObserversFiche
                        || AccType == TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation
                        || AccType == TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure))//نظارت صدرا
                    {
                        AccCode = "3033";

                    }
                    else
                    {
                        TSP.DataManager.TechnicalServices.AccTypeManager AccTypeManager = new DataManager.TechnicalServices.AccTypeManager();
                        AccTypeManager.FindByCode((int)AccType);
                        if (AccTypeManager.Count != 1) return "";
                        if (AccTypeManager[0]["AccCode"] == null || AccTypeManager[0]["AccCode"].ToString() == "")
                            return "";
                        AccCode = AccTypeManager[0]["AccCode"].ToString();
                    }
                }
                int DigitLenght = 17;

                //****Check Digit********
                string PaymentSting = AccCode + TafziliCode;
                if (PaymentSting.Length > DigitLenght)
                    return "";
                int LenPaymentString = PaymentSting.Length;
                int difLenght = DigitLenght - PaymentSting.Length;
                for (int i = 0; i < difLenght; i++)
                {
                    PaymentSting = "0" + PaymentSting;
                }
                PaymentId = AccCode + TafziliCode + GetCheckDigit(PaymentSting);
                return PaymentId;
            }
            #endregion
            public static string GetCheckDigit(string PaymentSting)
            {
                string CheckDigit = "";
                int[] CheckArray = CheckDigitArray;
                int sum = 0;
                for (int i = 0; i < CheckArray.Length; i++)
                {
                    sum += CheckArray[i] * Convert.ToInt32(PaymentSting[i].ToString());
                }
                if (sum < 99)
                    return sum.ToString();
                //////////else if (sum == 99)
                //////////    return "01";
                int Remain = 0;
                int Division = Math.DivRem(sum, 99, out Remain);
                CheckDigit = Remain.ToString();
                if (CheckDigit.Length == 1)
                    CheckDigit = "0" + CheckDigit;
                return CheckDigit;
            }

            public static string GetCheckDigitPOS(string PaymentSting, int LenPaymentString)
            {
                PaymentSting = PaymentSting + "01";
                string CheckDigit = "";
                int[] CheckArray = CheckDigitPOSArray;
                int sum = 0;
                for (int i = 0; i < CheckArray.Length; i++)
                {
                    sum += CheckArray[i] * Convert.ToInt32(PaymentSting[i].ToString());
                }
                //if (sum < 99)
                //    return sum.ToString();
                //////////else if (sum == 99)
                //////////    return "01";
                int Remain = 0;
                int Division = Math.DivRem(sum, 11, out Remain);
                if (Remain == 0 || Remain == 1)
                    Remain = 0;
                else
                    Remain = 11 - Remain;
                string CheckDigit1 = Remain.ToString();
                Int64 CheckDigit2 = 0;
                Int64 devision2 = Math.DivRem(Convert.ToInt64(LenPaymentString), 10, out CheckDigit2);
                CheckDigit = CheckDigit1 + CheckDigit2.ToString();// Remain.ToString();
                if (CheckDigit.Length == 1)
                    CheckDigit = "0" + CheckDigit;
                return CheckDigit;
            }

            public static string GetTafziliCodeOfPaymentIdForTS(TSP.DataManager.TSAccountingAccType AccType, string ProjectId, string MeId)
            {
                string TafziliCode = "";
                int TafziliProjectLenght = GetTafziliProjectLenghtForTSCheckDigit();
                int TafziliMemberLenght = GetTafziliMemberLenghtForTSCheckDigit();
                int DiffLenght = TafziliProjectLenght - ProjectId.Length;
                for (int i = 0; i < DiffLenght; i++)
                {
                    ProjectId = "0" + ProjectId;
                }
                DiffLenght = TafziliMemberLenght - MeId.Length;
                for (int i = 0; i < DiffLenght; i++)
                {
                    MeId = "0" + MeId;
                }
                TafziliCode = ProjectId + MeId;
                return TafziliCode;
            }

            public static string GetTafziliCodeOfPaymentIdForTSProvince(TSP.DataManager.TSAccountingAccType AccType, string AgentCode, string ProjectId, string MeId)
            {
                string TafziliCode = "";
                int TafziliProjectLenght = 6;
                int TafziliMemberLenght = 5;
                int DiffLenght = TafziliProjectLenght - ProjectId.Length;
                for (int i = 0; i < DiffLenght; i++)
                {
                    ProjectId = "0" + ProjectId;
                }
                DiffLenght = TafziliMemberLenght - MeId.Length;
                for (int i = 0; i < DiffLenght; i++)
                {
                    MeId = "0" + MeId;
                }
                TafziliCode = AgentCode + ProjectId + MeId;
                return TafziliCode;
            }

            public static string GetTafziliCodeOfPaymentIdForDocument(TSP.DataManager.TSAccountingAccType AccType, TSP.DataManager.DocumentOfMemberRequestType DocumentOfMemberRequestType, string MeId)
            {
                string DocumentMemberRequestTypeCode = GetDocumentMemberRequestTypeCode(DocumentOfMemberRequestType);                        
                string TafziliCode = "";
                int TafziliMemberLenght = 5;
                int DiffLenght = TafziliMemberLenght - MeId.Length;
                for (int i = 0; i < DiffLenght; i++)
                {
                    MeId = "0" + MeId;
                }
                TafziliCode =  DocumentMemberRequestTypeCode.ToString() + "0001" + MeId;
                return TafziliCode;
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

        }

    }

}