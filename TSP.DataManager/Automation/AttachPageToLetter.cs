using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.Automation
{
    /// <summary>
    /// اضافه کردن صفحه به سند اتوماسیون اداری
    /// </summary>
    public class AttachPageToLetter
    {
        #region ClassProperties
        private String _Message;
        /// <summary>
        /// پیغام سیستم
        /// </summary>
        public String Message { get { return _Message; } }

        private Boolean _SaveState;
        /// <summary>
        /// وضعیت انجام عملیات ذخیره (True: انجام شد  ,  False: خطایی در عملیات انجام گرفته است)
        /// </summary>
        public Boolean SaveState { get { return _SaveState; } }

        private static String _QueryName = "APTL";
        /// <summary>
        /// نام پارامتر صفحه
        /// </summary>
        public static String QueryName { get { return _QueryName; } }

        private int LetterId;
        #endregion

        #region Constructor
        public AttachPageToLetter()
        {
            this._Message = "";
            this._SaveState = false;
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// ذخیره اطلاعات --> اضافه کردن صفحه به سند
        /// </summary>
        /// <param name="LetterNumber">شماره سند</param>
        /// <param name="PageAddress">آدرس صفحه</param>
        /// <param name="QueryString">پارامترهای صفحه</param>
        /// <param name="LinkName">نام لینک</param>
        /// <param name="TimeOut">مدت اعتبار</param>
        /// <param name="UserId">کد کاربری</param>
        /// <returns></returns>
        public Boolean AttachPage(String LetterNumber, String PageAddress, String QueryString, String LinkName, int TimeOut, int UserId)
        {
            try
            {
                if (CheckLetterInCartable(LetterNumber, UserId))
                    InsertLetterAttachPage(LetterNumber, PageAddress, QueryString, LinkName, TimeOut, UserId);
            }
            catch (Exception)
            {
                this._SaveState = false;
                this._Message = "خطایی در عملیات ذخیره انجام گرفته است.";
            }
            return this._SaveState;
        }

        /// <summary>
        /// گرفتن مقدار پارامتر صفحه
        /// </summary>
        /// <returns></returns>
        public static String GetPageParameter()
        {
            return _QueryName + "=" + GetDateOfToday();
        }

        /// <summary>
        /// بررسی مقدار پارامتر صفحه (True: پارامتر صحیح است  ,  False: پارامتر نامعتبر است)
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Boolean CheckPageParameterValue(String Value)
        {
            if (Value == null || String.IsNullOrEmpty(Value)) return false;

            if (String.Compare(Value, GetDateOfToday()) != 0)
                return false;

            return true;
        }
        #endregion

        #region PrivateMethods
        private Boolean CheckLetterInCartable(String LetterNumber, int UserId)
        {
            LettersManager LetterManager = new LettersManager();
            LetterManager.FindByLetterNumber(LetterNumber);
            if (LetterManager.Count > 0)
            {
                this.LetterId = int.Parse(LetterManager[0]["LetterId"].ToString());
                CartablesManager CartableManager = new CartablesManager();
                CartableManager.FindByCartableUserIdAndLetter(UserId, LetterId);
                if (CartableManager.Count > 0)
                    return true;
                else
                {
                    _SaveState = false;
                    this._Message = "چنین سندی در کارتابل شما وجود ندارد.";
                }
            }
            else
            {
                _SaveState = false;
                _Message = "شماره سند وارد شده اشتباه است.";
            }
            return false;
        }

        private void InsertLetterAttachPage(String LetterNumber, String PageAddress, String QueryString, String LinkName, int TimeOut, int UserId)
        {
            LetterAttachPagesManager AttachPagesManager = new LetterAttachPagesManager();
            System.Data.DataRow dr = AttachPagesManager.NewRow();
            dr["LetterId"] = this.LetterId;
            dr["PageAddress"] = PageAddress.Trim();
            if (String.IsNullOrEmpty(QueryString.Trim()))
                dr["QueryString"] = DBNull.Value;
            else
                dr["QueryString"] = QueryString;
            if (String.IsNullOrEmpty(LinkName.Trim()))
                dr["LinkName"] = "لینک";
            else
                dr["LinkName"] = LinkName;
            dr["TimeOut"] = TimeOut;
            dr["AddDate"] = GetDateOfToday();
            dr["AddTime"] = GetCurrentTime();
            dr["UserId"] = UserId;
            dr["ModifiedDate"] = DateTime.Now;
            AttachPagesManager.AddRow(dr);
            AttachPagesManager.Save();
            AttachPagesManager.DataTable.AcceptChanges();
            this._SaveState = true;
            //this._Message = "صفحه به سند شماره " + LetterNumber + " اضافه شد.";
            this._Message = "صفحه با موفقیت به سند اضافه شد. \nشماره سند : " + LetterNumber;// + " اضافه شد.";

        }

        private string GetCurrentTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
        }

        private static string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }
        #endregion
    }
}
