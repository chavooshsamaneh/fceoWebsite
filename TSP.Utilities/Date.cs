using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


public partial class Utility
{
    public class Date
    {
        #region StaticMethods
        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static DateTime ShamsiToMiladi(int Year, int Month, int Day)
        {
            PersianCalendar FC = new PersianCalendar();
            return FC.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static int[] MiladiToShamsi(int Year, int Month, int Day)
        {
            DateTime DT = new DateTime(Year, Month, Day);
            PersianCalendar FC = new PersianCalendar();
            int[] PDate = new int[3];
            PDate[0] = FC.GetYear(DT);
            PDate[1] = FC.GetMonth(DT);
            PDate[2] = FC.GetDayOfMonth(DT);
            return PDate;
        }

        /// <summary>
        /// محاسبه تعداد روز ها بین دو تاریخ شمسی
        /// </summary>
        /// <param name="PersianDateFrom"></param>
        /// <param name="PersianDateTo"></param>
        /// <returns></returns>
        public static int TotalDaysBetween2PersianDates(String PersianDateFrom, String PersianDateTo)
        {
            String[] StrDateFrom = PersianDateFrom.Split('/');
            DateTime DateFrom = ShamsiToMiladi(int.Parse(StrDateFrom[0]), int.Parse(StrDateFrom[1]), int.Parse(StrDateFrom[2]));
            String[] StrDateTo = PersianDateTo.Split('/');
            DateTime DateTo = ShamsiToMiladi(int.Parse(StrDateTo[0]), int.Parse(StrDateTo[1]), int.Parse(StrDateTo[2]));
            TimeSpan ts = DateTo.Subtract(DateFrom);
            return (int)ts.TotalDays;
        }

        /// <summary>
        /// محاسبه تعداد سال بین دو تاریخ شمسی
        /// </summary>
        /// <param name="PersianDateFrom"></param>
        /// <param name="PersianDateTo"></param>
        /// <returns></returns>
        public static int TotalYearsBetween2PersianDates(String PersianDateFrom, String PersianDateTo)
        {
            String[] StrDateFrom = PersianDateFrom.Split('/');
            DateTime DateFrom = ShamsiToMiladi(int.Parse(StrDateFrom[0]), int.Parse(StrDateFrom[1]), int.Parse(StrDateFrom[2]));
            String[] StrDateTo = PersianDateTo.Split('/');
            DateTime DateTo = ShamsiToMiladi(int.Parse(StrDateTo[0]), int.Parse(StrDateTo[1]), int.Parse(StrDateTo[2]));
            int Years = int.Parse(StrDateTo[0]) - int.Parse(StrDateFrom[0]);
            return Years;
        }



        /// <summary>
        /// محاسبه تعداد ماه بین دو تاریخ شمسی
        /// </summary>
        /// <param name="PersianDateFrom"></param>
        /// <param name="PersianDateTo"></param>
        /// <returns></returns>
        public static int TotalMonthsBetween2PersianDates(String PersianDateFrom, String PersianDateTo)
        {
            if (string.IsNullOrWhiteSpace(PersianDateFrom) || string.IsNullOrWhiteSpace(PersianDateTo)) return 0;
            String[] StrDateFrom = PersianDateFrom.Split('/');
            DateTime DateFrom = ShamsiToMiladi(int.Parse(StrDateFrom[0]), int.Parse(StrDateFrom[1]), int.Parse(StrDateFrom[2]));
            String[] StrDateTo = PersianDateTo.Split('/');
            DateTime DateTo = ShamsiToMiladi(int.Parse(StrDateTo[0]), int.Parse(StrDateTo[1]), int.Parse(StrDateTo[2]));
            int Years = int.Parse(StrDateTo[0]) - int.Parse(StrDateFrom[0]);
            int months = 0;
            if (Years == 0)
            {
                months = int.Parse(StrDateTo[1]) - int.Parse(StrDateFrom[1]) == 0 ? 1 : int.Parse(StrDateTo[1]) - int.Parse(StrDateFrom[1]);
            }
            else
            {
                months = Years * 12;
               // months += int.Parse(StrDateTo[1]) - int.Parse(StrDateFrom[1]) == 0 ? 1 : int.Parse(StrDateTo[1]) - int.Parse(StrDateFrom[1]);
            }
            return months;
        }

        /// <summary>
        /// محاسبه تعداد روز ها بین دو تاریخ میلادی
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        public static int TotalDaysBetween2Dates(DateTime DateFrom, DateTime DateTo)
        {
            TimeSpan ts = DateTo.Subtract(DateFrom);
            return (int)ts.TotalDays;
        }

        /// <summary>
        /// بازگرداندن تاریخ آخرین روز سال به فرمت : روز/ماه/سال
        /// </summary>
        /// <returns></returns>
        public static String Get_LastDateOfYear(int Year)
        {
            return Year + "/12/" + (new System.Globalization.PersianCalendar()).GetDaysInMonth(Year, 12);
        }
        #endregion

        private PersianCalendar FC;
        private DateTime DT;

        private int _Year;
        public int Year
        {
            get { return _Year; }
        }
        private int _Month;
        public int Month
        {
            get { return _Month; }
        }
        private String _MonthName;
        public String MonthName
        {
            get { return _MonthName; }
        }
        private int _Day;
        public int Day
        {
            get { return _Day; }
        }

        #region Constructor
        /// <summary>
        /// تاریخ امروز به عنوان تاریخ پیش فرض در نظر گرفته می شود
        /// </summary>
        public Date()
        {
            FC = new PersianCalendar();
            DT = DateTime.Now;
            Load_Date();
        }

        /// <summary>
        /// تاریخ را براساس DateTime تنظیم می کند
        /// </summary>
        /// <param name="dt"></param>
        public Date(DateTime dt)
        {
            FC = new PersianCalendar();
            this.DT = dt;
            Load_Date();
        }

        /// <summary>
        /// تاریخ را براساس سال،ماه و روز میلادی تنظیم می کند
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        public Date(int Year, int Month, int Day)
        {
            FC = new PersianCalendar();
            DT = new DateTime(Year, Month, Day);
            Load_Date();
        }

        /// <summary>
        /// تاریخ را براساس یک رشته تاریخ شمسی تنظیم می کند
        /// </summary>
        /// <param name="PersianDate"></param>
        public Date(String PersianDate)
        {
            String[] str = PersianDate.Split('/');
            DT = ShamsiToMiladi(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
            FC = new PersianCalendar();
            Load_Date();
        }
        #endregion

        /// <summary>
        /// بازگرداندن تاریخ به فرمت : روز/ماه/سال
        /// </summary>
        /// <returns></returns>
        public String Get_ShortDate()
        {
            return FC.GetYear(DT) + "/" + FC.GetMonth(DT).ToString().PadLeft(2, '0') + "/" + FC.GetDayOfMonth(DT).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// بازگرداندن تاریخ به فرمت : نام هفته   روز   نام ماه   سال
        /// </summary>
        /// <returns></returns>
        public String Get_LongDate()
        {
            return Get_WeekDay(FC.GetDayOfWeek(DT)) + " " + FC.GetDayOfMonth(DT) + " " + Get_MonthName(FC.GetMonth(DT)) + " " + FC.GetYear(DT);
        }

        /// <summary>
        /// بازگرداندن تاریخ به فرمت : روز   نام ماه   سال
        /// </summary>
        /// <returns></returns>
        public String Get_Date()
        {
            return FC.GetDayOfMonth(DT) + " " + Get_MonthName(FC.GetMonth(DT)) + " " + FC.GetYear(DT);
        }

        /// <summary>
        /// اضافه کردن روز به تاریخ شمسی، بازگرداندن تاریخ شمسی به فرمت روز/ماه/سال 
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public String AddDays(int Days)
        {
            DateTime DtAddDays = FC.AddDays(DT, Days);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            return PDate.GetYear(DtAddDays) + "/" + PDate.GetMonth(DtAddDays).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddDays).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// اضافه کردن ماه به تاریخ شمسی، بازگرداندن تاریخ شمسی به فرمت روز/ماه/سال 
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public String AddMonths(int Months)
        {
            DateTime DtAddMonths = FC.AddMonths(DT, Months);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            return PDate.GetYear(DtAddMonths) + "/" + PDate.GetMonth(DtAddMonths).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddMonths).ToString().PadLeft(2, '0');
        }


        /// <summary>
        /// اضافه کردن سال به تاریخ شمسی، بازگرداندن تاریخ شمسی به فرمت روز/ماه/سال 
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public String AddYears(int Years)
        {
            DateTime DtAddYears = FC.AddYears(DT, Years);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            return PDate.GetYear(DtAddYears) + "/" + PDate.GetMonth(DtAddYears).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYears).ToString().PadLeft(2, '0');
        }

        private void Load_Date()
        {
            _Year = FC.GetYear(DT);
            _Month = FC.GetMonth(DT);
            _MonthName = Get_MonthName(Month);
            _Day = FC.GetDayOfMonth(DT);
        }

        private String Get_WeekDay(DayOfWeek Weekday)
        {
            String day = "";
            switch (Weekday)
            {
                case DayOfWeek.Saturday:
                    day = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    day = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    day = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    day = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    day = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday:
                    day = "پنج شنبه";
                    break;
                case DayOfWeek.Friday:
                    day = "جمعه";
                    break;
            }
            return day;
        }

        private String Get_MonthName(int MonthNo)
        {
            String Month = "";
            switch (MonthNo)
            {
                case 1:
                    Month = "فروردین";
                    break;
                case 2:
                    Month = "اردیبهشت";
                    break;
                case 3:
                    Month = "خرداد";
                    break;
                case 4:
                    Month = "تیر";
                    break;
                case 5:
                    Month = "مرداد";
                    break;
                case 6:
                    Month = "شهریور";
                    break;
                case 7:
                    Month = "مهر";
                    break;
                case 8:
                    Month = "آبان";
                    break;
                case 9:
                    Month = "آذر";
                    break;
                case 10:
                    Month = "دی";
                    break;
                case 11:
                    Month = "بهمن";
                    break;
                case 12:
                    Month = "اسفند";
                    break;
            }
            return Month;
        }
    }
}

